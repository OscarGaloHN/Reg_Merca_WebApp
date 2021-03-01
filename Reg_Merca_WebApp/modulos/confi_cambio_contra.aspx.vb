Public Class confi_cambio_contra
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
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','No puede usar una contraseña igual a las usasdas anteriormente.', 'warning');</script>")
                        Case 2 'contraseña sin usar
                            Session.Abandon()
                            Response.Redirect("~/Inicio/login.aspx?action=changepasswordout")
                    End Select
                End If
            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña no valida','Su contraseña actual no es correcta.', 'error');</script>")
            End If
        End If
    End Sub
End Class