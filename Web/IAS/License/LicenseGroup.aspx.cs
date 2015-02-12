using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.IO;

using IAS.BLL.AttachFilesIAS;
using IAS.Properties;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;

namespace IAS.License
{
    public partial class LicenseGroup : basepage
    {

        #region Properties

        DTO.SummaryReceiveLicense _summaryReceiveLicense = new DTO.SummaryReceiveLicense();

        public DTO.SummaryReceiveLicense SummaryReceiveLicense
        {
            get
            {
                if (Session["SummaryReceiveLicense"] == null)
                {
                    _summaryReceiveLicense.Header = new DTO.UploadHeader();
                    _summaryReceiveLicense.Identity = "";
                    _summaryReceiveLicense.ReceiveLicenseDetails = new List<DTO.ReceiveLicenseDetail>();

                    return _summaryReceiveLicense;
                }

                else
                {
                    _summaryReceiveLicense = (DTO.SummaryReceiveLicense)Session["SummaryReceiveLicense"];
                    return _summaryReceiveLicense;
                }

            }
            set
            {
                _summaryReceiveLicense = value;
                Session["SummaryReceiveLicense"] = _summaryReceiveLicense;
            }
        }

        public string IdCardNo
        {
            get
            {
                return (string)Session["IdCardNo"];
            }
            set
            {
                Session["IdCardNo"] = value;
            }
        }

        private List<DTO.AttachFileDetail> AttatchFileDetail
        {
            get
            {
                return Session["AttatchFileDetail"] == null
                        ? new List<DTO.AttachFileDetail>()
                        : (List<DTO.AttachFileDetail>)Session["AttatchFileDetail"];
            }
            set
            {
                Session["AttatchFileDetail"] = value;
            }
        }

        public List<DTO.DataItem> GetDocumentTypeIsImage
        {
            get
            {
                if (Session["DocumentTypeIsImage"] == null)
                {
                    Session["DocumentTypeIsImage"] = new List<DTO.DataItem>();
                }
                return (List<DTO.DataItem>)Session["DocumentTypeIsImage"];
            }
            set
            {
                Session["DocumentTypeIsImage"] = value;
            }
        }

