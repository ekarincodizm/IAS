using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ConfigListModel
    {
        private ConfigTabModel _configTabModel;

        public ConfigListModel()
        {
        }
        public ConfigListModel(ConfigTabModel configTabModel)
        {
            _configTabModel = configTabModel;
        }
        public ConfigTabModel ConfigTabModel { get { return _configTabModel; } set { _configTabModel = value; } }
        //************  setting ID ***********************
        private String _id;
        public String Id { get { return _id; } set { _id = value; } }
        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id.GetHashCode();
            hashCode = 19 * hashCode + ConfigTabModel.Id.GetHashCode();
            return hashCode;
            return _id.GetHashCode();
        }
             
        //*************************************************//
    
    
    }
}
