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

Date: 2014-04-08 09:00:50
*/


-- ----------------------------
-- Table structure for AG_IAS_APPLICANT_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_APPLICANT_T";
CREATE TABLE "AGDOI"."AG_IAS_APPLICANT_T" (
"APPLICANT_CODE" NUMBER(6) NULL ,
"TESTING_NO" VARCHAR2(6 BYTE) NULL ,
"EXAM_PLACE_CODE" VARCHAR2(6 BYTE) NULL ,
"ACCEPT_OFF_CODE" VARCHAR2(3 BYTE) NULL ,
"APPLY_DATE" DATE NULL ,
"ID_CARD_NO" VARCHAR2(13 BYTE) NULL ,
"PRE_NAME_CODE" VARCHAR2(3 BYTE) NULL ,
"NAMES" VARCHAR2(30 BYTE) NULL ,
"LASTNAME" VARCHAR2(35 BYTE) NULL ,
"BIRTH_DATE" DATE NULL ,
"SEX" VARCHAR2(1 BYTE) NULL ,
"EDUCATION_CODE" VARCHAR2(2 BYTE) NULL ,
"ADDRESS1" VARCHAR2(60 BYTE) NULL ,
"ADDRESS2" VARCHAR2(60 BYTE) NULL ,
"AREA_CODE" VARCHAR2(8 BYTE) NULL ,
"PROVINCE_CODE" VARCHAR2(3 BYTE) NULL ,
"ZIPCODE" VARCHAR2(5 BYTE) NULL ,
"TELEPHONE" VARCHAR2(15 BYTE) NULL ,
"AMOUNT_TRAN_NO" VARCHAR2(15 BYTE) NULL ,
"PAYMENT_NO" VARCHAR2(12 BYTE) NULL ,
"INSUR_COMP_CODE" VARCHAR2(4 BYTE) NULL ,
"ABSENT_EXAM" VARCHAR2(1 BYTE) NULL ,
"RESULT" VARCHAR2(1 BYTE) NULL ,
"EXPIRE_DATE" DATE NULL ,
"LICENSE" VARCHAR2(1 BYTE) NULL ,
"CANCEL_REASON" VARCHAR2(300 BYTE) NULL ,
"RECORD_STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"EXAM_STATUS" VARCHAR2(1 BYTE) NULL ,
"REQUEST_NO" VARCHAR2(20 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_APPLICANT_T
-- ----------------------------