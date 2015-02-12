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

namespace IAS.DataServices.Registration.Helpers
{
    public class MailConfirmHelper
    {
        public static bool SendMailConfirmRegistration(AG_IAS_REGISTRATION_T reg)       
        {                                                                               
            StringBuilder emailBody = new StringBuilder();
            String webUrl = String.Empty;
            //String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();

            if (reg.MEMBER_TYPE.Equals(DTO.MemberType.General.GetEnumValue().ToString()) ||
                reg.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()) ||
                reg.MEMBER_TYPE.Equals(DTO.MemberType.Insurance.GetEnumValue().ToString()))
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrlForUser"].ToString();
            }
            else
            {
                webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();
            }

            string emailAddress = reg.EMAIL;
            string fullname = String.Format("{0} {1} {2}", "", reg.NAMES, reg.LASTNAME);
            string status = "";

            Int32 statusNo;

            if (reg.STATUS == null)
            {
                reg.STATUS = DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString();
            }
            if (Int32.TryParse(reg.STATUS.ToString(), out statusNo))
            {
                
                if (statusNo == 1 || statusNo == 2)
                {
                    DTO.RegistrationStatus regstatus = (DTO.RegistrationStatus)statusNo;
                    //switch (regstatus)
                    //{
                    //    case DTO.RegistrationStatus.WaitForApprove: status = "รอการอนุมัติ"; break;
                    //    case DTO.RegistrationStatus.NotApprove: status = "ได้รับการอนุมัติ"; break;

                    //    default:
                    //        break;
                    //}
                    if (regstatus.GetEnumValue().ToString().Equals(DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString()))
                    {
                        status = "รอการอนุมัติ";
                    }
                    else if (regstatus.GetEnumValue().ToString().Equals(DTO.RegistrationStatus.Approve.GetEnumValue().ToString()))
                    {
                        status = "ได้รับการอนุมัติ";
                    }

                }

            }



            emailBody.AppendLine(String.Format("เนื่องด้วย {0} ได้ทำการสมัครสมาชิก ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ<br/><br/>", fullname));
            


            if (!String.IsNullOrEmpty(reg.MEMBER_TYPE) && Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.TestCenter)
            {
                //String usernamepass = Utils.CryptoBase64.Encryption(reg.EMAIL+"||"+reg.REG_PASS);
                emailBody.AppendLine(String.Format("ชื่อผู้ใช้ : {0} <br /><br />", reg.EMAIL));
                String link = String.Format("<a href='{0}ChangePassword/ChangePass.aspx?dat={1}'>คลิกเพื่อทำการยืนยันการลงทะเบียน</a>", webUrl, reg.LINK_REDIRECT);
                emailBody.AppendLine(link);
            }
            else if (!String.IsNullOrEmpty(reg.MEMBER_TYPE) && Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.General)
            {
                emailBody.AppendLine(String.Format("ชื่อผู้ใช้  : {0} <br /><br />", reg.ID_CARD_NO));
                String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);
                emailBody.AppendLine(link);
            }
            else if (!String.IsNullOrEmpty(reg.MEMBER_TYPE) && (Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.Insurance
                                                                || Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.Association))
            {
                emailBody.AppendLine(String.Format("ชื่อผู้ใช้  : {0} <br /><br />", reg.EMAIL));
                String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);
                emailBody.AppendLine(link);
            }
           
          
           
            emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ {0} <br /> ", status) );

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
