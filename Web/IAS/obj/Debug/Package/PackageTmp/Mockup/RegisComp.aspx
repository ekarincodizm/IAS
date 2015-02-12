<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site.Master" AutoEventWireup="true"
    CodeBehind="RegisComp.aspx.cs" Inherits="IAS.Exam.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Label Text="ลงทะเบียน (บริษัท)" BorderStyle="None" ID="lblRegister" CssClass="Initial"
        runat="server" /><br />
    <br />
    &nbsp;<fieldset style="width: 90%; margin: 0px auto 10px auto;">
        <legend class="Initial">
            <asp:Label Text="ข้อมูลบริษัท" BorderStyle="None" ID="Label3" runat="server" /></legend>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblCompany" runat="server" Text="บริษัท" CssClass="lbl"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="txt" Width="90%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CompanyRequired" runat="server" ControlToValidate="txtCompany"
                        CssClass="failureNotification" ErrorMessage="Company is required." ToolTip="Company is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hdf" runat="server" />
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblCompanyRegister" runat="server" Text="ทะเบียนบริษัท" CssClass="lbl"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtCompanyRegister" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="g" runat="server" ControlToValidate="txtCompanyRegister"
                        CssClass="failureNotification" ErrorMessage="Register Company is required." ToolTip="Company Register is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblCompanyAddress1" runat="server" Text="ที่อยู่ 1" CssClass="lbl"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 30%;" rowspan="3">
                    <asp:TextBox ID="txtCompanyAddress1" runat="server" CssClass="txt" Height="70px"
                        TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CompanyAddress1Required" runat="server" ControlToValidate="txtCompanyAddress1"
                        CssClass="failureNotification" ErrorMessage="CompanyAddress1 is required." ToolTip="CompanyAddress1 is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblProvince" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlProvince" runat="server" CssClass="ddl" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ProvinceRequired" runat="server" ControlToValidate="ddlProvince"
                        CssClass="failureNotification" ErrorMessage="Province is required." ToolTip="Province is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%; height: 17px;">
                    &nbsp;
                </td>
                <td style="text-align: right; width: 20%; height: 17px;">
                    <span>
                        <asp:Label ID="lblDistrict" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                    </span>:
                </td>
                <td style="text-align: left; width: 30%; height: 17px;">
                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="ddl" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="DistrictRequired" runat="server" ControlToValidate="ddlDistrict"
                        CssClass="failureNotification" ErrorMessage="District is required." ToolTip="District is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    &nbsp;
                </td>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblParish" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                    </span>:
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlParish" runat="server" CssClass="ddl" Width="150px" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ParishRequired" runat="server" ControlToValidate="ddlParish"
                        CssClass="failureNotification" ErrorMessage="Parish is required." ToolTip="Parish is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <span>
                        <asp:Label ID="lblCompanyTel" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                    </span>:
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtCompanyTel" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CompanyTelRequired" runat="server" ControlToValidate="txtCompanyTel"
                        CssClass="failureNotification" ErrorMessage="CompanyTel is required." ToolTip="CompanyTel is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblCompanyPostcode" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtCompanyPostcode" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="CompanyPostcodeRequired" runat="server" ControlToValidate="txtCompanyPostcode"
                        CssClass="failureNotification" ErrorMessage="Company Postcode is required." ToolTip="Company Postcode is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 90%; margin: 0px auto 10px auto;">
        <legend class="Initial">
            <asp:Label Text="ข้อมูลพนักงานบริษัท" BorderStyle="None" ID="Label4" runat="server" /></legend>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 15%;">
                    <span>
                        <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtMemberType" runat="server" CssClass="txtreadonly"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 15%;">
                    <span>
                        <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 25%;">
                    <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl" Width="150px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle"
                        CssClass="failureNotification" ErrorMessage="Title is required." ToolTip="Title is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%;">
                    <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                        CssClass="failureNotification" ErrorMessage="FirstName is required." ToolTip="FirstName is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right; width: 15%;">
                    <span>
                        <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                        :</span>
                </td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                        CssClass="failureNotification" ErrorMessage="LastName is required." ToolTip="LastName is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%; height: 17px;">
                    <asp:Label ID="lblTel" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 25%; height: 17px;">
                    <asp:TextBox ID="txtTel" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="TelRequired" runat="server" ControlToValidate="txtTel"
                        CssClass="failureNotification" ErrorMessage="Tel is required." ToolTip="Tel is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right; width: 15%; height: 17px;">
                    <asp:Label ID="lblStartDate0" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 30%; height: 17px;">
                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                        <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 15%;">
                    <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                        CssClass="failureNotification" ErrorMessage="Email is required." ToolTip="Email is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblIDCard" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                    :
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="IDCardRequired" runat="server" ControlToValidate="txtIDCard"
                        CssClass="failureNotification" ErrorMessage="ID-Card is required." ToolTip="ID-Card is required."
                        ValidationGroup="CompanyValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="width: 90%; margin: 0px auto auto auto;">
        <legend class="Initial">
            <asp:Label Text="เอกสารแนบ" BorderStyle="None" ID="Label5" runat="server" /></legend>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center; width: 30%;">
                    <asp:Label ID="lblTypeAttachment" runat="server" Text="ประเภทไฟล์แนบ" CssClass="lblbig"></asp:Label>
                </td>
                <td style="text-align: center; width: 20%;">
                    <asp:Label ID="lblFile" runat="server" Text="ไฟล์แนบ" CssClass="lblbig"></asp:Label>
                </td>
                <td style="text-align: center; width: 40%;">
                    <asp:Label ID="lblDetail" runat="server" Text="หมายเหตุ" CssClass="lblbig"></asp:Label>
                    <br />
                </td>
                <td style="text-align: center; width: 10%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlTypeAttachment" runat="server" CssClass="ddl" Width="100px">
                    </asp:DropDownList>
                </td>
                <td style="text-align: center; width: 20%;">
                    <asp:FileUpload ID="fulFile" runat="server" />
                </td>
                <td style="text-align: left; width: 40%;">
                    <%--<asp:TextBox ID="txtDetail" runat="server" CssClass="txt" Width="150px"></asp:TextBox>--%>
                </td>
                <td style="text-align: center; width: 10%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="4">
                    <asp:GridView ID="gvUpload" runat="server" BackColor="White" BorderColor="#999999"
                        HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3"
                        GridLines="Vertical" Width="100%" AllowPaging="True" BorderStyle="None" EnableTheming="True"
                        PageSize="12" ssClass="gridtxlist" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">"
                        PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First"
                        AllowSorting="True" PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center"
                        AutoGenerateColumns="False" PagerStyle-CssClass="viewpager" DataKeyNames="ID">
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            NextPageText="&gt;" PreviousPageText="&lt;" />
                        <RowStyle CssClass="view" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                        <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                        <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                        <AlternatingRowStyle CssClass="view2" />
                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                        <Columns>
                            <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ShowCancelButton="true"
                                EditText="แก้ไข" UpdateText="ตกลง" CancelText="ยกเลิก" ItemStyle-Width="10%" />--%>
                            <asp:TemplateField HeaderText="ID" Visible="false" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblIDGv" runat="server" Text='<%# Bind("REGISTRATION_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ประเภทไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeAttachmentGv" runat="server" Text='<%# Bind("ATTACH_FILE_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ไฟล์แนบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblFileGv" runat="server" Text='<%# Bind("ATTACH_FILE_PATH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="หมายเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="40%" />
                                <ItemTemplate>
                                    <%# Eval("REMARK")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDetailGv" runat="server" Text='<%# Bind("REMARK") %>' CssClass="txt"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle Width="20%" />
                                <ItemTemplate>
                                    <%--     <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click">ดู</asp:LinkButton>
                                    <asp:LinkButton ID="hplDelete" runat="server" OnClick="hplDelete_Click">ลบ</asp:LinkButton>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="text-align: left;" colspan="4">
                </td>
            </tr>
        </table>
    </fieldset>
    <table style="width: 90%; margin: 0 auto;">
        <tr>
            <td style="text-align: left; vertical-align: top; width: 15%;">
                <asp:Label ID="lblResultReg" runat="server" Text="ผลการสมัคร" CssClass="lbl"></asp:Label>
                <span>:</span>
            </td>
            <td style="text-align: left; width: 85%;">
                <asp:TextBox ID="txtResultReg" runat="server" CssClass="txt" Height="100px" Width="500px"
                    ReadOnly="true" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;" colspan="4">
                &nbsp;&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" />
            </td>
        </tr>
    </table>
</asp:Content>
