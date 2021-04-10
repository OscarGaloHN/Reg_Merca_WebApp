﻿Public Class estadomerc_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #25
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_25_Estado_Mercancias"
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
                    Case "newestado"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('estado','El estado se almaceno con exito.', 'success');</script>")
                    Case "delteestado"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('estado','El estado se elimino con exito.', 'success');</script>")
                    Case "editestado"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('estado','El estado se modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 25
                        Session("NombrefrmQueIngresa") = "Mantenimiento de estados de la mercancia"
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

    Private Sub bttGuardarEstado_Click(sender As Object, e As EventArgs) Handles bttGuardarEstado.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_25_Estado_Mercancias where Id_Estado = BINARY  '" & txtId_Estado.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('estado','El estado  ya esta registrado.', 'error');</script>")
            Else
                Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_25_Estado_Mercancias` (`Id_Estado`, `descripcion`) VALUES ('" & txtId_Estado.Text & "' '" & txtdescripcion.Text & "');"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos del estado de la mercancia con el id:" & lblHiddenIDestado.Value)
                End Using
                Response.Redirect("~/modulos/mantenimiento/estadomerc_mant.aspx?acction=editestado")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarEstado_Click(sender As Object, e As EventArgs) Handles bttEliminarEstado.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_25_Estado_Mercancias` WHERE Id_estado= " & lblHiddenIDestado.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino el estado  con nombre: " & lblHiddenNombreEstado.Value & " con exito")
            End Using
            Response.Redirect("~/modulos/mantenimiento/estadomerc_mant.aspx?acction=delteaduana")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificar_Click(sender As Object, e As EventArgs) Handles bttModificar.Click
        Try
            Dim Ssql As String = String.Empty
            If txtdescripcionEditar.Text <> lblHiddenNombreEstado.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_25_Estado_Mercancias where Id_Estado = BINARY  '" & txtId_Estado.Text & "' "
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
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_25_Estado_Mercancias` SET `Id_Estado` = '" & txtId_EstadoEditar.Text & "', `descripcion` = '" & txtdescripcionEditar.Text & "' WHERE `Id_Estado` = " & lblHiddenIDestado.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nuevo estado con descripcion: " & txtdescripcion.Text)
                End Using
                Response.Redirect("~/modulos/mantenimiento/estadomerc_mant.aspx?acction=newestado")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class