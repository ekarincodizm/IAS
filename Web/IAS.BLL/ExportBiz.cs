using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using Ionic.Zip;
using System.Text;
using System.Data;
using OfficeOpenXml;
using IAS.BLL.Properties;
using System.Net.Mail;
using IAS.Common.Email;

namespace IAS.BLL
{
    public class ExportBiz : Page
    {
        MailMessage _mailMessage;
        public void CreateApplcantCSV(DataTable App_Data)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.BufferOutput = false;
            HttpContext c = HttpContext.Current;
            string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            string archiveName = "Export_CSV_" + namedate + ".zip";
            HttpContext.Current.Response.ContentType = "application/zip";
            HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
            StringBuilder stringWrite = new StringBuilder();

            stringWrite.Append("เลขที่สอบ,หมายเลขบัตรประชาชน,คำนำหน้า,ชื่อ,นามสกุล,วัน/เดือน/ปีเกิด,เพศ,วุฒิการศึกษา,รหัสบริษัท/สมาคม,ที่อยู่,รหัสพื้นที่,หน่วยรับสมัคร");
            stringWrite.Append(Environment.NewLine);

            foreach (System.Data.DataRow rows in App_Data.Rows)
            {
                stringWrite.Append(rows["RUN_NO"].ToString() == null ? "" : rows["RUN_NO"].ToString() + ",");//เลขที่สอบ
                stringWrite.Append(rows["ID_CARD_NO"].ToString() == null ? "" : rows["ID_CARD_NO"].ToString() + ",");//หมายเลขบัตรประชาชน
                stringWrite.Append(rows["NAME"].ToString() == null ? "" : rows["NAME"].ToString() + ",");//คำนำหน้า
                stringWrite.Append(rows["NAMES"].ToString() == null ? "" : rows["NAMES"].ToString() + ",");//ชื่อ
                stringWrite.Append(rows["LASTNAME"].ToString() == null ? "" : rows["LASTNAME"].ToString() + ",");//นามสกุล
                stringWrite.Append(rows["BIRTH_DATE"].ToString() == null ? "" : rows["BIRTH_DATE"].ToString().Replace(" 0:00:00", "") + ",");//วัน/เดือน/ปีเกิด
                if (rows["SEX"].ToString() != null)
                {
                    string sex = rows["SEX"].ToString() == "M" ? "ช," : "ญ,";//เพศ
                    stringWrite.Append(sex);
                }
                //stringWrite.Append(rows["TESTING_NO"].ToString() == null ? "" : rows["TESTING_NO"].ToString() + ",");//รหัสสอบ
                //stringWrite.Append(rows["TESTING_DATE"].ToString() == null ? "" : rows["TESTING_DATE"].ToString().Replace(" 0:00:00", "") + ",");//วันที่สอบ
                //stringWrite.Append(rows["TEST_TIME_CODE"].ToString() == null ? "" : rows["TEST_TIME_CODE"].ToString() + ",");  //รหัสเวลาสอบ
                stringWrite.Append(rows["EDUCATION_CODE"].ToString() == null ? "" : rows["EDUCATION_CODE"].ToString() + ",");   //วุฒิการศึกษา  
                stringWrite.Append(rows["INSUR_COMP_CODE"].ToString() == null ? "" : rows["INSUR_COMP_CODE"].ToString() + ",");//,รหัสบริษัท/สมาคม
                stringWrite.Append(rows["ADDRESS1"].ToString() == null ? "" : rows["ADDRESS1"].ToString() + ",");//ที่อยู่
                stringWrite.Append(rows["AREA_CODE"].ToString() == null ? "" : rows["AREA_CODE"].ToString() + ",");//รหัสพื้นที่                    
                stringWrite.Append(rows["UPLOAD_BY_SESSION"].ToString() == null ? "" : rows["UPLOAD_BY_SESSION"].ToString());//หน่วยรับสมัคร
                stringWrite.Append(Environment.NewLine);
            }

