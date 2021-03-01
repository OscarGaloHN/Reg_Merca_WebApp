<%@ Page Title="Preguntas De Seguridad" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_perfil_preguntas.aspx.vb" Inherits="Reg_Merca_WebApp.confi_perfil_preguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- JQuery DataTable Css -->
    <link href="../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">

    <!-- Jquery DataTable Plugin Js -->
    <script src="../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>

    <script type="text/javascript">
        function xModal(xcolor, xtxtfoco) {
            var color = xcolor;
            var txtfoco = xtxtfoco;
            $('#mdModal .modal-content').removeAttr('class').addClass('modal-content modal-col-' + color);
            $('#mdModal').modal('show');
            $('#mdModal').on('shown.bs.modal', function () {
                $('#' + txtfoco).focus();
            });
        }



        function ShowNuevo() {
            document.getElementById('ContentPrincipal_panelNew').style.display = 'block'; // to show
            document.getElementById('ContentPrincipal_PanelEditor').style.display = 'none'; // to hide
            document.getElementById('ContentPrincipal_txtrespuesta').value = '';

        }

        function ShowEditor() {
            document.getElementById('ContentPrincipal_panelNew').style.display = 'none'; // to show
            document.getElementById('ContentPrincipal_PanelEditor').style.display = 'block'; // to hide
            //document.getElementById('ContentPrincipal_txtRespuestaEditar').value = ''; 
        }

        function GetSelectedRow(lnk) {
            //Reference the GridView Row.
            var row = lnk.parentNode.parentNode;

            //Determine the Row Index.
            //var message = "Row Index: " + (row.rowIndex - 1);

            ////Read values from Cells.
            //message += "\nId: " + row.cells[1].innerHTML;
            //message += "\nPregunta: " + row.cells[2].innerHTML;
            //message += "\nRespuesta: " + row.cells[3].innerHTML;

            ShowEditor();
            document.getElementById('ContentPrincipal_lblIDPregunta').innerHTML = row.cells[1].innerHTML;
            document.getElementById('ContentPrincipal_lblHidden1').value = row.cells[1].innerHTML;
            document.getElementById('ContentPrincipal_txtRespuestaEditar').value = row.cells[3].innerHTML;

            document.getElementById('ContentPrincipal_CmbHiddenField1').value = row.cells[2].innerHTML;

            

            var textToFind = row.cells[2].innerHTML;

            var dd = document.getElementById('ContentPrincipal_cmbNuevaPregunta');
            for (var i = 0; i < dd.options.length; i++) {
                if (dd.options[i].text === textToFind) {
                    dd.selectedIndex = i;
                    break;
                }
            }


            xModal('pink', 'ContentPrincipal_txtRespuestaEditar');

            //alert(message);
            return false;
        }
            </script>
    <script src="src/jsTabla.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Perfil de Usuario - Preguntas De Seguridad</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
   
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <% If Session("user_estado") = 2 Then %>
        <li>
            <a href="menu_principal.aspx">
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
                            <button onclick="ShowNuevo()" type="button" data-color="teal" data-txtfoco="ContentPrincipal_txtrespuesta" class="btn bg-teal waves-effect">NUEVA PREGUNTA</button>
                        </div>

                    </asp:Panel>

                    <br />
                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Editor">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRow(this);" type="button" data-color="red" class="btn bg-pink waves-effect">Editar</button>
                                            </ItemTemplate>
                                        </asp:TemplateField>

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
                            <asp:Panel ID="panelNew" runat="server" DefaultButton="bttGuardarPregunta">
                                <div class="modal-header">
                                    <h4 class="modal-title">Preguntas de Seguridad</h4>
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
                                                    <asp:TextBox onkeyup="mayus(this);" placeholder="Respuesta" AutoComplete="off" ID="txtrespuesta" runat="server" class="form-control"></asp:TextBox>
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
                                    <asp:LinkButton runat="server" ID="bttGuardarPregunta" ValidationGroup="UsuarioValidar" class="btn  btn-link  waves-effect">GUARDAR</asp:LinkButton>
                                    <button type="button" class="btn bg-pink waves-effect" data-dismiss="modal">CERRAR</button>
                                </div>
                            </asp:Panel>


                            <!-- editor de respuestas -->
                            <asp:Panel ID="PanelEditor" runat="server" DefaultButton="bttActualizar">
                                <div class="modal-header">
                                    <h4 class="modal-title">Preguntas de Seguridad</h4>
                                </div>
                                <div class="modal-body">
                                    Para guardar los cambios haga click en "Actulizar" al terminar su edición.<br />
                                    <small>ID de respuesta actual: 
                                        <asp:HiddenField ID="lblHidden1" runat="server" />    
                                        <asp:Label ID="lblIDPregunta" class="msg" runat="server" Text="..."></asp:Label></small>


                                    <br />

                                    <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                            <asp:SqlDataSource
                                                ID="SqlEditaPregunta"
                                                runat="server"
                                                DataSourceMode="DataReader"
                                                ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                                                ProviderName="MySql.Data.MySqlClient"></asp:SqlDataSource>

                                        <asp:HiddenField ID="CmbHiddenField1" runat="server" />    

                                            <asp:DropDownList onchange="document.getElementById('ContentPrincipal_txtRespuestaEditar').focus();
                                    document.getElementById('ContentPrincipal_txtRespuestaEditar').value = '';"
                                                ID="cmbNuevaPregunta" runat="server" DataSourceID="SqlEditaPregunta" class="form-control show-tick"
                                                DataTextField="pregunta" DataValueField="id_pregunta" AppendDataBoundItems="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <br />

                                    <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="form-group">
                                                <div class="form-line">
                                                    <asp:TextBox onkeyup="mayus(this);" placeholder="Respuesta" AutoComplete="off" ID="txtRespuestaEditar" runat="server" class="form-control"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtRespuestaEditar"
                                                    ErrorMessage="Debe de ingresar su respuesta."
                                                    Display="Dynamic"
                                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton runat="server" ID="bttActualizar" ValidationGroup="actualizarRespuesta" class="btn  btn-link  waves-effect">Actualizar</asp:LinkButton>
                                    <button type="button" class="btn bg-teal waves-effect" data-dismiss="modal">CERRAR</button>
                                </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>



            </div>
        </div>
    </div>

</asp:Content>
