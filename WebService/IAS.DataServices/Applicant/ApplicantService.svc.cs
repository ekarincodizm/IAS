using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using IAS.DAL;
using IAS.Utils;
using System.Transactions;
using System.Data;
using System.Text.RegularExpressions;
using Oracle.DataAccess.Client;
using System.Globalization;
using IAS.DataServices.Applicant.ApplicantRequestUploads;
using IAS.DataServices.Properties;
using System.ServiceModel.Activation;
using System.Configuration;

using System.IO;
using IAS.DTO;
using IAS.DataServices.Applicant.ApplicantHelper;
using IAS.DataServices.Class;
using IAS.Common.Logging;
using IAS.DTO.FileService;
using IAS.DataServices.FileManager;
using System.Collections;

namespace IAS.DataServices.Applicant
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApplicantService : AbstractService, IApplicantService
    {

        #region Constructor

        public ApplicantService()
        {
        }
        public ApplicantService(IAS.DAL.Interfaces.IIASPersonEntities _ctx)
            : base(_ctx)
        {

        }

        #endregion

        private static String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();

        #region Method

        public DTO.ResponseMessage<bool> Insert(DTO.Applicant appl)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_APPLICANT_T ent = new AG_APPLICANT_T();

                appl.MappingToEntity(ent);

                ctx.AG_APPLICANT_T.AddObject(ent);

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_Insert", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> Update(DTO.Applicant appl)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_APPLICANT_T entApp = new AG_APPLICANT_T();
                entApp = ctx.AG_APPLICANT_T
                                      .Where(s => s.APPLICANT_CODE == appl.APPLICANT_CODE && s.TESTING_NO == appl.TESTING_NO && s.EXAM_PLACE_CODE == appl.EXAM_PLACE_CODE)
                                      .FirstOrDefault();

                entApp.APPLICANT_CODE = appl.APPLICANT_CODE;
                entApp.TESTING_NO = appl.TESTING_NO;
                entApp.EXAM_PLACE_CODE = appl.EXAM_PLACE_CODE;
                entApp.ID_CARD_NO = appl.ID_CARD_NO;
                entApp.PRE_NAME_CODE = appl.PRE_NAME_CODE;
                entApp.NAMES = appl.NAMES;
                entApp.LASTNAME = appl.LASTNAME;
                entApp.BIRTH_DATE = appl.BIRTH_DATE;
                entApp.SEX = appl.SEX;
                entApp.EDUCATION_CODE = appl.EDUCATION_CODE;
                entApp.ADDRESS1 = string.IsNullOrEmpty(appl.ADDRESS1) ? entApp.ADDRESS1 : appl.ADDRESS1;
                entApp.AREA_CODE = string.IsNullOrEmpty(appl.AREA_CODE) ? entApp.AREA_CODE : appl.AREA_CODE;

                using (TransactionScope ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_Update", ex);
            }

            return res;

        }

        public DTO.ResponseService<DTO.Applicant> GetById(string Id)
        {
            throw new NotImplementedException();
        }

        public DTO.ResponseService<DataSet>
            GetApplicantById(string applicantCode, string testingNo, string examPlaceCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.APPLICANT_CODE = '{0}' AND ", applicantCode));
                sb.Append(GetCriteria("AP.TESTING_NO = '{0}' AND ", testingNo));
                sb.Append(GetCriteria("AP.EXAM_PLACE_CODE = '{0}' AND ", examPlaceCode));

                string tmp = sb.ToString();
                string crit = tmp.Length > 4
                                ? " AND " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;



                string sql = "SELECT AP.*,LR.TESTING_DATE,TT.NAME TITLE_NAME, " +
                             "       ED.EDUCATION_NAME " +
                             "FROM	 AG_APPLICANT_T AP, " +
                             "       VW_IAS_TITLE_NAME TT, " +
                             "       AG_EDUCATION_R ED, " +
                             "       AG_EXAM_LICENSE_R LR " +
                             "WHERE	 AP.PRE_NAME_CODE = TT.ID AND " +
                             "       AP.EDUCATION_CODE = ED.EDUCATION_CODE AND " +
                             "       AP.TESTING_NO = LR.TESTING_NO AND " +
                             "       AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE " + crit;

                OracleDB ora = new OracleDB();
                DataSet ds = ora.GetDataSet(sql);

                res.DataResponse = ds;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantById", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> Delete(string Id)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_APPLICANT_T ent = ctx.AG_APPLICANT_T
                                        .Where(s => s.APPLICANT_CODE == Id.ToInt())
                                        .FirstOrDefault();

                ctx.AG_APPLICANT_T.DeleteObject(ent);

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_Delete", ex);
            }

            return res;
        }

        //ตรวจสอบความถูกต้องของข้อมูล
        private void ValidateApplicantTempBeforSubmit(DTO.ApplicantTemp temp)
        {
            //ตรวจสอบว่าผู้สมัครสอบดังกล่าวเคยสมัครสอบหรือยัง
            var existApp = base.ctx.AG_APPLICANT_T
                                    .Where(w => w.TESTING_NO == temp.TESTING_NO &&
                                               w.EXAM_PLACE_CODE == temp.EXAM_PLACE_CODE &&
                                               w.ID_CARD_NO == temp.ID_CARD_NO &&
                                               w.RECORD_STATUS != "X")
                                    .FirstOrDefault();

            if (existApp != null)
            {
                temp.ERROR_MSG += "\n" + Resources.errorApplicantService_001;
            }

            //ตรวจสอบว่ามีรายการสอบที่ผู้สมัครสอบส่งมาหรือไม่
            var exam = base.ctx.AG_EXAM_LICENSE_R
                               .Where(w => w.TESTING_NO == temp.TESTING_NO &&
                                          w.EXAM_PLACE_CODE == temp.EXAM_PLACE_CODE)
                               .FirstOrDefault();

            if (exam == null)
            {
                temp.ERROR_MSG += "\n" + Resources.errorApplicantService_002 + temp.TESTING_NO;
            }
        }

        //ตรวจสอบที่ว่าง
        private int SeatRemain(string testingNo, string examPlaceCode, int applyAmount)
        {
            var ent = base.ctx.AG_EXAM_LICENSE_R
                              .SingleOrDefault(s => s.TESTING_NO == testingNo &&
                                                    s.EXAM_PLACE_CODE == examPlaceCode);
            int apply = 0;
            short admission = (short)0;

            if (!string.IsNullOrEmpty(ent.EXAM_PLACE_CODE))
            {
                var entSeat = base.ctx.AG_EXAM_PLACE_R.SingleOrDefault(s => s.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE);

                if (entSeat != null)
                {
                    if (ent != null)
                    {
                        apply = ent.EXAM_APPLY == null ? "0".ToShort() : ent.EXAM_APPLY.Value;
                        admission = entSeat.SEAT_AMOUNT == null ? "0".ToShort() : entSeat.SEAT_AMOUNT.Value;
                    }
                }
            }
            int remain = admission - apply;
            return remain < 0 ? 0 : remain;

        }

        //ตรวจสอบที่ว่าง with List<string>
        private List<int> SeatListRemain(List<string> testingNo, List<string> examPlaceCode, int applyAmount)
        {
            List<int> lsRemain = new List<int>();
            IQueryable<AG_EXAM_LICENSE_R> ent = base.ctx.AG_EXAM_LICENSE_R.Where(s => testingNo.Contains(s.TESTING_NO) && examPlaceCode.Contains(s.EXAM_PLACE_CODE));

            int apply = 0;
            short admission = (short)0;

            if (ent.ToList().Count > 0)
            {
                ent.ToList().ForEach(chk =>
                {
                    AG_EXAM_PLACE_R entSeat = base.ctx.AG_EXAM_PLACE_R.FirstOrDefault(s => s.EXAM_PLACE_CODE == chk.EXAM_PLACE_CODE);

                    apply = chk.EXAM_APPLY == null ? "0".ToShort() : chk.EXAM_APPLY.Value;
                    admission = entSeat.SEAT_AMOUNT == null ? "0".ToShort() : entSeat.SEAT_AMOUNT.Value;

                    int remain = admission - apply;
                    lsRemain.Add(remain);
                });
            }

            return lsRemain;

            //if (!string.IsNullOrEmpty(ent.EXAM_PLACE_CODE))
            //{
            //    var entSeat = base.ctx.AG_EXAM_PLACE_R.SingleOrDefault(s => s.EXAM_PLACE_CODE == ent.EXAM_PLACE_CODE);

            //    if (entSeat != null)
            //    {
            //        if (ent != null)
            //        {
            //            apply = ent.EXAM_APPLY == null ? "0".ToShort() : ent.EXAM_APPLY.Value;
            //            admission = entSeat.SEAT_AMOUNT == null ? "0".ToShort() : entSeat.SEAT_AMOUNT.Value;
            //        }
            //    }
            //}
            //int remain = admission - apply;
            //return remain < 0 ? 0 : remain;

        }

        //ตรวจสอบข้อมูลก่อนนำเข้า
        private void ValidateApplicantTemp(DTO.ApplicantTemp app)
        {
            app.ERROR_MSG = "";

            List<VW_IAS_TITLE_NAME> lsTitle = base.ctx.VW_IAS_TITLE_NAME.ToList();
            List<AG_EDUCATION_R> lsEdu = base.ctx.AG_EDUCATION_R.ToList();

            //หารายการ Head
            var appHead = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP
                                  .Where(w => w.UPLOAD_GROUP_NO == app.UPLOAD_GROUP_NO)
                                  .FirstOrDefault();

            //ดึงรหัสประเภทใบอนุญาต
            string licenseTypeCode = appHead.LICENSE_TYPE_CODE.Trim().Length == 1
                                        ? "0" + appHead.LICENSE_TYPE_CODE.Trim()
                                        : appHead.LICENSE_TYPE_CODE.Trim();

            //ตรวจสอบว่าเลขใบอนุญาตเป็นประเภทตัวแทนหรือไม่
            bool checkCompCode = base.ctx.AG_LICENSE_TYPE_R
                                         .Where(w => w.LICENSE_TYPE_CODE == licenseTypeCode &&
                                                     w.AGENT_TYPE == "A")
                                         .Count() > 0;


            //เลขที่นั่งสอบต้องเป็นตัวเลข
            string app_Code = app.APPLICANT_CODE.ToString();
            Regex regDigit = new Regex(@"^\d+$");

            if (!regDigit.IsMatch(app_Code) || app_Code.Trim().Length == 0)
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_003;
            }

            app.APPLICANT_CODE = app_Code.ToInt();

            //ตรวจสอบเลขบัตรประชาชน
            if (app.ID_CARD_NO.Trim().Length == 0)
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_004;
            }

            if (app.ID_CARD_NO.Trim().Length > 0 && app.ID_CARD_NO.Trim().Length != 13)
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_005;
            }


            //หารหัสคำนำหน้าชื่อ
            string title = app.TITLE == "น.ส." ? "นางสาว" : app.TITLE;
            VW_IAS_TITLE_NAME entTitle = lsTitle.FirstOrDefault(s => s.NAME == title);
            if (entTitle != null)
                app.PRE_NAME_CODE = entTitle.ID.ToString();
            else
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_006;
            }

            //ตรวจสอบชื่อ
            if (string.IsNullOrEmpty(app.NAMES))
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_007;
            }

            //ตรวจสอบนามสกุล
            if (string.IsNullOrEmpty(app.LASTNAME))
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_008;
            }

            //ตรวจสอบเพศ
            string _sex = "M";

            if (app.TITLE.Contains("หญิง") || app.TITLE.Contains("นาง") || app.TITLE.Contains("นางสาว") ||
               app.TITLE.Contains("แม่") || app.TITLE.Contains("พญ."))
            {
                _sex = "F";
            }


            //คำนำหน้าชื่อ กับ เพศ สัมพันธ์กันหรือไม่
            if (string.IsNullOrEmpty(app.SEX))
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_009;
            }
            else
            {
                if (_sex != app.SEX)
                {
                    app.ERROR_MSG += "\n" + Resources.errorApplicantService_010;
                }
            }

            //ตรวจสอบวุฒิการศึกษา
            if (string.IsNullOrEmpty(app.EDUCATION_CODE))
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_011;
            }
            else
            {
                var eduEnt = lsEdu.Where(w => w.EDUCATION_CODE == app.EDUCATION_CODE).FirstOrDefault();
                if (eduEnt == null)
                {
                    app.ERROR_MSG += "\n" + Resources.errorApplicantService_012;
                }
            }


            //ตรวจสอบรหัสบริษัท
            if (checkCompCode && string.IsNullOrEmpty(app.INSUR_COMP_CODE))
            {
                app.ERROR_MSG += "\n" + Resources.errorApplicantService_013;
            }

            if (!string.IsNullOrEmpty(app.INSUR_COMP_CODE))
            {
                var insurCompEnt = base.ctx.VW_IAS_COM_CODE
                                           .Where(w => w.ID == app.INSUR_COMP_CODE)
                                           .FirstOrDefault();
                if (insurCompEnt == null)
                {
                    app.ERROR_MSG += "\n" + Resources.errorApplicantService_014;
                }
            }

            app.APPLY_DATE = DateTime.Now.Date;
            app.TESTING_DATE = appHead.TESTING_DATE;

        }


        ///// <summary>
        ///// เก็บข้อมูลจากไฟล์สมัครสอบแบบกลุ่มใน Temp และตรวจสอบข้อมูล
        ///// </summary>
        ///// <param name="data">Class เก็บข้อมูล Upload</param>
        ///// <param name="fileName">ชื่อไฟล์ที่ Upload</param>
        ///// <param name="regType">Upload โดยบริษัทหรือสมาคม</param>
        ///// <param name="testingNo">รหัสการสอบ</param>
        ///// <param name="examPlaceCode">รหัสสนามสอบ</param>
        ///// <param name="userId">user id</param>
        ///// <returns>DTO.ResponseService<UploadResult<UploadHeader, DTO.ApplicantTemp>></returns>
        //public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>
        //    InsertAndCheckApplicantGroupUpload(DTO.UploadData data, string fileName,
        //                                       DTO.RegistrationType regType,
        //                                       string testingNo, DTO.UserProfile userProfile)
        //{
        //    var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>();
        //    res.DataResponse = new DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>();

        //    res.DataResponse.Header = new List<DTO.UploadHeader>();
        //    res.DataResponse.Detail = new List<DTO.ApplicantTemp>();



        //    Func<string, bool> IsRightDate = (aryString) =>
        //    {
        //        if (string.IsNullOrEmpty(aryString)) return false;

        //        if (aryString.Trim().Length != 10) return false;

        //        DateTime _dt;
        //        string[] strDate = aryString.Split('/');
        //        if (strDate.Length < 3)
        //        {
        //            return false;
        //        }
        //        if (strDate[2] != null && Convert.ToInt32(strDate[2]) > 2500)
        //        {
        //            int iDate = Convert.ToInt32(strDate[2]) - 543;
        //            aryString = strDate[0] + "/" + strDate[1] + "/" + iDate;
        //        }
        //        return DateTime.TryParseExact(aryString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dt);
        //        //return DateTime.TryParse(aryString, out _dt);
        //    };

        //    Func<string[], int, string> GetByIndex = (aryString, index) =>
        //    {
        //        return aryString.Length - 1 >= index
        //                    ? aryString[index].Trim()
        //                    : string.Empty;
        //    };

        //    try
        //    {
        //        #region เตรียมข้อมูลส่วน Header

        //        string[] headData = data.Header.Split(',');

        //        #region ตรวจสอบข้อมูล Header

        //        //ตรวจสอบสถานที่สอบ
        //        string provinceCode = GetByIndex(headData, 1);
        //        string compCode = GetByIndex(headData, 2);
        //        string examLicensetype = GetByIndex(headData, 3);

        //        DateTime examDate;
        //        string stdate = GetByIndex(headData, 4);

        //        if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
        //        {
        //            if (compCode != userProfile.CompCode)
        //            {
        //                res.ErrorMsg = "กลุ่มสนามสอบไม่ถูกต้อง";
        //                return res;
        //            }
        //        }


        //        if (!string.IsNullOrEmpty(stdate))
        //        {
        //            examDate = GetByIndex(headData, 4).String_dd_MM_yyyy_ToDate('/', true);
        //        }
        //        else
        //        {
        //            res.ErrorMsg = "วันที่สอบไม่ถูกต้อง";
        //            return res;
        //        }

        //        string timcode = GetByIndex(headData, 7);
        //        string examPlaceCode = provinceCode + compCode;
        //        string totalExam = GetByIndex(headData, 5);
        //        var examPlaceEnt = base.ctx.AG_EXAM_PLACE_R.Where(w => w.EXAM_PLACE_CODE == examPlaceCode).FirstOrDefault();



        //        var examTotal = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo && w.EXAM_PLACE_CODE == examPlaceCode).FirstOrDefault();
        //        int examTotalToInt = examTotal == null ? "0".ToInt() : examTotal.EXAM_ADMISSION.ToInt();

        //        var emamEnt = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo);

        //        if (userProfile.MemberType == 2)
        //        {
        //            var examDetail = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo).FirstOrDefault();


        //            //ตรวจสอบรหัสสนามสอบ
        //            if (examDetail.EXAM_PLACE_CODE != examPlaceCode)
        //            {
        //                res.ErrorMsg = "รหัสสนามสอบไม่ถูกต้อง";
        //                return res;

        //            }

        //            //ตรวจสอบรหัสประเภทใบอนุญาต
        //            if (examDetail.LICENSE_TYPE_CODE != examLicensetype)
        //            {
        //                res.ErrorMsg = "รหัสประเภทใบอนุญาตไม่ถูกต้อง";
        //                return res;

        //            }

        //            //ตรวจสอบวันที่สอบ
        //            if (examDetail.TESTING_DATE != examDate)
        //            {
        //                res.ErrorMsg = "วันที่สอบไม่ถูกต้อง";
        //                return res;
        //            }

        //            //ตรวจสอบจำนวนผู้สอบ
        //            if (!string.IsNullOrEmpty(totalExam))
        //            {
        //                int numTotalexam = Convert.ToInt16(totalExam);
        //                int countbody = data.Body.Count;
        //                if (countbody != numTotalexam)
        //                {
        //                    res.ErrorMsg = "จำนวนผู้สมัครไม่ถูกต้อง";
        //                    return res;
        //                }
        //            }

        //            //ตรวจสอบจำนวนเงิน
        //            //if (!string.IsNullOrEmpty(free))
        //            //{
        //            //    long numfree = Convert.ToInt64(free);
        //            //    if (examDetail.EXAM_FEE != numfree)
        //            //    {
        //            //        res.ErrorMsg = "จำนวนเงินค่าสมัครไม่ถูกต้อง";
        //            //        return res;
        //            //    }

        //            //}


        //            //ตรวจสอบ timecode
        //            if (!string.IsNullOrEmpty(timcode))
        //            {
        //                if (examDetail.TEST_TIME_CODE != timcode)
        //                {
        //                    res.ErrorMsg = "เวลาสอบไม่ถูกต้อง";
        //                    return res;

        //                }

        //            }


        //        }

        //        if (regType == DTO.RegistrationType.Insurance)
        //        {
        //            if (examDate < DateTime.Today)
        //            {
        //                res.ErrorMsg = "ไม่สามารถสอบย้อนหลังได้";
        //                return res;
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(testingNo))
        //        {
        //            if (!string.IsNullOrEmpty(testingNo))
        //            {
        //                int remain = this.SeatRemain(testingNo, examPlaceCode, examTotalToInt);
        //                if (remain - totalExam.ToInt() < 0)
        //                {
        //                    res.ErrorMsg = "จำนวนผู้สมัครมากกว่าจำนวนที่รับได้";
        //                    return res;
        //                }
        //            }


        //            if (examPlaceEnt == null)
        //            {
        //                res.ErrorMsg = "ไม่พบรหัสสนามสอบ " + examPlaceCode;
        //                return res;
        //            }
        //        }

        //        if (examPlaceEnt == null)
        //        {
        //            res.ErrorMsg = "ไม่พบรหัสสนามสอบ " + examPlaceCode;
        //            return res;
        //        }

        //        //ตรวจสอบเลขที่บริษัท
        //        string strCompCode = GetByIndex(headData, 2);
        //        if (string.IsNullOrEmpty(strCompCode))
        //        {
        //            res.ErrorMsg = "ไม่พบรหัสบริษัท";
        //            return res;
        //        }

        //        //ตรวจสอบรหัสประเภทใบอนุุญาต
        //        string licenseTypeCode = GetByIndex(headData, 3);

        //        var licenseTypeEnt = base.ctx.AG_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == licenseTypeCode).FirstOrDefault();
        //        if (licenseTypeCode == null)
        //        {
        //            res.ErrorMsg = "รหัสประเภทใบอนุญาตผิด " + licenseTypeCode;
        //            return res;
        //        }

        //        //ตรวจสอบวันที่สอบ
        //        DateTime testingDate = DateTime.MinValue;
        //        string strTestingDate = GetByIndex(headData, 4);

        //        if (!IsRightDate(strTestingDate))
        //        {
        //            res.ErrorMsg = "วันที่สอบผิด ";
        //            return res;
        //        }
        //        //else  //Confirm กับ คุณเบิร์ดแล้ว ว่าเป็นปี พ.ศ.
        //        //{
        //        //    if (strTestingDate.Substring(6, 2) != "20")
        //        //    {
        //        //        res.ErrorMsg = "วันที่สอบผิด ปีผิดต้องเป็น ค.ศ. " + strTestingDate;
        //        //        return res;
        //        //    }
        //        //}

        //        testingDate = GetByIndex(headData, 4).String_dd_MM_yyyy_ToDate('/', true);

        //        //ตรวจสอบจำนวนคนต้องเป็นตัวเลขเท่านั้น
        //        Regex regDigit = new Regex(@"^\d+$");

        //        string examApply = GetByIndex(headData, 5);
        //        if (!regDigit.IsMatch(examApply))
        //        {
        //            res.ErrorMsg = "จำนวนคนต้องเป็นตัวเลขล้วน";
        //            return res;
        //        }

        //        if (data.Body.Count == 0)
        //        {
        //            res.ErrorMsg = "ไม่พบรายการผู้สมัครสอบ หรือ รูปแบบของแฟ้มข้อมูลไม่ถูกต้อง";
        //            return res;
        //        }

        //        if (examApply.ToInt() != data.Body.Count)
        //        {
        //            res.ErrorMsg = string.Format("จำนวนคนรวมส่วน Header {0} คน ไม่ตรงกับจำนวนข้อมูล Detail {1} คน", examApply, data.Body.Count);
        //            return res;
        //        }

        //        //ตรวจสอบจำนวนเงินรวมทั้งหมด
        //        string amount = headData.Length > 8
        //                            ? (GetByIndex(headData, 6).Replace("\"", "") + GetByIndex(headData, 7).Replace("\"", "")).Replace(",", "")
        //                            : GetByIndex(headData, 6).Replace(",", "");

        //        if (!regDigit.IsMatch(amount))
        //        {
        //            res.ErrorMsg = "จำนวนเงินทั้งหมดต้องเป็นตัวเลขล้วน " + amount;
        //            return res;
        //        }

        //        //ตรวจสอบรหัสเวลาสอบ
        //        if (string.IsNullOrEmpty(GetByIndex(headData, 7)))
        //        {
        //            res.ErrorMsg = "ไม่มีรหัสเวลา";
        //            return res;
        //        }
        //        string testTimeCode = GetByIndex(headData, 7);
        //        var examTime =  base.ctx.AG_EXAM_TIME_R.Where(w => w.TEST_TIME_CODE == testTimeCode).FirstOrDefault();
        //        var timeR =     base.ctx.AG_EXAM_TIME_R.Where(w => w.TEST_TIME_CODE == examTime.TEST_TIME_CODE).FirstOrDefault();
        //        if (examTime == null && timeR == null || timeR == null || examTime == null)
        //        {
        //            res.ErrorMsg = "รหัสเวลาสอบผิด " + examTime;
        //            return res;
        //        }

        //        ////ตรวจสอบจำนวนเงินรวมที่ระบุ
        //        //decimal sumFree = 0;

        //        //for (int i = 0; i < data.Body.Count; i++)
        //        //{
        //        //    string d = data.Body[i];
        //        //    string[] rawData = d.ClearQuoteInCSV().Split(',');

        //        //    decimal dfree = GetByIndex(rawData, 6).ToDecimal();

        //        //    sumFree = sumFree + dfree;
        //        //}
        //        //decimal numMoney = Convert.ToDecimal(_money);

        //        //if (numMoney != sumFree)
        //        //{
        //        //    res.ErrorMsg = "\nระบุจำนวนเงินรวมไม่ถูกต้อง";
        //        //    return res;
        //        //}

        //        #endregion

        //        //Gen รหัสกลุ่ม
        //        res.DataResponse.GroupId = OracleDB.GetGenAutoId();

        //        //เก็บรายการส่วน Header เข้า Temp
        //        DTO.ApplicantHeaderTemp appHead = new DTO.ApplicantHeaderTemp
        //        {
        //            UPLOAD_GROUP_NO = res.DataResponse.GroupId,
        //            SOURCE_TYPE = regType == DTO.RegistrationType.Insurance ? "C" : "A",
        //            PROVINCE_CODE = provinceCode,
        //            COMP_CODE = compCode,
        //            LICENSE_TYPE_CODE = licenseTypeCode,
        //            TESTING_DATE = testingDate,
        //            EXAM_APPLY = examApply.ToShort(),
        //            EXAM_AMOUNT = amount.ToDecimal(),
        //            TEST_TIME_CODE = testTimeCode,
        //            FILENAME = fileName
        //        };


        //        //Add Header
        //        var entHead = new AG_IAS_APPLICANT_HEADER_TEMP();
        //        appHead.MappingToEntity(entHead);
        //        base.ctx.AG_IAS_APPLICANT_HEADER_TEMP.AddObject(entHead);

        //        #endregion


        //        #region เตรียมข้อมูลส่วน Details

        //        //ตรวจสอบเลขที่นั่งสอบต้องไม่ซ้ำ

        //        List<VW_IAS_TITLE_NAME> lsTitle = base.ctx.VW_IAS_TITLE_NAME.ToList();
        //        List<AG_EDUCATION_R> lsEdu = base.ctx.AG_EDUCATION_R.ToList();

        //        licenseTypeCode = licenseTypeCode.Trim().Length == 1 ? "0" + licenseTypeCode.Trim() : licenseTypeCode;

        //        bool checkCompCode = base.ctx.AG_LICENSE_TYPE_R
        //                                     .Where(w => w.LICENSE_TYPE_CODE == licenseTypeCode &&
        //                                                 w.AGENT_TYPE == "A")
        //                                     .Count() > 0;

        //        int iCount = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.TESTING_NO == testingNo && w.LOAD_STATUS == "C").Count();
        //        bool checkIDCard = false;
        //        for (int i = 0; i < data.Body.Count; i++)
        //        {
        //            DTO.ApplicantTemp app = new DTO.ApplicantTemp();

        //            string d = data.Body[i];

        //            string[] rawData = d.Split(',');

        //            string app_Code = GetByIndex(rawData, 0);

        //            if (!regDigit.IsMatch(app_Code) || app_Code.Trim().Length == 0)
        //            {
        //                app.ERROR_MSG += "\nเลขที่สอบต้องเป็นตัวเลขล้วน";
        //            }

        //            //ตรวจสอบว่าเลขที่นั่งสอบซ้ำหรือไม่
        //            //int iCountDupp = res.DataResponse.Detail
        //            //                                 .Where(delegate(DTO.ApplicantTemp tm)
        //            //                                 {
        //            //                                     return tm.APPLICANT_CODE == app_Code.ToInt();
        //            //                                 })
        //            //                                 .Count();

        //            //if (iCountDupp > 0)
        //            //{
        //            //    app.ERROR_MSG += "\nเลขที่นั่งสอบซ้ำ";
        //            //}

        //            app.UPLOAD_GROUP_NO = appHead.UPLOAD_GROUP_NO;
        //            app.SEQ_NO = (i + 1).ToString("0000");
        //            app.APPLICANT_CODE = app_Code.ToInt() + iCount;
        //            app.TESTING_NO = testingNo;
        //            app.ID_CARD_NO = GetByIndex(rawData, 1);




        //            //ตรวจสอบเลขบัตรประชาชน
        //            if (app.ID_CARD_NO.Trim().Length == 0)
        //            {
        //                app.ERROR_MSG += "\nไม่มีเลขบัตรประชาชน";
        //            }

        //            //if (app.ID_CARD_NO.Trim().Length > 0 && app.ID_CARD_NO.Trim().Length != 13)
        //            //{
        //            //    app.ERROR_MSG += "\nเลขบัตรประชาชนไม่ถูกต้อง";
        //            //}


        //            //check Digit
        //            var ValidateId = Utils.IdCard.Verify(app.ID_CARD_NO.Trim());
        //            if (ValidateId == false)
        //            {
        //                app.ERROR_MSG += "\nเลขบัตรประชาชนไม่ถูกต้อง";
        //            }


        //            //var chkIDCard = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.ID_CARD_NO == app.ID_CARD_NO && w.UPLOAD_GROUP_NO == app.UPLOAD_GROUP_NO).FirstOrDefault();
        //            //if (chkIDCard != null)
        //            //{
        //            //    {
        //            //        app.ERROR_MSG += "\nเลขบัตรประชาชนซ้ำ";
        //            //    }
        //            //}
        //            //if (app.ID_CARD_NO == GetByIndex(rawData, 1))
        //            //{
        //            //    app.ERROR_MSG += "\nเลขบัตรประชาชนซ้ำ";
        //            //}


        //            //ตรวจสอบคำนำหน้าชื่อ
        //            app.TITLE = GetByIndex(rawData, 2);

        //            string title = app.TITLE == "น.ส." ? "นางสาว" : app.TITLE;
        //            VW_IAS_TITLE_NAME entTitle = lsTitle.FirstOrDefault(s => s.NAME == title);
        //            if (entTitle != null)
        //                app.PRE_NAME_CODE = entTitle.ID.ToString();
        //            else
        //            {
        //                app.ERROR_MSG += "\nคำนำชื่อไม่ถูกต้อง";
        //            }

        //            app.USER_ID = userProfile.Id;
        //            app.USER_DATE = DateTime.Now;

        //            //ตรวจสอบชื่อ
        //            app.NAMES = GetByIndex(rawData, 3);
        //            if (string.IsNullOrEmpty(app.NAMES))
        //            {
        //                app.ERROR_MSG += "\nไม่มีชื่อ";
        //            }

        //            //ตรวจสอบนามสกุล
        //            app.LASTNAME = GetByIndex(rawData, 4);
        //            if (string.IsNullOrEmpty(app.LASTNAME))
        //            {
        //                app.ERROR_MSG += "\nไม่มีนามสกุล";
        //            }

        //            //ตรวจสอบวันเกิด
        //            DateTime birthDate = DateTime.MinValue;
        //            string strBirthDate = GetByIndex(rawData, 5);

        //            if (!IsRightDate(strBirthDate))
        //            {
        //                app.ERROR_MSG += "\nวันเกิดผิด ";
        //            }
        //            else
        //            {
        //                app.BIRTH_DATE = strBirthDate.String_dd_MM_yyyy_ToDate('/', true);

        //                //validate วันเกิด
        //                //if (!string.IsNullOrEmpty(strBirthDate))
        //                //{

        //                //    DateTime Temp;
        //                //    if (DateTime.TryParse(strBirthDate, out Temp))
        //                //    {
        //                //          DateTime currDate = DateTime.Now;
        //                //          DateTime dateFromCtrl = Convert.ToDateTime(strBirthDate);

        //                //          string currDateFormat = String.Format("{0:dd/MM/yyyy}", currDate).ToString();
        //                //          string birthDateFormat = String.Format("{0:dd/MM/yyyy}", dateFromCtrl).ToString();
        //                //          DateTime currTime = DateTime.Parse(currDateFormat);
        //                //          DateTime birthTime = DateTime.Parse(birthDateFormat);

        //                //    int dateCompare = DateTime.Compare(birthTime, currTime);

        //                //    if (dateCompare == 0)
        //                //    {
        //                //        app.ERROR_MSG += "\nไม่สามารถระบุวันเกิดมากกว่าหรือเท่ากับเวลาปัจจุบัน";
        //                //    }
        //                //    //BirthDay > CurrentTime
        //                //    if (dateCompare == 1)
        //                //    {
        //                //        app.ERROR_MSG += "\nไม่สามารถระบุวันเกิดมากกว่าหรือเท่ากับเวลาปัจจุบัน";
        //                //    }
        //                //    }
        //                //}

        //            }


        //            //ตรวจสอบเพศ
        //            string _sex = "M";

        //            if (app.TITLE.Contains("หญิง") || app.TITLE.Contains("นาง") || app.TITLE.Contains("นางสาว") ||
        //               app.TITLE.Contains("แม่") || app.TITLE.Contains("พญ."))
        //            {
        //                _sex = "F";
        //            }


        //            string sex = GetByIndex(rawData, 6);
        //            app.SEX = (sex == "ช" || sex == "M" ? "M" : "F");

        //            //คำนำหน้าชื่อ กับ เพศ สัมพันธ์กันหรือไม่
        //            if (string.IsNullOrEmpty(app.SEX))
        //            {
        //                app.ERROR_MSG += "\nไม่ระบุเพศ";
        //            }
        //            else
        //            {
        //                if (_sex != app.SEX)
        //                {
        //                    app.ERROR_MSG += "\nรหัสเพศไม่สัมพันธ์กับคำนำหน้าชื่อ";
        //                }
        //            }

        //            //ตรวจสอบวุฒิการศึกษา
        //            app.EDUCATION_CODE = GetByIndex(rawData, 7).ToInt().ToString("00");

        //            if (string.IsNullOrEmpty(app.EDUCATION_CODE))
        //            {
        //                app.ERROR_MSG += "\nไม่ระบุรหัสประเภทการศึกษา";
        //            }
        //            else
        //            {
        //                var eduEnt = lsEdu.Where(w => w.EDUCATION_CODE == app.EDUCATION_CODE).FirstOrDefault();
        //                if (eduEnt == null)
        //                {
        //                    app.ERROR_MSG += "\nรหัสประเภทการศึกษาผิด";
        //                }
        //            }




        //            app.INSUR_COMP_CODE = GetByIndex(rawData, 8);





        //            //ตรวจสอบรหัสบริษัท''

        //            //  if (userProfile.MemberType == 2)
        //            // {
        //            //app.ERROR_MSG += "\nตัวแทนต้องระบุรหัสบริษัท";


        //            //  }

        //            if (!string.IsNullOrEmpty(app.INSUR_COMP_CODE))
        //            {
        //                var insurCompEnt = base.ctx.VW_IAS_COM_CODE.Where(w => w.ID == app.INSUR_COMP_CODE).FirstOrDefault();
        //                if (insurCompEnt == null)
        //                {
        //                    app.ERROR_MSG += "\nตัวแทนต้องระบุรหัสบริษัท";
        //                }
        //                else
        //                {
        //                    if (app.INSUR_COMP_CODE != userProfile.CompCode)
        //                    {
        //                        app.ERROR_MSG += "\nระบุรหัสบริษัทไม่ถูกต้อง";
        //                    }
        //                }
        //            }

        //            app.EXAM_PLACE_CODE = examPlaceCode;
        //            app.APPLY_DATE = DateTime.Now.Date;
        //            app.TESTING_DATE = appHead.TESTING_DATE;

        //            if (regType == DTO.RegistrationType.Insurance)
        //            {
        //                var chkLoad = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.TESTING_NO == testingNo && w.ID_CARD_NO == app.ID_CARD_NO && w.LOAD_STATUS == "C").FirstOrDefault();
        //                //var head = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP.Where(w => w.UPLOAD_GROUP_NO == chkLoad.UPLOAD_GROUP_NO &&
        //                //                                                        w.LICENSE_TYPE_CODE == licenseTypeCode &&
        //                //                                                        w.TESTING_DATE == testingDate &&
        //                //                                                        w.TEST_TIME_CODE == testTimeCode).FirstOrDefault();
        //                if (chkLoad != null)
        //                {
        //                    app.ERROR_MSG += "\nเลขที่ประจำตัวประชาชน ได้ถูกนำเข้าไปก่อนหน้านี้แล้ว";
        //                }
        //                //if (GetByIndex(headData, 8) == )
        //                //{

        //                //}
        //            }
        //            else if (regType == DTO.RegistrationType.Association)
        //            {
        //                var chkLoad = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.ID_CARD_NO == app.ID_CARD_NO && w.LOAD_STATUS == "C" && w.EXAM_PLACE_CODE == app.EXAM_PLACE_CODE).FirstOrDefault();
        //                if (chkLoad != null)
        //                {
        //                    var head = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP.Where(w => w.UPLOAD_GROUP_NO == chkLoad.UPLOAD_GROUP_NO &&
        //                                                                            w.LICENSE_TYPE_CODE == licenseTypeCode &&
        //                                                                            w.TESTING_DATE == testingDate &&
        //                                                                            w.TEST_TIME_CODE == testTimeCode).FirstOrDefault();
        //                    if (chkLoad != null && head != null)
        //                    {
        //                        app.ERROR_MSG += "\nเลขที่ประจำตัวประชาชน ได้ถูกนำเข้าไปก่อนหน้านี้แล้ว";
        //                    }
        //                }

        //            }

        //            //this.ValidateApplicantTemp(app);
        //            DAL.AG_IAS_APPLICANT_DETAIL_TEMP ent = new AG_IAS_APPLICANT_DETAIL_TEMP();

        //            app.MappingToEntity(ent);
        //            base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.AddObject(ent);

        //            //check dup 13/11/2013
        //            //res.DataResponse.Detail.Add(app);


        //        }
        //        using (TransactionScope ts = new TransactionScope())
        //        {
        //            base.ctx.SaveChanges();
        //            ts.Complete();
        //        }


        //        //check id dup by ming
        //        //check dup 13/11/2013
        //        var detail = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.UPLOAD_GROUP_NO == res.DataResponse.GroupId && w.ID_CARD_NO == w.ID_CARD_NO).ToList();
        //        var duplicates = detail.GroupBy(i => i.ID_CARD_NO).Where(g => g.Count() > 1).Select(g => g.Key);

        //        if (duplicates != null)
        //        {
        //            foreach (var iddup in duplicates)
        //            {
        //                string id = iddup;
        //                string err = "\nเลขบัตรประชาชนซ้ำ";
        //                Updaatedetail(res.DataResponse.GroupId, id, err);
        //            }

        //        }

        //        res.DataResponse.Detail = GetDetailbyGroupNo(res.DataResponse.GroupId);


        //        #endregion

        //        int total = res.DataResponse.Detail.Count();
        //        int missingTrans = res.DataResponse.Detail.Where(w => !string.IsNullOrEmpty(w.ERROR_MSG)).Count();
        //        int rightTrans = res.DataResponse.Detail.Where(w => string.IsNullOrEmpty(w.ERROR_MSG)).Count();

        //        res.DataResponse.Header.Add(new DTO.UploadHeader
        //        {
        //            Totals = total,
        //            MissingTrans = missingTrans,
        //            RightTrans = rightTrans,
        //            UploadFileName = fileName
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }

        //    return res;
        //}
        /// <summary>
        /// เก็บข้อมูลจากไฟล์สมัครสอบแบบกลุ่มใน Temp และตรวจสอบข้อมูล
        /// </summary>
        /// <param name="data">Class เก็บข้อมูล Upload</param>
        /// <param name="fileName">ชื่อไฟล์ที่ Upload</param>
        /// <param name="regType">Upload โดยบริษัทหรือสมาคม</param>
        /// <param name="testingNo">รหัสการสอบ</param>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <param name="userId">user id</param>
        /// <returns>DTO.ResponseService<UploadResult<UploadHeader, DTO.ApplicantTemp>></returns>
        public DTO.ResponseService<DTO.SummaryReceiveApplicant>
            InsertAndCheckApplicantGroupUpload(InsertAndCheckApplicantGroupUploadRequest request)
        {
            var res = new DTO.ResponseService<DTO.SummaryReceiveApplicant>();
            res.DataResponse = new DTO.SummaryReceiveApplicant();                   

            //res.DataResponse.Header = new List<DTO.UploadHeader>();
            //res.DataResponse.Detail = new List<DTO.ApplicantTemp>();

            DTO.UploadData data =  ReadFileUpload(request.FilePath);            

            try
            {
                DTO.ApplicantUploadRequest req = new DTO.ApplicantUploadRequest()
                                                                {
                                                                    FileName = request.FileName,
                                                                    TestingNo = request.TestingNo,
                                                                    UploadData = data,
                                                                    UserProfile = request.UserProfile,
                                                                    ExamPlaceCode = request.ExamPlaceCode
                                                                };
                DTO.ResponseService<ApplicantFileHeader> response = ApplicantFileFactory.ConcreateApplicantFileRequest(ctx, req);
                if (response.IsError)
                {
                    res.ErrorMsg = response.ErrorMsg;
                    LoggerFactory.CreateLog().Fatal("ApplicantService_InsertAndCheckApplicantGroupUpload", response.ErrorMsg);
                }
                if (response.DataResponse == null)
                {
                    res.ErrorMsg = Resources.errorApplicantFileHeader_001;
                }

                AG_IAS_APPLICANT_HEADER_TEMP applicantHeadTemp = new AG_IAS_APPLICANT_HEADER_TEMP();
                response.DataResponse.MappingToEntity<ApplicantFileHeader, AG_IAS_APPLICANT_HEADER_TEMP>(applicantHeadTemp);
                ctx.AG_IAS_APPLICANT_HEADER_TEMP.AddObject(applicantHeadTemp);

                var examLicense = ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == req.TestingNo).FirstOrDefault();
                foreach (ApplicantFileDetail detail in response.DataResponse.ApplicantFileDetails)
                {
                    AG_IAS_APPLICANT_DETAIL_TEMP detailTemp = new AG_IAS_APPLICANT_DETAIL_TEMP();
                    detail.EXAM_PLACE_CODE = examLicense.EXAM_PLACE_CODE;
                    detail.MappingToEntity<ApplicantFileDetail, AG_IAS_APPLICANT_DETAIL_TEMP>(detailTemp);
                    ctx.AG_IAS_APPLICANT_DETAIL_TEMP.AddObject(detailTemp);
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                DTO.SummaryReceiveApplicant summarize = response.DataResponse.ValidateDataOfData();
                res.DataResponse = summarize;


            }
            catch (Exception)
            {
                res.ErrorMsg = Resources.errorApplicantFileHeader_001;
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertAndCheckApplicantGroupUpload", res.ErrorMsg);
            }

            return res;
        }
        private void Updaatedetail(string upload, string id, string err)
        {

            string sql = "UPDATE AG_IAS_APPLICANT_DETAIL_TEMP "
                         + " SET ERROR_MSG = ERROR_MSG ||" + "'" + err + "' "
                         + " WHERE UPLOAD_GROUP_NO = " + "'" + upload + "'"
                         + "and ID_CARD_NO = " + "'" + id + "'";

            OracleDB ora = new OracleDB();
            ora.GetDataSet(sql);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="impId"></param>
        /// <returns></returns>
        private List<DTO.ApplicantTemp> GetDetailbyGroupNo(string impId)
        {
            var res = new List<DTO.ApplicantTemp>();

            try
            {
                res = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP
                   .Where(w => w.UPLOAD_GROUP_NO == impId)
                   .Select(s => new DTO.ApplicantTemp
                   {
                       APPLICANT_CODE = s.APPLICANT_CODE,
                       TESTING_NO = s.TESTING_NO,
                       EXAM_PLACE_CODE = s.EXAM_PLACE_CODE,
                       ACCEPT_OFF_CODE = s.ACCEPT_OFF_CODE,
                       APPLY_DATE = s.APPLY_DATE,
                       ID_CARD_NO = s.ID_CARD_NO,
                       PRE_NAME_CODE = s.PRE_NAME_CODE,
                       NAMES = s.NAMES,
                       LASTNAME = s.LASTNAME,
                       BIRTH_DATE = s.BIRTH_DATE,
                       SEX = s.SEX,
                       EDUCATION_CODE = s.EDUCATION_CODE,
                       ADDRESS1 = s.ADDRESS1,
                       ADDRESS2 = s.ADDRESS2,
                       AREA_CODE = s.AREA_CODE,
                       PROVINCE_CODE = s.PROVINCE_CODE,
                       ZIPCODE = s.ZIPCODE,
                       TELEPHONE = s.TELEPHONE,
                       AMOUNT_TRAN_NO = s.AMOUNT_TRAN_NO,
                       PAYMENT_NO = s.PAYMENT_NO,
                       INSUR_COMP_CODE = s.INSUR_COMP_CODE,
                       ABSENT_EXAM = s.ABSENT_EXAM,
                       RESULT = s.RESULT,
                       EXPIRE_DATE = s.EXPIRE_DATE,
                       LICENSE = s.LICENSE,
                       CANCEL_REASON = s.CANCEL_REASON,
                       RECORD_STATUS = s.RECORD_STATUS,
                       USER_ID = s.USER_ID,
                       USER_DATE = s.USER_DATE,
                       EXAM_STATUS = s.EXAM_STATUS,
                       REQUEST_NO = s.REQUEST_NO,
                       UPLOAD_GROUP_NO = s.UPLOAD_GROUP_NO,
                       SEQ_NO = s.SEQ_NO,
                       TITLE = s.TITLE,
                       ERROR_MSG = s.ERROR_MSG,
                       LOAD_STATUS = s.LOAD_STATUS,
                   }).ToList();
            }
            catch (Exception ex)
            {
                //res = ex.Message;
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertAndCheckApplicantGroupUpload", ex);
            }
            return res;
        }






        /// <summary>
        /// ปรับปรุงจาก InsertAndCheckApplicantGroupUpload
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        /// <param name="regType"></param>
        /// <param name="testingNo"></param>
        /// <param name="userProfile"></param>
        /// <returns> DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>
            InsertApplicantGroupUpload(DTO.UploadData data, string fileName, string testingNo, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>();
            res.DataResponse = new DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>();

            res.DataResponse.Header = new List<DTO.UploadHeader>();
            res.DataResponse.Detail = new List<DTO.ApplicantTemp>();



            Func<string, bool> IsRightDate = (aryString) =>
            {
                if (string.IsNullOrEmpty(aryString)) return false;

                if (aryString.Trim().Length != 10) return false;

                DateTime _dt;
                string[] strDate = aryString.Split('/');
                if (strDate.Length < 3)
                {
                    return false;
                }
                if (strDate[2] != null && Convert.ToInt32(strDate[2]) > 2500)
                {
                    int iDate = Convert.ToInt32(strDate[2]) - 543;
                    aryString = strDate[0] + "/" + strDate[1] + "/" + iDate;
                }
                return DateTime.TryParseExact(aryString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _dt);
                //return DateTime.TryParse(aryString, out _dt);
            };

            Func<string[], int, string> GetByIndex = (aryString, index) =>
            {
                return aryString.Length - 1 >= index
                            ? aryString[index].Trim()
                            : string.Empty;
            };

            try
            {
                #region เตรียมข้อมูลส่วน Header

                string[] headData = data.Header.Split(',');

                #region ตรวจสอบข้อมูล Header

                //ตรวจสอบสถานที่สอบ
                string provinceCode = GetByIndex(headData, 1);
                string compCode = GetByIndex(headData, 2);
                string examLicensetype = GetByIndex(headData, 3);

                DateTime examDate;
                string stdate = GetByIndex(headData, 4);


                if (!string.IsNullOrEmpty(stdate))
                {
                    examDate = GetByIndex(headData, 4).String_dd_MM_yyyy_ToDate('/', true);
                }
                else
                {
                    res.ErrorMsg = Resources.errorApplicantService_015;
                    return res;
                }

                string timcode = GetByIndex(headData, 7);
                string examPlaceCode = provinceCode + compCode;
                string totalExam = GetByIndex(headData, 5);
                var examPlaceEnt = base.ctx.AG_EXAM_PLACE_R.Where(w => w.EXAM_PLACE_CODE == examPlaceCode).FirstOrDefault();



                var examTotal = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo && w.EXAM_PLACE_CODE == examPlaceCode).FirstOrDefault();
                int examTotalToInt = examTotal == null ? "0".ToInt() : examTotal.EXAM_ADMISSION.ToInt();

                var emamEnt = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo);

                if (userProfile.MemberType == (int)DTO.RegistrationType.Insurance)
                {
                    var examDetail = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo).FirstOrDefault();


                    //ตรวจสอบรหัสสนามสอบ
                    if (examDetail.EXAM_PLACE_CODE != examPlaceCode)
                    {
                        res.ErrorMsg = Resources.errorApplicantService_016;
                        return res;

                    }

                    //ตรวจสอบรหัสประเภทใบอนุญาต
                    if (examDetail.LICENSE_TYPE_CODE != examLicensetype)
                    {
                        res.ErrorMsg = Resources.errorApplicantService_017;
                        return res;

                    }

                    //ตรวจสอบวันที่สอบ
                    if (examDetail.TESTING_DATE != examDate)
                    {
                        res.ErrorMsg = Resources.errorApplicantService_015;
                        return res;
                    }

                    //ตรวจสอบจำนวนผู้สอบ
                    if (!string.IsNullOrEmpty(totalExam))
                    {
                        int numTotalexam = Convert.ToInt16(totalExam);
                        int countbody = data.Body.Count;
                        if (countbody != numTotalexam)
                        {
                            res.ErrorMsg = Resources.errorApplicantService_018;
                            return res;
                        }
                    }

                    //ตรวจสอบจำนวนเงิน
                    //if (!string.IsNullOrEmpty(free))
                    //{
                    //    long numfree = Convert.ToInt64(free);
                    //    if (examDetail.EXAM_FEE != numfree)
                    //    {
                    //        res.ErrorMsg = "จำนวนเงินค่าสมัครไม่ถูกต้อง";
                    //        return res;
                    //    }

                    //}


                    //ตรวจสอบ timecode
                    if (!string.IsNullOrEmpty(timcode))
                    {
                        if (examDetail.TEST_TIME_CODE != timcode)
                        {
                            res.ErrorMsg = Resources.errorApplicantService_019;
                            return res;

                        }

                    }

                    if (examDate < DateTime.Today)
                    {
                        res.ErrorMsg = Resources.errorApplicantService_020;
                        return res;
                    }



                }


                if (!string.IsNullOrEmpty(testingNo))
                {
                    if (!string.IsNullOrEmpty(testingNo))
                    {
                        int remain = this.SeatRemain(testingNo, examPlaceCode, examTotalToInt);
                        if (remain - totalExam.ToInt() < 0)
                        {
                            res.ErrorMsg = Resources.errorApplicantService_021;
                            return res;
                        }
                    }


                    if (examPlaceEnt == null)
                    {
                        res.ErrorMsg = Resources.errorApplicantService_023 + examPlaceCode;
                        return res;
                    }
                }

                if (examPlaceEnt == null)
                {
                    res.ErrorMsg = Resources.errorApplicantService_023 + examPlaceCode;
                    return res;
                }

                //ตรวจสอบเลขที่บริษัท
                string strCompCode = GetByIndex(headData, 2);
                if (string.IsNullOrEmpty(strCompCode))
                {
                    res.ErrorMsg = Resources.errorApplicantService_022;
                    return res;
                }

                //ตรวจสอบรหัสประเภทใบอนุุญาต
                string licenseTypeCode = GetByIndex(headData, 3);

                var licenseTypeEnt = base.ctx.AG_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == licenseTypeCode).FirstOrDefault();
                if (licenseTypeCode == null)
                {
                    res.ErrorMsg = Resources.errorApplicantService_024 + licenseTypeCode;
                    return res;
                }

                //ตรวจสอบวันที่สอบ
                DateTime testingDate = DateTime.MinValue;
                string strTestingDate = GetByIndex(headData, 4);

                if (!IsRightDate(strTestingDate))
                {
                    res.ErrorMsg = Resources.errorApplicantService_025;
                    return res;
                }
                //else  //Confirm กับ คุณเบิร์ดแล้ว ว่าเป็นปี พ.ศ.
                //{
                //    if (strTestingDate.Substring(6, 2) != "20")
                //    {
                //        res.ErrorMsg = "วันที่สอบผิด ปีผิดต้องเป็น ค.ศ. " + strTestingDate;
                //        return res;
                //    }
                //}

                testingDate = GetByIndex(headData, 4).String_dd_MM_yyyy_ToDate('/', true);

                //ตรวจสอบจำนวนคนต้องเป็นตัวเลขเท่านั้น
                Regex regDigit = new Regex(@"^\d+$");

                string examApply = GetByIndex(headData, 5);
                if (!regDigit.IsMatch(examApply))
                {
                    res.ErrorMsg = Resources.errorApplicantService_026;
                    return res;
                }

                if (data.Body.Count == 0)
                {
                    res.ErrorMsg = Resources.errorApplicantService_027;
                    return res;
                }

                if (examApply.ToInt() != data.Body.Count)
                {
                    res.ErrorMsg = string.Format("จำนวนคนรวมส่วน Header {0} คน ไม่ตรงกับจำนวนข้อมูล Detail {1} คน", examApply, data.Body.Count);
                    return res;
                }

                //ตรวจสอบจำนวนเงินรวมทั้งหมด
                string amount = headData.Length > 8
                                    ? (GetByIndex(headData, 6).Replace("\"", "") + GetByIndex(headData, 7).Replace("\"", "")).Replace(",", "")
                                    : GetByIndex(headData, 6).Replace(",", "");

                if (!regDigit.IsMatch(amount))
                {
                    res.ErrorMsg = Resources.errorApplicantService_028 + amount;
                    return res;
                }

                //ตรวจสอบรหัสเวลาสอบ
                if (string.IsNullOrEmpty(GetByIndex(headData, 7)))
                {
                    res.ErrorMsg = Resources.errorApplicantService_029;
                    return res;
                }
                string testTimeCode = GetByIndex(headData, 7);
                var examTime = base.ctx.AG_EXAM_TIME_R.Where(w => w.TEST_TIME_CODE == testTimeCode).FirstOrDefault();
                var timeR = base.ctx.AG_EXAM_TIME_R.Where(w => w.TEST_TIME_CODE == examTime.TEST_TIME_CODE).FirstOrDefault();
                if (examTime == null && timeR == null || timeR == null || examTime == null)
                {
                    res.ErrorMsg = Resources.errorApplicantService_030 + examTime;
                    return res;
                }

                ////ตรวจสอบจำนวนเงินรวมที่ระบุ
                //decimal sumFree = 0;

                //for (int i = 0; i < data.Body.Count; i++)
                //{
                //    string d = data.Body[i];
                //    string[] rawData = d.ClearQuoteInCSV().Split(',');

                //    decimal dfree = GetByIndex(rawData, 6).ToDecimal();

                //    sumFree = sumFree + dfree;
                //}
                //decimal numMoney = Convert.ToDecimal(_money);

                //if (numMoney != sumFree)
                //{
                //    res.ErrorMsg = "\nระบุจำนวนเงินรวมไม่ถูกต้อง";
                //    return res;
                //}

                #endregion

                //Gen รหัสกลุ่ม
                res.DataResponse.GroupId = OracleDB.GetGenAutoId();

                //เก็บรายการส่วน Header เข้า Temp
                DTO.ApplicantHeaderTemp appHead = new DTO.ApplicantHeaderTemp
                {
                    UPLOAD_GROUP_NO = res.DataResponse.GroupId,
                    SOURCE_TYPE = (userProfile.MemberType == (int)DTO.RegistrationType.Insurance) ? "C" : "A",
                    PROVINCE_CODE = provinceCode,
                    COMP_CODE = compCode,
                    LICENSE_TYPE_CODE = licenseTypeCode,
                    TESTING_DATE = testingDate,
                    EXAM_APPLY = examApply.ToShort(),
                    EXAM_AMOUNT = amount.ToDecimal(),
                    TEST_TIME_CODE = testTimeCode,
                    FILENAME = fileName
                };


                //Add Header
                var entHead = new AG_IAS_APPLICANT_HEADER_TEMP();
                appHead.MappingToEntity(entHead);
                base.ctx.AG_IAS_APPLICANT_HEADER_TEMP.AddObject(entHead);

                #endregion


                #region เตรียมข้อมูลส่วน Details

                //ตรวจสอบเลขที่นั่งสอบต้องไม่ซ้ำ

                List<VW_IAS_TITLE_NAME> lsTitle = base.ctx.VW_IAS_TITLE_NAME.ToList();
                List<AG_EDUCATION_R> lsEdu = base.ctx.AG_EDUCATION_R.ToList();

                licenseTypeCode = licenseTypeCode.Trim().Length == 1 ? "0" + licenseTypeCode.Trim() : licenseTypeCode;

                bool checkCompCode = base.ctx.AG_LICENSE_TYPE_R
                                             .Where(w => w.LICENSE_TYPE_CODE == licenseTypeCode &&
                                                         w.AGENT_TYPE == "A")
                                             .Count() > 0;

                int iCount = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.TESTING_NO == testingNo && w.LOAD_STATUS == "C").Count();
                bool checkIDCard = false;
                for (int i = 0; i < data.Body.Count; i++)
                {
                    DTO.ApplicantTemp app = new DTO.ApplicantTemp();

                    string d = data.Body[i];

                    string[] rawData = d.Split(',');

                    string app_Code = GetByIndex(rawData, 0);

                    if (!regDigit.IsMatch(app_Code) || app_Code.Trim().Length == 0)
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_003;
                    }

                    //ตรวจสอบว่าเลขที่นั่งสอบซ้ำหรือไม่
                    //int iCountDupp = res.DataResponse.Detail
                    //                                 .Where(delegate(DTO.ApplicantTemp tm)
                    //                                 {
                    //                                     return tm.APPLICANT_CODE == app_Code.ToInt();
                    //                                 })
                    //                                 .Count();

                    //if (iCountDupp > 0)
                    //{
                    //    app.ERROR_MSG += "\nเลขที่นั่งสอบซ้ำ";
                    //}

                    app.UPLOAD_GROUP_NO = appHead.UPLOAD_GROUP_NO;
                    app.SEQ_NO = (i + 1).ToString("0000");
                    app.APPLICANT_CODE = app_Code.ToInt() + iCount;
                    app.TESTING_NO = testingNo;
                    app.ID_CARD_NO = GetByIndex(rawData, 1);




                    //ตรวจสอบเลขบัตรประชาชน
                    //if (app.ID_CARD_NO.Trim().Length == 0)
                    //{
                    //    app.ERROR_MSG += "\nไม่มีเลขบัตรประชาชน";
                    //}

                    //if (app.ID_CARD_NO.Trim().Length > 0 && app.ID_CARD_NO.Trim().Length != 13)
                    //{
                    //    app.ERROR_MSG += "\nเลขบัตรประชาชนไม่ถูกต้อง";
                    //}

                    //var chkIDCard = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.ID_CARD_NO == app.ID_CARD_NO && w.UPLOAD_GROUP_NO == app.UPLOAD_GROUP_NO).FirstOrDefault();
                    //if (chkIDCard != null)
                    //{
                    //    {
                    //        app.ERROR_MSG += "\nเลขบัตรประชาชนซ้ำ";
                    //    }
                    //}
                    //if (app.ID_CARD_NO == GetByIndex(rawData, 1))
                    //{
                    //    app.ERROR_MSG += "\nเลขบัตรประชาชนซ้ำ";
                    //}


                    //ตรวจสอบคำนำหน้าชื่อ
                    app.TITLE = GetByIndex(rawData, 2);

                    string title = app.TITLE == "น.ส." ? "นางสาว" : app.TITLE;
                    VW_IAS_TITLE_NAME entTitle = lsTitle.FirstOrDefault(s => s.NAME == title);
                    if (entTitle != null)
                        app.PRE_NAME_CODE = entTitle.ID.ToString();
                    else
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_006;
                    }

                    app.USER_ID = userProfile.Id;
                    app.USER_DATE = DateTime.Now;

                    //ตรวจสอบชื่อ
                    app.NAMES = GetByIndex(rawData, 3);
                    if (string.IsNullOrEmpty(app.NAMES))
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_007;
                    }

                    //ตรวจสอบนามสกุล
                    app.LASTNAME = GetByIndex(rawData, 4);
                    if (string.IsNullOrEmpty(app.LASTNAME))
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_008;
                    }

                    //ตรวจสอบวันเกิด
                    DateTime birthDate = DateTime.MinValue;
                    string strBirthDate = GetByIndex(rawData, 5);

                    if (!IsRightDate(strBirthDate))
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_031;
                    }
                    else
                    {
                        app.BIRTH_DATE = strBirthDate.String_dd_MM_yyyy_ToDate('/', true);

                    }



                    //ตรวจสอบเพศ
                    string _sex = "M";

                    if (app.TITLE.Contains("หญิง") || app.TITLE.Contains("นาง") || app.TITLE.Contains("นางสาว") ||
                       app.TITLE.Contains("แม่") || app.TITLE.Contains("พญ."))
                    {
                        _sex = "F";
                    }


                    string sex = GetByIndex(rawData, 6);
                    app.SEX = (sex == "ช" || sex == "M" ? "M" : "F");

                    //คำนำหน้าชื่อ กับ เพศ สัมพันธ์กันหรือไม่
                    if (string.IsNullOrEmpty(app.SEX))
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_009;
                    }
                    else
                    {
                        if (_sex != app.SEX)
                        {
                            app.ERROR_MSG += "\n" + Resources.errorApplicantService_010;
                        }
                    }

                    //ตรวจสอบวุฒิการศึกษา
                    app.EDUCATION_CODE = GetByIndex(rawData, 7).ToInt().ToString("00");

                    if (string.IsNullOrEmpty(app.EDUCATION_CODE))
                    {
                        app.ERROR_MSG += "\n" + Resources.errorApplicantService_011;
                    }
                    else
                    {
                        var eduEnt = lsEdu.Where(w => w.EDUCATION_CODE == app.EDUCATION_CODE).FirstOrDefault();
                        if (eduEnt == null)
                        {
                            app.ERROR_MSG += "\n" + Resources.errorApplicantService_012;
                        }
                    }




                    app.INSUR_COMP_CODE = GetByIndex(rawData, 8);





                    //ตรวจสอบรหัสบริษัท''

                    //  if (userProfile.MemberType == 2)
                    // {
                    //app.ERROR_MSG += "\nตัวแทนต้องระบุรหัสบริษัท";


                    //  }

                    if (!string.IsNullOrEmpty(app.INSUR_COMP_CODE))
                    {
                        var insurCompEnt = base.ctx.VW_IAS_COM_CODE.Where(w => w.ID == app.INSUR_COMP_CODE).FirstOrDefault();
                        if (insurCompEnt == null)
                        {
                            app.ERROR_MSG += "\n" + Resources.errorApplicantService_013;
                        }
                    }

                    app.EXAM_PLACE_CODE = examPlaceCode;
                    app.APPLY_DATE = DateTime.Now.Date;
                    app.TESTING_DATE = appHead.TESTING_DATE;

                    if (userProfile.MemberType == (int)DTO.RegistrationType.Insurance)
                    {
                        var chkLoad = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.TESTING_NO == testingNo && w.ID_CARD_NO == app.ID_CARD_NO && w.LOAD_STATUS == "C").FirstOrDefault();
                        //var head = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP.Where(w => w.UPLOAD_GROUP_NO == chkLoad.UPLOAD_GROUP_NO &&
                        //                                                        w.LICENSE_TYPE_CODE == licenseTypeCode &&
                        //                                                        w.TESTING_DATE == testingDate &&
                        //                                                        w.TEST_TIME_CODE == testTimeCode).FirstOrDefault();
                        if (chkLoad != null)
                        {
                            app.ERROR_MSG += "\n" + Resources.errorApplicantService_032;
                        }
                    }
                    else if (userProfile.MemberType == (int)DTO.RegistrationType.Association)
                    {
                        var chkLoad = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.ID_CARD_NO == app.ID_CARD_NO && w.LOAD_STATUS == "C" && w.EXAM_PLACE_CODE == app.EXAM_PLACE_CODE).FirstOrDefault();
                        if (chkLoad != null)
                        {
                            var head = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP.Where(w => w.UPLOAD_GROUP_NO == chkLoad.UPLOAD_GROUP_NO &&
                                                                                    w.LICENSE_TYPE_CODE == licenseTypeCode &&
                                                                                    w.TESTING_DATE == testingDate &&
                                                                                    w.TEST_TIME_CODE == testTimeCode).FirstOrDefault();
                            if (chkLoad != null && head != null)
                            {
                                app.ERROR_MSG += "\n" + Resources.errorApplicantService_032;
                            }
                        }

                    }

                    //this.ValidateApplicantTemp(app);
                    DAL.AG_IAS_APPLICANT_DETAIL_TEMP ent = new AG_IAS_APPLICANT_DETAIL_TEMP();

                    app.MappingToEntity(ent);
                    base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.AddObject(ent);
                    res.DataResponse.Detail.Add(app);
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }


                #endregion

                int total = res.DataResponse.Detail.Count();
                int missingTrans = res.DataResponse.Detail.Where(w => !string.IsNullOrEmpty(w.ERROR_MSG)).Count();
                int rightTrans = res.DataResponse.Detail.Where(w => string.IsNullOrEmpty(w.ERROR_MSG)).Count();

                res.DataResponse.Header.Add(new DTO.UploadHeader
                {
                    Totals = total,
                    MissingTrans = missingTrans,
                    RightTrans = rightTrans,
                    UploadFileName = fileName
                });

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertApplicantGroupUpload", ex);
            }

            return res;
        }



        /// <summary>
        /// ดึงข้อมูลรายการที่ Upload เข้า Temp แล้ว
        /// </summary>
        /// <param name="uploadGroupNo">เลขที่กลุ่ม Upload</param>
        /// <param name="seqNo">ลำดับที่ของข้อมูล</param>
        /// <returns>ข้อมูลที่ Upload เข้า Temp</returns>
        public DTO.ResponseService<DTO.ApplicantTemp>
            GetApplicantUploadTempById(string uploadGroupNo, string seqNo)
        {
            var res = new DTO.ResponseService<DTO.ApplicantTemp>();
            try
            {
                var head = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP
                                   .SingleOrDefault(s => s.UPLOAD_GROUP_NO == uploadGroupNo);

                var ent = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP
                                  .SingleOrDefault(s => s.UPLOAD_GROUP_NO == uploadGroupNo &&
                                                        s.SEQ_NO == seqNo);
                DTO.ApplicantTemp temp = new DTO.ApplicantTemp();
                if (ent != null)
                {
                    ent.MappingToEntity(temp);

                    if (head != null)
                        temp.TESTING_DATE = head.TESTING_DATE;
                }
                res.DataResponse = temp;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantUploadTempById", ex);
            }
            return res;
        }


        /// <summary>
        /// Update ข้อมูลที่แก้ไขแล้วเข้า Temp
        /// </summary>
        /// <param name="exam">Class ข้อมูล</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateApplicantGroupUpload(DTO.ApplicantTemp exam)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                this.ValidateApplicantTemp(exam);



                var ent = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP
                                  .SingleOrDefault(s => s.UPLOAD_GROUP_NO == exam.UPLOAD_GROUP_NO &&
                                                        s.SEQ_NO == exam.SEQ_NO);

                if (ent != null)
                {
                    exam.MappingToEntity(ent);
                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_UpdateApplicantGroupUpload", ex);
            }
            return res;
        }

        /// <summary>
        /// สำหรับดึงข้อมูลที่ Upload การสมัครสอบกลุ่ม
        /// </summary>
        /// <param name="groupUploadNo">เลขที่กลุ่ม Upload</param>
        /// <returns>ResponseService<UploadResult<UploadHeader, ApplicantTemp>></returns>
        public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>
            GetApplicantGroupUploadByGroupUploadNo(string groupUploadNo)
        {
            var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>();
            res.DataResponse = new DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>();

            res.DataResponse.Header = new List<DTO.UploadHeader>();
            res.DataResponse.Detail = new List<DTO.ApplicantTemp>();

            try
            {
                var entHead = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP
                                      .SingleOrDefault(s => s.UPLOAD_GROUP_NO == groupUploadNo);

                var details = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP
                                      .Where(w => w.UPLOAD_GROUP_NO == groupUploadNo)
                                      .ToList();

                foreach (var d in details)
                {
                    DTO.ApplicantTemp app = new DTO.ApplicantTemp();
                    d.MappingToEntity(app);
                    app.TESTING_DATE = entHead.TESTING_DATE;
                    res.DataResponse.Detail.Add(app);
                }

                int total = res.DataResponse.Detail.Count();
                int missingTrans = res.DataResponse.Detail.Where(w => !string.IsNullOrEmpty(w.ERROR_MSG)).Count();
                int rightTrans = res.DataResponse.Detail.Where(w => string.IsNullOrEmpty(w.ERROR_MSG)).Count();

                res.DataResponse.Header.Add(new DTO.UploadHeader
                {
                    Totals = total,
                    MissingTrans = missingTrans,
                    RightTrans = rightTrans,
                    UploadFileName = entHead.FILENAME
                });

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantGroupUploadByGroupUploadNo", ex);
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

                    objCmd.CommandText = "AG_IAS_GEN_EXAM_NO";

                    objCmd.CommandType = CommandType.StoredProcedure;

                    objCmd.Parameters.Add("BATCH_ID", OracleDbType.Varchar2).Value = batchId;

                    var TestingNo = new OracleParameter("TEST_NO", OracleDbType.Varchar2, ParameterDirection.Output);
                    TestingNo.Size = 4000;
                    TestingNo.Value = "";
                    objCmd.Parameters.Add(TestingNo);

                    var errFlag = new OracleParameter("ERR_FLG", OracleDbType.Varchar2, ParameterDirection.InputOutput);
                    errFlag.Value = "N";
                    objCmd.Parameters.Add(errFlag);


                    var errMess = new OracleParameter("ERR_MESS", OracleDbType.Varchar2, ParameterDirection.Output);
                    errMess.Size = 4000;
                    errMess.Value = "";
                    objCmd.Parameters.Add(errMess);

                    objConn.Open();
                    objCmd.ExecuteNonQuery();
                    objConn.Close();

                    errorMessage = errMess.Value.ToString();

                    if (errFlag.Value.ToString() != "Y")
                    {
                        _testingNo = TestingNo.Value.ToString();

                        if (string.IsNullOrEmpty(_testingNo))
                        {
                            throw new ArgumentException(Resources.errorApplicantService_033);
                        }
                    }
                    else
                    {
                        throw new ArgumentException(Resources.errorApplicantService_033);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
            return _testingNo;
        }


        /// <summary>
        /// ส่งข้อมูลการสมัครสอบแบบกลุ่ม
        /// </summary>
        /// <param name="groupId">รหัสกลุ่มข้อมูล</param>
        /// <returns>ResponseService<string></returns>
        public DTO.ResponseService<string> ApplicantGroupUploadToSubmit(string groupId, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseService<string>();
            try
            {
                //#region แจ้งใบซ้ำ

                //var strDetail = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP
                //                     .Where(w => w.UPLOAD_GROUP_NO == groupId)
                //                     .ToList();
                //if (strDetail != null)
                //{
                //    res.ErrorMsg = "ไฟล์ข้อมูลซ้ำ";
                //    return res;
                //}

                //#endregion
                var details = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP
                                      .Where(w => w.UPLOAD_GROUP_NO == groupId)
                                      .ToList();

                Boolean DupError = false;
                foreach (AG_IAS_APPLICANT_DETAIL_TEMP d in details)
                {
                    var chkLoad = base.ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.TESTING_NO == d.TESTING_NO && w.ID_CARD_NO == d.ID_CARD_NO && w.LOAD_STATUS == "C").FirstOrDefault();
                    if (chkLoad != null)
                    {
                        if (!string.IsNullOrEmpty(d.ERROR_MSG))
                        {
                            if (!d.ERROR_MSG.Contains(Resources.errorApplicantService_032))
                            {
                                d.ERROR_MSG += "\n" + Resources.errorApplicantService_032;
                                DupError = true;
                            }
                        }
                    }
                }

                if (DupError)
                {
                    using (var ts = new TransactionScope())
                    {
                        base.ctx.SaveChanges();
                        ts.Complete();
                    }
                }

                int hasError = details.Where(delegate(AG_IAS_APPLICANT_DETAIL_TEMP temp)
                {
                    return !string.IsNullOrEmpty(temp.ERROR_MSG);
                })
                                       .Count();

                if (hasError > 0)
                {
                    res.ErrorMsg = Resources.errorApplicantService_034;
                    return res;
                }

                /////
                var header = base.ctx.AG_IAS_APPLICANT_HEADER_TEMP
                                     .Where(w => w.UPLOAD_GROUP_NO == groupId)
                                     .FirstOrDefault();

                var detail = ctx.AG_IAS_APPLICANT_DETAIL_TEMP.Where(w => w.UPLOAD_GROUP_NO == groupId).FirstOrDefault();

                //Update By Fuse 05/06/2014
                //#region Update By Fuse 05/06/2014
                //var examLicense = base.ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == testingNo).FirstOrDefault();
                //string examPlaceCode = examLicense.EXAM_PLACE_CODE;
                //string testTimeCode = examLicense.TEST_TIME_CODE;
                //string licenseTypeCode = examLicense.LICENSE_TYPE_CODE;
                //DateTime testingDate = examLicense.TESTING_DATE.Value;

                //#endregion

                #region ตรวจสอบวันที่สอบ

                //

                #region ปิดไปก่อน
                string examPlaceCode = detail.EXAM_PLACE_CODE;
                string testTimeCode = header.TEST_TIME_CODE;
                string licenseTypeCode = header.LICENSE_TYPE_CODE.Trim().Length == 1
                                                ? "0" + header.LICENSE_TYPE_CODE.Trim()
                                                : header.LICENSE_TYPE_CODE.Trim();
                DateTime testingDate = header.TESTING_DATE.Value;

                var examLicense = base.ctx.AG_EXAM_LICENSE_R
                                          .Where(delegate(AG_EXAM_LICENSE_R lr)
                                          {
                                              return lr.EXAM_PLACE_CODE == examPlaceCode &&
                                                     lr.TEST_TIME_CODE == testTimeCode &&
                                                     lr.LICENSE_TYPE_CODE == licenseTypeCode &&
                                                     lr.TESTING_DATE == testingDate;
                                          })
                                          .FirstOrDefault();
                #endregion
                #endregion

                #region สำหรับสร้างรอบการสอบอัตโนมัติ ใช้กับสมาคมเท่านั้น ตอนนี้ยังไม่เปิดให้ใช้งาน


                Boolean flagAssoc = false;
                string testingNoAsso = String.Empty;
                ////กรณีไม่พบรายการสอบ
                if (examLicense == null)
                {
                    //ถ้าเป็นสมาคมให้สร้างการสอบอัตโนมัติ
                    if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        flagAssoc = true;
                        string errMsg = string.Empty;
                        testingNoAsso = GetTestingNo(groupId, out errMsg);

                        if (!string.IsNullOrEmpty(errMsg) && errMsg != "null")
                        {
                            res.ErrorMsg = errMsg;
                            return res;
                        }
                    }

                }
                else
                {
                    if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        testingNoAsso = examLicense.TESTING_NO;
                    }
                }
                #endregion
        #endregion

                var entHeader = new AG_IAS_APPLICANT_HEADER_T();

                header.MappingToEntity(entHeader);

                base.ctx.AG_IAS_APPLICANT_HEADER_T
                        .AddObject(entHeader);

                //เตรียมข้อมูล EXAM_APPLY
                int startApply = 0;
                int curApply = 0;
                string strTesingNoByCondition = string.Empty;
                if (userProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    strTesingNoByCondition = details[0].TESTING_NO;
                }
                else if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    strTesingNoByCondition = testingNoAsso;
                }
                else if (userProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || userProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    strTesingNoByCondition = details[0].TESTING_NO;
                }

                if (details.Count > 0)
                {
                    var examLic = base.ctx.AG_EXAM_LICENSE_R
                                          .Where(delegate(AG_EXAM_LICENSE_R lr)
                                          {
                                              return lr.TESTING_NO == details[0].TESTING_NO &&
                                                         lr.EXAM_PLACE_CODE == examPlaceCode;
                                          })
                                          .FirstOrDefault();

                    if (examLic != null)
                    {

                        startApply = examLic.EXAM_APPLY == null
                                              ? 0
                                              : examLic.EXAM_APPLY.Value.ToInt();

                        curApply = startApply + details.Count;

                        examLic.EXAM_APPLY = curApply.ToString().ToShort();
                    }
                }

                int rowAffected = 0;

                int applicantCounts = 0;
                if (!flagAssoc)
                {
                    string strTestingNo = strTesingNoByCondition;

                    try
                    {
                        var ls = base.ctx.AG_APPLICANT_T.Where(w => w.EXAM_PLACE_CODE == examPlaceCode && w.TESTING_NO == strTesingNoByCondition);
                        if (ls == null || ls.Count() <= 0)
                        {
                            applicantCounts = 0;
                        }
                        else
                        {
                            applicantCounts = ls.Max(a => a.APPLICANT_CODE);
                        }

                    }
                    catch (Exception ex)
                    {

                        applicantCounts = 0;
                    }

                }

                foreach (AG_IAS_APPLICANT_DETAIL_TEMP d in details)
                {
                    rowAffected += 1;
                    if (userProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        d.TESTING_NO = d.TESTING_NO == null || d.TESTING_NO == "" ? testingNoAsso : d.TESTING_NO;
                    }

                    AG_APPLICANT_T ent = new AG_APPLICANT_T()
                    {
                        //HEAD_REQUEST_NO =
                        //GROUP_REQUEST_NO = 
                        UPLOAD_GROUP_NO = d.UPLOAD_GROUP_NO,
                        UPLOAD_BY_SESSION = userProfile.CompCode,
                        //ID_ATTACH_FILE = 
                        APPLICANT_CODE = applicantCounts + rowAffected,
                        TESTING_NO = d.TESTING_NO == null || d.TESTING_NO == "" ? examLicense.TESTING_NO : d.TESTING_NO,
                        EXAM_PLACE_CODE = d.EXAM_PLACE_CODE,
                        ACCEPT_OFF_CODE = d.ACCEPT_OFF_CODE,
                        APPLY_DATE = d.APPLY_DATE,
                        ID_CARD_NO = d.ID_CARD_NO,
                        PRE_NAME_CODE = d.PRE_NAME_CODE,
                        NAMES = d.NAMES,
                        LASTNAME = d.LASTNAME,
                        BIRTH_DATE = d.BIRTH_DATE,
                        SEX = d.SEX,
                        EDUCATION_CODE = d.EDUCATION_CODE,
                        ADDRESS1 = d.ADDRESS1,
                        ADDRESS2 = d.ADDRESS2,
                        AREA_CODE = d.AREA_CODE,
                        PROVINCE_CODE = d.PROVINCE_CODE,
                        ZIPCODE = d.ZIPCODE,
                        TELEPHONE = d.TELEPHONE,
                        AMOUNT_TRAN_NO = d.AMOUNT_TRAN_NO,
                        PAYMENT_NO = d.PAYMENT_NO,
                        INSUR_COMP_CODE = d.INSUR_COMP_CODE,
                        ABSENT_EXAM = d.ABSENT_EXAM,
                        RESULT = d.RESULT,
                        EXPIRE_DATE = d.EXPIRE_DATE,
                        LICENSE = d.LICENSE,
                        CANCEL_REASON = d.CANCEL_REASON,
                        RECORD_STATUS = d.RECORD_STATUS,
                        USER_ID = d.USER_ID,
                        USER_DATE = d.USER_DATE,
                        EXAM_STATUS = d.EXAM_STATUS
                    };
                    base.ctx.AG_APPLICANT_T.AddObject(ent);
                    d.LOAD_STATUS = "C";
                    d.TESTING_NO = d.TESTING_NO == null ? examLicense.TESTING_NO : d.TESTING_NO;

                }


                using (var ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                res.DataResponse = string.Format("บันทึกข้อมูลเรียบร้อย จำนวน {0} รายการ", rowAffected);


            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ApplicantGroupUploadToSubmit", ex);
            }
            return res;
        }

        private DTO.UploadData ReadFileUpload(String fileData)  
        {
           DownloadFileResponse res =   FileManagerService.RemoteFileCommand(new DownloadFileRequest() { TargetContainer = "", TargetFileName = fileData }).Action();

           if (res.Code != "0000") {
               LoggerFactory.CreateLog().LogError("ไม่พบไฟล์ที่ระบุ ." + fileData);
               throw new ApplicationException("ไม่พบไฟล์ที่ระบุ .");
           }


           Stream rawData = res.FileByteStream;

            DTO.UploadData data = new DTO.UploadData
            {
                Body = new List<string>()
            };
            //Stream rawData = FileUpload1.PostedFile.InputStream;
            using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
            {

                string line = sr.ReadLine();
                if (line != null && line.Length > 0)
                {


                    if (line.Substring(0, 1).ToUpper() != "H")
                    {
                        line = sr.ReadLine();
                    }
                    if (line.Substring(0, 1) == "H")
                    {
                        data.Header = line;
                    }
                    else
                    {
                        throw new ApplicationException("บรรทัดแรก ตำแหน่งแรก ต้องเป็น Header เท่านั้น!");
                    }

                }
                else
                {
                    throw new ApplicationException("ไม่พบข้อมูลภายในไฟล์นำเข้า");
                }
                bool IsBlankRecord = false;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Trim().Length > 0)
                    {
                        int iParse = 0;
                        if (Int32.TryParse((line.Split(',')[0]), out iParse))
                        {
                            data.Body.Add(line.Trim());
                            IsBlankRecord = false;
                        }
                        else
                        {
                            if (IsBlankRecord)
                            {
                                break;
                            }
                            else
                            {
                                IsBlankRecord = true;
                            }
                        }

                    }

                }


            }

            return data;
        }


        /// <summary>
        /// เพิ่มข้อมูลผู้สมัครสอบแบบเดี่ยว
        /// </summary>
        /// <param name="app">ข้อมูลการสมัครสอบ</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseService<string> InsertSingleApplicant(List<DTO.ApplicantTemp> app, string userId)
        {
            var res = new DTO.ResponseService<string>();
            string groupHeaderNo = string.Empty;
            //  var lsSubPayment = new List<DTO.lsGroupRequestNo>();
            var lsSubPayment = new List<string>();
            var paySrv = new IAS.DataServices.Payment.PaymentService();

            var payments = new List<DTO.OrderInvoice>();
            var payments2 = new List<DTO.OrderInvoice>();

            try
            {
                var dtTestingDTCompare = app.Where(x => x.TESTING_DATE != null &&
                    (DateTime.Compare(DateTime.Parse(String.Format("{0:dd/MM/yyy}", x.TESTING_DATE).ToString()), DateTime.Parse(String.Format("{0:dd/MM/yyy}", DateTime.Now).ToString())) == -1)).ToList();
                //TESTING_DATE less then DateTime.Now
                if (dtTestingDTCompare.Count > 0)
                {
                    res.ErrorMsg = Resources.errorApplicantService_020;
                    return res;
                }

                //Get ID_CARD_NO
                IEnumerable<string> appIdCard = app.Select(idc => idc.ID_CARD_NO);
                IEnumerable<string> appTestingNo = app.Select(tsn => tsn.TESTING_NO);
                IEnumerable<string> appExamPlaceCode = app.Select(exn => exn.EXAM_PLACE_CODE);


                #region ValidateApplicantTempBeforSubmit

                //ตรวจสอบว่ามีรายการสอบที่ผู้สมัครสอบส่งมาหรือไม่
                IQueryable<AG_EXAM_LICENSE_R> chkexam = base.ctx.AG_EXAM_LICENSE_R.Where(w => appTestingNo.Contains(w.TESTING_NO) && appExamPlaceCode.Contains(w.EXAM_PLACE_CODE));
                if (chkexam.ToList().Count == 0)
                {
                    res.ErrorMsg += "\n" + Resources.errorApplicantService_002 + chkexam.Select(t => t.TESTING_NO);
                    return res;
                }
                #endregion


                //Check App List
                app.ForEach(x =>
                {
                    int curApply = 0;
                    int curApplicantCode = 0;
                    int lsApp = 0;
                    string accept_off_code = string.Empty;

                    AG_EXAM_LICENSE_R exam = base.ctx.AG_EXAM_LICENSE_R.FirstOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE);

                    var lsApplicant = base.ctx.AG_APPLICANT_T.Where(w => w.TESTING_NO == x.TESTING_NO && w.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE).ToList();
                    if (lsApplicant.Count > 0)
                    {
                        lsApp = lsApplicant.Max(w => w.APPLICANT_CODE);
                    }
                    else
                    {
                        lsApp = 0;
                    }
                    //curApply = lsApp.Count();
                    if (exam != null)
                    {
                        curApply = exam.EXAM_APPLY == null ? 0 : exam.EXAM_APPLY.Value.ToInt();
                        exam.EXAM_APPLY = (++curApply).ToString().ToShort();
                        base.ctx.SaveChanges();

                        //หารหัสกลุ่มสนามสอบ
                        var examPlaceCode = base.ctx.AG_EXAM_PLACE_R.SingleOrDefault(w => w.EXAM_PLACE_CODE == exam.EXAM_PLACE_CODE);
                        if (examPlaceCode != null)
                        {
                            accept_off_code = examPlaceCode.EXAM_PLACE_GROUP_CODE;
                        }
                    }

                    AG_IAS_PERSONAL_T person = base.ctx.AG_IAS_PERSONAL_T.FirstOrDefault(s => s.ID == userId);

                    AG_APPLICANT_T ent = new AG_APPLICANT_T();

                    app.MappingToEntity(ent);

                    curApplicantCode = lsApp + 1;
                    ent.APPLICANT_CODE = curApplicantCode;

                    //ent.APPLICANT_CODE = curApply;
                    var accp = base.ctx.AG_ACCEPT_OFF_R.SingleOrDefault(w => w.ACCEPT_OFF_CODE == accept_off_code);
                    if (accp != null)
                    {
                        ent.ACCEPT_OFF_CODE = accept_off_code;
                    }
                    else
                    {
                        ent.ACCEPT_OFF_CODE = null;
                    }

                    ent.TESTING_NO = x.TESTING_NO;
                    if (person != null)
                    {
                        ent.ID_CARD_NO = person.ID_CARD_NO;
                        ent.PRE_NAME_CODE = person.PRE_NAME_CODE;
                        ent.NAMES = person.NAMES;
                        ent.LASTNAME = person.LASTNAME;
                        ent.BIRTH_DATE = person.BIRTH_DATE;
                        if (person.SEX == "M" || person.SEX == "F")
                        {
                            ent.SEX = person.SEX;
                        }
                        else
                        {
                            ent.SEX = person.SEX == "ช" ? "M" : "F";
                        }
                        if (!string.IsNullOrEmpty(x.INSUR_COMP_CODE))
                        {
                            ent.INSUR_COMP_CODE = x.INSUR_COMP_CODE;
                        }
                        ent.EXAM_PLACE_CODE = exam.EXAM_PLACE_CODE;
                        ent.EDUCATION_CODE = person.EDUCATION_CODE;
                        ent.ADDRESS1 = person.ADDRESS_1;
                        ent.AREA_CODE = person.PROVINCE_CODE + person.AREA_CODE + person.TUMBON_CODE;
                        ent.ZIPCODE = person.ZIP_CODE;
                        ent.TELEPHONE = person.TELEPHONE;
                        ent.USER_ID = userId;
                        ent.USER_DATE = DateTime.Now;
                        ent.EXAM_STATUS = (exam != null ? exam.EXAM_STATUS : string.Empty);
                        ent.APPLY_DATE = x.APPLY_DATE;
                        string strPersonIDCard = x.ID_CARD_NO;


                        //หาการสมัครสอบของคนๆนี่
                        DateTime dtToday = DateTime.Now.Date;
                        var appEx = from a in ctx.AG_APPLICANT_T
                                    join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                    where e.TESTING_DATE == exam.TESTING_DATE && a.APPLY_DATE == dtToday
                                     && a.ID_CARD_NO == x.ID_CARD_NO
                                    select a;

                        AG_APPLICANT_T personExam = new AG_APPLICANT_T();

                        if (appEx != null)
                        {
                            personExam = base.ctx.AG_APPLICANT_T.FirstOrDefault(w => w.APPLICANT_CODE == x.APPLICANT_CODE && w.TESTING_NO == x.TESTING_NO && w.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE);
                        }



                        /*
                        var examLicense = base.ctx.AG_EXAM_LICENSE_R.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE);

                        var ent1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.APPLY_DATE != x.APPLY_DATE && w.ID_CARD_NO == x.ID_CARD_NO);

                        var ent2 = from a in ctx.AG_APPLICANT_T
                                   join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                   where e.TESTING_DATE == x.TESTING_DATE && e.TEST_TIME_CODE == examLicense.TEST_TIME_CODE && a.APPLY_DATE != x.APPLY_DATE
                                   && a.ID_CARD_NO == x.ID_CARD_NO && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                                   select a;

                        var ent3 = from a in ctx.AG_APPLICANT_T
                                   join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                   where e.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE && e.TEST_TIME_CODE == examLicense.TEST_TIME_CODE && a.APPLY_DATE != x.APPLY_DATE
                                      && a.ID_CARD_NO == x.ID_CARD_NO && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                                   select a;


                        var ent4 = from a in ctx.AG_APPLICANT_T
                                   join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                   where e.TESTING_DATE == x.TESTING_DATE && a.APPLY_DATE == dtToday
                                      && a.ID_CARD_NO == x.ID_CARD_NO
                                   select a;


                        var ent5 = from a in ctx.AG_APPLICANT_T
                                   join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                   where e.TESTING_DATE == x.TESTING_DATE && a.APPLY_DATE != x.TESTING_DATE
                                      && a.ID_CARD_NO == x.ID_CARD_NO
                                   select a;



                        if (personExam != null || ent1 != null || ent2 != null || ent3 != null || ent4 != null || ent5 != null)
                        {
                            ent.APPLY_DATE = DateTime.Now.Date;
                            ent.RECORD_STATUS = "D";
                        }
                    */

                        DateTime applyDate = DateTime.Now.Date;
                        //รอบสอบเดียวกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน
                        AG_APPLICANT_T confirm1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.APPLY_DATE != x.APPLY_DATE && w.ID_CARD_NO == x.ID_CARD_NO);
                        if (confirm1 != null)
                        {
                            ent.APPLY_DATE = DateTime.Now.Date;
                            ent.RECORD_STATUS = "D";
                        }

                        //วันที่สอบเดียวกัน เวลาเดียวกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน
                        IEnumerable<AG_APPLICANT_T> confirm2 = from a in ctx.AG_APPLICANT_T
                                                               join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                               where e.TESTING_DATE == x.TESTING_DATE && e.TEST_TIME_CODE == x.TEST_TIME_CODE
                                                               && a.APPLY_DATE != applyDate
                                                               && a.ID_CARD_NO == x.ID_CARD_NO
                                                               select a;

                        //วันที่สอบเดียวกัน เวลาเดียวกัน สนามสอบเดียวกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน
                        IEnumerable<AG_APPLICANT_T> confirm3 = from a in ctx.AG_APPLICANT_T
                                                               join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                               where e.TESTING_DATE == x.TESTING_DATE
                                                               && e.TEST_TIME_CODE == x.TEST_TIME_CODE
                                                               && e.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE
                                                               && a.ID_CARD_NO == x.ID_CARD_NO
                                                               && a.APPLY_DATE != applyDate
                                                               select a;
                        if (confirm2.Count() > 0 || confirm3.Count() > 0)
                        {
                            ent.APPLY_DATE = DateTime.Now.Date;
                            ent.RECORD_STATUS = "D";
                        }

                        //วันที่สอบเดียวกัน เวลาคร่อมกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน

                        //หาช่วงเวลาที่จะสอบ
                        AG_EXAM_TIME_R examTime = base.ctx.AG_EXAM_TIME_R.SingleOrDefault(w => w.TEST_TIME_CODE == x.TEST_TIME_CODE && w.ACTIVE == "Y");

                        if (examTime != null)
                        {
                            decimal eStartTime2 = 0;
                            decimal eEndTime2 = 0;

                            if (examTime.START_TIME != null)
                            {
                                eStartTime2 = Convert.ToDecimal(examTime.START_TIME);
                            }
                            if (examTime.END_TIME != null)
                            {
                                eEndTime2 = Convert.ToDecimal(examTime.END_TIME);
                            }

                            //หาข้อมูลเวลาทั้งหมดใส่ลง List เพื่อเทียบกับเวลาที่จะสอบ
                            IEnumerable<AG_EXAM_TIME_R> lsTime = from etr in ctx.AG_EXAM_TIME_R
                                                                 join el in ctx.AG_EXAM_LICENSE_R on etr.TEST_TIME_CODE equals el.TEST_TIME_CODE
                                                                 join pp in ctx.AG_APPLICANT_T on el.TESTING_NO equals pp.TESTING_NO
                                                                 where pp.ID_CARD_NO == x.ID_CARD_NO && el.TESTING_DATE == x.TESTING_DATE
                                                                 select etr;

                            List<ExamTime> lsExamTime = new List<ExamTime>();
                            if (lsTime != null)
                            {

                                lsTime.ToList().ForEach(
                                    z =>
                                    {
                                        ExamTime exam2 = new ExamTime();
                                        exam2.TEST_TIME_CODE = z.TEST_TIME_CODE;
                                        exam2.START_TIME = Convert.ToDecimal(z.START_TIME);
                                        exam2.END_TIME = Convert.ToDecimal(z.END_TIME);
                                        lsExamTime.Add(exam2);
                                    });

                            }

                            //ตรวจสอบข้อมูลทั้งหมดใน List ว่า มีคร่อมเวลาหรือไม่
                            if (lsExamTime.ToList().Count > 0)
                            {
                                IEnumerable<ExamTime> acrossTimeCase1 = lsExamTime.Where(w => w.START_TIME <= eStartTime2 && w.END_TIME <= eEndTime2 && w.END_TIME >= eStartTime2);
                                IEnumerable<ExamTime> acrossTimeCase2 = lsExamTime.Where(w => w.START_TIME >= eStartTime2 && w.END_TIME >= eEndTime2 && w.START_TIME <= eEndTime2);
                                IEnumerable<ExamTime> acrossTimeCase3 = lsExamTime.Where(w => w.START_TIME >= eStartTime2 && w.END_TIME <= eEndTime2);
                                IEnumerable<ExamTime> acrossTimeCase4 = lsExamTime.Where(w => w.START_TIME <= eStartTime2 && w.END_TIME >= eEndTime2);
                                IEnumerable<ExamTime> acrossTimeCase5 = lsExamTime.Where(w => w.START_TIME == eStartTime2 && w.END_TIME == eEndTime2);
                                if (acrossTimeCase1.ToList().Count > 0 ||
                                    acrossTimeCase2.ToList().Count > 0 ||
                                    acrossTimeCase3.ToList().Count > 0 ||
                                    acrossTimeCase4.ToList().Count > 0 ||
                                    acrossTimeCase5.ToList().Count > 0)
                                {
                                    ent.APPLY_DATE = DateTime.Now.Date;
                                    ent.RECORD_STATUS = "D";
                                }
                            }
                        }




                    }

                    base.ctx.AG_APPLICANT_T.AddObject(ent);
                    base.ctx.SaveChanges();

                    payments.Add(new DTO.OrderInvoice
                    {
                        ApplicantCode = ent.APPLICANT_CODE,
                        EXAM_PLACE_CODE = ent.EXAM_PLACE_CODE,
                        TESTING_NO = ent.TESTING_NO,
                        PaymentType = "01",
                        RUN_NO = x.RUN_NO,
                        comcode = ent.INSUR_COMP_CODE
                    });

                });

                //Begin New Tracsaction
                var resSubGroup = paySrv.SetSubGroupSingleApplicant(payments, userId, out groupHeaderNo);

                if (resSubGroup.IsError)
                {
                    res.ErrorMsg = resSubGroup.ErrorMsg;
                }
                else
                {
                    res.DataResponse = groupHeaderNo;
                }            
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }


        private bool GenSinglePayment(List<string> payments)
        {
            //var biz = new BLL.PaymentBiz();
            //var res = biz.GenPaymentCode();

            //if (res != null)
            //{
            //    clearControl();

            //    lblPaymentAssimilateNumberDetail.Text = res;

            //    mpEdit.Show();

            //    UplPopUp.Update();
            //}
            return true;
        }

        private string GetCriteria(string criteria, string value)
        {
            return !string.IsNullOrEmpty(value)
                        ? string.Format(criteria, value)
                        : string.Empty;
        }

        //สำหรับ บุคคลทั่วไป, บริษัท, สมาคม, คปภ.
        public DTO.ResponseService<DataSet> GetApplicantByCriteria(DTO.RegistrationType userRegType, string compCode,
                                                                   string idCard, string testingNo,
                                                                   string firstName, string lastName,
                                                                   DateTime? startDate, DateTime? toDate,
                                                                   string paymentNo, string billNo,
                                                                   int pageNo, int recordPerPage, Boolean Count, string license, string time, string examPlaceGroupCode, string examPlaceCode, string chequeNo, string examResult, DateTime? startCandidates, DateTime? endCandidates)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();


            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO like '{0}%' AND ", idCard));
                sb.Append(GetCriteria("AP.TESTING_NO like '{0}%' AND ", testingNo));
                sb.Append(GetCriteria("AP.NAMES LIKE '{0}%' AND ", firstName));
                sb.Append(GetCriteria("AP.LASTNAME LIKE '{0}%' AND ", lastName));
                sb.Append(GetCriteria("LR.TEST_TIME_CODE = '{0}' AND ", time));
                sb.Append(GetCriteria("LR.LICENSE_TYPE_CODE = '{0}' AND ", license));
                sb.Append(GetCriteria("PG.EXAM_PLACE_GROUP_CODE = '{0}' AND ", examPlaceGroupCode));
                sb.Append(GetCriteria("PL.EXAM_PLACE_CODE = '{0}' AND ", examPlaceCode));
                sb.Append(GetCriteria("SHT.GROUP_REQUEST_NO = '{0}' AND ", chequeNo));
                sb.Append(GetCriteria("SRT.RECEIPT_NO = '{0}' AND ", billNo));
                if (examResult == "null" || examResult == null)
                {
                    sb.Append(GetCriteria("AP.RESULT is {0} AND ", "null"));
                }
                else if (examResult == "all")
                {
                     sb.Append(GetCriteria(" ",null));
                }
                else
                {
                    sb.Append(GetCriteria("AP.RESULT = '{0}' AND ", examResult));
                }


                string findEmail = string.Empty;
                string sql_export = string.Empty;
                if (userRegType != DTO.RegistrationType.General)
                {
                    if (startDate != null && toDate != null)
                    {
                        sb.Append("(TRUNC(LR.TESTING_DATE) BETWEEN TO_DATE('" +
                                        Convert.ToDateTime(startDate).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                        Convert.ToDateTime(toDate).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                    }
                    if (startCandidates != null && endCandidates != null)
                    {
                        sb.Append("(TRUNC(AP.APPLY_DATE) BETWEEN TO_DATE('" +
                                        Convert.ToDateTime(startCandidates).ToString_yyyyMMdd() + "','yyyymmdd') AND TO_DATE('" +
                                        Convert.ToDateTime(endCandidates).ToString_yyyyMMdd() + "','yyyymmdd')) AND ");
                    }
                }
                // Call by เจ้าหน้าที่สมาคม
                if (userRegType == DTO.RegistrationType.Association)
                {
                    sb.Append(GetCriteria("( LR.EXAM_OWNER = '" + compCode + "' or AP.UPLOAD_BY_SESSION = '{0}' ) AND ", compCode));
                }
                //Call by เจ้าหน้าที่บริษัท
                else if (userRegType == DTO.RegistrationType.Insurance)
                {

                    sb.Append(GetCriteria("AP.INSUR_COMP_CODE = '{0}' AND ", compCode));
                }
                else if (userRegType == DTO.RegistrationType.OIC)
                {

                }
                else if (userRegType == DTO.RegistrationType.General)
                {
                    findEmail = "and APT.ID = '" + compCode + "' ";
                    if (string.IsNullOrEmpty(idCard))
                    {
                        res.ErrorMsg = "No IdCard.";
                        return res;
                    }
                }

                string tmp = sb.ToString();

                string crit = tmp.Length > 4
                                ? " where " + tmp.Substring(0, tmp.Length - 4)
                                : tmp;

                string firstcon = string.Empty;
                string critRecNo = string.Empty;
                string midtxt = string.Empty;
                string EXport_sql = string.Empty;
                if (!Count)
                {

                    firstcon = "select * from  ";
                    midtxt = " ,ROW_NUMBER() OVER (ORDER BY AP.TESTING_NO) RUN_NO    ";
                    critRecNo = pageNo == 0
                                   ? ""
                                   : "  order by AP.TESTING_NO asc, AP.NAMES asc,AP.LASTNAME asc ) A  WHERE A.RUN_NO BETWEEN " +
                                            pageNo.StartRowNumber(recordPerPage).ToString() + " AND " +

                                            pageNo.ToRowNumber(recordPerPage).ToString() + " order by A.RUN_NO asc ";
                    sql_export = "TT.NAME,AP.NAMES,AP.BIRTH_DATE,AP.SEX,AP.EDUCATION_CODE,AP.ADDRESS1,AP.AREA_CODE,AP.UPLOAD_BY_SESSION,AP.INSUR_COMP_CODE,LR.TEST_TIME_CODE,";
                    EXport_sql = ", NAME, NAMES, BIRTH_DATE, SEX, EDUCATION_CODE, ADDRESS1, AREA_CODE, UPLOAD_BY_SESSION, INSUR_COMP_CODE,HEAD_REQUEST_NO,TEST_TIME_CODE,APPLY_DATE";//,TELEPHONE,EMAIL ";
                }
                else
                {
                    critRecNo = " )";
                    firstcon = "select COUNT(*) CCount from ";
                    midtxt = "    ";
                    sql_export = " ";
                }

                string sql = firstcon +
                              "  (SELECT distinct( SHT.GROUP_REQUEST_NO) PAYMENT_NO, AP.ID_CARD_NO, AP.CANCEL_REASON, PL.PROVINCE_CODE , LR.LICENSE_TYPE_CODE ,LTR.LICENSE_TYPE_NAME, PG.EXAM_PLACE_GROUP_CODE,  AP.NAMES FIRSTNAME, AP.LASTNAME, AP.TESTING_NO, " + sql_export +
                             "       LR.TESTING_DATE ,SHT.HEAD_REQUEST_NO , case AP.RESULT when 'P' then 'ผ่าน'  when 'F' then 'ไม่ผ่าน'  when 'B' then 'แบล๊กลิสต์' else  (case ap.absent_exam when 'M' then 'ขาดสอบ' else 'รอผลการสอบ' end) end RESULT, SRT.RECEIPT_NO BillNo " +
                             "       ,AP.APPLICANT_CODE, AP.EXAM_PLACE_CODE, SDT.PAYMENT_DATE PAYMENT_DATE,AER.TEST_TIME,SHT.STATUS,GT.EXPIRATION_DATE,	AP.APPLY_DATE" + midtxt + //,AP.TELEPHONE,APT.EMAIL " 
                             " FROM  AG_APPLICANT_T AP LEFT OUTER JOIN AG_IAS_PERSONAL_T APT ON AP.ID_CARD_NO = APT.ID_CARD_NO  and APT.MEMBER_TYPE='1' " + findEmail +
                             " LEFT OUTER JOIN VW_IAS_TITLE_NAME TT ON  AP.PRE_NAME_CODE = TT.ID " +
                             " LEFT OUTER JOIN AG_EXAM_LICENSE_R LR ON AP.TESTING_NO = LR.TESTING_NO AND AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE " +
                             " LEFT OUTER JOIN AG_LICENSE_TYPE_R LTR ON LR.LICENSE_TYPE_CODE = LTR.LICENSE_TYPE_CODE " +
                             " LEFT OUTER JOIN AG_IAS_SUBPAYMENT_D_T SDT ON SDT.ID_CARD_NO = AP.ID_CARD_NO and SDT.TESTING_NO = AP.TESTING_NO and ap.exam_place_code = sdt.exam_place_code  and AP.APPLICANT_CODE = SDT.APPLICANT_CODE " +
                             "  LEFT OUTER JOIN (SELECT * from AG_IAS_SUBPAYMENT_H_T SHT where SHT.STATUS != 'X' OR SHT.STATUS is null) SHT ON SHT.HEAD_REQUEST_NO = SDT.HEAD_REQUEST_NO " + // and SHT.STATUS!= 'X'" + //เพิ่มกรณี ไม่เอาคนที่ยกเลิกใบสั่งจ่ายมาออกในตาราง
                             " LEFT OUTER JOIN AG_IAS_PAYMENT_G_T GT ON GT.GROUP_REQUEST_NO = SHT.GROUP_REQUEST_NO " +
                             " LEFT OUTER JOIN AG_EXAM_TIME_R AER ON AER.TEST_TIME_CODE = LR.TEST_TIME_CODE " +
                             " LEFT OUTER JOIN AG_EXAM_PLACE_R PL on LR.EXAM_PLACE_CODE = PL.EXAM_PLACE_CODE " +
                              " LEFT OUTER JOIN AG_IAS_SUBPAYMENT_RECEIPT SRT on SRT.PAYMENT_NO = SDT.PAYMENT_NO and SRT.HEAD_REQUEST_NO = SDT.HEAD_REQUEST_NO " +
                             "    LEFT OUTER JOIN AG_EXAM_PLACE_GROUP_R PG ON PL.EXAM_PLACE_GROUP_CODE = PG.EXAM_PLACE_GROUP_CODE " + crit;
           



                sql = sql + " " + critRecNo;

                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantByCriteria", ex);
            }

            return res;

        }

        public DTO.ResponseService<string> GetApplicantByCriteriaSendMail(DTO.RegistrationType userRegType, string compCode,
                                                            string idCard, string testingNo,
                                                            string firstName, string lastName,
                                                            DateTime? startDate, DateTime? toDate,
                                                            string paymentNo, string billNo,
                                                            int RowPerPage, int pageNum, Boolean Count, string license, string time, string examPlaceGroupCode, string examPlaceCode, string chequeNo, string examResult, DateTime? startCandidates, DateTime? endCandidates, string address, string name, string email)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            DataTable table = GetApplicantByCriteria(userRegType, compCode,
                                                 idCard, testingNo,
                                                 firstName, lastName,
                                                 startDate, toDate,
                                                 paymentNo, billNo,
                                                 RowPerPage, pageNum, Count, license, time, examPlaceGroupCode, examPlaceCode, chequeNo, examResult, startCandidates, endCandidates).DataResponse.Tables[0];
            String fromEmail = ConfigurationManager.AppSettings["EmailOut"].ToString();
            SendMailExel.Send(table, fromEmail, address, compCode, name, email);
            return res;
        }

        //UC015 แสดงรายชื่อผู้ผ่านสอบ
        /// <summary>
        /// ดึงข้อมูลการสมัครสอบ
        /// </summary>
        /// <param name="applicantCode">เลขที่สอบ</param>
        /// <param name="testingNo">รหัสสอบ</param>
        /// <param name="examPlaceCode">รหัสสนามสอบ</param>
        /// <returns>ResponseService<ApplicantInfo></returns>
        public DTO.ResponseService<DTO.ApplicantInfo> GetApplicantInfo(string applicantCode,
                                                                       string testingNo,
                                                                       string examPlaceCode,
                                                                       int num_page, int RowPerPage, Boolean Count)
        {
            string presql = string.Empty;
            string endsql = string.Empty;
            var res = new DTO.ResponseService<DTO.ApplicantInfo>();
            try
            {
                if (Count)
                {
                    presql = "select count(*) CCount from ( ";
                    endsql = " ) ";
                }
                else
                {
                    presql = "select * from ( ";
                    endsql = num_page == 0
                                ? ""
                                : " ) A  WHERE A.RUN_NO BETWEEN " +
                                         num_page.StartRowNumber(RowPerPage).ToString() + " AND " +
                                         num_page.ToRowNumber(RowPerPage).ToString();

                }
                string sql = presql + " SELECT ass.ASSOCIATION_NAME AssociationName, EP.ASSOCIATION_CODE AssociationCode, lr.special Special, ltr.license_type_name LicenseTypeName, lr.license_type_code LicenseTypeCode, sr.PAYMENT_DATE PaymentDate, sr.RECEIPT_NO ReceiptNO, pro.name ProvinceName, ass.ASSOCIATION_NAME ExamOwner, lr.exam_owner, epg.exam_place_group_name PlaceGroupName, TT.NAME Title, AP.NAMES FirstName, AP.LASTNAME LastName, AP.ID_CARD_NO IdCard, " +
                             "       LR.TESTING_DATE TestingDate, case when AP.APPLY_DATE is null then to_date('01/01/0001','dd/MM/yyyy') else AP.APPLY_DATE end  ApplyDate, AP.APPLICANT_CODE ApplicantCode,ATT.TEST_TIME TestTime, " +
                             "       AP.TESTING_NO TestingNo, EP.EXAM_PLACE_NAME ExamPlace,AP.ACCEPT_OFF_CODE AcceptOfficeName, " +
                             "       CP.COMP_NAMET InsuranceCompanyName, AP.EXPIRE_DATE ExpireDate, AP.ABSENT_EXAM Absent, LR.EXAM_STATUS ExamForce, " +
                             "       AP.LICENSE LicenseApprove, case AP.RESULT when 'P' then 'ผ่าน'  when 'F' then 'ไม่ผ่าน' when 'B' then 'แบล๊กลิสต์' else  (case ap.absent_exam when 'M' then 'ขาดสอบ' else 'รอผลการสอบ' end) end ExamResult, AP.PAYMENT_NO PaymentNo , ROW_NUMBER() OVER (ORDER BY LR.TESTING_DATE ) RUN_NO " +
                             "FROM AG_APPLICANT_T AP LEFT OUTER JOIN VW_IAS_TITLE_NAME TT ON  AP.PRE_NAME_CODE = TT.ID " +
                             " LEFT OUTER JOIN AG_EXAM_LICENSE_R LR ON AP.TESTING_NO = LR.TESTING_NO AND AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE " +
                             " LEFT OUTER JOIN AG_IAS_SUBPAYMENT_D_T SDT ON SDT.ID_CARD_NO = AP.ID_CARD_NO and SDT.TESTING_NO = AP.TESTING_NO and ap.exam_place_code = sdt.exam_place_code " +
                             " LEFT OUTER JOIN AG_IAS_SUBPAYMENT_H_T SHT ON SHT.HEAD_REQUEST_NO = SDT.HEAD_REQUEST_NO " +
                             " LEFT OUTER JOIN AG_EXAM_PLACE_R EP ON EP.exam_place_code = AP.EXAM_PLACE_codE " +
                             " Left outer join AS_COMPANY_T CP  on AP.INSUR_COMP_CODE = CP.COMP_CODE  " +
                             " LEFT OUTER JOIN AG_EXAM_TIME_R ATT on ATT.TEST_TIME_CODE = LR.TEST_TIME_CODE " +
                             " LEFT OUTER JOIN ag_exam_place_group_r epg on ep.exam_place_group_code = epg.exam_place_group_code  " +
                             " LEFT OUTER JOIN AG_IAS_ASSOCIATION ass on lr.exam_owner = ass.ASSOCIATION_CODE " +
                             " LEFT OUTER JOIN vw_ias_province pro on EP.PROVINCE_CODE = pro.id " +
                             " left outer join ag_ias_subpayment_receipt sr on SHT.HEAD_REQUEST_NO = sr.HEAD_REQUEST_NO " +
                             " left outer join ag_license_type_r ltr on lr.license_type_code = ltr.license_type_code " +
                             " WHERE " +
                             "      AP.APPLICANT_CODE = '" + applicantCode + "' AND " +
                             "      AP.TESTING_NO = '" + testingNo + "' AND " +
                             "      AP.EXAM_PLACE_CODE = '" + examPlaceCode + "' " + endsql;

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);
                if (!Count)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //res.DataResponse = dt.Rows[0].MapToEntity<DTO.ApplicantInfo>();


                        var ai = new DTO.ApplicantInfo();
                        DataRow r1 = dt.Rows[0];
                        ai.Title = r1["Title"].ToString();
                        ai.FirstName = r1["FirstName"].ToString();
                        ai.LastName = r1["LastName"].ToString();
                        ai.IdCard = r1["IdCard"].ToString();
                        ai.TestingTime = r1["TestTime"].ToString();
                        if (!string.IsNullOrWhiteSpace(r1["TestingDate"].ToString()))
                        {
                            ai.TestingDate = Convert.ToDateTime(r1["TestingDate"]);
                        }

                        if (!string.IsNullOrWhiteSpace(r1["ApplyDate"].ToString()))
                        {
                            ai.ApplyDate = Convert.ToDateTime(r1["ApplyDate"]);
                        }

                        ai.ApplicantCode = r1["ApplicantCode"].ToString();
                        ai.TestingNo = r1["TestingNo"].ToString();
                        ai.ExamPlace = r1["ExamPlace"].ToString();
                        ai.AcceptOfficeName = r1["AcceptOfficeName"].ToString();
                        ai.InsuranceCompanyName = r1["InsuranceCompanyName"].ToString();
                        ai.PlaceGroupName = r1["PlaceGroupName"].ToString();

                        if (!string.IsNullOrWhiteSpace(r1["ExpireDate"].ToString()))
                        {
                            ai.ExpireDate = Convert.ToDateTime(r1["ExpireDate"]);
                        }

                        ai.Absent = r1["Absent"].ToBool();
                        ai.ExamResult = r1["ExamResult"].ToString();
                        ai.ExamForce = r1["ExamForce"].ToBool();
                        ai.PaymentNo = r1["PaymentNo"].ToString();
                        ai.LicenseApprove = r1["LicenseApprove"].ToBool();

                        ai.Special = r1["Special"].ToString();
                        ai.LicenseTypeName = r1["LicenseTypeName"].ToString();
                        if (!string.IsNullOrWhiteSpace(r1["PaymentDate"].ToString()))
                        {
                            ai.PaymentDate = Convert.ToDateTime(r1["PaymentDate"]);
                        }
                        ai.BillNumber = r1["ReceiptNO"].ToString();
                        ai.Province = r1["ProvinceName"].ToString();
                        ai.ExamOwner = r1["ExamOwner"].ToString();
                        ai.AssociationCode = r1["AssociationCode"].ToString();
                        ai.AssociationName = r1["AssociationName"].ToString();
                        res.DataResponse = ai;

                        //Dictionary<string, string> dicLicenseType =
                        //base.ctx.AG_LICENSE_TYPE_R
                        //        .ToDictionary(k => k.LICENSE_TYPE_CODE, v => v.LICENSE_TYPE_NAME);


                        sql = "SELECT   t.SUBJECT_CODE, t.SCORE,r.subject_name " +
                              "FROM     AG_APPLICANT_SCORE_T t left join ag_subject_r r on  r.subject_code = t.subject_code  and r.license_type_code = t.license_type_code " +
                              "WHERE    APPLICANT_CODE ='" + applicantCode + "'  AND " +
                              "         TESTING_NO = '" + testingNo + "' AND " +
                              "         EXAM_PLACE_CODE = '" + examPlaceCode + "'  order by t.subject_code asc";

                        DataTable dt2 = ora.GetDataTable(sql);

                        if (dt2.Rows.Count > 0)
                        {
                            List<DTO.ExamScoreResult> lsExam = new List<DTO.ExamScoreResult>();

                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                DataRow rr = dt2.Rows[i];
                                DTO.ExamScoreResult entScore = new DTO.ExamScoreResult();
                                entScore.SubjectCode = rr["SUBJECT_CODE"].ToString();
                                entScore.Score = rr["SCORE"].ToInt();
                                //entScore.LicenseType = dicLicenseType[rr["subject_name"].ToString()];
                                entScore.LicenseType = rr["subject_name"].ToString();
                                lsExam.Add(entScore);
                            }
                            res.DataResponse.Subjects = lsExam;
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        var ai = new DTO.ApplicantInfo();
                        DataRow r1 = dt.Rows[0];
                        ai.Title = r1["CCount"].ToString();
                        res.DataResponse = ai;
                    }
                    else
                    {
                        res.ErrorMsg = Resources.errorApplicantService_037;
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantInfo", ex);
            }
            return res;
        }

        //สำหรับสมาคม
        //public DTO.ResponseService<DataSet> GetApplicantByLicenseType(string licenseType)
        //{
        //    DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

        //    try
        //    {
        //        string sql = "SELECT AP.ID_CARD_NO, TT.NAME || AP.NAMES, AP.LASTNAME, AP.TESTING_NO, " +
        //                     "       LR.TESTING_DATE, AP.RESULT, AP.PAYMENT_NO, 'BillNo' BillNo " +
        //            //"       ,AP.APPLICANT_CODE, AP.TESTING_NO, AP.EXAM_PLACE_CODE " +
        //                     "FROM  AG_APPLICANT_T AP, " +
        //                     "      VW_IAS_TITLE_NAME TT, " +
        //                     "      AG_EXAM_LICENSE_R LR, " +
        //                     "      AG_IAS_APPLICANT_HEADER_T HP " +
        //                     "WHERE AP.PRE_NAME_CODE = TT.ID AND " +
        //                     "      AP.TESTING_NO = LR.TESTING_NO AND " +
        //                     "      AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND " +
        //                     "      HP.UPLOAD_GROUP_NO = AP.UPLOAD_GROUP_NO AND " +
        //                     "      HP.LICENSE_TYPE_CODE IN('01','02','03','04')";  //แก้ไขกรณีรหัสประเภทใบอนุญาตเป็น ชีวิต กับ วินาศ


        //        OracleDB db = new OracleDB();
        //        DataSet ds = ds = db.GetDataSet(sql);

        //        res.DataResponse = ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }

        //    return res;

        //}

        //สำหรับบุคคลทั่วไป
        //public DTO.ResponseService<DataSet> PersonGetApplicant(string idCard)
        //{
        //    DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

        //    try
        //    {
        //        string sql = "SELECT AP.ID_CARD_NO, TT.NAME || AP.NAMES, AP.LASTNAME, AP.TESTING_NO, " +
        //                     "       LR.TESTING_DATE, AP.RESULT, AP.PAYMENT_NO, 'BillNo' BillNo " +
        //            //"       ,AP.APPLICANT_CODE, AP.TESTING_NO, AP.EXAM_PLACE_CODE " +
        //                     "FROM  AG_APPLICANT_T AP, " +
        //                     "      VW_IAS_TITLE_NAME TT, " +
        //                     "      AG_EXAM_LICENSE_R LR " +
        //                     "WHERE AP.PRE_NAME_CODE = TT.ID AND " +
        //                     "      AP.TESTING_NO = LR.TESTING_NO AND " +
        //                     "      AP.EXAM_PLACE_CODE = LR.EXAM_PLACE_CODE AND " +
        //                     "      AP.ID_CARD_NO = '" + idCard + "' " +
        //                     "ORDER BY LR.TESTING_DATE DESC";

        //        OracleDB db = new OracleDB();
        //        DataSet ds = ds = db.GetDataSet(sql);

        //        res.DataResponse = ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }

        //    return res;

        //}

        public DTO.ResponseService<DataSet> GetRequestEditApplicant(DTO.RegistrationType userRegType,
                                                                  string idCard, string testingNo, string CompCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO like '{0}%' AND ", idCard));
                sb.Append(GetCriteria("AP.TESTING_NO like '{0}%' AND ", testingNo));

                string sqlcomp = string.Empty;
                if (CompCode == null)
                {
                    sqlcomp = "";
                }
                else
                {
                    sqlcomp = " AND A.upload_by_session='" + CompCode + "' ";
                }
                //else if (CompCode.Length == 3)
                //{
                //    sqlcomp = "  AND A.INSUR_COMP_CODE is null and  D.EXAM_OWNER='" + CompCode + "' ";
                //}
                //else
                //{
                //    sqlcomp = " AND A.INSUR_COMP_CODE='" + CompCode + "' ";
                //}


                string tmp = sb.ToString();

                string sql = "SELECT A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, "
                            + "A.ID_CARD_NO ,A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                            + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                            + "WHERE A.PRE_NAME_CODE=B.ID "
                            + "AND a.testing_no=d.testing_no "
                            + "and d.exam_owner=f.association_code "
                            + "and a.absent_exam is not null "
                            + "AND A.TESTING_NO='" + testingNo + "' "
                            + "AND A.ID_CARD_NO='" + idCard + "' "
                            + sqlcomp
                            + "order by 1";





                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetRequestEditApplicant", ex);
            }

            return res;

        }
        public DTO.ResponseService<DataSet> GetApplicantChangeMaxID()
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select Max(change_id) AS change_id from ag_ias_applicant_change";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantChangeMaxID", ex);
            }
            return res;
        }
        public DTO.ResponseMessage<bool> InsertApplicantChange(DTO.ApplicantChange appChange)
        {

            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var app = base.ctx.AG_IAS_APPLICANT_CHANGE.FirstOrDefault(a => a.CHANGE_ID == appChange.CHANGE_ID);

                AG_IAS_APPLICANT_CHANGE newapp = new AG_IAS_APPLICANT_CHANGE();

                if (app == null)
                {
                    newapp.CHANGE_ID = appChange.CHANGE_ID;
                    newapp.COMP_CODE = appChange.COMP_CODE;
                    newapp.TESTING_NO = appChange.TESTING_NO;
                    newapp.OLD_ID_CARD_NO = appChange.OLD_ID_CARD_NO;
                    newapp.OLD_PREFIX = appChange.OLD_PREFIX;
                    newapp.OLD_FNAME = appChange.OLD_FNAME;
                    newapp.OLD_LNAME = appChange.OLD_LNAME;
                    newapp.NEW_ID_CARD_NO = appChange.NEW_ID_CARD_NO;
                    newapp.NEW_PREFIX = appChange.NEW_PREFIX;
                    newapp.NEW_FNAME = appChange.NEW_FNAME;
                    newapp.NEW_LNAME = appChange.NEW_LNAME;
                    newapp.REMARK = appChange.REMARK;
                    newapp.STATUS = appChange.STATUS;
                    newapp.CREATE_BY = appChange.CREATE_BY;
                    newapp.CREATE_DATE = DateTime.Now;
                    newapp.ASSOCIATION_USER_ID = null;
                    newapp.ASSOCIATION_DATE = null;
                    newapp.ASSOCIATION_RESULT = appChange.ASSOCIATION_RESULT;
                    newapp.OIC_USER_ID = null;
                    newapp.OIC_DATE = null;
                    newapp.OIC_RESULT = appChange.OIC_RESULT;
                    base.ctx.AG_IAS_APPLICANT_CHANGE.AddObject(newapp);

                }
                else
                {
                    app.CHANGE_ID = appChange.CHANGE_ID;
                    app.STATUS = appChange.STATUS;
                    if (appChange.OIC_RESULT == 1 || appChange.OIC_RESULT == 2)
                    {
                        app.OIC_USER_ID = appChange.OIC_USER_ID;
                        app.OIC_DATE = appChange.OIC_DATE;
                        app.OIC_RESULT = appChange.OIC_RESULT;
                        if (appChange.OIC_RESULT == 1)//update AG_Applicant_T
                        {
                            AG_APPLICANT_T UpdateAppT = new AG_APPLICANT_T();

                            string testingno = appChange.TESTING_NO.ToString();
                            string idcardno = appChange.OLD_ID_CARD_NO.ToString();
                            string newidcard = appChange.NEW_ID_CARD_NO.ToString();
                            string newprenamecode = appChange.NEW_PREFIX.ToString();

                            var appT = base.ctx.AG_APPLICANT_T.FirstOrDefault(b => b.TESTING_NO == testingno && b.ID_CARD_NO == idcardno && b.EXAM_PLACE_CODE == b.EXAM_PLACE_CODE);
                            appT.ID_CARD_NO = newidcard;
                            appT.PRE_NAME_CODE = newprenamecode;
                            appT.NAMES = appChange.NEW_FNAME;
                            appT.LASTNAME = appChange.NEW_LNAME;

                            base.ctx.AG_APPLICANT_T.MappingToEntity(appT);
                        }
                        else
                        {
                            app.CANCEL_REASON = appChange.CANCEL_REASON;
                        }
                    }
                    else
                    {
                        app.ASSOCIATION_USER_ID = appChange.ASSOCIATION_USER_ID;
                        app.ASSOCIATION_DATE = appChange.ASSOCIATION_DATE;
                        app.ASSOCIATION_RESULT = appChange.ASSOCIATION_RESULT;

                        app.CANCEL_REASON = appChange.CANCEL_REASON;
                    }


                    base.ctx.AG_IAS_APPLICANT_CHANGE.MappingToEntity(app);

                }
                ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                res.ResultMessage = false;
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertApplicantChange", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetHistoryApplicant(DTO.RegistrationType userRegType,
                                                                 string idCard, string testingNo, string CompCode, string ExamPlaceCode, string Status, int pageNo, int recordPerPage, Boolean Count, string Asso, string oic)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                //StringBuilder sb = new StringBuilder();
                //sb.Append(GetCriteria("AP.ID_CARD_NO like '{0}%' AND ", idCard));
                //sb.Append(GetCriteria("AP.TESTING_NO like '{0}%' AND ", testingNo));
                //sb.Append(GetCriteria("AP.COMP_CODE like '{0}%' AND ", CompCode));
                //sb.Append(GetCriteria("AP.EXAM_PLACE_CODE ='{0}' AND ", ExamPlaceCode));
                //sb.Append(GetCriteria("AP.STATUS like '{0}%' AND ", Status));

                //if (userRegType == DTO.RegistrationType.Association)
                //{

                //    sb.Append(GetCriteria("AP.EXAM_PLACE_CODE = '{0}' AND ", ExamPlaceCode));
                //}
                //string tmp = sb.ToString();
                string sql;
                if (!Count)
                {
                    sql = "select * from( select row_number()over(ORDER BY A.CHANGE_ID) As Rowno, A.CHANGE_ID, A.COMP_CODE,A.TESTING_NO, B.EXAM_PLACE_CODE, "
                        + "B.EXAM_owner,e.association_name,A.OLD_ID_CARD_NO,A.OLD_PREFIX,C.NAME AS OLD_PREFIX_NAME,A.OLD_FNAME,A.OLD_LNAME, A.NEW_ID_CARD_NO, "
                        + "A.NEW_PREFIX, A.NEW_FNAME,A.NEW_LNAME,A.REMARK,A.STATUS,A.CREATE_BY,A.CREATE_DATE, A.ASSOCIATION_USER_ID,A.ASSOCIATION_DATE, "
                        + "A.ASSOCIATION_RESULT, A.OIC_USER_ID,A.OIC_DATE,A.OIC_RESULT,A.CANCEL_REASON , "
                        + "case status when 0 then 'รอการพิจารณา(สมาคม)' when 1 then (case association_result  when 1  then (case oic_result when 0  then 'รอการพิจารณา(คปภ.)' end)else 'ไม่อนุมัต(สมาคม)' end )else  (case  oic_result when 1 then 'อนุมัติ(คปภ.)' else 'ไม่อนุมัติ(คปภ.)'end )end statusname, "
                        + "status || association_result || oic_result as searchstatus "
                        + "FROM AG_IAS_APPLICANT_CHANGE A ,  AG_EXAM_LICENSE_R B,VW_IAS_TITLE_NAME C,  ag_ias_association E "
                        + "where A.OLD_PREFIX=C.ID "
                        + "and b.exam_owner like'" + ExamPlaceCode + "%' "
                        + "and a.Testing_no=b.Testing_no "
                        + "and B.EXAM_OWNER=e.association_code  "
                        + "and A.TESTING_NO LIKE '" + testingNo + "%' "
                        + "AND A.OLD_ID_CARD_NO LIKE '" + idCard + "%'  "
                        + "and a.status like'" + Status + "%' "
                        + "and a.association_result like'" + Asso + "%' "
                        + "and a.oic_result like'" + oic + "%' order by a.change_id asc) G  "
                        + "where G.Rowno between " + pageNo.StartRowNumber(recordPerPage).ToString() + " and " + pageNo.ToRowNumber(recordPerPage).ToString();
                }
                else
                {
                    sql = "select COUNT(*) CCount from (  "
                        + "select * from( select row_number()over(ORDER BY A.CHANGE_ID) As Rowno, A.CHANGE_ID, A.COMP_CODE,A.TESTING_NO, B.EXAM_PLACE_CODE, "
                        + "B.EXAM_owner,e.association_name,A.OLD_ID_CARD_NO,A.OLD_PREFIX,C.NAME AS OLD_PREFIX_NAME,A.OLD_FNAME,A.OLD_LNAME, A.NEW_ID_CARD_NO, "
                        + "A.NEW_PREFIX, A.NEW_FNAME,A.NEW_LNAME,A.REMARK,A.STATUS,A.CREATE_BY,A.CREATE_DATE, A.ASSOCIATION_USER_ID,A.ASSOCIATION_DATE, "
                        + "A.ASSOCIATION_RESULT, A.OIC_USER_ID,A.OIC_DATE,A.OIC_RESULT,A.CANCEL_REASON , "
                        + "case status when 0 then 'รอการพิจารณา(สมาคม)' when 1 then (case association_result  when 1  then (case oic_result when 0  then 'รอการพิจารณา(คปภ.)' end)else 'ไม่อนุมัต(สมาคม)' end )else  (case  oic_result when 1 then 'อนุมัติ(คปภ.)' else 'ไม่อนุมัติ(คปภ.)'end )end statusname, "
                        + "status || association_result || oic_result as searchstatus "
                        + "FROM AG_IAS_APPLICANT_CHANGE A ,  AG_EXAM_LICENSE_R B,VW_IAS_TITLE_NAME C,  ag_ias_association E "
                        + "where A.OLD_PREFIX=C.ID "
                        + "and b.exam_owner like'" + ExamPlaceCode + "%' "
                        + "and a.Testing_no=b.Testing_no "
                        + "and B.EXAM_OWNER=e.association_code  "
                        + "and A.TESTING_NO LIKE '" + testingNo + "%' "
                        + "AND A.OLD_ID_CARD_NO LIKE '" + idCard + "%'  "
                        + "and a.status like'" + Status + "%' "
                        + "and a.association_result like'" + Asso + "%' "
                        + "and a.oic_result like'" + oic + "%' order by a.change_id asc) G  "
                        + " )";
                }










                OracleDB db = new OracleDB();
                res.DataResponse = db.GetDataSet(sql);
                int a = res.DataResponse.Tables[0].Rows.Count;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetHistoryApplicant", ex);
            }

            return res;

        }



        public DTO.ResponseService<DataSet> GetApproveEditApplicant(DTO.RegistrationType userRegType,
                                                                 string idCard, string testingNo, string CompCode, string ExamPlaceCode, string Status, int pageNo, int recordPerPage, Boolean Count, string membertype, string Asso, string oic)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO like '{0}%' AND ", idCard));
                sb.Append(GetCriteria("AP.TESTING_NO like '{0}%' AND ", testingNo));
                sb.Append(GetCriteria("AP.COMP_CODE like '{0}%' AND ", CompCode));
                sb.Append(GetCriteria("AP.EXAM_PLACE_CODE ='{0}' AND ", ExamPlaceCode));
                sb.Append(GetCriteria("AP.STATUS like '{0}%' AND ", Status));
                // sb.Append(GetCriteria("AP.PAYMENT_NO like '{0}%' AND ", paymentNo));
                //sb.Append(GetCriteria("AP.PAYMENT_NO = '{0}' AND ", billNo));  


                //if (userRegType == DTO.RegistrationType.General)
                // {
                //     if (string.IsNullOrEmpty(idCard))
                //     {
                //         res.ErrorMsg = "No IdCard.";
                //         return res;
                //     }
                // }
                //if (userRegType == DTO.RegistrationType.OIC)
                //{

                //    sb.Append(GetCriteria("AP.EXAM_PLACE_CODE = '{0}' AND ", ExamPlaceCode));
                //}

                string tmp = sb.ToString();
                string sql;
                if (!Count)
                {
                    sql = "select * from( "
                            + "select row_number()over(ORDER BY A.CHANGE_ID) As Rowno, A.CHANGE_ID, A.COMP_CODE,A.TESTING_NO, "
                            + "B.EXAM_owner,e.association_name as exam_place_name, "
                            + "A.OLD_ID_CARD_NO,A.OLD_PREFIX,C.NAME AS OLD_PREFIX_NAME,A.OLD_FNAME,A.OLD_LNAME, "
                            + "A.NEW_ID_CARD_NO,A.NEW_PREFIX,A.NEW_FNAME,A.NEW_LNAME,A.REMARK,A.STATUS,A.CREATE_BY,A.CREATE_DATE, A.ASSOCIATION_USER_ID, "
                            + "A.ASSOCIATION_DATE,A.ASSOCIATION_RESULT,A.OIC_USER_ID,A.OIC_DATE,A.OIC_RESULT,A.CANCEL_REASON, "
                            + "case status when 0 then 'รอการพิจารณา(สมาคม)' when 1 then (case association_result  when 1  then (case oic_result when 0  then 'รอการพิจารณา(คปภ.)' end)else 'ไม่อนุมัต(สมาคม)' end )else  (case  oic_result when 1 then 'อนุมัติ(คปภ.)' else 'ไม่อนุมัติ(คปภ.)'end )end statusname, "
                            + "status || association_result || oic_result as searchstatus "
                            + "FROM AG_IAS_APPLICANT_CHANGE A ,VW_IAS_TITLE_NAME C,AG_IAS_MEMBER_TYPE D ,ag_exam_license_r B,ag_ias_association E "
                            + "where A.OLD_PREFIX=C.ID and A.TESTING_NO LIKE '" + testingNo + "%' "
                            + "AND A.OLD_ID_CARD_NO LIKE '" + idCard + "%' "
                            + "and a.testing_no=b.testing_no "
                            + "and b.exam_owner=E.ASSOCIATION_CODE "
                            + "and a.status like'" + Status + "%' and a.association_result like'" + Asso + "%' and a.oic_result like'" + oic + "%' "
                            + "and A.ASSOCIATION_RESULT='1' "
                            + "and D.MEMBER_CODE='" + membertype + "' "
                            + "order by a.change_id asc) "
                            + "G "
                            + "where  G.Rowno between " + pageNo.StartRowNumber(recordPerPage).ToString() + " and " + pageNo.ToRowNumber(recordPerPage).ToString();
                }
                else
                {
                    sql = "select COUNT(*) CCount from (  "
                            + "select * from( "
                            + "select row_number()over(ORDER BY A.CHANGE_ID) As Rowno, A.CHANGE_ID, A.COMP_CODE,A.TESTING_NO, "
                            + "B.EXAM_owner,e.association_name as exam_place_name, "
                            + "A.OLD_ID_CARD_NO,A.OLD_PREFIX,C.NAME AS OLD_PREFIX_NAME,A.OLD_FNAME,A.OLD_LNAME, "
                            + "A.NEW_ID_CARD_NO,A.NEW_PREFIX,A.NEW_FNAME,A.NEW_LNAME,A.REMARK,A.STATUS,A.CREATE_BY,A.CREATE_DATE, A.ASSOCIATION_USER_ID, "
                            + "A.ASSOCIATION_DATE,A.ASSOCIATION_RESULT,A.OIC_USER_ID,A.OIC_DATE,A.OIC_RESULT,A.CANCEL_REASON, "
                            + "case status when 0 then 'รอการพิจารณา(สมาคม)' when 1 then (case association_result  when 1  then (case oic_result when 0  then 'รอการพิจารณา(คปภ.)' end)else 'ไม่อนุมัต(สมาคม)' end )else  (case  oic_result when 1 then 'อนุมัติ(คปภ.)' else 'ไม่อนุมัติ(คปภ.)'end )end statusname, "
                            + "status || association_result || oic_result as searchstatus "
                            + "FROM AG_IAS_APPLICANT_CHANGE A ,VW_IAS_TITLE_NAME C,AG_IAS_MEMBER_TYPE D ,ag_exam_license_r B,ag_ias_association E "
                            + "where A.OLD_PREFIX=C.ID and A.TESTING_NO LIKE '" + testingNo + "%' "
                            + "AND A.OLD_ID_CARD_NO LIKE '" + idCard + "%' "
                            + "and a.testing_no=b.testing_no "
                            + "and b.exam_owner=E.ASSOCIATION_CODE "
                            + "and a.status like'" + Status + "%' and a.association_result like'" + Asso + "%' and a.oic_result like'" + oic + "%' "
                            + "and A.ASSOCIATION_RESULT='1' "
                            + "and D.MEMBER_CODE='" + membertype + "' "
                            + "order by a.change_id asc) "
                            + "G "
                            + " )";
                }






                OracleDB db = new OracleDB();
                res.DataResponse = db.GetDataSet(sql);
                int a = res.DataResponse.Tables[0].Rows.Count;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApproveEditApplicant", ex);
            }

            return res;

        }

        public DTO.ResponseService<DataSet> GetApplicantTLogMaxID()
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select max(applicant_code_log) as applicant_code_log from ag_ias_applicant_t_log";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantTLogMaxID", ex);
            }
            return res;
        }


        public DTO.ResponseService<DataSet> GetApplicantTtoLog(DTO.RegistrationType userRegType,
                                                                 string idCard, string testingNo, string CompCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO = '{0}' AND ", idCard));
                sb.Append(GetCriteria("AP.TESTING_NO = '{0}' AND ", testingNo));


                //if (userRegType == DTO.RegistrationType.Insurance)
                //{

                //    sb.Append(GetCriteria("AP.INSUR_COMP_CODE = '{0}' AND ", CompCode));
                //}

                string tmp = sb.ToString();
                string sql = string.Empty;
                //if (CompCode.Length == 3)
                //{
                //sql = "select * from ag_applicant_t APP "
                //    + "left join AG_EXAM_LICENSE_R LR on APP.TESTING_NO=LR.TESTING_NO "
                //    + "where APP.TESTING_NO='" + testingNo + "' "
                //    + "and APP.ID_CARD_NO='" + idCard + "' "
                //    + "and app.absent_exam is not null  "
                //    + "and APP.TESTING_NO=LR.TESTING_NO "
                //    + "and LR.EXAM_OWNER='" + CompCode + "' order by 1";
                //}
                //else
                //{
                sql = "select * from ag_applicant_t "
                           + "where TESTING_NO='" + testingNo + "' "
                           + "and ID_CARD_NO='" + idCard + "' "
                           + "and upload_by_session='" + CompCode + "' order by 1";
                //}


                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantTtoLog", ex);
            }

            return res;

        }



        public DTO.ResponseService<DTO.Applicant> GetApplicantDetail(int applicantCode, string testingNo, string examPlaceCode)
        {
            DTO.ResponseService<DTO.Applicant> res = new DTO.ResponseService<DTO.Applicant>();

            try
            {
                var appl = base.ctx.AG_APPLICANT_T.SingleOrDefault(s => s.APPLICANT_CODE == applicantCode && s.TESTING_NO == testingNo && s.EXAM_PLACE_CODE == examPlaceCode);
                if (appl != null)
                {
                    res.DataResponse = new DTO.Applicant();
                    appl.MappingToEntity(res.DataResponse);
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantDetail", ex);
            }

            return res;
        }






        public DTO.ResponseMessage<bool> InsertApplicantTLog(DTO.ApplicantTLog appTLog)
        {

            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var app = base.ctx.AG_IAS_APPLICANT_T_LOG.FirstOrDefault(a => a.TESTING_NO == appTLog.TESTING_NO && a.ID_CARD_NO == appTLog.ID_CARD_NO && a.INUSR_COMP_CODE == appTLog.INSUR_COMP_CODE);

                AG_IAS_APPLICANT_T_LOG newappTLog = new AG_IAS_APPLICANT_T_LOG();
                if (app == null)
                {

                    newappTLog.APPLICANT_CODE_LOG = appTLog.APPLICANT_CODE_LOG;
                    newappTLog.APPLICANT_CODE = appTLog.APPLICANT_CODE;
                    newappTLog.TESTING_NO = appTLog.TESTING_NO;
                    newappTLog.EXAM_PLACE_CODE = appTLog.EXAM_PLACE_CODE;
                    newappTLog.ACCEPT_OFF_CODE = appTLog.ACCEPT_OFF_CODE;
                    newappTLog.APPLY_DATE = appTLog.APPLY_DATE;
                    newappTLog.ID_CARD_NO = appTLog.ID_CARD_NO;
                    newappTLog.PRE_NAME_CODE = appTLog.PRE_NAME_CODE;
                    newappTLog.NAMES = appTLog.NAMES;
                    newappTLog.LASTNAME = appTLog.LASTNAME;
                    newappTLog.BIRTH_DATE = appTLog.BIRTH_DATE;
                    newappTLog.SEX = appTLog.SEX;
                    newappTLog.EDUCATION_CODE = appTLog.EDUCATION_CODE;
                    newappTLog.ADDRESS1 = appTLog.ADDRESS1;
                    newappTLog.ADDRESS2 = appTLog.ADDRESS2;
                    newappTLog.AREA_CODE = appTLog.AREA_CODE;
                    newappTLog.PROVINCE_CODE = appTLog.PROVINCE_CODE;
                    newappTLog.ZIPCODE = appTLog.ZIPCODE;
                    newappTLog.TELEPHONE = appTLog.TELEPHONE;
                    newappTLog.AMOUNT_TRAN_NO = appTLog.AMOUNT_TRAN_NO;
                    newappTLog.PAYMENT_NO = appTLog.PAYMENT_NO;
                    newappTLog.INUSR_COMP_CODE = appTLog.INSUR_COMP_CODE;
                    newappTLog.ABSENT_EXAM = appTLog.ABSENT_EXAM;
                    newappTLog.RESULT = appTLog.RESULT;
                    newappTLog.EXPIRE_DATE = appTLog.EXPIRE_DATE;
                    newappTLog.LICENSE = appTLog.LICENSE;
                    newappTLog.CANCEL_RESON = appTLog.CANCEL_REASON;
                    newappTLog.RECODE_STATUS = appTLog.RECORD_STATUS;
                    newappTLog.USER_ID = appTLog.USER_ID;
                    newappTLog.USER_DATE = appTLog.USER_DATE;
                    newappTLog.EXAM_STATUS = appTLog.EXAM_STATUS;
                    newappTLog.UPLOAD_GROUP_NO = appTLog.UPLOAD_GROUP_NO;
                    newappTLog.HEAD_REQUEST_NO = appTLog.HEAD_REQUEST_NO;
                    newappTLog.GROUP_REQUEST_NO = appTLog.GROUP_REQUEST_NO;
                    newappTLog.UPLOAD_BY_SESSION = appTLog.UPLOAD_BY_SESSION;
                    newappTLog.ID_ATTACH_FILE = appTLog.ID_ATTACH_FILE;
                    newappTLog.CREATE_BY = appTLog.CREATE_BY;
                    newappTLog.CREATE_DATE = appTLog.CREATE_DATE;


                    base.ctx.AG_IAS_APPLICANT_T_LOG.AddObject(newappTLog);

                }
                ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                res.ResultMessage = false;
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertApplicantTLog", ex);
            }
            return res;
        }



        public DTO.ResponseMessage<bool> InsertAttrachFileApplicantChange(List<DTO.AttachFileApplicantChange> appAttachFileChange, DTO.UserProfile userProfile, DTO.ApplicantChange appChange)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            string Current_CHANGE_ID = string.Empty;

            try
            {
                using (TransactionScope trans = new TransactionScope())
                {
                    #region Insert App & Update
                    var app = base.ctx.AG_IAS_APPLICANT_CHANGE.FirstOrDefault(a => a.CHANGE_ID == appChange.CHANGE_ID);


                    string sql = "SELECT change_seq.nextval FROM dual";
                    OracleDB db = new OracleDB();
                    DataSet ds = ds = db.GetDataSet(sql);
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[0];
                    Current_CHANGE_ID = dr["NEXTVAL"].ToString();

                    AG_IAS_APPLICANT_CHANGE newapp = new AG_IAS_APPLICANT_CHANGE();
                    if (app == null)
                    {
                        //newapp.CHANGE_ID = appChange.CHANGE_ID;
                        newapp.CHANGE_ID = Convert.ToDecimal(Current_CHANGE_ID);
                        newapp.COMP_CODE = appChange.COMP_CODE;
                        newapp.TESTING_NO = appChange.TESTING_NO;
                        newapp.OLD_ID_CARD_NO = appChange.OLD_ID_CARD_NO;
                        newapp.OLD_PREFIX = appChange.OLD_PREFIX;
                        newapp.OLD_FNAME = appChange.OLD_FNAME;
                        newapp.OLD_LNAME = appChange.OLD_LNAME;
                        newapp.NEW_ID_CARD_NO = appChange.NEW_ID_CARD_NO;
                        newapp.NEW_PREFIX = appChange.NEW_PREFIX;
                        newapp.NEW_FNAME = appChange.NEW_FNAME;
                        newapp.NEW_LNAME = appChange.NEW_LNAME;
                        newapp.REMARK = appChange.REMARK;
                        newapp.STATUS = appChange.STATUS;
                        newapp.CREATE_BY = appChange.CREATE_BY;
                        newapp.CREATE_DATE = appChange.CREATE_DATE;
                        newapp.ASSOCIATION_USER_ID = appChange.ASSOCIATION_USER_ID;
                        newapp.ASSOCIATION_DATE = appChange.ASSOCIATION_DATE;
                        newapp.ASSOCIATION_RESULT = appChange.ASSOCIATION_RESULT;
                        newapp.OIC_USER_ID = null;
                        newapp.OIC_DATE = null;
                        newapp.OIC_RESULT = appChange.OIC_RESULT;
                        base.ctx.AG_IAS_APPLICANT_CHANGE.AddObject(newapp);

                    }
                    else
                    {
                        //app.CHANGE_ID = appChange.CHANGE_ID;
                        app.CHANGE_ID = Convert.ToDecimal(Current_CHANGE_ID);
                        app.STATUS = appChange.STATUS;
                        if (appChange.OIC_RESULT == 1 || appChange.OIC_RESULT == 2)
                        {
                            app.OIC_USER_ID = appChange.OIC_USER_ID;
                            app.OIC_DATE = appChange.OIC_DATE;
                            app.OIC_RESULT = appChange.OIC_RESULT;
                            if (appChange.OIC_RESULT == 1)//update AG_Applicant_T
                            {
                                AG_APPLICANT_T UpdateAppT = new AG_APPLICANT_T();

                                string testingno = appChange.TESTING_NO.ToString();
                                string idcardno = appChange.OLD_ID_CARD_NO.ToString();
                                string newidcard = appChange.NEW_ID_CARD_NO.ToString();
                                string newprenamecode = appChange.NEW_PREFIX.ToString();

                                var appT = base.ctx.AG_APPLICANT_T.FirstOrDefault(b => b.TESTING_NO == testingno && b.ID_CARD_NO == idcardno && b.EXAM_PLACE_CODE == b.EXAM_PLACE_CODE);
                                appT.ID_CARD_NO = newidcard;
                                appT.PRE_NAME_CODE = newprenamecode;
                                appT.NAMES = appChange.NEW_FNAME;
                                appT.LASTNAME = appChange.NEW_LNAME;
                                base.ctx.AG_APPLICANT_T.MappingToEntity(appT);
                            }
                        }
                        else
                        {
                            app.ASSOCIATION_USER_ID = appChange.ASSOCIATION_USER_ID;
                            app.ASSOCIATION_DATE = appChange.ASSOCIATION_DATE;
                            app.ASSOCIATION_RESULT = appChange.ASSOCIATION_RESULT;
                            app.NEW_PREFIX = appChange.NEW_PREFIX;
                            app.NEW_FNAME = appChange.NEW_FNAME;
                            app.NEW_LNAME = appChange.NEW_LNAME;
                            app.REMARK = appChange.REMARK;
                        }


                        base.ctx.AG_IAS_APPLICANT_CHANGE.MappingToEntity(app);

                    }
                    #endregion

                    //Attach Files
                    #region Insert Attach Files
                    foreach (DTO.AttachFileApplicantChange l in appAttachFileChange)
                    {
                        //Check before add new
                        AG_IAS_ATTACH_FILE_APPLICANT ent = base.ctx.AG_IAS_ATTACH_FILE_APPLICANT.FirstOrDefault(f => f.ID_ATTACH_FILE == l.ID_ATTACH_FILE);
                        if (ent == null)
                        {
                            AG_IAS_ATTACH_FILE_APPLICANT attach = new AG_IAS_ATTACH_FILE_APPLICANT();
                            //l.MappingToEntity(attach);
                            //attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                            //base.ctx.AG_IAS_ATTACH_FILE_LICENSE.AddObject(attach);

                            String targetFileName = new System.IO.FileInfo(l.ATTACH_FILE_PATH).Name;
                            //String targetContainer = String.Format(@"{0}\{1}", AttachFileContainer, userProfile.Id);
                            String targetContainer = String.Format(@"{0}\{1}", AttachFileContainer, userProfile.Id);
                            MoveFileResponse moveResponse = FileManagerService.RemoteFileCommand(new MoveFileRequest()
                            {
                                CurrentContainer = ""
                                ,
                                CurrentFileName = l.ATTACH_FILE_PATH
                                ,
                                TargetContainer = targetContainer
                                ,
                                TargetFileName = targetFileName
                            }).Action();
                            //attach.CHANGE_ID = l.CHANGE_ID;
                            //l.MappingToEntity(attach);
                            attach.ID_ATTACH_FILE = OracleDB.GetGenAutoId();
                            attach.ATTACH_FILE_PATH = moveResponse.TargetFullName;

                            attach.ID_CARD_NO = l.ID_CARD_NO;
                            attach.ATTACH_FILE_TYPE = l.ATTACH_FILE_TYPE;
                            attach.REMARK = l.REMARK;
                            attach.CREATED_BY = l.CREATED_BY;
                            attach.CREATED_DATE = l.CREATED_DATE;
                            attach.UPDATED_BY = l.UPDATED_BY;
                            attach.UPDATED_DATE = l.UPDATED_DATE;
                            attach.FILE_STATUS = l.FILE_STATUS;
                            //attach.CHANGE_ID = l.CHANGE_ID;
                            attach.CHANGE_ID = newapp.CHANGE_ID;

                            base.ctx.AG_IAS_ATTACH_FILE_APPLICANT.AddObject(attach);
                            //System.Threading.Thread.Sleep(500);

                            if (moveResponse.Code != "0000")
                                throw new IOException(moveResponse.Message);
                        }

                    }
                    #endregion

                    //Submit All Data
                    base.ctx.SaveChanges();
                    //System.Data.Objects.SaveOptions.AcceptAllChangesAfterSave
                    trans.Complete();
                    res.ResultMessage = true;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                res.ResultMessage = false;
                LoggerFactory.CreateLog().Fatal("ApplicantService_InsertAttrachFileApplicantChange", ex);
            }
            return res;
        }






        public DTO.ResponseService<DataSet> GetAttachFileAppChange(DTO.RegistrationType userRegType,
                                                                 string changeid)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO = '{0}' AND ", changeid));
                //sb.Append(GetCriteria("AP.TESTING_NO = '{0}' AND ", testingNo));


                //if (userRegType == DTO.RegistrationType.Insurance)
                //{

                //    sb.Append(GetCriteria("AP.INSUR_COMP_CODE = '{0}' AND ", CompCode));
                //}

                string tmp = sb.ToString();

                string sql = "select * from ag_ias_attach_file_applicant "
                           + "where change_id='" + changeid + "' order by 1";


                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetAttachFileAppChange", ex);
            }

            return res;

        }

        public DTO.ResponseService<IList<DTO.AttachFileApplicantChangeEntity>> GetAttatchFilesAppChangeByIDCard(string idcard, int changeid)
        {
            DTO.ResponseService<IList<DTO.AttachFileApplicantChangeEntity>> res = new DTO.ResponseService<IList<DTO.AttachFileApplicantChangeEntity>>();

            try
            {
                IList<AG_IAS_ATTACH_FILE_APPLICANT> regs = base.ctx.AG_IAS_ATTACH_FILE_APPLICANT.Where(a => a.ID_CARD_NO == idcard && a.CHANGE_ID == changeid).ToList();
                IList<AG_IAS_DOCUMENT_TYPE> docs = base.ctx.AG_IAS_DOCUMENT_TYPE.ToList();
                IEnumerable<DTO.AttachFileApplicantChangeEntity> result = from r in regs
                                                                          join d in docs on r.ATTACH_FILE_TYPE equals d.DOCUMENT_CODE into rd
                                                                          from d in rd.DefaultIfEmpty()
                                                                          select new DTO.AttachFileApplicantChangeEntity
                                                                          {
                                                                              ID = r.ID_ATTACH_FILE,
                                                                              REGISTRATION_ID = r.ID_CARD_NO,
                                                                              ATTACH_FILE_TYPE = r.ATTACH_FILE_TYPE,
                                                                              ATTACH_FILE_PATH = r.ATTACH_FILE_PATH,
                                                                              REMARK = r.REMARK,
                                                                              CREATED_BY = r.CREATED_BY,
                                                                              CREATED_DATE = r.CREATED_DATE,
                                                                              UPDATED_BY = r.UPDATED_BY,
                                                                              UPDATED_DATE = r.UPDATED_DATE,
                                                                              FILE_STATUS = r.FILE_STATUS,
                                                                              DocumentTypeName = d.DOCUMENT_NAME,
                                                                              FileName = r.ATTACH_FILE_PATH.Split('\\')[r.ATTACH_FILE_PATH.Split('\\').Length - 1],
                                                                              CHANGE_ID = r.CHANGE_ID

                                                                          };

                res.DataResponse = result.ToList();
                //foreach (AG_IAS_ATTACH_FILE reg in regs) { 
                //    DTO.RegistrationAttatchFile registeration = new DTO.RegistrationAttatchFile();
                //    reg.MappingToEntity(registeration);
                //    res.DataResponse.Add(registeration);
                //}

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetAttatchFilesAppChangeByIDCard", ex);
            }

            return res;
        }



        public DTO.ResponseService<DataSet> GetApproveAppForStatus(DTO.RegistrationType userRegType,
                                                                string idcard, string status, string asso, string oic)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO = '{0}' AND ", idcard));
                //sb.Append(GetCriteria("AP.TESTING_NO = '{0}' AND ", testingNo));


                //if (userRegType == DTO.RegistrationType.Insurance)
                //{

                //    sb.Append(GetCriteria("AP.INSUR_COMP_CODE = '{0}' AND ", CompCode));
                //}

                string tmp = sb.ToString();

                string sql = "select * from ag_ias_applicant_change "
                            + "where new_ID_CARD_NO='" + idcard + "' and status='" + status + "' "
                            + "and association_result='" + asso + "' "
                            + "and oic_result='" + oic + "'order by change_id desc";


                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApproveAppForStatus", ex);
            }

            return res;

        }
        public DTO.ResponseMessage<bool> SendMailAppChange(string idcard, string TestingNo, string CompCode)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            IList<DTO.EmailApplicantChange> _EmailApplicantChange = new List<DTO.EmailApplicantChange>();
            try
            {

                DAL.AG_IAS_PERSONAL_T personal = ctx.AG_IAS_PERSONAL_T.FirstOrDefault(p => p.ID == idcard);
                Int64 OLDIDCard = Convert.ToInt64(CompCode);
                Int32 Testing = Convert.ToInt32(TestingNo);
                DAL.AG_IAS_APPLICANT_CHANGE Appchange = ctx.AG_IAS_APPLICANT_CHANGE.OrderByDescending(a => a.CHANGE_ID).FirstOrDefault(a => a.OLD_ID_CARD_NO == CompCode && a.TESTING_NO == TestingNo);

                if (personal != null && !String.IsNullOrWhiteSpace(personal.EMAIL))
                {
                    string sqlold = string.Empty;
                    string sqlnew = string.Empty;
                    sqlold = "select name from VW_IAS_TITLE_NAME where id=" + Appchange.OLD_PREFIX;
                    OracleDB db = new OracleDB();
                    DataTable DTOLD = db.GetDataTable(sqlold);
                    DataRow DRold = DTOLD.Rows[0];
                    string oldprefix = DRold["name"].ToString();

                    //sqlnew = "select name from VW_IAS_TITLE_NAME where id=" + Appchange.NEW_PREFIX;
                    //DataTable DTNEW = db.GetDataTable(sqlnew);
                    //DataRow DRnew = DTNEW.Rows[0];
                    //string newprefix = DRnew["name"].ToString();

                    string sql = string.Empty;

                    List<EmailApplicantChange> LsMail = new List<EmailApplicantChange>();
                    LsMail = getSQL(Appchange.STATUS, Appchange.ASSOCIATION_RESULT, Appchange.OIC_RESULT, idcard, TestingNo, CompCode);

                    //OracleDB oraa = new OracleDB();
                    //DataTable DT = oraa.GetDataSet(sql).Tables[0];
                    // if (DT.Rows.Count > 0)
                    bool sendOK = true;
                    if (LsMail.Count() > 0)
                    {
                        foreach (EmailApplicantChange ls in LsMail.Select(x => x))
                        {
                            if (sendOK)
                            {
                                if ((ls.Email != "") && (ls.Email != null))
                                    sendOK = MailApplicantHelper.SendMailConfirmApplicant(ls);
                            }
                            ////////_EmailApplicantChange.Add(new EmailApplicantChange()
                            ////////{
                            ////////    //Email = personal.EMAIL,
                            ////////    //FullNameOld = String.Format("{0} {1} {2} {3}", oldprefix, Appchange.OLD_FNAME, "&nbsp;&nbsp;", Appchange.OLD_LNAME),
                            ////////    //FullNameNew = String.Format("{0} {1} {2} {3}", newprefix, Appchange.NEW_FNAME, "&nbsp;&nbsp;", Appchange.NEW_LNAME),
                            ////////    //OLDIDCard = Appchange.OLD_ID_CARD_NO,
                            ////////    //NewIDCard = Appchange.NEW_ID_CARD_NO,
                            ////////    //CancelReason = Appchange.CANCEL_REASON,
                            ////////    //TestingNo = Appchange.TESTING_NO,
                            ////////    //status = Appchange.STATUS,
                            ////////    //Asso = Appchange.ASSOCIATION_RESULT,
                            ////////    //OIC = Appchange.OIC_RESULT
                            ////////    //Email = DT.Rows[0]["email"].ToString(),

                            ////////     FullNameOld =  ls.FullNameOld.ToString(),



                            ////////    FullNameOld = String.Format("{0} {1} {2} {3}",  DT.Rows[0]["name"].ToString(), DT.Rows[0]["names"].ToString(), "&nbsp;&nbsp;", DT.Rows[0]["lastname"].ToString()),
                            ////////    FullNameNew = String.Format("{0} {1} {2} {3}", newprefix, Appchange.NEW_FNAME, "&nbsp;&nbsp;", Appchange.NEW_LNAME),
                            ////////    OLDIDCard=DT.Rows[0]["ID_CARD_NO"].ToString(),
                            ////////    NewIDCard = Appchange.NEW_ID_CARD_NO,
                            ////////    TestingNo = Appchange.TESTING_NO,
                            ////////    status = Appchange.STATUS,
                            ////////    Asso = Appchange.ASSOCIATION_RESULT,
                            ////////    OIC = Appchange.OIC_RESULT
                            ////////});
                        }
                        res.ResultMessage = sendOK;
                    }
                    else
                    {
                        res.ResultMessage = false;
                    }
                    ////////string[] E_mail = new string[DT.Rows.Count];
                    ////////for (int P = 0; P < DT.Rows.Count; P++)
                    ////////{
                    ////////    E_mail[P] = DT.Rows[P]["EMAIL"].ToString();
                    ////////}
                    //////////ApplicantHelper
                    ////////if (_EmailApplicantChange.Count > 0)
                    ////////{
                    ////////    MailApplicantHelper.SendMailConfirmApplicant(_EmailApplicantChange[0]);
                    ////////    //MailApplicantHelper.SendMail(String.Format("{0} {1} {2}", "", app.NAMES, personalUser.LASTNAME), personalUser.EMAIL, _emailReceiptTaskings);


                    ////////}


                }





            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_SendMailAppChange", ex);
            }
            return res;
        }


        private List<EmailApplicantChange> getSQL(short STATUS, short? ASSOCIATION_RESULT, short? OIC_RESULT, string idcard, string TestingNo, string CompCode)
        {
            List<EmailApplicantChange> LsEmail = new List<EmailApplicantChange>();
            try
            {
                string sqlComp = "";
                string sqlAsso = "";
                string sqlOIC = "";
                string sqlAgent = "";
                DAL.AG_IAS_PERSONAL_T personal = ctx.AG_IAS_PERSONAL_T.FirstOrDefault(p => p.ID == idcard);
                Int64 OLDIDCard = Convert.ToInt64(CompCode);
                Int32 Testing = Convert.ToInt32(TestingNo);
                DAL.AG_IAS_APPLICANT_CHANGE Appchange = ctx.AG_IAS_APPLICANT_CHANGE.OrderByDescending(a => a.CHANGE_ID).FirstOrDefault(a => a.OLD_ID_CARD_NO == CompCode && a.TESTING_NO == TestingNo);
                #region คำนำหน้าใหม่
                OracleDB db = new OracleDB();
                string sqlnew = "select name from VW_IAS_TITLE_NAME where id=" + Appchange.NEW_PREFIX;
                DataTable DTNEW = db.GetDataTable(sqlnew);
                DataRow DRnew = DTNEW.Rows[0];
                string newprefix = DRnew["name"].ToString();
                #endregion คำนำหน้าใหม่

                if (STATUS == 0 && ASSOCIATION_RESULT == 0 && OIC_RESULT == 0)//รอการพิจารณาจากสมาคม
                {
                    #region รอการพิจารณาจากสมาคม
                    //ส่งเมล์หาสมาคม
                    sqlAsso = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                            + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                            + ", pt.email "
                            + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                            + ",AG_ias_personal_t PT "
                            + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                            + "and d.exam_owner=PT.Comp_code "
                            + "and PT.member_type='3' "
                            + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                    #endregion รอการพิจารณาจากสมาคม

                }
                else if (STATUS == 1 && ASSOCIATION_RESULT == 1 && OIC_RESULT == 0)//รอการพิจารณาจากคปภ.
                {
                    #region รอการพิจารณาจากคปภ.
                    #region ส่งบริษัท
                    sqlComp = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                            + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                            + ", pt.email "
                            + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                            + ",AG_ias_personal_t PT "
                            + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                            + "and pt.id='" + idcard + "' "
                            + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                    #endregion ส่งบริษัท

                    #region แอดมินใหญ่
                    ///////////AR01////////
                    sqlOIC = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                            + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                            + ", pt.email "
                            + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                            + ",AG_ias_personal_t PT "
                            + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                            + "and PT.member_type='4' "
                            + "and PT.ID='130923093821787'"
                            + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                    #endregion แอดมินใหญ่

                    #region คปภ.ตัวแทน
                    //////////AR02//////////
                    sqlAgent = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                            + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                            + ", pt.email "
                            + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                            + ",AG_ias_personal_t PT "
                            + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                            + "and PT.member_type='6' "
                            + "and PT.ID='131026161815896'"
                            + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                    #endregion คปภ.ตัวแทน
                    #endregion รอการพิจารณาจากคปภ.
                }
                else if (STATUS == 1 && ASSOCIATION_RESULT == 2 && OIC_RESULT == 0)//ไม่ผ่านการพิจารณาจากสมาคม
                {
                    #region ไม่ผ่านการพิจารณาจากสมาคม
                    sqlComp = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                            + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                            + ", pt.email "
                            + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                            + ",AG_ias_personal_t PT "
                            + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                            + "and pt.id='" + idcard + "' "
                            + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                    #endregion ไม่ผ่านการพิจารณาจากสมาคม
                }
                else if (STATUS == 2 && ASSOCIATION_RESULT == 1 && OIC_RESULT == 1)//ผ่านการพิจารณาจากคปภ.
                {
                    #region ผ่านการพิจารณาจากคปภ.
                    if (Appchange.CREATE_BY == Appchange.ASSOCIATION_USER_ID)//สมาคมส่งเรื่องแก้ไข
                    {
                        #region สมาคมส่งเรื่องแก้ไข
                        #region ส่งสมาคม(1คน)
                        sqlAsso = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                               + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                               + ", pt.email "
                               + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                               + ",AG_ias_personal_t PT "
                               + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                               + "and pt.id='" + idcard + "' "
                               + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                        #endregion ส่งบริษัท
                        #endregion สมาคมส่งเรื่องแก้ไข
                    }
                    else
                    {
                        #region บริษัทส่งเรื่องแก้ไข
                        #region ส่งบริษัท
                        sqlComp = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                                + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                                + ", pt.email "
                                + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                                + ",AG_ias_personal_t PT "
                                + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                                + "and pt.id='" + idcard + "' "
                                + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                        #endregion ส่งบริษัท
                        #region ส่งสมาคม
                        sqlAsso = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                               + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                               + ", pt.email "
                               + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                               + ",AG_ias_personal_t PT "
                               + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                               + "and d.exam_owner=PT.Comp_code "
                               + "and PT.member_type='3' "
                               + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                        #endregion ส่งสมาคม
                        #endregion บริษัทส่งเรื่องแก้ไข
                    }
                    #endregion ผ่านการพิจารณาจากคปภ.
                }
                else if (STATUS == 2 && ASSOCIATION_RESULT == 1 && OIC_RESULT == 2)//ไม่ผ่านการพิจารณาจากคปภ.
                {
                    #region ไม่ผ่านการพิจารณาจากคปภ.
                    if (Appchange.CREATE_BY == Appchange.ASSOCIATION_USER_ID)//สมาคมส่งเรื่องแก้ไข
                    {
                        #region สมาคมส่งเรื่องแก้ไข
                        #region ส่งสมาคม(1คน)
                        sqlAsso = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                               + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                               + ", pt.email "
                               + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                               + ",AG_ias_personal_t PT "
                               + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                               + "and pt.id='" + idcard + "' "
                               + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                        #endregion ส่งบริษัท
                        #endregion สมาคมส่งเรื่องแก้ไข
                    }
                    else
                    {
                        #region บริษัทส่งเรื่องแก้ไข
                        #region ส่งบริษัท
                        sqlComp = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                               + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                               + ", pt.email "
                               + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                               + ",AG_ias_personal_t PT "
                               + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                               + "and pt.id='" + idcard + "' "
                               + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                        #endregion ส่งบริษัท
                        #region สมาคม
                        sqlAsso = "select A.APPLICANT_CODE,A.TESTING_NO,A.EXAM_PLACE_CODE,D.EXAM_OWNER,f.association_name as exam_place_name, A.ID_CARD_NO , "
                              + "A.INSUR_COMP_CODE, A.PRE_NAME_CODE,B.NAME,A.NAMES,A.LASTNAME,A.sex "
                              + ", pt.email "
                              + "FROM AG_APPLICANT_T A,VW_IAS_TITLE_NAME B ,ag_exam_license_r D,ag_ias_association f "
                              + ",AG_ias_personal_t PT "
                              + "WHERE A.PRE_NAME_CODE=B.ID AND a.testing_no=d.testing_no and d.exam_owner=f.association_code "
                              + "and d.exam_owner=PT.Comp_code "
                              + "and PT.member_type='3' "
                              + "and a.absent_exam is not null AND A.TESTING_NO='" + Testing + "' AND A.ID_CARD_NO='" + OLDIDCard + "' order by 1";
                        #endregion สมาคม
                        #endregion บริษัทส่งเรื่องแก้ไข
                    }

                    #endregion ไม่ผ่านการพิจารณาจากคปภ.
                }
                OracleDB oraa = new OracleDB();

                #region sqlComp
                if (sqlComp != "")
                {
                    DataTable DT = oraa.GetDataSet(sqlComp).Tables[0];
                    if (DT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in DT.Rows)
                        {
                            LsEmail.Add(new DTO.EmailApplicantChange
                            {
                                Email = dr["email"].ToString().ToString(),
                                FullNameOld = String.Format("{0} {1} {2} {3}", dr["name"].ToString(), dr["names"].ToString(), "&nbsp;&nbsp;", dr["lastname"].ToString()),
                                FullNameNew = String.Format("{0} {1} {2} {3}", newprefix, Appchange.NEW_FNAME, "&nbsp;&nbsp;", Appchange.NEW_LNAME),
                                OLDIDCard = dr["ID_CARD_NO"].ToString(),
                                NewIDCard = Appchange.NEW_ID_CARD_NO,
                                CancelReason = Appchange.CANCEL_REASON,
                                TestingNo = Appchange.TESTING_NO,
                                status = Appchange.STATUS,
                                Asso = Appchange.ASSOCIATION_RESULT,
                                OIC = Appchange.OIC_RESULT
                            });
                        }

                    }
                }
                #endregion sqlComp
                #region sqlAsso
                if (sqlAsso != "")
                {
                    DataTable DT = oraa.GetDataSet(sqlAsso).Tables[0];
                    if (DT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in DT.Rows)
                        {
                            LsEmail.Add(new DTO.EmailApplicantChange
                            {
                                Email = dr["email"].ToString().ToString(),
                                FullNameOld = String.Format("{0} {1} {2} {3}", dr["name"].ToString(), dr["names"].ToString(), "&nbsp;&nbsp;", dr["lastname"].ToString()),
                                FullNameNew = String.Format("{0} {1} {2} {3}", newprefix, Appchange.NEW_FNAME, "&nbsp;&nbsp;", Appchange.NEW_LNAME),
                                OLDIDCard = dr["ID_CARD_NO"].ToString(),
                                NewIDCard = Appchange.NEW_ID_CARD_NO,
                                CancelReason = Appchange.CANCEL_REASON,
                                TestingNo = Appchange.TESTING_NO,
                                status = Appchange.STATUS,
                                Asso = Appchange.ASSOCIATION_RESULT,
                                OIC = Appchange.OIC_RESULT
                            });
                        }

                    }
                }
                #endregion sqlAsso
                #region sqlOIC
                if (sqlOIC != "")
                {
                    DataTable DT = oraa.GetDataSet(sqlOIC).Tables[0];
                    if (DT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in DT.Rows)
                        {
                            LsEmail.Add(new DTO.EmailApplicantChange
                            {
                                Email = dr["email"].ToString().ToString(),
                                FullNameOld = String.Format("{0} {1} {2} {3}", dr["name"].ToString(), dr["names"].ToString(), "&nbsp;&nbsp;", dr["lastname"].ToString()),
                                FullNameNew = String.Format("{0} {1} {2} {3}", newprefix, Appchange.NEW_FNAME, "&nbsp;&nbsp;", Appchange.NEW_LNAME),
                                OLDIDCard = dr["ID_CARD_NO"].ToString(),
                                NewIDCard = Appchange.NEW_ID_CARD_NO,
                                CancelReason = Appchange.CANCEL_REASON,
                                TestingNo = Appchange.TESTING_NO,
                                status = Appchange.STATUS,
                                Asso = Appchange.ASSOCIATION_RESULT,
                                OIC = Appchange.OIC_RESULT
                            });
                        }

                    }
                }
                #endregion sqlOIC
                #region sqlAgent
                if (sqlAgent != "")
                {
                    DataTable DT = oraa.GetDataSet(sqlAgent).Tables[0];
                    if (DT.Rows.Count > 0)
                    {
                        foreach (DataRow dr in DT.Rows)
                        {
                            LsEmail.Add(new DTO.EmailApplicantChange
                            {
                                //FullNameOld = dr["    "].ToString(),
                                //FullNameNew = dr["    "].ToString(),
                                //Email = dr["   "].ToString().ToInt(),
                                //OLDIDCard = dr["   "].ToString().ToInt(),

                                Email = dr["email"].ToString().ToString(),
                                FullNameOld = String.Format("{0} {1} {2} {3}", dr["name"].ToString(), dr["names"].ToString(), "&nbsp;&nbsp;", dr["lastname"].ToString()),
                                FullNameNew = String.Format("{0} {1} {2} {3}", newprefix, Appchange.NEW_FNAME, "&nbsp;&nbsp;", Appchange.NEW_LNAME),
                                OLDIDCard = dr["ID_CARD_NO"].ToString(),
                                NewIDCard = Appchange.NEW_ID_CARD_NO,
                                CancelReason = Appchange.CANCEL_REASON,
                                TestingNo = Appchange.TESTING_NO,
                                status = Appchange.STATUS,
                                Asso = Appchange.ASSOCIATION_RESULT,
                                OIC = Appchange.OIC_RESULT
                            });
                        }

                    }
                }
                #endregion sqlAgent

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("ApplicantService_getSQL", ex);
            }
            return LsEmail;
        }


        public DTO.ResponseService<DataSet> GetCheckIDAppT(string idcard, string TestingNo, string CompCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetCriteria("AP.ID_CARD_NO = '{0}' AND ", idcard));
                //sb.Append(GetCriteria("AP.TESTING_NO = '{0}' AND ", testingNo));


                //if (userRegType == DTO.RegistrationType.Insurance)
                //{

                //    sb.Append(GetCriteria("AP.INSUR_COMP_CODE = '{0}' AND ", CompCode));
                //}
                string sql = string.Empty;
                string tmp = sb.ToString();
                //if (CompCode.Count() == 4)//บริษัท
                //{
                sql = "select count(*) as count from ag_applicant_t "
                    //+ "where insur_comp_code='" + CompCode + "' "
                    //+ "where upload_by_session='" + CompCode + "' "
                    + "where TESTING_NO='" + TestingNo + "' "
                    + "and ID_CARD_NO like '" + idcard + "%' order by 1";
                //}
                //else//(CompCode.Count() == 3)
                //{
                //    sql = "select count(*) as count from ag_applicant_t AppT,ag_exam_license_r LR "
                //        +"where APPT.TESTING_NO=LR.TESTING_NO "
                //        + "and LR.EXAM_OWNER='" + CompCode + "' "
                //        + "AND AppT.TESTING_NO='" + TestingNo + "' "
                //        + "and AppT.ID_CARD_NO like '" + idcard + "%' order by 1";
                //}

                OracleDB db = new OracleDB();
                DataSet ds = ds = db.GetDataSet(sql);

                res.DataResponse = ds;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetCheckIDAppT", ex);
            }

            return res;

        }

        #region ManageApplicant_page

        /// <summary>
        /// สำหรับเลือกข้อมูลว่า ต้องการจัดคนเข้าห้องสอบหรือออกจากห้องสอบ
        /// </summary>
        /// <param name="LicenseType"></param>
        /// <param name="StartExamDate"></param>
        /// <param name="EndExamDate"></param>
        /// <param name="Place"></param>
        /// <param name="PlaceName"></param>
        /// <param name="TimeExam"></param>
        /// <param name="TestingNO"></param>
        /// <param name="resultPage"></param>
        /// <param name="PAGE_SIZE"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet> getManageApplicantCourse(string LicenseType, string StartExamDate,
                                                                    string EndExamDate, string Place, string PlaceName,
                                                                    string TimeExam, string TestingNO, int resultPage, int PAGE_SIZE, Boolean Count)
        {
            DTO.ResponseService<DataSet> DS = new ResponseService<DataSet>();
            try
            {

                string sql = "";
                if (Count)
                    sql = " select count(*) CCount from ( ";
                else
                    sql = " select distinct * from ( ";
                sql = sql + "  select distinct A.TESTING_NO ,A.LICENSE_TYPE,A.EXAM_DATE,   " +
                            "    A.EXAM_TIME,A.PLACE_GROUP, A.PLACE  ,   ROW_NUMBER() OVER (ORDER BY A.EXAM_DATE) RUN_NO     " +
                            "    from (  select distinct exr.testing_no TESTING_NO ,lir.license_type_name  LICENSE_TYPE, " +
                            "    exr.testing_date EXAM_DATE,etr.test_time EXAM_TIME,    asso.association_name PLACE_GROUP, " +
                            "   expr.exam_place_name PLACE   " +
                            "    from   ag_exam_place_r EXPR, " +
                            "    ag_ias_license_type_r LIR,ag_exam_time_r ETR, " +
                            "    ag_ias_association asso,ag_exam_license_r EXR " +
                            "    left join ag_ias_subpayment_d_t DT on DT.TESTING_NO = exr.TESTING_NO " +
                            "    left join ag_ias_subpayment_h_t HT on  HT.HEAD_REQUEST_NO = DT.HEAD_REQUEST_NO " +
                            "   left join ag_ias_payment_g_t GT on GT.payment_date<GT.EXPIRATION_DATE  and  (GT.status='P' or GT.status='Z') and ht.group_request_no = gt.group_request_no " +
                            " where exr.testing_no like '" + TestingNO + "%'  and   " +
                            " exr.exam_place_code like '" + PlaceName + "%' and " +
                            " trunc(testing_date) between to_date(to_char(to_date('" + StartExamDate + "','DD/MM/RRRR', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/RRRR', ' NLS_DATE_LANGUAGE=AMERICAN'),'DD/MM/RRRR') " +
                            " and to_date(to_char(to_date('" + EndExamDate + "','DD/MM/RRRR', 'NLS_CALENDAR=''THAI BUDDHA'' NLS_DATE_LANGUAGE=THAI'),'DD/MM/RRRR', ' NLS_DATE_LANGUAGE=AMERICAN'),'DD/MM/RRRR')  " +
                           " and exr.license_type_code like '" + LicenseType + "%' and  " +
                           " exr.test_time_code like '" + TimeExam + "' and " +
                           " exr.exam_place_code = expr.exam_place_code and  " +
                           " exr.import_type = 'N'  and exr.EXAM_STATE != 'D' and " +
                           "  EXR.exam_owner like '" + Place + "%' and " +
                           " lir.license_type_code = exr.license_type_code and  " +
                           " exr.exam_owner = asso.association_code and exr.exam_owner is not null and  " + " etr.test_time_code = exr.test_time_code order by exr.testing_date asc )A";
                if (Count)
                    sql = sql + " )A ";
                else
                    sql = sql + " ) B where B.RUN_NO  between " + resultPage.StartRowNumber(PAGE_SIZE).ToString() +
                           " and " + resultPage.ToRowNumber(PAGE_SIZE).ToString() + " order by B.run_no asc";
                OracleDB ora = new OracleDB();
                DS.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetManageApplicantCourse", ex);
                DS.ErrorMsg = "ไม่สามารถทำรายการได้.";
            }
            return DS;

        }
        /// <summary>
        /// ดึงข้อมูลสนามสอบ จากสมาคมเจ้าของรอบสอบ
        /// </summary>
        /// <param name="owner">รหัสสมาคมเจ้าของ</param>
        /// <param name="license"></param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet> GetExamPlaceByLicenseAneOwner(string owner, string license)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = " select R.exam_place_code id ,PR.EXAM_PLACE_NAME || ' ['|| PV.NAME ||']' name " +
                             "    from  " +
                              "   (select exam_place_code from ag_exam_license_R  " +
                             "    where exam_owner is not null and exam_owner like '" + owner + "'  and license_type_code ='" + license + "'  " +
                             "    group by exam_place_code  order by exam_place_code asc) R,ag_exam_place_r PR, vw_ias_province PV " +
                             "    where pr.exam_place_code = r.exam_place_code and pv.id = pr.province_code order by PR.EXAM_PLACE_NAME";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetExamPlaceByLicenseAneOwner", ex);
                res.ErrorMsg = "ไม่สามารถทำรายการได้.";
            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TestingNo"></param>
        /// <param name="ConSQL"></param>
        /// <param name="resultPage"></param>
        /// <param name="PAGE_SIZE"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public DTO.ResponseService<DataSet> GetApplicantFromTestingNoForManageApplicant(string TestingNo, string ConSQL, int resultPage, int PAGE_SIZE, Boolean Count)
        {
            DTO.ResponseService<DataSet> DS = new ResponseService<DataSet>();
            try
            {
                string sql = "";
                string firstCon = "";
                string fromCon = "";
                if (Count)
                    sql = " select count(*) CCount from ( ";
                else
                    sql = " select * from ( " +
                          "   select Q.APP_CODE,Q.IDNO,Q.Name,Q.testNo,q.licensetype,q.owner,q.place,q.timer,q.ddate , q.RoomName" +
                          "  ,ROW_NUMBER() OVER (ORDER BY Q.APP_CODE) RUN_NO  " +
                          "   from ( ";


                if (ConSQL == "")
                {
                    ConSQL = " is null ";
                    firstCon = " '' RoomName ";
                }
                else
                {
                    ConSQL = " = epr.exam_room_code and epr.exam_place_code = app.exam_place_code ";
                    firstCon = " epr.exam_room_name RoomName  ";
                    fromCon = " ,ag_ias_exam_place_room EPR ";
                }
                string lastCon = " and app.testing_no = dt.testing_no " +
                                 "   and app.id_card_no = dt.id_card_no  ";

                sql = sql + " select distinct app.APPLICANT_CODE APP_CODE, app.id_card_no IDNO ,p.name ||' ' || app.names || ' ' || app.lastname Name  ,  " +
                            "     app.testing_no TestNo,tr.license_type_name LicenseType ,gr.ASSOCIATION_NAME OWNER,   " +
                            "     PR.EXAM_PLACE_NAME  Place,timer.test_time TIMeR,    lr.testing_date  DDate   , " + firstCon + "  " +
                            "  from vw_ias_title_name P, " +
                            "  ag_exam_license_r LR     ,ag_ias_license_type_r TR,ag_ias_association GR      " +
                            " , ag_exam_place_r PR, ag_exam_time_r TimeR " + fromCon + " ,  ag_applicant_t app  ,ag_ias_subpayment_d_t DT " +
                    //  "  left join ag_ias_subpayment_d_t DT   on  (dt.accounting='Y' or dt.accounting='Z') " + lastCon +
                            "  left join ag_ias_subpayment_h_t HT on HT.HEAD_REQUEST_NO = DT.HEAD_REQUEST_NO " +
                            "  left join ag_ias_payment_g_t GT on  GT.group_request_no = HT.group_request_no  " +
                            "   and GT.payment_date is not  null  " +
                            "   and (GT.status ='Z' or gt.status ='P')   " +
                            "   and  GT.payment_date<=GT.EXPIRATION_DATE " +
                            "    where app.exam_room " + ConSQL + " and app.testing_no ='" + TestingNo + "'   " +
                            " and (app.record_status != 'X' or app.record_status is null) " +
                            "     and lr.testing_no = app.testing_no  " +
                            "     and pr.exam_place_code =   lr.exam_place_code    " +
                            "     and app.exam_place_code = pr.exam_place_code      " +
                            " and (dt.accounting='Y' or dt.accounting='Z') " + lastCon +
                            "     and gr.association_code =  lr.exam_owner     and timer.test_time_code = lr.test_time_code    " +
                            "     and p.id=app.pre_name_code and tr.license_type_code = lr.license_type_code  order by app.APPLICANT_CODE   ";


                if (Count)
                    sql = sql + " )";
                else
                    sql = sql + " ) Q " +
                            " ) QQ " +
                            " where QQ.RUN_NO between " + resultPage.StartRowNumber(PAGE_SIZE).ToString() +
                            " and " + resultPage.ToRowNumber(PAGE_SIZE).ToString() + " order by QQ.RUN_NO asc";
                OracleDB ora = new OracleDB();
                DS.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("ApplicantService_GetApplicantFromTestingNoForManageApplicant", ex);
                DS.ErrorMsg = "ไม่สามารถทำรายการได้.";
            }
            return DS;
        }

        /// <summary>
        /// ดึงห้องสอบจาก testingNo มาเพื่อเอาไปใช้ในกรณี จัดเข้าห้องสอบ
        /// </summary>
        /// <param name="testingNo"></param>
        /// <returns></returns>
        public DTO.ResponseService<List<DTO.DataItem>> GetExamRoomByTestingNoforManage(string testingNo, string PlaceCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new ResponseService<List<DTO.DataItem>>();
            try
            {
                List<DTO.ManageApplicantList> MA = new List<ManageApplicantList>();
                string sql = " select distinct erl.exam_room_code RoomCode , room.exam_room_name RoomName, erl.number_seat_room numSeat " +
                             "    from  " +
                             "    ag_ias_exam_place_room room ,ag_ias_exam_room_license_r ERL   ,ag_exam_license_r LR " +
                             "    where room.exam_room_code = erl.exam_room_code " +
                             "    and room.active = 'Y' " +
                             "    and erl.active = 'Y' " +
                             "    and erl.testing_no = '" + testingNo + "' and room.exam_place_code like '" + PlaceCode + "'  and lr.exam_place_code = room.exam_place_code and lr.testing_no = erl.testing_no";
                OracleDB ora = new OracleDB();
                DataTable DT = ora.GetDataSet(sql).Tables[0];

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow dr in DT.Rows)
                    {
                        string Rname = dr["ROOMCODE"].ToString();
                        int RSeat = dr["NUMSEAT"].ToString() == "" ? 0 : dr["NUMSEAT"].ToString().ToInt();
                        int Count_Seat_now = base.ctx.AG_APPLICANT_T.Where(x => x.TESTING_NO == testingNo && x.EXAM_ROOM == Rname).Select(x => x.APPLICANT_CODE).Count();
                        int CountAppT = RSeat - Count_Seat_now;
                        //   int cappt = CountAppT;

                        MA.Add(new DTO.ManageApplicantList
                        {
                            RoomCode = dr["ROOMCODE"].ToString(),
                            RoomName = dr["ROOMNAME"].ToString() + " [ จำนวนที่นั่งคงเหลือ " + CountAppT + " ]",
                            NumSeat = dr["NUMSEAT"].ToString().ToInt(),
                        });
                    }

                    string useSQL = " select exam_room R ,count(exam_room) CC " +
                                    "  from ag_applicant_t " +
                                    "  where testing_no = '" + testingNo + "' " +
                                    "  group by exam_room  " +
                                    "  order by exam_room";
                    DataTable DTT = ora.GetDataSet(useSQL).Tables[0];
                    if (DTT.Rows.Count > 0)
                    {
                        foreach (DataRow drR in DTT.Rows)
                        {

                            var temp = MA.FirstOrDefault(x => x.RoomCode == drR["R"].ToString());
                            if (temp != null)
                            {
                                if (temp.NumSeat > drR["CC"].ToString().ToInt())
                                    temp.NumSeat = temp.NumSeat - drR["CC"].ToString().ToInt();
                                else
                                    MA.Remove(temp);
                            }
                        }
                    }

                    List<DTO.DataItem> list = new List<DTO.DataItem>();
                    MA.ForEach(l => list.Add(new DTO.DataItem
                    {
                        Id = l.RoomCode,
                        Name = l.RoomName
                    }));

                    res.DataResponse = list;
                }
                else
                {
                    res.DataResponse = null;
                }
            }
            catch
            {

            }
            return res;
        }

        public DTO.ResponseMessage<bool> SaveExamAppRoom(List<string> Manage_App, string room, string testingNo, string PaymentNo, Boolean AutoManage, string UserId)
        {
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            try
            {
                if (!AutoManage)
                {
                    //using (TransactionScope trans = new TransactionScope())
                    //{
                    Manage_App = Manage_App.OrderBy(a => a).ToList();

                    string temp = ManualSaveExamAppRoom(Manage_App, room, testingNo);
                    if (temp == "")
                    {
                        #region ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว
                        //string AddtoRoom = AddtoDBRoom(Manage_App, room, testingNo,UserId);
                        //if (AddtoRoom == "")
                        //{
                        //    res.ResultMessage = true;
                        //}
                        //else
                        //{
                        //    res.ResultMessage = false;
                        //    res.ErrorMsg = AddtoRoom;
                        //}
                        #endregion ทำเผื่อกรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว

                        res.ResultMessage = true;
                    }
                    else
                    {
                        res.ResultMessage = false;
                        res.ErrorMsg = temp;
                    }

                    //}

                }

            }
            catch (Exception ex)
            {
                res.ResultMessage = false;
                LoggerFactory.CreateLog().Fatal("ApplicantService_SaveExamAppRoom", ex);
            }
            return res;

        }
        #region ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว

        public string AddtoDBRoom(List<string> Manage_App, string room, string testingNo, string UserId)
        {
            var res = string.Empty;
            try
            {
                //var AppT = base.ctx.AG_APPLICANT_T.Where(x => x.TESTING_NO == testingNo).OrderBy(x=>x.APPLICANT_CODE);
                //   foreach (string IDCARD in Manage_App)
                //    {

                //        var Manage = AppT.FirstOrDefault(x => x.ID_CARD_NO == IDCARD && x.EXAM_ROOM != null);
                //       if(Manage!=null)
                //       {
                //           string AppCode = Manage.APPLICANT_CODE.ToString();
                //           var chkAppinPlaceCode = ctx.AG_IAS_EXAM_APPLICANT.FirstOrDefault(x => x.TESTINGNO == Manage.TESTING_NO &&
                //                                         x.APPICANT_CODE == AppCode );
                //           if (chkAppinPlaceCode == null)
                //           {

                //               AG_IAS_EXAM_APPLICANT EX = new AG_IAS_EXAM_APPLICANT();
                //               EX.EXAM_PLACE_CODE = Manage.EXAM_PLACE_CODE;
                //               EX.EXAM_ROOM_CODE = Manage.EXAM_ROOM;
                //               EX.APPICANT_CODE = AppCode;
                //               EX.USER_DATE = DateTime.Now;
                //               EX.USER_ID = UserId;
                //               EX.TESTINGNO= Manage.TESTING_NO;
                //               EX.EXAM_APPLICANT_CODE = Convert.ToString(ctx.AG_IAS_EXAM_APPLICANT.Where(c => c.TESTINGNO == Manage.TESTING_NO).Count() + 1);
                //               ctx.AG_IAS_EXAM_APPLICANT.AddObject(EX);
                //               base.ctx.SaveChanges();
                //           }
                //           else
                //           {
                //               res = res + "มีเลขที่นั่งสอบ " + AppCode + " ในสนามสอบนี้แล้วแล้ว <br/>";
                //           }
                //       }
                //       else
                //       {
                //           res = "พบข้อผิดพลาด";
                //       }

                //    }

            }
            catch (Exception ex)
            {
                //res = "ไม่สามารถบันทึกรายการดังกล่าวเข้าสู่ห้องสอบได้";
                LoggerFactory.CreateLog().Fatal("ApplicantService_AddtoDBRoom", ex);
            }
            return res;

        }
        #endregion ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว


        #region autoSaveApplicantToRoom
        ///// <summary>
        ///// จัดคนเข้าห้องสอบแบบ ออโต้
        ///// </summary>
        ///// <param name="Manage_App">รายชื่อคน ส่งมาเป็น list</param>
        ///// <param name="PaymentNo">เลขที่ใบสั่งจ่าย</param>
        ///// <returns></returns>
        //private bool AutoSaveExamAppRoom( string PaymentNo) // PaymentNo = Group_Request_No
        //{
        //    bool res = new bool();
        //    try
        //    {

        //        string sql_TESTNO_SEAT_ROOM = " select distinct lic.testing_no TESTING_NO,lic.number_seat_room SEAT, "+
        //                                                " lic.exam_room_code ROOM "+
        //                                                " from ag_ias_subpayment_d_t DT "+
        //                                                " ,ag_ias_exam_room_license_r LIC "+
        //                                                " ,ag_ias_subpayment_h_t HT "+
        //                                                " where DT.head_request_no = ht.head_request_no "+
        //                                                " and lic.active = 'Y' "+
        //                                                " and lic.testing_no = dt.testing_no "+
        //                                                " and ht.group_request_no = '"+PaymentNo+"' "+
        //                                                " order by lic.exam_room_code ";
        //        OracleDB ora = new OracleDB();
        //        DataSet testing_seat_room= ora.GetDataSet(sql_TESTNO_SEAT_ROOM);
        //        DataTable DT = ora.GetDataSet(sql_TESTNO_SEAT_ROOM).Tables[0];
        //        //List<DTO.ManageApplicantList> MA = new List<ManageApplicantList>();
        //       if (DT.Rows.Count > 0)
        //       {
        //           foreach (DataRow dr in DT.Rows)
        //           {
        //               //MA.Add(new DTO.ManageApplicantList
        //               //    {
        //               //        RoomCode = dr["ROOMCODE"].ToString(),
        //               //        TESTING_NO = dr["TESTING_NO"].ToString(),
        //               //        NumSeat = dr["NUMSEAT"].ToString().ToInt(),
        //               //    });


        //               string SQL_appT = " select distinct appt.id_card_no  " +
        //                                  " from ag_applicant_t appT " +
        //                                  " left join  ag_ias_subpayment_d_t DT on dt.id_card_no = appt.id_card_no and dt.accounting ='Y' " +
        //                                  " left join ag_ias_subpayment_h_t HT on ht.group_request_no ='" + PaymentNo + "' and dt.head_request_no=ht.head_request_no " +
        //                                  " where appT.testing_no='" + dr["TESTING_NO"].ToString() + "'  " +
        //                                  " and appT.exam_room is null group by appt.id_card_no order by  appt.id_card_no";  
        //                                    // หาคนที่สอบในรอบนั้น เรียงตามวันที่สมัครเพื่อจัดที่
        //               DataTable DTappT =  ora.GetDataSet(SQL_appT).Tables[0];
        //               if (DTappT.Rows.Count > 0)
        //               {
        //                   Boolean HaveFreeSEAT = true;
        //                   foreach (DataRow drAPPT in DTappT.Rows)
        //                   {
        //                       if (HaveFreeSEAT)
        //                       {
        //                           int applicantT = base.ctx.AG_APPLICANT_T.Where(x => x.TESTING_NO == dr["TESTING_NO"].ToString()
        //                                                                        && x.EXAM_ROOM == dr["ROOMCODE"].ToString()).Count();

        //                           if (dr["NUMSEAT"].ToString().ToInt() > applicantT)//ถ้าที่นั่งสอบมากกว่าคนที่สอบรอบนั้นและอยู่ในห้องนั้น (มีที่นั่งเหลือ)
        //                           {
        //                               var UpdateApp = base.ctx.AG_APPLICANT_T.FirstOrDefault(x => x.ID_CARD_NO == drAPPT["ID_CARD_NO"].ToString()
        //                                                    && x.TESTING_NO == dr["TESTING_NO"].ToString()
        //                                                    && x.EXAM_ROOM == null);
        //                               UpdateApp.EXAM_ROOM = dr["ROOMCODE"].ToString();
        //                               base.ctx.SaveChanges();
        //                           }
        //                           else
        //                           {
        //                               HaveFreeSEAT = false;
        //                           }
        //                       }
        //                       else
        //                       {
        //                           break;   
        //                       }
        //                   }
        //               }
        //           }

        //       }


        //       res = true;

        //    }
        //    catch(Exception ex)
        //    {
        //        res = false;
        //        LoggerFactory.CreateLog().Fatal("ApplicantService_AutoSaveExamAppRoom", ex);
        //    }
        //    return res;
        //}
        #endregion autoSaveApplicantToRoom


        /// <summary>
        /// จัดคนเข้าห้องสอบแบบ จัดมือ
        /// </summary>
        /// <param name="Manage_App">รายชื่อคนแบบ list</param>
        /// <param name="room">ห้องสอบที่เลือกให้เข้า</param>
        /// <param name="testingNo">รหัสรอบสอบ</param>
        /// <returns></returns>
        private string ManualSaveExamAppRoom(List<string> Manage_App, string room, string testingNo)
        {
            string res = "";
            try
            {
                
                var temp = base.ctx.AG_IAS_EXAM_ROOM_LICENSE_R.FirstOrDefault(x => x.TESTING_NO == testingNo && x.EXAM_ROOM_CODE == room && x.ACTIVE == "Y");
                int NUMSEAT = temp.NUMBER_SEAT_ROOM.ToString().ToInt();
                var TestNo = base.ctx.AG_APPLICANT_T.Where(x => x.TESTING_NO == testingNo && (x.RECORD_STATUS != "X" || x.RECORD_STATUS == null) && x.CANCEL_REASON == null);
                int OldAppInRoom = TestNo.Where(x => x.EXAM_ROOM == room).Count();
                int applicantT = OldAppInRoom + Manage_App.Count();
                if (TestNo != null)
                {
                    if (NUMSEAT >= applicantT)//ถ้าที่นั่งสอบมากกว่าคนที่สอบรอบนั้นและอยู่ในห้องนั้น (มีที่นั่งเหลือ)
                    {

                        foreach (string AppCode in Manage_App)
                        {
                            int IAppCode = AppCode.ToInt();
                            var Manage = TestNo.FirstOrDefault(x => x.APPLICANT_CODE == IAppCode
                                                                            && x.EXAM_ROOM == null);
                            if (Manage != null)
                            {
                                Manage.EXAM_ROOM = room;
                                base.ctx.SaveChanges();
                            }
                        }

                        res = "";
                    }
                    else
                    {
                        res = "จำนวนคนที่เลือกมีมากกว่าที่นั่งสอบคงเหลือในห้อง " + room + "  มีที่นั่งสอบคงเหลือ " + NUMSEAT;
                    }
                }

            }
            catch (Exception ex)
            {
                res = "พบข้อผิดพลาด";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ManualSaveExamAppRoom", ex);
            }

            #region ส่งอีเมลล์ไปยังบริษัทเมื่อมีการจัดคนเข้าห้องสอบ
            try
            {
                if (res == "")
                {

                    string sqlEmail = string.Empty;
                    string sqlConEmail = string.Empty;
                    string sqlData = string.Empty;
                    OracleDB ora = new OracleDB();
                    int i = 0;
                    foreach (string idc in Manage_App)
                    {
                        sqlConEmail += string.Format("'{0}'{1}", idc.Trim(), (i < Manage_App.Count - 1) ? ", " : "");
                        i++;
                    }

                    sqlEmail = string.Format(
                                        " select distinct EMAIL from AG_IAS_PERSONAL_T "
                                        + " where MEMBER_TYPE = '2' and EMAIL is not null and STATUS != '7' "
                                        + " and COMP_CODE in( "
                                        + "      select distinct INSUR_COMP_CODE  "
                                        + "      from AG_APPLICANT_T  "
                                        + "      where TESTING_NO = '{0}' and INSUR_COMP_CODE is not null and ID_CARD_NO in ({1})) "
                                        , testingNo.Trim()
                                        , sqlConEmail
                                        );
                    DataTable dt = ora.GetDataTable(sqlEmail);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sqlData = string.Format(
                                    " select distinct A.TESTING_NO, A.TESTING_DATE, B.TEST_TIME, D.EXAM_ROOM_NAME "
                                    + " from AG_EXAM_LICENSE_R A, AG_EXAM_TIME_R B, AG_APPLICANT_T C, AG_IAS_EXAM_PLACE_ROOM D "
                                    + " where A.TEST_TIME_CODE = B.TEST_TIME_CODE and A.TESTING_NO = C.TESTING_NO  "
                                    + "   and C.EXAM_ROOM = D.EXAM_ROOM_CODE and A.EXAM_PLACE_CODE = D.EXAM_PLACE_CODE "
                                    + "   and A.TESTING_NO = '{0}' and C.EXAM_ROOM = '{1}' "
                                    , testingNo.Trim(), room.Trim());

                        DTO.EmailAddOwnerToExamRoom DataEmail = base.ctx.ExecuteStoreQuery<DTO.EmailAddOwnerToExamRoom>(sqlData).First();

                        if (DataEmail != null)
                        {
                            DataEmail.EMAIL = new List<string>();
                            foreach (DataRow row in dt.Rows)
                                DataEmail.EMAIL.Add(row["EMAIL"].ToString());
                            MailApplicantHelper.SendMailAddOwnerToExamRoom(DataEmail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorLog = string.Format("เกิดข้อผิดพลาดในส่งอีเมลล์ไปยังบริษัทเมื่อมีการจัดคนเข้าห้องสอบ รหัสรอบสอบ:{0} ห้องสอบ:{1} ", testingNo, room);
                LoggerFactory.CreateLog().Fatal(errorLog + "[ApplicantService_ManualSaveExamAppRoom]", ex);
            }
            #endregion

            return res;
        }

        public DTO.ResponseMessage<bool> CancleExamApplicantManage(List<string> Manage_App, string testingNo)
        {
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            try
            {
                //using (TransactionScope trans = new TransactionScope())
                //{

                var TestNO = base.ctx.AG_APPLICANT_T.Where(x => x.TESTING_NO == testingNo);
                foreach (string AppCode in Manage_App)
                {
                    int IAppCode = AppCode.ToInt();
                    var temp = TestNO.FirstOrDefault(x => x.APPLICANT_CODE == IAppCode);

                    if (temp != null)
                    {
                        #region ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว
                        //string CancleAppinRoom = getOut(temp);
                        //if (CancleAppinRoom == string.Empty)
                        //{
                        //    temp.EXAM_ROOM = null;
                        //}
                        //else
                        //{
                        //    res.ErrorMsg = CancleAppinRoom;
                        //}
                        temp.EXAM_ROOM = null;
                        base.ctx.SaveChanges();
                        #endregion ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว
                    }
                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
                //}
            }
            catch (Exception ex)
            {
                res.ResultMessage = false;
                LoggerFactory.CreateLog().Fatal("ApplicantServiceCancleExamApplicantManage", ex);
            }
            return res;

        }
        #region ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว

        private string getOut(AG_APPLICANT_T temp)
        {
            string res = string.Empty;
            try
            {
                //    string AppCode = temp.APPLICANT_CODE.ToString();
                //    AG_IAS_EXAM_APPLICANT ExamRoom = ctx.AG_IAS_EXAM_APPLICANT.FirstOrDefault(x => x.APPICANT_CODE == AppCode
                //                                            && x.TESTINGNO == temp.TESTING_NO && x.EXAM_ROOM_CODE == temp.EXAM_ROOM
                //                                            && x.EXAM_PLACE_CODE == temp.EXAM_PLACE_CODE);
                //    ctx.AG_IAS_EXAM_APPLICANT.DeleteObject(ExamRoom);
            }
            catch
            {

                res = "พบข้อผิดพลาด";
            }
            return res;
        }
        #endregion ทำเผื่อไว้กรณีที่ต้องลงตารางที่นั่งสอบในแต่ละห้องว่าใครนั่งที่นั่งไหน แต่คอมเม้นท์ไว้ก่อนเพราะไม่อย่างนั้นมันช้า และ user ยังไม่ได้ต้องการ ถ้าจะเอาแค่มาเอาคอมเม้นท์ออกก็ใช้ได้แล้ว


        public string GetQuantityBillPerPageByConfig()
        {
            string strAmount = string.Empty;
            try
            {
                var Manual = ctx.AG_IAS_CONFIG.FirstOrDefault(x => x.ID == "08");
                strAmount = Manual.ITEM_VALUE;
            }
            catch (Exception ex)
            {
                strAmount = "";
            }
            return strAmount;

        }


        #endregion ManageApplicant_page





        public DTO.ResponseMessage<bool> CheckApplicantIsDuplicate(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode, string examPlaceCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            res.ResultMessage = false;
            try
            {
                DateTime dtToday = DateTime.Now.Date;

                var ent1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == TestingNo && w.APPLY_DATE == dtToday && w.ID_CARD_NO == idcard);
                if (ent1 != null)
                {
                    res.ResultMessage = true;
                }

                var ent2 = from a in ctx.AG_APPLICANT_T
                           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                           where e.TESTING_DATE == testTingDate && e.TEST_TIME_CODE == testTimeCode && a.APPLY_DATE == dtToday
                            && a.ID_CARD_NO == idcard
                           select a;
                if (ent2.ToList().Count() > 0)
                {
                    res.ResultMessage = true;
                }


                var ent3 = from a in ctx.AG_APPLICANT_T
                           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                           where e.EXAM_PLACE_CODE == examPlaceCode && e.TEST_TIME_CODE == testTimeCode && a.APPLY_DATE == dtToday
                           && a.ID_CARD_NO == idcard
                           select a;
                if (ent3.ToList().Count() > 0)
                {
                    res.ResultMessage = true;
                }

                var ent4 = from a in ctx.AG_APPLICANT_T
                           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                           where e.TESTING_DATE == testTingDate && e.TEST_TIME_CODE == testTimeCode && a.APPLY_DATE == dtToday
                           && a.ID_CARD_NO == idcard
                           select a;
                if (ent4.ToList().Count > 0)
                {

                    //res.ResultMessage = true;
                    //AG_EXAM_TIME_R lst = from elt in ctx.AG_EXAM_TIME_R
                    //          join e in ctx.AG_EXAM_LICENSE_R on elt.TEST_TIME_CODE equals e.TEST_TIME_CODE
                    //          join a in ctx.AG_APPLICANT_T on e.TESTING_NO equals a.TESTING_NO
                    //          where a.TESTING_NO == "" e.TESTING_DATE == testTingDate && e.TEST_TIME_CODE == testTimeCode && a.APPLY_DATE == dtToday
                    //          && a.ID_CARD_NO == idcard
                    //          select a;


                    //lst.for
                }


            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_CheckApplicantIsDuplicate", ex);
            }
            return res;
        }


        public DTO.ResponseService<List<string>> CheckApplicantExamDup(string idcard)
        {
            var res = new DTO.ResponseService<List<string>>();
            try
            {
                var ent = (from ap in ctx.AG_APPLICANT_T
                           where ap.ID_CARD_NO.Trim() == idcard.Trim()
                           && ap.RECORD_STATUS == "D"
                           select ap.TESTING_NO).ToList();


                res.DataResponse = ent;
            }
            catch (Exception ex)
            {

                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_CheckApplicantExamDup", ex);
            }


            return res;
        }

        public DTO.ResponseMessage<bool> ValidateApplicantSingleBeforeSubmit(ValidateApplicantSingleBeforeSubmitRequest request)
        {
            var res = new DTO.ResponseMessage<bool>();
            res.ResultMessage = false;

            IEnumerable<string> appTestingNo = request.Applicants.Select(tsn => tsn.TESTING_NO);

            try
            {
                request.Applicants.ToList().ForEach(x =>
               {
                   DateTime dtToday = DateTime.Now.Date;

                   var examLicense = base.ctx.AG_EXAM_LICENSE_R.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE);


                   var ent1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == x.TESTING_NO && w.APPLY_DATE != x.APPLY_DATE && w.ID_CARD_NO == x.ID_CARD_NO);
                   if (ent1 != null)
                   {
                       res.ResultMessage = true;
                   }

                   var ent2 = from a in ctx.AG_APPLICANT_T
                              join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                              where e.TESTING_DATE == x.TESTING_DATE && e.TEST_TIME_CODE == examLicense.TEST_TIME_CODE
                              && a.APPLY_DATE != x.APPLY_DATE
                              && a.ID_CARD_NO == x.ID_CARD_NO
                              && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                              select a;
                   if (ent2.ToList().Count() > 0)
                   {
                       res.ResultMessage = true;
                   }


                   var ent3 = from a in ctx.AG_APPLICANT_T
                              join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                              where e.EXAM_PLACE_CODE == x.EXAM_PLACE_CODE && e.TEST_TIME_CODE == examLicense.TEST_TIME_CODE
                              && a.APPLY_DATE != x.APPLY_DATE
                              && a.ID_CARD_NO == x.ID_CARD_NO
                              && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                              select a;
                   if (ent3.ToList().Count() > 0)
                   {
                       res.ResultMessage = true;
                   }

                   var ent4 = from a in ctx.AG_APPLICANT_T
                              join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                              where e.TESTING_DATE == x.TESTING_DATE && a.APPLY_DATE == dtToday
                              && a.ID_CARD_NO == x.ID_CARD_NO
                              && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                              select a;
                   if (ent4.ToList().Count > 0)
                   {
                       res.ResultMessage = true;
                   }

                   var ent5 = from a in ctx.AG_APPLICANT_T
                              join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                              where e.TESTING_DATE == x.TESTING_DATE && a.APPLY_DATE != x.TESTING_DATE
                              && a.ID_CARD_NO == x.ID_CARD_NO
                              && a.TESTING_NO == x.TESTING_NO && e.TESTING_NO == x.TESTING_NO
                              select a;
                   if (ent5.ToList().Count > 0)
                   {
                       res.ResultMessage = true;
                   }


                   //var ent6 = from a in ctx.AG_APPLICANT_T
                   //           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                   //           join etr in ctx.AG_EXAM_TIME_R on e.TEST_TIME_CODE equals etr.TEST_TIME_CODE
                   //           where e.TESTING_DATE == x.TESTING_DATE
                   //           && a.ID_CARD_NO == x.ID_CARD_NO
                   //           select a;
                   //if (ent6.ToList().Count > 0)
                   //{
                   //    ent6.ToList().ForEach(chk =>
                   //    {
                   //        AG_EXAM_TIME_R entTime = base.ctx.AG_EXAM_TIME_R.FirstOrDefault(s => s.TEST_TIME_CODE == examLicense.TEST_TIME_CODE);
                   //        if (entTime != null)
                   //        {
                   //            int startTime1 =Convert.ToInt32(entTime.START_TIME);
                   //             int endTime1 =Convert.ToInt32(entTime.END_TIME);
                   //            int startTime2 = Convert.ToInt32(examLicense
                   //             if (startTime)
                   //            {

                   //            }
                   //        }
                   //        //apply = chk.EXAM_APPLY == null ? "0".ToShort() : chk.EXAM_APPLY.Value;
                   //        //admission = entSeat.SEAT_AMOUNT == null ? "0".ToShort() : entSeat.SEAT_AMOUNT.Value;

                   //        //int remain = admission - apply;
                   //        //lsRemain.Add(remain);
                   //    });
                   //}


               });

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ValidateApplicantSingleBeforeSubmit", ex);
            }
            return res;

        }

        public DTO.ResponseMessage<bool> ValidateApplicantTestCenter(string TestingNo, string idcard, DateTime testTingDate, string testTimeCode, string examPlaceCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            res.ResultMessage = false;
            try
            {
                DateTime dtToday = DateTime.Now.Date;

                var examLicense = base.ctx.AG_IAS_PERSONAL_T.SingleOrDefault(w => w.ID_CARD_NO == idcard && w.MEMBER_TYPE == "7" && (w.STATUS == "2" || w.STATUS == "5"));

                if (examLicense != null)
                {

                    var testCenter = from lr in ctx.AG_EXAM_LICENSE_R
                                     join epr in ctx.AG_EXAM_PLACE_R on lr.EXAM_PLACE_CODE equals epr.EXAM_PLACE_CODE
                                     join epgr in ctx.AG_EXAM_PLACE_GROUP_R on epr.EXAM_PLACE_GROUP_CODE equals epgr.EXAM_PLACE_GROUP_CODE
                                     where epgr.EXAM_PLACE_GROUP_CODE == examLicense.COMP_CODE && lr.TESTING_NO == TestingNo
                                     select lr;

                    res.ResultMessage = true;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ValidateApplicantTestCenter", ex);
            }
            return res;
        }


        public ResponseMessage<bool> IsPersonCanApplicant(IsPersonCanApplicantRequest request)
        {
            ResponseMessage<bool> response = new ResponseMessage<bool>();

            // เขียนโปรแกรมตรวจสอบ ด้านล่าง
            response.ResultMessage = true;



            return response;
        }

        public DTO.ResultValidateApplicant ValidateApplicantBeforeSaveList(ValidateApplicantBeforeSaveListRequest request)
        {
            var res = new DTO.ResultValidateApplicant();
            try
            {
                DateTime applyDate = DateTime.Now.Date;
                res.IsCanExam = true;
                #region ตรวจสอบซ้ำ แจ้งเตือนสอบไม่ได้

                //ตรวจสอบการสมัครสอบ ในรอบที่เป็นเจ้าหน้าที่
                var examLicense = base.ctx.AG_IAS_PERSONAL_T.SingleOrDefault(w => w.ID_CARD_NO == request.IdCard && w.MEMBER_TYPE == "7" && (w.STATUS == "2" || w.STATUS == "5"));
                //IEnumerable<AG_EXAM_LICENSE_R> testCenter;
                if (examLicense != null)
                {
                    IEnumerable<AG_EXAM_LICENSE_R> testCenter = from lr in ctx.AG_EXAM_LICENSE_R
                                                                join epr in ctx.AG_EXAM_PLACE_R on lr.EXAM_PLACE_CODE equals epr.EXAM_PLACE_CODE
                                                                join epgr in ctx.AG_EXAM_PLACE_GROUP_R on epr.EXAM_PLACE_GROUP_CODE equals epgr.EXAM_PLACE_GROUP_CODE
                                                                where epgr.EXAM_PLACE_GROUP_CODE == examLicense.COMP_CODE && lr.TESTING_NO == request.TestingNo
                                                                select lr;
                    if (testCenter != null)
                    {
                        if (testCenter.ToList().Count > 0)
                        {
                            res.ValidateMessage = "ไม่สามารถสอบได้ เนื่องจากเป็นเจ้าที่กลุ่มสนามสอบของรอบสอบนี้";
                            res.IsConfirm = false;
                            res.IsCanExam = false;
                            return res;

                        }
                    }

                }

                //รอบสอบเดียวกัน คนเดียวกัน วันที่สมัครวันเดียวกัน
                AG_APPLICANT_T dup1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == request.TestingNo && w.APPLY_DATE == applyDate && w.ID_CARD_NO == request.IdCard);

                //วันที่สอบเดียวกัน เวลาเดียวกัน คนเดียวกัน วันที่สมัครวันเดียวกัน
                IEnumerable<AG_APPLICANT_T> dup2 = from a in ctx.AG_APPLICANT_T
                                                   join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                   where e.TESTING_DATE == request.TestingDate && e.TEST_TIME_CODE == request.TestTimeCode && a.APPLY_DATE == applyDate
                                                    && a.ID_CARD_NO == request.IdCard
                                                   select a;

                //วันที่สอบเดียวกัน เวลาเดียวกัน สนามสอบเดียวกัน คนเดียวกัน วันที่สมัครวันเดียวกัน
                IEnumerable<AG_APPLICANT_T> dup3 = from a in ctx.AG_APPLICANT_T
                                                   join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                   where e.TESTING_DATE == request.TestingDate && e.EXAM_PLACE_CODE == request.ExamPlaceCode && e.TEST_TIME_CODE == request.TestTimeCode && a.APPLY_DATE == applyDate
                                                   && a.ID_CARD_NO == request.IdCard
                                                   select a;

                // ตรวจสอบรายการสอบซ้ำจากรายการที่เลือกทั้งหมด
                IEnumerable<DTO.AddApplicant> lstApp = request.AddApplicants.Where(r => r.ExamDate == request.TestingDate && r.ExamTime == request.Time);
                if (dup1 != null || dup2.Count() > 0 || dup3.Count() > 0 || lstApp.Count() > 1)
                {
                    res.ValidateMessage = "ได้มีการสมัครในวันและเวลาสอบนี้แล้วไม่สามารถสมัครสอบได้ กรุณาทำการสมัครสอบในวันพรุ่งนี้";
                    res.IsConfirm = false;
                    res.IsCanExam = false;
                    return res;
                }

                // จำนวนคนเกินรอบสอบ
                var examL = base.ctx.AG_EXAM_LICENSE_R.SingleOrDefault(w => w.TESTING_NO == request.TestingNo && w.EXAM_PLACE_CODE == request.ExamPlaceCode);
                if (examL != null)
                {
                    if (examL.EXAM_PLACE_CODE != null)
                    {
                        Int32 apply = examL.EXAM_APPLY == null ? 0 : examL.EXAM_APPLY.Value;
                        Int32 admission = examL.EXAM_ADMISSION == null ? 0 : examL.EXAM_ADMISSION.Value;

                        Int32 remain = admission - apply;
                        res.ResultMessage = (remain >= 1);
                        if (res.ResultMessage == false)
                        {
                            res.ValidateMessage = "ห้องเต็ม";
                            res.IsConfirm = false;
                            res.IsCanExam = false;
                            return res;
                        }
                    }
                }

                #endregion

                #region ตรวจสอบซ้ำ ยืนยันสมัครสอบได้

                var newexamtime = ctx.AG_EXAM_TIME_R.FirstOrDefault(x => x.TEST_TIME_CODE == request.TestTimeCode);
                foreach (var item in request.AddApplicants.Where(x => x.ExamDate == request.TestingDate))
                {
                    var objecttieme = ctx.AG_EXAM_TIME_R.FirstOrDefault(x => x.TEST_TIME_CODE == item.TestTimeCode);
                    if (newexamtime.START_TIME.ToFloat() >= objecttieme.START_TIME.ToFloat() && newexamtime.START_TIME.ToFloat() <= objecttieme.END_TIME.ToFloat())
                    {
                        res.ValidateMessage = "ทำรายการสมัครสอบในวันที่ " + request.TestingDate.ToString("dd/MM/yyyy") + " เวลาคร่อมกัน กรุณายืนยันเพื่อทำรายการสมัครสอบ";
                        res.IsConfirm = true;
                        return res;
                    }

                    if (newexamtime.END_TIME.ToFloat() >= objecttieme.START_TIME.ToFloat() && newexamtime.END_TIME.ToFloat() <= objecttieme.END_TIME.ToFloat())
                    {
                        res.ValidateMessage = "ทำรายการสมัครสอบในวันที่ " + request.TestingDate.ToString("dd/MM/yyyy") + " เวลาคร่อมกัน กรุณายืนยันเพื่อทำรายการสมัครสอบ";
                        res.IsConfirm = true;
                        return res;
                    }
                }

                //รอบสอบเดียวกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน
                AG_APPLICANT_T confirm1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == request.TestingNo && w.APPLY_DATE != applyDate && w.ID_CARD_NO == request.IdCard);
                if (confirm1 != null)
                {
                    res.ValidateMessage = "ทำรายการสมัครสอบในรอบสอบ " + request.TestingNo + " วันที่ " + request.TestingDate.ToString("dd/MM/yyyy") + " เวลา " + request.Time + " แล้ว" + " กรุณายืนยันเพื่อทำรายการ";
                    res.IsConfirm = true;
                    res.IsCanExam = true;
                    return res;
                }


                //วันที่สอบเดียวกัน เวลาเดียวกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน
                IEnumerable<AG_APPLICANT_T> confirm2 = from a in ctx.AG_APPLICANT_T
                                                       join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                       where e.TESTING_DATE == request.TestingDate && e.TEST_TIME_CODE == request.TestTimeCode
                                                       && a.APPLY_DATE != applyDate
                                                       && a.ID_CARD_NO == request.IdCard
                                                       select a;

                //วันที่สอบเดียวกัน เวลาเดียวกัน สนามสอบเดียวกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน
                IEnumerable<AG_APPLICANT_T> confirm3 = from a in ctx.AG_APPLICANT_T
                                                       join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                       where e.TESTING_DATE == request.TestingDate
                                                       && e.TEST_TIME_CODE == request.TestTimeCode
                                                       && e.EXAM_PLACE_CODE == request.ExamPlaceCode
                                                       && a.ID_CARD_NO == request.IdCard
                                                       && a.APPLY_DATE != applyDate
                                                       select a;
                if (confirm2.Count() > 0 || confirm3.Count() > 0)
                {
                    res.ValidateMessage = "ทำรายการสมัครสอบในวันที่ " + request.TestingDate.ToString("dd/MM/yyyy") + " เวลา " + request.Time + " แล้ว" + " กรุณายืนยันเพื่อทำรายการ";
                    res.IsConfirm = true;
                    res.IsCanExam = true;
                    return res;
                }

                //วันที่สอบเดียวกัน เวลาคร่อมกัน คนเดียวกัน วันที่สมัครไม่ใช่วันเดียวกัน

                //หาช่วงเวลาที่จะสอบ
                AG_EXAM_TIME_R examTime = base.ctx.AG_EXAM_TIME_R.SingleOrDefault(w => w.TEST_TIME_CODE == request.TestTimeCode && w.ACTIVE == "Y");

                if (examTime != null)
                {
                    decimal eStartTime2 = 0;
                    decimal eEndTime2 = 0;

                    if (examTime.START_TIME != null)
                    {
                        eStartTime2 = Convert.ToDecimal(examTime.START_TIME);
                    }
                    if (examTime.END_TIME != null)
                    {
                        eEndTime2 = Convert.ToDecimal(examTime.END_TIME);
                    }

                    //หาข้อมูลเวลาทั้งหมดใส่ลง List เพื่อเทียบกับเวลาที่จะสอบ
                    IEnumerable<AG_EXAM_TIME_R> lsTime = from etr in ctx.AG_EXAM_TIME_R
                                                         join el in ctx.AG_EXAM_LICENSE_R on etr.TEST_TIME_CODE equals el.TEST_TIME_CODE
                                                         join app in ctx.AG_APPLICANT_T on el.TESTING_NO equals app.TESTING_NO
                                                         where app.ID_CARD_NO == request.IdCard && el.TESTING_DATE == request.TestingDate
                                                         select etr;

                    List<ExamTime> lsExamTime = new List<ExamTime>();
                    if (lsTime != null)
                    {

                        lsTime.ToList().ForEach(
                            x =>
                            {
                                ExamTime exam = new ExamTime();
                                exam.TEST_TIME_CODE = x.TEST_TIME_CODE;
                                exam.START_TIME = Convert.ToDecimal(x.START_TIME);
                                exam.END_TIME = Convert.ToDecimal(x.END_TIME);
                                lsExamTime.Add(exam);
                            });

                    }

                    //ตรวจสอบข้อมูลทั้งหมดใน List ว่า มีคร่อมเวลาหรือไม่
                    if (lsExamTime.ToList().Count > 0)
                    {
                        IEnumerable<ExamTime> acrossTimeCase1 = lsExamTime.Where(w => w.START_TIME <= eStartTime2 && w.END_TIME <= eEndTime2 && w.END_TIME >= eStartTime2);
                        IEnumerable<ExamTime> acrossTimeCase2 = lsExamTime.Where(w => w.START_TIME >= eStartTime2 && w.END_TIME >= eEndTime2 && w.START_TIME <= eEndTime2);
                        IEnumerable<ExamTime> acrossTimeCase3 = lsExamTime.Where(w => w.START_TIME >= eStartTime2 && w.END_TIME <= eEndTime2);
                        IEnumerable<ExamTime> acrossTimeCase4 = lsExamTime.Where(w => w.START_TIME <= eStartTime2 && w.END_TIME >= eEndTime2);
                        IEnumerable<ExamTime> acrossTimeCase5 = lsExamTime.Where(w => w.START_TIME == eStartTime2 && w.END_TIME == eEndTime2);
                        if (acrossTimeCase1.ToList().Count > 0 ||
                            acrossTimeCase2.ToList().Count > 0 ||
                            acrossTimeCase3.ToList().Count > 0 ||
                            acrossTimeCase4.ToList().Count > 0 ||
                            acrossTimeCase5.ToList().Count > 0)
                        {
                            res.ValidateMessage = "ทำรายการสมัครสอบในวันที่ " + request.TestingDate.ToString("dd/MM/yyyy") + " เวลาคร่อมกัน กรุณายืนยันเพื่อทำรายการสมัครสอบ";
                            res.IsConfirm = true;
                            res.IsCanExam = true;
                            return res;
                        }
                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("ApplicantService_ValidateApplicantBeforeSaveList", ex);
            }
            return res;
        }


    }

}
