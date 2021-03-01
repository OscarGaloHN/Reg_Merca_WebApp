<%@ Page Title="Gestio de Usuarios" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="Config_Gestion_Usuario.aspx.vb" Inherits="Reg_Merca_WebApp.Config_Gestion_Usuario" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="../plugins/bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Select Css -->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Gestion de Usuarios</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <li>
            <a href="../../index.html">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li>
            <a href="../../index.html">
                <i class="material-icons">settings</i>
                <span>Configuraciones</span>
            </a>
        </li>

        <li class="active">

            <a href="#">
                <i class="material-icons">edit</i>
                <span>Gestion de usuarios</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Gestion de usuarios
         
                        <small>El administrador podrá editar información general del
            usuario</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ID="txtNombre"
                                        AutoComplete="off"
                                        runat="server"
                                       onkeypress="return txNombres(event)"
                                        class="form-control"
                                        onkeyup="mayusculapalabras(this);">
                                    </asp:TextBox>
                                    <label class="form-label">Nombre</label>
                                </div>
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ID="Requsuario"
                                    ControlToValidate="txtNombre"
                                    ErrorMessage="Ingrese su nombre"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        MaxLength="150"
                                        ID="txtUsuario"
                                        AutoComplete="off"
                                        runat="server"
                                        onkeypress="return isNumberOrLetter(event)"
                                        class="form-control"></asp:TextBox>
                                    <label class="form-label">Usuario</label>
                                </div>
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ID="Reqnombre"
                                    ControlToValidate="txtUsuario"
                                    ErrorMessage="Ingrese su usuario"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1 align-right">
                            <label class="form-label">Rol:</label>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-11">
                            <asp:SqlDataSource
                                ID="SqlRol"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"
                                SelectCommand="Select rol, id_rol from DB_Nac_Merca.tbl_15_rol where id_rol not in (5,6)"></asp:SqlDataSource>
                            <asp:DropDownList
                                ID="cmbRol" runat="server" DataSourceID="SqlRol" class="form-control show-tick"
                                DataTextField="Rol" DataValueField="id_Rol" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ID="txtCorreoElectronico"
                                        AutoComplete="off"
                                        runat="server"
                                        class="form-control"></asp:TextBox>
                                    <label class="form-label">Correo Eletrónico</label>
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
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ID="txtContraseña"
                                        AutoComplete="off"
                                        PasswordChar="*"
                                        runat="server"
                                        class="form-control" TextMode="Password"></asp:TextBox>
                                    <label class="form-label">Contraseña</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="ReqContaseña" ControlToValidate="txtContraseña"
                                    ErrorMessage="Ingrese una contraseña"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator runat="server" ID="Regulcontraseña"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtContraseña"
                                    ValidationExpression="^[a-zA-Z0-9'@&#.\s]{5,10}$"
                                    ErrorMessage="El rango de caracteres debe de ser entre (5 - 10)." />
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttResetear"
                                class="btn bg-pink waves-effect">
          <i class="material-icons">refresh</i>
          <span>Resetear</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <h2 class="card-inside-title">Fecha de Creación</h2>
                            <div class="form-group">
                                <div class="form-line" id="bs_Fecha_Creación">
                                    <input runat="server"
                                        id="Fecha_Creacion"
                                        type="date"
                                        class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
                            <h2 class="card-inside-title">Fecha de Vencimiento</h2>
                            <div class="form-group">
                                <div class="form-line" id="bs_Fecha_Vencimiento">
                                    <input
                                        runat="server"
                                        id="Fecha_Vencimiento"
                                        type="date"
                                        class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-2">
                            <label class="form-label">Estado:</label>
                        </div>
                        <asp:SqlDataSource
                            ID="SqlEstado"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="Select descripcion, id_estado from DB_Nac_Merca.tbl_19_estatus where id_estado not in (5,6)"></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-10">
                            <asp:DropDownList
                                ID="cmbEstado" runat="server" DataSourceID="SqlEstado" class="form-control show-tick"
                                DataTextField="descripcion" DataValueField="id_estado" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 col-sm-offset-8 col-md-offset-8">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttGuardar"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">save</i>
          <span>Guardar</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
