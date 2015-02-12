using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Specialized;

namespace IAS.Mockup.FuncTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void BindGridView()
        {
            GetDataSource();
            gvCheckboxes.DataSource = GetDataSource();
            gvCheckboxes.DataBind();
        }

        protected DataTable GetDataSource()
        {
            DataTable dTable = new DataTable();
            DataRow dRow = null;
            Random rnd = new Random();
            dTable.Columns.Add("sno");

            for (int n = 0; n < 10; ++n)
            {
                dRow = dTable.NewRow();

                dRow["sno"] = n + ".";

                dTable.Rows.Add(dRow);
                dTable.AcceptChanges();
            }

            return dTable;
        }

        protected void gvCheckboxes_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
            {
                //CheckBox rb = (CheckBox)gvCheckboxes.Rows[e.Row].Cells[0].FindControl("chkBxSelect");
                CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkBxSelect");
                CheckBox chkBxHeader = (CheckBox)this.gvCheckboxes.HeaderRow.FindControl("chkBxHeader");

                chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);
            }
        }

        protected void gvCheckboxes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // If multiple buttons are used in a GridView control, use the
            // CommandName property to determine which button was clicked.
            if (e.CommandName == "Select")
            {
                // Convert the row index stored in the CommandArgument
                // property to an Integer.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button clicked 
                // by the user from the Rows collection.
                GridViewRow row = gvCheckboxes.Rows[index];

            }
        }

        protected void ok_Click(object sender, EventArgs e)
        {
            StringCollection idCollection = new StringCollection();
            string strID = string.Empty;

            for (int i = 0; i < gvCheckboxes.Rows.Count; i++)
            {
                CheckBox chkDelete = (CheckBox)gvCheckboxes.Rows[i].Cells[0].FindControl("chkBxSelect");
                if (chkDelete != null)
                {
                    if (chkDelete.Checked)
                    {
                        string strID2 = gvCheckboxes.Rows[i].Cells[0].Text;
                        strID = gvCheckboxes.Rows[i].Cells[1].Text;
                        idCollection.Add(strID);
                    }
                }
            }

        }
    }
}