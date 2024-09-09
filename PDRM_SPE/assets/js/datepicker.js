function initDatePicker(frID,altFrID,toID,altToID) {
    $(function () {
        var currentDate = new Date();
        var beforeDate = new Date();
        beforeDate.setMonth(beforeDate.getMonth() - 1);
        $(frID).datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            altField: altFrID,
            altFormat: "yy-mm-dd",
            dateFormat: "yy-mm-dd",
            onClose: function (selectedDate) {
                $(toID).datepicker("option", "minDate", selectedDate);
            }
        });
        $(toID).datepicker({
            changeMonth: true,
            changeYear: true,
            maxDate: 0,
            numberOfMonths: 1,
            altField: altToID,
            altFormat: "yy-mm-dd",
            dateFormat: "yy-mm-dd",
            onClose: function (selectedDate) {
                $(frID).datepicker("option", "maxDate", selectedDate);
            }
        });
        if ($(altFrID).val() == '' || $(altToID).val() == '') {
            $(frID).datepicker("setDate", beforeDate);
            $(toID).datepicker("setDate", currentDate);
            $(altFrID).datepicker("setDate", beforeDate);
            $(altToID).datepicker("setDate", currentDate);
            $(frID).datepicker("option", "maxDate", $(altToID).val());
            $(toID).datepicker("option", "minDate", $(altFrID).val());
        }
        else {
            $(frID).val($(altFrID).val());
            $(toID).val($(altToID).val());
            $(frID).datepicker("option", "maxDate", $(altToID).val());
            $(toID).datepicker("option", "minDate", $(altFrID).val());
        }
    });
};

function initDatePickerNoMax(frID, altFrID, toID, altToID) {
    $(function () {
        var currentDate = new Date();
        var beforeDate = new Date();
        beforeDate.setMonth(beforeDate.getMonth() - 1);
        $(frID).datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            altField: altFrID,
            altFormat: "yy-mm-dd",
            dateFormat: "yy-mm-dd",
            onClose: function (selectedDate) {
                $(toID).datepicker("option", "minDate", selectedDate);
            }
        });
        $(toID).datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            altField: altToID,
            altFormat: "yy-mm-dd",
            dateFormat: "yy-mm-dd",
            onClose: function (selectedDate) {
                $(frID).datepicker("option", "maxDate", selectedDate);
            }
        });
        if ($(altFrID).val() == '' || $(altToID).val() == '') {
            $(frID).datepicker("setDate", beforeDate);
            $(toID).datepicker("setDate", currentDate);
            $(altFrID).datepicker("setDate", beforeDate);
            $(altToID).datepicker("setDate", currentDate);
            $(frID).datepicker("option", "maxDate", $(altToID).val());
            $(toID).datepicker("option", "minDate", $(altFrID).val());
        }
        else {
            $(frID).val($(altFrID).val());
            $(toID).val($(altToID).val());
            $(frID).datepicker("option", "maxDate", $(altToID).val());
            $(toID).datepicker("option", "minDate", $(altFrID).val());
        }
    });
};


function initBasicDatePicker(frID, altFrID) {
    $(function () {
        var currentDate = new Date();
        $(frID).datepicker({
            changeMonth: true,
            changeYear: true,
            numberOfMonths: 1,
            altField: altFrID,
            altFormat: "yy-mm-dd",
            dateFormat: "yy-mm-dd",
        });
        if ($(altFrID).val() == '') {
            $(frID).datepicker("setDate", currentDate);
            $(altFrID).datepicker("setDate", currentDate);
        }
        else {
            $(frID).val($(altFrID).val());
        }
    });
};
