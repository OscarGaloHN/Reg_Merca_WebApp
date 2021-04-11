Public Class tipoitems_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #37
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_26_Tipo_Items"
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
                    Case "newItems"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Items','El tipo de items se almaceno con exito.', 'success');</script>")
                    Case "delteItems"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Items','El tipo de items se elimino con exito.', 'success');</script>")
                    Case "editItems"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Items','El tipo de items se modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 37
                        Session("NombrefrmQueIngresa") = "Mantenimiento de tipos de items"
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

    Private Sub bttGuardarItems_Click(sender As Object, e As EventArgs) Handles bttGuardarItems.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_26_Tipo_Items where Id_TipoItems = BINARY  '" & txtId_TipoItems.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Items','El estado  ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_26_Tipo_Items where Id_TipoItems` (`Id_TipoItems`, `Descripcion`) VALUES ('" & txtId_TipoItems.Text & "' '" & txtDescripcion.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se edito el tipo de items  con el id:" & lblHiddenIDItems.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/tipoitems_mant.aspx?acction=editestado")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarItems_Click(sender As Object, e As EventArgs) Handles bttEliminarItems.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_26_Tipo_Items where Id_TipoItems` WHERE Id_TipoItems= " & lblHiddenIDItems.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el estado  con nombre: " & lblHiddenNombreItems.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/tipoitems_mant.aspx?acction=delteaduana")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificarItems_Click(sender As Object, e As EventArgs) Handles bttModificarItems.Click
        Try
            Dim Ssql As String = String.Empty
            If txtId_TipoItemsEditar.Text <> lblHiddenNombreItems.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_26_Tipo_Items where Id_TipoItems = BINARY  '" & txtId_TipoItems.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El Tipo de Items ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_26_Tipo_Items` SET `Id_TipoItems` = '" & txtId_TipoItemsEditar.Text & "', `descripcion` = '" & txtDescripcionEditar.Text & "' WHERE `Id_TipoItems` = " & lblHiddenIDItems.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo un nuevo tipo de Items con descripcion: " & txtDescripcion.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/tipoitems_mant.aspx?acction=newItems")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class