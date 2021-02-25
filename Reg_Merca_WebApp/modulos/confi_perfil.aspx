<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_perfil.aspx.vb" Inherits="Reg_Merca_WebApp.confi_perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Perfil de Usuario</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <!-- Tabs With Icon Title -->
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>TABS WITH ICON TITLE
                    </h2>

                </div>
                <div class="body">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs tab-col-teal" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#profile_with_icon_title" data-toggle="tab">
                                <i class="material-icons">face</i> PERFIL
                            </a>
                        </li>
                        <li role="presentation">
                            <a href="#settings_with_icon_title" data-toggle="tab">
                                <i class="material-icons">settings</i> CONFIGURACIONES
                            </a>
                        </li>
                        <li role="presentation">
                            <a href="#preguntas_with_icon_title" data-toggle="tab">
                                <i class="material-icons">help</i> PREUNTAS DE SEGURIDAD
                            </a>
                        </li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">

                        <div role="tabpanel" class="tab-pane fade in active" id="profile_with_icon_title">
                            <b>Profile Content</b>
                            <p>
                                Lorem ipsum dolor sit amet, ut duo atqui exerci dicunt, ius impedit mediocritatem an. Pri ut tation electram moderatius.
                                        Per te suavitate democritum. Duis nemore probatus ne quo, ad liber essent aliquid
                                        pro. Et eos nusquam accumsan, vide mentitum fabellas ne est, eu munere gubergren
                                        sadipscing mel.
                            </p>
                        </div>
                        <div role="tabpanel" class="tab-pane fade" id="settings_with_icon_title">
                            <b>Message Content</b>
                            <p>
                                Lorem ipsum dolor sit amet, ut duo atqui exerci dicunt, ius impedit mediocritatem an. Pri ut tation electram moderatius.
                                        Per te suavitate democritum. Duis nemore probatus ne quo, ad liber essent aliquid
                                        pro. Et eos nusquam accumsan, vide mentitum fabellas ne est, eu munere gubergren
                                        sadipscing mel.
                            </p>
                        </div>
                        <div role="tabpanel" class="tab-pane fade" id="preguntas_with_icon_title">
                            <b>Settings Content</b>
                            <div class="row clearfix">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:SqlDataSource ID="CustomersSource"
                                        SelectCommand="SELECT * FROM DB_Nac_Merca.tbl_22_preguntas;"
                                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                        ProviderName="MySql.Data.MySqlClient"
                                        runat="server" />
                                    <div class="table-responsive">
                                        <div id="DataTables_Table_0_wrapper" class="dataTables_wrapper form-inline dt-bootstrap">
                                            <asp:GridView ID="CustomersGridView"
                                                DataSourceID="CustomersSource"
                                                AutoGenerateColumns="False"
                                                EmptyDataText="No data available."
                                                AllowPaging="True"
                                                runat="server" DataKeyNames="id_pregunta"
                                              AutoPostback = "Flash"
                                                class="table table-bordered table-striped table-hover js-basic-example dataTable">
                                                <Columns>
                                                    <asp:BoundField DataField="id_pregunta" HeaderText="id_pregunta"
                                                        InsertVisible="False" ReadOnly="True" SortExpression="id_pregunta" />
                                                    <asp:BoundField DataField="pregunta" HeaderText="pregunta"
                                                        SortExpression="pregunta" />
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- #END# Tabs With Icon Title -->
</asp:Content>
