<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="registro.aspx.vb" Inherits="Reg_Merca_WebApp.registro" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>RegMERCA | Registrarse</title>

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
                                    <asp:TextBox MaxLength="150" AutoComplete="off" ID="txtnombre" runat="server" onkeypress="txNombres(event);"  onkeydown="mayus(this);borrarespacios(this);"   onkeyup="mayus(this); borrarespacios(this);" class="form-control"></asp:TextBox>
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
                                    <asp:TextBox AutoComplete="off" ID="txtUsuario" runat="server" class="form-control" onkeypress="soloLetras();" onkeyup="mayus(this);BorrarRepetidas(this);" onfocusout="mayus(this);"  onkeydown="mayus(this);BorrarRepetidas(this);"></asp:TextBox>
                                    <label class="form-label">Ingrese su usuario</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="Requsuario" ControlToValidate="txtusuario"
                                    ErrorMessage="Ingrese un nombre de usuario"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                  <asp:RegularExpressionValidator runat="server" ID="valiUserLargo"
                                    Display="Dynamic" ForeColor="OrangeRed" Font-Size="X-Small"
                                    ControlToValidate="txtUsuario" />
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