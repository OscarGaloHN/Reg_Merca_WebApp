Public Class menu_principal
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

        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        ElseIf Session("user_rol") = 6 Then
            'no tiene rol
            Response.Redirect("~/modulos/confi_rol.aspx")
        Else
            'si tiene acceso al sistema
            Dim Ssql As String = " Select * From DB_Nac_Merca.tbl_03_permisos Where id_rol = " & Session("user_rol") & ""
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            Dim registro As DataRow
            Dim arrayPermisos(0) As Integer

            If Session("NumReg") > 0 Then
                'ReDim arrayPermisos(Session("NumReg") - 1)
                For i = 0 To Session("NumReg") - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    Select Case registro("id_objeto")
                        Case 1
                            PanelConfi.Visible = True
                    End Select
                Next
            End If
        End If
    End Sub
End Class