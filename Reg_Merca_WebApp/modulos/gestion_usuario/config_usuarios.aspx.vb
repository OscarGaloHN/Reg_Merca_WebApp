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

        'parametros de configuracion de sistema
        Using Parametros_Sistema As New ControlDB
            Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        End Using

        'PARAMETROS DE ADMINISTRADOR
        Using Parametros_admin As New ControlDB
            Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        End Using
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            Response.Redirect("~/Inicio/login.aspx")
        Else
            'codigo que corresponde al envento load
        End If

        If Not IsPostBack Then
            Using cusuario_bitacora As New ControlBitacora
                cusuario_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 7, "El usuario ingresa a la pantalla de configuracion de usuarios")
            End Using
        End If

        Dim Ssql As String = String.Empty
        Session("user_rol") = 5
        Session("user_idUsuario") = 2

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
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El Usuario se elimino exitosamente.', 'success');</script>")
            Case "deleteinactive"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Este usuario no puede ser eliminado, su estado paso a inactivo.', 'warning');</script>")
            Case "deletefailed"
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Error inesperado, este usuario no puedo ser eliminado.', 'error');</script>")
        End Select

        'pasar el nombre del usuario en la bitacora
        'Ssql = "SELECT usuario FROM DB_Nac_Merca.tbl_02_usuarios where id_usuario= " & Session("user_idUsuario") & ";"
        'Using con As New ControlDB
        '    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        '    Session("NumReg") = DataSetX.Tables(0).Rows.Count
        'End Using
        'Dim registro As DataRow
        'If Session("NumReg") > 0 Then
        '    registro = DataSetX.Tables(0).Rows(0)
        '    lblHidden1.Value = registro("usuario")
        'End If

        'ocultar NOUSUARIO
        'If Session("user_idUsuario") = 1 Then
        '    Ssql = "Select usuario, id_usuario from DB_Nac_Merca.tbl_02_usuarios"
        'Else
        '    Ssql = "Select usuario, id_usuario from DB_Nac_Merca.tbl_02_usuarios where id_usuario= " & lblHidden1.Value & " not in (1)"
        'End If

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
            'bitacora para capturar eliminación de un usuario
            Using cusuario_bitacora As New ControlBitacora
                'log_bitacora.log_sesion_inicio(5, Session("user_idUsuario"), "" & txtUsuario.Text & " ya esta registrado")
                cusuario_bitacora.acciones_Comunes(6, Session("user_idUsuario"), 7, "" & lblHidden2.Value & "con codigo " & lblHidden1.Value & " se elimino exitosamente")
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
                            cusuario_bitacora.acciones_Comunes(7, Session("user_idUsuario"), 7, "" & lblHidden2.Value & "con codigo " & lblHidden1.Value & " no puede ser eliminado, su estado paso a inactivo.")
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