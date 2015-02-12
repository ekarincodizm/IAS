<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="LicenseGroup.aspx.cs" Inherits="IAS.License.LicenseGroup" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/UCModalWarning.ascx" TagName="UCModalWarning" TagPrefix="uc5" %>
<%--<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl"
    TagPrefix="uc1" %>--%>
<%@ Register Src="~/UserControl/ucLicenseDetail.ascx" TagName="UCLicenseDetail" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showload() {
            //document.getElementById('Detail_UCLoadingProgress1_Image1').src = '~/Images/newloading23.gif';
            //$find('Detail_UCLoadingProgress_mpeProgress').show();
            document.getElementById('ucLoad_Image1').src = '../Images/newloading23.gif';
            if (document.getElementById('ucLoad_Image1').src != null) {
                $find('ucLoad_mpeProgress').show();
            }            
        }

        function confirmSave() {
            return confirm('คุณต้องการขอรับใบอนุญาตใช่หรือไม่');
        }
        function CancleSave() {
            return confirm('คุณต้องการยกเลิกการนำเข้าข้อมูลขอรับใบอนุญาตใช่หรือไม่');
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ข้อมูลการขอรับใบอนุญาต" BorderStyle="None" ID="lblTheLicensee" runat="server" />
            </div>
        </div>
        <div>
            <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
                <table style="width: 100%;">
                </table>
                <asp:UpdatePanel ID="UplSearch" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="PnlSearch" runat="server" Visible="true" Width="100%">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                </div>
                                <div class="box_textbox_bgadd">
                                    <div class="box_texbox_add_box">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="text-align: right; width:10%;white-space: nowrap;">
                                                    <span>
                                                        <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต :" AutoPostBack="True"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;width:20%">
                                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl" AutoPostBack="True"
                                                        Width="200px" OnSelectedIndexChanged="ddlLicenseType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="ObtainLicenseTypeRequired" runat="server" ControlToValidate="ddlObtainLicenseType"
                                        CssClass="failureNotification" ValidationGroup="LicenseGroupValidationGroup">*</asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td style="text-align: right;width:15%;white-space: nowrap;" >
                                                    <asp:Label ID="lblObtainLicenseType" runat="server" CssClass="lbl" Text="ประเภทขอรับใบอนุญาต :"></asp:Label>
                                                </td>
                                                <td style="text-align: left;width:20%">
                                                    <asp:DropDownList ID="ddlObtainLicenseType" runat="server" CssClass="ddl" AutoPostBack="true"
                                                        Width="200px " OnSelectedIndexChanged="ddlObtainLicenseType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="LicenseTypeRequired" runat="server" ControlToValidate="ddlLicenseType"
                                        CssClass="failureNotification" ValidationGroup="LicenseGroupValidationGroup">*</asp:RequiredFieldValidator>--%>
                                                </td>
                                                <td style="text-align: right;width:8%;white-space: nowrap;">
                                                    <span>
                                                        <asp:Label ID="lblOrder" runat="server" CssClass="lbl" Text="ไฟล์ข้อมูล :"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;width:35%;" >
                                                    <asp:FileUpload ID="FuFile" runat="server" CssClass="txt" Height="23px"/>
                                                    &nbsp
                                                    <%-- <ajaxToolkit:AsyncFileUpload OnClientUploadError="uploadError" OnClientUploadComplete="uploadComplete"
                                        runat="server" ID="FuFile" Width="400px" UploaderStyle="Modern" UploadingBackColor="#CCFFFF"
                                        ThrobberID="myThrobber" style="margin-left:0; margin-right:0;"/>--%>
                                                    <%--<asp:RequiredFieldValidator ID="FuFileRequired" runat="server" ControlToValidate="FuFile"
                                        CssClass="failureNotification" ValidationGroup="LicenseGroupValidationGroup">*</asp:RequiredFieldValidator>--%>
                                                    <asp:Button ID="btnLoadFile" runat="server" CssClass="btn" Text="โหลดข้อมูล" OnClientClick="showload();"
                                                        OnClick="btnLoadFile_Click" />
                                                    <%--     <asp:Button ID="Button1" runat="server" CssClass="btn" Text="โหลดข้อมูล" OnClick="btnLoadFile_Click" 
                                     ValidationGroup="LicenseGroupValidationGroup"/>--%>
                                                </td>
                                            </tr>
                                            <%--  <tr>
                                                <td style="text-align: right;" class="style1">
                                                    <asp:Label ID="lblApproveby" runat="server" CssClass="lbl" Text="ผู้ตรวจเอกสาร"></asp:Label>
                                                    :</span>
                                                </td>
                                                <td style="text-align: left;" class="style2">
                                                    <asp:DropDownList ID="ddlApproveby" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td style="text-align: right;width:10%;white-space: nowrap;">
                                                    <%--<asp:ValidationSummary ID="licenseGroupValidationSummary" runat="server" CssClass="failureNotification"
                                        HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                        ValidationGroup="LicenseGroupValidationGroup" />--%>
                                                    <span>
                                                        <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="ผู้อนุมัติ :" AutoPostBack="True"></asp:Label>
                                                    </span>
                                                </td>
                                                <td style="text-align: left;width:20%" >
                                                    <asp:DropDownList ID="ddlApproveBy" runat="server" CssClass="ddl" Width="200px ">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right; width:15%;white-space: nowrap;">
                                                    <asp:Label ID="lblReplaceType" runat="server" CssClass="lbl" Text="ประเภทใบแทนใบอนุญาต  :"
                                                        Visible="false"></asp:Label>
                                                </td>
                                                <td style="text-align: left; margin-left:0;width:20%;white-space: nowrap;">
                                                    <asp:RadioButtonList ID="rblType" runat="server" AutoPostBack="True" CssClass="rb" style="margin-left:0"
                                                        Visible="false" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="ชำรุดสูญหาย" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="เปลี่ยนชื่อ-สกุล" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnLoadFile" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdateImportFile" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlImportFile" runat="server" Visible="false">
                            <div class="box_body_content">
                                <div class="box_t_h">
                                    ::ผลการนำเข้า</div>
                                <div class="box_textbox_bg_table">
                                    <div class="box_texbox_add_box">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:GridView ID="gvImportFile" runat="server" AllowPaging="false" AllowSorting="True"
                                                        ShowHeaderWhenEmpty="True" CssClass="datatable" CellSpacing="-1" GridLines="None"
                                                        BorderWidth="1" AutoGenerateColumns="False" CaptionAlign="Top" CellPadding="3"
                                                        EnableTheming="True" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                                                        PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10"
                                                        PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager" PagerStyle-HorizontalAlign="Center"
                                                        PageSize="12" Width="100%" OnRowDataBound="gvImportFile_RowDataBound">
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                        <PagerStyle HorizontalAlign="Center" />
                                                        <HeaderStyle Font-Bold="True" />
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ลำดับที่">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ชื่อไฟล์" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="30%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFileNameGv" runat="server" Text='<%#  Eval("FileName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รายการทั้งหมด" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("Totals") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รายการที่ถูกต้อง" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="15%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("RightTrans") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" ItemStyle-HorizontalAlign="Center"
                                                                Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("MissingTrans") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItemsIncorrectRemark" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnPreviousGvSearch" runat="server" Text="<" Visible="false" OnClick="btnPreviousGvSearch_Click" />
                                                    <asp:TextBox ID="txtNumberGvSearch" runat="server" Width="15px" Visible="false" Text="1"></asp:TextBox>
                                                    <asp:Button ID="btnNextGvSearch" runat="server" Text=">" Visible="false" OnClick="btnNextGvSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div style="width: 100%;">
                                <div class="box_body_content">
                                    <div class="box_t_h">
                                        ::ตรวจสอบรายการ</div>
                                    <div class="box_textbox_bg_table">
                                        <div class="box_texbox_add_box">
                                            <asp:GridView ID="gvCheckList" runat="server" Width="100%" EnableTheming="True" AutoGenerateColumns="False"
                                                CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1" CaptionAlign="Top"
                                                ShowHeaderWhenEmpty="True" CellPadding="3" OnRowDataBound="gvCheckList_RowDataBound"
                                                PagerStyle-HorizontalAlign="Center" OnSelectedIndexChanged="gvCheckList_SelectedIndexChanged">
                                                <AlternatingRowStyle CssClass="altrow" />
                                                <%-- <SelectedRowStyle BackColor="#f7fe8d" />--%>
                                                <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ลำดับที่">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="td-center" Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขบัตรประชาชน" ItemStyle-HorizontalAlign="Center"
                                                        Visible="true">
                                                        <ItemStyle Width="80" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Eval("CITIZEN_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ชื่อ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemStyle Width="80" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("NAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="นามสกุล" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemStyle Width="80" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("SURNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เลขที่ใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                                        Visible="true">
                                                        <ItemStyle Width="60" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLicenseNo" runat="server" Text='<%# Eval("LICENSE_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันที่ออกใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                                        Visible="true">
                                                        <ItemStyle Width="60" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLicenseDate" runat="server" Text='<%# Eval("LICENSE_ACTIVE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="วันหมดอายุ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemStyle Width="60" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLicenseExpireDate" runat="server" Text='<%# Eval("LICENSE_EXPIRE_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="คุณสมบัติ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemStyle Width="20" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="hplViewProp" runat="server"  CommandName="Select"
                                                                Visible="true" Text="ผ่าน" title="ดูคุณสมบัตผู้ขอรับใบอนุญาต"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemStyle Width="20" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="hplview" runat="server" OnClick="hplView_Click" Visible="true" title="ดูประวัติผู้ขอรับใบอนุญาต">แสดง</asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="สาเหตุ" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="150" />
                                                        <ItemTemplate>
                                                            <%--          <div id="dvErr" style="background-color: transparent; overflow: hidden; height: auto" >
                                                                 <%# Eval("ERR_MSG") %></div>--%>
                                                            <asp:Label ID="lblErrGv" Style="background-color: transparent; overflow: hidden;
                                                                height: auto" runat="server" Text='<%# Eval("ERR_MSG") %>'></asp:Label>
                                                            <%--                <asp:TextBox ID="txtDetailGv" runat="server" Text='<%# Eval("ERR_MSG") %>' 
                                                                BorderStyle="None" Width="100%" Height="100%" ReadOnly="True" Style="background-color: transparent; overflow:hidden;height:auto "></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SEQ_NO" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemStyle Width="20" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSeqNoGv" runat="server" Text='<%# Eval("SEQ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GROUP_ID" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemStyle Width="30" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGroupID" runat="server" Text='<%# Eval("IMPORT_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="เอกสารแนบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                        <ItemStyle Width="20" CssClass="td-center" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="hplViewImg" runat="server" OnClick="hplViewImg_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียดเอกสารแนบ" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: center;" align="center">
                                    <br />
                                    <div>
                                        <asp:Label runat="server" ID="lblMessageError" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div style="text-align: center;" align="center">
                                        <asp:Panel runat="server" ID="pnlCondition">
                                            <table style="width: 100%; min-width: 100%" align="center">
                                                <tr>
                                                    <td style="text-align: right; width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right; width: 80%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right; width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 80%; text-align: center">
                                                        <table style="width: 100%; min-width: 100%; border-width: 1px; border-color: #bebebe;
                                                            border-style: solid">
                                                            <tr>
                                                                <td style="text-align: center; width: 100%; min-width: 100%" colspan="2">
                                                                    <asp:Label ID="lblDetailHeader" runat="server" Text="เงื่อนไขการขอรับใบอนุญาต"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;" colspan="2">
                                                                    <asp:Label ID="lblDetailNumber1" runat="server" Text="1" CssClass="lbl"></asp:Label>
                                                                    <span>.</span>
                                                                    <asp:Label ID="lblDetail1" runat="server" Text="ผู้ขอรับใบอนุญาต ต้องไม่เป็นคนวิกลจริตหรือจิตฟั่นเฟือนไม่สมประกอบ"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;" colspan="2">
                                                                    <asp:Label ID="lblDetailNumber2" runat="server" Text="2" CssClass="lbl"></asp:Label>
                                                                    <span>.</span>
                                                                    <asp:Label ID="lblDetail2" runat="server" Text="ผู้ขอรับใบอนุญาต ต้องไม่เคยต้องโทษจำคุกโดยคำพิพากษาถึงที่สุดให้จำคุกในความผิดเกี่ยวกับทรัพย์ที่กระทำโดยทุจริต เว้นแต่พ้นโทษมาแล้วไม่น้อยกว่าห้าปีก่อนวันขอรับใบอนุญาต"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;" colspan="2">
                                                                    <asp:Label ID="lblDetailNumber3" runat="server" Text="3" CssClass="lbl"></asp:Label>
                                                                    <span>.</span>
                                                                    <asp:Label ID="lblDetail3" runat="server" Text="ผู้ขอรับใบอนุญาต ต้องไม่เป็นบุคคลล้มละลาย"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left;" colspan="2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="text-align: right; width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right; width: 80%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right; width: 10%;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: right; width: 20%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: right; width: 30%;">
                                                        &nbsp;<asp:CheckBox ID="chkComfirm" runat="server" Text="ข้าพเจ้ายอมรับเงื่อนไขทุกประการ" />
                                                    </td>
                                                    <td style="text-align: right; width: 20%;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="text-align: left; width: 30%;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="text-align: center;">
                                                <asp:Button runat="server" ID="btnValidateProp" CssClass="btn" Text="ยืนยัน" OnClick="btnValidateProp_Click" />
                                                &nbsp;
                                                <asp:Button runat="server" ID="btnImport" CssClass="btn" Text="นำเข้าระบบ" OnClick="btnImport_Click"
                                                    Visible="false" OnClientClick="return confirmSave();" />
                                                &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิกการนำเข้า" OnClick="btnCancle_Click"
                                                    OnClientClick="return CancleSave();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                &nbsp;
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div style="width: 550;">
                                <asp:Button runat="server" ID="btn_pop2" Style="display: none" />
                                <ajaxToolKit:ModalPopupExtender ID="mdpAttachFilePop" runat="server" TargetControlID="btn_pop2"
                                    PopupControlID="pnlAtfile" BackgroundCssClass="modalbackground" 
                                    OkControlID="OkButton2" OnOkScript="onOk2()" CancelControlID="btnCanelPopup2">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnlAtfile" runat="server" CssClass="modalpopup" style="display:none;">
                                    <table style="width: 100%;">
                                        <tr style="width: 100%;">
                                            <td style="width: 5%;">
                                                &nbsp
                                            </td>
                                            <td style="width: 90%;">
                                                <div class="box_t_h">
                                                    ::เอกสารแนบ
                                                </div>
                                                <div class="box_textbox_bg_table" style="width: 100%">
                                                    <div class="box_texbox_add_box">
                                                        <table style="width: 100%;">
                                                            <tr align="center">
                                                                <td style="text-align: center;" colspan="3">
                                                                    <asp:GridView ID="gvUpload" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                                        ShowHeaderWhenEmpty="true" CssClass="datatable" CellSpacing="-1" GridLines="None"
                                                                        BorderWidth="1" EnableTheming="True" OnRowDataBound="gvUpload_RowDataBound" Width="100%">
                                                                        <AlternatingRowStyle CssClass="altrow" />
                                                                        <HeaderStyle BorderStyle="None" />
                                                                        <PagerStyle HorizontalAlign="Right" />
                                                                        <EmptyDataTemplate>
                                                                            <div style="text-align: center; margin-left: 0; margin-right: 0">
                                                                                ไม่พบข้อมูล
                                                                            </div>
                                                                        </EmptyDataTemplate>
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="ลำดับที่">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex + 1 %>
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="td-center" Width="5%" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ประเภทไฟล์แนบ" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                                                <ItemStyle Width="40%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTypeGv" runat="server" Text='<%# Bind("FileType") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ไฟล์แนบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="30%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFileGv" runat="server" Text='<%# Bind("FileName") %>' Visible="true"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="รายละเอียด" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                                <ItemStyle Width="30%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="hplViewDetailImg" runat="server" OnClick="hplViewDetailImg_Click"><img src="../Images/view-icon.png" title="ดูเอกสาร" /></asp:LinkButton>
                                                                                    <%--   <asp:LinkButton ID="hplDelete" runat="server" OnClick="hplDelete_Click" OnClientClick="return confirmDelete();">ลบ</asp:LinkButton>--%>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="TempFilePath" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                                <ItemStyle Width="30%" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTempFilePath" runat="server" Text='<%# Bind("MapFileName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ATTACH_FILE_PATH" ItemStyle-HorizontalAlign="Center"
                                                                                Visible="false">
                                                                                <ItemStyle Width="30%" CssClass="td-center" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAttachFilePathGv" runat="server" Text='<%# Bind("MapFileName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div style="text-align: center;" align="center">
                                                        <br />
                                                        <asp:Button runat="server" ID="btnCanelPopup2" Text="ปิด" />
                                                    </div>
                                                    <div>
                                                        &nbsp
                                                    </div>
                                                </div>
                                            </td>
                                            <td style="width: 5%;">
                                                &nbsp
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                            <div style="width: 900px;">
                                <asp:Button runat="server" ID="Open_P" Style="display: none" />
                                <ajaxToolKit:ModalPopupExtender ID="mdpValidateprop" runat="server" TargetControlID="Open_P"
                                    PopupControlID="pnlValidateprop" BackgroundCssClass="modalbackground" 
                                    OkControlID="OkButton" OnOkScript="onOk()" CancelControlID="btnCloseProp">
                                </ajaxToolKit:ModalPopupExtender>
                                <asp:Panel ID="pnlValidateprop" runat="server" CssClass="modalpopup"  Style="display: none" >
                                    <div style="width: 100%;">
                                        <div class="box_body_content">
                                            <div class="box_t_h" style="width: 800;">
                                                ::ตรวจสอบคุณสมบัติ
                                            </div>
                                            <br />
                                            <div class="box_t_h" style="text-align: right; float: right; padding-right: 30px;">
                                                *หมายเหตุ
                                                <asp:Image ID="Image1" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                    Height="16px" />&nbsp ผ่าน&nbsp
                                                <asp:Image ID="Image2" runat="server" ImageUrl="../Images/discorrect.png" Width="15px"
                                                    Height="16px" />&nbsp ไม่ผ่าน
                                            </div>
                                            <div class="box_textbox_bg_table">
                                                <div class="box_texbox_add_box">
                                                    <asp:GridView ID="gvGeneral" runat="server" EnableTheming="True" AutoGenerateColumns="False"
                                                        CssClass="datatableRow" CellSpacing="-1" GridLines="None" BorderWidth="1" CaptionAlign="Top"
                                                        PagerStyle-HorizontalAlign="Center" OnRowCreated="Head_RowCreated">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="คุณสมบัติทั่วไป" HeaderStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="50%" CssClass="td-left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                        Height="15px" Visible="false" />
                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                        Width="15px" Height="15px" Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle Wrap="true" />
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvExamResult" runat="server" Width="100%" CssClass="datatableRow"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowCreated="Head_RowCreated">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ผลสอบ" HeaderStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  Visible="true">
                                                                <ItemStyle Width="50%" CssClass="td-left" VerticalAlign="Top"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                        Height="15px" Visible="false" />
                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                        Width="15px" Height="15px" Visible="false" />
                                                                    <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" VerticalAlign="Top" CssClass="td-left"/>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDef" Style="background-color: transparent; overflow: hidden; height: auto"
                                                                        runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    </asp:GridView>
                                                    <%--<asp:GridView ID="gvEducation" runat="server" Width="100%" CssClass="datatableRow"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowCreated="Head_RowCreated">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="คุณวุฒิการศึกษา" HeaderStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="50%" CssClass="td-left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                        Height="15px" Visible="false" />
                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                        Width="15px" Height="15px" Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDef" runat="server" Text="เมื่อมีวุติการศึกษาตรงตามเงื่อนไข สามารถแทนผลสอบได้"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-left" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    </asp:GridView>--%>
                                                    <asp:GridView ID="gvTrainResult" runat="server" Width="100%" CssClass="datatableRow"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowCreated="Head_RowCreated">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ผลอบรม" HeaderStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="50%" CssClass="td-left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("ITEM") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                        Height="15px" Visible="false" />
                                                                    <asp:Image ID="impdisCorrect" runat="server" ImageUrl="../Images/discorrect.png"
                                                                        Width="15px" Height="15px" Visible="false" />
                                                                    <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ" Visible="false"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvExamSpecial" runat="server" Width="100%" CssClass="datatableRow"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowCreated="Head_RowCreated">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="คุณสมบัติพิเศษลดชั่วโมงการสอบ" HeaderStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="50%" CssClass="td-left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ"></asp:Label>
                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                        Height="15px" Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvTrainSpecial" runat="server" Width="100%" CssClass="datatableRow"
                                                        CellSpacing="-1" GridLines="None" BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False"
                                                        OnRowCreated="Head_RowCreated">
                                                        <AlternatingRowStyle CssClass="altrow" />
                                                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                        <PagerStyle HorizontalAlign="Left" BackColor="#99CCCC" ForeColor="#003399" />
                                                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                            NextPageText="&gt;" PreviousPageText="&lt;" />
                                                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="คุณสมบัติพิเศษลดชั่วโมงการอบรม" HeaderStyle-Wrap="true">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex + 1 %>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="td-center" Width="10%" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="50%" CssClass="td-left" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                     <asp:Label ID="lblwait" runat="server" Text="รอการตรวจสอบ"></asp:Label>
                                                                    <asp:Image ID="impCorrect" runat="server" ImageUrl="../Images/correct.png" Width="15px"
                                                                        Height="15px" Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                                <ItemStyle Width="20%" CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDef" runat="server" Text=''></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle CssClass="td-center" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                        <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                        <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    </asp:GridView>
                                                </div>
                                                <div style="text-align: center;" align="center">
                                                <div class="box_body_content">
                                                   <br />
                                                </div>
                                                 <div>
                                                    <asp:Button runat="server" ID="btnCloseProp" Text="ปิด" />
                                                    </div>
                                                  <div>
                                                   <br />
                                                </div>
                                                 </div>

                                            </div>
                                            
                                            
                                        </div>
                            
                                    </div>
                                </asp:Panel>
                            </div>
                        </asp:Panel>
                        <uc2:UCModalError ID="UCModalError" runat="server" style="display: none" />
                        <uc5:UCModalWarning ID="UCModalWarning" runat="server" style="display: none"/>
                        <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" style="display: none" />
                        <uc4:UCLicenseDetail ID="ucLicenseDetail" runat="server" style="display: none" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>
