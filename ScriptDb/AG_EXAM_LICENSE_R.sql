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

Date: 2014-04-09 14:40:43
*/


-- ----------------------------
-- Table structure for AG_EXAM_LICENSE_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_EXAM_LICENSE_R" (
"TESTING_NO" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_PLACE_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"TESTING_DATE" DATE NULL ,
"TEST_TIME_CODE" VARCHAR2(2 BYTE) NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"EXAM_STATUS" VARCHAR2(1 BYTE) NULL ,
"EXAM_APPLY" NUMBER(4) NULL ,
"EXAM_ADMISSION" NUMBER(4) NULL ,
"EXAM_FEE" NUMBER(7,2) NULL ,
"EXAM_OWNER" VARCHAR2(1 BYTE) NULL ,
"SPECIAL" VARCHAR2(1 BYTE) NULL ,
"COURSE_NUMBER" NUMBER NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_EXAM_LICENSE_R" IS '????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."TESTING_NO" IS '???????????? (??????????)';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."EXAM_PLACE_CODE" IS '???????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."TESTING_DATE" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."TEST_TIME_CODE" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."LICENSE_TYPE_CODE" IS '????????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."USER_DATE" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_LICENSE_R"."EXAM_STATUS" IS '?????????????? E = ?????????, N = ????????????';

-- ----------------------------
-- Indexes structure for table AG_EXAM_LICENSE_R
-- ----------------------------
CREATE INDEX "AGDOI"."AGIN_EXAM_LICENSE_TYPE"
ON "AGDOI"."AG_EXAM_LICENSE_R" ("LICENSE_TYPE_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_EXAM_PLACE_CODE"
ON "AGDOI"."AG_EXAM_LICENSE_R" ("EXAM_PLACE_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_EXAM_TESTING_DATE"
ON "AGDOI"."AG_EXAM_LICENSE_R" ("TESTING_DATE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_EXAM_TESTING_NO"
ON "AGDOI"."AG_EXAM_LICENSE_R" ("TESTING_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_EXAM_TIME_CODE"
ON "AGDOI"."AG_EXAM_LICENSE_R" ("TEST_TIME_CODE" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_EXAM_LICENSE_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_EXAM_LICENSE_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_EXAM_LICENSE_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID  := GET_SYS_USER;
   :NEW.USER_DATE  := SYSDATE;

END;


-- ----------------------------
-- Checks structure for table AG_EXAM_LICENSE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_LICENSE_R" ADD CHECK (EXAM_STATUS in ('E','N'));

-- ----------------------------
-- Primary Key structure for table AG_EXAM_LICENSE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_LICENSE_R" ADD PRIMARY KEY ("TESTING_NO", "EXAM_PLACE_CODE");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_EXAM_LICENSE_R"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_LICENSE_R" ADD FOREIGN KEY ("LICENSE_TYPE_CODE") REFERENCES "AGDOI"."AG_LICENSE_TYPE_R" ("LICENSE_TYPE_CODE");
ALTER TABLE "AGDOI"."AG_EXAM_LICENSE_R" ADD FOREIGN KEY ("TEST_TIME_CODE") REFERENCES "AGDOI"."AG_EXAM_TIME_R" ("TEST_TIME_CODE");
