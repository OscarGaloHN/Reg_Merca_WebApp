<%@ Page Title="Creacion de Items" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.Master" CodeBehind="Creacion_items.aspx.vb" Inherits="Reg_Merca_WebApp.Creacion_items" %>

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
    <script src="https://cdn.datatables.net/plug-ins/1.10.24/dataRender/datetime.js "></script>
    <script src="../src/jsTabla.js"></script>


    <script src="../src/jsModales.js"></script>


    <script type="text/javascript">

        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblHiddenitems').innerHTML = row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHiddennumitems').value = row.cells[2].innerHTML;
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Creación y listado de items</a>
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

        <li class="active">
            <a href="#">
                <i class="material-icons">aspect_ratio</i>
                <span>Declaracion Aduanera</span>
            </a>
        </li>
    </ul>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">

    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />


    <script type="text/javascript">
            tituloImprimir = 'Listado de Items'
            xColumnas.push(2, 4, 5, 6, 7, 8); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
            xMargenes.push(100, 0, 100, 0)
            xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
            xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Listado de Items de la caratula - 
                        <asp:Label runat="server" ID="lblCatatura"></asp:Label>

                        <small>A continuación se muestra el listado de items</small>
                    </h2>
                </div>
                <div class="body">


                    <div class="row clearfix">




                        <div class="body">
                            <div class="row clearfix">

                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                                    <asp:LinkButton
                                        Width="100%"
                                        runat="server"
                                        ID="bttNuevo"
                                        type="button"
                                        class="btn bg-teal waves-effect">
          <i class="material-icons">add</i>
          <span>Nuevo</span>
                                    </asp:LinkButton>

                                </div>


                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                                    <asp:LinkButton
                                        Width="100%"
                                        runat="server"
                                        ID="btt_volver"
                                        type="button"
                                        class="btn bg-teal waves-effect">
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
                                                <asp:BoundField HeaderText="Editar" DataField="ID_Merca;Id_poliza" HtmlEncode="False" DataFormatString="<a class='btn bg-pink waves-effect' href='items.aspx?iditems={0}&idCaratula={1}&action=update&ignore=92​​'><i class='material-icons'>edit</i> </a>" />
                                             
                                                                                          <%-- <asp:HyperLinkField HeaderText="Editar"  DataNavigateUrlFields="ID_Merca;Id_poliza" DataNavigateUrlFormatString=" href=items.aspx?iditems={0}&idCaratula={1}&action=update&ignore=92  " Text="<i class='btn bg-pink waves-effect'><i class='material-icons'>edit</i> " />--%>
                                                
                                                <asp:TemplateField HeaderText="Eliminar">
                                                    <ItemTemplate>
                                                        <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:BoundField DataField="row_number" HeaderText="Numero de Item" />--%>
                                                <asp:BoundField DataField="numeroitems" HeaderText="Número de Items" />
                                                <asp:BoundField DataField="ID_Merca" HeaderText="Id Items" />
                                                <asp:BoundField DataField="Id_poliza" HeaderText="Número de Poliza" />
                                                <asp:BoundField DataField="pesoneto" HeaderText="Peso Neto" />
                                                <asp:BoundField DataField="num_partida" HeaderText="Partida Arancelaria" />
                                                <asp:BoundField DataField="cod_pais_fab" HeaderText="País de origen" />
                                                <asp:BoundField DataField="importes_factura" HeaderText="Importe de Factura " />


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade" id="mdModal" tabindex="-1" role="dialog">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <asp:Panel ID="PanelEditor" runat="server" DefaultButton="bttEliminar">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Eliminar Items</h4>
                                        </div>
                                        <div class="modal-body">
                                            <h2 class="modal-title">
                                                <b>¿Está seguro que desea eliminar el registro?</b> </h2>
                                            <br />
                                            <b>El usuario pertenece a:</b>
                                            <asp:Label ID="lblitems" class="msg" runat="server" Text="..."></asp:Label>
                                            <br />
                                            <br />
                                            Si desea eliminar este registro de items, haga clic en el botón eliminar.
                                    <br>
                                            Si desea cancelar haga clic en el boton cerrar.
                                    <br />
                                            <div class="row clearfix">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <asp:HiddenField ID="lblHiddenitems" runat="server" />
                                                    <asp:HiddenField ID="lblHiddennumitems" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:LinkButton runat="server" ID="bttEliminar" ValidationGroup="actualizarRespuesta" class="btn  btn-link  waves-effect">Eliminar</asp:LinkButton>
                                            <button type="button" class="btn bg-pink waves-effect" data-dismiss="modal">CERRAR</button>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
