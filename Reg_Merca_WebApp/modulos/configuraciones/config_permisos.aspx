<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="config_permisos.aspx.vb" Inherits="Reg_Merca_WebApp.config_permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <div class="row clearfix">
                         
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
