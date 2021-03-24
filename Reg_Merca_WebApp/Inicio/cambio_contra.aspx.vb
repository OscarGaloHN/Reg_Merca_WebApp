Public Class cambio_contra
    'OBJETO #10
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
        Try
            If Session("id_usuarioPreguntas") = Nothing Or Session("usuarioCambioPW") <> True Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else

                If Not IsPostBack Then
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(9, Session("id_usuarioPreguntas"), 10, "El usuario ingresa a la pantalla de desbloquear usuario o cambiar contraseña")
                    End Using
                End If
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
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lblcancelar_Click(sender As Object, e As EventArgs) Handles lblcancelar.Click
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx")
    End Sub

    Private Sub bttDesbloquear_Click(sender As Object, e As EventArgs) Handles bttDesbloquear.Click

        Dim Ssql As String = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where id_usuario = " & Session("id_usuarioPreguntas") & " and estado=4;"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =0, estado=2 where id_usuario = " & Session("id_usuarioPreguntas") & ";"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(5, Session("id_usuarioPreguntas"), 10, "El usuario se desbloqueo con exito")
            End Using
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Desbloqueo','Su Usuario ha sido desbloqueado.', 'success');</script>")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Desbloqueo','El usuario no se encuentra bloqueado.', 'warning');</script>")
        End If
    End Sub

    Private Sub bttCambiar_Click(sender As Object, e As EventArgs) Handles bttCambiar.Click
        If IsValid Then
            Dim Ssql As String = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where id_usuario =  " & Session("id_usuarioPreguntas") & " and estado = 4;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario Bloqueado','Para cambiar su contraseña, debe desbloquear su usuario.', 'warning');</script>")
            Else
                If UCase(txtContra.Text) = UCase(Session("nombre_usuario_comparar")) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña no valida','La contraseña no puede ser igual a su nombre de usuario.', 'error');</script>")
                Else

                    Ssql = "CALL contrasenas(" & Session("id_usuarioPreguntas") & ", SHA('" & txtContraConfirmar.Text & "'))"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                        Select Case registro("repetida")
                            Case 1 'contraseña ya usada
                                Using log_bitacora As New ControlBitacora
                                    log_bitacora.acciones_Comunes(5, Session("id_usuarioPreguntas"), 10, "No se permite el cambio de contraseña, porque la nueva contraseña ya fue usada anteriormente")
                                End Using
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','No puede usar una contraseña igual a las usasdas anteriormente.', 'warning');</script>")
                            Case 2 'contraseña sin usar
                                'la contraseña se actualiza en el SP
                                Using log_bitacora As New ControlBitacora
                                    log_bitacora.acciones_Comunes(5, Session("id_usuarioPreguntas"), 10, "El cambio de contraseña fue exitoso")
                                End Using

                                Using log_bitacora As New ControlBitacora
                                    log_bitacora.acciones_Comunes(4, Session("id_usuarioPreguntas"), 10, "Se crea historico de cambio de contraseña con exito")
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