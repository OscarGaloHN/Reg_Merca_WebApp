<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Reporte_caratula.aspx.vb" Inherits="Reg_Merca_WebApp.Reporte_caratula" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <!-- Jquery Core Js -->
    <script src="../plugins/jquery/jquery.min.js"></script>


    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <title>RegMERCA | <%: Page.Title  %></title>
    <!-- Favicon-->
    <link rel="icon" href="../favicon.ico" type="image/x-icon">
    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,700&subset=latin,cyrillic-ext" rel="stylesheet" type="text/css">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css" />

    <!-- Bootstrap Core Css -->
    <link href="../plugins/bootstrap/css/bootstrap.css" rel="stylesheet" />

    <!-- Waves Effect Css -->
    <link href="../plugins/node-waves/waves.css" rel="stylesheet" />

    <!-- Animation Css -->
    <link href="../plugins/animate-css/animate.css" rel="stylesheet" />

    <!-- Custom Css -->
    <link href="../css/style.css" rel="stylesheet">

    <!-- AdminBSB Themes. You can choose a theme from css/themes instead of get all themes -->
    <link href="../css/themes/all-themes.css" rel="stylesheet" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <%--<meta http-equiv="X-UA-Compatible" content="IE=edge" />--%>
</head>

<body class="theme-teal">
    <!-- Page Loader -->

    <div class="page-loader-wrapper">
        <div class="loader">
            <div class="preloader">
                <div class="spinner-layer pl-teal">
                    <div class="circle-clipper left">
                        <div class="circle"></div>
                    </div>
                    <div class="circle-clipper right">
                        <div class="circle"></div>
                    </div>
                </div>
            </div>
            <p>Espere por favor...</p>
        </div>
    </div>
    <!-- #END# Page Loader -->
    <!-- Overlay For Sidebars -->
    <div class="overlay"></div>
    <!-- #END# Overlay For Sidebars -->

    <!-- Top Bar -->
    <nav class="navbar">
        <div class="container-fluid">
            <div class="navbar-header">
                <a href="javascript:void(0);" class="bars"></a>
                <a class="navbar-brand" href="#">Menú Principal</a>
            </div>
        </div>
    </nav>

    <form id="form2" runat="server">
        <section>
            <!-- Left Sidebar -->
            <aside id="leftsidebar" class="sidebar">
                <!-- User Info -->
                <div class="user-info">
                    <div class="image">
                        <img src="../images/user.png" width="48" height="48" alt="User" />
                    </div>
                    <div class="info-container">
                        <div class="name" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><%: Session("user_nombre_personal") %></div>
                        <div class="email"><%: Session("user_correo") %></div>
                        <div class="btn-group user-helper-dropdown">
                            <i class="material-icons" data-toggle="dropdown">keyboard_arrow_down</i>
                            <ul class="dropdown-menu pull-right">
                                <% If Session("user_estado") = 2 Then %>
                                <li><a id="lblIraPerfil" href="perfil_usuario/confi_perfil.aspx"><i class="material-icons">person</i>Perfil</a></li>
                                <li role="separator" class="divider"></li>
                                <% end if %>
                                <li>
                                    <asp:LinkButton ValidationGroup="none" ID="lblcerrarSesion" runat="server"><i class="material-icons">input</i>Cerrar Sesión</asp:LinkButton></li>
                            </ul>
                        </div>

                    </div>
                </div>
                <!-- #User Info -->
                <!-- Menu -->
                <div class="menu">
                    <%--   <asp:ContentPlaceHolder ID="ContentMenu" runat="server">
                    </asp:ContentPlaceHolder>--%>
                </div>
                <!-- #Menu -->
                <!-- Footer -->

                <%--<uc1:piedepagina runat="server" id="piedepagina" />--%>
                <%-- <div class="legal">
                    <div class="copyright">
                        &copy; <%: Now.Year %> <a href="javascript:void(0);">RegMERCA</a>
                    </div>
                    <div class="version">
                        <b>Version: </b>1.0.0
                    </div>
                </div>--%>
                <!-- #Footer -->
            </aside>
            <!-- #END# Left Sidebar -->
        </section>

        <section class="content">
            <div class="container-fluid">
                <div class="block-header">
                    <div>
                        <%--<asp:Button ID="Button2" runat="server" Text="IMPRIMIR CLIENTES uno" OnClientClick="target ='_blank';" />

                        <asp:Button ID="Button1" runat="server" Text="IMPRIMIR CLIENTES dos" OnClientClick="target ='_blank';" />--%>
                        <asp:ScriptManager runat="server"></asp:ScriptManager>
                        <rsweb:ReportViewer SizeToReportContent="True" ID="ReportViewer1" runat="server" Height="100%" Width="100%">
                        </rsweb:ReportViewer>

                    </div>
                </div>
            </div>
        </section>


        <!-- Bootstrap Core Js -->
        <script src="../plugins/bootstrap/js/bootstrap.js"></script>

        <!-- Slimscroll Plugin Js -->
        <script src="../plugins/jquery-slimscroll/jquery.slimscroll.js"></script>

        <!-- Jquery Spinner Plugin Js -->
        <script src="../plugins/jquery-spinner/js/jquery.spinner.js"></script>

        <!-- Waves Effect Plugin Js -->
        <script src="../plugins/node-waves/waves.js"></script>

        <!-- Custom Js -->
        <script src="../js/admin.js"></script>
        <script src="../js/pages/ui/modals.js"></script>

        <!-- Demo Js -->
        <script src="../js/demo.js"></script>
        <%--<asp:ContentPlaceHolder ID="contenJSpie" runat="server">
        </asp:ContentPlaceHolder>--%>
    </form>

</body>
</html>
