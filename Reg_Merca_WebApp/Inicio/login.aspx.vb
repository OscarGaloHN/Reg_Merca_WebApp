
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
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_21_parametros WHERE parametro like '%SYS%' order by 1;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            Dim registro As DataRow
            If Session("NumReg") > 0 Then
                Dim arrayParametros(CInt(Session("NumReg")) - 1) As String
                For i = 0 To arrayParametros.Length - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    'arrayParametros(i) = registro("valor")
                    If IsDBNull(registro("valor")) = False Then
                        arrayParametros(i) = registro("valor")
                    End If
                Next

                'parametros de contraseña
                Application("ParametrosSYS") = arrayParametros
                reContraLogin.ErrorMessage = "El rango de caracteres debe de ser entre (5 -" & Application("ParametrosSYS")(0) & ")."
                reContraLogin.ValidationExpression = "^[\s\S]{5," & Application("ParametrosSYS")(0) & "}$"
                txtContra.MaxLength = Application("ParametrosSYS")(0)
            End If
            'PARAMETROS DE ADMINISTRADOR
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_21_parametros WHERE parametro like '%ADMIN%' order by 1;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim arrayParametrosADMIN(CInt(Session("NumReg")) - 1) As String
                For i = 0 To arrayParametrosADMIN.Length - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    'arrayParametros(i) = registro("valor")
                    If IsDBNull(registro("valor")) = False Then
                        arrayParametrosADMIN(i) = registro("valor")
                    End If
                Next
                Application("ParametrosADMIN") = arrayParametrosADMIN
            End If
        End If
        bttEntrar.Focus()

    End Sub

    Private Sub bttEntrar_Click(sender As Object, e As EventArgs) Handles bttEntrar.Click
        'Dim Ssql As String = String.Empty
        'Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "' and clave = SHA('" & txtContra.Text & "');"
        'Using con As New ControlDB
        '    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        '    Session("NumReg") = DataSetX.Tables(0).Rows.Count
        'End Using
        'Dim registro As DataRow
        'If Session("NumReg") > 0 Then
        '    'Si coloco las credenciales correctas
        '    registro = DataSetX.Tables(0).Rows(0)
        '    Select Case CInt(registro("estado"))
        '        Case 0 'Formulario de registro o usuario nuevo

        '        Case 1 'activo
        '            'CARGAR DATOS DE USUARIO
        '            If Session("NumReg") > 0 Then
        '                registro = DataSetX.Tables(0).Rows(0)
        '                'CARGAR DATOS DE USUARIO
        '                Session("user_usuario") = registro("usuario")
        '                Session("user_nombre") = registro("nombre")
        '                Session("user_correo") = registro("correo")
        '                Session("user_rol") = registro("id_rol")
        '            End If

        '            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  fecha_ultima_conexion = CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), intentos=0, en_linea=1 where usuario = BINARY  '" & txtUsuario.Text & "';"
        '            Using con As New ControlDB
        '                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        '            End Using
        '            If CBool(Application("ParametrosSYS")(3)) = True Then
        '                'si el sitio esta configurado
        '                Response.Redirect("~/modulos/principal.aspx")
        '            Else
        '                Select Case CInt(Session("user_rol"))
        '                    Case 5 'si el rol es admin
        '                        Response.Redirect("~/modulos/configurar.aspx")
        '                    Case Else 'si no es admin
        '                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuración','El administrador no ha completado la configuración del sistema.', 'warning');</script>")
        '                        Session.Abandon()

        '                End Select
        '            End If
        '        Case 2 'bloqueado o inactivo
        '        Case 3 'bloqueo por intentos
        '            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'warning');</script>")
        '        Case 4 'usuario caducado
        '    End Select
        'Else
        '    Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuario.Text & "';"
        '    Using con As New ControlDB
        '        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        '        Session("NumReg") = DataSetX.Tables(0).Rows.Count
        '    End Using
        '    If Session("NumReg") > 0 Then
        '        registro = DataSetX.Tables(0).Rows(0)
        '        Select Case CInt(registro("intentos"))
        '            Case 3
        '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','Usuario Bloqueado, Contactece con el administrador.', 'error');</script>")
        '            Case Else
        '                If CInt(registro("intentos")) + 1 = CInt(Application("ParametrosADMIN")(6)) Then 'PARAMETRO INTENTOS DE CONTRASEÑA
        '                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & ", estado=3 where usuario = BINARY  '" & txtUsuario.Text & "';"
        '                Else
        '                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =" & CInt(registro("intentos")) + 1 & " where usuario = BINARY  '" & txtUsuario.Text & "';"
        '                End If
        '                Using con As New ControlDB
        '                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        '                End Using
        '                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
        '        End Select
        '    Else
        '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Usuario o Contraseña Incorrectos.', 'error');</script>")
        '    End If
        'End If


        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()
        SmtpServer.Credentials = New Net.NetworkCredential("registrodemercanciahn@gmail.com", "mercancia2021")
        SmtpServer.Port = 25
        SmtpServer.Host = "smtp.gmail.com"
        SmtpServer.EnableSsl = True
        mail = New MailMessage()
        mail.From = New MailAddress("registrodemercanciahn@gmail.com", "RegMERCA")
        mail.To.Add("oscaramador7@gmail.com")
        mail.Subject = "Prueba del titulo"
        mail.IsBodyHtml = True
        mail.Body = "hola hoy es 16/2/2021"
        SmtpServer.Send(mail)
    End Sub



End Class