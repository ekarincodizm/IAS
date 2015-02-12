using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data;
using IAS.Utils;
using System.Threading;
using System.Globalization;
using IAS.BLL;

using IAS.Properties;

namespace IAS.Reporting
{
    public partial class Verifydoc : basepage
    {
        #region Public Param & Session
        public string flgApprove = string.Empty;
        public string aa = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            txtStartDate.Attributes.Add("readonly", "true");
            txtEndDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                GetTypeDocument();
                GetProvince();
                //GetCompayByRequest();//พี่ฟิว
                bindUserType();//ดา แทนGetCompayByRequestของพี่ฟิว
                defaultData();
            }
            this.CurrentLicenseRenewTime = "0";
        }
        private List<DTO.AttachFileDetail> AttatchFileDetail
        {
            get
            {
                return Session["AttatchFileDetail"] == null
                        ? new List<DTO.AttachFileDetail>()
                        : (List<DTO.AttachFileDetail>)Session["AttatchFileDetail"];
            }
            set
            {
                Session["AttatchFileDetail"] = value;
            }
        }
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
        string UploadGroupNo = string.Empty;
        #endregion
        public string LicenseTypeCode
        {
            get
            {
                return Session["LicenseTypeCode"] == null ? string.Empty : Session["LicenseTypeCode"].ToString();
            }
            set
            {
                Session["LicenseTypeCode"] = value;
            }
        }

        public string PettionTypeCode
        {
            get
            {
                return Session["PettionTypeCode"] == null ? string.Empty : Session["PettionTypeCode"].ToString();
            }
            set
            {
                Session["PettionTypeCode"] = value;
            }
        }

        public string CurrentLicenseRenewTime
        {
            get
            {
                return Session["CurrentLicenseRenewTime"] == null ? string.Empty : Session["CurrentLicenseRenewTime"].ToString();
            }
            set
            {
                Session["CurrentLicenseRenewTime"] = value;
            }
        }
        public string CheckApprove
        {
            get
            {
                return Session["CheckApprove"] == null ? string.Empty : Session["CheckApprove"].ToString();
            }
            set
            {
                Session["CheckApprove"] = value;
            }
        }
        public string Mode
        {
            get
            {
                return Session["Mode"] == null ? string.Empty : Session["Mode"].ToString();
            }
            set
            {
                Session["Mode"] = value;
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

        private void GetCompayByRequest()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyByRequest(base.UserProfile.CompCode);
            BindToDDLAr(ddlCompanyRequest, ls.DataResponse);
        }

        private void GetProvince()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(message);
            BindToDDL(ddlProvinceCurrentAddress, ls);
            BindToDDL(ddlProvinceRegisterAddress, ls);
        }

        private void GetTypeDocument()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetRequestLicenseType("ทั้งหมด");
            BindToDDLAr(ddlLicenseType, ls.DataResponse);
        }
        private void bindUserType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetUserVerifyDoc(base.UserProfile.CompCode);
            ddlCompanyRequest.DataSource = res.DataResponse;
            ddlCompanyRequest.DataTextField = "names";
            ddlCompanyRequest.DataValueField = "upload_by_session";
            ddlCompanyRequest.DataBind();
            ddlCompanyRequest.Items.Insert(0, "ทั้งหมด");
        }

        /// <summary>
        /// Renew 19-12-56
        /// PersonService > GetById(string IDCard)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hplViewDoc_Click(object sender, EventArgs e)
        {
            pnlAttach.Visible = false;
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNumber = (Label)gv.FindControl("lblIDNumberGv");
            var strGroupNumber = (Label)gv.FindControl("lblGroupIDNumberGv");
            var status = (Label)gv.FindControl("lblStatus");
            rblApprove.SelectedIndex = 0;
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
            txtCompCode.Text = res.DataResponse.COMP_NAME == "" ? "" : res.DataResponse.COMP_NAME;
            //txtFee.Text = Convert.ToString(res.DataResponse.FEES) == "" ? "" : Convert.ToString(res.DataResponse.FEES);
            txtCurrentAddress.Text = res.DataResponse.CURRENT_ADDRESS == "" ? "" : res.DataResponse.CURRENT_ADDRESS;
            txtRegisterAddress.Text = res.DataResponse.LOCAL_ADDRESS == "" ? "" : res.DataResponse.LOCAL_ADDRESS;
            //txtDateAccept.Text = Convert.ToString(res.DataResponse.AR_DATE) == "" ? "" : Convert.ToString(res.DataResponse.AR_DATE);

            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();

            PersonBiz perBiz = new PersonBiz();
            DTO.ResponseService<DTO.Registration> resPer = perBiz.getPDetailByIDCard(res.DataResponse.ID_CARD_NO);
            if (resPer.DataResponse != null)
            {
                ddlProvinceCurrentAddress.SelectedValue = resPer.DataResponse.PROVINCE_CODE;
                ddlProvinceRegisterAddress.SelectedValue = resPer.DataResponse.LOCAL_PROVINCE_CODE;
            }
            else if (resPer.DataResponse == null)
            {
                ddlProvinceCurrentAddress.SelectedValue = "99";
                ddlProvinceRegisterAddress.SelectedValue = "99";

                DTO.ResponseService<DTO.Registration> TempPer = new DTO.ResponseService<DTO.Registration>();
                TempPer.DataResponse = new DTO.Registration();
                TempPer.DataResponse.AREA_CODE = "";
                TempPer.DataResponse.TUMBON_CODE = "";

                TempPer.DataResponse.LOCAL_AREA_CODE = "";
                TempPer.DataResponse.LOCAL_TUMBON_CODE = "";
                resPer.DataResponse = TempPer.DataResponse;
            }

            var lsPC = dataCenter.GetAmpur(message, ddlProvinceCurrentAddress.SelectedValue);
            BindToDDL(ddlDistrictCurrentAddress, lsPC);
            foreach (var item in lsPC)
            {
                if (item.Id == resPer.DataResponse.AREA_CODE)
                {
                    ddlDistrictCurrentAddress.SelectedValue = resPer.DataResponse.AREA_CODE;
                }
            }

            var lsTC = dataCenter.GetTumbon(message, ddlProvinceCurrentAddress.SelectedValue, ddlDistrictCurrentAddress.SelectedValue);
            BindToDDL(ddlParishCurrentAddress, lsTC);
            foreach (var item in lsTC)
            {
                if (item.Id == resPer.DataResponse.TUMBON_CODE)
                {
                    ddlParishCurrentAddress.SelectedValue = resPer.DataResponse.TUMBON_CODE;
                }
            }

            var lsPR = dataCenter.GetAmpur(message, ddlProvinceRegisterAddress.SelectedValue);
            BindToDDL(ddlDistrictRegisterAddress, lsPR);
            foreach (var item in lsPR)
            {
                if (item.Id == resPer.DataResponse.LOCAL_AREA_CODE)
                {
                    ddlDistrictRegisterAddress.SelectedValue = resPer.DataResponse.LOCAL_AREA_CODE;
                }
            }

            var lsTR = dataCenter.GetTumbon(message, ddlProvinceRegisterAddress.SelectedValue, ddlDistrictRegisterAddress.SelectedValue);
            BindToDDL(ddlParishRegisterAddress, lsTR);
            foreach (var item in lsTR)
            {
                if (item.Id == resPer.DataResponse.LOCAL_TUMBON_CODE)
                {
                    ddlParishRegisterAddress.SelectedValue = resPer.DataResponse.LOCAL_TUMBON_CODE;
                }
            }


            UpdatePanelSearch.Update();
            var HeaderApprove = biz.GetVerifyHeadByUploadGroupNo(strGroupNumber.Text);
            if (HeaderApprove.DataResponse.APPROVED_DOC == "Y")
            {
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
            }
            if (res.DataResponse.APPROVED == "Y")
            {
                rblApprove.Enabled = false;
                rblApprove.SelectedValue = "Y";
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
            }
            else if (res.DataResponse.APPROVED == "N")
            {
                rblApprove.Enabled = false;
                rblApprove.SelectedValue = "N";
                btnSubmit.Visible = false;
                btnCancel.Visible = false;
                
            }
            else if (res.DataResponse.APPROVED == "W")
            {
                rblApprove.Enabled = true;
                rblApprove.SelectedValue = "0";
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                //rblApprove.ClearSelection();
            }
            var lsExam = biz.GetSpecialTempExamById(res.DataResponse.ID_CARD_NO.Trim());
            var lsTrain = biz.GetSpecialTempTrainById(res.DataResponse.ID_CARD_NO.Trim());
            if (lsExam.DataResponse != null)
            {
                gvExamSpecial.DataSource = lsExam.DataResponse;
                gvExamSpecial.DataBind();
            }

            if (lsTrain.DataResponse != null)
            {
                gvTrainSpecial.DataSource = lsTrain.DataResponse;
                gvTrainSpecial.DataBind();
            }
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblGroupIDNumberGv = (Label)gr.FindControl("lblGroupIDNumberGv");
            Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
            BindGridImageAccount(lblGroupIDNumberGv.Text, lblIDCardGv.Text);
            hdfgroupId.Value = lblGroupIDNumberGv.Text;
            hdfIdCard.Value = lblIDCardGv.Text;
            pnlAttach.Visible = true;
            pnlDetail.Visible = true;

        }
        #region gv3
        protected void hplViewImg_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblGroupIDNumberGv = (Label)gr.FindControl("lblGroupIDNumberGv");
            Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
            BindGridImageAccount(lblGroupIDNumberGv.Text, lblIDCardGv.Text);
            hdfgroupId.Value = lblGroupIDNumberGv.Text;
            hdfIdCard.Value = lblIDCardGv.Text;
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
            var lsCount = biz.GetLicenseVerifyImgDetail(strIDNumberGv, strIdCard, "Y", resultPage, PAGE_SIZE);
            double dblPageCount = (double)((decimal)lsCount.DataResponse.Count() / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage3.Text = Convert.ToString(TotalPages);
            var ls = biz.GetLicenseVerifyImgDetail(strIDNumberGv, strIdCard, "N", resultPage, PAGE_SIZE);

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
                    //btnPreviousGvSearch3.Visible = false;
                    //btnNextGvSearch3.Visible = false;
                    //txtNumberGvSearch3.Visible = true;
                    //lblParaPage3.Visible = true;
                    //txtTotalPage3.Visible = true;
                    //divGv3.Visible = true;
                    //btngo3.Visible = true;
                    //txtInputMaxrow3.Visible = true;
                    //lblHeadInputMaxrow3.Visible = true;
                    //lblHeadTotal3.Visible = true;
                    //lblTotalrecord3.Visible = true;
                    //lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = "0";
                    txtTotalPage3.Text = "1";
                }
                else if (TotalPages == 1)
                {
                    //txtNumberGvSearch3.Visible = true;
                    //lblParaPage3.Visible = true;
                    //txtTotalPage3.Visible = true;
                    //btnPreviousGvSearch3.Visible = false;
                    //btnNextGvSearch3.Visible = false;
                    //divGv3.Visible = true;
                    //btngo3.Visible = true;
                    //txtInputMaxrow3.Visible = true;
                    //lblHeadInputMaxrow3.Visible = true;
                    //lblHeadTotal3.Visible = true;
                    //lblTotalrecord3.Visible = true;
                    //lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = Convert.ToString(lsCount.DataResponse.Count());
                }
                else if (TotalPages > 1)
                {
                    //txtNumberGvSearch3.Visible = true;
                    //btnPreviousGvSearch3.Visible = false;
                    //lblParaPage3.Visible = true;
                    //txtTotalPage3.Visible = true;
                    //btnNextGvSearch3.Visible = true;
                    //divGv3.Visible = true;
                    //btngo3.Visible = true;
                    //txtInputMaxrow3.Visible = true;
                    //lblHeadInputMaxrow3.Visible = true;
                    //lblHeadTotal3.Visible = true;
                    //lblTotalrecord3.Visible = true;
                    //lblEndTotal3.Visible = true;
                    lblTotalrecord3.Text = Convert.ToString(lsCount.DataResponse.Count());
                }

            }
        }
        protected void btnGo3_Click(object sender, EventArgs e)
        {
            BindGridImageAccount(hdfgroupId.Value, hdfIdCard.Value);
        }

        protected void BindPage3()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow3.Text);
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var resultPage = txtNumberGvSearch3.Text.ToInt();
            var ls = biz.GetLicenseVerifyImgDetail(GroupIDNumber, IDCard, "N", resultPage, PAGE_SIZE);
            gvUpload.DataSource = ls.DataResponse;
            gvUpload.DataBind();
        }

        protected void btnPreviousGvSearch3_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch3.Text.ToInt() - 1;

            txtNumberGvSearch3.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                //txtNumberGvSearch3.Visible = true;
                //lblParaPage3.Visible = true;
                //txtTotalPage3.Visible = true;
                //btnNextGvSearch3.Visible = true;
                //btnPreviousGvSearch3.Visible = false;

                //divGv3.Visible = true;
                //btngo3.Visible = true;
                //txtInputMaxrow3.Visible = true;
                //lblHeadInputMaxrow3.Visible = true;
                //lblHeadTotal3.Visible = true;
                //lblTotalrecord3.Visible = true;
                //lblEndTotal3.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                //txtNumberGvSearch3.Visible = true;
                //lblParaPage3.Visible = true;
                //txtTotalPage3.Visible = true;
                //btnNextGvSearch3.Visible = true;
                //btnPreviousGvSearch3.Visible = true;

                //divGv3.Visible = true;
                //btngo3.Visible = true;
                //txtInputMaxrow3.Visible = true;
                //lblHeadInputMaxrow3.Visible = true;
                //lblHeadTotal3.Visible = true;
                //lblTotalrecord3.Visible = true;
                //lblEndTotal3.Visible = true;
            }
            BindPage3();
        }

        protected void btnNextGvSearch3_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch3.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage3.Text))
            {
                txtNumberGvSearch3.Text = result.ToString();
                //txtNumberGvSearch3.Visible = true;
                //lblParaPage3.Visible = true;
                //txtTotalPage3.Visible = true;
                //btnNextGvSearch3.Visible = true;
                //btnPreviousGvSearch3.Visible = true;

                //divGv3.Visible = true;
                //btngo3.Visible = true;
                //txtInputMaxrow3.Visible = true;
                //lblHeadInputMaxrow3.Visible = true;
                //lblHeadTotal3.Visible = true;
                //lblTotalrecord3.Visible = true;
                //lblEndTotal3.Visible = true;
            }
            else
            {
                txtNumberGvSearch3.Text = txtTotalPage3.Text;
                //txtNumberGvSearch3.Visible = true;
                //lblParaPage3.Visible = true;
                //txtTotalPage3.Visible = true;
                //btnNextGvSearch3.Visible = false;
                //btnPreviousGvSearch3.Visible = true;

                //divGv3.Visible = true;
                //btngo3.Visible = true;
                //txtInputMaxrow3.Visible = true;
                //lblHeadInputMaxrow3.Visible = true;
                //lblHeadTotal3.Visible = true;
                //lblTotalrecord3.Visible = true;
                //lblEndTotal3.Visible = true;

            }
            BindPage3();
        }
        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            pnlDetail.Visible = false;
            pnlAttach.Visible = false;
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                {
                    UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                    UCModalError.ShowModalError();
                    pnlSearch.Visible = false;
                }
                else
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
                    GetLicenseHeadVerify();
                    divDetail.Visible = false;
                    pnlSearch.Visible = true;
                    ShowHideExport();
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                UCModalError.ShowModalError();
            }

        }

        private void ShowHideExport()
        {
            if (gvHead.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }
        }

        private void GetLicenseHeadVerify()
        {
            gv1.Visible = true;
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
                //DataSet lsCount = new DataSet();
                var lsCount = new DTO.ResponseService<DataSet>();
                var ls = new DTO.ResponseService<DataSet>();

                if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType==DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    lsCount = biz.GetLicenseVerifyHeadByOIC(strLicenseType, dtStartDate, dtEndDate, ddlCompanyRequest.SelectedValue, "Y", resultPage, PAGE_SIZE, ddlStatusApprove.SelectedValue);
                    ls = biz.GetLicenseVerifyHeadByOIC(strLicenseType, dtStartDate, dtEndDate, ddlCompanyRequest.SelectedValue, "N", resultPage, PAGE_SIZE, ddlStatusApprove.SelectedValue);
                }
                else
                {
                    lsCount = biz.GetLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, ddlCompanyRequest.SelectedValue, "Y", resultPage, PAGE_SIZE, ddlStatusApprove.SelectedValue);
                    ls = biz.GetLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, ddlCompanyRequest.SelectedValue, "N", resultPage, PAGE_SIZE, ddlStatusApprove.SelectedValue);
                }

                DataSet ds = lsCount.DataResponse;
                DataTable dt = ds.Tables[0];
                DataSet dsResult = ls.DataResponse;
                gvHead.DataSource = dsResult;
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

                else if (dt.Rows.Count != 0)
                {
                    DataRow dr = dt.Rows[0];
                    int rowcount = Convert.ToInt32(dr["TOTAL"].ToString());
                    double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                    TotalPages = (int)Math.Ceiling(dblPageCount);
                    txtTotalPage.Text = Convert.ToString(TotalPages);

                    if (ls.IsError)
                    {
                        UCModalError.ShowMessageError = ls.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {

                        if (TotalPages == 0)
                        {


                        }
                        else if (TotalPages > 1)
                        {
                            divGv1.Visible = true;

                            txtNumberGvSearch.Visible = true;
                            lblParaPage.Visible = true;
                            txtTotalPage.Visible = true;
                            btnNextGvSearch.Visible = true;
                            btnPreviousGvSearch.Visible = false;
                            btngo.Visible = true;
                            lblTotalrecord.Text = dr["TOTAL"].ToString();
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
                        UpdatePanelSearch.Update();
                    }
                }

            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                UCModalError.ShowModalError();
            }

        }

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
            var ls = new DTO.ResponseService<DataSet>();
            if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
            {
                ls = biz.GetLicenseVerifyHeadByOIC(strLicenseType, dtStartDate, dtEndDate, ddlCompanyRequest.SelectedValue, "N", resultPage, PAGE_SIZE, ddlStatusApprove.SelectedValue);
            }
            else
            {
                ls = biz.GetLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, ddlCompanyRequest.SelectedValue, "N", resultPage, PAGE_SIZE, ddlStatusApprove.SelectedValue);
            }

            gvHead.DataSource = ls.DataResponse;
            gvHead.DataBind();
            pnlDetail.Visible = false;
            divDetail.Visible = false;
            pnlAttach.Visible = false;
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

        private void GetLicenseVerify()
        {

        }

        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndDate.Text = string.Empty;
        }

        protected void btnOverAllApprove_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.SubmitLicenseVerify>();

            if (gvDetail.Visible == true)
            {
                foreach (GridViewRow gr in gvDetail.Rows)
                {
                    if (((CheckBox)gr.FindControl("chkSelectDetail")).Checked == true)
                    {
                        var strSeqNo = (Label)gr.FindControl("lblIDNumberGv");
                        var strUploadGroupNo = (Label)gr.FindControl("lblGroupIDNumberGv");

                        data.Add(new DTO.SubmitLicenseVerify
                        {
                            SeqNo = strSeqNo.Text,
                            UploadGroupNo = strUploadGroupNo.Text,
                        });
                    }
                }
            }
            if (data.Count == 0)
            {
                UCModalError.ShowMessageError = Resources.errorVerifydoc_001;
                UCModalError.ShowModalError();
                UpdatePanelSearch.Update();
            }
            else if (data != null)
            {
                var biz = new BLL.LicenseBiz();
                var res = biz.ApproveLicenseVerify(data, "Y", base.UserProfile.Id);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.Approval;
                    UCModalSuccess.ShowModalSuccess();
                    GetLicenseHeadVerify();
                    GetLicenseDetailVerify(Session["UploadGroupNoGv"].ToString());

                }
            }


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (rblApprove.SelectedValue == "0")
            {
                UCModalError.ShowMessageError = "กรุณาให้ความเห็นชอบ";
                UCModalError.ShowModalError();
                return;
            }
            else
            {
                var data = new List<DTO.SubmitLicenseVerify>();

                if (gvDetail.Visible == true)
                {

                    data.Add(new DTO.SubmitLicenseVerify
                    {
                        SeqNo = hdfNumber.Value,
                        UploadGroupNo = hdfGroupNumber.Value,

                    });
                }

                if (data != null)
                {
                    var biz = new BLL.LicenseBiz();


                    if (rblApprove.SelectedValue == "Y")
                    {
                        flgApprove = "Y";
                        UCModalSuccess.ShowMessageSuccess = SysMessage.Approval;

                    }
                    else if (rblApprove.SelectedValue == "N")
                    {
                        flgApprove = "N";
                        UCModalSuccess.ShowMessageSuccess = SysMessage.NotApproval;

                    }

                    var res = biz.ApproveLicenseVerify(data, flgApprove, base.UserProfile.Id);

                    if (res.IsError)
                    {
                        var errorMsg = res.ErrorMsg;

                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        UCModalSuccess.ShowModalSuccess();
                        GetLicenseHeadVerify();
                        GetLicenseDetailVerify(Session["UploadGroupNoGv"].ToString());

                        pnlDetail.Visible = false;
                        pnlAttach.Visible = false;
                    }
                }
            }
        }

        private void GetLicenseDetailVerify(string groupNumber)
        {
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            //var ls = biz.GetListLicenseDetailVerify(groupNumber);
            //DataSet ds = ls.DataResponse;
            //gvDetail.DataSource = ds;
            //gvDetail.DataBind();
            BindGv2();
            ClientScript.RegisterStartupScript(GetType(), "Get_TotalChkBx", string.Format("Get_TotalChkBx({0})", gvDetail.Rows.Count), true);

        }

        //protected void chkSelectVerify_CheckedChanged(object sender, EventArgs e)
        //{
        //    int count = 0;
        //    foreach (GridViewRow gvr in gvDetail.Rows)
        //    {
        //        CheckBox chkBx = (CheckBox)gvr.FindControl("chkSelectVerify");

        //        if (chkBx != null && chkBx.Checked)
        //        {
        //            count++;
        //        }
        //    }
        //    if (count > 0)
        //    {
        //        btnOverAllApprove.Enabled = true;
        //    }
        //    else
        //    {
        //        btnOverAllApprove.Enabled = false;
        //    }
        //}

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkSelectVerify = ((CheckBox)e.Row.FindControl("chkSelectDetail"));
                Label status = ((Label)e.Row.FindControl("lblStatus"));
                Label statusName = ((Label)e.Row.FindControl("lblStatusName"));
                Label lblIDCardG = ((Label)e.Row.FindControl("lblIDCardGv"));
                Label lblGroupIDNumberGv = ((Label)e.Row.FindControl("lblGroupIDNumberGv"));
                Image imgSpecial = ((Image)e.Row.FindControl("imgSpecial"));
                CheckBox btn_chkAll = ((CheckBox)gvDetail.HeaderRow.FindControl("chkAll"));
                Label runNo = ((Label)e.Row.FindControl("lblRunNo"));
                LinkButton viewImg = ((LinkButton)e.Row.FindControl("hplViewImg"));
                LicenseBiz biz = new LicenseBiz();



                if (runNo.Text == "1")
                    btn_chkAll.Visible = false;


                var resCheck = biz.CheckSpecial(lblIDCardG.Text);
                if (resCheck.ResultMessage == true)
                {
                    imgSpecial.Visible = true;
                }
                else
                {
                    imgSpecial.Visible = false;
                }


                if (status != null && !string.IsNullOrEmpty(status.Text))
                {
                    if (status.Text == "N")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveN;
                        //btnOverAllApprove.Visible = false;
                        //btn_chkAll.Visible = false;
                        chkSelectVerify.Visible = false;
                    }
                    else if (status.Text == "Y")
                    {
                        statusName.Text = Resources.propSysMessage_LicenseApproveP;
                        chkSelectVerify.Visible = false;
                        //btnOverAllApprove.Visible = false;
                        //btn_chkAll.Visible = false;
                    }
                    else if (status.Text == "W")
                    {
                        chkSelectVerify.Visible = true;
                        //btn_chkAll.Visible = true;
                        statusName.Text = Resources.propSysMessage_LicenseApproveW;
                        //btnOverAllApprove.Visible = true;
                    }
                    else
                    {
                        statusName.Text = "";
                    }
                    

                }
                //if (gvDetail.Rows.Count>0)
                //{
                //    btnOverAllApprove.Visible = false;
                //    btn_chkAll.Visible = false;
                //}
                //else
                //{
                //    btnOverAllApprove.Visible = true;
                //    btn_chkAll.Visible = true;
                //}


                var resultPage = txtNumberGvSearch3.Text.ToInt();
                var res = biz.GetLicenseVerifyImgDetail(lblGroupIDNumberGv.Text, lblIDCardG.Text, "Y", resultPage, PAGE_SIZE);
                if (res.DataResponse.ToList().Count <= 0)
                {
                    viewImg.Text = string.Empty;
                }

                if(btn_chkAll.Visible==false)
                {
                    if (chkSelectVerify.Visible == true)
                    {
                        btn_chkAll.Visible = true;
                        btnOverAllApprove.Visible = true;
                    }
                    else
                    {
                        btn_chkAll.Visible = false;
                        btnOverAllApprove.Visible = false;
                    }
                }
            }
        }

        protected void gvUpload_PreRender(object sender, EventArgs e)
        {

        }

        protected void gvUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hplView = (LinkButton)e.Row.FindControl("hplViewDetailImg");
                Label lblFileGv = (Label)e.Row.FindControl("lblAttachFilePathGv");
                Label lblFileGvName = (Label)e.Row.FindControl("lblFileGv");
                string FileATT = lblFileGvName.Text;
                string FILENAME = FileATT.Substring(FileATT.IndexOf("\\") + 1);
                string FileNameAttach = FILENAME.Substring(FILENAME.IndexOf("\\") + 1);
                lblFileGvName.Text = FileNameAttach;
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

        private String LinkPopUp(String postData)
        {
            return String.Format("window.open('{0}?targetImage={1}','','')"
                            , UrlHelper.Resolve("/UserControl/ViewFile.aspx"), IAS.Utils.CryptoBase64.Encryption(postData));
        }

        protected void gvUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvUpload_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDetail.PageIndex = e.NewPageIndex;
            gvDetail.DataBind();
            pnlAttach.Visible = false;
        }

        //protected void gvUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
        //    Label lblIDNumberGv = (Label)gr.FindControl("lblGroupIDNumberGv");
        //    Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
        //    BLL.LicenseBiz biz = new BLL.LicenseBiz();
        //    var ls = biz.GetLicenseVerifyImgDetail(lblIDNumberGv.Text, lblIDCardGv.Text);
        //    if (ls.IsError)
        //    {
        //        UCModalError.ShowMessageError = ls.ErrorMsg;
        //        UCModalError.ShowModalError();

        //    }
        //    else
        //    {
        //        gvUpload.PageIndex = e.NewPageIndex;
        //        gvUpload.DataBind();
        //    }

        //}
        #region gv2
        protected void hplHeadViewDoc_Click(object sender, EventArgs e)
        {
            divDetail.Visible = true;

            pnlAttach.Visible = false;
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblUploadGroupNoGv = (Label)gr.FindControl("lblUploadGroupNoGv");
            Session["UploadGroupNoGv"] = lblUploadGroupNoGv.Text;
            UploadGroupNo = lblUploadGroupNoGv.Text;
            H_UploadGroupNo.Value = lblUploadGroupNoGv.Text;
            BindGv2();
        }
        protected void hplGo2_Click(object sender, EventArgs e)
        {
            BindGv2();
        }
        protected void BindGv2()
        {
            divDetail.Visible = true;
            gvDetail.Visible = true;
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

            if (rowcount == 0)
            {
                pnlAttach.Visible = false;
                pnlDetail.Visible = false;
                CheckBox Chall = (CheckBox)gvDetail.HeaderRow.FindControl("chkAll");
                Chall.Visible = false;
                txtNumberGvSearch2.Visible = true;
                lblParaPage2.Visible = true;
                txtTotalPage2.Visible = true;
                btnNextGvSearch2.Visible = false;
                btnPreviousGvSearch2.Visible = false;
                divGv2.Visible = true;
                btngo2.Visible = true;
                txtInputMaxrow2.Visible = true;
                lblHeadInputMaxrow2.Visible = true;
                lblHeadTotal2.Visible = true;
                lblTotalrecord2.Visible = true;
                lblEndTotal2.Visible = true;
                lblTotalrecord2.Text = "0";
                txtTotalPage2.Text = "1";
            }
            else
            {
                CheckBox Chall = (CheckBox)gvDetail.HeaderRow.FindControl("chkAll");
                Chall.Visible = true;

                pnlDetail.Visible = false;
                pnlAttach.Visible = false;
                if (ls.IsError)
                {
                    UCModalError.ShowMessageError = ls.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvDetail.DataSource = ls.DataResponse;
                    gvDetail.DataBind();
                    if (TotalPages > 1)
                    {
                        txtNumberGvSearch2.Visible = true;
                        lblParaPage2.Visible = true;
                        txtTotalPage2.Visible = true;
                        btnNextGvSearch2.Visible = true;
                        btnPreviousGvSearch2.Visible = false;
                        divGv2.Visible = true;
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
                        btnNextGvSearch2.Visible = false;
                        btnPreviousGvSearch2.Visible = false;
                        txtNumberGvSearch2.Visible = true;
                        lblParaPage2.Visible = true;
                        txtTotalPage2.Visible = true;
                        divGv2.Visible = true;
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

                    }
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlDetail.Visible = false;
            pnlAttach.Visible = false;
        }

        protected void gvDetail_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkSelectDetail");
                CheckBox chkBxHeader = (CheckBox)this.gvDetail.HeaderRow.FindControl("chkAll");

                chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}','gvDetail','chkSelectDetail');", chkBxHeader.ClientID);
            }
        }

        protected void btnMainCancle_Click(object sender, EventArgs e)
        {
            GetTypeDocument();
            //GetCompayByRequest();พี่ฟิว
            defaultData();
            gv1.Visible = false;
            divDetail.Visible = false;
            pnlAttach.Visible = false;
            pnlDetail.Visible = false;
            ddlCompanyRequest.SelectedIndex = 0;
            ddlStatusApprove.SelectedIndex = 0;
        }

        protected void defaultData()
        {

            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
        }

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
                    ExportBiz export = new ExportBiz();
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับที่", "RUN_NO");
                    columns.Add("เลขที่ใบคำขออนุญาต", "UPLOAD_GROUP_NO");
                    columns.Add("ชื่อไฟล์", "FILENAME");
                    columns.Add("วันที่นำส่ง", "TRAN_DATE");
                    columns.Add("รูปแบบการขอใบอนุญาต", "PETITION_TYPE_NAME");
                    columns.Add("ประเภทการขอใบอนุญาต", "LICENSE_TYPE_NAME");
                    columns.Add("รายการทั้งหมด", "LOTS");
                    columns.Add("สถานะ", "APPROVED_DOC");

                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ประเภท ",
                        ValueColumnsOne = ddlLicenseType.SelectedItem.Text,
                        NameColumnsTwo = "ผู้ยื่นคำร้อง ",
                        ValueColumnsTwo = ddlCompanyRequest.SelectedItem.Text
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
                    if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                    {
                        DateTime dtStartDate = Convert.ToDateTime(GetCrit(txtStartDate.Text == "" ? "" : txtStartDate.Text));
                        DateTime dtEndDate = Convert.ToDateTime(GetCrit(txtEndDate.Text == "" ? "" : txtEndDate.Text));

                        var ls = new DTO.ResponseService<DataSet>();
                        if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
                        {
                            ls = biz.GetLicenseVerifyHeadByOIC(strLicenseType, dtStartDate, dtEndDate, ddlCompanyRequest.SelectedValue, "N", 1, base.EXCEL_SIZE_Key, ddlStatusApprove.SelectedValue);
                        }
                        else
                        {
                            ls = biz.GetLicenseVerifyHead(strLicenseType, dtStartDate, dtEndDate, base.UserProfile.CompCode, ddlCompanyRequest.SelectedValue, "N", 1, base.EXCEL_SIZE_Key, ddlStatusApprove.SelectedValue);
                        }
                        export.CreateExcel(ls.DataResponse.Tables[0], columns, header, base.UserProfile);

                    }
                }
            }
            catch
            {

            }
        }
        public override void VerifyRenderingInServerForm(Control control) { }

        protected void btnexport2_Click(object sender, ImageClickEventArgs e)
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
                    BLL.LicenseBiz biz = new BLL.LicenseBiz();
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับที่", "RUN_NO");
                    columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                    columns.Add("หมายเลขกลุ่ม", "UPLOAD_GROUP_NO");
                    columns.Add("วันที่พิจารณา", "APPROVED_DATE");
                    columns.Add("สถานะ", "APPROVED");
                    columns.Add("ผู้พิจารณา", "NAME");

                    var ls = biz.GetListLicenseDetailVerify(H_UploadGroupNo.Value, "N", 1, base.EXCEL_SIZE_Key);
                    ExportBiz export = new ExportBiz();
                    export.CreateExcel(ls.DataResponse, columns);
                }
            }
            catch { }
        }

        protected void gvHead_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvDetail_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        string checkHead = string.Empty;
        protected void gvDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            LicenseBiz biz = new LicenseBiz();
            if (cmdName.Equals("Select"))
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = gvDetail.Rows[index];

                //Label PetitionCode = (Label)row.FindControl("lblPetitionTypeCode");
                //Label LicenseCode = (Label)row.FindControl("lblLicenseTypeCode");
                Label RenewTime = (Label)row.FindControl("lblRenewTime");
                Label IdCard = (Label)row.FindControl("lblIDCardGv");
                Label lblLicenseno = (Label)row.FindControl("lblLicenseNo");
                Label dateApprove = (Label)row.FindControl("lblApproveDateTime");
                
                if (dateApprove.Text != "" )
                {
                    txtdatetimeApprove.Text = String.Format("{0:dd/MM/yyyy HH:mm}", dateApprove.Text) + " น.";
                }
                else
                {
                    txtdatetimeApprove.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " น.";
                }
                this.LicenseTypeCode = Session["LicenseCode"].ToString();
                this.PettionTypeCode = Session["PetitionCode"].ToString();
                this.CurrentLicenseRenewTime = RenewTime.Text == "" ? "0" : RenewTime.Text;
                this.CheckApprove = "ApproveL";
                this.Mode = DTO.LicensePropMode.General.GetEnumValue().ToString();

                #region gvGeneral
                DTO.ResponseService<DTO.ValidateLicense[]> result = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 1);
                
                // this.GridviewGeneral = result.DataResponse.ToList();
                if (result.DataResponse.Count() > 0)
                {
                    //Create Head Genaral
                    gvGeneral.DataSource = result.DataResponse;
                    gvGeneral.DataBind();
                    gvGeneral.Visible = true;
                    if (gvGeneral.Controls.Count > 0)
                    {
                        AddSuperHeader(gvGeneral);
                        checkHead = "T";
                    }
                    else
                    {
                        //AddSuperHeader(gvTrainResult);
                    }

                }
                else
                {
                    gvGeneral.Visible = false;
                }
                
                #endregion

                #region gvExamResult
                DTO.ResponseService<DTO.ValidateLicense[]> result2 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime),2);
                if (result2.DataResponse.Count() > 0)
                {
                    gvExamResult.DataSource = result2.DataResponse;
                    gvExamResult.DataBind();
                    gvExamResult.Visible = true;
                    // this.GridviewExamResult = result2.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvGeneral.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvExamResult);
                    }
                    else
                    {
                        if (checkHead != "T")
                        {
                            AddSuperHeader(gvExamResult);
                            checkHead = "T";
                        }
                    }
                }
                else
                {
                    gvExamResult.Visible = false;
                }
                

                #endregion

                #region gvEducation
                DTO.ResponseService<DTO.ValidateLicense[]> result3 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime),3);
                if (result3.DataResponse.Count() > 0)
                {
                    gvEducation.DataSource = result3.DataResponse;
                    gvEducation.DataBind();
                    gvEducation.Visible = true;
                    // this.GridviewEducation = result3.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvExamResult.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvEducation);
                    }
                    else
                    {
                        if (checkHead != "T")
                        {
                            AddSuperHeader(gvEducation);
                            checkHead = "T";
                        }
                    }
                }
                else
                {
                    gvEducation.Visible = false;
                }
                
                #endregion

                #region gvTrainResult
                DTO.ResponseService<DTO.ValidateLicense[]> result4 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 4);
                if (result4.DataResponse.Count() > 0)
                {
                    gvTrainResult.DataSource = result4.DataResponse;
                    gvTrainResult.DataBind();
                    gvTrainResult.Visible = true;
                    //  this.GridviewTrainResult = result4.DataResponse.ToList();

                    //Create Head Genaral
                    if (gvEducation.Controls.Count > 0)
                    {
                        //AddSuperHeader(gvTrainResult);
                    }
                    else
                    {
                        if (checkHead != "T")
                        {
                            AddSuperHeader(gvTrainResult);
                            checkHead = "T";
                        }
                    }
                }
                else
                {
                    gvTrainResult.Visible = false;
                }
                
                #endregion

                #region gvOther
               // DTO.ResponseService<DTO.ValidateLicense[]> result5 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 5);
                //if (result5.DataResponse.Count() > 0)
                //{
                //    gvOther.DataSource = result5.DataResponse;
                //    gvOther.DataBind();
                //    gvOther.Visible = true;
                //    //  this.GridviewOther = result5.DataResponse.ToList();

                //    //Create Head Genaral
                //    if (gvTrainResult.Controls.Count > 0)
                //    {
                //        //AddSuperHeader(gvOther);
                //    }
                //    else
                //    {
                //        if (checkHead != "T")
                //        {
                //            AddSuperHeader(gvOther);
                //            checkHead = "T";
                //        }
                //    }
                //}
                //else
                //{
                //    gvOther.Visible = false;
                //}
                
                #endregion

                pnlValidateprop.Visible = true;
                UpdatePanelSearch.Update();
                UpdatePanel1.Update();
                
            }
        }
        //private void chkValidatePropLicenseApp(GridView gv, string mode)
        //{
        //    LicenseBiz biz = new LicenseBiz();

        //    foreach (GridViewRow row in gv.Rows)
        //    {
        //        Label lblID = (Label)row.FindControl("lblID");
        //        Label lblItemName = (Label)row.FindControl("lblItemName");
        //        Image impCorrect = (Image)row.FindControl("impCorrect");
        //        Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                
        //        if (impCorrect.Visible == false )
        //        {
                    
        //            impCorrect.Visible = true;
        //            impdisCorrect.Visible = false;

        //        }
        //        //Single License
        //        if (mode.Equals(DTO.LicensePropMode.General.GetEnumValue().ToString()))
        //        {
        //            // var result = text.Where(l => l.Equals(lblItemName.Text.Trim())).FirstOrDefault();

        //            if (gv.ID.Equals("gvGeneral"))
        //            {
        //                impCorrect.Visible = true;
        //                impdisCorrect.Visible = true;
        //                //1	บรรลุนิติภาวะ
        //                //6	มีภูมิลำเนาในประเทศไทย
        //                //7	ไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ
        //                //8	ไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุก ในความผิดเกี่ยวกับทรัพย์ที่กระทำโดยทุจริต เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต
        //                //9	ไม่เป็นบุคคลล้มละลาย
        //                //11 ไม่เป็นตัวแทนประกันชีวิต
        //                //13 ไม่เคยถูกเพิกถอนใบอนุญาตเป็นตัวแทนประกันชีวิต หรือใบอนุญาตเป็นนายหน้าประกันชีวิต ระยะเวลาห้าปีก่อนวันขอรับใบอนุญาต
        //                switch (lblID.Text)
        //                {

        //                    case "1":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "6":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "7":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "8":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "9":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "10":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "11":
        //                        if (this.LicenseTypeCode.Equals("03"))
        //                        {

        //                            impCorrect.Visible = true;
        //                            impdisCorrect.Visible = false;
        //                        }


        //                        else if (this.LicenseTypeCode.Equals("04"))
        //                        {

        //                            impCorrect.Visible = true;
        //                            impdisCorrect.Visible = false;
        //                        }
        //                        else
        //                        {
        //                            impdisCorrect.Visible = true;
        //                            impCorrect.Visible = false;
        //                        }


        //                        break;
        //                    case "12":
        //                        if (this.LicenseTypeCode.Equals("03"))
        //                        {

        //                            impCorrect.Visible = true;
        //                            impdisCorrect.Visible = false;
        //                        }


        //                        else if (this.LicenseTypeCode.Equals("04"))
        //                        {


        //                            impCorrect.Visible = true;
        //                            impdisCorrect.Visible = false;
        //                        }
        //                        else
        //                        {
        //                            impdisCorrect.Visible = true;
        //                            impCorrect.Visible = false;
        //                        }


        //                        break;
        //                    case "13":
        //                        if (this.LicenseTypeCode != " ")
        //                        {

        //                            impCorrect.Visible = true;
        //                            impdisCorrect.Visible = false;
        //                        }

        //                        else
        //                        {
        //                            impCorrect.Visible = true;
        //                            impdisCorrect.Visible = false;
        //                        }
        //                        break;
        //                    case "15":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;

        //                        break;
        //                    case "16":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;

        //                        break;
        //                    case "19":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;


        //                        break;
        //                    case "22":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;


        //                        break;
        //                }
        //                UpdatePanel1.Update();

        //            }
        //            else if (gv.ID.Equals("gvExamResult"))
        //            {
        //                //2	ผลสอบ
        //                impCorrect.Visible = true;
        //                impdisCorrect.Visible = true;
        //                switch (lblID.Text)
        //                {
        //                    case "2":

        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;

        //                        break;
        //                }
        //                UpdatePanel1.Update();
        //            }
        //            else if (gv.ID.Equals("gvEducation"))
        //            {
        //                //3	สำเร็จการศึกษาไม่ต่ำกว่าปริญญาตรี วิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต
        //                impCorrect.Visible = true;
        //                impdisCorrect.Visible = true;
        //                switch (lblID.Text)
        //                {
        //                    case "3":
        //                        impdisCorrect.Visible = true;
        //                        impCorrect.Visible = false;
        //                        break;
        //                    case "17":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                }
        //                UpdatePanel1.Update();
        //            }
        //            else if (gv.ID.Equals("gvTrainResult"))
        //            {
        //                //4	ผลอบรม
        //                impCorrect.Visible = true;
        //                impdisCorrect.Visible = true;
        //                switch (lblID.Text)
        //                {
        //                    case "4":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                }
        //                UpdatePanel1.Update();
        //            }
        //            else if (gv.ID.Equals("gvOther"))
        //            {
        //                //5	    รูปถ่าย
        //                //14	มีการชำระค่าธรรมเนียมค่าขอรับใบอนุญาต
        //                //18	มีการชำระค่าธรรมเนียมค่ขอต่ออายุใบอนุญาต
        //                impCorrect.Visible = true;
        //                impdisCorrect.Visible = true;
        //                switch (lblID.Text)
        //                {
        //                    case "5":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "14":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                    case "18":
        //                        impCorrect.Visible = true;
        //                        impdisCorrect.Visible = false;
        //                        break;
        //                }
        //                UpdatePanel1.Update();

        //            }
        //        }


        //    }

        //}
       
        private static void AddSuperHeader(GridView gridView)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            myNewRow.Cells.Add(MakeCell("ลำดับที่", 1));
            myNewRow.Cells.Add(MakeCell("เงื่อนไขการตรวจสอบ", 1));
            myNewRow.Cells.Add(MakeCell("ตรวจสอบผ่านระบบ", 1));
            myNewRow.Cells.Add(MakeCell("หมายเหตุ", 1));

            myTable.Rows.AddAt(0, myNewRow);

        }
        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { Text = text ?? string.Empty, ColumnSpan = span, HorizontalAlign = HorizontalAlign.Center };
        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manualy onece the header creater
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;


                //var myGridView = (GridView)sender;

            }
        }

        protected void Head_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manualy onece the header creater
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;


            }
        }

        //string PetitionCode;
        //string LicenseCode;
        protected void gvHead_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string cmdName = e.CommandName;

            if (cmdName.Equals("Select"))
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                GridViewRow row = gvHead.Rows[index];


                Label PetitionCode1 = (Label)row.FindControl("lblPetitionTypeCode");
                Label LicenseCode1 = (Label)row.FindControl("lblLicenseTypeCode");
                Session["PetitionCode"] = PetitionCode1.Text;
                Session["LicenseCode"] = LicenseCode1.Text;
            }
        }
    }
}


