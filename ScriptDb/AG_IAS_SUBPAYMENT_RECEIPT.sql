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

Date: 2014-05-19 14:49:11
*/


-- ----------------------------
-- Table structure for AG_IAS_SUBPAYMENT_RECEIPT
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT";
CREATE TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT" (
"PAYMENT_NO" VARCHAR2(14 BYTE) NOT NULL ,
"HEAD_REQUEST_NO" VARCHAR2(20 BYTE) NOT NULL ,
"PAYMENT_DATE" DATE NULL ,
"RECEIPT_DATE" DATE NULL ,
"AMOUNT" NUMBER(7,2) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"ACCOUNTING" VARCHAR2(1 BYTE) NULL ,
"DOWNLOAD_TIMES" NUMBER(5) NULL ,
"RECEIPT_BY_ID" VARCHAR2(150 BYTE) NULL ,
"RECEIVE_PATH" VARCHAR2(100 BYTE) NULL ,
"GUID" VARCHAR2(100 BYTE) NULL ,
"HASHING_CODE" VARCHAR2(100 BYTE) NULL ,
"SIGNATUER_POSITION" VARCHAR2(50 BYTE) NULL ,
"COPY_RECEIVE_PATH" VARCHAR2(100 BYTE) NULL ,
"SEQ_OF_SUBGROUP" VARCHAR2(4 BYTE) NULL ,
"GROUP_REQUEST_NO" VARCHAR2(20 BYTE) NOT NULL ,
"GEN_STATUS" VARCHAR2(1 BYTE) NULL ,
"RECEIPT_NO" VARCHAR2(20 BYTE) NOT NULL ,
"PRINT_TIMES" NUMBER NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT"."GEN_STATUS" IS 'สถานะการ approve ให้ user download W=ยังไม่สร้าง C = สร้าแล้ว';

-- ----------------------------
-- Records of AG_IAS_SUBPAYMENT_RECEIPT
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_SUBPAYMENT_RECEIPT
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_SUBPAYMENT_RECEIPT
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT" ADD CHECK ("PAYMENT_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT" ADD CHECK ("HEAD_REQUEST_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT" ADD CHECK ("GROUP_REQUEST_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT" ADD CHECK ("RECEIPT_NO" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_SUBPAYMENT_RECEIPT
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_SUBPAYMENT_RECEIPT" ADD PRIMARY KEY ("RECEIPT_NO");
