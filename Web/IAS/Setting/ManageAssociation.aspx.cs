using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IAS.Utils;

namespace IAS.Setting
{
    public partial class ManageAssociation : basepage
    {
        private int RowPerPage { get{return PAGE_SIZE_Key;}}
        private int NumberGvSearch = 1;

        private double TotalRows
        {
            get { return (Session["_TotalRows"] == null) ? double.Parse("0") : double.Parse(Session["_TotalRows"].ToString()); }
            set { Session["_TotalRows"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initGridAssociation();
            }
        }

        private void initGridAssociation()
        {
            BindGrid();
            ClearControlSearch();
        }

        protected void btnPopUp_Click(object sender, EventArgs e)
        {
            InitCheckBoxLicense();
            txtAssociationCode.Enabled = true;
            txtAssociationCode.Text = "";
            txtAssociationName.Text = "";
            ddlAgentType.SelectedValue = "03";
            ddlCompType.SelectedValue = "03";
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            mpeAssociation.Show();
            UplPopUp.Update();
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
            txtAssociationCode.Text = ((Label)gr.FindControl("lblAssociationCode")).Text;
            txtAssociationName.Text = ((Label)gr.FindControl("lblAssociationName")).Text;

            string _compType = ((Label)gr.FindControl("lblCompType")).Text;
            if (_compType != "")
                ddlCompType.SelectedValue = _compType;
            else ddlCompType.SelectedValue = "03";

            string _agentType = ((Label)gr.FindControl("lblAgentType")).Text;
            if (_agentType != "")
                ddlAgentType.SelectedValue = _agentType;
            else ddlAgentType.SelectedValue = "03";
            txtAssociationCode.Enabled = false;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            SetCheckBoxLicense(txtAssociationCode.Text);
            mpeAssociation.Show();
            UplPopUp.Update();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                GridViewRow gr = (GridViewRow)((LinkButton)sender).Parent.Parent;
                string ID = ((Label)gr.FindControl("lblAssociationCode")).Text;

                var res = biz.DeleteAsscoiation(ID);
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
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAssociationCode.Text.Length == 3)
                {
                    BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                    DTO.ConfigAssociation ent = new DTO.ConfigAssociation();
                    ent.ASSOCIATION_CODE = txtAssociationCode.Text.Trim();
                    ent.ASSOCIATION_NAME = txtAssociationName.Text.Trim();
                    if (ddlAgentType.SelectedValue != "")
                        ent.AGENT_TYPE = ddlAgentType.SelectedValue;
                    if (ddlCompType.SelectedValue != "")
                        ent.COMP_TYPE = ddlCompType.SelectedValue;
                    ent.ACTIVE = "Y";

                    List<DTO.AssociationLicense> al = new List<DTO.AssociationLicense>();
                    foreach (ListItem i in chkLicense.Items)
                    {
                        al.Add(new DTO.AssociationLicense
                        {
                            LICENSE_TYPE_CODE = i.Value,
                            ACTIVE = i.Selected ? "Y" : "N"
                        });
                    }
                    var res = biz.InsertAssociation(ent, UserProfile, al);
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
                        mpeAssociation.Show();
                        UplPopUp.Update();
                    }
                }
                else
                {
                    UCError.ShowMessageError = "รหัสสมาคมต้องมีจำนวน 3 หลักเท่านั้น";
                    UCError.ShowModalError();
                }

            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
                mpeAssociation.Show();
                UplPopUp.Update();
            }
            //txtAssociationCode.Text = "";
            //txtAssociationName.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                DTO.ConfigAssociation ent = new DTO.ConfigAssociation();
                ent.ASSOCIATION_CODE = txtAssociationCode.Text.Trim();
                ent.ASSOCIATION_NAME = txtAssociationName.Text.Trim();
                ent.AGENT_TYPE = ddlAgentType.SelectedValue;
                ent.COMP_TYPE = ddlCompType.SelectedValue;

