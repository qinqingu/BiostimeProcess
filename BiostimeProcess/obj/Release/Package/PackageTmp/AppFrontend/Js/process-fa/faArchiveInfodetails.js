webui.faArchiveInfodetails = (function () {
    var confirmButtonClick;
    var $faArchiveManagementContainer;
    var $faArchiveManagementErrorContainer;

    var $company;
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
    
    var $confirmButton;
    var $cancelButton;
    
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
        $company.removeClass('highlight');
        $year.removeClass('highlight');
        $month.removeClass('highlight');
        $voucherWord.removeClass('highlight');
        $voucherNumber.removeClass('highlight');
        $voucherNo.removeClass('highlight');
        $path.removeClass('highlight');
        $cabinetNo.removeClass('highlight');
        
        $company.val('');
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


    var that = {};
    that.initialize = function (ids) {
        $faArchiveManagementContainer = $('#' + ids.faArchiveManagementContainer);
        $faArchiveManagementErrorContainer = $('#' + ids.faArchiveManagementErrorContainer);
        $company = $('#' + ids.company);
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
        $confirmButton = $('#' + ids.confirmButton);
        $cancelButton = $('#' + ids.cancelButton);
        
        $confirmButton.click(function () {
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

        $company.rules('add', { required: true, messages: { required: errorMessages.companyRequired } });
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
        webui.addRequiredMark($company);
        webui.addRequiredMark($year);
        webui.addRequiredMark($month);
        webui.addRequiredMark($voucherWord);
        webui.addRequiredMark($voucherNumber);
        webui.addRequiredMark($day);
        
        var setPathAndCabinetNoVal = function () {
            var company = $company.val();
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
            var remark = company + year + month + voucherWord + voucherNumber;
            $.post("Controller.aspx?action=GetPathAndCabinetNo", { remark: remark, d: $.now() }, function (model) {
                if (model.result != 0) {
                    webui.disableCloseAlert('出现错误,请联系管理员。');
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
        $company.change(function () {
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
            $company.val(archiveInfoObj.company);
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
            company: $company.val(),
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