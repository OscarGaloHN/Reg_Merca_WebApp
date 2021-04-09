Public Class config_permisos
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #27
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'parametros de configuracion de sistema
            Using Parametros_Sistema As New ControlDB
                Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
            End Using

            'PARAMETROS DE ADMINISTRADOR
            Using Parametros_admin As New ControlDB
                Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
            End Using

            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
            'llenar grid

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlRoles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoles.SelectedIndexChanged

        Dim Ssql As String = String.Empty
            Ssql = "SELECT T001.id_permiso_modulo , T002.nombre , T003.rol
                FROM DB_Nac_Merca.tbl_37_permisos_modulos T001 
                LEFT JOIN DB_Nac_Merca.tbl_36_modulos T002 ON T001.id_modulo= T002.id_modulo
                LEFT JOIN DB_Nac_Merca.tbl_15_rol T003 ON T001.id_rol = T003.id_rol where T001.id_rol = " & ddlRoles.SelectedValue & " ;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            End If
            SqlModulos.SelectCommand = "SELECT 0  AS id_modulo, 'Seleccionar Módulo...' AS nombre UNION ALL 
        SELECT id_modulo, nombre  FROM DB_Nac_Merca.tbl_36_modulos T001
        WHERE id_modulo NOT IN (SELECT *FROM (SELECT id_modulo FROM DB_Nac_Merca.tbl_37_permisos_modulos  WHERE id_rol = " & ddlRoles.SelectedValue & ")
        TBL_NO_MODULOS)"

    End Sub
End Class