using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;

namespace IAS.Reporting
{
    public partial class LicenseReport : basepage
    {
        DataCenterBiz Dcbiz = new DataCenterBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateStart.Attributes.Add("readonly", "true");
            txtDateEnd.Attributes.Add("readonly", "true");


            if (!IsPostBack)
            {
                SetLoadControl();
                txtDateStart.Text = DateTime.Now.ToShortDateString();
                txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void SetLoadControl()
        {
            SetLicenseType();
            SetCompany();
        }

        protected void SetLicenseType()
        {
            var res = Dcbiz.GetConfigLicenseType("---- เลือก ----").DataResponse;
            if (res != null)
            {
                List<DTO.DataItem> tempItem = new List<DTO.DataItem>();
                foreach (var item in res)
                {
                    if(Convert.ToInt32(item.Id) < 10)
                        tempItem.Add(new DTO.DataItem { Id = item.Id, Name = item.Name });
                }
                tempItem.Add(new DTO.DataItem { Id = "09", Name = "รวมประเภทตัวแทนประกันชีวิต" }); //(ให้แสดงข้อมูลของประเภท 01 และ 07)
                tempItem.Add(new DTO.DataItem { Id = "10", Name = "รวมประเภทตัวแทนประกันวินาศภัย" }); //(ให้แสดงข้อมูลของประเภท 02, 05, 06, 08)
                BindToDDL(ddlLicenseType, tempItem.ToArray());

            }
        }

        protected void SetCompany()
        {
            var res = Dcbiz.GetCompanyCode("---- เลือก ----");
            if (res != null)
            {
                BindToDDL(ddlCompany, res.ToArray());
            }
        }

        protected void ClearControl()
        {
            ddlLicenseType.SelectedValue = "";
            ddlCompany.SelectedValue = "";
            txtDateStart.Text = DateTime.Now.ToShortDateString();
            txtDateEnd.Text = DateTime.Now.ToShortDateString();
        }
        //public static string licensetype;
        //public static string comp;
        //public static DateTime strdate;
        //public static DateTime enddate;
        protected void btnView_Click(object sender, EventArgs e)
        {
            //RegistrationBiz biz = new RegistrationBiz();
            //var res = biz.GetLicenseReport(ddlLicenseType.SelectedValue, ddlCompany.SelectedItem.Text, txtDateStart.Text, txtDateEnd.Text);
            //licensetype = ddlLicenseType.SelectedValue.Trim();
            //comp = ddlCompany.SelectedValue.Trim();
            //strdate = Convert.ToDateTime(txtDateStart.Text);
            //enddate = Convert.ToDateTime(txtDateEnd.Text);


            //string date_st = Utils.ConvertCustom.ConvertToTxtThai(txtDateStart.Text, '/');
            //string date_en = Utils.ConvertCustom.ConvertToTxtThai(txtDateEnd.Text, '/');


            string date_st = string.Format("{0:dd/MM/yyyy}", txtDateStart.Text);
            string date_en = string.Format("{0:dd/MM/yyyy}", txtDateEnd.Text);
           
            string PetitionType = ddlLicenseType.SelectedValue.Trim();
            string CompCode = ddlCompany.SelectedValue.Trim();
            string param = String.Format("'{0}','{1}','{2}','{3}'",
                                PetitionType,
                                CompCode,
                                date_st, 
                                date_en
                                //txtDateStart.Text,
                                //txtDateEnd.Text                                
                           );
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + param + ")", true);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
    }
}