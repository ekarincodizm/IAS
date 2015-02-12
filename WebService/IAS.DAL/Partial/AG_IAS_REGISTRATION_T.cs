using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data.Objects.DataClasses;

namespace IAS.DAL
{
    
    public partial class AG_IAS_REGISTRATION_T
    {
        //public string Company_Name { get; set; }
        //public string REG_PASSWORD { get; set; }

        [DataMemberAttribute()]
        public global::System.String Company_Name
        {
            get
            {
                return _Company_Name;
            }
            set
            {
                _Company_Name = StructuralObject.SetValidValue(value, true);
            }
        }
        private global::System.String _Company_Name;

        [DataMemberAttribute()]
        public global::System.String REG_PASSWORD
        {
            get
            {
                return _REG_PASSWORD;
            }
            set
            {
                _REG_PASSWORD = StructuralObject.SetValidValue(value, true);
            }
        }
        private global::System.String _REG_PASSWORD;
    }
}
