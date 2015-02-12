using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public class ConfigModel
    {
        IList<ConfigTabModel> _configTabModels = new List<ConfigTabModel>();

        public ConfigModel(String id)
        {
        }
        public IEnumerable<ConfigTabModel> ConfigTabModels { get { return _configTabModels; } }
        public void AddTabModel(ConfigTabModel configTabModel) {
            configTabModel.ConfigModel = this;
            _configTabModels.Add(configTabModel);
        }
        //************  setting ID ***********************
        private String _id;
        public String Id { get { return _id; } set { _id = value; } }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        //*************************************************//

        // ToDo  add property below
        
        public String Name { get; set; }




    }
}
