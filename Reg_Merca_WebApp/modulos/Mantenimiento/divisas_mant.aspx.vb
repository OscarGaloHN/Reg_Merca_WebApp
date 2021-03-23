Public Class Divisas_mant
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
        Try
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_29_Divisas"
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
                    Case "newdivisas"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','La aduana se almaceno con exito.', 'success');</script>")
                    Case "deltedivisas"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','La aduana se elimino con exito.', 'success');</script>")
                    Case "editdivisas"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','La aduana se modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 17
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Divisas"
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
            If txtdescripcionEditar.Text <> lblHiddenNombreDivisas.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_29_Divisas where Nombre_aduana = BINARY  '" & txtdescripcionEditar.Text & "' "
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
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_06_aduanas` SET `Descripcion` = '" & txtdescripcionEditar.Text & "', `Total_Factura` = '" & txttotalfacturaEditar.Text & "', `Total_Flete` = " & txttotalfleteEditar.Text & ", `Total_Seguro` = '" & txttotalseguroEditar.Text & "', `Total_Otros_gastos` = '" & txttotalotrosEditar.Text & "' WHERE `Id_Divisas` = " & lblHiddenIDdivisas.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtdescripcion.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/mantenimiento_adunas.aspx?acction=newaduana")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarDivisa_Click(sender As Object, e As EventArgs) Handles bttGuardarDivisa.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_29_Divisas where Descripcion = BINARY  '" & txtdescripcion.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El nombre de aduana ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_29_Divisas` (`Descripcion`, `Total_Factura`, `Total_Flete`, `Total_Seguro`, `Total_Otros_gastos`) VALUES ('" & txtdescripcion.Text & "', '" & txttotalfactura.Text & "', '" & txttotalflete.Text & "', '" & txttotalseguro.Text & "','" & txttotalotros.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDdivisas.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/divisas_mant.aspx?acction=editduana")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarDivisa_Click(sender As Object, e As EventArgs) Handles bttEliminarDivisa.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_06_tbl_29_Divisas` WHERE Id_Divisas= " & lblHiddenIDdivisas.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la divisa con nombre: " & lblHiddenNombreDivisas.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/divisas_mant.aspx?acction=deltedivisa")
        Catch ex As Exception

        End Try
    End Sub
End Class