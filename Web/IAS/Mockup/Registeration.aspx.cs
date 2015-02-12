using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IAS.BLL.AttachFilesIAS;
using IAS.MasterPage;

namespace IAS.Mockup
{
    public partial class Registeration : System.Web.UI.Page
    {
        public DTO.UserProfile UserProfile
        {
            get
            {
                return Session[PageList.UserProfile] == null ? null : (DTO.UserProfile)Session[PageList.UserProfile];
            }
        }

        public Site1 MasterPage
        {
            get { return (this.Page.Master as Site1); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack) {

                IList<AttachFile> _listAttachs = new List<BLL.AttachFilesIAS.AttachFile>();
                _listAttachs.Add(new AttachFile() { ID = "130828173805065", REGISTRATION_ID = "130828190050115", ATTACH_FILE_TYPE = "01", ATTACH_FILE_PATH = @"AttachFile\130828190050115\130828173805065.jpg", REMARK = "", CREATED_BY = "130828173711229", CREATED_DATE = DateTime.Now, UPDATED_BY = "130828173711229", UPDATED_DATE = DateTime.Now, FILE_STATUS = "A" });
                _listAttachs.Add(new AttachFile() { ID = "130828174114748", REGISTRATION_ID = "130828190050115", ATTACH_FILE_TYPE = "02", ATTACH_FILE_PATH = @"AttachFile\130828190050115\130828174114748.jpg", REMARK = "", CREATED_BY = "130828174026918", CREATED_DATE = DateTime.Now, UPDATED_BY = "130828174026918", UPDATED_DATE = DateTime.Now, FILE_STATUS = "A" });
                _listAttachs.Add(new AttachFile() { ID = "130828174120801", REGISTRATION_ID = "130828190050115", ATTACH_FILE_TYPE = "03", ATTACH_FILE_PATH = @"AttachFile\130828190050115\130828174120801.jpg", REMARK = "", CREATED_BY = "130828174026918", CREATED_DATE = DateTime.Now, UPDATED_BY = "130828174026918", UPDATED_DATE = DateTime.Now, FILE_STATUS = "A" });


                InitDocumentType();
                ucAttachFileControl1.AttachFiles = _listAttachs.ToList();
                ucAttachFileControl1.RegisterationId = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                ucAttachFileControl1.CurrentUser = "user1";
                ucAttachFileControl1.ModeForm = DTO.DataActionMode.Add ;
            }
        }


        private void InitDocumentType() {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            ucAttachFileControl1.DocumentTypes = ls.ToList();
        }
    }
}