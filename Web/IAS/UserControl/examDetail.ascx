<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="examDetail.ascx.cs" Inherits="IAS.UserControl.examDetail" %>
<fieldset style="width: 90%; margin: 0px auto 10px auto;">
        <legend class="Initial">
            <asp:Label ID="Label4" runat="server" BorderStyle="None" Text="ข้อมูลการสอบ" />
        </legend>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 30%;">
                    <span>
                        <asp:Label ID="lblDetailExamCode" runat="server" CssClass="lbl" 
                        Text="รหัสสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtDetailExamCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 25%;">
                    <span>
                        <asp:Label ID="lblDetailDateExam" runat="server" CssClass="lbl" 
                        Text="วันที่สอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtDetailDateExam" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="lblDetailTimeExamCode" runat="server" CssClass="lbl" 
                        Text="รหัสเวลาที่สอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailTimeExamCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailTimeExam" runat="server" CssClass="lbl" 
                        Text="เวลาที่สอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailTimeExam" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="lblDetailYardCode" runat="server" CssClass="lbl" 
                        Text="รหัสสนามสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailYardCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailNameYard" runat="server" CssClass="lbl" 
                        Text="ชื่อสนามสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailNameYard" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="lblDetailOfficerCode" runat="server" CssClass="lbl" 
                        Text="รหัสประเภทใบอนุญาตที่สอบ"></asp:Label>
                    &nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailOfficerCode" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailTypeLicenseExam" runat="server" CssClass="lbl" 
                        Text="ชื่อประเภทใบอนุญาตที่สอบ"></asp:Label>
                    </span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailTypeLicenseExam" runat="server" 
                        CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="Label15" runat="server" CssClass="lbl" Text="บังคับสอบ"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailQuantityExam" runat="server" CssClass="lbl" 
                        Text="จำนวนสมัคร (คน)"></asp:Label>
                    </span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailQuantityExam" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <span>
                        <asp:Label ID="lblDetailNumberOfSeat" runat="server" CssClass="lbl" 
                        Text="จำนวนที่นั่ง (คน)"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtDetailNumberOfSeat" runat="server" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblDetailFee" runat="server" CssClass="lbl" 
                        Text="ค่าธรรมเนียม (บาท)"></asp:Label>
                        </span>&nbsp;</td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtDetailFee" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
            </tr>
        </table>
    </fieldset>