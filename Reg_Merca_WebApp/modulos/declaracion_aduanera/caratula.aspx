<%@ Page Title="Carátula" Language="vb" AutoEventWireup="false" MasterPageFile="~/modulos/declaracion_aduanera/master_registros.master" CodeBehind="caratula.aspx.vb" Inherits="Reg_Merca_WebApp.caratula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../plugins/bootstrap-material-datetimepicker/css/bootstrap-material-datetimepicker.css" rel="stylesheet" />
    <link href="../plugins/bootstrap-datepicker/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../plugins/momentjs/moment.js"></script>
    <!-- Bootstrap Select Css -->
    <link href="../plugins/bootstrap-select/css/bootstrap-select.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="encabezado" runat="server">
    <a class="navbar-brand" href="#">Carátula de la Poliza</a>
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
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
    </div>

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

        <div class="card">
            <div class="header">
                <h2 style="font-weight: bold;">Carátula
                        <small>Ingreso de Datos de la Carátula</small>
                </h2>
            </div>
            <div class="body">
                <div class="row clearfix">
                    <div class="col-lg-3 col-md-3 col-sm-10 col-xs-10">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox
                                    ReadOnly="true"
                                    AutoComplete="off"
                                    ID="txtFechaCreacion"
                                    runat="server"
                                    class="form-control">
                                </asp:TextBox>
                                <label class="form-label">Fecha de Creación</label>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-10 col-xs-10">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox
                                    ReadOnly="true"
                                    AutoComplete="off"
                                    ID="txt_numproyec"
                                    runat="server"
                                    class="form-control">
                                </asp:TextBox>
                                <label class="form-label">Número de proyecto</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <asp:SqlDataSource
                        ID="sqlestadopol"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="Select descripcion, id_estado from DB_Nac_Merca.tbl_19_estado where id_estado in (7,8)"></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Estado</label>
                        <asp:DropDownList
                            ID="ddlestado" runat="server" DataSourceID="sqlestadopol" class="form-control show-tick"
                            DataTextField="descripcion" DataValueField="id_estado" AppendDataBoundItems="true" ItemType="">
                        </asp:DropDownList>
                    </div>
                    <asp:SqlDataSource
                        ID="SqlClientes"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_cliente,nombrec FROM DB_Nac_Merca.tbl_04_cliente;"></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Cliente</label>
                        <asp:DropDownList
                            ID="cmbCliente" runat="server" DataSourceID="SqlClientes" class="form-control show-tick"
                            DataTextField="nombrec" DataValueField="Id_cliente" AppendDataBoundItems="true" ItemType="">
                        </asp:DropDownList>
                    </div>
                </div>




                <div class="row clearfix">
                    <!-- declarante -->
                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox AutoComplete="off" ID="txtdeclarante" runat="server" class="form-control" onkeyup="mayus(this);BorrarRepetidas(this);" MaxLength="60" Wrap="True"></asp:TextBox>
                                <label class="form-label">Declarante</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese Declarante"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>

                    <!-- aduana de despacho -->
                    <asp:SqlDataSource
                        ID="Sqladuanadespacho"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Aduana, UPPER(Nombre_aduana) Nombre_aduana FROM DB_Nac_Merca.tbl_06_aduanas order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Aduana de Despacho</label>
                        <asp:DropDownList
                            ID="ddladuanadespacho" AutoComplete="off" runat="server" selectlistitem="seleccione" DataSourceID="Sqladuanadespacho" class="form-control show-tick"
                            DataTextField="Nombre_aduana" DataValueField="Id_Aduana" AppendDataBoundItems="true" ItemType="">
                        </asp:DropDownList>

                    </div>


                    <!-- regimen aduanero -->

                    <asp:SqlDataSource
                        ID="sqlregimenaduanero"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Regimen, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_27_Regimenes order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="form-label">Regimen Aduanero</label>
                        <asp:DropDownList
                            ID="ddlregimenaduanero" runat="server" DataSourceID="sqlregimenaduanero" class="form-control show-tick"
                            DataTextField="Descripcion" DataValueField="Id_Regimen" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>



                </div>

                <!-- RTN IMPORTADOR EXPORTADOR -->
                <div class="row clearfix">
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtrtnimp_exp" AutoComplete="off" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" onkeyup="mayus(this);BorrarRepetidas(this);" class="form-control" MaxLength="14"></asp:TextBox>
                                <label class="form-label">RTN Importador/Exportador</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese RTN Exportador/Importador"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtimp_exp" AutoComplete="off" runat="server" onkeypress="return txNombres(event)" onkeyup="mayus(this);BorrarRepetidas(this);" class="form-control"></asp:TextBox>
                                <label class="form-label">Importador-Exportador</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese Nombre Exportador/Importador"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtRTNagen_aduanera" AutoComplete="off" runat="server" onkeypress="SoloNumeros()" onkeydown="return noespacios(event)" onkeyup="BorrarRepetidas(this)" class="form-control" MaxLength="14"></asp:TextBox>
                                <label class="form-label">RTN Agencia Aduanera</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese RTN Agencia Aduanera"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtagen_aduanera"  AutoComplete="off" runat="server" onkeypress="return txNombres(event)" onkeyup="mayus(this);BorrarRepetidas(this);" class="form-control"></asp:TextBox>
                                <label class="form-label">Agencia Aduanera</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese Nombre Agencia Aduanera"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>
                </div>

                <div class="row clearfix">
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtmanifiestorap" AutoComplete="off" runat="server" onkeydown="return noespacios(event)" onkeyup="mayus(this);BorrarRepetidas(this);" onkeypress="return isNumberOrLetter(event)" class="form-control" MaxLength="17"></asp:TextBox>
                                <label class="form-label">Manifiesto de Entrega Rapida</label>
                            </div>
                            <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtdeclarante"
                                    ErrorMessage="Ingrese Declarante"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtNproveedor" AutoComplete="off" runat="server" onkeypress="return txNombres(event);return noespacios(event);" onkeyup="mayus(this);BorrarRepetidas(this);" class="form-control"></asp:TextBox>
                                <label class="form-label">Nombre Proveedor/Destinatario</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese Nombre Proveedor"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtContra_proveedor" AutoComplete="off" runat="server" onkeydown="return noespacios(event)" onkeyup="mayus(this);BorrarRepetidas(this);" onkeypress="return isNumberOrLetter(event)" class="form-control"></asp:TextBox>
                                <label class="form-label">Contrato Proveedor Destinatario</label>
                            </div>
                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtdeclarante"
                                    ErrorMessage="Ingrese Declarante"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtDomicioProve" AutoComplete="off" runat="server" class="form-control" onkeyup="mayus(this);BorrarRepetidas(this);"></asp:TextBox>
                                <label class="form-label">Domicilio del Proveedor</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="txtdeclarante"
                                ErrorMessage="Domicilio Proveedor"
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
                                <asp:TextBox ID="txtNumPreimp" AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control"></asp:TextBox>
                                <label class="form-label">Número PreImpreso</label>
                            </div>
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtdeclarante"
                                ErrorMessage="Ingrese Numero PreImpreso"
                                Display="Dynamic"
                                ForeColor="OrangeRed" Font-Size="X-Small" />
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtEntidadMed" AutoComplete="off" runat="server" onkeyup="mayus(this)" class="form-control" onkeypress="return isNumberOrLetter(event)"></asp:TextBox>
                                <label class="form-label">Entidad Mediación</label>
                            </div>
                            <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtdeclarante"
                                    ErrorMessage="Ingrese Declarante"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                        </div>
                    </div>

                    <asp:SqlDataSource
                        ID="sqldepositoalmacen"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_almacen, UPPER(Nombre) Nombre FROM DB_Nac_Merca.tbl_9_almacenes order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Deposito de Almacenamiento</label>
                        <asp:DropDownList
                            ID="ddldepositoalmacen" runat="server" DataSourceID="sqldepositoalmacen" class="form-control show-tick"
                            DataTextField="Nombre" DataValueField="Id_almacen" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                    <asp:SqlDataSource
                        ID="sqladuanasalida"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Aduana, UPPER(Nombre_aduana) Nombre_aduana FROM DB_Nac_Merca.tbl_06_aduanas order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Aduana de Ingreso/Salida</label>
                        <asp:DropDownList
                            ID="ddladuanaingsal" runat="server" DataSourceID="sqladuanasalida" class="form-control show-tick"
                            DataTextField="Nombre_aduana" DataValueField="Id_Aduana" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row clearfix">
                    <asp:SqlDataSource
                        ID="sqlpaisdeorigen"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Pais, UPPER(Nombre_pais) Nombre_pais FROM DB_Nac_Merca.tbl_8_paises order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Paises de Origen</label>
                        <asp:DropDownList
                            ID="ddlpaisesdeorigen" runat="server" DataSourceID="sqlpaisdeorigen" class="form-control show-tick"
                            DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
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
                        <label class="form-label">Paises de Procedencia</label>
                        <asp:DropDownList
                            ID="ddlpaisprocedencia" runat="server" DataSourceID="sqlpaisprocedencia" class="form-control show-tick"
                            DataTextField="Nombre_Pais" DataValueField="Id_Pais" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                    <asp:SqlDataSource
                        ID="sqlformadepago"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Pago, UPPER(Nombre_Pago) Nombre_Pago FROM DB_Nac_Merca.tbl_13_forma_pago order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Forma de Pago</label>
                        <asp:DropDownList
                            ID="ddlformadepago" runat="server" DataSourceID="sqlformadepago" class="form-control show-tick"
                            DataTextField="Nombre_Pago" AutoComplete="off" DataValueField="Id_Pago" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                    <asp:SqlDataSource
                        ID="sqlcondicionentrega"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_condicion, UPPER(Nombre_condicion) Nombre_condicion FROM DB_Nac_Merca.tbl_14_condicion_entrega order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Condición de Entrega</label>
                        <asp:DropDownList
                            ID="ddlcondicionentrega" runat="server" DataSourceID="sqlcondicionentrega" class="form-control show-tick"
                            DataTextField="Nombre_condicion" DataValueField="Id_condicion" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                </div>
                <div class="row clearfix">
                    <!-- aDUANA TRANSITO DESTINO     -->
                    <asp:SqlDataSource
                        ID="sqladanatransitodes"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Aduana, UPPER(Nombre_aduana) Nombre_aduana FROM DB_Nac_Merca.tbl_06_aduanas order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Aduana Tránsito de Destino</label>
                        <asp:DropDownList
                            ID="ddladuanatransitodes" runat="server" DataSourceID="sqladanatransitodes" class="form-control show-tick"
                            DataTextField="Nombre_aduana" DataValueField="Id_Aduana" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                    <asp:SqlDataSource
                        ID="sqlmodalidadesp"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Modalidad, UPPER(Nombre_modalidad) Nombre_modalidad FROM DB_Nac_Merca.tbl_39_modalidad_especial order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Modalidad Especial</label>
                        <asp:DropDownList
                            ID="ddlmodalidadesp" runat="server" DataSourceID="sqlmodalidadesp" class="form-control show-tick"
                            DataTextField="Nombre_modalidad" DataValueField="Id_Modalidad" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                    <asp:SqlDataSource
                        ID="sqldepositoaduana"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_almacen, UPPER(Nombre) Nombre FROM DB_Nac_Merca.tbl_9_almacenes order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Deposito de Aduanas</label>
                        <asp:DropDownList
                            ID="ddldepositoaduana" runat="server" DataSourceID="sqldepositoaduana" class="form-control show-tick"
                            DataTextField="Nombre" DataValueField="Id_almacen" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtplazodiasmeses" AutoComplete="off" runat="server" class="form-control"></asp:TextBox>
                                <label class="form-label">Plazo Días-Meses  </label>
                            </div>
                            <%--  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtdeclarante"
                                    ErrorMessage="Ingrese Declarante"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <%-- <asp:SqlDataSource
                            ID="sqlrutadetransito"
                            runat="server"
                            DataSourceMode="DataReader"
                            ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                            ProviderName="MySql.Data.MySqlClient"
                            SelectCommand="SELECT Id_Aduana, UPPER(Nombre_aduana) Nombre_aduana FROM DB_Nac_Merca.tbl_06_aduanas order by rand() "></asp:SqlDataSource>--%>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtrutatransito" AutoComplete="off" runat="server" onkeyup="mayus(this)" class="form-control" onkeypress="return isNumberOrLetter(event)"></asp:TextBox>
                                <label class="form-label">Ruta Tránsito</label>
                            </div>
                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtrutatransito"
                                    ErrorMessage="Ingrese Declarante"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                        </div>
                    </div>
                    <asp:SqlDataSource
                        ID="sqlmotivooper"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_almacen, UPPER(Nombre) Nombre FROM DB_Nac_Merca.tbl_9_almacenes order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                        <label class="form-label">Motivo Operación Suspensiva</label>
                        <asp:DropDownList
                            ID="ddlmotivooper" runat="server" class="form-control show-tick"
                            AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-12 col-md-12 col-sm-6 col-xs-12 ">
                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtobservacion" AutoComplete="off" runat="server" onkeyup="mayus(this)" class="form-control"></asp:TextBox>
                                <label class="form-label">Observaciones</label>
                            </div>
                            <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtdeclarante"
                                    ErrorMessage="Ingrese Declarante"
                                    Display="Dynamic"
                                    ForeColor="OrangeRed" Font-Size="X-Small" />--%>
                        </div>
                    </div>

                </div>
                <%--                    CARACTERISTICAS DE BULTOS--%>

                <div class="row clearfix">

                    <asp:SqlDataSource
                        ID="sqlclasebultos"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Clase_deBulto, UPPER(Descripción) Descripción FROM DB_Nac_Merca.tbl_18_Clase_deBulto order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Clase de bultos</label>
                        <asp:DropDownList
                            ID="ddlclasebultos" runat="server" DataSourceID="sqlclasebultos" class="form-control show-tick"
                            DataTextField="Descripción" DataValueField="Id_Clase_deBulto" AppendDataBoundItems="true">
                        </asp:DropDownList>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtcantbultos" AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control" ReadOnly="True"></asp:TextBox>
                                <label class="form-label">Cantidad de Bultos</label>
                            </div>

                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtpesobrutobul"  AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control" ReadOnly="True"></asp:TextBox>
                                <label class="form-label">Peso Bruto de los Bultos</label>
                            </div>

                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txttotalitems"  AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control" ReadOnly="True"></asp:TextBox>
                                <label class="form-label">Total de Items</label>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row clearfix">
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txttotalfact"  AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control" ReadOnly="True"></asp:TextBox>
                                <label class="form-label">Total Factura</label>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">


                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txttotalotrosgast" AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control"></asp:TextBox>
                                <label class="form-label">Total Otros Gastos</label>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">


                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txtttotalseg"  AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control"></asp:TextBox>
                                <label class="form-label">Total Seguro</label>
                            </div>

                        </div>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">


                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txttotalflet"  AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control"></asp:TextBox>
                                <label class="form-label">Total Flete</label>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="row clearfix">
                    <asp:SqlDataSource
                        ID="sqldivisafact"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Divisas, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_29_Divisas order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Divisa Factura</label>
                        <asp:DropDownList
                            ID="ddldivisafact" runat="server" selectlistitem="seleccione" DataSourceID="sqldivisafact" class="form-control show-tick"
                            DataTextField="Descripcion" DataValueField="Id_Divisas" AppendDataBoundItems="true" ItemType="">
                        </asp:DropDownList>
                    </div>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label"></label>

                        <div class="form-group form-float">
                            <div class="form-line">
                                <asp:TextBox ID="txttipodecambio" AutoComplete="off" runat="server" onkeypress="SoloNumeros()" class="form-control"></asp:TextBox>
                                <label class="form-label">Tipo de Cambio</label>
                            </div>

                        </div>
                    </div>

                    <asp:SqlDataSource
                        ID="sqldivisaseg"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Divisas, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_29_Divisas order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Divisa Seguro</label>
                        <asp:DropDownList
                            ID="ddldivisaseg" runat="server" selectlistitem="seleccione" DataSourceID="sqldivisaseg" class="form-control show-tick"
                            DataTextField="Descripcion" DataValueField="Id_Divisas" AppendDataBoundItems="true" ItemType="">
                        </asp:DropDownList>

                    </div>
                    <asp:SqlDataSource
                        ID="sqldivisaflet"
                        runat="server"
                        DataSourceMode="DataReader"
                        ConnectionString="<%$ ConnectionStrings:Cstr_1 %>"
                        ProviderName="MySql.Data.MySqlClient"
                        SelectCommand="SELECT Id_Divisas, UPPER(Descripcion) Descripcion FROM DB_Nac_Merca.tbl_29_Divisas order by rand() "></asp:SqlDataSource>

                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <label class="form-label">Divisa Flete</label>
                        <asp:DropDownList
                            ID="ddldivisafl" runat="server" DataSourceID="sqldivisaflet" class="form-control show-tick"
                            DataTextField="Descripcion" DataValueField="Id_Divisas" AppendDataBoundItems="true" ItemType="">
                        </asp:DropDownList>

                    </div>
                </div>
                <div class="row clearfix">
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
                </div>

                <div class="row clearfix">
                    <asp:Panel ID="pbotones" runat="server">
                        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttitems"
                                type="button"
                                ValidationGroup="Validarbttitems"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">note_add</i>
          <span>Items</span>
                            </asp:LinkButton>
                        </div>
                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="bttdocumen"
                                type="button"
                                ValidationGroup="Validarbttdocumen"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">note_add</i>
          <span>Documentos</span>
                            </asp:LinkButton>
                        </div>

                        <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                            <asp:LinkButton
                                Width="100%"
                                runat="server"
                                ID="btt_bultos"
                                type="button"
                                ValidationGroup="Validarbttbultos"
                                class="btn bg-teal waves-effect">
          <i class="material-icons">collections_bookmark</i>
          <span>Bultos</span>
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
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
                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="contenJSpie" runat="server">
</asp:Content>
