using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using IAS.Utils;
using IAS.DataServices.Helpers;
using IAS.Common.Email;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class MailApplicantHelper
    {
        public static bool SendMailConfirmApplicant(DTO.EmailApplicantChange app)
        { 
            StringBuilder emailBody = new StringBuilder();

            String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();
            string emailAddress = app.Email;
            string fullnameOLD = String.Format("{0} {1} ", "", app.FullNameOld);
            string fullnameNEW = String.Format("{0} {1} ", "", app.FullNameNew);
            string OLdidcard = app.OLDIDCard;
            string Newidcard = app.NewIDCard;

            int status = app.status;
            Int16? asso = app.Asso;
            Int16? oic = app.OIC;
            int testingno = Convert.ToInt32(app.TestingNo);
            string CancelReason = app.CancelReason;
            
          


            emailBody.AppendLine(String.Format("เรื่อง : ผลการยื่นขอแก้ไขรายละเอียดผู้สมัครสอบ<br/><br/>จากการยื่นเรื่องขอแก้ไขรายละเอียดผู้สมัครสอบ<br/>รหัสรอบสอบ : " +  + testingno + "<br/><br/>"));
            emailBody.AppendLine(String.Format("จากข้อมูล<br/>หมายเลขบัตรประชาชน : " + OLdidcard + "&nbsp;&nbsp&nbsp;&nbsp   ชื่อ - นามสกุล : " + fullnameOLD + "<br/><br/>"));
            emailBody.AppendLine(String.Format("เป็นข้อมูล<br/>หมายเลขบัตรประชาชน : " + Newidcard + "&nbsp;&nbsp&nbsp;&nbsp   ชื่อ - นามสกุล : " + fullnameNEW + "<br/><br/>"));
            
            
            if (status == 1 && asso == 1 && oic == 0)//Com
            {

                
                emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ รอการพิจารณาจากคปภ."));
                
            }
            else if (status == 2 && asso == 1 && oic == 1)//Com
            {
                
                emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ อนุมัติเรียบร้อย"));
            }
            else if (status == 1 && asso == 2 && oic == 0)//Com
            {
                
                emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ ไม่ผ่านการพิจารณาจากสมาคม<br/>เนื่องจาก : {0}",CancelReason));
            }
            else if (status == 2 && asso == 1 && oic == 2)//Com//asso
            {
               
                emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ ไม่ผ่านการพิจารณาจากคปภ.<br/>เนื่องจาก : {0}",CancelReason));
            }
            else//if (status == 0 && asso == 0 && oic == 0)//Com
            {
                

                emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ รอการพิจารณาจากสมาคม"));
            }
            //string status = "";

            //Int32 statusNo;
            //if (Int32.TryParse(reg.STATUS.ToString(), out statusNo))
            //{

            //    if (statusNo == 1 || statusNo == 2)
            //    {
            //        DTO.RegistrationStatus regstatus = (DTO.RegistrationStatus)statusNo;
            //        switch (regstatus)
            //        {
            //            case DTO.RegistrationStatus.WaitForApprove: status = "รอการอนุมัติ"; break;
            //            case DTO.RegistrationStatus.NotApprove: status = "ได้รับการอนุมัติ"; break;

            //            default:
            //                break;
            //        }

            //    }

            //}



            //emailBody.AppendLine(String.Format("เนื่องด้วย {0} xxxxx<br/><br/>", fullname + "หมายเลขบัตรประชาชน" + idcard));



            //if (!String.IsNullOrEmpty(app.MEMBER_TYPE) && Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.TestCenter)
            //{
            //    //String usernamepass = Utils.CryptoBase64.Encryption(reg.EMAIL+"||"+reg.REG_PASS);
            //    emailBody.AppendLine(String.Format("ชื่อผู้ใช้ : {0} <br /><br />", reg.EMAIL));
            //    String link = String.Format("<a href='{0}ChangePassword/ChangePass.aspx?dat={1}'>คลิกเพื่อทำการยืนยันการลงทะเบียน</a>", webUrl, reg.LINK_REDIRECT);
            //    emailBody.AppendLine(link);
            //}
            //else if (!String.IsNullOrEmpty(reg.MEMBER_TYPE) && Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.General)
            //{
            //    emailBody.AppendLine(String.Format("ชื่อผู้ใช้  : {0} <br /><br />", reg.ID_CARD_NO));
            //    String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);
            //    emailBody.AppendLine(link);
            //}
            //else if (!String.IsNullOrEmpty(reg.MEMBER_TYPE) && (Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.Insurance
            //                                                    || Convert.ToInt32(reg.MEMBER_TYPE) == (int)DTO.RegistrationType.Association))
            //{
            //    emailBody.AppendLine(String.Format("ชื่อผู้ใช้  : {0} <br /><br />", reg.EMAIL));
            //    String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);
            //    emailBody.AppendLine(link);
            //}



            //emailBody.AppendLine(String.Format("ขณะนี้สถานะของคุณ คือ {0} <br /> ", status));



            //MailApplicantHelper.SendMailConfirmApplicant(String.Format("{0} {1} {2}", "", app.FullName, personalUser.LASTNAME), app.Email, (_EmailApplicantChange);
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

        public static void SendMailAddOwnerToExamRoom(DTO.EmailAddOwnerToExamRoom Data)
        {
            String emailOut = ConfigurationManager.AppSettings["EmailOut"].ToString();
            StringBuilder emailBody = new StringBuilder();
            string title = "แจ้งข้อมูลการสอบของตัวแทน/นายหน้า ในระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ";
            emailBody.AppendLine(" สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย (คปภ.) <br/>");
            emailBody.AppendLine(string.Format(" เรื่อง <strong>{0}</strong>", title));
            emailBody.AppendLine("<br/><br/>");
            emailBody.AppendLine("ระบบทำการจัดห้องสอบเรียบร้อยแล้ว<br/>");
            emailBody.AppendLine("โดยมีรายละเอียดดังนี้<br/><br/>");
            emailBody.AppendLine(string.Format("รหัสสอบ : <strong>{0}</strong><br/>", Data.TESTING_NO));
            emailBody.AppendLine(string.Format("สอบวันที่ : <strong>{0} {1} น.</strong><br/>", Data.TESTING_DATE.Value.ToShortDateString(), Data.TEST_TIME));
            emailBody.AppendLine(string.Format("ห้องสอบ : <strong>{0}</strong><br/>", Data.EXAM_ROOM_NAME));

            foreach (string email in Data.EMAIL)
            {
                try
                {
                    EmailServiceFactory.GetEmailService().SendMail(emailOut, email.Trim(), title, emailBody.ToString());
                }
                catch { }
            }
        }
    }
}