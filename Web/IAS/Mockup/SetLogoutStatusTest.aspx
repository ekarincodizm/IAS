<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLogoutStatusTest.aspx.cs" Inherits="IAS.Mockup.SetLogoutStatusTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>


</head>
<body onunload="bodyUnload();" onclick="clicked=true;">
    <script type ="text/javascript" >
        var clicked = false;
        function CheckBrowser() {
            if (clicked == false) {
                //Browser closed
            }
            else {
                //redirected             
                clicked = false;
            }
        }

        function bodyUnload() {
            if (clicked == false)//browser is closed       
            {
                var qstr = "http://localhost:15555/soria/AutoLogout.aspx?UID=" + '<%= Session["currentusername"] %>' + "&LogoutStatus=0&Result=Success";


                var left = (screen.width - 300) / 2;
                var top = (screen.height - 300) / 3;  // for 25% - devide by 4  |  for 33% - devide by 3
                //var window_dimensions = "toolbars=no,menubar=no,location=no,scrollbars=yes,resizable=yes,status=yes"

                var request = GetRequest();
                //request.open("GET", "AutoLogout.aspx", true);
                request.open("GET", "http://localhost:15555/soria/AutoLogout.aspx", true);
                window.open(qstr, 'ออกจากระบบ', 'scrollbars=no,resizable=no,location=0,menubar=no,toolbar=no, width=300, height=200, top=' + top + ', left=' + left);
                request.send();
            }
        }

        function GetRequest() {
            var request = null;
            if (window.XMLHttpRequest) {
                //incase of IE7,FF, Opera and Safari browser            
                request = new XMLHttpRequest();
            }
            else {
                //for old browser like IE 6.x and IE 5.x           
                request = new ActiveXObject('MSXML2.XMLHTTP.3.0');
            }

            return request;
        }
   </script>

    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
