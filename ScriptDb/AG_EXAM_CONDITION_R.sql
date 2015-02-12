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

Date: 2014-04-09 14:40:26
*/


-- ----------------------------
-- Table structure for AG_EXAM_CONDITION_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_EXAM_CONDITION_R" (
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"GRP_SUBJECT_CODE" NUMBER(2) NOT NULL ,
"SUBJECT_CODE" VARCHAR2(3 BYTE) NOT NULL ,
"EXAM_PASS" NUMBER(3) NULL ,
"MAX_SCORE" NUMBER(3) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_EXAM_CONDITION_R" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."LICENSE_TYPE_CODE" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."GRP_SUBJECT_CODE" IS '?????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."SUBJECT_CODE" IS '????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."EXAM_PASS" IS '???????????????(%)';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."MAX_SCORE" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_CONDITION_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Indexes structure for table AG_EXAM_CONDITION_R
-- ----------------------------
CREATE INDEX "AGDOI"."AGIN_EXAM_CON_GRP_SUB_INX"
ON "AGDOI"."AG_EXAM_CONDITION_R" ("GRP_SUBJECT_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_EXAM_CON_LIC_TYPE"
ON "AGDOI"."AG_EXAM_CONDITION_R" ("LICENSE_TYPE_CODE" ASC)
REVERSE
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_EXAM_CONDITION_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_EXAM_CONDITION_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_EXAM_CONDITION_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID 	:= GET_SYS_USER;
  	:NEW.USER_DATE 	:= SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_EXAM_CONDITION_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_CONDITION_R" ADD PRIMARY KEY ("LICENSE_TYPE_CODE", "GRP_SUBJECT_CODE", "SUBJECT_CODE");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_EXAM_CONDITION_R"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_CONDITION_R" ADD FOREIGN KEY ("LICENSE_TYPE_CODE") REFERENCES "AGDOI"."AG_LICENSE_TYPE_R" ("LICENSE_TYPE_CODE");
