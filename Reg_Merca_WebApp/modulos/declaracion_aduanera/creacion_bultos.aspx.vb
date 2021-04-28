Public Class creacion_bultos
    Inherits System.Web.UI.Page
    '#OBJETO 28
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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 28 and permiso_consulta = 1"

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    'si tiene los permisos



                    lblCatatura.Text = Request.QueryString("idCaratula")

                    'cargar logo para imprimir
                    HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
                    HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

                    If Session("user_idUsuario") = Nothing Then
                        Session.Abandon()
                        Response.Redirect("~/Inicio/login.aspx")
                    Else

                        'llenar grid
                        Ssql = "SELECT id_bulto, manifiesto, titulo_transporte, 
case when indicador =1 then 'SI' else 'NO' end indicador, id_poliza_bul FROM DB_Nac_Merca.tbl_40_Bulto
                    where id_poliza_bul = " & Request.QueryString("idCaratula") & ""
                        Using con As New ControlDB
                            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                            Session("NumReg") = DataSetX.Tables(0).Rows.Count
                        End Using

                        If Session("NumReg") > 0 Then
                            gvCustomers.DataSource = DataSetX
                            gvCustomers.DataBind()
                        Else
                            bttfin.Visible = False
                        End If

                        If Not IsPostBack Then
                            Select Case Request.QueryString("acction")
                                Case "newbulto"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto se almaceno con éxito.', 'success');</script>")
                                Case "deltebulto"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto se elimino con éxito.', 'success');</script>")
                                Case "editbulto"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto se modifico con éxito.', 'success');</script>")
                                Case Else
                                    'bitacora de que salio de un form
                                    If Not IsPostBack Then
                                        Using log_bitacora As New ControlBitacora
                                            log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                        End Using
                                    End If

                                    'bitacora de que ingreso al form
                                    Session("IDfrmQueIngresa") = 28
                                    Session("NombrefrmQueIngresa") = "Creación de Bultos"
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
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try

    End Sub

    Private Sub bttGuardarbulto_Click(sender As Object, e As EventArgs) Handles bttGuardarbulto.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_40_Bulto where id_poliza_bul = '" & Request.QueryString("idCaratula") & "' "
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bulto','El bulto de su carátula ya esta registrado.', 'error');</script>")
            Else
                If chkindicador.Checked = True Then
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_40_Bulto (manifiesto, id_poliza_bul, titulo_transporte, indicador) VALUES ('" & txtmanifiesto.Text & "'," & Request.QueryString("idCaratula") & ", '" & txt_trans.Text & "', '1');"
                Else
                    Ssql = "INSERT INTO DB_Nac_Merca.tbl_40_Bulto (manifiesto, id_poliza_bul, titulo_transporte, indicador) VALUES ('" & txtmanifiesto.Text & "'," & Request.QueryString("idCaratula") & ", '" & txt_trans.Text & "', '0');"

                End If
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se editaron los datos para la aduana con id: " & lblHiddenIDAduna.Value)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_bultos.aspx?acction=newbulto&idCaratula=" & Request.QueryString("idCaratula"))
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub

    Private Sub bttEliminarbulto_Click(sender As Object, e As EventArgs) Handles bttEliminarbulto.Click
        Try
            Dim Ssql As String = "DELETE FROM DB_Nac_Merca.tbl_40_Bulto WHERE id_bulto= " & lblHiddenIDbulto.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se elimino la aduna con nombre: " & lblHiddenNombreAduna.Value & " con exito")
            'End Using

            Response.Redirect("~/modulos/declaracion_aduanera/creacion_bultos.aspx?acction=deltebulto&idCaratula=" & Request.QueryString("idCaratula"))

        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub
    Private Sub bttModificarbulto_Click(sender As Object, e As EventArgs) Handles bttModificarbulto.Click
        Try
            Dim Ssql As String = String.Empty
            If txtmanifiestoEditar.Text <> lblHiddenmanifiesto.Value Then
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_40_Bulto where manifiesto = BINARY  '" & txtmanifiestoEditar.Text & "' "
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
            Else
                Session("NumReg") = 0
            End If

            If Session("NumReg") > 0 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bultos','El manifiesto de bultos ya esta registrado.', 'error');</script>")
            Else
                Ssql = "UPDATE DB_Nac_Merca.tbl_40_Bulto SET manifiesto = '" & txtmanifiestoEditar.Text & "', titulo_transporte = '" & txt_transEditar.Text & "' WHERE id_bulto = " & lblHiddenIDbulto.Value & ";"
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                'Using log_bitacora As New ControlBitacora
                '    log_bitacora.acciones_Comunes(4, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se guardo una nueva aduna con nombre: " & txtAduana.Text)
                'End Using
                Response.Redirect("~/modulos/declaracion_aduanera/creacion_bultos.aspx?acction=editbulto&idCaratula=" & Request.QueryString("idCaratula"))
            End If
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub

    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Try
            Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttfin_Click(sender As Object, e As EventArgs) Handles bttfin.Click
        Try
            Dim Ssql As String = String.Empty
            Ssql = "UPDATE DB_Nac_Merca.tbl_01_polizas set estado_poliza = 8 where id_poliza = " & Request.QueryString("idCaratula")
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using


            Session("nombreRPT") = "~/modulos/reportes/rptCaratula.rdlc"
            Session("nombreDS") = "DSCaratula"
            Session("nombreDT") = "DtCaratula"
            Session("xSsql") = "SELECT T001.Id_poliza, CONVERT(T001.fecha_creacion,DATE) fecha_creacion, T002.descripcion, T003.nombrec, T003.rtn_cli,
