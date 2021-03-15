﻿Public Class bitacora
    Inherits System.Web.UI.Page
    'OBJETO #12
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Using log_bitacora As New ControlBitacora
                log_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 12, "El usuario ingresa a la pantalla de bitácora")
            End Using
        End If


        Dim Ssql As String = String.Empty

        Ssql = "SELECT T01.*, T02.usuario, T03.objeto FROM DB_Nac_Merca.tbl_17_bitacora T01
LEFT JOIN DB_Nac_Merca.tbl_02_usuarios T02 ON T01.id_usuario = T02.id_usuario
LEFT JOIN DB_Nac_Merca.tbl_16_objetos T03 ON T01.id_objeto = T03.id_objeto"




        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            gvCustomers.DataSource = DataSetX
            gvCustomers.DataBind()
        End If
    End Sub

End Class