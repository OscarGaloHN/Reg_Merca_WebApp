<%@ Page Title="Preguntas De Seguridad" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="confi_perfil_preguntas.aspx.vb" Inherits="Reg_Merca_WebApp.confi_perfil_preguntas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Perfil de Usuario - Preguntas De Seguridad</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">

    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <% If Session("user_rol") <> 6 Then %>
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
                </div>
            </div>
        </div>
    </div>

</asp:Content>
