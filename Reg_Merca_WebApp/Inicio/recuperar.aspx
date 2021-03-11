<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="recuperar.aspx.vb" Inherits="Reg_Merca_WebApp.recuperar" %>

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
    <script type="text/javascript">
        function xModal(xcolor, xtxtfoco, nombremodal) {
            var color = xcolor;
            var txtfoco = xtxtfoco;
            $('#' + nombremodal + ' .modal-content').removeAttr('class').addClass('modal-content modal-col-' + color);
            $('#' + nombremodal).modal('show');
            $('#' + nombremodal).on('shown.bs.modal', function () {
                $('#' + txtfoco).focus();
            });
        }
    </script>
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
                    <div class="msg">
                        Ingrese la dirección de correo electrónico que utilizó para registrarse. 
                        Le enviaremos un correo electrónico con los pasos para recuerar su contraseña. 
                    </div>
                    <asp:Panel ID="Panel1" runat="server">


                        <div class="row">
                            <div class="col-xs-12">
                                <button onclick="xModal('pink','txtEmail','modalCorreo');" type="button" class="btn btn-block btn-lg bg-pink waves-effect"><i class="material-icons">send</i> <span>SOLICITAR CONTRASEÑA POR CORREO</span>  </button>
                                <%-- <asp:LinkButton autofocus="true" onfocus="myFunctionfoco('txtEmail')" ValidationGroup="CorreoValidar" ID="bttEnviar" runat="server" class="btn btn-block btn-lg bg-pink waves-effect">
                                <i class="material-icons">send</i> <span>SOLICITAR CONTRASEÑA</span>  
                                </asp:LinkButton>--%>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-xs-12">
                            <div class="button-demo js-modal-buttons">
                                <asp:LinkButton OnClientClick="clearTextBox()" ID="bttPreguntas" ValidationGroup="UsuarioValidarNADA" runat="server" class="btn btn-block btn-lg bg-teal waves-effect" data-color="teal" data-txtfoco="txtUsuarioPreguntas">
                                <i class="material-icons">help_outline</i> <span>RESPONDER PREGUNTAS DE SEGURIDAD</span>  
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 align-center">
                            <a href="login.aspx">Iniciar Sesión!</a>
                        </div>
                    </div>

                    <!-- modal recuperar contraseña por correo -->
                    <div class="modal fade" id="modalCorreo" tabindex="-1" role="dialog">
                        <div class="modal-dialog" role="document">
                            <asp:Panel ID="Panel3" runat="server" DefaultButton="bttEnviar">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title" id="lblMOdalCorreo">RECUPERAR CONTRASEÑA</h4>
                                    </div>
                                    <div class="modal-body">
                                        Ingrese su correo electrónico y haga click en el botón 'ENVIAR' para continuar el proceso de recuperacion de contraseña.
                                    <div class="row">
                                        <br />
                                        <div class="col-xs-12">
                                            <div class="row">
                                                <div style="padding-top: 8px;" class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                                                    <div class="input-group form-float">
                                                        <span class="">
                                                            <i class="material-icons">email</i>
                                                        </span>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10">
                                                        <div class="form-group">
                                                            <div class="form-line">
                                                                <asp:TextBox placeholder="Correo Electrónico" AutoComplete="off" ValidationGroup="CorreoValidar" runat="server" type="email" class="form-control" ID="txtEmail"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator runat="server" ID="reqemailvacio" ControlToValidate="txtEmail"
                                                                ErrorMessage="Ingrese su correo electronico."
                                                                Display="Dynamic"
                                                                ForeColor="White" Font-Size="Small" ValidationGroup="CorreoValidar" />
                                                            <asp:RegularExpressionValidator runat="server" ID="reEmailRegistro"
                                                                Display="Dynamic" ForeColor="White" Font-Size="Small"
                                                                ControlToValidate="txtEmail"
                                                                ErrorMessage="El correo electronico no es valido." ValidationGroup="CorreoValidar"
                                                                ValidationExpression="^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:LinkButton runat="server" ID="bttEnviar" ValidationGroup="CorreoValidar" class="btn  btn-link  waves-effect">ENVIAR</asp:LinkButton>
                                        <button type="button" class="btn bg-teal waves-effect" data-dismiss="modal">CERRAR</button>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>







            <!-- modal preguntas seguridad -->
            <div class="modal fade" id="mdModal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="bttContinuar">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="defaultModalLabel">RECUPERAR USUARIO O CONTRASEÑA</h4>
                            </div>
                            <div class="modal-body">
                                Ingrese su nombre de usuario para continuar el proceso de recuperacion de usuario o contraseña por medio de las preguntas 
                                  de seguridad.
                                    <div class="row">
                                        <br />

                                        <div class="col-xs-12">
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:TextBox MaxLength="15" ValidationGroup="UsuarioValidar" AutoComplete="off" runat="server" placeholder="Nombre de usuario" class="form-control" ID="txtUsuarioPreguntas"  onkeypress="soloLetras();" onkeyup="mayus(this);BorrarRepetidas(this);" onfocusout="mayus(this);"  onkeydown="mayus(this);BorrarRepetidas(this);"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUsuarioPreguntas"
                                                    ErrorMessage="Ingrese su nombre de usuario."
                                                    Display="Dynamic"
                                                    ForeColor="White" Font-Size="Small" ValidationGroup="UsuarioValidar" />
                                            </div>
                                        </div>
                                    </div>
                            </div>
                            <div class="modal-footer">
                                <asp:LinkButton runat="server" ID="bttContinuar" ValidationGroup="UsuarioValidar" class="btn  btn-link  waves-effect">CONTINUAR</asp:LinkButton>
                                <button type="button" class="btn bg-pink waves-effect" data-dismiss="modal">CERRAR</button>
                            </div>
                        </div>
                    </asp:Panel>
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

