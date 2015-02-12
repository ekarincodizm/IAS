<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="examSchedule.ascx.cs"
    Inherits="IAS.UserControl.examSchedule" %>
<fieldset style="width: 90%; margin: 0px auto 10px auto;">
    <legend class="Initial">
        <asp:Label ID="Label4" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" />
    </legend>
    <table style="width: 100%;">
        <tr>
            <td style="text-align: right; width: 20%;">
                <span>
                    <asp:Label ID="lblYard" runat="server" CssClass="lbl" Text="สถานที่สอบ"></asp:Label>
                    :</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:DropDownList ID="ddlYard" runat="server" CssClass="ddl" Width="150px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="YardRequired" runat="server" ControlToValidate="ddlYard"
                    CssClass="failureNotification" ErrorMessage="Yard is required." ToolTip="Yard is required."
                    ValidationGroup="AssociationValidationGroup">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right; width: 20%;">
                <span>
                    <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>
                    :</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtTypeLicense" runat="server" CssClass="txt"></asp:TextBox>
                <asp:RequiredFieldValidator ID="TypeLicenseRequired" runat="server" ControlToValidate="txtTypeLicense"
                    CssClass="failureNotification" ErrorMessage="TypeLicense is required." ToolTip="TypeLicense is required."
                    ValidationGroup="AssociationValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblDateStartExam" runat="server" CssClass="lbl" Text="วันที่สอบ(เริ่ม)"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtDateStartExam" runat="server" CssClass="txtDate dtpicker"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DateStartExamRequired" runat="server" ControlToValidate="txtDateStartExam"
                    CssClass="failureNotification" ErrorMessage="DateStartExam is required." ToolTip="DateStartExam is required."
                    ValidationGroup="AssociationValidationGroup">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right; width: 20%;">
                <span>
                    <asp:Label ID="lblDateEndExam" runat="server" CssClass="lbl" Text="วันที่สอบ(สิ้นสุด)"></asp:Label>
                    :</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtDateEndExam" runat="server" CssClass="txtDate dtpicker"></asp:TextBox>
                <asp:RequiredFieldValidator ID="DateEndExamRequired" runat="server" ControlToValidate="txtDateEndExam"
                    CssClass="failureNotification" ErrorMessage="DateEndExam is required." ToolTip="DateEndExam is required."
                    ValidationGroup="AssociationValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtTel" runat="server" CssClass="txt"></asp:TextBox>
                <asp:RequiredFieldValidator ID="TelRequired" runat="server" ControlToValidate="txtTel"
                    CssClass="failureNotification" ErrorMessage="Tel is required." ToolTip="Tel is required."
                    ValidationGroup="AssociationValidationGroup">*</asp:RequiredFieldValidator>
            </td>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblDisplay" runat="server" CssClass="lbl" Text="แสดงผล"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                                <asp:RadioButtonList ID="rblDisplay" runat="server" 
                    CssClass="rb" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblDisplay_SelectedIndexChanged" 
                                    AutoPostBack="True">
                                    <asp:ListItem Selected="True" Text="ปฏิทิน" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ตาราง" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="DisplayRequired" runat="server" ControlToValidate="ddlDisplay"
                    CssClass="failureNotification" ErrorMessage="Display is required." ToolTip="Display is required."
                    ValidationGroup="AssociationValidationGroup">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" ValidationGroup="AssociationValidationGroup" />
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
        </tr>
    </table>
</fieldset>
<div style="width: 90%; margin: 0px auto 10px auto;">
    <asp:Calendar ID="cld" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4"
        DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="382px"
        Width="100%" ShowGridLines="True">
        <DayHeaderStyle BackColor="#CCCCCC" BorderColor="Black" Font-Bold="True" Height="50px"
            HorizontalAlign="Left" />
        <DayStyle BorderColor="Black" Height="50px" HorizontalAlign="Left" />
        <TitleStyle BorderColor="Black" Height="15px" HorizontalAlign="Center" />
        <TodayDayStyle BackColor="#FF9900" />
    </asp:Calendar>
</div>
