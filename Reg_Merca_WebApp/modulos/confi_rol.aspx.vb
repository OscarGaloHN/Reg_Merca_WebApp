Public Class confi_rol
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        ElseIf Session("user_rol") <> 6 Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        End If
    End Sub
End Class