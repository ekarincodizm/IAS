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

Date: 2014-04-08 09:35:15
*/


-- ----------------------------
-- Table structure for AG_IAS_SUBJECT_GROUP
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_SUBJECT_GROUP";
CREATE TABLE "AGDOI"."AG_IAS_SUBJECT_GROUP" (
"ID" NUMBER NOT NULL ,
"GROUP_NAME" VARCHAR2(200 BYTE) NULL ,
"EXAM_PASS" NUMBER NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(20 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE
;
COMMENT ON COLUMN "AGDOI"."AG_IAS_SUBJECT_GROUP"."ID" IS 'รหัสลำดับ';
COMMENT ON COLUMN "AGDOI"."AG_IAS_SUBJECT_GROUP"."GROUP_NAME" IS 'ชื่อกลุ่ม';
COMMENT ON COLUMN "AGDOI"."AG_IAS_SUBJECT_GROUP"."EXAM_PASS" IS 'คะแนนที่ผ่าน %';
COMMENT ON COLUMN "AGDOI"."AG_IAS_SUBJECT_GROUP"."STATUS" IS 'สถานะ A = ใช้งาน , D = ลบ';

-- ----------------------------
-- Records of AG_IAS_SUBJECT_GROUP
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_SUBJECT_GROUP" VALUES ('1', 'จรรยาบรรณ', '60', 'A', null, null);
INSERT INTO "AGDOI"."AG_IAS_SUBJECT_GROUP" VALUES ('2', 'ความรู้ทั่วไป', '50', 'A', null, null);
INSERT INTO "AGDOI"."AG_IAS_SUBJECT_GROUP" VALUES ('3', 'ความรู้ทั่วไป2', '40', 'A', null, null);

-- ----------------------------
-- Indexes structure for table AG_IAS_SUBJECT_GROUP
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_SUBJECT_GROUP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_SUBJECT_GROUP" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_SUBJECT_GROUP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_SUBJECT_GROUP" ADD PRIMARY KEY ("ID");
