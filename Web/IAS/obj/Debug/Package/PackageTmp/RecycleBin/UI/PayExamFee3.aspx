<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="PayExamFee3.aspx.cs" Inherits="IAS.UI.PayExamFee3" %>

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
            var txtPayDate = document.getElementById('<%=txtPayDate.ClientID %>');
            var txtReceiptDate = document.getElementById('<%=txtReceiptDate.ClientID%>');
            var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');
            lblMsg.innerHTML = "";

            if (txtPayDate.value != '') {
                var items = new Array();
                var items2 = new Array();
                items = txtPayDate.value.split('/');
                items2 = txtReceiptDate.value.split('/');
                lblMsg.innerHTML = "";
                if (items != null && items.length == 3) {

                    //return true;
                    if (txtReceiptDate.value != '') {
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
                        return txtReceiptDate.value != '';
                    }
                }
                else {
                    lblMsg.innerHTML = "Date format is 'dd/mm/yyyy' only.";
                    return false;
                }
            }
            else {

                lblMsg.innerHTML = "Please Enter Start Date.";
                return txtPayDate.value != '';
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
            <div align="center">
                <table style="border: 3px solid black; width: 90%; text-align: center; vertical-align: middle;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblReceiptNumber" runat="server" Text="เลขใบสั่งจ่าย" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txReceiptNumber" runat="server" CssClass="txt" Text="กรุณากรอกข้อมูล"
                                ForeColor="GrayText" onblur="WaterMark(this, event);"
                                onfocus="WaterMark(this, event);"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblPayDate" runat="server" Text="วันที่จ่าย" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtPayDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtPayDate" runat="server" TargetControlID="txtPayDate" Format = "dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblPeople" runat="server" Text="จำนวนคน" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtPeople" runat="server" CssClass="txt" Text="กรุณากรอกข้อมูล"
                                ForeColor="GrayText" onblur="WaterMark(this, event);"
                                onfocus="WaterMark(this, event);"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblReceiptDate" runat="server" Text="วันที่ออกใบเสร็จ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtReceiptDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtReceiptDate" runat="server" TargetControlID="txtReceiptDate" Format = "dd/MM/yyyy">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:GridView ID="gvList" runat="server" BackColor="White" BorderColor="#999999"
                                HeaderStyle-BackColor="#999999" BorderWidth="1px" CaptionAlign="Top" CellPadding="3" GridLines="Vertical" Width="100%"
                                AllowPaging="True" BorderStyle="None" EnableTheming="True" PageSize="12" ssClass="gridtxlist"
                                PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText=">" PagerSettings-PreviousPageText="<"
                                PagerSettings-LastPageText="Last" PagerSettings-FirstPageText="First" AllowSorting="True"
                                PagerSettings-PageButtonCount="10" PagerStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                PagerStyle-CssClass="viewpager">
                                <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                                    NextPageText="&gt;" PreviousPageText="&lt;" />
                                <RowStyle CssClass="view" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                                <PagerStyle HorizontalAlign="Center" CssClass="viewpager" />
                                <HeaderStyle Font-Bold="True" CssClass="viewheader" />
                                <AlternatingRowStyle CssClass="view2" />
                                <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblIdNumberGv" runat="server" Text='<%# Eval("IdNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSurNameGv" runat="server" Text='<%# Eval("SurName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="วันที่สมัคร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDateTestGv" runat="server" Text='<%# Eval("DateTest") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ชำระเงิน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPayment" runat="server" Checked='<%# Eval("Payment") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <asp:HiddenField ID="HiddenField_ID" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
