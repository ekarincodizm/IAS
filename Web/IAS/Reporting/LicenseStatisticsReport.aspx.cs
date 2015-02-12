using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.MasterPage;
using System.Data;


namespace IAS.Reporting
{
    public partial class LicenseStatisticsReport : basepage
    {
        DataCenterBiz Dcbiz = new DataCenterBiz();
        public Site1 MasterPage
        {
            get
            {
                return (this.Page.Master as Site1);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDateStart1.Attributes.Add("readonly", "true");
            txtDateEnd1.Attributes.Add("readonly", "true");
            txtDateStart2.Attributes.Add("readonly", "true");
            txtDateEnd2.Attributes.Add("readonly", "true");



            if (!IsPostBack)
            {
                SetLoadControl();
                ddlLicenseType.SelectedIndex = 0;
                txtDateStart1.Text = DateTime.Now.ToShortDateString();
                txtDateEnd1.Text = DateTime.Now.ToShortDateString();
                txtDateStart2.Text = DateTime.Now.ToShortDateString();
                txtDateEnd2.Text = DateTime.Now.ToShortDateString();
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
            //SetCompany();
        }

        protected void SetLicenseType()
        {
            var res = Dcbiz.GetConfigLicenseType("---- เลือก ----").DataResponse;
            if (res != null)
            {
                List<DTO.DataItem> tempItem = new List<DTO.DataItem>();
                //foreach (var item in res)
                //{
                //    if(Convert.ToInt32(item.Id) < 10)
                //        tempItem.Add(new DTO.DataItem { Id = item.Id, Name = item.Name });
                //}
                tempItem.Add(new DTO.DataItem { Id = "01", Name = "ตัวแทนประกันชีวิต" }); //(ให้แสดงข้อมูลของประเภท 01 และ 07)
                tempItem.Add(new DTO.DataItem { Id = "02", Name = "ตัวแทนประกันวินาศภัย" }); //(ให้แสดงข้อมูลของประเภท 02, 05, 06, 08)
                tempItem.Add(new DTO.DataItem { Id = "03", Name = "นายหน้าประกันชีวิต (บุคคลธรรมดา)" }); //(ให้แสดงข้อมูลของประเภท 03)
                tempItem.Add(new DTO.DataItem { Id = "04", Name = "นายหน้าประกันวินาศภัย (บุคคลธรรมดา)" }); //(ให้แสดงข้อมูลของประเภท 04)
                tempItem.Add(new DTO.DataItem { Id = "00", Name = "ทั้งหมด" }); //(ให้แสดงข้อมูลของประเภท Id=01,02,03,04)
                BindToDDL(ddlLicenseType, tempItem.ToArray());
                ddlLicenseType.Items.Insert(0, "---- เลือก ----");

            }
        }

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
            ddlLicenseType.SelectedIndex = 0;
            
            txtDateStart1.Text = DateTime.Now.ToShortDateString();
            txtDateEnd1.Text = DateTime.Now.ToShortDateString();
            txtDateStart2.Text = DateTime.Now.ToShortDateString();
            txtDateEnd2.Text = DateTime.Now.ToShortDateString();
        }
        double COUNT_COME_CODE1;
        double COUNT_COME_CODE2;
        double sum1;
        double sum2;
        double share1;
        double share2;
        double CompareLicense;
        protected void btnView_Click(object sender, EventArgs e)
        {
            string time1 = string.Empty;
            string time2 = string.Empty;
            if (ddlLicenseType.SelectedItem.Text == "---- เลือก ----")
            {
                this.MasterPage.ModelError.ShowMessageError = "กรุณาเลือกประเภทใบอนุญาต";
                this.MasterPage.ModelError.ShowModalError();
                return;
            }
            if (Convert.ToDateTime(txtDateStart1.Text) > Convert.ToDateTime(txtDateEnd1.Text) || Convert.ToDateTime(txtDateStart2.Text)  > Convert.ToDateTime(txtDateEnd2.Text))
            {
                if (Convert.ToDateTime(txtDateStart1.Text) > Convert.ToDateTime(txtDateEnd1.Text))
                {
                    time1="ช่วงที่ 1 ";
                    
                }
                if (Convert.ToDateTime(txtDateStart2.Text) > Convert.ToDateTime(txtDateEnd2.Text))
                {
                    if (time1 == "ช่วงที่ 1 ")
                    {
                        time2 = "และ ช่วงที่ 2";
                    }
                    else
                    {
                        time2 = "ช่วงที่ 2";
                    }

                }
                this.MasterPage.ModelError.ShowMessageError = "กรุณาเลือกวันที่ออกใบอนุญาต" + time1 + "" + time2 + " วันที่เริ่มต้องน้อยกว่าวันที่สิ้นสุด";
                this.MasterPage.ModelError.ShowModalError();
                return;
            }
            else
            {
                  //string param = "";
                  //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupSingle(" + param + ")", true);
            }



            string date_st1 = string.Format("{0:dd/MM/yyyy}", txtDateStart1.Text);
            string date_en1 = string.Format("{0:dd/MM/yyyy}", txtDateEnd1.Text);
            string date_st2 = string.Format("{0:dd/MM/yyyy}", txtDateStart2.Text);
            string date_en2 = string.Format("{0:dd/MM/yyyy}", txtDateEnd2.Text);


            string licensename = ddlLicenseType.SelectedItem.Text.Trim();

            Session["license"] = licensename;
            string PetitionType = ddlLicenseType.SelectedValue.Trim();
            string param = String.Format("'{0}','{1}','{2}','{3}','{4}'",
                                PetitionType,
                                date_st1,
                                date_en1,
                                date_st2,
                                date_en2

                           );
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopupViewer(" + param + ")", true);
              
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControl();
        }
        
    }
}