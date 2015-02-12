using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication.Groups
{
    public abstract class MemberGroup : IMemberGroup
    {
        public virtual Int32 Id { get; set; }
        public virtual String Name { get; set; }
        public virtual String LoweredName { get { return Name.ToLower(); } }
        public virtual String Description { get; set; } 
        public abstract MemberLevel Level { get; }
        public abstract IMemberProfile Profile { get; }
    }
}
