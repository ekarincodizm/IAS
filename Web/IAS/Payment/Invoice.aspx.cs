using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Threading;
using System.Globalization;
using System.Data;
using IAS.BLL;
using IAS.Properties;
using AjaxControlToolkit;
using System.Data.SqlClient;
using System.Configuration;

namespace IAS.Payment
{
    public partial class Invoice : basepage
    {
        #region Public Param & Session
        public int PAGE_SIZE = 20;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        public string maxBefore = string.Empty;
        public List<DTO.OrderInvoice> lsOderInvoice
        {
            get
            {
                if (Session["lsOderInvoice"] == null)
                {
                    Session["lsOderInvoice"] = new List<DTO.OrderInvoice>();
                }

                return (List<DTO.OrderInvoice>)Session["lsOderInvoice"];
            }
            set
            {
                Session["lsOderInvoice"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEndPaidSubDate.Attributes.Add("readonly", "true");
            txtStartPaidSubDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {

                base.HasPermit();
                defaultData();
                GetPaymentType();
                listPayment = new List<DTO.SubGroupPayment>();
                BindTxtCompany();
                lsOderInvoice.Clear();
            
            }
        }
        protected void defaultData()
        {
            Set_textDate(0);
            divGv1.Visible = false;
            divGv2.Visible = false;
            btnExportExcel.Visible = false;
            txtCompany.Text = "";
            btnOk.Visible = false;
        }

        protected void Set_textDate(int Teextbox)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            switch (Teextbox)
            {
                case 1:
                    txtStartPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtStartPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                    break;
                case 2:
                    txtEndPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtEndPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                    break;
                default:
                    txtStartPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtStartPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                    txtEndPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtEndPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                    break;
            }

        }

        public MasterPage.Site1 MasterSite
        {

            get { return (this.Page.Master as MasterPage.Site1); }
        }

        #region testSelect


        //protected void gvPayment_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        //    {
        //        CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelectPayment");
        //        CheckBox chkBxHeader = (CheckBox)this.gvPayment.HeaderRow.FindControl("chkAll");


        //        //chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}','gvPayment');", chkBxHeader.ClientID);
        //    }
        //}

        //protected void gvLicenseNumber_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        //    {
        //        CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelect");
        //        CheckBox chkBxHeader = (CheckBox)this.gvLicenseNumber.HeaderRow.FindControl("chkAll");

        //        // chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}','gvLicenseNumber');", chkBxHeader.ClientID);
        //    }
        //}

        #endregion testSelect


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lsOderInvoice.Clear();
            UpdatePanelSearch.Update();
            DivExam.Visible = false;
            SubLicense.Visible = false;
            if (Convert.ToDateTime(txtStartPaidSubDate.Text) > Convert.ToDateTime(txtEndPaidSubDate.Text))
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError();
                blueDiv.Visible = false;
                btnExportExcel.Visible = false;
            }
            else
            {
                listPayment = new List<DTO.SubGroupPayment>();
                BindDataInGv();
            }
        }
        protected void hdf_ValueChanged(object sender, EventArgs e)
        {
            string selectedWidgetID = ((HiddenField)sender).Value;
            //Widget w = MyEntityService.GetWidget(selectedWidgetID);
            string[] compCode = selectedWidgetID.Split('[', ']');

            txtID.Text = compCode[1];
            //txtNmae.Text = compCode[0];

        }
        //private void GetCompany()
        //{
        //    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
        //    var list = biz.GetCompanyCodeAsCompanyTname("");
        //    string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(list);
        //    hdf.Value = jsonString;
        //}
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (lsOderInvoice.Count != 0)
            {
                remark.Visible = true;
                if (ddlTypePayment.SelectedValue == "01")
                {
                    GVExamPopup.Visible = true;

                    GVExamPopup.DataSource = lsOderInvoice.ToList();
                    GVExamPopup.DataBind();
                }
                else
                {

                    GVPopupLicense.Visible = true;

                    GVPopupLicense.DataSource = lsOderInvoice.ToList();
                    GVPopupLicense.DataBind();

                }
                MPDetail.Show();

            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = "กรุณาเลือกรายการที่ต้องการออกใบสั่งจ่ายย่อย";
                this.MasterSite.ModelError.ShowModalError();
            }


        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void BindTxtCompany()
        {
            if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
            {
                var biz = new BLL.DataCenterBiz();
                var getCompanyName = biz.GetDefaultcompanyName(base.UserProfile.CompCode);
                if (getCompanyName.DataResponse.Tables[0].Rows.Count > 0)
                {
                    DataRow drComName = getCompanyName.DataResponse.Tables[0].Rows[0];
                    txtCompany.Text = drComName["comName"].ToString();
                    txtCompany.Enabled = false;
                }
                txtID.Text = base.UserProfile.CompCode;
            }
        }



