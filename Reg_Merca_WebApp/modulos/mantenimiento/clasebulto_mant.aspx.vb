Public Class clasebulto_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #44

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("user_idUsuario") = Nothing Then
            Session.Abandon()
            'REDIRECCIONAR A MENU PRINCIPAL
            Response.Redirect("~/Inicio/login.aspx")
        Else
            'si hay una sesion activa
            'comprobar que el rol del usuario tenga permisos para editar
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 44 and permiso_consulta = 1"

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'si tiene los permisos
                Try
                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

                    'llenar grid

                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_18_Clase_deBulto"
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
                            Case "newbulto"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('bulto','La clase de bulto se almaceno con exito.', 'success');</script>")
                            Case "deltebulto"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('bulto','La clase de bulto se elimino con exito.', 'success');</script>")
                            Case "editbulto"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('bulto','La clase de bulto se modifico con exito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 44
                                Session("NombrefrmQueIngresa") = "Mantenimiento Clase de Bulto"
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If
                        End Select
                    End If
                Catch ex As Exception

                End Try
            Else
                'si no tiene permisos 
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 12, "El usuario intenta ingresa a una pantalla sin permisos")
                End Using
                Response.Redirect("~/modulos/acceso_denegado.aspx")
            End If
        End If
    End Sub

    Private Sub bttGuardarbulto_Click(sender As Object, e As EventArgs) Handles bttGuardarbulto.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_18_Clase_deBulto where Id_Clase_deBulto = BINARY  " & txtId_Clase_deBulto.Text & " "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('bulto','La clase de bulto  ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_18_Clase_deBulto` (`Id_Clase_deBulto`, `Descripción`) VALUES (" & txtId_Clase_deBulto.Text & ",'" & txtDescripcion.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo La Clase De Bulto  Con El Id:" & lblHiddenIDbulto.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/clasebulto_mant.aspx?acction=newbulto")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarbulto_Click(sender As Object, e As EventArgs) Handles bttEliminarbulto.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_18_Clase_deBulto` WHERE `Id_Clase_deBulto`= '" & lblHiddenIDbulto.Value & "';"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el estado  con nombre: " & lblHiddenNombrebulto.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/clasebulto_mant.aspx?acction=deltebulto")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificarbulto_Click(sender As Object, e As EventArgs) Handles bttModificarbulto.Click
        Try
            Dim Ssql As String = String.Empty
            If txtId_Clase_deBultoEditar.Text <> lblHiddenNombrebulto.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_18_Clase_deBulto where Id_Clase_deBulto = BINARY  '" & txtId_Clase_deBultoEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('bulto','La clase de bulto ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_18_Clase_deBulto` SET `Id_Clase_deBulto` = '" & txtId_Clase_deBultoEditar.Text & "', `Descripción` = '" & txtDescripcionEditar.Text & "' WHERE `Id_Clase_deBulto` = " & lblHiddenIDbulto.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se modifico la clase de bulto con descripción: " & txtDescripcion.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/clasebulto_mant.aspx?acction=editbulto")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class