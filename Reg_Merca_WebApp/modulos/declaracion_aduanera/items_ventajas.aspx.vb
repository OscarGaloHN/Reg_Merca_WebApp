Public Class items_ventajas
    Inherits System.Web.UI.Page
    '#OBJETO 34
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 34 and permiso_consulta = 1"

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'si tiene los permisos


                Try
                    lblitems.Text = Request.QueryString("iditems")


                    ''If Session("user_idUsuario") = Nothing Then
                    '    Session.Abandon()
                    '    Response.Redirect("~/Inicio/login.aspx")
                    'Else
                    'End If
                    'llenar grid
                    Ssql = "SELECT a.Id_Codigo, a.Id_ventaja, a.Id_merca,b.Descripcion
                    FROM DB_Nac_Merca.tbl_42_Datos_Ventaja a, DB_Nac_Merca.tbl_30_Ventajas b
                    where a.id_Ventaja=b.id_Ventaja
                    and a.Id_merca=" & Request.QueryString("iditems") & ""

                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        gvCustomers.DataSource = DataSetX
                        gvCustomers.DataBind()
                    Else
                        bttcontinuar.Visible = False
                        bttnuevonuevoitems.Visible = False
                    End If

                    If Not IsPostBack Then
                        Select Case Request.QueryString("acction")
                            Case "newdocumento"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documento','El documento se almaceno con éxito.', 'success');</script>")
                            Case "deldocumento"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documento','El documento se elimino con éxito.', 'success');</script>")
                            Case "editdocumento"
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documento','El documento se modifico con éxito.', 'success');</script>")
                            Case Else
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 34
                                Session("NombrefrmQueIngresa") = "Ventas del Item"
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

    Private Sub bttGuardarDocumento_Click(sender As Object, e As EventArgs) Handles bttGuardarDocumento.Click

        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_42_Datos_Ventaja where Id_merca = '" & Request.QueryString("iditems") & "' and Id_ventaja = '" & ddlventajas.SelectedValue & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','El documento ya esta registrado.', 'error');</script>")
            Else

                'Ssql = "INSERT INTO DB_Nac_Merca.tbl_42_Datos_Ventaja (Id_ventaja, Id_merca) VALUES ('" & ddlventajas.SelectedValue & "'," & Request.QueryString("iditems") & "'"
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_42_Datos_Ventaja (Id_ventaja, Id_merca) 
VALUES('" & ddlventajas.SelectedValue & "'," & Request.QueryString("iditems") & "); "


            End If
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDAduna.Value)
            'End Using
            Response.Redirect("~/modulos/declaracion_aduanera/items_ventajas.aspx?acction=newdocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
            'End If

            'habilitar indicador
            'If chkindicador.Checked = True Then
            '    Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =35"
            'Else
            '    Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =35"
            'End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarDocumento_Click(sender As Object, e As EventArgs) Handles bttEliminarDocumento.Click
        Try
            Dim Ssql As String = "DELETE FROM DB_Nac_Merca.tbl_42_Datos_Ventaja WHERE Id_Codigo= " & lblHiddenIDDocumento.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la aduna con nombre: " & lblHiddenNombreAduna.Value & " con exito")
            'End Using

            Response.Redirect("~/modulos/declaracion_aduanera/items_ventajas.aspx?acction=deldocumento&iditems=" & Request.QueryString("iditems") & Request.QueryString("idCaratula"))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificardocumento_Click(sender As Object, e As EventArgs) Handles bttModificardocumento.Click

        Try
            Dim Ssql As String = String.Empty
            If ddlventajaedit.SelectedValue = lblHiddenIDDocumento.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_42_Datos_Ventaja where  id_ventaja = '" & ddlventajaedit.SelectedValue & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Ventaja','La ventaja ya esta registrada.', 'error');</script>")
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_42_Datos_Ventaja SET Id_ventaja = '" & ddlventajaedit.SelectedValue & "', WHERE id_codigo= " & lblHiddenIDDocumento.Value
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtAduana.Text)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/items_ventajas.aspx?acction=editdocumento&iditems=" & Request.QueryString("iditems") & Request.QueryString("idCaratula"))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Try
            Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=update&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttcontinuar_Click(sender As Object, e As EventArgs) Handles bttcontinuar.Click
        Try
            Response.Redirect("/modulos/declaracion_aduanera/creacion_documentos.aspx?idcaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttnuevonuevoitems_Click(sender As Object, e As EventArgs) Handles bttnuevonuevoitems.Click
        Try
            Response.Redirect("/modulos/declaracion_aduanera/items.aspx?action=new&idcaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try

    End Sub
End Class
