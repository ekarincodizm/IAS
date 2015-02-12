using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.IO;
using IAS.Utils;
using IAS.Properties;
using IAS.DTO.FileService;
using System.Globalization;

namespace IAS.License
{
    public partial class RenewLicenseNo : basepage
    {
        class Renew
        {
            public DateTime RenewDate { get; set; }
        }

        private List<Renew> lstRenew;
        private int RowPerPage { get { return PAGE_SIZE_Key; } }
        private int NumberGvSearch = 1;
        private double TotalRows
        {
            get { return (Session["_TotalRows"] == null) ? double.Parse("0") : double.Parse(Session["_TotalRows"].ToString()); }
            set { Session["_TotalRows"] = value; }
        }
        private List<DTO.GenLicenseDetail> lsLicenseDetail
        {
            get { return (List<DTO.GenLicenseDetail>)Session["_lsLicenseDetail"]; }
            set { Session["_lsLicenseDetail"] = value; }
        }

        private List<string> PageCheckAll
        {
            get { return (List<string>)Session["_PageCheckAll"]; }
            set { Session["_PageCheckAll"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtApproveDate.Attributes.Add("readonly", "true");
            txtExpireDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                //PnlSearch.Visible = true;
                //txtApproveDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                //txtExpireDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                SetLicenseType();
                ResetControl();
            }
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lsLicenseDetail = null;
            PageCheckAll = null;

            if (Convert.ToDateTime(txtApproveDate.Text) > Convert.ToDateTime(txtExpireDate.Text))
            {
                UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                UCModalError.ShowModalError();
                gvSearch.Visible = false;
                ctrGridView.Visible = false;
            }
            else if(!string.IsNullOrEmpty(txtCompany.Text) && isCompanyPattern() == false){
                UCModalError.ShowMessageError = "ข้อมูลบริษัทไม่ถูกต้อง";
                UCModalError.ShowModalError();
                gvSearch.Visible = false;
                ctrGridView.Visible = false;
            }
            else
            {
                //gvSearch.Visible = true;
                //gvSearch.DataSource = GetData();
                //gvSearch.DataBind();
                
                InitDataToGrid();
            }
        }
        protected void loadFile_Click(object sender, EventArgs e)
        {
           
            try                            
            {
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                String lblTransectionDateGv = ((Label)gr.FindControl("lblTransectionDateGv")).Text;
                DateTime requestDate = new DateTime();
                if (!DateTime.TryParseExact(lblTransectionDateGv, "dd/MM/yyyy hh:mm:ss", new CultureInfo("th-TH"), DateTimeStyles.None, out requestDate))
                {
                    UCModalError.ShowMessageError = SysMessage.DateErrorFormat;
                    UCModalError.ShowModalError();
                    return ;
                }

                String dirZip = "";
                using (BLL.LicenseBiz licenseBiz = new BLL.LicenseBiz()) {
                   var res =  licenseBiz.GenZipFileLicenseRequest(requestDate, base.UserProfile.LoginName);
                   if (res.IsError) {
                       UCModalError.ShowMessageError = res.ErrorMsg;
                      UCModalError.ShowModalError();
                       return;
                   }

                   dirZip = res.DataResponse; 
                }
                //Download(CryptoBase64.Decryption(dirZip));

                Response.Redirect( LinkRedirect(dirZip), true); 
               // ToolkitScriptManager.RegisterClientScriptBlock(UpdatePanelSearch, UpdatePanelSearch.GetType(), "เอกสาร", @"<script type='text/javascript'> PopDownload('"+dirZip+@"'); <\script> ", true);
                //UpdatePanelSearch.Update();  
                
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public void Download(string FileName)
        {

            DownloadFileResponse download;

            using (FileService.FileTransferServiceClient fileService = new FileService.FileTransferServiceClient())
            {
                download = fileService.DownloadFile(new DownloadFileRequest()
                {
                    TargetContainer = "",
                    TargetFileName = FileName

                });

                LoadDocument(download.FileByteStream, (long)download.Length, download.ContentType);

            }


        }
        private void LoadDocument(Stream fileStream, Int64 length, String contentType)
        {
            byte[] img = new byte[(int)length];

            using (BinaryReader br = new BinaryReader(fileStream))
            {
                img = br.ReadBytes((int)length);
                Response.ContentType = contentType;// "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Document"); // change name by milk
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(img, 0, img.Length);
                Response.OutputStream.Flush();
                //Response.End();
            }

        }

        private void AlertMessageShow(String message)
        {
            string alertMessage = String.Format("alert('{0}');", message);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alertMessage, true);
            //AjaxControlToolkit.ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", alertMessage, true);
        }

        private String LinkRedirect(String postData) {
            return String.Format("{0}?req={1}&mode=D"
                        , "../UserControl/FileResponse.aspx", postData);
            //return String.Format("'{0}?req={1}&mode=D'"
            //             , UrlHelper.Resolve("/UserControl/FileResponse.aspx"), postData);
        }
        private String LinkPopUp(String postData)
        {
            return String.Format("window.open('{0}?req={1}&mode=D','','')"
                            , UrlHelper.Resolve("/UserControl/FileResponse.aspx"),postData);
        }
        private List<Renew> GetData() {
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();
            if (!DateTime.TryParse(txtApproveDate.Text.Trim(), out startDate))
            {
                return null;
            }
            if (!DateTime.TryParse(txtExpireDate.Text.Trim(), out endDate))
            {
                return null;
            }
            DTO.ResponseService<DateTime[]> res = new DTO.ResponseService<DateTime[]>();
            using (BLL.LicenseBiz licenseBiz = new BLL.LicenseBiz())
            {
                DTO.RangeDateRequest request = new DTO.RangeDateRequest()
                {
                    StartDate = startDate,
                    EndDate = endDate
                };
                res = licenseBiz.GetLicenseRequestOicApprove(request);
            }
            if (res.IsError)
                return null;

            lstRenew = new List<Renew>();
            foreach (DateTime item in res.DataResponse)
            {
                lstRenew.Add(new Renew() { RenewDate = item });
            }

            return lstRenew;
           

        }
        protected void ibtClearStartPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtApproveDate.Text = string.Empty;
        }

        protected void ibtClearEndPaidDate_Click(object sender, ImageClickEventArgs e)
        {
            txtExpireDate.Text = string.Empty;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            //PnlSearch.Visible = true;
            //txtApproveDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtExpireDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //gvSearch.Visible = false;
            ResetControl();
        }

        #region New License DaTail GridView

        private void SetLicenseType()
        {
            BLL.DataCenterBiz Dcbiz = new BLL.DataCenterBiz();
            var res = Dcbiz.GetConfigLicenseType("ทั้งหมด").DataResponse;
            if (res != null)
            {
                BindToDDL(ddlLicenseType, res.ToList());
            }
        }

        private bool isCompanyPattern()
        {
            string Pattern = "^(.*)\\[(\\d)+\\]$";
            return System.Text.RegularExpressions.Regex.IsMatch(txtCompany.Text, Pattern);
        }

        private string GetCompCode()
        {
            string code = string.Empty;
            string input = txtCompany.Text;
            if (!string.IsNullOrEmpty(txtCompany.Text))
            {
                string Pattern = "^(.*)\\[(\\d)+\\]$";
                if(System.Text.RegularExpressions.Regex.IsMatch(input, Pattern)){
                    code = (txtCompany.Text.Split('[', ']'))[1];
                }
            }
            return code;
        }

        private void InitDataToGrid()
        {
            try
            {
                TotalRows = TotalRow();
                BLL.LicenseBiz LBIZ = new BLL.LicenseBiz();
                txtNumberGvSearch.Text = (txtTotalPage.Text == LastPage().ToString() ? txtNumberGvSearch.Text : "1");
                int Page = int.Parse(txtNumberGvSearch.Text);
                DateTime dateStart = Convert.ToDateTime(txtApproveDate.Text.Trim());
                DateTime dateEnd = Convert.ToDateTime(txtExpireDate.Text.Trim());
                string LicenseType = ddlLicenseType.SelectedValue;
                string CompCode = GetCompCode();
                var res = LBIZ.GetLicenseDetailByCriteria(dateStart, dateEnd, txtIdCardNo.Text.Trim(), txtNames.Text.Trim(), txtLastname.Text.Trim(), LicenseType, CompCode, Page, rowsPerPage(), false);
                if (res.DataResponse != null)
                    gvLicenseDetail.DataSource = res.DataResponse;
                gvLicenseDetail.DataBind();
                
                SetControlPage();
                CheckPageSelectAll();

                bool havaRow = gvLicenseDetail.Rows.Count > 0;
                CheckBox H = (CheckBox)gvLicenseDetail.HeaderRow.FindControl("headSelected") as CheckBox;
                H.Enabled = havaRow;
                btnDownloadZip.Visible = havaRow;
                lblTotalRows.Text = String.Format("จำนวน {0} รายการ", TotalRows);
                ctrGridView.Visible = true;
                
            }
            catch (Exception ex)
            {
                UCModalError.ShowMessageError = ex.Message;
                UCModalError.ShowModalError();
            }

        }

        private double TotalRow()
        {
            BLL.LicenseBiz LBIZ = new BLL.LicenseBiz();
            DateTime dateStart = Convert.ToDateTime(txtApproveDate.Text.Trim());
            DateTime dateEnd = Convert.ToDateTime(txtExpireDate.Text.Trim());
            string LicenseType = ddlLicenseType.SelectedValue;
            string CompCode = GetCompCode();
            var res = LBIZ.GetLicenseDetailByCriteria(dateStart, dateEnd, txtIdCardNo.Text.Trim(), txtNames.Text.Trim(), txtLastname.Text.Trim(), LicenseType, CompCode, 0, 0, true).DataResponse;
            return res == null ? 0 : double.Parse(res.Tables[0].Rows[0][0].ToString());
        }

        private int rowsPerPage()
        {
            int _rpp = RowPerPage;
            if (txtRowsPerpage.Text != "")
                _rpp = int.Parse(txtRowsPerpage.Text);
            return _rpp;
        }

        private double LastPage()
        {
            double mod = TotalRows / rowsPerPage();
            double lp = Math.Ceiling(mod);
            return lp == 0 ? 1 : lp;
        }

        private void SetControlPage()
        {
            int gvPage = int.Parse(txtNumberGvSearch.Text);
            txtTotalPage.Text = LastPage().ToString();
            if (txtNumberGvSearch.Text == NumberGvSearch.ToString() && txtTotalPage.Text == NumberGvSearch.ToString())
            {
                btnPreviousGvSearch.Visible = false;
                btnNextGvSearch.Visible = false;
            }
            else if (TotalRows <= rowsPerPage())
            {
                btnPreviousGvSearch.Visible = false;
                btnNextGvSearch.Visible = false;
            }
            else if (gvPage == 1)
            {
                btnPreviousGvSearch.Visible = false;
                btnNextGvSearch.Visible = true;
            }
            else if (gvPage == LastPage())
            {
                btnPreviousGvSearch.Visible = true;
                btnNextGvSearch.Visible = false;
            }
            else
            {
                btnPreviousGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;
            }
        }

        private void ResetControl()
        {
            PnlSearch.Visible = true;
            ctrGridView.Visible = false;
            txtApproveDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtExpireDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtRowsPerpage.Text = RowPerPage.ToString();
            txtIdCardNo.Text = "";
            txtNames.Text = "";
            txtLastname.Text = "";
            ddlLicenseType.SelectedValue = "";
            txtCompany.Text = "";
            lsLicenseDetail = null;
            PageCheckAll = null;
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            int currPage = int.Parse(txtNumberGvSearch.Text) - 1;
            if (currPage > 0)
                txtNumberGvSearch.Text = currPage.ToString();
            InitDataToGrid();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            int currPage = int.Parse(txtNumberGvSearch.Text) + 1;
            if (currPage <= LastPage())
                txtNumberGvSearch.Text = currPage.ToString();
            InitDataToGrid();
        }

        protected void pageGo_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            InitDataToGrid();
        }
       
