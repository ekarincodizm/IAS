<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Exam.aspx.cs" Inherits="IAS.Exam.Exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function setdate() {
            $('#txtEndExamDate').attr('readOnly', 'true');
            $('#txtStartExamDate').attr('readOnly', 'true');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลผู้สมัครสอบ" BorderStyle="None" ID="lblDetailPersonExam" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::ผลการนำเข้า</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblIdNumber" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtIdNumber" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblEaxmID" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtEaxmID" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td rowspan="4">
                                        <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblSurName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtSurName" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblStartExamDate" runat="server" Text="วันที่สอบ(เริ่ม)" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtStartExamDate" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtStartExamDate" ImageUrl="~/Images/calendar.gif"
                                            ImageAlign="Bottom" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CLDtxtStartExamDate" PopupButtonID="imgPopup_txtStartExamDate"
                                            runat="server" TargetControlID="txtStartExamDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblEndExamDate" runat="server" Text="วันที่สอบ(สิ้นสุด)" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtEndExamDate" runat="server"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup_txtEndExamDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                            runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CLDtxtEndExamDate" PopupButtonID="imgPopup_txtEndExamDate"
                                            runat="server" TargetControlID="txtEndExamDate" Format="dd/MM/yyyy">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblOrdersGroup" runat="server" Text="ใบสั่งจ่ายรายกลุ่ม" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtOrdersGroup" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right; width: 20%;">
                                        <asp:Label ID="lblOrdersPerson" runat="server" Text="ใบสั่งจ่ายรายบุคคล" CssClass="lbl"></asp:Label>
                                        <span>:</span>
                                    </td>
                                    <td style="text-align: left; width: 30%;">
                                        <asp:TextBox ID="txtOrdersPerson" runat="server" CssClass="txt"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <br />
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center;">
                                    <div class="box_body_content">
                                        <div class="box_t_h">
                                        </div>
                                        <div class="box_textbox_bg_table">
                                            <div class="box_texbox_add_box">
                                                <asp:GridView ID="gvDetail" runat="server" Width="100%" AllowPaging="false" CssClass="datatable"
                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" ShowHeaderWhenEmpty="True"
                                                    EnableTheming="True" AutoGenerateColumns="False" DataKeyNames="ID" OnRowCancelingEdit="gvDetail_RowCancelingEdit"
                                                    OnRowEditing="gvDetail_RowEditing" OnRowUpdating="gvDetail_RowUpdating" OnPreRender="gvDetail_PreRender">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowCancelButton="true"
                                                            EditText="<img src='../Images/edit-icon.gif' title='แก้ไข' />" UpdateText="ตกลง"
                                                            CancelText="ยกเลิก" ItemStyle-Width="5%" ItemStyle-CssClass="td-center" />
                                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdNumberGv" runat="server" Text='<%# Bind("IdNumber") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ใบสั่งจ่ายรายบุคคล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrdersPersonGv" runat="server" Text='<%# Eval("OrdersPerson") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่สมัครสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDateRegExamGv" runat="server" Text='<%# Eval("DateRegExam") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Eval("ExamID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
                                                                <asp:LinkButton ID="hplDelete" runat="server" OnClick="hplDelete_Click"><img src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
            <div class="box_body_content">
                <div class="box_t_h">
                    ::<asp:Label Text="ผู้สมัครสอบ" BorderStyle="None" ID="lblPersonExam" runat="server" /></div>
                <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">
                        <fieldset>
                            <asp:Panel ID="PnlSubMain" runat="server" Visible="true" Width="100%">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDetailTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:DropDownList ID="ddlDetailTitleName" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblDetailName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailName" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDetailSurName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDetailSurName" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblDetailIdNumber" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailIdNumber" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:RadioButtonList ID="rblDetailSex" runat="server" RepeatDirection="Horizontal"
                                                CssClass="rb">
                                                <asp:ListItem Text="ชาย" Value="Male" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="หญิง" Value="Female"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblDetailBirthDay" runat="server" Text="วันเกิด" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailBirthDay" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgPopup_txtDetailBirthDay" ImageUrl="~/Images/calendar.gif"
                                                ImageAlign="Bottom" runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="CLDtxtDetailBirthDay" PopupButtonID="imgPopup_txtDetailBirthDay"
                                                runat="server" TargetControlID="txtDetailBirthDay" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDetailEducation" runat="server" Text="ระดับการศึกษา" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:DropDownList ID="ddlDetailEducation" runat="server" CssClass="ddl">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblDetailExamDate" runat="server" Text="วันที่สอบ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailExamDate" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgPopup_txtDetailExamDate" ImageUrl="~/Images/calendar.gif"
                                                ImageAlign="Bottom" runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="CLDtxtDetailExamDate" PopupButtonID="imgPopup_txtDetailExamDate"
                                                runat="server" TargetControlID="txtDetailExamDate" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDetailRegExamID" runat="server" Text="รหัสผู้สมัครสอบ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDetailRegExamID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblDetailExamNumber" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailExamNumber" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDetailGroundExam" runat="server" Text="รหัสสนามสอบ" CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDetailGroundExam" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblDetailAssociationExamID" runat="server" Text="รหัส สนง. ประกันภัยที่สมัครสอบ"
                                                CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 30%;">
                                            <asp:TextBox ID="txtDetailAssociationExamID" runat="server" CssClass="txt"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 20%;">
                                            <asp:Label ID="lblDetailInsuranceCompanyID" runat="server" Text="รหัสบริษัทประกันภัย"
                                                CssClass="lbl"></asp:Label>
                                            <span>:</span>
                                        </td>
                                        <td style="text-align: left; width: 20%;">
                                            <asp:TextBox ID="txtDetailInsuranceCompanyID" runat="server" CssClass="txt"></asp:TextBox>
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
                                                            <asp:Label ID="lblDetailRegisterAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 30%;" rowspan="2">
                                                            <asp:TextBox ID="txtDetailRegisterAddress" runat="server" CssClass="txt" Height="70px"
                                                                Width="200px" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblDetailProvinceRegisterAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 30%;">
                                                            <asp:DropDownList ID="ddlDetailProvinceRegisterAddress" runat="server" CssClass="ddl"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 20%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblDetailDistrictRegisterAddress" runat="server" Text="เขต/อำเภอ"
                                                                CssClass="lbl"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 30%;">
                                                            <asp:DropDownList ID="ddlDetailDistrictRegisterAddress" runat="server" CssClass="ddl"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblDetailPostcodeRegisterAddress" runat="server" Text="รหัสไปรษณีย์"
                                                                CssClass="lbl"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 30%;">
                                                            <asp:TextBox ID="txtDetailPostcodeRegisterAddress" runat="server" CssClass="txt"
                                                                MaxLength="5"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblDetailParishRegisterAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 30%;">
                                                            <asp:DropDownList ID="ddlDetailParishRegisterAddress" runat="server" CssClass="ddl">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                            <br />
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailAbsentExam" runat="server" Text="ขาดสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:RadioButtonList ID="rblDetailAbsentExam" runat="server" RepeatDirection="Horizontal"
                                                            CssClass="rb">
                                                            <asp:ListItem Text="ใช่" Value="Y" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="ไม่ใช่" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="text-align: right; width: 25%;">
                                                        <asp:Label ID="lblDetailTotalExam" runat="server" Text="ผลการสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 35%;">
                                                        <asp:TextBox ID="txtDetailTotalExam" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailExpireTotalExamDate" runat="server" Text="วันที่ผลสอบหมดอายุ"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:TextBox ID="txtDetailExpireTotalExamDate" runat="server"></asp:TextBox>
                                                        <asp:ImageButton ID="imgPopup_txtDetailExpireTotalExamDate" ImageUrl="~/Images/calendar.gif"
                                                            ImageAlign="Bottom" runat="server" />
                                                        <ajaxToolkit:CalendarExtender ID="CLDtxtDetailExpireTotalExamDate" PopupButtonID="imgPopup_txtDetailExpireTotalExamDate" runat="server"
                                                            TargetControlID="txtDetailExpireTotalExamDate" Format="dd/MM/yyyy">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                    <td style="text-align: right; width: 25%;">
                                                        <asp:Label ID="lblDetailOrderIndividualNumber" runat="server" Text="เลขใบสั่งจ่ายรายบุคคล"
                                                            CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 35%;">
                                                        <asp:TextBox ID="txtDetailOrderIndividualNumber" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailForceStatusExam" runat="server" Text="สถานะบังคับสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:RadioButtonList ID="rblDetailForceStatusExam" runat="server" RepeatDirection="Horizontal"
                                                            CssClass="rb">
                                                            <asp:ListItem Text="ใช่" Value="Y" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="ไม่ใช่" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailStateExam" runat="server" Text="สถานะบังคับสอบ" CssClass="lbl"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:RadioButtonList ID="rblDetailStateExam" runat="server" RepeatDirection="Horizontal"
                                                            CssClass="rb">
                                                            <asp:ListItem Text="ออกแล้ว" Value="Y" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Text="ยังไม่ออก" Value="N"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailRevocationStatus" runat="server" CssClass="lbl" Text="สถานะการยกเลิก"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:CheckBox ID="chkDetailRevocationStatus" runat="server" />
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        <asp:Label ID="lblDetailReasonCancel" runat="server" CssClass="lbl" Text="เหตุผลในการยกเลิก"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 35%;">
                                                        <asp:TextBox ID="txtDetailReasonCancel" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">
                                                        <asp:UpdatePanel ID="UpdatePanelSubject" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:GridView ID="gvSubject" runat="server" Width="100%" AllowPaging="True" CssClass="datatable"
                                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False">
                                                                    <AlternatingRowStyle CssClass="altrow" />
                                                                    <HeaderStyle BorderStyle="None" />
                                                                    <PagerStyle HorizontalAlign="Right" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="วิชา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="50%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSubjectGv" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="คะแนน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPointGv" runat="server" Text='<%# Eval("Point") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ประเภทใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                                            <ItemStyle Width="40%" />
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTypePermitGv" runat="server" Text='<%# Eval("TypePermit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center;" colspan="4">
                                                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;" colspan="2">
                                                        <asp:Button runat="server" ID="btnSubmit" Text="ตกลง" CssClass="btn" />
                                                    </td>
                                                    <td style="text-align: left;" colspan="2">
                                                        <asp:Button runat="server" ID="btnCacel" Text="ยกเลิก" CssClass="btn" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>
