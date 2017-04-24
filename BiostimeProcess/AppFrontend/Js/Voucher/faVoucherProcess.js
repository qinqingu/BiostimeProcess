webui.faVoucherProcess = (function ($) {
    var buttonTemplate = '<input type="button" name="editArchiveInfoButton" style="width: 40px;" value="编辑"/>' +
       '<input type="button" name="removeArchiveInfoButton" style="width: 40px;" value="删除"/>';
    
    var $faProcessContainer;
    var $faProcessErrorContainer;
    var $stepValue;

    var $shenQingren;
    var $shenQingrenDeptName;
    var $shenQingRiqi;
    var $jieyueYuanyin;
    var $voucherInfosGrid;
    var voucherInfosPager;
    var $faArchiveInfoData;
    var $faGridHasNoData;
    var $jieyueArchiveIds;
    var $archiveId;
    var stepValue;
    var editArchiveInfoButtonName;
    var removeArchiveInfoButtonName;
    var archiveInfos;
    
    var errorMessages = {
        formDataRequired: '请添加需借阅的档案。',
        jieyueYuanyinRequired:'请填写借阅原因'
    };

    var addArchiveGridRow = function() {
        webui.faVoucherInfodetails.show(function () {
            var faArchiveObj = webui.faVoucherInfodetails.faArchiveInfo();
            var no = 0; 
            for (var index = 0; index < archiveInfos.length; index++) {
                var archiveInfo = archiveInfos[index];
                if (archiveInfo.no > no) {
                    no = archiveInfo.no;
                }
            }
            faArchiveObj.no = parseInt(no, 10) + 1;
            setArchiveInfos(faArchiveObj);
        }, '添加财务档案信息', archiveInfos);
    };

    var editArchiveInfo = function () {
        var $editArchiveInfoButton = $(this);
        var no = $editArchiveInfoButton.parent().parent().children('td').eq(0).text();
        var archiveInfo = getArchiveInfo(no);
        webui.faVoucherInfodetails.show(function () {
            var returnFaArchiveInfo = webui.faVoucherInfodetails.faArchiveInfo();
            if (returnFaArchiveInfo) {
                returnFaArchiveInfo.no = no;
                setArchiveInfos(returnFaArchiveInfo);
            }
        }, '编辑财务档案信息', archiveInfo);
        return false;
    };

    var deleteArchiveInfo = function () {
        var $removeArchiveInfoButton = $(this);
        var no = $removeArchiveInfoButton.parent().parent().children('td').eq(0).text();
        var message = '确定要删除序号为' + no + '的数据吗？删除后将无法恢复。';
        webui.confirmation.show(message,
            function () {
                confirmArchiveInfoHandler($removeArchiveInfoButton);
            }
        );
    };

    var confirmArchiveInfoHandler = function (removeArchiveInfoButtonElement) {
        var $removeArchiveInfoButton = removeArchiveInfoButtonElement;
        var no = $removeArchiveInfoButton.parent().parent().children('td').eq(0).text();
        var archiveInfo = getArchiveInfo(no);

        setArchiveInfos(archiveInfo, true);
    };

    var getArchiveInfo = function(no) {
        for (var index = 0; index < archiveInfos.length; index++) {
            var archiveInfo = archiveInfos[index];
            if (String(archiveInfo.no) == no) {
                return archiveInfo;
            }
        }
        return null;
    };

    var setArchiveInfos = function(faArchiveObj, isDelete) {
        var hasArchiveInfo = false;
        var archiveInfosResult = [];
        for (var index = 0; index < archiveInfos.length; index++) {
            var archiveInfo = archiveInfos[index];
            if (String(archiveInfo.no) == faArchiveObj.no) {
                hasArchiveInfo = true;
                if (isDelete) {
                    continue;
                }
                archiveInfo = {
                    archiveId: faArchiveObj.archiveId,
                    no: faArchiveObj.no,
                    company: faArchiveObj.company,
                    year: faArchiveObj.year,
                    month: faArchiveObj.month,
                    voucherWord: faArchiveObj.voucherWord,
                    voucherNumber: faArchiveObj.voucherNumber,
                    voucherNo: faArchiveObj.voucherNo,
                    path: faArchiveObj.path,
                    cabinetNo: faArchiveObj.cabinetNo,
                    day: faArchiveObj.day
                };
            }
            archiveInfosResult.push(archiveInfo);
        }
        if (!hasArchiveInfo) {
            archiveInfosResult.push(faArchiveObj);
        }
        archiveInfos = archiveInfosResult;
        initializationFaArchiveInfoGrid();
    };

    var initializationFaArchiveInfoGrid = function() {
        var archiveInfoGridData = [];
        for (var index = 0; index < archiveInfos.length; index++) {
            var archiveInfo = archiveInfos[index];
            archiveInfo.operating = buttonTemplate;
            archiveInfoGridData.push(archiveInfo);
        }
        $voucherInfosGrid.jqGrid('clearGridData')
            .jqGrid('setGridParam', { data: archiveInfoGridData })
            .trigger('reloadGrid');
        initializeArchiveInfoButton();
    };
    
    var initializeArchiveInfoButton = function () {
        $('input[name="' + editArchiveInfoButtonName + '"]').each(function (index, element) {
            var no = $(element).parent().parent().children('td').eq(0).text();
            if (no == '') {
                $(element).hide();
            } else {
                $(element).click(editArchiveInfo);
            }
            $(element).val('编辑');
            if (stepValue != '0') {
                $(element).val('查看');
            }
        });
        $('input[name="' + removeArchiveInfoButtonName + '"]').each(function (index, element) {
            if ($stepValue.val() != '0') {
                $(element).hide();
            } else {
                var no = $(element).parent().parent().children('td').eq(0).text();
                if (no == '') {
                    $(element).hide();
                } else {
                    $(element).click(deleteArchiveInfo);
                }
            }
            var no = $(element).parent().parent().children('td').eq(0).text();
            if (no == '') {
                $(element).hide();
            } else {
                $(element).click(deleteArchiveInfo);
            }
        });
    };

    var initializeGrid = function () {
        var colNames = ['序号', '档案编号', '公司', '年份', '月份', '凭证字', '凭证号', '凭证券号', '存储位置', '存储柜号', '借阅天数', '操作'];
        var colModel = [
            { name: 'no', index: 'no', width: 40, sortable: true },
            { name: 'archiveId', index: 'archiveId', hidden: true },
            { name: 'company', index: 'company', width: 300, sortable: true },
            { name: 'year', index: 'year', width: 60, sortable: true },
            { name: 'month', index: 'month', width: 40, sortable: true },
            { name: 'voucherWord', index: 'voucherWord', width: 70, sortable: true },
            { name: 'voucherNumber', index: 'voucherNumber', width: 70, sortable: true },
            { name: 'voucherNo', index: 'voucherNo', width: 70, sortable: true },
            { name: 'path', index: 'path', width: 95, sortable: true },
            { name: 'cabinetNo', index: 'cabinetNo', width: 95, sortable: true },
            { name: 'day', index: 'day', width: 70, sortable: true },
            { name: 'operating', index: 'operating', width: 100, sortable: true }
        ];
        webui.jqGrid.setGridWidth(1080);
        if (stepValue == '0') {
            webui.jqGrid.setGridAddfunc(addArchiveGridRow);
        }
        webui.jqGrid.initialize($voucherInfosGrid, voucherInfosPager, colNames, colModel, null);
    };
    var that = {};

    that.initialize = function (ids, names) {
        $faProcessContainer = $('#' + ids.faProcessContainer);
        $faProcessErrorContainer = $('#' + ids.faProcessErrorContainer);
        $stepValue = $('#' + ids.stepValue);
        stepValue = $stepValue.val();
        $shenQingren = $('#' + ids.shenQingren);
        $shenQingrenDeptName = '#' + ids.shenQingrenDeptName;
        $shenQingRiqi = $('#' + ids.shenQingRiqi);
        $jieyueYuanyin = $('#' + ids.jieyueYuanyin);
        $voucherInfosGrid = $('#' + ids.voucherInfosGrid);
        voucherInfosPager = '#' + ids.voucherInfosPager;
        $faArchiveInfoData = $('#' + ids.faArchiveInfoData);
        $faGridHasNoData = $('#' + ids.faGridHasNoData);
        $jieyueArchiveIds = $('#' + ids.jieyueArchiveIds);
        $archiveId = $('#' + ids.archiveId);
        editArchiveInfoButtonName = names.editArchiveInfoButton;
        removeArchiveInfoButtonName = names.removeArchiveInfoButton;
        archiveInfos = $.parseJSON($faArchiveInfoData.val());
        //archiveoutInfos = $.parseJSON($jieyueArchiveIds.val());
        $faGridHasNoData.rules("add", { required: true, messages: { required: errorMessages.formDataRequired } });
        $jieyueYuanyin.rules("add", { required: true, messages: { required: errorMessages.jieyueYuanyinRequired } });
        webui.addRequiredMark($jieyueYuanyin);
        initializeGrid();
        initializationFaArchiveInfoGrid();
    };

    that.validate = function () {
        var isValidateSuccess = webui.validator.validate($faProcessContainer.find(':input:not(:hidden)'), $faProcessErrorContainer);
        if (isValidateSuccess) {
            var archiveInfosLength = archiveInfos.length;
            if (archiveInfosLength == 0) {
                return webui.validator.validate($faGridHasNoData, $faProcessErrorContainer);
            }
        }
        return isValidateSuccess;
    };

    that.submitValidate = function () {
        var isValidateSuccess = webui.faVoucherProcess.validate();
        $faArchiveInfoData.val(JSON.stringify(archiveInfos));
        if (!isValidateSuccess) {
            $('html, body').animate({ scrollTop: 0 }, 0);
            return false;
        }
        webui.disableCloseAlert('正在提交......');
        webui.hideButton();
        return true;
    };
    that.stringifyArchiveInfoData = function () {
        $faArchiveInfoData.val(JSON.stringify(archiveInfos));
    };
    return that;
})(jQuery);