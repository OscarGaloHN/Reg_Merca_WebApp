Imports System.IO
'OBJETO #47
Public Class exportar
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
                    where id_rol = " & Session("user_rol") & " and id_objeto = 47 and permiso_consulta = 1"

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    'si tiene los permisos
                    If Request.QueryString("action") = "exportar" Then
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 47
                        Session("NombrefrmQueIngresa") = "Exportar Archivo"
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                        exportarArchivo(Request.QueryString("xIdCaratual"))
                    Else
                        'REDIRECCIONAR A MENU PRINCIPAL
                        Response.Redirect("~/modulos/menu_principal.aspx")
                    End If
                Else
                    'si no tiene permisos 
                    Using log_bitacora As New ControlBitacora
                        log_bitacora.acciones_Comunes(14, Session("user_idUsuario"), 47, "El usuario intenta ingresa a una pantalla sin permisos")
                    End Using
                    Response.Redirect("~/modulos/acceso_denegado.aspx")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub exportarArchivo(ByVal xIdCaratual As String)
        Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_01_polizas where Id_poliza =" & xIdCaratual & ";"

        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            'crear archivo
            Dim registro As DataRow

            Dim xArchivo As String = "{" & Chr(34) & "declaracion" & Chr(34) & ":{" & Chr(34) & "caratula" & Chr(34) & ":{"

            'For i = 0 To Session("NumReg") - 1
            registro = DataSetX.Tables(0).Rows(0)
            'lleanr caratula
            'regimen
            xArchivo = xArchivo & Chr(34) & "regimen" & Chr(34) & ":" & Chr(34) & registro("Id_regimen") & Chr(34) & ","
            'aduana
            xArchivo = xArchivo & Chr(34) & "aduana" & Chr(34) & ":" & Chr(34) & registro("cod_aduana_ent") & Chr(34) & ","
            'modalidad_especial
            xArchivo = xArchivo & Chr(34) & "indModalidadEspecial" & Chr(34) & ":" & Chr(34) & registro("modalidad_especial") & Chr(34) & ","
            'rtn importador exportador
            xArchivo = xArchivo & Chr(34) & "rucImpoExpo" & Chr(34) & ":" & Chr(34) & registro("rtn_importador") & Chr(34) & ","
            'nombre_importador
            xArchivo = xArchivo & Chr(34) & "descripcionImpoExpo" & Chr(34) & ":" & Chr(34) & registro("nombre_importador") & Chr(34) & ","
            'manifiestoCourrier
            xArchivo = xArchivo & Chr(34) & "manifiestoCourrier" & Chr(34) & ":" & Chr(34) & registro("manifiesto_entregarap") & Chr(34) & ","
            'id proveedor
            xArchivo = xArchivo & Chr(34) & "docDestProv" & Chr(34) & ":" & Chr(34) & registro("Id_proveedor") & Chr(34) & ","
            'nombre proveedor               *************************
            xArchivo = xArchivo & Chr(34) & "nomDestProv" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
            'nombre proveedor
            xArchivo = xArchivo & Chr(34) & "domDestProv" & Chr(34) & ":" & Chr(34) & registro("domicilio_proveed") & Chr(34) & ","
            'nroPreimpreso
            xArchivo = xArchivo & Chr(34) & "nroPreimpreso" & Chr(34) & ":" & Chr(34) & registro("Numero_Preimpreso") & Chr(34) & ","
            'fauca
            xArchivo = xArchivo & Chr(34) & "isFauca" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
            'idFauca                *************************
            xArchivo = xArchivo & Chr(34) & "isFauca" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
            'aduanaIngSal
            xArchivo = xArchivo & Chr(34) & "aduanaIngSal" & Chr(34) & ":" & Chr(34) & registro("cod_aduana_sal") & Chr(34) & ","
            ' paisOrigen
            xArchivo = xArchivo & Chr(34) & "paisOrigen" & Chr(34) & ":" & Chr(34) & registro("Cod_pais_org") & Chr(34) & ","
            ' paisProcDestino
            xArchivo = xArchivo & Chr(34) & "paisProcDestino" & Chr(34) & ":" & Chr(34) & registro("Cod_pais_pro") & Chr(34) & ","
            ' pesoBruto
            xArchivo = xArchivo & Chr(34) & "pesoBruto" & Chr(34) & ":" & Chr(34) & registro("pesobruto_bultos") & Chr(34) & ","
            ' cantidadBultos
            xArchivo = xArchivo & Chr(34) & "cantidadBultos" & Chr(34) & ":" & Chr(34) & registro("cant_bultos") & Chr(34) & ","
            ' claseBultos
            xArchivo = xArchivo & Chr(34) & "claseBultos" & Chr(34) & ":" & Chr(34) & registro("Id_Clase_deBulto") & Chr(34) & ","
            ' canItems
            xArchivo = xArchivo & Chr(34) & "canItems" & Chr(34) & ":" & Chr(34) & registro("canti_items") & Chr(34) & ","
            ' comentario
            xArchivo = xArchivo & Chr(34) & "comentario" & Chr(34) & ":" & Chr(34) & registro("Observaciones") & Chr(34) & ","
            ' entFinancieraMed
            xArchivo = xArchivo & Chr(34) & "entFinancieraMed" & Chr(34) & ":" & Chr(34) & registro("entidad_mediacion") & Chr(34) & ","
            ' condicionEntrega
            xArchivo = xArchivo & Chr(34) & "condicionEntrega" & Chr(34) & ":" & Chr(34) & registro("id_condicion") & Chr(34) & ","
            ' divisaFactura
            xArchivo = xArchivo & Chr(34) & "divisaFactura" & Chr(34) & ":" & Chr(34) & registro("divisa_factura") & Chr(34) & ","
            ' totalFactura
            xArchivo = xArchivo & Chr(34) & "totalFactura" & Chr(34) & ":" & Chr(34) & registro("Total_Factura") & Chr(34) & ","
            ' cotDivFactura
            xArchivo = xArchivo & Chr(34) & "cotDivFactura" & Chr(34) & ":" & Chr(34) & registro("divisa_factura") & Chr(34) & ","
            'divisaFlete
            xArchivo = xArchivo & Chr(34) & "divisaFlete" & Chr(34) & ":" & Chr(34) & registro("divisa_flete") & Chr(34) & ","
            'totalFlete
            xArchivo = xArchivo & Chr(34) & "totalFlete" & Chr(34) & ":" & Chr(34) & registro("Total_Flete") & Chr(34) & ","
            'divisaSeguro
            xArchivo = xArchivo & Chr(34) & "divisaSeguro" & Chr(34) & ":" & Chr(34) & registro("divisa_seguro") & Chr(34) & ","
            'totalSeguro
            xArchivo = xArchivo & Chr(34) & "totalSeguro" & Chr(34) & ":" & Chr(34) & registro("Total_Seguro") & Chr(34) & ","
            'totalOGastos
            xArchivo = xArchivo & Chr(34) & "totalOGastos" & Chr(34) & ":" & Chr(34) & registro("Total_Otros_gastos") & Chr(34) & ","
            'totalAlmacenaje
            xArchivo = xArchivo & Chr(34) & "totalAlmacenaje" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
            'formaPagoFac
            xArchivo = xArchivo & Chr(34) & "formaPagoFac" & Chr(34) & ":" & Chr(34) & registro("id_pago") & Chr(34) & ","
            'porcInteresFac
            xArchivo = xArchivo & Chr(34) & "porcInteresFac" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
            'motivoSusp
            xArchivo = xArchivo & Chr(34) & "motivoSusp" & Chr(34) & ":" & Chr(34) & registro("motivo_operacion") & Chr(34) & ","
            'plazo
            xArchivo = xArchivo & Chr(34) & "plazo" & Chr(34) & ":" & Chr(34) & registro("plazo") & Chr(34) & ","
            'depAlmacenamiento
            xArchivo = xArchivo & Chr(34) & "depAlmacenamiento" & Chr(34) & ":" & Chr(34) & registro("deposito_aduanas") & Chr(34) & ","
            'aduanaTransitoDest
            xArchivo = xArchivo & Chr(34) & "aduanaTransitoDest" & Chr(34) & ":" & Chr(34) & registro("aduana_transdes") & Chr(34) & ","
            'depositoTransitoDest
            xArchivo = xArchivo & Chr(34) & "depositoTransitoDest" & Chr(34) & ":" & Chr(34) & registro("deposito_aduanas") & Chr(34) & ","
            'rutaTransito
            xArchivo = xArchivo & Chr(34) & "rutaTransito" & Chr(34) & ":" & Chr(34) & registro("ruta_transito") & Chr(34)
            'rutaTransito
            'FIN  lleanr caratula
            xArchivo = xArchivo & "}," & Chr(34) & "documentos" & Chr(34) & ":["

            ''''''## inicio documentos caratula
            Ssql = "SELECT T001.*,  T002.Descripcion ,CASE WHEN T001.presencia = 1 THEN 'S' ELSE 'N' END AS presencia_SARAWEB FROM DB_Nac_Merca.tbl_28_Documentos T001
                    LEFT JOIN  DB_Nac_Merca.tbl_32_Cod_Documentos T002 ON T001.Id_Documento = T002.Id_Documento 
                    Where id_poliza_doc =" & xIdCaratual & ";"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using

            If Session("NumReg") > 0 Then
                For i = 0 To Session("NumReg") - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    xArchivo = xArchivo & "{" & Chr(34) & "codDocumento" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & " - " & registro("Descripcion") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "referencia" & Chr(34) & ":" & Chr(34) & registro("Referencia") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "presencia" & Chr(34) & ":" & Chr(34) & registro("presencia_SARAWEB") & Chr(34) & "}"
                    If Session("NumReg") - 1 = i Then
                    Else
                        xArchivo = xArchivo & ","
                    End If
                Next
            Else


            End If
            xArchivo = xArchivo & "],"
            ''''''## fin documentos caratula
            ''''''## datos complementarios
            xArchivo = xArchivo & Chr(34) & "datos_complementarios" & Chr(34) & ":[],"
            ''''''## fin datos complementarios


            ''''''## ITEMS
            xArchivo = xArchivo & Chr(34) & "items" & Chr(34) & ":["


            Ssql = "SELECT T001.*,  T002.Descripcion ,CASE WHEN T001.presencia = 1 THEN 'S' ELSE 'N' END AS presencia_SARAWEB FROM DB_Nac_Merca.tbl_28_Documentos T001
                    LEFT JOIN  DB_Nac_Merca.tbl_32_Cod_Documentos T002 ON T001.Id_Documento = T002.Id_Documento 
                    Where id_poliza_doc =" & xIdCaratual & ";"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using

            If Session("NumReg") > 0 Then
                For i = 0 To Session("NumReg") - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    xArchivo = xArchivo & "{" & Chr(34) & "nroItem" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "tipoItem" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "posArancelaria" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "tituloCourrier" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "idMatrizInsumos" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "nroItemAsoc" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "nroItemACancelar" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "pesoNeto" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "pesoBruto" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "cantBultos" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "paisOrigen" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "paisProcDestino" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "paisAdquisicion" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "estadoMercancia" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "uniComercial" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "cantComercial" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "uniEstadistica" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "uniEstadisticaCod" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "cantEstadistica" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "importeFOB" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "importeFlete" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "importeSeguro" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "importeOGastos" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "importeAlmacenaje" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "ajusteAIncluir" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "ajusteADeducir" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "comentario" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "cuotaArancelaria" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "nroResolucion" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "convenioPerfActivo" & Chr(34) & ":" & Chr(34) & Chr(34) & ","
                    xArchivo = xArchivo & Chr(34) & "nroExoneracionAdu" & Chr(34) & ":" & Chr(34) & Chr(34) & ","


                    ''''''## DOC ITEMS
                    Ssql = "SELECT T001.*,  T002.Descripcion ,CASE WHEN T001.presencia = 1 THEN 'S' ELSE 'N' END AS presencia_SARAWEB FROM DB_Nac_Merca.tbl_28_Documentos T001
                    LEFT JOIN  DB_Nac_Merca.tbl_32_Cod_Documentos T002 ON T001.Id_Documento = T002.Id_Documento 
                    Where id_poliza_doc =" & xIdCaratual & ";"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using

                    xArchivo = xArchivo & Chr(34) & "documentos" & Chr(34) & ":["

                    If Session("NumReg") > 0 Then
                        For i2 = 0 To Session("NumReg") - 1
                            registro = DataSetX.Tables(0).Rows(i2)
                            xArchivo = xArchivo & "{" & Chr(34) & "codDocumento" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                            xArchivo = xArchivo & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & " - " & registro("Descripcion") & Chr(34) & ","
                            xArchivo = xArchivo & Chr(34) & "referencia" & Chr(34) & ":" & Chr(34) & registro("Referencia") & Chr(34) & ","
                            xArchivo = xArchivo & Chr(34) & "presencia" & Chr(34) & ":" & Chr(34) & registro("presencia_SARAWEB") & Chr(34) & "}"
                            If Session("NumReg") - 1 = i2 Then
                            Else
                                xArchivo = xArchivo & ","
                            End If
                        Next
                    Else


                    End If
                    xArchivo = xArchivo & "],"
                    ''''''## FIN DOC ITEMS

                    ''''''##  DATOS COMPLEMENTARIOS ITEMS
                    Ssql = "SELECT T001.*,  T002.Descripcion ,CASE WHEN T001.presencia = 1 THEN 'S' ELSE 'N' END AS presencia_SARAWEB FROM DB_Nac_Merca.tbl_28_Documentos T001
                    LEFT JOIN  DB_Nac_Merca.tbl_32_Cod_Documentos T002 ON T001.Id_Documento = T002.Id_Documento 
                    Where id_poliza_doc =" & xIdCaratual & ";"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using

                    xArchivo = xArchivo & Chr(34) & "datos_complementarios" & Chr(34) & ":["

                    If Session("NumReg") > 0 Then
                        For i2 = 0 To Session("NumReg") - 1
                            registro = DataSetX.Tables(0).Rows(i2)
                            xArchivo = xArchivo & "{" & Chr(34) & "codDatoComp" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                            xArchivo = xArchivo & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & " - " & registro("Descripcion") & Chr(34) & ","
                            xArchivo = xArchivo & Chr(34) & "valor" & Chr(34) & ":" & Chr(34) & registro("presencia_SARAWEB") & Chr(34) & "}"
                            If Session("NumReg") - 1 = i2 Then
                            Else
                                xArchivo = xArchivo & ","
                            End If
                        Next
                    Else


                    End If
                    xArchivo = xArchivo & "],"
                    ''''''##  FIN DATOS COMPLEMENTARIOS ITEMS
                    ''''''##   VENTAJAS ITEMS
                    Ssql = "SELECT T001.*,  T002.Descripcion ,CASE WHEN T001.presencia = 1 THEN 'S' ELSE 'N' END AS presencia_SARAWEB FROM DB_Nac_Merca.tbl_28_Documentos T001
                    LEFT JOIN  DB_Nac_Merca.tbl_32_Cod_Documentos T002 ON T001.Id_Documento = T002.Id_Documento 
                    Where id_poliza_doc =" & xIdCaratual & ";"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using

                    xArchivo = xArchivo & Chr(34) & "ventajas" & Chr(34) & ":["

                    If Session("NumReg") > 0 Then
                        For i3 = 0 To Session("NumReg") - 1
                            registro = DataSetX.Tables(0).Rows(i3)
                            xArchivo = xArchivo & "{" & Chr(34) & "codVentaja" & Chr(34) & ":" & Chr(34) & registro("Id_Documento") & Chr(34) & ","
                            xArchivo = xArchivo & Chr(34) & "descripcion" & Chr(34) & ":" & Chr(34) & registro("presencia_SARAWEB") & Chr(34) & "}"
                            If Session("NumReg") - 1 = i3 Then
                            Else
                                xArchivo = xArchivo & ","
                            End If
                        Next
                    Else


                    End If
                    xArchivo = xArchivo & "]"
                    ''''''##  FIN VENTAJAS ITEMS




                    If Session("NumReg") - 1 = i Then
                    Else
                        xArchivo = xArchivo & "},"
                    End If
                Next
            Else


            End If






            xArchivo = xArchivo & "}],"
            ''''''## fin ITEMS

            ''''''## transportes
            xArchivo = xArchivo & Chr(34) & "transportes" & Chr(34) & ":[]," & Chr(34) & "transportesGraneles" & Chr(34) & ":[],"
            ''''''## fin transportes


            'fin de archivo SOLO DE CARATULA Y LA declaracion
            xArchivo = xArchivo & Chr(34) & "bultos" & Chr(34) & ":{" & Chr(34) & "manifiesto" & Chr(34) & ":" & Chr(34) & Chr(34) & "," & Chr(34) & "tituloTransporte" & Chr(34) & ":" & Chr(34) & Chr(34) & "," & Chr(34) & "cancelaGlobal" & Chr(34) & ":" & Chr(34) & Chr(34) & "," & Chr(34) & "lineas" & Chr(34) & ":[]}}," & Chr(34) & "usuario_id" & Chr(34) & ":" & Chr(34) & "11623" & Chr(34) & "}"
            'xArchivo = xArchivo & Chr(34) & "usuario_id" & Chr(34) & ":" & Chr(34) & "11623" & Chr(34) & "}"
            'Next
            Dim fs As MemoryStream = New MemoryStream()
                fs = New System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(xArchivo))

                Response.ContentType = "text/plain"
                Response.AppendHeader("Content-Disposition", "attachment; filename=Exportar.decdat")
                Response.BinaryWrite(fs.ToArray())
                Response.End()

            End If
    End Sub

End Class