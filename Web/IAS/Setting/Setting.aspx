<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Setting.aspx.cs" Inherits="IAS.Setting.Setting" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else if (charCode == 13) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ตั้งค่า Config" BorderStyle="None" ID="lblConfigSetting" runat="server" />
            </div>
        </div>
        <br />
        <br />
        <div>
            <asp:Panel ID="PnlMain" runat="server" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlApproveRegisterGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::อนุมัติสมัครสมาชิก</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvApproveRegis" runat="server" Width="100%" BorderWidth="1" EnableTheming="True"
                                                        AutoGenerateColumns="False" AllowSorting="True" OnRowDataBound="gvApproveRegis_RowDataBound"
                                                        CssClass="datatable" CellSpacing="-1" GridLines="None">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="GROUP_CODE" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGroupCodeRegis" runat="server" Text='<%# Bind("GROUP_CODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdGvApproveRegis" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValueGvApproveRegis" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อฟังก์ชัน" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="30%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFuncationNameGv" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSpecifiedValue" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="คำอธิบาย" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="50%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescriptionApproveRegisterGv" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="2">
                                                    <asp:Button ID="btnOkApproveRegis" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOkApproveRegis_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancelApproveRegis" runat="server" Text="ยกเลิก" CssClass="btn"
                                                        OnClick="btnCancelApproveRegis_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::กำหนดข้อมูลสำคัญที่ต้องทำการตรวจสอบเมื่อมีการแก้ไข</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <asp:Panel ID="pnlProfile" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"
                                                            Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkFirstName" runat="server" Text="ชื่อ" CssClass="lbl" Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkLastName" runat="server" Text="นามสกุล" CssClass="lbl" Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkNationality" runat="server" Text="สัญชาติ" CssClass="lbl" Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkBirthday" runat="server" Text="วันเกิด" CssClass="lbl" Checked="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkEducation" runat="server" Text="ระดับการศึกษา" CssClass="lbl"
                                                            Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkEmail" runat="server" Text="อีเมล" CssClass="lbl" Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkTel" runat="server" Text="โทรศัพท์" CssClass="lbl" Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkMobile" runat="server" Text="โทรศัพท์มือถือ" CssClass="lbl"
                                                            Checked="false" />
                                                    </td>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkCurrenAddress" runat="server" Text="ที่อยู่ปัจจุบัน" CssClass="lbl"
                                                            Checked="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; text-align: left;">
                                                        <asp:CheckBox ID="chkLocalAddress" runat="server" Text="ที่อยู่ตามทะเบียนบ้าน" CssClass="lbl"
                                                            Checked="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="5">
                                                        <asp:Button ID="btnSubmitExtraApproveRegis" runat="server" Text="ตกลง" CssClass="btn"
                                                            OnClick="btnSubmitExtraApproveRegis_Click" />
                                                        &nbsp;
                                                        <asp:Button ID="btnCancelExtraApproveRegis" runat="server" Text="ยกเลิก" CssClass="btn"
                                                            OnClick="btnCancelExtraApproveRegis_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="clear: both;">
                        </div>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelGrid">
                                        <ProgressTemplate>
                                            Please wait....
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PnlInspectorDocGridView" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ผู้ตรวจเอกสาร</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvInspectorDoc" runat="server" GridLines="None" Width="100%" CellSpacing="-1"
                                                        BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False" AllowSorting="True"
                                                        CssClass="datatable" OnRowDataBound="gvInspectorDoc_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdGvInspectorDoc" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValueGvInspectorDoc" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemValueGvInspectorDoc" runat="server" Text='<%# Bind("Item_Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="30%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLicenseTypeGv" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ผู้ตรวจเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlInspector" runat="server" CssClass="ddl">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSpecifiedValueInspectorDoc" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ผู้ตรวจเอกสาร" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="40%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescriptionInspectorDocGv" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="2">
                                                    <asp:Button ID="btnOkInspectorDoc" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOkInspectorDoc_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancelInspectorDoc" runat="server" Text="ยกเลิก" CssClass="btn"
                                                        OnClick="btnCancelInspectorDoc_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PnlCfgCheckExamLicense" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::กำหนดการตรวจสอบการขอรับใบอนุญาตใหม่</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvCfgCheckExamLicense" runat="server" CellSpacing="-1" GridLines="None"
                                                        Width="100%" CssClass="datatable" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        AllowSorting="True" onrowdatabound="gvCfgCheckExamLicense_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdCfgCheckExamLicense" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อฟังก์ชัน" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="30%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemCfgCheckExamLicense" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValueCfgCheckExamLicense" runat="server" Text='<%# Bind("Item_Value") %>' Visible="false"></asp:Label>
                                                                    <asp:CheckBox ID="chkItemValueCfgCheckExamLicense" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="คำอธิบาย" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="50%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescriptionCfgCheckExamLicense" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="2">
                                                    <asp:Button ID="btnOkCfgCheckExamLicense" runat="server" Text="ตกลง" 
                                                        CssClass="btn" onclick="btnOkCfgCheckExamLicense_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancelCfgCheckExamLicense" runat="server" Text="ยกเลิก" 
                                                        CssClass="btn" onclick="btnCancelCfgCheckExamLicense_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PnlGeneralGridView" runat="server">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ทั่วไป</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvGeneral" runat="server" CellSpacing="-1" GridLines="None" Width="100%"
                                                        CssClass="datatable" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        AllowSorting="True" OnRowDataBound="gvGeneral_RowDataBound">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle HorizontalAlign="Left" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIdGvGeneral" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="10%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValueGvGeneral" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อฟังก์ชัน" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="30%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFuncationNameGvGeneral" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="10%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSpecifiedValueGeneral" runat="server" />
                                                                    <asp:TextBox ID="txtYearValue" runat="server" Visible="false" Width="100px" Style="text-align: center"
                                                                        onpaste="return false;" ondrop="return false;" onkeypress="return runScript(event)"
                                                                        MaxLength="2"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddlConfigPicForLicense" runat="server" Visible="false" CssClass="ddl"></asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="คำอธิบาย" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                                <ItemStyle Width="50%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDescriptionApproveRegisterGvGeneral" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;" colspan="2">
                                                    <asp:Button ID="btnOkGeneral" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOkGeneral_Click" />
                                                    &nbsp;
                                                    <asp:Button ID="btnCancelGeneral" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancelGeneral_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="clear: left;">
                        </div>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>
