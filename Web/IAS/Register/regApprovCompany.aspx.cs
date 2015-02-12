using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;
using IAS.Utils;
using System.Data;
using System.Web.Configuration;
using AjaxControlToolkit;
using IAS.DTO;
using IAS.BLL.AttachFilesIAS;
using IAS.Properties;

namespace IAS.Register
{
    public partial class regApprovCompany : basepage
    {

        public string RegStatus
        {
            get { return Session["RegStatus"] == null ? string.Empty : Session["RegStatus"].ToString(); }
            set { Session["RegStatus"] = value; }
        }

        public string PersonalStatus
        {
            get { return Session["personalstatus"] == null ? string.Empty : Session["personalstatus"].ToString(); }
            set { Session["personalstatus"] = value; }
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
        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session["UserProfile"] == null ? null : (DTO.UserProfile)Session["UserProfile"];
            }
        }
        DTO.DataActionMode _DataAction;
        public DTO.DataActionMode DataAction
        {
            get
            {
                _DataAction = Session["UserProfile"] == null ? DTO.DataActionMode.Add : DTO.DataActionMode.Edit;

                return _DataAction;
            }
            set
            {
                _DataAction = value;
            }
        }
        string mapPath;



        public List<DTO.DataItem> GetDocumentTypeIsImage
        {
            get
            {
                if (Session["DocumentTypeIsImage"] == null)
                {
                    Session["DocumentTypeIsImage"] = new List<DTO.DataItem>();
                }
                return (List<DTO.DataItem>)Session["DocumentTypeIsImage"];
            }
            set
            {
                Session["DocumentTypeIsImage"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Add new
                string Mode = DataActionMode.View.ToString();
                ucAttachFileControl1.ModeForm = DataActionMode.View;              
                mapPath = WebConfigurationManager.AppSettings["UploadFilePath"];
                Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
                txtApproval.Text = DateTime.Today.ToString("dd MMMM yyyy");

                GetRegistrationDetail();
                GetStatusApproval();
                txtUsername.Text = base.UserProfile.Name;
                Disablecontrol();
                GetAttatchFiles();
            
            }
        }

        private void GetAttatchFiles()
        {
            var biz = new BLL.PersonBiz();
            string personID = this.PersonID;
            DTO.ResponseService<DTO.PersonAttatchFile[]> res = biz.GetAttatchFileByPersonId(personID);
            var list = res.DataResponse.ToList();
            ucAttachFileControl1.AttachFiles = list.ConvertToAttachFilesView();

        }

        private void Disablecontrol()
        {
            ucAttachFileControl1.EnableGridView(false);
            ucAttachFileControl1.VisableUpload(false);
            ucAttachFileControl1.EnableUpload(false);
        }
        private void GetRegistrationDetail()
        {
            var biz = new BLL.RegistrationBiz();
            var dbiz = new BLL.DataCenterBiz();
            string personID = this.PersonID;
            var res = biz.GetById(personID);
            var comp = string.Empty;
            if (!string.IsNullOrEmpty(res.DataResponse.COMP_CODE))
            {
                if (res.DataResponse.MEMBER_TYPE.Equals(DTO.MemberType.Association.GetEnumValue().ToString()))
                {
                    DTO.ResponseService<DTO.ASSOCIATION> getass = dbiz.GetInsuranceAssociateNameByID(res.DataResponse.COMP_CODE);
                    if (getass.DataResponse != null)
                    {
                        comp = getass.DataResponse.ASSOCIATION_NAME;
                    }
                    //DTO.ResponseService<string> cc = dbiz.GetAssociateNameById(res.DataResponse.COMP_CODE);
                    //comp = cc.DataResponse.ToString();
                    //string dd = dbiz.GetAssociateNameById(res.DataResponse.COMP_CODE).ToString();
                }
                else
                {
                    comp = dbiz.GetCompanyNameById(res.DataResponse.COMP_CODE);
                }
            }
            if (res != null)
            {
                this.PersonalStatus = res.DataResponse.STATUS;
                int title = Convert.ToInt32(res.DataResponse.PRE_NAME_CODE);
                GetTitleAfter(title);
                if (res.DataResponse.MEMBER_TYPE != null)
                {
                    if (res.DataResponse.MEMBER_TYPE == "2")
                    {
                        txtMemberType.Text = Resources.propReg_Co_MemberTypeCompany;
                        lblTitleType.Text = Resources.propregApproveCompany_001;
                        lblTitletypePerson.Text = Resources.propregApproveCompany_002;
                        lblCompanyRegister.Text = Resources.propregApproveCompany_003;
                        lblRegister.Text = "ลงทะเบียน "+ Resources.propReg_Co_MemberTypeCompany +" (อนุมัติ)";
                        lblCompany.Text = Resources.propReg_Co_MemberTypeCompany;
                    }
                    else if (res.DataResponse.MEMBER_TYPE == "3")
                    {
                        txtMemberType.Text = Resources.propReg_Assoc_MemberTypeAssoc;
                        lblTitleType.Text = Resources.propregApproveCompany_004;
                        lblTitletypePerson.Text = Resources.propregApproveCompany_005;
                        lblCompanyRegister.Text = Resources.propregApproveCompany_006;
                        lblRegister.Text = "ลงทะเบียน " + Resources.propReg_Assoc_MemberTypeAssoc + " (อนุมัติ)";
                        lblCompany.Text = Resources.propReg_Assoc_MemberTypeAssoc;
                    }
                }
                else
                {
                    //txtTypeMemberBeforeReg.Text = "-";
                }
                if (comp != null)
                {
                    txtCompany.Text = comp;
                }
                else
                {
                    txtCompany.Text = "-";
                }

                if (res.DataResponse.COMP_CODE != null)
                {
                    txtCompanyRegister.Text = res.DataResponse.COMP_CODE;
                }
                else
                {
                    txtCompanyRegister.Text = "-";
                }

                if (res.DataResponse.NAMES != null)
                {
                    txtFirstName.Text = res.DataResponse.NAMES;
                }
                else
                {
                    txtFirstName.Text = "-";
                }

                if (res.DataResponse.LASTNAME != null)
                {
                    txtLastName.Text = res.DataResponse.LASTNAME;
                }
                else
                {
                    txtLastName.Text = "-";
                }

                if (res.DataResponse.SEX != null)
                {
                    rblSex.SelectedValue = res.DataResponse.SEX;
                }
                else
                {
                    rblSex.SelectedIndex = 0;
                }

                if (res.DataResponse.ID_CARD_NO != null)
                {
                    txtIDNumber.Text = res.DataResponse.ID_CARD_NO;
                }
                else
                {
                    txtIDNumber.Text = "-";
                }          

           

                if (res.DataResponse.EMAIL != null)
                {
                    txtEmail.Text = res.DataResponse.EMAIL;
                }
                else
                {
                    txtEmail.Text = "-";
                }

                if (res.DataResponse.LOCAL_TELEPHONE != null)
                {
                    txtCompanyTel.Text = txtCompanyTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(res.DataResponse.LOCAL_TELEPHONE);
                    txtCompanyTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(res.DataResponse.LOCAL_TELEPHONE);
                }
                else
                {
                    txtCompanyTel.Text = "-";
                }

                if (res.DataResponse.TELEPHONE != null)
                {
                    txtTel.Text = LocalTelephoneNumberHelper.GetPhoneNumber(res.DataResponse.TELEPHONE);
                    txtTelExt.Text = LocalTelephoneNumberHelper.GetExtenNumber(res.DataResponse.TELEPHONE);
                }
                else
                {
                    txtTel.Text = "-";
                }
                if (res.DataResponse.ADDRESS_1 != null)
                {
                   txtAddress.Text = res.DataResponse.ADDRESS_1;
                }
                else
                {
                    txtAddress.Text = "-";
                }

                string province = res.DataResponse.PROVINCE_CODE;
                GetProvinceCurrentAddressBefore(province);

                string ampur = res.DataResponse.AREA_CODE;
                GetAmpurCurrentAddressBefore(province, ampur);

                string tumbon = res.DataResponse.TUMBON_CODE;
                GetTumbonCurrentAddressBefore(province, ampur, tumbon);

                if (res.DataResponse.LOCAL_ADDRESS1 != null)
                {
                    //txtRegisterAddressBeforeReg.Text = res.DataResponse.LOCAL_ADDRESS1;
                }
                else
                {
                   // txtRegisterAddressBeforeReg.Text = "-";
                }


                string localProvince = res.DataResponse.LOCAL_PROVINCE_CODE;
               // GetProvinceRegisterAddressBefore(localProvince);

                string localampur = res.DataResponse.LOCAL_AREA_CODE;
                //GetAmpurRegisterAddressBefore(localProvince, localampur);

                string localtumbon = res.DataResponse.LOCAL_TUMBON_CODE;
               // GetTumbonRegisterAddressBefore(localProvince, localampur, localtumbon);

                //Add new 28/8/2556
                if (res.DataResponse.ZIP_CODE != null)
                {
                    //txtPostcodeRegisterAddress.Enabled = false;
                    txtPost.Text = res.DataResponse.ZIP_CODE;
                }
                else//milk
                {
                    //txtPostcodeCurrentAddress.Text = "-";
                }

                if (res.DataResponse.LOCAL_ZIPCODE != null)
                {
                    //txtPostcodeCurrentAddress.Enabled = false;

                    //txtPostcodeRegisterAddress.Text = res.DataResponse.LOCAL_ZIPCODE;
                }
                else//milk
                {
                    //txtPostcodeRegisterAddress.Text = "-";
                }

                txtResultReg.Text = res.DataResponse.APPROVE_RESULT;
            }

            if (res.IsError)
            {
                //UCModalError.ShowMessageError = res.ErrorMsg;
                //UCModalError.ShowModalError();
            }

        }
        private void GetStatusApproval()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetStatus(SysMessage.DefaultSelecting);

            List<DTO.DataItem> newls = new List<DTO.DataItem>();
            if (ls.Count >= 0)
            {
                if (this.PersonalStatus.Equals("1") || this.PersonalStatus.Equals("2") || this.PersonalStatus.Equals("3"))
                {
                    for (int i = 0; i < ls.Count; i++)
                    {

                        if (ls[i].Id.Equals("") || ls[i].Id.Equals("2") || ls[i].Id.Equals("3"))
                        {
                            DTO.DataItem item = new DTO.DataItem();
                            item.Name = ls[i].Name;
                            item.Id = ls[i].Id;
                            newls.Add(item);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < ls.Count; i++)
                    {

                        if (ls[i].Id.Equals("4") || ls[i].Id.Equals("5") || ls[i].Id.Equals("6"))
                        {
                            DTO.DataItem item = new DTO.DataItem();
                            item.Name = ls[i].Name;
                            item.Id = ls[i].Id;
                            newls.Add(item);
                        }

                    }

                }

            }
            BindToDDL(ddlStatusApproval, newls);
        }
        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };
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
        private void GetProvinceCurrentAddressBefore(string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetProvinceById(id);

            if (ls.DataResponse != null)
            {
                txtProvince.Text = ls.DataResponse.Name;
            }
            else
            {
                txtProvince.Text = "-";
            }
        }
        private void GetAmpurCurrentAddressBefore(string provinceId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetAmpurById(provinceId, id);

            if (ls.DataResponse != null)
            {
                txtAmpher.Text = ls.DataResponse.Name;
            }
            else
            {
                txtAmpher.Text = "-";
            }
        }

