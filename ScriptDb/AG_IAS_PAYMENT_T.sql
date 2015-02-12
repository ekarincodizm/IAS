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

Date: 2014-04-08 09:27:45
*/


-- ----------------------------
-- Table structure for AG_IAS_PAYMENT_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PAYMENT_T";
CREATE TABLE "AGDOI"."AG_IAS_PAYMENT_T" (
"PAYMENT_NO" VARCHAR2(12 BYTE) NOT NULL ,
"PAYMENT_DATE" DATE NULL ,
"REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"ID_CARD_NO" VARCHAR2(13 BYTE) NULL ,
"LICENSE_NO" VARCHAR2(15 BYTE) NULL ,
"RECEIPT_NO" VARCHAR2(12 BYTE) NULL ,
"RECEIPT_DATE" DATE NULL ,
"AMOUNT" NUMBER(7,2) NULL ,
"CANCEL_REASON" VARCHAR2(300 BYTE) NULL ,
"RECORD_STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"OLD_LICENSE_NO" VARCHAR2(15 BYTE) NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"TESTING_NO" VARCHAR2(6 BYTE) NULL ,
"COMPANY_CODE" VARCHAR2(4 BYTE) NULL ,
"PAYMENT_REP" VARCHAR2(12 BYTE) NULL ,
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"ELICENSING_FLAG" VARCHAR2(1 BYTE) NULL ,
"INSURE_TYPE" VARCHAR2(1 BYTE) NULL ,
"DEPT_CODE" VARCHAR2(12 BYTE) NULL ,
"SPECIAL_TYPE_CODE" VARCHAR2(7 BYTE) NULL ,
"INVOICE_TYPE" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_PAYMENT_T
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_PAYMENT_T
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_PAYMENT_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_T" ADD CHECK ("PAYMENT_NO" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_PAYMENT_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_T" ADD PRIMARY KEY ("PAYMENT_NO");
