<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicenseReplace.aspx.cs" Inherits="IAS.License.LicenseReplace" %>
<%@ Register Src="~/UserControl/ucReplaceLicense.ascx" TagName="ucReplace" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
<asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
<Triggers>
    <asp:PostBackTrigger ControlID="btnNext" />
</Triggers>
<ContentTemplate>
    <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center">
        <table width="100%">
            <tr>
                <td>
                </td
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table width="80%">
                        <tr>
                            <td align="left">
                                <table width="100%">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <uc1:ucReplace ID="ucReplaceLi" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width:40%; margin: 0px auto 10px auto;">
                                            <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Images/button_next.png" onclick="btnNext_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
