using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using System.Threading;
using System.Globalization;
using IAS.DTO;
using AjaxControlToolkit;
using IAS.BLL.AttachFilesIAS;
using IAS.Properties;

namespace IAS.Person
{
    public partial class Edit_Reg_Person : basepage
    {
        public int EDIT_USER_TYPE; //milk
        public List<DTO.PersonAttatchFile> AttachFiles
        {
            get
            {
                if (Session["AttatchFiles"] == null)
                {
                    Session["AttatchFiles"] = new List<DTO.PersonAttatchFile>();
                }

                return (List<DTO.PersonAttatchFile>)Session["AttatchFiles"];
            }

            set
            {
                Session["AttatchFiles"] = value;
            }
        }

        public DTO.DataActionMode DataAction
        {
            get
            {
                return (DTO.DataActionMode)Session["DataAction"];
            }
        }

        public string PersonID
        {
            get
            {
                if (Session["PersonID"] == null)
                {
                    Session["PersonID"] = base.UserProfile.Id;
                }

                return Session["PersonID"].ToString();
            }
            set
            {
                Session["PersonID"] = value;
            }
        }

        public IAS.MasterPage.Site1 MasterPage
        {
            get { return (this.Page.Master as IAS.MasterPage.Site1); }
        }

        public string MemberTypeSession
        {
            get
            {
                return Session["MemberTypeSession"] == null ? string.Empty : Session["MemberTypeSession"].ToString();
            }
            set
            {
                Session["MemberTypeSession"] = value;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                base.HasPermit();

                //TODO: For Test
                Session["DataAction"] = DTO.DataActionMode.Edit;
                
                //Bind before
                //MasterPage.ddlAgentTypeInit(this.ddlAgentTypeBefore);
                //MasterPage.ddlAgentTypeInit(this.ddlAgentTypeAfter);

                GetStatus();
                GetBeforeReg();
                GetAfterReg();
                GetAttatchFiles();
                GetAttachFilesType();
                disableControl();
                this.DisableControlByMemberType(this.MemberTypeSession);

                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtApproveDate.Text = DateTime.Today.ToString("dd MMMM yyyy");
                txtApproveDate.Text = DateUtil.dd_MMMM_yyyy_Now_TH;

                txtApprover.Text = base.UserProfile.Name;

            }

        }

        private void disableControl()
        {

            //Before Reg
            txtTypeMemberBeforeReg.Enabled = false;
            txtCompanyBeforeReg.Enabled = false;
            txtTitleBeforeReg.Enabled = false;
            txtFirstNameBeforeReg.Enabled = false;
            txtLastNameBeforeReg.Enabled = false;
            rblSexBeforeReg.Enabled = false;
            txtIDNumberBeforeReg.Enabled = false;
            txtBirthDayBeforeReg.Enabled = false;
            txtEducationBeforeReg.Enabled = false;
            txtNationalityBeforeReg.Enabled = false;
            txtEmailBeforeReg.Enabled = false;
            txtTelBeforeReg.Enabled = false;
            txtMobilePhoneBeforeReg.Enabled = false;
            txtIDOicBeforeReg.Enabled = false;
            txtIDMemberNumberBeforeReg.Enabled = false;
            txtCurrentAddressBeforeReg.Enabled = false;
            txtProvinceCurrentAddressBeforeReg.Enabled = false;
            txtDistrictCurrentAddressBeforeReg.Enabled = false;
            txtParishCurrentAddressBeforeReg.Enabled = false;
            txtRegisterAddressBeforeReg.Enabled = false;
            txtProvinceRegisterAddressBeforeReg.Enabled = false;
            txtDistrictRegisterAddressBeforeReg.Enabled = false;
            txtParishRegisterAddressBeforeReg.Enabled = false;
            txtZipCodeCurrentAddressBeforeReg.Enabled = false;
            txtZipCodeRegisterAddressBeforeReg.Enabled = false;
            txtApprover.Enabled = false;
            txtApproveDate.Enabled = false;
            //ddlAgentTypeBefore.Enabled = false;

            //After Reg
            txtTypeMemberAfterReg.Enabled = false;
            txtCompanyAfterReg.Enabled = false;
            txtTitleAfterReg.Enabled = false;
            txtFirstNameAfterReg.Enabled = false;
            txtLastNameAfterReg.Enabled = false;
            rblSexAfterReg.Enabled = false;
            txtIDNumberAfterReg.Enabled = false;
            txtBirthDayAfterReg.Enabled = false;
            txtEducationAfterReg.Enabled = false;
            txtNationalityAfterReg.Enabled = false;
            txtEmailAfterReg.Enabled = false;
            txtTelAfterReg.Enabled = false;
            txtMobilePhoneAfterReg.Enabled = false;
            txtIDOicAfterReg.Enabled = false;
            txtIDMemberNumberAfterReg.Enabled = false;
            txtCurrentAddressAfterReg.Enabled = false;
            txtProvinceCurrentAddressAfterReg.Enabled = false;
            txtDistrictCurrentAddressAfterReg.Enabled = false;
            txtParishCurrentAddressAfterReg.Enabled = false;
            txtRegisterAddressAfterReg.Enabled = false;
            txtProvinceRegisterAddressAfterReg.Enabled = false;
            txtDistrictRegisterAddressAfterReg.Enabled = false;
            txtParishRegisterAddressAfterReg.Enabled = false;
            txtZipCodeCurrentAddressAfterReg.Enabled = false;
            txtZipCodeRegisterAddressAfterReg.Enabled = false;
            txtApprover.Enabled = false;
            txtApproveDate.Enabled = false;
            //ddlAgentTypeAfter.Enabled = false;


            ucAttachFileControl1.EnableGridView(false);
            ucAttachFileControl1.VisableUpload(false);
            ucAttachFileControl1.EnableUpload(false);
            ucAttachFileControl1.EnablehpCancle(false);
            //txtUsername.Enabled = false;
            //txtApproval.Enabled = false;
        }

