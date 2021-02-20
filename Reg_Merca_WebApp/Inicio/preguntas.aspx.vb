Public Class preguntas
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

    End Sub

    Private Sub bttverificar_Click(sender As Object, e As EventArgs) Handles bttverificar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_23_preguntas_usuario   where id_pregunta=" & cmbPreguntas.SelectedValue & " and id_usuario=" & Session("usuarioPreguntas") & " and respuesta = '" & txtrespuesta.Text & "';"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        Dim registro As DataRow
        If Session("NumReg") > 0 Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas','Respuesta correcta.', 'success');</script>")
            Response.Redirect("~/Inicio/preguntas.aspx?result=god")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas','Respuesta incorrecta.', 'error');</script>")
            Response.Redirect("~/Inicio/preguntas.aspx?result=error")
        End If
    End Sub
End Class