<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="IAS.Mockup.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblNewPass" runat="server"></asp:Label><br />
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txt"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="reqRegPW2" Display="Dynamic"
                                                                runat="server" ControlToValidate="txtPassword" ErrorMessage="รหัสผ่านต้องมีความยาวอย่างน้อย 8 ตัวอักษร แต่ไม่เกิน 14 ตัวอักษร<br/>รหัสผ่านต้องผสมระหว่างตัวเลข ตัวอักษรพิมพ์ใหญ่ ตัวอักษรพิมพ์เล็ก และอักขระพิเศษ อย่างละ 1 ตัวขึ้นไป<br/>อักขระที่อนุญาตให้กำหนดรหัสผ่านได้คือ 0-9 A-Z a-z @ # $ % ^ & _ ( ) { } + ? และ , เท่านั้น" CssClass="failureNotification"
                                                                ValidationGroup="regGuestValidationGroup" ValidationExpression="^(?=^.{8,14}$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&*(){}+?,_])(?!./s*)[a-zA-Z0-9@#$%^&*(){}+?,_]*$"><asp:Label ID="Label1" runat="server" ForeColor="Red" visible="true">*</asp:Label>
                            </asp:RegularExpressionValidator>
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td style="text-align: right; width: 10%;">
                            <asp:Label ID="lblUserName" runat="server" Text="ชื่อผู้ใช้งาน"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtUserName" runat="server" autocomplete="off"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Button runat="server" ID="btnAdd" Text="ตกลง" Width="100px" />
                            &nbsp;
                            <asp:Button runat="server" ID="btnCancel" Text="ยกเลิก" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
