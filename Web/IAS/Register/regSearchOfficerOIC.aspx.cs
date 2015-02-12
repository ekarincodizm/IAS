using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.BLL.RegistrationService;
using IAS.BLL;
using System.Data;
using AjaxControlToolkit;
using System.Threading;
using System.Globalization;
using IAS.Properties;

namespace IAS.Register
{
    public partial class regSearchOfficerOIC : basepage
    {
        public DateTime? sdate;
        public DateTime? tdate;
        int PAGE_SIZE = 20;
        public int _totalPages;

        public List<DTO.RegSearchOfficer> ListRegSearch
        {
            get
            {
                if (Session["ListRegSearch"] == null)
                {
                    Session["ListRegSearch"] = new List<DTO.RegSearchOfficer>();
                }

                return (List<DTO.RegSearchOfficer>)Session["ListRegSearch"];
            }

            set
            {
                Session["ListRegSearch"] = value;
            }
        }

        public string RegStatus
        {
            get { return Session["RegStatus"] == null ? string.Empty : Session["RegStatus"].ToString(); }
            set { Session["RegStatus"] = value; }
        }

        public string ApproveStatus
        {
            get { return Session["ApproveStatus"] == null ? string.Empty : Session["ApproveStatus"].ToString(); }
            set { Session["ApproveStatus"] = value; }
        }

        public string PersonalIDCard
        {
            get { return Session["PersonalIDCard"] == null ? string.Empty : Session["PersonalIDCard"].ToString(); }
            set { Session["PersonalIDCard"] = value; }
        }

        public int TotalPages
        {
            get { return _totalPages; }
            set { _totalPages = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtstartDate.Attributes.Add("readonly", "true");
            txttoDate.Attributes.Add("readonly", "true");
            if (!IsPostBack)
            {
                initBindData();
                ListID = new List<string>();
            }
            
        }

        private void DefaultData()
        {
            //txtstartDate.Text = string.Empty;
            //txttoDate.Text = string.Empty;
            txtstartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtstartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txttoDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txttoDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
        }

        private void GetSearchOICType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //var ls = biz.GetMemberType(SysMessage.DefaultSelecting, true);
            var ls = biz.GetMemberTypeNotOIC_for_regSearchOfficerOIC(SysMessage.DefaultSelecting, true);
            var chkls = ls.FirstOrDefault(m => m.Id.Equals("1"));
            if (chkls != null)
            {
                chkls.Name = "บุคคลทั่วไป/ตัวแทน/นายหน้า";
            }
            //ls.RemoveAt(4);
            BindToDDL(ddlSearchMemberType, ls);
        }

        private void GetSearchStatus()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetStatus("");
            // 7:ไม่ใช้งาน(กรณีการเปลี่ยน member type แล้วสร้าง ID ใหม่โดยอ้างอิงข้อมูลเดิม)
            ls = ls.Where(s => s.Id != "7").ToList();
            ls.Insert(0, new DTO.DataItem { Id = string.Empty, Name = "เลือก" });
            ls.Insert(ls.Count, new DTO.DataItem { Id = "0", Name = "ทั้งหมด" });
            BindToDDL(ddlSearchStatus, ls);
        }

        /// <summary>
        /// Edited by Nattapong
        /// Filter if Search count = 0 > disable btnGroupApprove
        /// Last Update 29/8/2556
        /// </summary>
        /// 
        private void initBindData()
        {
            if (this.ListRegSearch.Count == 0 || Request.QueryString["Back"] == null)
            {
                //&& Request.QueryString["regis"] == nullSession["ListRegSearch"] == null ||
              
                base.HasPermit();
                DefaultData();
                GetSearchOICType();
                GetSearchStatus();
                
            }
            else
            {
               
                base.HasPermit();
                //DefaultData();
                GetSearchOICType();
                GetSearchStatus();
                foreach(DTO.RegSearchOfficer item in ListRegSearch){
                    ddlSearchMemberType.SelectedValue = item.MemberType;
                    ddlSearchStatus.SelectedValue = item.status;
                    txtIDCard.Text = item.Idcard;
                    txtFirstName.Text = item.FName;
                    txtLastName.Text = item.LName;
                    txtEmail.Text = item.Email;
                    txtstartDate.Text = item.txt_startDate;//milk
                    txttoDate.Text = item.txt_endDate;//milk
                    txtNumberGvSearch.Text = item.CurrentPage;
                }
               
                GetSearchOfficerOIC();
            }
        }


