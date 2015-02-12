<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Site1.Master" AutoEventWireup="true"
    CodeBehind="regExplain.aspx.cs" Inherits="IAS.Register.regExplain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="detail" runat="server">
    <script type="text/javascript">

        $("#Panel1").hide();
 
</script>

<div class="box_content">
        	<div class="box_content_header">
            	<div class="box_content_header_text">
           <asp:Label Text="ลงทะเบียน" BorderStyle="None" ID="lblExplain" runat="server" />
                </div>
            </div>


    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <td style="text-align: right; width: 42%;">
                <asp:Label ID="lblMemberType" runat="server" CssClass="lbl" Text="ประเภทสมาชิก"></asp:Label>
                <span>:</span>
                <asp:DropDownList ID="ddlMemberType" runat="server" CssClass="ddl" Width="200px">
                </asp:DropDownList>
            </td>
            <td style="text-align: left; width: 58%;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: left; width: 30%; height: 17px;">
                &nbsp;
            </td>
            <td style="text-align: right; width: 20%; height: 17px;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 30%; height: 17px;">
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%;min-width:100%" align="center">
        <tr>
            <td>
                <table style="width: 100%;min-width:100%; border-width: 1px; border-color: #bebebe; border-style: solid">
                    <tr>
                        <td style="text-align: center;width:100%;min-width:100%" colspan="2">
                            <asp:Label ID="lblDetailHeader" runat="server" Text="เงื่อนไขการสมัครสมาชิก"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblDetailNumber1" runat="server" Text="1" CssClass="lbl"></asp:Label>
                            <span>.</span>
                            <asp:Label ID="lblDetail1" runat="server" Text="ผู้ที่สมัครสมาชิกจะต้องใช้คำที่เหมาะสม ไม่หยาบคายหรือส่อไปในทางลามกอนาจาร"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblDetailNumber2" runat="server" Text="2" CssClass="lbl"></asp:Label>
                            <span>.</span>
                            <asp:Label ID="lblDetail2" runat="server" Text="ผู้ที่สมัครสมาชิกต้องกรอกข้อมูลที่เป็นจริงให้ครบทุกข้อ เพื่อสิทธิประโยชน์ของท่าน"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblDetailNumber3" runat="server" Text="3" CssClass="lbl"></asp:Label>
                            <span>.</span>
                            <asp:Label ID="lblDetail3" runat="server" Text="สมาชิกต้องปฎิบัติตามกฎระเบียบและข้อตกลงอย่างเคร่งครัด เพื่อความสงบเรียบร้อยในกรณีที่ท่านละเมิดกฏ Admin และผู้ดูแลมีสิทธิ์ยกเลิกการเป็นสมาชิกได้"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblDetailNumber4" runat="server" Text="4" CssClass="lbl"></asp:Label>
                            <span>.</span>
                            <asp:Label ID="lblDetail4" runat="server" Text="เพื่อความเป็นส่วนตัวและความปลอดภัยในข้อมูลของท่านเอง ผู้ดูแลขอแจ้งให้ท่านทราบว่า เป็นหน้าที่ของท่านเองในการรักษาชื่อ Login และ Password ของท่านให้ดีโดยไม่บอกให้ผู้อื่นทราบ"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblDetailNumber5" runat="server" Text="5" CssClass="lbl"></asp:Label>
                            <span>.</span>
                            <asp:Label ID="lblDetail5" runat="server" Text="ข้อมูลของสมาชิกจะถูกเก็บเป็นความลับอย่างสูงสุด ผู้ดูแลจะไม่เปิดเผยข้อมูลของท่านเพื่อประโยชน์ทางการค้า หรื่อเพื่อประโยชน์ในด้านอื่นๆทั้งสิ้น"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            <asp:Label ID="lblDetailNumber6" runat="server" Text="6" CssClass="lbl"></asp:Label>
                            <span>.</span>
                            <asp:Label ID="lblDetail6" runat="server" Text="ผู้ดูแลขอสงวนสิทธิ์ในการยกเลิกความเป็นสมาชิกหรือหยุดให้บริการระบบสมาชิกเมื่อใดก็ได้ โดยไม่ต้องแจ้งให้สมาชิกทราบล่วงหน้า"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td style="text-align: right; width: 20%;">
                &nbsp;
            </td>
            <td style="text-align: right; width: 30%;">
                &nbsp;<asp:CheckBox ID="isComfirm" runat="server" Text="ข้าพเจ้ายอมรับเงื่อนไขการสมัครทุกประการ" />
            </td>
            <td style="text-align: right; width: 20%;">
                &nbsp;
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: right; width: 25%;">
                &nbsp;
            </td>
            <td style="text-align: center; width: 30%;" colspan="2">
                <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="ตกลง" OnClick="btnSubmit_Click" />
                <%--</td>
            <td style="text-align: right; width: 20%;">--%>
                &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn" Text="ยกเลิก" OnClick="btnCancel_Click" />
            </td>
            <td style="text-align: left; width: 30%;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4" style="text-align: center;">
                &nbsp;
            </td>
        </tr>
    </table>

    </div>
    <div style="clear:left;"></div>
</asp:Content>
