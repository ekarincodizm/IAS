using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.Properties;

namespace IAS.BLL.RegistrationIAS.States
{
    public class NewRegistrationState : RegistrationState
    {
        public override RegistrationStatus Status
        {
            get { return RegistrationStatus.NewRegisteration; }
        }

        public override bool CanAddAttachFile()
        {
            return true;
        }

        public override void Approve(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.errorNewRegistrationState_001); 
        }

        public override void Disapprove(BaseRegistration registration)
        {
            throw new InvalidOperationException(Resources.errorNewRegistrationState_001); 
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
                brokenRules.AppendLine(Resources.errorNewRegistrationState_002);

                foreach (BusinessRule businessRule in registration.GetBrokenRules())
                {
                    brokenRules.AppendLine(businessRule.Rule);
                }

                throw new ApplicationException(brokenRules.ToString());

            }
            registration.SetStateTo(RegistrationStates.WaitForApprove);

            registration.Save();
        }
    }
}
