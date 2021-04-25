Public Class items_documentos
    Inherits System.Web.UI.Page
    '#OBJETO 33
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
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'cargar logo para imprimir
                HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

                lblitems.Text = Request.QueryString("iditems")


                'llenar grid
                Dim Ssql As String = String.Empty
                Ssql = "Select a.id_doc, a.Id_Documento, a.Referencia,
                    case when a.presencia =1 then 'SI' else 'NO' end presencia, a.Id_Merca, b.Descripcion
                    From tbl_41_documentos_items a, tbl_32_Cod_Documentos b
                    Where a.Id_Documento = b.Id_Documento  
                    and a.Id_merca =" & Request.QueryString("iditems") & ""

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
                            Session("IDfrmQueIngresa") = 33
                            Session("NombrefrmQueIngresa") = "Documentos para un Item"
                            If Not IsPostBack Then
                                Using log_bitacora As New ControlBitacora
                                    log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                End Using
                            End If
                    End Select
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarDocumento_Click(sender As Object, e As EventArgs) Handles bttGuardarDocumento.Click

        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_41_documentos_items where id_merca = '" & Request.QueryString("iditems") & "' and Referencia = '" & txtreferencia.Text & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','El documento ya esta registrado.', 'error');</script>")
            Else
                If chkPresencia.Checked = True Then
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_41_documentos_items (Id_Documento, id_merca, referencia, presencia) VALUES ('" & ddldocumentos.SelectedValue & "'," & Request.QueryString("iditems") & ",'" & txtreferencia.Text & "', '1');"
                Else
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_41_documentos_items (Id_Documento, id_merca, referencia, presencia) VALUES ('" & ddldocumentos.SelectedValue & "'," & Request.QueryString("iditems") & ",'" & txtreferencia.Text & "', '0');
"

                End If
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDAduna.Value)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/items_documentos.aspx?acction=newdocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarDocumento_Click(sender As Object, e As EventArgs) Handles bttEliminarDocumento.Click
        Try
            Dim Ssql As String = "DELETE FROM DB_Nac_Merca.tbl_41_documentos_items WHERE id_doc= " & lblHiddenIDDocumento.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la aduna con nombre: " & lblHiddenNombreAduna.Value & " con exito")
            'End Using

            Response.Redirect("~/modulos/declaracion_aduanera/items_documentos.aspx?acction=deldocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificardocumento_Click(sender As Object, e As EventArgs) Handles bttModificardocumento.Click
        Try
            Dim Ssql As String = String.Empty
            If dddocumentoEditar.SelectedValue <> lblHiddendddocumento.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_41_documentos_items where Id_Documento= " & lblHiddenIDDocumento.Value

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','El documento ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_41_documentos_items SET Id_Documento = '" & dddocumentoEditar.SelectedValue & "', Referencia = '" & txtreferenciaEditar.Text & "'  WHERE id_doc= " & lblHiddenIDDocumento.Value
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtAduana.Text)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/items_documentos.aspx?acction=editdocumento&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
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
            Response.Redirect("/modulos/declaracion_aduanera/items_dcomplementarios.aspx?iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub
End Class
