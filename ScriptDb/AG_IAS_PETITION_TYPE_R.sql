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

Date: 2014-04-08 09:28:25
*/


-- ----------------------------
-- Table structure for AG_IAS_PETITION_TYPE_R
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PETITION_TYPE_R";
CREATE TABLE "AGDOI"."AG_IAS_PETITION_TYPE_R" (
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"PETITION_TYPE_NAME" VARCHAR2(60 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_PETITION_TYPE_R
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('14', 'ขอต่ออายุใบอนุญาต 5 ปี', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('01', 'ค่าสมัครสอบ', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('11', 'ขอรับใบอนุญาตใหม่', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('13', 'ขอต่ออายุใบอนุญาต 1 ปี', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('15', 'ขาดต่อขอใหม่', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('16', 'ใบแทนใบอนุญาต', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('17', 'ใบอนุญาต (ย้ายบริษัท)', null, null, 'A');
INSERT INTO "AGDOI"."AG_IAS_PETITION_TYPE_R" VALUES ('18', 'ใบอนุญาต (ใบที่ 2)', null, null, 'A');

-- ----------------------------
-- Indexes structure for table AG_IAS_PETITION_TYPE_R
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_PETITION_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PETITION_TYPE_R" ADD CHECK ("PETITION_TYPE_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_PETITION_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PETITION_TYPE_R" ADD PRIMARY KEY ("PETITION_TYPE_CODE");
