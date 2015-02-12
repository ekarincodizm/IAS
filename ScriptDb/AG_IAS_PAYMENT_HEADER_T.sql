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

Date: 2014-04-08 09:27:02
*/


-- ----------------------------
-- Table structure for AG_IAS_PAYMENT_HEADER_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER_T";
CREATE TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER_T" (
"ID" VARCHAR2(2 BYTE) NOT NULL ,
"COMPANY_NAME" VARCHAR2(40 BYTE) NULL ,
"EFFECTIVE_DATE" DATE NULL ,
"SERVICE_CODE" VARCHAR2(8 BYTE) NULL ,
"PAY_AMOUNT" NUMBER NULL ,
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
-- Records of AG_IAS_PAYMENT_HEADER_T
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_PAYMENT_HEADER_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER_T" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_PAYMENT_HEADER_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_HEADER_T" ADD PRIMARY KEY ("ID") DISABLE;
