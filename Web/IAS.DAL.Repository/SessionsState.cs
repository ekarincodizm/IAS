using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace IAS.DTO
{
    [Serializable]
    public class SessionsState
    {
        public static string ISessionID { get; set; }
        public static string IUserName { get; set; }
        public static string ILoginStatus { get; set; }
        public static int IMemType { get; set; }

        #region ISerializable Members
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ISessionID = (string)info.GetValue("ISessionID", typeof(string));
            IUserName = (string)info.GetValue("IUserName", typeof(string));
            ILoginStatus = (string)info.GetValue("ILoginStatus", typeof(string));
            IMemType = (int)info.GetValue("IMemType", typeof(int));
            //return;
        }

        public void SetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ISessionID", ISessionID);
            info.AddValue("IUserName", IUserName);
            info.AddValue("ILoginStatus", ILoginStatus);
            info.AddValue("IMemType", IMemType);
        }

        //public void GetObjectData(string sid, string uname, string logins, int memty)
        //{
        //    ISessionID = sid;
        //    IUserName = uname;
        //    ILoginStatus = logins;
        //    IMemType = memty;

        //    //return;
        //}

        //public void SetObjectData(string sid, string uname, string logins, int memty)
        //{
        //    sid = ISessionID;
        //    uname = IUserName;
        //    logins = ILoginStatus;
        //    memty = IMemType;
        //}
        #endregion
    }
}
