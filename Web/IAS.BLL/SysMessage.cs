using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.BLL.Properties;

namespace IAS.BLL
{
    public class SysMessage
    {
        public static string DialogLoginFail { get { return Resources.infoSysMessage_DialogLoginFail; } }
        public static string DialogLoginTitle { get { return Resources.infoSysMessage_DialogLoginTitle; } }
        public static string DefaultSelecting { get { return Resources.infoSysMessage_DefaultSelecting; } }
        public static string SaveAssociationSuccess { get { return Resources.infoSysMessage_SaveAssociationSuccess; } }
        public static string PleaseChooseMemberType { get { return Resources.errorSysMessage_PleaseChooseMemberType; } }
        //Fuse Edit 8/03/2013
        public static string ServerError { get { return Resources.errorSysMessage_ServerError; } }

        //Tob Edit 22/02/2013//
        public static string DataEmpty { get { return Resources.errorSysMessage_DataEmpty; } }
        public static string SaveSucess { get { return Resources.infoSysMessage_SaveSucess; } }
        public static string Fail { get { return Resources.errorSysMessage_Fail; } }
        public static string TryAgain { get { return Resources.errorSysMessage_TryAgain; } }
        public static string DeleteFile { get { return Resources.infoSysMessage_DeleteFile; } }
        public static string PleaseDeleteFile { get { return Resources.errorSysMessage_PleaseDeleteFile; } }
        public static string SelectFile { get { return Resources.infoSysMessage_SelectFile; } }
        public static string PleaseChooseFile { get { return Resources.errorSysMessage_PleaseChooseFile; } }
        public static string PleaseSelectFile { get { return Resources.errorSysMessage_PleaseSelectFile; } }
        public static string PleaseInputIDNumber { get { return Resources.infoSysMessage_PleaseInputIDNumber; } }


        //Tob Edit 28/02/2013//
        public static string NotSame { get { return Resources.errorSysMessage_NotSame; } }

        //Tob Edit 12/03/2013//
        public static string EmailErrorFormat { get { return Resources.errorSysMessage_EmailErrorFormat; } }

        public static string CheckCondition { get { return Resources.infoSysMessage_CheckCondition; } }

        //Fuse Edit 25/03/2013//
        public static string TitleTypeMemberOIC { get { return Resources.infoSysMessage_TitleTypeMemberOIC; } }
        public static string PleaseChooesTypeMemberOIC { get { return Resources.errorSysMessage_PleaseChooesTypeMemberOIC; } }


        //Tob Edit 25/04/2013//
        public static string NoData { get { return Resources.errorSysMessage_NoData; } }
        public static string PleaseInputFill { get { return Resources.errorSysMessage_PleaseInputFill; } }
        public static string ChooseData { get { return Resources.infoSysMessage_ChooseData; } }

        //Fuse Edit 30/04/2013//
        public static string CannotDeleteExamTestingNo { get { return Resources.errorSysMessage_CannotDeleteExamTestingNo; } }
        public static string CannotEditExamTestingNo { get { return Resources.errorSysMessage_CannotEditExamTestingNo; } }

        //Fuse Edit 01/05/2013//
        public static string CannotUploadFile { get { return Resources.errorSysMessage_CannotUploadFile; } }

        //Tob Edit 04/05/2013
        public static string PleaseSelectFileTxt { get { return Resources.errorSysMessage_PleaseSelectFileTxt; } }

        //Fuse Edit 07/05/2013
        public static string PleaseInputTestingNo { get { return Resources.errorSysMessage_PleaseInputTestingNo; } }

        //Fuse Edit 08/05/2013
        public static string SuccessInsertTypeOIC { get { return Resources.infoSysMessage_SuccessInsertTypeOIC; } }

        //Fuse Edit 14/05/2013
        public static string SuccessInsertApplicant { get { return Resources.infoSysMessage_SuccessInsertApplicant; } }

        //Fuse Edit 17/05/2013
        public static string SuccessImportData { get { return Resources.infoSysMessage_SuccessImportData; } }

        //Fuse Edit 18/05/2013
        public static string PleaseSelectFileData { get { return Resources.infoSysMessage_PleaseSelectFileData; } }

        //Fuse Edit 20/05/2013
        public static string PleaseInputCompCode { get { return Resources.infoSysMessage_PleaseInputCompCode; } }

        //Fuse Edit 21/05/2013
        public static string Approval { get { return Resources.infoSysMessage_Approval; } }
        public static string PleaseSelectDate { get { return Resources.infoSysMessage_PleaseSelectDate; } }

        //Fuse Edit 27/05/2013
        public static string PleaseInputCompany { get { return Resources.errorSysMessage_PleaseInputCompany; } }

        //Tob Edit 24/06/2013
        public static string PleaseChangeDefault { get { return Resources.errorSysMessage_PleaseChangeDefault; } }
        public static string PleaseSamePassword { get { return Resources.errorSysMessage_PleaseSamePassword; } }


        //Fuse Edit 24/06/2013
        public static string SuccessInsertTypeGroupPlace { get { return Resources.infoSysMessage_SuccessInsertTypeGroupPlace; } }

        //Fuse Edit 30/06/2013
        public static string SuccessInsertLicenseSingle { get { return Resources.infoSysMessage_SuccessInsertLicenseSingle; } }

    }
}
