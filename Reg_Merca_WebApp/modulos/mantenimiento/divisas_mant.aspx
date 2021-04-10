<%@ Page Title="Divisas" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/mantenimiento/master_mantenimiento.Master" CodeBehind="Divisas_mant.aspx.vb" Inherits="Reg_Merca_WebApp.Divisas_mant" %>
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
            document.getElementById('ContentPrincipal_txtdescripcion').value = '';
            //document.getElementById('ContentPrincipal_txttotalfactura').value = '';
            //document.getElementById('ContentPrincipal_txttotalflete').value = '';
            //document.getElementById('ContentPrincipal_txttotalseguro').value = '';
            //document.getElementById('ContentPrincipal_txttotalotros').value = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblDivisas').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDdivisas').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenNombreDivisas').value = row.cells[3].innerHTML;
            xModal('red', 'ContentPrincipal_txtdescripcion', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_txtdescripcionEditar').value = '';
            //document.getElementById('ContentPrincipal_txttotalfacturaEditar').value = '';
            //document.getElementById('ContentPrincipal_txttotalfleteEditar').value = '';
            //document.getElementById('ContentPrincipal_txttotalseguroEditar').value = '';
            //document.getElementById('ContentPrincipal_txttotalotrosEditar').value = '';
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenNombreDivisas').value = row.cells[3].innerHTML;

            if (row.cells[3].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtdescripcionEditar').value = row.cells[3].innerHTML;
            }
            //if (row.cells[4].innerHTML != '&nbsp;') {
            //    document.getElementById('ContentPrincipal_txttotalfacturaEditar').value = row.cells[4].innerHTML;
            //}
            //if (row.cells[5].innerHTML != '&nbsp;') {
            //    document.getElementById('ContentPrincipal_txttotalfleteEditar').value = row.cells[5].innerHTML;
            //}
            //if (row.cells[6].innerHTML != '&nbsp;') {
            //    document.getElementById('ContentPrincipal_txttotalotrosEditar').value = row.cells[6].innerHTML;
            //}
            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDdivisas').value = row.cells[2].innerHTML;
            }
            xModal('pink', 'ContentPrincipal_txtdescripcionEditar', 'modalEditar');
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Matenimiento de Divisas</a>
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
             <li >
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
        <li class="active">
            <a href="#">
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
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />
   <script type="text/javascript">
       tituloImprimir = 'Listado de las Aduanas'
       xColumnas.push(2, 3); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
       xMargenes.push(100, 0, 100, 0)
       xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
       xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
   </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Divisas
                                 <small>A continuación se muestra el listado de las Divisas registradas.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo(); xModal('teal','ContentPrincipal_txtdescripcion','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                                        <asp:BoundField DataField="Id_Divisas" HeaderText="ID" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                                        <%--<asp:BoundField DataField="Total_Factura" HeaderText="Total Factura" />
                                        <asp:BoundField DataField="Total_Flete" HeaderText="Total Flete" />
                                        <asp:BoundField DataField="Total_Seguro" HeaderText="Total Seguro" />
                                        <asp:BoundField DataField="Total_Otros_gastos" HeaderText="Total Otros Gastos" />--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal nueva divisa-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarDivisa">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVA DIVISA</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos de la divisa y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                    <asp:TextBox placeholder="Descripción" AutoComplete="off" ValidationGroup="Validadivisa"  onkeypress="return txtdescripcion(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)"  onkeyup="borrarespacios(this);" ID="txtdescripcion" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtdescripcion"
                                        ErrorMessage="Ingrese la descripcion de la divisa."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validadivisa" />
                                </div>
                            </div>
                            <%--<div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" onkeydown="borrarespacios(this)"  onkeyup="borrarespacios(this);" ID="txttotalfactura" runat="server" class="form-control"></asp:TextBox>

                                    <label class="form-label">Total Factura</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txttotalfactura"
                                    ErrorMessage="Ingrese el total factura."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                              
                            </div>
                            </div>--%>

                        </div>
                        <%--<div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                               <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" onkeydown="borrarespacios(this)"  onkeyup="borrarespacios(this);" ID="txttotalflete" runat="server" class="form-control"></asp:TextBox>

                                    <label class="form-label">Total Fletes</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txttotalflete"
                                    ErrorMessage="Ingrese el total de fletes de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                              
                            </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" onkeydown="borrarespacios(this)"  onkeyup="borrarespacios(this);" ID="txttotalseguro" runat="server" class="form-control"></asp:TextBox>

                                    <label class="form-label">Total Seguro</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txttotalseguro"
                                    ErrorMessage="Ingrese el Total del Seguro."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                              
                            </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" onkeydown="borrarespacios(this)"  onkeyup="borrarespacios(this);" ID="txttotalotros" runat="server" class="form-control"></asp:TextBox>

                                    <label class="form-label">Total Otros Gastos</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txttotalotros"
                                    ErrorMessage="Ingrese el Total de Otros Gastos."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                              
                            </div>
                            </div>


                        </div>--%>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarDivisa" ValidationGroup="Validadivisa" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
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
                    <h4 class="modal-title" id="LblDelete">ELIMINAR DIVISA</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que dese eliminar esta Divisa:
                    <asp:Label runat="server" ID="lblDivisas" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDdivisas" />
                        <asp:HiddenField runat="server" ID="lblHiddenNombreDivisas" />
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarDivisa" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


       <!-- modal editar aduana-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarDivisa">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditar">EDITAR DIVISA</h4>
                    </div>
                    <div class="modal-body">
                      Luego de terminar de editar los datos de la divisa haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Descripción" AutoComplete="off" ValidationGroup="ValidadivisaEditar" runat="server" class="form-control" ID="txtdescripcionEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtdescripcionEditar"
                                        ErrorMessage="Ingrese la descripcion de la Divisa."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduanaEditar" />
                                </div>
                            </div>
                           <%-- <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Total Factura" AutoComplete="off" ValidationGroup="ValidadivisaEditar" runat="server" class="form-control" ID="txttotalfacturaEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txttotalfacturaEditar"
                                        ErrorMessage="Ingrese el total de la factura."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidadivisaEditar" />
                                </div>
                            </div>--%>

                        </div>
                        <%--<div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Total Flete" AutoComplete="off" ValidationGroup="ValidadivisaEditar" runat="server" class="form-control" ID="txttotalfleteEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txttotalfleteEditar"
                                        ErrorMessage="Ingrese el total del flete."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidadivisaEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Total Seguro" AutoComplete="off" ValidationGroup="ValidadivisaEditar" runat="server" class="form-control" ID="txttotalseguroEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txttotalseguroEditar"
                                        ErrorMessage="Ingrese el total del seguro."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidadivisaEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Total Otros gastos" AutoComplete="off" ValidationGroup="ValidadivisaEditar" runat="server" class="form-control" ID="txttotalotrosEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txttotalotrosEditar"
                                        ErrorMessage="Ingrese el total de Otros Gastos."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidadivisaEditar" />
                                </div>
                            </div>

                        </div>--%>
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
