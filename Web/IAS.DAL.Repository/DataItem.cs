using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class DataItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Request { get; set; }

        public string TRAIN_DISCOUNT_STATUS { get; set; }
        public string EXAM_DISCOUNT_STATUS { get; set; }
        public string SPECIAL_TYPE_CODE_TRAIN { get; set; }
        public string SPECIAL_TYPE_CODE_EXAM { get; set; }
        

    }
}
