
Imports System.Net.Mail

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
            Select Case Request.QueryString("acction")
                Case "changepasswordout"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','Cambio de contraseña completo.', 'success');</script>")
                Case "registro"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Registro','La solicite de registro se ha completado, hemos enviado detalles a su correo electronico para completar su solicitud.', 'success');</script>")
                Case "activateuser"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Activación','Su ususario esta activado, inicie sesión para configurar su cuenta.', 'success');</script>")
                Case "newsolicitud"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Nueva Solicitud','Hemos enviado detalles a su correo electronico para completar su solicitud', 'success');</script>")
                Case "activateemail"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','Gracias por verificar su correo electrónico.', 'success');</script>")
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
            'datos de conexion y reseteo de intentos malos
            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  fecha_ultima_conexion = CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), intentos=0, en_linea=1 where usuario = BINARY  '" & txtUsuario.Text & "';"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'If Session("user_confirma_correo") = 1 Then ''VALIDAR QUE TENGA LA PREGUNTA
            Select Case Session("user_estado")
                Case 0 'USUARIO CREADO
                    'usuario creado por admin se otorgo contraseña enviar a activar
                    Response.Redirect("~/inicio/activacion.aspx?acction=activateuser&userregis=" & Session("user_idUsuario"))

                Case 1 'CONFIGURAR USUARIO / nuevo
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas','Enviar a respoder preguntas.', 'error');</script>")
                    Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=autoquestions")
                Case 2 'activo
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
                        If Session("user_canti_preguntas") >= Application("ParametrosADMIN")(8) Then
                            'verificar el rol
                            Select Case CInt(Session("user_rol"))
                                Case 6 'si es auto registro
                                    Response.Redirect("~/modulos/confi_rol.aspx")
                                Case Else
                                    'si el sitio esta configurado
                                    Response.Redirect("~/modulos/principal.aspx")
                            End Select
                        Else
                            Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=awquestions")
                        End If

                    Else
                        Select Case CInt(Session("user_rol"))
                            Case 5 'si el rol es admin
                                Response.Redirect("~/modulos/confi_configurar.aspx")
                            Case Else 'si no es admin
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuración','El administrador no ha completado la configuración del sistema.', 'warning');</script>")
                                Session.Abandon()

                        End Select
                    End If

                Case 3 'bloqueado o inactivo

                Case 4 'bloqueo por intentos
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'warning');</script>")
                Case 5 'usuario caducado

                Case 6 'cambio clave

                End Select
            'Else
            '    Session.Abandon()
            '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Confirmar Correo Electrónico','Para ingresar al sistema debe de confirmar su correo electrónico.', 'warning');</script>")
            'End If
        Else
            ''''comporbar si el usuario existe para saber si escribio mal la contrasñea
            Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "';"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                registro = DataSetX.Tables(0).Rows(0)
                Select Case CInt(registro("estado"))
                    Case 0 'intento ingresar con el usuario recien creado
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
                    Case Else
                        Select Case CInt(registro("intentos"))
                            Case 3
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'warning');</script>")
                            Case Else
                                If CInt(registro("intentos")) + 1 = CInt(Application("ParametrosADMIN")(7)) Then 'PARAMETRO INTENTOS DE CONTRASEÑA
                                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & ", estado=4 where usuario = BINARY  '" & txtUsuario.Text & "';"
                                Else
                                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & " where usuario = BINARY  '" & txtUsuario.Text & "';"
                                End If
                                Using con As New ControlDB
                                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                End Using
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
                        End Select
                End Select
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
            End If
        End If


        'Dim SmtpServer As New SmtpClient()
        'Dim mail As New MailMessage()
        'SmtpServer.Credentials = New Net.NetworkCredential("registrodemercanciahn@gmail.com", "mercancia2021")
        'SmtpServer.Port = 25
        'SmtpServer.Host = "smtp.gmail.com"
        'SmtpServer.EnableSsl = True
        'mail = New MailMessage()
        'mail.From = New MailAddress("registrodemercanciahn@gmail.com", "RegMERCA")
        'mail.To.Add("oscaramador7@gmail.com")
        'mail.Subject = "Prueba del titulo"
        'mail.IsBodyHtml = True
        'mail.Body = "hola hoy es 16/2/2021"
        'SmtpServer.Send(mail)
    End Sub



End Class