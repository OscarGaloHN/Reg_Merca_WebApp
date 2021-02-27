<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_perfil.aspx.vb" Inherits="Reg_Merca_WebApp.confi_perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- JQuery DataTable Css -->
    <link href="../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">

    <!-- Jquery DataTable Plugin Js -->
    <script src="../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>


    <script type="text/javascript">
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
                    "stateSave": true
                }
            });
        });

        function GetSelectedRow(lnk) {
            //Reference the GridView Row.
            var row = lnk.parentNode.parentNode;

            //Determine the Row Index.
            var message = "Row Index: " + (row.rowIndex - 1);

            //Read values from Cells.
            message += "\nCustomer Id: " + row.cells[1].innerHTML;
            message += "\nName: " + row.cells[2].innerHTML;

            //Reference the TextBox and read value.
            //  message += "\nCountry: " + row.cells[2].getElementsByTagName("input")[0].value;
            document.getElementById('ContentPrincipal_lblid').value = row.cells[1].innerHTML;
            document.getElementById('ContentPrincipal_txtpregunta').value = row.cells[2].innerHTML;
            //Display the data using JavaScript Alert Message Box.
            alert(message);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Perfil de Usuario</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <!-- Basic Examples -->
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>BASIC EXAMPLE
                    </h2>
                </div>
                <div class="body">


                    <div class="table-responsive">
                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                            Width="100%">
                            <Columns>
                                <%--<asp:CustomButton ID="bttEditor" Styles-Style-Font-Size="Large" Styles-Style-CssClass="fas fa-user-edit" Text=" "></asp:CustomButton>
                                <asp:ButtonField CommandName="Edit" Text='<span class="material-icons">face</span>' ButtonType="Button" ShowHeader="True" HeaderText="Editar"></asp:ButtonField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <a href="javascript:void(0);" datanavigateurlformatstring="confi_perfil.aspx?id={0}&name={1}" runat="server" datanavigateurlfields="id_pregunta,pregunta">

                                            <i class="material-icons" data-toggle="modal" data-target="#defaultModal">accessibility</i>    </a>
                                 
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>

                                        <div class="button-demo js-modal-buttons">
                                            <button type="button" data-color="red" class="btn bg-red waves-effect">RED</button>
                                        </div>
                                        <button onclick="return GetSelectedRow(this);" type="button" data-color="red" class="btn bg-red waves-effect">java</button>


                                    </ItemTemplate>
                                </asp:TemplateField>

                            <%--    <asp:HyperLinkField DataNavigateUrlFields="id_pregunta,pregunta"
                                    DataNavigateUrlFormatString="confi_perfil.aspx?id={0}&name={1}" DataTextField="id_pregunta" HeaderText="id_pregunta" />--%>
                                <asp:BoundField DataField="id_pregunta" HeaderText="pregunta" />
                                <asp:BoundField DataField="pregunta" HeaderText="pregunta" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- For Material Design Colors -->
    <div class="modal fade" id="mdModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="defaultModalLabel">Modal title</h4>
                </div>
                <div class="modal-body">
                    <asp:Label ID="lblid" runat="server" Text="Label"></asp:Label>
                    <asp:TextBox ID="txtpregunta" runat="server"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-link waves-effect">SAVE CHANGES</button>
                    <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
