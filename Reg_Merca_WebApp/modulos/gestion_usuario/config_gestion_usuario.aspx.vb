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

        Using Parametros_Sistema As New ControlDB
            Application("ParametrosSYS") = Parametros_Sistema.ParametrosSYS_ADMIN("sistema")
        End Using

        Using Parametros_admin As New ControlDB
            Application("ParametrosADMIN") = Parametros_admin.ParametrosSYS_ADMIN("adminstrador")
        End Using










        If Session("user_rol") = 5 Then
            SqlRol.SelectCommand = "Select rol, id_rol from DB_Nac_Merca.tbl_15_rol"
        Else
            SqlRol.SelectCommand = "Select rol, id_rol from DB_Nac_Merca.tbl_15_rol where id_rol not in (5)"
        End If

        'parametros de contraseña
        reqcontra.ErrorMessage = "El rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        reqcontra.ValidationExpression = "^[\s\S]{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"
        txtContra.MaxLength = Application("ParametrosADMIN")(0)

        'parametros de contraseña robusta
        ReqValidacionRobusta.ErrorMessage = "La contraseña debe contener 1 letra minuscula, 1 letra mayuscula, 1 carácter especial, 1 numero y el rango de caracteres debe de ser entre (" & Application("ParametrosADMIN")(18) & " -" & Application("ParametrosADMIN")(0) & ")."
        ReqValidacionRobusta.ValidationExpression = "^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{" & Application("ParametrosADMIN")(18) & "," & Application("ParametrosADMIN")(0) & "}$"



        Select Case Request.QueryString("action")
            Case "new"
                Dim fechaactual As Date = (Date.Now)
                txtFechaCreacion.Text = fechaactual
                Fecha_Vencimiento_usuario.Text = fechaactual.AddDays(Application("ParametrosADMIN")(12))




                txtContra.Attributes("value") = CrearPassword(Application("ParametrosADMIN")(0))
                txtContraConfirmar.Attributes("value") = txtContra.Attributes("value")

                cmbEstado.Enabled = False

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
                    txtContra.Text = registro("clave")

                    'Fecha_Creacion.Value = registro("fecha_creacion")
                    ' Fecha_Vencimiento.Attributes("value") = CDate(registro("fecha_vencimiento")).ToShortDateString
                    'cmbEstado.SelectedValue = registro("estado")
                End If
            Case Else
                Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx")
        End Select

    End Sub
    Private Sub bttGuardar_Click(sender As Object, e As EventArgs) Handles bttGuardar.Click

        'Dim nombre As String

        Select Case Request.QueryString("action")
            Case "new"

                If (UCase(txtUsuario.Text) = UCase(txtContra.Text)) Then
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Contraseña','El usuario y la contraseña deben ser distintos.', 'error');</script>")
                Else


                    Dim Ssql As String = "CALL autoregistro('" & txtUsuario.Text & "', '" & txtCorreoElectronico.Text & "')"
                    Using con As New ControlDB
                        DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                        Session("NumReg") = DataSetX.Tables(0).Rows.Count
                    End Using
                    If Session("NumReg") > 0 Then
                        Dim registro As DataRow = DataSetX.Tables(0).Rows(0)
                        Select Case registro("EXISTE")
                            Case -1 'usuario y correo existen
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario & Correo','El usuario y correo electronico ya estan registrados.', 'error');</script>")
                            Case -2 'usuario existe
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Nombre de usuario','El nombre de usuario ya esta registrado.', 'error');</script>")
                            Case -3 'correo existe
                                Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Correo electronico','El correo electronico ya estan registrado.', 'error');</script>")
                            Case 0 'no existe
                                Ssql = "Insert into DB_Nac_Merca.tbl_02_usuarios (Nombre,Usuario,id_rol,correo,clave,estado,fecha_creacion,fecha_vencimiento,creado_por,intentos,emailconfir) values ('" & txtNombre.Text & "', '" & txtUsuario.Text & "', " & cmbRol.SelectedValue & ", '" & txtCorreoElectronico.Text & "',  SHA('" & txtContra.Text & "'),0,CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosADMIN")(12) & " DAY), '" & Session("user_nombre_usuario") & "',0,0)"
                                Using con As New ControlDB
                                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                                End Using
                                SendActivationEmail()
                                Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?action=UsuarioRegistrado")
                        End Select
                    End If


                End If


        End Select





        'nombre = InputBox("Ingrese Nombre ",
        '                "Registro de Datos Personales",
        '                "Nombre", 100, 0)
        'MessageBox.Show("Bienvenido Usuario: " + nombre,
        '                "Registro de Datos Personales",
        '                MessageBoxButtons.OK,
        '                MessageBoxIcon.Information)

    End Sub
    Private Sub SendActivationEmail()

        Dim Ssql As String = "SELECT * FROM DB_Nac_Merca.tbl_02_usuarios where usuario = BINARY  '" & txtUsuario.Text & "'"
        Using con As New ControlDB
            DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            Session("NumReg") = DataSetX.Tables(0).Rows.Count
        End Using
        If Session("NumReg") > 0 Then

            Dim registro As DataRow
            registro = DataSetX.Tables(0).Rows(0)
            Dim activationCode As String = Guid.NewGuid().ToString()
            Ssql = "INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & registro("id_usuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL " & Application("ParametrosSYS")(7) & " DAY),'registro',1);"
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using

            Dim urllink As String = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) & "/Inicio/activacion.aspx?ActivationCode=" & activationCode

            Using xCorreo As New ControlCorreo
                xCorreo.envio_correo("Para continuar con el registro haga click en el siguiente enlace y asi poder activar su cuenta.", "ACTIVAR CUENTA",
                                         txtCorreoElectronico.Text, Application("ParametrosADMIN")(9), Application("ParametrosADMIN")(11),
                                       txtNombre.Text.Trim(),
                                         urllink, "Activación de Cuenta",
                                         Application("ParametrosADMIN")(15), Application("ParametrosADMIN")(10),
                                         Application("ParametrosSYS")(0) & " " & Application("ParametrosSYS")(1))
            End Using

        Else
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El registro no fue completado. Intentelo de nuevo.', 'error');</script>")
        End If
    End Sub

    Private Sub bttVolver_Click(sender As Object, e As EventArgs) Handles bttVolver.Click
        Response.Redirect("~/modulos/gestion_usuario/config_usuarios.aspx?")

    End Sub

    Private Sub bttResetear_Click(sender As Object, e As EventArgs) Handles bttResetear.Click

    End Sub

    Private Sub bttGenerar_Click(sender As Object, e As EventArgs) Handles bttGenerar.Click
        txtContra.Attributes("value") = CrearPassword(Application("ParametrosADMIN")(0))
        txtContraConfirmar.Attributes("value") = txtContra.Attributes("value")
    End Sub

    Private Function CrearPassword(ByVal longitud As Integer) As String
        Dim obj As New Random()
        Dim xNuevoPass As New StringBuilder
        Dim idx As Integer
        Dim xmayus As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" '1
        Dim xminus As String = "abcdefghijklmnopqrstuvwxyz" '1
        Dim xnumeros As String = "0123456789" '1
        Dim xcaracteres As String = "!#$@%&()""*+'<>?=" '1
        Dim xTodos As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$@%&()""*+'<>?=" 'el parametro de longitud de contra menos la cantidad variables anteriores


        idx = obj.Next(0, xmayus.Length - 1)
        xNuevoPass.Append(xmayus.Substring(idx, 1))

        idx = obj.Next(0, xminus.Length - 1)
        xNuevoPass.Append(xminus.Substring(idx, 1))

        idx = obj.Next(0, xnumeros.Length - 1)
        xNuevoPass.Append(xnumeros.Substring(idx, 1))

        idx = obj.Next(0, xcaracteres.Length - 1)
        xNuevoPass.Append(xcaracteres.Substring(idx, 1))

        For i = 1 To longitud - 4
            idx = obj.Next(0, xTodos.Length - 1)
            xNuevoPass.Append(xTodos.Substring(idx, 1))
        Next


        Return xNuevoPass.ToString
    End Function

End Class