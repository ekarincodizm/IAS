using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;
using IAS.Utils;
using IAS.DataServices.Properties;
using IAS.Common.Email;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.Payment.Helpers
{
    public class MailReceiptSuccessHelper
    {
        public static bool SendMail(String fullname, String emailAddress, FileInfo receipt)
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();



            emailBody.AppendLine(String.Format("เนื่องด้วย {0} ได้ทำการชำระเงินให้กับ ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ<br/><br />", fullname));

            emailBody.AppendLine(Resources.infoMailReceiptSuccessHelper_001+"<br />");




            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(link + "<br /><br />");

            try
            {
                IList<FileInfo> attachs = new List<FileInfo>();
                attachs.Add(receipt);
                EmailSender.Sending(emailBody, emailAddress, attachs).Sent();

            }
            catch (Exception)
            {
                return false;
            }


            return true;
        }


        public static bool SendMail(String fullname, String emailAddress, IEnumerable<DTO.EmailReceiptTaskingRequest> receipts)
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();



            emailBody.AppendLine(String.Format("เนื่องด้วย {0} ได้ทำการชำระเงินให้กับ ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ<br/>", fullname));

            emailBody.AppendLine(Resources.infoMailReceiptSuccessHelper_002+"<br />");
            //ฟิวส์ ย้ายเข้าไปใน loop เพื่อหาลำดับที่
            if (receipts.Count() > 0)
            {
                emailBody.AppendLine("<table><tr><th>ลำดับ</th><th>ชื่อ-สกุล</th><th>เลขบัตรประชาชน</th><th>รูปแบบการชำระเงิน</th></tr>");
                Int32 rowcount = 0;

                foreach (DTO.EmailReceiptTaskingRequest item in receipts)
                {
                    rowcount++;
                    emailBody.AppendLine(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", rowcount.ToString(), item.FullName, item.IDCard, item.PettionTypeName));
                }
                emailBody.AppendLine("</table><br /><br />");
            }


            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(link + "<br /><br />");

            try
            {

                EmailSender.Sending(emailBody, emailAddress).Sent();

            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }


        public static bool SendMail(DTO.EmailSingleReceipt receipts)
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();


            emailBody.AppendLine(String.Format("    ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ ได้รับการชำระเงิน <u>{0}</u> <br/>", receipts.LicenseType));
            emailBody.AppendLine(String.Format("จาก {0} เมื่อวันที่ {1} เป็นจำนวนเงิน {2}  <br/>", receipts.FullName,receipts.ReceiptDate,receipts.totalMoney));
            emailBody.AppendLine(String.Format("และได้ทำการออกใบเสร็จรับเงินให้เป็นที่เรียบร้อยแล้ว (หมายเลขใบเสร็จรับเงินของคุณคือ <b>{0}</b>) <br/>", receipts.ReceiptNo));

            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(link + "<br /><br />");

            try
            {

                EmailSender.Sending(emailBody, receipts.Email).Sent();

            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }

        public static bool SendMail(string fullname, string[] emailAddress, IList<DTO.EmailReceiptTaskingRequest> receipts)
        {
            StringBuilder emailBody = new StringBuilder();
            String webUrl = ConfigurationManager.AppSettings["WebPublicUrl"].ToString();



            emailBody.AppendLine("    ระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ ทาง คปภ.ได้รับการชำระเงินจาก <br/>");
           
            foreach (DTO.EmailReceiptTaskingRequest item in receipts)
           {
               emailBody.AppendLine(String.Format("    ชื่อ-สกุล : {0}<br/>", item.FullName));
               emailBody.AppendLine(String.Format("    เลขบัตรประชาชน : {0}<br/>", item.IDCard));
               emailBody.AppendLine(String.Format("    รูปแบบการชำระเงิน : {0}<br/>", item.PettionTypeName));
               emailBody.AppendLine(String.Format("    เลขที่ใบเสร็จ : {0}<br/>", item.ReciveNo));
           }
            emailBody.AppendLine("    จึงขอส่ง เพื่อยืนยันการรับชำระเงิน และสามารถติดต่อขอรับใบเสร็จได้ที่บริษัทต้นสังกัด(ที่สมัครสอบ / ขอรับใบอนุญาต) ตั้งแต่บัดนี้เป็นต้นไป  <br/>");
            //ฟิวส์ ย้ายเข้าไปใน loop เพื่อหาลำดับที่
            //if (receipts.Count() > 0)
            //{
            //    emailBody.AppendLine("<table><tr><th>ลำดับ</th><th>ชื่อ-สกุล</th><th>เลขบัตรประชาชน</th><th>รูปแบบการชำระเงิน</th></tr>");
            //    Int32 rowcount = 0;

            //    foreach (DTO.EmailReceiptTaskingRequest item in receipts)
            //    {
            //        rowcount++;
            //        emailBody.AppendLine(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", rowcount.ToString(), item.FullName, item.IDCard, item.PettionTypeName));
            //    }
            //    emailBody.AppendLine("</table><br /><br />");
            //}


            String link = String.Format("<a href='{0}home.aspx'>คลิกเพื่อเข้าใช้ระบบ</a>", webUrl);

            emailBody.AppendLine(link + "<br /><br />");

            try
            {
                //IEnumerable<String> emails = emailAddress.ToList();

                //EmailServiceFactory.GetEmailService()
                //    .SendMail(ConfigurationManager.AppSettings["EmailOut"], emails, "สร้างใบเสร็จ", emailBody.ToString());

                foreach (string E_Mail in emailAddress)
                    EmailSender.Sending(emailBody, E_Mail).Sent();

            }
            catch (Exception ex)
            {

                return false;
            }


            return true;
        }
    }
}