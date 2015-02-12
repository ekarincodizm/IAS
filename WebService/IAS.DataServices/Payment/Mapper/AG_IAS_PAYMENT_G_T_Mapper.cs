using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.DataServices.Payment.Mapper
{
    public static class AG_IAS_PAYMENT_G_T_Mapper
    {
        public static IEnumerable<DTO.PaymentByRangeResult> ConvertToPaymentByRangeResults(this IEnumerable<DAL.AG_IAS_PAYMENT_G_T> payment_gts) {
            IList<DTO.PaymentByRangeResult> paymentByRangeResults = new List<DTO.PaymentByRangeResult>();
            foreach (DAL.AG_IAS_PAYMENT_G_T item in payment_gts)
            {
                paymentByRangeResults.Add(new DTO.PaymentByRangeResult()
                {         
                    Id = item.GROUP_REQUEST_NO,
                    PaymentRefNo = item.GROUP_REQUEST_NO,
                    CreateDate = ((DateTime)item.CREATED_DATE).ToString("dd/MM/yyyy"),
                    PaymentAmount = ((Decimal)item.GROUP_AMOUNT).ToString("#,##0.00"),
                    Status = item.STATUS
                });

            }
            return paymentByRangeResults;
        }
    }
}