        private void GetTumbonCurrentAddressBefore(string provinceId, string ampurId, string id)
        {
            var biz = new BLL.DataCenterBiz();
            var ls = biz.GetTumbonById(provinceId, ampurId, id);

            if (ls.DataResponse != null)
            {
                txtTumbol.Text = ls.DataResponse.Name;
            }
            else
            {
                txtTumbol.Text = "-";
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();
            BLL.PersonBiz bizPerson = new BLL.PersonBiz();

            var detail = biz.GetRegistrationsByCriteria(txtFirstName.Text, txtLastName.Text, null, null, txtIDNumber.Text, null, txtEmail.Text, null, Session["RegStatus"].ToString(), 1, 20, "2");
            DataSet ds = detail.DataResponse;

            #region Get MemberType From Control @Nattapong
            string RegMemType = string.Empty;
            if (txtMemberType.Text != "")
            {
                switch (txtMemberType.Text)
                {
                    case "บุคคลทั่วไป":
                        RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
                        break;
                    case "บุคคลทั่วไป/ตัวแทน/นายหน้า":
                        RegMemType = Convert.ToString((int)DTO.MemberType.General.GetEnumValue());
                        break;
                    case "บริษัทประกัน":
                        RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
                        break;
                    case "บริษัท":
                        RegMemType = Convert.ToString((int)DTO.MemberType.Insurance.GetEnumValue());
                        break;
                    case "สมาคม":
                        RegMemType = Convert.ToString((int)DTO.MemberType.Association.GetEnumValue());
                        break;
                }
            }

            #endregion

            if (ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                var id = dt.AsEnumerable().Select(s => s.Field<string>("ID"));

                List<string> ls = new List<string>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strID = Convert.ToString(dt.Rows[i]["ID"]);
                    ls.Add(strID);
                }

                if (ddlStatusApproval.SelectedValue.Equals("2"))
                {
                    //var res = biz.RegistrationApprove(ls, "อนุมัติกลุ่ม");
                    string userid = UserProfile.Id;
                    var res = biz.RegistrationApprove(ls, txtResultReg.Text, userid, RegMemType);
                    if (res.IsError)
                    {
                        UCModalError1.ShowMessageError = res.ErrorMsg;
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        //UCModalSuccess.ShowMessageSuccess = "ทำรายการเรียบร้อย";
                        //UCModalSuccess.ShowModalSuccess();
                        ////Response.Redirect("~/Register/regSearchOfficerOIC.aspx");
                        //btnOk.Enabled = false;
                        //btnCancel.Text = "ย้อนกลับ";
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }
                else if (ddlStatusApproval.SelectedValue.Equals("3"))
                {
                    //string userid = UserProfile.Id;
                    string userid = Session["PersonalIDCard"].ToString();
                    var res = biz.RegistrationNotApprove(ls, txtResultReg.Text.ToString(), userid);
                    if (res.IsError)
                    {
                        UCModalError1.ShowMessageError = res.ErrorMsg;
                        UCModalError1.ShowModalError();
                    }
                    else
                    {
                        //UCModalSuccess.ShowMessageSuccess = "ทำรายการเรียบร้อย";
                        //UCModalSuccess.ShowModalSuccess();
                        ////Response.Redirect("~/Register/regSearchOfficerOIC.aspx");
                        //btnOk.Enabled = false;
                        //btnCancel.Text = "ย้อนกลับ";
                        string AlertSussuss = String.Format("alert('{0}');window.location.assign('../Register/regSearchOfficerOIC.aspx?Back=R')", SysMessage.SaveSucess);
                        ToolkitScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", AlertSussuss, true);
                    }
                }              
               
                else if (ddlStatusApproval.SelectedValue.Equals(""))
                {
                    UCModalError1.ShowMessageError = Resources.errorEdit_Reg_Person_004;
                    UCModalError1.ShowModalError();
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Register/regSearchOfficerOIC.aspx?Back=R");
        }

    }
}