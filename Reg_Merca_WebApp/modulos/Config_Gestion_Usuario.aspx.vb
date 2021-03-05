Public Class Config_Gestion_Usuario
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
        If Session("user_rol") = 5 Then
            SqlRol.SelectCommand = "Select rol, id_rol from DB_Nac_Merca.tbl_15_rol"
        Else
            SqlRol.SelectCommand = "Select rol, id_rol from DB_Nac_Merca.tbl_15_rol where id_rol not in (5)"
        End If

        'parametros de contraseña
        reContra.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        reContra.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
        txtContraseña.MaxLength = Application("ParametrosADMIN")(0)

        'parametros de contraseña robusta
        validadorContraRobusta.ErrorMessage = "La contraseña debe contener 1 letra minuscula, 1 letra mayuscula, 1 carácter especial, 1 numero y el rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        validadorContraRobusta.ValidationExpression = "^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"



        Select Case Request.QueryString("action")
            Case "new"


            Case "update"
                Dim Ssql As String = String.Empty
                Ssql = "select *from DB_Nac_Merca.tbl_02_usuarios where id_usuario =" & Request.QueryString("xuser") & ""
                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                Dim registro As DataRow
                If Session("NumReg") > 0 Then
                    'cargar txt
                    registro = DataSetX.Tables(0).Rows(0)

                    txtNombre.Text = registro("nombre")
                    txtUsuario.Text = registro("usuario")
                    cmbRol.SelectedValue = registro("id_rol")
                    txtCorreoElectronico.Text = registro("correo")
                    txtContraseña.Text = registro("clave")

                    'Fecha_Creacion.Value = registro("fecha_creacion")
                    ' Fecha_Vencimiento.Attributes("value") = CDate(registro("fecha_vencimiento")).ToShortDateString
                    'cmbEstado.SelectedValue = registro("estado")
                End If

        End Select
    End Sub
    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        If (UCase(txtUsuario.Text) = UCase(txtContraseña.Text)) Then
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','El usuario y la contraseña deben ser distintos.', 'error');</script>")
        Else
            'insertarUsuario(txtNombre.Text, txtUsuario.Text, cmbRol.SelectedValue, txtCorreoElectronico.Text, txtContraseña.Text, Fecha_Vencimiento.Value, cmbEstado.SelectedValue)
            Dim Ssql As String = ""
            Select Case Request.QueryString("action")
                Case "new"
                    Ssql = "Insert into DB_Nac_Merca.tbl_02_usuarios (Nombre,Usuario,id_rol,correo,clave,fecha_vencimiento,estado,fecha_creacion,fecha_vencimiento,creado_por,intentos,emailconfir) values ('" & txtNombre.Text & "', '" & txtUsuario.Text & "', " & cmbRol.SelectedValue & ", '" & txtCorreoElectronico.Text & "',  SHA('" & txtContraseña.Text & "'), '" & Fecha_Vencimiento.Value & "',0,CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosADMIN")(12) & " DAY), '" & Session("user_nombre_usuario") & "',0,0)"
            End Select

            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
        End If
    End Sub

End Class