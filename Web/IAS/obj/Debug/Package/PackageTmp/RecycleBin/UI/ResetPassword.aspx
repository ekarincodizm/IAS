<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="ResetPassword.aspx.cs" Inherits="IAS.UI.ResetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <table style="width: 100%;">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblUserName" runat="server" Text="ชื่อผู้ใช้งาน" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtUserName" runat="server" autocomplete="off" CssClass="txt"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;">
                        <asp:Button runat="server" ID="btnAdd" Text="ตกลง" CssClass="btn" />
                        &nbsp;
                        <asp:Button runat="server" ID="btnCancel" Text="ยกเลิก" CssClass="btn" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
