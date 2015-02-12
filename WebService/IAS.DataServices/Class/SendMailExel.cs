using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Text;
using OfficeOpenXml;
using IAS.DAL;
using IAS.Common.Email;

namespace IAS.DataServices.Class
{
    public static class SendMailExel
    {
        public static void Send(DataTable table,string fromemail, string address,string comcode,string name,string email)
        {
            int count = table.Rows.Count;
            IASPersonEntities ctx = new IASPersonEntities();  
            //var ent = ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == comcode).NAME;
            string comname = ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == comcode).NAME; 
            string emailSubject = "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย";
            StringBuilder emailBody = new StringBuilder();
           // emailBody.AppendLine("<div style='font-family: Verdana;font-size: 12px;'>");
            emailBody.AppendLine(String.Format("{0} ขอจัดส่งข้อมูลรายชื่อผู้สมัครสอบ ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย ", comname));
            emailBody.AppendLine("ตามเอกสารแนบ");
            emailBody.AppendLine(string.Format("ชื่อผู้นำส่งข้อมูล {0}  email {1} ", name,email));
            emailBody.AppendLine(string.Format("นำส่งข้อมูลเมื่อวันที่ {0} ", DateTime.Now.ToLongDateString()));
            emailBody.AppendLine(string.Format("ด้วยความเคารพ {0} ", comname));
          //  emailBody.AppendLine("</div>");
            MailMessage _mailMessage = new MailMessage(fromemail, address)
            {
                IsBodyHtml = true,
                Subject = emailSubject,
                Body = emailBody.ToString()
            };
            Attachment attachment = new Attachment(SendMailExel.CreateExel(table),comcode+DateTime.Now.ToLongDateString() + ".xlsx");
            //_mailMessage.Attachments.Add(attachment);
            //using (SmtpClient SmtpServer = new SmtpClient())
            //{
            //    if (_mailMessage != null)
            //    {
            //        SmtpServer.Send(_mailMessage);
            //    }
            //}

            AttachStream attachFile = new AttachStream() { FileName = comcode+DateTime.Now.ToLongDateString()+".xlsx", FileStream = SendMailExel.CreateExel(table) };
            IList<AttachStream> listattach = new List<AttachStream>();
            listattach.Add(attachFile);
            EmailServiceFactory.GetEmailService().SendMail(fromemail, address, "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย", emailBody.ToString(), listattach);
        }

        public static Stream CreateExel(DataTable table)
        {
            byte[] file = new byte[1024];
            DataTable tbExport = new DataTable();
            Dictionary<string, string> columns = new Dictionary<string, string>();
            columns.Add("รหัสสอบ", "TESTING_NO");
            columns.Add("เลขที่นั่งสอบ", "APPLICANT_CODE");
            columns.Add("วันที่สมัครสอบ", "APPLY_DATE");
            columns.Add("รหัสบริษัทที่สังกัด", "INSUR_COMP_CODE");
            columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
            columns.Add("คำนำหน้า", "NAME");
            columns.Add("ชื่อ", "FIRSTNAME");
            columns.Add("นามสกุล", "LASTNAME");
            columns.Add("วันเดือนปีเกิด", "BIRTH_DATE");
            columns.Add("E-mail", "EMAIL");
            columns.Add("เบอร์โทรศัพท์", "TELEPHONE");
            foreach (var item in columns)
            {
                tbExport.Columns.Add(item.Key, typeof(string));
            }

            foreach (DataRow rows in table.Rows)
            {
                DataRow row = tbExport.NewRow();
                foreach (var item in columns)
                {
                    if (item.Key.Contains("เบอร์โทรศัพท์"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace("#", " ต่อ ");
                    }
                    else if (item.Key.Contains("วันเดือนปีเกิด") || item.Key.Contains("วันที่สมัครสอบ"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace(" 0:00:00", "");
                    }
                    else if (item.Key.Contains("สถานะ"))
                    {
                        if (rows[item.Value].ToString() == "N")
                        {
                            row[item.Key] = "ไม่อนุมัติ";
                        }
                        else if (rows[item.Value].ToString() == "Y")
                        {
                            row[item.Key] = "อนุมัติ";
                        }
                        else if (rows[item.Value].ToString() == "W")
                        {
                            row[item.Key] = "รออนุมัติ";
                        }
                        else
                        {
                            row[item.Key] = rows[item.Value].ToString();
                        }
                    }
                    else
                    {
                        row[item.Key] = rows[item.Value].ToString();
                    }
                }
                tbExport.Rows.Add(row);
            }

            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FileExport");
                worksheet.Cells["A1"].LoadFromDataTable(tbExport, true);
                worksheet.DefaultColWidth = 25;
                file = package.GetAsByteArray();
            }

            Stream stream = new MemoryStream(file);
            return stream;
        }

    }
}