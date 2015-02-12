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

Date: 2014-04-08 09:27:26
*/


-- ----------------------------
-- Table structure for AG_IAS_PAYMENT_RUNNINGNO
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO";
CREATE TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" (
"ID" VARCHAR2(10 BYTE) NOT NULL ,
"LAST_RUNNO" NUMBER NOT NULL ,
"LAST_UPDATE" DATE NOT NULL ,
"CREATE_DATE" DATE NOT NULL ,
"DOCUMENTNAME" VARCHAR2(50 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_PAYMENT_RUNNINGNO
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" VALUES ('KTB', '697', TO_DATE('2013-09-19 15:43:42', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2013-09-19 15:43:42', 'YYYY-MM-DD HH24:MI:SS'), 'bank ref1 running');
INSERT INTO "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" VALUES ('RECV', '20', TO_DATE('2013-09-20 12:38:32', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2013-09-20 12:38:32', 'YYYY-MM-DD HH24:MI:SS'), 'ลำดับหมายเลขใบสั่งจ่าย');

-- ----------------------------
-- Indexes structure for table AG_IAS_PAYMENT_RUNNINGNO
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_PAYMENT_RUNNINGNO
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" ADD CHECK ("ID" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" ADD CHECK ("LAST_RUNNO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" ADD CHECK ("LAST_UPDATE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" ADD CHECK ("CREATE_DATE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_PAYMENT_RUNNINGNO
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_PAYMENT_RUNNINGNO" ADD PRIMARY KEY ("ID");
