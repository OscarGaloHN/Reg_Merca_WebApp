Public Class confi_perfil
    Inherits System.Web.UI.Page
    'OBJETO #13
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
            'codigo que corresponde al envento load
            'If Not IsPostBack Then
            '    btt_actualizarco.Enabled = False
            'End If
            Dim Ssql As String = String.Empty
            'Session("user_idUsuario") = 10
            'Session("user_nombre_personal") = "Oscar Rene Amador"

            Ssql = "SELECT nombre,usuario,correo,fecha_creacion,fecha_vencimiento FROM DB_Nac_Merca.tbl_02_usuarios where id_usuario= " & Session("user_idUsuario") & ";"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            Dim registro As DataRow
            If Session("NumReg") > 0 Then
                registro = DataSetX.Tables(0).Rows(0)
                Lbnombre.Text = registro("nombre")
                Lbusuario.Text = registro("usuario")
                Lbcorreo.Text = registro("correo")
                lb_fecha.Text = CDate(registro("fecha_creacion")).ToLongDateString
                lb_uscadu.Text = CDate(registro("fecha_vencimiento")).ToLongDateString
            End If
            Select Case Request.QueryString("action")
                Case "cambiocorreo"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','Hemos enviado una solicitud de confirmacion a su nuevo correo electrónico. La actualización fue exitosa.', 'success');</script>")
                Case Else
                    If Not IsPostBack Then
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 13, "El usuario ingresa a la pantalla de perfil de usuario")
                        End Using
                    End If
            End Select
        End If

    End Sub

    Private Sub btt_actualizarco_Click(sender As Object, e As EventArgs) Handles btt_actualizarco.Click

        If IsValid Then
            If (Lbcorreo.Text = txtCorreoElectronico.Text) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','El nuevo correo electrónico es el mismo actualmente.', 'error');</script>")
            Else
                Dim Ssql As String = "SELECT *FROM DB_Nac_Merca.tbl_02_usuarios where correo= '" & txtCorreoElectronico.Text & "';"
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


                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  correo = '" & txtCorreoElectronico.Text & "' where id_usuario=" & Session("user_idUsuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 13, "El usuario cambia su correo de " & Session("user_correo") & " a " & txtCorreoElectronico.Text & "")
                    End Using


                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  emailconfir = 0 where  id_usuario=" & Session("user_idUsuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using


                    'enviar correo electronico con token de nueva contraseña
                    Dim activationCode As String = Guid.NewGuid().ToString()
                    Ssql = "DELETE FROM `DB_Nac_Merca`.`tbl_35_activacion_usuario` WHERE tipo='correo' and id_usuario=" & Session("user_idUsuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & Session("user_idUsuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(8) & " DAY),'correo',1);"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Dim urllink As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & "/Inicio/activacion.aspx?ActivationCode=" & activationCode

                    Using xCorreo As New ControlCorreo
                        xCorreo.envio_correo("Recibimos una solicitud para confirmar su correo electrónico. Si no realizó esta solicitud, ignore esta notificación. De lo contrario, puede confirmar su correo electrónico mediante este enlace. ", "CONFIRMAR",
                                         txtCorreoElectronico.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                         Session("user_nombre_personal"),
                                         urllink, "Confirmar Correo Electrónico",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                    End Using
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 13, "Se crea un token para que el usuario valide su correo")
                    End Using
                    Session("user_correo") = txtCorreoElectronico.Text
                    Session("user_confirma_correo") = 0
                    txtCorreoElectronico.Text = ""
                    Response.Redirect("~/modulos/perfil_usuario/confi_perfil.aspx?action=cambiocorreo")
                End If
            End If
        End If
    End Sub
End Class