Imports System.IO

Public Class exportar
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'parametros de configuracion de sistema
        Using Parametros_Sistema As New ControlDB
            Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        End Using

        'PARAMETROS DE ADMINISTRADOR
        Using Parametros_admin As New ControlDB
            Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        End Using

    End Sub

    Private Sub bttFiltrar_Click(sender As Object, e As EventArgs) Handles bttFiltrar.Click

        Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_01_polizas where Id_poliza =114;"

        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            'crear archivo
            Dim registro As DataRow

            Dim xArchivo As String = "{" & Chr(34) & "declaracion" & Chr(34) & ":{" & Chr(34) & "caratula" & Chr(34) & ":{"

            'For i = 0 To Session("NumReg") - 1
            registro = DataSetX.Tables(0).Rows(0)
            'lleanr caratula
            'regimen
            xArchivo = xArchivo & Chr(34) & "regimen" & Chr(34) & ":" & Chr(34) & registro("Id_regimen") & Chr(34) & ","
            'aduana
            xArchivo = xArchivo & Chr(34) & "aduana" & Chr(34) & ":" & Chr(34) & registro("cod_aduana_ent") & Chr(34) & ","
            'modalidad_especial
            xArchivo = xArchivo & Chr(34) & "indModalidadEspecial" & Chr(34) & ":" & Chr(34) & registro("modalidad_especial") & Chr(34) & ","
            'FIN  lleanr caratula


            'fin de archivo SOLO DE CARATULA Y LA declaracion
            xArchivo = xArchivo & "}," & Chr(34) & "usuario_id" & Chr(34) & ":" & Chr(34) & "11623" & Chr(34) & "}"
            'Next
            Dim fs As MemoryStream = New MemoryStream()
            fs = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(xArchivo))

            Response.ContentType = "text/plain"
            Response.AppendHeader("Content-Disposition", "attachment; filename=Exportar.decdat")
            Response.BinaryWrite(fs.ToArray())
            Response.End()

        End If
    End Sub
End Class