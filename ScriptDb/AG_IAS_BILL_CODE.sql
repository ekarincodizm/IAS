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

Date: 2014-04-08 09:05:43
*/


-- ----------------------------
-- Table structure for AG_IAS_BILL_CODE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_BILL_CODE";
CREATE TABLE "AGDOI"."AG_IAS_BILL_CODE" (
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"BILL_CODE" VARCHAR2(2 BYTE) NOT NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_BILL_CODE
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('01', 'e1');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('11', 'e4');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('13', 'e5');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('14', 'e5');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('15', 'e4');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('16', 'e4');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('17', 'e4');
INSERT INTO "AGDOI"."AG_IAS_BILL_CODE" VALUES ('18', 'e4');

-- ----------------------------
-- Indexes structure for table AG_IAS_BILL_CODE
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_BILL_CODE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_BILL_CODE" ADD CHECK ("PETITION_TYPE_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_BILL_CODE" ADD CHECK ("BILL_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_BILL_CODE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_BILL_CODE" ADD PRIMARY KEY ("PETITION_TYPE_CODE", "BILL_CODE");
