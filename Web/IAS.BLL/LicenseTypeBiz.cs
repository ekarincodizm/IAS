using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class LicenseTypeBiz
    {
        private ExamService.ExamServiceClient svc;
        public LicenseTypeBiz()
        {
            svc = new ExamService.ExamServiceClient();
        }
        public DTO.ResponseService<DTO.LicenseTypet[]> GetLicensetypeList(string agenttype)
        {
            return svc.GetLicenseList(agenttype);
        }

        public DTO.ResponseService<DTO.AgentType[]> GetAgentTypeList()
        {
            return svc.GetAgentTypeList();
        }

        public DTO.ResponseService<string> AddLicenseType(DTO.LicenseTypet licensettype)
        {
            return svc.AddLicenseType(licensettype);
        }

        public DTO.ResponseService<string> UpdateLicenseType(DTO.LicenseTypet licensettype)
        {
            return svc.UpdateLicenseType(licensettype);
        }

        public DTO.ResponseService<string> DeleteLicenseType(string licensecode)
        {
            return svc.DeleteLicensetype(licensecode);
        }
    }
}
