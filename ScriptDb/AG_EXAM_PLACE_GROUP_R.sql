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

Date: 2014-04-18 09:49:14
*/


-- ----------------------------
-- Table structure for "AGDOI"."AG_EXAM_PLACE_GROUP_R"
-- ----------------------------

CREATE TABLE "AGDOI"."AG_EXAM_PLACE_GROUP_R" (
"EXAM_PLACE_GROUP_CODE" VARCHAR2(3 BYTE) NOT NULL ,
"EXAM_PLACE_GROUP_NAME" VARCHAR2(60 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"ACTIVE" VARCHAR2(1 BYTE) NULL ,
"UPDATED_BY" VARCHAR2(15 BYTE) NULL ,
"UPDATED_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_EXAM_PLACE_GROUP_R" IS '??????????????? (????????????????)';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_PLACE_GROUP_R"."EXAM_PLACE_GROUP_CODE" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_PLACE_GROUP_R"."EXAM_PLACE_GROUP_NAME" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_PLACE_GROUP_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_EXAM_PLACE_GROUP_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Records of AG_EXAM_PLACE_GROUP_R
-- ----------------------------
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('111', 'สำนักงาน คปภ.', 'AGDOI', TO_DATE('2014-04-17 15:06:45', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('222', 'สถาบันประกันภัยไทย', 'AGDOI', TO_DATE('2014-04-17 15:07:02', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('999', 'สมาคมประกันชีวิตไทย', 'AGDOI', TO_DATE('2014-04-17 15:07:17', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('666', 'ธนาคาร', 'AGDOI', TO_DATE('2014-04-17 15:07:24', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('000', 'ศูนย์สอบประจำจังหวัด', 'AGDOI', TO_DATE('2014-04-17 15:07:28', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('555', 'สนามสอบนายหน้าประกันวินาศภัย', 'AGDOI', TO_DATE('2014-04-17 15:07:47', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('333', 'สมาคมประกันวินาศภัย (ต่างจังหวัด)', 'AGDOI', TO_DATE('2014-04-17 15:07:52', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('444', 'สนามสอบนายหน้าประกันชีวิต', 'AGDOI', TO_DATE('2014-04-17 15:07:56', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('098', 'สมาคมประกันวินาศภัย', 'AGDOI', TO_DATE('2014-04-17 15:08:09', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('777', 'คปภ.(คอมพิวเตอร์)', 'AGDOI', TO_DATE('2014-04-17 15:08:17', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('888', 'บริษัทโบรกเกอร์', 'AGDOI', TO_DATE('2014-04-17 15:08:22', 'YYYY-MM-DD HH24:MI:SS'), 'Y', null, null);
INSERT INTO "AGDOI"."AG_EXAM_PLACE_GROUP_R" VALUES ('123', 'abc', 'AGDOI', TO_DATE('2014-04-18 09:28:53', 'YYYY-MM-DD HH24:MI:SS'), 'Y', '130923093821787', TO_DATE('2014-04-18 09:39:57', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Indexes structure for table AG_EXAM_PLACE_GROUP_R
-- ----------------------------

-- ----------------------------
-- Triggers structure for table "AGDOI"."AG_EXAM_PLACE_GROUP_R"
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_EXAM_PLACEGRP_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_EXAM_PLACE_GROUP_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID  := GET_SYS_USER;
   :NEW.USER_DATE  := SYSDATE;
END;
;

-- ----------------------------
-- Primary Key structure for table "AGDOI"."AG_EXAM_PLACE_GROUP_R"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_EXAM_PLACE_GROUP_R" ADD PRIMARY KEY ("EXAM_PLACE_GROUP_CODE");
