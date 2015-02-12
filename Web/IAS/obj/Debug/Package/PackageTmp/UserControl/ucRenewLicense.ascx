<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucRenewLicense.ascx.cs"
    Inherits="IAS.UserControl.ucRenewLicense" %>
<%@ Register Src="UCLoadingProgress.ascx" TagName="UCLoadingProgress" TagPrefix="uc1" %>
<script type="text/javascript">
    function confirmDeleteAttachFile() {
        return confirm('คุณต้องการลบข้อมูลหรือไม่?');
    }

    //Renew Check All check box Natta@6-9-2556
    function RadioCheck(rb) {
        var gv = document.getElementById("<%= gvRenewLicense.ClientID %>");
        var rbs = gv.getElementsByTagName("input");
        var row = rb.parentNode.parentNode;
        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
                if (rbs[i].checked && rbs[i] != rb) {
                    rbs[i].checked = false;
                    break;
                }
            }
        }
    } 

</script>
<asp:UpdatePanel ID="udpPayment" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlPayment" runat="server" Visible="true" Width="100%">
            <fieldset>
                <legend>รายการใบอนุญาตที่สามารถต่ออายุได้ในปัจจุบัน</legend>
                <table width="100%">
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvRenewLicense" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                Width="100%" BorderStyle="None" HorizontalAlign="Center" OnPageIndexChanging="OnPaging"
                                OnRowDataBound="gvRenewLicense_RowDataBound">
                                <AlternatingRowStyle CssClass="table_alter_row" />
                                <HeaderStyle BorderStyle="None" />
                                <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                <RowStyle CssClass="table_item_row" />
                                <EmptyDataTemplate>
                                    <div style="text-align: center; margin-left: 0; margin-right: 0">
                                        ไม่พบข้อมูล</div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                                        <HeaderTemplate>
                                            <%--<radiobutton id="chkAll" onclick="CheckAllRecords(this,'chkSelect')" runat="server" type="checkbox" visible="true"/></radiobutton>--%>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:RadioButton ID="rdCheck" runat="server" AutoPostBack="true" OnCheckedChanged="rbtnSelect_CheckedChanged"
                                                onclick="RadioCheck(this);" />
                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("LICENSE_NO")%>' />
                                            <%--<asp:RadioButton ID="rdCheck" runat="server" AutoPostBack="true"  />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="เลขใบอนุญาต" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="8%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLicenseNo" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สังกัดบริษัท" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCompName" runat="server" Text='<%# Bind("COMP_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ครั้งที่ต่ออายุ" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRenewTime" runat="server" Text='<%# Bind("RENEW_TIMES") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่ต่ออายุ" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRenewDate" runat="server" Text='<%# Bind("LICENSE_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่หมดอายุ" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpireDate" runat="server" Text='<%# Bind("LICENSE_EXPIRE_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="License_Type_Code" Visible="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="1%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLicenseType" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ค่าธรรมเนียม" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="5%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFee" runat="server" Text='<%# Bind("FEES") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRenewType" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
