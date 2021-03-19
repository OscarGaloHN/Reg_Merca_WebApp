﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Mantenimiento/master_mantenimiento.Master" CodeBehind="almacen_mant.aspx.vb" Inherits="Reg_Merca_WebApp.almacen_mant" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="../src/jsTabla.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
     <a class="navbar-brand" href="#"> Mantenimiento del Sistema</a>
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
                <span>Almacén</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;"> MANTENIMIENTO DE ALMACEN
         
                        <small>A continuacion se muestra el mantenimiento del Almacen.</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id_bitacora" HeaderText="ID" />Id_almacen, Nombre, Ubicación, Contacto, Tel
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
</asp:Content>
