using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Data;
using IAS.DTO;
using AjaxControlToolkit;
using IAS.BLL;
using System.Text.RegularExpressions;
using IAS.Properties;
using System.Text;
using System.Configuration;
using System.Web.Configuration;
using IAS.Common.Email;

namespace IAS.Applicant
{
    public partial class SingleApplicant : basepage
    {

        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }

        public int PageSize;
        private Boolean _isCanRender = true;
        public Boolean IsCanRender
        {
            get { return _isCanRender; }
            set { _isCanRender = value; ViewState["IsCanRender"] = _isCanRender; }
        }
        private int daysCounter;
        public List<DTO.ExamRender> lsRender
        {
            get
            {
                if (Session["lsRender"] == null)
                {
                    Session["lsRender"] = new List<DTO.ExamRender>();
                }

                return (List<DTO.ExamRender>)Session["lsRender"];
            }
            set
            {
                Session["lsRender"] = value;
            }
        }
        public List<DTO.ExamRoomDDLTemp> lsEXRoomDDL
        {
            get
            {
                if (Session["ExRoomDDL"] == null)
                {
                    Session["ExRoomDDL"] = new List<DTO.ExamRoomDDLTemp>();
                }

                return (List<DTO.ExamRoomDDLTemp>)Session["ExRoomDDL"];
            }
            set
            {
                Session["ExRoomDDL"] = value;
            }
        }

        public List<DTO.AddApplicant> lstApplicant
        {
            get
            {
                if (Session["lstApplicant"] == null)
                {
                    Session["lstApplicant"] = new List<DTO.AddApplicant>();
                }

                return (List<DTO.AddApplicant>)Session["lstApplicant"];
            }
            set
            {
                Session["lstApplicant"] = value;
            }
        }
        public List<string> ListPrintPayment
        {
            get
            {
                if (Session["ListPrintPayment"] == null)
                {
                    Session["ListPrintPayment"] = new List<string>();
                }

                return (List<string>)Session["ListPrintPayment"];
            }

            set
            {
                Session["ListPrintPayment"] = value;
            }
        }

        protected void DefaultData() // ข้อมูลแรกเริ่มตั้งต้น (พี่ฟิลด์เขียนไว้มิ้วจับยกออกมาข้างนอก)
        {
            CalculateMinMaxYears();
            IsCanRender = true;
            base.HasPermit();
            lblS.Visible = false;
            ddlSpecial.Visible = false;
            string type = Request.QueryString["Type"];

            if (type == null)
                lblExam.Text = "ข้อมูลการสอบ";
            else
            {
                if (type == "ManageExam")
                    lblExam.Text = "สร้างรอบสอบ";
                else if (type == "Import")
                    lblExam.Text = "นำเข้าข้อมูลผู้สมัครสอบ";
            }

            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
            {
                if (type == null)
                {
                    pnlTable.Visible = false;
                    GetExamPlaceGroup();
                    GetExamTime();
                    GetMonth();
                    GetLicenseType();
                    lsRender.Clear();
                    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                    List<string> ls = biz.GetCompanyCodeByName("");
                    Session["lsCompCode"] = ls;
                    GetCompany("");
                    Div1.Visible = false;
                }
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
            {
                pnlExamSearch.Visible = true;
                pnlCalendar.Visible = false;
                pnlSearch.Visible = false;
                pnlTable.Visible = false;
                GetLicenseType();
                GetExamTime();
                GetExamPlaceGroup();
                GetMonth();
            }

            else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                pnlExamSearch.Visible = true;
                pnlCalendar.Visible = false;
                pnlSearch.Visible = false;
                pnlTable.Visible = false;

                GetAssociationLicenseByAssocCode();
                btnInsertExamSchedule.Visible = true;


                if ((type != null) && type == "ManageExam")
                {
                    pnlExamSearch.Visible = false;
                    pnlSearch.Visible = true;
                    pnlCalendar.Visible = true;
                }

                GetLicenseType();
                GetExamTime();
                GetExamPlaceGroup();
                GetMonth();
            }

            else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
            {
                GetExamSchedulePlaceGroup();
                GetExamPlace();
                GetLicenseTypeCreateTest();
                GetExamTime();
                GetMonth();
                lsRender.Clear();

                Div1.Visible = false;
                btnInsertExamSchedule.Visible = true;
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() ||
                base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
            {
                //GetExamPlaceGroupOIC();
                //GetLicenseTypeCreateTest();
                //GetExamTime();
                //GetMonth();
                //lsRender.Clear();
                //lblS.Visible = true;
                //ddlSpecial.Visible = true;
                //Div1.Visible = false;
                //btnInsertExamSchedule.Visible = true;

                if (type != null && type == "ManageExam")//สร้างรอบสอบ
                {
                    GetExamPlaceGroupOIC();
                    GetLicenseTypeCreateTest();
                    GetExamTime();
                    GetMonth();
                    lsRender.Clear();
                    lblS.Visible = true;
                    ddlSpecial.Visible = true;
                    Div1.Visible = false;
                    btnInsertExamSchedule.Visible = true;

                }
                else if (type != null && type == "Import")//สร้างรอบสอบ
                {
                    GetExamPlaceGroupOIC();//แก้ให้PageLoad OIC แสดงเหมือนบริษัทกับสมาคม
                    GetLicenseTypeCreateTest();
                    GetExamTime();
                    GetMonth();
                    pnlExamSearch.Visible = true;
                    pnlCalendar.Visible = false;
                    pnlSearch.Visible = false;
                    pnlTable.Visible = false;

                }
                else if (type == null)
                {
                    //นำเข้าผู้สมัครสอบ
                    GetExamPlaceGroupOIC();//แก้ให้PageLoad OIC แสดงเหมือนบริษัทกับสมาคม
                    GetLicenseTypeCreateTest();
                    GetExamTime();
                    GetMonth();
                    pnlExamSearch.Visible = false;
                    pnlCalendar.Visible = true;
                    pnlSearch.Visible = true;
                    pnlTable.Visible = false;
                }

            }
        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvExamSchedule":
                    lblTotal.Text = "จำนวน <b>" + Convert.ToString(total_row_count) + "</b> รายการ";
                    rows_per_page = (rows_per_page == 0 || rows_per_page == null) ? 1 : rows_per_page;
                    double Paggge = Math.Ceiling(total_row_count / rows_per_page);
                    txtTotalPage.Text = (total_row_count > 0) ? Convert.ToString(Paggge) : "0";
                    lblTotal.Visible = visible_or_disvisible;
                    txtTotalPage.Visible = visible_or_disvisible;
                    rowPerpage.Visible = visible_or_disvisible;
                    lblParaPage.Visible = visible_or_disvisible;
                    pageGo.Visible = visible_or_disvisible;
                    TXTrowperpage.Visible = visible_or_disvisible;

                    break;

                default:
                    break;
            }
        }
        protected void NPbutton(Button PreName, TextBox txtNum, Button NextName, string N_or_P, Label Maxpage)
        {
            int MaxP = 1;
            MaxP = ((Maxpage.Text.Trim() == "") || (Maxpage.Text.Trim() == "0")) ? MaxP : Maxpage.Text.ToInt();
            Maxpage.Text = (Maxpage.Text.ToInt() != MaxP) ? MaxP.ToString() : Maxpage.Text;
            if (N_or_P == "P")
            {
                if (txtNum.Text.ToInt() > 1)
                {
                    txtNum.Text = Convert.ToString(Convert.ToInt32(txtNum.Text) - 1);
                }
            }
            else if (N_or_P == "N")
            {
                txtNum.Text = Convert.ToString(Convert.ToInt32(txtNum.Text) + 1);

            }
            else
            {
                txtNum.Text = "1";
                PreName.Visible = false;
            }


            Hide_show(PreName, txtNum, NextName, N_or_P, MaxP);
        }
        protected void Hide_show(Button PreName, TextBox txtNum, Button NextName, string N_or_P, int MaxP)
        {
            if (txtNum.Text.ToInt() == 1)
            {
                PreName.Visible = false;
            }
            else
            {
                PreName.Visible = true;
            }
            upn.Update();
            if (txtNum.Text.ToInt() == MaxP)
            {
                NextName.Visible = false;
            }
            else
            {
                NextName.Visible = true;
            }
            btnInsertExamSchedule2.Visible = false;
        }
        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            BindExamScheduleByTableDefault(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            upn.Update();
        }
        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindExamScheduleByTableDefault(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            upn.Update();
        }
        #endregion Pageing_milk

        private int CheckToYear(int iYear)
        {
            if (iYear < 2500)
            {
                return (iYear + 543);
            }
            else
            {
                return iYear;
            }

        }

        private int GetExamPerBill()
        {
            BLL.DataCenterBiz biz = new DataCenterBiz();
            var res = biz.GetExamPerBill();
            int iCountPerBill = Convert.ToInt32(res.DataResponse);
            return iCountPerBill;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            daysCounter = 0;
            if (!IsPostBack)
            {
                DefaultData();
                txtNumberGvSearch.Text = "0";//milk
                cldExam.VisibleDate = DateTime.Now;
                txtYear.Text = CheckToYear(cldExam.VisibleDate.Year).ToString();
                this.PageSize = PAGE_SIZE_Key;
                SelectDataFrombase(cldExam.VisibleDate);

                CalTOR_2_1_5();

                txtScheduleDetailDateExam.Attributes.Add("readonly", "true");
                upn.Update();
            }
        }

        private void CalTOR_2_1_5()
        {
            #region TOR 2.1.5
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {


                if (txtScheduleExamCode.Text != "")
                {
                    if (Session["ExamDate"] != null)
                    {

                        //lsExamSubLicense = null;
                        //////btnChangeRoom.Visible = true;
                        //////btnDelete.Visible = true;
                        //////ddlExamRoom.Enabled = true;
                        //////btnAddExamRoom.Enabled = true;
                        getOldData();
                        //pnlModalExamSchedule.Style.Add("z-index", "199");
                        GetExamRoom();
                        EditMode(true);
                        gvExamRoom.Enabled = true;

                        if (Convert.ToDateTime(Session["ExamDate"].ToString()) < DateTime.Now.Date)
                        {
                            setShowRoom(false);

                            //////btnChangeRoom.Enabled = false;
                            //////btnDelete.Enabled = false;
                            //////gvExamRoom.Enabled = false;
                            lblRemark.Enabled = false;
                            ////////btnAddExamRoom.Enabled = false;
                            ////////ddlExamRoom.Enabled = false;
                            ////////btnExamScheduleSubmit.Visible = false;
                            ////////btnExamScheduleCancel.Visible = true;
                        }
                    }
                }
                else
                {
                    //lsExamSubLicense = null;
                    DefaultData_forRoom();
                    btnChangeRoom.Visible = false;
                    btnDelete.Visible = false;
                }

            }
            #endregion TOR 2.1.5
        }

        private void getOnwer()
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    string AssC = "";
                    DataCenterBiz biz = new DataCenterBiz();
                    if (base.UserRegType.GetEnumValue() == DTO.RegistrationType.Association.GetEnumValue())
                        AssC = base.UserProfile.CompCode;

                    var ls = biz.GetAssociationJoinLicense(AssC, ddlLicenseType.SelectedValue.ToString());
                    ddlExamOnwer.DataValueField = "ASSOCIATION_CODE";
                    ddlExamOnwer.DataTextField = "ASSOCIATION_NAME";

                    ddlExamOnwer.DataSource = ls.DataResponse;
                    ddlExamOnwer.DataBind();
                    ddlExamOnwer.Items.Insert(0, SysMessage.DefaultSelecting);

