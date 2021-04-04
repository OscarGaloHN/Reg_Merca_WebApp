Imports System.Net
Imports System.IO
Imports MySql.Data.MySqlClient
Imports Microsoft.Reporting.WebForms

Public Class WebForm1
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property

    Private Function GetData() As DSClientes

        Dim constr As String = ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString
        Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand("SELECT Id_cliente,  nombrec, ciudad, telefono, Id_pais FROM DB_Nac_Merca.tbl_04_cliente;")
                Using sda As New MySqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    Using dsCustomers As New DSClientes()
                        sda.Fill(dsCustomers, "DtClientes")
                        Return dsCustomers
                    End Using
                End Using
            End Using
        End Using
    End Function
    Private Sub ObtenerDatos(ByRef DataSetdeDatos As DataSet, ByVal DtTabla As String, ByVal Ssql As String)
        Using connection As New MySqlConnection(ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString)
            Dim command As New MySqlCommand(Ssql, connection)
            Dim ReporteAdapter As New MySqlDataAdapter(command)
            ReporteAdapter.Fill(DataSetdeDatos, DtTabla)
        End Using
    End Sub
    Private Sub WebForm1_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then




            'Set the processing mode for the ReportViewer to Local  
            ReportViewer1.ProcessingMode = ProcessingMode.Local
            Dim localReport As LocalReport
            localReport = ReportViewer1.LocalReport
            localReport.ReportPath = Server.MapPath("~/modulos/reportes/rptClientes.rdlc")

            Dim datasetClientes As New DataSet("DSClientes")
            'Get the sales order data  
            ObtenerDatos(datasetClientes, "DtClientes", "SELECT Id_cliente,  nombrec, ciudad, telefono, Id_pais FROM DB_Nac_Merca.tbl_04_cliente where ciudad='tegus';")            'Create a report data source for the sales order data  
            Dim dsClientes As New ReportDataSource()
            dsClientes.Name = "DSClientes"
            dsClientes.Value = datasetClientes.Tables("DtClientes")
            localReport.DataSources.Add(dsClientes)

            Dim nombreReporte As String = "Reporte de Clientes"
            'Get the sales order data  
            ObtenerDatos(datasetClientes, "DtEmpresa", "SELECT '" & Application("ParametrosADMIN")(2) & "' as nombre, '" & Application("ParametrosADMIN")(3) & "' as alias, '" & Application("ParametrosADMIN")(22) & "' as logo, '" & nombreReporte & "' as reporte FROM DB_Nac_Merca.tbl_21_parametros LIMIT 1;")            'Create a report data source for the sales order data  
            Dim dsEmpresa As New ReportDataSource()
            dsEmpresa.Name = "DSEmpresa"
            dsEmpresa.Value = datasetClientes.Tables("DtEmpresa")
            localReport.DataSources.Add(dsEmpresa)


        End If
    End Sub


End Class