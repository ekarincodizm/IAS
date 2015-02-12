using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using IAS.Properties;

namespace IAS.Setting
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DTO.UserProfile user = new DTO.UserProfile();

        AjaxControlToolkit.TabContainer tbcDynamic;
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateTab();
            if (!Page.IsPostBack)
            {
                BindData();
            }
            if (TabSetting.ActiveTabIndex == 1)
            {
                //dvSubmitConfigLicense.Style.Add("display", "block");
            }
            else
            {
                //dvSubmitConfigLicense.Style.Add("display", "none");
            }

            user.Id = "130923093821787";
            user.MemberType = 4;
            user.Name = "AR01";
            user.LastName = "ARSoft";
            user.CompCode = null;
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void BindData()
        {
            GetMemberType();
            GetDocumentType();
            GetLicenseType();
            GetPetitionType();
            GetConfigDocumentGrid();
            GetDocumentTypeGrid();
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetConfigLicenseType(SysMessage.DefaultSelecting);
            //BindToDDL(ddlLicenseType, ls.DataResponse.ToList());
        }

        private void GetPetitionType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetConfigPetitionLicenseType(SysMessage.DefaultSelecting);
            //BindToDDL(ddlPetitionType, ls.DataResponse.ToList());
        }

        private void GetDocumentTypeGrid()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetDocumentType("");
            gvDocumentType.DataSource = res;
            gvDocumentType.DataBind();
        }

        protected void TabSetting_ActiveTabChanged(object sender, EventArgs e)
        {

        }

        protected void btnInsertConfigDocument_Click(object sender, EventArgs e)
        {
            //if (ddlDocumentType.SelectedIndex > 0 && ddlMemberType.SelectedIndex > 0)
            //{
            //    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //    DTO.ConfigDocument doc = new DTO.ConfigDocument();
            //    var detail = biz.GetConfigDocumentByDocumentCode(ddlDocumentType.SelectedValue, ddlMemberType.SelectedValue);
            //    if (detail.DataResponse != null)
            //    {
            //        UCModalError.ShowMessageError = SysMessage.DupConfigSettingDocument;
            //        UCModalError.ShowModalError();
            //    }
            //    else
            //    {


            //        doc.FUNCTION_ID = "40";
            //        doc.MEMBER_CODE = ddlMemberType.SelectedValue;
            //        doc.DOCUMENT_CODE = ddlDocumentType.SelectedValue;
            //        doc.DOCUMENT_REQUIRE = "N";
            //        doc.STATUS = "A";
            //        var res = biz.InsertConfigDocument(doc, user);
            //        if (res.IsError)
            //        {
            //            UCModalError.ShowMessageError = res.ErrorMsg;
            //            UCModalError.ShowModalError();
            //        }
            //        else
            //        {
            //            UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
            //            UCModalSuccess.ShowModalSuccess();
            //            GetConfigDocumentGrid();
            //        }
            //    }
            //}
            //else
            //{
            //    UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
            //    UCModalError.ShowModalError();
            //}
        }
        protected void btnSubmitConfigDocument_Click(object sender, EventArgs e)
        {
            //var data = new List<DTO.ConfigDocument>();

            //foreach (GridViewRow gr in gvConfigDocument.Rows)
            //{
            //    Label strNo = (Label)gr.FindControl("lblNo");

            //    if (((CheckBox)gr.FindControl("chkDocumentRequire")).Checked == true)
            //    {
            //        data.Add(new DTO.ConfigDocument
            //        {
            //            ID = strNo.Text,
            //            DOCUMENT_REQUIRE = "Y",
            //        });
            //    }
            //    else
            //    {
            //        data.Add(new DTO.ConfigDocument
            //        {
            //            ID = strNo.Text,
            //            DOCUMENT_REQUIRE = "N",
            //        });
            //    }
            //}
            //if (data != null)
            //{
            //    var biz = new BLL.DataCenterBiz();

            //    var res = biz.UpdateConfigApproveLicense(data, user);

            //    if (res.IsError)
            //    {
            //        var errorMsg = res.ErrorMsg;

            //        UCModalError.ShowMessageError = res.ErrorMsg;
            //        UCModalError.ShowModalError();
            //    }
            //    else
            //    {
            //        UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessConfigLicense;
            //        UCModalSuccess.ShowModalSuccess();
            //        GetConfigDocumentGrid();
            //        GetMemberType();
            //        GetDocumentType();
            //        ClearConfigDocumentValue();
            //        UpdatePanelGrid.Update();
            //    }
            //}
        }

        protected void hplDeleteConfigDocument_Click(object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNo = (Label)gv.FindControl("lblNo");

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.DeleteConfigDocument(Convert.ToInt16(strNo), user);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {

                UCModalSuccess.ShowMessageSuccess = SysMessage.DeleteSuccess;
                UCModalSuccess.ShowModalSuccess();
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

                if (lblValueGvApprove.Text != "Y")
                {
                    chkDocumentRequire.Checked = false;
                }
                else
                {
                    chkDocumentRequire.Checked = true;
                }
            }
        }

        protected void btnCancelConfigDocument_Click(object sender, EventArgs e)
        {
            GetConfigDocumentGrid();
            GetMemberType();
            GetDocumentType();
        }

        private void GetConfigDocumentGrid()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetDocumentConfigApproveMember();
            gvConfigDocument.DataSource = res.DataResponse;
            gvConfigDocument.DataBind();
        }
        private void GetMemberType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberType(SysMessage.DefaultSelecting, true);
            ls.RemoveAt(4);
            BindToDDL(ddlMemberType, ls.ToList());
        }
        private void GetDocumentType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(SysMessage.DefaultSelecting);
            BindToDDL(ddlDocumentType, ls.ToList());
            //BindToDDL(ddlLicenseDocumentType, ls.ToList());
        }

        protected void btnInsertConfigLicense_Click(object sender, EventArgs e)
        {
            //if (ddlPetitionType.SelectedIndex > 0 && ddlLicenseType.SelectedIndex > 0 && ddlLicenseDocumentType.SelectedIndex > 0)
            //{
            //    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //    DTO.ConfigDocument doc = new DTO.ConfigDocument();
            //    var detail = biz.GetConfigDocumentLicense(ddlPetitionType.SelectedValue, ddlLicenseType.SelectedValue, ddlLicenseDocumentType.SelectedValue);
            //    if (detail.DataResponse != null)
            //    {
            //        UCModalError.ShowMessageError = SysMessage.DupConfigSettingDocument;
            //        UCModalError.ShowModalError();
            //        UpdatePanelGrid.Update();
            //    }
            //    else
            //    {
            //        doc.FUNCTION_ID = "41";
            //        doc.PETITION_TYPE_CODE = ddlPetitionType.SelectedValue;
            //        doc.LICENSE_TYPE_CODE = ddlLicenseType.SelectedValue;
            //        doc.DOCUMENT_CODE = ddlLicenseDocumentType.SelectedValue;
            //        doc.CREATED_BY = user.Name;
            //        doc.CREATED_DATE = DateTime.Today.ToString();
            //        doc.UPDATED_DATE = DateTime.Today.ToString();
            //        doc.UPDATED_BY = user.Name;
            //        if (chkDocumentLicense.Checked == true)
            //        {
            //            doc.DOCUMENT_REQUIRE = "Y";
            //        }
            //        else
            //        {
            //            doc.DOCUMENT_REQUIRE = "N";
            //        }
            //        doc.STATUS = "A";
            //        var res = biz.InsertConfigDocument(doc, user);
            //        if (res.IsError)
            //        {
            //            UCModalError.ShowMessageError = res.ErrorMsg;
            //            UCModalError.ShowModalError();
            //        }
            //        else
            //        {
            //            UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
            //            UCModalSuccess.ShowModalSuccess();
            //            UpdatePanelGrid.Update();
            //        }
            //    }
            //}
            //else
            //{
            //    UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
            //    UCModalError.ShowModalError();
            //}
        }

        private void CreateTab()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetConfigPetitionLicenseType("");
            tbcDynamic = new AjaxControlToolkit.TabContainer();

            for (int i = 0; i < res.DataResponse.Count(); i++)
            {
                TabPanel tbpnlProcessCategory = new TabPanel();
                tbpnlProcessCategory.HeaderText = res.DataResponse[i].Name;
                tbpnlProcessCategory.ID = "Tab" + i.ToString();

                var ls = biz.GetDocumentLicenseConfigByPetitionType(res.DataResponse[i].Id);
                IEnumerable<DTO.ConfigDocument> configs = ls.DataResponse;

                var tmp = configs.GroupBy(c => c.LICENSE_TYPE_CODE);

                Class.CustomConfigTable configTagle = new Class.CustomConfigTable(res.DataResponse[i].Id);

                configTagle.LinkButtonDelete_Click += new EventHandler(LinkButtonDelete_Click);
                tbpnlProcessCategory.Controls.Add(configTagle);

                tbcDynamic.Tabs.Add(tbpnlProcessCategory);
                TabSettingLicense.Controls.Add(tbcDynamic);
                TabSetting.Controls.Add(TabSettingLicense);
                TabSetting.Controls.Add(TabSettingRegister);
            }

        }

        protected void LinkButtonDelete_Click(Object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            //gv.Cells[0].Visible = true;
            string strNo = ((Label)gv.Cells[0].Controls[0]).Text;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.UserProfile profiles = (DTO.UserProfile)HttpContext.Current.Session[PageList.UserProfile];
            string Alert = "confirm('"+ Resources.infoSettingAttach_001 +"')";
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

        protected void btnInsertDocumentType_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtDocumentName.Text))
            //{
            //    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //    DTO.DocumentType doc = new DTO.DocumentType();
            //    doc.DOCUMENT_NAME = txtDocumentName.Text;
            //    doc.DOCUMENT_REQUIRE = "N";
            //    doc.STATUS = "A";
            //    if (chkDocumentLicense.Checked == true)
            //    {
            //        doc.DOCUMENT_REQUIRE = "Y";
            //    }
            //    else
            //    {
            //        doc.DOCUMENT_REQUIRE = "N";
            //    }
            //    var res = biz.InsertDocumentType(doc, user);
            //    if (res.IsError)
            //    {
            //        UCModalError.ShowMessageError = res.ErrorMsg;
            //        UCModalError.ShowModalError();
            //    }
            //    else
            //    {
            //        UCModalSuccess.ShowMessageSuccess = SysMessage.InsertSuccess;
            //        UCModalSuccess.ShowModalSuccess();
            //        ClearDocumentTypeValue();
            //        GetDocumentTypeGrid();
            //    }
            //}
            //else
            //{
            //    UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
            //    UCModalError.ShowModalError();
            //}
        }

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


        protected void gvDocumentType_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }




    }
}