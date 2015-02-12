using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;

namespace IAS.Exam
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        class ExamFee
        {
            public string strPaymentType { get; set; }
            public string strIDNumber { get; set; }
            public string strFirstName { get; set; }
            public string strLastName { get; set; }
            public DateTime dChequeDate { get; set; }
            public string strCheque { get; set; }
            public string strBillNumber { get; set; }
        }

        private List<ExamFee> lstExamFee;

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
        }

        protected void ibtClearStartDate_Click(object sender, ImageClickEventArgs e)
        {
            txtStartDate.Text = string.Empty;
        }

        protected void ibtClearEndDate_Click(object sender, ImageClickEventArgs e)
        {
            txtEndDate.Text = string.Empty;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lstExamFee = new List<ExamFee>();
            lstExamFee.Add(new ExamFee { strPaymentType = "สมัครสอบ", strIDNumber = "3129485736183", strFirstName = "นาย เจริญ", strLastName = "มัธยัสถ์", dChequeDate = Convert.ToDateTime("01/01/2556"), strBillNumber = "123456/2556", strCheque = "5601000123" });
            lstExamFee.Add(new ExamFee { strPaymentType = "ขอใบอนุญาต", strIDNumber = "3109385735049", strFirstName = "นาย เอนก", strLastName = "ประสงค์", dChequeDate = Convert.ToDateTime("14/01/2556"), strBillNumber = "123457/2556", strCheque = "5601000124" });
            lstExamFee.Add(new ExamFee { strPaymentType = "ต่ออายุ", strIDNumber = "3590198794532", strFirstName = "นาย ทรัพย์", strLastName = "เปี่ยมสุข", dChequeDate = Convert.ToDateTime("20/02/2556"), strBillNumber = "123458/2556", strCheque = "5601000125" });

            gvSearch.DataSource = lstExamFee;
            gvSearch.DataBind();
            btnDownload.Visible = true;
            btnPrint.Visible = true;
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../Reporting/RcvPayment.aspx', '_blank');", true);
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            ddlTypePayment.SelectedIndex = 0;
            txtExamCode.Text = string.Empty;
            txtExamCode1.Text = string.Empty;
            txtExamCode0.Text = string.Empty;
            PnlDetailSearchGridView.Visible = false;
        }
    }
}