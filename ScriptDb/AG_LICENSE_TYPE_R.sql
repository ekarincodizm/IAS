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

Date: 2014-04-09 14:43:25
*/


-- ----------------------------
-- Table structure for AG_LICENSE_TYPE_R
-- ----------------------------

CREATE TABLE "AGDOI"."AG_LICENSE_TYPE_R" (
"LICENSE_TYPE_CODE" VARCHAR2(2 BYTE) NOT NULL ,
"LICENSE_TYPE_NAME" VARCHAR2(100 BYTE) NULL ,
"AGENT_TYPE" VARCHAR2(1 BYTE) NULL ,
"INSURANCE_TYPE" VARCHAR2(1 BYTE) NULL ,
"JURIS_PERSON_TYPE" VARCHAR2(1 BYTE) NULL ,
"RENEWAL_TIMES" NUMBER(2) NULL ,
"TIME_PERIOD" NUMBER(4,2) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"IAS_FLAG" VARCHAR2(1 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_LICENSE_TYPE_R" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."LICENSE_TYPE_CODE" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."LICENSE_TYPE_NAME" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."AGENT_TYPE" IS '????????????/??????? , A = ??????,B = ???????,J = ????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."INSURANCE_TYPE" IS '??????????????????, 1 = ?????,2 = ????????, 3=?????/????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."JURIS_PERSON_TYPE" IS '??????????????????????, 1 = ???, 2= ??? , 3=???/???';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."RENEWAL_TIMES" IS '???????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."TIME_PERIOD" IS '????????????????????????????(??)';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_TYPE_R"."USER_DATE" IS '????????????';

-- ----------------------------
-- Indexes structure for table AG_LICENSE_TYPE_R
-- ----------------------------

-- ----------------------------
-- Triggers structure for table AG_LICENSE_TYPE_R
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_DLG_AG_TYPE_TRG" AFTER INSERT OR DELETE OR UPDATE ON "AGDOI"."AG_LICENSE_TYPE_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  IF SUBSTR(:NEW.LICENSE_TYPE_CODE,1,1) <> '3' THEN
    IF INSERTING THEN
            BEGIN
                INSERT  INTO  DLG_AG_TYPE  (AGENT_TYPE
                    ,AGENT_CAT
                    , INSURE_CAT
      ,DESCRIPTION )
                            VALUES (:NEW.LICENSE_TYPE_CODE
                      ,:NEW.AGENT_TYPE
                      ,:NEW.INSURANCE_TYPE
                      ,SUBSTR(:NEW.LICENSE_TYPE_NAME,1,40)) ;
              EXCEPTION
                   WHEN OTHERS THEN NULL;
         END;
    ELSIF UPDATING  THEN
           BEGIN
             UPDATE  DLG_AG_TYPE
                    SET  AGENT_TYPE   = :NEW.LICENSE_TYPE_CODE,
                            AGENT_CAT      = :NEW.AGENT_TYPE,
                            INSURE_CAT     = :NEW.INSURANCE_TYPE,
                             DESCRIPTION  = SUBSTR(:NEW.LICENSE_TYPE_NAME,1,40)
              WHERE AGENT_TYPE  = :OLD.LICENSE_TYPE_CODE ;
             EXCEPTION
              WHEN OTHERS THEN NULL;
           END;
    END IF;
  END IF;

  IF SUBSTR(:OLD.LICENSE_TYPE_CODE,1,1) <> '3' THEN
    IF DELETING  THEN
          BEGIN
   DELETE DLG_AG_TYPE
   WHERE AGENT_TYPE  = :OLD.LICENSE_TYPE_CODE  ;
           EXCEPTION
           WHEN OTHERS THEN NULL;
           END;
    END IF;
  END IF;
END;CREATE OR REPLACE TRIGGER "AGDOI"."AG_DLG_COMP_CATE_TRG" AFTER INSERT OR DELETE OR UPDATE ON "AGDOI"."AG_LICENSE_TYPE_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  IF SUBSTR(:NEW.LICENSE_TYPE_CODE,1,1) = '3' THEN
    IF INSERTING THEN
            BEGIN
                INSERT  INTO  DLG_COMP_CATE  (COMP_CATE, DESCRIPTION,
       INSURE_CAT, SERV_CAT )
                          VALUES (SUBSTR(:NEW.LICENSE_TYPE_CODE,2,1) ,    SUBSTR(:NEW.LICENSE_TYPE_NAME,1,40),
                             :NEW.INSURANCE_TYPE , :NEW.JURIS_PERSON_TYPE) ;
              EXCEPTION
                   WHEN OTHERS THEN NULL;
         END;
    ELSIF UPDATING  THEN
           BEGIN
             UPDATE  DLG_COMP_CATE
                    SET  COMP_CATE    = SUBSTR(:NEW.LICENSE_TYPE_CODE,2,1) ,
                            DESCRIPTION  = SUBSTR(:NEW.LICENSE_TYPE_NAME,1,40),
                            INSURE_CAT    = :NEW.INSURANCE_TYPE,
                             SERV_CAT       = :NEW.JURIS_PERSON_TYPE
              WHERE COMP_CATE  = SUBSTR(:OLD.LICENSE_TYPE_CODE,2,1)  ;
             EXCEPTION
                   WHEN OTHERS THEN NULL;
           END;
    END IF;
  END IF;

  IF SUBSTR(:OLD.LICENSE_TYPE_CODE,1,1) = '3' THEN
    IF DELETING  THEN
          BEGIN
              DELETE DLG_COMP_CATE
   WHERE COMP_CATE  = SUBSTR(:OLD.LICENSE_TYPE_CODE,2,1)  ;
               EXCEPTION
                   WHEN OTHERS THEN NULL;
           END;
       END IF;
  END IF;
END;CREATE OR REPLACE TRIGGER "AGDOI"."AG_LICENSE_TYPE_TRG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_LICENSE_TYPE_R" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
  	:NEW.USER_ID 	:= GET_SYS_USER;
  	:NEW.USER_DATE 	:= SYSDATE;
END;

-- ----------------------------
-- Checks structure for table AG_LICENSE_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_LICENSE_TYPE_R" ADD CHECK (JURIS_PERSON_TYPE  in ('1','2','3')) DISABLE;
ALTER TABLE "AGDOI"."AG_LICENSE_TYPE_R" ADD CHECK (AGENT_TYPE in ('A','B','J')) DISABLE;
ALTER TABLE "AGDOI"."AG_LICENSE_TYPE_R" ADD CHECK (INSURANCE_TYPE in ('1','2','3')) DISABLE;

-- ----------------------------
-- Primary Key structure for table AG_LICENSE_TYPE_R
-- ----------------------------
ALTER TABLE "AGDOI"."AG_LICENSE_TYPE_R" ADD PRIMARY KEY ("LICENSE_TYPE_CODE");
