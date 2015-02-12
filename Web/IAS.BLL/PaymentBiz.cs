using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IAS.DTO;
using IAS.Utils;

using System.IO;
using System.Threading;
using System.Web;
using IAS.BLL.Properties;
using IAS.Common.Logging;
using System.ServiceModel;

namespace IAS.BLL
{
    public class PaymentBiz : IDisposable
    { 

        private PaymentService.PaymentServiceClient svc;

        public PaymentBiz()
        {
            svc = new PaymentService.PaymentServiceClient();
        }

        //Gen Bill No
        public string GetBillNo(string user_id, string doc_date,
                                string doc_code, string doc_type,
                                string date_mode)
        {
            PaymentService.PaymentServiceClient svc = new PaymentService.PaymentServiceClient();
            //string billNo = svc.GetBillNo(user_id, doc_date, doc_code, doc_type, date_mode);
            return ""; // billNo;
        }


        #region KTB Upload File

        public static int ValidateDataOfKTB(List<DTO.BankTransaction> transList)
        {
            if (transList.Count == 0) return 0;

            //using (var db = new TRSEntities())
            //{
            //db.ObjectTrackingEnabled = false;
            //DataLoadOptions dl = new DataLoadOptions();
            //dl.LoadWith<TRS_REGISTRATION>(r => r.TRS_REGISTRATIONS_DETAILs);
            //dl.LoadWith<TRS_REGISTRATIONS_DETAIL>(rd => rd.TRS_REGISTRATIONS_RECEIPTs);
            //db.LoadOptions = dl;

            //var regList = new List<TRS_REGISTRATION>();
            //int validCount = 0;
            foreach (var trans in transList)
            {
                //var ent = db.TRS_REGISTRATIONs.FirstOrDefault(r => r.REG_PAY_REF_1.Trim() == trans.Ref1);
                //if (ent != null)
                //{
                //ต้องเป็นรายการที่ยังไม่ได้ชำระเงิน
                //if (ent.REG_PAY_STATUS == 0)
                //{
                //    trans.RegisterId = ent.REG_REG_ID;
                //    trans.PayOption = ent.REG_PAY_RECEIPT_OPTION;
                //    regList.Add(ent);
                //    //validCount += 1;
                //}
                //else
                //{
                //    //ระบุเหตุผล
                //    trans.Remark = "สถานะการจ่ายเปลี่ยนเป็นชำระแล้ว";
                //    trans.IsValid = false;
                //}
                //}
                //else
                //{
                //ระบุเหตุผล
                //trans.Remark = "ไม่พบเลข Reference 1 ในระบบ";
                //trans.IsValid = false;
                //}
            }

            //ดึงข้อมูลมาแล้วต้อง > 0
            //if (regList.Count == 0) return 0;

            //Trans Ref2 ต้องเท่ากับ Ref2
            Func<string, string, bool> IsValidRef2 = (transRef2, regRef2) =>
            {
                return transRef2 == regRef2;
            };

            //Trans Amount ต้องมากกว่าหรือเท่ากับ REG_PAY_TOTAL
            Func<double, double, bool> IsValidTotal = (transAmount, regTotal) =>
            {
                return transAmount >= regTotal;
            };

            int iCount = 0;
            foreach (var trans in transList)
            {
                //var reg = regList.FirstOrDefault(r => r.REG_PAY_REF_1 == trans.Ref1);

                ////ถ้าพบข้อมูล Ref1 ของแบงค์ ใน Table TRS_Registrations
                //if (reg != null)
                //{
                //ถ้าข้อมูลธนาคารมี Ref2 หรือไม่
                //if (!string.IsNullOrEmpty(trans.Ref2) & trans.Ref2.Length == 13)
                //{
                //    //ถ้า Ref2 ของ Transaction Bank ไม่เท่ากับ Ref2 ของข้อมูล TRS_Registrations
                //    if (IsValidRef2(trans.Ref2, reg.REG_PAY_REF_2))
                //    {
                //        if (trans.Amount == reg.REG_PAY_TOTAL)
                //        {
                //            //ตรวจสอบยอดเงิน โดยที่ยอดของธนาคารต้อง >= ยอดเงินที่ลงทะเบียนไว้
                //            iCount += IsValidTotal(trans.Amount, (double)reg.REG_PAY_TOTAL) ? 1 : 0;
                //            trans.Remark = "ผ่าน";
                //            trans.IsValid = true;
                //        }
                //        else
                //        {
                //            //ระบุเหตุผล
                //            trans.Remark = "จำนวนเงินไม่ตรง";
                //            trans.IsValid = false;
                //        }
                //    }
                //    else
                //    {
                //        //ระบุเหตุผล
                //        trans.Remark += "Reference 2 ไม่ตรงกัน";
                //        trans.IsValid = false;
                //    }
                //}
                //else //ถ้า Ref2 ไม่เท่ากับ 13 หลัก (เลขบัตรประชาชน) แสดงว่าเป็นการลงทะเบียนแบบกลุ่ม
                //{
                //    //ตรวจสอบว่า TRS_Registrations เป็นการลงทะเบียนแบบกลุ่ม
                //    if ((bool)reg.REG_IS_GROUP)
                //    {
                //        if (trans.Amount == reg.REG_PAY_TOTAL)
                //        {
                //            //ตรวจสอบยอดเงิน โดยที่ยอดของธนาคารต้อง >= ยอดเงินที่ลงทะเบียนไว้
                //            iCount += IsValidTotal(trans.Amount, (double)reg.REG_PAY_TOTAL) ? 1 : 0;
                //            trans.Remark = "ผ่าน";
                //            trans.IsValid = true;
                //        }
                //        else
                //        {
                //            //ระบุเหตุผล
                //            trans.Remark = "จำนวนเงินไม่ตรง";
                //            trans.IsValid = false;
                //        }
                //    }
                //}

                //}
                //else
                //{
                //    //ระบุเหตุผล
                //    trans.Remark = "Reference 1 ไม่มีในฐานข้อมูล";
                //    trans.IsValid = false;
                //}
            }

            return iCount;
            //}
        }

