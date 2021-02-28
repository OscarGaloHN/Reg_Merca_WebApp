Public Class Principal
    Inherits System.Web.UI.MasterPage

    Private Sub lblcerrarSesion_Click(sender As Object, e As EventArgs) Handles lblcerrarSesion.Click
        Session.Abandon()
        Response.Redirect("../Inicio/login.aspx", False)
    End Sub



    'Private Sub lblcerrar_ServerClick(sender As Object, e As EventArgs) Handles lblcerrar.ServerClick
    '  
    'End Sub

    'Protected Sub Salir_ServerClick(sender As Object, e As EventArgs)
    '    Session.Abandon()
    '    Response.Redirect("../Inicio/login.aspx", False)
    'End Sub


End Class