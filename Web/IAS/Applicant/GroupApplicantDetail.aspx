<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="GroupApplicantDetail.aspx.cs" Inherits="IAS.Exam.GroupApplicantDetail" %>

<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/ucPersonalApplicantDetail.ascx" TagName="ucPersonalApplicantDetail"
    TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmImport() {
            return confirm('คุณต้องการนำเข้าระบบหรือไม่?');
        }

        function showload() {
            document.getElementById('ucLoad_Image1').src = '../Images/newloading23.gif';
            if (document.getElementById('ucLoad_Image1').src != null) {
                $find('ucLoad_mpeProgress').show();
            }
        }

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                    {
                        e.returnValue = false;
                        return false;
                    }
                    else//Firefox
                        e.preventDefault();
                }
            }
        }

        function checkMaxLength(texbox) {
            var textvalue = texbox.value;

            if (textvalue.length > 100) {
                return textvalue.substring(0, 100);
            }
            else {
                return texbox.value;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header" style="height: 50px;">
            <div class="box_content_header_text">
                <asp:Label Text="สมัครสอบแบบกลุ่ม" BorderStyle="None" ID="lblDetailExamGroup" runat="server" />
            </div>
            <div>
                <div style="text-align: right;">
                    <asp:LinkButton ID="linkApplicantFile" runat="server">
                        <asp:Image runat="server" ID="linkImageCSV" ImageUrl="~/image/csv-1.png" Width="30px"
                            Height="35px" />
                    </asp:LinkButton></div>
                <div style="text-align: right;">
                    <asp:Label Text="ดาวน์โหลดไฟล์ตัวอย่าง" BorderStyle="None" ID="lblDownload" runat="server"
                        Font-Bold="true" ForeColor="White" /></div>
            </div>
        </div>
        <asp:UpdatePanel ID="upn" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="box_body_content">
                    <div class="box_t_h">
                        ::รหัสสอบ</div>
                    <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: right; width: 35%;">
                                        <span>
                                            <asp:Label ID="lblExamNumber" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
                                            :</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtExamNumber" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                        <asp:Button ID="btnClear" runat="server" CssClass="btn" Text="ล้างข้อมูล" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="clear: left">
                </div>
                <div id="divSubMain" style="visibility: visible">
                    <div style="width: 90%; margin: 0px auto 10px auto;" align="center">
                        <div id="divGroup" runat="server">
                            <fieldset style="width: 80%; text-align: center">
                                <legend class="table">นำเข้าผู้สมัครสอบรายกลุ่ม</legend>
                                <table style="width: 100%; text-align: center;">
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblFileName0" runat="server" Text="ประเภทไฟล์นำเข้า :" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:DropDownList ID="ddlTypeFile" runat="server" CssClass="ddl" Width="150px" AutoPostBack="True">
                                                <asp:ListItem Value="0">CSV</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="lblFileName" runat="server" Text="ไฟล์ข้อมูล :" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:FileUpload ID="FuFile" runat="server" CssClass="txt" Height="20px" Width="300px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; width: 100%;" colspan="2">
                                            <asp:Button ID="btnLoadFile" runat="server" Text="โหลดข้อมูล" CssClass="btn" OnClick="btnLoadFile_Click"
                                                OnClientClick="showload();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <%--   <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upn">
                                                <ProgressTemplate>
                                                    Please wait image uploaded....</ProgressTemplate>
                                            </asp:UpdateProgress>--%>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                        <div id="divSingle" runat="server">
                            <fieldset style="width: 80%; text-align: center">
                                <legend>นำเข้าผู้สมัครสอบรายเดี่ยว</legend>
                                <table style="width: 100%; text-align: center;">
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label1" runat="server" CssClass="lbl" Text="รหัสบัตรประชาชน :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt" MaxLength="13"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtIDCard"
                                                CssClass="failureNotification" ValidationGroup="loadSingleValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label3" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:DropDownList ID="ddlTitle" runat="server" AutoPostBack="True" CssClass="ddl"
                                                Width="100px" OnSelectedIndexChanged="ddlTitle_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlTitle"
                                                CssClass="failureNotification" ValidationGroup="loadSingleValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:Label ID="Label4" runat="server" CssClass="lbl" Text="ชื่อ :"></asp:Label>
                                            <asp:TextBox ID="txtFirstNane" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtFirstNane"
                                                CssClass="failureNotification" ValidationGroup="loadSingleValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:Label ID="Label5" runat="server" CssClass="lbl" Text="นามสกุล :"></asp:Label>
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLastName"
                                                CssClass="failureNotification" ValidationGroup="loadSingleValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label6" runat="server" CssClass="lbl" Text="วันเกิด :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:TextBox ID="txtBirthDay" runat="server"></asp:TextBox>
                                            <asp:ImageButton ID="imgPopup_txtBirthDay" ImageUrl="~/Images/calendar.gif"
                                                ImageAlign="Bottom" runat="server" />
                                            <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" PopupButtonID="imgPopup_txtBirthDay" runat="server" TargetControlID="txtBirthDay"
                                                Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBirthDay"
                                                CssClass="failureNotification" ValidationGroup="loadSingleValidationGroup">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label7" runat="server" CssClass="lbl" Text="เพศ :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:RadioButtonList ID="rblSex" runat="server" CssClass="rb" Enabled="false" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="ชาย" Value="ช"></asp:ListItem>
                                                <asp:ListItem Text="หญิง" Value="ญ"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label2" runat="server" CssClass="lbl" Text="ระดับการศึกษา :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <span>
                                                <asp:DropDownList ID="ddlEducation" runat="server" AutoPostBack="True" CssClass="ddl"
                                                    Width="150px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEducation"
                                                    CssClass="failureNotification" ValidationGroup="loadSingleValidationGroup">*</asp:RequiredFieldValidator>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label8" runat="server" CssClass="lbl" Text="รหัสบริษัท :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <span>
                                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" CssClass="ddl"
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label11" runat="server" CssClass="lbl" Text="ที่อยู่ :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:TextBox ID="txtAddress" runat="server" Columns="10" CssClass="txt" Height="50px"
                                                MaxLength="100" Rows="10" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right; width: 30%;">
                                            <asp:Label ID="Label9" runat="server" CssClass="lbl" Text="รหัสพื้นที่ :"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 70%;">
                                            <asp:TextBox ID="txtAreaCode" runat="server" CssClass="txt" Width="100px" MaxLength="8"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; width: 100%;" colspan="2">
                                            <asp:Button ID="btnLoadFileSingle" runat="server" CssClass="btn" OnClick="btnLoadFileSingle_Click"
                                                Text="โหลดข้อมูล" ValidationGroup="loadSingleValidationGroup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center; width: 100%;">
                                            <asp:ValidationSummary ID="loadSingleValidationSummary" runat="server" CssClass="failureNotification"
                                                HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                                ValidationGroup="loadSingleValidationGroup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--  <td>
                                            <uc1:UCLoadingProgress ID="UCLoadingProgress2" runat="server" />
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upn">
                                                <ProgressTemplate>
                                                    Please wait image uploaded....</ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>--%>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </div>
                    <asp:Panel ID="pnlImportFile" runat="server" Visible="false">
                        <div class="box_body_content">
                            <div class="box_t_h">
                                ::ผลการนำเข้า</div>
                            <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;" colspan="4">
                                                <asp:GridView ID="gvImportFile" runat="server" CaptionAlign="Top" Width="100%" AllowPaging="True"
                                                    ShowHeaderWhenEmpty="True" EnableTheming="True" PageSize="12" PagerSettings-Mode="NumericFirstLast"
                                                    PagerSettings-NextPageText=">" CssClass="datatable" CellSpacing="-1" GridLines="None"
                                                    BorderWidth="1" PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last"
                                                    PagerSettings-FirstPageText="First" AllowSorting="True" PagerSettings-PageButtonCount="10"
                                                    PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False" PagerStyle-CssClass="viewpager"
                                                    OnRowDataBound="gvImportFile_RowDataBound">
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                    <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                                    <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ลำดับที่">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="td-center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อไฟล์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFileNameGv" runat="server" Text='<%# Eval("UploadFileName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รายการทั้งหมด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblViewAllGv" runat="server" Text='<%# Eval("Totals") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รายการที่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemsCorrectGv" runat="server" Text='<%# Eval("RightTrans") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รายการที่ไม่ถูกต้อง" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemsIncorrectGv" runat="server" Text='<%# Eval("MissingTrans") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="หมายเหตุ" ItemStyle-HorizontalAlign="Left" Visible="true">
                                                            <ItemStyle Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemsIncorrectRemark" runat="server" Text=''></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="box_body_content">
                            <div class="box_t_h">
                                ::ตรวจสอบรายการ</div>
                            <div class="box_textbox_bg_table">
                                <div class="box_texbox_add_box">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: center;" colspan="4">
                                                <asp:GridView ID="gvCheckList" runat="server" CaptionAlign="Top" CssClass="datatable"
                                                    CellSpacing="-1" GridLines="None" BorderWidth="1" Width="100%" EnableTheming="True"
                                                    PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False" OnRowDataBound="gvCheckList_RowDataBound">
                                                    <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                                        NextPageText="&gt;" PreviousPageText="&lt;" />
                                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                                    <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                                    <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ลำดับที่">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="td-center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIDNumberGv" runat="server" Text='<%# Eval("ID_CARD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Eval("NAMES") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Eval("LASTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Eval("TESTING_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hplview" runat="server" OnClick="hplView_Click" Visible="false">แสดง</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDetailGv" runat="server" Text='<%# Eval("ERROR_MSG") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SEQ_NO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSeqNoGv" runat="server" Text='<%# Eval("SEQ_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="4">
                                                <div>
                                                    <asp:Button ID="btnImport" runat="server" Text="นำเข้าระบบ" CssClass="btn" OnClick="btnImport_Click"
                                                        OnClientClick="return confirmImport();" />
                                                    <asp:Button ID="btnImportCancel" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnImportCancel_Click" />
                                                </div>
                                                <div>
                                                    <asp:Label runat="server" ID="lblErrMessage" Text="" ForeColor="Red"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:HiddenField ID="hdfGroupID" runat="server" />
                    <asp:HiddenField ID="hdfSegNo" runat="server" />
                </div>
                <div style="display: none">
                    <asp:Button ID="btnModal" runat="server" Text="Open" />
                </div>
                <ajaxToolkit:ModalPopupExtender ID="ModGroupApplicant" runat="server" CancelControlID="btnCancel"
                    OkControlID="btnOkay" TargetControlID="btnModal" PopupControlID="pnlModal" PopupDragHandleControlID="PopupHeader"
                    Drag="true" BackgroundCssClass="modalbackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="pnlModal" Style="display: block" runat="server" CssClass="modalpopup">
                    <div class="box_body_content">
                        <div class="box_t_h">
                            ::ข้อมูลการสมัครสอบ</div>
                        <div class="box_textbox_bgadd">
                            <div class="box_texbox_add_box">
                                <uc4:ucPersonalApplicantDetail ID="ucPersonalApplicantDetail1" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <%--    <uc2:UCModalError ID="UCModalError" runat="server" />
                <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />--%>
                <div align="center" style="width: 500px">
                    <asp:Button ID="btnListExamNDel" runat="server" Visible="false" />
                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmModalPopupExtenderListExam" runat="server"
                        DisplayModalPopupID="ModalPopupExtenderListExam" TargetControlID="btnListExamNDel">
                    </ajaxToolkit:ConfirmButtonExtender>
                    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderListExam" runat="server" PopupControlID="pnlConfirmListExamPopup"
                        TargetControlID="btnListExamNDel" Drag="true" CancelControlID="btnConfirmExamListCancel"
                        PopupDragHandleControlID="pnlConfirmListExamPopup" BackgroundCssClass="modalbackgroundComfirm">
                    </ajaxToolkit:ModalPopupExtender>
                    <asp:Panel ID="pnlConfirmListExamPopup" runat="server" CssClass="popup_container_import_data_Confirm"
                        Wrap="true" Style="display: none;">
                        <div class="popup_title_confirm">
                            <asp:Label ID="Label10" runat="server" Text="แจ้งเตือน"></asp:Label>
                        </div>
                        <div style="padding: 15px;">
                            <table width="90%" cellpadding="3" cellspacing="3">
                                <tr>
                                    <td align="center" width="20px" style="background: none; border: none;">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/info.png" />
                                    </td>
                                    <td align="center" style="background: none; border: none;">
                                        สอบซ้ำ กดยืนยันเพื่อสอบ
                                    </td>
                                </tr>
                            </table>
                            <div align="center">
                                <asp:Button ID="btnConfirmExamList" runat="server" CssClass="btn" Text="ยืนยัน" OnClick="btnConfirmExamList_Click" />
                                <asp:Button ID="btnConfirmExamListCancel" runat="server" CssClass="btn" Text="ยกเลิก" />
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnLoadFile" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div style="clear: left;">
    </div>
</asp:Content>
