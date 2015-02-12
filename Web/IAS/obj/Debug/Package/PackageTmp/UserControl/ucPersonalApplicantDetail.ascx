<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPersonalApplicantDetail.ascx.cs"
    Inherits="IAS.UserControl.ucPersonalApplicantDetail" %>
<table style="width: 100%;">
    <tr>
        <td style="text-align: right; width: 20%;">
            <span>
                <asp:Label ID="lblDetailTitileName" runat="server" CssClass="lbl" Text="คำนำหน้าชื่อ"></asp:Label>
            </span>&nbsp;
        </td>
        <td style="text-align: left; width: 25%;">
            <asp:DropDownList ID="ddlDetailTitleName" runat="server" CssClass="ddl">
            </asp:DropDownList>
        </td>
        <td style="text-align: right; width: 22%;">
            <span>
                <asp:Label ID="lblDetailName" runat="server" CssClass="lbl" Text="ชื่อ"></asp:Label>
            </span>
        </td>
        <td style="text-align: left; width: 25%;">
            <asp:TextBox ID="txtDetailName" runat="server" CssClass="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailLastName" runat="server" CssClass="lbl" Text="นามสกุล"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailLastName" runat="server" CssClass="txt"></asp:TextBox>
        </td>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailIdCardNumber" runat="server" CssClass="lbl" Text="เลขบัตรประชาชน"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailIdCardNumber" runat="server" CssClass="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailSex" runat="server" CssClass="lbl" Text="เพศ"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtSex" runat="server" CssClass="txt"></asp:TextBox>
        </td>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailBirthDay" runat="server" CssClass="lbl" Text="วันเกิด"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailBirthDay" runat="server" CssClass="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailEducation" runat="server" CssClass="lbl" Text="ระดับการศึกษา"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:DropDownList ID="ddlDetailEducation" runat="server" AutoPostBack="True" CssClass="ddl">
            </asp:DropDownList>
        </td>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailExamDate" runat="server" CssClass="lbl" Text="วันที่สอบ"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailExamDate" runat="server" CssClass="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <asp:Label ID="lblDetailPersonExam" runat="server" CssClass="lbl" Text="รหัสผู้สมัครสอบ"></asp:Label>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailPersonExam" runat="server" CssClass="txt"></asp:TextBox>
        </td>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailExamCode" runat="server" CssClass="lbl" Text="รหัสสอบ"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailExamCode" runat="server" CssClass="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailYardCode" runat="server" CssClass="lbl" Text="รหัสสนามสอบ"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailYardCode" runat="server" CssClass="txt"></asp:TextBox>
        </td>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailAssocExam" runat="server" CssClass="lbl" Text="รหัส สนง.ประกันภัยที่สมัครสอบ"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailAssocExam" runat="server" CssClass="txt"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
            <span>
                <asp:Label ID="lblDetailCompany" runat="server" CssClass="lbl" Text="รหัสบริษัทประกันภัย"></asp:Label>
            </span>
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtDetailCompany" runat="server" CssClass="txt"></asp:TextBox>
        </td>
        <td style="text-align: right;">
        </td>
        <td style="text-align: left;">
            <br />
        </td>
    </tr>
    <tr>
        <td style="text-align: right;">
        </td>
        <td style="text-align: right;">
        </td>
        <td style="text-align: left;">
        </td>
        <td style="text-align: left;">
        </td>
    </tr>
    <tr>
        <td style="text-align: center;" colspan="4">
            <asp:Button ID="btnDetailSubmit" runat="server" CssClass="btn" Text="ตกลง" />
        </td>
    </tr>
    <tr>
        <td colspan="4" style="text-align: center;">
            &nbsp;
        </td>
    </tr>
</table>
