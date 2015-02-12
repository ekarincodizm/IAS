<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="NewLicense.aspx.cs" Inherits="IAS.License.NewLicense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <script type="text/javascript">
     function changeUrl() {
         var url = window.location.toString();
         if (url.indexOf('?') == -1) {
             window.location = url + "?M=3&S=1";
         }
     }

     function ddLicenseType() {
         var textBox = document.getElementById("<%= txtFee.ClientID %>");
         var ddl = document.getElementById("<%= ddBusinessType.ClientID %>");

         textBox.value = ddl.options[ddl.selectedIndex].value;
     }
     
 </script>
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger  ControlID="btnNext"/>
        <asp:PostBackTrigger ControlID="ddBusinessType" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center">
            <table  width="100%">
                <tr>
                    <td>
                        <fieldset style="width:40%; margin: 0px auto 10px auto;">
                        <legend><asp:Label ID="lblHeadName" runat="server" CssClass="lbl" Text="ขอรับใบอนุญาตใหม่"></asp:Label></legend>
                        <table width="100%">
                            <tr>
                                <td >
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblType" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddBusinessType" runat="server" Width="200px" AutoPostBack="false" 
                                        onselectedindexchanged="ddBusinessType_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqBuType" runat="server" ControlToValidate="ddBusinessType" 
                                    ErrorMessage="*" CssClass="failureNotification" ValidationGroup="NewLicenseValidateGroup">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblFee" runat="server" CssClass="lbl" Text="ค่าธรรมเนียม"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFee" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFee" 
                                    ErrorMessage="*" CssClass="failureNotification" ValidationGroup="NewLicenseValidateGroup">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="บริษัท" Visible="false"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddCompany" runat="server" Width="200px" Enabled="false" Visible="false"></asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddCompany" 
                                    ErrorMessage="*" CssClass="failureNotification" ValidationGroup="NewLicenseValidateGroup">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right">
                                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Images/button_next.png" ValidationGroup="NewLicenseValidateGroup" onclick="btnNext_Click" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>



 
</asp:Content>
