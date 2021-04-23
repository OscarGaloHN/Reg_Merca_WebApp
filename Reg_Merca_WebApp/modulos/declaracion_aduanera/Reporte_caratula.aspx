<%@ Page Title="RegMerca| Reporte Declaración Aduanera" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="Reporte_caratula.aspx.vb" Inherits="Reg_Merca_WebApp.Reporte_caratula" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<asp:Content ID="Content6" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Reporte de Declaración Aduanera</a>
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentMenu" runat="server">
</asp:Content>
<asp:Content ID="Content9" ContentPlaceHolderID="ContentPrincipal" runat="server">

    <div>

        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <rsweb:ReportViewer   SizeToReportContent="True"  ID="ReportViewer1" runat="server" Height="100%" Width="100%">
        </rsweb:ReportViewer>

    </div>

</asp:Content>
<asp:Content ID="Content10" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>

