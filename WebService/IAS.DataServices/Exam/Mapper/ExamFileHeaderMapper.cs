using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Exam.ExamRequestUploads;

using System.Text;
using IAS.DAL;

namespace IAS.DataServices.Exam.Mapper
{
    public static class ExamFileHeaderMapper
    {
        public static DTO.SummaryReceiveExam ConvertToSummaryReceiveExam(this ExamFileHeader ExamFileHeader)
        {
            DTO.SummaryReceiveExam summarize = new DTO.SummaryReceiveExam();
            summarize.Identity = ExamFileHeader.UPLOAD_GROUP_NO;
            summarize.Header = new DTO.UploadHeader();
            IList<DTO.ExamTemp> details = new List<DTO.ExamTemp>();

            Int32 errorAmount = ExamFileHeader.ExamFileDetails.Count(a => a.STATUS_SAVE_SCORE == "F");
            Int32 passAmount = ExamFileHeader.ExamFileDetails.Count(a => a.STATUS_SAVE_SCORE == "T");

            summarize.Header.RightTrans = passAmount;
            summarize.Header.MissingTrans = errorAmount;
            summarize.Header.Totals = ExamFileHeader.ExamFileDetails.Count();
            summarize.Header.UploadFileName = ExamFileHeader.FILENAME;
            summarize.Header.FileName = ExamFileHeader.FILENAME;

            //if (ExamFileHeader.GetBrokenRules().Count() > 0)
            //{
            //    StringBuilder errorMessage = new StringBuilder("");
            //    foreach (BusinessRule item in ExamFileHeader.GetBrokenRules())
            //    {
            //        errorMessage.AppendLine(String.Format("- {0} <br />", item.Rule));
            //    }
            //    summarize.MessageError = errorMessage.ToString();
            //    //applicantFileHeader.e = errorMessage.ToString();
            //}

            if (Convert.ToInt32(ExamFileHeader.CNT_PER) != ExamFileHeader.ExamFileDetails.Count())
                summarize.MessageError = String.Format("- {0} <br />", ExamFileHeaderBusinessRules.CNT_PER_Worng.Rule);

           

            ExamFileHeader.ValidCiticenDuplicate();

            //foreach (ExamFileDetail item in ExamFileHeader.ExamFileDetails)
            //{
            //    DTO.ExamTemp detail = new DTO.ExamTemp()
            //    {
            //        APPLICANT_CODE=Convert.ToInt32(item.ApplicantCode),
            //        LICENSE_TYPE_CODE = item.LicenseTypeCode,
                    
            //        Header = summarize.Header
            //    };


            //    details.Add(detail);
            //}


            summarize.ReceiveExam = details;

            return summarize;
        }

    }
}