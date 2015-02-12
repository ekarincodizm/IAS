using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantDisplayDetail : Applicant
    {
        string _educationName;
        public string EducationName
        {
            get
            {
                return this._educationName;
            }
            set
            {
                this._educationName = value;
            }
        }

        string _titleName;
        public string TitleName
        {
            get
            {
                return this._titleName;
            }
            set
            {
                this._titleName = value;
            }
        }

        DateTime? _testingDate;
        public DateTime? TestingDate
        {
            get
            {
                return this._testingDate;
            }
            set
            {
                this._testingDate = value;
            }
        }
    }
}
