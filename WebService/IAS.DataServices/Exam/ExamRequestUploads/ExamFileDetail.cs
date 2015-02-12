using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DTO;
using System.Text;
using IAS.DataServices.Payment.TransactionBanking;

namespace IAS.DataServices.Exam.ExamRequestUploads 
{
    public class ExamFileDetail : AG_IAS_APPLICANT_SCORE_D_TEMP
    {

        private ExamFileHeader _examFileHeader;
        private String _loadStatus;
        private Boolean IsNotDuplicate = true;
        public ExamFileDetail()                               
        {
                                                     
        }

        public ExamFileDetail(IASPersonEntities ctx)
        {
            // TODO: Complete member initialization
            this.ctx = ctx;
        }
        public void DuplicateCitizen()
        {
            if (IsNotDuplicate)
                SetDuplicateCitizen();
        }
        protected void SetDuplicateCitizen()
        {
            _brokenRules.Add(ExamFileDetailBusinessRules.ID_CARD_NO_Dup);
            ERROR_MSG += ExamFileDetailBusinessRules.ID_CARD_NO_Dup.Rule + "<br />";
           this.STATUS_SAVE_SCORE = "F";
            IsNotDuplicate = false;
        }
        public ExamFileHeader ExamFileHeader { get { return _examFileHeader; } }
        public String LoadStatus { get { return _loadStatus; } set { _loadStatus = value; } }
        public void SetHeader(ExamFileHeader header)   
        {
            this.UPLOAD_GROUP_NO = header.UPLOAD_GROUP_NO;
            _examFileHeader = header;                            
        }

