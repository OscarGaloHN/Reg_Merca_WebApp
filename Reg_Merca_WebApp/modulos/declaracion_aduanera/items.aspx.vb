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
            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                'REDIRECCIONAR A MENU PRINCIPAL
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'si hay una sesion activa
                'comprobar que el rol del usuario tenga permisos para editar
                Dim Ssql As String = String.Empty
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_03_permisos
                    where id_rol = " & Session("user_rol") & " and id_objeto = 29 and permiso_consulta = 1"

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    'si tiene los permisos




                    lblCatatura.Text = Request.QueryString("iditems")

                    If Session("user_idUsuario") = Nothing Then
                        Session.Abandon()
                        Response.Redirect("~/Inicio/login.aspx")
                    Else

                        Select Case Request.QueryString("action")
                            Case "new"
                                pbotones.Visible = False
                            Case "update"
                                btt_guardar.Visible = False
                                pbotones.Visible = True
                                pactual.Visible = True
                                If Not IsPostBack Then

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
                                'bitacora de que salio de un form
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                                'bitacora de que ingreso al form
                                Session("IDfrmQueIngresa") = 29
                                Session("NombrefrmQueIngresa") = "Creación de Items"
                                If Not IsPostBack Then
                                    Using log_bitacora As New ControlBitacora
                                        log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                                    End Using
                                End If

                        End Select

                        If Not IsPostBack Then
                            Select Case Request.QueryString("alerta")
                                Case "update"
                                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Items','El Item se actualizo con éxito.', 'success');</script>")
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

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btt_guardar_Click(sender As Object, e As EventArgs) Handles btt_guardar.Click
        Try
            Dim Ssql As String = String.Empty

            Select Case Request.QueryString("action")
                Case "new"
                    Ssql = "Insert into DB_Nac_Merca.tbl_34_mercancias 
(Id_poliza,Id_Tipo_items,num_partida,titulo_currier,matriz_insumos,
item_asociado,declaracion_a_cancelar,item_a_cancelar,pesoneto,pesobruto,
bultcant,Estado_Merc,cod_pais_fab,cod_pais_pro,
cod_pais_adq,Id_UnidadComercial,Cantidad_Comercial,Unidad_Estadistica,
cantidad_estadistica,importes_factura,importes_otrosgastos,importes_seguro,importes_flete,
ajuste_a_incluir,numero_certificado_imp,convenio_perfeccionamiento,exoneracion_aduanera,observaciones,comentario) 
values 
(" & Request.QueryString("idCaratula") & ",'" & ddltipoitem.SelectedValue & "', '" & txtposarancel.Text & "','" & txttitulocurri.Text & "', '" & txtmmatrizinsu.Text & "', 
'" & txtnrroitemasoc.Text & "', '" & txtdeclaracioancancel.Text & "', '" & txtnmeroitemcancel.Text & "','" & txtpesoneto.Text & "','" & txtpesobruto.Text & "',
'" & txtcantbltos.Text & "','" & ddlestadomerca.SelectedValue & "','" & ddlpaisesdeorigeni.SelectedValue & "','" & ddlpaisproce.SelectedValue & "',
'" & ddlpaisadd.SelectedValue & "','" & ddlunidacomer.SelectedValue & "','" & txtcantidadcomer.Text & "','" & ddlunidadestadis.SelectedValue & "',
'" & txtcantidadestadis.Text & "','" & txtimportefact.Text & "','" & txtimporteotros.Text & "','" & txtseguro.Text & "','" & txtflete.Text & "',
'" & txtajuste.Text & "','" & txtnumerocerti.Text & "','" & txtconvenio.Text & "','" & txtexoneracionaduanera.Text & "',
'" & txtobservacion.Text & "','" & txtcomentario.Text & "'); SELECT LAST_INSERT_ID();"

                    Using con As New ControlDB
                        con.GME_Recuperar_ID(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Response.Redirect("~/modulos/declaracion_aduanera/items_documentos.aspx?action=new&iditems=" & Session("GME_Recuperar_ID") & "&idCaratula=" & Request.QueryString("idCaratula"))

                    'If Session("NumReg") > 0 Then
                    '    'Using log_bitacora As New ControlBitacora
                    '    '    log_bitacora.acciones_Comunes(5, Session("user_idUsuario"), 13, "El correo " & txtCorreoElectronico.Text & " ya esta registrado")
                    '    'End Using
                    'Else

                    'End If



            End Select

        Catch ex As Exception

        End Try
    End Sub



    Private Sub bttDocumentos_Click(sender As Object, e As EventArgs) Handles bttDocumentos.Click
        Response.Redirect("~/modulos/declaracion_aduanera/items_documentos.aspx?iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
    End Sub

    Private Sub bttComplementario_Click(sender As Object, e As EventArgs) Handles bttComplementario.Click
        Response.Redirect("~/modulos/declaracion_aduanera/items_dcomplementarios.aspx?iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
    End Sub

    Private Sub bttventajas_Click(sender As Object, e As EventArgs) Handles bttventajas.Click
        Response.Redirect("~/modulos/declaracion_aduanera/items_ventajas.aspx?iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula"))
    End Sub

    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Try
            Response.Redirect("~/modulos/declaracion_aduanera/Creacion_items.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub bttActualizar_Click(sender As Object, e As EventArgs) Handles bttActualizar.Click
        Dim Ssql As String
        Try


            Select Case Request.QueryString("action")
                Case "update"
                    Ssql = "update DB_Nac_Merca.tbl_34_mercancias set Id_Tipo_items= '" & ddltipoitem.SelectedValue & "', 
num_partida= '" & txtposarancel.Text & "' ,titulo_currier= '" & txttitulocurri.Text & "',
matriz_insumos='" & txtmmatrizinsu.Text & "', item_asociado= '" & txtnrroitemasoc.Text & "' , declaracion_a_cancelar='" & txtdeclaracioancancel.Text & "',
item_a_cancelar='" & txtnmeroitemcancel.Text & "', pesoneto='" & txtpesoneto.Text & "', pesobruto='" & txtpesobruto.Text & "',
bultcant='" & txtcantbltos.Text & "', Estado_Merc= '" & ddlestadomerca.SelectedValue & "',
cod_pais_fab='" & ddlpaisesdeorigeni.SelectedValue & "', cod_pais_pro='" & ddlpaisproce.SelectedValue & "',
cod_pais_adq='" & ddlpaisadd.SelectedValue & "', Id_UnidadComercial= '" & ddlunidacomer.SelectedValue & "',
Cantidad_Comercial= '" & txtcantidadcomer.Text & "', Unidad_Estadistica= '" & ddlunidadestadis.SelectedValue & "',
cantidad_estadistica= '" & txtcantidadestadis.Text & "', importes_factura= '" & txtimportefact.Text & "',
importes_otrosgastos= '" & txtimporteotros.Text & "', importes_seguro='" & txtseguro.Text & "',
importes_flete='" & txtflete.Text & "',
ajuste_a_incluir= '" & txtajuste.Text & "',numero_certificado_imp= '" & txtnumerocerti.Text & "',
convenio_perfeccionamiento= '" & txtconvenio.Text & "', exoneracion_aduanera= '" & txtexoneracionaduanera.Text & "',
observaciones= '" & txtobservacion.Text & "',comentario= '" & txtcomentario.Text & "' where ID_Merca =" & Request.QueryString("iditems") & " "
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=update&iditems=" & Request.QueryString("iditems") & "&idCaratula=" & Request.QueryString("idCaratula") & "&alerta=update")

            End Select
        Catch ex As Exception

        End Try
    End Sub
End Class