                List<DTO.AssociationLicense> al = new List<DTO.AssociationLicense>();
                foreach (ListItem i in chkLicense.Items)
                {
                    al.Add(new DTO.AssociationLicense
                    {
                        ASSOCIATION_CODE = txtAssociationCode.Text,
                        LICENSE_TYPE_CODE = i.Value,
                        ACTIVE = i.Selected ? "Y" : "N"
                    });
                }

                var res = biz.UpdateAsscoiation(ent, UserProfile, al);
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
                    mpeAssociation.Show();
                    UplPopUp.Update();
                }
            }
            catch (Exception ex)
            {
                UCError.ShowMessageError = ex.Message;
                UCError.ShowModalError();
                mpeAssociation.Show();
                UplPopUp.Update();
            }
        }

        private Action<DropDownList, DTO.DataItem[]> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        protected void gvAssociation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label CompType = e.Row.FindControl("lblCompType") as Label;
                Label lblCompType = e.Row.FindControl("lblCompTypeName") as Label;
                Label AgentType = e.Row.FindControl("lblAgentType") as Label;
                Label lblAgentType = e.Row.FindControl("lblAgentTypeName") as Label;
                Label Active = e.Row.FindControl("lblAssocActive") as Label;
                Label lblActive = e.Row.FindControl("lblAssocActiveName") as Label;
                LinkButton Edit = e.Row.FindControl("lbtnEdit") as LinkButton;
                LinkButton Delete = e.Row.FindControl("lbtnDelete") as LinkButton;

                switch (CompType.Text)
                {
                    case "01": lblCompType.Text = "ประกันชีวิต"; break;
                    case "02": lblCompType.Text = "ประกันวินาศภัย"; break;
                    case "03": lblCompType.Text = "ประกันชีวิตและวินาศภัย"; break;
                }

                switch (AgentType.Text)
                {
                    case "01": lblAgentType.Text = "ตัวแทน"; break;
                    case "02": lblAgentType.Text = "นายหน้า"; break;
                    case "03": lblAgentType.Text = "ตัวแทนและนายหน้า"; break;
                }

                switch (Active.Text)
                {
                    case "Y": 
                        lblActive.Text = "ใช้งาน"; 
                        Edit.Visible = true;
                        Delete.Visible = true;
                        break;
                    case "N": 
                        lblActive.Text = "ไม่ใช้งาน"; 
                        Edit.Visible = false;
                        Delete.Visible = false;
                        break;
                }


            }
        }

        protected void InitCheckBoxLicense()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetLicenseType("");

            chkLicense.DataSource = res.DataResponse;
            chkLicense.DataTextField = "Name";
            chkLicense.DataValueField = "Id";
            chkLicense.DataBind();
            ddlCompType.SelectedValue = "03";
            ddlAgentType.SelectedValue = "03";
            //ValidTypeCheckBox();
        }

        protected void SetCheckBoxLicense(string Assoc_code)
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetLicenseType("");

            chkLicense.DataSource = res.DataResponse;
            chkLicense.DataTextField = "Name";
            chkLicense.DataValueField = "Id";
            chkLicense.DataBind();

            var sel = biz.GetAssociationLicense(Assoc_code);
            if (sel.DataResponse != null)
            {
                var data = sel.DataResponse;
                foreach (ListItem ck in chkLicense.Items)
                {
                    var qry = data.FirstOrDefault(s => s.LICENSE_TYPE_CODE == ck.Value && s.ACTIVE == "Y");
                    if (qry != null)
                        ck.Selected = true;
                }
            }

            //ValidTypeCheckBox();
        }

        protected void ValidTypeCheckBox()
        {
            string CompType = ddlCompType.SelectedValue;
            string AgentType = ddlAgentType.SelectedValue;
            
            foreach (ListItem ck in chkLicense.Items)
            {
                ck.Enabled = true;
                if (CompType == "01" && AgentType == "01")
                {
                    if (ck.Value != "01" && ck.Value != "07")
                    {
                        ck.Enabled = false;
                        ck.Selected = false;
                        continue;
                    }
                }
                else if (CompType == "01" && AgentType == "02")
                {
                    if (ck.Value != "03")
                    {
                        ck.Enabled = false;
                        ck.Selected = false;
                        continue;
                    }
                }
                else if (CompType == "01" && AgentType == "03")
                {
                    if (ck.Value != "01" && ck.Value != "03" && ck.Value != "07")
                    {
                        ck.Enabled = false;
                        ck.Selected = false;
                        continue;
                    }
                }
                else if (CompType == "02" && AgentType == "01")
                {
                    if (ck.Value != "02" && ck.Value != "05" && ck.Value != "06" && ck.Value != "08")
                    {
                        ck.Enabled = false;
                        ck.Selected = false;
                        continue;
                    }
                }
                else if (CompType == "02" && AgentType == "02")
                {
                    if (ck.Value != "04")
                    {
                        ck.Enabled = false;
                        ck.Selected = false;
                        continue;
                    }

                }
                else if (CompType == "02" && AgentType == "03")
                {
                    if (ck.Value != "02" && ck.Value != "04" && ck.Value != "05" && ck.Value != "06" && ck.Value != "08")
                    {
                        ck.Enabled = false;
                        ck.Selected = false;
                        continue;
                    }
                }
                //else if (CompType == "3" && AgentType == "Z")
                //{
                //    ck.Enabled = true;
                //    continue;
                //}
            }
        }

        protected void ddlCompType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidTypeCheckBox();
            mpeAssociation.Show();
            UplPopUp.Update();
        }

        protected void ddlAgentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidTypeCheckBox();
            mpeAssociation.Show();
            UplPopUp.Update();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            txtRowsPerpage.Text = RowPerPage.ToString();
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            BindGrid();
            SetControlPage();
        }

        protected void BindGrid()
        {
            TotalRows = TotalRow();
            int page = int.Parse(txtNumberGvSearch.Text);
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetAssociationByByCriteria(txtIdAss.Text.Trim().ReplaceQuote(), txtNameAss.Text.Trim().ReplaceQuote(), ddlCType.SelectedValue.ToString(),
                                                    ddlAtype.SelectedValue.ToString(), page, rowsPerPage(), false, getStatus());
            gvAssociation.DataSource = res.DataResponse;
            gvAssociation.DataBind();

            SetTextTotlaRows();
        }

        protected void ClearControlSearch()
        {
            txtIdAss.Text = "";
            txtNameAss.Text = "";
            ddlAtype.SelectedValue = "";
            ddlCType.SelectedValue = "";
            txtNumberGvSearch.Text = NumberGvSearch.ToString();
            txtRowsPerpage.Text = RowPerPage.ToString();
            SetControlPage();
            ddlStatus.SelectedValue = "";

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControlSearch();
            BindGrid();
            SetControlPage();
        }

        protected int rowsPerPage()
        {
            int _rpp = RowPerPage;
            if(txtRowsPerpage.Text != "")
                _rpp =  int.Parse(txtRowsPerpage.Text);
            return _rpp;
        }

        private double LastPage()
        {
            double mod = TotalRows / rowsPerPage();
            double lp = Math.Ceiling(mod);
            return lp == 0 ? 1 : lp;
        }

        private double TotalRow()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var res = biz.GetAssociationByByCriteria(txtIdAss.Text.Trim().ReplaceQuote(), txtNameAss.Text.Trim().ReplaceQuote(), ddlCType.SelectedValue.ToString(),
                                                    ddlAtype.SelectedValue.ToString(), 0, 0, true, getStatus());
            return double.Parse(res.DataResponse.Tables[0].Rows[0][0].ToString());
        }

        private void SetControlPage()
        {
            //if (TotalRows < 1 || rowsPerPage() == 0)
            //{
            //    ctrPage.Visible = false;
            //}
            //else
            //{
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
            //}
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

        protected void SetTextTotlaRows()
        {
            lblTotalRows.Text = String.Format("จำนวน {0} รายการ", TotalRows);
        }

        protected bool? getStatus()
        {
            bool? status = null;
            if (ddlStatus.SelectedValue == "Y")
            {
                status = true;
            }
            else if (ddlStatus.SelectedValue == "N")
            {
                status = false;
            }
            return status;
        }
    }
}