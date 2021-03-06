Imports System.Net
Imports System.Net.Mail

Public Class registro
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
        btt_registrar.Focus()
    End Sub

    Private Sub btt_registrar_Click(sender As Object, e As EventArgs) Handles btt_registrar.Click
        Dim Ssql As String = "CALL autoregistro('" & txtUsuario.Text & "', '" & txtemail.Text & "')"
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
                    Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_02_usuarios` (`id_rol`,`usuario`, `nombre`,`estado`, `correo`,  `fecha_vencimiento`, `creado_por`, `fecha_creacion`, `intentos`, `emailconfir`) VALUES (6,'" & txtUsuario.Text & "', '" & txtnombre.Text & "',0, '" & txtemail.Text & "', null, 'Autoregistro',  CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), 0, 0);"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    SendActivationEmail()
                    Response.Redirect("~/Inicio/login.aspx?action=newsolicitud")
            End Select
        End If
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
                                         txtemail.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                       txtnombre.Text.Trim(),
                                         urllink, "Activación de Cuenta",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
            End Using

            'Using mm As New MailMessage("registrodemercanciahn@gmail.com", txtemail.Text)
            '    mm.Subject = "Activación de Cuenta"
            '    mm.From = New MailAddress("registrodemercanciahn@gmail.com", "RegMERCA")
            '    Dim body As String = "Hola " + txtnombre.Text.Trim() + ","
            '    body += "<br /><br />Para continuar con el registro haga click en el siguiente enlace y asi poder activar su cuenta."
            '    body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("registro", Convert.ToString("activacion.aspx?ActivationCode=") & activationCode) + "'>Click aqui para poder activar su cuenta.</a>"
            '    body += "<br /><br />Gracias"
            '    mm.Body = body
            '    mm.IsBodyHtml = True
            '    Dim smtp As New SmtpClient()
            '    smtp.Host = "smtp.gmail.com"
            '    smtp.EnableSsl = True
            '    Dim NetworkCred As New NetworkCredential("registrodemercanciahn@gmail.com", "mercancia2021")
            '    smtp.UseDefaultCredentials = True
            '    smtp.Credentials = NetworkCred
            '    smtp.Port = 587
            '    smtp.Send(mm)
            'End Using
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El registro no fue completado. Intentelo de nuevo.', 'error');</script>")
        End If
    End Sub

End Class