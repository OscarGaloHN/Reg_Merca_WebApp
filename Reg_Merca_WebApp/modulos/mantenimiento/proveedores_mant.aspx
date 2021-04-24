<%@ Page Title="proveedores" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/mantenimiento/master_mantenimiento.Master" CodeBehind="proveedores_mant.aspx.vb" Inherits="Reg_Merca_WebApp.proveedores_mant" %>
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
            document.getElementById('ContentPrincipal_txtdirecciondomicilio').value = '';
            document.getElementById('ContentPrincipal_txtdireccionenvio').value = '';
            document.getElementById('ContentPrincipal_txtciudad').value = '';
            document.getElementById('ContentPrincipal_txttelefono').value = '';
            document.getElementById('ContentPrincipal_txttelefono2').value = '';
            document.getElementById('ContentPrincipal_txttelefono3').value = '';
            document.getElementById('ContentPrincipal_txtfax').value = '';
            document.getElementById('ContentPrincipal_txtemailpersonal').value = '';
            document.getElementById('ContentPrincipal_txtemailempresarial').value = '';
            document.getElementById('ContentPrincipal_txtcontacto').value = '';
            document.getElementById('ContentPrincipal_txtrtnpro').value = '';
            document.getElementById('ContentPrincipal_txtlimitecr').value = '';
            document.getElementById('ContentPrincipal_txtplazocr').value = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblproveedor').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[4].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDproveedor').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenNombreproveedor').value = row.cells[4].innerHTML;
            xModal('red', 'ContentPrincipal_txtnombre', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_txtnombreEditar').value = '';
            document.getElementById('ContentPrincipal_txtdirecciondomicilioEditar').value = '';
            document.getElementById('ContentPrincipal_txtdireccionenvioEditar').value = '';
            document.getElementById('ContentPrincipal_txtciudadEditar').value = '';
            document.getElementById('ContentPrincipal_txttelefonoEditar').value = '';
            document.getElementById('ContentPrincipal_txttelefono2Editar').value = '';
            document.getElementById('ContentPrincipal_txttelefono3Editar').value = '';
            document.getElementById('ContentPrincipal_txtfaxEditar').value = '';
            document.getElementById('ContentPrincipal_txtemailpersonalEditar').value = '';
            document.getElementById('ContentPrincipal_txtemailempresarialEditar').value = '';
            document.getElementById('ContentPrincipal_txtcontactoEditar').value = '';
            document.getElementById('ContentPrincipal_txtrtnproEditar').value = '';
            document.getElementById('ContentPrincipal_txtlimitecrEditar').value = '';
            document.getElementById('ContentPrincipal_txtplazocrEditar').value = '';
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenNombreproveedor').value = row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_LblIdentidadproveedorEditar').innerHTML = row.cells[3].innerHTML;

            if (row.cells[4].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtnombreEditar').value = row.cells[4].innerHTML;
            }
            if (row.cells[5].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtdirecciondomicilioEditar').value = row.cells[5].innerHTML;
            }
            if (row.cells[6].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtdireccionenvioEditar').value = row.cells[6].innerHTML;
            }
            if (row.cells[7].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtciudadEditar').value = row.cells[7].innerHTML;
            }
            if (row.cells[8].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txttelefonoEditar').value = row.cells[8].innerHTML;
            }
            if (row.cells[9].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txttelefono2Editar').value = row.cells[9].innerHTML;
            }
            if (row.cells[10].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txttelefono3Editar').value = row.cells[10].innerHTML;
            }
            if (row.cells[11].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtfaxEditar').value = row.cells[11].innerHTML;
            }
            if (row.cells[12].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtemailpersonalEditar').value = row.cells[12].innerHTML;
            }
            if (row.cells[13].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtemailempresarialEditar').value = row.cells[13].innerHTML;
            }
            if (row.cells[14].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtrtnproEditar').value = row.cells[14].innerHTML;
            }
            if (row.cells[15].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtcontactoEditar').value = row.cells[15].innerHTML;
            }
            if (row.cells[16].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtlimitecrEditar').value = row.cells[16].innerHTML;
            }
            if (row.cells[17].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtplazocrEditar').value = row.cells[17].innerHTML;
            }
            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDproveedor').value = row.cells[2].innerHTML;
            }
        /*cargar combo box*/
            if (row.cells[18].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_cmbPaiseditar').value = row.cells[18].innerHTML;
            }
            if (row.cells[3].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_cmbNivelComercialeditar').value = row.cells[3].innerHTML;
            }

            xModal('pink', 'ContentPrincipal_txtnombreEditar', 'modalEditar');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Mantenimiento de Proveedores</a>
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
             <li>
                     <a href="almacenes_mant.aspx">
                <i class="material-icons">store</i>
                <span>Almacén</span>
            </a>
        </li>
         <li>
            <a href="clasebulto_mant.aspx">
                <i class="material-icons">inventory_2</i>
                <span>Clase de Bulto</span>
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
            <a href="datosventajas_mant.aspx">
                <i class="material-icons">history_edu</i>
                <span>Datos Ventajas</span>
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
         <li class="active" >
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
         tituloImprimir = 'Listado de los Proveedores'
         xColumnas.push(2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
         xMargenes.push(100, 0, 100, 0)
         xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
         xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
     </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Proveedores
                                 <small>A continuación se muestra el listado de los proveedores registrados.</small>
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
                                                <button onclick="return GetSelectedRowEdit(this);" type="button" class="btn bg-pink waves-effect"><i class="material-icons">edit</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id_proveedor" HeaderText="ID" />
                                        <asp:BoundField DataField="Id_nivel_com" HeaderText="id nivel comercial" />
                                        <asp:BoundField DataField="nombre" HeaderText=" Nombre" />
                                        <asp:BoundField DataField="direccion_domicilio" HeaderText="Dirección domicilio" />
                                        <asp:BoundField DataField="direccion_envio" HeaderText="Dirección envío" />
                                        <asp:BoundField DataField="ciudad" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
                                        <asp:BoundField DataField="telefono2" HeaderText=" Teléfono2" />
                                        <asp:BoundField DataField="telefono3" HeaderText=" Teléfono3" />
                                        <asp:BoundField DataField="fax" HeaderText="fax" />
                                        <asp:BoundField DataField="email_personal" HeaderText="Email personal" />
                                        <asp:BoundField DataField="email_empresarial" HeaderText="Email empresarial" />
                                        <asp:BoundField DataField="rtn_proveedor" HeaderText="RTN proveedor" />
                                        <asp:BoundField DataField="contacto" HeaderText="Contacto" />
                                        <asp:BoundField DataField="limitecr" HeaderText="Limite del credito" />
                                        <asp:BoundField DataField="plazocr" HeaderText="Plazo de credito" />
                                        <asp:BoundField DataField="Id_pais" HeaderText="Id_pais" />
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
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarproveedor">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVO PROVEEDOR</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos del proveedor y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-8 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre" AutoComplete="off" ValidationGroup="Validacliente" runat="server" class="form-control" ID="txtnombre" onkeypress="txNombres(event);"  onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtnombre"
                                        ErrorMessage="Ingrese el nombre del proveedores."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validacliente" />
                                </div>
                                 
                            </div>
                      <!--      <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Identidad" AutoComplete="off" ValidationGroup="Validacliente" runat="server" class="form-control" ID="txtIdentidadCliente" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator27" ControlToValidate="txtIdentidadCliente"
                                        ErrorMessage="Ingrese la identidad."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validacliente" />
                                </div>
                            </div> -->
                            <div class="col-lg-12 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Dirección domicilio" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtdirecciondomicilio" onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="validarContactoVac" ControlToValidate="txtdirecciondomicilio"
                                        ErrorMessage="Ingrese la dirección de domicilio."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Dirección envío" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtdireccionenvio" onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtdireccionenvio"
                                        ErrorMessage="Ingrese la Dirección del envío"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>
                               <asp:SqlDataSource
                                ID="SqlPais"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"
                                SelectCommand="SELECT Id_Pais, Nombre_Pais FROM DB_Nac_Merca.tbl_8_paises order by 2;"></asp:SqlDataSource>
                                <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                                    <asp:DropDownList 
                                        ID="CmbPais" runat="server" DataSourceID="SqlPais" class="form-control show-tick"
                                        DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                            <asp:SqlDataSource
                                ID="SqlNivelComercial"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"
                                SelectCommand="SELECT Id_nivel_com, Tipo FROM DB_Nac_Merca.tbl_12_nivel_comercial;"></asp:SqlDataSource>
                                <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                                    <asp:DropDownList 
                                        ID="cmbNivelComercial" runat="server" DataSourceID="SqlNivelComercial" class="form-control show-tick"
                                        DataTextField="Tipo" DataValueField="Id_nivel_com" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>


                            

                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Ciudad" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtciudad" onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtciudad"
                                        ErrorMessage="Ingrese la Ciudad"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txttelefono" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txttelefono"
                                        ErrorMessage="Ingrese el teléfono."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono2" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txttelefono2" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txttelefono2"
                                        ErrorMessage="Ingrese el telefono."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono3" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txttelefono3" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txttelefono3"
                                        ErrorMessage="Ingrese el teléfono"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>
                             </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="fax" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtfax" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtfax"
                                        ErrorMessage="Ingrese el fax"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>

                        
                        
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Email Personal" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtemailpersonal" onkeypress="return noespacios(event);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtemailpersonal"
                                        ErrorMessage="Ingrese el correo personal"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtemailpersonal"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Email Empresarial" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtemailempresarial" onkeypress="return noespacios(event);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtemailempresarial"
                                        ErrorMessage="Ingrese el correo empresarial"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                 <asp:RegularExpressionValidator runat="server" ID="reEmailRegistro"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtemailempresarial"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                               </div>
                            </div>
                            </div>
                            <div class="row">
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Contacto" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtcontacto" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtcontacto"
                                        ErrorMessage="Ingrese el Contacto"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="RTN Proveedores" AutoComplete="off" ValidationGroup="Validaproveedores" runat="server" class="form-control" ID="txtrtnpro" MaxLength="14" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtrtnpro"
                                        ErrorMessage="Ingrese el RTN del proveedor"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedores" />
                                </div>
                            </div>

                        
                        
                            <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Limite de credito" AutoComplete="off" ValidationGroup="Validaproveedor" runat="server" class="form-control" ID="txtlimitecr" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtlimitecr"
                                        ErrorMessage="Ingrese el limite del credito"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedor" />
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Plazo de Credito" AutoComplete="off" ValidationGroup="Validaproveedor" runat="server" class="form-control" ID="txtplazocr" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ControlToValidate="txtplazocr"
                                        ErrorMessage="Ingrese el plazo del credito"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validaproveedor" />
                                </div>
                            </div>
                        </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton runat="server" ID="bttGuardarproveedor" ValidationGroup="Validaproveedor" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
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
                    <h4 class="modal-title" id="LblDelete">ELIMINAR PROVEEDOR</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que dese eliminar este proveedor:
                    <asp:Label runat="server" ID="lblproveedor" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDproveedor" />
                    <asp:HiddenField runat="server" ID="lblHiddenNombreproveedor" />
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarproveedor" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>



    <!-- modal editar cliente-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarproveedor">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditar">EDITAR PROVEEDOR</h4>
                    </div>
                    <div class="modal-body">
                        Luego de terminar de editar los datos del cliente haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row"> 
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                

                                     
                                        <asp:label runat="server" class="form-control" ID="LblIdentidadproveedorEditar" Text="..."></asp:label>
                                   
                            </div>
                            <div class="col-lg-8 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre" AutoComplete="off" ValidationGroup="ValidaproveedorEditar" runat="server" class="form-control" ID="txtnombreEditar" onkeypress="txNombres(event);"  onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtnombreEditar"
                                        ErrorMessage="Ingrese el nombre del proveedor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedorEditar" />
                                </div>
                            </div>
                         

                            <div class="col-lg-12 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Dirección domicilio" AutoComplete="off" ValidationGroup="ValidaproveedorEditar" runat="server" class="form-control" ID="txtdirecciondomicilioEditar" onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtdirecciondomicilioEditar"
                                        ErrorMessage="Ingrese el domicilio del proveedor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedorEditar" />
                                </div>
                            </div>
                             <div class="col-lg-12 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Dirección envio" AutoComplete="off" ValidationGroup="ValidaproveedorEditar" runat="server" class="form-control" ID="txtdireccionenvioEditar" onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtdireccionenvioEditar"
                                        ErrorMessage="Ingrese la direccion de envio del proveedor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaclienteEditar" />
                                </div>
                            </div>
     
                                <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                                    <asp:DropDownList 
                                        ID="cmbPaiseditar" runat="server" DataSourceID="SqlPais" class="form-control show-tick"
                                        DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>
                
                                <div class="col-lg-4 col-md-12 col-sm-12 col-xs-12">
                                    <asp:DropDownList 
                                        ID="cmbNivelComercialeditar" runat="server" DataSourceID="SqlNivelComercial" class="form-control show-tick"
                                        DataTextField="Tipo" DataValueField="Id_nivel_com" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </div>

                     
                       
                           
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Ciudad" AutoComplete="off" ValidationGroup="ValidaproveedorEditar" runat="server" class="form-control" ID="txtciudadEditar" onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" onfocusout="mayus(this);quitarEspacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtciudadEditar"
                                        ErrorMessage="Ingrese la Ciudad del proveedores."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>

                           </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txttelefonoEditar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator17" ControlToValidate="txttelefonoEditar"
                                        ErrorMessage="Ingrese el teléfono del cliente."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono2" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txttelefono2Editar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ControlToValidate="txttelefono2Editar"
                                        ErrorMessage="Ingrese el teléfono2 del cliente."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>

                       
                        
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono3" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txttelefono3Editar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" ControlToValidate="txttelefono3Editar"
                                        ErrorMessage="Ingrese el teléfono3 del cliente."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>
                            </div>
                        <div class="row">
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="fax" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtfaxEditar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator20" ControlToValidate="txtfaxEditar"
                                        ErrorMessage="Ingrese el Fax del cliente."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>

                        
                        
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Email personal" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtemailpersonalEditar" onkeypress="return noespacios(event);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator21" ControlToValidate="txtemailpersonalEditar"
                                        ErrorMessage="Ingrese el email personal."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                     <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtemailpersonalEditar"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Email empresarial" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtemailempresarialEditar" onkeypress="return noespacios(event);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator22" ControlToValidate="txtemailempresarialEditar"
                                        ErrorMessage="Ingrese el email empresarial."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                    <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtemailempresarialEditar"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                                </div>
                            </div>
                            </div>

                        <div class="row">
                        
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Contacto" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtcontactoEditar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator23" ControlToValidate="txtcontactoEditar"
                                        ErrorMessage="Ingrese el contacto del proveedor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="RTN proveedores" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtrtnproEditar" MaxLength="14" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator24" ControlToValidate="txtrtnproEditar"
                                        ErrorMessage="Ingrese el RTN del proveedor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedorEditar" />
                                </div>
                            </div>

                        
                        
                            <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Limite de credito" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtlimitecrEditar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator25" ControlToValidate="txtlimitecrEditar"
                                        ErrorMessage="Ingrese el limite de credito"
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Plazo de credito" AutoComplete="off" ValidationGroup="ValidaproveedoresEditar" runat="server" class="form-control" ID="txtplazocrEditar" onkeypress="SoloNumeros();"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator26" ControlToValidate="txtplazocrEditar"
                                        ErrorMessage="Ingrese el Plazo de credito."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaproveedoresEditar" />
                                </div>
                            </div>
                             </div>
                        
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificar" ValidationGroup="ValidaproveedoresEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
