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

Date: 2014-04-09 14:34:57
*/


-- ----------------------------
-- Table structure for AG_ACCEPT_OFF_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_ACCEPT_OFF_R" (
"ACCEPT_OFF_CODE" VARCHAR2(3 BYTE) NOT NULL ,
"OFF_NAME" VARCHAR2(50 BYTE) NULL ,
"PROVINCE_CODE" VARCHAR2(3 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_ACCEPT_OFF_R" IS '???????????????';
COMMENT ON COLUMN "AGDOI"."AG_ACCEPT_OFF_R"."ACCEPT_OFF_CODE" IS '???????????';
COMMENT ON COLUMN "AGDOI"."AG_ACCEPT_OFF_R"."OFF_NAME" IS '???????????';
COMMENT ON COLUMN "AGDOI"."AG_ACCEPT_OFF_R"."PROVINCE_CODE" IS '???????????';
COMMENT ON COLUMN "AGDOI"."AG_ACCEPT_OFF_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_ACCEPT_OFF_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Records of AG_ACCEPT_OFF_R
-- ----------------------------
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('999', 'สมาคมประกันชีวิตไทย', '10', 'AGDOI', TO_DATE('2005-02-14 13:51:41', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('010', 'ศูนย์สอบประจำจังหวัดกรุงเทพฯ', '10', 'AGDOI', TO_DATE('2004-08-20 14:53:43', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('012', 'สำนักงานประกันภัยจังหวัดนนทบุรี', '12', 'AGDOI', TO_DATE('2004-08-25 15:17:20', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('222', 'สถาบันประกันภัยไทย', '10', 'AGDOI', TO_DATE('2004-08-26 15:07:31', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('098', 'สมาคมประกันวินาศภัยไทย', '10', 'AGA112', TO_DATE('2012-11-02 15:48:10', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('111', 'สำนักงาน คปภ.', '10', 'AGA112', TO_DATE('2010-08-23 10:25:12', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('013', 'สำนักงานประกันภัยจังหวัดปทุมธานี', '13', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('011', 'สำนักงานประกันภัยจังหวัดสมุทรปราการ', '11', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('015', 'สำนักงานประกันภัยจังหวัดอ่างทอง', '15', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('016', 'สำนักงานประกันภัยจังหวัดลพบุรี', '16', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('019', 'สำนักงานประกันภัยจังหวัดสระบุรี', '19', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('020', 'สำนักงานประกันภัยจังหวัดชลบุรี', '20', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('021', 'สำนักงานประกันภัยจังหวัดระยอง', '21', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('022', 'สำนักงานประกันภัยจังหวัดจันทบุรี', '22', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('023', 'สำนักงานประกันภัยจังหวัดตราด', '23', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('027', 'สำนักงานประกันภัยจังหวัดสระแก้ว', '27', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('024', 'สำนักงานประกันภัยจังหวัดฉะเชิงเทรา', '24', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('025', 'สำนักงานประกันภัยจังหวัดปราจีนบุรี', '25', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('026', 'สำนักงานประกันภัยจังหวัดนครนายก', '26', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('017', 'สำนักงานประกันภัยจังหวัดสิงห์บุรี', '17', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('018', 'สำนักงานประกันภัยจังหวัดชัยนาท', '18', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('032', 'สำนักงานประกันภัยจังหวัดสุรินทร์', '32', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('033', 'สำนักงานประกันภัยจังหวัดศรีสะเกษ', '33', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('031', 'สำนักงานประกันภัยจังหวัดบุรีรัมย์', '31', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('034', 'สำนักงานประกันภัยจังหวัดอุบลราชธานี', '34', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('037', 'สำนักงานประกันภัยจังหวัดอำนาจเจริญ', '37', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('040', 'สำนักงานประกันภัยจังหวัดขอนแก่น', '40', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('042', 'สำนักงานประกันภัยจังหวัดเลย', '42', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('043', 'สำนักงานประกันภัยจังหวัดหนองคาย', '43', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('044', 'สำนักงานประกันภัยจังหวัดมหาสารคาม', '44', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('041', 'สำนักงานประกันภัยจังหวัดอุดรธานี', '41', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('035', 'สำนักงานประกันภัยจังหวัดยโสธร', '35', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('036', 'สำนักงานประกันภัยจังหวัดชัยภูมิ', '36', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('048', 'สำนักงานประกันภัยจังหวัดนครพนม', '48', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('049', 'สำนักงานประกันภัยจังหวัดมุกดาหาร', '49', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('045', 'สำนักงานประกันภัยจังหวัดร้อยเอ็ด', '45', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('050', 'สำนักงานประกันภัยจังหวัดเชียงใหม่', '50', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('051', 'สำนักงานประกันภัยจังหวัดลำพูน', '51', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('053', 'สำนักงานประกันภัยจังหวัดอุตรดิตถ์', '53', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('054', 'สำนักงานประกันภัยจังหวัดแพร่', '54', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('055', 'สำนักงานประกันภัยจังหวัดน่าน', '55', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('056', 'สำนักงานประกันภัยจังหวัดพะเยา', '56', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('063', 'สำนักงานประกันภัยจังหวัดตาก', '63', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('065', 'สำนักงานประกันภัยจังหวัดพิษณุโลก', '65', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('058', 'สำนักงานประกันภัยจังหวัดแม่ฮ่องสอน', '58', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('060', 'สำนักงานประกันภัยจังหวัดนครสวรรค์', '60', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('061', 'สำนักงานประกันภัยจังหวัดอุทัยธานี', '61', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('052', 'สำนักงานประกันภัยจังหวัดลำปาง', '52', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('070', 'สำนักงานประกันภัยจังหวัดราชบุรี', '70', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('071', 'สำนักงานประกันภัยจังหวัดกาญจนบุรี', '71', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('072', 'สำนักงานประกันภัยจังหวัดสุพรรณบุรี', '72', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('073', 'สำนักงานประกันภัยจังหวัดนครปฐม', '73', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('074', 'สำนักงานประกันภัยจังหวัดสมุทรสาคร', '74', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('066', 'สำนักงานประกันภัยจังหวัดพิจิตร', '66', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('075', 'สำนักงานประกันภัยจังหวัดสมุทรสงคราม', '75', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('077', 'สำนักงานประกันภัยจังหวัดประจวบคีรีขันธ์', '77', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('080', 'สำนักงานประกันภัยจังหวัดนครศรีธรรมราช', '80', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('067', 'สำนักงานประกันภัยจังหวัดเพชรบูรณ์', '67', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('082', 'สำนักงานประกันภัยจังหวัดพังงา', '82', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('083', 'สำนักงานประกันภัยจังหวัดภูเก็ต', '83', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('084', 'สำนักงานประกันภัยจังหวัดสุราษฎร์ธานี', '84', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('086', 'สำนักงานประกันภัยจังหวัดชุมพร', '86', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('090', 'สำนักงานประกันภัยจังหวัดสงขลา', '90', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('094', 'สำนักงานประกันภัยจังหวัดปัตตานี', '94', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('095', 'สำนักงานประกันภัยจังหวัดยะลา', '95', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('096', 'สำนักงานประกันภัยจังหวัดนราธิวาส', '96', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('091', 'สำนักงานประกันภัยจังหวัดสตูล', '91', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('092', 'สำนักงานประกันภัยจังหวัดตรัง', '92', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('081', 'สำนักงานประกันภัยจังหวัดกระบี่', '81', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('014', 'สำนักงานประกันภัยจังหวัดพระนครศรีอยุธยา', '14', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('030', 'สำนักงานประกันภัยจังหวัดนครราชสีมา', '30', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('039', 'สำนักงานประกันภัยจังหวัดหนองบัวลำภู', '39', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('062', 'สำนักงานประกันภัยจังหวัดกำแพงเพชร', '62', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('064', 'สำนักงานประกันภัยจังหวัดสุโขทัย', '64', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('076', 'สำนักงานประกันภัยจังหวัดเพชรบุรี', '76', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('085', 'สำนักงานประกันภัยจังหวัดระนอง', '85', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('046', 'สำนักงานประกันภัยจังหวัดกาฬสินธุ์', '46', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('057', 'สำนักงานประกันภัยจังหวัดเชียงราย', '57', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('093', 'สำนักงานประกันภัยจังหวัดพัทลุง', '93', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('047', 'สำนักงานประกันภัยจังหวัดสกลนคร', '47', 'AGDOI', TO_DATE('2004-08-26 14:49:22', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('660', 'ธนาคารกรุงไทย', '10', 'AGDOI', TO_DATE('2008-08-21 10:32:39', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('671', 'ธนาคาร ซีไอเอ็มบี ไทย จำกัด(มหาชน)', '10', 'AGDOI', TO_DATE('2011-08-09 15:26:32', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('667', 'ธนาคารยูโอบี จำกัด (มหาชน)', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:07', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('661', 'ธนาคารกรุงศรีอยุทยา', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:08', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('669', 'ธนาคารนครหลวงไทย', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:08', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('675', 'ธนาคารธนชาต', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:09', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('666', 'ธนาคาร', '10', 'AGDOI', TO_DATE('2013-11-06 17:41:42', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('663', 'ธนาคารทหารไทย', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:10', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('664', 'ธนาคารกสิกรไทย', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:11', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('662', 'ธนาคารกรุงเทพ', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:11', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('670', 'ธนาคารออมสิน', '10', 'AGDOI', TO_DATE('2009-05-18 11:47:12', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('000', 'ศูนย์สอบประจำจังหวัด', '10', 'AGDOI', TO_DATE('2013-10-30 08:42:51', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('333', 'สมาคมประกันวนาศภัย(ต่างจังหวัด)', '10', 'AGDOI', TO_DATE('2013-11-06 17:44:10', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('555', 'สนามสอบนายหน้าประกันวินาศภัย', '10', 'AGDOI', TO_DATE('2013-11-06 17:44:34', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('777', 'คปภ.(คอมพิวเตอร์)', '10', 'AGDOI', TO_DATE('2013-11-06 17:44:52', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('444', 'สนามสอบนายหน้าประกันชีวิต', '10', 'AGDOI', TO_DATE('2013-11-06 17:52:02', 'YYYY-MM-DD HH24:MI:SS'));
INSERT INTO "AGDOI"."AG_ACCEPT_OFF_R" VALUES ('888', 'บริษัทโบรกเกอร์', '10', 'AGDOI', TO_DATE('2013-11-06 17:52:32', 'YYYY-MM-DD HH24:MI:SS'));

-- ----------------------------
-- Indexes structure for table AG_ACCEPT_OFF_R
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_ACCEPT_OFF_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_ACCEPT_OFF_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_ACCEPT_OFF_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID	:= GET_SYS_USER;
  	:NEW.USER_DATE 	:= SYSDATE;
END;

-- ----------------------------
-- Primary Key structure for table AG_ACCEPT_OFF_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_ACCEPT_OFF_R" ADD PRIMARY KEY ("ACCEPT_OFF_CODE");
