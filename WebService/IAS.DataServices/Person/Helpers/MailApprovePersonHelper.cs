using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IAS.DTO;
using IAS.Utils;
using IAS.DataServices.Properties;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.Person.Helpers  
{
    public class MailApprovePersonHelper              
    {
        public static bool SendMailApprovePerson(PersonTemp person, String username)   
        {
            StringBuilder emailBody = new StringBuilder();       
            String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();

            string emailAddress = person.EMAIL;
            string fullname = String.Format("{0} {1} {2}", "", person.NAMES, person.LASTNAME);
            
            emailBody.AppendLine(String.Format(@"เนื่องด้วย  {0} ได้ทำการสมัครเข้าใช้ ระบบช่องทางการบริการตัวแทนหรือนายหน้าประกันภัยแบบเบ็ดเสร็จ<br/><br />", fullname));

            emailBody.AppendLine(String.Format("ชื่อผู้ใช้ระบบ : {0} <br />", username));

            if (person.STATUS == ((int)PersonDataStatus.Approve).ToString())
            {
                emailBody.AppendLine(Resources.infoMailApprovePersonHelper_001+"<br />");
            }
            else if (person.STATUS == ((int)DTO.PersonDataStatus.NotApprove).ToString())
            {
                emailBody.AppendLine(Resources.infoMailApprovePersonHelper_002+"<br />");
                emailBody.AppendLine(Resources.infoMailApprovePersonHelper_003+"<br />");
            }
           
             
            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(link + "<br /><br />");
         
            try
            {
                EmailSender.Sending(emailBody, emailAddress).Sent();
            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }

    }
}