Imports System.Net
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WebForms
'#OBJETO 46
Public Class Reporte_caratula
    Inherits System.Web.UI.Page

    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property

    Private Sub ObtenerDatos(ByRef DataSetdeDatos As DataSet, ByVal DtTabla As String, ByVal Ssql As String)
        Using connection As New MySqlConnection(ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString)
            Dim command As New MySqlCommand(Ssql, connection)
            Dim ReporteAdapter As New MySqlDataAdapter(command)
            ReporteAdapter.Fill(DataSetdeDatos, DtTabla)
        End Using
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                Select Case Request.QueryString("action")
                    Case "print"
                        Session("nombreRPT") = "~/modulos/reportes/rptCaratula.rdlc"
                        Session("nombreDS") = "DSCaratula"
                        Session("nombreDT") = "DtCaratula"
                        Session("xSsql") = "SELECT T001.Id_poliza, CONVERT(T001.fecha_creacion,DATE) fecha_creacion, T002.descripcion, T003.nombrec, T003.rtn_cli,
                                            T004.nombredecla, T004.rtn_agencia, T004.nombre_agencia, T005.Nombre_aduana AS aduana_despacho, T006.descripcion AS regimen,
                                            T007.nombre AS proveedor, T001.domicilio_proveed, T001.Numero_Preimpreso, T008.Nombre AS almacen, T009.Nombre_aduana AS aduna_ingreso_salida,
                                            T010.Nombre_Pais AS pais_origen, T011.Nombre_Pais AS pais_procedencia, T012.nombre_pago as forma_pago, T013.nombre_condicion,
                                            T014.Nombre_aduana AS aduana_tansito , T015.Nombre_modalidad AS modalidad_especial , T016.Nombre AS deposito_aduanas,
                                            T017.Descripción AS Clase_de_bulto , T001.divisa_factura,T001.divisa_flete,T001.divisa_seguro, T001.plazo, T001.manifiesto_entregarap,
                                            T001.contrato_proveedor, T001.entidad_mediacion, T001.ruta_transito, T001.motivo_operacion, T001.Observaciones
                                            FROM DB_Nac_Merca.tbl_01_polizas T001
                                            LEFT JOIN DB_Nac_Merca.tbl_19_estado T002 ON T001.estado_poliza = T002.id_estado
                                            LEFT JOIN DB_Nac_Merca.tbl_04_cliente T003 ON T001.Id_cliente = T003.Id_cliente
                                            LEFT JOIN DB_Nac_Merca.tbl_43_declarante T004 ON T001.declarante = T004.id_declarante
                                            LEFT JOIN DB_Nac_Merca.tbl_06_aduanas T005 ON T001.cod_aduana_sal = T005.Id_Aduana #aduana despacho
                                            LEFT JOIN DB_Nac_Merca.tbl_27_Regimenes T006 ON T001.Id_regimen = T006.Id_Regimen
                                            LEFT JOIN DB_Nac_Merca.tbl_05_proveedores T007 ON T001.Id_proveedor = T007.Id_proveedor
                                            LEFT JOIN DB_Nac_Merca.tbl_9_almacenes T008 ON T001.Id_almacen = T008.Id_almacen
                                            LEFT JOIN DB_Nac_Merca.tbl_06_aduanas T009 ON T001.cod_aduana_ent = T009.Id_Aduana #aduana ingreso salida
                                            LEFT JOIN DB_Nac_Merca.tbl_8_paises T010 ON T001.Cod_pais_org = T010.Id_Pais #pais origen
                                            LEFT JOIN DB_Nac_Merca.tbl_8_paises T011 ON T001.Cod_pais_pro = T011.Id_Pais #pais procede
                                            LEFT JOIN DB_Nac_Merca.tbl_13_forma_pago T012 ON T001.id_pago = T012.id_pago
                                            LEFT JOIN DB_Nac_Merca.tbl_14_condicion_entrega T013 ON T001.id_condicion = T013.id_condicion
                                            LEFT JOIN DB_Nac_Merca.tbl_06_aduanas T014 ON T001.aduana_transdes = T014.Id_Aduana #aduana transi/destino
                                            LEFT JOIN DB_Nac_Merca.tbl_39_modalidad_especial T015 ON T001.modalidad_especial = T015.Id_Modalidad
                                            LEFT JOIN DB_Nac_Merca.tbl_9_almacenes T016 ON T001.deposito_aduanas = T016.Id_almacen
                                            LEFT JOIN DB_Nac_Merca.tbl_18_Clase_deBulto T017 ON T001.Id_Clase_deBulto = T017.Id_Clase_deBulto
                                            WHERE T001.Id_poliza =" & Request.QueryString("idCaratula")


                        Session("nombreDS2") = "DSItems"
                        Session("nombreDT2") = "DtItems"
                        Session("xSsql2") = "SELECT T001.ID_Merca, T001.Id_poliza, T002.Descripcion AS Id_TipoItems ,T001.num_partida,
                                            T001.titulo_currier, T001.matriz_insumos,T001.item_asociado, T001.declaracion_a_cancelar, T001.item_a_cancelar,
                                            T001.pesoneto,T001.pesobruto,T001.bultcant , T003.Descripcion AS Estado_Merc,
                                            T004.Nombre_Pais AS pais_fab,T005.Nombre_Pais AS pais_pro, T006.Nombre_Pais AS pais_adq,
                                            T007.Descripcion as UnidadComercial , T001.Cantidad_Comercial , T008.Descripcion as Unidad_Estadistica,
                                            T001.cantidad_estadistica,T001.importes_factura, T001.importes_otrosgastos ,T001.importes_seguro, T001.importes_flete ,
                                            T001.ajuste_a_incluir, T001.numero_certificado_imp, T001.convenio_perfeccionamiento, T001.exoneracion_aduanera ,
                                            T001.observaciones, T001.comentario, T001.num_partida,  ROW_NUMBER() OVER ( ORDER BY ID_Merca) as num_Item

                                            FROM DB_Nac_Merca.tbl_34_mercancias T001
                                            LEFT JOIN DB_Nac_Merca.tbl_26_Tipo_Items T002 ON T001.Id_Tipo_items = T002.Id_TipoItems
                                            LEFT JOIN DB_Nac_Merca.tbl_25_Estado_Mercancias T003 ON T001.Estado_Merc = T003.Id_Estado
                                            LEFT JOIN DB_Nac_Merca.tbl_8_paises T004 ON T001.cod_pais_fab = T004.Id_Pais #pais FAB
                                            LEFT JOIN DB_Nac_Merca.tbl_8_paises T005 ON T001.cod_pais_pro = T005.Id_Pais #pais PRO
                                            LEFT JOIN DB_Nac_Merca.tbl_8_paises T006 ON T001.cod_pais_adq = T006.Id_Pais #pais ADQ
                                            LEFT JOIN DB_Nac_Merca.tbl_24_Unidad_Medida T007 ON T001.Id_UnidadComercial = T007.Id_UnidadMed
                                            LEFT JOIN DB_Nac_Merca.tbl_24_Unidad_Medida T008 ON T001.Unidad_Estadistica = T008.Id_UnidadMed

                                            WHERE T001.Id_poliza = " & Request.QueryString("idCaratula")

                        Session("nombreDS3") = "DSResumenItems"
                        Session("nombreDT3") = "DtResumenItems"
                        Session("xSsql3") = "SELECT T001.Id_poliza,count(*) canti_items, T002.observaciones,
                                                sum(pesoneto) pesoneto,sum(pesobruto) pesobruto,sum(bultcant) bultcant,
                                                sum(importes_factura) importes_factura,sum(importes_otrosgastos)importes_otrosgastos,
                                                sum(importes_seguro) importes_seguro,sum(importes_flete) importes_flete,
                                                (sum(importes_factura) +sum(importes_otrosgastos)+sum(importes_seguro)+ sum(importes_flete) ) Total
                                                FROM DB_Nac_Merca.tbl_34_mercancias T001
                                                LEFT JOIN ( SELECT *FROM (SELECT Id_poliza, observaciones FROM DB_Nac_Merca.tbl_34_mercancias
                                                where length(observaciones) > 1 and Id_poliza = " & Request.QueryString("idCaratula") & " limit 1) TBL_TEMP
                                                ) T002 ON T001.Id_poliza = T002.Id_poliza
                                                where T001.Id_poliza = " & Request.QueryString("idCaratula")

                End Select

                'Set the processing mode for the ReportViewer to Local  
                ReportViewer1.ProcessingMode = ProcessingMode.Local
                Dim localReport As LocalReport
                localReport = ReportViewer1.LocalReport
                localReport.ReportPath = Server.MapPath(Session("nombreRPT"))

                Dim datasetClientes As New DataSet(Session("nombreDS"))
                'Get the sales order data  
                ObtenerDatos(datasetClientes, Session("nombreDT"), Session("xSsql"))            'Create a report data source for the sales order data  
                Dim dsClientes As New ReportDataSource()
                dsClientes.Name = Session("nombreDS")
                dsClientes.Value = datasetClientes.Tables(Session("nombreDT"))
                localReport.DataSources.Add(dsClientes)


                'Get the sales order data  
                ObtenerDatos(datasetClientes, Session("nombreDT2"), Session("xSsql2"))            'Create a report data source for the sales order data  
                Dim dsClientes2 As New ReportDataSource()
                dsClientes2.Name = Session("nombreDS2")
                dsClientes2.Value = datasetClientes.Tables(Session("nombreDT2"))
                localReport.DataSources.Add(dsClientes2)


                'Get the sales order data  
                ObtenerDatos(datasetClientes, Session("nombreDT3"), Session("xSsql3"))            'Create a report data source for the sales order data  
                Dim dsResumen As New ReportDataSource()
                dsResumen.Name = Session("nombreDS3")
                dsResumen.Value = datasetClientes.Tables(Session("nombreDT3"))
                localReport.DataSources.Add(dsResumen)


                Dim nombreReporte As String = "REPORTE DE DECLARACION ADUANERA"
                'Get the sales order data  
                ObtenerDatos(datasetClientes, "DtEmpresa", "SELECT '" & Application("ParametrosADMIN")(2) & "' as nombre, '" & Application("ParametrosADMIN")(3) & "' as alias, '" & Application("ParametrosADMIN")(22) & "' as logo, '" & nombreReporte & "' as reporte FROM DB_Nac_Merca.tbl_21_parametros LIMIT 1;")            'Create a report data source for the sales order data  
                Dim dsEmpresa As New ReportDataSource()
                dsEmpresa.Name = "DSEmpresa"
                dsEmpresa.Value = datasetClientes.Tables("DtEmpresa")
                localReport.DataSources.Add(dsEmpresa)


                'bitacora de que salio de un form
                If Not IsPostBack Then
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                    End Using
                End If

                'bitacora de que ingreso al form
                Session("IDfrmQueIngresa") = 46
                Session("NombrefrmQueIngresa") = "Reporte de Póliza"
                If Not IsPostBack Then
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                    End Using
                End If


            End If

        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub

End Class