using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ApplicantInfo
    {
        /// <summary>
        /// คำนำ
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ชื่อ
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// นามสกุล
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// รหัสบัตรประชาชน
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// วันที่สอบ 
        /// </summary>
        public DateTime TestingDate { get; set; }

        /// <summary>
        /// เวลาสอบ
        /// </summary>
        public string TestingTime { get; set; }

        /// <summary>
        /// วันที่สมัครสอบ
        /// </summary>
        public DateTime ApplyDate { get; set; }

        /// <summary>
        /// รหัสผู้สมัครสอบ
        /// </summary>
        public string ApplicantCode { get; set; }

        /// <summary>
        /// รหัสสอบ
        /// </summary>
        public string TestingNo { get; set; }

        /// <summary>
        /// สนามสอบ
        /// </summary>
        public string ExamPlace { get; set; }

        /// <summary>
        /// รหัส สนง. ประกันภัยที่สมัครสอบ
        /// </summary>
        public string AcceptOfficeName { get; set; }

        /// <summary>
        /// บริษัทประกันภัย
        /// </summary>
        public string InsuranceCompanyName { get; set; }

        /// <summary>
        /// วันที่ผลสอบหมดอายุ
        /// </summary>
        public DateTime ExpireDate { get; set; }

        /// <summary>
        /// ขาดสอบ 
        /// </summary>
        public bool Absent { get; set; }

        /// <summary>
        /// สถานะบังคับสอบ
        /// </summary>
        public bool ExamForce { get; set; }

        /// <summary>
        /// ใบอนุญาต ออกแล้ว หรือยังไม่ออก
        /// </summary>
        public bool LicenseApprove { get; set; }

        /// <summary>
        /// ผลการสอบ
        /// </summary>
        public string ExamResult { get; set; }

        /// <summary>
        /// เลขใบสั่งจ่าย
        /// </summary>
        public string PaymentNo { get; set; }


        /// <summary>
        /// หน่วยงานจัดสอบ
        /// </summary>
        public string PlaceGroupName { get; set; }

        public string ExamOwner { get; set; }
        public string Province { get; set; }
        public string BillNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public string LicenseTypeName { get; set; }
        public string Special { get; set; }
        public string AssociationCode { get; set; }
        public string AssociationName { get; set; }

        /// <summary>
        /// รายการคะแนนแต่ละวิชา
        /// </summary>
        public List<ExamScoreResult> Subjects { get; set; }
    }

    [Serializable]
    public class ExamScoreResult
    {
        public string SubjectCode { get; set; }
        public int Score { get; set; }
        public string LicenseType { get; set; }
    }

}
