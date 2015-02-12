using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;

namespace IAS.DataServices.Applicant.ApplicantHelper
{
    public class PreNameHelper
    {
        public static String ConvertToCode(IAS.DAL.Interfaces.IIASPersonEntities ctx, String source) 
        {
            string title = (source == "น.ส.") ? "นางสาว" : source;
            VW_IAS_TITLE_NAME entTitle = ctx.VW_IAS_TITLE_NAME.FirstOrDefault(s => s.NAME == title);
            if (entTitle == null)
            {
                return "999";
            }
            else {
                return entTitle.ID.ToString();
            }
        }
    }
}