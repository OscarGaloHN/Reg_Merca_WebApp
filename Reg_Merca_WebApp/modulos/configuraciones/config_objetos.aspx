﻿<%@ Page Title="Permisos Objetos" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/configuraciones/master_config.Master" CodeBehind="config_objetos.aspx.vb" Inherits="Reg_Merca_WebApp.config_objetos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Bootstrap Select Css -->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
    <!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js "></script>
    <script src="https://cdn.datatables.net/plug-ins/1.10.24/dataRender/datetime.js "></script>
    <script src="../src/jsTabla.js"></script>

    <script src="../src/jsModales.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Permisos - Objetos</a>
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

        <li class="active">
            <a href="#">
                <i class="material-icons">vpn_key</i>
                <span>Permisos - Objetos</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />

    <script type="text/javascript">
        tituloImprimir = 'Listado de Permiso en Objetos';
        xColumnas.push(1,2, 3, 4,5,6); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0);
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold">Permisos a los módulos por rol
                        <small>Permite controlar los accesos a los módulos del sistema</small>
                    </h2>

                </div>
                <div class="body">
                    <asp:SqlDataSource
                        ID="SqlRoles"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT 0  AS id_rol, 'Seleccionar Rol...' AS rol UNION ALL  SELECT id_rol, rol FROM DB_Nac_Merca.tbl_15_rol;"></asp:SqlDataSource>
                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                            <p>
                                <b>Seleccione un rol:</b>
                            </p>

                            <asp:DropDownList AutoPostBack="true" ValidationGroup="ValidaRol"
                                ID="ddlRoles" runat="server" DataSourceID="SqlRoles" class="form-control show-tick"
                                DataTextField="rol" DataValueField="id_rol" AppendDataBoundItems="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRoles"
                                ErrorMessage="Selecccione un rol" ForeColor="OrangeRed" Font-Size="X-Small" ValidationGroup="ValidaRol"
                                InitialValue="0" SetFocusOnError="True" />
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 ">
                            <asp:SqlDataSource
                                ID="SqlModulos"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient" ></asp:SqlDataSource>
                            <div class="row clearfix">
                                    <p>
                                        <b>Seleccione un módulo:</b>
                                    </p>

                                    <asp:DropDownList AutoPostBack="true" ValidationGroup="ValidaModulo"
                                        ID="ddlModulos" runat="server" DataSourceID="SqlModulos" class="form-control show-tick"
                                        DataTextField="nombre" DataValueField="id_modulo" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlModulos"
                                        ErrorMessage="Selecccione un módulo" ForeColor="OrangeRed" Font-Size="X-Small" ValidationGroup="ValidaModulo"
                                        InitialValue="0" SetFocusOnError="True" />
                                </div>

                        </div>
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowEdit(this);" type="button" class="btn bg-pink waves-effect"><i class="material-icons">edit</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRowDelete(this);" type="button" data-color="red" class="btn bg-red waves-effect"><i class="material-icons">delete</i></button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="Id_Permisos" HeaderText="ID" />
                                        <asp:BoundField DataField="objeto" HeaderText="Objeto" />
                                        <asp:BoundField DataField="permiso_consulta" HeaderText="Consulta" />
                                        <asp:BoundField DataField="permiso_insercion" HeaderText="Insertar" />
                                        <asp:BoundField DataField="permiso_eliminacion" HeaderText="Eliminar" />
                                        <asp:BoundField DataField="permiso_actualizacion" HeaderText="Actualizar" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
        <!-- Select Plugin Js -->
    <script src="../../plugins/bootstrap-select/js/bootstrap-select.js"></script>

</asp:Content>
