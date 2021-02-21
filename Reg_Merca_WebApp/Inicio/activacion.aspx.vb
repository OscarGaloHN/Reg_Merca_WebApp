Public Class activacion
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
        If Not Me.IsPostBack Then
            Dim activationCode As String = If(Not String.IsNullOrEmpty(Request.QueryString("ActivationCode")), Request.QueryString("ActivationCode"), Guid.Empty.ToString())
            Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_35_activacion_usuario where codigo_activacion =  '" & activationCode & "'"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                'comprobar que no este vencido
                Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                If CDate(registro("vencimiento")) > DateTime.Now Then
                    Select Case registro("tipo")
                        Case "registro" 'usuario nuevo, confirmar email, cambio de clave.

                        Case "clave" 'cambio de contraseña

                    End Select
                Else
                    'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Su solicitud a cadacudao, realice una nueva solicitud de configuración.', 'warning');</script>")

                End If
            Else
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Autenticación','Este intento de validacion.', 'warning');</script>")

            End If
        End If
    End Sub
End Class