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

Date: 2014-04-08 09:06:28
*/


-- ----------------------------
-- Table structure for AG_IAS_DOCUMENT_TYPE
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_DOCUMENT_TYPE";
CREATE TABLE "AGDOI"."AG_IAS_DOCUMENT_TYPE" (
"DOCUMENT_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"DOCUMENT_NAME" VARCHAR2(500 BYTE) NULL ,
"USER_ID" VARCHAR2(20 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"MEMBER_TYPE_CODE" VARCHAR2(1 BYTE) NULL ,
"DOCUMENT_REQUIRE" VARCHAR2(1 BYTE) NULL ,
"IS_CARD_PIC" VARCHAR2(1 BYTE) NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
"TRAIN_DISCOUNT_STATUS" VARCHAR2(1 BYTE) NULL ,
"EXAM_DISCOUNT_STATUS" VARCHAR2(1 BYTE) NULL ,
"SPECIAL_TYPE_CODE_TRAIN" VARCHAR2(5 BYTE) NULL ,
"SPECIAL_TYPE_CODE_EXAM" VARCHAR2(5 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_DOCUMENT_TYPE
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('03', 'รูปถ่าย', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', 'Y', 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('04', '	สำเนาเอกสารทีเกี่ยวข้องกับการปลี่ยนชื่อ และ/หรือ นามสกุล (ถ้ามี)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('05', 'หนังสือแสดงความต้องการของบริษัทให้เป็นตัวแทนประกันชีวิต (แบบ ตช.5)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('06', 'ใบอนุญาตเป็นนายหน้าประกันชีวิตเลขที่.................(กรณีเป็นนายหน้าประกันชีวิตและใบอนุญาตยังไม่หมดอายุ)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('07', 'สำเนาทะเบียนบ้าน (กรณีเปลี่ยนที่อยู่)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('08', 'หนังสือแสดงความยินยอมของบริษัทให้เป็นตัวแทนประกันชีวิตของบริษัทอื่น (แบบ ตช.6)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('09', 'ใบอนุญาตเป็นตัวแทนประกันชีวิต (กรณียังไม่หมดอายุ) เลขที่.......................', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('10', 'หนังสืออนุมัติให้ลาออกหรือสำเนาหนังสือลาออกของบริษัทเดิมพร้อมทั้งไปรษณีย์ตอบรับ ซึ่งระบุวันที่ส่งไปรษณีย์ตอบรับก่อนวันยื่นคำขอรับใบอนุญาตเป็นตัวแทนประกันชีวิตเป็นเวลาไม่น้อยกว่า 30 วัน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('11', '	สำเนาบันทึกประจำวัน (กรณีบัตรหาย)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('12', '	สำเนาเอกสารที่เกี่ยวข้องกับการเปลี่ยนชื่อ-สกุล (กรณีเปลี่ยนชื่อ-สกุล)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('13', '	หนังสืออนุมัติให้ลาออกจากการเป็นตัวแทนประกันชีวิตจากบริษัท (กรณีใบอนุญาตยังไม่หมดอายุ ) หรือสำเนาหนังสือลาออกของบริษัทพร้อมทั้งไปรษณีย์ตอบรับ ซึ่งระบุวันที่ส่งไปรษณีย์ตอบรับก่อนวันยื่นคำขอรับใบอนุญาตเป็นนายหน้าประกันชีวิตเป็นเวลาไม่น้อยกว่า 30 วัน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('14', 'ใบอนุญาตเป็นตัวแทนประกันวินาศภัยเลขที่.................(กรณีใบอนุญาตยังไม่หมดอายุ)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('15', 'หนังสืออนุมัติให้ลาออกจากการเป็นตัวแทนประกันวินาศภัยจากบริษัท (กรณีใบอนุญาตยังไม่หมดอายุ ) หรือสำเนาหนังสือลาออกของบริษัทพร้อมทั้งไปรษณีย์ตอบรับ ซึ่งระบุวันที่ส่งไปรษณีย์ตอบรับก่อนวันยื่นคำขอรับใบอนุญาตเป็นนายหน้าประกันวินาศภัยเป็นเวลาไม่น้อยกว่า 30 วัน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('16', 'หนังสือแสดงความต้องการของบริษัทให้เป็นตัวแทนประกันวินาศภัย (แบบ ตว.5)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('17', 'ใบอนุญาตเป็นนายหน้าประกันวินาศภัยเลขที่.................(กรณีเป็นนายหน้าประกันวินาศภัยและใบอนุญาตยังไม่หมดอายุ)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('18', 'หนังสือแสดงความยินยอมของบริษัทให้เป็นตัวแทนประกันวินาศภัยของบริษัทอื่น (แบบ ตว.6)', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('50', 'สำเนารับรองการศึกษาวิชาการประกันชีวิตไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, 'Y', null, '50001');
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('01', 'สำเนาบัตรประจำตัวประชาชน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('02', 'สำเนาทะเบียนบ้าน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, null, null, null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('51', 'สำเนารับรองการศึกษาวิชาการประกันวินาศภัยไม่ต่ำกว่าชั้นปริญญาตรีหรือเทียบเท่าไม่น้อยกว่า 6 หน่วยกิต', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', null, 'Y', null, '50002');
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('60', 'FChFP', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '10001', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('61', 'CFP', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '10002', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('62', 'AFPT', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '10003', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('63', 'ประกาศณียบัตร', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', 'Y', 'A', 'Y', null, '10004', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('64', 'CII', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '10005', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('65', 'CIA', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '10006', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('66', 'บริหารธุรกิจ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '20001', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('67', 'การเงิน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '20002', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('68', 'การบัญชี', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '20003', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('69', 'สถิติ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '20004', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('70', 'กฎหมาย', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '20005', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('71', 'เศรษฐศาสตร์', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '20006', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('72', 'สาขาอื่นๆ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '29999', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('73', 'วิทยากร', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '30001', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('74', 'ผู้บรรยายความ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '30002', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('75', 'อาจารย์ประจำ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '30003', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('76', 'อาจารย์พิเศษ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '30004', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('77', 'กรรมการ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '40001', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('78', 'อนุกรรมการ', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '40002', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('79', 'ที่ปรึกษา', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '40003', null);
INSERT INTO "AGDOI"."AG_IAS_DOCUMENT_TYPE" VALUES ('80', 'คณะทำงาน', '130923093821787', TO_DATE('2014-03-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), null, 'N', null, 'A', 'Y', null, '40004', null);

-- ----------------------------
-- Checks structure for table AG_IAS_DOCUMENT_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_DOCUMENT_TYPE" ADD CHECK ("DOCUMENT_CODE" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_DOCUMENT_TYPE
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_DOCUMENT_TYPE" ADD PRIMARY KEY ("DOCUMENT_CODE") DISABLE;
