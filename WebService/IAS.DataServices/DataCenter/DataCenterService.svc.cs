using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Collections.ObjectModel;
using IAS.DAL;
using IAS.Utils;
using System.Transactions;
using System.IO;
using IAS.DataServices.Properties;
using System.ServiceModel.Activation;
using IAS.DTO;
using IAS.Common.Logging;
using DataItem = IAS.DTO.DataItem;

namespace IAS.DataServices.DataCenter
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DataCenterService : AbstractService, IDataCenterService, IDisposable
    {

        //private PersonEntities ctx = null;

        public DataCenterService()
        {
            //ctx = DALFactory.GetPersonContext();
        }

        private void AddFirstItem(List<DTO.DataItem> list, string firstItem)
        {
            if (!string.IsNullOrEmpty(firstItem))
                list.Insert(0, new DTO.DataItem { Id = string.Empty, Name = firstItem });
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetTitleName(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                //var ls = ctx.VW_IAS_TITLE_NAME.ToList();
                var ls = ctx.VW_IAS_TITLE_NAME_PRIORITY.ToList();
                List<DTO.DataItem> list = new List<DTO.DataItem>();
                ls.ForEach(l => list.Add(new DTO.DataItem
                {
                    Id = l.ID.ToString(),
                    Name = l.NAME
                }));

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetTitleName", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetTitleNameById(int Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.VW_IAS_TITLE_NAME.SingleOrDefault(s => s.ID == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.ID.ToString(), Name = ent.NAME };
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetTitleNameById", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetMemberTypeById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.AG_IAS_MEMBER_TYPE.SingleOrDefault(s => s.MEMBER_CODE == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.MEMBER_CODE.ToString(), Name = ent.MEMBER_NAME };
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetMemberTypeById", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetProvince(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.VW_IAS_PROVINCE
                              .Select(s => new DTO.DataItem
                                        {
                                            Id = s.ID,
                                            Name = s.NAME
                                        })
                              .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetProvince", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetAmpur(string firstItem, string provinceCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.VW_IAS_AMPUR
                              .Where(w => w.PROVINCECODE == provinceCode)
                              .Select(s => new DTO.DataItem
                              {
                                  Id = s.ID,
                                  Name = s.NAME
                              })
                              .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAmpur", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetTumbon(string firstItem, string provinceCode, string ampurCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.VW_IAS_TUMBON
                              .Where(w => w.PROVINCECODE == provinceCode &&
                                          w.AMPURCODE == ampurCode)
                              .Select(s => new DTO.DataItem
                              {
                                  Id = s.ID,
                                  Name = s.NAME
                              })
                              .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetTumbon", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetProvinceById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {

                var ent = ctx.VW_IAS_PROVINCE
                              .SingleOrDefault(s => s.ID == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.ID, Name = ent.NAME };
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetProvinceById", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetAmpurById(string provinceId, string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.VW_IAS_AMPUR
                                .SingleOrDefault(s => s.PROVINCECODE == provinceId && s.ID == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.ID, Name = ent.NAME };
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAmpurById", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetTumbonById(string provinceId, string ampurId, string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.VW_IAS_TUMBON
                                .SingleOrDefault(s => s.AMPURCODE == ampurId &&
                                                      s.ID == Id &&
                                                      s.PROVINCECODE == provinceId);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.ID, Name = ent.NAME };
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetTumbonById", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetEducation(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                IEnumerable<DTO.DataItem> list = ctx.AG_EDUCATION_R.Select(s => new DTO.DataItem
                {
                    Id = s.EDUCATION_CODE,
                    Name = s.EDUCATION_NAME
                });
                List<DTO.DataItem> listsort = list.OrderBy(a => a.Name).ToList();
                AddFirstItem(listsort, firstItem);

                res.DataResponse = listsort;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetEducation", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetEducationById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {

                var ent = ctx.AG_EDUCATION_R.SingleOrDefault(s => s.EDUCATION_CODE == Id);
                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.EDUCATION_CODE, Name = ent.EDUCATION_NAME };
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetEducationById", ex);
            }

            return res;
        }
        public DTO.ResponseService<List<DTO.DataItem>> GetDocumentTypeDeleted(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = ctx.AG_IAS_DOCUMENT_TYPE.Where(d => d.STATUS == "D").OrderBy(x => x.DOCUMENT_CODE).Select(s => new DTO.DataItem
                {
                    Id = s.DOCUMENT_CODE,
                    Name = s.DOCUMENT_NAME

                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetDocumentTypeDeleted", ex);
            }

            return res;
        }
        public DTO.ResponseService<List<DTO.DataItem>> GetDocumentType(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = ctx.AG_IAS_DOCUMENT_TYPE.Where(d => d.STATUS == "A").OrderBy(x => x.DOCUMENT_CODE).Select(s => new DTO.DataItem
                {
                    Id = s.DOCUMENT_CODE,
                    Name = s.DOCUMENT_NAME,
                    TRAIN_DISCOUNT_STATUS = s.TRAIN_DISCOUNT_STATUS,
                    EXAM_DISCOUNT_STATUS = s.EXAM_DISCOUNT_STATUS

                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetDocumentType", ex);
            }

            return res;
        }
        public DTO.ResponseService<List<DTO.DataItem>> GetDocumentTypeAll(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = ctx.AG_IAS_DOCUMENT_TYPE.OrderBy(x => x.DOCUMENT_CODE).Select(s => new DTO.DataItem
                {
                    Id = s.DOCUMENT_CODE,
                    Name = s.DOCUMENT_NAME

                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetDocumentTypeAll", ex);
            }

            return res;
        }
        public DTO.ResponseService<List<DTO.DataItem>> GetDocumentTypeIsImage()
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.AG_IAS_DOCUMENT_TYPE
                              .Where(w => w.IS_CARD_PIC == "Y")
                              .Select(s => new DTO.DataItem
                                    {
                                        Id = s.DOCUMENT_CODE,
                                        Name = s.DOCUMENT_NAME
                                    }).ToList();

                AddFirstItem(list, "");

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetDocumentTypeIsImage", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetStatus(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.AG_IAS_STATUS.Select(s => new DTO.DataItem
                {
                    Id = s.STATUS_CODE,
                    Name = s.STATUS_NAME
                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetStatus", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetMemberType(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.AG_IAS_MEMBER_TYPE.Select(s => new DTO.DataItem
                {
                    Id = s.MEMBER_CODE,
                    Name = s.MEMBER_NAME
                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetMemberType", ex);
            }

            return res;
        }
        #region milk
        public DTO.ResponseService<List<DTO.DataItem>> GetMemberTypeNotOIC_for_regSearchOfficerOIC(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.AG_IAS_MEMBER_TYPE
                    .Select(s => new DTO.DataItem
                {
                    Id = s.MEMBER_CODE,
                    Name = s.MEMBER_NAME
                }).ToList();

                list = list.Where(l => l.Id == "1" || l.Id == "2" || l.Id == "3").ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list.ToList();

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetMemberTypeNotOIC_for_regSearchOfficerOIC", ex);
            }

            return res;
        }
        #endregion

        public DTO.ResponseService<List<DTO.DataItem>> GetMemberTypeNotOIC(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.AG_IAS_MEMBER_TYPE.Select(s => new DTO.DataItem
                {
                    Id = s.MEMBER_CODE,
                    Name = s.MEMBER_NAME
                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list.Where(l => !l.Name.Contains("คปภ.")).ToList(); ;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetMemberTypeNotOIC", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetOICType(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.AG_IAS_OIC_TYPE.Select(s => new DTO.DataItem
                {
                    Id = s.OIC_CODE,
                    Name = s.OIC_NAME
                }).ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetOICType", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetNationality(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.AG_IAS_NATIONALITY.Select(s => new DTO.DataItem
                {
                    Id = s.NATIONALITY_CODE,
                    Name = s.NATIONALITY_NAME
                }).OrderBy(o => o.Name)
                  .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetNationality", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetNationalityById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {

                var ent = ctx.AG_IAS_NATIONALITY.SingleOrDefault(s => s.NATIONALITY_CODE == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.NATIONALITY_CODE, Name = ent.NATIONALITY_NAME };
                }

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetNationalityById", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetCompanyCode(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = ctx.VW_IAS_COM_CODE.Select(s => new DTO.DataItem
                {
                    Id = s.ID,
                    Name = s.NAME
                }).OrderBy(o => o.Name)
                  .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyCode", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<string>> GetCompanyByName(string anyName)
        {
            DTO.ResponseService<List<string>> res = new DTO.ResponseService<List<string>>();

            try
            {

                var list = ctx.VW_IAS_COM_CODE.Where(w => w.NAME.Contains(anyName)).Select(s => s.NAME + " [" + s.ID + "]").ToList();
                res.DataResponse = list;
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyByName", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetCompanyNameById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.ID, Name = ent.NAME + " [" + ent.ID + "]" };
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyNameById", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetCompanyCodeById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.VW_IAS_COM_CODE.SingleOrDefault(s => s.ID == Id);

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem { Id = ent.ID, Name = ent.NAME };
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyCodeById", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<string>> GetCompanyCodeByName(string anyText)
        {
            DTO.ResponseService<List<string>> res = new DTO.ResponseService<List<string>>();

            try
            {
                var list = ctx.VW_IAS_COM_CODE
                              .Where(w => w.NAME.Contains(anyText))
                              .Select(s => s.NAME + " [" + s.ID + "]").ToList();

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyCodeByName", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.ApproveConfig>> GetApproveConfig()
        {
            var res = new DTO.ResponseService<List<DTO.ApproveConfig>>();

            try
            {
                var list = ctx.AG_IAS_APPROVE_CONFIG
                              .Select(s => new DTO.ApproveConfig
                                  {
                                      ID = s.ID,
                                      ITEM = s.ITEM,
                                      ITEM_VALUE = s.ITEM_VALUE,
                                      DESCRIPTION = s.DESCRIPTION,
                                  })
                              .ToList();

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetApproveConfig", ex);
            }

            return res;

        }

        public DTO.ResponseService<DTO.ResponseMessage<bool>> SetApproveConfig(List<DTO.ApproveConfig> cfgList)
        {
            DTO.ResponseService<DTO.ResponseMessage<bool>> res =
                new DTO.ResponseService<DTO.ResponseMessage<bool>>
                {
                    DataResponse = new DTO.ResponseMessage<bool>
                                            {
                                                ResultMessage = false
                                            }
                };
            try
            {
                for (int i = 0; i < cfgList.Count; i++)
                {
                    string id = cfgList[i].ID;
                    string newValue = cfgList[i].ITEM_VALUE;

                    AG_IAS_APPROVE_CONFIG ent = ctx.AG_IAS_APPROVE_CONFIG
                                                   .SingleOrDefault(s => s.ID == id);
                    if (ent != null)
                    {
                        ent.ITEM_VALUE = newValue;
                        ent.USER_DATE = DateTime.Now;
                    }
                }

                using (var ts = new TransactionScope())
                {
                    ctx.SaveChanges();
                    ts.Complete();
                    res.DataResponse.ResultMessage = true;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_SetApproveConfig", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetCompanyByRequest(string compCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list =
                    (from LD in ctx.AG_IAS_LICENSE_D
                     join LH in ctx.AG_IAS_LICENSE_H on LD.UPLOAD_GROUP_NO equals LH.UPLOAD_GROUP_NO
                     join LR in ctx.AG_LICENSE_TYPE_R on LH.LICENSE_TYPE_CODE equals LR.LICENSE_TYPE_CODE
                     where LH.APPROVE_COMPCODE == compCode
                     group new { LD, LH, LR } by new
                     {
                         LH.COMP_CODE,
                         LH.COMP_NAME
                     } into a
                     select new DTO.DataItem
                     {
                         Id = a.Key.COMP_CODE,
                         Name = a.Key.COMP_NAME
                     }).ToList();

                List<DTO.DataItem> lsGroup = list.Distinct().ToList();
                AddFirstItem(lsGroup, "ทั้งหมด");

                res.DataResponse = lsGroup;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyByRequest", ex);
            }

            return res;
        }

        //New Method

        public DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceGroup(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.AG_EXAM_PLACE_GROUP_R
                              .Where(s => s.ACTIVE == "Y")
                              .Select(s => new DTO.DataItem
                              {
                                  Id = s.EXAM_PLACE_GROUP_CODE,
                                  Name = s.EXAM_PLACE_GROUP_NAME
                              })
                              .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamPlaceGroup", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamPlace(string firstItem, string groupCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.AG_EXAM_PLACE_R
                              .Where(w => w.EXAM_PLACE_GROUP_CODE == groupCode && w.ACTIVE == "Y")
                              .Select(s => new DTO.DataItem
                              {
                                  Id = s.EXAM_PLACE_CODE,
                                  Name = s.EXAM_PLACE_NAME
                              })
                              .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamPlace", ex);
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetExamPlace_AndProvince(string groupCode)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = "select pr.exam_place_code Id,'[' || pro.name || '] ' || pr.exam_place_name Name  from AG_EXAM_PLACE_R PR,vw_ias_province Pro where PR.EXAM_PLACE_GROUP_CODE ='" + groupCode + "' and pro.id = pr.PROVINCE_CODE and pr.active = 'Y'  order by pro.name";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamPlace_AndProvince", ex);
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetExamPlace_UnderAssocicate(string firstItem, string groupCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();

            try
            {
                string sql = "select s.EXAM_PLACE_CODE Id ,'[' || e.NAME || '] ' || s.EXAM_PLACE_NAME  as name from AG_EXAM_PLACE_R s , VW_IAS_PROVINCE e where e.ID = s.PROVINCE_CODE and s.association_code ='" + groupCode + "' and s.active ='Y' order by e.NAME asc ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamPlace_UnderAssocicate", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamTime(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.AG_EXAM_TIME_R.Where(w => w.ACTIVE == "Y")
                              .Select(s => new DTO.DataItem
                              {
                                  Id = s.TEST_TIME_CODE,
                                  Name = s.TEST_TIME
                              })
                              .ToList();

                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamTime", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetLicenseType(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                //TODO: ดึง AG_LICENSE_TYPE_R เข้า Model ก่อน
                var list = base.ctx.AG_IAS_LICENSE_TYPE_R
                               .Select(s => new DTO.DataItem
                              {
                                  Id = s.LICENSE_TYPE_CODE,
                                  Name = s.LICENSE_TYPE_NAME
                              })
                              .ToList();


                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetLicenseType", ex);
            }

            return res;
        }

        //Gen Insurance Associate List
        public List<DTO.InsuranceAssociate> GetInsuranceAssociateList()
        {

            //var Comlist = (from vw in base.ctx.VW_IAS_COM_CODE
            //               select new DTO.InsuranceAssociate
            //               {
            //                   Id = vw.ID,
            //                   Name = vw.NAME
            //               }).ToList();

            // update 20130909 13:52 by Tik 
            var Comlist = (from ap in base.ctx.AG_EXAM_PLACE_GROUP_R
                           select new DTO.InsuranceAssociate
                           {
                               Id = ap.EXAM_PLACE_GROUP_CODE,
                               Name = ap.EXAM_PLACE_GROUP_NAME
                           }).ToList();
            return Comlist;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByAsso(string firstItem, string Asso)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                string sql = " select distinct R.LICENSE_TYPE_CODE ID,R.LICENSE_TYPE_NAME Name " +
                              "  from ag_ias_association_license ls,ag_ias_license_type_r R " +
                              "  where r.license_type_code = ls.license_type_code " +
                              "  and r.active_flag='Y' and ls.active='Y' " +
                              "  and ls.association_code like '" + Asso + "' ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                if (dt != null)
                {
                    List<DTO.DataItem> list = new List<DTO.DataItem>();

                    list.Add(new DTO.DataItem { Id = "", Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                    res.DataResponse = list;
                }

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetLicenseTypeByAsso", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetAssociate(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            res.DataResponse = new List<DTO.DataItem>();
            try
            {
                //Get Insurance Associate List
                var assoList = GetInsuranceAssociateList();

                if (!string.IsNullOrEmpty(firstItem))
                    res.DataResponse.Insert(0, new DTO.DataItem { Id = "", Name = firstItem });

                foreach (DTO.InsuranceAssociate asso in assoList)
                {
                    res.DataResponse.Add(new DTO.DataItem { Id = asso.Id, Name = asso.Name });
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAssociate", ex);
            }
            return res;
        }


        public DTO.ResponseService<List<string>> GetAssociateCodeByName(string anyText)
        {
            DTO.ResponseService<List<string>> res = new DTO.ResponseService<List<string>>();

            try
            {
                //Get Insurance Associate List
                var assoList = GetInsuranceAssociateList();

                var list = assoList
                             .Where(w => w.Name.StartsWith(anyText))
                             .Select(s => s.Name + " [" + s.Id + "]").ToList();

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAssociateCodeByName", ex);
            }

            return res;
        }

        public DTO.ResponseService<DTO.DataItem> GetAssociateNameById(string Id)
        {
            DTO.ResponseService<DTO.DataItem> res = new DTO.ResponseService<DTO.DataItem>();

            try
            {
                var ent = ctx.AG_IAS_ASSOCIATION.SingleOrDefault(s => s.ASSOCIATION_CODE == Id && s.ACTIVE == "Y");

                if (ent != null)
                {
                    res.DataResponse = new DTO.DataItem
                    {
                        Id = ent.ASSOCIATION_CODE,
                        Name = ent.ASSOCIATION_NAME + " [" + ent.ASSOCIATION_CODE + "]"
                    };
                }
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAssociateNameById", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetPaymentType(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            string text = firstItem;
            try
            {
                string sql = "SELECT PETITION_TYPE_CODE Id, PETITION_TYPE_NAME Name " +
                             "FROM AG_PETITION_TYPE_R " +
                             "WHERE PETITION_TYPE_CODE in ('01','11','13','14','15','16','17','18') " +
                             "ORDER BY PETITION_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                if (dt != null)
                {
                    List<DTO.DataItem> list = new List<DTO.DataItem>();

                    list.Add(new DTO.DataItem { Id = "00", Name = text });

                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                    res.DataResponse = list;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetPaymentType", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetRequestLicenseType(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                string sql = "SELECT PETITION_TYPE_CODE Id, PETITION_TYPE_NAME Name " +
                             "FROM AG_IAS_PETITION_TYPE_R " +
                             "ORDER BY PETITION_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                res.DataResponse = new List<DTO.DataItem>();

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(firstItem))
                        res.DataResponse.Add(new DTO.DataItem { Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        res.DataResponse.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetRequestLicenseType", ex);
            }
            return res;
        }



        public DTO.ResponseService<List<DTO.DataItem>> GetPettitionTypebyLicenseType(string firstItem, string licenseType)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            string sql;
            try
            {

                var agenType = ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());


                sql = "SELECT PETITION_TYPE_CODE Id, PETITION_TYPE_NAME Name " +
                      "FROM AG_IAS_PETITION_TYPE_R ";
                if (agenType.AGENT_TYPE == "B")
                {
                    if (licenseType == "03" || licenseType == "04")
                    {
                        sql += "WHERE PETITION_TYPE_CODE not in ('17','18','01','44') ";
                    }
                    else if (licenseType == "11" || licenseType == "12")
                    {
                        sql += "WHERE PETITION_TYPE_CODE = '11' ";
                    }
                }
                else
                {
                    sql += "WHERE PETITION_TYPE_CODE not in ('01','44') ";
                }

                sql += "ORDER BY PETITION_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                res.DataResponse = new List<DTO.DataItem>();

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(firstItem))
                        res.DataResponse.Add(new DTO.DataItem { Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        res.DataResponse.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetPettitionTypebyLicenseType", ex);
            }
            return res;



        }

        public DTO.ResponseService<List<DTO.DataItem>> GetRequestLicenseType_NOias(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                string sql = "SELECT PETITION_TYPE_CODE Id, PETITION_TYPE_NAME Name " +
                             "FROM AG_PETITION_TYPE_R " +
                             "WHERE PETITION_TYPE_CODE IN ('01','16','11','15','13','14','17','18') " +
                             "ORDER BY PETITION_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                res.DataResponse = new List<DTO.DataItem>();

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(firstItem))
                        res.DataResponse.Add(new DTO.DataItem { Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        res.DataResponse.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetRequestLicenseType_NOias", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config อนุมัติสมัครสมาชิก
        /// </summary>
        /// <returns>ResponseService<List<ConfigEntity>></returns>
        public DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigApproveMember()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigEntity>>();
            try
            {
                var list = base.ctx.AG_IAS_APPROVE_CONFIG
                               .Where(s => s.ITEM_TYPE == "01" &&
                                           "01_02_03".Contains(s.ITEM_TYPE) && !s.KEYWORD.Contains("General"))
                               .Select(s => new DTO.ConfigEntity
                                {
                                    Id = s.ID,
                                    Name = s.ITEM,
                                    Value = s.ITEM_VALUE,
                                    Description = s.DESCRIPTION
                                }).ToList();

                var lsGeneral = base.ctx.AG_IAS_APPROVE_CONFIG
                               .Where(s => s.ITEM_TYPE == "01" && s.KEYWORD.Contains("General"))
                               .Select(s => new DTO.ConfigEntity
                               {
                                   Id = s.ID,
                                   Name = s.ITEM,
                                   Value = s.ITEM_VALUE,
                                   Description = s.DESCRIPTION,
                                   GROUP_CODE = s.KEYWORD
                               }).ToList();

                res.DataResponse = list.Union(lsGeneral).ToList();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigApproveMember", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config อนุมัติสมัครสมาชิก เพิ่มเติม
        /// </summary>
        public DTO.ResponseService<List<DTO.ConfigExtraEntity>> GetNewConfigApproveMember()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigExtraEntity>>();
            try
            {
                var list = (from cfg in base.ctx.AG_IAS_APPROVE_FIELD
                            select new DTO.ConfigExtraEntity
                            {
                                Id = cfg.APPEOVE_FIELD_ID,
                                Name = cfg.FIELD_NAME,
                                Value = cfg.STATUS,
                                Description = cfg.FIELD_NAME

                            }).ToList();


                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetNewConfigApproveMember", ex);

            }
            return res;
        }

        /// <summary>
        /// Update Config อนุมัติสมัครสมาชิก เพิ่มเติม
        /// </summary>
        public DTO.ResponseMessage<bool> UpdateNewConfigApproveMember(List<DTO.ConfigExtraEntity> config, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (DTO.ConfigExtraEntity cfg in config)
                    {
                        AG_IAS_APPROVE_FIELD cur = base.ctx.AG_IAS_APPROVE_FIELD.FirstOrDefault(fn => fn.FIELD_NAME.Trim() == cfg.Name);
                        if (cur != null)
                        {
                            cur.STATUS = cfg.Value;
                            cur.UPDATE_BY = userProfile.Name;
                            cur.UPDATE_DATE = DateTime.Now;
                        }

                    }

                    base.ctx.SaveChanges();
                    tran.Complete();
                    res.ResultMessage = true;
                };


            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_UpdateNewConfigApproveMember", ex);
            }
            return res;
        }



        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config ทั่วไป
        /// </summary>
        /// <returns>ResponseService<List<ConfigEntity>></returns>
        public DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigGeneral()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigEntity>>();
            try
            {
                //AG_IAS_APPROVE_CONFIG
                var list1 = base.ctx.AG_IAS_APPROVE_CONFIG
                               .Where(s => s.ITEM_TYPE == "02")
                               .Select(s => new DTO.ConfigEntity
                               {
                                   Id = s.ID,
                                   Name = s.ITEM,
                                   Value = s.ITEM_VALUE,
                                   Description = s.DESCRIPTION
                               }).ToList();
                //config viewFile
                var list2 = base.ctx.AG_IAS_CONFIG
                          .Where(s => (s.ITEM_TYPE == "02" && s.GROUP_CODE == "RC001") || s.ID.Equals("13"))
                          .Select(s => new DTO.ConfigEntity
                          {
                              Id = s.ID,
                              Name = s.ITEM,
                              Value = s.ITEM_VALUE,
                              Description = s.DESCRIPTION
                          }).ToList();
                res.DataResponse = list1.Union(list2).ToList();
                ;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigGeneral", ex);
            }
            return res;
        }

        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config ผู้ตรวจเอกสาร
        /// </summary>
        /// <returns>ResponseService<List<ConfigEntity>></returns>
        public DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigApproveDocument()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigEntity>>();
            try
            {
                var list = base.ctx.AG_IAS_APPROVE_DOC_TYPE
                               .OrderBy(o => o.APPROVE_DOC_TYPE)
                               .Select(s => new DTO.ConfigEntity
                               {
                                   Id = s.APPROVE_DOC_TYPE,
                                   Name = s.APPROVE_DOC_NAME,
                                   Value = s.APPROVER,
                                   Description = s.DESCRIPTION,
                                   Item_Value = s.ITEM_VALUE
                               }).ToList();
                res.DataResponse = list;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigApproveDocument", ex);
            }
            return res;
        }

        /// <summary>
        /// Update ข้อมูลตั้งค่า Config ผู้ตรวจเอกสาร
        /// </summary>
        /// <param name="Id">รหัสรายการ</param>
        /// <param name="value">ค่า</param>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateConfigApproveMember(List<DTO.ConfigEntity> configs)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (DTO.ConfigEntity conf in configs)
                {
                    if (conf.GROUP_CODE != null)
                    {
                        var entConfig = base.ctx.AG_IAS_CONFIG.SingleOrDefault(c => c.GROUP_CODE == conf.GROUP_CODE && c.ID == conf.Id);
                        if (entConfig != null)
                        {
                            entConfig.ITEM_VALUE = conf.Value;
                            entConfig.USER_DATE = DateTime.Now;
                        }
                    }
                    else
                    {
                        var ent = base.ctx.AG_IAS_APPROVE_CONFIG
                                   .SingleOrDefault(s => s.ID == conf.Id);

                        if (ent != null)
                        {
                            ent.ITEM_VALUE = conf.Value;
                            ent.USER_DATE = DateTime.Now;
                        }
                    }

                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_UpdateConfigApproveMember", ex);
            }
            return res;
        }


        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config เอกสารสมัครสมาชิก
        /// <FUNCTION_ID>40</FUNCTION_ID>
        /// </summary>
        /// <returns>ResponseService<List<ConfigEntity>></returns>
        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentConfigApproveMember()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                var list = (from at in base.ctx.AG_IAS_DOCUMENT_TYPE
                            join att in base.ctx.AG_IAS_DOCUMENT_TYPE_T on at.DOCUMENT_CODE equals att.DOCUMENT_CODE
                            join m in base.ctx.AG_IAS_MEMBER_TYPE on att.MEMBER_CODE equals m.MEMBER_CODE
                            where att.STATUS == "A" && att.FUNCTION_ID == "40"
                            orderby att.MEMBER_CODE descending
                            select new DTO.ConfigDocument
                            {
                                ID = att.ID,
                                FUNCTION_ID = att.FUNCTION_ID,
                                MEMBER_CODE = att.MEMBER_CODE,
                                DOCUMENT_CODE = att.DOCUMENT_CODE,
                                DOCUMENT_REQUIRE = att.DOCUMENT_REQUIRE,
                                DOCUMENT_NAME = at.DOCUMENT_NAME,
                                MEMBER_NAME = m.MEMBER_NAME
                            }).ToList();

                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetDocumentConfigApproveMember", ex);
            }

            return res;
        }

        /// <summary>
        /// Update ข้อมูลตั้งค่า Config ผู้ตรวจเอกสาร
        /// </summary>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateConfigApproveDocument(List<DTO.ConfigEntity> configs)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {

                foreach (DTO.ConfigEntity conf in configs)
                {
                    var ent = base.ctx.AG_IAS_APPROVE_DOC_TYPE
                                      .SingleOrDefault(s => s.APPROVE_DOC_TYPE == conf.Id);

                    ent.APPROVER = conf.Value;
                    ent.ITEM_VALUE = conf.Item_Value;
                    base.ctx.SaveChanges();
                }
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_UpdateConfigApproveDocument", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<decimal> GetPetitionFee(string petitionTypeCode)
        {
            var res = new DTO.ResponseMessage<decimal>();
            res.ResultMessage = 0m;
            try
            {
                var ent = base.ctx.AG_PETITION_TYPE_R
                                  .Where(w => w.PETITION_TYPE_CODE == petitionTypeCode)
                                  .FirstOrDefault();
                if (ent != null)
                {
                    res.ResultMessage = ent.FEE != null
                                            ? ent.FEE.Value
                                            : res.ResultMessage;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetPetitionFee", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> InsertLogOpenMenu(string userId, string functionId)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                DateTime curTime = DateTime.Now;
                var ent = new AG_IAS_LOG_ACTIVITY
                {
                    USER_ID = userId,
                    FUNCTION_ID = functionId,
                    ACTIVITY_DATETIME = curTime,
                    CREATED_BY = userId,
                    CREATED_DATE = curTime,
                    UPDATED_BY = userId,
                    UPDATED_DATE = curTime
                };
                base.ctx.AG_IAS_LOG_ACTIVITY.AddObject(ent);
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<string> GetPlaceExamNameById(string placeExamCode)
        {
            var res = new DTO.ResponseService<string>();

            try
            {
                var ent = base.ctx.AG_EXAM_PLACE_R
                                  .Where(w => w.EXAM_PLACE_CODE == placeExamCode)
                                  .FirstOrDefault();
                if (ent != null)
                {
                    res.DataResponse = ent.EXAM_PLACE_NAME;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetPlaceExamNameById", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> GetAcceptOffNameById(string acceptOffCode)
        {
            var res = new DTO.ResponseService<string>();

            try
            {
                var ent = base.ctx.AG_ACCEPT_OFF_R
                                 .Where(w => w.ACCEPT_OFF_CODE == acceptOffCode)
                                 .FirstOrDefault();
                if (ent != null)
                {
                    res.DataResponse = ent.OFF_NAME;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAcceptOffNameById", ex);
            }

            return res;
        }

        public DTO.ResponseService<string> GetCompanyNameByIdToText(string compCode)
        {
            var res = new DTO.ResponseService<string>();

            try
            {
                var ent = base.ctx.VW_IAS_COM_CODE
                                 .Where(w => w.ID == compCode)
                                 .FirstOrDefault();
                if (ent != null)
                {
                    res.DataResponse = ent.NAME;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetCompanyNameByIdToText", ex);
            }

            return res;
        }

        public List<DTO.InsuranceAssociate> GetAssociateList()
        {
            var Comlist = (from vw in base.ctx.AG_EXAM_PLACE_GROUP_R
                           where vw.ACTIVE == "Y"
                           select new DTO.InsuranceAssociate
                           {
                               Id = vw.EXAM_PLACE_GROUP_CODE,
                               Name = vw.EXAM_PLACE_GROUP_NAME
                           }).ToList();

            return Comlist;
        }


        public DTO.ResponseService<List<DTO.DataItem>> GetAssociateToSetting(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            res.DataResponse = new List<DTO.DataItem>();
            try
            {
                //Get Associate List
                var assoList = GetAssociateList();

                if (!string.IsNullOrEmpty(firstItem))
                    res.DataResponse.Insert(0, new DTO.DataItem { Id = "", Name = firstItem });

                foreach (DTO.InsuranceAssociate asso in assoList)
                {
                    res.DataResponse.Add(new DTO.DataItem { Id = asso.Id, Name = asso.Name });
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAssociateToSetting", ex);
            }
            return res;
        }

        public void Dispose()
        {
            if (ctx != null) ctx.Dispose();
            GC.SuppressFinalize(this);
        }

        public DTO.ResponseMessage<bool> InsertDocumentType(DTO.DocumentType doc, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var sameName = base.ctx.AG_IAS_DOCUMENT_TYPE.Where(w => w.DOCUMENT_NAME.Contains(doc.DOCUMENT_NAME) && w.DOCUMENT_NAME.EndsWith(doc.DOCUMENT_NAME) && w.STATUS == "A").ToList();
                if (sameName.Count <= 0)
                {
                    int iCode = base.ctx.AG_IAS_DOCUMENT_TYPE.Count();
                    iCode = iCode + 1;
                    string strDocumentCode = string.Empty;
                    if (iCode < 10)
                    {
                        strDocumentCode = "0" + (Convert.ToString(iCode));
                    }
                    else
                    {
                        strDocumentCode = (Convert.ToString(iCode));
                    }
                    var ent = new AG_IAS_DOCUMENT_TYPE
                    {
                        DOCUMENT_CODE = strDocumentCode,
                        DOCUMENT_NAME = doc.DOCUMENT_NAME,
                        DOCUMENT_REQUIRE = doc.DOCUMENT_REQUIRE,
                        USER_DATE = DateTime.Today,
                        USER_ID = userProfile.Id,
                        STATUS = doc.STATUS
                    };
                    base.ctx.AG_IAS_DOCUMENT_TYPE.AddObject(ent);
                    base.ctx.SaveChanges();
                }
                else
                {
                    res.ErrorMsg = Resources.errorDataCenterService_003;
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_InsertDocumentType", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> DeleteDocumentType(string docCode)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ent = base.ctx.AG_IAS_DOCUMENT_TYPE
                                  .SingleOrDefault(lr => lr.DOCUMENT_CODE == docCode);
                if (ent != null)
                {
                    ent.STATUS = "D";
                    base.ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_DeleteDocumentType", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> InsertConfigDocument(DTO.ConfigDocument doc, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                decimal iCode = base.ctx.AG_IAS_DOCUMENT_TYPE_T.Max(w => w.ID);
                var ent = new AG_IAS_DOCUMENT_TYPE_T
                {
                    ID = iCode + 1,
                    FUNCTION_ID = doc.FUNCTION_ID,
                    MEMBER_CODE = doc.MEMBER_CODE,
                    DOCUMENT_CODE = doc.DOCUMENT_CODE,
                    LICENSE_TYPE_CODE = doc.LICENSE_TYPE_CODE,
                    PETITION_TYPE_CODE = doc.PETITION_TYPE_CODE,
                    DOCUMENT_REQUIRE = doc.DOCUMENT_REQUIRE,
                    CREATED_BY = userProfile.Name,
                    CREATED_DATE = DateTime.Today,
                    UPDATED_BY = userProfile.Name,
                    UPDATED_DATE = DateTime.Today,
                    STATUS = doc.STATUS
                };
                base.ctx.AG_IAS_DOCUMENT_TYPE_T.AddObject(ent);
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_InsertConfigDocument", ex);
            }
            return res;
        }

        public DTO.ResponseService<string> GetConfigDocumentByDocumentCode(string FuncID, string docCode, string memberCode)
        {
            var res = new DTO.ResponseService<string>();

            try
            {
                var ent = base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                 .Where(w => w.MEMBER_CODE == memberCode && w.DOCUMENT_CODE == docCode && w.STATUS == "A" && w.FUNCTION_ID == FuncID)
                                 .FirstOrDefault();
                if (ent != null)
                {
                    res.DataResponse = ent.ID.ToString();
                }

            }

            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigDocumentByDocumentCode", ex);
            }

            return res;
        }

        public DTO.ResponseMessage<bool> DeleteConfigDocument(decimal id, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                var ent = base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                  .SingleOrDefault(lr => lr.ID == id);
                if (ent != null)
                {
                    ent.STATUS = "D";
                    ent.UPDATED_DATE = DateTime.Today;
                    ent.UPDATED_BY = userProfile.Name;
                    base.ctx.SaveChanges();
                }
                res.ConfigLicenseId = ent.LICENSE_TYPE_CODE;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_DeleteConfigDocument", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetConfigPetitionLicenseType(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                string sql = "SELECT PETITION_TYPE_CODE Id, PETITION_TYPE_NAME Name " +
                             "FROM AG_IAS_PETITION_TYPE_R " +
                             "WHERE STATUS='A' " +
                             "ORDER BY PETITION_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                res.DataResponse = new List<DTO.DataItem>();

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(firstItem))
                        res.DataResponse.Add(new DTO.DataItem { Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        res.DataResponse.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigPetitionLicenseType", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetConfigLicenseType(string firstItem)
        {
            var res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                string sql = "SELECT LICENSE_TYPE_CODE Id, LICENSE_TYPE_NAME Name " +
                             "FROM AG_IAS_LICENSE_TYPE_R " +
                             "WHERE STATUS='A' " +
                             "ORDER BY LICENSE_TYPE_CODE ";

                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                res.DataResponse = new List<DTO.DataItem>();

                if (dt != null && dt.Rows.Count > 0)
                {

                    if (!string.IsNullOrEmpty(firstItem))
                        res.DataResponse.Add(new DTO.DataItem { Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        res.DataResponse.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigLicenseType", ex);
            }
            return res;

        }

        public DTO.ResponseService<string> GetConfigDocumentLicense(string petitionCode, string licenseCode, string docCode)
        {
            var res = new DTO.ResponseService<string>();

            try
            {
                var ent = base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                 .Where(w => w.PETITION_TYPE_CODE == petitionCode && w.LICENSE_TYPE_CODE == licenseCode && w.DOCUMENT_CODE == docCode && w.STATUS == "A")
                                 .FirstOrDefault();

                if (ent != null)
                {
                    res.DataResponse = ent.ID.ToString();
                }


            }

            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfigByType(string licenseCode, string petitionCode)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                var list = (from dt in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                            join d in base.ctx.AG_IAS_DOCUMENT_TYPE on dt.DOCUMENT_CODE equals d.DOCUMENT_CODE
                            join l in base.ctx.AG_IAS_LICENSE_TYPE_R on dt.LICENSE_TYPE_CODE equals licenseCode
                            where dt.STATUS == "A" && d.STATUS == "A" && dt.FUNCTION_ID == "41" && dt.DOCUMENT_CODE == d.DOCUMENT_CODE && l.LICENSE_TYPE_CODE == licenseCode && dt.PETITION_TYPE_CODE == petitionCode
                            select new DTO.ConfigDocument
                            {
                                ID = dt.ID,
                                FUNCTION_ID = dt.FUNCTION_ID,
                                MEMBER_CODE = dt.MEMBER_CODE,
                                DOCUMENT_CODE = dt.DOCUMENT_CODE,
                                DOCUMENT_REQUIRE = dt.DOCUMENT_REQUIRE,
                                DOCUMENT_NAME = d.DOCUMENT_NAME,
                                LICENSE_TYPE_NAME = l.LICENSE_TYPE_NAME
                            }).ToList();
                res.DataResponse = list;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfigByPetitionType(string petitionCode)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                string id = string.Empty;
                var list = (from dt in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                            join d in base.ctx.AG_IAS_DOCUMENT_TYPE on dt.DOCUMENT_CODE equals d.DOCUMENT_CODE
                            join p in base.ctx.AG_IAS_PETITION_TYPE_R on dt.PETITION_TYPE_CODE equals p.PETITION_TYPE_CODE
                            join l in base.ctx.AG_IAS_LICENSE_TYPE_R on dt.LICENSE_TYPE_CODE equals l.LICENSE_TYPE_CODE
                            where dt.STATUS == "A" && d.STATUS == "A" && dt.FUNCTION_ID == "41" && dt.DOCUMENT_CODE == d.DOCUMENT_CODE && dt.PETITION_TYPE_CODE == petitionCode
                            select new DTO.ConfigDocument
                            {
                                ID = dt.ID,
                                FUNCTION_ID = dt.FUNCTION_ID,
                                MEMBER_CODE = dt.MEMBER_CODE,
                                DOCUMENT_CODE = dt.DOCUMENT_CODE,
                                DOCUMENT_REQUIRE = dt.DOCUMENT_REQUIRE,
                                DOCUMENT_NAME = d.DOCUMENT_NAME,
                                PETITION_TYPE_CODE = dt.PETITION_TYPE_CODE,
                                LICENSE_TYPE_CODE = dt.LICENSE_TYPE_CODE,
                                LICENSE_TYPE_NAME = l.LICENSE_TYPE_NAME
                            }).OrderBy(x => x.LICENSE_TYPE_CODE).ToList();
                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// Update ข้อมูลตั้งค่า Config ผู้ตรวจเอกสาร
        /// </summary>
        /// <returns>ResponseMessage<bool></returns>
        public DTO.ResponseMessage<bool> UpdateConfigApproveLicense(List<DTO.ConfigDocument> configs, DTO.UserProfile userProfile)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (DTO.ConfigDocument conf in configs)
                {
                    var ent = base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                      .SingleOrDefault(s => s.ID == conf.ID);
                    ent.UPDATED_DATE = DateTime.Today;
                    ent.UPDATED_BY = userProfile.Name;
                    ent.DOCUMENT_REQUIRE = conf.DOCUMENT_REQUIRE;

                }
                base.ctx.SaveChanges();
                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceGroupByCompCode(string firstItem, string compcode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                if ((compcode == null) || (compcode == ""))
                {
                    var list = (from c in ctx.AG_EXAM_PLACE_GROUP_R
                                where c.ACTIVE == "Y"
                                select new DTO.DataItem
                                {
                                    Id = c.EXAM_PLACE_GROUP_CODE,
                                    Name = c.EXAM_PLACE_GROUP_NAME
                                }).ToList();
                    res.DataResponse = list;
                }
                else
                {

                    var list = (from c in ctx.AG_EXAM_PLACE_GROUP_R
                                where c.EXAM_PLACE_GROUP_CODE == compcode && c.ACTIVE == "Y"
                                select new DTO.DataItem
                                 {
                                     Id = c.EXAM_PLACE_GROUP_CODE,
                                     Name = c.EXAM_PLACE_GROUP_NAME
                                 }).ToList();
                    res.DataResponse = list;
                }
                //AddFirstItem(list, firstItem);



                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceByCompCode(string groupCode, string compcode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = from ep in base.ctx.AG_EXAM_PLACE_R
                           join p in base.ctx.VW_IAS_PROVINCE on ep.PROVINCE_CODE equals p.ID
                           where ep.EXAM_PLACE_GROUP_CODE == compcode && ep.ACTIVE == "Y"
                           select new DTO.DataItem
                           {
                               Id = ep.EXAM_PLACE_CODE,
                               Name = ep.EXAM_PLACE_NAME + "[" + p.NAME + "]"
                           };

                res.DataResponse = list.ToList();

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        #region Personal License Func
        public DTO.ResponseService<List<DTO.DataItem>> GetCompanyByLicenseType(string firstItem, string licenseType)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            Int16 license = Convert.ToInt16(licenseType);

            try
            {
                if (licenseType.Equals(""))
                {
                    return null;
                }
                else
                {
                    var list = (from comp in base.ctx.VW_AS_COMPANY_T
                                from compn in this.ctx.VW_AS_BUSI_TYPE_R
                                where comp.BUSINESS_CODE == compn.BUSINESS_CODE &&
                                comp.BUSINESS_CODE == license
                                select new DTO.DataItem
                                {
                                    //Id = comp.COMP_CODE,
                                    //Name = comp.COMP_NAMET

                                    Id = comp.COMP_CODE,
                                    Name = comp.COMP_NAMET + " [" + comp.COMP_CODE + "]"
                                }).ToList();
                    AddFirstItem(list, firstItem);
                    res.DataResponse = list;
                    return res;
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config เอกสารขอใบอนุญาต
        /// </summary>
        /// <returns>ResponseService<List<ConfigEntity>></returns>
        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfig(string petitionCode, string licenseTypeCode)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                var list = (from dt in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                            join d in base.ctx.AG_IAS_DOCUMENT_TYPE on dt.DOCUMENT_CODE equals d.DOCUMENT_CODE
                            join p in base.ctx.AG_IAS_PETITION_TYPE_R on dt.PETITION_TYPE_CODE equals p.PETITION_TYPE_CODE
                            join l in base.ctx.AG_IAS_LICENSE_TYPE_R on dt.LICENSE_TYPE_CODE equals l.LICENSE_TYPE_CODE
                            where dt.STATUS == "A" &&
                            d.STATUS == "A" &&
                            dt.FUNCTION_ID == "41" &&
                            dt.DOCUMENT_CODE == d.DOCUMENT_CODE &&
                            dt.PETITION_TYPE_CODE == petitionCode &&
                            l.LICENSE_TYPE_CODE == licenseTypeCode
                            select new DTO.ConfigDocument
                            {
                                ID = dt.ID,
                                FUNCTION_ID = dt.FUNCTION_ID,
                                MEMBER_CODE = dt.MEMBER_CODE,
                                DOCUMENT_CODE = dt.DOCUMENT_CODE,
                                DOCUMENT_REQUIRE = dt.DOCUMENT_REQUIRE,
                                DOCUMENT_NAME = d.DOCUMENT_NAME,
                                PETITION_TYPE_CODE = dt.PETITION_TYPE_CODE,
                                LICENSE_TYPE_CODE = dt.LICENSE_TYPE_CODE,
                                LICENSE_TYPE_NAME = l.LICENSE_TYPE_NAME
                            }).OrderBy(x => x.LICENSE_TYPE_CODE).ToList();
                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// GetPersonLicenseType
        /// </summary>
        /// <param name="firstItem"></param>
        /// <returns>DTO.ResponseService<List<DTO.DataItem>></returns>
        public DTO.ResponseService<List<DTO.DataItem>> GetPersonLicenseType(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                //TODO: ดึง AG_LICENSE_TYPE_R เข้า Model ก่อน
                var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "03" || w.LICENSE_TYPE_CODE == "04")
                               .Select(s => new DTO.DataItem
                               {
                                   Id = s.LICENSE_TYPE_CODE,
                                   Name = s.LICENSE_TYPE_NAME
                               }).OrderBy(type => type.Id).ToList();


                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.SpecialDocument>> GetSpecialDocType(string docStatus, string trainStatus)
        {
            var res = new DTO.ResponseService<List<DTO.SpecialDocument>>();
            try
            {
                IQueryable<DTO.SpecialDocument> result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE
                                                          join spe in base.ctx.AG_TRAIN_SPECIAL_R on doct.SPECIAL_TYPE_CODE_TRAIN equals spe.SPECIAL_TYPE_CODE
                                                          where doct.STATUS == docStatus && doct.TRAIN_DISCOUNT_STATUS == trainStatus
                                                          select new DTO.SpecialDocument
                                                          {
                                                              DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                                              DOCUMENT_NAME = doct.DOCUMENT_NAME,
                                                              DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                                              TRAIN_DISCOUNT_STATUS = doct.TRAIN_DISCOUNT_STATUS,
                                                              EXAM_DISCOUNT_STATUS = doct.EXAM_DISCOUNT_STATUS,
                                                              SPECIAL_TYPE_CODE_TRAIN = doct.SPECIAL_TYPE_CODE_TRAIN,
                                                              SPECIAL_TYPE_CODE_EXAM = doct.SPECIAL_TYPE_CODE_EXAM,
                                                              STATUS = doct.STATUS

                                                          });

                res.DataResponse = result.ToList();

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetSpecialDocType", ex.Message);
            }
            return res;

        }

        public DTO.ResponseService<List<DTO.SpecialDocument>> GetExamSpecialDocType(string docStatus, string trainStatus, string licenseType)
        {
            var res = new DTO.ResponseService<List<DTO.SpecialDocument>>();
            var tagUser = new string[] { "" };

            try
            {
                var agenType = ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());

                if (agenType.INSURANCE_TYPE == "1")
                {
                    tagUser = new string[] { "L", "B" };
                }
                else if (agenType.INSURANCE_TYPE == "2")
                {
                    tagUser = new string[] { "D", "B" };
                }
                else
                {
                    tagUser = new string[] { "B" };
                }





                IQueryable<DTO.SpecialDocument> result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE
                                                          join spe in base.ctx.AG_IAS_EXAM_SPECIAL_R on doct.SPECIAL_TYPE_CODE_EXAM equals spe.SPECIAL_TYPE_CODE
                                                          where doct.STATUS == docStatus 
                                                          && doct.EXAM_DISCOUNT_STATUS == trainStatus
                                                          && tagUser.Contains(spe.USED_TYPE.Trim())
                                                          select new DTO.SpecialDocument
                                                          {
                                                              DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                                              DOCUMENT_NAME = doct.DOCUMENT_NAME,
                                                              DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                                              TRAIN_DISCOUNT_STATUS = doct.TRAIN_DISCOUNT_STATUS,
                                                              EXAM_DISCOUNT_STATUS = doct.EXAM_DISCOUNT_STATUS,
                                                              SPECIAL_TYPE_CODE_TRAIN = doct.SPECIAL_TYPE_CODE_TRAIN,
                                                              SPECIAL_TYPE_CODE_EXAM = doct.SPECIAL_TYPE_CODE_EXAM,
                                                              STATUS = doct.STATUS

                                                          });

                res.DataResponse = result.ToList();

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetSpecialDocType", ex.Message);
            }
            return res;

        }
        #endregion

        public DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByCompCode(string compcode)
        {

            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            //var list = string.Empty;
            try
            {
                if (compcode != null && compcode.Length == 4)
                {
                    if (compcode != null && compcode.StartsWith("1"))
                    {
                        var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "01" ||
                                                                       w.LICENSE_TYPE_CODE == "07").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                       .Select(s => new DTO.DataItem
                                                                       {
                                                                           Id = s.LICENSE_TYPE_CODE,
                                                                           Name = s.LICENSE_TYPE_NAME
                                                                       }).ToList();

                        res.DataResponse = list;
                    }
                    else if (compcode != null && compcode.StartsWith("2"))
                    {
                        var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "02" ||
                                                                   w.LICENSE_TYPE_CODE == "05" ||
                                                                   w.LICENSE_TYPE_CODE == "06" ||
                                                                   w.LICENSE_TYPE_CODE == "08").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                   .Select(s => new DTO.DataItem
                                                                   {
                                                                       Id = s.LICENSE_TYPE_CODE,
                                                                       Name = s.LICENSE_TYPE_NAME
                                                                   }).ToList();

                        res.DataResponse = list;
                    }
                    else if (compcode != null && compcode.StartsWith("3"))
                    {
                        var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "03" ||
                                                                   w.LICENSE_TYPE_CODE == "04" ||
                                                                   w.LICENSE_TYPE_CODE == "11" ||
                                                                   w.LICENSE_TYPE_CODE == "12").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                   .Select(s => new DTO.DataItem
                                                                   {
                                                                       Id = s.LICENSE_TYPE_CODE,
                                                                       Name = s.LICENSE_TYPE_NAME
                                                                   }).ToList();
                        res.DataResponse = list;
                    }
                }
                else if (compcode != null && compcode.Length == 3)
                {

                    var Aso = base.ctx.AG_IAS_ASSOCIATION.Where(w => w.ASSOCIATION_CODE == compcode).Select(s => new DTO.DataItem
                    {
                        Id = s.ASSOCIATION_CODE,
                        Name = s.ASSOCIATION_NAME
                    }).ToList();

                    if (Aso != null)
                    {
                        var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.ACTIVE_FLAG == "Y").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                             .Select(s => new DTO.DataItem
                                                             {
                                                                 Id = s.LICENSE_TYPE_CODE,
                                                                 Name = s.LICENSE_TYPE_NAME
                                                             }).ToList();
                        res.DataResponse = list;
                    }

                }

                else if (compcode == null)
                {
                    var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.ACTIVE_FLAG == "Y").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                .Select(s => new DTO.DataItem
                                                                {
                                                                    Id = s.LICENSE_TYPE_CODE,
                                                                    Name = s.LICENSE_TYPE_NAME
                                                                }).ToList();
                    res.DataResponse = list;
                }
                return res;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByCreateTest(DTO.UserProfile user)
        {

            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                if ((user.MemberType == DTO.RegistrationType.OIC.GetEnumValue()) || (user.MemberType == DTO.RegistrationType.OICAgent.GetEnumValue()))
                {
                    var list = base.ctx.AG_IAS_LICENSE_TYPE_R.OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                   .Select(s => new DTO.DataItem
                                                                   {
                                                                       Id = s.LICENSE_TYPE_CODE,
                                                                       Name = s.LICENSE_TYPE_NAME
                                                                   }).ToList();

                    res.DataResponse = list;
                }
                else if (user.MemberType == DTO.RegistrationType.TestCenter.GetEnumValue())
                {
                    if (user.CompCode == "666")
                    {
                        var list = base.ctx.AG_IAS_LICENSE_TYPE_R.Where(w => w.LICENSE_TYPE_CODE == "03" ||
                                                               w.LICENSE_TYPE_CODE == "04").OrderBy(x => x.LICENSE_TYPE_CODE)
                                                               .Select(s => new DTO.DataItem
                                                               {
                                                                   Id = s.LICENSE_TYPE_CODE,
                                                                   Name = s.LICENSE_TYPE_NAME
                                                               }).ToList();

                        res.DataResponse = list;
                    }
                    else
                    {
                        var list = base.ctx.AG_IAS_LICENSE_TYPE_R.OrderBy(x => x.LICENSE_TYPE_CODE)
                                                                  .Select(s => new DTO.DataItem
                                                                  {
                                                                      Id = s.LICENSE_TYPE_CODE,
                                                                      Name = s.LICENSE_TYPE_NAME
                                                                  }).ToList();

                        res.DataResponse = list;
                    }
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        #region Registration Get DocRequire
        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config เอกสารขอใบอนุญาต
        /// </summary>
        /// <returns>DTO.ResponseService<List<DTO.ConfigDocument>></returns>
        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocRequire(string docFunc, string memCode, string licenseType, string pettionType)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                Func<string, string> nullValidate = (input) =>
                {
                    if ((input == null) || (input == ""))
                    {



                    }

                    return input;
                };

                //Registration Mode
                if (docFunc.Equals(Convert.ToString(DTO.DocFunction.REGISTER_FUNCTION.GetEnumValue())))
                {
                    var result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                  from doc in this.ctx.AG_IAS_DOCUMENT_TYPE
                                  where doct.DOCUMENT_CODE == doc.DOCUMENT_CODE &&
                                  doct.FUNCTION_ID == docFunc &&
                                  doct.MEMBER_CODE == memCode &&
                                  doct.DOCUMENT_REQUIRE == "Y" &&
                                  doct.STATUS == "A"
                                  select new DTO.ConfigDocument
                                  {
                                      ID = doct.ID,
                                      FUNCTION_ID = doct.FUNCTION_ID,
                                      MEMBER_CODE = doct.MEMBER_CODE,
                                      DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                      DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                      DOCUMENT_NAME = doc.DOCUMENT_NAME,
                                      LICENSE_TYPE_CODE = doct.LICENSE_TYPE_CODE

                                  }).ToList();

                    res.DataResponse = result;
                }
                //License Mode
                else if (docFunc.Equals(Convert.ToString(DTO.DocFunction.LICENSE_FUNCTION.GetEnumValue())))
                {
                    var result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                  from doc in this.ctx.AG_IAS_DOCUMENT_TYPE
                                  where doct.DOCUMENT_CODE == doc.DOCUMENT_CODE &&
                                  doct.FUNCTION_ID == docFunc &&
                                  doct.LICENSE_TYPE_CODE == licenseType &&
                                  doct.PETITION_TYPE_CODE == pettionType &&
                                  doct.DOCUMENT_REQUIRE == "Y" &&
                                  doct.STATUS == "A"
                                  select new DTO.ConfigDocument
                                  {
                                      ID = doct.ID,
                                      FUNCTION_ID = doct.FUNCTION_ID,
                                      MEMBER_CODE = doct.MEMBER_CODE,
                                      DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                      DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                      DOCUMENT_NAME = doc.DOCUMENT_NAME,
                                      LICENSE_TYPE_CODE = doct.LICENSE_TYPE_CODE

                                  }).ToList();

                    res.DataResponse = result;
                }
                else if (docFunc.Equals(Convert.ToString(DTO.DocFunction.APPLICANT_FUNCTION.GetEnumValue())))
                {
                    var result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                  from doc in this.ctx.AG_IAS_DOCUMENT_TYPE
                                  where doct.DOCUMENT_CODE == doc.DOCUMENT_CODE &&
                                  doct.FUNCTION_ID == docFunc &&
                                  doct.DOCUMENT_REQUIRE == "Y" &&
                                  doct.STATUS == "A"
                                  select new DTO.ConfigDocument
                                  {
                                      ID = doct.ID,
                                      FUNCTION_ID = doct.FUNCTION_ID,
                                      MEMBER_CODE = doct.MEMBER_CODE,
                                      DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                      DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                      DOCUMENT_NAME = doc.DOCUMENT_NAME,
                                      LICENSE_TYPE_CODE = doct.LICENSE_TYPE_CODE

                                  }).ToList();

                    res.DataResponse = result;
                    int count = res.DataResponse.Count();
                }

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;

        }
        #endregion

        public DTO.ResponseService<List<DTO.ConfigDocument>> GetMemberDocumentType(string memCode)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                var result = (from doct in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                              from doc in this.ctx.AG_IAS_DOCUMENT_TYPE
                              where doct.DOCUMENT_CODE == doc.DOCUMENT_CODE &&
                              doct.MEMBER_CODE == memCode &&
                              doct.STATUS == "A"
                              select new DTO.ConfigDocument
                              {
                                  ID = doct.ID,
                                  FUNCTION_ID = doct.FUNCTION_ID,
                                  MEMBER_CODE = doct.MEMBER_CODE,
                                  DOCUMENT_CODE = doct.DOCUMENT_CODE,
                                  DOCUMENT_REQUIRE = doct.DOCUMENT_REQUIRE,
                                  DOCUMENT_NAME = doc.DOCUMENT_NAME,
                                  LICENSE_TYPE_CODE = doct.LICENSE_TYPE_CODE

                              }).ToList();
                res.DataResponse = result;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public List<DTO.DocumentType> GetDocumentConfigList()
        {
            var Doclist = (from doc in base.ctx.AG_IAS_DOCUMENT_TYPE
                           select new DTO.DocumentType
                           {
                               DOCUMENT_CODE = doc.DOCUMENT_CODE,
                               DOCUMENT_NAME = doc.DOCUMENT_NAME
                           }).ToList();

            return Doclist;
        }

        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentLicenseConfigByPetitionTypeName(string petitionName)
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                var petition = base.ctx.AG_IAS_PETITION_TYPE_R.Where(w => w.PETITION_TYPE_NAME == petitionName).FirstOrDefault();

                var list = (from dt in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                            join d in base.ctx.AG_IAS_DOCUMENT_TYPE on dt.DOCUMENT_CODE equals d.DOCUMENT_CODE
                            join p in base.ctx.AG_IAS_PETITION_TYPE_R on dt.PETITION_TYPE_CODE equals p.PETITION_TYPE_CODE
                            join l in base.ctx.AG_IAS_LICENSE_TYPE_R on dt.LICENSE_TYPE_CODE equals l.LICENSE_TYPE_CODE
                            where dt.STATUS == "A" && d.STATUS == "A" && dt.FUNCTION_ID == "41" && dt.DOCUMENT_CODE == d.DOCUMENT_CODE && dt.PETITION_TYPE_CODE == petition.PETITION_TYPE_CODE
                            select new DTO.ConfigDocument
                            {
                                ID = dt.ID,
                                FUNCTION_ID = dt.FUNCTION_ID,
                                MEMBER_CODE = dt.MEMBER_CODE,
                                DOCUMENT_CODE = dt.DOCUMENT_CODE,
                                DOCUMENT_REQUIRE = dt.DOCUMENT_REQUIRE,
                                DOCUMENT_NAME = d.DOCUMENT_NAME,
                                PETITION_TYPE_CODE = dt.PETITION_TYPE_CODE,
                                LICENSE_TYPE_CODE = dt.LICENSE_TYPE_CODE,
                                LICENSE_TYPE_NAME = l.LICENSE_TYPE_NAME
                            }).OrderBy(x => x.LICENSE_TYPE_CODE).ToList();
                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetPicByDocumentCode(string documentCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = ctx.AG_IAS_DOCUMENT_TYPE.Where(w => w.DOCUMENT_CODE == documentCode && documentCode == "04")
                              .Select(s => new DTO.DataItem
                              {
                                  Id = s.DOCUMENT_CODE,
                                  Name = s.DOCUMENT_NAME
                              })
                              .ToList();

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetAgentType(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
                var list = base.ctx.AG_AGENT_TYPE_R
                               .Select(s => new DTO.DataItem
                               {
                                   Id = s.AGENT_TYPE,
                                   Name = s.AGENT_TYPE_DESC
                               })
                              .ToList();


                AddFirstItem(list, firstItem);

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetLicenseTypeByAgentType(string agentType)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = from ag in base.ctx.AG_AGENT_TYPE_R
                           join l in base.ctx.AG_IAS_LICENSE_TYPE_R on ag.AGENT_TYPE equals l.AGENT_TYPE
                           where l.STATUS == "A" && l.AGENT_TYPE == agentType
                           select new DTO.DataItem
                           {
                               Id = l.LICENSE_TYPE_CODE,
                               Name = l.LICENSE_TYPE_NAME

                           };

                res.DataResponse = list.ToList();

                return res;
            }
            catch (Exception ex)
            {

                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetAssociation(string Asso_Code)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                if (Asso_Code == "")
                    Asso_Code = "%%";

                string sql = "select ASSOCIATION_CODE, ASSOCIATION_NAME, COMP_TYPE, AGENT_TYPE, ACTIVE " +
                        " from AG_IAS_ASSOCIATION where ACTIVE ='Y' and ASSOCIATION_CODE like '" + Asso_Code + "' order by ASSOCIATION_CODE asc ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetAssociationJoinLicense(string Asso_Code, string license_code)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                if (Asso_Code == "")
                    Asso_Code = "%%";

                string sql = "select distinct a.ASSOCIATION_CODE, a.ASSOCIATION_NAME " +
                        " from AG_IAS_ASSOCIATION a,ag_ias_association_license li " +
                        " where a.ACTIVE ='Y' and a.ASSOCIATION_CODE like '" + Asso_Code + "'  " +
                        " and li.active='Y' and a.association_code = li.association_code " +
                        " and li.license_type_code like '" + license_code + "%' order by a.ASSOCIATION_CODE asc ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> InsertAssociation(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var resCode = base.ctx.AG_IAS_ASSOCIATION.FirstOrDefault(x => x.ASSOCIATION_CODE == ent.ASSOCIATION_CODE);
                var resName = base.ctx.AG_IAS_ASSOCIATION.FirstOrDefault(x => x.ASSOCIATION_NAME == ent.ASSOCIATION_NAME);
                if (resCode != null && resCode.ACTIVE == "Y")
                {
                    res.ErrorMsg = Resources.errorDataCenterService_001;
                    return res;
                }
                else if (resCode != null && resCode.ACTIVE == "N")
                {
                    res.ErrorMsg = "ไม่สามารถเพิ่มข้อมูลได้ เนื่องจากมีการยกเลิกการใช้งาน รหัส " + resCode.ASSOCIATION_CODE + " แล้ว <br> กรุณาใช้รหัสสมาคมอื่น";
                    return res;
                }
                if (resName != null && resName.ACTIVE == "Y")
                {
                    res.ErrorMsg = Resources.errorDataCenterService_002;
                    return res;
                }
                else if (resName != null && resName.ACTIVE == "N")
                {
                    res.ErrorMsg = "ไม่สามารถเพิ่มข้อมูลได้ เนื่องจากมีการยกเลิกการใช้งาน สมาคม " + resName.ASSOCIATION_NAME + " แล้ว <br> กรุณาใช้ชื่อสมาคมอื่น";
                    return res;
                }
                AG_IAS_ASSOCIATION table = new AG_IAS_ASSOCIATION();
                ent.MappingToEntity(table);
                table.USER_ID = userProfile.Id;
                table.USER_DATE = DateTime.Now;
                base.ctx.AG_IAS_ASSOCIATION.AddObject(table);

                foreach (var i in license)
                {
                    var restemp = base.ctx.AG_IAS_ASSOCIATION_LICENSE.FirstOrDefault(x => x.ASSOCIATION_CODE == ent.ASSOCIATION_CODE && x.LICENSE_TYPE_CODE == i.LICENSE_TYPE_CODE);
                    if (restemp != null)
                    {
                        restemp.USER_ID = userProfile.Id;
                        restemp.USER_DATE = DateTime.Now;
                        restemp.UPDATED_BY = "";
                        restemp.UPDATED_DATE = null;
                        restemp.ACTIVE = i.ACTIVE;
                    }
                    else
                    {
                        AG_IAS_ASSOCIATION_LICENSE obj = new AG_IAS_ASSOCIATION_LICENSE();
                        obj.ASSOCIATION_CODE = ent.ASSOCIATION_CODE;
                        obj.LICENSE_TYPE_CODE = i.LICENSE_TYPE_CODE;
                        obj.USER_ID = userProfile.Id;
                        obj.USER_DATE = DateTime.Now;
                        obj.ACTIVE = i.ACTIVE;
                        base.ctx.AG_IAS_ASSOCIATION_LICENSE.AddObject(obj);
                    }
                }
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateAsscoiation(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var resName = base.ctx.AG_IAS_ASSOCIATION.FirstOrDefault(x => x.ASSOCIATION_NAME == ent.ASSOCIATION_NAME && x.ASSOCIATION_CODE != ent.ASSOCIATION_CODE);
                if (resName != null && resName.ACTIVE == "Y")
                {
                    res.ErrorMsg = Resources.errorDataCenterService_002;
                    return res;
                }
                else if (resName != null && resName.ACTIVE == "N")
                {
                    res.ErrorMsg = "ไม่สามารถเพิ่มข้อมูลได้ เนื่องจากมีการยกเลิกการใช้งาน สมาคม " + resName.ASSOCIATION_NAME + " แล้ว <br> กรุณาใช้ชื่อสมาคมอื่น";
                    return res;
                }
                AG_IAS_ASSOCIATION table = base.ctx.AG_IAS_ASSOCIATION.FirstOrDefault(s => s.ASSOCIATION_CODE == ent.ASSOCIATION_CODE);
                table.ASSOCIATION_NAME = ent.ASSOCIATION_NAME;
                table.COMP_TYPE = ent.COMP_TYPE;
                table.AGENT_TYPE = ent.AGENT_TYPE;
                //table.ACTIVE = ent.ACTIVE;
                table.UPDATED_BY = userProfile.Id;
                table.UPDATED_DATE = DateTime.Now;

                foreach (var i in license)
                {
                    var qry = base.ctx.AG_IAS_ASSOCIATION_LICENSE.FirstOrDefault(
                                s => s.ASSOCIATION_CODE == i.ASSOCIATION_CODE &&
                                    s.LICENSE_TYPE_CODE == i.LICENSE_TYPE_CODE);
                    if (qry == null)
                    {
                        AG_IAS_ASSOCIATION_LICENSE newObj = new AG_IAS_ASSOCIATION_LICENSE();
                        newObj.ASSOCIATION_CODE = ent.ASSOCIATION_CODE;
                        newObj.LICENSE_TYPE_CODE = i.LICENSE_TYPE_CODE;
                        newObj.USER_ID = userProfile.Id;
                        newObj.USER_DATE = DateTime.Now;
                        newObj.ACTIVE = i.ACTIVE;
                        base.ctx.AG_IAS_ASSOCIATION_LICENSE.AddObject(newObj);
                    }
                    else
                    {
                        qry.UPDATED_BY = userProfile.Id;
                        qry.UPDATED_DATE = DateTime.Now;
                        qry.ACTIVE = i.ACTIVE;
                    }
                }
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> DeleteAsscoiation(string ID)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_IAS_ASSOCIATION ent = base.ctx.AG_IAS_ASSOCIATION.FirstOrDefault(x => x.ASSOCIATION_CODE == ID);
                ent.ACTIVE = "N";
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }


        public DTO.ResponseService<List<DTO.DataItem>> GetQualification(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                IEnumerable<DTO.DataItem> list = ctx.AG_TRAIN_SPECIAL_R.Select(s => new DTO.DataItem
                {
                    Id = s.SPECIAL_TYPE_CODE,
                    Name = s.SPECIAL_TYPE_DESC
                });
                List<DTO.DataItem> listsort = list.OrderBy(a => a.Name).ToList();
                AddFirstItem(listsort, firstItem);

                res.DataResponse = listsort;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseMessage<bool> UpdateCancelApplicant(DTO.Applicant appl, DTO.ExamLicense examl)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                AG_APPLICANT_T entApp = new AG_APPLICANT_T();
                entApp = ctx.AG_APPLICANT_T
                                      .Where(s => s.APPLICANT_CODE == appl.APPLICANT_CODE && s.TESTING_NO == appl.TESTING_NO && s.EXAM_PLACE_CODE == appl.EXAM_PLACE_CODE)
                                      .FirstOrDefault();

                entApp.APPLICANT_CODE = appl.APPLICANT_CODE;
                entApp.EXAM_PLACE_CODE = appl.EXAM_PLACE_CODE;
                entApp.TESTING_NO = appl.TESTING_NO;
                entApp.RECORD_STATUS = appl.RECORD_STATUS;
                entApp.CANCEL_REASON = appl.CANCEL_REASON;

                AG_EXAM_LICENSE_R entExam = new AG_EXAM_LICENSE_R();
                entExam = ctx.AG_EXAM_LICENSE_R
                                    .Where(w => w.TESTING_NO == examl.TESTING_NO && w.EXAM_PLACE_CODE == examl.EXAM_PLACE_CODE && w.TESTING_DATE == examl.TESTING_DATE && w.TEST_TIME_CODE == examl.TEST_TIME_CODE && w.LICENSE_TYPE_CODE == examl.LICENSE_TYPE_CODE)
                                    .FirstOrDefault();

                if (entExam.EXAM_APPLY != null)
                {
                    //entExam.EXAM_APPLY = entExam.EXAM_APPLY > 0 ? --entExam.EXAM_APPLY : entExam.EXAM_APPLY;
                    entExam.EXAM_APPLY = entExam.EXAM_APPLY > 0 ? Convert.ToInt16(entExam.EXAM_APPLY - 1) : Convert.ToInt16(0);
                }
                else
                {
                    entExam.EXAM_APPLY = 0;
                }


                using (TransactionScope ts = new TransactionScope())
                {
                    base.ctx.SaveChanges();
                    ts.Complete();
                }

                res.ResultMessage = true;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetConfigPrint(string groupCode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select ID,ITEM,ITEM_VALUE,GROUP_CODE from agdoi.AG_IAS_CONFIG "
                           + "where GROUP_CODE = '" + groupCode + "' ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseMessage<bool> SaveConfigPrint(List<DTO.ConfigPrintPayment> ent)
        {
            var res = new DTO.ResponseMessage<bool>();
            try
            {
                foreach (var item in ent)
                {
                    AG_IAS_CONFIG TConfig = ctx.AG_IAS_CONFIG.Where(c => c.GROUP_CODE == item.GROUP_CODE && c.ID == item.Id).SingleOrDefault();
                    TConfig.ITEM_VALUE = item.ITEM_VALUE;
                    TConfig.USER_ID = item.USER_ID;
                    TConfig.USER_DATE = DateTime.Now;
                }
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";

            }
            return res;
        }

        public DTO.ResponseService<List<DTO.AssociationLicense>> GetAssociationLicense(string Association_Code)
        {
            DTO.ResponseService<List<DTO.AssociationLicense>> res = new DTO.ResponseService<List<DTO.AssociationLicense>>();
            try
            {
                var ls = base.ctx.AG_IAS_ASSOCIATION_LICENSE.Where(s => s.ASSOCIATION_CODE == Association_Code)
                            .Select(g => new DTO.AssociationLicense
                            {
                                ASSOCIATION_CODE = g.ASSOCIATION_CODE,
                                LICENSE_TYPE_CODE = g.LICENSE_TYPE_CODE,
                                ACTIVE = g.ACTIVE
                            }).ToList();
                res.DataResponse = ls;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.AssociationLicense>> GetAssociationLicenseByCode(string Association_Code)
        {
            DTO.ResponseService<List<DTO.AssociationLicense>> res = new DTO.ResponseService<List<DTO.AssociationLicense>>();
            try
            {
                var ls = base.ctx.AG_IAS_ASSOCIATION_LICENSE.Where(s => s.ASSOCIATION_CODE == Association_Code)
                            .Select(g => new DTO.AssociationLicense
                            {
                                ASSOCIATION_CODE = g.ASSOCIATION_CODE,
                                LICENSE_TYPE_CODE = g.LICENSE_TYPE_CODE,
                                ACTIVE = g.ACTIVE
                            }).ToList();
                res.DataResponse = ls;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceFromProvinceAndGroupCode(string ProCode, string GroupCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                //IEnumerable<DTO.DataItem>  ls = base.ctx.AG_EXAM_PLACE_R.Where(x => (x.PROVINCE_CODE == ProCode 
                //                                         && x.ACTIVE == "Y"
                //                                         && x.EXAM_PLACE_GROUP_CODE == GroupCode) ||( x.ACTIVE == "Y" && x.FREE =="Y"))
                //                                  .Select(s => new DTO.DataItem
                //                                         {
                //                                             Id = s.EXAM_PLACE_CODE,
                //                                             Name = s.EXAM_PLACE_NAME
                //                                         });
                string sql = " select  x.EXAM_PLACE_CODE, " +
                                " case x.PROVINCE_CODE when '" + ProCode + "' then  " +
                                " 		case	x.EXAM_PLACE_GROUP_CODE when'" + GroupCode + "' then x.EXAM_PLACE_NAME else x.EXAM_PLACE_NAME || ' *' END  " +
                                " 	else  " +
                                " 		x.EXAM_PLACE_NAME || ' *'  " +
                                "  end " +
                                " from AG_EXAM_PLACE_R x where  " +
                                " x.ACTIVE = 'Y' and ((x.PROVINCE_CODE = '" + ProCode + "' and x.EXAM_PLACE_GROUP_CODE ='" + GroupCode + "')  " +
                                " 		or (x.FREE='Y' and x.PROVINCE_CODE  = '" + ProCode + "')) ";
                OracleDB ora = new OracleDB();
                var resss = ora.GetDataSet(sql);

                DataTable dt = resss.Tables[0];

                var list = new List<DTO.DataItem>();

                foreach (DataRow item in dt.Rows)
                {
                    DTO.DataItem data = new DTO.DataItem();
                    data.Id = item[0].ToString();
                    data.Name = item[1].ToString();
                    list.Add(data);
                }

                List<DTO.DataItem> listsort = list.OrderBy(x => x.Id).ToList();
                res.DataResponse = listsort;
            }
            catch
            {

            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamPlaceFromProvinceAndAssoCode(string ProCode, string GroupCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                //IEnumerable<DTO.DataItem>  ls = base.ctx.AG_EXAM_PLACE_R.Where(x => (x.PROVINCE_CODE == ProCode 
                //                                         && x.ACTIVE == "Y"
                //                                         && x.EXAM_PLACE_GROUP_CODE == GroupCode) ||( x.ACTIVE == "Y" && x.FREE =="Y"))
                //                                  .Select(s => new DTO.DataItem
                //                                         {
                //                                             Id = s.EXAM_PLACE_CODE,
                //                                             Name = s.EXAM_PLACE_NAME
                //                                         });
                string sql = " select  x.EXAM_PLACE_CODE, " +
                                " case x.PROVINCE_CODE when '" + ProCode + "' then  " +
                                " 		case	x.association_code when'" + GroupCode + "' then x.EXAM_PLACE_NAME else x.EXAM_PLACE_NAME || ' *' END  " +
                                " 	else  " +
                                " 		x.EXAM_PLACE_NAME || ' *'  " +
                                "  end " +
                                " from AG_EXAM_PLACE_R x where  " +
                                " x.ACTIVE = 'Y' and ((x.PROVINCE_CODE = '" + ProCode + "' and x.association_code ='" + GroupCode + "')  " +
                                " 		or (x.FREE='Y' and x.PROVINCE_CODE  = '" + ProCode + "')) ";
                OracleDB ora = new OracleDB();
                var resss = ora.GetDataSet(sql);

                DataTable dt = resss.Tables[0];

                var list = new List<DTO.DataItem>();

                foreach (DataRow item in dt.Rows)
                {
                    DTO.DataItem data = new DTO.DataItem();
                    data.Id = item[0].ToString();
                    data.Name = item[1].ToString();
                    list.Add(data);
                }

                List<DTO.DataItem> listsort = list.OrderBy(x => x.Id).ToList();
                res.DataResponse = listsort;
            }
            catch
            {

            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetSubjectGroup(string firstItem)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {

                var list = new List<DTO.DataItem>();
                foreach (var item in ctx.AG_IAS_SUBJECT_GROUP.Where(x => x.STATUS == "A"))
                {
                    DTO.DataItem data = new DTO.DataItem();
                    data.Id = item.ID.ToString();
                    data.Name = item.GROUP_NAME;
                    list.Add(data);
                }

                AddFirstItem(list, firstItem);
                res.DataResponse = list;
            }
            catch
            {

            }
            return res;
        }

        //public DTO.ResponseService<List<DTO.AssociationLicense>> GetAssociationLicenseByAssocCode(string AssociationCode)
        //{
        //    DTO.ResponseService<List<DTO.AssociationLicense>> res = new DTO.ResponseService<List<DTO.AssociationLicense>>();
        //    try
        //    {
        //        var ls = base.ctx.AG_IAS_ASSOCIATION_LICENSE
        //             .Where(s => s.ASSOCIATION_CODE == AssociationCode && s.ACTIVE == "Y")
        //             .Select(s => new DTO.AssociationLicense
        //         {
        //             ASSOCIATION_CODE = s.ASSOCIATION_CODE,
        //             LICENSE_TYPE_CODE = s.LICENSE_TYPE_CODE,
        //             USER_ID = s.USER_ID,
        //             USER_DATE = s.USER_DATE,
        //             UPDATED_BY = s.UPDATED_BY,
        //             UPDATED_DATE = s.UPDATED_DATE,
        //             ACTIVE = s.ACTIVE

        //         }).ToList();
        //        res.DataResponse = ls;

        //    }
        //    catch (Exception ex)
        //    {

        //        res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
        //    }
        //    return res;
        //}

        public DTO.ResponseService<List<DTO.DataItem>> GetAssociationLicenseByAssocCode(string AssociationCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {

                var list = (from al in base.ctx.AG_IAS_ASSOCIATION_LICENSE
                            join lt in base.ctx.AG_IAS_LICENSE_TYPE_R on al.LICENSE_TYPE_CODE equals lt.LICENSE_TYPE_CODE
                            where al.ASSOCIATION_CODE == AssociationCode && al.ACTIVE == "Y"
                            select new DTO.DataItem
                            {
                                Id = lt.LICENSE_TYPE_CODE,
                                Name = lt.LICENSE_TYPE_NAME
                            }).ToList();

                res.DataResponse = list;

                return res;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<string>> GetCompanyCodeAsCompanyT(string anyText)
        {
            DTO.ResponseService<List<string>> res = new DTO.ResponseService<List<string>>();

            try
            {
                var list = ctx.VW_AS_COMPANY_T
                              .Where(w => w.COMP_NAMET.StartsWith(anyText))
                              .Select(s => s.COMP_NAMET + " [" + s.COMP_CODE + "]").ToList();

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetDefaultcompanyName(string Id)
        {
            var res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = "select COMP_NAMET||' '||'['||COMP_CODE||']' comName from agdoi.vw_as_company_t "
                           + "where comp_code = '" + Id + "' ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetCompanyByLicenseType(string licenseTypeCode)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new ResponseService<List<DataItem>>();

            try
            {
                if (!string.IsNullOrEmpty(licenseTypeCode))
                {
                    if (licenseTypeCode == "01" || licenseTypeCode == "07")
                    {
                        var ls =
                            base.ctx.VW_IAS_COM_CODE.Where(w => w.ID.StartsWith("1"))
                                .OrderBy(x => x.ID)
                                .Select(s => new DTO.DataItem
                                {
                                    Id = s.ID,
                                    Name = s.NAME
                                }).ToList();
                        res.DataResponse = ls;
                    }
                    else if (licenseTypeCode == "02" || licenseTypeCode == "05" || licenseTypeCode == "06" || licenseTypeCode == "08")
                    {
                        var ls =
                            base.ctx.VW_IAS_COM_CODE.Where(w => w.ID.StartsWith("2"))
                                .OrderBy(x => x.ID)
                                .Select(s => new DataItem
                                {
                                    Id = s.ID,
                                    Name = s.NAME
                                }).ToList();
                        res.DataResponse = ls;
                    }
                    else if (licenseTypeCode == "03" || licenseTypeCode == "04")
                    {
                        var ls =
                            base.ctx.VW_IAS_COM_CODE.Where(w => w.ID.StartsWith("3"))
                                .OrderBy(x => x.ID)
                                .Select(s => new DataItem { Id = s.ID, Name = s.NAME })
                                .ToList();
                        res.DataResponse = ls;
                    }
                }
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<string> GetExamPerBill()
        {
            var res = new DTO.ResponseService<string>();

            try
            {
                var ent = base.ctx.AG_IAS_CONFIG
                                  .Where(w => w.ID == "08")
                                  .FirstOrDefault();
                if (ent != null)
                {
                    res.DataResponse = ent.ITEM_VALUE;
                }
                else
                {
                    res.DataResponse = "3";
                }


            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;

        }

        public DTO.ResponseService<DataSet> GetAssByAssCodeAndAssName(string ID, string name, string compType, string aType)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = "select * from ag_ias_association where association_code like '" + ID + "%' and association_name like '" + name + "%' and active ='Y' " +
                            " and comp_type = '" + compType + "' and agent_type ='" + aType + "' ";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch
            {
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.TitleName>> GetTitleNameFromSex(string sex)
        {
            DTO.ResponseService<List<DTO.TitleName>> res = new DTO.ResponseService<List<DTO.TitleName>>();

            List<string> TF = new List<string>() { "หญิง", "นาง", "แม่ชี", "Mrs.", "Ms.", "นางสาว" };
            try
            {
                //Get All Title
                var ls = base.ctx.VW_IAS_TITLE_NAME_PRIORITY.ToList();

                //Get All Female
                List<VW_IAS_TITLE_NAME_PRIORITY> listF = new List<VW_IAS_TITLE_NAME_PRIORITY>();
                for (int i = 0; i < TF.Count; i++)
                {
                    string curr = TF[i];
                    var resx = base.ctx.VW_IAS_TITLE_NAME_PRIORITY.Where(s => s.NAME.Contains(curr)).ToList();
                    if (listF.Count == 0)
                    {
                        listF = resx;
                    }
                    else
                    {
                        listF = resx.Union(listF.ToList()).ToList();
                    }

                }

                if (sex.Equals("M"))
                {
                    var resultM = ls.Except(listF).ToList();
                    res.DataResponse = resultM.Select(s => new DTO.TitleName
                    {
                        PRE_CODE = s.ID,
                        FULL_NAME = s.NAME

                    }).ToList();
                }
                else if (sex.Equals("F"))
                {
                    var resultF = ls.Intersect(listF).ToList();
                    res.DataResponse = resultF.Select(s => new DTO.TitleName
                    {
                        PRE_CODE = s.ID,
                        FULL_NAME = s.NAME

                    }).ToList();
                }

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        /// <summary>
        /// ดึงข้อมูลตั้งค่า Config เอกสารสมัครสมาชิก
        /// </summary>
        /// <returns>ResponseService<List<ConfigEntity>></returns>
        public DTO.ResponseService<List<DTO.ConfigDocument>> GetDocumentConfigApproveMemberByApplicant()
        {
            var res = new DTO.ResponseService<List<DTO.ConfigDocument>>();
            try
            {
                var list = (from at in base.ctx.AG_IAS_DOCUMENT_TYPE
                            join att in base.ctx.AG_IAS_DOCUMENT_TYPE_T
                                        on at.DOCUMENT_CODE equals att.DOCUMENT_CODE
                            join m in base.ctx.AG_IAS_MEMBER_TYPE on att.MEMBER_CODE equals m.MEMBER_CODE
                            where att.STATUS == "A" && att.FUNCTION_ID == "64"
                            orderby att.MEMBER_CODE descending
                            select new DTO.ConfigDocument
                            {
                                ID = att.ID,
                                FUNCTION_ID = att.FUNCTION_ID,
                                MEMBER_CODE = att.MEMBER_CODE,
                                DOCUMENT_CODE = att.DOCUMENT_CODE,
                                DOCUMENT_REQUIRE = att.DOCUMENT_REQUIRE,
                                DOCUMENT_NAME = at.DOCUMENT_NAME,
                                MEMBER_NAME = m.MEMBER_NAME
                            }).ToList();

                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        public DTO.ResponseService<DataSet> GetExamPlaceGroupR(string ExamPlace_Code)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                if (ExamPlace_Code == "")
                    ExamPlace_Code = "%%";

                string sql = "select exam_place_group_code, exam_place_group_name, active "
                            + "from AG_EXAM_PLACE_GROUP_R where ACTIVE='Y'  and exam_place_group_code like '" + ExamPlace_Code + "' order by exam_place_group_code asc";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetExamPlaceGroupRByIDName(string ID, string name, int pageNo, int recordPerPage, Boolean Count)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;
                if (!Count)
                {
                    //sql = "select exam_place_group_code,exam_place_group_name,user_id,user_date,active,update_by,update_date,ROW_NUMBER() OVER (ORDER BY exam_place_group_code asc) RUN_NO from AG_EXAM_PLACE_GROUP_R where exam_place_group_code like '" + ID + "%' and exam_place_group_name like '" + name + "%' and active ='Y'  order by exam_place_group_code asc "

                    sql = "select * from(select exam_place_group_code,exam_place_group_name,user_id,user_date,active,updated_by,updated_date, "
                        + "ROW_NUMBER() OVER (ORDER BY exam_place_group_code asc) as RUN_NO from AG_EXAM_PLACE_GROUP_R "
                        + "where exam_place_group_code like '" + ID + "%' and exam_place_group_name like '" + name + "%' and active ='Y'  )a "
                        + "where a.RUN_NO between " + pageNo.StartRowNumber(recordPerPage).ToString() + " and " + pageNo.ToRowNumber(recordPerPage).ToString()
                        + " order by exam_place_group_code asc ";

                }
                else
                {
                    sql = "select count (*) CCount from( "
                        + "select * from(select exam_place_group_code,exam_place_group_name,user_id,user_date,active,updated_by,updated_date, "
                        + "ROW_NUMBER() OVER (ORDER BY exam_place_group_code asc) as RUN_NO from AG_EXAM_PLACE_GROUP_R "
                        + "where exam_place_group_code like '" + ID + "%' and exam_place_group_name like '" + name + "%' and active ='Y'  )a "
                        + "order by exam_place_group_code asc) ";
                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch
            {
            }
            return res;
        }

        public DTO.ResponseMessage<bool> InsertExamPlaceGroupR(DTO.ConfigAssociation ent, DTO.UserProfile userProfile, List<DTO.AssociationLicense> license)
        {
            DTO.ResponseMessage<bool> res = new DTO.ResponseMessage<bool>();
            try
            {
                var exam = base.ctx.AG_EXAM_PLACE_GROUP_R.FirstOrDefault(s => s.EXAM_PLACE_GROUP_CODE == ent.ASSOCIATION_CODE);
                AG_EXAM_PLACE_GROUP_R table = new AG_EXAM_PLACE_GROUP_R();
                if (exam == null)//new
                {

                    ent.MappingToEntity(table);
                    table.EXAM_PLACE_GROUP_CODE = ent.ASSOCIATION_CODE;
                    table.EXAM_PLACE_GROUP_NAME = ent.ASSOCIATION_NAME;
                    table.ACTIVE = ent.ACTIVE;
                    table.USER_ID = userProfile.Id;
                    table.USER_DATE = DateTime.Now;
                    base.ctx.AG_EXAM_PLACE_GROUP_R.AddObject(table);

                }
                else
                {
                    if (ent.ACTIVE == "Y")//แก้ไข
                    {
                        var exam2 = base.ctx.AG_EXAM_PLACE_GROUP_R.FirstOrDefault(s => s.EXAM_PLACE_GROUP_CODE == ent.ASSOCIATION_CODE && s.ACTIVE == "N");
                        if (exam2 != null)
                        {
                            res.ErrorMsg = "ไม่สามารถเพิ่มข้อมูลได้<br/>เนื่องจาก มีการยกเลิกการใช้งานรหัสหน่วยงานจัดสอบ " + ent.ASSOCIATION_CODE + " แล้ว<br/> กรุณาใช้รหัสหน่วยงานจัดสอบอื่น";
                        }
                        else
                        {
                            if (ent.UPDATED_BY != null && ent.UPDATED_DATE != null)
                            {
                                exam.EXAM_PLACE_GROUP_CODE = ent.ASSOCIATION_CODE;
                                exam.EXAM_PLACE_GROUP_NAME = ent.ASSOCIATION_NAME;
                                exam.UPDATED_BY = userProfile.Id;
                                exam.UPDATED_DATE = DateTime.Now;
                                exam.ACTIVE = ent.ACTIVE;
                            }
                            else
                            {
                                res.ErrorMsg = "มีรหัสหน่วยงานจัดสอบ " + ent.ASSOCIATION_CODE + " แล้ว<br/> กรุณาใช้การแก้ไขแทนการเพิ่มข้อมูลใหม่";

                            }
                        }
                    }
                    else//ลบ
                    {
                        exam.UPDATED_BY = userProfile.Id;
                        exam.UPDATED_DATE = DateTime.Now;
                        exam.ACTIVE = ent.ACTIVE;

                    }
                    base.ctx.AG_EXAM_PLACE_GROUP_R.MappingToEntity(exam);
                }
                base.ctx.SaveChanges();
                //int cntCode = base.ctx.AG_EXAM_PLACE_GROUP_R.Where(x => x.EXAM_PLACE_GROUP_CODE == ent.ASSOCIATION_CODE).Count();
                //int cntName = base.ctx.AG_EXAM_PLACE_GROUP_R.Where(x => x.EXAM_PLACE_GROUP_NAME == ent.ASSOCIATION_NAME).Count();
                //if (cntCode > 0)
                //{
                //    res.ErrorMsg = Resources.errorDataCenterService_001;
                //    return res;
                //}
                //if (cntName > 0)
                //{
                //    res.ErrorMsg = Resources.errorDataCenterService_002;
                //    return res;
                //}


                //base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<DataSet> GetExamPlaceGroupRByCheckID(string ID, string name)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = "select * from AG_EXAM_PLACE_GROUP_R where exam_place_group_code ='" + ID + "'";
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch
            {
            }
            return res;
        }

        public ResponseService<DataSet> GetAssociationByByCriteria(string Code, string Name, string CompType, string AgentType, int NumPage, int RowPerPage, bool Count, bool? IsActive)
        {
            DTO.ResponseService<DataSet> res = new ResponseService<DataSet>();
            try
            {
                string sql = "";
                if (Count)
                {
                    sql += "select count(*) TotalRows from ( ";
                }
                else
                {
                    sql += " select * from ( ";
                }

                sql += " select rownum as num, ASSOCIATION_CODE, ASSOCIATION_NAME, ACTIVE, COMP_TYPE, AGENT_TYPE "
                       + " from AG_IAS_ASSOCIATION ";

                List<string> where = new List<string>();
                if (!string.IsNullOrEmpty(Code))
                    where.Add(string.Format(" ASSOCIATION_CODE like '{0}%' ", Code));

                if (!string.IsNullOrEmpty(Name))
                    where.Add(string.Format(" ASSOCIATION_NAME like '%{0}%' ", Name));

                if (!string.IsNullOrEmpty(CompType))
                    where.Add(string.Format(" COMP_TYPE = '{0}' ", CompType));

                if (!string.IsNullOrEmpty(AgentType))
                    where.Add(string.Format(" AGENT_TYPE = '{0}' ", AgentType));

                if (IsActive != null)
                    where.Add(string.Format(" ACTIVE = '{0}' ", (IsActive == true) ? "Y" : "N"));

                if (where.Count > 0)
                    sql += " where ";
                int loopWhere = 0;
                foreach (string e in where)
                {
                    sql += e + (loopWhere < where.Count - 1 ? " and " : "");
                    loopWhere++;
                }

                if (Count)
                {
                    sql += " ) ";
                }
                else
                {
                    sql += " ) where num between " + (((NumPage * RowPerPage) - RowPerPage) + 1) + " and " + (NumPage * RowPerPage);
                }

                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        /// <summary>
        /// AUTO COMPLETE GetInsuranceAssociate
        /// </summary>
        /// <param name="anyText"></param>
        /// <returns></returns>
        /// <AUTHOR>Natta</AUTHOR>
        /// <LASTUPDATE>07/05/2557</LASTUPDATE>
        public DTO.ResponseService<List<string>> GetInsuranceAssociate(string anyText)
        {
            DTO.ResponseService<List<string>> res = new DTO.ResponseService<List<string>>();

            try
            {
                //Get Insurance Associate List
                var assoList = (from ass in base.ctx.AG_IAS_ASSOCIATION
                                where ass.ACTIVE.Equals("Y")
                                select new DTO.InsuranceAssociate
                                {
                                    Id = ass.ASSOCIATION_CODE,
                                    Name = ass.ASSOCIATION_NAME,

                                }).ToList();

                var list = assoList
                             .Where(w => w.Name.Contains(anyText))
                             .Select(s => s.Name + " [" + s.Id + "]").ToList();

                res.DataResponse = list;

                return res;
            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("DatacenterService_GetInsuranceAssociate", ex.Message);
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }

            return res;
        }

        /// <summary>
        /// GetInsuranceAssociateNameByID
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        /// <AUTHOR>Natta</AUTHOR>
        /// <LASTUPDATE>07/05/2557</LASTUPDATE>
        public DTO.ResponseService<DTO.ASSOCIATION> GetInsuranceAssociateNameByID(string AssID)
        {
            var res = new DTO.ResponseService<DTO.ASSOCIATION>();
            try
            {
                var result = (from a in base.ctx.AG_IAS_ASSOCIATION
                              where a.ASSOCIATION_CODE == AssID
                              select new DTO.ASSOCIATION
                              {
                                  ASSOCIATION_CODE = a.ASSOCIATION_CODE,
                                  ASSOCIATION_NAME = a.ASSOCIATION_NAME

                              }).FirstOrDefault();

                if (result != null)
                {
                    res.DataResponse = result;
                }

            }
            catch (Exception ex)
            {
                LoggerFactory.CreateLog().Fatal("DatacenterService_GetInsuranceAssociateNameByID", ex.Message);
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamSpecialDocument(List<string> fileType)
        {

            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                            from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                            where
                            dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                            && dt.EXAM_DISCOUNT_STATUS == "Y"
                            && fileType.Contains(dt.DOCUMENT_CODE)
                            && dt.STATUS == "A"
                            select new DTO.DataItem
                            {
                                Id = dt.DOCUMENT_CODE,
                                Name = ex.SPECIAL_TYPE_DESC
                            }).ToList();



                res.DataResponse = list;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดโปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamSpecialDocument", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetExamSpecial(string idCard, string licenseType)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            var tagUser = new string[] { "" };


            try
            {
                var agenType = ctx.AG_IAS_LICENSE_TYPE_R.FirstOrDefault(a => a.LICENSE_TYPE_CODE.Trim() == licenseType.Trim());

                if (agenType.INSURANCE_TYPE == "1")
                {
                    tagUser = new string[] { "L", "B" };
                }
                else if (agenType.INSURANCE_TYPE == "2")
                {
                    tagUser = new string[] { "D", "B" };
                }
                else
                {
                    tagUser = new string[] { "B" };
                }


                var list = (from a in ctx.AG_IAS_EXAM_SPECIAL_T
                            from ex in ctx.AG_IAS_EXAM_SPECIAL_R
                            from dt in ctx.AG_IAS_DOCUMENT_TYPE
                            where
                            a.ID_CARD_NO.Trim() == idCard.Trim()
                            && tagUser.Contains(ex.USED_TYPE.Trim())
                            && dt.SPECIAL_TYPE_CODE_EXAM == ex.SPECIAL_TYPE_CODE
                            select new DTO.DataItem
                            {
                                Id = dt.DOCUMENT_CODE,
                                Name = dt.DOCUMENT_NAME
                            }).ToList();



                res.DataResponse = list;

            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดโปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamSpecial", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetTrainSpecialDocument(List<string> fileType)
        {

            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = (from dt in ctx.AG_IAS_DOCUMENT_TYPE
                            from ts in ctx.AG_TRAIN_SPECIAL_R
                            where
                            dt.SPECIAL_TYPE_CODE_TRAIN == ts.SPECIAL_TYPE_CODE
                            && dt.TRAIN_DISCOUNT_STATUS == "Y"
                            && fileType.Contains(dt.DOCUMENT_CODE)
                            && dt.STATUS == "A"
                            select new DTO.DataItem
                            {
                                Id = dt.DOCUMENT_CODE,
                                Name = ts.SPECIAL_TYPE_DESC
                            }).ToList();



                res.DataResponse = list;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "พบข้อผิดพลาดโปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetExamSpecialDocument", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetTrainSpecialbyIdCard(string idCard)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            string sql = string.Empty;



            try
            {

                sql = "SELECT tr.SPECIAL_TYPE_CODE Id,tr.SPECIAL_TYPE_DESC Name "
                    + "FROM AG_TRAIN_SPECIAL_T ts,AG_TRAIN_SPECIAL_R tr "
                    + "WHERE ts.SPECIAL_TYPE_CODE = tr.SPECIAL_TYPE_CODE "
                    + "AND ts.ID_CARD_NO = '" + idCard.Trim() + "' ";


                OracleDB ora = new OracleDB();
                DataTable dt = ora.GetDataTable(sql);

                if (dt != null)
                {
                    List<DTO.DataItem> list = new List<DTO.DataItem>();

                    //list.Add(new DTO.DataItem { Id = "", Name = firstItem });

                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new DTO.DataItem { Id = dr["Id"].ToString(), Name = dr["Name"].ToString() });
                    }

                    res.DataResponse = list;
                }

                else
                {
                    res.DataResponse = null;
                }


            }
            catch (Exception ex)
            {

                res.ErrorMsg = "พบข้อผิดพลาดโปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetTrainSpecialbyIdCard", ex);
            }
            return res;

        }

        public DTO.ResponseService<List<DTO.DataItem>> GetAssociationApprove(string lincenseType)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();
            try
            {
                var list = (from ad in ctx.AG_IAS_APPROVE_DOC_TYPE
                            from aa in ctx.AG_IAS_ASSOCIATION_APPROVE
                            from aso in ctx.AG_IAS_ASSOCIATION
                            where
                            ad.APPROVE_DOC_TYPE == aa.APPROVE_DOC_TYPE
                            && aa.ASSOCIATION_CODE == aso.ASSOCIATION_CODE
                            && ad.APPROVE_DOC_TYPE == lincenseType.Trim()
                            && ad.ITEM_VALUE == "Y"
                            && aa.STATUS == "Y"
                            select new DTO.DataItem
                            {
                                Id = aso.ASSOCIATION_CODE,
                                Name = aso.ASSOCIATION_NAME
                            }).ToList();

                res.DataResponse = list;
            }
            catch (Exception ex)
            {

                res.ErrorMsg = "พบข้อผิดพลาดโปรดติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetAssociationApprove", ex);
            }

            return res;

        }

        public DTO.ResponseService<string> GetLicensefromTestingNo(string TestingNo)
        {
            DTO.ResponseService<string> res = new DTO.ResponseService<string>();
            try
            {
                string list = (from lic in ctx.AG_EXAM_LICENSE_R
                               from licName in ctx.AG_IAS_LICENSE_TYPE_R
                               where licName.ACTIVE_FLAG == "Y"
                               && lic.TESTING_NO == TestingNo
                               && lic.LICENSE_TYPE_CODE == licName.LICENSE_TYPE_CODE
                               select lic.LICENSE_TYPE_CODE).FirstOrDefault().ToString(); ;
                res.DataResponse = list;
            }
            catch (Exception ex)
            {

                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetLicensefromTestingNo", ex);
            }

            return res;
        }

        public DTO.ResponseService<List<DTO.ConfigEntity>> GetConfigCheckExamLicense()
        {
            DTO.ResponseService<List<DTO.ConfigEntity>> res = new ResponseService<List<ConfigEntity>>();
            try
            {
                var qry = base.ctx.AG_IAS_CONFIG
                            .Where(s => s.ID == "11" || s.ID == "12")
                            .OrderBy(s => s.ID)
                            .Select(s => new DTO.ConfigEntity()
                            {
                                Id = s.ID,
                                Name = s.ITEM,
                                Item_Value = s.ITEM_VALUE,
                                Description = s.DESCRIPTION
                            }).ToList();
                res.DataResponse = qry;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetConfigCheckExamLicense", ex);
            }
            return res;
        }

        public DTO.ResponseMessage<bool> UpdateConfigCheckExamLicense(List<DTO.ConfigEntity> cfgEnt, DTO.UserProfile userProfile)
        {
            DTO.ResponseMessage<bool> res = new ResponseMessage<bool>();
            try
            {
                foreach (var cfg in cfgEnt)
                {
                    var qry = base.ctx.AG_IAS_CONFIG.FirstOrDefault(s => s.ID == cfg.Id);
                    if (qry != null)
                    {
                        qry.ITEM_VALUE = cfg.Item_Value;
                        qry.USER_ID = userProfile.Id;
                        qry.USER_DATE = DateTime.Now;
                    }
                }
                base.ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_UpdateConfigCheckExamLicense", ex);
            }
            return res;
        }


        public DTO.ResponseService<string> GetConficValueByTypeAndGroupCode(string ID, string GroupCode)
        {
            var Auto = new DTO.ResponseService<string>();
            try
            {
                var Manual = ctx.AG_IAS_CONFIG.FirstOrDefault(x => x.ID == ID && x.GROUP_CODE == GroupCode);
                if (Manual != null)
                    Auto.DataResponse = Manual.ITEM_VALUE;
                else
                    Auto.DataResponse = "0";
            }
            catch (Exception ex)
            {
                Auto.ErrorMsg = "พบข้อผิดพลาด";
                LoggerFactory.CreateLog().Fatal("DataCenterervice_GetConficValueByTypeAndGroupCode", ex);
            }
            return Auto;
        }

        public DTO.ResponseService<DataSet> GetUserVerifyDoc(string compcode)
        {
            DTO.ResponseService<DataSet> res = new DTO.ResponseService<DataSet>();
            try
            {
                string sql = string.Empty;
                {
                    sql = "select distinct "
                               + "case LENGTH(lh.upload_by_session) when 15  then '000000000000000' else lh.upload_by_session  end as upload_by_session, "
                               + "case LENGTH(lh.upload_by_session) when 15  then 'บุคคลทั่วไป/ตัวแทน/นายหน้า'  when 4 then com.name else asso.association_name  end as names "
                               + "from  AG_IAS_LICENSE_D LD "
                               + "join AG_IAS_LICENSE_H LH on LD.UPLOAD_GROUP_NO = LH.UPLOAD_GROUP_NO "
                               + "join AG_LICENSE_TYPE_R LR on LH.LICENSE_TYPE_CODE = LR.LICENSE_TYPE_CODE "
                               + "left join VW_IAS_COM_CODE COM on LH.upload_by_session =COM.ID "
                               + "left join AG_IAS_ASSOCIATION ASSO on LH.upload_by_session=ASSO.association_code "
                         + "where LH.APPROVE_COMPCODE ='" + compcode + "' "
                         + "and lh.upload_by_session is not null "
                         + "order by upload_by_session,names";

                }
                OracleDB ora = new OracleDB();
                res.DataResponse = ora.GetDataSet(sql);
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("DataCenterService_GetUserVerifyDoc", ex);
            }
            return res;
        }

        public DTO.ResponseService<List<DTO.DataItem>> GetMemberTypeRegister(string item)
        {
            DTO.ResponseService<List<DTO.DataItem>> res = new DTO.ResponseService<List<DTO.DataItem>>();

            try
            {
               var list = from p in ctx.AG_IAS_MEMBER_TYPE where p.MEMBER_CODE == "1" || p.MEMBER_CODE == "2" || p.MEMBER_CODE == "3" select new DataItem { Id = p.MEMBER_CODE ,Name = p.MEMBER_NAME  };

               res.DataResponse = list.ToList();
                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMsg = "กรุณาติดต่อผู้ดูแลระบบ";
                LoggerFactory.CreateLog().Fatal("GetMemberTypeRegister", ex);
            }

            return res;
        }
    }
}
