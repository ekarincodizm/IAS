using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IAS.DAL;
using System.Runtime.Serialization;
using IAS.Utils;

namespace IAS.DTO
{
    [Serializable]
    public class PersonalT 
    {
        public static DTO.Person per;

        #region properties
        public String APPROVED_BY { get; set; } //	VARCHAR2	(	15	)
        public String AGENT_TYPE { get; set; } //	VARCHAR2	(	1	)
        public String SIGNATUER_IMG { get; set; } //	VARCHAR2	(	100	)
        public Byte[] IMG_SIGN { get; set; } //	BLOB	(	4000	)
        public String ID { get; set; } //	VARCHAR2	(	15	)
        public String MEMBER_TYPE { get; set; } //	VARCHAR2	(	1	)
        public String ID_CARD_NO { get; set; } //	VARCHAR2	(	13	)
        public String EMPLOYEE_NO { get; set; } //	VARCHAR2	(	20	)
        public String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	3	)
        public String NAMES { get; set; } //	VARCHAR2	(	50	)
        public String LASTNAME { get; set; } //	VARCHAR2	(	70	)
        public String NATIONALITY { get; set; } //	VARCHAR2	(	20	)
        public DateTime? BIRTH_DATE { get; set; } //	DATE	(	7	)
        public String SEX { get; set; } //	VARCHAR2	(	1	)
        public String EDUCATION_CODE { get; set; } //	VARCHAR2	(	2	)
        public String ADDRESS_1 { get; set; } //	VARCHAR2	(	200	)
        public String ADDRESS_2 { get; set; } //	VARCHAR2	(	200	)
        public String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String PROVINCE_CODE { get; set; } //	VARCHAR2	(	3	)
        public String ZIP_CODE { get; set; } //	VARCHAR2	(	5	)
        public String TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String LOCAL_ADDRESS1 { get; set; } //	VARCHAR2	(	100	)
        public String LOCAL_ADDRESS2 { get; set; } //	VARCHAR2	(	100	)
        public String LOCAL_AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        public String LOCAL_PROVINCE_CODE { get; set; } //	VARCHAR2	(	20	)
        public String LOCAL_ZIPCODE { get; set; } //	VARCHAR2	(	5	)
        public String LOCAL_TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        public String EMAIL { get; set; } //	VARCHAR2	(	255	)
        public String STATUS { get; set; } //	VARCHAR2	(	1	)
        public String TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        public String LOCAL_TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        public String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime? CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime? UPDATED_DATE { get; set; } //	DATE	(	7	)
        public String APPROVE_RESULT { get; set; } //	VARCHAR2	(	100	)
        #endregion


        #region ISerializable Members
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            DTO.Person ent = new DTO.Person();
            ent.ID = (string)info.GetValue("IID", typeof(string));
            ent.MEMBER_TYPE = (string)info.GetValue("IMEMBER_TYPE", typeof(string));
            ent.ID_CARD_NO = (string)info.GetValue("IID_CARD_NO", typeof(string));
            ent.EMPLOYEE_NO = (string)info.GetValue("IEMPLOYEE_NO", typeof(string));
            ent.PRE_NAME_CODE = (string)info.GetValue("IPRE_NAME_CODE", typeof(string));
            ent.NAMES = (string)info.GetValue("INAMES", typeof(string));
            ent.LASTNAME = (string)info.GetValue("ILASTNAME", typeof(string));
            ent.NATIONALITY = (string)info.GetValue("INATIONALITY", typeof(string));
            ent.BIRTH_DATE = (DateTime?)info.GetValue("IBIRTH_DATE", typeof(DateTime?));
            ent.SEX = (string)info.GetValue("ISEX", typeof(string));
            ent.EDUCATION_CODE = (string)info.GetValue("IEDUCATION_CODE", typeof(string));
            ent.ADDRESS_1 = (string)info.GetValue("IADDRESS_1", typeof(string));
            ent.ADDRESS_2 = (string)info.GetValue("IADDRESS_2", typeof(string));
            ent.AREA_CODE = (string)info.GetValue("IAREA_CODE", typeof(string));
            ent.PROVINCE_CODE = (string)info.GetValue("IPROVINCE_CODE", typeof(string));
            ent.ZIP_CODE = (string)info.GetValue("IZIP_CODE", typeof(string));
            ent.TELEPHONE = (string)info.GetValue("ITELEPHONE", typeof(string));
            ent.LOCAL_ADDRESS1 = (string)info.GetValue("ILOCAL_ADDRESS1", typeof(string));
            ent.LOCAL_ADDRESS2 = (string)info.GetValue("ILOCAL_ADDRESS2", typeof(string));
            ent.LOCAL_AREA_CODE = (string)info.GetValue("ILOCAL_AREA_CODE", typeof(string));
            ent.LOCAL_PROVINCE_CODE = (string)info.GetValue("ILOCAL_PROVINCE_CODE", typeof(string));
            ent.LOCAL_ZIPCODE = (string)info.GetValue("ILOCAL_ZIPCODE", typeof(string));
            ent.LOCAL_TELEPHONE = (string)info.GetValue("ILOCAL_TELEPHONE", typeof(string));
            ent.EMAIL = (string)info.GetValue("IEMAIL", typeof(string));
            ent.STATUS = (string)info.GetValue("ISTATUS", typeof(string));
            ent.TUMBON_CODE = (string)info.GetValue("ITUMBON_CODE", typeof(string));
            ent.LOCAL_TUMBON_CODE = (string)info.GetValue("ILOCAL_TUMBON_CODE", typeof(string));
            ent.COMP_CODE = (string)info.GetValue("ICOMP_CODE", typeof(string));
            ent.CREATED_BY = (string)info.GetValue("ICREATED_BY", typeof(string));
            ent.CREATED_DATE = (DateTime?)info.GetValue("ICREATED_DATE", typeof(DateTime?));
            ent.UPDATED_BY = (string)info.GetValue("IUPDATED_BY", typeof(string));
            ent.UPDATED_DATE = (DateTime?)info.GetValue("IUPDATED_DATE", typeof(DateTime?));
            ent.APPROVE_RESULT = (string)info.GetValue("IAPPROVE_RESULT", typeof(string));
            ent.APPROVED_BY = (string)info.GetValue("IAPPROVED_BY", typeof(string));
            ent.AGENT_TYPE = (string)info.GetValue("IAGENT_TYPE", typeof(string));
            ent.SIGNATUER_IMG = (string)info.GetValue("ISIGNATUER_IMG", typeof(string));
            ent.IMG_SIGN = (Byte[])info.GetValue("IIMG_SIGN", typeof(Byte[]));

            per = ent;
            //ent.MappingToEntity<AG_IAS_PERSONAL_T>(per);// = ent;
        }

        //public void SetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("ISessionID", ISessionID);
        //    info.AddValue("IUserName", IUserName);
        //    info.AddValue("ILoginStatus", ILoginStatus);
        //    info.AddValue("IMemType", IMemType);
        //}

        #endregion

    }
}
