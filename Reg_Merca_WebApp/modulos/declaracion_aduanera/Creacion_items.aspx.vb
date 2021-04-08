Public Class Creacion_items
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

        Using logo_imprimir As New ControlDB
            Application("ParametrosADMIN")(22) = logo_imprimir.ConvertirIMG(Server.MapPath("~/images/" & Application("ParametrosADMIN")(22)))
        End Using


        Try


            'If Session("user_idUsuario") = Nothing Then
            '    Session.Abandon()
            '    Response.Redirect("~/Inicio/login.aspx")
            'Else
            'Llenado de Gried
            Dim Ssql As String = String.Empty
            Ssql = "select a.numero_item,a.pesoneto,a.num_partida,a.cod_pais_fab,a.importes_factura
                        from tbl_34_mercancias a,tbl_01_polizas b 
                        where a.Id_poliza=b.Id_poliza"

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
                        Case "new"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('items','La caratula se almaceno con éxito.', 'success');</script>")
                        Case "edit"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('items','La caratula se modifico con éxito.', 'success');</script>")
                        Case Else
                            ''bitacora de que salio de un form
                            'If Not IsPostBack Then
                            '    Using log_bitacora As New ControlBitacora
                            '        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            '    End Using
                            'End If

                            ''bitacora de que ingreso al form
                            'Session("IDfrmQueIngresa") = 7
                            'Session("NombrefrmQueIngresa") = "Gestión de usuarios"
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


    Private Sub bttNuevo_Click(sender As Object, e As EventArgs) Handles bttNuevo.Click
        Try
            'redirecciona a form caratula
            Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=new")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btt_volver_Click(sender As Object, e As EventArgs) Handles btt_volver.Click
        Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx")
    End Sub
End Class