using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL
{
    public class UserAuthenBiz : IDisposable
    {
        public UserAuthenBiz()
        {

        }

        public DTO.ResponseService<DTO.UserProfile> Authentication(string userName, string password, bool IsOIC)
        {
            PersonService.PersonServiceClient svc = new PersonService.PersonServiceClient();
            return svc.Authentication(userName, password, IsOIC,"");
        }

        public DTO.ResponseMessage<bool> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            PersonService.PersonServiceClient svc = new PersonService.PersonServiceClient();
            return svc.ChangePassword(userId, oldPassword, newPassword);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
