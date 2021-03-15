<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="items.aspx.vb" Inherits="Reg_Merca_WebApp.items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="../plugins/bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Select Css -->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Declaración Aduanera</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <ul class="list">
        <li class="header">MENU PRINCIPAL</li>
        <li>

            <%--vinculo llamar al formuulario--%>
            <a href="../menu_principal.aspx">
                <i class="material-icons">home</i>
                <span>Inicio</span>
            </a>
        </li>
        <li class="active">
            <a href="caratula.aspx">
                <i class="material-icons">aspect_ratio</i>
                <span>Declaracion Aduanera</span>
            </a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPrincipal" runat="server">

    <div class="row clearfix">

        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="card">
                <div class="header">
                    <h2 style="font-weight: bold;">Items    
                        <small>Ingreso de Datos de Los Items</small>
                    </h2>
                </div>
                <div class="body">


                    <div class="row clearfix">
                        <%--                    numero de items--%>


                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtnummitem" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">Numero de Item</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtnummitem"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>



                        <asp:SqlDataSource
                            ID="sqltipoitems"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_TipoItems, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_26_Tipo_Items order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">Tipo de Item</label>
                            <asp:DropDownList
                                ID="ddltipoitem" runat="server" selectlistitem="seleccione" DataSourceID="sqltipoitems" class="form-control show-tick"
                                DataTextField="Descripcion" DataValueField="Id_TipoItems" AppendDataBoundItems="true" ItemType="">
                            </asp:DropDownList>

                        </div>

                         <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtposarancel" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)"  class="form-control" MaxLength="16"></asp:TextBox>
                                    <label class="form-label">Posición Arancelaria</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtposarancel"
                                    ErrorMessage="Ingrese el formato requerido"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>


                    </div>




                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttGuardar"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">list</i>
          <span>Items</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttNuevo"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">note_add</i>
          <span>Documentos</span>
                            </asp:LinkButton>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="LinkButton1"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">collections_bookmark</i>
          <span>Bultos</span>
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
