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

Date: 2014-04-09 14:47:33
*/


-- ----------------------------
-- Table structure for AG_TRAIN_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_TRAIN_T" (
"ID_CARD_NO" VARCHAR2(13 BYTE) NOT NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"TRAIN_TIMES" NUMBER(2) NOT NULL ,
"TRAIN_DATE" DATE NOT NULL ,
"TRAIN_DATE_EXP" DATE NULL ,
"TRAIN_COMP_CODE" VARCHAR2(5 BYTE) NOT NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_TRAIN_T" IS '????????????????? ??/??????????? ??? AG_LICENSE_T ';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."ID_CARD_NO" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."LICENSE_TYPE_CODE" IS '?????????????????? ?????????? AG_LICENSE_TYPE_R';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."TRAIN_TIMES" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."TRAIN_DATE" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."TRAIN_DATE_EXP" IS '?????????????';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."TRAIN_COMP_CODE" IS '??????????/????????/?????????????/?????????? AG_TRAIN_PLACE_R';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."USER_ID" IS '?????????????';
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_T"."USER_DATE" IS '?????????????????';

-- ----------------------------
-- Indexes structure for table AG_TRAIN_T
-- ----------------------------
CREATE INDEX "AGDOI"."AG_TRAIN_T_INX1"
ON "AGDOI"."AG_TRAIN_T" ("TRAIN_COMP_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_TRAIN_T_INX2"
ON "AGDOI"."AG_TRAIN_T" ("LICENSE_TYPE_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_TRAIN_T_INX3"
ON "AGDOI"."AG_TRAIN_T" ("TRAIN_TIMES" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_TRAIN_T_INX4"
ON "AGDOI"."AG_TRAIN_T" ("TRAIN_DATE" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_TRAIN_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_TRAIN_T_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_TRAIN_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID := GET_SYS_USER;
   :NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Checks structure for table AG_TRAIN_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD CHECK ("ID_CARD_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD CHECK ("LICENSE_TYPE_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD CHECK ("TRAIN_TIMES" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD CHECK ("TRAIN_DATE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD CHECK ("TRAIN_COMP_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_TRAIN_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD PRIMARY KEY ("ID_CARD_NO", "LICENSE_TYPE_CODE", "TRAIN_TIMES", "TRAIN_DATE");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_TRAIN_T"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_TRAIN_T" ADD FOREIGN KEY ("LICENSE_TYPE_CODE") REFERENCES "AGDOI"."AG_LICENSE_TYPE_R" ("LICENSE_TYPE_CODE");
