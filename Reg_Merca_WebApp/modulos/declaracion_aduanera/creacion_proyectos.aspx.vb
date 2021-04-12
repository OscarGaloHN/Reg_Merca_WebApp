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

            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else
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
            'Dim Ssql As String = String.Empty
            'Ssql = "select a.Id_poliza,a.fecha_creacion,b.nombrec,c.descripcion,d.nombre
            '                from tbl_04_cliente b, tbl_01_polizas a, tbl_19_estado c, tbl_02_usuarios d
            '                   where a.id_cliente = b.id_cliente and c.id_estado=a.estado_poliza
            '                   and d.id_usuario= a.usuario_creador"

            'Using con As New ControlDB
            '    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            '    Session("NumReg") = DataSetX.Tables(0).Rows.Count
            'End Using
            'If Session("NumReg") > 0 Then
            '    gvCustomers.DataSource = DataSetX
            '    gvCustomers.DataBind()
            'End If
        Catch ex As Exception

        End Try

    End Sub



End Class
