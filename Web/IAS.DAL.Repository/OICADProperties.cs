using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class OICADProperties
    {
        //รหัสแผนก
        //ชื่อแผนก
        //รหัสพนักงาน
        //ชื่อพนักงาน
        //รหัสตำแหน่ง
        //ชื่อตำแหน่ง

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public string Result { get; set; }
        public string ExtensionData { get; set; }
            
    }
}
