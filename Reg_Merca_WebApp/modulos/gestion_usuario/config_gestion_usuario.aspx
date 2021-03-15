<%@ Page ValidateRequest="false"  Title="Gestio de Usuarios" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/gestion_usuario/master_gestion_usuarios.Master" CodeBehind="config_gestion_usuario.aspx.vb" Inherits="Reg_Merca_WebApp.Config_Gestion_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../../plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Select Css -->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Configuración de Usuarios</a>
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
                <i class="material-icons">manage_accounts</i>
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
                    <h2 style="font-weight: bold;">Gestión De Usuarios
         
                        <small>El administrador podrá editar información general del
            usuario</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ID="txtNombre"
                                        AutoComplete="off"
                                        runat="server"
                                        onkeypress="txNombres(event);"
                                        onkeydown="borrarespacios(this);BorrarRepetidas(this);"
                                        class="form-control"
                                        onkeyup="mayus(this); borrarespacios(this);">
                                    </asp:TextBox>
                                    <label class="form-label">Nombre</label>
                                </div>
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ID="Reqnombre"
                                    ControlToValidate="txtNombre"
                                    ErrorMessage="Ingrese su nombre"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    ValidationExpression="^.*(.)\1{!2}.*"
                                    Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        MaxLength="150"
                                        ID="txtUsuario"
                                        AutoComplete="off"
                                        runat="server"
                                        onkeypress="txNombres(event);"
                                        onkeyup="mayus(this);"
                                        onfocusout="mayus(this);"
                                        onkeydown="mayus(this);BorrarRepetidas(this);"
                                        class="form-control"></asp:TextBox>

                                    <label class="form-label">Usuario</label>
                                </div>
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ID="Requsuario"
                                    ControlToValidate="txtUsuario"
                                    ErrorMessage="Ingrese su usuario"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    Font-Size="X-Small" />
                            </div>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <label class="form-label">Rol:</label>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-11">
                            <asp:SqlDataSource
                                ID="SqlRol"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                            <asp:DropDownList
                                ID="cmbRol" runat="server" DataSourceID="SqlRol" class="form-control show-tick"
                                DataTextField="Rol" DataValueField="id_Rol" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
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
                    </div>
                    <div class="row clearfix">
                        <asp:Panel ID="panelContra" runat="server">


                            <div class="col-lg-3 col-md-5 col-sm-5 col-xs-10">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox
                                            ID="txtContra"
                                            AutoComplete="off"
                                            PasswordChar="*"
                                            runat="server"
                                            class="form-control" TextMode="Password"></asp:TextBox>
                                        <label class="form-label">Nueva Contraseña</label>
                                    </div>

                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtContra"
                                        ErrorMessage="Ingrese su contraseña."
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />

                                    <asp:RegularExpressionValidator runat="server" ID="reqcontra"
                                        Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                        ControlToValidate="txtContra" />

                                    <asp:RegularExpressionValidator runat="server" ID="ReqValidacionRobusta"
                                        Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                        ControlToValidate="txtContra" />
                                </div>
                            </div>
                            <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                <div class="input-group">
                                    <span>
                                        <i id="mostraractual" onmouseover="mouseOver('ContentPrincipal_txtContra','mostraractual')" onmouseout="mouseOut('ContentPrincipal_txtContra','mostraractual')" style="cursor: default" class="material-icons">visibility_off</i>
                                    </span>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-5 col-sm-5 col-xs-10">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <div class="form-line">
                                            <asp:TextBox
                                                ID="txtContraConfirmar"
                                                AutoComplete="off"
                                                runat="server"
                                                TextMode="Password"
                                                class="form-control"></asp:TextBox>
                                            <label class="form-label">Confirmar Contraseña</label>
                                        </div>
                                    </div>
                                    <asp:CompareValidator ID="Comparecontra" runat="server" ControlToCompare="txtContra" ControlToValidate="txtContraConfirmar"
                                        ErrorMessage="Su contraseña no coincide"
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />
                                    <asp:RequiredFieldValidator runat="server" ID="Requcontraconf" ControlToValidate="txtContraConfirmar"
                                        ErrorMessage="Ingrese de nuevo la Contraseña"
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />
                                </div>
                            </div>
                            <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                <div class="input-group">
                                    <span>
                                        <i id="mostrarconfirmar" onmouseover="mouseOver('ContentPrincipal_txtContraConfirmar','mostrarconfirmar')" onmouseout="mouseOut('ContentPrincipal_txtContraConfirmar','mostrarconfirmar')" style="cursor: default" class="material-icons">visibility_off</i>
                                    </span>
                                </div>
                            </div>
                        </asp:Panel>



                        <asp:Panel ID="panelResetear" runat="server">


                            <div class="col-lg-3 col-md-5 col-sm-5 col-xs-10">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox
                                            ID="txtcontraresetear"
                                            AutoComplete="off"
                                            PasswordChar="*"
                                            runat="server"
                                            ValidationGroup="validarResetear"
                                            class="form-control" TextMode="Password">
                                        
                                        </asp:TextBox>
                                        <label class="form-label">Nueva Contraseña</label>
                                    </div>

                                    <asp:RequiredFieldValidator ValidationGroup="validarResetear" runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtcontraresetear"
                                        ErrorMessage="Ingrese su contraseña."
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />

                                    <asp:RegularExpressionValidator ValidationGroup="validarResetear" runat="server" ID="reqcontraresetear"
                                        Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                        ControlToValidate="txtcontraresetear" />

                                    <asp:RegularExpressionValidator ValidationGroup="validarResetear" runat="server" ID="ReqValidacionRobustaresetear"
                                        Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                        ControlToValidate="txtcontraresetear" />
                                </div>
                            </div>
                            <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                <div class="input-group">
                                    <span>
                                        <i id="mostraractualresetear" onmouseover="mouseOver('ContentPrincipal_txtcontraresetear','mostraractualresetear')" onmouseout="mouseOut('ContentPrincipal_txtcontraresetear','mostraractualresetear')" style="cursor: default" class="material-icons">visibility_off</i>
                                    </span>
                                </div>
                            </div>

                            <div class="col-lg-3 col-md-5 col-sm-5 col-xs-10">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <div class="form-line">
                                            <asp:TextBox
                                                ID="txtContraConfirmarresetear"
                                                AutoComplete="off"
                                                runat="server"
                                                ValidationGroup="validarResetear"
                                                TextMode="Password"
                                                class="form-control"></asp:TextBox>
                                            <label class="form-label">Confirmar Contraseña</label>
                                        </div>
                                    </div>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtcontraresetear" ControlToValidate="txtContraConfirmarresetear"
                                        ErrorMessage="Su contraseña no coincide"
                                        Display="Dynamic" ValidationGroup="validarResetear"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtContraConfirmarresetear"
                                        ErrorMessage="Ingrese de nuevo la Contraseña"
                                        Display="Dynamic" ValidationGroup="validarResetear"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />
                                </div>
                            </div>
                            <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                <div class="input-group">
                                    <span>
                                        <i id="mostrarconfirmarresetear" onmouseover="mouseOver('ContentPrincipal_txtContraConfirmarresetear','mostrarconfirmarresetear')" onmouseout="mouseOut('ContentPrincipal_txtContraConfirmarresetear','mostrarconfirmarresetear')" style="cursor: default" class="material-icons">visibility_off</i>
                                    </span>
                                </div>
                            </div>
                        </asp:Panel>










                        <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttResetear"
                                class="btn bg-pink waves-effect"
                                ValidationGroup="validarResetear">
                               <i class="material-icons">refresh</i> <span>Resetear</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttGenerar"
                                CausesValidation="False"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">refresh</i>
          <span>Generar</span>
                            </asp:LinkButton>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-10">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ReadOnly="true"
                                        ID="txtFechaCreacion"
                                        runat="server"
                                        class="form-control">
                                    </asp:TextBox>
                                    <label class="form-label">Fecha de Creación</label>
                                </div>

                            </div>
                        </div>
                        <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                            <div class="input-group">
                                <span>
                                    <i class="material-icons">calendar_today</i>
                                </span>
                            </div>
                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-10">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ReadOnly="true"
                                        ID="Fecha_Vencimiento_usuario"
                                        runat="server"
                                        class="form-control">
                                    </asp:TextBox>
                                    <label class="form-label">Fecha de Vencimiento</label>
                                </div>

                            </div>
                        </div>
                        <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                            <div class="input-group">
                                <span>
                                    <i class="material-icons">calendar_today</i>
                                </span>
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

                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-10">
                            <asp:DropDownList
                                ID="cmbEstado" runat="server" DataSourceID="SqlEstado" class="form-control show-tick"
                                DataTextField="descripcion" DataValueField="id_estado" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                        <%--   </div>--%>

                        <%--     <div class="row clearfix">--%>


                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 ">
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
                        <%-- <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttNuevo"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">add</i>
          <span>Nuevo</span>
                            </asp:LinkButton>
                        </div>--%>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttVolver"
                                type="button"
                                ValidationGroup="Validarnousarmas"
                                class="btn bg-teal waves-effect">
                                
                                <i class="material-icons">undo</i>
          <span>Volver</span>
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
