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

    Private Sub ColocarControles()
        For i = 0 To Session("preguntasDeUsuario").Length - 1
            Dim nuevoTxt As TextBox = New TextBox()
            nuevoTxt.ID = "txtRespuesta" & i.ToString()
            nuevoTxt.CssClass = "form-control"
            nuevoTxt.MaxLength = "15"
            nuevoTxt.Attributes.Add("autocomplete", "off")

            pnlMain.Controls.Add(New LiteralControl("   
                    <div Class='row'>
                           <div Class='col-xs-12'>
                             <p Class='font-bold col-teal'>" & UCase(Session("preguntasDeUsuario")(i)) & " </p>
                              <div Class='form-group form-float'>
                                  <div Class='form-line'>"))
            pnlMain.Controls.Add(nuevoTxt)
            pnlMain.Controls.Add(New LiteralControl("<Label Class='form-label'>Respuesta</label>
                                    </div>
                                </div>
                            </div>
                        </div>"))

        Next
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            'PARAMETROS DE ADMINISTRADOR
            Dim Ssql As String = String.Empty
            Dim registro As DataRow
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
            ColocarControles()
        End If
    End Sub

    Private Sub bttverificar_Click(sender As Object, e As EventArgs) Handles bttverificar.Click

        Dim xWere, cajatexto As String
        cajatexto = ""
        xWere = ""
        'xWere = "Pregunta ='" & Session("preguntasDeUsuario")(0) & "' and respuesta='" & tb.Text & "'"
        For i = 0 To Session("preguntasDeUsuario").Length - 1
            cajatexto = "txtRespuesta" & i

            xWere = xWere & " Pregunta ='" & Session("preguntasDeUsuario")(i) & "' and respuesta='" & Request.Form(cajatexto).ToString() & "'"
        Next
        MsgBox(xWere)
        ColocarControles()
        'txtRespuesta.Text = ""

    End Sub
End Class