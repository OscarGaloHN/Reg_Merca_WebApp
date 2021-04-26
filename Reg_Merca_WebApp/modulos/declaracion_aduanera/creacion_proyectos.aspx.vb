Public Class creacion_proyectos
    Inherits System.Web.UI.Page
    '#OBJETO 31
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
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else
                If Not IsPostBack Then
                    'Llenado de Gried
                    Dim Ssql As String = String.Empty
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador"

                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        gvCustomers.DataSource = DataSetX
                        gvCustomers.DataBind()
                    End If
                End If

                If Not IsPostBack Then
                    Select Case Request.QueryString("acction")
                        Case "new"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Caratula','La caratula se almaceno con éxito.', 'success');</script>")
                        Case "update"
                            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Caratula','La caratula se modifico con éxito.', 'success');</script>")
                        Case Else
                            'bitacora de que salio de un form
                            If Not IsPostBack Then
                                Using log_bitacora As New ControlBitacora
                                    log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                End Using
                            End If

                            'bitacora de que ingreso al form
                            Session("IDfrmQueIngresa") = 31
                            Session("NombrefrmQueIngresa") = "Creación de Proyectos"
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

    Private Sub bttNuevo_Click(sender As Object, e As EventArgs) Handles bttNuevo.Click
        Try
            'redirecciona a form caratula
            Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=new")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btt_buscar_Click(sender As Object, e As EventArgs) Handles btt_buscar.Click
        Try
            Dim Ssql As String = String.Empty
            Dim xInicio As String = fechaInicio.Value.ToString.Substring(6, 4) & fechaInicio.Value.ToString.Substring(0, 2) & fechaInicio.Value.ToString.Substring(3, 2)
            Dim xFin As String = fechaFin.Value.ToString.Substring(6, 4) & fechaFin.Value.ToString.Substring(0, 2) & fechaFin.Value.ToString.Substring(3, 2)

            Select Case ddlestado.SelectedIndex

                Case 0
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador AND  convert(a.fecha_creacion, DATE) BETWEEN '" & xInicio & "' 
                               AND '" & xFin & "'"
                Case Else
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador
                               and a.estado_poliza=" & ddlestado.SelectedValue & " 
                               AND  convert(a.fecha_creacion, DATE) BETWEEN '" & xInicio & "' 
                               AND '" & xFin & "'"
            End Select

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            Else
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub ddlestado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlestado.SelectedIndexChanged
        Dim Ssql As String = String.Empty

        'Llenado de Gried
        If fechaInicio.Value.ToString <> "" Then
            Dim xInicio As String = fechaInicio.Value.ToString.Substring(6, 4) & fechaInicio.Value.ToString.Substring(0, 2) & fechaInicio.Value.ToString.Substring(3, 2)
            Dim xFin As String = fechaFin.Value.ToString.Substring(6, 4) & fechaFin.Value.ToString.Substring(0, 2) & fechaFin.Value.ToString.Substring(3, 2)

            Select Case ddlestado.SelectedIndex

                Case 0
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador AND  convert(a.fecha_creacion, DATE) BETWEEN '" & xInicio & "' 
                               AND '" & xFin & "'"
                Case Else
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador
                               and a.estado_poliza=" & ddlestado.SelectedValue & " 
                               AND  convert(a.fecha_creacion, DATE) BETWEEN '" & xInicio & "' 
                               AND '" & xFin & "'"
            End Select
        Else


            Select Case ddlestado.SelectedIndex

                Case 0
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador"
                Case Else
                    Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador
                               and a.estado_poliza=" & ddlestado.SelectedValue & ""
            End Select
        End If

        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            gvCustomers.DataSource = DataSetX
            gvCustomers.DataBind()
        Else
            gvCustomers.DataSource = Nothing
            gvCustomers.DataBind()
        End If
    End Sub

    Private Sub btt_limpiar_Click(sender As Object, e As EventArgs) Handles btt_limpiar.Click
        Try
            fechaFin.Value = Nothing
            fechaInicio.Value = Nothing
            txt_cliente.Text = ""
            txt_usuario.Text = ""
            ddlestado.SelectedIndex = 0
            Dim Ssql As String = String.Empty
            Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
                            from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
                               where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
                               and d.id_usuario= a.usuario_creador"

            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            Else
                gvCustomers.DataSource = Nothing
                gvCustomers.DataBind()
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
