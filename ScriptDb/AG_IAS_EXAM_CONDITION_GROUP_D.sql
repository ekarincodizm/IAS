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

Date: 2014-04-08 09:07:27
*/


-- ----------------------------
-- Table structure for AG_IAS_EXAM_CONDITION_GROUP_D
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D";
CREATE TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" (
"ID" NUMBER NOT NULL ,
"COURSE_NUMBER" NUMBER NULL ,
"SUBJECT_CODE" VARCHAR2(3 BYTE) NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"USER_ID" VARCHAR2(20 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"MAX_SCORE" NUMBER(3) NULL ,
"GROUP_ID" NUMBER NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D"."ID" IS 'รหัส ลำดับ';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D"."COURSE_NUMBER" IS 'รหัสหลักสูตร';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D"."SUBJECT_CODE" IS 'รหัสวิชา';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D"."LICENSE_TYPE_CODE" IS 'ประเภทใบอนุญาต';

-- ----------------------------
-- Records of AG_IAS_EXAM_CONDITION_GROUP_D
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('702', '57001', '011', '01', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('703', '57001', '001', '01', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('701', '57001', '010', '01', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('704', '57001', '009', '01', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('705', '57001', '111', '01', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), '10', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('706', '57001', '122', '01', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), '10', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('707', '57002', '001', '02', 'AGDOI', TO_DATE('2014-03-31 16:08:41', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('708', '57002', '009', '02', 'AGDOI', TO_DATE('2014-03-31 16:08:41', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('709', '57002', '011', '02', 'AGDOI', TO_DATE('2014-03-31 16:08:41', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('710', '57002', '010', '02', 'AGDOI', TO_DATE('2014-03-31 16:08:41', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('711', '57003', '009', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '100', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('712', '57003', '010', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('713', '57003', '012', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('714', '57003', '011', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('715', '57003', '022', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('716', '57003', '023', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('717', '57003', '033', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('718', '57003', '001', '03', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('719', '57004', '009', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '28', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('720', '57004', '013', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '28', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('721', '57004', '012', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('722', '57004', '014', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('723', '57004', '015', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('724', '57004', '010', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '28', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('725', '57004', '011', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '28', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('726', '57004', '022', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('727', '57004', '016', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('728', '57004', '001', '04', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('729', '57005', '009', '05', 'AGDOI', TO_DATE('2014-03-31 16:09:37', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('730', '57005', '011', '05', 'AGDOI', TO_DATE('2014-03-31 16:09:37', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('731', '57005', '010', '05', 'AGDOI', TO_DATE('2014-03-31 16:09:37', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('732', '57005', '001', '05', 'AGDOI', TO_DATE('2014-03-31 16:09:37', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('733', '57006', '010', '01', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('734', '57006', '011', '01', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('735', '57006', '001', '01', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('736', '57006', '009', '01', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('737', '57006', '111', '01', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), '10', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('738', '57006', '122', '01', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), '10', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('739', '57007', '001', '02', '130923093821787', TO_DATE('2014-04-01 18:22:09', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('740', '57007', '009', '02', '130923093821787', TO_DATE('2014-04-01 18:22:09', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('741', '57007', '011', '02', '130923093821787', TO_DATE('2014-04-01 18:22:09', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('742', '57007', '010', '02', '130923093821787', TO_DATE('2014-04-01 18:22:09', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('743', '57008', '010', '06', '130923093821787', TO_DATE('2014-04-02 11:31:43', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('744', '57008', '009', '06', '130923093821787', TO_DATE('2014-04-02 11:31:43', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('745', '57008', '001', '06', '130923093821787', TO_DATE('2014-04-02 11:31:43', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('746', '57008', '011', '06', '130923093821787', TO_DATE('2014-04-02 11:31:43', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('747', '57009', '009', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '100', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('748', '57009', '010', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('749', '57009', '012', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('750', '57009', '011', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('751', '57009', '022', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('752', '57009', '023', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('753', '57009', '033', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('754', '57009', '001', '03', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('761', '57010', '009', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '100', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('762', '57010', '010', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('763', '57010', '012', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('764', '57010', '011', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('765', '57010', '022', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('766', '57010', '023', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('767', '57010', '033', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '50', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('768', '57010', '001', '03', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('769', '57011', '009', '05', '130923093821787', TO_DATE('2014-04-04 13:49:47', 'YYYY-MM-DD HH24:MI:SS'), '40', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('770', '57011', '011', '05', '130923093821787', TO_DATE('2014-04-04 13:49:47', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('771', '57011', '010', '05', '130923093821787', TO_DATE('2014-04-04 13:49:47', 'YYYY-MM-DD HH24:MI:SS'), '20', '2');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('772', '57011', '001', '05', '130923093821787', TO_DATE('2014-04-04 13:49:47', 'YYYY-MM-DD HH24:MI:SS'), '20', '1');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" VALUES ('773', '57011', '999', '05', '130923093821787', TO_DATE('2014-04-04 13:49:47', 'YYYY-MM-DD HH24:MI:SS'), '23', '2');

-- ----------------------------
-- Indexes structure for table AG_IAS_EXAM_CONDITION_GROUP_D
-- ----------------------------
create sequence IAS_CONGROUP_R;
-- ----------------------------
-- Triggers structure for table AG_IAS_EXAM_CONDITION_GROUP_D
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."IAS_CONGROUP_R" BEFORE INSERT ON "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
begin
      select IAS_CONGROUP_R.nextval
        into :new.id
      from dual;
    end;
-- ----------------------------
-- Checks structure for table AG_IAS_EXAM_CONDITION_GROUP_D
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_EXAM_CONDITION_GROUP_D
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP_D" ADD PRIMARY KEY ("ID");
