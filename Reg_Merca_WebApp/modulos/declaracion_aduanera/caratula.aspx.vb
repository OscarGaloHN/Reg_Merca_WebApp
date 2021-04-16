Public Class caratula
    Inherits System.Web.UI.Page
    '#OBJETO 16
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
            'parametros de configuracion de sistema
            Using Parametros_Sistema As New ControlDB
                Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
            End Using

            'PARAMETROS DE ADMINISTRADOR
            Using Parametros_admin As New ControlDB
                Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
            End Using
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'pbotones.Enabled = False
                Dim Ssql As String = String.Empty

                Ssql = "select * from DB_Nac_Merca.tbl_04_cliente where Id_cliente='" & ddlCliente.SelectedValue & "'"
                Using con As New ControlDB
                    con.GME_Recuperar_ID(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using


                Select Case Request.QueryString("action")
                    Case "new"
                        Dim fechaactual As Date = (Date.Now)
                        txtFechaCreacion.Text = fechaactual
                        ddlestado.Enabled = False
                        'inhabilita Panel de botones
                        ddlestado.SelectedValue = 7
                    Case "update"
                        'habilita Panel de botones
                        pbotones.Visible = True
                        pactualizar.Visible = True
                        If Not IsPostBack Then

                            Ssql = "select * from DB_Nac_Merca.tbl_01_polizas where id_poliza =" & Request.QueryString("idCaratula") & ""

                            Using con As New ControlDB
                                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                Session("NumReg") = DataSetX.Tables(0).Rows.Count
                            End Using
                            Dim registro As DataRow
                            If Session("NumReg") > 0 Then
                                'cargar txt
                                registro = DataSetX.Tables(0).Rows(0)
                                txtFechaCreacion.Text = registro("fecha_creacion")
                                'ddlestado.SelectedValue = registro("estado_poliza")
                                'Session("estado_temp") = registro("estado_poliza")
                                'ddlestado.Attributes.Add("disabled", "disabled")
                                ddlestado.SelectedValue = 7
                                ddlCliente.SelectedValue = registro("id_cliente")
                                ddldeclarante.SelectedValue = registro("declarante")
                                ddladuanadespacho.SelectedValue = registro("cod_aduana_ent")
                                ddlregimenaduanero.SelectedValue = registro("id_regimen")
                                txtrtnimp_exp.Text = registro("rtn_importador")
                                'txtimp_exp.Text = registro("nombre_importador")
                                txtRTNagen_aduanera.Text = registro("rtn_agenciaadu")
                                txtagen_aduanera.Text = registro("nombre_agenciaadu")
                                txtmanifiestorap.Text = registro("manifiesto_entregarap")
                                ddlproveedores.SelectedValue = registro("Id_proveedor")
                                txtContra_proveedor.Text = registro("contrato_proveedor")
                                txtDomicioProve.Text = registro("domicilio_proveed")
                                txtNumPreimp.Text = registro("Numero_Preimpreso")
                                txtEntidadMed.Text = registro("entidad_mediacion")
                                ddldepositoalmacen.SelectedValue = registro("Id_almacen")
                                ddladuanaingsal.SelectedValue = registro("cod_aduana_sal")
                                ddlpaisesdeorigen.SelectedValue = registro("Cod_pais_org")
                                ddlpaisprocedencia.SelectedValue = registro("Cod_pais_pro")
                                ddlformadepago.SelectedValue = registro("id_pago")
                                ddlcondicionentrega.SelectedValue = registro("id_condicion")
                                ddladuanatransitodes.SelectedValue = registro("aduana_transdes")
                                ddlmodalidadesp.SelectedValue = registro("modalidad_especial")
                                ddldepositoaduana.SelectedValue = registro("deposito_aduanas")
                                txtplazodiasmeses.Text = registro("plazo")
                                txtrutatransito.Text = registro("ruta_transito")
                                txt_motivoperacion.Text = registro("motivo_operacion")
                                txtobservacion.Text = registro("Observaciones")
                                ddlclasebultos.SelectedValue = registro("Id_Clase_deBulto")
                                'txtcantbultos.Text = registro("cant_bultos")
                                'txtpesobrutobul.Text = registro("pesobruto_bultos")
                                'txttotalitems.Text = registro("canti_items")
                                'txttotalfact.Text = registro("Total_Factura")
                                txttotalotrosgast.Text = registro("Total_Otros_gastos")
                                txtttotalseg.Text = registro("Total_Seguro")
                                txttotalflet.Text = registro("Total_Flete")
                                ddldivisafact.SelectedValue = registro("divisa_factura")
                                txttipodecambio.Text = registro("tipo_de_cambio")
                                ddldivisaseg.SelectedValue = registro("divisa_seguro")
                                ddldivisafl.SelectedValue = registro("divisa_flete")

                            End If
                        End If
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 16
                        Session("NombrefrmQueIngresa") = "Caratula"
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                End Select

            End If

        Catch ex As Exception

        End Try

    End Sub


    Private Sub btt_guardar_Click(sender As Object, e As EventArgs) Handles btt_guardar.Click
        Dim Ssql As String
        Try

            Select Case Request.QueryString("action")
                Case "new"
                    Ssql = "Insert into DB_Nac_Merca.tbl_01_polizas 
(fecha_creacion,estado_poliza, Id_cliente, declarante, cod_aduana_ent, Id_regimen, rtn_importador, 
rtn_agenciaadu, nombre_agenciaadu, manifiesto_entregarap, Id_proveedor, contrato_proveedor,
domicilio_proveed, Numero_Preimpreso, entidad_mediacion, Id_almacen, cod_aduana_sal, Cod_pais_org, 
Cod_pais_pro, id_pago, id_condicion, aduana_transdes, modalidad_especial, deposito_aduanas, plazo,
ruta_transito, motivo_operacion, Observaciones, Id_Clase_deBulto, Total_Otros_gastos, Total_Seguro, Total_Flete, divisa_factura, tipo_de_cambio,
divisa_seguro, divisa_flete, usuario_creador, Id_poliza) 
values (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'),'" & ddlestado.SelectedValue & "', '" & ddlCliente.SelectedValue & "',
'" & ddldeclarante.SelectedValue & "', '" & ddladuanadespacho.SelectedValue & "', '" & ddlregimenaduanero.SelectedValue & "', 
'" & txtrtnimp_exp.Text & "', '" & txtRTNagen_aduanera.Text & "','" & txtagen_aduanera.Text & "',
'" & txtmanifiestorap.Text & "','" & ddlproveedores.SelectedValue & "','" & txtContra_proveedor.Text & "','" & txtDomicioProve.Text & "',
'" & txtNumPreimp.Text & "','" & txtEntidadMed.Text & "','" & ddldepositoalmacen.SelectedValue & "','" & ddladuanaingsal.SelectedValue & "',
'" & ddlpaisesdeorigen.SelectedValue & "','" & ddlpaisprocedencia.SelectedValue & "','" & ddlformadepago.SelectedValue & "',
'" & ddlcondicionentrega.SelectedValue & "','" & ddladuanatransitodes.SelectedValue & "','" & ddlmodalidadesp.SelectedValue & "',
'" & ddldepositoaduana.SelectedValue & "','" & txtplazodiasmeses.Text & "','" & txtrutatransito.Text & "',
'" & txt_motivoperacion.Text & "','" & txtobservacion.Text & "','" & ddlclasebultos.SelectedValue & "',
'" & txttotalotrosgast.Text & "','" & txtttotalseg.Text & "','" & txttotalflet.Text & "','" & ddldivisafact.SelectedValue & "',
'" & txttipodecambio.Text & "','" & ddldivisaseg.SelectedValue & "','" & ddldivisafl.SelectedValue & "',
'" & Session("user_idUsuario") & "', '" & Session("idCaratula") & "'); SELECT LAST_INSERT_ID();"



                    Using con As New ControlDB
                        con.GME_Recuperar_ID(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=update&idCaratula=" & Session("GME_Recuperar_ID"))

                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using

                    If Session("NumReg") > 0 Then
                        'Using log_bitacora As New ControlBitacora
                        '    log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 13, "El correo " & txtCorreoElectronico.Text & " ya esta registrado")
                        'End Using
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Carátula','La carátula se almacenó con éxito.', 'success');</script>")
                    End If

                    'Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratula=" & Session("GME_Recuperar_ID"))

                Case "update"
                    Ssql = "update DB_Nac_Merca.tbl_01_polizas set Id_cliente= '" & ddlCliente.SelectedValue & "', 
                    declarante='" & ddldeclarante.SelectedValue & "', cod_aduana_ent='" & ddladuanadespacho.SelectedValue & "', 
                    Id_regimen='" & ddlregimenaduanero.SelectedValue & "', rtn_importador='" & txtrtnimp_exp.Text & "', 
                    rtn_agenciaadu='" & txtRTNagen_aduanera.Text & "', nombre_agenciaadu='" & txtagen_aduanera.Text & "', 
                    manifiesto_entregarap='" & txtmanifiestorap.Text & "', Id_proveedor='" & ddlproveedores.SelectedValue & "',
                    contrato_proveedor='" & txtContra_proveedor.Text & "',domicilio_proveed='" & txtDomicioProve.Text & "',
                    Numero_Preimpreso='" & txtNumPreimp.Text & "', entidad_mediacion='" & txtEntidadMed.Text & "', Id_almacen='" & ddldepositoalmacen.SelectedValue & "',
                    cod_aduana_sal='" & ddladuanaingsal.SelectedValue & "', Cod_pais_org='" & ddlpaisesdeorigen.SelectedValue & "', 
                    Cod_pais_pro='" & ddlpaisprocedencia.SelectedValue & "', id_pago='" & ddlformadepago.SelectedValue & "', 
                    id_condicion='" & ddlcondicionentrega.SelectedValue & "', aduana_transdes='" & ddladuanatransitodes.SelectedValue & "', 
                    modalidad_especial='" & ddlmodalidadesp.SelectedValue & "', deposito_aduanas='" & ddldepositoaduana.SelectedValue & "', 
                    plazo='" & txtplazodiasmeses.Text & "', ruta_transito='" & txtrutatransito.Text & "', 
                    motivo_operacion='" & txt_motivoperacion.Text & "', Observaciones='" & txtobservacion.Text & "', 
                    Id_Clase_deBulto='" & ddlclasebultos.SelectedValue & "', Total_Otros_gastos='" & txttotalotrosgast.Text & "', 
                    Total_Seguro='" & txtttotalseg.Text & "', Total_Flete='" & txttotalflet.Text & "', divisa_factura='" & ddldivisafact.SelectedValue & "', 
                    tipo_de_cambio='" & txttipodecambio.Text & "', divisa_seguro='" & ddldivisaseg.SelectedValue & "', 
                    divisa_flete='" & ddldivisafl.SelectedValue & "', usuario_creador='" & Session("user_idUsuario") & "'"

                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
            End Select

        Catch ex As Exception

        End Try

    End Sub

    Private Sub bttActualizar_Click(sender As Object, e As EventArgs) Handles bttActualizar.Click
        'Dim Ssql As String
        Try

            '            Select Case Request.QueryString("action")
            '                Case "update"
            '                    Ssql = "update DB_Nac_Merca.tbl_01_polizas set Id_cliente= '" & ddlCliente.SelectedValue & "', 
            'declarante='" & ddldeclarante.SelectedValue & "', cod_aduana_ent='" & ddladuanadespacho.SelectedValue & "', 
            'Id_regimen='" & ddlregimenaduanero.SelectedValue & "', rtn_importador='" & txtrtnimp_exp.Text & "', 
            'rtn_agenciaadu='" & txtRTNagen_aduanera.Text & "', nombre_agenciaadu='" & txtagen_aduanera.Text & "', 
            'manifiesto_entregarap='" & txtmanifiestorap.Text & "', Id_proveedor='" & ddlproveedores.SelectedValue & "',
            'contrato_proveedor='" & txtContra_proveedor.Text & "',domicilio_proveed='" & txtDomicioProve.Text & "',
            'Numero_Preimpreso='" & txtNumPreimp.Text & "', entidad_mediacion='" & txtEntidadMed.Text & "', Id_almacen='" & ddldepositoalmacen.SelectedValue & "',
            'cod_aduana_sal='" & ddladuanaingsal.SelectedValue & "', Cod_pais_org='" & ddlpaisesdeorigen.SelectedValue & "', 
            'Cod_pais_pro='" & ddlpaisprocedencia.SelectedValue & "', id_pago='" & ddlformadepago.SelectedValue & "', 
            'id_condicion='" & ddlcondicionentrega.SelectedValue & "', aduana_transdes='" & ddladuanatransitodes.SelectedValue & "', 
            'modalidad_especial='" & ddlmodalidadesp.SelectedValue & "', deposito_aduanas='" & ddldepositoaduana.SelectedValue & "', 
            'plazo='" & txtplazodiasmeses.Text & "', ruta_transito='" & txtrutatransito.Text & "', 
            'motivo_operacion='" & txt_motivoperacion.Text & "', Observaciones='" & txtobservacion.Text & "', 
            'Id_Clase_deBulto='" & ddlclasebultos.SelectedValue & "', Total_Otros_gastos='" & txttotalotrosgast.Text & "', 
            'Total_Seguro='" & txtttotalseg.Text & "', Total_Flete='" & txttotalflet.Text & "', divisa_factura='" & ddldivisafact.SelectedValue & "', 
            'tipo_de_cambio='" & txttipodecambio.Text & "', divisa_seguro='" & ddldivisaseg.SelectedValue & "', 
            'divisa_flete='" & ddldivisafl.SelectedValue & "', usuario_creador='" & Session("user_idUsuario") & "'"

            '                    'Using con As New ControlDB
            '                    '    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            '                    'End Using
            '                    'Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
            '            End Select
        Catch ex As Exception

        End Try

    End Sub


    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Try
            Response.Redirect("~/modulos/declaracion_aduanera/creacion_proyectos.aspx")
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btt_bultos_Click(sender As Object, e As EventArgs) Handles btt_bultos.Click
        Try
            Response.Redirect("/modulos/declaracion_aduanera/creacion_bultos.aspx?idcaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try

    End Sub
    Private Sub bttdocumen_Click(sender As Object, e As EventArgs) Handles bttdocumen.Click
        Try
            Response.Redirect("~/modulos/declaracion_aduanera/creacion_documentos.aspx?idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttitems_Click(sender As Object, e As EventArgs) Handles bttitems.Click
        Try
            'redirecciona a form items
            'Session("IdCaratulaEditor") = Request.QueryString("idCaratula")
            Response.Redirect("~/modulos/declaracion_aduanera/Creacion_items.aspx?idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        Dim ssql As String
        Try
            'Select Case a la BD a la tabla para llenar el RTN
            'pruebas 1
            If ddlCliente.SelectedValue = txtrtnimp_exp.Text Then
            Else
                ssql = "select rtn_cli from DB_Nac_Merca.tbl_04_cliente where Id_cliente= '" & ddlCliente.SelectedValue & "'"
                Using con As New ControlDB
                    DataSetX = con.SelectX(ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                Dim registro As DataRow
                If Session("NumReg") > 0 Then
                    registro = DataSetX.Tables(0).Rows(0)
                    txtrtnimp_exp.Text = registro("rtn_cli")
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddldeclarante_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldeclarante.SelectedIndexChanged
        Dim ssql As String
        Try
            'Select Case a la BD a la tabla para llenar el RTN
            'pruebas 1And txtRTNagen_aduanera.Text 
            If ddldeclarante.SelectedValue = txtagen_aduanera.Text Then
            Else
                ssql = "select nombre_agencia from DB_Nac_Merca.tbl_43_declarante where id_declarante= '" & ddldeclarante.SelectedValue & "'"
                Using con As New ControlDB
                    DataSetX = con.SelectX(ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                Dim registro As DataRow
                If Session("NumReg") > 0 Then
                    registro = DataSetX.Tables(0).Rows(0)
                    txtagen_aduanera.Text = registro("nombre_agencia")
                End If
            End If

            If ddldeclarante.SelectedValue = txtRTNagen_aduanera.Text Then
            Else
                ssql = "select rtn_agencia from DB_Nac_Merca.tbl_43_declarante where id_declarante= '" & ddldeclarante.SelectedValue & "'"
                Using con As New ControlDB
                    DataSetX = con.SelectX(ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                Dim registro As DataRow
                If Session("NumReg") > 0 Then
                    registro = DataSetX.Tables(0).Rows(0)
                    txtRTNagen_aduanera.Text = registro("rtn_agencia")
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


End Class