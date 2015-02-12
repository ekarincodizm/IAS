using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data;
using IAS.UserControl;
using IAS.DTO;
using IAS.Properties;
using IAS.Utils;


namespace IAS.Setting
{
    public partial class SettingAttach : basepage
    {
        #region Session
        AjaxControlToolkit.TabContainer tbcDynamic;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CheckLoadTabData();
            }
            if (TabSetting.ActiveTabIndex == 1 && !Page.IsPostBack)
            {
                CreateTab();
                //dvSubmitConfigLicense.Style.Add("display", "none");
            }
        }
        #endregion

        #region Public & Private Function

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void CheckLoadTabData()
        {
            if (TabSetting.ActiveTabIndex == 0)
            {
                BindDataRegistationTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
            else if (TabSetting.ActiveTabIndex == 1)
            {
                BindDataLicenseTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
            else if (TabSetting.ActiveTabIndex == 2)
            {
                BindDataInserDocumentTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
            else if (TabSetting.ActiveTabIndex == 3)
            {
                BindDataApplicantDetailTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
        }

        private void CreateTab()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetConfigPetitionLicenseType("");
            tbcDynamic = new AjaxControlToolkit.TabContainer();
            tbcDynamic.CssClass = "ajax_tabs";

            //var tmp = new IEnumerable<DTO.ConfigDocument>();
            for (int i = 1; i < res.DataResponse.Count(); i++)
            {
                TabPanel tbpnlProcessCategory = new TabPanel();
                tbpnlProcessCategory.HeaderText = res.DataResponse[i].Name;
                tbpnlProcessCategory.ID = "Tab" + i.ToString();

                var ls = biz.GetDocumentLicenseConfigByPetitionType(res.DataResponse[i].Id);
                IEnumerable<DTO.ConfigDocument> configs = ls.DataResponse;

                var tmp = configs.GroupBy(c => c.LICENSE_TYPE_CODE);
                Session["ConfigDocuments"] = tmp.ToList();
                Class.CustomConfigTable configTagle = new Class.CustomConfigTable(res.DataResponse[i].Id);

                configTagle.LinkButtonDelete_Click += new EventHandler(LinkButtonDelete_Click);
                tbpnlProcessCategory.Controls.Add(configTagle);

                tbcDynamic.Tabs.Add(tbpnlProcessCategory);
                TabSettingLicense.Controls.Add(tbcDynamic);

            }

            dvSubmitConfigLicense.Style.Add("display", "block");
        }

        private BoundField GenColumn(String header, String fileName, String SortBy, String Class, Int32 width)
        {
            BoundField column = new BoundField();
            column.HeaderText = header;
            column.DataField = fileName;
            column.SortExpression = SortBy;
            column.HeaderStyle.CssClass = Class;
            column.ItemStyle.Width = Unit.Percentage(width);
            return column;
        }

        private void BindDataRegistationTab()
        {
            GetConfigDocumentGrid();
            GetMemberType();
            GetDocumentType();
        }

        private void BindDataLicenseTab()
        {
            GetDocumentTypeLicense();
            GetLicenseType();
            GetPetitionType();
        }

        private void BindDataInserDocumentTab()
        {
            GetDocumentTypeGrid();
        }

        private void BindDataApplicantDetailTab()
        {
            GetApplicantDetailtGrid();
            GetApplicantDetailMemberType();
            GetApplicantDetailDocumentType();
        }

        private void GetDocumentType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(SysMessage.DefaultSelecting);
            BindToDDL(ddlDocumentType, ls.ToList());
        }

        private void GetApplicantDetailDocumentType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(SysMessage.DefaultSelecting);
            BindToDDL(ddlApplicantDocumentType, ls.ToList());
        }

        private void GetDocumentTypeLicense()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(SysMessage.DefaultSelecting);
            BindToDDL(ddlLicenseDocumentType, ls.ToList());
        }

        private void GetMemberType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberType(SysMessage.DefaultSelecting, true);
            ls.RemoveAt(4);
            BindToDDL(ddlMemberType, ls.ToList());
        }

        private void GetApplicantDetailMemberType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberType(SysMessage.DefaultSelecting, true);
            ls.RemoveAt(4);
            BindToDDL(ddlApplicantMemberType, ls.ToList());
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetConfigLicenseType(SysMessage.DefaultSelecting);
            BindToDDL(ddlLicenseType, ls.DataResponse.ToList());
        }

        private void GetPetitionType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetConfigPetitionLicenseType(SysMessage.DefaultSelecting);


            List<DTO.DataItem> newls = new List<DTO.DataItem>();
            int i = 0;

            foreach (DTO.DataItem item in ls.DataResponse)
            {
                if (item.Id != "01")
                {
                    if (i == 0)
                    {
                        i = i + 1;
                        newls.Add(item);
                        continue;
                    }
                    //item.Name = item.Name.Substring(3);
                    newls.Add(item);
                }

            }



            BindToDDL(ddlPetitionType, newls);
        }

        /// <summary>
        /// GetData : ตั้งค่าเอกสารสมาชิก 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        private void GetConfigDocumentGrid()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetDocumentConfigApproveMember();
            gvConfigDocument.DataSource = res.DataResponse;
            gvConfigDocument.DataBind();
        }

        /// <summary>
        /// GetData : ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        private void GetApplicantDetailtGrid()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetDocumentConfigApproveMemberByApplicant();
            gvApplicantDetail.DataSource = res.DataResponse;
            gvApplicantDetail.DataBind();
        }

        private void GetDocumentTypeGrid()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetDocumentType("");
            gvDocumentType.DataSource = res;
            gvDocumentType.DataBind();
        }

        private void ClearDocumentTypeValue()
        {
            txtDocumentName.Text = string.Empty;
        }

        private void ClearConfigDocumentValue()
        {
            ddlDocumentType.SelectedIndex = 0;
            ddlMemberType.SelectedIndex = 0;
        }

        private void GetDefaultSetting()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetConfigPetitionLicenseType("");
            tbcDynamic = new AjaxControlToolkit.TabContainer();
            tbcDynamic.CssClass = "ajax_tabs";

            //var tmp = new IEnumerable<DTO.ConfigDocument>();
            for (int i = 0; i < res.DataResponse.Count(); i++)
            {
                TabPanel tbpnlProcessCategory = new TabPanel();
                tbpnlProcessCategory.HeaderText = res.DataResponse[i].Name;
                tbpnlProcessCategory.ID = "Tab" + i.ToString();

                var ls = biz.GetDocumentLicenseConfigByPetitionType(res.DataResponse[i].Id);
                IEnumerable<DTO.ConfigDocument> configs = ls.DataResponse;

                var tmp = configs.GroupBy(c => c.LICENSE_TYPE_CODE);
                Session["ConfigDocuments"] = tmp.ToList();
                Class.CustomConfigTable configTagle = new Class.CustomConfigTable(res.DataResponse[i].Id);

                configTagle.LinkButtonDelete_Click += new EventHandler(LinkButtonDelete_Click);
                tbpnlProcessCategory.Controls.Add(configTagle);

                tbcDynamic.Tabs.Add(tbpnlProcessCategory);
                TabSettingLicense.Controls.Add(tbcDynamic);
            }
        }
        #endregion

        #region UI Function

        /// <summary>
        /// AddNew : ตั้งค่าเอกสารสมาชิก
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void btnInsertConfigDocument_Click(object sender, EventArgs e)
        {
            if (ddlDocumentType.SelectedIndex > 0 && ddlMemberType.SelectedIndex > 0)
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                DTO.ConfigDocument doc = new DTO.ConfigDocument();
                //string funcID = DTO.DocFunction.REGISTER_FUNCTION.GetEnumValue().ToString();
                var detail = biz.GetConfigDocumentByDocumentCode(DTO.DocFunction.REGISTER_FUNCTION.GetEnumValue().ToString(), ddlDocumentType.SelectedValue, ddlMemberType.SelectedValue);
                if (detail.DataResponse != null)
                {
                    UCModalError.ShowMessageError = SysMessage.DupConfigSettingDocument;
                    UCModalError.ShowModalError();
                }
                else
                {
                    doc.FUNCTION_ID = "40";
                    doc.MEMBER_CODE = ddlMemberType.SelectedValue;
                    doc.DOCUMENT_CODE = ddlDocumentType.SelectedValue;
                    doc.DOCUMENT_REQUIRE = "N";
                    doc.STATUS = "A";
                    var res = biz.InsertConfigDocument(doc, base.UserProfile);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
                        UCModalSuccess.ShowModalSuccess();
                        GetConfigDocumentGrid();
                    }
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
                UCModalError.ShowModalError();
            }
        }

        /// <summary>
        /// AddNew : ตั้งค่าเอกสารขอรับใบอนุญาต
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void btnInsertConfigLicense_Click(object sender, EventArgs e)
        {
            if (ddlPetitionType.SelectedIndex > 0 && ddlLicenseType.SelectedIndex > 0 && ddlLicenseDocumentType.SelectedIndex > 0)
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                DTO.ConfigDocument doc = new DTO.ConfigDocument();
                var detail = biz.GetConfigDocumentLicense(ddlPetitionType.SelectedValue, ddlLicenseType.SelectedValue, ddlLicenseDocumentType.SelectedValue);
                if (detail.DataResponse != null)
                {
                    UCModalError.ShowMessageError = SysMessage.DupConfigSettingDocument;
                    UCModalError.ShowModalError();
                    UpdatePanelGrid.Update();
                }
                else
                {
                    doc.FUNCTION_ID = "41";
                    doc.PETITION_TYPE_CODE = ddlPetitionType.SelectedValue;
                    doc.LICENSE_TYPE_CODE = ddlLicenseType.SelectedValue;
                    doc.DOCUMENT_CODE = ddlLicenseDocumentType.SelectedValue;
                    doc.CREATED_BY = base.UserProfile.Name;
                    doc.CREATED_DATE = DateTime.Today.ToString();
                    doc.UPDATED_DATE = DateTime.Today.ToString();
                    doc.UPDATED_BY = base.UserProfile.Name;
                    if (chkDocumentLicense.Checked == true)
                    {
                        doc.DOCUMENT_REQUIRE = "Y";
                    }
                    else
                    {
                        doc.DOCUMENT_REQUIRE = "N";
                    }
                    doc.STATUS = "A";
                    var res = biz.InsertConfigDocument(doc, base.UserProfile);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
                        UCModalSuccess.ShowModalSuccess();
                        UpdatePanelGrid.Update();
                    }
                }
            }
            else
            {
                BindDataLicenseTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
                CreateTab();
                UpdatePanelGrid.Update();

                UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
                UCModalError.ShowModalError();
               
            }
        }

        /// <summary>
        /// AddNew : ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void btnInsertApplicantDetail_Click(object sender, EventArgs e)
        {
            if (ddlApplicantDocumentType.SelectedIndex > 0 && ddlApplicantMemberType.SelectedIndex > 0)
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                DTO.ConfigDocument doc = new DTO.ConfigDocument();
                //string funcID = DTO.DocFunction.APPLICANT_FUNCTION.GetEnumValue().ToString();
                var detail = biz.GetConfigDocumentByDocumentCode(DTO.DocFunction.APPLICANT_FUNCTION.GetEnumValue().ToString(), ddlApplicantDocumentType.SelectedValue, ddlApplicantMemberType.SelectedValue);
                if (detail.DataResponse != null)
                {
                    UCModalError.ShowMessageError = SysMessage.DupConfigSettingDocument;
                    UCModalError.ShowModalError();
                }
                else
                {
                    doc.FUNCTION_ID = "64";
                    doc.MEMBER_CODE = ddlApplicantMemberType.SelectedValue;
                    doc.DOCUMENT_CODE = ddlApplicantDocumentType.SelectedValue;
                    doc.DOCUMENT_REQUIRE = "N";
                    doc.STATUS = "A";
                    var res = biz.InsertConfigDocument(doc, base.UserProfile);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
                        UCModalSuccess.ShowModalSuccess();
                        GetApplicantDetailtGrid();
                        //GetConfigDocumentGrid();
                    }
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
                UCModalError.ShowModalError();
            }
        }

        /// <summary>
        /// AddNew : เพิ่มประเภทเอกสาร
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void btnInsertDocumentType_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocumentName.Text))
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                DTO.DocumentType doc = new DTO.DocumentType();
                doc.DOCUMENT_NAME = txtDocumentName.Text;
                doc.DOCUMENT_REQUIRE = "N";
                doc.STATUS = "A";
                if (chkDocumentLicense.Checked == true)
                {
                    doc.DOCUMENT_REQUIRE = "Y";
                }
                else
                {
                    doc.DOCUMENT_REQUIRE = "N";
                }
                var res = biz.InsertDocumentType(doc, base.UserProfile);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
                    UCModalSuccess.ShowModalSuccess();
                    ClearDocumentTypeValue();
                    GetDocumentTypeGrid();
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
                UCModalError.ShowModalError();
            }
        }

        /// <summary>
        /// Update : ตั้งค่าเอกสารสมาชิก
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void btnSubmitConfigDocument_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.ConfigDocument>();

            foreach (GridViewRow gr in gvConfigDocument.Rows)
            {
                Label strNo = (Label)gr.FindControl("lblNo");

                if (((CheckBox)gr.FindControl("chkDocumentRequire")).Checked == true)
                {
                    data.Add(new DTO.ConfigDocument
                    {
                        ID = Convert.ToInt16(strNo.Text),
                        DOCUMENT_REQUIRE = "Y",
                    });
                }
                else
                {
                    data.Add(new DTO.ConfigDocument
                    {
                        ID = Convert.ToInt16(strNo.Text),
                        DOCUMENT_REQUIRE = "N",
                    });
                }
            }

            if (data != null)
            {
                var biz = new BLL.DataCenterBiz();

                var res = biz.UpdateConfigApproveLicense(data, base.UserProfile);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessConfigLicense;
                    UCModalSuccess.ShowModalSuccess();
                    GetConfigDocumentGrid();
                    GetMemberType();
                    GetDocumentType();
                    ClearConfigDocumentValue();
                    UpdatePanelGrid.Update();
                }
            }

        }

        /// <summary>
        /// Update : ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void btnSubmitApplicantDetail_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.ConfigDocument>();

            foreach (GridViewRow gr in gvApplicantDetail.Rows)
            {
                Label strNo = (Label)gr.FindControl("lblNo");

                if (((CheckBox)gr.FindControl("chkDocumentRequire")).Checked == true)
                {
                    data.Add(new DTO.ConfigDocument
                    {
                        ID = Convert.ToInt16(strNo.Text),
                        DOCUMENT_REQUIRE = "Y",
                    });
                }
                else
                {
                    data.Add(new DTO.ConfigDocument
                    {
                        ID = Convert.ToInt16(strNo.Text),
                        DOCUMENT_REQUIRE = "N",
                    });
                }
            }
            if (data != null)
            {
                var biz = new BLL.DataCenterBiz();

                var res = biz.UpdateConfigApproveLicense(data, base.UserProfile);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessConfigLicense;
                    UCModalSuccess.ShowModalSuccess();
                    GetConfigDocumentGrid();
                    GetMemberType();
                    GetDocumentType();
                    ClearConfigDocumentValue();
                    UpdatePanelGrid.Update();
                }
            }

        }

        /// <summary>
        /// Delete : ตั้งค่าเอกสารสมัครสมาชิก
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void hplDeleteConfigDocument_Click(object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNo = (Label)gv.FindControl("lblNo");

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.DeleteConfigDocument(Convert.ToUInt16(strNo.Text), base.UserProfile);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {

                UCModalSuccess.ShowMessageSuccess = SysMessage.DeleteSuccess;
                UCModalSuccess.ShowModalSuccess();
                GetConfigDocumentGrid();
                UpdatePanelGrid.Update();
            }
        }

        /// <summary>
        /// Delete : เพิ่ม/ลบ ประเภทเอกสาร
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void hplDeleteDocumentType_Click(object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNo = (Label)gv.FindControl("lblNo");

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.DeleteDocumentType(strNo.Text);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {

                UCModalSuccess.ShowMessageSuccess = SysMessage.DeleteSuccess;
                UCModalSuccess.ShowModalSuccess();
                GetDocumentTypeGrid();
                UpdatePanelGrid.Update();
            }
        }

        /// <summary>
        /// Delete : ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <EDITOR>Natta</EDITOR>
        /// <LASTUPDATE>09/08/2557</LASTUPDATE>
        protected void hplDeleteApplicantDetail_Click(object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNo = (Label)gv.FindControl("lblNo");

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.DeleteConfigDocument(Convert.ToUInt16(strNo.Text), base.UserProfile);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {

                UCModalSuccess.ShowMessageSuccess = SysMessage.DeleteSuccess;
                UCModalSuccess.ShowModalSuccess();
                GetApplicantDetailtGrid();
                //GetConfigDocumentGrid();
                UpdatePanelGrid.Update();
            }
        }

        protected void gvConfigDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                Label lblValueGvApprove = (Label)e.Row.Cells[0].FindControl("lblValueGvApprove");

                CheckBox chkDocumentRequire = (CheckBox)e.Row.Cells[0].FindControl("chkDocumentRequire");

                if (lblValueGvApprove.Text == "Y")
                {
                    chkDocumentRequire.Checked = true;
                }
                else
                {
                    chkDocumentRequire.Checked = false;
                }


            }
        }

        protected void gvDocumentType_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        /// <summary>
        /// CreateTab Function Added
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <LASTUPDATE>12/05/2557</LASTUPDATE>
        protected void TabSetting_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabSetting.ActiveTabIndex == 0)
            {
                BindDataRegistationTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
            else if (TabSetting.ActiveTabIndex == 1)
            {
                BindDataLicenseTab();
                dvSubmitConfigLicense.Style.Add("display", "none");

                //Add new
                this.CreateTab();
            }
            else if (TabSetting.ActiveTabIndex == 2)
            {
                BindDataInserDocumentTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
            else if (TabSetting.ActiveTabIndex == 3)
            {
                BindDataApplicantDetailTab();
                dvSubmitConfigLicense.Style.Add("display", "none");
            }
        }

        protected void btnCancelConfigDocument_Click(object sender, EventArgs e)
        {
            GetConfigDocumentGrid();
            GetMemberType();
            GetDocumentType();
        }

        protected void btnSubmitConfigLicense_Click(object sender, EventArgs e)
        {
            int iCountBeforeList = 0;
            int iCountBeforeRequire = 0;
            int iCountAfterList = 0;
            int iCountAfterRequire = 0;

            AjaxControlToolkit.TabContainer containers = (AjaxControlToolkit.TabContainer)tbcDynamic;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentLicenseConfigByPetitionTypeName(containers.ActiveTab.HeaderText);

            iCountBeforeList = biz.GetDocumentLicenseConfigByPetitionTypeName(containers.ActiveTab.HeaderText).DataResponse.Count();
            iCountBeforeRequire = ls.DataResponse.Where(w => w.DOCUMENT_REQUIRE == "Y").Count();

            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tbcDynamic;
            foreach (TabPanel tabPanel in container.Controls.OfType<TabPanel>())
            {
                foreach (Class.CustomConfigTable tpControls in tabPanel.Controls.OfType<Class.CustomConfigTable>())
                {
                    tpControls.SaveChange();
                }
            }

            var lsAfter = biz.GetDocumentLicenseConfigByPetitionTypeName(containers.ActiveTab.HeaderText);
            iCountAfterList = biz.GetDocumentLicenseConfigByPetitionTypeName(containers.ActiveTab.HeaderText).DataResponse.Count();
            iCountAfterRequire = lsAfter.DataResponse.Where(w => w.DOCUMENT_REQUIRE == "Y").Count();

            if (iCountBeforeList == iCountAfterList && iCountBeforeRequire == iCountAfterRequire)
            {
                UCModalError.ShowMessageError = Resources.errorSettingAttach_002;
                UCModalError.ShowModalError();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessConfigLicense;
                UCModalSuccess.ShowModalSuccess();
            }
        }

        protected void btnCancelConfigLicense_Click(object sender, EventArgs e)
        {
            AjaxControlToolkit.TabContainer container = (AjaxControlToolkit.TabContainer)tbcDynamic;
            int iTab = container.ActiveTabIndex;
            Session["iTab"] = iTab;
            Response.Redirect("~/Setting/SettingAttach.aspx");
        }

        protected void btnCancelApplicantDetail_Click(object sender, EventArgs e)
        {
            GetApplicantDetailtGrid();
            GetApplicantDetailMemberType();
            GetApplicantDetailDocumentType();
        }

        protected void gvApplicantDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                Label lblValueGvApprove = (Label)e.Row.Cells[0].FindControl("lblValueGvApprove");

                CheckBox chkDocumentRequire = (CheckBox)e.Row.Cells[0].FindControl("chkDocumentRequire");

                if (lblValueGvApprove.Text == "Y")
                {
                    chkDocumentRequire.Checked = true;
                }
                else
                {
                    chkDocumentRequire.Checked = false;
                }


            }
        }

        protected void LinkButtonDelete_Click(Object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            string strNo = ((Label)gv.Cells[0].Controls[0]).Text;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.UserProfile profiles = (DTO.UserProfile)HttpContext.Current.Session[PageList.UserProfile];
            string Alert = "confirm('" + Resources.infoSettingAttach_001 + "')";
            ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "confirm", Alert, true);
            var res = biz.DeleteConfigDocument(Convert.ToInt16(strNo), profiles);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = SysMessage.DeleteConfigLicense;
                UCModalSuccess.ShowModalSuccess();
                UpdatePanelGrid.Update();
            }

        }
        #endregion

    }
}