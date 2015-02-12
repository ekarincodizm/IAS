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

Date: 2014-04-08 09:41:07
*/


-- ----------------------------
-- Table structure for AG_IAS_VALIDATE_LICENSE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE";
CREATE TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE" (
"ID" NUMBER NOT NULL ,
"ITEM" VARCHAR2(1000 BYTE) NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_VALIDATE_LICENSE
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('1', 'บรรลุนิติภาวะ', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('6', 'มีภูมิลำเนาในประเทศไทย', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('7', 'ไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('10', 'ไม่เป็นนายหน้าประกันชีวิต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('8', 'ไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุก 
ในความผิดเกี่ยวกับทรัพย์ที่กระทำโดยทุจริต
เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('9', 'ไม่เป็นบุคคลล้มละลาย', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('13', 'ไม่เคยถูกเพิกถอนใบอนุญาตเป็นตัวแทนประกันชีวิต
หรือใบอนุญาตเป็นนายหน้าประกันชีวิต
ระยะเวลาห้าปีก่อนวันขอรับใบอนุญาต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('14', 'มีการชำระค่าธรรมเนียมค่าขอรับใบอนุญาต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('15', 'ไม่เป็นนายหน้าประกันวินาศภัย', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('16', 'ไม่เคยถูกเพิกถอนใบอนุญาตเป็นตัวแทนประกันวินาศภัยหรือใบอนุญาตเป็นนายหน้าวินาศภัยระยะเวลาห้าปีก่อนวันขอรับใบอนุญาต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('11', 'ไม่เป็นตัวแทนประกันชีวิต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('12', 'ไม่เป็นตัวแทนประกันวินาศภัย', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('17', 'สำเร็จการศึกษาไม่ต่ำกว่าปริญญาตรี วิชาการประกันวินาศภัยไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('18', 'มีการชำระค่าธรรมเนียมค่ขอต่ออายุใบอนุญาต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('5', 'รูปถ่าย', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('2', 'ผลสอบ', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('3', 'สำเร็จการศึกษาไม่ต่ำกว่าปริญญาตรี วิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต', 'A');
INSERT INTO "AGDOI"."AG_IAS_VALIDATE_LICENSE" VALUES ('4', 'ผลอบรม', 'A');

-- ----------------------------
-- Indexes structure for table AG_IAS_VALIDATE_LICENSE
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_VALIDATE_LICENSE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_VALIDATE_LICENSE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_VALIDATE_LICENSE" ADD PRIMARY KEY ("ID");
