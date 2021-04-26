Public Class forma_pago
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #35
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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 35 and permiso_consulta = 1"

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

                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_13_forma_pago"
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
                            Case "newpago"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('pago','La forma de pago se almaceno con exito.', 'success');</script>")
                            Case "deltepago"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('pago','La forma de pago se elimino con exito.', 'success');</script>")
                            Case "editpago"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('pago','La forma de pago se modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 35
                                Session("NombrefrmQueIngresa") = "Mantenimiento de formas de pago"
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
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttGuardarpago_Click(sender As Object, e As EventArgs) Handles bttGuardarpago.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_13_forma_pago where id_pago = BINARY  '" & txtid_pago.Text & "'"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('pago','la forma de pago  ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_13_forma_pago` (`id_pago`, `nombre_pago`) VALUES ('" & txtid_pago.Text & "','" & txtnombre_pago.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardaron los datos de la forma de pago de la mercancia con el id:" & lblHiddenIDpago.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/forma_pago.aspx?acction=editpago")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarpago_Click(sender As Object, e As EventArgs) Handles bttEliminarpago.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_13_forma_pago` WHERE `id_pago` = '" & lblHiddenIDpago.Value & "';"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la forma de pago  con nombre: " & lblHiddenNombrepago.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/forma_pago.aspx?acction=deltepago")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtid_pago.Text <> lblHiddenNombrepago.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_13_forma_pago where id_pago = BINARY  '" & txtid_pagoEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','La forma de pago ya esta registrada.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_13_forma_pago` SET `id_pago` = '" & txtid_pagoEditar.Text & "', `nombre_pago` = '" & txtnombre_pagoEditar.Text & "' WHERE `id_pago` = '" & lblHiddenIDpago.Value & "';"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se edito una nueva forma de pago con nombre de pago: " & txtnombre_pagoEditar.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/forma_pago.aspx?acction=newpago")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class