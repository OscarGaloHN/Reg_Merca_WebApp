Public Class config_avanz
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
            If Not IsPostBack Then

                txtEmailEnvio.Text = Application("ParametrosADMIN")(9)
                txtPreguntas.Value = Application("ParametrosADMIN")(8)
                txtIntentos.Value = Application("ParametrosADMIN")(7)

                Using desencriptarck As New ControlCorreo
                    txtContrasena.Attributes("value") = desencriptarck.Desencriptar(Application("ParametrosADMIN")(11))
                End Using

                txtPuerto.Text = Application("ParametrosADMIN")(10)
                TextSMTP.Text = Application("ParametrosADMIN")(15)


                txtmaximousu.Value = Application("ParametrosADMIN")(17)
                txtminimousu.Value = Application("ParametrosADMIN")(16)

                txtvigenciausu.Value = Application("ParametrosADMIN")(12)

                txtmaximocarat.Value = Application("ParametrosADMIN")(0)
                txtminimocarac.Value = Application("ParametrosADMIN")(18)

                chkRecordarusu.Checked = CBool(Application("ParametrosADMIN")(13))
                chkRegistro.Checked = CBool(Application("ParametrosADMIN")(1))
            End If
        End If
    End Sub

    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        Dim Ssql As String
        'url web
        'Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TxtADMIN_URL_WEB.Text & "' where id_parametro =29"
        'Using con As New ControlDB
        '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        'End Using
        'correo de alertas
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmailEnvio.Text & "' where id_parametro =14"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'puerto
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtPuerto.Text & "' where id_parametro =15"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'smtp
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TextSMTP.Text & "' where id_parametro =21"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'contraseña
        Using encriptax As New ControlCorreo
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & encriptax.Encriptar(txtContrasena.Text) & "' where id_parametro =16"
        End Using
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'maximo usuario
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtmaximousu.Value & "' where id_parametro =26"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'minimo usuario

        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtminimousu.Value & "' where id_parametro =25"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'minmo de caracter de contraseña
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtminimocarac.Value & "' where id_parametro =28"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'maximo caracteres de contrasea
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtmaximocarat.Value & "' where id_parametro =1"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'dias de vigencia
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtvigenciausu.Value & "' where id_parametro =18"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'intentos
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtIntentos.Value & "' where id_parametro =12"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'preguntas
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtPreguntas.Value & "' where id_parametro =13"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using


        'frm auto registro
        If chkRegistro.Checked = True Then
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =2"
        Else
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =2"

        End If
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using


        'frm recordar usuario
        If chkRecordarusu.Checked = True Then
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =19"
        Else
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =19"
        End If
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using


        'sistema configurado
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor ='TRUE' where id_parametro =10"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        Session.Abandon()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuraciones Avanzadas','Su configuacion fue almacenada exitosamente.', 'success');</script>")
        'Response.Redirect("~/Inicio/login.aspx?action=systemconfig")

    End Sub

    Private Sub bttLimpiar_Click(sender As Object, e As EventArgs) Handles bttLimpiar.Click
        txtEmailEnvio.Text = ""
        txtContrasena.Text = ""
        txtPuerto.Text = ""
        TextSMTP.Text = ""
        txtContrasena.Attributes("value") = ""
        chkRecordarusu.Checked = False
        chkRegistro.Checked = False

        txtPreguntas.Value = txtPreguntas.Attributes("data-min")
        txtmaximousu.Value = txtmaximousu.Attributes("data-min")
        txtminimousu.Value = txtminimousu.Attributes("data-min")

        txtvigenciausu.Value = txtvigenciausu.Attributes("data-min")
        txtmaximocarat.Value = txtmaximocarat.Attributes("data-min")
        txtminimocarac.Value = txtminimocarac.Attributes("data-min")

        txtIntentos.Value = txtIntentos.Attributes("data-min")
    End Sub



End Class