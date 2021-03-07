Public Class Principal
    Inherits System.Web.UI.MasterPage

    Private Sub lblcerrarSesion_Click(sender As Object, e As EventArgs) Handles lblcerrarSesion.Click
        'registrar bitacora logout
        Using log_bitacora As New ControlBitacora
            log_bitacora.log_sesion_inicio(2, Session("user_idUsuario"), "menu principal")
        End Using
        Session.Abandon()
        Response.Redirect("../Inicio/login.aspx", False)
    End Sub
End Class