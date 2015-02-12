<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true" CodeBehind="RegisApproveCompare.aspx.cs" Inherits="IAS.Register.RegisApproveCompare" %>
<%@ Register Src="~/UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ลงทะเบียน (อนุมัติ)" BorderStyle="None" ID="lblRegisterOfficer" runat="server" />
            </div>
        </div>
            <asp:UpdatePanel ID="udpMainRegCompare" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="box_body_content" style="width:47.5% !important;float:left;">
                                    <div class="box_t_h">::ลงทะเบียน (อนุมัติ) - เดิม</div>
                                    <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">

                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTypeMemberBeforeReg" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTypeMemberBeforeReg" runat="server" CssClass="txtreadonly" Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblAgentType" runat="server" CssClass="lbl" Text="ประเภทตัวแทนสมาชิก"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:DropDownList ID="ddlAgentTypeBefore" runat="server" CssClass="ddl" ></asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblCompanyBeforeReg" runat="server" CssClass="lbl" Text="บริษัท/สมาคม :"></asp:Label>
                                                <%--<span>:</span>--%>
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
                                                <asp:Label ID="lblTelCurrentAddressBeforeReg" runat="server" Text="เบอร์โทรศัพท์" CssClass="lbl"></asp:Label>
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
                                                <asp:Label ID="lblIDOicBeforeReg" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ. :"></asp:Label>
                                                <%--<span>:</span>--%>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDOicBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblMemberNumberBeforeReg" runat="server" CssClass="lbl" Text="เลขที่สมาชิก :"></asp:Label>
                                                <%--<span>:</span>--%>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDMemberNumberBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset class="fsFixIe">
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
                                                                <asp:TextBox ID="txtProvinceCurrentAddressBeforeReg" runat="server" CssClass="txt">

                                                                </asp:TextBox>
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
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblZipCodeCurrentAddressBeforeReg" runat="server" Text="รหัสไปรษณีย์"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtZipCodeCurrentAddressBeforeReg" runat="server" CssClass="txt">

                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset class="fsFixIe">
                                                <legend class="lbl">ที่อยู่ตามทะเบียนบ้าน</legend>
                                                <asp:Panel ID="pnlRegisAddressBefore" runat="server">
                                                    
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
                                                        <tr>
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblZipCodeRegisterAddressBeforeReg" runat="server" Text="รหัสไปรษณีย์"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtZipCodeRegisterAddressBeforeReg" runat="server" CssClass="txt">

                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>

                                    </div>
                                    </div>
                                    </div>
                                    

                             


                                    <div class="box_body_content" style="width:47.5% !important; float:right; margin-right:15px;">
                                    <div class="box_t_h">::ลงทะเบียน (อนุมัติ) - ใหม่</div>
                                    <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">

                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblTypeMemberAfterReg" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                                                <span>:</span>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtTypeMemberAfterReg" runat="server" CssClass="txtreadonly" Width="170px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ประเภทตัวแทนสมาชิก"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:DropDownList ID="ddlAgentTypeAfter" runat="server" CssClass="ddl" ></asp:DropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblCompanyAfterReg" runat="server" CssClass="lbl" Text="บริษัท/สมาคม :"></asp:Label>
                                                <%--<span>:</span>--%>
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
                                                <asp:Label ID="lblTelAfterReg" runat="server" Text="เบอร์โทรศัพท์" CssClass="lbl"></asp:Label>
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
                                                <asp:Label ID="lblIDOicAfterReg" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ. :"></asp:Label>
                                                <%--<span>:</span>--%>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDOicAfterReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblMemberNumberAfterReg" runat="server" CssClass="lbl" Text="เลขที่สมาชิก :"></asp:Label>
                                                <%--<span>:</span>--%>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtIDMemberNumberAfterReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <fieldset class="fsFixIe">
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
                                                            <td style="text-align: right; width: 20%;">
                                                                <asp:Label ID="lblZipCodeCurrentAddressAfterReg" runat="server" Text="รหัสไปรษณีย์"
                                                                    CssClass="lbl"></asp:Label>
                                                                <span>:</span>
                                                            </td>
                                                            <td style="text-align: left; width: 20%;">
                                                                <asp:TextBox ID="txtZipCodeCurrentAddressAfterReg" runat="server" CssClass="txt">

                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                        <tr>
                                                            <td colspan="2">
                                                            <asp:Panel ID="pnlRegisAddressAfter" runat="server">
                                                                <fieldset class="fsFixIe">
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
                                                                        <tr>
                                                                            <td style="text-align: right; width: 20%;">
                                                                                <asp:Label ID="lblZipCodeRegisterAddressAfterReg" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                                                                                <span>:</span>
                                                                            </td>
                                                                            <td style="text-align: left; width: 20%;">
                                                                                <asp:TextBox ID="txtZipCodeRegisterAddressAfterReg" runat="server" CssClass="txt">

                                                                                </asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </asp:Panel>
                                                            </td>
                                                        </tr>
                                    </table>

                                    </div>
                                    </div>
                                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        <table style="width: 100%;" align="center">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <uc1:ucAttachFileControl ID="ucAttachFileControl1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <uc2:UCModalError ID="UCModalError" runat="server" />
                            <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <%--<tr>
            <td style="text-align: right; width: 20%;">
                &nbsp;
            </td>
            <td style="text-align: right; width: 30%;">
                <asp:Button ID="btnAdd" runat="server" CssClass="btn" Text="ตกลง" />
            </td>
            <td style="text-align: right; width: 20%;">
                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" />
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
        </tr>--%>
            <tr>
                <td style="text-align: center;">
                    &nbsp;
                </td>
            </tr>
        </table>
        <div class="box_body_content">
            <div class="box_t_h">
            </div>
            <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 25%;">
                                <div align="center">
                                <asp:Panel ID="PnlResultReg" runat="server" Visible="true">
                                <fieldset style="width:50%; text-align:left" class="fsFixIe">
                                <legend>ผลการสมัคร</legend>
                                    <table align="center" style="width: 500px;">
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <asp:Label ID="lblResultReg" runat="server" Text="ผลการสมัคร" CssClass="lbl" Visible="false"></asp:Label>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtResultReg" runat="server" CssClass="txt" Height="100px" Width="500px" TextMode="MultiLine" Enabled="true"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div class="box_body_content">
            <div class="box_t_h">
            </div>
            <div class="box_textbox_bgadd">
                <div class="box_texbox_add_box">
                    <%--<asp:Button Text="เจ้าหน้าที่ คปภ." BorderStyle="None" ID="Button2" CssClass="Initial"
                        runat="server" />--%><strong>เจ้าหน้าที่ คปภ.</strong>
                    <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                <asp:Label ID="Label7" runat="server" Text="ผู้อนุมัติ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:TextBox ID="txtApprover" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="Label8" runat="server" Text="วันที่อนุมัติ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:TextBox ID="txtApproveDate" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                <asp:Label ID="Label9" runat="server" Text="สถานะ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:DropDownList ID="ddlStatusApproval" runat="server" CssClass="ddl">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;</td>
                            <td style="text-align: left; width: 20%;">
                                <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" ValidationGroup="regGuestValidationGroup"
                                    OnClick="btnOk_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 5%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>