<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="regApproveOfficerOic.aspx.cs" Inherits="IAS.Register.regApproveOfficerOic" %>

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
                <asp:Label Text="ลงทะเบียน (อนุมัติ)" BorderStyle="None" ID="lblRegisterOfficer"
                    runat="server" /></div>
        </div>

        <div class="box_body_content">
            <div class="box_t_h"></div>
            <div class="box_textbox_bgadd">
            <div class="box_texbox_add_box">
        <table style="width: 100%;">
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtTypeMemberBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblAntecedent" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                :
            </td>
            <div class="ui-widget">
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtTitleAfterReg" runat="server" CssClass="txt">
                    </asp:TextBox>
                </td>
            </div>
        </tr>
<%--        <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblAgentType" runat="server" CssClass="lbl" Text="ประเภทตัวแทนสมาชิก"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:DropDownList ID="ddlAgentType" runat="server" CssClass="ddl" ></asp:DropDownList>
            </td>
            <td style="text-align: right; width: 20%;">
            </td>
            <td style="text-align: left; width: 30%;">
            </td>--%>
        <tr>
            <td style="text-align: right; width: 20%;">
                <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 20%;">
                <span>
                    <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                    :</span>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate3" runat="server" CssClass="lbl" Text="หมายเลขบัตรประชาชน"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate4" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                &nbsp; :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtBirthDay" runat="server" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" runat="server" TargetControlID="txtBirthDay" Format = "dd/MM/yyyy">
                </ajaxToolkit:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                    <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate5" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtNationalityBeforeReg" runat="server" CssClass="txt">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate6" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtEducationBeforeReg" runat="server" CssClass="txt">
                </asp:TextBox>
            </td>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblTel" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtEmailBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate7" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtTelBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 20%; height: 17px;">
                <asp:Label ID="lblStartDate8" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                :
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                <asp:TextBox ID="txtMobilePhoneBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                <span>
                    <asp:Label ID="lblOfficerCode" runat="server" CssClass="lbl" Text="บริษัท/สมาคม :"></asp:Label>
                </span>&nbsp;
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtCompanyBeforeReg" runat="server" CssClass="txt">
                </asp:TextBox>
            </td>
            <td style="text-align: right; width: 20%;">
                &nbsp;<asp:Label ID="lblStartDate2" runat="server" CssClass="lbl" Text="รหัสเจ้าหน้าที่ คปภ. :"></asp:Label>
            </td>
            <td style="text-align: left; width: 30%;">
                <asp:TextBox ID="txtIDOicBeforeReg" runat="server" CssClass="txtreadonly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 20%;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
            <td style="text-align: right; width: 20%;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
        </tr>


    </table>

   

    <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" CssClass="ajax_tabs"  
                    Width="100%" ActiveTabIndex="0">
                            <ajaxToolkit:TabPanel ID="tabCurrent"  runat="server" HeaderText="ที่อยู่ปัจจุบัน">
                                <ContentTemplate>
                                    <table style="width: 100%; border-width: 1px; border-color: #dddddd; border-style: solid">
                                    <tr>
                                        <td style="text-align: right; width: 5%;">
                                            <asp:Label ID="lblCurrentAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 25%;" rowspan="3">
                                            <asp:TextBox ID="txtCurrentAddressBeforeReg" runat="server" CssClass="txt" Height="100px"
                                                Width="200px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblProvinceCurrentAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtProvinceCurrentAddressBeforeReg" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 10%;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDistrictCurrentAddress" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDistrictCurrentAddressBeforeReg" runat="server" CssClass="txt">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 10%;">
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblParishCurrentAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtParishCurrentAddressBeforeReg" runat="server" CssClass="txt">
                                            </asp:TextBox>
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
                                            <asp:Label ID="lblPostcodeCurrentAddress" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtPostcodeCurrentAddress" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                        <ajaxToolkit:TabPanel ID="tabRegis" runat="server" HeaderText="ที่อยู่ตามทะเบียนบ้าน">
                            <ContentTemplate>
                                <table style="width: 100%; border-width: 1px; border-color: #dddddd; border-style: solid">
                                    <tr>
                                        <td style="text-align: right; width: 5%;">
                                            <asp:Label ID="lblRegisterAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 25%;" rowspan="3">
                                            <asp:TextBox ID="txtRegisterAddressBeforeReg" runat="server" CssClass="txt" Height="100px"
                                                Width="200px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblProvinceRegisterAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtProvinceRegisterAddressBeforeReg" runat="server" CssClass="txt">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 10%;">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDistrictRegisterAddress" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDistrictRegisterAddressBeforeReg" runat="server" CssClass="txt">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 10%;">
                                        </td>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblParishRegisterAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtParishRegisterAddressBeforeReg" runat="server" CssClass="txt">
                                            </asp:TextBox>
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
                                            <asp:Label ID="lblPostcodeRegisterAddress" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtPostcodeRegisterAddress" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </ajaxToolkit:TabPanel>
                    </ajaxToolkit:TabContainer>
                </div>
            </div>
        </div>
        <table style="width: 100%;" align="center">
            <tr>
                <td>
                    <%--                <asp:Button Text="ที่อยู่ปัจจุบัน" BorderStyle="None" ID="TabCurrentAddress" CssClass="Initial"
                    runat="server" OnClick="TabCurrentAddress_Click" />
                <asp:Button Text="ที่อยู่ตามทะเบียนบ้าน" BorderStyle="None" ID="TabRegisterAddress"
                    CssClass="Initial" runat="server" OnClick="TabRegisterAddress_Click" />--%>
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
                                <fieldset style="width:50%; text-align:left" >
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
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="txt"></asp:TextBox>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="Label8" runat="server" Text="วันที่อนุมัติ" CssClass="lbl"></asp:Label>
                                <span>:</span>
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:TextBox ID="txtApproval" runat="server" CssClass="txt"></asp:TextBox>
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
