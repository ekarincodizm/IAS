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

Date: 2014-04-08 09:29:30
*/


-- ----------------------------
-- Table structure for AG_IAS_REGISTRATION_REMARK_T
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_REGISTRATION_REMARK_T";
CREATE TABLE "AGDOI"."AG_IAS_REGISTRATION_REMARK_T" (
"ID" VARCHAR2(15 BYTE) NOT NULL ,
"SEQ" NUMBER NOT NULL ,
"DESCRIPTION" VARCHAR2(2000 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_REGISTRATION_REMARK_T
-- ----------------------------

-- ----------------------------
-- Indexes structure for table AG_IAS_REGISTRATION_REMARK_T
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_REGISTRATION_REMARK_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_REGISTRATION_REMARK_T" ADD CHECK ("ID" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_REGISTRATION_REMARK_T" ADD CHECK ("SEQ" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_REGISTRATION_REMARK_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_REGISTRATION_REMARK_T" ADD PRIMARY KEY ("ID", "SEQ");
