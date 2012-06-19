
/*
$ -> 
	
	ko.bindingHandlers.fixIEPropChange = () ->
		init: (element) ->
			if (element.attachEvent)
				element.attachEvent("onpropertychange", (event) ->
					ko.utils.triggerEvent element, "change"
				);
	return
*/


(function() {

  $(function() {
    var counter1, counter2;
    counter1 = 0;
    counter2 = 0;
    return $(":input").each(function(i, element) {
      if (element.addEventListener) {
        return element.addEventListener('onpropertychange', function(event) {
          return element != null ? typeof element.trigger === "function" ? element.trigger('change') : void 0 : void 0;
        });
      } else if (element.attachEvent) {
        return element.attachEvent('onpropertychange', function(event) {
          return element != null ? typeof element.trigger === "function" ? element.trigger('change') : void 0 : void 0;
        });
      }
    });
  });

}).call(this);
