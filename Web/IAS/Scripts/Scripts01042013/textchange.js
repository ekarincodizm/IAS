// jQuery Text Change Event
// http://www.zurb.com/playground/jquery-text-change-custom-event
//
// Simple cross browser detection of text changes
// for input and textarea elements using a jQuery custom event plugin by ZURB
//
// date: 2010-06-19 15:52
// fork from gist: http://gist.github.com/424774
// revision: aef6d4612eecda17f47ad7ec58f199c12061fedd

(function ($) {
    
    $.event.special.textchange = {
        
        setup: function (data, namespaces) {
            $(this).bind('keyup.textchange', $.event.special.textchange.handler);
            $(this).bind('cut.textchange paste.textchange input.textchange', $.event.special.textchange.delayedHandler);
        },
        
        teardown: function (namespaces) {
            $(this).unbind('.textchange');
        },
        
        handler: function (event) {
            $.event.special.textchange.triggerIfChanged($(this));
        },
        
        delayedHandler: function (event) {
            var element = $(this);
            setTimeout(function () {
                $.event.special.textchange.triggerIfChanged(element);
            }, 25);
        },
        
        triggerIfChanged: function (element) {
          var current = element.attr('contenteditable') ? element.html() : element.val();
            if (current !== element.data('lastValue')) {
                element.trigger('textchange',  element.data('lastValue'));
                element.data('lastValue', current);
            }
        }
    };
    
    $.event.special.hastext = {
        
        setup: function (data, namespaces) {
            $(this).bind('textchange', $.event.special.hastext.handler);
        },
        
        teardown: function (namespaces) {
            $(this).unbind('textchange', $.event.special.hastext.handler);
        },
        
        handler: function (event, lastValue) {
            if ((lastValue === '' || lastValue === undefined) && lastValue !== $(this).val()) {
                $(this).trigger('hastext');
            }
        }
    };
    
    $.event.special.notext = {
        
        setup: function (data, namespaces) {
            $(this).bind('textchange', $.event.special.notext.handler);
        },
        
        teardown: function (namespaces) {
            $(this).unbind('textchange', $.event.special.notext.handler);
        },
        
        handler: function (event, lastValue) {
            if ($(this).val() === '' && $(this).val() !== lastValue) {
                $(this).trigger('notext');
            }
        }
    };    

})(jQuery);