                    if (base.UserRegType.GetEnumValue() == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        ddlExamOnwer.SelectedValue = AssC;
                        ddlExamOnwer.Enabled = false;
                    }
                }

            }
            catch
            {
            }
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private Action<DropDownList, List<DTO.DataItem>> BindToDDLAr = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetAssociationLicenseByAssocCode()
        {
            BLL.DataCenterBiz biz = new DataCenterBiz();
            var ls = biz.GetAssociationLicenseByAssocCode(base.UserProfile.CompCode);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetExamPlaceGroupOIC()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetCompany(string strLicenseTypeCode)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            List<string> ls = (List<string>)Session["lsCompCode"];
            List<String> ls2;
            switch (strLicenseTypeCode)
            {
                case "01":
                case "07": ls2 = ls.Where(l => l.Contains("[1")).ToList();
                    break;
                case "02":
                case "05":
                case "06":
                case "08": ls2 = ls.Where(l => l.Contains("[2")).ToList();
                    break;
                default: ls2 = ls;
                    break;
            }
            ddlCompanyCode.DataSource = ls2;
            ddlCompanyCode.DataBind();
            ddlCompanyCode.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, "0"));
        }

        private void GetExamPlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroup(SysMessage.DefaultSelecting);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetExamSchedulePlaceGroup()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, base.UserProfile.CompCode);
            BindToDDL(ddlPlaceGroup, ls.DataResponse);
        }

        private void GetLicenseTypeBySingle()
        {
            BLL.DataCenterBiz biz = new DataCenterBiz();
            var ls = biz.GetLicenseType("");
            BindToDDL(ddlTypeLicense, ls.DataResponse);

        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByCompCode(base.UserProfile.CompCode);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetLicenseTypeCreateTest()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByCreateTest(base.UserProfile);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
        }

        private void GetExamTime()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamTime(SysMessage.DefaultSelecting);
            BindToDDL(ddlTime, ls.DataResponse);
            BindToDDL(ddlDetailTimeExamCode, ls.DataResponse);
        }

        private void GetMonth()
        {
            var list = IAS.Utils.DateUtil.GetMonthList(SysMessage.DefaultSelecting);
            ddlMonth.DataTextField = "Name";
            ddlMonth.DataValueField = "Id";
            ddlMonth.DataSource = list;
            ddlMonth.DataBind();
            ddlMonth.SelectedValue = string.Format("{0:00}", DateTime.Now.Month);
        }

        private void GetExamPlace()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetExamPlaceByCompCode(ddlPlaceGroup.SelectedIndex == 0 ? "" : ddlPlaceGroup.SelectedItem.Value, ddlPlaceGroup.SelectedItem.Value);
            BindToDDL(ddlPlace, ls.DataResponse);
            ddlPlace.Items.Insert(0, SysMessage.DefaultSelecting);
        }

        private void GetLicneseByAgentType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByAgentType(base.UserProfile.AgentType);
            BindToDDL(ddlTypeLicense, ls.DataResponse);
            ddlTypeLicense.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, ""));
        }

        private void BindExamScheduleByTable(Boolean CountAgain)
        {

            if (IsCanRender == true)
            {

                string strExamPlaceGroup = Session["strExamPlaceGroup"].ToString();
                string strExamPlace = Session["strExamPlace"].ToString();
                string strLicenseType = Session["strLicenseType"].ToString();
                string strYearMonth = Session["strYearMonth"].ToString();
                string strTime = Session["strTime"].ToString();

                string Owner = "";
                string type = Request.QueryString["Type"];
                if ((type != null) && (type == "ManageExam"))
                {
                    if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                    {
                        strExamPlaceGroup = base.UserProfile.CompCode.ToString();
                        Owner = "";
                    }
                    else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        strExamPlaceGroup = "";
                        Owner = base.UserProfile.CompCode.ToString();
                    }

                }

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.ToInt() == 0))
                {
                    rowPerpage.Text = PAGE_SIZE_Key.ToString();
                    PageSize = PAGE_SIZE_Key;
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                if (CountAgain)
                {
                    #region Page
                    var CountPage = biz.GetExamByCriteriaDefault(strExamPlaceGroup, strExamPlace, strLicenseType, null, strYearMonth, strTime, null, resultPage, PageSize, true, Owner);

                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvExamSchedule, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);

                            if (totalROWs == 0)
                                txtTotalPage.Text = "1";
                        }
                        else
                        {
                            VisibleGV(gvExamSchedule, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }

                var ls = biz.GetExamByCriteriaDefault(strExamPlaceGroup, strExamPlace, strLicenseType, null, strYearMonth, strTime, null, resultPage, PageSize, false, Owner);

                //var ls = biz.GetExamByCriteriaDefault(strExamPlaceGroup, strExamPlace, strLicenseType, base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, strTime, null, resultPage, PageSize, false);

                DataSet ds = ls.DataResponse;
                DataTable dt = ds.Tables[0];

                Div1.Visible = true;
                gvExamSchedule.DataSource = dt;
                gvExamSchedule.DataBind();
                if (gvExamSchedule.Rows.Count > 0)
                {
                    btnExportExcel.Visible = true;
                }
                else
                {
                    btnExportExcel.Visible = false;
                }
            }


        }

        // โหลดข้อมูลตาราง
        private void BindExamScheduleByTableDefault(Boolean CountAgain) //milk มาแก้ไขเพื่อเพิ่ม paging เท่านั้น
        {
            if (IsCanRender == true)
            {
                string strExamPlaceGroup = ddlPlaceGroup.SelectedValue;
                string Owner = "";
                string type = Request.QueryString["Type"];
                if ((type != null) && (type == "ManageExam"))
                {
                    if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                    {
                        strExamPlaceGroup = base.UserProfile.CompCode.ToString();
                        Owner = "";
                    }
                    else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        strExamPlaceGroup = "";
                        Owner = base.UserProfile.CompCode.ToString();
                    }

                }

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.Trim() == "0"))
                {
                    rowPerpage.Text = PAGE_SIZE_Key.ToString();
                    PageSize = PAGE_SIZE_Key;
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                string strYearMonth = string.Empty;
                if (ddlMonth.SelectedValue != null)
                {
                    strYearMonth = (ConvertToYearMonth(ddlMonth.SelectedValue.ToInt()));
                }
                else
                {
                    strYearMonth = (ConvertToYearMonth(DateTime.Now.Month.ToInt()));
                }



                if (CountAgain)
                {
                    #region Page
                    var CountPage = biz.GetExamByCriteriaDefault(strExamPlaceGroup, ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue, ddlTypeLicense.SelectedValue == "" ? null : ddlTypeLicense.SelectedValue, base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue, null, resultPage, PageSize, true, Owner);


                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvExamSchedule, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvExamSchedule, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }

                var ls = biz.GetExamByCriteriaDefault(strExamPlaceGroup, ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue, ddlTypeLicense.SelectedValue == "" ? null : ddlTypeLicense.SelectedValue, base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue, null, resultPage, PageSize, false, Owner);
                DataSet ds = ls.DataResponse;
                DataTable dt = ds.Tables[0];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnExportExcel.Visible = true;
                }
                else
                {
                    btnExportExcel.Visible = false;
                }
                Div1.Visible = true;
                gvExamSchedule.Visible = true;
                gvExamSchedule.DataSource = dt;
                gvExamSchedule.DataBind();
                table_Paging.Visible = true;
            }


        }

        // ทุกๆการเลือกแสดงผลข้อมูลสอบ
        protected void rblDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(sender, e);
            //CheckDisplayExam();
            if (rblDisplay.SelectedValue == "2")
            {
                string type = Request.QueryString["Type"];
                if ((type != null) && (type == "ManageExam"))
                {
                    btnInsertExamSchedule2.Visible = false;
                    btnExportExcel.Visible = false;
                    btnInsertExamSchedule2.Visible = true;
                }
                else
                {
                    btnInsertExamSchedule2.Visible = true;
                    btnExportExcel.Visible = true;
                    btnInsertExamSchedule2.Visible = false;
                }
            }
            else
            {
                btnInsertExamSchedule2.Visible = false;
                btnInsertExamSchedule2.Visible = false;
            }
            checkMode();
        }

        protected void gvExamSchedule_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string type = Request.QueryString["Type"];


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region parameter
                Label lblTotal = ((Label)e.Row.FindControl("lblTotalApply"));
                Label lblExamAdmission = ((Label)e.Row.FindControl("lblExamAdmission"));
                Label lblLicenseTypeCodeNumber = ((Label)e.Row.FindControl("lblLicenseTypeCodeNumber"));
                Label lblExamDate = ((Label)e.Row.FindControl("lblExamDate"));
                Label lblExamRegister = ((Label)e.Row.FindControl("lblExamRegister"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));
                LinkButton lnkExamRegister = ((LinkButton)e.Row.FindControl("lnkExamRegister"));
                LinkButton lnkExamManageTest = ((LinkButton)e.Row.FindControl("lnkExamManageTest"));
                Label lblExamOwner = ((Label)e.Row.FindControl("lblExamOwner"));

                lblExamAdmission.Text = lblTotal.Text == null ? "0" : lblExamAdmission.Text == null ? "0" : lblExamAdmission.Text;
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lnkExamNumber");
                LinkButton lb = e.Row.FindControl("lnkExamNumber") as LinkButton;
                #endregion parameter


                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() ||
                    base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                {
                    #region OIC OICAGENT TESTCENTER
                    if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                        base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                        lnkExamRegister.Visible = true;
                    else
                    {
                        lnkExamRegister.Visible = false;
                        lnkExamRegister.Text = "";
                    }

                    if (type != null && type == "ManageExam")//สร้างรอบสอบ
                    {
                        lnkExamManageTest.Visible = true;
                        lnkExamManageTest.Text = "แก้ไข";
                        if (Convert.ToDateTime(lblExamDate.Text) <= DateTime.Now.Date)
                            lnkExamManageTest.Text = "เพิ่มเติม";

                    }
                    else
                    {
                        lnkExamManageTest.Visible = false;
                        lnkExamManageTest.Text = "";
                    }
                    #endregion OIC OICAGENT TESTCENTER
                }
                else if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() ||
                    base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() ||
                        base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    #region General Insurance Association
                    if (!string.IsNullOrEmpty(lblExamDate.Text))
                    {
                        if (base.UserProfile.MemberType != DTO.RegistrationType.Association.GetEnumValue())
                        {
                            lnkExamManageTest.Visible = false;
                            lnkExamManageTest.Text = "";
                        }
                        else
                        {

                            if (type != null && type == "ManageExam")
                            {
                                if (base.UserProfile.CompCode == lblExamOwner.Text)//สร้างรอบสอบ
                                {
                                    lnkExamManageTest.Visible = true;
                                    lnkExamManageTest.Text = "แก้ไข";
                                    if (Convert.ToDateTime(lblExamDate.Text) <= DateTime.Now.Date)
                                        lnkExamManageTest.Text = "เพิ่มเติม";
                                }
                                else
                                {
                                    lnkExamManageTest.Visible = true;
                                    lnkExamManageTest.Text = "เพิ่มเติม";
                                    //ไม่ใช่เจ้าของรอบสอบก็รวรจะดูข้อมูลได้เหมือนกัน
                                }
                            }
                            else
                            {
                                lnkExamManageTest.Visible = false;
                                lnkExamManageTest.Text = "";
                            }
                        }

                        DateTime dtExam = Convert.ToDateTime(lblExamDate.Text);
                        DateTime dtToday = DateTime.Today.AddDays(-1);
                        DateTime dtFinish = DateTime.Today.AddDays(+5);

                        if (dtExam.Date == dtExam.Date && dtExam.Date > dtToday && dtExam.Date <= dtFinish || dtExam.Date < DateTime.Today)
                        {
                            if (type != null && type == "ManageExam")
                            {
                                lnkExamRegister.Text = "";
                                lnkExamRegister.Visible = false;
                                lnkExamRegister.Enabled = false;
                            }
                            else
                            {
                                lblExamRegister.Visible = true;
                                lblExamRegister.ForeColor = System.Drawing.Color.Gray;
                                lblExamRegister.Font.Strikeout = true;
                            }
                        }
                        else
                        {

                            lnkExamRegister.Text = "สมัครสอบ";
                            lnkExamRegister.Visible = true;
                            lnkExamRegister.Enabled = true;
                        }

                    }
                    #endregion General Insurance Association
                }
                else
                {
                    lnkExamRegister.Text = "";
                    lnkExamRegister.Visible = true;
                    lnkExamRegister.Enabled = true;
                }

                if (type != null && type == "ManageExam")
                {
                    lnkExamRegister.Text = "";
                    lnkExamRegister.Visible = false;
                    lnkExamRegister.Enabled = false;
                }
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
            }
        }

        // ทุกๆครั้งที่เลือกรายการสอบแบบปฏิทิน
        protected void lnkExamNumber_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strExamNumber = (LinkButton)gr.FindControl("lnkExamNumber");
            var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            var strExamPlaceName = (Label)gr.FindControl("lblPlaceName");
            var strExamManageTest = (LinkButton)gr.FindControl("lnkExamManageTest");
            var strExamRegister = (LinkButton)gr.FindControl("lnkExamRegister");
            //strExamManageTest.Visible = false;
            //strExamRegister.Visible = true;

            #region Save Detail Exam By Fuse
            var lblExamNumber = (Label)gr.FindControl("ExamNumberNo");
            var lblExamDate = (Label)gr.FindControl("lblExamDate");
            var lblExamTime = (Label)gr.FindControl("lblExamTime");
            var lblExamPlaceGroup = (Label)gr.FindControl("lblGroupExamYard");
            var lblExamPlace = (Label)gr.FindControl("lblPlaceName");
            var lblProvince = (Label)gr.FindControl("lblProvice");
            var lblSeat = (Label)gr.FindControl("lblExamAdmission");
            var lblLicenseTypeName = (Label)gr.FindControl("lblLicenseTypeName");
            var lblExamFee = (Label)gr.FindControl("lblExamFee");
            var lblAgentType = (Label)gr.FindControl("lblAgentType");
            var lblPlaceCode = (Label)gr.FindControl("lblPlaceCode");
            var lblTestTimeCode = (Label)gr.FindControl("lblTestTimeCode");
            var lblLicenseTypeCode = (Label)gr.FindControl("lblLicenseTypeCode");
            var lblProvineCode = (Label)gr.FindControl("lblProvinceCode");
            var lblExamPlaceGroupCode = (Label)gr.FindControl("lblExamPlaceGroupCode");


            Session["ExamNumber"] = lblExamNumber.Text;
            Session["ExamDate"] = lblExamDate.Text;
            Session["ExamTime"] = lblExamTime.Text;
            Session["ExamPlaceGroup"] = lblExamPlaceGroup.Text;
            Session["ExamPlace"] = lblExamPlace.Text;
            Session["Province"] = lblProvince.Text;
            Session["Seat"] = lblSeat.Text;
            Session["LicenseTypeName"] = lblLicenseTypeName.Text;
            Session["ExamFee"] = lblExamFee.Text;
            Session["AgentType"] = lblAgentType.Text;
            Session["PlaceCode"] = lblPlaceCode.Text;

            Session["ExamPlaceCode"] = strPlaceCode.Text;
            Session["TestingNo"] = strExamNumber.Text;
            Session["TestingDate"] = lblExamDate.Text;
            Session["TestTimeCode"] = lblTestTimeCode.Text;
            Session["LicenseTypeCode"] = lblLicenseTypeCode.Text;
            Session["ProvinceCode"] = lblProvineCode.Text;
            Session["ExamPlaceGroupCode"] = lblExamPlaceGroupCode.Text;
            #endregion


            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();

            string type = Request.QueryString["Type"];


            //if (type != null && type == "ManageExam")//สร้างรอบสอบ
            //{
            //   if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() ||
            //           base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue() ||
            //         base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            //    {
            //        UplExamSchedule.Update();

            //        #region popupRoom
            //        txtScheduleExamCode.Text = strExamNumber.Text;
            //        CalTOR_2_1_5();
            //        #endregion popupRoom

            //        btnDelete.Visible = true;
            //        ModalExamSchedule.Show();
            //    }
            //}
            //else
            //    if (type == null )
            //{
            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
            {
                var strLicenseTypeCodeNumber = (Label)gr.FindControl("lblLicenseTypeCodeNumber");
                GetCompany(strLicenseTypeCodeNumber.Text);
                FilterCompanyByLicenseCode(strLicenseTypeCodeNumber.Text);


                if (!string.IsNullOrEmpty(strExamNumber.Text) && !string.IsNullOrEmpty(strPlaceCode.Text))
                {
                    EditMode(strExamNumber.Text, strPlaceCode.Text, strExamPlaceName.Text);
                }

                ModSingleApplicant.Show();

                UplPopUp.Update();
            }

            else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
            {
                upn.Update();
                Session["TestingNo"] = strExamNumber.Text;
                Response.Redirect("~/Applicant/GroupApplicantDetail.aspx?");
            }

            //}
        }

        protected void lnkExamManageTest_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["Type"];

            if (type != null && type == "ManageExam")//สร้างรอบสอบ
            {
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                var strExamNumber = (LinkButton)gr.FindControl("lnkExamNumber");
                var strPlaceCode = (Label)gr.FindControl("lblPlaceCode");
                var strExamPlaceName = (Label)gr.FindControl("lblPlaceName");
                var strExamManageTest = (LinkButton)gr.FindControl("lnkExamManageTest");
                //strExamManageTest.Visible = true;
                //strExamNumber.Visible = false;

                //lsExamSubLicense = null;
                //Session["ExamSubLicense"] = null;

                #region Save Detail Exam By Fuse
                var lblExamNumber = (Label)gr.FindControl("ExamNumberNo");
                var lblExamDate = (Label)gr.FindControl("lblExamDate");
                var lblExamTime = (Label)gr.FindControl("lblExamTime");
                var lblExamPlaceGroup = (Label)gr.FindControl("lblGroupExamYard");
                var lblExamPlace = (Label)gr.FindControl("lblPlaceName");
                var lblProvince = (Label)gr.FindControl("lblProvice");
                var lblSeat = (Label)gr.FindControl("lblExamAdmission");
                var lblLicenseTypeName = (Label)gr.FindControl("lblLicenseTypeName");
                var lblExamFee = (Label)gr.FindControl("lblExamFee");
                var lblAgentType = (Label)gr.FindControl("lblAgentType");
                var lblPlaceCode = (Label)gr.FindControl("lblPlaceCode");
                var lblTestTimeCode = (Label)gr.FindControl("lblTestTimeCode");
                var lblLicenseTypeCode = (Label)gr.FindControl("lblLicenseTypeCode");
                var lblProvineCode = (Label)gr.FindControl("lblProvinceCode");
                var lblExamPlaceGroupCode = (Label)gr.FindControl("lblExamPlaceGroupCode");

                Session["ExamManageTest"] = lblExamNumber.Text;
                Session["ExamDate"] = lblExamDate.Text;
                Session["ExamTime"] = lblExamTime.Text;
                Session["ExamPlaceGroup"] = lblExamPlaceGroup.Text;
                Session["ExamPlace"] = lblExamPlace.Text;
                Session["Province"] = lblProvince.Text;
                Session["Seat"] = lblSeat.Text;
                Session["LicenseTypeName"] = lblLicenseTypeName.Text;
                Session["ExamFee"] = lblExamFee.Text;
                Session["AgentType"] = lblAgentType.Text;
                Session["PlaceCode"] = lblPlaceCode.Text;

                Session["ExamPlaceCode"] = strPlaceCode.Text;
                Session["TestingNo"] = strExamNumber.Text;
                Session["TestingDate"] = lblExamDate.Text;
                Session["TestTimeCode"] = lblTestTimeCode.Text;
                Session["LicenseTypeCode"] = lblLicenseTypeCode.Text;
                Session["ProvinceCode"] = lblProvineCode.Text;
                Session["ExamPlaceGroupCode"] = lblExamPlaceGroupCode.Text;
                #endregion


                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue() ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    UplExamSchedule.Update();

                    #region popupRoom
                    txtScheduleExamCode.Text = strExamNumber.Text;
                    CalTOR_2_1_5();
                    #endregion popupRoom

                    btnDelete.Visible = true;
                    ModalExamSchedule.Show();
                }

                if (strExamManageTest.Text == "แก้ไข")
                    Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.DEFULT.ToString();
                else if (strExamManageTest.Text == "เพิ่มเติม")
                    Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.VIEW.ToString();
                checkMode();
            }
        }

        // โชว์ข้อมูลตามการเลือกรหัสสอบ
        private void BindExamByTestingNoAndPlaceCode(string testingNo, string placeCode)
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var exam = biz.GetExamByTestingNoAndPlaceCode(testingNo, placeCode);

            txtScheduleExamCode.Text = exam.DataResponse.TESTING_NO;
            txtScheduleDetailDateExam.Text = exam.DataResponse.TESTING_DATE.ToString("dd/MM/yyyy");

            ddlDetailTimeExamCode.SelectedValue = exam.DataResponse.TEST_TIME_CODE;

        }

        // เช็คการระบุต้นสังกัดโดยประเภทใบอนุญาต
        private void FilterCompanyByLicenseCode(string strLicenseTypeCodeNumber)
        {
            if (strLicenseTypeCodeNumber == "03" || strLicenseTypeCodeNumber == "04")
            {
                ddlCompanyCode.Enabled = false;
            }
            else
            {
                ddlCompanyCode.Enabled = true;
            }
        }

        // คำณวณปี
        private void CalculateMinMaxYears()
        {
            int iYear = DateTime.Now.Year;
            txtYear.Text = Convert.ToString(iYear + 543);

            NumericUpDownExtender1.Maximum = iYear + 553;
            NumericUpDownExtender1.Minimum = 0;
        }

        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetExamPlace();
            if (rblDisplay.SelectedValue == "1")
            {
                IsCanRender = false;
                table_Paging.Visible = false; //เป็นปฏิทิินเลยซ่อนการแบ่งเพจไว้
            }
            else if (rblDisplay.SelectedValue == "2")
            {
                lsRender.Clear();

                IsCanRender = true;
                pnlCalendar.Visible = false;
                pnlTable.Visible = true;
                BindExamScheduleByTable(true);
                table_Paging.Visible = true;
            }
        }

        private void ClearValue()
        {
            txtDetailExamCode.Text = string.Empty;
            txtDetailDateExam.Text = string.Empty;

        }

        private void EditMode(string testingNo, string placeCode, string examPlaceName)
        {
            //if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            //{
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            var exam = biz.GetExamByTestingNoAndPlaceCode(testingNo, placeCode);
            txtDetailExamCode.Text = exam.DataResponse.TESTING_NO;
            txtDetailDateExam.Text = exam.DataResponse.TESTING_DATE.ToString("dd/MM/yyyy");
            txtDetailPlaceCode.Text = examPlaceName;
            txtProvincePopup.Text = exam.DataResponse.NAME;
            txtTestTime.Text = exam.DataResponse.TEST_TIME;
            if (exam.DataResponse.EXAM_PLACE_GROUP_NAME != null)
            {
                txtExamPlaceName.Text = exam.DataResponse.EXAM_PLACE_GROUP_NAME;
                //if (exam.DataResponse.EXAM_OWNER == exam.DataResponse.EXAM_PLACE_GROUP_CODE)
                //{
                //    txtExamOwner.Text = exam.DataResponse.EXAM_PLACE_GROUP_NAME;
                //}
            }
            else
            {
                txtExamPlaceName.Text = exam.DataResponse.ASSOCIATION_NAME;
                //if (exam.DataResponse.EXAM_OWNER != null)
                //{
                //    txtExamOwner.Text = exam.DataResponse.ASSOCIATION_NAME;
                //}
            }

            if (!string.IsNullOrEmpty(exam.DataResponse.EXAM_OWNER))
            {
                if (exam.DataResponse.EXAM_OWNER == exam.DataResponse.ASSOCIATION_CODE)
                {
                    txtExamOwner.Text = exam.DataResponse.ASSOCIATION_NAME;
                }
                if (exam.DataResponse.EXAM_OWNER == exam.DataResponse.EXAM_PLACE_GROUP_CODE)
                {
                    txtExamOwner.Text = exam.DataResponse.EXAM_PLACE_GROUP_NAME;
                }


            }
            txtLicenseTypeName.Text = exam.DataResponse.LICENSE_TYPE_NAME;
            if (exam.DataResponse.SPECIAL == "Y")
            {
                chkSpecial.Checked = true;
            }




            //}
        }

        private string ConvertToYearMonth(int iMonth)
        {
            int iYear = (txtYear.Text.ToInt() > 2500) ? txtYear.Text.ToInt() - 543 : txtYear.Text.ToInt();
            DateTime dt = new DateTime(iYear, iMonth, 1);
            string strYear = Convert.ToString(dt.Year);
            string strMonth = dt.Month.ToString();
            if (dt.Month < 10)
            {
                strMonth = "0" + strMonth;
            }
            string strYearMonth = strYear + strMonth;
            return strYearMonth;
        }

        private string ConvertToYearMonth(int tx, int iMonth)
        {
            int iYear = (tx > 2500) ? tx - 543 : tx;
            DateTime dt = new DateTime(iYear, iMonth, 1);
            string strYear = Convert.ToString(dt.Year);
            string strMonth = dt.Month.ToString();
            if (dt.Month < 10)
            {
                strMonth = "0" + strMonth;
            }
            string strYearMonth = strYear + strMonth;
            return strYearMonth;
        }

        protected void cldExam_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            //btnSearch_Click(sender,e);
            pnlTable.Visible = false;
            cldExam.SelectedDates.Clear();

            #region set background today
            if (cldExam.VisibleDate.ToString("MMyyyy") == DateTime.Now.ToString("MMyyyy"))
            {
                cldExam.TodaysDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                cldExam.VisibleDate = cldExam.TodaysDate;
                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
            }
            #endregion

            SelectDataFrombase(((Calendar)sender).VisibleDate);

            #region old Code
            //if (e.NewDate.Month < 10)
            //{
            //    ddlMonth.SelectedValue = "0" + Convert.ToString(e.NewDate.Month);
            //}
            //else
            //{
            //    ddlMonth.SelectedValue = Convert.ToString(e.NewDate.Month);
            //}
            //txtYear.Text = Convert.ToString(e.NewDate.Year + 543);
            //gvExamSchedule.DataSource = null;
            //gvExamSchedule.DataBind();

            //BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            //Func<string, string> GetCrit = anyString =>
            //{
            //    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            //};

            //string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            //string strExamPlace = GetCrit(ddlPlace.SelectedValue);
            //string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue);
            //string strYearMonth = GetCrit(ConvertToYearMonth(e.NewDate.Month));
            //string strTime = GetCrit(ddlTime.SelectedValue);
            //var ls = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null);

            //DataSet ds = ls.DataResponse;

            //if (ds.Tables.Count > 0)
            //{
            //    DataTable dt = ds.Tables[0];

            //    var list = dt.AsEnumerable().Select(s => s.Field<DateTime>("TESTING_DATE")).Distinct().ToList();

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (e.NewDate.Date == list[i].Date)
            //        {
            //            Controls.Add(new LiteralControl("</br>"));
            //            Label lbl = new Label { ID = "btn" + i.ToString(), Text = "รายละเอียด" };
            //            Controls.Add(lbl);

            //        }

            //    }

            //}
            #endregion old Code
        }

        // ทุกๆการเลือกวันบนปฏิทิน
        private bool isLastRow = false;
        protected void cldExam_DayRender(object sender, DayRenderEventArgs e)
        {
            #region Day Render Rows
            /********** render day ************/
            daysCounter++;
            if (e.Day.Date.Day == 1 && !e.Day.IsOtherMonth) // 1st of current month. Turn visibility of row to ON.
            {
                isLastRow = false;
            }
            else if (daysCounter == 36 && e.Day.IsOtherMonth) // 5 rows already rendered. If its the next row is next month, hide it.
            {
                isLastRow = true;
            }
            else if (daysCounter == 1 && e.Day.IsOtherMonth && e.Day.Date.Month == e.Day.Date.AddDays(6).Month)
            {   // If first row completely is previous month, hide it.
                // Actually the flag should be isFirstRow, but I dont want one more boolean just for the sake of it.
                isLastRow = true;
            }

            if (isLastRow)
            {
                e.Cell.Visible = false;
                return;
            }
            if (e.Day.IsWeekend && !e.Day.IsSelected)
            {
                e.Cell.Style.Add("background-color", "#f0e7f1");
            }
            /********* end render ************/
            #endregion
            e.Day.IsSelectable = false;
            e.Cell.Enabled = false;
            e.Cell.ForeColor = System.Drawing.Color.Gray;
            if (IsCanRender == true)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };

                // Check First Load and Save Session

                string type = Request.QueryString["Type"];
                if (Session["lsDetailCalendar"] != null)
                {
                    DataSet ds = (DataSet)Session["lsDetailCalendar"];
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        var list = dt.AsEnumerable().Where(s => s.Field<DateTime>("TESTING_DATE").Date == e.Day.Date).ToList();
                        DateTime dtToday = DateTime.Today.AddDays(-1);
                        DateTime dtFinish = DateTime.Today.AddDays(+5);

                        ExamRender examrender = new ExamRender();
                        if (list != null && list.Count > 0)
                        {
                            LinkButton lnk = new LinkButton();

                            e.Cell.Controls.Add(new LiteralControl("</br>"));

                            lnk.ID = "lnkButton";
                            lnk.Text = "รายละเอียด";
                            lnk.ForeColor = System.Drawing.Color.Green;
                            examrender.ID = "lnkButton";
                            examrender.Name = "รายละเอียด";
                            examrender.testingDate = e.Day.Date;
                            lsRender.Add(examrender);
                            e.Cell.Attributes.Add("onclick", e.SelectUrl);
                            e.Cell.Style.Add("cursor", "pointer");

                            if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue() ||
                                base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() ||
                                base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                            {
                                DateTime dtTest = list[0].Field<DateTime>("TESTING_DATE").Date;

                                if ((type == null) || (type != "ManageExam"))
                                {
                                    if (e.Day.Date == dtTest && dtTest > dtToday && dtTest <= dtFinish)
                                    {
                                        lnk.Enabled = false;
                                        lnk.ForeColor = System.Drawing.Color.Gray;
                                        e.Cell.Attributes.Remove("onclick");
                                        e.Cell.Style.Remove("cursor");
                                        e.Cell.Style.Remove("pointer");
                                    }
                                }
                            }
                            e.Cell.Controls.Add(lnk);
                        }
                    }
                }
                if (Session["lsHoliday"] != null)
                {
                    DTO.ResponseService<DTO.GBHoliday[]> lsHoliday = (DTO.ResponseService<DTO.GBHoliday[]>)Session["lsHoliday"];
                    if (lsHoliday.DataResponse.Count() > 0)
                    {
                        //DataTable dt = dsHoliday.Tables[0];
                        var list = lsHoliday.DataResponse.Where(s => s.HL_DATE.Date == e.Day.Date).ToList();
                        DateTime dtToday = DateTime.Today.AddDays(-1);
                        DateTime dtFinish = DateTime.Today.AddDays(+5);

                        ExamRender examrender = new ExamRender();
                        if (list != null && list.Count > 0)
                        {
                            e.Cell.Controls.Add(new LiteralControl("</br>"));
                            Label lnk = new Label();
                            lnk.ID = "lnkButton";
                            lnk.Enabled = false;
                            lnk.Text = list[0].HL_DESC;
                            lnk.ForeColor = System.Drawing.Color.Red;
                            examrender.ID = "lnkButton";
                            examrender.Name = list[0].HL_DESC;
                            examrender.testingDate = e.Day.Date;
                            lsRender.Add(examrender);
                            e.Cell.Attributes.Add("onclick", e.SelectUrl);
                            e.Cell.Style.Add("cursor", "pointer");
                            e.Cell.Controls.Add(lnk);
                        }
                    }
                }
            }
        }

        private void SelectDataFrombase(DateTime visibleMonth)
        {
            try
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                Func<string, string> GetCrit = anyString =>
                {
                    return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                };


                string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
                string strExamPlace = GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue);
                string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue == "" ? "" : ddlTypeLicense.SelectedValue);
                string strYearMonth = string.Empty;

                #region New

                strYearMonth = GetCrit(ConvertToYearMonth(visibleMonth.Month));
                string strCurrentYear = strYearMonth.Substring(0, 4);
                if (Convert.ToInt32(strCurrentYear) != visibleMonth.Year)
                {
                    string strYear = visibleMonth.Year.ToString();
                    string strMonth = visibleMonth.Month.ToString();
                    if (visibleMonth.Month < 10)
                    {
                        strMonth = "0" + strMonth;
                    }
                    strYearMonth = strYear + strMonth;
                }

                #endregion
                string Owner = "";
                string type = Request.QueryString["Type"];
                if ((type != null) && (type == "ManageExam"))
                {
                    if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                    {
                        strExamPlaceGroup = base.UserProfile.CompCode.ToString();
                        Owner = "";
                    }
                    else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        strExamPlaceGroup = "";
                        Owner = base.UserProfile.CompCode.ToString();
                    }

                }

                string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);

                var ls = biz.GetExamMonthByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, null, Owner);
                GBBiz gbBiz = new GBBiz();
                DTO.ResponseService<DTO.GBHoliday[]> lsHoliday = gbBiz.GetHolidayListByYearMonth(strYearMonth);

                DataSet ds = ls.DataResponse;


                Session["lsDetailCalendar"] = null;
                Session["lsHoliday"] = null;

                Session["strExamPlaceGroup"] = null;
                Session["strExamPlace"] = null;
                Session["strLicenseType"] = null;
                Session["strYearMonth"] = null;
                Session["strTime"] = null;

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Session["lsDetailCalendar"] = ds;

                        Session["strExamPlaceGroup"] = strExamPlaceGroup;
                        Session["strExamPlace"] = strExamPlace;
                        Session["strLicenseType"] = strLicenseType;
                        Session["strYearMonth"] = strYearMonth;
                        Session["strTime"] = strTime;
                    }
                }
                if (lsHoliday.DataResponse.ToList().Count > 0)
                {
                    Session["lsHoliday"] = lsHoliday;
                }
            }
            catch
            {
            }

        }

        private void SaveCriteriaToSession()
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strExamPlaceGroup = GetCrit(ddlPlaceGroup.SelectedValue);
            string strExamPlace = GetCrit(ddlPlace.SelectedIndex == 0 ? "" : ddlPlace.SelectedValue);
            string strLicenseType = GetCrit(ddlTypeLicense.SelectedValue == "" ? "" : ddlTypeLicense.SelectedValue);
            string strYearMonth = string.Empty;
            strYearMonth = GetCrit(ConvertToYearMonth(ddlMonth.SelectedValue.ToInt()));
            string strTime = GetCrit(ddlTime.SelectedIndex == 0 ? "" : ddlTime.SelectedValue);

            Session["strExamPlaceGroup"] = strExamPlaceGroup;
            Session["strExamPlace"] = strExamPlace;
            Session["strLicenseType"] = strLicenseType;
            Session["strYearMonth"] = strYearMonth;
            Session["strTime"] = strTime;

        }

        // เลือกวันบนปฏิทิน
        protected void cldExam_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime cldselectDate = cldExam.SelectedDate;
                string strMonth = ConvertToYearMonth(cldExam.SelectedDate.Year, cldExam.SelectedDate.Month);
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                DataSet ds = (DataSet)Session["lsDetailCalendar"];
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                }

                // Get Session from Criteria Before
                string strExamPlaceGroup = Session["strExamPlaceGroup"].ToString();
                string strExamPlace = Session["strExamPlace"].ToString();
                string strLicenseType = Session["strLicenseType"].ToString();
                string strYearMonth = Session["strYearMonth"].ToString();
                string strTime = Session["strTime"].ToString();

                string Owner = "";
                string type = Request.QueryString["Type"];
                if ((type != null) && (type == "ManageExam"))
                {
                    if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                    {
                        strExamPlaceGroup = base.UserProfile.CompCode.ToString();
                        Owner = "";
                    }
                    else if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        strExamPlaceGroup = "";
                        Owner = base.UserProfile.CompCode.ToString();
                    }

                }

                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.Trim() == "0"))
                {
                    rowPerpage.Text = PAGE_SIZE_Key.ToString();
                    PageSize = PAGE_SIZE_Key;
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                var res = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, cldselectDate, resultPage, PageSize, false, Owner);
                if (res.DataResponse != null)
                {
                    if (res.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        Div1.Visible = true;
                        pnlTable.Visible = true;
                        gvExamSchedule.Visible = true;
                        btnExportExcel.Visible = true;
                        gvExamSchedule.DataSource = res.DataResponse;
                        gvExamSchedule.DataBind();
                    }
                    else
                    {
                        btnExportExcel.Visible = false;
                        pnlTable.Visible = false;
                    }
                }

                ModalExamSchedule.Hide();
            }
            catch
            {
            }
        }

        // ค้นหาประเภทของการแสดงผลตารางสมัครสอบ
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ModalExamSchedule.Hide();
                if (txtYear.Text.Trim().Length < 4)
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorSingleApplicant_002;
                    this.MasterSite.ModelError.ShowModalError();
                    udpLicense.Update();
                    return;
                }
                else
                {
                    if (ddlMonth.SelectedIndex == 0)
                        ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);

                    lsRender.Clear();
                    IsCanRender = true;
                    cldExam.SelectedDates.Clear();
                    txtNumberGvSearch.Text = "0"; //milk
                    ShowOrHide(true);//milk
                    gvExamSchedule.Visible = true;
                    gvExamSchedule.DataBind();
                    txtExamNumber.Visible = true;
                    SaveCriteriaToSession();
                    if (rblDisplay.SelectedValue == "1")
                    {

                        pnlCalendar.Visible = true;
                        table_Paging.Visible = false; //เป็นปฏิทินเลยซ่อนเพจจิ้ง
                        if ((base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()))
                        {
                            //if (btnInsertExamSchedule.Visible == true)
                            //btn_addtable.Visible = false;
                            // else
                            //btn_addtable.Visible = true;
                        }
                        else
                        {
                            // btn_addtable.Visible = false;

                        }
                        if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                        {
                            int iYear = txtYear.Text.ToInt() - 543;
                            DateTime dtSearch = new DateTime(iYear, ddlMonth.SelectedValue.ToInt(), 1);
                            string strYear = Convert.ToString(dtSearch.Year);
                            string strMonth = dtSearch.Month.ToString();
                            if (dtSearch.Month < 10)
                            {
                                strMonth = "0" + strMonth;
                            }
                            cldExam.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), 1);
                            cldExam.VisibleDate = cldExam.TodaysDate;

                            #region set background today
                            if (cldExam.TodaysDate.ToString("MMyyyy") == DateTime.Now.ToString("MMyyyy"))
                            {
                                cldExam.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), DateTime.Now.Day);
                                cldExam.VisibleDate = cldExam.TodaysDate;
                                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
                            }
                            else
                            {
                                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                            }
                            #endregion

                            SelectDataFrombase(cldExam.VisibleDate);
                            //milk add but code by n'na
                            if (gvExamSchedule.Rows.Count > 0)
                            {
                                btnExportExcel.Visible = true;
                            }
                            else
                            {
                                btnExportExcel.Visible = false;
                            }
                            //end 
                            upn.Update();
                        }
                    }
                    else
                    {
                        pnlCalendar.Visible = false;
                        pnlTable.Visible = true;
                        Div1.Visible = true;
                        BindExamScheduleByTable(true);
                        upn.Update();
                        table_Paging.Visible = true;
                        btnInsertExamSchedule2.Visible = false;
                    }
                }
            }
            catch
            {

            }
        }

        private void Search()
        {
            try
            {
                if (txtYear.Text.Trim().Length < 4)
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorSingleApplicant_002;
                    this.MasterSite.ModelError.ShowModalError();
                    udpLicense.Update();
                    //txtYear.Text = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                    return;
                }
                else
                {
                    if (ddlMonth.SelectedIndex == 0)
                        ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);

                    lsRender.Clear();
                    IsCanRender = true;
                    cldExam.SelectedDates.Clear();
                    txtNumberGvSearch.Text = "0"; //milk
                    ShowOrHide(true);//milk
                    gvExamSchedule.Visible = true;
                    gvExamSchedule.DataBind();
                    txtExamNumber.Visible = true;
                    SaveCriteriaToSession();
                    if (rblDisplay.SelectedValue == "1")
                    {

                        pnlCalendar.Visible = true;
                        table_Paging.Visible = false; //เป็นปฏิทินเลยซ่อนเพจจิ้ง
                        if (!string.IsNullOrEmpty(ddlMonth.SelectedValue))
                        {
                            int iYear = txtYear.Text.ToInt() - 543;
                            DateTime dtSearch = new DateTime(iYear, ddlMonth.SelectedValue.ToInt(), 1);
                            string strYear = Convert.ToString(dtSearch.Year);
                            string strMonth = dtSearch.Month.ToString();
                            if (dtSearch.Month < 10)
                            {
                                strMonth = "0" + strMonth;
                            }
                            cldExam.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), 1);
                            cldExam.VisibleDate = cldExam.TodaysDate;

                            #region set background today
                            if (cldExam.TodaysDate.ToString("MMyyyy") == DateTime.Now.ToString("MMyyyy"))
                            {
                                cldExam.TodaysDate = new DateTime(strYear.ToInt(), strMonth.ToInt(), DateTime.Now.Day);
                                cldExam.VisibleDate = cldExam.TodaysDate;
                                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
                            }
                            else
                            {
                                cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#f6f6f6");
                            }
                            #endregion

                            SelectDataFrombase(cldExam.VisibleDate);
                            //milk add but code by n'na
                            if (gvExamSchedule.Rows.Count > 0)
                            {
                                btnExportExcel.Visible = true;
                            }
                            else
                            {
                                btnExportExcel.Visible = false;
                            }
                            //end 
                            upn.Update();
                        }
                    }
                    else
                    {
                        pnlCalendar.Visible = false;
                        pnlTable.Visible = true;
                        Div1.Visible = true;
                        BindExamScheduleByTable(true);
                        upn.Update();
                        table_Paging.Visible = true;
                    }
                }
            }
            catch
            {

            }

        }

        // เพิ่มใบสมัครสอบ
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ModSingleApplicant.Show();
            ClearValue();
            IsCanRender = true;
        }

        protected void btnDetailSubmit_Click(object sender, EventArgs e)
        {
            SaveApplicant();
            ModSingleApplicant.Hide();
            ClearAllData();
            if (gvListExam.Rows.Count > 0)
            {
                ModalListExam.Show();
            }
            else
            {
                ModalExamSchedule.Hide();
            }
        }

        protected void gvExamSchedule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExamSchedule.PageIndex = e.NewPageIndex;
            gvExamSchedule.DataBind();
        }

        public class VariableTemp
        {
            public static IAS.DTO.ExamRender ValueTemp = new IAS.DTO.ExamRender();
        }

        #region milk
        protected void btnCancleSearch_Click(object sender, EventArgs e)
        {
            Session["TestingNo"] = string.Empty;
            ShowOrHide(false);//milk
            txtNumberGvSearch.Text = "0";//milk
            DefaultData();

            rblDisplay.SelectedValue = "1";
            txtExamNumber.Text = string.Empty;
            ddlTime.SelectedIndex = 0;
            ddlPlace.Items.Clear();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ModalExamSchedule.Hide();
            ClearAllData();
            ShowOrHide(false);//milk
            txtNumberGvSearch.Text = "0";//milk
            DefaultData();
            SetFirstLoad();
            btnSearchExamCode_Click(sender, e);
            CheckDisplayExam();

        }

        private void SetFirstLoad()
        {
            cldExam.VisibleDate = DateTime.Now;
            cldExam.SelectedDate = DateTime.Now;
            txtYear.Text = CheckToYear(cldExam.VisibleDate.Year).ToString();
            ddlPlace.Items.Clear();
            SelectDataFrombase(cldExam.VisibleDate);
            upn.Update();
            cldExam.TodayDayStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#fbfba6");
            rblDisplay.SelectedValue = "1";
            UplExamSchedule.Update();
        }

        private void CheckDisplayExam()
        {
            if (rblDisplay.SelectedValue == "1")
            {
                pnlCalendar.Visible = true;
                Div1.Visible = false;
                table_Paging.Visible = false; //ซ่อน Paging เพราะเป็นปฏิทิน
                txtNumberGvSearch.Text = "0";
                if (txtYear.Text.Trim().Length < 4)
                    txtYear.Text = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                if (ddlMonth.SelectedIndex == 0)
                    ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);
                SelectDataFrombase(Convert.ToDateTime(txtYear.Text + @"/" + ddlMonth.SelectedIndex.ToString("00") + @"/01"));
                upn.Update();
            }
            else
            {
                pnlTable.Visible = true;
                pnlCalendar.Visible = false;
                if (txtYear.Text.Trim().Length < 4)
                    txtYear.Text = Convert.ToString(Convert.ToInt16(DateTime.Now.Year) + 543);
                if (ddlMonth.SelectedIndex == 0)
                    ddlMonth.SelectedIndex = Convert.ToInt16(DateTime.Now.Month);
                BindExamScheduleByTableDefault(true);
                upn.Update();
            }
        }

        #endregion milk
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModSingleApplicant.Hide();
            ClearAllData();
            btnSearch_Click(sender, e);
        }
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("ลำดับที่", "RUN_NO");
                columns.Add("รหัสสอบ", "TESTING_NO");
                columns.Add("วันที่สอบ", "TESTING_DATE");
                columns.Add("เวลาสอบ", "TEST_TIME");
                columns.Add("หน่วยงานจัดสอบ", "EXAM_PLACE_GROUP_NAME");
                columns.Add("สนามสอบ", "EXAM_PLACE_NAME");
                columns.Add("จังหวัด", "PROVINCE");
                columns.Add("สมัคร/ที่นั่ง", "SEAT_AMOUNT");
                columns.Add("ใบอนุญาต", "LICENSE_TYPE_NAME");
                columns.Add("ค่าธรรมเนียม", "EXAM_FEE");


                ExportBiz export = new ExportBiz();
                ExamScheduleBiz biz = new ExamScheduleBiz();
                if (rblDisplay.SelectedValue == "2")
                {
                    int total = lblTotal.Text == "" ? 0 : Regex.Replace(lblTotal.Text, "[^0-9]", "").ToInt();
                    if (total > base.EXCEL_SIZE_Key)
                    {
                        this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                        this.MasterSite.ModelError.ShowModalError();
                        upn.Update();
                    }
                    else
                    {
                        string strExamPlaceGroup = Session["strExamPlaceGroup"].ToString();
                        string strExamPlace = Session["strExamPlace"].ToString();
                        string strLicenseType = Session["strLicenseType"].ToString();
                        string strYearMonth = Session["strYearMonth"].ToString();
                        string strTime = Session["strTime"].ToString();
                        var ls = biz.GetExamByCriteriaDefault(strExamPlaceGroup, strExamPlace, strLicenseType, base.UserProfile.AgentType == null ? "" : base.UserProfile.AgentType, strYearMonth, strTime, null, 1, base.EXCEL_SIZE_Key, false);
                        DataSet ds = ls.DataResponse;
                        DataTable dt = ds.Tables[0];
                        if (rblDisplay.SelectedValue == "1")
                        {
                            export.CreateExcel(dt, columns, base.UserProfile);
                        }
                        else
                        {
                            List<HeaderExcel> header = new List<HeaderExcel>();
                            header.Add(new HeaderExcel
                            {
                                NameColumnsOne = "ประเภทใบอนุญาต  ",
                                ValueColumnsOne = ddlTypeLicense.SelectedItem.Text,
                                NameColumnsTwo = "หน่วยงานจัดสอบ  ",
                                ValueColumnsTwo = ddlPlaceGroup.SelectedItem.Text
                            });

                            HeaderExcel he = new HeaderExcel();
                            he.NameColumnsOne = "สถานที่สอบ   ";
                            try
                            {
                                he.ValueColumnsOne = ddlPlace.SelectedItem.Text;
                            }
                            catch { }
                            he.NameColumnsTwo = "เดือน   ";
                            he.ValueColumnsTwo = ddlMonth.SelectedItem.Text;
                            header.Add(he);

                            header.Add(new HeaderExcel
                            {
                                NameColumnsOne = "ปี ",
                                ValueColumnsOne = txtYear.Text,
                                NameColumnsTwo = "เวลาสอบ   ",
                                ValueColumnsTwo = ddlTime.SelectedItem.Text
                            });
                            export.CreateExcel(dt, columns, header, base.UserProfile);
                        }
                    }
                }
                else
                {
                    DateTime cldselectDate = cldExam.SelectedDate;
                    // Get Session from Criteria Before
                    string strExamPlaceGroup = Session["strExamPlaceGroup"].ToString();
                    string strExamPlace = Session["strExamPlace"].ToString();
                    string strLicenseType = Session["strLicenseType"].ToString();
                    string strYearMonth = Session["strYearMonth"].ToString();
                    string strTime = Session["strTime"].ToString();
                    var res = biz.GetExamByCriteria(strExamPlaceGroup, strExamPlace, strLicenseType, strYearMonth, strTime, cldselectDate, 1, base.EXCEL_SIZE_Key, false);

                    if (rblDisplay.SelectedValue == "")
                    {
                        export.CreateExcel(res.DataResponse.Tables[0], columns, base.UserProfile);
                    }
                    else
                    {
                        List<HeaderExcel> header = new List<HeaderExcel>();
                        header.Add(new HeaderExcel
                        {
                            NameColumnsOne = "ประเภทใบอนุญาต  ",
                            ValueColumnsOne = ddlTypeLicense.SelectedItem.Text,
                            NameColumnsTwo = "หน่วยงานจัดสอบ  ",
                            ValueColumnsTwo = ddlPlaceGroup.SelectedItem.Text
                        });

                        HeaderExcel he = new HeaderExcel();
                        he.NameColumnsOne = "สถานที่สอบ   ";
                        try
                        {
                            he.ValueColumnsOne = ddlPlace.SelectedItem.Text;
                        }
                        catch { }
                        he.NameColumnsTwo = "เดือน   ";
                        he.ValueColumnsTwo = ddlMonth.SelectedItem.Text;
                        header.Add(he);

                        header.Add(new HeaderExcel
                        {
                            NameColumnsOne = "ปี ",
                            ValueColumnsOne = txtYear.Text,
                            NameColumnsTwo = "เวลาสอบ   ",
                            ValueColumnsTwo = ddlTime.SelectedItem.Text
                        });
                        export.CreateExcel(res.DataResponse.Tables[0], columns, header, base.UserProfile);
                    }
                }
            }
            catch { }
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void ddlAgentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetLicneseByAgentType();
        }

        private void SaveApplicantSession()
        {
            if (Session["lstApplicant"] == null)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();

                lstApplicant = new List<DTO.AddApplicant>();
                var arrComp = ddlCompanyCode.SelectedValue.Split('[', ']');

                string strInSurCompCode = string.Empty;
                if ((arrComp[0]) == "0")
                {
                    strInSurCompCode = "";
                }
                else
                {
                    strInSurCompCode = arrComp[1];
                }
                lstApplicant.Add(new DTO.AddApplicant
                {
                    ExamNumber = Session["ExamNumber"].ToString(),
                    ExamDate = (Convert.ToDateTime(Session["ExamDate"])),
                    ExamTime = Session["ExamTime"].ToString(),
                    ExamPlaceGroup = Session["ExamPlaceGroup"].ToString(),
                    ExamPlace = Session["ExamPlace"].ToString(),
                    Province = Session["Province"].ToString(),
                    Seat = Session["Seat"].ToString(),
                    LicenseTypeName = Session["LicenseTypeName"].ToString(),
                    ExamFee = Session["ExamFee"].ToString(),
                    AgentType = Session["AgentType"].ToString(),
                    ExamPlaceCode = Session["PlaceCode"].ToString(),
                    InSurCompCode = strInSurCompCode,
                    ApplyDate = DateTime.Now,
                });
                Session["lstApplicant"] = lstApplicant;
                ModalExamSchedule.Hide();
            }
            else
            {

                lstApplicant = (List<DTO.AddApplicant>)Session["lstApplicant"];
                var arrComp = ddlCompanyCode.SelectedValue.Split('[', ']');
                if (lstApplicant.Where(r => r.ExamNumber == txtDetailExamCode.Text).Count() == 0)
                {
                    string strInSurCompCode = string.Empty;
                    if ((arrComp[0]) == "0")
                    {
                        strInSurCompCode = "";
                    }
                    else
                    {
                        strInSurCompCode = arrComp[1];
                    }
                    lstApplicant.Add(new DTO.AddApplicant
                    {
                        ExamNumber = Session["ExamNumber"].ToString(),
                        ExamDate = (Convert.ToDateTime(Session["ExamDate"])),
                        ExamTime = Session["ExamTime"].ToString(),
                        ExamPlaceGroup = Session["ExamPlaceGroup"].ToString(),
                        ExamPlace = Session["ExamPlace"].ToString(),
                        Province = Session["Province"].ToString(),
                        Seat = Session["Seat"].ToString(),
                        LicenseTypeName = Session["LicenseTypeName"].ToString(),
                        ExamFee = Session["ExamFee"].ToString(),
                        AgentType = Session["AgentType"].ToString(),
                        ExamPlaceCode = Session["PlaceCode"].ToString(),
                        InSurCompCode = strInSurCompCode,
                        ApplyDate = DateTime.Now,
                    });
                    Session["lstApplicant"] = lstApplicant;
                    ModalExamSchedule.Hide();
                }
                else
                {
                    for (int i = 0; i < lstApplicant.Count; i++)
                    {
                        if (lstApplicant[i].ExamNumber == txtDetailExamCode.Text)
                        {
                            lstApplicant[i].ExamDate = Convert.ToDateTime(txtDetailDateExam.Text);
                            Session["lstApplicant"] = lstApplicant;
                            break;
                        }
                    }
                    ModalExamSchedule.Hide();
                }

            }

        }

        private void SaveApplicant()
        {
            ModalExamSchedule.Hide();
            ApplicantBiz abiz = new ApplicantBiz();
            if (!string.IsNullOrEmpty(txtDetailExamCode.Text))
            {
                string strPlaceCode = Session["ExamPlaceCode"].ToString();
                string strExamNumber = Session["TestingNo"].ToString();
                string strLicenseTypeCode = Session["LicenseTypeCode"].ToString();
                string strTestTimeCode = Session["TestTimeCode"].ToString();
                DateTime dtTestingDate = Convert.ToDateTime(Session["TestingDate"].ToString());
                string strExamTime = Session["ExamTime"].ToString();
                SaveApplicantSession();
                lstApplicant = (List<DTO.AddApplicant>)Session["lstApplicant"];

                if (strLicenseTypeCode != "03" && strLicenseTypeCode != "04")
                {
                    if (ddlCompanyCode.SelectedIndex == 0)
                    {
                        this.MasterSite.ModelError.ShowMessageError = "กรุณาเลือกต้นสังกัด";
                        this.MasterSite.ModelError.ShowModalError();
                        return;
                    }
                }

                DTO.ResultValidateApplicant res = abiz.ValidateApplicantBeforeSaveList(txtDetailExamCode.Text, base.UserProfile.IdCard, dtTestingDate, strTestTimeCode, strPlaceCode, strExamTime, lstApplicant);

                if (res.IsConfirm)
                {
                    ModalPopupExtenderListExam.Show();
                    lblConfirmExam.Text = res.ValidateMessage;
                    upn.Update();
                }
                if (!res.IsCanExam)
                {
                    //ลบข้อมูลที่ไม่ให้สอบ
                    List<DTO.AddApplicant> lstApplicants = (List<DTO.AddApplicant>)Session["lstApplicant"];
                    if (lstApplicants.Count() > 0)
                    {
                        foreach (var item in lstApplicant.ToList())
                        {
                            if (item.ExamNumber == strExamNumber)
                            {
                                lstApplicant.Remove(item);
                            }
                        }
                        Session["lstApplicant"] = lstApplicant;
                    }

                    this.MasterSite.ModelError.ShowMessageError = res.ValidateMessage;
                    this.MasterSite.ModelError.ShowModalError();
                }
                if (res.IsCanExam && !res.IsConfirm)
                {
                    SaveApplicantSession();
                    GetExamList();
                }
            }
            //ModalExamSchedule.Hide();
        }

        private void GetExamList()
        {
            if (Session["lstApplicant"] != null)
            {
                gvListExam.DataSource = Session["lstApplicant"];
                gvListExam.DataBind();

            }
            ModalExamSchedule.Hide();
        }

        protected void gvListExam_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblListExamNumberNo = (Label)e.Row.FindControl("lblListExamNumberNo");

                var LBUP = (LinkButton)e.Row.FindControl("LBUP");
                var LBDown = (LinkButton)e.Row.FindControl("LBDown");
                var list = lstApplicant.FirstOrDefault(x => x.ExamNumber == lblListExamNumberNo.Text);
                if (list != null)
                {
                    if (lstApplicant.Count == 1)
                    {
                        LBDown.Visible = false;
                        LBUP.Visible = false;
                    }
                    else if (e.Row.RowIndex == 0)
                    {
                        LBUP.Visible = false;
                    }
                    else if (e.Row.RowIndex == lstApplicant.Count - 1)
                    {
                        LBDown.Visible = false;
                    }
                    /*
                    if (lstApplicant.IndexOf(list) == 0)
                    {
                        LBUP.Visible = false;
                    }
                    else if (lstApplicant.IndexOf(list) == lstApplicant.Count - 1)
                    {
                        LBDown.Visible = false;
                    }*/

                }

            }
            //upnListExam.Update();
        }

        protected void btnListExamConfirm_Click(object sender, EventArgs e)
        {
            //SaveListExam();
            List<DTO.AddApplicant> lstApplicant = (List<DTO.AddApplicant>)Session["lstApplicant"];
            if (lstApplicant.ToList().Count > 0)
            {

                ApplicantBiz biz = new ApplicantBiz();
                string strAmount = biz.GetQuantityBillPerPageByConfig();
                if (lstApplicant.Count != 0 && lstApplicant.Count <= Convert.ToInt32(strAmount))
                {
                    List<DTO.ApplicantTemp> lst = new List<ApplicantTemp>();
                    for (int i = 0; i < lstApplicant.Count; i++)
                    {
                        DTO.ApplicantTemp app = new ApplicantTemp();
                        app.APPLICANT_CODE = lstApplicant[i].ToInt();
                        app.TESTING_NO = lstApplicant[i].ExamNumber;
                        app.TESTING_DATE = lstApplicant[i].ExamDate;
                        app.EXAM_PLACE_CODE = lstApplicant[i].ExamPlaceCode;
                        app.APPLY_DATE = DateTime.Today;
                        app.INSUR_COMP_CODE = lstApplicant[i].InSurCompCode;
                        app.USER_ID = base.UserId;
                        app.ID_CARD_NO = base.IdCard;
                        app.APPLY_DATE = lstApplicant[i].ApplyDate;
                        app.RUN_NO = Convert.ToString(i + 1);
                        lst.Add(app);
                    }
                    SaveSubmit(lst);
                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = "สมัครสอบเกินจำนวนรายการต่อใบสั่งจ่ายที่กำหนดไว้";
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = "ไม่พบรายการสอบ";
                this.MasterSite.ModelError.ShowModalError();
            }


        }

        protected void btnListExamContinue_Click(object sender, EventArgs e)
        {
            ModalExamSchedule.Hide();
            ModalListExam.Hide();

        }

        protected void lnkListExamRegister_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var lblExamNumber = (Label)gr.FindControl("lblListExamNumberNo");

            List<DTO.AddApplicant> lstApplicant = (List<DTO.AddApplicant>)Session["lstApplicant"];
            foreach (var item in lstApplicant.ToList())
            {
                if (item.ExamNumber == lblExamNumber.Text)
                {
                    lstApplicant.Remove(item);
                }
            }
            Session["lstApplicant"] = lstApplicant;
            gvListExam.DataSource = Session["lstApplicant"];
            gvListExam.DataBind();
        }

        private void SaveListExam()
        {
            string group = string.Empty;
            List<DTO.AddApplicant> lstApplicant = (List<DTO.AddApplicant>)Session["lstApplicant"];
            ApplicantBiz biz = new ApplicantBiz();
            string strAmount = biz.GetQuantityBillPerPageByConfig();
            if (lstApplicant.Count != 0 && lstApplicant.Count <= Convert.ToInt32(strAmount))
            {
                List<DTO.ApplicantTemp> lst = new List<ApplicantTemp>();
                for (int i = 0; i < lstApplicant.Count; i++)
                {
                    DTO.ApplicantTemp app = new ApplicantTemp();
                    app.APPLICANT_CODE = lstApplicant[i].ToInt();
                    app.TESTING_NO = lstApplicant[i].ExamNumber;
                    app.TESTING_DATE = lstApplicant[i].ExamDate;
                    app.EXAM_PLACE_CODE = lstApplicant[i].ExamPlaceCode;
                    app.APPLY_DATE = DateTime.Today;
                    app.INSUR_COMP_CODE = lstApplicant[i].InSurCompCode;
                    app.USER_ID = base.UserId;
                    app.ID_CARD_NO = base.IdCard;
                    app.APPLY_DATE = lstApplicant[i].ApplyDate;
                    app.RUN_NO = Convert.ToString(i + 1);
                    lst.Add(app);
                }
                string passGroup = string.Empty;

                //Check 
                var checkBeforeSubmit = biz.ValidateApplicantSingleBeforeSubmit(lst);
                if (checkBeforeSubmit.ResultMessage == true)
                {
                    ModalPopupExtenderListExam.Show();
                }
                //
                else
                {
                    var res = biz.InsertSingleApplicant(lst, base.UserId);
                    if (res.IsError)
                    {
                        this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                    else
                    {
                        this.ModalListExam.Hide();
                        ModalExamSchedule.Hide();
                        Session["lstApplicant"] = null;
                        this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.SuccessInsertApplicant;
                        this.MasterSite.ModelSuccess.ShowModalSuccess();
                        group = res.DataResponse;
                        passGroup = group + " " + base.UserId;

                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupSingle('" + passGroup + "')", true);

                    }
                }

            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = "กรุณาตรวจสอบจำนวนรายการที่สอบ";
                this.MasterSite.ModelError.ShowModalError();

                ModalListExam.Show();
                //UplListExam.Update();
            }
            //upn.Update();
        }

        #region 2.1.5
        private void getOldData()
        {
            try
            {
                BLL.ExamRoomBiz ex = new ExamRoomBiz();
                var OldData = ex.GetExamByTestingNo(txtScheduleExamCode.Text);
                try
                {
                    string ExamGroup = "";
                    var ds = ex.GetPlaceDetailByPlaceCode_noCheckActive(OldData.DataResponse.EXAM_PLACE_CODE.ToString());
                    DataSet Detail = ds.DataResponse;

                    if (Detail.Tables.Count > 0)
                    {

                        GetProvince();

                        ddlProvince.SelectedValue = Detail.Tables[0].Rows[0]["PROVINCE_CODE"].ToString();
                        try
                        {
                            if (Detail.Tables[0].Rows[0]["EXAM_PLACE_GROUP_CODE"].ToString() != "")
                            {
                                GetExamGRoup(Detail.Tables[0].Rows[0]["EXAM_PLACE_GROUP_CODE"].ToString());
                                ddlExamGroupCode.SelectedValue = Detail.Tables[0].Rows[0]["EXAM_PLACE_GROUP_CODE"].ToString();
                                ExamGroup = "G";
                            }
                            else
                            {
                                GetAssociation(Detail.Tables[0].Rows[0]["ASSOCIATION_CODE"].ToString());
                                ddlExamGroupCode.SelectedValue = Detail.Tables[0].Rows[0]["ASSOCIATION_CODE"].ToString();
                                ExamGroup = "A";
                            }
                        }
                        catch
                        {
                            ddlExamGroupCode.SelectedIndex = 0;
                        }
                        GetExamPlaceByGroupPlace(ExamGroup);
                        try
                        {
                            ddlPlace_forRoom.SelectedValue = OldData.DataResponse.EXAM_PLACE_CODE;
                        }
                        catch
                        {
                            ddlPlace_forRoom.SelectedIndex = 0;
                        }
                        GetddlGroupType();

                        txtScheduleDetailDateExam.Text = OldData.DataResponse.TESTING_DATE.ToString("dd/MM/yyyy");
                        // txtScheduleDetailDateExam.Text = DateUtil.String_dd_MM_yyyy_ToDate(txtScheduleDetailDateExam.Text,'/',false).ToString();

                        GetTime(OldData.DataResponse.TEST_TIME_CODE.ToString());
                        try
                        {
                            ddlDetailTimeExamCode.SelectedValue = OldData.DataResponse.TEST_TIME_CODE;

                        }
                        catch
                        {
                            ddlDetailTimeExamCode.SelectedIndex = 0;
                        }


                        getOnwer();
                        try
                        {
                            ddlExamOnwer.SelectedValue = OldData.DataResponse.EXAM_OWNER;
                        }
                        catch
                        {
                            ddlExamOnwer.SelectedIndex = 0;
                        }

                        GetLicenseType_forRoom(true);
                        try
                        {
                            ddlLicenseType.SelectedValue = OldData.DataResponse.LICENSE_TYPE_CODE;
                        }
                        catch
                        {
                            ddlLicenseType.SelectedIndex = 0;
                        }


                        ddlSpecial.SelectedValue = (OldData.DataResponse.SPECIAL == "Y") ? "Y" : "";

                        txtExamUseSeatRoom.Text = OldData.DataResponse.EXAM_ADMISSION.ToString();

                        var OldRoom = ex.GetExamRoomByLicenseNo(txtScheduleExamCode.Text, OldData.DataResponse.EXAM_PLACE_CODE.ToString());
                        lsExamSubLicense = OldRoom.DataResponse.ToList();
                        BindGvExamRoom();

                        if (!string.IsNullOrEmpty(OldData.DataResponse.REMARK))
                        {
                            string[] remark = OldData.DataResponse.REMARK.ToString().Split('|');
                            if (remark.Length == 2)
                            {
                                txtRemark.Text = remark[1];
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            catch
            {
            }
        }

        private void EditMode(bool Mode)
        {
            ddlDetailTimeExamCode.Enabled = !Mode;
            ddlExamGroupCode.Enabled = !Mode;
            ddlLicenseType.Enabled = !Mode;
            ddlProvince.Enabled = !Mode;
            ddlPlace_forRoom.Enabled = !Mode;
            ddlSpecial.Enabled = !Mode;
            txtScheduleDetailDateExam.Enabled = !Mode;
            ddlExamOnwer.Enabled = !Mode;
        }

        protected void DefaultData_forRoom()
        {
            try
            {
                ClearAllData();
                GetTime("");
                SetDate();
                ddlLicenseType.Items.Clear();
                //GetAssociation();
                GetProvince();
                BindGvExamRoom();
                setShowRoom(true);
                getOnwer();

            }
            catch
            {

            }
        }

        protected void ClearAllData()
        {
            try
            {
                chkSpecial.Checked = false;
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {

                    txtExamUseSeatRoom.Text = "";
                    if (lsExamSubLicense.Count() > 0)
                        lsExamSubLicense.Clear();
                    gvExamRoom.DataSource = lsExamSubLicense;
                    gvExamRoom.DataBind();
                    ddlSpecial.SelectedIndex = 0;
                    ddlPlace_forRoom.Items.Clear();
                    ddlLicenseType.Items.Clear();
                    ddlExamRoom.Items.Clear();
                    if (base.UserProfile.MemberType != DTO.RegistrationType.Association.GetEnumValue())
                    {
                        ddlExamOnwer.SelectedIndex = 0;
                        ddlExamOnwer.Enabled = true;
                    }
                    else
                    {
                        ddlExamOnwer.SelectedValue = base.UserProfile.CompCode;
                        ddlExamOnwer.Enabled = false;
                    }
                    if (ddlProvince.Items.Count > 0) ddlProvince.SelectedIndex = 0;
                    if (ddlExamGroupCode.Items.Count > 0) ddlExamGroupCode.SelectedIndex = 0;
                    txtScheduleExamCode.Text = "";
                    btnChangeRoom.Visible = false;
                    btnDelete.Visible = false;
                    SetDate();
                    if (ddlDetailTimeExamCode.Items.Count > 0) ddlDetailTimeExamCode.SelectedIndex = 0;
                    clearAll(true);
                    ModalExamSchedule.Hide();
                }
            }
            catch
            {
            }
        }

        protected void GetProvince()
        {
            try
            {
                BLL.DataCenterBiz dbiz = new DataCenterBiz();
                var ls = dbiz.GetProvince(SysMessage.DefaultSelecting);
                ddlProvince.DataTextField = "Name";
                ddlProvince.DataValueField = "Id";
                ddlProvince.DataSource = ls;
                ddlProvince.DataBind();
                ddlProvince.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        protected void GetAssociation(string AssC = "")
        {
            try
            {
                DataCenterBiz biz = new DataCenterBiz();
                if (((base.UserProfile.MemberType != DTO.RegistrationType.OICAgent.GetEnumValue()) &&
                    (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue()))
                    && (AssC == ""))
                    AssC = base.UserProfile.CompCode;



                var ls = biz.GetAssociation(AssC);
                ddlExamGroupCode.DataValueField = "ASSOCIATION_CODE";
                ddlExamGroupCode.DataTextField = "ASSOCIATION_NAME";

                ddlExamGroupCode.DataSource = ls.DataResponse;
                ddlExamGroupCode.DataBind();

                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    ddlExamGroupCode.SelectedValue = AssC;
                    ddlExamGroupCode.Enabled = false;
                }
                else
                {
                    ddlExamGroupCode.SelectedIndex = 0;
                    ddlExamGroupCode.Enabled = true;
                }
                ddlExamGroupCode.Items.Insert(0, SysMessage.DefaultSelecting);
            }
            catch
            {
            }
        }

        private void GetExamGRoup(string ComP = "")
        {
            try
            {

                DataCenterBiz biz = new DataCenterBiz();
                if (((base.UserProfile.MemberType != DTO.RegistrationType.OICAgent.GetEnumValue())
                    && (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue())) && (ComP == ""))
                    ComP = base.UserProfile.CompCode;

                var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, ComP);
                ddlExamGroupCode.DataValueField = "Id";
                ddlExamGroupCode.DataTextField = "Name";

                ddlExamGroupCode.DataSource = ls.DataResponse;
                ddlExamGroupCode.DataBind();

                if (base.UserProfile.MemberType != DTO.RegistrationType.TestCenter.GetEnumValue())
                {
                    ddlExamGroupCode.SelectedIndex = 0;
                    ddlExamGroupCode.Enabled = true;
                }
                else
                {
                    ddlExamGroupCode.SelectedValue = ComP;
                    ddlExamGroupCode.Enabled = false;
                }
                ddlExamGroupCode.Items.Insert(0, SysMessage.DefaultSelecting);
            }
            catch
            {
            }
        }

        protected void SetDate()
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                DateTime tempDAte = DateTime.Today;
                tempDAte = tempDAte.AddDays(6);
                txtScheduleDetailDateExam.Text = tempDAte.ToString("dd/MM/yyyy");
            }
        }

        protected void GetTime(string timeCode)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    BLL.ExamRoomBiz ebiz = new ExamRoomBiz();
                    var ddlTime = ebiz.getExamTimeShow(timeCode);


                    ddlDetailTimeExamCode.DataTextField = "TXTTIME";
                    ddlDetailTimeExamCode.DataValueField = "TIMECODE";
                    ddlDetailTimeExamCode.DataSource = ddlTime.DataResponse;
                    ddlDetailTimeExamCode.DataBind();
                    ddlDetailTimeExamCode.Items.Insert(0, SysMessage.DefaultSelecting);
                    ddlDetailTimeExamCode.SelectedIndex = 0;
                }
            }
            catch
            {
            }
        }

        public List<DTO.ExamSubLicense> lsExamSubLicense
        {
            get
            {
                if (Session["ExamSubLicense"] == null)
                {
                    Session["ExamSubLicense"] = new List<DTO.ExamSubLicense>();
                }
                return (List<DTO.ExamSubLicense>)Session["ExamSubLicense"];
            }
            set { Session["ExamSubLicense"] = value; }
        }

        private void BindGvExamRoom()
        {
            gvExamRoom.DataSource = lsExamSubLicense;
            gvExamRoom.DataBind();
            gvExamRoom.Visible = true;
        }

        private void initDDLExamRoom(string code)
        {
            BLL.ExamRoomBiz ebiz = new ExamRoomBiz();
            var res = ebiz.GetExamRoomByPlaceCode(code);
            BindToDDL(ddlExamRoom, res.DataResponse);
        }

        protected void ShowOrHide(Boolean S_H)//milk
        {
            if (S_H)
            {
                if (rblDisplay.SelectedValue == "1")
                {
                    pnlCalendar.Visible = S_H;
                    pnlTable.Visible = !S_H;
                }
                else
                {
                    pnlTable.Visible = S_H;
                    pnlCalendar.Visible = !S_H;
                }
            }
        }

        // ค้นหารอบสอบโดยระบุรหัสสอบ
        protected void btnSearchExamCode_Click(object sender, EventArgs e)
        {
            txtExamNumber.Text = txtExamNumber.Text.Trim();
            string type = Request.QueryString["Type"];

            if ((type != null) && type == "Import")
            {
                btnInsertExamSchedule.Visible = false;
                ShowOrHide(true);//milk
                //DefaultData();
                //txtNumberGvSearch.Text = "0";//milk
                txtExamNumber.Visible = true;
                if (!string.IsNullOrEmpty(txtExamNumber.Text))
                {
                    BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                    bool result = biz.IsRightTestingNo(txtExamNumber.Text);
                    if (result == false)
                    {
                        this.MasterSite.ModelError.ShowMessageError = SysMessage.PleaseInputTestingNo;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                    else
                    {
                        Session["TestingNo"] = txtExamNumber.Text;

                        var res = biz.GetExamByTestingNo(txtExamNumber.Text);
                        if (res.IsError)
                        {
                            this.MasterSite.ModelError.ShowMessageError = "โปรดตรวจสอบข้อมูล";
                            this.MasterSite.ModelError.ShowModalError();
                        }
                        else
                        {
                            Session["ExamNumber"] = res.DataResponse.TESTING_NO;
                            Session["ExamDate"] = res.DataResponse.TESTING_DATE;
                            Session["ExamTime"] = res.DataResponse.TEST_TIME_CODE;
                            Session["PlaceCode"] = res.DataResponse.EXAM_PLACE_CODE;
                            Session["ExamPlaceCode"] = res.DataResponse.EXAM_PLACE_CODE;
                            Session["TestingNo"] = res.DataResponse.TESTING_NO;
                            Session["TestingDate"] = res.DataResponse.TESTING_DATE;
                            Session["TestTimeCode"] = res.DataResponse.TEST_TIME_CODE;
                            Session["LicenseTypeCode"] = res.DataResponse.LICENSE_TYPE_CODE;
                            Session["ExamPlaceGroupCode"] = res.DataResponse.EXAM_PLACE_GROUP_CODE;
                        }

                        Response.Redirect("~/Applicant/GroupApplicantDetail.aspx");
                    }
                }
                else
                {
                    if (rblDisplay.SelectedValue == "1")
                    {
                        pnlSearch.Visible = true;
                        pnlCalendar.Visible = true;
                    }
                    else
                    {
                        pnlSearch.Visible = true;
                        pnlTable.Visible = true;
                    }

                }
            }
        }

        protected void ddlDetailYardGroupCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            UplExamSchedule.Update();
            ModalExamSchedule.Show();
        }

        // กำหนดค่าเริ่มต้นของจำนวนรับสมัคร โดยอ้างอิงจากสนามสอบ
        protected void ddlDetailExamYardCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
            ModalExamSchedule.Show();
        }

        // เพิ่มกำหนดการสอบ
        protected void btnInsertExamSchedule_Click(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.ADD.ToString();
                checkMode();
                ModalExamSchedule.Show();

                UplExamSchedule.Update();
                ClearAllData();
                setShowRoom(true);
                GetTypeforDDL(sender, e);
                GetLicenseType_forRoom(false);
            }
        }

        private void GetTypeforDDL(object sender, EventArgs e)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    {
                        ddlGroupType.SelectedValue = "A";
                        ddlGroupType.Enabled = false;
                        // GetAssociation();
                    }

                    else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                    {
                        ddlGroupType.SelectedValue = "G";
                        ddlGroupType.Enabled = false;
                        // GetExamGRoup();
                    }
                    else if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                    {
                        ddlGroupType.SelectedValue = "";
                        ddlGroupType.Enabled = true;
                    }
                    ddlGroupType_SelectedIndexChanged(sender, e);
                }
            }
            catch
            {
            }
        }

        private void SumExamSeatInPut()
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                ModalExamSchedule.Show();
                int sum = lsExamSubLicense.Sum(x => x.NUMBER_SEAT_ROOM).ToInt();
                txtExamUseSeatRoom.Text = sum.ToString();
            }
        }

        protected void btnAddExamRoom_Click(object sender, EventArgs e)
        {
            ModalExamSchedule.Show();
            if (InputRoomInGV("Add"))
            {
                btnChangeRoom.Visible = false;
                btnDelete.Visible = false;

                Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.EDIT.ToString();
                checkMode();
            }
        }

        protected bool InputRoomInGV(string AddInput)
        {
            Boolean InputOK = true;
            try
            {
                BLL.ExamRoomBiz ebiz = new ExamRoomBiz();
                ModalExamSchedule.Show();
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    if (ddlExamRoom.SelectedIndex > 0)
                    {

                        ModalExamSchedule.Show();
                        if (lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == ddlExamRoom.SelectedValue).Count() > 0)
                        {
                            if (gvExamRoom.Rows.Count > 0)
                            {

                                foreach (GridViewRow gr in gvExamRoom.Rows)
                                {
                                    string RoomCode = ((Label)gr.FindControl("lblExamRoomCode")).Text;
                                    string input = ((TextBox)gr.FindControl("txtExamNumberSeatRoom")).Text;
                                    string seat = ((Label)gr.FindControl("lblSeatAmount")).Text;
                                    var obj = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).First();
                                    //obj.NUMBER_SEAT_ROOM = input.ToInt() > seat.ToInt() ? obj.NUMBER_SEAT_ROOM : input.ToShort();
                                    if (input.ToInt() > seat.ToInt())
                                    {
                                        InputOK = false;
                                        //this.MasterSite.ModelError.ShowMessageError = "จำนวนรับสมัครมากกว่าจำนวนที่นั่งทั้งหมด";
                                        //this.MasterSite.ModelError.ShowModalError();
                                        //ModalExamSchedule.Show();
                                    }
                                    else
                                    {
                                        obj.NUMBER_SEAT_ROOM = input.ToShort();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (gvExamRoom.Rows.Count > 0)
                            {

                                foreach (GridViewRow gr in gvExamRoom.Rows)
                                {
                                    string RoomCode = ((Label)gr.FindControl("lblExamRoomCode")).Text;
                                    string input = ((TextBox)gr.FindControl("txtExamNumberSeatRoom")).Text;
                                    string seat = ((Label)gr.FindControl("lblSeatAmount")).Text;
                                    var obj = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).First();
                                    //obj.NUMBER_SEAT_ROOM = input.ToInt() > seat.ToInt() ? obj.NUMBER_SEAT_ROOM : input.ToShort();
                                    if (input.ToInt() > seat.ToInt())
                                    {
                                        InputOK = false;
                                        //this.MasterSite.ModelError.ShowMessageError = "จำนวนรับสมัครมากกว่าจำนวนที่นั่งทั้งหมด";
                                        //this.MasterSite.ModelError.ShowModalError();
                                        //ModalExamSchedule.Show();
                                    }
                                    else
                                    {
                                        obj.NUMBER_SEAT_ROOM = input.ToShort();
                                    }
                                }
                            }
                            lsExamSubLicense.Add(new DTO.ExamSubLicense()
                            {
                                EXAM_ROOM_CODE = ddlExamRoom.SelectedValue,
                                SEAT_AMOUNT = ebiz.GetSeatAmountRoom(ddlExamRoom.SelectedValue, ddlPlace_forRoom.SelectedValue).DataResponse.ToShort(),
                                ROOM_NAME = ddlExamRoom.SelectedItem.Text,
                                USER_ID = UserProfile.Id
                            });

                        }
                        BindGvExamRoom();
                        ddlExamRoom.Items.RemoveAt(ddlExamRoom.SelectedIndex);
                        ddlExamRoom.SelectedIndex = 0;
                    }
                    else
                    {

                        if (AddInput != "Add")
                        {
                            if (lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == AddInput).Count() > 0)
                            {
                                if (gvExamRoom.Rows.Count > 0)
                                {

                                    foreach (GridViewRow gr in gvExamRoom.Rows)
                                    {
                                        string RoomCode = ((Label)gr.FindControl("lblExamRoomCode")).Text;
                                        string input = ((TextBox)gr.FindControl("txtExamNumberSeatRoom")).Text;
                                        string seat = ((Label)gr.FindControl("lblSeatAmount")).Text;
                                        var obj = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).First();
                                        //obj.NUMBER_SEAT_ROOM = input.ToInt() > seat.ToInt() ? obj.NUMBER_SEAT_ROOM : input.ToShort();
                                        if (input.ToInt() > seat.ToInt())
                                        {
                                            InputOK = false;
                                            //this.MasterSite.ModelError.ShowMessageError = "จำนวนรับสมัครมากกว่าจำนวนที่นั่งทั้งหมด";
                                            //this.MasterSite.ModelError.ShowModalError();
                                            //ModalExamSchedule.Show();
                                        }
                                        else
                                        {
                                            obj.NUMBER_SEAT_ROOM = input.ToShort();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (gvExamRoom.Rows.Count > 0)
                                {

                                    foreach (GridViewRow gr in gvExamRoom.Rows)
                                    {
                                        string RoomCode = ((Label)gr.FindControl("lblExamRoomCode")).Text;
                                        string input = ((TextBox)gr.FindControl("txtExamNumberSeatRoom")).Text;
                                        string seat = ((Label)gr.FindControl("lblSeatAmount")).Text;
                                        var obj = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).First();
                                        //obj.NUMBER_SEAT_ROOM = input.ToInt() > seat.ToInt() ? obj.NUMBER_SEAT_ROOM : input.ToShort();
                                        if (input.ToInt() > seat.ToInt())
                                        {
                                            InputOK = false;
                                            //this.MasterSite.ModelError.ShowMessageError = "จำนวนรับสมัครมากกว่าจำนวนที่นั่งทั้งหมด";
                                            //this.MasterSite.ModelError.ShowModalError();
                                            //ModalExamSchedule.Show();
                                        }
                                        else
                                        {
                                            obj.NUMBER_SEAT_ROOM = input.ToShort();
                                        }
                                    }
                                }
                                //lsExamSubLicense.Add(new DTO.ExamSubLicense()
                                //{
                                //    EXAM_ROOM_CODE = AddInput,
                                //    SEAT_AMOUNT = ebiz.GetSeatAmountRoom(AddInput, ddlPlace_forRoom.SelectedValue).DataResponse.ToShort(),
                                //    ROOM_NAME = ddlExamRoom.SelectedItem.Text,
                                //    USER_ID = UserProfile.Id
                                //});

                            }
                            if (InputOK)
                                BindGvExamRoom();
                            ddlExamRoom.SelectedIndex = 0;
                        }
                        else
                        {
                            this.MasterSite.ModelError.ShowMessageError = SysMessage.ChooseData;
                            this.MasterSite.ModelError.ShowModalError();
                            ModalExamSchedule.Show();

                        }
                    }
                    txtExamUseSeatRoom.Text = lsExamSubLicense.Sum(x => x.NUMBER_SEAT_ROOM).ToString();
                }
                ModalExamSchedule.Show();
            }
            catch
            {
            }
            return InputOK;
        }

        protected void lbtnRemoveExamRoom_Click(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                btnChangeRoom.Visible = false;
                btnDelete.Visible = false;

                ModalExamSchedule.Show();
                GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                ddlExamRoom.DataTextField = "RoomName";
                ddlExamRoom.DataValueField = "RoomCode";
                ddlExamRoom.DataSource = lsEXRoomDDL;
                ddlExamRoom.DataBind();

                //if (lsExamSubLicense.Where(x => x.NUMBER_SEAT_ROOM == null).Count()>0)
                //{
                if (gvExamRoom.Rows.Count > 0)
                {

                    foreach (GridViewRow grr in gvExamRoom.Rows)
                    {
                        string RoomCodee = ((Label)grr.FindControl("lblExamRoomCode")).Text;
                        string input = ((TextBox)grr.FindControl("txtExamNumberSeatRoom")).Text;
                        string seat = ((Label)grr.FindControl("lblSeatAmount")).Text;
                        var obj = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCodee).First();
                        obj.NUMBER_SEAT_ROOM = input.ToInt() > seat.ToInt() ? obj.NUMBER_SEAT_ROOM : input.ToShort();
                    }
                }
                //}


                string RoomCode = ((Label)gr.FindControl("lblExamRoomCode")).Text;
                var ls = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).FirstOrDefault();
                if (ls != null)
                    lsExamSubLicense.Remove(lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).First());
                foreach (string RooomCode in lsExamSubLicense.Select(r => r.EXAM_ROOM_CODE))
                {
                    if (RooomCode == RoomCode)
                    {
                        int a = ddlExamRoom.Items.IndexOf((ListItem)ddlExamRoom.Items.FindByValue(RoomCode));
                        ddlExamRoom.Items.RemoveAt(a);
                    }
                }
                ddlExamRoom.Items.Insert(0, SysMessage.DefaultSelecting);
                GetExamRoom(true);
                ddlExamRoom.SelectedIndex = 0;
                SumExamSeatInPut();
                BindGvExamRoom();

                Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.EDIT.ToString();
                checkMode();
                txtExamUseSeatRoom.Text = lsExamSubLicense.Sum(x => x.NUMBER_SEAT_ROOM).ToString();
            }
        }

        private void GetExamSchedulePlaceGroup_forRoom()
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, base.UserProfile.CompCode);
                BindToDDL(ddlExamGroupCode, ls.DataResponse);
            }
        }

        protected void ddlExamGroupCode_SelectedIndexChanged(object sender, EventArgs e)//milk
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    GetExamPlaceByGroupPlace(ddlGroupType.SelectedValue.ToString());
                }
            }
            catch
            {
            }
        }

        //protected void txtExamNumberSeatRoom_lostFocus(object sender, EventArgs e)
        //{
        //    ModalExamSchedule.Show();
        //    var gr = (GridViewRow)((TextBox)sender).NamingContainer;
        //    Label txtRoom = (Label)gr.FindControl("lblExamRoomCode");
        //    InputRoomInGV(txtRoom.Text);
        //}

        private void GetExamRoom(Boolean Del = false)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {

                    if (ddlProvince.SelectedIndex > 0 && ddlExamGroupCode.SelectedIndex > 0 &&
                               ddlLicenseType.SelectedIndex > 0 && ddlPlace_forRoom.SelectedIndex > 0 && ddlDetailTimeExamCode.SelectedIndex > 0 && txtScheduleDetailDateExam.Text.Trim() != "")
                    {
                        ModalExamSchedule.Show();
                        BLL.ExamRoomBiz biz = new ExamRoomBiz();
                        var ls = biz.GetExamRoomByPlaceCodeAndTimeCode(ddlPlace_forRoom.SelectedValue.ToString(), ddlDetailTimeExamCode.SelectedItem.Text, txtScheduleDetailDateExam.Text, lsExamSubLicense, Del, txtScheduleExamCode.Text);
                        ddlExamRoom.Items.Clear();
                        ddlExamRoom.DataTextField = "Name";
                        ddlExamRoom.DataValueField = "Id";
                        lsEXRoomDDL = null;
                        foreach (DataRow dr in ls.DataResponse.Tables[0].Rows)
                        {
                            lsEXRoomDDL.Add(new DTO.ExamRoomDDLTemp
                            {
                                RoomCode = dr["Id"].ToString(),
                                RoomName = dr["Name"].ToString(),

                            });
                        }
                        ddlExamRoom.DataSource = ls.DataResponse;
                        ddlExamRoom.DataBind();
                        ddlExamRoom.Items.Insert(0, SysMessage.DefaultSelecting);
                        ddlExamRoom.SelectedIndex = 0;
                    }
                }
            }
            catch
            {
            }
        }

        private void GetLicenseType_forRoom(Boolean Show)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    if (ddlExamOnwer.SelectedIndex > 0)
                    {
                        BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                        var ls = biz.GetAssoLicense(ddlExamOnwer.SelectedValue);
                        ddlLicenseType.DataTextField = "License_type_name";
                        ddlLicenseType.DataValueField = "license_type_code";
                        ddlLicenseType.DataSource = ls.DataResponse;
                        ddlLicenseType.DataBind();
                        ddlLicenseType.Items.Insert(0, SysMessage.DefaultSelecting);
                        ddlLicenseType.SelectedIndex = 0;
                    }
                    if (Show)
                    {
                        ModalExamSchedule.Show();
                    }
                }
            }
            catch
            {
            }
        }

        private void GetExamPlaceByGroupPlace(string GroupType)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    if (ddlExamGroupCode.SelectedIndex > 0)
                        ModalExamSchedule.Show();

                    if ((ddlProvince.SelectedIndex > 0) && (ddlExamGroupCode.SelectedIndex > 0))
                    {

                        BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                        if (GroupType == "G")
                        {
                            var ls = biz.GetExamPlaceFromProvinceAndGroupCode(ddlProvince.SelectedValue, ddlExamGroupCode.SelectedValue);
                            ddlPlace_forRoom.DataTextField = "Name";
                            ddlPlace_forRoom.DataValueField = "Id";
                            ddlPlace_forRoom.DataSource = ls;
                        }
                        else if (GroupType == "A")
                        {
                            var ls = biz.GetExamPlaceFromProvinceAndAssoCode(ddlProvince.SelectedValue, ddlExamGroupCode.SelectedValue);
                            ddlPlace_forRoom.DataTextField = "Name";
                            ddlPlace_forRoom.DataValueField = "Id";
                            ddlPlace_forRoom.DataSource = ls;
                        }
                        ddlPlace_forRoom.DataBind();
                        ddlPlace_forRoom.Items.Insert(0, SysMessage.DefaultSelecting);
                        ddlPlace_forRoom.SelectedIndex = 0;

                        txtFree.Visible = false;
                    }
                    else
                    {
                        ddlPlace_forRoom.Items.Clear();
                        ddlPlace_forRoom.Items.Insert(0, SysMessage.DefaultSelecting);
                        ddlPlace_forRoom.SelectedIndex = 0;
                    }
                }
            }
            catch
            {
            }
        }


        protected void txtExamNumberSeatRoom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    // ModalExamSchedule.Show();
                    Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.EDIT.ToString();
                    checkMode();
                }
            }
            catch
            {
            }
        }

        private Boolean SumSeat()
        {
            Boolean SumOK = true;
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                    || (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                    || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                    || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    ModalExamSchedule.Show();
                    if (InputRoomInGV(""))
                    {
                        foreach (GridViewRow gr in gvExamRoom.Rows)
                        {
                            string RoomCode = ((Label)gr.FindControl("lblExamRoomCode")).Text;
                            string input = ((TextBox)gr.FindControl("txtExamNumberSeatRoom")).Text;
                            Label seat = ((Label)gr.FindControl("lblSeatAmount"));
                            TextBox txtExamNumberSeatRoom = (TextBox)gr.FindControl("txtExamNumberSeatRoom");
                            var obj = lsExamSubLicense.Where(x => x.EXAM_ROOM_CODE == RoomCode).First();
                            if (input.ToInt() > seat.Text.ToInt())
                            {
                                // txtExamNumberSeatRoom.Text = "";
                                return SumOK = false;
                            }
                            else
                            {
                                obj.NUMBER_SEAT_ROOM = input.ToShort();
                            }
                        }
                        if (SumOK)
                            SumExamSeatInPut();
                    }
                    else
                    {
                        SumOK = false;
                    }
                }
            }
            catch
            {
            }
            return SumOK;

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.DEL.ToString();
                    checkMode();
                    ModalExamSchedule.Show();
                    if (txtScheduleExamCode.Text != "")
                    {
                        BLL.ExamScheduleBiz biz = new ExamScheduleBiz();
                        DTO.ResponseMessage<bool> res = biz.SaveDeleteApplicantRoom(txtScheduleExamCode.Text, ddlPlace_forRoom.SelectedValue.ToString(), base.UserId);
                        if (res.ErrorMsg == null)
                        {
                            this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.DeleteSuccess;
                            this.MasterSite.ModelSuccess.ShowModalSuccess();
                            ClearAllData();
                            btnSearch_Click(sender, e);
                        }
                        else
                        {
                            this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                            this.MasterSite.ModelError.ShowModalError();
                        }

                        ModalExamSchedule.Hide();
                    }
                }
            }
            catch
            {
            }
        }

        protected void btnExamScheduleCancel_Click(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                setShowRoom(true);
                ModalExamSchedule.Hide();
                ClearAllData();
                UplPopUp.Update();
                DefaultData_forRoom();
                txtFree.Visible = false;
                btnDelete.Visible = false;
                btnChangeRoom.Visible = false;


                Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.DEFULT.ToString();
                checkMode();
            }
        }


        protected void btnExamScheduleSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string errMsg = "";

                //if (ddlDetailTimeExamCode.SelectedIndex <= 0)
                // {
                //     errMsg += "- กรุณาระบุเวลาที่สอบ<br>";
                // }

                if (txtScheduleDetailDateExam.Text.Trim() == "")
                {
                    errMsg += "- กรุณาระบุวันที่สอบ<br>";
                }
                if (errMsg == "")
                {
                    if (ddlProvince.SelectedIndex > 0 && ddlExamGroupCode.SelectedIndex > 0 && ddlExamOnwer.SelectedIndex > 0 &&
                             ddlLicenseType.SelectedIndex > 0 && ddlPlace_forRoom.SelectedIndex > 0 &&
                             ddlDetailTimeExamCode.SelectedIndex > 0 && txtScheduleDetailDateExam.Text.Trim() != "")
                    {
                        if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                            || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                            || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                            || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                        {
                            ModalExamSchedule.Show();
                            if (!SumSeat())
                            {
                                this.MasterSite.ModelError.ShowMessageError = "กรุณาตรวจสอบจำนวนรับสมัคร";
                                this.MasterSite.ModelError.ShowModalError();
                            }
                            else
                            {

                                if (gvExamRoom.Rows.Count <= 0)
                                {

                                    errMsg += "- กรุณากำหนดห้องสอบ <br>";

                                }
                                else if ((txtExamUseSeatRoom.Text == "") || (txtExamUseSeatRoom.Text == "0"))
                                {

                                    errMsg += "- กรุณาระบุจำนวนคนที่รับสมัคร <br>";

                                }
                                if (txtScheduleExamCode.Text.Trim() == "")
                                {
                                    if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                                    {
                                        DateTime temp = Convert.ToDateTime(txtScheduleDetailDateExam.Text);
                                        DateTime NowAdd5day = DateTime.Now.AddDays(5);
                                        if (Convert.ToDateTime(temp.Day + "/" + temp.Month + "/" + temp.Year) <= Convert.ToDateTime(NowAdd5day.Day + "/" + NowAdd5day.Month + "/" + NowAdd5day.Year))
                                        {
                                            errMsg += "- วันที่สอบต้องมากกว่าวันที่ปัจจุบัน 5 วันขึ้นไป <br>";
                                        }
                                    }
                                }
                                if (errMsg != "")
                                {

                                    this.MasterSite.ModelError.ShowMessageError = errMsg;
                                    this.MasterSite.ModelError.ShowModalError();
                                    ModalExamSchedule.Show();
                                }
                                else
                                {
                                    BLL.ExamScheduleBiz bizz = new ExamScheduleBiz();
                                    BLL.PaymentBiz Pbiz = new PaymentBiz();
                                    #region CheckHoliday
                                    DTO.ResponseMessage<bool> IsHoliday = Pbiz.CheckHolidayDate(txtScheduleDetailDateExam.Text);
                                    if (IsHoliday.ResultMessage)
                                    {
                                        ModalExamSchedule.Show();
                                        mpe.Show();
                                         
                                    }
                                    else
                                    {
                                        confirmSave(sender, e);
                                        Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.DEFULT.ToString();
                                        checkMode();
                                    }
                                    #endregion CheckHoliday

                                }

                            }
                        }
                    }
                }
                else
                {

                    this.MasterSite.ModelError.ShowMessageError = errMsg;
                    this.MasterSite.ModelError.ShowModalError();
                    ModalExamSchedule.Show();
                }
            }
            catch
            {
            }
        }

        private void confirmSave(object sender, EventArgs e)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    Boolean CanChange = true;
                    BLL.ExamScheduleBiz bizz = new ExamScheduleBiz();
                    BLL.PaymentBiz Pbiz = new PaymentBiz();
                    BLL.PersonBiz PerBiz = new PersonBiz();
                    if ((txtScheduleExamCode.Text != "") && (txtScheduleExamCode.Text != "0"))//กรณีแก้ไข หรือ เลื่อนรอบ ที่มีผู้สมัครแล้ว
                    {
                        string CountSeatUse = bizz.CountCountSeatUse(txtScheduleExamCode.Text).DataResponse.ToString(); //จำนวนคนที่สอบไปแล้วในรอบนั้น
                        ResponseMessage<bool> res = PerBiz.CheckAuthorityEditExam(base.UserProfile, txtScheduleExamCode.Text, txtScheduleDetailDateExam.Text);
                        if (txtExamUseSeatRoom.Text.ToInt() < CountSeatUse.ToInt())
                        {
                            CanChange = false;
                            this.MasterSite.ModelError.ShowMessageError = "ไม่มีสิทธิ์ในการเลื่อนรอบสอบ";
                            this.MasterSite.ModelError.ShowModalError();
                        }
                        else if (res.ResultMessage == false)
                        {
                            CanChange = false;
                            this.MasterSite.ModelError.ShowMessageError = "ไม่สามารถเลื่อนรอบสอบย้อนหลังได้";
                            this.MasterSite.ModelError.ShowModalError();
                        }
                    }

                    SaveSetApplicantRoom lr = new SaveSetApplicantRoom();
                    string testing_NO = string.Empty;
                    BLL.ExamScheduleBiz biz = new ExamScheduleBiz();

                    if (txtScheduleExamCode.Text == "")
                        testing_NO = biz.GenLicenseNo(ddlPlace_forRoom.Text, base.UserId).DataResponse;
                    else
                        testing_NO = txtScheduleExamCode.Text;

                    if (CanChange)
                    {
                        if ((txtScheduleExamCode.Text.Trim().Length == 0))//กรณีสร้างรอบสอบใหม่
                        {
                            lr.TESTING_NO = testing_NO;
                            lr.EXAM_PLACE_CODE = ddlPlace_forRoom.Text;
                            lr.COURSE_NUMBER = txtCourseNum.Text;
                            lr.TESTING_DATE = Convert.ToDateTime(txtScheduleDetailDateExam.Text);
                            lr.TEST_TIME_CODE = ddlDetailTimeExamCode.SelectedValue.ToString();
                            lr.LICENSE_TYPE_CODE = ddlLicenseType.SelectedValue.ToString();
                            lr.EXAM_STATUS = "E";
                            lr.EXAM_APPLY = 0;
                            // lr.EXAM_ADMISSION = Convert.ToInt16(txtExamUseSeatRoom.Text);
                            lr.EXAM_OWNER = ddlExamOnwer.SelectedValue.ToString();
                            lr.SPECIAL = ddlSpecial.SelectedValue.ToString();
                            lr.EXAM_REMARK = null;
                        }
                        else // กรณีเลื่อนรอบของพี่ฟิลด์ กับ ของเพิ่มห้องของมิ้ว
                        {
                            testing_NO = txtScheduleExamCode.Text;
                            lr.EXAM_PLACE_CODE = ddlPlace_forRoom.Text;
                            lr.TESTING_NO = testing_NO;
                            lr.TESTING_DATE = Convert.ToDateTime(Session["TestingDate"].ToString());// Convert.ToDateTime(txtScheduleDetailDateExam.Text);
                            lr.TEST_TIME_CODE = ddlDetailTimeExamCode.SelectedValue.ToString();
                            if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.MOVE.ToString())
                            {
                                lr.EXAM_REMARK = "จาก " + Convert.ToDateTime(Session["TestingDate"].ToString()).ToString("dd/MM/yyyy") + "(" + Session["ExamTime"].ToString() + ") " +
                                                    "ถึง " + Convert.ToDateTime(txtScheduleDetailDateExam.Text).ToString("dd/MM/yyyy") + "(" + ddlDetailTimeExamCode.SelectedItem.ToString() + ") "
                                                     + "|" + txtRemark.Text.ToString();
                            }
                            else
                            {
                                lr.EXAM_REMARK = txtRemark.Text.ToString();
                            }
                        }


                        if (Convert.ToDateTime(txtScheduleDetailDateExam.Text) < DateTime.Now.Date)
                        {
                            lr.IMPORT_TYPE = "W";
                        }
                        else
                        {
                            lr.IMPORT_TYPE = "N";
                        }
                        lr.EXAM_ADMISSION = Convert.ToInt16(txtExamUseSeatRoom.Text);
                        lr.EXAM_ROOM_CODE = lsExamSubLicense;

                        DTO.ResponseMessage<bool> Save_SetApplicantRoom = biz.SaveSetApplicantRoom(lr, Session["MANAGE_ROOM_MODE"].ToString());
                        if (Save_SetApplicantRoom.IsError)
                        {
                            this.MasterSite.ModelError.ShowMessageError = Save_SetApplicantRoom.ErrorMsg;
                            this.MasterSite.ModelError.ShowModalError();
                        }
                        else
                        {

                            if (txtScheduleExamCode.Text != "")
                            {
                                if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.MOVE.ToString())
                                {
                                    //mapPath = WebConfigurationManager.AppSettings["UploadRecieveLicense"];
                                    string emailOut = WebConfigurationManager.AppSettings["EmailOut"];

                                    string mailSubject = "แจ้งเลื่อนการสอบ \n";
                                    StringBuilder emailBody = new StringBuilder();
                                    emailBody.AppendLine(string.Format("สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย (คปภ.) \n"));
                                    emailBody.AppendLine(string.Format("ประกาศแจ้งเลื่อนการสอบของตัวแทน/นายหน้า ในระบบช่องทางการบริการตัวแทน/นายหน้าประกันภัยแบบเบ็ดเสร็จ \n"));
                                    emailBody.AppendLine(string.Format("โดยมีรายละเอียดดังนี้ \n"));
                                    emailBody.AppendLine(string.Format("รหัสสอบ {0} \n", lr.TESTING_NO));
                                    emailBody.AppendLine(string.Format("สอบวันที่ {0} เวลา {1} \n\n", Session["TestingDate"].ToString(), Session["ExamTime"].ToString()));
                                    emailBody.AppendLine(string.Format("เลื่อนไปสอบวันที่ {0} เวลา {1} \n\n", txtScheduleDetailDateExam.Text, ddlDetailTimeExamCode.SelectedItem.ToString()));
                                    emailBody.AppendLine(string.Format("เหตุผลในการเลื่อน {0} \n\n", lr.EXAM_REMARK));
                                    emailBody.AppendLine(string.Format("ด้วยความเคารพ \n"));
                                    emailBody.AppendLine(string.Format("สำนักงานคณะกรรมการกำกับและส่งเสริมการประกอบธุรกิจประกันภัย (คปภ.) \n"));
                                    emailBody.AppendLine(string.Format("โทร: xxxxxxxxxx "));
                                    emailBody.AppendLine(string.Format(emailOut));
                                    PersonBiz pbiz = new BLL.PersonBiz();
                                    // List<string> ls = pbiz.GetEmailMoveExam(txtScheduleExamCode.Text);
                                    IEnumerable<String> ls = pbiz.GetEmailMoveExam(txtScheduleExamCode.Text);
                                    //BLL.Mail.Send(ls, mailSubject, Convert.ToString(emailBody));
                                    EmailServiceFactory.GetEmailService().SendMail(emailOut, ls, mailSubject, Convert.ToString(emailBody));
                                }
                                this.MasterSite.ModelSuccess.ShowMessageSuccess = "แก้ไขสำเร็จ  (รหัสรอบสอบ ที่ " + testing_NO + ")";


                            }
                            else
                            {
                                this.MasterSite.ModelSuccess.ShowMessageSuccess = "บันทึกสำเร็จ  (รหัสรอบสอบ คือ " + testing_NO + ")";
                            }

                            this.MasterSite.ModelSuccess.ShowModalSuccess();
                            //   ClearAllData();
                            txtFree.Visible = false;
                            setShowRoom(true);
                        }

                    }

                    ClearAllData();
                    btnSearch_Click(sender, e);
                    ModalExamSchedule.Hide();
                }
            }
            catch
            {
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    GetExamPlaceByGroupPlace(ddlGroupType.SelectedValue.ToString());
                    Addroom(true);
                    ModalExamSchedule.Show();
                }

            }
            catch
            {
            }
        }

        protected void ddlPlace_forRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                Addroom(true);
            }
        }

        protected void ddlLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                //getOnwer();
                if (CheckCourse())
                    Addroom(true);
                else
                {
                    ModalExamSchedule.Show();
                    this.MasterSite.ModelError.ShowMessageError = "ยังไม่มีหลักสูตรในประเภทใบอนุญาตนี้";
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
        }

        private bool CheckCourse()
        {
            Boolean chkCourse = true;
            try
            {
                ExamScheduleBiz biz = new ExamScheduleBiz();
                var ls = biz.GetCourseNumber(ddlLicenseType.SelectedValue.ToString());
                if (ls.DataResponse != "")
                    txtCourseNum.Text = ls.DataResponse.ToString();
                else
                    chkCourse = false;
            }
            catch
            {
                chkCourse = false;
            }
            return chkCourse;
        }

        private void Addroom(Boolean Show)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    ModalExamSchedule.Show();

                    if (Session["MANAGE_ROOM_MODE"].ToString() != DTO.ManageExamRoom_MODE.EDIT.ToString())
                    {

                        if (Show)
                        {
                            if (ddlProvince.SelectedIndex > 0 && ddlExamGroupCode.SelectedIndex > 0 && ddlExamOnwer.SelectedIndex > 0 &&
                                     ddlLicenseType.SelectedIndex > 0 && ddlPlace_forRoom.SelectedIndex > 0 &&
                                     ddlDetailTimeExamCode.SelectedIndex > 0 && txtScheduleDetailDateExam.Text.Trim() != "")
                            {
                                if (Session["MANAGE_ROOM_MODE"].ToString() != DTO.ManageExamRoom_MODE.MOVE.ToString())
                                {
                                    GetExamRoom();
                                    // setShowRoom(!Show);
                                    ddlExamRoom.Enabled = true;
                                    ddlExamRoom.Visible = true;
                                    btnAddExamRoom.Enabled = true;
                                    gvExamRoom.DataSource = null;
                                    gvExamRoom.DataBind();
                                    lsExamSubLicense = null;
                                    FS_Room.Visible = true;
                                    btnAddExamRoom.Visible = true;
                                    btnExamScheduleSubmit.Visible = true;
                                }
                            }
                            else
                            {
                                FS_Room.Visible = false;
                                btnAddExamRoom.Visible = false;
                                btnExamScheduleSubmit.Visible = false;
                                gvExamRoom.Visible = false;
                                ddlExamRoom.Items.Clear();
                                ddlExamRoom.Enabled = false;
                                btnAddExamRoom.Enabled = false;
                                gvExamRoom.DataSource = null;
                                gvExamRoom.DataBind();
                                lsExamSubLicense = null;
                                //  setShowRoom(Show);
                            }
                        }
                        else
                        {
                            FS_Room.Visible = true;
                            btnAddExamRoom.Visible = true;
                            btnExamScheduleSubmit.Visible = true;
                            setShowRoom(Show);
                            gvExamRoom.DataSource = null;
                            gvExamRoom.DataBind();
                            lsExamSubLicense = null;
                        }
                    }
                }
            }
            catch
            {
            }
        }



        private void setShowRoom(bool Show)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                ddlDetailTimeExamCode.Enabled = Show;
                ddlExamGroupCode.Enabled = Show;
                ddlLicenseType.Enabled = Show;
                ddlProvince.Enabled = Show;
                ddlPlace_forRoom.Enabled = Show;
                ddlSpecial.Enabled = Show;
                ddlGroupType.Enabled = Show;
                if (base.UserProfile.MemberType != DTO.RegistrationType.Association.GetEnumValue())
                {
                    ddlExamOnwer.Enabled = Show;
                }
                else
                {
                    ddlExamOnwer.Enabled = false;
                }
                txtScheduleDetailDateExam.Enabled = Show;
                gvExamRoom.Visible = !Show;
                btnAddExamRoom.Enabled = !Show;
                ddlExamRoom.Enabled = !Show;
            }
        }

        protected void ddlDetailTimeExamCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                Addroom(true);
            }
        }

        protected void txtScheduleDetailDateExam_TextChanged(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                Addroom(true);
            }
        }

        protected void btnChangeRoom_Click(object sender, EventArgs e)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    Session["MANAGE_ROOM_MODE"] = DTO.ManageExamRoom_MODE.MOVE.ToString();

                    ModalExamSchedule.Show();
                    txtRemark.Enabled = true;
                    txtScheduleDetailDateExam.Enabled = true;
                    ddlDetailTimeExamCode.Enabled = true;
                    ddlPlace_forRoom.Enabled = false;
                    ddlSpecial.Enabled = false;
                    ddlExamGroupCode.Enabled = false;
                    ddlLicenseType.Enabled = false;
                    ddlProvince.Enabled = false;
                    ddlExamOnwer.Enabled = false;
                    txtExamUseSeatRoom.Enabled = false;
                    btnDelete.Visible = false;
                    btnChangeRoom.Visible = false;

                    //lsExamSubLicense = null;
                    ddlExamRoom.Enabled = false;
                    gvExamRoom.Enabled = false;
                    btnAddExamRoom.Enabled = false;
                    gvExamRoom.DataSource = lsExamSubLicense;
                    gvExamRoom.DataBind();


                    Session["ExamSubLicense"] = null;
                    //ddlExamRoom.Items.Clear();
                    //GetTime("");
                    // txtExamUseSeatRoom.Text = "";
                }
            }
            catch
            {
            }
        }

        protected void ddlSpecial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                ModalExamSchedule.Show();
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                confirmSave(sender, e);
            }
        }

        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) ||
                    (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    ModalExamSchedule.Show();
                    clearAll(false);
                    if (ddlGroupType.SelectedValue != "")
                    {
                        switch (ddlGroupType.SelectedValue.ToString())
                        {
                            case "A":
                                GetAssociation();
                                break;
                            case "G":
                                GetExamGRoup();
                                break;
                            default:

                                break;
                        }


                    }
                }
            }
            catch
            {

            }
        }

        private void clearAll(Boolean all)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                ddlExamGroupCode.Items.Clear();
                gvExamRoom.DataSource = null;
                gvExamRoom.Visible = false;
                ddlProvince.SelectedIndex = 0;
                ddlPlace_forRoom.Items.Clear();
                ddlPlace_forRoom.Items.Insert(0, SysMessage.DefaultSelecting);
                ddlPlace_forRoom.SelectedIndex = 0;
                ddlLicenseType.Items.Clear();

                if (all)
                    ddlGroupType.SelectedIndex = 0;
            }

        }

        protected void ddlExamOnwer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
            {
                GetLicenseType_forRoom(true);

                Addroom(true);
            }
        }

        private void GetddlGroupType()
        {
            try
            {
                if ((base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue()) || (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue()))
                {
                    ModalExamSchedule.Show();
                    BLL.ExamRoomBiz biz = new ExamRoomBiz();
                    var OldData = biz.GetExamByTestingNo(txtScheduleExamCode.Text);

                    ddlGroupType.SelectedValue = biz.GetddlGroupType(OldData.DataResponse.EXAM_PLACE_CODE.ToString()).DataResponse.Tables[0].Rows[0][0].ToString();

                    ddlGroupType.Enabled = false;
                }
            }
            catch
            {
            }
        }

        #endregion 2.1.5

        protected void gvListExam__RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            List<DTO.AddApplicant> newls = SortApplicant(lstApplicant.ToList(), CurrentIndex, e.CommandName);

            //Rebind
            this.gvListExam.DataSource = newls;
            this.gvListExam.DataBind();
            //this.UplListExam.Update();
            this.ModalListExam.Show();
            //upnListExam.Update();
        }
        public List<DTO.AddApplicant> SortApplicant(List<DTO.AddApplicant> ls, int CurrentIndex, String Mode)
        {
            /*foreach (AddApplicant item in ls)
            {
                var AllOrder = ls.FirstOrDefault(a => a.ExamNumber == item.ExamNumber);
                AllOrder.RUN_NO = Convert.ToString(ls.IndexOf(AllOrder) + 1);
            }*/

            for (int i = 0; i < lstApplicant.Count; i++)
            {
                lstApplicant[i].RUN_NO = (i + 1).ToString();
            }
            if (Mode.Equals("Up"))
            {
                //Get Current & Previous Data
                AddApplicant cur = lstApplicant[CurrentIndex];
                AddApplicant pre = lstApplicant[CurrentIndex - 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = pre.RUN_NO;
                pre.RUN_NO = Convert.ToString(order);

                //Resort
                lstApplicant[CurrentIndex] = pre;
                lstApplicant[CurrentIndex - 1] = cur;
            }
            else if (Mode.Equals("Down"))
            {
                //Get Current & Next Data
                AddApplicant cur = lstApplicant[CurrentIndex];
                AddApplicant next = lstApplicant[CurrentIndex + 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = next.RUN_NO;
                next.RUN_NO = Convert.ToString(order);

                //Resort
                lstApplicant[CurrentIndex] = next;
                lstApplicant[CurrentIndex + 1] = cur;
            }

            return lstApplicant.OrderBy(idx => idx.RUN_NO).ToList();

        }

        private void SaveConfirm()
        {
            if (Session["lstApplicant"] == null)
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();

                lstApplicant = new List<DTO.AddApplicant>();
                var arrComp = ddlCompanyCode.SelectedValue.Split('[', ']');

                string strInSurCompCode = string.Empty;
                if ((arrComp[0]) == "0")
                {
                    strInSurCompCode = "";
                }
                else
                {
                    strInSurCompCode = arrComp[1];
                }
                lstApplicant.Add(new DTO.AddApplicant
                {
                    ExamNumber = Session["ExamNumber"].ToString(),
                    ExamDate = (Convert.ToDateTime(Session["ExamDate"])),
                    ExamTime = Session["ExamTime"].ToString(),
                    ExamPlaceGroup = Session["ExamPlaceGroup"].ToString(),
                    ExamPlace = Session["ExamPlace"].ToString(),
                    Province = Session["Province"].ToString(),
                    Seat = Session["Seat"].ToString(),
                    LicenseTypeName = Session["LicenseTypeName"].ToString(),
                    ExamFee = Session["ExamFee"].ToString(),
                    AgentType = Session["AgentType"].ToString(),
                    ExamPlaceCode = Session["PlaceCode"].ToString(),
                    InSurCompCode = strInSurCompCode,
                    ApplyDate = DateTime.Now,
                });
                Session["lstApplicant"] = lstApplicant;
                ModalExamSchedule.Hide();
            }
            else
            {

                lstApplicant = (List<DTO.AddApplicant>)Session["lstApplicant"];
                var arrComp = ddlCompanyCode.SelectedValue.Split('[', ']');
                if (lstApplicant.Where(r => r.ExamNumber == txtDetailExamCode.Text).Count() == 0)
                {
                    string strInSurCompCode = string.Empty;
                    if ((arrComp[0]) == "0")
                    {
                        strInSurCompCode = "";
                    }
                    else
                    {
                        strInSurCompCode = arrComp[1];
                    }
                    lstApplicant.Add(new DTO.AddApplicant
                    {
                        ExamNumber = Session["ExamNumber"].ToString(),
                        ExamDate = (Convert.ToDateTime(Session["ExamDate"])),
                        ExamTime = Session["ExamTime"].ToString(),
                        ExamPlaceGroup = Session["ExamPlaceGroup"].ToString(),
                        ExamPlace = Session["ExamPlace"].ToString(),
                        Province = Session["Province"].ToString(),
                        Seat = Session["Seat"].ToString(),
                        LicenseTypeName = Session["LicenseTypeName"].ToString(),
                        ExamFee = Session["ExamFee"].ToString(),
                        AgentType = Session["AgentType"].ToString(),
                        ExamPlaceCode = Session["PlaceCode"].ToString(),
                        InSurCompCode = strInSurCompCode,
                        ApplyDate = DateTime.Now,
                    });
                    Session["lstApplicant"] = lstApplicant;
                    ModalExamSchedule.Hide();
                }
                else
                {
                    for (int i = 0; i < lstApplicant.Count; i++)
                    {
                        if (lstApplicant[i].ExamNumber == txtDetailExamCode.Text)
                        {
                            lstApplicant[i].ExamDate = Convert.ToDateTime(txtDetailDateExam.Text);
                            Session["lstApplicant"] = lstApplicant;
                            break;
                        }
                    }
                    ModalExamSchedule.Hide();
                }

            }

        }

        protected void btnConfirmExamList_Click(object sender, EventArgs e)
        {
            SaveConfirm();
            GetExamList();
            if (gvListExam.Rows.Count > 0)
            {
                ModalListExam.Show();
            }
            else
            {
                ModalExamSchedule.Hide();
            }
        }

        private void checkMode()
        {
            try
            {
                if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.ADD.ToString())
                {
                    // btnExamScheduleSubmit.Visible = true;
                    // btnChangeRoom.Visible=false;
                    // btnDelete.Visible=false;
                    // btnExamScheduleSubmit.Visible=true;
                    //btnChangeRoom.Visible=true;
                    //btnDelete.Visible=true;
                    //txtScheduleDetailDateExam.Enabled=false;
                    //ddlDetailTimeExamCode.Enabled=false;
                    btnAddExamRoom.Visible = false;
                    btnExamScheduleSubmit.Visible = false;
                    FS_Room.Visible = false;
                    txtRemark.Text = "";
                    //ddlExamRoom.Enabled=true;
                    //gvExamRoom.Enabled=true;
                }
                else if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.DEFULT.ToString())
                {

                    FS_Room.Visible = true;
                    btnExamScheduleSubmit.Visible = true;
                    btnChangeRoom.Visible = true;
                    btnDelete.Visible = true;
                    btnChangeRoom.Enabled = true;
                    btnDelete.Enabled = true;
                    txtScheduleDetailDateExam.Enabled = false;
                    ddlDetailTimeExamCode.Enabled = false;
                    btnAddExamRoom.Visible = true;
                    btnAddExamRoom.Enabled = true;
                    ddlExamRoom.Enabled = true;
                    ddlExamRoom.Visible = true;
                    gvExamRoom.Enabled = true;
                    txtRemark.Enabled = false;
                }
                else if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.EDIT.ToString())
                {
                    FS_Room.Visible = true;
                    btnExamScheduleSubmit.Visible = true;
                    btnChangeRoom.Visible = false;
                    btnDelete.Visible = false;
                    txtScheduleDetailDateExam.Enabled = false;
                    ddlDetailTimeExamCode.Enabled = false;
                    btnAddExamRoom.Enabled = true;
                    btnAddExamRoom.Visible = true;
                    ddlExamRoom.Enabled = true;
                    ddlExamRoom.Visible = true;
                    gvExamRoom.Enabled = true;

                }
                else if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.DEL.ToString())
                {

                }
                else if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.VIEW.ToString())
                {
                    FS_Room.Visible = true;
                    btnExamScheduleSubmit.Visible = false;
                    btnChangeRoom.Visible = false;
                    btnDelete.Visible = false;
                    txtScheduleDetailDateExam.Enabled = false;
                    ddlDetailTimeExamCode.Enabled = false;
                    btnAddExamRoom.Visible = false;
                    ddlExamRoom.Enabled = false;
                    ddlExamRoom.Visible = false;
                    gvExamRoom.Enabled = false;
                    txtRemark.Enabled = false;
                }
                else if (Session["MANAGE_ROOM_MODE"].ToString() == DTO.ManageExamRoom_MODE.MOVE.ToString())
                {
                    FS_Room.Visible = true;
                    btnExamScheduleSubmit.Visible = true;
                    btnChangeRoom.Visible = false;
                    btnDelete.Visible = false;
                    txtScheduleDetailDateExam.Enabled = true;
                    ddlDetailTimeExamCode.Enabled = true;
                    btnAddExamRoom.Enabled = false;
                    btnAddExamRoom.Visible = false;
                    ddlExamRoom.Enabled = false;
                    ddlExamRoom.Visible = false;
                    gvExamRoom.Enabled = false;
                    txtRemark.Enabled = true;
                }



            }
            catch
            {
            }
        }

        private void SaveSubmit(List<ApplicantTemp> lst)
        {
            string group = string.Empty;
            string passGroup = string.Empty;

            ApplicantBiz biz = new ApplicantBiz();
            var res = biz.InsertSingleApplicant(lst, base.UserId);
            if (res.IsError)
            {
                this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                ModalListExam.Hide();
                Session["lstApplicant"] = null;
                this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.SuccessInsertApplicant;
                this.MasterSite.ModelSuccess.ShowModalSuccess();
                group = res.DataResponse;
                passGroup = group + " " + base.UserId;

                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupSingle('" + passGroup + "')", true);
            }

        }

        private void validateApplicant()
        {


        }

        protected void btnCancelConfirmExamList_Click(object sender, EventArgs e)
        {
            string strExamNumber = Session["TestingNo"].ToString();
            //ลบข้อมูลที่ไม่ให้สอบ
            List<DTO.AddApplicant> lstApplicants = (List<DTO.AddApplicant>)Session["lstApplicant"];
            if (lstApplicants.Count() > 0)
            {
                foreach (var item in lstApplicant.ToList())
                {
                    if (item.ExamNumber == strExamNumber)
                    {
                        lstApplicant.Remove(item);
                    }
                }
                Session["lstApplicant"] = lstApplicant;
            }
        }


    }
}
