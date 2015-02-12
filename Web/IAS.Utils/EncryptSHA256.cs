using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace IAS.Utils
{
    public class EncryptSHA256
    {
        public static string Encrypt(string anyString)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(anyString));
            return Convert.ToBase64String(hashedDataBytes);
        }
    }
}
