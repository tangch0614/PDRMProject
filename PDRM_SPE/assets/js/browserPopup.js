
function OpenPopupWindow(path, width, height) {
    popup = window.open(path, "_blank", 'scrollbars=yes, status=no, resizable=yes, top=0, left=0, width=' + width + ', height=' + height);
    popup.focus();
};

function OpenTabWindow(path) {
    popup = window.open(path, "_blank");
    popup.focus();
};