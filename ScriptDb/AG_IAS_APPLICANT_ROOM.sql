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

Date: 2014-04-08 08:59:13
*/


-- ----------------------------
-- Table structure for AG_IAS_APPLICANT_ROOM
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_APPLICANT_ROOM";
CREATE TABLE "AGDOI"."AG_IAS_APPLICANT_ROOM" (
"APPLICANT_CODE" NUMBER(6) NOT NULL ,
"TESTING_NO" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_ROOM_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_PLACE_GROUP_CODE" VARCHAR2(6 BYTE) NULL ,
"EXAM_PLACE_CODE" VARCHAR2(6 BYTE) NULL ,
"USER_ID" VARCHAR2(20 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_APPLICANT_ROOM
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_APPLICANT_ROOM
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_APPLICANT_ROOM
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_ROOM" ADD CHECK ("APPLICANT_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_ROOM" ADD CHECK ("TESTING_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_ROOM" ADD CHECK ("EXAM_ROOM_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_APPLICANT_ROOM
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_ROOM" ADD PRIMARY KEY ("APPLICANT_CODE", "TESTING_NO", "EXAM_ROOM_CODE");
