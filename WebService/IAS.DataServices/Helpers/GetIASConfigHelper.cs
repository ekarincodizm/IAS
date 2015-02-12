using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;

namespace IAS.DataServices.Helpers
{
    public class GetIASConfigHelper
    {
        public static AG_IAS_ATTACH_FILE_LICENSE GetAttachLicensePhoto(IAS.DAL.Interfaces.IIASPersonEntities ctx, String idCardNo, String uploadGroupNo)
        {
            AG_IAS_CONFIG config = ctx.AG_IAS_CONFIG.SingleOrDefault(a=>a.ID == "13");
            if(config==null || String.IsNullOrEmpty(config.ITEM_VALUE) )
                throw new ApplicationException("ไม่พบข้อมูล Config รูปภาพ.");

             var attach = ctx.AG_IAS_ATTACH_FILE_LICENSE.FirstOrDefault(a => a.ID_CARD_NO == idCardNo
                                    && a.GROUP_LICENSE_ID == uploadGroupNo
                                    && a.ATTACH_FILE_TYPE== config.ITEM_VALUE);
             return attach;
        } 
    }
}