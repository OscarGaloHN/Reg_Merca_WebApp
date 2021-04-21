<%@ Page Language="vb" Title="Datos Complementarios" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="items_dcomplementarios.aspx.vb" Inherits="Reg_Merca_WebApp.items_dcomplementarios" %>

<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
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
            document.getElementById('ContentPrincipal_ddlcomplementario').value = '';
            document.getElementById('ContentPrincipal_txtvalor').value = '';

        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblDocumento').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML + ' - ' + row.cells[4].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[5].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddendddocumento').value = row.cells[2].innerHTML;

            xModal('red', 'ContentPrincipal_ddlcomplementario', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_ddlcomplementariedit').value = '';
            document.getElementById('ContentPrincipal_txtvaloredit').value = '';

            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddendddocumento').value = row.cells[2].innerHTML;

            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_ddlcomplementariedit').value = row.cells[2].innerHTML;
            }
            if (row.cells[4].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtvaloredit').value = row.cells[4].innerHTML;
            }

            if (row.cells[5].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDDocumento').value = row.cells[5].innerHTML;
            }
            xModal('pink', 'ContentPrincipal_ddlcomplementariedit', 'modalEditar');
        }

    </script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Items de Datos Complementarios</a>
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
            <a href="caratula.aspx">
                <i class="material-icons">aspect_ratio</i>
                <span>Declaración Aduanera</span>
            </a>
        </li>
    </ul>



</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />


    <script type="text/javascript">
        tituloImprimir = 'Listado de Documentos Complementarios del Item'
        xColumnas.push(2, 3, 4); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Datos Complementarios del Item - 
                        <asp:Label runat="server" ID="lblitems"></asp:Label>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo; xModal('teal','ContentPrincipal_ddlcomplementario','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">

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
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttcontinuar"
                                type="button"
                                ValidationGroup="Validarbttvolver"
                                class="btn btn-block btn-lg bg-teal waves-effect">
                                <i class="material-icons">keyboard_tab</i>
                                <span>Continuar</span>
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
                                            <%-- Id_DatoComple, descripcion, Valor, Id_Codigo--%>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id_DatoComple" HeaderText="Código Dato" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                        <asp:BoundField DataField="Valor" HeaderText="Valor" />
                                        <asp:BoundField DataField="Id_Codigo" HeaderText="ID" />
                                        <asp:BoundField DataField="Id_Merca" HeaderText="ID Items" />
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
                        Ingrese todos los datos del ítem de datos complementarios y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label"></label>
                                <asp:SqlDataSource
                                    ID="sqldatoscomplementarios"
                                    runat="server"
                                    DataSourceMode="DataReader"
                                    ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                    ProviderName="MySql.Data.MySqlClient"
                                    SelectCommand="SELECT Id_DatoComple, UPPER(Descripcion) Descripcion FROM  DB_Nac_Merca.tbl_31_Cod_Datos_Complementarios"></asp:SqlDataSource>

                                <label class="form-label">Documento<span class="required">*</span></label>
                                <asp:DropDownList
                                    ID="ddlcomplementario" runat="server"
                                    selectlistitem="" DataSourceID="sqldatoscomplementarios" class="form-control show-tick" data-live-search="true"
                                    DataTextField="Descripcion" DataValueField="Id_DatoComple" AppendDataBoundItems="true"
                                    ItemType="">
                                    <asp:ListItem Value="Seleccione"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="validarcomplementario"
                                    ControlToValidate="ddlcomplementario" InitialValue="Seleccione" ErrorMessage="seleccione un dato"
                                    ForeColor="OrangeRed" Font-Size="X-Small" runat="server" />
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label">Valor</label>


                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="" AutoComplete="off" ValidationGroup="ValidaDocumento" runat="server" class="form-control" ID="txtvalor"
                                            onkeydown="borrarespacios(this);" onkeyup="mayus(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtvalor"
                                        ErrorMessage="Ingrese valor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaDocumento" />
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
                    ¿Seguro que desea eliminar el siguiente dato complementario?
                    <br />
                    <asp:Label runat="server" ID="lblDocumento" Text="..."></asp:Label>
                    <asp:HiddenField runat="server" ID="lblHiddenIDDocumento" />
                    <asp:HiddenField runat="server" ID="lblHiddendddocumento" />


                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarDocumento"  ValidationGroup="Validadocumento" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
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
                        Luego de terminar de editar el ítem complementario haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
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
                                    SelectCommand="SELECT Id_DatoComple, UPPER(Descripcion) Descripcion FROM  DB_Nac_Merca.tbl_31_Cod_Datos_Complementarios"></asp:SqlDataSource>

                                <label class="form-label">Documento</label>
                                <asp:DropDownList
                                    ID="ddlcomplementariedit" runat="server"
                                    selectlistitem="" DataSourceID="sqldocumentosedit" class="form-control show-tick" data-live-search="true"
                                    DataTextField="Descripcion" DataValueField="Id_DatoComple" AppendDataBoundItems="true"
                                    ItemType="">
                                    <asp:ListItem Value="Seleccione"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                    ControlToValidate="ddlcomplementario" InitialValue="Seleccione" ErrorMessage="seleccione un dato"
                                    ForeColor="OrangeRed" Font-Size="X-Small" runat="server" />
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <label class="form-label">Valor</label>

                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox placeholder="" AutoComplete="off" ValidationGroup="ValidaDocumento" runat="server" class="form-control" ID="txtvaloredit"
                                            onkeydown="borrarespacios(this);BorrarRepetidas(this);" onkeyup="mayus(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtvalor"
                                        ErrorMessage="Ingrese valor."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidaDocumento" />
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
        <!-- Select Plugin Js -->
    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js"></script>
</asp:Content>
