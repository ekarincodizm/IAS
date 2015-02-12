using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using IAS.Common.Authentication.Groups;

namespace IAS.Common.Authentication.MemberProfiles
{
    public class RootProfile : MemberProfile, IMemberProfile
    {
        static IMemberProfile rootProfile;
        static public IMemberProfile CurrentUser
        {
            get
            {
                if (rootProfile == null)
                    rootProfile = new RootProfile(ProfileBase.Create(HttpContext.Current.User.Identity.Name));

                return CurrentUser;
            }
        }

        public RootProfile(ProfileBase profileBase)
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
            get { return MemberGroups.Root; }
        }



    }
}
