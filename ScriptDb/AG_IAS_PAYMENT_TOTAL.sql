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

Date: 2014-05-19 15:57:50
*/


-- ----------------------------
-- Table structure for AG_IAS_PAYMENT_TOTAL
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PAYMENT_TOTAL";
CREATE TABLE "AGDOI"."AG_IAS_PAYMENT_TOTAL" (
"ID" NVARCHAR2(15) NOT NULL ,
"RECORD_TYPE" VARCHAR2(1 BYTE) NULL ,
"SEQUENCE_NO" VARCHAR2(6 BYTE) NULL ,
"BANK_CODE" VARCHAR2(3 BYTE) NULL ,
"COMPANY_ACCOUNT" VARCHAR2(10 BYTE) NULL ,
"TOTAL_DEBIT_AMOUNT" VARCHAR2(13 BYTE) NULL ,
"TOTAL_DEBIT_TRANSACTION" VARCHAR2(6 BYTE) NULL ,
"TOTAL_CREDIT_AMOUNT" VARCHAR2(13 BYTE) NULL ,
"TOTAL_CREDIT_TRANSACTION" VARCHAR2(6 BYTE) NULL ,
"HEADER_ID" VARCHAR2(15 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Checks structure for table AG_IAS_PAYMENT_TOTAL
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_TOTAL" ADD CHECK ("ID" IS NOT NULL);
