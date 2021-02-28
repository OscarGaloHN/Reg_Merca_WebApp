<%@ Page Title="Preguntas De Seguridad" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_perfil_preguntas.aspx.vb" Inherits="Reg_Merca_WebApp.confi_perfil_preguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- JQuery DataTable Css -->
    <link href="../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">

    <!-- Jquery DataTable Plugin Js -->
    <script src="../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>

    <script type="text/javascript">
        $(function () {
            $('[id*=gvCustomers]').prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "sPaginationType": "full_numbers",
                "language": {
                    "lengthMenu": "Mostrar  _MENU_ registros",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Ultimo",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    },
                    "info": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                    "infoEmpty": "Mostrando del 0 al 0 de 0 registros",
                    "zeroRecords": "No se encontraron registros coincidentes.",
                    "emptyTable": "No hay datos disponibles.",
                    "stateSave": true
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Perfil de Usuario - Preguntas De Seguridad</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">

    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <% If Session("user_estado") <> 1 Then %>
        <li>
            <a href="#">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">contact_page</i>
                <span>Datos Generales</span>
            </a>
        </li>
        <li>
            <a href="#">
                <i class="material-icons">password</i>
                <span>Cambio De Contraseña</span>
            </a>
        </li>
        <li class="active">
            <a href="#">
                <i class="material-icons">help</i>
                <span>Preguntas De Seguridad</span>
            </a>
        </li>
        <%else %>
        <li class="active">
            <a href="#">
                <i class="material-icons">help</i>
                <span>Preguntas De Seguridad</span>
            </a>
        </li>
        <%End if %>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2>Configuración Y Selección De Preguntas
                                <small>No comparta sus respuestas con danie, ya que estas preguntas le ayudaran a desbloquear su cuenta o cambiar contraseña.</small>
                    </h2>

                </div>
                <div class="body">

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <asp:Label ID="Label1" runat="server" Text="Se han configurado: "></asp:Label>
                            <asp:Label class="font-bold col-teal" ID="lblprguntas" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text=" de "></asp:Label>
                            <asp:Label class="font-bold col-teal" ID="lblpreguntasrequeridas" runat="server" Text=""></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text=" preguntas de seguridad requeridas."></asp:Label><br />
                        </div>
                    </div>
                    <asp:Panel ID="PanelPregunta" runat="server" Visible="false">
                        <div class="button-demo js-modal-buttons">
                            <button type="button" data-color="teal" class="btn bg-teal waves-effect">NUEVA PREGUNTA</button>
                        </div>
                    </asp:Panel>
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="pregunta" HeaderText="Preguntas" />
                                        <asp:BoundField DataField="respuesta" HeaderText="Respuestas" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>

                <!-- For Material Design Colors -->
                <div class="modal fade" id="mdModal" tabindex="-1" role="dialog">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="defaultModalLabel">Preguntas de Seguridad</h4>
                            </div>
                            <div class="modal-body">
                                Seleccione y responda una pregunta.
                            <asp:SqlDataSource
                                ID="SqlPreguntas"
                                runat="server"
                                DataSourceMode="DataReader"
                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>
                                <div class="row clearfix">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <asp:DropDownList onchange="document.getElementById('ContentPrincipal_txtrespuesta').focus();
                                    document.getElementById('ContentPrincipal_txtrespuesta').value = '';"
                                            ID="cmbPreguntas" runat="server" DataSourceID="SqlPreguntas" class="form-control show-tick"
                                            DataTextField="pregunta" DataValueField="id_pregunta" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <div class="form-line">
                                                <asp:TextBox placeholder="Respuesta" AutoComplete="off" ID="txtrespuesta" runat="server" class="form-control" onkeypress="return isNumberOrLetter(event)" onkeyup="mayus(this);"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator runat="server" ID="ValiUser" ControlToValidate="txtrespuesta"
                                                ErrorMessage="Debe de ingresar su respuesta."
                                                Display="Dynamic"
                                                ForeColor="OrangeRed" Font-Size="X-Small" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-link waves-effect">SAVE CHANGES</button>
                                <button type="button" class="btn btn-link waves-effect" data-dismiss="modal">CLOSE</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
