﻿$(document).ready(function() {
    var confirmation = '<div id="confirmationContainer" style="display: none;">' +
        '<p><label id="confirmationMessage"></label>' +
        '</p>' +
        '<div style="text-align: center; width: 100%;">' +
        '<div>' +
        '<a id="confirmationYesButton" class="button02 left" style="cursor:pointer">' +
        '<span class="l"></span>' +
        '<span class="m">确定</span>' +
        '<span class="r"></span></a>' +
        '<a id="confirmationNoButton" class="button02 right" style="cursor:pointer">' +
        '<span class="l"></span>' +
        '<span class="m">取消</span>' +
        '<span class="r"></span></a>' +
        '</div>' +
        '</div>' +
        '<label id="confirmationTitle" style="display: none">提示</label>' +
        '</div>';

    if ($("#confirmationContainer").length == 0) {
        $(document.body).append(confirmation);
    }

    webui.confirmation.initialize({
        dialog: 'confirmationContainer',
        message: 'confirmationMessage',
        yesButton: 'confirmationYesButton',
        noButton: 'confirmationNoButton',
        title: 'confirmationTitle'
    });
});

webui.confirmation = (function ($) {
    var $dialog;
    var $dialogTitle;
    var $dialogMessage;
    var $dialogYesButton;
    var $dialogNoButton;

    var yesButtonHandler = function() {
    };

    var noButtonHandler = function() {
    };

    var that = {};

    that.initialize = function(ids) {
        $dialog = $('#' + ids.dialog);
        $dialogTitle = $('#' + ids.title);
        $dialogMessage = $('#' + ids.message);
        $dialogYesButton = $('#' + ids.yesButton);
        $dialogNoButton = $('#' + ids.noButton);

        $dialogYesButton.click(function() {
            yesButtonHandler();
            $dialog.dialog("close");
            return false;
        });

        $dialogNoButton.click(function() {
            noButtonHandler();
            $dialog.dialog("close");
            return false;
        });
    };

    that.show = function(message, confirmHandler, cancelHandler) {
        $dialogMessage.html(message);
        if ($.isFunction(confirmHandler)) {
            yesButtonHandler = confirmHandler;
        }
        if ($.isFunction(cancelHandler)) {
            noButtonHandler = cancelHandler;
        }

        $dialog.dialog({
            title: $dialogTitle.text(),
            modal: true,
            resizable: false,
            draggable: true,
            width: 450,
            minHeight: 100
//            ,position: [470, 120]
        });

        $dialog.parent().appendTo($("form:first"));
        $dialogYesButton.blur();
        $dialogNoButton.blur();
    };

    return that;
})(jQuery);

webui.confirm = function (message, confirmHandler, cancelHandler) {
    webui.confirmation.show(message, confirmHandler, cancelHandler);
};