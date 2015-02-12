using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.MasterPage;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL;
using System.Text;
using System.Data;

namespace IAS.License
{
    public partial class LicensePayment : basepage
    {
        #region Private & Public Session
        public MasterLicense MasterLicense
        {
            get
            {
                return (this.Page.Master as MasterLicense);
            }

        }
        public List<string> ListPaymentLicense
        {
            get
            {
                if (Session["ListPaymentLicense"] == null)
                {
                    Session["ListPaymentLicense"] = new List<string>();
                }

                return (List<string>)Session["ListPaymentLicense"];
            }

            set
            {
                Session["ListPaymentLicense"] = value;
            }
        }
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuInit();
            }

        }
        #endregion

        #region Public & Private Fuction
        private void MenuInit()
        {
            GetLicenseDetail();

        }

        private void GetLicenseDetail()
        {
            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = this.MasterLicense.GetPaymentLicenseTransaction();

            if (res.DataResponse != null)
            {
                //Disable Status = "W" || "รออนุมัติ"

                res.DataResponse.ToList().ForEach(lic =>
                {
                    if (lic.FEES == 0)
                    {
                        lic.PETITION_TYPE_CODE = "ใบแทนใบอนุญาต(เปลี่ยนชื่อ-สกุล)";
                    }
                });


                if (res.DataResponse.Count() > 0)
                {
                    //Change


                    this.ucLicensePaymentY.GridPayment.DataSource = res.DataResponse;
                    this.ucLicensePaymentY.GridPayment.DataBind();

                    int getApproveStatus = res.DataResponse.Where(status => status.APPROVED == DTO.ApprocLicense.W.ToString()).ToList().Count;
                    if (getApproveStatus == res.DataResponse.Count())
                    {
                        btnPayment.Enabled = false;
                        btnPayment.Visible = false;
                        btnCancel.Enabled = false;
                        btnCancel.Visible = false;

                        CheckBox Chkall = (CheckBox)this.ucLicensePaymentY.GridPayment.HeaderRow.FindControl("chkBxHeader");
                        Chkall.Enabled = false;
                    }

                    //Show notice
                    //status.APPROVED = W
                    //แสดงข้อความ "ดำเนินการเรียบร้อย กรุณารออีเมลแจ้งผลการตรวจสอบเอกสารจากระบบ"
                    if (res.DataResponse.Where(status => status.APPROVED == DTO.ApprocLicense.W.ToString()).ToList().Count > 0)
                    {
                        this.SetProperties(true);
                    }
                    else
                    {
                        this.SetProperties(false);
                    }
                }
                else if (res.DataResponse.Count() == 0)
                {
                    this.ucLicensePaymentY.GridPayment.DataSource = res.DataResponse;
                    this.ucLicensePaymentY.GridPayment.DataBind();

                    btnPayment.Enabled = false;
                    btnPayment.Visible = false;
                    btnCancel.Enabled = false;
                    btnCancel.Visible = false;

                    CheckBox Chkall = (CheckBox)this.ucLicensePaymentY.GridPayment.HeaderRow.FindControl("chkBxHeader");
                    Chkall.Enabled = false;

                    this.SetProperties(false);
                }

              

            }
            else
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.IsError.ToString();
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;

            }
        }

        private int GetSumConfigViewBillPayment()
        {
            int SumAllPayment = 0;
            PaymentBiz biz = new PaymentBiz();
            DTO.ResponseService<DataSet> ViewBillPayment = biz.GetConfigViewBillPayment();
            //SumAllPayment = ViewBillPayment.DataResponse.Tables[0].Rows.Count;

            if (ViewBillPayment.DataResponse.Tables[0].Rows.Count > 0)
            {
                DataRow drTran = ViewBillPayment.DataResponse.Tables[0].Rows[0];
                SumAllPayment = Convert.ToInt16(drTran["ITEM_VALUE"]);
            }
            return SumAllPayment;
        }

        private void SetProperties(bool obj)
        {
            if (obj == true)
            {
                this.lblNotice.ForeColor = System.Drawing.Color.Blue;
                this.lblNotice.Text = "ดำเนินการเรียบร้อย กรุณารออีเมลแจ้งผลการตรวจสอบเอกสารจากระบบ";
            }
            else
            {
                this.lblNotice.Text = string.Empty;
            }

            this.lblNotice.Font.Bold = obj;
            this.lblNotice.Visible = obj;

        }

        #endregion

        #region UI
        protected void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentBiz biz = new PaymentBiz();
            LicenseBiz bizLicense = new LicenseBiz();
            string ref1 = string.Empty;
            string group = string.Empty;
            string headrequestNo = string.Empty;
            StringBuilder ChkUpload = new StringBuilder();

            if (this.ucLicensePaymentY.GridPayment.Rows.Count <= 0)
            {
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.LicenseYNotFound;
                this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                this.MasterLicense.UpdatePanelLicense.Update();
                return;
            }
            else
            {
                #region Get UploadGroupNo

                for (int i = 0; i < ucLicensePaymentY.GridPayment.Rows.Count; i++)
                {
                    CheckBox chkDelete = (CheckBox)ucLicensePaymentY.GridPayment.Rows[i].Cells[0].FindControl("chkBxSelect");
                    if (chkDelete != null)
                    {
                        if (chkDelete.Checked)
                        {
                            HiddenField hf = (HiddenField)ucLicensePaymentY.GridPayment.Rows[i].Cells[0].FindControl("HiddenField1");

                            if (hf.Value != null)
                            {
                                //Get
                                DTO.ResponseService<DTO.PersonLicenseTransaction> resD = bizLicense.GetLicenseDetailByUploadGroupNo(hf.Value);
                                if ((resD.DataResponse != null) && (!resD.IsError))
                                {
                                    ///Get & Set SubGroupPayment
                                   var resLS = this.MasterLicense.lsLicensePayment.Where(ls => ls.uploadG == resD.DataResponse.UPLOAD_GROUP_NO).FirstOrDefault();
                                    if (resLS ==null){

                                        this.MasterLicense.lsLicensePayment.Add(new DTO.SubGroupPayment
                                        {
                                            uploadG = resD.DataResponse.UPLOAD_GROUP_NO,
                                            LicenseNo = resD.DataResponse.LICENSE_NO,
                                            RenewTime = resD.DataResponse.RENEW_TIMES,
                                            seqNo = resD.DataResponse.SEQ_NO,
                                            PaymentType = resD.DataResponse.PETITION_TYPE_CODE
                                        });

                                        this.MasterLicense.ListUploadGroupNo.Add(hf.Value);
                                    }
                                }


                            }
                        }
                    }
                }
                #endregion

                //ตรวจสอบรายการต่อใบสั่งจ่าย @1/7/2557
                int getSumConfig = GetSumConfigViewBillPayment();
                if (this.MasterLicense.ListUploadGroupNo.Count > getSumConfig)
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = "กรุณาตรวจสอบจำนวนรายการใบอนุญาตที่ต้องการออกใบสั่งจ่าย";
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;
                }

                #region สร้างใบสั่งจ่ายย่อย > NewCreatePayment

                DTO.ResponseService<string> res = biz.SetSubGroupSingleLicense(this.MasterLicense.lsLicensePayment.ToArray(), this.UserProfile.Id, this.UserProfile.Id, out ref1);
                if ((res.DataResponse != null) && (!res.IsError))
                {
                    this.MasterLicense.UCLicenseUCLicenseModelSuccess.ShowMessageSuccess = SysMessage.CreatePaymentSuccess;
                    this.MasterLicense.UCLicenseUCLicenseModelSuccess.ShowModalSuccess();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    group = ref1;

                    ChkUpload.Append(group);
                    ChkUpload.Append(" ");
                    ChkUpload.Append(this.UserProfile.Id);

                    //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + group + "')", true);
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "OpenPopup('" + ChkUpload.ToString() + "')", true);

                    this.MasterLicense.lsLicensePayment.Clear();
                    this.MasterLicense.ListUploadGroupNo.Clear();
                    this.MasterLicense.PersonLicenseD.Clear();
                    this.MasterLicense.PersonLicenseH.Clear();
                    this.GetLicenseDetail();
                }
                else
                {
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                    this.MasterLicense.UpdatePanelLicense.Update();
                    return;
                }
                //this.MasterLicense.lsLicensePayment.Clear();
                //this.MasterLicense.ListUploadGroupNo.Clear();
                #endregion

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BankPage.aspx");
        }

        #endregion

    }
}
