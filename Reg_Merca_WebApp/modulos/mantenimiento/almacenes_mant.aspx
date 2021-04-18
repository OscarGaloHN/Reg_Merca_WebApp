<%@ Page Title="Almacenes" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/mantenimiento/master_mantenimiento.Master" CodeBehind="almacenes_mant.aspx.vb" Inherits="Reg_Merca_WebApp.almacenes_mant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
     <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js"></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
  <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js "></script>
    <script src="../src/jsTabla.js"></script>

    <script src="../src/jsModales.js"></script>


    <script type="text/javascript">
        function borrarTxtNuevo() {
            
            document.getElementById('ContentPrincipal_txtnombre').value = '';
            document.getElementById('ContentPrincipal_txtubicacion').value = '';
            document.getElementById('ContentPrincipal_txtcontacto').value = '';
            document.getElementById('ContentPrincipal_txttel').value = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblAlmacen').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDAlmacen').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenNombreAlmacen').value = row.cells[3].innerHTML;
            xModal('red', 'ContentPrincipal_txtnombre', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_txtnombreEditar').value = '';
            document.getElementById('ContentPrincipal_txtubicacionEditar').value = '';
            document.getElementById('ContentPrincipal_txtcontactoEditar').value = '';
            document.getElementById('ContentPrincipal_txttelEditar').value = '';
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenNombreAlmacen').value = row.cells[3].innerHTML;

            if (row.cells[3].innerHTML != '&nbsp;') { 
                document.getElementById('ContentPrincipal_txtnombreEditar').value = row.cells[3].innerHTML;
            }
            if (row.cells[4].innerHTML != '&nbsp;') { 
                document.getElementById('ContentPrincipal_txtubicacionEditar').value = row.cells[4].innerHTML;
            }
            if (row.cells[5].innerHTML != '&nbsp;') { 
                document.getElementById('ContentPrincipal_txtcontactoEditar').value = row.cells[5].innerHTML;
            }
            if (row.cells[6].innerHTML != '&nbsp;') { 
                 document.getElementById('ContentPrincipal_txttelEditar').value = row.cells[6].innerHTML;
            }
            if (row.cells[2].innerHTML != '&nbsp;') { 
            document.getElementById('ContentPrincipal_lblHiddenIDAlmacen').value = row.cells[2].innerHTML;
            }
            xModal('pink', 'ContentPrincipal_txtnombreEditar', 'modalEditar');
        }
         
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
<a class="navbar-brand" href="#">Matenimiento de Almacenes</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
<ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <li>
            <a href="../menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li  >
            <a href="mantenimiento_adunas.aspx">
                <i class="material-icons">directions_boat</i>
                <span>Aduanas</span>
            </a>
            </li>
             <li class="active">
                     <a href="#">
                <i class="material-icons">store</i>
                <span>Almacén</span>
            </a>
        </li>
        <li>
            <a href="cliente_mant.aspx">
                <i class="material-icons">groups</i>
                <span>Clientes</span>
            </a>
            </li>
        
        <li>
            <a href="condentrega_mant">
                <i class="material-icons">flaky</i>
                <span>Condicion de Entrega</span>
            </a>
            </li>
        <li>
            <a href="divisas_mant.aspx">
                <i class="material-icons">monetization_on</i>
                <span>Divisas</span>
            </a>
            </li>
        <li>
            <a href="estadomerc_mant.aspx">
                <i class="material-icons">rule</i>
                <span>Estado de Mercancia</span>
            </a>
            </li>
        <li>
            <a href="forma_pago.aspx">
                <i class="material-icons">point_of_sale</i>
                <span>Forma de Pago</span>
            </a>
            </li>
         <li>
            <a href="modalidadesp_mant.aspx">
                <i class="material-icons">add_moderator</i>
                <span>Modalidad Especial</span>
            </a>
            </li>
         <li>
            <a href="nivelcomerc_mant.aspx">
                <i class="material-icons">credit_score</i>
                <span>Nivel Comercial</span>
            </a>
            </li>
         <li>
            <a href="proveedores_mant.aspx">
                <i class="material-icons">hail</i>
                <span>Proveedores</span>
            </a>
            </li>
         <li>
            <a href="preguntas_mant.aspx">
                <i class="material-icons">help</i>
                <span>Preguntas</span>
            </a>
            </li>
         <li>
            <a href="paises_mant.aspx">
                <i class="material-icons">travel_explore</i>
                <span>Paises</span>
            </a>
            </li>
         <li>
            <a href="regimenes_mant.aspx">
                <i class="material-icons">menu_book</i>
                <span>Regimenes</span>
            </a>
            </li>
         <li>
            <a href="tipoitems_mant.aspx">
                <i class="material-icons">segment</i>
                <span>Tipo de Item</span>
            </a>
            </li>
         <li>
            <a href="unidmedida_mant.aspx">
                <i class="material-icons">straighten</i>
                <span>Unidad de Medidas</span>
            </a>
            </li>
         <li>
            <a href="ventajas_mant.aspx">
                <i class="material-icons">verified_user</i>
                <span>Ventajas</span>
            </a>
            </li>
    </ul>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
     <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />
    <script type="text/javascript">
        tituloImprimir = 'LISTADO DE LOS ALMACENES'
        xColumnas.push(2, 3, 4, 5, 6); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">ALMACENES
                                 <small>A continuación se muestra el listado de los Almacenes registradas.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo(); xModal('teal','ContentPrincipal_txtnombre','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

                                <i class="material-icons">add</i> <span>Nuevo</span>
                            </button>
                        </div>

                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowEdit(this);" type="button"   class="btn bg-pink waves-effect"><i class="material-icons">edit</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>   <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id_almacen" HeaderText="ID" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" />
                                        <asp:BoundField DataField="Contacto" HeaderText="Contacto" />
                                        <asp:BoundField DataField="Tel" HeaderText="Teléfono" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal nueva almacen-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarAlmacen">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVO ALMACEN</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos deL Almacén y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre" AutoComplete="off" ValidationGroup="ValidaAlmacen" runat="server" class="form-control" ID="txtnombre"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtnombre"
                                        ErrorMessage="Ingrese el nombre del almacén."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduana" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Ubicación" AutoComplete="off" ValidationGroup="ValidaAlmacen" runat="server" class="form-control" ID="txtubicacion"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="validarContactoVac" ControlToValidate="txtubicacion"
                                        ErrorMessage="Ingrese la ubicación del almacén."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacen" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Contacto" AutoComplete="off" ValidationGroup="ValidaAlmacen" runat="server" class="form-control" ID="txtcontacto"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtcontacto"
                                        ErrorMessage="Ingrese la dirección de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacen" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono" AutoComplete="off" ValidationGroup="ValidaAlmacen" runat="server" class="form-control" ID="txttel"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txttel"
                                        ErrorMessage="Ingrese el teléfono del Almacén."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacen" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarAlmacen" ValidationGroup="ValidaAlmacen" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    <!-- modal eliminar aduana-->
    <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <!-- TITULO -->
                    <h4 class="modal-title" id="LblDelete">ELIMINAR ALMACEN</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que dese eliminar este Almacén:
                    <asp:Label runat="server" ID="lblAlmacen" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDAlmacen" />
                        <asp:HiddenField runat="server" ID="lblHiddenNombreAlmacen" />
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarAlmacen" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


       <!-- modal editar aduana-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarAlmacen">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditar">EDITAR ALMACENES</h4>
                    </div>
                    <div class="modal-body">
                      Luego de terminar de editar los datos de los almacenes haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre" AutoComplete="off" ValidationGroup="ValidaAlmacenEditar" runat="server" class="form-control" ID="txtnombreEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtnombreEditar"
                                        ErrorMessage="Ingrese el nombre del almacén."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacenEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Ubicación" AutoComplete="off" ValidationGroup="ValidaAlmacenEditar" runat="server" class="form-control" ID="txtubicacionEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtubicacionEditar"
                                        ErrorMessage="Ingrese la ubicación."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacenEditar" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono" AutoComplete="off" ValidationGroup="ValidaAlmacenEditar" runat="server" class="form-control" ID="txttelEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txttelEditar"
                                        ErrorMessage="Ingrese el teléfono del almacén."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacenEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Contacto" AutoComplete="off" ValidationGroup="ValidaAlmacenEditar" runat="server" class="form-control" ID="txtcontactoEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtcontactoEditar"
                                        ErrorMessage="Ingrese el nombre del contacto del Almacén."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacenEditar" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificar" ValidationGroup="ValidaAlmacenEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
