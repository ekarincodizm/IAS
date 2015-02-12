using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.IO;

namespace IAS.DataServices.Person
{
    [ServiceContract]
    public interface IPersonService
    {
        /// <summary>
        /// ดึงข้อมูล จาก AG_IAS_REGISTRATION_T  โดยค้นหาจาก
        /// - ID 
        /// - MEMBER_TYPE
        /// ***** ติดข้อส่งสัย ว่า ควรจะ ดึงจาก AG_IAS_PERSONAL_T  by Tik ******
        /// </summary>
        /// <returns>DTO.Person</returns>
        [OperationContract]
        DTO.ResponseService<DTO.Person> GetUserProfile(string Id, string memType);

        /// <summary>
        /// ค้นหาข้อมูล AG_IAS_PERSONAL_T
        /// ด้วย Id
        /// </summary>
        /// <returns>DTO.Person</returns>
        [OperationContract]
        DTO.ResponseService<DTO.Person> GetById(string Id);



        /// <summary>
        /// ค้นหาข้อมูล สำหรับออก Report
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        DTO.ResponseService<DataSet> GetDataTo8Report(string ID, string license_code);

        /// <summary>
        /// ค้นหาข้อมูล AG_IAS_PERSONAL_T ด้วย ID
        /// รับค่าเป็น ID  prosonal
        /// โดยมีเงือนไข้ว่าต้องอนุมัติแล้ว
        /// ถ้ายังไม่อนุมัติ  ให้ดึงข้อมูล จาก AG_IAS_TEMP_PERSONAL_T
        /// </summary>
        /// <returns>DTO.Person</returns>
        [OperationContract]
        DTO.ResponseService<DTO.Person> GetUserProfileById(string Id);

        /// <summary>
        /// ค้นหาข้อมูล AG_IAS_TEMP_PERSONAL_T ด้วย ID
        /// ถ้าไม่พบ  ให้ดึงข้อมูลจาก AG_IAS_PERSONAL_T ด้วย ID
        /// </summary>
        /// <returns>DTO.PersonTemp</returns>
        [OperationContract]
        DTO.ResponseService<DTO.PersonTemp> GetPersonTemp(string Id);

        /// <summary>
        /// บันทึกข้อมูล คำขอ แก้ไขข้อมูลส่วนบุคคล  AG_IAS_PERSONAL_T
        /// โดยการ นำข้อมูล ไปบันทึกลง AG_IAS_TEMP_PERSONAL_T 
        /// และข้อมูลไฟล์แนบ ที่ AG_IAS_TEMP_ATTACH_FILE
        /// กรณี ไม่พบข้อมูลที่ AG_IAS_PERSONAL_T  
        /// ให้ไป แก้ข้อมูล AG_IAS_REGISTRATION_T
        /// </summary>
        /// <returns>return true; กรณีทำรายการสำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> SetPersonTemp(DTO.PersonTemp tmpPerson, List<DTO.PersonAttatchFile> tmpFiles);

        /// <summary>
        /// ปรับปรุงข้อมูลส่วนตัว ผู้ใช่ระบบ  
        /// - DTO.PersonTemp
        /// - ทำการคัดลอกข้อมูลเดิมจาก AG_IAS_PERSONAL_T  
        /// - นำไปบันทึกเก็บไว้ที่ AG_IAS_HIST_PERSONAL_T
        /// - นำข้อมูลที่ต้องการแก้จาก AG_IAS_TEMP_PERSONAL_T มา update AG_IAS_PERSONAL
        /// - ทำการปรับปรุงข้อมูล File แนบ จาก AG_IAS_TEMP_ATTACH_FILE to AG_IAS_ATTACH_FILE และทำการย้ายไฟล์ไปเก็บให้ถูกต้อง
        /// </summary>
        /// <remarks>
        /// - AG_IAS_PERSONAL_T  
        /// - AG_IAS_HIST_PERSONAL_T
        /// - AG_IAS_TEMP_PERSONAL_T 
        /// - AG_IAS_USERS
        /// - AG_IAS_TEMP_ATTACH_FILE 
        /// - AG_IAS_ATTACH_FILE
        /// </remarks>
        [OperationContract]
        DTO.ResponseMessage<bool> EditPerson(DTO.PersonTemp tmpPerson);

