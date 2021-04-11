<%@ Page Title="Items" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="items.aspx.vb" Inherits="Reg_Merca_WebApp.items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="../plugins/bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../plugins/m   omentjs/moment.js"></script>
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
                        <%--          numero de items--%>


                        <%--  <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
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
                        </div>--%>



                        <asp:SqlDataSource
                            ID="sqltipoitems"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_TipoItems, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_26_Tipo_Items order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <label class="form-label">Tipo de Item</label>
                            <asp:DropDownList
                                ID="ddltipoitem" runat="server" selectlistitem="seleccione" DataSourceID="sqltipoitems" class="form-control show-tick"
                                DataTextField="Descripcion" DataValueField="Id_TipoItems" AppendDataBoundItems="true" ItemType="">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="idvalidarddl"
                                ControlToValidate="ddltipoitem" InitialValue="Seleccione" ErrorMessage="selecciones datos" ForeColor="OrangeRed" Font-Size="X-Small" runat="server" />

                        </div>

                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtposarancel" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="16"></asp:TextBox>
                                    <label class="form-label">Posición Arancelaria</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtposarancel"
                                    ErrorMessage="Ingrese el formato requerido"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txttitulocurri" runat="server" onkeypress="return isNumberOrLetter(event)" onkeydown="return noespacios(event)" onkeyup="mayus(this)" class="form-control" MaxLength="17"></asp:TextBox>
                                    <label class="form-label">Titulo de Manifiesto Currier</label>
                                </div>
                                <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtnummitem"
                                   <%--     ErrorMessage="Ingrese Numero de Item"
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                            </div>
                        </div>


                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtmmatrizinsu" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">ID Matriz Insumos</label>
                                </div>
                                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtnummitem"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtnrroitemasoc" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">Nro.de Item Asociado</label>
                                </div>
                                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtnummitem"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtdeclaracioancancel" runat="server" onkeypress="return isNumberOrLetter(event)" onkeydown="return noespacios(event)" onkeyup="mayus(this)" class="form-control" MaxLength="17"></asp:TextBox>
                                    <label class="form-label">Declaracion a Cancelar</label>
                                </div>
                                <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtnummitem"
                                   <%--     ErrorMessage="Ingrese Numero de Item"
                                        Display="Dynamic"
                                        ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtnmeroitemcancel" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">Nro. de Item a Cancelar</label>
                                </div>
                                <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtnummitem"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                            </div>

                        </div>

                    </div>

                    <div class="row clearfix">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtpesoneto" runat="server" onkeyup="SoloNumeros()" onkeydown="return noespacios(event)" onkeypress="return onKeyDecimal(event,this)" class="form-control" MaxLength="7"></asp:TextBox>
                                    <label class="form-label">Peso Neto</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtpesoneto"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtpesobruto" runat="server" onkeyup="SoloNumeros()" onkeypress="return onKeyDecimal(event,this)" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">Peso Bruto</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtpesobruto"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtcantbltos" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">Cantidad de Bultos</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtcantbltos"
                                    ErrorMessage="Ingrese Numero de Item"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>


                        <asp:SqlDataSource
                            ID="sqlestadomerca"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_Estado, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_25_Estado_Mercancias order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">Estado Mercancia</label>
                            <asp:DropDownList
                                ID="ddlestadomerca" runat="server" selectlistitem="seleccione" DataSourceID="sqlestadomerca" class="form-control show-tick"
                                DataTextField="Descripcion" DataValueField="Id_Estado" AppendDataBoundItems="true" ItemType="">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row clearfix">
                        <asp:SqlDataSource
                            ID="sqlpaisdeorigeni"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_Pais, UPPER(Nombre_pais) Nombre_pais FROM DB_Nac_Merca.tbl_8_paises order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">País de Origen</label>
                            <asp:DropDownList
                                ID="ddlpaisesdeorigeni" runat="server" DataSourceID="sqlpaisdeorigeni" class="form-control show-tick"
                                DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <asp:SqlDataSource
                            ID="sqlpaisprocedencia"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_Pais, UPPER(Nombre_pais) Nombre_pais FROM DB_Nac_Merca.tbl_8_paises order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">País Procedencia/Destino</label>
                            <asp:DropDownList
                                ID="ddlpaisproce" runat="server" DataSourceID="sqlpaisprocedencia" class="form-control show-tick"
                                DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <asp:SqlDataSource
                            ID="sqladquisicion"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_Pais, UPPER(Nombre_pais) Nombre_pais FROM DB_Nac_Merca.tbl_8_paises order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">País de Adquisición</label>
                            <asp:DropDownList
                                ID="ddlpaisadd" runat="server" DataSourceID="sqladquisicion" class="form-control show-tick"
                                DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                        </div>


                        <%--                                                          <asp:SqlDataSource
                            ID="SqlDataSource3"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_Pais, UPPER(Nombre_pais) Nombre_pais FROM DB_Nac_Merca.tbl_8_paises order by rand() "></asp:SqlDataSource>--%>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">Cuota Arancelaria</label>
                            <asp:DropDownList
                                ID="ddlcuotaarancel" runat="server" class="form-control show-tick"
                                AppendDataBoundItems="true">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>




                    <div class="row clearfix">
                        <asp:SqlDataSource
                            ID="SqlDataSource1"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_UnidadMed, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_24_Unidad_Medida order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">Unidad Comercial</label>
                            <asp:DropDownList
                                ID="ddlunidacomer" runat="server" DataSourceID="SqlDataSource1" class="form-control show-tick"
                                DataTextField="Descripcion" DataValueField="Id_UnidadMed" AppendDataBoundItems="true">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtcantidadcomer" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="5"></asp:TextBox>
                                    <label class="form-label">Cantidad Comercial</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtcantidadcomer"
                                    ErrorMessage="Ingrese la Cantidad Comercial"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>
                        <asp:SqlDataSource
                            ID="SqlDataSource2"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_UnidadMed, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_24_Unidad_Medida order by rand() "></asp:SqlDataSource>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label">Unidad Estadistica</label>
                            <asp:DropDownList
                                ID="ddlunidadestadis" runat="server" DataSourceID="SqlDataSource2" class="form-control show-tick"
                                DataTextField="Descripcion" DataValueField="Id_UnidadMed" AppendDataBoundItems="true">
                                <asp:ListItem Value="Seleccione"></asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox ID="txtcantidadestadis" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" class="form-control" MaxLength="7"></asp:TextBox>
                                    <label class="form-label">Cantidad Estadística</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtcantidadestadis"
                                    ErrorMessage="Ingrese Cantidad Estadística"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>


                    </div>


                    <div class="row clearfix">

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtimportefact" runat="server" onkeyup="SoloNumeros()" onkeypress="return onKeyDecimal(event,this)" onkeydown="return noespacios(event)" class="form-control" MaxLength="10"></asp:TextBox>
                                    <label class="form-label">Importe de Factura</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtimportefact"
                                    ErrorMessage="Ingrese Importe de Factura"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtimporteotros" runat="server" onkeyup="SoloNumeros()" onkeypress="return onKeyDecimal(event,this)" onkeydown="return noespacios(event)" class="form-control" MaxLength="10"></asp:TextBox>
                                    <label class="form-label">Importe Otros Gastos</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtimporteotros"
                                    ErrorMessage="Ingrese Imgrese Importe Otros Gastos"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtseguro" runat="server" onkeyup="SoloNumeros()" onkeypress="return onKeyDecimal(event,this)" onkeydown="return noespacios(event)" class="form-control" MaxLength="10"></asp:TextBox>
                                    <label class="form-label">Importe de Seguro</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtseguro"
                                    ErrorMessage="Ingrese Importe de Seguro"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtflete" runat="server" onkeyup="SoloNumeros()" onkeypress="return onKeyDecimal(event,this)" onkeydown="return noespacios(event)" class="form-control" MaxLength="10"></asp:TextBox>
                                    <label class="form-label">Importe Flete</label>
                                </div>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtflete"
                                    ErrorMessage="Ingrese Importe Flete"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />
                            </div>
                        </div>

                    </div>

                    <div class="row clearfix">

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtajuste" runat="server" onkeyup="SoloNumeros()" onkeypress="return onKeyDecimal(event,this)" onkeydown="return noespacios(event)" class="form-control" MaxLength="10"></asp:TextBox>
                                    <label class="form-label">Ajuste a Incluir</label>
                                </div>

                            </div>
                        </div>


                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtnumerocerti" runat="server" onkeypress="return isNumberOrLetter(event)" onkeydown="return noespacios(event)" onkeyup="mayus(this)" class="form-control" MaxLength="17"></asp:TextBox>
                                    <label class="form-label">N. de Certificado Importación</label>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtconvenio" runat="server" onkeypress="return isNumberOrLetter(event)" onkeydown="return noespacios(event)" onkeyup="mayus(this)" class="form-control" MaxLength="17"></asp:TextBox>
                                    <label class="form-label">Convenio Perfeccionamiento</label>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                            <label class="form-label"></label>

                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtexoneracionaduanera" runat="server" onkeypress="return isNumberOrLetter(event)" onkeydown="return noespacios(event)" onkeyup="mayus(this)" class="form-control" MaxLength="17"></asp:TextBox>
                                    <label class="form-label">Exoneración Aduanera</label>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="row clearfix">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 ">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtobservacion" runat="server" onkeydown="return noespacios(event)" onkeyup="mayus(this);BorrarRepetidas(this);" class="form-control"></asp:TextBox>
                                    <label class="form-label">Observaciones</label>
                                </div>

                            </div>
                        </div>



                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 ">
                            <div class="form-group form-float">
                                <div class="form-line">
                                    <asp:TextBox AutoComplete="off" ID="txtcomentario" runat="server" onkeydown="return noespacios(event)" onkeyup="mayus(this);BorrarRepetidas(this);" class="form-control"></asp:TextBox>
                                    <label class="form-label">Comentario</label>
                                </div>

                            </div>
                        </div>

                    </div>


                    <div class="row clearfix">
                        <asp:Panel ID="pbotones" runat="server">
                            <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                <asp:LinkButton ValidationGroup="novalidar"
                                    Width="100%"
                                    runat="server"
                                    ID="bttDocumentos"
                                    type="button"
                                    class="btn bg-teal waves-effect">
              <i class="material-icons">list</i>
              <span>Documentos</span>
                                </asp:LinkButton>
                            </div>

                            <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                                <asp:LinkButton
                                    Width="100%"
                                    runat="server"
                                    ID="bttComplementario"
                                    type="button"
                                    class="btn bg-teal waves-effect">
              <i class="material-icons">collections_bookmark</i>
              <span>Complementario</span>
                                </asp:LinkButton>
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                        <asp:LinkButton
                            Width="100%"
                            runat="server"
                            ID="bttNuevo"
                            type="button"
                            class="btn bg-teal waves-effect">
              <i class="material-icons">note_add</i>
              <span>Ventajas</span>
                        </asp:LinkButton>
                    </div>

                    <div class="row clearfix">

                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="btt_guardar"
                                type="button"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">save</i>
          <span>Guardar</span>
                            </asp:LinkButton>
                        </div>

                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttVolver"
                                type="button"
                                ValidationGroup="Validarbttvolver"
                                class="btn bg-teal waves-effect">
                                <i class="material-icons">undo</i>
                                <span>Volver</span>
                            </asp:LinkButton>
                        </div>

                        <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12">
                        </div>

                    </div>
                </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