        #region Validate
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        private IASPersonEntities ctx;
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
            //if (String.IsNullOrEmpty(STATUS_SAVE_SCORE)) AddBrokenRule(ExamFileDetailBusinessRules.STATUS_SAVE_SCORE_Required);
            if (String.IsNullOrEmpty(ABSENT_EXAM)) 
            { 
                AddBrokenRule(ExamFileDetailBusinessRules.ABSENT_EXAM_Required); 
            }
            else if(ABSENT_EXAM.Length>1)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.ABSENT_EXAM_Worng);
            }
            if (String.IsNullOrEmpty(PRE_NAME_CODE)) AddBrokenRule(ExamFileDetailBusinessRules.PRE_NAME_CODE_Required);
            //if (String.IsNullOrEmpty(ERROR_MSG)) AddBrokenRule(ExamFileDetailBusinessRules.ERROR_MSG_Required);
            if (String.IsNullOrEmpty(UPLOAD_GROUP_NO)) AddBrokenRule(ExamFileDetailBusinessRules.UPLOAD_GROUP_NO_Required);
            if (String.IsNullOrEmpty(SEQ_NO))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.SEQ_NO_Required);
            }
            else if (SEQ_NO.Length > 20) 
            {
                AddBrokenRule(ExamFileDetailBusinessRules.SEQ_NO_Worng);
            }
            if (String.IsNullOrEmpty(SEAT_NO))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.SEAT_NO_Required);
            }
            else if (SEAT_NO.Length > 20)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.SEAT_NO_Worng);
            }
            if (String.IsNullOrEmpty(ID_CARD_NO))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.ID_CARD_NO_Required);
            }
            else if(ID_CARD_NO.Length!=13)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.ID_CARD_NO_Required);
            }
            if (String.IsNullOrEmpty(TITLE))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.TITLE_Required);
            }
            else if (TITLE.Length>20) 
            {
                AddBrokenRule(ExamFileDetailBusinessRules.TITLE_Worng);
            }
            if (String.IsNullOrEmpty(NAMES))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.NAMES_Required);
            }
            else if (NAMES.Length > 20)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.NAMES_Worng);
            }
            if (String.IsNullOrEmpty(LAST_NAME)) 
            {
                AddBrokenRule(ExamFileDetailBusinessRules.LAST_NAME_Required);
            }
            else if (LAST_NAME.Length > 20)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.LAST_NAME_Worng);
            }

            if (!String.IsNullOrEmpty(ADDRESS1))
            {
                ADDRESS1 = (ADDRESS1.Length > 100) ? ADDRESS1.Substring(0, 100) : ADDRESS1;
            }
            if (!String.IsNullOrEmpty(ADDRESS2))
            {
                ADDRESS2 = (ADDRESS2.Length > 100) ? ADDRESS2.Substring(0, 100) : ADDRESS2;
            }

            if (String.IsNullOrEmpty(AREA_CODE)) 
            { 
                AddBrokenRule(ExamFileDetailBusinessRules.AREA_CODE_Required); 
            }
            else if (AREA_CODE.Length != 20)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.AREA_CODE_Worng);
            }
            if (BIRTH_DATE == null || BIRTH_DATE == DateTime.MinValue) AddBrokenRule(ExamFileDetailBusinessRules.BIRTH_DATE_Required);
            if (String.IsNullOrEmpty(SEX))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.SEX_Required);
            }
            else if (SEX.Length != 1)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.SEX_NOT_1);
            }
            if (String.IsNullOrEmpty(EDUCATION_CODE))
            {
                AddBrokenRule(ExamFileDetailBusinessRules.EDUCATION_CODE_Required);
            }
            else if(EDUCATION_CODE.Length>3)
            {
                AddBrokenRule(ExamFileDetailBusinessRules.EDUCATION_CODE_Worng);
            }
            if (String.IsNullOrEmpty(COMP_CODE)) AddBrokenRule(ExamFileDetailBusinessRules.COMP_CODE_Required);
            if (APPROVE_DATE==null || APPROVE_DATE==DateTime.MinValue) AddBrokenRule(ExamFileDetailBusinessRules.APPROVE_DATE_Required);
            //if (String.IsNullOrEmpty(EXAM_RESULT)) AddBrokenRule(ExamFileDetailBusinessRules.EXAM_RESULT_Required);
            if (!String.IsNullOrEmpty(SCORE_1) && (SCORE_1.Length>3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_1_Required);
            if (!String.IsNullOrEmpty(SCORE_2) && (SCORE_2.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_2_Required);
            if (!String.IsNullOrEmpty(SCORE_3) && (SCORE_3.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_3_Required);
            if (!String.IsNullOrEmpty(SCORE_4) && (SCORE_4.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_4_Required);
            if (!String.IsNullOrEmpty(SCORE_5) && (SCORE_5.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_5_Required);
            if (!String.IsNullOrEmpty(SCORE_6) && (SCORE_6.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_6_Required);
            if (!String.IsNullOrEmpty(SCORE_7) && (SCORE_7.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_7_Required);
            if (!String.IsNullOrEmpty(SCORE_8) && (SCORE_8.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_8_Required);
            if (!String.IsNullOrEmpty(SCORE_9) && (SCORE_9.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_9_Required);
            if (!String.IsNullOrEmpty(SCORE_10) && (SCORE_10.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_10_Required);
            if (!String.IsNullOrEmpty(SCORE_11) && (SCORE_11.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_11_Required);
            if (!String.IsNullOrEmpty(SCORE_12) && (SCORE_12.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_12_Required);
            if (!String.IsNullOrEmpty(SCORE_13) && (SCORE_13.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_13_Required);
            if (!String.IsNullOrEmpty(SCORE_14) && (SCORE_14.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_14_Required);
            if (!String.IsNullOrEmpty(SCORE_15) && (SCORE_15.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_15_Required);
            if (!String.IsNullOrEmpty(SCORE_16) && (SCORE_16.Length > 3)) AddBrokenRule(ExamFileDetailBusinessRules.SCORE_16_Required);

            if (_brokenRules.Count > 0)
                STATUS_SAVE_SCORE = "F";
            else
                STATUS_SAVE_SCORE = "T";

        }

        private void GenMessageValidate() {
            if (this.GetBrokenRules().Count() > 0)
            {
                StringBuilder messageError = new StringBuilder("");
                foreach (BusinessRule item in this.GetBrokenRules())
                {
                    messageError.Append(item.Rule + "<br />");
                }
                this.ERROR_MSG = messageError.ToString();
                this.STATUS_SAVE_SCORE = "F";
            }
            else
            {
                this.STATUS_SAVE_SCORE = "T";
            }
        }


        #endregion
    }
}