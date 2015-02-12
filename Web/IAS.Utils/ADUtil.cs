using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Web;
using System.Collections;

namespace IAS.Utils
{
    public class ADUtil
    {
        public ADUtil(string adPath, string userName, string passWord)
        {
            this.directory = new DirectoryEntry(adPath,userName,passWord);
            this.dirSearcher = new DirectorySearcher(this.directory);
            SetFilter(userName);
        }

      
        private DirectoryEntry directory { get; set; }

        private DirectorySearcher dirSearcher { get; set; }

        public SearchResult searchResult { get; set; }

        public void SetFilter(string userName)
        {
            this.dirSearcher.Filter = "(sAMAccountName=" + userName + ")";
            this.searchResult = dirSearcher.FindOne();
        }

        public Dictionary<string,string> FindUserByProperties(string[] param)
        {
            var dic = new Dictionary<string,string>();
            for(int i=0; i<param.Length; i++)
            {
                dic.Add(param[i],"");
                this.dirSearcher.PropertiesToLoad.Add(param[i]);
            }
            SearchResult result = this.dirSearcher.FindOne();

            if(result!=null)
            {
                for (int i = 0; i < param.Length; i++)
                {
                    var property =  result.Properties[param[i]];
                    dic[param[i]] =  property.Count > 0 ? property[0].ToString() : string.Empty;
                }
            }

            return dic;
        }


    }
}
