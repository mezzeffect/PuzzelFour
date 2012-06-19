###
$ -> 
	
	ko.bindingHandlers.fixIEPropChange = () ->
		init: (element) ->
			if (element.attachEvent)
				element.attachEvent("onpropertychange", (event) ->
					ko.utils.triggerEvent element, "change"
				);
	return

###

$ ->
	counter1 = 0
	counter2 = 0
	$(":input").each (i, element) ->
		
		if element.addEventListener
			#counter1++
			element.addEventListener 'onpropertychange', (event)-> 
				element?.trigger? 'change'
		
		else if element.attachEvent
			#counter2++
			element.attachEvent 'onpropertychange', (event) -> 
				element?.trigger? 'change'

	#alert "addEventListener = " + counter1
	#alert "attachEvent = " + counter2

