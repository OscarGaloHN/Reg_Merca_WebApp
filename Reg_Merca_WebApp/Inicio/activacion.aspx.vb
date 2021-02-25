Imports System.Net
Imports System.Net.Mail

Public Class activacion
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
        If Not Me.IsPostBack Then
            Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), Guid.Empty.ToString())
            Dim Ssql As String = "SELECT T01.*, T02.usuario FROM DB_Nac_Merca.tbl_35_activacion_usuario T01 left join DB_Nac_Merca.tbl_02_usuarios T02 on T01.id_usuario = T02.id_usuario where T01.codigo_activacion =  '" & activationCode & "'"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'comprobar que no este vencido
                Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                If CDate(registro("vencimiento")) > DateTime.Now Then
                    Select Case registro("tipo")
                        Case "registro" 'usuario nuevo, confirmar email, cambio de clave.
                            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  emailconfir = 1 where usuario='" & registro("usuario") & "';"
                            Using con As New ControlDB
                                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            End Using

                            Page.Title = "Activación de Cuenta"
                            lblSaludo.Text = "Hola"
                            lblUsuario.Text = registro("usuario")
                            ltMessage.Text = "Hemos verificado su correo electronico, ahora es necesario crear una contraseña."
                            PanelConfirmar.Visible = True
                            PanelConfirmar.DefaultButton = "bttContra"
                            bttContra.Visible = True

                        Case "clave" 'cambio de contraseña 
                            Page.Title = "Cambio de Contraseña"
                            lblSaludo.Text = "Hola"
                            lblUsuario.Text = registro("usuario")
                            ltMessage.Text = "Hemos verificado su solicitud, ahora es necesario crear una nueva contraseña."
                            PanelConfirmar.Visible = True
                            PanelConfirmar.DefaultButton = "bttCambiarContra"
                            bttCambiarContra.Visible = True
                    End Select
                Else
                    PanelCaducada.Visible = True
                    lblcaduca.Text = "Su solicitud a cadacudao, debe de realizar una nueva solicitud de configuración."
                    Page.Title = "Solicitud Caducada"
                End If
            Else
                PanelError.Visible = True
                lblerror.Text = "Este intento de validación no es valido."
                Page.Title = "Solicitud Invalida"

            End If
        End If
    End Sub

    Private Sub bttContra_Click(sender As Object, e As EventArgs) Handles bttContra.Click
        Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), Guid.Empty.ToString())
        If PanelConfirmar.Visible = True Then
            Dim Ssql As String = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                'pendiente de editar la fecha de vencimiento
                Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  clave = SHA('" & txtContraConfirmar.Text & "'), estado = 1 where id_usuario=" & registro("id_usuario") & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using

                Ssql = "insert into DB_Nac_Merca.tbl_20_historico_claves (`id_usuario`, `clave`, `fecha`) VALUES(" & registro("id_usuario") & " , SHA('" & txtContraConfirmar.Text & "'), CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'));"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using

                Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Response.Redirect("~/Inicio/login.aspx?acction=activateuser")
            End If
        End If
    End Sub

    Private Sub lblcancelar_Click(sender As Object, e As EventArgs) Handles lblcancelar.Click
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx")
    End Sub

    Private Sub bttNuevaSolicitud_Click(sender As Object, e As EventArgs) Handles bttNuevaSolicitud.Click
        Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), "0")
        Dim Ssql As String = "SELECT T01.*, T02.correo, T02.nombre FROM DB_Nac_Merca.tbl_35_activacion_usuario T01 left join DB_Nac_Merca.tbl_02_usuarios T02 on T01.id_usuario = T02.id_usuario where T01.codigo_activacion =  '" & activationCode & "'"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Dim registro As DataRow = DataSetX.Tables(0).Rows(0)

            Dim activationCodeNuevo As String = Guid.NewGuid().ToString()
            Ssql = "UPDATE `DB_Nac_Merca`.`tbl_35_activacion_usuario` SET codigo_activacion= '" & activationCodeNuevo & "', vencimiento= DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL 2 DAY);"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Using mm As New MailMessage("registrodemercanciahn@gmail.com", registro("correo"))
                mm.Subject = "Activación de Cuenta(Nueva Solicitud)"
                mm.From = New MailAddress("registrodemercanciahn@gmail.com", "RegMERCA")
                Dim body As String = "Hola " + registro("nombre") + ","
                body += "<br /><br />Para continuar con el registro haga click en el siguiente enlace y asi poder activar su cuenta."
                body += "<br /><a href = '" + Request.Url.AbsoluteUri.Replace(activationCode, activationCodeNuevo) + "'>Click aqui para poder activar su cuenta.</a>"
                body += "<br /><br />Gracias"
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
            Response.Redirect("~/Inicio/login.aspx?acction=newsolicitud")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Solicitud','No fue posible realizar la solicitud, contacte al adminstrador.', 'error');</script>")
        End If
    End Sub

    Private Sub bttCambiarContra_Click(sender As Object, e As EventArgs) Handles bttCambiarContra.Click
        Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), Guid.Empty.ToString())
        If PanelConfirmar.Visible = True Then
            Dim Ssql As String = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                'pendiente de editar la fecha de vencimiento
                Ssql = "CALL contrasenas(" & registro("id_usuario") & ", SHA('" & txtContraConfirmar.Text & "'))"
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    registro = DataSetX.Tables(0).Rows(0)
                    Select Case registro("repetida")
                        Case 1 'contraseña ya usada
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','No puede usar una contraseña igual a las usasdas anteriormente.', 'warning');</script>")
                        Case 2 'contraseña sin usar
                            Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"
                            Using con As New ControlDB
                                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            End Using
                            Session.Abandon()
                            Response.Redirect("~/Inicio/login.aspx?acction=changepasswordout")
                    End Select
                End If
            End If
        End If
    End Sub
End Class