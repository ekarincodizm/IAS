using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using IAS.DAL;
using IAS.Utils;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.Person.Helpers  
{
    public class MailChangePasswordHelper
    {
        public static bool SendMailChangePasswordRegistration(AG_IAS_PERSONAL_T person, String username, String oldpassword, String newpassword)   
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = String.Empty;

            if ( person.MEMBER_TYPE.Equals(DTO.MemberType.General.GetEnumValue().ToString()) ||
                person.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()) ||
                person.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()) )
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrlForUser"].ToString();
            }
            else
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();
            }
            

            string emailAddress = person.EMAIL;
            string fullname = String.Format("{0} {1} {2}", "", person.NAMES, person.LASTNAME);

            emailBody.AppendLine(String.Format("เนื่องด้วย  {0} ได้ทำการเปลี่ยนรหัสผ่าน ระบบช่องทางการบริการตัวแทนหรือนายหน้าประกันภัยแบบเบ็ดเสร็จ<br/><br />", fullname));

            emailBody.AppendLine(String.Format("ชื่อผู้ใช้ระบบ : {0} <br />", username));
            emailBody.AppendLine(String.Format("รหัสผ่านใหม่ของคุณคือ  {0} <br /><br />", newpassword));

            String paras = Utils.CryptoBase64.Encryption(String.Format("{0}||{1}||{2}||{3}",username, oldpassword, newpassword, person.EMAIL));
            String linkrenew = String.Format("<a href='{0}ForgetPassword/ConfirmRenew.aspx?renew={1}'>คลิกเพื่อยืนยันการเปลี่ยนรหัสผ่าน</a>", webUrl, paras);
             
            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(linkrenew + "<br /><br />");


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

        public static bool SendMailChangePasswordRegistration(AG_IAS_REGISTRATION_T person, String username, String oldpassword, String newpassword)  
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = String.Empty;

            if (person.MEMBER_TYPE.Equals(DTO.MemberType.General.GetEnumValue().ToString()) ||
                person.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()) ||
                person.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()))
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrlForUser"].ToString();
            }
            else
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();
            }

            string emailAddress = person.EMAIL;

            string fullname = String.Format("{0} {1} {2}", "", person.NAMES, person.LASTNAME);



            emailBody.AppendLine(String.Format("เนื่องด้วย  {0} ได้ทำการเปลี่ยนรหัสผ่าน ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ<br/><br />", fullname));

            emailBody.AppendLine(String.Format("ชื่อผู้ใช้ระบบ : {0} <br />", username));
            emailBody.AppendLine(String.Format("รหัสผ่านใหม่ของคุณคือ  {0} <br /><br />", newpassword));

            String paras = Utils.CryptoBase64.Encryption(String.Format("{0}||{1}||{2}||{3}", username, oldpassword, newpassword, person.EMAIL));
            String linkrenew = String.Format("<a href='{0}ForgetPassword/ConfirmRenew.aspx?renew={1}'>คลิกเพื่อยืนยันการเปลี่ยนรหัสผ่าน</a>", webUrl, paras);

            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(linkrenew + "<br /><br />");


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
