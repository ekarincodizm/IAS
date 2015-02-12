using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IAS.UserControl
{
    public partial class ConfigTable : System.Web.UI.UserControl
    {
        private String _petitionTypeCode = "";
        public String DataSourceId { get { return String.Format("{0}_{1}", "", _petitionTypeCode); } }

        public String PetitionTypeCode { get { return _petitionTypeCode; } set { _petitionTypeCode = value; } }
        public IEnumerable<DTO.ConfigDocument> ConfigDataSource { get { return (IEnumerable<DTO.ConfigDocument>)Session[DataSourceId]; } }

        public ConfigTable(String petitionTypeCode)
        {
            _petitionTypeCode = petitionTypeCode;
            Session[String.Format("PetitionTypeCode_{0}", _petitionTypeCode)] = _petitionTypeCode;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Init();
            }
            else
            {
                if (Session[String.Format("PetitionTypeCode_{0}", _petitionTypeCode)] != null) _petitionTypeCode = Session[String.Format("PetitionTypeCode_{0}", _petitionTypeCode)].ToString();
                Init();
            }
        }

        public void Load(String petitionTypeCode)
        {
            _petitionTypeCode = petitionTypeCode;
            if (!string.IsNullOrEmpty(_petitionTypeCode))
            {
                Session[String.Format("PetitionTypeCode_{0}", _petitionTypeCode)] = _petitionTypeCode;
            }

            Init();
        }
        private void Init()
        {
            if (!String.IsNullOrEmpty(PetitionTypeCode))
            {
                BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
                IEnumerable<DTO.ConfigDocument> configDocuments = biz.GetDocumentLicenseConfigByPetitionType(PetitionTypeCode).DataResponse;
                if (configDocuments != null && configDocuments.Count() > 0)
                {
                    Session[DataSourceId] = configDocuments;
                    InitGridView(biz);
                }
            }

        }

        protected void InitGridView(BLL.DataCenterBiz biz)
        {

            IEnumerable<DTO.DataItem> licenseTypes = biz.GetLicenseType("").DataResponse;
            licenseTypes.ToList().RemoveAt(0);
            foreach (DTO.DataItem licenseType in licenseTypes)
            {
                ConfigGridView gvDataView = ConCreateGridView(biz, licenseType);
                pnlGridConfig.Controls.Add(gvDataView);
                gvDataView.DataBind();
            }
        }
        public bool SaveChange()
        {
            foreach (ConfigGridView configGrid in pnlGridConfig.Controls.OfType<ConfigGridView>())
            {
                if (!configGrid.SaveChanges())
                    return false;
            }
            return true;
        }
        public ConfigGridView ConCreateGridView(BLL.DataCenterBiz biz, DTO.DataItem licenseType)
        {
            ConfigGridView configGridView = new ConfigGridView(String.Format("gvConfig_{0}", licenseType.Id));
            IEnumerable<DTO.ConfigDocument> configs = ConfigDataSource.Where(c => c.LICENSE_TYPE_CODE == licenseType.Id);

            if (configs != null && configs.Count() > 0)
            {
                configGridView = new ConfigGridView(String.Format("gvConfig_{0}", licenseType.Id));

                /********** Config GridView  **************/
                configGridView.AutoGenerateColumns = false;
                configGridView.Caption = licenseType.Name;

                /************  Add Columns ****************/
                configGridView.AddBoundField("ID", "ID", "", 10, true);
                configGridView.AddBoundField("LICENSE_TYPE_CODE", "LICENSE_TYPE_CODE", "", 10, true);
                configGridView.AddBoundField("DOCUMENT_NAME", "DOCUMENT_NAME", "", 10, true);
                //configGridView.AddCheckBoxField("IS_REQUIRE", "IS_REQUIRE",  10);
                //configGridView.AddCommandField(true);
                configGridView.AddTemplateField("IS_REQUIRE", "IS_REQUIRE");
                configGridView.DataSource = configs;
            }


            return configGridView;
        }
    }


    public class ConfigGridView : System.Web.UI.WebControls.GridView
    {

        public ConfigGridView(String id)
        {
            this.ID = id;
        }

        #region AddControl

        public void AddBoundField(String header, String dataField, String sortByField, Int32 percentWidth, Boolean IsVisible)
        {
            BoundField boundField = new BoundField();
            boundField.HeaderText = header;
            boundField.DataField = dataField;
            boundField.SortExpression = sortByField;
            boundField.ItemStyle.Width = Unit.Percentage(percentWidth);
            boundField.Visible = IsVisible;
            this.Columns.Add(boundField);

        }

        public void AddCheckBoxField(String header, String dataField, Int32 percentWidth)
        {
            CheckBoxField checkBoxField = new CheckBoxField();
            checkBoxField.HeaderText = header;
            checkBoxField.DataField = dataField;
            checkBoxField.ItemStyle.Width = Unit.Percentage(percentWidth);
            this.Columns.Add(checkBoxField);
        }

        public void AddCommandField(Boolean showEditButton)
        {
            CommandField commandField = new CommandField();
            commandField.ShowEditButton = true;

            this.Columns.Add(commandField);
        }

        public void AddTemplateField(String header, String dataField)
        {
            TemplateField templateField = new TemplateField();

            templateField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, header, "");
            templateField.ItemTemplate = new GridViewTemplate(ListItemType.Item, dataField, "CheckBox");
            this.Columns.Add(templateField);
        }

        #endregion Add Region

        #region Event Handler
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
        }

        protected override void OnRowEditing(GridViewEditEventArgs e)
        {
            //base.OnRowEditing(e);
            this.EditIndex = e.NewEditIndex;
            this.DataBind();


        }
        protected override void OnRowUpdating(GridViewUpdateEventArgs e)
        {
            //base.OnRowUpdating(e);
            GridViewRow row = this.Rows[e.RowIndex];
            String id = row.Cells[0].Text;
            Boolean checkValue = ((CheckBox)row.Cells[3].Controls[0]).Checked;
            //CheckBoxField checkBoxField = (CheckBoxField)row.Cells[3].Controls[0];

            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
        }

        protected override void OnRowCancelingEdit(GridViewCancelEditEventArgs e)
        {
            //base.OnRowCancelingEdit(e);

            e.Cancel = true;
            this.EditIndex = -1;
        }

        public Boolean SaveChanges()
        {
            BLL.DataCenterBiz biz = new BLL.DataCenterBiz();
            IList<DTO.ConfigDocument> configDocuments = new List<DTO.ConfigDocument>();

            foreach (GridViewRow row in this.Rows)
            {
                String id = row.Cells[0].Text;
                Boolean checkValue = ((CheckBox)row.Cells[3].Controls[0]).Checked;
                DTO.ConfigDocument config = new DTO.ConfigDocument();
                config.ID = Convert.ToInt16(id);
                config.DOCUMENT_REQUIRE = (checkValue) ? "Y" : "N";
                configDocuments.Add(config);
            }

            DTO.UserProfile profile = (DTO.UserProfile)Context.Session[PageList.UserProfile];
            DTO.ResponseMessage<Boolean> res = biz.UpdateConfigApproveLicense(configDocuments.ToList(), profile);
            if (res.IsError)
            {
                return false;
            }

            return true;
        }
        #endregion End Event Handler
    }

    //A customized class for displaying the Template Column
    public class GridViewTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;

        //A variable to hold the column name.
        string _columnName;

        String _controlType;

        String _id;

        //Constructor where we define the template type and column name.
        public GridViewTemplate(ListItemType type, string colname, string controlType)
        {
            //Stores the template type.
            _templateType = type;

            //Stores the column name.
            _columnName = colname;

            _controlType = controlType;

        }

        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (_templateType)
            {
                case ListItemType.Header:
                    //Creates a new label control and add it to the container.
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                    break;

                case ListItemType.Item:
                    //Creates a new text box control and add it to the container.
                    //Allocates the new text box object.

                    if (_controlType == "TextBox")
                    {
                        TextBox tb1 = new TextBox();
                        tb1.ID = _id;
                        tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                        tb1.Columns = 4;                                        //Creates a column with size 4.
                        container.Controls.Add(tb1);
                    }
                    else if (_controlType == "CheckBox")
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.ID = _id;
                        checkBox.DataBinding += new EventHandler(tb1_DataBinding); //Attaches the data binding event.
                        checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChange);
                        //checkBox_CheckedChange((sender).,e);
                        container.Controls.Add(checkBox);
                    }
                    //Adds the newly created textbox to the container.
                    break;

                case ListItemType.EditItem:
                    //As, I am not using any EditItem, I didnot added any code here.
                    break;

                case ListItemType.Footer:
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + _columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }

        /// <summary>
        /// This is the event, which will be raised when the binding happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tb1_DataBinding(object sender, EventArgs e)
        {
            if (_controlType == "TextBox")
            {
                TextBox txtdata = (TextBox)sender;
                GridViewRow container = (GridViewRow)txtdata.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);
                if (dataValue != DBNull.Value)
                {
                    txtdata.Text = dataValue.ToString();
                }
            }
            else if (_controlType == "CheckBox")
            {
                CheckBox checkBox = (CheckBox)sender;
                GridViewRow container = (GridViewRow)checkBox.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);
                if (dataValue != DBNull.Value)
                {
                    checkBox.Checked = (Boolean)dataValue;
                }
            }


        }
        void checkBox_CheckedChange(Object sender, EventArgs e)
        {


        }
    }
}