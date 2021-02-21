<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="preguntas.aspx.vb" Inherits="Reg_Merca_WebApp.preguntas" %>


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

    <!-- Bootstrap Select Css -->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />


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
                        Seleccione y responda una de sus preguntas de seguridad para para confirmar que el usuario le pertenece. 
                    </div>
                    <asp:Panel ID="pnlMain" runat="server" DefaultButton="bttverificar">
                        <asp:SqlDataSource
                            ID="SqlPreguntas"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT id_pregunta, UPPER(pregunta) pregunta FROM DB_Nac_Merca.tbl_22_preguntas order by rand() "></asp:SqlDataSource>
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:DropDownList onchange="document.getElementById('txtrespuesta').focus();
                                    document.getElementById('txtrespuesta').value = '';"
                                    ID="cmbPreguntas" runat="server" DataSourceID="SqlPreguntas" class="form-control show-tick"
                                    DataTextField="pregunta" DataValueField="id_pregunta" AppendDataBoundItems="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <div class="form-group form-float">
                                    <div class="form-line">
                                        <asp:TextBox AutoComplete="off" ID="txtrespuesta" runat="server" class="form-control" onkeypress="return isNumberOrLetter(event)" onkeyup="mayus(this);"></asp:TextBox>
                                        <label class="form-label">Respuesta</label>
                                    </div>
                                    <asp:RequiredFieldValidator runat="server" ID="ValiUser" ControlToValidate="txtrespuesta"
                                        ErrorMessage="Debe de ingresar su respuesta."
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:LinkButton onfocus="myFunctionfoco('txtrespuesta')" ID="bttverificar" runat="server" class="btn btn-block btn-lg bg-pink waves-effect">
                                <i class="material-icons">check</i> <span>VERIFICAR RESPUESTA</span>  
                                </asp:LinkButton>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-xs-12 align-center">
                            <asp:LinkButton runat="server" ID="lblcancelar" Text="Iniciar Sesión!" CausesValidation="false"></asp:LinkButton>
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
    <!-- Select Plugin Js -->
    <script src="../plugins/bootstrap-select/js/bootstrap-select.js"></script>

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
