using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL;

namespace IAS.UserControl
{
    public partial class ucReplaceLicense : System.Web.UI.UserControl
    {
        #region Public Param & Session
        public GridView GridviewRenewLicense
        {
            get
            {
                return gvReplaceLicense;
            }
            set
            {
                gvReplaceLicense = value;
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
                GetAllLicenseByIDCard();
                GetSelectedRecord();

                if (this.MasterLicense != null)
                {
                    if (this.MasterLicense.Menu.Equals(4))
                    {
                        //ใบแทนใบอนุญาต(ชำรุดสูญหาย)
                        lblHeaderDetail.Text = "เลือกรายการใบแทนใบอนุญาต(ชำรุดสูญหาย)";
                    }
                    else
                    {
                        //ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)
                        lblHeaderDetail.Text = "เลือกรายการใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)";

                    }
                }
            }
        }
        #endregion

        #region Main Public & Private Function
        public void GetAllLicenseByIDCard()
        {
            LicenseBiz biz = new LicenseBiz();
            Int32 FeeMode = 1;

            //Get All Licese By ID_CARD_NO

            if (this.MasterLicense != null)
            {
                if (this.MasterLicense.Menu.Equals(4))
                {
                    //ใบแทนใบอนุญาต(ชำรุดสูญหาย)
                    FeeMode = 1;
                }
                else
                {
                    //ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)
                    FeeMode = 2;

                }
            }

            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetAllLicenseByIDCard(this.MasterLicense.UserProfile.IdCard, "1", FeeMode);
            if (res.DataResponse != null)
            {
                //ใบแทนใบอนุญาต(ชำรุดสูญหาย) Fee = GetDB()
                if (this.MasterLicense.Menu.Equals(4))
                {
                    gvReplaceLicense.DataSource = res.DataResponse;
                    gvReplaceLicense.DataBind();
                }
                //ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล) Fee = 0
                else
                {
                    res.DataResponse.ToList().ForEach(x => x.FEES = 0);
                    gvReplaceLicense.DataSource = res.DataResponse;
                    gvReplaceLicense.DataBind();
                }
                
            }
            else if (res.IsError)
            {

                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();

            }

        }

        private void GetSelectedRecord()
        {
            for (int i = 0; i < gvReplaceLicense.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)gvReplaceLicense.Rows[i]
                                .Cells[0].FindControl("rdCheck");
                if (rb != null)
                {
                    HiddenField hf = (HiddenField)gvReplaceLicense.Rows[i].Cells[0].FindControl("HiddenField1");
                    Label lblLicenseCode = (Label)gvReplaceLicense.Rows[i].Cells[0].FindControl("lblLicenseTypeCode");
                    Label lblRenewDate = (Label)gvReplaceLicense.Rows[i].Cells[0].FindControl("lblRenewDate");
                    Label lblExpireDate = (Label)gvReplaceLicense.Rows[i].Cells[0].FindControl("lblExpireDate");

                    if (rb.Checked == true)
                    {
                        if ((hf != null) && (lblLicenseCode.Text != null))
                        {
                            this.MasterLicense.SelectedLicenseNo = hf.Value;
                            this.MasterLicense.LicenseTypeCode = lblLicenseCode.Text;

                            if (lblRenewDate.Text == "")
                            {
                                this.MasterLicense.SelectedReplaceLicenseDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                            }
                            else
                            {
                                this.MasterLicense.SelectedReplaceLicenseDate = lblRenewDate.Text;
                            }

                            if (lblExpireDate.Text == "")
                            {
                                this.MasterLicense.SelectedReplaceLicenseDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
                            }
                            else
                            {
                                this.MasterLicense.SelectedReplaceLicenseExpireDate = lblExpireDate.Text;
                            }
                        }

                        break;
                    }
                }
            }
        }

        private void SetSelectedRecord()
        {
            for (int i = 0; i < gvReplaceLicense.Rows.Count; i++)
            {
                RadioButton rb = (RadioButton)gvReplaceLicense.Rows[i].Cells[0]
                                                .FindControl("rdCheck");
                if (rb != null)
                {
                    HiddenField hf = (HiddenField)gvReplaceLicense.Rows[i]
                                        .Cells[0].FindControl("HiddenField1");
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
        protected void gvReplaceLicense_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gvReplaceLicense.DataSource != null)
                {

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
            gvReplaceLicense.PageIndex = e.NewPageIndex;
            gvReplaceLicense.DataBind();
            SetSelectedRecord();
        }
        #endregion


    }
}