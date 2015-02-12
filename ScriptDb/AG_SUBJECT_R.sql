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

Date: 2014-04-09 14:44:32
*/


-- ----------------------------
-- Table structure for AG_SUBJECT_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_SUBJECT_R" (
"SUBJECT_CODE" VARCHAR2(3 BYTE) NOT NULL ,
"SUBJECT_NAME" VARCHAR2(50 BYTE) NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
"MAX_SCORE" NUMBER(3) NULL ,
"GROUP_ID" NUMBER NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_SUBJECT_R" IS '???????';
COMMENT ON COLUMN "AGDOI"."AG_SUBJECT_R"."SUBJECT_CODE" IS '????????';
COMMENT ON COLUMN "AGDOI"."AG_SUBJECT_R"."SUBJECT_NAME" IS '????????';
COMMENT ON COLUMN "AGDOI"."AG_SUBJECT_R"."LICENSE_TYPE_CODE" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_SUBJECT_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_SUBJECT_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Indexes structure for table AG_SUBJECT_R
-- ----------------------------
CREATE INDEX "AGDOI"."AGIN_SUBJECTR_LICENSE_TYPE"
ON "AGDOI"."AG_SUBJECT_R" ("LICENSE_TYPE_CODE" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_SUBJECT_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_SUBJECT_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_SUBJECT_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID 	:= GET_SYS_USER;
  	:NEW.USER_DATE 	:= SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_SUBJECT_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_SUBJECT_R" ADD PRIMARY KEY ("SUBJECT_CODE", "LICENSE_TYPE_CODE");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_SUBJECT_R"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_SUBJECT_R" ADD FOREIGN KEY ("LICENSE_TYPE_CODE") REFERENCES "AGDOI"."AG_LICENSE_TYPE_R" ("LICENSE_TYPE_CODE");
