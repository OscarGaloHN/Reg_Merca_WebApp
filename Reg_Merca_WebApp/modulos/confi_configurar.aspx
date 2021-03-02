<%@ Page Title="Configuración" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_configurar.aspx.vb" Inherits="Reg_Merca_WebApp.configurar" %>


<asp:Content ID="Content4" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Configuración</a>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Spinner Css -->
    <link href="../plugins/jquery-spinner/css/bootstrap-spinner.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <% If Session("user_idUsuario") <> Nothing Then %>
        <% If CBool(Application("ParametrosSYS")(2)) = True Then   %>
        <li>
            <a href="menu_principal">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li class="active">
            <a href="#">
                <i class="material-icons">settings</i>
                <span>Configuraciones</span>
            </a>
        </li>

        <li>
         
            <a href="Config_Usuarios.aspx">
                <i class="material-icons">manage_accounts</i>
                <span>Gestion de usuarios</span>
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

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold">Configuraciones 
                        <small>Informacion basica de la empresa y otras configuraciones</small>
                    </h2>

                </div>
                <div class="body">
                    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Detalles de la empresa</h2>
                    <div class="row clearfix">
                        <div class="col-sm-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox onkeyup="borrarespacios(this);" ID="txtEmpresa" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Nombre de la empresa</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmpresa"
                                    ErrorMessage="Ingrese el nombre de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox onkeyup="borrarespacios(this);" ID="txtAlias" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Alias de la empresa</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtAlias"
                                    ErrorMessage="Ingrese el alias de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>

                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" ID="txtRTN" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">RTN</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtRTN"
                                    ErrorMessage="Ingrese el RTN de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Correo electrónico</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtEmail"
                                    ErrorMessage="Ingrese el Correo electrónico de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator runat="server" ID="reEmailRegistro"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtEmail"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtTel" MaxLength="8" onkeypress="SoloNumeros()" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Teléfono </label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txttel"
                                    ErrorMessage="Ingrese el teléfono de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtDireccion" onkeyup="borrarespacios(this);" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Dirección</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtDireccion"
                                    ErrorMessage="Ingrese la dirección de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox onkeypress="return noespacios(event)" ID="TxtADMIN_URL_WEB" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">URL WEB</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtADMIN_URL_WEB"
                                    ErrorMessage="Ingrese la URL WEB."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                    </div>

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
                                    <input readonly="" runat="server" id="txtmaximousu" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="1">
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
                                    <input readonly="" runat="server" id="txtminimousu" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="1">
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
                                    <input readonly="" runat="server" id="txtminimocarac" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="1">
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
                                    <input readonly="" runat="server" id="txtmaximocarat" type="text" class="form-control text-center" value="1" data-rule="quantity" data-max="30" data-min="1">
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
