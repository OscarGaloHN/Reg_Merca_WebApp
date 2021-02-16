
//mostrar contraseña
$(document).ready(function () {
    $('#show_password').hover(function show() {
        //Cambiar el atributo a texto
        $('#txtContra').attr('type', 'text');
        //$('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
        document.getElementById("show_password").innerHTML = "visibility";
    },
        function () {
            //Cambiar el atributo a contraseña
            $('#txtContra').attr('type', 'password');
            document.getElementById("show_password").innerHTML = "visibility_off";
            //$('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        });
});

//SOLO NUMERO Y LETRAS
function isNumberOrLetter(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 65 && charCode < 91) || (charCode >= 97 && charCode < 123) || (charCode > 47 && charCode < 58) || (charCode == 241) || (charCode == 209))
        return true;
    return false;
}

//pasar a mayusculas
function mayus(e) {
    e.value = e.value.toUpperCase();
}

////SOLO letras
function txNombres(event) {
    if ((event.keyCode != 32) && (event.keyCode < 65) || (event.keyCode > 90) && (event.keyCode < 97) || (event.keyCode > 122))
        event.returnValue = false;
}


function clearTextBox() {
    document.getElementById('txtUsuarioPreguntas').value = '';
}


var xFoco = false;
function myFunctionfoco(txtfoco) {
    if (xFoco == false) {
        setTimeout(function () { document.getElementById(txtfoco).focus() }, 1000);
        xFoco = true;
    }
}