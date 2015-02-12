using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using iTextSharp.text.pdf;
using System.Net;
using IAS.MasterPage;
using IAS.BLL;
using IAS.Class;
using CrystalDecisions.Web;
using System.Web.Services;
using IAS.DTO;

namespace IAS.License
{
    public partial class Agreement_2 : basepage
    {
        #region Public Param & Session
        public object DataSource { get; set; }
        string outPutFile = "Agreement.pdf";
        public CrystalReportViewer cryReport { get { return LicenseCrystalReportViewer; } set { LicenseCrystalReportViewer = value; } }

        public Int32 Menu
        {
            get
            {
                return (Int32)Session["menu"];
            }
            set
            {
                Session["menu"] = value;
            }

        }


        public class lsPrint
        {
            //public int str
            public string C31 { get; set; }
            public string C32 { get; set; }
            public string C33 { get; set; }
            public string C34 { get; set; }
            public string C35 { get; set; }
            public string C41 { get; set; }
            public string C42 { get; set; }
        }

        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitMenu();
            }
            else
            {
                InitMenu();
            }
          
        }
        #endregion

        #region public & Private Fuction
        private void InitMenu()
        {
            string menu = this.Request.QueryString["M"];

            this.Menu = Convert.ToInt32(menu);
            //string menu1 = HttpContext.Current.Session["menu"].ToString();
            //string step1 = HttpContext.Current.Session["step"].ToString();

            switch (menu)
            {
                case "1":
                    InitDataPage1();
                    break;
                case "2":
                    InitDataPage2();
                    break;
                case "3":
                    InitDataPage3();
                    break;
                case "4":
                    InitDataPage4();
                    break;
                case "5":
                    InitDataPage5();
                    break;
                case "6":
                    InitDataPage6();
                    break;
                case "7":
                    InitDataPage7();
                    break;
                case "8":
                    InitDataPage8();
                    break;
                default:
                    break;
            }
            

        }

        #endregion

        public byte[] BYTE_IMG(string pathFile) //ดึงภาพจาก servicer
        { 
            BLL.PaymentBiz bizPayment = new BLL.PaymentBiz();
            byte[] Bimg = bizPayment.Signature_Img(pathFile);
            return Bimg;
        }

        #region ที่comment ไว้ข้างใน ใช้งานหมด เมื่อแก้แล้วเอาคอมเม้นท์ออก

        private void InitDataPage1()
        {
            ReportDocument rpt = new ReportDocument();
            PersonBiz biz = new PersonBiz();
            var res = biz.GetById(this.UserProfile.Id);
            var ls = new List<PersonLicenseAgreement>();

            if ((!res.IsError) && (res.DataResponse != null))
            {
                PersonLicenseAgreement ent = new PersonLicenseAgreement();
                ent.SEX = res.DataResponse.SEX;
                ent.NAMES = res.DataResponse.NAMES;
                ent.LASTNAME = res.DataResponse.LASTNAME;
                ent.MEMBER_TYPE = res.DataResponse.MEMBER_TYPE;
                ls.Add(ent);
            }

            if (this.Menu.Equals((int)DTO.MenuLicenses.Step1))
            {
                rpt.Load(Server.MapPath("../Reports/" + "RptAgreement_1.rpt"));
                rpt.SetDataSource(ls);
                //;rpt.SetDataSource(new[] { ls });
                //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + this.ArgOutPutFile));
                
                BindReport(rpt);

                //resArg.ResultMessage = true;

            }
            else if (this.Menu.Equals((int)DTO.MenuLicenses.Step2))
            {
                rpt.Load(Server.MapPath("../Reports/" + "RptAgreement_2.rpt"));
                rpt.SetDataSource(ls);
                //;rpt.SetDataSource(new[] { ls });
                //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + this.ArgOutPutFile));

                BindReport(rpt);

                //resArg.ResultMessage = true;

            }
            else if (this.Menu.Equals((int)DTO.MenuLicenses.Step3))
            {
                rpt.Load(Server.MapPath("../Reports/" + "RptAgreement_3.rpt"));
                rpt.SetDataSource(ls);
                //;rpt.SetDataSource(new[] { ls });
                //rpt.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath(this.ArgPath + this.ArgOutPutFile));

                BindReport(rpt);

                //resArg.ResultMessage = true;

            }
        }

        private void InitDataPage2()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_2.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        private void InitDataPage3()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_3.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        private void InitDataPage4()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_4.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        private void InitDataPage5()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_5.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        private void InitDataPage6()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_6.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        private void InitDataPage7()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_7.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        private void InitDataPage8()
        {

            //BLL.PersonBiz biz = new BLL.PersonBiz();
            //var res = biz.GetStatisticResetPassword(IDCard, FirstName, LastName);

            //DataTable dt = res.DataResponse.Tables.Count > 0 ? res.DataResponse.Tables[0] : null;



            //string MemberType = base.UserProfile.MemberType.ToString();

            //string ReportFolder = base.ReportFilePath_Key;

            //string PDF_Temp = base.PDFPath_Temp_Key;

            //string PDF_OIC = base.PDFPath_OIC_Key;


            //ReportDocument rpt = new ReportDocument();

            //rpt.Load(Server.MapPath(ReportFolder + "RptAgreement_8.rpt"));

            //rpt.DataDefinition.FormulaFields["UserName"].Text = "'" + base.UserProfile.Name + "'";

            //rpt.SetDataSource(dt);

            //BindReport(rpt);
        }

        #endregion

        public void BindReport(ReportDocument rpt)
        {
            LicenseCrystalReportViewer.Width.Equals("100%");
            LicenseCrystalReportViewer.ReportSource = rpt;
            //this.LicenseCrystalReportViewer.DataBind();
        }
    }
}