using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAS.BLL;
using System.Text;

namespace IAS.Setting
{
    public partial class Setting : basepage
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                BindDataInGridView();
            }
        }
        #endregion

        #region Private & Public Function
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void BindDataInGridView()
        {
            /*1. GetConfigApproveMember*/
            var biz = new BLL.DataCenterBiz();
            var res = biz.GetConfigApproveMember();

            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvApproveRegis.DataSource = res.DataResponse;
                gvApproveRegis.DataBind();


            }
            /*1. GetConfigApproveMember*/


            /*3. GetConfigGeneral*/
            var res3 = biz.GetConfigGeneral();

            if (res3.IsError)
            {
                UCModalError.ShowMessageError = res3.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvGeneral.DataSource = res3.DataResponse;
                gvGeneral.DataBind();


            }
            /*3. GetConfigGeneral*/

            /*2. GetConfigApproveDocument*/
            var res2 = biz.GetConfigApproveDocument();

            if (res2.IsError)
            {
                UCModalError.ShowMessageError = res2.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvInspectorDoc.DataSource = res2.DataResponse;
                gvInspectorDoc.DataBind();
            }
            /*2. GetConfigApproveDocument*/


            //Config อนุมัติสมัครสมาชิก เพิ่มเติม
            this.GetNewRegistrationApprove();

            // Config Check Exam License
            var res4 = biz.GetConfigCheckExamLicense();
            if (res4.IsError)
            {
                UCModalError.ShowMessageError = res4.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvCfgCheckExamLicense.DataSource = res4.DataResponse;
                gvCfgCheckExamLicense.DataBind();
            }

            UpdatePanelGrid.Update();
        }

        /// <summary>
        /// GridViewDataBindSingleMode
        /// </summary>
        /// <param name="LayerMode">String</param>
        /// <LASTUPDATE>08/08/2557</LASTUPDATE>
        /// <AUTHOR>Natta</AUTHOR>
        private void GridViewDataBindSingleMode(string LayerMode)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();

            if (LayerMode.Equals("1"))
            {
                /*1. GetConfigApproveMember*/
                var res = biz.GetConfigApproveMember();

                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvApproveRegis.DataSource = res.DataResponse;
                    gvApproveRegis.DataBind();

                }
                /*1. GetConfigApproveMember*/

            }
            else if (LayerMode.Equals("2"))
            {
                //Config อนุมัติสมัครสมาชิก เพิ่มเติม
                this.GetNewRegistrationApprove();
            }
            else if (LayerMode.Equals("3"))
            {
                /*2. GetConfigApproveDocument*/
                var res2 = biz.GetConfigApproveDocument();

                if (res2.IsError)
                {
                    UCModalError.ShowMessageError = res2.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvInspectorDoc.DataSource = res2.DataResponse;
                    gvInspectorDoc.DataBind();
                }
                /*2. GetConfigApproveDocument*/

            }
            else if (LayerMode.Equals("4"))
            {
                /*3. GetConfigGeneral*/
                var res3 = biz.GetConfigGeneral();

                if (res3.IsError)
                {
                    UCModalError.ShowMessageError = res3.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvGeneral.DataSource = res3.DataResponse;
                    gvGeneral.DataBind();
                }
                /*3. GetConfigGeneral*/

            }
            else if (LayerMode.Equals("5"))
            {
                // Config Check Exam License
                var res4 = biz.GetConfigCheckExamLicense();
                if (res4.IsError)
                {
                    UCModalError.ShowMessageError = res4.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvCfgCheckExamLicense.DataSource = res4.DataResponse;
                    gvCfgCheckExamLicense.DataBind();
                }
            }

            UpdatePanelGrid.Update();
        }
        #endregion

        #region UI Function
        protected void btnCancelApproveRegis_Click(object sender, EventArgs e)
        {
            GridViewDataBindSingleMode("1");
        }

        protected void btnCancelExtraApproveRegis_Click(object sender, EventArgs e)
        {
            GridViewDataBindSingleMode("2");
        }

        protected void btnCancelInspectorDoc_Click(object sender, EventArgs e)
        {
            GridViewDataBindSingleMode("3");
        }

        protected void btnCancelGeneral_Click(object sender, EventArgs e)
        {
            GridViewDataBindSingleMode("4");
        }

        protected void gvApproveRegis_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                #region New Cfg
                /// <Author>Natta</Author>
                /// <Object>Web Configuration : Disable General Root</Object>
                /// <CreateDate>28/3/2557</CreateDate>
                Label lblGroupCodeRegis = (Label)e.Row.FindControl("lblGroupCodeRegis");
                if (lblGroupCodeRegis != null)
                {
                    if (lblGroupCodeRegis.Text.Equals("General"))
                    {
                        e.Row.Enabled = false;
                        CheckBox chk = (CheckBox)e.Row.FindControl("chkSpecifiedValue");
                        Label lblDesc = (Label)e.Row.FindControl("lblDescriptionApproveRegisterGv");
                        if ((chk != null) && (lblDesc != null))
                        {
                            chk.Enabled = false;
                            chk.Visible = false;
                            lblDesc.Enabled = false;
                            lblDesc.Visible = false;
                        }
                    }
                    if (lblGroupCodeRegis.Text.StartsWith("General_"))
                    {
                        Label lblFuncationNameGv = (Label)e.Row.FindControl("lblFuncationNameGv");
                        if (lblFuncationNameGv != null)
                        {
                            StringBuilder newstr = new StringBuilder();
                            newstr.Append("- ");
                            newstr.Append(lblFuncationNameGv.Text);
                            lblFuncationNameGv.Text = newstr.ToString();

                        }
                    }
                }
                #endregion


                Label lblValueGvApproveRegis = (Label)e.Row.Cells[0].FindControl("lblValueGvApproveRegis");

                CheckBox chkSpecifiedValue = (CheckBox)e.Row.Cells[0].FindControl("chkSpecifiedValue");

                if (lblValueGvApproveRegis.Text != "Y")
                {
                    chkSpecifiedValue.Checked = false;
                }
                else
                {
                    chkSpecifiedValue.Checked = true;
                }
            }

        }

        protected void gvInspectorDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                DropDownList ddlInspector = (DropDownList)e.Row.Cells[0].FindControl("ddlInspector");

                Label lblItemValueGvInspectorDoc = (Label)e.Row.Cells[0].FindControl("lblItemValueGvInspectorDoc");

                CheckBox chkSpecifiedValueInspectorDoc = (CheckBox)e.Row.Cells[0].FindControl("chkSpecifiedValueInspectorDoc");

                if (lblItemValueGvInspectorDoc.Text != "Y")
                {
                    chkSpecifiedValueInspectorDoc.Checked = false;
                }
                else
                {
                    chkSpecifiedValueInspectorDoc.Checked = true;
                }

                var message = SysMessage.DefaultSelecting;

                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                var ls = biz.GetAssociateToSetting(message);

                BindToDDL(ddlInspector, ls.DataResponse.ToList());


                Label lblValueGvInspectorDoc = (Label)e.Row.Cells[0].FindControl("lblValueGvInspectorDoc");

                if (lblValueGvInspectorDoc.Text != null)
                {
                    var value = lblValueGvInspectorDoc.Text;

                    ddlInspector.SelectedValue = value;
                }



            }

        }

        protected void gvGeneral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;
                Label lblId = (Label)e.Row.Cells[0].FindControl("lblIdGvGeneral");
                Label lblValueGvGeneral = (Label)e.Row.Cells[0].FindControl("lblValueGvGeneral");

                CheckBox chkSpecifiedValueGeneral = (CheckBox)e.Row.Cells[0].FindControl("chkSpecifiedValueGeneral");

                TextBox txtYearValue = (TextBox)e.Row.Cells[0].FindControl("txtYearValue");
                DropDownList ddlPicLicense = (DropDownList)e.Row.Cells[0].FindControl("ddlConfigPicForLicense");

                if (lblId.Text == "13")
                {
                    DataCenterBiz dcbiz = new DataCenterBiz();
                    BindToDDL(ddlPicLicense, dcbiz.GetDocumentTypeIsImage());
                    ddlPicLicense.Visible = true;
                    ddlPicLicense.SelectedValue = lblValueGvGeneral.Text;
                    chkSpecifiedValueGeneral.Checked = false;
                    chkSpecifiedValueGeneral.Visible = false;
                    txtYearValue.Visible = false;
                }
                else if (lblValueGvGeneral.Text == "N")
                {
                    chkSpecifiedValueGeneral.Checked = false;
                    txtYearValue.Visible = false;
                    ddlPicLicense.Visible = false;
                }
                else if (lblValueGvGeneral.Text == "Y")
                {
                    chkSpecifiedValueGeneral.Checked = true;
                    txtYearValue.Visible = false;
                    ddlPicLicense.Visible = false;
                }
                else if (lblValueGvGeneral.Text != "Y" && lblValueGvGeneral.Text != "N")
                {
                    txtYearValue.Text = lblValueGvGeneral.Text;
                    chkSpecifiedValueGeneral.Visible = false;
                    txtYearValue.Visible = true;
                    ddlPicLicense.Visible = false;
                }
            }
        }

        protected void btnOkApproveRegis_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.ConfigEntity>();

            foreach (GridViewRow gr in gvApproveRegis.Rows)
            {
                var lblIdGvApproveRegis = (Label)gr.FindControl("lblIdGvApproveRegis");
                var lblValueGvApproveRegis = (Label)gr.FindControl("lblValueGvApproveRegis");

                if (((CheckBox)gr.FindControl("chkSpecifiedValue")).Checked == true)
                {
                    data.Add(new DTO.ConfigEntity
                    {
                        Id = lblIdGvApproveRegis.Text,
                        Value = "Y"
                    });
                }
                else
                {
                    data.Add(new DTO.ConfigEntity
                    {
                        Id = lblIdGvApproveRegis.Text,
                        Value = "N"
                    });
                }
            }

            if (data != null)
            {
                var biz = new BLL.DataCenterBiz();

                var res = biz.UpdateConfigApproveMember(data);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                    UCModalSuccess.ShowModalSuccess();
                    BindDataInGridView();
                    UpdatePanelGrid.Update();
                }
            }
        }

        protected void btnOkInspectorDoc_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.ConfigEntity>();

            foreach (GridViewRow gr in gvInspectorDoc.Rows)
            {
                var lblIdGvInspectorDoc = (Label)gr.FindControl("lblIdGvInspectorDoc");
                var ddlInspector = (DropDownList)gr.FindControl("ddlInspector");
                var chkSpecifiedValueInspectorDoc = (CheckBox)gr.FindControl("chkSpecifiedValueInspectorDoc");

                if (((CheckBox)gr.FindControl("chkSpecifiedValueInspectorDoc")).Checked == true)
                {
                    data.Add(new DTO.ConfigEntity
                    {
                        Id = lblIdGvInspectorDoc.Text,
                        Value = ddlInspector.SelectedValue,
                        Item_Value = "Y"
                    });
                }
                else
                {
                    data.Add(new DTO.ConfigEntity
                    {
                        Id = lblIdGvInspectorDoc.Text,
                        Value = ddlInspector.SelectedValue,
                        Item_Value = "N"
                    });
                }
            }

            if (data != null)
            {
                var biz = new BLL.DataCenterBiz();

                var res = biz.UpdateConfigApproveDocument(data);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                    UCModalSuccess.ShowModalSuccess();
                    BindDataInGridView();
                    UpdatePanelGrid.Update();
                }
            }
            //var biz = new BLL.DataCenterBiz();

            //var res = biz.UpdateConfigApproveDocument(
        }

        protected void btnOkGeneral_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.ConfigEntity>();

            foreach (GridViewRow gr in gvGeneral.Rows)
            {
                var lblIdGvGeneral = (Label)gr.FindControl("lblIdGvGeneral");
                var lblValueGvGeneral = (Label)gr.FindControl("lblValueGvGeneral");
                var txtYear = (TextBox)gr.FindControl("txtYearValue");
                var ddlPicForLicense = (DropDownList)gr.FindControl("ddlConfigPicForLicense");
                if (lblValueGvGeneral.Text == "Y" || lblValueGvGeneral.Text == "N")
                {
                    if (((CheckBox)gr.FindControl("chkSpecifiedValueGeneral")).Checked == true)
                    {
                        data.Add(new DTO.ConfigEntity
                        {
                            Id = lblIdGvGeneral.Text,
                            Value = "Y"
                        });
                    }
                    else if (((CheckBox)gr.FindControl("chkSpecifiedValueGeneral")).Checked == false)
                    {
                        data.Add(new DTO.ConfigEntity
                        {
                            Id = lblIdGvGeneral.Text,
                            Value = "N"
                        });
                    }
                }
                else if (lblIdGvGeneral.Text == "13")
                {
                    data.Add(new DTO.ConfigEntity { Id = lblIdGvGeneral.Text, Value = ddlPicForLicense.SelectedValue , GROUP_CODE="DT001"});
                }
                else if (lblValueGvGeneral.Text != "Y" && lblValueGvGeneral.Text != "N")
                {
                    data.Add(new DTO.ConfigEntity
                    {
                        Id = lblIdGvGeneral.Text,
                        Value = txtYear.Text,
                        GROUP_CODE = "RC001"
                    });
                }
            }

            if (data != null)
            {
                var biz = new BLL.DataCenterBiz();

                var res = biz.UpdateConfigApproveMember(data);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                    UCModalSuccess.ShowModalSuccess();
                    BindDataInGridView();
                    UpdatePanelGrid.Update();
                }
            }
        }

        protected void btnOkAttachDoc_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelAttach_Click(object sender, EventArgs e)
        {

        }

        protected void gvUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvUpload_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvUpload_PreRender(object sender, EventArgs e)
        {

        }

        protected void gvUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void hplView_Click(object sender, EventArgs e)
        {

        }

        protected void hplDelete_Click(object sender, EventArgs e)
        {

        }

        protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        #endregion
        
        #region New TOR
        private void GetNewRegistrationApprove()
        {
            DataCenterBiz biz = new DataCenterBiz();
            int chkCount = 0;
            List<CheckBox> chkls = new List<CheckBox>();

            DTO.ResponseService<DTO.ConfigExtraEntity[]> resnewCfg = biz.GetNewConfigApproveMember();
            if (resnewCfg.DataResponse != null)
            {
                if (resnewCfg.DataResponse.Count() > 0)
                {
                    //int chkCounts = pnlProfile.Controls.OfType<CheckBox>().Count();
                    foreach (CheckBox chk in pnlProfile.Controls.OfType<CheckBox>())
                    {
                        chkls.Add(chk);
                        chkCount++;
                    }

                    if (resnewCfg.DataResponse.Count() == chkCount)
                    {
                        for (int i = 0; i < resnewCfg.DataResponse.Count(); i++)
                        {

                            CheckBox chkIdx = chkls.Where(x => x.Text == resnewCfg.DataResponse[i].Name).FirstOrDefault();
                            if (chkIdx != null)
                            {
                                if (resnewCfg.DataResponse[i].Value.Equals(1))
                                {
                                    chkIdx.Checked = true;
                                }
                                else
                                {
                                    chkIdx.Checked = false;
                                }
                            }
                        }
                    }


                }
            }
            else
            {
                UCModalError.ShowMessageError = resnewCfg.ErrorMsg;
                UCModalError.ShowModalError();
            }
        }

        protected void btnSubmitExtraApproveRegis_Click(object sender, EventArgs e)
        {
            DataCenterBiz biz = new DataCenterBiz();
            //int chkCount = 0;
            List<CheckBox> chkls = new List<CheckBox>();
            List<DTO.ConfigExtraEntity> data = new List<DTO.ConfigExtraEntity>();


            foreach (CheckBox chk in pnlProfile.Controls.OfType<CheckBox>())
            {
                if (chk.Checked == true)
                {
                    data.Add(new DTO.ConfigExtraEntity
                    {
                        Name = chk.Text,
                        Value = 1
                    });
                }
                else
                {
                    data.Add(new DTO.ConfigExtraEntity
                    {
                        Name = chk.Text,
                        Value = 0
                    });
                }
            }

            if (data != null)
            {
                DTO.ResponseMessage<bool> res = biz.UpdateNewConfigApproveMember(data.ToArray(), base.UserProfile);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                    UCModalSuccess.ShowModalSuccess();

                    this.GetNewRegistrationApprove();
                    UpdatePanelGrid.Update();
                }
            }
        }
        #endregion

        protected void gvCfgCheckExamLicense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label value = e.Row.FindControl("lblValueCfgCheckExamLicense") as Label;
                CheckBox chk = e.Row.FindControl("chkItemValueCfgCheckExamLicense") as CheckBox;

                if (value.Text == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }

        protected void btnOkCfgCheckExamLicense_Click(object sender, EventArgs e)
        {
            try
            {
                DataCenterBiz Dcbiz = new DataCenterBiz();
                List<DTO.ConfigEntity> ent = new List<DTO.ConfigEntity>();
                foreach (GridViewRow row in gvCfgCheckExamLicense.Rows)
                {
                    Label ID = row.FindControl("lblIdCfgCheckExamLicense") as Label;
                    CheckBox chk = row.FindControl("chkItemValueCfgCheckExamLicense") as CheckBox;

                    ent.Add(new DTO.ConfigEntity
                    {
                        Id = ID.Text,
                        Item_Value = (chk.Checked ? "1" : "0")
                    });
                }
                
                var res = Dcbiz.UpdateConfigCheckExamLicense(ent, UserProfile);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                    UCModalSuccess.ShowModalSuccess();
                    GridViewDataBindSingleMode("5");
                    UpdatePanelGrid.Update();
                }
            }
            catch (Exception ex)
            {
                UCModalError.ShowMessageError = ex.Message;
                UCModalError.ShowModalError();
            }
        }

        protected void btnCancelCfgCheckExamLicense_Click(object sender, EventArgs e)
        {
            GridViewDataBindSingleMode("5");
        }
    }
}