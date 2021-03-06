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

Date: 2014-04-08 09:34:31
*/


-- ----------------------------
-- Table structure for AG_IAS_SPECIAL_T_TEMP
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_SPECIAL_T_TEMP";
CREATE TABLE "AGDOI"."AG_IAS_SPECIAL_T_TEMP" (
"ID_CARD_NO" VARCHAR2(13 BYTE) NOT NULL ,
"SPECIAL_TYPE_CODE" VARCHAR2(5 BYTE) NOT NULL ,
"START_DATE" DATE NULL ,
"END_DATE" DATE NULL ,
"SEND_DATE" DATE NULL ,
"SEND_BY" VARCHAR2(13 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"SEND_YEAR" VARCHAR2(4 BYTE) NOT NULL ,
"UNI_CODE" VARCHAR2(4 BYTE) NULL ,
"UNI_NAME" VARCHAR2(100 BYTE) NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
"TRAIN_DISCOUNT_STATUS" VARCHAR2(1 BYTE) NULL ,
"EXAM_DISCOUNT_STATUS" VARCHAR2(1 BYTE) NULL ,
"ID_ATTACH_FILE" VARCHAR2(15 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_SPECIAL_T_TEMP
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_SPECIAL_T_TEMP" VALUES ('7532493607310', '50002', TO_DATE('2014-04-02 13:21:54', 'YYYY-MM-DD HH24:MI:SS'), null, TO_DATE('2014-04-02 13:21:54', 'YYYY-MM-DD HH24:MI:SS'), 'IAS', '140108153944492', TO_DATE('2014-04-02 13:21:54', 'YYYY-MM-DD HH24:MI:SS'), '2557', null, null, 'W', null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_SPECIAL_T_TEMP" VALUES ('1980000000001', '50001', TO_DATE('2014-04-02 10:47:39', 'YYYY-MM-DD HH24:MI:SS'), null, TO_DATE('2014-04-02 10:47:39', 'YYYY-MM-DD HH24:MI:SS'), 'IAS', '131026163746250', TO_DATE('2014-04-02 10:47:40', 'YYYY-MM-DD HH24:MI:SS'), '2557', null, null, 'W', null, 'Y');
INSERT INTO "AGDOI"."AG_IAS_SPECIAL_T_TEMP" VALUES ('7532493607310', '10001', TO_DATE('2014-04-02 13:21:54', 'YYYY-MM-DD HH24:MI:SS'), null, TO_DATE('2014-04-02 13:21:54', 'YYYY-MM-DD HH24:MI:SS'), 'IAS', '140108153944492', TO_DATE('2014-04-02 13:21:54', 'YYYY-MM-DD HH24:MI:SS'), '2557', null, null, 'W', 'Y', null);

-- ----------------------------
-- Indexes structure for table AG_IAS_SPECIAL_T_TEMP
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AG_IAS_SPECIAL_T_TEMP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_SPECIAL_T_TEMP" ADD PRIMARY KEY ("ID_CARD_NO", "SPECIAL_TYPE_CODE", "SEND_YEAR");
