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

Date: 2014-04-09 14:42:44
*/


-- ----------------------------
-- Table structure for AG_LICENSE_RENEW_T
-- ----------------------------

CREATE TABLE "AGDOI"."AG_LICENSE_RENEW_T" (
"LICENSE_NO" VARCHAR2(15 BYTE) NOT NULL ,
"RENEW_TIME" NUMBER(2) NOT NULL ,
"RENEW_DATE" DATE NULL ,
"EXPIRE_DATE" DATE NULL ,
"REQUEST_NO" VARCHAR2(8 BYTE) NULL ,
"PAYMENT_NO" VARCHAR2(12 BYTE) NULL ,
"LICENSE_ACT_DATE" DATE NULL ,
"LICENSE_ACTOR" VARCHAR2(60 BYTE) NULL ,
"CANCEL_REASON" VARCHAR2(300 BYTE) NULL ,
"RECORD_STATUS" VARCHAR2(1 BYTE) NULL ,
"USER_ID" VARCHAR2(15 BYTE) NULL ,
"USER_DATE" DATE NULL ,
"RECEIPT_NO" VARCHAR2(12 BYTE) NULL ,
"RECEIPT_DATE" DATE NULL ,
"ELICENSING_FLAG" VARCHAR2(1 BYTE) NULL ,
"PROVINCE_CODE" VARCHAR2(2 BYTE) NULL ,
"HEAD_REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"GROUP_REQUEST_NO" VARCHAR2(20 BYTE) NULL ,
"PETITION_TYPE_CODE" VARCHAR2(2 BYTE) NULL ,
"APPROVE_DOC_TYPE" VARCHAR2(2 BYTE) NULL ,
"APPROVE_BY" VARCHAR2(15 BYTE) NULL ,
"APPROVE_DATE" DATE NULL ,
"UPLOAD_BY_SESSION" VARCHAR2(15 BYTE) NULL ,
"LICENSE_FEE" NUMBER NULL ,
"ID_ATTACH_FILE" VARCHAR2(15 BYTE) NULL ,
"APPROVE_FLAG" VARCHAR2(15 BYTE) NULL 
)
LOGGING
NOCOMPRESS
NOCACHE

;
COMMENT ON TABLE "AGDOI"."AG_LICENSE_RENEW_T" IS '??????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."LICENSE_NO" IS '??????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."RENEW_TIME" IS '???????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."RENEW_DATE" IS '?????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."EXPIRE_DATE" IS '??????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."REQUEST_NO" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."PAYMENT_NO" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."LICENSE_ACT_DATE" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."LICENSE_ACTOR" IS '?????????? (??????????????)';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."CANCEL_REASON" IS '?????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."RECORD_STATUS" IS '??????????????  X = cancel ??????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."USER_ID" IS '?????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."USER_DATE" IS '????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."RECEIPT_NO" IS '????????????????????';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."ELICENSING_FLAG" IS 'E = INSERT OR UPDATE BY E-licensing , Null ???????????????? AGDOI ?????????????? ';
COMMENT ON COLUMN "AGDOI"."AG_LICENSE_RENEW_T"."PROVINCE_CODE" IS '????????????????????????????????';

