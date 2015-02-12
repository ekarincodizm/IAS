using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IAS.DTO.FileService;
using System.Configuration;
using IAS.BLL.RegistrationIAS.States;
using IAS.BLL.AttachFilesIAS.States;

using IAS.BLL.Properties;

namespace IAS.BLL.RegistrationIAS
{

    public abstract class BaseRegistration : EntityBase<String>
    {
        
        protected String TempFileContainer = ConfigurationManager.AppSettings["FS_TEMP"].ToString();
        protected String AttachFileContainer = ConfigurationManager.AppSettings["FS_ATTACH"].ToString();

        private IList<AttachFilesIAS.AttachFile> _attachFiles = new List<AttachFilesIAS.AttachFile>();
        private IRegistrationState _state;

        public BaseRegistration()
        {
            Init();
        }

        public IEnumerable<AttachFilesIAS.AttachFile> AttachFiles
        {
            get { return _attachFiles; }
        }

        
       
        public String DataActionMode { get; set; }



        public String ID 
        {
            get { return base.Id; }
            set { base.Id = value; } 
        }
        public String MEMBER_TYPE { get; set; }

        public String ID_CARD_NO { get; set; }
        public String EMPLOYEE_NO { get; set; }
        public String PRE_NAME_CODE { get; set; }
        public String NAMES { get; set; }
        public String LASTNAME { get; set; }
        public String NATIONALITY { get; set; }
        public DateTime BIRTH_DATE { get; set; }
        public String SEX { get; set; }
        public String EDUCATION_CODE { get; set; }
        public String ADDRESS_1 { get; set; }
        public String ADDRESS_2 { get; set; }
        public String AREA_CODE { get; set; }
        public String PROVINCE_CODE { get; set; }
        public String ZIP_CODE { get; set; }
        public String TELEPHONE { get; set; }
        public String LOCAL_ADDRESS1 { get; set; }
        public String LOCAL_ADDRESS2 { get; set; }
        public String LOCAL_AREA_CODE { get; set; }
        public String LOCAL_PROVINCE_CODE { get; set; }
        public String LOCAL_ZIPCODE { get; set; }
        public String LOCAL_TELEPHONE { get; set; }
        public String EMAIL { get; set; }
        public String STATUS {
            get { return ((int)StateStatus).ToString(); }
            set {
                switch (value) {
                    case "0": this.SetStateTo(States.RegistrationStates.New); break;
                    case "1": this.SetStateTo(States.RegistrationStates.WaitForApprove); break;
                    case "2": this.SetStateTo(States.RegistrationStates.Approve); break;
                    case "3": this.SetStateTo(States.RegistrationStates.Disapprove); break;
                    case "4": this.SetStateTo(States.RegistrationStates.WaitForApproveEdit); break;
                    case "5": this.SetStateTo(States.RegistrationStates.ApproveEdit); break;
                    case "6": this.SetStateTo(States.RegistrationStates.DisapproveEdit); break;
                    case "7": this.SetStateTo(States.RegistrationStates.Cancel); break;
                    default: throw new InvalidOperationException("ไม่พบสถานะการลงทะเบียนสัมครที่ระบุ.");
                }
            } 
        }

        public States.RegistrationStatus StateStatus  
        {
            get { return _state.Status; } 
        }

