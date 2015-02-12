<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="SettingBillPayment.aspx.cs" Inherits="IAS.Setting.SettingPaymentExpireDate" %>

<%@ Register Src="~/UserControl/UCModalError.ascx" TagPrefix="uc" TagName="UCModalError" %>
<%@ Register Src="~/UserControl/UCModalSuccess.ascx" TagPrefix="uc" TagName="UCModalSuccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <div class="box_content">
        <div class="box_content_header">
            <div class="box_content_header_text">
                <asp:Label Text="ตั้งค่าใบสั่งจ่าย" BorderStyle="None" ID="lblSettingBillPayment"
                    runat="server" />
            </div>
        </div>
        <div class="clear">
        </div>
        <asp:UpdatePanel ID="uplPaymentExpireDay" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="padding-top: 20px; padding-bottom: 10px">
                            <asp:Label ID="lblPrintBillPayment" runat="server" Text="การตั้งค่าสิทธิ์ในการพิมพ์ใบสั่งจ่าย"
                                Style="font-weight: bold; margin-left: 150px; font-size: 18px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <asp:GridView ID="gvConfigPrint" runat="server" Width="70%" CssClass="datatable"
                                ShowHeaderWhenEmpty="True" AllowSorting="True" AutoGenerateColumns="False">
                                <AlternatingRowStyle CssClass="altrow" />
                                <HeaderStyle BorderStyle="None" />
                                <EmptyDataRowStyle CssClass="td-center" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <div style="text-align: center; margin-left: 0; margin-right: 0">
                                        ไม่พบข้อมูล...</div>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="ประเภทใบสั่งจ่าย">
                                        <HeaderStyle HorizontalAlign="Center" Width="40%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%# Bind("ITEM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สิทธิ์ในการพิมพ์ใบสั่งจ่าย">
                                        <HeaderStyle HorizontalAlign="Center" Width="60%" />
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="RBLConfigPrint" runat="server" SelectedValue='<%#Eval("ITEM_VALUE")%>'
                                                RepeatDirection="Horizontal" CssClass="bordernone" OnSelectedIndexChanged="RBLConfigPrint_Change"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="สมาคม" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="บริษัทประกันภัย" Value="1"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="60%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupCode" runat="server" Text='<%# Bind("GROUP_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <br />
                            <asp:Button ID="btnSave" runat="server" Text="บันทึก" CssClass="btn" OnClick="btnSave_Click" />&nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td style="padding-top: 20px; padding-bottom: 10px">
                            <asp:Label ID="lblHeadRecordPerBillPayment" runat="server" Text="ตั้งค่ารายการต่อใบสั่งจ่าย"
                                Style="font-weight: bold; margin-left: 150px; font-size: 18px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblSubRecordPerBillPayment" runat="server" Text="รายการสูงสุดที่จะแสดงในใบสั่งจ่ายค่าธรรมเนียมต่างๆ สูงสุด"></asp:Label>
                            &nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtRecordPerBillPayment" runat="server" onpaste="return false;"
                                ondrop="return false;" Width="5%" onkeypress="return runScript(event)" MaxLength="1"
                                AutoComplete="off" Style="text-align: center"></asp:TextBox>
                            &nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server" Text="ต่อ 1 ใบสั่งจ่าย"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding-top: 20px; padding-bottom: 20px">
                            <asp:Button ID="btnSaveRecordPerPage" runat="server" Text="บันทึก" CssClass="btn"
                                OnClick="btnSaveRecordPerPage_Click" />&nbsp;
                            <asp:Button ID="btnCancelRecordPerPage" runat="server" Text="ยกเลิก" CssClass="btn"
                                OnClick="btnCancelRecordPerPage_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <%--<table width="100%">
                    <tr align="left">
                        <td style="padding-top: 20px; padding-bottom: 20px">
                            <asp:Label ID="lblHeadBillPaymentExpireDate" runat="server" Text="ตั้งค่าวันหมดอายุใบสั่งจ่าย"
                                Style="font-weight: bold; margin-left: 150px; font-size: 18px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvPaymentExpireDate" runat="server" AutoGenerateColumns="false"
                                GridLines="None" CssClass="datatable" Width="60%">
                                <Columns>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="40%">
                                        <HeaderTemplate>
                                            ประเภทใบสั่งจ่าย</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDESCRIPTION" runat="server" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="20%">
                                        <HeaderTemplate>
                                            วันหมดอายุ(วัน)</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentExpireDay" runat="server" Text='<%# Bind("PAYMENT_EXPIRE_DAY") %>'
                                                Visible="false"></asp:Label>
                                            <asp:TextBox ID="txtExpireDay" runat="server" Text='<%# Bind("PAYMENT_EXPIRE_DAY") %>'
                                                CssClass="textbox" onkeypress="return NumberOnly(event)" Width="150px" Style="text-align: center"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="td-center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding-top: 20px">
                            <asp:Button ID="btnSavePaymentExpireDay" runat="server" Text="บันทึก" OnClick="btnSavePaymentExpireDay_Click"
                                CssClass="btn" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnCancelPaymentExpireDay" runat="server" Text="ยกเลิก" CssClass="btn"
                                OnClick="btnCancelPaymentExpireDay_Click" />
                        </td>
                    </tr>
                </table>--%>
                <br />
                <uc:UCModalSuccess runat="server" ID="UCModalSuccess" />
                <uc:UCModalError runat="server" ID="UCModalError" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <script type="text/javascript">
            function NumberOnly(e) {
                var charCode = (e.which) ? e.which : e.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }

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
    </div>
</asp:Content>
