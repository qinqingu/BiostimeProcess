webui.faPaymentInfoDetails = (function () {
    var confirmButtonClick;
    var $faPaymentManagementContainer;
    var $faPaymentManagementErrorContainer;

    var $companyNames;
    var companyNames;
    var $companyName;
    var $year;
    var $month;
    var $hetongHao;
    var $path;
    var $cabinetNo;
    var $day;
    var $archiveId;
    var $stepValue;
    var $jieyueIds;

    var $confirmButton;
    var $cancelButton;
    var jieyueDataInfos;

    var errorMessages = {
        companyRequired: '请填写公司。',
        yearRequired: '请填写年度。',
        yearFormat: '年度应为数字。',
        monthRequired: '请填写月份。',
        monthFormat: '月份应为数字。',
        hetongHaoRequired: '请填写合同号。',
        dayRequired: '请填写借阅天数。',
        dayFormat: '借阅天数应为数字。',
        archiveIdRequired: '系统中此档案不存在。'
    };

    var initialization = function () {
        $faPaymentManagementErrorContainer.hide();
        $companyName.removeClass('highlight');
        $year.removeClass('highlight');
        $month.removeClass('highlight');
        $hetongHao.removeClass('highlight');
        $path.removeClass('highlight');
        $cabinetNo.removeClass('highlight');

        $companyName.val('');
        $year.val('');
        $month.val('');
        $hetongHao.val('');
        $path.val('');
        $cabinetNo.val('');
        $day.val('');
        $archiveId.val('');
    };

    var createAutocomplete = function (thisElement, source) {
        if (source.length <= 0) {
            return;
        }
        var sourceValues = [];
        for (var i = 0; i < source.length; i++) {
            sourceValues.push(source[i]);
        }

        thisElement.attr("title", "").autocomplete({
            delay: 0,
            minLength: 0,
            maxLength:10,
            source: sourceValues,
            change: function (event, ui) {
                setPathAndCabinetNoVal();
            }
        }).click(function () {
            // 双击的时候进行查找
            $(this).autocomplete('search', '');
        });
    };
    var setPathAndCabinetNoVal = function () {
        var company = $companyName.val();
        var year = $year.val();
        var month = $month.val();
        var hetongHao = $hetongHao.val();
        if (company.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        if (year.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        if (month.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        if (hetongHao.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        var remark = company + '\\' + year + '\\' + month + '\\' + hetongHao;
        $.post("../Shared/Controller.aspx?action=GetPathAndCabinetNo", { remark: remark, d: $.now() }, function (model) {
            if (model.result != 0) {
                webui.alert('出现错误,请联系管理员。');
            }
            else {
                if (model.data != null) {
                    $path.val(model.data.Path);
                    $cabinetNo.val(model.data.CabinetNo);
                    $archiveId.val(model.data.Id);
                } else {
                    $path.val('');
                    $cabinetNo.val('');
                    $archiveId.val('');
                }
            }
        });
    };
    
    var that = {};
    that.initialize = function (ids) {
        $faPaymentManagementContainer = $('#' + ids.faPaymentManagementContainer);
        $faPaymentManagementErrorContainer = $('#' + ids.faPaymentManagementErrorContainer);
        $companyNames = $('#' + ids.companyNames);
        companyNames = $.parseJSON($companyNames.val());
        $companyName = $('#' + ids.companyName);
        $year = $('#' + ids.year);
        $month = $('#' + ids.month);
        $hetongHao = $('#' + ids.hetongHao);
        $path = $('#' + ids.path);
        $cabinetNo = $('#' + ids.cabinetNo);
        $day = $('#' + ids.day);
        $archiveId = $('#' + ids.archiveId);
        $stepValue = $('#' + ids.stepValue);
        $jieyueIds = $('#' + ids.jieyueIds);
        $confirmButton = $('#' + ids.confirmButton);
        $cancelButton = $('#' + ids.cancelButton);
        jieyueDataInfos = $.parseJSON($jieyueIds.val());
        $confirmButton.click(function () {
            var archiveId = $archiveId.val();
            for (var index = 0; index < jieyueDataInfos.length; index++) {
                var archiveInfo = jieyueDataInfos[index];
                if (archiveInfo == archiveId) {
                    webui.alert('此档案已借出');
                    return false;
                }
            }
            if (!webui.validator.validate($faPaymentManagementContainer.find(':input:not(:hidden)'), $faPaymentManagementErrorContainer)) {
                return false;
            }
            if (!webui.validator.validate($archiveId, $faPaymentManagementErrorContainer)) {
                return false;
            }
            $faPaymentManagementContainer.dialog('close');
            confirmButtonClick();
            return false;
        });

        $cancelButton.click(function () {
            $faPaymentManagementContainer.dialog('close');
            return false;
        });

        $year.numeric({ decimal: false, negative: false });
        $month.numeric({ decimal: false, negative: false });
        $day.numeric({ decimal: false, negative: false });

        $companyName.rules('add', { required: true, messages: { required: errorMessages.companyRequired } });
        $year.rules('add', {
            required: true,
            digits: true,
            messages: {
                required: errorMessages.yearRequired,
                digits: errorMessages.yearFormat
            }
        });
        $month.rules('add', {
            required: true,
            digits: true,
            messages: {
                required: errorMessages.monthRequired,
                digits: errorMessages.monthFormat
            }
        });
        $day.rules('add', { required: true, messages: { required: errorMessages.dayRequired } });
        $day.rules('add', {
            required: true,
            digits: true,
            messages: {
                required: errorMessages.dayRequired,
                digits: errorMessages.dayNumberFormat
            }
        });
        $hetongHao.rules('add', { required: true, messages: { required: errorMessages.hetongHaoRequired } });
        $archiveId.rules('add', { required: true, messages: { required: errorMessages.archiveIdRequired } });
        webui.addRequiredMark($companyName);
        webui.addRequiredMark($year);
        webui.addRequiredMark($month);
        webui.addRequiredMark($hetongHao);
        webui.addRequiredMark($day);

        $companyName.change(function () {
            setPathAndCabinetNoVal();
        });
        $year.change(function () {
            setPathAndCabinetNoVal();
        });
        $month.change(function () {
            setPathAndCabinetNoVal();
        });
        $hetongHao.change(function () {
            setPathAndCabinetNoVal();
        });
        createAutocomplete($companyName, companyNames);
    };

    that.show = function (clickHandler, titleText, faReportInfoObj) {;
        confirmButtonClick = clickHandler;
        $faPaymentManagementContainer.dialog({
            title: titleText,
            modal: true,
            resizable: false,
            draggable: true,
            width: 595
        });
        $faPaymentManagementContainer.parent().appendTo($('form:first'));
        initialization();
        if (faReportInfoObj) {
            $companyName.val(faReportInfoObj.company);
            $year.val(faReportInfoObj.year);
            $month.val(faReportInfoObj.month);
            $hetongHao.val(faReportInfoObj.hetongHao);
            $path.val(faReportInfoObj.path);
            $cabinetNo.val(faReportInfoObj.cabinetNo);
            $day.val(faReportInfoObj.day);
            $archiveId.val(faReportInfoObj.archiveId);
        }
        return false;
    };
    that.faArchiveInfo = function () {
        return {
            company: $companyName.val(),
            year: $year.val(),
            month: $month.val(),
            hetongHao: $hetongHao.val(),
            path: $path.val(),
            cabinetNo: $cabinetNo.val(),
            day: $day.val(),
            archiveId: $archiveId.val()
        };
    };
    return that;
})(jQuery);