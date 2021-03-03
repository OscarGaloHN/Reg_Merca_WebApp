<%@ Page Title="Inicio" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/Principal.Master" CodeBehind="menu_principal.aspx.vb" Inherits="Reg_Merca_WebApp.menu_principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Menú Principal</a>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>

    </ul>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">

 
        <%Try %>
        <div class="row clearfix">
            <% For i = 0 To Session("TotalModulos") - 1 %>
            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                <div class="card">
                    <div class="header bg-teal">
                        <h2><i class="material-icons"><%:Session("arrayModulos")(i, 4) %></i>&nbsp;<%: Session("arrayModulos")(i, 1) %> <small><%: Session("arrayModulos")(i, 2) %></small></h2>
                    </div>
                    <div class="body">
                        <h6><%:Session("arrayModulos")(i, 3) %></h6>
                        <button onclick="location.href='<%:Session("arrayModulos")(i, 5) %>';" name="<%:Session("arrayModulos")(i, 1) %>" type="button" class="btn bg-teal btn-block waves-effect  waves-float">ENTRAR</button>
                    </div>
                </div>
            </div>
            <% Next %>
        </div>
        <%Catch ex As Exception %>

        <%end try %>



        <%--<asp:Button ID="<%: Session("ModulosArray")(i, 1)%>" runat="server" Text="Button" />--%>
        <%--<dx:ASPxButton ID="" runat="server" Text="ENTRAR" Width="100%" Theme="Material" BackColor="#e4002b" OnClick="bttRuta_Click"></dx:ASPxButton>--%>
   


</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>


