using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using System.Threading;

namespace IAS.Applicant
{
    public partial class ImportCandidateDelay : basepage
    {

        public IAS.MasterPage.Site1 MasterSite
        {
            get
            {
                return (this.Page.Master as IAS.MasterPage.Site1);
            }
        }


        private int RowPerPage { get { return PAGE_SIZE_Key; } }
        private int NumberGvSearch = 1;
        private double TotalRows
        {
            get { return (Session["_TotalRows"] == null) ? double.Parse("0") : double.Parse(Session["_TotalRows"].ToString()); }
            set { Session["_TotalRows"] = value; }
        }

        //private DateTime? dtDateExam = DateTime.MinValue;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txtDateExam.Text = DateTime.Today.AddDays(1).ToString("dd/MM/yyyy");
                txtDateExam.Attributes.Add("readonly", "true");
                BindGrid();
            }

        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtRowsPerpage.Text = RowPerPage.ToString();
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtTestingNo.Text = string.Empty;
            txtDateExam.Text = string.Empty;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ExamScheduleBiz biz = new ExamScheduleBiz();
            try
            {
                DTO.ExamSchedule ent = new DTO.ExamSchedule();
                ent.TESTING_NO = txtSetTestingNo.Text;
                ent.EXAM_PLACE_CODE = hdfExamPlaceCode.Value;
                ent.PRIVILEGE_STATUS = rblSetImport.SelectedValue;

                var res = biz.UpdateExam(ent);
                if (!res.IsError)
                {
                    PopUpLicense.Hide();
                    this.MasterSite.ModelSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    this.MasterSite.ModelSuccess.ShowModalSuccess();
                    btnSearch_Click(sender, e);
                    //uplConditionDelay.Update();
                    UplPopUp.Update();
                }
                else
                {
                    PopUpLicense.Show();
                    this.MasterSite.ModelError.ShowMessageError = res.ErrorMsg;
                    this.MasterSite.ModelError.ShowModalError();
                    UplPopUp.Update();
                }
            }
            catch (Exception ex)
            {
                PopUpLicense.Show();
                this.MasterSite.ModelError.ShowMessageError = ex.Message;
                this.MasterSite.ModelError.ShowModalError();
                UplPopUp.Update();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }



        private void BindGrid()
        {
            TotalRows = TotalRow();
            ExamScheduleBiz biz = new ExamScheduleBiz();
            txtNumberGvSearch.Text = (txtTotalPage.Text == LastPage().ToString() ? txtNumberGvSearch.Text : "1");
            int page = int.Parse(txtNumberGvSearch.Text);
            gvCandidateDelay.DataSource = biz.GetExamLicenseByCriteria(txtTestingNo.Text, txtDateExam.Text, page, rowsPerPage(), false).DataResponse;
            gvCandidateDelay.DataBind();
            SetTextTotlaRows();
        }

        private double TotalRow()
        {
            ExamScheduleBiz biz = new ExamScheduleBiz();
            var res = biz.GetExamLicenseByCriteria(txtTestingNo.Text, txtDateExam.Text, 0, 0, true).DataResponse;
            return double.Parse(res.Tables[0].Rows[0][0].ToString());
        }

        private double LastPage()
        {
            double mod = TotalRows / rowsPerPage();
            double lp = Math.Ceiling(mod);
            return lp == 0 ? 1 : lp;
        }

        protected int rowsPerPage()
        {
            int _rpp = RowPerPage;
            if (txtRowsPerpage.Text != "")
                _rpp = int.Parse(txtRowsPerpage.Text);
            return _rpp;
        }

        protected void SetTextTotlaRows()
        {
            lblTotalRows.Text = String.Format("จำนวน {0} รายการ", TotalRows);
        }

        private void SetControlPage()
        {
            ctrPage.Visible = true;
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

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            int currPage = int.Parse(txtNumberGvSearch.Text) - 1;
            if (currPage > 0)
                txtNumberGvSearch.Text = currPage.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            int currPage = int.Parse(txtNumberGvSearch.Text) + 1;
            if (currPage <= LastPage())
                txtNumberGvSearch.Text = currPage.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void pageGo_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void gvCandidateDelay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPrivilegeStatus = e.Row.FindControl("lblPrivilegeStatus") as Label;
                Label lblPrivilegeStatusName = e.Row.FindControl("lblPrivilegeStatusName") as Label;
                if (!string.IsNullOrEmpty(lblPrivilegeStatus.Text))
                {
                    if (lblPrivilegeStatus.Text == "Y")
                    {
                        lblPrivilegeStatusName.Text = "เปิดสิทธิ์การนำเข้า";
                    }
                    if (lblPrivilegeStatus.Text == "N")
                    {
                        lblPrivilegeStatusName.Text = "ปิดสิทธิ์การนำเข้า";
                    }
                }
            }
        }
        protected void lbtnConfig_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            string testingNo = ((Label)gr.FindControl("lblTestingNo")).Text;
            string examPlaceCode = ((Label)gr.FindControl("lblExamPlaceCode")).Text;
            hdfExamPlaceCode.Value = examPlaceCode;
            BindPopUp(testingNo, examPlaceCode);
            PopUpLicense.Show();
            UplPopUp.Update();
        }


        protected void BindPopUp(string testingNo, string examPlaceCode)
        {
            try
            {
                ExamScheduleBiz biz = new ExamScheduleBiz();
                var res = biz.GetExamByTestingNoAndPlaceCode(testingNo, examPlaceCode);
                if (res.DataResponse != null)
                {
                    var ent = res.DataResponse;
                    txtSetTestingNo.Text = string.IsNullOrEmpty(ent.TESTING_NO) ? testingNo : ent.TESTING_NO;
                    if (!string.IsNullOrEmpty(ent.PRIVILEGE_STATUS))
                    {
                        rblSetImport.SelectedValue = ent.PRIVILEGE_STATUS;
                    }
                    else
                    {
                        rblSetImport.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBoxError(ex.Message);
            }

        }


    }
}