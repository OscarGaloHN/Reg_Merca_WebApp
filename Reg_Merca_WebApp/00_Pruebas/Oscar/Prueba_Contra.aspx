<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Prueba_Contra.aspx.vb" Inherits="Reg_Merca_WebApp.Prueba_Contra" %>

<!DOCTYPE html>
 
<html>
<head runat="server">
    <title>Mostrar u ocultar contraseña</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Cambiar el atributo a texto
                $('#txtPassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Cambiar el atributo a contraseña
                $('#txtPassword').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
             
            <div class="row">
                <div class="col-md-6">
                
                    <div class="input-group">
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-append">
                            <button id="show_password" class="btn btn-primary" type="button">
                                <span class="fa fa-eye-slash icon"></span>
                            </button>
                        </div>
                    </div>
                </div>
                
            </div>
        </div>

    </form>
</body>
</html>