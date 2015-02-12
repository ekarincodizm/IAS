using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class CreateMultiGridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Class.CustomConfigTable ConfigTable1 = new Class.CustomConfigTable("11");
                ConfigTable1.PetitionTypeCode = "11";
                ConfigTable1.ViewStateMode = System.Web.UI.ViewStateMode.Enabled;
                DTO.UserProfile user = new DTO.UserProfile();
                user.Name = "pichit";
                Session[PageList.UserProfile] = user;
                Session["ConfigTable1"] = ConfigTable1; 
                pnlMain.Controls.Add(ConfigTable1);
            }
            else {
                pnlMain.Controls.Add((Class.CustomConfigTable)Session["ConfigTable1"]);
            }

        }

        protected void chk_Test_CheckedChanged(object sender, EventArgs e)
        {

        }


        protected void btn_Click(object sender, EventArgs e)
        {
            foreach (Class.CustomConfigTable item in pnlMain.Controls.OfType<Class.CustomConfigTable>())
            {
                //item.SaveChange();
                //item.Load("11");
            }
            
        }
    }
}