T004.nombredecla, T004.rtn_agencia, T004.nombre_agencia, T005.Nombre_aduana AS aduana_despacho, T006.descripcion AS regimen,
T007.nombre AS proveedor, T001.domicilio_proveed, T001.Numero_Preimpreso, T008.Nombre AS almacen, T009.Nombre_aduana AS aduna_ingreso_salida,
T010.Nombre_Pais AS pais_origen, T011.Nombre_Pais AS pais_procedencia, T012.nombre_pago as forma_pago, T013.nombre_condicion,
T014.Nombre_aduana AS aduana_tansito , T015.Nombre_modalidad AS modalidad_especial , T016.Nombre AS deposito_aduanas,
T017.Descripción AS Clase_de_bulto , T001.divisa_factura,T001.divisa_flete,T001.divisa_seguro, T001.plazo, T001.manifiesto_entregarap,
T001.contrato_proveedor, T001.entidad_mediacion, T001.ruta_transito, T001.motivo_operacion, T001.Observaciones
FROM DB_Nac_Merca.tbl_01_polizas T001
LEFT JOIN DB_Nac_Merca.tbl_19_estado T002 ON T001.estado_poliza = T002.id_estado
LEFT JOIN DB_Nac_Merca.tbl_04_cliente T003 ON T001.Id_cliente = T003.Id_cliente
LEFT JOIN DB_Nac_Merca.tbl_43_declarante T004 ON T001.declarante = T004.id_declarante
LEFT JOIN DB_Nac_Merca.tbl_06_aduanas T005 ON T001.cod_aduana_sal = T005.Id_Aduana #aduana despacho
LEFT JOIN DB_Nac_Merca.tbl_27_Regimenes T006 ON T001.Id_regimen = T006.Id_Regimen
LEFT JOIN DB_Nac_Merca.tbl_05_proveedores T007 ON T001.Id_proveedor = T007.Id_proveedor
LEFT JOIN DB_Nac_Merca.tbl_9_almacenes T008 ON T001.Id_almacen = T008.Id_almacen
LEFT JOIN DB_Nac_Merca.tbl_06_aduanas T009 ON T001.cod_aduana_ent = T009.Id_Aduana #aduana ingreso salida
LEFT JOIN DB_Nac_Merca.tbl_8_paises T010 ON T001.Cod_pais_org = T010.Id_Pais #pais origen
LEFT JOIN DB_Nac_Merca.tbl_8_paises T011 ON T001.Cod_pais_pro = T011.Id_Pais #pais procede
LEFT JOIN DB_Nac_Merca.tbl_13_forma_pago T012 ON T001.id_pago = T012.id_pago
LEFT JOIN DB_Nac_Merca.tbl_14_condicion_entrega T013 ON T001.id_condicion = T013.id_condicion
LEFT JOIN DB_Nac_Merca.tbl_06_aduanas T014 ON T001.aduana_transdes = T014.Id_Aduana #aduana transi/destino
LEFT JOIN DB_Nac_Merca.tbl_39_modalidad_especial T015 ON T001.modalidad_especial = T015.Id_Modalidad
LEFT JOIN DB_Nac_Merca.tbl_9_almacenes T016 ON T001.deposito_aduanas = T016.Id_almacen
LEFT JOIN DB_Nac_Merca.tbl_18_Clase_deBulto T017 ON T001.Id_Clase_deBulto = T017.Id_Clase_deBulto
WHERE T001.Id_poliza =" & Request.QueryString("idCaratula")












            Session("nombreDS2") = "DSItems"
            Session("nombreDT2") = "DtItems"
            Session("xSsql2") = "SELECT T001.ID_Merca, T001.Id_poliza, T002.Descripcion AS Id_TipoItems ,T001.num_partida,
