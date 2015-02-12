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

Date: 2014-04-23 13:26:12
*/


-- ----------------------------
-- Table structure for AG_IAS_EXAM_APPLICANT
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_EXAM_APPLICANT";
CREATE TABLE "AGDOI"."AG_IAS_EXAM_APPLICANT" (
"EXAM_PLACE_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_ROOM_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_APPLICANT_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"APPICANT_CODE" VARCHAR2(6 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_EXAM_APPLICANT
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_EXAM_APPLICANT
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_EXAM_APPLICANT
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_APPLICANT" ADD CHECK ("EXAM_PLACE_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_EXAM_APPLICANT" ADD CHECK ("EXAM_ROOM_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_EXAM_APPLICANT" ADD CHECK ("EXAM_APPLICANT_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_EXAM_APPLICANT
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_APPLICANT" ADD PRIMARY KEY ("EXAM_PLACE_CODE", "EXAM_ROOM_CODE", "EXAM_APPLICANT_CODE");
