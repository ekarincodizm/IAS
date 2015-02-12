using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Properties;
using System.Threading;
using IAS.Utils;
using System.Globalization;
using System.Data;
using IAS.BLL;


namespace IAS.License
{
    public partial class LicenseApprove : basepage
    {
        #region Public Param & Session
        string maxBefore = string.Empty;
        public int PAGE_SIZE;
        public int _totalPages;
        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        public int countCheckDetail = 0;
        public MasterPage.Site1 MasterSite
        {

            get { return (this.Page.Master as MasterPage.Site1); }
        }
        public List<DTO.GenLicense> ListLicense
        {
            get
            {
                return Session["lsLicense"] == null
                                        ? new List<DTO.GenLicense>()
                                        : (List<DTO.GenLicense>)Session["lsLicense"];
            }

            set
            {
                Session["lsLicense"] = value;
            }
        }
        public string LicenseTypeCode
        {
            get
            {
                return Session["LicenseTypeCode"] == null ? string.Empty : Session["LicenseTypeCode"].ToString();
            }
            set
            {
                Session["LicenseTypeCode"] = value;
            }
        }

        public string PettionTypeCode
        {
            get
            {
                return Session["PettionTypeCode"] == null ? string.Empty : Session["PettionTypeCode"].ToString();
            }
            set
            {
                Session["PettionTypeCode"] = value;
            }
        }

        public string CurrentLicenseRenewTime
        {
            get
            {
                return Session["CurrentLicenseRenewTime"] == null ? string.Empty : Session["CurrentLicenseRenewTime"].ToString();
            }
            set
            {
                Session["CurrentLicenseRenewTime"] = value;
            }
        }
        public string CheckApprove
        {
            get
            {
                return Session["CheckApprove"] == null ? string.Empty : Session["CheckApprove"].ToString();
            }
            set
            {
                Session["CheckApprove"] = value;
            }
        }

