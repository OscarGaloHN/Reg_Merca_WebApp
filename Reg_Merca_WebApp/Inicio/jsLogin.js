
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

//mostrar contraseña
$(document).ready(function () {
    $('#show_password2').hover(function show() {
        //Cambiar el atributo a texto
        $('#txtContraConfirmar').attr('type', 'text');
        //$('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
        document.getElementById("show_password2").innerHTML = "visibility";
    },
        function () {
            //Cambiar el atributo a contraseña
            $('#txtContraConfirmar').attr('type', 'password');
            document.getElementById("show_password2").innerHTML = "visibility_off";
            //$('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
        });
});


//SOLO NUMERO Y LETRAS

function isNumberOrLetter(evt,e) {
    var regex = new RegExp("^(?!.*?([A-Z])\1\1\1).+");
    var charCode = (evt.which) ? evt.which : event.keyCode;
    xcharCode =  (evt.which) ? evt.which : event.keyCode;

    if ((charCode >= 65 && charCode < 91) || (charCode >= 97 && charCode < 123) || (charCode > 47 && charCode < 58) || (charCode == 241) || (charCode == 209)) {
         return true;
    } else {
        return false;
        }
    }

//pasar a mayusculas
var xcharCode 
function mayus(e) {
    e.value = e.value.toUpperCase();
    //var str = e.value.toUpperCase();

    //var xxcharCode = String.fromCharCode(xcharCode);

    //var xcharCodeMayus = xxcharCode.toUpperCase();
    //var repetida = xcharCodeMayus.repeat(4)

    //var n = str.indexOf(repetida);
    //if (n >= 0) {
    //    //alert("El carácter pulsado es: " + n);
    //    event.returnValue = false;
    //}
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

//borrarespacios
function borrarespacios(e) {
    e.value = e.value.replace("  ", " ");
    e.value = e.value.trimLeft();
}

//mayusculas cada palabra
function mayusculapalabras(e) {
    //e.value = e.value[0].toUpperCase() + e.value.slice(1);
    var nombre = e.value 
    var cadena = nombre.toLowerCase().split(' ');
    for (var i = 0; i < cadena.length; i++) {
        cadena[i] = cadena[i].charAt(0).toUpperCase() + cadena[i].substring(1);
    }
    nombre = cadena.join(' ');
    e.value = nombre
}

//NO ESPACIOS
function noespacios(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode == 32)
        return false;
    return true;
}

function mouseOver(txtMostrar, iconoOjoo) {
    document.querySelector('#' + txtMostrar).setAttribute("type", "text");
    document.getElementById(iconoOjoo).innerHTML = "visibility";
}

function mouseOut(txtOcultar, iconoOjoo) {
    document.querySelector('#' + txtOcultar).setAttribute("type", "password");
    document.getElementById(iconoOjoo).innerHTML = "visibility_off";
}

//Función que permite solo Números
function SoloNumeros() {
 if ((event.keyCode < 48) || (event.keyCode > 57)) 
  event.returnValue = false;
}

var repetida = 0;
var teclaAnterior = 0;
function tresrepetida(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;

    if (teclaAnterior == charCode) {
        repetida = repetida + 1;
        teclaAnterior = charCode;
    } else {
        repetida = 0
        teclaAnterior = charCode;
    }


    if (repetida == 4) {
        return true;
    } else {
        return false;

    }
}