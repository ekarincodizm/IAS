using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.DTO;

using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;
using AjaxControlToolkit;
using System.Web.Configuration;
using System.Data;
using IAS.Properties;

namespace IAS.License
{
    public partial class LicenseSingle : basepage
    {
        public string TempFolderOracle
        {
            get
            {
                return (string)Session["TempFolderOracle"];
            }
        }



        public List<DTO.PersonAttatchFile> PersonAttachFiles
        {
            get
            {
                if (Session["PersonAttachFiles"] == null)
                {
                    Session["PersonAttachFiles"] = new List<DTO.PersonAttatchFile>();
                }

                return (List<DTO.PersonAttatchFile>)Session["PersonAttachFiles"];
            }
            set
            {
                Session["PersonAttachFiles"] = value;
            }
        }

        public List<DTO.AttatchFileLicense> AttachFiles
        {
            get
            {
                if (Session["AttatchFiles"] == null)
                {
                    Session["AttatchFiles"] = new List<DTO.AttatchFileLicense>();
                }

                return (List<DTO.AttatchFileLicense>)Session["AttatchFiles"];
            }

            set
            {
                Session["AttatchFiles"] = value;
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

        public string UserID
        {
            get
            {
                return (string)Session["UserID"];
            }

        }

        DTO.DataActionMode _DataAction;
        public DTO.DataActionMode DataAction
        {
            get
            {
                _DataAction = Session["UserProfile"] == null ? DTO.DataActionMode.Add : DTO.DataActionMode.Edit;

                return _DataAction;
            }
            set
            {
                _DataAction = value;
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

        string mapPath;

        protected void Page_Load(object sender, EventArgs e)
        {
            mapPath = WebConfigurationManager.AppSettings["UploadRecieveLicense"];

            if (!IsPostBack)
            {
                Session["UserID"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                Session["TempFolderOracle"] = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                DefaultData();//milk ไม่ได้แก้โค้ดแค่ยกออกไปไว้ข้างนอก
            }
        }

        protected void DefaultData()
        {
            GetRequestLicenseType();
            GetAttachFilesType();
            GetLicenseType();
            GetCompany();
            GetAttatchFiles();
            GetAttachFilesTypeImage();


            BindPersonByIDCard();


            if (ddlRequestLicenseType.SelectedIndex <= 0)
            {
                UnEnabledUpload();
            }
            else
            {
                EnabledUpload();
            }
        }
        private Action<DropDownList, DTO.DataItem[]> BindToDDLArr = (ddl, ls) =>
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

        private void GetAttachFilesTypeImage()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            Session["DocumentTypeIsImage"] = biz.GetDocumentTypeIsImage();
        }

        private void GetCompany()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyCodeByName("");
            ddlCompany.DataSource = ls;
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, "0"));
        }

        private void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.UserProfile.Id;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetUserProfileAttatchFileByPersonId(personID);

            var list = res.DataResponse.ToList();


            if (this.DataAction == DTO.DataActionMode.Edit)
            {
                this.PersonAttachFiles = list;
            }

            gvUpload.DataSource = list;
            gvUpload.DataBind();

            UpdatePanelUpload.Update();
        }

        private void GetLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetLicenseType(SysMessage.DefaultSelecting);
            BindToDDLArr(ddlLicenseType, ls.DataResponse);
        }

