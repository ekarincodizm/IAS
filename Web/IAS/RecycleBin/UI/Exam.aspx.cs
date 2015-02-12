using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using IAS.Properties;

namespace IAS.UI
{
    public partial class Exam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtIdNumber.Attributes.Add("onkeypress", "javascript:return checkKeyNumberKeyPressFixPoint(event, this, 13);");

            if (!Page.IsPostBack)
            {

            }
        }

        private void ClearControl()
        {
            HiddenField_ID.Value = string.Empty;
            txtIdNumber.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSurName.Text = string.Empty;
            txtCertifyNumber.Text = string.Empty;
            //txtEndDate.Text = DateUtil.dd_MM_yyyy_Now;
            //ddlFrom.SelectedIndex = 0;
            //ddlTo.SelectedIndex = 0;
            //txtReason.Text = string.Empty;

            //btnAdd.Enabled = true;
            //btnUpdate.Enabled = false;
            //btnDelete.Enabled = false;
            //pnlInfo.DefaultButton = btnAdd.ID.ToString();
        }
        

        private bool ValidDateInput()
        {
            StringBuilder message = new StringBuilder();
            StringBuilder messageOther = new StringBuilder();
            bool isFocus = false;

            if (txtIdNumber.Text.Length < 1)
            {
                message.Append(lblIdNumber.Text);
                txtIdNumber.Focus();
                isFocus = true;
            }

            if (txtName.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblName.Text);
                if (!isFocus)
                {
                    txtName.Focus();
                    isFocus = true;
                }
            }

            if (txtCertifyNumber.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblCertifyNumber.Text);
                if (!isFocus)
                {
                    txtCertifyNumber.Focus();
                    isFocus = true;
                }
            }

            if (txtSurName.Text.Length < 1)
            {
                if (message.Length > 0)
                {
                    message.Append(", ");
                }
                message.Append(lblSurName.Text);
                if (!isFocus)
                {
                    txtSurName.Focus();
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
                txtIdNumber.Focus();
                return false;
            }
            return true;

        }

    }
}