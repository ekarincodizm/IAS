using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ComCode 
    {
        public String ID { get; set; } //	VARCHAR2	(	4	)
        public String NAME { get; set; } //	VARCHAR2	(	200	)
    }
}
