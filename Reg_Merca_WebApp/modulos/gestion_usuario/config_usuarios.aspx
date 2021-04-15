﻿<%@ Page Title="Gestion de Usuarios" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/gestion_usuario/master_gestion_usuarios.Master" CodeBehind="config_usuarios.aspx.vb" Inherits="Reg_Merca_WebApp.Config_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
    <!-- JQuery DataTable Css -->
    <link href="../../plugins/jquery-datatable/skin/bootstrap/css/dataTables.bootstrap.css" rel="stylesheet">
    <!-- Jquery DataTable Plugin Js -->
    <script src="../../plugins/jquery-datatable/jquery.dataTables.js"></script>
    <script src="../../plugins/jquery-datatable/skin/bootstrap/js/dataTables.bootstrap.js"></script>

     <script src="https://cdn.datatables.net/buttons/1.7.0/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.7.0/js/buttons.html5.min.js "></script>

    <script src="../src/jsTabla.js"></script>
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
        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;
            document.getElementById('ContentPrincipal_lblUsuario').innerHTML = row.cells[3].innerHTML;
            document.getElementById('ContentPrincipal_lblHidden1').value = row.cells[2].innerHTML;
            document.getElementById('ContentPrincipal_lblHidden2').value = row.cells[3].innerHTML;
            //document.getElementById('ContentPrincipal_lblHidden3').value = row.cells[3].innerHTML;
            //document.getElementById('ContentPrincipal_CmbHiddenField1').value = row.cells[2].innerHTML;
            if (row.cells[2].innerHTML == 2) {
                swal('Configuración de usuarios', 'El usuario ADMIN no se puede eliminar.', 'error');
            } else {
                xModal('teal', 'ContentPrincipal_txtRespuestaEditar');
            }
            return false;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Configuración de Usuarios</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
         <% If Session("user_idUsuario") <> Nothing Then %>
        <% If CBool(Application("ParametrosSYS")(2)) = True Then   %>
        <li>
            <a href="../menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
    
        <li class="active">
            <a href="#">
               <i class="material-icons">manage_accounts</i>
                <span>Gestion de usuarios</span>
            </a>
        </li>
        <%ELSE %>
                <li class="active">
            <a href="#">
               <i class="material-icons">manage_accounts</i>
                <span>Gestion de usuarios</span>
            </a>
        </li>
    </ul>
        <% End if  %>



    <% End if  %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">

     <asp:HiddenField ID="HiddenLogo" runat="server" />
    <asp:HiddenField ID="HiddenEmpresa" runat="server" />


    <script type="text/javascript">
        tituloImprimir = 'Listado de Usuarios'
        xColumnas.push(2, 3, 4, 5); /*AGREGAR ELEMENTOS AL FINAL DE UN ARRAY*/
        xMargenes.push(100, 0, 100, 0)
        xlogo = document.getElementById('ContentPrincipal_HiddenLogo').value;
        xempresa = document.getElementById('ContentPrincipal_HiddenEmpresa').value;
    </script>

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Configuración de usuarios
         
                        <small>Llenado de Datos de usuarios</small>
                    </h2>
                </div>
                <div class="body">
                    <div class="row clearfix">
                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttNuevo"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">add</i>
          <span>Nuevo</span>
                            </asp:LinkButton>
                        </div>
                        <%--<div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 ">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttEditar"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">edit</i>
          <span>Editar</span>
                            </asp:LinkButton>
                        </div>--%>
             
                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" class="table table-bordered table-striped table-hover display compact"
                                    Width="100%">
                                    <Columns>
                                        <asp:BoundField  HeaderText="Editar" DataField="id_usuario" HtmlEncode="False" DataFormatString="<a class='btn bg-pink waves-effect' href='config_gestion_usuario.aspx?xuser={0}&action=update&ignore=92​​'><i class='material-icons'>edit</i> </a>" />
                                        <%--<asp:BoundField DataField="id_usuario" HtmlEncode="False" DataFormatString="<a class='btn bg-red waves-effect' href='confi_perfil.aspx?code={​​0}​​'><i class='material-icons'>edit</i> </a>" />--%>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <button onclick="return GetSelectedRow(this);" type="button" data-color="red"  class="btn bg-deep-red waves-effect"><i class="material-icons">delete</i></button>
                                           
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="id_usuario" HeaderText="Usuario" />
                                        <%--<asp:BoundField DataField="usuario" HeaderText="Nombre de Usuario" />--%>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="rol" HeaderText="Rol" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Estado" />

                                    </Columns>

                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="modal fade" id="mdModal" tabindex="-1" role="dialog">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">

                            <asp:Panel ID="PanelEditor" runat="server" DefaultButton="bttEliminar">
                                <div class="modal-header">
                                    <h4 class="modal-title">Eliminar Usuarios</h4>
                                </div>
                                <div class="modal-body"> 
                                        <h2 class="modal-title">
                                           <b>¿Está seguro que desea eliminar el registro?</b> </h2> 
                                    <br /> <b>El usuario pertenece a:</b>  <asp:Label ID="lblUsuario" class="msg" runat="server" Text="..."></asp:Label> <br /><br /> Si desea eliminar este registro de usuario, haga clic en el botón eliminar. <br>  Si desea cancelar haga clic en el boton cerrar.
                                    <br />
                                    <div class="row clearfix">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <asp:HiddenField ID="lblHidden1" runat="server" />
                                            <asp:HiddenField ID="lblHidden2" runat="server" />
                                            <asp:HiddenField ID="lblHidden3" runat="server" />
                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton runat="server" ID="bttEliminar" ValidationGroup="actualizarRespuesta" class="btn  btn-link  waves-effect">Eliminar</asp:LinkButton>
                                    <button type="button" class="btn bg-pink waves-effect" data-dismiss="modal">CERRAR</button>
                                    </div>
                            </asp:Panel>

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>