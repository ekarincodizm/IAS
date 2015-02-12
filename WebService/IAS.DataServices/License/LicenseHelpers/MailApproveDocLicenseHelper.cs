using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using IAS.DTO;
using IAS.DataServices.Properties;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.License.LicenseHelpers
{
    public class MailApproveDocLicenseHelper
    {
        public static void SendMailApproveDoc(List<string> lsEmail,string pettitionName,string licenseName,string flagApprove)
        {
            StringBuilder emailBody = new StringBuilder();
            String emailOut = ConfigurationManager.AppSettings["EmailOut"];


            emailBody.AppendLine(string.Format("สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย (คปภ.) <br />"));
            emailBody.AppendLine(string.Format("ประกาศแจ้งผลการอนุมัติเอกสาร <br />"));
            emailBody.AppendLine(string.Format("โดยมีรายละเอียดดังนี้ <br />"));
            emailBody.AppendLine(string.Format("ประเภทใบอนุญาต : {0} <br />", licenseName));
            emailBody.AppendLine(string.Format("ประเภทการขอรับใบอนุญาต : {0} <br />", pettitionName));
            if (flagApprove == "Y")
            {
                emailBody.AppendLine(string.Format("ผลการอนุมัติ : ผ่าน <br />"));
            }
            else
            {
                emailBody.AppendLine(string.Format("ผลการอนุมัติ : ไม่ผ่าน <br />"));
            }



            try
            {
                foreach (var emailAddress in lsEmail)
                {
                    EmailSender.Sending(emailBody, emailAddress).Sent();
                }
              
            }
            catch (Exception)
            {
              //  return false;
            }

        }


    }
}