
Public Class confi_perfil_preguntas
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
        SqlPreguntas.SelectCommand = "SELECT id_pregunta, UPPER(pregunta) pregunta FROM DB_Nac_Merca.tbl_22_preguntas
        WHERE id_pregunta Not in (select id_pregunta FROM DB_Nac_Merca.tbl_23_preguntas_usuario where id_usuario = " & Session("user_idUsuario") & ")  order by 1;"
        Dim Ssql As String = String.Empty
        Ssql = "select T01.id_pregunta_usuario ID, T02.pregunta, T01.respuesta from DB_Nac_Merca.tbl_23_preguntas_usuario  T01
                left join DB_Nac_Merca.tbl_22_preguntas  T02 ON T01.id_pregunta = T02.id_pregunta WHERE id_usuario=" & Session("user_idUsuario") & ""
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using

        lblprguntas.Text = Session("NumReg")
        lblpreguntasrequeridas.Text = Application("ParametrosADMIN")(8)
        If Session("NumReg") > 0 Then
            gvCustomers.DataSource = DataSetX
            gvCustomers.DataBind()
        Else
            gvCustomers.DataSource = DataSetX
            gvCustomers.DataBind()
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','sin preguntas', 'warning');</script>")
        End If

        If Session("NumReg") >= Application("ParametrosADMIN")(8) Then
            PanelPregunta.Visible = False
        Else
            PanelPregunta.Visible = True
        End If
    End Sub

End Class