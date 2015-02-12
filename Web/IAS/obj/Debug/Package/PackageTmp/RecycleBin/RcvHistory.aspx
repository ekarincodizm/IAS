<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="RcvHistory.aspx.cs" Inherits="IAS.RecycleBin.RcvHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:Label Text="ประวัติการดาวน์โหลด" BorderStyle="None" ID="lblHistoryDownload" CssClass="Initial"
        runat="server" /><br />
    <br />
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <fieldset>
                <legend class="lbl">ค้นหา:</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblLicenseNumber" runat="server" Text="เลขที่ใบอนุญาต" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblIDCard" runat="server" Text="เลขบัตรประชาชน" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtIDCard" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblFirstName" runat="server" Text="ชื่อ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblLastName" runat="server" Text="นามสกุล" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblStartDate" runat="server" Text="ตั้งแต่วันที่" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtStartDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" runat="server" TargetControlID="txtStartDate" Format = "dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblEndDate" runat="server" Text="ถึงวันที่" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtEndDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtEndDate" runat="server" TargetControlID="txtEndDate" Format = "dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblTypeReceipt" runat="server" Text="ประเภทใบเสร็จ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTypeReceipt" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center;" colspan="4">
                                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                    <ProgressTemplate>
                                        Please wait....
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:GridView ID="gvDetail" runat="server" GridLines="Vertical" Width="100%" AllowPaging="True"
                                    BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False">
                                    <AlternatingRowStyle CssClass="table_alter_row" />
                                    <HeaderStyle BorderStyle="None" />
                                    <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                    <RowStyle CssClass="table_item_row" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="ประเภทใบเสร็จ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeReceiptGv" runat="server" Text='<%# Bind("TypeReceipt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขที่ใบเสร็จ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblReceiptNumberGv" runat="server" Text='<%# Bind("ReceiptNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("IDCard") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmountGv" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="จำนวนครั้งที่พิมพ์" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNumberofPrintGv" runat="server" Text='<%# Bind("NumberofPrint") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
