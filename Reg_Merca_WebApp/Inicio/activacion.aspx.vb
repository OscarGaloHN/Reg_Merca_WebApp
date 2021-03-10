﻿Imports System.Net
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
        'parametros de configuracion de sistema
        Using Parametros_Sistema As New ControlDB
            Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        End Using

        'PARAMETROS DE ADMINISTRADOR
        Using Parametros_admin As New ControlDB
            Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        End Using


        'parametros de contraseña
        reContra.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        reContra.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
        txtContra.MaxLength = Application("ParametrosADMIN")(0)

        'parametros de contraseña confirmar
        reContraConfirmar.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        reContraConfirmar.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
        txtContraConfirmar.MaxLength = Application("ParametrosADMIN")(0)

        'parametros de contraseña robusta
        validadorContraRobusta.ErrorMessage = "La contraseña debe contener 1 letra minuscula, 1 letra mayuscula, 1 carácter especial, 1 numero y el rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        validadorContraRobusta.ValidationExpression = "^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"


        If Not Me.IsPostBack Then
            'si vine desde el frm login
            Select Case Request.QueryString("acction")
                Case "activateuser"
                    Dim Ssql As String = "SELECT T01.*, T02.usuario FROM DB_Nac_Merca.tbl_35_activacion_usuario T01 left join DB_Nac_Merca.tbl_02_usuarios T02 on T01.id_usuario = T02.id_usuario where T01.id_usuario =  " & Request.QueryString("userregis") & " and tipo='registro'"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                        If CDate(registro("vencimiento")) > DateTime.Now Then
                            Page.Title = "Activación de Cuenta"
                            lblSaludo.Text = "Hola"
                            lblUsuario.Text = registro("usuario")
                            ltMessage.Text = "hemos verificado su solicitud, ahora es necesario crear una contraseña."
                            PanelConfirmar.Visible = True
                            PanelConfirmar.DefaultButton = "bttContra"
                            bttContra.Visible = True
                            bttContra.Focus()
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
                Case Else
                    'si viene desde el correo electronico
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
                                    ltMessage.Text = "hemos verificado su correo electronico, ahora es necesario crear una contraseña."
                                    PanelConfirmar.Visible = True
                                    PanelConfirmar.DefaultButton = "bttContra"
                                    bttContra.Visible = True
                                    bttContra.Focus()

                                Case "clave" 'cambio de contraseña 
                                    Page.Title = "Cambio de Contraseña"
                                    lblSaludo.Text = "Hola"
                                    lblUsuario.Text = registro("usuario")
                                    ltMessage.Text = "hemos verificado su solicitud, ahora es necesario crear una nueva contraseña."
                                    PanelConfirmar.Visible = True
                                    PanelConfirmar.DefaultButton = "bttCambiarContra"
                                    bttCambiarContra.Visible = True
                                    bttCambiarContra.Focus()

                                Case "correo" 'cambio de contraseña 
                                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  emailconfir = 1 where usuario='" & registro("usuario") & "';"
                                    Using con As New ControlDB
                                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                    End Using

                                    Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"
                                    Using con As New ControlDB
                                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                    End Using
                                    Session.Abandon()
                                    Response.Redirect("~/Inicio/login.aspx?action=activateemail")
                            End Select
                        Else
                            PanelCaducada.Visible = True
                            Select Case registro("tipo")
                                Case "registro"
                                    lblcaduca.Text = "Su solicitud a cadacudao, debe de realizar una nueva solicitud de configuración."
                                Case "clave"
                                    lblcaduca.Text = "Su solicitud a cadacudao, debe de realizar una nueva solicitud de cambio de contraseña."
                                Case "correo"
                                    lblcaduca.Text = "Su solicitud a cadacudao, debe de realizar una nueva solicitud de confirmación de correo electrónico."

                            End Select
                            Page.Title = "Solicitud Caducada"
                        End If
                    Else
                        PanelError.Visible = True
                        lblerror.Text = "Este intento de validación no es valido."
                        Page.Title = "Solicitud Invalida"
                    End If
            End Select
        End If
    End Sub

    Private Sub bttContra_Click(sender As Object, e As EventArgs) Handles bttContra.Click
        If UCase(txtContra.Text) = UCase(lblUsuario.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña no valida','La contraseña no puede ser igual a su nombre de usuario.', 'error');</script>")
        Else
            Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), Guid.Empty.ToString())
            Dim Ssql As String = ""
            Select Case Request.QueryString("acction")
                Case "activateuser"
                    Ssql = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where id_usuario =  " & Request.QueryString("userregis") & ""

                Case Else
                    Ssql = "SELECT *FROM DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"

            End Select

            If PanelConfirmar.Visible = True Then
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    Dim registro As DataRow = DataSetX.Tables(0).Rows(0)

                    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  clave = SHA('" & txtContraConfirmar.Text & "'), estado = 1 ,fecha_vencimiento = DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosADMIN")(12) & " DAY ) where id_usuario=" & registro("id_usuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Ssql = "insert into DB_Nac_Merca.tbl_20_historico_claves (`id_usuario`, `clave`, `fecha`) VALUES(" & registro("id_usuario") & " , SHA('" & txtContraConfirmar.Text & "'), CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'));"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Select Case Request.QueryString("acction")
                        Case "activateuser"
                            Ssql = "update DB_Nac_Merca.tbl_35_activacion_usuario set tipo='correo', vencimiento= DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(9) & " DAY) where id_usuario =  " & Request.QueryString("userregis") & " "
                        Case Else
                            Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "'"
                    End Select

                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Session.Abandon()
                    Response.Redirect("~/Inicio/login.aspx?action=activateuser")
                End If
            End If
        End If
    End Sub

    Private Sub lblcancelar_Click(sender As Object, e As EventArgs) Handles lblcancelar.Click
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx")
    End Sub

    Private Sub bttNuevaSolicitud_Click(sender As Object, e As EventArgs) Handles bttNuevaSolicitud.Click
        Dim Ssql As String '= "SELECT T01.*, T02.correo, T02.nombre FROM DB_Nac_Merca.tbl_35_activacion_usuario T01 left join DB_Nac_Merca.tbl_02_usuarios T02 on T01.id_usuario = T02.id_usuario where T01.codigo_activacion =  '" & activationCode & "'"
        Dim urllink As String = ""
        Dim activationCodeNuevo As String = Guid.NewGuid().ToString()
        Select Case Request.QueryString("acction")
            Case "activateuser"
                Ssql = "SELECT T01.*, T02.usuario,T02.correo, T02.nombre FROM DB_Nac_Merca.tbl_35_activacion_usuario T01 left join DB_Nac_Merca.tbl_02_usuarios T02 on T01.id_usuario = T02.id_usuario where T01.id_usuario =  " & Request.QueryString("userregis") & " and tipo='registro'"
                urllink = Request.Url.AbsoluteUri.Replace("?acction=activateuser&userregis=" & Request.QueryString("userregis"), "?ActivationCode=" & activationCodeNuevo)
            Case Else
                Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), "0")
                Ssql = "SELECT T01.*, T02.correo, T02.nombre FROM DB_Nac_Merca.tbl_35_activacion_usuario T01 left join DB_Nac_Merca.tbl_02_usuarios T02 on T01.id_usuario = T02.id_usuario where T01.codigo_activacion =  '" & activationCode & "'"
                urllink = Request.Url.AbsoluteUri.Replace(activationCode, activationCodeNuevo)
        End Select

        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using

        If Session("NumReg") > 0 Then
            Dim registro As DataRow = DataSetX.Tables(0).Rows(0)

            Select Case registro("tipo")
                Case "registro"
                    Ssql = "UPDATE `DB_Nac_Merca`.`tbl_35_activacion_usuario` SET codigo_activacion= '" & activationCodeNuevo & "', vencimiento= DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(7) & " DAY) where id_usuario=" & registro("id_usuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Using xCorreo As New ControlCorreo
                        xCorreo.envio_correo("Para continuar con el registro haga click en el siguiente enlace y asi poder activar su cuenta. ", "Continuar Registro",
                                         registro("correo"), Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                         registro("nombre"),
                                         urllink, "Activación de Cuenta(Nueva Solicitud)",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                    End Using


                Case "clave"
                    Ssql = "UPDATE `DB_Nac_Merca`.`tbl_35_activacion_usuario` SET codigo_activacion= '" & activationCodeNuevo & "', vencimiento= DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(8) & " DAY) where id_usuario=" & registro("id_usuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Using xCorreo As New ControlCorreo
                        xCorreo.envio_correo("Recibimos una solicitud para restablecer su contraseña. Si no realizó esta solicitud, ignore esta notificación. De lo contrario, puede restablecer su contraseña mediante este enlace. ", "RESTABLECER CONTRASEÑA",
                                         registro("correo"), Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                         registro("nombre"),
                                         urllink, "Restablecer Contraseña",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                    End Using

                Case "correo"
                    Ssql = "UPDATE `DB_Nac_Merca`.`tbl_35_activacion_usuario` SET codigo_activacion= '" & activationCodeNuevo & "', vencimiento= DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(9) & " DAY) where id_usuario=" & registro("id_usuario") & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    Using xCorreo As New ControlCorreo
                        xCorreo.envio_correo("Recibimos una solicitud para confirmar su correo electrónico. Si no realizó esta solicitud, ignore esta notificación. De lo contrario, puede confirmar su correo electrónico mediante este enlace. ", "CONFIRMAR",
                                         registro("correo"), Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                         registro("nombre"),
                                         urllink, "Confirmar Correo Electrónico",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
                    End Using
            End Select
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx?action=newsolicitud")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Solicitud','No fue posible realizar la solicitud, contacte al adminstrador.', 'error');</script>")
        End If
    End Sub

    Private Sub bttCambiarContra_Click(sender As Object, e As EventArgs) Handles bttCambiarContra.Click
        If UCase(txtContra.Text) = UCase(lblUsuario.Text) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña no valida','La contraseña no puede ser igual a su nombre de usuario.', 'error');</script>")
        Else
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
                                Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where codigo_activacion =  '" & activationCode & "' and tipo='clave'"
                                Using con As New ControlDB
                                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                End Using

                                Session.Abandon()
                                Response.Redirect("~/Inicio/login.aspx?action=changepasswordout")
                        End Select
                    End If
                End If
            End If
        End If
    End Sub
End Class