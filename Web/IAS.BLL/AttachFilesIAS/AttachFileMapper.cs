using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IAS.BLL.AttachFilesIAS
{
    public static class AttachFileMapper
    {
        public static IList<DTO.RegistrationAttatchFile> ConvertToRegistrationAttachFiles(this IEnumerable<AttachFile> attachFiles) {
            IList<DTO.RegistrationAttatchFile> regAttachFiles = new List<DTO.RegistrationAttatchFile>();
            foreach (AttachFile attachFile in attachFiles)
            {
                regAttachFiles.Add(new DTO.RegistrationAttatchFile() {
                    ID = attachFile.ID,
                    REGISTRATION_ID = attachFile.REGISTRATION_ID,
                    ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                    REMARK = attachFile.REMARK,
                    CREATED_BY = attachFile.CREATED_BY,
                    CREATED_DATE = attachFile.CREATED_DATE,
                    UPDATED_BY = attachFile.UPDATED_BY,
                    UPDATED_DATE = attachFile.UPDATED_DATE,
                    FILE_STATUS = attachFile.FILE_STATUS,
                    REQUEST_STATUS = attachFile.REQUEST_STATUS

                });
            }

            return regAttachFiles;
        }
        public static IList<DTO.PersonAttatchFile> ConvertToPersonAttatchFiles(this IEnumerable<AttachFile> attachFiles) 
        {
            IList<DTO.PersonAttatchFile> regAttachFiles = new List<DTO.PersonAttatchFile>();
            foreach (AttachFile attachFile in attachFiles)
            {
                regAttachFiles.Add(new DTO.PersonAttatchFile()
                {
                    ID = attachFile.ID,
                    REGISTRATION_ID = attachFile.REGISTRATION_ID,
                    ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                    REMARK = attachFile.REMARK,
                    CREATED_BY = attachFile.CREATED_BY,
                    CREATED_DATE = attachFile.CREATED_DATE,
                    UPDATED_BY = attachFile.UPDATED_BY,
                    UPDATED_DATE = attachFile.UPDATED_DATE,
                    FILE_STATUS = attachFile.FILE_STATUS,
                    REQUEST_STATUS = attachFile.REQUEST_STATUS
                });
            }

            return regAttachFiles;
        }
        public static DTO.PersonAttatchFile ConvertToPersonAttachFile(this AttachFile attachFile) 
        {
            return new DTO.PersonAttatchFile()
            {
                ID = attachFile.ID,
                REGISTRATION_ID = attachFile.REGISTRATION_ID,
                ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                REMARK = attachFile.REMARK,
                CREATED_BY = attachFile.CREATED_BY,
                CREATED_DATE = attachFile.CREATED_DATE,
                UPDATED_BY = attachFile.UPDATED_BY,
                UPDATED_DATE = attachFile.UPDATED_DATE,
                FILE_STATUS = attachFile.FILE_STATUS,
                REQUEST_STATUS = attachFile.REQUEST_STATUS
            };

        }
        public static AttachFile ConvertToAttachFileView(this AttachFile attachFile) 
        {

            return new AttachFile()
            {
                ID = attachFile.ID,
                REGISTRATION_ID = attachFile.REGISTRATION_ID,
                ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                REMARK = attachFile.REMARK,
                CREATED_BY = attachFile.CREATED_BY,
                CREATED_DATE = attachFile.CREATED_DATE,
                UPDATED_BY = attachFile.UPDATED_BY,
                UPDATED_DATE = attachFile.UPDATED_DATE,
                FILE_STATUS = attachFile.FILE_STATUS,
                REQUEST_STATUS = attachFile.REQUEST_STATUS
            };
        }
        public static AttachFile ConvertToAttachFileView(this DTO.RegistrationAttatchFile attachFile)
        {

            return new AttachFile()
            {
                ID = attachFile.ID,
                REGISTRATION_ID = attachFile.REGISTRATION_ID,
                ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                REMARK = attachFile.REMARK,
                CREATED_BY = attachFile.CREATED_BY,
                CREATED_DATE = (attachFile.CREATED_DATE==null)? DateTime.MinValue: (DateTime)attachFile.CREATED_DATE,
                UPDATED_BY = attachFile.UPDATED_BY,
                UPDATED_DATE = (attachFile.UPDATED_DATE==null)? DateTime.MinValue: (DateTime)attachFile.UPDATED_DATE,
                FILE_STATUS = attachFile.FILE_STATUS,
                REQUEST_STATUS = attachFile.REQUEST_STATUS
            };
        }
        public static IList<AttachFile> ConvertToAttachFilesView(this IList<DTO.RegistrationAttatchFile> attachFiles)
        {
            IList<AttachFile> resAttachFiles = new List<AttachFile>();
            foreach (DTO.RegistrationAttatchFile attachFile in attachFiles)
            {
                resAttachFiles.Add(new AttachFile()
                {
                    ID = attachFile.ID,
                    REGISTRATION_ID = attachFile.REGISTRATION_ID,
                    ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                    REMARK = attachFile.REMARK,
                    CREATED_BY = attachFile.CREATED_BY,
                    CREATED_DATE = (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
                    UPDATED_BY = attachFile.UPDATED_BY,
                    UPDATED_DATE = (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
                    FILE_STATUS = attachFile.FILE_STATUS,
                    REQUEST_STATUS = attachFile.REQUEST_STATUS
                });
            }
            return resAttachFiles;
        }
        public static AttachFile ConvertToAttachFileView(this DTO.PersonAttatchFile attachFile)   
        {

            return new AttachFile()
            {
                ID = attachFile.ID,
                REGISTRATION_ID = attachFile.REGISTRATION_ID,
                ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                REMARK = attachFile.REMARK,
                CREATED_BY = attachFile.CREATED_BY,
                CREATED_DATE = (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
                UPDATED_BY = attachFile.UPDATED_BY,
                UPDATED_DATE = (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
                FILE_STATUS = attachFile.FILE_STATUS,
                REQUEST_STATUS = attachFile.REQUEST_STATUS
            };
        }
        public static IList<AttachFile> ConvertToAttachFilesView(this IList<DTO.PersonAttatchFile> attachFiles)
        { 
            IList<AttachFile> resAttachFiles = new List<AttachFile>();
            foreach (DTO.PersonAttatchFile attachFile in attachFiles)
            {
                resAttachFiles.Add(new AttachFile()
                {
                    ID = attachFile.ID,
                    REGISTRATION_ID = attachFile.REGISTRATION_ID,
                    ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                    REMARK = attachFile.REMARK,
                    CREATED_BY = attachFile.CREATED_BY,
                    CREATED_DATE = (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
                    UPDATED_BY = attachFile.UPDATED_BY,
                    UPDATED_DATE = (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
                    FILE_STATUS = attachFile.FILE_STATUS,
                    REQUEST_STATUS = attachFile.REQUEST_STATUS
                });
            }
            return resAttachFiles;
        }

        public static IList<DTO.AttachFileDetail> ConvertToAttatchFileDetails(this IEnumerable<AttachFile> attachFiles) 
        {
            IList<DTO.AttachFileDetail> regAttachFiles = new List<DTO.AttachFileDetail>();
            foreach (AttachFile attachFile in attachFiles)
            {
                regAttachFiles.Add(new DTO.AttachFileDetail()
                {
                    FileName = attachFile.ATTACH_FILE_NAME,
                    Extension = attachFile.REGISTRATION_ID,
                    FullFileName = attachFile.ATTACH_FILE_PATH,
                    MapFileName = attachFile.ATTACH_FILE_PATH,
          
                });
            }

            return regAttachFiles;
        }
        public static DTO.AttachFileDetail ConvertToAttatchFileDetail(this AttachFile attachFile) 
        {
            return new DTO.AttachFileDetail()
            {
                FileName = attachFile.ATTACH_FILE_NAME,
                Extension = attachFile.REGISTRATION_ID,
                FullFileName = attachFile.ATTACH_FILE_PATH,
                MapFileName = attachFile.ATTACH_FILE_PATH,
            };

        }

        public static AttachFile ConvertToAttachFileView(this DTO.AttachFileDetail attachFile)
        {

            return new AttachFile()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                REGISTRATION_ID = "",
                ATTACH_FILE_TYPE = "01",
                ATTACH_FILE_PATH = attachFile.MapFileName,
                REMARK = "",
                CREATED_BY = "",
                CREATED_DATE = DateTime.Now , // (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
                UPDATED_BY = "" , // attachFile.UPDATED_BY,
                UPDATED_DATE = DateTime.Now, // (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
                FILE_STATUS = "A" //  attachFile.FILE_STATUS
            };
        }
        public static IList<AttachFile> ConvertToAttachFilesView(this IList<DTO.AttachFileDetail> attachFiles)
        {
            IList<AttachFile> resAttachFiles = new List<AttachFile>();
            foreach (DTO.AttachFileDetail attachFile in attachFiles)
            {
                resAttachFiles.Add(new AttachFile()
                {
                    ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                    REGISTRATION_ID = "",
                    ATTACH_FILE_TYPE = "01",
                    ATTACH_FILE_PATH = attachFile.MapFileName,
                    REMARK = "",
                    CREATED_BY = "",
                    CREATED_DATE = DateTime.Now, // (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
                    UPDATED_BY = "", // attachFile.UPDATED_BY,
                    UPDATED_DATE = DateTime.Now, // (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
                    FILE_STATUS = "A" //  attachFile.FILE_STATUS
                });
            }
            return resAttachFiles;
        }

        public static IList<BLL.AttachFilesIAS.AttachFile> ConvertToAttachFilesApplicantView(IList<DTO.AttachFileApplicantChangeEntity> attachFiles)
        {
            IList<BLL.AttachFilesIAS.AttachFile> resAttachFiles = new List<BLL.AttachFilesIAS.AttachFile>();
            foreach (DTO.AttachFileApplicantChangeEntity attachFile in attachFiles)
            {
                resAttachFiles.Add(new BLL.AttachFilesIAS.AttachFile()
                {
                    ID = attachFile.ID,
                    REGISTRATION_ID = attachFile.REGISTRATION_ID,
                    ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                    ATTACH_FILE_PATH = attachFile.ATTACH_FILE_PATH,
                    REMARK = attachFile.REMARK,
                    CREATED_BY = attachFile.CREATED_BY,
                    CREATED_DATE = (attachFile.CREATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.CREATED_DATE,
                    UPDATED_BY = attachFile.UPDATED_BY,
                    UPDATED_DATE = (attachFile.UPDATED_DATE == null) ? DateTime.MinValue : (DateTime)attachFile.UPDATED_DATE,
                    FILE_STATUS = attachFile.FILE_STATUS,
                    REQUEST_STATUS = attachFile.REQUEST_STATUS
                });
            }
            return resAttachFiles;
        }
    }
}
