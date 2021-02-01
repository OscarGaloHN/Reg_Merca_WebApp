<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="Reg_Merca_WebApp.login" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Iniciar Sesión | RegMERCA</title>
    <!--   contraseña-->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="jsLogin.js"></script>

    <!-- fin contraseña-->

    <!-- Favicon-->
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
                    <div class="input-group">
                        <span class="input-group-addon">
                            <i class="material-icons">person</i>
                        </span>
                        <div class="form-line">
                            <asp:TextBox MaxLength="15" AutoComplete="off" ID="txtUsuario" runat="server" class="form-control" placeholder="Usuario"  onkeypress="return isNumberOrLetter(event)" autofocus="true" onkeyup="mayus(this);"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtUsuario"
                            ErrorMessage="Ingrese Su Usuario"
                            Display="Dynamic"
                            ForeColor="OrangeRed" Font-Size="X-Small" />
                    </div>
                    <div class="input-group">
                        <div class="row">
                            <div class="col-xs-1">
                                <span class="input-group-addon">
                                    <i class="material-icons">lock</i>
                                </span>
                            </div>
                            <div class="col-xs-9">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="10" ID="txtContra" runat="server" class="form-control" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtContra"
                                    ErrorMessage="Ingrese su contraseña."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                 <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small" 
                                    ControlToValidate="txtContra"
                                    ValidationExpression="^[\s\S]{5,10}$"
                                    ErrorMessage="El rango de caracteres debe de ser entre (5 - 10)." />
                            </div>
                            <div class="col-xs-1">
                                <i id="show_password" class="material-icons">visibility_off</i>

                                <%-- <div class="input-group-append">
                                    <button id="show_password" class="btn btn-default waves-effect" type="button">
                                     <span class="fa fa-eye-slash icon"></span>
                                    </button>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-8 p-t-5">

                            <input type="checkbox" name="CheckBox" runat="server" id="chkRecordar" class="filled-in chk-col-teal " />
                            <label for="chkRecordar">Recordarme</label>

                        </div>
                        <div class="col-xs-4">
                            <asp:Button ID="bttEntrar" runat="server" Text="ENTRAR" class="btn btn-block bg-pink waves-effect" />
                        </div>
                    </div>
                    <div class="row m-t-15 m-b--20">
                        <div class="col-xs-5">
                            <a href="registro.aspx">Registrarse!</a>
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
