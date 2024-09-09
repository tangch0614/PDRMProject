
function validate(message, group) {
    try {
        if (Page_ClientValidate(group)) {
            var messagetext = document.getElementById(message).value
            return confirm(messagetext);
        }
    }
    catch (ex) {
        var messagetext = document.getElementById(message).value
        return confirm(messagetext);
    };
    Page_BlockSubmit = false;
    return false;
}

function validate2(control, message, group) {
    try {
        if (Page_ClientValidate(group)) {
            var messagetext = document.getElementById(message).value
            if (confirm(messagetext)) {
                control.disabled = true;
                return true;
            } else {
                return false;
            }
        }
    }
    catch (ex) {
        var messagetext = document.getElementById(message).value
        if (confirm(messagetext)) {
            control.disabled = true;
            return true;
        } else {
            return false;
        }
    };
    Page_BlockSubmit = false;
    return false;
}

function validatePopup(group) {
    if (Page_ClientValidate(group)) {
        showmodal('popupform')
    }
    Page_BlockSubmit = false;
    return false;
}

function ValidateDecimal(txt, event) {
    var charCode = (event.which) ? event.which : event.keyCode
    //if (txt.value.length <= 0) {
    //    if (charCode == 46 || (charCode > 31 && (charCode < 48 || charCode > 57)))
    //        return false;
    //    else
    //        return true;
    //}
    if (charCode == 46) {
        if (txt.value.indexOf(".") < 0)
            return true;
        else
            return false;
    }

    //if (txt.value.indexOf(".") >= 0) {
    //    var txtlen = txt.value.length;
    //    var dotpos = txt.value.indexOf(".");
    //    if ((txtlen - dotpos) > 2)
    //        return false;
    //}
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function ValidateInteger(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode == 46 || (charCode > 31
      && (charCode < 48 || charCode > 57)))
        return false;
    return true;
}

function ValidateDefault(field, defaultvalue) {
    field.value = defaultvalue;
}

function ValidateDecimalPoint(txt) {
    if (txt.value.indexOf(".") >= 0) {
        var txtlen = txt.value.length;
        var dotpos = txt.value.indexOf(".");
        if ((txtlen - dotpos) > 2)
            txt.value = txt.value.substring(0, dotpos + 3);
        if(txt.value.indexOf(".") == 0 && txtlen == 1)
            txt.value = ""
    }
}


function CalculateTotal(price, amount, total) {
    var totalValue = document.getElementById(total);
    var priceValue = document.getElementById(price);
    var amountValue = amount;
    if (priceValue.value == "" || amountValue.value == "") {
        totalValue.innerText = 0;}
    else {
        totalValue.innerText = parseFloat(priceValue.value) * parseFloat(amountValue.value);
    }

}


function Confirmation(field,message) {
    var result = confirm(document.getElementById(message).value);
    if (result == true)
        document.getElementById(field).value = "true";
    else
        document.getElementById(field).value = "false";
}

function AutoDistribute(main, sub, packagePrice) {
    var totalPrice = document.getElementById(packagePrice);
    var mainPay = document.getElementById(main);
    var subPay = document.getElementById(sub);
    if (subPay != null) {
        if (parseFloat(mainPay.value) > totalPrice.value) {
            mainPay.value = parseFloat(totalPrice.value);
            subPay.value = parseFloat(totalPrice.value - mainPay.value);
        }
        else {
            subPay.value = parseFloat(totalPrice.value - mainPay.value);
        }
    }
}

function AutoCalculate(main, sub, result, operand) {
    var operand = document.getElementById(operand);
    var mainNum = document.getElementById(main);
    var subNum = document.getElementById(sub);
    var resultNum = document.getElementById(result);
    if (resultNum != null) {
        if (mainNum.value == "" || subNum.value == "") {
            resultNum.innerText = subNum.value;
        }
        else {
            if (operand.value == "+") {
                resultNum.innerText = parseFloat(mainNum.value) + parseFloat(subNum.value);
            }
            if (operand.value == "-") {
                resultNum.innerText = parseFloat(subNum.value) - parseFloat(mainNum.value);
            }
        }
    }
}

function SetDefaultValue(field, defaultValue) {
    if (field.value == "") {
        field.value = defaultValue
    }
}