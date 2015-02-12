using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAS.Utils;
using IAS.DTO;
using AjaxControlToolkit;
using IAS.BLL;
using System.Text.RegularExpressions;
using IAS.Properties;

namespace IAS.Setting
{
    public partial class SetExamPlaceGroup : basepage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initGridAssociation();
            }
        }
        int PageSize = 20;
        private void initGridAssociation()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            //DataSet ds = biz.GetExamPlaceGroupR("").DataResponse;
            //gvAssociation.DataSource = ds.Tables[0];
            //gvAssociation.DataBind();
            txtNumberGvSearch.Text = "0";
            BindData(true);
        }

        protected void btnPopUp_Click(object sender, EventArgs e)
        {
            txtAssociationCode.Enabled = true;
            txtAssociationCode.Text = "";
            txtAssociationName.Text = "";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            mpeAssociation.Show();
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
            txtAssociationCode.Text = ((Label)gr.FindControl("lblAssociationCode")).Text;
            txtAssociationName.Text = ((Label)gr.FindControl("lblAssociationName")).Text;

           
            txtAssociationCode.Enabled = false;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            ////SetCheckBoxLicense(txtAssociationCode.Text);
            mpeAssociation.Show();
        }
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.ExamScheduleBiz biz = new BLL.ExamScheduleBiz();
                GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
               string ID = ((Label)gr.FindControl("lblAssociationCode")).Text;

              
                if (!biz.CheckUsedPlaceGroup(ID).ResultMessage)//false = มีใช้งานไปแล้ว
                {
                    mpeAssociation.Show();
                    
                }
                else
                {
                    functionDelete(ID);
                }
               // functionDelete(ID);
                
            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
            }
        }
        protected void functionDelete(string ID)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            DTO.ConfigAssociation ent = new DTO.ConfigAssociation();
            List<DTO.AssociationLicense> al = new List<DTO.AssociationLicense>();

            ent.ASSOCIATION_CODE = ID;
            ent.ACTIVE = "N";
            var res = biz.InsertExamPlaceGroupR(ent, UserProfile, al);

            //var res = biz.DeleteAsscoiation(ID);
            if (!res.IsError)
            {
                initGridAssociation();
                UCSuccess.ShowMessageSuccess = SysMessage.DeleteSuccess;
                UCSuccess.ShowModalSuccess();
            }
            else
            {
                UCError.ShowMessageError = res.ErrorMsg;
                UCError.ShowModalError();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAssociationCode.Text) && !string.IsNullOrEmpty(txtAssociationName.Text))
                {
                    if (txtAssociationCode.Text.Length == 3)
                    {
                        BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                        var checkid = biz.GetExamPlaceGroupRByCheckID(txtAssociationCode.Text, txtAssociationName.Text);

                        List<DTO.AssociationLicense> al = new List<DTO.AssociationLicense>();
                        DTO.ConfigAssociation ent = new DTO.ConfigAssociation();
                        ent.ASSOCIATION_CODE = txtAssociationCode.Text;
                        ent.ASSOCIATION_NAME = txtAssociationName.Text;
                        ent.ACTIVE = "Y";

                        var res = biz.InsertExamPlaceGroupR(ent, UserProfile, al);
                        if (!res.IsError)
                        {
                            initGridAssociation();
                            UCSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                            UCSuccess.ShowModalSuccess();
                        }
                        else
                        {
                            UCError.ShowMessageError = res.ErrorMsg;
                            UCError.ShowModalError();
                        }
                    }
                    else
                    {
                        UCError.ShowMessageError = "รหัสหน่วยงานจัดสอบต้องมีจำนวน 3 ตัวอักษรเท่านั้น";
                        UCError.ShowModalError();
                    }
                }
                else 
                {
                    UCError.ShowMessageError = "กรุณากรอกข้อมูลสมาคมให้ครบถ้วน";
                    UCError.ShowModalError();
                }

            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
            }
            txtAssociationCode.Text = "";
            txtAssociationName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                DTO.ConfigAssociation ent = new DTO.ConfigAssociation();
                ent.ASSOCIATION_CODE = txtAssociationCode.Text;
                ent.ASSOCIATION_NAME = txtAssociationName.Text;
                ent.UPDATED_BY = base.UserId.ToString();
                ent.UPDATED_DATE = DateTime.Now;
                ent.ACTIVE = "Y";

                List<DTO.AssociationLicense> al = new List<DTO.AssociationLicense>();
                var res = biz.InsertExamPlaceGroupR(ent, UserProfile, al);
                if (!res.IsError)
                {
                    initGridAssociation();
                    UCSuccess.ShowMessageSuccess = SysMessage.SaveSucess;
                    UCSuccess.ShowModalSuccess();
                }
                else
                {
                    UCError.ShowMessageError = res.ErrorMsg;
                    UCError.ShowModalError();
                }
            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
            }
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

       

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           txtNumberGvSearch.Text = "0"; //milk
           BindData(true);
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
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                if (Count)
                {
                    #region Page
                    var CountPage = biz.GetExamPlaceGroupRByIDName(txtIdAss.Text.Trim(), txtNameAss.Text.Trim(), resultPage, PageSize, true);

                    if (CountPage.DataResponse != null)
                        if (CountPage.DataResponse.Tables[0].Rows.Count > 0)
                        {
                            Int64 totalROWs = Convert.ToInt32(CountPage.DataResponse.Tables[0].Rows[0]["CCount"].ToString());

                            VisibleGV(gvAssociation, totalROWs, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                        }
                        else
                        {
                            VisibleGV(gvAssociation, 0, Convert.ToInt32(rowPerpage.Text), true);
                            if (Rpage == 0)
                                NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage);
                            txtTotalPage.Text = "1";
                        }
                    #endregion Page
                }

                var res = biz.GetExamPlaceGroupRByIDName(txtIdAss.Text.Trim(), txtNameAss.Text.Trim(), resultPage, PageSize, false);
                gvAssociation.DataSource = res.DataResponse;
                gvAssociation.DataBind();
            }
            catch
            {
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
            string ID = ((Label)gr.FindControl("lblAssociationCode")).Text;
            functionDelete(ID);
        }

        protected void btnClear_Click1(object sender, EventArgs e)
        {
            txtIdAss.Text = "";
            txtNameAss.Text = "";
            rowPerpage.Text = "20";
            initGridAssociation();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnNextGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "N", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            uplAssociation.Update();
           
        }

        protected void btnPreviousGvSearch_Click(object sender, EventArgs e)
        {
            NPbutton(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "P", txtTotalPage);
            BindData(false);
            Hide_show(btnPreviousGvSearch, txtNumberGvSearch, btnNextGvSearch, "", txtTotalPage.Text.ToInt());
            uplAssociation.Update();
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
                case "gvAssociation":
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
            uplAssociation.Update();
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