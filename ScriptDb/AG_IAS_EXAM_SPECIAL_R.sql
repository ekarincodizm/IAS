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

Date: 2014-04-08 09:14:29
*/


-- ----------------------------
-- Table structure for AG_IAS_EXAM_SPECIAL_R
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_EXAM_SPECIAL_R";
CREATE TABLE "AGDOI"."AG_IAS_EXAM_SPECIAL_R" (
"SPECIAL_TYPE_CODE" VARCHAR2(5 BYTE) NOT NULL ,
"SPECIAL_TYPE_DESC" VARCHAR2(100 BYTE) NULL ,
"USED_TYPE" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_SPECIAL_R"."USED_TYPE" IS 'L = ประกันชีวิตอย่างเดียว, D = ประกันวินาศภัยอย่างเดียว ,B = ใช้ได้ทั้งชีวิตและวินาศภัย';

-- ----------------------------
-- Records of AG_IAS_EXAM_SPECIAL_R
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_EXAM_SPECIAL_R" VALUES ('50001', 'สำเนารับรองการศึกษาวิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต', 'L');
INSERT INTO "AGDOI"."AG_IAS_EXAM_SPECIAL_R" VALUES ('50002', 'สำเนารับรองการศึกษาวิชาการประกันวินาศภัยไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต', 'D');

-- ----------------------------
-- Indexes structure for table AG_IAS_EXAM_SPECIAL_R
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_EXAM_SPECIAL_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_SPECIAL_R" ADD CHECK ("SPECIAL_TYPE_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_EXAM_SPECIAL_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_SPECIAL_R" ADD PRIMARY KEY ("SPECIAL_TYPE_CODE");
