Public Class paises_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #23
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_8_paises"
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
                    Case "newpaises"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PAISES','El Pais se Guardo con exito.', 'success');</script>")
                    Case "deltepaises"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PAISES','El Pais se Elimino con exito.', 'success');</script>")
                    Case "editpaises"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PAISES','El Pais se Modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 23
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Paises"
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
            If txtpaisesEditar.Text <> lblHiddenNombrepaises.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_8_paises where Nombre_Pais = BINARY  '" & txtpaisesEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PAISES','El nombre del Pais ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_8_paises` SET `Id_Pais` = '" & txtidEditar.Text & "',`Nombre_Pais` = '" & txtpaisesEditar.Text & "' WHERE `Id_Pais` = '" & lblHiddenIDpaises.Value & "';"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para el Pais con id: " & lblHiddenIDpaises.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/paises_mant.aspx?acction=editpaises")

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarpaises_Click(sender As Object, e As EventArgs) Handles bttGuardarpaises.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_8_paises where Nombre_Pais = BINARY  '" & txtpaises.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PAISES','El nombre de Pais ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_8_paises` (`Id_Pais`,`Nombre_Pais`) VALUES ('" & txtid.Text & "','" & txtpaises.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo un nuevo Pais con nombre: " & txtpaises.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/paises_mant.aspx?acction=newpaises")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarpaises_Click(sender As Object, e As EventArgs) Handles bttEliminarpaises.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_8_paises` WHERE Id_pais= " & lblHiddenIDpaises.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el país con nombre: " & lblHiddenNombrepaises.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/paises_mant.aspx?acction=deltepaises")
        Catch ex As Exception

        End Try
    End Sub
End Class