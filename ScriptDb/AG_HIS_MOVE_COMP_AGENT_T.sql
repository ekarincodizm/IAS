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

Date: 2014-04-09 14:41:45
*/


-- ----------------------------
-- Table structure for AG_HIS_MOVE_COMP_AGENT_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" (
"LICENSE_NO" VARCHAR2(15 BYTE) NOT NULL ,
"ID_CARD_NO" VARCHAR2(13 BYTE) NOT NULL ,
"MOVE_TIME" NUMBER(2) NOT NULL ,
"MOVE_DATE" DATE NULL ,
"COMP_MOVE_OUT_ID" VARCHAR2(4 BYTE) NULL ,
"COMP_MOVE_IN_ID" VARCHAR2(4 BYTE) NULL ,
"NEW_LICENSE_NO" VARCHAR2(15 BYTE) NULL ,
"REQUEST_NO" VARCHAR2(8 BYTE) NULL ,
"PAYMENT_NO" VARCHAR2(12 BYTE) NULL ,
"CANCEL_REASON" VARCHAR2(300 BYTE) NULL ,
"RECORD_STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"MOVE_FLAG" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" IS '?????????????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."LICENSE_NO" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."ID_CARD_NO" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."MOVE_TIME" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."MOVE_DATE" IS '??????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."COMP_MOVE_OUT_ID" IS '????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."COMP_MOVE_IN_ID" IS '?????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."NEW_LICENSE_NO" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."REQUEST_NO" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."PAYMENT_NO" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."CANCEL_REASON" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."RECORD_STATUS" IS '??????????????  X = cancel ??????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."USER_ID" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."USER_DATE" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"."MOVE_FLAG" IS '?????????????????? Y = ??????????????????????????  N = ?????????????????????????';

-- ----------------------------
-- Indexes structure for table AG_HIS_MOVE_COMP_AGENT_T
-- ----------------------------
CREATE INDEX "AGDOI"."AGIN_HMOVE_COMP_OUT"
ON "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ("COMP_MOVE_OUT_ID" ASC)
REVERSE
LOGGING;
CREATE INDEX "AGDOI"."AGIN_HMOVE_DATE"
ON "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ("MOVE_DATE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_HMOVE_ID_CARD"
ON "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ("ID_CARD_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_HMOVE_LICENSE"
ON "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ("LICENSE_NO" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_HIS_MOVE_COMP_AGENT_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_HIS_MOVE_COMP_AGENT_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID := GET_SYS_USER;
  	:NEW.USER_DATE := SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_HIS_MOVE_COMP_AGENT_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ADD PRIMARY KEY ("LICENSE_NO", "ID_CARD_NO", "MOVE_TIME");

-- ----------------------------
-- Foreign Key structure for table "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T"
-- ----------------------------
ALTER TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ADD FOREIGN KEY ("LICENSE_NO") REFERENCES "AGDOI"."AG_AGENT_LICENSE_T" ("LICENSE_NO") DISABLE;
ALTER TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ADD FOREIGN KEY ("ID_CARD_NO") REFERENCES "AGDOI"."AG_PERSONAL_T" ("ID_CARD_NO");
ALTER TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ADD FOREIGN KEY ("COMP_MOVE_IN_ID") REFERENCES "ASDOI"."AS_COMPANY_T" ("COMP_CODE");
ALTER TABLE "AGDOI"."AG_HIS_MOVE_COMP_AGENT_T" ADD FOREIGN KEY ("COMP_MOVE_OUT_ID") REFERENCES "ASDOI"."AS_COMPANY_T" ("COMP_CODE");