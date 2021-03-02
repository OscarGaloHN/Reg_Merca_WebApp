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
                <div class="row clearfix">
                    <asp:Panel ID="PanelConfi" runat="server" Visible ="false" >
                    <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                        <div class="card">
                            <div class="header bg-teal">
                                <h2> <i class="material-icons">settings</i>&nbsp;Modulo de Configuración <small>Gestion de usurios y configuraciones de sistema. </small></h2>
                            </div>
                            <div class="body">
                                <%--<h6>Configuraciones</h6>--%>
                                <button onclick="location.href='confi_configurar.aspx';" name=">"  type="button" class="btn bg-teal btn-block waves-effect  waves-float">ENTRAR</button>
                                <%--<asp:Button ID="<%: Session("ModulosArray")(i, 1)%>" runat="server" Text="Button" />--%>
                                <%--<dx:ASPxButton ID="" runat="server" Text="ENTRAR" Width="100%" Theme="Material" BackColor="#e4002b" OnClick="bttRuta_Click"></dx:ASPxButton>--%>
                            </div>
                        </div>
                    </div>
                  </asp:Panel>

                </div>
            
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>


