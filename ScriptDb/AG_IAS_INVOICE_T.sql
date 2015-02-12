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

Date: 2014-04-08 09:21:59
*/


-- ----------------------------
-- Table structure for AG_IAS_INVOICE_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_INVOICE_T";
CREATE TABLE "AGDOI"."AG_IAS_INVOICE_T" (
"REQUEST_NO" VARCHAR2(20 BYTE) NOT NULL ,
"INVOICE_TYPE" VARCHAR2(1 BYTE) NULL ,
"PERSON_NO" NUMBER NULL ,
"INV_AMOUNT" NUMBER NULL ,
"TESTING_NO" VARCHAR2(6 BYTE) NULL ,
"APPLY_DATE" DATE NULL ,
"INVOICE_DATE" DATE NULL ,
"PAYMENT_BY" VARCHAR2(20 BYTE) NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
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
-- Records of AG_IAS_INVOICE_T
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_INVOICE_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_INVOICE_T" ADD CHECK ("REQUEST_NO" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_INVOICE_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_INVOICE_T" ADD PRIMARY KEY ("REQUEST_NO") DISABLE;
