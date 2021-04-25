Public Class modalidadesp_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #20
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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 20 and permiso_consulta = 1"

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

                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_39_modalidad_especial"
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
                            Case "newmodalidad"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Modalidad','La modalidad especial se almaceno con exito.', 'success');</script>")
                            Case "deltemodalidad"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Modalidad','La modaalidad especial se elimino con exito.', 'success');</script>")
                            Case "editmodalidad"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Modalidad','La modalidad especial se modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 20
                                Session("NombrefrmQueIngresa") = "Mantenimiento de Modalidad Especial"
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





    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtnombremodalidadEditar.Text <> lblHiddenNombreModalidad.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_39_modalidad_especial where Nombre_modalidad = BINARY  '" & txtnombremodalidad.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Modalidad','El nombre de modalida especial ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_39_modalidad_especial` SET `Nombre_modalidad` = '" & txtnombremodalidadEditar.Text & "'  WHERE `Id_Modalidad` = " & lblHiddenIDModalidad.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la modalidad especial con id: " & lblHiddenIDModalidad.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/modalidadesp_mant.aspx?acction=editmodalidad")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarModalidad_Click(sender As Object, e As EventArgs) Handles bttGuardarModalidad.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_39_modalidad_especial where Nombre_modalidad = BINARY  '" & txtnombremodalidad.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Modalidad','El nombre de Modalidad especial ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_39_modalidad_especial` (`Nombre_modalidad`) VALUES ('" & txtnombremodalidad.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva modalidad especial con nombre: " & txtnombremodalidad.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/modalidadesp_mant.aspx?acction=newmodalidad")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarModalidad_Click(sender As Object, e As EventArgs) Handles bttEliminarModalidad.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_39_modalidad_especial` WHERE Id_Modalidad = " & lblHiddenIDModalidad.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la Modalidad especial con nombre: " & lblHiddenNombreModalidad.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/modalidadesp_mant.aspx?acction=deltemodalidad")
        Catch ex As Exception

        End Try
    End Sub
End Class