<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="config_permisos.aspx.vb" Inherits="Reg_Merca_WebApp.config_permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Select Css -->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
     <a class="navbar-brand" href="#">Permisos</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
      <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <% If Session("user_idUsuario") <> Nothing Then %>
        <% If CBool(Application("ParametrosSYS")(2)) = True Then   %>
        <li>
            <a href="../menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li >
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
                <i class="material-icons-round">vpn_key</i>
                <span>Permisos</span>
            </a>
        </li>

        <%ELSE %>

        <li class="active">
            <a href="#">
                <i class="material-icons">settings</i>
                <span>Configuraciones</span>
            </a>
        </li>
    </ul>
    <% End if  %>



    <% End if  %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
        <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold">Permisos por rol
                        <small>Permite controlar los accesos al sistema</small>
                    </h2>

                </div>
                <div class="body">
                           <asp:SqlDataSource
                            ID="SqlRoles"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT id_rol, rol FROM DB_Nac_Merca.tbl_15_rol where id_rol != 5;"></asp:SqlDataSource>
                        <div class="row clearfix">
                            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                 <p>
                                        <b>Seleccione un rol:</b>
                                    </p>

                                <asp:DropDownList  onchange="document.getElementById('txtrespuesta').focus();
                                    document.getElementById('txtrespuesta').value = '';"
                                    ID="ddlRoles" runat="server" DataSourceID="SqlRoles" class="form-control show-tick"
                                    DataTextField="rol" DataValueField="id_rol" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
        <!-- Select Plugin Js -->
    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js"></script>

</asp:Content>
