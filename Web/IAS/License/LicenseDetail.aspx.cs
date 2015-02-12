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
using AjaxControlToolkit;
using System.Text;
using IAS.Properties;

namespace IAS.License
{
    public partial class LicenseDetail : basepage
    {

        #region Public Param & Session
        public string IdCardNo
        {
            get
            {
                return (string)Session["IdCardNo"];
            }
            set
            {
                Session["IdCardNo"] = value;
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

        public MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as MasterPage.Site1);
            }

        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            txtstartDate.Attributes.Add("readonly", "true");
            txttoDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                DefaultData();
            }
        }

        #endregion

        #region Private & Public Func

        #region Visible Tab by Nattapong @25-11-56
        protected void FixTabPanelVisible(TabContainer tabcontainer)
        {
            foreach (TabPanel tp in tabcontainer.Tabs)
            {
                if (tp.Visible == false || Convert.ToBoolean(ViewState[tp.UniqueID + "_Display"] ?? true) == false)
                {
                    ViewState[tp.UniqueID + "_Display"] = false;
                    tp.Visible = true;
                    DisableTab(tabcontainer, tabcontainer.Tabs.IndexOf(tp));
                }
            }
            StringBuilder fixScript = new StringBuilder();
            fixScript.Append("function DisableTab(container, index) {$get(container.get_tabs()[index].get_id() + \"_tab\").style.display = \"none\";}");
            fixScript.Append("function EnableTab(container, index) {$get(container.get_tabs()[index].get_id() + \"_tab\").style.display = \"block\";}");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "FixScriptReg", fixScript.ToString(), true);
        }

        protected void EnableTab(TabContainer container, int index)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EnableTabFun" + index,
                "Sys.Application.add_load(function () {EnableTab($find('" + container.ClientID + "')," + index + ");});", true);
        }

        protected void DisableTab(TabContainer container, int index)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DisableTabFun" + index,
                "Sys.Application.add_load(function () {DisableTab($find('" + container.ClientID + "')," + index + ");});", true);
        }
        #endregion

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void DefaultData() //code เดิมไม่ได้แก้ แต่แค่จับแยกออกมา มิ้ว
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtstartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtstartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txttoDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txttoDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtLicenseNumber.Text = string.Empty;

            if (base.UserRegType == DTO.RegistrationType.General)
            {
                PnlSearch.Visible = false;
                BindDataSingle();
                CheckRows();
            }
            else
            {
                PnlSearch.Visible = true;
            }

            GetLicenseType();
        }



        private void CheckRows()
        {
            if (gvSearch.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }
        }

        private void GetLicenseType()
        {
            //BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //var ls = biz.GetLicenseType("ทั้งหมด");

            //BindToDDL(ddlLicenseType, ls.DataResponse.ToList());
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByCompCode(base.UserProfile.CompCode);
            BindToDDL(ddlLicenseType, ls.DataResponse.ToList());
            ddlLicenseType.Items.Insert(0, new ListItem("ทั้งหมด", "0"));

        }

        private void BindDataInGv()
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


            var biz = new BLL.LicenseBiz();
            var resCount = new DTO.ResponseService<DataSet>();
            var res = new DTO.ResponseService<DataSet>();
            if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
            {

                resCount = biz.GetListLicenseDetailAdminByCriteria(txtLicenseNumber.Text
                  , ddlLicenseType.SelectedValue
                  , Convert.ToDateTime(txtstartDate.Text)
                  , Convert.ToDateTime(txttoDate.Text)
                  , ""
                  , ""
                  , base.UserProfile
                  , resultPage
                  , PAGE_SIZE, "Y");

                res = biz.GetListLicenseDetailAdminByCriteria(txtLicenseNumber.Text
                            , ddlLicenseType.SelectedValue
                            , Convert.ToDateTime(txtstartDate.Text)
                            , Convert.ToDateTime(txttoDate.Text)
                            , ""
                            , ""
                            , base.UserProfile
                            , resultPage
                            , PAGE_SIZE, "N");

            }
            else
            {
                resCount = biz.GetListLicenseDetailByCriteria(txtLicenseNumber.Text
                            , ddlLicenseType.SelectedValue
                            , Convert.ToDateTime(txtstartDate.Text)
                            , Convert.ToDateTime(txttoDate.Text)
                            , ""
                            , ""
                            , base.UserProfile
                            , resultPage
                            , PAGE_SIZE, "Y");

                res = biz.GetListLicenseDetailByCriteria(txtLicenseNumber.Text
                            , ddlLicenseType.SelectedValue
                            , Convert.ToDateTime(txtstartDate.Text)
                            , Convert.ToDateTime(txttoDate.Text)
                            , ""
                            , ""
                            , base.UserProfile
                            , resultPage
                            , PAGE_SIZE, "N");
            }
            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage.Text = Convert.ToString(TotalPages);

            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();
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
                else if (TotalPages == 1)
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
                else if (TotalPages == 0)
                {
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    btngo.Visible = true;
                    lblTotalrecord.Text = "0";
                    txtTotalPage.Text = "1";
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                }
                VisibleButton();
                divDetail.Visible = true;
                UpdatePanelSearch.Update();



            }
        }


        private void BindDataSingle()
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

            var resCount = new DTO.ResponseService<DataSet>();
            var res = new DTO.ResponseService<DataSet>();

            var biz = new BLL.LicenseBiz();






            resCount = biz.GetListLicenseDetailByPersonal("", "", null, null, "", "", base.UserProfile, resultPage, PAGE_SIZE, true);

            res = biz.GetListLicenseDetailByPersonal("", "", null, null, "", "", base.UserProfile, resultPage, PAGE_SIZE, false);


            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow dr = dt.Rows[0];
            int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage.Text = Convert.ToString(TotalPages);


            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();

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
                else if (TotalPages == 1)
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
                else if (TotalPages == 0)
                {
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    btngo.Visible = true;
                    lblTotalrecord.Text = "0";
                    txtTotalPage.Text = "1";
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                }


                divDetail.Visible = true;

                UpdatePanelSearch.Update();
            }
        }






        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            var biz = new BLL.LicenseBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            var res = new DTO.ResponseService<DataSet>();

            if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
            {
                res = biz.GetListLicenseDetailAdminByCriteria(txtLicenseNumber.Text
                                  , ddlLicenseType.SelectedValue
                                  , Convert.ToDateTime(txtstartDate.Text)
                                  , Convert.ToDateTime(txttoDate.Text)
                                  , ""
                                  , ""
                                  , base.UserProfile
                                  , resultPage
                                  , PAGE_SIZE, "N");
            }

            else
            {
                if (base.UserRegType == DTO.RegistrationType.General)
                {
                    res = biz.GetListLicenseDetailByPersonal("", "", null, null, "", "", base.UserProfile, resultPage, PAGE_SIZE, false);
                }
                else
                {

                    res = biz.GetListLicenseDetailByCriteria(txtLicenseNumber.Text
                                            , ddlLicenseType.SelectedValue
                                            , Convert.ToDateTime(txtstartDate.Text)
                                            , Convert.ToDateTime(txttoDate.Text)
                                            , ""
                                            , ""
                                            , base.UserProfile
                                            , resultPage
                                            , PAGE_SIZE, "N");

                }
            }



            gvSearch.DataSource = res.DataResponse;
            gvSearch.DataBind();

        }

        private void VisibleButton()
        {
            //btnPreviousGvSearch.Visible = true;
            //txtNumberGvSearch.Visible = true;
            //btnNextGvSearch.Visible = true;
        }

        private void FirstTabDataBind(object sender, EventArgs e)
        {
            LicenseBiz biz = new LicenseBiz();
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var idCardNo = (Label)gr.FindControl("lblIDNumberGv");
            Session["idCardNo"] = idCardNo.Text;
            var IDCard = Session["idCardNo"].ToString();

            var res = biz.GetPersonalHistoryByIdCard(IDCard);
            if (res.IsError)
            {
                //UCModalError.ShowMessageError = res.ErrorMsg;
                //UCModalError.ShowModalError();
            }
            else
            {
                DTO.PersonalHistory person = res.DataResponse;
                if (res.DataResponse != null)
                {
                    if (!string.IsNullOrEmpty(person.FLNAME))
                    {
                        this.ucLicenseDetail.TxtName.Text = person.FLNAME;
                        this.ucLicenseDetail.TxtName.DataBind();
                    }
                    if (!string.IsNullOrEmpty(person.ID_CARD_NO))
                    {
                        this.ucLicenseDetail.TxtIDCard.Text = person.ID_CARD_NO;
                        this.ucLicenseDetail.TxtIDCard.DataBind();
                    }
                }
            }

        }
        #endregion

        #region UI Function
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (base.UserRegType == DTO.RegistrationType.General)
            {
                BindDataSingle();
                CheckRows();
            }
            else
            {


                if (!string.IsNullOrEmpty(txtstartDate.Text) && !string.IsNullOrEmpty(txttoDate.Text))
                {
                    if (Convert.ToDateTime(txtstartDate.Text) > Convert.ToDateTime(txttoDate.Text))
                    {
                        UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                        UCModalError.ShowModalError();

                        divDetail.Visible = false;
                    }
                    else
                    {
                        BindDataInGv();
                        //DisableTab();
                        CheckRows();
                    }

                }
                else
                {
                    UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                    UCModalError.ShowModalError();
                }
            }
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            //Get IDCard By Selected Row
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var idCardNo = (Label)gr.FindControl("lblIDNumberGv");
            if (idCardNo != null)
            {
                this.ucLicenseDetail.FirstTabDataBind(idCardNo.Text);
                this.ucLicenseDetail.InitTab(sender, e);
                this.ucLicenseDetail.UpdateTab.Update();
            }
            else
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorLicenseDetail_001;
                this.MasterSite.ModelError.ShowModalError();
                return;
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

        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = ((Label)e.Row.FindControl("lblStatus"));
                Label statusName = ((Label)e.Row.FindControl("lblStatusName"));

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
                else
                {
                    statusName.Text = Resources.propSysMessage_LicenseApproveW;

                }


            }
        }

        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtstartDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txttoDate.Text = string.Empty;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultData();
            divDetail.Visible = false;
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
                    columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                    columns.Add("ชื่อ", "NAMES");
                    columns.Add("นามสกุล", "LASTNAME");
                    columns.Add("ประเภทคำขอ", "PETITION_NAME");
                    columns.Add("ประเภทใบอนุญาต", "LICENSE_TYPE_NAME");
                    columns.Add("เลขที่อนุญาต", "LICENSE_NO");
                    columns.Add("วันที่อนุญาต", "LICENSE_DATE");
                    columns.Add("วันที่หมดอายุ", "LICENSE_EXPIRE_DATE");
                    columns.Add("สถานะ", "APPROVED");

                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "เลขที่อนุญาต ",
                        ValueColumnsOne = txtLicenseNumber.Text,
                        NameColumnsTwo = "ประเภทใบอนุญาต ",
                        ValueColumnsTwo = ddlLicenseType.SelectedItem.Text
                    });
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ตั้งแต่วันที่ ",
                        ValueColumnsOne = txtstartDate.Text,
                        NameColumnsTwo = "ถึงวันที่ ",
                        ValueColumnsTwo = txttoDate.Text
                    });



                    if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                    {

                        var biz = new BLL.LicenseBiz();
                        var res = new DTO.ResponseService<DataSet>();
                        res = biz.GetListLicenseDetailAdminByCriteria(txtLicenseNumber.Text
                                    , ddlLicenseType.SelectedValue
                                    , Convert.ToDateTime(txtstartDate.Text)
                                    , Convert.ToDateTime(txttoDate.Text)
                                    , ""
                                    , ""
                                    , base.UserProfile
                                    , 1
                                    , base.EXCEL_SIZE_Key, "N");

                        export.CreateExcel(res.DataResponse.Tables[0], columns, header, base.UserProfile);

                    }
                    else
                    {
                        var biz = new BLL.LicenseBiz();
                        var res = new DTO.ResponseService<DataSet>();
                        res = biz.GetListLicenseDetailByCriteria(txtLicenseNumber.Text
                                       , ddlLicenseType.SelectedValue
                                       , Convert.ToDateTime(txtstartDate.Text)
                                       , Convert.ToDateTime(txttoDate.Text)
                                       , ""
                                       , ""
                                       , base.UserProfile
                                       , 1
                                       , base.EXCEL_SIZE_Key, "N");
                        //export.CreateExcel(res.DataResponse,columns);
                        export.CreateExcel(res.DataResponse.Tables[0], columns, header, base.UserProfile);
                    }
                }
            }
            catch { }


        }
        #endregion

        public override void VerifyRenderingInServerForm(Control control) { }

    }
}
