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

Date: 2014-04-08 09:23:06
*/


-- ----------------------------
-- Table structure for AG_IAS_LICENSE_H
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_LICENSE_H";
CREATE TABLE "AGDOI"."AG_IAS_LICENSE_H" (
"UPLOAD_GROUP_NO" VARCHAR2(15 BYTE) NOT NULL ,
"COMP_CODE" VARCHAR2(4 BYTE) NULL ,
"COMP_NAME" VARCHAR2(200 BYTE) NULL ,
"TRAN_DATE" DATE NULL ,
"LOTS" NUMBER NULL ,
"MONEY" NUMBER NULL ,
"REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"PAYMENT_NO" VARCHAR2(20 BYTE) NULL ,
"FLAG_REQ" VARCHAR2(1 BYTE) NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"FILENAME" VARCHAR2(300 BYTE) NULL ,
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"UPLOAD_BY_SESSION" VARCHAR2(15 BYTE) NULL ,
"FLAG_LIC" VARCHAR2(1 BYTE) NULL ,
"APPROVE_COMPCODE" VARCHAR2(4 BYTE) NULL ,
"APPROVED_DOC" VARCHAR2(1 BYTE) NULL ,
"APPROVED_DATE" DATE NULL ,
"APPROVED_BY" VARCHAR2(20 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;

-- ----------------------------
-- Records of AG_IAS_LICENSE_H
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131028165251174', '1007', 'อลิอันซ์ อยุธยา จำกัด (มหาชน)', TO_DATE('2013-10-28 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '16', '4800', null, null, null, '01', '131028165251180.rar', '11', '1007', null, '111', 'Y', TO_DATE('2013-10-28 17:07:36', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131029132814315', '1007', 'อลิอันซ์ อยุธยา', TO_DATE('2013-10-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '6', '1800', null, null, null, '01', '131029132814331.rar', '15', '1007', null, '111', 'Y', TO_DATE('2013-10-29 13:31:23', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131029170028607', '1007', 'อลิอันซ์ อยุธยา', TO_DATE('2013-10-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '01', '131029170028623.rar', '14', '1007', null, '111', 'Y', TO_DATE('2013-10-29 17:02:33', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131029184237793', '1001', 'บริษัท ไทยประกันชีวิต', TO_DATE('2013-10-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '01', '131029184237808.rar', '13', '1001', null, '111', 'Y', TO_DATE('2013-10-29 18:43:12', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131030170656167', '1001', 'ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-10-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '01', '131030170656182.rar', '11', '1001', null, '111', 'Y', TO_DATE('2013-10-30 17:07:27', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131029213528210', null, null, TO_DATE('2013-10-29 21:35:28', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131029212756195', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131029203259697', '1001', 'อลิอันซ์ อยุธยา จำกัด (มหาชน)', TO_DATE('2013-10-28 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '16', '4800', null, null, null, '01', '131029203302787.rar', '11', null, null, '111', 'Y', TO_DATE('2013-11-05 20:01:37', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131030140236506', '1001', 'ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-10-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '01', '131030140236522.rar', '11', '1001', null, '111', 'Y', TO_DATE('2013-10-30 14:03:23', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131030174110490', '1001', 'ไทยประกันชีวิต', TO_DATE('2013-10-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '200', null, null, null, '01', '131030174110506.rar', '13', '1001', null, '111', 'Y', TO_DATE('2013-10-30 17:41:58', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131101174526388', null, null, TO_DATE('2013-11-01 17:45:26', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131104180337113', null, null, TO_DATE('2013-11-04 18:03:37', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131104174346789', null, '111', 'N', TO_DATE('2013-11-11 20:08:21', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131105090326465', '1001', 'ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-10-29 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '100', '30000', null, null, null, '01', 'Temp\131105090319097\ขอใหม่100.csv', '11', '1001', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106102310442', null, null, TO_DATE('2013-11-06 10:23:10', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 10:47:42', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131105184935858', null, null, TO_DATE('2013-11-05 18:49:35', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131105184954485', null, null, TO_DATE('2013-11-05 18:49:54', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106102313047', null, null, TO_DATE('2013-11-06 10:23:13', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:44:24', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106103017372', null, null, TO_DATE('2013-11-06 10:30:17', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:44:59', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106103127308', null, null, TO_DATE('2013-11-06 10:31:27', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:45:05', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106103837732', null, null, TO_DATE('2013-11-06 10:38:37', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:45:13', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106104121237', null, null, TO_DATE('2013-11-06 10:41:21', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:45:18', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106104532993', null, null, TO_DATE('2013-11-06 10:45:32', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:45:24', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106113730842', null, null, TO_DATE('2013-11-06 11:37:30', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:45:30', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106114102458', null, null, TO_DATE('2013-11-06 11:41:02', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-06 11:45:35', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106115605568', null, null, TO_DATE('2013-11-06 11:56:05', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-20 18:10:13', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131106184648656', '1001', 'ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-11-06 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '600', null, null, null, '01', 'Temp\131106184648793\ขอใหม่ - li01_1001_22.csv', '11', '1001', null, '111', 'Y', TO_DATE('2013-11-06 18:52:17', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131107103015360', '2080', 'บริษัท อเมริกันอินเตอร์แนชชั่นแนลแอสชัวรันส์ จำกัด (ประกันวินาศภัย)สาขาประเทศไทย', TO_DATE('2013-11-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '400', null, null, null, '02', 'Temp\131107103015496\LicenseReceive_ต่ออายุ1ปี.csv', '14', '2080', null, '111', 'Y', TO_DATE('2013-11-07 13:41:29', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131107165654661', null, null, TO_DATE('2013-11-07 16:56:54', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131107131349452', null, '111', 'Y', TO_DATE('2013-11-07 17:13:57', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131107170259674', null, null, TO_DATE('2013-11-07 17:02:59', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131107131349452', null, '111', 'Y', TO_DATE('2013-11-07 17:14:06', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131108114622364', null, null, TO_DATE('2013-11-08 11:46:22', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131107131349452', null, '111', 'Y', TO_DATE('2013-11-08 11:48:57', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131108114653237', null, null, TO_DATE('2013-11-08 11:46:53', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131107131349452', null, '111', 'Y', TO_DATE('2013-11-11 13:40:10', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131111160032390', null, null, TO_DATE('2013-11-11 16:00:32', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131111155408267', null, '111', 'Y', TO_DATE('2013-11-11 16:01:42', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131111164805784', null, null, TO_DATE('2013-11-11 16:48:05', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131111155408267', null, '111', 'Y', TO_DATE('2013-11-11 16:51:03', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131111182923429', null, null, TO_DATE('2013-11-11 18:29:23', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131111182925738', null, null, TO_DATE('2013-11-11 18:29:25', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131101111615778', null, '111', 'Y', TO_DATE('2013-11-11 18:31:41', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131111202746435', null, null, TO_DATE('2013-11-11 20:27:46', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131104135708591', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113105548465', null, null, TO_DATE('2013-11-13 10:55:48', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131113103807994', null, '111', 'Y', TO_DATE('2013-11-13 13:52:02', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113105836573', null, null, TO_DATE('2013-11-13 10:58:36', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131113103807994', null, '111', 'Y', TO_DATE('2013-11-13 13:51:57', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113105919198', null, null, TO_DATE('2013-11-13 10:59:19', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131113103807994', null, '111', 'Y', TO_DATE('2013-11-13 13:51:51', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113110506193', null, null, TO_DATE('2013-11-13 11:05:06', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '04', null, '11', '131113103807994', null, '111', 'Y', TO_DATE('2013-11-13 13:51:44', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113114603257', null, null, TO_DATE('2013-11-13 11:46:03', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113113608064', null, '111', 'Y', TO_DATE('2013-11-13 11:47:39', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113134216079', null, null, null, null, '300', null, null, null, '03', null, '15', null, null, null, 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113144136521', null, null, null, null, '300', null, null, null, '03', null, '15', null, null, null, 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113152908588', '2080', 'บริษัท อเมริกันอินเตอร์แนชชั่นแนลแอสชัวรันส์ จำกัด (ประกันวินาศภัย)สาขาประเทศไทย', TO_DATE('2013-11-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '20000', null, null, null, '02', 'Temp\131113152908668\LicenseReceive_ต่ออายุ1ปี.csv', '11', '2080', null, '111', 'Y', TO_DATE('2013-11-13 15:38:00', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113153430264', '2080', 'บริษัท อเมริกันอินเตอร์แนชชั่นแนลแอสชัวรันส์ จำกัด (ประกันวินาศภัย)สาขาประเทศไทย', TO_DATE('2013-11-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '20000', null, null, null, '02', 'Temp\131113153430344\LicenseReceive_ต่ออายุ1ปี.csv', '11', '2080', null, '111', 'Y', TO_DATE('2013-11-13 15:38:12', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113160736359', null, null, TO_DATE('2013-11-13 16:07:36', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113161049013', null, null, TO_DATE('2013-11-13 16:10:49', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113162237054', null, null, TO_DATE('2013-11-13 16:22:37', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113162706603', null, null, TO_DATE('2013-11-13 16:27:06', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113163027018', null, null, TO_DATE('2013-11-13 16:30:27', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113163440446', null, null, TO_DATE('2013-11-13 16:34:40', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113172036415', null, null, TO_DATE('2013-11-13 17:20:36', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113194253510', null, null, TO_DATE('2013-11-13 19:42:53', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114102026923', null, null, TO_DATE('2013-11-14 10:20:26', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113095411348', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131113205937178', null, null, TO_DATE('2013-11-13 20:59:37', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131104135708591', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114112138320', null, null, null, null, '300', null, null, null, '03', null, '15', '131113193129724', null, null, 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114110619735', null, null, TO_DATE('2013-11-14 11:06:19', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113114859461', null, '111', 'Y', TO_DATE('2013-11-14 11:07:45', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114112533570', null, null, null, null, '300', null, null, null, '04', null, '15', '131113185650394', null, null, 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114140757201', null, null, TO_DATE('2013-11-14 14:07:57', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113114859461', null, '111', 'Y', TO_DATE('2013-11-14 14:11:34', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114145640224', null, null, null, null, '200', null, null, null, '01', null, '14', '131030135430624', null, null, 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114151951971', '2080', 'บริษัท อเมริกันอินเตอร์แนชชั่นแนลแอสชัวรันส์ จำกัด (ประกันวินาศภัย)สาขาประเทศไทย', TO_DATE('2013-11-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '20000', null, null, null, '02', 'Temp\131114151952082\LicenseReceive_ต่ออายุ1ปี.csv', '14', '2080', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114153339591', '2080', 'บริษัท อเมริกันอินเตอร์แนชชั่นแนลแอสชัวรันส์ จำกัด (ประกันวินาศภัย)สาขาประเทศไทย', TO_DATE('2013-11-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3', '2400', null, null, null, '02', 'Temp\131114153339687\LicenseReceive_ต่ออายุ1ปี.csv', '14', '2080', null, '111', 'Y', TO_DATE('2013-11-14 15:36:27', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114154445422', '2080', 'บริษัท อเมริกันอินเตอร์แนชชั่นแนลแอสชัวรันส์ จำกัด (ประกันวินาศภัย)สาขาประเทศไทย', TO_DATE('2013-11-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '400', null, null, null, '02', 'Temp\131114154445470\LicenseReceive_ต่ออายุ1ปี.csv', '13', '2080', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114155522767', '1001', 'ไทยประกันชีวิต', TO_DATE('2013-11-14 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '400', null, null, null, '01', 'Temp\131114155522846\LicenseReceive_ต่ออายุ1ปี.csv', '13', '1001', null, '111', 'Y', TO_DATE('2013-11-14 15:57:23', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114170113043', null, null, TO_DATE('2013-11-14 17:01:13', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113115500698', null, '111', 'Y', TO_DATE('2013-11-14 17:02:52', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114185956785', null, null, TO_DATE('2013-11-14 18:59:56', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113160431523', null, '111', 'Y', TO_DATE('2013-11-14 19:35:23', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131114192924100', null, null, TO_DATE('2013-11-14 19:29:24', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113184654639', null, '111', 'Y', TO_DATE('2013-11-14 19:35:54', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115155353844', null, null, TO_DATE('2013-11-15 15:53:53', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115155922508', null, null, TO_DATE('2013-11-15 15:59:22', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115160239133', null, null, TO_DATE('2013-11-15 16:02:39', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115160846876', null, null, null, null, '300', null, null, null, '03', null, '15', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115173824191', null, null, TO_DATE('2013-11-15 17:38:24', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113160431523', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115174109917', null, null, TO_DATE('2013-11-15 17:41:09', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113160431523', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131115182023643', null, null, TO_DATE('2013-11-15 18:20:23', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113160431523', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131118152850086', null, null, null, null, '800', null, null, null, '03', null, '14', '131118150502125', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131118165746404', null, null, null, null, '200', null, null, null, '04', null, '16', '131118150502125', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131118170128598', null, null, null, null, '200', null, null, null, '04', null, '16', '131118150502125', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131118172503735', null, null, null, null, '800', null, null, null, '03', null, '14', '131118160259902', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131118173309072', null, null, null, null, '800', null, null, null, '03', null, '14', '131118160259902', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131120160347114', '3139', 'ไทยพาณิชย์ประกันชีวิต', TO_DATE('2013-11-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '2', '600', null, null, null, '03', 'Temp\131120160346518\Buk.csv', '11', '3139', null, '111', 'Y', TO_DATE('2013-11-27 11:46:23', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131121105851721', null, null, TO_DATE('2013-11-21 10:58:51', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122171359461', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122172545306', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122172913771', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122173110008', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122174110240', null, null, TO_DATE('2013-11-22 17:41:10', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122174154653', null, null, null, null, '300', null, null, null, '04', null, '15', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122174322685', null, null, TO_DATE('2013-11-22 17:43:22', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122174502807', null, null, null, null, '300', null, null, null, '04', null, '15', '131113185650394', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122180544580', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131122182103648', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125100427409', null, null, null, null, '800', null, null, null, '01', null, '14', '131030135430624', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125100603539', null, null, TO_DATE('2013-11-25 10:06:04', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131030135430624', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125103748194', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125103916382', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125104008143', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125105439100', null, null, null, null, '800', null, null, null, '01', null, '14', '131030135430624', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125105056455', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125105729033', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125110831759', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125111029821', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125111412405', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125112321437', null, null, null, null, '300', null, null, null, '03', null, '15', '131101111615778', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125112428018', null, null, null, null, '200', null, null, null, '03', null, '16', '131101111615778', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125114651630', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125143324597', null, null, TO_DATE('2013-11-25 14:33:24', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125165431370', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131125165632006', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131126085026881', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131126093602043', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131126093630295', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131126105101475', null, null, TO_DATE('2013-11-26 10:51:01', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131126094543711', null, '111', 'Y', TO_DATE('2013-11-26 11:44:37', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131126140919004', null, null, TO_DATE('2013-11-26 14:09:19', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131126135520121', null, '111', 'Y', TO_DATE('2013-11-26 14:12:45', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127090926579', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127105332699', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127115144032', null, null, TO_DATE('2013-11-27 11:51:44', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131127090145267', null, '111', 'Y', TO_DATE('2013-11-27 11:55:29', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127121835932', null, null, null, null, '300', null, null, null, '03', null, '15', '131101111615778', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127140105110', '1001', 'บริษัท ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-11-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '10', '3000', null, null, null, '01', 'Temp\131127140104647\LisenceReqImp-1001131126.csv', '11', '1001', null, '111', 'N', TO_DATE('2013-11-28 12:15:46', 'YYYY-MM-DD HH24:MI:SS'), '131029131649499');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127145737714', null, null, null, null, '200', null, null, null, '04', null, '16', '131108164232732', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127153249249', null, null, TO_DATE('2013-11-27 15:32:49', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131111155408267', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127154012981', null, null, TO_DATE('2013-11-27 15:40:12', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131111155408267', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127155210068', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127160139256', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131127163521069', '1006', 'บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-11-27 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '5', '1000', null, null, null, '01', 'Temp\131127163518955\ต่ออายุ1ปี_1001.csv', '13', '1006', null, '111', 'Y', TO_DATE('2013-11-28 16:56:33', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128090231284', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128091556455', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128091702693', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128091749213', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128092133778', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128092232419', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128092504801', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128092532834', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128113721168', null, null, null, null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128113746409', null, null, null, null, '200', null, null, null, '04', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128113728956', null, null, null, null, '200', null, null, null, '02', null, '16', '131101111615778', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128114052175', null, null, null, null, '200', null, null, null, '03', null, '16', '131101111615778', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128180337523', null, null, TO_DATE('2013-11-28 18:03:37', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113160431523', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128180724997', null, null, TO_DATE('2013-11-28 18:07:24', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113160431523', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128180911843', null, null, TO_DATE('2013-11-28 18:09:11', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131113142353276', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131128182255192', '1006', 'บริษัท กรุงเทพประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-10-21 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '10', '3000', null, null, null, '01', 'Temp\131128182254308\LisenceReqImp-100620131021.csv', '11', '1006', null, '111', 'Y', TO_DATE('2013-11-28 18:25:14', 'YYYY-MM-DD HH24:MI:SS'), '131029131649499');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131129091509739', '3139', 'ไทยพาณิชย์ประกันชีวิต', TO_DATE('2013-11-05 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '16', '4800', null, null, null, '03', 'Temp\131129091509261\ขอใหม่_234.csv', '11', '3139', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131129110259852', null, null, null, null, '800', null, null, null, '03', null, '14', '131118160259902', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131129113922143', null, null, null, null, '800', null, null, null, '01', null, '14', '131129113728793', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131129120041414', '3139', 'บริษัท ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-12-20 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '10', '3000', null, null, null, '03', 'Temp\131129120041201\LisenceReqI.csv', '11', '3139', null, '111', 'Y', TO_DATE('2013-11-29 12:13:28', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131129150636178', null, null, TO_DATE('2013-11-29 15:06:36', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131129121659004', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131129161145674', null, null, TO_DATE('2013-11-29 16:11:45', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131202170125071', '3139', 'บริษัท ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-12-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '10', '3000', null, null, null, '03', 'Temp\131202170125018\LisenceReqI.csv', '11', '3139', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131206160406274', null, null, TO_DATE('2013-12-06 16:04:06', 'YYYY-MM-DD HH24:MI:SS'), null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131206163755434', null, null, TO_DATE('2013-12-06 16:37:55', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131206180431756', null, null, TO_DATE('2013-12-06 18:04:31', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131206180545561', null, null, TO_DATE('2013-12-06 18:05:45', 'YYYY-MM-DD HH24:MI:SS'), null, '200', null, null, null, '03', null, '16', '131113141440765', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131209151402856', null, null, TO_DATE('2013-12-09 15:14:02', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131209154919066', null, null, TO_DATE('2013-12-09 15:49:19', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131028103319243', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131211133346106', '1001', 'ไทยประกันชีวิต', TO_DATE('2013-12-11 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '5', '1000', null, null, null, '01', 'Temp\131211133346130\ต่อ1_01_1.csv', '13', '1001', null, '111', 'Y', TO_DATE('2013-12-11 13:38:34', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131211161447416', '1001', 'บริษัท ไทยประกันชีวิต จำกัด (มหาชน)', TO_DATE('2013-11-26 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '10', '2000', null, null, null, '01', 'Temp\131211161446393\LisenceReqImp-1001131126.csv', '13', '1001', null, '111', 'N', TO_DATE('2013-12-11 16:22:22', 'YYYY-MM-DD HH24:MI:SS'), '131029131649499');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131211164640160', '1001', 'ไทยประกันชีวิต', TO_DATE('2013-12-11 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '5', '1000', null, null, null, '01', 'Temp\131211164640206\ต่อ1_01_1.csv', '13', '1001', null, '111', 'Y', TO_DATE('2013-12-11 16:47:23', 'YYYY-MM-DD HH24:MI:SS'), '131028165823028');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131212102544382', null, null, TO_DATE('2013-12-12 10:25:44', 'YYYY-MM-DD HH24:MI:SS'), null, '300', null, null, null, '03', null, '15', '131118160731173', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131212103502150', null, null, TO_DATE('2013-12-12 10:35:02', 'YYYY-MM-DD HH24:MI:SS'), null, '800', null, null, null, '01', null, '14', '131203133710769', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131212103936027', null, null, TO_DATE('2013-12-12 10:39:36', 'YYYY-MM-DD HH24:MI:SS'), null, '800', null, null, null, '01', null, '14', '131203133710769', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131217101511558', null, null, TO_DATE('2013-12-17 10:15:11', 'YYYY-MM-DD HH24:MI:SS'), null, '300', null, null, null, '03', null, '15', '131118150502125', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131217114316126', null, null, TO_DATE('2013-12-17 11:43:16', 'YYYY-MM-DD HH24:MI:SS'), null, '300', null, null, null, '04', null, '15', '131108164232732', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131217121221399', null, null, TO_DATE('2013-12-17 12:12:21', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '15', '131108164232732', null, '111', 'Y', TO_DATE('2013-12-17 12:13:10', 'YYYY-MM-DD HH24:MI:SS'), '131029131649499');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131219110753003', null, null, TO_DATE('2013-12-19 11:07:53', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '15', '131108164232732', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220094422523', '3139', 'ธนาคารไทยพาณิชย์  จำกัด', TO_DATE('2013-12-18 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '4', '1200', null, null, null, '03', 'Temp\131220094422506\LicenseReceive_ต่ออายุ1ปี.csv', '11', '3139', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220094802533', '3139', 'ธนาคารไทยพาณิชย์  จำกัด', TO_DATE('2013-12-20 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '4', '1200', null, null, null, '03', 'Temp\131220094802625\LicenseReceive_ต่ออายุ1ปี.csv', '11', '3139', null, '111', 'Y', TO_DATE('2013-12-20 13:51:34', 'YYYY-MM-DD HH24:MI:SS'), '131029131649499');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220100827785', '3139', 'ธนาคารไทยพาณิชย์  จำกัด', TO_DATE('2013-12-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '3', '900', null, null, null, '04', 'Temp\131220100827893\LicenseReceive_ต่ออายุ1ปี.csv', '11', '3139', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220100848627', '3139', 'ธนาคารไทยพาณิชย์  จำกัด', TO_DATE('2013-12-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '5', '1500', null, null, null, '04', 'Temp\131220100848719\LicenseReceive_ต่ออายุ1ปี.csv', '11', '3139', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220100908143', '3139', 'ธนาคารไทยพาณิชย์  จำกัด', TO_DATE('2013-12-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '5', '1500', null, null, null, '04', 'Temp\131220100908235\LicenseReceive_ต่ออายุ1ปี.csv', '11', '3139', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220134825995', null, null, TO_DATE('2013-12-17 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '6', '1800', null, null, null, '04', 'Temp\131220134825556\ขอใหม่04-131219.csv', '11', '111', null, '111', 'Y', TO_DATE('2013-12-20 14:07:43', 'YYYY-MM-DD HH24:MI:SS'), '131029131649499');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131220173812627', '3139', 'ธนาคารไทยพานิชย์', TO_DATE('2013-12-12 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '5', '1000', null, null, null, '03', 'Temp\131220173812609\license_renew_03.csv', '13', null, null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131226144326187', null, null, TO_DATE('2013-12-26 14:43:26', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131127090145267', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131226165408407', null, null, TO_DATE('2013-12-26 16:54:08', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131226163955733', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131227100141218', null, null, TO_DATE('2013-12-27 10:01:41', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131227084337128', null, '111', 'Y', TO_DATE('2013-12-27 10:05:02', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131227161609250', null, null, TO_DATE('2013-12-27 16:16:09', 'YYYY-MM-DD HH24:MI:SS'), '1', '200', null, null, null, '03', null, '13', '131227160820886', null, '111', 'Y', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('131227172626147', null, null, TO_DATE('2013-12-27 17:26:26', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '131227164204509', null, '111', 'Y', TO_DATE('2013-12-27 17:34:32', 'YYYY-MM-DD HH24:MI:SS'), '130923093821787');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140106144159820', null, null, TO_DATE('2014-01-06 14:41:59', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140106134515863', null, '111', 'Y', TO_DATE('2014-01-06 15:16:20', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140108140640077', null, null, TO_DATE('2014-01-08 14:06:40', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140108112255903', null, '111', 'Y', TO_DATE('2014-01-08 14:10:13', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140110125301479', null, null, TO_DATE('2014-01-10 12:53:01', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140109090756075', null, '111', 'Y', TO_DATE('2014-01-10 13:54:21', 'YYYY-MM-DD HH24:MI:SS'), '140110133833224');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140110140100954', null, null, TO_DATE('2014-01-10 14:01:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140109090756075', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140113100220926', null, null, TO_DATE('2014-01-13 10:02:20', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '15', '140113094649674', null, '111', 'Y', TO_DATE('2014-01-13 10:10:26', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140128145054118', null, null, TO_DATE('2014-01-28 14:50:54', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140128143353275', null, '111', 'Y', TO_DATE('2014-01-28 14:57:50', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140128164433716', null, null, TO_DATE('2014-01-28 16:44:33', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140128143353275', null, '111', 'Y', TO_DATE('2014-01-28 16:47:18', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140129103540476', null, null, TO_DATE('2014-01-29 10:35:40', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '03', null, '11', '140129102127849', null, '111', 'Y', TO_DATE('2014-01-29 10:39:45', 'YYYY-MM-DD HH24:MI:SS'), '131119183647391');
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140318153114451', '3139', 'บริษัท ไทยพาณิชย์ประกันภัย จำกัด (มหาชน)', TO_DATE('2013-09-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '01', 'Temp\140318153113497\TEST.csv', '11', '1001', null, '111', 'W', null, null);
INSERT INTO "AGDOI"."AG_IAS_LICENSE_H" VALUES ('140402132101896', '2060', 'hhh', TO_DATE('2013-09-25 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), '1', '300', null, null, null, '05', 'Temp\140402132057365\TEST.csv', '11', '2060', null, '111', 'W', null, null);

-- ----------------------------
-- Indexes structure for table AG_IAS_LICENSE_H
-- ----------------------------

-- ----------------------------
-- Checks structure for table AG_IAS_LICENSE_H
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_H" ADD CHECK ("UPLOAD_GROUP_NO" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_LICENSE_H
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_LICENSE_H" ADD PRIMARY KEY ("UPLOAD_GROUP_NO");
