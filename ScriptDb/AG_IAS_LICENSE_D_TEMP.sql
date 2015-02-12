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

Date: 2014-04-08 09:22:45
*/


-- ----------------------------
-- Table structure for AG_IAS_LICENSE_D_TEMP
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_LICENSE_D_TEMP";
CREATE TABLE "AGDOI"."AG_IAS_LICENSE_D_TEMP" (
"UPLOAD_GROUP_NO" VARCHAR2(15 BYTE) NOT NULL ,
"SEQ_NO" VARCHAR2(4 BYTE) NOT NULL ,
"ORDERS" VARCHAR2(4 BYTE) NULL ,
"LICENSE_NO" VARCHAR2(15 BYTE) NULL ,
"LICENSE_DATE" DATE NULL ,
"LICENSE_EXPIRE_DATE" DATE NULL ,
"FEES" NUMBER NULL ,
"ID_CARD_NO" VARCHAR2(15 BYTE) NULL ,
"RENEW_TIMES" VARCHAR2(3 BYTE) NULL ,
"PRE_NAME_CODE" VARCHAR2(3 BYTE) NULL ,
"TITLE_NAME" VARCHAR2(20 BYTE) NULL ,
"NAMES" VARCHAR2(30 BYTE) NULL ,
"LASTNAME" VARCHAR2(35 BYTE) NULL ,
"ADDRESS_1" VARCHAR2(60 BYTE) NULL ,
"ADDRESS_2" VARCHAR2(60 BYTE) NULL ,
"AREA_CODE" VARCHAR2(8 BYTE) NULL ,
"CURRENT_ADDRESS_1" VARCHAR2(60 BYTE) NULL ,
"CURRENT_ADDRESS_2" VARCHAR2(60 BYTE) NULL ,
"CURRENT_AREA_CODE" VARCHAR2(8 BYTE) NULL ,
"EMAIL" VARCHAR2(50 BYTE) NULL ,
"AR_DATE" DATE NULL ,
"OLD_COMP_CODE" VARCHAR2(4 BYTE) NULL ,
"ERR_DESC" VARCHAR2(100 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_LICENSE_D_TEMP
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_LICENSE_D_TEMP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_D_TEMP" ADD CHECK ("UPLOAD_GROUP_NO" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_D_TEMP" ADD CHECK ("SEQ_NO" IS NOT NULL);
