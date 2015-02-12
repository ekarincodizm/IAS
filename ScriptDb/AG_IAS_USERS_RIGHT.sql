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

Date: 2014-04-08 09:40:44
*/


-- ----------------------------
-- Table structure for AG_IAS_USERS_RIGHT
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_USERS_RIGHT";
CREATE TABLE "AGDOI"."AG_IAS_USERS_RIGHT" (
"USER_RIGHT" VARCHAR2(5 BYTE) NOT NULL ,
"RIGHT_DESCRIPTION" VARCHAR2(100 BYTE) NULL ,
"FUNCTION_ID" VARCHAR2(10 BYTE) NOT NULL ,
"VISIBLE" VARCHAR2(20 BYTE) NULL ,
"CREATED_BY" VARCHAR2(20 BYTE) NULL ,
"CREATED_DATE" DATE NULL ,
"UPDATED_BY" VARCHAR2(20 BYTE) NULL ,
"UPDATED_DATE" DATE NULL ,
"OIC_TYPE" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_USERS_RIGHT
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '2', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '3', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '4', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '5', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '6', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '7', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '3', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '5', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '6', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '7', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '12', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '13', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '14', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '3', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '15', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '16', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '5', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '6', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '7', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '17', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '18', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '12', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '3', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '16', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '5', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '47', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '20', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '6', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '21', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '7', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '22', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '23', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '24', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '25', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '26', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '27', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '13', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '28', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '29', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '30', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '31', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '31', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '31', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '32', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '33', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '32', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '31', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '12', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '3', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '33', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '19', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '20', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '6', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '21', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '7', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '33', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '22', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '18', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '12', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '3', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '16', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '5', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '30', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '34', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '35', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '36', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '37', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '9', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '9', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '11', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '11', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '38', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '34', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '39', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '45', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '46', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '48', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '49', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '50', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '51', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '52', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '53', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '54', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '11', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '9', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '45', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '45', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '45', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '13', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '45', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '24', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('5', 'คปภ.การเงิน', '23', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '14', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '22', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '6', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '55', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '19', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '19', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '19', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '14', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '14', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '56', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '57', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '2', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '2', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '2', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '16', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '29', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '1', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '22', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('1', 'บุคคลทั่วไป', '58', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '1', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '1', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '8', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '8', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '8', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '10', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '10', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '10', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '60', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('6', 'คปภ.ตัวแทน', '59', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '59', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '62', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('2', 'บริษัท', '63', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '64', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '64', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '64', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '65', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '65', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '66', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '66', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('7', 'เจ้าหน้าที่สนามสอบ', '67', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ', '67', null, null, null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('4', 'คปภ.ตัวแทน', '68', null, null, TO_DATE('2557-03-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, null, null);
INSERT INTO "AGDOI"."AG_IAS_USERS_RIGHT" VALUES ('3', 'สมาคม', '2', null, null, null, null, null, null);

-- ----------------------------
-- Indexes structure for table AG_IAS_USERS_RIGHT
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_USERS_RIGHT
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_USERS_RIGHT" ADD CHECK ("USER_RIGHT" IS NOT NULL);
ALTER TABLE "AGDOI"."AG_IAS_USERS_RIGHT" ADD CHECK ("FUNCTION_ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_USERS_RIGHT
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_USERS_RIGHT" ADD PRIMARY KEY ("USER_RIGHT", "FUNCTION_ID");
