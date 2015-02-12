using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DTO;
using System.Text;
using IAS.Utils;

namespace IAS.DataServices.License.Mapper
{
    public static class LicenseFileHeaderMapper
    {
        public static DTO.SummaryReceiveLicense ConvertToSummaryReceiveLicense(this LicenseFileHeader licenseFileHeader) 
        {
            SummaryReceiveLicense summarize = new SummaryReceiveLicense();
            summarize.Identity = licenseFileHeader.IMPORT_ID.ToString();
            summarize.Header = new DTO.UploadHeader();
            IList<ReceiveLicenseDetail> details = new List<ReceiveLicenseDetail>();


            Int32 errorAmount = licenseFileHeader.LicenseFileDetails.Count(a => a.LOAD_STATUS == "F");
            Int32 passAmount = licenseFileHeader.LicenseFileDetails.Count(a => a.LOAD_STATUS == "T");

            summarize.Header.RightTrans = passAmount;
            summarize.Header.MissingTrans = errorAmount;
            summarize.Header.Totals = licenseFileHeader.LicenseFileDetails.Count();
            summarize.Header.UploadFileName = licenseFileHeader.FILE_NAME;
            summarize.Header.FileName = licenseFileHeader.FileName;

            if (licenseFileHeader.GetBrokenRules().Count() > 0)
            {
                StringBuilder errorMessage = new StringBuilder("");
                foreach (BusinessRule item in licenseFileHeader.GetBrokenRules())
                {
                    errorMessage.AppendLine(item.Rule + "<br />");
                }
                summarize.MessageError = errorMessage.ToString();
                licenseFileHeader.ERR_MSG = errorMessage.ToString();
            }

            licenseFileHeader.ValidCiticenDuplicate();

            foreach (LicenseFileDetail item in licenseFileHeader.LicenseFileDetails)
            {
                ReceiveLicenseDetail detail = new ReceiveLicenseDetail()
                {
                    IMPORT_ID = item.IMPORT_ID,
                    PETITION_TYPE = item.PETITION_TYPE,
                    COMP_CODE = item.COMP_CODE,
                    SEQ = item.SEQ,
                    LICENSE_NO = item.LICENSE_NO,
                    LICENSE_ACTIVE_DATE = item.LICENSE_ACTIVE_DATE,
                    LICENSE_EXPIRE_DATE = item.LICENSE_EXPIRE_DATE,
                    LICENSE_FEE = item.LICENSE_FEE,
                    CITIZEN_ID = item.CITIZEN_ID,
                    TITLE_NAME = item.TITLE_NAME,
                    NAME = item.NAME,
                    SURNAME = item.SURNAME,
                    ADDR1 = item.ADDR1,
                    ADDR2 = item.ADDR2,
                    AREA_CODE = item.AREA_CODE,
                    EMAIL = item.EMAIL,
                    CUR_ADDR = item.CUR_ADDR,
                    TEL_NO = item.TEL_NO,
                    CUR_AREA_CODE = item.CUR_AREA_CODE,
                    REMARK = item.REMARK,
                    AR_ANSWER = item.AR_ANSWER,
                    OLD_COMP_CODE = item.OLD_COMP_CODE,
                    ERR_MSG = item.ERR_MSG,
                    LOAD_STATUS = item.LOAD_STATUS,
                    AttachFileDetails = item.AttachFileDetails,
                    Header = summarize.Header
                };


                details.Add(detail);
            }
            
          
            summarize.ReceiveLicenseDetails = details;

            return summarize; 


        }
    }
}