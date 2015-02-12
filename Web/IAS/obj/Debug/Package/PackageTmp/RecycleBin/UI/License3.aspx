<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Home.Master" AutoEventWireup="true"
    CodeBehind="License3.aspx.cs" Inherits="IAS.UI.License3" %>

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
            var txtTestDate = document.getElementById('<%=txtTestDate.ClientID %>');
            var lblMsg = document.getElementById('<%=lblMsg.ClientID %>');
            lblMsg.innerHTML = "";

            if (txtTestDate.value != '') {
                var items = new Array();
                items = txtTestDate.value.split('/');
                lblMsg.innerHTML = "";
                if (items != null && items.length == 3) {

                    return true;

                }
                else {
                    lblMsg.innerHTML = "Date format is 'dd/mm/yyyy' only.";
                    return false;
                }
            }
            else {

                lblMsg.innerHTML = "Please Enter Date.";
                return txtTestDate.value != '';
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
                <legend class="lbl">ค้นหา</legend>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblTestPlace" runat="server" Text="สถานที่สอบ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTestPlace" runat="server" CssClass="txt" Text="กรุณากรอกข้อมูล"
                                ForeColor="GrayText" onblur="WaterMark(this, event);"
                                onfocus="WaterMark(this, event);"></asp:TextBox>
                        </td>
                        <td style="text-align: right; width: 20%;">
                            <asp:Label ID="lblTestDate" runat="server" Text="วันที่สอบ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTestDate" runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CLDtxtTestDate" runat="server" TargetControlID="txtTestDate" Format = "dd/MM/yyyy">
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
                            <asp:Label ID="lblTestTime" runat="server" Text="เวลาที่สอบ" CssClass="lbl"></asp:Label>
                            <span>:</span>
                        </td>
                        <td style="text-align: left; width: 30%;">
                            <asp:TextBox ID="txtTestTime" runat="server" CssClass="txt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" CssClass="lbl"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;" colspan="4">
                            <asp:Button runat="server" ID="btnSearch" Text="ค้นหา" CssClass="btn" OnClientClick="javascript:isValid(); return false;" />
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
                                <asp:TemplateField HeaderText="เลขบัตรประชาชน" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="20%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdNumberGv" runat="server" Text='<%# Eval("IdNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ชื่อ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameGv" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="นามสกุล" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSurNameGv" runat="server" Text='<%# Eval("SurName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="วันที่สมัคร" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTestGv" runat="server" Text='<%# Eval("DateTest") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="เวลาที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTimeTestGv" runat="server" Text='<%# Eval("TimeTest") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สถานที่สอบ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlaceTestGv" runat="server" Text='<%# Eval("PlaceTest") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ปัญหา" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProblemGv" runat="server" Text='<%# Eval("Problem") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="สาเหตุ" Visible="true" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10%" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblProblemGv" runat="server" Text='<%# Eval("Cause") %>'></asp:Label>
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