            using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
            {
                zip.AddEntry("FileImport_" + namedate + ".csv", stringWrite.ToString(), System.Text.Encoding.UTF8);

                zip.Save(HttpContext.Current.Response.OutputStream);
            }
            HttpContext.Current.Response.Close();

        }
        public void CreateExcel(DataTable table, Dictionary<string, string> columns,List<HeaderExcel>header, DTO.UserProfile userProfile)
        {
            DataTable tbExport = new DataTable();    
            foreach (var item in columns)
            {
                tbExport.Columns.Add(item.Key, typeof(string));
            }      

            foreach (var item in header)
            {

                tbExport.Rows.Add(item.NameColumnsOne, item.ValueColumnsOne, item.NameColumnsTwo, item.ValueColumnsTwo);
            }

            DataRow row = tbExport.NewRow();
            tbExport.Rows.Add(row);
             row = tbExport.NewRow();
            foreach (var item in columns)
            {               
                row[item.Key] = item.Key;                
            }
            tbExport.Rows.Add(row);
           
            foreach (DataRow rows in table.Rows)
            {
                row = tbExport.NewRow();
                foreach (var item in columns)
                {
                    if (item.Key.Contains("เบอร์โทรศัพท์"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace("#", " ต่อ ");
                    }
                    else if (item.Key.Contains("วันที่"))
                    {
                        //if (!string.IsNullOrEmpty(row[item.Key].ToString()))
                        //{

                          row[item.Key] = rows[item.Value].ToString().Replace(" 0:00:00", "");
                        //}
                        //else
                        //{
                        //    row[item.Key] = "0:00:00";
                        //}

                    }
                    else if (item.Key.Contains("สถานะ"))
                    {
                        if (rows[item.Value].ToString() == "N")
                        {
                            row[item.Key] = Resources.errorExportBiz_001;
                        }
                        else if (rows[item.Value].ToString() == "Y")
                        {
                            row[item.Key] = Resources.errorExportBiz_002;
                        }
                        else if (rows[item.Value].ToString() == "W")
                        {
                            row[item.Key] = Resources.errorExportBiz_003;
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
            row = tbExport.NewRow();
            tbExport.Rows.Add(row);
            row = tbExport.NewRow();
            row[0] = string.Format("ผู้ออกรายงาน {0} {1} วันที่ {2}",userProfile.Name,userProfile.LastName,DateTime.Now.ToLongDateString());
            tbExport.Rows.Add(row);
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FileExport");
                worksheet.Cells["A1"].LoadFromDataTable(tbExport, true);

                worksheet.DefaultColWidth = 25;
                worksheet.DeleteRow(1, 1);
                           

                
                //ดาวน์โหลด
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                HttpContext c = HttpContext.Current;
                string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string archiveName = "Export_Excel_" + namedate + ".xlsx";
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
                HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                //using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
                //{
                //    zip.AddEntry("FileExport.xlsx", package.GetAsByteArray());
                //    zip.Save(HttpContext.Current.Response.OutputStream);
                //}
                HttpContext.Current.Response.Close();
            }
        }
        public void CreateExcel(DataTable table, Dictionary<string, string> columns)
        {
            DataTable tbExport = new DataTable();
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
                    else if (item.Key.Contains("วันที่"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace(" 0:00:00", "");
                    }
                    else if (item.Key.Contains("สถานะ"))
                    {
                        if (rows[item.Value].ToString() == "N")
                        {
                            row[item.Key] = Resources.errorExportBiz_001;
                        }
                        else if (rows[item.Value].ToString() == "Y")
                        {
                            row[item.Key] = Resources.errorExportBiz_002;
                        }
                        else if (rows[item.Value].ToString() == "W")
                        {
                            row[item.Key] = Resources.errorExportBiz_003;
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
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                HttpContext c = HttpContext.Current;
                string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string archiveName = "Export_Excel_" + namedate + ".zip";
                HttpContext.Current.Response.ContentType = "application/zip";
                HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
                using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
                {
                    zip.AddEntry("FileExport.xlsx", package.GetAsByteArray());
                    zip.Save(HttpContext.Current.Response.OutputStream);
                }
                HttpContext.Current.Response.Close();
            }
        }
        public void CreateExcel(DataSet dataset, Dictionary<string, string> columns)
        {
            DataTable table = dataset.Copy().Tables[0];
            DataTable tbExport = new DataTable();
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
                    else if (item.Key.Contains("วันที่"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace(" 0:00:00", "");
                    }
                    else if (item.Key.Contains("สถานะ"))
                    {
                        if (rows[item.Value].ToString() == "N")
                        {
                            row[item.Key] = Resources.errorExportBiz_001;
                        }
                        else if (rows[item.Value].ToString() == "Y")
                        {
                            row[item.Key] = Resources.errorExportBiz_002;
                        }
                        else if (rows[item.Value].ToString() == "W")
                        {
                            row[item.Key] = Resources.errorExportBiz_003;
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
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                HttpContext c = HttpContext.Current;
                string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string archiveName = "Export_Excel_" + namedate + ".zip";
                HttpContext.Current.Response.ContentType = "application/zip";
                HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
                using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
                {
                    zip.AddEntry("FileExport.xlsx", package.GetAsByteArray());
                    zip.Save(HttpContext.Current.Response.OutputStream);
                }
                HttpContext.Current.Response.Close();
            }
        }               
        public void SendExcel(string address, DataTable table, Dictionary<string, string> columns, DTO.UserProfile UserProfile)
        {

            Dictionary<string, string> Select = new Dictionary<string, string>();
            Select.Add("รหัสสอบ", "TESTING_NO");
            Select.Add("เลขที่นั่งสอบ", "APPLICANT_CODE");
            Select.Add("วันที่สมัครสอบ", "APPLY_DATE");
            Select.Add("รหัสบริษัทที่สังกัด", "INSUR_COMP_CODE");
            Select.Add("เลขบัตรประชาชน", "ID_CARD_NO");
            Select.Add("คำนำหน้า", "NAME");
            Select.Add("ชื่อ", "FIRSTNAME");
            Select.Add("นามสกุล", "LASTNAME");
            Select.Add("วันเดือนปีเกิด", "BIRTH_DATE");
            Select.Add("E-mail", "EMAIL");
            Select.Add("เบอร์โทรศัพท์", "TELEPHONE");

            columns = Select;
         
            try
            {

                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                string comname = biz.GetCompanyCodeById(UserProfile.CompCode).Name;
                string emailSubject = "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย";
                StringBuilder emailBody = new StringBuilder();
                //emailBody.AppendLine("<div style='font-family: Verdana;font-size: 12px;'>");
                emailBody.AppendLine(String.Format("บริษัท {0} ขอจัดส่งข้อมูลรายชื่อผู้สมัครสอบ ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย", comname));
                emailBody.AppendLine("ตามเอกสารแนบ");
                emailBody.AppendLine(string.Format("ชื่อผู้นำส่งข้อมูล {0} {1} email {2} ", UserProfile.Name, UserProfile.LastName, UserProfile.LoginName));
                emailBody.AppendLine(string.Format("นำส่งข้อมูลเมื่อวันที่ {0} ", DateTime.Now.ToLongDateString()));
                emailBody.AppendLine(string.Format("ด้วยความเคารพ บริษัท {0} ", comname));
               // emailBody.AppendLine("</div>");
                string fromemail = "amsadmin@oic.or.th";
                _mailMessage = new MailMessage(fromemail, address)
                    {
                        IsBodyHtml = true,
                        Subject = emailSubject,
                        Body = emailBody.ToString()
                    };
                byte[] file = new byte[1024];

                DataTable tbExport = new DataTable();
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
                                row[item.Key] = Resources.errorExportBiz_001;
                            }
                            else if (rows[item.Value].ToString() == "Y")
                            {
                                row[item.Key] = Resources.errorExportBiz_002;
                            }
                            else if (rows[item.Value].ToString() == "W")
                            {
                                row[item.Key] = Resources.errorExportBiz_003;
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
                Attachment attachment = new Attachment(stream, UserProfile.CompCode + "_" + DateTime.Now.ToLongDateString() + ".xlsx");

                //AttachStream attachFile = new AttachStream() { FileName = "ExelFile", FileStream = stream };
                //IList<AttachStream> listattach = new List<AttachStream>();
                //listattach.Add(attachFile);
                //EmailServiceFactory.GetEmailService().SendMail(fromemail, address, "ระบบลงทะเบียนระบบช่องทางการบริหารตัวแทนหรือนายหน้าประกันภัย", emailBody.ToString(), listattach);


                _mailMessage.Attachments.Add(attachment);
                using (SmtpClient SmtpServer = new SmtpClient())
                {
                    if (_mailMessage != null)
                    {
                        SmtpServer.Send(_mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void CreateExcel(DataTable table, Dictionary<string, string> columns,DTO.UserProfile userProfile)
        {
            DataTable tbExport = new DataTable();
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
                    else if (item.Key.Contains("วันที่"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace(" 0:00:00", "");
                    }
                    else if (item.Key.Contains("สถานะ"))
                    {
                        if (rows[item.Value].ToString() == "N")
                        {
                            row[item.Key] = Resources.errorExportBiz_001;
                        }
                        else if (rows[item.Value].ToString() == "Y")
                        {
                            row[item.Key] = Resources.errorExportBiz_002;
                        }
                        else if (rows[item.Value].ToString() == "W")
                        {
                            row[item.Key] = Resources.errorExportBiz_003;
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

            DataRow row1 = tbExport.NewRow();
            tbExport.Rows.Add(row1);
            row1 = tbExport.NewRow();
            row1[0] = string.Format("ผู้ออกรายงาน {0} {1} วันที่ {2}", userProfile.Name, userProfile.LastName, DateTime.Now.ToLongDateString());
            tbExport.Rows.Add(row1);


            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FileExport");
                worksheet.Cells["A1"].LoadFromDataTable(tbExport, true);
                worksheet.DefaultColWidth = 25;
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.BufferOutput = false;
                //HttpContext c = HttpContext.Current;
                //string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                //string archiveName = "Export_Excel_" + namedate + ".zip";
                //HttpContext.Current.Response.ContentType = "application/zip";
                //HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
                //using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
                //{
                //    zip.AddEntry("FileExport.xlsx", package.GetAsByteArray());
                //    zip.Save(HttpContext.Current.Response.OutputStream);
                //}
                //HttpContext.Current.Response.Close();

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                HttpContext c = HttpContext.Current;
                string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string archiveName = "Export_Excel_" + namedate + ".xlsx";
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
                HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                HttpContext.Current.Response.Close();
            }
        }


        //Invoice5
        public void CreateExcelInvoice5(DataTable table, Dictionary<string, string> columns, List<HeaderExcel> header, DTO.UserProfile userProfile)
        {
            DataTable tbExport = new DataTable();
            foreach (var item in columns)
            {
                tbExport.Columns.Add(item.Key, typeof(string));
            }

            foreach (var item in header)
            {

                tbExport.Rows.Add(item.NameColumnsOne, item.ValueColumnsOne, item.NameColumnsTwo, item.ValueColumnsTwo);
            }

            DataRow row = tbExport.NewRow();
            tbExport.Rows.Add(row);
            row = tbExport.NewRow();
            foreach (var item in columns)
            {
                row[item.Key] = item.Key;
            }
            tbExport.Rows.Add(row);

            foreach (DataRow rows in table.Rows)
            {
                row = tbExport.NewRow();
                foreach (var item in columns)
                {
                    if (item.Key.Contains("เบอร์โทรศัพท์"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace("#", " ต่อ ");
                    }
                    else if (item.Key.Contains("วันที่"))
                    {
                        row[item.Key] = rows[item.Value].ToString().Replace(" 0:00:00", "");
                    }
                    //STATUS
                    else if (item.Key.Contains("สถานะการจ่ายเงิน"))
                    {
                        try
                        {
                            string PaymentDate = rows["PAYMENT_DATE"].ToString();
                            string ExpireDate = rows["EXPIRATION_DATE"].ToString();
                            string Status = rows["STATUS"].ToString();
                            string StatusText = "";

                            if (ExpireDate == "")
                            {
                                StatusText = "รอชำระเงิน";
                            }
                            else if ((Status == "P") && ((Convert.ToDateTime(PaymentDate) < Convert.ToDateTime(ExpireDate)) || (Convert.ToDateTime(PaymentDate) == Convert.ToDateTime(ExpireDate))))
                            {
                                StatusText = "ชำระเงินแล้ว";
                            }
                            else if ((Status == "P") && (Convert.ToDateTime(PaymentDate) > Convert.ToDateTime(ExpireDate)))
                            {
                                StatusText = "ชำระเงินล่าช้า";
                            }
                            else if ((PaymentDate == "") && (Convert.ToDateTime(ExpireDate) < DateTime.Now))
                            {
                                StatusText = "ยังไม่ได้ชำระเงิน";
                            }
                            else if (PaymentDate == "")
                            {
                                StatusText = "รอชำระเงิน";
                            }
                            row[item.Key] = StatusText;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        row[item.Key] = rows[item.Value].ToString();
                    }
                }
                tbExport.Rows.Add(row);


            }
            row = tbExport.NewRow();
            tbExport.Rows.Add(row);
            row = tbExport.NewRow();
            row[0] = string.Format("ผู้ออกรายงาน {0} {1} วันที่ {2}", userProfile.Name, userProfile.LastName, DateTime.Now.ToLongDateString());
            tbExport.Rows.Add(row);
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FileExport");
                worksheet.Cells["A1"].LoadFromDataTable(tbExport, true);

                worksheet.DefaultColWidth = 25;
                worksheet.DeleteRow(1, 1);



                //ดาวน์โหลด
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BufferOutput = false;
                HttpContext c = HttpContext.Current;
                string namedate = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                string archiveName = "Export_Excel_" + namedate + ".xlsx";
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.AddHeader("content-disposition", "filename=" + archiveName);
                HttpContext.Current.Response.BinaryWrite(package.GetAsByteArray());
                //using (ZipFile zip = new ZipFile(System.Text.Encoding.UTF8))
                //{
                //    zip.AddEntry("FileExport.xlsx", package.GetAsByteArray());
                //    zip.Save(HttpContext.Current.Response.OutputStream);
                //}
                HttpContext.Current.Response.Close();
            }
        }
        
    }
}

