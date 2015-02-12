<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SettingAttach.aspx.cs" Inherits="IAS.Setting.SettingAttach" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

    </script>
    <script type="text/javascript">
        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }
        function confirmCancel() {
            return confirm('คุณต้องการยกเลิก?');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                ตั้งค่าเอกสารแนบ
            </div>
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
        <asp:Panel ID="PanelMain" runat="server">
            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left;">
                                <ajaxToolkit:TabContainer ID="TabSetting" runat="server" Width="100%" ActiveTabIndex="1"
                                    ScrollBars="Auto" OnActiveTabChanged="TabSetting_ActiveTabChanged" CssClass="ajax_tabs"
                                    AutoPostBack="true">
                                    <ajaxToolkit:TabPanel ID="TabSettingRegister" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSettingRegisterHeader" runat="server" ForeColor="Black" Text="ตั้งค่าเอกสารสมัครสมาชิก"></asp:Label></HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::ตั้งค่าเอกสารสมัครสมาชิก</div>
                                                <div class="box_textbox_bgadd">
                                                    <div class="box_texbox_add_box">
                                                        <table align="center" style="width: 100%;">
                                                            <tr>
                                                                <td style="text-align: right;width: 15%;">
                                                                    <asp:Label ID="lblUserType" runat="server" CssClass="lbl" Text="ประเภทผู้ใช้ระบบ"></asp:Label> :
                                                                </td>
                                                                <td style="text-align: left;width: 15%;">
                                                                    <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="ddl" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right;width: 10%;">
                                                                    <asp:Label ID="lblDocument" runat="server" CssClass="lbl" Text="เอกสาร"></asp:Label> :
                                                                </td>
                                                                <td style="text-align: left;width: 50%;">
                                                                    <asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="ddl" width="650px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: left;width: 10%;">
                                                                    <asp:Button ID="btnInsertConfigDocument" runat="server" CssClass="btn" OnClick="btnInsertConfigDocument_Click"
                                                                        Text="เพิ่ม" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="padding-left:0.5%;">
                                                        <asp:GridView ID="gvConfigDocument" runat="server" AutoGenerateColumns="False" EnableTheming="True"
                                                            OnRowDataBound="gvConfigDocument_RowDataBound" CssClass="datatable" CellSpacing="-1"
                                                            GridLines="None" BorderWidth="1px" Width="99%">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ลำดับ">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No." Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ID") %>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เอกสาร" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumentCode" runat="server" Text='<%# Bind("DOCUMENT_CODE") %>'
                                                                            Visible="true"></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เอกสาร">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("DOCUMENT_NAME") %>'
                                                                            Visible="true"></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทผู้ใช้">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMember" runat="server" Text='<%# Bind("MEMBER_NAME") %>' Visible="true"></asp:Label></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="15%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ต้องทำการแนบเอกสาร">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkDocumentRequire" runat="server" Checked="false" /></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Value" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblValueGvApprove" runat="server" Text='<%# Bind("DOCUMENT_REQUIRE") %>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ดำเนินการ">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="hplDeleteConfigDocument" runat="server" OnClick="hplDeleteConfigDocument_Click"
                                                                            OnClientClick="return confirmDelete();"><img src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="10%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Right" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="text-align: center;">
                                                <asp:Button ID="btnSubmitConfigDocument" runat="server" CssClass="btn" OnClick="btnSubmitConfigDocument_Click"
                                                    Text="ตกลง" /> <asp:Button ID="btnCancelConfigDocument" runat="server" CssClass="btn"
                                                        OnClick="btnCancelConfigDocument_Click" OnClientClick="return confirmCancel();"
                                                        Text="ยกเลิก" /></div>
                                            <br />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabSettingLicense" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSettingLicenseHeader" runat="server" ForeColor="Black" Text="ตั้งค่าเอกสารขอรับใบอนุญาต"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::ตั้งค่าเอกสารขอรับใบอนุญาต</div>
                                                <div class="box_textbox_bgadd">
                                                    <div class="box_texbox_add_box">
                                                        <table align="center" style="width: 90%;">
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="รูปแบบการขอรับใบอนุญาต"></asp:Label> :
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlPetitionType" runat="server" CssClass="ddl" Width="200px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ประเภทการขอรับใบอนุญาต"></asp:Label> : 
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl" Width="200px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td rowspan="2">
                                                                    <asp:Button ID="btnInsertConfigLicense" runat="server" CssClass="btn" OnClick="btnInsertConfigLicense_Click" Text="เพิ่ม" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="เอกสาร"></asp:Label> : 
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:DropDownList ID="ddlLicenseDocumentType" runat="server" CssClass="ddl" Width="200px"> 
                                                                    </asp:DropDownList> 
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="บังคับแนบเอกสาร"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <asp:CheckBox ID="chkDocumentLicense" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div style="text-align: center;">
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabSettingInsertDelete" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSettingInsertDeleteHeader" runat="server" ForeColor="Black" Text="เพิ่ม/ลบ ประเภทเอกสาร"></asp:Label></HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::เพิ่มประเภทเอกสาร</div>
                                                <div class="box_textbox_bgadd">
                                                    <div class="box_texbox_add_box">
                                                        <table align="center" style="width: 90%;">
                                                            <tr>
                                                                <td style="text-align: right; width: 40%;">
                                                                    <asp:Label ID="lblDocumentType" runat="server" CssClass="lbl" Text="ชื่อประเภทเอกสาร"></asp:Label> :
                                                                </td>
                                                                <td style="text-align: left; ">
                                                                    <asp:TextBox ID="txtDocumentName" runat="server" CssClass="txt"></asp:TextBox>
                                                                </td>
                                                                <td style="text-align: left; width: 40%;">
                                                                    <asp:Button ID="btnInsertDocumentType" runat="server" CssClass="btn" OnClick="btnInsertDocumentType_Click"
                                                                        Text="เพิ่ม" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::<asp:Label ID="Label4" runat="server" CssClass="lblbig" Text="ตารางแสดงประเภทเอกสารทั้งหมด"></asp:Label></div>
                                                <div class="box_textbox_bg_table">
                                                    <div class="box_texbox_add_box">
                                                        <table align="center" style="width: 100%;">
                                                            <tr>
                                                                <td style="text-align: center;">
                                                                    <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" CssClass="datatable"
                                                                        CellSpacing="-1" GridLines="None" BorderWidth="1px" EnableTheming="True" OnRowDataBound="gvDocumentType_RowDataBound"
                                                                        Width="100%">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ลำดับ">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="No." Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblNo" runat="server" Text='<%# Bind("Id") %>'></asp:Label></ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="เอกสาร">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("Name") %>' Visible="true"></asp:Label></ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Left" Width="60%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ดำเนินการ">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="hplDeleteDocumentType" runat="server" OnClick="hplDeleteDocumentType_Click"
                                                                                        OnClientClick="return confirmDelete();"><img src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton></ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="15%" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <PagerStyle HorizontalAlign="Right" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabSettingApplicantDetail" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label6" runat="server" ForeColor="Black" Text="ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ"></asp:Label></HeaderTemplate>
                                        <ContentTemplate>
                                            <div class="box_body_content">
                                                <div class="box_t_h">
                                                    ::ตั้งค่าเอกสารแนบแก้ไขข้อมูลผู้สมัครสอบ</div>
                                                <div class="box_textbox_bgadd">
                                                    <div class="box_texbox_add_box">
                                                        <table align="center" style="width: 100%;">
                                                            <tr>
                                                                <td style="text-align: right;width: 15%;">
                                                                    <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="ประเภทผู้ใช้ระบบ"></asp:Label> :
                                                                </td>
                                                                <td style="text-align: left;width:15%;">
                                                                    <asp:DropDownList ID="ddlApplicantMemberType" runat="server" CssClass="ddl" >
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: right;width:10%;">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="เอกสาร"></asp:Label> :
                                                                </td>
                                                                <td style="text-align: left;width:50%;">
                                                                    <asp:DropDownList ID="ddlApplicantDocumentType" runat="server" CssClass="ddl" width="650px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="text-align: left;width: 10%;">
                                                                    <asp:Button ID="btnInsertApplicantDetail" runat="server" CssClass="btn" OnClick="btnInsertApplicantDetail_Click"
                                                                        Text="เพิ่ม" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                            <div>
                                            </div>
                                            <div style="clear: both;">
                                            </div>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="padding-left:0.5%;">
                                                        <asp:GridView ID="gvApplicantDetail" runat="server" AutoGenerateColumns="False" EnableTheming="True"
                                                            OnRowDataBound="gvApplicantDetail_RowDataBound" CssClass="datatable" CellSpacing="-1"
                                                            GridLines="None" BorderWidth="1px" Width="99%">
                                                            <AlternatingRowStyle CssClass="altrow" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="ลำดับ">
                                                                    <ItemTemplate>
                                                                        <%#Container.DataItemIndex + 1 %>
                                                                    </ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="No." Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ID") %>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="5%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เอกสาร" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumentCode" runat="server" Text='<%# Bind("DOCUMENT_CODE") %>'
                                                                            Visible="true"></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="เอกสาร">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("DOCUMENT_NAME") %>'
                                                                            Visible="true"></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ประเภทผู้ใช้">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMember" runat="server" Text='<%# Bind("MEMBER_NAME") %>' Visible="true"></asp:Label></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="15%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ต้องทำการแนบเอกสาร">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkDocumentRequire" runat="server" Checked="false" /></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Value" Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblValueGvApprove" runat="server" Text='<%# Bind("DOCUMENT_REQUIRE") %>'></asp:Label></ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="ดำเนินการ">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="hplDeleteApplicantDetail" runat="server" OnClick="hplDeleteApplicantDetail_Click"
                                                                            OnClientClick="return confirmDelete();"><img src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton></ItemTemplate>
                                                                    <ItemStyle CssClass="td-center" Width="10%" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <HeaderStyle BorderStyle="None" />
                                                            <PagerStyle HorizontalAlign="Right" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="text-align: center;">
                                                <asp:Button ID="btnSubmitApplicantDetail" runat="server" CssClass="btn" OnClick="btnSubmitApplicantDetail_Click"
                                                    Text="ตกลง" />
                                                     <asp:Button ID="btnCancelApplicantDetail" runat="server" CssClass="btn"
                                                        OnClick="btnCancelApplicantDetail_Click" OnClientClick="return confirmCancel();"
                                                        Text="ยกเลิก" /></div>
                                            <br />
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                                <div runat="server" id="dvSubmitConfigLicense" style="text-align: center;">
                                    <br />
                                    <asp:Button ID="btnSubmitConfigLicense" runat="server" Text="บันทึก" CssClass="btn"
                                        OnClick="btnSubmitConfigLicense_Click" />
                                     <asp:Button ID="btnCancelConfigLicense" runat="server" CssClass="btn" 
                                        Text="ยกเลิก" onclick="btnCancelConfigLicense_Click" />
                                     <br /><br />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>
