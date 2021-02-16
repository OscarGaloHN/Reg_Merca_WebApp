Public Class recuperar
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
        bttPreguntas.Attributes.Add("onClick", "return false;")
    End Sub

    Private Sub bttContinuar_Click(sender As Object, e As EventArgs) Handles bttContinuar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuarioPreguntas.Text & "';"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        'Dim registro As DataRow
        If Session("NumReg") > 0 Then
            Session("usuarioPreguntas") = txtUsuarioPreguntas.Text
            Response.Redirect("~/Inicio/preguntas_seguridad.aspx")
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('EXITO','Usuario  encontrado', 'success');</script>")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Advertencia','Usuario no encontrado', 'warning');</script>")
        End If
    End Sub
End Class