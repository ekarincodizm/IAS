<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="Invoice.aspx.cs" Inherits="IAS.RecycleBin.Invoice" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Detail" runat="server">
    <asp:Label Text="สร้างใบสั่งจ่าย" BorderStyle="None" ID="lblCreateOrder" CssClass="Initial"
        runat="server" /><br />
    <br />
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <asp:UpdatePanel ID="UpdatePanelSearch" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <fieldset>
                        <legend class="lbl">ค้นหา:</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="text-align: right; width: 20%;">
                                    <asp:Label ID="lblTypeOrder" runat="server" Text="ประเภทใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                                    <span>:</span>
                                </td>
                                <td style="text-align: left; width: 30%;">
                                    <asp:DropDownList ID="ddlTypeOrder" runat="server" CssClass="ddl">
                                    </asp:DropDownList>
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
                                <td style="text-align: center;" colspan="4">
                                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelSearch">
                                        <ProgressTemplate>
                                            <uc1:UCLoadingProgress ID="UCLoadingProgress1" runat="server" />
                                            Please wait image uploaded....</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:GridView ID="gvDetail" runat="server" GridLines="Vertical" Width="100%" AllowPaging="True"
                                    BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False">
                                    <AlternatingRowStyle CssClass="table_alter_row" />
                                    <HeaderStyle BorderStyle="None" />
                                    <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                    <RowStyle CssClass="table_item_row" />
                                    <Columns>
                                        <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="5%" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckID" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ประเภท" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeDocumentGv" runat="server" Text='<%# Bind("TypeDocument") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="25%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblIDCardGv" runat="server" Text='<%# Bind("IDCard") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstNameGv" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastNameGv" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamDateGv" runat="server" Text='<%# Bind("ExamDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="รหัสสอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblExamIDGv" runat="server" Text='<%# Bind("ExamID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="gvLicenseNumber" runat="server" GridLines="Vertical" Width="100%"
                                    AllowPaging="True" BorderStyle="None" EnableTheming="True" AutoGenerateColumns="False">
                                    <AlternatingRowStyle CssClass="table_alter_row" />
                                    <HeaderStyle BorderStyle="None" />
                                    <PagerStyle CssClass="table_item_row" HorizontalAlign="Right" />
                                    <RowStyle CssClass="table_item_row" />
                                    <Columns>
                                        <asp:TemplateField Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="5%" />
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="CheckAll" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckID" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="เลขใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="25%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLicenseNumberGv" runat="server" Text='<%# Bind("LicenseNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ครั้งที่ต่ออายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRenewalGv" runat="server" Text='<%# Bind("Renewal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่ต่ออายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRenewalStartDateGv" runat="server" Text='<%# Bind("RenewalStartDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="วันที่หมดอายุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblRenewalEndDateGv" runat="server" Text='<%# Bind("RenewalEndDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstNameRenewalGv" runat="server" Text='<%# Bind("FirstNameRenewal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="20%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastNameRenewalGv" runat="server" Text='<%# Bind("LastNameRenewal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table align="center" style="width: 100%;">
                        <tr>
                            <td style="text-align: right;" colspan="2">
                                <asp:Button ID="btnOk" runat="server" Text="ตกลง" CssClass="btn" />
                            </td>
                            <td style="text-align: left;" colspan="2">
                                <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="btn" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
