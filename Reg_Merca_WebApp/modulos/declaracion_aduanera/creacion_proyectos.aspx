<%@ Page Title="Creación de Proyectos" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="creacion_proyectos.aspx.vb" Inherits="Reg_Merca_WebApp.creacion_proyectos" %>

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
    <%--<script src="https://cdn.datatables.net/plug-ins/1.10.24/dataRender/datetime.js "></script>--%>

    <script src="../src/jsTabla.js"></script>


    <script type="text/javascript">

        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;

            document.getElementById('ContentPrincipal_lblnombre').innerHTML = row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblid').value = row.cells[2].innerHTML;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Creación y Listado de Proyectos</a>
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
            tituloImprimir = 'Listado de Pólizas'
            xColumnas.push(1, 2, 3, 4, 5); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
            xMargenes.push(100, 0, 100, 0)
            xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
            xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

<%--    <script type="text/javascript">

</script>--%>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Listado de pólizas
         
                        <small>A continuación se muestra el listado de pólizas</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-xs-6">
                            <h2 class="card-inside-title">Rango de fechas</h2>
                            <div class="input-daterange input-group" id="bs_datepicker_range_container1">
                                <span class="input-group-addon">Del</span>
                                <div class="form-line">
                                    <input type="text" class="form-control" placeholder="Fecha inicio...">
                                </div>
                                <span class="input-group-addon">hasta el</span>
                                <div class="form-line">
                                    <input type="text" class="form-control" placeholder="Fecha fin...">
                                </div>
                            </div>
                        </div>


                        <%--                          <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <h2 class="card-inside-title">Rango de Fecha</h2>
                            <div class="input-daterange input-group" id="bs_datepicker_range_container">
                                <div class="form-line">
                                    <input name="dt_finicial" type="date" runat="server" class="form-control" placeholder="Fecha inicio">
                                </div>
                                <span class="input-group-addon">to</span>
                                <div class="form-line">
                                    <input name="dt_ffin" type="date" runat="server" class="form-control" placeholder="Fecha Fin">
                                </div>
                            </div>
                    </div>--%>

                                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <label class="form-label"></label>
                            <div class="form-line">
                                <asp:TextBox
                                    AutoComplete="off"
                                    ID="txt_cliente"
                                    runat="server" onkeyup="mayus(this); borrarespacios(this);" onkeypress="return txNombres(event)"
                                    class="form-control">
                                </asp:TextBox>
                                <label class="form-label">Nombre del cliente</label>
                            </div>
                        </div>
                    </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <asp:SqlDataSource
                                ID="sqlestadopol"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"
                                SelectCommand="Select descripcion, id_estado from DB_Nac_Merca.tbl_19_estado where id_estado in (7,8)"></asp:SqlDataSource>

                            <label class="form-label">Estado</label>
                            <asp:DropDownList
                                ID="ddlestado" runat="server" DataSourceID="sqlestadopol" class="form-control show-tick"
                                DataTextField="descripcion" DataValueField="id_estado" AppendDataBoundItems="true" ItemType="">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>

                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="form-group form-float">
                                <label class="form-label"></label>
                                <div class="form-line">
                                    <asp:TextBox
                                        onkeydown="borrarespacios(this);BorrarRepetidas(this);" onkeyup="mayus(this); borrarespacios(this);" onkeypress="return txNombres(event)"
                                        AutoComplete="off"
                                        MaxLength="15"
                                        ID="txt_usuario"
                                        runat="server"
                                        class="form-control">
                                    </asp:TextBox>
                                    <label class="form-label">Usuario</label>
                                </div>
<%--                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txt_usuario"
                                    ErrorMessage="Ingrese nombre de usuario"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                            </div>
                        </div>

                        <div class="body">
                            <div class="row clearfix">
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                                    <asp:LinkButton
                                        Width="100%"
                                        runat="server"
                                        ID="btt_buscar"
                                        type="button"
                                        class="btn bg-teal waves-effect">
          <i class="material-icons">search</i>
          <span>Buscar</span>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                                    <asp:LinkButton
                                        Width="100%"
                                        runat="server"
                                        ID="btt_limpiar"
                                        ValidationGroup="Validarbttvolver"
                                        type="button"
                                        class="btn bg-teal waves-effect">
          <i class="material-icons">refresh</i>
          <span>Limpiar</span>
                                    </asp:LinkButton>
                                </div>

                                <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                                    <asp:LinkButton
                                        Width="100%"
                                        runat="server"
                                        ID="bttNuevo"
                                        type="button"
                                        ValidationGroup="Validarnuevo"
                                        class="btn bg-teal waves-effect">
          <i class="material-icons">add</i>
          <span>Nuevo</span>
                                    </asp:LinkButton>

                                </div>
                            </div>

                            <div class="row clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                            Width="100%">
                                            <Columns>
                                                <asp:BoundField HeaderText="Editar" DataField="id_poliza" HtmlEncode="False" DataFormatString="<a class='btn bg-pink waves-effect' href='caratula.aspx?idCaratula={0}&action=update&ignore=92​​'><i class='material-icons'>edit</i> </a>" />
                                                <%--                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <button onclick="return GetSelectedRow(this);" type="button" data-color="red" class="btn bg-deep-orange waves-effect"><i class="material-icons">delete</i></button>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                <asp:BoundField DataField="id_poliza" HeaderText="ID" />
                                                <asp:BoundField DataField="fecha_creacion" HeaderText="Fecha creación" />
                                                <asp:BoundField DataField="nombrec" HeaderText="Cliente" />
                                                <asp:BoundField DataField="descripcion" HeaderText="Estado" />
                                                <asp:BoundField DataField="nombre" HeaderText="Usuario" />

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
                                            <h4 class="modal-title">Eliminar Caratula</h4>
                                        </div>
                                        <div class="modal-body">
                                            <h2 class="modal-title">
                                                <b>¿Está seguro que desea eliminar el registro?</b> </h2>
                                            <br />
                                            <b>El usuario pertenece a:</b>
                                            <asp:Label ID="lblUsuario" class="msg" runat="server" Text="..."></asp:Label>
                                            <br />
                                            <br />
                                            Si desea eliminar este registro de usuario, haga clic en el botón eliminar.
                                    <br>
                                            Si desea cancelar haga clic en el boton cerrar.
                                    <br />
                                            <div class="row clearfix">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <asp:HiddenField ID="lblnombre" runat="server" />
                                                    <asp:HiddenField ID="lblid" runat="server" />
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
    <!-- Bootstrap Datepicker Plugin Js -->
    <script src="../../plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <!-- Select Plugin Js -->
    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js"></script>
</asp:Content>
