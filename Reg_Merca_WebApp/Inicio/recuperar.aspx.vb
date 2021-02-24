Public Class recuperar
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
        bttPreguntas.Attributes.Add("onClick", "return false;")
        bttEnviar.Focus()
        If IsPostBack = False Then
            'parametros de configuracion de sistema
            Using Parametros_Sistema As New ControlDB
                Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
            End Using

            'PARAMETROS DE ADMINISTRADOR
            Using Parametros_admin As New ControlDB
                Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
            End Using
        End If
    End Sub

    Private Sub bttContinuar_Click(sender As Object, e As EventArgs) Handles bttContinuar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where usuario = BINARY  '" & txtUsuarioPreguntas.Text & "';"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
            If CInt(registro("estado")) = 0 Or CInt(registro("estado")) = 3 Or CInt(registro("estado")) = 5 Then
                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Advertencia','Este usuario puede estar inactivo, caducado o sin completar el registro.', 'warning');</script>")
            Else
                Session("usuarioPreguntas") = registro("id_usuario")
                Response.Redirect("~/Inicio/preguntas.aspx")
            End If
            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('EXITO','Usuario  encontrado', 'success');</script>")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Advertencia','Usuario no encontrado', 'warning');</script>")
        End If
    End Sub

    Private Sub bttEnviar_Click(sender As Object, e As EventArgs) Handles bttEnviar.Click
        Dim Ssql As String = String.Empty
        Ssql = "SELECT  * FROM DB_Nac_Merca.tbl_02_usuarios   where correo = BINARY  '" & txtEmail.Text & "';"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then
            'enviar correo electronico con token de nueva contraseña
            Dim registro As DataRow
            registro = DataSetX.Tables(0).Rows(0)
            Dim activationCode As String = Guid.NewGuid().ToString()
            Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & registro("id_usuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL 2 DAY),'registro',1);"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electronico','Correo Enviado', 'success');</script>")
        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo Electronico','Correo no enviado', 'error');</script>")
        End If
    End Sub
End Class