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

Date: 2014-04-09 14:47:19
*/


-- ----------------------------
-- Table structure for AG_TRAIN_SPECIAL_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_TRAIN_SPECIAL_T" (
"ID_CARD_NO" VARCHAR2(13 BYTE) NOT NULL ,
"SPECIAL_TYPE_CODE" VARCHAR2(5 BYTE) NOT NULL ,
"START_DATE" DATE NULL ,
"END_DATE" DATE NULL ,
"SEND_DATE" DATE NULL ,
"SEND_BY" VARCHAR2(13 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"SEND_YEAR" VARCHAR2(4 BYTE) NOT NULL ,
"UNI_CODE" VARCHAR2(4 BYTE) NULL ,
"UNI_NAME" VARCHAR2(100 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Indexes structure for table AG_TRAIN_SPECIAL_T
-- ----------------------------
CREATE INDEX "AGDOI"."AG_TRAIN_SPECIAL_T_IND1"
ON "AGDOI"."AG_TRAIN_SPECIAL_T" ("ID_CARD_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_TRAIN_SPECIAL_T_IND2"
ON "AGDOI"."AG_TRAIN_SPECIAL_T" ("ID_CARD_NO" ASC, "END_DATE" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_TRAIN_SPECIAL_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_TRAIN_SPECIAL_T_TRG" BEFORE INSERT ON "AGDOI"."AG_TRAIN_SPECIAL_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID := GET_SYS_USER;
   :NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_TRAIN_SPECIAL_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_TRAIN_SPECIAL_T" ADD PRIMARY KEY ("ID_CARD_NO", "SPECIAL_TYPE_CODE", "SEND_YEAR");
