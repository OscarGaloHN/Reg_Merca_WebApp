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
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            'REDIRECCIONAR A MENU PRINCIPAL
            Response.Redirect("~/Inicio/login.aspx")
        Else
            'si hay una sesion activa
            'comprobar que el rol del usuario tenga permisos para ingresar
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 27 and permiso_consulta = 1"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then

                Try
                    ''parametros de configuracion de sistema
                    'Using Parametros_Sistema As New ControlDB
                    '    Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
                    'End Using

                    ''PARAMETROS DE ADMINISTRADOR
                    'Using Parametros_admin As New ControlDB
                    '    Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
                    'End Using

                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

                    If Not IsPostBack Then
                        Select Case Request.QueryString("acction")
                            Case "newpermiso"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','El permiso se almaceno con exito.', 'success');</script>")
                            Case "deltepermiso"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','El permiso se elimino con exito.', 'success');</script>")
                            Case "editpermiso"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','El permiso se modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If
                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 27
                                Session("NombrefrmQueIngresa") = "Permiso de Módulos"
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If
                        End Select

                        Select Case Request.QueryString("rol")
                            Case > 0
                                LlenarGrid(CInt(Request.QueryString("rol")))
                                ddlRoles.SelectedValue = Request.QueryString("rol")

                        End Select
                    End If
                Catch ex As Exception

                End Try

            Else
                'si no tiene permisos 
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 27, "El usuario intenta ingresa a una pantalla sin permisos")
                End Using
                Response.Redirect("~/modulos/acceso_denegado.aspx")
            End If
        End If
    End Sub
    Private Sub LlenarGrid(ByVal xidrol As Integer)
        Dim Ssql As String = String.Empty
        Ssql = "SELECT T001.id_permiso_modulo , T002.nombre , T003.rol
                FROM DB_Nac_Merca.tbl_37_permisos_modulos T001 
                LEFT JOIN DB_Nac_Merca.tbl_36_modulos T002 ON T001.id_modulo= T002.id_modulo
                LEFT JOIN DB_Nac_Merca.tbl_15_rol T003 ON T001.id_rol = T003.id_rol where T001.id_rol = " & xidrol & " ;"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            gvCustomers.DataSource = DataSetX
            gvCustomers.DataBind()
        Else
            gvCustomers.DataSource = Nothing
            gvCustomers.DataBind()
        End If
        SqlModulos.SelectCommand = "SELECT 0  AS id_modulo, 'Seleccionar Módulo...' AS nombre UNION ALL 
        SELECT id_modulo, nombre  FROM DB_Nac_Merca.tbl_36_modulos T001
        WHERE id_modulo NOT IN (SELECT *FROM (SELECT id_modulo FROM DB_Nac_Merca.tbl_37_permisos_modulos  WHERE id_rol = " & xidrol & ")
        TBL_NO_MODULOS)"
    End Sub
    Private Sub ddlRoles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoles.SelectedIndexChanged
        LlenarGrid(ddlRoles.SelectedValue)
    End Sub

    Private Sub bttGuardarPermiso_Click(sender As Object, e As EventArgs) Handles bttGuardarPermiso.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 27 and permiso_insercion = 1"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Try
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_37_permisos_modulos where id_modulo = " & ddlModulos.SelectedValue & " and id_rol =    " & ddlRoles.SelectedValue & " "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','El permiso ya fue otorgado.', 'error');</script>")
                Else
                    Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_37_permisos_modulos` (`id_modulo`, `id_rol`, `estado`, `creacion`, `modificacion`, `ultimo_editor`) VALUES ( " & ddlModulos.SelectedValue & ",  " & ddlRoles.SelectedValue & ", '1',  CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), null, " & Session("user_idUsuario") & ");"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se crea permiso para el módulo: " & ddlModulos.SelectedItem.ToString & " en el rol: " & ddlRoles.SelectedItem.ToString)
                    End Using

                    'isertrar permisos a los objetos
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_03_permisos
                        SELECT NULL ID, id_objeto,
                        " & ddlRoles.SelectedValue & " id_rol,
                        0 permiso_insercion,0 permiso_eliminacion,
                        0 permiso_actualizacion,1 permiso_consulta
 
                         FROM DB_Nac_Merca.tbl_16_objetos where id_modulo = " & ddlModulos.SelectedValue & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se crea permiso de consulta para los objetos del módulo: " & ddlModulos.SelectedItem.ToString & " en el rol: " & ddlRoles.SelectedItem.ToString)
                    End Using
                    Response.Redirect("~/modulos/configuraciones/config_permisos.aspx?acction=newpermiso&rol=" & ddlRoles.SelectedValue)
                End If
            Catch ex As Exception

            End Try
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','Su usuario no tiene permisos para realizar esta acción.', 'error');</script>")
        End If
    End Sub

    Private Sub bttEliminarPermiso_Click(sender As Object, e As EventArgs) Handles bttEliminarPermiso.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 27 and permiso_eliminacion = 1"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Try
                'eliminar permisos a los objetos
                Ssql = "DELETE  from DB_Nac_Merca.tbl_03_permisos where id_rol=" & ddlRoles.SelectedValue & "  and id_objeto 
                                  in ( SELECT id_objeto FROM DB_Nac_Merca.tbl_16_objetos where id_modulo = 
                                  (SELECT id_modulo FROM DB_Nac_Merca.tbl_36_modulos WHERE nombre = '" & lblNombreModulo.Value & "'))"


                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se eliminan permisos para los objetos del módulo: " & ddlModulos.SelectedItem.ToString & " en el rol: " & ddlRoles.SelectedItem.ToString)
                End Using

                'eliminar permiso modulo
                Ssql = "DELETE FROM `DB_Nac_Merca`.`tbl_37_permisos_modulos` WHERE id_permiso_modulo= " & lblHiddenIDPermisoEliminar.Value
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el permiso para el módulo: " & lblNombreModulo.Value & " en el rol: " & ddlRoles.SelectedItem.ToString)
                End Using


                Response.Redirect("~/modulos/configuraciones/config_permisos.aspx?acction=deltepermiso&rol=" & ddlRoles.SelectedValue)
            Catch ex As Exception

            End Try
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','Su usuario no tiene permisos para realizar esta acción.', 'error');</script>")
        End If
    End Sub

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 27 and permiso_actualizacion = 1"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Try
                If ddlModulosEditar.SelectedItem.ToString = HiddenLblEditarNombreModulo.Value Then
                    Session("NumReg") = 0
                    Exit Sub
                ElseIf ddlModulosEditar.SelectedItem.ToString <> HiddenLblEditarNombreModulo.Value Then
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_37_permisos_modulos where id_modulo = '" & ddlModulosEditar.SelectedValue & "' and  id_rol=" & ddlRoles.SelectedValue & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                Else
                    Session("NumReg") = 0
                End If

                If Session("NumReg") > 0 Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','El rol ya tiene asignado este permiso.', 'error');</script>")
                Else
                    'eliminar permisos a los objetos
                    Ssql = "DELETE  from DB_Nac_Merca.tbl_03_permisos where id_rol=" & ddlRoles.SelectedValue & "  and id_objeto 
                                  in ( SELECT id_objeto FROM DB_Nac_Merca.tbl_16_objetos where id_modulo = " & HiddenLblEditarIdModulo.Value & " )"

                    '"delete from DB_Nac_Merca.tbl_03_permisos where id_rol=" & ddlRoles.SelectedValue & " and id_modulo = " & HiddenLblEditarIdModulo.Value & ";"

                    '"delete from DB_Nac_Merca.tbl_03_permisos where id_rol=" & ddlRoles.SelectedValue & " and id_modulo = 
                    '              (SELECT *FROM(SELECT id_modulo FROM DB_Nac_Merca WHERE nombre = '" & lblNombreModulo.Value & "')TBL_BORRAR);"

                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    'editando el permiso al modulo 
                    Ssql = "UPDATE `DB_Nac_Merca`.`tbl_37_permisos_modulos` SET `id_modulo` = '" & ddlModulosEditar.SelectedValue & "'   WHERE `id_permiso_modulo` = " & HiddenLblEditarIdPermiso.Value & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se cambio el acceso al módulo : " & HiddenLblEditarNombreModulo.Value & " por el módulo " & ddlModulosEditar.SelectedItem.ToString & " para el rol " & ddlRoles.SelectedItem.ToString)
                    End Using


                    'isertrar permisos a los objetos
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_03_permisos
                        SELECT NULL ID,id_objeto,
                        " & ddlRoles.SelectedValue & " id_rol,
                        0 permiso_insercion,0 permiso_eliminacion,
                        0 permiso_actualizacion,1 permiso_consulta
 
                         FROM DB_Nac_Merca.tbl_16_objetos where id_modulo = " & ddlModulosEditar.SelectedValue & ";"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    'agregando los permisos a los objetos por cambio de modulo
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se actualizan permisos para los objetos del módulo: " & ddlModulos.SelectedItem.ToString & " en el rol: " & ddlRoles.SelectedItem.ToString)
                    End Using


                    Response.Redirect("~/modulos/configuraciones/config_permisos.aspx?acction=editpermiso&rol=" & ddlRoles.SelectedValue)
                End If
            Catch ex As Exception

            End Try
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','Su usuario no tiene permisos para realizar esta acción.', 'error');</script>")
        End If
    End Sub
End Class