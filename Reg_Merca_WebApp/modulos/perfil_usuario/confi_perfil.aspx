<%@ Page Title="Perfil de Usuario" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/perfil_usuario/master_perfil.Master" CodeBehind="confi_perfil.aspx.vb" Inherits="Reg_Merca_WebApp.confi_perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <script src="../plugins/momentjs/moment.js"></script>
    <link href="../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Datos Generales</a>

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
                <i class="material-icons">contact_page</i>
                <span>Datos Generales</span>
            </a>
        </li>
        <li>
            <a href="confi_cambio_contra.aspx">
                <i class="material-icons">password</i>
                <span>Cambio De Contraseña</span>
            </a>
        </li>
        <li>
            <a href="confi_perfil_preguntas.aspx">
                <i class="material-icons">help</i>
                <span>Preguntas De Seguridad</span>
            </a>
        </li>
    </ul>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Perfil de Usuarios
                        <small>Aquí encontrará su información de Perfil</small>
                    </h2>
                </div>
                <%--Contenido del form--%>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="form-group">
                                <asp:Label ID="Lbnomb" runat="server" class="form-label font-bold" Text="Nombre Completo:"></asp:Label>
                                <asp:Label ID="Lbnombre" runat="server" class="form-label" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="form-group form-float">
                                <asp:Label ID="Lbusua" runat="server" class="form-label font-bold" Text="Nombre de Usuario:"></asp:Label>
                                <asp:Label ID="Lbusuario" runat="server" class="form-label" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:Label ID="Lbcorre" runat="server" class="form-label font-bold" Text="Correo Eletrónico:"></asp:Label>
                            <asp:Label ID="Lbcorreo" runat="server" class="form-label" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ID="txtCorreoElectronico" AutoComplete="off" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Desea actualizar su correo eletrónico</label>
                                </div>
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ID="Reqcorreo"
                                    ControlToValidate="txtCorreoElectronico"
                                    ErrorMessage="Ingrese su correo electrónico"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small" />
                                <asp:RegularExpressionValidator
                                    ID="Regemail"
                                    runat="server"
                                    ControlToValidate="txtCorreoElectronico"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small"
                                    ErrorMessage="Su correo electronico es incorrecto"
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                            </div>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="btt_actualizarco"
                                class="btn bg-pink waves-effect">
          <i class="material-icons">refresh</i>
          <span>Actualizar</span>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="form-group form-float">
                                <asp:Label ID="lb_fec" runat="server" class="form-label font-bold" Text="Fecha de Creación:"></asp:Label>
                                <asp:Label ID="lb_fecha" runat="server" class="form-label" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="form-group form-float">
                                <asp:Label ID="Lb_usca" runat="server" class="form-label font-bold" Text="Su usuario vence el:"></asp:Label>
                                <asp:Label ID="lb_uscadu" runat="server" class="form-label font-bold col-red" Text=""></asp:Label>
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

