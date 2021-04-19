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
            ObtenerDatos(datasetClientes, "DtPoliza", "select a.Id_poliza, a.estado_poliza, a.cod_aduana_ent,  a.cod_aduana_sal, a.Id_regimen, a.nombre_agenciaadu,
 a.Numero_Preimpreso, a.id_condicion,  a.Id_Clase_deBulto,  a.fecha_creacion, a.divisa_seguro,
b.nombrec, c.Descripcion, d.nombre, e.nombredecla, f.nombre, g.Nombre_Pais

from tbl_01_polizas a, tbl_04_cliente b, tbl_32_Cod_Documentos c, tbl_02_usuarios d, tbl_43_declarante e, 
     tbl_05_proveedores f, tbl_8_paises g, tbl_28_Documentos dc

where a.Id_cliente = b.Id_cliente
and dc.id_poliza_doc = a.Id_poliza
and dc.Id_Documento = c.Id_Documento
and a.ultimo_editor = d.id_usuario
and a.Id_proveedor = f.Id_proveedor
and a.Cod_pais_org = g.Id_Pais
and a.declarante = e.id_declarante;")
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