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

Date: 2014-04-08 09:23:28
*/


-- ----------------------------
-- Table structure for AG_IAS_LICENSE_H_TEMP
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_LICENSE_H_TEMP";
CREATE TABLE "AGDOI"."AG_IAS_LICENSE_H_TEMP" (
"UPLOAD_GROUP_NO" VARCHAR2(15 BYTE) NOT NULL ,
"COMP_CODE" VARCHAR2(4 BYTE) NULL ,
"COMP_NAME" VARCHAR2(200 BYTE) NULL ,
"TRAN_DATE" DATE NULL ,
"LOTS" NUMBER NULL ,
"MONEY" NUMBER NULL ,
"REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"PAYMENT_NO" VARCHAR2(20 BYTE) NULL ,
"FLAG_REQ" VARCHAR2(1 BYTE) NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"FILENAME" VARCHAR2(50 BYTE) NULL ,
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_LICENSE_H_TEMP
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_LICENSE_H_TEMP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_H_TEMP" ADD CHECK ("UPLOAD_GROUP_NO" IS NOT NULL);
