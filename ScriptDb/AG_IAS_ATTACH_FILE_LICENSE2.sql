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

Date: 2014-04-08 09:05:16
*/


-- ----------------------------
-- Table structure for AG_IAS_ATTACH_FILE_LICENSE2
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_ATTACH_FILE_LICENSE2";
CREATE TABLE "AGDOI"."AG_IAS_ATTACH_FILE_LICENSE2" (
"ID_ATTACH_FILE" VARCHAR2(15 BYTE) NOT NULL ,
"ID_CARD_NO" VARCHAR2(15 BYTE) NULL ,
"ATTACH_FILE_TYPE" VARCHAR2(4 BYTE) NULL ,
"ATTACH_FILE_PATH" VARCHAR2(100 BYTE) NULL ,
"REMARK" VARCHAR2(100 BYTE) NULL ,
"CREATED_BY" VARCHAR2(20 BYTE) NULL ,
"CREATED_DATE" DATE NULL ,
"UPDATED_BY" VARCHAR2(20 BYTE) NULL ,
"UPDATED_DATE" DATE NULL ,
"FILE_STATUS" VARCHAR2(1 BYTE) NULL ,
"LICENSE_NO" VARCHAR2(15 BYTE) NULL ,
"RENEW_TIME" VARCHAR2(2 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_ATTACH_FILE_LICENSE2
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_ATTACH_FILE_LICENSE2
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_ATTACH_FILE_LICENSE2" ADD CHECK ("ID_ATTACH_FILE" IS NOT NULL);
