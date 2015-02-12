<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Global.Master" AutoEventWireup="true"
    CodeBehind="regGuest.aspx.cs" Inherits="IAS.RecycleBin.regGuest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--Datepicker-th--%>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.min.js" type="text/javascript"></script>
    <link href="../style/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui-1.8.10.offset.datepicker.min.js" type="text/javascript"></script>
    <script src="../Scripts/LoadScript.js" type="text/javascript"></script>
    <script src="../Scripts/formatTextBox.js" type="text/javascript"></script>
    <%--Datepicker-th--%>
    <script type="text/javascript">

        function confirmDelete() {
            return confirm('คุณต้องการลบข้อมูลหรือไม่?');
        }

        function confirmSave() {
            return confirm('คุณต้องการบันทึกข้อมูลหรือไม่');
        }

        function isValid() {
            var txtBirthDay = document.getElementById('<%=txtBirthDay.ClientID %>');
            var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');

            lblMsg.innerHTML = "";

            if (txtBirthDay.value != '') {
                var items = new Array();
                items = txtBirthDay.value.split(' ');
                lblMsg.innerHTML = "";
                if (items != null && items.length == 3) {

                    return true;

                }
                else {
                    lblMsg.innerHTML = "Date format is 'dd MMMM yyyy' only.";
                    return false;
                }
            }
            else {

                lblMsg.innerHTML = "Please Enter Date.";
                return txtBirthDay.value != '';
            }
        }

        function checkUser() {
            $.ajax({
                url: "../Service/RegistrationWebService.asmx/VerifyIdCard",
                data: "{ strIdCard : '" + $('#<%= txtIDNumber.ClientID %>').val() + "' }",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data == null) {
                        alert("nothing.");
                    }
                    else {
                        if (data.d == true) {
                        }
                        else {
                            alert("กรุณากรอกเลขบัตรประชาชนให้ถูกต้อง");
                        }
                    }
                }
            });
        }

    </script>
    <div>
        <asp:Panel ID="pnlMain" runat="server" Visible="true" Width="100%">
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblTypeMember" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtTypeMember" runat="server" CssClass="txtreadonly"></asp:TextBox>
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblTitle" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:DropDownList ID="ddlTitle" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="TitleRequired" runat="server" ControlToValidate="ddlTitle"
                            CssClass="failureNotification" ToolTip="กรุณาเลือกคำนำหน้าชื่อ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblFirstName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt" autocomplete="off" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="FirstNameRequired" runat="server" ControlToValidate="txtFirstName"
                            CssClass="failureNotification" ToolTip="กรุณากรอกชื่อ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt" autocomplete="off" MaxLength="70"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="LastNameRequired" runat="server" ControlToValidate="txtLastName"
                            CssClass="failureNotification" ToolTip="กรุณากรอกนามสกุล" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblIDNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtIDNumber" runat="server" autocomplete="off" CssClass="txt" MaxLength="13"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="IDNumberRequired" runat="server" ControlToValidate="txtIDNumber"
                            CssClass="failureNotification" ToolTip="กรุณากรอกเลขบัตรประชาชน" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtBirthDay" runat="server" CssClass="txtDate dtpicker"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="BirthDayRequired" runat="server" ControlToValidate="txtBirthDay"
                            CssClass="failureNotification" ToolTip="กรุณาเลือกวัน เดือน ปี เกิด" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                            <asp:ListItem Text="ชาย" Value="M" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="หญิง" Value="F"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblNationality" runat="server" CssClass="lbl" Text="สัญชาติ"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:DropDownList ID="ddlNationality" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="NationalityRequired" runat="server" ControlToValidate="ddlNationality"
                            CssClass="failureNotification" ToolTip="กรุณาเลือกสัญชาติ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:DropDownList ID="ddlEducation" runat="server" CssClass="ddl">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="EducationRequired" runat="server" ControlToValidate="ddlEducation"
                            CssClass="failureNotification" ToolTip="กรุณาเลือกระดับการศึกษา" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblEmail" runat="server" CssClass="lbl" Text="อีเมล"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txt" MaxLength="255"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="txtEmail"
                            CssClass="failureNotification" ToolTip="กรุณากรอกอีเมล" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblTel" runat="server" Text="โทรศัพท์" CssClass="lbl"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtTel" runat="server" CssClass="txt" MaxLength="15"></asp:TextBox>
                    </td>
                    <td style="text-align: right; width: 20%;">
                        <asp:Label ID="lblMobilePhone" runat="server" CssClass="lbl" Text="เบอร์โทรศัพท์มือถือ"></asp:Label>
                        <span>:</span>
                    </td>
                    <td style="text-align: left; width: 30%;">
                        <asp:TextBox ID="txtMobilePhone" runat="server" CssClass="txt" MaxLength="15"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="MobilePhoneRequired" runat="server" ControlToValidate="txtMobilePhone"
                            CssClass="failureNotification" ToolTip="กรุณากรอกเบอร์โทรศัพท์มือถือ" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: center;">
                    </td>
                </tr>
            </table>
            <table style="width: 100%;" align="center">
                <tr>
                    <td>
                        <asp:Button Text="ที่อยู่ปัจจุบัน" ID="TabCurrentAddress" BorderStyle="None" CssClass="Initial"
                            runat="server" OnClick="TabCurrentAddress_Click" />
                        <asp:Button Text="ที่อยู่ตามทะเบียนบ้าน" ID="TabRegisterAddress" BorderStyle="None"
                            CssClass="Initial" runat="server" OnClick="TabRegisterAddress_Click" />
                        <asp:UpdatePanel ID="udpview" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <asp:MultiView ID="mainView" runat="server">
                                    <asp:View ID="viewCurrentAddress" runat="server">
                                        <table style="width: 100%; border-width: 1px; border-color: #dddddd; border-style: solid">
                                            <tr>
                                                <td style="text-align: right; width: 5%;">
                                                    <asp:Label ID="lblCurrentAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 25%;" rowspan="2">
                                                    <asp:TextBox ID="txtCurrentAddress" runat="server" CssClass="txt" Height="50px" Width="200px"
                                                        TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="CurrentAddressRequired" runat="server" ControlToValidate="txtCurrentAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณากรอกที่อยู่ปัจจุบัน" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblProvinceCurrentAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:DropDownList ID="ddlProvinceCurrentAddress" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlProvinceCurrentAddress_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="ProvinceCurrentAddressRequired" runat="server" ControlToValidate="ddlProvinceCurrentAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณาเลือกจังหวัด ของที่อยู่ปัจจุบัน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 10%;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblDistrictCurrentAddress" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:DropDownList ID="ddlDistrictCurrentAddress" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlDistrictCurrentAddress_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="DistrictCurrentAddressRequired" runat="server" ControlToValidate="ddlDistrictCurrentAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณาเลือก เขต/อำเภอ ของที่อยู่ปัจจุบัน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 5%;">
                                                    <asp:Label ID="lblPostcodeCurrentAddress" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 25%;">
                                                    <asp:TextBox ID="txtPostcodeCurrentAddress" runat="server" CssClass="txt" MaxLength="5"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PostcodeCurrentAddressRequired" runat="server" ControlToValidate="txtPostcodeCurrentAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณากรอกรหัสไปรษณีย์ ที่อยู่ปัจจุบัน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblParishCurrentAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:DropDownList ID="ddlParishCurrentAddress" runat="server" CssClass="ddl" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="ParishCurrentAddressRequired" runat="server" ControlToValidate="ddlParishCurrentAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณาเลือก แขวง/ตำบล ของที่อยู่ปัจจุบัน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="viewRegisterAddress" runat="server">
                                        <table style="width: 100%; border-width: 1px; border-color: #dddddd; border-style: solid">
                                            <tr>
                                                <td style="text-align: right; width: 5%;">
                                                    <asp:Label ID="lblRegisterAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 25%;" rowspan="2">
                                                    <asp:TextBox ID="txtRegisterAddress" runat="server" CssClass="txt" Height="50px"
                                                        Width="200px" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RegisterAddressRequired" runat="server" ControlToValidate="txtRegisterAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณากรอกที่อยู่ ตามทะเบียนบ้าน" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblProvinceRegisterAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:DropDownList ID="ddlProvinceRegisterAddress" runat="server" CssClass="ddl" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlProvinceRegisterAddress_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="ProvinceRegisterAddressRequired" runat="server" ControlToValidate="ddlProvinceRegisterAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณาเลือกจังหวัด ตามทะเบียนบ้าน" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 10%;">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblDistrictRegisterAddress" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:DropDownList ID="ddlDistrictRegisterAddress" runat="server" CssClass="ddl" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlDistrictRegisterAddress_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="DistrictRegisterAddressRequired" runat="server" ControlToValidate="ddlDistrictRegisterAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณาเลือก เขต/อำเภอ ตามทะเบียนบ้าน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; width: 5%;">
                                                    <asp:Label ID="lblPostcodeRegisterAddress" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 25%;">
                                                    <asp:TextBox ID="txtPostcodeRegisterAddress" runat="server" CssClass="txt" MaxLength="5"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PostcodeRegisterAddressRequired" runat="server" ControlToValidate="txtPostcodeRegisterAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณากรอกรหัสไปรษณีย์ ตามทะเบียนบ้าน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td style="text-align: right; width: 20%;">
                                                    <asp:Label ID="lblParishRegisterAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                                                    <span>:</span>
                                                </td>
                                                <td style="text-align: left; width: 20%;">
                                                    <asp:DropDownList ID="ddlParishRegisterAddress" runat="server" CssClass="ddl">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="ParishRegisterAddressRequired" runat="server" ControlToValidate="ddlParishRegisterAddress"
                                                        CssClass="failureNotification" ToolTip="กรุณาเลือก แขวง/ตำบล ตามทะเบียนบ้าน"
                                                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <fieldset>
                <legend class="lbl">เอกสารแนบ</legend>
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
                            <asp:FileUpload ID="fulFile" runat="server" Width="300px" />
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtDetail" runat="server" CssClass="txt" Width="350px" MaxLength="100"></asp:TextBox>
                        </td>
                        <td style="text-align: left; width: 10%;">
                            <asp:Button ID="btnUploadFile" runat="server" Text="โหลดไฟล์" CssClass="btn" OnClick="btnUploadFile_Click" />
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
                                AutoGenerateColumns="False" PagerStyle-CssClass="viewpager" DataKeyNames="ID"
                                OnRowCancelingEdit="gvUpload_RowCancelingEdit" OnRowEditing="gvUpload_RowEditing"
                                OnRowUpdating="gvUpload_RowUpdating" OnPreRender="gvUpload_PreRender">
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                <RowStyle CssClass="view" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                <AlternatingRowStyle CssClass="view2" />
                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                <Columns>
                                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowCancelButton="true"
                                        EditText="แก้ไข" UpdateText="ตกลง" CancelText="ยกเลิก" ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="ID" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIDGv" runat="server" Text='<%# Bind("REGISTRATION_ID") %>'></asp:Label>
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
                                        <ItemStyle Width="40%" />
                                        <ItemTemplate>
                                            <%# Eval("REMARK")%>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDetailGv" runat="server" Text='<%# Bind("REMARK") %>' CssClass="txt"
                                                Width="350px" MaxLength="100"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hplView" runat="server" OnClick="hplView_Click">ดู</asp:LinkButton>
                                            <asp:LinkButton ID="hplDelete" runat="server" OnClientClick="return confirmDelete();"
                                                OnClick="hplDelete_Click">ลบ</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TempFilePath" Visible="false" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="30%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTempFilePath" runat="server" Text='<%# Bind("TempFilePath") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="4">
                            <asp:ValidationSummary ID="regGuestValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="regGuestValidationGroup" HeaderText="กรุณากรอกข้อมูลให้ครบ"
                                ShowSummary="False" ShowMessageBox="true" />
                        </td>
                    </tr>
                </table>
                <table align="center" style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 25%;">
                            <asp:Label ID="lblResultReg" runat="server" Text="ผลการสมัคร" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 75%;" colspan="3">
                            <asp:TextBox ID="txtResultReg" runat="server" CssClass="txt" Height="100px" Width="500px"
                                TextMode="MultiLine" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOk_Click"
                                OnClientClick="javascript:isValid();" ValidationGroup="regGuestValidationGroup" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
            <table style="width: 100%;" align="center">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="udpCondition" UpdateMode="Always" runat="server" Visible="false">
                            <ContentTemplate>
                                <fieldset>
                                    <legend class="lbl">เงื่อนไขข้อตกลงและยืนยัน</legend>
                                    <table align="center" style="border: 3px solid black; width: 90%; text-align: center;
                                        vertical-align: middle;">
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblCondition1" runat="server" Text="1........................................."></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblCodition2" runat="server" Text="2........................................."></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblCodition3" runat="server" Text="3........................................."></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="text-align: left;" colspan="4">
                                                <asp:CheckBox ID="chkCodition" runat="server" CssClass="chk" Text="ข้าพเจ้าขอยอมรับ.............................................." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left;" colspan="4">
                                                <asp:Label ID="lblText" runat="server" Text="ชื่อผู้ใช้งานของท่านคือเลขบัตรประชาชน<br>กรุณาระบุรหัสผ่านของท่านเพื่อเป็นการยืนยันการสมัคร"
                                                    CssClass="lbl"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblPassword" runat="server" Text="รหัสผ่าน" CssClass="lbl"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtPassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td style="text-align: right; width: 20%;">
                                                <asp:Label ID="lblConfirmPassword" runat="server" Text="ยืนยันรหัสผ่าน" CssClass="lbl"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 30%;">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center;">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Enabled="false" Text="ตกลง"
                                                    OnClientClick="return confirmSave();" OnClick="btnSubmit_Click" ValidationGroup="regGuestValidationGroup" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
