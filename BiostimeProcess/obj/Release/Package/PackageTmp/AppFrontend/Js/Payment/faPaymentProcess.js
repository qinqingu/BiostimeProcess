webui.faPaymentProcess = (function ($) {
    var buttonTemplate = '<input type="button" name="editArchiveInfoButton" style="width: 40px;" value="编辑"/>' +
       '<input type="button" name="removeArchiveInfoButton" style="width: 40px;" value="删除"/>';

    var $faPaymentProcessContainer;
    var $faPaymentErrorContainer;
    var $stepValue;

    var $shenQingren;
    var $shenQingrenDeptName;
    var $shenQingRiqi;
    var $jieyueYuanyin;
    var $faArchiveInfosGrid;
    var faArchiveInfosPager;
    var $faArchiveInfoData;
    var $faArchiveGridHasNoData;
    var $jieyueIds;
    var stepValue;
    var editArchiveInfoButtonName;
    var removeArchiveInfoButtonName;
    var faArchiveInfos;

    var errorMessages = {
        formDataRequired: '请添加需借阅的档案。',
        jieyueYuanyinRequired: '请填写借阅原因'
    };

    var addFaArchiveGridRow = function () {
        webui.faPaymentInfoDetails.show(function () {
            var archiveObj = webui.faPaymentInfoDetails.faArchiveInfo();
            var no = 0;
            for (var index = 0; index < faArchiveInfos.length; index++) {
                var archiveInfo = faArchiveInfos[index];
                if (archiveInfo.no > no) {
                    no = archiveInfo.no;
                }
            }
            archiveObj.no = parseInt(no, 10) + 1;
            setArchiveInfos(archiveObj);
        }, '添加档案信息', faArchiveInfos);
    };

    var editArchiveInfo = function () {
        var $editArchiveInfoButton = $(this);
        var no = $editArchiveInfoButton.parent().parent().children('td').eq(0).text();
        var archiveInfo = getArchiveInfo(no);
        webui.faPaymentInfoDetails.show(function () {
            var returnFaArchiveInfo = webui.faPaymentInfoDetails.faArchiveInfo();
            if (returnFaArchiveInfo) {
                returnFaArchiveInfo.no = no;
                setArchiveInfos(returnFaArchiveInfo);
            }
        }, '编辑档案信息', archiveInfo);
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

    var confirmArchiveInfoHandler = function (removeArchiveButtonElement) {
        var $removeArchiveInfoButton = removeArchiveButtonElement;
        var no = $removeArchiveInfoButton.parent().parent().children('td').eq(0).text();
        var baogaoInfo = getArchiveInfo(no);

        setArchiveInfos(baogaoInfo, true);
    };

    var getArchiveInfo = function (no) {
        for (var index = 0; index < faArchiveInfos.length; index++) {
            var archiveInfo = faArchiveInfos[index];
            if (String(archiveInfo.no) == no) {
                return archiveInfo;
            }
        }
        return null;
    };

    var setArchiveInfos = function (archiveObj, isDelete) {
        var hasFaArchiveInfo = false;
        var faArchiveInfosResult = [];
        for (var index = 0; index < faArchiveInfos.length; index++) {
            var archiveInfo = faArchiveInfos[index];
            if (String(archiveInfo.no) == archiveObj.no) {
                hasFaArchiveInfo = true;
                if (isDelete) {
                    continue;
                }
                archiveInfo = {
                    archiveId: archiveObj.archiveId,
                    no: archiveObj.no,
                    company: archiveObj.company,
                    year: archiveObj.year,
                    month: archiveObj.month,
                    hetongHao: archiveObj.hetongHao,
                    path: archiveObj.path,
                    cabinetNo: archiveObj.cabinetNo,
                    day: archiveObj.day
                };
            }
            faArchiveInfosResult.push(archiveInfo);
        }
        if (!hasFaArchiveInfo) {
            faArchiveInfosResult.push(archiveObj);
        }
        faArchiveInfos = faArchiveInfosResult;
        initializationFaArchiveInfoGrid();
    };

    var initializationFaArchiveInfoGrid = function () {
        var archiveInfoGridData = [];
        for (var index = 0; index < faArchiveInfos.length; index++) {
            var archiveInfo = faArchiveInfos[index];
            archiveInfo.operating = buttonTemplate;
            archiveInfoGridData.push(archiveInfo);
        }
        $faArchiveInfosGrid.jqGrid('clearGridData')
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
        var colNames = ['序号', '档案编号', '公司', '年份', '月份', '合同号', '存储位置', '存储柜号', '借阅天数', '操作'];
        var colModel = [
            { name: 'no', index: 'no', width: 40, sortable: true },
            { name: 'archiveId', index: 'archiveId', hidden: true },
            { name: 'company', index: 'company', width: 300, sortable: true },
            { name: 'year', index: 'year', width: 80, sortable: true },
            { name: 'month', index: 'month', width: 40, sortable: true },
            { name: 'hetongHao', index: 'hetongHao', width: 110, sortable: true },
            { name: 'path', index: 'path', width: 120, sortable: true },
            { name: 'cabinetNo', index: 'cabinetNo', width: 100, sortable: true },
            { name: 'day', index: 'day', width: 60, sortable: true },
            { name: 'operating', index: 'operating', width: 100, sortable: true }
        ];
        webui.jqGrid.setGridWidth(1080);
        if (stepValue == '0') {
            webui.jqGrid.setGridAddfunc(addFaArchiveGridRow);
        }
        webui.jqGrid.initialize($faArchiveInfosGrid, faArchiveInfosPager, colNames, colModel, null);
    };
    var that = {};

    that.initialize = function (ids, names) {
        $faPaymentProcessContainer = $('#' + ids.faPaymentProcessContainer);
        $faPaymentErrorContainer = $('#' + ids.faPaymentErrorContainer);
        $stepValue = $('#' + ids.stepValue);
        stepValue = $stepValue.val();
        $shenQingren = $('#' + ids.shenQingren);
        $shenQingrenDeptName = '#' + ids.shenQingrenDeptName;
        $shenQingRiqi = $('#' + ids.shenQingRiqi);
        $jieyueYuanyin = $('#' + ids.jieyueYuanyin);
        $faArchiveInfosGrid = $('#' + ids.faArchiveInfosGrid);
        faArchiveInfosPager = '#' + ids.faArchiveInfosPager;
        $faArchiveInfoData = $('#' + ids.faArchiveInfoData);
        $faArchiveGridHasNoData = $('#' + ids.faArchiveGridHasNoData);
        $jieyueIds = $('#' + ids.jieyueIds);
        editArchiveInfoButtonName = names.editArchiveInfoButton;
        removeArchiveInfoButtonName = names.removeArchiveInfoButton;
        faArchiveInfos = $.parseJSON($faArchiveInfoData.val());
        $faArchiveGridHasNoData.rules("add", { required: true, messages: { required: errorMessages.formDataRequired } });
        $jieyueYuanyin.rules("add", { required: true, messages: { required: errorMessages.jieyueYuanyinRequired } });
        webui.addRequiredMark($jieyueYuanyin);
        initializeGrid();
        initializationFaArchiveInfoGrid();
    };

    that.validate = function () {
        var isValidateSuccess = webui.validator.validate($faPaymentProcessContainer.find(':input:not(:hidden)'), $faPaymentErrorContainer);
        if (isValidateSuccess) {
            var archiveInfosLength = faArchiveInfos.length;
            if (archiveInfosLength == 0) {
                return webui.validator.validate($faArchiveGridHasNoData, $faPaymentErrorContainer);
            }
        }
        return isValidateSuccess;
    };

    that.submitValidate = function () {
        var isValidateSuccess = webui.faPaymentProcess.validate();
        $faArchiveInfoData.val(JSON.stringify(faArchiveInfos));
        if (!isValidateSuccess) {
            $('html, body').animate({ scrollTop: 0 }, 0);
            return false;
        }
        webui.disableCloseAlert('正在提交......');
        webui.hideButton();
        return true;
    };
    that.stringifyArchiveInfoData = function () {
        $faArchiveInfoData.val(JSON.stringify(faArchiveInfos));
    };
    return that;
})(jQuery);