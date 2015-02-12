using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using IAS.Utils;
using IAS.Properties;

namespace IAS.UI
{
    public partial class License3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtTestPlace.Attributes.Add("onblur", "WaterMark(this, event);");
            txtTestPlace.Attributes.Add("onfocus", "WaterMark(this, event);");
            txtPeople.Attributes.Add("onblur", "WaterMark(this, event);");
            txtPeople.Attributes.Add("onfocus", "WaterMark(this, event);"); 

            if (!Page.IsPostBack)
            {
                txtTestDate.Text = DateUtil.dd_MM_yyyy_Now;
            }
        }

        private void ClearControl()
        {
            HiddenField_ID.Value = string.Empty;
            txtTestPlace.Text = string.Empty;
            txtPeople.Text = string.Empty;
            txtTestDate.Text = string.Empty;
            txtTestTime.Text = string.Empty;
        }

        private bool ValidDateInput()
        {
            StringBuilder message = new StringBuilder();
            StringBuilder messageOther = new StringBuilder();
            bool isFocus = false;

            if (txtTestPlace.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblTestPlace.Text);
                if (!isFocus)
                {
                    txtTestPlace.Focus();
                    isFocus = true;
                }
            }

            if (txtPeople.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblPeople.Text);
                if (!isFocus)
                {
                    txtPeople.Focus();
                    isFocus = true;
                }
            }

            if (txtTestDate.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblTestDate.Text);
                if (!isFocus)
                {
                    txtTestDate.Focus();
                    isFocus = true;
                }
            }

            if (txtTestTime.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblTestTime.Text);
                if (!isFocus)
                {
                    txtTestTime.Focus();
                    isFocus = true;
                }
            }

            if (message.Length > 0)
            {
                MessageBox.Show(Resources.infoSysMessage_DataEmpty + message.ToString());
                //if (btnAdd.Enabled)
                //{
                //    pnlInfo.DefaultButton = btnAdd.ID.ToString();
                //}
                //else if (btnUpdate.Enabled)
                //{
                //    //pnlInfo.DefaultButton = btnUpdate.ID.ToString();
                //}
                return false;
            }

            if (messageOther.Length > 0)
            {
                MessageBox.Show(messageOther.ToString());
                //if (btnAdd.Enabled)
                //{
                //    pnlInfo.DefaultButton = btnAdd.ID.ToString();
                //}
                //else if (btnUpdate.Enabled)
                //{
                //    pnlInfo.DefaultButton = btnUpdate.ID.ToString();
                //}
                txtTestPlace.Focus();
                return false;
            }
            return true;

        }
    }
}