<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestUploadData.aspx.cs"
    Inherits="IAS.Mockup.TestUploadData" %>

<%@ Register Src="../UserControl/UCLoadingProgress.ascx" TagName="UCLoadingProgress"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script src="../Scripts/Scripts01042013/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
<head runat="server">
    <title></title>
    <script type="text/javascript" >
//        function newpage(page) {
//            window.open(replaceBackSlash(page), null, "height=250, width=250,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no");
//            window.open("../Register/ImagePopup.aspx?image=<%# Eval("TargetFullName") %>",null,"height=250, width=250,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no")
//        }

//        function replaceBackSlash(path) {
//            var s = path.replace("\\", "||");
        //        }
        $(document).ready(function () {
             $("#fUpload").attr('accept', '.CSV');
         
        });
        
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data" >
    <div>
    <table>
        <tr>
            <td><asp:TextBox runat="server" ID="txtIdCard" MaxLength="13" ></asp:TextBox></td>
            <td><asp:DropDownList runat="server" ID="ddlAttechType" >
                <asp:ListItem Value="1"  Text="person"></asp:ListItem>
                <asp:ListItem Value="2" Text="company" ></asp:ListItem>
                <asp:ListItem Value="3" Text="Assoc" ></asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:FileUpload ID="fUpload" runat="server"  accept="image/jpeg, image/gif, Application/x-msexcel, .CSV"   /></td><td><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
         <%--       <asp:RegularExpressionValidator ID="rexp" runat="server" ControlToValidate="fUpload"
     ErrorMessage="Only .gif, .jpg, .png, .tiff and .jpeg"
     ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.csv)$" ></asp:RegularExpressionValidator>--%>
            </td>

        </tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        <tr><td colspan="2">
             <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" 
                 onselectedindexchanged="gv_SelectedIndexChanged" Width="421px"  onrowdeleting="gv_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" />
                        <asp:BoundField DataField="AttechType" HeaderText="AttechType" />
                        <asp:BoundField DataField="FileName" HeaderText="FileName" />
                        <asp:BoundField DataField="FileType" HeaderText="FileType" />
                        <asp:BoundField DataField="FileSize" HeaderText="FileSize" />
                        <asp:BoundField DataField="TargetFileName" HeaderText="TargetFileName" />
                        <asp:BoundField DataField="TargetContainer" HeaderText="TargetContainer" />
                        <asp:BoundField DataField="TargetFullName" HeaderText="TargetFullName" />
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:CommandField ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />

                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
              
                              
                                    <a href='javascript:window.open("../Register/ImagePopup.aspx?image=<%# Eval("TargetFullName") %>",null,"height=250, width=250,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no");' id="line" >Link</a>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
        </td></tr>
    </table>

            
                
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                <asp:Button ID="btnDownload" runat="server" Text="Download" 
                onclick="btnDownload_Click" />
               


    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Image ID="myImage" ImageUrl="~/ImageHandler.ashx?id=1" runat="server" />
    <a href='javascript:window.open("../Register/ImagePopup.aspx",null,"height=250, width=250,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no");' id="line" >Link</a>
    
  <%--  <input type="file" name="upload"  accept="image/jpeg, image/gif, Application/x-msexcel, .CSV" />--%>
    </form>
</body>
</html>
