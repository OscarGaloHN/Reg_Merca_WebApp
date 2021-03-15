Public Class master_registros
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub lblcerrarSesion_Click(sender As Object, e As EventArgs) Handles lblcerrarSesion.Click
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx", False)
    End Sub
End Class

