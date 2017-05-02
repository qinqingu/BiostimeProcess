webui.faVoucherInfodetails = (function () {
    var confirmButtonClick;
    var $faArchiveManagementContainer;
    var $faArchiveManagementErrorContainer;

    var $companyNames;
    var companyNames;
    var $companyName;
    var $year;
    var $month;
    var $voucherWord;
    var $voucherNumber;
    var $voucherNo;
    var $path ;
    var $cabinetNo;
    var $day;
    var $archiveId;
    var $stepValue;
    var $jieyueArchiveIds;
    
    var $confirmButton;
    var $cancelButton;
    var archiveLendInfos;

    var errorMessages = {
        companyRequired: '请填写公司。',
        yearRequired: '请填写年度。',
        yearFormat: '年度应为数字。',
        monthRequired: '请填写月份。',
        monthFormat: '月份应为数字。',
        voucherWordRequired: '请填写凭证字。',
        voucherNumberRequired: '请填写凭证号。',
        voucherNumberFormat: '凭证号应为数字。',
        dayRequired: '请填写借阅天数。',
        dayFormat: '借阅天数应为数字。',
        archiveIdRequired: '系统中此档案不存在。'
    };
    
    var initialization = function () {
        $faArchiveManagementErrorContainer.hide();
        $companyName.removeClass('highlight');
        $year.removeClass('highlight');
        $month.removeClass('highlight');
        $voucherWord.removeClass('highlight');
        $voucherNumber.removeClass('highlight');
        $voucherNo.removeClass('highlight');
        $path.removeClass('highlight');
        $cabinetNo.removeClass('highlight');
        
        $companyName.val('');
        $year.val('');
        $month.val('');
        $voucherWord.val('');
        $voucherNumber.val('');
        $voucherNo.val('');
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
        var voucherWord = $voucherWord.val();
        var voucherNumber = $voucherNumber.val();
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
        if (voucherWord.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        if (voucherNumber.length == 0) {
            $path.val('');
            $cabinetNo.val('');
            $archiveId.val('');
            return;
        }
        var remark = company + '\\' + year + '\\' + month + '\\' + voucherWord + '\\' + voucherNumber;
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
        $faArchiveManagementContainer = $('#' + ids.faArchiveManagementContainer);
        $faArchiveManagementErrorContainer = $('#' + ids.faArchiveManagementErrorContainer);
        $companyNames = $('#' + ids.companyNames);
        companyNames = $.parseJSON($companyNames.val());
        $companyName = $('#' + ids.companyName);
        $year = $('#' + ids.year);
        $month = $('#' + ids.month);
        $voucherWord = $('#' + ids.voucherWord);
        $voucherNumber = $('#' + ids.voucherNumber);
        $voucherNo = $('#' + ids.voucherNo);
        $path = $('#' + ids.path);
        $cabinetNo = $('#' + ids.cabinetNo);
        $day = $('#' + ids.day);
        $archiveId = $('#' + ids.archiveId);
        $stepValue = $('#' + ids.stepValue);
        $jieyueArchiveIds = $('#' + ids.jieyueArchiveIds);
        $confirmButton = $('#' + ids.confirmButton);
        $cancelButton = $('#' + ids.cancelButton);
        archiveLendInfos = $.parseJSON($jieyueArchiveIds.val());
        $confirmButton.click(function () {
            var archiveId = $archiveId.val();
            for (var index = 0; index < archiveLendInfos.length; index++) {
                var archiveInfo = archiveLendInfos[index];
                if (archiveInfo == archiveId) {
                    webui.alert('此劵已借出');
                    return false;
                }
            }
            if (!webui.validator.validate($faArchiveManagementContainer.find(':input:not(:hidden)'), $faArchiveManagementErrorContainer)) {
                return false;
            }
            if (!webui.validator.validate($archiveId, $faArchiveManagementErrorContainer)) {
                return false;
            }
            $faArchiveManagementContainer.dialog('close');
            confirmButtonClick();
            return false;
        });

        $cancelButton.click(function () {
            $faArchiveManagementContainer.dialog('close');
            return false;
        });

        $year.numeric({ decimal: false, negative: false });
        $month.numeric({ decimal: false, negative: false });
        $voucherNumber.numeric({ decimal: false, negative: false });
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
        $voucherWord.rules('add', { required: true, messages: { required: errorMessages.voucherWordRequired } });
        $voucherNumber.rules('add', { required: true, messages: { required: errorMessages.voucherNumberRequired } });
        $voucherNumber.rules('add', {
            required: true,
            digits: true,
            messages: {
                required: errorMessages.voucherNumberRequired,
                dateISO: errorMessages.voucherNumberFormat
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
        $archiveId.rules('add', { required: true, messages: { required: errorMessages.archiveIdRequired } });
        webui.addRequiredMark($companyName);
        webui.addRequiredMark($year);
        webui.addRequiredMark($month);
        webui.addRequiredMark($voucherWord);
        webui.addRequiredMark($voucherNumber);
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
        $voucherWord.change(function () {
            setPathAndCabinetNoVal();
        });
        $voucherNumber.change(function () {
            setPathAndCabinetNoVal();
        });
        createAutocomplete($companyName, companyNames);
    };
    that.show = function (clickHandler, titleText, archiveInfoObj) {;
        confirmButtonClick = clickHandler;
        $faArchiveManagementContainer.dialog({
            title: titleText,
            modal: true,
            resizable: false,
            draggable: true,
            width: 595
        });
        $faArchiveManagementContainer.parent().appendTo($('form:first'));
        initialization();
        if (archiveInfoObj) {
            $companyName.val(archiveInfoObj.company);
            $year.val(archiveInfoObj.year);
            $month.val(archiveInfoObj.month);
            $voucherWord.val(archiveInfoObj.voucherWord);
            $voucherNumber.val(archiveInfoObj.voucherNumber);
            $voucherNo.val(archiveInfoObj.voucherNo);
            $path.val(archiveInfoObj.path);
            $cabinetNo.val(archiveInfoObj.cabinetNo);
            $day.val(archiveInfoObj.day);
            $archiveId.val(archiveInfoObj.archiveId);
        } 
        return false;
    };
    that.faArchiveInfo = function () {
        return {
            company: $companyName.val(),
            year: $year.val(),
            month: $month.val(),
            voucherWord: $voucherWord.val(),
            voucherNumber: $voucherNumber.val(),
            voucherNo: $voucherNo.val(),
            path: $path.val(),
            cabinetNo: $cabinetNo.val(),
            day: $day.val(),
            archiveId: $archiveId.val()
        };
    };
    return that;
})(jQuery);