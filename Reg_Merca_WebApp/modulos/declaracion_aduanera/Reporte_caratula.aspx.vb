Imports System.Net
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WebForms
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
        If Not Page.IsPostBack Then

            'Set the processing mode for the ReportViewer to Local  
            ReportViewer1.ProcessingMode = ProcessingMode.Local
            Dim localReport As LocalReport
            localReport = ReportViewer1.LocalReport
            localReport.ReportPath = Server.MapPath("~/modulos/reportes/rptPoliza.rdlc")

            Dim datasetClientes As New DataSet(Session("DSPoliza"))
            'Get the sales order data  
            ObtenerDatos(datasetClientes, "DtPoliza", "select Id_poliza, fecha_creacion, Id_cliente, estado_poliza, usuario_creador;")
            Dim dsClientes As New ReportDataSource()
            dsClientes.Name = Session("DSPoliza")
            dsClientes.Value = datasetClientes.Tables(Session("DtPoliza"))
            localReport.DataSources.Add(dsClientes)



            Dim nombreReporte As String = "Reporte de Polizas"
            'Get the sales order data  
            ObtenerDatos(datasetClientes, "DtEmpresa", "SELECT '" & Application("ParametrosADMIN")(2) & "' as nombre, '" & Application("ParametrosADMIN")(3) & "' as alias, '" & Application("ParametrosADMIN")(22) & "' as logo, '" & nombreReporte & "' as reporte FROM DB_Nac_Merca.tbl_21_parametros LIMIT 1;")            'Create a report data source for the sales order data  
            Dim dsEmpresa As New ReportDataSource()
            dsEmpresa.Name = "DSEmpresa"
            dsEmpresa.Value = datasetClientes.Tables("DtEmpresa")
            localReport.DataSources.Add(dsEmpresa)


        End If
    End Sub

End Class