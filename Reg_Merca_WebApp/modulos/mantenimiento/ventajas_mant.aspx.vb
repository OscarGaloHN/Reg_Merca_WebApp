Public Class ventajas_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #26
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_30_Ventajas"
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
                    Case "newventajas"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('VENTAJAS','La ventaja se Guardo con exito.', 'success');</script>")
                    Case "delteventajas"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('VENTAJAS','La ventaja se Elimino con exito.', 'success');</script>")
                    Case "editventajas"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('VENTAJAS','Laventaja se Modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 26
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Ventajas"
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
            If txtdescripcionEditar.Text <> lblHiddenIDVentajas.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_30_Ventajas where Descripcion = BINARY  '" & txtdescripcionEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('VENTAJAS','La Ventaja ya esta Registrada ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_30_Ventajas` SET  `id_Ventaja` = '" & txtidEditar.Text & "',`Descripcion` = '" & txtdescripcionEditar.Text & "' WHERE `id_Ventaja` = " & lblHiddenIDVentajas.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para el Pais con id: " & lblHiddenIDVentajas.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/ventajas_mant.aspx?acction=editventajas")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarventajas_Click(sender As Object, e As EventArgs) Handles bttGuardarventajas.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_30_Ventajas where Descripcion = BINARY  '" & txtventajas.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PAISES','El nombre de Pais ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_30_Ventajas` (`id_Ventaja`,`Descripcion`) VALUES ('" & txtid.Text & "','" & txtventajas.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva Ventaja con nombre: " & txtventajas.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/ventajas_mant.aspx?acction=newventajas")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarventajas_Click(sender As Object, e As EventArgs) Handles bttEliminarventajas.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_30_Ventajas` WHERE id_Ventaja= " & lblHiddenIDVentajas.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la ventaja con ID: " & lblHiddenIDVentajas.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/ventajas_mant.aspx?acction=delteventajas")
        Catch ex As Exception

        End Try
    End Sub
End Class