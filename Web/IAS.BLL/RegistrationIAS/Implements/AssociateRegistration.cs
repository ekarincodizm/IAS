using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.AttachFilesIAS;

namespace IAS.BLL.RegistrationIAS.Implements
{
    public class AssociateRegistration : BaseRegistration, IRegistration
    {


        public override void Init()
        {
            
        }

        public override void Save()
        {
            BLL.RegistrationBiz biz = new BLL.RegistrationBiz();

            UploadAttachFiles();

            using (RegistrationService.RegistrationServiceClient svc = new RegistrationService.RegistrationServiceClient())
            {

                DTO.ResponseService<DTO.Registration> res = svc.InsertWithAttatchFile(DTO.RegistrationType.General,
                                                                                        this.ConvertToDTORegisteration(),
                                                                                        this.AttachFiles.ConvertToRegistrationAttachFiles().ToArray());
                if (res.IsError)
                {
                    throw new RegistrationException(res.ErrorMsg);
                }
            }
        }

        protected override void ValidateEntity()
        {
            throw new NotImplementedException();
        }
    }
}
