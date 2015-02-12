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
    public partial class ucAttachFileControl : System.Web.UI.UserControl
    {
        #region Control
        public DropDownList DropDownDocumentType { get { return ddlDocumentType; } set { ddlDocumentType = value; } }
        public TextBox TextRemark { get { return txtRemark; } set { txtRemark = value; } }
        public Button ButtonUpload { get { return btnUploadFile; } set { btnUploadFile = value; } }
        public GridView GridAttachFiles { get { return gvAttachFiles; } set { gvAttachFiles = value; } }
        public Panel PnlAttachFiles { get { return pnlAttachFiles; } set { pnlAttachFiles = value; } }
        public UpdatePanel UDPAttachFiles { get { return udpAttachFiles; } set { udpAttachFiles = value; } }

        #endregion Control

        #region Public Param & Session
        private string[] memberType = { Resources.propReg_NotApprove_MemberTypeGeneral, Resources.propReg_Co_MemberTypeCompany, Resources.propReg_Assoc_MemberTypeAssoc };
        private IList<AttachFile> _attachFiles;
        private IList<DTO.DataItem> _documentTypes;
        private IList<DTO.DataItem> _documentTypeDeleteds;
        private IList<DTO.DataItem> _documentTypeAll;
        private IList<DTO.ConfigDocument> _configDocuments = new List<DTO.ConfigDocument>();
        private String _registrationId = "";
        private String _remark = "";
        private String _currentUser = "";
        private DTO.DataActionMode _modeForm;
        public String RegisterationId { get { return _registrationId; } set { _registrationId = value; } }
        public DTO.DataActionMode ModeForm { get { return _modeForm; } set { _modeForm = value; } }
        public IEnumerable<DTO.ConfigDocument> ConfigDocuments { get { return _configDocuments; } }


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

        public string GetDocStatus
        {
            get
            {
                return (Session["GetDocStatus"] == null) ? "" : Session["GetDocStatus"].ToString();
            }
            set
            {
                Session["GetDocStatus"] = value;
            }
        }

        public Site1 MasterPage
        {
            get { return (this.Page.Master as Site1); }
        }

        public MasterRegister MasterRegis
        {
            get { return (this.Page.Master as MasterRegister); }
        }

        public MasterPerson MasterPerson
        {
            get { return (this.Page.Master as MasterPerson); }
        }

        public MasterLicense MasterLicense
        {
            get { return (this.Page.Master as MasterLicense); }
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.MasterRegis.ActionMode != null)
            //{
            //    if (!this.MasterRegis.ActionMode.Equals(IAS.DTO.DataActionMode.Add))
            //    {
            //        CheckSession();
            //    }
            //}
            //CheckSession();
            if (!Page.IsPostBack)
            {
                Session["AttachFiles"] = null;
                InitAttachFiles();
                BindAll();
                ViewState["RegisterationId"] = RegisterationId;
                ViewState["CurrentUser"] = CurrentUser;
                ViewState["ModeForm"] = ModeForm;

            }
            else
            {
                _attachFiles = (IList<AttachFile>)Session["AttachFiles"];
                _registrationId = ViewState["RegisterationId"].ToString();
                _currentUser = ViewState["CurrentUser"].ToString();

            }
            CheckSession();

            //Add New For EditApplicant
            if (Session["DocSession"] != null)
            {
                GetDocReqApplicantName();
            }
            else
            {
                GetDocReqName();
            }
            
            
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
                        if (_attachFiles[i].FILE_STATUS == AttachFileStatus.Wait.Value())
                        {
                            AttachFile attachFileActive = _attachFiles.SingleOrDefault(a => a.ATTACH_FILE_TYPE == _attachFiles[i].ATTACH_FILE_TYPE && a.FILE_STATUS == AttachFileStatus.Active.Value());
                            if (attachFileActive != null)
                            {
                                if (_attachFiles[i].FILE_STATUS != AttachFileStatus.Active.Value())
                                {
                                    DateTime dateNow = DateTime.Now;
                                    _attachFiles[i].FILE_STATUS = AttachFileStatus.Delete.Value();
                                    _attachFiles[i].ATTACH_FILE_TYPE = _attachFiles[i].ATTACH_FILE_TYPE;
                                    _attachFiles[i].ATTACH_FILE_PATH = _attachFiles[i].ATTACH_FILE_PATH;
                                    _attachFiles[i].LICENSE_NO = _attachFiles[i].LICENSE_NO;
                                    _attachFiles[i].REMARK = _attachFiles[i].REMARK;
                                    _attachFiles[i].ATTACH_FILE_TYPE = _attachFiles[i].ATTACH_FILE_TYPE;
                                    _attachFiles[i].ID_CARD_NO = _attachFiles[i].ID_CARD_NO;
                                    _attachFiles[i].ID = _attachFiles[i].ID;
                                    _attachFiles[i].UPDATED_DATE = dateNow;
                                }

                            }
                            else
                            {
                                _attachFiles.RemoveAt(i);
                            }
                        }
                        else
                        {
                            _attachFiles.RemoveAt(i);
                        }

                        Session["AttachFiles"] = _attachFiles;
                        BindAttachFile();
                        udpAttachFiles.Update();
                        return;
                    }

                }

                //BindAttachFile();
                //udpAttachFiles.Update();
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

        private Boolean FilterContentType(String contentType)
        {
            String typeXls = Utils.ContentTypeHelper.MimeType(".xls");

            DataCenterBiz biz = new DataCenterBiz();
            var res = biz.GetDocumentTypeAll("");

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

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (!fulAttachFile.HasFile)
            {
                AlertMessageShow(SysMessage.CannotUploadFile);
                return;
            }

          
            //check file ที่สามารถ upload ได้
            if (!FilterContentType(fulAttachFile.PostedFile.ContentType))
            {
                AlertMessageShow(String.Format("ไม่สามารถนำเข้าไฟล์ ประเภท{0} ได้.", fulAttachFile.FileName.Substring(fulAttachFile.FileName.LastIndexOf('.'))));
                return;
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

                        AlertMessageShow(SysMessage.AttachFileDupstart + ddlDocumentType.SelectedItem.Text + SysMessage.AttachFileDupEnt);
                        return;
                    }
                    else
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

                        _attachFiles.Add(attachFile);
                        DefaultValue();
                        BindAttachFile();
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
                    DTO.DataItem item = DocumentTypeAll.FirstOrDefault(a => a.Id == attachFile.ATTACH_FILE_TYPE);
                    if (item == null)
                    {
                        item = new DTO.DataItem() { Id = "--", Name = "ไม่พบประเภทเอกสาร" };
                    }
                    lblDocumentName.Text = item.Name; // ddlDocumentType.Items.FindByValue(attachFile.ATTACH_FILE_TYPE).Text;

                    //if (hplView != null) hplView.OnClientClick = LinkPopUp(attachFile.ATTACH_FILE_PATH);
                    String mode = "";
                    if (Request.QueryString["Mode"] != null)
                    {
                        mode = Request.QueryString["Mode"].ToString();
                    }

                    if (mode == "V" || mode == "T")
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


                    //โค้ดเดิม
                    //  if (hplDelete != null) hplDelete.Visible = true;
                    //if (hplCancel != null) hplCancel.Visible = false;

                    //เริ่ม by nami คับ
                    DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
                    if (userProfile != null)
                    {
                        if (userProfile.MemberType == 1 || userProfile.MemberType == 2 || userProfile.MemberType == 3)
                        {
                            PersonBiz biz = new PersonBiz();
                            //DTO.ResponseService<DTO.Person> res = biz.GetUserProfile(userProfile.Id, userProfile.MemberType.ToString());

                            if (userProfile.STATUS == "1" || userProfile.STATUS == "4")
                            {
                                if (hplDelete != null) hplDelete.Visible = false;
                                if (hplCancel != null) hplCancel.Visible = false;
                                if (lbnEditGv != null) lbnEditGv.Visible = false;
                            }
                            else
                            {
                                if (hplDelete != null) hplDelete.Visible = true;
                                if (hplCancel != null) hplCancel.Visible = true;
                                if (hplCancel != null) hplCancel.Visible = false;
                            }
                        }
                        else
                        {
                            if (hplDelete != null) hplDelete.Visible = false;
                            if (hplCancel != null) hplCancel.Visible = false;
                            if (lbnEditGv != null) lbnEditGv.Visible = false;
                        }
                    }
                    else
                    {
                        if (hplDelete != null) hplDelete.Visible = true;
                        if (hplCancel != null) hplCancel.Visible = false;
                    }

                }
                //สิิ้นสุด


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

                    // add by nami
                    DTO.UserProfile userProfile = (DTO.UserProfile)Session[PageList.UserProfile];
                    if (userProfile != null)
                    {
                        if (userProfile.STATUS == "4" || userProfile.STATUS == "1" || userProfile.STATUS == null)
                        {
                            if (hplCancel != null) hplCancel.Visible = false;
                        }
                        else
                        {
                            if (hplCancel != null) hplCancel.Visible = true;
                        }
                    }
                    //if (hplCancel != null) hplCancel.Visible = true;
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

        public IList<DTO.DataItem> DocumentTypeDeleteds
        {
            get { return _documentTypeDeleteds; }
            set { _documentTypeDeleteds = value; }
        }

        public IList<DTO.DataItem> DocumentTypeAll
        {
            get { return _documentTypeAll; }
            set { _documentTypeAll = value; }
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

        public void EnablehpCancle(Boolean IsEnabled)
        {
            if (gvAttachFiles.Rows.Count != 0)
            {
                for (int i = 0; i < gvAttachFiles.Rows.Count; i++)
                {
                    LinkButton hplCancel = (LinkButton)gvAttachFiles.Rows[i].FindControl("hplCancel");
                    if (hplCancel != null)
                        hplCancel.Visible = IsEnabled;
                }

            }
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
            if (_documentTypeAll == null)
            {
                using (DataCenterBiz dataBiz = new DataCenterBiz())
                {
                    _documentTypeAll = dataBiz.GetDocumentTypeAll("");
                }
            }
            String mode = "";
            if (Request.QueryString["Mode"] != null)
            {
                mode = Request.QueryString["Mode"].ToString();
            }
            if (mode == "E")
                gvAttachFiles.DataSource = (_attachFiles.OrderBy(l => l.ATTACH_FILE_TYPE)).ToList();
            else if (mode == "V")
                gvAttachFiles.DataSource = (this.ActiveAttachFiles.OrderBy(l => l.ATTACH_FILE_TYPE)).ToList();
            else if (mode == "T")
                gvAttachFiles.DataSource = (_attachFiles.OrderBy(l => l.ATTACH_FILE_TYPE)).ToList();
            else
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
            DateTime curDate = DateTime.Now;
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
                FILE_STATUS = AttachFileStatus.Wait.Value()
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

        //Last update 11/10/2556 by Nattapong
        //public DTO.ResponseMessage<bool> DocRequire()
        //{
        //    var res = new DTO.ResponseMessage<bool>();
        //    DataCenterBiz biz = new DataCenterBiz();
        //    List<FilesValidate> filesCheck = new List<FilesValidate>();
        //    //TextBox txtMemberType = (TextBox)this.MasterRegis.ContentDetails.FindControl("txtMemberType");
        //    TextBox txtMemberType = new TextBox();
        //    if (MasterRegis != null)
        //    {
        //        TextBox txtMemType = (TextBox)this.MasterRegis.ContentDetails.FindControl("txtMemberType");
        //        if (txtMemType != null)
        //        {
        //            txtMemberType = txtMemType;
        //        }
        //    }
        //    else if (MasterPerson != null)
        //    {
        //        TextBox txtMemType = (TextBox)this.MasterPerson.ContentDetails.FindControl("txtMemberType");
        //        if (txtMemType != null)
        //        {
        //            txtMemberType = txtMemType;
        //        }
        //    }
        //    //IAS.DTO.ResponseService<IAS.DTO.ConfigDocument[]> getDoc = biz.GetDocumentConfigApproveMember();

        //    //MemberType Validation and then Convert to String
        //    Func<string, string> MemTypeValidate = (memT) =>
        //    {
        //        if ((memT != null) || (memT != ""))
        //        {
        //            if(memT.Equals(memberType[0]))
        //            {
        //                memT = Convert.ToString(DTO.MemberType.General.GetEnumValue());
        //            }
        //            else if(memT.Equals(memberType[1]))
        //            {
        //                memT = Convert.ToString(DTO.MemberType.Insurance.GetEnumValue());
        //            }
        //            else if (memT.Equals(memberType[2]))
        //            {
        //                memT = Convert.ToString(DTO.MemberType.Association.GetEnumValue());
        //            }
        //        }
        //        return memT;
        //    };


        //    //Add Doclist
        //    Func<List<string>, string> getDocName = (doclist) =>
        //    {
        //        StringBuilder docname = new StringBuilder();
        //        List<string> defaultStr = new List<string>();

        //        if (doclist.Count > 0)
        //        {
        //            for (int j = 0; j < doclist.Count(); j++)
        //            {
        //                docname.Append(" : " + doclist[j].ToString());

        //            }
        //            doclist[0] = docname.ToString();
        //        }
        //        else
        //        {
        //            string strNull = "";
        //            defaultStr.Add(strNull);
        //            doclist = defaultStr;

        //        }

        //        return doclist[0];
        //    };



        //    //Get Doc Require
        //    DTO.ResponseService<DTO.ConfigDocument[]> docres = biz.GetDocRequire(Convert.ToString(DTO.DocFunction.REGISTER_FUNCTION.GetEnumValue()), MemTypeValidate(txtMemberType.Text), "", "");

        //    //Case DocReq = 0 => Not need to Attach
        //    if (docres.DataResponse.Count().Equals(0))
        //    {
        //        res.ResultMessage = true;
        //    }
        //    //Case DocReq > 0 > Doc need to Attach
        //    else
        //    {
        //        try
        //        {
        //            var docReq = docres.DataResponse.Where(doc => doc.MEMBER_CODE.Equals(MemTypeValidate(txtMemberType.Text)) && doc.DOCUMENT_REQUIRE.Equals("Y")).ToList();
        //            if (docReq.Count != 0)
        //            {
        //                for (int i = 0; i < docReq.Count; i++)
        //                {
        //                    foreach (BLL.AttachFilesIAS.AttachFile item in this.AttachFiles)
        //                    {
        //                        FilesValidate ent = new FilesValidate();
        //                        if (item.ATTACH_FILE_TYPE.Equals(docReq[i].DOCUMENT_CODE) && (item.FILE_STATUS != "D"))
        //                        {
        //                            ent.Status = true;
        //                            ent.DocName = docReq[i].DOCUMENT_NAME;
        //                            filesCheck.Add(ent);
        //                        }

        //                    }

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //        }



        //        if (filesCheck.Count > 0)
        //        {
        //            if (filesCheck.Where(chk => chk.Status == true).ToList().Count == docres.DataResponse.Count())
        //            {
        //                res.ResultMessage = true;
        //            }
        //            else
        //            {
        //                if (MasterRegis != null)
        //                {
        //                    List<string> ls = docres.DataResponse.Select(doct => doct.DOCUMENT_NAME).ToList();
        //                    string docNAme = getDocName(ls);
        //                    this.MasterRegis.ModelError.ShowMessageError = SysMessage.FileRequire.ToString() + docNAme;
        //                    this.MasterRegis.ModelError.ShowModalError();
        //                    res.ErrorMsg = docNAme;
        //                    res.ResultMessage = false;
        //                }
        //                else if (MasterPerson != null)
        //                {
        //                    List<string> ls = docres.DataResponse.Select(doct => doct.DOCUMENT_NAME).ToList();
        //                    string docNAme = getDocName(ls);
        //                    this.MasterPerson.ModelError.ShowMessageError = SysMessage.FileRequire.ToString() + docNAme;
        //                    this.MasterPerson.ModelError.ShowModalError();
        //                    res.ErrorMsg = docNAme;
        //                    res.ResultMessage = false;
        //                }


        //            }
        //        }
        //        else if (filesCheck.Count == 0)
        //        {
        //            if (MasterRegis != null)
        //            {
        //                List<string> ls = docres.DataResponse.Select(doct => doct.DOCUMENT_NAME).ToList();
        //                string docNAme = getDocName(ls);
        //                this.MasterRegis.ModelError.ShowMessageError = SysMessage.FileRequire.ToString() + docNAme;
        //                this.MasterRegis.ModelError.ShowModalError();
        //                res.ErrorMsg = docNAme;
        //                res.ResultMessage = false;
        //            }
        //            else if (MasterPerson != null)
        //            {
        //                List<string> ls = docres.DataResponse.Select(doct => doct.DOCUMENT_NAME).ToList();
        //                string docNAme = getDocName(ls);
        //                this.MasterPerson.ModelError.ShowMessageError = SysMessage.FileRequire.ToString() + docNAme;
        //                this.MasterPerson.ModelError.ShowModalError();
        //                res.ErrorMsg = docNAme;
        //                res.ResultMessage = false;

        //            }

        //        }

        //    }

        //    return res;

        //}

        public String ValidDocRequire()
        {
            StringBuilder errMessage = new StringBuilder("");
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            foreach (DTO.ConfigDocument item in ConfigDocuments)
            {
                IEnumerable<BLL.AttachFilesIAS.AttachFile> tempdoc = TempAttachFiles.Where(a => a.ATTACH_FILE_TYPE == item.DOCUMENT_CODE);
                IEnumerable<BLL.AttachFilesIAS.AttachFile> activeDoc = AttachFiles.Where(a => a.ATTACH_FILE_TYPE == item.DOCUMENT_CODE);
                bool IsNotAlert = true;
                if (activeDoc == null)
                {
                    errMessage.AppendLine(String.Format("กรุณาเพิ่มเอกสาร {0}.<br />", item.DOCUMENT_NAME));
                    IsNotAlert = false;
                }
                else if (activeDoc.Count() <= 0)
                {
                    errMessage.AppendLine(String.Format("กรุณาเพิ่มเอกสาร {0}.<br />", item.DOCUMENT_NAME));
                    IsNotAlert = false;
                }
                else if (activeDoc.Count(a => a.FILE_STATUS == "D") > 0)
                {
                    errMessage.AppendLine(String.Format("กรุณาเพิ่มเอกสาร {0}.<br />", item.DOCUMENT_NAME));
                    IsNotAlert = false;
                }


            }

            return errMessage.ToString();

        }

        public DTO.ResponseMessage<bool> DocRequire(DTO.Person regis)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();

            return res;

        }

        private void GetDocReqName()
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            DataCenterBiz biz = new DataCenterBiz();

            TextBox txtMemberType = new TextBox();
            if (MasterRegis != null)
            {
                TextBox txtMemType = (TextBox)this.MasterRegis.ContentDetails.FindControl("txtMemberType");
                if (txtMemType != null)
                {
                    txtMemberType = txtMemType;
                }
            }
            else if (MasterPerson != null)
            {
                TextBox txtMemType = (TextBox)this.MasterPerson.ContentDetails.FindControl("txtMemberType");
                if (txtMemType != null)
                {
                    txtMemberType = txtMemType;
                }
            }
            else if (MasterPage != null)
            {
                TextBox txtMemType = (TextBox)this.MasterPage.ContentDetails.FindControl("txtTypeMemberBeforeReg");
                if (txtMemType != null)
                {
                    txtMemberType = txtMemType;
                }
            }
            //IAS.DTO.ResponseService<IAS.DTO.ConfigDocument[]> getDoc = biz.GetDocumentConfigApproveMember();

            //MemberType Validation and then Convert to String
            Func<string, string> MemTypeValidate = (memT) =>
            {
                if ((memT != null) || (memT != ""))
                {
                    if (memT.Equals(memberType[0]))
                    {
                        memT = Convert.ToString(DTO.MemberType.General.GetEnumValue());
                    }
                    else if (memT.Equals(memberType[1]))
                    {
                        memT = Convert.ToString(DTO.MemberType.Insurance.GetEnumValue());
                    }
                    else if (memT.Equals(memberType[2]))
                    {
                        memT = Convert.ToString(DTO.MemberType.Association.GetEnumValue());
                    }
                }
                return memT;
            };

            DTO.ResponseService<DTO.ConfigDocument[]> getDoc = biz.GetDocRequire(Convert.ToString(DTO.DocFunction.REGISTER_FUNCTION.GetEnumValue()), MemTypeValidate(txtMemberType.Text), "", "");
            //Add Doc Req to lblDocReq
            if ((getDoc.DataResponse.Count() > 0) && (getDoc.DataResponse != null))
            {
                StringBuilder docName = new StringBuilder();
                StringBuilder docnewLine = new StringBuilder();

                if (getDoc.DataResponse.Count() == 1)
                {
                    docName.Append(getDoc.DataResponse[0].DOCUMENT_NAME + "&nbsp;" + "" + Environment.NewLine + "");
                }
                else
                {
                    for (int i = 0; i < getDoc.DataResponse.Count(); i++)
                    {
                        Label lbl = new Label();
                        docName.Append((i + 1).ToString() + "." + getDoc.DataResponse[i].DOCUMENT_NAME + "&nbsp;" + "" + Environment.NewLine + "");
                    }
                }
                lblDocReq.Text = docnewLine.ToString() + Environment.NewLine + docName.ToString();
                lblNote1.Visible = true;

                _configDocuments = getDoc.DataResponse;
            }
            else
            {
                _configDocuments.Clear();
                lblNote1.Visible = false;

            }
            

        }

        public void GetDocReqApplicantName()
        {
            if (Session["ApplicantView"] != "AppView")
            {
                DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
                DataCenterBiz biz = new DataCenterBiz();
                string memtype = Convert.ToString(DTO.MemberType.Insurance.GetEnumValue());

                DTO.ResponseService<DTO.ConfigDocument[]> getDoc = biz.GetDocRequire(Convert.ToString(DTO.DocFunction.APPLICANT_FUNCTION.GetEnumValue()), memtype, "", "");
                //Add Doc Req to lblDocReq
                if ((getDoc.DataResponse.Count() > 0) && (getDoc.DataResponse != null))
                {
                    StringBuilder docName = new StringBuilder();
                    StringBuilder docnewLine = new StringBuilder();

                    if (getDoc.DataResponse.Count() == 1)
                    {
                        docName.Append(getDoc.DataResponse[0].DOCUMENT_NAME + "&nbsp;" + "" + Environment.NewLine + "");
                    }
                    else
                    {
                        for (int i = 0; i < getDoc.DataResponse.Count(); i++)
                        {
                            Label lbl = new Label();
                            docName.Append((i + 1).ToString() + "." + getDoc.DataResponse[i].DOCUMENT_NAME + "&nbsp;" + "" + Environment.NewLine + "");
                        }
                    }
                    lblDocReq.Text = docnewLine.ToString() + Environment.NewLine + docName.ToString();
                    lblNote1.Visible = true;
                    lblDocReq.Visible = true;
                    _configDocuments = getDoc.DataResponse;
                }
                else
                {
                    _configDocuments.Clear();
                    lblNote1.Visible = false;

                }
            }
            else
            {
                //_configDocuments.Clear();
                lblDocReq.Visible = false;
                lblNote1.Visible = false;

            }

        }

        public void CheckSession()
        {
            if (this.MasterRegis != null)
            {
                if (this.MasterRegis.PageTimeout == "")
                {
                    Session["SessionLost"] = true;
                    Response.Redirect(PageList.Home);
                }
            }

        }
        #endregion

    }

    //Only for Files validation
    //Add by Nattapong @24-9-2556
    public class FilesValidate
    {
        public bool Status { get; set; }
        public string DocName { get; set; }
    }
}
