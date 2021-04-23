<%@ Page Title="Exportar" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/exportar/master_exportar.Master" CodeBehind="exportar.aspx.vb" Inherits="Reg_Merca_WebApp.exportar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <a class="navbar-brand" href="#">Exportar Polizas</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">

    <div class="row clearfix">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Registro de bitácora
         
                        <small>A continuación se muestra la actividad de las acciones de los usuarios en el sistema.</small>
                    </h2>
                </div>
                <div class="body">

                    <div class="row clearfix">


                        <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12">
                            <asp:LinkButton Width="100%" runat="server" ID="bttFiltrar" type="button" class="btn bg-pink waves-effect">
                            <i class="material-icons">search</i>
                            <span>Filtrar fechas</span>
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
