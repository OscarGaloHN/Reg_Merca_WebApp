Imports MySql.Data.MySqlClient
Public Class ControlBitacora
    Implements IDisposable
    Public Sub Dispose3() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub

    Public Enum TipoConexion_Bitacora
        Cx_Aduana = 1
    End Enum
    Private Function GetCadenaConexion_Bitacora(ByRef Tipo As TipoConexion_Bitacora) As String
        Select Case Tipo
            Case TipoConexion_Bitacora.Cx_Aduana
                Return ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString
        End Select
        Return String.Empty
    End Function

    Private Function GME_Bitacora(ByRef Ssql As String, ByRef Tipo As TipoConexion_Bitacora)
        Using con As New MySqlConnection(GetCadenaConexion_Bitacora(Tipo))
            con.Open()
            Dim command As MySqlCommand
            Dim Adapter As MySqlDataAdapter = New MySqlDataAdapter()
            command = New MySqlCommand(Ssql, con)
            Adapter.InsertCommand = New MySqlCommand(Ssql, con)
            Adapter.InsertCommand.ExecuteNonQuery()
            command.Dispose()
            con.Close()
        End Using
        Return Ssql
    End Function

    Function acciones_Comunes(ByVal TipoEvento As Integer, ByVal Id_usuario As Integer, ByVal xObjeto As Integer, ByVal xDetalle As String)
        Dim Ssql As String = ""
        Select Case TipoEvento
            Case 1 'el objeto del frm login es 3
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", 3, '" & "login" & "', '" & xDetalle & "');"
            Case 2 'el logout  
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "logout" & "', 'El usuario cierra sesión exitosamente desde la pagina de " & xDetalle & "');"
            Case 3 'consultar
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "consulta" & "', '" & xDetalle & "');"
            Case 4 'insertar
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "insert" & "', '" & xDetalle & "');"
            Case 5 'actualizar
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "update" & "', '" & xDetalle & "');"
            Case 6 'eliminar
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "delete" & "', 'El Usuario " & xDetalle & "');"
            Case 7 'inactivar
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "inactivo" & "', 'El Usuario " & xDetalle & "');"
            Case 8 'resetear
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "resetear" & "', 'El Usuario " & xDetalle & "');"
            Case 9 'INGRESA A PANTALLA
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "ingreso" & "', '" & xDetalle & "');"
            Case 10 'SALE DE PANTALLA
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "salida" & "', '" & xDetalle & "');"
            Case 11 'OTORGAR PERMISOS
                Ssql = "INSERT INTO DB_Nac_Merca.tbl_17_bitacora (fecha, id_usuario, id_objeto, accion, descripcion) VALUES (CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), " & Id_usuario & ", " & xObjeto & ", '" & "permisos" & "', '" & xDetalle & "');"
        End Select
        GME_Bitacora(Ssql, TipoConexion_Bitacora.Cx_Aduana)
        Return 0
    End Function


End Class
