using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS.States
{
    public enum RegistrationStatus
    {
        NewRegisteration=0,
        WaitForApprove=1,
        Approve=2,
        Disapprove=3,
        WaitForApproveEdit=4,
        ApproveEdit=5,
        DisapproveEdit=6,
        Cancel=7
    }
}
