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

        Catch ex As Exception

        End Try
    End Sub

End Class