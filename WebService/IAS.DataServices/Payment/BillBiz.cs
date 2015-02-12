using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using IAS.DAL;
using System.Transactions;
using IAS.Utils;
using IAS.DataServices.Payment.Helpers;
using System.Globalization;
using Oracle.DataAccess.Client;
using System.Configuration;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Properties;
using IAS.Common.Logging;
using IAS.DataServices.Payment.States;
using IAS.DataServices.Payment.Messages;
using System.Drawing;
using IAS.DataServices.Payment.Mapper;
using System.Threading.Tasks;
using IAS.DataServices.Payment.Receipts;


namespace IAS.DataServices.Payment
{
    public class BillBiz : IDisposable
    {

        private IEnumerable<AG_IAS_TEMP_PAYMENT_DETAIL> GetBankPaymentDetailRequest(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.ImportBankTransferRequest request)
        {
            IEnumerable<AG_IAS_TEMP_PAYMENT_DETAIL> datalist = ctx.AG_IAS_TEMP_PAYMENT_DETAIL.Where(w => w.HEADER_ID == request.GroupId);
            return datalist.Where(w => w.HEADER_ID == request.GroupId && w.STATUS != (int)DTO.ImportPaymentStatus.Invalid);
        }

        private DTO.ResponseService<BankTransactionTempData> MapReceiveDataBank(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.ImportBankTransferRequest request)
        {
            var res = new DTO.ResponseService<BankTransactionTempData>();

            AG_IAS_TEMP_PAYMENT_HEADER tmpPaymentHeader = ctx.AG_IAS_TEMP_PAYMENT_HEADER.SingleOrDefault(a => a.ID == request.GroupId);

            if (tmpPaymentHeader == null)
                throw new ApplicationException("ไม่พบข้อมูลที่ระบุ");

            AG_IAS_PAYMENT_HEADER paymentHeader = new AG_IAS_PAYMENT_HEADER();
            tmpPaymentHeader.MappingToEntity<AG_IAS_TEMP_PAYMENT_HEADER, AG_IAS_PAYMENT_HEADER>(paymentHeader);
            ctx.AG_IAS_PAYMENT_HEADER.AddObject(paymentHeader);

            BankTransactionTempData bankTransaction = BankTransactionDataFactory.ConCreate();
            try
            {
                // ********************* Update Change Request **************************//
                bankTransaction.Details = ctx.AG_IAS_TEMP_PAYMENT_DETAIL.Where(w => w.HEADER_ID == request.GroupId);

                IList<AG_IAS_TEMP_PAYMENT_DETAIL_HIS> detailHistory = new List<AG_IAS_TEMP_PAYMENT_DETAIL_HIS>();
                foreach (var item in bankTransaction.Details)
                {
                    DTO.ImportBankTransferData data = request.ImportBankTransfers.FirstOrDefault(a => a.Id == item.ID);
                    if (data != null)
                    {
                        if (data.Status == (int)DTO.ImportPaymentStatus.MissingRefNo)
                        {
                            AG_IAS_TEMP_PAYMENT_DETAIL_HIS importDetailTemp = new AG_IAS_TEMP_PAYMENT_DETAIL_HIS();

                            item.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL, AG_IAS_TEMP_PAYMENT_DETAIL_HIS>(importDetailTemp);
                            importDetailTemp.HIS_ID = DAL.OracleDB.GetGenAutoId();

                            ctx.AG_IAS_TEMP_PAYMENT_DETAIL_HIS.AddObject(importDetailTemp);

                            item.CUSTOMER_NO_REF1 = data.ChangeRef1;
                            item.STATUS = (int)DTO.ImportPaymentStatus.ChangeData;
                            //dataMissRefNos.Add(data);
                            detailHistory.Add(importDetailTemp);

                            AG_IAS_PAYMENT_DETAIL_HIS importDetailHis = importDetailTemp.ConvertToAG_IAS_PAYMENT_DETAIL_HIS();


                            ctx.AG_IAS_PAYMENT_DETAIL_HIS.AddObject(importDetailHis);
                        }
                        else
                        {
                            item.STATUS = (int)DTO.ImportPaymentStatus.Valid;
                        }
                    }


                    AG_IAS_PAYMENT_DETAIL importDetail = item.ConvertToAG_IAS_PAYMENT_DETAIL();


                    ctx.AG_IAS_PAYMENT_DETAIL.AddObject(importDetail);
                }

                // ********************************************************************//




                //******* หา Header ข้อมูล Bank ****************************//
                bankTransaction.Header = ctx.AG_IAS_TEMP_PAYMENT_HEADER
                                                       .SingleOrDefault(s => s.ID == request.GroupId);

                if (bankTransaction.Header == null)
                {
                    res.ErrorMsg = Resources.errorBillBiz_001 + request.GroupId;
                    return res;
                }

                DateTime effDate = ParseDateFromString.ParseDateHeaderBank(bankTransaction.Header.EFFECTIVE_DATE);
                if (effDate == DateTime.MinValue)
                {
                    res.ErrorMsg = Resources.errorBillBiz_002 + bankTransaction.Header.ID;
                    return res;
                }

                //*****************************************************************//

                //************************* บันทึกข้อมูล ส่วน Detail **********************//

                //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
                IEnumerable<AG_IAS_TEMP_PAYMENT_DETAIL> transferbankDetails = bankTransaction.Details.Where(w => w.STATUS != (int)DTO.ImportPaymentStatus.Invalid
                                                                                                            && w.STATUS != (int)DTO.ImportPaymentStatus.MissingRefNo
                                                                                                            && w.STATUS != (int)DTO.ImportPaymentStatus.Paid);
                //วนทำงานทีละรายการ
                foreach (AG_IAS_TEMP_PAYMENT_DETAIL bankPaymentDetail in transferbankDetails)
                {

                    //หารายการ Payment ที่ตรงกับ Ref1 ของข้อมูลธนาคาร
                    AG_IAS_PAYMENT_G_T groupPayment = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(f => f.GROUP_REQUEST_NO == bankPaymentDetail.CUSTOMER_NO_REF1.Trim());

                    // ตรวจสอบสถานะ ใบสั่งจ่าย
                    if (groupPayment == null)
                    {
                        res.ErrorMsg = Resources.errorBillBiz_003 + bankPaymentDetail.CUSTOMER_NO_REF1;
                        return res;
                    }
                    else
                    {
                        if (groupPayment.STATUS == PaymentStatus.P.ToString())
                        {
                            res.ErrorMsg = Resources.errorBillBiz_004;
                            return res;
                        }
                    }



                    if (groupPayment.STATUS == PaymentStatus.W.ToString() || String.IsNullOrEmpty(groupPayment.STATUS))
                    {
                        if (groupPayment.GROUP_AMOUNT == PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT))
                        {
                            groupPayment.STATUS = PaymentStatus.M.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT < PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT))
                        {
                            groupPayment.STATUS = PaymentStatus.U.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT > PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT))
                        {
                            groupPayment.STATUS = PaymentStatus.L.ToString();
                        }
                    }
                    else
                    {
                        IEnumerable<AG_IAS_SUBPAYMENT_RECEIPT> receipts = ctx.AG_IAS_SUBPAYMENT_RECEIPT.Where(a => a.GROUP_REQUEST_NO == groupPayment.GROUP_REQUEST_NO);
                        decimal paid = 0;
                        if (receipts != null)
                        {
                            paid = (Decimal)receipts.Sum(a => a.AMOUNT);
                        }
                        decimal amount = (PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT) - paid);

                        if (groupPayment.GROUP_AMOUNT == amount)
                        {
                            groupPayment.STATUS = PaymentStatus.M.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT < amount)
                        {
                            groupPayment.STATUS = PaymentStatus.U.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT > amount)
                        {
                            groupPayment.STATUS = PaymentStatus.L.ToString();
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(bankPaymentDetail.CHEQUE_NO))
                        groupPayment.CHEQUE_NO = bankPaymentDetail.CHEQUE_NO;

                    groupPayment.PAYMENT_DATE = ParseDateFromString.ParseDateHeaderBank(bankPaymentDetail.PAYMENT_DATE);// effDate;
                    groupPayment.UPDATED_DATE = DateTime.Now;

                }
                //***************************************************************************//


                //**************************** บันทึกข้อมูลส่วน Total **********************************//

                bankTransaction.Total = ctx.AG_IAS_TEMP_PAYMENT_TOTAL
                                          .SingleOrDefault(s => s.HEADER_ID == request.GroupId);

                if (bankTransaction.Total == null)
                {
                    res.ErrorMsg = Resources.errorBillBiz_001 + request.GroupId;
                    return res;
                }

