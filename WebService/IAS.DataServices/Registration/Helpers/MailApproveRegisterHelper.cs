using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using IAS.DAL;
using IAS.Utils;
using System.Net.Mail;
using IAS.DataServices.Properties;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.Registration.Helpers          
{
    public class MailApproveRegisterHelper               
    {                                    
        public static bool SendMailApproveRegister(AG_IAS_REGISTRATION_T register, String username)   
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = String.Empty;
            String fromMail = ConfigurationManager.AppSettings["EmailOut"].ToString();        
            //String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();

            if (register.MEMBER_TYPE.Equals(DTO.MemberType.General.GetEnumValue().ToString()) ||
                register.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()) ||
                register.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()))
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrlForUser"].ToString();
            }
            else
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();
            }

            string emailAddress = register.EMAIL;
            //string emailSubject = "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทน/นายหน้าประกันภัย";
            string fullname = String.Format("{0} {1} {2}", "", register.NAMES, register.LASTNAME);

            emailBody.AppendLine(String.Format("เนื่องด้วย  {0} ได้ทำการสมัครเข้าใช้ ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ<br/><br />", fullname));

            emailBody.AppendLine(String.Format("ชื่อผู้ใช้ระบบ : {0} <br />", username));

            if (register.STATUS == ((int)DTO.RegistrationStatus.Approve).ToString())
            {
                emailBody.AppendLine(Resources.infoMailApprovePersonHelper_001+"<br />");
            }
            else if (register.STATUS == ((int)DTO.RegistrationStatus.NotApprove).ToString())
            {
                emailBody.AppendLine(Resources.infoMailApprovePersonHelper_002+"<br />");
                emailBody.AppendLine(Resources.infoMailApprovePersonHelper_003+"<br />");
            }
           
             
            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(link + "<br /><br />");
         

            try
            {
                MailMessage mailMsg = EmailSender.Sending(emailBody, emailAddress);
                mailMsg.Sent();

            }
            catch (Exception )
            {
                return false;
            }
            

            return true;
        }

       

    }
}
