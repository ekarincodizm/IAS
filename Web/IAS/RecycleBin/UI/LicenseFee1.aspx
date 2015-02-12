<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="LicenseFee1.aspx.cs" Inherits="IAS.UI.LicenseFee1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <%--Datepicker-th--%>
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.4.min.js" type="text/javascript"></script>
    <link href="../style/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-ui-1.8.10.offset.datepicker.min.js" type="text/javascript"></script>
    <script src="../Scripts/LoadScript.js" type="text/javascript"></script>
    <%--Datepicker-th--%>
    <script type="text/javascript">

        function isValid() {
            var txtStartDate = document.getElementById('<%=txtStartDate.ClientID %>');
            var txtEndDate = document.getElementById('<%=txtEndDate.ClientID%>');
            var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');
            lblMsg.innerHTML = "";

            if (txtStartDate.value != '') {
                var items = new Array();
                var items2 = new Array();
                items = txtStartDate.value.split('/');
                items2 = txtEndDate.value.split('/');
                lblMsg.innerHTML = "";
                if (items != null && items.length == 3) {

                    //return true;
                    if (txtEndDate.value != '') {
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
                        return txtEndDate.value != '';
                    }
                }
                else {
                    lblMsg.innerHTML = "Date format is 'dd/mm/yyyy' only.";
                    return false;
                }
            }
            else {

                lblMsg.innerHTML = "Please Enter Start Date.";
                return txtStartDate.value != '';
            }
        }

        function WaterMark(txt, evt) {
            var defaultText = "กรุณากรอกข้อมูล";

            if (txt.value.length == 0 && evt.type == "blur") {
                txt.style.color = "gray";
                txt.value = defaultText;
            }
            if (txt.value == defaultText && evt.type == "focus") {
                txt.style.color = "black";
                txt.value = "";
            }
        }
    </script>
    <div>
        <asp:Panel ID="PnlMain" runat="server" Visible="true" Width="100%">
            <fieldset>
                <legend>ค้นหา</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 30%;">
                            <asp:Label ID="lblLicenseOrder" runat="server" Text="เลขใบสั่งจ่ายขอรับใบอนุญาต"
                                CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtLicenseOrder" runat="server" CssClass="txt" Text="กรุณากรอกข้อมูล"
                                ForeColor="GrayText" onblur="WaterMark(this, event);"
                                onfocus="WaterMark(this, event);"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 30%;">
                            <asp:Label ID="lblStartDate" runat="server" Text="ตั้งแต่วันที่" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtStartDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtStartDate" runat="server" TargetControlID="txtStartDate" Format = "dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                        <td style="text-align: right; width: 10%;">
                            <asp:Label ID="lblEndDate" runat="server" Text="ถึงวันที่" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 40%;">
                            <asp:TextBox ID="txtEndDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtEndDate" runat="server" TargetControlID="txtEndDate" Format = "dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button runat="server" ID="btnSearch" Text="ค้นหา" CssClass="btn" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <br />
            <br />
            <br />
            <table style="width: 100%;">
                <tr>
                    <td style="text-align: center;">
                        <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#999999"
                            HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3"
                            GridLines="Vertical" Width="100%" AllowPaging="True" BorderStyle="None" EnableTheming="True"
                            PageSize="12" ssClass="gridtxlist" PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">"
                            PagerSettings-PreviousPageText="<" PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First"
                            AllowSorting="True" PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center"
                            AutoGenerateColumns="False" PagerStyle-CssClass="viewpager">
                            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                NextPageText="&gt;" PreviousPageText="&lt;" />
                            <RowStyle CssClass="view" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                            <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                            <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                            <AlternatingRowStyle CssClass="view2" />
                            <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="เลขที่ใบสั่งจ่ายขอรับใบอนุญาต" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="30%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblLicenseOrderGv" runat="server" Text='<%# Eval("LicenseOrder") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนคน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeopleGv" runat="server" Text='<%# Eval("People") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="จำนวนเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblMoneyGv" runat="server" Text='<%# Eval("Money") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่จ่าย" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPayDateGv" runat="server" Text='<%# Eval("PayDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานะ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatusGv" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ดำเนินการ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblActionGv" runat="server" Text='<%# Eval("Action") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
