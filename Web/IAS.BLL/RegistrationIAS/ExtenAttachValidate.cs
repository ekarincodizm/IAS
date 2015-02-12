using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS
{
    public static class ExtenAttachValidate
    {
        public static void  AttachValidate(this DTO.RegistrationAttatchFile attachFile ){

            string fileName = attachFile.FileName;

            validateFileType(fileName);

            if (attachFile.ATTACH_FILE_TYPE == "")
                throw new ApplicationException(SysMessage.PleaseSelectFile);

            if (attachFile.FileName == "")
                throw new ApplicationException(SysMessage.PleaseChooseFile);


        }

        private static void validateFileType(string fileName)
        {
            string fileExtension = System.IO.Path.GetExtension(fileName).Replace(".", string.Empty).ToLower();
            if (!DTO.DocumentFileType.IMAGE_BMP_GIF_JPG_PNG_TIF_PDF.ToString().ToLower().Contains(fileExtension))
                throw new ApplicationException(SysMessage.PleaseSelectFile);

        }
    }
}
