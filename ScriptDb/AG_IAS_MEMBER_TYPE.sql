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

Date: 2014-04-08 09:25:21
*/


-- ----------------------------
-- Table structure for AG_IAS_MEMBER_TYPE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_MEMBER_TYPE";
CREATE TABLE "AGDOI"."AG_IAS_MEMBER_TYPE" (
"MEMBER_CODE" VARCHAR2(1 BYTE) NOT NULL ,
"MEMBER_NAME" VARCHAR2(20 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_MEMBER_TYPE
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('1', 'บุคคลทั่วไป', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('2', 'บริษัทประกัน', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('3', 'สมาคม', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('4', 'เจ้าหน้าที่คปภ.Admin', 'AGDOI', TO_DATE('2013-02-13 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', 'AGDOI', TO_DATE('2013-06-24 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('5', 'คปภ.การเงิน', 'AGDOI', TO_DATE('2013-06-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_MEMBER_TYPE" VALUES ('6', 'คปภ.ตัวแทน', 'AGDOI', TO_DATE('2013-06-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Checks structure for table AG_IAS_MEMBER_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_MEMBER_TYPE" ADD CHECK ("MEMBER_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_MEMBER_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_MEMBER_TYPE" ADD PRIMARY KEY ("MEMBER_CODE") DISABLE;
