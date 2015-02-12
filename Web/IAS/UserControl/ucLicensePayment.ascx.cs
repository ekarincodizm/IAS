using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAS.BLL;
using IAS.MasterPage;

namespace IAS.UserControl
{
    public partial class ucLicensePayment : System.Web.UI.UserControl
    {
        #region Public & Private Session
        public class lsPrint
        {
            //public int str
            public string HEAD_REQUEST_NO { get; set; }
            public string GROUP_REQUEST_NO { get; set; }
            public string PERSON_NO { get; set; }
            public string GROUP_AMOUNT { get; set; }
            public string SUBPAYMENT_DATE { get; set; }
            public string REMARK { get; set; }
        }

   

        public GridView GridPayment { get { return gvPayment; } set { gvPayment = value; } }
        public Label lblGvHead { get { return lblPaymentHead; } set { lblPaymentHead = value; } }

        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LicenseValidate();

            }

        }
        #endregion

        #region Public & Private Function
        private void LicenseValidate()
        {


        }


        private void GetSelectedRecord()
        {
            LicenseBiz bizLicense = new LicenseBiz();

            for (int i = 0; i < gvPayment.Rows.Count; i++)
            {
                CheckBox rb = (CheckBox)gvPayment.Rows[i].Cells[0].FindControl("chkSelect");

                if (rb != null)
                {
                    HiddenField hf = (HiddenField)gvPayment.Rows[i].Cells[0].FindControl("HiddenField1");

                    if (rb.Checked)
                    {
                        //HiddenField hf = (HiddenField)gvPayment.Rows[i].Cells[0].FindControl("HiddenField1");
                        if (hf != null)
                        {
                            this.MasterLicense.SelectedUploadGroupNo = hf.Value;

                            if (this.MasterLicense.lsLicensePayment.Count > 0)
                            {
                                DTO.ResponseService<DTO.PersonLicenseTransaction[]> resLicense = bizLicense.getViewPersonLicense(this.MasterLicense.UserProfile.IdCard, "A");
                                if (resLicense.DataResponse != null)
                                {
                                    DTO.PersonLicenseTransaction licenseD = resLicense.DataResponse.Where(gno => gno.UPLOAD_GROUP_NO == hf.Value).FirstOrDefault();
                                    if (licenseD != null)
                                    {
                                        ///Get & Set SubGroupPayment
                                        this.MasterLicense.lsLicensePayment.Add(new DTO.SubGroupPayment
                                        {
                                            uploadG = licenseD.UPLOAD_GROUP_NO,
                                            LicenseNo = licenseD.LICENSE_NO,
                                            RenewTime = licenseD.RENEW_TIMES,
                                            seqNo = licenseD.SEQ_NO,
                                            PaymentType = licenseD.PETITION_TYPE_CODE
                                        });
                                    }

                                }
                            }
                            else
                            {
                                DTO.ResponseService<DTO.PersonLicenseTransaction[]> resLicense = bizLicense.getViewPersonLicense(this.MasterLicense.UserProfile.IdCard, "A");
                                if (resLicense.DataResponse != null)
                                {
                                    DTO.PersonLicenseTransaction licenseD = resLicense.DataResponse.Where(gno => gno.UPLOAD_GROUP_NO == hf.Value).FirstOrDefault();
                                    if (licenseD != null)
                                    {
                                        ///Get & Set SubGroupPayment
                                        this.MasterLicense.lsLicensePayment.Add(new DTO.SubGroupPayment
                                        {
                                            uploadG = licenseD.UPLOAD_GROUP_NO,
                                            LicenseNo = licenseD.LICENSE_NO,
                                            RenewTime = licenseD.RENEW_TIMES,
                                            seqNo = licenseD.SEQ_NO,
                                            PaymentType = licenseD.PETITION_TYPE_CODE
                                        });
                                    }
                                }

                                //this.MasterLicense.ListUploadGroupNo.Add(this.MasterLicense.SelectedUploadGroupNo);
                            }
                            //this.MasterLicense.ListUploadGroupNo.Add(this.MasterLicense.SelectedUploadGroupNo);
                        }
                        break;
                    }
                    else
                    {
                        //HiddenField hf = (HiddenField)gvPayment.Rows[i].Cells[0].FindControl("HiddenField1");
                        if (hf != null)
                        {
                            this.MasterLicense.SelectedUploadGroupNo = hf.Value;
                            this.MasterLicense.ListUploadGroupNo.Remove(this.MasterLicense.SelectedUploadGroupNo);
                        }
                        break;

                    }
                }
            }
        }

        #endregion

        #region UI Function
        protected void gvPayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            PaymentBiz bizPay = new PaymentBiz();
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            List<string> lsRegNo = new List<string>();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gvPayment.DataSource != null)
                {
                    Label lblApproveDoc = (Label)e.Row.FindControl("lblApproveDoc");
                    if (lblApproveDoc != null)
                    {
                        if (lblApproveDoc.Text.Equals(SysMessage.LicenseApproveW) || lblApproveDoc.Text.Equals(SysMessage.LicenseApproveN))
                        {
                            e.Row.Enabled = false;
                        }
                        else
                        {
                            e.Row.Enabled = true;

                            #region สร้างใบสั่งจ่ายย่อย > NewCreatePayment

                            //Label lblRequestNo = (Label)e.Row.FindControl("lblHeadRequestNo");
                            //if (lblRequestNo != null)
                            //{
                                
                            //    lsRegNo.Add(lblRequestNo.Text);

                            //    string ref1 = string.Empty;
                            //    //res = bizPay.NewCreatePayment(lsRegNo.ToArray(), "", this.MasterLicense.UserProfile.Id, "", Convert.ToString(this.MasterLicense.UserProfile.MemberType), );
                            //    res = bizPay.NewCreatePayment(lsRegNo.ToArray(), "", this.MasterLicense.UserProfile.Id, "", Convert.ToString(this.MasterLicense.UserProfile.MemberType), out ref1);


                            //    //Get RefNo
                            //    string refNo = ref1;
                            //    ImageButton btnPayment = (ImageButton)e.Row.FindControl("btnPayment");
                            //    btnPayment.Attributes.Add("onclick", string.Format("OpenPopup('{0}')", refNo));

                            //    if ((res.IsError) || (res.ResultMessage == false))
                            //    {

                            //        this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                            //        this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                            //    }
                            //    else
                            //    {
                            //        //this.MasterLicense.UCLicenseModelSuccess.ShowMessageSuccess = SysMessage.CreatePaymentSuccess;
                            //        //this.MasterLicense.UCLicenseModelSuccess.ShowModalSuccess();
                            //    }

                            //}

                            #endregion
                        }
                    }
                }
            }

        }

        protected void btnPayment_Click(object sender, ImageClickEventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var data = new List<lsPrint>();

            //var lblPaidGroupGv = (Label)gr.FindControl("lblPaidGroupGv");
            var lblGroupRequsetNo = (Label)gr.FindControl("lblRequestNo");
            //จำนวนคน
            //var lblAmountPeopleGv = (Label)gr.FindControl("lblAmountPeopleGv");
            var lblAmountPeopleGv = "1";
            var lblAmountMoneyGv = (Label)gr.FindControl("lblMoney");
            var lblDatePayGv = (Label)gr.FindControl("lblTranDate");
            //var lblRemarkGv = (Label)gr.FindControl("lblRemarkGv");
            var lblRemarkGv = "DXXX";

            data.Add(new lsPrint
            {

                //HEAD_REQUEST_NO = lblPaidGroupGv == null ? "" : lblPaidGroupGv.Text,
                GROUP_REQUEST_NO = lblGroupRequsetNo == null ? "" : lblGroupRequsetNo.Text,
                PERSON_NO = lblAmountPeopleGv == null ? "" : lblAmountPeopleGv,
                GROUP_AMOUNT = lblAmountMoneyGv == null ? "" : lblAmountMoneyGv.Text,
                SUBPAYMENT_DATE = lblDatePayGv == null ? "" : lblDatePayGv.Text,
                REMARK = lblRemarkGv == null ? "" : lblRemarkGv
            });

            Session["lsRecivePrint"] = data;

            Session["MemberType"] = this.MasterLicense.UserProfile.MemberType;

            udpPayment.Update();


        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.GetSelectedRecord();


        }

        /// <summary>
        /// แก้ไขBug 4/7/2557
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPayment_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkBxSelect");
                CheckBox chkBxHeader = (CheckBox)this.gvPayment.HeaderRow.FindControl("chkBxHeader");
                chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);
            }
        }

        #endregion

    }
}