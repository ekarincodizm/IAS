using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication.Groups
{
    public class RootMember : MemberGroup
    {
        public override MemberLevel Level
        {
            get { return MemberLevel.Root; }
        }

        public override IMemberProfile Profile
        {
            get { return RootProfile.CurrentUser; ; }
        }
    }
}
