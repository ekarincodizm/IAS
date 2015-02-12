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

Date: 2014-04-08 09:07:47
*/


-- ----------------------------
-- Table structure for AG_IAS_EXAM_PLACE_ROOM
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_EXAM_PLACE_ROOM";
CREATE TABLE "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" (
"EXAM_ROOM_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"EXAM_ROOM_NAME" VARCHAR2(60 BYTE) NULL ,
"SEAT_AMOUNT" NUMBER(5) NULL ,
"EXAM_PLACE_CODE" VARCHAR2(6 BYTE) NOT NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"USER_ID_UPDATE" VARCHAR2(15 BYTE) NULL ,
"USER_DATE_UPDATE" DATE NULL ,
"ACTIVE" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" IS 'กำหนด ห้องสอบ ของสถานที่สอบ';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_PLACE_ROOM"."ACTIVE" IS 'Y=ใช้อยู่ , N=ยกเลิกไม่ใช้';

-- ----------------------------
-- Records of AG_IAS_EXAM_PLACE_ROOM
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" VALUES ('101', 'ห้องสอบ 101.1', '200', '13000', '130923093821787', TO_DATE('2014-03-27 14:41:53', 'YYYY-MM-DD HH24:MI:SS'), null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" VALUES ('102', 'ห้องสอบ 102', '100', '10111', '130923093821787', TO_DATE('2014-03-31 20:57:23', 'YYYY-MM-DD HH24:MI:SS'), null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" VALUES ('103', 'ห้องสอบที่ 103', '100', '10111', '130923093821787', TO_DATE('2014-03-19 14:00:42', 'YYYY-MM-DD HH24:MI:SS'), null, null, 'N');
INSERT INTO "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" VALUES ('104', 'ห้องสอบที่ 104', '100', '10111', '130923093821787', TO_DATE('2014-03-31 21:19:23', 'YYYY-MM-DD HH24:MI:SS'), null, null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" VALUES ('105', 'ห้องสอบ 105', '90', '10111', '130923093821787', TO_DATE('2014-03-31 20:18:31', 'YYYY-MM-DD HH24:MI:SS'), null, null, 'N');
INSERT INTO "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" VALUES ('106', 'ห้องสอบ สีชมพูวิ้งๆ', '10', '10111', '130923093821787', TO_DATE('2014-03-31 21:57:40', 'YYYY-MM-DD HH24:MI:SS'), null, null, 'N');

-- ----------------------------
-- Indexes structure for table AG_IAS_EXAM_PLACE_ROOM
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_EXAM_PLACE_ROOM
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" ADD CHECK ("EXAM_ROOM_CODE" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" ADD CHECK ("EXAM_PLACE_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_EXAM_PLACE_ROOM
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_PLACE_ROOM" ADD PRIMARY KEY ("EXAM_PLACE_CODE", "EXAM_ROOM_CODE");
