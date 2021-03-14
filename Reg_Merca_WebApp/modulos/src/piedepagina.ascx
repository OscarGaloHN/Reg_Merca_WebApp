<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="piedepagina.ascx.vb" Inherits="Reg_Merca_WebApp.piedepagina" %>
<!-- Footer -->
<div class="legal">
    <div class="copyright">
        &copy; <%: Now.Year %> <a href="javascript:void(0);"><%: Application("ParametrosSYS")(0) %></a>
    </div>
    <div class="version">
        <b>Version: </b><%: Application("ParametrosSYS")(1)  %><br />
         <b class="col-red"><% if Session("user_confirma_correo") = 0 Then %> Correo Electrónico sin confirmar <%End if %> </b>
    </div>
</div>
<!-- #Footer -->
