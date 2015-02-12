using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Configuration;
using IAS.DataServices.Properties;
using IAS.Common.Email;
using IAS.DAL;
using IAS.DataServices.Helpers;

namespace IAS.DataServices.Payment.Helpers
{
    public class MailUpdatePaymentHelper
    {
        public static void ConcreateEmail(IAS.DAL.Interfaces.IIASPersonEntities ctx, IEnumerable<DTO.ImportBankTransferData> datas) 
        {

            IEnumerable<AG_IAS_PERSONAL_T> personalAdmins = ctx.AG_IAS_PERSONAL_T.Where(a=>a.MEMBER_TYPE=="4" || a.MEMBER_TYPE=="5");

            String emailOut = ConfigurationManager.AppSettings["EmailOut"];

            StringBuilder emailBody = new StringBuilder();

            emailBody.AppendLine("รายละเอียดการปรับปรุงข้อมูล การนำเข้าการเงินจากธนาคาร<br /><br />");

            foreach (DTO.ImportBankTransferData item in datas)
            {
                emailBody.AppendLine(String.Format("- ปรับปรุง เลขที่ใบสั่งจ่าย จาก {0} เป็น {1} <br />", item.Ref1, item.ChangeRef1));
            }

            if(personalAdmins.Count() > 0)
            {
                foreach (AG_IAS_PERSONAL_T item in personalAdmins)
	            {
                    if (!String.IsNullOrEmpty(item.EMAIL))
                        EmailSender.Sending(emailBody, item.EMAIL, "ปรับปรุงข้อมูลนำเข้าการเงิน");
                        //EmailServiceFactory.GetEmailService().SendMail(emailOut, item.EMAIL, "ปรับปรุงข้อมูลนำเข้าการเงิน", emailBody.ToString());
	            }
            }
  
        } 
    }
}