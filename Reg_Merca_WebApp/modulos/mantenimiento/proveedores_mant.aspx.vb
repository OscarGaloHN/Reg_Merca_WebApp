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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
            'llenar grid
            Dim Ssql As String = String.Empty
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
                    Case "newcliente"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El cliente se almaceno con exito.', 'success');</script>")
                    Case "deltecliente"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El cliente se elimino con exito.', 'success');</script>")
                    Case "editcliente"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El cliente se modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 19
                        Session("NombrefrmQueIngresa") = "Mantenimiento de proveedor"
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtnombre.Text <> lblHiddenNombreproveedor.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_05_proveedores where nombre = BINARY  '" & txtnombre.Text & "' "
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
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_04_cliente` SET `nombre` = '" & txtnombre.Text & "', `direccion_domicilio` = '" & txtdirecciondomicilio.Text & "', `direccion_envio` = " & txtdireccionenvio.Text & "', `ciudad` = '" & txtciudad.Text & " ' ,'telefono' = '" & txttelefono.Text & "','telefono2'= '" & txttelefono2.Text & "','telefono3' = '" & txttelefono3.Text & "','fax'= '" & txtfax.Text & "','email_personal'= '" & txtemailpersonal.Text & "' ,'email_empresarial'= '" & txtemailempresarial.Text & "','rtn_cli'='" & txtrtnpro.Text & "','contacto'= '" & txtcontacto.Text & "','limitecr`='" & txtlimitecr.Text & "','plazocr' = '" & txtplazocr.Text & "'where 'Id_cliente =" & lblHiddenIDproveedor.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtnombre.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/cliente_mant.aspx?acction=newcliente")
            End If
        Catch ex As Exception

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
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_05_proveedores` (Id_nivel_com,Id_pais,`nombrec`,`direccion_domicilio`,`direccion_envio`,`ciudad`,`telefono`,`telefono2`,`telefono3`,`fax`,`email_personal`,`email_empresarial`,`rtn_proveedor`,`contacto`,`limitecr`,`plazocr`) VALUES ('" & cmbNivelComercial.SelectedValue & "','" & CmbPais.SelectedValue & "','" & txtnombre.Text & "', '" & txtdirecciondomicilio.Text & "','" & txtdireccionenvio.Text & "','" & txtciudad.Text & "','" & txttelefono.Text & "','" & txttelefono2.Text & "','" & txttelefono3.Text & "','" & txtfax.Text & "','" & txtemailpersonal.Text & "','" & txtemailempresarial.Text & "','" & txtrtnpro.Text & "','" & txtcontacto.Text & "','" & txtlimitecr.Text & "','" & txtplazocr.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idusuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & txtIdentidadCliente.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/cliente_mant.aspx?acction=newcliente")
            End If
        Catch ex As Exception

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
            Response.Redirect("~/modulos/mantenimiento/cliente_mant.aspx?acction=deltecliente")
        Catch ex As Exception

        End Try
    End Sub
End Class