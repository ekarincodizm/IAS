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

Date: 2014-04-08 09:01:35
*/


-- ----------------------------
-- Table structure for AG_IAS_APPROVE_CONFIG
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_APPROVE_CONFIG";
CREATE TABLE "AGDOI"."AG_IAS_APPROVE_CONFIG" (
"ID" VARCHAR2(4 BYTE) NOT NULL ,
"ITEM" VARCHAR2(100 BYTE) NULL ,
"ITEM_VALUE" VARCHAR2(2 BYTE) NULL ,
"DESCRIPTION" VARCHAR2(200 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"KEYWORD" VARCHAR2(50 BYTE) NULL ,
"ITEM_TYPE" VARCHAR2(2 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_APPROVE_CONFIG"."ITEM" IS 'รายการ';
COMMENT ON COLUMN "AGDOI"."AG_IAS_APPROVE_CONFIG"."ITEM_VALUE" IS 'ค่าข้อมูล Y=อนุมัติ, N=ไม่ต้องอนุมัติ, 0=ไม่ระบุ, 1=สถาบัน, 2=สำนักงาน คปภ.';
COMMENT ON COLUMN "AGDOI"."AG_IAS_APPROVE_CONFIG"."DESCRIPTION" IS 'คำอธิบาย';

-- ----------------------------
-- Records of AG_IAS_APPROVE_CONFIG
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('01', 'บุคคลทั่วไปสมัครสมาชิกต้องมีการอนุมัติ', 'N', 'ผู้ที่จะสมัครเป็นสมาชิกต้องได้รับการอนุมัติจาก คปภ.', 'AGDOI', TO_DATE('2014-03-28 21:03:56', 'YYYY-MM-DD HH24:MI:SS'), 'General', '01');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('02', 'คนของบริษัทสมัครสมาชิกต้องมีการอนุมัติ', 'Y', 'ผู้ที่จะสมัครเป็นสมาชิกต้องได้รับการอนุมัติจาก คปภ.', 'AGDOI', TO_DATE('2014-03-28 21:04:33', 'YYYY-MM-DD HH24:MI:SS'), 'Insurance', '01');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('03', 'คนของสมาคมสมัครสมาชิกต้องมีการอนุมัติ', 'Y', 'ผู้ที่จะสมัครเป็นสมาชิกต้องได้รับการอนุมัติจาก คปภ.', 'AGDOI', TO_DATE('2014-03-28 21:04:33', 'YYYY-MM-DD HH24:MI:SS'), 'Association', '01');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('04', 'ผู้ตรวจเอกสาร', 'N', 'ผู้ที่ทำการตรวจเอกสาร 1:สถาบัน 2:สนง.คปภ.', 'AGDOI', TO_DATE('2013-02-20 11:50:04', 'YYYY-MM-DD HH24:MI:SS'), 'DocumentApprover', null);
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('05', 'เปลี่ยนรหัสผ่านเมื่อเข้าใช้งานครั้งแรกสำหรับเจ้าหน้าที่กลุ่มสนามสอบ', 'N', 'กำหนดการเปลี่ยนรหัสผ่านเมื่อเข้าใช้งานระบบครั้งแรก', 'AGDOI', TO_DATE('2014-03-31 17:26:32', 'YYYY-MM-DD HH24:MI:SS'), null, '03');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('06', 'กรณีไม่อนุมัติให้ใช้ข้อมูลเดิมได้ภายในกี่เดือน', 'Y', 'กรณีไม่อนุมัติผู้ลงทะเบียนสามารถใช้ข้อมูลเดิมได้ภายในกี่เดือน', 'AGDOI', TO_DATE('1956-03-21 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'MonthEffective', null);
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('09', 'เปลี่ยนครั้งแรกที่ Login (สำหรับเจ้าหน้าที่กลุ่มสนามสอบ)', 'Y', 'เปลี่ยนรหัสผ่านครั้งแรกที่ Login (สำหรับเจ้าหน้าที่กลุ่มสนามสอบ)', 'AGDOI', TO_DATE('2014-03-31 17:26:32', 'YYYY-MM-DD HH24:MI:SS'), 'Change_Password_Begin', '02');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('10', 'เปลี่ยนทุก 3 เดือน', 'Y', 'เปลี่ยนรหัสผ่านทุก  3  เดือน', 'AGDOI', TO_DATE('2014-03-31 17:26:32', 'YYYY-MM-DD HH24:MI:SS'), 'Change_Password_3months', '02');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('11', 'ไม่บังคับ', 'N', 'ไม่บังคับเปลี่ยน Password', 'AGDOI', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Optional_Password', null);
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('12', 'เป็นผู้สมัครใหม่ และไม่มีการอ้างอิงข้อมูลเดิม', 'Y', 'สมัครใหม่ บุคคลใหม่', 'AGDOI', TO_DATE('2014-03-28 21:04:18', 'YYYY-MM-DD HH24:MI:SS'), 'General_New', '01');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('13', 'เป็นผู้สมัครใหม่ ไม่มีการอ้างอิงข้อมูลเดิม แต่มีความสอดคล้องของข้อมูล', 'N', 'สมัครใหม่  บุคคลเก่า ไม่อ้างอิงข้อมูล', 'AGDOI', TO_DATE('2014-03-28 21:04:33', 'YYYY-MM-DD HH24:MI:SS'), 'General_NotImport', '01');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('14', 'มีการอ้างอิงข้อมูลเดิม และไม่มีการแก้ไขข้อมูล', 'Y', 'อ้างอิงข้อมูล ไม่มีการแก้ไข', 'AGDOI', TO_DATE('2014-03-28 21:04:33', 'YYYY-MM-DD HH24:MI:SS'), 'General_Old_Import_EditN', '01');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_CONFIG" VALUES ('15', 'มีการอ้างอิงข้อมูลเดิม และมีการแก้ไขข้อมูล', 'N', 'อ้างอิงข้อมูล  มีการแก้ไขเกิดขึ้น', 'AGDOI', TO_DATE('2014-03-28 21:04:33', 'YYYY-MM-DD HH24:MI:SS'), 'General_Old_Import_EditY', '01');

-- ----------------------------
-- Checks structure for table AG_IAS_APPROVE_CONFIG
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPROVE_CONFIG" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_APPROVE_CONFIG
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPROVE_CONFIG" ADD PRIMARY KEY ("ID") DISABLE;
