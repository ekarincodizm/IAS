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

Date: 2014-04-08 09:14:05
*/


-- ----------------------------
-- Table structure for AG_IAS_EXAM_SCHEDULE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_EXAM_SCHEDULE";
CREATE TABLE "AGDOI"."AG_IAS_EXAM_SCHEDULE" (
"EXAM_ID" VARCHAR2(15 BYTE) NOT NULL ,
"EXAM_DATE" DATE NULL ,
"EXAM_TIME_START" DATE NULL ,
"EXAM_TIME_FINISH" DATE NULL ,
"EXAM_APPLY" NUMBER NULL ,
"EXAM_ADMISSION" NUMBER NULL ,
"EXAM_FEE" VARCHAR2(20 BYTE) NULL ,
"EXAM_PLACE" VARCHAR2(20 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_EXAM_SCHEDULE
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_EXAM_SCHEDULE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_SCHEDULE" ADD CHECK ("EXAM_ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_EXAM_SCHEDULE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_SCHEDULE" ADD PRIMARY KEY ("EXAM_ID") DISABLE;
