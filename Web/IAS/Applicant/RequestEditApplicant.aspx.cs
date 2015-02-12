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
using IAS.MasterPage;
using IAS.DTO;
using System.Text;
using IAS.BLL.AttachFilesIAS;
using IAS.BLL.AttachFilesIAS.States;

namespace IAS.Applicant
{
    public partial class RequestEditApplicant : basepage
    {
        #region Privarte & Public Session
        Utils.ControlHelper ctrlHelper = new ControlHelper();
        List<DTO.AttachFileApplicantChange> attachFilesApp = new List<AttachFileApplicantChange>();
        public String doc1 = string.Empty;
        private IList<DTO.DataItem> _documentTypes;
        private int A = 1;
        private int Z = 20;
        private string id_card_no = string.Empty;
        private int changeid;
        private int MaxID = 0;

        public Site1 MasterPage
        {
            get
            {
                return (this.Page.Master as Site1);
            }
        }

        public IList<DTO.DataItem> DocumentTypes
        {
            get { return _documentTypes; }
            set { _documentTypes = value; }
        }

        public string DocSession
        {
            get
            {
                return Session["DocSession"] == null ? string.Empty : Session["DocSession"].ToString();
            }
            set
            {
                Session["DocSession"] = value;
            }

        }
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
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //ddlAttrachFile.Items.Insert(0, new ListItem("-----เลือก-----", string.Empty));
            
            if (!IsPostBack)
            {
                this.DocSession = "APP";
                InitAttachFileControl();
                lblPreNameCode.Visible = false;
                txtPreNameCode.Visible = false;
                lblNames.Visible = false;
                txtNames.Visible = false;
                lblLastName.Visible = false;
                txtLastName.Visible = false;
                pnlAttachFileControl.Style.Add("display", "none");
                
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

        public void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            this.ucAttachFileControl1.DocumentTypes = ls;
        }

        public void GetAttachFilesAllType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            this.ucAttachFileControl1.DocumentTypeAll = ls;
        }

        public void InitAttachFileControl()
        {
            GetAttachFilesType();
            ucAttachFileControl1.RegisterationId = this.UserProfile.Id;
            //ucAttachFileControl1.RegisterationId = base.UserId;
        }
  
