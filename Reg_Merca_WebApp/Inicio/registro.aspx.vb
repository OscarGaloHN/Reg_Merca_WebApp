Imports System.Net
Imports System.Net.Mail

Public Class registro
    'OBJETO #5
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


        'parametros de USUARIO
        valiUserLargo.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(16) & " -" & Application("ParametrosADMIN")(17) & ")."
        valiUserLargo.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(16) & "," & Application("ParametrosADMIN")(17) & "}$"
        txtUsuario.MaxLength = Application("ParametrosADMIN")(17)


        btt_registrar.Focus()
    End Sub

    Private Sub btt_registrar_Click(sender As Object, e As EventArgs) Handles btt_registrar.Click
        If IsValid Then
            Dim Ssql As String = "CALL autoregistro('" & txtUsuario.Text & "', '" & txtemail.Text & "')"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                Select Case registro("EXISTE")
                    Case -1 'usuario y correo existen
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(4, 1, 5, "El usuario " & txtUsuario.Text & " y correo " & txtemail.Text & " ya estan registrados")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario & Correo','El usuario y correo electronico ya estan registrados.', 'error');</script>")
                    Case -2 'usuario existe
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(4, 1, 5, "El usuario " & txtUsuario.Text & " ya esta registrado")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Nombre de usuario','El nombre de usuario ya esta registrado.', 'error');</script>")
                    Case -3 'correo existe
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(4, 1, 5, "El correo " & txtemail.Text & " ya esta registrado")
                        End Using
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo electronico','El correo electronico ya esta registrado.', 'error');</script>")
                    Case 0 'no existe
                        Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_02_usuarios` (`id_rol`,`usuario`, `nombre`,`estado`, `correo`,  `fecha_vencimiento`, `creado_por`, `fecha_creacion`, `intentos`, `emailconfir`) VALUES (6,'" & txtUsuario.Text & "', '" & txtnombre.Text & "',0, '" & txtemail.Text & "', null, 'Autoregistro',  CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), 0, 0);"
                        Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using
                        SendActivationEmail()
                        Response.Redirect("~/Inicio/login.aspx?action=newsolicitud")
                End Select
            End If
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
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(4, registro("id_usuario"), 5, "El usuario " & txtUsuario.Text & " fue autoregistrado")
            End Using
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El registro no fue completado. Intentelo de nuevo.', 'error');</script>")
        End If
    End Sub

End Class