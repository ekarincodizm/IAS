using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAS.Utils;
using IAS.BLL;

namespace IAS.Setting
{
    public partial class ManageExamRoom : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                defaultData(sender,e);
            }
        }
        int PageSize = 20;
        private void defaultData(object sender, EventArgs e)
        {
            if (base.UserProfile.MemberType == DTO.RegistrationType.Association.GetEnumValue())
            {
                ddlGroupType.SelectedValue = "3";
                ddlGroupType.Enabled = false;
                GetAssociation();
                gvExamRoom.Visible = false;
                ddlExamPlaceGroup_SelectedIndexChanged(sender, e);
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
            {
                ddlGroupType.SelectedValue = "7";
                ddlGroupType.Enabled = false;
                GetExamGRoup();
                gvExamRoom.Visible = false;
                ddlExamPlaceGroup_SelectedIndexChanged(sender, e);
            }
            else if (base.UserProfile.MemberType == DTO.RegistrationType.OIC.GetEnumValue())
            {
                ddlGroupType.SelectedValue = "";
                ddlGroupType.Enabled = true;
                gvExamRoom.Visible = false;
            }
        }

        private void initDDLExamPlace(string placeGroup)
        {
            BLL.DataCenterBiz dbiz = new BLL.DataCenterBiz();
            if (ddlGroupType.SelectedValue == "3")
            {
                 
                ddlExamPlace.DataTextField = "Name";
                ddlExamPlace.DataValueField = "Id";
                ddlExamPlace.DataSource = dbiz.GetExamPlace_UnderAssocicate(SysMessage.DefaultSelecting, ddlExamPlaceGroup.SelectedValue.ToString()).DataResponse;
                ddlExamPlace.DataBind();
                if (ddlExamPlace.Items.Count != 0)
                    ddlExamPlace.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, ""));
            }
            else if(ddlGroupType.SelectedValue == "7")
            {

                ddlExamPlace.DataSource = dbiz.GetExamPlace_AndProvince(ddlExamPlaceGroup.SelectedValue.ToString()).DataResponse;
                    ddlExamPlace.DataTextField = "Name";
                    ddlExamPlace.DataValueField = "Id";
                    ddlExamPlace.DataBind();

                    if(ddlExamPlace.Items.Count !=0)
                        ddlExamPlace.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, ""));
            }
          //  ddlExamPlace.Items.Insert(0, new ListItem(SysMessage.DefaultSelecting, ""));
        }

        private void GetExamGRoup()
        {
            try
            {
                string ComP = "";
                DataCenterBiz biz = new DataCenterBiz();
                if (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue())
                    ComP = base.UserProfile.CompCode;

                var ls = biz.GetExamPlaceGroupByCompCode(SysMessage.DefaultSelecting, ComP);
                ddlExamPlaceGroup.DataValueField = "Id";
                ddlExamPlaceGroup.DataTextField = "Name";

                ddlExamPlaceGroup.DataSource = ls.DataResponse;
                ddlExamPlaceGroup.DataBind();
                ddlExamPlaceGroup.Items.Insert(0, SysMessage.DefaultSelecting);


                if (ComP != "")
                {
                    ddlExamPlaceGroup.SelectedValue = ComP;
                    ddlExamPlaceGroup.Enabled = false;
                }
                else
                {
                    ddlExamPlaceGroup.SelectedIndex = 0;
                    ddlExamPlaceGroup.Enabled = true;
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
                if (base.UserProfile.MemberType != DTO.RegistrationType.OIC.GetEnumValue())
                    AssC = base.UserProfile.CompCode;



                var ls = biz.GetAssociation(AssC);
                ddlExamPlaceGroup.DataValueField = "ASSOCIATION_CODE";
                ddlExamPlaceGroup.DataTextField = "ASSOCIATION_NAME";

                ddlExamPlaceGroup.DataSource = ls.DataResponse;
                ddlExamPlaceGroup.DataBind();
                ddlExamPlaceGroup.Items.Insert(0, SysMessage.DefaultSelecting);
                if (AssC != "")
                {
                    ddlExamPlaceGroup.SelectedValue = AssC;
                    ddlExamPlaceGroup.Enabled = false;
                }
                else
                {
                    ddlExamPlaceGroup.SelectedIndex = 0;
                    ddlExamPlaceGroup.Enabled = true;
                }
            }
            catch
            {
            }
        }
       
      
        private Action<DropDownList, DataSet> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
            if(ddl.DataMember.Count()!=0)
            ddl.Items.Insert(0, SysMessage.DefaultSelecting);
        };

        private void ClearControl()
        {
            txtCodeRoom.Text = "";
            txtNameRoom.Text = "";
            txtSeatAmount.Text = "";
        }

        protected void btnPopUp_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.ExamScheduleBiz biz = new ExamScheduleBiz();
                lblSeatAmountPlace.Text = biz.SumSeatFromPlace(ddlExamPlace.SelectedValue.ToString(),"").DataResponse.ToString();
                // ClearControl();
                if (ddlExamPlaceGroup.SelectedIndex > 0 && ddlExamPlace.SelectedIndex > 0)
                {
                   // ddlExamPlace.Enabled = true;
                   // ddlExamPlaceGroup.Enabled = true;
                    txtCodeRoom.Enabled = true;
                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
                    ClearControl();
                    mpeExamRoom.Show();
                    uplPopUp.Update();
                }
                else
                {
                    UCError.ShowMessageError = SysMessage.ChooseData;
                    UCError.ShowModalError();
                }
            }
            catch
            { }

        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.ExamScheduleBiz biz = new ExamScheduleBiz();
                GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
                txtCodeRoom.Text = ((Label)gr.FindControl("lblCodeRoom")).Text;
                txtNameRoom.Text = ((Label)gr.FindControl("lblNameRoom")).Text;
                txtSeatAmount.Text = ((Label)gr.FindControl("lblSeatAmount")).Text;
                lblOldSeat.Text = txtSeatAmount.Text;

                lblSeatAmountPlace.Text = biz.SumSeatFromPlace(ddlExamPlace.SelectedValue.ToString(),txtCodeRoom.Text).DataResponse.ToString();
                


                txtCodeRoom.Enabled = false;
                btnUpdate.Visible = true;
                btnSave.Visible = false;
                uplPopUp.Update();
                mpeExamRoom.Show();
                
            }
            catch
            { 
            }
        }


        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
            //ddlExamPlaceGroup.SelectedValue = ((Label)gr.FindControl("lblPlaceGroupCode")).Text;
            DelThisRoom(((Label)gr.FindControl("lblCodeRoom")).Text);
            btnSearch_Click(sender, e);
            UpdatePanelGridviw.Update();
        }

        private void DelThisRoom(string Room)
        {
            try
            {
                BLL.ExamRoomBiz biz = new BLL.ExamRoomBiz();
              
                if (biz.DelExamRoom(Room).ResultMessage)
                {
                    UCSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    UCSuccess.ShowModalSuccess();
                }
                else
                {
                    UCError.ShowMessageError = SysMessage.ServerError;
                    UCError.ShowModalError();
                }
               
            }
            catch
            { 
            }
        }


         private Boolean CheckBeforeSave()
        { 
             Boolean BeforeSave = true;
            try
            {
               
                if (txtSeatAmount.Text.ToInt() == 0)
                {

                    UCError.ShowMessageError = "จำนวนที่นั่งต้องมีค่ามากกว่า 0";
                    UCError.ShowModalError();
                    BeforeSave = false;
                }
                else
                {

                    if (txtSeatAmount.Text.ToInt() > 30000)
                    {
                        UCError.ShowMessageError = "ห้องสอบแต่ละห้องสามารถรองรับผู้สมัครสอบได้สูงสุด 30,000 คน เท่านั้น";
                        UCError.ShowModalError();
                        BeforeSave = false;
                    }
                    else
                    {
                        BLL.ExamScheduleBiz biz = new ExamScheduleBiz();
                        string Room = "";
                        if (txtCodeRoom.Enabled == false)
                            Room = txtCodeRoom.Text;
                        lblSeatAmountPlace.Text = (lblSeatAmountPlace.Text == "") ? biz.SumSeatFromPlace(ddlExamPlace.SelectedValue.ToString(),Room).DataResponse.ToString() : lblSeatAmountPlace.Text;
                        lbltotalSeat.Text = biz.SumSeat(ddlExamPlace.SelectedValue.ToString()).DataResponse.ToString();
                        lbltotalSeat.Text = lbltotalSeat.Text == "" ? "0" : lbltotalSeat.Text;
                        BeforeSave = true;
                    }
                   
                }
               
            }
            catch
            {

            } 
             return BeforeSave;
        }
        string ExamRoomCode;
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckBeforeSave())
                {
                    if (lblSeatAmountPlace.Text.ToInt() < txtSeatAmount.Text.ToInt())
                    {
                        UCError.ShowMessageError = "จำนวนที่นั่งของทุกห้องมากเกินกว่า<br>จำนวนที่นั่งที่สนามสอบนี้สามารถรองรับได้";
                        UCError.ShowModalError();
                    }
                    else
                    {
                        if (txtCodeRoom.Text.Length == 6)
                        {
                            BLL.ExamRoomBiz biz = new BLL.ExamRoomBiz();
                            DTO.ConfigExamRoom ent = new DTO.ConfigExamRoom();
                            ent.EXAM_ROOM_CODE = txtCodeRoom.Text;
                            ent.EXAM_ROOM_NAME = txtNameRoom.Text;
                            ent.SEAT_AMOUNT = txtSeatAmount.Text.ToShort();
                            ent.EXAM_PLACE_CODE = ddlExamPlace.SelectedValue;

                            var res = biz.InsertExamRoom(ent, UserProfile);
                            if (!res.IsError)
                            {
                                //initGvExamRoom();
                                UCSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                                UCSuccess.ShowModalSuccess();
                                btnSearch_Click(sender, e);
                                UpdatePanelGridviw.Update();
                            }
                            else
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
                                ExamScheduleBiz bizGV = new ExamScheduleBiz();
                                var Place = bizGV.GetGVExamRoomByPlaceCode(ddlExamPlace.SelectedValue.ToString(), resultPage, PageSize, false);

                                for (int i = 0; i < Place.DataResponse.Tables[0].Rows.Count; i++)
                                {
                                    DataTable DT = Place.DataResponse.Tables[0];
                                    DataRow DR = DT.Rows[i];
                                    ExamRoomCode = DR["Exam_room_code"].ToString();
                                    if (ExamRoomCode == txtCodeRoom.Text)
                                    {
                                        break;
                                    }
                                }
                                if (ExamRoomCode == txtCodeRoom.Text)
                                {
                                    UCError.ShowMessageError = res.ErrorMsg;
                                    UCError.ShowModalError();
                                }
                                else
                                {
                                    UCError.ShowMessageError = "ไม่สามารถเพิ่มข้อมูลได้ เนื่องจากมีการยกเลิกการใช้งานรหัสห้องสอบ " + txtCodeRoom.Text + " แล้ว<br>กรุณาใช้รหัสห้องสอบอื่น";
                                    UCError.ShowModalError();
                                }
                            }
                        }
                        else
                        {
                            UCError.ShowMessageError = "รหัสห้องสอบต้องมีจำนวน 6 ตัวอักษรเท่านั้น";
                            UCError.ShowModalError();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
            }
           // ClearControl();
           
            lblSeatAmountPlace.Text = "";
            txtCodeRoom.Text = "";
            txtNameRoom.Text = "";
            txtSeatAmount.Text = "";
            uplPopUp.Update();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckBeforeSave())
                {
                    if (lblSeatAmountPlace.Text.ToInt() < txtSeatAmount.Text.ToInt())
                    {
                        UCError.ShowMessageError = "จำนวนที่นั่งของทุกห้องมากเกินกว่า<br>จำนวนที่นั่งที่สนามสอบนี้สามารถรองรับได้";
                        UCError.ShowModalError();
                    }
                    else
                    {
                        BLL.ExamRoomBiz biz = new BLL.ExamRoomBiz();
                        DTO.ConfigExamRoom ent = new DTO.ConfigExamRoom();
                        ent.EXAM_ROOM_CODE = txtCodeRoom.Text;
                        ent.EXAM_ROOM_NAME = txtNameRoom.Text;
                        ent.SEAT_AMOUNT = txtSeatAmount.Text.ToShort();
                        ent.EXAM_PLACE_CODE = ddlExamPlace.SelectedValue;

                        var res = biz.UpdateExamRoom(ent, UserProfile);
                        if (!res.IsError)
                        {
                            //initGvExamRoom();


                            UCSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                            UCSuccess.ShowModalSuccess();
                            btnSearch_Click(sender, e);
                            UpdatePanelGridviw.Update();

                        }
                        else
                        {
                            UCError.ShowMessageError = res.ErrorMsg;
                            UCError.ShowModalError();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
            }
            //ClearControl();
            lblSeatAmountPlace.Text = "";
            txtCodeRoom.Text = "";
            txtNameRoom.Text = "";
            txtSeatAmount.Text = "";
            uplPopUp.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtNumberGvSearch.Text = "0";
                PanalPage.Visible = true;
                BindData(true);
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
                ExamScheduleBiz biz = new ExamScheduleBiz();
                if (Count)
                {
                    #region Page
                    var CountPage = biz.GetGVExamRoomByPlaceCode(ddlExamPlace.SelectedValue.ToString(), resultPage, PageSize, true);

                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvExamRoom, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvExamRoom, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }
                if (ddlExamPlace.SelectedIndex > 0 && ddlExamPlaceGroup.SelectedIndex > 0)
                {
                    DTO.ResponseService<DataSet> Place = biz.GetGVExamRoomByPlaceCode(ddlExamPlace.SelectedValue.ToString(), resultPage, PageSize, false);
                    gvExamRoom.DataSource = Place.DataResponse;
                    gvExamRoom.DataBind();
                    gvExamRoom.Visible = true;
                    btnPopUp.Visible = true;
                    if (gvExamRoom.Visible == true)
                    {
                        PanalPage.Visible = true;
                    }
                    else
                    {
                        PanalPage.Visible = false;
                    }
                }
                else
                {
                    UCError.ShowMessageError = SysMessage.ChooseData;
                    UCError.ShowModalError();
                    btnPopUp.Visible = false;
                    if (gvExamRoom.Visible == true)
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
        protected void btn_Cancle_Click(object sender, EventArgs e)
        {
            try
            {

                btnPopUp.Visible = false;
                gvExamRoom.DataSource = null;
                gvExamRoom.DataBind();
                gvExamRoom.Visible = false;
                ClearControl();
                ddlExamPlace.Items.Clear();
                clearAll(true);
                defaultData(sender,e);
                PanalPage.Visible = false;
                rowPerpage.Text = "20";
            }
            catch
            { 
            }
        }

        protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                clearAll(false);
                btnPopUp.Visible = false;
                
                if (ddlGroupType.SelectedValue != "")
                {
                    switch (ddlGroupType.SelectedValue.ToString())
                    {
                        case "3":
                            GetAssociation();
                            break;
                        case "7":
                            GetExamGRoup();
                            break;
                        default:
                           
                            break;
                    }
                }
            }
            catch
            { 
            
            }
        }

        private void clearAll(Boolean all)
        {
            ddlExamPlaceGroup.Items.Clear();
            ddlExamPlace.Items.Clear();
            //gvExamRoom.DataSource = null;

            if (gvExamRoom.Visible==true )
            {
                PanalPage.Visible = true;
            }
            else
            {
                PanalPage.Visible = false;
            }
            if (all)
                ddlGroupType.SelectedIndex = 0;
                

             
        }

        protected void ddlExamPlaceGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblSeatAmountPlace.Text = "";
            initDDLExamPlace(ddlExamPlaceGroup.SelectedValue);
            uplPopUp.Update();
            btnPopUp.Visible = false;
            if (gvExamRoom.Visible == true)
            {
                PanalPage.Visible = true;
            }
            else
            {
                PanalPage.Visible = false;
            }
           
            //mpeExamRoom.Show();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mpeExamRoom.Hide();
            btnSearch_Click(sender,e);
        }

        protected void ddlExamPlace_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPopUp.Visible = false;
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelGridviw.Update();
        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            UpdatePanelGridviw.Update();
        }

        protected void btnSearchGo_Click(object sender, EventArgs e)
        {
            txtNumberGvSearch.Text = "0";
            BindData(true);
        }
        #region Pageing_milk
        protected void VisibleGV(GridView GVname, double total_row_count, double rows_per_page, Boolean visible_or_disvisible)
        {
            switch (GVname.ID.ToString())
            {
                case "gvExamRoom":
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
            UpdatePanelGridviw.Update();
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