        /// <summary>
        /// เพิ่มและตรวจสอบข้อมูลการเงินเข้า Temp
        /// </summary>
        /// <param name="fileName">ชื่อไฟล์</param>
        /// <param name="rawData">ข้อมูลดิบ</param>
        /// <param name="userId">user id</param>
        /// <returns>ResponseService<UploadResult<SummaryBankTransaction, BankTransaction>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>
            UploadData(string fileName, Stream rawData, string userId, String selbankCode)
        {
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>();
            if (rawData == null)
            {
                res.ErrorMsg = Resources.errorApplicantBiz_001;
                return res;
            }

            //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

            DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };

            try
            {
                using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
                {
                    String type = "";

                    String headline = sr.ReadLine();
                    if (!String.IsNullOrEmpty(headline))
                    {
                        String bankcode = "";
                      
                        if (headline.Substring(0, 1) == "H")
                        {
                            if (headline.Length != 256)
                            {
                                throw new ApplicationException(Resources.errorPaymentBiz_001);
                            }
                            bankcode = headline.Substring(7, 3);
                        }
                        else if (headline.Substring(0, 1) == "1")
                        {
                            bankcode = headline.Substring(33, 3);
                        }
                        else {
                            throw new ApplicationException(Resources.errorPaymentBiz_001);
                        }


                        type = headline.Substring(0, 1);

                        if (bankcode != selbankCode)
                        {
                            throw new ApplicationException(Resources.errorPaymentBiz_002);
                        }

                        data.Body.Add(headline);
                        
                    }
                    else
                    {
                        res.ErrorMsg = Resources.errorPaymentBiz_001;
                        return res;
                    }

                    Boolean IsBreak = false;
                    Boolean IsHaveData = false;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        if (line!=null && line.Length > 0)
                        {
                           
                            String recordType = line.Substring(0,1);

                            if (type == "H") 
                            {
                                if (line.Length != 256)
                                {
                                    throw new ApplicationException(Resources.errorPaymentBiz_001);
                                }
                               if (recordType == " ") {
                                   res.ErrorMsg = Resources.errorPaymentBiz_001;
                                   return res;
                               }
                               else if (recordType == "D")
                               {
                                    data.Body.Add(line);
                                    IsHaveData = true;
                               }
                               else if (recordType == "T")
                               {
                                   if (IsHaveData)
                                   {
                                       data.Body.Add(line);
                                       IsBreak = true;
                                   }
                                   else {
                                       res.ErrorMsg = Resources.errorPaymentBiz_001;
                                       return res;
                                   }
                                   
                               }
                               else {
                                   res.ErrorMsg = Resources.errorPaymentBiz_001;
                                   return res;
                               }
                                
                            }
                            else if (type == "1") 
                            {
                                if (recordType == " ")
                                {
                                    res.ErrorMsg = Resources.errorPaymentBiz_001;
                                    return res;
                                }
                                else if (recordType == "2" 
                                    || recordType == "4" 
                                    || recordType == "5" 
                                    || recordType == "7" 
                                     )
                                {
                                    data.Body.Add(line);
                                } 
                                else if(recordType == "6")
                                {
                                    data.Body.Add(line);
                                    IsHaveData = true;
                                }
                                else if(recordType == "8")
                                {
                                    if (IsHaveData)
                                    {
                                        data.Body.Add(line);
                                        IsHaveData = false;
                                    }
                                    else {
                                        res.ErrorMsg = Resources.errorPaymentBiz_001;
                                        return res;
                                    }
                                }
                                else if (recordType == "9")
                                {
                                    data.Body.Add(line);
                                    IsBreak = true;
                                }
                                else
                                {
                                    res.ErrorMsg = Resources.errorPaymentBiz_001;
                                    return res;
                                }
                            }
                        }

                        if (IsBreak)
                        {
                            break;
                        }
                    }

                    res = svc.InsertAndCheckPaymentUpload(data, fileName, userId);
                }
             

                
            }
            catch (IOException ex)
            {
                res.ErrorMsg = Resources.errorPaymentBiz_001;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorPaymentBiz_001;
            }

