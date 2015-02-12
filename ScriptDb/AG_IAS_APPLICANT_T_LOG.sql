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

Date: 2014-04-08 09:01:12
*/


-- ----------------------------
-- Table structure for AG_IAS_APPLICANT_T_LOG
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG";
CREATE TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG" (
"APPLICANT_CODE_LOG" NUMBER(6) NOT NULL ,
"APPLICANT_CODE" NUMBER(6) NOT NULL ,
"TESTING_NO" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_PLACE_CODE" VARCHAR2(6 BYTE) NOT NULL ,
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
"INUSR_COMP_CODE" VARCHAR2(4 BYTE) NULL ,
"ABSENT_EXAM" VARCHAR2(1 BYTE) NULL ,
"RESULT" VARCHAR2(1 BYTE) NULL ,
"EXPIRE_DATE" DATE NULL ,
"LICENSE" VARCHAR2(1 BYTE) NULL ,
"CANCEL_RESON" VARCHAR2(300 BYTE) NULL ,
"RECODE_STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"EXAM_STATUS" VARCHAR2(1 BYTE) NULL ,
"UPLOAD_GROUP_NO" VARCHAR2(15 BYTE) NULL ,
"HEAD_REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"GROUP_REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"UPLOAD_BY_SESSION" VARCHAR2(15 BYTE) NULL ,
"ID_ATTACH_FILE" VARCHAR2(15 BYTE) NULL ,
"CREATE_BY" VARCHAR2(30 BYTE) NULL ,
"CREATE_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_APPLICANT_T_LOG
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('1', '1', '550591', '10666', '111', TO_DATE('2012-02-11 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3101201305814', '2', 'วรรณี', 'สุธีรพรชัย', TO_DATE('1973-01-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'F', '05', null, null, null, '10', null, null, null, '039618/2555', '3139', 'M', null, null, null, null, null, 'AGLOAD', TO_DATE('2012-03-16 12:12:12', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-21 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('2', '1', '550465', '10666', '666', TO_DATE('2012-02-04 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1739900005726', '1', 'ราเมช', 'แสงเสถียร', TO_DATE('1984-02-11 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'M', '05', null, null, null, '10', null, null, null, '031755/2555', '3139', null, null, null, null, null, null, 'AP3139', TO_DATE('2012-02-01 10:03:46', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-21 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('3', '1', '550359', '10666', '666', TO_DATE('2012-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3101100455861', '1', 'ไชยยัน', 'ตั่นเล่ง', TO_DATE('1953-06-21 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'M', '05', null, null, null, '10', null, null, null, '021054/2555', '3139', 'N', 'P', TO_DATE('2013-01-28 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGA580', TO_DATE('2012-04-27 12:33:04', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-21 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('4', '1', '550908', '10666', '111', TO_DATE('2012-03-03 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1820700035576', '2', 'อนงครัตน์', 'นาคบรรพ์วารีกุล', TO_DATE('1987-12-16 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'F', '05', null, null, null, '10', null, null, null, '074616/2555', '3139', 'N', 'F', TO_DATE('2013-03-02 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, null, null, 'AGA112', TO_DATE('2012-03-16 12:34:32', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-24 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('5', '1', '551067', '10666', '111', TO_DATE('2012-03-11 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3100800414342', '1', 'สิทธิพันธ์', 'วิจิตรลีลา', TO_DATE('1970-05-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'M', '05', null, null, null, '10', null, null, null, '088076/2555', '3139', 'N', 'P', TO_DATE('2013-03-10 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGA579', TO_DATE('2012-04-03 14:55:21', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-24 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('6', '1', '551696', '10666', '111', TO_DATE('2012-04-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3102201173731', '1', 'โชคชัย', 'จันทร์ผาสุข', TO_DATE('1956-07-28 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'M', '05', null, null, null, '10', null, null, null, '151514/2555', '3139', 'N', 'P', TO_DATE('2013-04-28 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGA111', TO_DATE('2012-05-31 16:53:05', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-24 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('7', '22', '511658', '10666', '111', TO_DATE('2008-05-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3250800050234', '2', 'ศิริวรรณ', 'ทองใบ', TO_DATE('1978-08-19 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'F', '05', null, null, null, '10', null, null, null, '192162/2551', '3139', 'N', 'P', TO_DATE('2009-05-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGA111', TO_DATE('2008-07-03 14:12:22', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('8', '22', '511658', '10666', '111', TO_DATE('2008-05-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1919191919191', '3', 'ลลิตา', 'ปัญโญภาส', TO_DATE('1978-08-19 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'F', '05', null, null, null, '10', null, null, null, '192162/2551', '3139', 'N', 'P', TO_DATE('2009-05-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGDOI', TO_DATE('2014-03-31 14:00:54', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('9', '26', '511658', '10666', '111', TO_DATE('2008-05-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3200100893808', '2', 'เทียมแข', 'พงษ์กระสินทร์', TO_DATE('1982-06-19 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'F', '05', null, null, null, '10', null, null, null, '192166/2551', '3139', 'N', 'P', TO_DATE('2009-05-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGA111', TO_DATE('2008-08-21 10:56:15', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-04-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_APPLICANT_T_LOG" VALUES ('10', '104', '511658', '10666', '111', TO_DATE('2008-05-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3169900256704', '1', 'ชัยวัธน์', 'ธันยปาลิต', TO_DATE('1963-04-28 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'M', '05', null, null, null, '10', null, null, null, '192244/2551', '3139', 'N', 'P', TO_DATE('2009-05-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null, 'AGA111', TO_DATE('2008-07-03 14:06:26', 'YYYY-MM-DD HH24:MI:SS'), 'E', null, null, null, null, null, '130923093821787', TO_DATE('2014-04-04 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Indexes structure for table AG_IAS_APPLICANT_T_LOG
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_APPLICANT_T_LOG
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG" ADD CHECK ("APPLICANT_CODE_LOG" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG" ADD CHECK ("APPLICANT_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG" ADD CHECK ("TESTING_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG" ADD CHECK ("EXAM_PLACE_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_APPLICANT_T_LOG
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_APPLICANT_T_LOG" ADD PRIMARY KEY ("APPLICANT_CODE_LOG");
