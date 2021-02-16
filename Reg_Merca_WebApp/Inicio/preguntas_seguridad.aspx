<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="preguntas_seguridad.aspx.vb" Inherits="Reg_Merca_WebApp.preguntas_seguridad" %>


<!DOCTYPE html>
<html>

<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>RegMERCA | Recuperar Contraseña</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="jsLogin.js"></script>

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
                    <div class="msg font-bold">
                        Responda las siguientes preguntas para desbloquear su usuario o cambiar su contraseña. 
                    </div>
                    <asp:Panel ID="pnlMain" runat="server">
                       
                    </asp:Panel>
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:LinkButton ID="bttverificar" runat="server" class="btn btn-block btn-lg bg-pink waves-effect">
                                <i class="material-icons">check</i> <span>VERIFICAR RESPUESTAS</span>  
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 align-center">
                            <a href="login.aspx">Iniciar Sesión!</a>
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
    <script src="../js/pages/ui/modals.js"></script>
</body>

</html>