        /// <summary>
        /// GetBeforeReg()
        /// </summary>
        /// <AUTHOR>Natta</AUTHOR>
        /// <LASTUPDATE>04/06/2557</LASTUPDATE>
        private void GetBeforeReg()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.PersonID;
            var res = biz.GetById(personID);

            if (res != null)
            {
                EDIT_USER_TYPE = res.DataResponse.MEMBER_TYPE.ToInt();
                string memberType = this.NullableString(res.DataResponse.MEMBER_TYPE);
                
                this.MemberTypeSession = memberType;
                GetMemberTypeBefore(memberType);

                if (res.DataResponse.APPROVE_RESULT != null)
                {
                    txtResultReg.Text = res.DataResponse.APPROVE_RESULT;
                }
                else
                {
                    txtResultReg.Text = "";
                }

                string compCode = res.DataResponse.COMP_CODE;
                GetCompanyByIdBefore(compCode);

                int title = Convert.ToInt32(res.DataResponse.PRE_NAME_CODE);
                GetTitleBefore(title);

                if (res.DataResponse.NAMES != null)
                {
                    txtFirstNameBeforeReg.Text = res.DataResponse.NAMES;
                }
                else
                {
                    txtFirstNameBeforeReg.Text = "-";
                }

                if (res.DataResponse.LASTNAME != null)
                {
                    txtLastNameBeforeReg.Text = res.DataResponse.LASTNAME;
                }
                else
                {
                    txtLastNameBeforeReg.Text = "-";
                }

                if (res.DataResponse.SEX != null)
                {
                    rblSexBeforeReg.SelectedValue = res.DataResponse.SEX;
                }
                else
                {
                    rblSexBeforeReg.SelectedIndex = 0;
                }

                if (res.DataResponse.ID_CARD_NO != null)
                {
                    txtIDNumberBeforeReg.Text = res.DataResponse.ID_CARD_NO;
                }
                else
                {
                    txtIDNumberBeforeReg.Text = "-";
                }
                //Remove CSS
                if (res.DataResponse.BIRTH_DATE != null)
                {
                    txtBirthDayBeforeReg.CssClass = "txt";
                    txtBirthDayBeforeReg.Text = res.DataResponse.BIRTH_DATE.Value.ToString("dd MMMM yyyy");
                }
                else
                {
                    txtBirthDayBeforeReg.CssClass = "txt";
                    txtBirthDayBeforeReg.Text = "-";
                }

                string education = res.DataResponse.EDUCATION_CODE;
                GetEducationBefore(education);

                string Nationality = res.DataResponse.NATIONALITY;
                GetNationalityBefore(Nationality);

                if (res.DataResponse.EMAIL != null)
                {
                    txtEmailBeforeReg.Text = res.DataResponse.EMAIL;
                }
                else
                {
                    txtEmailBeforeReg.Text = "-";
                }

                if (res.DataResponse.LOCAL_TELEPHONE != null)
                {
                    txtTelBeforeReg.Text = res.DataResponse.LOCAL_TELEPHONE;
                }
                else
                {
                    txtTelBeforeReg.Text = "-";
                }

                if (res.DataResponse.TELEPHONE != null)
                {
                    txtMobilePhoneBeforeReg.Text = res.DataResponse.TELEPHONE;
                }
                else
                {
                    txtMobilePhoneBeforeReg.Text = "-";
                }
                //txtIDOicBeforeReg.Text =
                //txtIDMemberNumberBeforeReg.Text = 
                if (res.DataResponse.ADDRESS_1 != null)
                {
                    txtCurrentAddressBeforeReg.Text = res.DataResponse.ADDRESS_1;
                }
                else
                {
                    txtCurrentAddressBeforeReg.Text = "-";
                }

                string province = res.DataResponse.PROVINCE_CODE;
                GetProvinceCurrentAddressBefore(province);

                string ampur = res.DataResponse.AREA_CODE;
                GetAmpurCurrentAddressBefore(province, ampur);

                string tumbon = res.DataResponse.TUMBON_CODE;
                GetTumbonCurrentAddressBefore(province, ampur, tumbon);

                if (res.DataResponse.LOCAL_ADDRESS1 != null)
                {
                    txtRegisterAddressBeforeReg.Text = res.DataResponse.LOCAL_ADDRESS1;
                }
                else
                {
                    txtRegisterAddressBeforeReg.Text = "-";
                }

                string localProvince = res.DataResponse.LOCAL_PROVINCE_CODE;
                GetProvinceRegisterAddressBefore(localProvince);

                string localampur = res.DataResponse.LOCAL_AREA_CODE;
                GetAmpurRegisterAddressBefore(localProvince, localampur);

                string localtumbon = res.DataResponse.LOCAL_TUMBON_CODE;
                GetTumbonRegisterAddressBefore(localProvince, localampur, localtumbon);

                if (res.DataResponse.ZIP_CODE != null)
                {
                    txtZipCodeCurrentAddressBeforeReg.Text = res.DataResponse.ZIP_CODE;
                }
                else //milk
                {
                    txtZipCodeCurrentAddressBeforeReg.Text = "-";
                }

                if (res.DataResponse.LOCAL_ZIPCODE != null)
                {
                    txtZipCodeRegisterAddressBeforeReg.Text = res.DataResponse.LOCAL_ZIPCODE;
                }
                else//milk
                {
                    txtZipCodeRegisterAddressBeforeReg.Text = "-";
                }

                //if ((res.DataResponse.AGENT_TYPE != "") && (res.DataResponse.AGENT_TYPE != null))
                //{
                //    ddlAgentTypeBefore.SelectedValue = res.DataResponse.AGENT_TYPE;
                //}
                //else if ((res.DataResponse.AGENT_TYPE == "") || (res.DataResponse.AGENT_TYPE == null))
                //{
                //    ListItem ddlList = new ListItem("", "", true);
                //    ddlList.Selected = true;
                //    ddlAgentTypeBefore.Items.Add(ddlList);
                //}

            }

