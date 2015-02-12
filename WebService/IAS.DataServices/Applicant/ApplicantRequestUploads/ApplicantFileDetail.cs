using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DTO;
using System.Text;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Applicant.ApplicantHelper;

namespace IAS.DataServices.Applicant.ApplicantRequestUploads
{
    public class ApplicantFileDetail : AG_IAS_APPLICANT_DETAIL_TEMP
    {
        private IAS.DAL.Interfaces.IIASPersonEntities _ctx;
        private DateTime dtToday = DateTime.Now.Date;
        private ApplicantFileHeader _applicantFileHeader;
        private Boolean IsNotDuplicate = true;
        public ApplicantFileDetail()
        {

        }
        public ApplicantFileDetail(IAS.DAL.Interfaces.IIASPersonEntities ctx)
        {
            this._ctx = ctx;
        }

        private List<AG_APPLICANT_T> _applicant = new List<AG_APPLICANT_T>();
        private AG_EXAM_LICENSE_R _license = new AG_EXAM_LICENSE_R();


        public ApplicantFileHeader ApplicantFileHeader { get { return _applicantFileHeader; } }
        public void SetHeader(ApplicantFileHeader header)
        {
            this.UPLOAD_GROUP_NO = header.UPLOAD_GROUP_NO;
            _applicantFileHeader = header;
        }

