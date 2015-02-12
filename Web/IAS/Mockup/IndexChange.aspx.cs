using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using System.Data;

namespace IAS.Mockup
{
    public partial class IndexChange : System.Web.UI.Page
    {
        protected string groupReqNo = "999999561000000359";
        //protected string groupReqNo = "999999561000000341";
        public List<DTO.SubPaymentHead> CurrentSessionListHT
        {
            get
            {
                return (List<DTO.SubPaymentHead>)Session["CurrentSessionListHT"] == null ? null : (List<DTO.SubPaymentHead>)Session["CurrentSessionListHT"];
            }
            set
            {
                Session["CurrentSessionListHT"] = value;
            }
        }

        public List<DTO.SubPaymentDetail> CurrentSessionListDT
        {
            get
            {
                return (List<DTO.SubPaymentDetail>)Session["CurrentSessionListDT"] == null ? null : (List<DTO.SubPaymentDetail>)Session["CurrentSessionListDT"];
            }
            set
            {
                Session["CurrentSessionListDT"] = value;
            }
        }

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.GetIndex();

            }
        }
        #endregion

        #region Public & Private Fucntion
        public void GetIndex()
        {
            IndexChangeBiz biz = new IndexChangeBiz();

            if (txtGroupReqNo.Text == "")
            {
                this.ucError.ShowMessageError = "Please input 'GROUP_REQUEST_NO' before search";
                this.ucError.ShowModalError();
                return;
            }

            DTO.ResponseService<DTO.SubPaymentHead[]> res = biz.GetIndexSubPaymentH(this.txtGroupReqNo.Text.Trim());
            if (res.DataResponse != null)
            {
                this.CurrentSessionListHT = res.DataResponse.ToList();
                gvHT.DataSource = this.CurrentSessionListHT;
                gvHT.DataBind();
                btnShow.Visible = true;
            }
            else
            {
                btnShow.Visible = false;
            }
        }

        public List<DTO.SubPaymentHead> SortDescriptionsHT(List<DTO.SubPaymentHead> ls, int CurrentIndex, String Mode)
        {
            if(Mode.Equals("Up"))
            {
                //Get Current & Previous Data
                DTO.SubPaymentHead cur = ls[CurrentIndex];
                DTO.SubPaymentHead pre = ls[CurrentIndex - 1];

                int order = Convert.ToInt32(cur.SEQ_OF_GROUP);
                cur.SEQ_OF_GROUP = pre.SEQ_OF_GROUP;
                pre.SEQ_OF_GROUP = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = pre;
                ls[CurrentIndex - 1] = cur;
            }
            else if (Mode.Equals("Down"))
            {
                //Get Current & Next Data
                DTO.SubPaymentHead cur = ls[CurrentIndex];
                DTO.SubPaymentHead next = ls[CurrentIndex + 1];

                int order = Convert.ToInt32(cur.SEQ_OF_GROUP);
                cur.SEQ_OF_GROUP = next.SEQ_OF_GROUP;
                next.SEQ_OF_GROUP = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = next;
                ls[CurrentIndex + 1] = cur;
            }

            return ls;

        }


        public List<DTO.SubPaymentDetail> SortDescriptionsDT(List<DTO.SubPaymentDetail> ls, int CurrentIndex, String Mode)
        {
            if (Mode.Equals("Up"))
            {
                //Get Current & Previous Data
                DTO.SubPaymentDetail cur = ls[CurrentIndex];
                DTO.SubPaymentDetail pre = ls[CurrentIndex - 1];

                int order = Convert.ToInt32(cur.SEQ_OF_SUBGROUP);
                cur.SEQ_OF_SUBGROUP = pre.SEQ_OF_SUBGROUP;
                pre.SEQ_OF_SUBGROUP = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = pre;
                ls[CurrentIndex - 1] = cur;
            }
            else if (Mode.Equals("Down"))
            {
                //Get Current & Next Data
                DTO.SubPaymentDetail cur = ls[CurrentIndex];
                DTO.SubPaymentDetail next = ls[CurrentIndex + 1];

                int order = Convert.ToInt32(cur.SEQ_OF_SUBGROUP);
                cur.SEQ_OF_SUBGROUP = next.SEQ_OF_SUBGROUP;
                next.SEQ_OF_SUBGROUP = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = next;
                ls[CurrentIndex + 1] = cur;
            }

            return ls;

        }

        #endregion

        #region UI Function
        protected void btnShow_Click(object sender, EventArgs e)
        {
            this.gvHTpop.DataSource = this.CurrentSessionListHT;
            this.gvHTpop.DataBind();

            modalMain.Show();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.GetIndex();
        }

        protected void lbtMoveUp_Click(object sender, EventArgs e)
        {
            int index = 0;
            GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            var lblORDERS = (Label)gr.FindControl("lblORDERS");
            int selectedIndex = gr.RowIndex;

            //var grid = (GridView)((GridView)sender).NamingContainer;
            //int rowcount = (grid.DataSource as DataTable).Rows.Count;
            //for (int i = 0; i < rowcount; i++)
            //{
            //    index++;
            //}

            //Get Row Index
            int RowIndex = selectedIndex;
            if (RowIndex < 1)
            {
                return;
            }


        }

        protected void lbtMoveDown_Click(object sender, EventArgs e)
        {


        }

        protected void gvHTpop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            if (e.CommandName.Equals("View"))
            {
                IndexChangeBiz biz = new IndexChangeBiz();
                //GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                var lblHEAD_REQUEST_NO = (Label)gvr.FindControl("lblHEAD_REQUEST_NO");
                DTO.ResponseService<DTO.SubPaymentDetail[]> ls = biz.GetIndexSubPaymentD(lblHEAD_REQUEST_NO.Text.Trim());

                //Test HEAD_REQUEST_NO='131029133903341';
                //DTO.ResponseService<DTO.SubPaymentDetail[]> ls = biz.GetIndexSubPaymentD("131029133903341");
                if (ls.DataResponse != null)
                {
                    this.CurrentSessionListDT = ls.DataResponse.ToList();
                    gvDTpop.DataSource = ls.DataResponse;
                    gvDTpop.DataBind();
                    modalMain.Show();
                    modalMain2.Show();
                }
                
            }
            else
            {

                if (e.CommandName == "Up")
                {
                    int rowcount = this.gvHTpop.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        index++;
                    }

                    //Get Row Index
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    if (RowIndex < 1)
                    {
                        this.ucError.ShowMessageError = "Can't moveup";
                        this.ucError.ShowModalError();
                        this.modalMain.Show();
                        return;
                    }
                }
                else
                {
                    int rowcount = this.gvHTpop.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        index++;
                    }

                    //Get Row Index
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    RowIndex = RowIndex + 1;
                    if (RowIndex >= index)
                    {
                        this.ucError.ShowMessageError = "Can't movedown";
                        this.ucError.ShowModalError();
                        this.modalMain.Show();
                        return;
                    }
                }

                List<DTO.SubPaymentHead> newls = this.SortDescriptionsHT(this.CurrentSessionListHT, CurrentIndex, e.CommandName);

                //Rebind
                this.gvHTpop.DataSource = newls;
                this.gvHTpop.DataBind();

                this.gvHT.DataSource = newls;
                this.gvHT.DataBind();

                udpMain.Update();
                modalMain.Show();
            }

            //if (e.CommandName == "Up")
            //{
            //    int index = 0;
            //    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //    int CurrentIndex = gvr.RowIndex;

            //    int rowcount = this.gvHTpop.Rows.Count;
            //    for (int i = 0; i < rowcount; i++)
            //    {
            //        index++;
            //    }

            //    //Get Row Index
            //    int RowIndex = int.Parse(e.CommandArgument.ToString());
            //    if (RowIndex < 1)
            //    {
            //        this.ucError.ShowMessageError = "Can't moveup";
            //        this.ucError.ShowModalError();
            //        this.modalMain.Show();
            //        return;
            //    }

            //    this.SortDescriptions(this.CurrentSessionList, CurrentIndex, e.CommandName);

            //    //Get Current & Previous Data
            //    DTO.SubPaymentHead cur = this.CurrentSessionList[CurrentIndex];
            //    DTO.SubPaymentHead pre = this.CurrentSessionList[CurrentIndex - 1];

            //    int order = Convert.ToInt32(cur.SEQ_OF_GROUP);
            //    cur.SEQ_OF_GROUP = pre.SEQ_OF_GROUP;
            //    pre.SEQ_OF_GROUP = Convert.ToString(order);


            //    //Resort
            //    this.CurrentSessionList[CurrentIndex] = pre;
            //    this.CurrentSessionList[CurrentIndex - 1] = cur;

            //    //Rebind
            //    this.gvHTpop.DataSource = this.CurrentSessionList.OrderBy(idx => idx.SEQ_OF_GROUP).ToList();
            //    this.gvHTpop.DataBind();

            //    this.gvHT.DataSource = this.CurrentSessionList.OrderBy(idx => idx.SEQ_OF_GROUP).ToList();
            //    this.gvHT.DataBind();

            //    udpMain.Update();
            //    modalMain.Show();
            //}
            //else if (e.CommandName == "Down")
            //{
            //    int index = 0;
            //    GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //    int CurrentIndex = gvr.RowIndex;

            //    int rowcount = this.gvHTpop.Rows.Count;
            //    for (int i = 0; i < rowcount; i++)
            //    {
            //        index++;
            //    }

            //    //Get Row Index
            //    int RowIndex = int.Parse(e.CommandArgument.ToString());
            //    RowIndex = RowIndex + 1;
            //    if (RowIndex >= index)
            //    {
            //        this.ucError.ShowMessageError = "Can't movedown";
            //        this.ucError.ShowModalError();
            //        this.modalMain.Show();
            //        return;
            //    }

            //    //Get Current & Next Data
            //    DTO.SubPaymentHead cur = this.CurrentSessionList[CurrentIndex];
            //    DTO.SubPaymentHead next = this.CurrentSessionList[CurrentIndex + 1];
            //    int order = Convert.ToInt32(cur.SEQ_OF_GROUP);
            //    cur.SEQ_OF_GROUP = next.SEQ_OF_GROUP;
            //    next.SEQ_OF_GROUP = Convert.ToString(order);

            //    //Resort
            //    this.CurrentSessionList[CurrentIndex] = next;
            //    this.CurrentSessionList[CurrentIndex + 1] = cur;

            //    //Rebind
            //    this.gvHTpop.DataSource = this.CurrentSessionList.OrderBy(idx => idx.SEQ_OF_GROUP).ToList();
            //    this.gvHTpop.DataBind();

            //    this.gvHT.DataSource = this.CurrentSessionList.OrderBy(idx => idx.SEQ_OF_GROUP).ToList();
            //    this.gvHT.DataBind();

            //    udpMain.Update();
            //    modalMain.Show();
            //}
            //else if (e.CommandName == "View")
            //{
            //    GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
            //    var lblHEAD_REQUEST_NO = (Label)gr.FindControl("lblHEAD_REQUEST_NO");

            //}


        }

        protected void gvDTpop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int CurrentIndex = gvr.RowIndex;

            if (e.CommandName.Equals("View"))
            {
                IndexChangeBiz biz = new IndexChangeBiz();
                //GridViewRow gr = (GridViewRow)((LinkButton)sender).NamingContainer;
                var lblUPLOAD_GROUP_NO = (Label)gvr.FindControl("lblUPLOAD_GROUP_NO");
                DTO.ResponseService<DTO.PersonLicenseDetail[]> ls = biz.GetIndexLicenseD(lblUPLOAD_GROUP_NO.Text.Trim());

                //For Test 131029132814315
                //DTO.ResponseService<DTO.PersonLicenseDetail[]> ls = biz.GetIndexLicenseD("131029132814315");
                if (ls.DataResponse != null)
                {
                    gvLicenseD.DataSource = ls.DataResponse;
                    gvLicenseD.DataBind();
                    modalMain.Show();
                    modalMain2.Show();
                    modalMain3.Show();
                }

            }
            else
            {

                if (e.CommandName == "Up")
                {
                    int rowcount = this.gvDTpop.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        index++;
                    }

                    //Get Row Index
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    if (RowIndex < 1)
                    {
                        this.ucError.ShowMessageError = "Can't moveup";
                        this.ucError.ShowModalError();
                        this.modalMain.Show();
                        this.modalMain2.Show();
                        return;
                    }
                }
                else
                {
                    int rowcount = this.gvDTpop.Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        index++;
                    }

                    //Get Row Index
                    int RowIndex = int.Parse(e.CommandArgument.ToString());
                    RowIndex = RowIndex + 1;
                    if (RowIndex >= index)
                    {
                        this.ucError.ShowMessageError = "Can't movedown";
                        this.ucError.ShowModalError();
                        this.modalMain.Show();
                        this.modalMain2.Show();
                        return;
                    }
                }

                List<DTO.SubPaymentDetail> newls = this.SortDescriptionsDT(this.CurrentSessionListDT, CurrentIndex, e.CommandName);

                //Rebind
                this.gvDTpop.DataSource = newls;
                this.gvDTpop.DataBind();

                udpMain.Update();
                modalMain.Show();
                modalMain2.Show();
            }

        }

        protected void gvLicenseD_RowCommand(object sender, GridViewCommandEventArgs e)
        {



        }
        #endregion



    }
}