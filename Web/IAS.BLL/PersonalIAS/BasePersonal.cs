using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IAS.DTO.FileService;
using System.Configuration;
using IAS.BLL.Properties;

namespace IAS.BLL.PersonalIAS
{
    public abstract class BasePersonal : EntityBase<String>
    {
        protected String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        protected String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString(); 


        private IList<DTO.PersonAttatchFile> _attachFiles = new List<DTO.PersonAttatchFile>();

        public String ID { get; set; } //	VARCHAR2	(	15	)
        public String MEMBER_TYPE { get; set; } //	VARCHAR2	(	1	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	13	)
        public String EMPLOYEE_NO { get; set; } //	VARCHAR2	(	20	)
        public String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	3	)
        public String NAMES { get; set; } //	VARCHAR2	(	50	)
        public String LASTNAME { get; set; } //	VARCHAR2	(	70	)
        public String NATIONALITY { get; set; } //	VARCHAR2	(	20	)
        public DateTime BIRTH_DATE { get; set; } //	DATE	(	7	)
        public String SEX { get; set; } //	VARCHAR2	(	1	)
        public String EDUCATION_CODE { get; set; } //	VARCHAR2	(	2	)
        public String ADDRESS_1 { get; set; } //	VARCHAR2	(	200	)
        public String ADDRESS_2 { get; set; } //	VARCHAR2	(	200	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String PROVINCE_CODE { get; set; } //	VARCHAR2	(	3	)
        public String ZIP_CODE { get; set; } //	VARCHAR2	(	5	)
        public String TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String LOCAL_ADDRESS1 { get; set; } //	VARCHAR2	(	100	)
        public String LOCAL_ADDRESS2 { get; set; } //	VARCHAR2	(	100	)
        public String LOCAL_AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String LOCAL_PROVINCE_CODE { get; set; } //	VARCHAR2	(	20	)
        public String LOCAL_ZIPCODE { get; set; } //	VARCHAR2	(	5	)
        public String LOCAL_TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String EMAIL { get; set; } //	VARCHAR2	(	255	)
        public String STATUS { get; set; } //	VARCHAR2	(	1	)
        public String TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        public String LOCAL_TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        public String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)

        protected override void Validate()
        {
            ValidateEntity();
        }

        protected abstract void ValidateEntity();

        public IEnumerable<DTO.PersonAttatchFile> AttachFiles
        {
            get { return _attachFiles; }
        }
        public virtual void AddAttach(DTO.PersonAttatchFile attachFile, Stream fileSteam)
        {
            if (_attachFiles.Where(a => a.REGISTRATION_ID == attachFile.REGISTRATION_ID && a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE).Count() > 0)
            {
                throw new PersonalIssueException(Resources.errorBasePersonal_001);
            }
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                UploadFileResponse response = new UploadFileResponse();

                String fs_Attach = String.Format(@"{0}\{1}", AttachFileContainer, attachFile.REGISTRATION_ID);

                String newFileName = this.ID_CARD_NO + "_"
                                                    + Convert.ToInt32(attachFile.ATTACH_FILE_TYPE).ToString("00")
                                                    + "." + GetExtensionFile(attachFile.FileName);
                //response = svc.UploadFile(new UploadFileRequest() {
                //                                TargetContainer = fs_Attach,
                //                                TargetFileName = newFileName,
                //                                FileStream = fileSteam
                //                            });
                if (response.Code != "0000")
                    throw new PersonalIssueException(response.Message);

                attachFile.ATTACH_FILE_PATH = response.TargetFullName;
                attachFile.ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId();
                _attachFiles.Add(attachFile);

            }

        }

        protected String GetExtensionFile(String fileName)
        {
            String[] files = fileName.Split('.');
            return files[files.Length - 1];
        }

        public virtual DTO.PersonAttatchFile GetAttach(string id, string type, ref Stream fileStrem)
        {

            DTO.PersonAttatchFile attachFile = new DTO.PersonAttatchFile();
            if (_attachFiles.Where(a => a.ATTACH_FILE_TYPE == type).Count() <= 0)
                throw new PersonalIssueException(Resources.errorBaseRegistration_004);

            attachFile = _attachFiles.Where(a => a.ATTACH_FILE_TYPE == type).FirstOrDefault();
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                //DownloadFileResponse response = svc.DownloadFile(new DownloadFileRequest() { 
                //                                                    TargetContainer = "", 
                //                                                    TargetFileName = attachFile.ATTACH_FILE_PATH });
                //if (response.Code != "0000")
                //    throw new PersonalIssueException(response.Message);

                //fileStrem = response.FileByteStream;
            }


            return attachFile;
        }
        public virtual void DeleteAttach(DTO.PersonAttatchFile attachFile)
        {
            if (_attachFiles.Where(a => a.ID == attachFile.ID
                            && a.REGISTRATION_ID == attachFile.REGISTRATION_ID
                            && a.ATTACH_FILE_TYPE == attachFile.ATTACH_FILE_TYPE).Count() <= 0)
                throw new PersonalIssueException(Resources.errorBaseRegistration_004);

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                //DeleteFileResponse response = svc.DeleteFile(new DeleteFileRequest()
                //{
                //    TargetFileName = attachFile.ATTACH_FILE_PATH
                //});
                //if (response.Code != "0000")
                //    throw new PersonalIssueException(response.Message);

                _attachFiles.Remove(attachFile);
            }


        }
    }
}
