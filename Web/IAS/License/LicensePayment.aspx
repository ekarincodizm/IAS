<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicensePayment.aspx.cs" Inherits="IAS.License.LicensePayment" %>
<%@ Register Src="~/UserControl/ucLicensePayment.ascx" TagName="UCLicensePayment" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <%--<asp:PostBackTrigger  ControlID="btnNext"/>
        <asp:PostBackTrigger  ControlID="btnBack"/>--%>
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center" >
            <table  width="100%">
                <tr>
                    <td colspan="2" align="center">
                        <table width="80%">
                            <tr>
                                <td align="left">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <div align="center">
                                                <uc1:UCLicensePayment ID="ucLicensePaymentY" runat="server" ></uc1:UCLicensePayment>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblNotice" runat="server" CssClass="lbl" Visible="false" ForeColor="Blue"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table width="100%">
                            <tr>
                                <td style="width: 25%;">
                                </td>
                                <td colspan="2" style="width: 25%;" align="center">
                                    <asp:Button ID="btnPayment" runat="server" Text="ออกใบสั่งจ่าย" CssClass="btn" Width="120px" onclick="btnPayment_Click"/>&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClientClick="return confirm('ต้องการยกเลิกใช่หรือไม่');"
                                        Width="100px" onclick="btnCancel_Click" />
                                </td>
                                <td style="width: 25%;" align="left">
                                    
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
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
