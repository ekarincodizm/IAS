using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.Properties;

namespace IAS.Register
{
    public partial class regExplain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetMemberType();
            }
        }

        private enum RegisterType
        {
            Guest = 1,
            Company = 2,
            Association = 3
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;            
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--เลือก--", ""));
        };

        /// <summary>
        /// <LASTUPDATE>30/04/2557</LASTUPDATE>
        /// <AUTHOR>Natta</AUTHOR>
        /// </summary>
        private void GetMemberType()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberTypeRegister("").DataResponse.ToList();
            //Assign new Name for MemberType = 1
            //var chkls = ls.FirstOrDefault(m => m.Id.Equals("1"));
            //if (chkls != null)
            //{
            //    chkls.Name = "บุคคลทั่วไป/ตัวแทน/นายหน้า";
            //}

            //ls.RemoveAt(4);
            BindToDDL(ddlMemberType, ls);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlMemberType.SelectedIndex > 0)
            {
                if (!isComfirm.Checked)
                {
                    AlertMessage.ShowAlertMessage(SysMessage.DialogLoginTitle, Resources.inforegExplain_001);
                }
                else if (ddlMemberType.SelectedIndex == Convert.ToInt32(RegisterType.Guest))
                {
                    Response.Redirect("~/Register/RegisGeneral.aspx");
                }
                else if (ddlMemberType.SelectedIndex == Convert.ToInt32(RegisterType.Company))
                {
                    Response.Redirect("~/Register/RegisCompany.aspx");
                }
                else if (ddlMemberType.SelectedIndex == Convert.ToInt32(RegisterType.Association))
                {
                    Response.Redirect("~/Register/RegisAssociate.aspx");
                }
            }
            else
            {
                AlertMessage.ShowAlertMessage(SysMessage.DialogLoginTitle, SysMessage.PleaseChooseMemberType);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/home.aspx");
        }
    }
}