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

Date: 2014-04-09 14:41:21
*/


-- ----------------------------
-- Table structure for AG_EXAM_TIME_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_EXAM_TIME_R" (
"TEST_TIME_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"TEST_TIME" VARCHAR2(15 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"USER_DATE_UPDATE" DATE NULL ,
"USER_ID_UPDATE" VARCHAR2(15 BYTE) NULL ,
"START_TIME" VARCHAR2(5 BYTE) NULL ,
"END_TIME" VARCHAR2(5 BYTE) NULL ,
"ACTIVE" VARCHAR2(1 CHAR) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_EXAM_TIME_R" IS '???????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_TIME_R"."TEST_TIME_CODE" IS '???????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_TIME_R"."TEST_TIME" IS '???????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_TIME_R"."USER_DATE" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_TIME_R"."ACTIVE" IS 'Y=ใช้อยู่ , N=ยกเลิกไม่ใช้';

-- ----------------------------
-- Indexes structure for table AG_EXAM_TIME_R
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_EXAM_TIME_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_EXAM_TIME_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_EXAM_TIME_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID 	:= GET_SYS_USER;
  	:NEW.USER_DATE 	:= SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_EXAM_TIME_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_TIME_R" ADD PRIMARY KEY ("TEST_TIME_CODE");
