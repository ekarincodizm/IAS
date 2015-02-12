using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Web;

namespace IAS.BLL
{
    public class ReportPDF
    {
        /// <summary>
        /// แสดงปีย้อนหลังจากปัจจุบันลงไป 100 ปี
        /// </summary>
        /// <returns>ปีย้อนหลังจากปัจจุบันลงไป 100 ปี </returns>
        public DataTable GetYearSOnLoad()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Name", typeof(string));
            int ThisYear = System.Convert.ToInt16(DateTime.Now.Year);
            for (int i = 0; i <= 100; i++)
            {
                table.Rows.Add(ThisYear - i, (ThisYear + 543) - i);
            }
            return table;
        }

        public byte[] GetByteArray(String strFileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(strFileName, System.IO.FileMode.Open);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            byte[] imgbyte = new byte[fs.Length + 1];
            imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
            br.Close();
            fs.Close();
            return imgbyte;
        }

        public void GeneratePDFFromLocalReport(string argStrReportPath, IDataReader argObjInterfaceReader,string[] argArrStrFilter)
        {
            //get your report datasource from the database
            //IDataReader iReader = GetReportDataSourceReader();
            LocalReport report = new LocalReport();
            report.ReportPath = HttpContext.Current.Server.MapPath(argStrReportPath) ;//"~/reports/rdlc/MyReport.rdlc");
            report.DataSources.Add(new ReportDataSource("DataSource1",argObjInterfaceReader)); // iReader));

            if (argArrStrFilter.Length > 0)
            {
                ReportParameter[] parameters = new ReportParameter[argArrStrFilter.Length];
                for (int i = 0; i < argArrStrFilter.Length; i++)
                {
                    parameters[i] = new ReportParameter(argArrStrFilter[i], "");
                }
                //parameters[0] = new ReportParameter("Filter1", "Filter1's value");
                //parameters[1] = new ReportParameter("Filter2", "Filter2's value");
                //parameters[2] = new ReportParameter("Footer", "Footer's value");
                report.SetParameters(parameters);
            }
            //
            //code to render report as pdf document
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            Warning[] warnings = null;
            string[] streamids = null;
            //
            byte[] byteArray = report.Render("PDF", null,
            out mimeType, out encoding, out extension, out streamids, out warnings);
            //
            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Disposition","attachment; filename=MyPDF.pdf");
            HttpContext.Current.Response.AddHeader("Content-Length",byteArray.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.BinaryWrite(byteArray);
            HttpContext.Current.Response.End();
        }

        private IDataReader GetReportDataSourceReader()
        {
            try
            {
                System.Data.SqlClient.SqlDataReader dr=null;// = new System.Data.SqlClient.SqlDataReader();
                ReportDataSource reportDataSource = new ReportDataSource { Name = "DataSet1", Value = dr };
                return dr;// (IDataReader)reportDataSource.Value;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        #region FileManager

        public bool FileMange(string mode, string FilePathSour, string FilePathDest)
        {
            try
            {
                switch (mode)
                {
                    case "move":
                        if (System.IO.File.Exists(FilePathSour.ToString()))
                        {
                            System.IO.File.Move(FilePathSour.ToString(), FilePathDest.ToString());
                        }
                        break;
                    case "copy":
                        if (System.IO.File.Exists(FilePathSour.ToString()))
                        {
                            System.IO.File.Copy(FilePathSour.ToString(), FilePathDest.ToString());
                        }
                        break;
                    case "delete":
                        if (System.IO.File.Exists(FilePathSour.ToString()))
                        {
                            System.IO.File.Delete(FilePathSour.ToString());
                        }
                        break;
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        #endregion FileManager

        #region Datatable Test

        public DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            DataTable table = new DataTable();
            table.Columns.Add("ADF_REC_ID", typeof(string));
            table.Columns.Add("ADF_CATEGORY", typeof(string));
            table.Columns.Add("ADF_FILE_NAME", typeof(string));
            table.Columns.Add("ADF_FILE_TYPE", typeof(string));
            table.Columns.Add("ADF_FILE_SIZE", typeof(string));
            table.Columns.Add("PathFile", typeof(string));

            // Here we add five DataRows.
            table.Rows.Add("1", "ข้อมูลแผน/หลักสูตรอบรม", "rex1", "gif", "1111", null);
            table.Rows.Add("2", "ข้อมูลผู้เข้าสมัครอบรม", "rex2", "gif", "222", null);
            table.Rows.Add("3", "ข้อมูลการชำระเงิน", "rex3", "gif", "3333", null);

            return table;
        }

        #endregion Datatable Test

        #region ConvertToThaiBaht

        public string ConvertToThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        #endregion ConvertToThaiBaht

        #region CreatePDF
        public void CreatePDF(string argStrFilePath, string argStrFileName, DataTable argDataTable,string argStrDataSetName,bool argBoolIsForceDownload)
        {
            // argStrFilePath = @"~/Reports/"
            // Variables 
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            
            // Setup the report viewer object and get the array of bytes 
            ReportDataSource reportDataSource = new Microsoft.Reporting.WebForms.ReportDataSource { Name = argStrDataSetName, Value = argDataTable };
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(argStrFilePath + argStrFileName + ".rdlc");//"YourReportHere.rdlc";             
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(reportDataSource);

            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client. 

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.BufferOutput =true;
            HttpContext.Current.Response.ContentType = string.IsNullOrEmpty(mimeType) ? "application/pdf" : mimeType;
            //Forec to download
            if (argBoolIsForceDownload)
            {
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + argStrFileName + DateTime.Now.Ticks.ToString() + "." + extension);
            }
            else //Direct to browser if has Acrobat plug-in available
            {
                HttpContext.Current.Response.AddHeader("content-disposition", "inline; filename=" + argStrFileName + DateTime.Now.Ticks.ToString() + "." + extension);
            }

            HttpContext.Current.Response.BinaryWrite(bytes); // create the file 
            HttpContext.Current.Response.Flush(); // send it to the client to download 
            HttpContext.Current.Response.Close();
        }
        public void CreatePDF(string argStrFilePath, string argStrFileName, DataTable argDataTable, bool argBoolIsForceDownload)
        {
            // argStrFilePath = @"~/Reports/"
            // Variables 
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            // Setup the report viewer object and get the array of bytes 
            ReportDataSource reportDataSource = new Microsoft.Reporting.WebForms.ReportDataSource { Name = "DataSet1", Value = argDataTable };
            ReportViewer viewer = new ReportViewer();
            viewer.ProcessingMode = ProcessingMode.Local;
            viewer.LocalReport.ReportPath = HttpContext.Current.Server.MapPath(argStrFilePath + argStrFileName + ".rdlc");//"YourReportHere.rdlc";             
            viewer.LocalReport.DataSources.Clear();
            viewer.LocalReport.DataSources.Add(reportDataSource);

            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            // Now that you have all the bytes representing the PDF report, buffer it and send it to the client. 
            
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = string.IsNullOrEmpty(mimeType)?"application/pdf":mimeType;
           //Forec to download
            if (argBoolIsForceDownload)
            {
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + argStrFileName + DateTime.Now.Ticks.ToString() + "." + extension);
            }
            else //Direct to browser if has Acrobat plug-in available
            {            
                HttpContext.Current.Response.AddHeader("content-disposition", "inline; filename=" + argStrFileName+DateTime.Now.Ticks.ToString()+ "." + extension);
            }

            HttpContext.Current.Response.BinaryWrite(bytes); // create the file 
            HttpContext.Current.Response.Flush(); // send it to the client to download 
            HttpContext.Current.Response.Close();
        }
        #endregion CreatePDF
    }
}
