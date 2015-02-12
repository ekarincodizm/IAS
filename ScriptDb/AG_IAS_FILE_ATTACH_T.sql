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

Date: 2014-04-08 09:20:08
*/


-- ----------------------------
-- Table structure for AG_IAS_FILE_ATTACH_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_FILE_ATTACH_T";
CREATE TABLE "AGDOI"."AG_IAS_FILE_ATTACH_T" (
"ID" VARCHAR2(15 BYTE) NOT NULL ,
"REFERENCE_ID" NUMBER NULL ,
"FILE_NAME" VARCHAR2(100 BYTE) NULL ,
"DOC_TYPE" VARCHAR2(2 BYTE) NULL ,
"FILE_STATUS" VARCHAR2(2 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_FILE_ATTACH_T
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_FILE_ATTACH_T
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_FILE_ATTACH_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_FILE_ATTACH_T" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_FILE_ATTACH_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_FILE_ATTACH_T" ADD PRIMARY KEY ("ID");
