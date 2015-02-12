<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucConfiglicense.ascx.cs"
    Inherits="IAS.UserControl.ucConfiglicense" %>
<%@ Register Src="UCLoadingProgress.ascx" TagName="UCLoadingProgress" TagPrefix="uc1" %>
<style type="text/css">
    table th
    {
        color: White;
        background-color: #1073ED; /*background-image: url(../images/th.jpg);*/
        font-weight: normal;
        font-size: 12px;
        border-width: 1px;
        padding-top: 3px;
        padding-bottom: 3px;
        padding-left: 3px; /* 	padding-top:5px; 	vertical-align:top;  	text-align:left;  	 	background-repeat:repeat-x; 	height:25px; 	border-style:solid; 	border-color:White;	 	background-image:url(images/th.jpg); 	*/
    }
    
    .table_item_row td
    {
        border-width: 1px;
        border-style: solid;
        border-color: White;
        padding-left: 5px;
        background-color: #eefcec;
    }
    
    
    a:link
    {
        color: #048829;
        text-decoration: none;
        cursor: pointer;
    }
    
    .table_alter_row td
    {
        border-width: 1px;
        border-style: solid;
        border-color: White;
        padding-left: 5px;
        background-color: #CED4F1; /* #d7fbd1;*/
    }
</style>
<asp:UpdatePanel ID="udpConfigLicense" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlConfigLicense" runat="server" Visible="true" Width="100%">
            <asp:GridView ID="gvConfigLicense" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                EnableTheming="True" GridLines="Vertical" Width="100%">
                <AlternatingRowStyle CssClass="table_alter_row" />
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No." Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เอกสาร" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumentCode" runat="server" Text='<%# Bind("DOCUMENT_CODE") %>'
                                Visible="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="55%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เอกสาร">
                        <ItemTemplate>
                            <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("DOCUMENT_NAME") %>'
                                Visible="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="55%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ประเภทผู้ใช้">
                        <ItemTemplate>
                            <asp:Label ID="lblMember" runat="server" Text='<%# Bind("MEMBER_NAME") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ต้องทำการแนบเอกสาร">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDocumentRequire" runat="server" Checked="false" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Value" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblValueGvApprove" runat="server" Text='<%# Bind("DOCUMENT_REQUIRE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ดำเนินการ">
                        <ItemTemplate>
                            <asp:LinkButton ID="hplDeleteConfigLicense" runat="server" OnClick="hplDeleteConfigLicense_Click"
                                OnClientClick="return confirmDelete();">ลบ</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BorderStyle="None" />
                <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                <RowStyle CssClass="table_item_row" />
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
