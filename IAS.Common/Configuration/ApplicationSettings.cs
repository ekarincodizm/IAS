using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace IAS.Common.Configuration
{
    public class ApplicationSettings : IApplicationSettings  
    {
        public String SSOAuth
        {
            get { return ConfigurationManager.AppSettings["SSOAuth"]; } 
        }
        public String LoggerName      
        {
            get { return ConfigurationManager.AppSettings["LoggerName"]; }
        }




        public String APP_FOR_OIC
        {
            get { return ConfigurationManager.AppSettings["APP_FOR_OIC"]; }
        }

        public String FS_TEMP
        {
            get { return ConfigurationManager.AppSettings["FS_TEMP"]; }
        }

        public String FS_ATTACH
        {
            get { return ConfigurationManager.AppSettings["FS_ATTACH"]; }
        }

        public String FS_OIC
        {
            get { return ConfigurationManager.AppSettings["FS_OIC"]; }
        }

        public String FS_RECEIPT
        {
            get { return ConfigurationManager.AppSettings["FS_RECEIPT"]; }
        }

        public String PDFFilePath
        {
            get { return ConfigurationManager.AppSettings["PDFFilePath"]; }
        }

        public String PDFFilePath_Temp
        {
            get { return ConfigurationManager.AppSettings["PDFFilePath_Temp"]; }
        }

        public String PDFFilePath_OIC
        {
            get { return ConfigurationManager.AppSettings["PDFFilePath_OIC"]; }
        }

        public String PDFFilePath_Users
        {
            get { return ConfigurationManager.AppSettings["PDFFilePath_Users"]; }
        }

        public String ReportFilePath
        {
            get { return ConfigurationManager.AppSettings["ReportFilePath"]; }
        }

        public String UploadFilePath
        {
            get { return ConfigurationManager.AppSettings["UploadFilePath"]; }
        }

        public String UploadTempPath
        {
            get { return ConfigurationManager.AppSettings["UploadTempPath"]; }
        }

        public String UploadRecieveLicense
        {
            get { return ConfigurationManager.AppSettings["UploadRecieveLicense"]; }
        }

        public String CrystalImageCleaner_AutoStart
        {
            get { return ConfigurationManager.AppSettings["CrystalImageCleaner_AutoStart"]; }
        }

        public String CrystalImageCleaner_Sleep
        {
            get { return ConfigurationManager.AppSettings["CrystalImageCleaner_Sleep"]; }
        }

        public String CrystalImageCleaner_Age
        {
            get { return ConfigurationManager.AppSettings["CrystalImageCleaner_Age"]; }
        }

        public String RECIPIENTS_MAIL
        {
            get { return ConfigurationManager.AppSettings["RECIPIENTS_MAIL"]; }
        }

        public String EMAIL_CONTENTFILE_CONTACTUS_ADMIN
        {
            get { return ConfigurationManager.AppSettings["EMAIL_CONTENTFILE_CONTACTUS_ADMIN"]; }
        }

        public String EMAIL_SUBJECT_CONTACTUS
        {
            get { return ConfigurationManager.AppSettings["EMAIL_SUBJECT_CONTACTUS"]; }
        }

        public String PAGE_SIZE
        {
            get { return ConfigurationManager.AppSettings["PAGE_SIZE"]; }
        }

        public String EXCEL_SIZE
        {
            get { return ConfigurationManager.AppSettings["EXCEL_SIZE"]; }
        }

        public String AgreementFilePath
        {
            get { return ConfigurationManager.AppSettings["AgreementFilePath"]; }
        }


        public string WebPublicUrl
        {
            get { return ConfigurationManager.AppSettings["WebPublicUrl"]; }
        }

        public string ADPath
        {
            get { return ConfigurationManager.AppSettings["ADPath"]; }
        }

        public string ADUserName
        {
            get { return ConfigurationManager.AppSettings["ADUserName"]; }
        }

        public string ADPassword
        {
            get { return ConfigurationManager.AppSettings["ADPassword"]; }
        }

        public string ADDomain
        {
            get { return ConfigurationManager.AppSettings["ADDomain"]; }
        }

        public string FS_RECEIVE
        {
            get { return ConfigurationManager.AppSettings["FS_RECEIVE"]; }
        }

        public string OIC_SECTION
        {
            get { return ConfigurationManager.AppSettings["OIC_SECTION"]; }
        }

        public string OIC_BRANCH_NO
        {
            get { return ConfigurationManager.AppSettings["OIC_BRANCH_NO"]; }
        }

        public string EmailOut
        {
            get { return ConfigurationManager.AppSettings["EmailOut"]; }
        }

        public string EmailOutPass
        {
            get { return ConfigurationManager.AppSettings["EmailOutPass"]; }
        }

        public string TEMP_FOLDER_ATTACH
        {
            get { return ConfigurationManager.AppSettings["TEMP_FOLDER_ATTACH"]; }
        }

        public string OIC_FOLDER_ATTACH
        {
            get { return ConfigurationManager.AppSettings["OIC_FOLDER_ATTACH"]; }
        }

        public string FOLDER_ATTACH
        {
            get { return ConfigurationManager.AppSettings["FOLDER_ATTACH"]; }
        }

        public string COMPRESS_FOLDER
        {
            get { return ConfigurationManager.AppSettings["COMPRESS_FOLDER"]; }
        }

        public string CODE_ATTACH_PHOTO
        {
            get { return ConfigurationManager.AppSettings["CODE_ATTACH_PHOTO"]; }
        }

        public string DEFAULT_NET_DRIVE
        {
            get { return ConfigurationManager.AppSettings["DEFAULT_NET_DRIVE"]; }
        }

        public string USER_NET_DRIVE
        {
            get { return ConfigurationManager.AppSettings["USER_NET_DRIVE"]; }
        }

        public string PASS_NET_DRIVE
        {
            get { return ConfigurationManager.AppSettings["PASS_NET_DRIVE"]; }
        }

        public string CITYBANK_ACCOUNT
        {
            get { return ConfigurationManager.AppSettings["CITYBANK_ACCOUNT"]; }
        }

        public string CITYBANK_GROUP
        {
            get { return ConfigurationManager.AppSettings["CITYBANK_GROUP"]; }
        }

        public string KTB_ACCOUNT
        {
            get { return ConfigurationManager.AppSettings["KTB_ACCOUNT"]; }
        }

        public string KTB_GROUP
        {
            get { return ConfigurationManager.AppSettings["KTB_GROUP"]; }
        }
    }

}
