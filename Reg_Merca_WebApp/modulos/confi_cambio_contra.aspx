<%@ Page Title="Cambio de Contraseña" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_cambio_contra.aspx.vb" Inherits="Reg_Merca_WebApp.confi_cambio_contra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Cambio De Contraseña</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <li>
            <a href="menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li>
            <a href="confi_perfil.aspx">
                <i class="material-icons">contact_page</i>
                <span>Datos Generales</span>
            </a>
        </li>
        <li>
        <li class="active">
            <a href="#">
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
                    <h2 style="font-weight: bold;">Cambio de Contraseña
                        <small>mensajee pendiente</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="wellh">
                                <div class="row">
                                    <div style="padding-top: 8px;" class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="material-icons">lock</i></span>
                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtContraactual" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                <label class="form-label">Contraseña Actual</label>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="ValiContra" ControlToValidate="txtContraactual"
                                                ErrorMessage="Debe de ingresar su contraseña actual."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
                                        </div>
                                    </div>
                                    <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                        <div class="input-group">
                                            <span>
                                                <i id="mostraractual" onmouseover="mouseOver('ContentPrincipal_txtContraactual','mostraractual')" onmouseout="mouseOut('ContentPrincipal_txtContraactual','mostraractual')" style="cursor: default" class="material-icons">visibility_off</i>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div style="padding-top: 8px;" class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="material-icons">lock</i></span>
                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtContra" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                <label class="form-label">Contraseña Nueva</label>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtContra"
                                                ErrorMessage="Debe de ingresar su nueva contraseña."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtContra"
                                                ErrorMessage="La contraseña no puede ser igual a la anterior."
                                                Display="Dynamic" ControlToCompare ="txtContraactual"
                                                ForeColor="OrangeRed" Font-Size="X-Small" Operator="NotEqual">
   
                                            </asp:CompareValidator>
                                            <asp:RegularExpressionValidator runat="server" ID="reContra"
                                                Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                                ControlToValidate="txtContra" />
                                        </div>
                                    </div>
                                    <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                        <div class="input-group">
                                            <span>
                                                <i id="mostrarnueva" onmouseout="mouseOut('ContentPrincipal_txtContra','mostrarnueva')" onmouseover="mouseOver('ContentPrincipal_txtContra','mostrarnueva')" style="cursor: default" class="material-icons">visibility_off</i>
                                            </span>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div style="padding-top: 8px;" class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="material-icons">check</i></span>
                                        </div>
                                    </div>
                                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                                        <div class="form-group form-float">
                                            <div class="form-line">
                                                <asp:TextBox ID="txtContraConfirmar" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                <label class="form-label">Confirmar contraseña</label>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="ValidaConfirmar" ControlToValidate="txtContraConfirmar"
                                                ErrorMessage="Debe de confirmar la nueva contraseña."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
                                            <asp:CompareValidator ID="Comparecontra" runat="server" ControlToCompare="txtContra" ControlToValidate="txtContraConfirmar"
                                                ErrorMessage="Las contraseñas no coincide."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
                                             <asp:RegularExpressionValidator runat="server" ID="reContraConfirmar"
                                                Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                                ControlToValidate="txtContraConfirmar" />
                                                </div>
                                        </div>
                                    </div>
                                    <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                        <div class="input-group">
                                            <span>
                                                <i id="mostrarconfirmar" onmouseout="mouseOut('ContentPrincipal_txtContraConfirmar','mostrarconfirmar')" onmouseover="mouseOver('ContentPrincipal_txtContraConfirmar','mostrarconfirmar')" style="cursor: default" class="material-icons">visibility_off</i>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton
                                            onfocus="myFunctionfoco('ContentPrincipal_txtContra')"
                                            Width="100%"
                                            runat="server"
                                            ID="bttCambiar"
                                            class="btn btn-block btn-lg bg-pink waves-effect">
                                                      <i class="material-icons">refresh</i>
                                                       <span>CAMBIAR</span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
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
