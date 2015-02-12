<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailRegExam.ascx.cs"
    Inherits="IAS.UserControl.DetailRegExam" %>
<script type="text/javascript">
    function isValid() { }
</script>
<asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
    <table style="width: 100%;">
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:DropDownList ID="ddlTitleName" runat="server" CssClass="ddl">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Label ID="lblName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:TextBox ID="txtName" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblSurName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtSurName" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Label ID="lblIdNumber" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:TextBox ID="txtIdNumber" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                    <asp:ListItem Text="ชาย" Value="Male" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="หญิง" Value="Female"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Label ID="lblBirthDay" runat="server" Text="วันเกิด" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:TextBox ID="txtBirthDay" runat="server" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" runat="server" TargetControlID="txtBirthDay" Format = "dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblEducation" runat="server" Text="ระดับการศึกษา" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:DropDownList ID="ddlEducation" runat="server" CssClass="ddl">
                </asp:DropDownList>
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Label ID="lblExamDate" runat="server" Text="วันที่สอบ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:TextBox ID="TextBox1" runat="server" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CLDTextBox1" runat="server" TargetControlID="TextBox1" Format = "dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblRegExamID" runat="server" Text="รหัสผู้สมัครสอบ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtRegExamID" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Label ID="lblExamID" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:TextBox ID="txtExamID" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblGroundExam" runat="server" Text="รหัสสนามสอบ" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtGroundExam" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Label ID="lblAssociationExamID" runat="server" Text="รหัส สนง. ประกันภัยที่สมัครสอบ"
                    CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 20%;">
                <asp:TextBox ID="txtAssociationExamID" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblInsuranceCompanyID" runat="server" Text="รหัสบริษัทประกันภัย" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtInsuranceCompanyID" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table style="width: 100%;" align="center">
        <tr>
            <td>
                <fieldset>
                    <legend class="lbl">ที่อยู่ตามทะเบียนบ้าน</legend>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblRegisterAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;" rowspan="2">
                                <asp:TextBox ID="txtRegisterAddress" runat="server" CssClass="txt" Height="70px"
                                    Width="200px" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblProvinceRegisterAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlProvinceRegisterAddress" runat="server" CssClass="ddl" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblDistrictRegisterAddress" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlDistrictRegisterAddress" runat="server" CssClass="ddl" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblPostcodeRegisterAddress" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtPostcodeRegisterAddress" runat="server" CssClass="txt" MaxLength="5"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblParishRegisterAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlParishRegisterAddress" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Panel>
