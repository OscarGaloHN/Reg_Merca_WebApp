<%@ Page Title="Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="creacion_documentos.aspx.vb" Inherits="Reg_Merca_WebApp.creacion_documentos" %>

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
            document.getElementById('ContentPrincipal_ddlDocumento').value = '';
            document.getElementById('ContentPrincipal_txtReferencia').value = '';
            document.getElementById('ContentPrincipal_chkPresencia').checked = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblDocumento').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML + ' - ' + row.cells[4].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddendddocumento').value = row.cells[3].innerHTML;

            xModal('red', 'ContentPrincipal_ddlDocumento', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_dddocumentoEditar').value = '';
            document.getElementById('ContentPrincipal_txtreferenciaEditar').value = '';
            //document.getElementById('ContentPrincipal_txt_chkPresenciaEditar').checked = '';
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddendddocumento').value = row.cells[3].innerHTML;

            if (row.cells[3].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_dddocumentoEditar').value = row.cells[3].innerHTML;
            }
            if (row.cells[5].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtreferenciaEditar').value = row.cells[5].innerHTML;
            }
            //if (row.cells[6].innerHTML != '&nbsp;') {
            //    document.getElementById('ContentPrincipal_txt_chkPresenciaEditar').Checked = row.cells[6].innerHTML;
            //}
            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[2].innerHTML;
            }
            xModal('pink', 'ContentPrincipal_dddocumentoEditar', 'modalEditar');
        }

    </script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Creacion de Documentos</a>
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
                <span>Creación de documentos</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />


    <script type="text/javascript">
        tituloImprimir = 'Listado de Documentos'
        xColumnas.push(2, 3, 4); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Documentos
                                 <small>Acontinuación el usuario podra visualizar los documentos con los que cuenta su poliza.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="xModal('teal','ContentPrincipal_ddlDocumento','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                                        <asp:BoundField DataField="id_doc" HeaderText="ID" />
                                        <asp:BoundField DataField="Id_Documento" HeaderText="Código de Documento" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción del Documento" />
                                        <asp:BoundField DataField="referencia" HeaderText="Referencia del Documento" />
                                        <asp:BoundField DataField="presencia" HeaderText="Presencia" />
                                        <asp:BoundField DataField="id_poliza_doc" HeaderText="ID Póliza" />
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
                        <h4 class="modal-title" id="lblModalDocumentos">NUEVO DOCUMENTO</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos de los documetos y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label"></label>
                                <asp:SqlDataSource
                                    ID="sqldocumentos"
                                    runat="server"
                                    DataSourceMode="DataReader"
                                    ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                    ProviderName="MySql.Data.MySqlClient"
                                    SelectCommand="SELECT id_Documento, UPPER(descripcion) descripcion FROM  DB_Nac_Merca.tbl_32_Cod_Documentos"></asp:SqlDataSource>

                                <label class="form-label">Documento</label>
                                <asp:DropDownList
                                    ID="ddldocumentos" runat="server"
                                    selectlistitem="" DataSourceID="sqldocumentos" class="form-control show-tick"
                                    DataTextField="descripcion" DataValueField="Id_Documento" AppendDataBoundItems="true"
                                    ItemType="">
                                    <asp:ListItem Value="Seleccione"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label"></label>

                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Referencia" AutoComplete="off" ValidationGroup="ValidaDocumento" runat="server" class="form-control" ID="txtreferencia" onkeypress="txNombres(event);"
                                            onkeydown="borrarespacios(this);BorrarRepetidas(this);" onkeyup="mayus(this); borrarespacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtReferencia"
                                        ErrorMessage="Ingrese la referencia."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaDocumento" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <div class="demo-switch-title">Presencia</div>
                                <div class="switch">
                                    <label>
                                        NO
                                    <input type="checkbox" name="CheckBox" runat="server" id="chkPresencia" class="filled-in chk-col-pink " />
                                        <span class="lever switch-col-pink"></span>
                                        SI
                                    </label>

                                </div>
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
                    <h4 class="modal-title" id="LblDelete">ELIMINAR DOCUMENTO</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que desea eliminar el siguiente documento?
                    <br />
                    <asp:Label runat="server" ID="lblDocumento" Text="..."></asp:Label>
                    <asp:HiddenField runat="server" ID="lblHiddenIDDocumento" />
                    <asp:HiddenField runat="server" ID="lblHiddendddocumento" />

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
                        <h4 class="modal-title" id="lblEditarDoc">EDITAR DOCUMENTO</h4>
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
                                    ID="sqldocumentosedit"
                                    runat="server"
                                    DataSourceMode="DataReader"
                                    ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                    ProviderName="MySql.Data.MySqlClient"
                                    SelectCommand="SELECT id_Documento, UPPER(descripcion) descripcion FROM  DB_Nac_Merca.tbl_32_Cod_Documentos"></asp:SqlDataSource>

                                <label class="form-label">Documento</label>
                                <asp:DropDownList
                                    ID="dddocumentoEditar" runat="server"
                                    selectlistitem="" DataSourceID="sqldocumentosedit" class="form-control show-tick"
                                    DataTextField="descripcion" DataValueField="Id_Documento" AppendDataBoundItems="true"
                                    ItemType="">
                                    <asp:ListItem Value="Seleccione"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="Referencia" AutoComplete="off" ValidationGroup="ValidadocumentoEditar" runat="server" class="form-control" ID="txtreferenciaEditar"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtreferenciaEditar"
                                        ErrorMessage="Ingrese la referencia."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidadocumentoEditar" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-2">
                                <div class="demo-switch-title">Presencia</div>
                                <div class="switch">
                                    <label>
                                        NO
                                     <input type="checkbox" name="CheckBox" runat="server" id="chkpresenciaEditar" class="filled-in chk-col-grey" />
                                        <span class="lever switch-col-grey"></span>
                                        SI
                                    </label>
                                </div>
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
