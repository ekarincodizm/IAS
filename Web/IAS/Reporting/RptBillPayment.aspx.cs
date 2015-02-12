using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using iTextSharp.text.pdf;
using IAS.Utils;
using System.Net;
using System.Data;
using System.Text;
using System.Globalization;
using System.Threading;
using IAS.Properties;

namespace IAS.Reporting
{
    public partial class RptBillPayment : basepage
    {
        private string BankAcc = "11010201010800";
        Utils.BarCode GenBarcode = new Utils.BarCode();
        public class lsPrint
        {
            //public int str
            public string HEAD_REQUEST_NO { get; set; }
            public string GROUP_REQUEST_NO { get; set; }
            public string PERSON_NO { get; set; }
            public string GROUP_AMOUNT { get; set; }
            public string SUBPAYMENT_DATE { get; set; }
            public string REMARK { get; set; }
        }
        public List<string> ListPayment
        {
            get
            {
                if (Session["ListPayment"] == null)
                {
                    Session["ListPayment"] = new List<string>();
                }

                return (List<string>)Session["ListPayment"];
            }

            set
            {
                Session["ListPayment"] = value;
            }
        }
        public Int32? AddAMT
        {
            get { return Session["AddAMT"] == null ? 0 : Session["AddAMT"].ToInt(); }
            set { Session["AddAMT"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            InitData();
            AddAMT = null;
        }
        private string ProcessValue;
        private void InitData()
        {
            string Click = Request.QueryString["Click"];
           
            string ReportFolder = base.ReportFilePath_Key;

            //string PDFFolder = base.PDFPath_Key;

            string PDF_Temp = base.PDFPath_Temp_Key;

            string PDF_OIC = base.PDFPath_OIC_Key;

            string PDF_Users = base.PDFPath_Users_Key;

            string FileNameInput = "RptPayment_Temp.pdf";

            string FileNameOutput = "RptPayment.pdf";
            string LicenseName = string.Empty;
            string AppleDate = string.Empty;
            string TestTime = string.Empty;
            string ExamPlace = string.Empty;
            string SumDetail = string.Empty;
            string detailAmt = string.Empty;
            Int32 detailAmt2 = 0;
            Int16 TranPayment = 0;
            var biz = new BLL.PaymentBiz();
            var ls = new List<RcvPaymentClass>();
            var para = Request.QueryString["Invoice"];
            //string ppath = "~\\Images\\ico_error.jpg";
            ////string ppath = "~\\Images\\copy_test.png";
            ////string ppath = "OIC/copy_txt_payment.jpg".Replace(@"/", @"\");
            //byte[] Copy_pic = biz.Copy_Img(Server.MapPath(ppath));// ไปเรียกฟังก์ชั่นพี่ติ๊ก มาใส่ byte[] หรือไม่ก็ให้ปรับ ไซต์ซะ 
            var ViewBillPayment = biz.GetConfigViewBillPayment();
            if (ViewBillPayment.DataResponse.Tables[0].Rows.Count > 0)
            {
                DataRow drTran = ViewBillPayment.DataResponse.Tables[0].Rows[0];
                TranPayment = Convert.ToInt16(drTran["ITEM_VALUE"]);
            }

            #region PrintMulti Page
            if (Click == "Print")
            {

                foreach (var item in ListPayment)
                {
                    string[] ChkUpload = item.Split(' ');
                    string GroupRequestNo = ChkUpload[0];
                    string UploadBySession = ChkUpload[1];
                    var res = biz.getGroupDetail(GroupRequestNo);


                    if (!res.IsError && res != null)
                    {

                        RcvPaymentClass rcv = new RcvPaymentClass();
                        //var item = res.DataResponse;
                        DataSet ds = res.DataResponse;
                        DataTable dt = ds.Tables[0];

                        if (dt.Rows.Count != 0)
                        {
                            StringBuilder newline = new StringBuilder();
                            StringBuilder newlineAmt = new StringBuilder();
                          
                            DataRow dr = dt.Rows[0];
                            if (dt.Rows.Count > 0  )
                            {
                                    if (dr["UPLOAD_BY_SESSION"].ToString().Length <= 4)
                                    {
                                        for (int i = 0; i < TranPayment; i++)
                                        {
                                            if (i < TranPayment)
                                            {
                                                if (dt.Rows[i]["sumAmt"].ToString() != "0")
                                                {
                                                    SumDetail = dt.Rows[i]["BillName"].ToString() + "{}";
                                                    detailAmt = string.Format("{0:n2}", Convert.ToInt32(dt.Rows[i]["sumAmt"].ToString())) + "{}";
                                                    newline.Append(SumDetail);
                                                    newlineAmt.Append(detailAmt);
                                                    if (i == dt.Rows.Count - 1)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }


                                            //   newline.AppendLine();
                                        }
                                        for (int s = TranPayment; s < dt.Rows.Count; s++)
                                        {

                                            if (dt.Rows[s]["sumAmt"].ToString() != "0")
                                            {
                                                detailAmt2 += Convert.ToInt32(dt.Rows[s]["sumAmt"].ToString());
                                            }
                                            AddAMT = detailAmt2;

                                        }
                                        if (dt.Rows.Count > TranPayment)
                                        {
                                            newline.Append("ค่าธรรมเนียมส่วนที่เหลือ" + "{}");
                                            newlineAmt.Append(string.Format("{0:n2}", AddAMT) + "{}");
                                        }
                                        rcv.PatitionName = newline.ToString();
                                        rcv.SumAmt = newlineAmt.ToString();
                                    }
                                
                            }
                         
                            StringBuilder newlineLicenseN = new StringBuilder();
                            StringBuilder newlineAppleDate = new StringBuilder();
                            StringBuilder newlineTimeTest = new StringBuilder();
                            StringBuilder newlineExamPlace = new StringBuilder();
                            #region รายละเอียดสมัครสอบ
                            if (dr["petition_type_code"].ToString() == "01" && dr["UPLOAD_BY_SESSION"].ToString().Length > 4)
                            {
                                var exam = biz.GetPaymentExamDetail(GroupRequestNo);
                                DataSet dsexam = exam.DataResponse;
                                DataTable dtexam = dsexam.Tables[0];
                                // var exam = biz.getBindbillPaymentExam(GroupRequestNo);
                                if (dtexam.Rows.Count > 0 && dtexam.Rows.Count <= TranPayment)
                                {
                                    for (int i = 0; i < dtexam.Rows.Count; i++)
                                    {
                                        var ExamDetail = biz.getBindbillPaymentExam(GroupRequestNo, dtexam.Rows[i]["TESTING_NO"].ToString(),
                                           dtexam.Rows[i]["APPLICANT_CODE"].ToString(), dtexam.Rows[i]["EXAM_PLACE_CODE"].ToString());

                                        DataSet dsExamDetail = ExamDetail.DataResponse;
                                        DataTable dtExamDetail = dsExamDetail.Tables[0];
                                        if (dtExamDetail.Rows.Count > 0)
                                        {
                                            DataRow drExamDetail = dtExamDetail.Rows[0];
                                            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                                            //    // lblTemp.Text = DateTime.Today.ToString("dddd dd MMMM yyyy"); => พุธ 29 สิงหาคม 2550
                                            if (drExamDetail["license_type_name"].ToString().Length > 26)
                                            {
                                                LicenseName = drExamDetail["license_type_name"].ToString().Substring(0, 26) + "{}";
                                            }
                                            else
                                            {
                                                LicenseName = drExamDetail["license_type_name"].ToString() + "{}";
                                            }
                                            AppleDate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drExamDetail["TESTING_DATE"])) + "{}";

                                            TestTime = drExamDetail["test_time"].ToString() + "{}";


                                            if (drExamDetail["exam_place_name"].ToString().Length > 25)
                                            {
                                                ExamPlace = drExamDetail["exam_place_name"].ToString().Substring(0, 25) + "{}";
                                            }
                                            else
                                            {
                                                ExamPlace = drExamDetail["exam_place_name"].ToString() + "{}";
                                            }

                                            newlineLicenseN.Append(LicenseName);
                                            newlineAppleDate.Append(AppleDate);
                                            newlineTimeTest.Append(TestTime);
                                            newlineExamPlace.Append(ExamPlace);
                                            //-----
                                            SumDetail = "ค่าสมัครสอบ" + "{}";
                                            detailAmt = string.Format("{0:n2}", Convert.ToInt32(dtexam.Rows[i]["amount"].ToString())) + "{}";
                                            newline.Append(SumDetail);
                                            newlineAmt.Append(detailAmt);
                                        }

                                    }

                                    rcv.LicenseName = newlineLicenseN.ToString();
                                    rcv.HeadapplyDate = Resources.propRptBillPayment_HeadapplyDate;
                                    rcv.applyDate = newlineAppleDate.ToString();
                                    rcv.HeadtimeTest = Resources.propRptBillPayment_HeadtimeTest;
                                    rcv.timeTest = newlineTimeTest.ToString();
                                    rcv.HeadExamPlace = Resources.propRptBillPayment_HeadExamPlace;
                                    rcv.ExamPlace = newlineExamPlace.ToString();
                                    rcv.ExamRemark = Resources.infoRptBillPayment_ExamRemark;

                                    rcv.PatitionName = newline.ToString();
                                    rcv.SumAmt = newlineAmt.ToString();
                                }
                            }
                            else if (dr["petition_type_code"].ToString() != "01" && dr["UPLOAD_BY_SESSION"].ToString().Length > 4)
                            {
                                var resLicense = biz.getGroupDetailLicense(GroupRequestNo);

                                DataTable dtLicense = resLicense.DataResponse.Tables[0];
                                if (dtLicense.Rows.Count > 0)
                                {

                                    for (int i = 0; i < TranPayment; i++)
                                    {
                                        if (i < TranPayment)
                                        {

                                            SumDetail = dtLicense.Rows[i]["BillName"].ToString() + "{}";
                                            detailAmt = string.Format("{0:n2}", Convert.ToInt32(dtLicense.Rows[i]["AMOUNT"].ToString())) + "{}";
                                            newline.Append(SumDetail);
                                            newlineAmt.Append(detailAmt);
                                            if (i == dtLicense.Rows.Count - 1)
                                            {
                                                break;
                                            }

                                        }


                                        //   newline.AppendLine();
                                    }
                                    rcv.PatitionName = newline.ToString();
                                    rcv.SumAmt = newlineAmt.ToString();
                                }
                            }
                            #endregion
                            var FindName = biz.getNamePaymentBy(GroupRequestNo);
                            DataSet ds2Dup = FindName.DataResponse;
                            DataTable dt2Dup = ds2Dup.Tables[0];

                            if (dt2Dup.Rows.Count != 0)
                            {
                                DataRow dr2Dup = dt2Dup.Rows[0];
                                string aaa = dr2Dup["upload_by_session"].ToString();
                                Int32 lenghtUpload = dr2Dup["upload_by_session"].ToString().Length.ToInt();
                                if (dr2Dup["upload_by_session"].ToString().Length.ToInt() == 4)
                                {
                                    rcv.PaymentBy = dr2Dup["name"].ToString();
                                }
                                else if (dr2Dup["upload_by_session"].ToString().Length.ToInt() == 3)
                                {
                                    rcv.PaymentBy = dr2Dup["association_name"].ToString();
                                }
                                else
                                {
                                    rcv.PaymentBy = dr2Dup["A_NAME"].ToString();
                                }

                            }
                            rcv.GROUP_REQUEST_NO = dr["GROUP_REQUEST_NO"].ToString().Insert(6, " ").Insert(11, " ");
                            rcv.GROUP_AMOUNT = string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"]));
                            rcv.GROUP_DATE = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["GROUP_DATE"]));
                            rcv.REMARK = dr["REMARK"].ToString();

                            //rcv.PaymentBy = base.UserProfile.
                            ConvertMoneyToThai(rcv.GROUP_AMOUNT);
                            rcv.BathThai = ProcessValue;
                            // rcv.BankAccountNumber = this.BankAcc;
                            rcv.ExpireDateString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                            rcv.Referance2No = dr["ref2"].ToString();
                            Encoding myUTF16 = Encoding.Unicode; // UTF16 is called "Unicode"
                            //GetBarCodeData(ls_BarCodeData, "Tahoma", 350, 80)

                            rcv.ExpireDateShortString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                            rcv.Referance1No = dr["GROUP_REQUEST_NO"].ToString().Insert(6, " ").Insert(11, " ");
                            string DataBarCode = "|099400064092702" + Char.ConvertFromUtf32(13) + dr["GROUP_REQUEST_NO"].ToString() + Char.ConvertFromUtf32(13) + dr["ref2"].ToString() + Char.ConvertFromUtf32(13) + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                            // string DataBarCode = "099400064092700" +0994000640927 dr["GROUP_REQUEST_NO"].ToString() + " " + dr["ref2"].ToString() + " " + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                            //rcv.BarCodeImage = Utils.BarCode.GenBarCodeToImage(DataBarCode);
                            rcv.BarCodeImage = Utils.BarCode.GetBarCodeData(DataBarCode, "Tahoma", 1000, 80);
                            rcv.BarCode = "|099400064092702" + " " + dr["GROUP_REQUEST_NO"].ToString() + " " + dr["ref2"].ToString() + " " + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                            //if (base.UserProfile.MemberType == 1)
                            //{
                            //    if (base.UserProfile.Id != UploadBySession)
                            //    {
                            //        rcv.SigImg = Copy_pic;
                            //    }
                            //}
                            //else if (base.UserProfile.MemberType == 2 || base.UserProfile.MemberType == 3)
                            //{
                            //    if (base.UserProfile.CompCode != UploadBySession)
                            //    {
                            //        rcv.SigImg = Copy_pic;
                            //    }
                            //}
                            //else
                            //{
                            //    rcv.SigImg = Copy_pic;
                            //}
                            ls.Add(rcv);
                            //}
                        }
                    }
                }

             

            }
            #endregion
            #region กดดูรายใบ
            else if (Click == "PrintS")
            {
                // var biz = new BLL.PaymentBiz();
                string[] ChkUpload = para.Split(' ');
                string GroupRequestNo = ChkUpload[0];
                string UploadBySession = ChkUpload[1];
                var res = biz.getGroupDetail(GroupRequestNo);
                if (!res.IsError && res != null)
                {
                    RcvPaymentClass rcv = new RcvPaymentClass();
                    //var item = res.DataResponse;
                    DataSet ds = res.DataResponse;
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        DataRow dr = dt.Rows[0];


                        StringBuilder newline = new StringBuilder();
                        StringBuilder newlineAmt = new StringBuilder();
               

                        //if (dr["UPLOAD_BY_SESSION"].ToString().Length <= 4)
                        //{
                            
                      //  }

                        StringBuilder newlineLicenseN = new StringBuilder();
                        StringBuilder newlineAppleDate = new StringBuilder();
                        StringBuilder newlineTimeTest = new StringBuilder();
                        StringBuilder newlineExamPlace = new StringBuilder();
                        #region รายละเอียดสมัครสอบ
                        if (dr["petition_type_code"].ToString() == "01" && dr["UPLOAD_BY_SESSION"].ToString().Length > 4)
                        {
                            var exam = biz.GetPaymentExamDetail(GroupRequestNo);
                            DataSet dsexam = exam.DataResponse;
                            DataTable dtexam = dsexam.Tables[0];
                            // var exam = biz.getBindbillPaymentExam(GroupRequestNo);
                            if (dtexam.Rows.Count > 0 && dtexam.Rows.Count <= TranPayment)
                            {
                                for (int i = 0; i < dtexam.Rows.Count; i++)
                                {
                                    var ExamDetail = biz.getBindbillPaymentExam(GroupRequestNo, dtexam.Rows[i]["TESTING_NO"].ToString(),
                                       dtexam.Rows[i]["APPLICANT_CODE"].ToString(), dtexam.Rows[i]["EXAM_PLACE_CODE"].ToString());

                                    DataSet dsExamDetail = ExamDetail.DataResponse;
                                    DataTable dtExamDetail = dsExamDetail.Tables[0];
                                    if (dtExamDetail.Rows.Count > 0)
                                    {
                                        DataRow drExamDetail = dtExamDetail.Rows[0];
                                        Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                                        //    // lblTemp.Text = DateTime.Today.ToString("dddd dd MMMM yyyy"); => พุธ 29 สิงหาคม 2550
                                        if (drExamDetail["license_type_name"].ToString().Length > 26)
                                        {
                                            LicenseName = drExamDetail["license_type_name"].ToString().Substring(0, 26) + "{}";
                                        }
                                        else
                                        {
                                            LicenseName = drExamDetail["license_type_name"].ToString() + "{}";
                                        }
                                        AppleDate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drExamDetail["TESTING_DATE"])) + "{}";

                                        TestTime = drExamDetail["test_time"].ToString() + "{}";

                                       
                                        if (drExamDetail["exam_place_name"].ToString().Length > 25)
                                        {
                                            ExamPlace = drExamDetail["exam_place_name"].ToString().Substring(0, 25) + "{}";
                                        }
                                        else
                                        {
                                            ExamPlace = drExamDetail["exam_place_name"].ToString() + "{}";
                                        }
                                        newlineLicenseN.Append(LicenseName);
                                        newlineAppleDate.Append(AppleDate);
                                        newlineTimeTest.Append(TestTime);
                                        newlineExamPlace.Append(ExamPlace);
                                        //-----
                                        SumDetail = "ค่าสมัครสอบ" + "{}";
                                        detailAmt = string.Format("{0:n2}", Convert.ToInt32(dtexam.Rows[i]["amount"].ToString())) + "{}";
                                        newline.Append(SumDetail);
                                        newlineAmt.Append(detailAmt);
                                    }

                                }

                                rcv.LicenseName = newlineLicenseN.ToString();
                                rcv.HeadapplyDate = Resources.propRptBillPayment_HeadapplyDate;
                                rcv.applyDate = newlineAppleDate.ToString();
                                rcv.HeadtimeTest = Resources.propRptBillPayment_HeadtimeTest;
                                rcv.timeTest = newlineTimeTest.ToString();
                                rcv.HeadExamPlace = Resources.propRptBillPayment_HeadExamPlace;
                                rcv.ExamPlace = newlineExamPlace.ToString();
                                rcv.ExamRemark = Resources.infoRptBillPayment_ExamRemark;

                                rcv.PatitionName = newline.ToString();
                                rcv.SumAmt = newlineAmt.ToString();
                            }
                            else
                            {
                                for (int i = 0; i < TranPayment; i++)
                                {
                                    if (i < TranPayment)
                                    {
                                        if (dt.Rows[i]["sumAmt"].ToString() != "0")
                                        {
                                            SumDetail = dt.Rows[i]["BillName"].ToString() + "{}";
                                            detailAmt = string.Format("{0:n2}", Convert.ToInt32(dt.Rows[i]["sumAmt"].ToString())) + "{}";
                                            newline.Append(SumDetail);
                                            newlineAmt.Append(detailAmt);
                                            if (i == dt.Rows.Count - 1)
                                            {
                                                break;
                                            }
                                        }
                                    }


                                    //   newline.AppendLine();
                                }
                                for (int s = TranPayment; s < dt.Rows.Count; s++)
                                {

                                    if (dt.Rows[s]["sumAmt"].ToString() != "0")
                                    {
                                        detailAmt2 += Convert.ToInt32(dt.Rows[s]["sumAmt"].ToString());
                                    }
                                    AddAMT = detailAmt2;

                                }
                                if (dt.Rows.Count > TranPayment)
                                {
                                    newline.Append("ค่าธรรมเนียมส่วนที่เหลือ" + "{}");
                                    newlineAmt.Append(string.Format("{0:n2}", AddAMT) + "{}");
                                }
                                rcv.PatitionName = newline.ToString();
                                rcv.SumAmt = newlineAmt.ToString();
                            }
                        }
                        #endregion
                        else if (dr["petition_type_code"].ToString() != "01" && dr["UPLOAD_BY_SESSION"].ToString().Length > 4)
                        {
                            var resLicense = biz.getGroupDetailLicense(GroupRequestNo);

                            DataTable dtLicense = resLicense.DataResponse.Tables[0];
                            if (dtLicense.Rows.Count > 0)
                            {

                                for (int i = 0; i < TranPayment; i++)
                                {
                                    if (i < TranPayment)
                                    {
                              
                                            SumDetail = dtLicense.Rows[i]["BillName"].ToString() + "{}";
                                            detailAmt = string.Format("{0:n2}", Convert.ToInt32(dtLicense.Rows[i]["AMOUNT"].ToString())) + "{}";
                                            newline.Append(SumDetail);
                                            newlineAmt.Append(detailAmt);
                                            if (i == dtLicense.Rows.Count - 1)
                                            {
                                                break;
                                            }
                                        
                                    }


                                    //   newline.AppendLine();
                                }
                                rcv.PatitionName = newline.ToString();
                                rcv.SumAmt = newlineAmt.ToString();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < TranPayment; i++)
                            {
                                if (i < TranPayment)
                                {
                                    if (dt.Rows[i]["sumAmt"].ToString() != "0")
                                    {
                                        SumDetail = dt.Rows[i]["BillName"].ToString() + "{}";
                                        detailAmt = string.Format("{0:n2}", Convert.ToInt32(dt.Rows[i]["sumAmt"].ToString())) + "{}";
                                        newline.Append(SumDetail);
                                        newlineAmt.Append(detailAmt);
                                        if (i == dt.Rows.Count - 1)
                                        {
                                            break;
                                        }
                                    }
                                }


                                //   newline.AppendLine();
                            }
                            for (int s = TranPayment; s < dt.Rows.Count; s++)
                            {

                                if (dt.Rows[s]["sumAmt"].ToString() != "0")
                                {
                                    detailAmt2 += Convert.ToInt32(dt.Rows[s]["sumAmt"].ToString());
                                }
                                AddAMT = detailAmt2;

                            }
                            if (dt.Rows.Count > TranPayment)
                            {
                                newline.Append("ค่าธรรมเนียมส่วนที่เหลือ" + "{}");
                                newlineAmt.Append(string.Format("{0:n2}", AddAMT) + "{}");
                            }
                            rcv.PatitionName = newline.ToString();
                            rcv.SumAmt = newlineAmt.ToString();
                        }
                        var FindName = biz.getNamePaymentBy(GroupRequestNo);
                        DataSet ds2Dup = FindName.DataResponse;
                        DataTable dt2Dup = ds2Dup.Tables[0];

