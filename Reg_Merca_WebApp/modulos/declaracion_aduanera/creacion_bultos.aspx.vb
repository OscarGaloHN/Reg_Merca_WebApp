﻿Public Class creacion_bultos
    Inherits System.Web.UI.Page

    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'parametros de configuracion de sistema
        Using Parametros_Sistema As New ControlDB
            Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        End Using

        'PARAMETROS DE ADMINISTRADOR
        Using Parametros_admin As New ControlDB
            Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        End Using



        Try
            'If Session("user_idUsuario") = Nothing Then
            '    Session.Abandon()
            '    Response.Redirect("~/Inicio/login.aspx")
            'Else

            'llenar grid
            Dim Ssql As String = String.Empty
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_40_Bulto"
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
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto se almaceno con éxito.', 'success');</script>")
                        Case "deltebulto"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto se elimino con éxito.', 'success');</script>")
                        Case "editbulto"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto se modifico con éxito.', 'success');</script>")
                        Case Else
                            ''bitacora de que salio de un form
                            'If Not IsPostBack Then
                            '    Using log_bitacora As New ControlBitacora
                            '        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            '    End Using
                            'End If

                            ''bitacora de que ingreso al form
                            'Session("IDfrmQueIngresa") = 17
                            'Session("NombrefrmQueIngresa") = "Mantenimiento de Aduanas"
                            'If Not IsPostBack Then
                            '    Using log_bitacora As New ControlBitacora
                            '        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            '    End Using
                            'End If
                    End Select
                End If

            'End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarbulto_Click(sender As Object, e As EventArgs) Handles bttGuardarbulto.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_40_Bulto where manifiesto = BINARY  '" & txtmanifiesto.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El manifiesto ya esta registrado.', 'error');</script>")
            Else
                If chkindicador.Checked = True Then
                    Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_40_Bulto` (`manifiesto`, `titulo_transporte`, `indicador`) VALUES ('" & txtmanifiesto.Text & "', '" & txt_trans.Text & "', 'SI');"
                Else
                    Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_40_Bulto` (`manifiesto`, `titulo_transporte`, `indicador`) VALUES ('" & txtmanifiesto.Text & "', '" & txt_trans.Text & "', 'NO');"

                End If
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDAduna.Value)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_bultos.aspx?acction=newbulto")
            End If

            ''habilitar indicador
            'If chkindicador.Checked = True Then
            '    Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =35"
            'Else
            '    Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =35"
            'End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarbulto_Click(sender As Object, e As EventArgs) Handles bttEliminarbulto.Click
        Try
            Dim Ssql As String = "DELETE FROM `DB_Nac_Merca`.`tbl_40_Bulto` WHERE id_bulto= " & lblHiddenIDbulto.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la aduna con nombre: " & lblHiddenNombreAduna.Value & " con exito")
            'End Using
            Response.Redirect("~/modulos/declaracion_aduanera/creacion_bultos.aspx?acction=deltebulto")
        Catch ex As Exception

        End Try
    End Sub
    Private Sub bttModificarbulto_Click(sender As Object, e As EventArgs) Handles bttModificarbulto.Click
        Try
            Dim Ssql As String = String.Empty
            If txtmanifiestoEditar.Text <> lblHiddenmanifiesto.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_40_Bulto where manifiesto = BINARY  '" & txtmanifiestoEditar.Text & "' "
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
                Ssql = "UPDATE `DB_Nac_Merca`.`tbl_40_Bulto` SET `manifiesto` = '" & txtmanifiestoEditar.Text & "', `titulo_transporte` = '" & txt_trans.Text & "', `indicador` = '" & chkindicador.Checked & "' WHERE `id_bulto` = " & lblHiddenIDbulto.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtAduana.Text)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_bultos.aspx?acction=editbulto")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
