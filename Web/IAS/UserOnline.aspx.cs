using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;

namespace IAS
{
    public partial class UserOnline : System.Web.UI.Page
    {
        PersonBiz biz = new PersonBiz();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = biz.GetOnLineUser("").DataResponse.Tables[0];
                GridView1.DataBind();
                GetLanIPAddress();                
            }
        }
        public String GetLanIPAddress()
        {            
            String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip))
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_type = (Label)e.Row.FindControl("lbl_type");
                if (lbl_type.Text != "")
                {
                    if (lbl_type.Text == "1")
                    {
                        lbl_type.Text = "บุคคลทั่วไป";
                    }

                    else if (lbl_type.Text == "2")
                    {
                        lbl_type.Text = "บริษัท";
                    }

                    else if (lbl_type.Text == "3")
                    {
                        lbl_type.Text = "สมาคม";
                    }

                    else if (lbl_type.Text == "4")
                    {
                        lbl_type.Text = "คปภ";
                    }

                    else if (lbl_type.Text == "5")
                    {
                        lbl_type.Text = "คปภ.การเงิน";
                    }

                    else if (lbl_type.Text == "6")
                    {
                        lbl_type.Text = "คปภ.ตัวแทน";
                    }
                    else
                    {
                        lbl_type.Text = "เจ้าหน้าที่สนามสอบ";
                    }

                }
            }
        }
    }
}