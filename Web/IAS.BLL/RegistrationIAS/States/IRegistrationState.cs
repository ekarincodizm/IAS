using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS.States
{
    public interface IRegistrationState
    {
        int Id { get; set; }
        RegistrationStatus Status { get; }
        Boolean CanAddAttachFile();
        void Approve(BaseRegistration registration);
        void Disapprove(BaseRegistration registration);
        void Cancel(BaseRegistration registration);

        void Submit(BaseRegistration registration);

    }
}
