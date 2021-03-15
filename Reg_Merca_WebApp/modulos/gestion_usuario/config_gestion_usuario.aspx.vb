Public Class Config_Gestion_Usuario

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

        'Using Parametros_Sistema As New ControlDB
        '    Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        'End Using

        'Using Parametros_admin As New ControlDB
        '    Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        'End Using

        If Session("user_rol") = 5 Then
            SqlRol.SelectCommand = "Select rol, id_rol from DB_Nac_Merca.tbl_15_rol"
        Else
            SqlRol.SelectCommand = "Select rol, id_rol from DB_Nac_Merca.tbl_15_rol where id_rol not in (5)"
        End If

        'parametros de contraseña
        reqcontra.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        reqcontra.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
        txtContra.MaxLength = Application("ParametrosADMIN")(0)

        'parametros de contraseña robusta
        ReqValidacionRobusta.ErrorMessage = "La contraseña debe contener 1 letra minuscula, 1 letra mayuscula, 1 carácter especial, 1 numero y el rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        ReqValidacionRobusta.ValidationExpression = "^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"


        'parametros de contraseña resetear
        reqcontraresetear.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        reqcontraresetear.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
        txtcontraresetear.MaxLength = Application("ParametrosADMIN")(0)

        'parametros de contraseña robusta resetear
        ReqValidacionRobustaresetear.ErrorMessage = "La contraseña debe contener 1 letra minuscula, 1 letra mayuscula, 1 carácter especial, 1 numero y el rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        ReqValidacionRobustaresetear.ValidationExpression = "^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"



        Select Case Request.QueryString("action")
            Case "new"
                Dim fechaactual As Date = (Date.Now)
                txtFechaCreacion.Text = fechaactual
                Fecha_Vencimiento_usuario.Text = fechaactual.AddDays(Application("ParametrosADMIN")(12))
                txtContra.Attributes("value") = CrearPassword(Application("ParametrosADMIN")(0))
                txtContraConfirmar.Attributes("value") = txtContra.Attributes("value")
                cmbEstado.Enabled = False
                bttResetear.Visible = False
                panelResetear.Visible = False
            Case "update"
                If Not IsPostBack Then

                    panelContra.Visible = False
                Dim Ssql As String = String.Empty
                Ssql = "select * from DB_Nac_Merca.tbl_02_usuarios where id_usuario =" & Request.QueryString("xuser") & ""
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        'cargar txt
                        registro = DataSetX.Tables(0).Rows(0)

                        txtNombre.Text = registro("nombre")
                        txtUsuario.Text = registro("usuario")
                        txtUsuario.ReadOnly = "TRUE"
                        cmbRol.SelectedValue = registro("id_rol")
                        cmbRol.Attributes.Add("disabled", "disabled")
                        txtCorreoElectronico.Text = registro("correo")
                        HiddenCorreo.Value = registro("correo")
                        txtFechaCreacion.Enabled = "False"
                        txtContra.Text = registro("clave")
                        txtContra.Enabled = False
                        txtFechaCreacion.Text = registro("fecha_creacion")
                        txtFechaCreacion.Enabled = "TRUE"
                        Fecha_Vencimiento_usuario.Text = CDate(registro("fecha_vencimiento")).ToShortDateString
                        Fecha_Vencimiento_usuario.Enabled = "TRUE"
                        cmbEstado.SelectedValue = registro("estado")
                        Session("estado_temp") = registro("estado")
                        cmbEstado.Attributes.Add("disabled", "disabled")
                    End If
                End If
            Case Else
                Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx")
        End Select
        If Not IsPostBack Then
            Select Case Request.QueryString("alerta")
                Case "1"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','La contraseña se cambio con exito', 'success');</script>")
            End Select
        End If
    End Sub
    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        Dim Ssql As String

        'Dim nombre As String

        Select Case Request.QueryString("action")
            Case "new"

                If (UCase(txtUsuario.Text) = UCase(txtContra.Text)) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','El usuario y la contraseña deben ser distintos.', 'error');</script>")
                Else


                    Ssql = "CALL autoregistro('" & txtUsuario.Text & "', '" & txtCorreoElectronico.Text & "')"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                        Select Case registro("EXISTE")
                            Case -1 'usuario y correo existen
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario & Correo','El usuario y correo electronico ya estan registrados.', 'error');</script>")
                            Case -2 'usuario existe
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Nombre de usuario','El nombre de usuario ya esta registrado.', 'error');</script>")
                            Case -3 'correo existe
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo electronico','El correo electronico ya estan registrado.', 'error');</script>")
                            Case 0 'no existe
                                Ssql = "Insert into DB_Nac_Merca.tbl_02_usuarios (Nombre,Usuario,id_rol,correo,clave,estado,fecha_creacion,fecha_vencimiento,creado_por,intentos,emailconfir,cambio_clave) values ('" & txtNombre.Text & "', '" & txtUsuario.Text & "', " & cmbRol.SelectedValue & ", '" & txtCorreoElectronico.Text & "',  SHA('" & txtContra.Text & "'),0,CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosADMIN")(12) & " DAY), '" & Session("user_nombre_usuario") & "',0,0,0)"
                                Using con As New ControlDB
                                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                End Using
                                SendActivationEmail()
                                Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=UsuarioRegistrado")
                        End Select
                    End If


                End If
            Case "update"
                If txtCorreoElectronico.Text = HiddenCorreo.Value Then
                    ''GUARDAR NORMAL
                    'TERMINAR DE EDITAR 
                    Ssql = "update tbl_02_usuarios set Nombre='" & txtNombre.Text & "',correo='" & txtCorreoElectronico.Text & "' where id_usuario =" & Request.QueryString("xuser") & ""
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using


                    Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=UsuarioActualizado")

                Else
                    ''GUARDAR CORREO Y ENVIAR TOKEN PARA CONFIRMAR CORREO ELECTRONICO 
                    Ssql = "SELECT *FROM DB_Nac_Merca.tbl_02_usuarios where correo= '" & txtCorreoElectronico.Text & "';"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 13, "El correo " & txtCorreoElectronico.Text & " ya esta registrado")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo electronico','El correo electronico ya esta registrado.', 'error');</script>")
                    Else

                        'COMPLETAR QRY UPDATE
                        Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  Nombre='" & txtNombre.Text & "',  correo = '" & txtCorreoElectronico.Text & "' where id_usuario =" & Request.QueryString("xuser") & ""
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using

                        'Using log_bitacora As New ControlBitacora
                        '    log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 13, "El usuario cambia su correo de " & HiddenCorreo.Value & " a " & txtCorreoElectronico.Text & "")
                        'End Using


                        Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  emailconfir = 0 where  id_usuario =" & Request.QueryString("xuser") & ""
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using


                        'enviar correo electronico con token de nueva contraseña
                        Dim activationCode As String = Guid.NewGuid().ToString()
                        Ssql = "DELETE FROM `DB_Nac_Merca`.`tbl_35_activacion_usuario` WHERE tipo='correo' and id_usuario=" & Request.QueryString("xuser") & ";"
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using

                        Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & Request.QueryString("xuser") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(8) & " DAY),'correo',1);"
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using
                        Dim urllink As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & "/Inicio/activacion.aspx?ActivationCode=" & activationCode

                        Using xCorreo As New ControlCorreo
                            xCorreo.envio_correo("Recibimos una solicitud para confirmar su correo electrónico. Si no realizó esta solicitud, ignore esta notificación. De lo contrario, puede confirmar su correo electrónico mediante este enlace. ", "CONFIRMAR",
                                             txtCorreoElectronico.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                             txtNombre.Text,
                                             urllink, "Confirmar Correo Electrónico",
                                             Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                             Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                        End Using
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 13, "Se crea un token para que el usuario valide su correo")
                        End Using
                        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=UsuarioActualizado")
                    End If
                End If
        End Select



    End Sub
    Private Sub SendActivationEmail()

        Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_02_usuarios where usuario = BINARY  '" & txtUsuario.Text & "'"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then

            Dim registro As DataRow
            registro = DataSetX.Tables(0).Rows(0)
            Dim activationCode As String = Guid.NewGuid().ToString()
            Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & registro("id_usuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(7) & " DAY),'registro',1);"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Dim urllink As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & "/Inicio/activacion.aspx?ActivationCode=" & activationCode

            Using xCorreo As New ControlCorreo
                xCorreo.envio_correo("Para continuar con el registro haga click en el siguiente enlace y asi poder activar su cuenta.", "ACTIVAR CUENTA",
                                         txtCorreoElectronico.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                       txtNombre.Text.Trim(),
                                         urllink, "Activación de Cuenta",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
            End Using

        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El registro no fue completado. Intentelo de nuevo.', 'error');</script>")
        End If
    End Sub

    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?")

    End Sub



    Private Sub bttGenerar_Click(sender As Object, e As EventArgs) Handles bttGenerar.Click

        Select Case Request.QueryString("action")
            Case "new"
                txtContra.Attributes("value") = CrearPassword(Application("ParametrosADMIN")(0))
                txtContraConfirmar.Attributes("value") = txtContra.Attributes("value")
            Case "update"
                txtcontraresetear.Attributes("value") = CrearPassword(Application("ParametrosADMIN")(0))
                txtContraConfirmarresetear.Attributes("value") = txtcontraresetear.Attributes("value")
        End Select
    End Sub

    Private Sub bttResetear_Click(sender As Object, e As EventArgs) Handles bttResetear.Click
        If Session("estado_temp") <> 3 And Session("estado_temp") <> 5 Then

            Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_02_usuarios where id_usuario = " & Request.QueryString("xuser") & ""
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim registro As DataRow
                registro = DataSetX.Tables(0).Rows(0)

                Ssql = "CALL contrasenas(" & registro("id_usuario") & ", SHA('" & txtContraConfirmarresetear.Text & "'))"
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    registro = DataSetX.Tables(0).Rows(0)
                    Select Case registro("repetida")
                        Case 1 'contraseña ya usada
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 8, "No se permite el cambio de contraseña, porque la nueva contraseña ya fue usada anteriormente para el usuario " & txtUsuario.Text)
                            End Using
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','No puede usar una contraseña igual a las usasdas anteriormente.', 'warning');</script>")
                        Case 2 'contraseña sin usar


                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 8, "El cambio de contraseña fue exitoso para el usuario " & txtUsuario.Text)
                            End Using

                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 8, "Se crea historico de cambio de contraseña con exito para el usuario " & txtUsuario.Text)
                            End Using


                            ''envio de correo usuario token cambio clave
                            Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where id_usuario =  " & Request.QueryString("xuser") & " and tipo='clave_admin'"
                            Using con As New ControlDB
                                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            End Using

                            Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where id_usuario =  " & Request.QueryString("xuser") & " and tipo='clave'"
                            Using con As New ControlDB
                                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            End Using

                            Dim activationCode As String = Guid.NewGuid().ToString()
                            If Session("estado_temp") = 0 Then
                                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & Request.QueryString("xuser") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(7) & " DAY),'clave_admin',1);"
                            Else
                                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & Request.QueryString("xuser") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(7) & " DAY),'clave',1);"
                            End If
                            Using con As New ControlDB
                                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            End Using


                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), 8, "El token de la solicitud de cambio de contraseña fue creado para el usuario " & txtUsuario.Text)
                            End Using

                            Dim urllink As String
                            If Session("estado_temp") = 0 Then
                                urllink = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & "/Inicio/reiniciarcontra.aspx?ActivationCode=" & activationCode
                            Else
                                urllink = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & "/Inicio/activacion.aspx?ActivationCode=" & activationCode
                            End If


                            Using xCorreo As New ControlCorreo
                                xCorreo.envio_correo("Recibimos una solicitud para restablecer su contraseña. Si no realizó esta solicitud, ignore esta notificación. De lo contrario, puede restablecer su contraseña mediante este enlace.", "CAMBIAR CONTRASEÑA",
                                                     txtCorreoElectronico.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                                   txtNombre.Text.Trim(),
                                                     urllink, "Cambio de Contraseña",
                                                     Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                                     Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                            End Using

                            If Session("estado_temp") = 4 Then
                                Ssql = "update tbl_02_usuarios set cambio_clave=1  where id_usuario =" & Request.QueryString("xuser") & ""
                            Else
                                Ssql = "update tbl_02_usuarios set estado=" & Session("estado_temp") & " , cambio_clave=1  where id_usuario =" & Request.QueryString("xuser") & ""
                            End If

                            Using con As New ControlDB
                                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            End Using

                            Response.Redirect("~/modulos/gestion_usuario/config_gestion_usuario.aspx?action=update&xuser=" & Request.QueryString("xuser") & "&alerta=1")
                    End Select
                End If
            Else
                ''aqui alerta de error que no encontro usuario
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','Usuario no encontrado, la contraseña no pudo ser reseteada.', 'error');</script>")

            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Estado','No se permite el cambio de contraseña a un usuario inactivo o caducado', 'error');</script>")
        End If
    End Sub

    Private Function CrearPassword(ByVal longitud As Integer) As String
        Dim obj As New Random()
        Dim xNuevoPass As New StringBuilder
        Dim idx As Integer
        Dim xmayus As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" '1
        Dim xminus As String = "abcdefghijklmnopqrstuvwxyz" '1
        Dim xnumeros As String = "0123456789" '1
        Dim xcaracteres As String = "!#$@%&()*+<>?=" '1
        Dim xTodos As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$@%&()*+<>?=" 'el parametro de longitud de contra menos la cantidad variables anteriores


        idx = obj.Next(0, xmayus.Length - 1)
        xNuevoPass.Append(xmayus.Substring(idx, 1))

        idx = obj.Next(0, xminus.Length - 1)
        xNuevoPass.Append(xminus.Substring(idx, 1))

        idx = obj.Next(0, xnumeros.Length - 1)
        xNuevoPass.Append(xnumeros.Substring(idx, 1))

        idx = obj.Next(0, xcaracteres.Length - 1)
        xNuevoPass.Append(xcaracteres.Substring(idx, 1))

        For i = 1 To longitud - 4
            idx = obj.Next(0, xTodos.Length - 1)
            xNuevoPass.Append(xTodos.Substring(idx, 1))
        Next


        Return xNuevoPass.ToString
    End Function

End Class