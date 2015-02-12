using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using IAS.DTO.FileService;
using IAS.BLL.AttachFilesIAS;

namespace IAS.BLL.RegistrationIAS.Implements
{
    public class GeneralRegistration : BaseRegistration, IRegistration
    {
        public GeneralRegistration()
        {
        
        }

        public GeneralRegistration(String idCard)
        {
            ID_CARD_NO = idCard;
        }


        public override void Init()
        {

        }

        public override void Save()
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

            

            using (RegistrationService.RegistrationServiceClient svc = new RegistrationService.RegistrationServiceClient()) 
            {
                DTO.ResponseService<DTO.Registration> res = new DTO.ResponseService<DTO.Registration>();
                switch (StateStatus)
                {
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.NewRegisteration:
                         if (this.StateStatus == States.RegistrationStatus.NewRegisteration)
                        {
                            UploadAttachFiles();
                            res = svc.InsertWithAttatchFile(DTO.RegistrationType.General,
                                                                        this.ConvertToDTORegisteration(),
                                                                        this.AttachFiles.ConvertToRegistrationAttachFiles().ToArray());
                        }
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.WaitForApprove:
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.Approve:
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.Disapprove:
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.WaitForApproveEdit:
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.ApproveEdit:
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.DisapproveEdit:
                        break;
                    case IAS.BLL.RegistrationIAS.States.RegistrationStatus.Cancel:
                        break;
                    default:
                        break;
                }
    
                
               
                if (res.IsError)
                {
                    throw new RegistrationException(res.ErrorMsg);
                }
            }
        }

        


        public  DTO.User ExportIASUser()
        {
            DateTime currentDate = DateTime.Now;
            DTO.User user = new DTO.User()
            {
                USER_ID = this.ID,
                USER_NAME = this.ID_CARD_NO,
                USER_PASS = this.REG_PASS,
                USER_TYPE = this.MEMBER_TYPE,
                IS_ACTIVE = DTO.UserStatus.Active.ToString().Substring(0, 1),
                USER_RIGHT = this.MEMBER_TYPE,
                CREATED_BY = this.CREATED_BY,
                CREATED_DATE = currentDate,
                UPDATED_BY = this.UPDATED_BY,
                UPDATED_DATE = currentDate
     
            };
            return user;
        }
        protected override void ValidateEntity()  
        {
            
        }


    }
}
