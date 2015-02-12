<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SetSubjectGroupD.aspx.cs" Inherits="IAS.Setting.SetSubjectGroup" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content" align="center">
        <div class="box_content_header">
            <div class="box_content_header_text">
                จัดการหลักสูตร
            </div>
        </div>
        <div style="clear: left;">
        </div>
        <div style="width: 850px; margin: auto;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>ประเภทใบอนุญาต</legend>
                        <table width="100%" cellpadding="5" cellspacing="5">
                            <tr>
                                <td align="right" width="45%">
                                    ประเภทใบอนุญาต :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="ddl" DataValueField="Id"
                                        DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span style="color: Red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlType"
                                        Text="*" ValidationGroup="a" ErrorMessage="กรุณาเลือกประเภทใบอนุญาต">
                                    </asp:RequiredFieldValidator>
                                    <asp:Button ID="btnadd" runat="server" Text="เพิ่ม" OnClick="btnadd_Click" CssClass="btn" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset id="fieldSubject" runat="server" visible="false">
                        <legend>ข้อมูลวิชา</legend>
                        <table width="100%" cellpadding="5" cellspacing="5">
                            <tr>
                                <td align="right" width="45%">
                                    วันที่มีผลบังคับใช้ :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="imgPopup_txtStartDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="txtStartDate_CalendarExtender" PopupButtonID="imgPopup_txtStartDate"
                                        runat="server" Enabled="True" TargetControlID="txtStartDate">
                                    </ajaxToolkit:CalendarExtender>
                                    <span style="color: Red;">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtStartDate"
                                        Text="*" runat="server" ValidationGroup="a" ErrorMessage="กรุณาเลือกวันที่มีผลบังคับใช้"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="45%">
                                    ถึงวันที่ :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="imgPopup_txtEndDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                        runat="server" />
                                    <ajaxToolkit:CalendarExtender ID="txtEndDate_CalendarExtender" PopupButtonID="imgPopup_txtEndDate"
                                        runat="server" Enabled="True" TargetControlID="txtEndDate">
                                    </ajaxToolkit:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="45%" valign="top">
                                    หมายเหตุ :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="txt" TextMode="MultiLine" Width="250"
                                        Height="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    ใช้งาน :
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="check" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div align="center">
                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn" OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                        </div>
                        <br />
                    </fieldset>
                    <fieldset id="fieldListSubject" runat="server" visible="false">
                        <legend>รายการวิชา</legend>
                        <asp:GridView ID="GvGroup" runat="server" AutoGenerateColumns="false" Width="99%"
                            CssClass="datatable" CellSpacing="-1" GridLines="None">
                            <AlternatingRowStyle CssClass="altrow" />
                            <Columns>
                                <asp:TemplateField HeaderText="รหัสวิชา">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblSubjectCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อวิชา">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสประเภทใบอนุญาต" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="คะแนนสูงสุด">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblMaxScore" runat="server" Text='<%# Bind("MAX_SCORE") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="กลุ่ม" Visible="false">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblGroup" runat="server" Text='<%# Bind("GROUP_ID") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">
                                    ไม่พบข้อมูล
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <br />
                    </fieldset>
                    <fieldset id="fieldCourse" runat="server" visible="false">
                        <legend>รายการหลักสูตร</legend>
                        <asp:GridView ID="gvSearch" runat="server" AutoGenerateColumns="false" BorderWidth="1"
                            CssClass="datatable" CellSpacing="-1" GridLines="None" Width="99%" OnRowDataBound="gvSearch_RowDataBound">
                            <AlternatingRowStyle CssClass="altrow" />
                            <Columns>
                                <asp:TemplateField HeaderText="ใช้งาน">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:CheckBox ID="check" runat="server" AutoPostBack="true" OnCheckedChanged="check_CheckedChanged" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="รหัสหลักสูตร">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblCourseId" runat="server" Text='<%# Bind("COURSE_NUMBER") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานะ">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                    <ItemStyle Width="150px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LICENSE_TYPE_CODE" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseTypeCode" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLisenseTypeName" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่มีผลบังคับใช้">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Bind("START_DATE", "{0:d}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ถึงวันที่">
                                    <ItemTemplate>
                                        <div align="center">
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Bind("END_DATE", "{0:d}") %>'></asp:Label>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="หมายเหตุ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNote" runat="server" Text='<%# Bind("NOTE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div align="center">
                                    ไม่พบข้อมูล
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:HiddenField ID="hidNewCourse" runat="server" />
                        <asp:HiddenField ID="hidOldCourse" runat="server" />
                        <br />
                        <asp:Button ID="btnSaveCourse" OnClick="btnSaveCourse_Click" runat="server" Text="บันทึก"
                            CssClass="btn" />
                        <br />
                        <br />
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <uc1:UCModalError ID="UCModalError1" runat="server" />
        <uc2:UCModalSuccess ID="UCModalSuccess1" runat="server" />
        <br />
        <br />
        <br />
    </div>
    <div style="clear: left">
    </div>
</asp:Content>
