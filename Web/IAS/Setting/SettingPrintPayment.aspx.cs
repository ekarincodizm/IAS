using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Setting
{
    public partial class SettingPrintPayment : basepage
    {
        private string iasgroupCode = "SP001";
        public List<DTO.ConfigPrintPayment> PrintPayment
        {
            get
            {
                if (Session["PrintPayment"] == null)
                {
                    Session["PrintPayment"] = new List<DTO.ConfigPrintPayment>();
                }

                return (List<DTO.ConfigPrintPayment>)Session["PrintPayment"];
            }

            set
            {
                Session["PrintPayment"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.HasPermit();
                BindData();
              
            }
        }
        protected void BindData()
        {
            var biz = new BLL.DataCenterBiz();
            var BindConfig = biz.GetConfigPrint(this.iasgroupCode);
            gvConfigPrint.DataSource = BindConfig.DataResponse;
            gvConfigPrint.DataBind();

        }
        protected void RBLConfigPrint_Change(object sender, EventArgs e)
        {
            RadioButtonList RBLConfigM = (RadioButtonList)sender;
            GridViewRow gr = (GridViewRow)RBLConfigM.Parent.Parent;
            RadioButtonList RBLConfig = (RadioButtonList)gr.FindControl("RBLConfigPrint");
            Label lblId = (Label)gr.FindControl("lblId");
            Label lblGroupCode = (Label)gr.FindControl("lblGroupCode");
            PrintPayment.Add(new DTO.ConfigPrintPayment
            {
                Id = lblId.Text,
                ITEM_VALUE = RBLConfig.SelectedValue,
                GROUP_CODE = lblGroupCode.Text,
                USER_ID = base.UserId
            });

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var biz = new BLL.DataCenterBiz();

            var res = biz.SaveConfigPrint(PrintPayment);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                UCModalSuccess.ShowModalSuccess();
                BindData();
                UpdatePanelSearch.Update();
            }
        }
    }
}