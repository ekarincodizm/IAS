<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicenseContinue.aspx.cs" Inherits="IAS.License.LicenseContinue" %>
<%@ Register Src="~/UserControl/ucLicense.ascx" TagName="UCLicense" TagPrefix="ucLic" %>
<%@ Register Src="~/UserControl/ucAttachFileControl.ascx" TagName="ucAttachFileControl" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <script type="text/javascript">
    function delrecord() {
        if (confirm("ยันยันการทำรายาร ?")) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
<asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger ControlID="btnNext"/>
        <asp:PostBackTrigger ControlID="btnBack"/>
        <%--<asp:PostBackTrigger ControlID="ddlOther" />--%>
<%--        <asp:PostBackTrigger ControlID="rdLicenseList" />
        <asp:PostBackTrigger ControlID="rdPayment" />--%>
<%--        <asp:AsyncPostBackTrigger ControlID="rdPayment" EventName="CheckedChanged" />
        <asp:AsyncPostBackTrigger ControlID="rdLicenseList" EventName="SelectedIndexChanged" />--%>
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center" >
            <table  width="100%">
                <tr>
                    <td colspan="2">
                        <fieldset style="width:40%; margin: 0px auto 10px auto;">
                        <legend><asp:Label ID="lblHeadName" runat="server" CssClass="lbl" Text="กรณีเสร็จสิ้นการดำเนินการ และต้องการออกใบสั่งจ่าย"></asp:Label></legend>
                            <table width="100%">
                            <tr>
                                <td >
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="10%">
                                    &nbsp;
                                </td>
                                <td align="left" width="90%">
                                    <asp:RadioButton ID="rdPayment" runat="server" Checked="false" AutoPostBack="true"
                                        Text="สร้างใบสั่งจ่าย" oncheckedchanged="rdPayment_CheckedChanged"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="right" >
                                    &nbsp;
                                </td>
                            </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <fieldset style="width:40%; margin: 0px auto 10px auto;">
                        <legend><asp:Label ID="Label2" runat="server" CssClass="lbl" Text="กรณีดำเนินการต่อ"></asp:Label></legend>
                            <table width="100%">
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="10%">
                                    </td>
                                    <td align="left" width="90%">
                                        <asp:RadioButtonList ID="rdLicenseList" runat="server" AutoPostBack="true" 
                                            RepeatDirection="Vertical" Enabled="true" RepeatLayout="Flow" 
                                            onselectedindexchanged="rdLicenseList_SelectedIndexChanged">
                                            <asp:ListItem Text="ขอรับใบอนุญาตใหม่" Selected="False" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="ขอต่อใบอนุญาต" Selected="False" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="ขาดต่อขอใหม่" Selected="False" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="ใบแทนใบอนุญาต(ชำรุดสูญหาย)" Selected="False" Value="4" ></asp:ListItem>
                                        </asp:RadioButtonList>&nbsp;
                                       <%-- <asp:DropDownList ID="ddlOther" runat="server" AutoPostBack="false" Enabled="false" Visible = "false">
                                        </asp:DropDownList>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                    </td>
                                    <td align="left">
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%;">
                                </td>
                                <td style="width: 25%;">
                                </td>
                                <td style="width: 25%;" align="right">
                                    <asp:ImageButton ID="btnBack" runat="server" Visible="false"
                                        ImageUrl="~/Images/button_back.png" onclick="btnBack_Click" />
                                </td>
                                <td style="width: 25%;" align="left">
                                    <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/Images/button_next.png" onclick="btnNext_Click" />
<%--                                    <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return delrecord();" 
                                        ImageUrl="~/Images/button_next.png" onclick="btnNext_Click" />--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <ucLic:UCLicense ID="ucPerLicense" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
