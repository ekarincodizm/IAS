/*
Navicat Oracle Data Transfer
Oracle Client Version : 11.2.0.3.0

Source Server         : AGDOI
Source Server Version : 90200
Source Host           : :1521
Source Schema         : AGDOI

Target Server Type    : ORACLE
Target Server Version : 90200
File Encoding         : 65001

Date: 2014-04-08 09:20:30
*/


-- ----------------------------
-- Table structure for AG_IAS_FUNCTION_R
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_FUNCTION_R";
CREATE TABLE "AGDOI"."AG_IAS_FUNCTION_R" (
"FUNCTION_ID" VARCHAR2(10 BYTE) NOT NULL ,
"FUNCTION_NAME" VARCHAR2(50 BYTE) NULL ,
"CREATED_BY" VARCHAR2(20 BYTE) NULL ,
"CREATED_DATE" DATE NULL ,
"UPDATED_BY" VARCHAR2(20 BYTE) NULL ,
"UPDATED_DATE" DATE NULL ,
"FUNCTION_URI" VARCHAR2(200 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_FUNCTION_R
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('1', 'ข้อมูลผู้ใช้ระบบ/แก้ไขข้อมูลผู้ใช้ระบบ', null, null, null, null, '/Person/PersonGeneral.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('19', 'สร้างใบสั่งจ่ายย่อย', null, null, null, null, '/Payment/Invoice.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('2', 'สมัครสอบ', null, null, null, null, '/Applicant/SingleApplicant.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('3', 'ข้อมูลผู้สมัครสอบ', null, null, null, null, '/Applicant/ApplicantDetail.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('4', 'ขอรับใบอนุญาต', null, null, null, null, '/License/LicenseSingle.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('5', 'ข้อมูลผู้ขอรับใบอนุญาต', null, null, null, null, '/License/LicenseDetail.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('6', 'พิมพ์ใบสั่งจ่าย/ข้อมูลใบสั่งจ่าย', null, null, null, null, '/Payment/Invoice5.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('7', 'ข้อมูลการชำระเงิน/พิมพ์ใบเสร็จ', null, null, null, null, '/Payment/PaymentDetail.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('8', 'ข้อมูลผู้ใช้ระบบ/แก้ไขข้อมูลผู้ใช้ระบบ', null, null, null, null, '/Person/PersonCompany.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('9', 'สร้างใบสั่งจ่ายย่อย', null, null, null, null, '/Payment/InvoiceSubGroup.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('10', 'ข้อมูลผู้ใช้ระบบ/แก้ไขข้อมูลผู้ใช้ระบบ', null, null, null, null, '/Person/PersonAssociate.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('11', 'สร้างใบสั่งจ่ายกลุ่ม', null, null, null, null, '/Payment/InvoiceSubGroup.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('12', 'สร้างกำหนดการสอบ', null, null, null, null, '/Exam/ExamSchedule.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('13', 'นำเข้าข้อมูลผู้สมัครสอบ', null, null, null, null, '/Applicant/GroupApplicant.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('14', 'นำเข้าผลการสอบ', null, null, null, null, '/Exam/ExamResult.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('15', 'นำเข้าข้อมูลผู้ขอรับใบอนุญาต', null, null, null, null, '/License/LicenseGroup.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('16', 'ตรวจสอบข้อมูลผู้ขอรับใบอนุญาต', null, null, null, null, '/Reporting/Verifydoc.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('17', 'ข้อมูลผู้ใช้ระบบ/แก้ไขข้อมูลผู้ใช้ระบบ', null, null, null, null, '/Register/Reg_OIC.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('18', 'อนุมัติการสมัคร', null, null, null, null, '/Register/regSearchOfficerOIC.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('20', 'สร้างใบสั่งจ่ายกลุ่ม', null, null, null, null, '/Payment/Invoice.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('21', 'นำเข้าข้อมูลการชำระเงิน', null, null, null, null, '/Payment/PayRenewFee.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('22', 'ยกเลิกผูสมัครสอบ', null, null, null, null, '/Applicant/ApplicantNoPay.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('23', 'รายงานสถิติขอเปลี่ยนรหัสผ่าน', null, null, null, null, '/Reporting/ResetReport.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('24', 'ประวัติการดาวน์โหลดใบเสร็จรับเงิน', null, null, null, null, '/Reporting/RcvHistory.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('25', 'รายงานจำนวนครั้งในการขอใบเสร็จ', null, null, null, null, '/Reporting/RcpReport.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('26', 'ตั้งค่าระบบ', null, null, null, null, '/Setting/Setting.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('27', 'นำเข้าข้อมูลการชำระเงิน', null, null, null, null, '/Payment/PayRenewFee.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('28', 'รายละเอียดข้อมูลผู้สมัครสอบ', null, null, null, null, '/Applicant/GroupApplicantDetail.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('29', 'อนุมัติแก้ไขข้อมูล', null, null, null, null, '/Person/Edit_Reg_Person.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('30', 'สร้างไฟล์ผู้ขอต่ออายุใบอนุญาต', null, null, null, null, '/License/RenewLicenseNo.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('31', 'เปลี่ยนรหัสผ่าน', null, null, null, null, '/ChangePassword/ChangePass.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('32', 'ลงทะเบียนเจ้าหน้าที่กลุ่มสนามสอบ
', null, null, null, null, 'Register/Reg_Place_Group.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('33', 'ข้อมูลการชำระเงินของกลุ่มสนามสอบ', null, null, null, null, 'Payment/PaymentDetail3.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('34', 'อนุมัติเดี่ยว', null, null, null, null, '/Register/regApproveOfficerOic.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('35', 'สมาคม', null, null, null, null, '/Register/Reg_Assoc.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('36', 'นำเข้าข้อมูลผู้สมัครสอบ/สมาคม', null, null, null, null, '/Applicant/ImportApplicantAssoc.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('37', 'แก้ไขข้อมูลที่ยังไม่ได้รับการอนุมัติ', null, null, null, null, '/Register/Reg_NotApprove.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('38', 'ผลการตรวจสอบขอใบอนุญาต', null, null, null, null, '/Reporting/RequestVerifydoc.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('39', 'ตั้งค่าเอกสารแนบ', null, null, null, null, '/Setting/Setting.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('40', 'ตั้งค่าเอกสารแนบสมัครสมาชิก', null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('41', 'ตั้งค่าเอกสารแนบขอใบอนุญาต', null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('42', 'ลงทะเบียนผู้ใช้แบบบุคคล', null, null, null, null, '/Register/RegisGeneral.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('43', 'ลงทะเบียนผู้ใช้ของบริษัท', null, null, null, null, '/Register/RegisCompany.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('44', 'ลงทะเบียนผู้ใช้ของสมาคม', null, null, null, null, '/Register/RegisAssociate.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('45', 'สร้างใบเสร็จ', null, null, null, null, '/Payment/GenPayment.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('46', 'ขอรับใบอนุญาตใหม่', null, null, null, null, '/License/NewLicense.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('47', 'ขอต่ออายุใบอนุญาต', null, null, null, null, '/License/RenewLicense.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('48', 'ขาดขอต่อใหม่', null, null, null, null, '/License/ExpiredRenewLicense.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('49', 'อื่นๆ', null, null, null, null, '/License/OtherLicense.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('50', 'ตรวจสอบผลสอบและอบรม', null, null, null, null, '/License/LicenseValidate.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('51', 'เอกสารแนบ', null, null, null, null, '/License/LicenseAttatchFiles.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('52', 'ยอมรับเงื่อนไข', null, null, null, null, '/License/LicenseAgreement.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('53', 'ดำเนินการต่อ', null, null, null, null, '/License/LicenseContinue.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('54', 'สร้างใบสั่งจ่าย', null, null, null, null, '/License/LicensePayment.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('55', 'ตรวจสอบการอนุมัติเอกสาร', null, null, null, null, '/License/LicenseCheck.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('56', 'ทดสอบสมัครสอบ', null, null, null, null, '/Applicant/AppTest.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('57', 'ผลการตรวจสอบเอกสาร', null, null, null, null, '/Reporting/ResultVerify.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('58', 'ใบแทนใบอนุญาต (ชำรุดสูญหาย)', null, null, null, null, '/License/LicenseReplace.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('60', 'ตั้งค่าใบสั่งจ่าย', null, null, null, null, '/Setting/SettingBillPayment.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('59', 'ให้ความเห็นชอบในการออกใบอนุญาต', null, null, null, null, '/License/LicenseApprove.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('62', 'ตั้งค่าสมาคม', null, null, null, null, '/Setting/ManageAssociation.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('63', 'แก้ไขข้อมูลผู้สมัครสอบ', null, null, null, null, '/Applicant/RequestEditApplicant.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('64', 'ตั้งค่ารอบสอบ', null, null, null, null, '/Applicant/SetApplicantRoom.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('65', 'ตั้งค่าเวลา', null, null, null, null, '/Setting/SetExamTime.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('66', 'ตั้งค่าสนามสอบ', null, null, null, null, '/Setting/SetExamPlace.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('67', 'ตั้งค่าห้องสอบ', null, null, null, null, '/Setting/ManageExamRoom.aspx');
INSERT INTO "AGDOI"."AG_IAS_FUNCTION_R" VALUES ('68', 'อนุมัติเดี่ยวกรณีนำเข้าข้อมูลเก่า', null, null, null, null, '/Register/RegisApproveCompare.aspx');

-- ----------------------------
-- Indexes structure for table AG_IAS_FUNCTION_R
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_FUNCTION_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_FUNCTION_R" ADD CHECK ("FUNCTION_ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_FUNCTION_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_FUNCTION_R" ADD PRIMARY KEY ("FUNCTION_ID");
