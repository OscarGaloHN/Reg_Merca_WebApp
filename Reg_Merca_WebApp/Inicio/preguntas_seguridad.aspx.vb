Public Class preguntas_seguridad
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
        If IsPostBack = False Then
            'parametros de configuracion de sistema
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_21_parametros WHERE parametro like '%SYS%' order by 1;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            Dim registro As DataRow
            If Session("NumReg") > 0 Then
                Dim arrayParametros(CInt(Session("NumReg")) - 1) As String
                For i = 0 To arrayParametros.Length - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    'arrayParametros(i) = registro("valor")
                    If IsDBNull(registro("valor")) = False Then
                        arrayParametros(i) = registro("valor")
                    End If
                Next
                'parametros de contraseña
                Application("ParametrosSYS") = arrayParametros
            End If

            'PARAMETROS DE ADMINISTRADOR
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_21_parametros WHERE parametro like '%ADMIN%' order by 1;"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim arrayParametrosADMIN(CInt(Session("NumReg")) - 1) As String
                For i = 0 To arrayParametrosADMIN.Length - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    'arrayParametros(i) = registro("valor")
                    If IsDBNull(registro("valor")) = False Then
                        arrayParametrosADMIN(i) = registro("valor")
                    End If
                Next
                Application("ParametrosADMIN") = arrayParametrosADMIN
            End If

            'PREGUNTAS
            Ssql = "SELECT T01.*, T02.usuario, T03.pregunta FROM DB_Nac_Merca.tbl_23_preguntas_usuario T01 LEFT JOIN DB_Nac_Merca.tbl_02_usuarios T02 ON T01.id_usuario = T02.id_usuario LEFT JOIN DB_Nac_Merca.tbl_22_preguntas T03 ON T01.id_pregunta = T03.id_pregunta WHERE T02.usuario = '" & Session("usuarioPreguntas") & "' order by rand() limit " & Application("ParametrosADMIN")(7) & ""
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Dim arrayPREGUNTAS(CInt(Session("NumReg")) - 1) As String
                For i = 0 To arrayPREGUNTAS.Length - 1
                    registro = DataSetX.Tables(0).Rows(i)
                    'arrayParametros(i) = registro("valor")
                    If IsDBNull(registro("pregunta")) = False Then
                        arrayPREGUNTAS(i) = registro("pregunta")
                    End If
                Next
                Session("preguntasDeUsuario") = arrayPREGUNTAS
            End If
        End If
    End Sub

End Class