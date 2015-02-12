using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Configuration;
using IAS.DTO.FileService;
using IAS.Utils;

using IAS.BLL.Properties;

namespace IAS.BLL
{
    public class UploadDataBiz
    {
        protected String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        protected String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString(); 


        private ApplicantService.ApplicantServiceClient svc;

        public UploadDataBiz()
        {
            svc = new ApplicantService.ApplicantServiceClient();
        }

        public DTO.RegistrationAttatchFile SaveAttachToTemp(DTO.RegistrationAttatchFile attachFile, Stream fileSteam) 
        {
      
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                UploadFileResponse response = new UploadFileResponse();
                String fs_container = String.Format(@"{0}\{1}", TempFileContainer, attachFile.TempFilePath);


                response = svc.UploadFile(new UploadFileRequest(){
                                                TargetContainer = fs_container,
                                                TargetFileName = attachFile.FileName,
                                                FileStream = fileSteam
                                            });
                if (response.Code != "0000")
                    throw new IOException(response.Message);

                attachFile.ATTACH_FILE_PATH = response.TargetFullName;
            
            }

            return attachFile;

        }

        public DTO.PersonAttatchFile SaveEditAttachFileTemp(DTO.PersonAttatchFile attachFile, Stream fileSteam) { 

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                UploadFileResponse response = new UploadFileResponse();
                String fs_container = String.Format(@"{0}\{1}", TempFileContainer, attachFile.TempFilePath);

                String filename = String.Format("{0}.{1}", IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId() , attachFile.FileName.Split('.')[attachFile.FileName.Split('.').Length - 1]);
                response = svc.UploadFile(new UploadFileRequest() { 
                                            TargetContainer = fs_container, 
                                            TargetFileName = filename, 
                                            FileStream = fileSteam });

                if (response.Code != "0000")
                    throw new IOException(response.Message);

                attachFile.ATTACH_FILE_PATH = response.TargetFullName;
                attachFile.FILE_STATUS = "W";
            }

