using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace IAS.DataServices.Account
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountService" in both code and config file together.
    [ServiceContract]
    public interface IAccountService
    {
        [OperationContract]
        DTO.ResponseService<DataSet> GetAccountDetail(string member_type, string user_name, string id_card, string email, int num_page, int RowPerPage, Boolean Count);

        [OperationContract]
        DTO.ResponseService<DTO.AccountDetail> GetAccountDetailById(string id);

        [OperationContract]
        DTO.ResponseMessage<bool> EditMemberTypeAndActive(DTO.AccountDetail ent, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> IsChangePassword(DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> ChangePassword(DTO.User user, string newPassword);

        [OperationContract]
        DTO.ResponseMessage<bool> DisableUser(DTO.AccountDetail user, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseMessage<bool> ChangePasswordByAdmin(DTO.User user, DTO.UserProfile userProfile);

        [OperationContract]
        DTO.ResponseService<List<DTO.ApploveDocumnetType>> SelectApploveDocumentType(string p);

        [OperationContract]
        DTO.ResponseService<List<DTO.ASSOCIATION>> SelectAsso(string p);

        [OperationContract]
        DTO.ResponseService<List<DTO.ASSOCIATION_APPROVE>> SelectAssoApplove(string p);

        [OperationContract]
        DTO.ResponseService<string> AddAssocitionApplove(List<DTO.ASSOCIATION_APPROVE> listadd, List<DTO.ASSOCIATION_APPROVE> listdelete, string by_user);

        [OperationContract]
        DTO.ResponseService<string> UpdateApploveDoctype(List<DTO.ApploveDocumnetType> listDoc, string by_user);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetAssociationNameById(string Id);

        [OperationContract]
        DTO.ResponseService<DTO.DataItem> GetCompanyNameById(string Id);
    }
}
