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

Date: 2014-04-08 09:26:23
*/


-- ----------------------------
-- Table structure for AG_IAS_PAYMENT_EXPIRE_DAY
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY";
CREATE TABLE "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" (
"ID" VARCHAR2(2 BYTE) NOT NULL ,
"DESCRIPTION" VARCHAR2(200 BYTE) NULL ,
"PAYMENT_EXPIRE_DAY" NUMBER NULL ,
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
-- Records of AG_IAS_PAYMENT_EXPIRE_DAY
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('01', 'ค่าสมัครสอบ', '1', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('11', 'ขอรับใบอนุญาตใหม่', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('13', 'ขอต่ออายุใบอนุญาต 1 ปี', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('14', 'ขอต่ออายุใบอนุญาต 5 ปี', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('15', 'ขาดต่อขอใหม่', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('16', 'ใบแทนใบอนุญาต', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('17', 'ใบอนุญาต (ย้ายบริษัท)', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" VALUES ('18', 'ใบอนุญาต (ใบที่ 2)', '3', null, null, 'AR01', TO_DATE('2014-01-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Indexes structure for table AG_IAS_PAYMENT_EXPIRE_DAY
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_PAYMENT_EXPIRE_DAY
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_PAYMENT_EXPIRE_DAY
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_EXPIRE_DAY" ADD PRIMARY KEY ("ID");
