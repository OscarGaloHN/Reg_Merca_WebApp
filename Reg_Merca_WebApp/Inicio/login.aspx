<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Reg_Merca_WebApp.login" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>RegMERCA | Iniciar Sesión</title>
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
                    <div class="msg">Iniciar Sesión</div>
                    <div class="row">
                        <div style="padding-top: 8px;" class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="input-group">
                                <span class="input-group-addon"><i class="material-icons">person</i></span>
                            </div>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtUsuario" runat="server" class="form-control" onkeypress="return isNumberOrLetter(event)" onkeyup="mayus(this);"></asp:TextBox>
                                    <label class="form-label">Usuario</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="ValiUser" ControlToValidate="txtUsuario"
                                    ErrorMessage="Debe de ingresar  su usuario."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                    <asp:RegularExpressionValidator runat="server" ID="valiUserLargo"
                                        Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                        ControlToValidate="txtUsuario" />
                            </div>
                        </div>
                    </div>

                    <asp:Panel ID="Panel1" runat="server" DefaultButton="bttEntrar">
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
                            <div class="col-xs-8 p-t-5">
                                <input type="checkbox" name="CheckBox" runat="server" id="chkRecordar" class="filled-in chk-col-teal " />
                                <label for="chkRecordar">Recordarme</label>
                            </div>
                            <div class="col-xs-4">
                                <asp:LinkButton   autofocus="true" onfocus="myFunctionfoco('txtUsuario')" ID="bttEntrar" runat="server" Text="ENTRAR" class="btn btn-block bg-pink waves-effect" />
                            </div>

                        </div>
                    </asp:Panel>
                    <div class="row m-t-15 m-b--20">
                        <div class="col-xs-5">
                            <%If CBool(Application("ParametrosADMIN")(0)) = True Then  %>
                            <a href="registro.aspx">Registrarse!</a>
                            <%End If %>
                        </div>
                        <div class="col-xs-7 align-right">
                            <a href="recuperar.aspx">¿Olvidó Su Contraseña?</a>
                        </div>
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
