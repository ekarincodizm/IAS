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

Date: 2014-04-08 09:25:42
*/


-- ----------------------------
-- Table structure for AG_IAS_NATIONALITY
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_NATIONALITY";
CREATE TABLE "AGDOI"."AG_IAS_NATIONALITY" (
"NATIONALITY_CODE" VARCHAR2(3 BYTE) NOT NULL ,
"NATIONALITY_NAME" VARCHAR2(30 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_NATIONALITY
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_NATIONALITY" VALUES ('001', 'ไทย', 'AGDOI', TO_DATE('2012-02-18 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_NATIONALITY" VALUES ('002', 'จีน', 'AGDOI', TO_DATE('2012-02-18 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Checks structure for table AG_IAS_NATIONALITY
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_NATIONALITY" ADD CHECK ("NATIONALITY_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_NATIONALITY
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_NATIONALITY" ADD PRIMARY KEY ("NATIONALITY_CODE") DISABLE;
