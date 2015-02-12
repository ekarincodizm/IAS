using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Applicant.ApplicantRequestUploads;
using System.Text;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DAL;

namespace IAS.DataServices.Applicant.Mapper
{
    public static class ApplicantFileHeaderMapper
    {
        public static DTO.SummaryReceiveApplicant ConvertToSummaryReceiveApplicants(this ApplicantFileHeader applicantFileHeader) {
            DTO.SummaryReceiveApplicant summarize = new DTO.SummaryReceiveApplicant();
            summarize.UploadGroupNo = applicantFileHeader.UPLOAD_GROUP_NO;
            summarize.Header = new DTO.UploadHeader();
            IList<DTO.ApplicantTemp> details = new List<DTO.ApplicantTemp>();

            Int32 errorAmount = applicantFileHeader.ApplicantFileDetails.Count(a => a.LOAD_STATUS == "F");
            Int32 passAmount = applicantFileHeader.ApplicantFileDetails.Count(a => a.LOAD_STATUS == "T");

            summarize.Header.RightTrans = passAmount;
            summarize.Header.MissingTrans = errorAmount;
            summarize.Header.Totals = applicantFileHeader.ApplicantFileDetails.Count();
            summarize.Header.UploadFileName = applicantFileHeader.FILENAME;
            summarize.Header.FileName = applicantFileHeader.FileName;

            if (applicantFileHeader.GetBrokenRules().Count() > 0)
            {
                StringBuilder errorMessage = new StringBuilder("");
                foreach (BusinessRule item in applicantFileHeader.GetBrokenRules())
                {
                    errorMessage.AppendLine(String.Format("- {0} <br />",item.Rule));
                }
                summarize.MessageError = errorMessage.ToString();
                //applicantFileHeader.e = errorMessage.ToString();
            }

            if (applicantFileHeader.EXAM_APPLY != applicantFileHeader.ApplicantFileDetails.Count())
                summarize.MessageError = String.Format("- {0} <br />", ApplicantFileHeaderBusinessRules.EXAM_APPLY_Required.Rule);

            Decimal price = new Decimal();
            AG_PETITION_TYPE_R ent = applicantFileHeader.CTX.AG_PETITION_TYPE_R.SingleOrDefault(s => s.PETITION_TYPE_CODE == "01");
            if (ent != null && ent.FEE != null)
            {
                price =  ((Decimal)ent.FEE);
            }
            //Decimal sumAmount = price * applicantFileHeader.ApplicantFileDetails.Count();
            //if (applicantFileHeader.EXAM_AMOUNT != null && sumAmount != applicantFileHeader.EXAM_AMOUNT)
            //{
            //    summarize.MessageError +=  ApplicantFileHeaderBusinessRules.EXAM_AMOUNT_Required.Rule;
            //}


            applicantFileHeader.ValidCiticenDuplicate();

            foreach (ApplicantFileDetail item in applicantFileHeader.ApplicantFileDetails)
            {
                DTO.ApplicantTemp detail = new DTO.ApplicantTemp()
                {
                    LOAD_STATUS = item.LOAD_STATUS,
                    APPLICANT_CODE = item.APPLICANT_CODE,
                    TESTING_NO = item.TESTING_NO,
                    EXAM_PLACE_CODE = item.EXAM_PLACE_CODE,
                    ACCEPT_OFF_CODE = item.ACCEPT_OFF_CODE,
                    APPLY_DATE = item.APPLY_DATE,
                    ID_CARD_NO = item.ID_CARD_NO,
                    PRE_NAME_CODE = item.PRE_NAME_CODE,
                    NAMES = item.NAMES,
                    LASTNAME = item.LASTNAME,
                    BIRTH_DATE = item.BIRTH_DATE,
                    SEX = item.SEX,
                    EDUCATION_CODE = item.EDUCATION_CODE,
                    ADDRESS1 = item.ADDRESS1,
                    ADDRESS2 = item.ADDRESS2,
                    AREA_CODE = item.AREA_CODE,
                    PROVINCE_CODE = item.PROVINCE_CODE,
                    ZIPCODE = item.ZIPCODE,
                    TELEPHONE = item.TELEPHONE,
                    AMOUNT_TRAN_NO = item.AMOUNT_TRAN_NO,
                    PAYMENT_NO = item.PAYMENT_NO,
                    INSUR_COMP_CODE = item.INSUR_COMP_CODE,
                    ABSENT_EXAM = item.ABSENT_EXAM,
                    RESULT = item.RESULT,
                    EXPIRE_DATE = item.EXPIRE_DATE,
                    LICENSE = item.LICENSE,
                    CANCEL_REASON = item.CANCEL_REASON,
                    RECORD_STATUS = item.RECORD_STATUS,
                    USER_ID = item.USER_ID,
                    USER_DATE = item.USER_DATE,
                    EXAM_STATUS = item.EXAM_STATUS,
                    REQUEST_NO = item.REQUEST_NO,
                    UPLOAD_GROUP_NO = item.UPLOAD_GROUP_NO,
                    SEQ_NO = item.SEQ_NO,
                    TITLE = item.TITLE,
                    ERROR_MSG = item.ERROR_MSG, 
                    Header = summarize.Header
                };


                details.Add(detail);
            }


            summarize.ReceiveApplicantDetails = details;

            return summarize; 
        }

    }
}