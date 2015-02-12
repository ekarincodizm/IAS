<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="Registeration.aspx.cs" Inherits="IAS.Mockup.Registeration" %>
<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" tagname="ucAttachFileControl" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
 <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="udpPage" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <uc1:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
