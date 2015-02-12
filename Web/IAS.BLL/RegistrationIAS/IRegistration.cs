using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IAS.BLL.RegistrationIAS.States;

namespace IAS.BLL.RegistrationIAS
{
    public interface IRegistration : IBaseEntity<String>
    {

        IEnumerable<AttachFilesIAS.AttachFile> AttachFiles { get; }
        String DataActionMode { get; set; }
        String ID { get; set; }
        String MEMBER_TYPE { get; set; }  
        String ID_CARD_NO { get; set; }
        String EMPLOYEE_NO { get; set; }
        String PRE_NAME_CODE { get; set; }
        String NAMES { get; set; }
        String LASTNAME { get; set; }
        String NATIONALITY { get; set; }
        DateTime BIRTH_DATE { get; set; }
        String SEX { get; set; }
        String EDUCATION_CODE { get; set; }
        String ADDRESS_1 { get; set; }
        String ADDRESS_2 { get; set; }
        String AREA_CODE { get; set; }
        String PROVINCE_CODE { get; set; }
        String ZIP_CODE { get; set; }
        String TELEPHONE { get; set; }
        String LOCAL_ADDRESS1 { get; set; }
        String LOCAL_ADDRESS2 { get; set; }
        String LOCAL_AREA_CODE { get; set; }
        String LOCAL_PROVINCE_CODE { get; set; }
        String LOCAL_ZIPCODE { get; set; }
        String LOCAL_TELEPHONE { get; set; }
        String EMAIL { get; set; }
        String STATUS { get; set; }
        String TUMBON_CODE { get; set; }
        String LOCAL_TUMBON_CODE { get; set; }
        String COMP_CODE { get; set; }
        String CREATED_BY { get; set; }
        DateTime CREATED_DATE { get; set; }
        String UPDATED_BY { get; set; }
        DateTime UPDATED_DATE { get; set; }
        DateTime? NOT_APPROVE_DATE { get; set; }
        String LINK_REDIRECT { get; set; }
        String REG_PASS { get; set; }

        States.RegistrationStatus StateStatus { get; }

        void Init();

        void Save();


        void AddAttach(FileInfo fileInfo, String attachType);
        AttachFilesIAS.AttachFile GetAttach(String id, ref Stream fileStrem);
        void DeleteAttach(String id);

        void SetApprove(DTO.UserProfile userProfile);
        void SetDisapprove(DTO.UserProfile userProfile);
        void Submit();
        
    }
}
