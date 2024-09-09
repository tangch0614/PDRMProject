function PopupDialog(id) {
    $(function () {
        var dlg = jQuery("#" + id).dialog({
            title: "SYSTEM DIALOG",
            modal: true,
            height: "auto",
            width: "auto",
            resizable: false,
            overflow: false,
            draggable: false,
            open: function (event, ui) {
                originalContent = $("#" + id).html();
            },
            close: function (event, ui) {
                $("#" + id).html(originalContent);
            }
        });
        $('.ui-dialog').css('z-index', 9999); /*to fix the dialog behind the modal bug*/
        $('.ui-widget-overlay').css('z-index', 9998); /*to fix the dialog behind the modal bug*/
        dlg.parent().appendTo(jQuery("form:first"));
    });
};

function PopupWidth40() {
    $(function () {
        var dlg = jQuery(".popupform").dialog({
            title: "SYSTEM DIALOG",
            modal: true,
            height: "auto",
            width: "40%",
            resizable: false,
            overflow: false,
            draggable: false,
            open: function (event, ui) {
                originalContent = $(".popupform").html();
            },
            close: function (event, ui) {
                $(".popupform").html(originalContent);
            }
        });
        $('.ui-dialog').css('z-index', 9999); /*to fix the dialog behind the modal bug*/
        $('.ui-widget-overlay').css('z-index', 9998); /*to fix the dialog behind the modal bug*/
        dlg.parent().appendTo(jQuery("form:first"));
    });
};

function PopupWidth70() {
    $(function () {
        var dlg = jQuery(".popupform").dialog({
            title: "SYSTEM DIALOG",
            modal: true,
            height: "auto",
            width: "70%",
            resizable: false,
            overflow: false,
            draggable: false,
            open: function (event, ui) {
                originalContent = $(".popupform").html();
            },
            close: function (event, ui) {
                $(".popupform").html(originalContent);
            }
        });
        $('.ui-dialog').css('z-index', 9999); /*to fix the dialog behind the modal bug*/
        $('.ui-widget-overlay').css('z-index', 9998); /*to fix the dialog behind the modal bug*/
        dlg.parent().appendTo(jQuery("form:first"));
    });
};

function PopupWidthAuto() {
    $(function () {
        var dlg = jQuery(".popupform").dialog({
            title: "SYSTEM DIALOG",
            modal: true,
            height: "auto",
            width: "auto",
            resizable: false,
            overflow: false,
            draggable: false,
            open: function (event, ui) {
                originalContent = $(".popupform").html();
            },
            close: function (event, ui) {
                $(".popupform").html(originalContent);
            }
        });
        $('.ui-dialog').css('z-index', 9999); /*to fix the dialog behind the modal bug*/
        $('.ui-widget-overlay').css('z-index', 9998); /*to fix the dialog behind the modal bug*/
        dlg.parent().appendTo(jQuery("form:first"));
    });
};

function BankEditPopup() {
    $(function () {
        var dlg = jQuery(".BankEditPopup").dialog({
            title: "SYSTEM DIALOG",
            modal: true,
            height: "auto",
            width: "40%",
            resizable: false,
            overflow: false,
            draggable: false,
            close: function (event, ui) {
                //$('#bankInfo input').val('');
            }
        });
        $('.ui-dialog').css('z-index', 9999); /*to fix the dialog behind the modal bug*/
        $('.ui-widget-overlay').css('z-index', 9998); /*to fix the dialog behind the modal bug*/
        dlg.parent().appendTo(jQuery("form:first"));
    });
};

