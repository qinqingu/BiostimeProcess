(function($) {
    $.validator.addMethod("judgePDF", function(value, element) {
        if (this.optional(element)) {
            return true;
        }

        var ext = value.substr(value.lastIndexOf("\\") + 1).split(".")[1].toLowerCase();
        if (ext != "pdf") return false;
        return true;
    }, "You must select a PDF document.");
})(jQuery);