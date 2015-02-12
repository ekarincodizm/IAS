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

Date: 2014-04-08 09:34:08
*/


-- ----------------------------
-- Table structure for AG_IAS_RESET_HISTORY
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_RESET_HISTORY";
CREATE TABLE "AGDOI"."AG_IAS_RESET_HISTORY" (
"RESET_ID" VARCHAR2(20 BYTE) NOT NULL ,
"ID_CARD_NO" VARCHAR2(13 BYTE) NULL ,
"EMAIL" VARCHAR2(255 BYTE) NULL ,
"RESET_TIMES" NUMBER(5) NULL ,
"CREATED_BY" VARCHAR2(20 BYTE) NULL ,
"CREATED_DATE" DATE NULL ,
"UPDATED_BY" VARCHAR2(20 BYTE) NULL ,
"UPDATED_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_RESET_HISTORY
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_RESET_HISTORY
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_RESET_HISTORY
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_RESET_HISTORY" ADD CHECK ("RESET_ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_RESET_HISTORY
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_RESET_HISTORY" ADD PRIMARY KEY ("RESET_ID");
