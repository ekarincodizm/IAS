using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
     [Serializable]
    public class ExamRender
    {
         public string examPlaceGroupCode { get; set; }
         public string examPlaceCode { get; set; }
         public string licenseTypeCode { get; set; }
         public string yearMonth { get; set; }
         public string timeCode { get; set; }
         public DateTime testingDate { get; set; }


         public String ID { get; set; }
         public String Name { get; set; }
         public string CommandArgument { get; set; }
         public Boolean IsSetProperty { get; set; }

    }
}
