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

Date: 2014-04-09 14:47:44
*/


-- ----------------------------
-- Table structure for AG_U_TRAIN_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_U_TRAIN_T" (
"ID_CARD_NO" VARCHAR2(13 BYTE) NOT NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"TRAIN_TIMES" NUMBER(2) NOT NULL ,
"TRAIN_DATE" DATE NOT NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"F_FLAG" VARCHAR2(1 BYTE) NULL ,
"PAYMENT_NO" VARCHAR2(12 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_U_TRAIN_T" IS '????????????????? ??/??????????? UNIT LINK ??? AG_LICENSE_T ';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."ID_CARD_NO" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."LICENSE_TYPE_CODE" IS '?????????????????? ?????????? AG_LICENSE_TYPE_R';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."TRAIN_TIMES" IS '?????/???? ?????????????';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."TRAIN_DATE" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."USER_ID" IS '?????????????';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."USER_DATE" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."F_FLAG" IS 'Y = ?????????????????????????????????????????????? 4';
COMMENT ON COLUMN "AGDOI"."AG_U_TRAIN_T"."PAYMENT_NO" IS '????????? book ???????????????????????????????????????????????????????? 4';

-- ----------------------------
-- Indexes structure for table AG_U_TRAIN_T
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_U_TRAIN_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_U_TRAIN_T_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_U_TRAIN_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID := GET_SYS_USER;
   :NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Checks structure for table AG_U_TRAIN_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_U_TRAIN_T" ADD CHECK ("ID_CARD_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_U_TRAIN_T" ADD CHECK ("LICENSE_TYPE_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_U_TRAIN_T" ADD CHECK ("TRAIN_TIMES" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_U_TRAIN_T" ADD CHECK ("TRAIN_DATE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_U_TRAIN_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_U_TRAIN_T" ADD PRIMARY KEY ("ID_CARD_NO", "LICENSE_TYPE_CODE", "TRAIN_DATE");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_U_TRAIN_T"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_U_TRAIN_T" ADD FOREIGN KEY ("LICENSE_TYPE_CODE") REFERENCES "AGDOI"."AG_LICENSE_TYPE_R" ("LICENSE_TYPE_CODE");
