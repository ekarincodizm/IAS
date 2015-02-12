using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;
using IAS.DTO;

namespace IAS.Mockup
{
    public partial class InsertSingleLicense : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Init();

            }
        }

        private void Init()
        {
            LicenseBiz biz = new LicenseBiz();
            #region Header Init
            List<PersonLicenseHead> headerls = new List<PersonLicenseHead>(); // TODO: Initialize to an appropriate value
            PersonLicenseHead headEnt = new PersonLicenseHead();
            headEnt.APPROVE_COMPCODE = "111";
            headEnt.APPROVED_BY = null;
            headEnt.APPROVED_DATE = null;
            headEnt.APPROVED_DOC = "W";
            headEnt.COMP_CODE = null;
            headEnt.COMP_NAME = null;
            headEnt.FILENAME = null;
            headEnt.FLAG_LIC = null;
            headEnt.FLAG_REQ = null;
            headEnt.LICENSE_TYPE_CODE = "03";
            headEnt.LOTS = 1;
            headEnt.MONEY = 200;
            headEnt.PAYMENT_NO = null;
            headEnt.PETITION_TYPE_CODE = "16";
            headEnt.REQUEST_NO = null;
            headEnt.TRAN_DATE = Convert.ToDateTime("1/7/2557");
            headEnt.UPLOAD_BY_SESSION = "140609145948540";
            headEnt.UPLOAD_GROUP_NO = "140701152850898";
            headerls.Add(headEnt);
            #endregion

            #region Detail Init
            List<PersonLicenseDetail> detaills = new List<PersonLicenseDetail>(); // TODO: Initialize to an appropriate value
            PersonLicenseDetail detailEnt = new PersonLicenseDetail();
            detailEnt.ADDRESS_1 = "asdfasfdasf";
            detailEnt.ADDRESS_2 = null;
            detailEnt.APPROVED = "W";
            detailEnt.APPROVED_BY = null;
            detailEnt.APPROVED_DATE = null;
            detailEnt.AR_DATE = Convert.ToDateTime("1/1/0544");
            detailEnt.AREA_CODE = "10";
            detailEnt.CURRENT_ADDRESS_1 = "asdfasfdasf";
            detailEnt.CURRENT_ADDRESS_2 = null;
            detailEnt.CURRENT_AREA_CODE = "10";
            detailEnt.EMAIL = "ss@ss.ss";
            detailEnt.ERR_DESC = null;
            detailEnt.FEES = 200;
            detailEnt.HEAD_REQUEST_NO = null;
            detailEnt.ID_CARD_NO = "1589900053854";
            detailEnt.LASTNAME = "มุ่งดี";
            detailEnt.LICENSE_DATE = null;
            detailEnt.LICENSE_EXPIRE_DATE = null;
            detailEnt.LICENSE_NO = "5603007789";
            detailEnt.NAMES = "เบญจมาส";
            detailEnt.OLD_COMP_CODE = null;
            detailEnt.ORDERS = null;
            detailEnt.PAY_EXPIRE = Convert.ToDateTime("21/5/2558");
            detailEnt.PRE_NAME_CODE = "2";
            detailEnt.RENEW_TIMES = "1";
            detailEnt.REQUEST_NO = null;
            detailEnt.SEQ_NO = "0001";
            detailEnt.TITLE_NAME = "นางสาว";
            detailEnt.UPLOAD_GROUP_NO = "140701152850898";
            detaills.Add(detailEnt);
            #endregion

            #region UserProfile Init
            UserProfile userProfile = new UserProfile(); // TODO: Initialize to an appropriate value
            userProfile.AgentType = "Z";
            userProfile.CompCode = null;
            userProfile.DepartmentCode = null;
            userProfile.DepartmentName = null;
            userProfile.DeptCode = null;
            userProfile.EmployeeCode = null;
            userProfile.EmployeeName = null;
            userProfile.Id = "140609145948540";
            userProfile.IdCard = "1589900053854";
            userProfile.IS_APPROVE = "Y";
            userProfile.LastName = null;
            userProfile.LicenseNo = null;
            userProfile.LoginName = "1589900053854";
            userProfile.LoginStatus = "0";
            userProfile.MemberType = 1;
            userProfile.Name = "เบญจมาส มุ่งดี";
            userProfile.OIC_EMP_NO = null;
            userProfile.OIC_User_Id = null;
            userProfile.OIC_User_Type = null;
            userProfile.PositionCode = null;
            userProfile.PositionName = null;
            userProfile.STATUS = "2";
            userProfile.TitleName = null;
            userProfile.UserGroup = null;
            #endregion

            #region AttatchFileLicense Init
            List<AttatchFileLicense> files = new List<AttatchFileLicense>(); // TODO: Initialize to an appropriate value
            int count = 2;
            for (int i = 0; i < count; i++)
            {
                AttatchFileLicense file = new AttatchFileLicense();
                if (i == 1)
                {
                    file.ATTACH_FILE_PATH = "Temp\\140609145948540\\140701152905098_03.jpg";
                    file.ATTACH_FILE_TYPE = "03";
                    file.CREATED_BY = "1589900053854";
                    file.CREATED_DATE = Convert.ToDateTime("1/7/2557");
                    file.FILE_STATUS = "W";
                    file.GROUP_LICENSE_ID = "140701152850898";
                    file.ID_ATTACH_FILE = "140609145948540";
                    file.ID_CARD_NO = "1589900053854";
                    file.LICENSE_NO = null;
                    file.REMARK = "hhh";
                    file.RENEW_TIME = null;
                    file.UPDATED_BY = "1589900053854";
                    file.UPDATED_DATE = Convert.ToDateTime("1/7/2557");
                }
                else
                {

                    file.ATTACH_FILE_PATH = "Temp\\140609145948540\\140701153226031_65.jpg";
                    file.ATTACH_FILE_TYPE = "65";
                    file.CREATED_BY = "1589900053854";
                    file.CREATED_DATE = Convert.ToDateTime("1/7/2557");
                    file.FILE_STATUS = "W";
                    file.GROUP_LICENSE_ID = "140701153215473";
                    file.ID_ATTACH_FILE = "140609145948540";
                    file.ID_CARD_NO = "1589900053854";
                    file.LICENSE_NO = null;
                    file.REMARK = "";
                    file.RENEW_TIME = null;
                    file.UPDATED_BY = "1589900053854";
                    file.UPDATED_DATE = Convert.ToDateTime("1/7/2557");
                }

                files.Add(file);
            }
            #endregion


            DTO.ResponseMessage<bool> resInsertLicense = biz.InsertPersonLicense(headerls.ToArray(), detaills.ToArray(), userProfile, files.ToArray());


            if (resInsertLicense.ResultMessage == true)
            {
                lblRes.ForeColor = System.Drawing.Color.Green;
                lblRes.Text = Convert.ToString(resInsertLicense.ResultMessage);
            }
            else
            {
                lblRes.ForeColor = System.Drawing.Color.Red;
                lblRes.Text = resInsertLicense.ErrorMsg;
            }

        }
    }
}