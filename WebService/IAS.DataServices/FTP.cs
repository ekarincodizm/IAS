using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace IAS.DataServices
{
    public class FTP
    {
        private string host = null;
        private string user = null;
        private string pass = null;
        private FtpWebRequest ftpRequest = null;
        private FtpWebResponse ftpResponse = null;
        private Stream ftpStream = null;

        public FTP(string HostIp, string Username, string Password)
        {
            host = HostIp;
            user = Username;
            pass = Password;
        }

        /* Upload File */
        public void Upload(Stream localFile, string remoteFile)
        {
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpStream = ftpRequest.GetRequestStream();

                try
                {
                    int length = (int)localFile.Length;
                    byte[] buffer = new byte[length];
                    int count;
                    int sum = 0;
                    while ((count = localFile.Read(buffer, sum, length - sum)) > 0)
                        sum += count;
                    ftpStream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateDirectory(string newDirectory)
        {
            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (WebException we)
            {
                throw new WebException(we.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CreateDirectoryByRemoteFile(string remoteFile)
        {
            try
            {
                string newDir = Path.GetDirectoryName(remoteFile);
                string[] splitDir = newDir.Replace(@"\", "/").Split("/".ToCharArray());
                string tempDir = "";
                for (int i = 0; i < splitDir.Length; i++)
                {
                    try
                    {
                        tempDir += splitDir[i]+"/";
                        CreateDirectory(tempDir);
                    }
                    catch (WebException) { }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CheckFTPConnection()
        {

            Uri siteUri = new Uri(host);
            ftpRequest = (FtpWebRequest)WebRequest.Create(siteUri);
            ftpRequest.Credentials = new NetworkCredential(user, pass);
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            ftpRequest.UsePassive = true;
            ftpRequest.UseBinary = true;
            ftpRequest.KeepAlive = false;
            try
            {
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpRequest = null;
                ftpResponse.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
