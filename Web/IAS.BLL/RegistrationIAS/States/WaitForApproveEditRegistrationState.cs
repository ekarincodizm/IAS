using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS.States
{
    public class WaitForApproveEditRegistrationState : RegistrationState
    {
        public override RegistrationStatus Status
        {
            get { return States.RegistrationStatus.WaitForApproveEdit; }
        }

        public override bool CanAddAttachFile()
        {
            return false;
        }

        public override void Approve(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.ApproveEdit);
        }

        public override void Disapprove(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.DisapproveEdit);
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
