using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.BLL.RegistrationIAS.Implements;

using IAS.BLL.Properties;

namespace IAS.BLL.RegistrationIAS 
{
    public  class RegistrationFactory
    {
        private static RegistrationService.RegistrationServiceClient svc;

        public static IRegistration CreateGeneralRegistration()
        {
            IRegistration generalRegistration = new GeneralRegistration() { 
                ID= IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE="1",                
                STATUS="0"
                            };
            return generalRegistration;
        }

        public static IRegistration CreateCompanyRegistration() 
        {
            IRegistration companyRegistration = new CompanyRegistration()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE = "2",
                STATUS = "0"
            };
            return companyRegistration;
        }


        public static IRegistration CreateAssoiateRegistration() {
            IRegistration assoiateRegistration = new AssociateRegistration()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE = "3",
                STATUS = "0"
            };


            return assoiateRegistration;
        }

        public static IRegistration CreateOICAdminRegistration() {
            IRegistration oicAdminRegistration = new OICAdminRegistration()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE = "4",
                STATUS = "0"
            };

            return oicAdminRegistration;
        }

        public static IRegistration CreateOICAgantStaffRegistration() {
            IRegistration oicAgantStaffRegistration = new OICAgantStaffRegistration()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE = "5",
                STATUS = "0"
            };

            return oicAgantStaffRegistration;
        }

        public static IRegistration CreateOICFinanceStuffRegistration() {
            IRegistration oicFinanceStuffRegistration = new OICFinanceStuffRegistration()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE = "6",
                STATUS = "0"
            };

            return oicFinanceStuffRegistration;
        }

        public static IRegistration CreateOICTestStuffRegistration() {
            IRegistration oicTestStuffRegistration = new OICTestStuffRegistration()
            {
                ID = IAS.BLL.Helpers.GenerateIdHelper.GetGenAutoId(),
                MEMBER_TYPE = "7",
                STATUS = "0"
            };

            return oicTestStuffRegistration;
        }

        public static IRegistration NewOICMember(String email) {
            IRegistration newOICMember = new OICTestStuffRegistration(email);

            return newOICMember;
        }




        public static IRegistration FindPersonByIDCard(String idCard) {
            IRegistration personMember = new GeneralRegistration();
            using (RegistrationService.RegistrationServiceClient svc = new RegistrationService.RegistrationServiceClient()) 
            {
                DTO.ResponseService<DTO.Registration> res = svc.GetByIdCard(idCard);
                if (res.IsError)
                    throw new RegistrationException(Resources.errorRegistrationFactory_001);

                personMember = res.DataResponse.ConvertToPersonRegisteration();

                //IList<DTO.RegistrationAttatchFile> attachFiles = svc.
            }
           
           


            return personMember;
        }
    }
}
