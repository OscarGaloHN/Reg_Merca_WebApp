<%@ Page Title="configuracion avanzada" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="config_avanz.aspx.vb" Inherits="Reg_Merca_WebApp.config_avanz" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Spinner Css -->
    <link href="../plugins/jquery-spinner/css/bootstrap-spinner.css" rel="stylesheet">
</asp:Content>
     
<asp:Content ID="Content7" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">configuracion avanzada</a>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentMenu" runat="server">
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
        <li>
            <a href="confi_configurar.aspx">
                <i class="material-icons">settings</i>
                <span>Configuraciones</span>
            </a>
        </li>

        
         <li class="active">
            <a href="confi_avanz.aspx">
                <i class="material-icons">manage_accounts</i>
                <span>Configuracion Avanzada</span>
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
                    <h2 style="font-weight: bold">Configuraciones Avanzadas 
                        <small>Configuraciones </small>
                    </h2>
    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Correo electronico para envio de alertas</h2>
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmailEnvio" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Correo electrónico</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtEmailEnvio"
                                    ErrorMessage="Ingrese el correo electrónico."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />

                                <asp:RegularExpressionValidator runat="server" ID="RegularEmailEnvio"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtEmailEnvio"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox TextMode="Password" onkeypress="return noespacios(event)" ID="txtContrasena" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Contraseña</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtContrasena"
                                    ErrorMessage="Ingrese la contraseña."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                            <div class="input-group">
                                <span>
                                    <i id="mostrarconfirmar" onmouseout="mouseOut('ContentPrincipal_txtContrasena','mostrarconfirmar')" onmouseover="mouseOver('ContentPrincipal_txtContrasena','mostrarconfirmar')" style="cursor: default" class="material-icons">visibility_off</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox onkeypress="SoloNumeros()" ID="txtPuerto" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Puerto de salida</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtPuerto"
                                    ErrorMessage="Ingrese el puerto de salida."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox onkeypress="return noespacios(event)" ID="TextSMTP" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">SMTP</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtPuerto"
                                    ErrorMessage="Ingrese el SMTP."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                    </div>
                    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Controlador de usuarios</h2>
                    <div class="row clearfix">

                        <div class="col-sm-2">
                            <small>Máximo de usuario</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtmaximousu" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="6">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>

                        </div>
                        <div class="col-sm-2">
                            <small>Mínimo de usuario</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtminimousu" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="4">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>

                        </div>
                        <div class="col-sm-3">
                            <small>Mínimo de caracteres contraseña</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtminimocarac" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="15" data-min="5">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>

                        </div>
                        <div class="col-sm-3">
                            <small>Máximo caracteres contraseña</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtmaximocarat" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="4">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>

                        </div>
                        <div class="col-sm-2">
                            <small>Vigencia de usuarios</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtvigenciausu" type="text" class="form-control text-center" value="5" data-rule="quantity" data-max="1095" data-min="60">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>
                        </div>


                    </div>
                    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Inicio de sesión y constraseña</h2>
                    <div class="row clearfix">

                        <div class="col-sm-2">
                            <small>Intentos</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtIntentos" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="10">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <small>Preguntas</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtPreguntas" type="text" class="form-control text-center" value="3" data-rule="quantity" data-max="10" data-min="3">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>

                        </div>
                        <div class="col-sm-2">
                            <div class="demo-switch-title">Recoradar Usuario</div>
                            <div class="switch">
                                <label>
                                    NO
                                    <input type="checkbox" name="CheckBox" runat="server" id="chkRecordarusu" class="filled-in chk-col-teal " />
                                    <span class="lever switch-col-teal"></span>
                                    SI
                                </label>

                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="demo-switch-title">Formulario de registro</div>
                            <div class="switch">
                                <label>
                                    NO
                                    <input type="checkbox" name="CheckBox" runat="server" id="chkRegistro" class="filled-in chk-col-teal " />
                                    <span class="lever switch-col-teal"></span>
                                    SI
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-sm-3 col-sm-offset-6 col-md-offset-6">
                            <asp:LinkButton ValidationGroup="novalidarconfig" Width="100%" runat="server" ID="bttLimpiar" type="button" class="btn bg-pink waves-effect">
                            <i class="material-icons">refresh</i>
                            <span>Limpiar</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-sm-3">
                            <asp:LinkButton Width="100%" runat="server" ID="bttGuardar" type="button" class="btn bg-teal waves-effect">
                            <i class="material-icons">save</i>
                            <span>Guardar</span>
                             
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
