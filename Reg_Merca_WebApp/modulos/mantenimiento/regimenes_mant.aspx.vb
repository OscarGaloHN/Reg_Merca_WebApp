Public Class regimenes_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #24
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_27_Regimenes"
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
                    Case "newregimenes"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('REGIMENES','El Regimenes se Guardo con exito.', 'success');</script>")
                    Case "delteregimenes"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('REGIMENES','El Regimenes se Elimino con exito.', 'success');</script>")
                    Case "editregimenes"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('REGIMENES','El Regimenes se Modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 24
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Regimenes"
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
            If txtregimenesEditar.Text <> lblHiddenNombreregimenes.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_27_Regimenes where Nombre = BINARY  '" & txtregimenes.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('REGIMENES','El Regimenes ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_27_Regimenes` SET `Descripcion` = '" & txtregimenesEditar.Text & "' WHERE `Id_Regimen` = " & lblHiddenIDregimenes.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo un nuevo regimen con nombre: " & txtregimenes.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/regimenes_mant.aspx?acction=newregimenes")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarRegimenes_Click(sender As Object, e As EventArgs) Handles bttGuardarRegimenes.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_27_Regimenes where Descripcion = BINARY  '" & txtregimenes.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('REGIMENES','El regimenes ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_27_Regimenes` (`Descripcion`) VALUES ('" & txtregimenes.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para el Regimenes con id: " & lblHiddenIDregimenes.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/regimenes_mant.aspx?acction=editalmacenes")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarRegimenes_Click(sender As Object, e As EventArgs) Handles bttEliminarRegimenes.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_27_Regimenes` WHERE Id_Regimen= " & lblHiddenIDregimenes.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el Regimen con nombre: " & lblHiddenNombreregimenes.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/regimenes_mant.aspx?acction=delteregimenes")
        Catch ex As Exception

        End Try

    End Sub
End Class