
var tituloImprimir = '';
var xColumnas = [];
 
$(function () {
 

    $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable(
        {
            
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
                text: '<i class="material-icons">print</i>',
                titleAttr: 'Imprimir',
                className: 'btn bg-teal waves-effect',
                title: tituloImprimir,
                exportOptions: {columns: xColumnas},
                download: 'open',

                columnDefs: [{
                    targets: 2,
                }],
                customize: function (doc) {
                    doc.content[1].margin = [50, 0, 50, 0],
                     

                        doc['header'] = (function () {
                            var f = new Date();
                            var newdate = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear();
                            const fecha = new Date();
                            newdate = newdate + " " + fecha.toLocaleTimeString('en-US');
                            var xfecha = newdate
                            return {

                                columns: [
                                    {
                                        alignment: 'right',
                                        text: [{ text: xfecha.toString(), bold: true, fontSize: 8} ],
                                        bold: true
                                    }],
                                margin: [10, 10]
                            }
                        }
                        ),




                               
                        doc['footer'] = (function (page, pages) {
                        
                        return {
                        
                            columns: [
                                {
                                    alignment: 'right',
                                    text: [' Página ', {text:page.toString(), bold: true},
                                        ' de ',
                                        { text: pages.toString(), bold: true},
                                    ],
                                        bold: true
                                }],
                            margin: [10, 0],
                            fontSize: 8
                            }
                    } 
                      );
                }                    
            }
        ],
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