            return res;
        }

        #endregion


        /// <summary>
        /// Gen เลขที่ใบสั่งจ่ายย่อย
        /// </summary>
        /// <returns>เลขที่ใบสั่งจ่ายย่อย</returns>
        public DTO.ResponseService<DTO.ReferanceNumber> GenPaymentCode()
        {
            //string thiYear = (DateTime.Now.Year + 543).ToString("0000").Substring(2, 2);
            //return thiYear + DateTime.Now.ToString("MMddHHmmss");
            return svc.CreateReferanceNumber();
            
        }

        //ใบเสร็จหลายๆใบในไฟล์ .pdf เดียวกัน
        public string CreatePdf(string[] fileName)
        {
            return svc.CreatePdf(fileName);
        }

        /// <summary>
        /// ดึงข้อมูลเพื่อเตรียมสร้างใบสั่งจ่ายย่อย
        /// </summary>
        /// <param name="paymentType">รหัสประเภทใบสั่งจ่าย</param>
        /// <param name="userProfile">Session UserProfile</param>
        /// <param name="pageNo">หน้าที่ ถ้าใส่ 0 จะถึงข้อมูลมาหมด</param>
        /// <param name="recordPerPage">จำนวนรายการต่อหน้า</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetSubGroup(string paymentType, DateTime? startDate, DateTime? toDate, DTO.UserProfile userProfile, string CompanyCode, int pageNo, int recordPerPage, string CountTotalRecord)
        {
            DTO.ResponseService<DataSet> res;

            return res = svc.GetSubGroup(paymentType, startDate, toDate, userProfile, CompanyCode, pageNo, recordPerPage, CountTotalRecord);
           
        }

        public DTO.ResponseService<string> SetSubGroupSingleLicense(SubGroupPayment[] subGroups,
                                                   string userId, string compCode, out string groupHeaderNo)
        {
            return svc.SetSubGroupSingleLicense(out groupHeaderNo, subGroups, userId, compCode);

        }
        /// <summary>
        /// กำหนดกลุ่มย่อยก่อนออกใบสั่งจ่าย
        /// </summary>
        /// <param name="subGroupList">Collection ข้อมูลที่จะรวมเป็นกลุ่มย่อย</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> SetSubGroup(OrderInvoice[] subGroups, string userId, string compCode, string typeUser)
        {
            return svc.SetSubGroup(subGroups, userId, compCode,typeUser);
        }

        /// <summary>
        /// ดึงข้อมูลเพื่อจัดเตรียมออกใบสั่งจ่าย
        /// </summary>
        /// <returns>GroupPayment[]</returns>
        public DTO.ResponseService<DataSet> GetGroupPayment(string compCode, DateTime? startDate, DateTime? EndDate, string UserT,string CompanyCode, int pageNo, int recordPerPage, string Count)
        {
            return svc.GetGroupPayment(compCode,startDate,EndDate,UserT,CompanyCode,pageNo,recordPerPage,Count);
        }



        /// <summary>
        /// สร้างใบสั่งจ่าย
        /// </summary>
        /// <param name="reqList">Collection ของเลขที่ใบสั่งจ่ายย่อย</param>
        /// <param name="paymentId">เลขที่ใบสั่งจ่าย</param>
        /// <param name="userId">user id</param>
        /// <param name="compCode">รหัสบริษัท หรือ รหัสสมาคม</param>
        /// <returns></returns>
        //public DTO.ResponseMessage<bool> CreatePayment(string[] reqList, string remark,
        //                                               string paymentId,
        //                                               string userId,
        //                                               string compCode,out string  groupRequestNo)
        //{
        //    return svc.CreatePayment(out groupRequestNo, reqList, remark, paymentId, userId, compCode);
        //}
        //เพิ่มadd ref2
        public DTO.ResponseMessage<bool> NewCreatePayment(OrderInvoice[] Groups, string remark,
                                                   string userId,
                                                   string compCode, string dayExp, out string groupRequestNo)
        {
            IEnumerable<OrderInvoice> orderInvoices =  DistinctDuplicatesHelper.Duplicates<OrderInvoice>(Groups.ToList(), true);
            return svc.NewCreatePayment(out groupRequestNo, orderInvoices.ToArray(), remark, userId, compCode, dayExp);
        }
        /// <summary>
        /// ดึงรายการใบสั่งจ่ายทั้งหมด
        /// </summary>
        /// <param name="compCode">รหัสบริษัท หรือ รหัสสมาคม</param>
        /// <param name="startDate">วันที่จ่ายย่อย(เริ่ม)</param>
        /// <param name="toDate">วันที่จ่ายย่อย(สิ้นสุด)</param>
        /// <param name="paymentCode">รหัสใบสั่งจ่าย</param>
        /// <returns>ResponseSerivce<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetAllGroupPayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode)
        {
            return svc.GetAllGroupPayment(compCode, startDate, toDate, paymentCode);
        }

        /// <summary>
        /// ดึงรายการใบสั่งจ่ายกลุ่มจากการค้นหา
        /// </summary>
        /// <param name="compCode">รหัสบริษัท หรือ รหัสสมาคม</param>
        /// <param name="startDate">วันที่จ่ายย่อย(เริ่ม)</param>
        /// <param name="toDate">วันที่จ่ายย่อย(สิ้นสุด)</param>
        /// <param name="paymentCode">รหัสใบสั่งจ่าย</param>
        /// <returns>ResponseSerivce<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetGroupPaymentByCriteria(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode)
        {
            return svc.GetGroupPaymentByCriteria(compCode, startDate, toDate, paymentCode);
        }

        /// <summary>
        /// ดึงรายการใบสั่งจ่ายรายบุคคล
        /// </summary>
        /// <param name="userID">รหัสบุคคล</param>
        /// <param name="startDate">วันที่จ่ายย่อย(เริ่ม)</param>
        /// <param name="toDate">วันที่จ่ายย่อย(สิ้นสุด)</param>
        /// <param name="paymentCode">รหัสใบสั่งจ่าย</param>
        /// <returns>ResponseSerivce<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetSinglePayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode,DateTime? startExamDate,DateTime? EndExamDate,string licenseType,string testingNo, string para, int pageNo, int recordPerPage, string Totalrecoad)
        {
            DTO.ResponseService<DataSet> res;
           
                res = svc.GetSinglePayment(compCode, startDate, toDate, paymentCode, startExamDate,EndExamDate, licenseType, testingNo, para, pageNo, recordPerPage, Totalrecoad);
           
            return res;
        }
        /// <summary>
        /// หมายเลขใบเสร็จ
        /// </summary>
        /// <param name="userID">รหัสบุคคล</param>
        /// <param name="startDate">วันที่จ่ายย่อย(เริ่ม)</param>
        /// <param name="toDate">วันที่จ่ายย่อย(สิ้นสุด)</param>
        /// <param name="paymentCode">รหัสใบสั่งจ่าย</param>
        /// <returns>ResponseSerivce<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GenPaymentNumberTable(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode, string CountRecord, int pageNo, int recordPerPage)
        {
            return svc.GenPaymentNumberTable(compCode, startDate, toDate, paymentCode, CountRecord, pageNo, recordPerPage);
        }

        //log4net
        public DTO.ResponseService<DataSet>
          GetDataFromSub_D_T(string Headno, string UID, string HeadOrDetail)
        {
            return svc.GetDataFromSub_D_T(Headno, UID, HeadOrDetail);
        }

         public byte[]
            Signature_Img(string imgPathstr)
         {
             return svc.Signature_Img(imgPathstr);
         }

         public byte[] Copy_Img(string imgPathStr)
         {
             byte[] buffer = null;

             FileStream fileStream = File.Open(imgPathStr, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
             //Stream fileStream = new FileStream(imgPathStr, FileAccess.ReadWrite);
             buffer = new Byte[fileStream.Length + 1];
             BinaryReader br = new BinaryReader(fileStream);
             buffer = br.ReadBytes(Convert.ToInt32((fileStream.Length)));
             br.Close();

             return buffer;
         }

         //log4net
         public DTO.ResponseMessage<bool> GenPaymentNumber(string paymentCode, string UID, string receiptNo)
         {
             return svc.GenPaymentNumber(paymentCode, UID,receiptNo);
         }
         public DTO.ResponseMessage<bool> GenReceiptAll(GenReceipt[] GenReceipt )
         {
             return svc.GenReceiptAll(GenReceipt);
         }
         //public string
         //  AddGenReceiveNumbertoDB(string Headno, string UID, string st_date, string ed_date, string ID)
         //{
         //    return svc.AddGenReceiveNumbertoDB(Headno, UID, st_date, ed_date, ID);
         //}

        public string
        AddStatusReceiveCompletetoDB(string H_req_no, string UID, string strPath, string IDcard, string hashingCode, Guid GU_ID, string receiveNo, Int64 Filesize)
        {
            return svc.AddStatusReceiveCompletetoDB(H_req_no, UID, strPath, IDcard, hashingCode, new Guid(), receiveNo, Filesize);
        }


        /// <summary>
        /// ดึงรายการใบสั่งจ่ายย่อย
        /// </summary>
        /// <param name="groupReqNo">รหัสใบสั่งจ่าย</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetSubPaymentByHeaderRequestNo(string groupReqNo, string CountRecord, int pageNo, int recordPerPage)
        {
            return svc.GetSubPaymentByHeaderRequestNo(groupReqNo,CountRecord,pageNo,recordPerPage);
        }

        /// <summary>
        /// ดึงข้อมูลผู้สมัครสอบทั้งหมดที่ยังไม่ชำระเงิน
        /// </summary>
        /// <returns>DataSet</returns>
        public DTO.ResponseService<DataSet> GetApplicantNoPaymentHeadder(DateTime? startDate, DateTime? toDate,string testingDate,string testNo,string ExamPlaceCode,int resultPage,int PageSize,Boolean Count,Boolean IsAuto=false)
        {
            return svc.GetApplicantNoPaymentHeadder(startDate, toDate,testingDate,testNo,ExamPlaceCode, resultPage, PageSize, Count,IsAuto);
        }

        public DTO.ResponseService<DataSet> GetApplicantNoPayment(string testingDate, string testing_no, string examPlace, DateTime? startDate, DateTime? toDate, string GroupNo, int resultPage, int PageSizeDetail, Boolean Count)
        {
            return svc.GetApplicantNoPayment( testingDate,  testing_no,  examPlace, startDate, toDate, GroupNo, resultPage, PageSizeDetail, Count);
        }

        /// <summary>
        /// ดึงข้อมูลการสมัครสอบเฉพาะรายการที่ต้องการ
        /// </summary>
        /// <param name="applicantCode">เลขที่สมัครสอบ</param>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <param name="examPlaceCode">รหัสสถานที่สอบ</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetApplicantNoPayById(string applicantCode, string testingNo, string examPlaceCode)
        {
            return svc.GetApplicantNoPayById(applicantCode, testingNo, examPlaceCode);
        }

        /// <summary>
        /// ยกเลิกการสมัครสอบ
        /// </summary>
        /// <param name="applicants">Collection รายการสมัครสอบที่ต้องการยกเลิก</param>
        /// <returns>ResponseMessage<bool></returns>
        //public DTO.ResponseMessage<bool> CancelApplicants(DTO.CancelApplicant[] applicants)
        //{
        //    return svc.CancelApplicants(applicants);
        //}
        public DTO.ResponseMessage<bool> CancelApplicantsHeader(AppNoPay[] GroupNo,Boolean IsAuto=false)
        {
            return svc.CancelApplicantsHeader(GroupNo,IsAuto);
        }



        /// <summary>
        /// ดึงข้อมูลเพื่อออกรายงานขอใช้บริการใบเสร็จอิเล็กทรอนิกส์
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <param name="petitionTypeCode">รหัสประเภทค่าใช้จ่าย</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
                GetReportNumberPrintBill(string idCard, string petitionTypeCode,
                                         string firstName, string lastName,int resultPage,int PageSize, Boolean CountAGain)
        {
            return svc.GetReportNumberPrintBill(idCard, petitionTypeCode, firstName, lastName, resultPage, PageSize, CountAGain);
        }


        public DTO.ResponseService<DataSet>
            GetDataPayment_BeforeSentToReport(string H_req_no, string IDcard, string PayNo)
        {
            return svc.GetDataPayment_BeforeSentToReport(H_req_no, IDcard, PayNo);
        }
     

        /// <summary>
        /// ดึงข้อมูลรายละเอียดการชำระเงิน
        /// </summary>
        /// <param name="userProfile">SESSION UserProfile</param>
        /// <param name="paymentType">ประเภทการชำระ</param>
        /// <param name="startDate">วันที่เริ่มสั่งจ่าย</param>
        /// <param name="toDate">วันที่สิ้นสุดสั่งจ่าย</param>
        /// <param name="idCard">เลขบัตรประชาชน</param>
        /// <param name="billNo">เลขที่ใบเสร็จ</param>
        /// <param name="pageNo">หน้าที่</param>
        /// <param name="recordPerPage">จำนวนรายการต่อหน้า</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetPaymentByCriteria(DTO.UserProfile userProfile,
                                 string paymentType,
                                 DateTime? startDate, DateTime? toDate,
                                 string idCard, string billNo,
                                 int pageNo, int recordPerPage)
        {
            return svc.GetPaymentByCriteria(userProfile,
                                 paymentType,
                                 startDate, toDate,
                                 idCard, billNo,
                                 pageNo, recordPerPage);
        }

        public DTO.ResponseService<DataSet>
           GetPaymentDetailByGroup(int type,
                                 string Gcode,
                                 string Ccode, DateTime? startDate,
                                 DateTime? toDate, int RowPerPage,
                                 int num_page,Boolean Count,string Comp)
        {
            return svc.GetPaymentDetailByGroup(type,
                                 Gcode,
                                 Ccode, startDate,
                                 toDate, RowPerPage,
                                 num_page,  Count,Comp);
        }


        ///<summary>
        ///ดึงข้อมูลกลุ่มสนามสอบ
        /// </summary>
        public DTO.ResponseService<DataSet>
            GetGroupExam(int type, string code)
        {
            return svc.GetGroupExam(type, code);
        }

        public DTO.ResponseService<DataSet>
            GetExamCode(string code)
        {
            return svc.GetExamCode(code);
        }
        

        /// <summary>
        /// ดึงรายละเอียดการชำระเงิน
        /// </summary>
        /// <param name="applicantCode">เลขที่สอบ</param>
        /// <param name="testingNo">เลขที่การสอบ</param>
        /// <param name="examPlace_code">รหัสสนามที่สอบ</param>
        /// <param name="licenseNo">เลขที่ใบอนุญาต</param>
        /// <param name="renewTime">ครั้งที่ต่อใบอนุญาต</param>
        /// <param name="isApplicant">เป็นการสอบถามสำหรับการสอบใช่หรือไม่</param>
        /// <returns>ResponseService<PaymentDetail></returns>
        public DTO.ResponseService<DTO.PaymentDetail>
            GetPaymentDetail(string applicantCode, string testingNo, string examPlace_code,
                             string licenseNo, string renewTime, bool isApplicant)
        {
            return svc.GetPaymentDetail(applicantCode, testingNo, examPlace_code,
                                        licenseNo, renewTime, isApplicant);
        }


        /// <summary>
        /// ดึงรายละเอียดการชำระเงิน
        /// </summary>
        /// <param name="headerId">เลขที่กลุ่ม</param>
        /// <param name="Id">เลขที่รายการ</param>
        /// <returns>ResponseService<BankTransDetail></returns>
        public DTO.ResponseService<DTO.BankTransDetail> GetTempBankTransDetail(string groupId, string Id)
        {
            return svc.GetTempBankTransDetail(groupId, Id);
        }


        /// <summary>
        /// Submit ข้อมูลการเงิน
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม</param>
        /// <returns>ResponseService<string></returns>
        public DTO.ResponseService<string> SubmitBankTrans(DTO.ImportBankTransferRequest request)
        {
            //var res = new DTO.ResponseService<string>();
            DTO.ResponseService<string>  res = new ResponseService<string>();
            try
            {
                res = svc.SubmitBankTrans(request);
            }
            catch (CommunicationException comEx) {
                LoggerFactory.CreateLog().LogError("TimeOut", comEx);
                res.ErrorMsg = "การเชื่อมต่อสินสุดก่อนทำรายการเรียบร้อย";
            }
            catch (TimeoutException timeEx)
            {
                LoggerFactory.CreateLog().LogError("TimeOut", timeEx);
                res.ErrorMsg = "การเชื่อมต่อสินสุดก่อนทำรายการเรียบร้อย";
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("การทำงานผิดผลาด", ex);
                res.ErrorMsg = "ไม่สามารถทำรายการได้กรุณาติดต่อผู้ดูแลระบบ";
            }


            //res.DataResponse = "ส่งข้อมูลเรียบร้อยแล้ว...";
            return res;
        }
        public DTO.ResponseMessage<bool> Insert12T(DTO.GenLicense[] GenLicense)
        {
            return svc.Insert12T(GenLicense);
        }
        public DTO.ResponseService<DTO.SubPaymentHead> GetSubPaymentHeadByHeadRequestNo(string headReqNo)
        {
            return svc.GetSubPaymentHeadByHeadRequestNo(headReqNo);
        }

        public DTO.ResponseService<DataSet>
         GetCountPaymentDetailByCriteria(DTO.UserProfile userProfile,
                                 string paymentType,
                                 string order,
                                 DateTime? startDate, DateTime? toDate,
                                 string idCard, string billNo,string ViewYear)

        {
            return svc.GetCountPaymentDetailByCriteria(userProfile,
                                    paymentType,
                                    order,
                                    startDate, toDate,
                                    idCard, billNo,ViewYear);
                                 
        }
        public DTO.ResponseService<DataSet>
            GetPaymentDetailByCriteria(DTO.UserProfile userProfile,
                                    string paymentType,
                                    string order,
                                    DateTime? startDate, DateTime? toDate,
                                    string idCard, string billNo,
                                    int pageNo, int recordPerPage, string ViewYear)
        {
            return svc.GetPaymentDetailByCriteria(userProfile,
                                    paymentType,
                                    order,
                                    startDate, toDate,
                                    idCard, billNo,
                                    pageNo, recordPerPage, ViewYear);
        }


        public DTO.ResponseMessage<bool> PlusPrintDownloadCount(DTO.SubPaymentDetail[] subPaymentDetail)
        {
            return svc.PlusPrintDownloadCount(subPaymentDetail);
        }


        public DTO.ResponseMessage<bool> PrintDownloadCount(DTO.SubPaymentDetail[] subPaymentDetail,string id_card,string createby)
        {
            return svc.PrintDownloadCount(subPaymentDetail,id_card,createby);
        }

        public DTO.ResponseMessage<bool> Zip_PrintDownloadCount(string[] rcvPaht, string EventZip,string id_card,string createby)
        {
            return svc.Zip_PrintDownloadCount(rcvPaht,EventZip,id_card,createby);
        }

        public string CreateZip(string PathFile)
        {
            return svc.CreateZip(PathFile);
        }

        public DTO.ResponseService<DataSet>
            GetDetailSubPayment(string groupReqNo, int pageNo, int recordPerPage, string CountRecord)
        {
            return svc.GetDetailSubPayment(groupReqNo, pageNo, recordPerPage, CountRecord);
        }

        public DTO.ResponseService<DataSet> getGroupDetail(string group_reuest)
        {
            return svc.getGroupDetail(group_reuest);
        }
        public DTO.ResponseService<DataSet> getNamePaymentBy(string group_reuest)
        {
            return svc.getNamePaymentBy(group_reuest);
        }
        public DTO.ResponseService<DataSet> getBindbillPaymentExam(string groupRequestNo, string testNo, string appCode, string examPlaceCode)
        {
            return svc.getBindbillPaymentExam(groupRequestNo,testNo, appCode,examPlaceCode);
        }
        public void Dispose()
        {
            if (svc != null) svc.Close();
            GC.SuppressFinalize(this);
        }

  

        public ResponseService<DateTime[]> GetLicenseGroupRequestPaid(RangeDateRequest request)
        {
            return  svc.GetLicenseGroupRequestPaid(request);
        }

        public ResponseService<DataSet> GetPaymentExpireDay()
        {
            return svc.GetPaymentExpireDay();
        }

        public ResponseMessage<bool> UpdatePaymentExpireDay(List<DTO.ConfigPaymentExpireDay> ls, DTO.UserProfile userProfile)
        {
            return svc.UpdatePaymentExpireDay(ls.ToArray(), userProfile);
        }
      

        public DTO.ResponseService<DataSet>
          GetRcvHisDetail(string RcvId, string RcvEvent, string st_num, string en_num)
        {
            return svc.GetRcvHisDetail(RcvId,RcvEvent,st_num, en_num);
        }

        public ResponseMessage<bool> UpdateCountDownload(string UserId, object FileName, string Event)
        {
            return svc.UpdateCountDownload(UserId, FileName, Event);
        }

        public ResponseService<DataSet>  GetPaymentLicenseAppove(string petitonType, string IdCard, string groupNo, DateTime startDate, DateTime endDate,
           string FirstName,string LastName,  string CountPage, int pageNo, int recordPerPage)
        {
            return svc.GetPaymentLicenseAppove(petitonType,IdCard,groupNo, startDate, endDate,FirstName, LastName,  CountPage,pageNo, recordPerPage);
        }
        public ResponseService<DataSet> GetPaymentExamDetail(string GroupRequestNo)
        {
            return svc.GetPaymentExamDetail(GroupRequestNo);
        }

        public ResponseService<DataSet> GetConfigViewFile()
        {
            return svc.GetConfigViewFile();
        }
        public ResponseService<DataSet> GetNonPayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode, DateTime? startExamDate, DateTime? EndExamDate, string licenseType, string testingNo, string para, int pageNo, int recordPerPage, string Totalrecoad)
        {
            return svc.GetNonPayment(compCode,
                                startDate,  toDate,
                             paymentCode, startExamDate, EndExamDate,  licenseType, testingNo,  para,  pageNo, recordPerPage,  Totalrecoad);
        }

        public ResponseService<DataSet> GetConfigViewBillPayment()
        {
            return svc.GetConfigViewBillPayment();
        }

        public ResponseService<DTO.GetPaymentByRangeResponse> GetPaymentByRange(DTO.GetPaymentByRangeRequest request)
        {
            return svc.GetPaymentByRange(request);
        }

        public DTO.ResponseMessage<bool>  CancelGroupRequestNo(string GroupRequestNo)
        {
            return svc.CancelGroupRequestNo(GroupRequestNo);
        }


        public DTO.ResponseMessage<bool> UpdatePrintGroupRequestNo(string[] reqList)
        {
            return svc.UpdatePrintGroupRequestNo(reqList);
        }

        public ResponseMessage<bool> CheckHolidayDate(string strDate)
        {
           return svc.CheckHolidayDate(strDate);
        }

        public ResponseService<DataSet> GetSubPaymentByGenPaymentForm(string hearReqNo, string CountRecord, int pageNo, int recordPerPage)
        {
            return svc.GetSubPaymentByGenPaymentForm(hearReqNo, CountRecord,  pageNo, recordPerPage);
        }

        public ResponseService<DataSet> GetSubGroupDetail(string paymentType, string UploadGroupNo, int pageNo, int recordPerPage, string CountTotalRecord)
        {
            return svc.GetSubGroupDetail(paymentType,UploadGroupNo,pageNo,recordPerPage,CountTotalRecord);
        }



        #region AutoCancleApplicant

        //public ResponseMessage<bool> Auto_CancleApplicant()
        //{
        //    return svc.Auto_CancleApplicant();
        //}

        #endregion AutoCancleApplicant

        public DTO.ResponseMessage<bool> SavePaymentNoFile(DateTime Date_Time, int CIT, int KTB, string userID)
        {
            return svc.SavePaymentNoFile(Date_Time, CIT, KTB, userID);
        }

        public  DTO.ResponseService<DataSet>
            GetEndOfPay(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string TypeEnd, string CountRecord, int pageNo, int recordPerPage)
        {
            return svc.GetEndOfPay(compCode, startDate, toDate, TypeEnd, CountRecord, pageNo, recordPerPage);
        }


        public DTO.ResponseMessage<bool> Auto_CancelAppNoPay(DateTime stDate, DateTime enDate)
        {
            return svc.Auto_CancelAppNoPay(stDate, enDate);
        }

        public DTO.ResponseService<DataSet> GetReceiptSomePay(string paymentNo, string HeadrequestNo)
        {
            return svc.GetReceiptSomePay(paymentNo, HeadrequestNo);
        }


        public DTO.ResponseService<PaymentNotCompleteResponse> PaymentNotComplete(PaymentNotCompleteRequest request) {

            return svc.PaymentNotComplete(request);
        }

        /// <summary>
        /// Submit ข้อมูลการเงิน
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม</param>
        /// <returns>ResponseService<string></returns>
        public DTO.ResponseService<string> ReSubmitBankTrans(DTO.ImportBankTransferRequest request)   
        {
            var res = new DTO.ResponseService<string>();

            //res.DataResponse = "ส่งข้อมูลเรียบร้อยแล้ว...";
            return  svc.ReSubmitBankTrans(request);
        }

        public DTO.ResponseService<string> GetLastEndDate()
        {
            return svc.GetLastEndDate();
        }

        public DTO.ResponseService<DataSet> GetCheckFileSize(string PetitionTypeName, string StartDate, string EndDate)
        {
            return svc.GetCheckFileSize(PetitionTypeName, StartDate, EndDate);
        }

        public DTO.ResponseService<DataSet> GetDownloadReceiptHistory(string idCard, string petitionTypeCode, string firstName, string lastName)
        {
            return svc.GetDownloadReceiptHistory(idCard, petitionTypeCode, firstName, lastName);
        }

        public DTO.ResponseService<DataSet> getGroupDetailLicense(string group_reuest)
        {
            return svc.getGroupDetailLicense(group_reuest);
        }

        public DTO.ResponseService<DataSet> GetRecriptByHeadRequestNoAndPaymentNo(string HeadNo, string PaymentNo)
        {
            return svc.GetRecriptByHeadRequestNoAndPaymentNo(HeadNo, PaymentNo);
        }
    }
}
