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
                txtIntentos.Value = Application("ParametrosADMIN")(7)
                txtAlias.Text = Application("ParametrosADMIN")(3)
                txtRTN.Text = Application("ParametrosADMIN")(6)
                txtEmail.Text = Application("ParametrosADMIN")(4)
                txtTel.Text = Application("ParametrosADMIN")(5)
                txtDireccion.Text = Application("ParametrosADMIN")(14)
                TxtADMIN_URL_WEB.Text = Application("ParametrosADMIN")(19)
                txtContraseña.Text = Application("ParametrosADMIN")(0)
                txtPuerto.Text = Application("ParametrosADMIN")(10)
                TextSMTP.Text = Application("ParametrosADMIN")(15)

                chkRecordar.Checked = CBool(Application("ParametrosADMIN")(13))
                chkRegistro.Checked = CBool(Application("ParametrosADMIN")(1))

            End If
        End If
    End Sub

    Private Sub bttLimpiar_Click(sender As Object, e As EventArgs) Handles bttLimpiar.Click
        txtEmpresa.Text = ""
        txtAlias.Text = ""
        txtEmailEnvio.Text = ""
        txtRTN.Text = ""
        txtEmail.Text = ""
        txtTel.Text = ""
        txtDireccion.Text = ""
        TxtADMIN_URL_WEB.Text = ""
        txtContraseña.Text = ""
        txtPuerto.Text = ""
        TextSMTP.Text = ""


        txtPreguntas.Value = txtPreguntas.Attributes("data-min")
        txtmaximousu.Value = txtmaximousu.Attributes("data-min")
        txtvigenciausu.Value = txtvigenciausu.Attributes("data-min")
        txtminimousu.Value = txtminimousu.Attributes("data-min")
        txtmaximocarat.Value = txtmaximocarat.Attributes("data-min")
        txtIntentos.Value = txtIntentos.Attributes("data-min")
        txtminimocarac.Value = txtminimocarac.Attributes("data-min")


    End Sub

    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        Dim Ssql As String
        'nombre empresa 
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtEmpresa.Text & "' where id_parametro =5"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'alias
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtAlias.Text & "' where id_parametro =6"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

    End Sub
End Class