<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RenewLicenseNo.aspx.cs" Inherits="IAS.License.RenewLicenseNo" %>

<%@ Import Namespace="IAS.Utils" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function setdate() {
            $('#txtApproveDate').attr('readOnly', 'true');
            $('#txtExpireDate').attr('readOnly', 'true');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <script type="text/javascript">
        function PopDownload(path) {
            window.open('<%= UrlHelper.Resolve("/UserControl/FileResponse.aspx") %>?req=' + path + '&mode=D');
        }
    </script>
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการขอรับใบอนุญาต" BorderStyle="None" ID="lblTheLicensee" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server" Visible="false" Width="100%">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ค้นหา</div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblSearchLicense" runat="server" Text="วันที่อนุมัติใบอนุญาต :" CssClass="lbl"></asp:Label>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtApproveDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtApproveDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxtApproveDate" PopupButtonID="imgPopup_txtApproveDate"
                                                        runat="server" TargetControlID="txtApproveDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    <asp:Label ID="lblExpireDate" runat="server" Text="ถึง" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left;">
                                                    <asp:TextBox ID="txtExpireDate" runat="server"></asp:TextBox>
                                                    <asp:ImageButton ID="imgPopup_txtExpireDate" ImageUrl="~/Images/calendar.gif" ImageAlign="Bottom"
                                                        runat="server" />
                                                    <ajaxToolkit:CalendarExtender ID="CLDtxtExpireDate" PopupButtonID="imgPopup_txtExpireDate"
                                                        runat="server" TargetControlID="txtExpireDate" Format="dd/MM/yyyy">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="text-align: right;">
                                                    ประเภทใบอนุญาต :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    ชื่อ :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNames" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    นามสกุล :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLastname" runat="server" CssClass="txt"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right;">
                                                    เลขบัตรประชาชน :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIdCardNo" runat="server" CssClass="txt" MaxLength="13"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right;">
                                                    บริษัท :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCompany" runat="server" CssClass="txt" Width="380px"></asp:TextBox>
                                                    <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender2" TargetControlID="txtCompany"
                                                        ServiceMethod="GetAutomCompleteCompany" ServicePath="~/Service/AutoComplete.asmx"
                                                        MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                                        ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </ajaxToolkit:AutoCompleteExtender>
                                                    <asp:Image ID="imgS" runat="server" ImageUrl="~/Images/find_search.png" ToolTip="พิมพ์ชื่อเพื่อทำการค้นหา" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" OnClick="btnSearch_Click" />
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancle_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clear">
                        </div>
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            Please wait....</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                        <div class="box_body_content">
                            <div class="box_t_h">
                            </div>
                            <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center;">
                                                <asp:GridView ID="gvSearch" runat="server" Width="100%" ShowHeaderWhenEmpty="True"
                                                    CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True"
                                                    AutoGenerateColumns="False">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center">
                                                            ไม่พบข้อมูล...</div>
                                                    </EmptyDataTemplate>
                                                    <PagerStyle HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ลำดับ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOOO" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="50%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTransectionDateGv" runat="server" Text='<%# Bind("RenewDate","{0:dd/MM/yyyy hh:mm:ss}") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดาวน์โหลด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="50%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload" OnClick="loadFile_Click"><img src="../Images/download-icon.gif" title="ดาวน์โหลด" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div id="ctrGridView" runat="server">
                                        <asp:GridView ID="gvLicenseDetail" runat="server" Width="100%" ShowHeaderWhenEmpty="true"
                                            CssClass="datatable" AutoGenerateColumns="False" OnRowDataBound="gvLicenseDetail_RowDataBound">
                                            <AlternatingRowStyle CssClass="altrow" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-CssClass="td-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="headSelected" runat="server" OnCheckedChanged="headSelected_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="itemSelected" runat="server" OnCheckedChanged="itemSelected_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ลำดับ" ItemStyle-CssClass="td-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NUMROW") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UPLOAD_GROUP_NO" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUploadGroupNo" runat="server" Text='<%# Bind("UPLOAD_GROUP_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="เลขบัตรประชาชน" ItemStyle-CssClass="td-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdCardNo" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Names" runat="server" Text='<%# Bind("NAMES") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="นามสกุล">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lastname" runat="server" Text='<%# Bind("LASTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ประเภทคำขอ" ItemStyle-CssClass="td-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPetitionTypeName" runat="server" Text='<%# Bind("PETITION_TYPE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ประเภทใบอนุญาต" ItemStyle-CssClass="td-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLicenseTypeName" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="วันที่อนุมัติใบอนุญาต">
                                                    <ItemTemplate>
                                                        <asp:Label ID="OicApprovedDate" runat="server" Text='<%# Bind("OIC_APPROVED_DATE","{0:dd/MM/yyyy hh:mm:ss}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="บริษัท">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompName" runat="server" Text='<%# Bind("COMP_NAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="สถานะ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIsDownload" runat="server" Text='<%# Bind("IS_DOWNLOAD") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblStatusDownload" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <center>
                                                    ไม่พบข้อมูล...</center>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <br />
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
                                        <br />
                                        <center>
                                            <asp:Button ID="btnDownloadZip" runat="server" Text="ดาวน์โหลด" CssClass="btn" OnClick="btnDownloadZip_Click" /></center>
                                        <br />
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <uc2:UCModalError ID="UCModalError" runat="server" />
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnDownloadZip" />
                    </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
