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

Date: 2014-04-08 09:01:55
*/


-- ----------------------------
-- Table structure for AG_IAS_APPROVE_DOC_TYPE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_APPROVE_DOC_TYPE";
CREATE TABLE "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" (
"APPROVE_DOC_TYPE" VARCHAR2(2 BYTE) NOT NULL ,
"APPROVE_DOC_NAME" VARCHAR2(200 BYTE) NULL ,
"APPROVER" VARCHAR2(4 BYTE) NULL ,
"DESCRIPTION" VARCHAR2(200 BYTE) NULL ,
"CREATED_BY" VARCHAR2(20 BYTE) NULL ,
"CREATED_DATE" DATE NULL ,
"UPDATED_BY" VARCHAR2(20 BYTE) NULL ,
"UPDATED_DATE" DATE NULL ,
"ITEM_VALUE" VARCHAR2(2 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_APPROVE_DOC_TYPE
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('01', 'ตัวแทนประกันชีวิต', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('02', 'ตัวแทนประกันวินาศภัย', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('03', 'การจัดการประกันชีวิตโดยตรง', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('04', 'การจัดการประกันวินาศภัยโดยตรง', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('05', 'การประกันภัยอุบัติเหตุส่วนบุคคลและประกันสุขภาพ', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('06', 'พรบ.คุ้มครองผู้ประสบภัยจากรถ', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('07', 'สำหรับการประกันภัยรายย่อย (ชีวิต)', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" VALUES ('08', 'สำหรับการประกันภัยรายย่อย (วินาศภัย)', '111', 'กำหนดผู้ตรวจใบอนุญาต', null, null, null, null, 'Y');

-- ----------------------------
-- Indexes structure for table AG_IAS_APPROVE_DOC_TYPE
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_APPROVE_DOC_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" ADD CHECK ("APPROVE_DOC_TYPE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_APPROVE_DOC_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPROVE_DOC_TYPE" ADD PRIMARY KEY ("APPROVE_DOC_TYPE");
