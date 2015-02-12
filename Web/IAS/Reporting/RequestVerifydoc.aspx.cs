using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data;
using IAS.Properties;

namespace IAS.Reporting
{
    public partial class RequestVerifydoc : basepage
    {
        public string flgApprove = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            txtStartDate.Attributes.Add("readonly", "true");
            txtEndDate.Attributes.Add("readonly", "true");

            if (!Page.IsPostBack)
            {
                base.HasPermit();

                GetTypeDocument();
                GetProvince();
                //GetCompayByRequest();
                txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

            }
        }

        private void VerifyTempData()
        {
            DataSet ds = new DataSet();
            DataTable dt = ds.Tables.Add();

            //Adding Columns
            dt.Columns.Add("TypeDocument", typeof(string));
            dt.Columns.Add("IDCard", typeof(string));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Action", typeof(string));

            //Adding Rows to the Table
            dt.Rows.Add("สมัครสอบ", "3129485736183", "นาย เจริญ", "มัธยัสถ์", "ดู");
            dt.Rows.Add("สมัครสอบ", "3109365735049", "นาย อเนก", "ประสงค์", "ดู");
            dt.Rows.Add("สมัครสอบ", "3590198794532", "นาย ทรัพย์", "เปี่ยมสุข", "ดู");

            gvDetail.DataSource = ds;
            gvDetail.DataBind();
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDLAr = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        //private void GetCompayByRequest()
        //{
        //    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
        //    var ls = biz.GetCompanyByRequest(base.UserProfile.CompCode);
        //    BindToDDLAr(ddlCompanyRequest, ls.DataResponse);
        //}

        private void GetProvince()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(message);
            BindToDDL(ddlProvinceCurrentAddress, ls);
            BindToDDL(ddlProvinceRegisterAddress, ls);
        }

