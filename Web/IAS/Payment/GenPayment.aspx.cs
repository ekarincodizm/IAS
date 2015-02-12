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
using IAS.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IAS.BLL;
using System.Configuration;
using System.IO;
using IAS.Properties;

namespace IAS.Payment
{
    public partial class GenPayment : basepage
    {
        #region Public Param & Session

   
        public List<DTO.GenReceipt> lsGenRecive
        {
            get
            {
                if (Session["lsGenRecive"] == null)
                {
                    Session["lsGenRecive"] = new List<DTO.GenReceipt>();
                }

                return (List<DTO.GenReceipt>)Session["lsGenRecive"];
            }
            set
            {
                Session["lsGenRecive"] = value;
            }
        }
        public int PAGE_SIZE;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        public string maxBefore;
        string GroupRequsetNo = string.Empty;
        string HeadRequestNoSub = string.Empty;
        public MasterPage.Site1 MasterSite
        {

            get { return (this.Page.Master as MasterPage.Site1); }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session[PageList.UserProfile] = new DTO.UserProfile
            //{
            //    Id = "1100800754317",
            //    IdCard = "3101400554580",
            //    MemberType = DTO.RegistrationType.Insurance.GetEnumValue(),
            //    Name = "ทดสอบ ระบบ",
            //    CompCode = "1008",
            //    UserGroup = ""
            //};
            txtStartPaidSubDate.Attributes.Add("readonly", "true");
            txtEndPaidSubDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                DefaultData();
                PAGE_SIZE = PAGE_SIZE_Key;
            }
        }
        protected void DefaultData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndPaidSubDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndPaidSubDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtPaidGroup.Text = string.Empty;

