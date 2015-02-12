
CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_AS_BUSI_TYPE_R" AS 
SELECT "BUSINESS_CODE","BUSINESS_DESC","USER_ID","USER_DATE"
FROM  AS_BUSI_TYPE_R;

CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_AS_COMPANY_T" AS 
SELECT "COMP_CODE","COMP_NAMET","COMP_ABBR_NAMET","COMP_NAMEE","COMP_ABBR_NAMEE","COMP_TYPE_CODE","BUSINESS_CODE","ESTA_COND","TRADE_NO","COMP_FLAG","RECV_CODE","ZONE_ID","CT_CODE","COMM_LTD","REGIS_FUND","CURR_FUND","SHAREHOLDER_E_PCT","ADDR_NO","ADDR_NO_E","MOO","MOO_E","SOI_T","SOI_E","ROAD_T","ROAD_E","PV_TUMBON","PV_AMPUR","PV_CODE","TELEPHONE_NO","FAX_NO","E_MAIL","WEBSITE","PROP_CODE","INFORM_PERMIT_NO","INFORM_PERMIT_DATE","INFORM_LICENSE_NO","INFORM_LICENSE_DATE","FUND_PAID_AMT","CURR_STOCK_PRICE","CURR_STOCK_AMT","APPROVE_FLAG","PAY_REQ_NO","PAY_REQ_DATE","RECEIPT_REQ_NO","RECEIPT_REQ_DATE","PAY_LICENSE_NO","PAY_LICENSE_DATE","RECEIPT_LICENSE_NO","RECEIPT_LICENSE_DATE","LICENSE_NO","LICENSE_DATE","FEE_AMT","SHAREHOLDER_T_PCT","TRADE_DATE","DOC_NO","DOC_DATE","STATUS","USER_ID","USER_DATE","FLAG_MOTOR","CURR_PAID_AMT","FEE_LICENSE","OLD_COMP_CODE","UCOM_CODE","MIS_SEQ","MINISTER_APPROVE","COMP_NAMEE_MS","COMP_START_DATE","ESTA_TYPE","MOBILE_NO"
FROM AS_COMPANY_T
ORDER BY COMP_NAMET;

CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_AS_COMPANY_T_BC1" AS 
SELECT "COMP_CODE","COMP_NAMET","COMP_ABBR_NAMET","COMP_NAMEE","COMP_ABBR_NAMEE","COMP_TYPE_CODE","BUSINESS_CODE","ESTA_COND","TRADE_NO","COMP_FLAG","RECV_CODE","ZONE_ID","CT_CODE","COMM_LTD","REGIS_FUND","CURR_FUND","SHAREHOLDER_E_PCT","ADDR_NO","ADDR_NO_E","MOO","MOO_E","SOI_T","SOI_E","ROAD_T","ROAD_E","PV_TUMBON","PV_AMPUR","PV_CODE","TELEPHONE_NO","FAX_NO","E_MAIL","WEBSITE","PROP_CODE","INFORM_PERMIT_NO","INFORM_PERMIT_DATE","INFORM_LICENSE_NO","INFORM_LICENSE_DATE","FUND_PAID_AMT","CURR_STOCK_PRICE","CURR_STOCK_AMT","APPROVE_FLAG","PAY_REQ_NO","PAY_REQ_DATE","RECEIPT_REQ_NO","RECEIPT_REQ_DATE","PAY_LICENSE_NO","PAY_LICENSE_DATE","RECEIPT_LICENSE_NO","RECEIPT_LICENSE_DATE","LICENSE_NO","LICENSE_DATE","FEE_AMT","SHAREHOLDER_T_PCT","TRADE_DATE","DOC_NO","DOC_DATE","STATUS","USER_ID","USER_DATE","FLAG_MOTOR","CURR_PAID_AMT","FEE_LICENSE","OLD_COMP_CODE","UCOM_CODE","MIS_SEQ","MINISTER_APPROVE","COMP_NAMEE_MS","COMP_START_DATE","ESTA_TYPE","MOBILE_NO"
 FROM AS_COMPANY_T
 WHERE BUSINESS_CODE='1'
 ORDER BY COMP_NAMET;
 
 CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_AS_COMPANY_T_BC2" AS 
