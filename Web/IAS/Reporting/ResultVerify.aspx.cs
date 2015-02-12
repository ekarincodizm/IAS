using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using System.Data;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Reporting
{
    public partial class ResultVerify : basepage
    {
        #region Public Param & Session
        public int PAGE_SIZE;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        string maxBefore = string.Empty;
        string GroupIDNumber = string.Empty;
        string IDCard = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            txtStartDate.Attributes.Add("readonly", "true");
            txtEndDate.Attributes.Add("readonly", "true");


            if (!Page.IsPostBack)
            {
                base.HasPermit();

                GetTypeDocument();
                GetProvince();

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
                txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            }
        }


        private Action<DropDownList, DTO.DataItem[]> BindToDDLAr = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetTypeDocument()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetRequestLicenseType("ทั้งหมด");
            BindToDDLAr(ddlLicenseType, ls.DataResponse);
        }

        private void GetProvince()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(message);
            BindToDDL(ddlProvinceCurrentAddress, ls);
            BindToDDL(ddlProvinceRegisterAddress, ls);
        }



        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndDate.Text = string.Empty;
        }
        #region gv2
        protected void hplHeadViewDoc_Click(object sender, EventArgs e)
        {
            divDetail.Visible = true;

            pnlAttach.Visible = false;
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblUploadGroupNoGv = (Label)gr.FindControl("lblUploadGroupNoGv");
            Session["UploadGroupNoGv"] = lblUploadGroupNoGv.Text;
            BindGv2();
        }
        protected void hplGo2_Click(object sender, EventArgs e)
        {
            BindGv2();
        }
        protected void BindGv2()
        {
            divDetail.Visible = true;
            PAGE_SIZE = PAGE_SIZE_Key;
            txtNumberGvSearch2.Text = "1";
            maxBefore = txtInputMaxrow2.Text;
            if ((txtInputMaxrow2.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow2.Text != "" && Convert.ToInt32(txtInputMaxrow2.Text) != 0))
            {

                txtInputMaxrow2.Text = maxBefore;
            }
            else if (txtInputMaxrow2.Text == "" || Convert.ToInt32(txtInputMaxrow2.Text) == 0)
            {
                txtInputMaxrow2.Text = Convert.ToString(PAGE_SIZE);
            }
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow2.Text);
            var resultPage = txtNumberGvSearch2.Text.ToInt();
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var resCount = biz.GetListLicenseDetailVerify(Session["UploadGroupNoGv"].ToString(), "Y", resultPage, PAGE_SIZE);
            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage2.Text = Convert.ToString(TotalPages);
            var ls = biz.GetListLicenseDetailVerify(Session["UploadGroupNoGv"].ToString(), "N", resultPage, PAGE_SIZE);
            gvDetail.DataSource = ls.DataResponse;
            gvDetail.DataBind();
            pnlDetail.Visible = false;
            pnlAttach.Visible = false;
            if (ls.IsError)
            {
                UCModalError.ShowMessageError = ls.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
            
                if (TotalPages > 1)
                {
                    txtNumberGvSearch2.Visible = true;
                    lblParaPage2.Visible = true;
                    txtTotalPage2.Visible = true;
                    btnNextGvSearch2.Visible = true;
                    btnPreviousGvSearch2.Visible = false;
                    btngo2.Visible = true;
                    txtInputMaxrow2.Visible = true;
                    lblHeadInputMaxrow2.Visible = true;
                    lblHeadTotal2.Visible = true;
                    lblTotalrecord2.Visible = true;
                    lblEndTotal2.Visible = true;
                    lblTotalrecord2.Text = dr["rowcount"].ToString();
                }
                else if (TotalPages == 1)
                {
                    txtNumberGvSearch2.Visible = true;
                    lblParaPage2.Visible = true;
                    txtTotalPage2.Visible = true;
                    btnNextGvSearch2.Visible = false;
                    btnPreviousGvSearch2.Visible = false;
                    btngo2.Visible = true;
                    txtInputMaxrow2.Visible = true;
                    lblHeadInputMaxrow2.Visible = true;
                    lblHeadTotal2.Visible = true;
                    lblTotalrecord2.Visible = true;
                    lblEndTotal2.Visible = true;
                    lblTotalrecord2.Text = dr["rowcount"].ToString();
                }
                if (TotalPages == 0)
                {
                    txtNumberGvSearch2.Visible = true;
                    lblParaPage2.Visible = true;
                    txtTotalPage2.Visible = true;
                    btnNextGvSearch2.Visible = false;
                    btnPreviousGvSearch2.Visible = false;

                    btngo2.Visible = true;
                    txtInputMaxrow2.Visible = true;
                    lblHeadInputMaxrow2.Visible = true;
                    lblHeadTotal2.Visible = true;
                    lblTotalrecord2.Visible = true;
                    lblEndTotal2.Visible = true;
                    lblTotalrecord2.Text = "0";
                    txtTotalPage2.Text = "1";
                }
            }
            // Session["UploadGroupNoGv"] = Session["UploadGroupNoGv"].ToString();
        }
        protected void btnPreviousGvSearch2_Click(object sender, EventArgs e)
        {
            //gv3.Visible = false;
            //divGv3.Visible = false;
            var result = txtNumberGvSearch2.Text.ToInt() - 1;

            txtNumberGvSearch2.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = true;
                btnPreviousGvSearch2.Visible = false;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = true;
                btnPreviousGvSearch2.Visible = true;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            BindPage2();
        }

        protected void btnNextGvSearch2_Click(object sender, EventArgs e)
        {
            //gv3.Visible = false;
            //divGv3.Visible = false;
            var result = txtNumberGvSearch2.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage2.Text))
            {
                txtNumberGvSearch2.Text = result.ToString();
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = true;
                btnPreviousGvSearch2.Visible = true;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            else
            {
                txtNumberGvSearch2.Text = txtTotalPage2.Text;
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = false;
                btnPreviousGvSearch2.Visible = true;

                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
            }
            BindPage2();
        }

        protected void BindPage2()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow2.Text);
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var resultPage = txtNumberGvSearch2.Text.ToInt();
            var ls = biz.GetListLicenseDetailVerify(Session["UploadGroupNoGv"].ToString(), "N", resultPage, PAGE_SIZE);
            gvDetail.DataSource = ls.DataResponse;
            gvDetail.DataBind();
        }
        #endregion
        protected void gvHead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = ((Label)e.Row.FindControl("lblStatus"));
                Label statusName = ((Label)e.Row.FindControl("lblStatusName"));
                LinkButton viewImg = ((LinkButton)e.Row.FindControl("hplViewImg"));
                if (status != null && !string.IsNullOrEmpty(status.Text))
                {
                    statusName.Text = status.Text == "N" ? Resources.propSysMessage_LicenseApproveN : Resources.propSysMessage_LicenseApproveP;
                    if (status.Text == "N")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveN;
                    }
                    else if (status.Text == "Y")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveP;
                    }
                    else if (status.Text == "W")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveW;
                    }
                    else
                    {
                        statusName.Text = "";
                    }
                }


            }
        }

        protected void gvHead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetLicenseHeadVerify();
            gvHead.PageIndex = e.NewPageIndex;
            gvHead.DataBind();
            pnlAttach.Visible = false;
        }

        protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetail.PageIndex = e.NewPageIndex;
            gvDetail.DataBind();
            pnlAttach.Visible = false;
        }
        #region Gv3
        //protected void hplViewImg_Click(object sender, EventArgs e)
        //{
        //    var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    Label lblGroupIDNumberGv = (Label)gr.FindControl("lblGroupIDNumberGv");
        //    Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
        //    BindGridImageAccount(lblGroupIDNumberGv.Text, lblIDCardGv.Text);
        //    pnlAttach.Visible = true;
        //}
        protected void hplViewImg_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblGroupIDNumberGv = (Label)gr.FindControl("lblGroupIDNumberGv");
            Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
            lblGroupIdGv2.Text = lblGroupIDNumberGv.Text;
            lblIdCardGV2.Text = lblIDCardGv.Text;
            BindGridImageAccount(lblGroupIDNumberGv.Text, lblIDCardGv.Text);
            pnlAttach.Visible = true;
            pnlDetail.Visible = false;
        }

        private void BindGridImageAccount(string strIDNumberGv, string strIdCard)
        {
            pnlAttach.Visible = true;
            PAGE_SIZE = PAGE_SIZE_Key;
            txtNumberGvSearch3.Text = "1";
            maxBefore = txtInputMaxrow3.Text;
            if ((txtInputMaxrow3.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow3.Text != "" && Convert.ToInt32(txtInputMaxrow3.Text) != 0))
            {

                txtInputMaxrow3.Text = maxBefore;
            }
            else if (txtInputMaxrow3.Text == "" || Convert.ToInt32(txtInputMaxrow3.Text) == 0)
            {
                txtInputMaxrow3.Text = Convert.ToString(PAGE_SIZE);
            }
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var resultPage = txtNumberGvSearch3.Text.ToInt();
            var lsCount = biz.GetLicenseVerifyImgDetail(lblGroupIdGv2.Text, lblIdCardGV2.Text, "Y", resultPage, PAGE_SIZE);
            double dblPageCount = (double)((decimal)lsCount.DataResponse.Count() / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage3.Text = Convert.ToString(TotalPages);
            var ls = biz.GetLicenseVerifyImgDetail(lblGroupIdGv2.Text, lblIdCardGV2.Text, "N", resultPage, PAGE_SIZE);
            gvUpload.DataSource = ls.DataResponse;
            gvUpload.DataBind();
            if (ls.IsError)
            {
                UCModalError.ShowMessageError = ls.ErrorMsg;
                UCModalError.ShowModalError();

            }
            else
            {
              
                if (TotalPages == 0)
                {
                    btnPreviousGvSearch3.Visible = false;
                    btnNextGvSearch3.Visible = false;
                    txtNumberGvSearch3.Visible = true;
                    lblParaPage3.Visible = true;
                    txtTotalPage3.Visible = true;
                 
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = "0";
                    txtTotalPage3.Text = "1";
                }
                else if (TotalPages == 1)
                {
                    txtNumberGvSearch3.Visible = true;
                    lblParaPage3.Visible = true;
                    txtTotalPage3.Visible = true;
                    btnPreviousGvSearch3.Visible = false;
                    btnNextGvSearch3.Visible = false;
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = Convert.ToString(lsCount.DataResponse.Count());
                }
                else if (TotalPages > 1)
                {
                    txtNumberGvSearch3.Visible = true;
                    lblParaPage3.Visible = true;
                    txtTotalPage3.Visible = true;
                    btnNextGvSearch3.Visible = true;
                    btnPreviousGvSearch3.Visible = false;
                    btngo3.Visible = true;
                    txtInputMaxrow3.Visible = true;
                    lblHeadInputMaxrow3.Visible = true;
                    lblHeadTotal3.Visible = true;
                    lblTotalrecord3.Visible = true;
                    lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = Convert.ToString(lsCount.DataResponse.Count());
                }

            }
        }
        protected void btnGo3_Click(object sender, EventArgs e)
        {
            BindGridImageAccount(GroupIDNumber, IDCard);
        }

        protected void BindPage3()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var resultPage = txtNumberGvSearch3.Text.ToInt();
            var ls = biz.GetLicenseVerifyImgDetail(lblGroupIdGv2.Text, lblIdCardGV2.Text, "N", resultPage, PAGE_SIZE);
            gvUpload.DataSource = ls.DataResponse;
            gvUpload.DataBind();
        }

        protected void btnPreviousGvSearch3_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch3.Text.ToInt() - 1;

            txtNumberGvSearch3.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = true;
                btnPreviousGvSearch3.Visible = false;

           
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = true;
                btnPreviousGvSearch3.Visible = true;

        
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;
            }
            BindPage3();
        }

        protected void btnNextGvSearch3_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch3.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage3.Text))
            {
                txtNumberGvSearch3.Text = result.ToString();
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = true;
                btnPreviousGvSearch3.Visible = true;

             
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;
            }
            else
            {
                txtNumberGvSearch3.Text = txtTotalPage3.Text;
                txtNumberGvSearch3.Visible = true;
                lblParaPage3.Visible = true;
                txtTotalPage3.Visible = true;
                btnNextGvSearch3.Visible = false;
                btnPreviousGvSearch3.Visible = true;

             
                btngo3.Visible = true;
                txtInputMaxrow3.Visible = true;
                lblHeadInputMaxrow3.Visible = true;
                lblHeadTotal3.Visible = true;
                lblTotalrecord3.Visible = true;
                lblEndTotal3.Visible = true;

            }
            BindPage3();
        }
        #endregion


        private String LinkPopUp(String postData)
        {
            return String.Format("window.open('{0}?targetImage={1}','','')"
                            , UrlHelper.Resolve("/UserControl/ViewFile.aspx"), IAS.Utils.CryptoBase64.Encryption(postData));
        }

        protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hplView = (LinkButton)e.Row.FindControl("hplViewDetailImg");
                Label lblFileGv = (Label)e.Row.FindControl("lblAttachFilePathGv");
                Label lblTypeGv = (Label)e.Row.FindControl("lblTypeGv");
                if (hplView != null)
                {
                    hplView.Visible = true;
                    hplView.OnClientClick = LinkPopUp(lblFileGv.Text);
                }
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                List<DTO.DocumentType> ls = biz.GetDocumentConfigList();

                if (!string.IsNullOrEmpty(lblFileGv.Text))
                {

                    string[] ary = lblFileGv.Text.Split('.');
                    string[] arrFileType = ary[0].Split('_');
                    foreach (var item in ls)
                    {
                        if (arrFileType[1] == item.DOCUMENT_CODE)
                        {
                            lblTypeGv.Text = item.DOCUMENT_NAME;
                        }

                    }
                    if (string.IsNullOrEmpty(lblTypeGv.Text))
                    {
                        lblTypeGv.Text = Resources.errorResultVerify_001;
                    }

                }

            }


        }

        protected void gvUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblIDNumberGv = (Label)gr.FindControl("lblGroupIDNumberGv");
            Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var ls = biz.GetLicenseVerifyImgDetail(lblIDNumberGv.Text, lblIDCardGv.Text, "N", 1, 20);
            if (ls.IsError)
            {
                UCModalError.ShowMessageError = ls.ErrorMsg;
                UCModalError.ShowModalError();

            }
            else
            {
                gvUpload.PageIndex = e.NewPageIndex;
                gvUpload.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetail.Visible = false;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                    UCModalError.ShowModalError();
                    pnlSearch.Visible = false;
                    divDetail.Visible = false;
                    pnlAttach.Visible = false;
                }
                else
                {
                    GetLicenseHeadVerify();
                    divDetail.Visible = false;
                    pnlSearch.Visible = true;
                    pnlAttach.Visible = false;
                    if (gvHead.Rows.Count > 0)
                    {
                        btnExportExcel.Visible = true;
                    }
                    else
                    {
                        btnExportExcel.Visible = false;
                    }
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                UCModalError.ShowModalError();
            }
        }
        private void GetLicenseHeadVerify()
        {
            PAGE_SIZE = PAGE_SIZE_Key;
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
            txtNumberGvSearch.Text = "1";
            var resultPage = txtNumberGvSearch.Text.ToInt();
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strLicenseType = GetCrit(ddlLicenseType.SelectedValue);
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                DateTime dtStartDate = Convert.ToDateTime(GetCrit(txtStartDate.Text == "" ? "" : txtStartDate.Text));
                DateTime dtEndDate = Convert.ToDateTime(GetCrit(txtEndDate.Text == "" ? "" : txtEndDate.Text));

                var lsCount = biz.GetResultLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, "Y", resultPage, PAGE_SIZE);
                DataSet ds = lsCount.DataResponse;
                DataTable dt = ds.Tables[0];
                var ls = biz.GetResultLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, "N", resultPage, PAGE_SIZE);
                DataSet dsls = ls.DataResponse;
                gvHead.DataSource = dsls;
                gvHead.DataBind();
                if (dt.Rows.Count == 0)
                {


                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    txtTotalPage.Text = "1";
                    btngo.Visible = true;
                    lblTotalrecord.Text = "0";
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                }
                else
                {
                    DataRow dr = dt.Rows[0];

                    int rowcount = Convert.ToInt32(dr["TOTAL"].ToString());
                    double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                    TotalPages = (int)Math.Ceiling(dblPageCount);
                    txtTotalPage.Text = Convert.ToString(TotalPages);


                    if (TotalPages == 0)
                    {

                        txtNumberGvSearch.Visible = true;
                        lblParaPage.Visible = true;
                        txtTotalPage.Visible = true;
                        btnNextGvSearch.Visible = false;
                        btnPreviousGvSearch.Visible = false;

                        btngo.Visible = true;
                        lblTotalrecord.Text = dr["TOTAL"].ToString();
                        txtInputMaxrow.Visible = true;
                        lblHeadInputMaxrow.Visible = true;
                        lblHeadTotal.Visible = true;
                        lblTotalrecord.Visible = true;
                        lblEndTotal.Visible = true;
                    }
                    else if (TotalPages > 1)
                    {
                        txtNumberGvSearch.Visible = true;
                        lblParaPage.Visible = true;
                        txtTotalPage.Visible = true;
                        btnNextGvSearch.Visible = true;

                        btngo.Visible = true;
                        lblTotalrecord.Text = dr["TOTAL"].ToString();
                        txtInputMaxrow.Visible = true;
                        lblHeadInputMaxrow.Visible = true;
                        lblHeadTotal.Visible = true;
                        lblTotalrecord.Visible = true;
                        lblEndTotal.Visible = true;
                        //btnExportExcel.Visible = true;
                    }
                    else if (TotalPages == 1)
                    {
                        txtNumberGvSearch.Visible = true;
                        lblParaPage.Visible = true;
                        txtTotalPage.Visible = true;

                        btngo.Visible = true;
                        lblTotalrecord.Text = dr["TOTAL"].ToString();
                        txtInputMaxrow.Visible = true;
                        lblHeadInputMaxrow.Visible = true;
                        lblHeadTotal.Visible = true;
                        lblTotalrecord.Visible = true;
                        lblEndTotal.Visible = true;
                        // btnExportExcel.Visible = true;
                    }
                    UpdatePanelSearch.Update();
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                UCModalError.ShowModalError();
            }

        }
        #region gvHead
       

        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strLicenseType = GetCrit(ddlLicenseType.SelectedValue);
            DateTime dtStartDate = Convert.ToDateTime(GetCrit(txtStartDate.Text == "" ? "" : txtStartDate.Text));
            DateTime dtEndDate = Convert.ToDateTime(GetCrit(txtEndDate.Text == "" ? "" : txtEndDate.Text));
            var ls = biz.GetResultLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, "N", resultPage, PAGE_SIZE);
            gvHead.DataSource = ls.DataResponse;
            gvHead.DataBind();
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            //gv2.Visible = false;
            //divGv2.Visible = false;
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
            //gv2.Visible = false;
            //divGv2.Visible = false;

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
        #endregion
        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelectVerify = ((CheckBox)e.Row.FindControl("chkSelectVerify"));
                Label status = ((Label)e.Row.FindControl("lblStatus"));
                Label statusName = ((Label)e.Row.FindControl("lblStatusName"));
                Label lblIDCardG = ((Label)e.Row.FindControl("lblIDCardGv"));
                Label lblGroupIDNumberGv = ((Label)e.Row.FindControl("lblGroupIDNumberGv"));

                LinkButton viewImg = ((LinkButton)e.Row.FindControl("hplViewImg"));

                if (status != null && !string.IsNullOrEmpty(status.Text))
                {
                    if (status.Text == "N")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveN;
                    }
                    else if (status.Text == "Y")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveP;
                    }
                    else if (status.Text == "W")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveW;
                    }
                    else
                    {
                        statusName.Text = "";
                    }
                }

                BLL.LicenseBiz biz = new BLL.LicenseBiz();
                var res = biz.GetLicenseVerifyImgDetail(lblGroupIDNumberGv.Text, lblIDCardG.Text, "N", 1, 20);
                if (res.DataResponse.ToList().Count <= 0)
                {
                    viewImg.Text = string.Empty;
                }

            }
        }

        protected void hplViewDoc_Click(object sender, EventArgs e)
        {
            pnlAttach.Visible = false;
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNumber = (Label)gv.FindControl("lblIDNumberGv");
            var strGroupNumber = (Label)gv.FindControl("lblGroupIDNumberGv");
            var status = (Label)gv.FindControl("lblStatus");

            hdfNumber.Value = strNumber.Text;
            hdfGroupNumber.Value = strGroupNumber.Text;
            var biz = new BLL.LicenseBiz();
            var res = biz.GetLicenseVerifyDetail(strGroupNumber.Text, strNumber.Text);

            pnlDetail.Visible = true;

            txtFirstName.Text = res.DataResponse.NAMES == "" ? "" : res.DataResponse.NAMES;
            txtLastName.Text = res.DataResponse.LASTNAME == "" ? "" : res.DataResponse.LASTNAME;
            txtIDNumber.Text = res.DataResponse.ID_CARD_NO == "" ? "" : res.DataResponse.ID_CARD_NO;
            txtTitle.Text = res.DataResponse.TITLE_NAME == "" ? "" : res.DataResponse.TITLE_NAME;
            txtDateLicense.Text = Convert.ToString(res.DataResponse.LICENSE_DATE) == "" ? "" : string.Format("{0:dd/MM/yyyy}", res.DataResponse.LICENSE_DATE);
            txtExpireDate.Text = Convert.ToString(res.DataResponse.LICENSE_EXPIRE_DATE) == "" ? "" : string.Format("{0:dd/MM/yyyy}", res.DataResponse.LICENSE_EXPIRE_DATE);
            txtNumberLicense.Text = res.DataResponse.LICENSE_NO == "" ? "" : res.DataResponse.LICENSE_NO;
            txtEmail.Text = res.DataResponse.EMAIL == "" ? "" : res.DataResponse.EMAIL;
            txtTimeMove.Text = res.DataResponse.RENEW_TIMES == "" ? "" : res.DataResponse.RENEW_TIMES;
            txtCompCode.Text = res.DataResponse.OLD_COMP_CODE == "" ? "" : res.DataResponse.OLD_COMP_CODE;
            txtFee.Text = Convert.ToString(res.DataResponse.FEES) == "" ? "" : Convert.ToString(res.DataResponse.FEES);
            txtCurrentAddress.Text = res.DataResponse.CURRENT_ADDRESS == "" ? "" : res.DataResponse.CURRENT_ADDRESS;
            txtRegisterAddress.Text = res.DataResponse.LOCAL_ADDRESS == "" ? "" : res.DataResponse.LOCAL_ADDRESS;
            txtDateAccept.Text = Convert.ToString(res.DataResponse.AR_DATE) == "" ? "" : Convert.ToString(res.DataResponse.AR_DATE);

            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();
            ddlProvinceCurrentAddress.SelectedValue = res.DataResponse.CURRENT_PROVINCE_CODE;
            var lsPC = dataCenter.GetAmpur(message, ddlProvinceCurrentAddress.SelectedValue);
            BindToDDL(ddlDistrictCurrentAddress, lsPC);
            foreach (var item in lsPC)
            {
                if (item.Id == res.DataResponse.CURRENT_AMPUR_CODE)
                {
                    ddlDistrictCurrentAddress.SelectedValue = res.DataResponse.CURRENT_AMPUR_CODE;
                }
            }

            var lsTC = dataCenter.GetTumbon(message, ddlProvinceCurrentAddress.SelectedValue, ddlDistrictCurrentAddress.SelectedValue);
            BindToDDL(ddlParishCurrentAddress, lsTC);
            foreach (var item in lsTC)
            {
                if (item.Id == res.DataResponse.CURRENT_TUMBON_CODE)
                {
                    ddlParishCurrentAddress.SelectedValue = res.DataResponse.CURRENT_TUMBON_CODE;
                }
            }

            ddlProvinceRegisterAddress.SelectedValue = res.DataResponse.LOCAL_PROVINCE_CODE;
            var lsPR = dataCenter.GetAmpur(message, ddlProvinceRegisterAddress.SelectedValue);
            BindToDDL(ddlDistrictRegisterAddress, lsPR);
            foreach (var item in lsPR)
            {
                if (item.Id == res.DataResponse.LOCAL_AMPUR_CODE)
                {
                    ddlDistrictRegisterAddress.SelectedValue = res.DataResponse.LOCAL_AMPUR_CODE;
                }
            }

            var lsTR = dataCenter.GetTumbon(message, ddlProvinceRegisterAddress.SelectedValue, ddlDistrictRegisterAddress.SelectedValue);
            BindToDDL(ddlParishRegisterAddress, lsTR);
            foreach (var item in lsTR)
            {
                if (item.Id == res.DataResponse.LOCAL_TUMBON_CODE)
                {
                    ddlParishRegisterAddress.SelectedValue = res.DataResponse.LOCAL_TUMBON_CODE;
                }
            }


            UpdatePanelSearch.Update();
            var HeaderApprove = biz.GetVerifyHeadByUploadGroupNo(strGroupNumber.Text);
            if (res.DataResponse.APPROVED == "Y")
            {
                rblApprove.SelectedValue = "Y";
            }
            else if (res.DataResponse.APPROVED == "N")
            {
                rblApprove.SelectedValue = "N";
            }
            else if (res.DataResponse.APPROVED == "W")
            {
                rblApprove.ClearSelection();
            }
        }
        ExportBiz export = new ExportBiz();
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int total = lblTotalrecord.Text == "" ? 0 : lblTotalrecord.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    UpdatePanelSearch.Update();
                }
                else
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับ", "RUN_NO");
                    columns.Add("เลขที่ใบคำขออนุญาต", "UPLOAD_GROUP_NO");
                    columns.Add("ชื่อไฟล์", "FILENAME");
                    columns.Add("วันที่นำส่ง", "TRAN_DATE");
                    columns.Add("รูปแบบการขอใบอนุญาต", "PETITION_TYPE_NAME");
                    columns.Add("ประเภทการขอใบอนุญาต", "LICENSE_TYPE_NAME");
                    columns.Add("รายการทั้งหมด", "LOTS");

                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ประเภท ",
                        ValueColumnsOne = ddlLicenseType.SelectedItem.Text
                    });
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ตั้งแต่วันที่ ",
                        ValueColumnsOne = txtStartDate.Text,
                        NameColumnsTwo = "ถึงวันที่ ",
                        ValueColumnsTwo = txtEndDate.Text
                    });
                    


                    BLL.LicenseBiz biz = new BLL.LicenseBiz();
                    Func<string, string> GetCrit = anyString =>
                    {
                        return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
                    };
                    string strLicenseType = GetCrit(ddlLicenseType.SelectedValue);
                    DateTime dtStartDate = Convert.ToDateTime(GetCrit(txtStartDate.Text == "" ? "" : txtStartDate.Text));
                    DateTime dtEndDate = Convert.ToDateTime(GetCrit(txtEndDate.Text == "" ? "" : txtEndDate.Text));
                    export.CreateExcel(biz.GetResultLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, "N", 1, base.EXCEL_SIZE_Key).DataResponse.Tables[0],columns,header,base.UserProfile);
                }
            }
            catch { }
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void btnExportExcel2_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int total = lblTotalrecord2.Text == "" ? 0 : lblTotalrecord2.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    UpdatePanelSearch.Update();
                }
                else
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับ", "RUN_NO");
                    columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                    columns.Add("หมายเลขกลุ่ม", "UPLOAD_GROUP_NO");
                    columns.Add("หมายเลข", "SEQ_NO");
                    columns.Add("วันที่พิจารณา", "APPROVED_DATE");
                    columns.Add("ผู้พิจารณา", "NAME");
                    BLL.LicenseBiz biz = new BLL.LicenseBiz();
                    export.CreateExcel(biz.GetListLicenseDetailVerify(Session["UploadGroupNoGv"].ToString(), "N", 1, base.EXCEL_SIZE_Key).DataResponse.Tables[0],columns,base.UserProfile);
                }
            }
            catch { }
        }

        protected void btnMainCancle_Click(object sender, EventArgs e)
        {
            GetTypeDocument();
     
            defaultData();
            pnlSearch.Visible = false;
            pnlAttach.Visible = false;
            pnlDetail.Visible = false;
        }


        protected void defaultData()
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
        }
    }
}
