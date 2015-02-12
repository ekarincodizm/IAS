using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace IAS.UserControl
{
    public partial class ucAddress : System.Web.UI.UserControl
    {
        #region Public Param & Session
        const int LENGTH_TEXT = 100;
        public TextBox TextBoxAddress { get { return txtAddress; } set { txtAddress = value; } }
        public TextBox TextBoxPostCode { get { return txtPostcodeAddress; } set { txtPostcodeAddress = value; } }
        public DropDownList DropdownProvince { get { return ddlProvinceAddress; } set { ddlProvinceAddress = value; } }
        public DropDownList DropdownDistrict { get { return ddlDistrictAddress; } set { ddlDistrictAddress = value; } }
        public DropDownList DropdownParish { get { return ddlParishAddress; } set { ddlParishAddress = value; } }

        public String Address { get { return txtAddress.Text.Trim(); } set { txtAddress.Text = value; } }
        public String PostCode { get { return txtPostcodeAddress.Text; } set { txtPostcodeAddress.Text = value; } }
        public String ProvinceValue { get { return ddlProvinceAddress.SelectedValue.ToString(); } }
        public String ProvinceName { get { return ddlProvinceAddress.SelectedItem.Text; } }
        public String DistrictValue { get { return ddlDistrictAddress.SelectedValue.ToString(); } }
        public String DistrictName { get { return ddlDistrictAddress.SelectedItem.Text; } }
        public String ParishValue { get { return ddlParishAddress.SelectedValue.ToString(); } }
        public String ParishName { get { return ddlParishAddress.SelectedItem.Text; } }

        public String LabalAddress { get { return lblAddress.Text; } set { lblAddress.Text = value; } }
        public String LabalProvince { get { return lblProvinceAddress.Text; } set { lblProvinceAddress.Text = value; } }
        public String LabelDistrict { get { return lblDistrictAddress.Text; } set { lblDistrictAddress.Text = value; } }
        public String LabalParis { get { return lblParishAddress.Text; } set { lblParishAddress.Text = value; } }
        public String LabalPostCode { get { return lblPostcodeAddress.Text; } set { lblPostcodeAddress.Text = value; } }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Init();
                //MaxLength(this.txtAddress);
            }
        }
        #endregion

        #region UI Function
        protected void ddlProvinceAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDistrict();
        }

        protected void ddlDistrictAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetParish();
        }

        #endregion


        public string errorAddressRequired { set { AddressRequired.ErrorMessage = value; } }
        public string errorProvinceAddressRequired { set { ProvinceAddressRequired.ErrorMessage = value; } }
        public string errorDistrictAddressRequired { set { DistrictAddressRequired.ErrorMessage = value; } }
        public string errorParishAddressRequired { set { ParishAddressRequired.ErrorMessage= value;} }
        public string errorPostcodeAddressRequired { set { PostcodeAddressRequired.ErrorMessage = value; } }

        #region Main Public && Private Function
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.Items.Clear();
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void MaxLength(TextBox txtBox)
        {
            string
                lengthFunction = "function isMaxLength(txtBox) {";
            lengthFunction += " if(txtBox) { ";
            lengthFunction += "     return ( txtBox.value.length <=" + LENGTH_TEXT + ");";
            lengthFunction += " }";
            lengthFunction += "}";

            this.txtAddress.Attributes.Add("onkeypress", "return isMaxLength(this);");
            AjaxControlToolkit.ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "txtLength", lengthFunction, true);
        }

        private void Init()
        {
            GetProvince();
        }

        private void GetProvince()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvince(message);
            BindToDDL(ddlProvinceAddress, ls);
        }

        private void GetDistrict()
        {
            var message = SysMessage.DefaultSelecting;

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpur(message, ddlProvinceAddress.SelectedValue);
            BindToDDL(ddlDistrictAddress, ls);
            ddlParishAddress.Items.Clear();
        }
        private void GetParish()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbon(message, ddlProvinceAddress.SelectedValue, ddlDistrictAddress.SelectedValue);

            BindToDDL(ddlParishAddress, ls);
        }

        public void SelectDropDownStep(String privinceCode, String districtCode, String parishCode) {
            GetProvince();
            ddlProvinceAddress.SelectedValue = privinceCode;
            GetDistrict();
            ddlDistrictAddress.SelectedValue = districtCode;
            GetParish();
            ddlParishAddress.SelectedValue = parishCode;
        }

        public void Enabled(Boolean isEnable)
        {
            pnlAddress.Enabled = isEnable;
        }

        public void Visable(Boolean isVisable)
        {
            pnlAddress.Visible = isVisable;
        }

        public void Clear()
        {
            Address = "";
            PostCode = "";
            DropdownProvince.SelectedIndex = 0;
            DropdownParish.Items.Clear();
            DropdownParish.Items.Clear();
        }

        public String ThrowIsInValidMessage()
        {
            StringBuilder bussinessRules = new StringBuilder();
            bool isFocus = false;
            if (string.IsNullOrEmpty(this.Address) && this.Address.Length < 1)
            {

                bussinessRules.AppendLine(this.LabalAddress);
                if (!isFocus)
                {
                    this.TextBoxAddress.Focus();
                    isFocus = true;
                }
            }

            if (this.DropdownProvince.SelectedValue.Length < 1 && this.DropdownProvince.SelectedIndex == 0)
            {

                bussinessRules.AppendLine(this.LabalProvince);
                if (!isFocus)
                {
                    this.DropdownProvince.Focus();
                    isFocus = true;
                }
            }

            if (this.DropdownDistrict.SelectedValue.Length < 1 && this.DropdownDistrict.SelectedIndex == 0)
            {

                bussinessRules.AppendLine(this.LabelDistrict);
                if (!isFocus)
                {
                    this.DropdownDistrict.Focus();
                    isFocus = true;
                }
            }

            if (this.DropdownParish.SelectedValue.Length < 1 && this.DropdownParish.SelectedIndex == 0)
            {

                bussinessRules.AppendLine(this.LabelDistrict);
                if (!isFocus)
                {
                    this.DropdownParish.Focus();
                    isFocus = true;
                }
            }

            if (string.IsNullOrEmpty(this.PostCode) && this.PostCode.Length < 1)
            {

                bussinessRules.AppendLine(this.LabalPostCode);
                if (!isFocus)
                {
                    this.TextBoxPostCode.Focus();
                    isFocus = true;
                }
            }

            return bussinessRules.ToString();
        }

        /// <summary>
        /// Group of Control
        /// 1.Person => regGuestValidationGroup
        /// 2.Company => CompanyValidationGroup
        /// 3.Association => AssociationValidationGroup
        /// </summary>
        /// <param name="reqFormat"></param>
        public void SetValidateGroup(string reqFormat)
        {
            foreach (Control c in this.Controls.OfType<UpdatePanel>())
            {
                if (c.Controls.Count > 0)
                {
                    for (int i = 0; i < c.Controls.Count; i++)
                    {
                        string[] ctrlName = { "AddressRequired", "ProvinceAddressRequired", "DistrictAddressRequired", 
                                                "ParishAddressRequired", "PostcodeAddressRequired","valInput", "RegularExpressionValidator2" };
                        for (int j = 0; j < ctrlName.Count(); j++)
                        {

                            var getCtrl = c.Controls[i].FindControl("" + ctrlName[j] + "");

                            if (getCtrl.GetType() == typeof(RequiredFieldValidator))
                            {
                                (getCtrl as RequiredFieldValidator).ValidationGroup = reqFormat;

                            }

                            if (getCtrl.GetType() == typeof(RegularExpressionValidator))
                            {
                                (getCtrl as RegularExpressionValidator).ValidationGroup = reqFormat;

                            }

                        }
                    }
                }


            }

        }
        #endregion

        //protected void txtPostcodeAddress_TextChanged(object sender, EventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch("[^0-9]{5}", txtPostcodeAddress.Text))
        //    {
        //        txtPostcodeAddress.Text.Remove(txtPostcodeAddress.Text.Length - 1);
        //    }
        //}

    }
}
