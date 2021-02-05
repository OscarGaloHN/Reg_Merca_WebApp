Public Class login
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
        If IsPostBack = False Then
            'parametros de configuracion de sistema
            Dim Ssql As String = String.Empty
            Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_21_parametros order by 1;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            Dim registro As DataRow
            If Session("NumReg") > 0 Then
                Dim arrayParametros(CInt(Session("NumReg")) - 1) As String
                For i = 0 To arrayParametros.Length - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    arrayParametros(i) = registro("valor")
                Next
                'parametros de contraseña
                Application("Parametros") = arrayParametros
                reContraLogin.ErrorMessage = "El rango de caracteres debe de ser entre (5 -" & Application("Parametros")(0) & ")."
                reContraLogin.ValidationExpression = "^[\s\S]{5," & Application("Parametros")(0) & "}$"
                txtContra.MaxLength = Application("Parametros")(0)
            End If
        End If
    End Sub

    Private Sub bttEntrar_Click(sender As Object, e As EventArgs) Handles bttEntrar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "' and clave = SHA('" & txtContra.Text & "');"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        Dim registro As DataRow
        If Session("NumReg") > 0 Then
            'Si coloco las credenciales correctas
            registro = DataSetX.Tables(0).Rows(0)
            Select Case CInt(registro("estado"))
                Case 0 'Formulario de registro

                Case 1 'activo
                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  fecha_ultima_conexion = CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), intentos=0, en_linea=1 where usuario = BINARY  '" & txtUsuario.Text & "';"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Session("nombreUsuario") = txtUsuario.Text
                    Response.Redirect("~/modulos/principal.aspx")
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Bienvenido.', 'success');</script>")

                Case 2 'bloqueado o inactivo
                Case 3 'bloqueo por intentos
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'error');</script>")
            End Select
        Else
            Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "';"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                registro = DataSetX.Tables(0).Rows(0)
                Select Case CInt(registro("intentos"))
                    Case 3
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'error');</script>")
                    Case Else
                        If CInt(registro("intentos")) + 1 = 3 Then
                            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & ", estado=3 where usuario = BINARY  '" & txtUsuario.Text & "';"
                        Else
                            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & " where usuario = BINARY  '" & txtUsuario.Text & "';"
                        End If
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
                End Select
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
            End If
        End If
    End Sub
End Class