Imports MySql.Data.MySqlClient
Imports System.Web
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
    Function GME_Recuperar_ID(ByRef Ssql As String, ByRef Tipo As TipoConexion)
        Using con As New MySqlConnection(GetCadenaConexion(Tipo))
            con.Open()
            Dim command As MySqlCommand
            'Dim Adapter As MySqlDataAdapter = New MySqlDataAdapter()
            command = New MySqlCommand(Ssql, con)
            'Adapter.InsertCommand = New MySqlCommand(Ssql, con)
            HttpContext.Current.Session("GME_Recuperar_ID") = CInt(command.ExecuteScalar())
            'Adapter.InsertCommand.ExecuteNonQuery()
            'HttpContext.Current.Session("GME_Recuperar_ID") = CInt(command.LastInsertedId())
            command.Dispose()
            con.Close()
        End Using
        Return True
        'Dim conn As String = ConfigurationManager.ConnectionStrings("Cstr_6").ConnectionString
        'Dim con As SqlConnection = New SqlConnection(conn)
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
    Function ParametrosSYS_ADMIN(ByVal TipoParametro As String)
        'parametros de configuracion de sistema
        Dim Ssql As String = String.Empty
        Dim ds As DataSet = New DataSet()
        Dim NumReg As Integer
        Dim arrayParametros(0) As String
        Select Case TipoParametro
            Case "sistema"
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_21_parametros WHERE parametro like '%SYS%' order by 1;"
            Case "adminstrador"
                Ssql = "SELECT * FROM DB_Nac_Merca.tbl_21_parametros WHERE parametro like '%ADMIN%' order by 1;"
        End Select
        Using con As New ControlDB
            ds = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            NumReg = ds.Tables(0).Rows.Count
        End Using
        Dim registro As DataRow
        If NumReg > 0 Then
            ReDim arrayParametros(NumReg - 1)
            For i = 0 To arrayParametros.Length - 1
                registro = ds.Tables(0).Rows(i)
                If IsDBNull(registro("valor")) = False Then
                    arrayParametros(i) = registro("valor")
                End If
            Next
        End If
        Return arrayParametros
    End Function

    Function ConvertirIMG(ByVal xPath As String)
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(xPath)
        Dim response As System.Net.WebResponse = req.GetResponse()
        Dim stream As IO.Stream = response.GetResponseStream()
        'Dim img As System.Drawing.Image = System.Drawing.Image.FromStream(stream)
        Dim fs As System.IO.Stream = response.GetResponseStream()
        Dim br As New System.IO.BinaryReader(fs)
        Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Integer))
        Dim base64String As String = Convert.ToBase64String(bytes, 0, bytes.Length)
        stream.Close()
        Return base64String
    End Function
End Class
