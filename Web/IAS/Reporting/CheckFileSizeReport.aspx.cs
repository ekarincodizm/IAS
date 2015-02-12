using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.MasterPage;
using IAS.Properties;
using IAS.Utils;

namespace IAS.Reporting
{
    public partial class CheckFileSizeReport : basepage
    {
        DataCenterBiz Dcbiz = new DataCenterBiz();
        public Site1 MasterPage
        {
            get
            {
                return (this.Page.Master as Site1);
            }
        }
        private void GetPaymentType()
        {
            var message = SysMessage.DefaultAll;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetPaymentType(message);

            BindToDDL(ddlTypePay, ls.DataResponse.ToList());

        }
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateStart.Attributes.Add("readonly", "true");
            txtDateEnd.Attributes.Add("readonly", "true");

            if (!IsPostBack)
            {
                SetLoadControl();

                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateStart.Text = DateUtil.dd_MM_yyyy_Now_TH;

                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateUtil.dd_MM_yyyy_Now_TH;
                //txtDateStart.Text = DateTime.Now.ToShortDateString();
                //txtDateEnd.Text = DateTime.Now.ToShortDateString();
            }
        }

        //private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        //{
        //    ddl.DataTextField = "Name";
        //    ddl.DataValueField = "Id";
        //    ddl.DataSource = ls;
        //    ddl.DataBind();
        //};

        protected void SetLoadControl()
        {
            //SetLicenseType();
            //SetCompany();
            GetPaymentType();
            ddlTypePay.SelectedIndex = 0;
        }

        //protected void SetLicenseType()
        //{
        //    var res = Dcbiz.GetConfigLicenseType("---- เลือก ----").DataResponse;
        //    if (res != null)
        //    {
        //        List<DTO.DataItem> tempItem = new List<DTO.DataItem>();
        //        foreach (var item in res)
        //        {
        //            if(Convert.ToInt32(item.Id) < 10)
        //                tempItem.Add(new DTO.DataItem { Id = item.Id, Name = item.Name });
        //        }
        //        tempItem.Add(new DTO.DataItem { Id = "09", Name = "รวมประเภทตัวแทนประกันชีวิต" }); //(ให้แสดงข้อมูลของประเภท 01 และ 07)
        //        tempItem.Add(new DTO.DataItem { Id = "10", Name = "รวมประเภทตัวแทนประกันวินาศภัย" }); //(ให้แสดงข้อมูลของประเภท 02, 05, 06, 08)
        //        BindToDDL(ddlLicenseType, tempItem.ToArray());

        //    }
        //}

        //protected void SetCompany()
        //{
        //    var res = Dcbiz.GetCompanyCode("---- เลือก ----");
        //    if (res != null)
        //    {
        //        BindToDDL(ddlCompany, res.ToArray());
        //    }
        //}

        protected void ClearControl()
        {
            //ddlLicenseType.SelectedValue = "";
            //ddlCompany.SelectedValue = "";
            txtDateStart.Text = "";
            txtDateEnd.Text = "";
            ddlTypePay.SelectedIndex = 0;
        }
        //public static string licensetype;
        //public static string comp;
        //public static DateTime strdate;
        //public static DateTime enddate;
        protected void btnView_Click(object sender, EventArgs e)
        {
            if (txtDateStart.Text == "" && txtDateEnd.Text == "" || txtDateStart.Text!="" && txtDateEnd.Text!="")
            {
                
                if ((txtDateStart.Text!="" && txtDateEnd.Text!="")&&(Convert.ToDateTime(txtDateStart.Text)>Convert.ToDateTime(txtDateEnd.Text)))
                {
                    this.MasterPage.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                    this.MasterPage.ModelError.ShowModalError();
                    return;
                }
                else
                {
                    string date_st = Utils.ConvertCustom.ConvertToTxtThai(txtDateStart.Text, '/');
                    string date_en = Utils.ConvertCustom.ConvertToTxtThai(txtDateEnd.Text, '/');

                    //string PetitionType = ddlLicenseType.SelectedValue.Trim();
                    //string CompCode = ddlCompany.SelectedValue.Trim();
                    //string param = String.Format("'{0}','{1}','{2}','{3}'",
                    //                    PetitionType,
                    //                    CompCode,
                    //                    date_st, 
                    //                    date_en
                    //                    //txtDateStart.Text,
                    //                    //txtDateEnd.Text                                
                    //               );
                    string TypePay = ddlTypePay.SelectedItem.Text.Trim();
                    string param = String.Format("'{0}','{1}','{2}'",
                                        TypePay,
                                        date_st,
                                        date_en
                        //txtDateStart.Text,
                        //txtDateEnd.Text                                
                                   );
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + param + ")", true);
                }
            }
            else if (txtDateStart.Text!="" && txtDateEnd.Text == "")
            {
                this.MasterPage.ModelError.ShowMessageError = "กรุณาเลือกวันที่สร้างใบเสร็จ(สิ้นสุด)";
                this.MasterPage.ModelError.ShowModalError();
                return;

            }
            else
            {
                this.MasterPage.ModelError.ShowMessageError = "กรุณาเลือกวันที่สร้างใบเสร็จ(เริ่ม)";
                this.MasterPage.ModelError.ShowModalError();
                return;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
    }
}