function xModal(xcolor, xtxtfoco, nombremodal) {
    var color = xcolor;
    var txtfoco = xtxtfoco;
    $('#' + nombremodal + ' .modal-content').removeAttr('class').addClass('modal-content modal-col-' + color);
    $('#' + nombremodal).modal('show');
    $('#' + nombremodal).on('shown.bs.modal', function () {
        $('#' + txtfoco).focus();
    });
}