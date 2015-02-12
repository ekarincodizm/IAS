<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterLicense.master" AutoEventWireup="true" CodeBehind="LicenseAttatchFiles.aspx.cs" Inherits="IAS.License.LicenseAttatchFiles" %>
<%@ Register Src="~/UserControl/ucAttachFileLicense.ascx" TagName="ucAttachFileLicense" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LicenseDetail" runat="server">
    <asp:UpdatePanel ID="udpMain" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:PostBackTrigger  ControlID="btnNext"/>
        <asp:PostBackTrigger  ControlID="btnBack"/>
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center" >
            <table  width="100%">
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblLicenseApprover" runat="server" CssClass="lbl" Text="สมาคมตรวจสอบเอกสาร "></asp:Label>
                        <asp:DropDownList ID="ddlLicenseApprover" runat="server" Width="200px" AutoPostBack="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <uc1:ucAttachFileLicense ID="ucAttachLicense" runat="server" />
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
                                    <asp:ImageButton ID="btnBack" runat="server" 
                                        ImageUrl="~/Images/button_back.png" onclick="btnBack_Click" />
                                </td>
                                <td style="width: 25%;" align="left">
                                    <asp:ImageButton ID="btnNext" runat="server" 
                                        ImageUrl="~/Images/button_next.png" onclick="btnNext_Click" />
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
