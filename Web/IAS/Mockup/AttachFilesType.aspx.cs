using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.Mockup
{
    public partial class AttachFilesType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetAttachFilesType();
        }

        public void GetAttachFilesType()
        {
            var message = SysMessage.DefaultSelecting;
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            var ls = biz.GetDocumentType(message);
            //this.ucAttachFileControl1.DocumentTypes = ls;

            var ls1 = ls.Where(s => s.EXAM_DISCOUNT_STATUS == null && s.TRAIN_DISCOUNT_STATUS == null).ToList();
            //var ls2 = ls.Where(s => s.EXAM_DISCOUNT_STATUS != "Y" || s.TRAIN_DISCOUNT_STATUS != "Y").ToList();
            var ls3 = ls.Where(s => s.EXAM_DISCOUNT_STATUS == "N" && s.TRAIN_DISCOUNT_STATUS == "N").ToList();

            var lss1 = ls1.Union(ls3).ToList();

            List<DTO.DataItem> newls = ls.Where(s => s.EXAM_DISCOUNT_STATUS == null && s.TRAIN_DISCOUNT_STATUS == null).ToList()
                .Union(ls.Where(s => s.EXAM_DISCOUNT_STATUS == "N" && s.TRAIN_DISCOUNT_STATUS == "N").ToList()).ToList().OrderBy(code => code.Id).ToList();

            //this.ucAttachFileControl1.DocumentTypes = newls;
        }
    }
}