        private void GetPaymentType()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetPaymentType(message);
            BindToDDL(ddlTypePayment, ls.DataResponse.ToList());

        }


        private void VisiblePageing()
        {
            if (ddlTypePayment.SelectedValue == "01")
            {

                btnPreviousGvLicenseNumber.Visible = false;
                txtNumberGvLicenseNumber.Visible = false;
                btnNextGvLicenseNumber.Visible = false;


                btnPreviousGvPayment.Visible = true;
                txtNumberGvPayment.Visible = true;
                btnNextGvPayment.Visible = true;
            }
            else
            {
                btnPreviousGvPayment.Visible = false;
                txtNumberGvPayment.Visible = false;
                btnNextGvPayment.Visible = false;


                btnPreviousGvLicenseNumber.Visible = true;
                txtNumberGvLicenseNumber.Visible = true;
                btnNextGvLicenseNumber.Visible = true;
            }
        }


        private void BindDataInGv()
        {
            try
            {
                if (txtStartPaidSubDate.Text == "")
                    Set_textDate(1);
                if (txtEndPaidSubDate.Text == "")
                    Set_textDate(2);

                txtNumberGvPayment.Text = "1";
                txtNumberGvLicenseNumber.Text = "1";
                if (ddlTypePayment.SelectedIndex != 0)
                {
                    if (ddlTypePayment.SelectedValue == "01")
                    {
                
                        divGv1.Visible = true;
                        divGv2.Visible = false;
                        blueDiv.Visible = true;
                        maxBefore = txtInputMaxrow1.Text;
                        if ((txtInputMaxrow1.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow1.Text != "" && txtInputMaxrow1.Text.ToInt() != 0))
                        {

                            txtInputMaxrow1.Text = maxBefore;
                        }
                        else if (txtInputMaxrow1.Text == "" || txtInputMaxrow1.Text.ToInt() == 0)
                        {
                            txtInputMaxrow1.Text = Convert.ToString(PAGE_SIZE);
                        }
                        var resultPage = txtNumberGvPayment.Text.ToInt();
                        PAGE_SIZE = Convert.ToInt32(txtInputMaxrow1.Text);
                        var biz = new BLL.PaymentBiz();
                        if (txtCompany.Text == "")
                        {
                            txtID.Text = "";
                        }
                        var resCount = biz.GetSubGroup(ddlTypePayment.SelectedValue, Convert.ToDateTime(txtStartPaidSubDate.Text),
                                                        Convert.ToDateTime(txtEndPaidSubDate.Text), base.UserProfile, txtID.Text, resultPage, PAGE_SIZE, "Y");

                        DataRow dr = resCount.DataResponse.Tables[0].Rows[0];
                        int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                        double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                        TotalPages = (int)Math.Ceiling(dblPageCount);
                        lblTotalPage1.Text = Convert.ToString(TotalPages);
                        div_total1.Visible = true;
                        var res = biz.GetSubGroup(ddlTypePayment.SelectedValue, Convert.ToDateTime(txtStartPaidSubDate.Text),
                                                 Convert.ToDateTime(txtEndPaidSubDate.Text), base.UserProfile, txtID.Text, resultPage, PAGE_SIZE, "N");
                        DataTable DT = res.DataResponse.Tables[0];

                        gvPayment.Visible = true;

                        //VisiblePageing();
                        blueDiv.Visible = true;
                        gvPayment.DataSource = res.DataResponse;
                        gvPayment.DataBind();
                        btnExportExcel.Visible = true;
                        btnOk.Visible = true;
                        // UpdatePanelSearch.Update();
                        if (lblTotalPage1.Text == "1")
                        {
                            txtNumberGvPayment.Visible = true;

                        }
                        else if (TotalPages > 1)
                        {
                            txtNumberGvPayment.Visible = true;
                            btnNextGvPayment.Visible = true;
                        }


                        lblParaPage1.Visible = true;
                        lblTotalPage1.Visible = true;
                        btngo1.Visible = true;
                        lblTotalrecord1.Text = dr["rowcount"].ToString();
                        txtInputMaxrow1.Visible = true;
                        lblHeadInputMaxrow1.Visible = true;
                        lblHeadTotal1.Visible = true;
                        lblTotalrecord1.Visible = true;
                        lblEndTotal1.Visible = true;
                        divGv1.Visible = true;

                        if (res.DataResponse.Tables[0].Rows.Count <= 0)
                        {
                            btnExportExcel.Visible = false;
                            btnOk.Visible = false;
                            txtNumberGvPayment.Visible = true;
                            txtNumberGvPayment.Text = "1";
                            lblTotalPage1.Text = "1";
                            btnNextGvPayment.Visible = false;
                            CheckBox ChAllP = (CheckBox)gvPayment.HeaderRow.FindControl("Checkall");
                            ChAllP.Visible = false;

                        }
                        else
                        {
                            btnExportExcel.Visible = true;
                            btnOk.Visible = true;
                            txtNumberGvPayment.Visible = true;
                            btnNextGvPayment.Visible = true;
                            CheckBox ChAllP = (CheckBox)gvPayment.HeaderRow.FindControl("Checkall");
                            ChAllP.Visible = true;
                        }
                        if (txtNumberGvPayment.Text == "1")
                        {
                            btnPreviousGvPayment.Visible = false;
                        }
                        if (txtNumberGvPayment.Text == lblTotalPage1.Text)
                        {
                            btnNextGvPayment.Visible = false;
                        }
                        if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() ||
                            base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                        {
                            btnOk.Visible = false;
                            //CheckBox ChAllP = (CheckBox)gvPayment.HeaderRow.FindControl("Checkall");
                            //ChAllP.Visible = false;
                            gvPayment.Columns[0].Visible = false;
                        }

                    }

                    else
                    {
                   
                        divGv2.Visible = true;
                        divGv1.Visible = false;
                        blueDiv.Visible = true;
                        maxBefore = txtInputMaxrow2.Text;
                        if ((txtInputMaxrow2.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow2.Text != "" && txtInputMaxrow2.Text.ToInt() != 0))
                        {

                            txtInputMaxrow2.Text = maxBefore;
                        }
                        else if (txtInputMaxrow2.Text == "" || txtInputMaxrow2.Text.ToInt() == 0)
                        {
                            txtInputMaxrow2.Text = Convert.ToString(PAGE_SIZE);
                        }
                        var resultPage = txtNumberGvLicenseNumber.Text.ToInt();
                        PAGE_SIZE = Convert.ToInt32(txtInputMaxrow2.Text);
                        var biz = new BLL.PaymentBiz();
                        if (txtCompany.Text == "")
                        {
                            txtID.Text = "";
                        }
                        var resCount = biz.GetSubGroup(ddlTypePayment.SelectedValue, Convert.ToDateTime(txtStartPaidSubDate.Text),
                                                        Convert.ToDateTime(txtEndPaidSubDate.Text), base.UserProfile, txtID.Text, resultPage, PAGE_SIZE, "Y");

                        DataRow dr = resCount.DataResponse.Tables[0].Rows[0];

                        int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                        double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                        TotalPages = (int)Math.Ceiling(dblPageCount);
                        lblTotalPage2.Text = Convert.ToString(TotalPages);

                        var res = biz.GetSubGroup(ddlTypePayment.SelectedValue, Convert.ToDateTime(txtStartPaidSubDate.Text),
                                                     Convert.ToDateTime(txtEndPaidSubDate.Text), base.UserProfile, txtID.Text, resultPage, PAGE_SIZE, "N");

                        DataTable DT = res.DataResponse.Tables[0];

                        gvLicenseNumber.Visible = true;

                        //VisiblePageing();
                        blueDiv.Visible = true;
                        gvLicenseNumber.DataSource = res.DataResponse;
                        gvLicenseNumber.DataBind();
                        btnOk.Visible = true;
                        //  UpdatePanelSearch.Update();
                        btnExportExcel.Visible = true;


                        if (lblTotalPage2.Text == "1")
                        {
                            txtNumberGvLicenseNumber.Visible = true;
                        }
                        else if (TotalPages > 1)
                        {
                            txtNumberGvLicenseNumber.Visible = true;
                            btnNextGvLicenseNumber.Visible = true;
                        }


                        lblParaPage2.Visible = true;
                        lblTotalPage2.Visible = true;
                        btngo2.Visible = true;
                        txtInputMaxrow2.Visible = true;
                        lblHeadInputMaxrow2.Visible = true;
                        lblHeadTotal2.Visible = true;
                        lblTotalrecord2.Visible = true;
                        lblEndTotal2.Visible = true;
                        divGv2.Visible = true;
                        lblTotalrecord2.Text = dr["rowcount"].ToString();
                        if (res.DataResponse.Tables[0].Rows.Count <= 0)
                        {
                            btnExportExcel.Visible = false;
                            btnOk.Visible = false;
                            txtNumberGvLicenseNumber.Visible = true;
                            txtNumberGvLicenseNumber.Text = "1";
                            CheckBox ChAllL = (CheckBox)gvLicenseNumber.HeaderRow.FindControl("chkAll");
                            lblTotalPage2.Text = "1";
                            ChAllL.Visible = false;
                            btnNextGvLicenseNumber.Visible = false;
                        }
                        else
                        {
                            btnExportExcel.Visible = true;
                            btnOk.Visible = true;
                            txtNumberGvLicenseNumber.Visible = true;
                            CheckBox ChAllL = (CheckBox)gvLicenseNumber.HeaderRow.FindControl("chkAll");
                            ChAllL.Visible = true;
                        }


                        if (txtNumberGvLicenseNumber.Text == "1")
                        {
                            btnPreviousGvLicenseNumber.Visible = false;
                        }
                        if (txtNumberGvLicenseNumber.Text == lblTotalPage2.Text)
                        {
                            btnNextGvLicenseNumber.Visible = false;
                        }
                        if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()
                            || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                        {
                            btnOk.Visible = false;
                            gvLicenseNumber.Columns[0].Visible = false;
                        }
                    }

                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorInvoice_002;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            catch
            {

            }
           
        }

        protected void btnPreviousGvPayment_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvPayment.Text.ToInt() - 1;

            txtNumberGvPayment.Text = result == 0 ? "1" : result.ToString();
            txtNumberGvPayment.Visible = true;
            lblParaPage1.Visible = true;
            lblTotalPage1.Visible = true;
            // btnNextGvPayment.Visible = true;
            btngo1.Visible = true;
            txtInputMaxrow1.Visible = true;
            lblHeadInputMaxrow1.Visible = true;
            lblHeadTotal1.Visible = true;
            btnPreviousGvPayment.Visible = result.ToString() == "1" ? false : true;
            btnNextGvPayment.Visible = txtNumberGvPayment.Text == lblTotalPage1.Text ? false : true;
            lblTotalrecord1.Visible = true;
            lblEndTotal1.Visible = true;


            BindPage();


        }

        protected void btnNextGvPayment_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvPayment.Text.ToInt() + 1;

            txtNumberGvPayment.Text = Convert.ToInt32(result) < Convert.ToInt32(lblTotalPage1.Text) ? result.ToString() : lblTotalPage1.Text;
            txtNumberGvPayment.Visible = true;
            lblParaPage1.Visible = true;
            lblTotalPage1.Visible = true;
            btnNextGvPayment.Visible = txtNumberGvPayment.Text == lblTotalPage1.Text ? false : true;
            btnPreviousGvPayment.Visible = txtNumberGvPayment.Text == "1" ? false : true; ;
            btngo1.Visible = true;
            txtInputMaxrow1.Visible = true;
            lblHeadInputMaxrow1.Visible = true;
            lblHeadTotal1.Visible = true;
            lblTotalrecord1.Visible = true;
            lblEndTotal1.Visible = true;

            BindPage();
        }

        protected void btnPreviousGvLicenseNumber_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvLicenseNumber.Text.ToInt() - 1;

            txtNumberGvLicenseNumber.Text = result == 0 ? "1" : result.ToString();

            txtNumberGvLicenseNumber.Visible = true;
            lblParaPage2.Visible = true;
            lblTotalPage2.Visible = true;
            btnNextGvLicenseNumber.Visible = txtNumberGvLicenseNumber.Text == lblTotalPage2.Text ? false : true;
            btnPreviousGvLicenseNumber.Visible = result.ToString() == "1" ? false : true;

            btngo2.Visible = true;
            txtInputMaxrow2.Visible = true;
            lblHeadInputMaxrow2.Visible = true;
            lblHeadTotal2.Visible = true;
            lblTotalrecord2.Visible = true;
            lblEndTotal2.Visible = true;

            BindPage();


        }

        protected void btnNextGvLicenseNumber_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvLicenseNumber.Text.ToInt() + 1;

            txtNumberGvLicenseNumber.Text = Convert.ToInt32(result) < Convert.ToInt32(lblTotalPage2.Text) ? result.ToString() : lblTotalPage2.Text;
            txtNumberGvLicenseNumber.Visible = true;
            lblParaPage2.Visible = true;
            lblTotalPage2.Visible = true;
            btnNextGvLicenseNumber.Visible = txtNumberGvLicenseNumber.Text == lblTotalPage2.Text ? false : true;
            btnPreviousGvLicenseNumber.Visible = txtNumberGvLicenseNumber.Text == "1" ? false : true;


            BindPage();
        }
        protected void BindPage()
        {
            if (ddlTypePayment.SelectedIndex != 0)
            {
                var biz = new BLL.PaymentBiz();
                if (ddlTypePayment.SelectedValue == "01")
                {
                    PAGE_SIZE = Convert.ToInt32(txtInputMaxrow1.Text);
                    var resultPage = txtNumberGvPayment.Text.ToInt();

                    var res = biz.GetSubGroup(ddlTypePayment.SelectedValue,
                 Convert.ToDateTime(txtStartPaidSubDate.Text),
                         Convert.ToDateTime(txtEndPaidSubDate.Text),
               base.UserProfile, txtID.Text, resultPage, PAGE_SIZE, "N");

                    gvPayment.Visible = true;
                    gvPayment.DataSource = res.DataResponse;
                    gvPayment.DataBind();


                }
                else
                {
                    PAGE_SIZE = Convert.ToInt32(txtInputMaxrow2.Text);
                    var resultPage = txtNumberGvLicenseNumber.Text.ToInt();
                    var res = biz.GetSubGroup(ddlTypePayment.SelectedValue,
              Convert.ToDateTime(txtStartPaidSubDate.Text),
              Convert.ToDateTime(txtEndPaidSubDate.Text),
               base.UserProfile, txtID.Text, resultPage, PAGE_SIZE, "N");

                    gvLicenseNumber.Visible = true;
                    gvLicenseNumber.DataSource = res.DataResponse;
                    gvLicenseNumber.DataBind();

                }
            }

        }
        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartPaidSubDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndPaidSubDate.Text = string.Empty;
        }



        //เริ่มเขียน gvPayment
        private List<DTO.SubGroupPayment> listPayment
        {
            get
            {
                return Session["payment"] == null
                              ? new List<DTO.SubGroupPayment>()
                              : (List<DTO.SubGroupPayment>)Session["payment"];
            }
            set
            {
                Session["payment"] = value;
            }
        }

        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataCenterBiz biz = new DataCenterBiz();
       
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // var lblApplicantCodeGv = (Label)e.Row.FindControl("lblApplicantCodeGv");
                var lblTestingNoGv = (Label)e.Row.FindControl("lblTestingNoGv");
                var lblExamPlaceCodeGv = (Label)e.Row.FindControl("lblExamPlaceCodeGv");
                CheckBox chkSelectPayment = (CheckBox)e.Row.FindControl("chkSelectPayment");
                var lblRUN_NO = (Label)e.Row.FindControl("lblRunNo");
                var lblUploadGroupNo = (Label)e.Row.FindControl("lblUploadGroupNo");
                var lblUPLOAD_BY_SESSION = (Label)e.Row.FindControl("lblUPLOAD_BY_SESSION");
                var lblPETITION_TYPE_NAME = (Label)e.Row.FindControl("lblPaymentTypeNameGv");
                var lblPerson = (Label)e.Row.FindControl("lblPerson");
                var lblTestingDateGv = (Label)e.Row.FindControl("lblTestingDateGv");
              
                if (lblUPLOAD_BY_SESSION.Text.Length == 3)
                {
                    var ls = biz.GetAssociation(lblUPLOAD_BY_SESSION.Text);
                    DataTable dt = ls.DataResponse.Tables[0];
                    DataRow dr = dt.Rows[0];
                    lblUPLOAD_BY_SESSION.Text = dr["ASSOCIATION_NAME"].ToString();
                }
                else if (lblUPLOAD_BY_SESSION.Text.Length == 4)
                {
                    var ls = biz.GetCompanyCodeById(lblUPLOAD_BY_SESSION.Text);
                    lblUPLOAD_BY_SESSION.Text = ls.Name;
                }
                else
                {
                    lblUPLOAD_BY_SESSION.Text = "";
                }
                //if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.ToInt())
                //{
                //    chkSelectPayment.Visible = false;
                //}
                var list = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);
                if (list != null)
                {
                    chkSelectPayment.Checked = true;
                }
                else
                {
                    chkSelectPayment.Checked = false;
                }
            }
        }

        protected void chkSelectPayment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkSelectPayment = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)checkSelectPayment.Parent.Parent;
          //  var lblApplicantCodeGv = (Label)gr.FindControl("lblApplicantCodeGv");
            var lblTestingNoGv = (Label)gr.FindControl("lblTestingNoGv");
            var lblExamPlaceCodeGv = (Label)gr.FindControl("lblExamPlaceCodeGv");
                    var lblRUN_NO = (Label)gr.FindControl("lblRunNo");
                var lblUploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
                var lblUPLOAD_BY_SESSION = (Label)gr.FindControl("lblUPLOAD_BY_SESSION");
                var lblPaymentTypeNameGv = (Label)gr.FindControl("lblPaymentTypeNameGv");
                var lblPerson = (Label)gr.FindControl("lblPerson");
                var lblTestingDateGv = (Label)gr.FindControl("lblTestingDateGv");
            if (checkSelectPayment.Checked)
            {
              lsOderInvoice.Add(new DTO.OrderInvoice
                        {
                            PaymentType = ddlTypePayment.SelectedValue,
                           // ApplicantCode = lblApplicantCodeGv.Text == ""? 0:Convert.ToInt32(lblApplicantCodeGv.Text),
                            TESTING_NO = lblTestingNoGv.Text,
                            EXAM_PLACE_CODE = lblExamPlaceCodeGv.Text,
                            RUN_NO = Convert.ToString(lsOderInvoice.Count + 1),
                            UPLOAD_BY_SESSION = lblUPLOAD_BY_SESSION.Text,
                            UPLOAD_GROUP_NO = lblUploadGroupNo.Text,
                            CountPerson = lblPerson.Text,
                            testing_date = lblTestingDateGv.Text,
                            PETITION_TYPE_NAME = lblPaymentTypeNameGv.Text

                        });
            }
            else
            {
                var pament   = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);
                ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("Checkall")).Checked = false;
                lsOderInvoice.Remove(pament);
            }

        }

        protected void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = (CheckBox)sender;
            foreach (GridViewRow row in gvPayment.Rows)
            {
              //  var lblApplicantCodeGv = (Label)row.FindControl("lblApplicantCodeGv");
                var lblTestingNoGv = (Label)row.FindControl("lblTestingNoGv");
                var lblExamPlaceCodeGv = (Label)row.FindControl("lblExamPlaceCodeGv");
                var lblRUN_NO = (Label)row.FindControl("lblRunNo");
                var lblUploadGroupNo = (Label)row.FindControl("lblUploadGroupNo");
                var lblUPLOAD_BY_SESSION = (Label)row.FindControl("lblUPLOAD_BY_SESSION");
                var lblPETITION_TYPE_NAME = (Label)row.FindControl("lblPaymentTypeNameGv");
                var lblPerson = (Label)row.FindControl("lblPerson");
                var lblTestingDateGv = (Label)row.FindControl("lblTestingDateGv");

                CheckBox chkSelectPayment = (CheckBox)row.FindControl("chkSelectPayment");

                if (ck.Checked)
                {
                    if (!chkSelectPayment.Checked)
                    {
                        lsOderInvoice.Add(new DTO.OrderInvoice
                        {
                            PaymentType = ddlTypePayment.SelectedValue,
                           // ApplicantCode = lblApplicantCodeGv.Text == "" ? 0 : Convert.ToInt32(lblApplicantCodeGv.Text),
                            TESTING_NO = lblTestingNoGv.Text,
                            EXAM_PLACE_CODE = lblExamPlaceCodeGv.Text,
                            RUN_NO = Convert.ToString(lsOderInvoice.Count + 1),
                            UPLOAD_BY_SESSION = lblUPLOAD_BY_SESSION.Text,
                            UPLOAD_GROUP_NO = lblUploadGroupNo.Text,
                            CountPerson = lblPerson.Text,
                            testing_date = lblTestingDateGv.Text,
                            PETITION_TYPE_NAME = lblPETITION_TYPE_NAME.Text

                        });
                    }
                    chkSelectPayment.Checked = true;
                }
                else
                {
                    if (chkSelectPayment.Checked)
                    {
                        var list = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);
                        lsOderInvoice.Remove(list);
                    }
                    chkSelectPayment.Checked = false;
                }
            }
        }

        protected void hplViewExam_Click(object sender, EventArgs e)
        {
            GvSubExam.Visible = true;
            if (txtInputMaxrow3.Text == "")
            {
                txtInputMaxrow3.Text = Convert.ToString(PAGE_SIZE_Key);//milk
                PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
                txtNumberGvSubExam.Text = "1";
            }
            else
            {
                PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
                txtNumberGvSubExam.Text = "1";
            }
            // divGv3.Visible = false;
            var resultPage = txtNumberGvSubExam.Text.ToInt();
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var UploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
            UploadgroupNoPage.Value = UploadGroupNo.Text;
            var biz = new BLL.PaymentBiz();
            var resCount = biz.GetSubGroupDetail(ddlTypePayment.SelectedValue, UploadGroupNo.Text, resultPage, PAGE_SIZE, "Y");
            DataRow dr = resCount.DataResponse.Tables[0].Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            lblTotalPage3.Text = Convert.ToString(TotalPages);
            div_total1.Visible = true;
            var res = biz.GetSubGroupDetail(ddlTypePayment.SelectedValue, UploadGroupNo.Text, resultPage, PAGE_SIZE, "N");
            DataTable DT = res.DataResponse.Tables[0];

            gvPayment.Visible = true;

            //VisiblePageing();
            blueDiv.Visible = true;
            DivExam.Visible = true;
            SubLicense.Visible = false;
            GvSubExam.DataSource = res.DataResponse;
            GvSubExam.DataBind();
            //btnExportExcel.Visible = true;
            btnOk.Visible = true;
            // UpdatePanelSearch.Update();
            if (lblTotalPage3.Text == "1")
            {
                txtNumberGvSubExam.Visible = true;
                
            }
            else if (TotalPages > 1)
            {
                txtNumberGvSubExam.Visible = true;
                btnNextGvSubExam3.Visible = true;
                btnPreviousGvSubExam.Visible = false;
            }
            else if (txtNumberGvSubExam.Text == lblTotalPage3.Text)
            {
                txtNumberGvSubExam.Visible = true;
                btnNextGvSubExam3.Visible = false;
            }
         

            lblParaPage3.Visible = true;
            lblTotalPage3.Visible = true;
            btngo3.Visible = true;
            lblTotalrecord3.Text = dr["rowcount"].ToString();
            txtInputMaxrow3.Visible = true;
            lblHeadInputMaxrow3.Visible = true;
            lblHeadTotal3.Visible = true;
            lblTotalrecord3.Visible = true;
            lblEndTotal3.Visible = true;
            divGv1.Visible = true;

      

        }
        #region SubExam
        protected void btngo3_Click(object sender, EventArgs e)
        {
            BindGvSub();
        }
        protected void btnPreviousGvSubExam_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSubExam.Text.ToInt() - 1;

            txtNumberGvSubExam.Text = result == 0 ? "1" : result.ToString();
            txtNumberGvSubExam.Visible = true;
            lblParaPage3.Visible = true;
            lblTotalPage3.Visible = true;
            // btnNextGvPayment.Visible = true;
            btngo3.Visible = true;
            txtInputMaxrow3.Visible = true;
            lblHeadInputMaxrow3.Visible = true;
            lblHeadTotal3.Visible = true;
            btnPreviousGvSubExam.Visible = result.ToString() == "1" ? false : true;
            btnNextGvSubExam3.Visible = txtNumberGvSubExam.Text == lblTotalPage3.Text ? false : true;
            lblTotalrecord3.Visible = true;
            lblEndTotal3.Visible = true;
            BindGvSub();
        }
        protected void btnNextGvSubExam3_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSubExam.Text.ToInt() + 1;

            txtNumberGvSubExam.Text = Convert.ToInt32(result) < Convert.ToInt32(lblTotalPage3.Text) ? result.ToString() : lblTotalPage3.Text;
            txtNumberGvSubExam.Visible = true;
            lblParaPage3.Visible = true;
            lblTotalPage3.Visible = true;
            btnNextGvSubLicense.Visible = txtNumberGvSubExam.Text == lblTotalPage3.Text ? false : true;
            btnPreviousGvSubLicense.Visible = txtNumberGvSubExam.Text == "1" ? false : true; ;
            btngo3.Visible = true;
            txtInputMaxrow3.Visible = true;
            lblHeadInputMaxrow3.Visible = true;
            lblHeadTotal3.Visible = true;
            lblTotalrecord3.Visible = true;
            lblEndTotal3.Visible = true;
            BindGvSub();
        }

        #endregion
        protected void BindGvSub()
        {

            if (ddlTypePayment.SelectedIndex != 0)
            {
                var biz = new BLL.PaymentBiz();
                if (ddlTypePayment.SelectedValue == "01")
                {
                 
           
                    if (txtInputMaxrow3.Text == "" || Convert.ToInt32(txtInputMaxrow3.Text) == 0)
                    {
                        txtInputMaxrow3.Text = Convert.ToString(PAGE_SIZE_Key);//milk
                    }
              
                    PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);

                    // divGv3.Visible = false;
                    var resultPage = txtNumberGvSubExam.Text.ToInt();

                    var resCount = biz.GetSubGroupDetail(ddlTypePayment.SelectedValue, UploadgroupNoPage.Value, resultPage, PAGE_SIZE, "Y");
                    DataRow dr = resCount.DataResponse.Tables[0].Rows[0];
                    int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                    double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                    TotalPages = (int)Math.Ceiling(dblPageCount);
                    lblTotalPage3.Text = Convert.ToString(TotalPages);
                    div_total1.Visible = true;
                    var res = biz.GetSubGroupDetail(ddlTypePayment.SelectedValue, UploadgroupNoPage.Value, resultPage, PAGE_SIZE, "N");
                    DataTable DT = res.DataResponse.Tables[0];

                    gvPayment.Visible = true;
                    lblTotalrecord3.Text = Convert.ToString(rowcount);
                    //VisiblePageing();
                    blueDiv.Visible = true;
                    DivExam.Visible = true;
                    SubLicense.Visible = false;
                    GvSubExam.DataSource = res.DataResponse;
                    GvSubExam.DataBind();
                    //btnExportExcel.Visible = true;
                    lblParaPage3.Visible = true;
                    lblTotalPage3.Visible = true;
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    btnOk.Visible = true;
                    // UpdatePanelSearch.Update();
                    if (lblTotalPage3.Text == "1")
                    {
                        txtNumberGvSubExam.Visible = true;

                    }
                    else if ((TotalPages > 1) && (txtNumberGvSubExam.Text != lblTotalPage3.Text) && (txtNumberGvSubExam.Text == "1"))
                    {
                        txtNumberGvSubExam.Visible = true;
                        btnNextGvSubExam3.Visible = true;
                        btnPreviousGvSubExam.Visible = false;
                    }
                    else if ((TotalPages > 1) && (txtNumberGvSubExam.Text != lblTotalPage3.Text) && (txtNumberGvSubExam.Text != "1"))
                    {
                        txtNumberGvSubExam.Visible = true;
                        btnNextGvSubExam3.Visible = true;
                        btnPreviousGvSubExam.Visible = true;
                    }
                    else if (txtNumberGvSubExam.Text == lblTotalPage3.Text)
                    {
                        txtNumberGvSubExam.Visible = true;
                        btnNextGvSubExam3.Visible = false;
                        btnPreviousGvSubExam.Visible = true;
                    }
                }
                else
                {
                    gvSubLicenseNumber.Visible = true;
                    if (txtInputMaxrow4.Text == "" || Convert.ToInt32(txtInputMaxrow4.Text) == 0)
                    {
                        txtInputMaxrow4.Text = Convert.ToString(PAGE_SIZE_Key);//milk
                    }
                    PAGE_SIZE = Convert.ToInt32(txtInputMaxrow4.Text);

                    // divGv3.Visible = false;
                    var resultPage = txtNumberGvSublicense.Text.ToInt();


                    var resCount = biz.GetSubGroupDetail(ddlTypePayment.SelectedValue, UploadgroupNoPage.Value, resultPage, PAGE_SIZE, "Y");
                    DataRow dr = resCount.DataResponse.Tables[0].Rows[0];
                    int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                    double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                    TotalPages = (int)Math.Ceiling(dblPageCount);
                    lblTotalPage4.Text = Convert.ToString(TotalPages);
                    div_total1.Visible = true;
                    var res = biz.GetSubGroupDetail(ddlTypePayment.SelectedValue, UploadgroupNoPage.Value, resultPage, PAGE_SIZE, "N");
                    DataTable DT = res.DataResponse.Tables[0];
                    lblTotalrecord4.Text = Convert.ToString(rowcount);
                    gvLicenseNumber.Visible = true;

                    //VisiblePageing();
                    blueDiv.Visible = true;
                    DivExam.Visible = false;
                    SubLicense.Visible = true;
                    gvSubLicenseNumber.DataSource = res.DataResponse;
                    gvSubLicenseNumber.DataBind();
                    //btnExportExcel.Visible = true;
                    lblParaPage4.Visible = true;
                    lblTotalPage4.Visible = true;
                    btngo4.Visible = true;
                    txtInputMaxrow4.Visible = true;
                    lblHeadInputMaxrow4.Visible = true;
                    lblHeadTotal4.Visible = true;
                    lblTotalrecord4.Visible = true;
                    lblEndTotal4.Visible = true;
                    btnOk.Visible = true;
                    // UpdatePanelSearch.Update();
                    if (lblTotalPage4.Text == "1")
                    {
                        txtNumberGvSublicense.Visible = true;

                    }
                    else if ((TotalPages > 1) && (txtNumberGvSublicense.Text != lblTotalPage4.Text) && (txtNumberGvSublicense.Text == "1"))
                    {
                        txtNumberGvSublicense.Visible = true;
                        btnNextGvSubLicense.Visible = true;
                        btnPreviousGvSubLicense.Visible = false;
                    }
                    else if ((TotalPages > 1) && (txtNumberGvSublicense.Text != lblTotalPage4.Text) && (txtNumberGvSublicense.Text != "1"))
                    {
                        txtNumberGvSublicense.Visible = true;
                        btnNextGvSubLicense.Visible = true;
                        btnPreviousGvSubLicense.Visible = true;
                    }
                    else if (txtNumberGvSublicense.Text == lblTotalPage4.Text)
                    {
                        txtNumberGvSublicense.Visible = true;
                        btnNextGvSubLicense.Visible = false;
                    }


                }
            }
        }
        //License
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            foreach (GridViewRow gr in gvLicenseNumber.Rows)
            {
                CheckBox checkselect = (CheckBox)gr.FindControl("chkSelect");
                var lblRunNo = (Label)gr.FindControl("lblRunNo");
                var lblUploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
                var lblUPLOAD_BY_SESSION = (Label)gr.FindControl("lblUPLOAD_BY_SESSION");

                var lblPetitionType = (Label)gr.FindControl("lblPetitionType");
                var lblLicenseType = (Label)gr.FindControl("lblLicenseType");
                var lblLots = (Label)gr.FindControl("lblLots");


                if (ch.Checked)
                {

                    if (!checkselect.Checked)
                    {
                        lsOderInvoice.Add(new DTO.OrderInvoice
                        {
                            RUN_NO = Convert.ToString(lsOderInvoice.Count + 1),
                            UPLOAD_GROUP_NO = lblUploadGroupNo.Text,
                            UPLOAD_BY_SESSION = lblUPLOAD_BY_SESSION.Text,
                            PaymentType = ddlTypePayment.SelectedValue,
                            PETITION_TYPE_NAME = lblPetitionType.Text,
                            LICENSE_TYPE_NAME = lblLicenseType.Text,
                            LOTS = lblLots.Text
                        });
                    }
                    checkselect.Checked = true;
                }
                else
                {

                    var pament = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);
                    lsOderInvoice.Remove(pament);

                    checkselect.Checked = false;
                }
            }
            UpdatePanelSearch.Update();

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chkSelect.Parent.Parent;
            //CheckBox checkselect = (CheckBox)gr.FindControl("chkSelect");
            var lblRunNo = (Label)gr.FindControl("lblRunNo");
            var lblUploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
            var lblUPLOAD_BY_SESSION = (Label)gr.FindControl("lblUPLOAD_BY_SESSION");
            var lblPetitionType = (Label)gr.FindControl("lblPetitionType");
            var lblLicenseType = (Label)gr.FindControl("lblLicenseType");
            var lblLots = (Label)gr.FindControl("lblLots");



            if (chkSelect.Checked)
            {
                lsOderInvoice.Add(new DTO.OrderInvoice
                {
                    RUN_NO = Convert.ToString(lsOderInvoice.Count + 1),
                    UPLOAD_GROUP_NO = lblUploadGroupNo.Text,
                    UPLOAD_BY_SESSION = lblUPLOAD_BY_SESSION.Text,
                    PaymentType = ddlTypePayment.SelectedValue,
                    PETITION_TYPE_NAME = lblPetitionType.Text,
                    LICENSE_TYPE_NAME = lblLicenseType.Text,
                    LOTS = lblLots.Text
                });
            }
            else
            {

                var pament = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);
                ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("chkAll")).Checked = false;
                lsOderInvoice.Remove(pament);
            }

            UpdatePanelSearch.Update();

        }

        protected void gvLicenseNumber_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataCenterBiz biz = new DataCenterBiz();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblUploadGroupNo = (Label)e.Row.FindControl("lblUploadGroupNo");
                var lblUPLOAD_BY_SESSION = (Label)e.Row.FindControl("lblUPLOAD_BY_SESSION");
                if (lblUPLOAD_BY_SESSION.Text.Length == 3)
                {
                    var ls = biz.GetAssociation(lblUPLOAD_BY_SESSION.Text);
                    DataTable dt = ls.DataResponse.Tables[0];
                    DataRow dr = dt.Rows[0];
                    lblUPLOAD_BY_SESSION.Text = dr["ASSOCIATION_NAME"].ToString();
                }
                else  if (lblUPLOAD_BY_SESSION.Text.Length == 4)
                {
                    var ls = biz.GetCompanyCodeById(lblUPLOAD_BY_SESSION.Text);
                    lblUPLOAD_BY_SESSION.Text = ls.Name;
                }
                else
                {
                    lblUPLOAD_BY_SESSION.Text = "";
                }
                CheckBox checkselect = (CheckBox)e.Row.FindControl("chkSelect");
                var list = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);

                if (list != null)
                {
                    checkselect.Checked = true;
                }
                else
                {
                    checkselect.Checked = false;
                }
            }
        }

        protected void hplViewLicense_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var UploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
            UploadgroupNoPage.Value = UploadGroupNo.Text;
            BindGvSub();
        }
        #region SubLicense
        protected void btngo4_Click(object sender, EventArgs e)
        {
            BindGvSub();
        }
        protected void btnPreviousGvSubLicense_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSublicense.Text.ToInt() - 1;

            txtNumberGvSublicense.Text = result == 0 ? "1" : result.ToString();
            txtNumberGvSublicense.Visible = true;
            lblParaPage4.Visible = true;
            lblTotalPage4.Visible = true;
            // btnNextGvPayment.Visible = true;
            btngo4.Visible = true;
            txtInputMaxrow4.Visible = true;
            lblHeadInputMaxrow4.Visible = true;
            lblHeadTotal4.Visible = true;
            btnPreviousGvSubLicense.Visible = result.ToString() == "1" ? false : true;
            btnNextGvSubLicense.Visible = txtNumberGvSublicense.Text == lblTotalPage4.Text ? false : true;
            lblTotalrecord4.Visible = true;
            lblEndTotal4.Visible = true;
            BindGvSub();
        }
        protected void btnNextGvSubLicense_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSublicense.Text.ToInt() + 1;

            txtNumberGvSublicense.Text = Convert.ToInt32(result) < Convert.ToInt32(lblTotalPage4.Text) ? result.ToString() : lblTotalPage4.Text;
            txtNumberGvSublicense.Visible = true;
            lblParaPage4.Visible = true;
            lblTotalPage4.Visible = true;
            btnNextGvSubLicense.Visible = txtNumberGvSublicense.Text == lblTotalPage4.Text ? false : true;
            btnPreviousGvSubLicense.Visible = txtNumberGvSublicense.Text == "1" ? false : true; ;
            btngo4.Visible = true;
            txtInputMaxrow4.Visible = true;
            lblHeadInputMaxrow4.Visible = true;
            lblHeadTotal4.Visible = true;
            lblTotalrecord4.Visible = true;
            lblEndTotal4.Visible = true;
            BindGvSub();
        }
        #endregion
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            defaultData();
            ddlTypePayment.SelectedIndex = 0;
            blueDiv.Visible = false;
            BindTxtCompany();
        }
        #region popup
        protected void GVPopupLicense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblUploadGroupNo = (Label)e.Row.FindControl("lblUploadGroupNo");
                var LBUP = (LinkButton)e.Row.FindControl("LBUP");
                var LBDown = (LinkButton)e.Row.FindControl("LBDown");
                var list = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUploadGroupNo.Text);
                if (list != null)
                {
                    if (lsOderInvoice.Count == 1)
                    {
                        LBDown.Visible = false;
                        LBUP.Visible = false;
                    }
                    if (lsOderInvoice.IndexOf(list) == 0)
                    {
                        LBUP.Visible = false;
                    }
                    else if (lsOderInvoice.IndexOf(list) == lsOderInvoice.Count - 1)
                    {
                        LBDown.Visible = false;
                    }

                }

            }
            UpdatePanelSearch.Update();
        }

        protected void GVPopupLicense__RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            List<DTO.OrderInvoice> newls = new Class.InvoiceSortDescriptions().PaymentSortDesc(this.lsOderInvoice, CurrentIndex, e.CommandName);

            //Rebind
            this.GVPopupLicense.DataSource = newls;
            this.GVPopupLicense.DataBind();
            MPDetail.Show();
            UpdatePanelSearch.Update();
        }

        protected void GVExamPopup__RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            List<DTO.OrderInvoice> newls = new Class.InvoiceSortDescriptions().PaymentSortDesc(this.lsOderInvoice, CurrentIndex, e.CommandName);

            //Rebind
            this.GVExamPopup.DataSource = newls;
            this.GVExamPopup.DataBind();
            MPDetail.Show();
            UpdatePanelSearch.Update();
        }
        //protected void LBUP_Click(object sender, EventArgs e)
        //{
        //    var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

        //    var UploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
        //    var Upload = lsOderInvoice.SingleOrDefault(a => a.UPLOAD_GROUP_NO == UploadGroupNo.Text);

        //    Upload.IndexOfGroup = lsOderInvoice.IndexOf(Upload);

        //    lsOderInvoice.RemoveAt(Upload.IndexOfGroup);
        //    lsOderInvoice.Insert(Upload.IndexOfGroup - 1, Upload);
        //    // Upload.RUN_NO = Convert.ToString(lsOderInvoice.IndexOf(Upload) - 1);
        //    foreach (DTO.OrderInvoice item in lsOderInvoice)
        //    {
        //        var AllOrder = lsOderInvoice.SingleOrDefault(a => a.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO);
        //        AllOrder.RUN_NO = Convert.ToString(lsOderInvoice.IndexOf(AllOrder) + 1);
        //    }
        //    GVPopupLicense.DataSource = lsOderInvoice.ToList().OrderBy(x => x.RUN_NO);
        //    GVPopupLicense.DataBind();
        //    MPDetail.Show();
        //}

        //protected void LBDown_Click(object sender, EventArgs e)
        //{
        //    var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

        //    var UploadGroupNo = (Label)gr.FindControl("lblUploadGroupNo");
        //    var Upload = lsOderInvoice.SingleOrDefault(a => a.UPLOAD_GROUP_NO == UploadGroupNo.Text);
        //    //ChangeIndex(lsOderInvoice.ToList(), Upload, false);
        //    Upload.IndexOfGroup = lsOderInvoice.IndexOf(Upload);

        //    lsOderInvoice.RemoveAt(Upload.IndexOfGroup);
        //    lsOderInvoice.Insert(Upload.IndexOfGroup + 1, Upload);
        //    // Upload.RUN_NO = Convert.ToString(lsOderInvoice.IndexOf(Upload) + 1);
        //    foreach (DTO.OrderInvoice item in lsOderInvoice)
        //    {
        //        var AllOrder = lsOderInvoice.SingleOrDefault(a => a.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO);
        //        AllOrder.RUN_NO = Convert.ToString(lsOderInvoice.IndexOf(AllOrder) + 1);
        //    }
        //    GVPopupLicense.DataSource = lsOderInvoice.ToList().OrderBy(x => x.RUN_NO);
        //    GVPopupLicense.DataBind();
        //    MPDetail.Show();
        //}
        #endregion

        protected void btnSavePopup_Click(object sender, EventArgs e)
        {
            if (lsOderInvoice.Count != 0)
            {
                
                var biz = new BLL.PaymentBiz();
                var res = biz.SetSubGroup(lsOderInvoice.ToArray(), base.UserId, base.UserProfile.CompCode, base.UserProfile.MemberType.ToString());
               
                if (res.IsError)
                {

                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();

                }
                else
                {
                    
                   // listPayment = new List<DTO.SubGroupPayment>();
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.infoInvoice_001;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();
                    lsOderInvoice.Clear();
                    MPDetail.Hide();
                    BindDataInGv();
                }
               
                UpdatePanelSearch.Update();
            }
            
        }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                ExportBiz export = new ExportBiz();
                UpdatePanelSearch.Update();
                if (gvPayment.Visible)
                {

                    int total = lblTotalrecord1.Text == "" ? 0 : lblTotalrecord1.Text.ToInt();
                    if (total > base.EXCEL_SIZE_Key)
                    {
                        this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                    else
                    {
                        Dictionary<string, string> columns = new Dictionary<string, string>();
                        columns.Add("ลำดับ", "RUN_NO");
                        columns.Add("ใบสั่งจ่าย", "PAYMENT_TYPE_NAME");
                        columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                        columns.Add("ชื่อ", "FIRST_NAME");
                        columns.Add("นามสกุล", "LASTNAME");
                        columns.Add("วันที่สอบ", "TESTING_DATE");
                        columns.Add("รหัสสอบ", "TESTING_NO");

                        var biz = new BLL.PaymentBiz();
                        var res = biz.GetSubGroup(ddlTypePayment.SelectedValue, Convert.ToDateTime(txtStartPaidSubDate.Text),
                                                         Convert.ToDateTime(txtEndPaidSubDate.Text), base.UserProfile, txtID.Text, 1, base.EXCEL_SIZE_Key, "N");

                        export.CreateExcel(res.DataResponse.Tables[0], columns);

                    }
                }
                else
                {
                    int total = lblTotalrecord2.Text == "" ? 0 : lblTotalrecord2.Text.ToInt();
                    if (total > base.EXCEL_SIZE_Key)
                    {
                        this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                        this.MasterSite.ModelError.ShowModalError();
                    }
                    else
                    {
                        Dictionary<string, string> columns = new Dictionary<string, string>();
                        columns.Add("ลำดับ", "RUN_NO");
                        columns.Add("เลขใบอนุญาต", "LICENSE_NO");
                        columns.Add("ครั้งที่ต่ออายุ", "renew_times");
                        columns.Add("วันที่ต่ออายุ", "RENEW_DATE");
                        columns.Add("วันที่หมดอายุ", "EXPIRE_DATE");
                        columns.Add("ชื่อ", "T_NAME");
                        columns.Add("นามสกุล", "T_LAST");
                        var biz = new BLL.PaymentBiz();
                        var res = biz.GetSubGroup(ddlTypePayment.SelectedValue, Convert.ToDateTime(txtStartPaidSubDate.Text),
                                                         Convert.ToDateTime(txtEndPaidSubDate.Text), base.UserProfile, txtID.Text, 1, base.EXCEL_SIZE_Key, "N");

                        export.CreateExcel(res.DataResponse.Tables[0], columns);

                    }
                }
            }
            catch { }
        }

        public override void VerifyRenderingInServerForm(Control control) { }


    }
}