-- ----------------------------
-- Indexes structure for table AG_LICENSE_RENEW_T
-- ----------------------------
CREATE INDEX "AGDOI"."AGIN_RENEW_DATE"
ON "AGDOI"."AG_LICENSE_RENEW_T" ("RENEW_DATE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_RENEW_LICENSE"
ON "AGDOI"."AG_LICENSE_RENEW_T" ("LICENSE_NO" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_RENEW_LICENSE_PV"
ON "AGDOI"."AG_LICENSE_RENEW_T" ("PROVINCE_CODE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AGIN_RENEW_LI_REC_STATUS"
ON "AGDOI"."AG_LICENSE_RENEW_T" ("RECORD_STATUS" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_LICENSE_RENEW_IND1"
ON "AGDOI"."AG_LICENSE_RENEW_T" ("EXPIRE_DATE" ASC)
LOGGING;
CREATE INDEX "AGDOI"."AG_LICENSE_RENEW_IND2"
ON "AGDOI"."AG_LICENSE_RENEW_T" ("RENEW_TIME" ASC)
LOGGING;

-- ----------------------------
-- Triggers structure for table AG_LICENSE_RENEW_T
-- ----------------------------
CREATE OR REPLACE TRIGGER "AGDOI"."AG_DLG_AG_MAS_RENEW_TRG" AFTER INSERT OR DELETE OR UPDATE ON "AGDOI"."AG_LICENSE_RENEW_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
DECLARE
 	V_OLD_AGENT_TYPE	AG_PAYMENT_T.LICENSE_TYPE_CODE%TYPE;
        V_AGENT_TYPE  		AG_PAYMENT_T.LICENSE_TYPE_CODE%TYPE;
        V_RECV         		AG_PAYMENT_T.RECEIPT_NO%TYPE;       
		V_USER VARCHAR2(20);
 BEGIN
--*** GET USER NAME ?????????????????? ***--	
	V_USER := GET_SYS_USER;		
	
		IF V_USER IS NOT NULL THEN
			IF UPPER(V_USER) <> 'ELICENSE' THEN
-- ????????????????? ????????????? insert ????  update ??????????????
					    IF INSERTING or UPDATING THEN
					      BEGIN
					        SELECT LICENSE_TYPE_CODE,  RECEIPT_NO
					          INTO V_AGENT_TYPE, V_RECV
					          FROM AG_PAYMENT_T
					         WHERE PAYMENT_NO = :NEW.PAYMENT_NO
					               AND REQUEST_NO = :NEW.REQUEST_NO;
					         EXCEPTION
					                    WHEN NO_DATA_FOUND THEN
					                        V_AGENT_TYPE  	:= NULL; 
					       		  	V_RECV   	:= NULL; 
					                    WHEN OTHERS THEN 	
					                    	V_AGENT_TYPE  	:= NULL; 
					       		  	V_RECV   	:= NULL;       					
					      END;
					      IF V_AGENT_TYPE IS NULL THEN
					         BEGIN
					           SELECT LICENSE_TYPE_CODE
					             INTO V_AGENT_TYPE
					            FROM AG_LICENSE_T
					           WHERE LICENSE_NO = :NEW.LICENSE_NO;
					           EXCEPTION
					                      WHEN NO_DATA_FOUND THEN V_AGENT_TYPE := NULL; 
					                      WHEN OTHERS THEN 	V_AGENT_TYPE := NULL; 
					        END;
					      END IF;
					    END IF;    
					    -- ??????????????
					    
					    -- ????????? ????????????? where ??? delete ??? update
					    IF DELETING or UPDATING THEN
					      BEGIN
					        SELECT LICENSE_TYPE_CODE
					          INTO V_OLD_AGENT_TYPE
					          FROM AG_PAYMENT_T
					         WHERE PAYMENT_NO = :OLD.PAYMENT_NO
					               AND REQUEST_NO = :OLD.REQUEST_NO;
					         EXCEPTION
					                    WHEN NO_DATA_FOUND THEN V_OLD_AGENT_TYPE := NULL; 
					                    WHEN OTHERS THEN V_OLD_AGENT_TYPE := NULL; 
					      END;
					      IF V_OLD_AGENT_TYPE IS NULL THEN
					         BEGIN
					           SELECT LICENSE_TYPE_CODE
					             INTO V_OLD_AGENT_TYPE
					            FROM AG_LICENSE_T
					           WHERE LICENSE_NO = :OLD.LICENSE_NO;
					           EXCEPTION
					                      WHEN NO_DATA_FOUND THEN V_OLD_AGENT_TYPE := NULL;
					                      WHEN OTHERS THEN 	V_OLD_AGENT_TYPE := NULL; 
					                      			
					        END;
					      END IF;    
					    END IF;   
					    -- ??????????????
					    
					    IF INSERTING THEN
					        BEGIN
					             INSERT  INTO DLG_AG_MAS_RENEW (AGENT_ID, RENEW_NO , APP_DATE , EXP_DATE, 
										    PAY_NO, RECV_NO, AGENT_TYPE) 
					             	    	VALUES	(:NEW.LICENSE_NO, :NEW.RENEW_TIME, 
					             	    	         :NEW.RENEW_DATE, :NEW.EXPIRE_DATE,
					             	         	 :NEW.PAYMENT_NO, V_RECV, V_AGENT_TYPE) ;
					             EXCEPTION
					                  WHEN OTHERS THEN NULL;                                                                            
					        END;
					    ELSIF UPDATING  THEN
					           BEGIN
					             UPDATE DLG_AG_MAS_RENEW
					                    SET AGENT_ID   = :NEW.LICENSE_NO ,
					                  	AGENT_TYPE = V_AGENT_TYPE,
						          	RENEW_NO   = :NEW.RENEW_TIME, 
					                    	APP_DATE   = :NEW.RENEW_DATE,
					                        EXP_DATE   = :NEW.EXPIRE_DATE,
					                        PAY_NO     = :NEW.PAYMENT_NO,
					                        RECV_NO    = V_RECV
					             	  WHERE AGENT_ID   = :OLD.LICENSE_NO 
					                    AND AGENT_TYPE = V_OLD_AGENT_TYPE
						            AND RENEW_NO   = :OLD.RENEW_TIME;
					       	   EXCEPTION 
					       	   	WHEN OTHERS THEN NULL;
					       	   END;    
					       ELSIF DELETING  THEN
					          BEGIN
						    DELETE DLG_AG_MAS_RENEW
						     WHERE AGENT_ID   = :OLD.LICENSE_NO 
					               AND AGENT_TYPE = V_OLD_AGENT_TYPE      
						       AND RENEW_NO   = :OLD.RENEW_TIME; 
					          EXCEPTION
					          	WHEN OTHERS THEN NULL;
					          END;	  
					       END IF;
			ELSE
				NULL;
			END IF;
		END IF;	
  END;
CREATE OR REPLACE TRIGGER "AGDOI"."AG_LICENSE_RENEW_TRIG" BEFORE INSERT OR UPDATE ON "AGDOI"."AG_LICENSE_RENEW_T" REFERENCING OLD AS "OLD" NEW AS "NEW" FOR EACH ROW
BEGIN
    	:NEW.USER_ID   	:= GET_SYS_USER;
    	:NEW.USER_DATE 	:= SYSDATE;
  END;

-- ----------------------------
-- Primary Key structure for table AG_LICENSE_RENEW_T
-- ----------------------------
ALTER TABLE "AGDOI"."AG_LICENSE_RENEW_T" ADD PRIMARY KEY ("LICENSE_NO", "RENEW_TIME");
