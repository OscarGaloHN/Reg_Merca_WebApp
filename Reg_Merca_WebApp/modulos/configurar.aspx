<%@ Page Title="Configuración" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="configurar.aspx.vb" Inherits="Reg_Merca_WebApp.configurar" %>

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
        <li>
            <a href="../../index.html">
                <i class="material-icons">home</i>
                <span>Home</span>
            </a>
        </li>
        <li class="active">
             <a href="../../index.html">
                <i class="material-icons">home</i>
                <span>Configuraciones</span>
            </a>
        </li>

        <li>

            <a href="../../index.html">
                <i class="material-icons">home</i>
                <span>Gestion de usuarios</span>
            </a>
        </li>
    </ul>
    <% If Session("user_usuario") <> "" Then %>
    <% If CBool(Application("ParametrosSYS")(3)) = True Then   %>

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
                    <ul class="header-dropdown m-r--5">
                        <li class="dropdown">
                            <a href="javascript:void(0);" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="true">
                                <i class="material-icons">more_vert</i>
                            </a>
                            <ul class="dropdown-menu pull-right">
                                <li><a href="javascript:void(0);" class=" waves-effect waves-block">Action</a></li>
                                <li><a href="javascript:void(0);" class=" waves-effect waves-block">Another action</a></li>
                                <li><a href="javascript:void(0);" class=" waves-effect waves-block">Something else here</a></li>
                            </ul>
                        </li>
                    </ul>
                </div>
                <div class="body">
                    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Detalles de la empresa</h2>
                    <div class="row clearfix">
                        <div class="col-sm-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmpresa" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Nombre</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtAlias" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Alias</label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtRTN" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">RTN</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmail" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Correo electronico</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtTel" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Telefono</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-sm-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtDireccion" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Dirección</label>
                                </div>
                            </div>
                        </div>

                    </div>

                    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Correo electronico para envio de alertas</h2>
                    <div class="row clearfix">
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtEmailEnvio" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Correo electronico</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtContraseña" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Contraseña</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtPuerto" runat="server" class="form-control"></asp:TextBox>
                                    <label class="form-label">Puerto</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <h2 style="padding-bottom: 8px; font-weight: bold" class="card-inside-title">Inicio de sesión y constraseña</h2>
                    <div class="row clearfix">
                        <div class="col-sm-2">
                            <small>Vigencia de usuarios</small>
                            <div class="input-group spinner" data-trigger="spinner">
                                <div class="form-line">
                                    <input readonly="" runat="server" id="txtCaracteresContra" type="text" class="form-control text-center" value="5" data-rule="quantity" data-max="1095" data-min="60">
                                </div>
                                <span class="input-group-addon">
                                    <a href="javascript:;" class="spin-up" data-spin="up"><i class="glyphicon glyphicon-chevron-up"></i></a>
                                    <a href="javascript:;" class="spin-down" data-spin="down"><i class="glyphicon glyphicon-chevron-down"></i></a>
                                </span>
                            </div>
                        </div>
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
                                    <input type="checkbox" name="CheckBox" runat="server" id="chkRecordar" class="filled-in chk-col-teal " />
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
                            <asp:LinkButton Width="100%" runat="server" ID="BttLimpiar" type="button" class="btn bg-pink waves-effect">
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
