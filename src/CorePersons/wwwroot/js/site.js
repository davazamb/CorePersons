// Write your Javascript code.
$('#myModal').on('shown.bs.modal', function () {
    $('#Email').focus()
})

var items;
var id;
var email;
var phoneNumber;
var userName;
var accessFailedCount;
var concurrencyStamp;
var emailConfirmed;
var lockoutEnabled;
var lockoutEnd;
var normalizedEmail;
var normalizedUserName;
var passwordHash;
var phoneNumberConfirmed;
var securityStamp;
var twoFactorEnabled;

function getDataAjax(id, action) {
    $.ajax({
        type: "POST",
        url: action,
        data: { id },
        success: function (response) {
            //console.log(response);
            OnSuccess(response);
        }
    });
}

//Obtener los datos
function OnSuccess(response) {
    items = response;
    $.each(items, function (index, val) {
        $('input[name=Id]').val(val.id);
        $('input[name=Email]').val(val.email);
        $('input[name=PhoneNumber]').val(val.phoneNumber);
        $('input[name=UserName]').val(val.userName);
    });
}

//Pasar los datos
function setDataUser(action) {
    id = $('input[name=Id]')[0].value;
    email = $('input[name=Email]')[0].value;
    phoneNumber = $('input[name=PhoneNumber]')[0].value;
    userName = $('input[name=UserName]')[0].value;

    $.each(items, function (index, val) {
        accessFailedCount = val.accessFailedCount;
        concurrencyStamp = val.concurrencyStamp;
        emailConfirmed = val.emailConfirmed;
        lockoutEnabled = val.lockoutEnabled;
        lockoutEnd = val.lockoutEnd;
        normalizedEmail = val.normalizedEmail;
        normalizedUserName = val.normalizedUserName;
        passwordHash = val.passwordHash;
        phoneNumberConfirmed = val.phoneNumberConfirmed;
        securityStamp = val.securityStamp;
        twoFactorEnabled = val.twoFactorEnabled;
    });
    if (email == "") {
        $("#Email").focus();
        alert("Ingrese el email");
    } else {
        if (phoneNumber == "") {
            $("#PhoneNumber").focus();
            alert("Ingrese el telefono");
        } else {
            if (userName == "") {
                $("#UserName").focus();
                alert("Ingrese el usuario");
            } else {

            }
        }
    }
}