            if (res.IsError)
            {
                //Response.Write(res.ErrorMsg);

                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
        }

        private void GetAfterReg()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.PersonID;
            var res = biz.GetPersonTemp(personID);

            if (res != null)
            {
                string memberType = res.DataResponse.MEMBER_TYPE;

                this.MemberTypeSession = memberType;
                GetMembeTypeAfter(memberType);


                string compCode = res.DataResponse.COMP_CODE;
                GetCompanyByIdAfter(compCode);

                int title = Convert.ToInt32(res.DataResponse.PRE_NAME_CODE);
                GetTitleAfter(title);

                if (res.DataResponse.NAMES != null)
                {
                    txtFirstNameAfterReg.Text = res.DataResponse.NAMES;
                }
                else
                {
                    txtFirstNameAfterReg.Text = "-";
                }

                if (res.DataResponse.LASTNAME != null)
                {
                    txtLastNameAfterReg.Text = res.DataResponse.LASTNAME;
                }
                else
                {
                    txtLastNameAfterReg.Text = "-";
                }

                if (res.DataResponse.SEX != null)
                {
                    rblSexAfterReg.SelectedValue = res.DataResponse.SEX;
                }
                else
                {
                    rblSexAfterReg.SelectedIndex = 0;
                }

                if (res.DataResponse.ID_CARD_NO != null)
                {
                    txtIDNumberAfterReg.Text = res.DataResponse.ID_CARD_NO;
                }
                else
                {
                    txtIDNumberAfterReg.Text = "-";
                }

                if (res.DataResponse.BIRTH_DATE != null)
                {
                    txtBirthDayAfterReg.CssClass = "txt";
                    txtBirthDayAfterReg.Text = res.DataResponse.BIRTH_DATE.Value.ToString("dd MMMM yyyy");
                }
                else
                {
                    txtBirthDayAfterReg.CssClass = "txt";
                    txtBirthDayAfterReg.Text = "-";
                }

                string education = res.DataResponse.EDUCATION_CODE;
                GetEducationAfter(education);

                string Nationality = res.DataResponse.NATIONALITY;
                GetNationalityAfter(Nationality);

                if (res.DataResponse.EMAIL != null)
                {
                    txtEmailAfterReg.Text = res.DataResponse.EMAIL;
                }
                else
                {
                    txtEmailAfterReg.Text = "-";
                }

                if (res.DataResponse.LOCAL_TELEPHONE != null)
                {
                    txtTelAfterReg.Text = res.DataResponse.LOCAL_TELEPHONE;
                }
                else
                {
                    txtTelAfterReg.Text = "-";
                }

                if (res.DataResponse.TELEPHONE != null)
                {
                    txtMobilePhoneAfterReg.Text = res.DataResponse.TELEPHONE;
                }
                else
                {
                    txtMobilePhoneAfterReg.Text = "-";
                }
                //txtIDOicBeforeReg.Text =
                //txtIDMemberNumberAfterReg.Text = 

                if (res.DataResponse.ADDRESS_1 != null)
                {
                    txtCurrentAddressAfterReg.Text = res.DataResponse.ADDRESS_1;
                }
                else
                {
                    txtCurrentAddressAfterReg.Text = "-";
                }

                string province = res.DataResponse.PROVINCE_CODE;
                GetProvinceCurrentAddressAfter(province);

                string ampur = res.DataResponse.AREA_CODE;
                GetAmpurCurrentAddressAfter(province, ampur);

                string tumbon = res.DataResponse.TUMBON_CODE;
                GetTumbonCurrentAddressAfter(province, ampur, tumbon);

                if (res.DataResponse.LOCAL_ADDRESS1 != null)
                {
                    txtRegisterAddressAfterReg.Text = res.DataResponse.LOCAL_ADDRESS1;
                }
                else
                {
                    txtRegisterAddressAfterReg.Text = "-";
                }

                string localProvince = res.DataResponse.LOCAL_PROVINCE_CODE;
                GetProvinceRegisterAddressAfter(localProvince);

                string localampur = res.DataResponse.LOCAL_AREA_CODE;
                GetAmpurRegisterAddressAfter(localProvince, localampur);

                string localtumbon = res.DataResponse.LOCAL_TUMBON_CODE;
                GetTumbonRegisterAddressAfter(localProvince, localampur, localtumbon);


                if (res.DataResponse.ZIP_CODE != null)
                {
                    txtZipCodeCurrentAddressAfterReg.Text = res.DataResponse.ZIP_CODE;
                }
                else //milk
                {
                    txtZipCodeCurrentAddressAfterReg.Text = "-";
                }
                if (res.DataResponse.LOCAL_ZIPCODE != null)
                {
                    txtZipCodeRegisterAddressAfterReg.Text = res.DataResponse.LOCAL_ZIPCODE;
                }
                else//milk
                {
                    txtZipCodeRegisterAddressAfterReg.Text = "-";
                }

                //if ((res.DataResponse.AGENT_TYPE != "") && (res.DataResponse.AGENT_TYPE != null))
                //{
                //    ddlAgentTypeAfter.SelectedValue = res.DataResponse.AGENT_TYPE;
                //}
                //else if ((res.DataResponse.AGENT_TYPE == "") || (res.DataResponse.AGENT_TYPE == null))
                //{
                //    ListItem ddlList = new ListItem("", "", true);
                //    ddlList.Selected = true;
                //    ddlAgentTypeAfter.Items.Add(ddlList);
                //}
            }
            else
            {
                UCModalError.ShowMessageError = res.ErrorMsg;
                UCModalError.ShowModalError();
            }
        }

