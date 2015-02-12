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
    public partial class SettingExam : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindDataInGridView();
            }
        }

        private void BindDataInGridView()
        {
            var biz = new BLL.ExamScheduleBiz();
            var res = biz.ManageApplicantIn_OutRoom();

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


            UpdatePanelGrid.Update();
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
                Label lblGroupCodeRegis = (Label)e.Row.FindControl("lblIdGvApproveRegis");
                Label lblValueGvApproveRegis = (Label)e.Row.Cells[0].FindControl("lblValueGvApproveRegis");
                DropDownList ddlDateExpiration = (DropDownList)e.Row.Cells[0].FindControl("ddlDateExpiration");
                CheckBox chkSpecifiedValue = (CheckBox)e.Row.Cells[0].FindControl("chkSpecifiedValue");

                if (lblGroupCodeRegis != null)
                {
                    if (lblGroupCodeRegis.Text.Equals("09"))
                    {
                        if (lblValueGvApproveRegis.Text != "1")
                        {
                            chkSpecifiedValue.Checked = false;
                        }
                        else
                        {
                            chkSpecifiedValue.Checked = true;
                        }
                        ddlDateExpiration.Visible = false;
                        chkSpecifiedValue.Visible = true;
                    }
                    else if (lblGroupCodeRegis.Text.Equals("10"))
                    {
                        ddlDateExpiration.SelectedValue = lblValueGvApproveRegis.Text;

                        ddlDateExpiration.Visible = true;
                        chkSpecifiedValue.Visible = false;
                    }
                }
                #endregion


               
               
            }

        }


        protected void btnOkApproveRegis_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.ConfigEntity>();

            foreach (GridViewRow gr in gvApproveRegis.Rows)
            {
                Label lblIdGvApproveRegis = (Label)gr.FindControl("lblIdGvApproveRegis");
                //Label lblValueGvApproveRegis = (Label)gr.FindControl("lblValueGvApproveRegis");
                DropDownList ddlDateExpiration = (DropDownList)gr.FindControl("ddlDateExpiration");
                if (lblIdGvApproveRegis.Text == "09")
                {
                    if (((CheckBox)gr.FindControl("chkSpecifiedValue")).Checked == true)
                    {
                        data.Add(new DTO.ConfigEntity
                        {
                            Id = lblIdGvApproveRegis.Text,
                            GROUP_CODE = "AP001",
                            Value = "1"
                        });
                    }
                    else
                    {
                        data.Add(new DTO.ConfigEntity
                        {
                            Id = lblIdGvApproveRegis.Text,
                            GROUP_CODE = "AP001",
                            Value = "0"
                        });
                    }
                }
                else if (lblIdGvApproveRegis.Text == "10")
                {
                    data.Add(new DTO.ConfigEntity
                    {
                        Id = lblIdGvApproveRegis.Text,
                        GROUP_CODE = "AP001",
                        Value = ddlDateExpiration.SelectedValue
                    });
                }

            }

            if (data != null)
            {
                var biz = new BLL.ExamScheduleBiz();

                var res = biz.UpdateManageApplicantIn_OutRoom(data);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกเรียบร้อย";
                    UCModalSuccess.ShowModalSuccess();
                    BindDataInGridView();
                    UpdatePanelGrid.Update();
                }
            }
        }

        protected void btnCancelApproveRegis_Click(object sender, EventArgs e)
        {
            BindDataInGridView();
        }   

    }
}