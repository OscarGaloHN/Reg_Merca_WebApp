﻿Public Class recuperar
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
        bttPreguntas.Attributes.Add("onClick", "return false;")
        'bttEnviar.Focus()
        'If IsPostBack = False Then
        'parametros de configuracion de sistema
        Using Parametros_Sistema As New ControlDB
            Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        End Using

        'PARAMETROS DE ADMINISTRADOR
        Using Parametros_admin As New ControlDB
            Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        End Using
        'End If


    End Sub

    Private Sub bttContinuar_Click(sender As Object, e As EventArgs) Handles bttContinuar.Click
        'comprobar que el usuario existe y que este activo o bloqueado para permitir ingreso
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuarioPreguntas.Text & "';"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
            If CInt(registro("estado")) <> 2 And CInt(registro("estado")) <> 4 Then

                Using log_bitacora As New ControlBitacora
                    log_bitacora.log_sesion_inicio(6, Session("user_idUsuario"), "" & txtUsuarioPreguntas.Text & " puede estar inactivo, caducado o sin completar el registro y no se  le permite continuar")
                End Using
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Advertencia','Este usuario puede estar inactivo, caducado o sin completar el registro.', 'warning');</script>")
            Else

                Using log_bitacora As New ControlBitacora
                    log_bitacora.log_sesion_inicio(6, Session("user_idUsuario"), "" & txtUsuarioPreguntas.Text & " es enviado a respoder preguntas de seguridad para continuar")
                End Using
                Session("id_usuarioPreguntas") = registro("id_usuario")
                Session("nombre_usuario_comparar") = txtUsuarioPreguntas.Text
                Response.Redirect("~/Inicio/preguntas.aspx")
            End If
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Advertencia','Usuario no encontrado', 'warning');</script>")
        End If
    End Sub

    Private Sub bttEnviar_Click(sender As Object, e As EventArgs) Handles bttEnviar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where correo = BINARY  '" & txtEmail.Text & "';"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Dim registro As DataRow = DataSetX.Tables(0).Rows(0)

            'solo los usuarios activos
            If CInt(registro("estado")) <> 2 And CInt(registro("estado")) <> 4 Then
                Using log_bitacora As New ControlBitacora
                    log_bitacora.log_sesion_inicio(6, Session("user_idUsuario"), "con correo " & txtEmail.Text & " puede estar inactivo, caducado o sin completar el registro y no se  le permite continuar")
                End Using
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Advertencia','Este usuario puede estar inactivo, caducado o sin completar el registro.', 'warning');</script>")
            Else
                'enviar correo electronico con token de nueva contraseña
                Dim activationCode As String = Guid.NewGuid().ToString()
                Ssql = "DELETE FROM `DB_Nac_Merca`.`tbl_35_activacion_usuario` WHERE tipo='clave' and id_usuario=" & registro("id_usuario") & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using

                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & registro("id_usuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(8) & " DAY),'clave',1);"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using

                Using xCorreo As New ControlCorreo
                    xCorreo.envio_correo("Recibimos una solicitud para restablecer su contraseña. Si no realizó esta solicitud, ignore esta notificación. De lo contrario, puede restablecer su contraseña mediante este enlace. ", "RESTABLECER CONTRASEÑA",
                                         txtEmail.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                         registro("nombre"),
                                         Request.Url.AbsoluteUri.Replace("recuperar", Convert.ToString("activacion.aspx?ActivationCode=") & activationCode),
                                         "Restablecer Contraseña",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.log_sesion_inicio(6, Session("user_idUsuario"), "con correo " & txtEmail.Text & " solicita cambio de clave por correo electrónico")
                End Using
                Response.Redirect("~/Inicio/login.aspx?action=newsolicitud")
            End If
        Else
            Using log_bitacora As New ControlBitacora
                log_bitacora.log_sesion_inicio(6, Session("user_idUsuario"), "con correo " & txtEmail.Text & " no fue encontrado")
            End Using
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','Correo no enviado', 'error');</script>")
            txtEmail.Text = ""
        End If
    End Sub
End Class