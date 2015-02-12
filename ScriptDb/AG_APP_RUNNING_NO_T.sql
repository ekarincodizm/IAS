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

Date: 2014-04-09 14:39:13
*/


-- ----------------------------
-- Table structure for AG_APP_RUNNING_NO_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_APP_RUNNING_NO_T" (
"LEAD_TESTING" VARCHAR2(6 BYTE) NOT NULL ,
"LAST_APP_NO" VARCHAR2(6 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_APP_RUNNING_NO_T" IS '??????????????????????????????? ??????';
COMMENT ON COLUMN "AGDOI"."AG_APP_RUNNING_NO_T"."LEAD_TESTING" IS '???????????? (??????????)';
COMMENT ON COLUMN "AGDOI"."AG_APP_RUNNING_NO_T"."LAST_APP_NO" IS '??????????????? ??????';
COMMENT ON COLUMN "AGDOI"."AG_APP_RUNNING_NO_T"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_APP_RUNNING_NO_T"."USER_DATE" IS '????????????';

-- ----------------------------
-- Indexes structure for table AG_APP_RUNNING_NO_T
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_APP_RUNNING_NO_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_APP_RUNNING_NO_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_APP_RUNNING_NO_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID  := GET_SYS_USER;
   :NEW.USER_DATE  := SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_APP_RUNNING_NO_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_APP_RUNNING_NO_T" ADD PRIMARY KEY ("LEAD_TESTING");
