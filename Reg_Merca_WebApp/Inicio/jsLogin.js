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