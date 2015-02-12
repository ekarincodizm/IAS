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

Date: 2014-04-08 09:38:53
*/


-- ----------------------------
-- Table structure for AG_IAS_TRACE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_TRACE";
CREATE TABLE "AGDOI"."AG_IAS_TRACE" (
"TraceId" NUMBER NOT NULL ,
"APPLICATIONNAME" VARCHAR2(256 BYTE) NULL ,
"SOURCE" VARCHAR2(64 BYTE) NULL ,
"ID" NUMBER NOT NULL ,
"EVENTTYPE" VARCHAR2(32 BYTE) NOT NULL ,
"UTCDATETIME" DATE NOT NULL ,
"MACHINENAME" VARCHAR2(32 BYTE) NOT NULL ,
"APPDOMAINFRIENDLYNAME" VARCHAR2(512 BYTE) NOT NULL ,
"PROCESSID" NUMBER NOT NULL ,
"THREADNAME" VARCHAR2(512 BYTE) NULL ,
"MESSAGE" VARCHAR2(1500 BYTE) NULL ,
"ACTIVITYID" CHAR(36 BYTE) NULL ,
"RELATEDACTIVITYID" CHAR(36 BYTE) NULL ,
"LOGICALOPERATIONSTACK" VARCHAR2(512 BYTE) NULL ,
"DATA" CLOB NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_TRACE
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_TRACE
-- ----------------------------
create sequence AG_IAS_SEQUENCE;
-- ----------------------------
-- Triggers structure for table AG_IAS_TRACE
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."IAS_TRACE" BEFORE INSERT ON "AGDOI"."AG_IAS_TRACE" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
begin
      select AG_IAS_SEQUENCE.nextval
        into :new.id
      from dual;
    end;

-- ----------------------------
-- Checks structure for table AG_IAS_TRACE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("TraceId" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("ID" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("EVENTTYPE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("UTCDATETIME" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("MACHINENAME" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("APPDOMAINFRIENDLYNAME" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD CHECK ("PROCESSID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_TRACE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_TRACE" ADD PRIMARY KEY ("TraceId");
