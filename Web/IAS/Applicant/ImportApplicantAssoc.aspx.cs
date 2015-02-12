using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using IAS.Utils;
using IAS.DTO;
using System.Text;
using IAS.Properties;

namespace IAS.Applicant
{
    public partial class ImportApplicantOIC : basepage
    {
        public SummaryReceiveApplicant SummaryReceiveApplicant 
        {
            get {
                return (Session["SummaryReceiveApplicant"] == null) ? CreateSummaryReceiveApplicant() 
                                                                    : (SummaryReceiveApplicant)Session["SummaryReceiveApplicant"];
            }
        }

        private SummaryReceiveApplicant CreateSummaryReceiveApplicant() {
            SummaryReceiveApplicant summary = new SummaryReceiveApplicant();
            summary.Header = new DTO.UploadHeader();
            summary.ReceiveApplicantDetails = new List<ApplicantTemp>();
            summary.UploadGroupNo = "";
            summary.MessageError = "";
          
            return summary;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                base.HasPermit();
                ucPersonalApplicantDetail1.GetTitleName();
                ucPersonalApplicantDetail1.GetEducation();
            }
            linkApplicantFile.OnClientClick = String.Format("window.open('{0}','','')"
                        , UrlHelper.Resolve("/UserControl/FileApplicantUpload.aspx"));
        }
        private void CanSubmitData()
        {
            Boolean result = false;
            if (!String.IsNullOrEmpty(SummaryReceiveApplicant.MessageError))
            {
                result = false;
            }
            else
            {
                Int32 trueAmount = SummaryReceiveApplicant.ReceiveApplicantDetails.Count(a => a.LOAD_STATUS == "T");
                if (trueAmount == SummaryReceiveApplicant.ReceiveApplicantDetails.Count())
                {
                    result = true;
                }
                else result = false;
            }


            btnImport.Visible = result;
            btnImportCancel.Visible = result;
            btnImport.Enabled = result;
            btnImportCancel.Enabled = result;

            lblErrMessage.Text = Resources.propGroupApplicantDetail_ErrMessage;
            lblErrMessage.Visible = (!result);
        }
        public void ClearSummaryApplicant()
        {
            Session["SummaryReceiveApplicant"] = null;
            pnlImportFile.Visible = false;
        }
        protected void btnLoadFile_Click(object sender, EventArgs e)
        {
            string tempFileName = Path.GetFileName(FuFile.PostedFile.FileName);
            bool invalid = validateFileType(tempFileName);
            ClearSummaryApplicant();
            if (!string.IsNullOrEmpty(FuFile.FileName))
            {

                if (invalid)
                {
                    BLL.ApplicantBiz biz = new BLL.ApplicantBiz();
                    var res = biz.UploadData(FuFile.FileName, FuFile.PostedFile.InputStream, "","", UserId, base.UserProfile);
                    if (!res.IsError)
                    {
                        Session["SummaryReceiveApplicant"] = res.DataResponse;
                        pnlImportFile.Visible = true;
                        List<DTO.UploadHeader> headers = new List<UploadHeader>();
                        headers.Add(res.DataResponse.Header);
                        gvImportFile.DataSource = headers;
                        gvImportFile.DataBind();

                        hdfGroupID.Value = res.DataResponse.UploadGroupNo;
                    

                        gvCheckList.DataSource = res.DataResponse.ReceiveApplicantDetails;
                        gvCheckList.DataBind();
                        CanSubmitData();
                    }
                    else
                    {

                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                }
                else
                {
                    UCModalError.ShowMessageError = SysMessage.CannotWrongUploadFile;
                    UCModalError.ShowModalError();
                }
            }
            else
            {
                UCModalError.ShowMessageError = SysMessage.CannotUploadFile;
                UCModalError.ShowModalError();

            }
        }

        private static bool validateFileType(string tempFileName)
        {
            string fileExtension = System.IO.Path.GetExtension(tempFileName).Replace(".", string.Empty).ToLower();
            bool invalidFileExtensions = DTO.DocumentFileCSV.CSV.ToString().ToLower().Contains(fileExtension);
            return invalidFileExtensions;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            ImportInsertMode();
        }

        private void ImportInsertMode()
        {
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();

            var result = biz.ApplicantGroupUploadToSubmit(hdfGroupID.Value, base.UserProfile);
            if (!result.IsError)
            {
                UCModalSuccess.ShowMessageSuccess = Resources.infoGroupApplicantDetail_001;
                UCModalSuccess.ShowModalSuccess();
                CleanData();
                pnlImportFile.Visible = false;

            }
            else
            {
                UCModalError.Visible = true;
                UCModalError.ShowMessageError = result.ErrorMsg;
                UCModalError.ShowModalError();
            }
        }

        private void CleanData()
        {
            hdfGroupID.Value = string.Empty;
            hdfSegNo.Value = string.Empty;
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var strGroupID = hdfGroupID.Value;
            var gv = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label strSeqNo = (Label)gv.FindControl("lblSeqNoGv");
            hdfSegNo.Value = strSeqNo.Text;
            ucPersonalApplicantDetail1.PersonApplicantReadOnlyMode();
            ucPersonalApplicantDetail1.BindApplicantUploadTempByID(strGroupID, strSeqNo.Text);
            ModGroupApplicant.Show();
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

        protected void btnDetailSubmit_Click(object sender, EventArgs e)
        {
            //DetailInsertMode();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void ReBindDatainGrid(string strGroupUploadNo)
        {
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();

            var res = biz.GetApplicantGroupUploadByGroupUploadNo(strGroupUploadNo);
            if (!res.IsError)
            {
                gvImportFile.DataSource = res.DataResponse.Header;
                gvImportFile.DataBind();

                gvCheckList.DataSource = res.DataResponse.Detail;
                gvCheckList.DataBind();
            }
            else
            {
                var errorMsg = res.ErrorMsg;
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }

        }

        protected void btnImportCancel_Click(object sender, EventArgs e)
        {
            pnlImportFile.Visible=false;
        }

        protected void gvImportFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblItemsIncorrectRemarkGv = (Label)e.Row.FindControl("lblItemsIncorrectRemark");

                if (lblItemsIncorrectRemarkGv != null)
                {
                    lblItemsIncorrectRemarkGv.Text = SummaryReceiveApplicant.MessageError;
                }

            }
        }
    }
}