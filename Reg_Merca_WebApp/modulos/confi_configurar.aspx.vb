Public Class configurar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
            If Not IsPostBack Then
                txtEmpresa.Text = Application("ParametrosADMIN")(2)
                txtEmailEnvio.Text = Application("ParametrosADMIN")(9)
                txtPreguntas.Value = Application("ParametrosADMIN")(8)
                chkRecordar.Checked = CBool(Application("ParametrosADMIN")(13))
                chkRegistro.Checked = CBool(Application("ParametrosADMIN")(1))


            End If
        End If
    End Sub

    Private Sub bttLimpiar_Click(sender As Object, e As EventArgs) Handles bttLimpiar.Click
        txtEmpresa.Text = ""
        txtAlias.Text = ""
        txtEmailEnvio.Text = ""
        txtPreguntas.Value = txtPreguntas.Attributes("data-min")





    End Sub
End Class