<%@ Page Title="Creación de Bultos" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="creacion_bultos.aspx.vb" Inherits="Reg_Merca_WebApp.creacion_bultos" %>

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
            document.getElementById('ContentPrincipal_txtmanifiesto').value = '';
            document.getElementById('ContentPrincipal_txt_trans').value = '';
            document.getElementById('ContentPrincipal_chkindicador').checked = '';
        }

        function GetSelectedRowDelete(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblbulto').innerHTML = row.cells[2].innerHTML + ' - ' + row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenIDbulto').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddenmanifiesto').value = row.cells[3].innerHTML;

            xModal('red', 'ContentPrincipal_txtmanifiesto', 'modalDelete');
        }

        function GetSelectedRowEdit(lnk) {
            document.getElementById('ContentPrincipal_txtmanifiestoEditar').value = '';
            document.getElementById('ContentPrincipal_txt_transEditar').value = '';
            document.getElementById('ContentPrincipal_chkindicadorEditar').checked = '';
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenmanifiesto').value = row.cells[3].innerHTML;

            if (row.cells[3].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txtmanifiestoEditar').value = row.cells[3].innerHTML;
            }
            if (row.cells[4].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_txt_transEditar').value = row.cells[4].innerHTML;
            }
            if (row.cells[5].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_chkindicadorEditar').checked = row.cells[5].innerHTML;
            }
            if (row.cells[2].innerHTML != '&nbsp;') {
                document.getElementById('ContentPrincipal_lblHiddenIDbulto').value = row.cells[2].innerHTML;
            }
            xModal('pink', 'ContentPrincipal_txtmanifiestoEditar', 'modalEditar');
        }

    </script>
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Creacion de Bultos</a>
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
        tituloImprimir = 'Listado de Bultos'
        xColumnas.push(2, 3, 4, 5, 6); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <script type="text/javascript">

</script>


    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Listado de Bultos de la Carátula - 
                        <asp:Label runat="server" ID="lblCatatura"></asp:Label>
                        <small>Acontinuación el usuario seleccionara datos de bultos para la poliza.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <button onclick="borrarTxtNuevo(); xModal('teal','ContentPrincipal_txtmanifiesto','modalNuevo');" type="button" class="btn btn-block btn-lg bg-teal waves-effect">
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

                        <div class="col-lg3- col-md-3 col-sm-6 col-xs-12 ">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttfin"
                                type="button"
                                ValidationGroup="Validarbttvolver"
                                class="btn btn-block btn-lg bg-teal waves-effect">
                                <i class="material-icons">verified</i>
                                <span>Finalizar e Imprimir Póliza</span>
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
                                        <asp:BoundField DataField="id_bulto" HeaderText="ID" />
                                        <asp:BoundField DataField="manifiesto" HeaderText="Manifiesto" />
                                        <asp:BoundField DataField="titulo_transporte" HeaderText="Título de transporte" />
                                        <asp:BoundField DataField="indicador" HeaderText="Indicador" />
                                        <asp:BoundField DataField="id_poliza_bul" HeaderText="ID Póliza" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- modal nuevo bulto-->
    <div class="modal fade" id="modalNuevo" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttGuardarbulto">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblMOdalCorreo">NUEVO BULTO</h4>
                    </div>
                    <div class="modal-body">
                        Ingrese todos los datos de los bultos y haga clic en el botón 'GUARDAR' para confirmar el nuevo registro.
                                            <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox MaxLength="17" placeholder="Manifiesto" AutoComplete="off" ValidationGroup="Validabulto" runat="server" class="form-control" ID="txtmanifiesto" onkeypress="isNumberOrLetter(evt) ;"
                                            onkeyup="mayus(this); borrarespacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="reqnombrevacio" ControlToValidate="txtmanifiesto"
                                        ErrorMessage="Ingrese el manifiesto."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validabulto" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox MaxLength="25" placeholder="Título Transporte" AutoComplete="off" ValidationGroup="Validabulto" runat="server" class="form-control" ID="txt_trans" onkeypress="isNumberOrLetter(evt) ;"
                                            onkeyup="mayus(this); borrarespacios(this);">></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="validartrans" ControlToValidate="txt_trans"
                                        ErrorMessage="Ingrese el nombre de título de transporte."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="Validatrans" />
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-2">
                                <div class="demo-switch-title">Indicador Bultos</div>
                                <div class="switch">
                                    <label>
                                        NO
                                    <input type="checkbox" name="CheckBox" runat="server" id="chkindicador" class="filled-in chk-col-pink " />
                                        <span class="lever switch-col-pink"></span>
                                        SI
                                    </label>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttGuardarbulto" ValidationGroup="Validabulto" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
                        <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>


    <!-- modal eliminar bulto-->
    <div class="modal fade" id="modalDelete" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <!-- TITULO -->
                    <h4 class="modal-title" id="LblDelete">ELIMINAR BULTO</h4>
                </div>
                <div class="modal-body">
                    ¿Seguro que dese eliminar este bulto:
                    <asp:Label runat="server" ID="lblbulto" Text="..."></asp:Label>?
                        <asp:HiddenField runat="server" ID="lblHiddenIDbulto" />
                    <asp:HiddenField runat="server" ID="lblHiddenmanifiesto" />
                    <br />
                    <br />
                    <!-- CUERPO DEL MODAL -->

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttEliminarbulto" class="btn  btn-link  waves-effect">ELIMINAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>


    <!-- modal editar bulto-->
    <div class="modal fade" id="modalEditar" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <asp:Panel ID="Panel1" runat="server" DefaultButton="bttGuardarbulto">
                <div class="modal-content">
                    <div class="modal-header">
                        <!-- TITULO -->
                        <h4 class="modal-title" id="lblEditarbul">EDITAR BULTO</h4>
                    </div>
                    <div class="modal-body">
                        Luego de terminar de editar los datos de los bultos haga clic en el botón 'MODIFICAR' para confirmar los nuevos datos.
                        <br />
                        <br />
                        <!-- CUERPO DEL MODAL -->

                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox MaxLength="17" placeholder="Manifiesto" AutoComplete="off" ValidationGroup="ValidabultoEditar" runat="server"
                                            class="form-control" ID="txtmanifiestoEditar" onkeypress="isNumberOrLetter(evt)"
                                            onkeyup="mayus(this); borrarespacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtmanifiestoEditar"
                                        ErrorMessage="Ingrese el manifiesto."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidabultoEditar" />
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <div class="form-line">
                                        <asp:TextBox MaxLength="25" placeholder="Título Transporte" AutoComplete="off" ValidationGroup="ValidabultoEditar" runat="server" class="form-control" ID="txt_transEditar"
                                            onkeyup="mayus(this); borrarespacios(this);"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txt_transEditar"
                                        ErrorMessage="Ingrese el nombre de título de transporte."
                                        Display="Dynamic"
                                        ForeColor="White" Font-Size="Small" ValidationGroup="ValidabultoEditar" />
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-2">
                                <div class="demo-switch-title">Indicador Bultos</div>
                                <div class="switch">
                                    <label>
                                        NO
                                     <input type="checkbox" name="CheckBox" runat="server" id="chkindicadorEditar" class="filled-in chk-col-pink " />
                                        <span class="lever switch-col-teal"></span>
                                        SI
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton runat="server" ID="bttModificarbulto" ValidationGroup="ValidabultoEditar" class="btn  btn-link  waves-effect">MODIFICAR</asp:LinkButton>
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
