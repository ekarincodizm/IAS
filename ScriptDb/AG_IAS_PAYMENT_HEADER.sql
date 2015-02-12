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

Date: 2014-05-19 15:57:20
*/


-- ----------------------------
-- Table structure for AG_IAS_PAYMENT_HEADER
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER";
CREATE TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER" (
"ID" VARCHAR2(15 BYTE) NOT NULL ,
"RECORD_TYPE" VARCHAR2(1 BYTE) NULL ,
"SEQUENCE_NO" VARCHAR2(6 BYTE) NULL ,
"BANK_CODE" VARCHAR2(3 BYTE) NULL ,
"COMPANY_ACCOUNT" VARCHAR2(10 BYTE) NULL ,
"COMPANY_NAME" VARCHAR2(40 BYTE) NULL ,
"EFFECTIVE_DATE" VARCHAR2(8 BYTE) NULL ,
"SERVICE_CODE" VARCHAR2(8 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Indexes structure for table AG_IAS_PAYMENT_HEADER
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_PAYMENT_HEADER
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_PAYMENT_HEADER
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER" ADD PRIMARY KEY ("ID");
