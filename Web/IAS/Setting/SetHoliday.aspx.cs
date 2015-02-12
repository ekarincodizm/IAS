using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;
using IAS.Utils;
using IAS.Properties;

namespace IAS.Setting
{
    public partial class SetHoliday : basepage
    {
        GBBiz biz = new GBBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
           txtCount.Text = txtCount.Text == "0" ? txtCount.Text = "20" : txtCount.Text;
           txtPage.Text = txtPage.Text == "0" ? txtPage.Text = "1" : txtPage.Text;
            DTO.ResponseService<DTO.GBHoliday[]> res = biz.GETGBHoliday(txtPage.Text==""?1:txtPage.Text.ToInt(),txtCount.Text==""?20:txtCount.Text.ToInt());
            GVholiday.DataSource = res.DataResponse;
            GVholiday.DataBind();
            if (res.DataResponse.Count() != 0)
            {
                lblCount.Text = res.DataResponse.First() == null ? "" : res.DataResponse.First().COUNT.ToString();
                lblPage.Text = Math.Ceiling((double)res.DataResponse.First().COUNT / txtCount.Text.ToInt()).ToString();
                pnPage.Visible = true;
            }
            else
            {
                pnPage.Visible = false;
                txtCount.Text = "20";
                txtPage.Text = "1";
            }        
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                BindGrid();
            }
            else
            {
                Search();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string validate = Validation.Validate(Page, "a");
            if (validate != "")
            {
                UCModalError1.ShowMessageError = validate;
                UCModalError1.ShowModalError();
                return;
            }
            GBHoliday holiday = new GBHoliday();
            holiday.HL_DATE = Convert.ToDateTime(txtDateHoliday.Text);
            holiday.HL_DESC = txtNameHoliday.Text;
            var res = biz.AddHoliday(holiday);
            if (res.ErrorMsg == null)
            {
                UCModalSuccess1.ShowMessageSuccess = Resources.infoSetHoliday_001;
                UCModalSuccess1.ShowModalSuccess();
                BindGrid();
                txtDateHoliday.Text = "";
                txtNameHoliday.Text = "";
            }
            else
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
        }


        protected void lbtDelete_Click(object sender, EventArgs e)
        {
            string date = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbldate")).Text;
           var res = biz.DeleteHoliday(date);
           if (res.ErrorMsg == null)
           {
               UCModalSuccess1.ShowMessageSuccess = Resources.infoSetHoliday_002;
               UCModalSuccess1.ShowModalSuccess();
               BindGrid();
           }
           else
           {
               UCModalError1.ShowMessageError = res.ErrorMsg;
               UCModalError1.ShowModalError();
           }
        }

        protected void lbtUpdate_Click(object sender, EventArgs e)
        {
            lbldatate.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lbldate")).Text;
            txtUpdatename.Text = ((Label)((GridViewRow)((LinkButton)sender).Parent.Parent).FindControl("lblNamedate")).Text;
            ModalPopupUpdate.Show();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ModalPopupUpdate.Hide();
            DTO.GBHoliday holiday = new GBHoliday();
            holiday.HL_DATE = Convert.ToDateTime(lbldatate.Text);
            holiday.HL_DESC = txtUpdatename.Text;
            var res = biz.UpdateHoliday(holiday);
            if (res.ErrorMsg == null)
            {
                UCModalSuccess1.ShowMessageSuccess = Resources.infoSetHoliday_003;
                UCModalSuccess1.ShowModalSuccess();              
            }
            else
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ModalPopupUpdate.Hide();
        }

        protected void btnCancelSave_Click(object sender, EventArgs e)
        {
            txtUpdatename.Text = "";
            txtNameHoliday.Text = "";
            txtDateHoliday.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            txtCount.Text = txtCount.Text == "0" ? txtCount.Text = "20" : txtCount.Text;
            txtPage.Text = txtPage.Text == "0" ? txtPage.Text = "1" : txtPage.Text;
            DTO.ResponseService<DTO.GBHoliday[]> res = biz.SearchHoliday(txtSearch.Text, txtPage.Text.ToInt(), txtCount.Text.ToInt());
            if (res.ErrorMsg == null)
            {
                GVholiday.DataSource = res.DataResponse;
                GVholiday.DataBind();
                if (res.DataResponse.Count() != 0)
                {
                    lblCount.Text = res.DataResponse.First() == null ? "" : res.DataResponse.First().COUNT.ToString();
                    lblPage.Text = Math.Ceiling((double)res.DataResponse.First().COUNT / txtCount.Text.ToInt()).ToString();
                    pnPage.Visible = true;
                }
                else
                {
                    pnPage.Visible = false;
                    txtCount.Text = "20";
                    txtPage.Text = "1";
                }
            }
            else
            {
                UCModalError1.ShowMessageError = res.ErrorMsg;
                UCModalError1.ShowModalError();
            }
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            txtPage.Text = "1";
            if (txtSearch.Text == "")
            {
                BindGrid();
            }
            else
            {
                Search();
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            txtPage.Text = lblPage.Text;
            if (txtSearch.Text == "")
            {
                BindGrid();
            }
            else
            {
                Search();
            }
        }        
    }
}