            return attachFile;
        }

        public UploadFileResponse UploadAttachFileToTemp(BLL.AttachFilesIAS.AttachFile attachFile, Stream fileStream) 
        { 
            String brokenRuleString = ThrowExceptionIfUploadInvalid(attachFile);
            if(!String.IsNullOrEmpty(brokenRuleString)){
                throw new ApplicationException(brokenRuleString);
            }
            UploadFileResponse response = new UploadFileResponse();
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {


                    String fs_container = String.Format(@"{0}\{1}", TempFileContainer, attachFile.REGISTRATION_ID);

                    String filename = String.Format("{0}{1}", IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(), attachFile.EXTENSION);

                    UploadFileResponse res = svc.UploadFile(new UploadFileRequest()
                    {
                        TargetContainer = fs_container,
                        TargetFileName = filename,
                        FileStream = fileStream
                    });

                    return res;

                //if (response.Code != "0000")
                //    throw new IOException(response.Message);

                //attachFile.ATTACH_FILE_PATH = response.TargetFullName;

            }
            return response;
        }

        //Added by Nattapong for License
        public UploadFileResponse UploadAttachFileLicenseToTemp(BLL.AttachFilesIAS.AttachFile attachFile, Stream fileStream)
        {
            String brokenRuleString = ThrowExceptionIfUploadInvalid(attachFile);
            if (!String.IsNullOrEmpty(brokenRuleString))
            {
                throw new ApplicationException(brokenRuleString);
            }
            UploadFileResponse response = new UploadFileResponse();
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {

                String fs_container = String.Format(@"{0}\{1}", TempFileContainer, attachFile.REGISTRATION_ID);

                String filename = String.Format("{0}{1}", IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId() + "_" + attachFile.ATTACH_FILE_TYPE, attachFile.EXTENSION);
                response = svc.UploadFile(new UploadFileRequest()
                {
                    TargetContainer = fs_container,
                    TargetFileName = filename,
                    FileStream = fileStream
                });

                //if (response.Code != "0000")
                //    throw new IOException(response.Message);

                //attachFile.ATTACH_FILE_PATH = response.TargetFullName;

            }
            return response;
        }

        public void DeleteAttachFileInTemp(String pathRemoteFile) 
        {
            if (String.IsNullOrEmpty(pathRemoteFile))
                throw new IOException(Resources.errorUploadDataBiz_001);

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                DeleteFileResponse response = svc.DeleteFile(new DeleteFileRequest() { TargetFileName = pathRemoteFile });
                if (response.Code != "0000")
                    throw new IOException(response.Message);
            }

        }
        private String ThrowExceptionIfUploadInvalid(BLL.AttachFilesIAS.AttachFile attachFile)
        {
            StringBuilder brokenRules = new StringBuilder();
            if (attachFile.GetBrokenRules().Count() > 0)
            {
                brokenRules.AppendLine(Resources.errorUploadDataBiz_002);
                foreach (BLL.BusinessRule bussinessRule in attachFile.GetBrokenRules())
                {
                    brokenRules.AppendLine(bussinessRule.Rule);
                }
            }

            return brokenRules.ToString();
        }
        //public DTO.PersonAttatchFile SaveToEditAttachFilePath(DTO.PersonAttatchFile newAttachFile, DTO.PersonAttatchFile oldAttachFile, Stream fileSteam)
        //{
        //    DTO.RegistrationAttatchFile _newAttachFile =  new DTO.RegistrationAttatchFile();
        //    DTO.RegistrationAttatchFile _oldAttachFile = new DTO.RegistrationAttatchFile();
        //    newAttachFile.MappingToEntity<DTO.RegistrationAttatchFile>(_newAttachFile);
        //    oldAttachFile.MappingToEntity<DTO.RegistrationAttatchFile>(_oldAttachFile);

        //    using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
        //    {
   
        //        String findString = "_"+oldAttachFile.ATTACH_FILE_TYPE.ToInt().ToString("00");
        //        String newfileName = oldAttachFile.ATTACH_FILE_PATH.Replace(findString, findString + IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId());


        //        MoveFileResponse moveResponse = svc.MoveFile("", oldAttachFile.ATTACH_FILE_PATH, "", newfileName);
        //        if (moveResponse.Code != "0000")
        //            throw new IOException(moveResponse.Message);

        //        DeleteFileResponse deleteResponse = svc.DeleteFile(oldAttachFile.ATTACH_FILE_PATH);
        //        if (deleteResponse.Code != "0000")
        //            throw new IOException(deleteResponse.Message);

        //        UploadFileResponse uploadResponse = svc.UploadFile("", oldAttachFile.ATTACH_FILE_PATH, fileSteam);
        //        if (uploadResponse.Code != "0000")
        //            throw new IOException(uploadResponse.Message);



        //        _newAttachFile.ATTACH_FILE_PATH = uploadResponse.TargetFullName;
   
        //    }
        //    _newAttachFile.MappingToEntity<DTO.RegistrationAttatchFile, DTO.PersonAttatchFile>(newAttachFile);
        //    return newAttachFile;

        //} 

        public DTO.RegistrationAttatchFile SaveToAttachFilePath(DTO.RegistrationAttatchFile attachFile, Stream fileSteam) 
        {

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                UploadFileResponse response = new UploadFileResponse();
                String fs_container = String.Format(@"{0}\{1}", AttachFileContainer, attachFile.TempFilePath); 

                //String newFileName = this.ID_CARD_NO + "_"
                //                                    + Convert.ToInt32(attachFile.ATTACH_FILE_TYPE).ToString("00")
                //                                    + "." + GetExtensionFile(attachFile.FileName);

                response = svc.UploadFile(new UploadFileRequest()
                                                {
                                                    TargetContainer = fs_container,
                                                    TargetFileName = attachFile.FileName,
                                                    FileStream = fileSteam
                                                });
                if (response.Code != "0000")
                    throw new IOException(response.Message);

                attachFile.ATTACH_FILE_PATH = response.TargetFullName;
   
            }

            return attachFile;

        }

        public virtual void GetFile(string pathRemoteFile, ref Stream fileStrem)   
        {

            //DTO.RegistrationAttatchFile attachFile = new DTO.RegistrationAttatchFile();
            //if (_attachFiles.Where(a => a.ATTACH_FILE_TYPE == type).Count() <= 0)
            //    throw new RegisterationException("ไม่พบเอกสารแนบตามประเภทที่ต้องการ !!!");

            //attachFile = _attachFiles.Where(a => a.ATTACH_FILE_TYPE == type).FirstOrDefault();
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                DownloadFileResponse response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = pathRemoteFile });
                if (response.Code != "0000")
                    throw new IOException(response.Message);

                fileStrem = response.FileByteStream;
            }

        }

        public void DeleteTempFile(String pathRemoteFile)    
        {

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                DeleteFileResponse response = svc.DeleteFile(new DeleteFileRequest() { TargetFileName = pathRemoteFile });
                if (response.Code != "0000")
                    throw new IOException(response.Message);
            }

        }

        //public DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>> 
        //    UploadData(string fileName, Stream rawData, string testingNo, string userId)
        //{
        //    var res = new DTO.ResponseService<DTO.UploadResult<DTO.UploadHeader, DTO.ApplicantTemp>>();
        //    if (rawData == null)
        //    {
        //        res.ErrorMsg = "ไม่มีข้อมูล";
        //        return res;
        //    }

        //    //เปลี่ยนสไตล์ของวันที่เป็นแบบไทย เพื่อแสดงค่าที่เป็นปี พ.ศ.
        //    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");

        //    DTO.UploadData data = new DTO.UploadData
        //    {
        //        Body = new List<string>()
        //    };

        //    try
        //    {
        //        try
        //        {
        //            //Stream rawData = FileUpload1.PostedFile.InputStream;
        //            using (StreamReader sr = new StreamReader(rawData, System.Text.Encoding.GetEncoding("TIS-620")))
        //            {
        //                string line = sr.ReadLine().Trim();
        //                if (line != null && line.Length > 0) data.Header = line;
        //                while ((line = sr.ReadLine()) != null)
        //                {
        //                    if (line.Trim().Length > 0)
        //                    {
        //                        data.Body.Add(line.Trim());
        //                    }
        //                }
        //            }

        //            res = svc.InsertAndCheckApplicantGroupUpload(data, fileName, DTO.RegistrationType.Insurance, testingNo, userId);

        //        }
        //        catch (Exception ex)
        //        {
        //            res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //        }
        //    }
        //    catch (IOException ex)
        //    {
        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }
        //    return res;
        //}


    }
}
