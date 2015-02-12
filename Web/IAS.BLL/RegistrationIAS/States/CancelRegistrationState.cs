using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.Properties;

namespace IAS.BLL.RegistrationIAS.States
{
    public class CancelRegistrationState : RegistrationState
    {
        public override RegistrationStatus Status
        {
            get { return RegistrationStatus.Cancel; }
        }

        public override bool CanAddAttachFile()
        {
            return false;
        }

        public override void Approve(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.CancelRegistrationState);
        }

        public override void Disapprove(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.CancelRegistrationState);
        }

        public override void Cancel(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.CancelRegistrationState);
        }

        public override void Submit(BaseRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}
