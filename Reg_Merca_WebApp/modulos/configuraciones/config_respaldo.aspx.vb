Imports System.IO
Imports System.Security.Cryptography
Imports MySql.Data.MySqlClient

Public Class config_respaldo
    Inherits System.Web.UI.Page
    'objeto #42
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
            'comprobar que el rol del usuario tenga permisos para ingresar
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 42 and permiso_consulta = 1"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'cargar load que pueda ingresar 
                bttModal.Attributes.Add("onClick", "return false;")
                Try
                    If Not IsPostBack Then
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 42
                        Session("NombrefrmQueIngresa") = "Respaldos"
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                        Select Case Request.QueryString("acction")
                            Case "respaldo"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','Respaldo exitoso.', 'success');</script>")

                        End Select
                    End If


                Catch ex As Exception

                End Try
            Else
                'si no tiene permisos 
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 42, "El usuario intenta ingresa a una pantalla sin permisos")
                End Using
                Response.Redirect("~/modulos/acceso_denegado.aspx")
            End If
        End If
    End Sub
    Private Sub bttRespaldo_Click(sender As Object, e As EventArgs) Handles bttRespaldo.Click
        Session("xNombreArchivo") = Format(CDate(Now.Date), "yyyyMMdd") + "_" + Now.ToString("HHmmss") + ".merca"
        Dim conn As MySqlConnection = New MySqlConnection(ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString)
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.Connection = conn
        conn.Open()
        Dim mb As MySqlBackup = New MySqlBackup(cmd)
        Dim fs As MemoryStream = New MemoryStream()

        mb.ExportToMemoryStream(fs)
        conn.Close()

        Dim original As String = Encoding.UTF8.GetString(fs.ToArray())

        Using encriptarck As New ControlCorreo
            original = encriptarck.Encriptar(original)
        End Using

        fs = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(original))

        'original = Encoding.UTF8.GetString(fs.ToArray())
        'Using Desencriptarck As New ControlCorreo
        '    original = Desencriptarck.Desencriptar(original)
        'End Using

        Response.ContentType = "text/plain"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Session("xNombreArchivo"))
        Response.BinaryWrite(fs.ToArray())
        Response.End()
        Response.Redirect("~/modulos/configuraciones/config_respaldo.aspx?acction=respaldo")
    End Sub

    Private Sub bttRestaurar_Click(sender As Object, e As EventArgs) Handles bttRestaurar.Click
        Dim ba As Byte() = FileUpload1.FileBytes
        Dim ms As MemoryStream = New MemoryStream(ba)
        Dim original As String = Encoding.UTF8.GetString(ms.ToArray())
        Using Desencriptarck As New ControlCorreo
            original = Desencriptarck.Desencriptar(original)
        End Using
        ms = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(original))
        Dim conn As MySqlConnection = New MySqlConnection(ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString)
        Dim cmd As MySqlCommand = New MySqlCommand
        cmd.Connection = conn
        conn.Open()
        Dim mb As MySqlBackup = New MySqlBackup(cmd)
        mb.ImportFromMemoryStream(ms)
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx?action=restaurar")
    End Sub
End Class