using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL;

namespace IAS.UserControl
{
    public partial class ucCurrentLicense : System.Web.UI.UserControl
    {
        #region public & private Session
        public MasterPage.MasterRegister MasterRegis
        {

            get
            {
                return (this.Page.Master as MasterPage.MasterRegister);
            }
        }

        public GridView GridCurrentLicense { get { return gvCurrentLicense; } set { gvCurrentLicense = value; } }

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }

        }
        #endregion

        #region UI Function

        #endregion

        #region Private & Public Function
        public void GetCurrentLicense(string idCard)
        {
            LicenseBiz biz = new LicenseBiz();

            //Get All Licese By ID_CARD_NO
            //DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetAllLicenseByIDCard(this.MasterRegis.UserProfile.IdCard);
            DTO.ResponseService<DTO.PersonLicenseTransaction[]> res = biz.GetAllLicenseByIDCard(idCard, "1", 1);
            if (res.DataResponse != null)
            {
                gvCurrentLicense.DataSource = res.DataResponse;
                gvCurrentLicense.DataBind();
            }
            else if (res.IsError)
            {
                this.MasterRegis.ModelError.ShowMessageError = res.ErrorMsg;
                this.MasterRegis.ModelError.ShowModalError();
                return;

            }

        }
        #endregion

    }
}