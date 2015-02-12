<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorMsgNewLine.aspx.cs" Inherits="IAS.Mockup.ErrorMsgNewLine" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <cc1:ToolkitScriptManager ID="script1" runat="server"></cc1:ToolkitScriptManager>
    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtPass2" runat="server"></asp:TextBox>
    <asp:RegularExpressionValidator ID="regPass" runat="server" Display="Dynamic" EnableClientScript="true" ForeColor="Red" ControlToValidate="txtPass"
    ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$"
    ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 8 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร<br/>รหัสผ่านต้องผสมระหว่างตัวเลข ตัวอักษรพิมพ์ใหญ่ ตัวอักษรพิมพ์เล็ก และอักขระพิเศษ อย่างละ 1 ตัวขึ้นไป<br/>อักขระที่อนุญาตให้กำหนดรหัสผ่านได้คือ 0-9 A-Z a-z @ # $ % ^ & _ ( ) { } + ? และ , เท่านั้น">
    </asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="regPass1" runat="server" Display="Dynamic" EnableClientScript="true" ForeColor="Red" ControlToValidate="txtPass2"
    ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$"
    ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 8 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร<br/>รหัสผ่านต้องผสมระหว่างตัวเลข ตัวอักษรพิมพ์ใหญ่ ตัวอักษรพิมพ์เล็ก และอักขระพิเศษ อย่างละ 1 ตัวขึ้นไป<br/>อักขระที่อนุญาตให้กำหนดรหัสผ่านได้คือ 0-9 A-Z a-z @ # $ % ^ & _ ( ) { } + ? และ , เท่านั้น">
    </asp:RegularExpressionValidator>
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="ยืนยัน" onclick="btnSubmit_Click" />
    <uc1:UCLoadingProgress ID="ucLoading" runat="server" />
    <uc2:UCModalError ID="ucError" runat="server" />
    <uc3:UCModalSuccess ID="ucSuccess" runat="server" />
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
