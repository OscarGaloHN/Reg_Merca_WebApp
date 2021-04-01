Public Class items
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Using Parametros_Sistema As New ControlDB
        '    Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        'End Using

        ''PARAMETROS DE ADMINISTRADOR
        'Using Parametros_admin As New ControlDB
        '    Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        'End Using




        'Try

        '    If Session("user_idUsuario") = Nothing Then
        '        Session.Abandon()
        '        Response.Redirect("~/Inicio/login.aspx")
        '    Else


        '        Select Case Request.QueryString("iditems")

        '            Case "update"


        '                If Not IsPostBack Then

        '                    Dim Ssql As String = String.Empty
        '                    Ssql = "select * from DB_Nac_Merca.tbl_34_mercancias where id_merca =" & Request.QueryString("iditems") & ""

        '                    Using con As New ControlDB
        '                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        '                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
        '                    End Using
        '                    Dim registro As DataRow
        '                    If Session("NumReg") > 0 Then
        '                        'cargar txt
        '                        registro = DataSetX.Tables(0).Rows(0)
        '                        txtdeclarante.Text = registro("declarante")
        '                        ddladuanadespacho.SelectedValue = registro("cod_aduana_ent")
        '                        ddlregimenaduanero.SelectedValue = registro("id_regimen")
        '                        txtrtnimp_exp.Text = registro("rtn_importador")
        '                        txtimp_exp.Text = registro("nombre_importador")
        '                        txtRTNagen_aduanera.Text = registro("rtn_agenciaadu")
        '                        txtagen_aduanera.Text = registro("nombre_agenciaadu")
        '                        txtmanifiestorap.Text = registro("manifiesto_entregarap")
        '                        txtNproveedor.Text = registro("Id_proveedor")
        '                        txtContra_proveedor.Text = registro("contrato_proveedor")
        '                        txtDomicioProve.Text = registro("domicilio_proveed")
        '                        txtNumPreimp.Text = registro("Numero_Preimpreso")
        '                        txtEntidadMed.Text = registro("entidad_mediacion")
        '                        ddldepositoalmacen.SelectedValue = registro("Id_almacen")
        '                        ddladuanaingsal.SelectedValue = registro("cod_aduana_sal")
        '                        ddlpaisesdeorigen.SelectedValue = registro("Cod_pais_org")
        '                        ddlpaisprocedencia.SelectedValue = registro("Cod_pais_pro")
        '                        ddlformadepago.SelectedValue = registro("id_pago")
        '                        ddlcondicionentrega.SelectedValue = registro("id_condicion")
        '                        ddladuanatransitodes.SelectedValue = registro("aduana_transdes")
        '                        ddlmodalidadesp.SelectedValue = registro("modalidad_especial")
        '                        ddldepositoaduana.SelectedValue = registro("deposito_aduanas")
        '                        txtplazodiasmeses.Text = registro("plazo")
        '                        txtrutatransito.Text = registro("ruta_transito")
        '                        ddlmotivooper.SelectedValue = registro("motivo_operacion")
        '                        txtobservacion.Text = registro("Observaciones")
        '                        ddlclasebultos.SelectedValue = registro("Id_Clase_deBulto")
        '                        txtcantbultos.Text = registro("cant_bultos")
        '                        txtpesobrutobul.Text = registro("pesobruto_bultos")
        '                        txttotalitems.Text = registro("canti_items")
        '                        txttotalfact.Text = registro("Total_Factura")
        '                        txttotalotrosgast.Text = registro("Total_Otros_gastos")
        '                        txtttotalseg.Text = registro("Total_Seguro")
        '                        txttotalflet.Text = registro("Total_Flete")
        '                        ddldivisafact.SelectedValue = registro("divisa_factura")
        '                        txttipodecambio.Text = registro("tipo_de_cambio")
        '                        ddldivisaseg.SelectedValue = registro("divisa_seguro")
        '                        ddldivisafl.SelectedValue = registro("divisa_flete")
        '                    End If
        '                End If
        '            Case Else
        '                Response.Redirect("~/modulos/declaracion_aduanera/creacion_proyectos.aspx")
        '        End Select

        '    End If



        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub bttgaurdar_Click(sender As Object, e As EventArgs) Handles bttgaurdar.Click
