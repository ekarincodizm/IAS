using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class TestUCAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void chkCopyAdd_CheckedChanged(object sender, EventArgs e)
        {
            //ucAddressCurrent.Clone(ucAddressRegister);
            //ucAddressRegister = ucAddressCurrent;
            if (((CheckBox)sender).Checked)
            {

                CloneAddress();
                ucAddressRegister.Enabled(false);
            }
            else
            {
                ucAddressRegister.Enabled(true);
                ucAddressRegister.DropdownProvince.SelectedIndex = 0;
                ucAddressRegister.DropdownDistrict.Items.Clear();
                ucAddressRegister.DropdownParish.Items.Clear();
                ucAddressRegister.Address = ucAddressCurrent.Address;
                ucAddressRegister.PostCode = ucAddressCurrent.PostCode;
                ucAddressRegister.Clear();
            }

            udpMain.Update();

        }

        private void CloneAddress()
        {
            ucAddressRegister.DropdownProvince.SelectedIndex = ucAddressCurrent.DropdownProvince.SelectedIndex;

            ucAddressRegister.DropdownDistrict.Items.Clear();
            ListItem districtItem = ucAddressCurrent.DropdownDistrict.SelectedItem;
            if (ucAddressCurrent.DropdownDistrict.SelectedValue != "")//--milk
                ucAddressRegister.DropdownDistrict.Items.Add(districtItem);

            ucAddressRegister.DropdownParish.Items.Clear();
            ListItem parish = ucAddressCurrent.DropdownParish.SelectedItem;
            if (ucAddressCurrent.DropdownParish.SelectedValue != "")//--milk
                ucAddressRegister.DropdownParish.Items.Add(parish);

            ucAddressRegister.Address = ucAddressCurrent.Address;
            ucAddressRegister.PostCode = ucAddressCurrent.PostCode;
        }

        protected void TabAddress_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabAddress.ActiveTabIndex == 1 && chkCopyAdd.Checked)
            {
                CloneAddress();
            }
        }
    }
}