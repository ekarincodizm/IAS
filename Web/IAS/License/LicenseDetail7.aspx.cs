using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;

namespace IAS.License
{
    public partial class LicenseDetail7 : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                DefaultDAta();//มิ้ว จับแยกไปข้างล่าง ไม่ได้แก้โค้ด
            }
        }
        protected void DefaultDAta()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;

            GetLicenseType();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindDataInGridView();
        }

        private void BindDataInGridView()
        {   
            var biz = new BLL.LicenseBiz();


            if (!string.IsNullOrEmpty(txtAllowNumber.Text) && !string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var res = biz.GetReceiveLicenseByCriteria(txtAllowNumber.Text,
                    ddlTypeLicense.SelectedValue,
                    Convert.ToDateTime(txtStartDate.Text),
                    Convert.ToDateTime(txtEndDate.Text));

                if (res.IsError)
                {
                    //var errorMsg = res.ErrorMsg;

                    //AlertMessage.ShowAlertMessage(string.Empty, errorMsg);

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvLicense.DataSource = res.DataResponse;
                    gvLicense.DataBind();

                    UpdatePanelSearch.Update();
                }
            }
            else if (string.IsNullOrEmpty(txtAllowNumber.Text))
            {
                if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
                {
                    var res = biz.GetReceiveLicenseByCriteria("",
                    ddlTypeLicense.SelectedValue,
                    Convert.ToDateTime(txtStartDate.Text),
                    Convert.ToDateTime(txtEndDate.Text));

                    if (res.IsError)
                    {
                        //var errorMsg = res.ErrorMsg;

                        //AlertMessage.ShowAlertMessage(string.Empty, errorMsg);

                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        
                        gvLicense.DataSource = res.DataResponse;
                        gvLicense.DataBind();

                        UpdatePanelSearch.Update();
                    }
                }
                else
                {
                    //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.PleaseInputFill);

                    UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
                    UCModalError.ShowModalError();
                }
            }

        }

        protected void ibtClearStartDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartDate.Text = string.Empty;
        }

        protected void ibtClearEndDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndDate.Text = string.Empty;
        }


        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetLicenseType()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseType(message);

            BindToDDL(ddlTypeLicense, ls.DataResponse.ToList());
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultDAta();
        }


    }
}