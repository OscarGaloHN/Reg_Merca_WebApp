Public Class items_dcomplementarios
    Inherits System.Web.UI.Page
    '#OBJETO 
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

        Using logo_imprimir As New ControlDB
            Application("ParametrosADMIN")(22) = logo_imprimir.ConvertirIMG(Server.MapPath("~/images/" & Application("ParametrosADMIN")(22)))
        End Using

        Try


            ''If Session("user_idUsuario") = Nothing Then
            '    Session.Abandon()
            '    Response.Redirect("~/Inicio/login.aspx")
            'Else
            'End If
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT a.Id_DatoComple,b.descripcion,a.Valor,a.Id_Codigo
                    from tbl_31_Cod_Datos_Complementarios b,tbl_10_Datos_Complementarios a
                    where a.Id_DatoComple=b.Id_DatoComple
                    and a.Id_Merca=5 "

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


        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttGuardarDocumento_Click(sender As Object, e As EventArgs) Handles bttGuardarDocumento.Click

        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_10_Datos_Complementarios where Id_Merca = '" & Request.QueryString("idCaratula") & "' and Id_DatoComple = '" & ddlcomplementario.SelectedValue & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Documentos','El documento ya esta registrado.', 'error');</script>")
            Else

                Ssql = "INSERT INTO DB_Nac_Merca.tbl_10_Datos_Complementarios (Valor, Id_DatoComple, Id_Merca) VALUES ('" & txtvalor.Text & "'," & ddlcomplementario.SelectedValue & ",'" & Request.QueryString("idCaratula") & "');"

            End If
            '                Using con As New ControlDB
            '                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            '                End Using
            '                'Using log_bitacora As New ControlBitacora
            '                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDAduna.Value)
            '                'End Using
            '                Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?acction=newdocumento&idCaratula=" & Request.QueryString("idCaratula"))
            '            'End If

            '            'habilitar indicador
            '            'If chkindicador.Checked = True Then
            '            '    Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'TRUE' where id_parametro =35"
            '            'Else
            '            '    Ssql = "UPDATE DB_Nac_Merca.tbl_21_parametros SET valor = 'FALSE' where id_parametro =35"
            '            'End If



        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttEliminarDocumento_Click(sender As Object, e As EventArgs) Handles bttEliminarDocumento.Click
        Try
            Dim Ssql As String = "DELETE from tbl_31_Cod_Datos_Complementarios WHERE Id_Codigo= " & lblHiddenIDDocumento.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la aduna con nombre: " & lblHiddenNombreAduna.Value & " con exito")
            'End Using

            Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?acction=deldocumento&idCaratula=" & Request.QueryString("idCaratula"))

        Catch ex As Exception

        End Try
        
    End Sub

    Private Sub bttModificardocumento_Click(sender As Object, e As EventArgs) Handles bttModificardocumento.Click

        Try
            Dim Ssql As String = String.Empty
            If txtvaloredit.Text <> lblHiddenIDDocumento.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_10_Datos_Complementarios where Valor = '" & txtvalor.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Aduanas','El documento ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_10_Datos_Complementarios SET Id_Codigo = '" & ddlcomplementario.SelectedValue & "', Referencia = '" & txtvalor.Text & "'  WHERE Id_DatoComple= " & lblHiddenIDDocumento.Value
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtAduana.Text)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?acction=editdocumento&idCaratula=" & Request.QueryString("idCaratula"))
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class