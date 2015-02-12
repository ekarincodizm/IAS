using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Text;
using IAS.Utils;
using System.Globalization;
using System.Threading;
using System.Net;
using System.ComponentModel;
using System.IO;
using System.Data;
using IAS.Class;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System.Configuration;
using IAS.BLL;
using IAS.Properties;

namespace IAS.Payment
{
    public partial class PaymentDetail : basepage
    {
        string a = "";


        public class lsPrint
        {
            //public int str
            public string strBillNumber { get; set; }
            public string strPaymentType { get; set; }
            public string strIDNumber { get; set; }
            public string strFirstName { get; set; }
            public string strLastName { get; set; }
            public string strTestingNo { get; set; }
            public string strCompanyCode { get; set; }
            public string strExamPlaceCode { get; set; }
        }

        public class RECIVE_PARTH
        {
            public string rcv_path { get; set; }
        }

        public string FileName
        {
            get { return Session["FileName"] == null ? string.Empty : Session["FileName"].ToString(); }
            set { Session["FileName"] = value; }
        }

        public string ConfigViewYear
        {
            get { return Session["ConfigViewYear"] == null ? string.Empty : Session["ConfigViewYear"].ToString(); }
            set { Session["ConfigViewYear"] = value; }
        }

        #region Public Param & Session
        public List<string> ListChkAll
        {
            get
            {
                if (Session["ListChkAll"] == null)
                {
                    Session["ListChkAll"] = new List<string>();
                }

                return (List<string>)Session["ListChkAll"];
            }

            set
            {
                Session["ListChkAll"] = value;
            }
        }
        public int PAGE_SIZE = 20;
        public int _totalPages;
        public int countCheckDetail = 0;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }
        public List<DTO.ReceiveNo> lsReceiveNo
        {
            get
            {
                if (Session["lsReceiveNo"] == null)
                {
                    Session["lsReceiveNo"] = new List<DTO.ReceiveNo>();
                }

                return (List<DTO.ReceiveNo>)Session["lsReceiveNo"];
            }
            set
            {
                Session["lsReceiveNo"] = value;
            }
        }
        public List<DTO.ReceiveNo> lsReceiveNoPopup
        {
            get
            {
                if (Session["lsReceiveNoPopup"] == null)
                {
                    Session["lsReceiveNoPopup"] = new List<DTO.ReceiveNo>();
                }

                return (List<DTO.ReceiveNo>)Session["lsReceiveNoPopup"];
            }
            set
            {
                Session["lsReceiveNoPopup"] = value;
            }
        }
        public List<DTO.ReceiveNo> lsReceiveNoSingle
        {
            get
            {
                if (Session["lsReceiveNoSingle"] == null)
                {
                    Session["lsReceiveNoSingle"] = new List<DTO.ReceiveNo>();
                }

                return (List<DTO.ReceiveNo>)Session["lsReceiveNoSingle"];
            }
            set
            {
                Session["lsReceiveNoSingle"] = value;
            }
        }
        public MasterPage.Site1 MasterSite
        {

            get { return (this.Page.Master as MasterPage.Site1); }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            txtStartDate.Attributes.Add("readonly", "true");
            txtEndDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                GetPaymentType();
                DefaultData();
                if (DTO.RegistrationType.General.GetEnumValue() == base.UserProfile.MemberType)
                {
                    txtIdCard.Text = base.UserProfile.IdCard;
                    txtIdCard.Enabled = false;
                }
            }

        }
        protected void DefaultData()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            countCheckDetail = 0;
            ddlTypePay.SelectedIndex = 0;
            txtOrder.Text = string.Empty;
            txtIdCard.Text = string.Empty;
            txtReceiptNumber.Text = string.Empty;
        }
        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var TypePay = (Label)gr.FindControl("lblTypePayGv");

            var ApplicantCode = (Label)gr.FindControl("lblApplicantCodeGv");
            var TestingNo = (Label)gr.FindControl("lblTestingNoGv");
            var ExamPlaceCode = (Label)gr.FindControl("lblExamPlaceCodeGv");

            var LicenseNo = (Label)gr.FindControl("lblLicenseNoGv");
            var RenewTime = (Label)gr.FindControl("lblRenewTimeGv");


            var biz = new BLL.PaymentBiz();

            if (TypePay.Text == Resources.propPaymentDetail_TypePay)
            {
                var res = biz.GetPaymentDetail(ApplicantCode.Text, TestingNo.Text, ExamPlaceCode.Text, string.Empty, string.Empty, true);

                if (res.IsError)
                {
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    var pay = res.DataResponse;

                    txtDetailOrderPay.Text = pay.PAYMENT_NO.Insert(6, " ").Insert(11, " ");
                    txtDetailExamID.Text = pay.TESTING_NO;

                    if (pay.PAYMENT_DATE != null)
                    {
                        txtDetailCheckOutOrderDate.Text = pay.PAYMENT_DATE.Value.ToString("dd/MM/yyyy");
                    }

                    txtDetailTypeOrder.Text = pay.PAYMENT_TYPE_NAME;
                    txtDetailIDCard.Text = pay.ID_CARD_NO;
                    txtDetailCompanyID.Text = pay.COMPANY_CODE;
                    txtDetailLicenseNumber.Text = pay.LICENSE_NO;
                    txtDetailLicenseNumberComplaint.Text = pay.LICENSE_NO_REQUEST;
                    txtDetailReceiptNumber.Text = pay.RECEIPT_NO;

                    if (pay.RECEIPT_DATE != null)
                    {
                        txtDetailReceiptNumberDate.Text = pay.RECEIPT_DATE.Value.ToString("dd/MM/yyyy");
                    }

                    if (pay.AMOUNT != null)
                    {
                        txtAmount.Text = pay.AMOUNT.Value.ToString();
                    }

                    txtDetailTypeLicenseNumber.Text = pay.LICENSE_TYPE_CODE;

                }
            }
            else
            {
                var res = biz.GetPaymentDetail(string.Empty, string.Empty, string.Empty, LicenseNo.Text, RenewTime.Text, false);

                if (res.IsError)
                {
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    var pay = res.DataResponse;

                    txtDetailOrderPay.Text = pay.PAYMENT_NO.Insert(6, " ").Insert(11, " ");
                    txtDetailExamID.Text = pay.TESTING_NO;

                    if (pay.PAYMENT_DATE != null)
                    {
                        txtDetailCheckOutOrderDate.Text = pay.PAYMENT_DATE.Value.ToString("dd/MM/yyyy");
                    }

                    txtDetailTypeOrder.Text = pay.PAYMENT_TYPE_NAME;
                    txtDetailIDCard.Text = pay.ID_CARD_NO;
                    txtDetailCompanyID.Text = pay.COMPANY_CODE;
                    txtDetailLicenseNumber.Text = pay.LICENSE_NO;
                    txtDetailLicenseNumberComplaint.Text = pay.LICENSE_NO_REQUEST;
                    txtDetailReceiptNumber.Text = pay.RECEIPT_NO;

                    if (pay.RECEIPT_DATE != null)
                    {
                        txtDetailReceiptNumberDate.Text = pay.RECEIPT_DATE.Value.ToString("dd/MM/yyyy");
                    }

                    if (pay.AMOUNT != null)
                    {
                        txtAmount.Text = pay.AMOUNT.Value.ToString();
                    }

                    txtDetailTypeLicenseNumber.Text = pay.LICENSE_TYPE_CODE;

                }
            }
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                btnPreviousGvSearch.Visible = false;

                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;


                btngo1.Visible = true;
                txtInputMaxrow1.Visible = true;
                lblHeadInputMaxrow1.Visible = true;
                lblHeadTotal1.Visible = true;
                lblTotalrecord1.Visible = true;
                lblEndTotal1.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;


                btngo1.Visible = true;
                txtInputMaxrow1.Visible = true;
                lblHeadInputMaxrow1.Visible = true;
                lblHeadTotal1.Visible = true;
                lblTotalrecord1.Visible = true;
                lblEndTotal1.Visible = true;
            }
            BindPage();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(lblTotalPage1.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;

                btngo1.Visible = true;
                txtInputMaxrow1.Visible = true;
                lblHeadInputMaxrow1.Visible = true;
                lblHeadTotal1.Visible = true;
                lblTotalrecord1.Visible = true;
                lblEndTotal1.Visible = true;
            }
            else
            {
                txtNumberGvSearch.Text = lblTotalPage1.Text;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;

                btngo1.Visible = true;
                txtInputMaxrow1.Visible = true;
                lblHeadInputMaxrow1.Visible = true;
                lblHeadTotal1.Visible = true;
                lblTotalrecord1.Visible = true;
                lblEndTotal1.Visible = true;
            }
            BindPage();
        }

        private void visibleControl()
        {
            btnPreviousGvSearch.Visible = true;
            txtNumberGvSearch.Visible = true;
            btnNextGvSearch.Visible = true;


        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetPaymentType()
        {
            var message = SysMessage.DefaultAll;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetPaymentType(message);

            BindToDDL(ddlTypePay, ls.DataResponse.ToList());

        }
        private List<RECIVE_PARTH> listPayment
        {
            get
            {
                return Session["lsPaymentPrint"] == null
                              ? new List<RECIVE_PARTH>()
                              : (List<RECIVE_PARTH>)Session["lsPaymentPrint"];
            }
            set
            {
                Session["lsPaymentPrint"] = value;
            }
        }

        protected void chkSelectPayment_CheckedChanged(object sender, EventArgs e)
        {
            string newPath = string.Empty;
            string PathAdd = string.Empty;
            CheckBox checkSelectPayment = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)checkSelectPayment.Parent.Parent;
            var lblPathFile = (Label)gr.FindControl("lblPathFile");
            var lblTypePayGv = (Label)gr.FindControl("lblTypePayGv");
            var lblOrderGv = (Label)gr.FindControl("lblOrderGv");
            var lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
            var lblDatePayGv = (Label)gr.FindControl("lblDatePayGv");
            var lblFirstNameGv = (Label)gr.FindControl("lblFirstNameGv");
            var lblLastNameGv = (Label)gr.FindControl("lblLastNameGv");
            var lblReceiptNumberGv = (Label)gr.FindControl("lblReceiptNumberGv");
            var lblReceiveDate = (Label)gr.FindControl("lblReceiveDate");
            var lblAMOUNT = (Label)gr.FindControl("lblAMOUNT");
            var lblRecordStatus = (Label)gr.FindControl("lblRecordStatus");
            var lblPaymentNoGv = (Label)gr.FindControl("lblPaymentNoGv");
            var lblHeadNoGv = (Label)gr.FindControl("lblHeadNoGv");
            LinkButton ltnRecordStatus = (LinkButton)gr.FindControl("ltnRecordStatus");
            var lblUserUpload = (Label)gr.FindControl("lblUserUpload");
            if (lblUserUpload.Text.Length > 4)
            {
                if (lblUserUpload.Text != base.UserProfile.Id)
                {
                    newPath = "_COPY.";
                }
                else
                {
                    newPath = ".";
                }
            }
            else
            {
                if (lblUserUpload.Text != base.UserProfile.CompCode)
                {
                    newPath = "_COPY.";
                }
                else
                {
                    newPath = ".";
                }
            }
            if (lblPathFile.Text != "")
            {
                string[] PathS = lblPathFile.Text.Split('.');
                PathAdd = PathS[0] + newPath + PathS[1];
            }
            if (checkSelectPayment.Checked)
            {
                if (ltnRecordStatus.Text == "กรณีแบ่งจ่าย")
                {
                    var biz = new BLL.PaymentBiz();
                    var getAll = biz.GetReceiptSomePay(lblPaymentNoGv.Text, lblHeadNoGv.Text);
                    if (getAll.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < getAll.DataResponse.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = getAll.DataResponse.Tables[0].Rows[i];
                            string[] PathSomepay = dr["RECEIVE_PATH"].ToString().Split('.');
                            string PathSomepayAdd = PathSomepay[0] + newPath + PathSomepay[1];
                            lsReceiveNo.Add(new DTO.ReceiveNo
                              {

                                  rcv_path = PathSomepayAdd,

                              });

                        }

                    }
                }
                else
                {
                    lsReceiveNo.Add(new DTO.ReceiveNo
                    {
                        rcv_path = PathAdd,
                        PaymentType = lblTypePayGv.Text,
                        GroupRequestNo = lblOrderGv.Text,
                        id_card = lblIDCardGv.Text,
                        groupDate = lblDatePayGv.Text,
                        FirstName = lblFirstNameGv.Text,
                        LastName = lblLastNameGv.Text,
                        ReceiptNumber = lblReceiptNumberGv.Text,
                        ReceiptDate = lblReceiveDate.Text,
                        Amt = lblAMOUNT.Text
                    });
                }
            }
            else
            {
                if (ltnRecordStatus.Text == "กรณีแบ่งจ่าย")
                {
                    var biz = new BLL.PaymentBiz();
                    var getAll = biz.GetReceiptSomePay(lblPaymentNoGv.Text, lblHeadNoGv.Text);
                    if (getAll.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < getAll.DataResponse.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = getAll.DataResponse.Tables[0].Rows[i];
                            string[] PathSomepay = dr["RECEIVE_PATH"].ToString().Split('.');
                            string PathSomepayAdd = PathSomepay[0] + newPath + PathSomepay[1];
                            var pament = lsReceiveNo.FirstOrDefault(x => x.rcv_path == PathSomepayAdd);
                            ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("Checkall")).Checked = false;
                            lsReceiveNo.Remove(pament);
                        }

                    }
                }
                else
                {
                    var pament = lsReceiveNo.FirstOrDefault(x => x.rcv_path == PathAdd);
                    ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("Checkall")).Checked = false;
                    lsReceiveNo.Remove(pament);
                }
            }

        }

        protected void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            string PathAdd = string.Empty;
            CheckBox ck = (CheckBox)sender;
            ListChkAll.Add(txtNumberGvSearch.Text);
            foreach (GridViewRow row in gvSearch.Rows)
            {
                string newPath = string.Empty;
                CheckBox chkSelectPayment = (CheckBox)row.FindControl("chkSelectPayment");
                var lblPathFile = (Label)row.FindControl("lblPathFile");
                var lblTypePayGv = (Label)row.FindControl("lblTypePayGv");
                var lblOrderGv = (Label)row.FindControl("lblOrderGv");
                var lblIDCardGv = (Label)row.FindControl("lblIDCardGv");
                var lblDatePayGv = (Label)row.FindControl("lblDatePayGv");
                var lblFirstNameGv = (Label)row.FindControl("lblFirstNameGv");
                var lblLastNameGv = (Label)row.FindControl("lblLastNameGv");
                var lblReceiptNumberGv = (Label)row.FindControl("lblReceiptNumberGv");
                var lblReceiveDate = (Label)row.FindControl("lblReceiveDate");
                var lblAMOUNT = (Label)row.FindControl("lblAMOUNT");
                var lblRecordStatus = (Label)row.FindControl("lblRecordStatus");
                var lblPaymentNoGv = (Label)row.FindControl("lblPaymentNoGv");
                var lblHeadNoGv = (Label)row.FindControl("lblHeadNoGv");
                var lblUserUpload = (Label)row.FindControl("lblUserUpload");
                if (lblUserUpload.Text.Length > 4)
                {
                    if (lblUserUpload.Text != base.UserProfile.Id)
                    {
                        newPath = "_COPY.";
                    }
                    else 
                    {
                        newPath = ".";
                    }
                }
                else
                {
                    if (lblUserUpload.Text != base.UserProfile.CompCode)
                    {
                        newPath = "_COPY.";
                    }
                    else
                    {
                        newPath = ".";
                    }
                }
                if (lblPathFile.Text != "")
                {
                    string[] PathS = lblPathFile.Text.Split('.');
                    PathAdd = PathS[0] + newPath + PathS[1];
                }
                LinkButton ltnRecordStatus = (LinkButton)row.FindControl("ltnRecordStatus");
                if (ck.Checked)
                {
                    if ((!chkSelectPayment.Checked) && (chkSelectPayment.Enabled))
                    {
                   
                        chkSelectPayment.Checked = true;
                        if (ltnRecordStatus.Text == "กรณีแบ่งจ่าย")
                        {
                            var biz = new BLL.PaymentBiz();
                            var getAll = biz.GetReceiptSomePay(lblPaymentNoGv.Text, lblHeadNoGv.Text);
                            if (getAll.DataResponse.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < getAll.DataResponse.Tables[0].Rows.Count; i++)
                                {
                                    DataRow dr = getAll.DataResponse.Tables[0].Rows[i];
                                    string[] PathSomepay = dr["RECEIVE_PATH"].ToString().Split('.');
                                    string PathSomepayAdd = PathSomepay[0] + newPath + PathSomepay[1];
                                    lsReceiveNo.Add(new DTO.ReceiveNo
                                    {

                                        rcv_path = PathSomepayAdd,

                                    });

                                }
                            }
                        }
                        else
                        {
                            lsReceiveNo.Add(new DTO.ReceiveNo
                            {
                                rcv_path = PathAdd,
                                PaymentType = lblTypePayGv.Text,
                                GroupRequestNo = lblOrderGv.Text,
                                id_card = lblIDCardGv.Text,
                                groupDate = lblDatePayGv.Text,
                                FirstName = lblFirstNameGv.Text,
                                LastName = lblLastNameGv.Text,
                                ReceiptNumber = lblReceiptNumberGv.Text,
                                ReceiptDate = lblReceiveDate.Text,
                                Amt = lblAMOUNT.Text
                            });
                        }
                    }
                }
                else
                {
                    if ((chkSelectPayment.Checked) && (chkSelectPayment.Enabled))
                    {
                        if (ltnRecordStatus.Text == "กรณีแบ่งจ่าย")
                        {
                            var biz = new BLL.PaymentBiz();
                            var getAll = biz.GetReceiptSomePay(lblPaymentNoGv.Text, lblHeadNoGv.Text);
                            if (getAll.DataResponse.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < getAll.DataResponse.Tables[0].Rows.Count; i++)
                                {
                                    DataRow dr = getAll.DataResponse.Tables[0].Rows[i];
                                    string[] PathSomepay = dr["RECEIVE_PATH"].ToString().Split('.');
                                    string PathSomepayAdd = PathSomepay[0] + newPath + PathSomepay[1];
                                    var pament = lsReceiveNo.FirstOrDefault(x => x.rcv_path == PathSomepayAdd);

                                    lsReceiveNo.Remove(pament);
                                    ListChkAll.Remove(txtNumberGvSearch.Text);
                                    chkSelectPayment.Checked = false;
                                }

                            }
                        }
                        else
                        {
                            var pament = lsReceiveNo.FirstOrDefault(x => x.rcv_path == PathAdd);
                            lsReceiveNo.Remove(pament);
                            ListChkAll.Remove(txtNumberGvSearch.Text);
                            chkSelectPayment.Checked = false;
                        }
                    }
                }
            }

        }



        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string newPath = string.Empty;
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblPaymentNoGv = (Label)gr.FindControl("lblPaymentNoGv");
            Label lblHeadNoGv = (Label)gr.FindControl("lblHeadNoGv");
            Label lblOrderGv = (Label)gr.FindControl("lblOrderGv");
            Label lblPathFile = (Label)gr.FindControl("lblPathFile");
            Label lblUserUpload = (Label)gr.FindControl("lblUserUpload");
            Label lblReceiptNumberGv = (Label)gr.FindControl("lblReceiptNumberGv");
            string strUserUpload = lblUserUpload.Text;
            //string SumPath = GetPathFile(lblPathFile.Text, strUserUpload);
            if (lblUserUpload.Text.Length > 4)
            {
                if (lblUserUpload.Text != base.UserProfile.Id)
                {
                    newPath = "_COPY.";
                }
                else
                {
                    newPath = ".";
                }
            }
            else
            {
                if (lblUserUpload.Text != base.UserProfile.CompCode)
                {
                    newPath = "_COPY.";
                }
                else
                {
                    newPath = ".";
                }
            }
            var data2 = new List<DTO.SubPaymentDetail>();
            data2.Add(new DTO.SubPaymentDetail
            {
                HEAD_REQUEST_NO = lblHeadNoGv.Text,
                PAYMENT_NO = lblPaymentNoGv.Text,
                RECEIPT_NO = lblReceiptNumberGv.Text,
                Click = "Print"
            });
             string [] PathS = lblPathFile.Text.Split('.');
             string PathAdd = PathS[0] + newPath + PathS[1];
            lsReceiveNoSingle.Add(new DTO.ReceiveNo
            {
                rcv_path = PathAdd,
                HEAD_REQUEST_NO = lblHeadNoGv.Text,
                PAYMENT_NO = lblPaymentNoGv.Text,
                ReceiptNumber = lblReceiptNumberGv.Text,
            });
            //var biz = new BLL.PaymentBiz();
            //var resPrint = biz.PrintDownloadCount(data2.ToArray(), "", base.UserId);
            //SumPath = SumPath + "-P";
            ////string SumPath = lblPathFile.Text;
            //Session["FileName"] = SumPath;
            //DownloadEvent();
            if (lsReceiveNoSingle.Count() > 0)
            {

                string[] path_file = lsReceiveNoSingle.Select(x => x.rcv_path).ToArray();
                var biz = new BLL.PaymentBiz();
                var resPrint = biz.Zip_PrintDownloadCount(path_file, "Print", "", base.UserId);
                if (!resPrint.IsError)
                {
                    string PathFile = biz.CreatePdf(path_file);
                    string SumPathS = PathFile + "-P";
                    Session["FileName"] = SumPathS;

                    ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร", "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);
                    // lsReceiveNo.Clear();
                    UpdatePanelSearch.Update();
                }
                lsReceiveNoSingle.Clear();
            }
         

        }

        private string GetPathFile(string pathStr, string userUpload)
        {
            string pathFile = string.Empty;
            try
            {
                pathFile = pathStr.Replace(".pdf", "");
                if (base.UserProfile.MemberType == DTO.RegistrationType.General.GetEnumValue())
                {
                    if (base.UserProfile.Id == userUpload)
                    {
                        pathFile = pathStr;
                    }
                    else
                    {
                        pathFile = pathFile + "_COPY.pdf";
                    }
                }
                else if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue() ||
                        base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    if (base.UserProfile.CompCode == userUpload)
                    {
                        pathFile = pathStr;
                    }
                    else
                    {
                        pathFile = pathFile + "_COPY.pdf";
                    }
                }
                else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue() ||
                     base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue() ||
                     base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue())
                {
                    pathFile = pathFile + "_COPY.pdf";
                }
            }
            catch (Exception ex)
            {
                this.MasterSite.ModelError.ShowMessageError = "Path ไฟล์ไม่ถูกต้อง";
                this.MasterSite.ModelError.ShowModalError();
            }
            return pathFile;
        }
        private void ShowDocument(Stream fileStream, Int64 length, String contentType)
        {
            byte[] img = new byte[(int)length];

            using (BinaryReader br = new BinaryReader(fileStream))
            {
                img = br.ReadBytes((int)length);
                Response.ContentType = contentType;// "application/pdf";
                Response.AddHeader("content-length", "attachment;filename=labtest.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(img, 0, img.Length);
                Response.OutputStream.Flush();
                Response.End();
            }

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblPaymentNoGv = (Label)gr.FindControl("lblPaymentNoGv");
            Label lblHeadNoGv = (Label)gr.FindControl("lblHeadNoGv");
            Label lblOrderGv = (Label)gr.FindControl("lblOrderGv");
            Label lblPathFile = (Label)gr.FindControl("lblPathFile");
            Label lblUserUpload = (Label)gr.FindControl("lblUserUpload");
            Label lblReceiptNo = (Label)gr.FindControl("lblReceiptNumberGv");
            string strUserUpload = lblUserUpload.Text;
            string SumPath = GetPathFile(lblPathFile.Text, strUserUpload);
            var data2 = new List<DTO.SubPaymentDetail>();
            data2.Add(new DTO.SubPaymentDetail
            {
                HEAD_REQUEST_NO = lblHeadNoGv.Text,
                PAYMENT_NO = lblPaymentNoGv.Text,
                Click = "Download",
                RECEIPT_NO = lblReceiptNo.Text
            });

            var biz = new BLL.PaymentBiz();
            var resDownload = biz.PrintDownloadCount(data2.ToArray(), "", base.UserId);
            SumPath = SumPath + "-D";
            Session["FileName"] = SumPath;

            DownloadEvent();
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร",
            "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);



            UpdatePanelSearch.Update();
        }

        private void DownloadEvent()
        {
            try
            {
                var biz = new BLL.PaymentBiz();
                var count = biz.UpdateCountDownload(base.UserId, Session["FileName"], "L");
                Boolean a = count.ToBool();
            }
            catch
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lsReceiveNo.Clear();
            if ((txtStartDate.Text != "" && txtEndDate.Text != "") && (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text)))
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError();
                PnlDetailSearchGridView.Visible = false;
            }
            else
            {
                countCheckDetail = 0;
                txtNumberGvSearch.Text = "1";
                PAGE_SIZE = PAGE_SIZE_Key;
                listPayment = new List<RECIVE_PARTH>();
                PnlDetailSearchGridView.Visible = true;
                ListChkAll.Clear();
                BindDataInGridView();
            }
        }

        private void BindDataInGridView()
        {
            string maxBefore = string.Empty;
            maxBefore = txtInputMaxrow1.Text;

            if ((txtInputMaxrow1.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow1.Text != "" && Convert.ToInt32(txtInputMaxrow1.Text) != 0))
            {
                txtInputMaxrow1.Text = maxBefore;
            }
            else if (txtInputMaxrow1.Text == "" || Convert.ToInt32(txtInputMaxrow1.Text) == 0)
            {
                txtInputMaxrow1.Text = Convert.ToString(PAGE_SIZE);
            }
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow1.Text);
            var biz = new BLL.PaymentBiz();
            var getYear = biz.GetConfigViewFile();
            DataSet dsYear = getYear.DataResponse;
            DataTable dtYear = dsYear.Tables[0];
            if (dtYear.Rows.Count > 0)
            {
                DataRow drYear = dtYear.Rows[0];
                ConfigViewYear = drYear["ITEM_VALUE"].ToString();
            }

            var resultPage = txtNumberGvSearch.Text.ToInt();

            if (!string.IsNullOrEmpty(txtStartDate.Text)
                && !string.IsNullOrEmpty(txtEndDate.Text))
            {
                var resCount = biz.GetCountPaymentDetailByCriteria(base.UserProfile,
                          ddlTypePay.SelectedValue.ToString(), txtOrder.Text, Convert.ToDateTime(txtStartDate.Text)
                          , Convert.ToDateTime(txtEndDate.Text), txtIdCard.Text, txtReceiptNumber.Text, ConfigViewYear);
                DataSet ds = resCount.DataResponse;
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                TotalPages = (int)Math.Ceiling(dblPageCount);
                lblTotalPage1.Text = Convert.ToString(TotalPages);

                var res = biz.GetPaymentDetailByCriteria(base.UserProfile,
                          ddlTypePay.SelectedValue.ToString(), txtOrder.Text.Trim(), Convert.ToDateTime(txtStartDate.Text.Trim())
                          , Convert.ToDateTime(txtEndDate.Text.Trim()), txtIdCard.Text.Trim(), txtReceiptNumber.Text.Trim(), resultPage, PAGE_SIZE, ConfigViewYear);

                DataSet dsresult = res.DataResponse;
                gvSearch.Visible = true;
                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();
                if (res.IsError)
                {
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                }
                else
                {
                    btnPreviousGvSearch.Visible = true;
                    btnNextGvSearch.Visible = true;
                    txtNumberGvSearch.Visible = true;
                    UpdatePanelSearch.Update();
                    CheckBox Ch_all = (CheckBox)gvSearch.HeaderRow.FindControl("Checkall");
                    if (TotalPages == 0)
                    {
                        Ch_all.Visible = false;
                        // btnExportExcel.Visible = true;
                        btnPreviousGvSearch.Visible = false;
                        btnNextGvSearch.Visible = false;
                        txtNumberGvSearch.Visible = true;
                        lblParaPage1.Visible = true;
                        lblTotalPage1.Visible = true;
                        lblTotalPage1.Text = "1";
                        btngo1.Visible = true;
                        btn_Load_All.Visible = false;
                        btn_Print_All.Visible = false;
                        txtInputMaxrow1.Visible = true;
                        lblHeadInputMaxrow1.Visible = true;
                        lblHeadTotal1.Visible = true;
                        lblTotalrecord1.Visible = true;
                        lblEndTotal1.Visible = true;
                        lblTotalrecord1.Text = "0";
                        divGv1.Visible = true;
                        btnExportExcel.Visible = false;
                    }
                    else if (TotalPages == 1)
                    {
                        btnExportExcel.Visible = true;
                        btnPreviousGvSearch.Visible = false;
                        btnNextGvSearch.Visible = false;
                        txtNumberGvSearch.Visible = true;
                        lblParaPage1.Visible = true;
                        lblTotalPage1.Visible = true;
                        btn_Load_All.Visible = true;
                        btn_Print_All.Visible = true;
                        btngo1.Visible = true;
                        txtInputMaxrow1.Visible = true;
                        lblHeadInputMaxrow1.Visible = true;
                        lblHeadTotal1.Visible = true;
                        lblTotalrecord1.Visible = true;
                        lblEndTotal1.Visible = true;
                        lblTotalrecord1.Text = dr["rowcount"].ToString();
                        divGv1.Visible = true;
                    }
                    else if (TotalPages > 1)
                    {
                        btnExportExcel.Visible = true;
                        btnPreviousGvSearch.Visible = false;
                        btnNextGvSearch.Visible = true;
                        txtNumberGvSearch.Visible = true;
                        lblParaPage1.Visible = true;
                        lblTotalPage1.Visible = true;
                        btn_Load_All.Visible = true;
                        btn_Print_All.Visible = true;
                        btngo1.Visible = true;
                        txtInputMaxrow1.Visible = true;
                        lblHeadInputMaxrow1.Visible = true;
                        lblHeadTotal1.Visible = true;
                        lblTotalrecord1.Visible = true;
                        lblEndTotal1.Visible = true;
                        lblTotalrecord1.Text = dr["rowcount"].ToString();
                        divGv1.Visible = true;
                    }
                    boxResult.Visible = true;

                }
            }
            #region comment
            //else if (ddlTypePay.SelectedIndex == 0)
            //{
            //    if (!string.IsNullOrEmpty(txtStartDate.Text) && !string.IsNullOrEmpty(txtEndDate.Text))
            //    {
            //        var resCount = biz.GetCountPaymentDetailByCriteria(base.UserProfile,
            //             ddlTypePay.SelectedValue.ToString(), txtOrder.Text, Convert.ToDateTime(txtStartDate.Text)
            //             , Convert.ToDateTime(txtEndDate.Text), txtIdCard.Text, txtReceiptNumber.Text);
            //        DataSet ds = resCount.DataResponse;
            //        DataTable dt = ds.Tables[0];
            //        DataRow dr = dt.Rows[0];
            //        int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
            //        double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
            //        TotalPages = (int)Math.Ceiling(dblPageCount);
            //        lblTotalPage1.Text = Convert.ToString(TotalPages);

            //        var res = biz.GetPaymentDetailByCriteria(base.UserProfile, ddlTypePay.SelectedIndex.ToString(), txtOrder.Text,
            //                  Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text),
            //                  txtIdCard.Text, txtReceiptNumber.Text, resultPage, PAGE_SIZE);


            //        if (res.IsError)
            //        {
            //            UCModalError.ShowMessageError = res.ErrorMsg;
            //            UCModalError.ShowModalError();
            //        }
            //        else
            //        {
            //            DataSet ds = res.DataResponse;

            //            gvSearch.Visible = true;
            //            gvSearch.DataSource = res.DataResponse;
            //            btnPreviousGvSearch.Visible = true;
            //            btnNextGvSearch.Visible = true;
            //            txtNumberGvSearch.Visible = true;
            //            gvSearch.DataBind();

            //            if (ds.Tables[0].Rows.Count == 0)
            //            {
            //            }
            //            else if (ds.Tables[0].Rows.Count == 1)
            //            {
            //                btnPreviousGvSearch.Visible = false;
            //                btnNextGvSearch.Visible = false;
            //                txtNumberGvSearch.Visible = true;

            //                btngo1.Visible = true;
            //                txtInputMaxrow1.Visible = true;
            //                lblHeadInputMaxrow1.Visible = true;
            //                lblHeadTotal1.Visible = true;
            //                lblTotalrecord1.Visible = true;
            //                lblEndTotal1.Visible = true;
            //                lblTotalrecord1.Text = dr["rowcount"].ToString();
            //            }
            //            else if (ds.Tables[0].Rows.Count > 1)
            //            {
            //                btnNextGvSearch.Visible = true;
            //                txtNumberGvSearch.Visible = true;

            //                btngo1.Visible = true;
            //                txtInputMaxrow1.Visible = true;
            //                lblHeadInputMaxrow1.Visible = true;
            //                lblHeadTotal1.Visible = true;
            //                lblTotalrecord1.Visible = true;
            //                lblEndTotal1.Visible = true;
            //                lblTotalrecord1.Text = dr["rowcount"].ToString();
            //            }
            //            boxResult.Visible = true;

            //            UpdatePanelSearch.Update();


            //        }
            //    }
            //    //else
            //    //{
            //    //    //UCModalError.ShowMessageError = SysMessage.PleaseInputFill;
            //    //    //UCModalError.ShowModalError();
            //    //}
            //}
            #endregion
        }

        protected void ibtClearStartDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartDate.Text = string.Empty;
        }

        protected void ibtClearEndDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndDate.Text = string.Empty;
        }
        CheckBox check_all_head;
        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                check_all_head = (CheckBox)e.Row.FindControl("Checkall");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label OrderGv = (Label)e.Row.FindControl("lblOrderGv");
                Label txtPath = (Label)e.Row.FindControl("lblPathFile");
                LinkButton Print = (LinkButton)e.Row.FindControl("hplPrint");
                LinkButton Download = (LinkButton)e.Row.FindControl("linkDownload");
                LinkButton ltnRecordStatus = (LinkButton)e.Row.FindControl("ltnRecordStatus");
                CheckBox chkSelectPayment = (CheckBox)e.Row.FindControl("chkSelectPayment");
                Label RowNUM = (Label)e.Row.FindControl("lblNo");
                OrderGv.Text = OrderGv.Text.Insert(6, " ").Insert(11, " ");
                Label RecordStatus = (Label)e.Row.FindControl("lblRecordStatus");
                var lblUserUpload = (Label)e.Row.FindControl("lblUserUpload");
                string PathAdd = string.Empty;
                string newPath = string.Empty;
                if (lblUserUpload.Text.Length > 4)
                {
                    if (lblUserUpload.Text != base.UserProfile.Id)
                    {
                        newPath = "_COPY.";
                    }
                    else
                    {
                        newPath = ".";
                    }
                }
                else
                {
                    if (lblUserUpload.Text != base.UserProfile.CompCode)
                    {
                        newPath = "_COPY.";
                    }
                    else
                    {
                        newPath = ".";
                    }
                }
                if (txtPath.Text != "")
                {
                    string[] PathS = txtPath.Text.Split('.');
                    PathAdd = PathS[0] + newPath + PathS[1];
                }
                var list = lsReceiveNo.FirstOrDefault(x => x.rcv_path == PathAdd);
                CheckBox chk_all = (CheckBox)gvSearch.HeaderRow.FindControl("Checkall");
                if (list != null)
                {
                    chkSelectPayment.Checked = true;
                }
                else
                {
                    chkSelectPayment.Checked = false;
                }

                if (txtPath.Text != "")
                {
                    //add 3/12/2556
                    
                    countCheckDetail++;
                    chkSelectPayment.Enabled = true;
                    chk_all.Enabled = true;
                    Print.Visible = true;
                    Download.Visible = true;
                    if (RecordStatus.Text == "S" || RecordStatus.Text == "Z")
                    {
                        ltnRecordStatus.Text = "กรณีแบ่งจ่าย";
                        ltnRecordStatus.Visible = true;
                        RecordStatus.Text = "";
                        Print.Visible = false;
                        Download.Visible = false;

                    }
                    else
                    {
                        RecordStatus.Text = "";
                    }
                }
                else
                {
                    //add 3/12/2556
                    
                    chkSelectPayment.Enabled = false;
                   
                    if (RecordStatus.Text == "S" || RecordStatus.Text == "Z")
                    {
                        ltnRecordStatus.Text = "กรณีแบ่งจ่าย";
                        ltnRecordStatus.Visible = true;
                        RecordStatus.Text = "";
                        Print.Visible = false;
                        Download.Visible = false;

                    }
                    else
                    {
                        Print.Visible = false;
                        Download.Visible = false;
                        RecordStatus.Text = "";
                    }
                }
                if (countCheckDetail > 0)
                {
                    chk_all.Enabled = true;
                }
                else
                {
                    chk_all.Enabled = false;
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                var HeaderAll = ListChkAll.FirstOrDefault(a => a == txtNumberGvSearch.Text);
                if (HeaderAll != null)
                {
                    check_all_head.Checked = true;
                }
            }

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultData();
            boxResult.Visible = false;
            btnExportExcel.Visible = false;
            ListChkAll.Clear();
        }

        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow1.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            var res = biz.GetPaymentDetailByCriteria(base.UserProfile,
                        ddlTypePay.SelectedValue.ToString(), txtOrder.Text, Convert.ToDateTime(txtStartDate.Text)
                        , Convert.ToDateTime(txtEndDate.Text), txtIdCard.Text, txtReceiptNumber.Text, resultPage, PAGE_SIZE, ConfigViewYear);
            gvSearch.DataSource = res.DataResponse;
            gvSearch.DataBind();

        }
        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            int total = lblTotalrecord1.Text == "" ? 0 : lblTotalrecord1.Text.ToInt();
            if (total > base.EXCEL_SIZE_Key)
            {
                this.MasterSite.ModelError.ShowMessageError = SysMessage.ExcelSizeError;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                ExportBiz export = new ExportBiz();
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("ลำดับ", "RUN_NO");
                columns.Add("ประเภทการชำระ", "PETITION_TYPE_NAME");
                columns.Add("บัตรประชาชน", "ID_CARD_NO");
                columns.Add("ชื่อ- นามสกุล", "FIRST_NAME");
                columns.Add("เลขที่ใบสั่งจ่าย", "group_request_no");
                columns.Add("วันที่ออกใบสั่งจ่าย", "CREATED_DATE");
                columns.Add("วันที่ชำระเงิน", "payment_date");
                columns.Add("เลขที่ใบเสร็จ", "RECEIPT_NO");

                List<HeaderExcel> header = new List<HeaderExcel>();
                header.Add(new HeaderExcel
                {
                    NameColumnsOne = "ประเภทการชำระ ",
                    ValueColumnsOne = ddlTypePay.SelectedItem.Text,
                    NameColumnsTwo = "วันที่สั่งจ่าย(เริ่ม) ",
                    ValueColumnsTwo = txtStartDate.Text
                });

                header.Add(new HeaderExcel
                {
                    NameColumnsOne = "วันที่สั่งจ่าย(สิ้นสุด) ",
                    ValueColumnsOne = txtEndDate.Text,
                    NameColumnsTwo = "ใบสั่งจ่าย ",
                    ValueColumnsTwo = txtOrder.Text
                });

                header.Add(new HeaderExcel
                {
                    NameColumnsOne = "เลขบัตรประชาชน ",
                    ValueColumnsOne = txtIdCard.Text,
                    NameColumnsTwo = "เลขที่ใบเสร็จ ",
                    ValueColumnsTwo = txtReceiptNumber.Text
                });

                var biz = new BLL.PaymentBiz();
                var res = biz.GetPaymentDetailByCriteria(base.UserProfile,
                              ddlTypePay.SelectedValue.ToString(), txtOrder.Text.Trim(), Convert.ToDateTime(txtStartDate.Text.Trim())
                              , Convert.ToDateTime(txtEndDate.Text.Trim()), txtIdCard.Text.Trim(), txtReceiptNumber.Text.Trim(), 1, base.EXCEL_SIZE_Key, ConfigViewYear);
                export.CreateExcel(res.DataResponse.Tables[0], columns, header, base.UserProfile);
            }

        }
        public override void VerifyRenderingInServerForm(Control control) { }


        protected void Print_All(object sender, EventArgs e)
        {
            try
            {
                if (lsReceiveNo.Count() > 0)
                {

                    string[] path_file = lsReceiveNo.Select(x => x.rcv_path).ToArray();
                    var biz = new BLL.PaymentBiz();
                    var resPrint = biz.Zip_PrintDownloadCount(path_file, "Print", "", base.UserId);
                    if (!resPrint.IsError)
                    {
                        string PathFile = biz.CreatePdf(path_file);
                        string SumPath = PathFile + "-P";
                        Session["FileName"] = SumPath;

                        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร", "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);
                       // lsReceiveNo.Clear();
                        UpdatePanelSearch.Update();
                    }

                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_001;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            catch (Exception ex)
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_002;
                this.MasterSite.ModelError.ShowModalError();

            }
        }

        protected void Load_All(object sender, EventArgs e)
        {
            try
            {
                if (lsReceiveNo.Count() > 0)
                {

                    string[] path_file = lsReceiveNo.Select(x => x.rcv_path).ToArray();
                    var biz = new BLL.PaymentBiz();
                    var resPrint = biz.Zip_PrintDownloadCount(path_file, "Download", "", base.UserId);
                    if (!resPrint.IsError)
                    {
                        string PathFile = biz.CreatePdf(path_file);
                        string ZipFile = biz.CreateZip(PathFile);
                        string SumPath = ZipFile + "-D";
                        Session["FileName"] = SumPath;

                        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร", "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);
                        //lsReceiveNo.Clear();
                        UpdatePanelSearch.Update();
                    }

                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_001;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            catch
            {

                this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_002;
                this.MasterSite.ModelError.ShowModalError();

            }
        }
        #region Popup
        protected void chkSelectPaymentPopup_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkSelectPayment = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)checkSelectPayment.Parent.Parent;
            var lblPathFile = (Label)gr.FindControl("lblPathFile");
            var lblOrderGv = (Label)gr.FindControl("lblOrderGv");
            var lblReceiptNumberGv = (Label)gr.FindControl("lblReceiptNumberGv");
            var lblReceiveDate = (Label)gr.FindControl("lblReceiveDate");
            var lblUserUpload = (Label)gr.FindControl("lblUserUpload");
            string PathAdd = string.Empty;
            string newPath = string.Empty;
            if (lblUserUpload.Text.Length > 4)
            {
                if (lblUserUpload.Text != base.UserProfile.Id)
                {
                    newPath = "_COPY.";
                }
                else
                {
                    newPath = ".";
                }
            }
            else
            {
                if (lblUserUpload.Text != base.UserProfile.CompCode)
                {
                    newPath = "_COPY.";
                }
                else
                {
                    newPath = ".";
                }
            }
            if (lblPathFile.Text != "")
            {
                string[] PathS = lblPathFile.Text.Split('.');
                PathAdd = PathS[0] + newPath + PathS[1];
            }

            if (checkSelectPayment.Checked)
            {
                lsReceiveNoPopup.Add(new DTO.ReceiveNo
                {
                    rcv_path = PathAdd,

                });
            }
            else
            {
                var pament = lsReceiveNoPopup.FirstOrDefault(x => x.rcv_path == PathAdd);
                ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("CheckallPopup")).Checked = false;
                lsReceiveNoPopup.Remove(pament);
            }
            MPDetail.Show();

        }

        protected void CheckallPopup_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = (CheckBox)sender;
            foreach (GridViewRow row in GVPopupReceipt.Rows)
            {

                CheckBox chkSelectPaymentPopup = (CheckBox)row.FindControl("chkSelectPaymentPopup");
                var lblPathFile = (Label)row.FindControl("lblPathFile");
                var lblUserUpload = (Label)row.FindControl("lblUserUpload");
                string PathAdd = string.Empty;
                string newPath = string.Empty;
                if (lblUserUpload.Text.Length > 4)
                {
                    if (lblUserUpload.Text != base.UserProfile.Id)
                    {
                        newPath = "_COPY.";
                    }
                    else
                    {
                        newPath = ".";
                    }
                }
                else
                {
                    if (lblUserUpload.Text != base.UserProfile.CompCode)
                    {
                        newPath = "_COPY.";
                    }
                    else
                    {
                        newPath = ".";
                    }
                }
                if (lblPathFile.Text != "")
                {
                    string[] PathS = lblPathFile.Text.Split('.');
                    PathAdd = PathS[0] + newPath + PathS[1];
                }

                if (ck.Checked)
                {
                    if ((!chkSelectPaymentPopup.Checked) && (chkSelectPaymentPopup.Enabled))
                    {
                        lsReceiveNoPopup.Add(new DTO.ReceiveNo
                        {
                            rcv_path = PathAdd,

                        });
                        chkSelectPaymentPopup.Checked = true;
                    }
                }
                else
                {
                    if ((chkSelectPaymentPopup.Checked) && (chkSelectPaymentPopup.Enabled))
                    {
                        var pament = lsReceiveNoPopup.FirstOrDefault(x => x.rcv_path == PathAdd);
                        lsReceiveNoPopup.Remove(pament);
                        chkSelectPaymentPopup.Checked = false;
                    }
                }
            }
            MPDetail.Show();
        }
        protected void GVPopupReceipt_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label OrderGv = (Label)e.Row.FindControl("lblOrderGv");
                Label txtPath = (Label)e.Row.FindControl("lblPathFile");
                LinkButton Print = (LinkButton)e.Row.FindControl("hplPrint");
                LinkButton Download = (LinkButton)e.Row.FindControl("linkDownload");
                CheckBox chkSelectPaymentPopup = (CheckBox)e.Row.FindControl("chkSelectPaymentPopup");
                Label RowNUM = (Label)e.Row.FindControl("lblNo");

                Label RecordStatus = (Label)e.Row.FindControl("lblRecordStatus");
                var list = lsReceiveNoPopup.FirstOrDefault(x => x.rcv_path == txtPath.Text);

                if (list != null)
                {
                    chkSelectPaymentPopup.Checked = true;
                }
                else
                {
                    chkSelectPaymentPopup.Checked = false;
                }

                if (txtPath.Text != "")
                {
                    //add 3/12/2556
                    CheckBox chk_all = (CheckBox)GVPopupReceipt.HeaderRow.FindControl("CheckallPopup");
                    countCheckDetail++;
                    chkSelectPaymentPopup.Enabled = true;
                    chk_all.Enabled = true;
                    Print.Visible = true;
                    Download.Visible = true;
                }
                else
                {
                    //add 3/12/2556
                    CheckBox chk_all = (CheckBox)GVPopupReceipt.HeaderRow.FindControl("CheckallPopup");
                    chkSelectPaymentPopup.Enabled = false;
                    chk_all.Enabled = false;
                    Print.Visible = false;
                    Download.Visible = false;
                }


                CheckBox chk_allTemp = (CheckBox)GVPopupReceipt.HeaderRow.FindControl("CheckallPopup");

                if (countCheckDetail > 0)
                {
                    chk_allTemp.Enabled = true;


                }
                else
                {
                    chk_allTemp.Enabled = false;
                }


            }

        }
        protected void ltnRecordStatus_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblPaymentNoGv = (Label)gr.FindControl("lblPaymentNoGv");
            Label lblHeadNoGv = (Label)gr.FindControl("lblHeadNoGv");
            Label lblDatePayGv = (Label)gr.FindControl("lblDatePayGv");
            Label lblIDCardGv = (Label)gr.FindControl("lblIDCardGv");
            Label lblOrderGv = (Label)gr.FindControl("lblOrderGv");
            Label lblFirstNameGv = (Label)gr.FindControl("lblFirstNameGv");
            Label lblLastNameGv = (Label)gr.FindControl("lblLastNameGv");
            Label lblTypePayGv = (Label)gr.FindControl("lblTypePayGv");
            var biz = new BLL.PaymentBiz();
            var getAll = biz.GetReceiptSomePay(lblPaymentNoGv.Text, lblHeadNoGv.Text);
            if (getAll.DataResponse.Tables[0].Rows.Count > 0)
            {
                lbldetailPettion.Text = lblTypePayGv.Text;
                lbldetailGroupRequestNo.Text = lblOrderGv.Text;
                lbldetailPopupIdCard.Text = lblIDCardGv.Text;
                lbldetailCreateDate.Text = lblDatePayGv.Text;
                lblDetailName.Text = lblFirstNameGv.Text + " " + lblLastNameGv.Text;
                GVPopupReceipt.Visible = true;
                btnPopupDownload.Visible = true;
                btnPrintPopup.Visible = true;
                GVPopupReceipt.DataSource = getAll.DataResponse;
                GVPopupReceipt.DataBind();
                MPDetail.Show();
            }



            lsReceiveNoPopup.Clear();
            //lsReceiveNo.Clear();
            UpdatePanelSearch.Update();
        }
        protected void PrintPopup_All(object sender, EventArgs e)
        {
            try
            {
                if (lsReceiveNoPopup.Count() > 0)
                {

                    string[] path_file = lsReceiveNoPopup.Select(x => x.rcv_path).ToArray();
                    var biz = new BLL.PaymentBiz();
                    var resPrint = biz.Zip_PrintDownloadCount(path_file, "Print", "", base.UserId);
                    if (!resPrint.IsError)
                    {
                        string PathFile = biz.CreatePdf(path_file);
                        string SumPath = PathFile + "-P";
                        Session["FileName"] = SumPath;

                        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร", "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);

                        UpdatePanelSearch.Update();
                    }

                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_001;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            catch
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_002;
                this.MasterSite.ModelError.ShowModalError();
            }
        }

        protected void LoadPopup_All(object sender, EventArgs e)
        {
            try
            {
                if (lsReceiveNoPopup.Count() > 0)
                {

                    string[] path_file = lsReceiveNoPopup.Select(x => x.rcv_path).ToArray();
                    var biz = new BLL.PaymentBiz();
                    var resPrint = biz.Zip_PrintDownloadCount(path_file, "Download", "", base.UserId);
                    if (!resPrint.IsError)
                    {
                        string PathFile = biz.CreatePdf(path_file);
                        string ZipFile = biz.CreateZip(PathFile);
                        string SumPath = ZipFile + "-D";
                        Session["FileName"] = SumPath;

                        ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร", "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);

                        UpdatePanelSearch.Update();
                    }

                }
                else
                {
                    this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_001;
                    this.MasterSite.ModelError.ShowModalError();
                }
            }
            catch
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorPaymentDetail_002;
                this.MasterSite.ModelError.ShowModalError();
            }
        }
        #endregion


    }
}
