<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Mantenimiento/master_mantenimiento.Master" CodeBehind="mantenimiento_adunas.aspx.vb" Inherits="Reg_Merca_WebApp.mantenimiento_adunas" %>
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
            document.getElementById('ContentPrincipal_txtAduana').value = '';
            document.getElementById('ContentPrincipal_txtContacto').value = '';
            document.getElementById('ContentPrincipal_txtTel').value = '';
            document.getElementById('ContentPrincipal_txtDireccion').value = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblAduna').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDAduna').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenNombreAduna').value = row.cells[3].innerHTML;
            xModal('red', 'ContentPrincipal_txtAduana', 'modalDelete');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Matenimiento de Aduanas</a>
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
        <li class="active" >
            <a href="#">
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
            <a href="#">
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
        <li>
            <a href="divisas_mant.aspx">
                <i class="material-icons">monetization_on</i>
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
            <a href="#">
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
            <a href="ventajas_mant.aspx">
                <i class="material-icons">verified_user</i>
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
                    <h2 style="font-weight: bold;">Adunas
                                 <small>A continuación se muestra el listado de las aduanas registradas.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo(); xModal('teal','ContentPrincipal_txtAduana','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                                        <asp:BoundField HeaderText="Editar" DataField="Id_Aduana" HtmlEncode="False" DataFormatString="<a class='btn bg-pink waves-effect' href='config_gestion_usuario.aspx?xuser={0}&action=update&ignore=92'><i class='material-icons'>edit</i> </a>" />
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id_Aduana" HeaderText="ID" />
                                        <asp:BoundField DataField="Nombre_aduana" HeaderText="Nombre" />
                                        <asp:BoundField DataField="Contacto" HeaderText="Contacto" />
                                        <asp:BoundField DataField="Tel" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ubicación" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal nueva aduana-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarAduana">
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
                                        <asp:TextBox placeholder="Nombre de Aduana" AutoComplete="off" ValidationGroup="ValidaAduana" runat="server" class="form-control" ID="txtAduana"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtAduana"
                                        ErrorMessage="Ingrese el nombre de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduana" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre del Contacto" AutoComplete="off" ValidationGroup="ValidaAduana" runat="server" class="form-control" ID="txtContacto"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="validarContactoVac" ControlToValidate="txtContacto"
                                        ErrorMessage="Ingrese el nombre del contacto de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduana" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono" AutoComplete="off" ValidationGroup="ValidaAduana" runat="server" class="form-control" ID="txtTel"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTel"
                                        ErrorMessage="Ingrese el teléfono de la aduna."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduana" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Dirección" AutoComplete="off" ValidationGroup="ValidaAduana" runat="server" class="form-control" ID="txtDireccion"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDireccion"
                                        ErrorMessage="Ingrese la dirección de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduana" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarAduana" ValidationGroup="ValidaAduana" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
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
                    <h4 class="modal-title" id="LblDelete">ELIMINAR ADUANA ADUANA</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que dese eliminar esta aduna:
                    <asp:Label runat="server" ID="lblAduna" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDAduna" />
                        <asp:HiddenField runat="server" ID="lblHiddenNombreAduna" />
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarAduna" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
