﻿Public Class config_objetos
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #41
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                'REDIRECCIONAR A MENU PRINCIPAL
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'si hay una sesion activa
                'comprobar que el rol del usuario tenga permisos para ingresar
                Dim Ssql As String = String.Empty
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 41 and permiso_consulta = 1"
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then

                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

                    If Not IsPostBack Then
                        Select Case Request.QueryString("acction")
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
                                Session("IDfrmQueIngresa") = 41
                                Session("NombrefrmQueIngresa") = "Permisos de Objetos"
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If
                        End Select

                        Select Case Request.QueryString("rol")
                            Case > 0
                                'LlenarGrid(CInt(Request.QueryString("rol")))
                                ddlRoles.SelectedValue = Request.QueryString("rol")
                                SqlModulos.SelectCommand = "SELECT 0  AS id_modulo, 'Seleccionar Módulo...' AS nombre UNION ALL 
                                                    SELECT id_modulo, nombre  FROM DB_Nac_Merca.tbl_36_modulos T001
                                                    WHERE id_modulo   IN (SELECT *FROM (SELECT id_modulo FROM DB_Nac_Merca.tbl_37_permisos_modulos  WHERE id_rol = " & Request.QueryString("rol") & ")
                                                    TBL_NO_MODULOS)"
                                ddlModulos.SelectedValue = Request.QueryString("modulo")
                                LlenarGrid(Request.QueryString("rol"), Request.QueryString("modulo"))
                        End Select
                    End If
                Else
                    'si no tiene permisos 
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 41, "El usuario intenta ingresa a una pantalla sin permisos")
                    End Using
                    Response.Redirect("~/modulos/acceso_denegado.aspx")
                End If
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub
    Private Sub LlenarGrid(ByVal xidrol As Integer, ByVal xidModulo As Integer)
        Dim Ssql As String = String.Empty
        Ssql = "SELECT 
                 T001.Id_Permisos, T001.id_objeto, T002.objeto, 
                CASE WHEN T001.permiso_consulta = 1 THEN 'SI' ELSE 'NO' END AS permiso_consulta,
                CASE WHEN T001.permiso_insercion = 1 THEN 'SI' ELSE 'NO' END AS permiso_insercion,
                CASE WHEN T001.permiso_eliminacion = 1 THEN 'SI' ELSE 'NO' END AS permiso_eliminacion, 
                CASE WHEN T001.permiso_actualizacion = 1 THEN 'SI' ELSE 'NO' END AS permiso_actualizacion

                 FROM DB_Nac_Merca.tbl_03_permisos T001
                 LEFT JOIN DB_Nac_Merca.tbl_16_objetos T002 ON T001.id_objeto = T002.id_objeto
                where T001.id_rol = " & xidrol & "
                AND T002.id_modulo = " & xidModulo & ";"
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

    End Sub
    Private Sub ddlRoles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRoles.SelectedIndexChanged
        SqlModulos.SelectCommand = "SELECT 0  AS id_modulo, 'Seleccionar Módulo...' AS nombre UNION ALL 
        SELECT id_modulo, nombre  FROM DB_Nac_Merca.tbl_36_modulos T001
        WHERE id_modulo   IN (SELECT *FROM (SELECT id_modulo FROM DB_Nac_Merca.tbl_37_permisos_modulos  WHERE id_rol = " & ddlRoles.SelectedValue & ")
        TBL_NO_MODULOS)"

    End Sub

    Private Sub ddlModulos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModulos.SelectedIndexChanged
        LlenarGrid(ddlRoles.SelectedValue, ddlModulos.SelectedValue)
    End Sub

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 41 and permiso_actualizacion = 1"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then

                'editando el permiso del objeto
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_03_permisos` SET `permiso_consulta` = " & chkConsultar.Checked & " ,`permiso_insercion` = " & chkInsetar.Checked & " ,`permiso_eliminacion` = " & chkEliminar.Checked & " ,`permiso_actualizacion` = " & chkActualizar.Checked & "  WHERE `Id_Permisos` = " & HiddenLblEditarIDPermiso.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(11, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se cambian los permisos para el objeto con id : " & HiddenLblEditarIDPermiso.Value & " para el rol " & ddlRoles.SelectedItem.ToString)
                End Using


                Response.Redirect("~/modulos/configuraciones/config_objetos.aspx?acction=editpermiso&rol=" & ddlRoles.SelectedValue & "&modulo=" & ddlModulos.SelectedValue)


            Else
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Permisos','Su usuario no tiene permisos para realizar esta acción.', 'error');</script>")
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub
End Class