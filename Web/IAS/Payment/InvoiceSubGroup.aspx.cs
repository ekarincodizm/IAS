using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Payment
{
    public partial class InvoiceSubGroup : basepage
    {
        #region Public Param & Session

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
        
       // Int32 Maxrow = Convert.ToInt32(txtInputMaxrow.Text);

        public Int32 PAGE_SIZE;

        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEndPaidSubDate.Attributes.Add("readonly", "true");
            txtStartPaidSubDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {

                base.HasPermit();

                defaultData();
                BindTxtCompany();
                lsOderInvoice.Clear();
                PAGE_SIZE = PAGE_SIZE_Key;
            }
        }
        protected void defaultData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtCompany.Text = "";
            btnOk.Visible = false;
            Detaill.Visible = false;
        }
        public MasterPage.Site1 MasterSite
        {

            get { return (this.Page.Master as MasterPage.Site1); }
        }
        #endregion
        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnOk_Click(object sender, EventArgs e)
        {

            //this.ListPayment.Clear();
            clearControl();
            if (gvPaymentAssimilate.Visible == true)
            {
                //foreach (GridViewRow gr in gvPaymentAssimilate.Rows)
                //{
                //    if (((CheckBox)gr.FindControl("chkSelectPayment")).Checked == true)
                //    {
                //        var lblPaymentAssimilateGv = (Label)gr.FindControl("lblPaymentAssimilateGv");
                //        this.ListPayment.Add(lblPaymentAssimilateGv.Text);
                //    }
                //}

                if (lsOderInvoice.Count > 0)
                {
                    
                    var biz = new BLL.PaymentBiz();
             

                        //lblPaymentAssimilateNumberDetail.Text = res.DataResponse.Ref1;
                        //lblRef2.Text = res.DataResponse.Ref2;
                        mpEdit.Show();

                        //UplPopUp.Update();
                   
                }
                else
                {
                    //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.ChooseData);

                    this.MasterSite.ModelError.ShowMessageError = SysMessage.ChooseData;
                    this.MasterSite.ModelError.ShowModalError();
                }

            }
        }
        public string Mem;
       // protected string ExpDate = ConfigurationManager.AppSettings["
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            GvInvoicePopup.Visible = true;
            GvInvoicePopup.DataSource = lsOderInvoice.ToList();
            GvInvoicePopup.DataBind();
            MPDetail.Show();
            remark.Visible = true;
            UpdatePanelSearch.Update();

        }
        protected void btnSavePopup_Click(object sender, EventArgs e)
        {

            string ref1 = string.Empty;
            var biz = new BLL.PaymentBiz();
            var res = biz.NewCreatePayment(lsOderInvoice.ToArray(), txtRemark.Text, base.UserId, base.UserProfile.CompCode, Convert.ToString(base.UserProfile.MemberType), out ref1);

            if (res.IsError)
            {
                //var errorMsg = res.ErrorMsg;

                //AlertMessage.ShowAlertMessage(string.Empty, errorMsg);

                this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                this.MasterSite.ModelError.ShowModalError();
                GetDataInGridView();
            }
            else
            {
                GetDataInGridView();
                this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.errorSysMessage_CreatePaymentSuccess;
                this.MasterSite.ModelSuccess.ShowModalSuccess();
                lsOderInvoice.Clear();
            }
        }


        private void clearControl()
        {
            //lblPaymentAssimilateNumberDetail.Text = string.Empty;
            txtRemark.Text = string.Empty;
        }

        protected void hdf_ValueChanged(object sender, EventArgs e)
        {
            string selectedWidgetID = ((HiddenField)sender).Value;
            //Widget w = MyEntityService.GetWidget(selectedWidgetID);
            string[] compCode = selectedWidgetID.Split('[', ']');

            txtID.Text = compCode[1];
            //txtNmae.Text = compCode[0];

        }
        private void BindTxtCompany()
        {
            if (base.UserProfile.MemberType == 2)
            {
                var biz = new BLL.DataCenterBiz();
                var getCompanyName = biz.GetDefaultcompanyName(base.UserProfile.CompCode);
                if (getCompanyName.DataResponse.Tables[0].Rows.Count > 0)
                {
                    DataRow drComName = getCompanyName.DataResponse.Tables[0].Rows[0];
                    txtCompany.Text = drComName["comName"].ToString();
                    txtCompany.Enabled = false;
                }
            }
        }
        private void GetDataInGridView()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            txtNumberGvSearch.Text = "1";
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            string IdParameter;
            if (base.UserProfile.MemberType == 1)
            {
                IdParameter = base.UserProfile.Id;
            }
            else 
            {
                IdParameter = base.UserProfile.CompCode;
            
            }
            if (txtCompany.Text == "")
            {
                txtID.Text = "";
            }
                var resCount = biz.GetGroupPayment(IdParameter, Convert.ToDateTime(txtStartPaidSubDate.Text), Convert.ToDateTime(txtEndPaidSubDate.Text), Convert.ToString(base.UserProfile.MemberType),txtID.Text, resultPage, PAGE_SIZE, "Y");
                DataSet ds = resCount.DataResponse;
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                TotalPages = (int)Math.Ceiling(dblPageCount);
                txtTotalPage.Text = Convert.ToString(TotalPages);

                var res = biz.GetGroupPayment(IdParameter, Convert.ToDateTime(txtStartPaidSubDate.Text), Convert.ToDateTime(txtEndPaidSubDate.Text), Convert.ToString(base.UserProfile.MemberType), txtID.Text, resultPage, PAGE_SIZE, "N");
            string date1 = txtStartPaidSubDate.Text;
            string date2 = txtEndPaidSubDate.Text;
            gvPaymentAssimilate.Visible = true;
            gvPaymentAssimilate.DataSource = res.DataResponse;
            gvPaymentAssimilate.DataBind();
            if (res.IsError)
            {
                this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                if (gvPaymentAssimilate.Rows.Count == 0 )
                {
                    btnOk.Visible = false;
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;

                    btngo.Visible = true;
                    lblTotalrecord.Text = "0";
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                    txtTotalPage.Text = "1";
                    CheckBox Chall = (CheckBox)gvPaymentAssimilate.HeaderRow.FindControl("checkall");
                    Chall.Visible = false;
                }
                else
                {
                    CheckBox Chall = (CheckBox)gvPaymentAssimilate.HeaderRow.FindControl("checkall");
                    Chall.Visible = true;
                    btnOk.Visible = true;
                    btnExportExcel.Visible = true;
         
                }


                boxresult.Visible = true;
                UpdatePanelSearch.Update();
                if (TotalPages > 1)
                {
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;
                    btnNextGvSearch.Visible = true;
                    btnPreviousGvSearch.Visible = false;
                    btngo.Visible = true;
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                    lblTotalrecord.Text = dr["rowcount"].ToString();
                }
                else if(TotalPages == 1)
                {
                    txtNumberGvSearch.Visible = true;
                   lblParaPage.Visible = true;
                   txtTotalPage.Visible = true;
                   btnNextGvSearch.Visible = false;
                   btnPreviousGvSearch.Visible = false;
                   btngo.Visible = true;
                   lblTotalrecord.Text = dr["rowcount"].ToString();
                   txtInputMaxrow.Visible = true;
                   lblHeadInputMaxrow.Visible = true;
                   lblHeadTotal.Visible = true;
                   lblTotalrecord.Visible = true;
                   lblEndTotal.Visible = true;
                }
                Detaill.Visible = true;
            }
            if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.ToInt() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.ToInt())
            {
                btnOk.Visible = false;
                gvPaymentAssimilate.Columns[0].Visible = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           
            txtNumberGvSearch.Text = "1";
            boxresult.Visible = true;
            PAGE_SIZE = PAGE_SIZE_Key;
            string maxBefore = string.Empty;
            maxBefore = txtInputMaxrow.Text;
            if ((txtStartPaidSubDate.Text != "" && txtEndPaidSubDate.Text !="")&&(Convert.ToDateTime(txtStartPaidSubDate.Text)>(Convert.ToDateTime(txtEndPaidSubDate.Text))))
            {
                div_total.Visible = false;
                btnPreviousGvSearch.Visible = false;
                btnNextGvSearch.Visible = false;
                txtNumberGvSearch.Visible = false;
                lblParaPage.Visible = false;
                txtTotalPage.Visible = false;
                lblHeadInputMaxrow.Visible = false;
                txtInputMaxrow.Visible = false;
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError();
                Detaill.Visible = false;


            }
            else
            {
                div_total.Visible = true;
                if ((txtInputMaxrow.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow.Text != "" && Convert.ToInt32(txtInputMaxrow.Text) != 0) )
                {
                    txtInputMaxrow.Text = maxBefore;
                }
                else if (txtInputMaxrow.Text == "" || Convert.ToInt32(txtInputMaxrow.Text) == 0)
                {
                    txtInputMaxrow.Text = Convert.ToString(PAGE_SIZE);
                }            
                GetDataInGridView();
                ShowHideExport();
            }

        }

        private void ShowHideExport()
        {
            if (gvPaymentAssimilate.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
                var result = txtNumberGvSearch.Text.ToInt() - 1;
            
                txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
                if (result.ToString() == "1")
                {
                    btnPreviousGvSearch.Visible = false;
                   
                    txtNumberGvSearch.Visible = true;
                    btnNextGvSearch.Visible = true;


                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                    btngo.Visible = true;
                }
                else if (Convert.ToInt32(result) > 1)
                {
                    btnPreviousGvSearch.Visible = true;
                    txtNumberGvSearch.Visible = true;
                    btnNextGvSearch.Visible = true;


                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                    btngo.Visible = true;
                }
                BindPage();
                    
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            else
            {
                txtNumberGvSearch.Text = txtTotalPage.Text;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
               BindPage();
        }


        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            string IdParameter;
            if (base.UserProfile.MemberType == 1)
            {
                IdParameter = base.UserProfile.Id;
            }
            else 
            {
                IdParameter = base.UserProfile.CompCode;
            
            }
            var res = biz.GetGroupPayment(IdParameter, Convert.ToDateTime(txtStartPaidSubDate.Text), Convert.ToDateTime(txtEndPaidSubDate.Text), Convert.ToString(base.UserProfile.MemberType), txtID.Text, resultPage, PAGE_SIZE, "N");

       
                gvPaymentAssimilate.Visible = true;
                gvPaymentAssimilate.DataSource = res.DataResponse;
                gvPaymentAssimilate.DataBind();

                boxresult.Visible = true;
           
        
        }
        protected void gvPaymentAssimilate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.HtmlControls.HtmlInputControl headerchk = this.FindControl("chkAll") as System.Web.UI.HtmlControls.HtmlInputControl;
               // CheckBox headerchk = (CheckBox)gvPaymentAssimilate.HeaderRow.FindControl("chkheader");
                CheckBox childchk = (CheckBox)e.Row.FindControl("chkchild");
                childchk.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + headerchk.ClientID + "')");


            }
        }
        private void GetCtrl()
        {
            System.Web.UI.HtmlControls.HtmlInputControl datepickerControl = this.FindControl("chkAll") as System.Web.UI.HtmlControls.HtmlInputControl;

            //Get String Value
            string getValue = datepickerControl.Value;
            //Get bool Value
            bool getChecked = ((System.Web.UI.HtmlControls.HtmlInputCheckBox)(datepickerControl)).Checked;

        }


        //code by nami
        protected void checkall_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkall = (CheckBox)sender;
            foreach (GridViewRow gr in gvPaymentAssimilate.Rows)
            {
                CheckBox checkselect = (CheckBox)gr.FindControl("chkSelectPayment");

                if (checkall.Checked)
                {
                    if (!checkselect.Checked)
                    {
                        var lblPaymentAssimilateGv = (Label)gr.FindControl("lblPaymentAssimilateGv");
                        var lblRunNo = (Label)gr.FindControl("lblRunNo");
                        var lblTypePaymentGv = (Label)gr.FindControl("lblTypePaymentGv");
                        var lblAmountPeopleGv = (Label)gr.FindControl("lblAmountPeopleGv");
                        var lblAmountMoneyGv = (Label)gr.FindControl("lblAmountMoneyGv");
                        var lblPaymentAssimilateDateGv = (Label)gr.FindControl("lblPaymentAssimilateDateGv");
                        lsOderInvoice.Add(new DTO.OrderInvoice
                        {
                            UPLOAD_GROUP_NO = lblPaymentAssimilateGv.Text,
                            RUN_NO = Convert.ToString(lsOderInvoice.Count + 1),
                            PETITION_TYPE_NAME = lblTypePaymentGv.Text,
                            CountPerson = lblAmountPeopleGv.Text,
                            Amount = lblAmountMoneyGv.Text,
                            SubPaymentDate = lblPaymentAssimilateDateGv.Text

                        });
                        checkselect.Checked = true;
                    }
                }
                else
                {
                    var lblPaymentAssimilateGv = (Label)gr.FindControl("lblPaymentAssimilateGv");
                    var pament = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblPaymentAssimilateGv.Text);
                    lsOderInvoice.Remove(pament);
                    checkselect.Checked = false;
                }
            }
        }
        bool b_check = true;
        CheckBox check_all_head;
        protected void gvPaymentAssimilate_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                check_all_head = (CheckBox)e.Row.FindControl("checkall");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblPaymentAssimilateGv = (Label)e.Row.FindControl("lblPaymentAssimilateGv");
                CheckBox checkselect = (CheckBox)e.Row.FindControl("chkSelectPayment");
                var l = lsOderInvoice.FirstOrDefault(x => x.HeadrequestNo == lblPaymentAssimilateGv.Text);
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
        }

        protected void chkSelectPayment_CheckedChanged1(object sender, EventArgs e)
        {
            CheckBox checkselect = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)checkselect.Parent.Parent;
            var lblPaymentAssimilateGv = (Label)gr.FindControl("lblPaymentAssimilateGv");
             var lblRunNo = (Label)gr.FindControl("lblRunNo");
             var lblTypePaymentGv = (Label)gr.FindControl("lblTypePaymentGv");
             var lblAmountPeopleGv = (Label)gr.FindControl("lblAmountPeopleGv");
             var lblAmountMoneyGv = (Label)gr.FindControl("lblAmountMoneyGv");
             var lblPaymentAssimilateDateGv = (Label)gr.FindControl("lblPaymentAssimilateDateGv");
            if (checkselect.Checked)
            {
                lsOderInvoice.Add(new DTO.OrderInvoice
                {
                    UPLOAD_GROUP_NO = lblPaymentAssimilateGv.Text,
                    RUN_NO = Convert.ToString(lsOderInvoice.Count + 1),
                    PETITION_TYPE_NAME = lblTypePaymentGv.Text,
                    CountPerson = lblAmountPeopleGv.Text,
                    Amount = lblAmountMoneyGv.Text,
                    SubPaymentDate = lblPaymentAssimilateDateGv.Text

                });
         
            }
            else
            {
                ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("checkall")).Checked = false;
                var pament = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblPaymentAssimilateGv.Text);
                lsOderInvoice.Remove(pament);
            }           
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            mpEdit.Hide();
           // boxresult.Visible = false;
            
        }
        protected void btnMainCancle_Click(object sender, EventArgs e)
        {
            defaultData();
            boxresult.Visible = false;
        }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int total = lblTotalrecord.Text == "" ? 0 : lblTotalrecord.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                    this.MasterSite.ModelError.ShowModalError();
                    UpdatePanelSearch.Update();
                }
                else
                {
                    ExportBiz export = new ExportBiz();
                    var biz = new BLL.PaymentBiz();
                    string IdParameter;
                    if (base.UserProfile.MemberType == 1)
                    {
                        IdParameter = base.UserProfile.Id;
                    }
                    else
                    {
                        IdParameter = base.UserProfile.CompCode;
                    }
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับ", "RUN_NO");
                    columns.Add("ใบสั่งจ่ายย่อย", "HEAD_REQUEST_NO");
                    columns.Add("ประเภทใบสั่งจ่าย", "PETITION_TYPE_NAME");
                    columns.Add("จำนวนคน", "PERSON_NO");
                    columns.Add("จำนวนเงิน", "SUBPAYMENT_AMOUNT");
                    columns.Add("วันที่จ่ายย่อย", "SUBPAYMENT_DATE");
                    var res = biz.GetGroupPayment(IdParameter, Convert.ToDateTime(txtStartPaidSubDate.Text), Convert.ToDateTime(txtEndPaidSubDate.Text), Convert.ToString(base.UserProfile.MemberType), txtID.Text, 1, base.EXCEL_SIZE_Key, "N");
                    export.CreateExcel(res.DataResponse, columns);
                }
            }
            catch { }
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void btngo_Click(object sender, EventArgs e)
        {
            //ListPayment = new List<string>();
            txtNumberGvSearch.Text = "1";
            btnPreviousGvSearch.Visible = false;
            txtNumberGvSearch.Visible = false;
            btnNextGvSearch.Visible = false;
            lblParaPage.Visible = false;
            txtTotalPage.Visible = false;
            txtInputMaxrow.Visible = false;
            lblHeadInputMaxrow.Visible = false;
            lblHeadTotal.Visible = false;
            lblTotalrecord.Visible = false;
            lblEndTotal.Visible = false;
            btngo.Visible = false;
            PAGE_SIZE = PAGE_SIZE_Key;
            string maxBefore = string.Empty;
            maxBefore = txtInputMaxrow.Text;
            if ((txtInputMaxrow.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow.Text != "" && txtInputMaxrow.Text != "0"))
            {

                txtInputMaxrow.Text = maxBefore;
            }
            else if (txtInputMaxrow.Text == "" || txtInputMaxrow.Text == "0")
            {
                txtInputMaxrow.Text = Convert.ToString(PAGE_SIZE);
            }

            GetDataInGridView();           
        }

        protected void GvInvoicePopup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblPaymentAssimilateGv = (Label)e.Row.FindControl("lblPaymentAssimilateGv");
                var LBUP = (LinkButton)e.Row.FindControl("LBUP");
                var LBDown = (LinkButton)e.Row.FindControl("LBDown");
                var list = lsOderInvoice.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblPaymentAssimilateGv.Text);
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
        }

        protected void GvInvoicePopup__RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            List<DTO.OrderInvoice> newls = new Class.InvoiceSortDescriptions().PaymentSortDesc(this.lsOderInvoice, CurrentIndex, e.CommandName);

            //Rebind
            this.GvInvoicePopup.DataSource = newls;
            this.GvInvoicePopup.DataBind();
            MPDetail.Show();
        }

        protected void btnCloseProp_Click(object sender, EventArgs e)
        {
            lsOderInvoice.Clear();
            MPDetail.Hide();
            GetDataInGridView();
            UpdatePanelSearch.Update();
        }
    }
}
