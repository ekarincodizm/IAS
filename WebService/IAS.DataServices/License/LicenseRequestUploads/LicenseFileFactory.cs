using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using IAS.DataServices.License.LicenseHelpers;
using System.IO;
using IAS.Utils;
using System.Configuration;
using System.Text;
using IAS.DTO;

namespace IAS.DataServices.Payment.TransactionBanking
{
    public class LicenseFileFactory   
    {


        public static DTO.ResponseService<LicenseFileHeader> ConcreateLicenseRequest(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.DataLicenseRequest request)  
        {
            DTO.ResponseService<LicenseFileHeader> response = new DTO.ResponseService<LicenseFileHeader>();

            DTO.ResponseService<DTO.CompressFileDetail> resCompress = ExtractFileLicenseRequestHelper.ExtractFile(request.FileName);
            if (resCompress.IsError) {
                response.ErrorMsg = resCompress.ErrorMsg;
                return response;
            }

            DTO.ResponseService<DTO.UploadData> resData = DataLicenseRequestReaderHelper.ReadDataFromFile(resCompress.DataResponse.TextFilePath);
            if (resData.IsError) {
                response.ErrorMsg = resData.ErrorMsg;
                return response;
            }

            LicenseFileHeader header = new LicenseFileHeader(ctx, request.UserProfile, resCompress.DataResponse.TextFilePath, request.PettitionTypeCode, request.LicenseTypeCode);
            Int32 row = 0;
            header.replaceType = request.ReplaceType;
            CreateLicenseFileHeader(ctx, header, resData.DataResponse.Header, resCompress.DataResponse,request.ApproveCom);

            foreach (String record in resData.DataResponse.Body)
            {
                row++;
                LicenseFileDetail detail = CreateLicenseFileDetail(ctx, record, row);
               
                header.AddDetail(detail,request.ReplaceType);
                
            } 
            response.DataResponse = header;

            return response;
        }







        private static LicenseFileHeader CreateLicenseFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx, LicenseFileHeader header, String lineData, CompressFileDetail compress, string approveCom)
        {
            String[] rowDatas = lineData.Split(',');
            String licenseTypeCode = GetDataField(rowDatas, 3);


           // AG_IAS_APPROVE_DOC_TYPE comp = ctx.AG_IAS_APPROVE_DOC_TYPE.Where(w => w.APPROVE_DOC_TYPE == licenseTypeCode && w.ITEM_VALUE == "Y").FirstOrDefault();
            


            header.IMPORT_DATETIME = DateTime.Now;
            header.LICENSE_TYPE_CODE = licenseTypeCode;
            header.COMP_CODE = GetDataField(rowDatas, 1);
            header.COMP_NAME = GetDataField(rowDatas, 2);
            header.LICENSE_TYPE = licenseTypeCode;
            header.SEND_DATE = LicenseFileHelper.PhaseToDate(GetDataField(rowDatas, 4));
            header.TOTAL_AGENT = LicenseFileHelper.PhaseToAmount(GetDataField(rowDatas, 5));
            header.TOTAL_FEE = LicenseFileHelper.PhaseToMoney(GetDataField(rowDatas, 6));
            header.ERR_MSG = "";

            if (string.IsNullOrEmpty(approveCom))
            {
                approveCom = null;
            }

            header.APPROVE_COMPCODE = approveCom == null ? null : approveCom;

            AG_IAS_PETITION_TYPE_R petitionType = ctx.AG_IAS_PETITION_TYPE_R.SingleOrDefault(a => a.PETITION_TYPE_CODE == header.PettitionTypeCodeRequest);
            header.PetitionTypeR = petitionType;


            AG_IAS_LICENSE_TYPE_R licenseType = ctx.AG_IAS_LICENSE_TYPE_R.SingleOrDefault(a => a.LICENSE_TYPE_CODE == header.LicenseTypeCodeRequest);
            header.LicenseTypeR = licenseType;

            header.CompressFileDetail = compress;
            return header;
        }


        private static LicenseFileDetail CreateLicenseFileDetail(IAS.DAL.Interfaces.IIASPersonEntities ctx, String rawData, Int32 rownum)   
        {
            //IMPORT_ID = recHead.IMPORT_ID,
            //COMP_CODE = recHead.COMP_CODE,
             //ERR_MSG = errMsg,
             //PETITION_TYPE = petitionTypeCode,
            string[] rowDatas = rawData.Split(',');
            Int32 seq = Convert.ToInt32(rowDatas[0]); 
            LicenseFileDetail detail = new LicenseFileDetail(ctx)
            {
                Sequence = seq.ToString("0000"),
                SEQ = rownum.ToString("0000"), // seq.ToString("0000"),
                LICENSE_NO = rowDatas[1],
                LICENSE_ACTIVE_DATE = LicenseFileHelper.PhaseToDateNull(rowDatas[2]),// issueDate,
                LICENSE_EXPIRE_DATE = LicenseFileHelper.PhaseToDateNull(rowDatas[3]),  // expireDate,
                LICENSE_FEE = LicenseFileHelper.PhaseToMoney(rowDatas[4]),
                CITIZEN_ID = GetDataField(rowDatas, 5),
                TITLE_NAME = GetDataField(rowDatas, 6),
                NAME = GetDataField(rowDatas, 7),
                SURNAME = GetDataField(rowDatas, 8),
                ADDR1 = GetDataField(rowDatas, 9),
                ADDR2 = GetDataField(rowDatas, 10),
                AREA_CODE = GetDataField(rowDatas, 11),
                EMAIL = GetDataField(rowDatas, 12),
                CUR_ADDR = GetDataField(rowDatas, 13),
                TEL_NO = GetDataField(rowDatas, 14),
                CUR_AREA_CODE = GetDataField(rowDatas, 15),
                AR_ANSWER = LicenseFileHelper.PhaseARDate(GetDataField(rowDatas, 16)),
                OLD_COMP_CODE = GetDataField(rowDatas, 17),
                SPECIAL_TYPE_CODE = GetDataField(rowDatas,18),
                START_DATE = LicenseFileHelper.PhaseStartDate(GetDataField(rowDatas, 19))

            };


            return detail;
        }


        private static String GetDataField(String[] fields, Int32 index)
        {
            return (fields.Length > index) ? fields[index] : "";
        }
    


    }
}