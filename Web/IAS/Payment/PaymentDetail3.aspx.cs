using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Data;
using AjaxControlToolkit;
using IAS.BLL;
using System.Text.RegularExpressions;
using IAS.Properties;

namespace IAS.Payment
{
    public partial class PaymentDetail3 : basepage
    {
       int PageSize =  0;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtEndDate.Attributes.Add("readonly", "true");
            txtStartDate.Attributes.Add("readonly", "true");
            //ScriptManager.RegisterStartupScript(this, GetType(), "textreadonly", "setdate();", true);
            if (!Page.IsPostBack)
            {
                PageSize = PAGE_SIZE_Key;
                //PageSize = PAGE_SIZE_Key;
                DefaultData();
               // GetExam_group();
                TXTrowperpage.Visible = false;
                txtNumberGvSearch.Text = "0";
                rowPerpage.Text = Convert.ToString(PageSize);
            }
        }
        protected void DefaultData()
        {

            txtStartDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtStartDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            txtEndDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
            txtEndDate.Text = DateUtil.dd_MM_yyyy_Now_TH;
            bludDiv.Visible = false;
            btnPreviousGvSearch.Visible = false;
            btnNextGvSearch.Visible = false;
            txtNumberGvSearch.Text = "0";
            txtNumberGvSearch.Visible = true;
            txtTotalPage.Visible = false;
            lblTotal.Visible = false;

            ddlType.Enabled = false;
            if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                ddlType.SelectedValue = "A";
                gvSearch.Visible = false;
                GetAssociation();
                getPlace("A");
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
            {
                ddlType.SelectedValue = "G";
                gvSearch.Visible = false;
                GetExamGRoup();
                getPlace("G");
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue()
                || base.UserProfile.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()
                || base.UserProfile.MemberType == DTO.RegistrationType.OICFinace.GetEnumValue())
            {

                ddlExamPlaceCode.Items.Clear();
                ddlGroupExam.Items.Clear();
                ddlType.SelectedValue = "";
                ddlType.Enabled = true;
                gvSearch.Visible = false;
            }
           
        }

        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvSearch":
                    lblTotal.Text = "จำนวน <b>" + Convert.ToString(total_row_count) + "</b> รายการ";
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

        //protected void GetExam_group()
        //{
        //    int type = base.UserProfile.MemberType;

        //    string id = base.UserProfile.CompCode;
        //    string SQLtemp = string.Empty;
        //    var biz = new BLL.PaymentBiz();
        //    var res = biz.GetGroupExam(type, id);
            

        //    if (res.IsError)
        //    {
        //        UCModalError.ShowMessageError = res.ErrorMsg;
        //        UCModalError.ShowModalError();
        //    }
        //    else
        //    {

        //        DataSet ds = res.DataResponse;
        //        DataTable dt = ds.Tables[0];
        //        DataRow dr = dt.Rows[0];
        //        bludDiv.Visible = false;
                
        //        ddlGroupExam.DataTextField = "GROUP_NAME";
        //        ddlGroupExam.DataValueField = "GROUP_ID";
        //        ddlGroupExam.DataSource = dt.DataSet;
                
        //        ddlGroupExam.DataBind();
        //        ddlGroupExam.SelectedIndex = 0;
        //          var resExam =   biz.GetExamCode(ddlGroupExam.SelectedValue);
        //          bindExamPlace(resExam.DataResponse);

