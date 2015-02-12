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

Date: 2014-04-09 14:44:18
*/


-- ----------------------------
-- Table structure for AG_PETITION_TYPE_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_PETITION_TYPE_R" (
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"PETITION_TYPE_NAME" VARCHAR2(60 BYTE) NULL ,
"FEE" NUMBER(8,2) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"IAS_FLAG" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_PETITION_TYPE_R" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_PETITION_TYPE_R"."PETITION_TYPE_CODE" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_PETITION_TYPE_R"."PETITION_TYPE_NAME" IS '????????????????';
COMMENT ON COLUMN "AGDOI"."AG_PETITION_TYPE_R"."FEE" IS '????????????(???)';
COMMENT ON COLUMN "AGDOI"."AG_PETITION_TYPE_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_PETITION_TYPE_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Indexes structure for table AG_PETITION_TYPE_R
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AG_PETITION_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_PETITION_TYPE_R" ADD PRIMARY KEY ("PETITION_TYPE_CODE");
