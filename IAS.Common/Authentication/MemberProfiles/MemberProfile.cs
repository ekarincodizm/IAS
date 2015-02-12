using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Web.Profile;
using System.Web.Security;
using IAS.Common.Authentication.Groups;

namespace IAS.Common.Authentication.MemberProfiles
{
    public abstract class MemberProfile :  IMemberProfile
    {
        private static ProfileBase _profileBase;

        public MemberProfile(ProfileBase profileBase)
        {
            _profileBase = profileBase; 
        }

        public virtual Guid ProfileId
        {
            get { return ((Guid)(_profileBase["ProfileId"])); }
            set { _profileBase["ProfileId"] = value; }
        }

        public virtual string FirstName
        {
            get { return ((string)(_profileBase["FirstName"])); }
            set { _profileBase["FirstName"] = value; }
        }

        public virtual String LastName
        {
            get { return ((String)(_profileBase["LastName"])); }
            set { _profileBase["LastName"] = value; }
        }

        public virtual String Email
        {
            get { return ((String)(_profileBase["Email"])); }
            set { _profileBase["Email"] = value; }
        }

        public ProfileAddressList ProfileAddresses
        {
            get { return ((ProfileAddressList)(_profileBase["ProfileAddresses"])); }
            set { _profileBase["ProfileAddresses"] = value; }
        }

        public ProfilePhoneList ProfilePhones
        {
            get { return ((ProfilePhoneList)(_profileBase["ProfilePhones"])); }
            set { _profileBase["ProfilePhones"] = value; }
        }

        //public ProfileProperties ProfileProperties
        //{
        //    get { return ((ProfileProperties)(_profileBase["ProfileProperties"])); }
        //    set { _profileBase["ProfileProperties"] = value; }
        //}


        public abstract void Setting(ProfileProperties properties);


        public abstract IMemberGroup Group { get; }


        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static IMemberProfile Create(string username)
        {
            return new AdminProfile(ProfileBase.Create(username));
        }

        public static IMemberProfile Create(string username, bool isAuthenticated)
        {
            return new AdminProfile(ProfileBase.Create(username, isAuthenticated));
        }


        public bool IsAnonymous
        {
            get { return _profileBase.IsAnonymous; }
        }

        public bool IsDirty
        {
            get { return _profileBase.IsDirty; }
        }

        public DateTime LastActivityDate
        {
            get { return _profileBase.LastActivityDate; }
        }

        public DateTime LastUpdatedDate
        {
            get { return _profileBase.LastUpdatedDate; }
        }

        public static SettingsPropertyCollection Properties
        {
            get { return ProfileBase.Properties;  }
        }

        public string UserName
        {
            get { throw new NotImplementedException(); }
        }

        public object this[string propertyName]
        {
            get
            {
               return _profileBase[propertyName];
            }
            set
            {
                _profileBase[propertyName] = value;
            }
        }

        public ProfileGroupBase GetProfileGroup(string groupName)
        {
            return _profileBase.GetProfileGroup(groupName);
        }

        public object GetPropertyValue(string propertyName)
        {
            return _profileBase.GetPropertyValue(propertyName);
        }

        public void Initialize(string username, bool isAuthenticated)
        {
            _profileBase.Initialize(username, isAuthenticated);
        }

        public void Save()
        {
            _profileBase.Save();
        }

        public void SetPropertyValue(string propertyName, object propertyValue)
        {
            _profileBase.SetPropertyValue(propertyName, propertyValue);
        }





    }
}