        private void GetTileNameBySex(string sex)
        {
            DataCenterBiz biz = new DataCenterBiz();
            DTO.ResponseService<DTO.TitleName[]> resTitle = biz.GetTitleNameFromSex(sex);
            if (resTitle.DataResponse.Count() > 0)
            {
                ddlNewPreNameCode.DataTextField = "FULL_NAME";
                ddlNewPreNameCode.DataValueField = "PRE_CODE";
                ddlNewPreNameCode.DataSource = resTitle.DataResponse;
                ddlNewPreNameCode.DataBind();

            }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtTestingNo.Text)&& !string.IsNullOrEmpty(txtIdCard.Text))
                {
                    int idcard=txtIdCard.Text.Length;
                    int Testing=txtTestingNo.Text.Length;
                    if (idcard < 13 && Testing < 6)
                    {

                        this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสรอบสอบ และรหัสประจำตัวประชาชนให้ครบถ้วน";
                        this.MasterPage.ModelError.ShowModalError();
                    }
                    else if (idcard < 13 || Testing < 6)
                    {
                        if (idcard < 13)
                        {
                            this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสประจำตัวประชาชนให้ครบถ้วน";
                            this.MasterPage.ModelError.ShowModalError();
                        }
                        if (Testing < 6)
                        {
                            this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสรอบสอบให้ครบถ้วน";
                            this.MasterPage.ModelError.ShowModalError();
                        }
                    }
                    else//BindData
                    {
                        txtRemark.Text = "";
                        lblStatusName.Visible = false;
                        btnAddNew.Visible = false;
                        ucAttachFileControl1.TextRemark.Text = "";
                        //ucAttachFileControl1.AttachFiles.Clear();
                        //ucAttachFileControl1.DataBind();
                        
                        int MemType = base.UserProfile.MemberType;
                        if (MemType == 2 || MemType == 3 || MemType == 4|| MemType==6)
                        {
                            BindData();

                            if (PanelSend.Visible == true)
                            {
                                this.ApplicantView = "";
                                ucAttachFileControl1.GetDocReqApplicantName();
                            }
                            else
                            {
                                this.ApplicantView = "AppView";
                                ucAttachFileControl1.GetDocReqApplicantName();
                            }
                        }
                    }
                }
                else 
                {
                    if (string.IsNullOrEmpty(txtTestingNo.Text) && string.IsNullOrEmpty(txtIdCard.Text))
                    {
                        this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสรอบสอบ และรหัสประจำตัวประชาชน";
                        this.MasterPage.ModelError.ShowModalError();
                    }
                    else if (string.IsNullOrEmpty(txtIdCard.Text))
                    {
                        this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสประจำตัวประชาชน";
                        this.MasterPage.ModelError.ShowModalError();
                    }
                    else 
                    {
                        this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสรอบสอบ";
                        this.MasterPage.ModelError.ShowModalError();
                    }
                }
            }
            catch
            {
            
            }
        }
        protected void BindData()
        {
            try
            {
                var biz = new BLL.ApplicantBiz();

                string compcode = base.UserProfile.CompCode;
                

                var res = biz.GetRequestEditApplicant((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, txtTestingNo.Text,base.UserProfile.CompCode);
                if (!res.IsError)
                {
                    if (!string.IsNullOrEmpty(txtTestingNo.Text) && !string.IsNullOrEmpty(txtIdCard.Text))
                    {
                        if(res.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            DataTable DT = res.DataResponse.Tables[0];
                            DataRow DR = DT.Rows[0];

                            this.GetTileNameBySex(DR["sex"].ToString());
                            
                            txtPreNameCode.Text = DR["Name"].ToString();
                            txtNames.Text = DR["Names"].ToString();
                            txtLastName.Text = DR["LastName"].ToString();
                            txtExamPlaceCode.Text = DR["Exam_Place_Name"].ToString();
                            txtNewIDCard.Text = DR["ID_CARD_No"].ToString();
                            txtNewNames.Text = DR["Names"].ToString();
                            txtNewLastName.Text = DR["LastName"].ToString();
                            id_card_no = DR["id_card_no"].ToString();
                            ddlNewPreNameCode.SelectedValue = DR["pre_name_code"].ToString();
                            lblTestingNoVisibleF.Text = DR["testing_no"].ToString();

                            var res1 = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, "", "", A, Z, false, "", "");
                            if (res1.DataResponse.Tables[0].Rows.Count == 1)//NewData
                            {
                                DataTable DT1 = res1.DataResponse.Tables[0];
                                DataRow DR1 = DT1.Rows[0];
                                //DataRow DR1 = DT1.Rows[res1.DataResponse.Tables[0].Rows.Count - 1];

                                string asso_result = DR1["ASSOCIATION_RESULT"].ToString();
                                string oic_result = DR1["OIC_RESULT"].ToString();
                                string status = DR1["STATUS"].ToString();
                                string reason = DR1["CANCEL_REASON"].ToString();
                                


                                if (status == "0" && asso_result == "0" && oic_result == "0")//ผ่าน
                                {
                                    lblStatusName.Visible = true;
                                    lblStatusName.Text = "***รอการพิจารณาจากสมาคม***";

                                    txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                    txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                    ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                    txtNewNames.Text = DR1["new_fname"].ToString();
                                    txtNewLastName.Text = DR1["new_lname"].ToString();
                                    txtRemark.Text = DR1["REMARK"].ToString();
                                    changeid = Convert.ToInt32(DR1["change_id"]);

                                    txtNewIDCard.Enabled = false;
                                    ddlNewPreNameCode.Enabled = false;
                                    txtNewNames.Enabled = false;
                                    txtNewLastName.Enabled = false;
                                    txtRemark.Enabled = false;

                                    PanelSend.Visible = false;
                                    
                                }
                                else if (status == "1" && asso_result == "1" && oic_result == "0")
                                {
                                    lblStatusName.Visible = true;
                                    lblStatusName.Text = "***รอการพิจารณาจากคปภ.***";

                                    txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                    txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                    ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                    txtNewNames.Text = DR1["new_fname"].ToString();
                                    txtNewLastName.Text = DR1["new_lname"].ToString();
                                    txtRemark.Text = DR1["REMARK"].ToString();
                                    changeid = Convert.ToInt32(DR1["change_id"]);

                                    txtNewIDCard.Enabled = false;
                                    ddlNewPreNameCode.Enabled = false;
                                    txtNewNames.Enabled = false;
                                    txtNewLastName.Enabled = false;
                                    txtRemark.Enabled = false;

                                    PanelSend.Visible = false;
                                    
                                }
                                else if (status == "1" && asso_result == "2" && oic_result == "0")
                                {
                                    lblStatusName.Visible = true;
                                    if (reason != "")
                                    {
                                        lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากสมาคม*** <br> <U>เนื่องจาก</U> " + reason;
                                    }
                                    else
                                    {
                                        lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากสมาคม*** <br> กรุณาแก้ไขข้อมูลผู้สมัครสอบ";
                                    }

                                    txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                    txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                    ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                    txtNewNames.Text = DR1["new_fname"].ToString();
                                    txtNewLastName.Text = DR1["new_lname"].ToString();
                                    txtRemark.Text = DR1["REMARK"].ToString();
                                    changeid = Convert.ToInt32(DR1["change_id"]);

                                    txtNewIDCard.Enabled = false;
                                    ddlNewPreNameCode.Enabled = false;
                                    txtNewNames.Enabled = false;
                                    txtNewLastName.Enabled = false;
                                    txtRemark.Enabled = false;

                                    PanelSend.Visible = false;
                                    btnAddNew.Visible = true;
                                    btnAddNew.Enabled = true;
                                    
                                }
                                else if (status == "2" && oic_result == "1" && oic_result == "1")
                                {
                                    lblStatusName.Visible = true;
                                    lblStatusName.Text = "***ผ่านการพิจารณาจากคปภ.เรียบร้อย***";

                                    txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                    txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                    ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                    txtNewNames.Text = DR1["new_fname"].ToString();
                                    txtNewLastName.Text = DR1["new_lname"].ToString();
                                    txtRemark.Text = DR1["REMARK"].ToString();
                                    changeid = Convert.ToInt32(DR1["change_id"]);

                                    txtNewIDCard.Enabled = false;
                                    ddlNewPreNameCode.Enabled = false;
                                    txtNewNames.Enabled = false;
                                    txtNewLastName.Enabled = false;
                                    txtRemark.Enabled = false;

                                    PanelSend.Visible = false;


                                    #region BindDataที่ Approveผ่านoicแล้ว (ส่งเรื่องใหม่)
                                    var res5 = biz.GetApproveAppForStatus((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, "2", "1", "1");
                                    if (res5.DataResponse.Tables[0].Rows.Count > 0)//BindDataที่ Approveผ่านoicแล้ว
                                    {
                                        DataTable DT5 = res5.DataResponse.Tables[0];
                                        DataRow DR5 = DT5.Rows[0];
                                        if (DT5.Rows.Count >= 1)
                                        {
                                            string create_date = string.Format("{0:dd/MM/yyyy}", DR5["create_date"]);
                                            string oic_date = string.Format("{0:dd/MM/yyyy}", DR5["oic_date"]);

                                            string status1 = DR5["status"].ToString();
                                            string asso_result1 = DR5["association_result"].ToString();
                                            string oic_result1 = DR5["oic_result"].ToString();
                                            lblStatusName.Visible = true;
                                            lblStatusName.Text = "ผ่านการอนุมัติเรียบร้อย <br> คุณได้แก้ไขข้อมูลผู้สมัครสอบล่าสุดเมื่อวันที่ " + create_date + " มีผลการใช้งานเมื่อวันที่ " + oic_date;
                                            //PanelSend.Visible = true;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    #endregion
                                    btnAddNew.Visible = true;
                                    btnAddNew.Enabled = true;
                                    
                                }

                                else if (status == "2" && asso_result == "1" && oic_result == "2")
                                {
                                    lblStatusName.Visible = true;

                                    if (reason != "")
                                    {
                                        lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากคปภ*** <br> <U>เนื่องจาก</U> " + reason;
                                    }
                                    else
                                    {
                                        lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากคปภ***";
                                    }
                                    txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                    txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                    ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                    txtNewNames.Text = DR1["new_fname"].ToString();
                                    txtNewLastName.Text = DR1["new_lname"].ToString();
                                    txtRemark.Text = DR1["REMARK"].ToString();
                                    changeid = Convert.ToInt32(DR1["change_id"]);
                                    txtNewIDCard.Enabled = false;
                                    ddlNewPreNameCode.Enabled = false;
                                    txtNewNames.Enabled = false;
                                    txtNewLastName.Enabled = false;
                                    txtRemark.Enabled = false;

                                    PanelSend.Visible = false;
                                    btnAddNew.Visible = true;
                                    btnAddNew.Enabled = true;
                                    
                                }
                                else
                                {


                                }
                            }
                            else if (res1.DataResponse.Tables[0].Rows.Count > 1)//oldData
                            {
                                //var res2 = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, "0", A, Z, false, "0", "0");
                                if (res1.DataResponse.Tables[0].Rows.Count > 0)
                                {
                                    DataTable DT1 = res1.DataResponse.Tables[0];
                                    DataRow DR1 = DT1.Rows[res1.DataResponse.Tables[0].Rows.Count - 1];
                                    string asso_result = DR1["ASSOCIATION_RESULT"].ToString();
                                    string oic_result = DR1["OIC_RESULT"].ToString();
                                    string status = DR1["STATUS"].ToString();
                                    string reason = DR1["CANCEL_REASON"].ToString();

                                    if (status == "0" && asso_result == "0" && oic_result == "0")//ผ่าน
                                    {
                                        lblStatusName.Visible = true;
                                        lblStatusName.Text = "***รอการพิจารณาจากสมาคม***";

                                        txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                        txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                        ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                        txtNewNames.Text = DR1["new_fname"].ToString();
                                        txtNewLastName.Text = DR1["new_lname"].ToString();
                                        txtRemark.Text = DR1["REMARK"].ToString();
                                        changeid = Convert.ToInt32(DR1["change_id"]);

                                        txtNewIDCard.Enabled = false;
                                        ddlNewPreNameCode.Enabled = false;
                                        txtNewNames.Enabled = false;
                                        txtNewLastName.Enabled = false;
                                        txtRemark.Enabled = false;

                                        PanelSend.Visible = false;
                                        
                                    }
                                    else if (status == "1" && asso_result == "1" && oic_result == "0")
                                    {
                                        lblStatusName.Visible = true;
                                        lblStatusName.Text = "***รอการพิจารณาจากคปภ.***";

                                        txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                        txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                        ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                        txtNewNames.Text = DR1["new_fname"].ToString();
                                        txtNewLastName.Text = DR1["new_lname"].ToString();
                                        txtRemark.Text = DR1["REMARK"].ToString();
                                        changeid = Convert.ToInt32(DR1["change_id"]);

                                        txtNewIDCard.Enabled = false;
                                        ddlNewPreNameCode.Enabled = false;
                                        txtNewNames.Enabled = false;
                                        txtNewLastName.Enabled = false;
                                        txtRemark.Enabled = false;

                                        PanelSend.Visible = false;
                                        
                                    }
                                    else if (status == "1" && asso_result == "2" && oic_result == "0")
                                    {
                                        lblStatusName.Visible = true;
                                        if (reason != "")
                                        {
                                            lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากสมาคม*** <br> <U>เนื่องจาก</U> " + reason;
                                        }
                                        else
                                        {
                                            lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากสมาคม*** <br> กรุณาแก้ไขข้อมูลผู้สมัครสอบ";
                                        }

                                        txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                        txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                        ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                        txtNewNames.Text = DR1["new_fname"].ToString();
                                        txtNewLastName.Text = DR1["new_lname"].ToString();
                                        txtRemark.Text = DR1["REMARK"].ToString();
                                        changeid = Convert.ToInt32(DR1["change_id"]);

                                        txtNewIDCard.Enabled = false;
                                        ddlNewPreNameCode.Enabled = false;
                                        txtNewNames.Enabled = false;
                                        txtNewLastName.Enabled = false;
                                        txtRemark.Enabled = false;

                                        PanelSend.Visible = false;
                                        btnAddNew.Visible = true;
                                        btnAddNew.Enabled = true;
                                        
                                    }
                                    else if (status == "2" && oic_result == "1" && oic_result == "1")
                                    {
                                        lblStatusName.Visible = true;
                                        lblStatusName.Text = "***ผ่านการพิจารณาจากคปภ.เรียบร้อย***";

                                        txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                        txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                        ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                        txtNewNames.Text = DR1["new_fname"].ToString();
                                        txtNewLastName.Text = DR1["new_lname"].ToString();
                                        txtRemark.Text = DR1["REMARK"].ToString();
                                        changeid = Convert.ToInt32(DR1["change_id"]);

                                        txtNewIDCard.Enabled = false;
                                        ddlNewPreNameCode.Enabled = false;
                                        txtNewNames.Enabled = false;
                                        txtNewLastName.Enabled = false;
                                        txtRemark.Enabled = false;

                                        PanelSend.Visible = false;

                                        #region BindDataที่ Approveผ่านoicแล้ว (ส่งเรื่องใหม่)
                                        var res5 = biz.GetApproveAppForStatus((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, "2", "1", "1");
                                        if (res5.DataResponse.Tables[0].Rows.Count > 0)//BindDataที่ Approveผ่านoicแล้ว
                                        {
                                            DataTable DT5 = res5.DataResponse.Tables[0];
                                            DataRow DR5 = DT5.Rows[0];
                                            if (DT5.Rows.Count >= 1)
                                            {
                                                string create_date = string.Format("{0:dd/MM/yyyy}", DR5["create_date"]);
                                                string oic_date = string.Format("{0:dd/MM/yyyy}", DR5["oic_date"]);

                                                string status1 = DR5["status"].ToString();
                                                string asso_result1 = DR5["association_result"].ToString();
                                                string oic_result1 = DR5["oic_result"].ToString();
                                                lblStatusName.Visible = true;
                                                lblStatusName.Text = "ผ่านการอนุมัติเรียบร้อย <br> คุณได้แก้ไขข้อมูลผู้สมัครสอบล่าสุดเมื่อวันที่ " + create_date + " มีผลการใช้งานเมื่อวันที่ " + oic_date;
                                            }
                                            else
                                            {
                                                
                                            }
                                        }
                                        #endregion
                                        btnAddNew.Visible = true;
                                        btnAddNew.Enabled = true;
                                        
                                    }

                                    else if (status == "2" && asso_result == "1" && oic_result == "2")
                                    {
                                        lblStatusName.Visible = true;

                                        if (reason != "")
                                        {
                                            lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากคปภ*** <br> <U>เนื่องจาก</U> " + reason;
                                        }
                                        else
                                        {
                                            lblStatusName.Text = "***ไม่ผ่านการพิจารณาจากคปภ***";
                                        }

                                        txtExamPlaceCode.Text = DR["Exam_place_name"].ToString();
                                        txtNewIDCard.Text = DR1["New_ID_CARD_NO"].ToString();
                                        ddlNewPreNameCode.SelectedValue = DR1["new_prefix"].ToString();
                                        txtNewNames.Text = DR1["new_fname"].ToString();
                                        txtNewLastName.Text = DR1["new_lname"].ToString();
                                        txtRemark.Text = DR1["REMARK"].ToString();
                                        changeid = Convert.ToInt32(DR1["change_id"]);

                                        txtNewIDCard.Enabled = false;
                                        ddlNewPreNameCode.Enabled = false;
                                        txtNewNames.Enabled = false;
                                        txtNewLastName.Enabled = false;
                                        txtRemark.Enabled = false;

                                        PanelSend.Visible = false;
                                        btnAddNew.Visible = true;
                                        btnAddNew.Enabled = true;
                                        
                                    }
                                    else
                                    {
                                    }
                                }
                                else
                                {    
                                }
                            }
                            else// res1==0
                            {
                                PanelSend.Visible = true;
                                txtNewIDCard.Enabled = true;
                                ddlNewPreNameCode.Enabled = true;
                                txtNewNames.Enabled = true;
                                txtNewLastName.Enabled = true;
                                txtRemark.Enabled = true;
                                this.ApplicantView = "";
                            }
                            PanelEditContent.Visible = true;
                            ucAttachFileControl1EnabledTrue();
                            GetAttatchRegisterationFiles();//BindDataAttachFile
                            lblPreNameCode.Visible = true;
                            txtPreNameCode.Visible = true;
                            lblNames.Visible = true;
                            txtNames.Visible = true;
                            lblLastName.Visible = true;
                            txtLastName.Visible = true;
                            pnlAttachFileControl.Style.Add("display", "block");
                        }
                        else
                        {
                            this.MasterPage.ModelError.ShowMessageError = "ไม่พบข้อมูลผู้สมัครสอบ";
                            this.MasterPage.ModelError.ShowModalError();
                            PanelEditContent.Visible = false;
                            PanelSend.Visible = false;
                            pnlAttachFileControl.Style.Add("display", "none");
                            lblPreNameCode.Visible = false;
                            txtPreNameCode.Visible = false;
                            lblNames.Visible = false;
                            txtNames.Visible = false;
                            lblLastName.Visible = false;
                            txtLastName.Visible = false;
                            lblStatusName.Visible = false;
                            btnAddNew.Visible = false;



                        } 
                    }
                    if (base.UserProfile.MemberType == 4 || base.UserProfile.MemberType == 6)
                    {
                        btnSend.Visible = false;
                        btnCancleSend.Visible = false;
                    }
                }
            }
            catch(Exception ex)
            {
            }
        }
        public void ucAttachFileControl1EnabledTrue()
        {
            ucAttachFileControl1.TextRemark.Enabled = true;
            ucAttachFileControl1.PnlAttachFiles.Enabled = true;
            ucAttachFileControl1.DropDownDocumentType.Enabled = true;
            ucAttachFileControl1.ButtonUpload.Enabled = true;
            ucAttachFileControl1.TextRemark.Text = "";
            this.ucAttachFileControl1.EnableUpload(true);
        }
        public void ucAttachFileControl1EnabledFalse()
        {
            ucAttachFileControl1.TextRemark.Enabled = false;
            ucAttachFileControl1.PnlAttachFiles.Enabled = false;
            ucAttachFileControl1.DropDownDocumentType.Enabled = false;
            ucAttachFileControl1.ButtonUpload.Enabled = false;
            this.ucAttachFileControl1.EnableUpload(false);
            ucAttachFileControl1.TextRemark.Text = "";

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
        }
        public void GetAttatchRegisterationFiles()
        {
            ApplicantBiz biz = new BLL.ApplicantBiz();
            DTO.ResponseService<DTO.AttachFileApplicantChangeEntity[]> res = biz.GetAttatchFilesAppChangeByIDCard(txtIdCard.Text.Trim(), changeid);
            //var list = res.DataResponse.ToList();
            if (res.DataResponse.Count() > 0)
            {
                IList<BLL.AttachFilesIAS.AttachFile> ls = BLL.AttachFilesIAS.AttachFileMapper.ConvertToAttachFilesApplicantView(res.DataResponse.ToList());
                ucAttachFileControl1.AttachFiles = ls.ToList();
                this.GetAttachFilesAllType();
                ucAttachFileControl1.GridAttachFiles.DataSource = ls.ToList();
                ucAttachFileControl1.GridAttachFiles.DataBind();
                ucAttachFileControl1EnabledFalse();
            }
            else
            {
                this.GetAttachFilesAllType();
                ucAttachFileControl1.AttachFiles.Clear();
                ucAttachFileControl1.DataBind();
            }
            UpdatePanelSearch.Update();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearText();
            lblStatusName.Visible = false;
            btnAddNew.Visible = false;
            ClearGridAttachFile();
            PanelSend.Visible = false;
            pnlAttachFileControl.Style.Add("display", "none");
        }

        private void ClearGridAttachFile()
        {
            this.GetAttachFilesAllType();
            ucAttachFileControl1.TextRemark.Text = "";
            ucAttachFileControl1.AttachFiles.Clear();
            ucAttachFileControl1.DataBind();
            
        }

        protected void ClearText()
        {
            txtTestingNo.Text = "";
            txtIdCard.Text = "";
            txtPreNameCode.Text = "";
            txtNames.Text = "";
            txtLastName.Text = "";
            txtExamPlaceCode.Text = "";
            txtNewIDCard.Text = "";
            if (ddlNewPreNameCode.Visible == true)
            {
                ddlNewPreNameCode.SelectedIndex = 0;
            }
            txtNewNames.Text = "";
            txtNewLastName.Text = "";
            txtRemark.Text = "";
            PanelEditContent.Visible = false;
            PanelSend.Visible = false;
            lblPreNameCode.Visible = false;
            txtPreNameCode.Visible = false;
            lblNames.Visible = false;
            txtNames.Visible = false;
            lblLastName.Visible = false;
            txtLastName.Visible = false;
            lblStatusName.Visible = false;
        }

        protected void btnCancleSend_Click(object sender, EventArgs e)
        {
            if (ucAttachFileControl1.DropDownDocumentType.Enabled == false)
            {
                btnCancel_Click(sender, e);
            }
            else
            {
                #region bindDataAppT
                ApplicantBiz biz = new ApplicantBiz();
                var res = biz.GetRequestEditApplicant((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.CompCode);
                if (res.DataResponse.Tables[0].Rows.Count > 0)
                {
                    DataTable DT = res.DataResponse.Tables[0];
                    DataRow DR = DT.Rows[0];

                    txtNewIDCard.Text = DR["ID_CARD_No"].ToString();
                    txtNewNames.Text = DR["Names"].ToString();
                    txtNewLastName.Text = DR["LastName"].ToString();
                    ddlNewPreNameCode.SelectedValue = DR["pre_name_code"].ToString();
                    txtRemark.Text = "";
                    ClearGridAttachFile();
                    ucAttachFileControl1EnabledTrue();
                }
                #endregion bindDataAppT

                if (PanelSend.Visible == true)
                {
                    ucAttachFileControl1.GetDocReqApplicantName();
                }
                pnlAttachFileControl.Style.Add("display", "block");
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {

            
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            DTO.ResponseMessage<bool> resInsertFiles = new ResponseMessage<bool>();
            try
            {
                var biz = new BLL.ApplicantBiz();
                if (ucAttachFileControl1.GridAttachFiles.Rows.Count >= 0)
                {
                    String resDoc = ucAttachFileControl1.ValidDocRequire();
                    if (ucAttachFileControl1.GridAttachFiles.Rows.Count == 0)
                    {
                        
                        this.MasterPage.ModelError.ShowMessageError = "กรุณาแนบเอกสาร";
                        this.MasterPage.ModelError.ShowModalError();
                        return;
                    }
                    else if (!String.IsNullOrEmpty(resDoc))
                    {
                        this.MasterPage.ModelError.ShowMessageError = resDoc;
                        this.MasterPage.ModelError.ShowModalError();
                        return;

                    }
                    //else
                    //{
                    //    for (int i = 0; i < ucAttachFileControl1.GridAttachFiles.Rows.Count; i++)
                    //    {
                    //        Label lblDocumentCode = (Label)ucAttachFileControl1.GridAttachFiles.Rows[i].FindControl("lblDocumentCode");
                    //        if (lblDocumentCode.Text == "01")
                    //        {
                    //            break;
                    //        }
                    //        else //if (lblDocumentCode.Text != "01")
                    //        {

                    //            this.MasterPage.ModelError.ShowMessageError = "กรุณาแนบเอกสารสำเนาบัตรประจำตัวประชาชน";
                    //            this.MasterPage.ModelError.ShowModalError();
                    //            return;
                    //        }

                    //    }
                    //}
                }
                if (txtNewIDCard.Text.Length == 13 && lblTestingNoVisibleF.Text != "")
                {

                    var CheckNewIDcard = biz.GetCheckIDAppT(txtNewIDCard.Text, lblTestingNoVisibleF.Text, base.UserProfile.CompCode);

                    if (txtIdCard.Text != txtNewIDCard.Text)
                    {

                        DataTable DT = CheckNewIDcard.DataResponse.Tables[0];
                        DataRow DR = DT.Rows[0];
                        int a = Convert.ToInt16(DR["count"]);
                        if (a != 0)
                        {
                            this.MasterPage.ModelError.ShowMessageError = "รหัสประจำตัวประชาชนซ้ำ!!!กับรหัสประจำตัวประชาชนในรอบสอบเดียวกัน";
                            this.MasterPage.ModelError.ShowModalError();

                            txtNewIDCard.Focus();
                        }
                        else
                        {
                            var res = biz.GetApplicantChangeMaxID();
                            if (!res.IsError)
                            {

                                DataTable DT1 = res.DataResponse.Tables[0];
                                DataRow DR1 = DT1.Rows[0];
                                string count = DR1["CHANGE_ID"].ToString();

                                if (!string.IsNullOrEmpty(txtNewIDCard.Text) && !string.IsNullOrEmpty(txtNewNames.Text) && !string.IsNullOrEmpty(txtNewLastName.Text))
                                {
                                    //var res1 = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, "0", A, Z, false, "0", "0");

                                    if (count == "")
                                    //if (res.DataResponse.Tables[0].Rows.Count > 0)//AddNewData
                                    {
                                        MaxID = 1;
                                    }
                                    else
                                    {
                                        
                                        MaxID = Convert.ToInt32(DR1["CHANGE_ID"]) + 1;
                                    }


                                    resInsertFiles = this.InsertAttachFileApplicantChange(); //Insert Attach Files
                                    if (resInsertFiles.ResultMessage == true)
                                    {
                                        this.MasterPage.ModelSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                                        this.MasterPage.ModelSuccess.ShowModalSuccess();
                                        
                                    }
                                    else
                                    {
                                        this.MasterPage.ModelError.ShowMessageError = "พบข้อผิดพลาด";
                                        this.MasterPage.ModelError.ShowModalError();
                                        return;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var res = biz.GetApplicantChangeMaxID();
                        if (!res.IsError)
                        {
                            var app = res.DataResponse;

                            if (!string.IsNullOrEmpty(txtNewIDCard.Text) && !string.IsNullOrEmpty(txtNewNames.Text) && !string.IsNullOrEmpty(txtNewLastName.Text))
                            {
                                //var res1 = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, base.UserProfile.CompCode, "0", A, Z, false, "0", "0");
                                DataTable DT = res.DataResponse.Tables[0];
                                DataRow DR = DT.Rows[0];
                                string count = DR["CHANGE_ID"].ToString();
                                if(count == "")
                                //if (res.DataResponse.Tables[0].Rows.Count > 0)//AddNewData
                                {
                                    MaxID = 1;
                                    
                                }

                                else
                                {
                                    MaxID = Convert.ToInt32(DR["CHANGE_ID"]) + 1;
                                }
                                //}
                                
                                resInsertFiles = this.InsertAttachFileApplicantChange(); //Insert Attach Files
                                if (resInsertFiles.ResultMessage == true)
                                {
                                    this.MasterPage.ModelSuccess.ShowMessageSuccess = "บันทึกข้อมูลเรียบร้อย";
                                    this.MasterPage.ModelSuccess.ShowModalSuccess();
                                }
                                else
                                {
                                    this.MasterPage.ModelError.ShowMessageError = "พบข้อผิดพลาด";
                                    this.MasterPage.ModelError.ShowModalError();
                                    return;

                                }

                            }
                        }
                    }
                }
                else
                {
                    this.MasterPage.ModelError.ShowMessageError = "กรุณากรอกรหัสประจำตัวประชาชนที่ต้องการเปลี่ยนให้ครบถ้วน";
                    this.MasterPage.ModelError.ShowModalError();
                    return;
                }
            }
            catch
            {

            }
        }

        private DTO.ApplicantChange GetAppChangeDetail()
        {
            ApplicantBiz biz = new BLL.ApplicantBiz();
            DTO.ApplicantChange AppChange = new DTO.ApplicantChange();
            try
            {
                var res = biz.GetRequestEditApplicant((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.CompCode);
                DataTable DT = res.DataResponse.Tables[0];
                DataRow DR = DT.Rows[0];
                if (base.UserProfile.MemberType == DTO.RegistrationType.Insurance.GetEnumValue())
                {
                    //Mappingto Ent
                    AppChange.CHANGE_ID = MaxID;
                    AppChange.COMP_CODE = base.UserProfile.CompCode;
                    AppChange.TESTING_NO = txtTestingNo.Text;
                    AppChange.OLD_ID_CARD_NO = txtIdCard.Text;
                    AppChange.OLD_PREFIX = Convert.ToDecimal(DR["pre_name_code"].ToString());
                    AppChange.OLD_FNAME = txtNames.Text;
                    AppChange.OLD_LNAME = txtLastName.Text;
                    AppChange.NEW_ID_CARD_NO = txtNewIDCard.Text;
                    AppChange.NEW_PREFIX = Convert.ToDecimal(ddlNewPreNameCode.SelectedValue);
                    AppChange.NEW_FNAME = txtNewNames.Text;
                    AppChange.NEW_LNAME = txtNewLastName.Text;
                    AppChange.REMARK = txtRemark.Text;
                    AppChange.STATUS = 0;
                    AppChange.CREATE_BY = base.UserId;
                    AppChange.CREATE_DATE = DateTime.Today;
                    AppChange.ASSOCIATION_USER_ID = null;
                    AppChange.ASSOCIATION_DATE = null;
                    AppChange.ASSOCIATION_RESULT = 0;
                    AppChange.OIC_USER_ID = null;
                    AppChange.OIC_DATE = null;
                    AppChange.OIC_RESULT = 0;

                    AppChange.CANCEL_REASON = null;
                }
                else//แก้ไขโดยสมาคม
                {
                    AppChange.CHANGE_ID = MaxID;
                    AppChange.COMP_CODE = base.UserProfile.CompCode;
                    AppChange.TESTING_NO = txtTestingNo.Text;
                    AppChange.OLD_ID_CARD_NO = txtIdCard.Text;
                    AppChange.OLD_PREFIX = Convert.ToDecimal(DR["pre_name_code"].ToString());
                    AppChange.OLD_FNAME = txtNames.Text;
                    AppChange.OLD_LNAME = txtLastName.Text;
                    AppChange.NEW_ID_CARD_NO = txtNewIDCard.Text;
                    AppChange.NEW_PREFIX = Convert.ToDecimal(ddlNewPreNameCode.SelectedValue);
                    AppChange.NEW_FNAME = txtNewNames.Text;
                    AppChange.NEW_LNAME = txtNewLastName.Text;
                    AppChange.REMARK = txtRemark.Text;
                    AppChange.STATUS = 1;
                    AppChange.CREATE_BY = base.UserId;
                    AppChange.CREATE_DATE = DateTime.Today;
                    AppChange.ASSOCIATION_USER_ID = base.UserId;
                    AppChange.ASSOCIATION_DATE = DateTime.Today;
                    AppChange.ASSOCIATION_RESULT = 1;
                    AppChange.OIC_USER_ID = null;
                    AppChange.OIC_DATE = null;
                    AppChange.OIC_RESULT = 0;

                    AppChange.CANCEL_REASON = null;
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return AppChange;
        }

        protected DTO.ResponseMessage<bool> InsertAttachFileApplicantChange()
        {
            var result = new ResponseMessage<bool>();
            ApplicantBiz biz = new ApplicantBiz();
            //Get Attach from List
            foreach (IAS.BLL.AttachFilesIAS.AttachFile lic in ucAttachFileControl1.AttachFiles)
            {
                DTO.AttachFileApplicantChange ent = new AttachFileApplicantChange
                {
                    ID_ATTACH_FILE = lic.REGISTRATION_ID,
                    //ID_CARD_NO = lic.ID_CARD_NO,
                    ID_CARD_NO = this.txtIdCard.Text,
                    ATTACH_FILE_TYPE = lic.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = lic.ATTACH_FILE_PATH,
                    REMARK = lic.REMARK,
                    //CREATED_BY = lic.CREATED_BY,
                    CREATED_BY = base.UserId,
                    CREATED_DATE = lic.CREATED_DATE,
                    //UPDATED_BY = lic.UPDATED_BY,
                    UPDATED_BY = base.UserId,
                    UPDATED_DATE = lic.UPDATED_DATE,
                    FILE_STATUS = lic.FILE_STATUS,
                    CHANGE_ID = MaxID
                    //LICENSE_NO = lic.LICENSE_NO,
                    //RENEW_TIME = lic.RENEW_TIME,
                    //GROUP_LICENSE_ID = lic.GROUP_LICENSE_ID,
                };

                this.attachFilesApp.Add(ent);
            }

            DTO.ResponseMessage<bool> insertAttachFile = biz.InsertAttrachFileApplicantChange(this.attachFilesApp.ToArray(), base.UserProfile, GetAppChangeDetail());
             if (insertAttachFile.ResultMessage == true)
            {
                if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
                {
                    var res = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, "", "1", A, Z, false, "1", "0");
                    DataTable DT = res.DataResponse.Tables[0];
                    DataRow DR = DT.Rows[0];
                    string status = DR["STATUS"].ToString();
                    string asso = DR["ASSOCIATION_RESULT"].ToString();
                    string oic = DR["OIC_RESULT"].ToString();
                    string IDCardCreateBy = DR["CREATE_BY"].ToString();
                    string OLDidcard = DR["OLD_ID_CARD_NO"].ToString();

                    string TestingNO = DR["Testing_no"].ToString();
                    var sendMail = biz.SendMailAppChange(IDCardCreateBy, TestingNO, OLDidcard);//ส่งเมล์
                }
                else
                {
                    var res = biz.GetHistoryApplicant((DTO.RegistrationType)base.UserRegType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.IdCard, "", "0", A, Z, false, "0", "0");
                    DataTable DT = res.DataResponse.Tables[0];
                    DataRow DR = DT.Rows[0];
                    string status = DR["STATUS"].ToString();
                    string asso = DR["ASSOCIATION_RESULT"].ToString();
                    string oic = DR["OIC_RESULT"].ToString();
                    string IDCardCreateBy = DR["CREATE_BY"].ToString();
                    string OLDidcard = DR["OLD_ID_CARD_NO"].ToString();

                    string TestingNO = DR["Testing_no"].ToString();
                    var sendMail = biz.SendMailAppChange(IDCardCreateBy, TestingNO, OLDidcard);//ส่งเมล์
                }
                ClearText();
                ClearGridAttachFile();
                btnAddNew.Visible = false;
                result.ResultMessage = true;
                pnlAttachFileControl.Style.Add("display", "none");
                


            }
            else 
            {
                result.ResultMessage = false;

            }

             return result;
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            PanelEditContent.Visible = true;
            PanelSend.Visible = true;
            txtNewIDCard.Enabled = true;
            ddlNewPreNameCode.Enabled = true;
            txtNewNames.Enabled = true;
            txtNewLastName.Enabled = true;
            txtRemark.Enabled = true;
            lblStatusName.Visible = false;
            lblPreNameCode.Visible = true;
            txtPreNameCode.Visible = true;
            lblNames.Visible = true;
            txtNames.Visible = true;
            lblLastName.Visible = true;
            txtLastName.Visible = true;
            btnAddNew.Enabled = false;
            ucAttachFileControl1EnabledTrue();
            #region bindDataAppT
            ApplicantBiz biz = new ApplicantBiz();
            var res = biz.GetRequestEditApplicant((DTO.RegistrationType)base.UserProfile.MemberType, txtIdCard.Text, txtTestingNo.Text, base.UserProfile.CompCode);
            if (res.DataResponse.Tables[0].Rows.Count > 0)
            {
                DataTable DT = res.DataResponse.Tables[0];
                DataRow DR = DT.Rows[0];

                txtNewIDCard.Text = DR["ID_CARD_No"].ToString();
                txtNewNames.Text = DR["Names"].ToString();
                txtNewLastName.Text = DR["LastName"].ToString();
                ddlNewPreNameCode.SelectedValue = DR["pre_name_code"].ToString();
                txtRemark.Text = "";
                
            }
            #endregion bindDataAppT
            ucAttachFileControl1.AttachFiles.Clear();
            ucAttachFileControl1.DataBind();
            this.GetAttachFilesType();

            if (PanelSend.Visible == true)
            {
                this.ApplicantView = "";
                ucAttachFileControl1.GetDocReqApplicantName();
            }
            else
            {
                this.ApplicantView = "AppView";
                ucAttachFileControl1.GetDocReqApplicantName();
            }
        }
    }
}
