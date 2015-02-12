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

Date: 2014-04-08 09:06:04
*/


-- ----------------------------
-- Table structure for AG_IAS_CONFIG
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_CONFIG";
CREATE TABLE "AGDOI"."AG_IAS_CONFIG" (
"ID" VARCHAR2(4 BYTE) NOT NULL ,
"ITEM" VARCHAR2(100 BYTE) NULL ,
"ITEM_VALUE" VARCHAR2(2 BYTE) NULL ,
"DESCRIPTION" VARCHAR2(200 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"ITEM_TYPE" VARCHAR2(2 BYTE) NULL ,
"GROUP_CODE" VARCHAR2(20 BYTE) NOT NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_CONFIG"."ITEM_VALUE" IS 'GROUP_CODE = SP001(ITEM_VALUE = 0(สมาคม),1(บริษัทประกันภัย))';

-- ----------------------------
-- Records of AG_IAS_CONFIG
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('01', 'จำนวนปีในการค้นหาใบเสร็จ', '10', 'กำหนดช่วงเวลาในการดูใบเสร็จ', 'AGDOI', TO_DATE('2014-03-31 17:26:32', 'YYYY-MM-DD HH24:MI:SS'), '02', 'RC001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('02
', 'ใบสั่งจ่ายค่าขอรับใบอนุญาตใหม่', '0', 'กำหนดสิทธิการพิมพ์ใบสั่งจ่ายค่าขอรับใบอนุญาตใหม่', 'AGDOI', TO_DATE('2557-03-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '11', 'SP001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('03', 'ใบสั่งจ่ายค่าขอต่ออายุใบอนุญาต', '1', 'กำหนดสิทธิการพิมพ์ใบสั่งจ่ายค่าขอต่ออายุใบอนุญาต', '130923093821787', TO_DATE('2014-03-14 13:49:23', 'YYYY-MM-DD HH24:MI:SS'), '13', 'SP001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('04', 'ใบสั่งจ่ายค่าขอรับใบอนุญาตกรณี ขาดต่อ-ขอใหม่', '0', 'กำหนดสิทธิการพิมพ์ใบสั่งจ่ายค่าขอรับใบอนุญาตกรณี ขาดต่อ-ขอใหม่', 'AGDOI', TO_DATE('2557-03-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '15', 'SP001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('05', 'ใบสั่งจ่ายค่าขอใบแทนใบอนุญาต', '0', 'กำหนดสิทธิการพิมพ์ใบสั่งจ่ายค่าขอใบแทนใบอนุญาต', 'AGDOI', TO_DATE('2557-03-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '16', 'SP001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('06', 'ใบสั่งจ่ายค่าขอใบอนุญาตใบที่ 2', '0', 'กำหนดสิทธิการพิมพ์ใบสั่งจ่ายค่าขอใบอนุญาตใบที่ 2', 'AGDOI', TO_DATE('2557-03-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '18', 'SP001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('07', 'ใบสั่งจ่ายค่าขอใบนุญาตใหม่ กรณีย้ายบริษัท', '0', 'กำหนดสิทธิการพิมพ์ใบสั่งจ่ายค่าขอใบนุญาตใหม่ กรณีย้ายบริษัท', 'AGDOI', TO_DATE('2557-03-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '17', 'SP001');
INSERT INTO "AGDOI"."AG_IAS_CONFIG" VALUES ('08', 'จำนวนรายการต่อ 1 ใบสั่งจ่าย', '3', 'กำหนดจำนวนรายการต่อ 1 ใบสั่งจ่าย', '130923093821787', TO_DATE('2014-03-28 15:53:11', 'YYYY-MM-DD HH24:MI:SS'), '02', 'SP002');

-- ----------------------------
-- Indexes structure for table AG_IAS_CONFIG
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_CONFIG
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_CONFIG" ADD CHECK ("ID" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_CONFIG" ADD CHECK ("GROUP_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_CONFIG
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_CONFIG" ADD PRIMARY KEY ("ID", "GROUP_CODE");
