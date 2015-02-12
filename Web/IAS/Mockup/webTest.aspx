<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webTest.aspx.cs" Inherits="IAS.Mockup.webTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnGet" runat="server" Text="Get" OnClick="btnGet_Click" />gg
        <asp:Button ID="btnSet" runat="server" Text="Set" OnClick="btnSet_Click" />
        <asp:Button ID="btnInsert" runat="server" Text="Test" OnClick="btnInsert_Click" />
        <asp:Button ID="btnInsert0" runat="server" Text="Test" OnClick="btnInsert0_Click" />
        <asp:Button ID="Button1" runat="server" Text="Test 2" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="Test 3" OnClick="Button2_Click" />
        <asp:Button ID="btnTest" runat="server" Text="test Month" OnClick="btnTest_Click" />
        <br />
        <asp:Button ID="btnGetData" runat="server" Text="Get Data" OnClick="btnGetData_Click" /><br />
        <asp:Button ID="Button3" runat="server" Text="Get Property Name" OnClick="Button3_Click" />
        <br />
        <asp:Button ID="btnTest1" runat="server" Text="บุคคลทั่วไป" OnClick="btnTest1_Click" />
        <asp:Button ID="Button4" runat="server" Text="บริษัท" OnClick="Button4_Click" />
        <asp:Button ID="Button5" runat="server" Text="สมาคม" OnClick="Button5_Click" />
        <asp:Button ID="Button6" runat="server" Text="คปภ." OnClick="Button6_Click" />
        <asp:Button ID="Button9" runat="server" Text="ทดสอบ" OnClick="Button9_Click" />
        <br />
        <asp:Button ID="btnTest7" runat="server" Text="App Info" OnClick="btnTest7_Click" /><br />
        <asp:Button ID="btnTestExam1" runat="server" Text="Get Exam Temp Edit" OnClick="btnTestExam1_Click"
            Width="184px" />
        <asp:Button ID="btnTestExam2" runat="server" Text="Update Exam Temp Edit" OnClick="btnTestExam2_Click" />
        <asp:Button ID="Button7" runat="server" Text="Submit Exam Temp Edit" />
        <asp:GridView ID="gv" runat="server">
        </asp:GridView>
        <br />
        <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                            oncheckedchanged="CheckBox1_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Id" />
            </Columns>
        </asp:GridView>
        <asp:DropDownList ID="ddl" runat="server">
        </asp:DropDownList>
        <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Button" />
        <asp:Button ID="btnTest8" runat="server" Text="Test Store" 
            onclick="btnTest8_Click" />
        <br />
    </div>
    </form>
</body>
</html>
