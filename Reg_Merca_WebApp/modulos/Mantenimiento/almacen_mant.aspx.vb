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
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Almacenes"
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





    Private Sub bttGuardarAlmacen_Click(sender As Object, e As EventArgs) Handles bttGuardarAlmacen.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_9_almacenes where Nombre = BINARY  '" & txtnombre.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Almacen','El nombre del Almacen ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_9_almacenes` (`Nombre`, `Ubicacion`, `Contacto`, `Tel`) VALUES ('" & txtnombre.Text & "', '" & txtubicacion.Text & "', '" & txtcontacto.Text & "', '" & txttel.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo un nuevo almacen con nombre: " & txtnombre.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/almacen_mant.aspx?acction=newalmacen")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttEliminarAlmacen_Click(sender As Object, e As EventArgs) Handles bttEliminarAlmacen.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_9_almacenes` WHERE Id_almacen= " & lblHiddenIDAlmacen.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el almacen con nombre: " & lblHiddenNombreAlmacen.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/almacen_mant.aspx?acction=deltealmacen")
        Catch ex As Exception

        End Try
    End Sub
End Class