<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

   
    <script src="ScriptModal/jquery-1.7.1.min.js"></script>
    <script src="ScriptModal/jquery-ui-1.8.17.custom.min.js"></script>

    <link href="StyleModal/jquery-ui-1.8.17.custom.css" rel="stylesheet" />

    <style type="text/css">
        .ButtonStyle {
            border: thin solid #C6C3DE;
            background-color: #ECEBFA;
            margin-left: 5px;
            width: auto;
            height: 25px;
            padding-left: 5px;
            padding-right: 5px;
            font-size: 13px;
            font-weight: bold;
            padding-top: 3px;
            padding-bottom: 3px;
            text-align: center;
            color: #636363;
            border-radius: 5px;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
        }

            .ButtonStyle:hover {
                border-color: #726E96;
                color: #636363;
                cursor: pointer;
            }

        .DynamicDialogStyle {
            /*background:none;
            background-color:transparent;*/
            background-color: #F7FAFE;
            font-size: small;
        }

        label {
            width: 100px;
        }
    </style>
    <script>
        $(function () {
            $("#LocalDialogModal").dialog({
                dialogClass: 'DynamicDialogStyle',
                autoOpen: false,
                resizable: false,
                draggable: false,
                modal: true,

                open: function (type, data) {
                    $(this).parent().appendTo("form");
                },

                width: 450,
                height: 150,
                title: "กำหนดการสอบ"
            });

        });

        function openDialog(registerID, registerDate) {
            $('#LocalDialogModal').dialog("open");

            $("#<%=txtExam.ClientID%>").val(registerDate);
        }

        function closeDialog(id) {
            $('#LocalDialogModal').dialog("close");
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="Scriptmanager1" runat="server">
        </asp:ScriptManager>
        <div>
            <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" DayNameFormat="Full" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" OnDayRender="Calendar1_DayRender" BorderWidth="1px" ShowGridLines="True" OnVisibleMonthChanged="Calendar1_VisibleMonthChanged" Height="418px">
                <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" HorizontalAlign="Left" />
                <DayStyle HorizontalAlign="Left" />
                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                <OtherMonthDayStyle ForeColor="#CC9966" />
                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                <SelectorStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
            </asp:Calendar>
        </div>
        <div id="LocalDialogModal" style="display: block">
            <table class="ui-accordion">
                <tr>
                    <td style="width: 20%; text-align: right">
                        <asp:Label ID="lblExam" runat="server"
                            Text="วันที่สอบ: "></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtExam" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="width: 20%; text-align: right">
                        <asp:Label ID="lblTimeExam" runat="server"
                            Text="เวลาที่สอบ: "></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtTimeExam" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right">
                        <asp:Label ID="lblQuantity" runat="server"
                            Text="จำนวนที่รับสมัคร: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>

                    </td>
                    <td style="width: 20%; text-align: right">
                        <asp:Label ID="lblTestingLocations" runat="server"
                            Text="สถานที่สอบ: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTestingLocations" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%; text-align: right">
                        <asp:Label ID="lblFee" runat="server"
                            Text="ค่าธรรมเนียม: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFee" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="width: 20%; text-align: right">
                        <asp:Label ID="lblNumberOfCandidates" runat="server"
                            Text="จำนวนสมัคร: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNumberOfCandidates" runat="server" Width="80%" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Button ID="btnRegister" runat="server"
                            Text="สมัคร" CssClass="ButtonStyle" Style="margin: 0px;" OnClick="btnRegister_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server"
                            Text="ยกเลิก" CssClass="ButtonStyle" Style="margin: 0px;" OnClick="btnCancel_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>

</body>
</html>
