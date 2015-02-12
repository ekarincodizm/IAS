<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SettingExam.aspx.cs" Inherits="IAS.Setting.SettingExam" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UCModalError.ascx" TagName="UCModalError" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagName="UCModalSuccess" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function runScript(e) {
            var charCode = (e.which) ? e.which : e.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            else if (charCode == 13) {
                return false;
            }
            return true;
        }

    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">

            <div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
               <asp:Label Text="ตั้งค่าการสอบ" BorderStyle="None" ID="lblConfigSetting" runat="server" />
                </div>
            </div>
        <br />
        <br />
    <div>
        <asp:Panel ID="PnlMain" runat="server" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelGrid" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="PnlApproveRegisterGridView" runat="server">
                        <div class="box_body_content">
            	        <div class="box_t_h">::ตั้งค่าการสอบ</div>
            	        <div class="box_textbox_bg_table">
                	    <div class="box_texbox_add_box">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;">
                                        <asp:GridView ID="gvApproveRegis" runat="server"  Width="100%"
                                            BorderWidth="1" EnableTheming="True" AutoGenerateColumns="False" AllowSorting="True"
                                            OnRowDataBound="gvApproveRegis_RowDataBound" CssClass="datatable" CellSpacing="-1" GridLines="None">

                                            <AlternatingRowStyle CssClass="altrow" />
                                            <HeaderStyle BorderStyle="None" />
                                            <PagerStyle  HorizontalAlign="Left" />
                                         
                                            <Columns>
                                                
                                                <asp:TemplateField HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdGvApproveRegis" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="10%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValueGvApproveRegis" runat="server" Text='<%# Bind("Value") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ชื่อฟังก์ชัน" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="30%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFuncationNameGv" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ค่าที่กำหนด" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle Width="10%" CssClass="td-center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSpecifiedValue" runat="server" />
                                                        <asp:DropDownList ID="ddlDateExpiration" runat="server" Width="50px">
                                                            <asp:ListItem Value="0" Text="0"> </asp:ListItem>
                                                            <asp:ListItem Value="1" Text="1"> </asp:ListItem>
                                                            <asp:ListItem Value="2" Text="2"> </asp:ListItem>
                                                            <asp:ListItem Value="3" Text="3"> </asp:ListItem>
                                                            <asp:ListItem Value="4" Text="4"> </asp:ListItem>
                                                            <asp:ListItem Value="5" Text="5"> </asp:ListItem>
                                                            <asp:ListItem Value="6" Text="6"> </asp:ListItem>
                                                            <asp:ListItem Value="7" Text="7"> </asp:ListItem>
                                                            <asp:ListItem Value="8" Text="8"> </asp:ListItem>
                                                            <asp:ListItem Value="9" Text="9"> </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="คำอธิบาย" Visible="true" ItemStyle-HorizontalAlign="Left">
                                                    <ItemStyle Width="50%" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescriptionApproveRegisterGv" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center;" colspan="2">
                                        <asp:Button ID="btnOkApproveRegis" runat="server" Text="ตกลง" CssClass="btn" OnClick="btnOkApproveRegis_Click" />
                                        &nbsp;
                                        <asp:Button ID="btnCancelApproveRegis" runat="server" Text="ยกเลิก" CssClass="btn"
                                            OnClick="btnCancelApproveRegis_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </div>
                        </div>
                        
                    </asp:Panel>

                      

                    <div style="clear:both;"></div>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelGrid">
                                    <ProgressTemplate>
                                        Please wait....
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                    </table>
              
                    <div style="clear:left;"></div>
                    <uc2:UCModalError ID="UCModalError" runat="server" />
                    <uc3:UCModalSuccess ID="UCModalSuccess" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>


    </div>
    <div style="clear:left;"></div>

    
</asp:Content>
