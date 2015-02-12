using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.License
{
    public static class AttachFileLicenseMapper
    {
        public static List<DTO.AttatchFileLicense> ConvertToAttachFileLicense(this List<DAL.AG_IAS_ATTACH_FILE_LICENSE> attachFiles)
        {
            List<DTO.AttatchFileLicense> list = new List<DTO.AttatchFileLicense>();
            foreach (DAL.AG_IAS_ATTACH_FILE_LICENSE item in attachFiles)
            {
                list.Add(new DTO.AttatchFileLicense()
                {
                    ID_ATTACH_FILE = item.ID_ATTACH_FILE,
                    ID_CARD_NO = item.ID_CARD_NO,
                    ATTACH_FILE_TYPE = item.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = item.ATTACH_FILE_PATH,
                    REMARK = item.REMARK,
                    CREATED_BY = item.CREATED_BY,
                    CREATED_DATE = (DateTime)item.CREATED_DATE,
                    UPDATED_BY = item.UPDATED_BY,
                    UPDATED_DATE = (DateTime)item.UPDATED_DATE,
                    FILE_STATUS = item.FILE_STATUS,
                    LICENSE_NO = item.LICENSE_NO,
                    RENEW_TIME = item.RENEW_TIME,
                    GROUP_LICENSE_ID = item.GROUP_LICENSE_ID
                });
            }

            return list;
        }
    }
}