<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="recuperar.aspx.vb" Inherits="Reg_Merca_WebApp.recuperar" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>Recuperar Contraseña | RegMERCA</title>
    <!-- Favicon-->
    <link rel="icon" href="../../favicon.ico" type="image/x-icon">

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
</head>

<body class="fp-page">
    <div class="fp-box">
        <div class="logo">
            <a href="javascript:void(0);">Reg<b>MERCA</b></a>
            <small>Sistema De Registro De Nacionalización De Mercancias</small>
        </div>
        <div class="card">
            <div class="body">
                <form id="forgot_password" runat="server">
                    <div class="msg">
                        Ingrese la dirección de correo electrónico que utilizó para registrarse. 
                        Le enviaremos un correo electrónico con su nombre de usuario y una contraseña temporal. 
                    </div>
                    <div class="row">
                        <div style="padding-top:10px; padding-left:20px;" class="col-sm-2">
                            <div class="input-group form-float">
                                <span  >
                                    <i class="material-icons">email</i>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-10">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox runat="server" type="email" class="form-control" id="txtEmail"  ></asp:TextBox>
                                    <label class="form-label">Correo Electronico</label>
                                </div>
                                  <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="txtEmail"
                            ErrorMessage="Ingrese su correo electronico."
                            Display="Dynamic"
                            ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                    </div>




                    <asp:LinkButton runat="server" class="btn btn-block btn-lg bg-pink waves-effect" Text="RESTABLECER MI CONTRASEÑA"></asp:LinkButton>

                    <div class="row m-t-20 m-b--5 align-center">
                        <a href="login.aspx">Iniciar Sesión!</a>
                    </div>
                    <div class="row m-t-20 m-b--5 align-center">
                        <a href="#">Responder Preguntas De Seguridad</a>
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
