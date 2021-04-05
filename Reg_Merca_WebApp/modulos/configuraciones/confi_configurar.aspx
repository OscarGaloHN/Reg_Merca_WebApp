<%@ Page Title="Configuración" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="confi_configurar.aspx.vb" Inherits="Reg_Merca_WebApp.configurar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Spinner Css -->
    <link href="../plugins/jquery-spinner/css/bootstrap-spinner.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Configuración</a>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentMenu" runat="server">
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
        <li class="active">
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
        <li>
            <a href="config_permisos.aspx">
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
                                    <asp:TextBox onkeypress="return txtEmpresa(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)" onkeyup="borrarespacios(this);" ID="txtEmpresa" runat="server" class="form-control"></asp:TextBox>
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
                                    <asp:TextBox onkeypress="return txtAlias(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)" onkeyup="borrarespacios(this);" ID="txtAlias" runat="server" class="form-control"></asp:TextBox>
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
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" onkeydown="borrarespacios(this)" onkeyup="borrarespacios(this);" ID="txtRTN" runat="server" class="form-control"></asp:TextBox>

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
                                    <asp:TextBox MaxLength="14" onkeypress="SoloNumeros()" onkeydown="borrarespacios(this)" onkeyup="borrarespacios(this);" ID="txttel" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Teléfono </label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txttel"
                                    ErrorMessage="Ingrese el teléfono de la empresa."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator runat="server" ID="retelefono"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txttel"
                                    ErrorMessage="El telefono  no es valido."
                                    ValidationExpression="^\d{4}[-.\s]?\d{4}$" />

                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox onkeypress="return txtDireccion(event)" onkeydown="borrarespacios(this);BorrarRepetidas(this)" onkeyup="borrarespacios(this);" ID="txtDireccion" runat="server" class="form-control"></asp:TextBox>
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
                                    <asp:TextBox onkeypress="return txtADMIN_URL_WEB(event)" onkeydown="borrarespacios(this)" onkeyup="borrarespacios(this);" ID="txtADMIN_URL_WEB" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">URL WEB</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtADMIN_URL_WEB"
                                    ErrorMessage="Ingrese la URL WEB."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
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



