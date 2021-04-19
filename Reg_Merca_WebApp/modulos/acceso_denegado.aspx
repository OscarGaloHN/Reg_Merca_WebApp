<%@ Page Title="Acceso Denegado" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="acceso_denegado.aspx.vb" Inherits="Reg_Merca_WebApp.acceso_denegado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Acceso Denegado</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 align-center">
            <span class="material-icons" style="font-size: 90px;">person_off</span>

            <div class="font-40">
                Acceso Denegado
                <br />
                El administrador
                <br />
                no ha definido el acceso para este usuario.
            </div>
        </div>
    </div>
               <br />
    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 align-center">
           <a style="text-decoration:none;" class="btn bg-pink waves-effect" href="menu_principal.aspx"">
                <i class="material-icons">arrow_back </i>
                <span>Ir al menu</span>         
           </a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
