Public Class items_dcomplementarios
    Inherits System.Web.UI.Page
    '#OBJETO 32
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 32 and permiso_consulta = 1"

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    'si tiene los permisos


                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

                    lblitems.Text = Request.QueryString("iditems")



                    If Session("user_idUsuario") = Nothing Then
                        Session.Abandon()
                        Response.Redirect("~/Inicio/login.aspx")
                    Else

                        'llenar grid
                        Ssql = "SELECT a.Id_DatoComple,b.descripcion,a.Valor,a.Id_Codigo, a.Id_Merca
                    from tbl_31_Cod_Datos_Complementarios b,tbl_10_Datos_Complementarios a
                    where a.Id_DatoComple=b.Id_DatoComple
                    and a.Id_Merca=" & Request.QueryString("iditems") & ""

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
                                Case "newdocumento"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documento','El dato complementario se almaceno con éxito.', 'success');</script>")
                                Case "deldocumento"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documento','El dato complementario se elimino con éxito.', 'success');</script>")
                                Case "editdocumento"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documento','El dato complementario se modifico con éxito.', 'success');</script>")
                                Case Else
                                    'bitacora de que salio de un form
                                    If Not IsPostBack Then
                                        Using log_bitacora As New ControlBitacora
                                            log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                        End Using
                                    End If

                                    'bitacora de que salio de un form
                                    If Not IsPostBack Then
                                        Using log_bitacora As New ControlBitacora
                                            log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                        End Using
                                    End If

                                    'bitacora de que ingreso al form
                                    Session("IDfrmQueIngresa") = 32
                                    Session("NombrefrmQueIngresa") = "Datos Complementarios"
                                    If Not IsPostBack Then
                                        Using log_bitacora As New ControlBitacora
                                            log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                        End Using
                                    End If
                            End Select
                        End If

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

    Private Sub bttGuardarDocumento_Click(sender As Object, e As EventArgs) Handles bttGuardarDocumento.Click

        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_10_Datos_Complementarios where Id_Merca = '" & Request.QueryString("iditems") & "' and valor = '" & txtvalor.Text & "' "

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','El dato complementario ya esta registrado.', 'error');</script>")
            Else

                Ssql = "INSERT INTO DB_Nac_Merca.tbl_10_Datos_Complementarios (Valor, Id_DatoComple, Id_Merca) VALUES ('" & txtvalor.Text & "','" & ddlcomplementario.SelectedValue & "'," & Request.QueryString("iditems") & "); "
                'Using con As New ControlDB
                '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                'End Using
            End If
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDAduna.Value)
            'End Using
            Response.Redirect("~/modulos/declaracion_aduanera/items_dcomplementarios.aspx?acction=newdocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
            'End If




        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarDocumento_Click(sender As Object, e As EventArgs) Handles bttEliminarDocumento.Click
        Try
            Dim Ssql As String = "DELETE FROM DB_Nac_Merca.tbl_10_Datos_Complementarios where Id_Codigo = " & lblHiddenIDDocumento.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la aduna con nombre: " & lblHiddenNombreAduna.Value & " con exito")
            'End Using

            Response.Redirect("~/modulos/declaracion_aduanera/items_dcomplementarios.aspx?acction=deldocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))

        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub bttModificardocumento_Click(sender As Object, e As EventArgs) Handles bttModificardocumento.Click

        Try
            Dim Ssql As String = String.Empty
            If ddlcomplementariedit.SelectedValue <> lblHiddendddocumento.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_10_Datos_Complementarios where Id_DatoComple = " & lblHiddenIDDocumento.Value

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Item Complementario','El item complementario ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_10_Datos_Complementarios SET Id_DatoComple = '" & ddlcomplementariedit.SelectedValue & "', valor = '" & txtvaloredit.Text & "'  WHERE Id_Codigo= " & lblHiddenIDDocumento.Value
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtAduana.Text)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/items_dcomplementarios.aspx?acction=editdocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
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
            Response.Redirect("/modulos/declaracion_aduanera/items_ventajas.aspx?iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub
End Class