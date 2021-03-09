Public Class Config_Usuarios
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
        Session("user_rol") = 5

        If Session("user_rol") = 5 Then
            Ssql = "select a.id_usuario, a.Nombre, b.rol, c.descripcion
                            from tbl_02_usuarios a, tbl_15_rol b, tbl_19_estatus c
                               where a.id_rol = b.id_rol
                                and a.estado = c.id_estado and a.id_rol"

        Else
            Ssql = "select a.id_usuario, a.Nombre, b.rol, c.descripcion
                            from tbl_02_usuarios a, tbl_15_rol b, tbl_19_estatus c
                               where a.id_rol = b.id_rol
                                and a.estado = c.id_estado and a.id_rol != 5"

        End If


        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            gvCustomers.DataSource = DataSetX
            gvCustomers.DataBind()
        End If

        Select Case Request.QueryString("action")
            Case "deleteusuer"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El Usuario se elemino exitosamente.', 'success');</script>")
            Case "deleteinactive"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Este usuario no puede ser eliminao, pero paso a ser inactivado.', 'warning');</script>")
            Case "deletefailed"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Error inesperado, este usuario no puedo ser eliminao.', 'error');</script>")
        End Select

    End Sub

    Private Sub bttNuevo_Click(sender As Object, e As EventArgs) Handles bttNuevo.Click
        Response.Redirect("~/modulos/gestion_usuario/config_gestion_usuario.aspx?action=new")
    End Sub

    Private Sub bttEliminar_Click(sender As Object, e As EventArgs) Handles bttEliminar.Click
        Dim Ssql As String = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where id_usuario =  " & lblHidden1.Value & ""
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using

        Try
            Ssql = "delete from DB_Nac_Merca.tbl_02_usuarios where id_usuario =  " & lblHidden1.Value & ""
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=deleteusuer")

        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    'MessageBox.Show("Cannot connect to server. Contact administrator")
                Case 1451
                    Ssql = "update DB_Nac_Merca.tbl_02_usuarios set estado=3 where id_usuario =  " & lblHidden1.Value & ""
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=deleteinactive")
                Case Else
                    Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=deletefailed")
            End Select
        End Try

    End Sub
End Class