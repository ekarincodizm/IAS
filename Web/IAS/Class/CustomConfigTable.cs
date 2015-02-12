using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace IAS.Class
{
    public class CustomConfigTable : System.Web.UI.WebControls.Panel
    {
        public event EventHandler LinkButtonDelete_Click;
        private String _petitionTypeCode = "";
        public String DataSourceId { get { return String.Format("{0}_{1}", "", _petitionTypeCode); } }

        public String PetitionTypeCode { get { return _petitionTypeCode; } set { _petitionTypeCode = value; } }
        public IEnumerable<DTO.ConfigDocument> ConfigDataSource { get { return (IEnumerable<DTO.ConfigDocument>)Context.Session[DataSourceId]; } }

        public CustomConfigTable(String petitionTypeCode)
        {
            _petitionTypeCode = petitionTypeCode;
            Context.Session[String.Format("PetitionTypeCode_{0}", _petitionTypeCode)] = _petitionTypeCode;
            Init();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Load(String petitionTypeCode)
        {
            _petitionTypeCode = petitionTypeCode;
            if (!string.IsNullOrEmpty(_petitionTypeCode))
            {
                Context.Session[String.Format("PetitionTypeCode_{0}", _petitionTypeCode)] = _petitionTypeCode;
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
                    Context.Session[DataSourceId] = configDocuments;
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
                gvDataView.LinkButtonDelete_Success += new EventHandler(LinkButtonDelete_Success);
                this.Controls.Add(gvDataView);
                gvDataView.DataBind();
            }
        }

        protected void LinkButtonDelete_Success(Object sender, EventArgs e)
        {
            LinkButtonDelete_Click(sender, e);
        }
        public bool SaveChange()
        {
            foreach (ConfigGridView configGrid in this.Controls.OfType<ConfigGridView>())
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
                configGridView.HorizontalAlign = HorizontalAlign.Center;
                configGridView.Caption = licenseType.Name;
                //configGridView.SelectedDataKey["ID"].ToString();
                configGridView.CaptionAlign = TableCaptionAlign.Left;

                /************  Add Columns ****************/
                configGridView.AddLabelTemplateField("ID", "ID", 5, true);
                configGridView.AddLabelRowNumberField("ลำดับ", "ID", 5, true);

                configGridView.AddBoundField("LICENSE_TYPE_CODE", "LICENSE_TYPE_CODE", "", 0, false);
                configGridView.AddBoundField("เอกสาร", "DOCUMENT_NAME", "", 73, true);
                configGridView.AddTemplateField("ต้องทำการแนบเอกสาร", "IS_REQUIRE", 12);
                configGridView.AddLinkButtonTemplateField("ดำเนินการ", "ID", 6);
                configGridView.DataSource = configs;
            }
            return configGridView;
        }
    }


    public class ConfigGridView : System.Web.UI.WebControls.GridView
    {
        private Boolean _isError = false;
        public event EventHandler LinkButtonDelete_Success;

        public ConfigGridView(String id)
        {
            this.ID = id;
            this.CssClass = "datatable";
            this.CellSpacing = -1;
            this.GridLines = System.Web.UI.WebControls.GridLines.None;
            this.BorderWidth = 1;
            this.AlternatingRowStyle.CssClass = "altrow";

        }

        #region AddControl

        public void AddLabelTemplateField(String header, String dataField, Int32 percentWidth, Boolean IsVisible)
        {
            TemplateField templateField = new TemplateField();

            templateField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, header, "");
            templateField.ItemTemplate = new GridViewTemplate(ListItemType.Item, dataField, "Label");
            templateField.ItemStyle.Width = Unit.Percentage(percentWidth);
            templateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            templateField.Visible = IsVisible;
            this.Columns.Add(templateField);
        }
        public void AddLabelRowNumberField(String header, String dataField, Int32 percentWidth, Boolean IsVisible)
        {
            TemplateField templateField = new TemplateField();

            templateField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, header, "");
            templateField.ItemTemplate = new GridViewTemplate(ListItemType.Item, dataField, "LabelRowNumber");
            templateField.ItemStyle.Width = Unit.Percentage(percentWidth);
            templateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            templateField.ItemStyle.CssClass = "td-center";
            templateField.Visible = IsVisible;
            this.Columns.Add(templateField);
        }

        public void AddBoundField(String header, String dataField, String sortByField, Int32 percentWidth, Boolean IsVisible)
        {
            BoundField boundField = new BoundField();
            boundField.HeaderText = header;
            boundField.DataField = dataField;
            boundField.SortExpression = sortByField;
            boundField.ItemStyle.Width = Unit.Percentage(percentWidth);
            boundField.Visible = IsVisible;
            //boundField.ItemStyle.CssClass = "Hide";
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

        public void AddTemplateField(String header, String dataField, Int32 percentWidth)
        {
            TemplateField templateField = new TemplateField();

            templateField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, header, "");
            templateField.ItemTemplate = new GridViewTemplate(ListItemType.Item, dataField, "CheckBox");
            templateField.ItemStyle.Width = Unit.Percentage(percentWidth);
            templateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            templateField.ItemStyle.CssClass = "td-center";
            this.Columns.Add(templateField);
        }

        public void AddLinkButtonTemplateField(String header, String dataField, Int32 percentWidth)
        {
            TemplateField templateField = new TemplateField();

            templateField.HeaderTemplate = new GridViewTemplate(ListItemType.Header, header, "");
            templateField.ItemTemplate = new GridViewTemplate(ListItemType.Item, dataField, "LinkButton");
            templateField.ItemStyle.Width = Unit.Percentage(percentWidth);
            templateField.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            templateField.ItemStyle.CssClass = "td-center";
            this.Columns.Add(templateField);
            (templateField.ItemTemplate as GridViewTemplate).LinkButtonDelete_Click += new EventHandler(LinkButtonDelete_Click);
        }
        public Boolean IsError
        {
            get { return IsError; }
        }
        public void ClearError()
        {
            _isError = false;
        }
        protected void LinkButtonDelete_Click(Object sender, EventArgs e)
        {
            LinkButtonDelete_Success(sender, e);
        }
        #endregion Add Region

        #region Event Handler
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            e.Row.Cells[0].Visible = false;
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
            String id = ((Label)row.Cells[0].Controls[0]).Text;
            Boolean checkValue = ((CheckBox)row.Cells[4].Controls[0]).Checked;

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
                String id = ((Label)row.Cells[0].Controls[0]).Text;
                Boolean checkValue = ((CheckBox)row.Cells[4].Controls[0]).Checked;
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
        public event EventHandler LinkButtonDelete_Click;

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
                    else if (_controlType == "LabelRowNumber")
                    {
                        Label label = new Label();
                        label.ID = _id;
                        label.DataBinding += new EventHandler(tb1_DataBinding);
                        container.Controls.Add(label);
                    }
                    else if (_controlType == "Label")
                    {
                        Label label = new Label();
                        label.ID = _id;
                        label.DataBinding += new EventHandler(tb1_DataBinding);
                        container.Controls.Add(label);
                    }
                    else if (_controlType == "CheckBox")
                    {
                        CheckBox checkBox = new CheckBox();
                        checkBox.ID = _id;
                        checkBox.DataBinding += new EventHandler(tb1_DataBinding); //Attaches the data binding event.
                        checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChange);
                        container.Controls.Add(checkBox);
                    }
                    else if (_controlType == "LinkButton")
                    {
                        LinkButton linkButton = new LinkButton();
                        linkButton.ID = _id;
                        linkButton.Text = "<img src='../images/delete_icon.png' title='ลบ'>";
                        linkButton.Click += new EventHandler(linkButton_Click);
                        container.Controls.Add(linkButton);
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

        protected void linkButton_Click(object sender, EventArgs e)
        {
            LinkButtonDelete_Click(sender, e);
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
            else if (_controlType == "LinkButton")
            {
                LinkButton linkButton = (LinkButton)sender;
                GridViewRow container = (GridViewRow)linkButton.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);
                if (dataValue != DBNull.Value)
                {
                    linkButton.Text = dataValue.ToString();
                }
            }
            else if (_controlType == "Label")
            {
                Label label = (Label)sender;
                GridViewRow container = (GridViewRow)label.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);
                if (dataValue != DBNull.Value)
                {
                    label.Text = dataValue.ToString();
                }

            }
            else if (_controlType == "LabelRowNumber")
            {
                Label label = (Label)sender;
                GridViewRow container = (GridViewRow)label.NamingContainer;
                object dataValue = DataBinder.Eval(container.DataItem, _columnName);
                if (dataValue != DBNull.Value)
                {
                    label.Text = Convert.ToString(container.DataItemIndex + 1);
                }

            }


        }
        void checkBox_CheckedChange(Object sender, EventArgs e)
        {


        }
    }
}