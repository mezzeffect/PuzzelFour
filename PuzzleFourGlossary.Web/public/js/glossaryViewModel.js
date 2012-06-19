(function() {

  $(function() {
    return myApp.glossaryViewModel = {
      model: {},
      errorMessage: ko.observable(""),
      openPopUp: function(data, e) {
        myApp.data = data;
        if (data.GlossaryId === void 0) {
          myApp.glossaryViewModel.model.GlossaryEditModel.Id(-1);
          myApp.glossaryViewModel.model.GlossaryEditModel.Term("");
          return myApp.glossaryViewModel.model.GlossaryEditModel.Definition("");
        } else {
          myApp.glossaryViewModel.model.GlossaryEditModel.Id(data.GlossaryId());
          myApp.glossaryViewModel.model.GlossaryEditModel.Term(data.Term());
          return myApp.glossaryViewModel.model.GlossaryEditModel.Definition(data.Definition());
        }
      },
      resetEditGlossary: function() {
        myApp.glossaryViewModel.errorMessage("");
        myApp.glossaryViewModel.model.GlossaryEditModel.Id(-1);
        myApp.glossaryViewModel.model.GlossaryEditModel.Term("");
        return myApp.glossaryViewModel.model.GlossaryEditModel.Definition("");
      },
      saveGlossary: function(data, e) {
        var action, serverData;
        action = "Glossary/SaveGlossary/";
        if (window.utils.validate.isFormValid("#glossaryEditFrom")) {
          serverData = window.utils.formater.formatJson(this.model.GlossaryEditModel);
          return $.ajax({
            url: action,
            data: serverData,
            dataType: 'json',
            type: 'POST',
            success: function(data, textStatus, jqXHR) {
              var jsModel;
              if (data.success === false) {
                if (data.errorMessage) {
                  return myApp.glossaryViewModel.errorMessage(data.errorMessage);
                } else {
                  return myApp.glossaryViewModel.errorMessage("failed to save compliance, check inputs and try again");
                }
              } else {
                jsModel = window.utils.json.jsonToJsObject(data.model);
                myApp.glossaryViewModel.model.Glossaries((ko.mapping.fromJS(jsModel)).Glossaries());
                return $('#glossaryEditContainer').modal('hide');
              }
            },
            error: function(jqXHR, textStatus, errorThrown) {
              return myApp.glossaryViewModel.errorMessage("Unable to connect to the Internet" + errorThrown);
            },
            contentType: "application/json; charset=utf-8"
          });
        }
      },
      removeRow: function(id) {
        var row, _i, _len, _ref;
        _ref = this.model.Glossaries();
        for (_i = 0, _len = _ref.length; _i < _len; _i++) {
          row = _ref[_i];
          if (row.GlossaryId() === id) {
            this.model.Glossaries.remove(row);
            return;
          }
        }
      },
      deleteGlossary: function(data, e) {
        var action;
        action = "Glossary/DeleteGlossary/" + (this.model.GlossaryEditModel.Id());
        return $.ajax({
          url: action,
          dataType: 'json',
          type: 'POST',
          success: function(data, textStatus, jqXHR) {
            if (data.success === false) {
              if (data.errorMessage) {
                return myApp.glossaryViewModel.errorMessage(data.errorMessage);
              } else {
                return myApp.glossaryViewModel.errorMessage("failed to save compliance, check inputs and try again");
              }
            } else {
              myApp.data = data.id;
              myApp.glossaryViewModel.removeRow(data.id);
              return $('#confirmDeleteTemplate').modal('hide');
            }
          },
          error: function(jqXHR, textStatus, errorThrown) {
            return myApp.glossaryViewModel.errorMessage("Unable to connect to the Internet" + errorThrown);
          },
          contentType: "application/json; charset=utf-8"
        });
      },
      init: function() {}
    };
  });

}).call(this);
