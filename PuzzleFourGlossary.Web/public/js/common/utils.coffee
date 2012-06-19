window.utils =
	formater:
		formatPhone: (str) ->
			npa=str.substring(0,3)
			nxx=str.substring(3,6)
			line=str.substring(6,10)
			"#{npa}-#{nxx}-#{line}"
		
		formatDate: (str) ->
			d = new Date(str.replace(/\//g, ' '))
			d.toDateString()

		returnDate:(str) ->
			d = new Date(str.replace(/\//g, ' '))
			"#{d.getFullYear()}-#{d.getMonth()+1}-#{d.getDate()}"

		returnDateMMDDYY:(str) ->
			d = new Date(str.replace(/\//g, ' '))
			"#{d.getMonth()+1}/#{d.getDate()}/#{d.getFullYear()}"

		removeBreakLines:(str)->
			str = str.replace(/\n/g,' ')
			str = str.replace(/\r/g,' ')
			str = str.replace(/\t/g,' ')
			str

		returnUTCDateTime:(str) ->
			d = new Date(str.replace(/\//g, ' '))
			year = d.getUTCFullYear()
			
			month = d.getUTCMonth()+1
			if month < 10
				month = "0" + (d.getUTCMonth()+1).toString()

			day = d.getUTCDate()
			if day < 10
				day = "0" + day.toString()

			hours = d.getUTCHours()
			if hours > 12
				hours = d.getUTCHours() - 12 
				tt = 'PM'
			else
				tt = 'AM'
				
			if hours < 10
				hours = "0" + hours.toString()
			
			minutes = d.getUTCMinutes()
			if minutes < 10
				minutes = "0" + d.getUTCMinutes().toString()

			seconds = d.getUTCSeconds()
			if seconds < 10
				seconds = "0" + d.getUTCSeconds().toString()

			"#{month}/#{day}/#{year} #{hours}:#{minutes}:#{seconds} #{tt}"

			#"#{d.getFullYear()}-#{d.getMonth()+1}-#{d.getDate()} #{d.getHours()}:#{d.getMinutes()}:#{d.getSeconds()}"

		returnCallbackStartDateTime:() ->
			currentDate = new Date()
			tomorrowDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate()+1, currentDate.getHours(), currentDate.getMinutes(), currentDate.getSeconds())
			
			year = tomorrowDate.getFullYear()
			
			month = tomorrowDate.getMonth()+1
			if month < 10
				month = "0" + (tomorrowDate.getMonth()+1).toString()

			day = tomorrowDate.getDate()
			if day < 10
				day = "0" + day.toString()

			hours = tomorrowDate.getHours()
			if hours > 12
				hours = tomorrowDate.getHours() - 12 
				t = 'PM'
			else
				t = 'AM'
				
			if hours < 10
				hours = "0" + hours.toString()
			
			minutes = tomorrowDate.getMinutes()
			if minutes < 10
				minutes = "0" + tomorrowDate.getMinutes().toString()


			"#{month}/#{day}/#{year} #{hours}:#{minutes} #{t}"
			
		#Takes a java script object, converts it into json string and format it to be serialized for post
		formatJson: (obj) ->
			strJson = ko.mapping.toJSON(obj)
			matches = strJson.match(/\/Date\(\d+[\+\-]\d+\)\//g)
			if matches?
				for match in matches
					date = window.utils.formater.returnDateTime(match)
					strJson = strJson.replace(match,date)
			strJson
		
		formatMoney: (str) ->
			if !str
				return "$0.00"
			num = parseFloat str
			if isNaN num
				return "$0.00"
			
			"$" + num.toFixed(2)


	#Created a div element and use to URL encode the JSON string and parse it.
	json:
		jsonToJsObject: (str) ->
			x = document.createElement('div')
			x.innerHTML = str
			$.parseJSON(x.innerHTML.replace(/&amp;/g,'&'))	

	validate:
			isClosestFormValid: (element) ->
				_currentForm = $(element).closest('form')
				return $(_currentForm).validate().form()
		
			isFormValid: (id) ->
				return $(id).validate().form()

			isInputValid: (id) ->
				_currentForm = $(id).closest('form')
				_currentForm.validate().element(id)
				return $(id).hasClass('input-validation-error')

			resetValidationErrors: (element) ->
				if element.hasClass('field-validation-error')
					element.removeClass('field-validation-error')
						.addClass('field-validation-valid')
						.trigger('focus')
						.trigger('blur')

				if element.hasClass('input-validation-error')
					element.removeClass('input-validation-error')
						.addClass('valid')
						.trigger('focus')
						.trigger('blur')

				if element.hasClass('validation-summary-errors')
					element.removeClass('validation-summary-errors')
						.addClass('validation-summary-valid')

				element.find('.field-validation-error')
					.removeClass('field-validation-error')
					.addClass('field-validation-valid')
					.trigger('focus')
					.trigger('blur')
				
				element.find('.input-validation-error')
					.removeClass('input-validation-error')
					.addClass('valid')
					.trigger('focus')
					.trigger('blur')

				element.find('.validation-summary-errors')
					.removeClass('validation-summary-errors')
					.addClass('validation-summary-valid')


	#message = the message to display
	#buttons = array of 2 Buttons that contain label & callback
	#example = utils.confirmDemo("Your message", {label:"Na3am", callback:function(){alert('yes clicked');}}, {label:"La2", callback:function(){alert('no clicked');}})
	confirm: (message, yesButton, noButton) ->
		noElem = $("#confirmMessageContainer [name=btn-no]")
		yesElem = $("#confirmMessageContainer [name=btn-yes]")
		msgElem = $("#confirmMessageContainer .message")
		
		yesElem.unbind 'click'
		yesElem.bind 'click', ()->
			yesButton?.callback?()
			$('#confirmMessageContainer').modal('hide')
		yesElem.val yesButton?.label ? "Yes"

		noElem.unbind 'click'
		noElem.bind 'click', ()->
			noButton?.callback?()
			$('#confirmMessageContainer').modal('hide')
		noElem.val noButton?.label ? "No"

		msgElem.text message ? "Are you sure?"

		$('#confirmMessageContainer').modal('show')

	#message = the message to display
	#example = window.utils.showError("Your message", {label:"Ok", callback:function(){alert('ok clicked');}}, function(){alert('popup hidden');})
	showError: (message, okButton, onHidden) ->
		console?.log "showError"
		okElem = $("#errorMessageContainer [name=btn-ok]") 
		msgElem = $("#errorMessageContainer .message")
		
		okElem.unbind 'click'
		okElem.bind 'click', ()->
			okButton?.callback?()
			$('#errorMessageContainer').modal('hide')
		okElem.val okButton?.label ? "Ok"

		msgElem.text message ? "Sorry, an unknown error occured!"

		$("#errorMessageContainer").unbind 'hidden'
		$("#errorMessageContainer").bind 'hidden', ()->
				onHidden?()

		$("#errorMessageContainer").modal('show')


