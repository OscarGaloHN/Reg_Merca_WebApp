<%@ Page Title="Bitácora" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/bitacora/master_bitacora.Master" CodeBehind="bitacora.aspx.vb" Inherits="Reg_Merca_WebApp.bitacora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>

    <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js"></script>


    <script src="../src/jsTabla.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Registro De Actividad</a>
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
                <i class="material-icons">receipt_long</i>
                <span>Bitácora</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />

    <script type="text/javascript">
        tituloImprimir = 'Detalle de la Bitácora'
        xColumnas.push(0, 1, 2, 3, 4, 5); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(50, 0, 50, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Registro de bitácora
         
                        <small>A continuación se muestra la actividad de las acciones de los usuarios en el sistema.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="col-xs-6">
                        <h2 class="card-inside-title">Rango de fechas</h2>
                        <div class="input-daterange input-group" id="bs_datepicker_range_container">
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

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id_bitacora" HeaderText="ID" />
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="usuario" HeaderText="Usuario" />
                                        <asp:BoundField DataField="objeto" HeaderText="Objeto" />
                                        <asp:BoundField DataField="accion" HeaderText="Acción" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                                    </Columns>
                                </asp:GridView>
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
</asp:Content>
