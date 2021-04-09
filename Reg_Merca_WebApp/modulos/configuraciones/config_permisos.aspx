<%@ Page Title="Permisos Módulos" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="config_permisos.aspx.vb" Inherits="Reg_Merca_WebApp.config_permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Select Css -->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
    <!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js "></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.24/dataRender/datetime.js "></script>
    <script src="../src/jsTabla.js"></script>

    <script src="../src/jsModales.js"></script>

    <script type="text/javascript">
        function borrarTxtNuevo() {
  

            var textToFind = 'Seleccionar Módulo...';
            var dd = document.getElementById('ContentPrincipal_ddlModulos');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === textToFind) {
                    dd.selectedIndex = i;
                    break;
                }
            }

 
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Permisos - Módulos</a>
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
            <a href="confi_configurar.aspx">
                <i class="material-icons">settings</i>
                <span>Configuraciones</span>
            </a>
        </li>

        <li>

            <a href="config_avanz.aspx">
                <i class="material-icons">manage_accounts</i>
                <span>Configuracion Avanzada</span>
            </a>
        </li>
        <li class="active">
            <a href="#">
                <i class="material-icons">vpn_key</i>
                <span>Permisos - Módulos</span>
            </a>
        </li>

    </ul>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />

    <script type="text/javascript">
        tituloImprimir = 'Listado de Permiso en Modulos';
        xColumnas.push(2, 3, 4); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0);
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold">Permisos a los módulos por rol
                        <small>Permite controlar los accesos a los módulos del sistema</small>
                    </h2>

                </div>
                <div class="body">
                    <asp:SqlDataSource
                        ID="SqlRoles"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT 0  AS id_rol, 'Seleccionar Rol...' AS rol UNION ALL  SELECT id_rol, rol FROM DB_Nac_Merca.tbl_15_rol where id_rol != 5;"></asp:SqlDataSource>
                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                            <p>
                                <b>Seleccione un rol:</b>
                            </p>

                            <asp:DropDownList AutoPostBack="true" ValidationGroup="ValidaRol"
                                ID="ddlRoles" runat="server" DataSourceID="SqlRoles" class="form-control show-tick"
                                DataTextField="rol" DataValueField="id_rol" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRoles"
                                ErrorMessage="Selecccione un rol" ForeColor="OrangeRed" Font-Size="X-Small" ValidationGroup="ValidaRol"
                                InitialValue="0" SetFocusOnError="True" />
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button validationgroup="ValidaRol" onclick="xModal('teal','','modalNuevo');borrarTxtNuevo(); " type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                                        <asp:BoundField DataField="id_permiso_modulo" HeaderText="ID" />
                                        <asp:BoundField DataField="nombre" HeaderText="Modulo" />
                                        <asp:BoundField DataField="rol" HeaderText="Rol" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- modal nuevo permiso-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarAduana">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVA ADUANA</h4>
                    </div>
                    <div class="modal-body">
                        Seleccione el módulo al que desea dar acceso luego haga clic en el botón 'GUARDAR' para confirmar el nuevo permiso.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->


                        <asp:SqlDataSource
                            ID="SqlModulos"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                        <div class="row clearfix">
                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                                <p><b>Seleccione un módulo para el acceso:</b></p>

                                <asp:DropDownList ValidationGroup="ValidaModulo"
                                    ID="ddlModulos" runat="server" DataSourceID="SqlModulos" class="form-control show-tick"
                                    DataTextField="nombre" DataValueField="id_modulo" AppendDataBoundItems="false">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModulos"
                                    ErrorMessage="Selecccione un módulo" ForeColor="White" Font-Size="Small" ValidationGroup="ValidaModulo"
                                    InitialValue="0" SetFocusOnError="True" />
                            </div>
                           
                        </div>


                    </div>


                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarAduana" ValidationGroup="ValidaModulo" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
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


    <!-- modal editar aduana-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarAduana">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditar">EDITAR ADUANA</h4>
                    </div>
                    <div class="modal-body">
                        Luego de terminar de editar los datos de la aduana haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre de Aduana" AutoComplete="off" ValidationGroup="ValidaAduanaEditar" runat="server" class="form-control" ID="txtAduanaEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtAduanaEditar"
                                        ErrorMessage="Ingrese el nombre de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduanaEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Nombre del Contacto" AutoComplete="off" ValidationGroup="ValidaAduanaEditar" runat="server" class="form-control" ID="txtContactoEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtContactoEditar"
                                        ErrorMessage="Ingrese el nombre del contacto de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduanaEditar" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Teléfono" AutoComplete="off" ValidationGroup="ValidaAduanaEditar" runat="server" class="form-control" ID="txtTelEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtTelEditar"
                                        ErrorMessage="Ingrese el teléfono de la aduna."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduanaEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Dirección" AutoComplete="off" ValidationGroup="ValidaAduanaEditar" runat="server" class="form-control" ID="txtDireccionEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtDireccionEditar"
                                        ErrorMessage="Ingrese la dirección de la aduana."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaAduanaEditar" />
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificar" ValidationGroup="ValidaAduanaEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
    <!-- Select Plugin Js -->
    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js"></script>

</asp:Content>
