using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    public class EmailAddOwnerToExamRoom
    {
        public string TESTING_NO { get; set; }
        public DateTime? TESTING_DATE { get; set; }
        public string TEST_TIME { get; set; }
        public string EXAM_ROOM_NAME { get; set; }
        public List<string> EMAIL { get; set; }
    }
}
