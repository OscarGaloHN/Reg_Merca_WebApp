Public Class menu_principal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        ElseIf Session("user_rol") = 6 Then
            'no tiene rol
            Response.Redirect("~/modulos/confi_rol.aspx")
        Else
            'si tiene acceso al sistema


        End If
    End Sub
End Class