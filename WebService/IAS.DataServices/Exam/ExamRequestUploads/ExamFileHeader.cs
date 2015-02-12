using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.License.LicenseRequestUploads;
using IAS.DTO;
using System.Text;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Exam.Helpers;
using IAS.DataServices.Exam.Mapper;
using IAS.DataServices.Properties;


namespace IAS.DataServices.Exam.ExamRequestUploads 
{                                                                                 
    public class ExamFileHeader : AG_IAS_APPLICANT_SCORE_H_TEMP
    {
        // ต้องหาวิธี กันกรณีเปลี่ยน id

        private IAS.DAL.IASPersonEntities _ctx;
        private IList<ExamFileDetail> _examFileDetails = new List<ExamFileDetail>();
        private String _filename;

        private DTO.UserProfile _createBy;    
       
        

        public ExamFileHeader() { }
        public ExamFileHeader(ExamFileHeaderRequest request) 
        {

            _ctx = request.Context;
            UPLOAD_GROUP_NO = OracleDB.GetGenAutoId();
            _createBy = request.UserProfile;
            ASSOCIATE_NAME = ExamSubStringHelper.Get(request.LineData, 1, 80).Trim();
            LICENSE_TYPE_CODE = ExamSubStringHelper.Get(request.LineData, 81, 2).Trim();
            PROVINCE_CODE = ExamSubStringHelper.Get(request.LineData, 83, 2).Trim();
            ASSOCIATE_CODE = ExamSubStringHelper.Get(request.LineData, 85, 3).Trim();
            TESTING_DATE = ExamSubStringHelper.Get(request.LineData, 88, 10).Trim();
            EXAM_TIME_CODE = ExamSubStringHelper.Get(request.LineData, 98, 2).Trim();
            CNT_PER = request.LineData.Length > 100
                ? ExamSubStringHelper.Get(request.LineData, 100, request.LineData.Length).Trim():"";
        }


        public DTO.UserProfile CreateBy { get { return _createBy; } }

        public IAS.DAL.IASPersonEntities CTX { get { return _ctx; } }
        public IEnumerable<ExamFileDetail> ExamFileDetails { get { return _examFileDetails; } }

        public void AddDetail(ExamFileDetail detail) 
        {
            
            detail.SetHeader(this);
            detail.UPLOAD_GROUP_NO = this.UPLOAD_GROUP_NO;
            detail.AssociateName = this.ASSOCIATE_NAME;
            detail.AssociateCode = this.ASSOCIATE_CODE;
            detail.ProvinceCode = this.PROVINCE_CODE;
            detail.TestingDate = this.TESTING_DATE;
            detail.LicenseTypeCode = this.LICENSE_TYPE_CODE;
            detail.TimeCode = this.EXAM_TIME_CODE;
            VW_IAS_COM_CODE ent = this.CTX.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == "01");
            detail.InsurCompName = ent.NAME;

            string examResult = detail.EXAM_RESULT;
            if ("P_F".Contains(examResult))
            {
                detail.EXAM_RESULT = examResult;
            }
            else if (examResult == "M")
            {
                detail.ABSENT_EXAM = examResult;
            }


            string title = detail.TITLE == "น.ส." ? "นางสาว" : detail.TITLE;

            //ตรวจสอบคำนำหน้าชื่อ
            VW_IAS_TITLE_NAME entTitle = this.CTX.VW_IAS_TITLE_NAME.ToList().FirstOrDefault(s => s.NAME == title);
            if (entTitle != null)
                detail.PRE_NAME_CODE = entTitle.ID.ToString();
            else
            {
                detail.ERROR_MSG = Resources.errorExamFileHeader_001;
            }



            IEnumerable<BusinessRule> rules = detail.GetBrokenRules();
            if (rules != null && rules.Count() > 0) {
                StringBuilder errmsg = new StringBuilder("");
                foreach (BusinessRule rule in rules)
                {
                    errmsg.AppendLine(String.Format("- {0} <br />", rule.Rule));
                }
                detail.ERROR_MSG = errmsg.ToString();
            }
            _examFileDetails.Add(detail);
        }

        public void AddDetail(AG_IAS_APPLICANT_SCORE_D_TEMP detial_tmp)
        {
            ExamFileDetail detail = detial_tmp as ExamFileDetail;


            detail.SetHeader(this);
            detail.UPLOAD_GROUP_NO = this.UPLOAD_GROUP_NO;
            detail.GetBrokenRules();

            IEnumerable<BusinessRule> rules = detail.GetBrokenRules();
            if (rules != null && rules.Count() > 0)
            {
                StringBuilder errmsg = new StringBuilder("");
                foreach (BusinessRule rule in rules)
                {
                    errmsg.AppendLine(String.Format("- {0} <br />", rule.Rule));
                }
                detail.ERROR_MSG = errmsg.ToString();
            }
            _examFileDetails.Add(detail);

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

     

        protected void Validate()
        {

            if (String.IsNullOrEmpty(FILENAME))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.FILENAME_Required);
            }
            else
            {
                FILENAME = FILENAME.Length > 50 ? FILENAME.Substring(0, 50) : FILENAME;
            }
            if (String.IsNullOrEmpty(UPLOAD_GROUP_NO)) AddBrokenRule(ExamFileHeaderBusinessRules.UPLOAD_GROUP_NO_Required);
            if (String.IsNullOrEmpty(ASSOCIATE_NAME))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.ASSOCIATE_NAME_Required);
            }
            else if(ASSOCIATE_NAME.Length>80)
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.ASSOCIATE_NAME_Worng);
            }
            if (String.IsNullOrEmpty(LICENSE_TYPE_CODE))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.LICENSE_TYPE_CODE_Required);
            }
            else if (LICENSE_TYPE_CODE.Length != 2)
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.LICENSE_TYPE_CODE_Worng);
            }
                
            if (String.IsNullOrEmpty(PROVINCE_CODE))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.PROVINCE_CODE_Required); 
            }
            else if (PROVINCE_CODE.Length != 2)
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.PROVINCE_CODE_Worng);
            }
            if (String.IsNullOrEmpty(ASSOCIATE_CODE))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.ASSOCIATE_CODE_Required);
            }
            else if (ASSOCIATE_CODE.Length !=3)
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.ASSOCIATE_CODE_Worng);
            }
            if (String.IsNullOrEmpty(TESTING_DATE)) AddBrokenRule(ExamFileHeaderBusinessRules.TESTING_DATE_Required);
            if (String.IsNullOrEmpty(EXAM_TIME_CODE))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.EXAM_TIME_CODE_Required);
            }
            else if (EXAM_TIME_CODE.Length != 2)
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.EXAM_TIME_CODE_Worng);
            }
            if (String.IsNullOrEmpty(CNT_PER))
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.CNT_PER_Required);
            }
            else if (CNT_PER.Length > 18)
            {
                AddBrokenRule(ExamFileHeaderBusinessRules.CNT_PER_Worng);
            }

        }

        #endregion
      

        public void ValidCiticenDuplicate()
        {
            foreach (ExamFileDetail detail in ExamFileDetails)
            {
                IEnumerable<ExamFileDetail> ExamDetails = this.ExamFileDetails.Where(a => a.ID_CARD_NO == detail.ID_CARD_NO);
                if (ExamDetails.Count() > 1)
                {
                    //this.DuplicateCitizen();
                    foreach (ExamFileDetail Exam in ExamDetails)
                    {
                        Exam.DuplicateCitizen();
                    }
                }
            }

        }
    }
}
