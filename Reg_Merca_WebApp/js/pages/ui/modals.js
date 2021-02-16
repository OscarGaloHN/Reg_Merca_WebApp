$(function () {
    $('.js-modal-buttons .btn').on('click', function () {
        var color = $(this).data('color');
        var txtfoco = $(this).data('txtfoco');
        $('#mdModal .modal-content').removeAttr('class').addClass('modal-content modal-col-' + color);
        $('#mdModal').modal('show');
        $('#mdModal').on('shown.bs.modal', function () {
            $('#' + txtfoco).focus();
        });
    });
});