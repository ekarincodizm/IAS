using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class LogOpenMenu // : DAL.AG_IAS_LOG_ACTIVITY
    {
        public String USER_ID { get; set; } //	VARCHAR2	(	15	)
        public String FUNCTION_ID { get; set; } //	VARCHAR2	(	10	)
        public DateTime ACTIVITY_DATETIME { get; set; } //	DATE	(	7	)
        public String ACTION { get; set; } //	VARCHAR2	(	20	)
        public String CREATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime CREATED_DATE { get; set; } //	DATE	(	7	)
        public String UPDATED_BY { get; set; } //	VARCHAR2	(	20	)
        public DateTime UPDATED_DATE { get; set; } //	DATE	(	7	)
    }
}
