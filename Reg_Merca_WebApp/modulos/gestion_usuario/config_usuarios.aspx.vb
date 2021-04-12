Public Class Config_Usuarios
    Inherits System.Web.UI.Page
    'OBJETO #7
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else

                Dim Ssql As String = String.Empty
                Session("user_rol") = 5
                Session("user_idUsuario") = 2

                If Session("user_rol") = 5 Then
                    Ssql = "select a.id_usuario, a.Nombre, b.rol, c.descripcion
                            from tbl_02_usuarios a, tbl_15_rol b, tbl_19_estado c
                               where a.id_rol = b.id_rol
                                and a.estado = c.id_estado and a.id_rol and a.id_usuario <> 1"
                Else
                    Ssql = "select a.id_usuario, a.Nombre, b.rol, c.descripcion
                            from tbl_02_usuarios a, tbl_15_rol b, tbl_19_estado c
                               where a.id_rol = b.id_rol
                                and a.estado = c.id_estado and a.id_rol != 5 and a.id_usuario <> 1"

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
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El Usuario se elimino exitosamente.', 'success');</script>")
                    Case "deleteinactive"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Este usuario no puede ser eliminado, su estado paso a inactivo.', 'warning');</script>")
                    Case "deletefailed"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Error inesperado, este usuario no puedo ser eliminado.', 'error');</script>")
                End Select

                'bitacora de que salio de un form
                If Not IsPostBack Then
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                    End Using
                End If

                'bitacora de que ingreso al form
                Session("IDfrmQueIngresa") = 7
                Session("NombrefrmQueIngresa") = "Gestión de usuarios"
                If Not IsPostBack Then
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                    End Using
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub bttNuevo_Click(sender As Object, e As EventArgs) Handles bttNuevo.Click
        Try
            Response.Redirect("~/modulos/gestion_usuario/config_gestion_usuario.aspx?action=new")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttEliminar_Click(sender As Object, e As EventArgs) Handles bttEliminar.Click
        Dim Ssql As String

        Try

            'pasar el nombre del usuario en la bitacora
            Ssql = "SELECT usuario FROM DB_Nac_Merca.tbl_02_usuarios where id_usuario= " & lblHidden1.Value & ";"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            Dim registro As DataRow
            If Session("NumReg") > 0 Then
                registro = DataSetX.Tables(0).Rows(0)
                lblUsuario.Text = registro("usuario")
            End If

            Ssql = "delete from DB_Nac_Merca.tbl_35_activacion_usuario  where id_usuario =  " & lblHidden1.Value & ""
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Ssql = "delete from DB_Nac_Merca.tbl_02_usuarios where id_usuario =  " & lblHidden1.Value & ""
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'bitacora para capturar eliminación de un usuario
            Using cusuario_bitacora As New ControlBitacora
                'log_bitacora.log_sesion_inicio(5, Session("user_idUsuario"), "" & txtUsuario.Text & " ya esta registrado")
                cusuario_bitacora.acciones_Comunes(6, Session("user_idUsuario"), 7, "" & lblUsuario.Text & " con codigo " & lblHidden1.Value & " se elimino exitosamente")
            End Using
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    'MessageBox.Show("Cannot connect to server. Contact administrator")
                Case 1451
                    Ssql = "update DB_Nac_Merca.tbl_02_usuarios set estado=3 where id_usuario =  " & lblHidden1.Value & ""
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        'bitacora para capturar inactivación de usuario
                        Using cusuario_bitacora As New ControlBitacora
                            cusuario_bitacora.acciones_Comunes(7, Session("user_idUsuario"), 7, "" & lblUsuario.Text & " con codigo " & lblHidden1.Value & " no puede ser eliminado, su estado paso a inactivo.")
                        End Using
                        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=deleteinactive")
                    End Using
                Case Else
                    Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=deletefailed")
            End Select
        End Try
        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=deleteusuer")

    End Sub
End Class
