using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Authentication.Groups
{
    public class MemberGroups
    {
        public static readonly IMemberGroup Root = new RootMember() { Id = 1 };

        public static readonly IMemberGroup Admin = new AdminGroup() { Id = 2 };

        public static readonly IMemberGroup General = new GeneralGroup() { Id = 3 };
    }
}