        public String TUMBON_CODE { get; set; }
        public String LOCAL_TUMBON_CODE { get; set; }
        public String COMP_CODE { get; set; }
        public String CREATED_BY { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public String UPDATED_BY { get; set; }
        public DateTime UPDATED_DATE { get; set; }
        public DateTime? NOT_APPROVE_DATE { get; set; }
        public String LINK_REDIRECT { get; set; }
        public String REG_PASS { get; set; }


        public DTO.MemberType MemberType 
        {
            get {
                switch (MEMBER_TYPE)
                {
                    case "1": return  DTO.MemberType.General; 
                    case "2": return  DTO.MemberType.Insurance; 
                    case "3": return  DTO.MemberType.Association; 
                    case "4": return  DTO.MemberType.OIC;
                    case "5": return DTO.MemberType.OICAgent;
                    case "6": return DTO.MemberType.OICFinance;
                    case "7": return DTO.MemberType.TestCenter;
                   
                }
                return DTO.MemberType.General;
            }
            set {
                switch (value)
                {
                    case IAS.DTO.MemberType.General:
                        MEMBER_TYPE = "1";
                        break;
                    case IAS.DTO.MemberType.Insurance:
                        MEMBER_TYPE = "2";
                        break;
                    case IAS.DTO.MemberType.Association:
                        MEMBER_TYPE = "3";
                        break;
                    case IAS.DTO.MemberType.OIC:
                        MEMBER_TYPE = "4";
                        break;
                    case IAS.DTO.MemberType.OICFinance:
                        MEMBER_TYPE = "5";
                        break;
                    case IAS.DTO.MemberType.OICAgent:
                        MEMBER_TYPE = "6";
                        break;
                    case IAS.DTO.MemberType.TestCenter:
                        MEMBER_TYPE = "7";
                        break;
                    default:
                        MEMBER_TYPE = "";
                        break;
                }
                
            }
        
        }

        public abstract void Init();




        public abstract void Save();

        internal void SetStateTo(IRegistrationState state)  { this._state = state; }


        public void SetApprove(DTO.UserProfile userProfile) {
            if (userProfile.MemberType != (int)DTO.MemberType.OIC)
                throw new ApplicationException(Resources.errorBaseRegistration_002);

            this.STATUS = ((int)States.RegistrationStatus.Approve).ToString();

            this._state.Approve(this);
        }

        public void SetDisapprove(DTO.UserProfile userProfile) 
        { 
            if(userProfile.MemberType !=(int)DTO.MemberType.OIC)
                throw new ApplicationException(Resources.errorBaseRegistration_002);

            this.STATUS = ((int)States.RegistrationStatus.Disapprove).ToString();

            this._state.Disapprove(this);
        }

        public void Submit() {

            this._state.Submit(this);
        }

        public virtual void AddAttach(FileInfo fileInfo,String attachType)  
        {
            if (_attachFiles.Where(a => a.ATTACH_FILE_TYPE == attachType).Count() > 0)
            {
                throw new RegistrationException(Resources.errorBasePersonal_001);
            }
            if (!fileInfo.Exists) {
                throw new RegistrationException(Resources.errorBaseRegistration_003);
            }

            // ------------ Init Data --------------
            String container = String.Format(@"{0}\{1}", TempFileContainer, this.ID);

            String fileName = String.Format("{0}_{1:00}{2}", this.ID_CARD_NO, Convert.ToInt32(attachType), fileInfo.Extension);
            DateTime curDate = DateTime.Now;

            AttachFilesIAS.AttachFile attachFile = new AttachFilesIAS.AttachFile()
            { 
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                ATTACH_FILE_TYPE = attachType,
                REGISTRATION_ID = this.ID,
                CREATED_BY = this.ID_CARD_NO,
                CREATED_DATE = curDate,
                UPDATED_BY = this.ID_CARD_NO,
                UPDATED_DATE = curDate,
                FILE_STATUS = AttachFileStatus.Active.Value(),
                ATTACH_FILE_PATH = String.Format(@"{0}\{1}", container, fileName)
            };
            //-----------------------------------------

        

            // -------------- Upload File To Temp--------------------------------
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                UploadFileResponse response = new UploadFileResponse();


                //response = svc.UploadFile(new UploadFileRequest() {
                //                                TargetContainer = attachFile.ATTACH_CONTAINER,
                //                                TargetFileName = attachFile.ATTACH_FILE_NAME,
                //                                FileStream = fileInfo.Create()
                //                            });
                if (response.Code != "0000")
                    throw new RegistrationException(response.Message);

                attachFile.ATTACH_FILE_PATH = response.TargetFullName;
                _attachFiles.Add(attachFile);

            }
           // ------------------------------------------------------------------
        }


        public virtual AttachFilesIAS.AttachFile GetAttach(String id, ref Stream fileStrem)
        {

            AttachFilesIAS.AttachFile attachFile = new AttachFilesIAS.AttachFile();

            if (_attachFiles.Where(a => a.ID == id).Count() <= 0)
                throw new RegistrationException(Resources.errorBaseRegistration_004);

            attachFile = _attachFiles.Where(a => a.ID == id).Single();

            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                //DownloadFileResponse response = svc.DownloadFile(new DownloadFileRequest() { TargetContainer = "", TargetFileName = attachFile.ATTACH_FILE_PATH });
                //if (response.Code != "0000")
                //    throw new RegistrationException(response.Message);

                //fileStrem = response.FileByteStream;
            }
            
            return attachFile;
        }

