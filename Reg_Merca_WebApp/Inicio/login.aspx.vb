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

    End Sub

    Private Sub bttEntrar_Click(sender As Object, e As EventArgs) Handles bttEntrar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "' and clave = SHA('" & txtContra.Text & "');"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        Dim registro As DataRow
        registro = DataSetX.Tables(0).Rows(0)
        If Session("NumReg") > 0 Then
            Select Case CInt(registro("intentos"))
                Case 0 'Formulario de registro
                Case 1 'activo
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Bienvenido.', 'success');</script>")
                Case 2 'bloqueado o inactivo
                Case 3 'bloqueo por intentos

            End Select
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
        End If
    End Sub
End Class