                        if (dt2Dup.Rows.Count != 0)
                        {
                            DataRow dr2Dup = dt2Dup.Rows[0];
                            string aaa = dr2Dup["upload_by_session"].ToString();
                            Int32 lenghtUpload = dr2Dup["upload_by_session"].ToString().Length.ToInt();
                            if (dr2Dup["upload_by_session"].ToString().Length.ToInt() == 4)
                            {
                                rcv.PaymentBy = dr2Dup["name"].ToString();
                            }
                            else if (dr2Dup["upload_by_session"].ToString().Length.ToInt() == 3)
                            {
                                rcv.PaymentBy = dr2Dup["association_name"].ToString();
                            }
                            else
                            {
                                rcv.PaymentBy = dr2Dup["A_NAME"].ToString();
                            }

                        }
                        rcv.GROUP_REQUEST_NO = dr["GROUP_REQUEST_NO"].ToString().Insert(6, " ").Insert(11, " ");
                        rcv.GROUP_AMOUNT = string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"]));
                        rcv.GROUP_DATE = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["GROUP_DATE"]));
                        rcv.REMARK = dr["REMARK"].ToString();

                        //rcv.PaymentBy = base.UserProfile.
                        ConvertMoneyToThai(rcv.GROUP_AMOUNT);
                        rcv.BathThai = ProcessValue;
                        // rcv.BankAccountNumber = this.BankAcc;
                        if (dr["EXPIRATION_DATE"].ToString() == "") {
                            rcv.ExpireDateString = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                            rcv.ExpireDateShortString = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                        }
                        else
                        {
                            rcv.ExpireDateString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                            rcv.ExpireDateShortString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                        }
                        rcv.ExpireDateString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                        rcv.Referance2No = dr["ref2"].ToString();
                        Encoding myUTF16 = Encoding.Unicode; // UTF16 is called "Unicode"
                        //GetBarCodeData(ls_BarCodeData, "Tahoma", 350, 80)

                        rcv.ExpireDateShortString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                        rcv.Referance1No = dr["GROUP_REQUEST_NO"].ToString().Insert(6, " ").Insert(11, " ");
                        string DataBarCode = "|099400064092702" + Char.ConvertFromUtf32(13) + dr["GROUP_REQUEST_NO"].ToString() + Char.ConvertFromUtf32(13) + dr["ref2"].ToString() + Char.ConvertFromUtf32(13) + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                        // string DataBarCode = "099400064092700" +0994000640927 dr["GROUP_REQUEST_NO"].ToString() + " " + dr["ref2"].ToString() + " " + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                        //rcv.BarCodeImage = Utils.BarCode.GenBarCodeToImage(DataBarCode);
                        rcv.BarCodeImage = Utils.BarCode.GetBarCodeData(DataBarCode, "Tahoma", 1000, 80);
                        rcv.BarCode = "|099400064092702" + " " + dr["GROUP_REQUEST_NO"].ToString() + " " + dr["ref2"].ToString() + " " + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");

                        ls.Add(rcv);
                        //}
                    }
                }



            }
            #endregion
            #region PrintExam
            else if (Click == "PrintExam")
            {
                // var biz = new BLL.PaymentBiz();
                string[] ChkUpload = para.Split(' ');
                string GroupRequestNo = ChkUpload[0];
               // string UploadBySession = ChkUpload[1];
                var res = biz.getGroupDetail(GroupRequestNo);
                if (!res.IsError && res != null)
                {
                    RcvPaymentClass rcv = new RcvPaymentClass();
                    //var item = res.DataResponse;
                    DataSet ds = res.DataResponse;
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        DataRow dr = dt.Rows[0];


                        StringBuilder newline = new StringBuilder();
                        StringBuilder newlineAmt = new StringBuilder();
                       
                  
                        string sss = newline.ToString();
                        rcv.PatitionName = newline.ToString();
                        rcv.SumAmt = newlineAmt.ToString();
                        StringBuilder newlineLicenseN = new StringBuilder();
                        StringBuilder newlineAppleDate = new StringBuilder();
                        StringBuilder newlineTimeTest = new StringBuilder();
                        StringBuilder newlineExamPlace = new StringBuilder();
                        if (dr["petition_type_code"].ToString() == "01" && dr["UPLOAD_BY_SESSION"].ToString().Length > 4)
                        {
                            var exam = biz.GetPaymentExamDetail(GroupRequestNo);
                            DataSet dsexam = exam.DataResponse;
                            DataTable dtexam = dsexam.Tables[0];
                            // var exam = biz.getBindbillPaymentExam(GroupRequestNo);
                            if (dtexam.Rows.Count > 0 && dtexam.Rows.Count < TranPayment)
                            {
                                for (int i = 0; i < dtexam.Rows.Count; i++)
                                {
                                    var ExamDetail = biz.getBindbillPaymentExam(GroupRequestNo, dtexam.Rows[i]["TESTING_NO"].ToString(),
                                       dtexam.Rows[i]["APPLICANT_CODE"].ToString(), dtexam.Rows[i]["EXAM_PLACE_CODE"].ToString());

                                    DataSet dsExamDetail = ExamDetail.DataResponse;
                                    DataTable dtExamDetail = dsExamDetail.Tables[0];
                                    if (dtExamDetail.Rows.Count > 0)
                                    {
                                        DataRow drExamDetail = dtExamDetail.Rows[0];
                                        Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                                        //    // lblTemp.Text = DateTime.Today.ToString("dddd dd MMMM yyyy"); => พุธ 29 สิงหาคม 2550
                                        LicenseName = drExamDetail["license_type_name"].ToString() + "{}";

                                        AppleDate = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drExamDetail["TESTING_DATE"])) + "{}";

                                        TestTime = drExamDetail["test_time"].ToString() + "{}";

                                        ExamPlace = drExamDetail["exam_place_name"].ToString() + "{}";

                                        newlineLicenseN.Append(LicenseName);
                                        newlineAppleDate.Append(AppleDate);
                                        newlineTimeTest.Append(TestTime);
                                        newlineExamPlace.Append(ExamPlace);
                                        //-----
                                        SumDetail = "ค่าสมัครสอบ" + "{}";
                                        detailAmt = string.Format("{0:n2}", Convert.ToInt32(dtexam.Rows[i]["amount"].ToString())) + "{}";
                                        newline.Append(SumDetail);
                                        newlineAmt.Append(detailAmt);
                                    }

                                }

                                rcv.LicenseName = newlineLicenseN.ToString();
                                rcv.HeadapplyDate = Resources.propRptBillPayment_HeadapplyDate;
                                rcv.applyDate = newlineAppleDate.ToString();
                                rcv.HeadtimeTest = Resources.propRptBillPayment_HeadtimeTest;
                                rcv.timeTest = newlineTimeTest.ToString();
                                rcv.HeadExamPlace = Resources.propRptBillPayment_HeadExamPlace;
                                rcv.ExamPlace = newlineExamPlace.ToString();
                                rcv.ExamRemark = Resources.infoRptBillPayment_ExamRemark;

                                rcv.PatitionName = newline.ToString();
                                rcv.SumAmt = newlineAmt.ToString();
                            }
                            else if (dtexam.Rows.Count > TranPayment)
                            {
                                rcv.PatitionName = "ค่าสมัครสอบ" + dtexam.Rows.Count + "รายการ";
                                rcv.SumAmt = string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"]));
                            }
                        }
                  
                        var FindName = biz.getNamePaymentBy(GroupRequestNo);
                        DataSet ds2Dup = FindName.DataResponse;
                        DataTable dt2Dup = ds2Dup.Tables[0];

                        if (dt2Dup.Rows.Count != 0)
                        {
                            DataRow dr2Dup = dt2Dup.Rows[0];
                            string aaa = dr2Dup["upload_by_session"].ToString();
                            Int32 lenghtUpload = dr2Dup["upload_by_session"].ToString().Length.ToInt();
                            if (dr2Dup["upload_by_session"].ToString().Length.ToInt() == 4)
                            {
                                rcv.PaymentBy = dr2Dup["name"].ToString();
                            }
                            else if (dr2Dup["upload_by_session"].ToString().Length.ToInt() == 3)
                            {
                                rcv.PaymentBy = dr2Dup["off_name"].ToString();
                            }
                            else
                            {
                                rcv.PaymentBy = dr2Dup["A_NAME"].ToString();
                            }

                        }
                        rcv.GROUP_REQUEST_NO = dr["GROUP_REQUEST_NO"].ToString().Insert(6, " ").Insert(11, " ");
                        rcv.GROUP_AMOUNT = string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"]));
                        rcv.GROUP_DATE = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["GROUP_DATE"]));
                        rcv.REMARK = dr["REMARK"].ToString();

                        //rcv.PaymentBy = base.UserProfile.
                        ConvertMoneyToThai(rcv.GROUP_AMOUNT);
                        rcv.BathThai = ProcessValue;
                        // rcv.BankAccountNumber = this.BankAcc;
                        rcv.ExpireDateString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                        rcv.Referance2No = dr["ref2"].ToString();
                        Encoding myUTF16 = Encoding.Unicode; // UTF16 is called "Unicode"
                        //GetBarCodeData(ls_BarCodeData, "Tahoma", 350, 80)

                        rcv.ExpireDateShortString = string.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(dr["EXPIRATION_DATE"]));
                        rcv.Referance1No = dr["GROUP_REQUEST_NO"].ToString().Insert(6, " ").Insert(11, " ");
                        string DataBarCode = "|099400064092702" + Char.ConvertFromUtf32(13) + dr["GROUP_REQUEST_NO"].ToString() + Char.ConvertFromUtf32(13) + dr["ref2"].ToString() + Char.ConvertFromUtf32(13) + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                        // string DataBarCode = "099400064092700" +0994000640927 dr["GROUP_REQUEST_NO"].ToString() + " " + dr["ref2"].ToString() + " " + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
                        //rcv.BarCodeImage = Utils.BarCode.GenBarCodeToImage(DataBarCode);
                        rcv.BarCodeImage = Utils.BarCode.GetBarCodeData(DataBarCode, "Tahoma", 1000, 80);
                        rcv.BarCode = "|099400064092702" + " " + dr["GROUP_REQUEST_NO"].ToString() + " " + dr["ref2"].ToString() + " " + string.Format("{0:n2}", Convert.ToInt32(dr["GROUP_AMOUNT"])).Replace(",", "").Replace(".", "");
               
                        ls.Add(rcv);
                        //}
                    }
                }



            }
            #endregion
            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
            {
                ReportDocument rpt = new ReportDocument();

                rpt.Load(Server.MapPath(ReportFolder + "RptPayment.rpt"));

                rpt.SetDataSource(ls);

                BindReport(rpt);

                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));


                using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream output = new FileStream(Server.MapPath(PDF_Users + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader reader = new PdfReader(input);


                    /*ใส่ Password*/
                    //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                    /*ใส่ Password*/

                    PdfEncryptor.Encrypt(reader, output, true, string.Empty, string.Empty, PdfWriter.AllowPrinting);
                }

                //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                FileInfo File = new FileInfo(PathDelete);

                if (File.Exists)
                {
                    File.Delete();
                }
                //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง

                string FilePath = Server.MapPath(PDF_Users + FileNameOutput);
                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(FilePath);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }

                upd.Update();
            }
            else
            {
                ReportDocument rpt = new ReportDocument();

                rpt.Load(Server.MapPath(ReportFolder + "RptPayment.rpt"));

                rpt.SetDataSource(ls);

                BindReport(rpt);

                rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(PDF_Temp + FileNameInput));

                using (Stream input = new FileStream(Server.MapPath(PDF_Temp + FileNameInput), FileMode.Open, FileAccess.Read, FileShare.Read))
                using (Stream output = new FileStream(Server.MapPath(PDF_OIC + FileNameOutput), FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader reader = new PdfReader(input);
                    /*ใส่ Password*/
                    //PdfEncryptor.Encrypt(reader, output, true, "test", "test", PdfWriter.AllowPrinting);
                    /*ใส่ Password*/

                    PdfEncryptor.Encrypt(reader, output, true, string.Empty, string.Empty, PdfWriter.AllowPrinting);
                }

                //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง
                string PathDelete = Server.MapPath(PDF_Temp + FileNameInput);
                FileInfo File = new FileInfo(PathDelete);

                if (File.Exists)
                {
                    File.Delete();
                }
                //ลบไฟล์ ใน Folder PDF_Temp ทิ้ง

                string FilePath = Server.MapPath(PDF_OIC + FileNameOutput);
                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(FilePath);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }

                upd.Update();
            }
        }

        private void BindReport(ReportDocument rpt)
        {
            this.RptReciveReportViewer.ReportSource = rpt;
            //this.RptReciveReportViewer.DataBind();
        }

        public string ConvertMoneyToThai(string Amt)
        {
            string[] NumberWord;
            string[] NumberWord2;
            string Num3 = "";

            NumberWord = Amt.Split('.');
            NumberWord2 = NumberWord[0].Split(',');
            for (int i = 0; i <= NumberWord2.Length - 1; i++)
            {
                Num3 = Num3 + NumberWord2[i];
            }
            ProcessValue = SplitWord(Num3);
            if (NumberWord.Length > 1)
            {
                if (int.Parse(NumberWord[1]) > 0)
                {
                    ProcessValue = ProcessValue + Resources.propRptBillPayment_001 + SplitWord(NumberWord[1]) + Resources.propRptBillPayment_002;
                }
                else
                {
                    ProcessValue = ProcessValue + Resources.propRptBillPayment_003;
                }
            }
            else
            {
                ProcessValue = ProcessValue + Resources.propRptBillPayment_003;
            }
            return ProcessValue;
        }
        public string SplitWord(string numberVar)
        {
            int i = numberVar.Length;
            int k = 0;
            int n = i;
            int m = i;
            int b = 6;
            //char value2;
            char[] value1;
            string CurrencyWord = "";
            value1 = numberVar.ToCharArray();
            for (int a = 0; a <= i; a = a + 7)
            {
                if (n <= a + 7 && n > 0)
                {
                    b = n - 1;
                    if (i > 7)
                    {
                        k = 1;
                    }
                }
                else
                {
                    b = 6;
                }
                if (n > 0)
                {
                    for (int j = 0; j <= b; j++)
                    {
                        n--;
                        k++;
                        CurrencyWord = GetWord(value1[n].ToString(), k) + CurrencyWord;
                    }
                }
            }
            return CurrencyWord;
        }
        public string GetWord(string str1, int Num1)
        {
            string value1 = GetCurrency(Num1);
            switch (str1)
            {
                case "1":
                    if (Num1 == 1)
                    {
                        value1 = value1 + "เอ็ด";
                    }
                    else if (Num1 > 2)
                    {
                        value1 = "หนึ่ง" + value1;
                    }
                    break;
                case "2":
                    if (Num1 == 2)
                    {
                        value1 = "ยี่" + value1;
                    }
                    else
                    {
                        value1 = "สอง" + value1;
                    }
                    break;
                case "3":
                    value1 = "สาม" + value1;
                    break;
                case "4":
                    value1 = "สี่" + value1;
                    break;
                case "5":
                    value1 = "ห้า" + value1;
                    break;
                case "6":
                    value1 = "หก" + value1;
                    break;
                case "7":
                    value1 = "เจ็ด" + value1;
                    break;
                case "8":
                    value1 = "แปด" + value1;
                    break;
                case "9":
                    value1 = "เก้า" + value1;
                    break;
                default:
                    value1 = "";
                    break;
            }
            return value1;
        }
        public string GetCurrency(int Num2)
        {
            string value1;
            switch (Num2)
            {
                case 1:
                    value1 = "";
                    break;
                case 2:
                    value1 = "สิบ";
                    break;
                case 3:
                    value1 = "ร้อย";
                    break;
                case 4:
                    value1 = "พัน";
                    break;
                case 5:
                    value1 = "หมื่น";
                    break;
                case 6:
                    value1 = "แสน";
                    break;
                case 7:
                    value1 = "ล้าน";
                    break;
                default:
                    value1 = "";
                    break;
            }
            return value1;
        }
    }
}