        public virtual void DeleteAttach(String id)
        {
            if (_attachFiles.Where(a => a.ID==id ).Count() <= 0)
                throw new RegistrationException(Resources.errorBaseRegistration_004);

            AttachFilesIAS.AttachFile attachFile = AttachFiles.Where(a => a.ID == id).Single();
            using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
            {
                //DeleteFileResponse response = svc.DeleteFile(new DeleteFileRequest() { TargetFileName = attachFile.ATTACH_FILE_PATH });
                //if (response.Code != "0000")
                //    throw new RegistrationException(response.Message);

                _attachFiles.Remove(attachFile);
            }

        }

        protected void UploadAttachFiles()
        {
            try
            {
                using (FileService.FileTransferServiceClient svc = new FileService.FileTransferServiceClient())
                {
                    MoveFileResponse response = new MoveFileResponse();

                    foreach (AttachFilesIAS.AttachFile attatch in this.AttachFiles)
                    {
                        String container = String.Format(@"{0}\{1}", AttachFileContainer, this.ID_CARD_NO);

                        String newFileName = this.ID_CARD_NO + "_"
                                                + Convert.ToInt32(attatch.ATTACH_FILE_TYPE).ToString("00")
                                                + "." + attatch.EXTENSION;

                        //response = svc.MoveFile(new MoveFileRequest() { 
                        //                                CurrentContainer = "", 
                        //                                CurrentFileName = attatch.ATTACH_FILE_PATH, 
                        //                                TargetContainer = container, 
                        //                                TargetFileName = newFileName });
                        if (response.Code != "0000")
                            throw new RegistrationException(response.Message);

                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationException(ex.Message);
            }
        }

        //public virtual DTO.Person ExportPersonal() {
        //    DTO.Person person = new DTO.Person() 
        //    {
        //        ID = ID,
        //        MEMBER_TYPE = MEMBER_TYPE,
        //        ID_CARD_NO = ID_CARD_NO,
        //        EMPLOYEE_NO = EMPLOYEE_NO,
        //        PRE_NAME_CODE = PRE_NAME_CODE,
        //        NAMES = NAMES,
        //        LASTNAME = LASTNAME,
        //        NATIONALITY = NATIONALITY,
        //        BIRTH_DATE = BIRTH_DATE,
        //        SEX = SEX,
        //        EDUCATION_CODE = EDUCATION_CODE,
        //        ADDRESS_1 = ADDRESS_1,
        //        ADDRESS_2 = ADDRESS_2,
        //        AREA_CODE = AREA_CODE,
        //        PROVINCE_CODE = PROVINCE_CODE,
        //        ZIP_CODE = ZIP_CODE,
        //        TELEPHONE = TELEPHONE,
        //        LOCAL_ADDRESS1 = LOCAL_ADDRESS1,
        //        LOCAL_ADDRESS2 = LOCAL_ADDRESS2,
        //        LOCAL_AREA_CODE = LOCAL_AREA_CODE,
        //        LOCAL_PROVINCE_CODE = LOCAL_PROVINCE_CODE,
        //        LOCAL_ZIPCODE = LOCAL_ZIPCODE,
        //        LOCAL_TELEPHONE = LOCAL_TELEPHONE,
        //        EMAIL = EMAIL,
        //        STATUS = STATUS,
        //        TUMBON_CODE = TUMBON_CODE,
        //        LOCAL_TUMBON_CODE = LOCAL_TUMBON_CODE,
        //        COMP_CODE = COMP_CODE,
        //        CREATED_BY = CREATED_BY,
        //        CREATED_DATE = CREATED_DATE,
        //        UPDATED_BY = UPDATED_BY,
        //        UPDATED_DATE = UPDATED_DATE
                    
        //        };
             

        //    return person;
        
        //}



        protected abstract void ValidateEntity();

        protected override void Validate()
        {
      
        //if( String.IsNullOrEmpty(DataActionMode) )  base.AddBrokenRule( RegisterationBusinessRules.ID_Required ); 


        if( String.IsNullOrEmpty(ID) )  
            base.AddBrokenRule( RegistrationBusinessRules.ID_Required ); 

        if( String.IsNullOrEmpty(MEMBER_TYPE) )  
            base.AddBrokenRule( RegistrationBusinessRules.MEMBER_TYPE_Required ); 

        if( String.IsNullOrEmpty(ID_CARD_NO) )  
            base.AddBrokenRule( RegistrationBusinessRules.ID_CARD_NO_Required ); 

        //if( String.IsNullOrEmpty(EMPLOYEE_NO) )  
        //    base.AddBrokenRule( RegistrationBusinessRules.EMPLOYEE_NO_Required ); 

        if( String.IsNullOrEmpty(PRE_NAME_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.PRE_NAME_CODE_Required ); 

        if( String.IsNullOrEmpty(NAMES) )  
            base.AddBrokenRule( RegistrationBusinessRules.NAMES_Required ); 

        if( String.IsNullOrEmpty(LASTNAME) )  
            base.AddBrokenRule( RegistrationBusinessRules.LASTNAME_Required ); 

        if( String.IsNullOrEmpty(NATIONALITY) )  
            base.AddBrokenRule( RegistrationBusinessRules.NATIONALITY_Required ); 

        if( BIRTH_DATE==DateTime.MinValue )  
            base.AddBrokenRule( RegistrationBusinessRules.BIRTH_DATE_Required ); 

        if( String.IsNullOrEmpty(SEX) )  
            base.AddBrokenRule( RegistrationBusinessRules.SEX_Required ); 

        if( String.IsNullOrEmpty(EDUCATION_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.EDUCATION_CODE_Required ); 

        if( String.IsNullOrEmpty(ADDRESS_1) )  
            base.AddBrokenRule( RegistrationBusinessRules.ADDRESS_1_Required ); 

        if( String.IsNullOrEmpty(ADDRESS_2) )  
            base.AddBrokenRule( RegistrationBusinessRules.ADDRESS_2_Required ); 

        if( String.IsNullOrEmpty(AREA_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.AREA_CODE_Required ); 

        if( String.IsNullOrEmpty(PROVINCE_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.PROVINCE_CODE_Required );
 
        if( String.IsNullOrEmpty(ZIP_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.ZIP_CODE_Required );
 
        if( String.IsNullOrEmpty(TELEPHONE) )  
            base.AddBrokenRule( RegistrationBusinessRules.TELEPHONE_Required );
 
        if( String.IsNullOrEmpty(LOCAL_ADDRESS1) )  
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_ADDRESS1_Required ); 

        if( String.IsNullOrEmpty(LOCAL_ADDRESS2) )  
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_ADDRESS2_Required ); 

        if( String.IsNullOrEmpty(LOCAL_AREA_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_AREA_CODE_Required ); 

        if( String.IsNullOrEmpty(LOCAL_PROVINCE_CODE) ) 
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_PROVINCE_CODE_Required ); 

        if( String.IsNullOrEmpty(LOCAL_ZIPCODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_ZIPCODE_Required ); 

        if( String.IsNullOrEmpty(LOCAL_TELEPHONE) )  
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_TELEPHONE_Required );
 
        if( String.IsNullOrEmpty(EMAIL) )  
            base.AddBrokenRule( RegistrationBusinessRules.EMAIL_Required ); 

        if( String.IsNullOrEmpty(STATUS) )  
            base.AddBrokenRule( RegistrationBusinessRules.STATUS_Required ); 

        if( String.IsNullOrEmpty(TUMBON_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.TUMBON_CODE_Required );
 
        if( String.IsNullOrEmpty(LOCAL_TUMBON_CODE) )  
            base.AddBrokenRule( RegistrationBusinessRules.LOCAL_TUMBON_CODE_Required ); 

        //if( String.IsNullOrEmpty(COMP_CODE) )  
        //    base.AddBrokenRule( RegistrationBusinessRules.COMP_CODE_Required );
 
        if( String.IsNullOrEmpty(CREATED_BY) )  
            base.AddBrokenRule( RegistrationBusinessRules.CREATED_BY_Required ); 

        if( CREATED_DATE == DateTime.MinValue ) 
            base.AddBrokenRule( RegistrationBusinessRules.CREATED_DATE_Required ); 

        if( String.IsNullOrEmpty(UPDATED_BY) )  
            base.AddBrokenRule( RegistrationBusinessRules.UPDATED_BY_Required ); 

        if( UPDATED_DATE == DateTime.MinValue )  
            base.AddBrokenRule( RegistrationBusinessRules.UPDATED_DATE_Required );

        //if (NOT_APPROVE_DATE == null || NOT_APPROVE_DATE == DateTime.MinValue) 
        //    base.AddBrokenRule(RegisterationBusinessRules.NOT_APPROVE_DATE_Required);

        //if (String.IsNullOrEmpty(LINK_REDIRECT)) 
        //    base.AddBrokenRule(RegistrationBusinessRules.LINK_REDIRECT_Required);

        if (String.IsNullOrEmpty(REG_PASS)) 
            base.AddBrokenRule(RegistrationBusinessRules.REG_PASS_Required);


            //Call validation of each Registeration.
            ValidateEntity();
        }






    }


}
