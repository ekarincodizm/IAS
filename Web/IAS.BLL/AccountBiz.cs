using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DTO;
using System.Data;

namespace IAS.BLL
{
    public class AccountBiz : IDisposable
    {
        private AccountService.AccountServiceClient svc;

        public AccountBiz()
        {
            svc = new AccountService.AccountServiceClient();
        }

        public void Dispose()
        {
            if (svc != null) svc.Close();
            GC.SuppressFinalize(this);
        }

        public DTO.ResponseService<DataSet> GetAccountDetail(string member_type, string user_name, string id_card, string email, int num_page, int RowPerPage, Boolean Count)
        {
            return svc.GetAccountDetail(member_type, user_name, id_card, email, num_page, RowPerPage, Count);
        }

        public DTO.ResponseService<DTO.AccountDetail> GetAccountDetailById(string id)
        {
            return svc.GetAccountDetailById(id);
        }

        public DTO.ResponseMessage<Boolean> EditMemberTypeAndActive(DTO.AccountDetail ent, DTO.UserProfile userProfile)
        {
            return svc.EditMemberTypeAndActive(ent, userProfile);
        }

        public DTO.ResponseMessage<Boolean> IsChangePassword(DTO.UserProfile userProfile)
        {
            return svc.IsChangePassword(userProfile);
        }

        public DTO.ResponseMessage<Boolean> ChangePassword(DTO.User user, string newPassword)
        {
            return svc.ChangePassword(user, newPassword);
        }

        public DTO.ResponseMessage<bool> DisableUser(DTO.AccountDetail user, DTO.UserProfile userProfile)
        {
            return svc.DisableUser(user, userProfile);
        }

        public DTO.ResponseMessage<bool> ChangePasswordByAdmin(DTO.User user, DTO.UserProfile userProfile)
        {
            return svc.ChangePasswordByAdmin(user, userProfile);
        }

        public string GetAssociationNameById(string Id)
        {
            var res = svc.GetAssociationNameById(Id);
            return res.DataResponse != null ? res.DataResponse.Name : "";
        }

        public string GetCompanyNameById(string Id)
        {
            var res = svc.GetCompanyNameById(Id);
            return res.DataResponse != null ? res.DataResponse.Name : "";
        }

    }
}
