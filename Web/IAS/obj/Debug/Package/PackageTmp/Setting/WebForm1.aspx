<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="WebForm1.aspx.cs" Inherits="IAS.Setting.WebForm1" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
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
    <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <ajaxToolkit:TabContainer ID="TabSetting" runat="server" ActiveTabIndex="1" AutoPostBack="true"
                                Width="100%" OnActiveTabChanged="TabSetting_ActiveTabChanged">
                                <ajaxToolkit:TabPanel ID="TabSettingRegister" runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHistoryExam" runat="server" ForeColor="Black" Text="ประวัติการสอบ"></asp:Label>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <fieldset>
                                            <legend class="lbl">ตั้งค่าเอกสารสมัครสมาชิก</legend>
                                            <table align="center" style="width: 90%;">
                                                <tr>
                                                    <td style="text-align: left; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <asp:Label ID="lblUserType" runat="server" CssClass="lbl" Text="ประเภทผู้ใช้ระบบ"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="ddl" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <asp:Label ID="lblDocument" runat="server" CssClass="lbl" Text="เอกสาร"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="ddl" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left;">
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:Button ID="btnInsertConfigDocument" runat="server" CssClass="btn" OnClick="btnInsertConfigDocument_Click"
                                                            Text="เพิ่ม" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: center;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                        <table align="center" style="width: 90%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvConfigDocument" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        EnableTheming="True" GridLines="Vertical" OnRowDataBound="gvConfigDocument_RowDataBound"
                                                        Width="100%">
                                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No." Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ID") %>'></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เอกสาร" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocumentCode" runat="server" Text='<%# Bind("DOCUMENT_CODE") %>'
                                                                        Visible="true"></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="55%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เอกสาร">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("DOCUMENT_NAME") %>'
                                                                        Visible="true"></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="55%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ประเภทผู้ใช้">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMember" runat="server" Text='<%# Bind("MEMBER_NAME") %>' Visible="true"></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ต้องทำการแนบเอกสาร">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkDocumentRequire" runat="server" Checked="false" /></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Value" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblValueGvApprove" runat="server" Text='<%# Bind("DOCUMENT_REQUIRE") %>'></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplDeleteConfigDocument" runat="server" OnClick="hplDeleteConfigDocument_Click"
                                                                        OnClientClick="return confirmDelete();">ลบ</asp:LinkButton></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                                        <RowStyle CssClass="table_item_row" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="text-align: center;">
                                            <asp:Button ID="btnSubmitConfigDocument" runat="server" CssClass="btn" OnClick="btnSubmitConfigDocument_Click"
                                                Text="ตกลง" /><asp:Button ID="btnCancelConfigDocument" runat="server" CssClass="btn"
                                                    OnClick="btnCancelConfigDocument_Click" OnClientClick="return confirmCancel();"
                                                    Text="ยกเลิก" /></div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabSettingLicense" runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblHistoryTraining" runat="server" ForeColor="Black" Text="ประวัติการอบรม"></asp:Label>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                       <%--<fieldset>
                                            <legend class="lbl">ตั้งค่าเอกสารขอรับใบอนุญาต</legend>
                                            <table align="center" style="width: 90%;">
                                                <tr>
                                                    <td style="text-align: left; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="รูปแบบการขอรับใบอนุญาต"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:DropDownList ID="ddlPetitionType" runat="server" CssClass="ddl" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 20%;">
                                                        <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ประเภทการขอรับใบอนุญาต"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="เอกสาร"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:DropDownList ID="ddlLicenseDocumentType" runat="server" CssClass="ddl" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="บังคับแนบเอกสาร"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:CheckBox ID="chkDocumentLicense" runat="server" />
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left;">
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:Button ID="btnInsertConfigLicense" runat="server" CssClass="btn" OnClick="btnInsertConfigLicense_Click"
                                                            Text="เพิ่ม" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: center;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>--%>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabSettingInsertDelete" runat="server" Width="100%">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblTraining" runat="server" ForeColor="Black" Text="คุณสมบัติ(7)-(3)"></asp:Label>
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                      <fieldset>
                                            <legend class="lbl">เพิ่มประเภทเอกสาร</legend>
                                            <table align="center" style="width: 90%;">
                                                <tr>
                                                    <td style="text-align: left; width: 15%;">
                                                    </td>
                                                    <td style="text-align: left; width: 15%;">
                                                        <asp:Label ID="lblDocumentType" runat="server" CssClass="lbl" Text="ชื่อประเภทเอกสาร"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left; width: 25%;">
                                                        <asp:TextBox ID="txtDocumentName" runat="server" CssClass="txt"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: center; width: 35%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left;">
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        <asp:Button ID="btnInsertDocumentType" runat="server" CssClass="btn" OnClick="btnInsertDocumentType_Click"
                                                            Text="เพิ่ม" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="text-align: center;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>

                                        <table align="center" style="width: 90%;">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                                                        EnableTheming="True" GridLines="Vertical" OnRowDataBound="gvDocumentType_RowDataBound"
                                                        Width="100%">
                                                        <AlternatingRowStyle CssClass="table_alter_row" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="No.">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No." Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Bind("Id") %>'></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="เอกสาร">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("Name") %>' Visible="true"></asp:Label></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="60%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ดำเนินการ">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="hplDeleteDocumentType" runat="server" OnClick="hplDeleteDocumentType_Click"
                                                                        OnClientClick="return confirmDelete();">ลบ</asp:LinkButton></ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BorderStyle="None" />
                                                        <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                                        <RowStyle CssClass="table_item_row" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
             <uc2:UCModalError ID="UCModalError" runat="server" />
                <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
