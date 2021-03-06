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

Date: 2014-04-09 14:39:53
*/


-- ----------------------------
-- Table structure for AG_APPLICANT_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_APPLICANT_T" (
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
"UPLOAD_GROUP_NO" VARCHAR2(15 BYTE) NULL ,
"HEAD_REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"GROUP_REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"UPLOAD_BY_SESSION" VARCHAR2(15 BYTE) NULL ,
"ID_ATTACH_FILE" VARCHAR2(15 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_APPLICANT_T" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."APPLICANT_CODE" IS '???????????????(???????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."TESTING_NO" IS '???????????? (??????????????????????)';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."EXAM_PLACE_CODE" IS '???????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."ACCEPT_OFF_CODE" IS '????????????????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."APPLY_DATE" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."ID_CARD_NO" IS '?????????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."PRE_NAME_CODE" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."NAMES" IS '????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."LASTNAME" IS '???????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."BIRTH_DATE" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."SEX" IS '??? ?= ??????, ? = ???????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."EDUCATION_CODE" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."ADDRESS1" IS '???????1 ??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."ADDRESS2" IS '???????2 ??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."AREA_CODE" IS '??????????? ??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."PROVINCE_CODE" IS '??????????? ??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."ZIPCODE" IS '???????????? ??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."TELEPHONE" IS '???????? ??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."AMOUNT_TRAN_NO" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."PAYMENT_NO" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."INSUR_COMP_CODE" IS '???????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."ABSENT_EXAM" IS '????????????? M = ??????, N = ????????? ';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."RESULT" IS '????????  P = ????, F = ???????, B = ???????????? Black List ';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."EXPIRE_DATE" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."LICENSE" IS '??????????? , Y = ???????, N = ?????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."CANCEL_REASON" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."RECORD_STATUS" IS '??????????????  X = cancel ??????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."USER_DATE" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_APPLICANT_T"."EXAM_STATUS" IS '?????????????? E = ?????????,
N = ????????????';

-- ----------------------------
-- Indexes structure for table AG_APPLICANT_T
-- ----------------------------
CREATE INDEX "AGDOI"."AGIN_APP_APPLY_DATE"
ON "AGDOI"."AG_APPLICANT_T" ("APPLY_DATE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_APP_CODE"
ON "AGDOI"."AG_APPLICANT_T" ("APPLICANT_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_APP_ID_CARD"
ON "AGDOI"."AG_APPLICANT_T" ("ID_CARD_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_APP_OFF_CODE"
ON "AGDOI"."AG_APPLICANT_T" ("ACCEPT_OFF_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_APP_PAYMENT"
ON "AGDOI"."AG_APPLICANT_T" ("PAYMENT_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_APP_PLACE"
ON "AGDOI"."AG_APPLICANT_T" ("TESTING_NO" ASC, "EXAM_PLACE_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APPLI_T_USER_DATE_INX"
ON "AGDOI"."AG_APPLICANT_T" ("USER_DATE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APP_INDXPV"
ON "AGDOI"."AG_APPLICANT_T" ("PROVINCE_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APP_TEST_NO_INX"
ON "AGDOI"."AG_APPLICANT_T" ("TESTING_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APP_T_LNAME_INX"
ON "AGDOI"."AG_APPLICANT_T" ("LASTNAME" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APP_T_NAMES_INX"
ON "AGDOI"."AG_APPLICANT_T" ("NAMES" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APP_T_REC_STA_INX"
ON "AGDOI"."AG_APPLICANT_T" ("RECORD_STATUS" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_APP_T_USER_ID_INX"
ON "AGDOI"."AG_APPLICANT_T" ("USER_ID" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_APPLICANT_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_APPLICANT_RESULT_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_APPLICANT_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
         IF :NEW.EXAM_STATUS IS NOT NULL and    :NEW.EXAM_STATUS <> 'E' THEN
 :NEW.RESULT                := 'P';
                     :NEW.ABSENT_EXAM := 'M';
         END IF;
END;
CREATE OR REPLACE TRIGGER "AGDOI"."AG_APPLICANT_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_APPLICANT_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID := GET_SYS_USER;
  	:NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Checks structure for table AG_APPLICANT_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD CHECK (ABSENT_EXAM in ('M','N'));
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD CHECK (EXAM_STATUS in ('E','N'));
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD CHECK (LICENSE  in ('Y', 'N'));
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD CHECK (RESULT in ('P', 'F', 'B'));
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD CHECK (SEX  in ('F','M'));

-- ----------------------------
-- Primary Key structure for table AG_APPLICANT_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD PRIMARY KEY ("APPLICANT_CODE", "TESTING_NO", "EXAM_PLACE_CODE");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_APPLICANT_T"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD FOREIGN KEY ("ACCEPT_OFF_CODE") REFERENCES "AGDOI"."AG_ACCEPT_OFF_R" ("ACCEPT_OFF_CODE");
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD FOREIGN KEY ("EDUCATION_CODE") REFERENCES "AGDOI"."AG_EDUCATION_R" ("EDUCATION_CODE");
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD FOREIGN KEY ("INSUR_COMP_CODE") REFERENCES "ASDOI"."AS_COMPANY_T" ("COMP_CODE") ENABLE NOVALIDATE;
ALTER TABLE "AGDOI"."AG_APPLICANT_T" ADD FOREIGN KEY ("TESTING_NO", "EXAM_PLACE_CODE") REFERENCES "AGDOI"."AG_EXAM_LICENSE_R" ("TESTING_NO", "EXAM_PLACE_CODE");
