using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IAS.BLL
{
    public class ConfigBiz
    {
        LicenseService.LicenseServiceClient svc;
        public ConfigBiz()
        {
            svc = new LicenseService.LicenseServiceClient();
        }

        public DTO.ResponseService<DataSet> GetLincse0304(string lincense)
        {
            return svc.GetLincse0304(lincense);
        }

        public DTO.ResponseService<string> AddLincse0304(Dictionary<string, string> lincense)
        {
            return svc.AddLincse0304(lincense);
        }
    }
}
