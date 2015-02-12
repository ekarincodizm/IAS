using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS.States
{
    public class WaitForApproveRegistrationState : RegistrationState
    {
        public override RegistrationStatus Status
        {
            get { return RegistrationStatus.WaitForApprove; }
        }

        public override bool CanAddAttachFile()
        {
            return false;
        }

        public override void Submit(BaseRegistration registration)
        {
            
        }

        public override void Approve(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.Approve);
        }

        public override void Disapprove(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.Disapprove);
        }

        public override void Cancel(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.Cancel);
        }
    }
}
