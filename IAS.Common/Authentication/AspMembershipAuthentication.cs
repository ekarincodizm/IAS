using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.Security;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication
{
    public class AspMembershipAuthentication : ILocalAuthenticationService 
    {
        public User Login(string username, string password)
        {
            User user = new User() { IsAuthenticated = false };
         
            if (Membership.ValidateUser(username, password))
            {
                MembershipUser validatedUser = Membership.GetUser(username);
            
                user.AuthenticationToken = validatedUser.ProviderUserKey.ToString();
                user.UserName = username;
                user.IsAuthenticated = true;
                //ProfileBase.Create(user.UserName, user.IsAuthenticated);
            }
                  
            return user;
        }

        private string RoleName(int position)
        {
            switch (position)
            {
                case 0: return "root";
                case 1: return "Admin";
                case 2: return "Checker";
                case 3: return "Picker";
                default: return "Publish";
            }
        }
        public void SetUserToRole(string username, String memberGroup)
        {
            try
            {
                String[] roles = Roles.GetRolesForUser(username);
                if (roles.Count() > 0)
                    Roles.RemoveUserFromRoles(username, roles);

                Roles.AddUserToRole(username, memberGroup);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
         

        public User RegisterUser(string username, string email, string password)
        {            
            MembershipCreateStatus status;
            User user = new User();
            user.IsAuthenticated = false;

            Membership.CreateUser(username, password, email, 
                                  Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                                  true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                MembershipUser newlyCreatedUser = Membership.GetUser(username);
                user.AuthenticationToken = newlyCreatedUser.ProviderUserKey.ToString();
                user.Email = email;
                user.UserName = username;
                user.IsAuthenticated = true; 
            }
            else
            {
                switch (status)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                        throw new InvalidOperationException(
                               "There is already a user with this email address.");
                    case MembershipCreateStatus.DuplicateUserName:
                        throw new InvalidOperationException(
                               "There is already a user with this email address.");
                    case MembershipCreateStatus.InvalidEmail:
                        throw new InvalidOperationException(
                               "Your email address is invalid");
                    default:
                        throw new InvalidOperationException(
                        "There was a problem creating your account. Please try again.");
                }
            }

            return user;
        }

        public User RegisterUser(string username,  string password, ProfileProperties profileProperties)
        {
            MembershipCreateStatus status;
            User user = new User();
            user.IsAuthenticated = false;

            Membership.CreateUser(username, password, profileProperties.Email,
                                  Guid.NewGuid().ToString(), Guid.NewGuid().ToString(),
                                  true, out status);

            if (status == MembershipCreateStatus.Success)
            {
                MembershipUser newlyCreatedUser = Membership.GetUser(username);
                user.AuthenticationToken = newlyCreatedUser.ProviderUserKey.ToString();
                user.Email = profileProperties.Email;
                user.UserName = username;
                user.IsAuthenticated = true;

                // User to Role
                Roles.AddUserToRole(username, profileProperties.MemberGroupId.ToString());

                // Add Profile Data 
                IMemberProfile adminProfile = new AdminProfile(ProfileBase.Create(user.UserName));
                adminProfile.Setting(profileProperties);




            }
            else
            {
                switch (status)
                {
                    //case MembershipCreateStatus.DuplicateEmail:
                    //    throw new InvalidOperationException(
                    //           "There is already a user with this email address.");
                    //case MembershipCreateStatus.DuplicateUserName:
                    //    throw new InvalidOperationException(
                    //           "There is already a user with this email address.");
                    //case MembershipCreateStatus.InvalidEmail:
                    //    throw new InvalidOperationException(
                    //           "Your email address is invalid");
                    //default:
                    //    throw new InvalidOperationException(
                    //    "There was a problem creating your account. Please try again.");

                    case MembershipCreateStatus.DuplicateUserName:
                        throw new InvalidOperationException( "User name already exists. Please enter a different user name.");

                    case MembershipCreateStatus.DuplicateEmail:
                        throw new InvalidOperationException( "A user name for that e-mail address already exists. Please enter a different e-mail address.");

                    case MembershipCreateStatus.InvalidPassword:
                        throw new InvalidOperationException( "The password provided is invalid. Please enter a valid password value.");

                    case MembershipCreateStatus.InvalidEmail:
                        throw new InvalidOperationException( "The e-mail address provided is invalid. Please check the value and try again.");

                    case MembershipCreateStatus.InvalidAnswer:
                        throw new InvalidOperationException( "The password retrieval answer provided is invalid. Please check the value and try again.");

                    case MembershipCreateStatus.InvalidQuestion:
                        throw new InvalidOperationException( "The password retrieval question provided is invalid. Please check the value and try again.");

                    case MembershipCreateStatus.InvalidUserName:
                        throw new InvalidOperationException( "The user name provided is invalid. Please check the value and try again.");

                    case MembershipCreateStatus.ProviderError:
                        throw new InvalidOperationException( "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.");

                    case MembershipCreateStatus.UserRejected:
                        throw new InvalidOperationException( "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.");

                    default:
                        throw new InvalidOperationException( "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.");
                }
            }

            return user;
        }


        public void UpdateProfile(string username, ProfileProperties profileProperties) {
            MembershipUser memberUser = Membership.GetUser(username);
            if (memberUser == null)
                throw new AggregateException("There is not found username in member.");
  
            IMemberProfile adminProfile = new AdminProfile(ProfileBase.Create(username));
            adminProfile.Setting(profileProperties);
        }

        public User ChangePassword(string username, String oldPassword, String newPassword, String confirmPassword) 
        {
            User user = new User() { IsAuthenticated = false };

            if (Membership.ValidateUser(username, oldPassword))
            {
                MembershipUser validatedUser = Membership.GetUser(username);

                validatedUser.ChangePassword(oldPassword, newPassword);

                user.AuthenticationToken = validatedUser.ProviderUserKey.ToString();
                user.UserName = username;
                user.IsAuthenticated = true;
                //ProfileBase.Create(user.UserName, user.IsAuthenticated);
            }

            return user;
        }
    }

}
