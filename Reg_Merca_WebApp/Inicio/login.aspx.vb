
Imports System.Net.Mail
'OBJETO #3
Public Class login
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
                If IsPostBack = False Then
                    'parametros de configuracion de sistema
                    Using Parametros_Sistema As New ControlDB
                        Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
                    End Using

                    'PARAMETROS DE ADMINISTRADOR
                    Using Parametros_admin As New ControlDB
                        Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
                    End Using


                    'parametros de USUARIO
                    valiUserLargo.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(16) & " -" & Application("ParametrosADMIN")(17) & ")."
                    valiUserLargo.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(16) & "," & Application("ParametrosADMIN")(17) & "}$"
                    txtUsuario.MaxLength = Application("ParametrosADMIN")(17)

                    'parametros de contraseña
                    reContraLogin.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
                    reContraLogin.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
                    txtContra.MaxLength = Application("ParametrosADMIN")(0)

                    bttEntrar.Focus()
                    Select Case Request.QueryString("action")
                        Case "changepasswordout"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','Cambio de contraseña completo, inicie sesión con su nueva contraseña.', 'success');</script>")
                        Case "registro"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Registro','La solicite de registro se ha completado, hemos enviado detalles a su correo electronico para completar su solicitud.', 'success');</script>")
                        Case "activateuser"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Activación','Su usuario esta activado, inicie sesión para configurar su cuenta.', 'success');</script>")
                        Case "newsolicitud"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Nueva Solicitud','Hemos enviado detalles a su correo electronico para completar su solicitud', 'success');</script>")
                        Case "activateemail"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','Gracias por verificar su correo electrónico.', 'success');</script>")
                        Case "systemconfig"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Sistema Configurado','Los cambios fueron aplicados se cerro la sesión para recargar los parametros', 'success');</script>")
                    End Select


                    If ((Not (Request.Cookies("UserName")) Is Nothing) _
                              AndAlso (Not (Request.Cookies("Password")) Is Nothing) _
                              AndAlso (Not (Request.Cookies("chkRecordar")) Is Nothing)) Then
                        chkRecordar.Checked = Request.Cookies("chkRecordar").Value

                        Using desencriptarck As New ControlCorreo
                            txtContra.Attributes("value") = desencriptarck.Desencriptar(Request.Cookies("Password").Value)
                            txtUsuario.Text = desencriptarck.Desencriptar(Request.Cookies("UserName").Value)
                        End Using

                    End If

                End If
            Else
                'REDIRECCIONAR A MENU PRINCIPAL
                Response.Redirect("~/modulos/menu_principal.aspx")
            End If
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','Error inesperado recargue la pagina', 'error');</script>")
        End Try
    End Sub

    Private Sub bttEntrar_Click(sender As Object, e As EventArgs) Handles bttEntrar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT CASE WHEN T02.CantiPreguntas IS NULL THEN 0 ELSE T02.CantiPreguntas END CantiPreguntas, T01.* FROM DB_Nac_Merca.tbl_02_usuarios T01 
                LEFT JOIN (SELECT id_usuario, COUNT(*) as CantiPreguntas FROM DB_Nac_Merca.tbl_23_preguntas_usuario  
                WHERE id_usuario = (SELECT id_usuario FROM DB_Nac_Merca.tbl_02_usuarios  
                WHERE usuario = BINARY '" & txtUsuario.Text & "' GROUP BY id_usuario) 
                ) T02 ON T01.id_usuario = T02.id_usuario   where T01.usuario = BINARY  '" & txtUsuario.Text & "' and clave = SHA('" & txtContra.Text & "');"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        Dim registro As DataRow
        If Session("NumReg") > 0 Then
            'Si coloco las credenciales correctas
            registro = DataSetX.Tables(0).Rows(0)
            'CARGAR DATOS DE USUARIO
            Session("user_idUsuario") = registro("id_usuario")
            Session("user_nombre_usuario") = registro("usuario")
            Session("user_nombre_personal") = registro("nombre")
            Session("user_correo") = registro("correo")
            Session("user_rol") = registro("id_rol")
            Session("user_confirma_correo") = registro("emailconfir")
            Session("user_estado") = registro("estado")
            Session("user_canti_preguntas") = registro("CantiPreguntas")


            If registro("cambio_clave") = 0 Then
                Select Case Session("user_estado")
                    Case 0 'USUARIO CREADO
                        'usuario creado por admin se otorgo contraseña enviar a activar
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente pero es enviado a configurar contaseña ya que es un nuevo usuario")
                        End Using
                        Response.Redirect("~/inicio/activacion.aspx?acction=activateuser&userregis=" & Session("user_idUsuario"))
                    Case 1 'CONFIGURAR USUARIO / nuevo
                        'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas','Enviar a respoder preguntas.', 'error');</script>")
                        'registrar bitacora login
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente pero es enviado a configurar preguntas de seguridad ya que es nuevo usuario")
                        End Using
                        Response.Redirect("~/modulos/perfil_usuario/confi_perfil_preguntas.aspx?acction=autoquestions")
                    Case 2 'activo
                        'datos de conexion y reseteo de intentos malos
                        Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  fecha_ultima_conexion = CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), intentos=0, en_linea=1 where usuario = BINARY  '" & txtUsuario.Text & "';"
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using
                        If chkRecordar.Checked = True Then
                            Response.Cookies("UserName").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("Password").Expires = DateTime.Now.AddDays(30)
                            Response.Cookies("chkRecordar").Expires = DateTime.Now.AddDays(30)
                        Else
                            Response.Cookies("UserName").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("Password").Expires = DateTime.Now.AddDays(-1)
                            Response.Cookies("chkRecordar").Expires = DateTime.Now.AddDays(-1)
                        End If

                        Using encriptarck As New ControlCorreo
                            Response.Cookies("Password").Value = encriptarck.Encriptar(txtContra.Text.Trim)
                            Response.Cookies("UserName").Value = encriptarck.Encriptar(txtUsuario.Text.Trim)
                        End Using

                        Response.Cookies("chkRecordar").Value = chkRecordar.Checked
                        'verificar que el sistema este configurado
                        If CBool(Application("ParametrosSYS")(2)) = True Then
                            'virificar que tenga las preguntas de seguridad contestadas
                            'If Session("user_canti_preguntas") >= Application("ParametrosADMIN")(8) Then
                            'verificar el rol
                            Select Case CInt(Session("user_rol"))
                                Case 6 'si es auto registro no tiene rol asignado hasta que lo de el admin
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente pero es enviado a la alerta de que no tiene un rol asignado")
                                    End Using

                                    Response.Redirect("~/modulos/confi_rol.aspx")
                                Case Else
                                    'registrar bitacora login
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente y es enviado al menu principal")
                                    End Using
                                    'si el sitio esta configurado
                                    Response.Redirect("~/modulos/menu_principal.aspx")
                            End Select
                            'Else
                            'Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=awquestions")
                            'End If
                        Else
                            Select Case CInt(Session("user_rol"))
                                Case 5 'si el rol es mantenimiento
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente y es enviado a configurar el sistema ya que el parametro es falso")
                                    End Using
                                    Response.Redirect("~/modulos/configuraciones/confi_configurar.aspx")
                                Case 1 'si el rol es admin
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente y es enviado a configurar el sistema ya que el parametro es falso")
                                    End Using
                                    Response.Redirect("~/modulos/configuraciones/confi_configurar.aspx")
                                Case Else 'si no es admin
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario inicia sesión exitosamente pero se cierra la sesión automaticamente ya que el sistema no esta configurado y el usuario no es administrador")
                                    End Using

                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuración','El administrador no ha completado la configuración del sistema.', 'warning');</script>")
                                    Session.Abandon()
                            End Select
                        End If

                    Case 3 'bloqueado o inactivo

                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "Usuario inactivo intenta iniciar sesión")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Inactivo','Usuario Inactivo, Contactece con el administrador.', 'warning');</script>")
                        Session.Abandon()
                    Case 4 'bloqueo por intentos
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "Usuario bloqueado intenta iniciar sesión")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'warning');</script>")
                        Session.Abandon()
                    Case 5 'usuario caducado
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "Usuario caducado intenta iniciar sesión")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Caducado','Usuario Caducado, Contactece con el administrador.', 'warning');</script>")
                        Session.Abandon()
                        'Case 6 'ADMIN ENTREGA NUEVA CONTRASEÑA AL USUARIO Y SE LE SOLICITA CAMBIAR POR UNA NUEVA
                        '    Using log_bitacora As New ControlBitacora
                        '        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario recibio un cambio de contraseña y es envio a cambiarla")
                        '    End Using

                        '    Ssql = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where tipo='clave' and id_usuario =  " & Session("user_idUsuario") & ""
                        '    Using con As New ControlDB
                        '        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        '        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                        '    End Using
                        '    If Session("NumReg") > 0 Then
                        '        registro = DataSetX.Tables(0).Rows(0)
                        '        'envair a activacion de usuario
                        '        Response.Redirect("~/Inicio/activacion.aspx?ActivationCode=" & registro("codigo_activacion"))
                        '        Session.Abandon()
                        '    End If
                End Select
            Else


                Select Case Session("user_estado")
                    Case 0 'USUARIO CREADO
                        Ssql = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where tipo='clave_admin' and id_usuario =  " & Session("user_idUsuario") & ""
                        Using con As New ControlDB
                            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            Session("NumReg") = DataSetX.Tables(0).Rows.Count
                        End Using
                        If Session("NumReg") > 0 Then
                            registro = DataSetX.Tables(0).Rows(0)
                            'usuario creado por admin se otorgo contraseña enviar a activar
                            'envair a activacion de usuario
                            Response.Redirect("~/Inicio/reiniciarcontra.aspx?ActivationCode=" & registro("codigo_activacion"))
                            Session.Abandon()
                        Else
                            Session.Abandon()
                        End If
                    Case Else
                        Ssql = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where tipo='clave' and id_usuario =  " & Session("user_idUsuario") & ""
                        Using con As New ControlDB
                            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            Session("NumReg") = DataSetX.Tables(0).Rows.Count
                        End Using
                        If Session("NumReg") > 0 Then
                            registro = DataSetX.Tables(0).Rows(0)
                            Response.Redirect("~/Inicio/activacion.aspx?ActivationCode=" & registro("codigo_activacion"))
                            Session.Abandon()
                        Else
                            Session.Abandon()
                        End If
                End Select

            End If
        Else
                ''''comporbar si el usuario existe para saber si escribio mal la contrasñea
                Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "';"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                registro = DataSetX.Tables(0).Rows(0)
                Session("user_idUsuario") = registro("id_usuario")
                If IsDBNull(registro("clave")) Then 'el uusarui quiere entrar al sistema sin configurar su contraseña
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario intenta iniciar sesión sin completar registro, este usuario fue autoregistrado")
                    End Using

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
                Else
                    If registro("intentos") = Application("ParametrosADMIN")(7) Then
                        'usuario ingresa mal la contraseña pero su usuario esta bloqueado
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario intenta iniciar sesión pero ingresa mal sus credenciales y su estado es bloqueado")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
                    Else
                        If CInt(registro("intentos")) + 1 = CInt(Application("ParametrosADMIN")(7)) Then 'PARAMETRO INTENTOS DE CONTRASEÑA el usuario se bloquea
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario intenta iniciar sesión pero ingresa mal sus credenciales y el sistema lo bloquea porque llega a los " & CInt(registro("intentos")) + 1 & " intentos")
                            End Using
                            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & ", estado=4 where usuario = BINARY  '" & txtUsuario.Text & "';"
                        Else
                            'suma de intentos para el usuario
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(1, Session("user_idUsuario"), 3, "El usuario intenta iniciar sesión pero ingresa mal sus credenciales, el usuario suma " & CInt(registro("intentos")) + 1 & " intentos")
                            End Using

                            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & " where usuario = BINARY  '" & txtUsuario.Text & "';"
                        End If
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
                    End If
                End If
                Session.Abandon()
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
            End If
        End If
    End Sub
End Class