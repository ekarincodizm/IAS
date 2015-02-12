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

Date: 2014-04-08 09:37:29
*/


-- ----------------------------
-- Table structure for AG_IAS_TEMP_PAYMENT_DETAIL_HIS
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS";
CREATE TABLE "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" (
"HIS_ID" VARCHAR2(15 BYTE) NOT NULL ,
"ID" VARCHAR2(15 BYTE) NOT NULL ,
"RECORD_TYPE" VARCHAR2(1 BYTE) NULL ,
"BANK_CODE" VARCHAR2(3 BYTE) NULL ,
"COMPANY_ACCOUNT" VARCHAR2(10 BYTE) NULL ,
"PAYMENT_DATE" VARCHAR2(8 BYTE) NULL ,
"PAYMENT_TIME" VARCHAR2(6 BYTE) NULL ,
"CUSTOMER_NAME" VARCHAR2(70 BYTE) NULL ,
"CUSTOMER_NO_REF1" VARCHAR2(20 BYTE) NULL ,
"REF2" VARCHAR2(20 BYTE) NULL ,
"REF3" VARCHAR2(20 BYTE) NULL ,
"BRANCH_NO" VARCHAR2(4 BYTE) NULL ,
"TELLER_NO" VARCHAR2(4 BYTE) NULL ,
"KIND_OF_TRANSACTION" VARCHAR2(1 BYTE) NULL ,
"TRANSACTION_CODE" VARCHAR2(3 BYTE) NULL ,
"CHEQUE_NO" VARCHAR2(7 BYTE) NULL ,
"AMOUNT" VARCHAR2(13 BYTE) NULL ,
"CHEQUE_BANK_CODE" VARCHAR2(3 BYTE) NULL ,
"HEADER_ID" VARCHAR2(15 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_TEMP_PAYMENT_DETAIL_HIS
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401141556931', '140401141104483', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000050000', '000', '140401141104475');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401141659673', '140401141638155', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000050000', '000', '140401141638149');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401141757197', '140401141741490', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000050000', '000', '140401141741484');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401141856118', '140401141822929', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000050000', '000', '140401141822924');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401141941276', '140401141929727', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000050000', '000', '140401141929721');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401142200606', '140401141929727', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561100000517', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000050000', '000', '140401141929721');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401142938742', '140401142855187', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000020000', '000', '140401142855181');
INSERT INTO "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" VALUES ('140401191616998', '140401191455268', 'D', '006', '3856001476', '28102013', '093008', 'เกษม  ตราชู                                       ', '999999561000000140  ', '16102013            ', '                    ', '0602', '9953', 'C', 'CSH', '       ', '0000000020000', '000', '140401191455253');

-- ----------------------------
-- Checks structure for table AG_IAS_TEMP_PAYMENT_DETAIL_HIS
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" ADD CHECK ("HIS_ID" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_TEMP_PAYMENT_DETAIL_HIS
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_TEMP_PAYMENT_DETAIL_HIS" ADD PRIMARY KEY ("HIS_ID") DISABLE;
