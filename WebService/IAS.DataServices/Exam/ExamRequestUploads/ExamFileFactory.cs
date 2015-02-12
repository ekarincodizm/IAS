using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IAS.DAL;
using System.IO;
using IAS.Utils;
using System.Configuration;
using System.Text;
using IAS.DTO;

namespace IAS.DataServices.Exam.ExamRequestUploads 
{
    public class ExamFileFactory   
    {
                            

        public static DTO.ResponseService<ExamFileHeader> ConcreateApplicantFileRequest(IAS.DAL.IASPersonEntities ctx, DTO.ApplicantUploadRequest request)  
        {
            DTO.ResponseService<ExamFileHeader> response = new DTO.ResponseService<ExamFileHeader>();

            ExamFileHeader header = CreateExamFileHeader(ctx, request);
            Int32 row = 0;

            foreach (String record in request.UploadData.Body)
            {
                row++;
                ExamFileDetail detail = CreateExamFileDetail(ctx, record, row);
                header.AddDetail(detail);
            }
            response.DataResponse = header;

            return response;
        }
        
        private static ExamFileHeader CreateExamFileHeader(IAS.DAL.IASPersonEntities ctx, DTO.ApplicantUploadRequest request)
        {
            String header = request.UploadData.Header;

            ExamFileHeaderRequest headRequest = new ExamFileHeaderRequest()
            {
                Context = ctx,
                FileName = request.FileName,
                
                UserProfile = request.UserProfile,
                LineData = request.UploadData.Header
            };
            ExamFileHeader headerFile = new ExamFileHeader(headRequest);

           
            return headerFile;
        }
        
        private static ExamFileDetail CreateExamFileDetail(IAS.DAL.IASPersonEntities ctx, String rawData, Int32 rownum)
        {

            String[] data = rawData.Split('|');
              
            ExamFileDetail detail = new ExamFileDetail(ctx)
            {
                SEQ_NO = rownum.ToString("0000"),
              
                SEAT_NO = data.GetIndexOf(1),
                ID_CARD_NO =  data.GetIndexOf(2),
                TITLE =  data.GetIndexOf(3),
                NAMES =  data.GetIndexOf(4),
                LAST_NAME =  data.GetIndexOf(5),
                ADDRESS1 =  data.GetIndexOf(6),
                ADDRESS2 =  data.GetIndexOf(7),
                AREA_CODE =  data.GetIndexOf(8),
                BIRTH_DATE =  data.GetIndexOf(9).String_dd_MM_yyyy_ToDate('/', true),
                SEX =  data.GetIndexOf(10),
                EDUCATION_CODE =  data.GetIndexOf(11),
                COMP_CODE =  data.GetIndexOf(12),
                APPROVE_DATE =  data.GetIndexOf(13).String_dd_MM_yyyy_ToDate('/', true),
                EXAM_RESULT = data.GetIndexOf(14),
                SCORE_1 = data.GetIndexOf(15),
                SCORE_2 = data.GetIndexOf(16),
                SCORE_3 = data.GetIndexOf(17),
                SCORE_4 = data.GetIndexOf(18),
                SCORE_5 = data.GetIndexOf(19),
                SCORE_6 = data.GetIndexOf(20),
                SCORE_7 = data.GetIndexOf(21),
                SCORE_8 = data.GetIndexOf(22),
                SCORE_9 = data.GetIndexOf(23),
                SCORE_10 = data.GetIndexOf(24),
                SCORE_11 = data.GetIndexOf(25),
                SCORE_12 = data.GetIndexOf(26),
                SCORE_13 = data.GetIndexOf(27),
                SCORE_14 = data.GetIndexOf(28),
                SCORE_15 = data.GetIndexOf(29),
                SCORE_16 = data.GetIndexOf(30),
            };

            return detail;
        }


        private static String GetDataField(String[] fields, Int32 index)
        {
            return (fields.Length > index) ? fields[index] : "";
        }

        private static String getSubstring(String text, Int32 index, Int32 length)
        {
            return (text.Length > (index + length - 1)) ? text.Substring(index, length) : "";
        }

    }
}