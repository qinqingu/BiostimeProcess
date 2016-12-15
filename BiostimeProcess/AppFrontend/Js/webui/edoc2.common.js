webui.edoc2Common = (function ($) {
    var that = {};

    that.openUploadWnd = function(url, width, height) {
        return window.open(url, '_blank', 'height=' + height + 'px, width=' + width + 'px, toolbar= no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
    };

    /*文件预览
    fileId:文件id
    fileName:文件名称*/
    that.previewFile = function(fileId) {
        var url = '../../../Preview.aspx?FileId=' + fileId;
        window.open(url);
        //try {
        //    window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
        //} catch(e) {
        //    window.open(url);
        //}
    };

    /*获取文件上传链接
    isMultifile:是否允许多个文件
    folderId:上传目录id*/
    that.GetFileUploadExUrl = function(isMultifile, folderId) {
        return '../../../Document/File_UploadEx.aspx?multifile=' + isMultifile + '&folderid=' + folderId;
    };

    /*上传文件
    isMultifile:是否允许多个文件
    folderId:上传目录id*/
    that.fileUpload = function(isMultifile, folderId, afterUploadHandler) {
        var fileUploadExUrl = webui.edoc2Common.GetFileUploadExUrl(isMultifile, folderId);
        var win = webui.edoc2Common.openUploadWnd(fileUploadExUrl, 480, 360);
        uploadCallback = function(filelist) {
            if (filelist.length) {
                if ($.isFunction(afterUploadHandler)) {
                    afterUploadHandler(filelist);
                }
            }
            win.close();
        };
    };

    /*获取文件下载链接
    fileId:文件id*/
    that.GetFileDownloadUrl = function(fileId) {
        return '../../../Document/File_Download.aspx?file_Id=' + fileId;
    };

    /*删除文件
    fileId:文件id
    afterDropFileHandler:删除后执行的方法*/
    that.DropFile = function(fileId, afterDropFileHandler) {
        if (!$.isFunction(afterDropFileHandler)) {
            afterDropFileHandler = function() {
            };
        }
        //        $.post('../../../Ws403/WebServiceCore.asmx/DropFolderAndFiles', { key: 'fileMember', folderIds: 0, fileIds: fileId }, afterDropFileHandler);
        var url = "../ProcessCommon/ProcessCommonHandler.aspx?action=DropFolderAndFiles&fileId=" + fileId;
        $.ajaxSetup({ cache: false });
        $.getJSON(url, function(data) {
            if (data.value == 'true') {
                afterDropFileHandler();
            }
        });
    };

    /*获取选择用户窗口链接*/
    that.GetSelectRadioUserUrl = function(isMultifile) {
        var url = "../../../AppExt/Common/SelectOrgnization.aspx?" +
            "userTree= { show: true, multiSelect: false, current: true }" +
            "&deptTree= { show: false, multiSelect: false}" +
            "&userGroupTree= { show: false, multiSelect: false}";
        if (isMultifile) {
            url = '../../../AppExt/Common/SelectOrgnization.aspx?' +
                'userTree= { show: true, multiSelect: true, current: true }' +
                '&deptTree= { show: false, multiSelect: true}' +
                '&userGroupTree= { show: false, multiSelect: true}';
        }
        return url;
    };

    /*选择用户*/
    that.selectUser = function(isMultifile) {
        var url = webui.edoc2Common.GetSelectRadioUserUrl(isMultifile);
        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
        return res;
    };

    /*选择表单方法*/
    that.selectForm = function() {
        var url = '../../../AppExt/Common/SelectEDoc2Folder.aspx?showForm=true';
        ;
        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
        return res;
    };

    /*选择文件夹方法*/
    that.selectFolder = function(folderIds) {
        var url = '../../../AppExt/Common/SelectEDoc2Folder.aspx';
        if (folderIds) {
            url = url + '?folderIds=' + folderIds;
        }
        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
        return res;
    };

    /*选择文件方法*/
    that.selectFile = function(multiSelect, folderIds) {
        //        folderIds=9&multiSelect=false
        var url = '../../../AppExt/Common/SelectEDocFile.aspx?multiSelect=ture';
        if (multiSelect) {
            multiSelect = multiSelect;
            url = '../../../AppExt/Common/SelectEDocFile.aspx?multiSelect=' + multiSelect;
        }
        if (folderIds) {
            url = url + '&folderIds=' + folderIds;
        }
        var res = window.showModalDialog(url, "", "dialogWidth:750px; dialogHeight:450px;");
        return res;
    };

    return that;
})(jQuery);