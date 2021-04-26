﻿Public Class config_avanz
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'comprobar que el rol del usuario tenga permisos para ingresar
                Dim Ssql As String = String.Empty
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 2 and permiso_consulta = 1"
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then

                    If Not IsPostBack Then
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 2, "El usuario ingresa a la pantalla de configuraciones avanzadas")
                        End Using

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
                    If CBool(Application("ParametrosSYS")(2)) = True Then
                        bttGuardar.Visible = True
                        bttLimpiar.Visible = True
                        bttTerminarConfigurar.Visible = False
                    Else
                        bttGuardar.Visible = False
                        bttLimpiar.Visible = False
                        bttTerminarConfigurar.Visible = True
                    End If
                Else
                    'si no tiene permisos 
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 2, "El usuario intenta ingresa a una pantalla sin permisos")
                    End Using
                    Response.Redirect("~/modulos/acceso_denegado.aspx")
                End If
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try

    End Sub

    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        Try
            Dim Ssql As String
            'url web
            'Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TxtADMIN_URL_WEB.Text & "' where id_parametro =29"
            'Using con As New ControlDB
            '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            'End Using
            'correo de alertas
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso correo de alertas de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmailEnvio.Text & "' where id_parametro =14"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'puerto
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso del puerto de acceso")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtPuerto.Text & "' where id_parametro =15"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'smtp
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso del SMPT")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TextSMTP.Text & "' where id_parametro =21"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'contraseña
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso de la contraseña del correo de alertas")
            End Using
            Using encriptax As New ControlCorreo
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & encriptax.Encriptar(txtContrasena.Text) & "' where id_parametro =16"
            End Using
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'maximo usuario
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del maxiomo de usuarios")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtmaximousu.Value & "' where id_parametro =26"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'minimo usuario
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del minimo de usuarios")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtminimousu.Value & "' where id_parametro =25"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'minmo de caracter de contraseña
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del minimo de caracteres")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtminimocarac.Value & "' where id_parametro =28"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'maximo caracteres de contrasea
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio de maxiomo caracteres para contraseña")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtmaximocarat.Value & "' where id_parametro =1"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'dias de vigencia
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del los dias de vigencia")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtvigenciausu.Value & "' where id_parametro =18"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'intentos
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Cambio en el numero de intentos")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtIntentos.Value & "' where id_parametro =12"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'preguntas
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio en el numero de preguntas")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtPreguntas.Value & "' where id_parametro =13"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            'frm auto registro
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio en el autoregistro")
            End Using
            If chkRegistro.Checked = True Then
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =2"
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =2"

            End If
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            'frm recordar usuario
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio en el recordar usuarios")
            End Using
            If chkRecordarusu.Checked = True Then
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =19"
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =19"
            End If
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            'sistema configurado
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 2, "se realizaron modificaciones a configuraciones avanzadas")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor ='TRUE' where id_parametro =10"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using





            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuraciones Avanzadas','Su configuacion fue almacenada exitosamente.', 'success');</script>")

            'Response.Redirect("~/Inicio/login.aspx?action=systemconfig")

        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
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

    Private Sub bttTerminarConfigurar_Click(sender As Object, e As EventArgs) Handles bttTerminarConfigurar.Click
        Try
            Dim Ssql As String
            'url web
            'Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TxtADMIN_URL_WEB.Text & "' where id_parametro =29"
            'Using con As New ControlDB
            '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            'End Using
            'correo de alertas
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso correo de alertas de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmailEnvio.Text & "' where id_parametro =14"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'puerto
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso del puerto de acceso")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtPuerto.Text & "' where id_parametro =15"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'smtp
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso del SMPT")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & TextSMTP.Text & "' where id_parametro =21"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'contraseña
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Ingreso de la contraseña del correo de alertas")
            End Using
            Using encriptax As New ControlCorreo
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & encriptax.Encriptar(txtContrasena.Text) & "' where id_parametro =16"
            End Using
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'maximo usuario
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del maxiomo de usuarios")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtmaximousu.Value & "' where id_parametro =26"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'minimo usuario
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del minimo de usuarios")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtminimousu.Value & "' where id_parametro =25"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'minmo de caracter de contraseña
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del minimo de caracteres")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtminimocarac.Value & "' where id_parametro =28"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'maximo caracteres de contrasea
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio de maxiomo caracteres para contraseña")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtmaximocarat.Value & "' where id_parametro =1"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'dias de vigencia
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio del los dias de vigencia")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtvigenciausu.Value & "' where id_parametro =18"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'intentos
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 1, "Cambio en el numero de intentos")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtIntentos.Value & "' where id_parametro =12"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'preguntas
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio en el numero de preguntas")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtPreguntas.Value & "' where id_parametro =13"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            'frm auto registro
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio en el autoregistro")
            End Using
            If chkRegistro.Checked = True Then
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =2"
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =2"

            End If
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            'frm recordar usuario
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 2, "Cambio en el recordar usuarios")
            End Using
            If chkRecordarusu.Checked = True Then
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =19"
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =19"
            End If
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            'sistema configurado
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 2, "se realizaron modificaciones a configuraciones avanzadas")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor ='TRUE' where id_parametro =10"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx?action=systemconfig")

        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub
End Class