            bludDiv.Visible = false;
       
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtStartPaidSubDate.Text) > Convert.ToDateTime(txtEndPaidSubDate.Text))
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError(); 
                bludDiv.Visible = false;
               

                txtNumberGvSearch.Visible = false;
                lblParaPage.Visible = false;
                txtTotalPage.Visible = false;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = false;
           
            }
            else
            {
                ClearAll();
            }
        }

        protected void ClearAll()
        {
          

            txtNumberGvSearch.Visible = false;
            lblParaPage.Visible = false;
            txtTotalPage.Visible = false;
            btnNextGvSearch.Visible = false;
            btnPreviousGvSearch.Visible = false;
            gvSearch.DataMember = null;

            gvSearch.DataBind();
        
            PAGE_SIZE = PAGE_SIZE_Key;
            BindDataInGridView();

        }

        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartPaidSubDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndPaidSubDate.Text = string.Empty;
        }
   
        //log4net
 
        private void BindDataInGridView()
        {
            txtNumberGvSearch.Text = "1";
            maxBefore = txtInputMaxrow.Text;
            if ((txtInputMaxrow.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow.Text != "" && Convert.ToInt32(txtInputMaxrow.Text) != 0))
            {

                txtInputMaxrow.Text = maxBefore;
            }
            else if (txtInputMaxrow.Text == "" || Convert.ToInt32(txtInputMaxrow.Text) == 0)
            {
                txtInputMaxrow.Text = Convert.ToString(PAGE_SIZE);
            }
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            if ((base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) ||
                (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()))
            {
                if (txtStartPaidSubDate.Text != "" && txtEndPaidSubDate.Text != "")
                {
                    var resCount1 = biz.GenPaymentNumberTable(base.UserProfile.IdCard,
                                Convert.ToDateTime(txtStartPaidSubDate.Text),
                                Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", ""), "Y", resultPage, PAGE_SIZE);
                    DataSet ds = resCount1.DataResponse;
                    if (ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            DataRow dr = dt.Rows[0];
                            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                            TotalPages = (int)Math.Ceiling(dblPageCount);
                            txtTotalPage.Text = Convert.ToString(TotalPages);
                            var res1 = biz.GenPaymentNumberTable(base.UserProfile.IdCard,
                                        Convert.ToDateTime(txtStartPaidSubDate.Text),
                                        Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", ""), "N", resultPage, PAGE_SIZE);
                            gvSearch.Visible = true;
                            gvSearch.DataSource = res1.DataResponse;
                            gvSearch.DataBind();
                            bludDiv.Visible = true;
                            if (res1.IsError)
                            {
                                this.MasterSite.ModelError.ShowMessageError = Resources.errorGenPayment_002;
                                this.MasterSite.ModelError.ShowModalError();
                            }
                            else
                            {

                                if (TotalPages > 1)
                                {
                                    txtNumberGvSearch.Visible = true;
                                    lblParaPage.Visible = true;
                                    txtTotalPage.Visible = true;
                                    btnNextGvSearch.Visible = true;
                                    btnPreviousGvSearch.Visible = false;
                                    btngo.Visible = true;
                                    lblTotalrecord.Text = dr["rowcount"].ToString();
                                    txtInputMaxrow.Visible = true;
                                    lblHeadInputMaxrow.Visible = true;
                                    lblHeadTotal.Visible = true;
                                    lblTotalrecord.Visible = true;
                                    lblEndTotal.Visible = true;
                                }
                                else if (TotalPages == 1)
                                {
                                    txtNumberGvSearch.Visible = true;
                                    lblParaPage.Visible = true;
                                    txtTotalPage.Visible = true;
                                    btnPreviousGvSearch.Visible = false;
                                    btnNextGvSearch.Visible = false;
                                    btngo.Visible = true;
                                    lblTotalrecord.Text = dr["rowcount"].ToString();
                                    txtInputMaxrow.Visible = true;
                                    lblHeadInputMaxrow.Visible = true;
                                    lblHeadTotal.Visible = true;
                                    lblTotalrecord.Visible = true;
                                    lblEndTotal.Visible = true;
                                }
                                else if (TotalPages == 0)
                                {
                                    txtNumberGvSearch.Visible = true;
                                    lblParaPage.Visible = true;
                                    txtTotalPage.Visible = true;
                                    txtTotalPage.Text = "1";
                                    btngo.Visible = true;
                                    lblTotalrecord.Text = "0";
                                    txtInputMaxrow.Visible = true;
                                    lblHeadInputMaxrow.Visible = true;
                                    lblHeadTotal.Visible = true;
                                    lblTotalrecord.Visible = true;
                                    lblEndTotal.Visible = true;
                                    btnPreviousGvSearch.Visible = false;
                                    btnNextGvSearch.Visible = false;
                                }
                                if ((base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || 
                                    (base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()))
                                {
                                    btnGenReceiptAll.Visible = false;
                                    gvSearch.Columns[0].Visible = false;
                                    gvSearch.Columns[11].Visible = false;
                                }
                                else
                                {
                                    if (gvSearch.Rows.Count > 0)
                                    {
                                        btnGenReceiptAll.Visible = true;
                                        gvSearch.Columns[0].Visible = true;
                                    }
                                    else
                                    {
                                        btnGenReceiptAll.Visible = false;
                                        gvSearch.Columns[0].Visible = false;
                                    }
                                    
                                    gvSearch.Columns[11].Visible = true;
                                }
                                UpdatePanelSearch.Update();
                            }
                        }
                        else
                        {
                            this.MasterSite.ModelError.ShowMessageError = Resources.errorGenPayment_003;
                            this.MasterSite.ModelError.ShowModalError(); 
                            btnGenReceiptAll.Visible = false;
                        }
                    }
                    else
                    {
                        this.MasterSite.ModelError.ShowMessageError = Resources.errorGenPayment_003;
                        this.MasterSite.ModelError.ShowModalError(); 
                        btnGenReceiptAll.Visible = false;
                    }
                }
            }

            UpdatePanelSearch.Update();
        }

        //log4net
        protected void StatusA_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label group_request = (Label)gr.FindControl("lblGroupRequsetNo");
            Label lblReceiptNo = (Label)gr.FindControl("lblReceiptNo");
            Label lblHEADREQUESTNO = (Label)gr.FindControl("lblHEADREQUESTNO");
            Label lblPAYMENTNO = (Label)gr.FindControl("lblPAYMENTNO");
  
            lsGenRecive.Add(new DTO.GenReceipt
            {
                RECEIPT_NO = lblReceiptNo.Text,
                HEAD_REQUEST_NO = lblHEADREQUESTNO.Text,
                PAYMENT_NO = lblPAYMENTNO.Text,
                RECEIPT_BY_ID = base.UserProfile.Id,
            });
            if (lsGenRecive.Count == 0)
            {
                this.MasterSite.ModelError.ShowMessageError = SysMessage.ChooseData;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                BLL.PaymentBiz biz = new BLL.PaymentBiz();
                DTO.ResponseMessage<bool> res = biz.GenReceiptAll(lsGenRecive.ToArray());
                if (res.ResultMessage == false)
                {
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.infoGenPayment_001;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();

                }
                BindDataInGridView();
                lsGenRecive.Clear();
                this.UpdatePanelSearch.Update();
            }
        }

      
        bool b_check = true;
        CheckBox check_all_head;
        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                check_all_head = (CheckBox)e.Row.FindControl("checkall");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton button = (LinkButton)e.Row.FindControl("hplPrint");
                Label txtGno = (Label)e.Row.FindControl("lblGroupRequsetNo");
                string desc = DataBinder.Eval(e.Row.DataItem, "GROUP_REQUEST_NO").ToString();

                button.Attributes.Add("onclick", string.Format("OpenPopup('{0}')", desc));


                string formatTxt = txtGno.Text.Insert(6, " ").Insert(11, " ");
                txtGno.Text = formatTxt;
                var lblReceiptNo = (Label)e.Row.FindControl("lblReceiptNo");
                CheckBox checkselect = (CheckBox)e.Row.FindControl("chkSelectPayment");
                Label lblPAYMENTNO = (Label)e.Row.FindControl("lblPAYMENTNO");
                if (lsGenRecive.Count > 0)
                {
                    var l = lsGenRecive.FirstOrDefault(x => x.RECEIPT_NO == lblReceiptNo.Text && x.PAYMENT_NO == lblPAYMENTNO.Text);
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

        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            var res1 = biz.GenPaymentNumberTable(base.UserProfile.IdCard,
                                    Convert.ToDateTime(txtStartPaidSubDate.Text),
                                    Convert.ToDateTime(txtEndPaidSubDate.Text), txtPaidGroup.Text.Replace(" ", ""), "N", resultPage, PAGE_SIZE);
            gvSearch.DataSource = res1.DataResponse;
            gvSearch.DataBind();
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
           
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
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
            }
            else if (Convert.ToInt32(result) > 1)
            {
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;

                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            BindPage();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = true;

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
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            BindPage();
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultData();
            bludDiv.Visible = false;
   
        }

        protected void chkSelectPayment_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectPayment = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chkSelectPayment.Parent.Parent;
            Label lblReceiptNo = (Label)gr.FindControl("lblReceiptNo");
            Label lblHEADREQUESTNO = (Label)gr.FindControl("lblHEADREQUESTNO");
            Label lblPAYMENTNO = (Label)gr.FindControl("lblPAYMENTNO");

            if (chkSelectPayment.Checked)
            {
                lsGenRecive.Add(new DTO.GenReceipt
                {
                    RECEIPT_NO = lblReceiptNo.Text,
                    HEAD_REQUEST_NO = lblHEADREQUESTNO.Text,
                    PAYMENT_NO = lblPAYMENTNO.Text,
                    RECEIPT_BY_ID = base.UserProfile.Id,
                });

            }
            else
            {
                ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("checkall")).Checked = false;
                var pament = lsGenRecive.FirstOrDefault(x => x.RECEIPT_NO == lblReceiptNo.Text);
                lsGenRecive.Remove(pament);

            }

        }

        protected void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkall = (CheckBox)sender;
            foreach (GridViewRow gr in gvSearch.Rows)
            {
                CheckBox checkselect = (CheckBox)gr.FindControl("chkSelectPayment");

                if (checkall.Checked)
                {
                 
                        Label lblReceiptNo = (Label)gr.FindControl("lblReceiptNo");
                        Label lblHEADREQUESTNO = (Label)gr.FindControl("lblHEADREQUESTNO");
                        Label lblPAYMENTNO = (Label)gr.FindControl("lblPAYMENTNO");
                        if (checkall.Checked)
                        {
                            if (!checkselect.Checked)
                            {
                                lsGenRecive.Add(new DTO.GenReceipt
                                {
                                    RECEIPT_NO = lblReceiptNo.Text,
                                    HEAD_REQUEST_NO = lblHEADREQUESTNO.Text,
                                    PAYMENT_NO = lblPAYMENTNO.Text,
                                    RECEIPT_BY_ID = base.UserProfile.Id,
                                });
                            }
                            checkselect.Checked = true;
                        }
               
                }
                else
                {
                    Label lblReceiptNo = (Label)gr.FindControl("lblReceiptNo");
                    Label lblHEADREQUESTNO = (Label)gr.FindControl("lblHEADREQUESTNO");
                    Label lblPAYMENTNO = (Label)gr.FindControl("lblPAYMENTNO");
                    lsGenRecive.RemoveAll(x => x.RECEIPT_NO == lblReceiptNo.Text && x.HEAD_REQUEST_NO == lblHEADREQUESTNO.Text && x.PAYMENT_NO == lblPAYMENTNO.Text);
                    
                    //lsGenRecive.Remove(pament);
                    checkselect.Checked = false;
                }
            }
        }

        protected void btnGenReceiptAll_Click(object sender, EventArgs e)
        {
           
            if (lsGenRecive.Count == 0)
            {
                this.MasterSite.ModelError.ShowMessageError = SysMessage.ChooseData;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                
                BLL.PaymentBiz biz = new BLL.PaymentBiz();
                DTO.ResponseMessage<bool> res = biz.GenReceiptAll(lsGenRecive.ToArray());
                if (res.ResultMessage == false)
                {
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.infoGenPayment_001;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();
                    lsGenRecive.Clear();
                }
              
                BindDataInGridView();
                this.UpdatePanelSearch.Update();
            }
         
        }
    }
}
