using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using System.Data;
using IAS.MasterPage;
using IAS.DTO;

namespace IAS.UserControl
{
    public partial class ucRenewLicense : System.Web.UI.UserControl
    {
        #region Public Param & Session
        public GridView GridviewRenewLicense
        {
            get
            {
                return gvRenewLicense;
            }
            set
            {
                gvRenewLicense = value;
            }

        }
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPersonLicense();
                GetSelectedRecord();
            }
        }
        #endregion


        #region Main Public & Private Function
        public void GetPersonLicense()
        {
            LicenseBiz biz = new LicenseBiz();
            this.MasterLicense.CurrentExpiredLicneseList.Clear();

            //ต่ออายุก่อนหมดอายุ 60วัน
            if (this.MasterLicense.Menu.Equals(2))
            {
                DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetRenewLicneseByIdCard(this.MasterLicense.UserProfile.IdCard);
                if (res.DataResponse != null)
                {
                    gvRenewLicense.DataSource = res.DataResponse;
                    gvRenewLicense.DataBind();
                }
                else if (res.IsError)
                {

                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();

                }

            }
            //หมดอายุขอต่อใหม่
            else if (this.MasterLicense.Menu.Equals(3))
            {
                DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetExpiredLicneseByIdCard(this.MasterLicense.UserProfile.IdCard);
                if (res.DataResponse != null)
                {
                    this.MasterLicense.CurrentExpiredLicneseList = res.DataResponse.ToList();
                    gvRenewLicense.DataSource = res.DataResponse;
                    gvRenewLicense.DataBind();
                }
                else if (res.IsError)
                {

                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();

                }

            }


        }

        private void GetSelectedRecord()
        {
            for (int i = 0; i < gvRenewLicense.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)gvRenewLicense.Rows[i].Cells[0].FindControl("rdCheck");
                if (rb != null)
                {
                    HiddenField hf = (HiddenField)gvRenewLicense.Rows[i].Cells[0].FindControl("HiddenField1");
                    Label lblLicenseCode = (Label)gvRenewLicense.Rows[i].Cells[0].FindControl("lblLicenseTypeCode");
                    Label lblRenewType = (Label)gvRenewLicense.Rows[i].Cells[0].FindControl("lblRenewType");
                    Label lblRenewTime = (Label)gvRenewLicense.Rows[i].Cells[0].FindControl("lblRenewTime");

                    if (rb.Checked == true)
                    {
                        if ((hf != null) && (lblLicenseCode.Text != null))
                        {
                            this.MasterLicense.SelectedLicenseNo = hf.Value;
                            this.MasterLicense.LicenseTypeCode = lblLicenseCode.Text;
                            //Session["SelectedLicenseNo"] = hf.Value;

                            if (lblRenewType.Text != null)
                            {
                                this.MasterLicense.RenewPetitionName = lblRenewType.Text;
                            }

                        }

                        break;
                    }
                }
            }
        }

        private void SetSelectedRecord()
        {
            for (int i = 0; i < gvRenewLicense.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)gvRenewLicense.Rows[i].Cells[0].FindControl("rdCheck");
                if (rb != null)
                {
                    HiddenField hf = (HiddenField)gvRenewLicense.Rows[i].Cells[0].FindControl("HiddenField1");
                    if (hf != null && this.MasterLicense.SelectedLicenseNo != null)
                    {
                        if (hf.Value.Equals(this.MasterLicense.SelectedLicenseNo.ToString()))
                        {
                            rb.Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        #endregion


        #region UI Function
        protected void gvRenewLicense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //var radioList = e.Row.FindControl("rdCheck") as RadioButtonList;

                //// this will be the object that you are binding to the grid
                //var myObject = e.Row.DataItem as DataRowView;
                //bool isSelected = bool.Parse(myObject["is_selected"].ToString());

                //radioList.SelectedValue = isSelected ? "1" : "0";

                if (gvRenewLicense.DataSource != null)
                {

                    //Label lblApproveDoc = (Label)e.Row.FindControl("lblApproveDoc");

                    //if (lblApproveDoc.Text.Equals(SysMessage.LicenseApproveW) || lblApproveDoc.Text.Equals(SysMessage.LicenseApproveN))
                    //{
                    //    e.Row.Enabled = false;
                    //}
                }
            }

        }

        //Get RenewPersonLicense Entity
        protected void rbtnSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.GetSelectedRecord();

        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            gvRenewLicense.PageIndex = e.NewPageIndex;
            gvRenewLicense.DataBind();
            SetSelectedRecord();
        }

        #endregion
    }
}