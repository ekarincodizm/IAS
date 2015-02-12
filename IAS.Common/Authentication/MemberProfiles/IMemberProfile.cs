using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Web.Profile;
using IAS.Common.Authentication.Groups;

namespace IAS.Common.Authentication.MemberProfiles
{
    public interface IMemberProfile
    {
        #region Interface ProfileBase
        bool IsAnonymous { get; }
        bool IsDirty { get; }
        DateTime LastActivityDate { get; }
        DateTime LastUpdatedDate { get; }
        //SettingsPropertyCollection Properties { get; }
        string UserName { get; }
        object this[string propertyName] { get; set; }
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        ProfileGroupBase GetProfileGroup(string groupName);
        object GetPropertyValue(string propertyName);
        void Initialize(string username, bool isAuthenticated);
        void Save();
        void SetPropertyValue(string propertyName, object propertyValue);
        #endregion


        #region Properties Of Profile

        Guid ProfileId { get; set; }
        String FirstName { get; set; }
        String LastName { get; set; }
        String Email { get; set; }
        ProfileAddressList ProfileAddresses { get; set; }
        ProfilePhoneList ProfilePhones { get; set; }
        //ProfileProperties ProfileProperties { get; set; }

        IMemberGroup Group { get;}

        #endregion

        #region Methods

        void Setting(ProfileProperties properties);

        #endregion

    }
}
