Public Class confi_cambio_contra
    Inherits System.Web.UI.Page
    'OBJETO #14
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
            'contra actual
            txtContraactual.MaxLength = Application("ParametrosADMIN")(0)

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
            If Not IsPostBack Then
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 14, "El usuario ingresa a la pantalla cambio de contraseña en el perfil de usuario")
                End Using
            End If
        End If
    End Sub

    Private Sub bttCambiar_Click(sender As Object, e As EventArgs) Handles bttCambiar.Click
        If UCase(txtContra.Text) = UCase(Session("user_nombre_usuario")) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña no valida','La contraseña no puede ser igual a su nombre de usuario.', 'error');</script>")
        Else
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_02_usuarios  
                WHERE id_usuario = " & Session("user_idUsuario") & " and clave = SHA('" & txtContraactual.Text & "');"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then

                Ssql = "CALL contrasenas(" & Session("user_idUsuario") & ", SHA('" & txtContraConfirmar.Text & "'))"
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                    Select Case registro("repetida")
                        Case 1 'contraseña ya usada
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 14, "No se permite el cambio de contraseña, porque la nueva contraseña ya fue usada anteriormente")
                            End Using
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','No puede usar una contraseña igual a las usasdas anteriormente.', 'warning');</script>")
                        Case 2 'contraseña sin usar
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 14, "El cambio de contraseña fue exitoso")
                            End Using

                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), 14, "Se crea historico de cambio de contraseña con exito")
                            End Using

                            Session.Abandon()
                            Response.Redirect("~/Inicio/login.aspx?action=changepasswordout")
                    End Select
                End If
            Else
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 14, "El usuario ingresa incorrectamente su contraseña actual")
                End Using
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña no valida','Su contraseña actual no es correcta.', 'error');</script>")
            End If
        End If
    End Sub
End Class