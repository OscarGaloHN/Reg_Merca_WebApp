Public Class configurar
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
            If Not IsPostBack Then
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 1, "El usuario ingresa a la pantalla de configuraciones")
                End Using
                txtEmpresa.Text = Application("ParametrosADMIN")(2)
                txtAlias.Text = Application("ParametrosADMIN")(3)
                txtRTN.Text = Application("ParametrosADMIN")(6)
                txtEmail.Text = Application("ParametrosADMIN")(4)
                txttel.Text = Application("ParametrosADMIN")(5)
                txtDireccion.Text = Application("ParametrosADMIN")(14)
                txtADMIN_URL_WEB.Text = Application("ParametrosADMIN")(19)

            End If
        End If
    End Sub

    Private Sub bttLimpiar_Click(sender As Object, e As EventArgs) Handles bttLimpiar.Click
        txtEmpresa.Text = ""
        txtAlias.Text = ""
        txtRTN.Text = ""
        txtEmail.Text = ""
        txttel.Text = ""
        txtDireccion.Text = ""
        txtADMIN_URL_WEB.Text = ""




    End Sub

    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        Dim Ssql As String
        'nombre empresa 
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Ingreso del nombre de la empresa")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtEmpresa.Text & "' where id_parametro =5"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'alias
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Ingreso del alias de la empresa")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtAlias.Text & "' where id_parametro =6"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'RTN
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Ingreso del RTN de la empresa")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtRTN.Text & "' where id_parametro =9"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        'email empresa
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Ingreso del Email de la empresa")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmail.Text & "' where id_parametro =7"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'telefono de la empresa
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Ingreso del telefono de la empresa")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txttel.Text & "' where id_parametro =8"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        'direccion
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Ingreso de la direccion de la empresa")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtDireccion.Text & "' where id_parametro =20"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using




        'sistema configurado
        Using log_bitacora As New ControlBitacora
            log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 1, "se realizaron modificaciones a configuraciones")
        End Using
        Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor ='TRUE' where id_parametro =10"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        Session.Abandon()
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuraciones','Su configuacion fue almacenada exitosamente.', 'success');</script>")
    End Sub



End Class