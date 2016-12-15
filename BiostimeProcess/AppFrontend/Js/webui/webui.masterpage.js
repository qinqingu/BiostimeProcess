webui.masterPage = (function () {
    var that = {};

    that.initialize = function(ids) {
        var mainFormSelector = '#' + ids.mainForm;
        var errorContainerSelector = '#' + ids.errorContainer;
        var exclusiveContainerSelector = '#' + ids.exclusiveContainer;
        webui.validator.initialize(errorContainerSelector, exclusiveContainerSelector, mainFormSelector);
    };

    return that;
})(jQuery);

$(document).ready(function() {
    var $quickMenu = $('.quick_menu');
    var $listWrap = $('.list_wrap');

    var initializeElement = function() {
        $listWrap.width($quickMenu.width() - 18);
        $listWrap.addClass('margin_left_right');
    };

    initializeElement();
});