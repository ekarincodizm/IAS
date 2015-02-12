using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS.States
{
    public abstract class RegistrationState : IRegistrationState
    {
        public int Id { get; set; }

        public abstract RegistrationStatus Status { get; }

        public abstract bool CanAddAttachFile();

        public abstract void Approve(BaseRegistration registration);
        public abstract void Disapprove(BaseRegistration registration);
        public abstract void Cancel(BaseRegistration registration);

        public abstract void Submit(BaseRegistration registration);
    }
}