        private void GetSearchOfficerOIC()
        {
            pnPage.Visible = true;
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            BLL.PersonBiz bizPerson = new PersonBiz();
            this.RegStatus = ddlSearchStatus.SelectedValue;
            var resultPage = txtNumberGvSearch.Text.ToInt();
            //Status 1 = รออนุมัติ(สมัคร), Status 2 = อนุมัติ(สมัคร), Status 3 = ไม่อนุมัติ(สมัคร)

            if(ddlSearchStatus.SelectedValue.Equals("") && ddlSearchMemberType.SelectedValue.Equals(""))
            {
                UCModalError.ShowMessageError = Resources.errorRegSearchOfficerOIC_001;
                UCModalError.ShowModalError();
                return;
            
            }
            else if (ddlSearchStatus.SelectedValue.Equals(""))
            {
                UCModalError.ShowMessageError = Resources.errorRegSearchOfficerOIC_002;
                UCModalError.ShowModalError();
                return;
            }
            else if (ddlSearchMemberType.SelectedValue.Equals(""))
            {
                UCModalError.ShowMessageError = Resources.errorRegSearchOfficerOIC_003;
                UCModalError.ShowModalError();
                return;
            }

            //start date todate
            if (string.IsNullOrEmpty(txtstartDate.Text) &&  !string.IsNullOrEmpty(txttoDate.Text))
            {
                UCModalError.ShowMessageError = Resources.errorRegSearchOfficerOIC_004;
                UCModalError.ShowModalError();
                return;

            }
            else if (!string.IsNullOrEmpty(txtstartDate.Text) && string.IsNullOrEmpty(txttoDate.Text))
            {
                UCModalError.ShowMessageError = Resources.errorRegSearchOfficerOIC_005;
                UCModalError.ShowModalError();
                return;
            
            }

        
            if (!string.IsNullOrEmpty(txtstartDate.Text))
            {
                sdate = Convert.ToDateTime(txtstartDate.Text);
            }
            else
            {
                sdate = null;
            }
            if (!string.IsNullOrEmpty(txttoDate.Text))
            {
                tdate = Convert.ToDateTime(txttoDate.Text);

            }
            else
            {
                tdate = null;
            }


            if (ddlSearchStatus.SelectedValue.Equals("1") || ddlSearchStatus.SelectedValue.Equals("2") || ddlSearchStatus.SelectedValue.Equals("3"))
            {
                //btnGroupApprove.Visible = true;

                
                var resCount = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                        txtLastName.Text,
                                                        sdate,
                                                        tdate,
                                                        txtIDCard.Text, 
                                                        ddlSearchMemberType.SelectedValue, 
                                                        txtEmail.Text, 
                                                        "", 
                                                        ddlSearchStatus.SelectedValue, 
                                                        resultPage,
                                                        PAGE_SIZE,"1");
                DataSet ds = resCount.DataResponse;
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                lblrows.Text = rowcount.ToString();
                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                TotalPages = (int)Math.Ceiling(dblPageCount);
                txtTotalPage.Text = Convert.ToString(TotalPages);
                var res = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                      txtLastName.Text,
                                                      sdate,
                                                      tdate,
                                                      txtIDCard.Text,
                                                      ddlSearchMemberType.SelectedValue,
                                                      txtEmail.Text,
                                                      "",
                                                      ddlSearchStatus.SelectedValue,
                                                      resultPage,
                                                      PAGE_SIZE, "2");
                if (res.ErrorMsg == null)
                {
                    if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                            ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                    {
                        gvSearchOfficerOIC.DataSource = res.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();

                        if (gvSearchOfficerOIC.Rows.Count > 0)
                        {
                          //  btnExportExcel.Visible = true;
                            pnlDetail.Visible = true;
                            btnGroupApprove.Visible = true;
                            for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
                            {
                                gvSearchOfficerOIC.Columns[0].Visible = true;
                               
                            }
                            if (txtTotalPage.Text == "1")
                            {

                                txtNumberGvSearch.Visible = true;
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                                btnNextGvSearch.Visible = false;
                            
                            }
                            else {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                            }
                        }
                        else
                        {
                            pnlDetail.Visible = true;
                          //  btnExportExcel.Visible = false;
                            btnGroupApprove.Visible = false;
                            txtTotalPage.Visible = true;
                            txtTotalPage.Text = "1";
                            txtNumberGvSearch.Text = "1";
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = false;
                            btnPreviousGvSearch.Visible = false;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                            CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                            CH_all.Visible = false;
                        }
                    }
                    else
                    {

                        gvSearchOfficerOIC.DataSource = res.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();
                        if (gvSearchOfficerOIC.Rows.Count > 0)
                        {
                            pnlDetail.Visible = true;
                          //  btnExportExcel.Visible = true;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                            btnGroupApprove.Visible = false;
                            if (txtTotalPage.Text == "1")
                            {

                                txtNumberGvSearch.Visible = true;
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                btnNextGvSearch.Visible = false;
                                btnPreviousGvSearch.Visible = false;
                            }
                            else
                            {

                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = true;
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                            }
                        }
                        else
                        {
                            pnlDetail.Visible = true;
                         //   btnExportExcel.Visible = false;
                            txtTotalPage.Visible = true;
                            txtTotalPage.Text = "1";
                            txtNumberGvSearch.Text = "1";
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = false;
                            btnPreviousGvSearch.Visible = false;
                            btnGroupApprove.Visible = false;
                            CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                            CH_all.Visible = false;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                        }
                        
                    }
                }
                else
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                }
            }
            //Status 4 = รออนุมัติ(แก้ไข), Status 5 = อนุมัติ(แก้ไข), Status 6 = ไม่อนุมัติ(แก้ไข)
            else if (ddlSearchStatus.SelectedValue.Equals("4"))
            {
                btnGroupApprove.Visible = false;
                var ls = bizPerson.GetPersonTempEditByCriteria(txtFirstName.Text, txtLastName.Text,sdate,tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue, resultPage,
                                                        PAGE_SIZE,"1");
                DataSet ds = ls.DataResponse;
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                lblrows.Text = rowcount.ToString();
                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                TotalPages = (int)Math.Ceiling(dblPageCount);
                txtTotalPage.Text = Convert.ToString(TotalPages);
                var result = bizPerson.GetPersonTempEditByCriteria(txtFirstName.Text, txtLastName.Text, sdate, tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue, resultPage,
                                                        PAGE_SIZE, "2");
                if (result.ErrorMsg == null)
                {

                    if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                            ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                    {
                        gvSearchOfficerOIC.DataSource = result.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();

                        if (gvSearchOfficerOIC.Rows.Count > 0)
                        {
                            pnlDetail.Visible = true;
                            btnGroupApprove.Visible = true;
                            for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
                            {
                                gvSearchOfficerOIC.Columns[0].Visible = true;

                            }
                            if (txtTotalPage.Text == "1")
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = false;
                                btnPreviousGvSearch.Visible = false;
                            }
                            else
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                            }
                        }
                        else
                        {
                            btnGroupApprove.Visible = false;
                            pnlDetail.Visible = true;                           
                            txtTotalPage.Visible = true;
                            txtTotalPage.Text = "1";
                            txtNumberGvSearch.Text = "1";
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = false;
                            btnPreviousGvSearch.Visible = false;
                            CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                            CH_all.Visible = false;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                        }
                    }
                    else
                    {
                        gvSearchOfficerOIC.DataSource = result.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();
                        if (gvSearchOfficerOIC.Rows.Count > 0)
                        {
                            pnlDetail.Visible = true;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                            btnGroupApprove.Visible = false;
                            if (txtTotalPage.Text == "1")
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = false;
                                btnPreviousGvSearch.Visible = false;
                            }
                            else
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                            }
                        }
                        else
                        {
                            pnlDetail.Visible = true;
                            btnGroupApprove.Visible = false;
                            txtTotalPage.Visible = true;
                            txtTotalPage.Text = "1";
                            txtNumberGvSearch.Text = "1";
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = false;
                            btnPreviousGvSearch.Visible = false;
                            CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                            CH_all.Visible = false;
                           // gvSearchOfficerOIC.Columns[0].Visible = false;
                        }
                    }
                }
                else
                {
                    UCModalError.ShowMessageError = ls.ErrorMsg;
                    UCModalError.ShowModalError();
                }
            }
            else if (ddlSearchStatus.SelectedValue.Equals("5") || ddlSearchStatus.SelectedValue.Equals("6"))
            {
                btnGroupApprove.Visible = false;
                var ls = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text,sdate,tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue,
                                                     resultPage,PAGE_SIZE,"1");
                DataSet ds = ls.DataResponse;
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                lblrows.Text = rowcount.ToString();
                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                TotalPages = (int)Math.Ceiling(dblPageCount);
                txtTotalPage.Text = Convert.ToString(TotalPages);
                var result = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text,sdate,tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue,
                                                resultPage, PAGE_SIZE, "2");
                if (result.ErrorMsg == null)
                {

                    if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                            ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                    {
                        gvSearchOfficerOIC.DataSource = result.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();

                       if (gvSearchOfficerOIC.Rows.Count > 0)
                        { 
                            pnlDetail.Visible = true;
                            btnGroupApprove.Visible = true;
                            for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
                            {
                                gvSearchOfficerOIC.Columns[0].Visible = true;

                            }
                            if (txtTotalPage.Text == "1")
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = false;
                                btnPreviousGvSearch.Visible = false;
                            }
                            else
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                            }
                        }
                        else
                        {
                           
                            pnlDetail.Visible = true;
                            //  btnExportExcel.Visible = false;
                            btnGroupApprove.Visible = false;
                            txtTotalPage.Visible = true;
                            txtTotalPage.Text = "1";
                            txtNumberGvSearch.Text = "1";
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = false;
                            btnPreviousGvSearch.Visible = false;
                            CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                            CH_all.Visible = false;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                        }
                    }
                    else
                    {
                        gvSearchOfficerOIC.DataSource = result.DataResponse.Tables[0];
                        gvSearchOfficerOIC.DataBind();
                        if (gvSearchOfficerOIC.Rows.Count > 0)
                        {
                            pnlDetail.Visible = true;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                            btnGroupApprove.Visible = false;
                            if (txtTotalPage.Text == "1")
                            {

                                txtNumberGvSearch.Visible = true;
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                btnNextGvSearch.Visible = false;
                                btnPreviousGvSearch.Visible = false;
                            }
                            else
                            {
                                txtTotalPage.Visible = true;
                                lblspace.Visible = true;
                                txtNumberGvSearch.Visible = true;
                                btnNextGvSearch.Visible = true;
                                btnPreviousGvSearch.Visible = false;
                            }

                        }
                        else
                        {
                            pnlDetail.Visible = true;
                            //  btnExportExcel.Visible = false;
                            btnGroupApprove.Visible = false;
                            txtTotalPage.Visible = true;
                            txtTotalPage.Text = "1";
                            txtNumberGvSearch.Text = "1";
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = false;
                            btnPreviousGvSearch.Visible = false;
                            CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                            CH_all.Visible = false;
                            gvSearchOfficerOIC.Columns[0].Visible = false;
                        }
                    
                    }
                   
                }
                else
                {
                    UCModalError.ShowMessageError = ls.ErrorMsg;
                    UCModalError.ShowModalError();
                }
            }
            else if (ddlSearchStatus.SelectedValue.Equals("0"))
            {
                btnGroupApprove.Visible = false;

                var resCount = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                        txtLastName.Text,
                                                        sdate,
                                                        tdate,
                                                        txtIDCard.Text,
                                                        ddlSearchMemberType.SelectedValue,
                                                        txtEmail.Text,
                                                        "",
                                                        ddlSearchStatus.SelectedValue,
                                                        resultPage,
                                                        PAGE_SIZE, "3");
                DataSet ds = resCount.DataResponse;
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                int rowcount = Convert.ToInt32(dr["rowcount"].ToString());
                lblrows.Text = rowcount.ToString();
                double dblPageCount = (double)((decimal)rowcount / PAGE_SIZE);
                TotalPages = (int)Math.Ceiling(dblPageCount);
                txtTotalPage.Text = Convert.ToString(TotalPages);

                DTO.ResponseService<DataSet> res = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                      txtLastName.Text,
                                                      sdate,
                                                      tdate,
                                                      txtIDCard.Text,
                                                      ddlSearchMemberType.SelectedValue,
                                                      txtEmail.Text,
                                                      "",
                                                      ddlSearchStatus.SelectedValue,
                                                      resultPage,
                                                      PAGE_SIZE, "4");

                if (res.IsError)
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                    return;

                }
                else if (res.DataResponse != null)
                {
                    DataSet dss = res.DataResponse;
                    DataTable dtt = dss.Tables[0];
                    gvSearchOfficerOIC.DataSource = dtt;
                    gvSearchOfficerOIC.DataBind();

                    if (gvSearchOfficerOIC.Rows.Count > 0)
                    {
                        pnlDetail.Visible = true;
                        btnGroupApprove.Visible = false;
                        for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
                        {
                            gvSearchOfficerOIC.Columns[0].Visible = true;

                        }
                        if (txtTotalPage.Text == "1")
                        {

                            txtNumberGvSearch.Visible = true;
                            txtTotalPage.Visible = true;
                            lblspace.Visible = true;
                            btnPreviousGvSearch.Visible = false;
                            btnNextGvSearch.Visible = false;

                        }
                        else
                        {
                            txtTotalPage.Visible = true;
                            lblspace.Visible = true;
                            txtNumberGvSearch.Visible = true;
                            btnNextGvSearch.Visible = true;
                            btnPreviousGvSearch.Visible = false;
                        }

                    }
                    else
                    {
                        pnlDetail.Visible = true;
                        //  btnExportExcel.Visible = false;
                        btnGroupApprove.Visible = false;
                        txtTotalPage.Visible = true;
                        txtTotalPage.Text = "1";
                        txtNumberGvSearch.Text = "1";
                        lblspace.Visible = true;
                        txtNumberGvSearch.Visible = true;
                        btnNextGvSearch.Visible = false;
                        btnPreviousGvSearch.Visible = false;
                        CheckBox CH_all = (CheckBox)gvSearchOfficerOIC.HeaderRow.FindControl("checkAll");
                        CH_all.Visible = false;
                        gvSearchOfficerOIC.Columns[0].Visible = false;
                    }
                }
                else
                {
                    UCModalError.ShowMessageError = res.ErrorMsg;
                    UCModalError.ShowModalError();
                    return;
                }
                gvSearchOfficerOIC.Columns[0].Visible = false;
            }

            if (gvSearchOfficerOIC.Rows.Count > 0)
            {
                btnExportExcel.Visible = true;
            }
            else
            {
                btnExportExcel.Visible = false;
            }
            var data = new List<DTO.RegSearchOfficer>();

            data.Add(new DTO.RegSearchOfficer
            {
                MemberType = ddlSearchMemberType.SelectedValue,
                status = ddlSearchStatus.SelectedValue,
                Idcard = txtIDCard.Text,
                FName = txtFirstName.Text,
                LName = txtLastName.Text,
                Email = txtEmail.Text,
                startDate = sdate,
                endDate = tdate,
                CurrentPage = txtNumberGvSearch.Text,
                txt_startDate = txtstartDate.Text,
                txt_endDate=txttoDate.Text,
            });
            Session["ListRegSearch"] = data;
        }


                #region Code p'Tik
                //var ls_person = bizPerson.GetPersonByCriteriaAtPage(txtFirstName.Text, txtLastName.Text, txtIDCard.Text, 
                //                    ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue, resultPage,
                //                                        PAGE_SIZE);
                //var ls_register = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                //                                        txtLastName.Text,
                //                                        txtIDCard.Text,
                //                                        ddlSearchMemberType.SelectedValue,
                //                                        txtEmail.Text,       
                //                                        "",
                //                                        ddlSearchStatus.SelectedValue,
                //                                        resultPage,
                //                                        PAGE_SIZE,"1");
                //if ((!ls_person.IsError) && (!ls_register.IsError) )                        
                //{
                //    DataTable data = ls_person.DataResponse.Tables[0];
                //    data.Merge(ls_register.DataResponse.Tables[0]);

                //    if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                //            ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                //    {
                //        gvSearchOfficerOIC.DataSource = data;
                //        gvSearchOfficerOIC.DataBind();

                //        if (gvSearchOfficerOIC.Rows.Count > 0)
                //        {
                //            for (int i = 0; i < gvSearchOfficerOIC.Rows.Count; i++)
                //            {
                //                gvSearchOfficerOIC.Columns[0].Visible = true;
                //            }
                //            btnGroupApprove.Visible = true;
                //        }
                //        else
                //        {
                //            btnGroupApprove.Visible = false;
                //        }
                //    }
                //    else
                //    {
                //        gvSearchOfficerOIC.DataSource = data;
                //        gvSearchOfficerOIC.DataBind();
                //        gvSearchOfficerOIC.Columns[0].Visible = false;
                //        btnGroupApprove.Visible = false;
                //    }
                //}
                //else
                //{
                //    if(ls_person.IsError)
                //        UCModalError.ShowMessageError = ls_person.ErrorMsg;
                //    if(ls_register.IsError)
                //        UCModalError.ShowMessageError = ls_register.ErrorMsg;
                //    UCModalError.ShowModalError();
                //}
  
            //else if (ddlSearchStatus.SelectedValue.Equals(""))
            //{
            //    UCModalError.ShowMessageError = "กรุณาเลือกสถานะอนุมัติ";
            //    UCModalError.ShowModalError();

            //    var ls = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue);
            //    gvSearchOfficerOIC.DataSource = null;
            //    gvSearchOfficerOIC.DataBind();
            //    btnGroupApprove.Visible = false;
            //}
                #endregion
            



        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //Don't forget null check before !!!
            if((txtstartDate.Text != null) && (txtstartDate.Text != "" ) && (txttoDate.Text != null) && (txttoDate.Text != ""))
            {
                if (Convert.ToDateTime(txtstartDate.Text) > Convert.ToDateTime(txttoDate.Text))
                {
                    UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                    UCModalError.ShowModalError();
                    pnlDetail.Visible = false;
                    return;
                }
                else
                {
                    txtNumberGvSearch.Text = "1";
                    Session["status"] = ddlSearchStatus.SelectedValue;
                    ListID = new List<string>();
                    GetSearchOfficerOIC();
                    txtPage.Text = Convert.ToString(base.PAGE_SIZE_Key);
                    //pagegin();
                }
            }
            
        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lbl = (Label)gr.FindControl("lblID");
            Label lblType = (Label)gr.FindControl("lblMemberType");

            Session["PersonID"] = lbl.Text;
            String type = lblType.Text.Trim();
            String personId = Session["PersonID"].ToString();


            if (this.RegStatus.ToInt() <= 3)
            {
                switch (type)
                {
                    case "บุคคลทั่วไป (ตัวแทน/นายหน้า)":
                        Response.Redirect(String.Format("~/Register/RegisGeneral.aspx?&PersonIdQuery={0}&Mode={1}", personId, "T"));
                        break;
                    case "บริษัทประกัน":
                        Response.Redirect(String.Format("~/Register/RegisCompany.aspx?&PersonIdQuery={0}&Mode={1}", personId, "T"));
                        break;
                    case "สมาคม":
                        Response.Redirect(String.Format("~/Register/RegisAssociate.aspx?&PersonIdQuery={0}&Mode={1}", personId, "T"));
                        break;

                    default:
                        break;
                }
            }
            if (this.RegStatus.ToInt() >= 4)
            {
                switch (type)
                {
                    case "บุคคลทั่วไป (ตัวแทน/นายหน้า)":
                       // Response.Redirect(String.Format("~/Register/Reg_Person.aspx?&PersonIdQuery={0}&Mode={1}", personId, "V" + "&S=" + this.RegStatus + ""));
                        Response.Redirect(String.Format("~/Register/RegisGeneral.aspx?&PersonIdQuery={0}&Mode={1}", personId, "T"));
                        break;
                    case "บริษัทประกัน":
                        //Response.Redirect(String.Format("~/Register/Reg_Co.aspx?&PersonIdQuery={0}&Mode={1}", personId, "V" + "&S=" + this.RegStatus + ""));
                        Response.Redirect(String.Format("~/Register/RegisCompany.aspx?&PersonIdQuery={0}&Mode={1}", personId, "T"));
                        break;
                    case "สมาคม":
                        //Response.Redirect(String.Format("~/Register/Reg_Assoc.aspx?&PersonIdQuery={0}&Mode={1}", personId, "V" + "&S=" + this.RegStatus + ""));
                        Response.Redirect(String.Format("~/Register/RegisAssociate.aspx?&PersonIdQuery={0}&Mode={1}", personId, "T"));
                        break;

                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// Edited by Nattapong @08/08/2557
        /// Send Param to Response.Redirect page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void hplApprove_Click(object sender, EventArgs e)
        {
            RegistrationBiz regbiz = new RegistrationBiz();
            DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();

            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lbl = (Label)gr.FindControl("lblID");
            Label lblStatusCode = (Label)gr.FindControl("lblStatusCode");
            LinkButton lnk = (LinkButton)gr.FindControl("hplApprove");
            Session["PersonID"] = lbl.Text;

            Label lblIDCard = (Label)gr.FindControl("lblIDCard");
            Session["PersonalIDCard"] = lblIDCard.Text;

            if (ddlSearchStatus.SelectedIndex == 7)
            {
                if (lblStatusCode.Text == "4")
                {
                    this.RegStatus = "4";
                    Response.Redirect("~/Person/Edit_Reg_Person.aspx?S=" + this.RegStatus + "");
                }
                else
                {
                    this.RegStatus = "1";
                    if (ddlSearchMemberType.SelectedValue.Equals("2") || ddlSearchMemberType.SelectedValue.Equals("3"))
                    {
                        res = regbiz.GetByIdCard(this.PersonalIDCard);
                        if (res.DataResponse != null)
                        {
                            this.ApproveStatus = res.DataResponse.STATUS;
                            string ImportS =  ChkImportStatus(res.DataResponse.IMPORT_STATUS);
                            if (ImportS.Equals("Y"))
                            {
                                Response.Redirect("~/Register/RegisApproveCompare.aspx?S=" + this.RegStatus + "");
                            }
                            else
                            {
                                Response.Redirect("~/Register/regApprovCompany.aspx?S=" + this.RegStatus + "");
                            }
                        }
                        //Response.Redirect("~/Register/regApprovCompany.aspx?S=" + this.RegStatus + "");
                    }
                    else
                    {
                        res = regbiz.GetByIdCard(this.PersonalIDCard);
                        if (res.DataResponse != null)
                        {
                            this.ApproveStatus = res.DataResponse.STATUS;
                            string ImportS = ChkImportStatus(res.DataResponse.IMPORT_STATUS);
                            if (ImportS.Equals("Y"))
                            {
                                Response.Redirect("~/Register/RegisApproveCompare.aspx?S=" + this.RegStatus + "");
                            }
                            else
                            {
                                Response.Redirect("~/Register/regApproveOfficerOic.aspx?S=" + this.RegStatus + "");
                            }
                        }

                        //Response.Redirect("~/Register/regApproveOfficerOic.aspx?S=" + this.RegStatus + "");
                    }
                }
            
            }
            else
            {
                //Status = 4 = รออนุมัติ(แก้ไข)
                if (this.RegStatus == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString())
                {                  
                  
                        Response.Redirect("~/Person/Edit_Reg_Person.aspx?S=" + this.RegStatus + "");
                   
                }
                else
                {
                    if (ddlSearchMemberType.SelectedValue.Equals("2") || ddlSearchMemberType.SelectedValue.Equals("3"))
                    {
                        res = regbiz.GetByIdCard(this.PersonalIDCard);
                        if (res.DataResponse != null)
                        {
                            this.ApproveStatus = res.DataResponse.STATUS;
                            string ImportS = ChkImportStatus(res.DataResponse.IMPORT_STATUS);
                            if (ImportS.Equals("Y"))
                            {
                                Response.Redirect("~/Register/RegisApproveCompare.aspx?S=" + this.RegStatus + "");
                            }
                            else
                            {
                                Response.Redirect("~/Register/regApprovCompany.aspx?S=" + this.RegStatus + "");
                            }
                        }
                        //Response.Redirect("~/Register/regApprovCompany.aspx?S=" + this.RegStatus + "");
                    }
                    else
                    {
                        res = regbiz.GetByIdCard(this.PersonalIDCard);
                        if (res.DataResponse != null)
                        {
                            this.ApproveStatus = res.DataResponse.STATUS;
                            string ImportS = ChkImportStatus(res.DataResponse.IMPORT_STATUS);
                            if (ImportS.Equals("Y"))
                            {
                                Response.Redirect("~/Register/RegisApproveCompare.aspx?S=" + this.RegStatus + "");
                            }
                            else
                            {
                                Response.Redirect("~/Register/regApproveOfficerOic.aspx?S=" + this.RegStatus + "");
                            }
                        }
                        //Response.Redirect("~/Register/regApproveOfficerOic.aspx?S=" + this.RegStatus + "");
                    }
                }
            }
        }

        /// <summary>
        /// Last Update 17/7/2557
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGroupApprove_Click(object sender, EventArgs e)
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

            string status=Session["status"].ToString();

            #region Get MemberType From Control @Nattapong
            //string RegMemType = string.Empty;
            //if (ddlSearchMemberType.SelectedItem.Text != "")
            //{
            //    switch (ddlSearchMemberType.SelectedItem.Text)
            //    {
            //        case "บุคคลทั่วไป":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บุคคลทั่วไป/ตัวแทน/นายหน้า":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
            //            break;
            //        case "บริษัทประกัน":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
            //            break;
            //        case "บริษัท":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
            //            break;
            //        case "สมาคม":
            //            RegMemType = Convert.ToString((int)DTO.MemberType.Association.GetEnumValue());
            //            break;
            //    }
            //}

            #endregion

            if (ListID == null)
            {
                return;
            }

            if (ListID.Count == 0)
            {
                string AlertWaitingForApprove = "alert('"+Resources.infoRegSearchOfficerOIC_006+"')";
                ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
            }

            if (ListID.Count != 0)
            {
                
                var res = new DTO.ResponseMessage<bool>();

                if (status == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString())
                {
                    string userid = UserProfile.Id;
                    BLL.PersonBiz perbiz = new BLL.PersonBiz();
                    perbiz.PersonEditApprove(ListID, "อนุมัติกลุ่ม",userid);
                }
                else
                {
                    string userid = UserProfile.Id;
                    biz.RegistrationApprove(ListID, "อนุมัติกลุ่ม", userid, ddlSearchMemberType.SelectedItem.Value);
                }


                if (!string.IsNullOrEmpty(res.ErrorMsg))
                {
                    //UCModalError.ShowMessageError = "ท่านได้ทำการอนุมัติกลุ่มไม่สำเร็จ";
                    //UCModalError.ShowModalError();
                    string AlertWaitingForApprove = "alert('"+ Resources.errorReg_OIC_Search_001 +"')";
                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
                }
                else
                {
                    //string AlertWaitingForApprove = "alert('ท่านได้ทำการอนุมัติกลุ่มเรียบร้อยแล้ว');window.location.assign('../Register/regSearchOfficerOIC.aspx')";
                    string AlertWaitingForApprove = "alert('"+ Resources.infoRegSearchOfficerOIC_007 +"')";
                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertWaitingForApprove, true);
                    ListID = new List<string>();
                    GetSearchOfficerOIC();

                    //UCModalSuccess.ShowMessageSuccess = "ท่านได้ทำการอนุมัติกลุ่มเรียบร้อยแล้วครับ";
                    //UCModalSuccess.ShowModalSuccess();
                }

            }
        }

        bool b_check = true;
        CheckBox check_all_header;
        protected void gvSearchOfficerOIC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                check_all_header = (CheckBox)e.Row.FindControl("checkAll");
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (b_check)
                {
                    check_all_header.Checked = true;
                }               
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //check ว่าเลือกข้อมูลหรือไม่
                CheckBox check = (CheckBox)e.Row.FindControl("chkSelect");
                Label lblID = (Label)e.Row.FindControl("lblID");
                var l = ListID.FirstOrDefault(x => x == lblID.Text);
                if (l != null)
                {
                    check.Checked = true;
                }
                else
                {
                    check.Checked = false;
                    b_check = false;
                }         

                Label status = ((Label)e.Row.FindControl("lblStatusCode"));
                Label lblPhone = (Label)e.Row.FindControl("lblTel");
                LinkButton approve = ((LinkButton)e.Row.FindControl("hplApprove"));
                LinkButton view = ((LinkButton)e.Row.FindControl("hplView"));
                lblPhone.Text = LocalTelephoneNumberHelper.GetPhoneNumber(lblPhone.Text);


                if (ddlSearchStatus.SelectedValue == "0")
                {
                    if (status != null) {
                        if ((status.Text.Trim() == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                         status.Text.Trim() == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                        {
                            approve.Visible = true;
                            view.Visible = false;
                        }
                        else
                        {
                            approve.Visible = false;
                        } 
                    }
                }
                else {
                    if ((ddlSearchStatus.SelectedValue == DTO.RegistrationStatus.WaitForApprove.GetEnumValue().ToString() ||
                         ddlSearchStatus.SelectedValue == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString()))
                    {
                        approve.Visible = true;
                        view.Visible = false;
                    }
                    else
                    {
                        approve.Visible = false;
                    }
                }
                
            }
        }
        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() - 1;

            txtNumberGvSearch.Text = result == 0 ? "1" : result.ToString();
            if (result.ToString() == "1")
            {
                btnPreviousGvSearch.Visible = false;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;
              
            }
            else if (Convert.ToInt32(result) > 1)
            {
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;
               
            }
            BindPage();

        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            var result = txtNumberGvSearch.Text.ToInt() + 1;
            if (Convert.ToInt32(result) < Convert.ToInt32(txtTotalPage.Text))
            {
                txtNumberGvSearch.Text = result.ToString();
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;
                btnNextGvSearch.Visible = true;

            }
            else
            {
                txtNumberGvSearch.Text = txtTotalPage.Text;
                btnNextGvSearch.Visible = false;
                btnPreviousGvSearch.Visible = true;
                txtNumberGvSearch.Visible = true;

            }
            BindPage();
        }


        protected void BindPage()
        {
            if (txtPage.Text == "" || txtPage.Text == "0")
            {
                txtPage.Text = "20";
            }

          

            PAGE_SIZE = txtPage.Text.ToInt();
            var resultPage = txtNumberGvSearch.Text.ToInt();          
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            BLL.PersonBiz bizPerson = new PersonBiz();

            DateTime? stdate;
            DateTime? tdate;

            if (!string.IsNullOrEmpty(txtstartDate.Text))
            {
                stdate = Convert.ToDateTime(txtstartDate.Text);
            }
            else
            {
                stdate = null;
            }
            if (!string.IsNullOrEmpty(txttoDate.Text))
            {
                tdate = Convert.ToDateTime(txttoDate.Text);

            }
            else
            {
                tdate = null;
            }
            if (ddlSearchStatus.SelectedValue.Equals("1") || ddlSearchStatus.SelectedValue.Equals("2") || ddlSearchStatus.SelectedValue.Equals("3"))
            {

          
            
                var res = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                  txtLastName.Text,
                                                  stdate,
                                                  tdate,
                                                  txtIDCard.Text,
                                                  ddlSearchMemberType.SelectedValue,
                                                  txtEmail.Text,
                                                  "",
                                                  ddlSearchStatus.SelectedValue,
                                                  resultPage,
                                                  PAGE_SIZE, "2");



                gvSearchOfficerOIC.DataSource = res.DataResponse;
                gvSearchOfficerOIC.DataBind();

              
            }
            else if (ddlSearchStatus.SelectedValue.Equals("4"))
            {

                var result = bizPerson.GetPersonTempEditByCriteria(txtFirstName.Text, txtLastName.Text, sdate, tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue, resultPage,
                                                          PAGE_SIZE, "2");

                gvSearchOfficerOIC.DataSource = result.DataResponse;
                gvSearchOfficerOIC.DataBind();
            
            }
            else if (ddlSearchStatus.SelectedValue.Equals("5") || ddlSearchStatus.SelectedValue.Equals("6"))
            {
                var result = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text,sdate,tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue,
                                               resultPage, PAGE_SIZE, "2");
                gvSearchOfficerOIC.DataSource = result.DataResponse;
                gvSearchOfficerOIC.DataBind();
            }
            else if (ddlSearchStatus.SelectedValue.Equals("0"))
            {

                var result = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                         txtLastName.Text,
                                                         stdate,
                                                         tdate,
                                                         txtIDCard.Text,
                                                         ddlSearchMemberType.SelectedValue,
                                                         txtEmail.Text,
                                                         "",
                                                         ddlSearchStatus.SelectedValue,
                                                         resultPage,
                                                         PAGE_SIZE, "4");
                gvSearchOfficerOIC.DataSource = result.DataResponse;
                gvSearchOfficerOIC.DataBind();
            }
        }
    

        private List<string> ListID
                {
                    get
                    {
                        return Session["ID"] == null
                                      ? new List<string>()
                                      : (List<string>)Session["ID"];
                    }
                    set
                    {
                        Session["ID"] = value;
                    }
                }

        //เลือก checkbox แล้วเก็บ id ใส่ Session
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox check = (CheckBox)sender;
            GridViewRow thisGridViewRow = (GridViewRow)check.Parent.Parent;
            GridView gv = (GridView)thisGridViewRow.Parent.Parent;
            check_all_header = (CheckBox)gv.HeaderRow.FindControl("checkAll");
            Label lblID = (Label)thisGridViewRow.FindControl("lblID");
            if (check.Checked)
            {
                ListID.Add(lblID.Text);
            }
            else
            {
                ListID.Remove(lblID.Text);
                check_all_header.Checked = false;
            }
            btnGroupApprove.Enabled = true;
        }

        //เลือก checkall 
        protected void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkcall = (CheckBox)sender;
            foreach (GridViewRow row in gvSearchOfficerOIC.Rows)
            {
                if (checkcall.Checked)
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (!chkSelect.Checked)
                    {
                        ListID.Add(((Label)row.FindControl("lblID")).Text);
                    }
                    chkSelect.Checked = true;

                }
                else
                {
                    CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                    if (chkSelect.Checked)
                    {
                        ListID.Remove(((Label)row.FindControl("lblID")).Text);
                    }
                    chkSelect.Checked = false;
                }
            }

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ddlSearchStatus.SelectedIndex=0;
            ddlSearchMemberType.SelectedIndex = 0;
            txtIDCard.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
            txtstartDate.Text = string.Empty;
            txttoDate.Text = string.Empty;
            sdate = null;
            tdate = null;
            pnlDetail.Visible = false;
            pnPage.Visible = false;
            DefaultData();
        }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
                

                int total = lblrows.Text == "" ? 0 : lblrows.Text.ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    upnSeachOIC.Update();
                }
                else
                {
                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ประเภทสมาชิก ",
                        ValueColumnsOne = ddlSearchMemberType.SelectedItem.Text,
                        NameColumnsTwo = "สถานะ ",
                        ValueColumnsTwo = ddlSearchStatus.SelectedItem.Text
                    });
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "เลขบัตรประชาชน ",
                        ValueColumnsOne = txtIDCard.Text,
                        NameColumnsTwo = "ชื่อ ",
                        ValueColumnsTwo = txtFirstName.Text
                    });
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "นามสกุล  ",
                        ValueColumnsOne = txtLastName.Text,
                        NameColumnsTwo = "อีเมล  ",
                        ValueColumnsTwo = txtEmail.Text
                    });
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "ตั้งแต่วันที่   ",
                        ValueColumnsOne = txtstartDate.Text,
                        NameColumnsTwo = "ถึงวันที่",
                        ValueColumnsTwo = txttoDate.Text
                    });

                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("เลขที่สมาชิก", "RUN_NO");
                    columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                    columns.Add("อีเมล", "EMAIL");
                    columns.Add("วันที่สมัคร", "CREATED_DATE");
                    columns.Add("ชื่อ", "NAMES");
                    columns.Add("นามสกุล", "LASTNAME");
                    columns.Add("ประเภทสมาชิก", "MEMBER_TYPE");
                    columns.Add("เบอร์โทรศัพท์", "TELEPHONE");
                    columns.Add("รหัสไปรษณีย์", "ZIP_CODE");
                    columns.Add("สถานะ", "STATUS_NAME");
                    columns.Add("ผู้อนุมัติ", "APPOVED_NAME");
                    BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
                    ExportBiz export = new ExportBiz();
                    BLL.PersonBiz bizPerson = new PersonBiz();
                    DateTime? stdate;
                    DateTime? tdate;
                    int[] colum = new int[] { 0, 13 };
                    if (!string.IsNullOrEmpty(txtstartDate.Text))
                    {
                        stdate = Convert.ToDateTime(txtstartDate.Text);
                    }
                    else
                    {
                        stdate = null;
                    }
                    if (!string.IsNullOrEmpty(txttoDate.Text))
                    {
                        tdate = Convert.ToDateTime(txttoDate.Text);

                    }
                    else
                    {
                        tdate = null;
                    }

                    var resultPage = txtNumberGvSearch.Text.ToInt();

                    if (ddlSearchStatus.SelectedValue.Equals("1") || ddlSearchStatus.SelectedValue.Equals("2") || ddlSearchStatus.SelectedValue.Equals("3"))
                    {

                        var res = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                          txtLastName.Text,
                                                          stdate,
                                                          tdate,
                                                          txtIDCard.Text,
                                                          ddlSearchMemberType.SelectedValue,
                                                          txtEmail.Text,
                                                          "",
                                                          ddlSearchStatus.SelectedValue,
                                                          1,
                                                          base.EXCEL_SIZE_Key, "2");


                        int count = res.DataResponse.Tables[0].Rows.Count;
                        export.CreateExcel(res.DataResponse.Tables[0], columns,header,base.UserProfile);

                    }
                    else if (ddlSearchStatus.SelectedValue.Equals("4"))
                    {

                        var result = bizPerson.GetPersonTempEditByCriteria(txtFirstName.Text, txtLastName.Text, sdate, tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue, 1,
                                                                  base.EXCEL_SIZE_Key, "2");

                        export.CreateExcel(result.DataResponse.Tables[0], columns,header,base.UserProfile);

                    }
                    else if (ddlSearchStatus.SelectedValue.Equals("5") || ddlSearchStatus.SelectedValue.Equals("6"))
                    {
                        var result = bizPerson.GetPersonByCriteria(txtFirstName.Text, txtLastName.Text, sdate, tdate, txtIDCard.Text, ddlSearchMemberType.SelectedValue, txtEmail.Text, "", ddlSearchStatus.SelectedValue,
                                                       1, base.EXCEL_SIZE_Key, "2");
                        export.CreateExcel(result.DataResponse.Tables[0], columns);
                    }
                    else if (ddlSearchStatus.SelectedValue.Equals("0"))
                    {
                        var result = biz.GetRegistrationsByCriteria(txtFirstName.Text,
                                                                 txtLastName.Text,
                                                                 stdate,
                                                                 tdate,
                                                                 txtIDCard.Text,
                                                                 ddlSearchMemberType.SelectedValue,
                                                                 txtEmail.Text,
                                                                 "",
                                                                 ddlSearchStatus.SelectedValue,
                                                                 1,
                                                                 base.EXCEL_SIZE_Key, "4");
                        export.CreateExcel(result.DataResponse.Tables[0], columns,header,base.UserProfile);

                    }
                }
            }
            //catch { }

        //}

        protected void btnGo_Click(object sender, EventArgs e)
        {
             txtNumberGvSearch.Text = "1";
            Session["status"] = ddlSearchStatus.SelectedValue;
            ListID = new List<string>();
            if (txtPage.Text == "" || txtPage.Text == "0")
            {
                txtPage.Text = "20";
            }
            PAGE_SIZE = txtPage.Text.ToInt();
            GetSearchOfficerOIC();
        }

        protected string ChkImportStatus(string ImportS)
        {
            if (ImportS == null)
            {
                ImportS = "N";
            }
            return ImportS;
        }
    }
}
