using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.Properties;

namespace IAS.BLL.RegistrationIAS.States
{
    public class DisapproveRegistrationState : RegistrationState
    {
        public override RegistrationStatus Status
        {
            get { return RegistrationStatus.Disapprove; }
        }

        public override bool CanAddAttachFile()
        {
            return true;
        }

        public override void Approve(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.errorDisapproveRegistrationState_001);
        }

        public override void Disapprove(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.errorDisapproveRegistrationState_001);
        }

        public override void Cancel(BaseRegistration registration)
        {
            registration.SetStateTo(RegistrationStates.Cancel);
        }

        public override void Submit(BaseRegistration registration)
        {
            if (registration.GetBrokenRules().Count() > 0)
            {
                StringBuilder brokenRules = new StringBuilder();
                brokenRules.AppendLine(Resources.infoDisapproveRegistrationState_002);
                foreach (BusinessRule businessRule in registration.GetBrokenRules())
                {
                    brokenRules.AppendLine(businessRule.Rule);
                }

                throw new ApplicationException(brokenRules.ToString());
            }
        }
    }
}
