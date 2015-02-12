using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using IAS.DAL;
using IAS.DTO;
using IAS.Utils;
using System.Transactions;
using Oracle.DataAccess.Client;

using System.Configuration;
using System.IO;

using IAS.DataServices.Class;
using System.Web;
using IAS.Utils.Helpers;
using System.Threading;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.License.LicenseHelpers;
using IAS.DataServices.Payment.Mapper;
using IAS.DataServices.Payment.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Ionic.Zip;
using IAS.DataServices.Payment.Messages;
using System.Globalization;
using System.Data.Objects;
using IAS.DataServices.Properties;
using IAS.Common.Logging;
using System.ServiceModel.Activation;
using IAS.DataServices.Payment.TransactionBanking.Citi;
using IAS.DataServices.Payment.TransactionBanking.KTB;
using IAS.DataServices.Payment.Receipts;


namespace IAS.DataServices.Payment
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PaymentService : AbstractService, IPaymentService
    {

        public PaymentService()
        {

        }
        public PaymentService(IAS.DAL.Interfaces.IIASPersonEntities _ctx)
            : base(_ctx)
        {

        }
        //private IAS.DataServices.License.LicenseService svcLicense;

        //แก้ไขปัญหา log4net
        public DTO.ResponseService<string> GetBillNo(string user_id, string doc_date,
                                                     string doc_code, string doc_type,
                                                     string date_mode)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                BillBiz biz = new BillBiz();
                res.DataResponse = GenBillCodeFactory.GetBillNo(user_id, doc_date, doc_code, doc_type, date_mode);
            }
            catch (Exception ex)
            {
                res.DataResponse = ex.Message;
                LoggerFactory.CreateLog().Fatal("PaymentService_GetBillNo", ex);


            }
            return res;
        }

        #region สร้างใบสั่งจ่ายย่อย

        /// <summary>
        /// ดึงข้อมูลเพื่อจัดทำใบสั่งจ่ายย่อยตามประเภทการจ่าย
        /// </summary>
        /// <param name="paymentType">รหัสประเภทการจ่าย</param>
        /// <param name="startDate">วันที่เริ่มการค้นหา</param>
        /// <param name="toDate">วันที่สิ้นสุดการค้นหา</param>
        /// <param name="userProfile">Class Session ประจำตัวของ User</param>
        /// <param name="pageNo">ข้อมูลหน้าที่</param>
        /// <param name="recordPerPage">จำนวนข้อมูลต่อหน้า</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetSubGroup(string paymentType, DateTime? startDate, DateTime? toDate,
                        DTO.UserProfile userProfile, string CompanyCode, int pageNo, int recordPerPage, string CountTotalRecord)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;
                string crit = string.Empty;
                string critRecNo = string.Empty;
                string WhereExam = string.Empty;
                string TableExam = string.Empty;
                string CasePetition = string.Empty;
                string AddCondition = string.Empty;
                string AddComCode = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                paymentType = (paymentType == "00") ? "%" : paymentType;
                switch (CountTotalRecord)
                {
                    case "Y":
                        #region นับจำนวนทั้งหมด  ประเภทใบสั่งจ่าย
                        switch (paymentType)
                        {
                            case "01"://สมัครสอบ
                                if (userProfile.MemberType == DTO.RegistrationType.General.ToInt())
                                {
                                    crit = "and AP.ID_CARD_NO = '" + userProfile.IdCard + "' ";
                                }
                                //บริษัท หรือ สมาคม
                                else if (userProfile.MemberType == DTO.RegistrationType.Insurance.ToInt())
                                {
                                    crit = "and AP.UPLOAD_BY_SESSION = '" + userProfile.CompCode + "'  ";
                                    WhereExam = "and AP.TESTING_NO = Ex.TESTING_NO AND AP.EXAM_PLACE_CODE = Ex.EXAM_PLACE_CODE AND Ex.SPECIAL = 'Y' ";
                                    TableExam = ",AG_EXAM_LICENSE_R Ex ";
                                }
                                else if (userProfile.MemberType == DTO.RegistrationType.Association.ToInt())
                                {
                                    if (CompanyCode != "")
                                    {
                                        AddComCode = "and AP.INSUR_COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                    crit = "and Ex.EXAM_OWNER = '" + userProfile.CompCode + "' " + AddComCode;
                                    WhereExam = "AND  AP.TESTING_NO = Ex.TESTING_NO AND AP.EXAM_PLACE_CODE = Ex.EXAM_PLACE_CODE AND Ex.SPECIAL IS NULL ";
                                    TableExam = ",AG_EXAM_LICENSE_R Ex ";
                                }
                                else if ((userProfile.MemberType == DTO.RegistrationType.OIC.ToInt()) || (userProfile.MemberType == DTO.RegistrationType.OICAgent.ToInt()))
                                {
                                    if (CompanyCode != "")
                                    {
                                        crit = "and AP.INSUR_COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                }
                                //sql = "         SELECT COUNT(*) rowcount  " +
                                //      "         FROM    AG_APPLICANT_T AP, VW_IAS_TITLE_NAME TT, AG_EXAM_LICENSE_R LR " + TableExam +
                                //      "         WHERE   AP.PRE_NAME_CODE = TT.ID AND " +
                                //      "                 AP.TESTING_NO = LR.TESTING_NO AND " +
                                //      "                 AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND " + WhereExam + " AND " +
                                //      "                 AP.HEAD_REQUEST_NO IS NULL AND " + crit + " AND " +
                                //      "                 LR.TESTING_DATE BETWEEN " +
                                //      "                 to_date('{0}','yyyymmdd') AND " +
                                //      "                 to_date('{1}','yyyymmdd')  ";
                                sql = "SELECT COUNT(*) rowcount  FROM( " +
                                      "select CountPerson,UPLOAD_GROUP_NO,PAYMENT_TYPE_NAME,testing_date,EXAM_PLACE_CODE ,TESTING_NO " +
                                      "from(select AP.UPLOAD_GROUP_NO,count(*) CountPerson ,'ค่าสมัครสอบ' AS  PAYMENT_TYPE_NAME ,lr.testing_date " +
                                      " ,AP.EXAM_PLACE_CODE,AP.TESTING_NO " +
                                      "  from ag_applicant_t AP,AG_EXAM_LICENSE_R LR " + TableExam +
                                      "where AP.HEAD_REQUEST_NO IS NULL " +
                                      "and (AP.RECORD_STATUS is null or AP.record_status != 'X') " +
                                      "and AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE  " + WhereExam +
                                      "and AP.TESTING_NO = LR.TESTING_NO  " + crit +
                                      "and                LR.TESTING_DATE BETWEEN " +
                                      "                 to_date('{0}','yyyymmdd') AND " +
                                      "                 to_date('{1}','yyyymmdd')  " +
                                      "and ap.upload_group_no is not null " +
                                      "group by AP.UPLOAD_GROUP_NO ,'ค่าสมัครสอบ',lr.testing_date  ,AP.EXAM_PLACE_CODE,AP.TESTING_NO,AP.UPLOAD_BY_SESSION " +
                                      " order by testing_date ) ) ";


                                sql = string.Format(sql, Convert.ToDateTime(startDate).ToString_yyyyMMdd(), Convert.ToDateTime(toDate).ToString_yyyyMMdd());

                                break;


                            default:

                                if (paymentType == "14")
                                {
                                    CasePetition = "13";
                                }
                                else
                                {
                                    CasePetition = paymentType;
                                }
                                string groupC = "SP001";
                                if ((userProfile.MemberType == DTO.RegistrationType.Association.ToInt()) || (userProfile.MemberType == DTO.RegistrationType.Insurance.ToInt()))
                                {
                                    var CheckGenpayment = ctx.AG_IAS_CONFIG.SingleOrDefault(s => s.GROUP_CODE == "SP001" && s.ITEM_TYPE == CasePetition);
                                    if (CheckGenpayment.ITEM_VALUE == "1")
                                    {
                                        crit = "and lh.COMP_CODE = '" + userProfile.CompCode + "' ";
                                    }
                                    else
                                    {
                                        crit = "and lh.APPROVE_COMPCODE = '" + userProfile.CompCode + "' and lh.COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                }
                                else if ((userProfile.MemberType == DTO.RegistrationType.OIC.ToInt()) || (userProfile.MemberType == DTO.RegistrationType.OICAgent.ToInt()))
                                {
                                    if (CompanyCode != "")
                                    {
                                        crit = "and lh.COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                }

                                if (paymentType == "16")
                                {
                                    AddCondition = "AND ld.FEES <> 0  ";
                                }


                                //sql = " SELECT COUNT(*) rowcount "
                                //      + " from ag_ias_license_h lh, ag_petition_type_r pt,ag_ias_license_d ld "
                                //      + " where PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE and "
                                //      + " " + crit + " and lh.petition_type_code like'" + paymentType + "' "
                                //      + " and  lh.upload_group_no = ld.upload_group_no and ld.head_request_no is  null "
                                //      + " and lh.approved_doc ='Y' and " + AddCondition

                                //      + " (lh.tran_date between   to_date('{0}','yyyymmdd') AND "
                                //      + " to_date('{1}','yyyymmdd'))   "
                                //      + " order by ld.license_no";
                                sql = " SELECT COUNT(*) rowcount from ( "
                                      + "select distinct(UPLOAD_GROUP_NO),LOTS,PETITION_TYPE_NAME,LICENSE_TYPE_NAME from ( "
                                      + "select lh.UPLOAD_GROUP_NO,lh.LOTS,PT.PETITION_TYPE_NAME,LT.LICENSE_TYPE_NAME "
                                      + " from ag_ias_license_h lh, ag_petition_type_r pt,ag_ias_license_d ld,AG_LICENSE_TYPE_R LT"
                                      + " where PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE  "
                                      + " " + crit + " and lh.petition_type_code like'" + paymentType + "' "
                                      + " and  lh.upload_group_no = ld.upload_group_no and ld.head_request_no is  null and lh.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE "
                                      + " and lh.approved_doc ='Y'  " + AddCondition

                                      + "and (lh.tran_date between   to_date('{0}','yyyymmdd') AND "
                                      + " to_date('{1}','yyyymmdd'))   "
                                      + " order by ld.license_no ))";
                                sql = string.Format(sql, Convert.ToDateTime(startDate).ToString_yyyyMMdd(), Convert.ToDateTime(toDate).ToString_yyyyMMdd());

                                break;
                        }

                        break;
                        #endregion
                    default:
                        #region query ธรรมดา
                        //ใบสั่งจ่ายค่าสมัครสอบ
                        switch (paymentType)
                        {
                            case "01":
                                if (userProfile.MemberType == DTO.RegistrationType.General.ToInt())
                                {
                                    crit = "and AP.ID_CARD_NO = '" + userProfile.IdCard + "' ";
                                }
                                //บริษัท หรือ สมาคม
                                else if (userProfile.MemberType == DTO.RegistrationType.Insurance.ToInt())
                                {
                                    crit = "and AP.UPLOAD_BY_SESSION = '" + userProfile.CompCode + "'  ";
                                    WhereExam = "AND AP.TESTING_NO = Ex.TESTING_NO AND AP.EXAM_PLACE_CODE = Ex.EXAM_PLACE_CODE AND Ex.SPECIAL = 'Y' ";
                                    TableExam = ",AG_EXAM_LICENSE_R Ex ";
                                }
                                else if (userProfile.MemberType == DTO.RegistrationType.Association.ToInt())
                                {
                                    if (CompanyCode != "")
                                    {
                                        AddComCode = "and AP.INSUR_COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                    crit = "and Ex.EXAM_OWNER = '" + userProfile.CompCode + "' " + AddComCode;
                                    WhereExam = "and AP.TESTING_NO = Ex.TESTING_NO AND AP.EXAM_PLACE_CODE = Ex.EXAM_PLACE_CODE AND Ex.SPECIAL IS NULL ";
                                    TableExam = ",AG_EXAM_LICENSE_R Ex ";
                                }
                                else if ((userProfile.MemberType == DTO.RegistrationType.OIC.ToInt())|| (userProfile.MemberType == DTO.RegistrationType.OICAgent.ToInt()))
                                {
                                    if (CompanyCode != "")
                                    {
                                        crit = "and AP.INSUR_COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                }

                                //sql = "SELECT * " +
                                //      "FROM ( " +
                                //      "         SELECT 'ค่าสมัครสอบ' AS  PAYMENT_TYPE_NAME, " +
                                //      "                 AP.ID_CARD_NO, TT.NAME ||' '|| AP.NAMES FIRST_NAME, " +
                                //      "                 AP.LASTNAME, LR.TESTING_DATE, LR.TESTING_NO, AP.APPLICANT_CODE, AP.EXAM_PLACE_CODE, " +
                                //      "                 AP.INSUR_COMP_CODE , LR.LICENSE_TYPE_CODE , " +
                                //      "                 ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO, AP.APPLICANT_CODE ASC) RUN_NO " +
                                //      "         FROM    AG_APPLICANT_T AP, VW_IAS_TITLE_NAME TT, AG_EXAM_LICENSE_R LR " + TableExam +
                                //      "         WHERE   AP.PRE_NAME_CODE = TT.ID AND " +
                                //      "                 AP.TESTING_NO = LR.TESTING_NO AND " +
                                //      "                 AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND " + WhereExam + " AND " +
                                //      "                 AP.RECORD_STATUS is null AND " + // add by milk 070214
                                //      "                 AP.HEAD_REQUEST_NO IS NULL AND " + crit + " AND " +
                                //      "                 LR.TESTING_DATE BETWEEN " + //อย่ามาแก้อันนี้ อันนี้ต้องเป็นวันที่สอบ 
                                //      "                 to_date('{0}','yyyymmdd') AND " +
                                //      "                 to_date('{1}','yyyymmdd')  " +
                                //      ") A " + critRecNo;
                                sql = "SELECT * " +
                                      "FROM ( " +
                                      "select CountPerson,UPLOAD_GROUP_NO,PAYMENT_TYPE_NAME,testing_date,EXAM_PLACE_CODE ,TESTING_NO, ROW_NUMBER() OVER (ORDER BY TESTING_NO ASC) RUN_NO,UPLOAD_BY_SESSION " +
                                      "from ( select AP.UPLOAD_GROUP_NO, count(*) CountPerson ,'ค่าสมัครสอบ' AS  PAYMENT_TYPE_NAME ,lr.testing_date " +
                                      " ,AP.EXAM_PLACE_CODE,AP.TESTING_NO,AP.UPLOAD_BY_SESSION " +
                                      "  from ag_applicant_t AP,AG_EXAM_LICENSE_R LR " + TableExam +
                                      "where AP.HEAD_REQUEST_NO IS NULL " +
                                      "and (AP.RECORD_STATUS is null or AP.RECORD_STATUS != 'X') " +
                                      "and AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE  " + WhereExam +
                                      "and AP.TESTING_NO = LR.TESTING_NO  " + crit +
                                      "and                LR.TESTING_DATE BETWEEN " +
                                      "                 to_date('{0}','yyyymmdd') AND " +
                                      "                 to_date('{1}','yyyymmdd')  " +
                                      "and ap.upload_group_no is not null " +
                                      "GROUP BY AP.UPLOAD_GROUP_NO, 'ค่าสมัครสอบ',lr.testing_date,AP.EXAM_PLACE_CODE,AP.TESTING_NO,AP.UPLOAD_BY_SESSION  " +
                                      " order by testing_date  ) " +
                                      ") A " + critRecNo;
                                sql = string.Format(sql, Convert.ToDateTime(startDate).ToString_yyyyMMdd(), Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                                break;

                            default:
                                if (paymentType == "14")
                                {
                                    CasePetition = "13";
                                }
                                else
                                {
                                    CasePetition = paymentType;
                                }
                                if ((userProfile.MemberType == DTO.RegistrationType.Association.ToInt()) || (userProfile.MemberType == DTO.RegistrationType.Insurance.ToInt()))
                                {
                                    var CheckGenpayment = ctx.AG_IAS_CONFIG.SingleOrDefault(s => s.GROUP_CODE == "SP001" && s.ITEM_TYPE == CasePetition);
                                    if (CheckGenpayment.ITEM_VALUE == "1")
                                    {
                                        crit = "and lh.COMP_CODE = '" + userProfile.CompCode + "' ";
                                    }
                                    else
                                    {
                                        crit = "and lh.APPROVE_COMPCODE = '" + userProfile.CompCode + "' and lh.COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                }
                                else if ((userProfile.MemberType == DTO.RegistrationType.OIC.ToInt()) || (userProfile.MemberType == DTO.RegistrationType.OICAgent.ToInt()))
                                {
                                    if (CompanyCode != "")
                                    {
                                        crit = "and lh.COMP_CODE Like '" + CompanyCode + "%' ";
                                    }
                                }


                                if (paymentType == "16")
                                {
                                    AddCondition = "AND ld.FEES <> 0  ";
                                }

                                //sql = "select * from ( "
                                //      + " select ld.license_no,case ld.renew_times when '0' then '' else ld.renew_times end as renew_times ,ld.license_expire_date EXPIRE_DATE, ld.license_date as RENEW_DATE, "
                                //      + " ld.title_name || ' ' || ld.names as T_name, ld.lastname T_LAST, ld.upload_group_no,ld.seq_no "
                                //      + " ,ROW_NUMBER() over (order by ld.license_no,ld.seq_no) run_no "
                                //      + " from ag_ias_license_h lh, ag_petition_type_r pt,ag_ias_license_d ld "
                                //      + " where PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE and "
                                //      + " " + crit + " and lh.petition_type_code like'" + paymentType + "' "
                                //      + " and  lh.upload_group_no = ld.upload_group_no and ld.head_request_no is  null "
                                //      + " and lh.approved_doc ='Y' and " + AddCondition
                                //      + " (lh.tran_date between   to_date('{0}','yyyymmdd') AND " //อันนี้ต้องเป็นวันที่ทำรายการ
                                //      + " to_date('{1}','yyyymmdd')  ) "
                                //      + " order by ld.license_no)"
                                //      + " A " + critRecNo;
                                sql = "select * from ( "
                                      + "select UPLOAD_GROUP_NO,LOTS,PETITION_TYPE_NAME,LICENSE_TYPE_NAME,ROW_NUMBER() over (order by UPLOAD_GROUP_NO) run_no,UPLOAD_BY_SESSION from ( "
                                      + " select distinct(lh.UPLOAD_GROUP_NO),lh.LOTS,PT.PETITION_TYPE_NAME,LT.LICENSE_TYPE_NAME,lh.UPLOAD_BY_SESSION "
                                      + " from ag_ias_license_h lh, ag_petition_type_r pt,ag_ias_license_d ld,AG_LICENSE_TYPE_R LT "
                                      + " where PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE  "
                                      + " " + crit + " and lh.petition_type_code like'" + paymentType + "' "
                                      + " and  lh.upload_group_no = ld.upload_group_no and ld.head_request_no is  null and lh.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE "
                                      + " and lh.approved_doc ='Y'  " + AddCondition
                                      + "and (lh.tran_date between   to_date('{0}','yyyymmdd') AND " //อันนี้ต้องเป็นวันที่ทำรายการ
                                      + " to_date('{1}','yyyymmdd')  ) "
                                      + " order by lh.UPLOAD_GROUP_NO)) "
                                      + " A " + critRecNo;
                                sql = string.Format(sql, Convert.ToDateTime(startDate).ToString_yyyyMMdd(), Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                                break;
                        }

                        #endregion
                        break;
                }

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetSubGroup", ex);
            }
            return res;
        }

        /// <summary>
        /// สร้างใบสั่งจ่ายย่อย
        /// </summary>
        /// <param name="subGroups">Collection รายการที่ User เลือกเพื่อจัดกลุ่มย่อยก่อนออกใบสั่งจ่าย</param>
        /// <param name="userId">user id</param>
        /// <param name="compCode">company code บ.ประกัน, รหัสสมาคม</param>
        /// <returns></returns>
        public DTO.ResponseMessage<bool> SetSubGroup(List<DTO.OrderInvoice> subGroups,
                                                     string userId, string compCode, string typeUser)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            string groupHeaderNo = string.Empty;
            res = SetSubGroupSingle(subGroups, userId, compCode, out groupHeaderNo, typeUser);
            return res;
        }

        public DTO.ResponseMessage<bool> SetSubGroupSingle(List<DTO.OrderInvoice> subGroups,
                                                     string userId, string compCode, out string groupHeaderNo, string typeUser)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            groupHeaderNo = string.Empty;

            if (subGroups.Count == 0)
            {
                res.ErrorMsg = Resources.errorPaymentService_001;
                return res;
            }

            try
            {

                string headReqNo = OracleDB.GetGenAutoId();
                string hLICENSE_TYPE_CODE;
                string hPETITION_TYPE_CODE = "";
                DateTime? expDate = null;
                int iCount = 0;
                decimal fee = 0;
                decimal total = 0;
                Int16 CountPerson = 0;
                Int16 CountPersonEachH = 0;
                string PaymentType = string.Empty;
                AG_IAS_SUBPAYMENT_D_T detail = null;
                AG_IAS_SUBPAYMENT_H_T head = null;
                List<DateTime?> lsDateExp = new List<DateTime?>();
                List<DateTime?> LsTestingDate = new List<DateTime?>();
                foreach (DTO.OrderInvoice sg in subGroups)
                {
                    PaymentType = sg.PaymentType;

                    #region สมัครสอบ
                    //ถ้าเป็นประเภทค่าสมัคร
                    if (sg.PaymentType == "01")
                    {
                        var entls = base.ctx.AG_APPLICANT_T
                              .Where(w =>
                                          w.UPLOAD_GROUP_NO == sg.UPLOAD_GROUP_NO).OrderBy(w => w.APPLICANT_CODE);
                        CountPersonEachH = entls.Count().ToShort();
                        foreach (AG_APPLICANT_T item in entls)
                        {

                            var ent = base.ctx.AG_APPLICANT_T
                                              .Where(w => w.APPLICANT_CODE == item.APPLICANT_CODE &&
                                                          w.TESTING_NO == sg.TESTING_NO &&
                                                          w.EXAM_PLACE_CODE == sg.EXAM_PLACE_CODE)
                                              .SingleOrDefault();
                            var entExamLicense = base.ctx.AG_EXAM_LICENSE_R
                                            .Where(w => w.TESTING_NO == sg.TESTING_NO &&
                                                          w.EXAM_PLACE_CODE == sg.EXAM_PLACE_CODE)
                                            .SingleOrDefault();

                            if (ent.APPLY_DATE > entExamLicense.TESTING_DATE)
                            {
                                expDate = Convert.ToDateTime(entExamLicense.TESTING_DATE).AddDays(3);
                                if (entExamLicense != null)
                                {
                                    LsTestingDate.Add(expDate);
                                }
                            }
                            else
                            {
                                if (entExamLicense != null)
                                {
                                    expDate = Convert.ToDateTime(entExamLicense.TESTING_DATE).AddDays(-3);
                                    LsTestingDate.Add(expDate);
                                }
                            }
                            if (fee == 0)
                            {
                                var pt = base.ctx.AG_PETITION_TYPE_R
                                             .Where(w => w.PETITION_TYPE_CODE == sg.PaymentType)
                                             .FirstOrDefault();
                                if (pt != null)
                                {
                                    fee = pt.FEE == null ? 0m : pt.FEE.Value;

                                }
                                else
                                {
                                    throw new ApplicationException("dkdjfkdfjdlfjdkf");
                                }
                            }
                            #region สร้างใบสั่งจ่ายย่อยปกติ by milk


                            var ent2 = base.ctx.AG_EXAM_LICENSE_R
                                        .Where(w => w.TESTING_NO == sg.TESTING_NO &&
                                                    w.EXAM_PLACE_CODE == sg.EXAM_PLACE_CODE)
                                        .SingleOrDefault();
                            var entEach = base.ctx.AG_APPLICANT_T
                            .Where(w =>
                                        w.TESTING_NO == sg.TESTING_NO &&
                                        w.EXAM_PLACE_CODE == sg.EXAM_PLACE_CODE && w.APPLICANT_CODE == item.APPLICANT_CODE).SingleOrDefault();
                            entEach.HEAD_REQUEST_NO = headReqNo;
                            detail = new AG_IAS_SUBPAYMENT_D_T
                            {
                                PAYMENT_NO = (++iCount).ToString("0000"),
                                HEAD_REQUEST_NO = headReqNo,
                                //PAYMENT_DATE = DateTime.Now.Date,
                                ID_CARD_NO = entEach.ID_CARD_NO,
                                AMOUNT = fee,
                                USER_ID = userId,
                                USER_DATE = DateTime.Now.Date,
                                TESTING_NO = entEach.TESTING_NO,
                                COMPANY_CODE = entEach.INSUR_COMP_CODE,
                                PETITION_TYPE_CODE = sg.PaymentType,
                                APPLICANT_CODE = entEach.APPLICANT_CODE,
                                EXAM_PLACE_CODE = entEach.EXAM_PLACE_CODE,
                                LICENSE_TYPE_CODE = ent2.LICENSE_TYPE_CODE,
                                RECORD_STATUS = DTO.SubPayment_D_T_Status.W.ToString(),
                                SEQ_OF_SUBGROUP = sg.RUN_NO,
                                UPLOAD_GROUP_NO = entEach.UPLOAD_GROUP_NO,
                            };
                            total += fee;
                            base.ctx.AG_IAS_SUBPAYMENT_D_T.AddObject(detail);

                        }

                        //สร้าง Header ของ Sub Payment

                            #endregion
                        CountPerson += CountPersonEachH;
                    }


                    #endregion จบสมัครสอบ
                    else //ค่า License
                    {
                        #region license
                        var result = new DTO.ResponseService<DataSet>();
                        var LicenD = base.ctx.AG_IAS_LICENSE_D.Where(w => w.UPLOAD_GROUP_NO == sg.UPLOAD_GROUP_NO).OrderBy(a => a.SEQ_NO);
                        CountPersonEachH = LicenD.Count().ToShort();
                        foreach (AG_IAS_LICENSE_D item in LicenD)
                        {
                            if (fee == 0)
                            {
                                var pt = base.ctx.AG_PETITION_TYPE_R
                                             .Where(w => w.PETITION_TYPE_CODE == sg.PaymentType)
                                             .FirstOrDefault();
                                if (pt != null)
                                {
                                    fee = pt.FEE == null ? 0m : pt.FEE.Value;

                                }
                                else
                                {
                                    throw new ApplicationException("dkdjfkdfjdlfjdkf");
                                }
                            }
                            #region milk
                            string HEAD_REQUEST_NO = headReqNo;
                            string eLICENSE_NO = sg.LICENSE_NO;
                            short eRENEW_TIME = sg.RENEW_TIMES.ToShort();
                            string IDcard;

                            var entD = base.ctx.AG_IAS_LICENSE_D
                                              .Where(delegate(AG_IAS_LICENSE_D w)
                                              {
                                                  return w.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO &&
                                                      w.SEQ_NO == item.SEQ_NO;
                                              })
                                              .FirstOrDefault();

                            entD.HEAD_REQUEST_NO = headReqNo;
                            string SEQno = entD.SEQ_NO;
                            string uploadG = entD.UPLOAD_GROUP_NO;
                            IDcard = entD.ID_CARD_NO;
                            #endregion
                            string Re = Convert.ToString(eRENEW_TIME);

                            //string sql2 = "select H.LICENSE_TYPE_CODE,H.PETITION_TYPE_CODE  "
                            //            + " from AG_IAS_LICENSE_D D , AG_IAS_LICENSE_H H "
                            //            + " where D.LICENSE_NO = '" + sg.LicenseNo + "' and D.UPLOAD_GROUP_NO = H.UPLOAD_GROUP_NO ";
                            var endH = base.ctx.AG_IAS_LICENSE_H
                                            .Where(delegate(AG_IAS_LICENSE_H H)
                                            {
                                                return H.UPLOAD_GROUP_NO == uploadG;
                                            }).FirstOrDefault();

                            hLICENSE_TYPE_CODE = (endH.LICENSE_TYPE_CODE == null) ? "" : endH.LICENSE_TYPE_CODE; ;
                            hPETITION_TYPE_CODE = endH.PETITION_TYPE_CODE;
                            string Hcomp_code = endH.COMP_CODE;



                            if (entD != null)
                            {
                                lsDateExp.Add(entD.PAY_EXPIRE);
                            }
                            detail = new AG_IAS_SUBPAYMENT_D_T
                            {
                                PAYMENT_NO = (++iCount).ToString("0000"),
                                HEAD_REQUEST_NO = headReqNo,
                                // PAYMENT_DATE = DateTime.Now.Date,
                                ID_CARD_NO = IDcard,
                                LICENSE_NO = eLICENSE_NO,
                                AMOUNT = fee,
                                USER_ID = userId,
                                USER_DATE = DateTime.Now.Date,
                                LICENSE_TYPE_CODE = hLICENSE_TYPE_CODE,
                                PETITION_TYPE_CODE = hPETITION_TYPE_CODE,
                                RENEW_TIME = eRENEW_TIME, //ent.RENEW_TIME
                                RECORD_STATUS = DTO.SubPayment_D_T_Status.W.ToString(),
                                UPLOAD_GROUP_NO = uploadG,
                                SEQ_NO = SEQno,
                                COMPANY_CODE = Hcomp_code,
                                SEQ_OF_SUBGROUP = sg.RUN_NO,
                            };
                            total += fee;
                            base.ctx.AG_IAS_SUBPAYMENT_D_T.AddObject(detail);

                        }
                        //สร้าง Header ของ Sub Payment

                        #endregion
                        CountPerson += CountPersonEachH;
                    }



                }

                if (PaymentType == "01")
                {
                    head = new AG_IAS_SUBPAYMENT_H_T
                    {
                        HEAD_REQUEST_NO = headReqNo,
                        PETITION_TYPE_CODE = "01",
                        PERSON_NO = CountPerson,
                        SUBPAYMENT_AMOUNT = total,
                        SUBPAYMENT_DATE = DateTime.Now.Date,
                        CREATED_BY = userId,
                        CREATED_DATE = DateTime.Now.Date,
                        UPDATED_BY = userId,
                        UPDATED_DATE = DateTime.Now.Date,
                        UPLOAD_BY_SESSION = compCode,
                        DATE_EXP = LsTestingDate.Min()
                    };
                    base.ctx.AG_IAS_SUBPAYMENT_H_T.AddObject(head);
                }
                else
                {
                    head = new AG_IAS_SUBPAYMENT_H_T
                    {
                        HEAD_REQUEST_NO = headReqNo,
                        PETITION_TYPE_CODE = hPETITION_TYPE_CODE,
                        PERSON_NO = CountPerson,
                        SUBPAYMENT_AMOUNT = total,
                        SUBPAYMENT_DATE = DateTime.Now.Date,
                        CREATED_BY = userId,
                        CREATED_DATE = DateTime.Now.Date,
                        UPDATED_BY = userId,
                        UPDATED_DATE = DateTime.Now.Date,
                        UPLOAD_BY_SESSION = compCode,
                        DATE_EXP = lsDateExp.Min(),
                    };
                    base.ctx.AG_IAS_SUBPAYMENT_H_T.AddObject(head);
                }
                using (TransactionScope tc = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    tc.Complete();

                    groupHeaderNo = headReqNo;
                }
                res.ResultMessage = true;
            }
            catch (ApplicationException appEx)
            {
                LoggerFactory.CreateLog().LogError("", appEx);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_SetSubGroupSingle", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> SetSubGroupSingleLicense(List<DTO.SubGroupPayment> subGroups,
                                                    string userId, string compCode, out string groupHeaderNo)
        {
            var res = new DTO.ResponseService<string>();
            groupHeaderNo = string.Empty;

            if (subGroups.Count == 0)
            {
                res.ErrorMsg = Resources.errorPaymentService_001;
                return res;
            }

            try
            {
                AG_IAS_SUBPAYMENT_D_T detail = null;
                AG_IAS_SUBPAYMENT_H_T head = null;
                var lsSubPayment = new List<string>();
                string headReqNo = OracleDB.GetGenAutoId();
                string hLICENSE_TYPE_CODE;
                string hPETITION_TYPE_CODE = "";
                int iCount = 0;
                decimal fee = 0;
                decimal total = 0;
                string SEQno = string.Empty;
                string uploadG = string.Empty;
                List<DateTime?> lsDateExp = new List<DateTime?>();
                foreach (DTO.SubGroupPayment sg in subGroups)
                {
                    AG_PETITION_TYPE_R pt = base.ctx.AG_PETITION_TYPE_R.FirstOrDefault(w => w.PETITION_TYPE_CODE == sg.PaymentType);
                    if (pt != null)
                    {
                        fee = pt.FEE == null ? 0m : pt.FEE.Value;

                    }

                    #region license
                    var result = new DTO.ResponseService<DataSet>();

                    #region milk
                    string HEAD_REQUEST_NO = headReqNo;
                    string eLICENSE_NO = sg.LicenseNo;
                    short eRENEW_TIME = sg.RenewTime.ToShort();
                    string IDcard;

                    AG_IAS_LICENSE_D entD = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(ent => ent.UPLOAD_GROUP_NO == sg.uploadG
                        && ent.SEQ_NO == sg.seqNo);

                    entD.HEAD_REQUEST_NO = headReqNo;
                    SEQno = entD.SEQ_NO;
                    uploadG = entD.UPLOAD_GROUP_NO;
                    IDcard = entD.ID_CARD_NO;
                    #endregion
                    string Re = Convert.ToString(eRENEW_TIME);

                    AG_IAS_LICENSE_H endH = base.ctx.AG_IAS_LICENSE_H.FirstOrDefault(upno => upno.UPLOAD_GROUP_NO == uploadG);

                    hLICENSE_TYPE_CODE = (endH.LICENSE_TYPE_CODE == null) ? "" : endH.LICENSE_TYPE_CODE; ;
                    hPETITION_TYPE_CODE = endH.PETITION_TYPE_CODE;
                    string Hcomp_code = endH.COMP_CODE;

                    var TrainT = base.ctx.AG_TRAIN_T.FirstOrDefault(t => t.ID_CARD_NO == IDcard && t.LICENSE_TYPE_CODE == hLICENSE_TYPE_CODE && t.TRAIN_TIMES == eRENEW_TIME);
                    if (entD != null)
                    {
                        lsDateExp.Add(entD.PAY_EXPIRE);
                    }

                    detail = new AG_IAS_SUBPAYMENT_D_T
                    {
                        PAYMENT_NO = (++iCount).ToString("0000"),
                        HEAD_REQUEST_NO = headReqNo,
                        // PAYMENT_DATE = DateTime.Now.Date,
                        ID_CARD_NO = IDcard,
                        LICENSE_NO = eLICENSE_NO,
                        AMOUNT = fee,
                        USER_ID = userId,
                        USER_DATE = DateTime.Now.Date,
                        LICENSE_TYPE_CODE = hLICENSE_TYPE_CODE,
                        PETITION_TYPE_CODE = hPETITION_TYPE_CODE,
                        RENEW_TIME = eRENEW_TIME, //ent.RENEW_TIME
                        RECORD_STATUS = DTO.SubPayment_D_T_Status.W.ToString(),
                        UPLOAD_GROUP_NO = uploadG,
                        SEQ_NO = SEQno,
                        COMPANY_CODE = Hcomp_code,
                        SEQ_OF_SUBGROUP = sg.RUN_NO,
                    };
                    //สร้าง Header ของ Sub Payment
                    #endregion
                    total += fee;
                    base.ctx.AG_IAS_SUBPAYMENT_D_T.AddObject(detail);


                }
                head = new AG_IAS_SUBPAYMENT_H_T
                {
                    HEAD_REQUEST_NO = headReqNo,
                    PETITION_TYPE_CODE = hPETITION_TYPE_CODE,
                    PERSON_NO = 1,
                    SUBPAYMENT_AMOUNT = total,
                    SUBPAYMENT_DATE = DateTime.Now.Date,
                    CREATED_BY = userId,
                    CREATED_DATE = DateTime.Now.Date,
                    UPDATED_BY = userId,
                    UPDATED_DATE = DateTime.Now.Date,
                    //UPLOAD_BY_SESSION = compCode,
                    UPLOAD_BY_SESSION = userId,
                    UPLOAD_GROUP_NO = uploadG,
                    DATE_EXP = lsDateExp.Min(),
                };
                base.ctx.AG_IAS_SUBPAYMENT_H_T.AddObject(head);
                lsSubPayment.Add(headReqNo);
                var lsOderInvoice = new List<DTO.OrderInvoice>();
                //DTO.OrderInvoice lsOderInvoice = new DTO.OrderInvoice();
                lsOderInvoice.Add(new DTO.OrderInvoice
                        {
                            UPLOAD_GROUP_NO = headReqNo,
                            RUN_NO = "1"

                        });

                base.ctx.SaveChanges();
                string groupRequestNoGT = string.Empty;
                var resPayment = NewCreatePayment(lsOderInvoice.ToList(), string.Empty, userId, compCode, "1", out groupRequestNoGT);

                if (resPayment.IsError)
                {
                    res.ErrorMsg = resPayment.ErrorMsg;
                }
                else
                {
                    using (TransactionScope tc = new TransactionScope())
                    {
                        base.ctx.SaveChanges();
                        tc.Complete();

                        groupHeaderNo = groupRequestNoGT;
                    }
                }
                res.DataResponse = groupRequestNoGT;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_SetSubGroupSingleLicense", ex);
            }
            return res;
        }
        /// <summary>
        /// ดึงข้อมูล SubPayment เพื่อออกใบสั่งจ่าย
        /// </summary>
        /// <returns>Collection ใบสั่งย่ายย่อย</returns>
        #region oldCode
        //public DTO.ResponseService<List<DTO.GroupPayment>> GetGroupPayment(string compCode, string startDate, string EndDate)
        //{
        //    var res = new DTO.ResponseService<List<DTO.GroupPayment>>();
        //    try
        //    {
        //        //var result = (from a in base.ctx.PT_AUTOMOBILE_SIZE_R
        //        //              from b in ctx.PT_AUTOMOBILE_TYPE_R
        //        //              from c in ctx.PT_USING_TYPE_R
        //        //              orderby a.AUTOMOBILE_SIZE_CODE
        //        //              where a.AUTOMOBILE_CODE == b.AUTOMOBILE_TYPE + c.USING_TYPE
        //        //              select new DAL.Repository.PTM00040Entity
        //        //              {
        //        //                  AUTOMOBILE_SIZE_CODE = a.AUTOMOBILE_SIZE_CODE,
        //        //                  AUTOMOBILE_CODE = a.AUTOMOBILE_CODE,
        //        //                  START_VALUE = a.START_VALUE,
        //        //                  END_VALUE = a.END_VALUE,
        //        //                  REMARK = a.REMARK,
        //        //                  USER_ID = a.USER_ID,
        //        //                  USER_DATE = a.USER_DATE,
        //        //                  TYPE_NAME = b.AUTOMOBILE_NAME + "-" + c.USING_DESCRIPTION
        //        //              }).ToList();
        //        DateTime date1 = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", startDate));
        //        DateTime date2 = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", EndDate));
        //        res.DataResponse = base.ctx.AG_IAS_SUBPAYMENT_H_T.Where(h => h.UPLOAD_BY_SESSION == compCode
        //            && h.SUBPAYMENT_DATE >= date1 && h.SUBPAYMENT_DATE <= date2 && h.GROUP_REQUEST_NO == null)
        //                                .Select(s => new DTO.GroupPayment
        //                                {
        //                                    Amount = s.SUBPAYMENT_AMOUNT,
        //                                    HeadNoSubPayment = s.HEAD_REQUEST_NO,
        //                                    PaymentType = s.PETITION_TYPE_CODE,
        //                                    PersonNo = s.PERSON_NO,
        //                                    SubPaymentDate = s.SUBPAYMENT_DATE
        //                                }).ToList() ;

        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }
        //    return res;
        //}
        #endregion
        public DTO.ResponseService<DataSet>
           GetGroupPayment(string compCode, DateTime? startDate, DateTime? EndDate, string UserT, string CompanyCode, int pageNo, int recordPerPage, string Count)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                string crit = string.Empty;
                string critRecNo = string.Empty;
                string AddComCode = string.Empty;
                string selectComcode = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();


                if (UserT == "1")
                {

                    crit = String.Format("and HT.CREATED_BY = '{0}' ", compCode);
                }
                else if (UserT == "2")
                {
                    crit = String.Format("and HT.UPLOAD_BY_SESSION = '{0}' ", compCode);

                }
                else if (UserT == "3")
                {
                    crit = String.Format("and HT.UPLOAD_BY_SESSION = '{0}' ", compCode);

                }
                if (CompanyCode != "")
                {
                    selectComcode = ",'" + CompanyCode + "' ";
                    AddComCode = "and '" + CompanyCode + "' like (select lh.comp_code from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no ) ";
                }
                else
                {
                    selectComcode = ",('')upload_by_session ";
                }
                string tmp = string.Empty;
                if (Count == "Y")
                {
                    //tmp = "SELECT COUNT(HT.HEAD_REQUEST_NO) rowcount " +
                    //             "FROM AG_IAS_SUBPAYMENT_H_T HT, " +
                    //             "AG_PETITION_TYPE_R P " +
                    //             "WHERE HT.GROUP_REQUEST_NO is null and HT.PETITION_TYPE_CODE = P.PETITION_TYPE_CODE AND " +
                    //             " " + crit + " and " +
                    //             "HT.SUBPAYMENT_DATE BETWEEN " +
                    //             "to_date('{0}','yyyymmdd') AND " +
                    //             "to_date('{1}','yyyymmdd') ";

                    tmp = "SELECT COUNT(*) rowcount FROM( "
                          + "SELECT  distinct(HEAD_REQUEST_NO),PETITION_TYPE_NAME,PERSON_NO,SUBPAYMENT_AMOUNT,SUBPAYMENT_DATE,upload_by_session, "
                          + "ROW_NUMBER() OVER (ORDER BY HEAD_REQUEST_NO) RUN_NO   "
                          + "FROM ( "
                          + "SELECT  distinct( dt.HEAD_REQUEST_NO), "
                          + "(select P.PETITION_TYPE_NAME from AG_PETITION_TYPE_R P where  HT.PETITION_TYPE_CODE = P.PETITION_TYPE_CODE )PETITION_TYPE_NAME, "
                          + "HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT, "
                          + "HT.SUBPAYMENT_DATE, "
                          + "(select ap.upload_by_session from AG_APPLICANT_T AP  where   dt.testing_no = ap.testing_no "
                          + "and dt.exam_place_code = ap.exam_place_code "
                          + "and dt.applicant_code = ap.applicant_code  and ap.upload_by_session = '" + CompanyCode + "' )  upload_by_session  "
                          + "FROM ag_ias_subpayment_d_t dt, AG_IAS_SUBPAYMENT_H_T HT "
                          + "WHERE HT.GROUP_REQUEST_NO is null "
                          + "and dt.head_request_no = ht.head_request_no "
                          + "and ht.petition_type_code = '01'  "
                           + crit
                          + "and upload_by_session Like '" + CompanyCode + "%' and "
                          + "HT.SUBPAYMENT_DATE BETWEEN "
                          + "to_date('{0}','yyyymmdd') AND "
                          + "to_date('{1}','yyyymmdd') "
                          + "union "
                          + "SELECT  distinct( dt.HEAD_REQUEST_NO), "
                          + "(select P.PETITION_TYPE_NAME from AG_PETITION_TYPE_R P where  HT.PETITION_TYPE_CODE = P.PETITION_TYPE_CODE )PETITION_TYPE_NAME, "
                          + "HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT,  "
                          + "HT.SUBPAYMENT_DATE " + selectComcode

                          + "FROM ag_ias_subpayment_d_t dt, AG_IAS_SUBPAYMENT_H_T HT   "
                          + "WHERE HT.GROUP_REQUEST_NO is null "
                          + "and dt.head_request_no = ht.head_request_no " + AddComCode

                          + "and ht.petition_type_code <> '01'  "
                          + crit
                          + "and HT.SUBPAYMENT_DATE BETWEEN "
                          + "to_date('{0}','yyyymmdd') AND "
                          + "to_date('{1}','yyyymmdd') "
                          + ")) ";
                    tmp = string.Format(tmp,
                      Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                      Convert.ToDateTime(EndDate).ToString_yyyyMMdd());

                    OracleDB ora = new OracleDB();
                    DataSet ds = ora.GetDataSet(tmp);
                    res.DataResponse = ds;
                }
                else
                {
                    tmp =
                        //"SELECT * FROM( SELECT  P.PETITION_TYPE_NAME,HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT, " +
                        //"HT.SUBPAYMENT_DATE,HT.HEAD_REQUEST_NO, " +
                        //"ROW_NUMBER() OVER (ORDER BY HT.HEAD_REQUEST_NO) RUN_NO " +
                        //"FROM AG_IAS_SUBPAYMENT_H_T HT, " +
                        //"AG_PETITION_TYPE_R P " +
                        //"WHERE HT.GROUP_REQUEST_NO is null and HT.PETITION_TYPE_CODE = P.PETITION_TYPE_CODE AND " +
                        //" " + crit + " and " +
                        //"HT.SUBPAYMENT_DATE BETWEEN " +
                        //"to_date('{0}','yyyymmdd') AND " +
                        //"to_date('{1}','yyyymmdd'))A " + critRecNo;
                             "SELECT * FROM( "
                            + "SELECT  distinct(HEAD_REQUEST_NO),PETITION_TYPE_NAME,PERSON_NO,SUBPAYMENT_AMOUNT,SUBPAYMENT_DATE,upload_by_session, "
                            + "ROW_NUMBER() OVER (ORDER BY HEAD_REQUEST_NO) RUN_NO   "
                            + "FROM ( "
                            + "SELECT  distinct( dt.HEAD_REQUEST_NO), "
                            + "(select P.PETITION_TYPE_NAME from AG_PETITION_TYPE_R P where  HT.PETITION_TYPE_CODE = P.PETITION_TYPE_CODE )PETITION_TYPE_NAME, "
                            + "HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT, "
                            + "HT.SUBPAYMENT_DATE, "
                            + "(select ap.upload_by_session from AG_APPLICANT_T AP  where   dt.testing_no = ap.testing_no "
                            + "and dt.exam_place_code = ap.exam_place_code "
                            + "and dt.applicant_code = ap.applicant_code and ap.upload_by_session = '" + CompanyCode + "'  )  upload_by_session  "
                            + "FROM ag_ias_subpayment_d_t dt, AG_IAS_SUBPAYMENT_H_T HT "
                            + "WHERE HT.GROUP_REQUEST_NO is null "
                            + "and dt.head_request_no = ht.head_request_no "
                            + "and ht.petition_type_code = '01'  "
                             + crit
                            + "and upload_by_session Like '" + CompanyCode + "%' and "
                            + "HT.SUBPAYMENT_DATE BETWEEN "
                            + "to_date('{0}','yyyymmdd') AND "
                            + "to_date('{1}','yyyymmdd') "
                            + "union "
                            + "SELECT  distinct( dt.HEAD_REQUEST_NO), "
                            + "(select P.PETITION_TYPE_NAME from AG_PETITION_TYPE_R P where  HT.PETITION_TYPE_CODE = P.PETITION_TYPE_CODE )PETITION_TYPE_NAME, "
                            + "HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT,  "
                            + "HT.SUBPAYMENT_DATE "
                            + selectComcode
                            + "FROM ag_ias_subpayment_d_t dt, AG_IAS_SUBPAYMENT_H_T HT ,ag_ias_license_h lh  "
                            + "WHERE HT.GROUP_REQUEST_NO is null "
                            + "and dt.head_request_no = ht.head_request_no "
                            + AddComCode
                            + "and ht.petition_type_code <> '01'  "
                            + crit
                            + "and HT.SUBPAYMENT_DATE BETWEEN "
                            + "to_date('{0}','yyyymmdd') AND "
                            + "to_date('{1}','yyyymmdd') "
                            + ")) A " + critRecNo;



                    tmp = string.Format(tmp,
                         Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                         Convert.ToDateTime(EndDate).ToString_yyyyMMdd());

                    OracleDB ora = new OracleDB();
                    DataSet ds = ora.GetDataSet(tmp);
                    res.DataResponse = ds;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetGroupPayment", ex);
            }
            return res;
        }
        /// <summary>
        /// สร้างใบสั่งจ่าย
        /// </summary>
        /// <param name="reqList">List ของ ใบสั่งจ่ายย่อย</param>
        /// <param name="paymentId">เลขที่ใบสั่งจ่าย</param>
        /// <param name="remark">หมายเหตุ</param>
        /// <param name="userId">user id</param>
        /// /// <param name="compCode">รหัสบริษัทประกัน หรือ รหัสสมาคม</param>
        /// <returns>true = Success, false = fail</returns>
        //สร้างใบสั่งจ่ายรายเดี่ยว สมัครสอบ
        public DTO.ResponseMessage<bool> CreatePayment(List<DTO.OrderInvoice> reqList, string remark,
                                                       string paymentId,
                                                       string userId,
                                                       string compCode, out string groupRequestNo)
        {
            ReferanceNumber referanceNumber = GenReferanceNumber.CreateReferanceNumber();
            groupRequestNo = referanceNumber.FirstNumber;
            var res = new DTO.ResponseMessage<bool>();

            if (reqList.Count == 0)
            {
                res.ErrorMsg = Resources.errorPaymentService_001;
                return res;
            }

            try
            {

                //int iCount = 0;
                decimal? total = 0;
                StringBuilder sb = new StringBuilder();
                List<Int16> LsPETITION_TYPE_CODE = new List<Int16>();
                foreach (DTO.OrderInvoice req in reqList)
                {
                    //  AG_IAS_SUBPAYMENT_H_T head = new AG_IAS_SUBPAYMENT_H_T

                    AG_IAS_SUBPAYMENT_H_T updateHT = base.ctx.AG_IAS_SUBPAYMENT_H_T
                                       .SingleOrDefault(s => s.HEAD_REQUEST_NO == req.UPLOAD_GROUP_NO);

                    updateHT.GROUP_REQUEST_NO = referanceNumber.FirstNumber;
                    //base.ctx.AG_IAS_SUBPAYMENT_H_T.(head);


                    var sub = base.ctx.AG_IAS_SUBPAYMENT_D_T
                                      .SingleOrDefault(s => s.HEAD_REQUEST_NO == req.UPLOAD_GROUP_NO);
                    if (sub != null)
                    {
                        total += sub.AMOUNT;
                    }
                    //var getDay = base.ctx.AG_IAS_PAYMENT_EXPIRE_DAY.SingleOrDefault(p => p.ID == sub.PETITION_TYPE_CODE);
                    //if (getDay != null)
                    //{
                    //    LsPETITION_TYPE_CODE.Add(Convert.ToInt16(getDay.PAYMENT_EXPIRE_DAY));
                    //}
                }

                AG_IAS_PAYMENT_G_T payment = new AG_IAS_PAYMENT_G_T
                {
                    CREATED_BY = userId,
                    CREATED_DATE = DateTime.Now.Date,
                    GROUP_AMOUNT = total,
                    GROUP_DATE = DateTime.Now.Date,
                    SUBPAYMENT_QTY = 1,
                    GROUP_REQUEST_NO = referanceNumber.FirstNumber,
                    REF2 = DateTime.Now.Date.ToString("ddMMyyyy", new System.Globalization.CultureInfo("en-US")),
                    EXPIRATION_DATE = DateTime.Now.Date, //DateTime.Now.AddDays(Convert.ToInt32(LsPETITION_TYPE_CODE.Min())),
                    UPDATED_BY = userId,
                    UPDATED_DATE = DateTime.Now.Date,
                    REMARK = remark,
                    UPLOAD_BY_SESSION = userId
                };
                base.ctx.AG_IAS_PAYMENT_G_T.AddObject(payment);

                using (var tc = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    tc.Complete();

                    //สำหรับ Update GROUP_REQUEST_NO กลับไปยัง AG_APPLICANT_T, AG_LICENSE_RENEW_T แต่ยังไม่ใช้
                    //string strIn = sb.ToString();
                    //strIn = strIn.Substring(0, strIn.Length - 1);
                    //if(strIn.Trim().Length > 0)
                    //{
                    //    string sqlApp = "UPDATE AG_APPLICANT_T SET GROUP_REQUEST_NO = '" + paymentId + "' " +
                    //                    "WHERE HEAD_REQUEST_NO IN(" + strIn + ")";
                    //    string sqlLic = "UPDATE AG_LICENSE_RENEW_T SET GROUP_REQUEST_NO = '" + paymentId + "' " +
                    //                    "WHERE HEAD_REQUEST_NO IN(" + strIn + ")";
                    //    OracleDB ora = new OracleDB();
                    //    ora.ExecuteCommand(sqlApp);
                    //    ora.ExecuteCommand(sqlLic);
                    //}
                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_CreatePayment", ex);
            }
            return res;
        }
        //ใบสั่งจ่ายรายกลุ่ม
        public DTO.ResponseMessage<bool> NewCreatePayment(List<DTO.OrderInvoice> Groups, string remark,

                                                     string userId,
                                                     string compCode, string dayExp, out string groupRequestNo)
        {
            ReferanceNumber referanceNumber = GenReferanceNumber.CreateReferanceNumber();
            groupRequestNo = referanceNumber.FirstNumber;
            var res = new DTO.ResponseMessage<bool>();


            if (Groups.Count == 0)
            {
                res.ErrorMsg = Resources.errorPaymentService_001;
                return res;
            }

            try
            {
                int? iCount = 0;
                decimal total = 0;
                string sqlDT = string.Empty;

                StringBuilder sb = new StringBuilder();
                List<Int16> LsPETITION_TYPE_CODE = new List<Int16>();
                List<DateTime?> LsTrainDate = new List<DateTime?>();
                List<DateTime?> LsTestingDate = new List<DateTime?>();
                string DateFormat = string.Empty;

                foreach (DTO.OrderInvoice req in Groups)
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                    var subHeadLicense = base.ctx.AG_IAS_SUBPAYMENT_H_T
                                  .SingleOrDefault(s => s.HEAD_REQUEST_NO == req.UPLOAD_GROUP_NO);
                    if (subHeadLicense != null)
                    {//DateTime.Today.ToString("dd/MM/yyyy");
                        if (subHeadLicense.DATE_EXP != null)
                        {
                            //DateFormat = Convert.ToDateTime(subHeadLicense.TRAIN_DATE_EXP).ToString("dd/MM/yyyy");
                            //DateFormat = DateUtil.dd_MM_yyyy_Now_TH;
                            LsTrainDate.Add(Convert.ToDateTime(subHeadLicense.DATE_EXP));
                        }
                        subHeadLicense.SEQ_OF_GROUP = req.RUN_NO;
                    }

                    var sub = base.ctx.AG_IAS_SUBPAYMENT_H_T
                                      .SingleOrDefault(s => s.HEAD_REQUEST_NO == req.UPLOAD_GROUP_NO);

                    if (sub != null)
                    {
                        sub.GROUP_REQUEST_NO = groupRequestNo;
                        total += sub.SUBPAYMENT_AMOUNT != null
                                    ? sub.SUBPAYMENT_AMOUNT.Value
                                    : 0;

                        iCount += sub.PERSON_NO;
                        //var getDay = base.ctx.AG_IAS_PAYMENT_EXPIRE_DAY.SingleOrDefault(p => p.ID == sub.PETITION_TYPE_CODE);
                        //if (getDay != null)
                        //{
                        //    LsPETITION_TYPE_CODE.Add(Convert.ToInt16(getDay.PAYMENT_EXPIRE_DAY));
                        //}
                        //sb.Append(string.Format("'{0}',", req));
                    }
                    //PETITION_TYPE_CODE = 01
                    var subHead = base.ctx.AG_IAS_SUBPAYMENT_H_T
                                      .SingleOrDefault(s => s.HEAD_REQUEST_NO == req.UPLOAD_GROUP_NO && s.PETITION_TYPE_CODE == "01");
                    if (subHead != null)
                    {
                        sqlDT = "select a.TESTING_NO , a.EXAM_PLACE_CODE ,a.APPLICANT_CODE,LR.TESTING_DATE "
                              + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,ag_applicant_t a,AG_EXAM_LICENSE_R LR "
                              + "where d.petition_type_code = p.petition_type_code "
                              + "and d.id_card_no = a.id_card_no "
                              + "and d.HEAD_REQUEST_NO = '" + subHead.HEAD_REQUEST_NO + "' "
                              + "and a.TESTING_NO = d.testing_no "
                              + "and a.EXAM_PLACE_CODE = d.exam_place_code "
                              + "and LR.TESTING_NO = d.testing_no "
                              + "and LR.EXAM_PLACE_CODE = d.exam_place_code "
                              + "and a.APPLICANT_CODE = d.applicant_code ";
                        OracleDB ora = new OracleDB();
                        DataSet ds = ora.GetDataSet(sqlDT);

                        DataTable dtDT = ds.Tables[0];
                        if (dtDT.Rows.Count != 0)
                        {
                            for (int i = 0; i < dtDT.Rows.Count; i++)
                            {
                                DataRow drDT = dtDT.Rows[i];
                                string testNo = drDT["TESTING_NO"].ToString();
                                string ExamPlaceCode = drDT["EXAM_PLACE_CODE"].ToString();
                                Int32 AppCode = Convert.ToInt32(drDT["APPLICANT_CODE"]);
                                var InsertApplicant = base.ctx.AG_APPLICANT_T.SingleOrDefault(a => a.TESTING_NO == testNo
                                    && a.EXAM_PLACE_CODE == ExamPlaceCode && a.APPLICANT_CODE == AppCode);
                                InsertApplicant.GROUP_REQUEST_NO = groupRequestNo;

                            }
                            // base.ctx.SaveChanges();
                        }

                    }


                }
                string chkCase = string.Empty;
                DateTime? expDate;
                string bindValue;
                if (dayExp == "1")
                {
                    bindValue = userId;

                    if (remark == "Exam")
                    {
                        expDate = DateTime.Now;
                    }
                    else
                    {
                        expDate = LsTrainDate.Min();
                    }
                }
                else
                {
                    bindValue = compCode;
                    expDate = LsTrainDate.Min();
                    chkCase = remark;
                }

                //  string CheckWeekend = GetReceiptDate(Convert.ToString(DateTime.Now.AddDays(Convert.ToInt32(LsPETITION_TYPE_CODE.Min()))));

                string MinDate = string.Empty;
                string dd = string.Empty;
                string mm = string.Empty;
                string yy = string.Empty;
                string ddAdd = string.Empty;
                if (LsTrainDate.Count > 0)
                {
                    //if (Convert.ToDateTime(LsTrainDate.Min()).Year < 2500)
                    //{
                    string[] ary = Convert.ToDateTime(LsTrainDate.Min()).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")).Split('/');
                    dd = ary[0];
                    mm = ary[1];
                    yy = ary[2];
                    //yy = Convert.ToString(Convert.ToInt32(ary[2]) - 543);
                    //if (dd.Length == 1)
                    //{
                    //    ddAdd = dd.PadLeft(0, '0');
                    //}
                    //else {
                    //    ddAdd = dd;
                    //}
                    MinDate = dd + mm + yy;
                    // }
                    //else if (Convert.ToDateTime(LsTrainDate.Min()).Year > 2500)
                    //{
                    //    string[] ary = Convert.ToDateTime(LsTrainDate.Min()).ToString("dd/MM/yyyy").Split('/');
                    //    dd = ary[0];
                    //    mm = ary[1];
                    //    yy = ary[2];

                    //    MinDate = dd + mm + yy;
                    //}
                }

                #region วันตัดยอดยกเลิกผู้สมัครสอบ
                var DCbiz = new IAS.DataServices.DataCenter.DataCenterService();
                DateTime CancleSeatDate = Convert.ToDateTime(LsTrainDate.Min()).AddDays(DCbiz.GetConficValueByTypeAndGroupCode("10", "AP001").DataResponse.ToInt());
                DateTime DateCancleSeat = Convert.ToDateTime((CancleSeatDate.ToString())); // By Milk
                #endregion วันตัดยอดยกเลิกผู้สมัครสอบ

                AG_IAS_PAYMENT_G_T payment = new AG_IAS_PAYMENT_G_T
                {
                    CREATED_BY = userId,
                    CREATED_DATE = DateTime.Now.Date,
                    GROUP_AMOUNT = total,
                    GROUP_DATE = DateTime.Now.Date,
                    SUBPAYMENT_QTY = Groups.Count(),
                    GROUP_REQUEST_NO = groupRequestNo,
                    UPDATED_BY = userId,
                    UPDATED_DATE = DateTime.Now.Date,
                    REMARK = chkCase,
                    UPLOAD_BY_SESSION = bindValue,
                    REF2 = MinDate,
                    EXPIRATION_DATE = expDate,
                    CANCLE_SEAT_DATE = DateCancleSeat,
                };


                base.ctx.AG_IAS_PAYMENT_G_T.AddObject(payment);
                base.ctx.SaveChanges();
                res.ResultMessage = true;
                // }


            }
            catch (Exception ex)
            {

                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_NewCreatePayment", ex);
            }
            return res;
        }

        private string GetCriteria(string criteria, string value)
        {
            return !string.IsNullOrEmpty(value)
                        ? string.Format(criteria, value.ClearQuote())
                        : string.Empty;
        }

        /// <summary>
        /// ดึงข้อมูลใบสั่งจ่าย
        /// </summary>
        /// <param name="compCode">รหัสบริษัท, รหัสสมาคม</param>
        /// <returns>DataSet</returns>
        public DTO.ResponseService<DataSet>
            GetAllGroupPayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;

                tmp = string.Format(
                             "SELECT  HT.HEAD_REQUEST_NO,HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT, " +
                             "HT.SUBPAYMENT_DATE,DT.RECEIPT_DATE,HT.REMARK " +
                             "FROM AG_IAS_SUBPAYMENT_H_T HT, " +
                             "AG_IAS_SUBPAYMENT_D_T DT " +
                             "WHERE HT.HEAD_REQUEST_NO = DT.HEAD_REQUEST_NO AND " +
                             "DT.RECEIPT_DATE BETWEEN " +
                             "to_date('{0}','yyyymmdd') AND " +
                             "to_date('{1}','yyyymmdd') ",
                             Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                             Convert.ToDateTime(toDate).ToString_yyyyMMdd());

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetAllGroupPayment", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลใบสั่งจ่าย
        /// </summary>
        /// <param name="compCode">รหัสบริษัท, รหัสสมาคม</param>
        /// <returns>DataSet</returns>
        public DTO.ResponseService<DataSet>
            GetSinglePayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode, DateTime? startExamDate, DateTime? EndExamDate, string licenseType, string testingNo, string para, int pageNo, int recordPerPage, string Totalrecoad)
        {
            var res = new DTO.ResponseService<DataSet>();
            string tableExam = string.Empty;
            string joinExam = string.Empty;
            try
            {

                StringBuilder sb = new StringBuilder();
                if (startExamDate != null && EndExamDate != null)
                {
                    sb.Append(string.Format(
                            " AND ex.testing_date BETWEEN " +
                            "    to_date('{0}','yyyymmdd') AND " +
                            "    to_date('{1}','yyyymmdd')  ",
                            Convert.ToDateTime(startExamDate).ToString_yyyyMMdd(),
                            Convert.ToDateTime(EndExamDate).ToString_yyyyMMdd()));
                    tableExam = ",AG_EXAM_LICENSE_R ex ";
                    joinExam = "and dt.testing_no = ex.testing_no and dt.exam_place_code = ex.exam_place_code ";

                }
                else if (startExamDate != null)
                {
                    sb.Append(GetCriteria("AND ex.testing_date =   to_date('{0}','yyyymmdd')  ", Convert.ToDateTime(startExamDate).ToString_yyyyMMdd()));
                    tableExam = ",AG_EXAM_LICENSE_R ex ";
                    joinExam = "and dt.testing_no = ex.testing_no and dt.exam_place_code = ex.exam_place_code ";
                }
                else if (EndExamDate != null)
                {
                    sb.Append(GetCriteria("AND ex.testing_date =   to_date('{0}','yyyymmdd')  ", Convert.ToDateTime(EndExamDate).ToString_yyyyMMdd()));
                    tableExam = ",AG_EXAM_LICENSE_R ex ";
                    joinExam = "and dt.testing_no = ex.testing_no and dt.exam_place_code = ex.exam_place_code ";
                }

                if (licenseType != "")
                {
                    sb.Append(GetCriteria("AND dt.LICENSE_TYPE_CODE like  '{0}%'  ", licenseType));
                }
                if (testingNo != "")
                {
                    sb.Append(GetCriteria("AND dt.TESTING_NO like  '{0}%'  ", testingNo));
                }
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                string condition = sb.ToString();
                string tmp = string.Empty;
                if (para == "5" && Totalrecoad == "Y")//คปภ.
                {
                    tmp = string.Format(
                                   "SELECT COUNT(GROUP_REQUEST_NO)rowcount FROM " +
                                   "(SELECT distinct( GT.GROUP_REQUEST_NO),GT.GROUP_AMOUNT,GT.GROUP_DATE, GT.PAYMENT_DATE,GT.REMARK, " +
                                   "GT.UPLOAD_BY_SESSION,GT.STATUS,GT.EXPIRATION_DATE,GT.LAST_PRINT  " +
                                   ",(select Count(HT.HEAD_REQUEST_NO) from ag_ias_subpayment_h_t HT " +
                                   "where ht.group_request_no = gt.group_request_no GROUP BY HT.GROUP_REQUEST_NO) as PERSON_NO " +
                                   "FROM ag_ias_subpayment_d_t dt,AG_IAS_PAYMENT_G_T GT,ag_ias_subpayment_h_t HT " + tableExam +
                                   "WHERE GT.group_request_no like  '" + paymentCode + "%' AND " +
                                   " ht.head_request_no = dt.head_request_no  " +
                                   "  and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " + joinExam +
                                   "and ht.group_request_no = gt.group_request_no " + condition +
                                   "and GT.GROUP_DATE BETWEEN " +
                                   "to_date('{0}','yyyymmdd') AND " +
                                   "to_date('{1}','yyyymmdd')) ",
                                   Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                                   Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                }
                else if (para == "5" && Totalrecoad == "N")
                {
                    tmp = string.Format(
                                 "SELECT * FROM( " +
                                 "SELECT GROUP_REQUEST_NO,GROUP_AMOUNT,GROUP_DATE, PAYMENT_DATE,REMARK, " +
                                 "UPLOAD_BY_SESSION,STATUS,EXPIRATION_DATE,LAST_PRINT  " +
                                 ", PERSON_NO,ROW_NUMBER() OVER (ORDER BY GROUP_REQUEST_NO) RUN_NO  FROM( " +
                                 "SELECT  distinct(GT.GROUP_REQUEST_NO),GT.GROUP_AMOUNT,GT.GROUP_DATE, " +
                                 "GT.PAYMENT_DATE,GT.REMARK,GT.UPLOAD_BY_SESSION,GT.STATUS,GT.EXPIRATION_DATE,GT.LAST_PRINT, " +
                                 "(select Count(HT.HEAD_REQUEST_NO) from ag_ias_subpayment_h_t HT where ht.group_request_no = gt.group_request_no " +
                                 "GROUP BY HT.GROUP_REQUEST_NO) as PERSON_NO " +
                                 "FROM ag_ias_subpayment_d_t dt,AG_IAS_PAYMENT_G_T GT,ag_ias_subpayment_h_t HT " + tableExam +
                                 "WHERE GT.group_request_no like  '" + paymentCode + "%' AND " +
                                 " ht.head_request_no = dt.head_request_no " +
                                 "  and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " + joinExam +

                                 " and ht.group_request_no = gt.group_request_no " + condition +
                                 "and GT.GROUP_DATE BETWEEN " +
                                 "to_date('{0}','yyyymmdd') AND " +
                                 "to_date('{1}','yyyymmdd'))) A ",
                                 Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                                 Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + critRecNo;
                }
                else if (para != "5" && Totalrecoad == "Y")
                {
                    if (para == "dt.id_card_no")
                    {
                        tmp = string.Format(
                          "SELECT COUNT(GROUP_REQUEST_NO)rowcount FROM " +
                          "(SELECT distinct( GT.GROUP_REQUEST_NO),GT.GROUP_AMOUNT,GT.GROUP_DATE, GT.PAYMENT_DATE,GT.REMARK, " +
                          "GT.UPLOAD_BY_SESSION,GT.STATUS,GT.EXPIRATION_DATE,GT.LAST_PRINT  " +
                          ",(select Count(HT.HEAD_REQUEST_NO) from ag_ias_subpayment_h_t HT " +
                          "where ht.group_request_no = gt.group_request_no GROUP BY HT.GROUP_REQUEST_NO) as PERSON_NO " +
                          "FROM ag_ias_subpayment_d_t DT,AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_h_t HT,AG_IAS_USERS AUser " + tableExam +
                          "WHERE " + para + " = '" + compCode + "' " +
                          "and gt.group_request_no like  '" + paymentCode + "%' AND " +
                          "ht.group_request_no = gt.group_request_no " +
                          "and ht.head_request_no = dt.head_request_no " + joinExam +
                           "and AUser.USER_NAME = dt.id_card_no " +
                           "and AUser.USER_ID = gt.UPLOAD_BY_SESSION " + condition +

                           "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                          "and gt.GROUP_DATE BETWEEN " +
                          "to_date('{0}','yyyymmdd') AND " +
                          "to_date('{1}','yyyymmdd')) ",
                          Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                          Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                    }
                    else
                    {
                        tmp = string.Format(
                            "SELECT COUNT(*) rowcount FROM " +
                            "(SELECT distinct( GT.GROUP_REQUEST_NO),GT.GROUP_AMOUNT,GT.GROUP_DATE, GT.PAYMENT_DATE,GT.REMARK, " +
                            "GT.UPLOAD_BY_SESSION,GT.STATUS,GT.EXPIRATION_DATE,GT.LAST_PRINT " +
                            ",(select Count(HT.HEAD_REQUEST_NO) from ag_ias_subpayment_h_t HT " +
                            "where ht.group_request_no = gt.group_request_no GROUP BY HT.GROUP_REQUEST_NO) as PERSON_NO " +
                            "FROM ag_ias_subpayment_d_t dt,AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_h_t HT " + tableExam +
                            "WHERE gt." + para + " = '" + compCode + "' " +
                            "and gt.group_request_no like  '" + paymentCode + "%' AND " +
                            " ht.head_request_no = dt.head_request_no  " + joinExam +
                            "and ht.group_request_no = gt.group_request_no " + condition +
                            "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +

                            "and gt.GROUP_DATE BETWEEN " +
                            "to_date('{0}','yyyymmdd') AND " +
                            "to_date('{1}','yyyymmdd')) ",
                            Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                            Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                    }
                }
                else if (para != "5" && Totalrecoad == "N")
                {
                    if (para == "dt.id_card_no")
                    {
                        tmp = string.Format(
                             "SELECT * FROM( " +
                             "SELECT GROUP_REQUEST_NO,GROUP_AMOUNT,GROUP_DATE, PAYMENT_DATE,REMARK, " +
                             "UPLOAD_BY_SESSION,STATUS,EXPIRATION_DATE " +
                             ", PERSON_NO,LAST_PRINT,ROW_NUMBER() OVER (ORDER BY GROUP_REQUEST_NO) RUN_NO  FROM( " +
                             "SELECT   distinct( GT.GROUP_REQUEST_NO),GT.GROUP_AMOUNT,GT.GROUP_DATE, " +
                             "GT.PAYMENT_DATE,GT.REMARK,GT.UPLOAD_BY_SESSION,GT.STATUS,GT.EXPIRATION_DATE,GT.LAST_PRINT , " +
                             "(select Count(HT.HEAD_REQUEST_NO) from ag_ias_subpayment_h_t HT where ht.group_request_no = gt.group_request_no " +
                             "GROUP BY HT.GROUP_REQUEST_NO) as PERSON_NO " +
                             "FROM ag_ias_subpayment_d_t DT,AG_IAS_PAYMENT_G_T GT,ag_ias_subpayment_h_t HT,AG_IAS_USERS AUser " + tableExam +
                             "WHERE " + para + " = '" + compCode + "' " +
                             "and AUser.USER_NAME = dt.id_card_no " +
                             "and AUser.USER_ID = gt.UPLOAD_BY_SESSION " +
                             "and ht.group_request_no = gt.group_request_no " + joinExam +
                             "and ht.head_request_no = dt.head_request_no " + condition +

                             "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +

                             "and GT.group_request_no like  '" + paymentCode + "%' AND " +
                             "GT.GROUP_DATE BETWEEN " +
                             "to_date('{0}','yyyymmdd') AND " +
                             "to_date('{1}','yyyymmdd')))A ",
                             Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                             Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + critRecNo;
                    }
                    else
                    {
                        tmp = string.Format(
                                    "SELECT * FROM( " +
                                     "SELECT GROUP_REQUEST_NO,GROUP_AMOUNT,GROUP_DATE, PAYMENT_DATE,REMARK, " +
                                     "UPLOAD_BY_SESSION,STATUS,EXPIRATION_DATE " +
                                     ", PERSON_NO,LAST_PRINT,ROW_NUMBER() OVER (ORDER BY GROUP_REQUEST_NO) RUN_NO  FROM( " +
                                     "SELECT   distinct( GT.GROUP_REQUEST_NO),GT.GROUP_AMOUNT,GT.GROUP_DATE, " +
                                     "GT.PAYMENT_DATE,GT.REMARK,GT.UPLOAD_BY_SESSION,GT.STATUS,GT.EXPIRATION_DATE,GT.LAST_PRINT,  " +
                                     "(select Count(HT.HEAD_REQUEST_NO) from ag_ias_subpayment_h_t HT where ht.group_request_no = gt.group_request_no " +
                                     "GROUP BY HT.GROUP_REQUEST_NO) as PERSON_NO " +
                                    "FROM ag_ias_subpayment_d_t dt,AG_IAS_PAYMENT_G_T GT,ag_ias_subpayment_h_t HT " + tableExam +
                                    "WHERE gt." + para + " = '" + compCode + "' " +

                                    "and ht.group_request_no = gt.group_request_no " + joinExam +
                                    "and ht.head_request_no = dt.head_request_no " + condition +
                                    "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +

                                    "and GT.group_request_no like  '" + paymentCode + "%' AND " +
                                    "GT.GROUP_DATE BETWEEN " +
                                    "to_date('{0}','yyyymmdd') AND " +
                                    "to_date('{1}','yyyymmdd')))A ",
                                    Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                                    Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + critRecNo;
                    }
                }

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetSinglePayment", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> getGroupDetail(string group_reuest)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //string sql = "SELECT  distinct( g.GROUP_REQUEST_NO),g.PERSON_NO,g.GROUP_AMOUNT,g.GROUP_DATE, " +
                //             "g.PAYMENT_DATE,g.REMARK,a.petition_type_name,g.EXPIRATION_DATE,g.ref2 " +
                //             "FROM AG_IAS_PAYMENT_G_T g,AG_IAS_SUBPAYMENT_H_T h,ag_petition_type_r a " +
                //             "WHERE g.GROUP_REQUEST_NO = '" + group_reuest + "' " +
                //             "and h.petition_type_code = a.petition_type_code " +
                //             "and g.group_request_no = h.group_request_no ";
                string sql = "SELECT  h.petition_type_code,sum(h.subpayment_amount) as sumAmt "
                         + ",g.GROUP_REQUEST_NO,g.subpayment_qty,g.GROUP_AMOUNT,g.GROUP_DATE, "
                         + "g.PAYMENT_DATE,g.REMARK,a.petition_type_name as BillName,g.EXPIRATION_DATE,g.ref2,g.UPLOAD_BY_SESSION "
                         + "FROM AG_IAS_PAYMENT_G_T g,AG_IAS_SUBPAYMENT_H_T h,ag_petition_type_r a "
                         + "WHERE g.GROUP_REQUEST_NO = '" + group_reuest + "' "
                         + "and h.petition_type_code = a.petition_type_code "
                         + "and g.group_request_no = h.group_request_no "
                         + "GROUP BY h.petition_type_code, g.GROUP_REQUEST_NO, g.subpayment_qty, g.GROUP_AMOUNT, "
                         + "g.GROUP_DATE, g.PAYMENT_DATE, g.REMARK, a.petition_type_name, g.EXPIRATION_DATE, g.ref2,g.UPLOAD_BY_SESSION  ";
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_getGroupDetail", ex);
            }
            return res;
        }
        public DTO.ResponseService<DataSet> getNamePaymentBy(string group_reuest)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                string sql = "select distinct offr.association_name,comc.name ,asp.names || ' ' || asp.lastname A_NAME,gt.upload_by_session from "
                           + "ag_ias_payment_g_t gt "
                           + "left outer join ag_ias_subpayment_h_t ht on ht.group_request_no = gt.group_request_no "
                           + "left outer join AG_IAS_ASSOCIATION offr on offr.association_code = gt.upload_by_session "
                           + "left outer join vw_ias_com_code comc on comc.id =gt.upload_by_session "
                           + "left outer join ag_ias_subpayment_d_t sd on ht.head_request_no=sd.head_request_no "
                           + "left outer join ag_ias_personal_t asp on sd.id_card_no = asp.id_card_no and asp.id = gt.upload_by_session "
                           + "where gt.group_request_no = '" + group_reuest + "' ";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_getNamePaymentBy", ex);
            }
            return res;
        }
        public DTO.ResponseService<DataSet> getBindbillPaymentExam(string groupRequestNo, string testNo, string appCode, string examPlaceCode)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                string sql = "select a.apply_date,b.exam_place_name,d.test_time,e.license_type_name,f.TESTING_DATE "
                           + "from ag_applicant_t a,ag_exam_place_r b,ag_exam_license_r c,ag_exam_time_r d "
                           + ",ag_ias_license_type_r e,AG_EXAM_LICENSE_R f "
                           + ",ag_ias_subpayment_d_t dt,ag_ias_subpayment_h_t ht,ag_ias_payment_g_t gt "
                           + "where a.testing_no = '" + testNo + "' "
                           + "and a.applicant_code = '" + appCode + "' "
                           + "and a.exam_place_code = '" + examPlaceCode + "' "
                           + "and a.exam_place_code = b.exam_place_code "
                           + "and a.testing_no = c.testing_no "
                           + "and a.exam_place_code = c.exam_place_code "
                           + "and c.test_time_code = d.test_time_code "
                           + "and c.license_type_code = e.license_type_code "
                           + "and gt.group_request_no = ht.group_request_no "
                           + "and ht.head_request_no = dt.head_request_no "
                           + "and f.testing_no = a.testing_no "
                           + "and f.exam_place_code = a.exam_place_code "
                           + "and ht.group_request_no = '" + groupRequestNo + "' ";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_getBindbillPaymentExam", ex);
            }
            return res;
        }
        /// <summary>
        /// ดึงข้อมูลใบสั่งจ่ายย่อย
        /// </summary>
        /// <param name="groupReqNo">รหัสใบสั่งจ่าย</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetSubPaymentByHeaderRequestNo(string groupReqNo, string CountRecord, int pageNo, int recordPerPage)
        {
            string sql = string.Empty;
            StringBuilder sb = new StringBuilder();

            string critRecNo = string.Empty;
            critRecNo = pageNo == 0
                            ? ""
                            : "WHERE A.RUN_NO BETWEEN " +
                                     pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                     pageNo.ToRowNumber(recordPerPage).ToString();
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //string sql = "SELECT PER.NAMES  || ' ' || PER.LASTNAME FIRSTLASTNAME, " +
                //             "PT.PETITION_TYPE_NAME,AHT.PERSON_NO,AHT.SUBPAYMENT_AMOUNT,AHT.SUBPAYMENT_DATE,ADT.HEAD_REQUEST_NO " +
                //             "FROM AG_IAS_SUBPAYMENT_H_T AHT, " +
                //             "AG_IAS_SUBPAYMENT_D_T ADT, " +
                //             "AG_IAS_PERSONAL_T PER, " +
                //             "AG_PETITION_TYPE_R PT " +
                //             "WHERE AHT.HEAD_REQUEST_NO = ADT.HEAD_REQUEST_NO AND " +
                //             "PER.ID_CARD_NO = ADT.ID_CARD_NO AND " +
                //             "AHT.PETITION_TYPE_CODE = PT.PETITION_TYPE_CODE AND " +
                //             "AHT.HEAD_REQUEST_NO = '" + groupReqNo.ClearQuote() + "' ";
                if (CountRecord == "Y")
                {
                    sql = "SELECT Count(*) rowcount FROM( "
                       + "SELECT ID_CARD_NO,FIRSTLASTNAME,petition_type_name,amount,record_status, "
                       + "created_date,subpayment_date,head_request_no,ROW_NUMBER () OVER (ORDER BY ID_CARD_NO) RUN_NO "
                       + "FROM (SELECT D.ID_CARD_NO,A.names || ' ' || A.lastname FIRSTLASTNAME, "
                       + "P.petition_type_name,D.amount,D.record_status record_status,H.created_date, "
                       + "H.subpayment_date,H .head_request_no "
                       + "FROM ag_ias_subpayment_d_t D "
                       + "left join ag_petition_type_r P on D.petition_type_code = P.petition_type_code "
                       + "left join ag_applicant_t A on D.id_card_no = A.id_card_no AND A.TESTING_NO = D.testing_no AND A.EXAM_PLACE_CODE = D.exam_place_code AND A.APPLICANT_CODE = D.applicant_code "
                       + ",(select * from ag_ias_subpayment_h_t where GROUP_REQUEST_NO = '" + groupReqNo + "') H  "
                       + "WHERE D.head_request_no = H.head_request_no and H.PETITION_TYPE_CODE = '01' "
                       + "UNION ALL "
                       + "SELECT DISTINCT(D .ID_CARD_NO),A.names || ' ' || A.lastname FIRSTLASTNAME, "
                       + "P.petition_type_name,D .amount,D.record_status record_status,H.created_date, "
                       + "H.subpayment_date,H.head_request_no "
                       + "FROM ag_ias_subpayment_d_t D,ag_petition_type_r P,AG_IAS_LICENSE_D A,ag_ias_subpayment_h_t H "
                       + "WHERE D.petition_type_code = P.petition_type_code "
                       + "AND D.id_card_no = A.id_card_no "
                       + "AND D.SEQ_NO = A.SEQ_NO "
                       + "AND D.UPLOAD_GROUP_NO = A.UPLOAD_GROUP_NO "
                       + "AND H.GROUP_REQUEST_NO = '" + groupReqNo + "' "
                       + "AND D.head_request_no = H.head_request_no and H.PETITION_TYPE_CODE not in (01)))A ";//+ critRecNo;

                }
                else
                {
                    //sql = "SELECT * FROM(select h.head_request_no,h.created_date,h.subpayment_date,h.subpayment_amount,h.person_no "
                    //       + ",p.petition_type_name, "
                    //       + "ROW_NUMBER() OVER (ORDER BY h.head_request_no) RUN_NO "
                    //       + "from ag_ias_subpayment_h_t h,ag_petition_type_r p "
                    //       + "where h.petition_type_code = p.petition_type_code "
                    //       + "and h.GROUP_REQUEST_NO = '" + groupReqNo + "')A " + critRecNo;

                    sql = "SELECT * FROM( "
                        + "SELECT ID_CARD_NO,FIRSTLASTNAME,petition_type_name,amount,record_status, "
                        + "created_date,subpayment_date,head_request_no,ROW_NUMBER () OVER (ORDER BY UPLOAD_GROUP_NO) RUN_NO "
                        + "FROM (SELECT D.ID_CARD_NO,A.names || ' ' || A.lastname FIRSTLASTNAME, "
                        + "P.petition_type_name,D.amount,D.record_status record_status,H.created_date, "
                        + "H.subpayment_date,H.head_request_no,d.UPLOAD_GROUP_NO "
                       + "FROM ag_ias_subpayment_d_t D "
                       + "left join ag_petition_type_r P on D.petition_type_code = P.petition_type_code "
                       + "left join ag_applicant_t A on D.id_card_no = A.id_card_no AND A.TESTING_NO = D.testing_no AND A.EXAM_PLACE_CODE = D.exam_place_code AND A.APPLICANT_CODE = D.applicant_code "
                       + ",(select * from ag_ias_subpayment_h_t where GROUP_REQUEST_NO = '" + groupReqNo + "') H  "
                       + "WHERE D.head_request_no = H.head_request_no and H.PETITION_TYPE_CODE = '01' "
                        + "UNION ALL "
                        + "SELECT DISTINCT(D .ID_CARD_NO),A.names || ' ' || A.lastname FIRSTLASTNAME, "
                        + "P.petition_type_name,D .amount,D.record_status record_status,H.created_date, "
                        + "H.subpayment_date,H.head_request_no,d.UPLOAD_GROUP_NO "
                        + "FROM ag_ias_subpayment_d_t D,ag_petition_type_r P,AG_IAS_LICENSE_D A, "
                        + "(select * from ag_ias_subpayment_h_t  where GROUP_REQUEST_NO = '" + groupReqNo + "') H "
                        + "WHERE D.petition_type_code = P.petition_type_code "
                        + "AND D.id_card_no = A.id_card_no "
                        + "AND D.SEQ_NO = A.SEQ_NO "
                        + "AND D.UPLOAD_GROUP_NO = A.UPLOAD_GROUP_NO "
                        + "AND D.head_request_no = H.head_request_no and H.PETITION_TYPE_CODE not in (01)))A " + critRecNo;

                }
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetSubPaymentByHeaderRequestNo", ex);
            }
            return res;
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
                                         string firstName, string lastName, int resultPage, int PageSize, Boolean CountAgain)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                #region โค้ดเก่า
                //StringBuilder sb = new StringBuilder();
                //sb.Append(GetCriteria("P.ID_CARD_NO LIKE '{0}%' AND ", idCard));
                //sb.Append(GetCriteria("PR.PETITION_TYPE_CODE = '{0}' AND ", petitionTypeCode));
                //sb.Append(GetCriteria("P.NAMES LIKE '%{0}%' AND ", firstName));
                //sb.Append(GetCriteria("P.LASTNAME LIKE '%{0}%' AND ", lastName));

                //string tmp = sb.ToString();

                //string crit = tmp.Length > 4
                //                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                //                : tmp;

                //string FirstCon = string.Empty;
                //string MidCon = string.Empty;
                //string LastCon = string.Empty;

                //if (resultPage == 0 && PageSize == 0 && CountAgain == false)//รายงาน
                //{
                //    MidCon = " , ROW_NUMBER() OVER (ORDER BY SD.RECEIPT_NO) RUN_NO ";

                //}
                //else
                //{
                //    if (CountAgain)
                //    {
                //        FirstCon = "select count(*) CCount from ( ";
                //        MidCon = " ";
                //        LastCon = " ) ";
                //    }
                //    else
                //    {
                //        FirstCon = "select * from ( ";
                //        MidCon = " , ROW_NUMBER() OVER (ORDER BY SD.RECEIPT_NO) RUN_NO ";
                //        LastCon = "  order by RUN_NO asc ) A  WHERE A.RUN_NO BETWEEN " +
                //                                 resultPage.StartRowNumber(PageSize).ToString() + " AND " +
                //                                 resultPage.ToRowNumber(PageSize).ToString() + " order by A.RUN_NO asc ";
                //    }
                //}

                //string sql = FirstCon + " SELECT DISTINCT  SD.RECEIPT_NO, " +
                //             "       PR.PETITION_TYPE_NAME, " +
                //             "       TT.PRE_FULL || ' ' || P.NAMES || '  ' || P.LASTNAME FLNAME, " +
                //             "       P.ID_CARD_NO, SD.PAYMENT_DATE, SD.RECEIPT_DATE ORDER_DATE, SD.LICENSE_NO, SD.AMOUNT, " +
                //             "       case when SD.PRINT_TIMES is not null then SD.PRINT_TIMES else 0 end as PRINT_TIMES " + MidCon +
                //             "FROM	AG_PETITION_TYPE_R PR, " +
                //             "      AG_IAS_SUBPAYMENT_D_T SD, " +
                //             "      AG_IAS_PERSONAL_T P, " +
                //             "      GB_PREFIX_R TT " +
                //             "WHERE SD.PETITION_TYPE_CODE = PR.PETITION_TYPE_CODE and sd.receipt_no is not null AND " +
                //             "      SD.ID_CARD_NO = P.ID_CARD_NO AND " +
                //             "      P.PRE_NAME_CODE = TT.PRE_CODE " + crit + " " + LastCon;
           
                #endregion

                StringBuilder sqlCondition = new StringBuilder();
                sqlCondition.Append(GetCriteria(" and A.ID_CARD_NO LIKE '{0}%' ", idCard));
                sqlCondition.Append(GetCriteria(" and A.PETITION_TYPE_NAME = (select PETITION_TYPE_NAME from AG_PETITION_TYPE_R where PETITION_TYPE_CODE ='{0}') ", petitionTypeCode));
                if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                    sqlCondition.Append(string.Format(" and SUBSTR(A.FULL_NAME,INSTR(A.FULL_NAME,' ')+1) like '%{0}%{1}%' ", firstName, lastName));

                string sqlQry = string.Format(
                            @" 
                             select A.PETITION_TYPE_NAME, A.RECEIPT_NO, SUBSTR(A.FULL_NAME,INSTR(A.FULL_NAME,' ')+1) FLNAME, A.ID_CARD_NO, 
                                    A.PAYMENT_DATE, A.RECEIPT_DATE ORDER_DATE, A.AMOUNT,
                                    case when A.PRINT_TIMES is not null then A.PRINT_TIMES else 0 end as PRINT_TIMES
                             from AG_IAS_SUBPAYMENT_RECEIPT A
                             where A.PAYMENT_DATE is not null and A.RECEIPT_DATE is not null {0}
                             order by A.PRINT_TIMES desc ", sqlCondition);
                OracleDB db = new OracleDB();
                DataSet ds = db.GetDataSet(sqlQry);

                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetReportNumberPrintBill", ex);
            }
            return res;
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
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                string sql = string.Empty;
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();


                if (startDate != null && toDate != null)
                {
                    sb.Append(string.Format(
                              " (GP.GROUP_DATE BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd')) AND ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd()));
                }

                //ใบสั่งจ่ายค่าสมัครสอบ
                if (paymentType == "01")
                {
                    sb.Append(GetCriteria(" SPD.RECEIPT_NO = '{0}' AND ", billNo));

                    if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                    {
                        sb.Append(GetCriteria(" AP.ID_CARD_NO = '{0}' AND ", idCard));
                    }
                    else
                        //บริษัท หรือ สมาคม
                        if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue() ||
                            userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                        {
                            sb.Append(GetCriteria(" AP.UPLOAD_BY_SESSION = '{0}' AND ", userProfile.CompCode));
                        }

                    string condition = sb.ToString();

                    string crit = condition.Length > 4
                                    ? " AND " + condition.Substring(0, condition.Length - 4)
                                    : condition;

                    #region SQL Statement

                    sql = "SELECT * " +
                          "FROM ( " +
                          "         SELECT	'สมัครสอบ' AS PAYMENT_TYPE_NAME, AP.ID_CARD_NO, " +
                          "                 TT.NAME ||' '|| AP.NAMES FIRST_NAME, " +
                          "                 AP.LASTNAME, GP.GROUP_DATE, GP.GROUP_REQUEST_NO, " +
                          "                 SPD.RECEIPT_NO, AP.APPLICANT_CODE, AP.TESTING_NO, " +
                          "                 AP.EXAM_PLACE_CODE, " +
                          "                 ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO, AP.APPLICANT_CODE ASC) RUN_NO " +
                          "         FROM    AG_APPLICANT_T			AP, " +
                          "                 AG_IAS_SUBPAYMENT_H_T  SPH, " +
                          "                 AG_IAS_SUBPAYMENT_D_T	SPD, " +
                          "                 AG_IAS_PAYMENT_G_T		GP, " +
                          "                 VW_IAS_TITLE_NAME		TT " +
                          "         WHERE	AP.PRE_NAME_CODE = TT.ID AND " +
                          "                 GP.GROUP_REQUEST_NO = SPH.GROUP_REQUEST_NO AND " +
                          "                 SPH.HEAD_REQUEST_NO = SPD.HEAD_REQUEST_NO AND " +
                          "                 SPD.HEAD_REQUEST_NO = AP.HEAD_REQUEST_NO " + crit +
                          ") A " + critRecNo;

                    #endregion
                }
                else //ใบสั่งจ่ายอื่น ๆ
                {
                    //บุคคลธรรมดา
                    if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                    {
                        sb.Append(GetCriteria(" LN.LICENSE_NO = '{0}' AND ", userProfile.LicenseNo));
                    }
                    //บริษัท หรือ สมาคม
                    else
                    {
                        sb.Append(GetCriteria(" LN.UPLOAD_BY_SESSION = '{0}' AND ", userProfile.CompCode));
                    }

                    string condition = sb.ToString();

                    string crit = condition.Length > 4
                                    ? " AND " + condition.Substring(0, condition.Length - 4)
                                    : condition;

                    #region SQL Statement

                    sql = "SELECT * " +
                          "FROM ( " +
                          "         SELECT	PT.PETITION_TYPE_NAME, SPD.ID_CARD_NO, " +
                          "		            PER.FIRST_NAME, PER.LASTNAME, GP.GROUP_DATE, " +
                          "                 GP.GROUP_REQUEST_NO, SPD.RECEIPT_NO, " +
                          "		            SPD.LICENSE_NO, SPD.RENEW_TIME, " +
                          "                 ROW_NUMBER() OVER (ORDER BY RE.LICENSE_NO, RE.RENEW_TIME ASC) RUN_NO " +
                          "         FROM	AG_IAS_SUBPAYMENT_H_T	SPH, " +
                          "		            AG_IAS_SUBPAYMENT_D_T	SPD, " +
                          "                 AG_IAS_PAYMENT_G_T		GP, " +
                          "                 AG_PETITION_TYPE_R		PT, " +
                          "                 ( " +
                          "                     SELECT	AG.LICENSE_NO, AG.ID_CARD_NO, " +
                          "                             TT.NAME ||' '|| P.NAMES FIRST_NAME, P.LASTNAME " +
                          "                     FROM	AG_PERSONAL_T P, " +
                          "                             VW_IAS_TITLE_NAME TT, " +
                          "                             ( " +
                          "                                 SELECT LICENSE_NO, ID_CARD_NO " +
                          "                                 FROM AG_AGENT_LICENSE_PERSON_T " +
                          "                                 UNION " +
                          "                                 SELECT LICENSE_NO, ID_CARD_NO " +
                          "                                 FROM AG_AGENT_LICENSE_T " +
                          "                             ) AG " +
                          "                     WHERE	P.PRE_NAME_CODE = TT.ID AND " +
                          "                             P.ID_CARD_NO = AG.ID_CARD_NO " +
                          "                 ) PER, " +
                          "                 AG_LICENSE_RENEW_T RE " +
                          "         WHERE	GP.GROUP_REQUEST_NO = SPH.GROUP_REQUEST_NO AND " +
                          "                 SPH.HEAD_REQUEST_NO = SPD.HEAD_REQUEST_NO AND " +
                          "                 SPD.HEAD_REQUEST_NO = RE.HEAD_REQUEST_NO AND " +
                          "                 SPD.PETITION_TYPE_CODE = PT.PETITION_TYPE_CODE AND " +
                          "                 SPD.LICENSE_NO = RE.LICENSE_NO AND " +
                          "                 SPD.RENEW_TIME = RE.RENEW_TIME AND " +
                          "                 SPD.ID_CARD_NO = PER.ID_CARD_NO " + crit +
                          ") A " + critRecNo;

                    #endregion
                }

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentByCriteria", ex);
            }
            return res;
        }
        ///<summary>
        /// ดึงข้อมูลกลุ่มสนามสอบ
        /// </summary>
        public DTO.ResponseService<DataSet> GetGroupExam(int type, string Code)
        {
            var res = new DTO.ResponseService<DataSet>();
            string SQLtemp = string.Empty;
            //4=เจ้าหน้าที่คปภ.admin
            //5=คปภ.การเงิน
            //6=คปภ.ตัวแทน
            //7=เจ้าหน้าที่สนามสอบ
            try
            {
                SQLtemp = "SELECT EXAM_PLACE_GROUP_CODE ||' ' || EXAM_PLACE_GROUP_NAME GROUP_NAME, " +
                               " EXAM_PLACE_GROUP_CODE GROUP_ID " +
                               " FROM AG_EXAM_PLACE_GROUP_R ";
                switch (type)
                {
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        SQLtemp = SQLtemp + " WHERE ACTIVE ='Y'  order by EXAM_PLACE_GROUP_CODE       ";
                        break;
                    case 7:
                        SQLtemp = SQLtemp + " where exam_place_group_code = '" + Code + "'   and ACTIVE ='Y'  " +
                              " order by EXAM_PLACE_GROUP_CODE                 ";
                        break;
                    default:
                        SQLtemp = SQLtemp + "";
                        break;
                }

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(SQLtemp);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetGroupExam", ex);
            }
            return res;


        }
        public DTO.ResponseService<DataSet> GetExamCode(string Code)
        {
            var res = new DTO.ResponseService<DataSet>();
            string SQLtemp = string.Empty;
            //4=เจ้าหน้าที่คปภ.admin
            //5=คปภ.การเงิน
            //6=คปภ.ตัวแทน
            //7=เจ้าหน้าที่สนามสอบ
            try
            {
                //SQLtemp = "SELECT EXAM_PLACE_CODE ||' '|| EXAM_PLACE_NAME || '('  || v.name || ')'  PLACE_NAME ,  " +
                //            " EXAM_PLACE_CODE PLACE_ID " +
                //            " FROM AG_EXAM_PLACE_R,  vw_ias_province v  " +
                //            " WHERE EXAM_PLACE_GROUP_CODE = '" + Code + "' AND v.id = AG_EXAM_PLACE_R.PROVINCE_CODE ";

                SQLtemp = "SELECT '['  || v.name || '] ' || EXAM_PLACE_NAME  PLACE_NAME ,  " +
                          " EXAM_PLACE_CODE PLACE_ID " +
                          " FROM AG_EXAM_PLACE_R,  vw_ias_province v  " +
                          " WHERE EXAM_PLACE_GROUP_CODE = '" + Code + "' AND v.id = AG_EXAM_PLACE_R.PROVINCE_CODE ";
                SQLtemp = SQLtemp + " order by EXAM_PLACE_CODE";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(SQLtemp);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetExamCode", ex);
            }
            return res;


        }

        /// <summary>
        /// ดึงข้อมูลรายละเอียดการชำระ
        /// </summary>
        /// <param name="applicantCode">เลขที่สอบ</param>
        /// <param name="testingNo">เลขที่การสอบ</param>
        /// <param name="examPlace_code">รหัสสนามสอบ</param>
        /// <param name="licenseNo">เลขที่ใบอนุญาต</param>
        /// <param name="renewTime">จำนวนครั้งที่ต่อใบอนุญาต</param>
        /// <param name="isApplicant">เป็นรายการค่าสมัครสอบใช่หรือไม่</param>
        /// <returns>ResponseService<PaymentDetail></returns>
        public DTO.ResponseService<DTO.PaymentDetail>
            GetPaymentDetail(string applicantCode, string testingNo, string examPlace_code,
                             string licenseNo, string renewTime, bool isApplicant)
        {
            var res = new DTO.ResponseService<DTO.PaymentDetail>();
            try
            {
                string sql = string.Empty;

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.APPLICANT_CODE = {0} AND ", applicantCode));
                sb.Append(GetCriteria("AP.TESTING_NO = {0} AND ", testingNo));
                sb.Append(GetCriteria("AP.EXAM_PLACE_CODE = {0} AND ", examPlace_code));

                sb.Append(GetCriteria("RE.LICENSE_NO = {0} AND ", licenseNo));
                sb.Append(GetCriteria("RE.RENEW_TIME = {0} AND ", renewTime));

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                    ? " AND " + condition.Substring(0, condition.Length - 4)
                                    : condition;

                if (isApplicant)
                {

                    #region SQL Statement

                    sql = "SELECT    GP.GROUP_REQUEST_NO  PAYMENT_NO, AP.TESTING_NO, " +
                                 "          GP.GROUP_DATE  PAYMENT_DATE, 'ค่าสมัครสอบ' PETITION_TYPE_NAME, " +
                                 "          AP.ID_CARD_NO, SPD.COMPANY_CODE, SPD.RECEIPT_NO, SPD.RECEIPT_DATE, " +
                                 "		    SPD.AMOUNT " +

                                 "FROM	    AG_IAS_PAYMENT_G_T		GP, " +
                                 "          AG_IAS_SUBPAYMENT_H_T	SPH, " +
                                 "          AG_IAS_SUBPAYMENT_D_T	SPD, " +
                                 "          AG_APPLICANT_T			AP " +

                                 "WHERE	    GP.GROUP_REQUEST_NO = SPH.GROUP_REQUEST_NO AND " +
                                 "          SPH.HEAD_REQUEST_NO = SPD.HEAD_REQUEST_NO AND " +
                                 "          SPD.HEAD_REQUEST_NO = AP.HEAD_REQUEST_NO AND " +
                                 "          SPD.APPLICANT_CODE = AP.APPLICANT_CODE AND " +
                                 "          SPD.TESTING_NO = AP.TESTING_NO AND " +
                                 "          SPD.EXAM_PLACE_CODE = AP.EXAM_PLACE_CODE " + crit;


                    #endregion

                }
                //License
                else
                {
                    sql = "SELECT   GP.GROUP_REQUEST_NO  PAYMENT_NO, GP.GROUP_DATE  PAYMENT_DATE, " +
                          "         PT.PETITION_TYPE_NAME, SPD.ID_CARD_NO, SPD.COMPANY_CODE, " +
                          "         RE.LICENSE_NO, SPD.RECEIPT_NO, SPD.RECEIPT_DATE, " +
                          "         SPD.OLD_LICENSE_NO LICENSE_NO_REQUEST, SPD.RECEIPT_NO, " +
                          "         SPD.RECEIPT_DATE, SPD.AMOUNT, SPD.LICENSE_TYPE_CODE " +

                          "FROM	    AG_IAS_PAYMENT_G_T		GP, " +
                          "         AG_IAS_SUBPAYMENT_H_T	SPH, " +
                          "         AG_IAS_SUBPAYMENT_D_T	SPD, " +
                          "         AG_LICENSE_RENEW_T		RE, " +
                          "         AG_PETITION_TYPE_R		PT " +

                          "WHERE	GP.GROUP_REQUEST_NO = SPH.GROUP_REQUEST_NO AND " +
                          "         SPH.HEAD_REQUEST_NO = SPD.HEAD_REQUEST_NO AND " +
                          "         SPD.HEAD_REQUEST_NO = RE.HEAD_REQUEST_NO AND " +
                          "         SPD.LICENSE_NO = RE.LICENSE_NO AND " +
                          "         SPD.RENEW_TIME = RE.RENEW_TIME AND " +
                          "         PT.PETITION_TYPE_CODE = RE.PETITION_TYPE_CODE " + crit;
                }

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    res.DataResponse = dr.MapToEntity<DTO.PaymentDetail>();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentDetail", ex);
            }
            return res;
        }


        #endregion


        /// <summary>
        /// เพิ่มและตรวจสอบข้อมูลการเงินเข้า Temp
        /// </summary>
        /// <param name="data">ข้อมูลดิบ</param>
        /// <param name="fileName">ชื่อไฟล์</param>
        /// <param name="userId">user id</param>
        /// <returns>ResponseService<UploadResult<SummaryBankTransaction, BankTransaction>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>
            InsertAndCheckPaymentUpload(DTO.UploadData data, string fileName, string userId)
        {

            var res = new DTO.ResponseService<DTO.UploadResult<DTO.SummaryBankTransaction, DTO.BankTransaction>>();

            try
            {

                if (data.Body.FirstOrDefault().Substring(0, 1) == "H")
                {

                    String banktype = BankType.KTB.ToString();
                    BankFileHeader bankHeader = BankFileFactory.ConcreateKTBFileTransfer(ctx, fileName, data);
                    //IEnumerable<AG_IAS_PAYMENT_HEADER> findSame = ctx.AG_IAS_PAYMENT_HEADER.Where(a => a.RECORD_TYPE == bankHeader.RECORD_TYPE
                    //                                                                && a.SEQUENCE_NO == bankHeader.SEQUENCE_NO
                    //                                                                && a.BANK_CODE == bankHeader.BANK_CODE
                    //                                                                && a.COMPANY_ACCOUNT == bankHeader.COMPANY_ACCOUNT
                    //                                                                && a.COMPANY_NAME == bankHeader.COMPANY_NAME
                    //                                                                && a.SERVICE_CODE == bankHeader.SERVICE_CODE
                    //                                                                && a.EFFECTIVE_DATE == bankHeader.EFFECTIVE_DATE
                    //                                                                && a.BANK == banktype);

                    //if (findSame != null) {
                    //    res.ErrorMsg = Resources.errorPaymentUploadFileBankDup;
                    //    return res;
                    //}
                    res.DataResponse = bankHeader.ValidateData();
                    if (res.IsError)
                    {
                        return res;
                    }
                    

                    AG_IAS_TEMP_PAYMENT_HEADER payment_g_t = new AG_IAS_TEMP_PAYMENT_HEADER();
                    bankHeader.MappingToEntity<BankFileHeader, AG_IAS_TEMP_PAYMENT_HEADER>(payment_g_t);
                    ctx.AG_IAS_TEMP_PAYMENT_HEADER.AddObject(payment_g_t);

                    foreach (BankFileDetail item in bankHeader.KTBFileDetails)
                    {
                        AG_IAS_TEMP_PAYMENT_DETAIL detail = new AG_IAS_TEMP_PAYMENT_DETAIL();
                        item.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL>(detail);
                        detail.STATUS = (Int16)item.Status;
                        ctx.AG_IAS_TEMP_PAYMENT_DETAIL.AddObject(detail);
                    }

                    AG_IAS_TEMP_PAYMENT_TOTAL total = new AG_IAS_TEMP_PAYMENT_TOTAL();
                    bankHeader.KTBFileTotal.MappingToEntity<AG_IAS_TEMP_PAYMENT_TOTAL>(total);
                    ctx.AG_IAS_TEMP_PAYMENT_TOTAL.AddObject(total);
                }
                else if (data.Body.FirstOrDefault().Substring(0, 1) == "1")
                {

                    CityFileHeader bankHeader = BankFileFactory.ConcreateCityBankFileTransfer(ctx, fileName, data);
                    String banktype = BankType.CIT.ToString();
                    //IEnumerable<AG_IAS_PAYMENT_HEADER> findSame = ctx.AG_IAS_PAYMENT_HEADER.Where(a => a.RECORD_TYPE == bankHeader.RECORD_TYPE
                    //                                                                && a.SEQUENCE_NO == bankHeader.SEQUENCE_NO
                    //                                                                && a.BANK_CODE == bankHeader.BANK_CODE
                    //                                                                && a.COMPANY_ACCOUNT == bankHeader.COMPANY_ACCOUNT
                    //                                                                && a.COMPANY_NAME == bankHeader.COMPANY_NAME
                    //                                                                && a.SERVICE_CODE == bankHeader.SERVICE_CODE
                    //                                                                && a.EFFECTIVE_DATE == bankHeader.EFFECTIVE_DATE
                    //                                                                && a.BANK == banktype);

                    //if (findSame != null)
                    //{
                    //    res.ErrorMsg = Resources.errorPaymentUploadFileBankDup;
                    //    return res;
                    //}
                    res.DataResponse = bankHeader.ValidateData();
                    if (res.IsError)
                    {
                        return res;
                    }

                    AG_IAS_TEMP_PAYMENT_HEADER payment_g_t = new AG_IAS_TEMP_PAYMENT_HEADER();
                    bankHeader.MappingToEntity<CityFileHeader, AG_IAS_TEMP_PAYMENT_HEADER>(payment_g_t);
                    ctx.AG_IAS_TEMP_PAYMENT_HEADER.AddObject(payment_g_t);

                    foreach (CityFileDetail item in bankHeader.CityFileDetails)
                    {
                        AG_IAS_TEMP_PAYMENT_DETAIL detail = new AG_IAS_TEMP_PAYMENT_DETAIL();
                        item.MappingToEntity<AG_IAS_TEMP_PAYMENT_DETAIL>(detail);
                        detail.STATUS = (Int16)item.Status;
                        ctx.AG_IAS_TEMP_PAYMENT_DETAIL.AddObject(detail);
                    }


                    ctx.AG_IAS_TEMP_PAYMENT_TOTAL.AddObject(bankHeader.GetAG_IAS_TEMP_PAYMENT_TOTAL());
                }


                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_InsertAndCheckPaymentUpload", ex);
            }

            return res;
        }

        /// <summary>
        /// ดึงรายละเอียดการชำระเงิน
        /// </summary>
        /// <param name="headerId">เลขที่กลุ่ม</param>
        /// <param name="Id">เลขที่รายการ</param>
        /// <returns>ResponseService<BankTransDetail></returns>
        public DTO.ResponseService<DTO.BankTransDetail> GetTempBankTransDetail(string headerId, string Id)
        {
            var res = new DTO.ResponseService<DTO.BankTransDetail>();
            var ent = new DTO.BankTransDetail();
            try
            {
                var header = base.ctx.AG_IAS_TEMP_PAYMENT_HEADER
                                     .SingleOrDefault(s => s.ID == headerId);

                var total = base.ctx.AG_IAS_TEMP_PAYMENT_TOTAL
                                    .SingleOrDefault(s => s.HEADER_ID == headerId);

                var detail = base.ctx.AG_IAS_TEMP_PAYMENT_DETAIL
                                     .SingleOrDefault(s => s.HEADER_ID == headerId &&
                                                           s.ID == Id);
                if (header == null || total == null || detail == null)
                {
                    res.ErrorMsg = Resources.errorPaymentService_004;
                    return res;
                }
                detail.MappingToEntity(ent);

                if (header != null)
                {
                    ent.BankType = header.RECORD_TYPE;
                    ent.COMPANY_NAME = header.COMPANY_NAME;
                    ent.SERVICE_CODE = header.SERVICE_CODE;
                    ent.EFFECTIVE_DATE = header.EFFECTIVE_DATE;
                }

                if (total != null)
                {
                    ent.TOTAL_DEBIT_AMOUNT = total.TOTAL_DEBIT_AMOUNT;
                    ent.TOTAL_CREDIT_AMOUNT = total.TOTAL_CREDIT_AMOUNT;
                }

                res.DataResponse = ent;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorPaymentService_004;
                LoggerFactory.CreateLog().Fatal("PaymentService_GetTempBankTransDetail", ex);
            }
            return res;
        }


        public DTO.ResponseService<DTO.SubPaymentHead> GetSubPaymentHeadByHeadRequestNo(string headReqNo)
        {
            var res = new DTO.ResponseService<DTO.SubPaymentHead>();
            res.DataResponse = new DTO.SubPaymentHead();
            try
            {
                DAL.AG_IAS_SUBPAYMENT_H_T ent = base.ctx.AG_IAS_SUBPAYMENT_H_T.SingleOrDefault(
                                                s => s.HEAD_REQUEST_NO == headReqNo);


                if (ent != null)
                {
                    ent.MappingToEntity(res.DataResponse);
                    DAL.AG_IAS_PAYMENT_G_T pay = base.ctx.AG_IAS_PAYMENT_G_T.SingleOrDefault(a => a.GROUP_REQUEST_NO == ent.GROUP_REQUEST_NO);
                    //res.DataResponse.EXPIRATION_DATE = pay.EXPIRATION_DATE;
                    //res.DataResponse.PAYMENT_BY = pay.PAYMENT_BY;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetSubPaymentHeadByHeadRequestNo", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetPaymentDetailByGroup(int type, string Gcode, string Ccode,
                                            DateTime? startDate, DateTime? toDate
                                            , int RowPerPage, int num_page, Boolean Count,string CompCode)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                string sql = string.Empty;
                string condition = string.Empty;
                string critRecNo = string.Empty;

                string firstCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;
                string ViewYear = string.Empty;

                critRecNo = num_page == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         RowPerPage.StartRowNumber(num_page).ToString() + " AND " +
                                         RowPerPage.ToRowNumber(num_page).ToString();


                if (startDate != null && toDate != null)
                {
                    condition = (string.Format(
                              " (g.group_date BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd')) AND ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd()));
                }

                //if (Gcode != null)
                //{
                //    condition = condition + " FN.accept_off_code = " + Gcode + " AND ";
                //}

                if (Ccode != null)
                {
                    switch (type)
                    {
                        case 3:
                            condition = condition + " ELR.EXAM_OWNER like '" + CompCode + "' AND ";
                            condition = condition + " FN.exam_place_code = " + Ccode + " AND ";
                            break;
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            condition = condition + " FN.exam_place_code = " + Ccode + " AND ";
                            break;
                        default: condition = condition + "";
                            break;
                    }
               
                }

                if (Count)
                {
                    firstCon = " select count(*) CCount from ( ";
                    midCon = " ";
                    lastCon = " ) ";
                }
                else
                {
                    firstCon = " select * from ( ";
                    midCon = " , ROW_NUMBER() OVER (ORDER BY g.group_request_no) RUN_NO  ";
                    lastCon = "  ) A " + critRecNo;
                }
                var getViewYear = ctx.AG_IAS_CONFIG.SingleOrDefault(c => c.ID == "01" && c.GROUP_CODE == "RC001");

                if (getViewYear.ITEM_VALUE != "")
                {
                    DateTime AddYear = DateTime.Now.AddYears(-Convert.ToInt32(getViewYear.ITEM_VALUE));
                    ViewYear = string.Format(" AND d.RECEIPT_DATE >=  to_date('{0}','yyyymmdd')  ", AddYear.ToString_yyyyMMdd());
                }

                sql = firstCon + " SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  " +
                                " ,d.payment_date,TT.NAME ||' '|| FN.NAMES FIRST_NAME, FN.LASTNAME, " +
                                " g.group_request_no,g.group_date,d.PAYMENT_NO,FN.TESTING_NO, " +
                                " d.RECEIPT_DATE,d.AMOUNT,g.CREATED_DATE , " +
                                " d.RECEIVE_PATH " + midCon +
                                " " +
                                " from ag_ias_payment_g_t g,ag_ias_subpayment_h_t h,ag_ias_subpayment_d_t d, " +
                                " ag_petition_type_r p,AG_APPLICANT_T FN,VW_IAS_TITLE_NAME TT , AG_EXAM_LICENSE_R ELR " +
                                " where " + condition + " g.group_request_no = h.group_request_no " +
                                " and d.head_request_no = h.head_request_no " +
                                " and d.petition_type_code = p.petition_type_code " +
                                " and TT.ID = fn.pre_name_code " +
                                " and FN.ID_CARD_NO = d.id_card_no " +
                                " and fn.testing_no = d.TESTING_NO " + ViewYear +
                                " and d.petition_type_code ='01' and ELR.TESTING_no = FN.TESTING_NO  " + lastCon ;


                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentDetailByGroup", ex);
            }
            return res;
        }
        public DTO.ResponseService<DataSet>
        GetCountPaymentDetailByCriteria(DTO.UserProfile userProfile,
                            string paymentType,
                            string order,
                            DateTime? startDate, DateTime? toDate,
                            string idCard, string billNo, string ViewYear)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder whereDT = new StringBuilder();
                StringBuilder whereReceipt = new StringBuilder();
                string temp_App = string.Empty;
                string temp_Lic = string.Empty;
                string sql = string.Empty;
                string Subreceipt = string.Empty;
                string wherebillNO = string.Empty;
                string NopayApp = string.Empty;
                string NoPayLicense = string.Empty;
                string endPay = string.Empty;
                string NewpaymentType = string.Empty;
                if (startDate != null && toDate != null)
                {
                    sb.Append(string.Format(
                              " ( CREATED_DATE BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd'))  ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd()));
                }

                //บริษัท หรือ สมาคม
                if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue() ||
                    userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                      temp_Lic = " AND (g.UPLOAD_BY_SESSION like '" + userProfile.CompCode + "%'  or d.COMPANY_CODE like '" + userProfile.CompCode + "%' ) ";
                                  

                }
                if (idCard != "")
                {
                    //sb.Append(GetCriteria(" d.id_card_no like   '{0}%' AND ", idCard));
                    whereDT.Append(GetCriteria("AND id_card_no like   '{0}%'  ", idCard));
                }

                if (order != "")
                {
                    sb.Append(GetCriteria("AND group_request_no like   '{0}%'  ", order.Replace(" ", "")));
                }
                if (ViewYear != "")
                {
                    DateTime AddYear = DateTime.Now.AddYears(-Convert.ToInt32(ViewYear));
                    whereReceipt.Append(string.Format("(RECEIPT_DATE >=  to_date('{0}','yyyymmdd') or RECEIPT_DATE is null)  ", AddYear.ToString_yyyyMMdd()));
                    //sb.Append(string.Format("SubR.RECEIPT_DATE >=  to_date('{0}','yyyymmdd') AND ", AddYear.ToString_yyyyMMdd()));
                }
                  string condition = sb.ToString();
                  if (billNo != "")
                  {
                      whereReceipt.Append(string.Format("and receipt_no like  '{0}%'  ", billNo));
                      endPay = ") ";
                  }
                  else 
                  {
                      if (paymentType == "00")
                      {
                          NewpaymentType = "";
                      }
                      else
                      {
                          NewpaymentType = paymentType;
                      }
                      NopayApp = "  SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  ,d.payment_date,TT.NAME ||' '|| FN.NAMES ||' '|| FN.LASTNAME FIRST_NAME, "
                + "  FN.LASTNAME, g.group_request_no,g.group_date,('')receipt_no,d.PAYMENT_NO,To_date(null)RECEIPT_DATE,g.CREATED_DATE,('')RECEIVE_PATH, "
                + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,To_number(null) AMOUNT,d.ACCOUNTING,To_number(null)num   "
                + "  from (select * from ag_ias_payment_g_t where " + condition  + " ) g,ag_ias_subpayment_h_t h,  "
                + "  ag_petition_type_r p,VW_IAS_TITLE_NAME TT , "
                + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code Like '01%' and ACCOUNTING is null AND RECORD_STATUS <> 'X' " + whereDT + " ) d "

                + "  left join AG_APPLICANT_T FN on FN.ID_CARD_NO = d.id_card_no and fn.testing_no = d.TESTING_NO "
                + "  where g.group_request_no = h.group_request_no "
                + "  and d.head_request_no = h.head_request_no and d.petition_type_code = p.petition_type_code and TT.ID = fn.pre_name_code   " + temp_Lic 
                + "  union ";

                      NoPayLicense = "  SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  ,d.payment_date,FN.TITLE_NAME ||' '|| FN.NAMES ||' '|| FN.LASTNAME FIRST_NAME, "
                     + "  FN.LASTNAME, g.group_request_no,g.group_date,('')receipt_no,d.PAYMENT_NO,To_date(null)RECEIPT_DATE,g.CREATED_DATE,('')RECEIVE_PATH, "
                     + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,To_number(null) AMOUNT,d.ACCOUNTING,To_number(null)num   "
                     + "  from (select * from ag_ias_payment_g_t where " + condition  + " ) g,ag_ias_subpayment_h_t h,  "
                     + "  ag_petition_type_r p, "
                     + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code Like '" + NewpaymentType + "%' and ACCOUNTING is null AND RECORD_STATUS <> 'X' " + whereDT + " ) d "
                     + "  ,AG_IAS_LICENSE_D FN "
                     + "  where g.group_request_no = h.group_request_no "
                     + " and d.upload_group_no = fn.upload_group_no and d.seq_no = fn.seq_no "
                     + "  and d.head_request_no = h.head_request_no and d.petition_type_code = p.petition_type_code  " + temp_Lic
                     + "  union ";

                  }
              

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;
                if (paymentType == "00")
                {
                   
                    endPay = ") ";
    
                    sql = "SELECT Count(*) rowcount from( "
                     + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                     + "  LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                     + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
             + NopayApp
                     + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                     + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                     + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
                     + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                     + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                     + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                     + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                     + "  from (select * from ag_ias_payment_g_t where " + condition   + ") g, "
                     + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                     + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code = '01' and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + ") d  "
                     + "   where g.group_request_no = SubR.group_request_no  "
                     + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO  " + temp_Lic+ " )where num = 1 "
                     + " union "

                     + NoPayLicense
                           + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                           + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                           + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
                           + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                           + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                           + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                           + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                           + "  from (select * from ag_ias_payment_g_t where " + condition + ") g, "
                           + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                           + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code  in ('11','13','14','15','16','17','18') and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + ") d  "
                           + "   where g.group_request_no = SubR.group_request_no  "
                           + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO  " + temp_Lic + ")where num = 1 "
                     + "   ) "  + endPay ;
                }
                else if (paymentType == "01")
                {
                    endPay = ") ";
                    sql = "SELECT Count(*) rowcount from( "
                          + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                          + "  LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                          + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
                          + NopayApp
                          + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                          + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                          + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
                          + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                          + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                          + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                          + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                          + "  from (select * from ag_ias_payment_g_t where " + condition +  ") g, "
                          + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                          + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code = '01' and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + ") d  "
                          + "   where g.group_request_no = SubR.group_request_no  "
                          + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO " + temp_Lic + " )where num = 1 "
                          + "   ) "+ endPay;

                }
                else
                {
                    endPay = ") ";
                    sql = "SELECT Count(*) rowcount from( "
                           + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                           + "  LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                           + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
                           + NoPayLicense
                           + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                           + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                           + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num from( "
                           + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                           + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                           + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                           + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                           + "  from (select * from ag_ias_payment_g_t where " + condition +  ") g, "
                           + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                           + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code = '" + paymentType + "' and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + ") d  "
                           + "   where g.group_request_no = SubR.group_request_no  "
                           + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO " + temp_Lic + " )where num = 1 "
                           + "   ) " + endPay;

                }

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetCountPaymentDetailByCriteria", ex);
            }
            return res;

        }
        public DTO.ResponseService<DataSet>
            GetPaymentDetailByCriteria(DTO.UserProfile userProfile,
                                    string paymentType,
                                    string order,
                                    DateTime? startDate, DateTime? toDate,
                                    string idCard, string billNo,
                                    int pageNo, int recordPerPage, string ViewYear)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder whereDT = new StringBuilder();
                StringBuilder whereReceipt = new StringBuilder();
                StringBuilder whereGT = new StringBuilder();
                string sql = string.Empty;
                string critRecNo = string.Empty;
                string temp_App = string.Empty;
                string temp_Lic = string.Empty;
                //string whereGT = string.Empty;
                //string whereDT = string.Empty;
                string Subreceipt = string.Empty;
                string wherebillNO = string.Empty;
                string NopayApp = string.Empty;
                string NoPayLicense = string.Empty;
                string endPay = string.Empty;
                string NewpaymentType = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                if (ViewYear != "")
                {
                    DateTime AddYear = DateTime.Now.AddYears(-Convert.ToInt32(ViewYear));
                    // sb.Append(string.Format("Subr.RECEIPT_DATE >=  to_date('{0}','yyyymmdd') AND ", AddYear.ToString_yyyyMMdd()));
                    whereReceipt.Append(string.Format("(RECEIPT_DATE >=  to_date('{0}','yyyymmdd') or RECEIPT_DATE is null)  ", AddYear.ToString_yyyyMMdd()));
                }
                if (startDate != null && toDate != null)
                {

                    sb.Append(string.Format(
                              " (CREATED_DATE BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd'))  ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd()));
                }

                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;
                    //บริษัท หรือ สมาคม
                    if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue() ||
                        userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                    {
                        temp_Lic = " AND (g.UPLOAD_BY_SESSION like '" + userProfile.CompCode + "%'  or d.COMPANY_CODE like '" + userProfile.CompCode + "%' ) ";
                    }
                if (idCard != "")
                {
                    whereDT.Append(GetCriteria("AND id_card_no like   '{0}%'  ", idCard));
                }
                if (order != "")
                {
                    whereGT.Append(string.Format("AND group_request_no like   '{0}%'  ", order.Replace(" ", "")));
                }
                if (billNo != "")
                {
                    whereReceipt.Append(string.Format("AND receipt_no like  '{0}%'  ", billNo));
                    endPay = ") ";
                }
                else
                {
                    if (paymentType == "00")
                    {
                        NewpaymentType = "";
                    }
                    else
                    {
                        NewpaymentType = paymentType;
                    }
                    NopayApp = "  SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  ,d.payment_date,TT.NAME ||' '|| FN.NAMES ||' '|| FN.LASTNAME FIRST_NAME, "
                            + "  FN.LASTNAME, g.group_request_no,g.group_date,('')receipt_no,d.PAYMENT_NO,To_date(null)RECEIPT_DATE,g.CREATED_DATE,('')RECEIVE_PATH, "
                            + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,To_number(null) AMOUNT,d.ACCOUNTING,To_number(null)num,To_number(null)RUN_NO   "
                            + "  from (select * from ag_ias_payment_g_t where " + condition + whereGT +  " ) g,ag_ias_subpayment_h_t h,  "
                            + "  ag_petition_type_r p,VW_IAS_TITLE_NAME TT , "
                            + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code Like '01%' and ACCOUNTING is null AND RECORD_STATUS <> 'X' " + whereDT + " ) d "

                            + "  left join AG_APPLICANT_T FN on FN.ID_CARD_NO = d.id_card_no and fn.testing_no = d.TESTING_NO "
                            + "  where g.group_request_no = h.group_request_no "
                            + "  and d.head_request_no = h.head_request_no and d.petition_type_code = p.petition_type_code and TT.ID = fn.pre_name_code   " + temp_Lic
                            + "  union ";

                    NoPayLicense = "  SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  ,d.payment_date,FN.TITLE_NAME ||' '|| FN.NAMES ||' '|| FN.LASTNAME FIRST_NAME, "
                            + "  FN.LASTNAME, g.group_request_no,g.group_date,('')receipt_no,d.PAYMENT_NO,To_date(null)RECEIPT_DATE,g.CREATED_DATE,('')RECEIVE_PATH, "
                            + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,To_number(null) AMOUNT,d.ACCOUNTING,To_number(null)num,To_number(null)RUN_NO   "
                            + "  from (select * from ag_ias_payment_g_t where " + condition +  whereGT + " ) g,ag_ias_subpayment_h_t h,  "
                            + "  ag_petition_type_r p, "
                            + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code Like '" + NewpaymentType + "%' and ACCOUNTING is null AND RECORD_STATUS <> 'X' " + whereDT + " ) d "
                            + "  ,AG_IAS_LICENSE_D FN "
                            + "  where g.group_request_no = h.group_request_no "
                            + " and d.upload_group_no = fn.upload_group_no and d.seq_no = fn.seq_no "
                            + "  and d.head_request_no = h.head_request_no and d.petition_type_code = p.petition_type_code  " + temp_Lic
                            + "  union ";

                }

              
          
       
                if (paymentType == "00")
                {
                    
                 
                    sql = "SELECT * FROM ( "
                            + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                          + "  LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                          + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO from( "
                   + NopayApp
                          + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                          + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                          + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,To_number(null)RUN_NO from( "
                          + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                          + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                          + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                          + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                          + "  from (select * from ag_ias_payment_g_t where " + condition + whereGT +  ") g, "
                          + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                          + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code = '01' and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + " ) d  "
                          + "   where g.group_request_no = SubR.group_request_no  "
                          + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO " + temp_Lic + " )where num = 1 "

                          + " union "

                   + NoPayLicense
                           + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                        + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                        + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,To_number(null)RUN_NO from( "
                        + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                        + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                        + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                        + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                        + "  from (select * from ag_ias_payment_g_t where " + condition  + whereGT + ") g, "
                        + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                        + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code in ('11','13','14','15','16','17','18') and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + " ) d  "
                        + "   where g.group_request_no = SubR.group_request_no  "
                        + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO " + temp_Lic + " )where num = 1 "
                        + " )) A " + critRecNo;
                }
                else if (paymentType == "01")
                {
                    sql = "SELECT * FROM ( "

                          + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                          + "  LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                          + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO from( "
                          + NopayApp
                          + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                          + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                          + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,To_number(null)RUN_NO from( "
                          + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                          + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                          + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                          + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                          + "  from (select * from ag_ias_payment_g_t where " + condition + whereGT +  ") g, "
                          + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                          + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code = '01' and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + " ) d  "
                          + "   where g.group_request_no = SubR.group_request_no  "
                          + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO " + temp_Lic + " )where num = 1 "
                          + "   ))  A " + critRecNo;

                }
                else
                {
                    sql = "SELECT * FROM ( "

                        + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                        + "  LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                        + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO from( "
                        + NoPayLicense
                        + "  SELECT	head_request_no,id_card_no ,petition_type_name  ,payment_date,FIRST_NAME, "
                        + "  ('')LASTNAME, group_request_no,group_date,receipt_no,PAYMENT_NO,RECEIPT_DATE,CREATED_DATE,RECEIVE_PATH, "
                        + "  UPLOAD_BY_SESSION,RECORD_STATUS,AMOUNT,ACCOUNTING, num,To_number(null)RUN_NO from( "
                        + "  SELECT	d.head_request_no,d.id_card_no ,subr.petition_type_name  ,subr.payment_date,SubR.FULL_NAME FIRST_NAME, "
                        + "  ('')LASTNAME, Subr.group_request_no,g.group_date,SubR.receipt_no,SubR.PAYMENT_NO,SubR.RECEIPT_DATE,g.CREATED_DATE,SubR.RECEIVE_PATH, "
                        + "  g.UPLOAD_BY_SESSION,d.RECORD_STATUS,SubR.AMOUNT,subr.ACCOUNTING "
                        + "  ,ROW_NUMBER() OVER (PARTITION BY subr.head_request_no, subr.payment_no ORDER BY subr.receipt_no,subr.group_request_no) num  "
                        + "  from (select * from ag_ias_payment_g_t where " + condition  + whereGT + ") g, "
                        + "  (select * from AG_IAS_SUBPAYMENT_RECEIPT where " + whereReceipt + " ) SubR, "
                        + "  (SELECT * from ag_ias_subpayment_d_t where petition_type_code = '" + paymentType + "' and ACCOUNTING is not null AND RECORD_STATUS <> 'X' " + whereDT + " ) d  "
                        + "   where g.group_request_no = SubR.group_request_no  "
                        + "   and  d.PAYMENT_NO = SubR.PAYMENT_NO and d.HEAD_REQUEST_NO = SubR.HEAD_REQUEST_NO " + temp_Lic + " )where num = 1 "
                        + "   )) A " + critRecNo;


                }


                #region oldCode 19_09_13
                ////ใบสั่งจ่ายค่าสมัครสอบ
                //if (paymentType == "01")
                //{
                //    sb.Append(GetCriteria(" SPD.RECEIPT_NO = '{0}' AND ", billNo));

                //    if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                //    {
                //        sb.Append(GetCriteria(" SPD.ID_CARD_NO = '{0}' AND ", idCard));
                //    }
                //    else
                //        //บริษัท หรือ สมาคม
                //        if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue() ||
                //            userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                //        {
                //            sb.Append(GetCriteria(" AP.UPLOAD_BY_SESSION = '{0}' AND ", userProfile.CompCode));
                //        }

                //    string condition = sb.ToString();

                //    string crit = condition.Length > 4
                //                    ? " AND " + condition.Substring(0, condition.Length - 4)
                //                    : condition;

                //    #region SQL Statement

                //    sql = "SELECT *" +
                //          "FROM ( " +
                //          "         SELECT	'สมัครสอบ' PAYMENT_TYPE_NAME, SPD.ID_CARD_NO,        " +
                //          "                 TT.NAME ||' '|| AP.NAMES FIRST_NAME, AP.LASTNAME,   " +
                //          "                 SPD.PAYMENT_DATE, SPD.PAYMENT_NO, SPD.RECEIPT_NO,   " +
                //          "                 SPD.TESTING_NO, SPD.COMPANY_CODE, SPD.EXAM_PLACE_CODE," +
                //          "                 ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO, AP.APPLICANT_CODE ASC) RUN_NO  " +
                //          "         FROM    AG_PETITION_TYPE_R               APT,               " +
                //          "                 AG_IAS_SUBPAYMENT_D_T            SPD,               " +
                //          "                 AG_APPLICANT_T	                 AP,                " +
                //          "                 VW_IAS_TITLE_NAME		         TT                 " +
                //          "         WHERE   SPD.PETITION_TYPE_CODE = APT.PETITION_TYPE_CODE AND " +
                //          "                 SPD.APPLICANT_CODE = AP.APPLICANT_CODE     AND      " +
                //          "                 SPD.EXAM_PLACE_CODE = AP.EXAM_PLACE_CODE   AND      " +
                //          "                 SPD.TESTING_NO = AP.TESTING_NO AND                  " +
                //          "                 TT.ID = AP.PRE_NAME_CODE " + crit +
                //          ") A " + critRecNo;

                //    #endregion
                //}
                //else //ใบสั่งจ่ายอื่น ๆ
                //{
                //    if (paymentType == "0")
                //    {   //บุคคลธรรมดา
                //        if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                //        {
                //            sb.Append(GetCriteria(" LN.LICENSE_NO = '{0}' AND ", userProfile.LicenseNo));
                //        }
                //        //บริษัท หรือ สมาคม
                //        else
                //        {
                //            sb.Append(GetCriteria(" AP.UPLOAD_BY_SESSION = '{0}' AND ", userProfile.CompCode));
                //        }

                //        string condition = sb.ToString();

                //        string crit = condition.Length > 4
                //                        ? " AND " + condition.Substring(0, condition.Length - 4)
                //                        : condition;

                //        #region SQL Statement

                //        sql = "SELECT *" +
                //              "FROM ( " +
                //              "         SELECT	DISTINCT(SPD.ID_CARD_NO),APT.PETITION_TYPE_NAME,        " +
                //              "                 TT.NAME ||' '|| AP.NAMES FIRST_NAME, AP.LASTNAME,   " +
                //              "                 SPD.PAYMENT_DATE, SPD.PAYMENT_NO, SPD.RECEIPT_NO,   " +
                //              "                 SPD.TESTING_NO, SPD.COMPANY_CODE, SPD.EXAM_PLACE_CODE," +
                //              "                 SPD.RECEIPT_DATE, AP.UPLOAD_BY_SESSION,             " +
                //              "                 ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO, AP.APPLICANT_CODE ASC) RUN_NO  " +
                //              "         FROM    AG_PETITION_TYPE_R               APT,               " +
                //              "                 AG_IAS_SUBPAYMENT_D_T            SPD,               " +
                //              "                 AG_APPLICANT_T	                 AP,                " +
                //              "                 VW_IAS_TITLE_NAME		         TT                 " +
                //              "         WHERE   SPD.APPLICANT_CODE = AP.APPLICANT_CODE     AND      " +
                //              "                 SPD.ID_CARD_NO = AP.ID_CARD_NO             AND      " +
                //              "                 SPD.EXAM_PLACE_CODE = AP.EXAM_PLACE_CODE   AND      " +
                //              "                 SPD.TESTING_NO = AP.TESTING_NO AND                  " +
                //              "                 TT.ID = AP.PRE_NAME_CODE " + crit +
                //              ") A " + critRecNo;

                //        #endregion
                //    }
                //    else
                //    {
                //        //บุคคลธรรมดา
                //        if (userProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                //        {
                //            sb.Append(GetCriteria(" LN.LICENSE_NO = '{0}' AND ", userProfile.LicenseNo));
                //        }
                //        //บริษัท หรือ สมาคม
                //        else
                //        {
                //            sb.Append(GetCriteria(" AP.UPLOAD_BY_SESSION = '{0}' AND ", userProfile.CompCode));
                //        }

                //        string condition = sb.ToString();

                //        string crit = condition.Length > 4
                //                        ? " AND " + condition.Substring(0, condition.Length - 4)
                //                        : condition;

                //        #region SQL Statement

                //        sql = "SELECT *" +
                //              "FROM ( " +
                //              "         SELECT	APT.PETITION_TYPE_NAME, SPD.ID_CARD_NO,        " +
                //              "                 TT.NAME ||' '|| AP.NAMES FIRST_NAME, AP.LASTNAME,   " +
                //              "                 SPD.PAYMENT_DATE, SPD.PAYMENT_NO, SPD.RECEIPT_NO,   " +
                //              "                 SPD.TESTING_NO, SPD.COMPANY_CODE, SPD.EXAM_PLACE_CODE," +
                //              "                 AP.UPLOAD_BY_SESSION,                               " +
                //              "                 ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO, AP.APPLICANT_CODE ASC) RUN_NO  " +
                //              "         FROM    AG_PETITION_TYPE_R               APT,               " +
                //              "                 AG_IAS_SUBPAYMENT_D_T            SPD,               " +
                //              "                 AG_APPLICANT_T	                 AP,                " +
                //              "                 VW_IAS_TITLE_NAME		         TT                 " +
                //              "         WHERE   SPD.PETITION_TYPE_CODE = APT.PETITION_TYPE_CODE AND " +
                //              "                 SPD.APPLICANT_CODE = AP.APPLICANT_CODE     AND      " +
                //              "                 SPD.EXAM_PLACE_CODE = AP.EXAM_PLACE_CODE   AND      " +
                //              "                 SPD.TESTING_NO = AP.TESTING_NO AND                  " +
                //              "                 TT.ID = AP.PRE_NAME_CODE " + crit +
                //              ") A " + critRecNo;

                //        #endregion
                //    }


                //}
                #endregion
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentDetailByCriteria", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetDataPayment_BeforeSentToReport(string H_req_no, string IDcard, string PayNo)
        {
            string sql = string.Empty;

            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string crit = String.Format(" and d.head_request_no = '{0}' and d.ID_CARD_NO = '{1}' and d.payment_no = '{2}' ", H_req_no, IDcard, PayNo);

                //sql = "SELECT	d.head_request_no,d.id_card_no  ,p.petition_type_name  "
                //          + ",d.payment_date,FN.TITLE_NAME ||' '|| FN.NAMES || ' ' || FN.LASTNAME FirstName , "
                //          + " (select NN.NAME || ' ' || ipt.names || ' ' || ipt.lastname from AG_IAS_PERSONAL_T ipt,VW_IAS_TITLE_NAME NN "
                //          + "where subr.RECEIPT_BY_ID = ipt.ID and NN.ID = ipt.pre_name_code) LASTNAME, "
                //          + "(select ipt.img_sign from AG_IAS_PERSONAL_T ipt where subr.RECEIPT_BY_ID = ipt.ID ) img_sign, "
                //          + "g.group_request_no,g.group_date,SUBR.receipt_no,d.PAYMENT_NO,SUBR.RECEIPT_DATE "
                //          + " ,d.AMOUNT,g.CREATED_DATE, " //----> ipt.signature_img , add by milk
                //          + "ROW_NUMBER() OVER (ORDER BY g.group_request_no) RUN_NO   , g.CREATED_BY,d.LICENSE_TYPE_CODE,SUBR.RECEIPT_BY_ID "
                //          + "from ag_ias_payment_g_t g,ag_ias_subpayment_h_t h,ag_ias_subpayment_d_t d, "
                //          + "ag_petition_type_r p,AG_IAS_LICENSE_D FN ,AG_IAS_PERSONAL_T ipt,AG_IAS_SUBPAYMENT_RECEIPT SUBR " //,AG_IAS_PERSONAL_T ipt ---> add by milk
                //          + ",VW_IAS_TITLE_NAME NN " //milk
                //          + "where g.group_request_no = h.group_request_no "
                //          + "and d.head_request_no = h.head_request_no "
                //          + "and d.PAYMENT_NO = SUBR.PAYMENT_NO "
                //          + "and d.HEAD_REQUEST_NO = SUBR.HEAD_REQUEST_NO "
                //          + "and SUBR.RECEIPT_BY_ID = ipt.ID  "  //->>>> this line add by Milk
                //          + "and d.petition_type_code = p.petition_type_code "
                //          + "and FN.ID_CARD_NO = d.id_card_no and NN.ID = ipt.pre_name_code "
                //          + "and d.petition_type_code in ('01','11','13','14','15','16','17','18') " + crit
                //          + "union all "
                //          + "SELECT	d.head_request_no,d.id_card_no ,p.petition_type_name  "
                //          + ",d.payment_date,TT.NAME ||' '|| FN.NAMES || ' ' || FN.LASTNAME FirstName, "
                //           + " (select NN.NAME || ' ' || ipt.names || ' ' || ipt.lastname from AG_IAS_PERSONAL_T ipt,VW_IAS_TITLE_NAME NN "
                //          + "where subr.RECEIPT_BY_ID = ipt.ID and NN.ID = ipt.pre_name_code) LASTNAME, "
                //          + "(select ipt.img_sign from AG_IAS_PERSONAL_T ipt where subr.RECEIPT_BY_ID = ipt.ID ) img_sign, "
                //          + "g.group_request_no,g.group_date,SUBR.receipt_no,d.PAYMENT_NO,SUBR.RECEIPT_DATE,d.AMOUNT,g.CREATED_DATE,  " //----> ipt.signature_img , add by milk
                //          + "ROW_NUMBER() OVER (ORDER BY g.group_request_no) RUN_NO  , g.CREATED_BY,d.LICENSE_TYPE_CODE,SUBR.RECEIPT_BY_ID  "
                //          + "from ag_ias_payment_g_t g,ag_ias_subpayment_h_t h,ag_ias_subpayment_d_t d, "
                //          + "ag_petition_type_r p,AG_APPLICANT_T FN,VW_IAS_TITLE_NAME TT ,AG_IAS_PERSONAL_T ipt " //,AG_IAS_PERSONAL_T ipt ---> add by milk
                //          + ",VW_IAS_TITLE_NAME NN,AG_IAS_SUBPAYMENT_RECEIPT SUBR " //milk
                //          + "where g.group_request_no = h.group_request_no "
                //          + "and d.head_request_no = h.head_request_no "
                //          + "and d.PAYMENT_NO = SUBR.PAYMENT_NO "
                //          + "and d.HEAD_REQUEST_NO = SUBR.HEAD_REQUEST_NO "
                //          + "and SUBR.RECEIPT_BY_ID = ipt.ID  "  //->>>> this line add by Milk
                //          + "and d.petition_type_code = p.petition_type_code "
                //          + "and TT.ID = fn.pre_name_code "
                //          + "and FN.ID_CARD_NO = d.id_card_no and NN.ID = ipt.pre_name_code "
                //          + "and fn.testing_no = d.TESTING_NO "
                //          + "and d.petition_type_code in ('01','11','13','14','15','16','17','18') " + crit;
                sql = "SELECT SUBR.receipt_no,	subr.head_request_no,subr.id_card_no  ,subr.PETITION_TYPE_NAME  ,Subr.payment_date,DT.LICENSE_TYPE_CODE,GT.CREATED_BY, "
                    + "Subr.FULL_NAME FirstName ,  (select NN.NAME || ' ' || ipt.names || ' ' || ipt.lastname from AG_IAS_PERSONAL_T ipt,VW_IAS_TITLE_NAME NN "
                    + "where subr.RECEIPT_BY_ID = ipt.ID and NN.ID = ipt.pre_name_code) LASTNAME, (select ipt.img_sign from AG_IAS_PERSONAL_T ipt "
                    + "where subr.RECEIPT_BY_ID = ipt.ID ) img_sign, Subr.group_request_no,subr.PAYMENT_NO,SUBR.RECEIPT_DATE "
                    + ",subr.AMOUNT, ROW_NUMBER() OVER (ORDER BY subr.group_request_no) RUN_NO   "
                    + ",SUBR.RECEIPT_BY_ID from "
                    + "( select * from AG_IAS_SUBPAYMENT_RECEIPT where head_request_no = '" + H_req_no + "' and payment_no = '" + PayNo + "' "
                    + " and ID_CARD_NO = '" + IDcard + "') SUBR,AG_IAS_SUBPAYMENT_D_T DT,AG_IAS_PAYMENT_G_T GT  "
                    + "where SUBR.PAYMENT_NO = DT.PAYMENT_NO and SUBR.HEAD_REQUEST_NO = DT.HEAD_REQUEST_NO and SUBR.GROUP_REQUEST_NO = GT.GROUP_REQUEST_NO "
                    + "order by SUBR.receipt_no asc ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetDataPayment_BeforeSentToReport", ex);
            }
            return res;
        }
        public DTO.ResponseMessage<bool> PlusPrintDownloadCount(List<DTO.SubPaymentDetail> subPaymentDetail)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (DTO.SubPaymentDetail app in subPaymentDetail)
                {

                    var ent = base.ctx.AG_IAS_SUBPAYMENT_D_T
                                     .SingleOrDefault(delegate(AG_IAS_SUBPAYMENT_D_T ap)
                                     {
                                         return ap.ID_CARD_NO == app.ID_CARD_NO &&
                                                ap.TESTING_NO == app.TESTING_NO &&
                                                ap.COMPANY_CODE == app.COMPANY_CODE &&
                                                ap.EXAM_PLACE_CODE == app.EXAM_PLACE_CODE;
                                     });

                    if (ent != null)
                    {
                        if (app.Click == "Print")
                        {
                            decimal? time = new decimal();

                            if (ent.PRINT_TIMES == null)
                            {
                                ent.PRINT_TIMES = 1;
                            }
                            else
                            {
                                time = ent.PRINT_TIMES + 1;

                                ent.PRINT_TIMES = time;
                            }
                        }
                        else if (app.Click == "Download")
                        {
                            Int16? time = new Int16();

                            if (ent.DOWNLOAD_TIMES == null)
                            {
                                ent.DOWNLOAD_TIMES = 1;
                            }
                            else
                            {
                                time = Convert.ToInt16(ent.DOWNLOAD_TIMES + 1);

                                ent.DOWNLOAD_TIMES = time;
                            }
                        }

                        base.ctx.SaveChanges();
                    }

                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_PlusPrintDownloadCount", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> PrintDownloadCount(List<DTO.SubPaymentDetail> subPaymentDetail, string id_card, string createby)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (DTO.SubPaymentDetail app in subPaymentDetail)
                {

                    var ent = base.ctx.AG_IAS_SUBPAYMENT_RECEIPT.SingleOrDefault(x => x.RECEIPT_NO == app.RECEIPT_NO);

                    if (ent != null)
                    {

                        AG_IAS_RECEIPT_HISTORY history = new AG_IAS_RECEIPT_HISTORY();
                        if (app.Click == "Print")
                        {
                            decimal? time = new decimal();

                            if (ent.PRINT_TIMES == null)
                            {
                                ent.PRINT_TIMES = 1;
                            }
                            else
                            {
                                time = ent.PRINT_TIMES + 1;

                                ent.PRINT_TIMES = time;
                            }

                            history.RCV_EVENT = "P";
                        }
                        else if (app.Click == "Download")
                        {
                            Int16? time = new Int16();

                            if (ent.DOWNLOAD_TIMES == null)
                            {
                                ent.DOWNLOAD_TIMES = 1;
                            }
                            else
                            {
                                time = Convert.ToInt16(ent.DOWNLOAD_TIMES + 1);

                                ent.DOWNLOAD_TIMES = time;
                            }
                            history.RCV_EVENT = "L";
                        }
                        history.RECEIPT_NO = ent.RECEIPT_NO;
                        history.CREATED_BY = createby;
                        history.CREATED_DATE = DateTime.Now;
                        ctx.AG_IAS_RECEIPT_HISTORY.AddObject(history);
                        base.ctx.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                    }

                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_PrintDownloadCount", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> Zip_PrintDownloadCount(string[] rcvPath, string EventZip, string id_card, string createby)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                using (TransactionScope tc = new TransactionScope())
                {
                    foreach (string app in rcvPath)
                    {

                        var ent = base.ctx.AG_IAS_SUBPAYMENT_RECEIPT.SingleOrDefault(x => x.RECEIVE_PATH == app);
                        //.SingleOrDefault(delegate(AG_IAS_SUBPAYMENT_RECEIPT ap)
                        //{
                        //    return ap.RECEIVE_PATH == app;

                        //});
                        //.SingleOrDefault(x => x.RECEIVE_PATH == app).ToString();


                        if (ent != null)
                        {

                            AG_IAS_RECEIPT_HISTORY history = new AG_IAS_RECEIPT_HISTORY();
                            if (EventZip == "Print")
                            {
                                decimal? time = new decimal();

                                if (ent.PRINT_TIMES == null)
                                {
                                    ent.PRINT_TIMES = 1;
                                }
                                else
                                {
                                    time = ent.PRINT_TIMES + 1;
                                    ent.PRINT_TIMES = time;
                                }
                                history.RCV_EVENT = "P";
                            }
                            else if (EventZip == "Download")
                            {
                                Int16? time = new Int16();

                                if (ent.DOWNLOAD_TIMES == null)
                                {
                                    ent.DOWNLOAD_TIMES = 1;
                                }
                                else
                                {
                                    time = Convert.ToInt16(ent.DOWNLOAD_TIMES + 1);
                                    ent.DOWNLOAD_TIMES = time;
                                }
                                history.RCV_EVENT = "L";
                            }

                            history.RECEIPT_NO = ent.RECEIPT_NO;
                            history.CREATED_BY = createby;
                            history.ID_CARD_NO = id_card;
                            history.CREATED_DATE = DateTime.Now;
                            ctx.AG_IAS_RECEIPT_HISTORY.AddObject(history);
                        }

                    }

                    base.ctx.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                    tc.Complete();
                }
                // res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_Zip_PrintDownloadCount", ex);
            }

            return res;
        }


        public DTO.ResponseService<DataSet>
            GetDetailSubPayment(string groupReqNo, int pageNo, int recordPerPage, string CountRecord)
        {
            var res = new DTO.ResponseService<DataSet>();
            StringBuilder sb = new StringBuilder();

            string critRecNo = string.Empty;
            critRecNo = pageNo == 0
                            ? ""
                            : "WHERE A.RUN_NO BETWEEN " +
                                     pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                     pageNo.ToRowNumber(recordPerPage).ToString();
            string sql2 = string.Empty;
            try
            {
                if (CountRecord == "Y")
                {
                    sql2 = "select count(*) rowcount  from( "
                            + "select DISTINCT(d.ID_CARD_NO),a.names|| ' ' ||a.lastname FIRSTLASTNAME,p.petition_type_name,d.amount,d.record_status record_status "
                            + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,ag_applicant_t a "
                            + "where d.petition_type_code = p.petition_type_code "
                            + "and d.id_card_no = a.id_card_no "
                            + "and d.HEAD_REQUEST_NO = '" + groupReqNo + "' "
                            + "and a.TESTING_NO = d.testing_no "
                            + "and a.EXAM_PLACE_CODE = d.exam_place_code "
                            + "and a.APPLICANT_CODE = d.applicant_code "
                            + "union "
                           + "select DISTINCT(d.ID_CARD_NO),a.names|| ' ' ||a.lastname FIRSTLASTNAME,p.petition_type_name,d.amount,d.record_status record_status "
                           + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,AG_IAS_LICENSE_D a "
                           + "where d.petition_type_code = p.petition_type_code "
                           + "and d.id_card_no = a.id_card_no "
                           + "and d.SEQ_NO = a.SEQ_NO "
                           + "and d.UPLOAD_GROUP_NO = a.UPLOAD_GROUP_NO "
                           + "and d.HEAD_REQUEST_NO = '" + groupReqNo + "') ";
                }
                else
                {
                    sql2 = "SELECT * FROM( "
                        + "SELECT  ID_CARD_NO,FIRSTLASTNAME,petition_type_name, "
                        + "amount, record_status,ROW_NUMBER() OVER (ORDER BY ID_CARD_NO) RUN_NO   FROM( "
                        + "select DISTINCT(d.ID_CARD_NO),a.names|| ' ' ||a.lastname FIRSTLASTNAME,p.petition_type_name,d.amount,d.record_status record_status "
                        + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,ag_applicant_t a "
                        + "where d.petition_type_code = p.petition_type_code "
                        + "and d.id_card_no = a.id_card_no "
                        + "and d.HEAD_REQUEST_NO = '" + groupReqNo + "' "
                        + "and a.TESTING_NO = d.testing_no "
                        + "and a.EXAM_PLACE_CODE = d.exam_place_code "
                        + "and a.APPLICANT_CODE = d.applicant_code "
                        + "union "
                       + "select DISTINCT(d.ID_CARD_NO),a.names|| ' ' ||a.lastname FIRSTLASTNAME,p.petition_type_name,d.amount,d.record_status record_status "
                       + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,AG_IAS_LICENSE_D a "
                       + "where d.petition_type_code = p.petition_type_code "
                       + "and d.id_card_no = a.id_card_no "
                       + "and d.SEQ_NO = a.SEQ_NO "
                       + "and d.UPLOAD_GROUP_NO = a.UPLOAD_GROUP_NO "
                       + "and d.HEAD_REQUEST_NO = '" + groupReqNo + "' ))A " + critRecNo;
                }
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql2);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetDetailSubPayment", ex);
            }
            return res;
        }
        public DTO.ResponseService<string> ReSubmitBankTrans(DTO.ImportBankTransferRequest request)
        {

            DTO.ResponseService<string> res = new DTO.ResponseService<string>();

            try
            {
                IAS.DAL.Interfaces.IIASFinanceEntities ctxFin = DALFactory.GetFinanceContext();
                BillBiz biz = new BillBiz();

                request.UserOicId = OICUserIdHelper.PhaseOICId(request.UserOicId);
                APPS_CONFIG_INPUT userInfo = ctxFin.APPS_CONFIG_INPUT
                               .SingleOrDefault(s => s.USER_ID == request.UserOicId &&
                                                     s.MENU_CODE == "73050");

                if (userInfo == null)
                {
                    if (request.UserOicId.Equals("ar03"))
                    {
                        request.UserOicId = "52-2-034";
                    }
                    else
                    {
                        res.ErrorMsg = request.UserOicId + Resources.errorPaymentService_005;
                        return res;
                    }

                }



                //551004
                DTO.ResponseMessage<bool> response = biz.SubmitPaymentBankReTransaction(ref base.ctx, ref ctxFin, request);

                if (response.IsError)
                {
                    res.ErrorMsg = response.ErrorMsg;
                    return res;
                }

                int missStatus = (int)DTO.ImportPaymentStatus.MissingRefNo;
                IEnumerable<ImportBankTransferData> dataMissRefNos = request.ImportBankTransfers.Where(a => a.Status == missStatus);
                MailUpdatePaymentHelper.ConcreateEmail(base.ctx, dataMissRefNos);

                ctxFin.Dispose();

            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorBillBiz_007;
                LoggerFactory.CreateLog().Fatal("PaymentService_ReSubmitBankTrans", ex);
            }
            return res;


        }

        public DTO.ResponseService<string> SubmitBankTrans(DTO.ImportBankTransferRequest request)
        {
            LoggerFactory.CreateLog().LogInfo(String.Format("PaymentService.SubmitBankTrans batch:{0}", request.GroupId));
     
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();

            try
            {
                IAS.DAL.Interfaces.IIASFinanceEntities ctxFin = DALFactory.GetFinanceContext();
                BillBiz biz = new BillBiz();

                request.UserOicId = OICUserIdHelper.PhaseOICId(request.UserOicId);
                APPS_CONFIG_INPUT userInfo = ctxFin.APPS_CONFIG_INPUT
                               .SingleOrDefault(s => s.USER_ID == request.UserOicId &&
                                                     s.MENU_CODE == "73050");

                if (userInfo == null)
                {
                    if (request.UserOicId.Equals("ar03"))
                    {
                        request.UserOicId = "52-2-034";
                    }
                    else
                    {
                        res.ErrorMsg = request.UserOicId + Resources.errorPaymentService_005;
                        return res;
                    }
                }

                //551004
                DTO.ResponseMessage<bool> response = biz.SubmitPaymentBankUpload( base.ctx,  ctxFin, request);


                if (response.IsError)
                {
                    LoggerFactory.CreateLog().LogError(response.ErrorMsg);
                    res.ErrorMsg = response.ErrorMsg;
                    return res;
                }
                int missStatus = (int)DTO.ImportPaymentStatus.MissingRefNo;
                IEnumerable<ImportBankTransferData> dataMissRefNos = request.ImportBankTransfers.Where(a => a.Status == missStatus);
                if (dataMissRefNos != null && dataMissRefNos.Count() > 0)
                    MailUpdatePaymentHelper.ConcreateEmail(base.ctx, dataMissRefNos);

   

            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorBillBiz_007;
                LoggerFactory.CreateLog().Fatal("PaymentService_SubmitBankTrans", ex);
            }
            return res;


        }

        /// <summary>
        /// ดึงข้อมูลประวัติการสอบ ss
        /// </summary>
        /// <param name="idCard">รหัสบัตรประชาชน</param>
        /// <returns>ResponseService<List<DTO.ExamHistory></returns>
        private string GetExamHistoryByID(string idCard)
        {
            string res = string.Empty;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            try
            {
                #region Func
                Func<string, string> exresult = delegate(string r)
                {
                    if ((r != null) && (r != ""))
                    {
                        if (r.Equals("P"))
                        {
                            r = Resources.propLicenseService_P;
                        }
                        else if (r.Equals("F"))
                        {
                            r = Resources.propLicenseService_F;
                        }
                        else if (r.Equals("M"))
                        {
                            r = Resources.propLicenseService_M;
                        }
                        else if (r.Equals("B"))
                        {
                            r = Resources.propLicenseService_B;
                        }
                    }
                    else
                    {
                        r = Resources.propLicenseService_N;
                    }

                    return r;
                };
                #endregion

                #region New
                IQueryable<DTO.ExamHistory> result = (from AP in base.ctx.AG_APPLICANT_T
                                                      join LR in base.ctx.AG_EXAM_LICENSE_R on AP.TESTING_NO equals LR.TESTING_NO
                                                      join TR in base.ctx.AG_EXAM_TIME_R on LR.TEST_TIME_CODE equals TR.TEST_TIME_CODE
                                                      join PR in base.ctx.AG_EXAM_PLACE_R on AP.EXAM_PLACE_CODE equals PR.EXAM_PLACE_CODE
                                                      join LT in base.ctx.AG_IAS_LICENSE_TYPE_R on LR.LICENSE_TYPE_CODE equals LT.LICENSE_TYPE_CODE
                                                      where AP.TESTING_NO == LR.TESTING_NO &&
                                                      AP.EXAM_PLACE_CODE == LR.EXAM_PLACE_CODE &&
                                                      LR.TEST_TIME_CODE == TR.TEST_TIME_CODE &&
                                                      AP.EXAM_PLACE_CODE == PR.EXAM_PLACE_CODE &&
                                                      LR.LICENSE_TYPE_CODE == LT.LICENSE_TYPE_CODE &&
                                                      AP.ID_CARD_NO == idCard
                                                      select new DTO.ExamHistory
                                                      {
                                                          ID_CARD_NO = AP.ID_CARD_NO,
                                                          APPLICANT_CODE = AP.APPLICANT_CODE,
                                                          TESTING_NO = AP.TESTING_NO,
                                                          TESTING_DATE = LR.TESTING_DATE,
                                                          TEST_TIME = TR.TEST_TIME,
                                                          LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE,
                                                          LICENSE_TYPE_NAME = LT.LICENSE_TYPE_NAME,
                                                          EXAM_PLACE_NAME = PR.EXAM_PLACE_NAME,
                                                          EXAM_RESULT = AP.RESULT,
                                                          EXPIRE_DATE = AP.EXPIRE_DATE,
                                                      });



                string testingNo = result.Where(s => s.ID_CARD_NO == idCard).Select(x => x.TESTING_NO).FirstOrDefault();
                if ((testingNo != null) && (testingNo != ""))
                {
                    res = testingNo;
                }
                else
                {
                    res = "";
                }

                #endregion

            }
            catch (Exception ex)
            {
                res = "ไม่สามารถดึงข้อมูลประวัติการสอบ.";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetExamHistoryByID", ex);
            }

            sw.Stop();
            TimeSpan sp = sw.Elapsed;
            TimeSpan duration = sp.Duration();
            return res;

        }

        public DTO.ResponseMessage<bool> Insert12T(List<DTO.GenLicense> GenLicense)
        {
            var res = new DTO.ResponseMessage<bool>();

            List<DTO.Payment12TEntity> lsPaymeny = new List<Payment12TEntity>();
      
            try
            {
                #region Func
                Func<string, string> strValidate = delegate(string input)
                {
                    //string output = string.Empty;
                    if ((input == null) || (input == ""))
                    {
                        input = "";
                    }

                    return input;
                };

                #endregion

                #region Trans
                string oicSession = ConfigurationManager.AppSettings["OIC_SECTION"];
                string branch = ConfigurationManager.AppSettings["OIC_BRANCH_NO"];
                DateTime? expireDate = new DateTime();
                string getTestingNo = string.Empty;
                //----------------------------------
                string vcomp_license_no = string.Empty;
                string vcomp_license_type = string.Empty;
                string V_PRE_NAME_CODE = string.Empty;
                string V_NAMES = string.Empty;
                string V_LASTNAME = string.Empty;
                string v_revoke_upd_code = string.Empty;

                if (GenLicense.Count != 0)
                {
                    #region GetPaymeny List
                    GenLicense.ForEach(x =>
                    {
                        
                        if ((x.PAYMENT_NO == "") || (x.HEAD_REQUEST_NO == ""))
                        {
                            IQueryable<DTO.Payment12TEntity> rex = (from LH in base.ctx.AG_IAS_LICENSE_H
                                                                    join LD in base.ctx.AG_IAS_LICENSE_D on LH.UPLOAD_GROUP_NO equals LD.UPLOAD_GROUP_NO
                                                                    where LD.UPLOAD_GROUP_NO == x.UPLOAD_GROUP_NO && LD.SEQ_NO == x.SEQ_NO
                                                                    select new DTO.Payment12TEntity
                                                                    {
                                                                        IdCard = LD.ID_CARD_NO,
                                                                        petition_type_code = LH.PETITION_TYPE_CODE,
                                                                        SSeqNo = LD.SEQ_NO,
                                                                        upGroup = LD.UPLOAD_GROUP_NO,
                                                                        ReceiveDate = null,
                                                                        licenseT = LH.LICENSE_TYPE_CODE,
                                                                        ComCode = LH.COMP_CODE,
                                                                        requestNo = LD.REQUEST_NO,
                                                                        payment_no = "",
                                                                        receiptNo = "",
                                                                        area = LD.AREA_CODE.Substring(1, 2),
                                                                        selectLicense = LD.LICENSE_NO,
                                                                        LicenseExpireDate = LD.LICENSE_EXPIRE_DATE,
                                                                        oldComp = LD.OLD_COMP_CODE,
                                                                        ApproverUserId = x.USER_ID

                                                                    }).AsQueryable();

                            rex.ToList().ForEach(item =>
                            {
                                DTO.Payment12TEntity newEnt = new Payment12TEntity();
                                newEnt.SSeqNo = item.SSeqNo;
                                newEnt.upGroup = item.upGroup;
                                newEnt.ReceiveDate = item.ReceiveDate;
                                newEnt.licenseT = item.licenseT;
                                newEnt.IdCard = item.IdCard;
                                newEnt.ComCode = item.ComCode;
                                newEnt.requestNo = item.requestNo;
                                newEnt.payment_no = item.payment_no;
                                newEnt.receiptNo = item.receiptNo;
                                newEnt.area = item.area;
                                newEnt.selectLicense = item.selectLicense;
                                newEnt.LicenseExpireDate = item.LicenseExpireDate;
                                newEnt.oldComp = item.oldComp;
                                newEnt.ApproverUserId = item.ApproverUserId;

                                //Additem to ListPaymeny List
                                lsPaymeny.Add(item);
                            });

                        }
                        else
                        {
                            IQueryable<DTO.Payment12TEntity> rex = (from LD in base.ctx.AG_IAS_LICENSE_D
                                                                    join LH in base.ctx.AG_IAS_LICENSE_H on LD.UPLOAD_GROUP_NO equals LH.UPLOAD_GROUP_NO
                                                                    join SDT in base.ctx.AG_IAS_SUBPAYMENT_D_T on new { LD.UPLOAD_GROUP_NO, LD.SEQ_NO } equals new { SDT.UPLOAD_GROUP_NO, SDT.SEQ_NO }
                                                                    join RE in base.ctx.AG_IAS_SUBPAYMENT_RECEIPT on new { SDT.PAYMENT_NO, SDT.HEAD_REQUEST_NO } equals new { RE.PAYMENT_NO, RE.HEAD_REQUEST_NO }
                                                                    where LD.UPLOAD_GROUP_NO == x.UPLOAD_GROUP_NO &&
                                                                    LD.SEQ_NO == x.SEQ_NO &&
                                                                    SDT.HEAD_REQUEST_NO == x.HEAD_REQUEST_NO &&
                                                                    SDT.PAYMENT_NO == x.PAYMENT_NO &&
                                                                    RE.ACCOUNTING != "S"
                                                                    select new DTO.Payment12TEntity
                                                                    {
                                                                        IdCard = LD.ID_CARD_NO,
                                                                        petition_type_code = SDT.PETITION_TYPE_CODE,
                                                                        SSeqNo = LD.SEQ_NO,
                                                                        upGroup = LD.UPLOAD_GROUP_NO,
                                                                        ReceiveDate = SDT.RECEIPT_DATE,
                                                                        licenseT = LH.LICENSE_TYPE_CODE,
                                                                        ComCode = LH.COMP_CODE,
                                                                        requestNo = LD.REQUEST_NO,
                                                                        payment_no = SDT.PAYMENT_NO,
                                                                        receiptNo = SDT.RECEIPT_NO,
                                                                        area = LD.AREA_CODE.Substring(1, 2),
                                                                        selectLicense = LD.LICENSE_NO,
                                                                        LicenseExpireDate = LD.LICENSE_EXPIRE_DATE,
                                                                        oldComp = LD.OLD_COMP_CODE,
                                                                        ApproverUserId = x.USER_ID

                                                                    }).AsQueryable();

                            rex.ToList().ForEach(item =>
                            {
                                DTO.Payment12TEntity newEnt = new Payment12TEntity();
                                newEnt.SSeqNo = item.SSeqNo;
                                newEnt.upGroup = item.upGroup;
                                newEnt.ReceiveDate = item.ReceiveDate;
                                newEnt.licenseT = item.licenseT;
                                newEnt.IdCard = item.IdCard;
                                newEnt.ComCode = item.ComCode;
                                newEnt.requestNo = item.requestNo;
                                newEnt.payment_no = item.payment_no;
                                newEnt.receiptNo = item.receiptNo;
                                newEnt.area = item.area;
                                newEnt.selectLicense = item.selectLicense;
                                newEnt.LicenseExpireDate = item.LicenseExpireDate;
                                newEnt.oldComp = item.oldComp;
                                newEnt.ApproverUserId = item.ApproverUserId;

                                //Additem to ListPaymeny List
                                lsPaymeny.Add(item);

                            });


                        }

                    });
                    #endregion
                   
                    if (lsPaymeny.Count > 0)
                    {
                        lsPaymeny.ForEach(item =>
                            {
                                #region petition_type_code11
                                if (item.petition_type_code.Equals("11"))
                                {
                                    #region LicenseTypeCode != 11,12
                                    if ((item.licenseT != "11") || (item.licenseT != "12"))
                                    {
                                        //Create new culture
                                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                                        
                                        string LicenseNo = this.AG_LICENSE_RUNNING(DateTime.Now.Date, item.licenseT);
                                     
                                      
                                        expireDate = DateTime.Now.Date.AddMonths(12).AddDays(-1);
                                     
                                        string CheckWeekend = GetReceiptDate(Convert.ToString(expireDate));
                                        AG_LICENSE_T License = new AG_LICENSE_T();
                                        License.LICENSE_NO = LicenseNo.ToString();
                                        License.LICENSE_DATE = DateTime.Now.Date;
                                        License.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        License.LICENSE_TYPE_CODE = item.licenseT;
                                        License.NEW_LICENSE_NO = "";
                                        License.LICENSE_ACTOR = "";
                                        License.DATE_LICENSE_ACT = DateTime.Now.Date;
                                        License.REMARK = "";
                                        License.UNIT_LINK_RENEW = 0;
                                        License.START_UL_DATE = null;
                                        License.EXPIRE_UL_DATE = null;
                                        License.UNIT_LINK_STATUS = "";
                                        base.ctx.AG_LICENSE_T.AddObject(License);


                                        IQueryable<AG_IAS_LICENSE_D> selectlicenseD = from A in base.ctx.AG_IAS_LICENSE_D
                                                                           where A.SEQ_NO == item.SSeqNo &&
                                                                           A.UPLOAD_GROUP_NO == item.upGroup
                                                                           select A;
                                        foreach (AG_IAS_LICENSE_D entLicenseDs in selectlicenseD)
                                        {
                                            entLicenseDs.LICENSE_NO = LicenseNo;
                                            entLicenseDs.LICENSE_DATE = DateTime.Now.Date;
                                            entLicenseDs.LICENSE_EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                            entLicenseDs.OIC_APPROVED_BY = item.ApproverUserId;
                                            entLicenseDs.OIC_APPROVED_DATE = DateTime.Now;
                                        }
           
                                        IQueryable<AG_IAS_SUBPAYMENT_D_T> selectSubD = from A in base.ctx.AG_IAS_SUBPAYMENT_D_T
                                                                                      where A.SEQ_NO == item.SSeqNo &&
                                                                                      A.UPLOAD_GROUP_NO == item.upGroup
                                                                                      select A;
                                        foreach (AG_IAS_SUBPAYMENT_D_T entSubpaymentD in selectSubD)
                                        {
                                            entSubpaymentD.LICENSE_NO = LicenseNo;
                                        }

                                        AG_LICENSE_TYPE_R V_AGENT_TYPE = base.ctx.AG_LICENSE_TYPE_R.FirstOrDefault(v => v.LICENSE_TYPE_CODE == item.licenseT);
                                        if (V_AGENT_TYPE != null)
                                        {
                                            if (strValidate(V_AGENT_TYPE.AGENT_TYPE) == "A")
                                            {
                                                AG_AGENT_LICENSE_T insertAgentLicense = new DAL.AG_AGENT_LICENSE_T
                                                {
                                                    LICENSE_NO = LicenseNo,
                                                    ID_CARD_NO = item.IdCard,
                                                    INSURANCE_COMP_CODE = item.ComCode
                                                };
                                                base.ctx.AG_AGENT_LICENSE_T.AddObject(insertAgentLicense);
                                            }
                                            else if (strValidate(V_AGENT_TYPE.AGENT_TYPE) == "B")
                                            {

                                                AG_GET_JURISTIC_COMPCODE(strValidate(item.ComCode), item.licenseT, out vcomp_license_no, out vcomp_license_type);

                                                AG_AGENT_LICENSE_PERSON_T LicensePerson = new AG_AGENT_LICENSE_PERSON_T();
                                                LicensePerson.LICENSE_NO = LicenseNo;
                                                LicensePerson.ID_CARD_NO = item.IdCard;
                                                LicensePerson.COMP_LICENSE_NO = vcomp_license_no;
                                                LicensePerson.COMP_LICENSE_TYPE = vcomp_license_type;
                                                base.ctx.AG_AGENT_LICENSE_PERSON_T.AddObject(LicensePerson);

                                                if (vcomp_license_no != "")
                                                {
                                              
                                                    IQueryable<AG_PERSONAL_T> selectPerson = from A in base.ctx.AG_PERSONAL_T
                                                                                             where A.ID_CARD_NO == item.IdCard 
                                                                                                   select A;
                                                    foreach (AG_PERSONAL_T agPerson in selectPerson)
                                                    {
                                                        V_PRE_NAME_CODE = agPerson.PRE_NAME_CODE;
                                                        V_NAMES = agPerson.NAMES;
                                                        V_LASTNAME = agPerson.LASTNAME;
                                                    }
                                                    string tradeNo = string.Empty;
                                                    if (item.ComCode != "")
                                                    {
                                                        var trade = AG_GET_TRADE_NO(item.ComCode);
                                                        if (trade != null)
                                                        {
                                                            tradeNo = trade.ToString();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        tradeNo = "";
                                                    }

                                                    AG_PERSON_INCOMP_AGENT_T agpersonIncomp = new DAL.AG_PERSON_INCOMP_AGENT_T();
                                                    agpersonIncomp.COMPANY_CODE = item.ComCode;
                                                    agpersonIncomp.LICENSE_NO = LicenseNo;
                                                    agpersonIncomp.IN_DATE = DateTime.Now;
                                                    agpersonIncomp.COMP_LICENSE_NO = vcomp_license_no;
                                                    agpersonIncomp.COMP_LICENSE_TYPE = vcomp_license_type;
                                                    agpersonIncomp.STATUS = "A";
                                                    agpersonIncomp.LICENSE_TYPE_CODE = item.licenseT;
                                                    agpersonIncomp.REGISTER_COMP_NO = tradeNo;
                                                    agpersonIncomp.ID_CARD_NO = item.IdCard;
                                                    agpersonIncomp.PRE_NAME_CODE = Convert.ToInt16(V_PRE_NAME_CODE);
                                                    agpersonIncomp.NAME_LASTNAME = V_NAMES + " " + V_LASTNAME;
                                                    agpersonIncomp.PERSON_TYPE_CODE = "99";
                                                    base.ctx.AG_PERSON_INCOMP_AGENT_T.AddObject(agpersonIncomp);
                                                }
                                            }
                                        }

                                        else
                                        {
                                            res.ErrorMsg = Resources.errorPaymentService_008;
                                        }

                                        AG_LICENSE_RENEW_T renew = new AG_LICENSE_RENEW_T();
                                        renew.LICENSE_NO = LicenseNo;
                                        renew.RENEW_TIME = 0;
                                        renew.RECEIPT_DATE = Convert.ToDateTime(item.ReceiveDate);
                                        renew.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        renew.REQUEST_NO = item.requestNo;
                                        renew.PAYMENT_NO = item.payment_no;
                                        renew.LICENSE_ACTOR = "";
                                        renew.LICENSE_ACT_DATE = DateTime.Now.Date;
                                        renew.RECEIPT_NO = item.receiptNo;
                                        base.ctx.AG_LICENSE_RENEW_T.AddObject(renew);

                                        string sql = "select t.testing_no,t.result from ag_ias_license_d d "
                                                   + "join ag_applicant_t t on t.id_card_no = d.id_card_no "
                                                   + "where d.id_card_no = '" + item.IdCard + "' and t.result = 'P' and (t.RECORD_STATUS is null or t.RECORD_STATUS != 'X') ";
                                        OracleDB ora = new OracleDB();
                                        DataTable dt =  ora.GetDataSet(sql).Tables[0];

                                       for(int a = 0 ; a < dt.Rows.Count;a++)
                                       {
                                           DataRow dr = dt.Rows[a];
                                           string testno = dr["testing_no"].ToString();
                                            IQueryable<AG_APPLICANT_T> selectApp = from A in base.ctx.AG_APPLICANT_T
                                                                                   where A.ID_CARD_NO == item.IdCard
                                                && A.TESTING_NO == testno
                                                && A.RESULT == "P"
                                                && (A.RECORD_STATUS == null || A.RECORD_STATUS != "X" )
                                                                                   select A;
                                            foreach (AG_APPLICANT_T updateapp in selectApp)
                                            {
                                                updateapp.LICENSE = "Y";
                                            }
                                        }
                                 
                                    }
                                   
                                    #endregion
                                    #region LicenseTypeCode == 11,12
                                    if (item.licenseT == "11" || item.licenseT == "12")
                                    {
                                        AG_LICENSE_T entLicense = base.ctx.AG_LICENSE_T.FirstOrDefault(a => a.LICENSE_NO == item.selectLicense);
                                        if (entLicense != null)
                                        {
                                            entLicense.LICENSE_REINSURE = "Y";
                                        }
                                        AG_AGENT_LICENSE_REINSURE_T LicenseReinsure = new AG_AGENT_LICENSE_REINSURE_T();
                                        LicenseReinsure.LICENSE_NO = item.selectLicense;
                                        LicenseReinsure.LICENSE_DATE = DateTime.Now;
                                        LicenseReinsure.LICENSE_TYPE_CODE = item.licenseT;
                                        base.ctx.AG_AGENT_LICENSE_REINSURE_T.AddObject(LicenseReinsure);

                                    }
                                    #endregion
                                }
                                #endregion

                                #region petition_type_code13, 14
                                else if ((item.petition_type_code.Equals("13")) || (item.petition_type_code.Equals("14")))
                                {

                                    DateTime? V_DATE = null;
                                    Int16 maxTime = 0;
                                    Int16 maxTimePlus = 1;
                                    DateTime? ExDate;
                                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                                    string YearThai = string.Empty;
                                    string date = string.Empty;
                                    string Month = string.Empty;
                                    Int32 Year = 0;

                                    AG_LICENSE_T agLicenseCheck = base.ctx.AG_LICENSE_T.FirstOrDefault(l => l.LICENSE_NO == item.selectLicense);
                                    if (agLicenseCheck != null)
                                    {
                                        date = Convert.ToString(Convert.ToDateTime(agLicenseCheck.EXPIRE_DATE).Day);
                                        Month = Convert.ToString(Convert.ToDateTime(agLicenseCheck.EXPIRE_DATE).Month);
                                        Year = Convert.ToInt32(Convert.ToDateTime(agLicenseCheck.EXPIRE_DATE).Year);

                                        if (Year > 2500)
                                        {
                                            ExDate = Convert.ToDateTime(agLicenseCheck.EXPIRE_DATE);
                                        }
                                        else
                                        {
                                            YearThai = Convert.ToString((Convert.ToDateTime(agLicenseCheck.EXPIRE_DATE).Year) + 543);
                                            ExDate = Convert.ToDateTime(date + "/" + Month + "/" + YearThai);
                                        }
                                        if (item.petition_type_code.Equals("13"))
                                        {
                                            if (ExDate != null)
                                            {
                                                expireDate = Convert.ToDateTime(ExDate).AddMonths(12);
                                            }
                                        }
                                        else
                                        {
                                            if (ExDate != null)
                                            {
                                                expireDate = Convert.ToDateTime(ExDate).AddMonths(60);
                                            }
                                        }
                                        string CheckWeekend = GetReceiptDate(Convert.ToString(expireDate));
                                        agLicenseCheck.LICENSE_DATE = DateTime.Now.Date;
                                        agLicenseCheck.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        V_DATE = agLicenseCheck.DATE_LICENSE_ACT;


                                        //AG_IAS_LICENSE_D Null Check
                                        AG_IAS_LICENSE_D entLicenseD = base.ctx.AG_IAS_LICENSE_D.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo &&
                                            a.UPLOAD_GROUP_NO == item.upGroup);
                                        if (entLicenseD != null)
                                        {
                                            entLicenseD.LICENSE_DATE = DateTime.Now.Date;
                                            entLicenseD.LICENSE_EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                            entLicenseD.OIC_APPROVED_BY = item.ApproverUserId;
                                            entLicenseD.OIC_APPROVED_DATE = DateTime.Now;
                                        }

                                        //Get Max RenewTime
                                        AG_LICENSE_RENEW_T entAGLT = base.ctx.AG_LICENSE_RENEW_T.Where(no => no.LICENSE_NO == item.selectLicense).OrderByDescending(renew => renew.RENEW_TIME).FirstOrDefault();
                                        if (entAGLT != null)
                                        {
                                            maxTime = Convert.ToInt16(entAGLT.RENEW_TIME + maxTimePlus);
                                        }
                                        else if (entAGLT == null)
                                        {
                                            maxTime = 1;
                                        }

                                        AG_LICENSE_RENEW_T Insertrenew = new DAL.AG_LICENSE_RENEW_T();
                                        Insertrenew.LICENSE_NO = item.selectLicense;
                                        Insertrenew.RENEW_TIME = maxTime;
                                        Insertrenew.RENEW_DATE = DateTime.Now.Date;
                                        Insertrenew.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        Insertrenew.REQUEST_NO = item.requestNo;
                                        Insertrenew.PAYMENT_NO = item.payment_no;
                                        Insertrenew.LICENSE_ACT_DATE = V_DATE;
                                        Insertrenew.LICENSE_ACTOR = "";
                                        Insertrenew.CANCEL_REASON = "";
                                        Insertrenew.RECORD_STATUS = "";
                                        Insertrenew.RECEIPT_NO = item.receiptNo;
                                        Insertrenew.RECEIPT_DATE = Convert.ToDateTime(item.ReceiveDate);
                                        Insertrenew.ELICENSING_FLAG = "";
                                        Insertrenew.PROVINCE_CODE = item.area;
                                        base.ctx.AG_LICENSE_RENEW_T.AddObject(Insertrenew);
                                    }
                                }
                                #endregion

                                #region petition_type_code15
                                else if (item.petition_type_code.Equals("15"))
                                {
                                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                                    string LicenseNo = this.AG_LICENSE_RUNNING(DateTime.Now.Date, item.licenseT);

                                    expireDate = DateTime.Now.Date.AddMonths(12).AddDays(-1);

                                    string CheckWeekend = GetReceiptDate(Convert.ToString(expireDate));
                                    AG_LICENSE_T License = new AG_LICENSE_T();
                                    License.LICENSE_NO = LicenseNo.ToString();
                                    License.LICENSE_DATE = DateTime.Now.Date;
                                    License.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                    License.LICENSE_TYPE_CODE = item.licenseT;
                                    License.NEW_LICENSE_NO = "";
                                    License.LICENSE_ACTOR = "";
                                    License.DATE_LICENSE_ACT = DateTime.Now.Date;
                                    License.REMARK = "";
                                    License.UNIT_LINK_RENEW = 0;
                                    License.START_UL_DATE = null;
                                    License.EXPIRE_UL_DATE = null;
                                    License.UNIT_LINK_STATUS = "";
                                    base.ctx.AG_LICENSE_T.AddObject(License);

                                    //AG_IAS_LICENSE_D Null Check
                                    AG_IAS_LICENSE_D entLicenseD = base.ctx.AG_IAS_LICENSE_D.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo &&
                                        a.UPLOAD_GROUP_NO == item.upGroup);
                                    if (entLicenseD != null)
                                    {
                                        entLicenseD.LICENSE_NO = LicenseNo;
                                        entLicenseD.LICENSE_DATE = DateTime.Now.Date;
                                        entLicenseD.LICENSE_EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        entLicenseD.OIC_APPROVED_BY = item.ApproverUserId;
                                        entLicenseD.OIC_APPROVED_DATE = DateTime.Now;
                                    }

                                    //AG_IAS_SUBPAYMENT_D_T Null Check
                                    AG_IAS_SUBPAYMENT_D_T entSubpaymentD = base.ctx.AG_IAS_SUBPAYMENT_D_T.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo
                                        && a.UPLOAD_GROUP_NO == item.upGroup);

                                    if (entSubpaymentD != null)
                                    {
                                        entSubpaymentD.LICENSE_NO = LicenseNo;
                                        entSubpaymentD.OLD_LICENSE_NO = item.selectLicense;
                                    }

                                    //Insert AGENT_TYPE into AG_LICENSE_TYPE_R
                                    AG_LICENSE_TYPE_R V_AGENT_TYPE = base.ctx.AG_LICENSE_TYPE_R.FirstOrDefault(v => v.LICENSE_TYPE_CODE == item.licenseT);
                                    if (V_AGENT_TYPE != null)
                                    {
                                        if (V_AGENT_TYPE.AGENT_TYPE.ToString() == "A")
                                        {
                                            AG_AGENT_LICENSE_T insertAgentLicense = new DAL.AG_AGENT_LICENSE_T();
                                            insertAgentLicense.LICENSE_NO = LicenseNo;
                                            insertAgentLicense.ID_CARD_NO = item.IdCard;
                                            insertAgentLicense.INSURANCE_COMP_CODE = item.ComCode;
                                            base.ctx.AG_AGENT_LICENSE_T.AddObject(insertAgentLicense);

                                        }
                                        else if (V_AGENT_TYPE.AGENT_TYPE.ToString() == "B")
                                        {
                                            AG_GET_JURISTIC_COMPCODE(strValidate(item.ComCode), item.licenseT, out vcomp_license_no, out vcomp_license_type);

                                            AG_AGENT_LICENSE_PERSON_T LicensePerson = new AG_AGENT_LICENSE_PERSON_T();
                                            LicensePerson.LICENSE_NO = LicenseNo;
                                            LicensePerson.ID_CARD_NO = item.IdCard;
                                            LicensePerson.COMP_LICENSE_NO = vcomp_license_no;
                                            LicensePerson.COMP_LICENSE_TYPE = vcomp_license_type;
                                            base.ctx.AG_AGENT_LICENSE_PERSON_T.AddObject(LicensePerson);

                                            if ((vcomp_license_no != "") && (vcomp_license_no != null))
                                            {
                                                AG_PERSONAL_T agPerson = base.ctx.AG_PERSONAL_T.FirstOrDefault(a => a.ID_CARD_NO == item.IdCard);
                                                if (agPerson != null)
                                                {
                                                    V_PRE_NAME_CODE = agPerson.PRE_NAME_CODE;
                                                    V_NAMES = agPerson.NAMES;
                                                    V_LASTNAME = agPerson.LASTNAME;
                                                }

                                                string tradeNo = string.Empty;
                                                if ((item.ComCode != "") && (item.ComCode != null))
                                                {
                                                    string trade = AG_GET_TRADE_NO(item.ComCode);
                                                    if ((trade != "") && (trade != null))
                                                    {
                                                        tradeNo = trade.ToString();
                                                    }
                                                }
                                                else
                                                {
                                                    tradeNo = "";
                                                }

                                                AG_PERSON_INCOMP_AGENT_T agpersonIncomp = new AG_PERSON_INCOMP_AGENT_T();
                                                agpersonIncomp.COMPANY_CODE = item.ComCode;
                                                agpersonIncomp.LICENSE_NO = LicenseNo;
                                                agpersonIncomp.IN_DATE = DateTime.Now;
                                                agpersonIncomp.COMP_LICENSE_NO = vcomp_license_no;
                                                agpersonIncomp.COMP_LICENSE_TYPE = vcomp_license_type;
                                                agpersonIncomp.STATUS = "A";
                                                agpersonIncomp.LICENSE_TYPE_CODE = item.licenseT;
                                                agpersonIncomp.REGISTER_COMP_NO = tradeNo;
                                                agpersonIncomp.ID_CARD_NO = item.IdCard;
                                                agpersonIncomp.PRE_NAME_CODE = Convert.ToInt16(V_PRE_NAME_CODE);
                                                agpersonIncomp.NAME_LASTNAME = V_NAMES + " " + V_LASTNAME;
                                                agpersonIncomp.PERSON_TYPE_CODE = "99";
                                                base.ctx.AG_PERSON_INCOMP_AGENT_T.AddObject(agpersonIncomp);

                                            }
                                        }
                                    }

                                    else
                                    {
                                        res.ErrorMsg = Resources.errorPaymentService_008;
                                    }

                                    AG_LICENSE_RENEW_T renew = new AG_LICENSE_RENEW_T();
                                    renew.LICENSE_NO = LicenseNo;
                                    renew.RENEW_TIME = 0;
                                    renew.RECEIPT_DATE = Convert.ToDateTime(item.ReceiveDate);
                                    renew.EXPIRE_DATE = expireDate;
                                    renew.REQUEST_NO = item.requestNo;
                                    renew.PAYMENT_NO = item.payment_no;
                                    renew.LICENSE_ACTOR = "";
                                    renew.LICENSE_ACT_DATE = DateTime.Now.Date;
                                    renew.RECEIPT_NO = item.receiptNo;
                                    base.ctx.AG_LICENSE_RENEW_T.AddObject(renew);

                                    //AG_LICENSE_T Null Check
                                    AG_LICENSE_T revoke = base.ctx.AG_LICENSE_T.FirstOrDefault(r => r.REVOKE_TYPE_CODE == item.selectLicense);
                                    if (revoke != null)
                                    {
                                        if (revoke.REVOKE_TYPE_CODE == "F")
                                        {
                                            v_revoke_upd_code = "K";
                                        }
                                        else if (revoke.REVOKE_TYPE_CODE == "G")
                                        {
                                            v_revoke_upd_code = "L";
                                        }
                                        else
                                        {
                                            v_revoke_upd_code = "C";
                                        }
                                    }

                                    //AG_LICENSE_T Null Check
                                    AG_LICENSE_T updateAgLicense = base.ctx.AG_LICENSE_T.FirstOrDefault(a => a.LICENSE_NO == item.selectLicense);
                                    if (updateAgLicense != null)
                                    {
                                        updateAgLicense.NEW_LICENSE_NO = LicenseNo;
                                        updateAgLicense.REVOKE_LICENSE_DATE = DateTime.Now.Date;
                                        updateAgLicense.REVOKE_TYPE_CODE = v_revoke_upd_code;
                                    }

                                    //AG_PERSON_INCOMP_AGENT_T Null Check
                                    AG_PERSON_INCOMP_AGENT_T updateAgPersonIncomp = base.ctx.AG_PERSON_INCOMP_AGENT_T.FirstOrDefault(I => I.COMPANY_CODE == item.ComCode
                                        && I.LICENSE_NO == item.selectLicense && I.STATUS == "A");
                                    if (updateAgPersonIncomp != null)
                                    {
                                        updateAgPersonIncomp.OUT_DATE = DateTime.Now;
                                        updateAgPersonIncomp.STATUS = "C";
                                    }
                                }
                                #endregion

                                #region petition_type_code16
                                else if (item.petition_type_code.Equals("16"))
                                {
                                    AG_IAS_LICENSE_D entLicenseD = base.ctx.AG_IAS_LICENSE_D.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo &&
                                    a.UPLOAD_GROUP_NO == item.upGroup);
                                    if (entLicenseD != null)
                                    {
                                        if (entLicenseD.FEES == 0)
                                        {
                                            AG_PERSONAL_T entPerson = base.ctx.AG_PERSONAL_T.FirstOrDefault(a => a.ID_CARD_NO == item.IdCard);
                                            if (entPerson != null)
                                            {

                                                entPerson.REMARK = "เปลี่ยนชื่อจาก " + entPerson.NAMES + " " + entPerson.LASTNAME + " " + string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                                                entPerson.NAMES = entLicenseD.NAMES;
                                                entPerson.LASTNAME = entLicenseD.LASTNAME;

                                            }

                                            AG_IAS_PERSONAL_T entIASPerson = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(a => a.ID_CARD_NO == item.IdCard);
                                            if (entIASPerson != null)
                                            {
                                                entIASPerson.NAMES = entLicenseD.NAMES;
                                                entIASPerson.LASTNAME = entLicenseD.LASTNAME;
                                            }

                                            //Update to AG_IAS_REGISTRATION_T > if (!= null){ Update Entity }
                                            AG_IAS_REGISTRATION_T regisT = base.ctx.AG_IAS_REGISTRATION_T.FirstOrDefault(idc => idc.ID_CARD_NO == item.IdCard);
                                            if (regisT != null)
                                            {
                                                regisT.NAMES = entLicenseD.NAMES;
                                                regisT.LASTNAME = entLicenseD.LASTNAME;
                                            }
                                        }
                                        entLicenseD.OIC_APPROVED_BY = item.ApproverUserId;
                                        entLicenseD.OIC_APPROVED_DATE = DateTime.Now;
                                    }
                                }
                                #endregion

                                #region petition_type_code17
                                else if (item.petition_type_code.Equals("17"))
                                {
                                    Int16 V_max3 = 0;
                                    Int16 V_Plus = 1;
                                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                                    //var LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx, Convert.ToDateTime(item.ReceiveDate), item.licenseT);
                                    string LicenseNo = this.AG_LICENSE_RUNNING(DateTime.Now.Date, item.licenseT);


                                    expireDate = Convert.ToDateTime(DateTime.Now.Date.ToString()).AddMonths(12).AddDays(-1);

                                    string CheckWeekend = GetReceiptDate(Convert.ToString(expireDate));
                                    AG_LICENSE_T License = new AG_LICENSE_T();
                                    License.LICENSE_NO = LicenseNo.ToString();
                                    License.LICENSE_DATE = DateTime.Now.Date;
                                    License.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                    License.LICENSE_TYPE_CODE = item.licenseT;
                                    License.NEW_LICENSE_NO = "";
                                    License.LICENSE_ACTOR = "";
                                    License.DATE_LICENSE_ACT = Convert.ToDateTime(item.ReceiveDate);
                                    License.REMARK = "";
                                    License.UNIT_LINK_RENEW = 0;
                                    License.START_UL_DATE = null;
                                    License.EXPIRE_UL_DATE = null;
                                    License.UNIT_LINK_STATUS = "";
                                    base.ctx.AG_LICENSE_T.AddObject(License);

                                    //AG_IAS_LICENSE_D Null Check
                                    AG_IAS_LICENSE_D entLicenseD = base.ctx.AG_IAS_LICENSE_D.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo &&
                                        a.UPLOAD_GROUP_NO == item.upGroup);
                                    if (entLicenseD != null)
                                    {
                                        entLicenseD.LICENSE_NO = LicenseNo;
                                        entLicenseD.LICENSE_DATE = DateTime.Now.Date;
                                        entLicenseD.LICENSE_EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        entLicenseD.OIC_APPROVED_BY = item.ApproverUserId;
                                        entLicenseD.OIC_APPROVED_DATE = DateTime.Now;
                                    }

                                    //AG_IAS_SUBPAYMENT_D_T Cull Check
                                    AG_IAS_SUBPAYMENT_D_T entSubpaymentD = base.ctx.AG_IAS_SUBPAYMENT_D_T.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo
                                        && a.UPLOAD_GROUP_NO == item.upGroup);
                                    if (entSubpaymentD != null)
                                    {
                                        entSubpaymentD.OLD_LICENSE_NO = item.selectLicense;
                                        entSubpaymentD.LICENSE_NO = LicenseNo;
                                    }

                                    //Get Max Move
                                    AG_HIS_MOVE_COMP_AGENT_T maxMove = base.ctx.AG_HIS_MOVE_COMP_AGENT_T.Where(no => no.LICENSE_NO == item.selectLicense &&
                                        no.ID_CARD_NO == item.IdCard).OrderByDescending(m => m.MOVE_TIME).FirstOrDefault();
                                    if (maxMove != null)
                                    {
                                        V_max3 = Convert.ToInt16(maxMove.MOVE_TIME + V_Plus);
                                    }
                                    else if (maxMove == null)
                                    {
                                        V_max3 = 1;
                                    }

                                    //--*** 0. บันทึกข้อมูลประวัติการย้ายบริษัทตัวแทน AG_HIS_MOVE_COMP_AGENT_T
                                    AG_HIS_MOVE_COMP_AGENT_T insertHisMove = new AG_HIS_MOVE_COMP_AGENT_T();
                                    insertHisMove.LICENSE_NO = item.selectLicense;
                                    insertHisMove.ID_CARD_NO = item.IdCard;
                                    insertHisMove.MOVE_TIME = V_max3;
                                    insertHisMove.MOVE_DATE = DateTime.Now.Date;
                                    insertHisMove.COMP_MOVE_OUT_ID = item.oldComp;
                                    insertHisMove.COMP_MOVE_IN_ID = item.ComCode;
                                    insertHisMove.NEW_LICENSE_NO = LicenseNo;
                                    insertHisMove.REQUEST_NO = item.requestNo;
                                    insertHisMove.PAYMENT_NO = item.payment_no;
                                    insertHisMove.CANCEL_REASON = "";
                                    insertHisMove.RECORD_STATUS = "";
                                    insertHisMove.MOVE_FLAG = "Y";
                                    base.ctx.AG_HIS_MOVE_COMP_AGENT_T.AddObject(insertHisMove);

                                    //--*** 1. บันทึกข้อมูลใบอนุญาตใบหม่ ลง Table AG_AGENT_LICENSE_T
                                    AG_AGENT_LICENSE_T insertAgentL = new AG_AGENT_LICENSE_T();
                                    insertAgentL.LICENSE_NO = LicenseNo;
                                    insertAgentL.ID_CARD_NO = item.IdCard;
                                    insertAgentL.INSURANCE_COMP_CODE = item.ComCode;
                                    base.ctx.AG_AGENT_LICENSE_T.AddObject(insertAgentL);

                                    // --*** 2. บันทึกข้อมูลใบอนุญาตใบหม่ ลง Table AG_LICENSE_RENEW_T
                                    //AG_LICENSE_T Null Check
                                    AG_LICENSE_T getDate = base.ctx.AG_LICENSE_T.FirstOrDefault(a => a.LICENSE_NO == item.selectLicense);
                                    if (getDate != null)
                                    {
                                        AG_LICENSE_RENEW_T insertRenew = new AG_LICENSE_RENEW_T();
                                        insertRenew.LICENSE_NO = LicenseNo;
                                        insertRenew.RENEW_TIME = 0;
                                        insertRenew.RENEW_DATE = getDate.LICENSE_DATE;
                                        insertRenew.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        insertRenew.REQUEST_NO = item.requestNo;
                                        insertRenew.PAYMENT_NO = item.payment_no;
                                        insertRenew.LICENSE_ACT_DATE = getDate.LICENSE_DATE;
                                        insertRenew.RECEIPT_NO = item.receiptNo;
                                        base.ctx.AG_LICENSE_RENEW_T.AddObject(insertRenew);

                                        // -----------------------UPDATE ใบอนุญาต ฉบับเดิม------------
                                        //AG_LICENSE_T Null Check
                                        //AG_LICENSE_T revoke = base.ctx.AG_LICENSE_T.FirstOrDefault(r => r.REVOKE_TYPE_CODE == item.selectLicense);
                                        //if (revoke != null)
                                        //{
                                        //    if (revoke.REVOKE_TYPE_CODE == "F")
                                        //    {
                                        //        v_revoke_upd_code = "N";
                                        //    }
                                        //    else if (revoke.REVOKE_TYPE_CODE == "G")
                                        //    {
                                        //        v_revoke_upd_code = "M";
                                        //    }
                                        //    else
                                        //    {
                                        //        v_revoke_upd_code = "E";
                                        //    }
                                        //}

                                        //AG_LICENSE_T Null Check
                                        AG_LICENSE_T updateAgLicense = base.ctx.AG_LICENSE_T.FirstOrDefault(a => a.LICENSE_NO == item.selectLicense);
                                        if (updateAgLicense != null)
                                        {
                                            updateAgLicense.NEW_LICENSE_NO = LicenseNo;
                                            updateAgLicense.REVOKE_LICENSE_DATE = DateTime.Now.Date;
                                            updateAgLicense.REVOKE_TYPE_CODE = "E";
                                        }
                                    }
                                    else
                                    {
                                        res.ResultMessage = false;
                                        res.ErrorMsg = Resources.errorPaymentService_009;
                                    }

                                }
                                #endregion

                                #region petition_type_code 18
                                else if (item.petition_type_code.Equals("18"))
                                {
                                    Int16 V_max4 = 0;
                                    Int16 V_maxPlus = 1;
                                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                                    //var LicenseNo = GenLicenseNumber.AG_LICENSE_RUNNING(ctx, Convert.ToDateTime(item.ReceiveDate), item.licenseT);
                                    string LicenseNo = this.AG_LICENSE_RUNNING(DateTime.Now.Date, item.licenseT);


                                    expireDate = DateTime.Now.Date.AddMonths(12).AddDays(-1);

                                    string CheckWeekend = GetReceiptDate(Convert.ToString(expireDate));
                                    AG_LICENSE_T License = new AG_LICENSE_T();
                                    License.LICENSE_NO = LicenseNo.ToString();
                                    License.LICENSE_DATE = DateTime.Now.Date;
                                    License.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                    License.LICENSE_TYPE_CODE = item.licenseT;
                                    License.NEW_LICENSE_NO = "";
                                    License.LICENSE_ACTOR = "";
                                    License.DATE_LICENSE_ACT = DateTime.Now.Date;
                                    License.REMARK = "";
                                    License.UNIT_LINK_RENEW = 0;
                                    License.START_UL_DATE = null;
                                    License.EXPIRE_UL_DATE = null;
                                    License.UNIT_LINK_STATUS = "";
                                    base.ctx.AG_LICENSE_T.AddObject(License);

                                    //AG_IAS_LICENSE_D Null Check
                                    AG_IAS_LICENSE_D entLicenseD = base.ctx.AG_IAS_LICENSE_D.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo &&
                                        a.UPLOAD_GROUP_NO == item.upGroup);
                                    if (entLicenseD != null)
                                    {
                                        entLicenseD.LICENSE_NO = LicenseNo;
                                        entLicenseD.LICENSE_DATE = DateTime.Now.Date;
                                        entLicenseD.LICENSE_EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        entLicenseD.OIC_APPROVED_BY = item.ApproverUserId;
                                        entLicenseD.OIC_APPROVED_DATE = DateTime.Now;
                                    }


                                    //AG_IAS_SUBPAYMENT_D_T Null Check
                                    AG_IAS_SUBPAYMENT_D_T entSubpaymentD = base.ctx.AG_IAS_SUBPAYMENT_D_T.OrderBy(w => w.UPLOAD_GROUP_NO).FirstOrDefault(a => a.SEQ_NO == item.SSeqNo
                                        && a.UPLOAD_GROUP_NO == item.upGroup);
                                    if (entSubpaymentD != null)
                                    {
                                        entSubpaymentD.OLD_LICENSE_NO = item.selectLicense == null ? " " : item.selectLicense;
                                        entSubpaymentD.LICENSE_NO = LicenseNo;
                                    }

                                    //Get Max Move
                                    AG_HIS_MOVE_COMP_AGENT_T maxHisMove = base.ctx.AG_HIS_MOVE_COMP_AGENT_T.Where(no => no.LICENSE_NO == item.selectLicense &&
                                        no.ID_CARD_NO == item.IdCard).OrderByDescending(mm => mm.MOVE_TIME).FirstOrDefault();
                                    if (maxHisMove != null)
                                    {
                                        V_max4 = Convert.ToInt16(maxHisMove.MOVE_TIME + V_maxPlus);
                                    }
                                    else if (maxHisMove == null)
                                    {
                                        V_max4 = 1;
                                    }

                                    //--*** 0. บันทึกข้อมูลประวัติการย้ายบริษัทตัวแทน AG_HIS_MOVE_COMP_AGENT_T
                                    AG_HIS_MOVE_COMP_AGENT_T insertHisMove = new AG_HIS_MOVE_COMP_AGENT_T();
                                    insertHisMove.LICENSE_NO = item.selectLicense == null ? " " : item.selectLicense;
                                    insertHisMove.ID_CARD_NO = item.IdCard;
                                    insertHisMove.MOVE_TIME = V_max4;
                                    insertHisMove.MOVE_DATE = DateTime.Now.Date;
                                    insertHisMove.COMP_MOVE_OUT_ID = item.oldComp;
                                    insertHisMove.COMP_MOVE_IN_ID = item.ComCode;
                                    insertHisMove.NEW_LICENSE_NO = LicenseNo;
                                    insertHisMove.REQUEST_NO = item.requestNo;
                                    insertHisMove.PAYMENT_NO = item.payment_no;
                                    insertHisMove.CANCEL_REASON = "";
                                    insertHisMove.RECORD_STATUS = "";
                                    insertHisMove.MOVE_FLAG = "N";
                                    base.ctx.AG_HIS_MOVE_COMP_AGENT_T.AddObject(insertHisMove);

                                    //--*** 1. บันทึกข้อมูลใบอนุญาตใบหม่ ลง Table AG_AGENT_LICENSE_T
                                    AG_AGENT_LICENSE_T insertAgentL = new AG_AGENT_LICENSE_T();
                                    insertAgentL.LICENSE_NO = LicenseNo;
                                    insertAgentL.ID_CARD_NO = item.IdCard;
                                    insertAgentL.INSURANCE_COMP_CODE = item.ComCode;
                                    base.ctx.AG_AGENT_LICENSE_T.AddObject(insertAgentL);

                                    // --*** 2. บันทึกข้อมูลใบอนุญาตใบหม่ ลง Table AG_LICENSE_RENEW_T
                                    //AG_LICENSE_T Null CHeck
                                    //AG_LICENSE_T getDate = base.ctx.AG_LICENSE_T.FirstOrDefault(a => a.LICENSE_NO == item.selectLicense);

                                        AG_LICENSE_RENEW_T renew = new AG_LICENSE_RENEW_T();
                                        renew.LICENSE_NO = LicenseNo;
                                        renew.RENEW_TIME = 0;
                                        renew.RECEIPT_DATE = Convert.ToDateTime(item.ReceiveDate);
                                        renew.EXPIRE_DATE = Convert.ToDateTime(CheckWeekend);
                                        renew.REQUEST_NO = item.requestNo;
                                        renew.PAYMENT_NO = item.payment_no;
                                        renew.LICENSE_ACTOR = "";
                                        renew.LICENSE_ACT_DATE = DateTime.Now.Date;
                                        renew.RECEIPT_NO = item.receiptNo;
                                        base.ctx.AG_LICENSE_RENEW_T.AddObject(renew);


                                    //if (getDate != null)
                                    //{
                                       


                                        //var insertRenew = new DAL.AG_LICENSE_RENEW_T
                                        //{
                                        //    LICENSE_NO = LicenseNo,
                                        //    RENEW_TIME = 0,
                                        //    RENEW_DATE = getDate.LICENSE_DATE,
                                        //    EXPIRE_DATE = Convert.ToDateTime(CheckWeekend),
                                        //    REQUEST_NO = item.requestNo,
                                        //    PAYMENT_NO = item.payment_no,
                                        //    LICENSE_ACT_DATE = getDate.LICENSE_DATE,
                                        //    RECEIPT_NO = item.receiptNo,
                                        //};
                                        //base.ctx.AG_LICENSE_RENEW_T.AddObject(insertRenew);
                                        // -----------------------UPDATE ใบอนุญาต ฉบับเดิม------------

                                        //AG_LICENSE_T updateAgLicense = base.ctx.AG_LICENSE_T.FirstOrDefault(a => a.LICENSE_NO == item.selectLicense);
                                        //if (updateAgLicense != null)
                                        //{
                                        //    updateAgLicense.LICENSE_NO = LicenseNo;

                                        //}
                                    //}
                                    //else
                                    //{
                                    //    res.ErrorMsg = Resources.errorPaymentService_009;
                                    //}

                                }
                                #endregion

                            });


                    }
                    else
                    {
                        res.ResultMessage = false;
                        return res;
                    }
                    #region Save Change state

                    base.ctx.SaveChanges();
                    res.ResultMessage = true;

                    #endregion

                }
                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                res.ResultMessage = false;
                LoggerFactory.CreateLog().Fatal("PaymentService_Insert12T", ex);
            }

         
            return res;

        }

        private string AG_LICENSE_RUNNING(DateTime ReceiptDate, string licenseTypeC)
        {
            //var res = new DTO.ResponseService<string>();
            string res = string.Empty;
            string lastLicenseNo = string.Empty;
            string licenseNo = string.Empty;
            StringBuilder strLastNo = new StringBuilder();
            StringBuilder resNo = new StringBuilder();

            try
            {
                string LicenseNo = string.Empty;
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                string CutDate2 = string.Empty;
                if (ReceiptDate.Year < 2500)
                {
                    CutDate2 = Convert.ToString(ReceiptDate.Year + 543).Substring(2, 2);
                }
                else
                {
                    CutDate2 = Convert.ToString(ReceiptDate.Year).Substring(2, 2);
                }
                string CutDate = Convert.ToString(ReceiptDate).Replace("/", "");
                string V_Str = CutDate2 + licenseTypeC;

                AG_LICENSE_RUNNING_NO_T ent = base.ctx.AG_LICENSE_RUNNING_NO_T.FirstOrDefault(priPK => priPK.LEAD_STR == V_Str);
                if (ent != null)
                {
                    licenseNo = string.Format("{0:000000}", (Convert.ToInt32(ent.LAST_LICENSE_NO)) + 1);
                    ent.LAST_LICENSE_NO = licenseNo;

                    resNo.Append(V_Str);
                    resNo.Append(licenseNo);
                    res = resNo.ToString();

                }
                else if (ent == null)
                {
                    strLastNo.Append(V_Str);
                    strLastNo.Append("000001");
                    lastLicenseNo = strLastNo.ToString();

                    AG_LICENSE_RUNNING_NO_T newEnt = new AG_LICENSE_RUNNING_NO_T();
                    newEnt.LEAD_STR = V_Str;
                    newEnt.LAST_LICENSE_NO = "000001";
                    base.ctx.AG_LICENSE_RUNNING_NO_T.AddObject(newEnt);

                    res = lastLicenseNo;
                }

            }
            catch (Exception ex)
            {
                res = ex.Message;
                LoggerFactory.CreateLog().Fatal("PaymentService_AG_LICENSE_RUNNING", ex);
            }

            return res;

        }

        /// <summary>
        /// ดึงข้อมูลใบสั่งจ่ายกลุ่ม
        /// </summary>
        /// <param name="compCode">รหัสบริษัท, รหัสสมาคม</param>
        /// <param name="paymentCode">รหัสประเภทใบสั่งจ่าย</param>
        /// <returns>DataSet</returns>
        public DTO.ResponseService<DataSet>
            GetGroupPaymentByCriteria(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;

                tmp = string.Format(
                             "SELECT  HT.HEAD_REQUEST_NO,HT.PERSON_NO,HT.SUBPAYMENT_AMOUNT,HT.PETITION_TYPE_CODE, " +
                             "HT.SUBPAYMENT_DATE,DT.RECEIPT_DATE,HT.REMARK " +
                             "FROM AG_IAS_SUBPAYMENT_H_T HT, " +
                             "AG_IAS_SUBPAYMENT_D_T DT " +
                             "WHERE HT.HEAD_REQUEST_NO = DT.HEAD_REQUEST_NO AND " +
                             "HT.UPLOAD_BY_SESSION = '" + compCode + "' AND " +
                             "HT.petition_type_code like '" + paymentCode + "%' AND " +
                             "HT.SUBPAYMENT_DATE BETWEEN " +
                             "to_date('{0}','yyyymmdd') AND " +
                             "to_date('{1}','yyyymmdd') ",
                             Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                             Convert.ToDateTime(toDate).ToString_yyyyMMdd());

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetGroupPaymentByCriteria", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลเพื่อสร้างเลขใบเสร็จ
        /// </summary>
        /// <param name="headerId">เลขที่กลุ่ม</param>
        /// <param name="Id">เลขที่รายการ</param>
        /// <returns>ResponseService<BankTransDetail></returns>

        public DTO.ResponseService<DataSet>
           GenPaymentNumberTable(string compCode,
                              DateTime? startDate, DateTime? toDate,
                              string paymentCode, string CountRecord, int pageNo, int recordPerPage)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;
                StringBuilder sb = new StringBuilder();

                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                if (CountRecord == "Y")
                {
                    //tmp = string.Format(
                    //          " SELECT Count(*) as rowcount FROM(SELECT  GT.GROUP_REQUEST_NO , GT.GROUP_AMOUNT , GT.GROUP_DATE , GT.PAYMENT_DATE , GT.REMARK  , count(distinct subh.head_request_no) as PERSON_NO   " +
                    //          " ,ROW_NUMBER() OVER (ORDER BY GT.GROUP_REQUEST_NO) RUN_NO " +
                    //          " FROM AG_IAS_PAYMENT_G_T GT, ag_ias_subpayment_h_t subH  , " +
                    //          " ag_ias_subpayment_d_t subD,AG_IAS_SUBPAYMENT_RECEIPT SubR   " +
                    //          "WHERE GT.group_request_no like  '" + paymentCode + "%' AND " +
                    //          " SubR.RECEIPT_NO is not null AND " +
                    //          " gt.group_request_no = subh.group_request_no and subh.head_request_no = subd.head_request_no " +
                    //          " and subd.head_request_no =  SubR.head_request_no  " +
                    //          " and( subd.record_status = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' or subd.record_status='" + DTO.SubPayment_D_T_Status.W.ToString() + "') and " +
                    //          "GT.PAYMENT_DATE BETWEEN " +
                    //          "to_date('{0}','yyyymmdd') AND " +
                    //          "to_date('{1}','yyyymmdd') " +
                    //          "    GROUP BY GT.GROUP_REQUEST_NO, GT.GROUP_AMOUNT, GT.GROUP_DATE, GT.PAYMENT_DATE, GT.REMARK)A   ",
                    //          Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                    //          Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                    tmp = string.Format(
                         "SELECT Count(*) as rowcount FROM( "
                         + "select GROUP_REQUEST_NO, PAYMENT_DATE, AMOUNT,FULL_NAME,PETITION_TYPE_NAME,ID_CARD_NO "
                         + "from AG_IAS_SUBPAYMENT_RECEIPT "
                        + "where GROUP_REQUEST_NO like '" + paymentCode + "%' and GEN_STATUS in ('W', 'A') "
                        + "and PAYMENT_NO is not null and HEAD_REQUEST_NO is not null "
                        + "and PAYMENT_DATE BETWEEN "
                    + "to_date('{0}','yyyymmdd') AND "
                    + "to_date('{1}','yyyymmdd') "
                        + ")A ",
                        Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                        Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                }
                else
                {
                    //tmp = string.Format(
                    //             " SELECT * FROM(SELECT  GT.GROUP_REQUEST_NO , GT.GROUP_AMOUNT , GT.GROUP_DATE , GT.PAYMENT_DATE , GT.REMARK  , count(distinct subh.head_request_no) as PERSON_NO   " +
                    //             " ,ROW_NUMBER() OVER (ORDER BY GT.GROUP_REQUEST_NO) RUN_NO " +
                    //             " FROM AG_IAS_PAYMENT_G_T GT, ag_ias_subpayment_h_t subH  , " +
                    //             " ag_ias_subpayment_d_t subD,AG_IAS_SUBPAYMENT_RECEIPT SubR  " +
                    //             "WHERE GT.group_request_no like  '" + paymentCode + "%' AND " +
                    //             " SubR.RECEIPT_NO is not null AND " +
                    //             " gt.group_request_no = subh.group_request_no and subh.head_request_no = subd.head_request_no " +
                    //              " and subd.head_request_no =  SubR.head_request_no  " +
                    //             "and ( subd.record_status = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' or subd.record_status='" + DTO.SubPayment_D_T_Status.W.ToString() + "') and " +
                    //             "GT.PAYMENT_DATE BETWEEN " +
                    //             "to_date('{0}','yyyymmdd') AND " +
                    //             "to_date('{1}','yyyymmdd') " +
                    //             "    GROUP BY GT.GROUP_REQUEST_NO, GT.GROUP_AMOUNT, GT.GROUP_DATE, GT.PAYMENT_DATE, GT.REMARK)A   ",
                    //             Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                    //             Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + critRecNo;
                    tmp = string.Format(
                     "SELECT * FROM( "
              + "select GROUP_REQUEST_NO, PAYMENT_DATE, AMOUNT,FULL_NAME,PETITION_TYPE_NAME,ID_CARD_NO,RECEIPT_NO,HEAD_REQUEST_NO,PAYMENT_NO "
              + " ,ROW_NUMBER() OVER (ORDER BY GROUP_REQUEST_NO,HEAD_REQUEST_NO,PAYMENT_NO) RUN_NO "
                         + "from AG_IAS_SUBPAYMENT_RECEIPT "
                        + "where GROUP_REQUEST_NO like '" + paymentCode + "%' and GEN_STATUS in ('W', 'A') "
                          + "and PAYMENT_NO is not null and HEAD_REQUEST_NO is not null "
                    + "and PAYMENT_DATE BETWEEN "
                    + "to_date('{0}','yyyymmdd') AND "
                    + "to_date('{1}','yyyymmdd') "
                    + ")A ",
                    Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                    Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + critRecNo;
                }
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GenPaymentNumberTable", ex);
            }
            return res;
        }

        //log4net
        public DTO.ResponseMessage<bool> GenPaymentNumber(string Group_req_no, string UID, string receiptNo)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                //StringBuilder sb = new StringBuilder();
                //string tmp = string.Empty;
                //OracleDB oraUpdate;
                //tmp = " update AG_IAS_SUBPAYMENT_RECEIPT set receipt_by_id = '" + UID + "', GEN_STATUS = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' where  "
                //    + " head_request_no = '" + Group_req_no + "' and RECEIPT_NO = '" + receiptNo + "' ";

                //oraUpdate = new OracleDB();
                //oraUpdate.GetDataSet(tmp);


                AG_IAS_SUBPAYMENT_RECEIPT ent = base.ctx.AG_IAS_SUBPAYMENT_RECEIPT.FirstOrDefault(s => s.HEAD_REQUEST_NO == Group_req_no &&
                    s.RECEIPT_NO == receiptNo);
                if (ent != null)
                {
                    ent.RECEIPT_BY_ID = UID;
                    ent.GEN_STATUS = DTO.SubPayment_D_T_Status.A.ToString();
                    base.ctx.SaveChanges();
                    res.ResultMessage = true;
                }
                else
                {
                    res.ErrorMsg = "สร้างใบเสร็จไม่สำเร็จ" + " : " + "GenPaymentNumber()";
                    res.ResultMessage = false;

                }


            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorPaymentService_010;
                LoggerFactory.CreateLog().Fatal("PaymentService_GenPaymentNumber", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> GenReceiptAll(List<DTO.GenReceipt> GenReceipt)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                if (!FtpHelpers.CheckFtpConnect())
                {
                    res.ErrorMsg = "ไม่สามารถเชื่อมต่อ FTP";
                    res.ResultMessage = false;
                    return res;
                }

                string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
                string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
                string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
                using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive)) {
                    foreach (DTO.GenReceipt item in GenReceipt)
                    {
                        AG_IAS_SUBPAYMENT_RECEIPT ent = base.ctx.AG_IAS_SUBPAYMENT_RECEIPT.FirstOrDefault(s => s.HEAD_REQUEST_NO == item.HEAD_REQUEST_NO &&
                         s.RECEIPT_NO == item.RECEIPT_NO);
                        if (ent != null)
                        {
                            ent.RECEIPT_BY_ID = item.RECEIPT_BY_ID;
                            ent.GEN_STATUS = DTO.SubPayment_D_T_Status.A.ToString();
                            ent.RECEIPT_DATE = DateTime.Now;
                            base.ctx.SaveChanges();
                            #region GetDataFromSub_D_T


                           
                            string[] PDF_Receipt = new string[2];
                            PDF_Receipt[0] = ConfigurationManager.AppSettings["FS_RECEIVE"].ToString();
                            PDF_Receipt[1] = ConfigurationManager.AppSettings["FS_FINANCE"].ToString();
                            String[] dirpath = new string[PDF_Receipt.Length];
                            string[] report = new string[PDF_Receipt.Length];

                            string YMD = DateTime.Now.ToString_yyyyMMdd();
                            string tmp = string.Empty;
                            string strGUID = string.Empty;
                            string RecNo = string.Empty;
                            string idC = string.Empty;
                            String hashCode = string.Empty;
                            string tempGenRecive = string.Empty;
                            string FileNameInput = string.Empty;
                            string PathOut = string.Empty;
                            Int64 fileSize;
                            Guid Gu_id = Guid.NewGuid();
                            try
                            {
                                tmp = " select d.ID_CARD_NO,d.PAYMENT_NO,RE.SIGNATUER_POSITION,d.head_request_no,RE.PAYMENT_NO,RE.receipt_by_id,RE.RECEIPT_NO,RE.PAYMENT_DATE, "
                                    + "RE.FULL_NAME FirstName,  (select NN.NAME || ' ' || ipt.names || ' ' || ipt.lastname from AG_IAS_PERSONAL_T ipt,VW_IAS_TITLE_NAME NN "
                                    + "where RE.RECEIPT_BY_ID = ipt.ID and NN.ID = ipt.pre_name_code) LASTNAME, (select ipt.img_sign from AG_IAS_PERSONAL_T ipt "
                                    + "where RE.RECEIPT_BY_ID = ipt.ID ) img_sign,RE.AMOUNT,RE.RECEIPT_DATE,RE.PETITION_TYPE_NAME,RE.group_request_no,gt.CREATED_BY,d.LICENSE_TYPE_CODE "
                                    + ",(select TR.LICENSE_TYPE_NAME from AG_LICENSE_TYPE_R TR where TR.LICENSE_TYPE_CODE = d.LICENSE_TYPE_CODE) LICENSE_TYPE_NAME "
                                    + "from (select * from ag_ias_subpayment_d_t where HEAD_REQUEST_NO =  '" + item.HEAD_REQUEST_NO + "' and PAYMENT_NO = '" + item.PAYMENT_NO + "') d "
                                    + ",ag_ias_subpayment_h_t h "
                                    + " ,(select * from AG_IAS_SUBPAYMENT_RECEIPT where GEN_STATUS = '" + DTO.SubPayment_D_T_Status.A.ToString() + "'  and receipt_by_id = '" + item.RECEIPT_BY_ID + "' "
                                    + " and RECEIPT_NO = '" + item.RECEIPT_NO + "' ) RE "
                                    + ",(select * from AG_IAS_PAYMENT_G_T )GT "
                                    + " where d.head_request_no = h.head_request_no    "
                                    + " and d.PAYMENT_NO = RE.PAYMENT_NO  and d.HEAD_REQUEST_NO = RE.HEAD_REQUEST_NO "
                                    + " and  GT.GROUP_REQUEST_NO = RE.GROUP_REQUEST_NO ";

                                OracleDB ora = new OracleDB();
                                DataSet DS_D_T = ora.GetDataSet(tmp);
                                DataTable DT_D_T = DS_D_T.Tables[0];
                                if (DT_D_T.Rows.Count > 0)
                                {

                                    DataRow DR_D_T;
                                    DR_D_T = DT_D_T.Rows[0];
                                    for (int copy = 0; copy < 2; copy++) //add by milk case copy receipt add text สำเนา
                                    {

                                        var ls = new List<RptReciveClassService>();
                                        #region GenReceipt
                                        idC = DR_D_T["ID_CARD_NO"].ToString();
                                        string payNo = DR_D_T["PAYMENT_NO"].ToString();
                                        string Position = (DR_D_T["SIGNATUER_POSITION"].ToString().Length == 0) ? " " : DR_D_T["SIGNATUER_POSITION"].ToString();
                                        string H_req_no = DR_D_T["head_request_no"].ToString();

                                        RecNo = DR_D_T["RECEIPT_NO"].ToString();
                                        const string ReportFolder = @"\Reports\";

                                        #region Gu_id และ สำเนาใบเสร็จ
                                        RptReciveClassService rcv = new RptReciveClassService();
                                        string ppath = "OIC/copy_recive.jpg".Replace(@"/", @"\");

                                        // Check Is Exsit file Image Copy for Reporting
                                        //FileInfo copyImg = new FileInfo(Path.Combine(_netDrive, ppath));
                                        //if (!copyImg.Exists) {
                                        //    String wordError = String.Format("ไม่พบไฟล์ภาพ {0}", copyImg.FullName);
                                        //    LoggerFactory.CreateLog().LogError(wordError);
                                        //    throw new ApplicationException(wordError);
                                        //}
                                        //-----------------------------------------------------

                                        if (copy == 0)
                                        {

                                            RecNo = DR_D_T["RECEIPT_NO"].ToString();
                                            strGUID = Gu_id.ToString();
                                            rcv.BG_copy_pathArray = null;
                                        }
                                        else
                                        {
                                            rcv.BG_copy_pathArray = Signature_Img(ppath);
                                            RecNo = DR_D_T["RECEIPT_NO"].ToString() + "_COPY";
                                        }
                                        #endregion Gu_id และ สำเนาใบเสร็จ

                                        #region ตัวแปรต่างๆ


                                        FileNameInput = String.Format("{0}_{1}.pdf", idC, RecNo);
                                        string mapDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();

                                        #endregion ตัวแปรต่างๆ

                                        #region สร้างโฟรเดอร์ ใน PDF_Receipt.Length พาร์ท
                                        for (int Y = 0; Y < PDF_Receipt.Length; Y++)
                                        {
                                            string path = String.Format(@"{0}\{1}", PDF_Receipt[Y], idC);
                                            dirpath[Y] = Path.Combine(mapDrive, PDF_Receipt[Y], YMD, idC);

                                            DirectoryInfo dir = new DirectoryInfo(dirpath[Y]);
                                            if (!dir.Exists)
                                                dir.Create();
                                            if (copy == 1)
                                            {
                                                if (Y == 0)
                                                {
                                                    report[Y] = String.Format(@"{0}\{1}", dirpath[Y], FileNameInput);
                                                }
                                            }
                                            else//0
                                            {
                                                report[Y] = String.Format(@"{0}\{1}", dirpath[Y], FileNameInput);
                                            }
                                        }

                                        #endregion  สร้างโฟรเดอร์ ใน PDF_Receipt.Length พาร์ท

                                        #region ยัดค่าลง Report
                                        string[] TempBar = DR_D_T["RECEIPT_NO"].ToString().Split('e', 'N');
                                        string subBar = TempBar[1];

                                        string dtID = DR_D_T["id_card_no"].ToString();
                                        string Bar = subBar.PadLeft(13, '0'); // barcode 13 หลัก

                                        rcv.BillNumber = DR_D_T["RECEIPT_NO"].ToString();
                                        rcv.ReceiptDate = GetReceiptDate(DR_D_T["RECEIPT_DATE"].ToString());
                                        //rcv.ReceiptDate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(DT_DataRe.Rows[0]["RECEIPT_DATE"].ToString()));
                                        rcv.FirstName = DR_D_T["FIRSTNAME"].ToString();
                                        rcv.LastName = DR_D_T["LASTNAME"].ToString();
                                        rcv.AMOUNT = string.Format("{0:n0}", Convert.ToDecimal(DR_D_T["AMOUNT"].ToString()));
                                        rcv.PaymentType = String.Format("{0}  {1}", DR_D_T["PETITION_TYPE_NAME"], DR_D_T["LICENSE_TYPE_NAME"]);

                                        rcv.BathThai = Utils.ConvertMoneyToBath.ConvertMoneyToThai(rcv.AMOUNT);

                                        rcv.POSITION = Position;
                                        rcv.SigImgPathArray = (byte[])(DR_D_T["img_sign"]); // Signature_Img(ppath);
                                        rcv.QRcordPathArray = GenQRcode.CreateQRcode(ctx, DT_D_T.Rows[0].ToCreateReceiptRequest());
                                        //exit
                                        if (rcv.QRcordPathArray == null)
                                        {
                                            res.ErrorMsg = "ไม่พบข้อมูลของเจ้าของใบสั่งจ่าย กรุณาติดต่อผู้ดูแลระบบ";
                                            res.ResultMessage = false;
                                            return res;
                                        }

                                        rcv.GUID = strGUID;
                                        rcv.BarCodeImage = Utils.BarCode.GetBarCodeData(Bar, "Tahoma", 1000, 80);
                                        rcv.BarCode = Bar;

                                        ls.Add(rcv);
                                        #endregion ยัดค่าลง Report

                                        #region สร้างไฟล์เก็บในพาร์ท

                                        for (int Y = 0; Y < PDF_Receipt.Length; Y++)
                                        {
                                            ReportDocument rpt = new ReportDocument();

                                            if ((Y == 1) && (copy == 1))
                                            {
                                            }
                                            else
                                            {
                                                using (FileStream fs = new FileStream(report[Y], FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                                                {
                                                    StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                                                    sw.Close();
                                                    fs.Close();
                                                    if (Y == 0)//recrive Path
                                                    {
                                                        rpt.Load(String.Format("{0}{1}RptRecive.rpt", System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, ReportFolder));
                                                        if (ls.Count > 1)
                                                            ls[0].BG_copy_pathArray = ls[1].BG_copy_pathArray;
                                                        rpt.SetDataSource(ls);
                                                        rpt.ExportToDisk(ExportFormatType.PortableDocFormat, report[Y]);
                                                        if (Y == 0 && copy == 0)
                                                        {
                                                            FtpHelpers.Upload(rpt.ExportToStream(ExportFormatType.PortableDocFormat), report[Y].Replace(_netDrive, ""));
                                                        }
                                                    }
                                                    else// finace path
                                                    {
                                                        if (copy != 1) //1 = ver. copy
                                                        {
                                                            rpt.Load(String.Format("{0}{1}RptRecive.rpt", System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, ReportFolder));
                                                            rpt.SetDataSource(ls);
                                                            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, report[Y]);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        #endregion

                                        #region สร้าง hashCode
                                        hashCode = IAS.Utils.FileObject.GetHashSHA1(report[0]);

                                        PathOut = Path.Combine(PDF_Receipt[0], YMD, idC, FileNameInput.Replace("_COPY", ""));



                                        #endregion สร้าง hashCode



                                        #endregion

                                    }
                                    if (report[0] != "")
                                    {
                                        fileSize = new System.IO.FileInfo(report[0]).Length;

                                        tempGenRecive = AddStatusReceiveCompletetoDB(DR_D_T["HEAD_REQUEST_NO"].ToString(),
                                                                                   DR_D_T["RECEIPT_BY_ID"].ToString(),
                                                                                   PathOut,
                                                                                   DR_D_T["PAYMENT_NO"].ToString(), hashCode, Gu_id, DR_D_T["RECEIPT_NO"].ToString(), fileSize);
                                    }
                                    if (tempGenRecive == "Y")
                                    {
                                        try
                                        {

                                            #region ส่งเมล์เจ้าของใบสั่งจ่าย
                                            string SQLOwner = "select GT.UPLOAD_BY_SESSION,TT.EMAIL,case when  COMP.NAME is not null then COMP.name else Ass.Association_name end  C_name "
                                                            + "from  AG_IAS_PAYMENT_G_T GT "
                                                            + "join ag_ias_personal_T TT on TT.COMP_CODE = GT.UPLOAD_BY_SESSION "
                                                            + "left join vw_ias_com_code Comp on COMP.id = GT.upload_by_session "
                                                            + "left join ag_ias_ASSOCIATION Ass on Ass.ASSOCIATION_CODE = GT.upload_by_session  "
                                                            + "where GT.GROUP_REQUEST_NO = '" + DR_D_T["group_request_no"].ToString() + "' "
                                                            + "union "
                                                            + "select GT.UPLOAD_BY_SESSION,TT.EMAIL, RE.FULL_NAME C_name "
                                                            + "from  AG_IAS_PAYMENT_G_T GT "
                                                            + "join ag_ias_personal_T TT on tt.id = GT.UPLOAD_BY_SESSION "
                                                            + " join AG_IAS_SUBPAYMENT_RECEIPT RE on re.group_request_no = gt.group_request_no "
                                                            + "where GT.GROUP_REQUEST_NO = '" + DR_D_T["group_request_no"].ToString() + "' ";

                                            #endregion ส่งเมล์เจ้าของใบสั่งจ่าย
                                            //string sql = "select p.email  C_mail ,case when  COMP.NAME is not null then COMP.name else Ass.Association_name end  C_name  " +
                                            //           " from ag_ias_payment_g_T T " +
                                            //           "  left join vw_ias_com_code Comp on COMP.id = T.upload_by_session " +
                                            //            " left join ag_ias_ASSOCIATION Ass on Ass.ASSOCIATION_CODE = T.upload_by_session " +
                                            //           "  left join ag_ias_personal_t P on p.comp_code = T.upload_by_session " +
                                            //           "  left join ag_ias_users U on U.is_active = 'A'  " +
                                            //           "   where u.user_id=p.id and t.group_request_no ='" + DR_D_T["group_request_no"].ToString() + "' ";
                                            OracleDB oraa = new OracleDB();
                                            DataTable personalUser = oraa.GetDataSet(SQLOwner).Tables[0];
                                            if (personalUser.Rows.Count > 0)
                                            {
                                                IList<DTO.EmailReceiptTaskingRequest> _emailReceiptTaskings = new List<EmailReceiptTaskingRequest>();

                                                FileInfo fileInfoo = new FileInfo(report[1]);
                                                if (fileInfoo.Exists)
                                                {
                                                    _emailReceiptTaskings.Add(new EmailReceiptTaskingRequest()
                                                    {
                                                        //Email = personal.E_MAIL,
                                                        ReciveNo = DR_D_T["RECEIPT_NO"].ToString(),
                                                        FullName = DR_D_T["FIRSTNAME"].ToString(),
                                                        IDCard = DR_D_T["ID_CARD_NO"].ToString(),
                                                        PettionTypeName = DR_D_T["petition_type_name"].ToString(),
                                                        Receipt = fileInfoo
                                                    });
                                                }
                                                string[] E_mail = new string[personalUser.Rows.Count];
                                                for (int P = 0; P < personalUser.Rows.Count; P++)
                                                {
                                                    E_mail[P] = personalUser.Rows[P]["EMAIL"].ToString();
                                                }
                                                MailReceiptSuccessHelper.SendMail(personalUser.Rows[0]["C_name"].ToString(), E_mail, _emailReceiptTaskings);
                                                //ส่งเมล์คนอยู่ใต้สังกัด
                                                if (personalUser.Rows[0]["UPLOAD_BY_SESSION"].ToString().Length < 4)
                                                {
                                                    FileInfo fileInfooC = new FileInfo(report[0]);
                                                    if (fileInfoo.Exists)
                                                    {
                                                        _emailReceiptTaskings.Add(new EmailReceiptTaskingRequest()
                                                        {
                                                            //Email = personal.E_MAIL,
                                                            ReciveNo = DR_D_T["RECEIPT_NO"].ToString(),
                                                            FullName = DR_D_T["FIRSTNAME"].ToString(),
                                                            IDCard = DR_D_T["ID_CARD_NO"].ToString(),
                                                            PettionTypeName = DR_D_T["petition_type_name"].ToString(),
                                                            Receipt = fileInfooC
                                                        });
                                                    }
                                                    string SQLSub = "select TT.E_MAIL EMAIL from  AG_PERSONAL_T TT "
                                                                  + "join AG_IAS_SUBPAYMENT_RECEIPT RE on re.id_card_no = tt.id_card_no "
                                                                  + "where re.id_card_no = '" + DR_D_T["ID_CARD_NO"].ToString() + "' "
                                                                  + "union "
                                                                  + "select TT.EMAIL EMAIL from  ag_ias_personal_T TT "
                                                                  + "join AG_IAS_SUBPAYMENT_RECEIPT RE on re.id_card_no = tt.id_card_no "
                                                                  + "where re.id_card_no = '" + DR_D_T["ID_CARD_NO"].ToString() + "' ";

                                                    DataTable SubUser = oraa.GetDataSet(SQLSub).Tables[0];
                                                    if (SubUser.Rows.Count > 0)
                                                    {
                                                        string[] E_mailSub = new string[SubUser.Rows.Count];
                                                        for (int P = 0; P < SubUser.Rows.Count; P++)
                                                        {
                                                            E_mailSub[P] = personalUser.Rows[P]["EMAIL"].ToString();
                                                        }
                                                        MailReceiptSuccessHelper.SendMail(DR_D_T["FIRSTNAME"].ToString(), E_mailSub, _emailReceiptTaskings);

                                                    }


                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            res.ResultMessage = false;
                                            res.ErrorMsg = "พบข้อผิดพลาดในการส่งอีเมล์ให้ผู้เกี่ยวข้อง";
                                            LoggerFactory.CreateLog().Fatal("PaymentService_GenReceiptAll", ex);
                                            return res;
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                res.ErrorMsg = "สร้างใบเสร็จไม่สำเร็จ";
                                res.ResultMessage = false;
                                LoggerFactory.CreateLog().Fatal("PaymentService_GenReceiptAll", ex);
                                return res;
                            }

                            #endregion GetDataFromSub_D_T

                        }
                        else
                        {
                            res.ErrorMsg = "สร้างใบเสร็จไม่สำเร็จ";
                            res.ResultMessage = false;
                            return res;
                        }

                    }
                }
      

                res.ResultMessage = true;
                return res;

            }
            catch (Exception ex)
            {
                res.ResultMessage = false;
                res.ErrorMsg = Resources.errorPaymentService_010;
                LoggerFactory.CreateLog().Fatal("PaymentService_GenReceiptAll", ex);
            }
            return res;
        }
        //สร้างใบเสร็จหลายๆๆใบ
        public string CreatePdf(string[] fileNames)
        {
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);
            string namedate = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            string mapDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();
            string parth_filename = String.Format(@"ReceiptFile\CombinePDF\{0}\", namedate);
            DirectoryInfo dir = new DirectoryInfo(mapDrive + parth_filename);

            if (!dir.Exists)
                dir.Create();

            parth_filename = parth_filename + namedate;
            using (FileStream fs = new FileStream(mapDrive + parth_filename + ".pdf", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Close();
                fs.Close();


                int pageOffset = 0;
                int f = 0;
                Document document = null;
                PdfCopy writer = null;
                while (f < fileNames.Length)
                {
                    try
                    {

                        // เริ่มอ่านและสร้าง pdf 
                        PdfReader reader = new PdfReader(mapDrive + fileNames[f]);

                        reader.ConsolidateNamedDestinations();
                        // จำนวนหน้าทั้งหมดที่สร้าง
                        int n = reader.NumberOfPages;
                        pageOffset += n;
                        if (f == 0)
                        {
                            // ขั้นที่ 1: สร้าง pdf

                            document = new Document(reader.GetPageSizeWithRotation(1));
                            // ขั้น 2: กอปปี้


                            writer = new PdfCopy(document, new FileStream(mapDrive + parth_filename + ".pdf", FileMode.Create));


                            // ขั้นที่ 3: เปิด pdf
                            document.Open();
                        }
                        // ขั้นที่ 4: เพิ่ม pdf
                        for (int i = 0; i < n; )
                        {
                            ++i;
                            if (writer != null)
                            {
                                PdfImportedPage page = writer.GetImportedPage(reader, i);

                                writer.AddPage(page);
                            }
                        }
                        PRAcroForm form = reader.AcroForm;
                        if (form != null && writer != null)
                        {
                            writer.CopyAcroForm(reader);
                        }

                    }
                    catch (Exception ex)
                    {

                        LoggerFactory.CreateLog().Fatal("PaymentService_CreatePdf", ex);
                    }
                    f++;
                }

                // step 5: ปิดการสร้าง pdf
                if (document != null)
                {
                    document.Close();
                }

            }

            if (nasDrive != null)
            {
                nasDrive.Dispose();
            }

            return parth_filename + ".pdf";
        }


        //Zip ใบเสร็จหลายๆๆใบ
        public string CreateZip(string parthpdf)
        {
            //parthpdf = @"\\192.168.15.10\IASFileUpload\ReceiptFile\CombinePDF\2013112216459215\2013112216459215.pdf";
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);
            string namedate = DateTime.Now.Year.ToString("0000") + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            string mapDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();
            string parth_filename = @"ReceiptFile\CombinePDF\" + namedate + @"\";

            DirectoryInfo dir = new DirectoryInfo(mapDrive + parth_filename);

            if (!dir.Exists)
                dir.Create();

            parth_filename = parth_filename + namedate + ".Zip";
            using (FileStream fs = new FileStream(mapDrive + parth_filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                sw.Close();
                fs.Close();
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddFile(mapDrive + parthpdf, "");
                    string parthzip = mapDrive + parth_filename;
                    zip.Save(mapDrive + parth_filename);
                }
            }
            return parth_filename;
        }


        public string
           AddGenReceiveNumbertoDB(string H_req_no, string UID, string st_date, string ed_date, string ID)
        {
            var res = new DTO.ResponseService<DataSet>();

            string R = "N";

            try
            {
                string tmp = string.Empty;
                tmp = " update ag_ias_subpayment_d_t set receipt_by_id = '" + UID + "', record_status = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' where  "
                           + " head_request_no = '" + H_req_no + "' and ID_CARD_NO = '" + ID + "' and "
                           + " payment_date between to_date(to_char(to_date('" + st_date + "','DD/MM/RRRR', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/RRRR', ' NLS_DATE_LANGUAGE=AMERICAN'),'DD/MM/RRRR') "
                           + " and to_date(to_char(to_date('" + ed_date + "','DD/MM/RRRR', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/RRRR', ' NLS_DATE_LANGUAGE=AMERICAN'),'DD/MM/RRRR')";

                OracleDB ora = new OracleDB();
                ora.GetDataSet(tmp);
                R = "Y";
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_AddGenReceiveNumbertoDB", ex);
            }
            return R;
        }

        public DTO.ResponseService<DataSet> GetDataFromSub_D_T(string G_req_no, string UID, string IdWhenCareteInDetail)
        {
            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive);
            IList<DTO.EmailReceiptTaskingRequest> _emailReceiptTaskings = new List<EmailReceiptTaskingRequest>();
            string tempGenRecive = string.Empty;
            var res = new DTO.ResponseService<DataSet>();
            var data = new List<DTO.ReceiveNo>();
            string[] PDF_Receipt = new string[2];
            PDF_Receipt[0] = ConfigurationManager.AppSettings["FS_RECEIVE"].ToString();
            PDF_Receipt[1] = ConfigurationManager.AppSettings["FS_FINANCE"].ToString();
            String[] dirpath = new string[PDF_Receipt.Length];
            string[] report = new string[PDF_Receipt.Length];
            try
            {


                string IDcon = string.Empty;
                if (IdWhenCareteInDetail != "") //สำหรับกรณีเก็บตกที่มีการerrorในการสร้างเอกสาร และต้องมากำหนดการสร้างรายคน
                {
                    IDcon = String.Format(" and d.ID_CARD_NO = '{0}' ", IdWhenCareteInDetail);
                }


                string YMD = DateTime.Now.ToString_yyyyMMdd();
                string tmp = string.Empty;
                //tmp = " select * from ag_ias_subpayment_d_t d,ag_ias_subpayment_h_t h "
                //        + " where d.head_request_no = h.head_request_no and h.group_request_no =  '" + G_req_no + "' and "
                //        + " d.record_status = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' and  d.receipt_by_id = '" + UID + "' " + IDcon;
                tmp = " select d.ID_CARD_NO,d.PAYMENT_NO,RE.SIGNATUER_POSITION,d.head_request_no,RE.PAYMENT_NO,RE.receipt_by_id,RE.GROUP_REQUEST_NO "
                    + "from ag_ias_subpayment_d_t d,ag_ias_subpayment_h_t h,AG_IAS_SUBPAYMENT_RECEIPT RE "
                        + " where d.head_request_no = h.head_request_no and h.HEAD_REQUEST_NO =  '" + G_req_no + "'  "
                        + " and d.PAYMENT_NO = RE.PAYMENT_NO  and d.HEAD_REQUEST_NO = RE.HEAD_REQUEST_NO "
                        + " and RE.GEN_STATUS = '" + DTO.SubPayment_D_T_Status.A.ToString() + "' and  RE.receipt_by_id = '" + UID + "' " + IDcon;
                OracleDB ora = new OracleDB();
                DataSet DS_D_T = ora.GetDataSet(tmp);
                if (DS_D_T.Tables.Count > 0)
                {
                    DataTable DT_D_T = DS_D_T.Tables[0];
                    if (DT_D_T.Rows.Count > 0)
                    {
                        int i = 0;
                        DataRow DR_D_T;

                        // string createby = DT_D_T.Rows[0]["CREATE D_BY"].ToString();
                        //personalUser = ctx.AG_IAS_PERSONAL_T.SingleOrDefault(p => p.ID == createby);
                        for (i = 0; i < DT_D_T.Rows.Count; i++)
                        {
                            string strGUID = string.Empty;
                            for (int copy = 0; copy < 2; copy++) //add by milk case copy receipt add text สำเนา
                            {
                                DR_D_T = DT_D_T.Rows[i];
                                //---------ใบเสร็จ---------------//
                                #region GenReceipt
                                string idC = DR_D_T["ID_CARD_NO"].ToString();
                                string payNo = DR_D_T["PAYMENT_NO"].ToString();
                                string Position = (DR_D_T["SIGNATUER_POSITION"].ToString().Length == 0) ? " " : DR_D_T["SIGNATUER_POSITION"].ToString();
                                string H_req_no = DR_D_T["head_request_no"].ToString();
                                string RecNo = string.Empty;


                                var DataRe = GetDataPayment_BeforeSentToReport(H_req_no, idC, payNo);
                                DataSet DS_DataRe = DataRe.DataResponse;
                                if (DS_DataRe.Tables.Count > 0)
                                {
                                    DataTable DT_DataRe = DS_DataRe.Tables[0];
                                    if (DT_DataRe.Rows.Count > 0)
                                    {
                                        for (int n = 0; n < DT_DataRe.Rows.Count; n++)
                                        {
                                            RecNo = DT_DataRe.Rows[n]["RECEIPT_NO"].ToString();

                                            var ls = new List<RptReciveClassService>();

                                            string ReportFolder = @"\Reports\";

                                            #region Gu_id และ สำเนาใบเสร็จ
                                            RptReciveClassService rcv = new RptReciveClassService();
                                            //string ppath = DT_DataRe.Rows[0]["signature_img"].ToString().Replace(@"/", @"\");
                                            string ppath = "OIC/copy_recive.jpg".Replace(@"/", @"\");
                                            Guid Gu_id = Guid.NewGuid();
                                            if (copy == 0)
                                            {

                                                RecNo = DT_DataRe.Rows[n]["RECEIPT_NO"].ToString();
                                                strGUID = Gu_id.ToString();
                                                rcv.BG_copy_pathArray = null;
                                            }
                                            else
                                            {
                                                rcv.BG_copy_pathArray = Signature_Img(ppath);
                                                RecNo = DT_DataRe.Rows[n]["RECEIPT_NO"].ToString() + "_COPY";
                                            }
                                            #endregion Gu_id และ สำเนาใบเสร็จ

                                            #region ตัวแปรต่างๆ
                                            //string[] PDF_Receipt = new string[2];
                                            PDF_Receipt[0] = ConfigurationManager.AppSettings["FS_RECEIVE"].ToString();
                                            PDF_Receipt[1] = ConfigurationManager.AppSettings["FS_FINANCE"].ToString();


                                            string FileNameInput = idC + "_" + RecNo + ".pdf";
                                            string mapDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"].ToString();
                                            //String[] dirpath = new string[PDF_Receipt.Length];
                                            //string[] report = new string[PDF_Receipt.Length];
                                            #endregion ตัวแปรต่างๆ


                                            #region สร้างโฟรเดอร์ ใน PDF_Receipt.Length พาร์ท
                                            for (int Y = 0; Y < PDF_Receipt.Length; Y++)
                                            {
                                                string path = PDF_Receipt[Y] + @"\" + idC;
                                                dirpath[Y] = Path.Combine(mapDrive, PDF_Receipt[Y], YMD, idC);

                                                DirectoryInfo dir = new DirectoryInfo(dirpath[Y]);
                                                if (!dir.Exists)
                                                    dir.Create();
                                                if (copy == 1)
                                                {
                                                    if (Y == 0)
                                                    {
                                                        report[Y] = String.Format(@"{0}\{1}", dirpath[Y], FileNameInput);
                                                    }
                                                }
                                                else//0
                                                {
                                                    report[Y] = String.Format(@"{0}\{1}", dirpath[Y], FileNameInput);
                                                }
                                            }
                                            #endregion  สร้างโฟรเดอร์ ใน PDF_Receipt.Length พาร์ท






                                            #region ยัดค่าลง Report

                                            string[] TempBar = DT_DataRe.Rows[n]["RECEIPT_NO"].ToString().Split('e', 'N');
                                            string subBar = TempBar[1];

                                            string dtID = DT_DataRe.Rows[n]["id_card_no"].ToString();
                                            string Bar = subBar.PadLeft(13, '0'); // barcode 13 หลัก





                                            rcv.BillNumber = DT_DataRe.Rows[n]["RECEIPT_NO"].ToString();
                                            rcv.ReceiptDate = GetReceiptDate(DT_DataRe.Rows[n]["RECEIPT_DATE"].ToString());
                                            //rcv.ReceiptDate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(DT_DataRe.Rows[0]["RECEIPT_DATE"].ToString()));
                                            rcv.FirstName = DT_DataRe.Rows[n]["FIRSTNAME"].ToString();
                                            rcv.LastName = DT_DataRe.Rows[n]["LASTNAME"].ToString();
                                            rcv.AMOUNT = string.Format("{0:n0}", Convert.ToDecimal(DT_DataRe.Rows[n]["AMOUNT"].ToString()));
                                            rcv.PaymentType = DT_DataRe.Rows[n]["petition_type_name"].ToString();
                                            rcv.BathThai = Utils.ConvertMoneyToBath.ConvertMoneyToThai(rcv.AMOUNT);

                                            rcv.POSITION = Position;
                                            rcv.SigImgPathArray = (byte[])(DT_DataRe.Rows[0]["img_sign"]); // Signature_Img(ppath);
                                            rcv.QRcordPathArray = GenQRcode.CreateQRcode(ctx, DT_DataRe.Rows[0].ToCreateReceiptRequest());
                                            rcv.GUID = strGUID;
                                            rcv.BarCodeImage = Utils.BarCode.GetBarCodeData(Bar, "Tahoma", 1000, 80);
                                            rcv.BarCode = Bar;

                                            ls.Add(rcv);
                                            #endregion ยัดค่าลง Report


                                            #region สร้างไฟล์เก็บในพาร์ท

                                            for (int Y = 0; Y < PDF_Receipt.Length; Y++)
                                            {
                                                ReportDocument rpt = new ReportDocument();

                                                if ((Y == 1) && (copy == 1))
                                                {
                                                }
                                                else
                                                {
                                                    using (FileStream fs = new FileStream(report[Y], FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                                                    {
                                                        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

                                                        sw.Close();
                                                        fs.Close();
                                                        if (Y == 0)//recrive Path
                                                        {
                                                            rpt.Load(String.Format("{0}{1}RptRecive.rpt", System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, ReportFolder));
                                                            rpt.SetDataSource(ls);
                                                            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, report[Y]);
                                                        }
                                                        else// finace path
                                                        {
                                                            if (copy != 1) //1 = ver. copy
                                                            {
                                                                rpt.Load(String.Format("{0}{1}RptRecive.rpt", System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, ReportFolder));
                                                                rpt.SetDataSource(ls);
                                                                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, report[Y]);
                                                            }
                                                        }
                                                    }
                                                }
                                            }


                                            #endregion สร้างไฟล์เก็บในพาร์ท


                                            //if (copy != 1) // 1= ver. copy
                                            //{
                                            //    #region ส่งอีเมล
                                            //    //DAL.AG_PERSONAL_T personal = ctx.AG_PERSONAL_T.FirstOrDefault(p => p.ID_CARD_NO == idC);
                                            //    //if (personal != null && !String.IsNullOrWhiteSpace(personal.E_MAIL))




                                            //    string M_name = "", M_last = "", M_mail = "";
                                            //    GetDataMail(ref  M_name, ref  M_last, ref  M_mail, idC);

                                            //    if (M_mail != "" && !String.IsNullOrWhiteSpace(M_mail) && M_name != "" && M_last != "")
                                            //    {

                                            //        FileInfo fileInfo = new FileInfo(report[0]);
                                            //        if (fileInfo.Exists)
                                            //        {

                                            //            DTO.EmailSingleReceipt SingleMail = new DTO.EmailSingleReceipt();
                                            //            SingleMail.FullName = M_name + " " + M_last;
                                            //            SingleMail.Email = M_mail;
                                            //            SingleMail.ReceiptNo = DT_DataRe.Rows[n]["RECEIPT_NO"].ToString();
                                            //            SingleMail.ReceiptDate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(DT_DataRe.Rows[n]["RECEIPT_DATE"].ToString()));
                                            //            SingleMail.LicenseType = DT_DataRe.Rows[n]["petition_type_name"].ToString();
                                            //            SingleMail.totalMoney = string.Format("{0:n0}", Convert.ToDecimal(DT_DataRe.Rows[n]["AMOUNT"].ToString())) + " บาท (" + Utils.ConvertMoneyToBath.ConvertMoneyToThai(rcv.AMOUNT) + ")";
                                            //            MailReceiptSuccessHelper.SendMail(SingleMail);

                                            //        }
                                            //    }


                                            #region สร้าง hashCode
                                            String hashCode = IAS.Utils.FileObject.GetHashSHA1(report[0]);
                                            Path.Combine(PDF_Receipt[0], YMD, idC, FileNameInput);

                                            //tempGenRecive = AddStatusReceiveCompletetoDB(DR_D_T["HEAD_REQUEST_NO"].ToString(),
                                            //                                         DR_D_T["RECEIPT_BY_ID"].ToString(),
                                            //                                         String.Format(@"{0}/{1}/{2}", PDF_Receipt[0], idC, FileNameInput),
                                            //                                         DR_D_T["PAYMENT_NO"].ToString(), hashCode, Gu_id);
                                            //tempGenRecive = AddStatusReceiveCompletetoDB(DR_D_T["HEAD_REQUEST_NO"].ToString(),
                                            //                                         DR_D_T["RECEIPT_BY_ID"].ToString(),
                                            //                                         Path.Combine(PDF_Receipt[0], YMD, idC, FileNameInput),
                                            //                                         DR_D_T["PAYMENT_NO"].ToString(), hashCode, Gu_id, DT_DataRe.Rows[n]["RECEIPT_NO"].ToString(), fileSize);
                                            #endregion สร้าง hashCode


                                        }
                                    }
                                }
                                #endregion GenReceipt


                                //---------จบใบเสร็จ------------//
                            }
                        }
                        if (tempGenRecive == "Y")
                        {
                            try
                            {

                                #region ส่งเมล์เจ้าของใบสั่งจ่าย
                                string SQLOwner = "select GT.UPLOAD_BY_SESSION,TT.EMAIL,case when  COMP.NAME is not null then COMP.name else Ass.Association_name end  C_name "
                                                + "from  AG_IAS_PAYMENT_G_T GT "
                                                + "join ag_ias_personal_T TT on TT.COMP_CODE = GT.UPLOAD_BY_SESSION "
                                                + "left join vw_ias_com_code Comp on COMP.id = GT.upload_by_session "
                                                + "left join ag_ias_ASSOCIATION Ass on Ass.ASSOCIATION_CODE = GT.upload_by_session  "
                                                + "where GT.GROUP_REQUEST_NO = '" + DT_D_T.Rows[0]["GROUP_REQUEST_NO"].ToString() + "' "
                                                + "union "
                                                + "select GT.UPLOAD_BY_SESSION,TT.EMAIL, RE.FULL_NAME C_name "
                                                + "from  AG_IAS_PAYMENT_G_T GT "
                                                + "join ag_ias_personal_T TT on tt.id = GT.UPLOAD_BY_SESSION "
                                                + " join AG_IAS_SUBPAYMENT_RECEIPT RE on re.group_request_no = gt.group_request_no "
                                                + "where GT.GROUP_REQUEST_NO = '" + DT_D_T.Rows[0]["GROUP_REQUEST_NO"].ToString() + "' ";

                                #endregion ส่งเมล์เจ้าของใบสั่งจ่าย
                                //string sql = "select p.email  C_mail ,case when  COMP.NAME is not null then COMP.name else Ass.Association_name end  C_name  " +
                                //           " from ag_ias_payment_g_T T " +
                                //           "  left join vw_ias_com_code Comp on COMP.id = T.upload_by_session " +
                                //            " left join ag_ias_ASSOCIATION Ass on Ass.ASSOCIATION_CODE = T.upload_by_session " +
                                //           "  left join ag_ias_personal_t P on p.comp_code = T.upload_by_session " +
                                //           "  left join ag_ias_users U on U.is_active = 'A'  " +
                                //           "   where u.user_id=p.id and t.group_request_no ='" + DR_D_T["group_request_no"].ToString() + "' ";
                                OracleDB oraa = new OracleDB();
                                DataTable personalUser = oraa.GetDataSet(SQLOwner).Tables[0];
                                if (personalUser.Rows.Count > 0)
                                {


                                    FileInfo fileInfoo = new FileInfo(report[0]);
                                    if (fileInfoo.Exists)
                                    {
                                        _emailReceiptTaskings.Add(new EmailReceiptTaskingRequest()
                                        {
                                            //Email = personal.E_MAIL,
                                            ReciveNo = DT_D_T.Rows[0]["RECEIPT_NO"].ToString(),
                                            FullName = DT_D_T.Rows[0]["FIRSTNAME"].ToString(),
                                            IDCard = DT_D_T.Rows[0]["ID_CARD_NO"].ToString(),
                                            PettionTypeName = DT_D_T.Rows[0]["petition_type_name"].ToString(),
                                            Receipt = fileInfoo
                                        });
                                    }
                                    string[] E_mail = new string[personalUser.Rows.Count];
                                    for (int P = 0; P < personalUser.Rows.Count; P++)
                                    {
                                        E_mail[P] = personalUser.Rows[P]["EMAIL"].ToString();
                                    }
                                    MailReceiptSuccessHelper.SendMail(personalUser.Rows[0]["C_name"].ToString(), E_mail, _emailReceiptTaskings);
                                    //ส่งเมล์คนอยู่ใต้สังกัด
                                    if (personalUser.Rows[0]["UPLOAD_BY_SESSION"].ToString().Length < 4)
                                    {
                                        FileInfo fileInfooC = new FileInfo(report[1]);
                                        if (fileInfoo.Exists)
                                        {
                                            _emailReceiptTaskings.Add(new EmailReceiptTaskingRequest()
                                            {
                                                //Email = personal.E_MAIL,
                                                ReciveNo = DT_D_T.Rows[0]["RECEIPT_NO"].ToString(),
                                                FullName = DT_D_T.Rows[0]["FIRSTNAME"].ToString(),
                                                IDCard = DT_D_T.Rows[0]["ID_CARD_NO"].ToString(),
                                                PettionTypeName = DT_D_T.Rows[0]["petition_type_name"].ToString(),
                                                Receipt = fileInfooC
                                            });
                                        }
                                        string SQLSub = "select TT.E_MAIL EMAIL from  AG_PERSONAL_T TT "
                                                      + "join AG_IAS_SUBPAYMENT_RECEIPT RE on re.id_card_no = tt.id_card_no "
                                                      + "where re.id_card_no = '" + DT_D_T.Rows[0]["ID_CARD_NO"].ToString() + "' "
                                                      + "union "
                                                      + "select TT.EMAIL EMAIL from  ag_ias_personal_T TT "
                                                      + "join AG_IAS_SUBPAYMENT_RECEIPT RE on re.id_card_no = tt.id_card_no "
                                                      + "where re.id_card_no = '" + DT_D_T.Rows[0]["ID_CARD_NO"].ToString() + "' ";

                                        DataTable SubUser = oraa.GetDataSet(SQLSub).Tables[0];
                                        if (SubUser.Rows.Count > 0)
                                        {
                                            string[] E_mailSub = new string[SubUser.Rows.Count];
                                            for (int P = 0; P < SubUser.Rows.Count; P++)
                                            {
                                                E_mailSub[P] = personalUser.Rows[P]["EMAIL"].ToString();
                                            }
                                            MailReceiptSuccessHelper.SendMail(DT_D_T.Rows[0]["FIRSTNAME"].ToString(), E_mailSub, _emailReceiptTaskings);

                                        }


                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                                res.ErrorMsg = "พบข้อผิดพลาดในการส่งอีเมล์ให้ผู้เกี่ยวข้อง";
                                LoggerFactory.CreateLog().Fatal("PaymentService_GenReceiptAll", res.ErrorMsg);
                            }
                        }
                        else
                        {
                            res.ErrorMsg = "สร้างใบเสร็จไม่สำเร็จ";
                            LoggerFactory.CreateLog().Fatal("PaymentService_GetDataFromSub_D_T", "AddStatusReceiveCompletetoDB update");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetDataFromSub_D_T", ex);
            }
            if (nasDrive != null)
            {
                nasDrive.Dispose();
            }
            return res;
        }

        private void GetDataMail(ref string M_name, ref string M_last, ref string M_mail, string IDcard)
        {
            try
            {

                string st_tbl = " select  " +
                                    " V.NAME || ' ' ||  A.NAMES name,A.LASTNAME last ,a.EMAIL MAIL" +
                                    "   from AG_IAS_PERSONAL_T A ,VW_IAS_TITLE_NAME V ,ag_ias_users U" +
                                    "      where " +
                                    "  A.ID_CARD_NO = '" + IDcard + "' and v.id=a.pre_name_code  and u.is_active='A' and u.user_id = a.id ";
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(st_tbl);
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    M_mail = dt.Rows[0]["MAIL"].ToString();
                    M_name = dt.Rows[0]["NAME"].ToString();
                    M_last = dt.Rows[0]["last"].ToString();

                }
                else
                {
                    M_mail = "";
                    M_name = "";
                    M_last = "";
                }


            }
            catch
            {
            }
        }

        public DTO.ResponseMessage<bool> CheckHolidayDate(string strDate)
        {
            DTO.ResponseMessage<bool> IsHoliday = new ResponseMessage<bool>();
            DateTime date_Date = Convert.ToDateTime(strDate);
            try
            {

                if ((date_Date.DayOfWeek == DayOfWeek.Saturday) || (date_Date.DayOfWeek == DayOfWeek.Sunday) || (!checkEventDate(strDate)))
                {
                    IsHoliday.ResultMessage = true; //เป็นวันหยุด
                }
                else
                {
                    IsHoliday.ResultMessage = false;//ไม่ใช่วันหยุด
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("PaymentService_CheckHolidayDate", ex);
            }
            return IsHoliday;
        }


        private bool checkEventDate(string strDate) //01/01/2004
        {
            Boolean pass = false;
            DateTime tempDate = Convert.ToDateTime(strDate);
            string D = Convert.ToString(tempDate.Day);
            string M = Convert.ToString(tempDate.Month);
            string Y = Convert.ToString(tempDate.Year);
            strDate = (D + "/" + M + "/" + Y);
            //int Iyear = Convert.ToInt16(strDate.Substring(6, 4));
            //Iyear = Iyear - 543;
            try
            {
                //strDate = strDate.Substring(0, 6)+Convert.ToString(Iyear);

                string sql = "select * from GBDOI.gb_holiday_r where hl_date = to_date('" + strDate + "','dd/mm/yyyy')";
                OracleDB ora = new OracleDB();
                DataSet DS = ora.GetDataSet(sql);
                if (DS.Tables[0].Rows.Count == 0)
                {
                    pass = true;
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("PaymentService_checkEventDate", ex);
            }
            return pass;
        }

        private string GetReceiptDate(string strDate)
        {
            Boolean pass = false;
            DateTime date_Date = Convert.ToDateTime(strDate);
            try
            {
                while (!pass)
                {
                    if ((date_Date.DayOfWeek == DayOfWeek.Saturday) || (date_Date.DayOfWeek == DayOfWeek.Sunday))
                    {
                        date_Date = date_Date.AddDays(1);
                    }
                    else
                    {
                        if (checkEventDate(Convert.ToString(date_Date)))// false = stop date true = work date
                        {
                            pass = true;
                        }
                        else
                        {
                            date_Date = date_Date.AddDays(1);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("PaymentService_GetReceiptDate", ex);
            }
            return string.Format("{0:dd/MM/yyyy}", date_Date);
        }


        public byte[] Signature_Img(string ImgPath)
        {

            string _netDrive = ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"];
            string _userNetDrive = ConfigurationManager.AppSettings["USER_NET_DRIVE"];
            string _passNetDrive = ConfigurationManager.AppSettings["PASS_NET_DRIVE"];
            byte[] buffer = null;
            using (NASDrive nasDrive = new NASDrive(_netDrive, _userNetDrive, _passNetDrive))
            {
                Stream fileStream = new FileStream(_netDrive + "" + ImgPath, FileMode.Open);
                buffer = new Byte[fileStream.Length + 1];
                BinaryReader br = new BinaryReader(fileStream);
                buffer = br.ReadBytes(Convert.ToInt32((fileStream.Length)));
                br.Close();
            }
            return buffer;
        }


        public string
         AddStatusReceiveCompletetoDB(string H_req_no, string UID, string strPath, string IDcard, string hashingCode, Guid GU_ID, string receiveNo,Int64 Filesize)
        {
            var res = new DTO.ResponseService<DataSet>();
            string R = "N";

            try
            {
                string tmp = string.Empty;

                tmp = " update ag_ias_subpayment_receipt set GEN_STATUS = '" + DTO.SubPayment_D_T_Status.C.ToString() + "',RECEIVE_PATH = '" + strPath + "', "
                           + " GUID = '" + GU_ID + "' , HASHING_CODE = '" + hashingCode + "',FILE_SIZE = '"+Filesize+"' "
                           + " where  "
                           + " head_request_no = '" + H_req_no + "' and GEN_STATUS ='" + DTO.SubPayment_D_T_Status.A.ToString() + "' and RECEIPT_BY_ID = '" + UID + "' and PAYMENT_NO  = '" + IDcard + "' "
                           + " and RECEIPT_NO = '" + receiveNo + "' ";

                OracleDB ora = new OracleDB();
                ora.GetDataSet(tmp);
                R = "Y";
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_AddStatusReceiveCompletetoDB", ex);
            }
            return R;
        }

        public DTO.ResponseService<DTO.ReferanceNumber> CreateReferanceNumber()
        {
            DTO.ResponseService<DTO.ReferanceNumber> res = new DTO.ResponseService<DTO.ReferanceNumber>();
            res.DataResponse = new DTO.ReferanceNumber();
            ReferanceNumber refer = GenReferanceNumber.CreateReferanceNumber();
            res.DataResponse.Ref1 = refer.FirstNumber;
            res.DataResponse.Ref2 = refer.SecondNumber;

            return res;
        }


        public DTO.ResponseService<IEnumerable<DateTime>> GetLicenseGroupRequestPaid(RangeDateRequest request)
        {
            DTO.ResponseService<IEnumerable<DateTime>> response = new ResponseService<IEnumerable<DateTime>>();


            IList<AG_IAS_PAYMENT_G_T> paymentGTs = new List<AG_IAS_PAYMENT_G_T>();
            foreach (AG_IAS_PAYMENT_G_T payment in ctx.AG_IAS_PAYMENT_G_T.
                                                        Where(a => a.STATUS == "P"
                                                            && (a.PAYMENT_DATE >= request.StartDate
                                                            && a.PAYMENT_DATE <= request.EndDate)).ToList())
            {
                IEnumerable<AG_IAS_SUBPAYMENT_H_T> subHs = ctx.AG_IAS_SUBPAYMENT_H_T.Where(w => w.GROUP_REQUEST_NO == payment.GROUP_REQUEST_NO && (w.PETITION_TYPE_CODE == "11"
                                                                                            || w.PETITION_TYPE_CODE == "13"
                                                                                            || w.PETITION_TYPE_CODE == "14"
                                                                                            || w.PETITION_TYPE_CODE == "15"
                                                                                            || w.PETITION_TYPE_CODE == "16"
                                                                                            || w.PETITION_TYPE_CODE == "17"
                                                                                            || w.PETITION_TYPE_CODE == "18"));
                if (subHs != null && subHs.Count() > 0)
                {
                    paymentGTs.Add(payment);
                }

            }


            var paymentGroups = paymentGTs.GroupBy(p => p.PAYMENT_DATE);

            IList<DateTime> importDates = new List<DateTime>();

            foreach (var item in paymentGroups)
            {
                importDates.Add((DateTime)item.Key);
            }
            response.DataResponse = importDates;
            return response;
        }

        public DTO.ResponseMessage<bool> AG_GET_JURISTIC_COMPCODE(string comcode, string licenseType, out string VCOMP_LICENSE_NO, out string VCOMP_LICENSE_TYPE)
        {
            string licenseno = string.Empty;
            string licenseT = string.Empty;
            var res = new DTO.ResponseMessage<bool>();
            try
            {

                if (licenseType == "03")
                {
                    //AG_AGENT_LICENSE_JURISTIC_T cur03 = base.ctx.AG_AGENT_LICENSE_JURISTIC_T.Where(a => a.LICENSE_NO.StartsWith("ซ") && a.COMPANY_CODE == comcode).FirstOrDefault();
                    //.OrderBy(b => b.LICENSE_NO.Substring(-4)).FirstOrDefault(a => a.LICENSE_NO.StartsWith("ซ") && a.COMPANY_CODE == comcode);

                    AG_AGENT_LICENSE_JURISTIC_T cur03 = base.ctx.AG_AGENT_LICENSE_JURISTIC_T.OrderBy(b => b.LICENSE_NO.Substring(-4)).FirstOrDefault(a => a.LICENSE_NO.StartsWith("ซ") && a.COMPANY_CODE == comcode);
                    if (cur03 != null)
                    {
                        licenseno = cur03.LICENSE_NO;
                        licenseT = cur03.LICENSE_TYPE_CODE;
                    }
                }
                else if (licenseType == "04")
                {
                    AG_AGENT_LICENSE_JURISTIC_T cur04 = base.ctx.AG_AGENT_LICENSE_JURISTIC_T.OrderBy(b => b.LICENSE_NO.Substring(-4)).FirstOrDefault(a => a.LICENSE_NO.StartsWith("ว") && a.COMPANY_CODE == comcode);
                    if (cur04 != null)
                    {
                        licenseno = cur04.LICENSE_NO;
                        licenseT = cur04.LICENSE_TYPE_CODE;
                    }
                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_AG_GET_JURISTIC_COMPCODE", ex);
            }
            VCOMP_LICENSE_NO = licenseno;
            VCOMP_LICENSE_TYPE = licenseT;
            return res;
        }
        public string AG_GET_TRADE_NO(string P_code)
        {
            string trade_no = string.Empty;
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                VW_AS_COMPANY_T com = ctx.VW_AS_COMPANY_T.FirstOrDefault(a => a.COMP_CODE == P_code);
                if (com != null)
                {
                    trade_no = com.TRADE_NO;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_AG_GET_TRADE_NO", ex);
            }
            return trade_no;
        }

        public DTO.ResponseService<DataSet> GetPaymentExpireDay()
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = "SELECT ID, DESCRIPTION, PAYMENT_EXPIRE_DAY FROM AG_IAS_PAYMENT_EXPIRE_DAY";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentExpireDay", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdatePaymentExpireDay(List<DTO.ConfigPaymentExpireDay> ls, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            try
            {
                foreach (var item in ls)
                {
                    var temp = base.ctx.AG_IAS_PAYMENT_EXPIRE_DAY.Where(s => s.ID == item.ID).First();
                    temp.PAYMENT_EXPIRE_DAY = item.PAYMENT_EXPIRE_DAY;
                    temp.UPDATED_BY = userProfile.Name;
                    temp.UPDATED_DATE = DateTime.Now;
                    ((ObjectSet<AG_IAS_PAYMENT_EXPIRE_DAY>)base.ctx.AG_IAS_PAYMENT_EXPIRE_DAY).ApplyCurrentValues(temp);
                }
                base.ctx.SaveChanges();

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_UpdatePaymentExpireDay", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetRcvHisDetail(string RcvId, string EventCode, string st_num, string en_num)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                string sql = " select id_card,user_name,rcvevent,create_date,member_type,run_no from ( " +
                             " select distinct  rcvhis.id_card_no id_card,tn.name || ' ' || pt.names || ' ' || pt.lastname user_name " +
                             " ,rcvhis.RCV_EVENT  rcvevent,rcvhis.created_date create_date,mt.member_name member_type ,row_number() over (order by rcvhis.id_card_no) run_no" +
                             "  from ag_ias_receipt_history rcvhis,ag_ias_personal_t pt,ag_ias_member_type mt,vw_ias_title_name tn  " +
                             "  where " +
                             "  rcvhis.id_card_no = pt.id_card_no " +
                             "  and pt.member_type = mt.member_code " +
                             "  and pt.pre_name_code= tn.id  " +
                             "  and rcvhis.receipt_no = '" + RcvId + "' " +
                             "  and rcvhis.RCV_EVENT like '" + EventCode + "%' " +
                             " )A where A.run_no between " + st_num + " and " + en_num + "  order by run_no";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetRcvHisDetail", ex);
            }
            return res;
        }


        public DTO.ResponseMessage<bool> UpdateCountDownload(string UserId, object FileName, string Event)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var his = (from x in ctx.AG_IAS_RECEIPT_HISTORY select x).Count();
                int hhis = his.ToInt() + 1;


                //    string strInsert = "insert into ag_ias_receipt_history(rcv_his_id,id_card_no,receipt_no,rcv_event,created_by,created_date,updated_by,updated_date) " +
                //                        " values ('"+hhis+"','"+UserId+"','"+FileName+"'";
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("PaymentService_UpdateCountDownload", ex);
            }
            return res;
        }
        public DTO.ResponseService<DataSet> GetPaymentLicenseAppove(string petitonType, string IdCard, string groupNo, DateTime startDate, DateTime endDate,
          string FirstName, string LastName, string CountPage, int pageNo, int recordPerPage)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();
                StringBuilder sb3 = new StringBuilder();
                StringBuilder sbPet16 = new StringBuilder();
                StringBuilder sbPet16_1 = new StringBuilder();
                StringBuilder sbPettion16 = new StringBuilder();
                string sql = string.Empty;
                string critRecNo = string.Empty;
                string condition2 = string.Empty;
                string conditionPet16 = string.Empty;
                if (startDate != null && endDate != null)
                {
                    sb.Append(string.Format(
                              " AND ( gt.CREATED_DATE BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd'))  ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(endDate).ToString_yyyyMMdd()));
                }
                if (IdCard != "")
                {
                    sb.Append(GetCriteria("AND dt.id_card_no like   '{0}%'  ", IdCard));
                    sbPettion16.Append(GetCriteria("AND ld2.ID_CARD_NO like   '{0}%'  ", IdCard));
                }
                if (groupNo != "")
                {
                    sb.Append(GetCriteria("AND gt.group_request_no like   '{0}%'  ", groupNo));
                }
                if (petitonType != "00")
                {
                    sb.Append(GetCriteria("AND ht.petition_type_code =  '{0}'  ", petitonType));
                    sbPettion16.Append(GetCriteria(" AND lh2.PETITION_TYPE_CODE =  '{0}' ", petitonType));
                }
                if (FirstName != "")
                {
                    sb2.Append(GetCriteria(" dt.Names like   '{0}%'  ", FirstName));
                    sbPet16.Append(GetCriteria(" ld2.Names like   '{0}%'  ", FirstName));
                }
                if (LastName != "")
                {
                    sb3.Append(GetCriteria(" dt.LastName like  '{0}%' ", LastName));
                    sbPet16_1.Append(GetCriteria(" ld2.LastName like  '{0}%' ", LastName));
                }

                if (FirstName != "" && LastName != "")
                {
                    condition2 = "and" + sb2.ToString() + "and" + sb3.ToString();
                    conditionPet16 = "and" + sbPet16.ToString() + "and" + sbPet16_1.ToString();
                }
                else if (FirstName != "" && LastName == "")
                {
                    condition2 = "and" + sb2.ToString();
                    conditionPet16 = "and" + sbPet16.ToString();
                }
                else if (FirstName == "" && LastName != "")
                {
                    condition2 = "and" + sb3.ToString();
                    conditionPet16 = "and" + sbPet16_1.ToString();
                }



                string condition = sb.ToString();
                string Pet16Main = sbPettion16.ToString();
                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;

                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                if (CountPage == "Y")
                {

                    sql = "SELECT Count(*) as rowcount FROM ( "
                        + "select ('')license_no,ht.group_request_no  "
                        + ",p.petition_type_name,l.license_type_name,dt.Names,dt.LastName, "
                        + "dt.id_card_no, gt.payment_date,dt.PAYMENT_NO,dt.HEAD_REQUEST_NO "
                        + "from (select d1.id_card_no, d1.payment_no, d1.head_request_no, d1.License_no, d1.license_type_code,d1.UPLOAD_GROUP_NO,d1.SEQ_NO, d2.NAMES, d2.LASTNAME "
                        + "from  agdoi.ag_ias_subpayment_d_t  d1 "
                        + "left join agdoi.ag_personal_t d2 on d1.id_card_no=d2.id_card_no where d1.RECORD_STATUS = 'P') dt, "
                        + "(select * from  agdoi.ag_ias_subpayment_h_t where  petition_type_code not in ('01'))  ht "
                        + ", (select * from  agdoi.ag_ias_payment_g_t ) gt ,  "
                        + "agdoi.ag_ias_petition_type_r p , agdoi.ag_license_type_r l,agdoi.AG_IAS_LICENSE_D ld  "
                        + "where dt.head_request_no = ht.head_request_no "
                        + "and ht.group_request_no = gt.group_request_no "
                        + "and  ht.petition_type_code = p.petition_type_code "
                         + "and ld.UPLOAD_GROUP_NO = dt.UPLOAD_GROUP_NO "
                        + "and ld.SEQ_NO = dt.SEQ_NO "
                        + "and ld.oic_approved_date is null "
                        + "and  l.license_type_code = dt.license_type_code "
                        + "and ht.group_request_no is not null " + condition + condition2

                        + "union "
                        + "select distinct(ld2.license_no)license_no, ('')group_request_no  ,p2.petition_type_name petition_type_name,l2.license_type_name license_type_name "
                        + ",ld2.Names Names,ld2.LastName LastName, ld2.id_card_no id_card_no, (null)payment_date,('')PAYMENT_NO,('')HEAD_REQUEST_NO "
                        + "from AGDOI.ag_ias_license_d ld2, agdoi.ag_ias_petition_type_r p2,AGDOI.ag_ias_license_h lh2,agdoi.ag_license_type_r l2 "
                        + "where lh2.petition_type_code = p2.petition_type_code "
                        + "and   l2.license_type_code = lh2.license_type_code "
                        + "and lh2.upload_group_no = ld2.upload_group_no "
                        + "and lh2.petition_type_code in ('16','11') "
                        + "and ld2.fees = 0 " + Pet16Main + conditionPet16
                        + "and ld2.oic_approved_date is null) ";
                }
                else
                {
                    sql = "SELECT * FROM( "
                        + "select ('')license_no,group_request_no,  "
                        + "petition_type_name,license_type_name,Names,LastName, "
                        + "id_card_no, payment_date,PAYMENT_NO,HEAD_REQUEST_NO,petition_type_code, "
                        + "LICENSE_TYPE_CODE,renew_time,UPLOAD_GROUP_NO,SEQ_NO,ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO FROM( "
                        + "select ('')license_no, ht.group_request_no  "
                        + ",p.petition_type_name,l.license_type_name,dt.Names,dt.LastName, "
                        + "dt.id_card_no, gt.payment_date,dt.PAYMENT_NO,dt.HEAD_REQUEST_NO,ht.petition_type_code,dt.LICENSE_TYPE_CODE,(select max(re.renew_time) from agdoi.ag_license_renew_t re where re.license_no = ld.license_no)renew_time,ld.UPLOAD_GROUP_NO,ld.SEQ_NO "
                        + "from (select d1.id_card_no, d1.payment_no, d1.head_request_no, d1.License_no, d1.license_type_code,d1.UPLOAD_GROUP_NO,d1.SEQ_NO, d2.NAMES, d2.LASTNAME "
                        + "from  agdoi.ag_ias_subpayment_d_t  d1 "
                        + "left join agdoi.ag_personal_t d2 on d1.id_card_no=d2.id_card_no where d1.RECORD_STATUS = 'P') dt, "
                        + "(select * from  agdoi.ag_ias_subpayment_h_t where  petition_type_code not in ('01'))  ht "
                        + ", (select * from  agdoi.ag_ias_payment_g_t ) gt ,  "
                        + "agdoi.ag_ias_petition_type_r p , agdoi.ag_license_type_r l,agdoi.AG_IAS_LICENSE_D ld  "
                        + "where dt.head_request_no = ht.head_request_no "
                        + "and ht.group_request_no = gt.group_request_no "
                        + "and  ht.petition_type_code = p.petition_type_code "
                        + "and  l.license_type_code = dt.license_type_code "
                        + "and ld.UPLOAD_GROUP_NO = dt.UPLOAD_GROUP_NO "
                          + "and ld.oic_approved_date is null "
                        + "and ld.SEQ_NO = dt.SEQ_NO "
                        + "and ht.group_request_no is not null " + condition + condition2
                        // + "and dt.license_no is null "
                        + "union "
                        + "select distinct(ld2.license_no)license_no, ('')group_request_no  ,p2.petition_type_name petition_type_name,l2.license_type_name license_type_name "
                        + ",ld2.Names Names,ld2.LastName LastName, ld2.id_card_no id_card_no, (null)payment_date,('')PAYMENT_NO,('')HEAD_REQUEST_NO,lh2.petition_type_code, "
                        + "lh2.license_type_code,(null)renew_time,ld2.UPLOAD_GROUP_NO,ld2.SEQ_NO  "
                        + "from AGDOI.ag_ias_license_d ld2, agdoi.ag_ias_petition_type_r p2,AGDOI.ag_ias_license_h lh2,agdoi.ag_license_type_r l2 "
                        + "where lh2.petition_type_code = p2.petition_type_code "
                        + "and   l2.license_type_code = lh2.license_type_code "
                        + "and lh2.upload_group_no = ld2.upload_group_no "
                        + "and lh2.petition_type_code in ('16','11') "
                        + "and ld2.fees = 0 " + Pet16Main + conditionPet16
                        + "and ld2.oic_approved_date is null))A " + critRecNo;
                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentLicenseAppove", ex);
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetPaymentExamDetail(string GroupRequestNo)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;
                sql = "select d.TESTING_NO,d.EXAM_PLACE_CODE,d.APPLICANT_CODE,d.amount "
                    //+ "(select g.group_amount from AG_IAS_PAYMENT_G_T g where g.GROUP_REQUEST_NO =  '" + GroupRequestNo + "')GroupAmt "
                    + "from AG_IAS_SUBPAYMENT_D_T d,AG_IAS_SUBPAYMENT_H_T h "
                    + "where d.HEAD_REQUEST_NO = h.HEAD_REQUEST_NO "
                    + "and h.GROUP_REQUEST_NO = '" + GroupRequestNo + "' ";

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentExamDetail", ex);
            }
            return res;
        }
        public DTO.ResponseService<DataSet> GetConfigViewFile()
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select ITEM_VALUE from agdoi.AG_IAS_CONFIG "
                           + "where ID = '01' and GROUP_CODE = 'RC001' ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetConfigViewFile", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetNonPayment(string compCode,
                               DateTime? startDate, DateTime? toDate,
                               string paymentCode, DateTime? startExamDate, DateTime? EndExamDate, string licenseType, string testingNo, string para, int pageNo, int recordPerPage, string Totalrecoad)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder sbAdd = new StringBuilder();
                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                if (startDate != null && toDate != null)
                {
                    sb.Append(string.Format(
                              " (gt.CREATED_DATE BETWEEN " +
                              "    to_date('{0}','yyyymmdd') AND " +
                              "    to_date('{1}','yyyymmdd')) AND ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd()));
                }
                string condition = sb.ToString();

                string crit = condition.Length > 4
                                ? " AND " + condition.Substring(0, condition.Length - 4)
                                : condition;

                string tmp = string.Empty;

                if (startExamDate != null && EndExamDate != null)
                {
                    sbAdd.Append(string.Format(
                            " AND exr.testing_date BETWEEN " +
                            "    to_date('{0}','yyyymmdd') AND " +
                            "    to_date('{1}','yyyymmdd')  ",
                            Convert.ToDateTime(startExamDate).ToString_yyyyMMdd(),
                            Convert.ToDateTime(EndExamDate).ToString_yyyyMMdd()));
                }
                else if (startExamDate != null)
                {
                    sbAdd.Append(GetCriteria("AND exr.testing_date >=   to_date('{0}','yyyymmdd')  ", Convert.ToDateTime(startExamDate).ToString_yyyyMMdd()));
                }
                else if (EndExamDate != null)
                {
                    sbAdd.Append(GetCriteria("AND exr.testing_date <=   to_date('{0}','yyyymmdd')  ", Convert.ToDateTime(EndExamDate).ToString_yyyyMMdd()));
                }

                if (licenseType != "")
                {
                    sbAdd.Append(GetCriteria("AND dt.LICENSE_TYPE_CODE like  '{0}%'  ", licenseType));
                }
                if (testingNo != "")
                {
                    sbAdd.Append(GetCriteria("AND dt.TESTING_NO like  '{0}%'  ", testingNo));
                }
                string condition2 = sbAdd.ToString();
                string caseAdd = string.Empty;
                if (para != "5" && Totalrecoad == "Y")
                {
                    if (para == "dt.id_card_no")
                    {

                        tmp = "SELECT COUNT(*)rowcount FROM " +
                            "(select distinct( gt.group_request_no),ht.petition_type_code,exr.EXAM_OWNER accso, " +
                            "('') comCode,gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE " +
                            "from ag_ias_subpayment_d_t DT,AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_h_t HT " +
                            ",ag_exam_license_r exr,ag_exam_place_group_r exg,AG_IAS_USERS AUser  " +
                            "WHERE " + para + " = '" + compCode + "' " +
                            "and gt.group_request_no like  '" + paymentCode + "%' AND " +
                            "ht.group_request_no = gt.group_request_no " +
                            "and ht.head_request_no = dt.head_request_no " +
                            "and ht.petition_type_code = '01' " +
                            "and dt.testing_no <> ' ' " +
                            "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                            // "and substr(dt.exam_place_code,3,3) = exg.exam_place_group_code " +
                            "and dt.testing_no = exr.testing_no " +
                            "and dt.exam_place_code = exr.exam_place_code " +
                            "and AUser.USER_NAME = dt.id_card_no " +
                            "and AUser.USER_ID <> gt.UPLOAD_BY_SESSION " + condition2 + crit +
                            //"and substr(dt.exam_place_code,3,3) <> gt.upload_by_session " +
                            //"and dt.company_code <> gt.upload_by_session " + 
                            "union " +
                            "select distinct( gt.group_request_no),ht.petition_type_code, " +
                            "(select lh.approve_compcode from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no " +
                            "and lh.approve_compcode <> gt.upload_by_session) accso, " +
                            "lh.comp_code  comCode " +
                            ",gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE " +
                            "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,AG_IAS_USERS AUser,ag_ias_license_h lh,ag_exam_license_r exr   " +
                            "where  gt.group_request_no = ht.group_request_no " +
                            "and ht.head_request_no = dt.head_request_no " +
                            "and AUser.USER_NAME = dt.id_card_no " +
                            "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                            "and AUser.USER_ID <> gt.UPLOAD_BY_SESSION " +
                            "and lh.upload_group_no = dt.upload_group_no " +
                            "and dt.testing_no = exr.testing_no " + condition2 +
                            "and dt.exam_place_code = exr.exam_place_code " +
                            // "and lh.comp_code <> gt.upload_by_session " +
                            "and ht.petition_type_code <> '01' " + crit +
                            "and " + para + " = '" + compCode + "' " +
                            "and gt.group_request_no like  '" + paymentCode + "%' ) ";

                    }
                    else if (para == "Insurance")
                    {
                        tmp = "SELECT COUNT(*)rowcount FROM " +
                                             "(select distinct( gt.group_request_no),ht.petition_type_code,exr.EXAM_OWNER assoc, " +
                                             "('') comCode,gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE " +
                                             "from AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_d_t DT,ag_ias_subpayment_h_t HT " +
                                             ",ag_exam_license_r exr,ag_exam_place_group_r exg  " +

                                             "where gt.group_request_no like  '" + paymentCode + "%' AND " +
                                             "ht.group_request_no = gt.group_request_no " +
                                             "and ht.head_request_no = dt.head_request_no " +
                                             "and ht.petition_type_code = '01' " +
                                             "and dt.testing_no <> ' ' " +
                                             "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                              "and gt.upload_by_session <> '" + compCode + "' " +
                            //"and substr(dt.exam_place_code,3,3) = exg.exam_place_group_code " +
                                             "and dt.testing_no = exr.testing_no " +
                                             "and dt.exam_place_code = exr.exam_place_code " + condition2 +
                                             "and dt.company_code = '" + compCode + "' " +
                            //"and substr(dt.exam_place_code,3,3) <> gt.upload_by_session " +
                                             "and dt.company_code <> gt.upload_by_session " + crit +
                                             "union " +
                                             "select distinct( gt.group_request_no),ht.petition_type_code, " +
                                             "(select lh.approve_compcode from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no " +
                                             ") assoc, " +
                                             "lh.comp_code  comCode  " +
                                             ",gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE " +
                                             "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,ag_ias_license_h lh,ag_exam_license_r exr   " +
                                             "where  gt.group_request_no = ht.group_request_no " +
                                             "and ht.head_request_no = dt.head_request_no " +
                                             "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                                 "and lh.upload_group_no = dt.upload_group_no " +
                                                  "and gt.upload_by_session <> '" + compCode + "' " +
                                              "and lh.comp_code <> gt.upload_by_session " + condition2 +
                                                  "and dt.testing_no = exr.testing_no " +
                                             "and dt.exam_place_code = exr.exam_place_code " +
                                             "and ht.petition_type_code <> '01' " + crit +
                                             "and lh.comp_code = '" + compCode + "' " +
                                             "and gt.group_request_no like  '" + paymentCode + "%' ) ";
                    }
                    else if (para == "Association")
                    {
                        tmp = "SELECT COUNT(*)rowcount FROM " +
                                               "(select distinct( gt.group_request_no),ht.petition_type_code,exr.EXAM_OWNER assoc, " +
                                               "('') comCode,gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE " +
                                               "from AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_d_t DT,ag_ias_subpayment_h_t HT " +
                                               ",ag_exam_license_r exr,ag_exam_place_group_r exg  " +

                                               "where gt.group_request_no like  '" + paymentCode + "%' AND " +
                                               "ht.group_request_no = gt.group_request_no " +
                                               "and ht.head_request_no = dt.head_request_no " +
                                                "and gt.upload_by_session <> '" + compCode + "' " +
                                               "and ht.petition_type_code = '01' " +
                                               "and dt.testing_no <> ' ' " +
                            //"and substr(dt.exam_place_code,3,3) = exg.exam_place_group_code " +
                                               "and dt.testing_no = exr.testing_no " +
                                               "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                               "and dt.exam_place_code = exr.exam_place_code " + condition2 +
                                               "and substr(dt.exam_place_code,3,3) = '" + compCode + "' " +
                                               "and substr(dt.exam_place_code,3,3) <> gt.upload_by_session " + crit +
                            // "and dt.company_code <> gt.upload_by_session " 
                                               "union " +
                                               "select distinct( gt.group_request_no),ht.petition_type_code, " +
                                               "(select lh.approve_compcode from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no " +
                                               "and lh.approve_compcode <> gt.upload_by_session and lh.approve_compcode = '" + compCode + "' ) assoc, " +
                                               "lh.comp_code  comCode " +
                                               ",gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE " +
                                               "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,ag_ias_license_h lh,ag_exam_license_r exr   " +
                                               "where  gt.group_request_no = ht.group_request_no " +
                                                   "and dt.testing_no = exr.testing_no " +
                                                   "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                                    "and gt.upload_by_session <> '" + compCode + "' " +
                                             "and dt.exam_place_code = exr.exam_place_code " +
                                               "and ht.head_request_no = dt.head_request_no " + condition2 +
                                               "and lh.upload_group_no = dt.upload_group_no " +
                                              "and lh.comp_code <> gt.upload_by_session " +
                                               "and ht.petition_type_code <> '01' " + crit +

                                               "and gt.group_request_no like  '" + paymentCode + "%' ) ";


                    }
                }
                else if (para != "5" && Totalrecoad == "N")
                {
                    if (para == "dt.id_card_no")
                    {
                        tmp = "SELECT * FROM(SELECT group_request_no,petition_type_code,accso,comCode,upload_by_session,status,PAYMENT_DATE,EXPIRATION_DATE,GROUP_DATE, " +
                            "ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO FROM( " +
                            "select distinct( gt.group_request_no),ht.petition_type_code,exr.EXAM_OWNER accso, " +
                            "('') comCode,gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE,gt.GROUP_DATE " +
                            "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,AG_IAS_USERS AUser " +
                            ",ag_exam_license_r exr,ag_exam_place_group_r exg " +
                            "where  gt.group_request_no = ht.group_request_no " +
                            "and ht.head_request_no = dt.head_request_no " +
                            "and ht.petition_type_code = '01' " +
                            "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                            "and dt.testing_no <> ' ' " + condition2 +
                            // "and substr(dt.exam_place_code,3,3) = exg.exam_place_group_code " +
                            "and dt.testing_no = exr.testing_no " +
                            "and dt.exam_place_code = exr.exam_place_code " +
                               "and AUser.USER_NAME = dt.id_card_no " +
                            "and AUser.USER_ID <> gt.UPLOAD_BY_SESSION " + crit +
                            //"and substr(dt.exam_place_code,3,3) <> gt.upload_by_session " +
                            //"and dt.company_code <> gt.upload_by_session " + 
                            "and " + para + " = '" + compCode + "' " +
                            "and gt.group_request_no like  '" + paymentCode + "%' " +
                            "union " +
                            "select distinct( gt.group_request_no),ht.petition_type_code, " +
                            "(select lh.approve_compcode from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no " +
                            "and lh.approve_compcode <> gt.upload_by_session) accso, " +
                            "lh.comp_code  comCode " +
                            ",gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE,gt.GROUP_DATE " +
                            "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,AG_IAS_USERS AUser,ag_ias_license_h lh,ag_exam_license_r exr  " +
                            "where  gt.group_request_no = ht.group_request_no " +
                              "and AUser.USER_NAME = dt.id_card_no " + condition2 +
                            "and AUser.USER_ID <> gt.UPLOAD_BY_SESSION " +
                            "and ht.head_request_no = dt.head_request_no " +
                            "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                 "and dt.testing_no = exr.testing_no " +
                                             "and dt.exam_place_code = exr.exam_place_code " +
                                        "and lh.upload_group_no = dt.upload_group_no " +
                            //  "and lh.comp_code <> gt.upload_by_session " +
                            "and ht.petition_type_code <> '01' " +
                            "and " + para + " = '" + compCode + "' " +
                            "and gt.group_request_no like  '" + paymentCode + "%'))A " + critRecNo;


                    }
                    else if (para == "Insurance")
                    {
                        tmp = "SELECT * FROM(SELECT group_request_no,petition_type_code,assoc,comCode,upload_by_session,status,PAYMENT_DATE,EXPIRATION_DATE,GROUP_DATE, " +
                            "ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO FROM " +
                                             "(select distinct( gt.group_request_no),ht.petition_type_code,exr.EXAM_OWNER assoc, " +
                                             "('') comCode,gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE,gt.GROUP_DATE " +
                                             "from AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_d_t DT,ag_ias_subpayment_h_t HT " +
                                             ",ag_exam_license_r exr,ag_exam_place_group_r exg  " +

                                             "where gt.group_request_no like  '" + paymentCode + "%' AND " +
                                             "ht.group_request_no = gt.group_request_no " +
                                             "and ht.head_request_no = dt.head_request_no " +
                                              "and gt.upload_by_session <> '" + compCode + "' " +
                                             "and ht.petition_type_code = '01' " +
                                             "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                             "and dt.testing_no <> ' ' " + condition2 +
                            //"and substr(dt.exam_place_code,3,3) = exg.exam_place_group_code " +
                                             "and dt.testing_no = exr.testing_no " +
                                             "and dt.exam_place_code = exr.exam_place_code " +
                                             "and dt.company_code = '" + compCode + "' " +
                            //"and substr(dt.exam_place_code,3,3) <> gt.upload_by_session " +
                                             "and dt.company_code <> gt.upload_by_session " + crit +
                                             "union " +
                                             "select distinct( gt.group_request_no),ht.petition_type_code, " +
                                             "(select lh.approve_compcode from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no " +
                                             ") assoc, " +
                                             "lh.comp_code  comCode  " +
                                             ",gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE,gt.GROUP_DATE " +
                                             "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,ag_ias_license_h lh,ag_exam_license_r exr   " +
                                             "where  gt.group_request_no = ht.group_request_no " +
                                             "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                             "and ht.head_request_no = dt.head_request_no " + condition2 +
                                                  "and dt.testing_no = exr.testing_no " +
                                                   "and gt.upload_by_session <> '" + compCode + "' " +
                                             "and dt.exam_place_code = exr.exam_place_code " +
                                             "and ht.petition_type_code <> '01' " + crit +
                                                         "and lh.upload_group_no = dt.upload_group_no " +
                                              "and lh.comp_code <> gt.upload_by_session " +
                                             "and gt.group_request_no like  '" + paymentCode + "%' ))A " + critRecNo;
                    }
                    else if (para == "Association")
                    {
                        tmp = "SELECT * FROM(SELECT group_request_no,petition_type_code,assoc,comCode,upload_by_session,status,PAYMENT_DATE,EXPIRATION_DATE,GROUP_DATE, " +
                  "ROW_NUMBER() OVER (ORDER BY group_request_no) RUN_NO FROM " +
                                               "(select distinct( gt.group_request_no),ht.petition_type_code,exr.EXAM_OWNER assoc, " +
                                               "('') comCode,gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE,gt.GROUP_DATE " +
                                               "from AG_IAS_PAYMENT_G_T gt,ag_ias_subpayment_d_t DT,ag_ias_subpayment_h_t HT " +
                                               ",ag_exam_license_r exr,ag_exam_place_group_r exg  " +

                                               "where gt.group_request_no like  '" + paymentCode + "%' AND " +
                                               "ht.group_request_no = gt.group_request_no " +
                                               "and ht.head_request_no = dt.head_request_no " +
                                               "and ht.petition_type_code = '01' " +
                                               "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                               "and dt.testing_no <> ' ' " +
                            //   "and substr(dt.exam_place_code,3,3) = exg.exam_place_group_code " +
                                               "and dt.testing_no = exr.testing_no " + condition2 +
                                               "and dt.exam_place_code = exr.exam_place_code " +
                                               "and gt.upload_by_session <> '" + compCode + "' " +
                                               "and substr(dt.exam_place_code,3,3) = '" + compCode + "' " +
                                               "and substr(dt.exam_place_code,3,3) <> gt.upload_by_session " + crit +
                            //"and dt.company_code <> gt.upload_by_session " + 
                                               "union " +
                                               "select distinct( gt.group_request_no),ht.petition_type_code, " +
                                               "(select lh.approve_compcode from ag_ias_license_h lh where lh.upload_group_no = dt.upload_group_no " +
                                               "and lh.approve_compcode <> gt.upload_by_session and lh.approve_compcode = '" + compCode + "' ) assoc, " +
                                               "lh.comp_code  comCode  " +
                                               ",gt.upload_by_session,gt.status,dt.PAYMENT_DATE,gt.EXPIRATION_DATE,gt.GROUP_DATE " +
                                               "from ag_ias_payment_g_t gt,ag_ias_subpayment_h_t ht,ag_ias_subpayment_d_t dt,ag_ias_license_h lh,ag_exam_license_r exr   " +
                                               "where  gt.group_request_no = ht.group_request_no " +
                                                    "and dt.testing_no = exr.testing_no " +
                                                    "and (gt.STATUS NOT IN ('X') or gt.STATUS is null) " +
                                             "and dt.exam_place_code = exr.exam_place_code " +
                                               "and ht.head_request_no = dt.head_request_no " + condition2 +
                                               "and ht.petition_type_code <> '01' " + crit +
                                               "and lh.upload_group_no = dt.upload_group_no " +
                                                "and gt.upload_by_session <> '" + compCode + "' " +
                                              "and lh.comp_code <> gt.upload_by_session " +
                                               "and gt.group_request_no like  '" + paymentCode + "%' ))A " + critRecNo;


                    }
                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(tmp);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetNonPayment", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetConfigViewBillPayment()
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select ITEM_VALUE from agdoi.AG_IAS_CONFIG "
                           + "where ID = '08' and GROUP_CODE = 'SP002' ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetConfigViewBillPayment", ex);
            }
            return res;
        }


        public DTO.ResponseService<DTO.GetPaymentByRangeResponse> GetPaymentByRange(DTO.GetPaymentByRangeRequest request)
        {
            DTO.ResponseService<DTO.GetPaymentByRangeResponse> response = new ResponseService<GetPaymentByRangeResponse>();
            response.DataResponse = new DTO.GetPaymentByRangeResponse();

            try
            {
                //var result = ctx.AG_IAS_PAYMENT_G_T.Where(a => String.Compare(a.GROUP_REQUEST_NO, request.PaymentStarting) >= 0 && String.Compare(a.GROUP_REQUEST_NO, request.PaymentEnding) <= 0
                //    && a.GROUP_AMOUNT == request.Amount && String.IsNullOrEmpty(a.STATUS));
                var result = ctx.AG_IAS_PAYMENT_G_T.Where(a => String.Compare(a.GROUP_REQUEST_NO, request.PaymentStarting) >= 0
                   && String.Compare(a.GROUP_REQUEST_NO, request.PaymentEnding) <= 0
                   && (a.STATUS != "P" || String.IsNullOrEmpty(a.STATUS)));
                if (result != null && result.ToList().Count > 0)
                {
                    response.DataResponse.PaymentByRangeResults = result.ToList().ConvertToPaymentByRangeResults();
                }
                else
                {

                    response.DataResponse.PaymentByRangeResults = new List<PaymentByRangeResult>();
                }

            }
            catch (Exception)
            {
                response.ErrorMsg = "ไม่พบข้อมูล";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetPaymentByRange", "ไม่พบข้อมูล");
            }

            return response;

        }
        public DTO.ResponseMessage<bool> CancelGroupRequestNo(string GroupRequestNo)
        {
            string sql = string.Empty;
            string sqlDT = string.Empty;
            string sqlUpdateDT = string.Empty;
            var res = new DTO.ResponseMessage<bool>();
            var resSQL = new DTO.ResponseService<DataSet>();
            try
            {
                AG_IAS_PAYMENT_G_T FlagStatus = base.ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == GroupRequestNo);
                if (FlagStatus != null)
                {
                    FlagStatus.STATUS = "X";
                    FlagStatus.UPDATED_DATE = DateTime.Now.Date;
                }

                sql = "select ht.HEAD_REQUEST_NO from agdoi.AG_IAS_SUBPAYMENT_H_T ht,agdoi.AG_IAS_PAYMENT_G_T gt "
                    + "where gt.GROUP_REQUEST_NO = ht.GROUP_REQUEST_NO "
                    + "and ht.GROUP_REQUEST_NO = '" + GroupRequestNo + "' and gt.LAST_PRINT is null ";

                OracleDB ora = new OracleDB();
                resSQL.DataResponse = ora.GetDataSet(sql);
                if (resSQL.DataResponse.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < resSQL.DataResponse.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = resSQL.DataResponse.Tables[0].Rows[i];
                        string SGroup = string.Empty;
                        SGroup = dr["HEAD_REQUEST_NO"].ToString();
                        AG_IAS_SUBPAYMENT_H_T update = base.ctx.AG_IAS_SUBPAYMENT_H_T.FirstOrDefault(a => a.HEAD_REQUEST_NO == SGroup);

                        // update.GROUP_REQUEST_NO = "";
                        update.STATUS = "X";
                        update.UPDATED_DATE = DateTime.Now.Date;
                        sqlUpdateDT = "select PAYMENT_NO,HEAD_REQUEST_NO,UPLOAD_GROUP_NO,SEQ_NO from AG_IAS_SUBPAYMENT_D_T "
                                    + "where HEAD_REQUEST_NO = '" + SGroup + "' ";
                        DataSet dsUpdateDT = ora.GetDataSet(sqlUpdateDT);
                        DataTable dtUpdateDT = dsUpdateDT.Tables[0];
                        if (dtUpdateDT.Rows.Count > 0)
                        {
                            for (int d = 0; d < dtUpdateDT.Rows.Count; d++)
                            {
                                string UploadGroup = string.Empty;
                                string SeqNo = string.Empty;
                                DataRow drUpdateDT = dtUpdateDT.Rows[d];
                                string PAYMENTNO = drUpdateDT["PAYMENT_NO"].ToString();

                                AG_IAS_SUBPAYMENT_D_T updateDT = base.ctx.AG_IAS_SUBPAYMENT_D_T.FirstOrDefault(a => a.HEAD_REQUEST_NO == SGroup && a.PAYMENT_NO == PAYMENTNO);
                                if (updateDT != null)
                                {
                                    updateDT.RECORD_STATUS = "X";

                                    UploadGroup = updateDT.UPLOAD_GROUP_NO;
                                    SeqNo = updateDT.SEQ_NO;
                                    AG_IAS_LICENSE_D updateLD = base.ctx.AG_IAS_LICENSE_D.FirstOrDefault(a => a.UPLOAD_GROUP_NO == UploadGroup && a.SEQ_NO == SeqNo);
                                    if (updateLD != null)
                                    {
                                        updateLD.HEAD_REQUEST_NO = "";
                                    }
                                }


                            }
                        }

                        #region สมัครสอบ
                        var subHead = base.ctx.AG_IAS_SUBPAYMENT_H_T
                                         .SingleOrDefault(s => s.HEAD_REQUEST_NO == SGroup && s.PETITION_TYPE_CODE == "01");
                        if (subHead != null)
                        {
                            sqlDT = "select a.TESTING_NO , a.EXAM_PLACE_CODE ,a.APPLICANT_CODE,LR.TESTING_DATE "
                                  + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,ag_applicant_t a,AG_EXAM_LICENSE_R LR "
                                  + "where d.petition_type_code = p.petition_type_code "
                                  + "and d.id_card_no = a.id_card_no "
                                  + "and d.HEAD_REQUEST_NO = '" + subHead.HEAD_REQUEST_NO + "' "
                                  + "and a.TESTING_NO = d.testing_no "
                                  + "and a.EXAM_PLACE_CODE = d.exam_place_code "
                                  + "and LR.TESTING_NO = d.testing_no "
                                  + "and LR.EXAM_PLACE_CODE = d.exam_place_code "
                                  + "and a.APPLICANT_CODE = d.applicant_code ";

                            DataSet ds2 = ora.GetDataSet(sqlDT);

                            DataTable dtDT = ds2.Tables[0];
                            if (dtDT.Rows.Count != 0)
                            {
                                for (int b = 0; b < dtDT.Rows.Count; b++)
                                {
                                    DataRow drDT = dtDT.Rows[b];
                                    string testNo = drDT["TESTING_NO"].ToString();
                                    string ExamPlaceCode = drDT["EXAM_PLACE_CODE"].ToString();
                                    Int32 AppCode = Convert.ToInt32(drDT["APPLICANT_CODE"]);
                                    var InsertApplicant = base.ctx.AG_APPLICANT_T.SingleOrDefault(a => a.TESTING_NO == testNo
                                        && a.EXAM_PLACE_CODE == ExamPlaceCode && a.APPLICANT_CODE == AppCode);
                                    if (InsertApplicant != null)
                                    {
                                        InsertApplicant.GROUP_REQUEST_NO = "";
                                        InsertApplicant.HEAD_REQUEST_NO = "";
                                    }
                                }
                                // base.ctx.SaveChanges();
                            }

                        }
                        #endregion
                    }
                }


                using (var tc = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    tc.Complete();

                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_CancelGroupRequestNo", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdatePrintGroupRequestNo(List<string> reqList)
        {


            var res = new DTO.ResponseMessage<bool>();

            try
            {

                foreach (string item in reqList)
                {
                    string[] ChkUpload = item.Split(' ');
                    string GroupRequestNo = ChkUpload[0];
                    AG_IAS_PAYMENT_G_T update = base.ctx.AG_IAS_PAYMENT_G_T.FirstOrDefault(a => a.GROUP_REQUEST_NO == GroupRequestNo);

                    update.LAST_PRINT = DateTime.Now;
                    update.UPDATED_DATE = DateTime.Now;
                }


                using (var tc = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    tc.Complete();

                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_UpdatePrintGroupRequestNo", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet>
    GetSubPaymentByGenPaymentForm(string groupReqNo, string CountRecord, int pageNo, int recordPerPage)
        {
            string sql = string.Empty;
            StringBuilder sb = new StringBuilder();

            string critRecNo = string.Empty;
            critRecNo = pageNo == 0
                            ? ""
                            : "WHERE A.RUN_NO BETWEEN " +
                                     pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                     pageNo.ToRowNumber(recordPerPage).ToString();
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //string sql = "SELECT PER.NAMES  || ' ' || PER.LASTNAME FIRSTLASTNAME, " +
                //             "PT.PETITION_TYPE_NAME,AHT.PERSON_NO,AHT.SUBPAYMENT_AMOUNT,AHT.SUBPAYMENT_DATE,ADT.HEAD_REQUEST_NO " +
                //             "FROM AG_IAS_SUBPAYMENT_H_T AHT, " +
                //             "AG_IAS_SUBPAYMENT_D_T ADT, " +
                //             "AG_IAS_PERSONAL_T PER, " +
                //             "AG_PETITION_TYPE_R PT " +
                //             "WHERE AHT.HEAD_REQUEST_NO = ADT.HEAD_REQUEST_NO AND " +
                //             "PER.ID_CARD_NO = ADT.ID_CARD_NO AND " +
                //             "AHT.PETITION_TYPE_CODE = PT.PETITION_TYPE_CODE AND " +
                //             "AHT.HEAD_REQUEST_NO = '" + groupReqNo.ClearQuote() + "' ";
                if (CountRecord == "Y")
                {
                    sql = "SELECT Count(*) rowcount FROM( "
                        + "SELECT PT.PETITION_TYPE_NAME,AHT.SUBPAYMENT_AMOUNT,AHT.SUBPAYMENT_DATE,AHT.HEAD_REQUEST_NO,gt.created_date "
                        + "FROM AG_IAS_SUBPAYMENT_H_T AHT,AG_IAS_PAYMENT_G_T GT,AG_PETITION_TYPE_R PT "
                        + "WHERE AHT.GROUP_REQUEST_NO = GT.GROUP_REQUEST_NO AND "
                        + "AHT.PETITION_TYPE_CODE = PT.PETITION_TYPE_CODE AND "
                        + "AHT.GROUP_REQUEST_NO = '" + groupReqNo + "') ";


                }
                else
                {
                    //sql = "SELECT * FROM(select h.head_request_no,h.created_date,h.subpayment_date,h.subpayment_amount,h.person_no "
                    //       + ",p.petition_type_name, "
                    //       + "ROW_NUMBER() OVER (ORDER BY h.head_request_no) RUN_NO "
                    //       + "from ag_ias_subpayment_h_t h,ag_petition_type_r p "
                    //       + "where h.petition_type_code = p.petition_type_code "
                    //       + "and h.GROUP_REQUEST_NO = '" + groupReqNo + "')A " + critRecNo;

                    sql = "SELECT * FROM( "
                        + "SELECT PT.PETITION_TYPE_NAME,AHT.SUBPAYMENT_AMOUNT,AHT.SUBPAYMENT_DATE,AHT.HEAD_REQUEST_NO,gt.created_date, "
                        + "ROW_NUMBER() OVER (ORDER BY AHT.head_request_no) RUN_NO "
                        + "FROM AG_IAS_SUBPAYMENT_H_T AHT,AG_IAS_PAYMENT_G_T GT,AG_PETITION_TYPE_R PT "
                        + "WHERE AHT.GROUP_REQUEST_NO = GT.GROUP_REQUEST_NO AND "
                        + "AHT.PETITION_TYPE_CODE = PT.PETITION_TYPE_CODE AND "
                        + "AHT.GROUP_REQUEST_NO = '" + groupReqNo + "')A " + critRecNo;

                }
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetSubPaymentByHeaderRequestNo", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet>
       GetSubGroupDetail(string paymentType, string UploadGroupNo, int pageNo, int recordPerPage, string CountTotalRecord)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;
                string crit = string.Empty;
                string critRecNo = string.Empty;

                string AddCondition = string.Empty;
                string AddComCode = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();

                paymentType = (paymentType == "00") ? "%" : paymentType;
                switch (CountTotalRecord)
                {
                    case "Y":
                        #region นับจำนวนทั้งหมด  ประเภทใบสั่งจ่าย
                        switch (paymentType)
                        {
                            case "01"://สมัครสอบ

                                sql = "         SELECT COUNT(*) rowcount  " +
                                      "         FROM    AG_APPLICANT_T AP, VW_IAS_TITLE_NAME TT, AG_EXAM_LICENSE_R LR " +
                                      "         WHERE   AP.PRE_NAME_CODE = TT.ID AND " +
                                      "                 AP.TESTING_NO = LR.TESTING_NO AND " +
                                      "                 AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND  " +
                                      "                 AP.HEAD_REQUEST_NO IS NULL AND " +
                                      " AP.UPLOAD_GROUP_NO = '" + UploadGroupNo + "' ";


                                break;


                            default:

                                if (paymentType == "16")
                                {
                                    AddCondition = "and ld.FEES <> 0  ";
                                }


                                sql = " SELECT COUNT(*) rowcount "
                                      + " from ag_ias_license_h lh, ag_petition_type_r pt,ag_ias_license_d ld "
                                      + " where PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE  "
                                      + "  and lh.petition_type_code like'" + paymentType + "' "
                                      + " and  lh.upload_group_no = ld.upload_group_no and ld.head_request_no is  null  "
                                      + " and lh.approved_doc ='Y'  " + AddCondition
                                      + "and lh.UPLOAD_GROUP_NO = '" + UploadGroupNo + "' "
                                      + " order by ld.license_no ";
                                break;
                        }

                        break;
                        #endregion
                    default:
                        #region query ธรรมดา
                        //ใบสั่งจ่ายค่าสมัครสอบ
                        switch (paymentType)
                        {
                            case "01":


                                sql = "SELECT * " +
                                      "FROM ( " +
                                      "         SELECT 'ค่าสมัครสอบ' AS  PAYMENT_TYPE_NAME, " +
                                      "                 AP.ID_CARD_NO, TT.NAME ||' '|| AP.NAMES FIRST_NAME, " +
                                      "                 AP.LASTNAME, LR.TESTING_DATE, LR.TESTING_NO, AP.APPLICANT_CODE, AP.EXAM_PLACE_CODE, " +
                                      "                 AP.INSUR_COMP_CODE , LR.LICENSE_TYPE_CODE , " +
                                      "                 ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO, AP.APPLICANT_CODE ASC) RUN_NO " +
                                      "         FROM    AG_APPLICANT_T AP, VW_IAS_TITLE_NAME TT, AG_EXAM_LICENSE_R LR " +
                                      "         WHERE   AP.PRE_NAME_CODE = TT.ID AND " +
                                      "                 AP.TESTING_NO = LR.TESTING_NO AND " +
                                      "                 AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND " +
                                      "                 (AP.RECORD_STATUS is null or AP.RECORD_STATUS != 'X') AND " + // add by milk 070214
                                      "                 AP.HEAD_REQUEST_NO IS NULL AND " +
                                      " AP.UPLOAD_GROUP_NO = '" + UploadGroupNo + "' " +
                                      ") A " + critRecNo;


                                break;

                            default:


                                if (paymentType == "16")
                                {
                                    AddCondition = "and ld.FEES <> 0  ";
                                }
                                sql = "select * from ( "
                                      + " select ld.license_no,case ld.renew_times when '0' then '' else ld.renew_times end as renew_times ,ld.license_expire_date EXPIRE_DATE, ld.license_date as RENEW_DATE, "
                                      + " ld.title_name || ' ' || ld.names as T_name, ld.lastname T_LAST, ld.upload_group_no,ld.seq_no "
                                      + " ,ROW_NUMBER() over (order by ld.license_no,ld.seq_no) run_no "
                                      + " from ag_ias_license_h lh, ag_petition_type_r pt,ag_ias_license_d ld "
                                      + " where PT.PETITION_TYPE_CODE = LH.PETITION_TYPE_CODE "
                                      + "  and lh.petition_type_code like'" + paymentType + "' "
                                      + " and  lh.upload_group_no = ld.upload_group_no and ld.head_request_no is  null "
                                      + " and lh.approved_doc ='Y'  " + AddCondition
                                      + "and lh.UPLOAD_GROUP_NO = '" + UploadGroupNo + "' "
                                      + " order by ld.license_no)"
                                      + " A " + critRecNo;


                                break;
                        }

                        #endregion
                        break;
                }

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetSubGroup", ex);
            }
            return res;
        }




        #region AutoCancleApplicant
        //public DTO.ResponseMessage<bool> Auto_CancleApplicant()
        //{
        //    DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
        //    try
        //    {
        //        var Auto = ctx.AG_IAS_CONFIG.FirstOrDefault(x => x.ID == "10" && x.ITEM_VALUE == "1" && x.GROUP_CODE == "AP001");
        //        if (Auto != null)
        //            res.ResultMessage = true;
        //        else
        //            res.ResultMessage = false;
        //    }
        //    catch
        //    { 
        //    }
        //    return res;
        //}

        #endregion AutoCancleApplicant

        public DTO.ResponseMessage<bool> SavePaymentNoFile(DateTime Date_Time, int CIT, int KTB, string userID)
        {
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            try
            {
                if (Date_Time != null)
                {
                    int countFile = 0;
                    var CCount = ctx.AG_IAS_PAYMENT_FILE.Count();
                    if (CCount.ToInt() == 0)
                        countFile = 1;
                    else
                        countFile = CCount.ToInt() + 1;

                    var FileData = ctx.AG_IAS_PAYMENT_FILE.FirstOrDefault(x => x.FILE_DATE == Date_Time);
                    if (FileData != null)//ยังไม่มีข้อมูลของวันนี้เลย
                    {
                        FileData.ID = countFile;
                        FileData.FILE_DATE = Date_Time;
                        if (CIT != null) // 0=no data in that date // 1 = have data // null = no set value(Defult)
                        {
                            FileData.CITYBANK = CIT.ToString();
                        }

                        if (KTB != null)
                        {
                            FileData.KTB = KTB.ToString();
                        }
                        FileData.USER_ID = userID;
                        FileData.USER_DATE = DateTime.Now;
                        FileData.UPDATE_ID = userID;
                        FileData.UPDATE_DATE = DateTime.Now;
                        // FileData.COUNT = 1;
                    }
                    else //มีข้อมูลของวันนี้แล้ว
                    {
                        if (CIT != null) // 0=no data in that date // 1 = have data // null = no set value(Defult)
                        {
                            FileData.CITYBANK = CIT.ToString();
                        }

                        if (KTB != null)
                        {
                            FileData.KTB = KTB.ToString();
                        }
                        FileData.UPDATE_ID = userID;
                        FileData.UPDATE_DATE = DateTime.Now;
                        //FileData.COUNT = FileData.COUNT + 1;
                    }
                    ctx.SaveChanges();
                    res.ResultMessage = true;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_SavePaymentNoFile", ex);
                res.ResultMessage = false;
            }
            return res;
        }

        public DTO.ResponseService<DataSet>
         GetEndOfPay(string compCode,
                            DateTime? startDate, DateTime? toDate,
                            string typeEnd, string CountRecord, int pageNo, int recordPerPage)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                //StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;
                StringBuilder sb = new StringBuilder();

                string TempWhere = "";
                switch (typeEnd)
                {
                    case "1":
                        TempWhere = " ktb = null and citybank = null and  ";
                        break;
                    case "2":
                        TempWhere = " ktb != null and citybank != null and  ";
                        break;
                    default:
                        break;

                }


                string critRecNo = string.Empty;
                critRecNo = pageNo == 0
                                ? ""
                                : "WHERE A.RUN_NO BETWEEN " +
                                         pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +
                                         pageNo.ToRowNumber(recordPerPage).ToString();
                if (CountRecord == "Y")
                {
                    tmp = string.Format(
                              " SELECT Count(*) as rowcount FROM(SELECT  file_date ShowDate,ktb,citybank CITIBANK,update_date LASTDATE, update_id LASTWHO   " +
                              " ,ROW_NUMBER() OVER (ORDER BY file_date) RUN_NO " +
                              " FROM ag_ias_payment_file " +
                              "WHERE " + TempWhere +
                              " file_date BETWEEN " +
                              "to_date('{0}','yyyymmdd') AND " +
                              "to_date('{1}','yyyymmdd') " +
                              "  )A   ",
                              Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                              Convert.ToDateTime(toDate).ToString_yyyyMMdd());
                }
                else
                {
                    tmp = string.Format(
                                 " SELECT * FROM(SELECT  file_date ShowDate,ktb,citybank CITIBANK,update_date LASTDATE, update_id LASTWHO   " +
                                 " ,ROW_NUMBER() OVER (ORDER BY file_date) RUN_NO " +
                                 " FROM ag_ias_payment_file " +
                                 "WHERE " + TempWhere +
                                 " file_date BETWEEN " +
                                 "to_date('{0}','yyyymmdd') AND " +
                                 "to_date('{1}','yyyymmdd') " +
                                 " )A   ",
                                 Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                                 Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + critRecNo;

                }
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GenPaymentNumberTable", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> SetSubGroupSingleApplicant(List<DTO.OrderInvoice> subGroups,
                                              string userId, out string groupHeaderNo)
        {
            var res = new DTO.ResponseService<string>();
            groupHeaderNo = string.Empty;

            if (subGroups.Count == 0)
            {
                res.ErrorMsg = Resources.errorPaymentService_001;
                return res;
            }

            try
            {

                string headReqNo = OracleDB.GetGenAutoId();
                string Seq = "1";

                DateTime? expDate = null;
                int iCount = 0;
                decimal fee = 0;
                decimal total = 0;
                Int16 CountPerson = 0;
                string PaymentType = string.Empty;
                AG_IAS_SUBPAYMENT_D_T detail = null;
                AG_IAS_SUBPAYMENT_H_T head = null;
                List<DateTime?> lsDateExp = new List<DateTime?>();
                List<DateTime?> LsTestingDate = new List<DateTime?>();
                foreach (DTO.OrderInvoice sg in subGroups)
                {


                    var ent = base.ctx.AG_APPLICANT_T
                                      .Where(w => w.APPLICANT_CODE == sg.ApplicantCode &&
                                                  w.TESTING_NO == sg.TESTING_NO &&
                                                  w.EXAM_PLACE_CODE == sg.EXAM_PLACE_CODE)
                                      .SingleOrDefault();
                    var entExamLicense = base.ctx.AG_EXAM_LICENSE_R
                                    .Where(w => w.TESTING_NO == sg.TESTING_NO &&
                                                  w.EXAM_PLACE_CODE == sg.EXAM_PLACE_CODE)
                                    .SingleOrDefault();
                    
                    if (ent.APPLY_DATE > entExamLicense.TESTING_DATE)
                    {
                        expDate = Convert.ToDateTime(entExamLicense.TESTING_DATE).AddDays(3);
                        if (entExamLicense != null)
                        {
                            LsTestingDate.Add(expDate);
                        }
                    }
                    else
                    {
                        if (entExamLicense != null)
                        {
                            LsTestingDate.Add(entExamLicense.TESTING_DATE);
                        }
                    }
                    if (fee == 0)
                    {
                        var pt = base.ctx.AG_PETITION_TYPE_R
                                     .Where(w => w.PETITION_TYPE_CODE == sg.PaymentType)
                                     .FirstOrDefault();
                        if (pt != null)
                        {
                            fee = pt.FEE == null ? 0m : pt.FEE.Value;

                        }
                        else
                        {
                            throw new ApplicationException("dkdjfkdfjdlfjdkf");
                        }
                    }

                    ent.HEAD_REQUEST_NO = headReqNo;
                    detail = new AG_IAS_SUBPAYMENT_D_T
                    {
                        PAYMENT_NO = (++iCount).ToString("0000"),
                        HEAD_REQUEST_NO = headReqNo,
                        //PAYMENT_DATE = DateTime.Now.Date,
                        ID_CARD_NO = ent.ID_CARD_NO,
                        AMOUNT = fee,
                        USER_ID = userId,
                        USER_DATE = DateTime.Now.Date,
                        TESTING_NO = ent.TESTING_NO,
                        COMPANY_CODE = ent.INSUR_COMP_CODE,
                        PETITION_TYPE_CODE = sg.PaymentType,
                        APPLICANT_CODE = ent.APPLICANT_CODE,
                        EXAM_PLACE_CODE = ent.EXAM_PLACE_CODE,
                        LICENSE_TYPE_CODE = entExamLicense.LICENSE_TYPE_CODE,
                        RECORD_STATUS = DTO.SubPayment_D_T_Status.W.ToString(),
                        SEQ_OF_SUBGROUP = sg.RUN_NO,
                    };
                    total += fee;
                    base.ctx.AG_IAS_SUBPAYMENT_D_T.AddObject(detail);
                }
                head = new AG_IAS_SUBPAYMENT_H_T
                {
                    HEAD_REQUEST_NO = headReqNo,
                    PETITION_TYPE_CODE = "01",
                    PERSON_NO = Convert.ToInt16(subGroups.Count()),
                    SUBPAYMENT_AMOUNT = total,
                    SUBPAYMENT_DATE = DateTime.Now.Date,
                    CREATED_BY = userId,
                    CREATED_DATE = DateTime.Now.Date,
                    UPDATED_BY = userId,
                    UPDATED_DATE = DateTime.Now.Date,
                    UPLOAD_BY_SESSION = userId,
                    DATE_EXP = LsTestingDate.Min(),
                    SEQ_OF_GROUP = Seq,
                };
                base.ctx.AG_IAS_SUBPAYMENT_H_T.AddObject(head);

                using (TransactionScope tc = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    tc.Complete();

                    // groupHeaderNo = headReqNo;
                }
                // res.DataResponse = groupHeaderNo;
                string sqlDT = string.Empty;
                ReferanceNumber referanceNumber = GenReferanceNumber.CreateReferanceNumber();
                groupHeaderNo = referanceNumber.FirstNumber;
                var subHead = base.ctx.AG_IAS_SUBPAYMENT_H_T
                                     .SingleOrDefault(s => s.HEAD_REQUEST_NO == headReqNo && s.PETITION_TYPE_CODE == "01");
                if (subHead != null)
                {
                    subHead.GROUP_REQUEST_NO = groupHeaderNo;
                }
                sqlDT = "select a.TESTING_NO , a.EXAM_PLACE_CODE ,a.APPLICANT_CODE,LR.TESTING_DATE "
                      + "from ag_ias_subpayment_d_t d,ag_petition_type_r p,ag_applicant_t a,AG_EXAM_LICENSE_R LR "
                      + "where d.petition_type_code = p.petition_type_code "
                      + "and d.id_card_no = a.id_card_no "
                      + "and d.HEAD_REQUEST_NO = '" + headReqNo + "' "
                      + "and a.TESTING_NO = d.testing_no "
                      + "and a.EXAM_PLACE_CODE = d.exam_place_code "
                      + "and LR.TESTING_NO = d.testing_no "
                      + "and LR.EXAM_PLACE_CODE = d.exam_place_code "
                      + "and a.APPLICANT_CODE = d.applicant_code ";
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sqlDT);

                DataTable dtDT = ds.Tables[0];
                if (dtDT.Rows.Count != 0)
                {
                    for (int i = 0; i < dtDT.Rows.Count; i++)
                    {
                        DataRow drDT = dtDT.Rows[i];
                        string testNo = drDT["TESTING_NO"].ToString();
                        string ExamPlaceCode = drDT["EXAM_PLACE_CODE"].ToString();
                        Int32 AppCode = Convert.ToInt32(drDT["APPLICANT_CODE"]);
                        var InsertApplicant = base.ctx.AG_APPLICANT_T.SingleOrDefault(a => a.TESTING_NO == testNo
                            && a.EXAM_PLACE_CODE == ExamPlaceCode && a.APPLICANT_CODE == AppCode);
                        InsertApplicant.GROUP_REQUEST_NO = groupHeaderNo;
                     
                    }
                    // base.ctx.SaveChanges();
                }
                #region วันตัดยอดยกเลิกผู้สมัครสอบ
                var DCbiz = new IAS.DataServices.DataCenter.DataCenterService();
                DateTime CancleSeatDate = Convert.ToDateTime(DateTime.Now.Date).AddDays(DCbiz.GetConficValueByTypeAndGroupCode("10", "AP001").DataResponse.ToInt());
                DateTime DateCancleSeat = Convert.ToDateTime((CancleSeatDate.ToString())); // By Milk
                #endregion วันตัดยอดยกเลิกผู้สมัครสอบ
                #region วันที่ ref2
                string MinDate = string.Empty;
                string dd = string.Empty;
                string mm = string.Empty;
                string yy = string.Empty;
                string[] ary = Convert.ToDateTime(DateTime.Now.Date).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US")).Split('/');
                dd = ary[0];
                mm = ary[1];
                yy = ary[2];
                MinDate = dd + mm + yy;
                #endregion
                AG_IAS_PAYMENT_G_T payment = new AG_IAS_PAYMENT_G_T
                {
                    CREATED_BY = userId,
                    CREATED_DATE = DateTime.Now.Date,
                    GROUP_AMOUNT = total,
                    GROUP_DATE = DateTime.Now.Date,
                    SUBPAYMENT_QTY = Convert.ToDecimal(Seq),
                    GROUP_REQUEST_NO = groupHeaderNo,
                    UPDATED_BY = userId,
                    UPDATED_DATE = DateTime.Now.Date,
                    REMARK = "",
                    UPLOAD_BY_SESSION = userId,
                    REF2 = MinDate,
                    EXPIRATION_DATE = DateTime.Now,
                    CANCLE_SEAT_DATE = DateCancleSeat,
                    LAST_PRINT = DateTime.Now.Date,
                };


                base.ctx.AG_IAS_PAYMENT_G_T.AddObject(payment);

                base.ctx.SaveChanges();
                res.DataResponse = groupHeaderNo;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_SetSubGroupSingleApplicant", ex);
            }
            return res;
        }





        public DTO.ResponseService<DataSet> Auto_GetApplicantNoPaymentHeadder(DateTime? startDate, DateTime? toDate)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;

                if (startDate != null && toDate != null)
                {



                    tmp = string.Format(
                        " SELECT AG.GROUP_REQUEST_NO , AG.EXPIRATION_DATE,AG.cancle_seat_date " +
                        "  FROM AG_APPLICANT_T AP, GB_PREFIX_R TT,  AG_EXAM_LICENSE_R LR,    AG_IAS_REGISTRATION_T AIR ,ag_ias_payment_g_t AG  , " +
                        "    ag_ias_subpayment_h_t HT,ag_ias_subpayment_d_t DT  " +
                        "    WHERE AP.ID_CARD_NO = AIR.ID_CARD_NO  " +
                        "    AND   AP.PRE_NAME_CODE = TT.PRE_CODE  " +
                        "    AND   AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE  " +
                        "    AND   AG.created_by = AIR.ID  " +
                        "    And   (AP.record_status is null or AP.RECORD_STATUS != 'X') " +
                        "    AND   AP.cancel_reason is null  " +
                        "    AND   ht.group_request_no =ag.group_request_no  " +
                        "    and   ap.head_request_no = ht.head_request_no  " +
                        "    and   AP.TESTING_NO = LR.TESTING_NO   " +
                        " AND    to_char(AG.cancle_seat_date) BETWEEN  " +
                        " to_date('{0}','yyyymmdd') AND    " +
                        " to_date('{1}','yyyymmdd')  " +
                        " GROUP BY AG.GROUP_REQUEST_NO, AG.EXPIRATION_DATE, AG.cancle_seat_date",
                        Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                        Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + " ";
                }

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_Auto_GetApplicantNoPaymentHeadder", ex);
            }
            return res;
        }


        public DTO.ResponseMessage<bool> Auto_CancelAppNoPay(DateTime stDate, DateTime enDate)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                DateTime PKdatetime = DateTime.Now;
                if (ScheduleEvent(PKdatetime, "TIMETIC", stDate, enDate, "START"))
                {
                    //using (TransactionScope tc = new TransactionScope())
                    //{


                    List<DTO.AppNoPay> ListAppNoPay = new List<DTO.AppNoPay>();
                    DTO.ResponseService<DataSet> DS = GetApplicantNoPaymentHeadder(stDate, enDate, "", "%", "%", 0, 0, false, true);
                    foreach (DataRow GroupNo in DS.DataResponse.Tables[0].Rows)
                    {
                        ListAppNoPay.Add(new DTO.AppNoPay
                        {
                            GroupNumber = GroupNo["GROUP_REQUEST_NO"].ToString()
                        });
                    }

                    if (ListAppNoPay.Count > 0)
                    {
                        if (CancelApplicantsHeader(ListAppNoPay, true).ResultMessage)
                        {
                            //    base.ctx.SaveChanges();
                            //    tc.Complete();
                            if (!ScheduleEvent(PKdatetime, "TIMETIC", stDate, enDate, "END"))
                            {
                                LoggerFactory.CreateLog().Fatal("PaymentService_Auto_CancelAppNoPay", "บันทึกวันที่สิ้นสุดไม่ได้");
                                res.ResultMessage = false;
                            }
                            else
                                res.ResultMessage = true;
                        }
                        else
                        {
                            LoggerFactory.CreateLog().Fatal("PaymentService_Auto_CancelAppNoPay", "ยกเลิกผู้สมัครสอบโดยยกเลิกทั้งใบสั่งจ่ายไม่ได้");
                            res.ResultMessage = false;
                        }
                    }
                    else
                    {
                        if (!ScheduleEvent(PKdatetime, "TIMETIC", stDate, enDate, "END"))
                        {
                            LoggerFactory.CreateLog().Fatal("PaymentService_Auto_CancelAppNoPay", "บันทึกวันที่สิ้นสุดไม่ได้");
                            res.ResultMessage = false;
                        }
                        else
                            res.ResultMessage = true;
                    }
                    // }

                }
                else
                {
                    LoggerFactory.CreateLog().Fatal("PaymentService_Auto_CancelAppNoPay", "บันทึกวันที่เริ่มต้นไม่ได้");
                    res.ResultMessage = false;
                }
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("PaymentService_Auto_CancelAppNoPay", ex);
            }
            return res;
        }

        public bool ScheduleEvent(DateTime PKdatetime, string Name, DateTime stDate, DateTime enDate, string actionEvent)
        {
            bool AddOK = false;
            try
            {
                if (actionEvent == "START")
                {
                    AG_IAS_SCHEDULE res = new AG_IAS_SCHEDULE
                    {
                        SCHEDULE_ACTION = PKdatetime,
                        SCHEDULE_NAME = Name,
                        SCHEDULE_START_DATE = stDate,
                    };
                    base.ctx.AG_IAS_SCHEDULE.AddObject(res);
                    base.ctx.SaveChanges();
                    AddOK = true;
                }
                else if (actionEvent == "END")
                {
                    var sch = base.ctx.AG_IAS_SCHEDULE.FirstOrDefault(x => x.SCHEDULE_ACTION == PKdatetime);
                    sch.SCHEDULE_END_DATE = enDate;
                    base.ctx.SaveChanges();
                    AddOK = true;
                }

            }
            catch (Exception ex)
            {
                AddOK = false;
                LoggerFactory.CreateLog().Fatal("PaymentService_ScheduleEvent", ex);
            }
            return AddOK;
        }
        public DTO.ResponseService<string> GetLastEndDate()
        {
            var res = new ResponseService<string>();
            var DT = base.ctx.AG_IAS_SCHEDULE.Max(x => x.SCHEDULE_END_DATE);
            if (DT == null)
                DT = DateTime.Now.AddDays(-10);
            res.DataResponse = DT.ToString();
            return res;
        }
        public DTO.ResponseService<DataSet> GetReceiptSomePay(string paymentNo, string HeadrequestNo)
        {
            var res = new DTO.ResponseService<DataSet>();
            DateTime ViewYear;
            StringBuilder whereReceipt = new StringBuilder();
            try
            {
                var Year = base.ctx.AG_IAS_CONFIG.FirstOrDefault(c => c.ID == "01");
                if (Year != null) 
                {
                    ViewYear = DateTime.Now.AddYears(- Convert.ToInt16(Year.ITEM_VALUE));
                    whereReceipt.Append(string.Format("and RECEIPT_DATE >=  to_date('{0}','yyyymmdd')  ", ViewYear.ToString_yyyyMMdd()));
                   
                }
                string sql = "select SRT.RECEIPT_NO,SRT.RECEIPT_DATE,SRT.AMOUNT,SRT.RECEIVE_PATH,SRT.HEAD_REQUEST_NO,SRT.PAYMENT_NO,GT.UPLOAD_BY_SESSION "
                           + "from agdoi.AG_IAS_SUBPAYMENT_RECEIPT SRT,AG_IAS_PAYMENT_G_T GT "
                           + "where HEAD_REQUEST_NO = '" + HeadrequestNo + "' and PAYMENT_NO = '" + paymentNo + "' and SRT.GROUP_REQUEST_NO = GT.GROUP_REQUEST_NO "
                           + whereReceipt;
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetConfigViewBillPayment", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetApplicantNoPaymentHeadder(DateTime? startDate, DateTime? toDate,
                                                                        string testingDate, string testNo, string ExamPlaceCode,
                                                                        int resultPage, int PageSize, Boolean Count, Boolean IsAuto = false)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;

                string fristCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;
                string testDate = (testingDate == "") ? string.Empty : string.Format(" and lr.testing_date = to_date('{0}','yyyymmdd') ", Convert.ToDateTime(testingDate).ToString_yyyyMMdd());
                string ID = string.Empty;
                if (startDate != null && toDate != null)
                {
                    if (Convert.ToDateTime(startDate).Date > Convert.ToDateTime(toDate).Date)
                    {
                        res.ErrorMsg = Resources.errorPaymentService_003;
                        return res;
                    }
                    if (!IsAuto)
                    {
                        #region IsAuto
                        ID = " = AIR.ID ";
                        if (!Count)
                        {
                            fristCon = " SELECT * FROM ( ";
                            midCon = "  ,AG.GROUP_AMOUNT ,ROW_NUMBER() OVER(ORDER BY AG.GROUP_REQUEST_NO) RUN_NO ";
                            lastCon = resultPage == 0
                                        ? ")"
                                        : " ,AG.GROUP_AMOUNT order by RUN_NO asc ) A  WHERE A.RUN_NO BETWEEN " +
                                                 resultPage.StartRowNumber(PageSize).ToString() + " AND " +
                                                 resultPage.ToRowNumber(PageSize).ToString() + " order by A.RUN_NO asc ";
                        }
                        else
                        {
                            fristCon = " SELECT COUNT(*) CCount from ( ";
                            midCon = " ";
                            lastCon = " ) ";
                        }
                        #endregion IsAuto
                    }
                    else
                    {
                        ID = " like '%' ";

                    }
                    tmp = string.Format(fristCon +
                      " SELECT AG.GROUP_REQUEST_NO , AG.EXPIRATION_DATE,AG.cancle_seat_date " + midCon +
                      "  FROM AG_APPLICANT_T AP, GB_PREFIX_R TT,  AG_EXAM_LICENSE_R LR,    AG_IAS_REGISTRATION_T AIR ,ag_ias_payment_g_t AG  , " +
                      "    ag_ias_subpayment_h_t HT,ag_ias_subpayment_d_t DT  " +
                      "    WHERE AP.ID_CARD_NO = AIR.ID_CARD_NO  " +
                      "    AND   AP.PRE_NAME_CODE = TT.PRE_CODE  " +
                      "    AND   AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE  " +
                      "    AND   AG.created_by  " + ID + "  " +
                      "    And   (AP.record_status is null  or ap.record_status !='X') " +
                      "    AND   AP.cancel_reason is null  and  " +
                      " ( AG.payment_date > AG.expiration_date or AG.payment_date is null  or AG.status ='Y')  " +
                      "    AND   ht.group_request_no =ag.group_request_no  " +
                      "    and   ap.head_request_no = ht.head_request_no  " +
                      "    and   AP.TESTING_NO = LR.TESTING_NO   " +
                      " " +
                      "     and ap.testing_no like '" + testNo + "' " +
                      " and ap.exam_place_code like '" + ExamPlaceCode + "' " +
                        testDate +
                      " AND    to_char(AG.EXPIRATION_DATE) BETWEEN  " +
                      " to_date('{0}','yyyymmdd') AND    " +
                      " to_date('{1}','yyyymmdd')  " +
                      " GROUP BY AG.GROUP_REQUEST_NO, AG.EXPIRATION_DATE, AG.cancle_seat_date  ",
                      Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                      Convert.ToDateTime(toDate).ToString_yyyyMMdd()) + " " + lastCon;
                }

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetApplicantNoPaymentHeadder", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetApplicantNoPayment(string testingDate,string testing_no ,string examPlace,DateTime? startDate, DateTime? toDate,
            string GroupNo, int resultPage, int PageSizeDetail, Boolean Count)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string fristCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;

                StringBuilder sb = new StringBuilder();
                string tmp = string.Empty;
                string txtTestingDate = testingDate==""? "": " and lr.testing_date =to_date('" + Convert.ToDateTime(testingDate).ToString_yyyyMMdd() +"','yyyymmdd') ";
                string txtTestingNo = testing_no ==""?  "" : " and  AP.TESTING_NO = '"+testing_no+"' ";
                string txtexamPlace = examPlace == "" ? "" : " and lr.exam_place_code = '"+examPlace+"' ";
                if (startDate != null && toDate != null)
                {
                    if (!Count)
                    {
                        fristCon = " SELECT * FROM ( ";
                        midCon = " ,ROW_NUMBER() OVER(ORDER BY ID_CARD_NO) RUN_NO ";
                        lastCon = resultPage == 0
                                    ? ""
                                    : "  order by RUN_NO asc ) A  WHERE A.RUN_NO BETWEEN " +
                                             resultPage.StartRowNumber(PageSizeDetail).ToString() + " AND " +
                                             resultPage.ToRowNumber(PageSizeDetail).ToString() + " order by A.RUN_NO asc ";
                    }
                    else
                    {
                        fristCon = " SELECT COUNT(*) CCount from ( ";
                        midCon = " ";
                        lastCon = " ) ";
                    }

                    tmp = string.Format(
                        fristCon +
                       "  select  ID_CARD_NO, FIRSTNAME, LASTNAME,TESTING_DATE,TESTING_NO, APPLICANT_CODE, "
                       + "  EXAM_PLACE_CODE,CREATED_DATE  " + midCon
                       + "   from ( "
                       + "  SELECT DISTINCT(AP.ID_CARD_NO), TT.PRE_FULL || ' ' || AP.NAMES FIRSTNAME, AP.LASTNAME, "
                       + " LR.TESTING_DATE, AP.TESTING_NO, AP.APPLICANT_CODE, AP.EXAM_PLACE_CODE,AIR.CREATED_DATE "
                       + " FROM AG_APPLICANT_T AP, GB_PREFIX_R TT,  AG_EXAM_LICENSE_R LR, "
                       + " AG_IAS_REGISTRATION_T AIR ,ag_ias_payment_g_t AG , " +
                       "  ag_ias_subpayment_h_t HT,ag_ias_subpayment_d_t DT "
                       + " WHERE AP.ID_CARD_NO = AIR.ID_CARD_NO AND "
                       + "  AP.PRE_NAME_CODE = TT.PRE_CODE AND "
                       + "  AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND "
                       + "  AG.created_by = AIR.ID And "
                       + "  ht.group_request_no = '" + GroupNo + "' and "
                        //  + "   ap.insur_comp_code = dt.company_code and ap.testing_no=dt.testing_no and "
                       + "   ap.testing_no=dt.testing_no and "
                       + "  ht.head_request_no = dt.head_request_no and "
                       + " ( AG.payment_date > AG.expiration_date or AG.payment_date is null  or AG.status ='Y')  and  "
                       + "  dt.id_card_no = ap.id_card_no and "
                       + "  (AP.record_status is null or AP.record_status != 'X') AND "
                       + "   dt.applicant_code =ap.applicant_code and dt.exam_place_code = ap.exam_place_code and "
                       + "  AP.cancel_reason is null AND "
                       + "  AP.TESTING_NO = LR.TESTING_NO and  "
                        + " (SYSDATE >= LR.TESTING_DATE or       to_date(AG.EXPIRATION_DATE) < SYSDATE) AND   "
                        + " to_char(AG.EXPIRATION_DATE) BETWEEN "
                        + "      to_date('{0}','yyyymmdd') AND "
                        + "      to_date('{1}','yyyymmdd') " + txtTestingDate + txtTestingNo + txtexamPlace
                        ,
                        Convert.ToDateTime(startDate).ToString_yyyyMMdd(),
                        Convert.ToDateTime(toDate).ToString_yyyyMMdd())
                       + " ) " + lastCon;
                }

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(tmp);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetApplicantNoPayment", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลผู้สมัครสอบที่ยังไม่ชำระเงิน
        /// </summary>
        /// <param name="applicantCode">เลขที่สอบ</param>
        /// <param name="testingNo">รหัสการสอบ</param>
        /// <param name="examPlaceCode">รหัสสถานที่สอบ</param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet>
            GetApplicantNoPayById(string applicantCode, string testingNo, string examPlaceCode)
        {
            var biz = new IAS.DataServices.Applicant.ApplicantService();
            return biz.GetApplicantById(applicantCode, testingNo, examPlaceCode);
        }


        public DTO.ResponseMessage<bool> CancelApplicantsHeader(List<DTO.AppNoPay> GroupNo, Boolean IsAuto = false)//edit 25/10/13 milk
        {
            var res = new DTO.ResponseMessage<bool>();

            if (GroupNo.Count == 0)
            {
                res.ErrorMsg = Resources.errorPaymentService_001;
                return res;
            }
            else
            {
                try
                {
                    using (TransactionScope tc = new TransactionScope())
                    {
                        string ID = " = AIR.ID  ";

                        if (IsAuto)
                            ID = " like '%' ";

                        OracleConnection oraTran = new OracleConnection(ConfigurationManager.ConnectionStrings[ConnectionFor.OraDB_Person.ToString()].ToString());
                        oraTran.Open();
                        foreach (DTO.AppNoPay nopay in GroupNo)
                        {

                            var IDsql = " SELECT DISTINCT(AP.ID_CARD_NO) , AP.TESTING_NO, AP.APPLICANT_CODE, AP.EXAM_PLACE_CODE" +
                                            " FROM AG_APPLICANT_T AP,AG_EXAM_LICENSE_R LR,  " +
                                            " AG_IAS_REGISTRATION_T AIR , ag_ias_subpayment_h_t HT, " +
                                            " ag_ias_subpayment_d_t DT, GB_PREFIX_R TT,  ag_ias_payment_g_t AG " +
                                            " WHERE AP.ID_CARD_NO = AIR.ID_CARD_NO AND   " +
                                            " AP.PRE_NAME_CODE = TT.PRE_CODE AND " +
                                            " AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND  " +
                                            " AG.created_by " + ID + " And " +
                                //" TRIM(AP.record_status) = 'X' AND   TRIM(AP.cancel_reason) = 'ไม่ชำระเงิน' AND " +
                                            " ht.group_request_no =ag.group_request_no and " +
                                            " ap.head_request_no = ht.head_request_no and " +
                                            " ht.group_request_no = '" + nopay.GroupNumber.ToString() + "' and " +
                                            " ( AG.payment_date > AG.expiration_date or AG.payment_date is null  or AG.status ='Y') and " +
                                            " ht.head_request_no = dt.head_request_no and " +
                                             " dt.id_card_no = ap.id_card_no and " +
                                            " AP.TESTING_NO = LR.TESTING_NO ";
                            OracleDB ora = new OracleDB();
                            DataSet ds = ora.GetDataSet(IDsql);
                            DataTable DT = new DataTable();
                            DT = ds.Tables[0];
                            int count = 0;

                            for (count = 0; count < DT.Rows.Count; count++)
                            {


                                int AppCode = DT.Rows[count]["APPLICANT_CODE"].ToString().ToInt();
                                string testNo = DT.Rows[count]["TESTING_NO"].ToString();
                                string place = DT.Rows[count]["EXAM_PLACE_CODE"].ToString();
                                var updateAppT = base.ctx.AG_APPLICANT_T.FirstOrDefault(x =>
                                                                x.APPLICANT_CODE == AppCode &&
                                                                x.TESTING_NO == testNo &&
                                                                x.EXAM_PLACE_CODE == place
                                                                );

                                updateAppT.RECORD_STATUS = "X";
                                updateAppT.CANCEL_REASON = "ไม่ชำระเงิน";



                                // DataSet dsN = oraTran.GetDataSet(str);
                                string TestNo = DT.Rows[count]["TESTING_NO"].ToString();
                                string ExCode = DT.Rows[count]["EXAM_PLACE_CODE"].ToString();
                                var exam = base.ctx.AG_EXAM_LICENSE_R.Where(lr =>
                                                            lr.TESTING_NO == TestNo &&
                                                            lr.EXAM_PLACE_CODE == ExCode
                                                ).FirstOrDefault();

                                exam.EXAM_APPLY = Convert.ToInt16(
                                                        exam.EXAM_APPLY != null
                                                            ? exam.EXAM_APPLY.Value - "1".ToShort()
                                                            : "0".ToShort()
                                                    );
                            }

                        }


                        base.ctx.SaveChanges();

                        tc.Complete();

                    }
                    res.ResultMessage = true;

                }
                catch (Exception ex)
                {
                    res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                    LoggerFactory.CreateLog().Fatal("PaymentService_CancelApplicantsHeader", ex);
                }

            }
            return res;
        }

        public DTO.ResponseService<PaymentNotCompleteResponse> PaymentNotComplete(PaymentNotCompleteRequest request)
        {
            DTO.ResponseService<PaymentNotCompleteResponse> response = new ResponseService<PaymentNotCompleteResponse>();
            try
            {
                PaymentNotCompleteResponse dataResponse = new PaymentNotCompleteResponse();
               
                if (request.StartDate == DateTime.MinValue && request.EndDate == DateTime.MinValue && !string.IsNullOrEmpty(request.Ref1))
                {
                    IEnumerable<AG_IAS_PAYMENT_DETAIL> datares = ctx.AG_IAS_PAYMENT_DETAIL.Where(a => (a.CUSTOMER_NO_REF1.Trim() == request.Ref1.Trim() || a.CUSTOMER_NO_REF1.Trim().StartsWith(request.Ref1.Trim())) && a.STATUS == (Int16)ImportPaymentStatus.MissingRefNo);

                    if (datares != null)
                    {
                        dataResponse.BankTransaction = datares.ConvertToBankTransactions();
                        response.DataResponse = dataResponse;
                    }
                    else
                    {
                        response.ErrorMsg = "ไม่พบข้อมูลที่ค้นหา";
                    }
                }

                else if (request.StartDate != DateTime.MinValue && request.EndDate != DateTime.MinValue && string.IsNullOrEmpty(request.Ref1))
                {
                    
                    DateTime startDate = new DateTime(request.StartDate.Year, request.StartDate.Month, request.StartDate.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day, 23, 59, 59);
                    IEnumerable<AG_IAS_PAYMENT_DETAIL> datares = ctx.AG_IAS_PAYMENT_DETAIL.Where(a => (a.PAYMENT_DATE >= startDate
                                                                     && a.PAYMENT_DATE <= endDate) && a.STATUS == (Int16)ImportPaymentStatus.MissingRefNo);
                    if (datares != null)
                    {
                        dataResponse.BankTransaction = datares.ConvertToBankTransactions();
                        response.DataResponse = dataResponse;
                    }
                    else
                    {
                        response.ErrorMsg = "ไม่พบข้อมูลที่ค้นหา";
                    }
                }
                else if (request.StartDate != DateTime.MinValue && request.EndDate != DateTime.MinValue && !string.IsNullOrEmpty(request.Ref1))
                {
                    DateTime startDate = new DateTime(request.StartDate.Year, request.StartDate.Month, request.StartDate.Day, 0, 0, 0);
                    DateTime endDate = new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day, 23, 59, 59);
                    IEnumerable<AG_IAS_PAYMENT_DETAIL> datares = ctx.AG_IAS_PAYMENT_DETAIL.Where(a => (a.PAYMENT_DATE >= startDate
                                                                     && a.PAYMENT_DATE <= endDate) && a.STATUS == (Int16)ImportPaymentStatus.MissingRefNo
                                                                     && (a.CUSTOMER_NO_REF1.Trim() == request.Ref1.Trim() || a.CUSTOMER_NO_REF1.Trim().StartsWith(request.Ref1.Trim())));
                    if (datares != null)
                    {
                        dataResponse.BankTransaction = datares.ConvertToBankTransactions();
                        response.DataResponse = dataResponse;
                    }
                    else
                    {
                        response.ErrorMsg = "ไม่พบข้อมูลที่ค้นหา";
                    }
                }
                else
                {
                    response.ErrorMsg = "ไม่พบข้อมูลที่ค้นหา";
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("PaymentNotComplete", ex);
                response.ErrorMsg = "ไม่ค้นหาข้อมุลได้ กรุณาติดเจ้าหน้าที่ ดูแลระบบ.";
            }




            return response;
        }

        #region ReportCheckFileSize
        public DTO.ResponseService<DataSet> GetCheckFileSize(string PetitionTypeName,  string StartDate, string EndDate)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                string Petition = string.Empty;
                string datedate = string.Empty;
                string FileSize = string.Empty;
                if (StartDate == "" && EndDate == "")
                {
                    datedate = "";
                }
                else
                {
                    StartDate = Convert.ToDateTime(StartDate).AddYears(-543).ToShortDateString();
                    EndDate = Convert.ToDateTime(EndDate).AddYears(-543).ToShortDateString();

                    datedate = " and trunc(receipt_date) BETWEEN to_date('" + StartDate + "','dd/MM/yyyy') AND to_date('" + EndDate + "','dd/MM/yyyy') ";

                }
                if (PetitionTypeName == "ทั้งหมด")
                {
                    Petition = "";
                }
                else
                {
                    Petition += " and petition_type_name='" + PetitionTypeName + "' ";
                }
                FileSize += " where file_Size is not null and petition_type_name is not null";
                string sql = "select * from( "
                            +"select petition_type_name,count(petition_type_name) as countPetiyion,sum(File_size) as FileSize "
                            +"from( "
                            +"select petition_type_name,receipt_date,file_size from AG_IAS_SUBPAYMENT_RECEIPT "
                            + FileSize
                            + datedate
                            + Petition
                            +") "
                            +"group by petition_type_name)";
                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;

        }


        #endregion

        public DTO.ResponseService<DataSet> GetDownloadReceiptHistory(string idCard, string petitionTypeCode, string firstName, string lastName)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sqlCondition = new StringBuilder();
                sqlCondition.Append(GetCriteria(" and A.ID_CARD_NO LIKE '{0}%' ", idCard));
                sqlCondition.Append(GetCriteria(" and A.PETITION_TYPE_NAME = (select PETITION_TYPE_NAME from AG_PETITION_TYPE_R where PETITION_TYPE_CODE ='{0}') ", petitionTypeCode));
                if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName))
                    sqlCondition.Append(string.Format(" and A.FLNAME like '%{0}%{1}%' ", firstName, lastName));

                string sqlQry = string.Format(
                                @"select * from(
                                  select ROWNUM as NUM, A.* from (
                                    select SR.PETITION_TYPE_NAME, SR.RECEIPT_NO, SUBSTR(SR.FULL_NAME,INSTR(SR.FULL_NAME,' ')+1) FLNAME,
                                           SR.ID_CARD_NO, SR.PAYMENT_DATE, SR.RECEIPT_DATE ORDER_DATE, SR.AMOUNT, SR.DOWNLOAD_TIMES COUNTRECEIV
                                    from AG_IAS_SUBPAYMENT_RECEIPT SR
                                    order by DOWNLOAD_TIMES desc
                                  )A where ROWNUM <= 10 {0}
                                )A
                                inner join (
                                  select RH.RECEIPT_NO, P.NAMES, P.LASTNAME, RH.CREATED_DATE
                                  from AG_IAS_RECEIPT_HISTORY RH, AG_IAS_PERSONAL_T P
                                  where RH.CREATED_BY = P.ID
                                )B on A.RECEIPT_NO = B.RECEIPT_NO
                                order by NUM ", sqlCondition);
                OracleDB db = new OracleDB();
                DataSet ds = db.GetDataSet(sqlQry);

                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_GetDownloadReceiptHistory", ex);
            }
            return res;
        }
        public DTO.ResponseService<DataSet> getGroupDetailLicense(string group_reuest)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {

                string sql = "SELECT  d.PETITION_TYPE_CODE,g.GROUP_REQUEST_NO,a.petition_type_name as BillName,g.UPLOAD_BY_SESSION,d.AMOUNT "
                           + "FROM AG_IAS_PAYMENT_G_T g,AG_IAS_SUBPAYMENT_H_T h,ag_petition_type_r a ,AG_IAS_SUBPAYMENT_D_T d "
                         + "WHERE g.GROUP_REQUEST_NO = '" + group_reuest + "' "
                         + "and d.petition_type_code = a.petition_type_code "
                         + "and g.group_request_no = h.group_request_no "
                         + " and h.HEAD_REQUEST_NO = d.HEAD_REQUEST_NO "
                         + " order by d.petition_type_code ";

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("PaymentService_getGroupDetailLicense", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetRecriptByHeadRequestNoAndPaymentNo(string HeadNo, string PaymentNo)
        {
            var ls = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "  select  rownum,rec.receipt_no,rec.receipt_date,rec.amount from ag_ias_subpayment_receipt rec where REC.PAYMENT_NO = '" + PaymentNo + "' and rec.head_request_no ='" + HeadNo + "' order by rec.receipt_date ";
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                ls.DataResponse = ds;
            }
            catch(Exception ex)
            {
                ls.ErrorMsg = ex.Message;
                LoggerFactory.CreateLog().Fatal("PaymentService_GetRecriptByHeadRequestNoAndPaymentNo", ex);
            }
            return ls;
        }
    }

}
