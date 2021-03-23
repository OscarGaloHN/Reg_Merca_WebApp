Public Class master_mantenimiento
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub lblcerrarSesion_Click(sender As Object, e As EventArgs) Handles lblcerrarSesion.Click
        'registrar bitacora logout
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(2, Session("user_idUsuario"), Session("IDfrmQueIngresa"), Session("NombrefrmQueIngresa"))
        End Using
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx", False)
    End Sub
End Class