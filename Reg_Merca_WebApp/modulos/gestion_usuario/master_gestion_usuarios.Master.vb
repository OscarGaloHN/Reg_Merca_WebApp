Public Class gestion_usuarios
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub lblcerrarSesion_Click(sender As Object, e As EventArgs) Handles lblcerrarSesion.Click
        'registrar bitacora config_usuariout
        Using cusuario_bitacora As New ControlBitacora
            cusuario_bitacora.acciones_Comunes(2, Session("user_idUsuario"), 7, "gestion de usuarios")
        End Using
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx", False)

    End Sub

End Class