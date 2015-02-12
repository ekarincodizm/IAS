using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using AjaxControlToolkit;
using System.Data;
using IAS.BLL;
using IAS.Properties;
using IAS.DTO;

namespace IAS.Applicant
{
    public partial class ManageApplicant : basepage
    {
        #region Public Param & Session
        public List<string> Manage_App
        {
            get
            {
                if (Session["ManageApp"] == null)
                {
                    Session["ManageApp"] = new List<string>();
                }

                return (List<string>)Session["ManageApp"];
            }

            set
            {
                Session["ManageApp"] = value;
            }
        }
        public int PAGE_SIZE;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

   
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataCenterBiz biz = new DataCenterBiz();
                var Auto = biz.GetConficValueByTypeAndGroupCode("09", "AP001");
                if (Auto.DataResponse == "0")
                {
                    Manage_App = new List<string>();
                    base.HasPermit();
                    defaultData();
                    PnlSearch.Visible = true;
                }
                else
                {

                    base.HasPermit();
                    UCModalError.ShowMessageError = "การจัดผู้สมัครสอบเข้าสู่ห้องสอบเป็นการจัดการแบบ<u>อัตโนมัติ</u><br/>ผู้ใช้งานไม่สามารถดำเนินการได้ หากมีปัญหากรุณาติดต่อเจ้าหน้าที่ดูแลระบบ";
                    UCModalError.ShowModalError();
                    PnlSearch.Visible = false;
                }
                //GetPlaceGroup();
                //gv3.Visible = false;
                //gv2.Visible = false;
               
            }
        }

        private void GetAssociationDefult()
        {
            try
            {
                
                    string AssC = "";
                    DataCenterBiz biz = new DataCenterBiz();
                    if (base.UserRegType.GetEnumValue() == DTO.RegistrationType.Association.GetEnumValue())
                        AssC = base.UserProfile.CompCode;

                    var ls = biz.GetAssociation(AssC);
                   // var ls = biz.GetAssociationJoinLicense(AssC, ddlLicenseType.SelectedValue.ToString());
                    ddlAsso.DataValueField = "ASSOCIATION_CODE";
                    ddlAsso.DataTextField = "ASSOCIATION_NAME";

                    ddlAsso.DataSource = ls.DataResponse;
                    ddlAsso.DataBind();

                    ddlAsso.Items.Insert(0, SysMessage.DefaultSelecting);

                    ddlAsso.SelectedValue = AssC;
                    ddlAsso.Enabled = false;
            }
            catch
            {
            }
        }
     
        private void GetAssociation()
        {
            try
            {
                if (ddlLicenseType.SelectedIndex > 0)
                {
                  string AssC = "";
                    DataCenterBiz biz = new DataCenterBiz();
                    if (base.UserRegType.GetEnumValue() == DTO.RegistrationType.Association.GetEnumValue())
                        AssC = base.UserProfile.CompCode;


                    var ls = biz.GetAssociationJoinLicense(AssC, ddlLicenseType.SelectedValue.ToString());
                    ddlAsso.DataValueField = "ASSOCIATION_CODE";
                    ddlAsso.DataTextField = "ASSOCIATION_NAME";

                    ddlAsso.DataSource = ls.DataResponse;
                    ddlAsso.DataBind();

                    ddlAsso.Items.Insert(0, SysMessage.DefaultSelecting);
                }
                else
                {
                    UCModalError.ShowMessageError = "กรุณาเลือกประเภทใบอนุญาตที่สอบ";
                    UCModalError.ShowModalError();
                }
            }
            catch
            { 
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtStartExamDate.Text) > Convert.ToDateTime(txtEndExamDate.Text)) 
            {
                UCModalError.ShowMessageError = "วันที่สอบ(เริ่ม) ต้องมากกว่าหรือเท่ากับ วันที่สอบ(สิ้นสุด)";
                UCModalError.ShowModalError();
            }
            else if (Convert.ToDateTime(txtStartExamDate.Text) <DateTime.Now)
            {
                UCModalError.ShowMessageError = "วันที่สอบ(เริ่ม) ต้องมากกว่าวันที่ปัจจุบันถึงจะสามารถจัดคนเข้า/ออกห้องสอบได้ ";
                UCModalError.ShowModalError();
            }
            else
            {
                Manage_App = null;

                //if ((ddlAsso.SelectedIndex > 0) && (ddlLicenseType.SelectedIndex > 0) && (ddlPlaceName.SelectedIndex > 0) && (ddlTime.SelectedIndex > 0))
                //{
                Manage_App = new List<string>();

                #region Gv2
                divGv2.Visible = false;
                manageApp.Visible = false;
                divAddIn.Visible = false;
                divGetOut.Visible = false;

                #endregion

                #region Gv1
                divGv1.Visible = true;
                #endregion

                ddlEvent.Enabled = false;
                txtNumberGvSearch.Text = "0";
                BindDataInGridView(true);
                gvSearch2.Visible = true;
                // GetRoom();
              
            }
        }



        #region Pageing_milk

        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvSearch":
                    lblTotal.Text = Convert.ToString(total_row_count);
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
                case "gvSearch2":
                    lblTotal2.Text = Convert.ToString(total_row_count);
                    rows_per_page = (rows_per_page == 0 || rows_per_page == null) ? 1 : rows_per_page;
                    double Pagggee = Math.Ceiling(total_row_count / rows_per_page);
                    txtTotalPage2.Text = (total_row_count > 0) ? Convert.ToString(Pagggee) : "0";
                    lblTotal2.Visible = visible_or_disvisible;
                    txtTotalPage2.Visible = visible_or_disvisible;
                    rowPerpage2.Visible = visible_or_disvisible;
                    lblParaPage2.Visible = visible_or_disvisible;
                    pageGo2.Visible = visible_or_disvisible;
                    TXTrowperpage2.Visible = visible_or_disvisible;
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
            
            if (txtNum.Text.ToInt() == MaxP)
            {
                NextName.Visible = false;
            }
            else
            {
                NextName.Visible = true;
            }
            UpdatePanelSearch.Update();
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            BindDataInGridView(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelSearch.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindDataInGridView(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelSearch.Update();
        }
        
        protected void btnPreviousGvSearch2_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch2, txtNumberGvSearch2, btnNextGvSearch2, "P", txtTotalPage2);
            BindDataInGridView2(false);
            Hide_show(btnPreviousGvSearch2, txtNumberGvSearch2, btnNextGvSearch2, "", txtTotalPage2.Text.ToInt());
            UpdatePanelSearch.Update();
        }

        protected void btnNextGvSearch2_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch2, txtNumberGvSearch2, btnNextGvSearch2, "N", txtTotalPage2);
            BindDataInGridView2(false);
            Hide_show(btnPreviousGvSearch2, txtNumberGvSearch2, btnNextGvSearch2, "", txtTotalPage2.Text.ToInt());
            UpdatePanelSearch.Update();
        }
        
        #endregion Pageing_milk


        protected void BindDataInGridView2(Boolean count)
        {
            try
            {
                 PAGE_SIZE = PAGE_SIZE_Key;
                 BLL.ApplicantBiz Abiz = new ApplicantBiz();

                #region page
                int Rpage = (txtNumberGvSearch2.Text.Trim() == "") ? 0 : txtNumberGvSearch2.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch2.Text.Trim().ToInt();

                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage2.Text.Trim() == null) || (rowPerpage2.Text.Trim() == "") || (rowPerpage2.Text.ToInt() == 0))
                {
                    rowPerpage2.Text = PAGE_SIZE.ToString();
                }
                else
                {
                    PAGE_SIZE = Convert.ToInt32(rowPerpage2.Text);
                }
                #endregion page

                string ConSQL = ddlEvent.SelectedValue.ToString()=="in"?"":"out";

                if(count)
                {
                  
                   #region Page
                    var CountPage = Abiz.GetApplicantFromTestingNoForManageApplicant(selectRoom.Text,ConSQL,  resultPage, PAGE_SIZE, true);

                   
                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int32 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvSearch2, totalROWs, Convert.ToInt32(rowPerpage2.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch2, txtNumberGvSearch2, btnNextGvSearch2, "", txtTotalPage2);
                        }
                        else
                        {
                            VisibleGV(gvSearch2, 0, Convert.ToInt32(rowPerpage2.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch2, txtNumberGvSearch2, btnNextGvSearch2, "", txtTotalPage2);
                            txtTotalPage2.Text = "1";
                        }
                    #endregion Page
                }

                var res = Abiz.GetApplicantFromTestingNoForManageApplicant(selectRoom.Text,ConSQL, resultPage, PAGE_SIZE, false);
                if (res.IsError)
                {

                    btnNextGvSearch2.Visible = false;
                    btnPreviousGvSearch2.Visible = false;
                    txtNumberGvSearch2.Visible = false;
                    txtTotalPage2.Visible = false;
                    TXTrowperpage2.Visible = false;
                    lblParaPage2.Visible = false;
                    rowPerpage2.Visible = false;
                    pageGo2.Visible = false;
                    manageApp.Visible = false;
                    divAddIn.Visible = false;
                    divGetOut.Visible = false;
                    div_totalPage2.Visible = false;
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {

                    if (res != null)
                    {
                        if (res.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            btnPreviousGvSearch2.Enabled = true;
                            btnNextGvSearch2.Enabled = true;

                            #region ManageRoom
                            manageApp.Visible = true;
                            if (ddlEvent.SelectedValue.ToString() == "in")
                            {
                                divAddIn.Visible = true;
                                divGetOut.Visible = false;
                                btnAddIn.Visible = true;
                                btnGetOut.Visible = false;
                            }
                            else
                            {
                                divAddIn.Visible = false;
                                divGetOut.Visible = true;
                                btnAddIn.Visible = false;
                                btnGetOut.Visible = true;
                            }
                            #endregion ManageRoom
                        }
                        else
                        {
                            btnPreviousGvSearch2.Enabled = false;
                            btnNextGvSearch2.Enabled = false;
                            divAddIn.Visible = false;
                            divGetOut.Visible = false;
                        }
                        txtNumberGvSearch2.Enabled = false;
                        gvSearch2.DataSource = res.DataResponse;
                        gvSearch2.DataBind();
                        gvSearch2.Visible = true;
                     
                    }
                }
                UpdatePanelSearch.Update();
            }
            catch
            { 
            }

        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            try
            {
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                var TESTING_NO = (Label)gr.FindControl("lblTestingNo");

                ClickView(TESTING_NO.Text);
               
            }
            catch
            {
            }
        }

        private void ClickView(string TESTING_NO)
        {
            try
            {
                Manage_App = null;  
                if (TESTING_NO != "")
                    selectRoom.Text = TESTING_NO;

                txtNumberGvSearch2.Text = "0";
                divGv2.Visible = true;
                if (ddlEvent.SelectedValue == "in")
                    GetRoom();
                BindDataInGridView2(true);
                UpdatePanelSearch.Update();
            }
            catch
            { 
            }
        }

        private void GetRoom()
        {
            try
            {
                string PlaceName="%";
                if ((ddlPlaceName.SelectedValue.ToString() != "เลือก") && (ddlPlaceName.SelectedValue.ToString() != ""))
                    PlaceName = ddlPlaceName.SelectedValue.ToString();

                BLL.ApplicantBiz abiz = new ApplicantBiz();
                var res = abiz.GetExamRoomByTestingNoforManage(selectRoom.Text,PlaceName);
                ddlroom.DataValueField="Id";
                ddlroom.DataTextField = "Name";
                ddlroom.DataSource = res.DataResponse;
                ddlroom.DataBind();

            }
            catch
            { 
            }
        }
              
        protected void hplDelete_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var PaidGroup = (Label)gr.FindControl("lblGroupRequsetNo");
            var biz = new BLL.PaymentBiz();
            var res = biz.CancelGroupRequestNo(PaidGroup.Text);
            if (res.IsError)
            {

                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
                BindDataInGridView(true);
            }
            else
            {
                BindDataInGridView(true);
                UCModalSuccess.ShowMessageSuccess = Resources.errorSysMessage_CreatePaymentSuccess;
                UCModalSuccess.ShowModalSuccess();
   
            }
            UpdatePanelSearch.Update();
        }
       
        private void BindDataInGridView(Boolean Count)
        {
            try
            {
                PAGE_SIZE = PAGE_SIZE_Key;
                string Place = "";
                string Asso = "";
                string DDLTime = "%";
                if(ddlTime.SelectedIndex!=0)
                    DDLTime = ddlTime.SelectedValue.ToString();

                if (ddlPlaceName.SelectedValue.ToString() != "เลือก")
                    Place = ddlPlaceName.SelectedValue.ToString();

                if (ddlAsso.SelectedValue.ToString() != "เลือก")
                    Asso = ddlAsso.SelectedValue.ToString();
                UpdatePanelSearch.Update();
                    
                #region page
                int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();

                resultPage = resultPage == 0 ? 1 : resultPage;
                if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.ToInt() == 0))
                {
                    rowPerpage.Text = PAGE_SIZE.ToString();
                }
                else
                {
                    PAGE_SIZE = Convert.ToInt32(rowPerpage.Text);
                }
                #endregion page

                BLL.ApplicantBiz aBiz = new BLL.ApplicantBiz();
               if (Count)
                {
                    #region Page
                    var CountPage = aBiz.getManageApplicantCourse(ddlLicenseType.SelectedValue,
                                        txtStartExamDate.Text, txtEndExamDate.Text, Asso,
                                        Place, DDLTime,
                                        txtTestNo.Text.Trim(), resultPage, PAGE_SIZE, true);

                   
                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int32 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvSearch, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvSearch, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }



               var res = aBiz.getManageApplicantCourse(ddlLicenseType.SelectedValue, txtStartExamDate.Text, 
                                    txtEndExamDate.Text, Asso, Place,
                                    DDLTime, txtTestNo.Text.Trim(), resultPage, PAGE_SIZE, false);
               


                if (res.IsError)
                {
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    txtNumberGvSearch.Visible = false;
                    txtTotalPage.Visible = false;
                    TXTrowperpage.Visible = false;
                    lblParaPage.Visible = false;
                    rowPerpage.Visible = false;
                    pageGo.Visible = false;
                    div_totalPage.Visible = false;
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();


                }
                else
                {

                    divGv1.Visible = true;
                    gvSearch.Visible = true;
                    div_totalPage.Visible = true;
                    boxResult.Visible = true;

                    if (res != null)
                    {
                        if (res.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            btnPreviousGvSearch.Enabled = true;
                            btnNextGvSearch.Enabled = true;
                        }
                        else
                        {
                            btnPreviousGvSearch.Enabled = false;
                            btnNextGvSearch.Enabled = false;
                        }
                        txtNumberGvSearch.Enabled = false;
                        gvSearch.DataSource = res.DataResponse;
                        gvSearch.DataBind();
                        divGv2.Visible = false;
                    }



                }


               
            }
            catch (Exception ex)
            {

            }
        }

      
        bool b_check = true;
        CheckBox check_all_head;

        protected void gvSearch2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                UpdatePanelSearch.Update();
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    check_all_head = (CheckBox)e.Row.FindControl("checkall");
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label AppCode = (Label)e.Row.FindControl("lblappcode");
                    CheckBox checkselect = (CheckBox)e.Row.FindControl("chkSelect");
                    var l = Manage_App.FirstOrDefault(x => x == AppCode.Text);
                    if (l != null)
                    {
                        checkselect.Checked = true;
                    }
                    else
                    {
                        checkselect.Checked = false;
                        b_check = false;
                    }
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    if (b_check)
                    {
                        check_all_head.Checked = true;
                    }
                   
                }

                if (gvSearch2.Rows.Count > 0)
                    gvSearch2.HeaderRow.FindControl("checkall").Visible=true;
                else
                    gvSearch2.HeaderRow.FindControl("checkall").Visible = false;

              
            }
            catch
            { 
            }
        }

        protected void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            
                CheckBox ckall = (CheckBox)sender;
                if (ckall.Checked)
                {
                    foreach (GridViewRow row in gvSearch2.Rows)
                    {
                        Label AppCode = (Label)row.FindControl("lblappcode");
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (!chkSelect.Checked)
                        {
                            Manage_App.Add(AppCode.Text);
                        }
                        chkSelect.Checked = true;
                    }
                }
                else
                {                   
                    foreach (GridViewRow row in gvSearch2.Rows)
                    {
                        Label AppCode = (Label)row.FindControl("lblappcode");
                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        if (chkSelect.Checked)
                        {
                            Manage_App.Remove(AppCode.Text);
                        }
                        chkSelect.Checked = false;
                    }
                }           
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkrows = (CheckBox)sender;
            GridViewRow rowchek = (GridViewRow)checkrows.Parent.Parent;
            Label AppCode = (Label)rowchek.FindControl("lblappcode");
            if (checkrows.Checked)
            {
                Manage_App.Add(AppCode.Text);
            }
            else
            {
                Manage_App.Remove(AppCode.Text);
                CheckBox ckall = (CheckBox)gvSearch2.HeaderRow.FindControl("Checkall");
                ckall.Checked = false;
            }           
        }

        protected void GetTime(string timeCode)
        {
            try
            {
                BLL.ExamRoomBiz ebiz = new ExamRoomBiz();
                var DTime = ebiz.getExamTimeShow(timeCode);


                ddlTime.DataTextField = "TXTTIME";
                ddlTime.DataValueField = "TIMECODE";
                ddlTime.DataSource = DTime.DataResponse;
                ddlTime.DataBind();
                ddlTime.Items.Insert(0, SysMessage.DefaultSelecting);
                ddlTime.SelectedIndex = 0;
            }
            catch
            {
            }
        }

     
      
        protected void btnMainCancle_Click(object sender, EventArgs e)
        {
            defaultData();
            divGv1.Visible = false;
            divGv2.Visible = false;
            manageApp.Visible = false;
            //divGv3.Visible = false;
        }
        
        protected void defaultData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");

            txtStartExamDate.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            //txtStartExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndExamDate.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
            //txtEndExamDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            divGv1.Visible = false;
            divGv2.Visible = false;
            rowPerpage.Text = PAGE_SIZE_Key.ToString();
            rowPerpage2.Text = PAGE_SIZE_Key.ToString();
         
            GetTime("");
            ddlEvent.Enabled = true;
            ddlEvent.SelectedIndex = 0;
            ddlAsso.Items.Clear();
            ddlPlaceName.Items.Clear();
            txtTestNo.Text = string.Empty;

            if (base.UserRegType.GetEnumValue() != DTO.RegistrationType.Association.GetEnumValue())
                GetLicenseType();
            else
            {
                GetAssociationDefult();
                GetLicenseType();
            }
        }
        
        private void GetLicenseType()
        {
            string AssC = "%%";
            if (base.UserRegType.GetEnumValue() == DTO.RegistrationType.Association.GetEnumValue())
                AssC = base.UserProfile.CompCode;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByAsso(SysMessage.DefaultSelecting,AssC);
            BindToDDL(ddlLicenseType, ls.DataResponse);
        }
        
        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };


        protected void btnAddIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manage_App.ToArray().Count() > 0)
                {
                    BLL.ApplicantBiz abiz = new ApplicantBiz();
                    var res = abiz.SaveExamAppRoom(Manage_App.ToArray(), ddlroom.Text, selectRoom.Text, "", false,base.UserId);
                    if (res.ResultMessage)//save OK
                    {
                        ClickView(selectRoom.Text);
                        UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                        UCModalSuccess.ShowModalSuccess();

                    }
                    else
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    Manage_App = null;
                    UpdatePanelSearch.Update();
                }
                else
                {
                    UCModalError.ShowMessageError = "กรุณาเลือกผู้สมัครสอบก่อนทำรายการ";
                    UCModalError.ShowModalError();
                }
            }
            catch
            { 
            }
        }


        #region New_TOR
        protected void ddlLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlAsso.SelectedValue.ToInt();
                string Asso = "";
                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                    Asso = base.UserProfile.CompCode;
                if (base.UserProfile.MemberType != DTO.RegistrationType.Association.GetEnumValue())
                {
                    if (ddlLicenseType.SelectedIndex > 0)
                    {
                        GetAssociation();
                        try
                        {
                            ddlAsso.SelectedValue = Asso;
                            ddlAsso.Enabled = false;
                        }
                        catch
                        {
                            ddlAsso.SelectedIndex = 0;
                            ddlAsso.Enabled = true;
                        }
                    }
                    else
                    {
                        if (Asso != "")
                        {
                            GetAssociation();
                            try
                            {
                                ddlAsso.SelectedValue = Asso;
                                ddlAsso.Enabled = false;
                            }
                            catch
                            {
                                ddlAsso.SelectedIndex = 0;
                                ddlAsso.Enabled = true;
                            }
                        }
                        else
                        {
                            ddlAsso.Items.Clear();
                        }
                    }
                }
                if (ddlAsso.SelectedIndex > 0)
                    GetExamPlace();
            }
            catch
            { 
            
            }
        }

        private void GetExamPlace()
        {
            try
            {
                BLL.ApplicantBiz appB = new ApplicantBiz();
                var res = appB.GetExamPlaceByLicenseAndOwner(ddlAsso.SelectedValue.ToString() ,  ddlLicenseType.SelectedValue.ToString());
                ddlPlaceName.DataValueField = "ID";
                ddlPlaceName.DataTextField = "NAME";
                ddlPlaceName.DataSource = res.DataResponse;
                ddlPlaceName.DataBind();
                ddlPlaceName.Items.Insert(0, SysMessage.DefaultSelecting);

            }
            catch
            { 
            
            }
        }

        protected void ddlAsso_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((ddlAsso.SelectedIndex > 0) && (ddlLicenseType.SelectedIndex > 0))
                    GetExamPlace();
                else
                    ddlPlaceName.Items.Clear();
            }
            catch
            { 
            
            }
        }
        #endregion New_TOR

        protected void btnGetOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manage_App.ToArray().Count() > 0)
                {
                    BLL.ApplicantBiz abiz = new ApplicantBiz();
                    var res = abiz.CancleExamApplicantManage(Manage_App.ToArray(), selectRoom.Text);
                    if (res.ResultMessage == false)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        ClickView("");
                        UCModalSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                        UCModalSuccess.ShowModalSuccess();

                    }
                    Manage_App = null;
                    UpdatePanelSearch.Update();
                }
                else
                {
                    UCModalError.ShowMessageError = "กรุณาเลือกผู้สมัครสอบก่อนทำรายการ";
                    UCModalError.ShowModalError();
                }
            }
            catch
            { 
            }
        }

        protected void hplGo2_Click(object sender, EventArgs e)
        {
            ClickView(selectRoom.Text);
        }

       

    }
}
