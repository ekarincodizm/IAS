using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS.States
{
    public class RegistrationStates
    {
        public static readonly IRegistrationState New =
                    new NewRegistrationState() { Id = 0 };

        public static readonly IRegistrationState WaitForApprove =
                    new WaitForApproveRegistrationState() { Id = 1 };

        public static readonly IRegistrationState Approve =
                    new ApproveRegistrationState() { Id = 2 };

        public static readonly IRegistrationState Disapprove =
                    new DisapproveRegistrationState() { Id = 3 };

        public static readonly IRegistrationState WaitForApproveEdit =
                    new WaitForApproveEditRegistrationState() { Id = 4 };

        public static readonly IRegistrationState ApproveEdit =
                    new ApproveEditRegistrationState() { Id = 5 };

        public static readonly IRegistrationState DisapproveEdit =
                    new DisapproveEditRegistrationState() { Id = 6 };

        public static readonly IRegistrationState Cancel =
                    new CancelRegistrationState() { Id = 7 };
    }
}
