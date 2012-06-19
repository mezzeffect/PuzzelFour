$ ->
	myApp.glossaryViewModel = 
		model:{}
		errorMessage:ko.observable("")
		
		openPopUp:(data,e)->
			myApp.data = data
			if data.GlossaryId is undefined 
				myApp.glossaryViewModel.model.GlossaryEditModel.Id(-1)
				myApp.glossaryViewModel.model.GlossaryEditModel.Term("")
				myApp.glossaryViewModel.model.GlossaryEditModel.Definition("")
			else
				myApp.glossaryViewModel.model.GlossaryEditModel.Id(data.GlossaryId())
				myApp.glossaryViewModel.model.GlossaryEditModel.Term(data.Term())
				myApp.glossaryViewModel.model.GlossaryEditModel.Definition(data.Definition())
			
		resetEditGlossary:()->
			myApp.glossaryViewModel.errorMesssage("")

			myApp.glossaryViewModel.model?.Glossaries(myApp.glossaryViewModel.modelBase?.Glossaries())
			myApp.glossaryViewModel.model?.GlossaryEditModel(myApp.glossaryViewModel.model?.GlossaryEditModel())
			
			
			
		saveGlossary:(data,e)->
			action = "Glossary/SaveGlossary/"
			if window.utils.validate.isFormValid("#glossaryEditFrom")
				serverData = window.utils.formater.formatJson(@model.GlossaryEditModel)
				$.ajax
					url : action
					data: serverData
					dataType: 'json'
					type: 'POST'
					success: (data, textStatus, jqXHR) ->
						if	data.success == false
							if data.errorMessage
								myApp.glossaryViewModel.errorMessage(data.errorMessage)
							else
								myApp.glossaryViewModel.errorMessage("failed to save compliance, check inputs and try again")
						else
							jsModel = window.utils.json.jsonToJsObject(data.model)
							myApp.glossaryViewModel.model.Glossaries([])
							myApp.glossaryViewModel.model.Glossaries((ko.mapping.fromJS(jsModel)).Glossaries())
							$('#glossaryEditContainer').modal('hide')

					error : (jqXHR, textStatus, errorThrown) ->
						myApp.glossaryViewModel.errorMessage ("Unable to connect to the Internet" + errorThrown)
					contentType: "application/json; charset=utf-8"	
		
		removeRow:(id)->
			for row in @model.Glossaries()
				if row.GlossaryId() is id
					@model.Glossaries.remove(row)
					return
			

		deleteGlossary:(data,e)->
			action = "Glossary/DeleteGlossary/#{@model.GlossaryEditModel.Id()}"
			$.ajax
				url : action
				dataType: 'json'
				type: 'POST'
				success: (data, textStatus, jqXHR) ->
					if	data.success == false
						if data.errorMessage
							myApp.glossaryViewModel.errorMessage(data.errorMessage)
						else
							myApp.glossaryViewModel.errorMessage("failed to save compliance, check inputs and try again")
					else
						myApp.data = data.id
						myApp.glossaryViewModel.removeRow(data.id)
						$('#confirmDeleteTemplate').modal('hide')

				error : (jqXHR, textStatus, errorThrown) ->
					myApp.glossaryViewModel.errorMessage ("Unable to connect to the Internet" + errorThrown)
				contentType: "application/json; charset=utf-8"
		
		init:()->
			myApp.glossaryViewModel.modelBase = ko.mapping.fromJS(ko.toJS(myApp.glossaryViewModel.model))