Public Class menu_principal
    'OBJETO #4
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
                Response.Redirect("~/Inicio/login.aspx")
            ElseIf Session("user_rol") = 6 Then
                'no tiene rol
                Using log_bitacora As New ControlBitacora
                    log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 4, "El usuario no tiene un rol asignado del menu principar es enviado al mensjae de alerta")
                End Using
                Response.Redirect("~/modulos/confi_rol.aspx")
            Else
                If CBool(Application("ParametrosSYS")(2)) = True Then

                    Dim Ssql As String = "SELECT T02.* FROM DB_Nac_Merca.tbl_37_permisos_modulos T01 LEFT JOIN DB_Nac_Merca.tbl_36_modulos T02 ON T01.id_modulo = T02.id_modulo WHERE T01.id_rol = " & Session("user_rol") & " and T01.estado = 1 and T02.estado = 1"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    Dim registro As DataRow
                    Dim arrayDeModulos(0, 7) As String

                    If Session("NumReg") > 0 Then
                        ReDim arrayDeModulos(Session("NumReg") - 1, 7)
                        For i = 0 To Session("NumReg") - 1
                            registro = DataSetX.Tables(0).Rows(i)
                            arrayDeModulos(i, 0) = registro("id_modulo")
                            arrayDeModulos(i, 1) = registro("titulo")
                            arrayDeModulos(i, 2) = registro("subtitulo")
                            arrayDeModulos(i, 3) = registro("detalle")
                            arrayDeModulos(i, 4) = registro("icono")
                            arrayDeModulos(i, 5) = registro("ruta")
                        Next
                        Session("arrayModulos") = arrayDeModulos
                        Session("TotalModulos") = Session("NumReg")
                    End If
                    If Not IsPostBack Then
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 4
                        Session("NombrefrmQueIngresa") = "Menu Principal"
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                    End If
                Else
                    Response.Redirect("~/modulos/configuraciones/confi_configurar.aspx")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class