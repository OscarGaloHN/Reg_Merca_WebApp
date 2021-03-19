<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Mantenimiento/master_mantenimiento.Master" CodeBehind="almacen_mant.aspx.vb" Inherits="Reg_Merca_WebApp.almacen_mant" %>
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
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
