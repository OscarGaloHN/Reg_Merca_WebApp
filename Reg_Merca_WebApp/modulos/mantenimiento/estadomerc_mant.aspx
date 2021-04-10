﻿<%@ Page Title="estado_mercancias" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/mantenimiento/master_mantenimiento.Master" CodeBehind="estadomerc_mant.aspx.vb" Inherits="Reg_Merca_WebApp.estadomerc_mant" %>

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
            document.getElementById('ContentPrincipal_txtId_Estado').value = '';
            document.getElementById('ContentPrincipal_txtdescripcion').value = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblestado').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDestado').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenNombreEstado').value = row.cells[3].innerHTML;
            xModal('red', 'ContentPrincipal_txtId_Estado', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_txtId_EstadoEditar').value = '';
            document.getElementById('ContentPrincipal_txtdescripcionEditar').value = '';
           
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenNombreEstado').value = row.cells[3].innerHTML;

            if (row.cells[3].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtId_EstadoEditar').value = row.cells[3].innerHTML;
            }
            if (row.cells[4].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtdescripcionEditar').value = row.cells[4].innerHTML;
            }
           
            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDestado').value = row.cells[2].innerHTML;
            }
            xModal('pink', 'ContentPrincipal_txtId_EstadoEditar', 'modalEditar');
        }


    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Matenimiento del estado de la mercancias</a>
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
        <li>
            <a href="mantenimiento_adunas.aspx">
                <i class="material-icons">directions_boat</i>
                <span>Aduanas</span>
            </a>
        </li>
        <li>
            <a href="almacenes_mant.aspx">
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
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Condicion de Entrega</span>
            </a>
        </li>
        <li class="active">
            <a href="#">
                <i class="material-icons">monetization_on</i>
                <span>divisas</span>
            </a>
        </li>
        <li class="active">
            <a href="estadomerc_mant.aspx">
                <i class="material-icons">directions_boat</i>
                <span>Estado de Mercancia</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
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
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Nivel Comercial</span>
            </a>
        </li>
        <li>
            <a href="#">
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
            <a href="#">
                <i class="material-icons">menu_book</i>
                <span>Regimenes</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Tipo de Item</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">verified</i>
                <span>Unidad de Ventaja</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">verified_user</i>
                <span>Ventajas</span>
            </a>
        </li>
    </ul>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <script type="text/javascript">
        tituloImprimir = 'Listado del estado de mercancias'
        xColumnas.push(2, 3); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Estado de la mercancia
                                 <small>A continuación se muestra el listado del estado de la mercancia.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo(); xModal('teal','ContentPrincipal_txtestado','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                                                <button onclick="return GetSelectedRowEdit(this);" type="button" class="btn bg-pink waves-effect"><i class="material-icons">edit</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id_Estado" HeaderText="ID" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal nuevo estado-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarEstado">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVO ESTADO</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos del estado de la mercancia y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL principal -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="ID Estado" AutoComplete="off" ValidationGroup="Validaestado" runat="server" class="form-control" ID="txtId_Estado"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtId_Estado"
                                        ErrorMessage="Ingrese el ID del estado."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaestado" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Descripción" AutoComplete="off" ValidationGroup="Validaestado" runat="server" class="form-control" ID="txtdescripcion"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtdescripcion"
                                        ErrorMessage="Ingrese la descripcion de la divisa."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaestado" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarEstado" ValidationGroup="ValidaEstado" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    <!-- modal eliminar estado-->
    <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <!-- TITULO -->
                    <h4 class="modal-title" id="LblDelete">ELIMINAR ESTADO</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar el estado de la mercancia:
                    <asp:Label runat="server" ID="lblestado" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDestado" />
                    <asp:HiddenField runat="server" ID="lblHiddenNombreEstado" />
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarEstado" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


         <!-- modal editar Estado de la mercancia-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarestado">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditar">EDITAR ESTADO</h4>
                    </div>
                    <div class="modal-body">
                      Luego de terminar de editar los datos de los estados haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Id Estado" AutoComplete="off" ValidationGroup="ValidaestadoEditar" runat="server" class="form-control" ID="txtId_EstadoEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtId_EstadoEditar"
                                        ErrorMessage="Ingrese el ID del estado de la mercancia" 
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaestadoEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Descripcion" AutoComplete="off" ValidationGroup="ValidaestadoEditar" runat="server" class="form-control" ID="txtdescripcionEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtdescripcionEditar"
                                        ErrorMessage="Ingrese la descripcion del estado de la mercancia."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaestadoEditar" />
                                </div>
                            </div>

                        </div>
                        
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificar" ValidationGroup="ValidadivisaEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>