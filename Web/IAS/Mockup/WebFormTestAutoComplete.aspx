<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormTestAutoComplete.aspx.cs"
    Inherits="IAS.Mockup.WebFormTestAutoComplete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<link type="text/css" rel="Stylesheet" href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/themes/redmond/jquery-ui.css" />--%>
    <%--<script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.5.js"></script>
    <script type="text/javascript" src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.10/jquery-ui.js"></script>--%>
    
    
    <link href="../style/jquery-ui-1.8.10.custom.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.8.3.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.9.2.js" type="text/javascript"></script>
    <meta charset="utf-8">
    <style type="text/css">
        .ui-button
        {
            margin-left: -1px;
        }
        
        .ui-button-icon-only .ui-button-text
        {
            padding: 0.35em;
        }
        
        .ui-autocomplete-input
        {
            margin: 0;
            padding: 0.48em 0 0.47em 0.45em;
        }
    </style>
    <script type="text/javascript">

        function optionSelected(selectedValue) {

            document.title = selectedValue;

        }



        (function ($) {

            $.widget("ui.combobox", {

                _create: function () {

                    var self = this,

                                  select = this.element.hide(),

                                  selected = select.children(":selected"),

                                  value = selected.val() ? selected.text() : "";

                    var input = this.input = $("<input>")

                                  .insertAfter(select)

                                  .val(value)

                                  .autocomplete({

                                      delay: 0,

                                      minLength: 0,

                                      source: function (request, response) {

                                          var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");

                                          response(select.children("option").map(function () {

                                              var text = $(this).text();

                                              if (this.value && (!request.term || matcher.test(text)))

                                                  return {

                                                      label: text.replace(

                                                                           new RegExp(

                                                                                  "(?![^&;]+;)(?!<[^<>]*)(" +

                                                                                  $.ui.autocomplete.escapeRegex(request.term) +

                                                                                  ")(?![^<>]*>)(?![^&;]+;)", "gi"

                                                                           ), "<strong>$1</strong>"),

                                                      value: text,

                                                      option: this

                                                  };

                                          }));

                                      },

                                      select: function (event, ui) {

                                          ui.item.option.selected = true;

                                          self._trigger("selected", event, {

                                              item: ui.item.option

                                          });

                                          //JK

                                          optionSelected(ui.item.option.value);



                                      },

                                      change: function (event, ui) {

                                          if (!ui.item) {

                                              var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),

                                                              valid = false;

                                              select.children("option").each(function () {

                                                  if ($(this).text().match(matcher)) {

                                                      this.selected = valid = true;

                                                      return false;

                                                  }

                                              });

                                              if (!valid) {

                                                  // remove invalid value, as it didn't match anything

                                                  $(this).val("");

                                                  select.val("");

                                                  input.data("autocomplete").term = "";

                                                  return false;

                                              }

                                          }

                                      }

                                  })

                                  .addClass("ui-widget ui-widget-content ui-corner-left");

                    input.data("autocomplete")._renderItem = function (ul, item) {

                        return $("<li></li>")

                                         .data("item.autocomplete", item)

                                         .append("<a>" + item.label + "</a>")

                                         .appendTo(ul);

                    };



                    this.button = $("<button type='button'>&nbsp;</button>")

                                  .attr("tabIndex", -1)

                                  .attr("title", "Show All Items")

                                  .insertAfter(input)

                                  .button({

                                      icons: {

                                          primary: "ui-icon-triangle-1-s"

                                      },

                                      text: false

                                  })

                                  .removeClass("ui-corner-all")

                                  .addClass("ui-corner-right ui-button-icon")

                                  .click(function () {

                                      // close if already visible

                                      if (input.autocomplete("widget").is(":visible")) {

                                          input.autocomplete("close");

                                          return;

                                      }



                                      // pass empty string as value to search for, displaying all results

                                      input.autocomplete("search", "");

                                      input.focus();

                                  });

                },



                destroy: function () {

                    this.input.remove();

                    this.button.remove();

                    this.element.show();

                    $.Widget.prototype.destroy.call(this);

                }

            });

        })(jQuery);



        $(function () {

            $("#<%= ddlTest.ClientID%>").combobox();

//            $("#toggle").click(function () {

//                $("#<%= ddlTest.ClientID%>").toggle();

//            });

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="demo">
        <div class="ui-widget">
            <asp:DropDownList ID="ddlTest" runat="server" >
            </asp:DropDownList>
        </div>
        <%--<button id="toggle">
            Show underlying select</button>--%>
    </div>
    </form>
</body>
</html>
