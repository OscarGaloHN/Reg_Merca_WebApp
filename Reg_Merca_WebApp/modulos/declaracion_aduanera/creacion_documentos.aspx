<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="creacion_documentos.aspx.vb" Inherits="Reg_Merca_WebApp.creacion_documentos" %>

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
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <label class="form-label">Documento</label>
                            <asp:SqlDataSource
                                ID="SqlId_Documento"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"
                                SelectCommand="SELECT Id_Documento, UPPER(descripcion) descripcion FROM DB_Nac_Merca.tbl_28_Documentos"></asp:SqlDataSource>
                            <asp:DropDownList
                                ID="cmbDocumento" runat="server" selectlistitem="seleccione" DataSourceID="SqlId_Documento" class="form-control show-tick"
                                DataTextField="descripcion" DataValueField="Id_Documento" AppendDataBoundItems="true" ItemType="">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row clearfix">
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

                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12"> 
                            <div class="demo-switch-title">Presencia</div>
                            <div class="switch">
                                <label>
                                    NO
                                    <input type="checkbox" name="CheckBox" runat="server" id="chkRecordarusu" class="filled-in chk-col-teal " />
                                    <span class="lever switch-col-teal"></span>
                                    SI
                                </label>

                            </div>
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
          <i class="material-icons">refresh</i>
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
