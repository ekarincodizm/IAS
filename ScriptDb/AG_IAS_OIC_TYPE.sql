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

Date: 2014-04-08 09:26:01
*/


-- ----------------------------
-- Table structure for AG_IAS_OIC_TYPE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_OIC_TYPE";
CREATE TABLE "AGDOI"."AG_IAS_OIC_TYPE" (
"OIC_CODE" VARCHAR2(1 BYTE) NOT NULL ,
"OIC_NAME" VARCHAR2(20 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_OIC_TYPE
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_OIC_TYPE" VALUES ('1', 'เจ้าหน้าที่ตัวแทน', 'AGDOI', TO_DATE('2013-02-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_OIC_TYPE" VALUES ('2', 'เจ้าหน้าที่การเงิน', 'AGDOI', TO_DATE('2013-02-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_IAS_OIC_TYPE" VALUES ('0', 'ผู้ดูแลระบบ', 'AGDOI', TO_DATE('2013-06-25 10:33:07', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Checks structure for table AG_IAS_OIC_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_OIC_TYPE" ADD CHECK ("OIC_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_OIC_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_OIC_TYPE" ADD PRIMARY KEY ("OIC_CODE") DISABLE;
