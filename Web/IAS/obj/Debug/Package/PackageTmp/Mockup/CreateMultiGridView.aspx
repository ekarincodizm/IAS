<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="CreateMultiGridView.aspx.cs" Inherits="IAS.Mockup.CreateMultiGridView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
   <asp:Panel runat="server" ID="pnlMain" ViewStateMode="Enabled">
   
   </asp:Panel>

    <asp:Button runat="server" Text="Save" ID="btn" onclick="btn_Click" />
    </asp:Content>
