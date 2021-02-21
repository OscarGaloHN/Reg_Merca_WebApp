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
                    'Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_02_usuarios` (`usuario`, `nombre`, `correo`,  `fecha_vencimiento`, `creado_por`, `fecha_creacion`, `intentos`, `emailconfir`) VALUES ('" & txtUsuario.Text & "', '" & txtnombre.Text & "', '" & txtemail.Text & "', null, 'Autoregistro',  CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), 0, 0);"
                    'Using con As New ControlDB
                    '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    'End Using
                    SendActivationEmail()
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Registro','Exitoso.', 'success');</script>")
            End Select
        End If
    End Sub

    Private Sub SendActivationEmail()
        Dim activationCode As String = Guid.NewGuid().ToString()
        'Dim Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`) VALUES ('3', '" & activationCode & "');"
        'Using con As New ControlDB
        '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        'End Using
        Using mm As New MailMessage("registrodemercanciahn@gmail.com", txtemail.Text)
            mm.Subject = "Activación de Cuenta"
            mm.From = New MailAddress("registrodemercanciahn@gmail.com", "RegMERCA")
            Dim body As String = "Hola " + txtnombre.Text.Trim() + ","
            body += "<br /><br />Please click the following link to activate your account"
            body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace("registro", Convert.ToString("VB_Activation.aspx?ActivationCode=") & activationCode) + "'>Click here to activate your account.</a>"
            body += "<br /><br />Thanks"
            mm.Body = body
            mm.IsBodyHtml = True
            Dim smtp As New SmtpClient()
            smtp.Host = "smtp.gmail.com"
            smtp.EnableSsl = True
            Dim NetworkCred As New NetworkCredential("registrodemercanciahn@gmail.com", "mercancia2021")
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = 587
            smtp.Send(mm)
        End Using
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