Imports System.IO
'OBJETO #47
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
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            'REDIRECCIONAR A MENU PRINCIPAL
            Response.Redirect("~/Inicio/login.aspx")
        Else
            'si hay una sesion activa
            'comprobar que el rol del usuario tenga permisos para editar
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 47 and permiso_consulta = 1"

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'si tiene los permisos
                If Request.QueryString("action") = "exportar" Then
                    'bitacora de que salio de un form
                    If Not IsPostBack Then
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                        End Using
                    End If
                    'bitacora de que ingreso al form
                    Session("IDfrmQueIngresa") = 47
                    Session("NombrefrmQueIngresa") = "Exportar Archivo"
                    If Not IsPostBack Then
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                        End Using
                    End If
                    exportarArchivo(Request.QueryString("xIdCaratual"))
                Else
                    'REDIRECCIONAR A MENU PRINCIPAL
                    Response.Redirect("~/modulos/menu_principal.aspx")
                End If
            Else
                'si no tiene permisos 
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 47, "El usuario intenta ingresa a una pantalla sin permisos")
                End Using
                Response.Redirect("~/modulos/acceso_denegado.aspx")
            End If
        End If
    End Sub
    Private Sub exportarArchivo(ByVal xIdCaratual As String)
        Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_01_polizas where Id_poliza =" & xIdCaratual & ";"

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
            xArchivo = xArchivo & "}," & Chr(34) & "documentos" & Chr(34) & ":["

            ''''''## inicio documentos caratula
            Ssql = "SELECT T001.*,  T002.Descripcion ,CASE WHEN T001.presencia = 1 THEN 'S' ELSE 'N' END AS presencia_SARAWEB FROM DB_Nac_Merca.tbl_28_Documentos T001
                    LEFT JOIN  DB_Nac_Merca.tbl_32_Cod_Documentos T002 ON T001.Id_Documento = T002.Id_Documento 
                    Where id_poliza_doc =" & xIdCaratual & ";"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using

            If Session("NumReg") > 0 Then
                For i = 0 To Session("NumReg") - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    xArchivo = xArchivo & "{" & Chr(34) & "codDocumento" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & " - " & registro("Descripcion") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "referencia" & Chr(34) & ":" & Chr(34) & registro("Referencia") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "presencia" & Chr(34) & ":" & Chr(34) & registro("presencia_SARAWEB") & Chr(34) & "}"
                    If Session("NumReg") - 1 = i Then
                    Else
                        xArchivo = xArchivo & ","
                    End If
                Next
            Else


            End If
            xArchivo = xArchivo & "]"
            ''''''## fin documentos caratula






            'fin de archivo SOLO DE CARATULA Y LA declaracion
            xArchivo = xArchivo & Chr(34) & "usuario_id" & Chr(34) & ":" & Chr(34) & "11623" & Chr(34) & "}"
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