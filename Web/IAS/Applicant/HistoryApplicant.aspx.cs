using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Threading;
using System.Globalization;
using System.Data;
using IAS.BLL;
using IAS.Properties;


namespace IAS.Applicant
{
    public partial class HistoryApplicant : basepage
    {
        int PageSize = 20;
        public string ApplicantView
        {
            get
            {
                return Session["ApplicantView"] == null ? string.Empty : Session["ApplicantView"].ToString();
            }
            set
            {
                Session["ApplicantView"] = value;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ApplicantView = "AppView";
                ucAttachFileControl1.GetDocReqApplicantName();
                PanelOldApplicant.Visible = false;
                PanelEditContent.Visible = false;
                GetdllPreName();

                //int PageSize = PAGE_SIZE_Key;

                txtNumberGvSearch.Text = "0";
                rowPerpage.Text = Convert.ToString(PageSize);

                
            }

        }
        private void GetdllPreName()
        {
            this.GetTitle(ddlNewPreNameCode);
        }
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };
        public void GetTitle(DropDownList dropdown)
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(message);
            BindToDDL(dropdown, ls);

        }
       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlStatus.Text == "เลือก")
                {
                    UCModalError.ShowMessageError = "กรุณาเลือกสถานะ";
                    UCModalError.ShowModalError();
                }
                else
                {
                    txtNumberGvSearch.Text = "0"; //milk
                    lblStatusNames.Visible = false;
                    lblReason.Visible = false;
                    txtReason.Visible = false;
                    BindData(true);
                   
                    
                    if (ddlStatus.SelectedValue == "0")//รอการพิจารณา
                    {
                        PanelApprove.Visible = true;
                        PanelOldApplicant.Visible = false;
                        PanelEditContent.Visible = false;
                        
                    }
                    else if (ddlStatus.SelectedValue == "1")//สถานะผ่านการพิจารณา ไม่ให้แสดงปุ่มส่งผลการพิจารณา
                    {
                        PanelApprove.Visible = false;
                        PanelOldApplicant.Visible = false;
                        PanelEditContent.Visible = false;
                    }
                    else if (ddlStatus.SelectedValue == "2")//ถ้าสถานะไม่ผ่านการพิจารณา ไม่ให้แสดงปุ่มส่งผลการพิจารณา
                    {
                        PanelApprove.Visible = false;
                        PanelOldApplicant.Visible = false;
                        PanelEditContent.Visible = false;
                    }
                    else//ddlStatus==3
                    {
                        if (PanelOldApplicant.Visible == true && PanelOldApplicant.Visible==true)
                        {
                            //PnlDetailSearchGridView.Visible = false;
                            //gvDetail.DataSource = null;
                            txtPreNameCode.Text = "";
                            txtNames.Text = "";
                            txtLastName.Text = "";
                            PanelOldApplicant.Visible = false;
                            txtExamPlaceCode.Text = "";
                            txtNewIDCard.Text = "";
                            ddlNewPreNameCode.SelectedIndex = 0;
                            txtNewNames.Text = "";
                            txtNewLastName.Text = "";
                            txtRemark.Text = "";
                            ddlApprove.Text = "เลือก";
                            lblChangeid.Text = "";
                            PanelEditContent.Visible = false;
                            PanelApprove.Visible = false;
                        }

                    }
                }
            }
            catch
            { 
            
            }
        }
         
        protected void BindData(Boolean Count)
        {
            try
            {
              
                    #region page
                    int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
                    int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();

                    resultPage = resultPage == 0 ? 1 : resultPage;
                    if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (rowPerpage.Text.ToInt() == 0))
                    {
                        rowPerpage.Text = PageSize.ToString();
                    }
                    else
                    {
                        PageSize = Convert.ToInt32(rowPerpage.Text);
                    }
                    #endregion page


                    var biz = new BLL.ApplicantBiz();
                    //ApplicantBiz biz = new BLL.ApplicantBiz();




                    string a;
                    string s;
                    string AS_result;
                    string o_result;
                    a = ddlStatus.SelectedValue;

                    if (a != "")
                    {
                        var stringValue = a;
                        s = stringValue.Substring(0, 1);
                        AS_result = stringValue.Substring(1, 1);
                        o_result = stringValue.Substring(2, 1);
                    }
                    else
                    {
                        s = "";
                        AS_result = "";
                        o_result = "";
                    }

                    if (Count)
                    {
                        #region Page
                        var CountPage = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIDCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, s, resultPage, PageSize, true, AS_result, o_result);

                        if (CountPage.DataResponse != null)
                            if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                            {
                                Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                                VisibleGV(gvDetail, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                                if (Rpage == 0)
                                    NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            }
                            else
                            {
                                VisibleGV(gvDetail, 0, Convert.ToInt32(rowPerpage.Text), true);
                                if (Rpage == 0)
                                    NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                                txtTotalPage.Text = "1";
                            }
                        #endregion Page
                    }


                    //stringValue.substr(3, 7); //lo worl






                    var res = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIDCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, s, resultPage, PageSize, false, AS_result, o_result);

                    string compcode = base.UserProfile.CompCode;
                    if (res != null)
                    {
                        PnlDetailSearchGridView.Visible = true;
                        bludDiv1.Visible = true;

                        gvDetail.DataSource = res.DataResponse;
                        gvDetail.DataBind();
                        //int rowcount = res.DataResponse.Tables[0].Rows.Count;
                        //lblTotal.Text = rowcount.ToString();
                        div_totalPage.Visible = true;
                        lblTotal.Visible = true;
                    }
                    else
                    {
                        UCModalError.ShowMessageError = "ไม่พบข้อมูลผู้สมัครสอบ";
                        UCModalError.ShowModalError();
                    }
                 
            }
            catch (Exception ex)
            {

            }
        }
        protected void hplView_Click(object sender, EventArgs e)
        {
            PanelOldApplicant.Visible = true;
            PanelEditContent.Visible = true;
            ddlApprove.SelectedIndex = 0;
            lblReason.Visible = false;
            txtReason.Visible = false;
            txtReason.Text = "";
        }
        int A = 1;
        int Z = 1;
        string idcard;
        string Testing;
        protected void gvSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           

            
                GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;


                    
                    Label testingNo = (Label)gvDetail.Rows[rowindex].FindControl("lblTestingNo");
                    Label IDCard = (Label)gvDetail.Rows[rowindex].FindControl("lblIDCard");
                    Label OldPrefix = (Label)gvDetail.Rows[rowindex].FindControl("lblOLDPreFix");
                    Label OldFirstName = (Label)gvDetail.Rows[rowindex].FindControl("lblFirstName");
                    Label OldLastName = (Label)gvDetail.Rows[rowindex].FindControl("lblLastName");
                    Label ExamPlaceName = (Label)gvDetail.Rows[rowindex].FindControl("lblExamPlaceName");
                    Label NewIDCARD = (Label)gvDetail.Rows[rowindex].FindControl("lblNewIDCARD");
                    Label NewPrefix = (Label)gvDetail.Rows[rowindex].FindControl("lblNewPreFix");
                    Label NewFirstName = (Label)gvDetail.Rows[rowindex].FindControl("lblNewFName");
                    Label NewLastName = (Label)gvDetail.Rows[rowindex].FindControl("lblNewLName");
                    Label Remark = (Label)gvDetail.Rows[rowindex].FindControl("lblRemark");
                    Label Status = (Label)gvDetail.Rows[rowindex].FindControl("lblStatus");
                    Label OldPreFixName = (Label)gvDetail.Rows[rowindex].FindControl("lblOldPreFixName");
                    Label changeid = (Label)gvDetail.Rows[rowindex].FindControl("lblchangeid");
                    //Label lblStatus = (Label)gvDetail.Rows[rowindex].FindControl("lblStatus");
                    Label lblstatusID = (Label)gvDetail.Rows[rowindex].FindControl("lblstatusID");
                    Label lblAssociationResult = (Label)gvDetail.Rows[rowindex].FindControl("lblAssociationResult");
                    Label lbloicresult = (Label)gvDetail.Rows[rowindex].FindControl("lbloicresult");
                    Label lblCancelReason = (Label)gvDetail.Rows[rowindex].FindControl("lblCancelReason");
                    
                    
                    txtPreNameCode.Text = OldPreFixName.Text;
                    txtNames.Text = OldFirstName.Text;
                    txtLastName.Text = OldLastName.Text;
                    txtExamPlaceCode.Text = ExamPlaceName.Text;
                    txtNewIDCard.Text = NewIDCARD.Text;
                    ddlNewPreNameCode.SelectedValue = NewPrefix.Text;
                    txtNewNames.Text = NewFirstName.Text;
                    txtNewLastName.Text = NewLastName.Text;
                    txtRemark.Text = Remark.Text;
                    lblChangeid.Text = changeid.Text;
                    idcard = IDCard.Text;
                    Testing = testingNo.Text;


                    lblTestingNoVisibleF.Text = testingNo.Text;
                    lblOLdIDCardVisibleF.Text = IDCard.Text;

                    //var biz = new BLL.ApplicantBiz();

                    //var res1 = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIDCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, ddlStatus.SelectedValue, A, Z, false);


                    //int a = res1.DataResponse.Tables[0].Rows.Count;
                    //if (res1.DataResponse.Tables[0].Rows.Count > 0)
                    //{
                        //DataTable DT1 = res1.DataResponse.Tables[0];
                        //DataRow DR1 = DT1.Rows[0];

                        string asso_result = lblAssociationResult.Text;
                        string oic_result = lbloicresult.Text;
                        string status = lblstatusID.Text;
                        string reason = lblCancelReason.Text;            

                        if (status == "0" && asso_result == "0" && oic_result == "0")
                        {
                            lblStatusNames.Visible = false;
                            PanelApprove.Visible = true;
                        }
                        else if (status == "1" && asso_result == "1" && oic_result=="0")
                        {
                            lblStatusNames.Visible = true;
                            lblStatusNames.Text = "***รอการพิจารณาจากคปภ.***";

                           

                            PanelApprove.Visible = false;
                        }
                        else if (status == "1" && asso_result == "2" && oic_result=="0")
                        {
                            lblStatusNames.Visible = true;
                            if (reason != "")
                            {
                                lblStatusNames.Text = "***ไม่ผ่านการพิจารณาจากสมาคม*** <br> <U>เนื่องจาก</U> "+ reason ;
                            }
                            else
                            {
                                lblStatusNames.Text = "***ไม่ผ่านการพิจารณาจากสมาคม***";
                            }

                            
                            PanelApprove.Visible = false;
                        }
                        else if (status == "2" && asso_result=="1" && oic_result == "1" )
                        {
                            lblStatusNames.Visible = true;
                            lblStatusNames.Text = "***ผ่านการพิจารณาจากคปภ.เรียบร้อย***";

                           

                            PanelApprove.Visible = false;
                        }
                        else if (status == "2" && asso_result == "1" && oic_result == "2")
                        {
                            lblStatusNames.Visible = true;
                            if (reason != "")
                            {
                                lblStatusNames.Text = "***ไม่ผ่านการพิจารณาจากคปภ*** <br> เนื่องจาก " + reason;
                            }
                            else
                            {
                                lblStatusNames.Text = "***ไม่ผ่านการพิจารณาจากคปภ***";
                            }

                            


                            PanelApprove.Visible = false;

                        }
                        //txtExamPlaceCode.Text = DR1["Exam_place_name"].ToString();
                        //txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                        //ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                        //txtNewNames.Text = DR1["new_fname"].ToString();
                        //txtNewLastName.Text = DR1["new_lname"].ToString();
                        //txtRemark.Text = DR1["REMARK"].ToString();

                        txtNewIDCard.Enabled = false;
                        ddlNewPreNameCode.Enabled = false;
                        txtNewNames.Enabled = false;
                        txtNewLastName.Enabled = false;
                        txtRemark.Enabled = false;



                        

                        GetAttatchRegisterationFiles();
                   
                

            
        }
        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text != "เลือก")
            {
                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
                BindData(false);
                Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
                UpdatePanelSearch.Update();
            }
            else
            {
                UCModalError.ShowMessageError = "กรุณาเลือกสถานะ";
                UCModalError.ShowModalError();
            }
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text != "เลือก")
            {
                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
                BindData(false);
                Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
                UpdatePanelSearch.Update();
            }
            else
            {
                UCModalError.ShowMessageError = "กรุณาเลือกสถานะ";
                UCModalError.ShowModalError();
            }
        }

        protected void btnSearchGo_Click(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text != "เลือก")
            {
                txtNumberGvSearch.Text = "0";
                BindData(true);
            }
            else
            {
                UCModalError.ShowMessageError = "กรุณาเลือกสถานะ";
                UCModalError.ShowModalError();
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {

            if (ddlApprove.Text == "เลือก")
            {
                UCModalError.ShowMessageError = "กรุณาเลือกผลการพิจารณา";
                UCModalError.ShowModalError();

            }
            else
            {
                UpdateApplicantChange();

            }
        }

        protected void btnCancleSend_Click(object sender, EventArgs e)
        {
            PanelOldApplicant.Visible = false;
            PanelEditContent.Visible = false;
            PanelApprove.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
            
        }
        protected void ClearText()
        {
            txtIDCard.Text = "";
            txtTestingNo.Text = "";
            ddlStatus.Text = "เลือก";

            PnlDetailSearchGridView.Visible = false;
            gvDetail.DataSource = null;
            txtPreNameCode.Text = "";
            txtNames.Text = "";
            txtLastName.Text = "";
            PanelOldApplicant.Visible = false;
            txtExamPlaceCode.Text = "";
            txtNewIDCard.Text = "";
            ddlNewPreNameCode.SelectedIndex = 0;
            txtNewNames.Text = "";
            txtNewLastName.Text = "";
            txtRemark.Text = "";
            ddlApprove.Text = "เลือก";
            lblChangeid.Text = "";
            PanelEditContent.Visible = false;

            lblStatusNames.Visible = false;
        }
        private void UpdateApplicantChange()
        {

            var biz = new BLL.ApplicantBiz();
            //var res = biz.GetRequestEditApplicant((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.CompCode);
            //DataTable DT = res.DataResponse.Tables[0];
            //DataRow DR = DT.Rows[0];

            DTO.ApplicantChange AppChange = new DTO.ApplicantChange();

            //AppChange.STATUS = Convert.ToInt16(ddlApprove.SelectedValue);
            AppChange.CHANGE_ID = Convert.ToInt32(lblChangeid.Text);
            AppChange.ASSOCIATION_USER_ID = base.UserId;
            AppChange.ASSOCIATION_DATE = DateTime.Today;
            if (ddlApprove.SelectedValue == "0")
            {
                AppChange.ASSOCIATION_RESULT = 1;//ผ่านการพิจารณา
                AppChange.STATUS = 1;//ส่งเรื่องให้ OIC Approve
            }
            else
            {
                AppChange.ASSOCIATION_RESULT = 2;//ไม่ผ่านการพิจารณา 
                AppChange.STATUS = 1;//ไม่ส่งเรื่องให้ OIC Approve
                if (txtReason.Text == "")
                {
                    txtReason.Text = "ไม่ผ่านการพิจารณาจากสมาคม";
                    AppChange.CANCEL_REASON = txtReason.Text;
                }
                else
                {
                    AppChange.CANCEL_REASON = txtReason.Text;
                }
            }

            
            var insertAppChange = biz.InsertApplicantChange(AppChange);
            if (insertAppChange.ResultMessage == true)
            {
                var res = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, lblOLdIDCardVisibleF.Text, lblTestingNoVisibleF.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, "", A, Z, false, "", "");
                DataTable DT = res.DataResponse.Tables[0];
                DataRow DR = DT.Rows[0];

                string status = DR["STATUS"].ToString();
                string asso = DR["ASSOCIATION_RESULT"].ToString();
                string oic = DR["OIC_RESULT"].ToString();
                string IDCardCreateBy = DR["CREATE_BY"].ToString();
                string OLDidcard = DR["OLD_ID_CARD_NO"].ToString();
                string TestingNO = DR["Testing_no"].ToString();


                var sendMail = biz.SendMailAppChange(IDCardCreateBy, TestingNO, OLDidcard);//ส่งเมล์


                ClearText();
                UCModalSuccess.ShowMessageSuccess = "บันทึกสำเร็จ";
                UCModalSuccess.ShowModalSuccess();
                

            }
            else
            {
                UCModalError.ShowMessageError = "พบข้อผิดพลาด";
                UCModalError.ShowModalError();
            }

        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvDetail":
                    lblTotal.Text = Convert.ToString(total_row_count);
                    rows_per_page = (rows_per_page == 0 || rows_per_page == null) ? 1 : rows_per_page;
                    double Paggge = Math.Ceiling(total_row_count / rows_per_page);
                    txtTotalPage.Text = (total_row_count > 0) ? Convert.ToString(Paggge) : "0";
                    lblTotal.Visible = visible_or_disvisible;
                    txtTotalPage.Visible = visible_or_disvisible;
                    rowPerpage.Visible = visible_or_disvisible;
                    lblParaPage.Visible = visible_or_disvisible;
                    pageGo.Visible = visible_or_disvisible;
                    TXTrowperpage.Visible = visible_or_disvisible;
                    break;

                default:
                    break;
            }
        }

        protected void NPbutton(Button PreName, TextBox txtNum, Button NextName, string N_or_P, Label Maxpage)
        {
            int MaxP = 1;
            MaxP = ((Maxpage.Text.Trim() == "") || (Maxpage.Text.Trim() == "0")) ? MaxP : Maxpage.Text.ToInt();
            Maxpage.Text = (Maxpage.Text.ToInt() != MaxP) ? MaxP.ToString() : Maxpage.Text;
            if (N_or_P == "P")
            {
                if (txtNum.Text.ToInt() > 1)
                {
                    txtNum.Text = Convert.ToString(Convert.ToInt32(txtNum.Text) - 1);
                }
            }
            else if (N_or_P == "N")
            {
                txtNum.Text = Convert.ToString(Convert.ToInt32(txtNum.Text) + 1);

            }
            else
            {
                txtNum.Text = "1";
                PreName.Visible = false;
            }


            Hide_show(PreName, txtNum, NextName, N_or_P, MaxP);
        }
        protected void Hide_show(Button PreName, TextBox txtNum, Button NextName, string N_or_P, int MaxP)
        {
            if (txtNum.Text.ToInt() == 1)
            {
                PreName.Visible = false;
            }
            else
            {
                PreName.Visible = true;
            }
            UpdatePanelSearch.Update();
            if (txtNum.Text.ToInt() == MaxP)
            {
                NextName.Visible = false;
            }
            else
            {
                NextName.Visible = true;
            }

        }
       

      
        #endregion Pageing_milk

        protected void ddlApprove_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlApprove.SelectedValue == "0")
            {
                //txtReason.Enabled = false;
                txtReason.Visible = false;
                lblReason.Visible = false;
            }
            else if (ddlApprove.SelectedValue == "1")//ไม่อนุมัติให้กรอกสาเหตุ
            {
                //txtReason.Enabled = true;
                txtReason.Text = "";
                txtReason.Visible = true;
                lblReason.Visible = true;
            }
            else
            {
                txtReason.Visible = false;
                lblReason.Visible = false;

            }
        }
        public void GetAttachFilesAllType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            this.ucAttachFileControl1.DocumentTypeAll = ls;
        }
        public void GetAttatchRegisterationFiles()
        {
            ApplicantBiz biz = new BLL.ApplicantBiz();
            DTO.ResponseService<DTO.AttachFileApplicantChangeEntity[]> res = biz.GetAttatchFilesAppChangeByIDCard(idcard.Trim(), Convert.ToInt32(lblChangeid.Text));
            //var list = res.DataResponse.ToList();
            if (res.DataResponse.Count() > 0)
            {
                IList<BLL.AttachFilesIAS.AttachFile> ls = BLL.AttachFilesIAS.AttachFileMapper.ConvertToAttachFilesApplicantView(res.DataResponse.ToList());
                ucAttachFileControl1.AttachFiles = ls.ToList();
                //this.GetAttachFilesType();
                this.GetAttachFilesAllType();
                ucAttachFileControl1.GridAttachFiles.DataSource = ls.ToList();
                ucAttachFileControl1.GridAttachFiles.DataBind();

                ucAttachFileControl1EnabledFalse();
            }
            else
            {
                this.GetAttachFilesAllType();
                ucAttachFileControl1.GridAttachFiles.DataSource = null;
                ucAttachFileControl1.GridAttachFiles.DataBind();
            }

            UpdatePanelSearch.Update();
        }
        
        public void ucAttachFileControl1EnabledFalse()
        {
            ucAttachFileControl1.TextRemark.Enabled = false;
            ucAttachFileControl1.PnlAttachFiles.Enabled = false;
            ucAttachFileControl1.DropDownDocumentType.Enabled = false;
            ucAttachFileControl1.ButtonUpload.Enabled = false;
            this.ucAttachFileControl1.EnableUpload(false);
            ucAttachFileControl1.TextRemark.Text = "";



            //ucAttachFileControl1.TextRemark.Visible = false;
            //ucAttachFileControl1.DropDownDocumentType.Visible = false;
            //ucAttachFileControl1.ButtonUpload.Visible = false;
            //ucAttachFileControl1.VisableUpload(false);
            //ucAttachFileControl1.TextRemark.Visible = false;

            if (ucAttachFileControl1.GridAttachFiles.Rows.Count > 0)
            {
                for (int i = 0; i < ucAttachFileControl1.GridAttachFiles.Rows.Count; i++)
                {
                    LinkButton hplCancel = (LinkButton)ucAttachFileControl1.GridAttachFiles.Rows[i].FindControl("hplCancel");
                    if (hplCancel != null)
                    {
                        hplCancel.Visible = false;
                    }
                }
            }
            //ucAttachFileControl1.
        }

       
        
    }
}