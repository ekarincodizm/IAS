using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class ValidateApplicantSingleBeforeSubmitRequest
    {
        public IEnumerable<DTO.ApplicantTemp> Applicants { get; set; }
    }
}
