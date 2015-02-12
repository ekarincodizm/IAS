<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Global.Master" AutoEventWireup="true"
    CodeBehind="editRegGuest.aspx.cs" Inherits="IAS.RecycleBin.editRegGuest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <script type="text/javascript">
        function onOkSubmit() {
            if (confirm('ต้องการเปลี่ยนแปลงข้อมูล ใช่หรือไม่')) {
                document.getElementById('<%=btnOkSubmit.ClientID%>').click();
            }
        } 
    </script>
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;">
                        <asp:UpdatePanel ID="uplBeforeReg" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <fieldset>
                                    <legend class="lbl">ลงทะเบียน (อนุมัติ) - เดิม</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTypeMemberBeforeReg" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTypeMemberBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblCompanyBeforeReg" runat="server" CssClass="lbl" Text="บริษัท/สมาคม"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtCompanyBeforeReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTitleBeforeReg" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTitleBeforeReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblFirstNameBeforeReg" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtFirstNameBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblLastNameBrforeReg" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtLastNameBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblSexBeforeReg" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:RadioButtonList ID="rblSexBeforeReg" runat="server" RepeatDirection="Horizontal"
                                                    CssClass="rb">
                                                    <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblIDNumberBeforeReg" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDNumberBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblBirthDayBeforeReg" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtBirthDayBeforeReg" runat="server" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDayBeforeReg" runat="server" TargetControlID="txtBirthDayBeforeReg" Format = "dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblEducationBeforeReg" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtEducationBeforeReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblNationalityBeforeReg" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtNationalityBeforeReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblEmailBeforeReg" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtEmailBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTelCurrentAddressBeforeReg" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTelBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblMobilePhoneBeforeReg" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtMobilePhoneBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblIDOicBeforeReg" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ."></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDOicBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblMemberNumberBeforeReg" runat="server" CssClass="lbl" Text="เลขที่สมาชิก"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDMemberNumberBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset>
                                                    <legend class="lbl">ที่อยู่ปัจจุบัน</legend>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblCurrentAddressBeforeReg" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;" rowspan="2">
                                                                <asp:TextBox ID="txtCurrentAddressBeforeReg" runat="server" CssClass="txt" Height="100px"
                                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblProvinceCurrentAddressBeforeReg" runat="server" Text="จังหวัด"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtProvinceCurrentAddressBeforeReg" runat="server" 
                                                                    CssClass="txt" Width="145px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblDistrictCurrentAddressBeforeReg" runat="server" Text="เขต/อำเภอ"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtDistrictCurrentAddressBeforeReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblParishCurrentAddressBeforeReg" runat="server" Text="แขวง/ตำบล"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtParishCurrentAddressBeforeReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset>
                                                    <legend class="lbl">ที่อยู่ตามทะเบียนบ้าน</legend>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblRegisterAddressBeforeReg" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;" rowspan="2">
                                                                <asp:TextBox ID="txtRegisterAddressBeforeReg" runat="server" CssClass="txt" Height="100px"
                                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblProvinceRegisterAddressBeforeReg" runat="server" Text="จังหวัด"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtProvinceRegisterAddressBeforeReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblDistrictRegisterAddressBeforeReg" runat="server" Text="เขต/อำเภอ"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtDistrictRegisterAddressBeforeReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblParishRegisterAddressBeforeReg" runat="server" Text="แขวง/ตำบล"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtParishRegisterAddressBeforeReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: center;">
                        <asp:UpdatePanel ID="uplAfterReg" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <fieldset>
                                    <legend class="lbl">ลงทะเบียน (อนุมัติ) - ใหม่</legend>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTypeMemberAfterReg" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTypeMemberAfterReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblCompanyAfterReg" runat="server" CssClass="lbl" Text="บริษัท/สมาคม"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtCompanyAfterReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTitleAfterReg" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTitleAfterReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblFirstNameAfterReg" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtFirstNameAfterReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblLastNameAfterReg" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtLastNameAfterReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblSexAfterReg" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:RadioButtonList ID="rblSexAfterReg" runat="server" RepeatDirection="Horizontal"
                                                    CssClass="rb">
                                                    <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblIDNumberAfterReg" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDNumberAfterReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblBirthDayAfterReg" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtBirthDayAfterReg" runat="server" ></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDayAfterReg" runat="server" TargetControlID="txtBirthDayAfterReg" Format = "dd/MM/yyyy">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblEducationAfterReg" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtEducationAfterReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblNationalityAfterReg" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtNationalityAfterReg" runat="server" CssClass="txt">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblEmailAfterReg" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtEmailAfterReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTelAfterReg" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTelAfterReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblMobilePhoneAfterReg" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtMobilePhoneAfterReg" runat="server" CssClass="txt"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblIDOicAfterReg" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ."></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDOicAfterReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblMemberNumberAfterReg" runat="server" CssClass="lbl" Text="เลขที่สมาชิก"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDMemberNumberAfterReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset>
                                                    <legend class="lbl">ที่อยู่ปัจจุบัน</legend>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblCurrentAddressAfterReg" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 30%;" rowspan="2">
                                                                <asp:TextBox ID="txtCurrentAddressAfterReg" runat="server" CssClass="txt" Height="100px"
                                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblProvinceCurrentAddressAfterReg" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtProvinceCurrentAddressAfterReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblDistrictCurrentAddressAfterReg" runat="server" Text="เขต/อำเภอ"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtDistrictCurrentAddressAfterReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblParishCurrentAddressAfterReg" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtParishCurrentAddressAfterReg" runat="server" CssClass="txt">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <fieldset>
                                                                    <legend class="lbl">ที่อยู่ตามทะเบียนบ้าน</legend>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="text-align: right; width: 20%;">
                                                                                <asp:Label ID="lblRegisterAddressAfterReg" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 30%;" rowspan="2">
                                                                                <asp:TextBox ID="txtRegisterAddressAfterReg" runat="server" CssClass="txt" Height="100px"
                                                                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 20%;">
                                                                                <asp:Label ID="lblProvinceRegisterAddressAfterReg" runat="server" Text="จังหวัด"
                                                                                    CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                                <asp:TextBox ID="txtProvinceRegisterAddressAfterReg" runat="server" CssClass="txt">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 20%;">
                                                                                <asp:Label ID="lblDistrictRegisterAddressAfterReg" runat="server" Text="เขต/อำเภอ"
                                                                                    CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                                <asp:TextBox ID="txtDistrictRegisterAddressAfterReg" runat="server" CssClass="txt">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="text-align: right; width: 20%;">
                                                                                <asp:Label ID="lblParishRegisterAddressAfterReg" runat="server" Text="แขวง/ตำบล"
                                                                                    CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                                <asp:TextBox ID="txtParishRegisterAddressAfterReg" runat="server" CssClass="txt">
                                                                                </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="text-align: right; width: 15%;">
                        <asp:Label ID="lblResultReg" runat="server" Text="ผลการสมัคร" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 85%;" colspan="3">
                        <asp:TextBox ID="txtResultReg" runat="server" CssClass="txt" Height="100px" Width="500px"
                            TextMode="MultiLine" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <fieldset>
                <legend class="lbl">เอกสารแนบ:</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center; width: 30%;">
                            <asp:Label ID="lblTypeAttachment" runat="server" Text="ประเภทไฟล์แนบ" CssClass="lblbig"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 20%;">
                            <asp:Label ID="lblFile" runat="server" Text="ไฟล์แนบ" CssClass="lblbig"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 40%;">
                            <asp:Label ID="lblDetail" runat="server" Text="หมายเหตุ" CssClass="lblbig"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 10%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlTypeAttachment" runat="server" CssClass="ddl" Width="200px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: center; width: 20%;">
                            <asp:FileUpload ID="fulFile" runat="server" />
                        </td>
                        <td style="text-align: left; width: 40%;">
                            <asp:TextBox ID="txtDetail" runat="server" CssClass="txt" Width="200px"></asp:TextBox>
                        </td>
                        <td style="text-align: center; width: 10%;">
                            <asp:Button ID="btnUploadFile" runat="server" Text="โหลดไฟล์" CssClass="btn" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:GridView ID="gvUpload" runat="server" BackColor="White" BorderColor="#999999"
                                HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3"
                                GridLines="Vertical" Width="100%" AllowPaging="True" BorderStyle="None" EnableTheming="True"
                                PageSize="12" ssClass="gridtxlist" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">"
                                PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First"
                                AllowSorting="True" PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center"
                                AutoGenerateColumns="False" PagerStyle-CssClass="viewpager">
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                <RowStyle CssClass="view" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                <AlternatingRowStyle CssClass="view2" />
                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ประเภทไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeAttachmentGv" runat="server" Text='<%# Eval("ATTACH_FILE_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblFileGv" runat="server" Text='<%# Eval("ATTACH_FILE_PATH") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หมายเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="40%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click">ดู</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>--%>
                </table>
            </fieldset>
            <br />
            <br />
            <fieldset>
                <legend class="lbl">เจ้าหน้าที่ คปภ.</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblApprover" runat="server" CssClass="lbl" Text="ผู้อนุมัติ"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtApprover" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblApproveDate" runat="server" CssClass="lbl" Text="วันที่อนุมัติ"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtApproveDate" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblStatus" runat="server" CssClass="lbl" Text="สถานะ"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="ddl" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button ID="btnOk" runat="server" Style="position: static" Text="ตกลง" CssClass="btn"
                                OnClick="btnOk_Click" />
                            <asp:Button ID="btnOkSubmit" runat="server" Text="ตกลง" CssClass="btn" Style="display: none"
                                OnClick="btnOkSubmit_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
