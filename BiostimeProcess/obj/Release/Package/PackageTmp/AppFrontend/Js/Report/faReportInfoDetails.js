webui.faReportInfoDetails = (function () {
    var confirmButtonClick;
    var $faReportManagementContainer;
    var $faReportManagementErrorContainer;

    var $companyNames;
    var companyNames;
    var $companyName;
    var $year;
    var $reportNames;
    var reportNames;
    var $reportName;
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
        baogaoMingchengRequired: '请填写报告名称。',
        dayRequired: '请填写借阅天数。',
        dayFormat: '借阅天数应为数字。',
        archiveIdRequired: '系统中此档案不存在。'
    };

    var initialization = function () {
        $faReportManagementErrorContainer.hide();
        $companyName.removeClass('highlight');
        $year.removeClass('highlight');
        $reportName.removeClass('highlight');
        $path.removeClass('highlight');
        $cabinetNo.removeClass('highlight');

        $companyName.val('');
        $year.val('');
        $reportName.val('');
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
            maxLength: 10,
            source: sourceValues,
            change: function(event, ui) {
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
        var baogaoMingcheng = $reportName.val();
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
        if (baogaoMingcheng.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        var remark = company + '\\' + year + '\\' + baogaoMingcheng;
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
        $faReportManagementContainer = $('#' + ids.faReportManagementContainer);
        $faReportManagementErrorContainer = $('#' + ids.faReportManagementErrorContainer);
        $companyNames = $('#' + ids.companyNames);
        companyNames = $.parseJSON($companyNames.val());
        $companyName = $('#' + ids.companyName);
        $year = $('#' + ids.year);
        $reportNames = $('#' + ids.reportNames);
        reportNames = $.parseJSON($reportNames.val());
        $reportName = $('#' + ids.reportName);
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
            if (!webui.validator.validate($faReportManagementContainer.find(':input:not(:hidden)'), $faReportManagementErrorContainer)) {
                return false;
            }
            if (!webui.validator.validate($archiveId, $faReportManagementErrorContainer)) {
                return false;
            }
            $faReportManagementContainer.dialog('close');
            confirmButtonClick();
            return false;
        });

        $cancelButton.click(function () {
            $faReportManagementContainer.dialog('close');
            return false;
        });

        $year.numeric({ decimal: false, negative: false });
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
        $day.rules('add', { required: true, messages: { required: errorMessages.dayRequired } });
        $day.rules('add', {
            required: true,
            digits: true,
            messages: {
                required: errorMessages.dayRequired,
                digits: errorMessages.dayNumberFormat
            }
        });
        $reportName.rules('add', { required: true, messages: { required: errorMessages.baogaoMingchengRequired } });
        $archiveId.rules('add', { required: true, messages: { required: errorMessages.archiveIdRequired } });
        webui.addRequiredMark($companyName);
        webui.addRequiredMark($year);
        webui.addRequiredMark($reportName);
        webui.addRequiredMark($day);
        
        $year.change(function () {
            setPathAndCabinetNoVal();
        });
        createAutocomplete($companyName, companyNames);
        createAutocomplete($reportName, reportNames);
    };

    that.show = function (clickHandler, titleText, faReportInfoObj) {;
        confirmButtonClick = clickHandler;
        $faReportManagementContainer.dialog({
            title: titleText,
            modal: true,
            resizable: false,
            draggable: true,
            width: 595
        });
        $faReportManagementContainer.parent().appendTo($('form:first'));
        initialization();
        if (faReportInfoObj) {
            $companyName.val(faReportInfoObj.company);
            $year.val(faReportInfoObj.year);
            $reportName.val(faReportInfoObj.baogaoMingcheng);
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
            baogaoMingcheng: $reportName.val(),
            path: $path.val(),
            cabinetNo: $cabinetNo.val(),
            day: $day.val(),
            archiveId: $archiveId.val()
        };
    };
    return that;
})(jQuery);