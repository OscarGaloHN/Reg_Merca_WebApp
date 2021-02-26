Public Class confi_perfil
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

        Dim Ssql As String = String.Empty
        Ssql = "SELECT * FROM DB_Nac_Merca.tbl_22_preguntas;"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
            If Session("NumReg") > 0 Then
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            End If
        End Using
    End Sub

End Class