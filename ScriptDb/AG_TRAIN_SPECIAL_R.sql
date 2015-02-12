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

Date: 2014-04-09 14:47:02
*/


-- ----------------------------
-- Table structure for AG_TRAIN_SPECIAL_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_TRAIN_SPECIAL_R" (
"SPECIAL_TYPE_CODE" VARCHAR2(5 BYTE) NOT NULL ,
"SPECIAL_TYPE_DESC" VARCHAR2(100 BYTE) NULL ,
"USED_TYPE" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_TRAIN_SPECIAL_R"."USED_TYPE" IS 'L = ?????????????????????, D = ???????????????????????? ,B = ??????????????????????????';

-- ----------------------------
-- Indexes structure for table AG_TRAIN_SPECIAL_R
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_TRAIN_SPECIAL_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_TRAIN_SPECIAL_R" ADD CHECK ("SPECIAL_TYPE_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_TRAIN_SPECIAL_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_TRAIN_SPECIAL_R" ADD PRIMARY KEY ("SPECIAL_TYPE_CODE");
