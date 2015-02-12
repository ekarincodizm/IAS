using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class UserProfile
    {
        public string Id { get; set; }
        public string TitleName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string IdCard { get; set; }
        public List<DTO.FunctionMenu> CanAccessSystem { get; set; }
        public string UserGroup { get; set; }
        public int MemberType { get; set; }
        public string CompCode { get; set; }
        public string LicenseNo { get; set; }
        public string OIC_User_Id { get; set; }
        public string OIC_User_Type { get; set; }
        public string OIC_EMP_NO { get; set; }
        public string DeptCode { get; set; }
        public string LoginName { get; set; }
        public string IS_APPROVE { get; set; }

        //Added new By Nattapong
        public string STATUS { get; set; }
        public string AgentType { get; set; }

        //Login
        public string LoginStatus { get; set; }

        //AD Service Properties
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
    }
}
