using System;
using System.Collections.Generic;
using System.Linq;
using System.DirectoryServices;
using IAS.Common.Authentication.MemberProfiles;

namespace IAS.Common.Authentication
{
    public class ActiveDirectoryAuthentication : ILocalAuthenticationService, IDisposable
    {
        private DirectoryEntry directory;
        private DirectorySearcher dirSearcher;
        private String _adPath;

        public void Dispose()
        {
            if (directory != null)
            {
                directory.Dispose();
                directory = null;
            }
            if (dirSearcher != null)
            {
                dirSearcher.Dispose();
                dirSearcher = null;
            }
        }
        public ActiveDirectoryAuthentication(string adPath)
        {
            _adPath = adPath;
        }

        public void SetFilter(string userName)
        {
            
        }

        public SearchResult searchResult { get; set; }

        public User Login(string username, string password)
        {
            User user = new User() { IsAuthenticated = false };

            this.directory = new DirectoryEntry(_adPath, username, password);
            this.dirSearcher = new DirectorySearcher(this.directory);
            this.dirSearcher.Filter = String.Format("(sAMAccountName={0})", username);
            this.searchResult = dirSearcher.FindOne();

            return user;
        }




        public User RegisterUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }


        public User RegisterUser(string username,  string password, ProfileProperties profileProperties)
        {
            throw new NotImplementedException();
        }
    }
}
