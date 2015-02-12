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
    public partial class PayExamFee3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txReceiptNumber.Attributes.Add("onblur", "WaterMark(this, event);");
            txReceiptNumber.Attributes.Add("onfocus", "WaterMark(this, event);");
            txtPeople.Attributes.Add("onblur", "WaterMark(this, event);");
            txtPeople.Attributes.Add("onfocus", "WaterMark(this, event);"); 


            if (!Page.IsPostBack)
            {
                txtPayDate.Text = DateUtil.dd_MM_yyyy_Now;
                txtReceiptDate.Text = DateUtil.dd_MM_yyyy_Now;
            }
        }

        private void ClearControl()
        {
            HiddenField_ID.Value = string.Empty;
            txReceiptNumber.Text = string.Empty;
            txtPeople.Text = string.Empty;
            txtPayDate.Text = string.Empty;
            txtReceiptDate.Text = string.Empty;
        }

        private bool ValidDateInput()
        {
            StringBuilder message = new StringBuilder();
            StringBuilder messageOther = new StringBuilder();
            bool isFocus = false;

            if (txReceiptNumber.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblReceiptNumber.Text);
                if (!isFocus)
                {
                    txReceiptNumber.Focus();
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

            if (txtPayDate.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblPayDate.Text);
                if (!isFocus)
                {
                    txtPayDate.Focus();
                    isFocus = true;
                }
            }

            if (txtReceiptDate.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblReceiptDate.Text);
                if (!isFocus)
                {
                    txtReceiptDate.Focus();
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
                txReceiptNumber.Focus();
                return false;
            }
            return true;

        }
    }
}