        /// <summary>
        /// ค้นหาข้อมูลไฟล์แนบ จาก Personal_id  
        /// โดยมีเงื่อนไขตรวจสอบ  สถานะ
        /// - PersonDataStatus.WaitForApprove รออนุมัติแก้ไข ให้ดึงข้อมูล  Temp_Attach_File ด้วย
        /// </summary>
        /// <returns>List[DTO.PersonAttachFile]</returns>
        [OperationContract]
        DTO.ResponseService<List<DTO.PersonAttatchFile>> GetUserProfileAttatchFileByPersonId(string personId);

        /// <summary>
        /// ดึงข้อมูล  สถิติ การข้อเปลี่ยน รหัสผ่าน จาก AG_IAS_USERS และ AG_IAS_PERSONAL_T
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        DTO.ResponseService<DataSet>
                GetStatisticResetPassword(string idCard, string firstName, string lastName);

        /// <summary>
        /// ดึงข้อมูลเอกสารแนบ จาก AG_IAS_ATTACH_FILE
        /// - personId
        /// ค้นหาจาก  REGISTRATION_ID
        /// ได้ผลรับแล้วแปลงเป็น  DTO.PersonAttachFile
        /// </summary>
        /// <remarks>
        /// - AG_IAS_ATTACH_FILE
        /// - AG_IAS_DOCUMENT_TYPE
        /// </remarks>
        /// <returns>List[DTO.PersonAttachFile]</returns>
        [OperationContract]
        DTO.ResponseService<List<DTO.PersonAttatchFile>> GetAttatchFileByPersonId(string personId);

        /// <summary>
        /// บันทึกอนุมัติ แก้ไข ของมูลส่วนตัว 
        /// โดยค้นหาจาก  รายการ personal id 
        /// ดึงขอ้มูลจาก AG_IAS_TEMP_PERSONAL_T มาบันทึก
        /// ลง AG_IAS_PERSONAL_T
        /// และ กำหนด status เป็น อนมุัติแล้ว
        /// </summary>
        /// <returns>return true ; ถ้าทำรายการสำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> PersonApprove(List<string> listId);

        //Edit
        /// <summary>
        /// บันทึกอนุมัติ แก้ไข ของมูลส่วนตัว 
        /// โดยค้นหาจาก  รายการ personal id ที่มาเป็น list
        /// ดึงข้อมูล AG_IAS_PERSONAL_T มาเก็บที่ AG_IAS_HIST_PERSONAL_T ก่อน จากนั้น
        /// ดึงขอ้มูลจาก AG_IAS_TEMP_PERSONAL_T มาบันทึกลง AG_IAS_PERSONAL_T
        /// และ กำหนด status เป็น อนมุัติแล้ว พร้อม ข้อมูล appresult และระบุผู้อนุมัติ
        /// </summary>
        /// <returns>return true; ถ้าสำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> PersonEditApprove(List<string> listId, string appresult,string userid);

        /// <summary>
        /// บันทึกผล ไม่อนุมัติ ที่ AG_PERSONAL_T
        /// </summary>
        /// <returns>return true; ถ้าไม่สำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> PersonNotApprove(List<string> listId);

