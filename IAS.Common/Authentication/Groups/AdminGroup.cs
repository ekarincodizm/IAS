using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication.Groups
{
    public class AdminGroup : MemberGroup
    {
        public override MemberLevel Level
        {
            get { return MemberLevel.Admin; }
        }

        public override IMemberProfile Profile
        {
            get { return AdminProfile.CurrentUser; }
        }
    }
}
