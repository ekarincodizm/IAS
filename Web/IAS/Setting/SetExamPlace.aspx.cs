using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Data;
using IAS.DTO;
using AjaxControlToolkit;
using IAS.BLL;
using System.Text.RegularExpressions;
using IAS.Properties;


namespace IAS.Setting
{
    public partial class SetExamPlace : basepage
    {
        LicenseTypeBiz biz = new LicenseTypeBiz();
        //private int PageSize { get { return PAGE_SIZE_Key; } }
        int PageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                defaultData();

            }
        }

        private void defaultData()
        {

            GetProvince();
            ddlPlaceGroup.Enabled = false;
            if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                ddlPlaceGroup.SelectedValue = "A";
                gvPlace.Visible = false;
                GetAssociation();
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
            {
                ddlPlaceGroup.SelectedValue = "G";
                gvPlace.Visible = false;
                GetExamGRoup();
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
            {
                ddlPlaceGroup.SelectedValue = "";
                ddlPlaceGroup.Enabled = true;
                gvPlace.Visible = false;
            }
        }

        private void GetProvince()
        {
            try
            {
                DataCenterBiz biz = new DataCenterBiz();
                var ls = biz.GetProvince(SysMessage.DefaultSelecting);
                ddlProvince.DataValueField ="Id";
                ddlProvince.DataTextField = "Name";
                ddlProvince.DataSource = ls;
                ddlProvince.DataBind();
            }
            catch
            { 
            }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                tblName.Visible = true;
                var gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                var PlaceCode = (Label)gr.FindControl("lblPlaceCode");
                var PlaceName = (Label)gr.FindControl("lblTxtTime");
                var PlaceSeat = (Label)gr.FindControl("lblEnd");
                    
                txtCode.Text = PlaceCode.Text;
                txtCode.Enabled = false;
                txtPlace.Text = PlaceName.Text;
                txtSeat.Text = PlaceSeat.Text;
                txtPlace.Focus();
                ddlPlaceGroup.Enabled = false;
                ddlPlaceG.Enabled = false;
                ddlProvince.Enabled = false;

                ModalExamSchedule.Show();

            }
            catch
            { 
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Add.Visible = true;
                //btnPreviousGvSearch.Visible = true;
                //txtNumberGvSearch.Visible = true;
                //btnNextGvSearch.Visible = true;
                //div_totalPage.Visible = true;
                txtNumberGvSearch.Text = "0";
                BindData(true);
                
                
            }
            catch  (Exception ex)
            {
                string abc = ex.Message;
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
                ExamScheduleBiz biz = new ExamScheduleBiz();

                if (Count)
                {
                    #region Page
                    var CountPage = biz.GetExamPlaceAndDetailFromProvinceAndGroupCode(ddlProvince.SelectedValue.ToString(), ddlPlaceG.SelectedValue.ToString(), ddlPlaceGroup.SelectedValue.ToString(), resultPage, PageSize, true);

                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvPlace, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvPlace, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }
                if (ddlPlaceG.SelectedIndex > 0 && ddlProvince.SelectedIndex > 0 && ddlPlaceGroup.SelectedValue != "")
                {
                    DTO.ResponseService<DataSet> Place = biz.GetExamPlaceAndDetailFromProvinceAndGroupCode(ddlProvince.SelectedValue.ToString(), ddlPlaceG.SelectedValue.ToString(), ddlPlaceGroup.SelectedValue.ToString(), resultPage, PageSize, false);


                    if (Place != null)
                    {

                        gvPlace.DataSource = Place.DataResponse;
                        gvPlace.DataBind();
                        gvPlace.Visible = true;
                        
                        //int rowcount = res.DataResponse.Tables[0].Rows.Count;
                        //lblTotal.Text = rowcount.ToString();
                        div_totalPage.Visible = true;
                        lblTotal.Visible = true;
                        btn_Add.Visible = true;
                        if (gvPlace.Visible == true)
                        {
                            PanalPage.Visible = true;
                        }
                        else
                        {
                            PanalPage.Visible = false;
                        }
                    }
                    //btnPreviousGvSearch.Visible = true;
                    //txtNumberGvSearch.Visible = true;
                    //btnNextGvSearch.Visible = true;
                    //div_totalPage.Visible = true;
                }
                else
                {
                    UCModalError1.ShowMessageError = SysMessage.ChooseData;
                    UCModalError1.ShowModalError();
                    btn_Add.Visible = false;
                    if (gvPlace.Visible == true)
                    {
                        PanalPage.Visible = true;
                    }
                    else
                    {
                        PanalPage.Visible = false;
                    }
                }
            }
            catch 
            { 
            
            }
        }


        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (ddlPlaceG.SelectedIndex > 0 && ddlProvince.SelectedIndex > 0)
            {
                ModalExamSchedule.Show();
                UplExamSchedule.Update();
                tblName.Visible = true;
                //ddlProvince.Enabled = false;
                //ddlPlaceG.Enabled = false;
                //ddlPlaceGroup.Enabled = false;

            }
            else
            {
                UCModalError1.ShowMessageError = SysMessage.ChooseData;
                UCModalError1.ShowModalError();
            }
           
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            try
            {
                    tblName.Visible = false;
                    ddlProvince.SelectedIndex = 0;
                    ddlPlaceG.Items.Clear();
                    txtPlace.Text = "";
                    txtSeat.Text = "";
                    txtCode.Text="";
                    chkFree.Checked = false;
                    gvPlace.DataSource = null;
                    gvPlace.DataBind();
                    gvPlace.Visible = false;
                    ddlPlaceGroup.Enabled = true;
                    ddlProvince.Enabled = true;
                    ddlPlaceG.Enabled = true;
                    ddlPlaceGroup.SelectedIndex = 0;
                    btn_Add.Visible = false;
                    rowPerpage.Text = "20";
                    PanalPage.Visible = false;
                    defaultData(); 
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
                var lblTimeCode = (Label)gr.FindControl("lblPlaceCode");
                string Key = lblTimeCode.Text;
                ExamScheduleBiz biz = new ExamScheduleBiz();
                DTO.ResponseMessage<bool> DelP= biz.DelPlace(Key, base.UserId.ToString());

                Boolean Del_time = DelP.ResultMessage;
                if (!Del_time)
                {
                    UCModalSuccess1.ShowMessageSuccess = "บันทึกสำเร็จ";
                    UCModalSuccess1.ShowModalSuccess();
                }
                else
                {
                    UCModalError1.ShowMessageError = "ไม่สามารถบันทึกได้";
                    UCModalError1.ShowModalError();
                }
                btn_Search_Click(sender, e);
            }
            catch
            { 
            }
        }

        protected void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPlaceGroup.SelectedValue != "")
                {
                    if (txtSeat.Text.Trim() == "" || txtPlace.Text.Trim() == "" || txtCode.Text.Trim() == "")
                    {
                        UCModalError1.ShowMessageError = "กรุณาระบุข้อมูลให้ครบ";
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        if (txtSeat.Text.ToInt() > 30000)
                        {
                            UCModalError1.ShowMessageError = "สนามสอบแต่ละแห่งสามารถรองรับผู้สมัครสอบได้สูงสุด 30,000 คน เท่านั้น";
                            UCModalError1.ShowModalError();
                        }
                        else
                        {
                            if (txtCode.Text.Length == 5)
                            {
                                Boolean addnew = true;
                                if (txtCode.Enabled == false)
                                    addnew = false;
                                ExamScheduleBiz biz = new ExamScheduleBiz();
                                DTO.ResponseMessage<bool> SaveOK = biz.SavePlace(ddlPlaceG.SelectedValue.ToString(), ddlProvince.SelectedValue.ToString(), txtCode.Text, txtPlace.Text, txtSeat.Text, chkFree.Checked, base.UserId.ToString(), addnew, ddlPlaceGroup.SelectedValue.ToString());
                                if (SaveOK.ResultMessage == true)
                                {
                                    UCModalSuccess1.ShowMessageSuccess = SysMessage.SaveSucess;
                                    UCModalSuccess1.ShowModalSuccess();
                                    btn_Search_Click(sender, e);
                                    btn_Cancle_Click(sender, e);
                                    UpdatePanelUpload.Update();
                                }
                                else
                                {
                                    UCModalError1.ShowMessageError = SaveOK.ErrorMsg;
                                    UCModalError1.ShowModalError();
                                }
                            }
                            else
                            {
                                UCModalError1.ShowMessageError = "รหัสสนามสอบต้องมีจำนวน 5 ตัวอักษรเท่านั้น";
                                UCModalError1.ShowModalError();
                            }
                        }
                    }
                }
            }
            catch
            { 
            }
        }

       
        protected void btn_Cancle_Click(object sender, EventArgs e)
        {
            tblName.Visible = false;
            txtCode.Text = "";
            txtPlace.Text = "";
            txtSeat.Text = "";
            txtCode.Enabled = true;
            chkFree.Checked = false;
            ddlPlaceGroup.Enabled = true;
            ddlPlaceG.Enabled = true;
            ddlProvince.Enabled = true;
        }

        protected void ddlPlaceG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_Add.Visible = false;
                //gvPlace.DataSource = null;
                //gvPlace.DataBind();
                if (gvPlace.Visible == true)
                {
                    PanalPage.Visible = true;
                }
                else
                {
                    PanalPage.Visible = false;
                }
            }
            catch
            { 
            
            }
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_Add.Visible = false;
                //gvPlace.DataSource = null;
                //gvPlace.DataBind();
                //gvPlace.Visible = false;
                if (gvPlace.Visible == true)
                {
                    PanalPage.Visible = true;
                }
                else
                {
                    PanalPage.Visible = false;
                }
            }
            catch
            {

            }
        }

        protected void ddlPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btn_Add.Visible = false;
                if (ddlPlaceGroup.SelectedIndex == 0)
                {
                    ddlPlaceG.Items.Clear();
                    ddlPlaceG.DataBind();
                }
                else
                {
                    if (gvPlace.Visible == true)
                    {
                        PanalPage.Visible = true;
                    }
                    else
                    {
                        PanalPage.Visible = false;
                    }

                    if (ddlPlaceGroup.SelectedValue != "")
                    {
                        switch (ddlPlaceGroup.SelectedValue.ToString())
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
                string ComP ="";
                DataCenterBiz biz = new DataCenterBiz();
                if (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue())
                    ComP = base.UserProfile.CompCode;

                var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, ComP);
                ddlPlaceG.DataValueField = "Id";
                ddlPlaceG.DataTextField = "Name";

                ddlPlaceG.DataSource = ls.DataResponse;
                ddlPlaceG.DataBind();

                ddlPlaceG.Items.Insert(0, SysMessage.DefaultSelecting);
                if (ComP != "")
                {
                    ddlPlaceG.SelectedValue = ComP;
                    ddlPlaceG.Enabled = false;
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
                string AssC ="";
                DataCenterBiz biz = new DataCenterBiz();
                  if (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue())
                    AssC = base.UserProfile.CompCode;
               
                var ls = biz.GetAssociation(AssC);
                ddlPlaceG.DataValueField = "ASSOCIATION_CODE";
                ddlPlaceG.DataTextField = "ASSOCIATION_NAME";

                ddlPlaceG.DataSource = ls.DataResponse;
                ddlPlaceG.DataBind();

                ddlPlaceG.Items.Insert(0, SysMessage.DefaultSelecting);
                if (AssC != "")
                {
                    ddlPlaceG.SelectedValue = AssC;
                    ddlPlaceG.Enabled = false;
                }
            }
            catch
            {
            }
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelUpload.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelUpload.Update();
        }

        protected void btnSearchGo_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = "0";
            //btnPreviousGvSearch.Visible = true;
            //txtNumberGvSearch.Visible = true;
            //btnNextGvSearch.Visible = true;
            //div_totalPage.Visible = true;
            BindData(true);
        }
        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvPlace":
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
            UpdatePanelUpload.Update();
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