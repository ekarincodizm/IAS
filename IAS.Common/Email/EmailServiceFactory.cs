using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Common.Email
{
    public class EmailServiceFactory
    {
        private static IEmailService _emailService ;

        public static void InitializeEmailServiceFactory(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public static IEmailService GetEmailService()
        {

            if (_emailService == null)
                _emailService = StructureMap.ObjectFactory.GetInstance<IEmailService>();
            return _emailService;
        }
    }

}
