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

Date: 2014-04-08 09:07:08
*/


-- ----------------------------
-- Table structure for AG_IAS_EXAM_CONDITION_GROUP
-- ----------------------------
DROP TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP";
CREATE TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" (
"ID" NUMBER NOT NULL ,
"COURSE_NUMBER" NUMBER NULL ,
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"START_DATE" DATE NULL ,
"END_DATE" DATE NULL ,
"STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(100 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"NOTE" VARCHAR2(500 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."ID" IS 'รหัส ลำดับ';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."COURSE_NUMBER" IS 'รหัสหลักสูตร';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."LICENSE_TYPE_CODE" IS 'รหัสประเภทใบอนุญาต';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."START_DATE" IS 'วันที่มีผลบังคับใช้';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."END_DATE" IS 'สิ้นสุดวันที่มีผลบังคับใช้';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."STATUS" IS 'สถานะ A = ใช้งาน , I = รอใช้งาน , E ไม่ใช้งาน';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."USER_ID" IS 'ผู้บันทึก';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."USER_DATE" IS 'วันที่บันทึก';
COMMENT ON COLUMN "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP"."NOTE" IS 'หมายเหตุ';

-- ----------------------------
-- Records of AG_IAS_EXAM_CONDITION_GROUP
-- ----------------------------
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('182', '57002', '02', TO_DATE('2014-03-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'A', 'AGDOI', TO_DATE('2014-03-31 16:08:41', 'YYYY-MM-DD HH24:MI:SS'), null);
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('183', '57003', '03', TO_DATE('2014-03-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'A', 'AGDOI', TO_DATE('2014-03-31 16:09:01', 'YYYY-MM-DD HH24:MI:SS'), null);
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('181', '57001', '01', TO_DATE('2014-03-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'E', 'AGDOI', TO_DATE('2014-03-31 16:08:20', 'YYYY-MM-DD HH24:MI:SS'), null);
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('184', '57004', '04', TO_DATE('2014-03-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'A', 'AGDOI', TO_DATE('2014-03-31 16:09:20', 'YYYY-MM-DD HH24:MI:SS'), null);
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('185', '57005', '05', TO_DATE('2014-03-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'A', 'AGDOI', TO_DATE('2014-03-31 16:09:37', 'YYYY-MM-DD HH24:MI:SS'), null);
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('186', '57006', '01', TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-04-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'A', '130923093821787', TO_DATE('2014-04-01 11:22:11', 'YYYY-MM-DD HH24:MI:SS'), null);
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('187', '57007', '02', TO_DATE('2014-03-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-04-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'E', '130923093821787', TO_DATE('2014-04-01 18:22:09', 'YYYY-MM-DD HH24:MI:SS'), 'Test');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('188', '57008', '06', TO_DATE('2014-04-01 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-04-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'I', '130923093821787', TO_DATE('2014-04-02 11:31:43', 'YYYY-MM-DD HH24:MI:SS'), 'Test');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('189', '57009', '03', TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-04-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'I', '130923093821787', TO_DATE('2014-04-02 11:33:15', 'YYYY-MM-DD HH24:MI:SS'), 'Test');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('201', '57010', '03', TO_DATE('2014-04-04 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-04-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'I', '130923093821787', TO_DATE('2014-04-04 13:34:57', 'YYYY-MM-DD HH24:MI:SS'), 'test');
INSERT INTO "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" VALUES ('202', '57011', '05', TO_DATE('2014-03-31 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_DATE('2014-04-30 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), 'I', '130923093821787', TO_DATE('2014-04-04 13:49:47', 'YYYY-MM-DD HH24:MI:SS'), 'test');

-- ----------------------------
-- Indexes structure for table AG_IAS_EXAM_CONDITION_GROUP
-- ----------------------------
create sequence IAS_CON_GROUP;
-- ----------------------------
-- Triggers structure for table AG_IAS_EXAM_CONDITION_GROUP
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."IAS_CON_GROUP" BEFORE INSERT ON "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
begin
      select IAS_CON_GROUP.nextval
        into :new.id
      from dual;
    end;
-- ----------------------------
-- Checks structure for table AG_IAS_EXAM_CONDITION_GROUP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" ADD CHECK ("ID" IS NOT NULL);

-- ----------------------------
-- Primary Key structure for table AG_IAS_EXAM_CONDITION_GROUP
-- ----------------------------
ALTER TABLE "AGDOI"."AG_IAS_EXAM_CONDITION_GROUP" ADD PRIMARY KEY ("ID");
