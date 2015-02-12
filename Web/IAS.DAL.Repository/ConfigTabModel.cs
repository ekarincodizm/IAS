using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.DTO
{
    [Serializable]
    public  class ConfigTabModel
    {
        //***********  Init ****************//
        IList<ConfigListModel> _configListModel = new List<ConfigListModel>();
        private ConfigModel _configModel ;

        public ConfigTabModel()
        {

        }
        public ConfigTabModel(ConfigModel configModel)
        {
            _configModel = configModel;
        }
        public ConfigModel ConfigModel { get { return _configModel; } set { _configModel = value; } }
        public IEnumerable<ConfigListModel> ConfigListModels { get { return _configListModel; } }
        public void AddConfigListModel(ConfigListModel configListModel) {
            configListModel.ConfigTabModel = this;
            _configListModel.Add(configListModel);
        }

        //************  setting ID ***********************
        private String _id;
        public String Id { get { return _id; } set { _id = value; } }
        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode = 19 * hashCode + Id.GetHashCode();
            hashCode = 19 * hashCode + ConfigModel.Id.GetHashCode();
            return hashCode;
            return _id.GetHashCode();
        }
        //*************************************************//

        //********************** ***************//
                           

        // ToDo  add property below
        public String Name { get; set; }


    }
}
