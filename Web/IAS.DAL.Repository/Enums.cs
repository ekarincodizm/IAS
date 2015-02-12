using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public enum RegistrationType
    {
        General = 1,
        Insurance = 2,
        Association = 3,
        OIC = 4,
        OICFinace = 5,
        OICAgent = 6,
        TestCenter =7
    }

    public enum MemberType
    { 
        General = 1,
        Insurance = 2,
        Association = 3,
        OIC = 4,
        OICFinance = 5,
        OICAgent = 6,
        TestCenter = 7
    }

    public enum RegistrationStatus
    {
        WaitForApprove = 1,
        Approve = 2,
        NotApprove = 3,
    }

    public enum PersonDataStatus
    {
        WaitForApprove = 4,
        Approve = 5,
        NotApprove=6
    }

    public enum UserStatus
    {
        Active,
        InActive
    }

    public enum DataActionMode
    {
        Add = 1,
        Edit = 2,
        View = 3,
        TargetView = 4

    }

    public enum DocumentFileType
    {
        PDF,
        IMAGE_BMP_GIF_JPG_PNG_TIF_PDF
    }

    public enum DocumentFileCSV 
    {
        CSV
    }

    public enum DocumentFileTXT
    { 
        TXT
    }

    public enum OICType
    {
        Admin,
        OICAgent,
        OICFinance
    }

    /// <summary>
    /// A = Add New
    /// E = Edit
    /// W = Wait
    /// D = Delete
    /// </summary>
    public enum AttachFileStatus
    {
        A,
        E,
        W,
        D
    }

    #region Registration
    /// <summary>
    /// Added new By Nattapong
    /// </summary>
    public enum MemberTypeTH
    {
        บุคคลทั่วไป = 1,
        บริษัท = 2,
        สมาคม = 3
    }
    #endregion

    /// <summary>
    /// Added new By Nattapong
    /// </summary>
    #region Personal License
    public enum PersonLicenses
    {
        New = 1,
        ReNew = 2,
        ExpireReNew = 3,
        Other = 4

    }

    public enum MenuLicenses
    {
        Step1 = 1,
        Step2 = 2,
        Step3 = 3,
        Step4 = 4,
        Step5 = 5,
    }

    [Flags]
    public enum LicenseType
    {
        /*
        ตัวแทนประกันชีวิต
        ตัวแทนประกันวินาศภัย
        การจัดการประกันชีวิตโดยตรง
        การจัดการประกันวินาศภัยโดยตรง
        การประกันภัยอุบัติเหตุส่วนบุคคลและประกันสุขภาพ
        พรบ.คุ้มครองผู้ประสบภัยจากรถ
        สำหรับการประกันภัยรายย่อย (ชีวิต)
        สำหรับการประกันภัยรายย่อย (วินาศภัย)
         */

        Type01 = 01,
        Type02 = 02,
        Type03 = 03,
        Type04 = 04,
        Type05 = 05,
        Type06 = 06,
        Type07 = 07,
        Type08 = 08

    }

    public enum FeeLicense
    {
        NewLicense = 300, //ค่าออกใบอนุญาตบุคคลธรรมดา (ขอใหม่)
        Renewlicense_1Y = 200, //ค่าต่ออายุบุคคลธรรมดา 1 ปี
        Renewlicense_5Y = 800, //ค่าต่ออายุบุคคลธรรมดา 5 ปี
        ExpiredRenewLicense = 300, //ค่าออกใบอนุญาตบุคคลธรรมดา (ขาดต่อขอใหม่)

        OtherLicense_1 = 200, //ค่าออกใบอนุญาตนิติบุคคล(ใบแทน)
        OtherLicense_2 = 300, //ค่าออกใบอนุญาต (ใบอนุญาตใบที่ 2)
        OtherLicense_3 = 300, //ค่าออกใบอนุญาต (ย้ายบริษัท)
    }

    public enum DocFunction
    {
        REGISTER_FUNCTION = 40, //ตั้งค่าเอกสารแนบสมัครสมาชิก
        LICENSE_FUNCTION = 41, //ตั้งค่าเอกสารแนบขอใบอนุญาต
        APPLICANT_FUNCTION=64 //ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ
    }

    public enum PettionCode
    {
        NewLicense = 11,//ขอรับใบอนุญาตใหม่
        RenewLicense1Y = 13,//ขอต่ออายุใบอนุญาต 1 ปี
        RenewLicense5Y = 14, //ขอต่ออายุใบอนุญาต 5 ปี
        ExpireRenewLicense = 15,//ขาดต่อขอใหม่
        OtherLicense_1 = 16,//ใบแทนใบอนุญาต
        MoveLicense = 17,   //ใบอนุญาต (ย้ายบริษัท)
        SecondLicense= 18 //ใบอนุญาต (ใบที่ 2)

    }

    public enum LicenseResult
    {

        //P = "ผ่าน",
        //F = "ไม่ผ่าน",
        //M = "ขาดสอบ",
        //B = "Black List",
        //N = "ไม่มีผลสอบ",

    }

    public enum ApprocLicense
    {
        Y,
        N,
        W,

    }
    #endregion

    /// <summary>
    /// W = wait สถานะเริ่มสร้างกลุ่มย่อยใบสั่งจ่าย
    /// X = ยกเลิกไม่จ่ายเงิน
    /// A = approve อนุมัติ สร้างใบเสร็จ
    /// C = complete ใบเสร็จถูกสร้างเรียบร้อยแล้ว
    /// </summary>
    public enum SubPayment_D_T_Status
    { 
        W =0,
        X =1,
        A =2,
        C =3
    }

    public enum LoginStatus
    {
        OffLine = 0,
        OnLine = 1
        
    }


    /// <summary>
    /// คุณสมบัติการขอใบอนุญาต
    /// 1 = คุุณสมบัติทั่วไป
    /// 2 = ผลสอบ
    /// 3 = คุณวุฒิการศึกษา
    /// 4 = ผลอบรม
    /// 5 = อื่น ๆ
    /// </summary>
    public enum LicenseProperty
    {
        General = 1,
        ExamResult= 2,
        Education = 3,
        TrainResult = 4,
        Other = 5
    
    }

    /// <summary>
    /// License Mode for get PersonalSkill
    /// </summary>
    public enum LicensePropMode 
    {
        General = 1,
        Group = 2
    }

    public enum ApproveAddressMode
    {
        Personal_local = 11,
        Personal_regis = 12,
        Registration_local = 21,
        Registration_regis = 22,
    }


    public enum DocumentLicenseType
    { 
        Marriage_license = 19
    }

    /// <summary>
    /// License Mode for get ReplateType
    /// 1 = ชำรุดสูญหาย
    /// 2 = เปลี่ยนชื่อ-นามสุกล
    /// </summary>
    public enum ReplateType
    {
        Damageorloss = 1,
        ChangeNameandSur = 2,
    }


    public enum ConfigAgenType
    { 
        AgentLife = 11,
        AgentCasualty = 12,
    
    }

    /// <summary>
    /// ManageExamRoom_MODE
    /// 0 = DEFULT เรียกดูข้อมูลเดิมจากฐานข้อมูล
    /// 1 = VIEW ONLY ดูข้อมูลเกี่ยวกับรอบสอบนั้นๆ (ในกรณีที่ text แสดงคำว่า "เพิ่มเติม" เท่านั้น)
    /// 2 = EDIT แก้ไขรอบสอบนั้น กรณีเพิ่มห้อง
    /// 3 = ADD ทำการเพิ่มรอบใหม่
    /// 4 = DEL ลบข้อมูลรอบสอบนี้
    /// 5 = MOVE เลื่อนรอบ
    /// </summary>
    public enum ManageExamRoom_MODE
    {
        DEFULT =0,
        VIEW  =1,
        EDIT =2,
        ADD =3,
        DEL =4,
        MOVE=5,

    }
}