        private void GetRequestLicenseType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetRequestLicenseType(SysMessage.DefaultSelecting);
            BindToDDLArr(ddlRequestLicenseType, ls.DataResponse);
        }

        private void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            BindToDDL(ddlTypeAttachment, ls);
        }

        private void ProfileReadOnlyMode()
        {
            //ประวัติส่วนตัว
            txtBirthDay.ReadOnly = true;
            txtNationality.ReadOnly = true;
            txtEducation.ReadOnly = true; ;
            txtBirthDay.ReadOnly = true;
            txtCurrentAddress.ReadOnly = true;
            txtPostcodeCurrentAddress.ReadOnly = true;
            txtProvinceCurrentAddress.ReadOnly = true;
            txtDistrictCurrentAddress.ReadOnly = true;
            txtParishCurrentAddress.Enabled = true;
            txtRegisterAddress.ReadOnly = true;
            txtPostcodeRegisterAddress.ReadOnly = true;
            txtProvinceRegisterAddress.ReadOnly = true;
            txtDistrictRegisterAddress.Enabled = true;
            txtParishRegisterAddress.Enabled = true;
        }

        private void BindPersonByIDCard()
        {
            var biz = new BLL.LicenseBiz();
            var res = biz.GetPersonalHistoryByIdCard(base.UserProfile.IdCard);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                var person = res.DataResponse;
                if (res.DataResponse != null)
                {
                    if (!string.IsNullOrEmpty(person.FLNAME))
                    {
                        txtFirstNameLastName.Text = person.FLNAME;
                    }
                    if (!string.IsNullOrEmpty(person.ID_CARD_NO))
                    {
                        txtIdCard.Text = person.ID_CARD_NO;
                    }
                    if (!string.IsNullOrEmpty(person.SEX))
                    {
                        if (person.SEX != "M")
                        {
                            txtSex.Text = Resources.propLicenseGroup_Female;
                        }
                        else
                        {
                            txtSex.Text = Resources.propLicenseGroup_Male;
                        }
                    }
                    if (!string.IsNullOrEmpty(person.NATIONALITY))
                    {
                        txtNationality.Text = person.NATIONALITY;
                    }
                    if (!string.IsNullOrEmpty(person.EDUCATION_NAME))
                    {
                        txtEducation.Text = person.EDUCATION_NAME;
                    }
                    if (person.BIRTH_DATE.ToString() != null || person.BIRTH_DATE.ToString() != "")
                    {
                        txtBirthDay.Text = person.BIRTH_DATE.ToString("dd/MM/yyyy");
                    }
                    if (!string.IsNullOrEmpty(person.ADDRESS1))
                    {
                        txtCurrentAddress.Text = person.ADDRESS1;
                    }
                    if (!string.IsNullOrEmpty(person.PROVINCE))
                    {
                        txtProvinceCurrentAddress.Text = person.PROVINCE;
                    }
                    if (!string.IsNullOrEmpty(person.AMPUR))
                    {
                        txtDistrictCurrentAddress.Text = person.AMPUR;
                    }
                    if (!string.IsNullOrEmpty(person.TAMBON))
                    {
                        txtParishCurrentAddress.Text = person.TAMBON;
                    }
                    if (!string.IsNullOrEmpty(person.ZIPCODE))
                    {
                        txtPostcodeCurrentAddress.Text = person.ZIPCODE;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_ADDRESS1))
                    {
                        txtRegisterAddress.Text = person.LOCAL_ADDRESS1;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_PROVINCE))
                    {
                        txtProvinceRegisterAddress.Text = person.LOCAL_PROVINCE;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_AMPUR))
                    {
                        txtDistrictRegisterAddress.Text = person.LOCAL_AMPUR;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_TAMBON))
                    {
                        txtParishRegisterAddress.Text = person.LOCAL_TAMBON;
                    }
                    if (!string.IsNullOrEmpty(person.LOCAL_ZIPCODE))
                    {
                        txtPostcodeRegisterAddress.Text = person.LOCAL_ZIPCODE;
                    }
                    EnableControl();
                    UpdatePanelSearch.Update();
                }
            }
        }

        protected void TabDetail_ActiveTabChanged(object sender, EventArgs e)
        {
            var biz = new BLL.LicenseBiz();
            var IDCard = this.IdCardNo;
            if (TabDetail.ActiveTabIndex == 0)
            {
                BindPersonByIDCard();
            }
            else if (TabDetail.ActiveTabIndex == 1)
            {

                var res = biz.GetExamHistoryByIdCard(base.UserProfile.IdCard);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvHistoryExam.DataSource = res.DataResponse;
                    gvHistoryExam.DataBind();

                    UpdatePanelSearch.Update();
                }
            }
            else if (TabDetail.ActiveTabIndex == 2)
            {
                var res = biz.GetTrainingHistoryBy(base.UserProfile.IdCard);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvHistoryTraining.DataSource = res.DataResponse;
                    gvHistoryTraining.DataBind();

                    UpdatePanelSearch.Update();
                }
            }
            else if (TabDetail.ActiveTabIndex == 3)
            {
                var res = biz.GetTrain_1_To_4_ByIdCard(base.UserProfile.IdCard);

                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    GvTraining.DataSource = res.DataResponse;
                    GvTraining.DataBind();

                    UpdatePanelSearch.Update();
                }
            }
            else if (TabDetail.ActiveTabIndex == 4)
            {
                var res = biz.GetUnitLinkByIdCard(base.UserProfile.IdCard);

                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvUnitLink.DataSource = res.DataResponse;
                    gvUnitLink.DataBind();

                    UpdatePanelSearch.Update();
                }
            }
            else if (TabDetail.ActiveTabIndex == 5)
            {
                var res = biz.GetRequestLicenseByIdCard(base.UserProfile.IdCard);
                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else
                {
                    gvObtainLicense.DataSource = res.DataResponse;
                    gvObtainLicense.DataBind();

                    UpdatePanelSearch.Update();
                }
            }
        }

        private void EnableControl()
        {
            txtFirstNameLastName.Enabled = false;
            txtIdCard.Enabled = false;
            txtSex.Enabled = false;
            txtNationality.Enabled = false;
            txtEducation.Enabled = false;
            txtBirthDay.Enabled = false;
            txtCurrentAddress.Enabled = false;
            txtProvinceCurrentAddress.Enabled = false;
            txtDistrictCurrentAddress.Enabled = false;
            txtParishCurrentAddress.Enabled = false;
            txtPostcodeCurrentAddress.Enabled = false;
            txtRegisterAddress.Enabled = false;
            txtProvinceRegisterAddress.Enabled = false;
            txtDistrictRegisterAddress.Enabled = false;
            txtParishRegisterAddress.Enabled = false;
            txtPostcodeRegisterAddress.Enabled = false;
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var text = (Label)gr.FindControl("lblFileGv");

            Session["ViewFileName"] = text.Text;

            string OpenWindow = "window.open('licenseViewDocument.aspx','','')";
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", OpenWindow, true);


        }

        protected void gvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label error = ((Label)e.Row.FindControl("lblDetailGv"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplview"));

                if (error.Text != "")
                {
                    view.Visible = true;
                }
                else
                {
                    view.Visible = false;
                }
            }
        }

        protected void hplDelete_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            Label lblFileGv = (Label)gr.FindControl("lblFileGv");

            var list = this.AttachFiles;

            var _item = list.Find(l => l.ATTACH_FILE_PATH == lblFileGv.Text);

            list.Remove(_item);

            var source = Server.MapPath(mapPath + "/" + lblFileGv.Text);

            FileInfo fiPath = new FileInfo(source);
            if (fiPath.Exists)
            {
                File.Delete(source);
            }

            gvUpload.DataSource = list;
            gvUpload.DataBind();


            ddlTypeAttachment.SelectedIndex = 0;
            txtDetail.Text = string.Empty;

            UpdatePanelUpload.Update();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ReceiveLicenseHeader rech = new ReceiveLicenseHeader();
            var arrCom = ddlCompany.SelectedValue.Split('[', ']');
            rech.IMPORT_DATETIME = DateTime.Now;
            rech.PETTITION_TYPE = ddlRequestLicenseType.SelectedValue;
            rech.LICENSE_TYPE_CODE = rech.LICENSE_TYPE = ddlLicenseType.SelectedValue;
            rech.COMP_CODE = arrCom[1];
            rech.COMP_NAME = arrCom[0];
            rech.SEND_DATE = DateTime.Today;
            rech.TOTAL_AGENT = 1;
            rech.TOTAL_FEE = txtFee.Text.ToDecimal();

            ReceiveLicenseDetail recd = new ReceiveLicenseDetail();

            recd.IMPORT_ID = rech.IMPORT_ID;
            recd.PETITION_TYPE = rech.PETTITION_TYPE;
            recd.COMP_CODE = rech.COMP_CODE;
            recd.SEQ = "0001";
            recd.LICENSE_FEE = txtFee.Text.ToDecimal();
            recd.LICENSE_NO = txtLicenseNumber.Text;
            recd.CITIZEN_ID = base.IdCard;
            recd.TITLE_NAME = base.UserProfile.TitleName;
            recd.NAME = base.UserProfile.Name;
            recd.SURNAME = base.UserProfile.LastName;

            AttatchFileLicense att = new AttatchFileLicense();
            UserProfile a = new DTO.UserProfile();
            BLL.LicenseBiz biz = new BLL.LicenseBiz();
            var res = biz.InsertSingleReceiveLicense(rech, recd, base.UserProfile);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                UCModalSuccess.ShowMessageSuccess = SysMessage.SuccessInsertLicenseSingle;
                UCModalSuccess.ShowModalSuccess();
            }
        }

        protected void ddlRequestLicenseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetPetitionFee(ddlRequestLicenseType.SelectedValue);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                txtFee.Text = res.ResultMessage.ToString();
            }
            if (ddlRequestLicenseType.SelectedIndex <= 0)
            {
                UnEnabledUpload();
            }
            else
            {
                EnabledUpload();
            }
        }

        protected void gvUpload_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUpload.Rows[e.RowIndex];

            TextBox txtDetailGv = (TextBox)row.FindControl("txtDetailGv");

            string _ID = gvUpload.DataKeys[e.RowIndex].Value.ToString();


            var _item = this.AttachFiles.Find(l => l.ID_CARD_NO == _ID);

            if (_item != null)
            {
                _item.REMARK = txtDetailGv.Text;
            }


            gvUpload.EditIndex = -1;

            ((DataControlField)gvUpload.Columns
            .Cast<DataControlField>()
            .Where(fld => fld.HeaderText == "ดำเนินการ")
            .SingleOrDefault()).Visible = true;

            BindDataInGridView();

            UpdatePanelUpload.Update();
        }

        protected void gvUpload_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUpload.EditIndex = e.NewEditIndex;
            BindDataInGridView();

            UpdatePanelUpload.Update();
        }

        protected void gvUpload_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            e.Cancel = true;
            gvUpload.EditIndex = -1;

            ((DataControlField)gvUpload.Columns
            .Cast<DataControlField>()
            .Where(fld => fld.HeaderText == "ดำเนินการ")
            .SingleOrDefault()).Visible = true;

            BindDataInGridView();

            UpdatePanelUpload.Update();
        }

        protected void gvUpload_PreRender(object sender, EventArgs e)
        {
            if (this.gvUpload.EditIndex != -1)
            {

                LinkButton hplView = gvUpload.Rows[gvUpload.EditIndex].FindControl("hplView") as LinkButton;
                LinkButton hplDelete = gvUpload.Rows[gvUpload.EditIndex].FindControl("hplDelete") as LinkButton;

                ((DataControlField)gvUpload.Columns
                .Cast<DataControlField>()
                .Where(fld => fld.HeaderText == "ดำเนินการ")
                .SingleOrDefault()).Visible = false;

            }
        }

        protected void gvUpload_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        private void UploadFileImage(string fileName)
        {

            var list = this.AttachFiles;

            string userID = this.UserID;
            string tempFilePath = this.TempFolderOracle;

            string[] tempFileName = fileName.Split('_');

            string masterFileName = tempFileName[1] + "_" + tempFileName[2];

            list.Add(new AttatchFileLicense
            {
                ID_ATTACH_FILE = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                ID_CARD_NO = base.IdCard,
                ATTACH_FILE_TYPE = ddlTypeAttachment.SelectedValue,
                ATTACH_FILE_PATH = masterFileName,
                REMARK = txtDetail.Text,
                CREATED_BY = userID,
                CREATED_DATE = DateTime.Now,
                UPDATED_BY = userID,
                UPDATED_DATE = DateTime.Now,
                //FILE_STATUS : String
                //LICENSE_NO : String
                //RENEW_TIME : String
            });


            this.AttachFiles = list;
            BindDataInGridView();

            UpdatePanelUpload.Update();



        }

        private void BindDataInGridView()
        {

            gvUpload.DataSource = this.AttachFiles;
            gvUpload.DataBind();

            UpdatePanelUpload.Update();


        }

        private void UploadFile(string fileName)
        {
            var list = this.AttachFiles;

            string userID = this.UserID;
            string tempFilePath = this.TempFolderOracle;

            string[] tempFileName = fileName.Split('_');

            string masterFileName = tempFileName[1] + "_" + tempFileName[2] + "_" + tempFileName[3];

            list.Add(new AttatchFileLicense
            {
                ID_ATTACH_FILE = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                ID_CARD_NO = base.IdCard,
                ATTACH_FILE_TYPE = ddlTypeAttachment.SelectedValue,
                ATTACH_FILE_PATH = masterFileName,
                REMARK = txtDetail.Text,
                CREATED_BY = userID,
                CREATED_DATE = DateTime.Now,
                UPDATED_BY = userID,
                UPDATED_DATE = DateTime.Now,
                //FILE_STATUS : String
                //LICENSE_NO : String
                //RENEW_TIME : String
            });


            this.AttachFiles = list;
            BindDataInGridView();


            UpdatePanelUpload.Update();

        }

        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            string tempFileName = Path.GetFileName(fulFile.PostedFile.FileName);

            bool invalid = validateFileType(tempFileName);

            if (ddlRequestLicenseType.SelectedIndex != 0)
            {
                if (ddlTypeAttachment.SelectedIndex != 0)
                {
                    if (tempFileName != string.Empty)
                    {
                        FileInfo f = new FileInfo(tempFileName);
                        string surNameFile = f.Extension;

                        string fileName = string.Empty;

                        var TypeFile = AttachFiles.Where(w => w.ATTACH_FILE_TYPE == ddlTypeAttachment.SelectedValue).FirstOrDefault();


                        string yearMonthDay = DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                        string hourMinSec = DateTime.Now.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        DirectoryInfo DirInfo = new DirectoryInfo(Server.MapPath(mapPath) + this.TempFolderOracle);

                        if (invalid)
                        {
                            if (UserProfile.IdCard != string.Empty)
                            {
                                var attFileImage = this.GetDocumentTypeIsImage.Where(w => w.Id == ddlTypeAttachment.SelectedValue).FirstOrDefault();

                                if (attFileImage != null)
                                {
                                    if (TypeFile == null)
                                    {
                                        tempFileName = "_" + UserProfile.IdCard + "_" + ddlRequestLicenseType.SelectedValue + surNameFile;

                                        UploadFileImage(tempFileName);

                                        string[] _fileName = tempFileName.Split('_');

                                        string masterFileName = _fileName[1] + "_" + _fileName[2];

                                        //*** Create Folder ***//
                                        if (!DirInfo.Exists)
                                        {
                                            DirInfo.Create();

                                            fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                            ddlTypeAttachment.SelectedIndex = 0;
                                            txtDetail.Text = string.Empty;
                                        }
                                        else
                                        {
                                            fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                            ddlTypeAttachment.SelectedIndex = 0;
                                            txtDetail.Text = string.Empty;
                                        }
                                    }
                                    else
                                    {
                                        UCModalError.ShowMessageError = SysMessage.PleaseDeleteFile;
                                        UCModalError.ShowModalError();

                                        ddlTypeAttachment.SelectedIndex = 0;
                                        txtDetail.Text = string.Empty;
                                    }


                                }
                                else
                                {
                                    //Upload File Normal
                                    if (TypeFile == null)
                                    {
                                        tempFileName = "_" + UserProfile.IdCard + "_" + ddlRequestLicenseType.SelectedValue + "_" + ddlTypeAttachment.SelectedValue + surNameFile;

                                        UploadFile(tempFileName);

                                        string[] _fileName = tempFileName.Split('_');

                                        string masterFileName = _fileName[1] + "_" + _fileName[2] + "_" + _fileName[3];

                                        //*** Create Folder ***//
                                        if (!DirInfo.Exists)
                                        {
                                            DirInfo.Create();

                                            fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                            ddlTypeAttachment.SelectedIndex = 0;
                                            txtDetail.Text = string.Empty;
                                        }
                                        else
                                        {
                                            fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                            ddlTypeAttachment.SelectedIndex = 0;
                                            txtDetail.Text = string.Empty;
                                        }

                                    }
                                    else
                                    {

                                        tempFileName = "_" + UserProfile.IdCard + "_" + ddlTypeAttachment.SelectedValue + "_" + yearMonthDay + hourMinSec + surNameFile;

                                        UploadFile(tempFileName);

                                        string[] _fileName = tempFileName.Split('_');

                                        string masterFileName = _fileName[1] + "_" + _fileName[2] + "_" + _fileName[3];

                                        //*** Create Folder ***//
                                        if (!DirInfo.Exists)
                                        {
                                            DirInfo.Create();

                                            fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                            ddlTypeAttachment.SelectedIndex = 0;
                                            txtDetail.Text = string.Empty;
                                        }
                                        else
                                        {
                                            fulFile.PostedFile.SaveAs(Server.MapPath(mapPath + "/" + this.TempFolderOracle + "/" + masterFileName));

                                            ddlTypeAttachment.SelectedIndex = 0;
                                            txtDetail.Text = string.Empty;
                                        }

                                        //UCModalError.ShowMessageError = SysMessage.PleaseSelectFile;
                                        //UCModalError.ShowModalError();


                                        UCModalError.ShowMessageError = SysMessage.PleaseDeleteFile;
                                        UCModalError.ShowModalError();

                                        ddlTypeAttachment.SelectedIndex = 0;
                                        txtDetail.Text = string.Empty;
                                    }

                                }
                            }
                            else
                            {

                                UCModalError.ShowMessageError = SysMessage.PleaseInputIDNumber;
                                UCModalError.ShowModalError();

                                ddlTypeAttachment.SelectedIndex = 0;
                                txtDetail.Text = string.Empty;
                            }
                        }
                        else
                        {

                            //UCModalError.ShowMessageError = SysMessage.PleaseSelectFile;
                            //UCModalError.ShowModalError();

                            ddlTypeAttachment.SelectedIndex = 0;
                            txtDetail.Text = string.Empty;
                        }

                    }
                    else
                    {
                        UCModalError.ShowMessageError = SysMessage.PleaseChooseFile;
                        UCModalError.ShowModalError();

                        ddlTypeAttachment.SelectedIndex = 0;
                        txtDetail.Text = string.Empty;
                    }
                }
                else
                {
                    UCModalError.ShowMessageError = SysMessage.PleaseSelectFile;
                    UCModalError.ShowModalError();

                    ddlTypeAttachment.SelectedIndex = 0;
                    txtDetail.Text = string.Empty;
                }

                UpdatePanelUpload.Update();
            }
            else
            {

            }

        }

        private void UnEnabledUpload()
        {
            ddlTypeAttachment.Enabled = false;
            txtDetail.Enabled = false;
            fulFile.Enabled = false;
            btnUploadFile.Enabled = false;
        }

        private void EnabledUpload()
        {
            ddlTypeAttachment.Enabled = true;
            txtDetail.Enabled = true;
            fulFile.Enabled = true;
            btnUploadFile.Enabled = true;
        }

        public string CheckName(string filename)
        {
            if (System.IO.File.Exists(Server.MapPath("~/UploadFile/RecieveLicense/" + filename)))
            {
                filename = "_" + filename;
                CheckName(filename);
            }

            return filename;
        }

        private static bool validateFileType(string tempFileName)
        {
            string fileExtension = System.IO.Path.GetExtension(tempFileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileType.IMAGE_BMP_GIF_JPG_PNG_TIF_PDF.ToString().ToLower().Contains(fileExtension);
            return invalidFileExtensions;
        }

        protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatusGv = (Label)e.Row.FindControl("lblStatusGv");
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lbnEditGv");
                LinkButton hplView = (LinkButton)e.Row.FindControl("hplView");
                LinkButton hplDelete = (LinkButton)e.Row.FindControl("hplDelete");

                if (lblStatusGv.Text == "D")
                {
                    lbnEditGv.Visible = false;
                    hplView.Visible = false;
                    hplDelete.Visible = false;

                    e.Row.Style.Value = "text-decoration:line-through;";
                }


            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultData();
        }
    }
}
