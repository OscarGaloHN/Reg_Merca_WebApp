Public Class cliente_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #17
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            'REDIRECCIONAR A MENU PRINCIPAL
            Response.Redirect("~/Inicio/login.aspx")
        Else
            'si hay una sesion activa
            'comprobar que el rol del usuario tenga permisos para editar
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 17 and permiso_consulta = 1"

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'si tiene los permisos
                Try
                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
                    'llenar grid
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_04_cliente"
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
                            Case "newcliente"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Cliente','El cliente se almaceno con exito.', 'success');</script>")
                            Case "deltecliente"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Cliente','El cliente se elimino con exito.', 'success');</script>")
                            Case "editcliente"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Cliente','El cliente se modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 19
                                Session("NombrefrmQueIngresa") = "Mantenimiento de Cliente"
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If
                        End Select
                    End If
                Catch ex As Exception

                End Try
            Else
                'si no tiene permisos 
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 12, "El usuario intenta ingresa a una pantalla sin permisos")
                End Using
                Response.Redirect("~/modulos/acceso_denegado.aspx")
            End If
        End If
    End Sub





    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtnombre.Text <> lblHiddenNombrecliente.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_04_cliente where nombrec = BINARY  '" & txtnombreEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Cliente','El nombre del cliente ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_04_cliente` SET `nombrec` = '" & txtnombreEditar.Text & "', `direccion_domicilio` = '" & txtdirecciondomicilioEditar.Text & "', `direccion_envio` = '" & txtdireccionenvioEditar.Text & "', `ciudad` = '" & txtciudadEditar.Text & "' ,`telefono` = '" & txttelefonoEditar.Text & "',`telefono2`= '" & txttelefono2Editar.Text & "',`telefono3` = '" & txttelefono3Editar.Text & "',`fax`= '" & txtfaxEditar.Text & "',`email_personal`= '" & txtemailpersonalEditar.Text & "' ,`email_empresarial`= '" & txtemailempresarialEditar.Text & "',`rtn_cli`='" & txtrtncliEditar.Text & "',`contacto`= '" & txtcontactoEditar.Text & "',`limitecr`='" & txtlimitecrEditar.Text & "',`plazocr` = '" & txtplazocrEditar.Text & "' where `Id_cliente` =" & lblHiddenIDcliente.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se modifico los nuevos datos del cliente con nombre: " & txtnombre.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/cliente_mant.aspx?acction=newcliente")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarcliente_Click(sender As Object, e As EventArgs) Handles bttEliminarcliente.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_04_cliente` WHERE Id_cliente= " & lblHiddenIDcliente.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el cliente con nombre: " & lblHiddenNombrecliente.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/cliente_mant.aspx?acction=deltecliente")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarcliente_Click(sender As Object, e As EventArgs) Handles bttGuardarcliente.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_04_cliente where nombrec = BINARY  '" & txtnombre.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Cliente','El nombre del Cliente ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_04_cliente` (Id_nivel_com,Id_pais,`nombrec`,`direccion_domicilio`,`direccion_envio`,`ciudad`,`telefono`,`telefono2`,`telefono3`,`fax`,`email_personal`,`email_empresarial`,`rtn_cli`,`contacto`,`limitecr`,`plazocr`) VALUES ('" & cmbNivelComercial.SelectedValue & "','" & CmbPais.SelectedValue & "','" & txtnombre.Text & "', '" & txtdirecciondomicilio.Text & "','" & txtdireccionenvio.Text & "','" & txtciudad.Text & "','" & txttelefono.Text & "','" & txttelefono2.Text & "','" & txttelefono3.Text & "','" & txtfax.Text & "','" & txtemailpersonal.Text & "','" & txtemailempresarial.Text & "','" & txtrtncli.Text & "','" & txtcontacto.Text & "','" & txtlimitecr.Text & "','" & txtplazocr.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idusuario"), Session("IDfrmQueIngresa"), "Se guardaron los datos del cliente con id: " & txtIdentidadCliente.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/cliente_mant.aspx?acction=newcliente")
            End If
        Catch ex As Exception

        End Try


    End Sub
End Class