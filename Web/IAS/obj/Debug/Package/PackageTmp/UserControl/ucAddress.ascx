<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAddress.ascx.cs" Inherits="IAS.UserControl.ucAddress" %>
<%@ Register src="UCLoadingProgress.ascx" tagname="UCLoadingProgress" tagprefix="uc1" %>
    <link href="../style/StyleSheet1.css" rel="stylesheet" type="text/css" />
    <link href="../style/UCStyle.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            function CheckLength() {
                var textbox = document.getElementById("<%= txtAddress.ClientID %>").value;
                if (textbox.trim().length > 101) {
                    return false;
                }
                else {
                    return true;
                }
            }

            function isMaxLength(txtBox) {
                if (txtBox) {
                    return (txtBox.value.length <= " + LENGTH_TEXT + ");
                }
            }

            function Count(text, long) {
                var maxlength = new Number(long);
            }

            function fncInputNumericValuesOnly() 
            {
                if (!(event.keyCode == 45 ||
                event.keyCode == 46 ||
                event.keyCode == 48 ||
                event.keyCode == 49 ||
                event.keyCode == 50 ||
                event.keyCode == 51 ||
                event.keyCode == 52 ||
                event.keyCode == 53 ||
                event.keyCode == 54 ||
                event.keyCode == 55 ||
                event.keyCode == 56 ||
                event.keyCode == 57 ||
                event.keyCode == 8 ||
                event.keyCode == 190 ||
                event.keyCode == 37 ||
                event.keyCode == 39 ||
                event.keyCode == 16)) 
                {
                    event.returnValue = false;
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


            function checkSpecialKeys(e) {
                if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 35 && e.keyCode != 36 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                    return false;
                else
                    return true;
            }     
    </script>
<asp:UpdatePanel ID="udpAddress" runat="server" UpdateMode="Conditional">
    <ContentTemplate> 
    <asp:Panel runat="server" ID="pnlAddress">            
        <table style="width: 100%;">
            <tr>
                <td style="text-align: right; width: 5%;">
                    <asp:Label ID="lblAddress" runat="server" Text="ที่อยู่" CssClass="lbl"></asp:Label><span>:</span>
                </td>
                <td style="text-align: left; width: 25%;" rowspan="2">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="txt" Height="50px" Width="200px"  onkeyDown="return checkTextAreaMaxLength(this,event,'100');"
                     onkeypress="return checkTextAreaMaxLength(this,event,'100');"   onkeyup="this.value=checkMaxLength(this);" 
                        TextMode="MultiLine" Columns="10" Rows="10" MaxLength="100"   ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="AddressRequired" ErrorMessage="กรุณากรอกที่อยู่"
                            runat="server" ControlToValidate="txtAddress" CssClass="failureNotification"
                            ToolTip="กรุณากรอกที่อยู่" ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator runat="server" ID="valInput"
                                        ControlToValidate="txtAddress"
                                        ValidationExpression="^[ก-๙|0-9a-zA-Z|\s|.|(|)|-|\S|/]{1,100}?$"
                                        ErrorMessage="รูปแบบที่อยู่ไม่ถูกต้อง" CssClass="failureNotification"
                                        Display="Dynamic" ValidationGroup="regGuestValidationGroup"><asp:Label ID="Label1" runat="server" visible="false">*</asp:Label>
                    </asp:RegularExpressionValidator>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblProvinceAddress" runat="server" Text="จังหวัด" CssClass="lbl"></asp:Label><span>:</span>
                </td>
                <td style="text-align: left; width: 20%;">
                    <asp:DropDownList ID="ddlProvinceAddress" runat="server" CssClass="ddl" AutoPostBack="True" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True"
                        OnSelectedIndexChanged="ddlProvinceAddress_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ProvinceAddressRequired" runat="server" ControlToValidate="ddlProvinceAddress"
                        CssClass="failureNotification" ToolTip="กรุณาเลือกจังหวัด" ErrorMessage="กรุณาเลือกจังหวัด"
                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 10%;">
                    &nbsp;&nbsp;
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblDistrictAddress" runat="server" Text="เขต/อำเภอ" CssClass="lbl"></asp:Label><span>:</span>
                </td>
                <td style="text-align: left; width: 20%;">
                    <asp:DropDownList ID="ddlDistrictAddress" runat="server" CssClass="ddl" AutoPostBack="True" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True"
                        OnSelectedIndexChanged="ddlDistrictAddress_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="DistrictAddressRequired" runat="server" ControlToValidate="ddlDistrictAddress"
                        CssClass="failureNotification" ToolTip="กรุณาเลือก เขต/อำเภอ" ErrorMessage="กรุณาเลือก เขต-อำเภอ"
                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 5%;">
                    <asp:Label ID="lblPostcodeAddress" runat="server" Text="รหัสไปรษณีย์" CssClass="lbl"></asp:Label><span>:</span>
                </td>
                <td style="text-align: left; width: 25%;">
                    <asp:TextBox ID="txtPostcodeAddress" runat="server" CssClass="txt" 
                        MaxLength="5" onkeypress="fncInputNumericValuesOnly()" onkeyup="this.value=this.value.replace(/^0.*$|[^0-9]/g,'');"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PostcodeAddressRequired" runat="server" ControlToValidate="txtPostcodeAddress" ErrorMessage="กรุณากรอกรหัสไปรษณีย์"
                        CssClass="failureNotification" ToolTip="กรุณากรอกรหัสไปรษณีย์"
                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" runat="server" ControlToValidate="txtPostcodeAddress"
                            ErrorMessage="รหัสไปรษณีย์ 5 หลัก และ ขึ้นต้นด้วยเลข 1-9 เท่านั้น" CssClass="failureNotification" ValidationGroup="regGuestValidationGroup"
                            ValidationExpression="^[1-9][0-9]{4}$"><asp:Label ID="Label2" runat="server" visible="false">*</asp:Label>
                        </asp:RegularExpressionValidator>
                </td>
                <td style="text-align: right; width: 20%;">
                    <asp:Label ID="lblParishAddress" runat="server" Text="แขวง/ตำบล" CssClass="lbl"></asp:Label><span>:</span>
                </td>
                <td style="text-align: left; width: 20%;">
                    <asp:DropDownList ID="ddlParishAddress" runat="server" CssClass="ddl" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="True" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ParishAddressRequired" runat="server" ControlToValidate="ddlParishAddress"
                        CssClass="failureNotification" ToolTip="กรุณาเลือก แขวง/ตำบล" ErrorMessage="กรุณาเลือก แขวง/ตำบล"
                        ValidationGroup="regGuestValidationGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <%--<uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />--%>
                </td>
            </tr>
        </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
