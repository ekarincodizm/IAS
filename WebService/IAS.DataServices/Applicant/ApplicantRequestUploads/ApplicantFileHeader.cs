using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.License.LicenseRequestUploads;
using IAS.DTO;
using System.Text;
using IAS.DataServices.License.Mapper;
using IAS.DataServices.Applicant.ApplicantRequestUploads;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Applicant.ApplicantHelper;
using IAS.DataServices.Applicant.Mapper;
using IAS.DataServices.Properties;

namespace IAS.DataServices.Applicant.ApplicantRequestUploads
{
    public class ApplicantFileHeader : AG_IAS_APPLICANT_HEADER_TEMP
    {
        // ต้องหาวิธี กันกรณีเปลี่ยน id

        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;
        private IList<ApplicantFileDetail> _applicantFileDetails = new List<ApplicantFileDetail>();
        private String _filename;
        private DTO.RegistrationType _sourceFrom;
        private AG_IAS_LICENSE_TYPE_R _licenseType;
        private AG_EXAM_TIME_R _examTime;
        private AG_EXAM_PLACE_R _examPlace;
        private AG_EXAM_LICENSE_R _examLicense;
        private String _testingNumber;
        private String _examPlaceCode;
        private String _LicenseTypeCode;
        private String _TestTimeCode;
        private DateTime _TestTingDate;
        private DateTime _applyDate;
        private DTO.UserProfile _createBy;
        private bool flagRoomExam = true;

        private List<AG_APPLICANT_T> _applicantRegisted = new List<AG_APPLICANT_T>();


        public ApplicantFileHeader()
        {

            flagRoomExam = true;
        }
        public ApplicantFileHeader(ApplicantHeaderRequest request)
        {
            flagRoomExam = true;
            _applyDate = DateTime.Now;
            _ctx = request.Context;
            UPLOAD_GROUP_NO = OracleDB.GetGenAutoId();
            SOURCE_TYPE = GetSourceType(request.UserProfile);
            _testingNumber = request.TestingNumber;
            _examPlaceCode = request.ExamPlaceCode;

            _createBy = request.UserProfile;

            PROVINCE_CODE = request.LineData.GetIndexOf(1);
            //EXAM_PLACE_CODE = request.LineData.GetIndexOf(1);
            COMP_CODE = request.LineData.GetIndexOf(2);
            LICENSE_TYPE_CODE = request.LineData.GetIndexOf(3);
            TESTING_DATE = PhaseDateHelper.PhaseToDate(request.LineData.GetIndexOf(4));
            EXAM_APPLY = PhaseApplyAmountHelper.Phase(request.LineData.GetIndexOf(5));
            EXAM_AMOUNT = PhaseCurrencyAmount.Phase(request.LineData.GetIndexOf(6));
            TEST_TIME_CODE = request.LineData.GetIndexOf(7);
            FILENAME = request.FileName;
            this._examLicense = CTX.AG_EXAM_LICENSE_R.SingleOrDefault(w => w.TESTING_NO == _testingNumber && w.EXAM_PLACE_CODE == _examPlaceCode);
            this._examPlace = _ctx.AG_EXAM_PLACE_R.SingleOrDefault(w => w.EXAM_PLACE_CODE == _examPlaceCode);
            this._examTime = _ctx.AG_EXAM_TIME_R.SingleOrDefault(w => w.TEST_TIME_CODE == _examLicense.TEST_TIME_CODE);
            this._licenseType = _ctx.AG_IAS_LICENSE_TYPE_R.SingleOrDefault(w => w.LICENSE_TYPE_CODE == LICENSE_TYPE_CODE);

            //if (this.SourceFrom == DTO.RegistrationType.Association)
            //{
            //    //this._examLicense = _ctx.AG_EXAM_LICENSE_R.SingleOrDefault(e => e.EXAM_PLACE_CODE == _examPlaceCode
            //    //                                                            && e.TESTING_DATE == _TestTingDate
            //    //                                                            && e.TEST_TIME_CODE == _TestTimeCode
            //    //                                                            && e.LICENSE_TYPE_CODE == _LicenseTypeCode);
            //}
            //else if (this.SourceFrom == DTO.RegistrationType.Insurance)
            //{
            //    //this._examLicense = _ctx.AG_EXAM_LICENSE_R.SingleOrDefault(e => e.TESTING_NO == _testingNumber && e.EXAM_PLACE_CODE == _examPlaceCode);
            //}

        }

