Public Class unidmedida_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #40
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_24_Unidad_Medida"
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
                    Case "newmedida"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('medida','La unidad de medida se almaceno con exito.', 'success');</script>")
                    Case "deltemedida"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('medida','La unidad de medida se elimino con exito.', 'success');</script>")
                    Case "editmedida"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('medida','La unidad de medida se modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 25
                        Session("NombrefrmQueIngresa") = "Mantenimiento unidad de medida"
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

    Private Sub bttGuardarmedida_Click(sender As Object, e As EventArgs) Handles bttGuardarmedida.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_24_Unidad_Medida where Id_UnidadMed = BINARY  '" & txtId_UnidadMed.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('medida','La unidad de medida ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_24_Unidad_Medida` (`Id_UnidadMed`, `Descripcion`) VALUES ('" & txtId_UnidadMed.Text & "','" & txtDescripcion.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nuevo estado con descripcion: " & txtDescripcion.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/unidmedida_mant.aspx?acction=newmedida")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarmedida_Click(sender As Object, e As EventArgs) Handles bttEliminarmedida.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_24_Unidad_Medida` WHERE Id_UnidadMed= " & lblHiddenIDmedida.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el estado  con nombre: " & lblHiddenNombremedida.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/unidmedida_mant.aspx?acction=deltemedida")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificarmedida_Click(sender As Object, e As EventArgs) Handles bttModificarmedida.Click
        Try
            Dim Ssql As String = String.Empty
            If txtId_UnidadMedEditar.Text <> lblHiddenNombremedida.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_24_Unidad_Medida where Id_UnidadMed = BINARY  '" & txtId_UnidadMedEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El nombre de aduana ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_24_Unidad_Medida` SET `Id_UnidadMed` = '" & txtId_UnidadMedEditar.Text & "', `Descripcion` = '" & txtDescripcionEditar.Text & "' WHERE `Id_UnidadMed` = '" & lblHiddenIDmedida.Value & "';"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos del estado de la mercancia con el id:" & lblHiddenIDmedida.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/unidmedida_mant.aspx?acction=editmedida")

            End If
        Catch ex As Exception

        End Try
    End Sub






End Class