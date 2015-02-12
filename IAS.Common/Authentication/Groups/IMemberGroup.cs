using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication.Groups
{
    public interface IMemberGroup
    {
        Int32 Id { get; set; }
        MemberLevel Level { get; }
        String Name { get; set; }
        String LoweredName { get; }
        String Description { get; set; }
        IMemberProfile Profile { get;}

    }
}
