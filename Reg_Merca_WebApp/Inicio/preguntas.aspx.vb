Public Class preguntas
    'OBJETO #9
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
        If Session("id_usuarioPreguntas") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
            Session("usuarioCambioPW") = False
            If Not IsPostBack Then
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("id_usuarioPreguntas"), 9, "El usuario ingresa a la pantalla de responder pregunta de seguridad para desbloquear usuario o cambiar contraseña")
                End Using
            End If
        End If
        bttverificar.Focus()

    End Sub

    Private Sub bttverificar_Click(sender As Object, e As EventArgs) Handles bttverificar.Click
        If IsValid Then
            Dim Ssql As String = String.Empty
            Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_23_preguntas_usuario   where id_pregunta=" & cmbPreguntas.SelectedValue & " and id_usuario=" & Session("id_usuarioPreguntas") & " and respuesta = '" & txtrespuesta.Text & "';"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            'Dim registro As DataRow
            If Session("NumReg") > 0 Then
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("id_usuarioPreguntas"), 9, "El usuario da una respuesta correcta a la pregunta de seguridad y es enviado a cambio de contraseña")
                End Using
                Session("usuarioCambioPW") = True
                Response.Redirect("~/Inicio/cambio_contra.aspx")
            Else
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("id_usuarioPreguntas"), 9, "El usuario da una respuesta incorrecta a la pregunta de seguridad")
                End Using
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas De Seguridad','Respuesta incorrecta.', 'error');</script>")
            End If
        End If
    End Sub

    Private Sub lblcancelar_Click(sender As Object, e As EventArgs) Handles lblcancelar.Click
        Session.Abandon()
        Response.Redirect("~/Inicio/login.aspx")
    End Sub
End Class