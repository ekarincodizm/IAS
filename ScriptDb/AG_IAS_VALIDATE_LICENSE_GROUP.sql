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

Date: 2014-04-08 09:41:47
*/


-- ----------------------------
-- Table structure for AG_IAS_VALIDATE_LICENSE_GROUP
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP";
CREATE TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" (
"ID" NUMBER NOT NULL ,
"GROUP_NAME" VARCHAR2(100 BYTE) NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_VALIDATE_LICENSE_GROUP
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" VALUES ('1', 'คุณสมบัติทั่วไป', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" VALUES ('2', 'ผลสอบ', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" VALUES ('3', 'คุณวุฒิการศึกษา', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" VALUES ('4', 'ผลอบรม', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" VALUES ('5', 'อื่น ๆ', 'A');

-- ----------------------------
-- Indexes structure for table AG_IAS_VALIDATE_LICENSE_GROUP
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_VALIDATE_LICENSE_GROUP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_VALIDATE_LICENSE_GROUP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE_GROUP" ADD PRIMARY KEY ("ID");
