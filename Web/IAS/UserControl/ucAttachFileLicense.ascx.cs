using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;
using System.Xml;
using System.Text;
using System.IO;
using IAS.Utils;
using IAS.BLL.FileService;
using IAS.MasterPage;
using AjaxControlToolkit;
using IAS.BLL;
using IAS.Properties;
using IAS.DTO.FileService;

namespace IAS.UserControl
{
    public partial class ucAttachFileLicense : System.Web.UI.UserControl
    {
        //Add
        #region Control
        public DropDownList DropDownDocumentType { get { return ddlDocumentType; } set { ddlDocumentType = value; } }
        public TextBox TextRemark { get { return txtRemark; } set { txtRemark = value; } }
        public Button ButtonUpload { get { return btnUploadFile; } set { btnUploadFile = value; } }
        public GridView GridAttachFiles { get { return gvAttachFiles; } set { gvAttachFiles = value; } }
        public Panel PnlAttachFiles { get { return pnlAttachFiles; } set { pnlAttachFiles = value; } }
        #endregion Control

        #region Public Param & Session
        private string[] memberType = { Resources.propReg_NotApprove_MemberTypeGeneral, Resources.propReg_Co_MemberTypeCompany, Resources.propReg_Assoc_MemberTypeAssoc };
        private IList<AttachFile> _attachFiles;
        private IList<DTO.DataItem> _documentTypes;
        private String _registrationId = "";
        private String _remark = "";
        private String _currentUser = "";
        private DTO.DataActionMode _modeForm;
        public String RegisterationId { get { return _registrationId; } set { _registrationId = value; } }
        public DTO.DataActionMode ModeForm { get { return _modeForm; } set { _modeForm = value; } }
        public bool Enabled { get; set; }
        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session[PageList.UserProfile] == null ? null : (DTO.UserProfile)Session[PageList.UserProfile];
            }
        }

        public String CurrentUser
        {
            get { return (UserProfile == null) ? "Guest" : UserProfile.LoginName; }
            set { _currentUser = value; }

        }

        public Site1 MasterPage
        {
            get { return (this.Page.Master as Site1); }
        }

        public MasterRegister MasterRegis
        {
            get { return (this.Page.Master as MasterRegister); }
        }

        public MasterLicense MasterLicense
        {
            get { return (this.Page.Master as MasterLicense); }
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.MasterLicense.CurrentUploadGroupNO = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                Session["AttachFiles"] = null;
                InitAttachFiles();
                BindAll();
                ViewState["RegisterationId"] = this.UserProfile.Id;
                ViewState["CurrentUser"] = CurrentUser;
                ViewState["ModeForm"] = ModeForm;
                //ViewState["TextRemark"] = TextRemark.Text;
                //ViewState["DocumentType"] = DropDownDocumentType.SelectedValue;
            }
            else
            {
                _attachFiles = (IList<AttachFile>)Session["AttachFiles"];
                _registrationId = ViewState["RegisterationId"].ToString();
                _currentUser = ViewState["CurrentUser"].ToString();
                //TextRemark.Text = ViewState["TextRemark"].ToString();
            }
            GetDocReqName();
        }
        #endregion

        #region UI Function
        protected void hplCancel_Click(object sender, EventArgs e)
        {
            try
            {
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                String attachFileId = ((Label)gr.FindControl("lblAttachFileId")).Text;
                String lblDocumentCode = ((Label)gr.FindControl("lblDocumentCode")).Text;
                String fileStatus = ((Label)gr.FindControl("lblFileStatus")).Text;


                for (int i = 0; i < _attachFiles.Count(); i++)
                {
                    if (_attachFiles[i].ID == attachFileId && _attachFiles[i].FILE_STATUS == fileStatus)
                    {
                        _attachFiles.RemoveAt(i);
                        break;
                    }

                }

                BindAttachFile();
                udpAttachFiles.Update();
            }
            catch (Exception ex)
            {
                AlertMessageShow(ex.Message);
            }
        }

        protected void hplDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

                String attachFileId = ((Label)gr.FindControl("lblAttachFileId")).Text;
                String lblDocumentCode = ((Label)gr.FindControl("lblDocumentCode")).Text;
                String fileStatus = ((Label)gr.FindControl("lblFileStatus")).Text;

                AttachFile attachFile = _attachFiles.Single(a => a.ATTACH_FILE_TYPE == lblDocumentCode && a.FILE_STATUS == fileStatus);

                if (attachFile.FILE_STATUS == AttachFileStatus.Active.Value())
                {
                    AttachFile tempAtachFile = TempAttachFiles.SingleOrDefault(p => p.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE);

                    if (tempAtachFile == null)
                    {
                        DateTime dateNow = DateTime.Now;
                        AttachFile deleteAttach = new AttachFile();
                        deleteAttach = attachFile.ConvertToAttachFileView();
                        deleteAttach.FILE_STATUS = AttachFileStatus.Delete.Value();
                        deleteAttach.CREATED_DATE = dateNow;
                        deleteAttach.UPDATED_DATE = dateNow;
                        _attachFiles.Add(deleteAttach);
                        BindAttachFile();
                        return;
                    }
                    else if (tempAtachFile.FILE_STATUS == AttachFileStatus.Edit.Value())
                    {
                        _attachFiles.Remove(tempAtachFile);
                        DateTime dateNow = DateTime.Now;
                        AttachFile deleteAttach = attachFile.ConvertToAttachFileView();
                        deleteAttach.FILE_STATUS = AttachFileStatus.Delete.Value();
                        deleteAttach.CREATED_DATE = dateNow;
                        deleteAttach.UPDATED_DATE = dateNow;
                        _attachFiles.Add(deleteAttach);
                        BindAttachFile();
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                AlertMessageShow(ex.Message);
            }

            udpAttachFiles.Update();
        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (!fulAttachFile.HasFile)
            {
                AlertMessageShow(SysMessage.CannotUploadFile);
                return;
            }

            var documentCode = ddlDocumentType.SelectedValue;
            DataCenterBiz dbiz = new DataCenterBiz();
            var imgItem = dbiz.GetPicByDocumentCode(documentCode);
            if (imgItem != null)
            {
                if (!FilterContentTypeImage(fulAttachFile.PostedFile.ContentType))
                {
                    AlertMessageShow(String.Format("ไม่สามารถนำเข้าไฟล์ ประเภท{0} ได้.", fulAttachFile.FileName.Substring(fulAttachFile.FileName.LastIndexOf('.'))));
                    return;
                }
            }
            else
            {
                if (!FilterContentType(fulAttachFile.PostedFile.ContentType))
                {
                    AlertMessageShow(String.Format("ไม่สามารถนำเข้าไฟล์ ประเภท{0} ได้.", fulAttachFile.FileName.Substring(fulAttachFile.FileName.LastIndexOf('.'))));
                    return;
                }
            }

            if (fulAttachFile.FileBytes.LongCount() > 1048576)
            {
                AlertMessageShow(SysMessage.AttachNotOverOneMB);
                return;
            }

            AttachFile attachFile = ConcreateFromControl();

            Stream fileStream = fulAttachFile.PostedFile.InputStream;
            try
            {
                if (ActiveAttachFiles.Where(a => a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE).Count() > 0)
                {
                    if (TempAttachFiles.Where(t => t.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                                    && t.FILE_STATUS == AttachFileStatus.Wait.Value()).Count() > 0)
                    {
                        AlertMessageShow(SysMessage.AttachFileDupicate);
                        return;
                    }
                    if (TempAttachFiles.Where(t => t.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                                                    && t.FILE_STATUS == AttachFileStatus.Delete.Value()).Count() > 0)
                    {
                        var biz = new BLL.UploadDataBiz();
                        UploadFileResponse response = new UploadFileResponse();
                        response = biz.UploadAttachFileToTemp(attachFile, fileStream);

                        if (response.Code != "0000")
                        {
                            AlertMessageShow(response.Message);
                            return;
                        }
                        attachFile.ATTACH_FILE_PATH = response.TargetFullName;

                        AttachFile deleteAttach = _attachFiles.Single(t => t.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                                                    && t.FILE_STATUS == AttachFileStatus.Delete.Value());

                        attachFile.MappingToEntity<AttachFile>(deleteAttach);
                        DefaultValue();
                        BindAttachFile();
                    }
                    else
                    {
                        AlertMessageShow(SysMessage.AttachFileDupicate);
                        return;
                    }
                }
                else
                {
                    if (TempAttachFiles.Where(t => t.ATTACH_FILE_TYPE == ddlDocumentType.SelectedValue
                                                    && t.FILE_STATUS == AttachFileStatus.Wait.Value()).Count() > 0)
                    {
                        AlertMessageShow(SysMessage.AttachFileDupicate);
                        return;
                    }
                    else
                    {
                        var biz = new BLL.UploadDataBiz();
                        UploadFileResponse response = new UploadFileResponse();
                        //response = biz.UploadAttachFileToTemp(attachFile, fileStream);
                        response = biz.UploadAttachFileLicenseToTemp(attachFile, fileStream);


                        if (response.Code != "0000")
                        {
                            AlertMessageShow(response.Message);
                            return;
                        }
                        attachFile.ATTACH_FILE_PATH = response.TargetFullName;

                        _attachFiles.Add(attachFile);
                        DefaultValue();
                        BindAttachFile();


                        //Add to masterLicense
                        this.MasterLicense.AttachFiles = _attachFiles;
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessageShow(ex.Message);
            }

            udpAttachFiles.Update();
        }

        protected void gvAttachFiles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAttachFiles.EditIndex = e.NewEditIndex;
            BindAttachFile();
        }

        protected void gvAttachFiles_PreRender(object sender, EventArgs e)
        {
            if (this.gvAttachFiles.EditIndex != -1)
            {

                LinkButton hplView = gvAttachFiles.Rows[gvAttachFiles.EditIndex].FindControl("hplView") as LinkButton;
                LinkButton hplDelete = gvAttachFiles.Rows[gvAttachFiles.EditIndex].FindControl("hplDelete") as LinkButton;

                ((DataControlField)gvAttachFiles.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "ดำเนินการ")
                .SingleOrDefault()).Visible = false;

            }

        }

        protected void gvAttachFiles_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvAttachFiles.EditIndex = -1;

            ((DataControlField)gvAttachFiles.Columns
            .Cast<DataControlField>()
            .Where(fld => fld.HeaderText == "ดำเนินการ")
            .SingleOrDefault()).Visible = true;

            BindAttachFile();

        }


        private Boolean FilterContentType(String contentType)
        {
            String typeXls = Utils.ContentTypeHelper.MimeType(".xls");

            switch (contentType)
            {
                //case "application/vnd.ms-excel": return true;
                //case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": return true;
                case "application/msword": return true;
                case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": return true;
                case "image/bmp": return true;
                case "image/gif": return true;
                case "image/jpeg": return true;
                case "image/png": return true;
                case "image/x-png": return true;
                case "image/pjpeg": return true;
                case "image/tiff": return true;
                case "application/pdf": return true;

                default:
                    return false;
            }
        }

        private Boolean FilterContentTypeImage(String contentType)
        {
            String typeXls = Utils.ContentTypeHelper.MimeType(".xls");

            DataCenterBiz biz = new DataCenterBiz();
            var res = biz.GetDocumentTypeAll("");

            switch (contentType)
            {
                case "image/bmp": return true;
                case "image/gif": return true;
                case "image/jpeg": return true;
                case "image/png": return true;
                case "image/x-png": return true;
                case "image/pjpeg": return true;
                case "image/tiff": return true;

                default:
                    return false;
            }
        }

        protected void gvAttachFiles_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvAttachFiles.Rows[e.RowIndex];
            //Only 0-1 Element
            //AttachFile attachFile = ActiveAttachFiles.Single(a => a.ID == ((Label)row.FindControl("lblAttachFileId")).Text);
            //Only one more Element
            AttachFile attachFile = ActiveAttachFiles.FirstOrDefault(a => a.ID == ((Label)row.FindControl("lblAttachFileId")).Text);
            TextBox rowTextRemark = (TextBox)row.FindControl("txtDetailGv");

            AttachFile editAttachFile = attachFile.ConvertToAttachFileView();

            editAttachFile.REMARK = (rowTextRemark == null) ? "" : rowTextRemark.Text;
            editAttachFile.FILE_STATUS = AttachFileStatus.Edit.Value();


            if (TempAttachFiles.Where(t => t.ATTACH_FILE_TYPE == editAttachFile.ATTACH_FILE_TYPE).Count() > 0)
            {
                AlertMessageShow(SysMessage.AttachFileDupicate);
            }
            else
            {
                _attachFiles.Add(editAttachFile);
            }

            gvAttachFiles.EditIndex = -1;

            ((DataControlField)gvAttachFiles.Columns
            .Cast<DataControlField>()
            .Where(fld => fld.HeaderText == "ดำเนินการ")
            .SingleOrDefault()).Visible = true;

            BindAttachFile();
        }

        protected void gvAttachFiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvAttachFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) {

                String attachFileId = ((Label)e.Row.FindControl("lblAttachFileId")).Text;
                String fileStatus = ((Label)e.Row.FindControl("lblFileStatus")).Text;
                //Only For 0-1 Element
                //AttachFile attachFile = AttachFiles.Single(a => a.ID == attachFileId && a.FILE_STATUS==fileStatus);
                //Only For one more Element
                AttachFile attachFile = AttachFiles.FirstOrDefault(a => a.ID == attachFileId && a.FILE_STATUS == fileStatus);
                if (attachFile != null)
                {
                    LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lbnEditGv");
                    LinkButton hplView = (LinkButton)e.Row.FindControl("hplView");
                    LinkButton hplDelete = (LinkButton)e.Row.FindControl("hplDelete");
                    LinkButton hplCancel = (LinkButton)e.Row.FindControl("hplCancel");

                    Label lblDocumentName = (Label)e.Row.FindControl("lblDocumentName");
                    lblDocumentName.Text = ddlDocumentType.Items.FindByValue(attachFile.ATTACH_FILE_TYPE).Text;

                    //if (hplView != null) hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    if (_modeForm == DTO.DataActionMode.View)
                    {
                        BindingRowModeView(attachFile, lbnEditGv, hplView, hplDelete, hplCancel, e);
                    }
                    else
                    {
                        BindingRowMode(attachFile, lbnEditGv, hplView, hplDelete, hplCancel, e);
                    }


                }
                //}

            }
        }

        private void BindingRowMode(AttachFile attachFile, LinkButton lbnEditGv, LinkButton hplView, LinkButton hplDelete, LinkButton hplCancel, GridViewRowEventArgs e)
        {
            if (attachFile.FILE_STATUS == AttachFileStatus.Active.Value())
            {
                if (TempAttachFiles.Where(a => a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                                            && (a.FILE_STATUS == AttachFileStatus.Edit.Value()
                                            || a.FILE_STATUS == AttachFileStatus.Wait.Value())).Count() > 0)
                {
                    if (lbnEditGv != null) lbnEditGv.Visible = false;
                    if (hplView != null) hplView.Visible = false;
                    if (hplDelete != null) hplDelete.Visible = false;
                    if (hplCancel != null) hplCancel.Visible = false;

                    e.Row.Style.Value = "text-decoration:line-through;";
                }
                else if (TempAttachFiles.Where(a => a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                                           && a.FILE_STATUS == AttachFileStatus.Delete.Value()).Count() > 0)
                {
                    e.Row.Visible = false;

                }

                else
                {
                    if (lbnEditGv != null) lbnEditGv.Visible = true;
                    if (hplView != null)
                    {
                        hplView.Visible = true;
                        hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    }
                    if (hplDelete != null) hplDelete.Visible = true;
                    if (hplCancel != null) hplCancel.Visible = false;
                }

            }
            else if (attachFile.FILE_STATUS == AttachFileStatus.Delete.Value())
            {
                if (TempAttachFiles.Where(a => a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE
                                          && a.FILE_STATUS == AttachFileStatus.Wait.Value()).Count() > 0)
                {
                    if (lbnEditGv != null) lbnEditGv.Visible = false;
                    if (hplView != null)
                    {
                        hplView.Visible = true;
                        hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    }
                    if (hplDelete != null) hplDelete.Visible = false;
                    if (hplCancel != null) hplCancel.Visible = false;
                }
                else
                {
                    if (lbnEditGv != null) lbnEditGv.Visible = false;
                    if (hplView != null)
                    {
                        hplView.Visible = true;
                        hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    }
                    if (hplDelete != null) hplDelete.Visible = false;
                    if (hplCancel != null) hplCancel.Visible = true;
                }


                e.Row.Style.Value = "text-decoration:line-through;";
            }
            else if (attachFile.FILE_STATUS == AttachFileStatus.Edit.Value())
            {
                if (lbnEditGv != null) lbnEditGv.Visible = false;
                if (hplView != null)
                {
                    hplView.Visible = true;
                    hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                }
                if (hplDelete != null) hplDelete.Visible = false;
                if (hplCancel != null) hplCancel.Visible = true;
            }
            else if (attachFile.FILE_STATUS == AttachFileStatus.Wait.Value())
            {

                if (lbnEditGv != null) lbnEditGv.Visible = false;
                if (hplView != null)
                {
                    hplView.Visible = true;
                    hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                }
                if (hplDelete != null) hplDelete.Visible = false;
                if (hplCancel != null) hplCancel.Visible = true;


            }
        }

        private void SwitchControler(AttachFile attachFile, AttachFileStatus status, LinkButton lbnEditGv, LinkButton hplView, LinkButton hplDelete, LinkButton hplCancel)
        {
            switch (status)
            {
                case AttachFileStatus.Active:
                    if (lbnEditGv != null) lbnEditGv.Visible = true;
                    if (hplDelete != null) lbnEditGv.Visible = true;
                    if (hplCancel != null) lbnEditGv.Visible = true;
                    if (hplView != null)
                    {
                        hplView.Visible = true;
                        hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    }
                    break;
                case AttachFileStatus.Edit:
                    if (lbnEditGv != null) lbnEditGv.Visible = true;
                    if (hplDelete != null) lbnEditGv.Visible = true;
                    if (hplCancel != null) lbnEditGv.Visible = true;
                    if (hplView != null)
                    {
                        hplView.Visible = true;
                        hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    }
                    break;
                case AttachFileStatus.Wait:
                    break;
                case AttachFileStatus.Delete:
                    break;
                default:
                    break;
            }
        }

        private void BindingRowModeView(AttachFile attachFile, LinkButton lbnEditGv, LinkButton hplView, LinkButton hplDelete, LinkButton hplCancel, GridViewRowEventArgs e)
        {
            if (lbnEditGv != null) lbnEditGv.Visible = false;
            if (hplView != null)
            {
                hplView.Visible = true;
                hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
            }
            if (hplDelete != null) hplDelete.Visible = false;
            if (hplCancel != null) hplCancel.Visible = false;
        }
        #endregion

        #region Main Public && Private Function
        public IList<DTO.DataItem> DocumentTypes
        {
            get { return _documentTypes; }
            set { _documentTypes = value; }
        }

        public IList<AttachFile> AttachFiles
        {
            get { return _attachFiles; }

            set { _attachFiles = value; }
        }

        public IList<AttachFile> TempAttachFiles
        {
            get { return _attachFiles.Where(a => a.FILE_STATUS != AttachFileStatus.Active.Value()).ToList(); }
        }

        public IList<AttachFile> ActiveAttachFiles
        {
            get { return _attachFiles.Where(a => a.FILE_STATUS == AttachFileStatus.Active.Value()).ToList(); }
        }

        public void EnableUpload(Boolean IsEnableed)
        {
            pnlUpload.Enabled = IsEnableed;
            fulAttachFile.Enabled = IsEnableed;
            btnUploadFile.Enabled = IsEnableed;
        }

        public void EnableGridView(Boolean IsEnableed)
        {
            pnlGridView.Enabled = IsEnableed;
        }

        public void VisableUpload(Boolean IsVisable)
        {
            pnlUpload.Visible = IsVisable;
        }

        public void VisableGridView(Boolean IsVisable)
        {
            pnlGridView.Visible = IsVisable;
        }

        public void BindAll()
        {
            BindDocumentType();
            BindAttachFile();

        }

        public void BindAttachFile()
        {
            gvAttachFiles.DataSource = (_attachFiles.OrderBy(l => l.ATTACH_FILE_TYPE)).ToList();
            gvAttachFiles.DataBind();
            udpAttachFiles.Update();
        }

        public void BindDocumentType()
        {
            ddlDocumentType.DataSource = DocumentTypes;
            ddlDocumentType.DataTextField = "Name";
            ddlDocumentType.DataValueField = "Id";
            ddlDocumentType.DataBind();
            udpAttachFiles.Update();
        }

        private AttachFile ConcreateFromControl()
        {
            //var head = this.MasterLicense.PersonLicenseH;
            //var detail = this.MasterLicense.PersonLicenseD;

            DateTime curDate = DateTime.Now;
            //AttachFile attachFile = new AttachFile()
            //{
            //    ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
            //    REGISTRATION_ID = RegisterationId,
            //    ATTACH_FILE_TYPE = ddlDocumentType.SelectedValue,
            //    ATTACH_FILE_PATH = fulAttachFile.FileName,
            //    REMARK = txtRemark.Text,
            //    CREATED_BY = CurrentUser,
            //    CREATED_DATE = curDate,
            //    UPDATED_BY = CurrentUser,
            //    UPDATED_DATE = curDate,
            //    FILE_STATUS = AttachFileStatus.Wait.Value(),
            //    GROUP_LICENSE_ID = this.MasterLicense.PersonLicenseH[0].UPLOAD_GROUP_NO,
            //    ID_CARD_NO = this.MasterLicense.PersonLicenseD[0].ID_CARD_NO
            //};

            AttachFile attachFile = new AttachFile()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                REGISTRATION_ID = RegisterationId,
                ATTACH_FILE_TYPE = ddlDocumentType.SelectedValue,
                ATTACH_FILE_PATH = fulAttachFile.FileName,
                REMARK = txtRemark.Text,
                CREATED_BY = CurrentUser,
                CREATED_DATE = curDate,
                UPDATED_BY = CurrentUser,
                UPDATED_DATE = curDate,
                FILE_STATUS = AttachFileStatus.Wait.Value(),
                GROUP_LICENSE_ID = this.MasterLicense.CurrentUploadGroupNO,
                ID_CARD_NO = this.UserProfile.IdCard
            };


            return attachFile;

        }

        private void InitAttachFiles()
        {
            if (_attachFiles != null)
                Session["AttachFiles"] = _attachFiles;
            else if (Session["AttachFiles"] != null)
                _attachFiles = (IList<AttachFile>)Session["AttachFiles"];
            else
            {
                _attachFiles = new List<AttachFile>();
                Session["AttachFiles"] = _attachFiles;
            }
        }

        private void AlertMessageShow(String message)
        {
            string alertMessage = String.Format("alert('{0}');", message);

            ScriptManager.RegisterClientScriptBlock(this.Parent, this.GetType(), "alert", alertMessage, true);
            //AjaxControlToolkit.ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alertMessage, true);
        }

        private String LinkPopUp(String postData)
        {
            return String.Format("window.open('{0}?targetImage={1}','','')"
                            , UrlHelper.Resolve("/UserControl/ViewFile.aspx"), IAS.Utils.CryptoBase64.Encryption(postData));
        }

        private void DefaultValue()
        {
            ddlDocumentType.SelectedIndex = 0;
            txtRemark.Text = "";
        }

        /// <summary>
        /// License Req Validation
        /// </summary>
        /// DataCenterService
        /// <returns>DTO.ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> DocRequire()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            DataCenterBiz biz = new DataCenterBiz();
            List<FilesLiceValidate> filesCheck = new List<FilesLiceValidate>();
            //IAS.DTO.ResponseService<IAS.DTO.ConfigDocument[]> getDoc = biz.GetDocumentLicenseConfig(this.MasterLicense.PettionTypeCode, this.MasterLicense.LicenseTypeCode);
            //Get Doc Req
            IAS.DTO.ResponseService<IAS.DTO.ConfigDocument[]> getDoc = biz.GetDocRequire(Convert.ToString(DTO.DocFunction.LICENSE_FUNCTION.GetEnumValue()),
                Convert.ToString(this.MasterLicense.UserProfile.MemberType), this.MasterLicense.LicenseTypeCode, this.MasterLicense.PettionTypeCode);

            if (getDoc.DataResponse.Count().Equals(0) && (!getDoc.IsError))
            {
                res.ResultMessage = true;
            }
            else if ((getDoc.DataResponse.Count() > 0) && (!getDoc.IsError))
            {
                try
                {
                    string lccode = this.MasterLicense.LicenseTypeCode;
                    var docFunc = getDoc.DataResponse.Where(doc => doc.LICENSE_TYPE_CODE.Equals(this.MasterLicense.LicenseTypeCode) && doc.DOCUMENT_REQUIRE.Equals("Y")).ToList();
                    foreach (BLL.AttachFilesIAS.AttachFile item in this.AttachFiles)
                    {
                        for (int i = 0; i < docFunc.Count; i++)
                        {
                            FilesLiceValidate ent = new FilesLiceValidate();
                            if ((item.ATTACH_FILE_TYPE.Equals(docFunc[i].DOCUMENT_CODE)) && (item.FILE_STATUS != "D"))
                            {
                                ent.Status = true;
                                ent.DocName = getDoc.DataResponse[i].DOCUMENT_NAME;
                                filesCheck.Add(ent);
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                }

                Func<List<string>, string> getDocName = (doclist) =>
                {
                    StringBuilder docname = new StringBuilder();
                    for (int j = 0; j < doclist.Count(); j++)
                    {
                        docname.Append(" : " + doclist[j].ToString());

                    }
                    doclist[0] = docname.ToString();
                    return doclist[0];
                };

                if (filesCheck.Count > 0)
                {
                    if (filesCheck.Where(chk => chk.Status == true).ToList().Count == getDoc.DataResponse.Count())
                    {
                        res.ResultMessage = true;
                    }
                    else
                    {
                        if (MasterLicense != null)
                        {
                            List<string> ls = getDoc.DataResponse.Select(doct => doct.DOCUMENT_NAME).ToList();
                            string docNAme = getDocName(ls);
                            this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.FileRequire + docNAme;
                            this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                            res.ErrorMsg = SysMessage.FileRequire + docNAme;
                            res.ResultMessage = false;
                        }


                    }
                }
                else if (filesCheck.Count == 0)
                {
                    if (MasterLicense != null)
                    {
                        List<string> ls = getDoc.DataResponse.Select(doct => doct.DOCUMENT_NAME).ToList();
                        string docNAme = getDocName(ls);
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowMessageError = SysMessage.FileRequire.ToString() + docNAme;
                        this.MasterLicense.UCLicenseUCLicenseModelError.ShowModalError();
                        res.ErrorMsg = SysMessage.FileRequire + docNAme;
                        res.ResultMessage = false;

                    }

                }
            }

            return res;

        }

        private void GetDocReqName()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            DataCenterBiz biz = new DataCenterBiz();
            IAS.DTO.ResponseService<IAS.DTO.ConfigDocument[]> getDoc = biz.GetDocRequire(Convert.ToString(DTO.DocFunction.LICENSE_FUNCTION.GetEnumValue()),
                Convert.ToString(this.MasterLicense.UserProfile.MemberType), this.MasterLicense.LicenseTypeCode, this.MasterLicense.PettionTypeCode);

            //Add Doc Req to lblDocReq
            if ((getDoc.DataResponse.Count() > 0) && (getDoc.DataResponse != null))
            {

                StringBuilder docName = new StringBuilder();
                if (getDoc.DataResponse.Count() == 1)
                {
                    docName.Append(getDoc.DataResponse[0].DOCUMENT_NAME + "&nbsp;" + "" + Environment.NewLine + "");
                }
                else
                {
                    for (int i = 0; i < getDoc.DataResponse.Count(); i++)
                    {
                        docName.Append((i + 1).ToString() + "." + getDoc.DataResponse[i].DOCUMENT_NAME + Environment.NewLine);
                    }
                }

                lblNote1.Visible = true;
                lblDocReq.Text = docName.ToString();

            }
            //กรณีไม่ต้องแนบเอกสาร 25/11/2556
            else
            {
                lblNote1.Visible = false;

            }

        }
        #endregion
    }

    //Only for Files validation
    //Add by Nattapong @24-9-2556
    public class FilesLiceValidate
    {
        public bool Status { get; set; }
        public string DocName { get; set; }
    }
}
