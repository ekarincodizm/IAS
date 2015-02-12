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
    public partial class PayExamFee2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtReceiptNumber.Attributes.Add("onblur", "WaterMark(this, event);");
            txtReceiptNumber.Attributes.Add("onfocus", "WaterMark(this, event);"); 

            if (!Page.IsPostBack)
            {
                txtStartDate.Text = DateUtil.dd_MM_yyyy_Now;
                txtEndDate.Text = DateUtil.dd_MM_yyyy_Now;
            }
        }

        private void ClearControl()
        {
            HiddenField_ID.Value = string.Empty;
            txtStartDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
        }

        private bool ValidDateInput()
        {
            StringBuilder message = new StringBuilder();
            StringBuilder messageOther = new StringBuilder();
            bool isFocus = false;

            if (txtReceiptNumber.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblReceiptNumber.Text);
                if (!isFocus)
                {
                    txtReceiptNumber.Focus();
                    isFocus = true;
                }
            }

            if (txtStartDate.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblStartDate.Text);
                if (!isFocus)
                {
                    txtStartDate.Focus();
                    isFocus = true;
                }
            }

            if (txtEndDate.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblEndDate.Text);
                if (!isFocus)
                {
                    txtEndDate.Focus();
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
                txtStartDate.Focus();
                return false;
            }
            return true;

        }
    }
}