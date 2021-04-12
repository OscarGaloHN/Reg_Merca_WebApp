Public Class items
    Inherits System.Web.UI.Page
    'OBJETO #29
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
            ''parametros de configuracion de sistema
            'Using Parametros_Sistema As New ControlDB
            '    Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
            'End Using

            ''PARAMETROS DE ADMINISTRADOR
            'Using Parametros_admin As New ControlDB
            '    Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
            'End Using

            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else


                Select Case Request.QueryString("action")
                    Case "new"

                    Case "update"
                        If Not IsPostBack Then

                            Dim Ssql As String = String.Empty
                            Ssql = "select * from DB_Nac_Merca.tbl_34_mercancias where ID_Merca =" & Request.QueryString("iditems") & ""

                            Using con As New ControlDB
                                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                Session("NumReg") = DataSetX.Tables(0).Rows.Count
                            End Using
                            Dim registro As DataRow
                            If Session("NumReg") > 0 Then
                                'cargar txt
                                registro = DataSetX.Tables(0).Rows(0)
                                ddltipoitem.SelectedValue = registro("Id_Tipo_items")
                                txtposarancel.Text = (registro("num_partida"))
                                txttitulocurri.Text = registro("titulo_currier")
                                txtmmatrizinsu.Text = registro("matriz_insumos")
                                txtnrroitemasoc.Text = registro("item_asociado")
                                txtdeclaracioancancel.Text = registro("declaracion_a_cancelar")
                                txtnmeroitemcancel.Text = registro("item_a_cancelar")
                                txtpesoneto.Text = registro("pesoneto")
                                txtpesobruto.Text = registro("pesobruto")
                                txtcantbltos.Text = registro("bultcant")
                                ddlestadomerca.SelectedValue = registro("Estado_Merc")
                                ddlpaisesdeorigeni.SelectedValue = registro("cod_pais_fab")
                                ddlpaisproce.SelectedValue = registro("cod_pais_pro")
                                ddlpaisadd.SelectedValue = registro("cod_pais_adq")
                                ddlunidacomer.SelectedValue = registro("Id_UnidadComercial")
                                txtcantidadcomer.Text = registro("Cantidad_Comercial")
                                ddlunidadestadis.SelectedValue = registro("Unidad_Estadistica")
                                txtcantidadestadis.Text = registro("cantidad_estadistica")
                                txtimportefact.Text = registro("importes_factura")
                                txtimporteotros.Text = registro("importes_otrosgastos")
                                txtseguro.Text = registro("importes_seguro")
                                txtflete.Text = registro("importes_flete")
                                txtajuste.Text = registro("ajuste_a_incluir")
                                txtnumerocerti.Text = registro("numero_certificado_imp")
                                txtconvenio.Text = registro("convenio_perfeccionamiento")
                                txtexoneracionaduanera.Text = registro("exoneracion_aduanera")
                                txtobservacion.Text = registro("observaciones")
                                txtcomentario.Text = registro("comentario")
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

                End Select

            End If

        Catch ex As Exception

        End Try




    End Sub

    Private Sub btt_guardar_Click(sender As Object, e As EventArgs) Handles btt_guardar.Click
        Try
            Dim Ssql As String = String.Empty

            Select Case Request.QueryString("action")
                Case "new"
                    Ssql = "Insert into DB_Nac_Merca.tbl_34_mercancias 
(Id_Tipo_items,num_partida,titulo_currier,matriz_insumos,item_asociado,declaracion_a_cancelar,item_a_cancelar,
pesoneto,pesobruto,bultcant,Estado_Merc,cod_pais_fab,cod_pais_pro,cod_pais_adq,cuota_arancelaria,Id_UnidadComercial,Cantidad_Comercial,
Unidad_Estadistica,cantidad_estadistica,importes_factura,importes_otrosgastos,importes_seguro,importes_flete,ajuste_a_incluir,
numero_certificado_imp,convenio_perfeccionamiento,exoneracion_aduanera,observaciones,comentario) 
values ('" & ddltipoitem.SelectedValue & "', '" & txtposarancel.Text & "',
'" & txttitulocurri.Text & "', '" & txtmmatrizinsu.Text & "', '" & txtnrroitemasoc.Text & "', 
'" & txtdeclaracioancancel.Text & "', '" & txtnmeroitemcancel.Text & "','" & txtpesoneto.Text & "','" & txtpesobruto.Text & "',
'" & txtcantbltos.Text & "','" & ddlestadomerca.SelectedValue & "','" & ddlpaisesdeorigeni.SelectedValue & "','" & ddlpaisproce.SelectedValue & "',
'" & ddlpaisadd.SelectedValue & "','" & ddlunidacomer.SelectedValue & "','" & txtcantidadcomer.Text & "',
'" & ddlunidadestadis.SelectedValue & "','" & txtcantidadestadis.Text & "','" & txtimportefact.Text & "',
'" & txtimporteotros.Text & "','" & txtseguro.Text & "','" & txtflete.Text & "',
'" & txtajuste.Text & "','" & txtnumerocerti.Text & "','" & txtconvenio.Text & "',
'" & txtexoneracionaduanera.Text & "','" & txtobservacion.Text & "','" & txtcomentario.Text & "',
'" & Session("iditems") & "'); SELECT LAST_INSERT_ID();"

                    Using con As New ControlDB
                        con.GME_Recuperar_item(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using

                    If Session("NumReg") > 0 Then
                        'Using log_bitacora As New ControlBitacora
                        '    log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 13, "El correo " & txtCorreoElectronico.Text & " ya esta registrado")
                        'End Using
                        Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=update&iditems=" & Session("GME_Recuperar_item"))
                    Else

                    End If

            End Select

        Catch ex As Exception

        End Try
    End Sub



    Private Sub bttDocumentos_Click(sender As Object, e As EventArgs) Handles bttDocumentos.Click
        Response.Redirect("~/modulos/declaracion_aduanera/items_documentos.aspx?iditems=" & Request.QueryString("iditems"))
    End Sub

    Private Sub bttComplementario_Click(sender As Object, e As EventArgs) Handles bttComplementario.Click
        Response.Redirect("~/modulos/declaracion_aduanera/items_dcomplementarios.aspx?iditems=" & Request.QueryString("iditems"))
    End Sub

    Private Sub bttventajas_Click(sender As Object, e As EventArgs) Handles bttventajas.Click
        Response.Redirect("~/modulos/declaracion_aduanera/items_ventajas.aspx?iditems=" & Request.QueryString("iditems"))
    End Sub
End Class