using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IAS.DAL
{
    public partial class AG_IAS_APPLICANT_SCORE_D_TEMP
    {
        //string _InsurCompName;
        //public string InsurCompName
        //{
        //    get
        //    {
        //        return _InsurCompName;
        //    }
        //    set
        //    {
        //        this._InsurCompName = value;
        //    }
        //}
        string _InsurCompName;
         [DataMemberAttribute()]
        public global::System.String InsurCompName
        {
            get
            {
                return _InsurCompName;
            }
            set
            {
                this._InsurCompName = value;
            }
        }

        string _Associate_name;
        [DataMemberAttribute()]
        public global::System.String AssociateName
        {
            get
            {
                return _Associate_name;
            }
            set
            {
                _Associate_name = value;
            }
        }

        string _LicenseTypeCode;
        [DataMemberAttribute()]
        public global::System.String LicenseTypeCode
        {
            get
            {
                return _LicenseTypeCode;
            }
            set
            {
                _LicenseTypeCode = value;
            }
        }

        string _ProvinceCode;
        [DataMemberAttribute()]
        public global::System.String ProvinceCode
        {
            get
            {
                return _ProvinceCode;
            }
            set
            {
                _ProvinceCode = value;
            }
        }

        string _AssociateCode;
        [DataMemberAttribute()]
        public global::System.String AssociateCode
        {
            get
            {
                return _AssociateCode;
            }
            set
            {
                _AssociateCode = value;
            }
        }

        string _TestingDate;
        [DataMemberAttribute()]
        public global::System.String TestingDate
        {
            get
            {
                return _TestingDate;
            }
            set
            {
                _TestingDate = value;
            }
        }

        string _ApplicantCode;
        [DataMemberAttribute()]
        public global::System.String ApplicantCode
        {
            get
            {
                return _ApplicantCode;
            }
            set
            {
                _ApplicantCode = value;
            }
        }

        string _TimeCode;
        [DataMemberAttribute()]
        public global::System.String TimeCode
        {
            get
            {
                return _TimeCode;
            }
            set
            {
                _TimeCode = value;
            }
        }
    }
}
