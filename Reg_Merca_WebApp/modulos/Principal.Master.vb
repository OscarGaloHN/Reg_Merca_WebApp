Public Class Principal
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Salir_ServerClick(sender As Object, e As EventArgs)
        Session.Abandon()
        Response.Redirect("../Inicio/login.aspx", False)
    End Sub
End Class