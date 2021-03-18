﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="creacion_documentos.aspx.vb" Inherits="Reg_Merca_WebApp.creacion_documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="../../plugins/bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../../plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Select Css -->
    <link href="../../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Creación de Documentos</a>
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
        <li class="active">

            <a href="#">
                <i class="material-icons">create_new_folder</i>
                <span>Creación de Documentos</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Documentos
         
                        <small>El usuario podra visualizar los documentos de su proyecto</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <label class="form-label">Documento:</label>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-11">
                            <asp:SqlDataSource
                                ID="SqlDocumento"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                            <asp:DropDownList
                                ID="cmbDocumento" runat="server" DataSourceID="SqlDocumento" class="form-control show-tick"
                                DataTextField="Documento" DataValueField="id_Rol" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox
                                        ID="txtReferencia"
                                        AutoComplete="off"
                                        runat="server"
                                        onkeypress="txNombres(event);"
                                        onkeydown="borrarespacios(this);BorrarRepetidas(this);"
                                        class="form-control"
                                        onkeyup="mayus(this); borrarespacios(this);">
                                    </asp:TextBox>
                                    <label class="form-label">Referencia</label>
                                </div>
                                <asp:RequiredFieldValidator
                                    runat="server"
                                    ID="Reqnombre"
                                    ControlToValidate="txtReferencia"
                                    ErrorMessage="Ingrese la referencia"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed"
                                    ValidationExpression="^.*(.)\1{!2}.*"
                                    Font-Size="X-Small" />
                            </div>
                        </div>

                        <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
                            <label class="form-label">Presencia:</label>
                        </div>
                        <div class="col-lg-5 col-md-5 col-sm-5 col-xs-11">
                            <asp:SqlDataSource
                                ID="SqlPresencia"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                            <asp:DropDownList
                                ID="Presencia" runat="server" DataSourceID="SqlPresencia" class="form-control show-tick"
                                DataTextField="Presencia" DataValueField="id_Rol" AppendDataBoundItems="true">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttConfirmar_adicion"
                                class="btn bg-pink waves-effect"
                                ValidationGroup="validarResetear">
                               <i class="material-icons">control_point</i> <span>Confirmar Adición</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttCancelar"
                                CausesValidation="False"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">cancel</i>
          <span>Cancelar</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttLimpiar"
                                CausesValidation="False"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">cleaning_services</i>
          <span>Limpiar</span>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
