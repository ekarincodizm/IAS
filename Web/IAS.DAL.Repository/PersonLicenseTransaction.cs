using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class PersonLicenseTransaction
    {
        //AG_IAS_LICENSE_H
        public string UPLOAD_GROUP_NO { get; set; }
        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
        public DateTime? TRAN_DATE { get; set; }
        public decimal? LOTS { get; set; }
        public decimal? MONEY { get; set; }
        public string REQUEST_NO { get; set; }
        public string PAYMENT_NO { get; set; }
        public string FLAG_REQ { get; set; }
        public string LICENSE_TYPE_CODE { get; set; }
        public string FILENAME { get; set; }
        public string PETITION_TYPE_CODE { get; set; }
        public string UPLOAD_BY_SESSION { get; set; }
        public string FLAG_LIC { get; set; }
        public string APPROVE_COMPCODE { get; set; }
        public string APPROVED_DOC { get; set; }
        public string APPROVED_DATE { get; set; }
        public string APPROVED_BY { get; set; }


        //AG_IAS_LICENSE_D
        public string SEQ_NO { get; set; }
        public string ORDERS { get; set; }
        public string LICENSE_NO { get; set; }
        public DateTime? LICENSE_DATE { get; set; }
        public DateTime? LICENSE_EXPIRE_DATE { get; set; }
        public decimal? FEES { get; set; }
        public string ID_CARD_NO { get; set; }
        public string RENEW_TIMES { get; set; }
        public string TITLE_NAME { get; set; }
        public string NAMES { get; set; }
        public string LASTNAME { get; set; }
        public string ADDRESS_1 { get; set; }
        public string ADDRESS_2 { get; set; }
        public string AREA_CODE { get; set; }
        public string CURRENT_ADDRESS_1 { get; set; }
        public string CURRENT_ADDRESS_2 { get; set; }
        public string CURRENT_AREA_CODE { get; set; }
        public string EMAIL { get; set; }
        public string AR_DATE { get; set; }
        public string OLD_COMP_CODE { get; set; }
        public string ERR_DESC { get; set; }
        public string APPROVED { get; set; }
        public string PRE_NAME_CODE { get; set; }


        //AG_IAS_SUBPAYMENT_H_T
        public string HEAD_REQUEST_NO { get; set; }
        


        //AG_IAS_SUBPAYMENT_D_T
        public string GROUP_REQUEST_NO { get; set; }
        public DateTime? RECEIPT_DATE { get; set; }


        //AG_IAS_LICENSE_TYPE_R
        public string LICENSE_TYPE_NAME { get; set; }

        //AG_IAS_PETITION_TYPE_R
        public string PETITION_TYPE_NAME { get; set; }


        //AG_LICENSE_RENEW_T
        public int RENEW_TIME { get; set; }

        //AG_AGENT_T
        public string INSURANCE_COMP_CODE { get; set; }
        public DateTime? RENEW_DATE { get; set; }
        public DateTime? EXPIRE_DATE { get; set; }

        //AG_LICENSE_T
        public string REVOKE_TYPE_CODE { get; set; }
        public DateTime? REVOKE_LICENSE_DATE { get; set; }

        public string RUN_NO { get; set; }

    }
}
