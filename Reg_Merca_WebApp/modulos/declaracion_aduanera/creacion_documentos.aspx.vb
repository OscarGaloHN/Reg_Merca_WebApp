﻿Public Class creacion_documentos
    Inherits System.Web.UI.Page
    '#OBJETO 30
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''parametros de configuracion de sistema
        'Using Parametros_Sistema As New ControlDB
        '    Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        'End Using

        ''PARAMETROS DE ADMINISTRADOR
        'Using Parametros_admin As New ControlDB
        '    Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        'End Using

        'Using logo_imprimir As New ControlDB
        '    Application("ParametrosADMIN")(22) = logo_imprimir.ConvertirIMG(Server.MapPath("~/images/" & Application("ParametrosADMIN")(22)))
        'End Using

        Try
            lblCatatura.Text = Request.QueryString("idCaratula")

            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else

                'llenar grid
                Dim Ssql As String = String.Empty
                Ssql = "Select a.id_doc, a.Id_Documento, a.Referencia, 
                    case when a.presencia =1 then 'SI' else 'NO' end presencia, a.id_poliza_doc, b.Descripcion
                    From tbl_28_Documentos a, tbl_32_Cod_Documentos b
                    Where a.Id_Documento = b.Id_Documento  and id_poliza_doc = " & Request.QueryString("idCaratula") & ""

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
                            Session("IDfrmQueIngresa") = 30
                            Session("NombrefrmQueIngresa") = "Creación de Documentos"
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
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_28_Documentos where id_poliza_doc = '" & Request.QueryString("idCaratula") & "' and Referencia = '" & txtreferencia.Text & "' "

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','La referencia ya esta registrada.', 'error');</script>")
            Else
                If chkPresencia.Checked = True Then
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_28_Documentos (Id_Documento, Id_poliza_doc, referencia, presencia) VALUES ('" & ddldocumentos.SelectedValue & "'," & Request.QueryString("idCaratula") & ",'" & txtreferencia.Text & "', '1');"
                Else
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_28_Documentos (Id_Documento, Id_poliza_doc, referencia, presencia) VALUES ('" & ddldocumentos.SelectedValue & "'," & Request.QueryString("idCaratula") & ",'" & txtreferencia.Text & "', '0');"

                End If
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El documento número " & lblHiddenIDDocumento.Value & "  de la carátula  " & Request.QueryString("idCaratula") & " se guardo con éxito.")
                End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?acction=newdocumento&idCaratula=" & Request.QueryString("idCaratula"))
            End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarDocumento_Click(sender As Object, e As EventArgs) Handles bttEliminarDocumento.Click
        Try
            Dim Ssql As String = "DELETE FROM DB_Nac_Merca.tbl_28_Documentos WHERE id_doc= " & lblHiddenIDDocumento.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El documento número " & lblHiddenIDDocumento.Value & "  de la carátula  " & Request.QueryString("idCaratula") & " se elimino con éxito.")
            End Using
            Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?acction=deldocumento&idCaratula=" & Request.QueryString("idCaratula"))

        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttModificardocumento_Click(sender As Object, e As EventArgs) Handles bttModificardocumento.Click

        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_28_Documentos where id_poliza_doc = '" & Request.QueryString("idCaratula") & "' and Referencia = '" & txtreferencia.Text & "' "


            If dddocumentoEditar.SelectedValue <> lblHiddendddocumento.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_28_Documentos where Id_Documento= " & lblHiddenIDDocumento.value

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','La referencia ya esta registrada.', 'error');</script>")
            Else
                If chkPresencia.Checked = True Then
                    Ssql = "UPDATE DB_Nac_Merca.tbl_28_Documentos SET Id_Documento = '" & dddocumentoEditar.SelectedValue & "', 
               Referencia = '" & txtreferenciaEditar.Text & "', presencia= '1'  WHERE id_doc= " & lblHiddenIDDocumento.Value

                Else
                    Ssql = "UPDATE DB_Nac_Merca.tbl_28_Documentos SET Id_Documento = '" & dddocumentoEditar.SelectedValue & "', 
               Referencia = '" & txtreferenciaEditar.Text & "', presencia= '0'  WHERE id_doc= " & lblHiddenIDDocumento.Value
                End If


                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El documento número " & lblHiddenIDDocumento.Value & "  de la carátula  " & Request.QueryString("idCaratula") & " se actualizo con éxito.")
                End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?acction=editdocumento&idCaratula=" & Request.QueryString("idCaratula"))
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Try
            Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttcontinuar_Click(sender As Object, e As EventArgs) Handles bttcontinuar.Click
        Try
            Response.Redirect("/modulos/declaracion_aduanera/creacion_bultos.aspx?idcaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

End Class