        /// <summary>
        /// ยืนยันตัวต้นเพื่อเข้าใช่ระบบ  
        /// - userName    ชื่อผู้ใช้
        /// - password    รหัสผ่าน
        /// - IsOIC  ตรวจสอบว่าเป็นเจ้าหน้าที่ คปภ.หรือไม่
        /// กรณีเป็นเจ้าหน้าที่ คปภ.ให้ทำการ Authen กับ AD
        /// กรณีผู้สมัครทั่วไป  ให้ตรวจสอบจากฐานข้อมูล  AG_IAS_USER
        /// กรณี ยังไม่อนุมัติ ดึงข้อมูล AG_IAS_REGISTRATION_T  มากำหนด  UserProfile 
        /// กรณีอนุมัติ
        /// </summary>
        /// <remarks>- AG_IAS_USERS</remarks>
        /// <returns>DTO.UserProfile</returns>
        [OperationContract]
        DTO.ResponseService<DTO.UserProfile> Authentication(string userName, string password, bool IsOIC,string Ip);

        /// <summary>
        /// บันทึกข้อมูลลง AG_IAS_PERSONAL_T 
        /// เฉพาะ MemberType =
        /// - OIC = 4
        /// - OICFinance = 5
        /// - OICAgent = 6
        /// </summary>
        /// <returns>สำเร็จ return true;</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> InsertOIC(string oicEmpNo, string oicUserName, string preNameCode,
                                                   string firstName, string lastName,
                                                   string sex, string oicTypeCode,byte[]sign);

        /// <summary>
        /// Authen AD  ด้วย UserName Password  ของเจ้าหน้าที่ คปภ.
        /// </summary>
        /// <returns>return true;   ถ้า usernam password AD ถูกต้อง</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> IsRightUserOIC(string oicUserName);

        //milk
        /// <summary>
        /// เพิ่มจำนวนครั้ง การเปลี่ยนรหัสผ่านลงฐานข้อมูล
        /// AG_IAS_USERS.RESET_TIMES
        /// - โดยการค้นหาจาก username
        /// </summary>
        /// <returns>return ture ; ถ้าสำเร็จ</returns>
        [OperationContract]
        Boolean ChangePasswordTime(string userName);
        //milk

        /// <summary>
        /// แก้ไข้ รหัสเข้าใช้ระบบ  ChangePassword
        /// - userId   รหัส User
        /// - oldPassword  รหัสผ่านเก่า
        /// - newPassword  รหัสผ่านใหม่
        /// ตรวจสอบ   ID user กับ รหัสผ่านเก่า ตรงกับในระบบ  
        /// ให้ทำการ update รหัสผ่านใหม่
        /// </summary>
        /// <returns>
        /// return true;  กรณีสำเร็จ
        /// return false; กรณีไม่สำเร็จ
        /// </returns>
        [OperationContract]
        DTO.ResponseMessage<bool> ChangePassword(string userId, string oldPassword, string newPassword);

        /// <summary>
        /// ดึงข้อมูล จาก 
        /// - AG_IAS_PERSONAL_T : ID, EMAIL
        /// - AG_IAS_USERS : USER_ID, USER_NAME
        /// - AG_IAS_REGISTRATION_T : ID, EMAIL
        /// โดย นำมา UNION กัน
        /// ID, USER_ID, USER_NAME, EMAIL  ของทั้ง personal กับ registration
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        DTO.ResponseService<DataSet> GetUserProfileByUsername(string userName, string email);

        /// <summary>
        /// ค้นหาข้อมูล AG_IAS_TEMP_PERSONAL_T  by Criteria ที่ส่งเข้ามาให้
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        DTO.ResponseService<DataSet> GetPersonTempEditByCriteria(string firstName, string lastName,
                                                                  DateTime? starDate, DateTime? toDate,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage, string para);

        /// <summary>
        /// ค้นหาข้อมูล AG_IAS_PERSONAL_T  ที่ตรงกับเงื่อนไขในการค้นหา
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        DTO.ResponseService<DataSet> GetPersonByCriteria(string firstName, string lastName,
                                                                       DateTime? starDate, DateTime? toDate,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage, string para);
        /// <summary>
        /// ค้นหาข้อมูลจาก AG_IAS_TEMP_ATTACH_FILE   จาก REGISTRATION_ID
        /// </summary>
        /// <returns>List[DTO.PersonAttachFile]</returns>
        [OperationContract]
        DTO.ResponseService<List<DTO.PersonAttatchFile>> GetTempAttatchFileByPersonId(string personId);


