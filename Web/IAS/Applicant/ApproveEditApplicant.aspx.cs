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
    public partial class ApproveEditApplicant : basepage
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
                PanelOldApplicant.Visible = false;
                PanelEditContent.Visible = false;
                GetdllPreName();
                txtNumberGvSearch.Text = "0";
                rowPerpage.Text = Convert.ToString(PageSize);
                this.ApplicantView = "AppView";
                ucAttachFileControl1.GetDocReqApplicantName();
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

                    if (PanelOldApplicant.Visible == true && PanelOldApplicant.Visible == true)
                    {
                        ClearPanelOldANDEdit();
                        lblReason.Visible = false;
                        txtReason.Visible = false;
                        lblStatusNames.Visible = false;
                    }
                    lblStatusNames.Visible = false;
                    gvDetail.Visible = true;
                    BindData(true);

                    if (ddlStatus.SelectedValue == "1")//สถานะรอการพิจารณา ไม่ให้แสดงปุ่มส่งผลการพิจารณา
                    {
                        lblApprove.Visible = true;
                        ddlApprove.Visible = true;
                        btnSend.Visible = true;
                        btnCancleSend.Visible = true;
                    }
                    else if (ddlStatus.SelectedValue == "2" && ddlStatus.SelectedValue=="3")//สถานะผ่านและไม่ผ่านการพิจารณา ไม่ให้แสดงปุ่มส่งผลการพิจารณา
                    {
                        lblApprove.Visible = false;
                        ddlApprove.Visible = false;
                        btnSend.Visible = false;
                        btnCancleSend.Visible = false;
                    }
                    else//สถานะคือ ทั้งหมด
                    {
                        if (lblStatusName.Text == "รอการพิจารณา(คปภ.)")
                        {
                            lblApprove.Visible = true;
                            ddlApprove.Visible = true;
                            btnSend.Visible = true;
                            btnCancleSend.Visible = true;
                        }
                        else if (lblStatusName.Text == "อนุมัติ(คปภ.)" || lblStatusName.Text == "ไม่อนุมัติ(คปภ.)")
                        {
                            lblApprove.Visible = false;
                            ddlApprove.Visible = false;
                            btnSend.Visible = false;
                            btnCancleSend.Visible = false;
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


                ApplicantBiz biz = new ApplicantBiz();

                string a;
                string s;
                string AS_result;
                string o_result;
                a = ddlStatus.SelectedValue;
               
                if (a != "")
                {
                    var stringValue = a;
                    s= stringValue.Substring(0, 1);
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
                    var CountPage = biz.GetApproveEditApplicant((DTO.RegistrationType)base.UserRegType, txtIDCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, s, resultPage, PageSize, true, base.UserProfile.MemberType.ToString(), AS_result,o_result);
                    
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

                string aaa = base.UserProfile.MemberType.ToString();

                var res = biz.GetApproveEditApplicant((DTO.RegistrationType)base.UserRegType, txtIDCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, s, resultPage, PageSize, false, base.UserProfile.MemberType.ToString(), AS_result, o_result);
                if (res.IsError)
                {
                    btnNextGvSearch.Visible = false;
                    btnPreviousGvSearch.Visible = false;
                    txtNumberGvSearch.Visible = false;
                    txtTotalPage.Visible = false;
                    TXTrowperpage.Visible = false;
                    lblParaPage.Visible = false;
                    rowPerpage.Visible = false;
                    pageGo.Visible = false;
                    div_totalPage.Visible = false;
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
                else if (res != null)
                {
                    if (res.DataResponse.Tables[0].Rows.Count > 0)
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
                        PnlDetailSearchGridView.Visible = true;
                        bludDiv1.Visible = true;
                        div_totalPage.Visible = true;
                        lblTotal.Visible = true;
                        gvDetail.DataSource = res.DataResponse;
                        gvDetail.DataBind();

                        txtNumberGvSearch.Enabled = false;
                        btnPreviousGvSearch.Enabled = false;
                        btnNextGvSearch.Enabled = false;
                        
                    }
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
                    Label lblStatus = (Label)gvDetail.Rows[rowindex].FindControl("lblStatus");
                    Label CompCode = (Label)gvDetail.Rows[rowindex].FindControl("lblCompCode");
                    Label lblCancelReason = (Label)gvDetail.Rows[rowindex].FindControl("lblCancelReason");
                    Label lblOICResult = (Label)gvDetail.Rows[rowindex].FindControl("lblOICResult");
                    Label lblAssoResult = (Label)gvDetail.Rows[rowindex].FindControl("lblAssoResult");
                    Label lblStatusID = (Label)gvDetail.Rows[rowindex].FindControl("lblStatusID");


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
                    lblCompCodeVisibleF.Text = CompCode.Text;
                    lblIDCARDVisibleF.Text = IDCard.Text;
                    lblTestingNOVisibleF.Text = testingNo.Text;
                    lblNewIDCardVisibleF.Text = NewIDCARD.Text;
                    lblNewPreName.Text = NewPrefix.Text;
                    lblNewFirstName.Text = NewFirstName.Text;
                    lblNewLastNames.Text = NewLastName.Text;

                    idcard = IDCard.Text;

                    

                   

                    string oic_result = lblOICResult.Text;
                    string asso_result = lblAssoResult.Text;
                    string status = lblStatusID.Text;
                    string reason = lblCancelReason.Text;

                    if (status == "1" && asso_result == "1" && oic_result=="0")
                        {
                            lblStatusNames.Visible = false;
                            //lblStatusNames.Text = "***รอการพิจารณาจากคปภ.***";

                            PanelApprove.Visible = true;
                        }
                        else if (status == "2" && asso_result == "1" &&  oic_result == "1")
                        {
                            lblStatusNames.Visible = true;
                            lblStatusNames.Text = "***ผ่านการพิจารณาจากคปภ.เรียบร้อย***";

                            

                            txtNewIDCard.Enabled = false;
                            ddlNewPreNameCode.Enabled = false;
                            txtNewNames.Enabled = false;
                            txtNewLastName.Enabled = false;
                            txtRemark.Enabled = false;

                            PanelApprove.Visible = false; ;
                        }
                        else
                        {
                            lblStatusNames.Visible = true;
                            if (reason != "")
                            {
                                lblStatusNames.Text = "***ไม่ผ่านการพิจารณาจากคปภ*** <br> <U>เนื่องจาก</U> "+ reason;
                            }
                            else
                            {
                                lblStatusNames.Text = "***ไม่ผ่านการพิจารณาจากคปภ***";
                            }

                            

                            txtNewIDCard.Enabled = false;
                            ddlNewPreNameCode.Enabled = false;
                            txtNewNames.Enabled = false;
                            txtNewLastName.Enabled = false;
                            txtRemark.Enabled = false;

                            PanelApprove.Visible = false;
                        }
                        //txtExamPlaceCode.Text = DR1["Exam_place_name"].ToString();
                        //txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                        //ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                        //txtNewNames.Text = DR1["new_fname"].ToString();
                        //txtNewLastName.Text = DR1["new_lname"].ToString();
                        //txtRemark.Text = DR1["REMARK"].ToString();

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
            btnGridviewVisibleF();
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
            txtReason.Text = "";
            lblStatusNames.Visible = false;

            

        }
        protected void btnGridviewVisibleF()
        {
            btnPreviousGvSearch.Visible = false;
            btnNextGvSearch.Visible = false;




            
            //txtPaymentNo.Text = string.Empty;
            
            //gvDetail.DataMember = null;
            //gvDetail.DataBind();
            gvDetail.Visible = false;
            btnPreviousGvSearch.Visible = false;
            btnNextGvSearch.Visible = false;
            txtNumberGvSearch.Text = "0";
            txtNumberGvSearch.Visible = true;
            txtTotalPage.Visible = false;
            
            rowPerpage.Text = PageSize.ToString();
            
            //gvSubject.DataMember = null;
            //gvSubject.DataBind();
           
            bludDiv1.Visible = false;
           
        }
        protected void ClearPanelOldANDEdit()
        {

            PanelOldApplicant.Visible = false;
            PanelEditContent.Visible = false;
            txtPreNameCode.Text = "";
            txtNames.Text = "";
            txtLastName.Text = "";
            txtExamPlaceCode.Text = "";
            txtNewIDCard.Text = "";
            ddlNewPreNameCode.SelectedIndex = 0;
            txtNewNames.Text = "";
            txtNewLastName.Text = "";
            txtRemark.Text = "";

            ddlApprove.SelectedIndex = 0;
        }
        
        private void UpdateApplicantChange()
        {

            var biz = new BLL.ApplicantBiz();
            //var res = biz.GetRequestEditApplicant((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.CompCode);
            //DataTable DT = res.DataResponse.Tables[0];
            //DataRow DR = DT.Rows[0];

            DTO.ApplicantChange AppChange = new DTO.ApplicantChange();

            
            AppChange.CHANGE_ID = Convert.ToInt32(lblChangeid.Text);
            AppChange.OIC_USER_ID = base.UserId;
            AppChange.OIC_DATE = DateTime.Today;
            if (ddlApprove.SelectedValue == "1")//อนุมัติ 
            {
                AppChange.OIC_RESULT = 1;//เก็บ Log แล้ว Update AG_Applicant_T
                AppChange.STATUS = 2;//อนุมัติ 

                InsertApplicantTLog();//เก็บ Log
                
                
            }
            else//ไม่อนุมัติ
            {
                AppChange.OIC_RESULT = 2;//ไม่เก็บLog
                AppChange.STATUS = 2;//ไม่อนุมัติ
                if (txtReason.Text == "")
                {
                    txtReason.Text = "ไม่ผ่านการพิจารณาจากคปภ.";
                    AppChange.CANCEL_REASON = txtReason.Text;
                }
                else
                {
                    AppChange.CANCEL_REASON = txtReason.Text;
                }
            }
            
            AppChange.OLD_ID_CARD_NO = lblIDCARDVisibleF.Text;
            AppChange.TESTING_NO = lblTestingNOVisibleF.Text;

            AppChange.NEW_ID_CARD_NO = lblNewIDCardVisibleF.Text;
            AppChange.NEW_PREFIX = Convert.ToDecimal(lblNewPreName.Text);
            AppChange.NEW_FNAME = lblNewFirstName.Text;
            AppChange.NEW_LNAME = lblNewLastNames.Text;
            AppChange.CANCEL_REASON = txtReason.Text;

            var insertAppChange = biz.InsertApplicantChange(AppChange);
            if (insertAppChange.ResultMessage == true)
            {
                var res1 = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, lblIDCARDVisibleF.Text, lblTestingNOVisibleF.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, "", A, Z, false, "", "");
                DataTable DT = res1.DataResponse.Tables[0];
                DataRow DR = DT.Rows[0];

                string status = DR["STATUS"].ToString();
                string assoResult = DR["ASSOCIATION_RESULT"].ToString();
                string oicResult = DR["OIC_RESULT"].ToString();
                string IDCardCreateBy = DR["CREATE_BY"].ToString();
                string OLDidcard = DR["OLD_ID_CARD_NO"].ToString();
                string TestingNO = DR["Testing_no"].ToString();
                string IDCARDAsso=DR["ASSOCIATION_USER_ID"].ToString();


                var sendMail = biz.SendMailAppChange(IDCardCreateBy, TestingNO, OLDidcard);//ส่งเมล์

                //if (oicResult == "2")//ไม่อนุมัติ(ส่งให้สมาคมด้วย)
                //{
                //    var sendMailAsso = biz.SendMailAppChange(IDCARDAsso, TestingNO, OLDidcard);//ส่งเมล์
                //}


                ClearText();
                txtReason.Visible = false;
                lblReason.Visible = false;
                UCModalSuccess.ShowMessageSuccess = "บันทึกสำเร็จ";
                UCModalSuccess.ShowModalSuccess();

                

                
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

                var res = biz.GetApproveEditApplicant((DTO.RegistrationType)base.UserRegType, txtIDCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, "", resultPage, PageSize, false, base.UserProfile.MemberType.ToString(),"","");
                res.DataResponse.Clear();
                res.DataResponse.Reset();
                res.DataResponse.RejectChanges();

            }
            else
            {
                UCModalError.ShowMessageError = "พบข้อผิดพลาด";
                UCModalError.ShowModalError();
            }

        }
        int MaxID = 0;
        protected void InsertApplicantTLog()
        {
            var biz = new BLL.ApplicantBiz();
            var MaxIDLog = biz.GetApplicantTLogMaxID();
            
            if (MaxIDLog.DataResponse.Tables[0].Rows.Count > 0)
            {
                DataTable DT = MaxIDLog.DataResponse.Tables[0];
                DataRow DR = DT.Rows[0];
                string a = DR["applicant_code_log"].ToString();
                if (a == "")
                {
                    MaxID = 1;
                }
                else
                {
                    MaxID = Convert.ToInt32(DR["applicant_code_log"]) + 1;
                }

            }
            var ApplicantT = biz.GetApplicantTtoLog((DTO.RegistrationType)base.UserProfile.MemberType, lblIDCARDVisibleF.Text, lblTestingNOVisibleF.Text, lblCompCodeVisibleF.Text);
            DTO.ApplicantTLog appTLog = new DTO.ApplicantTLog();
            {
                DataTable DTAppT = ApplicantT.DataResponse.Tables[0];
                DataRow DRAppT = DTAppT.Rows[0];
                

                appTLog.APPLICANT_CODE_LOG = MaxID;
                appTLog.CREATE_BY = base.UserId;
                appTLog.CREATE_DATE = DateTime.Today;

                appTLog.APPLICANT_CODE = Convert.ToInt32(DRAppT["APPLICANT_CODE"]);
                appTLog.TESTING_NO = DRAppT["TESTING_NO"].ToString();
                appTLog.EXAM_PLACE_CODE = DRAppT["EXAM_PLACE_CODE"].ToString();
                appTLog.ACCEPT_OFF_CODE=DRAppT["ACCEPT_OFF_CODE"].ToString();
                appTLog.APPLY_DATE = Convert.ToDateTime(DRAppT["APPLY_DATE"]);
                appTLog.ID_CARD_NO=DRAppT["ID_CARD_NO"].ToString();
                appTLog.PRE_NAME_CODE = DRAppT["PRE_NAME_CODE"].ToString();
                appTLog.NAMES = DRAppT["NAMES"].ToString();
                appTLog.LASTNAME = DRAppT["LASTNAME"].ToString();
                appTLog.BIRTH_DATE = Convert.ToDateTime(DRAppT["BIRTH_DATE"]);
                appTLog.SEX=DRAppT["SEX"].ToString();
                appTLog.EDUCATION_CODE = DRAppT["EDUCATION_CODE"].ToString();
                appTLog.ADDRESS1 = DRAppT["ADDRESS1"].ToString();
                appTLog.ADDRESS2 = DRAppT["ADDRESS2"].ToString();
                appTLog.AREA_CODE = DRAppT["AREA_CODE"].ToString();
                appTLog.PROVINCE_CODE=DRAppT["PROVINCE_CODE"].ToString();
                appTLog.ZIPCODE = DRAppT["ZIPCODE"].ToString();
                appTLog.TELEPHONE = DRAppT["TELEPHONE"].ToString();
                appTLog.AMOUNT_TRAN_NO=DRAppT["AMOUNT_TRAN_NO"].ToString();
                appTLog.PAYMENT_NO = DRAppT["PAYMENT_NO"].ToString();
                appTLog.INSUR_COMP_CODE=DRAppT["INSUR_COMP_CODE"].ToString();
                appTLog.ABSENT_EXAM=DRAppT["ABSENT_EXAM"].ToString();
                appTLog.RESULT = DRAppT["RESULT"].ToString();
                if (DRAppT["EXPIRE_DATE"].ToString() != "") { appTLog.EXPIRE_DATE = Convert.ToDateTime(DRAppT["EXPIRE_DATE"]); }
              //  appTLog.EXPIRE_DATE = Convert.ToDateTime(DRAppT["EXPIRE_DATE"]);
                appTLog.LICENSE = DRAppT["LICENSE"].ToString();
                appTLog.CANCEL_REASON = DRAppT["CANCEL_REASON"].ToString();
                appTLog.RECORD_STATUS = DRAppT["RECORD_STATUS"].ToString();
                appTLog.USER_ID = DRAppT["USER_ID"].ToString();
                appTLog.USER_DATE = Convert.ToDateTime(DRAppT["USER_DATE"]);
                appTLog.EXAM_STATUS = DRAppT["EXAM_STATUS"].ToString();
                appTLog.UPLOAD_GROUP_NO = DRAppT["UPLOAD_GROUP_NO"].ToString();
                appTLog.HEAD_REQUEST_NO = DRAppT["HEAD_REQUEST_NO"].ToString();
                appTLog.GROUP_REQUEST_NO = DRAppT["GROUP_REQUEST_NO"].ToString();
                appTLog.UPLOAD_BY_SESSION = DRAppT["UPLOAD_BY_SESSION"].ToString();
                appTLog.ID_ATTACH_FILE = DRAppT["ID_ATTACH_FILE"].ToString();

                var insertAppTLog = biz.InsertApplicantTLog(appTLog);
                if (insertAppTLog.ResultMessage == true)
                {
                    UCModalSuccess.ShowMessageSuccess = "บันทึกลงLog";
                    UCModalSuccess.ShowModalSuccess();
                    ClearText();
                    txtReason.Visible = false;
                    txtReason.Visible = false;
                }
                else
                {
                    UCModalError.ShowMessageError = "พบข้อผิดพลาด";
                    UCModalError.ShowModalError();
                }
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
                    //txtTotalPage.Text = (total_row_count > 0) ? Convert.ToString(Paggge) : "0";
                    txtTotalPage.Text = (total_row_count > 0) ? Convert.ToString(Paggge) : "1";
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
            if (ddlApprove.SelectedValue == "2")
            {
                txtReason.Visible = true;
                lblReason.Visible = true;
            }
            else
            {
                txtReason.Visible = false;
                lblReason.Visible = false;
                txtReason.Text = "";
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