        private void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.PersonID;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetAttatchFileEditByPersonId(personID);

            var list = res.DataResponse.ToList();

            this.AttachFiles = list;
            ucAttachFileControl1.AttachFiles = this.AttachFiles.OrderBy(a => a.ATTACH_FILE_TYPE).ToList().ConvertToAttachFilesView();
  
            UpdatePanelEdit.Update();

        }

        protected void hplView_Click(object sender, EventArgs e)
        {
            var gr = (GridViewRow)((LinkButton)sender).NamingContainer;

            var text = (Label)gr.FindControl("lblFileGv");

            Session["ViewFileName"] = text.Text;

            string showPic = IAS.Utils.CryptoBase64.Encryption(text.Text.ToString().Trim());

            string OpenWindow = "window.open('../Register/ImagePopup.aspx?targetImage=" + showPic + "','','')";
            ToolkitScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", OpenWindow, true);

        

        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        private void GetStatus()
        {
            var message = SysMessage.DefaultSelecting;
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetStatus(message);
            ls = ls.Where(w => w.Id == "" || w.Id == DTO.PersonDataStatus.Approve.GetEnumValue().ToString() ||
                    w.Id == DTO.PersonDataStatus.NotApprove.GetEnumValue().ToString()).ToList();

            //foreach (var item in ls)
            //{
            //    if (item.Id != PersonDataStatus.Approve.GetEnumValue().ToString() ||
            //        item.Id != PersonDataStatus.NotApprove.GetEnumValue().ToString())
            //    {
            //        ls.Remove(item);
            //    }

            //}

            BindToDDL(ddlStatus, ls);

        }

        private void GetCompanyByIdBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyCodeById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtCompanyBeforeReg.Text = ls.Name;
            }
            else
            {
                txtCompanyBeforeReg.Text = "-";
            }

        }

        private void GetCompanyByIdAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetCompanyCodeById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtCompanyAfterReg.Text = ls.Name;
            }
            else
            {
                txtCompanyAfterReg.Text = "-";
            }
        }

        private void GetMemberTypeBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberTypeById(id);

            if (ls != null)
            {
                if (ls.DataResponse.Id == DTO.MemberType.General.GetEnumValue().ToString())
                {
                    ls.DataResponse.Name = ls.DataResponse.Name + "/ตัวแทน/นายหน้า";
                    txtTypeMemberBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtTypeMemberBeforeReg.Text = ls.DataResponse.Name;
                }
            }
            else
            {
                txtTypeMemberBeforeReg.Text = "-";
            }

        }

        private void GetMembeTypeAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetMemberTypeById(id);

            if (ls != null)
            {
                if (ls.DataResponse.Id == DTO.MemberType.General.GetEnumValue().ToString())
                {
                    ls.DataResponse.Name = ls.DataResponse.Name + "/ตัวแทน/นายหน้า";
                    txtTypeMemberAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtTypeMemberAfterReg.Text = ls.DataResponse.Name;
                }
                
            }
            else
            {
                txtTypeMemberAfterReg.Text = "-";
            }
        }

        private void GetTitleBefore(int id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleNameById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtTitleBeforeReg.Text = listItem.ToString();
            }
            else
            {
                txtTitleBeforeReg.Text = "-";
            }
        }

        private void GetTitleAfter(int id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleNameById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtTitleAfterReg.Text = listItem.ToString();
            }
            else
            {
                txtTitleAfterReg.Text = "-";
            }
        }

        private void GetEducationBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducationById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtEducationBeforeReg.Text = listItem.ToString();
            }
            else
            {
                txtEducationBeforeReg.Text = "-";
            }
        }

        private void GetEducationAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducationById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtEducationAfterReg.Text = listItem.ToString();
            }
            else
            {
                txtEducationAfterReg.Text = "-";
            }
        }

        private void GetNationalityBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationalityById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtNationalityBeforeReg.Text = listItem.ToString();
            }
            else
            {
                txtNationalityBeforeReg.Text = "-";
            }
        }

        private void GetNationalityAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetNationalityById(id);

            if (ls != null)
            {
                ListItem listItem = new ListItem();
                listItem.Value = ls.Id;
                listItem.Text = ls.Name;

                txtNationalityAfterReg.Text = listItem.ToString();
            }
            else
            {
                txtNationalityAfterReg.Text = "-";
            }
        }

        private void GetProvinceCurrentAddressBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);

            if (ls.DataResponse != null)
            {
                txtProvinceCurrentAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtProvinceCurrentAddressBeforeReg.Text = "-";
            }


        }

        private void GetProvinceCurrentAddressAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);

            if (ls.DataResponse != null )
            {
                txtProvinceCurrentAddressAfterReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtProvinceCurrentAddressAfterReg.Text = "-";
            }


        }

        private void GetAmpurCurrentAddressBefore(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (ls.DataResponse != null)
            {
                txtDistrictCurrentAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtDistrictCurrentAddressBeforeReg.Text = "-";
            }


        }

        private void GetAmpurCurrentAddressAfter(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (ls.DataResponse != null)
            {
                txtDistrictCurrentAddressAfterReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtDistrictCurrentAddressAfterReg.Text = "-";
            }


        }

        private void GetTumbonCurrentAddressBefore(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (ls.DataResponse != null)
            {
                txtParishCurrentAddressBeforeReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtParishCurrentAddressBeforeReg.Text = "-";
            }

        }

        private void GetTumbonCurrentAddressAfter(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (ls.DataResponse != null)
            {
                txtParishCurrentAddressAfterReg.Text = ls.DataResponse.Name;
            }
            else
            {
                txtParishCurrentAddressAfterReg.Text = "-";
            }

        }

        private void GetProvinceRegisterAddressBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);

            if (DTO.MemberType.General.GetEnumValue() == EDIT_USER_TYPE)//edit by milk
            {
                if (ls.DataResponse != null)
                {
                    txtProvinceRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtProvinceRegisterAddressBeforeReg.Text = "-";
                }
            }
            else
            {
                if (ls.DataResponse != null && UserProfile.MemberType == DTO.MemberType.General.GetEnumValue())
                {
                    txtProvinceRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtProvinceRegisterAddressBeforeReg.Text = "-";
                }
            }

        }

        private void GetProvinceRegisterAddressAfter(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);

            if (DTO.MemberType.General.GetEnumValue() == EDIT_USER_TYPE)//edit by milk
            {
                if (ls.DataResponse != null)
                {
                    txtProvinceRegisterAddressAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtProvinceRegisterAddressAfterReg.Text = "-";
                }
            }
            else
            {
                if (ls.DataResponse != null && UserProfile.MemberType == DTO.MemberType.General.GetEnumValue())
                {
                    txtProvinceRegisterAddressAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtProvinceRegisterAddressAfterReg.Text = "-";
                }
            }
        }

        private void GetAmpurRegisterAddressBefore(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (DTO.MemberType.General.GetEnumValue() == EDIT_USER_TYPE)//edit by milk
            {
                if (ls.DataResponse != null)
                {
                    txtDistrictRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtDistrictRegisterAddressBeforeReg.Text = "-";
                }
            }
            else
            {

                if (ls.DataResponse != null && UserProfile.MemberType == DTO.MemberType.General.GetEnumValue())
                {
                    txtDistrictRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtDistrictRegisterAddressBeforeReg.Text = "-";
                }
            }
        }

        private void GetAmpurRegisterAddressAfter(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (DTO.MemberType.General.GetEnumValue() == EDIT_USER_TYPE)//edit by milk
            {
                if (ls.DataResponse != null)
                {
                    txtDistrictRegisterAddressAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtDistrictRegisterAddressAfterReg.Text = "-";
                }
            }
            else
            {
                if (ls.DataResponse != null && UserProfile.MemberType == DTO.MemberType.General.GetEnumValue())
                {
                    txtDistrictRegisterAddressAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtDistrictRegisterAddressAfterReg.Text = "-";
                }
            }
        }

        private void GetTumbonRegisterAddressBefore(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (DTO.MemberType.General.GetEnumValue() == EDIT_USER_TYPE)
            {
                if (ls.DataResponse != null)
                {
                    txtParishRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtParishRegisterAddressBeforeReg.Text = "-";
                }
            }
            else
            {
                if (ls.DataResponse != null && UserProfile.MemberType == DTO.MemberType.General.GetEnumValue())
                {
                    txtParishRegisterAddressBeforeReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtParishRegisterAddressBeforeReg.Text = "-";
                }
            }
        }

        private void GetTumbonRegisterAddressAfter(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (DTO.MemberType.General.GetEnumValue() == EDIT_USER_TYPE)//milk
            {
                if (ls.DataResponse != null )
                {
                    txtParishRegisterAddressAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtParishRegisterAddressAfterReg.Text = "-";
                }
            }
            else
            {
                if (ls.DataResponse != null && UserProfile.MemberType == DTO.MemberType.General.GetEnumValue())
                {
                    txtParishRegisterAddressAfterReg.Text = ls.DataResponse.Name;
                }
                else
                {
                    txtParishRegisterAddressAfterReg.Text = "-";
                }
            }
        }

        private void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);

            ucAttachFileControl1.DocumentTypes = ls;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "alert", "<script language=\"javascript\" > onOkSubmit(); </script>");

            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterStartupScript(this.GetType(), "onOkSubmit", "onOkSubmit()", true);

        }

        protected void btnOkSubmit_Click(object sender, EventArgs e)
        {
            var biz = new BLL.PersonBiz();
            string personID = this.PersonID;
            var res = biz.GetPersonTemp(personID);
            string userid = UserProfile.Id; 

            PersonTemp item = new PersonTemp();

            if (res.DataResponse != null)
            {
                item.ID = res.DataResponse.ID;
                item.MEMBER_TYPE = res.DataResponse.MEMBER_TYPE;
                item.COMP_CODE = res.DataResponse.COMP_CODE;
                item.PRE_NAME_CODE = res.DataResponse.PRE_NAME_CODE;
                item.NAMES = res.DataResponse.NAMES;
                item.LASTNAME = res.DataResponse.LASTNAME;
                item.SEX = res.DataResponse.SEX;
                item.ID_CARD_NO = res.DataResponse.ID_CARD_NO;
                item.BIRTH_DATE = res.DataResponse.BIRTH_DATE;
                item.EDUCATION_CODE = res.DataResponse.EDUCATION_CODE;
                item.NATIONALITY = res.DataResponse.NATIONALITY;
                item.EMAIL = res.DataResponse.EMAIL;
                item.LOCAL_TELEPHONE = res.DataResponse.LOCAL_TELEPHONE;
                item.TELEPHONE = res.DataResponse.TELEPHONE;
               
                //ขาด 2 ตัว
                //txtIDOicBeforeReg.Text =
                //txtIDMemberNumberAfterReg.Text = 
                //ขาด 2 ตัว

                item.ADDRESS_1 = res.DataResponse.ADDRESS_1;
                item.PROVINCE_CODE = res.DataResponse.PROVINCE_CODE;
                item.AREA_CODE = res.DataResponse.AREA_CODE;
                item.TUMBON_CODE = res.DataResponse.TUMBON_CODE;
                item.LOCAL_ADDRESS1 = res.DataResponse.LOCAL_ADDRESS1;
                item.LOCAL_PROVINCE_CODE = res.DataResponse.PROVINCE_CODE;
                item.LOCAL_AREA_CODE = res.DataResponse.LOCAL_AREA_CODE;
                item.LOCAL_TUMBON_CODE = res.DataResponse.LOCAL_TUMBON_CODE;
                item.CREATED_DATE = res.DataResponse.CREATED_DATE;

                item.ZIP_CODE = res.DataResponse.ZIP_CODE;
                item.LOCAL_ZIPCODE = res.DataResponse.LOCAL_ZIPCODE;

                item.STATUS = ddlStatus.SelectedValue;
                item.APPROVE_RESULT = txtResultReg.Text;
                item.APPROVED_BY = userid;
                item.AGENT_TYPE = res.DataResponse.AGENT_TYPE;
            }
            if (res.DataResponse == null)
            {
                this.UCModalError.ShowMessageError = res.ErrorMsg;
                this.UCModalError.ShowModalError();
                return;
            }

            if(ddlStatus.SelectedValue.Equals("5"))
            {
                item.STATUS = DTO.PersonDataStatus.Approve.GetEnumValue().ToString();
                item.APPROVE_RESULT = txtResultReg.Text;
                var final = biz.EditPerson(item);

                //ถ้าเกิด Error อะไรให้มาทำในที่นี่ Tob 12022013
                if (final.IsError)
                {
                    //Response.Write(final.ErrorMsg);

                    UCModalError.ShowMessageError = final.ErrorMsg;
                    UCModalError.ShowModalError();

                }
                else {
                    string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                }


  
            }
            else if (ddlStatus.SelectedValue.Equals("6") && res.DataResponse.STATUS == DTO.PersonDataStatus.WaitForApprove.GetEnumValue().ToString())
            {
                item.APPROVE_RESULT = txtResultReg.Text;
                var final = biz.EditPerson(item);

                //ถ้าเกิด Error อะไรให้มาทำในที่นี่ Tob 12022013
                if (final.IsError)
                {
                    //Response.Write(final.ErrorMsg);

                    UCModalError.ShowMessageError = final.ErrorMsg;
                    UCModalError.ShowModalError();

                }
                else
                {
                    string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                    ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                }
            }
            //else if (ddlStatus.SelectedValue.Equals("6"))
            //{
            //    List<String> persons = new List<string>() { item.ID };
            //    var final = biz.PersonNotApprove(persons);
            //    if (final.IsError)
            //    {

            //        UCModalError.ShowMessageError = final.ErrorMsg;
            //        UCModalError.ShowModalError();
            //    }
            //    else
            //    {
            //        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx')", SysMessage.SaveSucess);
            //        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
            //    }

            //}
            else if (ddlStatus.SelectedValue.Equals(""))
            {
                UCModalError.ShowMessageError = Resources.errorEdit_Reg_Person_004;
                UCModalError.ShowModalError();
            }
            
            //var final = biz.EditPerson(item);

            ////ถ้าเกิด Error อะไรให้มาทำในที่นี่ Tob 12022013
            //if (final.IsError)
            //{
            //    //Response.Write(final.ErrorMsg);

            //    UCModalError.ShowMessageError = final.ErrorMsg;
            //    UCModalError.ShowModalError();

            //}

            //AlertMessage.ShowAlertMessage(string.Empty, SysMessage.SaveSucess);

        }

        protected void gvUpload_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFileStatus = (Label)e.Row.FindControl("lblFileStatus");
                Label lblTypeAttachmentGv = (Label)e.Row.FindControl("lblTypeAttachmentGv");
                LinkButton lbnEditGv = (LinkButton)e.Row.FindControl("lbnEditGv");
                LinkButton hplView = (LinkButton)e.Row.FindControl("hplView");
                LinkButton hplDelete = (LinkButton)e.Row.FindControl("hplDelete");

                if (this.DataAction != DTO.DataActionMode.Add)
                {
                    if (lblFileStatus.Text == "A")
                    {
                        if (this.AttachFiles.Where(a => a.DocumentTypeName == lblTypeAttachmentGv.Text && a.FILE_STATUS != "A").Count() > 0)
                        {
                            hplView.Visible = false;

                            e.Row.Style.Value = "text-decoration:line-through;";
                        }
                    }
                    else if (lblFileStatus.Text == "D")
                    {
                        e.Row.Visible = false;
                    }
                    else if (lblFileStatus.Text == "E" || lblFileStatus.Text == "W")
                    {
                    
                        hplView.Visible = true;
                 
                    }
                }




            }
        }

        private void DisableControlByMemberType(string memberType)
        {
            if (memberType == DTO.MemberType.General.GetEnumValue().ToString())
            {
                lblTelCurrentAddressBeforeReg.Text = Resources.propEdit_Reg_Person_001;
                lblMobilePhoneBeforeReg.Text = Resources.propEdit_Reg_Person_002;

                this.pnlRegisAddressAfter.Visible = true;
                this.pnlRegisAddressBefore.Visible = true;


                lblIDOicBeforeReg.Visible = false;
                txtIDOicBeforeReg.Visible = false;
                lblMemberNumberBeforeReg.Visible = false;
                txtIDMemberNumberBeforeReg.Visible = false;

                lblIDOicAfterReg.Visible = false;
                txtIDOicAfterReg.Visible = false;
                lblMemberNumberAfterReg.Visible = false;
                txtIDMemberNumberAfterReg.Visible = false;

                lblCompanyBeforeReg.Visible = false;
                txtCompanyBeforeReg.Visible = false;
                lblCompanyAfterReg.Visible = false;
                txtCompanyAfterReg.Visible = false;
            }
            else
            {
                lblTelCurrentAddressBeforeReg.Text = Resources.propEdit_Reg_Person_003;
                lblMobilePhoneBeforeReg.Text = Resources.propEdit_Reg_Person_001;

                this.pnlRegisAddressAfter.Visible = false;
                this.pnlRegisAddressBefore.Visible = false;

                lblIDOicBeforeReg.Visible = false;
                txtIDOicBeforeReg.Visible = false;
                lblMemberNumberBeforeReg.Visible = false;
                txtIDMemberNumberBeforeReg.Visible = false;

                lblIDOicAfterReg.Visible = false;
                txtIDOicAfterReg.Visible = false;
                lblMemberNumberAfterReg.Visible = false;
                txtIDMemberNumberAfterReg.Visible = false;

                lblCompanyBeforeReg.Visible = true;
                txtCompanyBeforeReg.Visible = true;
                lblCompanyAfterReg.Visible = true;
                txtCompanyAfterReg.Visible = true;

            }
        }

        public string NullableString(string input)
        {
            if ((input == null) || (input == ""))
            {
                input = "";
            }

            return input;
        }
    }
}
