<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="ImportCandidateDelay.aspx.cs" Inherits="IAS.Applicant.ImportCandidateDelay" %>

<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagPrefix="uc" TagName="UCModalSuccess" %>
<%@ Register Src="~/UserControl/UCModalError.ascx" TagPrefix="uc" TagName="UCModalError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmCancel() {
            return confirm('คุณต้องการปิดหรือไม่?');
        }

        function confirmDelete() {
            return confirm('คุณต้องการลบหรือไม่?');
        }

        $(function setdate() {
            $('#txtDateExam').attr('readOnly', 'true');
        });

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="การกำหนดสิทธิ์การนำเข้าผู้สมัครสอบล่าช้า" BorderStyle="None" ID="lblManageAssociation"
                    runat="server" />
            </div>
        </div>
        <asp:UpdatePanel ID="uplConditionDelay" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <br />
                <br />
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::การกำหนดสิทธิ์การนำเข้าผู้สมัครสอบล่าช้า</div>
                    <table width="100%">
                        <tr>
                            <td style="width: 3%">
                            </td>
                            <td style="width: 92%">
                                <div class="box_textbox_bgadd" style="width: 100%">
                                    <fieldset>
                                        กรุณาเลือก
                                        <table width="100%">
                                            <tr>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Label ID="lbl1" Text="รหัสรอบสอบ" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtTestingNo" runat="server" CssClass="textbox" MaxLength="6"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Label ID="Label1" Text="วันที่สอบ" runat="server"></asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtDateExam" runat="server" CssClass="txt" AutoPostBack="true" onkeydown="return false;"
                                                        Width="130px"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtDateExam" ImageUrl="~/Images/calendar.gif"
                                                        ImageAlign="Bottom" runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup_txtDateExam" runat="server" TargetControlID="txtDateExam"
                                                        Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="btnClear" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnClear_Click" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </td>
                            <td style="width: 5%">
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td width="95%" align="center">
                                <asp:GridView ID="gvCandidateDelay" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"
                                    CssClass="datatable" Width="93%" OnRowDataBound="gvCandidateDelay_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ลำดับที่">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="td-center" Width="5%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                รหัสรอบสอบ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTestingNo" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" CssClass="td-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                รหัสสนามสอบ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                วันที่สอบ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompType" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                สิทธิ์การนำเข้า</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrivilegeStatus" runat="server" Text='<%# Bind("PRIVILEGE_STATUS") %>'
                                                    Visible="false"></asp:Label>
                                                <asp:Label ID="lblPrivilegeStatusName" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                ดำเนินการ</HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnConfig" runat="server" Text="Delete" OnClientClick="return confirm('ยืนยันการกำหนดข้อมูล?')"
                                                    OnClick="lbtnConfig_Click"><img src="../Images/edit-account.png" alt="" /></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" CssClass="td-center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>
                                            ไม่พบข้อมูล</center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <br />
                                <div id="ctrPage" runat="server" style="text-align: left; width: 93%;">
                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="true" OnClick="btnPreviousGvSearch_Click" />
                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="true" Enabled="false"
                                        Text="1"></asp:TextBox>
                                    <asp:Label ID="lblParaPage" runat="server" Text="/" Visible="true"></asp:Label>
                                    <asp:Label ID="txtTotalPage" runat="server" Visible="true"></asp:Label>
                                    <asp:Button ID="btnNextGvSearch" runat="server" Text="&gt;" Visible="true" OnClick="btnNextGvSearch_Click" />
                                    &nbsp;&nbsp;&nbsp; <span id="spanRowsPerPage" runat="server">
                                        <asp:Label ID="lblRowsPerPage" runat="server" Text="จำนวนรายการต่อหน้า "></asp:Label>
                                        <asp:TextBox ID="txtRowsPerpage" runat="server" Style="text-align: center" CssClass="txt"
                                            MaxLength="4" onkeypress="return runScript(event)" onpaste="return false;" AutoComplete="Off"
                                            ondrop="return false;" Width="5%"></asp:TextBox>&nbsp;
                                        <asp:Button ID="pageGo" runat="server" Text="GO" OnClick="pageGo_Click" />
                                    </span><span style="float: right; margin-right: 20px;">
                                        <asp:Label ID="lblTotalRows" runat="server"></asp:Label>
                                    </span>
                                </div>
                                <div class="clear">
                                </div>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UplPopUp" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="display: none">
                    <asp:Button ID="btnModal" runat="server" Text="Open" />
                </div>
                <ajaxToolkit:ModalPopupExtender ID="PopUpLicense" runat="server" CancelControlID="btnCancel"
                    OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                    Drag="true" BackgroundCssClass="modalbackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlModal" Style="display: block; width: 550px; z-index: 99990 !important;"
                    runat="server" CssClass="modalpopup">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::กำหนดสิทธิ์</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <table>
                                    <tr>
                                        <td width="150px">
                                            รหัสสนามสอบ
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSetTestingNo" runat="server" CssClass="textbox" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="hdfExamPlaceCode" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            สิทธิ์การโหลดข้อมูล่าช้า
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="rblSetImport" runat="server" CssClass="rb" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="เปิดสิทธิ์การนำเข้า" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="ปิดสิทธิ์การนำเข้า" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;<br />
                                <center>
                                    <asp:Button ID="btnSave" runat="server" Text="บันทึก" OnClick="btnSave_Click" ValidationGroup="1"
                                        CssClass="btn" />
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                                </center>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <uc:UCModalError runat="server" ID="UCError" />
                <uc:UCModalSuccess runat="server" ID="UCSuccess" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
