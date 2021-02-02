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
    if ((charCode >= 65 && charCode < 91) || (charCode >= 97 && charCode < 123) || (charCode > 47 && charCode < 58) || (charCode = 241)  || ( charCode = 209))
        return true;
    return false;
}
//pasar a mayusculas
function mayus(e) {
    e.value = e.value.toUpperCase();
}



(function () {
    /*Declaramos variables Globales*/
    var waves_ripple, d, x, y;
    $('.waves').on('click', wavesRipple);
    function wavesRipple(e) {
        e.preventDefault();
        var that = $(this);

        /* Si no existe nuestra caja hijo se lo agregamos */
        if (that.find('.waves-ripple').length === 0) {
            that.prepend('<span class="waves-ripple"></span>');
        }

        waves_ripple = that.find('.waves-ripple');
        waves_ripple.removeClass('waves-animate');

        if (!waves_ripple.height() && !waves_ripple.width()) {
            /* Optemos el tamaño maximo de uno de los lados de la caja padre */
            d = Math.max(that.outerWidth(), that.outerHeight());
            /* Agregamos el tamaño optenido a nuestra etiqueta hijo */
            waves_ripple.css({
                height: d,
                width: d
            });
        }

        /* Obtener posiciones donde se dio click */
        x = e.pageX - that.offset().left - waves_ripple.width() / 2;
        y = e.pageY - that.offset().top - waves_ripple.height() / 2;
        /* Las posiciones optenidas se lo agregamos a nustra caja hijo */
        waves_ripple
            .css({
                top: y + 'px',
                left: x + 'px'
            })
            .addClass('waves-animate');
    }
})();


