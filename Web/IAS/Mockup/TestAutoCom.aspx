<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="TestAutoCom.aspx.cs" Inherits="IAS.Mockup.TestAutoCom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <script type="text/javascript">
        $(document).ready(function ($) {
            var json = $('#<%= hdf.ClientID %>').val();
            $('#<%= txtAssociation.ClientID %>').autocomplete({
                source: eval("(" + json + ")"),
                minLength: 1
            });
        });
    </script>
    <div>
        <asp:TextBox ID="txtAssociation" runat="server" CssClass="txt" Width="83%"></asp:TextBox>
        <asp:HiddenField ID="hdf" runat="server" />
    </div>
</asp:Content>