        private void GetTypeDocument()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetRequestLicenseType("");
            BindToDDLAr(ddlLicenseType, ls.DataResponse);
            ddlLicenseType.Items.RemoveAt(0);
        }

        protected void hplViewDoc_Click(object sender, EventArgs e)
        {
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            var strNumber = (Label)gv.FindControl("lblIDNumberGv");
            var strGroupNumber = (Label)gv.FindControl("lblGroupIDNumberGv");
            var status = (Label)gv.FindControl("lblStatus");
            if (status.Text != "")
            {
                //btnSubmit.Enabled = false;
                //chkCodition.Checked = true;
                //chkCodition.Enabled = false;
            }
            else
            {
                //btnSubmit.Enabled = true;
                //chkCodition.Checked = false;
                //chkCodition.Enabled = true;
            }
            hdfNumber.Value = strNumber.Text;
            hdfGroupNumber.Value = strGroupNumber.Text;
            var biz = new BLL.LicenseBiz();
            var res = biz.GetLicenseVerifyDetail(strGroupNumber.Text, strNumber.Text);

            pnlDetail.Visible = true;

            if (!string.IsNullOrEmpty(res.DataResponse.NAMES))
            {
                txtFirstName.Text = res.DataResponse.NAMES;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.LASTNAME))
            {
                txtLastName.Text = res.DataResponse.LASTNAME;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.ID_CARD_NO))
            {
                txtIDNumber.Text = res.DataResponse.ID_CARD_NO;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.TITLE_NAME))
            {
                txtTitle.Text = res.DataResponse.TITLE_NAME;
            }
            if (!string.IsNullOrEmpty(Convert.ToString((res.DataResponse.LICENSE_DATE))))
            {
                txtDateLicense.Text = Convert.ToString(res.DataResponse.LICENSE_DATE);
            }
            if (!string.IsNullOrEmpty(Convert.ToString(res.DataResponse.LICENSE_EXPIRE_DATE)))
            {
                txtExpireDate.Text = Convert.ToString(res.DataResponse.LICENSE_EXPIRE_DATE);
            }
            if (!string.IsNullOrEmpty(res.DataResponse.LICENSE_NO))
            {
                txtNumberLicense.Text = res.DataResponse.LICENSE_NO;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.EMAIL))
            {
                txtEmail.Text = res.DataResponse.EMAIL;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.RENEW_TIMES))
            {
                txtTimeMove.Text = res.DataResponse.RENEW_TIMES;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.OLD_COMP_CODE))
            {
                txtCompCode.Text = res.DataResponse.OLD_COMP_CODE;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(res.DataResponse.FEES)))
            {
                txtFee.Text = Convert.ToString(res.DataResponse.FEES);
            }
            if (!string.IsNullOrEmpty(res.DataResponse.CURRENT_ADDRESS))
            {
                txtCurrentAddress.Text = res.DataResponse.CURRENT_ADDRESS;
            }
            if (!string.IsNullOrEmpty(res.DataResponse.LOCAL_ADDRESS))
            {
                txtRegisterAddress.Text = res.DataResponse.LOCAL_ADDRESS;
            }


            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz dataCenter = new BLL.DataCenterBiz();
            ddlProvinceCurrentAddress.SelectedValue = res.DataResponse.CURRENT_PROVINCE_CODE;
            var lsPC = dataCenter.GetAmpur(message, ddlProvinceCurrentAddress.SelectedValue);
            BindToDDL(ddlDistrictCurrentAddress, lsPC);

            ddlDistrictCurrentAddress.SelectedValue = res.DataResponse.CURRENT_AMPUR_CODE;
            var lsTC = dataCenter.GetTumbon(message, ddlProvinceCurrentAddress.SelectedValue, ddlDistrictCurrentAddress.SelectedValue);
            BindToDDL(ddlParishCurrentAddress, lsTC);

            ddlParishCurrentAddress.SelectedValue = res.DataResponse.CURRENT_TUMBON_CODE;

            ddlProvinceRegisterAddress.SelectedValue = res.DataResponse.LOCAL_PROVINCE_CODE;
            var lsPR = dataCenter.GetAmpur(message, ddlProvinceRegisterAddress.SelectedValue);
            BindToDDL(ddlDistrictRegisterAddress, lsPR);

            ddlDistrictRegisterAddress.SelectedValue = res.DataResponse.LOCAL_AMPUR_CODE;
            var lsTR = dataCenter.GetTumbon(message, ddlProvinceRegisterAddress.SelectedValue, ddlDistrictRegisterAddress.SelectedValue);
            BindToDDL(ddlParishRegisterAddress, lsTR);

            ddlParishRegisterAddress.SelectedValue = res.DataResponse.LOCAL_TUMBON_CODE;


            UpdatePanelSearch.Update();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                GetLicenseVerify();
                pnlSearch.Visible = true;
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                UCModalError.ShowModalError();
            }
        }

        private void GetLicenseVerify()
        {
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            Func<string, string> GetCrit = anyString =>
            {
                return string.IsNullOrEmpty(anyString) ? string.Empty : anyString;
            };

            string strLicenseType = GetCrit(ddlLicenseType.SelectedValue);
            if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                DateTime dtStartDate = Convert.ToDateTime(GetCrit(txtStartDate.Text));
                DateTime dtEndDate = Convert.ToDateTime(GetCrit(txtEndDate.Text));


                var ls = biz.GetLicenseVerifyByRequest(strLicenseType, dtStartDate, dtEndDate, UserProfile.CompCode);
                DataSet ds = ls.DataResponse;

                gvDetail.DataSource = ds;
                gvDetail.DataBind();


            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.PleaseSelectDate;
                UCModalError.ShowModalError();
            }
        }

        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndDate.Text = string.Empty;
        }

        protected void btnOverAllApprove_Click(object sender, EventArgs e)
        {
            var data = new List<DTO.SubmitLicenseVerify>();

            if (gvDetail.Visible == true)
            {
                foreach (GridViewRow gr in gvDetail.Rows)
                {
                    if (((CheckBox)gr.FindControl("chkSelectVerify")).Checked == true)
                    {
                        var strSeqNo = (Label)gr.FindControl("lblIDNumberGv");
                        var strUploadGroupNo = (Label)gr.FindControl("lblGroupIDNumberGv");

                        data.Add(new DTO.SubmitLicenseVerify
                        {
                            SeqNo = strSeqNo.Text,
                            UploadGroupNo = strUploadGroupNo.Text,
                        });
                    }
                }
            }

            if (data != null)
            {
                var biz = new BLL.LicenseBiz();

                var res = biz.ApproveLicenseVerify(data, "Y", base.UserProfile.CompCode);

                if (res.IsError)
                {
                    var errorMsg = res.ErrorMsg;

                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    UCModalSuccess.ShowMessageSuccess = SysMessage.Approval;
                    UCModalSuccess.ShowModalSuccess();
                    GetLicenseVerify();
                    pnlDetail.Visible = false;
                    hdfGroupNumber.Value = string.Empty;
                    hdfNumber.Value = string.Empty;
                }
            }

        }

        protected void gvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = ((Label)e.Row.FindControl("lblStatus"));

                if (!string.IsNullOrEmpty(status.Text))
                {
                    status.Text = status.Text == "N" ? Resources.propSysMessage_LicenseApproveN : Resources.propSysMessage_LicenseApproveP;
                }
                else
                {
                    status.Text = Resources.propRequestVerifydoc_status;
                }
            }
        }






    }
}