        public IEnumerable<AG_APPLICANT_T> ApplicantRegisted
        {
            get
            {


                if (flagRoomExam)
                {
                    if (this.ExamLicense != null)
                    {
                        _applicantRegisted = _ctx.AG_APPLICANT_T.Where(a => a.TESTING_NO == this.ExamLicense.TESTING_NO && a.EXAM_PLACE_CODE == this.ExamLicense.EXAM_PLACE_CODE).ToList();
                        //_applicantRegisted = _ctx.AG_APPLICANT_T.Where(w => w.TESTING_NO == this.ExamLicense.TESTING_NO && w.APPLY_DATE == dtToday && w.ID_CARD_NO == this).ToList();
                    }
                    else
                    {
                        _applicantRegisted = new List<AG_APPLICANT_T>();
                    }

                  

                    flagRoomExam = false;
                }

                return _applicantRegisted;
            }

        }
        private String GetSourceType(DTO.UserProfile userProfile)
        {
            switch (userProfile.MemberType)
            {
                case 2: return "C";
                case 3: return "A";
                default:
                    return "";
            }
        }
        public IAS.DAL.Interfaces.IIASPersonEntities CTX { get { return _ctx; } }
        public DateTime ApplyDate { get { return _applyDate; } }
        public String FileName { get { return _filename; } set { _filename = value; } }
        //public String examPlaceCode { get { return PROVINCE_CODE + COMP_CODE; } }
        public String examPlaceCode { get { return _examPlaceCode; } }
        public String testTimeCode { get { return _TestTimeCode; } }
        public String licenseTypeCode { get { return _LicenseTypeCode; } }
        public DateTime testTingDate { get { return _TestTingDate; } }
        public AG_IAS_LICENSE_TYPE_R LicenseType { get { return _licenseType; } set { _licenseType = value; } }
        public AG_EXAM_PLACE_R ExamPlace { get { return _examPlace; } }
        public AG_EXAM_TIME_R ExamTime { get { return _examTime; } }
        public AG_EXAM_LICENSE_R ExamLicense { get { return _examLicense; } }
        public Boolean IsCanSeatRemain
        {
            get
            {
                if (ExamPlace != null || ExamLicense != null)
                {
                    Int32 apply = ExamLicense.EXAM_APPLY == null ? 0 : ExamLicense.EXAM_APPLY.Value;
                    //Int32 admission = ExamPlace.SEAT_AMOUNT == null ? "0".ToInt() : ExamPlace.SEAT_AMOUNT.Value;
                    Int32 admission = ExamLicense.EXAM_ADMISSION == null ? 0 : ExamLicense.EXAM_ADMISSION.Value;

                    Int32 remain = admission - apply;
                    return (remain >= EXAM_APPLY);
                }
                return true;

            }

        }
        public String TestingNumber { get { return _testingNumber; } }
        public DTO.UserProfile CreateBy { get { return _createBy; } }
        public DTO.RegistrationType SourceFrom
        {
            get
            {
                if (SOURCE_TYPE == "C")
                    return DTO.RegistrationType.Insurance;
                else if (SOURCE_TYPE == "A")
                    return DTO.RegistrationType.Association;
                else
                    return DTO.RegistrationType.Association; 

            }
        }

        public IEnumerable<ApplicantFileDetail> ApplicantFileDetails { get { return _applicantFileDetails; } }

        public void AddDetail(ApplicantFileDetail detail)
        {
            detail.SetHeader(this);
            detail.UPLOAD_GROUP_NO = this.UPLOAD_GROUP_NO;

            detail.TESTING_NO = TestingNumber;
            detail.EXAM_PLACE_CODE = this.PROVINCE_CODE + this.COMP_CODE;
            detail.APPLY_DATE = ApplyDate;
            detail.USER_ID = CreateBy.Id;
            detail.USER_DATE = _applyDate;

            IEnumerable<BusinessRule> rules = detail.GetBrokenRules();
            if (rules != null && rules.Count() > 0)
            {
                StringBuilder errmsg = new StringBuilder("");
                foreach (BusinessRule rule in rules)
                {
                    errmsg.AppendLine(rule.Rule);
                }
                detail.ERROR_MSG = errmsg.ToString();
            }
            _applicantFileDetails.Add(detail);
        }

        public void ValidCiticenDuplicate()
        {
            foreach (ApplicantFileDetail detail in ApplicantFileDetails)
            {
                IEnumerable<ApplicantFileDetail> ApplicantDetails = this.ApplicantFileDetails.Where(a => a.ID_CARD_NO == detail.ID_CARD_NO);
                if (ApplicantDetails.Count() > 1)
                {
                    //this.DuplicateCitizen();
                    foreach (ApplicantFileDetail applicant in ApplicantDetails)
                    {
                        applicant.DuplicateCitizen();
                    }
                }
            }

        }

        public void AddDetail(AG_IAS_APPLICANT_DETAIL_TEMP detial)
        {
            ApplicantFileDetail detail_A = detial as ApplicantFileDetail;
            detail_A.SetHeader(this);
            detail_A.UPLOAD_GROUP_NO = this.UPLOAD_GROUP_NO;
            detail_A.GetBrokenRules();
            _applicantFileDetails.Add(detail_A);
        }

