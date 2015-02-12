using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.BLL;
using System.Data;
using IAS.Properties;

namespace IAS.Reporting
{
    public partial class RcvHistory : basepage
    {
        int PageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                GetLicenseType();
                bluediv.Visible = false;
                TXTrowperpage.Visible = false;
                txtNumberGvSearch.Text = "0";
                rowPerpage.Text = Convert.ToString(PageSize);
            }
        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvSearch":
                    lblTotal.Text =  Convert.ToString(total_row_count);
                    rows_per_page = (rows_per_page == 0 ) ? 1 : rows_per_page;
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
        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            BindDataInGridView(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelSearch.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindDataInGridView(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelSearch.Update();
        }
        #endregion Pageing_milk

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = "0";
            UpdatePanelSearch.Update();
            BindDataInGridView(true);
            if (gvSearch.Rows.Count > 0)
            {
                //btnExportExcel.Visible = true; //milk
                btnExportExcel.Visible = false;
            }
            else{
                btnExportExcel.Visible = false;
            }
        }

        private void BindDataInGridView(Boolean CountAgain)
        {
            var biz = new BLL.PaymentBiz();

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

            if (CountAgain)
            {
                #region Page
                var CountPage = biz.GetReportNumberPrintBill(txtIDCard.Text, ddlLicenseType.SelectedValue, txtFirstName.Text, txtLastName.Text, resultPage, PageSize, true);
                

                if (CountPage.DataResponse != null)
                    if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                        VisibleGV(gvSearch, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                    }
                    else
                    {
                        VisibleGV(gvSearch, 0, Convert.ToInt32(rowPerpage.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        txtTotalPage.Text = "1";
                    }
                #endregion Page
            }



            var res = biz.GetReportNumberPrintBill(txtIDCard.Text, ddlLicenseType.SelectedValue, txtFirstName.Text, txtLastName.Text, resultPage, PageSize,false);

            if (res.IsError)
            {
                var errorMsg = res.ErrorMsg;

                AlertMessage.ShowAlertMessage(string.Empty, errorMsg);
            }
            else
            {

                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();
                bluediv.Visible = true;
              
            }
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
           
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetLicenseType()
        {
            var message = Resources.infoSysMessage_PleaseSelectAll;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetRequestLicenseType_NOias(message);

            BindToDDL(ddlLicenseType, ls.DataResponse.ToList());

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            ddlLicenseType.SelectedIndex = 0;
            txtFirstName.Text = string.Empty;
            txtIDCard.Text = string.Empty;
            txtLastName.Text = string.Empty;
            bluediv.Visible = false;

            btnPreviousGvSearch.Visible = false;
            btnNextGvSearch.Visible = false;
            txtNumberGvSearch.Text = "0";
            txtNumberGvSearch.Visible = true;
            txtTotalPage.Visible = false;
            lblTotal.Visible = false;
            rowPerpage.Text = PageSize.ToString();
            btnExportExcel.Visible = false;
        }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            int total = lblTotal.Text == "" ? 0 : lblTotal.Text.ToInt();
            if (total > base.EXCEL_SIZE_Key)
            {
                UCModalError1.ShowMessageError = SysMessage.ExcelSizeError;
                UCModalError1.ShowModalError();
                UpdatePanelSearch.Update();
            }
            else
            {
                try
                {
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับที่", "RUN_NO");
                    columns.Add("ประเภทใบเสร็จ", "PETITION_TYPE_NAME");
                    columns.Add("เลขที่ใบเสร็จ", "RECEIPT_NO");
                    columns.Add("ชื่อ-นามสกุล", "FLNAME");
                    columns.Add("เลขบัตรประชาชน", "ID_CARD_NO");
                    columns.Add("วันที่สั่งจ่าย", "PAYMENT_DATE");
                    columns.Add("วันที่ชำระเงิน", "ORDER_DATE");
                    columns.Add("เลขที่ใบอนุญาต", "LICENSE_NO");
                    columns.Add("จำนวนเงิน", "AMOUNT");
                    columns.Add("จำนวนครั้งที่พิมพ์", "PRINT_TIMES");

                    ExportBiz export = new ExportBiz();
                    var biz = new BLL.PaymentBiz();
                    var res = biz.GetReportNumberPrintBill(txtIDCard.Text, ddlLicenseType.SelectedValue, txtFirstName.Text, txtLastName.Text, 1, base.EXCEL_SIZE_Key, false);
                    export.CreateExcel(res.DataResponse, columns);
                    
                }
                catch { }
            }
        }
        protected void lnkShowDetail_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowDetail")
                {
                    string RcvName = e.CommandArgument.ToString();
                    ModalPopupExtender1.Show();
                    txtRcv.Text = RcvName;
                    GetReceiptHis(RcvName);
                }
            }
            catch
            {
            }

        }
        protected void GetReceiptHis(string RcvID)
        {
            try
            {
                var biz = new BLL.PaymentBiz();
                DTO.ResponseService<DataSet> res = biz.GetRcvHisDetail(RcvID, "L","1","100");
                if (res.IsError)
                {
                    AlertMessage.ShowAlertMessage(string.Empty, res.ErrorMsg);
                }
                else
                {
                    lblCount.Text = "(ดาวน์โหลดทั้งหมด <u>" + Convert.ToString(res.DataResponse.Tables[0].Rows.Count) + "</u> ครั้ง)";
                    pop_FROM_gv.DataSource = res.DataResponse;
                    pop_FROM_gv.DataBind();
                }
            }
            catch
            { 
            }
        }
        public override void VerifyRenderingInServerForm(Control control) { }
    }
}
