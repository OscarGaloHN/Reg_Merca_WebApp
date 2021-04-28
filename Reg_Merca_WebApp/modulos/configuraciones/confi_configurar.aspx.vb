Public Class configurar
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO 1
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'comprobar que el rol del usuario tenga permisos para ingresar
                Dim Ssql As String = String.Empty
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 1 and permiso_consulta = 1"
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then

                    If Not IsPostBack Then

                        'bitacora de que salio de un form

                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                        End Using
                    End If

                    'bitacora de que ingreso al form
                    Session("IDfrmQueIngresa") = 1
                    Session("NombrefrmQueIngresa") = "Configuraciones"
                    If Not IsPostBack Then
                        Using log_bitacora As New ControlBitacora
                            log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                        End Using
                    End If
                    'Using log_bitacora As New ControlBitacora
                    '    log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 1, "El usuario ingresa a la pantalla de configuraciones")
                    'End Using
                    txtEmpresa.Text = Application("ParametrosADMIN")(2)
                    txtAlias.Text = Application("ParametrosADMIN")(3)
                    txtRTN.Text = Application("ParametrosADMIN")(6)
                    txtEmail.Text = Application("ParametrosADMIN")(4)
                    txttel.Text = Application("ParametrosADMIN")(5)
                    txtDireccion.Text = Application("ParametrosADMIN")(14)
                    txtADMIN_URL_WEB.Text = Application("ParametrosADMIN")(19)

                    If CBool(Application("ParametrosSYS")(2)) = True Then
                        bttGuardar.Visible = True
                        bttLimpiar.Visible = True
                        BttGuaradryContinuar.Visible = False
                        divBttGuardar.Visible = False
                    Else
                        bttGuardar.Visible = False
                        bttLimpiar.Visible = False
                        BttGuaradryContinuar.Visible = True
                    End If
                Else
                    'si no tiene permisos 
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 1, "El usuario intenta ingresa a una pantalla sin permisos")
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

    Private Sub bttLimpiar_Click(sender As Object, e As EventArgs) Handles bttLimpiar.Click
        txtEmpresa.Text = ""
        txtAlias.Text = ""
        txtRTN.Text = ""
        txtEmail.Text = ""
        txttel.Text = ""
        txtDireccion.Text = ""
        txtADMIN_URL_WEB.Text = ""




    End Sub

    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        Try
            Dim Ssql As String
            'nombre empresa 
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del nombre de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtEmpresa.Text & "' where id_parametro =5"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'alias
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del alias de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtAlias.Text & "' where id_parametro =6"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'RTN
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del RTN de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtRTN.Text & "' where id_parametro =9"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'email empresa
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del Email de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmail.Text & "' where id_parametro =7"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'telefono de la empresa
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del telefono de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txttel.Text & "' where id_parametro =8"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'direccion
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso de la direccion de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtDireccion.Text & "' where id_parametro =20"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'sistema configurado
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "se realizaron modificaciones a configuraciones")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor ='TRUE' where id_parametro =10"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            If (FileUpload1.HasFile) Then
                'eliminar logo actual
                If (System.IO.File.Exists(Server.MapPath("~/images/") + Session("xLOGO"))) Then
                    System.IO.File.Delete(Server.MapPath("~/images/") + Session("xLOGO"))
                End If
                'guardar imagen
                Dim fso = Server.CreateObject("Scripting.FileSystemObject")
                FileUpload1.SaveAs(Server.MapPath("~/images/") + FileUpload1.FileName)
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Cambio del logo")
                End Using
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & FileUpload1.FileName & "' where id_parametro =36"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
            End If


            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuraciones','Su configuacion fue almacenada exitosamente.', 'success');</script>")
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

    Private Sub BttGuaradryContinuar_Click(sender As Object, e As EventArgs) Handles BttGuaradryContinuar.Click
        Try
            Dim Ssql As String
            'nombre empresa 
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del nombre de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtEmpresa.Text & "' where id_parametro =5"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'alias
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del alias de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros  SET  valor = '" & txtAlias.Text & "' where id_parametro =6"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'RTN
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del RTN de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtRTN.Text & "' where id_parametro =9"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            'email empresa
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del Email de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtEmail.Text & "' where id_parametro =7"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'telefono de la empresa
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso del telefono de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txttel.Text & "' where id_parametro =8"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'direccion
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Ingreso de la direccion de la empresa")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & txtDireccion.Text & "' where id_parametro =20"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'sistema configurado
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "se realizaron modificaciones a configuraciones")
            End Using
            Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor ='TRUE' where id_parametro =10"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            If (FileUpload1.HasFile) Then
                'eliminar logo actual
                If (System.IO.File.Exists(Server.MapPath("~/images/") + Session("xLOGO"))) Then
                    System.IO.File.Delete(Server.MapPath("~/images/") + Session("xLOGO"))
                End If
                'guardar imagen
                Dim fso = Server.CreateObject("Scripting.FileSystemObject")
                FileUpload1.SaveAs(Server.MapPath("~/images/") + FileUpload1.FileName)
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(12, Session("user_idUsuario"), 1, "Cambio del logo")
                End Using
                Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = '" & FileUpload1.FileName & "' where id_parametro =36"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
            End If
            Response.Redirect("~/modulos/configuraciones/config_avanz.aspx")
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Configuraciones','Su configuacion fue almacenada exitosamente.', 'success');</script>")
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