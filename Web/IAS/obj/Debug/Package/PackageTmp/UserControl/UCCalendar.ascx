<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCCalendar.ascx.cs"
    Inherits="IAS.UserControl.UCCalendar" %>
<fieldset style="width: 90%; margin: 0px auto 10px auto;">
                    <legend>
                        <asp:Label ID="Label1" runat="server" BorderStyle="None" Text="ค้นหารหัสสอบ" />
                    </legend>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblYardGroup" runat="server" CssClass="lbl" Text="กลุ่มสถานที่สอบ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlPlaceGroup" runat="server" CssClass="ddl" Width="150px"
                                    AutoPostBack="True" Enabled="false" 
                                    onselectedindexchanged="ddlPlaceGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblTypeLicense" runat="server" CssClass="lbl" Text="ประเภทใบอนุญาต"></asp:Label>:
                                </span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlTypeLicense" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblYard" runat="server" CssClass="lbl" Text="สถานที่สอบ"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlPlace" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblYear" runat="server" CssClass="lbl" Text="ปี"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:TextBox ID="txtYear" runat="server" Style="text-align: center" Width="120" />
                                <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" RefValues=""
                                    ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID="" TargetButtonUpID=""
                                    TargetControlID="txtYear" Width="120" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                <asp:Label ID="lblMonth" runat="server" CssClass="lbl" Text="เดือน"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%;">
                                <span>
                                    <asp:Label ID="lblDisplay" runat="server" CssClass="lbl" Text="แสดงผล"></asp:Label>
                                    :</span>
                            </td>
                            <td style="text-align: left; width: 30%;">
                                <asp:RadioButtonList ID="rblDisplay" runat="server" AutoPostBack="True" CssClass="rb"
                                    OnSelectedIndexChanged="rblDisplay_SelectedIndexChanged" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Text="ปฏิทิน" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="ตาราง" Value="2"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%; height: 17px;">
                                <asp:Label ID="lblTimeExam" runat="server" CssClass="lbl" Text="เวลาสอบ"></asp:Label>
                                :
                            </td>
                            <td style="text-align: left; width: 30%; height: 17px;">
                                <asp:DropDownList ID="ddlTime" runat="server" CssClass="ddl" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align: right; width: 20%; height: 17px;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 30%; height: 17px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; width: 20%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 30%;">
                                &nbsp;
                            </td>
                            <td style="text-align: left; width: 20%;">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="ค้นหา" OnClick="btnSearch_Click" />
                            </td>
                            <td style="text-align: left; width: 30%;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
               <div style="width: 90%; margin: 0px auto 10px auto;">
                    <asp:Calendar ID="cldExam" runat="server" BackColor="White" BorderColor="#999999"
                        CellPadding="4" DayNameFormat="Full" Font-Names="Verdana" Font-Size="10pt" ForeColor="Black"
                        Height="382px" Width="100%" ShowGridLines="True" OnDayRender="cldExam_DayRender"
                        OnSelectionChanged="cldExam_SelectionChanged" OnVisibleMonthChanged="cldExam_VisibleMonthChanged">
                        <DayHeaderStyle BackColor="#CCCCCC" BorderColor="Black" Font-Bold="True" Height="50px"
                            HorizontalAlign="Left" />
                        <DayStyle BorderColor="Black" Height="50px" HorizontalAlign="Center" Font-Bold="True" />
                        <TitleStyle BorderColor="Black" Height="15px" HorizontalAlign="Center" />
                        <TodayDayStyle BackColor="#FFFF99" />
                    </asp:Calendar>
                    <br />
                    <asp:GridView ID="gvExamSchedule" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CaptionAlign="Top" CellPadding="3" EnableTheming="True" GridLines="Vertical"
                        HeaderStyle-BackColor="#999999" PagerSettings-FirstPageText="First" PagerSettings-LastPageText="Last"
                        PagerSettings-Mode="NumericFirstLast" PagerSettings-NextPageText="&gt;" PagerSettings-PageButtonCount="10"
                        PagerSettings-PreviousPageText="&lt;" PagerStyle-CssClass="viewpager" 
                        PagerStyle-HorizontalAlign="Center" CssClass="gridtxlist" Width="100%" 
                        OnRowDataBound="gvExamSchedule_RowDataBound" EmptyDataText="ไม่มีข้อมูล..."
                        onpageindexchanging="gvExamSchedule_PageIndexChanging">
                        <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast"
                            NextPageText="&gt;" PreviousPageText="&lt;" />
                        <RowStyle CssClass="view" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" />
                        <PagerStyle CssClass="viewpager" HorizontalAlign="Center" />
                        <HeaderStyle CssClass="viewheader" Font-Bold="True" />
                        <AlternatingRowStyle CssClass="view2" />
                        <EmptyDataRowStyle BackColor="#4C4C4C" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="รหัสสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="16%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkExamNumber" runat="server" Text='<%# Bind("TESTING_NO") %>'
                                        OnClick="lnkExamNumber_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="วันที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="16%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamDate" runat="server" Text='<%# Bind("TESTING_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เวลาสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="16%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblExamTime" runat="server" Text='<%# Bind("TEST_TIME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="กลุ่มสนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="16%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblGroupExamYard" runat="server" Text='<%# Bind("EXAM_PLACE_GROUP_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สนามสอบ" ItemStyle-HorizontalAlign="Center" Visible="false">
                                <ItemStyle Width="16%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceCode" runat="server" Text='<%# Bind("EXAM_PLACE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="สถานที่สอบ" ItemStyle-HorizontalAlign="Center" Visible="true">
                                <ItemStyle Width="16%" />
                                <ItemTemplate>
                                    <asp:Label ID="lblPlaceName" runat="server" Text='<%# Bind("EXAM_PLACE_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
