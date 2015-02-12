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

Date: 2014-04-09 14:38:55
*/


-- ----------------------------
-- Table structure for AG_AGENT_TYPE_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_AGENT_TYPE_R" (
"AGENT_TYPE" VARCHAR2(1 BYTE) NOT NULL ,
"AGENT_TYPE_DESC" VARCHAR2(50 BYTE) NULL ,
"AGENT_TYPE_REMARK" VARCHAR2(50 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_AGENT_TYPE_R" IS '?????????????????????/???????';
COMMENT ON COLUMN "AGDOI"."AG_AGENT_TYPE_R"."AGENT_TYPE" IS '??????????';
COMMENT ON COLUMN "AGDOI"."AG_AGENT_TYPE_R"."AGENT_TYPE_DESC" IS '??????????';
COMMENT ON COLUMN "AGDOI"."AG_AGENT_TYPE_R"."AGENT_TYPE_REMARK" IS '????????';
COMMENT ON COLUMN "AGDOI"."AG_AGENT_TYPE_R"."USER_ID" IS '?????????????';
COMMENT ON COLUMN "AGDOI"."AG_AGENT_TYPE_R"."USER_DATE" IS '?????????????????';

-- ----------------------------
-- Indexes structure for table AG_AGENT_TYPE_R
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_AGENT_TYPE_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_AGENT_TYPE_R_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_AGENT_TYPE_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
   :NEW.USER_ID := GET_SYS_USER;
   :NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Checks structure for table AG_AGENT_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_AGENT_TYPE_R" ADD CHECK ("AGENT_TYPE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_AGENT_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_AGENT_TYPE_R" ADD PRIMARY KEY ("AGENT_TYPE");
