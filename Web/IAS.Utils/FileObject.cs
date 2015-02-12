using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;
using System.Web;

namespace IAS.Utils
{
    public static class FileObject
    {

        /// <summary>
        /// Convert File to ByteArray
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>byte array</returns>
        public static byte[] ToByteArray(this string filename)
        {
            byte[] byteArray = null;
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                byteArray = new byte[fs.Length];
                fs.Read(byteArray, 0, System.Convert.ToInt32(fs.Length));
            }
            return byteArray;
        }


        /// <summary>
        /// Convert Byte Array to File
        /// </summary>
        /// <param name="byteArray"></param>
        /// <param name="fileName"></param>
        /// <returns>bool</returns>
        public static bool ToFile(this byte[] byteArray, string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        /// <summary>
        /// Convert Byte Array to Memory Stream
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns>object</returns>
        public static object ToMemoryStream(this byte[] byteArray)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArray);
                BinaryFormatter bf = new BinaryFormatter();
                ms.Position = 0;
                return bf.Deserialize(ms);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// สำหรับดึงค่า Hash (SHA1)
        /// </summary>
        /// <param name="filePath">ไฟล์ที่ต้องการตรวจสอบ</param>
        /// <returns>Hash Value</returns>
        public static string GetHashSHA1(string filePath)
        {
            string hashValue = string.Empty;
            try
            {
                SHA1 ha = SHA1.Create();
                FileStream fs = new FileStream(filePath, FileMode.Open);

                byte[] hash = ha.ComputeHash(fs);
                fs.Close();
                hashValue = BitConverter.ToString(hash);
            }
            catch (Exception ex)
            {

                String error = ex.Message;
            }
            return hashValue;
        }

        public static string GetHashSHA2(string filePath)
        {
            string hashValue = string.Empty;
            try
            {
                // Create a file
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("WINDOWS-874"));
                    // Write data to the file
                    //newFile.Write(Buffer, 0, Buffer.Length);
                    HashAlgorithm ha = HashAlgorithm.Create();
                    //FileStream fs = new FileStream(filePath, FileMode.Open);

                    byte[] hash = ha.ComputeHash(fs);
                    fs.Close();
                    hashValue = BitConverter.ToString(hash);
                }

            }
            catch (Exception ex)
            {
                String error = ex.Message;
            }
            return hashValue;
        }
    }
}