        protected void btnDownloadZip_Click(object sender, EventArgs e)
        {
            try
            {
                if (lsLicenseDetail != null && lsLicenseDetail.Count > 0)
                {
                    BLL.LicenseBiz LBIZ = new BLL.LicenseBiz();
                    var res = LBIZ.GenZipFileLicenseByIdCardNo(lsLicenseDetail, base.UserProfile.LoginName);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        lsLicenseDetail = null;
                        PageCheckAll = null;
                        InitDataToGrid();
                        String dirZip = res.DataResponse;
                        Response.Redirect(LinkRedirect(dirZip), true);
                    }
                }
                else
                {
                    InitDataToGrid();
                    UCModalError.ShowMessageError = "กรุณาเลือกรายการ";
                    UCModalError.ShowModalError();
                }
            }
            catch (Exception ex)
            {
                UCModalError.ShowMessageError = ex.Message;
                UCModalError.ShowModalError();
            }
        }

        protected void headSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbAll = (CheckBox)sender;

            if (lsLicenseDetail == null)
                lsLicenseDetail = new List<DTO.GenLicenseDetail>();
            if (PageCheckAll == null)
                PageCheckAll = new List<string>();

            if (cbAll.Checked)
            {
                if (!PageCheckAll.Contains(txtNumberGvSearch.Text))
                    PageCheckAll.Add(txtNumberGvSearch.Text);
            }
            else
            {
                if (PageCheckAll.Contains(txtNumberGvSearch.Text))
                    PageCheckAll.Remove(txtNumberGvSearch.Text);
            }

