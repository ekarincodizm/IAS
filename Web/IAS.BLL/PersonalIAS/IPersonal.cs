using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IAS.BLL.PersonalIAS
{
    public interface IPersonal
    {
        IEnumerable<DTO.PersonAttatchFile> AttachFiles { get; }

        String ID { get; set; } //	VARCHAR2	(	15	)
        String MEMBER_TYPE { get; set; } //	VARCHAR2	(	1	)
        String ID_CARD_NO { get; set; } //	VARCHAR2	(	13	)
        String EMPLOYEE_NO { get; set; } //	VARCHAR2	(	20	)
        String PRE_NAME_CODE { get; set; } //	VARCHAR2	(	3	)
        String NAMES { get; set; } //	VARCHAR2	(	50	)
        String LASTNAME { get; set; } //	VARCHAR2	(	70	)
        String NATIONALITY { get; set; } //	VARCHAR2	(	20	)
        DateTime BIRTH_DATE { get; set; } //	DATE	(	7	)
        String SEX { get; set; } //	VARCHAR2	(	1	)
        String EDUCATION_CODE { get; set; } //	VARCHAR2	(	2	)
        String ADDRESS_1 { get; set; } //	VARCHAR2	(	200	)
        String ADDRESS_2 { get; set; } //	VARCHAR2	(	200	)
        String AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        String PROVINCE_CODE { get; set; } //	VARCHAR2	(	3	)
        String ZIP_CODE { get; set; } //	VARCHAR2	(	5	)
        String TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        String LOCAL_ADDRESS1 { get; set; } //	VARCHAR2	(	100	)
        String LOCAL_ADDRESS2 { get; set; } //	VARCHAR2	(	100	)
        String LOCAL_AREA_CODE { get; set; } //	VARCHAR2	(	8	)
        String LOCAL_PROVINCE_CODE { get; set; } //	VARCHAR2	(	20	)
        String LOCAL_ZIPCODE { get; set; } //	VARCHAR2	(	5	)
        String LOCAL_TELEPHONE { get; set; } //	VARCHAR2	(	15	)
        String EMAIL { get; set; } //	VARCHAR2	(	255	)
        String STATUS { get; set; } //	VARCHAR2	(	1	)
        String TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        String LOCAL_TUMBON_CODE { get; set; } //	VARCHAR2	(	4	)
        String COMP_CODE { get; set; } //	VARCHAR2	(	4	)
        String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)

        void Init();

        void Save();

        void AddAttach(DTO.PersonAttatchFile attachFile, Stream fileSteam);
        DTO.PersonAttatchFile GetAttach(string id, string type, ref Stream fileStrem);
        void DeleteAttach(DTO.PersonAttatchFile attachFile);
    }
}
