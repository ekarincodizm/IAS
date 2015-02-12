using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DAL.Interfaces;

namespace IAS.DataServices.Exam.Helpers
{
    public class DataItemLicenseTypeFactory
    {
        public static IEnumerable<DTO.DataItem> GetLicenseTypeItem(IIASPersonEntities ctx, DTO.MemberType memberType, String compcode)
        {
            //DTO.MemberType memberType = (DTO.MemberType)userProfile.MemberType;
            //String compcode = userProfile.CompCode;

            switch (memberType)
            {
                case IAS.DTO.MemberType.General:
                case IAS.DTO.MemberType.OIC:
                case IAS.DTO.MemberType.OICAgent:
                    return ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.ACTIVE_FLAG == "Y").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                .Select(s => new DTO.DataItem
                                                                {
                                                                    Id = s.LICENSE_TYPE_CODE,
                                                                    Name = s.LICENSE_TYPE_NAME
                                                                }).ToList();


                case IAS.DTO.MemberType.Insurance:
                    if (String.IsNullOrWhiteSpace(compcode))
                    {
                        throw new ApplicationException("ไม่พบรหัสบริษัทประกันภัย");
                    }

                    VW_AS_COMPANY_T company = ctx.VW_AS_COMPANY_T.SingleOrDefault(c => c.COMP_CODE == compcode);
                    if (company == null)
                        throw new ApplicationException("ไม่พบรหัสบริษัทประกันภัย");


                    if (compcode.StartsWith("1"))
                    {
                        return ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "01" ||
                                                                       w.LICENSE_TYPE_CODE == "07").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                       .Select(s => new DTO.DataItem
                                                                       {
                                                                           Id = s.LICENSE_TYPE_CODE,
                                                                           Name = s.LICENSE_TYPE_NAME
                                                                       }).ToList();


                    }
                    else if (compcode.StartsWith("2"))
                    {
                        return ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "02" ||
                                                                   w.LICENSE_TYPE_CODE == "05" ||
                                                                   w.LICENSE_TYPE_CODE == "06" ||
                                                                   w.LICENSE_TYPE_CODE == "08").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                   .Select(s => new DTO.DataItem
                                                                   {
                                                                       Id = s.LICENSE_TYPE_CODE,
                                                                       Name = s.LICENSE_TYPE_NAME
                                                                   }).ToList();


                    }
                    else if (compcode.StartsWith("3"))
                    {
                        return ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "03" ||
                                                                   w.LICENSE_TYPE_CODE == "04" ||
                                                                   w.LICENSE_TYPE_CODE == "11" ||
                                                                   w.LICENSE_TYPE_CODE == "12").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                   .Select(s => new DTO.DataItem
                                                                   {
                                                                       Id = s.LICENSE_TYPE_CODE,
                                                                       Name = s.LICENSE_TYPE_NAME
                                                                   }).ToList();

                    }
                    else
                    {
                        return new List<DTO.DataItem>();
                    }



                case IAS.DTO.MemberType.Association:
                case IAS.DTO.MemberType.TestCenter:

                    if (String.IsNullOrWhiteSpace(compcode))
                    {
                        throw new ApplicationException("ไม่พบรหัสสมาคม");
                    }
                    AG_IAS_ASSOCIATION assocition = ctx.AG_IAS_ASSOCIATION.SingleOrDefault(a => a.ASSOCIATION_CODE == compcode);
                    if (assocition == null)
                        throw new ApplicationException("ไม่พบรหัสสมาคม");

                    return ctx.AG_IAS_ASSOCIATION_LICENSE.Join(ctx.AG_IAS_LICENSE_TYPE_R,
                                                                s => s.LICENSE_TYPE_CODE,
                                                                c => c.LICENSE_TYPE_CODE,
                                                                (s, c) => new { s, c })
                                                                .Where(sc => sc.s.ASSOCIATION_CODE == compcode && sc.s.ACTIVE == "Y")
                                                                .Select(sc => new DTO.DataItem
                                                                {
                                                                    Id = sc.s.LICENSE_TYPE_CODE,
                                                                    Name = sc.c.LICENSE_TYPE_NAME
                                                                }).ToList();
                case IAS.DTO.MemberType.OICFinance:
                    return new List<DTO.DataItem>();


                default:
                    return new List<DTO.DataItem>();
            }
        }
    }
}