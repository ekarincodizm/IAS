using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication.Groups
{
    public class GeneralGroup : MemberGroup
    {
        public override MemberLevel Level
        {
            get { return MemberLevel.General; }
        }

        public override MemberProfiles.IMemberProfile Profile
        {
            get { return GeneralProfile.CurrentUser; }
        }
    }
}
