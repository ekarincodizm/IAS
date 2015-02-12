using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.DAL;
using IAS.Utils;
using System.Data;
using System.Transactions;
using System.Text.RegularExpressions;
using Oracle.DataAccess.Client;
using System.Globalization;
using IAS.DataServices.Properties;
using System.ServiceModel.Activation;
using IAS.Common.Logging;
using IAS.DataServices.Helpers;
using IAS.DTO.FileService;
using System.IO;
using IAS.DataServices.FileManager;
using IAS.DTO.Exams;
using IAS.DataServices.Exam.Helpers;
namespace IAS.DataServices.Exam
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ExamService : AbstractService, IExamService, IDisposable
    {
        //string Default_TESTING_NO = string.Empty; //ตัวแปรนี้สำหรับนำผลคะแนนเข้าเท่านั้น อย่ามาใช้ซ้ำ

        public ExamService()
        {

        }
        public ExamService(IAS.DAL.Interfaces.IIASPersonEntities _ctx)
            : base(_ctx)
        {

        }

        private string GenTestingNo(DateTime testingDate)
        {
            try
            {
                string yearThi = (testingDate.Year + 543).ToString("0000").Substring(2, 2);
                string result = "";
                string sql = "Select * From AG_TEST_RUNNING_NO_T Where LEAD_YEAR='" + yearThi + "'";
                var ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);
                result = "0001";
                if (dt.Rows.Count > 0)
                {
                    int nextNo = (dt.Rows[0]["LAST_TEST_NO"].ToInt() + 1);
                    result = nextNo.ToString("0000");
                    sql = "Update AG_TEST_RUNNING_NO_T SET LAST_TEST_NO = '" + result + "' " +
                          "Where LEAD_YEAR = '" + yearThi + "'";
                }
                else
                {
                    sql = "Insert Into AG_TEST_RUNNING_NO_T (LEAD_YEAR,LAST_TEST_NO,USER_ID,USER_DATE) ";
                    sql += string.Format("VALUES('{0}','{1}','{2}',To_Date('{3}','yyyymmdd'))",
                                        yearThi, result, "AGDOI", DateTime.Now.ToString_yyyyMMdd());
                }

                string strOut = ora.ExecuteCommand(sql);
                if (!string.IsNullOrEmpty(strOut))
                {
                    throw new ArgumentException(strOut);
                }
                return yearThi + result;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("ExamService_GenTestingNo", ex);
                throw new ArgumentException(ex.Message);
            }
        }

        public DTO.ResponseMessage<bool> InsertExam(DTO.ExamSchedule ent)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                //ตรวจสอบกำหนดการสอบซ้ำ
                var detail = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.LICENSE_TYPE_CODE == ent.LICENSE_TYPE_CODE
                                                              && w.TESTING_DATE == ent.TESTING_DATE
                                                              && w.TEST_TIME_CODE == ent.TEST_TIME_CODE
                                                              && w.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE).FirstOrDefault();
                //TODO: ตรวจสอบเรื่อง TestingNo ว่าจะ Running อย่างไร
                if (detail != null)
                {
                    if (!string.IsNullOrEmpty(detail.TESTING_NO))
                    {
                        res.ErrorMsg = Resources.errorExamService_001;
                        return res;
                    }
                }

                if (ent.TESTING_DATE == null)
                {
                    res.ErrorMsg = Resources.errorExamService_002;
                    return res;
                }

                if (!"E_N".Contains(ent.EXAM_STATUS))
                {
                    res.ErrorMsg = Resources.errorExamService_003;
                    return res;
                }


                ent.TESTING_NO = GenTestingNo(ent.TESTING_DATE);

                var exam = new AG_EXAM_LICENSE_R();
                ent.MappingToEntity(exam);
                base.ctx.AG_EXAM_LICENSE_R.AddObject(exam);
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ExamService_InsertExam", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateExam(DTO.ExamSchedule ent)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var exam = base.ctx.AG_EXAM_LICENSE_R
                                   .SingleOrDefault(s => s.TESTING_NO == ent.TESTING_NO &&
                                                         s.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE);

                exam.PRIVILEGE_STATUS = ent.PRIVILEGE_STATUS;
                //ent.MappingToEntity(exam);
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ExamService_UpdateExam", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> DeleteExam(string testingNo, string examPlaceCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ent = base.ctx.AG_EXAM_LICENSE_R
                                  .SingleOrDefault(lr => lr.TESTING_NO == testingNo &&
                                                         lr.EXAM_PLACE_CODE == examPlaceCode);
                if (ent != null)
                {
                    base.ctx.AG_EXAM_LICENSE_R.DeleteObject(ent);

                    var allsub = base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.Where(x => x.TESTING_NO == testingNo).ToList();
                    if (allsub != null)
                    {
                        foreach (var s in allsub)
                        {
                            base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.DeleteObject(s);
                        }
                    }

                    base.ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ExamService_DeleteExam", ex);
            }
            return res;
        }

        /// <summary>
        /// ตรวจสอบว่ามีรหัสสอบนี้หรือไม่
        /// </summary>
        /// <param name="testingNo">รหัสสอบ</param>
        /// <returns>DTO.ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> IsRightTestingNo(string testingNo)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ent = base.ctx.AG_EXAM_LICENSE_R
                                   .Where(w => w.TESTING_NO == testingNo)
                                   .FirstOrDefault();

                res.ResultMessage = ent != null;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ExamService_IsRightTestingNo", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลการสอบโดยระบุเงื่อนไข
        /// </summary>
        /// <param name="examPlaceGroupCode">รหัสกลุ่มสนามสอบ</param>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <param name="licenseTypeCode">ประเภทใบอนุญาต</param>
        /// <param name="yearMonth">ปีเดือนที่สอบ</param>
        /// <param name="timeCode">รหัสเวลาสอบ</param>
        /// <param name="testingDate">วันที่สอบ</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetExamByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                                                       string licenseTypeCode, string agentType, string yearMonth,
                                                                       string timeCode, string testingDate, int resultPage, int PageSize, Boolean CountAgain, string Owner = "")
        {

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("PL.EXAM_PLACE_GROUP_CODE like '{0}%' AND ", examPlaceGroupCode));
                sb.Append(GetCriteria("LR.EXAM_PLACE_CODE like '{0}%' AND ", examPlaceCode));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", licenseTypeCode));
                sb.Append(GetCriteria("LT.AGENT_TYPE = '{0}' AND ", agentType));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));
                sb.Append(GetCriteria("LR.TEST_TIME_CODE = '{0}' AND ", timeCode));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));
                sb.Append(GetCriteria("LR.EXAM_OWNER like '{0}%' AND ", Owner));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                #region MILK
                string firstCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;

                if (CountAgain)
                {
                    firstCon = " SELECT COUNT(*) CCount FROM ( ";
                    midCon = " ";
                    lastCon = " )";
                }
                else
                {
                    if (resultPage == 0 && PageSize == 0)
                    {

                    }
                    else
                    {
                        firstCon = " SELECT * FROM (";
                        midCon = " , ROW_NUMBER() OVER (ORDER BY LR.TESTING_NO) RUN_NO ";
                        lastCon = resultPage == 0
                                        ? "" :
                                        " Order By LR.TESTING_DATE ) A where A.RUN_NO BETWEEN " +
                                           resultPage.StartRowNumber(PageSize).ToString() + " AND " +

                                           resultPage.ToRowNumber(PageSize).ToString() + " order by A.RUN_NO asc ";

                    }
                }


                #endregion MILK
                string sql = firstCon + "SELECT distinct LR.TESTING_NO, LR.TESTING_DATE, TM.TEST_TIME,LR.EXAM_OWNER,(select ASSOCIATION_NAME from AG_IAS_ASSOCIATION where ASSOCIATION_CODE = LR.EXAM_OWNER)EXAM_OWNER_Name ,  " +
                             "  case when GR.EXAM_PLACE_GROUP_NAME is null then ASSO.ASSOCIATION_NAME else GR.EXAM_PLACE_GROUP_NAME end EXAM_PLACE_GROUP_NAME  " +
                             ",PL.EXAM_PLACE_NAME,PV.NAME PROVINCE,AG.AGENT_TYPE, " +
                             "(Select Count(*) " +
                             "From AG_APPLICANT_T exLr " +
                             "Where EXLR.TESTING_NO = LR.TESTING_NO AND " +
                             "EXLR.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND (EXLR.RECORD_STATUS IS NULL or EXLR.RECORD_STATUS !='X')) TOTAL_APPLY, " +
                             "LR.EXAM_ADMISSION, LT.LICENSE_TYPE_NAME, LR.EXAM_FEE, " +
                             "LR.TEST_TIME_CODE,LR.EXAM_PLACE_CODE, " +
                             "  case when GR.EXAM_PLACE_GROUP_CODE is null then ASSO.ASSOCIATION_CODE else GR.EXAM_PLACE_GROUP_CODE end EXAM_PLACE_GROUP_CODE , " +
                             "PL.PROVINCE_CODE,LR.LICENSE_TYPE_CODE, " +
                    //"(Select Count(*) " +
                    //"From AG_APPLICANT_T exLr " +
                    //"Where EXLR.TESTING_NO = LR.TESTING_NO AND " +
                    //"EXLR.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND EXLR.RECORD_STATUS IS NULL)|| '/'|| PL.SEAT_AMOUNT SEAT_AMOUNT " +
                              " LR.EXAM_APPLY || '/'|| LR.EXAM_ADMISSION SEAT_AMOUNT " +
                             midCon +
                             "FROM  " +
                             "AG_EXAM_LICENSE_R LR, " +
                             "AG_EXAM_TIME_R TM, " +
                             "vw_ias_province PV, " +
                             "AG_LICENSE_TYPE_R LT, " +
                             "AG_AGENT_TYPE_R AG, " +
                             "AG_EXAM_PLACE_R PL " +
                             " left join AG_EXAM_PLACE_GROUP_R GR on PL.EXAM_PLACE_GROUP_CODE = GR.EXAM_PLACE_GROUP_CODE " +
                             " left join AG_IAS_ASSOCIATION ASSO on pl.association_code = ASSO.ASSOCIATION_CODE " +
                             "WHERE " +
                             "LR.TEST_TIME_CODE = TM.TEST_TIME_CODE AND " +
                             "LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE AND " +
                             "PL.PROVINCE_CODE = PV.ID AND " +
                             "LR.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE AND " +
                             "AG.AGENT_TYPE = LT.AGENT_TYPE AND LR.EXAM_STATE in ('A','M') " +
                             crit + " " + lastCon;


                OracleDB db = new OracleDB();
                //DataSet ds = ds = db.GetDataSet(sql);
                DataSet ds = db.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetExamByTestCenter(string examPlaceGroupCode, string examPlaceCode,
                                                               string licenseTypeCode, string yearMonth,
                                                               string timeCode, string testingDate, string compcode)
        {

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("PL.EXAM_PLACE_GROUP_CODE = '{0}' AND ", examPlaceGroupCode));
                sb.Append(GetCriteria("LR.EXAM_PLACE_CODE = '{0}' AND ", examPlaceCode));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", licenseTypeCode));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMM') = '{0}' AND ", yearMonth));
                sb.Append(GetCriteria("LR.TEST_TIME_CODE = '{0}' AND ", timeCode));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));
                sb.Append(GetCriteria("RT.COMP_CODE = '{0}' AND ", compcode));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                string sql = "SELECT distinct LR.TESTING_NO, LR.TESTING_DATE, TM.TEST_TIME, " +
                             "GR.EXAM_PLACE_GROUP_NAME,PL.EXAM_PLACE_NAME,PV.NAME PROVINCE,LR.EXAM_OWNER,RT.COMP_CODE, " +
                             "(Select Count(*) " +
                             "From AG_APPLICANT_T exLr " +
                             "Where EXLR.TESTING_NO = LR.TESTING_NO AND " +
                             "EXLR.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND (EXLR.RECORD_STATUS IS NULL or EXLR.RECORD_STATUS != 'X') TOTAL_APPLY, " +
                             "LR.EXAM_ADMISSION, LT.LICENSE_TYPE_NAME, LR.EXAM_FEE, " +
                             "LR.TEST_TIME_CODE,LR.EXAM_PLACE_CODE,PL.EXAM_PLACE_GROUP_CODE, " +
                             "PL.PROVINCE_CODE,LR.LICENSE_TYPE_CODE " +
                             "FROM  " +
                             "AG_EXAM_LICENSE_R LR, " +
                             "AG_EXAM_TIME_R TM, " +
                             "AG_EXAM_PLACE_GROUP_R GR, " +
                             "AG_EXAM_PLACE_R PL, " +
                             "vw_ias_province PV, " +
                             "AG_LICENSE_TYPE_R LT, " +
                             "AG_IAS_REGISTRATION_T RT " +
                             "WHERE " +
                             "LR.TEST_TIME_CODE = TM.TEST_TIME_CODE AND " +
                             "LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE AND " +
                             "PL.EXAM_PLACE_GROUP_CODE = GR.EXAM_PLACE_GROUP_CODE AND " +
                             "PL.PROVINCE_CODE = PV.ID AND " +
                             "LR.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE AND " +
                             "LR.EXAM_OWNER = 'C' " +
                             crit +
                             " Order By LR.TESTING_DATE";

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

         
        /// <summary>
        /// ดึงจำนวนที่นั่งสอบตามสนามสอบ
        /// </summary>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <returns></returns>
        public DTO.ResponseService<string> GetSeatAmount(string examPlaceCode)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                var ent = base.ctx.AG_EXAM_PLACE_R.SingleOrDefault(s => s.EXAM_PLACE_CODE == examPlaceCode && s.ACTIVE == "Y");
                if (ent != null)
                {
                    res.DataResponse = ent.SEAT_AMOUNT.ToString();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// ดึงค่าสมัครสอบ
        /// </summary>
        /// <returns></returns>
        public DTO.ResponseService<string> GetExamFee()
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                var ent = base.ctx.AG_PETITION_TYPE_R.SingleOrDefault(s => s.PETITION_TYPE_CODE == "01");
                if (ent != null)
                {
                    res.DataResponse = ent.FEE.ToString();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// ตรวจสอบการเปลี่ยนแปลงข้อมูลการสอบ
        /// </summary>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> CanChangeExam(string testingNo, string examPlaceCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ent = base.ctx.AG_EXAM_LICENSE_R
                                  .SingleOrDefault(w => w.TESTING_NO == testingNo &&
                                                        w.EXAM_PLACE_CODE == examPlaceCode);

                //มีการ Input หรือ Upload ข้อมูลเข้าใน AG_APPLICANT_T หรือยัง
                int iHasApply = base.ctx.AG_APPLICANT_T
                                             .Where(w => w.TESTING_NO == testingNo &&
                                                         w.EXAM_PLACE_CODE == examPlaceCode &&
                                                         (w.RECORD_STATUS == null || w.RECORD_STATUS != "X")).Count();


                //วันที่สอบ Testing_Date > DateTime.Now.Date
                if (ent != null)
                {
                    res.ResultMessage = (iHasApply == 0) && (ent.TESTING_DATE > DateTime.Now.Date);
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลการสอบโดยเลขที่สอบและสนามสอบ
        /// </summary>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <param name="placeCode">รหัสสนามสอบ</param>
        /// <returns>ResponseService<ExamSchedule></returns>
        public DTO.ResponseService<DTO.ExamSchedule>
            GetExamByTestingNoAndPlaceCode(string testingNo, string placeCode)
        {
            var res = new DTO.ResponseService<DTO.ExamSchedule>();
            string sql = string.Empty;
            try
            {

                var examPlace = base.ctx.AG_EXAM_PLACE_R.SingleOrDefault(w => w.EXAM_PLACE_CODE == placeCode);
                if (examPlace != null)
                {
                    if (examPlace.EXAM_PLACE_GROUP_CODE != null)
                    {
                        sql = "SELECT LR.*,PR.EXAM_PLACE_GROUP_CODE ,PR.EXAM_PLACE_NAME,TR.TEST_TIME,VWP.NAME,LTR.LICENSE_TYPE_NAME,GR.EXAM_PLACE_GROUP_NAME ,ASS.association_name ,ASS.association_code "
                                             + "FROM AG_EXAM_LICENSE_R LR,AG_LICENSE_TYPE_R LTR,AG_EXAM_PLACE_GROUP_R GR, "
                                             + "AG_EXAM_PLACE_R PR , AG_EXAM_TIME_R TR,AGDOI.vw_ias_province VWP,ag_ias_association ASS "
                                             + "WHERE LR.EXAM_PLACE_CODE = PR.EXAM_PLACE_CODE "
                                             + "AND LR.TEST_TIME_CODE = TR.TEST_TIME_CODE "
                                             + "AND PR.PROVINCE_CODE = VWP.ID "
                                             + "AND LR.LICENSE_TYPE_CODE = LTR.LICENSE_TYPE_CODE "
                                             + "AND GR.EXAM_PLACE_GROUP_CODE = PR.EXAM_PLACE_GROUP_CODE "
                                             + "AND ASS.association_code = lr.exam_owner "
                                             + "AND      LR.TESTING_NO = '" + testingNo + "' AND "
                                             + "      LR.EXAM_PLACE_CODE = '" + placeCode + "' ";
                    }
                    else if (examPlace.ASSOCIATION_CODE != null)
                    {
                        sql = "SELECT LR.*,PR.EXAM_PLACE_GROUP_CODE ,PR.EXAM_PLACE_NAME,TR.TEST_TIME,VWP.NAME,LTR.LICENSE_TYPE_NAME,GR.ASSOCIATION_NAME ,GR.ASSOCIATION_CODE "
                                          + "FROM AG_EXAM_LICENSE_R LR,AG_LICENSE_TYPE_R LTR,AG_IAS_ASSOCIATION GR, "
                                          + "AG_EXAM_PLACE_R PR , AG_EXAM_TIME_R TR,AGDOI.vw_ias_province VWP "
                                          + "WHERE LR.EXAM_PLACE_CODE = PR.EXAM_PLACE_CODE "
                                          + "AND LR.TEST_TIME_CODE = TR.TEST_TIME_CODE "
                                          + "AND PR.PROVINCE_CODE = VWP.ID "
                                          + "AND LR.LICENSE_TYPE_CODE = LTR.LICENSE_TYPE_CODE "
                                          + "AND GR.ASSOCIATION_CODE = PR.ASSOCIATION_CODE "
                                          + "AND      LR.TESTING_NO = '" + testingNo + "' AND "
                                          + "      LR.EXAM_PLACE_CODE = '" + placeCode + "' ";
                    }
                }


                //var a = base.ctx.ag_exam



                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                DTO.ExamSchedule ent = new DTO.ExamSchedule();
                if (dt != null && dt.Rows.Count > 0)
                {
                    ent = dt.Rows[0].MapToEntity<DTO.ExamSchedule>();
                }

                res.DataResponse = ent;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลการสอบโดยใช้ ปี และ เดือน
        /// </summary>
        /// <param name="yearMonth">ปีและเดือน</param>
        /// <returns>ResponseService<List<DateTime>></returns>
        public DTO.ResponseService<List<DateTime>> GetExamByYearMonth(string yearMonth)
        {
            DTO.ResponseService<List<DateTime>> res = new DTO.ResponseService<List<DateTime>>();
            List<DateTime> ls = new List<DateTime>();
            try
            {
                string crit = string.Format("TO_CHAR(LR.TESTING_DATE,'YYYYMM') = '{0}' ", yearMonth);
                string order = "Order By LR.TESTING_DATE";

                string sql = "SELECT distinct LR.TESTING_DATE " +
                             "FROM  " +
                             "AG_EXAM_LICENSE_R LR " +
                             "WHERE " +
                             crit + order;

                OracleDB db = new OracleDB();
                DataTable dt = db.GetDataTable(sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(Convert.ToDateTime(dt.Rows[i][0]));
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            res.DataResponse = ls;
            return res;
        }


        /// <summary>
        /// ดึงข้อมูลการสอบโดยใช้เงื่อนไข
        /// </summary>
        /// <param name="idCard">เลขบัตรประชาชน</param>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <param name="firstName">ชื่อ</param>
        /// <param name="lastName">นามสกุล</param>
        /// <param name="startDate">วันที่เริ่ม</param>
        /// <param name="finishDate">วันที่สิ้นสุด</param>
        /// <param name="paymentNo">เลขที่ใบสั่งจ่าย</param>
        /// <param name="billNo">เลขที่ใบเสร็จ</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet>
            GetExamListByCriteria(string idCard, string testingNo,
                                  string firstName, string lastName,
                                  string startDate, string finishDate,
                                  string paymentNo, string billNo)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO like '{0}%' AND ", idCard));
                sb.Append(GetCriteria("AP.TESTING_NO LIKE '{0}%' AND ", testingNo));
                sb.Append(GetCriteria("AP.NAMES LIKE '{0}%' AND ", firstName));
                //sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMM') = '{0}' AND ", yearMonth));
                sb.Append(GetCriteria("AP.PAYMENT_NO LIKE '{0}%' AND ", paymentNo));
                //sb.Append(GetCriteria("LR.TESTING_DATE = to_date('{0}','yyyymmdd') AND ", testingDate));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;
                string sql = "SELECT AP.ID_CARD_NO, TN.NAME || AP.NAMES FIRSTNAME, " +
                             "       AP.LASTNAME, AP.TESTING_NO, EX.TESTING_DATE, " +
                             "       AP.RESULT, AP.PAYMENT_NO " +
                             "FROM AG_APPLICANT_T AP, " +
                             "     VW_IAS_TITLE_NAME TN, " +
                             "     AG_EXAM_LICENSE_R EX  " +
                             "WHERE AP.PRE_NAME_CODE = TN.ID AND " +
                             "      AP.TESTING_NO = EX.TESTING_NO AND " +
                             crit;

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

        /// <summary>
        /// ดึงข้อมูลการสอบโดยใช้เลขที่สอบ
        /// </summary>
        /// <param name="testingNo">เลขที่สอบ</param>
        /// <returns>ResponseService<ExamSchedule></returns>
        public DTO.ResponseService<DTO.ExamSchedule> GetExamByTestingNo(string testingNo)
        {
            var res = new DTO.ResponseService<DTO.ExamSchedule>();
            try
            {
                var exam = ctx.AG_EXAM_LICENSE_R
                                   .FirstOrDefault(s => s.TESTING_NO == testingNo && (s.EXAM_STATE == "A" || s.EXAM_STATE == "M"));
                DTO.ExamSchedule ent = new DTO.ExamSchedule();

                if (exam != null)
                {
                    exam.MappingToEntity(ent);
                }
                res.DataResponse = ent;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }


        /// <summary>
        /// ตรวจสอบข้อมูลการสอบ
        /// </summary>
        /// <param name="exr">ข้อมูลที่ Upload</param>
        private void ValidateExamResultTemp(string testingNo,
                                            DTO.ExamHeaderResultTemp head,
                                            DTO.ExamResultTemp exr, string IDcard, string com_code, List<AG_IAS_SUBPAYMENT_D_T> Havetest)
        {
            //string testingNo = exam != null ? exam.TESTING_NO : string.Empty;
            string ex_place = head.PROVINCE_CODE + "" + head.ASSOCIATE_CODE;

            try
            {
                if (com_code == "")
                {
                    Havetest = Havetest.Where(dt => dt.COMPANY_CODE == null).ToList();
                }
                else
                {
                    Havetest = Havetest.Where(dt => dt.COMPANY_CODE == com_code).ToList();
                }

                if (Havetest.Count > 0)
                {
                    Havetest = Havetest.Where(dtID => dtID.ID_CARD_NO == IDcard).ToList();
                    if (Havetest.Count > 0)
                    {
                        Havetest = Havetest.Where(acc => acc.ACCOUNTING == "Y").ToList();

                        if (Havetest.Count == 0)
                        {
                            exr.ERROR_MSG = "\n- " + Resources.errorExamService_004;
                        }
                        else
                        {
                            string save_data = SaveUploadDataToDatabase(testingNo, head, IDcard); //true = C in status_save_score
                            if (save_data == "T")
                            {
                                exr.ERROR_MSG = "\n- " + Resources.errorExamService_005;
                            }
                            else if (save_data == "M")
                            {
                                exr.ERROR_MSG = "\n- " + Resources.errorExamService_006;
                            }
                            else if (save_data == "F")
                            {

                                string sql = " select distinct  * from ag_applicant_t where testing_no='" + testingNo + "' " +
                                            " and exam_place_code='" + head.EXAM_PLACE_CODE + "' and "
                                            + " applicant_code='" + exr.SEAT_NO.ToInt() + "' and id_card_no='" + exr.ID_CARD_NO.ToString() + "'";
                                OracleDB db = new OracleDB();
                                DataTable dt = db.GetDataTable(sql);

                                if (dt.Rows.Count == 0)
                                {
                                    exr.ERROR_MSG = "\n- " + Resources.errorExamService_007;
                                }
                            }
                            else
                            {
                                exr.ERROR_MSG = "\n- " + Resources.errorExamService_008;
                            }
                        }
                    }
                    else
                    {
                        exr.ERROR_MSG = "\n- " + Resources.errorExamService_009;
                    }

                }
                else
                {
                    exr.ERROR_MSG = "\n- " + Resources.errorExamService_010;
                }

            }
            catch
            {
                exr.ERROR_MSG = "\n- " + Resources.errorExamService_008;
            }
        }

        private string SaveUploadDataToDatabase(string testingNo,
                                            DTO.ExamHeaderResultTemp head,
                                             string IDcard)
        {
            string save = "F"; //defult = ยังไม่เซฟ
            // string testingNo = exam != null ? exam.TESTING_NO : string.Empty;
            string ex_place = head.PROVINCE_CODE + "" + head.ASSOCIATE_CODE;
            var res = new DTO.ResponseService<DTO.ExamSchedule>();

            try
            {
                string test_date = " to_date('" + head.TESTING_DATE + "','DD/MM/YYYY HH24:mi:ss', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') ";

                string sql = " select * from AG_IAS_APPLICANT_SCORE_H_TEMP Htemp,AG_IAS_APPLICANT_SCORE_D_TEMP Dtemp " +
                            " where Htemp.upload_group_no = Dtemp.upload_group_no and " +
                            " Htemp.upload_group_no = '"+head.UPLOAD_GROUP_NO+"' and "+
                            " Htemp.license_type_code = '" + head.LICENSE_TYPE_CODE + "' and " +
                            " Htemp.province_code = '" + head.PROVINCE_CODE + "' and " +
                            " Htemp.associate_code = '" + head.ASSOCIATE_CODE + "' and " +
                            " to_date(Htemp.testing_date,'DD/MM/YYYY HH24:mi:ss', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI') = " + test_date + " and " +
                            " Dtemp.id_card_no = '" + IDcard + "' and " +
                            " Trim(Dtemp.status_save_score) = 'C'  ";

                OracleDB db = new OracleDB();
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)//มีการเซฟไฟล์ลงฐานข้อมูลหลักแล้ว
                {
                    save = "T";
                }
                else
                {
                    save = "F";
                }

                if (dt.Rows.Count > 1)
                {
                    save = "M"; //more data
                }

            }
            catch (Exception ex)
            {
                save = "Error";
            }
            return save;
        }

        public bool IsAlphaNumeric(String str)
        {
            Regex regexAlphaNum = new Regex("[^0-9]");

            return !regexAlphaNum.IsMatch(str);
        }


        private DTO.UploadData ReadFileUpload(String fileData)
        {
            DownloadFileResponse res = FileManagerService.RemoteFileCommand(new DownloadFileRequest() { TargetContainer = "", TargetFileName = fileData }).Action();

            if (res.Code != "0000")
            {
                LoggerFactory.CreateLog().LogError("ไม่พบไฟล์ที่ระบุ ." + fileData);
                throw new ApplicationException("ไม่พบไฟล์ที่ระบุ .");
            }


            Stream rawData = res.FileByteStream;


            DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };

            try
            {
                try
                {
                    //Stream rawData = FileUpload1.PostedFile.InputStream;
                    using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
                    {
                        int i = 0;
                        string line = string.Empty; // sr.ReadLine().Trim();
                        while ((line = sr.ReadLine()) != null)
                        {
                            if (i == 0)
                            {
                                if (line != null && line.Length > 0)
                                {
                                    if (line.Substring(0, 1) == "H")
                                    {
                                        data.Header = line;
                                    }
                                    else
                                    {
                                        //res.ErrorMsg = Resources.errorExamResultBiz_001;
                                        // return res;
                                    }
                                }
                            }
                            else
                            {
                                if (line.Trim().Length > 0)
                                {
                                    data.Body.Add(line.Trim());
                                }
                            }
                            i++;
                        }

                        //if (i == 0)//แสดงว่าไม่ได้เข้าไปวนลูป while เลย
                        //{
                        //    res.ErrorMsg = Resources.errorExamResultBiz_002;
                        //    return res;
                        //}
                    }

                    //  res = svc.InsertAndCheckExamResultUpload(data, fileName, userId, TestNo);

                }
                catch (Exception ex)
                {
                    // res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                }
            }
            catch (IOException ex)
            {
                //res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return data;
        }


        //UC014: นำผลการสอบเข้าระบบ
        /// <summary>
        /// นำข้อมูลผลการสอบเข้า Temp
        /// </summary>
        /// <param name="data">Class UploadData</param>
        /// <param name="fileName">ชื่อไฟล์</param>
        /// <param name="userId">user id</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ExamResultTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>> InsertAndCheckExamResultUpload(string fileName, string userId, string Default_TESTING_NO)
        {
            DTO.UploadData data = ReadFileUpload(fileName);
            List<DTO.ls_AG_EXAM_CONDITION_R> SubjectList = null;
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>>();
            res.DataResponse = new DTO.UploadResult<DTO.UploadResultHeader, DTO.ExamResultTemp>();

            res.DataResponse.Header = new List<DTO.UploadResultHeader>();
            res.DataResponse.Detail = new List<DTO.ExamResultTemp>();
            string testing_no = string.Empty;
            //  string Default_TESTING_NO = string.Empty;
            Func<string[], int, string> GetByIndex = (aryString, index) =>
            {
                return aryString.Length - 1 >= index
                            ? aryString[index].Trim()
                            : string.Empty;
            };

            try
            {

                res.DataResponse.GroupId = OracleDB.GetGenAutoId();

                DTO.ExamHeaderResultTemp head = new DTO.ExamHeaderResultTemp();
                // string strTestingNo = GetTestingNoFrom_fileImport(head);
                var examDetail = GetExamByTestingNo(Default_TESTING_NO);
                #region เช็คว่ามีรอบสอบนี้ในสถานะเป็น แอคทีฟ หรือ มูฟ ไหม?
                if (examDetail != null)
                {
                    #region HEADER
                    string IMPORT_TYPE = string.Empty;
                    try
                    {
                        #region return ทันที
                        if (data.Header == null)
                        {
                            res.ErrorMsg = "\n" + Resources.errorExamService_011;
                            return res;
                        }
                        if (data.Body.Count == 0)
                        {
                            res.ErrorMsg = "\n" + Resources.errorExamService_012;
                            return res;
                        }
                        #endregion

                        #region return หลังต่อสตริง
                        Regex regDigit = new Regex(@"^\d+$");
                        string testingDate = string.Empty;
                        IMPORT_TYPE = examDetail.DataResponse.IMPORT_YPE;
                        var DataSer = new IAS.DataServices.DataCenter.DataCenterService();

                        head.TESTING_DATE = examDetail.DataResponse.TESTING_DATE.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        head.UPLOAD_GROUP_NO = res.DataResponse.GroupId;
                        head.ASSOCIATE_NAME = DataSer.GetAssociateNameById(examDetail.DataResponse.EXAM_OWNER).DataResponse.Name.ToString();
                        head.ASSOCIATE_CODE = examDetail.DataResponse.EXAM_OWNER.ToString();
                        head.LICENSE_TYPE_CODE = examDetail.DataResponse.LICENSE_TYPE_CODE.ToString();
                        head.PROVINCE_CODE = GetPlaceDetailByPlaceCode_noCheckActive(examDetail.DataResponse.EXAM_PLACE_CODE.ToString()).DataResponse.Tables[0].Rows[0]["PROVINCE_CODE"].ToString();
                        head.EXAM_TIME_CODE = examDetail.DataResponse.TEST_TIME_CODE.ToString();
                        head.CNT_PER = data.Body.Count.ToString();
                        head.FILENAME = fileName;
                        head.EXAM_PLACE_CODE = examDetail.DataResponse.EXAM_PLACE_CODE;

                        decimal CourseNumber = examDetail.DataResponse.COURSE_NUMBER;
                        SubjectList = GetSubjectList(Convert.ToString(CourseNumber));

                        if (SubjectList.Count == 0)
                        {
                            res.ErrorMsg = "\n" + Resources.errorExamService_017;
                            return res;
                        }
                        else if (SubjectList == null)
                        {
                            res.ErrorMsg = "\n" + Resources.errorExamService_018;
                            return res;
                        }

                        #endregion return หลังต่อสตริง

                    }
                    catch
                    {
                        res.ErrorMsg = "\n" + Resources.errorExamService_029;
                        return res;
                    }


                    var entHead = new AG_IAS_APPLICANT_SCORE_H_TEMP();
                    head.MappingToEntity(entHead);
                    base.ctx.AG_IAS_APPLICANT_SCORE_H_TEMP.AddObject(entHead);

                    #endregion endHeader
                    //Add Header
                    List<VW_IAS_TITLE_NAME> lsTitle = base.ctx.VW_IAS_TITLE_NAME.ToList();
                    #region DETAIL
                    string compCode = string.Empty;
                    string insurCompName = string.Empty;
                    var Havetest = base.ctx.AG_IAS_SUBPAYMENT_D_T.Where(DT => DT.EXAM_PLACE_CODE == head.EXAM_PLACE_CODE && DT.TESTING_NO == Default_TESTING_NO).ToList();
                    if (Havetest.Count > 0)
                    {

                        for (int i = 0; i < data.Body.Count; i++)
                        {
                            string d = data.Body[i];
                            string testingNo = Default_TESTING_NO;
                            string[] rawData = d.Split('|');
                            try
                            {


                                DTO.ExamResultTemp exr = new DTO.ExamResultTemp();//ถ้าต้องแก้ใหม่ให้มาแก้ตรงนี้ 07 08 milk
                                exr.UPLOAD_GROUP_NO = head.UPLOAD_GROUP_NO;
                                exr.SEQ_NO = (i + 1).ToString("0000");
                                exr.SEAT_NO = GetByIndex(rawData, 1);
                                exr.ID_CARD_NO = GetByIndex(rawData, 2);
                                exr.TITLE = GetByIndex(rawData, 3);
                                exr.NAMES = GetByIndex(rawData, 4);
                                exr.LAST_NAME = GetByIndex(rawData, 5);
                                exr.ADDRESS1 = GetByIndex(rawData, 6);
                                exr.ADDRESS2 = GetByIndex(rawData, 7);
                                exr.AREA_CODE = GetByIndex(rawData, 8);
                                exr.BIRTH_DATE = GetByIndex(rawData, 9).String_dd_MM_yyyy_ToDate('/', true);
                                exr.SEX = GetByIndex(rawData, 10);
                                exr.EDUCATION_CODE = GetByIndex(rawData, 11);
                                exr.COMP_CODE = GetByIndex(rawData, 12);
                                exr.APPROVE_DATE = GetByIndex(rawData, 13).String_dd_MM_yyyy_ToDate('/', true);
                                exr.AssociateName = head.ASSOCIATE_NAME;
                                exr.AssociateCode = head.ASSOCIATE_CODE;
                                exr.ProvinceCode = head.PROVINCE_CODE;
                                exr.TestingDate = head.TESTING_DATE;
                                exr.LicenseTypeCode = head.LICENSE_TYPE_CODE;
                                exr.TimeCode = head.EXAM_TIME_CODE;

                                if (compCode != exr.COMP_CODE.Trim())
                                {
                                    compCode = exr.COMP_CODE.Trim();
                                    var comp = base.ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == compCode);
                                    insurCompName = comp != null ? comp.NAME : string.Empty;
                                }

                                exr.InsurCompName = insurCompName;

                             
                                string sqlapp_t = "SELECT * from AG_APPLICANT_T where TESTING_NO = '" + Default_TESTING_NO + "' and ID_CARD_NO = '" + exr.ID_CARD_NO + "' and EXAM_PLACE_CODE = '" + head.EXAM_PLACE_CODE + "'";
                                //AG_APPLICANT_T appT = base.ctx.AG_APPLICANT_T.FirstOrDefault(x => x.TESTING_NO == Default_TESTING_NO
                                //                        && x.ID_CARD_NO == exr.ID_CARD_NO && x.EXAM_PLACE_CODE == head.EXAM_PLACE_CODE);
                                AG_APPLICANT_T appT = new AG_APPLICANT_T();
                                try
                                {
                                    appT = base.ctx.ExecuteStoreQuery<AG_APPLICANT_T>(sqlapp_t).ToList().First();
                                }
                                catch (Exception ex)
                                {
                                    res.ErrorMsg = "\n ไม่พบข้อมูลผู้สมัครสอบ";
                                    return res;
                                }
                                if (appT != null)
                                {
                                    var HaveScoreInBase = base.ctx.AG_APPLICANT_SCORE_T.FirstOrDefault(X => X.APPLICANT_CODE == appT.APPLICANT_CODE
                                                            && X.TESTING_NO == Default_TESTING_NO);
                                    if (HaveScoreInBase != null)
                                        exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n" + "- มีคะแนนสอบในฐานข้อมูลแล้ว" : "\n" + "- มีคะแนนสอบในฐานข้อมูลแล้ว";


                                    if (IMPORT_TYPE != "W")
                                    {
                                        if (appT.EXAM_ROOM == null)
                                            exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n" + "- ไม่พบห้องสอบ" : "\n" + "- ไม่พบห้องสอบ";
                                    }
                                }
                                string examResult = GetByIndex(rawData, 14).Trim();

                                if ("P_F".Contains(examResult))
                                {
                                    exr.EXAM_RESULT = examResult;
                                }
                                else if (examResult == "M")
                                {
                                    exr.ABSENT_EXAM = examResult;
                                }
                                else
                                {
                                    exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n" + Resources.errorExamService_030 : "\n" + Resources.errorExamService_030;
                                }
                                exr.SCORE_1 = GetByIndex(rawData, 15);
                                exr.SCORE_2 = GetByIndex(rawData, 16);
                                exr.SCORE_3 = GetByIndex(rawData, 17);
                                exr.SCORE_4 = GetByIndex(rawData, 18);
                                exr.SCORE_5 = GetByIndex(rawData, 19);
                                exr.SCORE_6 = GetByIndex(rawData, 20);
                                exr.SCORE_7 = GetByIndex(rawData, 21);
                                exr.SCORE_8 = GetByIndex(rawData, 22);
                                exr.SCORE_9 = GetByIndex(rawData, 23);
                                exr.SCORE_10 = GetByIndex(rawData, 24);
                                exr.SCORE_11 = GetByIndex(rawData, 25);
                                exr.SCORE_12 = GetByIndex(rawData, 26);
                                exr.SCORE_13 = GetByIndex(rawData, 27);
                                exr.SCORE_14 = GetByIndex(rawData, 28);
                                exr.SCORE_15 = GetByIndex(rawData, 29);
                                exr.SCORE_16 = GetByIndex(rawData, 30);

                                Boolean SCOREisINT = true;
                                string errorScore = "";
                                for (int ScoreTemp = 15; ScoreTemp <= 30; ScoreTemp++)
                                {
                                    bool score_temp = IsAlphaNumeric(GetByIndex(rawData, ScoreTemp));
                                    if (!score_temp)
                                    {
                                        errorScore = (errorScore != "") ? errorScore + "," + (ScoreTemp - 14) + " " : errorScore + " " + (ScoreTemp - 14) + " ";
                                        SCOREisINT = false;
                                    }
                                }

                                if (!SCOREisINT)
                                    exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n" + Resources.errorExamService_031 + errorScore + Resources.errorExamService_032 : "\n" + Resources.errorExamService_031 + errorScore + Resources.errorExamService_032;


                                if (SCOREisINT)
                                {
                                    if ((exr.SCORE_1.Length > 3) || (exr.SCORE_2.Length > 3) || (exr.SCORE_3.Length > 3) || (exr.SCORE_4.Length > 3)
                                           || (exr.SCORE_5.Length > 3) || (exr.SCORE_6.Length > 3) || (exr.SCORE_7.Length > 3) || (exr.SCORE_8.Length > 3)
                                           || (exr.SCORE_9.Length > 3) || (exr.SCORE_10.Length > 3) || (exr.SCORE_11.Length > 3) || (exr.SCORE_12.Length > 3)
                                           || (exr.SCORE_13.Length > 3) || (exr.SCORE_14.Length > 3) || (exr.SCORE_15.Length > 3) || (exr.SCORE_16.Length > 3))
                                    {
                                        res.ErrorMsg = Resources.errorExamService_033 + "<br>" + Resources.errorExamService_034 + exr.SEQ_NO + Resources.errorExamService_035 + exr.ID_CARD_NO + ")";
                                        return res;
                                    }

                                    if (examResult == "M")
                                    {
                                        if ((exr.SCORE_1 != "" && exr.SCORE_1.ToInt() != 0) || (exr.SCORE_2 != "" && exr.SCORE_2.ToInt() != 0)
                                            || (exr.SCORE_3 != "" && exr.SCORE_3.ToInt() != 0) || (exr.SCORE_4 != "" && exr.SCORE_4.ToInt() != 0)
                                            || (exr.SCORE_5 != "" && exr.SCORE_5.ToInt() != 0) || (exr.SCORE_6 != "" && exr.SCORE_6.ToInt() != 0)
                                            || (exr.SCORE_7 != "" && exr.SCORE_7.ToInt() != 0) || (exr.SCORE_8 != "" && exr.SCORE_8.ToInt() != 0)
                                            || (exr.SCORE_9 != "" && exr.SCORE_9.ToInt() != 0) || (exr.SCORE_10 != "" && exr.SCORE_10.ToInt() != 0)
                                            || (exr.SCORE_11 != "" && exr.SCORE_11.ToInt() != 0) || (exr.SCORE_12 != "" && exr.SCORE_12.ToInt() != 0)
                                            || (exr.SCORE_13 != "" && exr.SCORE_13.ToInt() != 0) || (exr.SCORE_14 != "" && exr.SCORE_14.ToInt() != 0)
                                            || (exr.SCORE_15 != "" && exr.SCORE_15.ToInt() != 0) || (exr.SCORE_16 != "" && exr.SCORE_16.ToInt() != 0))
                                        {
                                            exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n" + Resources.errorExamService_036 : "\n" + Resources.errorExamService_036; //" มีบุคคลขาดสอบแต่มีผลการสอบ <br>(ลำดับที่ " + exr.SEQ_NO + "  หมายเลขบัตรประชาชน " + exr.ID_CARD_NO + ")";
                                        }
                                    }
                                }

                                string title = exr.TITLE == "น.ส." ? "นางสาว" : exr.TITLE;

                                //ตรวจสอบคำนำหน้าชื่อ
                                VW_IAS_TITLE_NAME entTitle = lsTitle.FirstOrDefault(s => s.NAME == title);
                                if (entTitle != null)
                                    exr.PRE_NAME_CODE = entTitle.ID.ToString();
                                else
                                {
                                    exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n" + Resources.errorExamService_037 : "\n" + Resources.errorExamService_037;
                                }

                                #region เช็คจ่ายเงิน


                                //หารายการสอบ
                                //TODO: ผลการสอบจะต้องมีรายการสอบ

                                if (Convert.ToInt32(head.CNT_PER) != Convert.ToInt32(data.Body.Count))
                                {
                                    res.ErrorMsg = "\n" + Resources.errorExamService_038;
                                    return res;
                                }
                                else
                                {

                                    testingNo = Default_TESTING_NO;

                                    this.ValidateExamResultTemp(testingNo, head, exr, GetByIndex(rawData, 2), GetByIndex(rawData, 12), Havetest);
                                    // testingNo = (examLicense != null) ? examLicense.TESTING_NO : "";

                                #endregion จ่ายเงิน

                                    //add by milk

                                    try
                                    {
                                        if (SCOREisINT)
                                        {
                                            #region check score

                                            string[] score = new string[16];

                                            Boolean ch_sc = false;
                                            int sc = 0;
                                            for (sc = 0; sc <= 15; sc++)
                                            {
                                                score[sc] = GetByIndex(rawData, sc + 15);
                                                if ((score[sc] != "") && (SubjectList.Count < sc + 1))
                                                    if (score[sc].ToInt() != 0)
                                                        ch_sc = true;
                                            }

                                            if (ch_sc)
                                                exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br> \n" + Resources.errorExamService_039 + "<br>\t" + Resources.errorExamService_040 + SubjectList.Count.ToInt() + Resources.errorExamService_041 : "\n" + Resources.errorExamService_042 + "<br>\t" + Resources.errorExamService_040 + SubjectList.Count.ToInt() + Resources.errorExamService_041;

                                            DataTable dt4 = this.checkBlackList(exr.ID_CARD_NO);
                                            DataTable dt5 = this.CheckBlack_List(exr.ID_CARD_NO, testingNo);


                                            string resss = this.CheckblacklistAndCheckPassorFall(dt4, dt5, exr.LicenseTypeCode, exr.SEAT_NO, testingNo, score, true, SubjectList); // B F P
                                            if (resss.Length > 1)
                                            {
                                                exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n- " + resss + " " : "\n- " + resss + " ";//<br>(ลำดับที่ " + exr.SEQ_NO + "  หมายเลขบัตรประชาชน " + exr.ID_CARD_NO + ")";

                                            }
                                            else if (examResult != "M")
                                            {
                                                if (resss != "B")
                                                {
                                                    if (exr.EXAM_RESULT != resss)
                                                    {
                                                        string txt1 = exr.EXAM_RESULT == "F" ? "ไม่ผ่าน" : "ผ่าน";
                                                        string txt2 = resss == "F" ? "ไม่ผ่าน" : "ผ่าน";
                                                        exr.ERROR_MSG = (exr.ERROR_MSG != null) ? exr.ERROR_MSG + "<br>" + "\n- ผลการสอบไม่สอดคล้องกับคะแนน <br>\t(ไฟล์นำเข้าระบุว่า '" + txt1 + "' แต่คะแนนที่คำนวณได้ '" + txt2 + "')" : "\n- ผลการสอบไม่สอดคล้องกับคะแนน <br>\t(ไฟล์นำเข้าระบุว่า '" + txt1 + "' แต่คะแนนที่คำนวณได้ '" + txt2 + "')";//<br>(ลำดับที่ " + exr.SEQ_NO + "  หมายเลขบัตรประชาชน " + exr.ID_CARD_NO + ")";

                                                    }
                                                }
                                                //else
                                                //{
                                                //    res.ErrorMsg = "\nบุคคลนี้ถูกแบล๊กลิสต์ (ลำดับที่ " + exr.SEQ_NO + "  หมายเลขบัตรประชาชน " + exr.ID_CARD_NO + ")";
                                                //    return res;
                                                //}
                                            }
                                            #endregion check score
                                        }
                                    }
                                    catch
                                    {
                                        res.ErrorMsg = "\n" + Resources.errorExamService_043;
                                        return res;
                                    }

                                    //add by milk

                                    DAL.AG_IAS_APPLICANT_SCORE_D_TEMP ent = new AG_IAS_APPLICANT_SCORE_D_TEMP();

                                    exr.MappingToEntity(ent);
                                    base.ctx.AG_IAS_APPLICANT_SCORE_D_TEMP.AddObject(ent);
                                    res.DataResponse.Detail.Add(exr);
                                }


                            }
                            catch
                            {
                                res.ErrorMsg = "\n" + Resources.errorExamService_044;
                                return res;
                            }
                        }
                    #endregion end detail
                        int total = res.DataResponse.Detail.Count();
                        int missingTrans = res.DataResponse.Detail.Where(w => !string.IsNullOrEmpty(w.ERROR_MSG)).Count();
                        int rightTrans = res.DataResponse.Detail.Where(w => string.IsNullOrEmpty(w.ERROR_MSG)).Count();

                        res.DataResponse.Header.Add(new DTO.UploadResultHeader
                        {
                            Totals = total,
                            MissingTrans = missingTrans,
                            RightTrans = rightTrans,
                            UploadFileName = fileName,
                            TestingNo = Default_TESTING_NO
                        });

                        using (TransactionScope ts = new TransactionScope())
                        {
                            base.ctx.SaveChanges();
                            ts.Complete();
                        }
                    }
                    else
                    {
                        res.ErrorMsg = "\n ไม่มีการสร้างใบสั่งจ่ายในรอบสอบนี้ (ไม่พบข้อมูลผู้สมัครสอบ)";
                        return res;
                    }
                #endregion  เช็คว่ามีรอบสอบนี้ในสถานะเป็น แอคทีฟ หรือ มูฟ ไหม?
                }
                else
                {
                    res.ErrorMsg = "\n" + Resources.errorExamService_045;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = Resources.errorExamService_046;
            }

            return res;
        }


      
        private List<DTO.ls_AG_EXAM_CONDITION_R> GetSubjectList(string typeCode)
        {
            List<DTO.ls_AG_EXAM_CONDITION_R> ListSubjectCode = new List<DTO.ls_AG_EXAM_CONDITION_R>();

            try
            {
                var sql = "SELECT SUBJECT_CODE , GROUP_ID FROM AG_IAS_EXAM_CONDITION_GROUP_D WHERE course_number = '" + typeCode + "'  order BY GROUP_ID , SUBJECT_CODE ";
                OracleDB db = new OracleDB();
                DataTable dt = db.GetDataTable(sql);
                if (dt.Rows.Count > 0)//มีการเซฟไฟล์ลงฐานข้อมูลหลักแล้ว
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DTO.ls_AG_EXAM_CONDITION_R lsSubCode = new DTO.ls_AG_EXAM_CONDITION_R();
                        lsSubCode.SUBJECT_CODE = dt.Rows[i][0].ToString();
                        ListSubjectCode.Add(lsSubCode);

                    }

                }
                else
                {
                    ListSubjectCode = null;
                }


            }
            catch
            {
                ListSubjectCode = null;
            }
            return ListSubjectCode;
        }

        public string GetAssCode_fromUserID(string ID, string ex_code)
        {
            string ASSO_ID = string.Empty;
            string sql = "select agt.comp_code from ag_ias_personal_t agt " +
                         " left outer join ag_exam_license_r apr on agt.comp_code = apr.exam_owner  " +
                          "  where agt.id='" + ID + "' and apr.exam_place_code='" + ex_code + "'";

            OracleDB ora = new OracleDB();
            DataTable dt = ora.GetDataTable(sql);

            if (dt.Rows.Count == 0)
            {
                ASSO_ID = "";
            }
            else
            {
                ASSO_ID = dt.Rows[0][0].ToString();
            }


            return ASSO_ID;
        }

        public string GetTestingNoFrom_fileImport(DTO.ExamHeaderResultTemp head)
        {
            string testingNo = string.Empty;

            DateTime tsDate = head.TESTING_DATE.String_dd_MM_yyyy_ToDate('/', false);
            string testingDate = tsDate.ToString("ddMM") + Convert.ToString(tsDate.Year.ToInt() - 543);
            string examPlaceCode = head.PROVINCE_CODE + head.ASSOCIATE_CODE;


            string sql = "SELECT TESTING_NO FROM AG_EXAM_LICENSE_R " +
                            "WHERE TESTING_DATE = to_date('" + testingDate + "','DDMMYYYY') AND " +
                            "      TEST_TIME_CODE = '" + head.EXAM_TIME_CODE + "' AND " +
                            "      LICENSE_TYPE_CODE = '" + head.LICENSE_TYPE_CODE + "' AND " +
                            "      EXAM_PLACE_CODE = '" + examPlaceCode + "'";


            OracleDB ora = new OracleDB();
            DataTable dt = ora.GetDataTable(sql);

            if (dt.Rows.Count == 0)
            {
                testingNo = "";
            }
            else
            {
                testingNo = dt.Rows[0][0].ToString();
            }

            return testingNo;
        }

        /// <summary>
        /// ดึงข้อมูลผลการสอบจาก Temp
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม Upload</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ExamResultTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ExamResultTemp>>
            GetExamResultUploadByGroupId(string groupId)
        {
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ExamResultTemp>>();
            res.DataResponse = new DTO.UploadResult<DTO.UploadHeader, DTO.ExamResultTemp>();

            res.DataResponse.Header = new List<DTO.UploadHeader>();
            res.DataResponse.Detail = new List<DTO.ExamResultTemp>();

            try
            {

                var details = base.ctx.AG_IAS_APPLICANT_SCORE_D_TEMP
                                      .Where(w => w.UPLOAD_GROUP_NO == groupId)
                                      .ToList();

                foreach (var d in details)
                {
                    var ent = new DTO.ExamResultTemp();
                    d.MappingToEntity(ent);
                    res.DataResponse.Detail.Add(ent);
                }

                int total = res.DataResponse.Detail.Count();
                int missingTrans = res.DataResponse.Detail.Where(w => !string.IsNullOrEmpty(w.ERROR_MSG)).Count();
                int rightTrans = res.DataResponse.Detail.Where(w => string.IsNullOrEmpty(w.ERROR_MSG)).Count();

                var header = base.ctx.AG_IAS_APPLICANT_SCORE_H_TEMP
                                     .SingleOrDefault(s => s.UPLOAD_GROUP_NO == groupId);
                res.DataResponse.Header.Add(new DTO.UploadHeader
                {
                    Totals = total,
                    MissingTrans = missingTrans,
                    RightTrans = rightTrans,
                    UploadFileName = header.FILENAME
                });

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        //UC014 ดึงรายการที่ Upload มาแก้ไข
        /// <summary>
        /// ดึงผลการสอบเพื่อแก้ไข
        /// </summary>
        /// <param name="uploadGroupNo">เลขที่กลุ่ม Upload</param>
        /// <param name="seqNo">ลำดับที่</param>
        /// <returns>ResponseService<ExamResultTempEdit></returns>
        public DTO.ResponseService<DTO.ExamResultTempEdit> GetExamResultTempEdit(string uploadGroupNo, string seqNo)
        {
            var res = new DTO.ResponseService<DTO.ExamResultTempEdit> { DataResponse = new DTO.ExamResultTempEdit() };
            try
            {
                var header = base.ctx.AG_IAS_APPLICANT_SCORE_H_TEMP
                                   .SingleOrDefault(s => s.UPLOAD_GROUP_NO == uploadGroupNo);
                if (header != null)
                {
                    DTO.ExamHeaderResultTemp h = new DTO.ExamHeaderResultTemp();
                    header.MappingToEntity(h);
                    res.DataResponse.Header = h;
                }

                var detail = base.ctx.AG_IAS_APPLICANT_SCORE_D_TEMP
                                     .SingleOrDefault(s => s.UPLOAD_GROUP_NO == uploadGroupNo &&
                                                           s.SEQ_NO == seqNo);
                if (detail != null)
                {
                    DTO.ExamResultTemp d = new DTO.ExamResultTemp();
                    detail.MappingToEntity(d);
                    res.DataResponse.Detail = d;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        //UC014 Update รายการ Temp ที่แก้ไข
        /// <summary>
        /// Update ข้อมูลผลการสอบหลังแก้ไข
        /// </summary>
        /// <param name="exam">ข้อมูลที่ต้องการนำไปแก้ไข</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateExamResultEdit(DTO.ExamResultTempEdit exam)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var header = base.ctx.AG_IAS_APPLICANT_SCORE_H_TEMP
                                     .SingleOrDefault(s => s.UPLOAD_GROUP_NO == exam.Header.UPLOAD_GROUP_NO);
                if (header != null)
                {
                    exam.Header.MappingToEntity(header);
                }

                var detail = base.ctx.AG_IAS_APPLICANT_SCORE_D_TEMP
                                  .SingleOrDefault(s => s.UPLOAD_GROUP_NO == exam.Detail.UPLOAD_GROUP_NO &&
                                                        s.SEQ_NO == exam.Detail.SEQ_NO);
                if (detail != null)
                {
                    exam.Detail.MappingToEntity(detail);
                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return null;
        }

        //UC014 Submit การ Upload คะแนน
        /// <summary>
        /// นำข้อมูลผลการสอบจาก Temp เข้าระบบ
        /// </summary>
        /// <param name="groupId">เลขที่กลุ่ม Upload</param>
        /// <param name="userId">user id</param>
        /// <param name="expireDate">วันหมดอายุ</param>
        /// <returns>ResponseMessage<bool></returns>
        //public DTO.ResponseMessage<bool> ExamResultUploadToSubmit(string groupId, string userId, DateTime? expireDate)
        //{
        //    var res = new DTO.ResponseMessage<bool>();
        //    try
        //    {

        //        //ดึง Header Temp
        //        var header = base.ctx.AG_IAS_APPLICANT_SCORE_H_TEMP
        //                             .SingleOrDefault(w => w.UPLOAD_GROUP_NO == groupId);

        //        //ดึง รายการ Detail Temp ทั้งหมด
        //        var details = base.ctx.AG_IAS_APPLICANT_SCORE_D_TEMP
        //                              .Where(w => w.UPLOAD_GROUP_NO == groupId)
        //                              .ToList();

        //        DateTime tsDate = header.TESTING_DATE.String_dd_MM_yyyy_ToDate('/', false);
        //        string testingDate = tsDate.ToString("ddMM") + tsDate.Year.ToString("0000");
        //        string examPlaceCode = header.PROVINCE_CODE + header.ASSOCIATE_CODE;

        //        #region หารายการสอบจาก AG_EXAM_LICENSE_R
        //        string testingNo =  //Default_TESTING_NO;
        //        #endregion


        //        //Loop แต่ละรายการใน Temp
        //        foreach (AG_IAS_APPLICANT_SCORE_D_TEMP d in details)
        //        {
        //            int appCode = d.SEAT_NO.ToInt();
        //            string id_card = d.ID_CARD_NO;
        //            #region Loop เก็บคะแนนสอบ

        //            for (int j = 1; j <= 16; j++)//ถ้าต้องแก้ใหม่ให้มาแก้ตรงนี้ 07 08 milk
        //            {

        //                //เก็บคะแนนเข้าตัวแปร
        //                object score = d.GetType().GetProperty("SCORE_" + j.ToString()).GetValue(d, null);

        //                //ถ้ามีคะแนน
        //                if (score != null)
        //                {
        //                    //วนเก็บข้อมูลเข้า AG_APPLICANT_SCORT_T     1 รายการคะแนน ต่อ 1 วิชา
        //                    //สร้างรายการ AG_APPLICANT_SCORE_T
        //                    var ent = new AG_APPLICANT_SCORE_T
        //                    {
        //                        APPLICANT_CODE = appCode,
        //                        TESTING_NO = testingNo,
        //                        EXAM_PLACE_CODE = examPlaceCode,
        //                        LICENSE_TYPE_CODE = header.LICENSE_TYPE_CODE,
        //                        USER_ID = userId,
        //                        USER_DATE = DateTime.Now.Date,

        //                    };

        //                    short sc = score.ToShort();
        //                    ent.SUBJECT_CODE = j.ToString("000");
        //                    ent.SCORE = sc;
        //                    base.ctx.AG_APPLICANT_SCORE_T.AddObject(ent);
        //                }
        //            }

        //            #endregion

        //            #region Update AG_APPLICANT_T

        //            DAL.AG_APPLICANT_T app = base.ctx.AG_APPLICANT_T
        //                                             .SingleOrDefault(
        //                                                s => s.ID_CARD_NO == id_card &&
        //                                                     s.TESTING_NO == testingNo &&
        //                                                     s.APPLICANT_CODE == appCode &&
        //                                                     s.EXAM_PLACE_CODE == examPlaceCode);
        //            //Update ผลการสอบ
        //            if (!string.IsNullOrEmpty(d.EXAM_RESULT))
        //            {
        //                app.RESULT = d.EXAM_RESULT;
        //                app.ABSENT_EXAM = "P_F_B".Contains(d.EXAM_RESULT)
        //                                            ? "N"
        //                                            : d.EXAM_RESULT;
        //            }
        //            else
        //            {
        //                app.ABSENT_EXAM = string.Empty;
        //            }

        //            //ถ้า User ใส่วันหมดอายุที่หน้า UI
        //            if (expireDate != null)
        //            {
        //                app.EXPIRE_DATE = expireDate;   //คุณณัฐ ให้เอา ApproveDate จาก Detail มาใส่ 23 เม.ย. 56
        //            }
        //            else
        //            {
        //                if (d.APPROVE_DATE != null)
        //                {
        //                    //วันที่อนุมัติ + 12 เดือน - 1 วัน
        //                    app.EXPIRE_DATE = d.APPROVE_DATE.Value.AddMonths(12).AddDays(-1);
        //                }
        //            }

        //            #endregion
        //        }
        //        //เปิด Transaction
        //        using (var ts = new TransactionScope())
        //        {
        //            base.ctx.SaveChanges(System.Data.Objects.SaveOptions.None);
        //            ts.Complete();
        //        }
        //        res.ResultMessage = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }
        //    return res;
        //}

        public DTO.ResponseMessage<bool> ExamResultUploadToSubmitNew(string groupId, string userId, DateTime? expireDate, string TestingNo)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                string UploadG = string.Empty;
                string Exam_ABSENT = "";
                string ID_no = string.Empty;
                string exam_place = string.Empty;
                //string testingDate = string.Empty;
                string examPlaceCode = string.Empty;
                string testingNo = string.Empty;
                string resss = "";
                List<DTO.ls_AG_EXAM_CONDITION_R> SubjectList = null;
                //ดึง Header Temp
                var header = base.ctx.AG_IAS_APPLICANT_SCORE_H_TEMP
                                     .SingleOrDefault(w => w.UPLOAD_GROUP_NO == groupId);

                //ดึง รายการ Detail Temp ทั้งหมด
                var details = base.ctx.AG_IAS_APPLICANT_SCORE_D_TEMP
                                      .Where(w => w.UPLOAD_GROUP_NO == groupId)
                                      .ToList();

                // GET testingNo

                DateTime tsDate = PhaseStringToDateHelper.ParseDate(header.TESTING_DATE, "dd/MM/yyyy");// .String_dd_MM_yyyy_ToDate('/', false);
                //testingDate = tsDate.ToString("ddMM") + tsDate.Year.ToString("0000");

                testingNo = TestingNo;
                ID_no = details[0].ID_CARD_NO.ToString();

                // Get coursenumber
                //var course = base.ctx.AG_EXAM_LICENSE_R
                //                    .Where(w => w.TESTING_NO == groupId)
                //                    .ToList();

                //string ASSOCIATE_CODE = header.ASSOCIATE_CODE;

                DTO.ExamHeaderResultTemp head = new DTO.ExamHeaderResultTemp();
                head.ASSOCIATE_CODE = header.ASSOCIATE_CODE;
                head.LICENSE_TYPE_CODE = header.LICENSE_TYPE_CODE;
                head.PROVINCE_CODE = header.PROVINCE_CODE;
                head.EXAM_TIME_CODE = header.EXAM_TIME_CODE;
                head.CNT_PER = header.CNT_PER;
                head.FILENAME = header.FILENAME;
                head.TESTING_DATE = header.TESTING_DATE;
                //string strTestingNo = GetTestingNoFrom_fileImport(head);
                var examDetail = GetExamByTestingNo(TestingNo);
                decimal CourseNumber = examDetail.DataResponse.COURSE_NUMBER;


                SubjectList = GetSubjectList(Convert.ToString(CourseNumber));
                if (SubjectList.Count == 0)
                {
                    res.ErrorMsg = "\n" + Resources.errorExamService_017;
                    return res;
                }
                else if (SubjectList == null)
                {
                    res.ErrorMsg = "\n" + Resources.errorExamService_018;
                    return res;
                }

                #region หารายการสอบจาก AG_EXAM_LICENSE_R

                string sql = "SELECT EXAM_PLACE_CODE FROM AG_EXAM_LICENSE_R " +
                             "WHERE " +
                             "      TEST_TIME_CODE = '" + header.EXAM_TIME_CODE + "' AND " +
                             "      LICENSE_TYPE_CODE = '" + header.LICENSE_TYPE_CODE + "' AND " +
                             "      TESTING_NO = '" + TestingNo + "'";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                #endregion

                if (dt.Rows.Count == 0)
                {
                    res.ErrorMsg = Resources.errorExamService_047;
                    return res;
                }
                else
                {
                    examPlaceCode = dt.Rows[0][0].ToString();
                }

                string lic = "";
                string app = "";
                //ทำอะไรก็ไม่รู้ อยากรู้ไปไล่แพ๊คเกจในเบสกะออราเคิลฟอร์มเอา  GET_DATA
                foreach (AG_IAS_APPLICANT_SCORE_D_TEMP d in details)
                {
                    int appCode = d.SEAT_NO.ToInt();

                    #region Loop เก็บคะแนนสอบ
                    string[] SCORE_array = new string[16];
                    string Main_sql = " select distinct agd.subject_code s_subj , agd.max_score s_max " +
                                    "     from ag_ias_exam_condition_group ag,ag_ias_exam_condition_group_d agd " +
                                    "      where ag.license_type_code =  '" + header.LICENSE_TYPE_CODE + "'  and  ag.course_number = agd.course_number and ag.status='A' " +
                                    "     order by 1,2";
                    OracleDB oraMain = new OracleDB();
                    DataTable dtMain = oraMain.GetDataTable(Main_sql);


                    int j = 0;
                    for (int countMain = 0; countMain < dtMain.Rows.Count; countMain++)
                    {
                        j = j + 1;
                        string score = "aia.score_" + j.ToString() + " Rescore ";


                        string a = dtMain.Rows[countMain]["s_subj"].ToString();//j.ToString("000");

                        string sqL =
                            " select distinct aia.seat_no appplicant_code, apt.testing_no testing_no,aas.province_code || aas.associate_code exam_place " +
                                " ,aec.subject_code subject_code, " + score + " " +
                                " ,aas.license_type_code license_type_code " +
                                "  from ag_ias_exam_condition_group_d aec,ag_ias_applicant_score_d_temp aia , " +
                                "  ag_ias_applicant_score_h_temp aas,ag_applicant_t apt " +
                                "  where aec.license_type_code = '" + header.LICENSE_TYPE_CODE + "' " +
                                "  and aia.upload_group_no = '" + groupId + "' " +
                                "  and aas.upload_group_no = aia.upload_group_no and apt.id_card_no=aia.id_card_no " +
                                "  and apt.testing_no = '" + testingNo + "' and aec.subject_code ='" + a + "' " +
                                " and aia.id_card_no = '" + d.ID_CARD_NO + "' " +
                                "  order by aec.subject_code asc ";


                        OracleDB ora2 = new OracleDB();
                        DataTable dt2 = ora2.GetDataTable(sqL);

                        if (dt2.Rows.Count > 0)
                        {
                            int jj = 0;
                            //for (jj = 0; jj < dt2.Rows.Count; jj++)
                            //{
                            try
                            {
                                lic = dt2.Rows[jj]["license_type_code"].ToString();
                                app = dt2.Rows[jj]["appplicant_code"].ToString();
                                string addSQL =
                                      " insert into ag_applicant_score_t (applicant_code,testing_no,exam_place_code,subject_code, " +
                                      " score,license_type_code,USER_ID,USER_DATE) " +
                                      "  values('" + dt2.Rows[jj]["appplicant_code"].ToString() + "', " +
                                      "'" + dt2.Rows[jj]["testing_no"].ToString() + "', " +
                                      "'" + examPlaceCode + "', " +
                                      "'" + dt2.Rows[jj]["subject_code"].ToString() + "', " +
                                      "'" + dt2.Rows[jj]["Rescore"].ToString() + "', " +
                                      "'" + dt2.Rows[jj]["license_type_code"].ToString() + "', " +
                                      "'" + userId + "',to_date('" + DateTime.Now.Date.ToString_yyyyMMdd() + "','YYYYMMDD')) ";
                                OracleDB ora3 = new OracleDB();
                                ora3.GetDataSet(addSQL);
                                SCORE_array[j - 1] = dt2.Rows[jj]["Rescore"].ToString(); //คะแนน
                            }
                            catch (Exception ex)
                            {
                                res.ErrorMsg = "มีการบันทึกคะแนนสอบของเลขที่นั่งสอบ " + dt2.Rows[jj]["appplicant_code"].ToString() + " แล้ว";
                                return res;
                            }
                            //}
                        }
                        // res.ResultMessage = true;
                    }

                    #endregion

                    #region ag_blacklist_apply_exam_t



                    DataTable dt4 = this.checkBlackList(ID_no);
                    DataTable dt5 = this.CheckBlack_List(ID_no, testingNo);
                    resss = this.CheckblacklistAndCheckPassorFall(dt4, dt5, lic, app, testingNo, SCORE_array, false, SubjectList); // B F P

                    #endregion
                    #region //-----------------UPD_EXAM_PASS------------//
                    appCode = app.ToInt();

                    DAL.AG_APPLICANT_T appT = base.ctx.AG_APPLICANT_T
                                                           .SingleOrDefault(
                                                              s => s.APPLICANT_CODE == appCode &&
                                                                   s.TESTING_NO == testingNo &&
                                                                   s.EXAM_PLACE_CODE == examPlaceCode &&
                                                                   (s.RECORD_STATUS == null || s.RECORD_STATUS != "X"));
                    if (appT != null)
                    {
                        //Update ผลการสอบ
                        if (!string.IsNullOrEmpty(d.ID_CARD_NO))
                        {
                            Exam_ABSENT = (d.ABSENT_EXAM != null) ? d.ABSENT_EXAM.ToString() : "N";
                            if (Exam_ABSENT != "M")
                            {
                                appT.RESULT = resss;
                                appT.ABSENT_EXAM = "N";
                            }
                            else
                            {
                                appT.ABSENT_EXAM = "M";
                            }
                            //ถ้า User ใส่วันหมดอายุที่หน้า UI
                            //if (expireDate != null)
                            //{
                            //    appT.EXPIRE_DATE = expireDate;   //คุณณัฐ ให้เอา ApproveDate จาก Detail มาใส่ 23 เม.ย. 56
                            //}
                            if (resss == "B")
                            {
                                appT.RESULT = "B";//แบ๊กลิสต์
                            }
                        }
                    }
                    else
                    {
                        res.ErrorMsg = Resources.errorExamService_049 + appCode + Resources.errorExamService_050 + testingNo + " รหัสสถานที่สอบ" + examPlaceCode;
                        return res;
                    }
                    #endregion //----------------UPD_EXAM_PASS----------------//
                    UploadG = d.UPLOAD_GROUP_NO;


                }
                //เปิด Transaction
                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }


                #region Update C in score_d_temp
                try
                {
                    string UpdateDtemp = " update AG_IAS_APPLICANT_SCORE_D_TEMP set STATUS_SAVE_SCORE='C' where UPLOAD_GROUP_NO = '" + UploadG + "'";
                    OracleDB ODtemp = new OracleDB();
                    ODtemp.GetDataTable(UpdateDtemp);
                    res.ResultMessage = true;
                }
                catch (Exception ex)
                {
                    res.ErrorMsg = Resources.errorExamService_052;
                    return res;
                }
                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                return res;
            }
            return res;
        }
        //จับแยกออกมาเพื่อเช็คค่าต่างๆก่อน
        public DataTable checkBlackList(string ID_no)
        {
            string rec_blacklist =
                           " Select APPLICANT_CODE,TESTING_NO,EXAM_PLACE_CODE " +
                           " From AG_APPLICANT_T " +
                            " Where Id_card_no = (Select Id_card_no " +
                           "  											 From AG_BLACKLIST_APPLY_EXAM_T " +
                           "  											Where Id_card_no = '" + ID_no + "' " +
                           "  											And NVL(REVOKE_DATE,SYSDATE) <= TRUNC(SYSDATE)   " +
                           " 									And NVL(REVOKE_END_DATE,SYSDATE) >= TRUNC(SYSDATE) " +
                           " 									And NVL(STATUS,'A') <> 'B')  " +
                           "  And Result Is Not Null  " +
                           "  And Result = 'B' ";

            OracleDB ora4 = new OracleDB();
            return ora4.GetDataTable(rec_blacklist);

        }
        public DataTable CheckBlack_List(string ID_no, string testingNo)
        {
            string insurance =
                     " select aat.applicant_code v_app_code, aat.testing_no v_no_test,aat.exam_place_code v_exam_place_code " +
                       " from ag_applicant_t aat          " +
                       " where aat.id_card_no ='" + ID_no + "'  " +
                       " and aat.testing_no = '" + testingNo + "'";


            OracleDB ora5 = new OracleDB();
            return ora5.GetDataTable(insurance);
        }
        public string CheckblacklistAndCheckPassorFall(DataTable dt4, DataTable dt5, string lic, string app, string testingNo, string[] score, Boolean checkfirst, List<DTO.ls_AG_EXAM_CONDITION_R> SubjectList)
        {
            string resss = string.Empty;
            string v_license_type = string.Empty;
            string v_insurance_type = string.Empty;
            string v_license_Blacklist = string.Empty;
            string v_insurance_Blacklist = string.Empty;
            if (dt5.Rows.Count > 0)
            {
                v_license_type = ag_get_license_type(dt5.Rows[0]["v_app_code"].ToString(), dt5.Rows[0]["v_no_test"].ToString(), dt5.Rows[0]["v_exam_place_code"].ToString());
                v_insurance_type = ag_get_insurance_type(v_license_type);

                int i = 1;
                if (dt4.Rows.Count > 0)
                {
                    for (i = 1; i <= dt4.Rows.Count; i++)
                    {
                        v_license_Blacklist = ag_get_license_type(dt4.Rows[0]["APPLICANT_CODE"].ToString(), dt4.Rows[0]["TESTING_NO"].ToString(), dt4.Rows[0]["EXAM_PLACE_CODE"].ToString());
                        v_insurance_Blacklist = ag_get_insurance_type(v_license_type);

                        if (v_insurance_type == v_insurance_Blacklist)
                        {
                            resss = "B";
                        }
                    }
                }
            }
            if (resss != "B")
            {
                resss = CAL_EXAM_PASS(lic, app, testingNo, score, checkfirst, SubjectList);
            }
            return resss;
        }
        //จับแยกออกมาโดยมิ้ว
        public string ag_get_license_type(string v_app_code, string v_no_test, string v_exam_place_code)
        {
            string abc = "";
            string str = " select distinct aas.license_type_code v_license_type " +
                         " from ag_applicant_score_t aas " +
                         " where aas.applicant_code = '" + v_app_code + "' " +
                         " and aas.testing_no = '" + v_no_test + "' " +
                         " and aas.exam_place_code ='" + v_exam_place_code + "' ";

            OracleDB ora = new OracleDB();
            DataTable dt = ora.GetDataTable(str);
            if (dt.Rows.Count > 0)
            {
                abc = dt.Rows[0][0].ToString();
            }

            return abc;
        }
        public string ag_get_insurance_type(string v_license_type)
        {
            string abc = "";
            string str = " select alt.insurance_type v_insurance_type " +
                         " from ag_license_type_r alt " +
                         " where alt.license_type_code = '" + v_license_type + "'";
            OracleDB ora = new OracleDB();
            DataTable dt = ora.GetDataTable(str);
            if (dt.Rows.Count > 0)
            {
                abc = dt.Rows[0][0].ToString();
            }
            return abc;
        }

        public DTO.ResponseService<DataSet> GetSubject_List(string lic_type_code)
        {
            var SubList = new DTO.ResponseService<DataSet>();
            string Ssql = "SELECT  dd.subject_code , rr.subject_name ,DD.MAX_SCORE FullScore , dd.group_id  FROM AG_IAS_EXAM_CONDITION_GROUP_D DD INNER JOIN AG_SUBJECT_R RR ON DD.LICENSE_TYPE_CODE = RR.LICENSE_TYPE_CODE AND DD.SUBJECT_CODE = RR.SUBJECT_CODE where DD.COURSE_NUMBER = '" + lic_type_code + "' order by dd.group_id , dd.subject_code ";
            OracleDB oraSql = new OracleDB();
            SubList.DataResponse = oraSql.GetDataSet(Ssql);
            return SubList;

        }
        public string CAL_EXAM_PASS(string lic, string app, string testNo, string[] score, Boolean Checkfirst, List<DTO.ls_AG_EXAM_CONDITION_R> SubjectList)
        {
            //if checkfirst = false then select from ag_applicant_score_t (OIC BASE) else select in temp(AR BASE)
            int v_score = 0;
            int v_max = 0;
            float v_avg = 0;
            int s_score = 0;
            int m_pass = 0;
            string res = "";

            string s_subj = string.Empty;
            int s_max = 0;
            //string Main_sql = "select distinct aec.grp_subject_code m_grp, aec.exam_pass m_pass " +
            //       " from ag_exam_condition_r aec " +
            //       " where aec.license_type_code = '" + lic + "' " +
            //       " order by 1,2"; //เอากลุ่ม และคะแนนผ่านของกลุ่ม มิว

            string Main_sql = "SELECT DISTINCT  " +
            "	ASG. ID m_grp,  " +
            "	ASG.EXAMPASS m_pass  " +
            "FROM  " +
            "	AG_EXAM_LICENSE_R AEL  " +
            "INNER JOIN AG_IAS_EXAM_SUBJECT_GROUP ASG ON AEL.COURSE_NUMBER = ASG.COURSE_NUMBER  " +
            "WHERE  " +
            "	AEL.TESTING_NO = '" + testNo + "'  " +
            "ORDER BY  " +
            "	1,  " +
            "	2";  // น้องนะคับ

            string sss = ctx.AG_IAS_USERS.First().DELETE_USER;
            OracleDB oraMain = new OracleDB();
            DataTable dtMain = oraMain.GetDataTable(Main_sql);

            if (dtMain.Rows.Count > 0)
            {
                int ii = 0;
                int tempScore = 0;
                for (ii = 0; ii < dtMain.Rows.Count; ii++)
                {
                    v_score = 0;
                    v_max = 0;
                    string m_grp = dtMain.Rows[ii]["m_grp"].ToString();
                    m_pass = Convert.ToInt32(dtMain.Rows[ii]["m_pass"].ToString()); //----- ตอนแรกลืมเก็บคะแนนผ่านไว้ - - 
                    #region //------------------ SUBJ ---------------//


                    string SUBJ_sql = "SELECT DISTINCT  " +
                    "	AD.SUBJECT_CODE s_subj,  " +
                    "	AD.MAX_SCORE s_max  " +
                    "FROM  " +
                    "	AG_IAS_EXAM_CONDITION_GROUP_D AD  " +
                    "INNER JOIN AG_EXAM_LICENSE_R AEL ON AD.COURSE_NUMBER = AEL.COURSE_NUMBER  " +
                    "WHERE  " +
                    "AD.GROUP_ID = '" + m_grp + "'  " +
                    "AND AEL.TESTING_NO = '" + testNo + "'";

                    OracleDB oraSUBJ = new OracleDB();
                    DataTable dtSUBJ = oraSUBJ.GetDataTable(SUBJ_sql);
                    int SJ = 0;
                    s_score = 0;
                    for (SJ = 0; SJ < dtSUBJ.Rows.Count; SJ++)
                    {
                        s_max = dtSUBJ.Rows[SJ]["S_MAX"].ToInt() == null ? 100 : dtSUBJ.Rows[SJ]["S_MAX"].ToInt();//คะแนนเต็ม เอาไปใช้เวลาคำนวนเปอร์เซนต์ผ่าน
                        s_subj = dtSUBJ.Rows[SJ]["S_SUBJ"].ToString();//รหัสวิชา(มั้ง)
                        v_max = ((v_max == null) ? 100 : v_max) + s_max; //คะแนนสอบที่ได้(มั้ง)

                        #region //------------------------Cal_Score_All-----------------//
                        if (app != null)
                        {
                            if (!Checkfirst)
                            {
                                string BI = "select a.score  as BI_SCORE from ag_applicant_score_t a " +
                                           " where a.testing_no = '" + testNo + "' and a.applicant_code = '" + app + "' " +
                                           " and a.subject_code = '" + s_subj + "' " +
                                           " and a.license_type_code = '" + lic + "' order by 1"; //ลืมจ๊ะ
                                OracleDB oraBI = new OracleDB();
                                DataTable dtBI = oraBI.GetDataTable(BI);//ค่าได้จากการที่ตัวเอง inset ลงไปตั้งแต่ฟังก์ชั่นก่อนหน้าที่มาเรียกตัวนี้
                                if (dtBI.Rows.Count > 0)
                                {
                                    int k = 0;
                                    for (k = 0; k < dtBI.Rows.Count; k++)//วนเก็บค่าคะแนน ในประเภทกรุ๊ป(1,2) เพื่อ รวมกันแล้วไปคิดเปอร์เซนต์ผ่าน
                                    {
                                        v_score = dtBI.Rows[k]["BI_SCORE"].ToInt();

                                        s_score = ((s_score == null) ? 0 : s_score) + ((v_score == null) ? 0 : v_score);
                                    }
                                }
                            }
                            //addbymilk
                            else
                            {
                                if (score != null)
                                {
                                    if (score[tempScore] != "")
                                    {
                                        v_score = Convert.ToInt32(score[tempScore]);
                                    }
                                    else
                                    {
                                        v_score = 0;
                                    }
                                    if (v_score > s_max)
                                        res = (res != "") ? res + "," + s_subj.ToInt() + " " : " " + s_subj.ToInt() + " ";
                                    s_score = ((s_score == null) ? 0 : s_score) + ((v_score == null) ? 0 : v_score);
                                    tempScore++;
                                }
                            }
                        }
                        #endregion  //------------------------End Cal_Score_All-----------------//
                    }
                    v_score = s_score; //ส่งค่ากลับ
                    #endregion //---------------------- ENd SUBJ -------------------//

                    float f_temp = (v_score.ToFloat() / v_max.ToFloat()) * 100;
                    v_avg = (v_max > 0) ? f_temp : 0; //--- เพราะค่าเฉลี่ยเลยทำให้สอบไม่ผ่านหมดแม้คะแนนผ่าน - - ?
                    if (((v_avg == null) ? 0 : v_avg) < ((m_pass == null) ? 0 : m_pass))
                    {
                        ii = dtMain.Rows.Count;
                    }

                }
                res = (res != "") ? "คะแนนที่ได้รับ มากกว่าคะแนนเต็ม (วิชาที่ " + res + ")" : (((v_avg == null) ? 0 : v_avg) >= ((m_pass == null) ? 0 : m_pass)) ? "P" : "F";


            }
            return res;
        }

        public void Dispose()
        {
            if (ctx != null) ctx.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// ดึงวันที่มีการการสอบโดยระบุเงื่อนไข
        /// </summary>
        /// <param name="examPlaceGroupCode">รหัสกลุ่มสนามสอบ</param>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <param name="licenseTypeCode">ประเภทใบอนุญาต</param>
        /// <param name="yearMonth">ปีเดือนที่สอบ</param>
        /// <param name="timeCode">รหัสเวลาสอบ</param>
        /// <param name="testingDate">วันที่สอบ</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetExamMonthByCriteria(string examPlaceGroupCode, string examPlaceCode,
                                                                       string licenseTypeCode, string yearMonth,
                                                                       string timeCode, string testingDate, string Owner = "")
        {

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };
                //DateTime dtBegin = new DateTime(Convert.ToInt32(yearMonth.Substring(0, 4)), Convert.ToInt32(yearMonth.Substring(5, 2)), 25);
                //dtBegin.AddMonths(-1);
                //DateTime dtEnd = new DateTime(Convert.ToInt32(yearMonth.Substring(0, 4)), Convert.ToInt32(yearMonth.Substring(5, 2)), 25);
                //dtEnd.AddMonths(1);
                //long lyearMonth = Convert.ToInt64(yearMonth);
                String dateBegin = DateTime.ParseExact(yearMonth, "yyyyMM", null).AddMonths(-1).ToString("yyyyMM") + "25"; //Convert.ToString(dtBegin.ToString("yyyyMMdd"));
                String dateEnd = DateTime.ParseExact(yearMonth, "yyyyMM", null).AddMonths(1).ToString("yyyyMM") + "07";//Convert.ToString(dtEnd.ToString("yyyyMMdd"));
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("PL.EXAM_PLACE_GROUP_CODE like '{0}%' AND ", examPlaceGroupCode));
                sb.Append(GetCriteria("LR.EXAM_PLACE_CODE like '{0}%' AND ", examPlaceCode));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", licenseTypeCode));
                sb.Append("LR.TESTING_DATE BETWEEN TO_DATE(" + dateBegin + ",'YYYYMMDD') AND TO_DATE(" + dateEnd + ",'YYYYMMDD') AND ");
                sb.Append(GetCriteria("LR.TEST_TIME_CODE = '{0}' AND ", timeCode));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}'  ", testingDate));
                sb.Append(GetCriteria("LR.EXAM_OWNER like '{0}%' AND ", Owner));
                //sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));
                //sb.Append(GetCriteria("(LR.EXAM_STATE = '{0}' or EXAM_STATE= '{1}' )", "A", "M"));
                //sb.Append("LR.EXAM_STATE = 'M' ");
                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                string sql = "SELECT distinct LR.TESTING_DATE " +
                             "FROM  " +
                             "AG_EXAM_LICENSE_R LR, " +
                             "AG_EXAM_PLACE_R PL " +
                             "WHERE " +
                             crit +
                             " AND LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE  AND LR.EXAM_STATE in ('A','M') Order By LR.TESTING_DATE ";


                OracleDB db = new OracleDB();
                //DataSet ds = ds = db.GetDataSet(sql);
                DataSet ds = db.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }



        /// <summary>
        /// ดึงข้อมูลการสอบโดยระบุเงื่อนไข
        /// </summary>
        /// <param name="examPlaceGroupCode">รหัสกลุ่มสนามสอบ</param>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <param name="licenseTypeCode">ประเภทใบอนุญาต</param>
        /// <param name="yearMonth">ปีเดือนที่สอบ</param>
        /// <param name="timeCode">รหัสเวลาสอบ</param>
        /// <param name="testingDate">วันที่สอบ</param>
        /// <returns>ResponseService<DataSet></returns>
        public DTO.ResponseService<DataSet> GetExamByCriteriaDefault(string examPlaceGroupCode, string examPlaceCode,
                                                                     string licenseTypeCode, string agentType, string yearMonth,
                                                                     string timeCode, string testingDate, int resultPage, int PageSize, Boolean CountAgain, string Owner = "")
        {

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };

                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("PL.EXAM_PLACE_GROUP_CODE like '{0}%' AND ", examPlaceGroupCode));
                sb.Append(GetCriteria("LR.EXAM_PLACE_CODE like '{0}%' AND ", examPlaceCode));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", licenseTypeCode));
                sb.Append(GetCriteria("LT.AGENT_TYPE = '{0}' AND ", agentType));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMM') = '{0}' AND ", yearMonth));
                sb.Append(GetCriteria("LR.TEST_TIME_CODE = '{0}' AND ", timeCode));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                #region MILK
                string firstCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;

                if (CountAgain)
                {
                    firstCon = " SELECT COUNT(*) CCount FROM ( ";
                    midCon = " ";
                    lastCon = " )";
                }
                else
                {
                    if (resultPage == 0 && PageSize == 0)
                    {

                    }
                    else
                    {
                        firstCon = " SELECT * FROM (";
                        midCon = " , ROW_NUMBER() OVER (ORDER BY LR.TESTING_DATE) RUN_NO ";
                        lastCon = resultPage == 0
                                        ? "" :
                                        " Order By LR.TESTING_DATE ) A where A.RUN_NO BETWEEN " +
                                           resultPage.StartRowNumber(PageSize).ToString() + " AND " +

                                           resultPage.ToRowNumber(PageSize).ToString() + " order by A.RUN_NO asc ";

                    }
                }


                #endregion MILK
                string sql = firstCon + "SELECT distinct LR.TESTING_NO, LR.TESTING_DATE, TM.TEST_TIME,LR.EXAM_OWNER , (select ASSOCIATION_NAME from AG_IAS_ASSOCIATION where ASSOCIATION_CODE = LR.EXAM_OWNER)EXAM_OWNER_Name , " +
                             " case when GR.EXAM_PLACE_GROUP_NAME is null then ASSO.ASSOCIATION_NAME else GR.EXAM_PLACE_GROUP_NAME end EXAM_PLACE_GROUP_NAME,PL.EXAM_PLACE_NAME,PV.NAME PROVINCE, AG.AGENT_TYPE, " +
                             "(Select Count(*) " +
                             "From AG_APPLICANT_T exLr " +
                             "Where EXLR.TESTING_NO = LR.TESTING_NO AND " +
                             "EXLR.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND (EXLR.RECORD_STATUS IS NULL or EXLR.RECORD_STATUS != 'X')) TOTAL_APPLY, " +
                             "LR.EXAM_ADMISSION, LT.LICENSE_TYPE_NAME, LR.EXAM_FEE, " +
                             "LR.TEST_TIME_CODE,LR.EXAM_PLACE_CODE,case when GR.EXAM_PLACE_GROUP_CODE is null " +
                             " then ASSO.ASSOCIATION_CODE else GR.EXAM_PLACE_GROUP_CODE end EXAM_PLACE_GROUP_CODE , " +
                             "PL.PROVINCE_CODE,LR.LICENSE_TYPE_CODE, " +
                             " LR.EXAM_APPLY || '/'|| LR.EXAM_ADMISSION SEAT_AMOUNT " +
                    // " (Select Count(*) From AG_APPLICANT_T exLr Where EXLR.TESTING_NO = LR.TESTING_NO AND EXLR.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND EXLR.RECORD_STATUS IS NULL) || '/'||  PL.SEAT_AMOUNT  SEAT_AMOUNT " +
                             " " + midCon +
                             "FROM  " +
                             "AG_EXAM_LICENSE_R LR, AG_EXAM_TIME_R TM, vw_ias_province PV, AG_LICENSE_TYPE_R LT, AG_AGENT_TYPE_R AG, AG_EXAM_PLACE_R PL " +
                             "left join AG_EXAM_PLACE_GROUP_R GR on PL.EXAM_PLACE_GROUP_CODE = GR.EXAM_PLACE_GROUP_CODE " +
                             " left join AG_IAS_ASSOCIATION ASSO on pl.association_code = ASSO.ASSOCIATION_CODE " +
                             "  WHERE LR.TEST_TIME_CODE = TM.TEST_TIME_CODE AND LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE " +
                             " AND PL.PROVINCE_CODE = PV.ID AND LR.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE AND AG.AGENT_TYPE = LT.AGENT_TYPE " +
                             " AND LR.EXAM_STATE in ('A','M') and lr.exam_owner like '" + Owner + "%' " +
                    //"AG_EXAM_LICENSE_R LR, " +
                    //"AG_EXAM_TIME_R TM, " +
                    //"AG_EXAM_PLACE_GROUP_R GR, " +
                    //"AG_EXAM_PLACE_R PL, " +
                    //"vw_ias_province PV, " +
                    //"AG_LICENSE_TYPE_R LT, " +
                    //"AG_AGENT_TYPE_R AG " +
                    //"WHERE " +
                    //"LR.TEST_TIME_CODE = TM.TEST_TIME_CODE AND " +
                    //"LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE AND " +
                    //"PL.EXAM_PLACE_GROUP_CODE = GR.EXAM_PLACE_GROUP_CODE AND " +
                    //"PL.PROVINCE_CODE = PV.ID AND " +
                    //"LR.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE AND " +
                    //"  lr.exam_owner like '" + Owner + "%' and " +
                    //"AG.AGENT_TYPE = LT.AGENT_TYPE " +
                    //" and (LR.EXAM_STATE = 'A' or LR.EXAM_STATE = 'M') " +
                             crit + " " + lastCon;

                OracleDB db = new OracleDB();
                //DataSet ds = ds = db.GetDataSet(sql);
                DataSet ds = db.GetDataSet(sql);
                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        #region จัดการวันหยุด
        //ดึงรายการวันหยุด
        public DTO.ResponseService<List<DTO.GBHoliday>> GetHolidayList(int page, int count)
        {
            DTO.ResponseService<List<DTO.GBHoliday>> res = new DTO.ResponseService<List<DTO.GBHoliday>>();
            try
            {
                using (var ctx = new IASGBModelEntities())
                {
                    int start = page * count;
                    int end = start - count;
                    string sql = "SELECT * FROM ( SELECT GHR.HL_DATE,HL_DESC,GHC.COUNT,rownum AS NUM FROM GB_HOLIDAY_R GHR,";
                    sql += "(SELECT COUNT(*) as COUNT FROM GB_HOLIDAY_R) GHC)  ";
                    sql += "WHERE NUM >" + end + " AND NUM <= " + start;
                    var list2 = ctx.ExecuteStoreQuery<DTO.GBHoliday>(sql).ToList();
                    res.DataResponse = list2;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        public DTO.ResponseService<DTO.GBHoliday> GetHoliday(string date)
        {
            throw new NotImplementedException();
        }
        //เพิ่มวันหยุด
        public DTO.ResponseService<string> AddHoliday(DTO.GBHoliday holiday)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                using (var ctx = new IASGBModelEntities())
                {
                    if (ctx.GB_HOLIDAY_R.FirstOrDefault(x => x.HL_DATE == holiday.HL_DATE) == null)
                    {
                        GB_HOLIDAY_R holidayr = new GB_HOLIDAY_R();
                        holidayr.HL_DATE = holiday.HL_DATE;
                        holidayr.HL_DESC = holiday.HL_DESC;
                        holidayr.USER_DATE = DateTime.Now;
                        holidayr.USER_ID = "AGDOI";
                        ctx.GB_HOLIDAY_R.AddObject(holidayr);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        res.ErrorMsg = Resources.errorExamService_053;
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        //ลบวันหยุด
        public DTO.ResponseService<string> DeleteHoliday(string date)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                using (var ctx = new IASGBModelEntities())
                {
                    var hdate = Convert.ToDateTime(date);
                    GB_HOLIDAY_R holiday = ctx.GB_HOLIDAY_R.FirstOrDefault(x => x.HL_DATE == hdate);
                    ctx.DeleteObject(holiday);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        //แก้ไขวันหยุด
        public DTO.ResponseService<string> UpdateHoliday(DTO.GBHoliday holidate)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                using (var ctx = new IASGBModelEntities())
                {
                    GB_HOLIDAY_R holidayr = ctx.GB_HOLIDAY_R.FirstOrDefault(x => x.HL_DATE == holidate.HL_DATE);
                    holidayr.HL_DESC = holidate.HL_DESC;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        //ค้นหาวันหยุด
        public DTO.ResponseService<List<DTO.GBHoliday>> SearchHoliday(string search, int page, int count)
        {
            DTO.ResponseService<List<DTO.GBHoliday>> res = new DTO.ResponseService<List<DTO.GBHoliday>>();
            try
            {
                int start = page * count;
                int end = start - count;
                using (var ctx = new IASGBModelEntities())
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(search.Trim(), "dd/MM/yyyy", null, DateTimeStyles.None, out dt) == true || DateTime.TryParseExact(search.Trim(), "d/M/yyyy", null, DateTimeStyles.None, out dt) == true || DateTime.TryParseExact(search.Trim(), "d/M/yy", null, DateTimeStyles.None, out dt) == true)
                    {

                        string sql = "SELECT * FROM (SELECT TT.*,rownum AS NUM FROM ";
                        sql += "( ";
                        sql += "SELECT GHR.HL_DATE,HL_DESC,GHC.COUNT FROM GB_HOLIDAY_R GHR , ";
                        sql += "( ";
                        sql += "SELECT COUNT(*) as COUNT FROM GB_HOLIDAY_R  WHERE to_char(to_date(HL_DATE,'dd/mm/YYYY'))=to_char(to_date('" + dt.Day + "/" + dt.Month + "/" + dt.Year + "','dd/mm/yyyy'))  ";
                        sql += ") GHC";
                        sql += ") TT  WHERE to_char(to_date(HL_DATE,'dd/mm/YYYY'))=to_char(to_date('" + dt.Day + "/" + dt.Month + "/" + dt.Year + "','dd/mm/yyyy')) ";
                        sql += ") WHERE NUM > " + end + " AND NUM < " + start;
                        var list = ctx.ExecuteStoreQuery<DTO.GBHoliday>(sql).ToList();
                        res.DataResponse = list;
                    }
                    else
                    {
                        string sql = "SELECT * FROM (SELECT TT.*,rownum AS NUM FROM ";
                        sql += "( ";
                        sql += "SELECT GHR.HL_DATE,HL_DESC,GHC.COUNT FROM GB_HOLIDAY_R GHR , ";
                        sql += "( ";
                        sql += "SELECT COUNT(*) as COUNT FROM GB_HOLIDAY_R  WHERE HL_DESC LIKE '%" + search + "%' ";
                        sql += ") GHC";
                        sql += ") TT  WHERE HL_DESC LIKE '%" + search + "%'";
                        sql += ") WHERE NUM > " + end + " AND NUM < " + start;
                        var list = ctx.ExecuteStoreQuery<DTO.GBHoliday>(sql).ToList();
                        res.DataResponse = list;
                    }
                }
            }

            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        //ค้นหาวันหยุดในปฏิทิน
        public DTO.ResponseService<List<DTO.GBHoliday>> GetHolidayListByYearMonth(string yearMonth)
        {
            DTO.ResponseService<List<DTO.GBHoliday>> res = new DTO.ResponseService<List<DTO.GBHoliday>>();
            try
            {
                using (var ctx = new IASGBModelEntities())
                {

                    String dateBegin = DateTime.ParseExact(yearMonth, "yyyyMM", null).AddMonths(-1).ToString("yyyyMM") + "25"; //Convert.ToString(dtBegin.ToString("yyyyMMdd"));
                    String dateEnd = DateTime.ParseExact(yearMonth, "yyyyMM", null).AddMonths(1).ToString("yyyyMM") + "07";//Convert.ToString(dtEnd.ToString("yyyyMMdd"));

                    string sql = "SELECT GHR.HL_DATE , GHR.HL_DESC FROM GB_HOLIDAY_R GHR ";
                    sql += "WHERE GHR.HL_DATE BETWEEN TO_DATE(" + dateBegin + ",'YYYYMMDD') AND TO_DATE(" + dateEnd + ",'YYYYMMDD')  ";
                    var list2 = ctx.ExecuteStoreQuery<DTO.GBHoliday>(sql).ToList();
                    res.DataResponse = list2;
                }

            }

            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        #endregion

        #region จัดการวิชาสอบ

        public DTO.ResponseService<List<DTO.LicenseTyperDropDrown>> GetLicensetypeList()
        {
            DTO.ResponseService<List<DTO.LicenseTyperDropDrown>> res = new DTO.ResponseService<List<DTO.LicenseTyperDropDrown>>();
            try
            {
                using (var ctx = new IASPersonEntities())
                {
                    var list = ctx.ExecuteStoreQuery<DTO.LicenseTyperDropDrown>("select LICENSE_TYPE_CODE,LICENSE_TYPE_NAME from AG_LICENSE_TYPE_R").ToList();
                    res.DataResponse = list.ToList();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }



        #endregion

        public DTO.ResponseService<DataSet> GetExamRoom()
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select A.EXAM_ROOM_CODE, A.EXAM_ROOM_NAME, A.SEAT_AMOUNT, A.EXAM_PLACE_CODE, "
                            + " B.EXAM_PLACE_NAME || '[' || D.NAME || ']' EXAM_PLACE_NAME, B.SEAT_AMOUNT SEAT_TOTAL,"
                            + " C.EXAM_PLACE_GROUP_CODE , C.EXAM_PLACE_GROUP_NAME "
                            + " from AG_IAS_EXAM_PLACE_ROOM A "
                            + " inner join AG_EXAM_PLACE_R B on A.EXAM_PLACE_CODE = B.EXAM_PLACE_CODE "
                            + " inner join AG_EXAM_PLACE_GROUP_R C on B.EXAM_PLACE_GROUP_CODE = C.EXAM_PLACE_GROUP_CODE "
                            + " inner join VW_IAS_PROVINCE D on B.PROVINCE_CODE = D.ID "
                            + " where A.ACTIVE = 'Y' and B.ACTIVE ='Y'";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> InsertExamRoom(DTO.ConfigExamRoom ent, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                int count = base.ctx.AG_IAS_EXAM_PLACE_ROOM.Where(s =>
                                s.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE &&
                                s.EXAM_ROOM_CODE == ent.EXAM_ROOM_CODE).Count();
                if (count > 0)
                {
                    res.ErrorMsg = "รหัสห้องสอบซ้ำ";
                    return res;
                }
                //string sql = string.Format(" select NVL(sum(SEAT_AMOUNT),0) total  from AG_IAS_EXAM_PLACE_ROOM "
                //                         + " where EXAM_PLACE_CODE = '{0}' ", ent.EXAM_PLACE_CODE);
                //OracleDB ora = new OracleDB();
                //DataTable dt = ora.GetDataTable(sql);
                int seat_amount = this.GetSeatAmount(ent.EXAM_PLACE_CODE).DataResponse.ToInt();
                //int seat_use = dt.Rows[0]["total"].ToInt();
                if (ent.SEAT_AMOUNT > seat_amount)// (ent.SEAT_AMOUNT > (seat_amount - seat_use))
                {
                    res.ErrorMsg = "จำนวนที่นั่งไม่เพียงพอ";
                    return res;
                }

                AG_IAS_EXAM_PLACE_ROOM table = new AG_IAS_EXAM_PLACE_ROOM();
                table.USER_ID = userProfile.Id;
                table.USER_DATE = DateTime.Now;
                table.EXAM_ROOM_CODE = ent.EXAM_ROOM_CODE;
                table.EXAM_ROOM_NAME = ent.EXAM_ROOM_NAME;
                table.SEAT_AMOUNT = Convert.ToInt16(ent.SEAT_AMOUNT);
                table.EXAM_PLACE_CODE = ent.EXAM_PLACE_CODE;
                table.ACTIVE = "Y";
                base.ctx.AG_IAS_EXAM_PLACE_ROOM.AddObject(table);
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateExamRoom(DTO.ConfigExamRoom ent, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {

                int seat_amount = this.GetSeatAmount(ent.EXAM_PLACE_CODE).DataResponse.ToInt();
                if (ent.SEAT_AMOUNT > seat_amount)//(ent.SEAT_AMOUNT > (seat_amount - seat_use))
                {
                    res.ErrorMsg = "จำนวนที่นั่งไม่เพียงพอ";
                    return res;
                }

                AG_IAS_EXAM_PLACE_ROOM table = ctx.AG_IAS_EXAM_PLACE_ROOM.FirstOrDefault(x => x.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE && x.ACTIVE == "Y" && x.EXAM_ROOM_CODE == ent.EXAM_ROOM_CODE);

                table.USER_ID_UPDATE = userProfile.Id;
                table.USER_DATE_UPDATE = DateTime.Now;
                table.ACTIVE = "Y";
                table.EXAM_ROOM_NAME = ent.EXAM_ROOM_NAME;
                table.SEAT_AMOUNT = Convert.ToInt16(ent.SEAT_AMOUNT);
                table.EXAM_PLACE_CODE = ent.EXAM_PLACE_CODE;
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }


        #region subject

        public DTO.ResponseService<List<DTO.Subjectr>> GetSubjectrList(string licensecode)
        {
            DTO.ResponseService<List<DTO.Subjectr>> res = new DTO.ResponseService<List<DTO.Subjectr>>();
            try
            {
                using (var ctx = new IASPersonEntities())
                {
                    string sql = "SELECT  " +
                    "	ASR.SUBJECT_CODE,  " +
                    "	ASR.SUBJECT_NAME,  " +
                    "	ASR.LICENSE_TYPE_CODE,  " +
                    "	ALT.LICENSE_TYPE_NAME,  " +
                    "	ASG.GROUP_NAME,  " +
                    "	ASG.EXAM_PASS,  " +
                    "	ASR. GROUP_ID,  " +
                    "	ASR.MAX_SCORE  " +
                    "FROM  " +
                    "	AG_SUBJECT_R ASR  " +
                    "INNER JOIN AG_LICENSE_TYPE_R ALT ON ASR.LICENSE_TYPE_CODE = ALT.LICENSE_TYPE_CODE  " +
                    "INNER JOIN AG_IAS_SUBJECT_GROUP ASG ON ASR. GROUP_ID = ASG. ID  " +
                    "WHERE  " +
                    "	ASR.LICENSE_TYPE_CODE = '" + licensecode + "' " +
                    "AND ASR.STATUS = 'A' ORDER BY ASR. GROUP_ID,ASR.SUBJECT_NAME ASC";
                    var list = ctx.ExecuteStoreQuery<DTO.Subjectr>(sql).ToList();
                    res.DataResponse = list.ToList();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        public DTO.ResponseService<string> AddSubject(DTO.Subjectr subject)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {

                using (var ctx = new IASPersonEntities())
                {
                    var dd = ctx.AG_SUBJECT_R.FirstOrDefault(x => x.SUBJECT_CODE == subject.SUBJECT_CODE && x.LICENSE_TYPE_CODE == subject.LICENSE_TYPE_CODE);
                    if (dd == null)
                    {
                        AG_SUBJECT_R asr = new AG_SUBJECT_R();
                        asr.SUBJECT_CODE = subject.SUBJECT_CODE;
                        asr.SUBJECT_NAME = subject.SUBJECT_NAME;
                        asr.LICENSE_TYPE_CODE = subject.LICENSE_TYPE_CODE;
                        asr.MAX_SCORE = subject.MAX_SCORE;
                        asr.GROUP_ID = subject.GROUP_ID;
                        asr.USER_DATE = DateTime.Now;
                        asr.STATUS = "A";
                        asr.USER_ID = subject.USER_ID;
                        ctx.AG_SUBJECT_R.AddObject(asr);

                        ctx.SaveChanges();
                    }
                    else
                    {
                        res.ErrorMsg = Resources.errorExamService_054;
                    }
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }
        public DTO.ResponseService<string> UpdateSubject(DTO.Subjectr subject)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                using (var ctx = new IASPersonEntities())
                {

                    var subject_r = ctx.AG_SUBJECT_R.FirstOrDefault(x => x.SUBJECT_CODE == subject.SUBJECT_CODE && x.LICENSE_TYPE_CODE == subject.LICENSE_TYPE_CODE);
                    subject_r.SUBJECT_NAME = subject.SUBJECT_NAME;
                    subject_r.MAX_SCORE = subject.MAX_SCORE;
                    //subject_r.GROUP_ID = subject.GROUP_ID;
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }
        public DTO.ResponseService<string> DeleteSubject(DTO.Subjectr subject)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {

                using (var ctx = new IASPersonEntities())
                {
                    var sub = ctx.AG_SUBJECT_R.FirstOrDefault(x => x.LICENSE_TYPE_CODE == subject.LICENSE_TYPE_CODE && x.SUBJECT_CODE == subject.SUBJECT_CODE);
                    sub.STATUS = "D";
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        #endregion


        public DTO.ResponseService<List<DTO.AgentType>> GetAgentTypeList()
        {
            DTO.ResponseService<List<DTO.AgentType>> res = new DTO.ResponseService<List<DTO.AgentType>>();
            try
            {
                using (var ctx = new IASPersonEntities())
                {
                    var list = ctx.ExecuteStoreQuery<DTO.AgentType>("SELECT AGENT_TYPE,AGENT_TYPE_DESC FROM AG_AGENT_TYPE_R").ToList();
                    res.DataResponse = list;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }
        public DTO.ResponseService<List<DTO.LicenseTypet>> GetLicenseList(string agentType)
        {
            DTO.ResponseService<List<DTO.LicenseTypet>> res = new DTO.ResponseService<List<DTO.LicenseTypet>>();
            using (var ctx = new IASPersonEntities())
            {
                string sql = "SELECT * FROM AG_LICENSE_TYPE_R";
                if (agentType != "")
                {
                    sql += " WHERE AGENT_TYPE = '" + agentType + "'";
                }
                res.DataResponse = ctx.ExecuteStoreQuery<DTO.LicenseTypet>(sql).ToList();
            }
            return res;
        }
        public DTO.ResponseService<string> UpdateLicenseType(DTO.LicenseTypet licensetype)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            using (var ctx = new IASPersonEntities())
            {
                var license = ctx.AG_LICENSE_TYPE_R.FirstOrDefault(x => x.LICENSE_TYPE_CODE == licensetype.LICENSE_TYPE_CODE);
                license.LICENSE_TYPE_NAME = licensetype.LICENSE_TYPE_NAME;
                license.INSURANCE_TYPE = licensetype.INSURANCE_TYPE;
                license.AGENT_TYPE = licensetype.AGENT_TYPE;
                license.IAS_FLAG = "Y";
                ctx.SaveChanges();
            }
            return res;
        }
        public DTO.ResponseService<string> AddLicenseType(DTO.LicenseTypet licensetype)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            using (var ctx = new IASPersonEntities())
            {
                var l = from p in ctx.AG_LICENSE_TYPE_R.ToList() orderby p.LICENSE_TYPE_CODE descending select p;
                AG_LICENSE_TYPE_R license = new AG_LICENSE_TYPE_R();
                if (l != null)
                {
                    int No = l.First().LICENSE_TYPE_CODE.ToInt();
                    No += 1;
                    license.LICENSE_TYPE_CODE = No.ToString();
                }
                else
                {
                    license.LICENSE_TYPE_CODE = "1";
                }

                license.LICENSE_TYPE_NAME = licensetype.LICENSE_TYPE_NAME;
                license.INSURANCE_TYPE = licensetype.INSURANCE_TYPE;
                license.AGENT_TYPE = licensetype.AGENT_TYPE;
                license.IAS_FLAG = "Y";
                ctx.AG_LICENSE_TYPE_R.AddObject(license);
                ctx.SaveChanges();
            }
            return res;
        }
        public DTO.ResponseService<string> DeleteLicensetype(string licensecode)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            using (var ctx = new IASPersonEntities())
            {
                var license = ctx.AG_LICENSE_TYPE_R.FirstOrDefault(x => x.LICENSE_TYPE_CODE == licensecode);
                ctx.DeleteObject(license);
                ctx.SaveChanges();
            }
            return res;
        }





        #region ExamRoom

        public DTO.ResponseService<List<DTO.DataItem>> GetExamRoomByPlaceCode(string code)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var ls = base.ctx.AG_IAS_EXAM_PLACE_ROOM
                                .Where(x => x.EXAM_PLACE_CODE == code && x.ACTIVE == "Y")
                                .Select(s => new DTO.DataItem
                                {
                                    Id = s.EXAM_ROOM_CODE,
                                    Name = s.EXAM_ROOM_NAME
                                }).ToList();
                res.DataResponse = ls;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        public DTO.ResponseService<string> GetSeatAmountRoom(string roomcode, string ExamPlace)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                var obj = base.ctx.AG_IAS_EXAM_PLACE_ROOM.Where(s => s.EXAM_ROOM_CODE == roomcode && s.EXAM_PLACE_CODE == ExamPlace && s.ACTIVE == "Y").First();
                res.DataResponse = obj.SEAT_AMOUNT.ToString();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        public DTO.ResponseMessage<bool> InsertExamAndRoom(DTO.ExamSchedule ent, List<DTO.ExamSubLicense> entsub)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                //ตรวจสอบกำหนดการสอบซ้ำ
                var detail = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.LICENSE_TYPE_CODE == ent.LICENSE_TYPE_CODE
                                                              && w.TESTING_DATE == ent.TESTING_DATE
                                                              && w.TEST_TIME_CODE == ent.TEST_TIME_CODE
                                                              && w.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE).FirstOrDefault();
                //TODO: ตรวจสอบเรื่อง TestingNo ว่าจะ Running อย่างไร
                if (detail != null)
                {
                    if (!string.IsNullOrEmpty(detail.TESTING_NO))
                    {
                        res.ErrorMsg = Resources.errorExamService_001;
                        return res;
                    }
                }

                if (ent.TESTING_DATE == null)
                {
                    res.ErrorMsg = Resources.errorExamService_002;
                    return res;
                }

                if (!"E_N".Contains(ent.EXAM_STATUS))
                {
                    res.ErrorMsg = Resources.errorExamService_003;
                    return res;
                }


                ent.TESTING_NO = GenTestingNo(ent.TESTING_DATE);

                var exam = new AG_EXAM_LICENSE_R();
                ent.MappingToEntity(exam);
                base.ctx.AG_EXAM_LICENSE_R.AddObject(exam);

                //--> sub license
                if (entsub != null)
                {
                    foreach (var itm in entsub)
                    {
                        AG_IAS_EXAM_ROOM_LICENSE_R sub = new AG_IAS_EXAM_ROOM_LICENSE_R();
                        itm.MappingToEntity(sub);
                        sub.TESTING_NO = ent.TESTING_NO;
                        sub.USER_DATE = DateTime.Now;
                        base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.AddObject(sub);
                    }
                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        public DTO.ResponseService<List<DTO.ExamSubLicense>> GetExamRoomByLicenseNo(string No, string Place)
        {
            DTO.ResponseService<List<DTO.ExamSubLicense>> res = new DTO.ResponseService<List<DTO.ExamSubLicense>>();
            try
            {
                var ls = (from SL in base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R
                          join ER in base.ctx.AG_IAS_EXAM_PLACE_ROOM on SL.EXAM_ROOM_CODE equals ER.EXAM_ROOM_CODE
                          where SL.TESTING_NO == No && SL.ACTIVE == "Y" && ER.EXAM_PLACE_CODE == Place
                          select new DTO.ExamSubLicense
                          {
                              EXAM_ROOM_CODE = SL.EXAM_ROOM_CODE,
                              NUMBER_SEAT_ROOM = SL.NUMBER_SEAT_ROOM,
                              ROOM_NAME = ER.EXAM_ROOM_NAME,
                              SEAT_AMOUNT = ER.SEAT_AMOUNT,
                              TESTING_NO = SL.TESTING_NO,
                              USER_ID = SL.USER_ID,
                              USER_DATE = SL.USER_DATE
                          }).ToList();
                res.DataResponse = ls;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }
        public DTO.ResponseMessage<bool> UpdateExamAndRoom(DTO.ExamSchedule ent, List<DTO.ExamSubLicense> entsub)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var exam = base.ctx.AG_EXAM_LICENSE_R
                                   .SingleOrDefault(s => s.TESTING_NO == ent.TESTING_NO &&
                                                         s.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE);

                ent.MappingToEntity(exam);
                //--> sub license
                if (entsub != null)
                {
                    var objDB = base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.Where(x => x.TESTING_NO == ent.TESTING_NO).ToList();
                    if (objDB.Count > entsub.Count)
                    {
                        foreach (var idb in objDB)
                        {
                            var objSub = entsub.FirstOrDefault(x => x.TESTING_NO == idb.TESTING_NO && x.EXAM_ROOM_CODE == idb.EXAM_ROOM_CODE);
                            if (objSub == null)
                            {
                                base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.DeleteObject(idb);
                            }
                            else
                            {
                                idb.USER_DATE = DateTime.Now;
                                entsub.MappingToEntity(idb);
                            }
                        }
                    }
                    else
                    {
                        foreach (var itm in entsub)
                        {
                            var obj = base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.FirstOrDefault(x => x.TESTING_NO == itm.TESTING_NO && x.EXAM_ROOM_CODE == itm.EXAM_ROOM_CODE);
                            if (obj == null)
                            {
                                AG_IAS_EXAM_ROOM_LICENSE_R sub = new AG_IAS_EXAM_ROOM_LICENSE_R();
                                itm.MappingToEntity(sub);
                                sub.TESTING_NO = ent.TESTING_NO;
                                sub.USER_DATE = DateTime.Now;
                                base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.AddObject(sub);
                            }
                            else
                            {
                                obj.USER_DATE = DateTime.Now;
                                itm.MappingToEntity(obj);
                            }
                        }
                    }
                }

                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        #endregion

        #region ExamTime
        public DTO.ResponseService<DataSet> GetExamTime(string st_hr, string st_min, string en_hr, string en_min, int pageNo, int recordPerPage, Boolean Count)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                //string sql = "select test_time_code TimeCode,Test_time TXTTIME, start_time STime,End_Time Etime, " +
                //           " EXTRACT (HOUR FROM(TO_TIMESTAMP(end_time,'HH24 MI')-TO_TIMESTAMP(start_time,'HH24 MI')))*60 " +
                //           " + EXTRACT (MINUTE FROM(TO_TIMESTAMP(end_time,'HH24 MI')-TO_TIMESTAMP(start_time,'HH24 MI'))) DiffMin " +
                //           " from ag_exam_time_r " +
                //           " where Active='Y' and start_time is not null and end_time is not null " +
                //           " and(( to_timestamp(start_time,'HH24 MI')  between to_timestamp('" + st_hr + "." + st_min + "','HH24 MI')   and to_timestamp('" + en_hr + "." + en_min + "','HH24 MI') ) " +
                //           " or ( to_timestamp(end_time,'HH24 MI') between  to_timestamp('" + st_hr + "." + st_min + "','HH24 MI')   and to_timestamp('" + en_hr + "." + en_min + "','HH24 MI')))";
                string sql = string.Empty;
                if (!Count)
                {
                    sql = " select * from( " +
                        "select test_time_code TimeCode,Test_time TXTTIME, start_time STime,End_Time Etime, " +
                        " EXTRACT (HOUR FROM(TO_TIMESTAMP(end_time,'HH24 MI')-TO_TIMESTAMP(start_time,'HH24 MI')))*60 " +
                        " + EXTRACT (MINUTE FROM(TO_TIMESTAMP(end_time,'HH24 MI')-TO_TIMESTAMP(start_time,'HH24 MI'))) DiffMin,ROW_NUMBER() OVER (ORDER BY test_time_code) RUN_NO " +
                        " from ag_exam_time_r " +
                        " where Active='Y' and start_time is not null and end_time is not null " +
                        " and(( to_timestamp(start_time,'HH24 MI')  between to_timestamp('" + st_hr + "." + st_min + "','HH24 MI')   and to_timestamp('" + en_hr + "." + en_min + "','HH24 MI') ) " +
                        " or ( to_timestamp(end_time,'HH24 MI') between  to_timestamp('" + st_hr + "." + st_min + "','HH24 MI')   and to_timestamp('" + en_hr + "." + en_min + "','HH24 MI'))))a "
                        + " where a.RUN_NO between " + pageNo.StartRowNumber(recordPerPage).ToString() + " and " + pageNo.ToRowNumber(recordPerPage).ToString();
                }
                else
                {
                    sql = "select count (*) CCount from( " +
                       " select * from( " +
                       "select test_time_code TimeCode,Test_time TXTTIME, start_time STime,End_Time Etime, " +
                       " EXTRACT (HOUR FROM(TO_TIMESTAMP(end_time,'HH24 MI')-TO_TIMESTAMP(start_time,'HH24 MI')))*60 " +
                       " + EXTRACT (MINUTE FROM(TO_TIMESTAMP(end_time,'HH24 MI')-TO_TIMESTAMP(start_time,'HH24 MI'))) DiffMin,ROW_NUMBER() OVER (ORDER BY test_time_code) RUN_NO " +
                       " from ag_exam_time_r " +
                       " where Active='Y' and start_time is not null and end_time is not null " +
                       " and(( to_timestamp(start_time,'HH24 MI')  between to_timestamp('" + st_hr + "." + st_min + "','HH24 MI')   and to_timestamp('" + en_hr + "." + en_min + "','HH24 MI') ) " +
                       " or ( to_timestamp(end_time,'HH24 MI') between  to_timestamp('" + st_hr + "." + st_min + "','HH24 MI')   and to_timestamp('" + en_hr + "." + en_min + "','HH24 MI'))))a) ";
                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch
            {
            }
            return res;
        }

        public DTO.ResponseService<string> GetCountSearch(string st_hr, string st_min, string en_hr, string en_min)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                string sql = "select count(*)  CCount from ag_exam_time_r  where Active='Y' and start_time = '" + st_hr + "." + st_min + "' and end_time = '" + en_hr + "." + en_min + "'";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                res.DataResponse = dt.Rows[0]["CCount"].ToString();

            }
            catch
            {

            }
            return res;
        }

        public DTO.ResponseMessage<bool> Add_Time(string st_hr, string st_min, string en_hr, string en_min, string userID)
        {
            DTO.ResponseMessage<bool> addOK = new DTO.ResponseMessage<bool>();
            try
            {
                var obj = base.ctx.AG_EXAM_TIME_R.OrderByDescending(x => x.TEST_TIME_CODE).First();
                string max_num = Convert.ToString(obj.TEST_TIME_CODE.ToInt() + 1);

                AG_EXAM_TIME_R ex = new AG_EXAM_TIME_R();
                ex.TEST_TIME_CODE = max_num;
                ex.TEST_TIME = st_hr + "." + st_min + "-" + en_hr + "." + en_min;
                ex.START_TIME = st_hr + "." + st_min;
                ex.END_TIME = en_hr + "." + en_min;
                ex.USER_ID = userID;
                ex.USER_ID_UPDATE = userID;
                ex.USER_DATE = DateTime.Now;
                ex.USER_DATE_UPDATE = DateTime.Now;
                ex.ACTIVE = "Y";
                base.ctx.AG_EXAM_TIME_R.AddObject(ex);
                base.ctx.SaveChanges();
                addOK.ResultMessage = true;
            }
            catch
            {
                addOK.ResultMessage = false;
            }
            return addOK;
        }

        public DTO.ResponseMessage<bool> Del_Time(string Key, string UserID)
        {
            DTO.ResponseMessage<bool> Del = new DTO.ResponseMessage<bool>();
            try
            {
                var obj = base.ctx.AG_EXAM_TIME_R.FirstOrDefault(x => x.TEST_TIME_CODE == Key);
                if (obj != null)
                {
                    obj.ACTIVE = "N";
                    obj.USER_DATE_UPDATE = DateTime.Now;
                    obj.USER_ID_UPDATE = UserID;
                    base.ctx.AG_EXAM_TIME_R.MappingToEntity(obj);
                    base.ctx.SaveChanges();
                    Del.ResultMessage = true;
                }
            }
            catch
            {

                Del.ResultMessage = false;
            }
            return Del;
        }

        public DTO.ResponseService<DataSet> getExamTimeShow(string ID)
        {
            DTO.ResponseService<DataSet> TimeShow = new DTO.ResponseService<DataSet>();
            try
            {
                if (ID != "") ID = " or test_time_code like '" + ID + "' ";

                string sql = "select test_time_code TimeCode ,   Test_time  TXTTIME from ag_exam_time_r " +
                                " where (active='Y' " + ID + " )  and start_time is not null and end_time is not null " +
                                " order by start_time,end_time asc ";
                OracleDB ora = new OracleDB();
                TimeShow.DataResponse = ora.GetDataSet(sql);
            }
            catch
            {
            }
            return TimeShow;
        }

        public DTO.ResponseService<DataSet> GetAssoLicense(string AssoCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                //string sql = "select ls.License_type_name ,ls.license_type_code " +
                //             "   from ag_ias_license_type_r ls " +
                //             "   left join ag_ias_association_license asls on " +
                //             "   ls.license_type_code = asls.license_type_code " +
                //             "   left join AG_IAS_EXAM_CONDITION_GROUP ECD on "+
                //             "   ECD.LICENSE_TYPE_CODE = LS.LICENSE_TYPE_CODE "+
                //             "   where  " +
                //             " asls.active = 'Y'  and  ECD.STATUS = 'A' " +
                //             "   and asls.association_code = '"+AssoCode+"'";
                string sql = "select ls.License_type_name ,ls.license_type_code    from ag_ias_license_type_r ls  "
                          + "left join ag_ias_association_license asls on    ls.license_type_code = asls.license_type_code  "
                          + "where   asls.active = 'Y' "
                          + "and asls.association_code = '" + AssoCode + "'";

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch
            {

            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetExamRoomByPlaceCodeAndTimeCode(string code, string Timetxt,
                                                    string dDate, List<DTO.ExamSubLicense> oldCode, Boolean Del, string testingNoo)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                OracleDB ora = new OracleDB();
                string temp = "";
                string st_time = string.Format("{0:00.00}", Timetxt.Substring(0, 5));
                string en_time = string.Format("{0:00.00}", Timetxt.Substring(6, 5));
                string tempSQL = "";
                List<AG_IAS_EXAM_PLACE_ROOM> AllRoom = ctx.AG_IAS_EXAM_PLACE_ROOM.Where(x => x.EXAM_PLACE_CODE == code && x.ACTIVE == "Y").ToList();//เอาห้องทั้งหมด

                if (AllRoom.Count() != 0)
                {

                    List<DTO.ExamRoomDDLTemp> lsEXRoomDDL = new List<DTO.ExamRoomDDLTemp>();//ห้องทั้งหมดใน DB และ ที่ใช้ในรอบสอบนี้(ยังไม่บันทึก)
                    List<DTO.ExamRoomDDLTemp> EXRoomDDL = new List<DTO.ExamRoomDDLTemp>(); //ห้องจากข้อมูลรอบสอบนี้ที่มีการแก้ไขลบรอบสอบ


                    foreach (string A in AllRoom.Select(m => m.EXAM_ROOM_CODE))
                    {
                        lsEXRoomDDL.Add(new DTO.ExamRoomDDLTemp
                        {
                            RoomCode = A,
                        });

                    }
                    EXRoomDDL = lsEXRoomDDL;
                    //string TN = "";

                    string sql1 = " select c.Exam_room_code Id ,  a.testing_no TESTNO " +
                                  "   from ag_exam_license_r a   " +
                                  "   left join ag_ias_exam_room_license_r  c on c.active = 'Y'   " +
                                  "   left join ag_exam_place_r b on  c.testing_no = a.testing_no   " +
                                  "   left join ag_ias_exam_place_room d on d.active = 'Y' and d.exam_room_code = c.exam_room_code  and d.exam_place_code = b.exam_place_code  " +
                                  "   left join ag_exam_time_r et on et.start_time is not null and et.end_time is not null and  et.active ='Y'  " +
                                  "   where  b.exam_place_code ='" + code + "' and  b.exam_place_code = a.exam_place_code and   " +
                                  "   a.testing_date =  to_date(to_char(to_date('" + dDate + "','DD/MM/RRRR', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/RRRR', ' NLS_DATE_LANGUAGE=AMERICAN'),'DD/MM/RRRR')   " +
                                  "   and  et.test_time_code = a.test_time_code  " +
                                  "   and   " +
                                  "   ( " +
                                  "                   to_timestamp('" + st_time + "','HH24.MI')  between to_timestamp(et.start_time,'HH24 MI') and to_timestamp(et.end_time,'HH24 MI')  " +
                                  "       or    to_timestamp('" + en_time + "','HH24.MI')  between to_timestamp(et.start_time,'HH24 MI') and to_timestamp(et.end_time,'HH24 MI')  " +
                                  "       or   to_timestamp(et.start_time,'HH24 MI')  between  to_timestamp('" + st_time + "','HH24.MI')   and to_timestamp('" + en_time + "','HH24.MI')   " +
                                  "       or    to_timestamp(et.end_time,'HH24 MI')  between  to_timestamp('" + st_time + "','HH24.MI')   and to_timestamp('" + en_time + "','HH24.MI')  " +
                                  "   )  " +
                                  "   and d.EXAM_PLACE_CODE = b.exam_place_code "; //หาห้องสอบที่ใช้ในเวลาดังกล่าว 


                    //DTO.ResponseService<DataSet> res1 = new DTO.ResponseService<DataSet>();
                    DataSet resUse = ora.GetDataSet(sql1);
                    DataTable dt = resUse.Tables[0];



                    if (dt.Rows.Count > 0) // ห้องสอบที่มีในวัน เวลา และ สถานที่เดียวกัน ;
                    {
                        foreach (DataRow C in dt.Rows)
                        {

                            var ls = lsEXRoomDDL.Where(x => x.RoomCode == C["Id"].ToString()).FirstOrDefault();
                            ls.RoomName = C["TESTNO"].ToString();
                            EXRoomDDL.Add(new DTO.ExamRoomDDLTemp
                            {
                                RoomName = ls.RoomName,
                                RoomCode = ls.RoomCode,
                            });

                            if (testingNoo == "")
                                EXRoomDDL.RemoveAll(x => x.RoomCode == C["Id"].ToString()); //ห้องทั้งหมด ลบ ห้องที่มีในฐานข้อมูล (กรณีเพิ่มใหม่)
                            else //(กรณีแก้ไขข้อมูล)
                            {
                                if (testingNoo != C["TESTNO"].ToString())
                                    EXRoomDDL.RemoveAll(x => x.RoomCode == C["Id"].ToString() && x.RoomName != testingNoo);
                            }
                        }
                    }

                    if (oldCode.Count > 0)//มี lsRoom ส่งมาด้วย
                    {
                        foreach (string C in oldCode.Select(s => s.EXAM_ROOM_CODE))
                        {
                            EXRoomDDL.RemoveAll(x => x.RoomCode == C);//ห้องที่มี ลบ list ห้องที่ส่งมา
                        }
                    }


                    #region Comment
                    //if (oldCode.Count > 0) // กรณีแก้ไขข้อมูล(ใช้อันบน)
                    //{

                    //    foreach (string C in oldCode.Select(x => x.EXAM_ROOM_CODE))
                    //    {
                    //        if (lsEXRoomDDL.Where(v => v.RoomCode == C).Count() == 0)
                    //        {
                    //            lsEXRoomDDL.Add(new DTO.ExamRoomDDLTemp
                    //            {
                    //                RoomCode = C,

                    //            });

                    //        }

                    //        if(EXRoomDDL.Where(m =>m.RoomCode == C).Count() ==0)
                    //         EXRoomDDL.Add(new DTO.ExamRoomDDLTemp
                    //         {
                    //             RoomCode = C,                            
                    //        });
                    //    }

                    //}

                    //if (dt.Rows.Count > 0) // ห้องสอบที่มีในวัน เวลา และ สถานที่เดียวกัน ;
                    //{




                    //    foreach (DataRow C in dt.Rows)
                    //    {


                    //        if ((testingNoo == C["TESTNO"].ToString()) && (Del))
                    //        { 
                    //        }
                    //        else if (testingNoo != TN)
                    //        {
                    //            TN = C["TESTNO"].ToString();
                    //            if (tempSQL != "")
                    //            {
                    //                tempSQL = tempSQL + " , " + C["Id"].ToString();
                    //            }
                    //            else
                    //            {
                    //                tempSQL = C["Id"].ToString();
                    //            }
                    //        }
                    //    }


                    //    if (oldCode.Count > 0) // กรณีแก้ไขข้อมูล(?)(ของเดิมลบปีกกาออก) (ใช้อันบน)
                    //    {
                    //        TN = "";

                    //        for (int i = 0; i < dt.Rows.Count; i++)
                    //        {
                    //            if (TN != dt.Rows[i]["Id"].ToString())
                    //            {

                    //                foreach (string C in oldCode.Select(x => x.EXAM_ROOM_CODE))
                    //                {
                    //                    TN = dt.Rows[i]["Id"].ToString();

                    //                    if (dt.Rows[i]["Id"].ToString() == C)
                    //                    {
                    //                        if (tempSQL != "")
                    //                        {
                    //                            tempSQL = tempSQL + " , " + dt.Rows[i]["Id"].ToString();
                    //                        }
                    //                        else
                    //                        {
                    //                            tempSQL = dt.Rows[i]["Id"].ToString();
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {

                    //        if (oldCode.Count() > 0)
                    //        {
                    //            foreach (string C in oldCode.Select(x => x.EXAM_ROOM_CODE))
                    //            {
                    //                if (tempSQL == "")
                    //                    tempSQL = C;
                    //                else
                    //                    tempSQL = tempSQL + " , " + C;

                    //            }

                    //            if (temp != "")
                    //                temp = " or (" + temp + " )";

                    //        }
                    //    }

                    //}

                    #endregion Comment

                    if (EXRoomDDL.Count() > 0)//มีห้องเหลือ
                    {
                        foreach (string R in EXRoomDDL.Select(x => x.RoomCode))
                        {
                            if (tempSQL == "")
                                tempSQL = R;
                            else
                                tempSQL = tempSQL + " , " + R;
                        }
                        tempSQL = " and d.exam_room_code  in (" + tempSQL + ")";
                    }
                    else//ใช้หมดทุกห้อง
                    {
                        foreach (string R in AllRoom.Select(x => x.EXAM_ROOM_CODE))
                        {
                            if (tempSQL == "")
                                tempSQL = R;
                            else
                                tempSQL = tempSQL + " , " + R;
                        }
                        tempSQL = " and d.exam_room_code not in (" + tempSQL + ")";
                    }




                    string new_SQL = " select DISTINCT d.Exam_room_code Id, d.exam_room_name  Name    " +
                                 "    from ag_ias_exam_place_room d   " +
                                 "    left join ag_exam_license_r a on  d.EXAM_PLACE_CODE = a.EXAM_PLACE_CODE    " +
                                 "    where  d.exam_place_code ='" + code + "'  " +
                                 "    and (d.active = 'Y'  " + temp + ") " + tempSQL + " order by d.exam_room_name asc";


                    res.DataResponse = ora.GetDataSet(new_SQL);
                }


            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetPlaceDetailByPlaceCode_noCheckActive(string PlaceCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                var sql = "select * from AG_EXAM_PLACE_R where EXAM_PLACE_CODE = '" + PlaceCode + "'";
                OracleDB ora = new OracleDB();
                var ex = ora.GetDataSet(sql);
                res.DataResponse = ex;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> DelExamRoom(string Room)
        {
            DTO.ResponseMessage<bool> Del = new DTO.ResponseMessage<bool>();
            try
            {
                string sql = "UPDATE AG_IAS_EXAM_PLACE_ROOM SET ACTIVE = 'N' WHERE EXAM_ROOM_CODE ='" + Room + "'";
                OracleDB ora = new OracleDB();
                string DelOK = ora.ExecuteCommand(sql);
                if (DelOK == "")
                {
                    Del.ResultMessage = true;
                }
                else
                {
                    Del.ResultMessage = false;
                }
            }
            catch
            {
                Del.ResultMessage = false;
            }
            return Del;
        }

        public DTO.ResponseService<string> SumSeat(string placeCode)
        {
            DTO.ResponseService<string> sumS = new DTO.ResponseService<string>();
            try
            {
                string Sql = "select sum(seat_amount) SS from ag_ias_exam_place_room where exam_place_code = '" + placeCode + "' and active='Y'";
                OracleDB ora = new OracleDB();
                var ds = ora.GetDataSet(Sql);
                sumS.DataResponse = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                sumS.ErrorMsg = ex.Message;
            }
            return sumS;
        }

        #endregion ExamTime

        #region searchsubject

        public DTO.ResponseService<List<DTO.Subjectr>> GetSubjectGroup(string p)
        {
            var res = new DTO.ResponseService<List<DTO.Subjectr>>();
            try
            {
                //var list = from d in ctx.AG_SUBJECT_R.Where(x => x.LICENSE_TYPE_CODE == p && x.STATUS == "A")
                //           select new DTO.Subjectr
                //           {
                //               LICENSE_TYPE_CODE = d.LICENSE_TYPE_CODE,
                //               SUBJECT_CODE = d.SUBJECT_CODE,
                //               SUBJECT_NAME = d.SUBJECT_NAME,
                //               MAX_SCORE = d.MAX_SCORE,
                //               GROUP_ID = d.GROUP_ID
                //           };
                //res.DataResponse = list.ToList();

                string sql = "SELECT  " +
                    "	SR.LICENSE_TYPE_CODE,  " +
                    "	SR.SUBJECT_CODE,  " +
                    "	SR.SUBJECT_NAME,  " +
                    "	SR.MAX_SCORE,  " +
                    "	GROUP_ID  " +
                    "FROM  " +
                    "	AG_SUBJECT_R SR  " +
                    "WHERE  " +
                    "	SR.LICENSE_TYPE_CODE = '" + p + "'  " +
                    "AND SR.STATUS = 'A'  " +
                    "ORDER BY  " +
                    "	SR. GROUP_ID,  " +
                    "	SR.SUBJECT_NAME ASC";
                res.DataResponse = ctx.ExecuteStoreQuery<DTO.Subjectr>(sql).ToList();

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("ข้อมูลวิชา", ex);
            }

            return res;
        }

        public DTO.ResponseService<string> AddExamGroup(DTO.ConditionGroup conditiongroup)
        {
            var res = new DTO.ResponseService<string>();
            try
            {

                string year = DateTime.Now.AddYears(543).Year.ToString().Substring(2, 2);
                var listgroup = ctx.AG_IAS_EXAM_CONDITION_GROUP.ToList();
                var lastnumber = listgroup.Where(x => x.COURSE_NUMBER.ToString().Substring(0, 2) == year).OrderByDescending(x => x.COURSE_NUMBER);
                decimal course_number = 0;
                bool check = false;
                if (lastnumber == null || lastnumber.Count() == 0)
                {
                    course_number = (year + "001").ToDecimal();
                    check = true;
                }
                else
                {
                    var examlast = lastnumber.First();
                    course_number = (decimal)(examlast.COURSE_NUMBER + 1);
                    var listcon_group = ctx.AG_IAS_EXAM_CONDITION_GROUP.Where(x => x.LICENSE_TYPE_CODE == conditiongroup.LICENSE_TYPE_CODE).OrderByDescending(x => x.COURSE_NUMBER);

                    if (listcon_group == null || listcon_group.Count() == 0)
                    {
                        check = true;
                    }
                    else
                    {
                        var con_group = listcon_group.First();
                        if (con_group.END_DATE == null)
                        {
                            if (conditiongroup.START_DATE >= con_group.START_DATE)
                            {
                                check = true;
                            }
                            else
                            {
                                check = false;
                                res.ErrorMsg = "1";
                            }
                        }
                        else if (conditiongroup.START_DATE >= con_group.START_DATE && conditiongroup.START_DATE <= con_group.END_DATE)
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                            res.ErrorMsg = "2";
                        }
                    }
                }

                if (check)
                {
                    AG_IAS_EXAM_CONDITION_GROUP group = new AG_IAS_EXAM_CONDITION_GROUP();
                    group.COURSE_NUMBER = course_number;
                    group.STATUS = conditiongroup.STATUS;
                    group.LICENSE_TYPE_CODE = conditiongroup.LICENSE_TYPE_CODE;
                    group.START_DATE = conditiongroup.START_DATE;
                    group.END_DATE = conditiongroup.END_DATE;
                    group.USER_DATE = conditiongroup.USER_DATE;
                    group.NOTE = conditiongroup.NOTE;
                    group.USER_ID = conditiongroup.USER_ID;
                    ctx.AG_IAS_EXAM_CONDITION_GROUP.AddObject(group);

                    if (conditiongroup.STATUS == "A")
                    {
                        foreach (var item in ctx.AG_IAS_EXAM_CONDITION_GROUP.Where(x => x.LICENSE_TYPE_CODE == conditiongroup.LICENSE_TYPE_CODE && x.STATUS == "A"))
                        {
                            item.STATUS = "E";
                        }
                    }


                    foreach (var item in conditiongroup.Subject)
                    {
                        AG_IAS_EXAM_CONDITION_GROUP_D groupd = new AG_IAS_EXAM_CONDITION_GROUP_D();
                        groupd.COURSE_NUMBER = course_number;
                        groupd.LICENSE_TYPE_CODE = item.LICENSE_TYPE_CODE;
                        groupd.SUBJECT_CODE = item.SUBJECT_CODE;
                        groupd.MAX_SCORE = item.MAX_SCORE;
                        groupd.GROUP_ID = item.GROUP_ID;
                        groupd.USER_ID = conditiongroup.USER_ID;
                        groupd.USER_DATE = conditiongroup.USER_DATE;
                        ctx.AG_IAS_EXAM_CONDITION_GROUP_D.AddObject(groupd);
                    }

                    foreach (var item in ctx.AG_IAS_SUBJECT_GROUP.Where(x => x.STATUS == "A"))
                    {
                        ctx.AG_IAS_EXAM_SUBJECT_GROUP.AddObject(new AG_IAS_EXAM_SUBJECT_GROUP
                        {
                            COURSE_NUMBER = course_number,
                            EXAMPASS = item.EXAM_PASS,
                            GROUP_NAME = item.GROUP_NAME,
                            ID = item.ID,
                            USER_ID = conditiongroup.USER_ID,
                            USER_DATE = conditiongroup.USER_DATE
                        });
                    }

                    ctx.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("เพิ่มหลักสูตร", ex);
            }

            return res;
        }

        public DTO.ResponseService<string> ActiveConditionGroup(string p, string license)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {

                decimal number = p.ToDecimal();
                var condition = ctx.AG_IAS_EXAM_CONDITION_GROUP.FirstOrDefault(x => x.COURSE_NUMBER == number);
                condition.STATUS = "A";

                foreach (var item in ctx.AG_IAS_EXAM_CONDITION_GROUP.Where(x => x.LICENSE_TYPE_CODE == license && x.STATUS == "A"))
                {
                    item.STATUS = "E";
                }
                ctx.SaveChanges(System.Data.Objects.SaveOptions.DetectChangesBeforeSave);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().LogError("ActiveConditionGroup", ex);
            }
            return res;
        }

        public DTO.ResponePage<List<DTO.SubjectGroup>> GetSubjectGroupSearch(string p, int Page, int record)
        {
            var res = new DTO.ResponePage<List<DTO.SubjectGroup>>();
            try
            {
                string s = p == "" ? "" : "WHERE ACG.LICENSE_TYPE_CODE = '" + p + "' ";
                //string sql = "SELECT  " +
                //            "	ACG.*, ALT.LICENSE_TYPE_NAME  " +
                //            "FROM  " +
                //            "	AG_IAS_EXAM_CONDITION_GROUP ACG  " +
                //            "INNER JOIN AG_LICENSE_TYPE_R ALT ON ACG.LICENSE_TYPE_CODE = ALT.LICENSE_TYPE_CODE  " +
                //            s +
                //            "ORDER BY  " +
                //            "	ACG. ID DESC";

                int end = Page * record;
                int start = end - record;


                string sql = "SELECT * FROM(  " +
                "SELECT  " +
                "	ROWNUM as Num,  " +
                "	ACG.*, ALT.LICENSE_TYPE_NAME  " +
                "FROM  " +
                "	AG_IAS_EXAM_CONDITION_GROUP ACG  " +
                "INNER JOIN AG_LICENSE_TYPE_R ALT ON ACG.LICENSE_TYPE_CODE = ALT.LICENSE_TYPE_CODE  " +
                s +
                "ORDER BY  " +
                "	ACG. ID DESC   " +
                ") AA WHERE AA.NUM >= '" + start + "' and AA.NUM <= '" + end + "' ORDER BY AA.NUM ASC";

                List<DTO.SubjectGroup> list = ctx.ExecuteStoreQuery<DTO.SubjectGroup>(sql).ToList();
                res.DataResponse = list;

                OracleDB db = new OracleDB();
                sql = "SELECT  " +
                "	COUNT (*) AS COUNT  " +
                "FROM  " +
                "	AG_IAS_EXAM_CONDITION_GROUP ACG  " +
                "INNER JOIN AG_LICENSE_TYPE_R ALT ON ACG.LICENSE_TYPE_CODE = ALT.LICENSE_TYPE_CODE " + s;
                res.CountRecord = db.GetDataTable(sql).Rows[0]["COUNT"].ToString().ToInt();
                double findpage = (double)res.CountRecord / (double)record;
                res.CountPage = (int)Math.Ceiling(findpage);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("GetSubjectGroupSearch", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.SubjectGroupD>> GetSubjectInGroup(string p)
        {
            var res = new DTO.ResponseService<List<DTO.SubjectGroupD>>();
            try
            {
                string sql = "SELECT  " +
                "	AD.*, AR.SUBJECT_NAME,AR.MAX_SCORE  " +
                "FROM  " +
                "	AG_IAS_EXAM_CONDITION_GROUP_D AD  " +
                "INNER JOIN AG_SUBJECT_R AR ON AD.LICENSE_TYPE_CODE = AR.LICENSE_TYPE_CODE  " +
                "AND AD.SUBJECT_CODE = AR.SUBJECT_CODE  " +
                "WHERE  " +
                "	AD.COURSE_NUMBER = '" + p + "'  " +
                "ORDER BY  " +
                "	AR.SUBJECT_NAME ASC";

                res.DataResponse = ctx.ExecuteStoreQuery<DTO.SubjectGroupD>(sql).ToList();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("GetSubjectInGroup", ex);
            }
            return res;
        }

        #endregion

        #region subjectgroup

        public DTO.ResponseService<List<DTO.GroupSubject>> GetSubjectGroupList(string p)
        {
            var res = new DTO.ResponseService<List<DTO.GroupSubject>>();
            try
            {
                var list = from a in ctx.AG_IAS_SUBJECT_GROUP.ToList().Where(x => x.STATUS == "A")
                           select new DTO.GroupSubject
                           {
                               EXAM_PASS = a.EXAM_PASS,
                               GROUP_NAME = a.GROUP_NAME,
                               ID = a.ID,
                               STATUS = a.STATUS
                           };
                res.DataResponse = list.ToList();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("ดึงข้อมูลรายการวิชา", ex);
            }

            return res;
        }

        public DTO.ResponseService<string> AddSubjectGroup(DTO.GroupSubject examgroup)
        {
            var res = new DTO.ResponseService<string>();
            try
            {
                var list = ctx.AG_IAS_SUBJECT_GROUP.FirstOrDefault(x => x.GROUP_NAME == examgroup.GROUP_NAME && x.STATUS == "A");
                if (list == null)
                {
                    var exam = ctx.AG_IAS_SUBJECT_GROUP.OrderByDescending(x => x.ID).ToList();
                    int ID = 1;
                    if (exam != null && exam.Count() != 0)
                    {
                        ID = (int)exam.First().ID;
                        ID++;
                    }
                    AG_IAS_SUBJECT_GROUP group = new AG_IAS_SUBJECT_GROUP();
                    group.ID = ID;
                    group.GROUP_NAME = examgroup.GROUP_NAME;
                    group.EXAM_PASS = examgroup.EXAM_PASS;
                    group.STATUS = "A";
                    ctx.AG_IAS_SUBJECT_GROUP.AddObject(group);
                    ctx.SaveChanges();
                }
                else
                {
                    res.ErrorMsg = "ชื่อกลุ่มวิชาที่ป้อนมีแล้ว";
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("เพิ่มข้อมูลวิชา", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> DeleteSubjectGroup(string p)
        {
            var res = new DTO.ResponseService<string>();
            try
            {
                decimal id = p.ToDecimal();
                var exam = ctx.AG_IAS_SUBJECT_GROUP.FirstOrDefault(x => x.ID == id);
                exam.STATUS = "D";
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("ลบข้อมูลวิชา", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> UpdateSubjectGroup(DTO.GroupSubject examgroup)
        {
            var res = new DTO.ResponseService<string>();
            try
            {

                var exam = ctx.AG_IAS_SUBJECT_GROUP.FirstOrDefault(x => x.ID == examgroup.ID);
                exam.STATUS = examgroup.STATUS;
                exam.EXAM_PASS = examgroup.EXAM_PASS;
                exam.GROUP_NAME = examgroup.GROUP_NAME;
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().LogError("แก้ไขข้อมูลวิชา", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> GenLicenseNo(string PlaceCode, string User_Id)
        {
            DTO.ResponseService<string> Lic = new DTO.ResponseService<string>();
            try
            {
                Boolean Pass = false;
                while (!Pass)
                {
                    //string sql = " select Replace(substr( EXTRACT (YEAR from to_date(to_char(sysdate,'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/YYYY')),3,2) " +
                    //                " || '' || to_char(case when max(testing_no) is null then 0001 else max(substr(testing_no,3,4))+1 end,'0000'),' ' ) YYear " +
                    //                " from ag_exam_license_r   " +
                    //                " where substr(testing_no,1,2) = substr( EXTRACT (YEAR from to_date(to_char(sysdate,'DD/MM/YYYY', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/YYYY')),3,2)";
                    //OracleDB ora = new OracleDB();
                    //DataSet dt = ora.GetDataSet(sql);
                    //Lic.DataResponse = dt.Tables[0].Rows[0][0].ToString();
                    Lic.DataResponse = GenTestingNo(DateTime.Now);
                    Pass = SaveNewTestno(Lic.DataResponse.ToString(), PlaceCode, User_Id);
                }
            }
            catch
            {
                Lic.DataResponse = null;
            }
            return Lic;
        }

        private bool SaveNewTestno(string TestingNo, string PlaceCode, string User_Id)
        {
            bool Pass = false;
            try
            {
                AG_EXAM_LICENSE_R SaveNew = base.ctx.AG_EXAM_LICENSE_R.Where(x => x.TESTING_NO == TestingNo && x.EXAM_PLACE_CODE == PlaceCode).FirstOrDefault();
                if (SaveNew == null)
                {
                    AG_EXAM_LICENSE_R exam = new AG_EXAM_LICENSE_R();
                    exam.TESTING_NO = TestingNo;
                    exam.EXAM_PLACE_CODE = PlaceCode;
                    exam.USER_ID = User_Id;
                    exam.USER_DATE = DateTime.Now;
                    exam.EXAM_FEE = ctx.AG_PETITION_TYPE_R.FirstOrDefault(s => s.PETITION_TYPE_CODE == "01").FEE.ToInt();//สมัครสอบต้องเป็นค่าเดิม ไม่งั้นกระทบกับคนที่สมัครไปแล้วได้
                    ctx.AG_EXAM_LICENSE_R.AddObject(exam);
                    ctx.SaveChanges();
                    Pass = true;
                }
                else
                {
                    Pass = false;
                }
            }
            catch
            {
            }
            return Pass;
        }


        public DTO.ResponseMessage<bool> SaveSetApplicantRoom(DTO.SaveSetApplicantRoom lr, string Event)
        {

            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                using (TransactionScope tc = new TransactionScope())
                {
                    var exam = ctx.AG_EXAM_LICENSE_R.FirstOrDefault(x => x.TESTING_NO == lr.TESTING_NO && x.EXAM_PLACE_CODE == lr.EXAM_PLACE_CODE);
                    if (exam != null)//NOTE : มีการบันทึกข้อมูลแล้วทุกครั้ง เพราะทันทีที่GEN TESTING_NO จะบันทึกลงฐานข้อมูลเลย ดังนั้นไม่ว่าสร้างรอบใหม่หรือแก้ไขจะมีข้อมูลแล้วเสมอ
                    {
                        if (Event == DTO.ManageExamRoom_MODE.MOVE.ToString())//กรณีเลื่อนรอบหรือแก้ไขห้อง
                        {
                            string TxtTime = getExamTimeShow(exam.TEST_TIME_CODE).DataResponse.Tables[0].Rows[0]["TXTTIME"].ToString();
                            string TxtTimeRef = getExamTimeShow(lr.TEST_TIME_CODE).DataResponse.Tables[0].Rows[0]["TXTTIME"].ToString();

                            if (exam.EXAM_APPLY  == 0)
                            {
                                exam.TESTING_DATE = lr.TESTING_DATE;
                                exam.TEST_TIME_CODE = lr.TEST_TIME_CODE;
                            }

                            exam.EXAM_STATE = "M";
                            exam.REMARK = lr.EXAM_REMARK;
                        }
                        else//กรณีสร้างใหม่
                        {
                            exam.EXAM_STATE = "A";
                            exam.REMARK = lr.EXAM_REMARK;
                            exam.TEST_TIME_CODE = lr.TEST_TIME_CODE;
                            exam.TESTING_DATE = lr.TESTING_DATE;
                        }

                        exam.LICENSE_TYPE_CODE = (lr.LICENSE_TYPE_CODE == null) ? exam.LICENSE_TYPE_CODE : lr.LICENSE_TYPE_CODE;
                        exam.EXAM_STATUS = (lr.EXAM_STATUS == null) ? exam.EXAM_STATUS : lr.EXAM_STATUS;
                        exam.EXAM_APPLY = (exam.EXAM_APPLY == null) ? Convert.ToInt16(lr.EXAM_APPLY) : Convert.ToInt16(exam.EXAM_APPLY);
                        exam.EXAM_ADMISSION = Convert.ToInt16(lr.EXAM_ADMISSION);
                        exam.COURSE_NUMBER = (exam.COURSE_NUMBER == null) ? Convert.ToInt32(lr.COURSE_NUMBER) : exam.COURSE_NUMBER;//ถ้าว่างแสดงว่าเป็นรอบสอบใหม่ ถ้ามีแล้วแสดงว่าเป็นกรณีแก้ไขหรือเลื่อนรอบ
                        exam.EXAM_OWNER = (lr.EXAM_OWNER == null) ? exam.EXAM_OWNER : lr.EXAM_OWNER;
                        exam.SPECIAL = (lr.SPECIAL == null) ? exam.SPECIAL : lr.SPECIAL;
                        exam.IMPORT_TYPE = (lr.IMPORT_TYPE == null) ? exam.IMPORT_TYPE : lr.IMPORT_TYPE;

                        if (lr.EXAM_ROOM_CODE.Count() > 0)
                        {
                            var examDel = ctx.AG_IAS_EXAM_ROOM_LICENSE_R.Where(y => y.TESTING_NO == lr.TESTING_NO && y.ACTIVE == "Y");
                            if (examDel != null)
                            {

                                foreach (var idb in examDel)
                                {
                                    idb.ACTIVE = "N";
                                    idb.USER_DATE_UPDATE = DateTime.Now;
                                    idb.USER_ID_UPDATE = lr.USER_ID;

                                }
                                #region กรณีแก้ไขห้องสอบ

                                if (lr != null)
                                {
                                    foreach (DTO.ExamSubLicense i in lr.EXAM_ROOM_CODE)
                                    {
                                        var examRoom = ctx.AG_IAS_EXAM_ROOM_LICENSE_R.FirstOrDefault
                                                    (y => y.TESTING_NO == lr.TESTING_NO && y.EXAM_ROOM_CODE == i.EXAM_ROOM_CODE);
                                        if (examRoom != null)//ห้องนี้มีตั้งแต่แรกแล้ว
                                        {

                                            examRoom.NUMBER_SEAT_ROOM = i.NUMBER_SEAT_ROOM;
                                            examRoom.USER_DATE_UPDATE = DateTime.Now;
                                            examRoom.USER_ID_UPDATE = i.USER_ID;
                                            examRoom.ACTIVE = "Y";

                                        }
                                        else//พึ่งเพิ่ม
                                        {
                                            AG_IAS_EXAM_ROOM_LICENSE_R RLR = new AG_IAS_EXAM_ROOM_LICENSE_R();
                                            RLR.TESTING_NO = exam.TESTING_NO;
                                            RLR.EXAM_ROOM_CODE = i.EXAM_ROOM_CODE;
                                            RLR.NUMBER_SEAT_ROOM = i.NUMBER_SEAT_ROOM;
                                            RLR.USER_DATE = DateTime.Now;
                                            RLR.USER_ID = i.USER_ID;
                                            RLR.USER_DATE_UPDATE = DateTime.Now;
                                            RLR.USER_ID_UPDATE = i.USER_ID;
                                            RLR.ACTIVE = "Y";
                                            ctx.AG_IAS_EXAM_ROOM_LICENSE_R.AddObject(RLR);
                                        }

                                    }
                                }

                                #endregion กรณีแก้ไขห้องสอบ
                            }

                            #region กรณีสร้างรอบใหม่ที่ต้องบันทีกห้องสอบครั้งแรก
                            else
                            {
                                foreach (DTO.ExamSubLicense i in lr.EXAM_ROOM_CODE)
                                {
                                    AG_IAS_EXAM_ROOM_LICENSE_R RLR = new AG_IAS_EXAM_ROOM_LICENSE_R();
                                    RLR.TESTING_NO = exam.TESTING_NO;
                                    RLR.EXAM_ROOM_CODE = i.EXAM_ROOM_CODE;
                                    RLR.NUMBER_SEAT_ROOM = i.NUMBER_SEAT_ROOM;
                                    RLR.USER_DATE = DateTime.Now;
                                    RLR.USER_ID = i.USER_ID;
                                    RLR.USER_ID_UPDATE = i.USER_ID;
                                    RLR.USER_DATE_UPDATE = DateTime.Now;
                                    RLR.ACTIVE = "Y";
                                    ctx.AG_IAS_EXAM_ROOM_LICENSE_R.AddObject(RLR);

                                }
                            }
                        }

                            #endregion กรณีสร้างรอบใหม่ที่ต้องบันทีกห้องสอบครั้งแรก

                        ctx.SaveChanges();
                        tc.Complete();
                        res.ResultMessage = true;
                    }
                    #region
                   
                    #endregion

                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public DTO.ResponseMessage<bool> SaveDeleteApplicantRoom(string testing_no, string ExampleaceCode, string USERID)
        {

            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {

                var examCheck = ctx.AG_EXAM_LICENSE_R.FirstOrDefault(x => x.TESTING_NO == testing_no && x.EXAM_PLACE_CODE == ExampleaceCode && x.EXAM_APPLY == 0);
                if (examCheck != null)
                {
                    examCheck.EXAM_STATE = "D";
                    examCheck.USER_ID = USERID;
                    examCheck.USER_DATE = DateTime.Now;

                    var examDel = ctx.AG_IAS_EXAM_ROOM_LICENSE_R.Where(y => y.TESTING_NO == testing_no && y.ACTIVE == "Y");
                    if (examDel != null)
                    {
                        foreach (var idb in examDel)
                        {
                            idb.ACTIVE = "N";
                            idb.USER_DATE_UPDATE = DateTime.Now;
                            idb.USER_ID_UPDATE = USERID;

                        }
                    }

                    //ctx.AG_IAS_EXAM_ROOM_LICENSE_R.Where(y => y.TESTING_NO == testing_no).ToList().ForEach(ctx.AG_IAS_EXAM_ROOM_LICENSE_R.DeleteObject);


                    //ctx.AG_EXAM_LICENSE_R.DeleteObject(examCheck);
                    ctx.SaveChanges();

                }
                else
                {
                    res.ErrorMsg = "ไม่สามารถดำเนินการได้ เนื่องจากรอบสอบนี้มีผู้สมัครสอบแล้ว";
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public DTO.ResponseService<string> GetCourseNumber(string licenseCode)
        {
            DTO.ResponseService<string> CourseNum = new DTO.ResponseService<string>();
            try
            {
                var res = ctx.AG_IAS_EXAM_CONDITION_GROUP.FirstOrDefault(x => x.LICENSE_TYPE_CODE == licenseCode
                                                                            && x.STATUS == "A");
                if (res != null)
                    CourseNum.DataResponse = res.COURSE_NUMBER.ToString();
                else
                    CourseNum.DataResponse = "";

            }
            catch
            {
            }
            return CourseNum;

        }

        #endregion
        #region place

        public DTO.ResponseService<DataSet> GetExamPlaceAndDetailFromProvinceAndGroupCode(string province, string placeG, string Group, int pageNo, int recordPerPage, Boolean Count)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string fieldName = Group == "A" ? "ASSOCIATION_CODE" : "EXAM_PLACE_GROUP_CODE";
                string sql = string.Empty;
                if (!Count)
                {
                    //sql = "select EXAM_PLACE_CODE PLACECODE,EXAM_PLACE_NAME PLACENAME,SEAT_AMOUNT SEAT,case FREE when 'Y' then 'ใช่' else 'ไม่'  end FREE " +
                    //                "  from AG_EXAM_PLACE_R where PROVINCE_CODE like '" + province + "' and " + fieldName + " = '" + placeG + "' and ACTIVE='Y' order by EXAM_PLACE_NAME asc";
                    sql = "select * from(select EXAM_PLACE_CODE PLACECODE,EXAM_PLACE_NAME PLACENAME,SEAT_AMOUNT SEAT,ROW_NUMBER() OVER (ORDER BY EXAM_PLACE_NAME asc) as RUN_NO ,case FREE when 'Y' then 'ใช่' else 'ไม่'  end FREE "
                     + "  from AG_EXAM_PLACE_R where PROVINCE_CODE like '" + province + "' and " + fieldName + " = '" + placeG + "' and ACTIVE='Y')a "
                     + "where a.RUN_NO between " + pageNo.StartRowNumber(recordPerPage).ToString() + " and " + pageNo.ToRowNumber(recordPerPage).ToString();

                }
                else
                {
                    sql = "select count (*) CCount from( "
                        + "select * from(select EXAM_PLACE_CODE PLACECODE,EXAM_PLACE_NAME PLACENAME,SEAT_AMOUNT SEAT,ROW_NUMBER() OVER (ORDER BY EXAM_PLACE_NAME asc) as RUN_NO  ,case FREE when 'Y' then 'ใช่' else 'ไม่'  end FREE "
                        + "from AG_EXAM_PLACE_R where PROVINCE_CODE like '" + province + "' and " + fieldName + " = '" + placeG + "' and ACTIVE='Y')a) ";

                }
                OracleDB ora = new OracleDB();
                var temp = ora.GetDataSet(sql);
                res.DataResponse = temp;
            }
            catch
            {
            }
            return res;
        }

        public DTO.ResponseMessage<bool> SavePlace(string PlaceG, string Province, string Code, string Place,
                                             string Seat, bool Free, string UserID, Boolean Addnew, string GroupType)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_EXAM_PLACE_R CheckCode = ctx.AG_EXAM_PLACE_R.FirstOrDefault(x => x.EXAM_PLACE_CODE == Code);
                if (CheckCode == null)
                {
                    AG_EXAM_PLACE_R epr = new AG_EXAM_PLACE_R();
                    epr.EXAM_PLACE_CODE = Code;

                    if (GroupType == "G")
                        epr.EXAM_PLACE_GROUP_CODE = PlaceG;
                    else
                        epr.ASSOCIATION_CODE = PlaceG;

                    epr.EXAM_PLACE_NAME = Place;
                    epr.PROVINCE_CODE = Province;
                    epr.SEAT_AMOUNT = Convert.ToInt16(Seat);
                    epr.FREE = Free == true ? "Y" : "N";
                    epr.USER_ID = UserID;
                    epr.USER_DATE = DateTime.Now;
                    epr.ACTIVE = "Y";
                    ctx.AG_EXAM_PLACE_R.AddObject(epr);
                    base.ctx.SaveChanges();
                    res.ResultMessage = true;
                }
                else
                {

                    AG_EXAM_PLACE_R tempP = ctx.AG_EXAM_PLACE_R.FirstOrDefault(x => x.EXAM_PLACE_CODE == Code && x.ACTIVE == "N");
                    if (tempP != null)
                    {
                        res.ErrorMsg = "ไม่สามารถเพิ่มข้อมูลได้ <br/>เนื่องจากมีการยกเลิกการใช้งานรหัสสนามสอบ " + Code + " แล้ว <br/> กรุณาใช้รหัสสนามสอบอื่น";
                        res.ResultMessage = false;
                    }
                    else
                    {
                        if (Addnew == true)
                        {
                            res.ErrorMsg = "มีรหัสสนามสอบนี้แล้ว กรุณาแก้ไขข้อมูลแทนการเพิ่มข้อมูลใหม่";
                            res.ResultMessage = false;
                        }
                        else
                        {

                            if (GroupType == "G")
                            {
                                CheckCode.EXAM_PLACE_GROUP_CODE = PlaceG;
                                CheckCode.ASSOCIATION_CODE = null;
                            }
                            else
                            {
                                CheckCode.ASSOCIATION_CODE = PlaceG;
                                CheckCode.EXAM_PLACE_GROUP_CODE = null;
                            }

                            CheckCode.EXAM_PLACE_NAME = Place;
                            CheckCode.PROVINCE_CODE = Province;
                            CheckCode.SEAT_AMOUNT = Convert.ToInt16(Seat);
                            CheckCode.FREE = Free == true ? "Y" : "N";
                            CheckCode.USER_ID_UPDATE = UserID;
                            CheckCode.USER_DATE_UPDATE = DateTime.Now;
                            CheckCode.ACTIVE = "Y";
                            base.ctx.SaveChanges();
                            res.ResultMessage = true;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> DelPlace(string Code, string UserID)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var CheckCode = ctx.AG_EXAM_PLACE_R.FirstOrDefault(x => x.EXAM_PLACE_CODE == Code);
                if (CheckCode != null)
                {

                    CheckCode.ACTIVE = "N";
                    CheckCode.USER_DATE_UPDATE = DateTime.Now;
                    CheckCode.USER_ID_UPDATE = UserID;
                    ctx.AG_EXAM_PLACE_R.MappingToEntity(CheckCode);
                    ctx.SaveChanges();
                }
                else
                {
                    res.ErrorMsg = "ไม่พบรหัสสนามสอบที่เลือก";
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetGVExamRoomByPlaceCode(string code, int pageNo, int recordPerPage, Boolean Count)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            string sql = string.Empty;
            try
            {
                //sql = " select epr.exam_room_code , epr.exam_room_name,epr.seat_amount , epr.exam_place_code,pr.seat_amount SEAT "
                //              + "    from ag_ias_exam_place_room epr "
                //              + "   left join ag_exam_place_r pr on pr.exam_place_code = epr.exam_place_code "
                //              + "    where epr.active='Y' and pr.active ='Y' and epr.exam_place_code ='" + code + "' order by epr.exam_room_code asc";
                if (!Count)
                {
                    sql = "select * from "
                       + "( "
                       + "select epr.exam_room_code , epr.exam_room_name,epr.seat_amount , epr.exam_place_code,pr.seat_amount SEAT   ,ROW_NUMBER() OVER (ORDER BY  epr.exam_room_code asc ) as RUN_NO  "
                       + "from ag_ias_exam_place_room epr    left join ag_exam_place_r pr on pr.exam_place_code = epr.exam_place_code  "
                       + "where epr.active='Y' and pr.active ='Y' and epr.exam_place_code ='" + code + "' "
                       + ")a "
                       + "where a.RUN_NO between " + pageNo.StartRowNumber(recordPerPage).ToString() + " and " + pageNo.ToRowNumber(recordPerPage).ToString()
                       + " order by a.exam_room_code asc";
                }
                else
                {
                    sql = "select count (*) CCount from( "
                       + "select * from "
                       + "( "
                       + "select epr.exam_room_code , epr.exam_room_name,epr.seat_amount , epr.exam_place_code,pr.seat_amount SEAT   ,ROW_NUMBER() OVER (ORDER BY  epr.exam_room_code asc ) as RUN_NO  "
                       + "from ag_ias_exam_place_room epr    left join ag_exam_place_r pr on pr.exam_place_code = epr.exam_place_code  "
                       + "where epr.active='Y' and pr.active ='Y' and epr.exam_place_code ='" + code + "' "
                       + ")a "
                       + "order by a.exam_room_code asc)";
                }



                OracleDB ora = new OracleDB();
                var ress = ora.GetDataSet(sql);
                res.DataResponse = ress;
            }
            catch
            {
            }
            return res;
        }

        public DTO.ResponseService<string> CountCountSeatUse(string testing_no)
        {
            DTO.ResponseService<string> CountSeat = new DTO.ResponseService<string>();
            try
            {
                string sql = " select EXAM_APPLY from  ag_exam_license_r where testing_no ='" + testing_no + "'";
                OracleDB ora = new OracleDB();
                var res = ora.GetDataSet(sql).Tables[0].Rows[0][0].ToString();
                CountSeat.DataResponse = res;
            }
            catch
            {
            }
            return CountSeat;
        }

        public DTO.ResponseMessage<Boolean> CheckUsedPlaceGroup(string ID)
        {
            DTO.ResponseMessage<Boolean> Used = new DTO.ResponseMessage<bool>();
            try
            {
                int res = ctx.AG_EXAM_PLACE_R.Where(x => x.ACTIVE == "Y" && x.EXAM_PLACE_GROUP_CODE == ID).Count();
                if (res > 0)
                    Used.ResultMessage = false;
                else
                    Used.ResultMessage = true;
            }
            catch
            {

            }

            return Used;
        }

        public DTO.ResponseService<string> SumSeatFromPlace(string ID, string Room)
        {
            DTO.ResponseService<string> amount = new DTO.ResponseService<string>();
            try
            {
                string temp = string.Empty;
                if (Room != "")
                {
                    temp = " and epr.exam_room_code not like '" + Room + "%' ";
                }

                var sql = " select PR.SEAT_AMOUNT - A.SEAT_AMOUNT " +
                          " SEAT_AMOUNT from AG_EXAM_PLACE_R PR, " +
                        "  (select case when  sum(EPR.seat_amount) is null then 0 else sum(EPR.seat_amount)  end SEAT_AMOUNT " +
                          "   from ag_ias_exam_place_room EPR  " +
                          "   where epr.exam_place_code = '" + ID + "' and epr.active='Y' " + temp + " ) A " +
                          "   where PR.ACTIVE = 'Y' and PR.EXAM_PLACE_CODE = '" + ID + "' ";
                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);
                amount.DataResponse = ds.Tables[0].Rows[0][0].ToString();

            }
            catch
            {

            }
            return amount;
        }

        #endregion place


        public DTO.ResponseService<DataSet> GetddlGroupType(string PlaceCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                var sql = "select case when exam_place_group_code is  null then 'A' else 'G' end GroupType from AG_EXAM_PLACE_R where EXAM_PLACE_CODE = '" + PlaceCode + "'";
                OracleDB ora = new OracleDB();
                var ex = ora.GetDataSet(sql);
                res.DataResponse = ex;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        #region ManageApplicantIn_OutRoom
        public DTO.ResponseService<List<DTO.ConfigEntity>> ManageApplicantIn_OutRoom()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigEntity>>();
            try
            {
                res.DataResponse = base.ctx.AG_IAS_CONFIG
                                   .Where(s => s.GROUP_CODE == "AP001")
                                   .Select(s => new DTO.ConfigEntity
                                   {
                                       Id = s.ID,
                                       Name = s.ITEM,
                                       Value = s.ITEM_VALUE,
                                       Description = s.DESCRIPTION,
                                       GROUP_CODE = "AP001"
                                   }).ToList();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ManageApplicantIn_OutRoom", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateManageApplicantIn_OutRoom(List<DTO.ConfigEntity> configs)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (DTO.ConfigEntity conf in configs)
                {
                    if (conf.GROUP_CODE != null)
                    {
                        var entConfig = base.ctx.AG_IAS_CONFIG.SingleOrDefault(c => c.GROUP_CODE == conf.GROUP_CODE && c.ID == conf.Id);
                        if (entConfig != null)
                        {
                            entConfig.ITEM_VALUE = conf.Value;
                            entConfig.USER_DATE = DateTime.Now;
                        }
                    }


                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        #endregion ManageApplicantIn_OutRoom


        public DTO.ResponseService<DataSet> GetExamStatistic(string LicenseType, DateTime? DateStart, DateTime? DateEnd)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;
                string selectTable = string.Empty;
                string whereDate = string.Empty;

                Func<string, string> GetTypeCode = (tIn) =>
                {
                    string tOut = string.Empty;
                    switch (tIn)
                    {
                        case "01": tOut = " and B.LICENSE_TYPE_CODE in ('01','07') "; break;
                        case "02": tOut = " and B.LICENSE_TYPE_CODE in ('02','05','06','08') "; break;
                        case "03":
                        case "04": tOut = string.Format(" and B.LICENSE_TYPE_CODE in ('{0}') ", tIn); break;
                    }
                    return tOut;
                };
                Func<string, string> GetTypeName = (tIn) =>
                {
                    string nameOut = string.Empty;
                    switch (tIn)
                    {
                        case "01": nameOut = "ตัวแทนประกันชีวิต"; break;
                        case "02": nameOut = "ตัวแทนประกันวินาศภัย"; break;
                        case "03": nameOut = "นายหน้าประกันชีวิต (บุคคลธรรมดา)"; break;
                        case "04": nameOut = "นายหน้าประกันวินาศ (บุคคลธรรมดา)"; break;
                    }
                    return nameOut;
                };
                if (DateStart != null && DateEnd != null)
                {
                    whereDate = string.Format(" and (A.APPLY_DATE >= to_date('{0}','yyyymmdd') and A.APPLY_DATE <= to_date('{1}','yyyymmdd')) ",
                                                ((DateTime)DateStart).ToString_yyyyMMdd(), ((DateTime)DateEnd).ToString_yyyyMMdd());
                }

                int loopSql = string.IsNullOrEmpty(LicenseType) ? 4 : 1;
                for (int i = 1; i <= loopSql; i++)
                {
                    string code = string.IsNullOrEmpty(LicenseType) ? string.Format("0{0}", i) : LicenseType;
                    string typeName = GetTypeName(code);
                    string typeCode = GetTypeCode(code);
                    selectTable += string.Format(
                                      " select A.*, "
                                    + " case when A.REGIS_EXAM = 0 then 0 else round(((A.EXAM_N * 100)/A.REGIS_EXAM),2) end PERCEN_EXAM, "
                                    + " case when A.EXAM_N = 0 then 0 else round(((A.RESULT_P * 100)/A.EXAM_N),2) end PERCEN_RESULT from ( "
                                    + "     select '{0}' as LICENSE_TYPE_CODE, '{1}' as LICENSE_TYPE_NAME, count(*) REGIS_EXAM  "
                                    + "               ,count(decode(ABSENT_EXAM,'M',ABSENT_EXAM)) EXAM_M "
                                    + "               ,count(decode(ABSENT_EXAM,'N',ABSENT_EXAM)) EXAM_N "
                                    + "               ,count(decode(RESULT,'P',RESULT)) RESULT_P "
                                    + "               ,count(decode(RESULT,'F',RESULT)) RESULT_F "
                                    + "     from AG_APPLICANT_T A, AG_EXAM_LICENSE_R B "
                                    + "     where A.TESTING_NO = B.TESTING_NO and A.ABSENT_EXAM is not null and A.RESULT is not null {2} {3} "
                                    + "     )A "
                                    + (loopSql != 1 && i != 4 ? " union all " : "")
                                    , code, typeName, typeCode, whereDate);
                }

                sql = string.Format(" select * from( {0} ) ", selectTable);
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ExamService_GetExamStatistic", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateExamCondition(List<DTO.ExamLicense> exam)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (DTO.ExamLicense ls in exam)
                {

                    AG_EXAM_LICENSE_R entExam = new AG_EXAM_LICENSE_R();
                    entExam = ctx.AG_EXAM_LICENSE_R
                                          .Where(s => s.TESTING_NO == ls.TESTING_NO && s.SPECIAL == "Y")
                                          .FirstOrDefault();
                    if (entExam != null)
                    {
                        DateTime dtStart = DateTime.Today.AddDays(-3);
                        if (dtStart < ls.TESTING_DATE)
                        {
                            //entExam.IMPORT_TYPE = "In Advance";
                            entExam.IMPORT_TYPE = "N";
                            entExam.PRIVILEGE_STATUS = "Y";
                        }
                        else
                        {
                            entExam.IMPORT_TYPE = "W";
                            //entExam.IMPORT_TYPE = "Walk In";
                            entExam.PRIVILEGE_STATUS = "N";
                        }
                    }
                    using (TransactionScope ts = new TransactionScope())
                    {
                        base.ctx.SaveChanges();
                        ts.Complete();
                    }
                }
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ExamServiceUpdateExamCondition", ex);
            }

            return res;

        }

        public DTO.ResponseService<DataSet> GetExamLicenseByCriteria(string testingNo, string testingDate, int num_page, int RowPerPage, Boolean Count)
        {
            #region
            //DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            //try
            //{
            //    Func<string, string, string> GetCriteria = (criteria, value) =>
            //    {
            //        return !string.IsNullOrEmpty(value)
            //                    ? string.Format(criteria, value)
            //                    : string.Empty;
            //    };

            //    StringBuilder sb = new StringBuilder();

            //    if (testingNo == "null" || testingNo == null || testingNo == "")
            //    {
            //        sb.Append(GetCriteria("LR.TESTING_NO is not ", null));
            //    }
            //    else
            //    {
            //        sb.Append(GetCriteria("LR.TESTING_NO = '{0}' AND ", testingNo));
            //    }

            //    if (testingDate == null)
            //    {
            //        sb.Append(GetCriteria("LR.TESTING_DATE is not ", null));
            //    }
            //    else
            //    {
            //        sb.Append("LR.TESTING_DATE = TO_DATE('" +
            //                            Convert.ToDateTime(testingDate).ToString_yyyyMMdd() + "','yyyymmdd')  ");
            //    }

            //    string sql_export = string.Empty;



            //    string tmp = sb.ToString();

            //    string crit = tmp.Length > 4
            //                    ? " where " + tmp.Substring(0, tmp.Length - 4)
            //                    : tmp;


            //    #region Page
            //    string firstCon = string.Empty;
            //    string midCon = string.Empty;
            //    string lastCon = string.Empty;

            //    if (CountAgain)
            //    {
            //        firstCon = " SELECT COUNT(*) CCount FROM ( ";
            //        midCon = " ";
            //        lastCon = " )";
            //    }
            //    else
            //    {
            //        if (resultPage == 0 && PageSize == 0)
            //        {

            //        }
            //        else
            //        {
            //            firstCon = " SELECT * FROM (";
            //            midCon = " , ROW_NUMBER() OVER (ORDER BY LR.TESTING_NO) RUN_NO ";
            //            lastCon = resultPage == 0
            //                            ? "" :
            //                            " Order By LR.TESTING_DATE ) A where A.RUN_NO BETWEEN " +
            //                               resultPage.StartRowNumber(PageSize).ToString() + " AND " +

            //                               resultPage.ToRowNumber(PageSize).ToString() + " order by A.RUN_NO asc ";

            //        }
            //    }


            //    #endregion Pager
            //    string sql = firstCon + "SELECT distinct LR.TESTING_NO, LR.TESTING_DATE,  " +

            //                 "(Select Count(*) " +
            //                 "From AG_EXAM_LICENSE_R LR " +
            //                 midCon +
            //                 "FROM  " +
            //                 "AG_EXAM_LICENSE_R LR, " +
            //                 crit + " " + lastCon;


            //    OracleDB db = new OracleDB();
            //    DataSet ds = ds = db.GetDataSet(sql);

            //    res.DataResponse = ds;
            //}
            //catch (Exception ex)
            //{
            //    res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            //    LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantByCriteria", ex);
            //}

            //return res;
            #endregion

            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "";
                if (Count)
                {
                    sql += "select count(*) from (";
                }
                else
                {
                    sql += " select * from ( ";
                }
                sql += " select rownum as num, LR.TESTING_NO, LR.TESTING_DATE , LR.EXAM_PLACE_CODE , LR.PRIVILEGE_STATUS "
                    //  + " P.EMAIL, P.MEMBER_TYPE, T.MEMBER_NAME, U.IS_ACTIVE "
                        + " from AG_EXAM_LICENSE_R LR  "
                        + " where LR.TESTING_DATE =  to_date('" + Convert.ToDateTime(testingDate).ToString_yyyyMMdd() + "','yyyy/mm/dd')";

                if (testingNo != "" && testingNo != "0") sql += "and LR.TESTING_NO = " + testingNo;

                if (Count)
                {
                    sql += ") ";
                }
                else
                {
                    sql += " ) where num between " + (((num_page * RowPerPage) - RowPerPage) + 1) + " and " + (num_page * RowPerPage);
                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดในการค้นหาข้อมูล";
                LoggerFactory.CreateLog().Fatal("AccountService_GetExamLicenseByCriteria", ex);
            }
            return res;
        }

        private string GetTestingNo(string batchId, out string errorMessage)
        {
            errorMessage = string.Empty;
            string _testingNo = string.Empty;
            try
            {

                using (OracleConnection objConn = new OracleConnection(DBConnection.GetConnectionString))
                {

                    OracleCommand objCmd = new OracleCommand();

                    objCmd.Connection = objConn;

                    objCmd.CommandText = "AG_IAS_GET_TEST_RUN_NO";

                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("I_License_Type", OracleDbType.Varchar2).Value = batchId;

                    OracleParameter retval = new OracleParameter("myretval", OracleDbType.Varchar2, 50);
                    retval.Direction = ParameterDirection.ReturnValue;
                    objCmd.Parameters.Add(retval);

                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    objConn.Close();

                    
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return _testingNo;
        }

        public DTO.ResponseMessage<bool> IsCanSeatRemainSingle(string testingNo, string examPlaceCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            var examLicense = base.ctx.AG_EXAM_LICENSE_R.SingleOrDefault(w => w.TESTING_NO == testingNo && w.EXAM_PLACE_CODE == examPlaceCode);

            if (examLicense != null)
            {
                if (examLicense.EXAM_PLACE_CODE != null)
                {
                    Int32 apply = examLicense.EXAM_APPLY == null ? 0 : examLicense.EXAM_APPLY.Value;
                    Int32 admission = examLicense.EXAM_ADMISSION == null ? 0 : examLicense.EXAM_ADMISSION.Value;

                    Int32 remain = admission - apply;
                    res.ResultMessage = (remain >= 1);
                    return res;
                }
                res.ResultMessage = true;
                return res;
            }
            return res;
        }



        public DTO.ResponseService<DTO.Exams.CarlendarExamInitResponse> CarlendarExamInit(DTO.Exams.CarlendarExamInitRequest request)
        {
            DTO.ResponseService<DTO.Exams.CarlendarExamInitResponse> response = new DTO.ResponseService<DTO.Exams.CarlendarExamInitResponse>();
            if (String.IsNullOrWhiteSpace(request.UserId))
            {
                response.ErrorMsg = "ไม่พบข้อมูลผู้ทำรายการ";
                return response;
            }
            try
            {
                AG_IAS_PERSONAL_T person = ctx.AG_IAS_PERSONAL_T.SingleOrDefault(u => u.ID == request.UserId);
                if (person == null)
                {
                    response.ErrorMsg = "ไม่พบข้อมูลผู้ทำรายการ";
                    return response;
                }

                DTO.Exams.CarlendarExamInitResponse dataResponse = new DTO.Exams.CarlendarExamInitResponse();


                response.DataResponse = dataResponse;

                DataCenter.DataCenterService dataCenterService = new DataCenter.DataCenterService();

                dataResponse.ExamPlaceGroups = dataCenterService.GetExamPlaceGroup(request.FirstItemExamPlaceGroup).DataResponse;
                dataResponse.ExamPlaces = dataCenterService.GetExamPlace(request.FirstItemExamPlace, dataResponse.ExamPlaceGroups.FirstOrDefault().Id).DataResponse;
                dataResponse.ExamTimes = dataCenterService.GetExamTime(request.FirstItemExamTime).DataResponse;
                DTO.MemberType membertype = (DTO.MemberType)Convert.ToInt32(person.MEMBER_TYPE);
                dataResponse.LicenseTypes = DataItemLicenseTypeFactory.GetLicenseTypeItem(ctx, membertype, person.COMP_CODE);// dataCenterService.GetLicenseType(request.FirstItemLicenseType).DataResponse;

            }
            catch (ApplicationException aex)
            {
                response.ErrorMsg = aex.Message;
            }
            catch (Exception)
            {

                throw;
            }  

            return response;
        }

        public DTO.ResponseService<GetExamByCriteriaResponse> GetExamCarlenderByCriteria(GetExamByCriteriaRequest request)
        {

            DTO.ResponseService<GetExamByCriteriaResponse> res = new DTO.ResponseService<GetExamByCriteriaResponse>();

            try
            {

                Func<string, string, string> GetCriteria = (criteria, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criteria, value)
                                : string.Empty;
                };
                //DateTime dtBegin = new DateTime(Convert.ToInt32(yearMonth.Substring(0, 4)), Convert.ToInt32(yearMonth.Substring(5, 2)), 25);
                //dtBegin.AddMonths(-1);
                //DateTime dtEnd = new DateTime(Convert.ToInt32(yearMonth.Substring(0, 4)), Convert.ToInt32(yearMonth.Substring(5, 2)), 25);
                //dtEnd.AddMonths(1);
                //long lyearMonth = Convert.ToInt64(yearMonth);

                DateTime getDate = new DateTime(request.Year, request.Month, 1);
                System.Globalization.CultureInfo cultureInfo = new CultureInfo("en-US");
                String dateBegin = getDate.GetFirstDayOfWeek().ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));// DateTime.ParseExact(yearMonth, "yyyyMM", null).AddMonths(-1).ToString("yyyyMM") + "25"; //Convert.ToString(dtBegin.ToString("yyyyMMdd"));
                String dateEnd = getDate.AddMonths(1).AddDays(-1).GetLastDayOfWeek().ToString("yyyyMMdd", CultureInfo.CreateSpecificCulture("en-US"));// DateTime.ParseExact(yearMonth, "yyyyMM", null).AddMonths(1).ToString("yyyyMM") + "07";//Convert.ToString(dtEnd.ToString("yyyyMMdd"));
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("PL.EXAM_PLACE_GROUP_CODE like '{0}%' AND ", request.ExamPlaceGroupCode));
                sb.Append(GetCriteria("LR.EXAM_PLACE_CODE like '{0}%' AND ", request.ExamPlaceCode));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", request.LicenseTypeCode));
                sb.Append("LR.TESTING_DATE BETWEEN TO_DATE('" + dateBegin + "','YYYYMMDD') AND TO_DATE('" + dateEnd + "','YYYYMMDD') AND ");
                sb.Append(GetCriteria("LR.TEST_TIME_CODE = '{0}' AND ", request.TimeCode));
                sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}'  ", request.TestingDate));
                sb.Append(GetCriteria("LR.EXAM_OWNER like '{0}%' AND ", request.Owner));
                //sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));
                //sb.Append(GetCriteria("(LR.EXAM_STATE = '{0}' or EXAM_STATE= '{1}' )", "A", "M"));
                //sb.Append("LR.EXAM_STATE = 'M' ");
                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                string sql = "SELECT distinct LR.TESTING_DATE " +
                             "FROM  " +
                             "AG_EXAM_LICENSE_R LR, " +
                             "AG_EXAM_PLACE_R PL " +
                             "WHERE " +
                             crit +
                             " AND LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE  AND LR.EXAM_STATE in ('A','M') Order By LR.TESTING_DATE ";


                OracleDB db = new OracleDB();
                //DataSet ds = ds = db.GetDataSet(sql);
                DataSet ds = db.GetDataSet(sql);
                GetExamByCriteriaResponse dataResponse = new GetExamByCriteriaResponse();
                IList<DateTime> dataResult = new List<DateTime>();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        dataResult.Add(Convert.ToDateTime(item[0]));
                    }
                }
                dataResponse.ExamShedules = dataResult;
                res.DataResponse = dataResponse;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<GetExamScheduleByCriteriaResponse> GetExamScheduleByCriteria(GetExamScheduleByCriteriaRequest request)
        {

            DTO.ResponseService<GetExamScheduleByCriteriaResponse> res = new DTO.ResponseService<GetExamScheduleByCriteriaResponse>();
            GetExamScheduleByCriteriaResponse dataResponse = new GetExamScheduleByCriteriaResponse();
            res.DataResponse = dataResponse;
            DTO.PagingInfo pageInfo = new DTO.PagingInfo();
            IList<ExamInfoDTO> examInfos = new List<ExamInfoDTO>();

            dataResponse.PageInfo = pageInfo;
            if (String.IsNullOrWhiteSpace(request.UserId))
            {
                res.ErrorMsg = "ไม่พบข้อมูลผู้ทำรายการ";
                return res;
            }

            AG_IAS_PERSONAL_T person = ctx.AG_IAS_PERSONAL_T.SingleOrDefault(u => u.ID == request.UserId);
            if (person == null)
            {
                res.ErrorMsg = "ไม่พบข้อมูลผู้ทำรายการ";
                return res;
            }



            if (request.PageInfo != null)
            {
                pageInfo.CurrentPage = request.PageInfo.CurrentPage;
                pageInfo.ItemsPerPage = request.PageInfo.ItemsPerPage;
                pageInfo.TotalItems = request.PageInfo.TotalItems;
            }

            try
            {

                Func<string, string, string> GetCriterion = (criterion, value) =>
                {
                    return !string.IsNullOrEmpty(value)
                                ? string.Format(criterion, value)
                                : string.Empty;
                };





                StringBuilder from = new StringBuilder("FROM  " +
                     "AG_EXAM_LICENSE_R LR, AG_EXAM_TIME_R TM, vw_ias_province PV, AG_LICENSE_TYPE_R LT, AG_AGENT_TYPE_R AG, AG_EXAM_PLACE_R PL " +
                     "left join AG_EXAM_PLACE_GROUP_R GR on PL.EXAM_PLACE_GROUP_CODE = GR.EXAM_PLACE_GROUP_CODE " +
                     " left join AG_IAS_ASSOCIATION ASSO on pl.association_code = ASSO.ASSOCIATION_CODE " +
                     "  WHERE LR.TEST_TIME_CODE = TM.TEST_TIME_CODE AND LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE " +
                     " AND PL.PROVINCE_CODE = PV.ID AND LR.LICENSE_TYPE_CODE = LT.LICENSE_TYPE_CODE AND AG.AGENT_TYPE = LT.AGENT_TYPE " +
                     " AND LR.EXAM_STATE in ('A','M') and lr.exam_owner like '" + request.ExamCriteria.OwnerCode + "%' ");

                DTO.MemberType membertype = (DTO.MemberType)Convert.ToInt32(person.MEMBER_TYPE);
                IEnumerable<DTO.DataItem> licenseTypes = DataItemLicenseTypeFactory.GetLicenseTypeItem(ctx, membertype, person.COMP_CODE);

                StringBuilder criteria = new StringBuilder();
                criteria.Append(GetCriterion("PL.EXAM_PLACE_GROUP_CODE like '{0}%' AND ", request.ExamCriteria.ExamPlaceGroupCode));
                criteria.Append(GetCriterion("LR.EXAM_PLACE_CODE like '{0}%' AND ", request.ExamCriteria.ExamPlaceCode));
                if (String.IsNullOrWhiteSpace(request.ExamCriteria.LicenseTypeCode))
                {
                    String litypes = "";
                    foreach (var item in licenseTypes)
                    {
                        litypes += String.Format("'{0}', ", item.Id);
                    }

                    litypes.Remove(litypes.LastIndexOf(','), 1);
                    criteria.Append(GetCriterion("LR.LICENSE_TYPE_CODE in ({0}) AND ", litypes));
                }
                else
                    criteria.Append(GetCriterion("LR.LICENSE_TYPE_CODE = '{0}' AND ", request.ExamCriteria.LicenseTypeCode));

                criteria.Append(GetCriterion("LT.AGENT_TYPE = '{0}' AND ", request.ExamCriteria.AgentType));

                if (request.ExamCriteria.Day == -1)
                {
                    String yearMonth = String.Format("{0}{1}", request.ExamCriteria.Year.ToString("0000"), request.ExamCriteria.Month.ToString("00"));
                    criteria.Append(GetCriterion("TO_CHAR(LR.TESTING_DATE,'YYYYMM') = '{0}' AND ", yearMonth));
                }
                else
                {
                    String yearMonthday = String.Format("{0}{1}{2}", request.ExamCriteria.Year.ToString("0000")
                                                                     , request.ExamCriteria.Month.ToString("00")
                                                                     , request.ExamCriteria.Day.ToString("00"));
                    criteria.Append(GetCriterion("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", yearMonthday));
                }
                criteria.Append(GetCriterion("LR.TEST_TIME_CODE = '{0}' AND ", request.ExamCriteria.TimeCode));
                //sb.Append(GetCriteria("TO_CHAR(LR.TESTING_DATE,'YYYYMMDD') = '{0}' AND ", testingDate));

                string tmp = criteria.ToString();

                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;


                string firstCon = string.Empty;
                string midCon = string.Empty;
                string lastCon = string.Empty;
                OracleDB db = new OracleDB();
                if (request.PageInfo.TotalItems == 0)
                {

                    String countSql = "select count(*) CCount " + from + crit;
                    DataTable dt = db.GetDataTable(countSql);

                    if (dt != null)
                    {
                        pageInfo.CurrentPage = 0;
                        pageInfo.ItemsPerPage = request.PageInfo.ItemsPerPage;
                        pageInfo.TotalItems = dt.Rows.Count;
                    }
                }


                string sql = "select * from ( SELECT distinct LR.TESTING_NO, LR.TESTING_DATE, TM.TEST_TIME,LR.EXAM_OWNER , (select ASSOCIATION_NAME from AG_IAS_ASSOCIATION where ASSOCIATION_CODE = LR.EXAM_OWNER)EXAM_OWNER_Name , " +
                             " case when GR.EXAM_PLACE_GROUP_NAME is null then ASSO.ASSOCIATION_NAME else GR.EXAM_PLACE_GROUP_NAME end EXAM_PLACE_GROUP_NAME,PL.EXAM_PLACE_NAME,PV.NAME PROVINCE, AG.AGENT_TYPE, " +
                             "(Select Count(*) " +
                             "From AG_APPLICANT_T exLr " +
                             "Where EXLR.TESTING_NO = LR.TESTING_NO AND " +
                             "EXLR.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND (EXLR.RECORD_STATUS IS NULL or EXLR.RECORD_STATUS != 'X')) TOTAL_APPLY, " +
                             "LR.EXAM_ADMISSION, LT.LICENSE_TYPE_NAME, LR.EXAM_FEE, " +
                             "LR.TEST_TIME_CODE,LR.EXAM_PLACE_CODE,case when GR.EXAM_PLACE_GROUP_CODE is null " +
                             " then ASSO.ASSOCIATION_CODE else GR.EXAM_PLACE_GROUP_CODE end EXAM_PLACE_GROUP_CODE , " +
                             "PL.PROVINCE_CODE,LR.LICENSE_TYPE_CODE, " +
                             " LR.EXAM_APPLY || '/'|| LR.EXAM_ADMISSION SEAT_AMOUNT " +
                        " " + midCon +
                             from +

                             crit + ")";


                //DataSet ds = ds = db.GetDataSet(sql);
                DataSet ds = new DataSet();
                Int32 currentRow = 0;
                if (pageInfo.ItemsPerPage == 0)
                {
                    ds = db.GetDataSet(sql);
                }
                else
                {
                    ds = db.GetDataSet(sql, pageInfo.CurrentPage - 1, pageInfo.ItemsPerPage);
                    currentRow = (pageInfo.CurrentPage - 1) * pageInfo.ItemsPerPage;
                }

                Func<object, string> GetStringValue = (value) =>
                {
                    return (value != null)
                                ? value.ToString()
                                : string.Empty;
                };
                Func<object, DateTime> GetDateTimeValue = (value) =>
                {
                    return (value != null)
                                ? Convert.ToDateTime(value)
                                : new DateTime();
                };
                Func<object, Decimal> GetDecimalValue = (value) =>
                {
                    return (value != null)
                                ? Convert.ToDecimal(value)
                                : new Decimal();
                };
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    currentRow++;
                    ExamInfoDTO entry = new ExamInfoDTO();
                    entry.RunNo = currentRow;
                    entry.TestingNo = GetStringValue(row["TESTING_NO"]);
                    entry.TestingDate = GetDateTimeValue(row["TESTING_DATE"]);
                    entry.TestingTime = GetStringValue(row["TEST_TIME"]);
                    entry.ExamPlaceGroup = GetStringValue(row["EXAM_PLACE_GROUP_NAME"]);
                    entry.ExamPlace = GetStringValue(row["EXAM_PLACE_NAME"]);
                    entry.Province = GetStringValue(row["PROVINCE"]);
                    entry.SeatAmount = GetStringValue(row["SEAT_AMOUNT"]);
                    entry.LicenseType = GetStringValue(row["LICENSE_TYPE_NAME"]);
                    entry.ExamFee = GetDecimalValue(row["EXAM_FEE"]);
                    entry.AgentType = GetStringValue(row["AGENT_TYPE"]);
                    entry.ExamOwner = GetStringValue(row["EXAM_OWNER_Name"]);
                    entry.EXAM_PLACE_Code = GetStringValue(row["EXAM_PLACE_Code"]);
                    entry.EXAM_PLACE_NAME = GetStringValue(row["EXAM_PLACE_NAME"]);
                    entry.TEST_TIME_CODE = GetStringValue(row["TEST_TIME_CODE"]);
                    entry.LICENSE_TYPE_CODE = GetStringValue(row["LICENSE_TYPE_CODE"]);
                    entry.PROVINCE_CODE = GetStringValue(row["PROVINCE_CODE"]);
                    entry.EXAM_PLACE_GROUP_CODE = GetStringValue(row["EXAM_PLACE_GROUP_CODE"]);
                    examInfos.Add(entry);
                }
                res.DataResponse.ExamInfos = examInfos;
                res.DataResponse.PageInfo = pageInfo;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }
    }
}

