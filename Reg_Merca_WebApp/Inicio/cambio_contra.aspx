﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="cambio_contra.aspx.vb" Inherits="Reg_Merca_WebApp.cambio_contra" %>


<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Recuperar Contraseña</title>
    <!-- Favicon-->
    <link rel="icon" href="../favicon.ico" type="image/x-icon">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="../../plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="../../plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../../plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Custom Css -->
    <link href="../../css/style.css" rel="stylesheet">
        <script src="../plugins/sweetalert/sweetalert-dev.js"></script>
    <link href="../plugins/sweetalert/sweetalert.css" rel="stylesheet" />
    <script src="../plugins/sweetalert/sweetalert.min.js"></script>

    <script src="jsLogin.js"></script>
</head>

<body class="fp-page">
    <div class="fp-box">
        <div class="logo">
            <a href="javascript:void(0);">Reg<b>MERCA</b></a>
            <small>Sistema De Registro De Nacionalización De Mercancias</small>
        </div>
        <div class="card">
            <div class="body">
                <form id="forgot_password" method="POST" runat="server">
                    <div class="msg font-bold">
                        Recuperación De Usuario
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:Button CausesValidation="false" runat="server" ID="bttDesbloquear" class="btn btn-block btn-lg bg-teal waves-effect" Text="DESBLOQUEAR USUARIO"></asp:Button>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <a class="btn btn-block btn-lg bg-pink waves-effect" role="button" data-toggle="collapse" href="#collapseExample" aria-expanded="false"
                                aria-controls="collapseExample">CAMBIAR CONTRASEÑA
                            </a>
                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="collapse" id="collapseExample">
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
                                                    <asp:TextBox ID="txtContra" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                                    <label class="form-label">Contraseña</label>
                                                </div>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtContra"
                                                    ErrorMessage="Ingrese su contraseña."
                                                    Display="Dynamic"
                                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                                <asp:RegularExpressionValidator runat="server" ID="reContraLogin"
                                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                                    ControlToValidate="txtContra" />
                                            </div>

                                        </div>
                                        <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                            <div class="input-group">
                                                <span>
                                                    <i id="show_password" style="cursor: default" class="material-icons">visibility_off</i>
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
                                                    <label class="form-label">Contraseña</label>
                                                </div>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtContra"
                                                    ErrorMessage="Ingrese su contraseña."
                                                    Display="Dynamic"
                                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                                <asp:RegularExpressionValidator runat="server" ID="reContraLogin2"
                                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                                    ControlToValidate="txtContra" />
                                            </div>

                                        </div>
                                        <div style="padding-top: 10px; padding-right: 40px;" class="col-xs-1">
                                            <div class="input-group">
                                                <span>
                                                    <i id="show_password2" style="cursor: default" class="material-icons">visibility_off</i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-8 p-t-5">
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:LinkButton ID="bttCambiar" runat="server" Text="CAMBIAR" class="btn btn-block bg-pink waves-effect" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row m-t-20 m-b--5 align-center">
                        <asp:LinkButton runat="server" ID="lblcancelar" Text="Iniciar Sesión!" CausesValidation="false"></asp:LinkButton>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Jquery Core Js -->
    <script src="../../plugins/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="../../plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="../../plugins/node-waves/waves.js"></script>

    <!-- Validation Plugin Js -->
    <script src="../../plugins/jquery-validation/jquery.validate.js"></script>

    <!-- Custom Js -->
    <script src="../../js/admin.js"></script>
    <script src="../../js/pages/examples/forgot-password.js"></script>
</body>

</html>
