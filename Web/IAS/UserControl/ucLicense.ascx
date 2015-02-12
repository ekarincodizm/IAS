<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucLicense.ascx.cs" Inherits="IAS.UserControl.ucLicense" %>
 <asp:UpdatePanel ID="udpAttachFiles" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:Panel ID="pnlAttachFiles" runat="server" Visible="true" Width="80%">
            <asp:GridView ID="gvLicense" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" BorderStyle="None" EmptyDataText="ไม่พบข้อมูล" width="100%" >
            <AlternatingRowStyle CssClass="table_alter_row" />
            <HeaderStyle BorderStyle="None" />
            <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
            <RowStyle CssClass="table_item_row" />
                <Columns>
                    <asp:TemplateField HeaderText="GroupNo" Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="5%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="15%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ประเภทคำร้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="15%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("PETITION_TYPE_CODE") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เลขที่อนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="15%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ต่ออายุครั้งที่" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="10%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("RENEW_TIMES") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="เลขที่ประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="15%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="บริษัท" Visible="false" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle Width="15%" />
                            <ItemTemplate>
                                <asp:Label ID="lblAttachFileId" runat="server" Text='<%# Bind("COMP_NAME") %>'></asp:Label>
                            </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>