        /// <summary>
        /// ปรับปรุงข้อมูลส่วนตัวเจ้าหน้าที่ คปภ.
        /// </summary>
        /// <returns>return true; กรณีทำรายการสำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<bool> UpdateOIC(string userId, string oicUserName, string preNameCode,
                                                   string firstName, string lastName,
                                                   string sex, string memberType, byte[] imgSign);

        /// <summary>
        /// ค้นหาข้อมูล AG_IAS_PERSONAL_T  ที่ตรงกับเงื่อนไขในการค้นหา
        /// </summary>
        /// <returns>DataSet</returns>
        [OperationContract]
        DTO.ResponseService<DataSet> GetPersonByCriteriaAtPage(string firstName, string lastName,
                                                                       string IdCard, string memberTypeCode,
                                                                       string email, string compCode,
                                                                       string status, int pageNo, int recordPerPage);

        /// <summary>
        /// แจ้งลืมรหัสผ่าน
        /// รับค่า 
        /// - username
        /// - email
        /// ตรวจสอบว่า  มีข้อมูลใน AG_IAS_PERSONAL_T หรือไม่ 
        /// ถ้ามี ให้ทำการส่ง email แจ้ง รหัสผ่านใหม่ และ link ยืนยันการเปลี่ยนรหัสผ่าน
        /// </summary>
        /// <returns>return true ถ้าสำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<Boolean> ForgetPasswordRequest(String username, String email);

        /// <summary>
        /// ตั้งรหัสผ่านใหม่
        /// รับค่า
        /// - username
        /// - email
        /// - oldpassword
        /// - newpassword
        /// ตรวจสอบข้อมูล จาก AG_IAS_USERS ว่า
        /// username กับ password มีหรือไม่
        /// ถ้ามี ให้แก้ เป็น รหัสผ่านใหม่
        /// และ บันทึกจำนวนครั่งขอเปลี่ยนรหัสผ่าน
        /// ที่ AG_IAS_PERSONAL_T กรณีอนุมัติแล้ว
        /// ที่ AG_IAS_REGISTRATION_T กรณียังไม่อนุมัติ
        /// </summary>
        /// <returns>return true; กรณีทำรายการสำเร็จ</returns>
        [OperationContract]
        DTO.ResponseMessage<Boolean> RenewPassword(String username, String email, String oldpassword, String newpassword);

        /// <summary>
        /// ค้นหาข้อมูลจาก AG_IAS_REGISTRATION_T
        /// ค้นหาจาก  ID_CARD_NO
        /// </summary>
        /// <returns>AG_IAS_REGISTRATION_T</returns>
        [OperationContract]
        DTO.ResponseService<DTO.Registration> getPDetailByIDCard(string idCard);

        /// <summary>
        /// กำหนดค่า สถานะ User ว่ายัง LogOff แล้ว
        /// </summary>
        [OperationContract]
        DTO.ResponseMessage<bool> SetOffLineStatus(string userName);

        [OperationContract]
        DTO.ResponseMessage<bool> SetOffLineAllStatus(string userName);
        [OperationContract]
        DTO.ResponseService<DataSet> GetOnLineUser(string userName);

        [OperationContract]
        DTO.ResponseService<DTO.Person> GetPersonalDetail(string idCard);

        [OperationContract]
        DTO.ResponseService<DTO.SignatureImg> GetOicPersonSignImg(String id);

        [OperationContract]
        DTO.ResponseMessage<bool> CheckAuthorityEditExam(DTO.UserProfile userProfile, string testingNo, string testingDate);

        [OperationContract]
        List<string> GetEmailMoveExam(string testingNo);

        [OperationContract]
        DTO.ResponseService<DTO.OICADProperties> OICAuthenWithADService(string ADUserName, string ADPassword);


        [OperationContract]
        DTO.ResponseService<DataSet> GetDataRenewReport(string id, string license_code, string license_no);

    }
}
