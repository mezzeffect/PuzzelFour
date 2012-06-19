(function() {

  window.utils = {
    formater: {
      formatPhone: function(str) {
        var line, npa, nxx;
        npa = str.substring(0, 3);
        nxx = str.substring(3, 6);
        line = str.substring(6, 10);
        return "" + npa + "-" + nxx + "-" + line;
      },
      formatDate: function(str) {
        var d;
        d = new Date(str.replace(/\//g, ' '));
        return d.toDateString();
      },
      returnDate: function(str) {
        var d;
        d = new Date(str.replace(/\//g, ' '));
        return "" + (d.getFullYear()) + "-" + (d.getMonth() + 1) + "-" + (d.getDate());
      },
      returnDateMMDDYY: function(str) {
        var d;
        d = new Date(str.replace(/\//g, ' '));
        return "" + (d.getMonth() + 1) + "/" + (d.getDate()) + "/" + (d.getFullYear());
      },
      removeBreakLines: function(str) {
        str = str.replace(/\n/g, ' ');
        str = str.replace(/\r/g, ' ');
        str = str.replace(/\t/g, ' ');
        return str;
      },
      returnUTCDateTime: function(str) {
        var d, day, hours, minutes, month, seconds, tt, year;
        d = new Date(str.replace(/\//g, ' '));
        year = d.getUTCFullYear();
        month = d.getUTCMonth() + 1;
        if (month < 10) {
          month = "0" + (d.getUTCMonth() + 1).toString();
        }
        day = d.getUTCDate();
        if (day < 10) {
          day = "0" + day.toString();
        }
        hours = d.getUTCHours();
        if (hours > 12) {
          hours = d.getUTCHours() - 12;
          tt = 'PM';
        } else {
          tt = 'AM';
        }
        if (hours < 10) {
          hours = "0" + hours.toString();
        }
        minutes = d.getUTCMinutes();
        if (minutes < 10) {
          minutes = "0" + d.getUTCMinutes().toString();
        }
        seconds = d.getUTCSeconds();
        if (seconds < 10) {
          seconds = "0" + d.getUTCSeconds().toString();
        }
        return "" + month + "/" + day + "/" + year + " " + hours + ":" + minutes + ":" + seconds + " " + tt;
      },
      returnCallbackStartDateTime: function() {
        var currentDate, day, hours, minutes, month, t, tomorrowDate, year;
        currentDate = new Date();
        tomorrowDate = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate() + 1, currentDate.getHours(), currentDate.getMinutes(), currentDate.getSeconds());
        year = tomorrowDate.getFullYear();
        month = tomorrowDate.getMonth() + 1;
        if (month < 10) {
          month = "0" + (tomorrowDate.getMonth() + 1).toString();
        }
        day = tomorrowDate.getDate();
        if (day < 10) {
          day = "0" + day.toString();
        }
        hours = tomorrowDate.getHours();
        if (hours > 12) {
          hours = tomorrowDate.getHours() - 12;
          t = 'PM';
        } else {
          t = 'AM';
        }
        if (hours < 10) {
          hours = "0" + hours.toString();
        }
        minutes = tomorrowDate.getMinutes();
        if (minutes < 10) {
          minutes = "0" + tomorrowDate.getMinutes().toString();
        }
        return "" + month + "/" + day + "/" + year + " " + hours + ":" + minutes + " " + t;
      },
      formatJson: function(obj) {
        var date, match, matches, strJson, _i, _len;
        strJson = ko.mapping.toJSON(obj);
        matches = strJson.match(/\/Date\(\d+[\+\-]\d+\)\//g);
        if (matches != null) {
          for (_i = 0, _len = matches.length; _i < _len; _i++) {
            match = matches[_i];
            date = window.utils.formater.returnDateTime(match);
            strJson = strJson.replace(match, date);
          }
        }
        return strJson;
      },
      formatMoney: function(str) {
        var num;
        if (!str) {
          return "$0.00";
        }
        num = parseFloat(str);
        if (isNaN(num)) {
          return "$0.00";
        }
        return "$" + num.toFixed(2);
      }
    },
    json: {
      jsonToJsObject: function(str) {
        var x;
        x = document.createElement('div');
        x.innerHTML = str;
        return $.parseJSON(x.innerHTML.replace(/&amp;/g, '&'));
      }
    },
    validate: {
      isClosestFormValid: function(element) {
        var _currentForm;
        _currentForm = $(element).closest('form');
        return $(_currentForm).validate().form();
      },
      isFormValid: function(id) {
        return $(id).validate().form();
      },
      isInputValid: function(id) {
        var _currentForm;
        _currentForm = $(id).closest('form');
        _currentForm.validate().element(id);
        return $(id).hasClass('input-validation-error');
      },
      resetValidationErrors: function(element) {
        if (element.hasClass('field-validation-error')) {
          element.removeClass('field-validation-error').addClass('field-validation-valid').trigger('focus').trigger('blur');
        }
        if (element.hasClass('input-validation-error')) {
          element.removeClass('input-validation-error').addClass('valid').trigger('focus').trigger('blur');
        }
        if (element.hasClass('validation-summary-errors')) {
          element.removeClass('validation-summary-errors').addClass('validation-summary-valid');
        }
        element.find('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid').trigger('focus').trigger('blur');
        element.find('.input-validation-error').removeClass('input-validation-error').addClass('valid').trigger('focus').trigger('blur');
        return element.find('.validation-summary-errors').removeClass('validation-summary-errors').addClass('validation-summary-valid');
      }
    },
    confirm: function(message, yesButton, noButton) {
      var msgElem, noElem, yesElem, _ref, _ref1;
      noElem = $("#confirmMessageContainer [name=btn-no]");
      yesElem = $("#confirmMessageContainer [name=btn-yes]");
      msgElem = $("#confirmMessageContainer .message");
      yesElem.unbind('click');
      yesElem.bind('click', function() {
        if (yesButton != null) {
          if (typeof yesButton.callback === "function") {
            yesButton.callback();
          }
        }
        return $('#confirmMessageContainer').modal('hide');
      });
      yesElem.val((_ref = yesButton != null ? yesButton.label : void 0) != null ? _ref : "Yes");
      noElem.unbind('click');
      noElem.bind('click', function() {
        if (noButton != null) {
          if (typeof noButton.callback === "function") {
            noButton.callback();
          }
        }
        return $('#confirmMessageContainer').modal('hide');
      });
      noElem.val((_ref1 = noButton != null ? noButton.label : void 0) != null ? _ref1 : "No");
      msgElem.text(message != null ? message : "Are you sure?");
      return $('#confirmMessageContainer').modal('show');
    },
    showError: function(message, okButton, onHidden) {
      var msgElem, okElem, _ref;
      if (typeof console !== "undefined" && console !== null) {
        console.log("showError");
      }
      okElem = $("#errorMessageContainer [name=btn-ok]");
      msgElem = $("#errorMessageContainer .message");
      okElem.unbind('click');
      okElem.bind('click', function() {
        if (okButton != null) {
          if (typeof okButton.callback === "function") {
            okButton.callback();
          }
        }
        return $('#errorMessageContainer').modal('hide');
      });
      okElem.val((_ref = okButton != null ? okButton.label : void 0) != null ? _ref : "Ok");
      msgElem.text(message != null ? message : "Sorry, an unknown error occured!");
      $("#errorMessageContainer").unbind('hidden');
      $("#errorMessageContainer").bind('hidden', function() {
        return typeof onHidden === "function" ? onHidden() : void 0;
      });
      return $("#errorMessageContainer").modal('show');
    }
  };

}).call(this);
