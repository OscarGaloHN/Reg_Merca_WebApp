<%@ Page Title="ventajas" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/mantenimiento/master_mantenimiento.Master" CodeBehind="ventajas_mant.aspx.vb" Inherits="Reg_Merca_WebApp.ventajas_mant" %>
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
            //document.getElementById('ContentPrincipal_txtid').value = '';
            document.getElementById('ContentPrincipal_txtventajas').value = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblventajas').innerHTML = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDVentajas').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenNombreventajas').value = row.cells[3].innerHTML;
            xModal('red', 'ContentPrincipal_txtventajas', 'modalDelete');
        }
        function GetSelectedRowEdit(lnk) {
            //document.getElementById('ContentPrincipal_txtidEditar').value = '';
            document.getElementById('ContentPrincipal_txtdescripcionEditar').value = '';
            
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenNombreventajas').value = row.cells[3].innerHTML;

            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtdescripcionEditar').value = row.cells[3].innerHTML;
                document.getElementById('ContentPrincipal_lblHiddenIDVentajas').value = row.cells[2].innerHTML;
            }
            //document.getElementById('ContentPrincipal_lblHiddenIDVentajas').value = row.cells[2].innerHTML;

            //if (row.cells[2].innerHTML != '&nbsp;') {
            //    document.getElementById('ContentPrincipal_txtidEditar').value = row.cells[2].innerHTML;
            //}
            xModal('pink', 'ContentPrincipal_txtdescripcionEditar', 'modalEditar');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
  <a class="navbar-brand" href="#">Mantenimiento de Ventajas</a>
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
                     <a href="#">
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
         <li class="active">
            <a href="#">
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
    tituloImprimir = 'Listado de las ventajas'
    xColumnas.push(2, 3); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
    xMargenes.push(100, 0, 100, 0)
    xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
    xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value
</script>
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Ventajas
                                 <small>A continuación se muestra el listado de las Ventajas registradas.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo(); xModal('teal','ContentPrincipal_txtventajas','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                                         <asp:BoundField DataField="id_Ventaja" HeaderText="ID" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcion de la ventaja" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <!-- modal nuevo Ventajas-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarventajas">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVA VENTAJAS</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese la ventaja y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                           <%-- <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                       <asp:TextBox placeholder="ID" AutoComplete="off" ValidationGroup="Validadivisa"  onkeypress="return txtid(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)"  onkeyup="borrarespacios(this);" ID="txtid" runat="server" class="form-control"></asp:TextBox> 
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtid"
                                        ErrorMessage="Ingrese la ID de ventaja."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaventajas" />
                                </div>
                            </div>--%>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Descripcion" AutoComplete="off" ValidationGroup="Validadivisa"  onkeypress="return txtventajas(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)"  onkeyup="borrarespacios(this);" ID="txtventajas" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtventajas"
                                        ErrorMessage="Ingrese la ventaja."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaventajas" />
                                </div>
                            </div>
                           
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarventajas" ValidationGroup="Validaventajas" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
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
                    <h4 class="modal-title" id="LblDelete">ELIMINAR VENTAJAS</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que dese eliminar esta Ventaja:
                    <asp:Label runat="server" ID="lblventajas" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDVentajas" />
                        <asp:HiddenField runat="server" ID="lblHiddenNombreventajas" />
                        
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarventajas" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


       <!-- modal editar aduana-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarventajas">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditar">EDITAR VENTAJAS</h4>
                    </div>
                    <div class="modal-body">
                      Luego de terminar de editar los datos de las ventajas haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                           <%-- <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="ID" AutoComplete="off" ValidationGroup="ValidaventajasEditar" runat="server" class="form-control" ID="txtidEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtidEditar"
                                        ErrorMessage="Ingrese el ID de la ventaja."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaventajasEditar" />
                                </div>
                            </div>--%>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                    <asp:TextBox placeholder="Descripcion de la ventaja" AutoComplete="off" ValidationGroup="Validadivisa"  onkeypress="return txtdescripcionEditar(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)"  onkeyup="borrarespacios(this);" ID="txtdescripcionEditar" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtdescripcionEditar"
                                        ErrorMessage="Ingrese la Ventaja."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaventajasEditar" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificar" ValidationGroup="ValidaventajasEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>  
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
