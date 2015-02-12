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

Date: 2014-04-09 14:40:11
*/


-- ----------------------------
-- Table structure for AG_EDUCATION_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_EDUCATION_R" (
"EDUCATION_CODE" VARCHAR2(3 BYTE) NOT NULL ,
"EDUCATION_NAME" VARCHAR2(60 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_EDUCATION_R" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_EDUCATION_R"."EDUCATION_CODE" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EDUCATION_R"."EDUCATION_NAME" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EDUCATION_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_EDUCATION_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Indexes structure for table AG_EDUCATION_R
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_EDUCATION_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_EDUCATION_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_EDUCATION_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID := GET_SYS_USER;
  	:NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_EDUCATION_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EDUCATION_R" ADD PRIMARY KEY ("EDUCATION_CODE");