                res.DataResponse = bankTransaction;
                //**************************************************************************************
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return res;
        }

        private DTO.ResponseService<BankReTransactionData> ReMapReceiveDataBank(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.ImportBankTransferRequest request)
        {
            var res = new DTO.ResponseService<BankReTransactionData>();
            BankReTransactionData bankTransaction = BankTransactionDataFactory.ConCreateReTrans();
            try
            {
                // ********************* Update Change Request **************************//
                //bankTransaction.Details = ctx.AG_IAS_TEMP_PAYMENT_DETAIL.Where(w => w.HEADER_ID == request.GroupId);


                //IList<DTO.ImportBankTransferData> dataMissRefNos = new List<DTO.ImportBankTransferData>();
                IList<AG_IAS_PAYMENT_DETAIL_HIS> detailHistory = new List<AG_IAS_PAYMENT_DETAIL_HIS>();
                IList<AG_IAS_PAYMENT_DETAIL> details = new List<AG_IAS_PAYMENT_DETAIL>();
                foreach (DTO.ImportBankTransferData data in request.ImportBankTransfers)
                {
                    //DTO.ImportBankTransferData data = request.ImportBankTransfers.FirstOrDefault(a => a.Id == item.ID);
                    AG_IAS_PAYMENT_DETAIL item = ctx.AG_IAS_PAYMENT_DETAIL.SingleOrDefault(w => w.ID == data.Id);
                    details.Add(item);
                    if (item != null)
                    {

                        AG_IAS_PAYMENT_DETAIL_HIS importDetailTemp = new AG_IAS_PAYMENT_DETAIL_HIS() { HIS_ID = DAL.OracleDB.GetGenAutoId() };
                        item.MappingToEntity<AG_IAS_PAYMENT_DETAIL, AG_IAS_PAYMENT_DETAIL_HIS>(importDetailTemp);
                        importDetailTemp.HIS_ID = DAL.OracleDB.GetGenAutoId();

                        ctx.AG_IAS_PAYMENT_DETAIL_HIS.AddObject(importDetailTemp);

                        item.CUSTOMER_NO_REF1 = data.ChangeRef1;
                        item.STATUS = (int)DTO.ImportPaymentStatus.ChangeData;

                        detailHistory.Add(importDetailTemp);

                    }

                }
                bankTransaction.Details = details;
                bankTransaction.DetailHis = detailHistory;

                // ********************************************************************//


                //************************* บันทึกข้อมูล ส่วน Detail **********************//

                //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
                IEnumerable<AG_IAS_PAYMENT_DETAIL> transferbankDetails = bankTransaction.Details.Where(w => w.STATUS != (int)DTO.ImportPaymentStatus.Invalid
                                                                                                            && w.STATUS != (int)DTO.ImportPaymentStatus.MissingRefNo
                                                                                                            && w.STATUS != (int)DTO.ImportPaymentStatus.Paid);
                //วนทำงานทีละรายการ
                foreach (AG_IAS_PAYMENT_DETAIL bankPaymentDetail in transferbankDetails)
                {

                    //หารายการ Payment ที่ตรงกับ Ref1 ของข้อมูลธนาคาร
                    AG_IAS_PAYMENT_G_T groupPayment = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(f => f.GROUP_REQUEST_NO == bankPaymentDetail.CUSTOMER_NO_REF1.Trim());

                    // ตรวจสอบสถานะ ใบสั่งจ่าย
                    if (groupPayment == null)
                    {
                        res.ErrorMsg = Resources.errorBillBiz_003 + bankPaymentDetail.CUSTOMER_NO_REF1;
                        return res;
                    }
                    else
                    {
                        if (groupPayment.STATUS == PaymentStatus.P.ToString())
                        {
                            res.ErrorMsg = Resources.errorBillBiz_004;
                            return res;
                        }
                    }


                    if (groupPayment.STATUS == PaymentStatus.W.ToString() || String.IsNullOrEmpty(groupPayment.STATUS))
                    {
                        if (groupPayment.GROUP_AMOUNT == PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT))
                        {
                            groupPayment.STATUS = PaymentStatus.M.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT < PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT))
                        {
                            groupPayment.STATUS = PaymentStatus.U.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT > PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT))
                        {
                            groupPayment.STATUS = PaymentStatus.L.ToString();
                        }
                    }
                    else
                    {
                        IEnumerable<AG_IAS_SUBPAYMENT_RECEIPT> receipts = ctx.AG_IAS_SUBPAYMENT_RECEIPT.Where(a => a.GROUP_REQUEST_NO == groupPayment.GROUP_REQUEST_NO);
                        decimal paid = 0;
                        if (receipts != null)
                        {
                            paid = (Decimal)receipts.Sum(a => a.AMOUNT);
                        }
                        decimal amount = (PhaseStringToDecimal.Phase(bankPaymentDetail.AMOUNT) - paid);

                        if (groupPayment.GROUP_AMOUNT == amount)
                        {
                            groupPayment.STATUS = PaymentStatus.M.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT < amount)
                        {
                            groupPayment.STATUS = PaymentStatus.U.ToString();
                        }
                        else if (groupPayment.GROUP_AMOUNT > amount)
                        {
                            groupPayment.STATUS = PaymentStatus.L.ToString();
                        }
                    }


                    if (!String.IsNullOrEmpty(bankPaymentDetail.CHEQUE_NO))
                        groupPayment.CHEQUE_NO = bankPaymentDetail.CHEQUE_NO;

                    groupPayment.PAYMENT_DATE = bankPaymentDetail.PAYMENT_DATE;// effDate;
                    groupPayment.UPDATED_DATE = DateTime.Now;

                }




                //***************************************************************************//


                //**************************** บันทึกข้อมูลส่วน Total **********************************//



                res.DataResponse = bankTransaction;
                //**************************************************************************************
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return res;
        }

        private DTO.ResponseMessage<bool> KeepingReceiveDataBank(ref IAS.DAL.Interfaces.IIASPersonEntities ctx, BankTransactionTempData transacBank, BankType bankType)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {

                // ********************* Update Change Request **************************//

                //******* หา Header ข้อมูล Bank ****************************//

                // ลอกข้อมูลลงจัดเก็บ
                AG_IAS_PAYMENT_HEADER transferHeader = transacBank.Header.ConvertToAG_IAS_PAYMENT_HEADER(bankType);
                ctx.AG_IAS_PAYMENT_HEADER.AddObject(transferHeader);
                //*****************************************************************//

                //************************* บันทึกข้อมูล ส่วน Detail **********************//

                //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID
                //วนทำงานทีละรายการ
                foreach (AG_IAS_TEMP_PAYMENT_DETAIL bankPaymentDetail in transacBank.Details)
                {
                    //***************** คัดลอกลงข้อมูลจริง **********************//
                    AG_IAS_PAYMENT_DETAIL transferDetail = bankPaymentDetail.ConvertToAG_IAS_PAYMENT_DETAIL();// new AG_IAS_PAYMENT_DETAIL();
                    ctx.AG_IAS_PAYMENT_DETAIL.AddObject(transferDetail);
                }
                //***************************************************************************//


                //**************************** บันทึกข้อมูลส่วน Total **********************************//

                // ลอกข้อมูลลงจัดเก็บ
                AG_IAS_PAYMENT_TOTAL transferTotal = transacBank.Total.ConvertToAG_IAS_PAYMENT_TOTAL();
                ctx.AddToAG_IAS_PAYMENT_TOTAL(transferTotal);

                //**************************************************************************************

                //*******************************  copy His ***************************************************//

                foreach (AG_IAS_TEMP_PAYMENT_DETAIL_HIS item in transacBank.DetailHis)
                {
                    AG_IAS_PAYMENT_DETAIL_HIS importDetailTemp = item.ConvertToAG_IAS_PAYMENT_DETAIL_HIS();
                    ctx.AG_IAS_PAYMENT_DETAIL_HIS.AddObject(importDetailTemp);
                }

                //********************************************************************//

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return res;
        }

        private void SavePaymentFile(DAL.Interfaces.IIASPersonEntities ctx, AG_IAS_SUBPAYMENT_D_T subpaymentd,
                                    BankType banktype, DTO.ImportBankTransferRequest request)
        {
            if (subpaymentd.RECEIPT_DATE != null)
            {
                int countFile = 0;
                var CCount = ctx.AG_IAS_PAYMENT_FILE.Count();
                if (CCount.ToInt() == 0)
                    countFile = 1;
                else
                    countFile = CCount.ToInt() + 1;

                var FileData = ctx.AG_IAS_PAYMENT_FILE.FirstOrDefault(x => x.FILE_DATE == subpaymentd.RECEIPT_DATE);
                if (FileData != null)//ยังไม่มีข้อมูลของวันนี้เลย
                {
                    FileData.ID = countFile;
                    FileData.FILE_DATE = subpaymentd.RECEIPT_DATE;
                    if (banktype == BankType.CIT) // 0=no data in that date // 1 = have data // null = no set value(Defult)
                    {
                        FileData.CITYBANK = "1";
                    }
                    else if (banktype == BankType.KTB)
                    {
                        FileData.KTB = "1";
                    }
                    FileData.USER_ID = request.UserOicId;
                    FileData.USER_DATE = DateTime.Now;
                    FileData.COUNT = 1;
                }
                else //มีข้อมูลของวันนี้แล้ว
                {
                    if (banktype == BankType.CIT) // 0=no data in that date // 1 = have data // null = no set value(Defult)
                    {
                        FileData.CITYBANK = "1";
                    }
                    else if (banktype == BankType.KTB)
                    {
                        FileData.KTB = "1";
                    }
                    FileData.UPDATE_ID = request.UserOicId;
                    FileData.UPDATE_DATE = DateTime.Now;
                    FileData.COUNT = FileData.COUNT + 1;
                }

            }
        }

        private static void IncludeApplicantToRoom(IAS.DAL.Interfaces.IIASPersonEntities ctx, Receipts.SubPaymentDetail subPayEnt)
        {
            if (subPayEnt.Get.TESTING_NO != null)
            {
                IEnumerable<AG_IAS_EXAM_ROOM_LICENSE_R> rooms = ctx.AG_IAS_EXAM_ROOM_LICENSE_R.Where(a => a.ACTIVE == "Y" && a.TESTING_NO == subPayEnt.Get.TESTING_NO);

                String codeRoom = "";
                foreach (AG_IAS_EXAM_ROOM_LICENSE_R room in rooms)
                {
                    int amount = ctx.AG_APPLICANT_T.Count(x => x.TESTING_NO == subPayEnt.Get.TESTING_NO
                                                             && x.EXAM_ROOM == room.EXAM_ROOM_CODE);
                    if (amount < ((room.NUMBER_SEAT_ROOM != null) ? Convert.ToInt32(room.NUMBER_SEAT_ROOM) : 0))
                    {
                        codeRoom = room.EXAM_ROOM_CODE;
                        break;
                    }
                }

                if (String.IsNullOrEmpty(codeRoom))
                    throw new ApplicationException("ห้องสอบเต็ม ไม่สามารถลงบันทึกได้");

                var UpdateApp = ctx.AG_APPLICANT_T.FirstOrDefault(x => x.ID_CARD_NO == subPayEnt.Get.ID_CARD_NO
                                        && x.TESTING_NO == subPayEnt.Get.TESTING_NO
                                        && x.EXAM_ROOM == null);
                UpdateApp.EXAM_ROOM = codeRoom;
                subPayEnt.SetApplicant(UpdateApp);
                //ctx.SaveChanges();

            }
        }
        /// <summary>
        /// จัดคนเข้าห้องสอบแบบ ออโต้
        /// </summary>
        /// <param name="Manage_App">รายชื่อคน ส่งมาเป็น list</param>
        /// <param name="PaymentNo">เลขที่ใบสั่งจ่าย</param>
        /// <returns></returns>
        private bool AutoSaveExamAppRoom(ref IAS.DAL.Interfaces.IIASPersonEntities ctx, ref OracleConnection connection,
                                              string PaymentNo) // PaymentNo = Group_Request_No
        {
            bool res = new bool();
            try
            {

                IEnumerable<AG_IAS_SUBPAYMENT_H_T> paymentHTs = ctx.AG_IAS_SUBPAYMENT_H_T.Where(p => p.GROUP_REQUEST_NO == PaymentNo);

                if (paymentHTs != null && paymentHTs.Count() > 0)
                {
                    foreach (AG_IAS_SUBPAYMENT_H_T item in paymentHTs)
                    {
                        IEnumerable<AG_IAS_SUBPAYMENT_D_T> subDTs = ctx.AG_IAS_SUBPAYMENT_D_T.Where(a => a.HEAD_REQUEST_NO == item.HEAD_REQUEST_NO && a.TESTING_NO != null);
                    }
                }

                string sql_TESTNO_SEAT_ROOM = " select distinct lic.testing_no TESTING_NO,lic.number_seat_room SEAT, " +
                                                        " lic.exam_room_code ROOM " +
                                                        " from ag_ias_subpayment_d_t DT " +
                                                        " ,ag_ias_exam_room_license_r LIC " +
                                                        " ,ag_ias_subpayment_h_t HT " +
                                                        " where DT.head_request_no = ht.head_request_no " +
                                                        " and lic.active = 'Y' " +
                                                        " and lic.testing_no = dt.testing_no " +
                                                        " and ht.group_request_no = '" + PaymentNo + "' " +
                                                        " order by lic.exam_room_code ";

                OracleDB ora = new OracleDB();

                DataTable DT = ora.GetDataSet(sql_TESTNO_SEAT_ROOM, ref connection).Tables[0];

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow dr in DT.Rows)
                    {

                        string SQL_appT = " select distinct appt.id_card_no  " +
                                           " from ag_applicant_t appT " +
                                           " left join  ag_ias_subpayment_d_t DT on dt.id_card_no = appt.id_card_no and dt.accounting ='Y' " +
                                           " left join ag_ias_subpayment_h_t HT on ht.group_request_no ='" + PaymentNo + "' and dt.head_request_no=ht.head_request_no " +
                                           " where appT.testing_no='" + dr["TESTING_NO"].ToString() + "'  " +
                                           " and appT.exam_room is null group by appt.id_card_no order by  appt.id_card_no";
                        // หาคนที่สอบในรอบนั้น เรียงตามวันที่สมัครเพื่อจัดที่
                        DataTable DTappT = ora.GetDataSet(SQL_appT).Tables[0];
                        if (DTappT.Rows.Count > 0)
                        {
                            Boolean HaveFreeSEAT = true;
                            foreach (DataRow drAPPT in DTappT.Rows)
                            {
                                if (HaveFreeSEAT)
                                {
                                    int applicantT = ctx.AG_APPLICANT_T.Where(x => x.TESTING_NO == dr["TESTING_NO"].ToString()
                                                                                 && x.EXAM_ROOM == dr["ROOMCODE"].ToString()).Count();

                                    if (dr["NUMSEAT"].ToString().ToInt() > applicantT)//ถ้าที่นั่งสอบมากกว่าคนที่สอบรอบนั้นและอยู่ในห้องนั้น (มีที่นั่งเหลือ)
                                    {
                                        var UpdateApp = ctx.AG_APPLICANT_T.FirstOrDefault(x => x.ID_CARD_NO == drAPPT["ID_CARD_NO"].ToString()
                                                             && x.TESTING_NO == dr["TESTING_NO"].ToString()
                                                             && x.EXAM_ROOM == null);
                                        UpdateApp.EXAM_ROOM = dr["ROOMCODE"].ToString();
                                        //ctx.SaveChanges();
                                    }
                                    else
                                    {
                                        HaveFreeSEAT = false;
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                }


                res = true;

            }
            catch (Exception ex)
            {
                res = false;
                LoggerFactory.CreateLog().Fatal("ApplicantService_AutoSaveExamAppRoom", ex);
                throw new ApplicationException(ex.Message);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> AddToAccount(ref IAS.DAL.Interfaces.IIASPersonEntities ctx,
                                                        ref IAS.DAL.Interfaces.IIASFinanceEntities ctxFin,

                                                        SubPaymentReceipt receipt,
                                                        APPM_PRODUCT product,
                                                        APPM_ACCOUNT_CODE dabitAccount,
                                                        APPM_ACCOUNT_CODE creditAccount,
                                                        String paymentGroup,
                                                        String accountCode
                                                        , String billHeader2
                                                        , String doc_type
                                                        , String cheque_no
                                                        , String OwnerRecieve)
        {

            var res = new DTO.ResponseMessage<bool>();

            //ดึง Section และ  BranchNo
            string section = ConfigurationManager.AppSettings["OIC_SECTION"].ToString(); // string.Empty;
            string branchNo = ConfigurationManager.AppSettings["OIC_BRANCH_NO"].ToString(); // string.Empty;

            try
            {

                //เตรียมข้อมูล User                                                              
                var userInfo = ctxFin.APPS_CONFIG_INPUT
                                     .SingleOrDefault(s => s.USER_ID == receipt.Get.USER_ID &&
                                                           s.MENU_CODE == "73050");

                int docDate = receipt.Get.PAYMENT_DATE.Value.ToNumberDays();

                string docTime = DateTime.Now.ToString("HHmmss");







                string budDate = GenBillCodeFactory.fnBKOFFGetBudgetYear("", "");

                string[] tmp = budDate.Split('/');
                string budYear = tmp[2] + "-" + tmp[1] + "-" + tmp[0];
                DateTime budYearDate = new DateTime(tmp[2].ToInt(), tmp[1].ToInt(), tmp[0].ToInt());

                #region DoHeader


                DAL.APPR_DO_HEADER doHeader = new APPR_DO_HEADER
                {
                    BRANCH_NO = branchNo.ToShort(),
                    SECTION = section,
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = docDate,
                    DESCRIPTION = billHeader2, //    petitionEnt.PETITION_TYPE_NAME, // Tik พบปัญหา Description ไม่ถูกต้อง
                    AMOUNT_BEFORE_DISCOUNT = (Decimal)receipt.Get.AMOUNT, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้    
                    DISCOUNT_CREDIT = 0m,
                    DISCOUNT_CREDIT_PATTERN = " ",
                    DISCOUNT_CASH_PATTERN = " ",
                    DISCOUNT_CASH = 0m,
                    AMOUNT_BEFORE_VAT = (Decimal)receipt.Get.AMOUNT, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    VAT_RATE = 0m,
                    VAT_AMOUNT = 0m,
                    TOTAL_AMOUNT = (Decimal)receipt.Get.AMOUNT, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    LEDGER_TYPE = "AA",
                    EMP_CODE = userInfo.USER_ID,
                    CUSTOMER_CODE = userInfo.DEFAULT_CUSTOMER_CODE,
                    CUSTOMER_CODE_SHIPTO = " ",
                    REFER_DATE = docDate,
                    REFER_NO = "0",  // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    REFER_TYPE = " ", // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    REMARK = " ",  // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    REMARK_APPROVE = " ", // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    SALE_TYPE = " ", // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    //LAST_LINE = userInfo.DEFAULT_BUDGET,
                    STATUS = " ", // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    STATUS_APPROVE = " ", // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    USER_ID = userInfo.USER_ID,
                    TIME = docTime,
                    REF_BUDGET = userInfo.DEFAULT_BUDGET,
                    REF_PROJECT = userInfo.DEFAULT_PROJECT,
                    BUD_ACCOUNT_CODE = userInfo.DEFAULT_BUD_ACCOUNT,
                    REF_ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    //DATE_APPROVED = "0",
                    BUD_YEAR = budYearDate, // ใช้ปีตามเลขที่เอกสาร // Ref:APPM_CALENDAR_DETAIL
                    PAYMENT_TERM = 0,
                    DELIVERY_DATE = docDate,
                    DUEDATE = docDate,
                    TYPE_TAX = "N",
                    PAY_TYPE = "1",
                    INVOICE_TYPE = "N",

                    PAY_DEPOSIT = 0m,
                    TIME_PRINT = 0,
                    DATE_UPDATE = DateTime.Today,
                    SHOW_DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    PREMIUM_AMOUNT = 0m,
                    FUND_CODE = " ",  // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    MEMBER_CODE = " ",   // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    PLAN_CODE = " ",     // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    TYPE_DISPOSE = " ",   // ไม่ว่าจะใสข้อมูลอะไร 20131001 ติ๊ก
                    AR_ACCOUNT = " "
                };

                if (String.IsNullOrWhiteSpace(cheque_no))
                {
                    doHeader.PAY_CASH = receipt.Get.AMOUNT; // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้  subD.AMOUNT;  // กรณีรับเงินสด
                    doHeader.PAY_OTHER = 0m; // กรณีรับเป็น เช็ค
                }
                else
                {
                    doHeader.PAY_CASH = 0m;  // กรณีรับเงินสด
                    doHeader.PAY_OTHER = receipt.Get.AMOUNT; // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้; //subD.AMOUNT; // กรณีรับเป็น เช็ค
                }

                #endregion


                #region DoDetails




                APPR_DO_DETAIL doDetail = new APPR_DO_DETAIL
                {
                    BRANCH_NO = branchNo.ToShort(),
                    SECTION = section,
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = docDate,
                    SEQUENCE = 1,
                    DESCRIPTION_HEADER = billHeader2, // petitionEnt.PETITION_TYPE_NAME,
                    //DESCRIPTION_DETAIL = product.PRO_TNAME, // petitionEnt.PETITION_TYPE_NAME,
                    //PRODUCT_CODE = product.PROD_CODE,
                    WAREHOUSE = section,
                    UNIT_CODE = "บาท",
                    QUANTITY_UNIT = 1m,
                    PRICE = receipt.Get.AMOUNT.Value,
                    AMOUNT_BEFORE_DISCOUNT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    DISCOUNT_PATTERN = "0%",
                    DISCOUNT_AMOUNT = 0,
                    AMOUNT_BEFORE_VAT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    VAT_AMOUNT = 0m,
                    AMOUNT_AFTER_VAT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    LEDGER_TYPE = "AA",
                    CURRENCY_CODE = "BAHT",
                    CUSTOMER_CODE = "C010106", // ไม่ระบุ  // userInfo.DEFAULT_CUSTOMER_CODE,
                    REF_ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    USER_ID = userInfo.USER_ID,
                    DATE_APPROVED = 0,
                    QTY_OUT = 0m,
                    REF_SO_DOCDATE = 0,
                    REF_SO_DOCSEQ = 0,
                    DISCOUNT_BILL_AMOUNT = 0m,
                    VAT_DISCOUNT_BILL = 0m,
                    TYPE_TAX = "N",
                    CHECK_PM = 0,
                    REF_PROJECT = userInfo.DEFAULT_PROJECT,
                    SHOW_DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    PREMIUM_AMOUNT = 0m
                };

                if (product != null)
                {
                    doDetail.DESCRIPTION_DETAIL = product.PRO_TNAME;
                    doDetail.PRODUCT_CODE = product.PROD_CODE;
                }
                else
                {
                    doDetail.DESCRIPTION_DETAIL = creditAccount.DESCRIPTION_;
                }
                #endregion


                #region DoAddress



                // ที่อยู่
                APPR_DO_ADDRESS doAddress = new APPR_DO_ADDRESS
                {
                    BRANCH_NO = branchNo.ToShort(),
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = docDate,
                    LEDGER_TYPE = "AA",
                    ADDRESS1 = "-",
                    ADDRESS2 = "",
                    ADDRESS3 = "",
                    ADDRESS4 = "",
                    ADDRESS_SHIPTO1 = "",
                    ADDRESS_SHIPTO2 = "",
                    ADDRESS_SHIPTO3 = "",
                    ADDRESS_SHIPTO4 = "",
                    CUSTOMER_CODE = "C010106", // ไม่ระบุ ,
                    //CUSTOMER_NAME = "ไม่ระบุ",
                    CUSTOMER_NAME = OwnerRecieve,
                    SHOW_DOC_DATE = receipt.Get.PAYMENT_DATE.Value
                };
                #endregion



                #region InsReceiveH



                APPR_RECEIVE_H receiveH = new APPR_RECEIVE_H
                {
                    BRACH_NO = branchNo.ToShort(),
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    SECTION = section,
                    EMP_CODE = userInfo.USER_ID,
                    CUSTOMER_CODE = "C010106",   // APPM_CUSTOMER 
                    //DESCRIPTION1 = "ไม่ระบุ",
                    DESCRIPTION1 = OwnerRecieve,
                    DESCRIPTION2 = billHeader2,
                    LEDGER_TYPE = "AA",
                    CHEQUE_DATE = 0,
                    CREDIT_BANK_SECRET_ACCOUNT = 0,
                    DEBIT_AR_SECRET_ACCOUNT = 0,
                    CREDIT_WHOLDING_SECRET_ACCOUNT = 0,
                    DEBIT_CHARGE_SECRET_ACCOUNT = 0,
                    INVOICE_AMOUNT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    INVOICE_RECEIVE_AMOUNT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    WITHOLDINGTAX_AMOUNT = 0,
                    CHARGE_AMOUNT = 0,
                    PAYMENT_AMOUNT = (decimal)receipt.Get.AMOUNT, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    REFER_DATE = docDate,
                    USER_ID = userInfo.USER_ID,
                    REF_BUDGET = userInfo.DEFAULT_BUDGET,
                    REF_PROJECT = userInfo.DEFAULT_PROJECT,
                    BUD_ACCOUNT_CODE = userInfo.DEFAULT_BUD_ACCOUNT,
                    ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    BUD_YEAR = budYearDate,
                    AMOUNT_DEPOSIT = 0,

                    RECEIVE_TYPE = "1",
                    INVOICE_TYPE = "N",
                    INVOICE_RECEIVE_BEFORE_VAT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    INVOICE_RECEIVE_VAT = 0,
                    SENDING_FLAG = 0,
                    DONATE_FLAG = 0,
                    PREMIUM_AMOUNT = 0
                };
                if (String.IsNullOrWhiteSpace(cheque_no))
                {
                    receiveH.PAY_CASH = receipt.Get.AMOUNT; // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ // subD.AMOUNT;  // กรณีรับเงินสด
                    receiveH.PAY_OTHER = 0m; // กรณีรับเป็น เช็ค
                }
                else
                {
                    receiveH.PAY_CASH = 0m;  // กรณีรับเงินสด
                    receiveH.PAY_OTHER = receipt.Get.AMOUNT; // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ // subD.AMOUNT; // กรณีรับเป็น เช็ค
                }
                #endregion


                #region RedeiveDetailD



                APPM_RECEIVE_GROUP receiveGroup = ctxFin.APPM_RECEIVE_GROUP.FirstOrDefault(a => a.RECEIVE_GROUP == paymentGroup);
                // บันทึก เฉพาะ กรณี เป็น เช็ค
                APPR_RECEIVE_DETAIL_D detailD = new APPR_RECEIVE_DETAIL_D
                {
                    BRANCH_NO = branchNo.ToShort(),
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = docDate,
                    SEQUENCE = 1,
                    PAYMENT_GROUP = paymentGroup,  //
                    PAYMENT_CODE = "",
                    REFER_NO = "-",
                    REFER_DATE = receipt.Get.PAYMENT_DATE,   // ปรับเป็น วันที่สั่งจ่าย หน้า เช็ค meeting 2
                    AMOUNT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    USER_ID = userInfo.USER_ID,
                    DATE_UPDATE = DateTime.Today,
                    TIME_UPDATE = DateTime.Now.ToString("HHmmss"),
                    DESCRIPTION = (receiveGroup == null) ? "" : receiveGroup.DESCRIPTION, // ปรับปรุง เป้น table: APPM_RECEIVE_GROUP.RECEIVE_GROUP [meeting 2] | dabitAccount.DESCRIPTION_, // petitionEnt.PETITION_TYPE_NAME,
                    STATUS = "",
                    FLAG_RECEIVE = 0,
                    RECEIVE_DATE = DateTime.Today,
                    SLIP_NO = "",
                    SLIP_DATE = DateTime.Today,
                    ACCOUNT_CODE = accountCode,
                    RETURN_FLAG = 0,
                    RETURN_TYPE = "",
                    RETURN_NO = "",
                    //RETURN_DATE = ,
                    SENDING_FLAG = 0,
                    SENDING_TYPE = "",
                    SENDING_NO = "",
                    //SENDING_DATE,
                    REF_SEQUENCE_NO = 0,
                    SHOW_DOC_DATE = DateTime.Today  // เปลี่ยนเป็นวันทึทำกรายการธนาคาร
                };


                #endregion


                #region GLHeader


                APPR_GL_HEADER glHeader = new APPR_GL_HEADER
                {
                    BRANCH_NO = branchNo.ToShort(),
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    // DESCRIPTION1 = "ไม่ระบุ",
                    DESCRIPTION1 = OwnerRecieve,
                    DESCRIPTION2 = billHeader2,
                    JOB_NO = "",
                    LEDGER_TYPE = "AA",
                    STATUS_ = "",
                    USER_ID = userInfo.USER_ID,
                    PERIOD = receipt.Get.PAYMENT_DATE.Value.Month.ToShort(),// 0,  // เก็บตาม เดือน ของ field DocDate
                    SECTION = section,
                    CONTRACT_CODE = "C010106",
                    TOTAL_AMOUNT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    PAYMENT_AMOUNT = 0,
                    STATUS_PAYMENT = "",
                    EMP_CODE = userInfo.USER_ID,
                    REFER_TYPE = "",
                    REFER_NO = "0",
                    REFER_DATE = receipt.Get.PAYMENT_DATE.Value,
                    PAYMENT_TERM = 0,
                    //DUEDATE = subD.RECEIPT_DATE,
                    REMARK = "",
                    REF_BUDGET = userInfo.DEFAULT_BUDGET,
                    REF_PROJECT = userInfo.DEFAULT_PROJECT,
                    REF_ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    REF_BUD_ACCOUNT = userInfo.DEFAULT_BUD_ACCOUNT,
                    BUD_YEAR = budYearDate,
                    APPROVE_STATUS = "Y",
                    DATE_APPROVED = receipt.Get.PAYMENT_DATE
                };


                #endregion


                #region GLDetails


                #region Debit


                APPR_GL_DETAIL debitGlDetail = new APPR_GL_DETAIL
                {
                    BRANCH_NO = branchNo.ToShort(),
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    SEQUENCE = 1,  // กรณี dabit  = 1
                    DEPARTMENT = section,
                    ACCOUNT_CODE = dabitAccount.ACCOUNT_CODE,
                    SUBSIDAIRY = " ",
                    ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    GL_ACCOUNT = dabitAccount.GL_ACCOUNT_, // Tik 20131022 แก้ตาม Database glAccount,
                    SECRET_ACCOUNT = dabitAccount.SECRET_ACCOUNT_, // secretAccount,
                    // DESCRIPTION1 =  "ไม่ระบุ",
                    DESCRIPTION1 = OwnerRecieve,
                    DESCRIPTION2 = dabitAccount.DESCRIPTION_,
                    DEBIT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    CREDIT = 0,
                    LEDGER_TYPE = "AA",
                    CURRENCY_TYPE = "BAHT",
                    UNIT_TYPE = " ",
                    UNIT_AMOUNT = 0,
                    REF_AP = " ",
                    REF_AR = " ",
                    REF_FIXASSET = " ",
                    REF_CHECK_NO = " ",
                    REF_TAX_NO = " ",
                    STATUS_ = "PA",
                    USER_ID = userInfo.USER_ID,
                    DATE_UPDATE = DateTime.Today,
                    DATE_POSTED = docDate,
                    TIME_POSTED = DateTime.Now.ToString("HHmmss"),
                    REF_BUDGET_CODE = userInfo.DEFAULT_BUDGET,
                    REF_PROJECT = userInfo.DEFAULT_PROJECT,
                    BUD_ACCOUNT_CODE = userInfo.DEFAULT_BUD_ACCOUNT,
                    REF_BUD_YEAR = budYearDate,
                    UNIT_START = 0,
                    UNIT_END = 0,
                    PLAN_CODE = " ",
                    FUND_CODE = " ",
                    DEBIT_CREDIT = "D",
                    JOB_NO = " ",
                    CREATE_FROM_CANCEL = 0,
                    FLAG_CURRENCY = 0,
                    R_ACTIVITY = " ",
                    R_PROJECT = " ",
                    R_SECTION = " "
                };

                #endregion


                #region Credit

                //เตรียมข้อมูลเพื่่อทำการ CREDIT


                APPR_GL_DETAIL creditGlDetail = new APPR_GL_DETAIL
                {
                    BRANCH_NO = branchNo.ToShort(),
                    DOC_TYPE = doc_type,
                    DOC_NO = receipt.Get.RECEIPT_NO,
                    DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    SEQUENCE = 2,
                    DEPARTMENT = section,
                    ACCOUNT_CODE = creditAccount.ACCOUNT_CODE,
                    SUBSIDAIRY = " ",
                    ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    GL_ACCOUNT = creditAccount.GL_ACCOUNT_, // creditGLAccount,
                    SECRET_ACCOUNT = creditAccount.SECRET_ACCOUNT_, // creditSecretAccount.ToInt(),
                    // DESCRIPTION1 = "ไม่ระบุ",
                    DESCRIPTION1 = OwnerRecieve,
                    DESCRIPTION2 = creditAccount.DESCRIPTION_,
                    DEBIT = 0,
                    CREDIT = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    LEDGER_TYPE = "AA",
                    CURRENCY_TYPE = "BAHT",
                    UNIT_TYPE = " ",
                    UNIT_AMOUNT = 0,
                    REF_AP = " ",
                    REF_AR = " ",
                    REF_FIXASSET = " ",
                    REF_CHECK_NO = " ",
                    REF_TAX_NO = " ",
                    STATUS_ = "PA",
                    USER_ID = userInfo.USER_ID,
                    DATE_UPDATE = DateTime.Today,
                    DATE_POSTED = docDate,
                    TIME_POSTED = DateTime.Now.ToString("HHmmss"),
                    REF_BUDGET_CODE = userInfo.DEFAULT_BUDGET,
                    REF_PROJECT = userInfo.DEFAULT_PROJECT,
                    BUD_ACCOUNT_CODE = userInfo.DEFAULT_BUD_ACCOUNT,
                    REF_BUD_YEAR = budYearDate,
                    UNIT_START = 0,
                    UNIT_END = 0,
                    PLAN_CODE = " ",
                    FUND_CODE = " ",
                    DEBIT_CREDIT = "C",
                    JOB_NO = " ",
                    CREATE_FROM_CANCEL = 0,
                    FLAG_CURRENCY = 0,
                    R_ACTIVITY = " ",
                    R_PROJECT = " ",
                    R_SECTION = " "
                };




                #endregion


                #endregion

                DateTime moveBALANCE_DATE = new DateTime(receipt.Get.PAYMENT_DATE.Value.Year, receipt.Get.PAYMENT_DATE.Value.Month, 1);

                APPM_BUDGET_CODE budget = ctxFin.APPM_BUDGET_CODE.FirstOrDefault(a => a.BUDGET_CODE == userInfo.DEFAULT_BUDGET);
                APPR_BD_MOVEMENT movement = new APPR_BD_MOVEMENT()
                {
                    BUDGET_CODE = userInfo.DEFAULT_BUDGET,
                    SECTION_CODE = section,
                    PROJECT_CODE = userInfo.DEFAULT_PROJECT,
                    ACTIVITY = userInfo.DEFAULT_ACTIVITY,
                    BUD_ACCOUNT_CODE = userInfo.DEFAULT_BUD_ACCOUNT,
                    ACCOUNT_CODE = " ",
                    PLAN_CODE = " ",
                    FUND_CODE = " ",
                    GROUP_SECTION = " ",
                    GROUP_ACTIVITY = " ",
                    MUAD_ACCOUNT = "9999",
                    BUD_ACCOUNT_TYPE = "900",
                    STATUS = " ",
                    STATUS_APPROVE = "N",
                    STATUS_POST = " ",
                    DESCRIPTION = budget.DESCRIPTION,   // APPM_BUDGET_CODE where userInfo.DEFAULT_BUDGET
                    DESCRIPTION2 = receiveH.DESCRIPTION2,
                    REQUEST_AMOUNT_A = 0,
                    REQUEST_AMOUNT_R = 0,
                    RETURN_AMOUNT_A = 0,
                    RETURN_AMOUNT_R = 0,
                    BUDGET_REFERENCE = " ",
                    TRI_21 = 0,
                    TRO_22 = 0,
                    ADD_25 = 0,
                    RED_26 = 0,
                    PR_A_31 = 0,
                    PR_R_31 = 0,
                    ESN_A_32 = 0,
                    ESN_R_32 = 0,
                    ESP_A_33 = 0,
                    ESP_R_33 = 0,
                    PO_A_41 = 0,
                    PO_R_41 = 0,
                    RO_A_42 = 0,
                    RO_R_42 = 0,
                    AP_A_43 = 0,
                    AP_R_43 = 0,
                    AR_A_44 = 0,
                    AR_R_44 = 0,
                    RCAR_A_48 = 0,
                    RCAR_R_48 = 0,
                    PA_A_51 = 0,
                    PA_R_51 = 0,
                    PAAP_A_52 = 0,
                    PAAP_R_52 = 0,
                    CLR_A_55 = 0,
                    CLR_R_55 = 0,
                    ADJ_A_52 = 0,
                    ADJ_R_52 = 0,
                    RC_A_80 = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    RC_R_80 = 0,
                    BALANCE_DATE = moveBALANCE_DATE,  // วันที่ 1 ของ เดือนปี subD.RECEIPT_DATE.Value
                    BUD_YEAR = doHeader.BUD_YEAR,
                    PERIOD = receipt.Get.PAYMENT_DATE.Value.Month,
                    BRANCH_NO = doHeader.BRANCH_NO,
                    SYSTEM_NO = 80,
                    SYSTEM_CODE = "RC",
                    SYSTEM_REF = "RC",
                    DOC_TYPE = doHeader.DOC_TYPE,
                    DOC_NO = doHeader.DOC_NO,
                    DOC_DATE = receipt.Get.PAYMENT_DATE.Value,// DateTime.Now,
                    REF_DOC_TYPE = " ",
                    REF_DOC_NO = " ",
                    REF_DOC_DATE = receipt.Get.PAYMENT_DATE.Value,
                    SEQUENCE_GL = 0,
                    BD_AMOUNT_A = 0,
                    BD_AMOUNT_R = 0,
                    TRAN_AMOUNT_A = 0,
                    TRAN_AMOUNT_R = 0,
                    RESERVE_AMOUNT_A = 0,
                    RESERVE_AMOUNT_R = 0,
                    USED_AMOUNT_A = 0,
                    USED_AMOUNT_R = 0,
                    PAYMENT_AMOUNT_A = 0,
                    PAYMENT_AMOUNT_R = 0,
                    RECEIVE_AMOUNT_A = receipt.Get.AMOUNT.Value, // subD.AMOUNT.Value, Tik update 2014-5-21 แก้ให้จ่ายเงินย่อยได้ 
                    RECEIVE_AMOUNT_R = 0
                };


                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(doHeader));


                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(doDetail));


                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(doAddress));


                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(receiveH));


                // บันทึกเฉพาะ กรณีเป็น เช็ค
                if (!String.IsNullOrWhiteSpace(cheque_no))
                {

                    receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(detailD));
                }

                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(glHeader));

                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(debitGlDetail));

                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(creditGlDetail));

                receipt.AddSqlCommand(PhaseEntityToSqlCommand.ConcreateInsertCommand(movement));

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                String errorMessage = String.Format("ไม่สามารถสร้างใบเสร็จได้ [{0}].[{1}].[{2}] .",
                        receipt.Get.GROUP_REQUEST_NO,
                        receipt.Get.HEAD_REQUEST_NO != null ? receipt.Get.HEAD_REQUEST_NO : "",
                        receipt.Get.PAYMENT_NO != null ? receipt.Get.PAYMENT_NO : "");
                LoggerFactory.CreateLog().LogError(errorMessage, ex);
                res.ErrorMsg = Resources.errorBillBiz_013;
            }
            return res;
        }

        public DTO.ResponseMessage<bool> SubmitPaymentBankUpload(IAS.DAL.Interfaces.IIASPersonEntities ctx,
             IAS.DAL.Interfaces.IIASFinanceEntities ctxFin, DTO.ImportBankTransferRequest request)
        {


            var res = new DTO.ResponseMessage<bool>();
            try
            {

                DTO.ResponseService<BankTransactionTempData> resMap = MapReceiveDataBank(ctx, request);

                if (resMap.IsError)
                {
                    res.ErrorMsg = resMap.ErrorMsg;
                    return res;
                }
                //เริ่ม Process ลงบัญชีและออกใบเสร็จ

                var userInfo = ctxFin.APPS_CONFIG_INPUT
                                     .SingleOrDefault(s => s.USER_ID == request.UserOicId &&
                                                           s.MENU_CODE == "73050");

                String paymentGroup = ""; // 20131021 payment KTB  ;   // payment Group SCB ยังไม่ใช้ของจริง
                string accountCode = ""; // update 20131021  //  "11010101010000"; // รหัสผังบัญชี เงินสดในมือ ใช้ชั่วคราว เพราไม่มีข้อมูล 11010201010800 ในฐานข้อมูล
                string section = ConfigurationManager.AppSettings["OIC_SECTION"].ToString(); // string.Empty;
                string branchNo = ConfigurationManager.AppSettings["OIC_BRANCH_NO"].ToString(); // string.Empty;
                BankType banktype = BankType.KTB;

                if (resMap.DataResponse.Header.RECORD_TYPE == "1")
                {
                    banktype = BankType.CIT;
                    paymentGroup = ConfigurationManager.AppSettings["CITYBANK_GROUP"].ToString();
                    accountCode = ConfigurationManager.AppSettings["CITYBANK_ACCOUNT"].ToString();
                }
                else if (resMap.DataResponse.Header.RECORD_TYPE == "H")
                {
                    banktype = BankType.KTB;
                    paymentGroup = ConfigurationManager.AppSettings["KTB_GROUP"].ToString();
                    accountCode = ConfigurationManager.AppSettings["KTB_ACCOUNT"].ToString();
                }
                if (String.IsNullOrEmpty(paymentGroup) || String.IsNullOrEmpty(accountCode))
                {
                    res.ErrorMsg = Resources.errorBillBiz_008;
                    return res;
                }
                // Get Config จัดคนเข้าห้องสอบ แบบ อัตโนมัติหรือไม่ 
                AG_IAS_CONFIG config = ctx.AG_IAS_CONFIG.SingleOrDefault(a => a.ID == "09" && a.GROUP_CODE == "AP001");


                //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID

                IEnumerable<AG_IAS_TEMP_PAYMENT_DETAIL> TransactionRequestList = resMap.DataResponse.Details.Where(a => a.HEADER_ID == request.GroupId
                                                                                                && (a.STATUS != (int)DTO.ImportPaymentStatus.Invalid
                                                                                                    && a.STATUS != (int)DTO.ImportPaymentStatus.MissingRefNo));


                PaymentCollection paymentCollection = new PaymentCollection();
                //วนทำงานที่ Payment Group ทีละรายการ
                foreach (AG_IAS_TEMP_PAYMENT_DETAIL ltemp in TransactionRequestList)
                {
                    LoggerFactory.CreateLog().LogInfo(String.Format("บันทึกข้อมูลการชำระเงิน ใบสั่งจ่าย {0} .", ltemp.CUSTOMER_NO_REF1));
                    Decimal customerPiadAmount = ParsePaymentAmountImport.Phase(ltemp.AMOUNT);

                    AG_IAS_PAYMENT_G_T groupPayment = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == ltemp.CUSTOMER_NO_REF1.Trim());
                    PaymentTransaction groupPaymentl = new PaymentTransaction(groupPayment);
                    paymentCollection.Add(groupPaymentl);

                    #region

                    //หาข้อมูลที่ Sub Payment Head
                    String paidStatus = PaymentStatus.P.ToString();
                    IEnumerable<AG_IAS_SUBPAYMENT_H_T> listH = ctx.AG_IAS_SUBPAYMENT_H_T
                                        .Where(w => w.GROUP_REQUEST_NO == groupPaymentl.Get.GROUP_REQUEST_NO &&
                                                    !(w.STATUS != null && w.STATUS == paidStatus)).OrderBy(a => a.SEQ_OF_GROUP);

                    // ตัวแปร สำหรับตัดยอดชำระจริง
                    Decimal calPayment = customerPiadAmount;

                    foreach (AG_IAS_SUBPAYMENT_H_T subPaymentH in listH)
                    {



                        if (calPayment <= 0)
                            break;


                        Receipts.SubPaymentHead subPaymentHead = new Receipts.SubPaymentHead(subPaymentH);
                        groupPaymentl.AddSubHead(subPaymentHead);

                        #region Gen เลขที่ใบเสร็จ

                        //ดึงข้อมูลที่ยังไม่มีเลขที่ใบเสร็จ
                        IEnumerable<AG_IAS_SUBPAYMENT_D_T> subPayment_D_Ts =
                            ctx.AG_IAS_SUBPAYMENT_D_T
                                    .Where(w => w.HEAD_REQUEST_NO == subPaymentHead.Get.HEAD_REQUEST_NO).OrderBy(a => a.PAYMENT_NO);


                        //หาประเภทเอกสาร doc_type
                        var billEnt = ctx.AG_IAS_BILL_CODE
                                                .Where(w => w.PETITION_TYPE_CODE == subPaymentHead.Get.PETITION_TYPE_CODE)
                                                .FirstOrDefault();
                        if (billEnt == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล AG_IAS_BILL_CODE.PETITION_TYPE_CODE = {0}", subPaymentHead.Get.PETITION_TYPE_CODE));
                            return res;
                        }

                        APPS_TABLE_DOCUMENT documentType = ctxFin.APPS_TABLE_DOCUMENT.FirstOrDefault(d => d.DOC_TYPE == billEnt.BILL_CODE);
                        if (documentType == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล APPS_TABLE_DOCUMENT.DOC_TYPE = {0}", billEnt.BILL_CODE));
                            return res;
                        }

                        string doc_type = billEnt.BILL_CODE;

                        IList<String> results = new List<String>();

                        #region Loop SubPayment

                        foreach (AG_IAS_SUBPAYMENT_D_T subPaymentEnt in subPayment_D_Ts)
                        {

                            Receipts.SubPaymentDetail subPayEnt = new SubPaymentDetail(subPaymentEnt);
                            subPaymentHead.AddSubDetail(subPayEnt);
                            if (!(calPayment <= 0))
                            {

                                LoggerFactory.CreateLog().LogInfo(String.Format("สร้างใบเสร็จ จาก [{0}].[1] .", subPayEnt.Get.HEAD_REQUEST_NO, subPayEnt.Get.PAYMENT_NO));

                                // ************************  ลงบัญชีการเงิน คปภ. ***********************************************/
                                #region Accounting
                                if (subPayEnt.Get.ACCOUNTING != PaymentAccountingStatus.Y.ToString() && subPayEnt.Get.ACCOUNTING != PaymentAccountingStatus.Z.ToString()
                                    && subPayEnt.Get.RECORD_STATUS != PaymentRecordStatus.X.ToString())
                                {
                                    string name = "";
                                    if (subPaymentEnt.PETITION_TYPE_CODE == "01")
                                    {
                                        try
                                        {
                                            string sql = "SELECT WN. NAME,AAT.NAMES,AAT.LASTNAME FROM	AG_APPLICANT_T AAT INNER JOIN VW_IAS_TITLE_NAME WN ON AAT.PRE_NAME_CODE = WN.ID WHERE AAT.ID_CARD_NO ='" + subPaymentEnt.ID_CARD_NO + "'";
                                            var person = ctx.ExecuteStoreQuery<OwnerRecive>(sql).ToList().FirstOrDefault();
                                            if (person != null)
                                            {
                                                name = person.NAME + person.NAMES + " " + person.LASTNAME;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggerFactory.CreateLog().LogError("ใส่ชื่อผู้ชำระเงิน " + ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            string sql = "SELECT WN.NAME,AAT.NAMES,AAT.LASTNAME FROM	AG_PERSONAL_T AAT INNER JOIN VW_IAS_TITLE_NAME WN ON AAT.PRE_NAME_CODE = WN.ID WHERE AAT.ID_CARD_NO ='" + subPaymentEnt.ID_CARD_NO + "'";
                                            var person = ctx.ExecuteStoreQuery<OwnerRecive>(sql).ToList().FirstOrDefault();
                                            if (person != null)
                                            {
                                                name = person.NAME + person.NAMES + " " + person.LASTNAME;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggerFactory.CreateLog().LogError("ใส่ชื่อผู้ชำระเงิน " + ex.Message);
                                        }

                                    }

                                    String resultMessage = ConCreateReceipt(ref calPayment, ref ctx, ref  ctxFin, subPayEnt,
                                                doc_type, request.UserOicId, paymentGroup, accountCode, config, section, name);



                                    if (!String.IsNullOrEmpty(resultMessage))
                                    {
                                        res.ErrorMsg = resultMessage;
                                        LoggerFactory.CreateLog().LogError(resultMessage);
                                        return res;

                                    }
                                }
                                #endregion
                                //**********************************************************************************************
                            }


                        }

                        #endregion End Loop SubPayment


                        if (results.Count > 0)
                        {
                            throw new ApplicationException(results.FirstOrDefault().ToString());
                        }

                        if (customerPiadAmount > 0)
                        {
                            IEnumerable<AG_IAS_SUBPAYMENT_D_T> notPaySubPaymentDT = subPayment_D_Ts.Where(a => a.RECORD_STATUS != paidStatus);
                            if (notPaySubPaymentDT != null && notPaySubPaymentDT.Count() <= 0)
                            {
                                subPaymentHead.Get.STATUS = PaymentHeadStatus.P.ToString();
                            }
                            else
                            {
                                subPaymentHead.Get.STATUS = PaymentHeadStatus.S.ToString();
                            }

                            subPaymentHead.Get.UPDATED_DATE = DateTime.Now;
                            subPaymentHead.Get.UPDATED_BY = request.UserOicId;
                        }
                        #endregion
                    }

                    /*********************** บันทึก เงินเกิน ****************************/
                    if (calPayment > 0)
                    {
                        String doc_type = "RN";
                        string doc_date = groupPaymentl.Get.PAYMENT_DATE.Value.ToString("dd/MM/") + groupPaymentl.Get.PAYMENT_DATE.Value.Year.ToString("0000");

                        AG_IAS_SUBPAYMENT_RECEIPT overReceipt = new AG_IAS_SUBPAYMENT_RECEIPT();
                        Receipts.SubPaymentReceipt receipt = new SubPaymentReceipt(overReceipt);
                        receipt.Get.RECEIPT_NO = "[undefined]";
                        receipt.InitBillNumber(doc_date, "", doc_type, "T");

                        receipt.Get.GROUP_REQUEST_NO = groupPaymentl.Get.GROUP_REQUEST_NO;
                        receipt.Get.PAYMENT_DATE = groupPaymentl.Get.PAYMENT_DATE;

                        receipt.Get.AMOUNT = calPayment;
                        receipt.Get.USER_ID = request.UserOicId;
                        receipt.Get.USER_DATE = DateTime.Now;
                        receipt.Get.ACCOUNTING = PaymentAccountingStatus.Y.ToString();
                        receipt.Get.GEN_STATUS = ReceiptAccountingStatus.W.ToString();
                        receipt.Get.DOWNLOAD_TIMES = 0;

                        receipt.Get.GUID = Guid.NewGuid().ToString();

                        receipt.Get.PRINT_TIMES = 0;
                        String accountAdvance = ConfigurationManager.AppSettings["ADVANCED_ACCOUNT"].ToString();
                        APPM_ACCOUNT_CODE dabitAccount = ctxFin.APPM_ACCOUNT_CODE.FirstOrDefault(a => a.ACCOUNT_CODE == accountCode && a.SECTION_CODE == section);
                        APPM_ACCOUNT_CODE creditAccount = ctxFin.APPM_ACCOUNT_CODE.FirstOrDefault(a => a.ACCOUNT_CODE == accountAdvance && a.SECTION_CODE == section);

                        if (dabitAccount == null || creditAccount == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_012;

                            return res;
                        }
                        var billRN = ctx.AG_IAS_BILL_CODE.SingleOrDefault(a => a.BILL_CODE == "RN");
                        if (billRN == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล AG_IAS_BILL_CODE.BILL_CODE = {0}", "RN"));
                            return res;
                        }

                        APPS_TABLE_DOCUMENT documentType = ctxFin.APPS_TABLE_DOCUMENT.FirstOrDefault(d => d.DOC_TYPE == billRN.BILL_CODE);
                        if (documentType == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล APPS_TABLE_DOCUMENT.DOC_TYPE = {0}", billRN.BILL_CODE));
                            return res;
                        }
                        AG_IAS_PETITION_TYPE_R pattiontion = ctx.AG_IAS_PETITION_TYPE_R.FirstOrDefault(a => a.PETITION_TYPE_CODE == billRN.PETITION_TYPE_CODE);
                        if (pattiontion == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล AG_IAS_PETITION_TYPE_R.PETITION_TYPE_CODE = {0}", billRN.PETITION_TYPE_CODE));
                            return res;
                        }
                        receipt.Get.PETITION_TYPE_NAME = pattiontion.PETITION_TYPE_NAME;


                        var resAddAccount = AddToAccount(ref ctx, ref  ctxFin, receipt,
                                                      null, dabitAccount, creditAccount,
                                                      paymentGroup, accountCode, creditAccount.DESCRIPTION_, billRN.BILL_CODE, groupPaymentl.Get.CHEQUE_NO, "ไม่ระบุ");

                        if (!resAddAccount.ResultMessage)
                        {
                            res.ErrorMsg = resAddAccount.ErrorMsg;
                            return res;
                        }

                        ctx.AG_IAS_SUBPAYMENT_RECEIPT.AddObject(receipt.Get);
                        paymentCollection.AddNewReceipt(receipt.Get);
                        groupPaymentl.AddOverReceipt(receipt);
                    }
                    /****************************************************************/
                    if (customerPiadAmount > 0)
                    {
                        if (listH.Where(a => a.STATUS != paidStatus).Count() > 0)
                        {
                            groupPaymentl.Get.STATUS = PaymentStatus.S.ToString();
                        }
                        else
                        {
                            groupPaymentl.Get.STATUS = PaymentStatus.P.ToString();
                        }

                        groupPaymentl.Get.UPDATED_DATE = DateTime.Now;
                        groupPaymentl.Get.UPDATED_BY = request.UserOicId;
                    }

                    #endregion

                }


                using (TransactionScope ts = new TransactionScope())
                {
                    OracleConnection Connection = new OracleConnection(ConfigurationManager
                                     .ConnectionStrings[ConnectionFor.OraDB_Finance.ToString()]
                                     .ToString());
                    Connection.Open();

                    foreach (Receipts.PaymentTransaction payment in paymentCollection.Payments)
                    {
                        foreach (Receipts.SubPaymentHead subPaymentHead in payment.SubPaymentHeads)
                        {
                            foreach (Receipts.SubPaymentDetail subPaymentDetail in subPaymentHead.SubPaymentDetails)
                            {
                                foreach (Receipts.SubPaymentReceipt receipt in subPaymentDetail.SubPaymentReceipts)
                                {
                                    receipt.GenBillNumber();
                                    foreach (String commandSQL in receipt.SQLCommmands)
                                    {
                                        OracleCommand cmd = new OracleCommand(commandSQL.Replace("[undefined]", receipt.Get.RECEIPT_NO), Connection);

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        foreach (Receipts.SubPaymentReceipt receipt in payment.OverPaymentReceipts)
                        {
                            receipt.GenBillNumber();
                            foreach (String commandSQL in receipt.SQLCommmands)
                            {
                                OracleCommand cmd = new OracleCommand(commandSQL.Replace("[undefined]", receipt.Get.RECEIPT_NO), Connection);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    IEnumerable<AG_IAS_SUBPAYMENT_RECEIPT> list = paymentCollection.AllNewReceipts.Where(a => a.RECEIPT_NO == "[undefined]");
                    if (list.Count() > 0)
                    {
                        throw new ApplicationException("พบข้อมูลไม่สามารถสร้างเลขที่ใบเสร็จได้");
                    }
                    ctx.SaveChanges();
                    ctxFin.SaveChanges();
                    ts.Complete();
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }



                res.ResultMessage = true;

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("ไม่สามารถบันทึกการเงินได้", ex);
                res.ErrorMsg = Resources.errorBillBiz_007;
            }
            return res;
        }


        private String ConCreateReceipt

            (ref Decimal calPayment,
                                                       ref IAS.DAL.Interfaces.IIASPersonEntities ctx,
                                                       ref IAS.DAL.Interfaces.IIASFinanceEntities ctxFin,

                                                        Receipts.SubPaymentDetail subPayEnt,

                                                        String doc_type,
                                                        String oicUserId,
                                                        String paymentGroup,
                                                        String accountCode,
                                                        AG_IAS_CONFIG config,
                                                        String section,
                                                        String ownername
            )
        {
            subPayEnt.Get.RECEIPT_DATE = subPayEnt.Parent.Parent.Get.PAYMENT_DATE;
            subPayEnt.Get.PAYMENT_DATE = subPayEnt.Parent.Parent.Get.PAYMENT_DATE;

            //********* ตัดยอดเงินที่ชำระเข้ามา ***********//
            Decimal amount = new Decimal();
            if (subPayEnt.Get.ACCOUNTING == PaymentAccountingStatus.S.ToString())
            {
                var amountPaid = ctx.AG_IAS_SUBPAYMENT_RECEIPT.Where(a => a.HEAD_REQUEST_NO == subPayEnt.Get.HEAD_REQUEST_NO && a.PAYMENT_NO == subPayEnt.Get.PAYMENT_NO).Sum(a => a.AMOUNT);
                var newamountPaid = subPayEnt.Parent.Parent.Parent.AllNewReceipts.Where(a => a.HEAD_REQUEST_NO == subPayEnt.Get.HEAD_REQUEST_NO && a.PAYMENT_NO == subPayEnt.Get.PAYMENT_NO).Sum(a => a.AMOUNT);
                if (amountPaid != null || newamountPaid != null)
                {
                    Decimal amountOld = (amountPaid == null) ? 0 : (Decimal)amountPaid;
                    Decimal amountNew = (newamountPaid == null) ? 0 : (Decimal)newamountPaid;

                    amount = (Decimal)subPayEnt.Get.AMOUNT - (amountOld + amountNew);
                    if (amount <= 0)
                    {
                        LoggerFactory.CreateLog().LogError(String.Format("คำนวณ ยอดเงินต่ำกว่า 0 : {0} ได้", subPayEnt.Get.HEAD_REQUEST_NO));
                        return "พบข้อมูลผิดผลาดไม่สามารถบันทึกรายการได้";
                    }
                }
                else
                {


                    LoggerFactory.CreateLog().LogError(String.Format("ไม่สามารถ รวมยอดเงิน AG_IAS_SUBPAYMENT_RECEIPT->HEAD_REQUEST_NO : {0} ได้", subPayEnt.Get.HEAD_REQUEST_NO));
                    return "พบข้อมูลผิดผลาดไม่สามารถบันทึกรายการได้";
                }


                if (calPayment >= amount)
                {
                    calPayment = calPayment - amount;
                    subPayEnt.Get.ACCOUNTING = PaymentAccountingStatus.Z.ToString();
                    subPayEnt.Get.RECORD_STATUS = PaymentRecordStatus.P.ToString();
                    //paymentHeadStatus = PaymentHeadStatus.P;
                    //paymentStatus = PaymentStatus.P;
                }
                else
                {
                    amount = calPayment;
                    calPayment = 0;
                    subPayEnt.Get.ACCOUNTING = PaymentAccountingStatus.S.ToString();
                    subPayEnt.Get.RECORD_STATUS = PaymentRecordStatus.S.ToString();
                    //paymentHeadStatus = PaymentHeadStatus.S;
                    //paymentStatus = PaymentStatus.S;
                }
            }
            else
            {
                if (subPayEnt.Get.AMOUNT != null)
                {
                    amount = (Decimal)subPayEnt.Get.AMOUNT;
                }
                else
                {

                    LoggerFactory.CreateLog().LogError(String.Format("ไม่พบยอดชำระ AG_IAS_SUBPAYMENT_D_T->HEAD_REQUEST_NO : {0}  PAYMENT_NO : {1} ได้."
                        , subPayEnt.Get.HEAD_REQUEST_NO
                        , subPayEnt.Get.PAYMENT_NO));
                    return "พบข้อมูลผิดผลาดไม่สามารถบันทึกรายการได้";
                }

                if (calPayment >= amount)
                {
                    calPayment = calPayment - amount;
                    subPayEnt.Get.ACCOUNTING = PaymentAccountingStatus.Y.ToString();
                    subPayEnt.Get.RECORD_STATUS = PaymentRecordStatus.P.ToString();
                    //paymentHeadStatus = PaymentHeadStatus.P;
                    //paymentStatus = PaymentStatus.P;
                }
                else
                {
                    amount = calPayment;
                    calPayment = 0;
                    subPayEnt.Get.ACCOUNTING = PaymentAccountingStatus.S.ToString();
                    subPayEnt.Get.RECORD_STATUS = PaymentRecordStatus.S.ToString();
                    //paymentHeadStatus = PaymentHeadStatus.S;
                    //paymentStatus = PaymentStatus.S;
                }
            }
            //****************************************************************************//





            //String petition_type_code = subPayEnt.PETITION_TYPE_CODE;
            var petitionEnt = ctx.AG_PETITION_TYPE_R.Where(w => w.PETITION_TYPE_CODE == subPayEnt.Get.PETITION_TYPE_CODE)
                                                            .FirstOrDefault();

            String prodCode = String.Format("AG0-{0}-{1}", subPayEnt.Get.PETITION_TYPE_CODE, subPayEnt.Get.LICENSE_TYPE_CODE.PadLeft(4, '0'));  // Update 
            APPM_PRODUCT product = ctxFin.APPM_PRODUCT.FirstOrDefault(w => w.PROD_CODE == prodCode);
            APPM_ACCOUNT_CODE dabitAccount = ctxFin.APPM_ACCOUNT_CODE.FirstOrDefault(a => a.ACCOUNT_CODE == accountCode && a.SECTION_CODE == section);
            APPM_ACCOUNT_CODE creditAccount = ctxFin.APPM_ACCOUNT_CODE.FirstOrDefault(a => a.ACCOUNT_CODE == product.CREDIT_GL_ACCOUNT && a.SECTION_CODE == section);

            if (dabitAccount == null)
            {
                //res.ErrorMsg = Resources.errorBillBiz_012;

                //return res;
                return Resources.errorBillBiz_012;
            }



            AG_IAS_SUBPAYMENT_RECEIPT subRecript = new AG_IAS_SUBPAYMENT_RECEIPT();

            Receipts.SubPaymentReceipt recript = new Receipts.SubPaymentReceipt(subRecript);
            subPayEnt.AddSubReceipt(recript);
            string doc_date = subPayEnt.Parent.Parent.Get.PAYMENT_DATE.Value.ToString("dd/MM/") + subPayEnt.Parent.Parent.Get.PAYMENT_DATE.Value.Year.ToString("0000");

            recript.InitBillNumber(doc_date, "", doc_type, "T");

            recript.Get.RECEIPT_NO = "[undefined]";

            recript.Get.GROUP_REQUEST_NO = subPayEnt.Parent.Parent.Get.GROUP_REQUEST_NO;
            recript.Get.PAYMENT_NO = subPayEnt.Get.PAYMENT_NO;
            recript.Get.HEAD_REQUEST_NO = subPayEnt.Get.HEAD_REQUEST_NO;

            recript.Get.PAYMENT_DATE = subPayEnt.Parent.Parent.Get.PAYMENT_DATE;
            //recript.RECEIPT_DATE = groupPaymentl.PAYMENT_DATE;

            recript.Get.AMOUNT = amount;
            recript.Get.USER_ID = oicUserId;
            recript.Get.USER_DATE = DateTime.Now;
            recript.Get.ACCOUNTING = subPayEnt.Get.ACCOUNTING;
            recript.Get.DOWNLOAD_TIMES = 0;
            recript.Get.GEN_STATUS = ReceiptAccountingStatus.W.ToString();
            recript.Get.GUID = Guid.NewGuid().ToString();

            recript.Get.PRINT_TIMES = 0;

            recript.Get.PETITION_TYPE_NAME = petitionEnt.PETITION_TYPE_NAME;

            if (subPayEnt.Get.PETITION_TYPE_CODE == "01")
            {
                String testing_no = subPayEnt.Get.TESTING_NO;
                String exam_place_code = subPayEnt.Get.EXAM_PLACE_CODE;
                Int32? applicant_code = subPayEnt.Get.APPLICANT_CODE;
                var applicate = ctx.AG_APPLICANT_T.Where(a => a.TESTING_NO == testing_no
                    && a.EXAM_PLACE_CODE == exam_place_code
                    && a.APPLICANT_CODE == applicant_code).FirstOrDefault();
                if (applicate == null)
                {
                    return Resources.errorBillBiz_010;
                }
                else
                {
                    using (DataCenter.DataCenterService dataCenter = new DataCenter.DataCenterService())
                    {
                        DTO.DataItem prename = dataCenter.GetTitleNameById(Convert.ToInt32(applicate.PRE_NAME_CODE)).DataResponse;
                        string strprename = prename.Name;
                        recript.Get.FULL_NAME = String.Format("{0} {1} {2}", strprename, applicate.NAMES, applicate.LASTNAME);
                        recript.Get.ID_CARD_NO = applicate.ID_CARD_NO;
                    }
                }
            }
            else
            {
                String upload_group_no = subPayEnt.Get.UPLOAD_GROUP_NO;
                String seq_no = subPayEnt.Get.SEQ_NO;
                var licen = ctx.AG_IAS_LICENSE_D.Where(a => a.UPLOAD_GROUP_NO == upload_group_no && a.SEQ_NO == seq_no).FirstOrDefault();
                if (licen == null)
                {
                    return Resources.errorBillBiz_010;
                }
                else
                {
                    recript.Get.FULL_NAME = String.Format("{0} {1} {2}", licen.TITLE_NAME, licen.NAMES, licen.LASTNAME);
                    recript.Get.ID_CARD_NO = licen.ID_CARD_NO;
                }
            }


            //string billHeader1 = "";
            string billHeader2 = petitionEnt.PETITION_TYPE_NAME;


            var resAddAccount = AddToAccount(ref ctx, ref  ctxFin, recript, product, dabitAccount, creditAccount,
                                        paymentGroup, accountCode, billHeader2, doc_type, recript.Parent.Parent.Parent.Get.CHEQUE_NO, ownername);


            if (!resAddAccount.ResultMessage)
                return resAddAccount.ErrorMsg;



            /************************จัดคนเข้าห้องสอบ*********************/
            if (config != null && config.ITEM_VALUE == "1") // เช็คว่าจัดคนแบบอัตโนมัติหรือไม่
                IncludeApplicantToRoom(ctx, subPayEnt);
            /**************************************************************/

            /*********************  สร้างใบเสร็จ *********************************/


            ctx.AG_IAS_SUBPAYMENT_RECEIPT.AddObject(recript.Get);
            subPayEnt.Parent.Parent.Parent.AddNewReceipt(recript.Get);

            //subPayEnt.RECORD_STATUS = PaymentRecordStatus.C.ToString();
            /*****************************************************************/


            return String.Empty;
        }


        /// <summary>
        /// ดึงข้อมูลเก่ามาทำรายการอีกครั้ง 
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="ctxFin"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> SubmitPaymentBankReTransaction(ref IAS.DAL.Interfaces.IIASPersonEntities ctx,
            ref IAS.DAL.Interfaces.IIASFinanceEntities ctxFin, DTO.ImportBankTransferRequest request)
        {


            var res = new DTO.ResponseMessage<bool>();
            try
            {

                DTO.ResponseService<BankReTransactionData> resMap = ReMapReceiveDataBank(ctx, request);
                if (resMap.IsError)
                {
                    res.ErrorMsg = resMap.ErrorMsg;
                    return res;
                }


                //เริ่ม Process ลงบัญชีและออกใบเสร็จ

                var userInfo = ctxFin.APPS_CONFIG_INPUT
                                     .SingleOrDefault(s => s.USER_ID == request.UserOicId &&
                                                           s.MENU_CODE == "73050");


                string section = ConfigurationManager.AppSettings["OIC_SECTION"].ToString(); // string.Empty;
                string branchNo = ConfigurationManager.AppSettings["OIC_BRANCH_NO"].ToString(); // string.Empty;


                // Get Config จัดคนเข้าห้องสอบ แบบ อัตโนมัติหรือไม่ 
                AG_IAS_CONFIG config = ctx.AG_IAS_CONFIG.SingleOrDefault(a => a.ID == "09" && a.GROUP_CODE == "AP001");


                //ดึงข้อมูลการเงินจากธนาคารใน Temp ตาม BatchID

                IEnumerable<AG_IAS_PAYMENT_DETAIL> TransactionRequestList = resMap.DataResponse.Details.Where(a =>(a.STATUS != (int)DTO.ImportPaymentStatus.Invalid
                                                                                                                  && a.STATUS != (int)DTO.ImportPaymentStatus.MissingRefNo));



                PaymentCollection paymentCollection = new PaymentCollection();
                //วนทำงานที่ Payment Group ทีละรายการ
                foreach (AG_IAS_PAYMENT_DETAIL ltemp in TransactionRequestList)
                {
                    AG_IAS_PAYMENT_HEADER header = ctx.AG_IAS_PAYMENT_HEADER.SingleOrDefault(a => a.ID == ltemp.HEADER_ID);

                    String paymentGroup = ""; // 20131021 payment KTB  ;   // payment Group SCB ยังไม่ใช้ของจริง
                    string accountCode = ""; // update 20131021  //  "11010101010000"; // รหัสผังบัญชี เงินสดในมือ ใช้ชั่วคราว เพราไม่มีข้อมูล 11010201010800 ในฐานข้อมูล
                    BankType banktype = BankType.KTB;

                    if (header.RECORD_TYPE == "1")
                    {
                        banktype = BankType.CIT;
                        paymentGroup = ConfigurationManager.AppSettings["CITYBANK_GROUP"].ToString(); ;
                        accountCode = ConfigurationManager.AppSettings["CITYBANK_ACCOUNT"].ToString(); ;
                    }
                    else if (header.RECORD_TYPE == "H")
                    {
                        banktype = BankType.KTB;
                        paymentGroup = ConfigurationManager.AppSettings["KTB_GROUP"].ToString(); ;
                        accountCode = ConfigurationManager.AppSettings["KTB_ACCOUNT"].ToString(); ;
                    }

                    if (String.IsNullOrEmpty(paymentGroup) || String.IsNullOrEmpty(accountCode))
                    {
                        res.ErrorMsg = Resources.errorBillBiz_008;
                        return res;
                    }

                    LoggerFactory.CreateLog().LogInfo(String.Format("บันทึกข้อมูลการชำระเงิน ใบสั่งจ่าย {0} .", ltemp.CUSTOMER_NO_REF1));
                    Decimal customerPiadAmount = ParsePaymentAmountImport.Phase(ltemp.AMOUNT);
                    PaymentStatus paymentStatus = PaymentStatus.P;

                    AG_IAS_PAYMENT_G_T groupPayment = ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == ltemp.CUSTOMER_NO_REF1.Trim());
                    PaymentTransaction groupPaymentl = new PaymentTransaction(groupPayment);
                    paymentCollection.Add(groupPaymentl);
                    #region

                    //หาข้อมูลที่ Sub Payment Head
                    String paidStatus = PaymentStatus.P.ToString();
                    IEnumerable<AG_IAS_SUBPAYMENT_H_T> listH = ctx.AG_IAS_SUBPAYMENT_H_T
                                        .Where(w => w.GROUP_REQUEST_NO == groupPaymentl.Get.GROUP_REQUEST_NO &&
                                                    !(w.STATUS != null && w.STATUS == paidStatus)).OrderBy(a => a.SEQ_OF_GROUP);

                    // ตัวแปร สำหรับตัดยอดชำระจริง
                    Decimal calPayment = customerPiadAmount;

                    foreach (AG_IAS_SUBPAYMENT_H_T paymentHead in listH)
                    {
                        if (calPayment <= 0)
                            break;

                        Receipts.SubPaymentHead subPaymentHead = new Receipts.SubPaymentHead(paymentHead);
                        groupPaymentl.AddSubHead(subPaymentHead);

                        PaymentHeadStatus paymentHeadStatus = PaymentHeadStatus.P;


                        #region Gen เลขที่ใบเสร็จ

                        //ดึงข้อมูลที่ยังไม่มีเลขที่ใบเสร็จ
                        IQueryable<AG_IAS_SUBPAYMENT_D_T> subPayment_D_Ts =
                            ctx.AG_IAS_SUBPAYMENT_D_T
                                    .Where(w => w.HEAD_REQUEST_NO == paymentHead.HEAD_REQUEST_NO).OrderBy(a => a.PAYMENT_NO);


                        //หาประเภทเอกสาร doc_type
                        var billEnt = ctx.AG_IAS_BILL_CODE
                                                .Where(w => w.PETITION_TYPE_CODE == paymentHead.PETITION_TYPE_CODE)
                                                .FirstOrDefault();
                        if (billEnt == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล AG_IAS_BILL_CODE.PETITION_TYPE_CODE = {0}", subPaymentHead.Get.PETITION_TYPE_CODE));
                            return res;
                        }
                        APPS_TABLE_DOCUMENT documentType = ctxFin.APPS_TABLE_DOCUMENT.FirstOrDefault(d => d.DOC_TYPE == billEnt.BILL_CODE);
                        if (documentType == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล APPS_TABLE_DOCUMENT.DOC_TYPE = {0}", billEnt.BILL_CODE));
                            return res;
                        }
                        string doc_type = billEnt.BILL_CODE;

                        IList<String> results = new List<String>();

                        foreach (AG_IAS_SUBPAYMENT_D_T subPaymentEnt in subPayment_D_Ts)
                        {
                            Receipts.SubPaymentDetail subPayEnt = new SubPaymentDetail(subPaymentEnt);
                            subPaymentHead.AddSubDetail(subPayEnt);
                            if (!(calPayment <= 0))
                            {

                                LoggerFactory.CreateLog().LogInfo(String.Format("สร้างใบเสร็จ จาก [{0}].[1] .", subPayEnt.Get.HEAD_REQUEST_NO, subPayEnt.Get.PAYMENT_NO));



                                // ************************  ลงบัญชีการเงิน คปภ. ***********************************************/
                                #region Accounting
                                if (subPayEnt.Get.ACCOUNTING != PaymentAccountingStatus.Y.ToString() && subPayEnt.Get.ACCOUNTING != PaymentAccountingStatus.Z.ToString()
                                    && subPayEnt.Get.RECORD_STATUS != PaymentRecordStatus.X.ToString())
                                {
                                    string name = "";
                                    if (subPaymentEnt.PETITION_TYPE_CODE == "01")
                                    {
                                        try
                                        {
                                            string sql = "SELECT WN. NAME,AAT.NAMES,AAT.LASTNAME FROM	AG_APPLICANT_T AAT INNER JOIN VW_IAS_TITLE_NAME WN ON AAT.PRE_NAME_CODE = WN.ID WHERE AAT.ID_CARD_NO ='" + subPaymentEnt.ID_CARD_NO + "'";
                                            var person = ctx.ExecuteStoreQuery<OwnerRecive>(sql).ToList().FirstOrDefault();
                                            if (person != null)
                                            {
                                                name = person.NAME + person.NAMES + " " + person.LASTNAME;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggerFactory.CreateLog().LogError("ใส่ชื่อผู้ชำระเงิน " + ex.Message);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            string sql = "SELECT WN.NAME,AAT.NAMES,AAT.LASTNAME FROM	AG_PERSONAL_T AAT INNER JOIN VW_IAS_TITLE_NAME WN ON AAT.PRE_NAME_CODE = WN.ID WHERE AAT.ID_CARD_NO ='" + subPaymentEnt.ID_CARD_NO + "'";
                                            var person = ctx.ExecuteStoreQuery<OwnerRecive>(sql).ToList().FirstOrDefault();
                                            if (person != null)
                                            {
                                                name = person.NAME + person.NAMES + " " + person.LASTNAME;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LoggerFactory.CreateLog().LogError("ใส่ชื่อผู้ชำระเงิน " + ex.Message);
                                        }

                                    }
                                    String resultMessage = ConCreateReceipt(ref calPayment, ref ctx, ref  ctxFin, subPayEnt,
                                                   doc_type, request.UserOicId, paymentGroup, accountCode, config, section, ltemp.CUSTOMER_NAME);



                                    if (!String.IsNullOrEmpty(resultMessage))
                                    {
                                        res.ErrorMsg = resultMessage;
                                        LoggerFactory.CreateLog().LogError(resultMessage);
                                        return res;

                                    }


                                }
                            }
                                #endregion
                            //**********************************************************************************************

                        }

                        if (results.Count > 0)
                        {
                            throw new ApplicationException(results.FirstOrDefault().ToString());
                        }

                        #endregion
                        if (customerPiadAmount > 0)
                        {
                            IEnumerable<AG_IAS_SUBPAYMENT_D_T> notPaySubPaymentDT = subPayment_D_Ts.Where(a => a.RECORD_STATUS != paidStatus);
                            if (notPaySubPaymentDT != null && notPaySubPaymentDT.Count() <= 0)
                            {
                                paymentHead.STATUS = PaymentStatus.S.ToString();
                            }
                            else
                            {
                                paymentHead.STATUS = PaymentStatus.P.ToString();
                            }

                            paymentHead.UPDATED_DATE = DateTime.Now;
                            paymentHead.UPDATED_BY = request.UserOicId;
                        }


                    }

                    /*********************** บันทึก เงินเกิน ****************************/
                    if (calPayment > 0)
                    {
                        String doc_type = "RN";
                        string doc_date = groupPaymentl.Get.PAYMENT_DATE.Value.ToString("dd/MM/") + groupPaymentl.Get.PAYMENT_DATE.Value.Year.ToString("0000");
                        //string billNo = GenBillCodeFactory.GetBillNo(request.UserOicId, doc_date, "", doc_type, "T");

                        AG_IAS_SUBPAYMENT_RECEIPT recript = new AG_IAS_SUBPAYMENT_RECEIPT();
                        Receipts.SubPaymentReceipt receipt = new SubPaymentReceipt(recript);
                        //string billNo = GenBillCodeFactory.GetBillNo(request.UserOicId, doc_date, "", doc_type, "T");
                        receipt.Get.RECEIPT_NO = "[undefined]";
                        receipt.InitBillNumber(doc_date, "", doc_type, "T");

                        recript.GROUP_REQUEST_NO = groupPaymentl.Get.GROUP_REQUEST_NO;
                        //recript.PAYMENT_NO =  subPayEnt.PAYMENT_NO;
                        //recript.HEAD_REQUEST_NO = subPayEnt.HEAD_REQUEST_NO;

                        recript.PAYMENT_DATE = groupPaymentl.Get.PAYMENT_DATE;
                        //recript.RECEIPT_DATE = groupPaymentl.PAYMENT_DATE;

                        recript.AMOUNT = calPayment;
                        recript.USER_ID = request.UserOicId;
                        recript.USER_DATE = DateTime.Now;
                        recript.ACCOUNTING = PaymentAccountingStatus.Y.ToString();
                        recript.GEN_STATUS = ReceiptAccountingStatus.W.ToString();
                        recript.DOWNLOAD_TIMES = 0;

                        recript.GUID = Guid.NewGuid().ToString();

                        recript.PRINT_TIMES = 0;
                        String accountAdvance = ConfigurationManager.AppSettings["ADVANCED_ACCOUNT"].ToString();
                        //APPM_PRODUCT product = ctxFin.APPM_PRODUCT.FirstOrDefault(w => w.CREDIT_GL_ACCOUNT == accountAdvance);
                        APPM_ACCOUNT_CODE dabitAccount = ctxFin.APPM_ACCOUNT_CODE.FirstOrDefault(a => a.ACCOUNT_CODE == accountCode && a.SECTION_CODE == section);
                        APPM_ACCOUNT_CODE creditAccount = ctxFin.APPM_ACCOUNT_CODE.FirstOrDefault(a => a.ACCOUNT_CODE == accountAdvance && a.SECTION_CODE == section);

                        if (dabitAccount == null || creditAccount == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_012;

                            return res;
                        }


                        var billRN = ctx.AG_IAS_BILL_CODE.SingleOrDefault(a => a.BILL_CODE == "RN");
                        if (billRN == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล AG_IAS_BILL_CODE.BILL_CODE = {0}", "RN"));
                            return res;
                        }

                        APPS_TABLE_DOCUMENT documentType = ctxFin.APPS_TABLE_DOCUMENT.FirstOrDefault(d => d.DOC_TYPE == billRN.BILL_CODE);
                        if (documentType == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล APPS_TABLE_DOCUMENT.DOC_TYPE = {0}", billRN.BILL_CODE));
                            return res;
                        }
                        AG_IAS_PETITION_TYPE_R pattiontion = ctx.AG_IAS_PETITION_TYPE_R.FirstOrDefault(a => a.PETITION_TYPE_CODE == billRN.PETITION_TYPE_CODE);
                        if (pattiontion == null)
                        {
                            res.ErrorMsg = Resources.errorBillBiz_009;
                            LoggerFactory.CreateLog().LogError(String.Format("ไม่พบข้อมูล AG_IAS_PETITION_TYPE_R.PETITION_TYPE_CODE = {0}", billRN.PETITION_TYPE_CODE));
                            return res;
                        }
                        receipt.Get.PETITION_TYPE_NAME = pattiontion.PETITION_TYPE_NAME;




                        var resAddAccount = AddToAccount(ref ctx, ref  ctxFin, receipt,
                                                      null, dabitAccount, creditAccount,
                                                      paymentGroup, accountCode, creditAccount.DESCRIPTION_, billRN.BILL_CODE, groupPaymentl.Get.CHEQUE_NO, "ไม่ระบุ");

                        if (!resAddAccount.ResultMessage)
                        {
                            res.ErrorMsg = resAddAccount.ErrorMsg;
                            return res;
                        }

                        ctx.AG_IAS_SUBPAYMENT_RECEIPT.AddObject(receipt.Get);
                        paymentCollection.AddNewReceipt(receipt.Get);
                        groupPaymentl.AddOverReceipt(receipt);

                    }
                    /****************************************************************/
                    if (customerPiadAmount > 0)
                    {
                        if (listH.Where(a => a.STATUS != paidStatus).Count() > 0)
                        {
                            groupPaymentl.Get.STATUS = PaymentStatus.S.ToString();
                        }
                        else
                        {
                            groupPaymentl.Get.STATUS = PaymentStatus.P.ToString();
                        }

                        groupPaymentl.Get.UPDATED_DATE = DateTime.Now;
                        groupPaymentl.Get.UPDATED_BY = request.UserOicId;
                    }


                    #endregion


                }
                using (var ts = new TransactionScope())
                {
                    OracleConnection Connection = new OracleConnection(ConfigurationManager
                                     .ConnectionStrings[ConnectionFor.OraDB_Finance.ToString()]
                                     .ToString());
                    Connection.Open();

                    foreach (Receipts.PaymentTransaction payment in paymentCollection.Payments)
                    {
                        foreach (Receipts.SubPaymentHead subPaymentHead in payment.SubPaymentHeads)
                        {
                            foreach (Receipts.SubPaymentDetail subPaymentDetail in subPaymentHead.SubPaymentDetails)
                            {
                                foreach (Receipts.SubPaymentReceipt receipt in subPaymentDetail.SubPaymentReceipts)
                                {
                                    receipt.GenBillNumber();
                                    foreach (String commandSQL in receipt.SQLCommmands)
                                    {
                                        OracleCommand cmd = new OracleCommand(commandSQL.Replace("[undefined]", receipt.Get.RECEIPT_NO), Connection);

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        foreach (Receipts.SubPaymentReceipt receipt in payment.OverPaymentReceipts)
                        {
                            receipt.GenBillNumber();
                            foreach (String commandSQL in receipt.SQLCommmands)
                            {
                                OracleCommand cmd = new OracleCommand(commandSQL.Replace("[undefined]", receipt.Get.RECEIPT_NO), Connection);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    IEnumerable<AG_IAS_SUBPAYMENT_RECEIPT> list = paymentCollection.AllNewReceipts.Where(a => a.RECEIPT_NO == "[undefined]");
                    if (list.Count() > 0)
                    {
                        throw new ApplicationException("พบข้อมูลไม่สามารถสร้างเลขที่ใบเสร็จได้");
                    }
                    ctx.SaveChanges();
                    ctxFin.SaveChanges();
                    ts.Complete();
                    if (Connection.State == ConnectionState.Open)
                    {
                        Connection.Close();
                    }

                } // Close Transaction



                res.ResultMessage = true;

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("ไม่สามารถบันทึกการเงินได้", ex);
                res.ErrorMsg = Resources.errorBillBiz_007;
            }
            return res;
        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
