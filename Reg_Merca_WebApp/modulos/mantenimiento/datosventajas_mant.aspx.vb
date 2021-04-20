Public Class datosventajas_mant
    Inherits System.Web.UI.Page
    Private Property DataSetX As DataSet
        Get
            Return CType(Session("DataSetX"), DataSet)
        End Get
        Set(ByVal value As DataSet)
            Session("DataSetX") = value
        End Set
    End Property
    'OBJETO #24
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)
            'llenar grid
            Dim Ssql As String = String.Empty
            Ssql = "SELECT * FROM DB_Nac_Merca.tbl_42_Datos_Ventaja"
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            End If

            If Not IsPostBack Then
                Select Case Request.QueryString("acction")
                    Case "newdatosvent"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('DATOS VENTAJA','El Dato Ventaja se Guardo con exito.', 'success');</script>")
                    Case "deltedatosvent"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('DATOS VENTAJA','El Dato Ventaja se Elimino con exito.', 'success');</script>")
                    Case "editdatosvent"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('DATOS VENTAJA','El Dato Ventaja se Modifico con exito.', 'success');</script>")
                    Case Else
                        'bitacora de que salio de un form
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(10, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario sale a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If

                        'bitacora de que ingreso al form
                        Session("IDfrmQueIngresa") = 43
                        Session("NombrefrmQueIngresa") = "Mantenimiento de Datos Ventaja"
                        If Not IsPostBack Then
                            Using log_bitacora As New ControlBitacora
                                log_bitacora.acciones_Comunes(9, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "El usuario ingresa a la pantalla de " & Session("NombrefrmQueIngresa"))
                            End Using
                        End If
                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub

End Class