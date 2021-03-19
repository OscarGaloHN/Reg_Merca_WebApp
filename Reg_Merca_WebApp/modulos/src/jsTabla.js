

$(function () {
    $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
   
        "responsive": true,
        "sPaginationType": "full_numbers",
        "language": {
            "lengthMenu": "Mostrar  _MENU_ registros",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            "info": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
            "infoEmpty": "Mostrando del 0 al 0 de 0 registros",
            "zeroRecords": "No se encontraron registros coincidentes.",
            "emptyTable": "No hay datos disponibles.",
            "stateSave": true,
            "infoFiltered": "(filtrado de _MAX_ registros)",
        }
    });
});