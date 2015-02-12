using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.Properties;
using System.Data;

namespace IAS.Setting
{
    public partial class SettingPaymentExpireDate : basepage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //initGridPaymentExpireDate();
                BindDataPrint();
                BindDataRecordPerPage();
            }
        }
        #region ExpireDate
        //private void initGridPaymentExpireDate()
        //{
        //    try
        //    {
        //        BLL.PaymentBiz biz = new BLL.PaymentBiz();
        //        var res = biz.GetPaymentExpireDay();
        //        gvPaymentExpireDate.DataSource = res.DataResponse;
        //        gvPaymentExpireDate.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        UCModalError.ShowMessageError = ex.Message;
        //        UCModalError.ShowModalError();
        //    }
        //}

        //protected void btnSavePaymentExpireDay_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var ls = new List<DTO.ConfigPaymentExpireDay>();
        //        foreach (GridViewRow gvr in gvPaymentExpireDate.Rows)
        //        {
        //            var id = (Label)gvr.FindControl("lblID");
        //            var day = (TextBox)gvr.FindControl("txtExpireDay");
        //            var temp = (Label)gvr.FindControl("lblPaymentExpireDay");
        //            if (day.Text.ToInt() == temp.Text.ToInt()) continue;

        //            ls.Add(new DTO.ConfigPaymentExpireDay()
        //            {
        //                ID = id.Text,
        //                PAYMENT_EXPIRE_DAY = day.Text.ToInt()
        //            });
        //        }

        //        if (ls.Count > 0)
        //        {

        //            BLL.PaymentBiz biz = new BLL.PaymentBiz();
        //            var res = biz.UpdatePaymentExpireDay(ls, base.UserProfile);
        //            if (!res.IsError)
        //            {
        //                UCModalSuccess.ShowMessageSuccess = Resources.infoSetHoliday_001;
        //                UCModalSuccess.ShowModalSuccess();

        //                initGridPaymentExpireDate();
        //                uplPaymentExpireDay.Update();
        //            }
        //            else
        //            {
        //                UCModalError.ShowMessageError = res.ErrorMsg;
        //                UCModalError.ShowModalError();
        //                initGridPaymentExpireDate();
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UCModalError.ShowMessageError = ex.Message;
        //        UCModalError.ShowModalError();
        //    }
        //}
        //protected void btnCancelPaymentExpireDay_Click(object sender, EventArgs e)
        //{
        //    initGridPaymentExpireDate();
        //}
        #endregion
        #region PrintSetting
        private string iasgroupCodePrintSetting = "SP001";
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
        protected void BindDataPrint()
        {
            var biz = new BLL.DataCenterBiz();
            var BindConfig = biz.GetConfigPrint(this.iasgroupCodePrintSetting);
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
            BindDataPrint();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var biz = new BLL.DataCenterBiz();

            var res = biz.SaveConfigPrint(PrintPayment);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
                BindDataPrint();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                UCModalSuccess.ShowModalSuccess();
                BindDataPrint();
                PrintPayment.Clear();
                uplPaymentExpireDay.Update();
            }
        }
        #endregion

        #region PrintRecordPerPage
        private string iasgroupCodeRecordPerPage = "SP002";
        public List<DTO.ConfigPrintPayment> PrintPerPage
        {
            get
            {
                if (Session["PrintPerPage"] == null)
                {
                    Session["PrintPerPage"] = new List<DTO.ConfigPrintPayment>();
                }

                return (List<DTO.ConfigPrintPayment>)Session["PrintPerPage"];
            }

            set
            {
                Session["PrintPerPage"] = value;
            }
        }
        protected void BindDataRecordPerPage()
        {
            var biz = new BLL.DataCenterBiz();
            var BindRecordPerPage = biz.GetConfigPrint(this.iasgroupCodeRecordPerPage);
            if (BindRecordPerPage.DataResponse.Tables[0].Rows.Count > 0)
            {
                DataRow drBindRecordPerPage = BindRecordPerPage.DataResponse.Tables[0].Rows[0];
                txtRecordPerBillPayment.Text = drBindRecordPerPage["ITEM_VALUE"].ToString();
            }

        }
        protected void btnSaveRecordPerPage_Click(object sender, EventArgs e)
        {
            var biz = new BLL.DataCenterBiz();
            PrintPerPage.Add(new DTO.ConfigPrintPayment
            {
                Id = "08",
                ITEM_VALUE = txtRecordPerBillPayment.Text,
                GROUP_CODE = this.iasgroupCodeRecordPerPage,
                USER_ID = base.UserId
            });
            var res = biz.SaveConfigPrint(PrintPerPage);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
              BindDataRecordPerPage();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = "บันทึกข้อมูลสำเร็จ";
                UCModalSuccess.ShowModalSuccess();
                BindDataRecordPerPage();
                PrintPerPage.Clear();
                uplPaymentExpireDay.Update();
            }
        }

        protected void btnCancelRecordPerPage_Click(object sender, EventArgs e)
        {
            BindDataRecordPerPage();
        }
        #endregion

      
    }
}