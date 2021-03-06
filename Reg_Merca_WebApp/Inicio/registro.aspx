<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="registro.aspx.vb" Inherits="Reg_Merca_WebApp.registro" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>RegMERCA | Registrarse</title>

    <!--   contraseña-->
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

<body class="signup-page">
    <div class="signup-box">
        <div class="logo">
            <a href="javascript:void(0);">Reg<b>MERCA</b></a>
            <small>Sistema De Registro De Nacionalización De Mercancias</small>
        </div>
        <div class="card">
            <div class="body">
                <form id="sign_up" method="POST" runat="server">
                    <div class="msg">Registrar Nuevo Usuario</div>
                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="input-group form-float">
                                <span class="input-group-addon">
                                    <i class="material-icons">person</i>
                                </span>
                            </div>
                        </div>

                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="150" AutoComplete="off" ID="txtnombre" runat="server" onkeypress="return txNombres(event)" onkeyup="mayusculapalabras(this); borrarespacios(this);" class="form-control"></asp:TextBox>
                                    <label class="form-label">Ingrese su nombre completo</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="Reqnombre" ControlToValidate="txtnombre"
                                    ErrorMessage="Ingrese su nombre completo"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="input-group form-float">
                                <span class="input-group-addon">
                                    <i class="material-icons">assignment_ind</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox MaxLength="15" AutoComplete="off" ID="txtUsuario" runat="server" class="form-control" onkeypress="return isNumberOrLetter(event)" onkeyup="mayus(this);"></asp:TextBox>
                                    <label class="form-label">Ingrese su usuario</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="Requsuario" ControlToValidate="txtusuario"
                                    ErrorMessage="Ingrese un nombre de usuario"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="input-group form-float">
                                <span class="input-group-addon">
                                    <i class="material-icons">email</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                            <div class="form-group form-float">

                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" MaxLength="50" runat="server"   class="form-control" ID="txtemail"></asp:TextBox>
                                    <label class="form-label">Ingrese un correo electrónico</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="reqemailvacio" ControlToValidate="txtEmail"
                                    ErrorMessage="Ingrese su correo electronico."
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator runat="server" ID="reEmailRegistro"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtEmail"
                                    ErrorMessage="El correo electronico no es valido."
                                    ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                            </div>
                        </div>
                    </div>

                <%--    <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="input-group form-float">
                                <span class="input-group-addon">
                                    <i class="material-icons">lock</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtContra" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    <label class="form-label">Ingrese una contraseña</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="Requcontra" ControlToValidate="txtContra"
                                    ErrorMessage="Ingrese una contraseña"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                <asp:RegularExpressionValidator runat="server" ID="Regulcontra"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtContra"
                                    ValidationExpression="^[a-zA-Z0-9'@&#.\s]{5,10}$"
                                    ErrorMessage="El rango de caracteres debe de ser entre (5 - 10)." />
                            </div>
                        </div>
                        <div class="col-xs-1" style="padding-top: 8px;">
                            <i id="show_password" class="material-icons">visibility_off</i>
                        </div>
                    </div>--%>
                <%--   <div class="row">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <div class="input-group form-float">
                                <span class="input-group-addon">
                                    <i class="material-icons">check</i>
                                </span>
                            </div>
                        </div>

                        <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtContraConfirmar" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
                                    <label class="form-label">Ingrese de nuevo la contraseña</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="Requcontraconf" ControlToValidate="txtContra"
                                    ErrorMessage="Ingrese de nuevo la Contraseña"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                <asp:CompareValidator ID="Comparecontra" runat="server" ControlToCompare="txtContra" ControlToValidate="txtContraConfirmar"
                                    ErrorMessage="Su contraseña no coincide"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-xs-1" style="padding-top: 8px;">
                            <i id="show_password2" class="material-icons">visibility_off</i>
                        </div>
                    </div>--%>
                    <asp:LinkButton onfocus="myFunctionfoco('txtnombre')" class="btn btn-block btn-lg bg-pink waves-effect" ID="btt_registrar" runat="server">REGISTRARSE</asp:LinkButton>
                    <div class="m-t-25 m-b--5 align-center">
                        <a class="col-teal" href="login.aspx">¿Ya Cuenta Con Un Usuario?</a>
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
    <script src="../js/pages/examples/sign-up.js"></script>

</body>

</html>