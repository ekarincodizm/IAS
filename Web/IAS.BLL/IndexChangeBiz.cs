using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IAS.BLL
{
    public class IndexChangeBiz : IDisposable
    {
        private LicenseService.LicenseServiceClient svc;
        private static String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();
        private static String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        public IndexChangeBiz()
        {
            svc = new LicenseService.LicenseServiceClient();
        }

        public DTO.ResponseService<DTO.SubPaymentHead[]> GetIndexSubPaymentH(string groupReqNo)
        {
            return svc.GetIndexSubPaymentH(groupReqNo);
        }

        public DTO.ResponseService<DTO.SubPaymentDetail[]> GetIndexSubPaymentD(string headReqNo)
        {
            return svc.GetIndexSubPaymentD(headReqNo);
        }

        public DTO.ResponseService<DTO.PersonLicenseDetail[]> GetIndexLicenseD(string uploadGroupNo)
        {
            return svc.GetIndexLicenseD(uploadGroupNo);
        }

        public void Dispose()
        {
            svc = null;
        }
    }
}
