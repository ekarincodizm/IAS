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

Date: 2014-04-08 09:24:55
*/


-- ----------------------------
-- Table structure for AG_IAS_MEMBER_RUNNO
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_MEMBER_RUNNO";
CREATE TABLE "AGDOI"."AG_IAS_MEMBER_RUNNO" (
"COMP_CODE" VARCHAR2(4 BYTE) NULL ,
"LAST_RUNNO" VARCHAR2(2 BYTE) NULL ,
"LAST_UPDATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_MEMBER_RUNNO
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_MEMBER_RUNNO" VALUES ('0001', '15', TO_DATE('2013-02-20 17:23:40', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_RUNNO" VALUES ('1', '01', TO_DATE('2013-02-20 17:27:54', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Indexes structure for table AG_IAS_MEMBER_RUNNO
-- ----------------------------
CREATE UNIQUE INDEX "AGDOI"."AG_IAS_MEMBER_RUNNO_PK"
ON "AGDOI"."AG_IAS_MEMBER_RUNNO" ("COMP_CODE" ASC)
LOGGING;