        #region Validate
        private List<BusinessRule> _brokenRules = new List<BusinessRule>();
        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();
            return _brokenRules;
        }
        public Boolean IsRegisted
        {
            get
            {
                if (ApplicantFileHeader.ExamLicense != null)
                {
                    if (ApplicantFileHeader.ApplicantRegisted.Count() > 0)
                    {
                        IEnumerable<AG_APPLICANT_T> resgised = ApplicantFileHeader.ApplicantRegisted.Where(a => a.TESTING_NO == ApplicantFileHeader.ExamLicense.TESTING_NO && a.ID_CARD_NO == this.ID_CARD_NO && a.EXAM_PLACE_CODE == ApplicantFileHeader.ExamLicense.EXAM_PLACE_CODE && a.RECORD_STATUS != "X");
                        return (resgised.Count() > 0);
                    }
                    else
                    {
                        return false;
                    }

                }

                if (ApplicantFileHeader.SourceFrom == RegistrationType.Association)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public Boolean IsRegistedToday
        {

            get
            {
                if (ApplicantFileHeader.ExamLicense != null)
                {
                    if (ApplicantFileHeader.ApplicantRegisted.Count() > 0)
                    {
                        IEnumerable<AG_APPLICANT_T> resgisedToday = ApplicantFileHeader.ApplicantRegisted.Where(a => a.TESTING_NO == ApplicantFileHeader.ExamLicense.TESTING_NO && a.APPLY_DATE == dtToday && a.ID_CARD_NO == this.ID_CARD_NO);
                        return (resgisedToday.Count() > 0);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ApplicantFileHeader.SourceFrom == RegistrationType.Association)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public Boolean IsRegistedTodayAndTime
        {
            get
            {
                if (ApplicantFileHeader.ExamLicense != null)
                {
                    if (ApplicantFileHeader.ApplicantRegisted.Count() > 0)
                    {
                        IEnumerable<AG_APPLICANT_T> registedTodayAndTime = from a in _ctx.AG_APPLICANT_T
                                                                           join e in _ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                                           where e.TESTING_DATE == ApplicantFileHeader.ExamLicense.TESTING_DATE && e.TEST_TIME_CODE == ApplicantFileHeader.ExamLicense.TEST_TIME_CODE && a.APPLY_DATE == dtToday
                                                                           && a.ID_CARD_NO == ID_CARD_NO && a.TESTING_NO == TESTING_NO && e.TESTING_NO == TESTING_NO
                                                                           select a;
                        return (registedTodayAndTime.Count() > 0);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ApplicantFileHeader.SourceFrom == RegistrationType.Association)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public Boolean IsRegistedTestingDateAndTimeAndOtherTestingNo
        {
            get
            {
                if (ApplicantFileHeader.ExamLicense != null)
                {
                    if (ApplicantFileHeader.ApplicantRegisted.Count() > 0)
                    {
                        IEnumerable<AG_APPLICANT_T> registedTestingDateAndTimeAndOtherTestingNo = from a in _ctx.AG_APPLICANT_T
                                                                                                  join e in _ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                                                                  where e.TESTING_DATE == ApplicantFileHeader.ExamLicense.TESTING_DATE && e.TEST_TIME_CODE == ApplicantFileHeader.ExamLicense.TEST_TIME_CODE
                                                                                                  && a.ID_CARD_NO == ID_CARD_NO
                                                                                                  select a;
                        return (registedTestingDateAndTimeAndOtherTestingNo.Count() > 0);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ApplicantFileHeader.SourceFrom == RegistrationType.Association)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public Boolean IsRegistedTodayAndTimeAndExamPlace
        {
            get
            {
                if (ApplicantFileHeader.ExamLicense != null)
                {
                    if (ApplicantFileHeader.ApplicantRegisted.Count() > 0)
                    {
                        IEnumerable<AG_APPLICANT_T> registedTodayAndTimeAndExamPlace = from a in _ctx.AG_APPLICANT_T
                                                                                       join e in _ctx.AG_EXAM_LICENSE_R on a.TESTING_NO equals e.TESTING_NO
                                                                                       where e.EXAM_PLACE_CODE == ApplicantFileHeader.ExamLicense.EXAM_PLACE_CODE && e.TEST_TIME_CODE == ApplicantFileHeader.ExamLicense.TEST_TIME_CODE && a.APPLY_DATE == dtToday
                                                                                       && a.ID_CARD_NO == ID_CARD_NO && a.TESTING_NO == TESTING_NO && e.TESTING_NO == TESTING_NO
                                                                                       select a;
                        return (registedTodayAndTimeAndExamPlace.Count() > 0);
                    }
                    else
                    {
                        return false;
                    }
                }

                if (ApplicantFileHeader.SourceFrom == RegistrationType.Association)
                {

                    return false;
                }
                else
                {
                    return true;
                }
            }

        }


        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        private Boolean ActivateLicense()
        {
            switch (ApplicantFileHeader.LICENSE_TYPE_CODE)
            {
                case "01":
                case "07": return !(INSUR_COMP_CODE.Substring(0, 1) == "1");

                case "02":
                case "05":
                case "06":
                case "08": return !(INSUR_COMP_CODE.Substring(0, 1) == "2");

                case "03":
                case "11":
                case "12":
                case "04": return !(String.IsNullOrEmpty(INSUR_COMP_CODE) || INSUR_COMP_CODE.Substring(0, 1) == "3");

                default:
                    return true;
            }

        }
        public void DuplicateCitizen()
        {
            if (IsNotDuplicate)
                SetDuplicateCitizen();
        }

        protected void SetDuplicateCitizen()
        {
            _brokenRules.Add(ApplicantFileDetailBusinessRules.ID_CARD_NO_DuplicateInFile);
            ERROR_MSG += ApplicantFileDetailBusinessRules.ID_CARD_NO_DuplicateInFile.Rule + "<br />";
            this.LOAD_STATUS = "F";
            IsNotDuplicate = false;
        }
        protected void Validate()
        {



            if (APPLICANT_CODE == null || APPLICANT_CODE == 0)
                AddBrokenRule(ApplicantFileDetailBusinessRules.APPLICANT_CODE_Required);

            //if (ApplicantFileHeader.SourceFrom == RegistrationType.Insurance)
            //{
            //    if (String.IsNullOrEmpty(TESTING_NO))
            //        AddBrokenRule(ApplicantFileDetailBusinessRules.TESTING_NO_Required);
            //    else if (ApplicantFileHeader.ExamLicense.TESTING_NO != TESTING_NO)
            //    {
            //        AddBrokenRule(ApplicantFileDetailBusinessRules.TESTING_NO_Required);
            //    }
            //}


            if (String.IsNullOrEmpty(EXAM_PLACE_CODE))
                AddBrokenRule(ApplicantFileDetailBusinessRules.EXAM_PLACE_CODE_Required);

            //if (String.IsNullOrEmpty(ACCEPT_OFF_CODE)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.ACCEPT_OFF_CODE_Required);

            if (APPLY_DATE == null || APPLY_DATE == DateTime.MinValue)
                AddBrokenRule(ApplicantFileDetailBusinessRules.APPLY_DATE_Required);

            if (String.IsNullOrEmpty(ID_CARD_NO))
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Required);
            else if (!Utils.IdCard.Verify(ID_CARD_NO))
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Required);
            else if (IsRegisted)
            {
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Registed);
            }

            else if (IsRegistedToday)
            {
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Registed);
            }
            else if (IsRegistedTodayAndTime)
            {
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Registed);
            }

            else if (IsRegistedTestingDateAndTimeAndOtherTestingNo)
            {
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Registed);
            }
            else if (IsRegistedTodayAndTimeAndExamPlace)
            {
                AddBrokenRule(ApplicantFileDetailBusinessRules.ID_CARD_NO_Registed);
            }

            if (String.IsNullOrEmpty(PRE_NAME_CODE))
                AddBrokenRule(ApplicantFileDetailBusinessRules.PRE_NAME_CODE_Required);

            if (String.IsNullOrEmpty(NAMES))
                AddBrokenRule(ApplicantFileDetailBusinessRules.NAMES_Required);
            else if (NAMES.Length > 30)
                AddBrokenRule(ApplicantFileDetailBusinessRules.NAMES_TooLong);
            else if (!StringNameHelper.Validate(this.NAMES))
                AddBrokenRule(ApplicantFileDetailBusinessRules.NAMES_Required);

            if (String.IsNullOrEmpty(LASTNAME))
                AddBrokenRule(ApplicantFileDetailBusinessRules.LASTNAME_Required);
            else if (LASTNAME.Length > 30)
                AddBrokenRule(ApplicantFileDetailBusinessRules.LASTNAME_TooLong);
            else if (!StringNameHelper.Validate(this.LASTNAME))
                AddBrokenRule(ApplicantFileDetailBusinessRules.LASTNAME_Required);

            if (BIRTH_DATE == null || BIRTH_DATE == DateTime.MinValue)
                AddBrokenRule(ApplicantFileDetailBusinessRules.BIRTH_DATE_Required);
            else if (!(((DateTime)BIRTH_DATE) < DateTime.Now.Date))
                AddBrokenRule(ApplicantFileDetailBusinessRules.BIRTH_DATE_Required);

            if (String.IsNullOrEmpty(SEX))
                AddBrokenRule(ApplicantFileDetailBusinessRules.SEX_Required);
            else if (SEX != "F" && SEX != "M")
                AddBrokenRule(ApplicantFileDetailBusinessRules.SEX_Required);

            if (String.IsNullOrEmpty(EDUCATION_CODE))
                AddBrokenRule(ApplicantFileDetailBusinessRules.EDUCATION_CODE_Required);

            if (!String.IsNullOrEmpty(ADDRESS1))
            {
                ADDRESS1 = (ADDRESS1.Length > 60) ? ADDRESS1.Substring(0, 60) : ADDRESS1;
            }

            if (!String.IsNullOrEmpty(ADDRESS2))
            {
                ADDRESS2 = (ADDRESS2.Length > 60) ? ADDRESS2.Substring(0, 60) : ADDRESS2;
            }

            //if (String.IsNullOrEmpty(AREA_CODE)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.AREA_CODE_Required);

            //if (String.IsNullOrEmpty(PROVINCE_CODE))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.PROVINCE_CODE_Required);

            //if (String.IsNullOrEmpty(ZIPCODE)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.ZIPCODE_Required);

            //if (String.IsNullOrEmpty(TELEPHONE)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.TELEPHONE_Required);

            //if (String.IsNullOrEmpty(AMOUNT_TRAN_NO))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.AMOUNT_TRAN_NO_Required);

            //if (String.IsNullOrEmpty(PAYMENT_NO)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.PAYMENT_NO_Required);

            if (ApplicantFileHeader.SourceFrom == RegistrationType.Insurance && String.IsNullOrEmpty(INSUR_COMP_CODE))
                AddBrokenRule(ApplicantFileDetailBusinessRules.INSUR_COMP_CODE_Required);
            else if (ApplicantFileHeader.SourceFrom == RegistrationType.Association && ActivateLicense())
                AddBrokenRule(ApplicantFileDetailBusinessRules.INSUR_COMP_CODE_Required);
            else if (ApplicantFileHeader.SourceFrom == RegistrationType.Insurance && ApplicantFileHeader.CreateBy.CompCode != INSUR_COMP_CODE)
                AddBrokenRule(ApplicantFileDetailBusinessRules.INSUR_COMP_CODE_Required);

            //if (String.IsNullOrEmpty(ABSENT_EXAM))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.ABSENT_EXAM_Required);

            //if (String.IsNullOrEmpty(RESULT))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.RESULT_Required);

            //if (EXPIRE_DATE == null || EXPIRE_DATE==DateTime.MinValue)
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.EXPIRE_DATE_Required);

            //if (String.IsNullOrEmpty(LICENSE))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.LICENSE_Required);

            //if (String.IsNullOrEmpty(CANCEL_REASON)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.CANCEL_REASON_Required);

            //if (String.IsNullOrEmpty(RECORD_STATUS)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.RECORD_STATUS_Required);

            if (String.IsNullOrEmpty(USER_ID))
                AddBrokenRule(ApplicantFileDetailBusinessRules.USER_ID_Required);

            if (USER_DATE == null || USER_DATE == DateTime.MinValue)
                AddBrokenRule(ApplicantFileDetailBusinessRules.USER_DATE_Required);

            //if (String.IsNullOrEmpty(EXAM_STATUS))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.EXAM_STATUS_Required);

            //if (String.IsNullOrEmpty(REQUEST_NO))
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.REQUEST_NO_Required);

            if (String.IsNullOrEmpty(UPLOAD_GROUP_NO))
                AddBrokenRule(ApplicantFileDetailBusinessRules.UPLOAD_GROUP_NO_Required);

            if (String.IsNullOrEmpty(SEQ_NO))
                AddBrokenRule(ApplicantFileDetailBusinessRules.SEQ_NO_Required);

            if (String.IsNullOrEmpty(TITLE))
                AddBrokenRule(ApplicantFileDetailBusinessRules.TITLE_Required);

            //if (String.IsNullOrEmpty(ERROR_MSG)) 
            //    AddBrokenRule(ApplicantFileDetailBusinessRules.ERROR_MSG_Required);

            if (_brokenRules.Count > 0)
                LOAD_STATUS = "F";
            else
                LOAD_STATUS = "T";

            if (String.IsNullOrEmpty(LOAD_STATUS))
                AddBrokenRule(ApplicantFileDetailBusinessRules.LOAD_STATUS_Required);

        }


        private void GenMessageValidate()
        {
            if (this.GetBrokenRules().Count() > 0)
            {
                StringBuilder messageError = new StringBuilder("");
                foreach (BusinessRule item in this.GetBrokenRules())
                {
                    messageError.Append(item.Rule + "<br />");
                }
                this.ERROR_MSG = messageError.ToString();
                this.LOAD_STATUS = "F";
            }
            else
            {
                this.LOAD_STATUS = "T";
            }
        }


        #endregion
    }
}