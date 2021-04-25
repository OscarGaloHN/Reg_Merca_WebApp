Public Class nivelcomerc_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #39


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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 39 and permiso_consulta = 1"

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

                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_12_nivel_comercial"
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
                            Case "newcomercial"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('comercial','El nivel comercial se Guardo con exito.', 'success');</script>")
                            Case "deltecomercial"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('comercial','El nivel comercial se Elimino con exito.', 'success');</script>")
                            Case "editcomercial"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('comercial','El nivel comercial se Modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 39
                                Session("NombrefrmQueIngresa") = "Mantenimiento del nivel comercial"
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

    Private Sub bttModificarcomercial_Click(sender As Object, e As EventArgs) Handles bttModificarcomercial.Click
        Try
            Dim Ssql As String = String.Empty
            If txtTipo.Text <> lblHiddenNombrecomercial.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_12_nivel_comercial where Tipo = BINARY  '" & txtTipoEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('PREGUNTAS','La pregunta ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_12_nivel_comercial` SET `Tipo` = '" & txtTipoEditar.Text & "' WHERE `Id_nivel_com` = " & lblHiddenIDcomercial.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos del nivel comercial con id: " & lblHiddenIDcomercial.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/nivelcomerc_mant.aspx?acction=editcomercial")
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttGuardarcomercial_Click(sender As Object, e As EventArgs) Handles bttGuardarcomercial.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT *  FROM DB_Nac_Merca.tbl_12_nivel_comercial where Tipo = BINARY  '" & txtTipo.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('comercial','El nivel comercial ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_12_nivel_comercial` (`Tipo`) VALUES ('" & txtTipo.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo un nuevo nivel comercial: " & txtTipo.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/nivelcomerc_mant.aspx?acction=newcomercial")

            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttEliminarcomercial_Click(sender As Object, e As EventArgs) Handles bttEliminarcomercial.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_12_nivel_comercial` WHERE Id_nivel_com = " & lblHiddenIDcomercial.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el nivel comercial  con nombre: " & lblHiddenNombrecomercial.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/nivelcomerc_mant.aspx?acction=deltecomercial")
        Catch ex As Exception

        End Try

    End Sub
End Class