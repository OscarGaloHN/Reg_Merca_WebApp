Public Class cambio_contra
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
        If Session("id_usuarioPreguntas") = Nothing Or Session("usuarioCambioPW") <> True Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
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


        End If
    End Sub

    Private Sub lblcancelar_Click(sender As Object, e As EventArgs) Handles lblcancelar.Click
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx")
    End Sub

    Private Sub bttDesbloquear_Click(sender As Object, e As EventArgs) Handles bttDesbloquear.Click

        Dim Ssql As String = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where id_usuario = " & Session("id_usuarioPreguntas") & " and estado=3;"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  intentos =0, estado=1 where id_usuario = " & Session("id_usuarioPreguntas") & ";"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
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
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','No puede usar una contraseña igual a las usasdas anteriormente.', 'warning');</script>")
                            Case 2 'contraseña sin usar
                                Session.Abandon()
                                Response.Redirect("~/Inicio/login.aspx?acction=changepasswordout")
                        End Select
                    End If
                End If
            End If
        End If
    End Sub
End Class