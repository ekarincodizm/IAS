using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.Properties;
using System.Data;
using IAS.Utils;

namespace IAS.Setting
{
    public partial class SetExamTime : basepage
    {
        LicenseTypeBiz biz = new LicenseTypeBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMin();
                GetHr();               
            }
        }
        int PageSize = 20;
        private void GetMin()
        {
            try 
            {
                string [] Min = new string[60];
                for (int i = 0; i < 60; i++)
                {
                    Min[i] = string.Format("{0:00}", i);
                }
                st_min.DataSource = Min;
                en_min.DataSource = Min;
                st_min.DataBind();
                en_min.DataBind();
                
            }
            catch
            { 
            
            }
        }

        private void GetHr()
        {
            try
            {
                string[] Hr = new string[24];
                for (int i = 0; i < 24; i++)
                {
                    Hr[i] = string.Format("{0:00}", i);
                }
                st_hr.DataSource = Hr;
                en_hr.DataSource = Hr;
                st_hr.DataBind();
                en_hr.DataBind();
                st_hr.SelectedIndex = 9;
                en_hr.SelectedIndex = st_hr.SelectedIndex + 3;
            }
            catch
            { 
            
            }
        }

        protected void st_hr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                en_hr.SelectedIndex = st_hr.SelectedIndex + 3;
            }
            catch
            { 
            }
        }

        protected void st_min_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                en_min.SelectedIndex = st_min.SelectedIndex;
            }
            catch
            { 
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                ExamRoomBiz biz = new ExamRoomBiz();
                DTO.ResponseService<string> CountSearch = biz.GetCountSearch(st_hr.SelectedValue.ToString(), st_min.SelectedValue.ToString(),
                                                en_hr.SelectedValue.ToString(), en_min.SelectedValue.ToString());
                string CCount = CountSearch.DataResponse.ToString();

                if (CCount == "0") //ยังไม่มีช่วงเวลาที่เริ่มต้น และ สิ้นสุดตรงกับที่ค้นหา
                {
                    btn_Add.Visible = true;
                    btn_Search.Visible = false;
                    lbl_txt_show.Text = "";
                    
                }
                else
                {
                    btn_Add.Visible = false;
                    btn_Search.Visible = true;
                    lbl_txt_show.Text = "*มีช่วงเวลาดังกล่าวในระบบแล้ว";
                }
                st_hr.Enabled = !btn_Add.Visible;
                st_min.Enabled = !btn_Add.Visible;
                en_hr.Enabled = !btn_Add.Visible;
                en_min.Enabled = !btn_Add.Visible;

                
                txtNumberGvSearch.Text = "0";
                PanalPage.Visible = true;
                GetDataToGV(true);
            }
            catch  (Exception ex)
            {
                string abc = ex.Message;
            }
        }

        protected void GetDataToGV(Boolean Count)
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
            ExamRoomBiz biz = new ExamRoomBiz();

            if (Count)
            {
                #region Page
                var CountPage = biz.GetExamTime(st_hr.SelectedValue.ToString(), st_min.SelectedValue.ToString(),
                                               en_hr.SelectedValue.ToString(), en_min.SelectedValue.ToString(), resultPage, PageSize, true);

                if (CountPage.DataResponse != null)
                    if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                    {
                        Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                        VisibleGV(gvTime, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                    }
                    else
                    {
                        VisibleGV(gvTime, 0, Convert.ToInt32(rowPerpage.Text), true);
                        if (Rpage == 0)
                            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        txtTotalPage.Text = "1";
                    }
                #endregion Page
            }
            var res = biz.GetExamTime(st_hr.SelectedValue.ToString(), st_min.SelectedValue.ToString(),
                                               en_hr.SelectedValue.ToString(), en_min.SelectedValue.ToString(), resultPage, PageSize, false);
            gvTime.DataSource = res.DataResponse;
            gvTime.DataBind();
            gvTime.Visible = true;
        }

        protected void btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(st_hr.SelectedValue) > Convert.ToInt32(en_hr.SelectedValue))
                {
                    UCModalError1.ShowMessageError = "เวลาเริ่มต้นมากกว่าเวลาสิ้นสุด";
                    UCModalError1.ShowModalError();
                    return;
                }
                else if (st_hr.SelectedValue.ToString() == en_hr.SelectedValue.ToString())
                {
                    UCModalError1.ShowMessageError = "เวลาเริ่มต้นเท่ากับเวลาสิ้นสุด";
                    UCModalError1.ShowModalError();
                    return;
                }
                ExamRoomBiz biz = new ExamRoomBiz();
                DTO.ResponseMessage<bool> AddTime = biz.Add_Time(st_hr.SelectedValue.ToString(), st_min.SelectedValue.ToString(),
                                                en_hr.SelectedValue.ToString(), en_min.SelectedValue.ToString(), base.UserId.ToString());

                Boolean add_time = AddTime.ResultMessage;
                if (add_time)
                {
                    UCModalSuccess1.ShowMessageSuccess = "บันทึกสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                    
                }
                else
                {
                    UCModalError1.ShowMessageError = "ไม่สามารถบันทึกได้";
                    UCModalError1.ShowModalError();
                }
                btn_Add.Visible = false;
                btn_Search.Visible = true;
                //st_hr.SelectedIndex = 0;
                //st_min.SelectedIndex = 0;
                //en_hr.SelectedIndex = 0;
                //en_min.SelectedIndex = 0;
                st_hr.Enabled = true;
                st_min.Enabled = true;
                en_min.Enabled = true;
                en_hr.Enabled = true;
                btn_Search_Click(sender, e);
                UpdatePanelGridviewExamTime.Update();
            }
            catch
            { 
            }
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_txt_show.Text = "";
                btn_Search.Text = "ค้นหา";
                btn_Search.Visible=true;
                btn_Add.Visible = false;
                st_hr.Enabled = !btn_Add.Visible;
                st_min.Enabled = !btn_Add.Visible;
                en_hr.Enabled = !btn_Add.Visible;
                en_min.Enabled = !btn_Add.Visible;
                gvTime.Visible = false;
                gvTime.DataSource = null;
                gvTime.DataBind();
                st_hr.SelectedIndex = 9;
                st_min.SelectedIndex = 0;
                en_hr.SelectedIndex = 12;
                en_min.SelectedIndex = 0;
                PanalPage.Visible = false;
                rowPerpage.Text = "20";
            }
            catch
            { 
            }
        }

        protected void hplCancel_Click(object sender, EventArgs e)
        {
            try
            {


                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                var lblTimeCode = (Label)gr.FindControl("lblTimeCode");
                string Key = lblTimeCode.Text;
                ExamRoomBiz biz = new ExamRoomBiz();
                DTO.ResponseMessage<bool> DelTime = biz.Del_Time(Key, base.UserId.ToString());

                Boolean Del_time = DelTime.ResultMessage;
                if (Del_time)
                {
                    UCModalSuccess1.ShowMessageSuccess = "ดำเนินการสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                }
                else
                {
                    UCModalError1.ShowMessageError = "ดำเนินการไม่สำเร็จ";
                    UCModalError1.ShowModalError();
                }
                btn_Search_Click(sender, e);
            }
            catch
            { 
            }
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            GetDataToGV(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelGridviewExamTime.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            GetDataToGV(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelGridviewExamTime.Update();
        }

        protected void btnSearchGo_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = "0";
            GetDataToGV(true);
        }
        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvTime":
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
            UpdatePanelGridviewExamTime.Update();
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
       
    }
}