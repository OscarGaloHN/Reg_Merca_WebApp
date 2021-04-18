<%@ Page Title="Copias De Seguridad" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="config_respaldo.aspx.vb" Inherits="Reg_Merca_WebApp.config_respaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../src/jsModales.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Respaldo</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>

        <li>
            <a href="../menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li>
            <a href="confi_configurar.aspx">
                <i class="material-icons">settings</i>
                <span>Configuraciones</span>
            </a>
        </li>
        <li>
            <a href="config_avanz.aspx">
                <i class="material-icons">manage_accounts</i>
                <span>Configuracion Avanzada</span>
            </a>
        </li>
        <li>
            <a href="config_permisos.aspx">
                <i class="material-icons">vpn_key</i>
                <span>Permisos - Módulos</span>
            </a>
        </li>
        <li>
            <a href="config_objetos.aspx">
                <i class="material-icons">vpn_key</i>
                <span>Permisos - Objetos</span>
            </a>
        </li>
        <li class="active">
            <a href="#">
                <i class="material-icons">save</i>
                <span>Copias De Seguridad</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold">Respaldar Información
                        <small>Permite crear copias de seguridad del sistema.</small>
                    </h2>

                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-sm-3">
                            <asp:LinkButton Width="100%" ValidationGroup="novalidarrestaurar" runat="server" ID="bttRespaldo" type="button" class="btn bg-teal waves-effect">
                            <i class="material-icons">save</i>
                            <span>RESPALDO</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold">Restaurar Información
                        <small>Permite restaurar copias de seguridad del sistema.</small>
                    </h2>

                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-sm-3">
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                            <asp:RequiredFieldValidator ControlToValidate="FileUpload1"
                                runat="server" ErrorMessage="Debe de seleccionar el archivo de respaldo."
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.merca)$"
                                ControlToValidate="FileUpload1" runat="server" ErrorMessage="Formato de archivo no valido."
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                            <br />
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 ">
                            <asp:LinkButton ID="bttModal"   OnClientClick="xModal('teal','','modalRestaurar');" Width="100%" runat="server" type="button" class="btn bg-pink waves-effect">
                                <i class="material-icons">settings_backup_restore</i>
                                <span>RESTAURAR</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- modal eliminar aduana-->
    <div class="modal fade" id="modalRestaurar" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <!-- TITULO -->
                    <h4 class="modal-title" id="LblDelete">RESTAURAR SISTEMA</h4>
                </div>
                <div class="modal-body">
                    Esta acción remplazara los datos del sistema por los que contiene el archivo de respaldo,
                   los datos ingresados luego de la creación del respaldo se perderan
                   <br />
                   <br />
                    <b>¿Seguro que dese restaurar los datos datos del archivo de respaldo?</b>

                    <br />
                    <br />
                    <asp:RequiredFieldValidator ControlToValidate="FileUpload1"
                        runat="server" ErrorMessage="Debe de seleccionar el archivo de respaldo."
                        Display="Dynamic"
                        ForeColor="White" Font-Size="Small" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.merca)$"
                        ControlToValidate="FileUpload1" runat="server" ErrorMessage="Formato de archivo no valido."
                        Display="Dynamic"
                        ForeColor="White" Font-Size="Small" />

                </div>
                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="bttRestaurar" class="btn  btn-link  waves-effect">SI, RESTAURAR</asp:LinkButton>
                    <button type="button" class="btn  btn-link waves-effect" data-dismiss="modal">CERRAR</button>
                </div>
            </div>

        </div>
    </div>



</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