        public List<DTO.LicenseAttatchFile> AttachFiles
        {
            get
            {
                if (Session["AttatchFiles"] == null)
                {
                    Session["AttatchFiles"] = new List<DTO.LicenseAttatchFile>();
                }

                return (List<DTO.LicenseAttatchFile>)Session["AttatchFiles"];
            }

            set
            {
                Session["AttatchFiles"] = value;
            }
        }

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetLicenseType();
            }
        }
        #endregion

        #region GetData

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseTypeByCompCode(base.UserProfile.CompCode);
            BindToDDL(ddlLicenseType, ls.DataResponse);
            ddlLicenseType.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, ""));
        }

        private void InitDocumentType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetAttachFilesTypeImage()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            Session["DocumentTypeIsImage"] = biz.GetDocumentTypeIsImage();
        }

        private void GetRequestLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.DataItem[] ls = biz.GetRequestLicenseType(SysMessage.DefaultSelecting).DataResponse;
            List<DTO.DataItem> newls = new List<DTO.DataItem>();
            int i = 0;
            foreach (DTO.DataItem item in ls)
            {
                if (item.Id != "01")
                {
                    if (i == 0)
                    {
                        i = i + 1;
                        newls.Add(item);
                        continue;
                    }
                    //item.Name = item.Name.Substring(3);
                    newls.Add(item);
                }

            }

            BindToDDL(ddlObtainLicenseType, newls.ToArray());
        }

        private void GetApprove()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.DataItem[] ls = biz.GetAssociationApprove(ddlLicenseType.SelectedValue).DataResponse;

            List<DTO.DataItem> newls = new List<DTO.DataItem>();

            if (ls != null)
            {
                var firstItem = SysMessage.DefaultSelecting;
                newls.Add(new DTO.DataItem { Name = firstItem });
                int i = 0;
                foreach (DTO.DataItem item in ls)
                {
                    if (i == 0)
                    {
                        i = i + 1;
                        newls.Add(item);
                        continue;
                    }
                    newls.Add(item);
                }

                BindToDDL(ddlApproveBy, newls.ToArray());

            }
            else
            {
                newls[0].Name = "ไม่มีผู้อนุมัติ";
                newls[0].Id = "";

                BindToDDL(ddlApproveBy, newls.ToArray());

            }
        }

        private void GetPettitionType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.DataItem[] ls = biz.GetPettitionTypebyLicenseType(SysMessage.DefaultSelecting, ddlLicenseType.SelectedValue).DataResponse;
            List<DTO.DataItem> newls = new List<DTO.DataItem>();

            int i = 0;
            foreach (DTO.DataItem item in ls)
            {
                if (item.Id != "01")
                {
                    if (i == 0)
                    {
                        i = i + 1;
                        newls.Add(item);
                        continue;
                    }
                    newls.Add(item);
                }

            }

            BindToDDL(ddlObtainLicenseType, newls.ToArray());
        }

        private List<string> GetAttachFilesData(IEnumerable<DTO.AttachFileDetail> atFile)
        {
            List<string> listFile = new List<string>();

            foreach (DTO.AttachFileDetail item in atFile)
            {
                listFile.Add(item.FileTypeCode);
            }

            return listFile;
        }


        #endregion

        #region VeiwData

        protected void hplViewImg_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblIDNumberGv = (Label)gr.FindControl("lblIDNumberGv");
            BindGridImageAccount(lblIDNumberGv.Text);
            mdpAttachFilePop.Show();
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            //Get IDCard By Selected Row
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var idCardNo = (Label)gr.FindControl("lblIDNumberGv");
            if (idCardNo != null)
            {
                this.ucLicenseDetail.FirstTabDataBind(idCardNo.Text);
                this.ucLicenseDetail.InitTab(sender, e);
                this.ucLicenseDetail.UpdateTab.Update();

            }
            else
            {
                UCModalWarning.ShowMessageWarning = Resources.errorLicenseDetail_001;
                UCModalWarning.ShowModalWarning();
                return;
            }
        }

        protected void hplViewDetailImg_Click(object sender, EventArgs e)
        {
            mdpAttachFilePop.Show();
        }

        #endregion

        #region Control

        private void VisibleButton()
        {
            btnPreviousGvSearch.Visible = true;
            txtNumberGvSearch.Visible = true;
            btnNextGvSearch.Visible = true;
        }



        private void DisablePanel()
        {
            pnlImportFile.Visible = false;
            btnValidateProp.Visible = false;
            pnlCondition.Visible = false;
        }


        #endregion

        #region Search

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() + 1;

            txtNumberGvSearch.Text = result.ToString();
        }

        #endregion

        #region BindData

        private void BindGridImageAccount(string strIDNumberGv)
        {

            DTO.ReceiveLicenseDetail receiveLicenseDetail = this.SummaryReceiveLicense.ReceiveLicenseDetails.FirstOrDefault(a => a.CITIZEN_ID == strIDNumberGv);
            if (receiveLicenseDetail != null)
            {
                //pnlAtfile.Visible = true;
                gvUpload.DataSource = receiveLicenseDetail.AttachFileDetails;
                gvUpload.DataBind();

            }
            else
            {
                gvUpload.DataSource = new List<DTO.AttatchFileLicense>();
                gvUpload.DataBind();

            }
            mdpAttachFilePop.Show();
        }

        #endregion

        #region ImportDataClick
        protected void btnImport_Click(object sender, EventArgs e)
        {
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var res = biz.SubmitReceiveLicenseGroupUpload(SummaryReceiveLicense.Identity, new List<DTO.AttachFileDetail>(), base.UploadRecieveLicense, base.UserId);

            if (res.IsError)
            {

                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                DisablePanel();
                UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessImportData;
                UCModalSuccess.ShowModalSuccess();
            }
        }
        #endregion

        #region LoadDataClick

        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            var biz = new BLL.LicenseBiz();

            #region CheckDropDown


            if (string.IsNullOrEmpty(ddlObtainLicenseType.SelectedValue) || string.IsNullOrEmpty(ddlLicenseType.SelectedValue))
            {
                if (string.IsNullOrEmpty(ddlObtainLicenseType.SelectedValue) && string.IsNullOrEmpty(ddlLicenseType.SelectedValue))
                {
                    DisablePanel();
                    UCModalWarning.ShowMessageWarning = SysMessage.CannotType;
                    UCModalWarning.ShowModalWarning();
                    return;

                }
                else if (string.IsNullOrEmpty(ddlLicenseType.SelectedValue))
                {
                    DisablePanel();
                    UCModalWarning.ShowMessageWarning = SysMessage.CannotLicesetype;
                    UCModalWarning.ShowModalWarning();
                    return;
                }
                else
                {
                    DisablePanel();
                    UCModalWarning.ShowMessageWarning = SysMessage.CannotPettitiontype;
                    UCModalWarning.ShowModalWarning();
                    return;
                }

            }

            #endregion

            #region chkApproveBy

            if (ddlApproveBy.SelectedItem.Text != "ไม่มีผู้อนุมัติ" && string.IsNullOrEmpty(ddlApproveBy.SelectedValue))
            {
                UCModalWarning.ShowMessageWarning = "กรุณาเลือกผู้อนุมัติ";
                UCModalWarning.ShowModalWarning();
                return;
            }

            #endregion

            #region chkReplaceType

            if (ddlObtainLicenseType.SelectedValue == "16")
            {
                if (string.IsNullOrEmpty(rblType.SelectedValue))
                {
                    UCModalWarning.ShowMessageWarning = "กรุณาเลือกประเภทใบแทนใบอนุญาต";
                    UCModalWarning.ShowModalWarning();
                    return;
                }

            }

            #endregion


            if (!string.IsNullOrEmpty(FuFile.FileName))
            {
                var res = biz.UploadData(FuFile.FileName,
                                        base.UserProfile,
                                        ddlObtainLicenseType.SelectedValue,
                                        ddlLicenseType.SelectedValue,
                                        FuFile.PostedFile.InputStream, rblType.SelectedValue, ddlApproveBy.SelectedValue);

                if (res.IsError)
                {
                    DisablePanel();
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    DisablePanel();
                    pnlImportFile.Visible = true;

                    Session["GroupID"] = res.DataResponse.Identity;
                    Session["licenseType"] = ddlLicenseType.SelectedValue;
                    Session["petitionType"] = ddlObtainLicenseType.SelectedValue;
                    Session["licneseName"] = ddlLicenseType.SelectedItem.Text;
                    Session["pettionName"] = ddlObtainLicenseType.SelectedItem.Text;

                    string petitionType = Session["petitionType"].ToString();

                    SummaryReceiveLicense = res.DataResponse;

                    IList<DTO.UploadHeader> listHead = new List<DTO.UploadHeader>();
                    listHead.Add(SummaryReceiveLicense.Header);
                    gvImportFile.DataSource = listHead;
                    gvImportFile.DataBind();

                    if (res.DataResponse.ReceiveLicenseDetails.Count() > 0)
                    {
                        btnValidateProp.Visible = false;
                        btnImport.Visible = false;
                        if (petitionType == "11")
                        {
                            pnlCondition.Visible = true;
                            chkComfirm.Checked = false;
                        }
                        else
                        {
                            pnlCondition.Visible = false;
                        }
                        gvCheckList.DataSource = SummaryReceiveLicense.ReceiveLicenseDetails;
                        gvCheckList.DataBind();

                        Int32 cntError = SummaryReceiveLicense.ReceiveLicenseDetails.Count(a => a.LOAD_STATUS == "F");
                        if (!String.IsNullOrEmpty(SummaryReceiveLicense.MessageError) || cntError > 0)
                        {
                            btnImport.Visible = false;
                            btnValidateProp.Visible = false;
                            pnlCondition.Visible = false;
                            lblMessageError.Visible = true;
                            lblMessageError.Text = Resources.errorLicenseGroup_001;
                            btnValidateProp.Visible = false;
                        }
                        else
                        {

                            btnValidateProp.Visible = true;
                            btnImport.Visible = false;

                            if (petitionType == "11")
                            {
                                pnlCondition.Visible = true;
                                chkComfirm.Checked = false;
                            }
                            else
                            {
                                pnlCondition.Visible = false;
                            }
                            lblMessageError.Visible = false;
                            lblMessageError.Text = "";
                            btnValidateProp.Visible = true;
                        }

                        gvCheckList.Columns[7].Visible = false;
                        gvCheckList.Columns[9].Visible = true;
                        return;
                    }
                    else
                    {
                        DisablePanel();
                        UCModalError.ShowMessageError = Resources.errorLicenseGroup_002;
                        UCModalError.ShowModalError();
                        return;
                    }
                }
            }
            else
            {
                DisablePanel();
                UCModalError.ShowMessageError = SysMessage.CannotAttachFile;
                UCModalError.ShowModalError();
            }


        }
        #endregion

        #region ValidateData

        private bool ValidateDetail()
        {
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var res = biz.ValidateProp(Session["GroupID"].ToString());
            return res.ResultMessage;
        }

        private bool ChkErrmesssage()
        {
            if (SummaryReceiveLicense.Header.MissingTrans == 0)
            {
                return true;
            }
            else
                return false;
        }

        private void UpdateErrHead(int missingTrans, int rightTrans)
        {
            for (int i = 0; i <= gvImportFile.Rows.Count - 1; i++)
            {
                Label txtCorrectGv = (Label)gvImportFile.Rows[i].FindControl("lblItemsCorrectGv");
                Label txtIncorectGv = (Label)gvImportFile.Rows[i].FindControl("lblItemsIncorrectGv");

                string right = Convert.ToString(rightTrans);
                string missing = Convert.ToString(missingTrans);

                txtCorrectGv.Text = right;
                txtIncorectGv.Text = missing;
            }
        }

        private void chkValidateProp(List<string> text, GridView gv)
        {

            foreach (GridViewRow row in gv.Rows)
            {
                Label lblID = (Label)row.FindControl("lblID");
                Image impCorrect = (Image)row.FindControl("impCorrect");
                Image impdisCorrect = (Image)row.FindControl("impdisCorrect");


                var result = text.Where(l => l.Equals(lblID.Text.Trim())).FirstOrDefault();

                if (result == null)
                {
                    impCorrect.Visible = true;
                    impdisCorrect.Visible = false;
                }
                else
                {
                    impdisCorrect.Visible = true;
                    impCorrect.Visible = false;
                }

            }


        }

        private void chkValidateSpecialProp(List<string> text, GridView gv)
        {
            foreach (GridViewRow row in gv.Rows)
            {
                Label lblID = (Label)row.FindControl("lblID");
                Image impCorrect = (Image)row.FindControl("impCorrect");
                Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                Label lblwait = (Label)row.FindControl("lblwait");


                var result = text.Where(l => l.Equals(lblID.Text.Trim())).FirstOrDefault();

                if (result == null)
                {
                    impCorrect.Visible = true;
                    impdisCorrect.Visible = false;
                    lblwait.Visible = false;
                }
                else
                {
                    impdisCorrect.Visible = true;
                    impCorrect.Visible = false;
                    lblwait.Visible = false;
                }

            }



        }

        protected void btnValidateProp_Click(object sender, EventArgs e)
        {
            bool valid = false;
            var biz = new BLL.LicenseBiz();

            UpdateImportFile.Update();

            System.Drawing.Color colRed = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            System.Drawing.Color colGreen = System.Drawing.ColorTranslator.FromHtml("#33CC00");
            System.Drawing.Color colBlue = System.Drawing.ColorTranslator.FromHtml("#0000FF");

            string licenseType = Session["licenseType"].ToString();
            string petitionType = Session["petitionType"].ToString();
            string licenseName = Session["licneseName"].ToString();
            string pettionName = Session["pettionName"].ToString();


            if (Session["petitionType"].ToString() == "11")
            {
                if (!chkComfirm.Checked)
                {
                    UCModalWarning.ShowMessageWarning = "ท่านยังไม่ทำการยอมรับเงื่อนไขการขอรับใบอนุญาต";
                    UCModalWarning.ShowModalWarning();
                    return;
                }
            }


            valid = ChkErrmesssage();


            if (valid != false)
            {
                valid = ValidateDetail();
            }


            if (!valid)
            {

                var res = biz.GetDetail(Session["GroupID"].ToString());
                gvCheckList.DataSource = res.DataResponse;
                gvCheckList.DataBind();


                for (int i = 0; i <= gvCheckList.Rows.Count - 1; i++)
                {
                    LinkButton hplview = (LinkButton)gvCheckList.Rows[i].FindControl("hplview");
                    LinkButton hplViewProp = (LinkButton)gvCheckList.Rows[i].FindControl("hplViewProp");
                    Label lblErrGv = (Label)gvCheckList.Rows[i].FindControl("lblErrGv");
                    Label lblSeqNoGv = (Label)gvCheckList.Rows[i].FindControl("lblSeqNoGv");


                    if (hplview != null && hplViewProp != null)
                    {
                        hplview.Visible = true;

                        if (String.IsNullOrEmpty(lblErrGv.Text))
                        {
                            hplViewProp.Text = "ผ่าน";
                            hplViewProp.ForeColor = colGreen;

                            if (petitionType == "11")
                            {
                                DTO.ReceiveLicenseDetail recreiveLicenst = new DTO.ReceiveLicenseDetail();

                                recreiveLicenst = SummaryReceiveLicense.ReceiveLicenseDetails.SingleOrDefault(a => a.SEQ == lblSeqNoGv.Text);

                                if (recreiveLicenst.AttachFileDetails.Count() > 0)
                                {
                                    List<string> fileType = new List<string>();
                                    fileType = GetAttachFilesData(recreiveLicenst.AttachFileDetails);
                                    var dataBiz = new BLL.DataCenterBiz();
                                    var listExamDoc = dataBiz.GetExamSpecialDocument(fileType);

                                    if (listExamDoc.DataResponse.Count() > 0)
                                    {
                                        hplViewProp.Text = "รอพิจารณา";
                                        hplViewProp.ForeColor = colBlue;
                                    }
                                }

                            }
                            else if (petitionType == "14")
                            {
                                 DTO.ReceiveLicenseDetail recreiveLicenst = new DTO.ReceiveLicenseDetail();

                                recreiveLicenst = SummaryReceiveLicense.ReceiveLicenseDetails.SingleOrDefault(a => a.SEQ == lblSeqNoGv.Text);

                                if (recreiveLicenst.AttachFileDetails.Count() > 0)
                                {
                                    List<string> fileType = new List<string>();
                                    fileType = GetAttachFilesData(recreiveLicenst.AttachFileDetails);

                                    var dataBiz = new BLL.DataCenterBiz();
                                    var listTrainDoc = dataBiz.GetTrainSpecialDocument(fileType);

                                    if (listTrainDoc.DataResponse.Count() > 0)
                                    {
                                        hplViewProp.Text = "รอพิจารณา";
                                        hplViewProp.ForeColor = colBlue;
                                    }

                                }
                            }


                        }
                        else
                        {
                            hplViewProp.Text = "ไม่ผ่าน";
                            hplViewProp.ForeColor = colRed;
                        }

                    }
                }

                int missingTrans = res.DataResponse.Where(w => !string.IsNullOrEmpty(w.ERR_MSG)).Count();
                int rightTrans = res.DataResponse.Where(w => string.IsNullOrEmpty(w.ERR_MSG)).Count();

                UpdateErrHead(missingTrans, rightTrans);


                btnImport.Visible = false;
                lblMessageError.Visible = true;
                lblMessageError.Text = "มีคุณสมบัติที่ยังไม่ผ่านไม่สามารถนำเข้าได้";


                pnlCondition.Visible = false;
                btnValidateProp.Visible = false;
                btnImport.Visible = false;
                gvCheckList.Columns[7].Visible = true;
                gvCheckList.Columns[9].Visible = false;


            }

            else
            {
                btnValidateProp.Visible = false;
                btnImport.Visible = true;


                var res = biz.GetDetail(Session["GroupID"].ToString());
                gvCheckList.DataSource = res.DataResponse;
                gvCheckList.DataBind();

                for (int i = 0; i <= gvCheckList.Rows.Count - 1; i++)
                {
                    LinkButton hplViewProp = (LinkButton)gvCheckList.Rows[i].FindControl("hplViewProp");
                    Label lblErrGv = (Label)gvCheckList.Rows[i].FindControl("lblErrGv");
                    Label lblSeqNoGv = (Label)gvCheckList.Rows[i].FindControl("lblSeqNoGv");

                    if (hplViewProp != null)
                    {
                        if (String.IsNullOrEmpty(lblErrGv.Text))
                        {
                            hplViewProp.Text = "ผ่าน";
                            hplViewProp.ForeColor = colGreen;

                            if (petitionType == "11")
                            {
                                DTO.ReceiveLicenseDetail recreiveLicenst = new DTO.ReceiveLicenseDetail();

                                recreiveLicenst = SummaryReceiveLicense.ReceiveLicenseDetails.SingleOrDefault(a => a.SEQ == lblSeqNoGv.Text);

                                if (recreiveLicenst.AttachFileDetails.Count() > 0)
                                {
                                    List<string> fileType = new List<string>();
                                    fileType = GetAttachFilesData(recreiveLicenst.AttachFileDetails);
                                    var dataBiz = new BLL.DataCenterBiz();
                                    var listExamDoc = dataBiz.GetExamSpecialDocument(fileType);

                                    if (listExamDoc.DataResponse.Count() > 0)
                                    {
                                        hplViewProp.Text = "รอพิจารณา";
                                        hplViewProp.ForeColor = colBlue;
                                    }
                                }


                            }
                            else if (petitionType == "14")
                            {
                                DTO.ReceiveLicenseDetail recreiveLicenst = new DTO.ReceiveLicenseDetail();

                                recreiveLicenst = SummaryReceiveLicense.ReceiveLicenseDetails.SingleOrDefault(a => a.SEQ == lblSeqNoGv.Text);

                                if (recreiveLicenst.AttachFileDetails.Count() > 0)
                                {
                                    List<string> fileType = new List<string>();
                                    fileType = GetAttachFilesData(recreiveLicenst.AttachFileDetails);
                                    var dataBiz = new BLL.DataCenterBiz();
                                    var listTrainDoc = dataBiz.GetTrainSpecialDocument(fileType);

                                    if (listTrainDoc.DataResponse.Count() > 0)
                                    {
                                        hplViewProp.Text = "รอพิจารณา";
                                        hplViewProp.ForeColor = colBlue;
                                    }

                                }
                            }

                        }
                        else
                        {
                            hplViewProp.Text = "ไม่ผ่าน";
                            hplViewProp.ForeColor = colRed;
                        }

                    }
                }

                pnlCondition.Visible = false;
                //btnValidateProp.Visible = true;
                gvCheckList.Columns[7].Visible = true;
                gvCheckList.Columns[9].Visible = false;
                lblMessageError.Text = "";

            }

        }



        #endregion

        #region LinkPopup
        private String LinkPopUp(String postData)
        {
            return String.Format("window.open('{0}?targetImage={1}','','')"
                            , UrlHelper.Resolve("/UserControl/ViewFile.aspx"), IAS.Utils.CryptoBase64.Encryption(postData));
        }

        #endregion

        #region RowDataBount

        protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string licenseType = Session["licenseType"].ToString();
                string petitionType = Session["petitionType"].ToString();
                string licenseName = Session["licneseName"].ToString();
                string pettionName = Session["pettionName"].ToString();

                System.Drawing.Color colRed = System.Drawing.ColorTranslator.FromHtml("#FF0000");
                System.Drawing.Color colGreen = System.Drawing.ColorTranslator.FromHtml("#33CC00");

                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));
                LinkButton viewImg = ((LinkButton)e.Row.FindControl("hplViewImg"));
                LinkButton hplViewProp = ((LinkButton)e.Row.FindControl("hplViewProp"));
                Label lblIDNumberGv = (Label)e.Row.FindControl("lblIDNumberGv");
                Label lblSeqNoGv = (Label)e.Row.FindControl("lblSeqNoGv");
                Label lblErrGv = (Label)e.Row.FindControl("lblErrGv");


               // DTO.ReceiveLicenseDetail recreiveLicenst = SummaryReceiveLicense.ReceiveLicenseDetails.SingleOrDefault(a => a.SEQ == lblSeqNoGv.Text);


                //if (recreiveLicenst != null && recreiveLicenst.LOAD_STATUS == "F")
                //{
                //    view.Visible = true;
                //    if (String.IsNullOrEmpty(lblErrGv.Text))
                //    {
                //        hplViewProp.Text = "ผ่าน";
                //        hplViewProp.ForeColor = colGreen;

                //        if (petitionType=="11")
                //        {
                //            if (recreiveLicenst.AttachFileDetails.Count() > 0)
                //            {
                //                 List<string> fileType = new List<string>();
                //                 fileType = GetAttachFilesData(recreiveLicenst.AttachFileDetails);
                //                 var dataBiz = new BLL.DataCenterBiz();
                //                 var listExamDoc = dataBiz.GetExamSpecialDocument(fileType);
                //                 if (listExamDoc.DataResponse.Count() > 0)
                //                 {
                //                     hplViewProp.Text = "รอตรวจสอบ";
                //                     hplViewProp.ForeColor = colGreen;
                //                 }


                //            }
                //        }

                //    }
                //    else
                //    {
                //        hplViewProp.Text = "ไม่ผ่าน";
                //        hplViewProp.ForeColor = colRed;
                //    }
                //}
                //else
                //{
                //    view.Visible = true;
                //    if (String.IsNullOrEmpty(lblErrGv.Text))
                //    {
                //        hplViewProp.Text = "ผ่าน";
                //        hplViewProp.ForeColor = colGreen;
                //    }
                //    else
                //    {
                //        hplViewProp.Text = "ไม่ผ่าน";
                //        hplViewProp.ForeColor = colRed;
                //    }
                //}
            }
        }

        protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hplView = (LinkButton)e.Row.FindControl("hplViewDetailImg");
                Label lblFileGv = (Label)e.Row.FindControl("lblAttachFilePathGv");
                Label lblTypeAttachmentGv = (Label)e.Row.FindControl("lblTypeAttachmentGv");

                if (hplView != null)
                {
                    hplView.Visible = true;
                    hplView.OnClientClick = LinkPopUp((lblFileGv != null) ? lblFileGv.Text : "");
                }

            }
        }

        protected void gvImportFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemsIncorrectRemarkGv = (Label)e.Row.FindControl("lblItemsIncorrectRemark");

                if (lblItemsIncorrectRemarkGv != null)
                {
                    lblItemsIncorrectRemarkGv.Text = SummaryReceiveLicense.MessageError;
                }

            }
        }

        #endregion

        #region CancleClick

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ControlHelper ctrlHelper = new ControlHelper();
            ctrlHelper.ClearInput(this);
            this.Response.Redirect("~/License/LicenseGroup.aspx?");
            return;
        }

        #endregion

        #region RowCreate

        protected void Head_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manualy onece the header creater
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;


            }
        }

        private static void AddSuperHeader(GridView gridView, string lincenseType, string pettionType, string licenseName, string pettionName)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            myNewRow.Cells.Add(MakeCell("ลำดับที่", 1));
            myNewRow.Cells.Add(MakeCell("เงื่อนไขการตรวจสอบ", 1));
            myNewRow.Cells.Add(MakeCell("ผลการตรวจสอบ", 1));
            myNewRow.Cells.Add(MakeCell("หมายเหตุ", 1));

            myTable.Rows.AddAt(0, myNewRow);

        }

        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { Text = text ?? string.Empty, ColumnSpan = span, HorizontalAlign = HorizontalAlign.Center };
        }

        #endregion

        #region SelectIndexChanged

        protected void gvCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {

            List<DTO.AttachFileDetail> attachls = new List<DTO.AttachFileDetail>();

            Label lblErr = (Label)gvCheckList.SelectedRow.FindControl("lblErrGv");
            Label idCard = (Label)gvCheckList.SelectedRow.FindControl("lblIDNumberGv");
            Label licenseNo = (Label)gvCheckList.SelectedRow.FindControl("lblLicenseNo");

            var biz = new BLL.LicenseBiz();

            string licenseType = Session["licenseType"].ToString();
            string petitionType = Session["petitionType"].ToString();
            string licenseName = Session["licneseName"].ToString();
            string pettionName = Session["pettionName"].ToString();

            int renewTime = 0;

            var tagPettition = new string[] { "11", "15", "16", "17", "18" };



            ClearGridViewData();


            if (tagPettition.Contains(petitionType))
            {
                renewTime = 0;
            }
            else
            {
                var res = biz.GetRenewTimebyLicenseNo(licenseNo.Text);
                if (res != null)
                {
                    renewTime = res.ResultMessage.ToInt() + 1;
                }
            }

            var result = biz.GetPropLiecense(licenseType, petitionType, renewTime, 1);
            gvGeneral.DataSource = result.DataResponse;
            gvGeneral.DataBind();



            result = biz.GetPropLiecense(licenseType, petitionType, renewTime, 2);
            gvExamResult.DataSource = result.DataResponse;
            gvExamResult.DataBind();

            if (result != null)
            {

                if (petitionType == "11")
                {

                    var exBiz = new BLL.ApplicantBiz();
                    var errDup = new StringBuilder("");

                    var chkdup = exBiz.CheckApplicantExamDup(idCard.Text.ToString());

                    if (chkdup.Count != 0)
                    {
                        errDup.Append("มีการสมัครสอบซ้ำ โดยมีรายละเอียดดังนี้ " + "<br />");

                        foreach (var dup in chkdup)
                        {
                            errDup.Append("- รหัสรอบสอบ " + dup + " " + " <br />");
                        }

                        foreach (GridViewRow row in gvExamResult.Rows)
                        {
                            Label lblDef = (Label)row.FindControl("lblDef");

                            lblDef.Text = errDup.ToString();
                        }
                    }


                }


            }

            if (licenseType == "03" || licenseType == "04")
            {
                if (petitionType == "11")
                {


                    string idValue = string.Empty;

                    if (licenseType == "03")
                    {
                        idValue = DTO.ConfigAgenType.AgentLife.GetEnumValue().ToString();
                    }
                    else if (licenseType == "04")
                    {
                        idValue = DTO.ConfigAgenType.AgentCasualty.GetEnumValue().ToString();
                    }

                    var res = biz.GetAgentCheckTrain(idValue);

                    if (res.ResultMessage == true)
                    {
                        result = biz.GetPropLiecense(licenseType, petitionType, renewTime, 4);
                        gvTrainResult.DataSource = result.DataResponse;
                        gvTrainResult.DataBind();

                    }
                }
                else
                {
                    result = biz.GetPropLiecense(licenseType, petitionType, renewTime, 4);
                    gvTrainResult.DataSource = result.DataResponse;
                    gvTrainResult.DataBind();
                }
            }
            else
            {
                result = biz.GetPropLiecense(licenseType, petitionType, renewTime, 4);
                gvTrainResult.DataSource = result.DataResponse;
                gvTrainResult.DataBind();
            }

            //Create Head Genaral
            if (gvGeneral.Controls.Count > 0)
            {
                AddSuperHeader(gvGeneral, licenseType, petitionType, licenseName, pettionName);
            }
            else if (gvTrainResult.Controls.Count > 0)
            {
                AddSuperHeader(gvTrainResult, licenseType, petitionType, licenseName, pettionName);
            }


            List<string> text = lblErr.Text.Trim().Split(new string[] { "<br />", "\n\r" }, StringSplitOptions.None).ToList();


            chkValidateProp(text, gvGeneral);
            chkValidateSpecialProp(text, gvExamResult);
            chkValidateSpecialProp(text, gvTrainResult);


            if (petitionType == "11" || petitionType == "14")
            {
                DTO.ReceiveLicenseDetail receiveLicenseDetail = this.SummaryReceiveLicense.ReceiveLicenseDetails.FirstOrDefault(a => a.CITIZEN_ID.Trim() == idCard.Text.Trim());
                if (receiveLicenseDetail.AttachFileDetails.Count() > 0)
                {
                    List<string> fileType = new List<string>();

                    fileType = GetAttachFilesData(receiveLicenseDetail.AttachFileDetails);
                    var dataBiz = new BLL.DataCenterBiz();


                    if (petitionType == "11")
                    {

                        var listExamSpecial = dataBiz.GetExamSpecial(idCard.Text.ToString(), licenseType);

                        if (listExamSpecial.DataResponse.Count() > 0)
                        {
                            gvExamSpecial.DataSource = listExamSpecial.DataResponse;
                            gvExamSpecial.DataBind();

                            foreach (GridViewRow row in gvExamSpecial.Rows)
                            {
                                Image impCorrect = (Image)row.FindControl("impCorrect");
                                Label lblwait = (Label)row.FindControl("lblwait");

                                impCorrect.Visible = true;
                                lblwait.Visible = false;
                            }
                        }
                        else
                        {
                            var listExamDoc = dataBiz.GetExamSpecialDocument(fileType);

                            if (listExamDoc.DataResponse.Count() > 0)
                            {
                                gvExamSpecial.DataSource = listExamDoc.DataResponse;
                                gvExamSpecial.DataBind();

                                foreach (GridViewRow row in gvExamSpecial.Rows)
                                {
                                    Image impCorrect = (Image)row.FindControl("impCorrect");
                                    Label lblwait = (Label)row.FindControl("lblwait");

                                    impCorrect.Visible = false;
                                    lblwait.Visible = true;
                                }


                                if (gvExamResult.Controls.Count > 0)
                                {
                                    foreach (GridViewRow row in gvExamResult.Rows)
                                    {
                                        Image impCorrect = (Image)row.FindControl("impCorrect");
                                        Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                                        Label lblwait = (Label)row.FindControl("lblwait");

                                        if (impCorrect.Visible == true)
                                        {
                                            impCorrect.Visible = false;
                                            impdisCorrect.Visible = false;
                                            lblwait.Visible = true;
                                        }

                                    }

                                }


                            }

                        }
                    }

                    if (petitionType == "14")
                    {
                        var listTrainSpecail = dataBiz.GetTrainSpecialbyIdCard(idCard.Text.ToString());

                        if (listTrainSpecail.DataResponse.Count() > 0)
                        {
                            gvTrainSpecial.DataSource = listTrainSpecail.DataResponse;
                            gvTrainSpecial.DataBind();


                            foreach (GridViewRow row in gvTrainSpecial.Rows)
                            {
                                Image impCorrect = (Image)row.FindControl("impCorrect");
                                Label lblwait = (Label)row.FindControl("lblwait");

                                impCorrect.Visible = true;
                                lblwait.Visible = false;
                            }
                        }


                        var listTrainDoc = dataBiz.GetTrainSpecialDocument(fileType);

                        if (listTrainDoc.DataResponse.Count() > 0)
                        {
                            if (gvTrainSpecial.Rows.Count == 0)
                            {
                                gvTrainSpecial.DataSource = listTrainDoc.DataResponse;
                                gvTrainSpecial.DataBind();

                                foreach (GridViewRow row in gvTrainSpecial.Rows)
                                {
                                    Image impCorrect = (Image)row.FindControl("impCorrect");
                                    Label lblwait = (Label)row.FindControl("lblwait");

                                    impCorrect.Visible = false;
                                    lblwait.Visible = true;
                                }




                                if (gvTrainResult.Controls.Count > 0)
                                {
                                    foreach (GridViewRow row in gvTrainResult.Rows)
                                    {
                                        Image impCorrect = (Image)row.FindControl("impCorrect");
                                        Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                                        Label lblwait = (Label)row.FindControl("lblwait");

                                        if (impCorrect.Visible == true)
                                        {
                                            impCorrect.Visible = false;
                                            impdisCorrect.Visible = false;
                                            lblwait.Visible = true;
                                        }

                                    }

                                }
                            }

                            else if (gvTrainSpecial.Rows.Count > 0)
                            {
                                int count = gvTrainSpecial.Rows.Count;

                                foreach (var item in listTrainDoc.DataResponse)
                                {
                                    count = count + 1;

                                    var myTable = (Table)gvTrainSpecial.Controls[0];

                                    var myNewRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);

                                    myNewRow.Cells.Add(new TableCell() { Text = count.ToString(), ColumnSpan = 1, CssClass = "td-center", VerticalAlign = VerticalAlign.Top });
                                    myNewRow.Cells.Add(new TableCell() { Text = item.Name, ColumnSpan = 1, CssClass = "td-left" });
                                    myNewRow.Cells.Add(new TableCell() { Text = "รอการตรวจสอบ", ColumnSpan = 1, HorizontalAlign = HorizontalAlign.Center });
                                    myNewRow.Cells.Add(new TableCell() { Text = "", ColumnSpan = 1, HorizontalAlign = HorizontalAlign.Center });

                                    if (count % 2 == 0)
                                    {
                                        myNewRow.CssClass = "altrow";
                                    }

                                    myTable.Rows.Add(myNewRow);
                                }

                                if (gvTrainResult.Controls.Count > 0)
                                {
                                    foreach (GridViewRow row in gvTrainResult.Rows)
                                    {
                                        Image impCorrect = (Image)row.FindControl("impCorrect");
                                        Image impdisCorrect = (Image)row.FindControl("impdisCorrect");
                                        Label lblwait = (Label)row.FindControl("lblwait");

                                        if (impCorrect.Visible == true)
                                        {
                                            impCorrect.Visible = false;
                                            impdisCorrect.Visible = false;
                                            lblwait.Visible = true;
                                        }

                                    }

                                }
                            }
                        }

                    }
                }
            }


            mdpValidateprop.Show();

        }

        private void ClearGridViewData()
        {

            this.gvGeneral.DataSource = null;
            this.gvGeneral.DataBind();

            this.gvExamResult.DataSource = null;
            this.gvExamResult.DataBind();

            this.gvTrainResult.DataSource = null;
            this.gvTrainResult.DataBind();


            this.gvTrainSpecial.DataSource = null;
            this.gvTrainSpecial.DataBind();

            this.gvExamSpecial.DataSource = null;
            this.gvExamSpecial.DataBind();

        }

        protected void ddlObtainLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlObtainLicenseType.SelectedValue == "16")
            {
                lblReplaceType.Visible = true;
                rblType.Visible = true;
            }
            else
            {
                lblReplaceType.Visible = false;
                rblType.Visible = false;
                rblType.ClearSelection();
            }
        }

        protected void ddlLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlLicenseType.SelectedValue))
            {
                GetPettitionType();
                GetApprove();
                lblReplaceType.Visible = false;
                rblType.Visible = false;
                rblType.ClearSelection();
            }
            else
            {
                ddlObtainLicenseType.Items.Clear();
                ddlApproveBy.Items.Clear();
                lblReplaceType.Visible = false;
                rblType.Visible = false;
                rblType.ClearSelection();
            }
        }

        #endregion

    }
}
