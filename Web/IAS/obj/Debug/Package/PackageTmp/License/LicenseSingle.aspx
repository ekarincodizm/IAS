<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="LicenseSingle.aspx.cs" Inherits="IAS.License.LicenseSingle" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
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
            <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PnlSearch" runat="server" Visible="true" Width="100%">

                    <div class="box_body_content">
                    <div class="box_t_h"></div>
                    <div class="box_textbox_bgadd">
                    <div class="box_texbox_add_box">


                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right; width: 20%;">
                                    <asp:Label ID="lblLicenseNumber" runat="server" Text="ประเภทขอรับใบอนุญาต" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left; width: 25%;">
                                    <asp:DropDownList ID="ddlRequestLicenseType" runat="server" CssClass="ddl" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlRequestLicenseType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequestLicenseTypeRequired" runat="server" ControlToValidate="ddlRequestLicenseType"
                                        CssClass="failureNotification" ValidationGroup="LicenseSingleValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right; width: 20%;">
                                    <asp:Label ID="lblLicenseType" runat="server" Text="ประเภทใบอนุญาต" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left; width: 30%;">
                                    <asp:DropDownList ID="ddlLicenseType" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="licenseTypeRequired" runat="server" ControlToValidate="ddlLicenseType"
                                        CssClass="failureNotification" ValidationGroup="LicenseSingleValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 20%;">
                                    <asp:Label ID="lblApproveDate" runat="server" Text="บริษัท" CssClass="lbl"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left; width: 25%;">
                                    <asp:DropDownList ID="ddlCompany" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="companyRequired" runat="server" ControlToValidate="ddlCompany"
                                        CssClass="failureNotification" ValidationGroup="LicenseSingleValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right; width: 20%;">
                                    &nbsp;<asp:Label ID="lblExpireDate" runat="server" CssClass="lbl" Text="ค่าธรรมเนียม"></asp:Label>
                                    :
                                </td>
                                <td style="text-align: left; width: 30%;">
                                    <asp:TextBox ID="txtFee" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right; width: 20%;">
                                    <span>
                                        <asp:Label ID="lblApproveDate0" runat="server" CssClass="lbl" Text="เลขที่ใบอนุญาต"></asp:Label>
                                        :</span>
                                </td>
                                <td style="text-align: left; width: 25%;">
                                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="txt"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="licenseNumberRequired" runat="server" ControlToValidate="txtLicenseNumber"
                                        CssClass="failureNotification" ValidationGroup="LicenseSingleValidationGroup">*</asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: right; width: 20%;">
                                    &nbsp;
                                </td>
                                <td style="text-align: left; width: 30%;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>


                    </div>
                    </div>
                    </div>
                    


                    
                            <asp:UpdatePanel ID="UpdatePanelUpload" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                <div class="box_body_content">
                                <div class="box_t_h">::เอกสารแนบ</div>
                                <div class="box_textbox_bgadd">
                                <div class="box_texbox_add_box">


                                    <table style="width: 90%;" align="center">
                                        <tr>
                                            <td style="text-align: center; width: 20%;">
                                                <asp:Label ID="lblTypeAttachment" runat="server" Text="ประเภทไฟล์แนบ" CssClass="lblbig"></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 30%;">
                                                <asp:Label ID="lblFile" runat="server" Text="ไฟล์แนบ" CssClass="lblbig"></asp:Label>
                                            </td>
                                            <td style="text-align: center; width: 30%;">
                                                <asp:Label ID="lblDetail" runat="server" Text="หมายเหตุ" CssClass="lblbig"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left; width: 20%;">
                                                <asp:DropDownList ID="ddlTypeAttachment" runat="server" CssClass="ddl" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:FileUpload ID="fulFile" runat="server" Width="300px" CssClass="fileUpload" />
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtDetail" runat="server" CssClass="txt" Width="350px" MaxLength="100"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left; width: 10%;">
                                                <asp:Button ID="btnUploadFile" runat="server" Text="โหลดไฟล์" CssClass="btn" OnClick="btnUploadFile_Click"
                                                    ValidationGroup="LicenseSingleValidationGroup" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <uc1:UCLoadingProgress ID="UCLoadingProgress2" runat="server" />
                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelUpload">
                                                    <ProgressTemplate>
                                                        Please wait image uploaded....</ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center;" colspan="4">



                                                <asp:GridView ID="gvUpload" runat="server"  Width="100%" AllowPaging="false" ShowHeaderWhenEmpty="True"
                                                CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1"
                                                    EnableTheming="True" AutoGenerateColumns="False" DataKeyNames="ID"
                                                    OnRowCancelingEdit="gvUpload_RowCancelingEdit" OnRowEditing="gvUpload_RowEditing"
                                                    OnRowUpdating="gvUpload_RowUpdating" OnPreRender="gvUpload_PreRender" OnPageIndexChanging="gvUpload_PageIndexChanging"
                                                    OnRowDataBound="gvUpload_RowDataBound" >
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle  HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="False">
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="lbnOkGv" runat="server" CausesValidation="True" CommandName="Update"
                                                                    Text="ตกลง"></asp:LinkButton>
                                                                &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="lbnCancelGv"
                                                                    Text="ยกเลิก"></asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbnEditGv" runat="server" CausesValidation="False" CommandName="Edit"
                                                                    Text="<img src='../Images/edit-icon.gif' title='แก้ไข' />"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIDGv" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeAttachmentGv" runat="server" Text='<%# Bind("DocumentTypeName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="20%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFileGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="หมายเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <%# Eval("REMARK")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDetailGv" runat="server" Text='<%# Bind("REMARK") %>' CssClass="txt"
                                                                    Width="350px" MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="15%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click"><img src="../Images/view-icon.png" title="ดูรายละเอียด" /></asp:LinkButton>&nbsp;&nbsp;
                                                                <asp:LinkButton ID="hplDelete" runat="server" OnClientClick="return confirmDelete();"
                                                                    OnClick="hplDelete_Click"><img src="../Images/delete_icon.png" title="ลบ" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TempFilePath" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTempFilePath" runat="server" Text='<%# Bind("TempFilePath") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                            <ItemStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusGv" runat="server" Text='<%# Bind("FILE_STATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="4">
                                                <asp:Label ID="lblNote" runat="server" ForeColor="Red" 
                                                    Text="หมายเหตุ : สามารถแนบไฟล์ที่มีนามสกุล BMP,GIF,JPG,PNG,TIF,PDF เท่านั้น "></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: left;">
                                                <asp:Label ID="lblMsg" runat="server" CssClass="lbl" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: left;">
                                                <asp:ValidationSummary ID="licenseSingleValidationSummary" runat="server" CssClass="failureNotification"
                                                    HeaderText="กรุณากรอกข้อมูลให้ครบ" ShowMessageBox="true" ShowSummary="False"
                                                    ValidationGroup="LicenseSingleValidationGroup" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                                </div>
                                </div>
                                


                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUploadFile" />
                                </Triggers>
                            </asp:UpdatePanel>
             
                    </asp:Panel>
                    <asp:Panel ID="pnlImportFile" runat="server" Visible="true">

                        <div class="box_body_content">
                        <div class="box_t_h"></div>
                        <div class="box_textbox_bgadd">
                        <div class="box_texbox_add_box">
                                <div style="text-align: center;">
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnSubmit_Click" ValidationGroup="LicenseSingleValidationGroup" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn"  OnClick="btnCancle_Click"   />
                                    
                                    <br />
                                    <br />
                                </div>
                                <ajaxToolkit:TabContainer ID="TabDetail" runat="server" ActiveTabIndex="1" 
                                    AutoPostBack="true" Width="100%" CssClass="ajax_tabs" 
                                    OnActiveTabChanged="TabDetail_ActiveTabChanged">
                                    <ajaxToolkit:TabPanel ID="TabProfile" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblProfile" runat="server" ForeColor="Black" Text="ประวัติส่วนบุคคล"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="text-align: right; width: 15%;">
                                                        <asp:Label ID="lblFirstNameLastName" runat="server" CssClass="lbl" Text="ชื่อ-นามสกุล"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 35%;">
                                                        <asp:TextBox ID="txtFirstNameLastName" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 22%;">
                                                        <asp:Label ID="lblIdCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 28%;">
                                                        <asp:TextBox ID="txtIdCard" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 15%;">
                                                        <asp:Label ID="lblSex" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 35%;">
                                                        <asp:TextBox ID="txtSex" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 22%;">
                                                        <asp:Label ID="lblNationality" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 28%;">
                                                        <asp:TextBox ID="txtNationality" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right; width: 15%;">
                                                        <asp:Label ID="lblEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 35%;">
                                                        <asp:TextBox ID="txtEducation" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right; width: 22%;">
                                                        <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                                                        <span>:</span>
                                                    </td>
                                                    <td style="text-align: left; width: 28%;">
                                                        <asp:TextBox ID="txtBirthDay" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <fieldset>
                                                <legend class="lbl">ที่อยู่ปัจจุบัน</legend>
                                                <table style="width: 100%; border-width: 1px;">
                                                    <tr>
                                                        <td style="text-align: right; width: 5%;">
                                                            <asp:Label ID="lblCurrentAddress" runat="server" CssClass="lbl" Text="ที่อยู่"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td rowspan="2" style="text-align: left; width: 25%;">
                                                            <asp:TextBox ID="txtCurrentAddress" runat="server" CssClass="txt" Height="50px" MaxLength="200"
                                                                ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblProvinceCurrentAddress" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtProvinceCurrentAddress" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 10%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblDistrictCurrentAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtDistrictCurrentAddress" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 5%;">
                                                            <asp:Label ID="lblPostcodeCurrentAddress" runat="server" CssClass="lbl" Text="รหัสไปรษณีย์"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 25%;">
                                                            <asp:TextBox ID="txtPostcodeCurrentAddress" runat="server" CssClass="txt" MaxLength="5"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblParishCurrentAddress" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtParishCurrentAddress" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                            <fieldset>
                                                <legend class="lbl">ที่อยู่ตามทะเบียนบ้าน</legend>
                                                <table style="width: 100%; border-width: 1px;">
                                                    <tr>
                                                        <td style="text-align: right; width: 5%;">
                                                            <asp:Label ID="lblRegisterAddress" runat="server" CssClass="lbl" Text="ที่อยู่"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td rowspan="2" style="text-align: left; width: 25%;">
                                                            <asp:TextBox ID="txtRegisterAddress" runat="server" CssClass="txt" Height="50px"
                                                                MaxLength="100" ReadOnly="True" TextMode="MultiLine" Width="150px"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblProvinceRegisterAddress" runat="server" CssClass="lbl" Text="จังหวัด"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtProvinceRegisterAddress" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 10%;">
                                                            &nbsp;
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblDistrictRegisterAddress" runat="server" CssClass="lbl" Text="เขต/อำเภอ"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtDistrictRegisterAddress" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; width: 5%;">
                                                            <asp:Label ID="lblPostcodeRegisterAddress" runat="server" CssClass="lbl" Text="รหัสไปรษณีย์"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 25%;">
                                                            <asp:TextBox ID="txtPostcodeRegisterAddress" runat="server" CssClass="txt" MaxLength="5"
                                                                ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                        <td style="text-align: right; width: 20%;">
                                                            <asp:Label ID="lblParishRegisterAddress" runat="server" CssClass="lbl" Text="แขวง/ตำบล"></asp:Label>
                                                            <span>:</span>
                                                        </td>
                                                        <td style="text-align: left; width: 20%;">
                                                            <asp:TextBox ID="txtParishRegisterAddress" runat="server" CssClass="txt" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabHistoryExam" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHistoryExam" runat="server" ForeColor="Black" Text="ประวัติการสอบ"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 100%;">


                                                <asp:GridView ID="gvHistoryExam" runat="server" AllowPaging="false" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    CssClass="datatable" CellSpacing="-1" GridLines="None" BorderWidth="1"
                                                     EnableTheming="True"  Width="100%">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle  HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="รหัสผู้สอบ">
                                                            <ItemStyle Width="10%" CssClass="td-center"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertifiedIDGv" runat="server" Text='<%# Bind("APPLICANT_CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="รหัสสอบ">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Bind("TESTING_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่สอบ">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExamDateGv" runat="server" Text='<%# Bind("TESTING_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เวลาที่สอบ">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExamTimeGv" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทใบอนุญาต">
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeExamGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สนามสอบ">
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFieldExamGv" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="บริษัทประกันภัย">
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompanyGv" runat="server" Text='<%# Bind("INSUR_COMP_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ผลสอบ">
                                                            <ItemStyle Width="10%" CssClass="td-center"/>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblResultExamGv" runat="server" Text='<%# Bind("EXAM_RESULT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <%-- <asp:TemplateField HeaderText="วันหมดอายุ">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("EXPIRE_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>


                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabHistoryTraining" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblHistoryTraining" runat="server" ForeColor="Black" Text="ประวัติการอบรม"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 100%;">
                                                <asp:GridView ID="gvHistoryTraining" runat="server" AllowPaging="True" AutoGenerateColumns="False" BorderWidth="1" ShowHeaderWhenEmpty="True"
                                                    BorderStyle="None" EnableTheming="True" GridLines="None" Width="100%" CellSpacing="-1" CssClass="datatable">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle  HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeIDGv" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeExamGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="อบรมครั้งที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrainingTimeGv" runat="server" Text='<%# Bind("TRAIN_TIMES") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ผลการอบรมเริ่มวันที่" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrainingStartDateGv" runat="server" Text='<%# Bind("TRAIN_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ผลอบรมสิ้นสุดวันที่" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrainingEndDateGv" runat="server" Text='<%# Bind("TRAIN_DATE_EXP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="จำนวนชม." ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalHourGv" runat="server" Text='<%# Bind("HOURS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สถานะ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatusGv" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabTraining" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTraining" runat="server" ForeColor="Black" Text="อบรม(1)-(4)"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 100%;">
                                                <asp:GridView ID="GvTraining" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datatable" ShowHeaderWhenEmpty="True"
                                                    BorderStyle="None" EnableTheming="True" GridLines="None" Width="100%" CellSpacing="-1" BorderWidth="1">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle  HorizontalAlign="Right" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeIDGv" runat="server" Text='<%# Bind("SPECIAL_TYPE_CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ชื่อประเภท" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeNameGv" runat="server" Text='<%# Bind("SPECIAL_TYPE_DESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เริ่มวันที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStartDateGv" runat="server" Text='<%# Bind("START_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="สิ้นสุดวันที่" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="5%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEndDateGv" runat="server" Text='<%# Bind("END_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ส่งวันที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSendDateGv" runat="server" Text='<%# Bind("SEND_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ส่งโดย" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSendByGv" runat="server" Text='<%# Bind("SEND_BY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID_CARD_NO" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabUnitLink" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUnitLink" runat="server" ForeColor="Black" Text="Unit link"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table style="width: 100%;">
                                                <asp:GridView ID="gvUnitLink" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="datatable" ShowHeaderWhenEmpty="True"
                                                    BorderStyle="None" EnableTheming="True" GridLines="None" Width="100%" CellSpacing="-1" BorderWidth="1">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                                 
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="รหัสประเภท" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="20%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeIDGv" runat="server" Text='<%# Bind("LICENSE_TYPE_CODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ประเภทใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="30%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeLicenseGv" runat="server" Text='<%# Bind("LICENSE_TYPE_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="อบรมครั้งที่" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="25%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrainingTimeGv" runat="server" Text='<%# Bind("TRAIN_TIMES") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่อบรม" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="25%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrainingDateGv" runat="server" Text='<%# Bind("TRAIN_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID_CARD_NO" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle Width="25%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel ID="TabObtainLicense" runat="server">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblObtainLicense" runat="server" ForeColor="Black" Text="ขอรับใบอนุญาต"></asp:Label>
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <table width="100%">
                                                <asp:GridView ID="gvObtainLicense" runat="server" AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    BorderStyle="None" EnableTheming="True" GridLines="None" CellSpacing="-1" BorderWidth="1" CssClass="td-center" 
                                                    Width="100%">
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <HeaderStyle BorderStyle="None" />
                                                    <PagerStyle CssClass="table_item_row" HorizontalAlign="Left" />
                                                    <RowStyle CssClass="table_item_row" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="เลขที่จ่าย" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayNumberGv" runat="server" Text='<%# Bind("PAYMENT_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="เลขใบอนุญาต" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Bind("LICENSE_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ครั้งที่ต่ออายุ" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRenewGv" runat="server" Text='<%# Bind("RENEW_TIME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่ต่ออายุ" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRenewDateGv" runat="server" Text='<%# Bind("RENEW_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="วันที่หมดอายุ" ItemStyle-HorizontalAlign="Center"
                                                            Visible="true">
                                                            <ItemStyle Width="10%" CssClass="td-center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpireDateGv" runat="server" Text='<%# Bind("EXPIRE_DATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ID_CARD_NO" ItemStyle-HorizontalAlign="Center" Visible="true">
                                                            <ItemStyle Width="10%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIdCardNoGv" runat="server" Text='<%# Bind("ID_CARD_NO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </table>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                          
                        </div>
                        </div>
                        </div>
                        
                    </asp:Panel>
                    <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUploadFile" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>



</div>
<div style="clear:left;"></div>
</asp:Content>
