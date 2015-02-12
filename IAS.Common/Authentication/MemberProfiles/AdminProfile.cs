using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using IAS.Common.Authentication.Groups;

namespace IAS.Common.Authentication.MemberProfiles
{
    public class AdminProfile : MemberProfile, IMemberProfile
    {
            static IMemberProfile adminProfile;
        static public IMemberProfile CurrentUser
        {
            get
            {
                if (adminProfile == null)
                    adminProfile = new RootProfile(ProfileBase.Create(HttpContext.Current.User.Identity.Name));

                return CurrentUser;
            }
        }

        public AdminProfile(ProfileBase profileBase)
            :base(profileBase)
        { }

        public override void Setting(ProfileProperties properties)
        {
            base.ProfileId = properties.ProfileId;
            base.FirstName = properties.FirstName;
            base.LastName = properties.LastName;
            base.Email = properties.Email;
            base.ProfileAddresses = properties.AddressProperties;
            base.ProfilePhones = properties.PhoneProperties;
            Save();
        }

        public override IMemberGroup Group
        {
            get { return MemberGroups.Admin; }
        }

    }
}
