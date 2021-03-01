<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="piedepagina.ascx.vb" Inherits="Reg_Merca_WebApp.piedepagina" %>
<!-- Footer -->
<div class="legal">
    <div class="copyright">
        &copy; <%: Now.Year %> <a href="javascript:void(0);"><%: Application("ParametrosSYS")(1) %></a>
    </div>
    <div class="version">
        <b>Version: </b><%: Application("ParametrosSYS")(0)  %>
    </div>
</div>
<!-- #Footer -->
