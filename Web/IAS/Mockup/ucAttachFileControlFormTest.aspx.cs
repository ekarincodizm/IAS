using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IAS.BLL.AttachFilesIAS;

namespace IAS.Mockup
{
    public partial class ucAttachFileControlFormTest : System.Web.UI.Page
    {
        private String _registrationId = "";
        public String RegistrationId { get { return _registrationId; } set { _registrationId = value; } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RegistrationId = "130924103253839";

                ucAttachFileControl1.RegisterationId = "130924103253839";

                BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

                DTO.ResponseService<DTO.RegistrationAttatchFile[]> res = biz.GetAttatchFilesByRegisterationID(RegistrationId);

                GetAttachFilesType();

                var list = res.DataResponse.ToList();

                ucAttachFileControl1.AttachFiles = list.ConvertToAttachFilesView();

            }

        }


        public void GetAttachFilesType()
        {
            //string memtype = UserProfile.MemberType.ToString();
            //string funcid = Convert.ToString((int)DTO.DocFunction.REGISTER_FUNCTION);

            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            this.ucAttachFileControl1.DocumentTypes = ls;
        }
    }
}