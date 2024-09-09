
function closemodal(id) {
    $("#" + id).on("hidden.bs.modal", function () {
        $(this)
           .find("input[type=text],input[type=password],textarea,select")
              .val('')
              .end()
           .find("input[type=checkbox], input[type=radio]")
              .prop("checked", "")
              .end();
    });
    $("#" + id).modal('hide');
};

function showmodal(id) {
    $("#" + id).modal('show');
};