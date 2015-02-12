<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintPayment.aspx.cs" Inherits="IAS.Reporting.PrintPayment"
    MasterPageFile="~/MasterPage/Site1.Master" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

<div class="box_content">
	<div class="box_content_header">
		<div class="box_content_header_text"><asp:Label Text="ผลการสอบ" BorderStyle="None" ID="lblResult" CssClass="Initial" runat="server" /></div>
	</div>

    <asp:Panel ID="pnlMain" runat="server">
        &nbsp;<CR:CrystalReportViewer ID="PrintPaymentCrystalReportViewer" runat="server" AutoDataBind="true"
            EnableToolTips="False" HasCrystalLogo="False" HasToggleGroupTreeButton="False"
            HasToggleParameterPanelButton="False" Height="50px" ToolPanelView="None" Width="350px"
            Style="text-align: center" />
        <br />
    </asp:Panel>

</div>
<div style="clear:left;"></div>
    
</asp:Content>
