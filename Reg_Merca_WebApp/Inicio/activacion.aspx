<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="activacion.aspx.vb" Inherits="Reg_Merca_WebApp.activacion" %>

<!DOCTYPE html>
<html>

<head runat="server">
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>RegMERCA |  <%: Page.Title  %></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <link rel="icon" href="../favicon.ico" type="image/x-icon">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css">

    <!-- Bootstrap Core Css -->
    <link href="../plugins/bootstrap/css/bootstrap.css" rel="stylesheet">

    <!-- Waves Effect Css -->
    <link href="../plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Custom Css -->
    <link href="../css/style.css" rel="stylesheet">

    <script src="../plugins/sweetalert/sweetalert-dev.js"></script>
    <link href="../plugins/sweetalert/sweetalert.css" rel="stylesheet" />
    <script src="../plugins/sweetalert/sweetalert.min.js"></script>
    <script src="jsLogin.js"></script>

</head>
<body class="login-page">
    <div class="login-box">
        <div class="logo">
            <a href="javascript:void(0);">Reg<b>MERCA</b></a>
            <small>Sistema De Registro De Nacionalización De Mercancias</small>
        </div>
        <div class="card">
            <div class="body">
                <form runat="server">
                    <asp:Panel ID="PanelCaducada" runat="server" Visible="false">
                        <div class="msg font-bold col-red">
                            <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:LinkButton class="btn btn-block btn-lg bg-pink waves-effect" ID="bttNuevaSolicitud" runat="server" Text="NUEVA SOLICITUD" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelError" runat="server" Visible="false">
                        <div class="msg font-bold col-red">
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelConfirmar" runat="server" DefaultButton="bttContra" Visible="false">
                        <div class="msg font-bold col-teal">
                            <asp:Label ID="lblSaludo" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label>
                            <asp:Literal ID="ltMessage" runat="server" />
                        </div>
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
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
                                                <label class="form-label">Nueva Contraseña</label>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="ValiContra" ControlToValidate="txtContra"
                                                ErrorMessage="Debe de ingresar su nueva contraseña."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
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
                                                <label class="form-label">Confrimar contraseña</label>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="ValidaConfirmar" ControlToValidate="txtContraConfirmar"
                                                ErrorMessage="Debe de confirmar la nueva contraseña."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
                                            <asp:CompareValidator ID="Comparecontra" runat="server" ControlToCompare="txtContra" ControlToValidate="txtContraConfirmar"
                                                ErrorMessage="Las contraseñas no coincide."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
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
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <asp:LinkButton class="btn btn-block btn-lg bg-pink waves-effect" ID="bttContra" runat="server" Text="ESTABLECER CONTRASEÑA" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="row m-t-20 m-b--5 align-center">
                        <asp:LinkButton runat="server" ID="lblcancelar" Text="Iniciar Sesión!" CausesValidation="false"></asp:LinkButton>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Jquery Core Js -->
    <script src="../plugins/jquery/jquery.min.js"></script>

    <!-- Bootstrap Core Js -->
    <script src="../plugins/bootstrap/js/bootstrap.js"></script>

    <!-- Waves Effect Plugin Js -->
    <script src="../plugins/node-waves/waves.js"></script>

    <!-- Validation Plugin Js -->
    <script src="../plugins/jquery-validation/jquery.validate.js"></script>

    <!-- Custom Js -->
    <script src="../js/admin.js"></script>
    <script src="../js/pages/examples/sign-in.js"></script>
</body>
</html>
