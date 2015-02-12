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

Date: 2014-04-08 08:57:22
*/


-- ----------------------------
-- Table structure for AG_IAS_APPLICANT_DETAIL_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_APPLICANT_DETAIL_T";
CREATE TABLE "AGDOI"."AG_IAS_APPLICANT_DETAIL_T" (
"ID" VARCHAR2(15 BYTE) NOT NULL ,
"UPLOAD_GROUP_NO" VARCHAR2(15 BYTE) NULL ,
"APPLICANT_CODE" NUMBER(5) NULL ,
"ID_CARD_NO" VARCHAR2(13 BYTE) NULL ,
"PRE_NAME_CODE" NUMBER(3) NULL ,
"NAMES" VARCHAR2(30 BYTE) NULL ,
"LASTNAME" VARCHAR2(30 BYTE) NULL ,
"BIRTH_DATE" DATE NULL ,
"SEX" VARCHAR2(1 BYTE) NULL ,
"EDUCATION_CODE" VARCHAR2(20 BYTE) NULL ,
"INSUR_COMP_CODE" VARCHAR2(20 BYTE) NULL ,
"EXAM_PLACE_CODE" VARCHAR2(20 BYTE) NULL ,
"APPLY_DATE" DATE NULL ,
"USER_ID" VARCHAR2(20 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_APPLICANT_DETAIL_T
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_APPLICANT_DETAIL_T
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_APPLICANT_DETAIL_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_DETAIL_T" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_APPLICANT_DETAIL_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_DETAIL_T" ADD PRIMARY KEY ("ID");
