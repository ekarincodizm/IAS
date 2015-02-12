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

Date: 2014-04-08 09:23:50
*/


-- ----------------------------
-- Table structure for AG_IAS_LICENSE_TYPE_R
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_LICENSE_TYPE_R";
CREATE TABLE "AGDOI"."AG_IAS_LICENSE_TYPE_R" (
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"LICENSE_TYPE_NAME" VARCHAR2(100 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
"AGENT_TYPE" VARCHAR2(1 BYTE) NULL ,
"ACTIVE_FLAG" VARCHAR2(1 BYTE) NULL ,
"INSURANCE_TYPE" NUMBER NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_LICENSE_TYPE_R"."INSURANCE_TYPE" IS 'ประเภทการประกันภัย, 1 = ชีวิต,2 = วินาศภัย, 3=ชีวิต/วินาศภัย';

-- ----------------------------
-- Records of AG_IAS_LICENSE_TYPE_R
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('01', 'ตัวแทนประกันชีวิต', null, null, 'A', 'A', 'Y', '1');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('02', 'ตัวแทนประกันวินาศภัย', null, null, 'A', 'A', 'Y', '2');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('03', 'การจัดการประกันชีวิตโดยตรง', null, null, 'A', 'B', 'Y', '1');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('04', 'การจัดการประกันวินาศภัยโดยตรง', null, null, 'A', 'B', 'Y', '2');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('05', 'การประกันภัยอุบัติเหตุส่วนบุคคลและประกันสุขภาพ', null, null, 'A', 'A', null, '2');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('06', 'พรบ.คุ้มครองผู้ประสบภัยจากรถ', null, null, 'A', 'A', null, '2');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('07', 'สำหรับการประกันภัยรายย่อย (ชีวิต)', null, null, 'A', 'A', 'Y', '1');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('08', 'สำหรับการประกันภัยรายย่อย (วินาศภัย)', null, null, 'A', 'A', 'Y', '2');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('11', 'การจัดการประกันวินาศภัย ประเภทต่อ', null, null, 'A', 'B', 'Y', '2');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_TYPE_R" VALUES ('12', 'การจัดการประกันชีวิต ประเภทต่อ', null, null, 'A', 'B', 'Y', '1');

-- ----------------------------
-- Indexes structure for table AG_IAS_LICENSE_TYPE_R
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_LICENSE_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_TYPE_R" ADD CHECK ("LICENSE_TYPE_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_LICENSE_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_TYPE_R" ADD PRIMARY KEY ("LICENSE_TYPE_CODE");
