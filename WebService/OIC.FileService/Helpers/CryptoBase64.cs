using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.FileService.Helpers
{
    public class CryptoBase64
    {
        public static string Encryption(string plaintext)
        {
            string result = string.Empty;
            try
            {
                result = Convert.ToBase64String(Encoding.UTF8.GetBytes(plaintext));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string Decryption(string plaintext)
        {
            string result = string.Empty;
            result = Encoding.UTF8.GetString(Convert.FromBase64String(plaintext));
            return result;
        }
    }
}
