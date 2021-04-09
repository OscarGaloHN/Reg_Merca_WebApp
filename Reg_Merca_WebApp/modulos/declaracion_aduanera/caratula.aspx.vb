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
                pbotones.Enabled = False

                Select Case Request.QueryString("action")
                    Case "new"
                        Dim fechaactual As Date = (Date.Now)
                        txtFechaCreacion.Text = fechaactual
                        ddlestado.Enabled = False
                        'inhabilita Panel de botones
                        pbotones.Enabled = False
                    Case "update"
                        'habilita Panel de botones
                        pbotones.Enabled = True
                        If Not IsPostBack Then

                            Dim Ssql As String = String.Empty
                            Ssql = "select * from DB_Nac_Merca.tbl_01_polizas where id_poliza =" & Request.QueryString("idCaratula") & ""

                            Using con As New ControlDB
                                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                Session("NumReg") = DataSetX.Tables(0).Rows.Count
                            End Using
                            Dim registro As DataRow
                            If Session("NumReg") > 0 Then
                                'cargar txt
                                registro = DataSetX.Tables(0).Rows(0)
                                txtFechaCreacion.Text = CDate(registro("fecha_creacion")).ToLongDateString
                                ddlestado.SelectedValue = registro("estado_poliza")
                                Session("estado_temp") = registro("estado_poliza")
                                ddlestado.Attributes.Add("disabled", "disabled")
                                ddlCliente.SelectedValue = registro("id_cliente")
                                txtdeclarante.Text = registro("declarante")
                                ddladuanadespacho.SelectedValue = registro("cod_aduana_ent")
                                ddlregimenaduanero.SelectedValue = registro("id_regimen")
                                txtrtnimp_exp.Text = registro("rtn_importador")
                                txtimp_exp.Text = registro("nombre_importador")
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
                                txtcantbultos.Text = registro("cant_bultos")
                                txtpesobrutobul.Text = registro("pesobruto_bultos")
                                txttotalitems.Text = registro("canti_items")
                                txttotalfact.Text = registro("Total_Factura")
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

                        'Response.Redirect("~/modulos/declaracion_aduanera/creacion_proyectos.aspx", False)
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
(fecha_creacion,estado_poliza, Id_cliente, declarante, cod_aduana_ent, Id_regimen, rtn_importador, nombre_importador, 
rtn_agenciaadu, nombre_agenciaadu, manifiesto_entregarap, Id_proveedor, contrato_proveedor,
domicilio_proveed, Numero_Preimpreso, entidad_mediacion, Id_almacen, cod_aduana_sal, Cod_pais_org, 
Cod_pais_pro, id_pago, id_condicion, aduana_transdes, modalidad_especial, deposito_aduanas, plazo,
ruta_transito, motivo_operacion, Observaciones, Id_Clase_deBulto, cant_bultos, pesobruto_bultos, canti_items, 
Total_Factura, Total_Otros_gastos, Total_Seguro, Total_Flete, divisa_factura, tipo_de_cambio,
divisa_seguro, divisa_flete, usuario_creador, Id_poliza) 
values (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'),'" & ddlestado.SelectedValue & "', '" & ddlCliente.SelectedValue & "',
'" & txtdeclarante.Text & "', '" & ddladuanadespacho.SelectedValue & "', '" & ddlregimenaduanero.SelectedValue & "', 
'" & txtrtnimp_exp.Text & "', '" & txtimp_exp.Text & "','" & txtRTNagen_aduanera.Text & "','" & txtagen_aduanera.Text & "',
'" & txtmanifiestorap.Text & "','" & ddlproveedores.SelectedValue & "','" & txtContra_proveedor.Text & "','" & txtDomicioProve.Text & "',
'" & txtNumPreimp.Text & "','" & txtEntidadMed.Text & "','" & ddldepositoalmacen.SelectedValue & "','" & ddladuanaingsal.SelectedValue & "',
'" & ddlpaisesdeorigen.SelectedValue & "','" & ddlpaisprocedencia.SelectedValue & "','" & ddlformadepago.SelectedValue & "',
'" & ddlcondicionentrega.SelectedValue & "','" & ddladuanatransitodes.SelectedValue & "','" & ddlmodalidadesp.SelectedValue & "',
'" & ddldepositoaduana.SelectedValue & "','" & txtplazodiasmeses.Text & "','" & txtrutatransito.Text & "',
'" & txt_motivoperacion.Text & "','" & txtobservacion.Text & "','" & ddlclasebultos.SelectedValue & "',
'" & txtcantbultos.Text & "','" & txtpesobrutobul.Text & "','" & txttotalitems.Text & "','" & txttotalfact.Text & "',
'" & txttotalotrosgast.Text & "','" & txtttotalseg.Text & "','" & txttotalflet.Text & "','" & ddldivisafact.SelectedValue & "',
'" & txttipodecambio.Text & "','" & ddldivisaseg.SelectedValue & "','" & ddldivisafl.SelectedValue & "',
'" & Session("user_idUsuario") & "', '" & Session("idCaratula") & "')"



                    Using con As New ControlDB
                            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        End Using

                    ''recupera el id de la caratura
                    'Ssql = "Select *from( Select  LAST_INSERT_ID() as Ultio_ID) Tbl_ID"
                    'Using con As New ControlDB
                    '    con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    '    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    'End Using
                    'Dim registro As DataRow
                    'If Session("NumReg") > 0 Then
                    '    registro = DataSetX.Tables(0).Rows(0)

                    '    Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratura=" & CStr(registro(0)))

                    'End If



                    'Ssql = "Select  LAST_INSERT_ID()"

                    'inhabilita Panel de botones
                    'pbotones.Enabled = True

            End Select

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
            Response.Redirect("~/modulos/declaracion_aduanera/Creacion_items.aspx?idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCliente.SelectedIndexChanged
        'Select Case a la BD a la tabla para llenar el RTN
    End Sub
End Class