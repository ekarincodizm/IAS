using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.Utils;
using IAS.Properties;

namespace IAS.UserControl
{
    public partial class ucPersonalApplicantDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        private Action<DropDownList, List<DTO.DataItem>> BindToDDL = (ddl, ls) =>
        {
            ddl.DataTextField = "Name";
            ddl.DataValueField = "Id";
            ddl.DataSource = ls;
            ddl.DataBind();
        };

        public void GetTitleName()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetTitleName(SysMessage.DefaultSelecting);
            BindToDDL(ddlDetailTitleName, ls);
        }

        public void GetEducation()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetEducation(SysMessage.DefaultSelecting);
            BindToDDL(ddlDetailEducation, ls);
        }

        public void PersonApplicantReadOnlyMode()
        {
            ddlDetailTitleName.Enabled = false;
            txtDetailName.ReadOnly = true;
            txtDetailLastName.ReadOnly = true;
            txtDetailIdCardNumber.ReadOnly = true;
            txtDetailPersonExam.ReadOnly = true;
            txtDetailExamCode.ReadOnly = true;
            txtDetailYardCode.ReadOnly = true;
            txtDetailAssocExam.ReadOnly = true;
            txtDetailCompany.ReadOnly = true;
            ddlDetailEducation.Enabled = false;
            txtSex.ReadOnly = true;
            txtDetailBirthDay.ReadOnly = true;
            txtDetailExamDate.ReadOnly = true;
        }

        public void BindApplicantUploadTempByID(string strGroupID, string strSeqNo)
        {
            BLL.ApplicantBiz biz = new BLL.ApplicantBiz();
            var res = biz.GetApplicantUploadTempById(strGroupID, strSeqNo);
            if (res.IsError)
            {
                var errorMsg = res.ErrorMsg;
                AlertMessage.ShowAlertMessage(string.Empty, errorMsg);
            }
            else
            {
                txtDetailName.Text = res.DataResponse.NAMES;
                ddlDetailTitleName.Text = res.DataResponse.PRE_NAME_CODE;
                txtDetailLastName.Text = res.DataResponse.LASTNAME;
                txtDetailIdCardNumber.Text = res.DataResponse.ID_CARD_NO;
                string strBD = res.DataResponse.BIRTH_DATE == null ? string.Empty : res.DataResponse.BIRTH_DATE.Value.ToString("dd/MM/yyyy");
                txtDetailBirthDay.Text = strBD;
                string strTestingDate = res.DataResponse.TESTING_DATE == null ? string.Empty : res.DataResponse.TESTING_DATE.Value.ToString("dd/MM/yyyy");
                txtDetailExamDate.Text = strTestingDate;
                string strED = res.DataResponse.EDUCATION_CODE == null ? string.Empty : res.DataResponse.EDUCATION_CODE.ToString();
                ddlDetailEducation.Text = strED;
                string strAppCode = res.DataResponse.APPLICANT_CODE == null ? string.Empty : res.DataResponse.APPLICANT_CODE.Value.ToString();
                txtDetailPersonExam.Text = strAppCode;
                txtDetailExamCode.Text = res.DataResponse.TESTING_NO;
                txtDetailYardCode.Text = res.DataResponse.EXAM_PLACE_CODE;
                txtDetailAssocExam.Text = res.DataResponse.ACCEPT_OFF_CODE;
                txtDetailCompany.Text = res.DataResponse.INSUR_COMP_CODE;
                if (res.DataResponse.SEX == "M")
                {
                    txtSex.Text = Resources.propLicenseGroup_Male;
                }
                else
                {
                    txtSex.Text = Resources.propLicenseGroup_Female;
                }
            }

        }



    }
}