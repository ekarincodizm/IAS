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
using IAS.DataServices.Applicant.ApplicantHelper;

namespace IAS.DataServices.Applicant.ApplicantRequestUploads
{
    public class ApplicantFileFactory
    {


        public static DTO.ResponseService<ApplicantFileHeader> ConcreateApplicantFileRequest(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.ApplicantUploadRequest request)
        {
            DTO.ResponseService<ApplicantFileHeader> response = new DTO.ResponseService<ApplicantFileHeader>();

            ApplicantFileHeader header = CreateApplicantFileHeader(ctx, request);
            Int32 row = 0;

            foreach (String record in request.UploadData.Body)
            {
                row++;
                ApplicantFileDetail detail = CreateApplicantFileDetail(ctx, record, row);
                header.AddDetail(detail);
            }
            response.DataResponse = header;

            return response;
        }


        private static String PhaseSex(String sex)
        {
            switch (sex)
            {
                case "ช": return "M";
                case "ญ": return "F";
                default: return sex;

            }
        }




        private static ApplicantFileHeader CreateApplicantFileHeader(IAS.DAL.Interfaces.IIASPersonEntities ctx, DTO.ApplicantUploadRequest request)
        {
            String[] header = request.UploadData.Header.Split(',');

            ApplicantHeaderRequest headRequest = new ApplicantHeaderRequest()
                                                        {
                                                            Context = ctx,
                                                            FileName = request.FileName,
                                                            TestingNumber = request.TestingNo,
                                                            ExamPlaceCode = request.ExamPlaceCode,
                                                            UserProfile = request.UserProfile,
                                                            LineData = header
                                                        };
            //ApplicantFileHeader headerFile = new ApplicantFileHeader(headRequest)
            //                                        {
            //                                            PROVINCE_CODE = header.GetIndexOf(1),
            //                                            COMP_CODE = header.GetIndexOf(2),
            //                                            LICENSE_TYPE_CODE = header.GetIndexOf(3),
            //                                            TESTING_DATE = PhaseDateHelper.PhaseToDateNull(header.GetIndexOf(4)),
            //                                            EXAM_APPLY = PhaseApplyAmountHelper.Phase(header.GetIndexOf(5)),
            //                                            EXAM_AMOUNT = PhaseCurrencyAmount.Phase(header.GetIndexOf(6)),
            //                                            TEST_TIME_CODE = header.GetIndexOf(7),
            //                                        };

            //IQueryable<AG_EXAM_LICENSE_R> examLicense = ctx.AG_EXAM_LICENSE_R.Where(w => request.TestingNo.Contains(w.TESTING_NO) && request.ExamPlaceCode.Contains(w.EXAM_PLACE_CODE));
            var examLicense = ctx.AG_EXAM_LICENSE_R.Where(w => w.TESTING_NO == request.TestingNo && w.EXAM_PLACE_CODE == request.ExamPlaceCode).FirstOrDefault();
            var exmPlace = ctx.AG_EXAM_PLACE_R.Where(w => w.EXAM_PLACE_CODE == examLicense.EXAM_PLACE_CODE).FirstOrDefault();
            int examFee = ctx.AG_PETITION_TYPE_R.FirstOrDefault(s => s.PETITION_TYPE_CODE == "01").FEE.ToInt();
            ApplicantFileHeader headerFile = new ApplicantFileHeader(headRequest)
                                                    {
                                                        PROVINCE_CODE = exmPlace.PROVINCE_CODE,
                                                        COMP_CODE = string.IsNullOrEmpty(exmPlace.EXAM_PLACE_GROUP_CODE) ? exmPlace.ASSOCIATION_CODE : exmPlace.EXAM_PLACE_GROUP_CODE,
                                                        LICENSE_TYPE_CODE = header.GetIndexOf(3),
                                                        TESTING_DATE = examLicense.TESTING_DATE,
                                                        EXAM_APPLY = PhaseApplyAmountHelper.Phase(header.GetIndexOf(5)),
                                                        EXAM_AMOUNT = PhaseCurrencyAmount.Phase(header.GetIndexOf(5)) * examFee,
                                                        TEST_TIME_CODE = examLicense.TEST_TIME_CODE,
                                                    };

            return headerFile;
        }


        private static ApplicantFileDetail CreateApplicantFileDetail(IAS.DAL.Interfaces.IIASPersonEntities ctx, String rawData, Int32 rownum)
        {
            String[] data = rawData.Split(',');

            ApplicantFileDetail detail = new ApplicantFileDetail(ctx)
            {
                SEQ_NO = rownum.ToString("0000"),
                LOAD_STATUS = "",
                APPLICANT_CODE = PhaseAppliantCodeHelper.Phase(data.GetIndexOf(0)),
                ID_CARD_NO = data.GetIndexOf(1),
                PRE_NAME_CODE = PreNameHelper.ConvertToCode(ctx, data.GetIndexOf(2)),
                NAMES = data.GetIndexOf(3),
                LASTNAME = data.GetIndexOf(4),
                BIRTH_DATE = PhaseDateHelper.PhaseToDateNull(data.GetIndexOf(5)),
                SEX = PhaseSex(data.GetIndexOf(6)),
                EDUCATION_CODE = EducationCodeHelper.Phase(data.GetIndexOf(7)),
                ADDRESS1 = data.GetIndexOf(9),
                AREA_CODE = data.GetIndexOf(10),
                INSUR_COMP_CODE = data.GetIndexOf(8),
                TITLE = data.GetIndexOf(2),
            };

            return detail;
        }


        private static String GetDataField(String[] fields, Int32 index)
        {
            return (fields.Length > index) ? fields[index] : "";
        }



    }
}