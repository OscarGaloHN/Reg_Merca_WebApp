Public Class proveedores_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #38
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                'REDIRECCIONAR A MENU PRINCIPAL
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'si hay una sesion activa
                'comprobar que el rol del usuario tenga permisos para editar
                Dim Ssql As String = String.Empty
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 38 and permiso_consulta = 1"

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    'si tiene los permisos
                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
                    'llenar grid

                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_05_proveedores"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        gvCustomers.DataSource = DataSetX
                        gvCustomers.DataBind()
                    End If

                    If Not IsPostBack Then
                        Select Case Request.QueryString("acction")
                            Case "newproveedores"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PROVEEDORES','El proveedor se almaceno con exito.', 'success');</script>")
                            Case "delteproveedores"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PROVEEDORES','El proveedor se elimino con exito.', 'success');</script>")
                            Case "editproveedores"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PROVEEDORES','El proveedor se modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 38
                                Session("NombrefrmQueIngresa") = "Mantenimiento de proveedor"
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If
                        End Select
                    End If
                Else
                    'si no tiene permisos 
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 12, "El usuario intenta ingresa a una pantalla sin permisos")
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

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtnombre.Text <> lblHiddenNombreproveedor.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_05_proveedores where nombre = BINARY  '" & txtnombreEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PROVEEDORES','El nombre de Proveedores ya esta registrado.', 'error');</script>")
            Else

                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_05_proveedores` SET `nombre` = '" & txtnombreEditar.Text & "', `direccion_domicilio` = '" & txtdirecciondomicilioEditar.Text & "', `direccion_envio` = '" & txtdireccionenvioEditar.Text & "', `ciudad` = '" & txtciudadEditar.Text & "' ,`telefono` = '" & txttelefonoEditar.Text & "',`telefono2`= '" & txttelefono2Editar.Text & "',`telefono3` = '" & txttelefono3Editar.Text & "',`fax`= '" & txtfaxEditar.Text & "',`email_personal`= '" & txtemailpersonalEditar.Text & "' ,`email_empresarial`= '" & txtemailempresarialEditar.Text & "',`rtn_proveedor`='" & txtrtnproEditar.Text & "',`contacto`= '" & txtcontactoEditar.Text & "',`limitecr`='" & txtlimitecrEditar.Text & "',`plazocr` = '" & txtplazocrEditar.Text & "'where `Id_proveedor` =" & lblHiddenIDproveedor.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo un nuevo proveedor con nombre: " & txtnombre.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/proveedores_mant.aspx?acction=newproveedores")
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

    Private Sub bttGuardarproveedor_Click(sender As Object, e As EventArgs) Handles bttGuardarproveedor.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_05_proveedores where nombre = BINARY  '" & txtnombre.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PROVEEDORES','El proveedor ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_05_proveedores` (Id_nivel_com,Id_pais,`nombre`,`direccion_domicilio`,`direccion_envio`,`ciudad`,`telefono`,`telefono2`,`telefono3`,`fax`,`email_personal`,`email_empresarial`,`rtn_proveedor`,`contacto`,`limitecr`,`plazocr`) VALUES ('" & cmbNivelComercial.SelectedValue & "','" & CmbPais.SelectedValue & "','" & txtnombre.Text & "', '" & txtdirecciondomicilio.Text & "','" & txtdireccionenvio.Text & "','" & txtciudad.Text & "','" & txttelefono.Text & "','" & txttelefono2.Text & "','" & txttelefono3.Text & "','" & txtfax.Text & "','" & txtemailpersonal.Text & "','" & txtemailempresarial.Text & "','" & txtrtnpro.Text & "','" & txtcontacto.Text & "','" & txtlimitecr.Text & "','" & txtplazocr.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idusuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para el proveedor con id: " & txtnombre.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/proveedores_mant.aspx?acction=newproveedores")
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

    Private Sub bttEliminarproveedor_Click(sender As Object, e As EventArgs) Handles bttEliminarproveedor.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_05_proveedores` WHERE Id_proveedor= " & lblHiddenIDproveedor.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el proveedor con nombre: " & lblHiddenNombreproveedor.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/proveedores_mant.aspx?acction=delteproveedores")
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