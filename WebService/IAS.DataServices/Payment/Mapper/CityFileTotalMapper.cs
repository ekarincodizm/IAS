using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.Payment.TransactionBanking;
using IAS.DataServices.Payment.TransactionBanking.Citi;

namespace IAS.DataServices.Payment.Mapper
{
    public static class CityFileTotalMapper
    {
        public static AG_IAS_TEMP_PAYMENT_TOTAL GetAG_IAS_TEMP_PAYMENT_TOTAL
                                        (this CityFileHeader cityFileheader ) 
        {
            IEnumerable<CityFileTotal> cityFileTotals = cityFileheader.CityFileTotals;

            AG_IAS_TEMP_PAYMENT_TOTAL ag_Total = new AG_IAS_TEMP_PAYMENT_TOTAL() 
            {
                ID=cityFileheader.ID,
                HEADER_ID = cityFileheader.ID,
                RECORD_TYPE= cityFileTotals.FirstOrDefault().RECORD_TYPE,
                COMPANY_ACCOUNT = cityFileTotals.FirstOrDefault().COMPANY_ACCOUNT,
                TOTAL_CREDIT_AMOUNT = cityFileTotals.Sum(a=> Convert.ToDecimal( a.TOTAL_CREDIT_AMOUNT)).ToString()
            };

            return ag_Total;
        }
    }
}