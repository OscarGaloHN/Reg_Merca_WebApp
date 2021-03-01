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
                    'cmbRol.SelectedValue = registro("id_rol")
                    txtCorreoElectronico.Text = registro("correo")
                    txtContraseña.Text = registro("clave")
                    Fecha_Creacion.Value = registro("fecha_creacion")
                    'Fecha_Vencimiento.Value = registro("fecha_vencimiento")
                    'cmbEstado.SelectedValue = registro("estado")
                End If

        End Select
    End Sub
    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click
        If (txtUsuario.Text = txtContraseña.Text) Then
            MsgBox("El usuario y la contraseña deben ser distintos", vbOKOnly, "Notificación")
        Else
            'insertarUsuario(txtNombre.Text, txtUsuario.Text, cmbRol.SelectedValue, txtCorreoElectronico.Text, txtContraseña.Text, Fecha_Vencimiento.Value, cmbEstado.SelectedValue)
            Dim Ssql As String = "Insert into DB_Nac_Merca.tbl_02_usuarios (Nombre,Usuario,id_rol,correo,clave,fecha_vencimiento,estado,fecha_creacion) values ('" & txtNombre.Text & "', '" & txtUsuario.Text & "', " & cmbRol.SelectedValue & ", '" & txtCorreoElectronico.Text & "',  SHA('" & txtContraseña.Text & "'), '" & Fecha_Vencimiento.Value & "',0,CONVERT_TZ(NOW(), @@session.time_zone, '-6:00')"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
        End If
    End Sub

End Class