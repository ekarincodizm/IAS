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

Date: 2014-04-08 09:34:56
*/


-- ----------------------------
-- Table structure for AG_IAS_STATUS
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_STATUS";
CREATE TABLE "AGDOI"."AG_IAS_STATUS" (
"STATUS_CODE" VARCHAR2(1 BYTE) NOT NULL ,
"STATUS_NAME" VARCHAR2(20 BYTE) NULL ,
"USER_ID" VARCHAR2(10 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_STATUS
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_STATUS" VALUES ('1', 'รออนุมัติ(สมัคร)', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_STATUS" VALUES ('2', 'อนุมัติ(สมัคร)', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_STATUS" VALUES ('3', 'ไม่อนุมัติ(สมัคร)', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_STATUS" VALUES ('4', 'รออนุมัติ(แก้ไข)', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_STATUS" VALUES ('5', 'อนุมัติ(แก้ไข)', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_STATUS" VALUES ('6', 'ไม่อนุมัติ(แก้ไข)', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Indexes structure for table AG_IAS_STATUS
-- ----------------------------
CREATE UNIQUE INDEX "AGDOI"."AG_IAS_APPLY_STATUS_PK"
ON "AGDOI"."AG_IAS_STATUS" ("STATUS_CODE" ASC)
LOGGING;

-- ----------------------------
-- Checks structure for table AG_IAS_STATUS
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_STATUS" ADD CHECK ("STATUS_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_STATUS
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_STATUS" ADD PRIMARY KEY ("STATUS_CODE") DISABLE;