        #region Validate
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public SummaryReceiveApplicant ValidateDataOfData()
        {
            SummaryReceiveApplicant summarize = this.ConvertToSummaryReceiveApplicants();

            return summarize;
        }

        private void CheckDup()
        {
            //IEnumerable<AG_IAS_APPLICANT_CHANGE> ApplicantDetails = this.ApplicantFileDetails.Where(a => a.ID_CARD_NO == detail.ID_CARD_NO);


            //DateTime dtToday = DateTime.Now.Date;

            //var ent1 = base.ctx.AG_APPLICANT_T.SingleOrDefault(w => w.TESTING_NO == TestingNo && w.APPLY_DATE == dtToday && w.ID_CARD_NO == idcard);
            //if (ent1 != null)
            //{
            //    res.ResultMessage = true;
            //}

            //var ent2 = from a in ctx.AG_APPLICANT_T
            //           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
            //           where e.TESTING_DATE == testTingDate && e.TEST_TIME_CODE == testTimeCode && a.APPLY_DATE == dtToday
            //            && a.ID_CARD_NO == idcard
            //           select a;
            //if (ent2.ToList().Count() > 0)
            //{
            //    res.ResultMessage = true;
            //}


            //var ent3 = from a in ctx.AG_APPLICANT_T
            //           join e in ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
            //           where e.EXAM_PLACE_CODE == examPlaceCode && e.TEST_TIME_CODE == testTimeCode && a.APPLY_DATE == dtToday
            //           && a.ID_CARD_NO == idcard
            //           select a;
            //if (ent3.ToList().Count() > 0)
            //{
            //    res.ResultMessage = true;
            //}
        }

        protected void Validate()
        {
            #region ปิดไปก่อน
            //if (String.IsNullOrEmpty(FILENAME))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.FILENAME_Required);

            //if (String.IsNullOrEmpty(UPLOAD_GROUP_NO))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.UPLOAD_GROUP_NO_Required);

            //if (String.IsNullOrEmpty(SOURCE_TYPE))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.SOURCE_TYPE_Required);

            //if (String.IsNullOrEmpty(PROVINCE_CODE))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.PROVINCE_CODE_Required);
            //else if (ExamLicense != null && PROVINCE_CODE != ExamLicense.EXAM_PLACE_CODE.Substring(0, 2))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.PROVINCE_CODE_Required);

            //if (String.IsNullOrEmpty(COMP_CODE))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.COMP_CODE_Required);
            //else if (ExamLicense != null && COMP_CODE != ExamLicense.EXAM_PLACE_CODE.Substring(2))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.COMP_CODE_Required);
            ////else if(this.SourceFrom== RegistrationType.Association && this.CreateBy.CompCode!=COMP_CODE)
            ////    AddBrokenRule(ApplicantFileHeaderBusinessRules.COMP_CODE_Required);

            //if (TESTING_DATE == DateTime.MinValue)
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.TESTING_DATE_Required);
            //else if (this.SourceFrom == RegistrationType.Insurance && ((DateTime)TESTING_DATE).Date != ((DateTime)ExamLicense.TESTING_DATE).Date)
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.TESTING_DATE_Required);

            //if (EXAM_AMOUNT == null || EXAM_AMOUNT == 0)
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.EXAM_AMOUNT_Required);

            //if (String.IsNullOrEmpty(TEST_TIME_CODE))
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.TEST_TIME_CODE_Required);
            //else if (ExamTime == null)
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.TEST_TIME_CODE_Required);
            //else if (ExamLicense != null && TEST_TIME_CODE != ExamLicense.TEST_TIME_CODE)
            //    AddBrokenRule(ApplicantFileHeaderBusinessRules.TEST_TIME_CODE_Required);
            #endregion

            if (String.IsNullOrEmpty(LICENSE_TYPE_CODE))
                AddBrokenRule(ApplicantFileHeaderBusinessRules.LICENSE_TYPE_CODE_Required);
            else if (LicenseType == null)
                AddBrokenRule(ApplicantFileHeaderBusinessRules.LICENSE_TYPE_CODE_Required);
            else if (ExamLicense != null && LICENSE_TYPE_CODE != ExamLicense.LICENSE_TYPE_CODE)
                AddBrokenRule(ApplicantFileHeaderBusinessRules.LICENSE_TYPE_CODE_Required);

            if (EXAM_APPLY == null || EXAM_APPLY == 0)
                AddBrokenRule(ApplicantFileHeaderBusinessRules.EXAM_APPLY_Required);
            else if (!IsCanSeatRemain)
                AddBrokenRule(new BusinessRule("EXAM_APPLY", "จำนวนผู้สมัครเกินจำนวนที่สามารถสมัครได้"));

        }

        #endregion

    }
}