        //    }
        //}
       
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
            if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                UCModalError.ShowMessageError = Resources.errorApplicantNoPay_004;
                UCModalError.ShowModalError();
                PnlDetailSearchGridView.Visible = false;
            }
            else
            {
                string errortxt = "";
                if (ddlType.SelectedIndex == 0)
                {
                   errortxt = "- กรุณาเลือกสังกัด";
                }
                 if (ddlGroupExam.SelectedIndex == 0)
                {
                    errortxt = errortxt == "" ? "- กรุณาเลือกหน่วยงานจัดสอบ/สมาคม" : errortxt + "<br /> " + "- กรุณาเลือกหน่วยงานจัดสอบ/สมาคม";
                }
                 if (ddlExamPlaceCode.SelectedIndex == 0)
                {
                    errortxt = errortxt == "" ? "- กรุณาเลือกสนามสอบ" : errortxt + "<br /> " + "- กรุณาเลือกสนามสอบ";
                }

                 if (errortxt != "")
                 {
                     UCModalError.ShowMessageError = errortxt;
                     UCModalError.ShowModalError();
                 }
                 else
                 {
                     PnlDetailSearchGridView.Visible = true;
                     txtNumberGvSearch.Text = "0";
                     UpdatePanelSearch.Update();
                     BindDataInGridView(true);
                 }
                
                //if (gvSearch.Rows.Count > 0)
                //{
                //    btnExportExcel.Visible = true;
                //}
                //else
                //{
                //    btnExportExcel.Visible = false;
                //}
            }
        }

        protected void ddlGroupExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPlace(ddlType.SelectedValue.ToString());
        }

        protected void getPlace(string type)
        {
            string id = ddlGroupExam.SelectedValue.ToString();
            string SQLtemp = string.Empty;
            switch (type)
            {
                case "A":
                        var biz = new BLL.DataCenterBiz();
                        var res = biz.GetExamPlace_UnderAssocicate("",id);
                    if (res.IsError)
                    {
                        UCModalError.ShowMessageError = res.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
               
                        bludDiv.Visible = false;
                        DataSet ds = res.DataResponse;
                        DataTable dt = ds.Tables[0];
                        ddlExamPlaceCode.DataTextField = "name";
                        ddlExamPlaceCode.DataValueField = "Id";
                        ddlExamPlaceCode.DataSource = dt.DataSet;
                        ddlExamPlaceCode.DataBind();
                        ddlExamPlaceCode.Items.Insert(0, SysMessage.DefaultSelecting);
                        ddlExamPlaceCode.SelectedIndex = 0;
                    }
                    break;
                case "G":
                        var bizz = new BLL.PaymentBiz();
                    var ress = bizz.GetExamCode(id);
                    if (ress.IsError)
                    {
                        UCModalError.ShowMessageError = ress.ErrorMsg;
                        UCModalError.ShowModalError();
                    }
                    else
                    {
                        DataSet dss = ress.DataResponse;
                        DataTable dtt = dss.Tables[0];
                        ddlExamPlaceCode.DataTextField = "PLACE_NAME";
                        ddlExamPlaceCode.DataValueField = "PLACE_ID";
                        ddlExamPlaceCode.DataSource = dtt.DataSet;
                        ddlExamPlaceCode.DataBind();
                        ddlExamPlaceCode.Items.Insert(0, SysMessage.DefaultSelecting);
                        ddlExamPlaceCode.SelectedIndex = 0;
                    }
                            
                    break;
                default:

                    break;
            }
        }
     
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblPaymentNoGv = (Label)gr.FindControl("lblPaymentNoGv");
            Label lblHeadNoGv = (Label)gr.FindControl("lblHeadNoGv");
            Label lblOrderGv = (Label)gr.FindControl("lblOrderPayGv");
            Label lblPathFile = (Label)gr.FindControl("lblPathFile");
            var data2 = new List<DTO.SubPaymentDetail>();
            data2.Add(new DTO.SubPaymentDetail
            {
                HEAD_REQUEST_NO = lblHeadNoGv.Text,
                PAYMENT_NO = lblPaymentNoGv.Text,
                Click = "Print"
            });
            var biz = new BLL.PaymentBiz();
            var resPrint = biz.PrintDownloadCount(data2.ToArray(),"",base.UserId);
            string SumPath = lblPathFile.Text + "-P";
            Session["FileName"] = SumPath;

            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร",
           "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);
            //String script = "window.open('SavePdfFileFromStream.aspx', 'myPopup')";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "someId", script, true);
            UpdatePanelSearch.Update();

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
           
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            Label lblPaymentNoGv = (Label)gr.FindControl("lblPaymentNoGv");
            Label lblHeadNoGv = (Label)gr.FindControl("lblHeadNoGv");
            Label lblOrderGv = (Label)gr.FindControl("lblOrderPayGv");
            Label lblPathFile = (Label)gr.FindControl("lblPathFile");
            var data2 = new List<DTO.SubPaymentDetail>();
            data2.Add(new DTO.SubPaymentDetail
            {
                HEAD_REQUEST_NO = lblHeadNoGv.Text,
                PAYMENT_NO = lblPaymentNoGv.Text,
                Click = "Download"
            });
            var biz = new BLL.PaymentBiz();
            var resDownload = biz.PrintDownloadCount(data2.ToArray(),"",base.UserId);
            string SumPath = lblPathFile.Text + "-D";
            Session["FileName"] = SumPath;

            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "เอกสาร",
           "window.open('../UserControl/SavePdfFileFromStream.aspx');", true);



            UpdatePanelSearch.Update();
        }
        private void BindDataInGridView(Boolean CountAgain)
        {
            var biz = new BLL.PaymentBiz();
           // var resultPage = txtNumberGvSearch.Text.ToInt();

            int Rpage = (txtNumberGvSearch.Text.Trim() == "") ? 0 : txtNumberGvSearch.Text.Trim().ToInt();
            int resultPage = (Rpage == 0) ? 1 : txtNumberGvSearch.Text.Trim().ToInt();
            resultPage = resultPage == 0 ? 1 : resultPage;

            if ((rowPerpage.Text.Trim() == null) || (rowPerpage.Text.Trim() == "") || (Convert.ToInt32(rowPerpage.Text.Trim()) == 0))
                {
                    rowPerpage.Text = (PageSize == 0)? Convert.ToString (PAGE_SIZE_Key):      PageSize.ToString();
                }
                else
                {
                    PageSize = Convert.ToInt32(rowPerpage.Text);
                }

            if (CountAgain)
            {
                #region Page
                var CountPage =  biz.GetPaymentDetailByGroup(base.UserProfile.MemberType, ddlGroupExam.SelectedValue.ToString(),
                ddlExamPlaceCode.SelectedValue.ToString(), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), resultPage, PageSize, true, base.UserProfile.CompCode);  
                   
              

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

            var res = biz.GetPaymentDetailByGroup(base.UserProfile.MemberType, ddlGroupExam.SelectedValue.ToString(),
                ddlExamPlaceCode.SelectedValue.ToString(), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), resultPage, (PageSize == 0) ? PAGE_SIZE_Key : PageSize, false,base.UserProfile.CompCode);


            if (res.IsError)
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                bludDiv.Visible = true;
                DataSet ds = res.DataResponse;
                gvSearch.Visible = true;
                gvSearch.DataSource = res.DataResponse;
                gvSearch.DataBind();
                UpdatePanelSearch.Update();

                if (res.DataResponse.Tables[0].Rows.Count>0)
                {
                    btnExportExcel.Visible = true;
                }
                else
                { 
                    btnExportExcel.Visible = false; 
                }
            }

        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var HeadNo = (Label)gr.FindControl("lblHeadNoGv");
            var PaymentNo = (Label)gr.FindControl("lblPaymentNoGv");
            MPDetail.Show();
            lblpaymentNo.Text = HeadNo.Text;
            var biz = new BLL.PaymentBiz();
            var res = biz.GetRecriptByHeadRequestNoAndPaymentNo(HeadNo.Text,PaymentNo.Text);


            if (res==null)
            {
                var errorMsg = res.ErrorMsg;

                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
            else
            {
                
                GVPopupReceipt.DataSource = res.DataResponse;
                GVPopupReceipt.DataBind();
                UpdatePanelSearch.Update();
            }
        }


        protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label OrderGv = (Label)e.Row.FindControl("lblOrderPayGv");
                Label txtPath = (Label)e.Row.FindControl("lblPathFile");
                LinkButton Print = (LinkButton)e.Row.FindControl("hplPrint");
                LinkButton Download = (LinkButton)e.Row.FindControl("linkDownload");

                OrderGv.Text = OrderGv.Text.Insert(6, " ").Insert(11, " ");
                if (txtPath.Text != "")
                {
                    Print.Visible = true;
                    Download.Visible = true;
                }
                else
                {
                    Print.Visible = false;
                    Download.Visible = false;
                }
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            DefaultData();
           
            btnExportExcel.Visible = false;
            
        }

        protected void btnExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int total = lblTotal.Text == "" ? 0 : Regex.Replace(lblTotal.Text, "[^0-9]", "").ToInt();
                if (total > base.EXCEL_SIZE_Key)
                {
                    UCModalError.ShowMessageError = SysMessage.ExcelSizeError;
                    UCModalError.ShowModalError();
                    UpdatePanelSearch.Update();
                }
                else
                {
                    ExportBiz export = new ExportBiz();
                    Dictionary<string, string> columns = new Dictionary<string, string>();
                    columns.Add("ลำดับ", "RUN_NO");
                    columns.Add("ประเภทการชำระ", "PETITION_TYPE_NAME");
                    columns.Add("บัตรประชาชน", "ID_CARD_NO");
                    columns.Add("ชื่อ", "FIRST_NAME");
                    columns.Add("นามสกุล", "LASTNAME");
                    columns.Add("เลขที่ใบสั่งจ่าย", "group_request_no");
                    columns.Add("รหัสรอบสอบ", "TESTING_NO");
                    columns.Add("วันที่ออกใบสั่งจ่าย", "CREATED_DATE");
                    columns.Add("วันที่ชำระเงิน", "payment_date");
                    columns.Add("เลขที่ใบเสร็จ", "RECEIPT_NO");

                    List<HeaderExcel> header = new List<HeaderExcel>();
                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "หน่วยงานจัดสอบ ",
                        ValueColumnsOne = ddlGroupExam.SelectedItem.Text,
                        NameColumnsTwo = "สนามสอบ ",
                        ValueColumnsTwo = ddlExamPlaceCode.SelectedItem.Text
                    });

                    header.Add(new HeaderExcel
                    {
                        NameColumnsOne = "วันที่สั่งจ่าย(เริ่ม) ",
                        ValueColumnsOne = txtStartDate.Text,
                        NameColumnsTwo = "วันที่สั่งจ่าย(สิ้นสุด) ",
                        ValueColumnsTwo = txtEndDate.Text
                    });

                    var biz = new BLL.PaymentBiz();
                    var res = biz.GetPaymentDetailByGroup(base.UserProfile.MemberType, ddlGroupExam.SelectedValue.ToString(),
                    ddlExamPlaceCode.SelectedValue.ToString(), Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), 1, base.EXCEL_SIZE_Key, false, base.UserProfile.CompCode);
                    export.CreateExcel(res.DataResponse.Tables[0],columns,header,base.UserProfile);
                }

            }
            catch { }       
        

        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlType.SelectedIndex == 0)
                {
                    ddlGroupExam.Items.Clear();
                    ddlGroupExam.DataBind();
                }
                else
                {
                    if (gvSearch.Visible == true)
                    {
                        PnlDetailSearchGridView.Visible = true;
                    }
                    else
                    {
                        PnlDetailSearchGridView.Visible = false;
                    }

                    if (ddlType.SelectedValue != "")
                    {
                        switch (ddlType.SelectedValue.ToString())
                        {
                            case "A":
                                GetAssociation();
                                break;
                            case "G":
                                GetExamGRoup();
                                break;
                            default:

                                break;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void GetExamGRoup()
        {
            try
            {
                string ComP = "";
                DataCenterBiz biz = new DataCenterBiz();
                if (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue() 
                    && base.UserProfile.MemberType != DTO.RegistrationType.OICAgent.GetEnumValue()
                    && base.UserProfile.MemberType != DTO.RegistrationType.OICFinace.GetEnumValue())
                    ComP = base.UserProfile.CompCode;

                var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, ComP);
                ddlGroupExam.DataValueField = "Id";
                ddlGroupExam.DataTextField = "Name";

                ddlGroupExam.DataSource = ls.DataResponse;
                ddlGroupExam.DataBind();

                ddlGroupExam.Items.Insert(0, SysMessage.DefaultSelecting);
                if (ComP != "")
                {
                    ddlGroupExam.SelectedValue = ComP;
                    ddlGroupExam.Enabled = false;
                }

            }
            catch
            {
            }
        }

        private void GetAssociation()
        {
            try
            {
                string AssC = "";
                DataCenterBiz biz = new DataCenterBiz();
                if (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue() 
                    && base.UserProfile.MemberType != DTO.RegistrationType.OICAgent.GetEnumValue()
                    && base.UserProfile.MemberType != DTO.RegistrationType.OICFinace.GetEnumValue())
                    AssC = base.UserProfile.CompCode;

                var ls = biz.GetAssociation(AssC);
                ddlGroupExam.DataValueField = "ASSOCIATION_CODE";
                ddlGroupExam.DataTextField = "ASSOCIATION_NAME";

                ddlGroupExam.DataSource = ls.DataResponse;
                ddlGroupExam.DataBind();

                ddlGroupExam.Items.Insert(0, SysMessage.DefaultSelecting);
                if (AssC != "")
                {
                    ddlGroupExam.SelectedValue = AssC;
                    ddlGroupExam.Enabled = false;
                }
            }
            catch
            {
            }
        }
        
    }
}