T001.titulo_currier, T001.matriz_insumos,T001.item_asociado, T001.declaracion_a_cancelar, T001.item_a_cancelar,
T001.pesoneto,T001.pesobruto,T001.bultcant , T003.Descripcion AS Estado_Merc,
T004.Nombre_Pais AS pais_fab,T005.Nombre_Pais AS pais_pro, T006.Nombre_Pais AS pais_adq,
T007.Descripcion as UnidadComercial , T001.Cantidad_Comercial , T008.Descripcion as Unidad_Estadistica,
T001.cantidad_estadistica,T001.importes_factura, T001.importes_otrosgastos ,T001.importes_seguro, T001.importes_flete ,
T001.ajuste_a_incluir, T001.numero_certificado_imp, T001.convenio_perfeccionamiento, T001.exoneracion_aduanera ,
T001.observaciones, T001.comentario
FROM DB_Nac_Merca.tbl_34_mercancias T001
LEFT JOIN DB_Nac_Merca.tbl_26_Tipo_Items T002 ON T001.Id_Tipo_items = T002.Id_TipoItems
LEFT JOIN DB_Nac_Merca.tbl_25_Estado_Mercancias T003 ON T001.Estado_Merc = T003.Id_Estado
LEFT JOIN DB_Nac_Merca.tbl_8_paises T004 ON T001.cod_pais_fab = T004.Id_Pais #pais FAB
LEFT JOIN DB_Nac_Merca.tbl_8_paises T005 ON T001.cod_pais_pro = T005.Id_Pais #pais PRO
LEFT JOIN DB_Nac_Merca.tbl_8_paises T006 ON T001.cod_pais_adq = T006.Id_Pais #pais ADQ
LEFT JOIN DB_Nac_Merca.tbl_24_Unidad_Medida T007 ON T001.Id_UnidadComercial = T007.Id_UnidadMed
LEFT JOIN DB_Nac_Merca.tbl_24_Unidad_Medida T008 ON T001.Unidad_Estadistica = T008.Id_UnidadMed

WHERE T001.Id_poliza = " & Request.QueryString("idCaratula")


            Session("nombreDS3") = "DSResumenItems"
            Session("nombreDT3") = "DtResumenItems"
            Session("xSsql3") = "SELECT T001.Id_poliza,count(*) canti_items, T002.observaciones,
sum(pesoneto) pesoneto,sum(pesobruto) pesobruto,sum(bultcant) bultcant,
sum(importes_factura) importes_factura,sum(importes_otrosgastos)importes_otrosgastos,
sum(importes_seguro) importes_seguro,sum(importes_flete) importes_flete,
(sum(importes_factura) +sum(importes_otrosgastos)+sum(importes_seguro)+ sum(importes_flete) ) Total
FROM DB_Nac_Merca.tbl_34_mercancias T001
LEFT JOIN ( SELECT *FROM (SELECT Id_poliza, observaciones FROM DB_Nac_Merca.tbl_34_mercancias
where length(observaciones) > 1 and Id_poliza = " & Request.QueryString("idCaratula") & " limit 1) TBL_TEMP
) T002 ON T001.Id_poliza = T002.Id_poliza
where T001.Id_poliza = " & Request.QueryString("idCaratula")

            Response.Redirect("/modulos/declaracion_aduanera/Reporte_caratula.aspx?idcaratula=" & Request.QueryString("idCaratula"))
        Catch ex As MySql.Data.MySqlClient.MySqlException
            Select Case ex.Number
                Case 0
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','La autenticación de usuario para el host falló.', 'error');</script>")
                Case 1042
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error de conexión','No fue posible conectarse al el servidor.', 'error');</script>")
                Case Else
                    Dim Ssql As String = String.Empty
                    Ssql = "SELECT * FROM DB_Nac_Merca.tbl_44_errores  where codigo = " & ex.Number & ""
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    If Session("NumReg") > 0 Then
                        registro = DataSetX.Tables(0).Rows(0)
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','" & registro("mensaje_usuario") & "', 'error');</script>")
                    Else
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
                    End If
            End Select
        Catch ex As NullReferenceException
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Error','Error inesperado contacte al administrador.', 'error');</script>")
        End Try
    End Sub
End Class