﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.BLL.RegistrationIAS
{
    public class RegistrationException : Exception
    {
        public RegistrationException(String message)
            :base(message)
        {

        }
    }
}