<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailExam.ascx.cs"
    Inherits="IAS.UserControl.DetailExam" %>
<link href="../style/global.css" rel="stylesheet" type="text/css" />
<div>
    <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblExamID" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtExamID" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblDateExam" runat="server" Text="วันที่สอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTimeExamID" runat="server" Text="รหัสเวลาที่สอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlTimeExamID" runat="server" CssClass="ddl">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTimeExam" runat="server" Text="เวลาที่สอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtTimeExam" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblGroundExam" runat="server" Text="รหัสสนามสอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlGroundExam" runat="server" CssClass="ddl">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblGroundExamName" runat="server" Text="ชื่อสนามสอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtGroundExamName" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTypeLicenseExam" runat="server" Text="รหัสประเภทใบอนุญาตที่สอบ"
                        CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlTypeLicenseExam" runat="server" CssClass="ddl">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTypeLicenseExamName" runat="server" Text="ชื่อประเภทใบอนุญาตที่สอบ"
                        CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtTypeLicenseExamName" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblForceExam" runat="server" CssClass="lbl" Text="บังคับสอบ"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:CheckBox ID="chkForceExam" runat="server" />
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblAmountReg" runat="server" CssClass="lbl" Text="จำนวนสมัคร(คน)"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtAmountReg" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblQuantityRegister" runat="server" CssClass="lbl" Text="จำนวนรับสมัครต่อคน"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtQuantityRegister" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblFeeExam" runat="server" CssClass="lbl" Text="ค่าธรรมเนียม (บาท)"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtFeeExam" runat="server" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;" colspan="2">
                    <asp:Button ID="btnChoose" runat="server" Text="เลือก" CssClass="btn" />
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: left;" colspan="2">
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
