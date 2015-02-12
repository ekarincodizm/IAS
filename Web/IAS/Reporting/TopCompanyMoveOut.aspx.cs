using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Utils;

namespace IAS.Reporting
{
    public partial class TopCompanyMoveOut : basepage
    {
        DataCenterBiz Dcbiz = new DataCenterBiz();

        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateStart.Attributes.Add("readonly", "true");
            txtDateEnd.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                SetLicenseType();

                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateStart.Text = DateUtil.dd_MM_yyyy_Now_TH;

                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateUtil.dd_MM_yyyy_Now_TH;
            }
        }

        protected void ClearControl()
        {
         


            //txtDateStart.Text = "";
            //txtDateEnd.Text = "";
            ddlLicenseType.SelectedValue = "";
        }

        protected void btnViewer_Click(object sender, EventArgs e)
        {
            if (txtDateStart.Text == "" && txtDateEnd.Text != "")
            {
                UCError.ShowMessageError = "กรุณาเลือก วันที่";
                UCError.ShowModalError();
            }
            else if (txtDateEnd.Text == "" && txtDateStart.Text != "")
            {
                UCError.ShowMessageError = "กรุณาเลือก วันที่สิ้นสุด";
                UCError.ShowModalError();
            }
            else
            {
                string src = string.Format("'{0}', '{1}', '{2}', '{3}'"
                    , ddlLicenseType.SelectedValue, txtDateStart.Text, txtDateEnd.Text, ddlLicenseType.SelectedItem.Text);
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + src + ")", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void SetLicenseType()
        {
            var res = Dcbiz.GetConfigLicenseType("ทั้งหมด").DataResponse;
            if (res != null)
            {
                List<DTO.DataItem> tempItem = new List<DTO.DataItem>();
                foreach (var item in res)
                {
                    if (Convert.ToInt32(item.Id) < 10)
                        tempItem.Add(new DTO.DataItem { Id = item.Id, Name = item.Name });
                }
                tempItem.Add(new DTO.DataItem { Id = "09", Name = "รวมประเภทตัวแทนประกันชีวิต" }); //(ให้แสดงข้อมูลของประเภท 01 และ 07)
                tempItem.Add(new DTO.DataItem { Id = "10", Name = "รวมประเภทตัวแทนประกันวินาศภัย" }); //(ให้แสดงข้อมูลของประเภท 02, 05, 06, 08)
                BindToDDL(ddlLicenseType, tempItem.ToArray());
            }
        }
    }
}