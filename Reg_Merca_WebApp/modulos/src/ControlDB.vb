Imports MySql.Data.MySqlClient



Public Class ControlDB
    Implements IDisposable
    Public Enum TipoConexion
        Cx_Aduana = 1
    End Enum
    Private Function GetCadenaConexion(ByRef Tipo As TipoConexion) As String
        Select Case Tipo
            Case TipoConexion.Cx_Aduana
                Return ConfigurationManager.ConnectionStrings("Cstr_1").ConnectionString
        End Select
        Return String.Empty
    End Function
    Public Sub Dispose() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub
    Function GME(ByRef Ssql As String, ByRef Tipo As TipoConexion)
        Using con As New MySqlConnection(GetCadenaConexion(Tipo))
            con.Open()
            Dim command As MySqlCommand
            Dim Adapter As MySqlDataAdapter = New MySqlDataAdapter()
            command = New MySqlCommand(Ssql, con)
            Adapter.InsertCommand = New MySqlCommand(Ssql, con)
            Adapter.InsertCommand.ExecuteNonQuery()
            command.Dispose()
            con.Close()
        End Using
        'Dim conn As String = ConfigurationManager.ConnectionStrings("Cstr_6").ConnectionString
        'Dim con As SqlConnection = New SqlConnection(conn)
        Return Ssql
    End Function
    Function SelectX(ByRef Ssql As String, ByRef Tipo As TipoConexion)
        Dim ds As DataSet = New DataSet()
        Using con As New MySqlConnection(GetCadenaConexion(Tipo))
            con.Open()
            Dim cmd As MySqlCommand = New MySqlCommand(Ssql, con)
            cmd.CommandType = CommandType.Text
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            sda.Fill(ds)
            cmd.Dispose()
            'Dim NumReg As Integer = ds.Tables(0).Rows.Count
        End Using
        Return ds
    End Function

    Function SP_contra(ByVal xUsuario As Integer, ByVal xClave As String, ByVal Tipo As TipoConexion)
        Dim ds As DataSet = New DataSet()
        Using cnn As MySqlConnection = New MySqlConnection(GetCadenaConexion(Tipo))
            Dim cmd As MySqlCommand = New MySqlCommand("contrasenas", cnn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@idUsuario", xUsuario)
            cmd.Parameters.AddWithValue("@claveUsuario", "SHA(" & xClave & ")")
            cmd.ExecuteNonQuery()
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            sda.Fill(ds)
            cmd.Dispose()
        End Using
        Return 0
    End Function
End Class
