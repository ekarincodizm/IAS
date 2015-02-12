using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class ApploveDocumnetTypeBiz
    {
        AccountService.AccountServiceClient svc;
        public ApploveDocumnetTypeBiz()
        {
            svc = new AccountService.AccountServiceClient();
        }

        public DTO.ResponseService<DTO.ApploveDocumnetType[]> SelectApploveDocumentType(string p)
        {
            return svc.SelectApploveDocumentType(p);
        }

        public DTO.ResponseService<DTO.ASSOCIATION[]> SelectAsso(string p)
        {
            return svc.SelectAsso(p);
        }

        public DTO.ResponseService<string> AddAssocitionApplove(List<DTO.ASSOCIATION_APPROVE> listadd, List<DTO.ASSOCIATION_APPROVE> listdelete, string by_user)
        {
            return svc.AddAssocitionApplove(listadd.ToArray(), listdelete.ToArray(), by_user);
        }

        public DTO.ResponseService<DTO.ASSOCIATION_APPROVE[]> SelectAssoApplove(string p)
        {
            return svc.SelectAssoApplove(p);
        }

        public DTO.ResponseService<string> UpdateApploveDoctype(List<DTO.ApploveDocumnetType> listDoc, string by_user)
        {
            return svc.UpdateApploveDoctype(listDoc.ToArray(), by_user);
        }
    }
}