SELECT "COMP_CODE","COMP_NAMET","COMP_ABBR_NAMET","COMP_NAMEE","COMP_ABBR_NAMEE","COMP_TYPE_CODE","BUSINESS_CODE","ESTA_COND","TRADE_NO","COMP_FLAG","RECV_CODE","ZONE_ID","CT_CODE","COMM_LTD","REGIS_FUND","CURR_FUND","SHAREHOLDER_E_PCT","ADDR_NO","ADDR_NO_E","MOO","MOO_E","SOI_T","SOI_E","ROAD_T","ROAD_E","PV_TUMBON","PV_AMPUR","PV_CODE","TELEPHONE_NO","FAX_NO","E_MAIL","WEBSITE","PROP_CODE","INFORM_PERMIT_NO","INFORM_PERMIT_DATE","INFORM_LICENSE_NO","INFORM_LICENSE_DATE","FUND_PAID_AMT","CURR_STOCK_PRICE","CURR_STOCK_AMT","APPROVE_FLAG","PAY_REQ_NO","PAY_REQ_DATE","RECEIPT_REQ_NO","RECEIPT_REQ_DATE","PAY_LICENSE_NO","PAY_LICENSE_DATE","RECEIPT_LICENSE_NO","RECEIPT_LICENSE_DATE","LICENSE_NO","LICENSE_DATE","FEE_AMT","SHAREHOLDER_T_PCT","TRADE_DATE","DOC_NO","DOC_DATE","STATUS","USER_ID","USER_DATE","FLAG_MOTOR","CURR_PAID_AMT","FEE_LICENSE","OLD_COMP_CODE","UCOM_CODE","MIS_SEQ","MINISTER_APPROVE","COMP_NAMEE_MS","COMP_START_DATE","ESTA_TYPE","MOBILE_NO"
 FROM AS_COMPANY_T
 WHERE BUSINESS_CODE='2'
 ORDER BY COMP_NAMET;
 
 CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_IAS_AMPUR" AS 
Select PV_CODE AS ProvinceCode, 
	   PV_AMPUR AS Id, 
	   PV_DEST AS Name
From GB_PROVINCE_R
Where  PV_AMPUR <> '00' AND PV_TUMBON='0000'
Order By Name, PV_CODE,PV_AMPUR;

CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_IAS_PROVINCE" AS 
Select PV_CODE AS Id, PV_DEST AS Name
From GB_PROVINCE_R
Where PV_AMPUR='00' AND PV_TUMBON='0000'
Order By PV_DEST;

CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_IAS_TITLE_NAME" AS 
SELECT PRE_CODE AS Id, PRE_FULL AS Name FROM GB_PREFIX_R ORDER BY Name;

CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_IAS_TITLE_NAME_PRIORITY" AS 
SELECT PRE_CODE AS Id, PRE_FULL AS Name, PRIORITY
FROM GB_PREFIX_R
ORDER BY PRIORITY, PRE_SORT;

CREATE OR REPLACE FORCE VIEW "AGDOI"."VW_IAS_TUMBON" AS 
Select PV_CODE AS ProvinceCode, 
	   PV_AMPUR AS AmpurCode, 
	   PV_TUMBON AS Id,
	   PV_DEST AS Name
From GB_PROVINCE_R
WHERE PV_TUMBON <> '0000'
Order By Name, PV_CODE, PV_AMPUR, PV_TUMBON;

CREATE OR REPLACE FORCE VIEW "AGDOI"."AG_IAS_LICENSE_TYPE_R" AS 
SELECT
AGDOI.AG_LICENSE_TYPE_R.LICENSE_TYPE_CODE,
AGDOI.AG_LICENSE_TYPE_R.LICENSE_TYPE_NAME,
AGDOI.AG_LICENSE_TYPE_R.USER_ID,
AGDOI.AG_LICENSE_TYPE_R.USER_DATE,
AGDOI.AG_LICENSE_TYPE_R.INSURANCE_TYPE,
AGDOI.AG_LICENSE_TYPE_R.AGENT_TYPE,
AGDOI.AG_LICENSE_TYPE_R.IAS_FLAG,
AGDOI.AG_LICENSE_TYPE_R.STATUS,
AGDOI.AG_LICENSE_TYPE_R.ACTIVE_FLAG
FROM
AGDOI.AG_LICENSE_TYPE_R
WHERE IAS_FLAG = 'Y';




