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

    Private Sub bttLimpiar_Click(sender As Object, e As EventArgs) Handles bttLimpiar.Click
        txtEmpresa.Text = ""
        txtAlias.Text = ""
        txtEmailEnvio.Text = ""
        txtRTN.Text = ""
        txtEmail.Text = ""
        txtTel.Text = ""
        txtDireccion.Text = ""
        TxtADMIN_URL_WEB.Text = ""
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

        'RTN
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtRTN.Text & "' where id_parametro =9"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'email empresa
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmail.Text & "' where id_parametro =7"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'telefono de la empresa
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtTel.Text & "' where id_parametro =8"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'direccion
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtDireccion.Text & "' where id_parametro =20"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'url web
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TxtADMIN_URL_WEB.Text & "' where id_parametro =29"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
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
        Response.Redirect("~/Inicio/login.aspx?action=systemconfig")
    End Sub
End Class