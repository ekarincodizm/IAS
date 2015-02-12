<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoComplet.aspx.cs" Inherits="IAS.Mockup.AutoComplet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function OnContactSelected(source, eventArgs) {

            var hdfValueID = "<%= hdf.ClientID %>";

            document.getElementById(hdfValueID).value = eventArgs.get_value();
            __doPostBack(hdfValueID, "");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:UpdatePanel ID="udpDetailCompany" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePartialRendering="true" EnableScriptGlobalization="true"
        LoadScriptsBeforeUI="true" EnablePageMethods="true" ScriptMode="Release" runat="server">
    </ajaxToolkit:ToolkitScriptManager>
    <asp:TextBox runat="server" ID="txtCompany" CssClass="txt" Width="83%"></asp:TextBox><br />
    ID ::<asp:TextBox ID="txtID" runat="server" Width="400px" ></asp:TextBox><br />
    NAME ::<asp:TextBox ID="txtNmae" runat="server" Width="400px" ></asp:TextBox>
    <ajaxToolkit:AutoCompleteExtender runat="server" ID="AutoCompleteExtender1" TargetControlID="txtCompany"
                                    ServiceMethod="GetAutomCompleteCompany" ServicePath="~/Service/AutoComplete.asmx"
                                    MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="20"
                                    CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=";,:"
                                    ShowOnlyCurrentWordInCompletionListItem="true" FirstRowSelected="true" onclientitemselected="OnContactSelected">
     </ajaxToolkit:AutoCompleteExtender>
     <asp:HiddenField ID="hdf" runat="server" onvaluechanged="hdf_ValueChanged"  />
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