'        Dim Ssql As String
    '        Try
    '            'inhabilita Panel de botones
    '            pbotones.Enabled = True

    '            Select Case Request.QueryString("action")
    '                Case "new"

    '                    Ssql = "Insert into DB_Nac_Merca.tbl_01_polizas 
    '                  (declarante, cod_aduana_ent, Id_regimen, rtn_importador, nombre_importador, rtn_agenciaadu, nombre_agenciaadu, manifiesto_entregarap, Id_proveedor, contrato_proveedor,
    'domicilio_proveed, Numero_Preimpreso, entidad_mediacion, Id_almacen, cod_aduana_sal, Cod_pais_org, Cod_pais_pro, id_pago, id_condicion, aduana_transdes, modalidad_especial, deposito_aduanas, plazo,
    'ruta_transito, motivo_operacion, Observaciones, Id_Clase_deBulto, cant_bultos, pesobruto_bultos, canti_items, Total_Factura, Total_Otros_gastos, Total_Seguro, Total_Flete, divisa_factura, tipo_de_cambio,
    'divisa_seguro, divisa_flete, Id_cliente) 
    'values ('" & txtdeclarante.Text & "', '" & ddladuanadespacho.SelectedValue & "', " & ddlregimenaduanero.SelectedValue & ", '" & txtrtnimp_exp.Text & "', 
    ' '" & txtimp_exp.Text & "','" & txtRTNagen_aduanera.Text & "','" & txtagen_aduanera.Text & "','" & txtmanifiestorap.Text & "','" & txtNproveedor.Text & "','" & txtContra_proveedor.Text & "'
    ','" & txtDomicioProve.Text & "','" & txtNumPreimp.Text & "','" & txtEntidadMed.Text & "','" & ddldepositoalmacen.SelectedValue & "','" & ddladuanaingsal.SelectedValue & "'
    ','" & ddlpaisesdeorigen.SelectedValue & "','" & ddlpaisprocedencia.SelectedValue & "','" & ddlformadepago.SelectedValue & "','" & ddlcondicionentrega.SelectedValue & "'
    ','" & ddladuanatransitodes.SelectedValue & "','" & ddlmodalidadesp.SelectedValue & "','" & ddldepositoaduana.SelectedValue & "','" & txtplazodiasmeses.Text & "'
    ','" & txtrutatransito.Text & "','" & ddlmotivooper.SelectedValue & "','" & txtobservacion.Text & "','" & ddlclasebultos.SelectedValue & "','" & txtcantbultos.Text & "'
    ','" & txtpesobrutobul.Text & "','" & txttotalitems.Text & "','" & txttotalfact.Text & "','" & txttotalotrosgast.Text & "','" & txtttotalseg.Text & "','" & txttotalflet.Text & "'
    ','" & ddldivisafact.SelectedValue & "','" & txttipodecambio.Text & "','" & ddldivisaseg.SelectedValue & "','" & ddldivisafl.SelectedValue & "', '" & cmbCliente.SelectedValue & "')"
    '                    Using con As New ControlDB
    '                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
    '                    End Using

    '                    'recupera el id de la caratura


    '                    Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratura=" & "ID_CARATURA_CREADA")


    '                    'Case "update"
    '                    '    If txtCorreoElectronico.Text = HiddenCorreo.Value Then
    '                    '        ''GUARDAR NORMAL
    '                    '        'TERMINAR DE EDITAR 
    '                    '        Ssql = "update tbl_02_usuarios set Nombre='" & txtNombre.Text & "',correo='" & txtCorreoElectronico.Text & "' where id_usuario =" & Request.QueryString("xuser") & ""
    '                    '        Using con As New ControlDB
    '                    '            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
    '                    '        End Using


    '                    '        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=UsuarioActualizado")

    '                    '    Else



    '                    '        'COMPLETAR QRY UPDATE
    '                    '        '    Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  Nombre='" & txtNombre.Text & "',  correo = '" & txtCorreoElectronico.Text & "' where id_usuario =" & Request.QueryString("xuser") & ""
    '                    '        '    Using con As New ControlDB
    '                    '        '        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
    '                    '        '    End Using

    '                    '        'Ssql = "UPDATE DB_Nac_Merca.tbl_02_usuarios  SET  emailconfir = 0 where  id_usuario =" & Request.QueryString("xuser") & ""
    '                    '        '    Using con As New ControlDB
    '                    '        '        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
    '                    '        '    End Using


    '                    '        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=UsuarioActualizado")
    '                    '        End If
    '                    '    End If
    '            End Select
    '        Catch ex As Exception

    '        End Try

    '    End Sub

End Class