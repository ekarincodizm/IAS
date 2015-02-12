<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="IAS.UserControl.Register" %>
<script type="text/javascript">
    function isValid() {
        var txtApproveDate = document.getElementById('<%=txtApproveDate.ClientID %>');
        var txtExpireDate = document.getElementById('<%=txtExpireDate.ClientID%>');
        var txtBirthDay = document.getElementById('<%=txtBirthDay.ClientID%>');
        var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');
        lblMsg.innerHTML = "";

        if (txtBirthDay.value == '') {
            lblMsg.innerHTML = "";

            lblMsg.innerHTML = "Please enter Birth Day";
            return false;
        }

        if (txtApproveDate.value != '') {
            var items = new Array();
            var items2 = new Array();
            items = txtApproveDate.value.split('/');
            items2 = txtExpireDate.value.split('/');
            lblMsg.innerHTML = "";
            if (items != null && items.length == 3) {

                //return true;
                if (txtExpireDate.value != '') {
                    if (items2 != null && items2.length == 3) {
                        if (items <= items2) {
                            if (items2 >= items) {
                                return true;
                            }

                        }
                        else {
                            lblMsg.innerHTML = "Please enter end date must be after or equal to start date.";
                            return false;
                        }

                    }
                    else {
                        lblMsg.innerHTML = "Date format is 'dd/mm/yyyy' only.";
                        return false;
                    }
                }
                else {
                    lblMsg.innerHTML = "Please Enter End Date.";
                    return txtExpireDate.value != '';
                }
            }
            else {
                lblMsg.innerHTML = "Date format is 'dd/mm/yyyy' only.";
                return false;
            }
        }
        else {

            lblMsg.innerHTML = "Please Enter Start Date.";
            return txtApproveDate.value != '';
        }
    }
</script>
<link href="../style/global.css" rel="stylesheet" type="text/css" />
<div>
    <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTitleName" runat="server" Text="คำนำหน้าชื่อ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlTitleName" runat="server" Width="185px" CssClass="ddl">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblName" runat="server" Text="ชื่อ/บริษัท/สมาคม" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtName" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblSurName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtSurName" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblSurNameBefore" runat="server" Text="นามสกุลเดิม(ถ้ามี)" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtSurNameBefore" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblSex" runat="server" Text="เพศ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" CssClass="rb">
                        <asp:ListItem Text="ชาย" Value="Male" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="หญิง" Value="Female"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblBirthDay" runat="server" Text="วันเกิด" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtBirthDay" runat="server" Width="160px" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CLDtxtBirthDay" runat="server" TargetControlID="txtBirthDay" Format = "dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblIdNumber" runat="server" Text="เลขบัตรประชาชน<br>/เลขทะเบียน" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtIdNumber" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblPlaceApproveCard" runat="server" Text="สถานที่ออกบัตร" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtPlaceApproveCard" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblApproveDate" runat="server" Text="ออกให้เมื่อวันที่" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtApproveDate" runat="server" Width="160px" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CLDtxtApproveDate" runat="server" TargetControlID="txtApproveDate" Format = "dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblExpireDate" runat="server" Text="หมดอายุวันที่" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtExpireDate" runat="server" Width="160px" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CLDtxtExpireDate" runat="server" TargetControlID="txtExpireDate" Format = "dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblEducation" runat="server" Text="ระดับการศึกษา" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlEducation" runat="server" Width="185px" CssClass="ddl">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblCareer" runat="server" Text="อาชีพ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlCareer" runat="server" Width="185px" CssClass="ddl">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;" rowspan="4">
                    <asp:Label ID="lblAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;" rowspan="4">
                    <asp:TextBox ID="txtAddress" runat="server" Width="180px" Height="100px" CssClass="txt" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblProvince" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlProvince" runat="server" Width="185px" CssClass="ddl">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblDistrict" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlDistrict" runat="server" Width="185px" CssClass="ddl">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblSubDistrict" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:DropDownList ID="ddlSubDistrict" runat="server" Width="185px" CssClass="ddl">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblPostalCode" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtPostalCode" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblEmail" runat="server" Text="อีเมล" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtEmail" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTelPhone" runat="server" Text="เบอร์โทรศัพท์" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtTelPhone" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblTypeCandidate" runat="server" Text="ประเภทผู้สมัคร" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtTypeCandidate" runat="server" Width="180px" CssClass="txt" Enabled="false"></asp:TextBox>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblMobilePhone" runat="server" Text="เบอร์โทรศัพท์มือถือ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtMobilePhone" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblIDTest" runat="server" Text="รหัสสอบ" CssClass="lbl"></asp:Label>
                    <span>:</span>
                </td>
                <td style="text-align: left; width: 30%;">
                    <asp:TextBox ID="txtIDTest" runat="server" Width="180px" CssClass="txt"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td style="text-align: center;">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center;">
                    <asp:Button runat="server" ID="btnAdd" Text="ตกลง" CssClass="btn" OnClientClick="javascript:isValid(); return false;" />
                    &nbsp;
                    <asp:Button runat="server" ID="btnCancel" Text="ยกเลิก" CssClass="btn" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
