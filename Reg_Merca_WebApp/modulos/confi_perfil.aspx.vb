Public Class confi_perfil
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
        'If Not IsPostBack Then
        '    btt_actualizarco.Enabled = False
        'End If
        Dim Ssql As String = String.Empty
        Session("user_idUsuario") = 10
        Session("user_nombre_personal") = "Oscar Rene Amador"

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
            lb_fecha.Text = registro("fecha_creacion")
            lb_uscadu.Text = registro("fecha_vencimiento")
        End If
        Select Case Request.QueryString("action")
            Case "cambiocorreo"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','Hemos enviado una solicitud de confirmacion a su nuevo correo electrónico. La actualización fue exitosa.', 'success');</script>")
        End Select
    End Sub

    Private Sub btt_actualizarco_Click(sender As Object, e As EventArgs) Handles btt_actualizarco.Click

        If IsValid Then
            If (Lbcorreo.Text = txtCorreoElectronico.Text) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electrónico','El nuevo correo electrónico es el mismo actualmente.', 'error');</script>")
            Else
                Dim Ssql As String = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  correo = '" & txtCorreoElectronico.Text & "' where id_usuario=" & Session("user_idUsuario") & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using

                Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  emailconfir = 0 where  id_usuario=" & Session("user_idUsuario") & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using

                'enviar correo electronico con token de nueva contraseña
                Dim activationCode As String = Guid.NewGuid().ToString()
                Ssql = "DELETE FROM `DB_Nac_Merca`.`tbl_35_activacion_usuario` WHERE id_usuario=" & Session("user_idUsuario") & ";"
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


                txtCorreoElectronico.Text = ""
                Response.Redirect("~/modulos/confi_perfil.aspx?action=cambiocorreo")
            End If
        End If
    End Sub
End Class