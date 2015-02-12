using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Utils
{
    public class ControlHelper
    {
        public void ClearInput(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Controls.Count > 0)
                    ClearInput(ctrl);
                else
                {
                    //Set TextBox
                    if (ctrl is TextBox)
                        if (!(ctrl as TextBox).ID.Equals("txtMemberType"))
                        {
                            (ctrl as TextBox).Text = string.Empty;
                        }
                        

                    //Set Checkbox
                    if (ctrl is CheckBox)
                        (ctrl as CheckBox).Checked = false;

                    //Set DropDown
                    if (ctrl is DropDownList)
                        if ((ctrl as DropDownList).ID.Equals("ddlProvinceAddress") || (ctrl as DropDownList).ID.Equals("ddlDistrictAddress") || (ctrl as DropDownList).ID.Equals("ddlParishAddress")
                            || (ctrl as DropDownList).ID.Equals("DropdownProvince") || (ctrl as DropDownList).ID.Equals("DropdownDistrict") || (ctrl as DropDownList).ID.Equals("DropdownParish"))
                        {
                            //Only for UCAddress
                            (ctrl as DropDownList).ClearSelection();
                            (ctrl as DropDownList).SelectedValue = "";

                        }
                        else
                        {
                            (ctrl as DropDownList).SelectedIndex = 0;
                        }
                        
                    //Set Panel
                    //if (ctrl is Panel)
                    //    (ctrl as Panel).Visible = false;

                    //Set Gridview
                    if (ctrl is GridView)
                        (ctrl as GridView).Dispose();

                    if (ctrl is RadioButton)
                        (ctrl as RadioButton).Dispose();

                }
            }
        }
    }
}
