Public Class condentrega_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #25
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_14_condicion_entrega"
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
                    Case "newcondicion"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('condicion','La condicion se almaceno con exito.', 'success');</script>")
                    Case "deltecondicion"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('condicion','La condicion se elimino con exito.', 'success');</script>")
                    Case "editcondicion"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('condicion','La condicion se modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 36
                        Session("NombrefrmQueIngresa") = "Mantenimiento de condicion de entrega "
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

    Private Sub bttGuardarcondicion_Click(sender As Object, e As EventArgs) Handles bttGuardarcondicion.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_14_condicion_entrega where id_condicion = BINARY  '" & txtid_condicion.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('condicion','la condicion  ya esta registrada.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_14_condicion_entrega` (`id_condicion`, `nombre_condicion`) VALUES ('" & txtid_condicion.Text & "','" & txtnombre_condicion.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo la nueva condicion con descripcion: " & txtnombre_condicion.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/condentrega_mant.aspx?acction=newcondicion")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarcondicion_Click(sender As Object, e As EventArgs) Handles bttEliminarcondicion.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_14_condicion_entrega` WHERE `id_condicion` = '" & lblHiddenIDcondicion.Value & "';"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la condicion  con nombre: " & lblHiddenNombrecondicion.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/condentrega_mant.aspx?acction=deltecondicion")
        Catch ex As Exception


        End Try
    End Sub

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtid_condicionEditar.Text <> lblHiddenNombrecondicion.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_14_condicion_entrega where id_condicion = BINARY  '" & txtid_condicionEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('condicion','La condicion ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_14_condicion_entrega` SET `id_condicion` = '" & txtid_condicionEditar.Text & "', `nombre_condicion` = '" & txtnombre_condicionEditar.Text & "' WHERE `id_condicion` = '" & lblHiddenIDcondicion.Value & "';"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se edito la condicion de la mercancia con el id:" & lblHiddenIDcondicion.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/condentrega_mant.aspx?acction=editcondicion")

            End If
        Catch ex As Exception

        End Try
    End Sub
End Class