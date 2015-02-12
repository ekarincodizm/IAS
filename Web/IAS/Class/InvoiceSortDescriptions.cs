using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Class
{
    public class InvoiceSortDescriptions
    {
        public List<DTO.OrderInvoice> PaymentSortDesc(List<DTO.OrderInvoice> ls, int CurrentIndex, String Mode)
        {
            if (Mode.Equals("Up"))
            {
                //Get Current & Previous Data
                DTO.OrderInvoice cur = ls[CurrentIndex];
                DTO.OrderInvoice pre = ls[CurrentIndex - 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = pre.RUN_NO;
                pre.IndexOfGroup = order;

                //Resort
                ls[CurrentIndex] = pre;
                ls[CurrentIndex - 1] = cur;

                foreach (DTO.OrderInvoice item in ls)
                {
                    var AllOrder = ls.SingleOrDefault(a => a.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO);
                    AllOrder.RUN_NO = Convert.ToString(ls.IndexOf(AllOrder) + 1);
                }
            }
            else if (Mode.Equals("Down"))
            {
                //Get Current & Previous Data

                DTO.OrderInvoice cur = ls[CurrentIndex];
                DTO.OrderInvoice next = ls[CurrentIndex + 1];

                int order = Convert.ToInt32(cur.IndexOfGroup);
                cur.RUN_NO = next.RUN_NO;
                next.RUN_NO = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = next;
                ls[CurrentIndex + 1] = cur;
                foreach (DTO.OrderInvoice item in ls)
                {
                
                    var AllOrder = ls.SingleOrDefault(a => a.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO);
                    AllOrder.RUN_NO = Convert.ToString(ls.IndexOf(AllOrder) + 1);
                }
            }

            return ls.OrderBy(x => x.RUN_NO).ToList();

        }

        public List<DTO.PersonLicenseTransaction> SortDescriptionsDT(List<DTO.PersonLicenseTransaction> ls, int CurrentIndex, String Mode)
        {
            foreach (DTO.PersonLicenseTransaction item in ls)
            {
                var AllOrder = ls.FirstOrDefault(a => a.UPLOAD_GROUP_NO == item.UPLOAD_GROUP_NO);
                AllOrder.RUN_NO = Convert.ToString(ls.IndexOf(AllOrder) + 1);
            }

            if (Mode.Equals("Up"))
            {
                //Get Current & Previous Data
                DTO.PersonLicenseTransaction cur = ls[CurrentIndex];
                DTO.PersonLicenseTransaction pre = ls[CurrentIndex - 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = pre.RUN_NO;
                pre.RUN_NO = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = pre;
                ls[CurrentIndex - 1] = cur;
            }
            else if (Mode.Equals("Down"))
            {
                //Get Current & Next Data
                DTO.PersonLicenseTransaction cur = ls[CurrentIndex];
                DTO.PersonLicenseTransaction next = ls[CurrentIndex + 1];

                int order = Convert.ToInt32(cur.RUN_NO);
                cur.RUN_NO = next.RUN_NO;
                next.RUN_NO = Convert.ToString(order);

                //Resort
                ls[CurrentIndex] = next;
                ls[CurrentIndex + 1] = cur;
            }

            return ls.OrderBy(idx => idx.RUN_NO).ToList();

        }
    }
}