using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.Properties;

namespace IAS.BLL.RegistrationIAS.States
{
    public class DisapproveEditRegistrationState : RegistrationState
    {
        public override RegistrationStatus Status
        {
            get { return RegistrationStatus.DisapproveEdit; }
        }

        public override bool CanAddAttachFile()
        {
            return true;
        }

        public override void Approve(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.errorDisapproveEditRegistrationState_001);
        }

        public override void Disapprove(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.errorDisapproveEditRegistrationState_001);
        }

        public override void Cancel(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.Cancel);
        }

        public override void Submit(BaseRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}
