<%@ Page Language="vb" Title="Ventajas del items" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="items_ventajas.aspx.vb" Inherits="Reg_Merca_WebApp.items_ventajas" %>

<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
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
            document.getElementById('ContentPrincipal_ddlventajas').value = '';

        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblDocumento').innerHTML = row.cells[3].innerHTML + ' - ' + row.cells[4].innerHTML + ' - ' + row.cells[5].innerHTML + ' - ' + row.cells[6].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[3].innerHTML;

            /* xModal('red', 'ContentPrincipal_txtReferencia', 'modalDelete');*/
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_ddlventajaedit').value = '';
            //document.getElementById('ContentPrincipal_txtReferencia').value = '';
            //document.getElementById('ContentPrincipal_chkPresenciaEditar').Checked = '';
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[3].innerHTML;

            if (row.cells[3].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_ddlddlventajaedit').value = row.cells[3].innerHTML;
                //}
                //if (row.cells[4].innerHTML != '&nbsp;') {
                //    document.getElementById('ContentPrincipal_txtReferenciaEditar').value = row.cells[4].innerHTML;
                //}
                //if (row.cells[5].innerHTML != '&nbsp;') {
                //    document.getElementById('ContentPrincipal_txt_chkPresenciaEditar').Checked = row.cells[5].innerHTML;
            }
            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[2].innerHTML;
            }
            /*  xModal('pink', 'ContentPrincipal_txtReferenciaEditar', 'modalEditar');*/
        }

    </script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Ventajas del Items</a>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <li>
            <a href="../menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li class="active">

            <a href="#">
                <i class="material-icons">create_new_folder</i>
                <span>Ventajas del Items</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />


    <script type="text/javascript">
        tituloImprimir = 'ventajas del items'
        xColumnas.push(2, 3, 4); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Ventajas del item
                                 <small>Acontinuación el usuario podra visualizar las ventajas del item.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="xModal('teal','','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

                                <i class="material-icons">add</i> <span>Nuevo</span>
                            </button>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttVolver"
                                type="button"
                                ValidationGroup="Validarbttvolver"
                                class="btn btn-block btn-lg bg-teal waves-effect">
                                <i class="material-icons">undo</i>
                                <span>Volver</span>
                            </asp:LinkButton>
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
                                        <%--Id_Codigo, Id_ventaja, Id_merca, Descripcion--%>
                                        <asp:BoundField DataField="Id_Codigo" HeaderText="ID" />
                                        <asp:BoundField DataField="Id_ventaja" HeaderText="Código de Documento" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripcción" />
                                        <asp:BoundField DataField="Id_merca" HeaderText="Numero de Item" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal nuevo Documentos-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarDocumento">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblModalDocumentos">Seleccione ventaja del item </h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos de los ventajas y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label"></label>
                                <asp:SqlDataSource
                                    ID="sqlventajas"
                                    runat="server"
                                    DataSourceMode="DataReader"
                                    ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                    ProviderName="MySql.Data.MySqlClient"
                                    SelectCommand="SELECT id_Ventaja,descripcion FROM  DB_Nac_Merca.tbl_30_Ventajas"></asp:SqlDataSource>

                                <label class="form-label">Ventajas</label>
                                <asp:DropDownList
                                    ID="ddlventajas" runat="server"
                                    selectlistitem="" DataSourceID="sqlventajas" class="form-control show-tick"
                                    DataTextField="descripcion" DataValueField="id_Ventaja" AppendDataBoundItems="true"
                                    ItemType="">
                                    <asp:ListItem Value="Seleccione"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator
                                    ID="ddlproveedoresv"
                                    ControlToValidate="ddlventajas"
                                    InitialValue="Seleccione"
                                    ErrorMessage="selecciones datos"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small"
                                    runat="server" />
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarDocumento" ValidationGroup="Validadocumento" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    <!-- modal eliminar documento-->
    <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <!-- TITULO -->
                    <h4 class="modal-title" id="LblDelete">ELIMINAR VENTAJA</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar la siguiente ventaja?
                    <br />
                    <asp:Label runat="server" ID="lblDocumento" Text="..."></asp:Label>
                    <asp:HiddenField runat="server" ID="lblHiddenIDDocumento" />


                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarDocumento" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


    <!-- modal editar documento-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarDocumento">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditarDoc">EDITAR VENTAJA</h4>
                    </div>
                    <div class="modal-body">
                        Luego de terminar de editar los datos de los documentos haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label"></label>
                                <asp:SqlDataSource
                                    ID="Sqlventajaedit"
                                    runat="server"
                                    DataSourceMode="DataReader"
                                    ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                    ProviderName="MySql.Data.MySqlClient"
                                    SelectCommand="SELECT id_Ventaja,descripcion FROM  DB_Nac_Merca.tbl_30_Ventajas"></asp:SqlDataSource>

                                <label class="form-label">Ventajas</label>
                                <asp:DropDownList
                                    ID="ddlventajaedit" runat="server"
                                    selectlistitem="" DataSourceID="Sqlventajaedit" class="form-control show-tick"
                                    DataTextField="descripcion" DataValueField="id_Ventaja" AppendDataBoundItems="true"
                                    ItemType="">
                                    <asp:ListItem Value="Seleccione"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1"
                                    ControlToValidate="ddlventajaedit"
                                    InitialValue="Seleccione"
                                    ErrorMessage="selecciones datos"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small"
                                    runat="server" />
                            </div>
                        </div>


                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificardocumento" ValidationGroup="ValidadocumentoEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content10" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