            foreach (GridViewRow row in gvLicenseDetail.Rows)
            {
                CheckBox cb = row.FindControl("itemSelected") as CheckBox;
                string Group = ((Label)row.FindControl("lblUploadGroupNo")).Text.Trim();
                string IdCardNo = ((Label)row.FindControl("lblIdCardNo")).Text.Trim();
                var dup = lsLicenseDetail.Where(s => s.UPLOAD_GROUP_NO == Group && s.ID_CARD_NO == IdCardNo).FirstOrDefault();
                if (cbAll.Checked)
                {
                    if (dup == null)
                    {
                        lsLicenseDetail.Add(new DTO.GenLicenseDetail() { UPLOAD_GROUP_NO = Group, ID_CARD_NO = IdCardNo });
                        cb.Checked = true;
                    }
                }
                else
                {
                    if (dup != null)
                    {
                        lsLicenseDetail.Remove(dup);
                        cb.Checked = false;
                    }
                }
            }
        }

        protected void itemSelected_CheckedChanged(object sender, EventArgs e)
        {
            if (lsLicenseDetail == null)
                lsLicenseDetail = new List<DTO.GenLicenseDetail>();

            var row = (GridViewRow)((CheckBox)sender).NamingContainer;
            CheckBox cb = row.FindControl("itemSelected") as CheckBox;
            string Group = ((Label)row.FindControl("lblUploadGroupNo")).Text.Trim();
            string IdCardNo = ((Label)row.FindControl("lblIdCardNo")).Text.Trim();
            var dup = lsLicenseDetail.Where(s => s.UPLOAD_GROUP_NO == Group && s.ID_CARD_NO == IdCardNo).FirstOrDefault();

            if (cb.Checked)
            {
                if (dup == null)
                {
                    lsLicenseDetail.Add(new DTO.GenLicenseDetail() { UPLOAD_GROUP_NO = Group, ID_CARD_NO = IdCardNo });
                }
            }
            else
            {
                if (dup != null)
                {
                    lsLicenseDetail.Remove(dup);
                    if (PageCheckAll != null && PageCheckAll.Contains(txtNumberGvSearch.Text))
                    {
                        CheckBox H = (CheckBox)gvLicenseDetail.HeaderRow.FindControl("headSelected") as CheckBox;
                        H.Checked = false;
                        PageCheckAll.Remove(txtNumberGvSearch.Text);
                    }
                }
            }
            CheckPageSelectAll();
        }

        protected void gvLicenseDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (PageCheckAll != null)
                {
                    CheckBox Hcb = e.Row.FindControl("headSelected") as CheckBox;
                    Hcb.Checked = PageCheckAll.Contains(txtNumberGvSearch.Text);
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label isDownload = e.Row.FindControl("lblIsDownload") as Label;
                Label StatusDownload = e.Row.FindControl("lblStatusDownload") as Label;

                StatusDownload.Text = (isDownload.Text == "T") ? "ดาวน์โหลดแล้ว" : "ยังไม่ได้ดาวน์โหลด";

                if (lsLicenseDetail != null)
                {
                    CheckBox cb = e.Row.FindControl("itemSelected") as CheckBox;
                    Label Group = e.Row.FindControl("lblUploadGroupNo") as Label;
                    Label IdCardNo = e.Row.FindControl("lblIdCardNo") as Label;

                    var q = lsLicenseDetail.Where(s => s.UPLOAD_GROUP_NO == Group.Text && s.ID_CARD_NO == IdCardNo.Text).FirstOrDefault();
                    cb.Checked = (q != null);
                }
            }
        }

        private void CheckPageSelectAll()
        {
            bool isAll = false;
            foreach (GridViewRow row in gvLicenseDetail.Rows)
            {
                CheckBox cb = row.FindControl("itemSelected") as CheckBox;
                isAll = cb.Checked;
                if (!isAll) break;
            }

            CheckBox H = (CheckBox)gvLicenseDetail.HeaderRow.FindControl("headSelected") as CheckBox;
            H.Checked = isAll;
            if (PageCheckAll == null && isAll)
            {
                PageCheckAll = new List<string>();
                PageCheckAll.Add(txtNumberGvSearch.Text);
            }
            else if (PageCheckAll != null && !PageCheckAll.Contains(txtNumberGvSearch.Text))
            {
                PageCheckAll.Add(txtNumberGvSearch.Text);
            }
            else if (PageCheckAll != null && PageCheckAll.Contains(txtNumberGvSearch.Text))
            {
                PageCheckAll.Remove(txtNumberGvSearch.Text);
            }
        }
        #endregion
    }
}