        public string Mode
        {
            get
            {
                return Session["Mode"] == null ? string.Empty : Session["Mode"].ToString();
            }
            set
            {
                Session["Mode"] = value;
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                txtStartDate.Attributes.Add("readonly", "true");
                txtEndDate.Attributes.Add("readonly", "true");
                base.HasPermit();
                GetPaymentType();
                DefaultData();
                ListLicense = new List<DTO.GenLicense>();

            }
            this.CurrentLicenseRenewTime = "0";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListLicense.Clear();
            if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                this.MasterSite.ModelError.ShowMessageError = Resources.errorApplicantNoPay_004;
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {
                txtNumberGvSearch.Text = "1";
                PAGE_SIZE = PAGE_SIZE_Key;
                maxBefore = txtInputMaxrow.Text;
                if ((txtInputMaxrow.Text != Convert.ToString(PAGE_SIZE) && txtInputMaxrow.Text != "" && Convert.ToInt32(txtInputMaxrow.Text) != 0))
                {

                    txtInputMaxrow.Text = maxBefore;
                }
                else if (txtInputMaxrow.Text == "" || Convert.ToInt32(txtInputMaxrow.Text) == 0)
                {
                    txtInputMaxrow.Text = Convert.ToString(PAGE_SIZE);
                }
                BindDataInGridView();
            }
        }
        protected void chkSelectGroupR_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkselect = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)checkselect.Parent.Parent;
            var lblPaymentNo = (Label)gr.FindControl("lblPaymentNo");
            var lblHeadRequestNo = (Label)gr.FindControl("lblHeadRequestNo");
            var lblUPLOAD_GROUP_NO = (Label)gr.FindControl("lblUPLOAD_GROUP_NO");
            var lblSEQ_NO = (Label)gr.FindControl("lblSEQ_NO");
            if (checkselect.Checked)
            {
                var ChkGroupNo = ListLicense.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUPLOAD_GROUP_NO.Text && x.SEQ_NO == lblSEQ_NO.Text);
                if (ChkGroupNo == null)
                {
                    this.ListLicense.Add(new DTO.GenLicense
                    {
                        HEAD_REQUEST_NO = lblHeadRequestNo.Text,
                        PAYMENT_NO = lblPaymentNo.Text,
                        USER_ID = base.UserId,
                        UPLOAD_GROUP_NO = lblUPLOAD_GROUP_NO.Text,
                        SEQ_NO = lblSEQ_NO.Text,
                    });
                }
            }
            else
            {
                var pament = ListLicense.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUPLOAD_GROUP_NO.Text && x.SEQ_NO == lblSEQ_NO.Text);
                ((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("Checkall")).Checked = false;
                ListLicense.Remove(pament);
                //((CheckBox)((GridView)gr.Parent.Parent).HeaderRow.FindControl("checkall")).Checked = false;
                //ListLicense.Remove(lblGroupRequestNo.Text);
            }
        }
        protected void checkall_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkall = (CheckBox)sender;
            foreach (GridViewRow gr in gvSearch.Rows)
            {
                CheckBox checkselect = (CheckBox)gr.FindControl("chkSelectGroupR");
                var lblPaymentNo = (Label)gr.FindControl("lblPaymentNo");
                var lblHeadRequestNo = (Label)gr.FindControl("lblHeadRequestNo");
                var lblUPLOAD_GROUP_NO = (Label)gr.FindControl("lblUPLOAD_GROUP_NO");
                var lblSEQ_NO = (Label)gr.FindControl("lblSEQ_NO");
                if (checkall.Checked)
                {
                    if (!checkselect.Checked)
                    {
                        ListLicense.Add(new DTO.GenLicense
                        {
                            HEAD_REQUEST_NO = lblHeadRequestNo.Text,
                            PAYMENT_NO = lblPaymentNo.Text,
                            USER_ID = base.UserId,
                            UPLOAD_GROUP_NO = lblUPLOAD_GROUP_NO.Text,
                            SEQ_NO = lblSEQ_NO.Text,
                        });
                        checkselect.Checked = true;
                    }
                }
                else
                {
                    var pament = ListLicense.FirstOrDefault(x => x.UPLOAD_GROUP_NO == lblUPLOAD_GROUP_NO.Text && x.SEQ_NO == lblSEQ_NO.Text);
                    ListLicense.Remove(pament);
                    checkselect.Checked = false;
                }
            }
        }
        bool b_check = true;
        CheckBox check_all_head;
        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                check_all_head = (CheckBox)e.Row.FindControl("checkall");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var lblPaymentNo = (Label)e.Row.FindControl("lblPaymentNo");
                var lblHeadRequestNo = (Label)e.Row.FindControl("lblHeadRequestNo");
                CheckBox chkSelectPayment = (CheckBox)e.Row.FindControl("chkSelectGroupR");
                var ls = ListLicense.FirstOrDefault(x => x.PAYMENT_NO == lblPaymentNo.Text && x.HEAD_REQUEST_NO == lblHeadRequestNo.Text);

                if (ls != null)
                {
                    chkSelectPayment.Checked = true;

                    b_check = false;
                }
                else
                {
                    chkSelectPayment.Checked = false;

                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    if (b_check)
                    {
                        check_all_head.Checked = true;
                    }
                }
            }
        }
        string checkHead = string.Empty;
     
       
        private void chkValidatePropLicenseApp(GridView gv, string mode)
        {
            LicenseBiz biz = new LicenseBiz();

            foreach (GridViewRow row in gv.Rows)
            {
                Label lblID = (Label)row.FindControl("lblID");
                Label lblItemName = (Label)row.FindControl("lblItemName");
                Image impCorrect = (Image)row.FindControl("impCorrect");
                Image impdisCorrect = (Image)row.FindControl("impdisCorrect");

                //Single License
                if (mode.Equals(DTO.LicensePropMode.General.GetEnumValue().ToString()))
                {
                    // var result = text.Where(l => l.Equals(lblItemName.Text.Trim())).FirstOrDefault();

                    if (gv.ID.Equals("gvGeneral"))
                    {
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = true;
                        //1	บรรลุนิติภาวะ
                        //6	มีภูมิลำเนาในประเทศไทย
                        //7	ไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ
                        //8	ไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุก ในความผิดเกี่ยวกับทรัพย์ที่กระทำโดยทุจริต เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต
                        //9	ไม่เป็นบุคคลล้มละลาย
                        //11 ไม่เป็นตัวแทนประกันชีวิต
                        //13 ไม่เคยถูกเพิกถอนใบอนุญาตเป็นตัวแทนประกันชีวิต หรือใบอนุญาตเป็นนายหน้าประกันชีวิต ระยะเวลาห้าปีก่อนวันขอรับใบอนุญาต
                        switch (lblID.Text)
                        {

                            case "1":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "6":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "7":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "8":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "9":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "10":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "11":
                                if (this.LicenseTypeCode.Equals("03"))
                                {

                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }


                                else if (this.LicenseTypeCode.Equals("04"))
                                {

                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                                }
                                else
                                {
                                    impdisCorrect.Visible = true;
                                    impCorrect.Visible = false;
                                }


                                break;
                            case "12":
                              

                                    impCorrect.Visible = true;
                                    impdisCorrect.Visible = false;
                             


                                break;
                            case "13":

                                impCorrect.Visible = true;
                                
                                    impdisCorrect.Visible = false;
                           
                                break;
                            case "15":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;

                                break;
                            case "16":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;

                                break;
                            case "19":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;


                                break;
                            case "22":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;


                                break;
                        }
                        UpdatePanelSearch.Update();

                    }
                    else if (gv.ID.Equals("gvExamResult"))
                    {
                        //2	ผลสอบ
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = true;
                        switch (lblID.Text)
                        {
                            case "2":

                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;

                                break;
                        }
                        UpdatePanelSearch.Update();
                    }
                    else if (gv.ID.Equals("gvEducation"))
                    {
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = true;
                        //3	สำเร็จการศึกษาไม่ต่ำกว่าปริญญาตรี วิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต
                        switch (lblID.Text)
                        {
                            case "3":
                                impdisCorrect.Visible = true;
                                impCorrect.Visible = false;
                                break;
                            case "17":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                        }
                        UpdatePanelSearch.Update();
                    }
                    else if (gv.ID.Equals("gvTrainResult"))
                    {
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = true;
                        //4	ผลอบรม
                        switch (lblID.Text)
                        {
                            case "4":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                        }
                        UpdatePanelSearch.Update();
                    }
                    else if (gv.ID.Equals("gvOther"))
                    {
                        impCorrect.Visible = true;
                        impdisCorrect.Visible = true;
                        //5	    รูปถ่าย
                        //14	มีการชำระค่าธรรมเนียมค่าขอรับใบอนุญาต
                        //18	มีการชำระค่าธรรมเนียมค่ขอต่ออายุใบอนุญาต
                        switch (lblID.Text)
                        {
                            case "5":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "14":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                            case "18":
                                impCorrect.Visible = true;
                                impdisCorrect.Visible = false;
                                break;
                        }
                        UpdatePanelSearch.Update();
                    }
                }

                
            }

        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

          
            Label PetitionCode = (Label)gr.FindControl("lblPetitionCode");
            Label LicenseCode = (Label)gr.FindControl("lblLicenseCode");
            Label RenewTime = (Label)gr.FindControl("lblRenewTime");
            Label IdCard = (Label)gr.FindControl("lblIdCard");
            Label lblLicenseno = (Label)gr.FindControl("lblLicenseno");
            this.LicenseTypeCode = LicenseCode.Text;
            this.PettionTypeCode = PetitionCode.Text;
            this.CurrentLicenseRenewTime = RenewTime.Text == "" ? "0" : RenewTime.Text;
            this.CheckApprove = "ApproveL";
            this.Mode = DTO.LicensePropMode.General.GetEnumValue().ToString();
            LicenseBiz biz = new LicenseBiz();
         

            #region gvGeneral
            DTO.ResponseService<DTO.ValidateLicense[]> result = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 1);
         
          
            // this.GridviewGeneral = result.DataResponse.ToList();
            if (result.DataResponse.Count() > 0)
            {
                gvGeneral.Visible = true;
                gvGeneral.DataSource = result.DataResponse;
                gvGeneral.DataBind();
               
                //Create Head Genaral
                if (gvGeneral.Controls.Count > 0)
                {
                    AddSuperHeader(gvGeneral);
                    checkHead = "T";
                }
                else
                {
                    //AddSuperHeader(gvTrainResult);
                }

            }
            else
            {
                gvGeneral.Visible = false;
            }

            #endregion

            #region gvExamResult
            DTO.ResponseService<DTO.ValidateLicense[]> result2 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 2);
            if (result2.DataResponse.Count() > 0)
            {
                gvExamResult.DataSource = result2.DataResponse;
                gvExamResult.DataBind();
                gvExamResult.Visible = true;
                // this.GridviewExamResult = result2.DataResponse.ToList();

                //Create Head Genaral
                if (gvGeneral.Controls.Count > 0)
                {
                    //AddSuperHeader(gvExamResult);
                }
                else
                {
                    if (checkHead != "T")
                    {
                        AddSuperHeader(gvExamResult);
                        checkHead = "T";
                    }
                }
            }
            else
            {
                gvExamResult.Visible = false;
            }


            #endregion

            #region gvEducation
            DTO.ResponseService<DTO.ValidateLicense[]> result3 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 3);
            if (result3.DataResponse.Count() > 0)
            {
                gvEducation.DataSource = result3.DataResponse;
                gvEducation.DataBind();
                gvEducation.Visible = true;
                // this.GridviewEducation = result3.DataResponse.ToList();

                //Create Head Genaral
                if (gvExamResult.Controls.Count > 0)
                {
                    //AddSuperHeader(gvEducation);
                }
                else
                {
                    if (checkHead != "T")
                    {
                        AddSuperHeader(gvEducation);
                        checkHead = "T";
                    }
                }
            }
            else
            {
                gvEducation.Visible = false;
            }

            #endregion

            #region gvTrainResult
            DTO.ResponseService<DTO.ValidateLicense[]> result4 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 4);
            if (result4.DataResponse.Count() > 0)
            {
                gvTrainResult.DataSource = result4.DataResponse;
                gvTrainResult.DataBind();
                gvTrainResult.Visible = true;
                //  this.GridviewTrainResult = result4.DataResponse.ToList();

                //Create Head Genaral
                if (gvEducation.Controls.Count > 0)
                {
                    //AddSuperHeader(gvTrainResult);
                }
                else
                {
                    if (checkHead != "T")
                    {
                        AddSuperHeader(gvTrainResult);
                        checkHead = "T";
                    }
                }
            }
            else
            {
                gvTrainResult.Visible = false;
            }
            #endregion

            #region gvOther
            DTO.ResponseService<DTO.ValidateLicense[]> result5 = biz.GetPropLiecense(this.LicenseTypeCode, this.PettionTypeCode, Convert.ToInt16(this.CurrentLicenseRenewTime), 5);
            if (result5.DataResponse.Count() > 0)
            {
                gvOther.DataSource = result5.DataResponse;
                gvOther.DataBind();
                gvOther.Visible = true;
                //  this.GridviewOther = result5.DataResponse.ToList();

                //Create Head Genaral
                if (gvTrainResult.Controls.Count > 0)
                {
                    //AddSuperHeader(gvOther);
                }
                else
                {
                    if (checkHead != "T")
                    {
                        AddSuperHeader(gvOther);
                        checkHead = "T";
                    }
                }
            }
            else
            {
                gvOther.Visible = false;
            }
            #endregion

            UpdatePanelSearch.Update();
            MPDetailLicense.Show();
        }

        private void BindDataInGridView()
        {
            //PAGE_SIZE = PAGE_SIZE_Key;
            PAGE_SIZE = Convert.ToInt16(txtInputMaxrow.Text);
            gvSearch.Visible = true;
            var resultPage = txtNumberGvSearch.Text.ToInt();
            var biz = new BLL.PaymentBiz();
            var resCount = biz.GetPaymentLicenseAppove(ddlTypePay.SelectedValue.ToString(), txtIdCard.Text,
                          txtOrder.Text, Convert.ToDateTime(txtStartDate.Text)
                         , Convert.ToDateTime(txtEndDate.Text), txtFirstName.Text, txtLastName.Text, "Y", resultPage, PAGE_SIZE);
            DataSet ds = resCount.DataResponse;
            DataTable dt = ds.Tables[0];
            DataRow drLicense = ds.Tables[0].Rows[0];
            int rowCount = Convert.ToInt32(drLicense["rowcount"].ToString());
            double dblPageCount = (double)((decimal)rowCount / PAGE_SIZE);
            TotalPages = (int)Math.Ceiling(dblPageCount);
            txtTotalPage.Text = Convert.ToString(TotalPages);
            var res = biz.GetPaymentLicenseAppove(ddlTypePay.SelectedValue.ToString(), txtIdCard.Text,
                         txtOrder.Text, Convert.ToDateTime(txtStartDate.Text)
                        , Convert.ToDateTime(txtEndDate.Text), txtFirstName.Text, txtLastName.Text, "N", resultPage, PAGE_SIZE);
            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();
                gvSearch.Visible = true;

                if (TotalPages == 0)
                {
                    divGv1.Visible = true;
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;

                    btngo.Visible = true;
                    lblTotalrecord.Text = "0";
                    txtTotalPage.Text = "1";
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;

                    CheckBox ckall = (CheckBox)gvSearch.HeaderRow.FindControl("Checkall");
                    ckall.Visible = false;
                }
                else if (TotalPages > 1)
                {
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;
                    btnNextGvSearch.Visible = true;
                    btnPreviousGvSearch.Visible = false;
                    btngo.Visible = true;
                    lblTotalrecord.Text = drLicense["rowcount"].ToString();
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;
                    btnGenLicense.Visible = true;
                    divGv1.Visible = true;
                }
                else if (TotalPages == 1)
                {
                    txtNumberGvSearch.Visible = true;
                    lblParaPage.Visible = true;
                    txtTotalPage.Visible = true;
                    btnGenLicense.Visible = true;
                    btngo.Visible = true;
                    btnPreviousGvSearch.Visible = false;
                    lblTotalrecord.Text = drLicense["rowcount"].ToString();
                    txtInputMaxrow.Visible = true;
                    lblHeadInputMaxrow.Visible = true;
                    lblHeadTotal.Visible = true;
                    lblTotalrecord.Visible = true;
                    lblEndTotal.Visible = true;

                    divGv1.Visible = true;
                }
                if (gvSearch.Rows.Count == 0)
                {
                    btnGenLicense.Visible = false;
                }
                else
                {
                    btnGenLicense.Visible = true;
                }

                //UpdatePanelSearch.Update();

            }
        }

        protected void DefaultData()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            //txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            btnGenLicense.Visible = false;
            txtOrder.Text = string.Empty;
            txtIdCard.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            ddlTypePay.SelectedIndex = 0;
            gvSearch.DataSource = null;
            txtInputMaxrow.Text = PAGE_SIZE_Key.ToString();

            gvSearch.Visible = false;
            btnPreviousGvSearch.Visible = false;
            txtNumberGvSearch.Visible = false;
            lblParaPage.Visible = false;
            txtTotalPage.Visible = false;
            btnNextGvSearch.Visible = false;
            lblHeadInputMaxrow.Visible = false;
            txtInputMaxrow.Visible = false;
            btngo.Visible = false;
            lblHeadTotal.Visible = false;
            lblTotalrecord.Visible = false;
            lblEndTotal.Visible = false;





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
            List<DTO.DataItem> lstemp = new List<DTO.DataItem>();
            lstemp = ls.DataResponse.ToList();
            lstemp.RemoveAll(x => x.Id == "01");
            BindToDDL(ddlTypePay, lstemp);


        }
        protected void BindPage()
        {
            PAGE_SIZE = Convert.ToInt32(txtInputMaxrow.Text);
            var biz = new BLL.PaymentBiz();
            var resultPage = txtNumberGvSearch.Text.ToInt();
            var res = biz.GetPaymentLicenseAppove(ddlTypePay.SelectedValue.ToString(), txtIdCard.Text,
                  txtOrder.Text, Convert.ToDateTime(txtStartDate.Text)
                 , Convert.ToDateTime(txtEndDate.Text), txtFirstName.Text, txtLastName.Text, "N", resultPage, PAGE_SIZE);
            gvSearch.Visible = true;
            gvSearch.DataSource = res.DataResponse;
            gvSearch.DataBind();
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = false;

                btngo.Visible = true;

                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            else if (Convert.ToInt32(result) > 1)
            {
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;
            }
            BindPage();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {

            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = true;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;

            }
            else
            {
                txtNumberGvSearch.Text = txtTotalPage.Text;
                txtNumberGvSearch.Visible = true;
                lblParaPage.Visible = true;
                txtTotalPage.Visible = true;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;

                btngo.Visible = true;
                txtInputMaxrow.Visible = true;
                lblHeadInputMaxrow.Visible = true;
                lblHeadTotal.Visible = true;
                lblTotalrecord.Visible = true;
                lblEndTotal.Visible = true;

            }
            BindPage();
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DefaultData();
        }

        protected void btnGenLicense_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var biz = new BLL.PaymentBiz();
            if (ListLicense.Count == 0)
            {
                this.MasterSite.ModelError.ShowMessageError = "กรุณาเลือกรายการ";
                this.MasterSite.ModelError.ShowModalError();
                return;
            }
            var res = biz.Insert12T(this.ListLicense.ToArray());
            if (res.ResultMessage == false)
            {
                this.MasterSite.ModelError.ShowMessageError = "ให้ความเห็นชอบออกใบอนุญาตไม่สำเร็จ";
                this.MasterSite.ModelError.ShowModalError();
            }
            else
            {

                ListLicense = new List<DTO.GenLicense>();
                this.MasterSite.ModelSuccess.ShowMessageSuccess = Resources.infoLicenseApprove_001;
                this.MasterSite.ModelSuccess.ShowModalSuccess();

                BindDataInGridView();

            }
            sw.Stop();
            TimeSpan sp3 = sw.Elapsed;
            TimeSpan duration3 = sp3.Duration();
        }

        private static void AddSuperHeader(GridView gridView)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            myNewRow.Cells.Add(MakeCell("ลำดับที่", 1));
            myNewRow.Cells.Add(MakeCell("เงื่อนไขการตรวจสอบ", 1));
            myNewRow.Cells.Add(MakeCell("ตรวจสอบผ่านระบบ", 1));
            myNewRow.Cells.Add(MakeCell("หมายเหตุ", 1));

            myTable.Rows.AddAt(0, myNewRow);

        }


        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { Text = text ?? string.Empty, ColumnSpan = span, HorizontalAlign = HorizontalAlign.Center };
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // Adding a column manualy onece the header creater
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].ColumnSpan = 4;
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;


                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;


                //var myGridView = (GridView)sender;

            }
        }

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
    }
}