Public Class almacen_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #18
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                'llenar grid
                Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_9_almacenes"
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
                    Case "newalmacen"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Almacen','El Almacen se almaceno con exito.', 'success');</script>")
                    Case "deltealmacen"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Almacen','El Almacen se elimino con exito.', 'success');</script>")

                    Case Else
                            'bitacora de que salio de un form
                            If Not IsPostBack Then
                                Using log_bitacora As New ControlBitacora
                                    log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                End Using
                            End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 18
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Aduanas"
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

    Private Sub bttGuardarAduana_Click(sender As Object, e As EventArgs) Handles bttGuardarAlmacén.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_9_almacenes where Nombre_aduana = BINARY  '" & txtalmacen.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El nombre de aduana ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_9_almacenes` (`Nombre_aduana`, `Contacto`, `Tel`, `Ubicacion`) VALUES ('" & txtalmacen.Text & "', '" & txtcontacto.Text & "', '" & txtTel.Text & "', '" & txtubicacion.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtalmacen.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/mantenimiento_adunas.aspx?acction=newaduana")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarAduna_Click(sender As Object, e As EventArgs) Handles bttEliminarAlmacen.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_9_almacenes` WHERE Id_Aduana= " & lblHiddenId_almacen.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el almacén con nombre: " & lblHiddenNombre.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/mantenimiento_adunas.aspx?acction=delteaduana")
        Catch ex As Exception

        End Try
    End Sub


End Class