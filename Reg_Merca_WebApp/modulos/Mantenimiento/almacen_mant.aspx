<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Mantenimiento/master_mantenimiento.Master" CodeBehind="almacen_mant.aspx.vb" Inherits="Reg_Merca_WebApp.almacen_mant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
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

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Mantenimiento de Almacén</a>
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
             <li class="active">
                     <a href="almacen_mant.aspx">
                <i class="material-icons">directions_boat</i>
                <span>Almacén</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Clientes</span>
            </a>
            </li>
        
        <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Condicion de Entrega</span>
            </a>
            </li>
        <li>
            <a href="divisas_mant.aspx">
                <i class="material-icons">directions_boat</i>
                <span>divisas</span>
            </a>
            </li>
        <li>
            <a href="#">
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
            <a href="#">
                <i class="material-icons">directions_boat</i>
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
                <i class="material-icons">directions_boat</i>
                <span>Proveedores</span>
            </a>
            </li>
         <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Preguntas</span>
            </a>
            </li>
         <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Paises</span>
            </a>
            </li>
         <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
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
                <i class="material-icons">directions_boat</i>
                <span>Unidad de Ventaja</span>
            </a>
            </li>
         <li>
            <a href="#">
                <i class="material-icons">directions_boat</i>
                <span>Ventajas</span>
            </a>
            </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">ALMACEN
                                 <small>A continuación se muestra el listado de los Almacenes.</small>
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
                                       <asp:BoundField HeaderText="Editar" DataField="Id_almacen" HtmlEncode="False" DataFormatString="<a class='btn bg-pink waves-effect' href='config_gestion_usuario.aspx?xuser={0}&action=update&ignore=92'><i class='material-icons'>edit</i> </a>" /> 
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id_almacen" HeaderText="ID" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Ubicación" HeaderText="Ubicación" />
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
    <!-- modal nuevo Almacen-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarAlmacen">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVA ADUANA</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos de la aduana y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
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
                                        ErrorMessage="Ingrese el nombre del Almacen."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacen" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Ubicación" AutoComplete="off" ValidationGroup="ValidaAlmacen" runat="server" class="form-control" ID="txtubicacion"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtubicacion"
                                        ErrorMessage="Ingrese la Ubicacion del Almacen."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAlmacen" />
                                </div>
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Contacto" AutoComplete="off" ValidationGroup="ValidaAlmacen" runat="server" class="form-control" ID="txtcontacto"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="validarContactoVac" ControlToValidate="txtcontacto"
                                        ErrorMessage="Ingrese el nombre del contacto del Almacen."
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
                                        ErrorMessage="Ingrese el teléfono del Almacen."
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
                    ¿Seguro que dese eliminar este Almacen:
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

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>