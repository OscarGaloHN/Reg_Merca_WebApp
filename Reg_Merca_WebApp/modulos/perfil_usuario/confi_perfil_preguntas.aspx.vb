
Public Class confi_perfil_preguntas
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
        If Session("user_idUsuario") <> Nothing Then

            SqlPreguntas.SelectCommand = "SELECT id_pregunta, UPPER(pregunta) pregunta FROM DB_Nac_Merca.tbl_22_preguntas
            WHERE id_pregunta Not in (select id_pregunta FROM DB_Nac_Merca.tbl_23_preguntas_usuario where id_usuario = " & Session("user_idUsuario") & ")  order by 1;"
            SqlEditaPregunta.SelectCommand = "SELECT id_pregunta, UPPER(pregunta) pregunta FROM DB_Nac_Merca.tbl_22_preguntas order by 1"
            Dim Ssql As String = String.Empty
            Ssql = "select T01.id_pregunta_usuario ID, T02.pregunta, T01.respuesta from DB_Nac_Merca.tbl_23_preguntas_usuario  T01
                left join DB_Nac_Merca.tbl_22_preguntas  T02 ON T01.id_pregunta = T02.id_pregunta WHERE id_usuario=" & Session("user_idUsuario") & ""
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using

            lblprguntas.Text = Session("NumReg")
            lblpreguntasrequeridas.Text = Application("ParametrosADMIN")(8)
            If Session("NumReg") > 0 Then
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
            Else
                gvCustomers.DataSource = DataSetX
                gvCustomers.DataBind()
                'Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Bloqueo','sin preguntas', 'warning');</script>")
            End If

            If Session("NumReg") >= Application("ParametrosADMIN")(8) Then
                PanelPregunta.Visible = False
                If Session("user_estado") = 1 Then
                    Ssql = "UPDATE   `DB_Nac_Merca`.`tbl_02_usuarios` SET ESTADO=2  WHERE id_usuario=" & Session("user_idUsuario") & ";" '"INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & registro("id_usuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL 2 DAY),'registro',1);"
                    Using con As New ControlDB
                        con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    End Using
                    Session("user_estado") = 2
                End If
            Else
                PanelPregunta.Visible = True
            End If

            'leer url
            Select Case Request.QueryString("acction")
                Case "newquestions"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas de Seguridad','La pregunta y su respuesta se almacenaron con exito.', 'success');</script>")
                Case "updatequestions"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas de Seguridad','La pregunta y su respuesta se actualizaron con exito.', 'success');</script>")
                Case "updateerror"
                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas de Seguridad','No se permite repetir preguntas.', 'error');</script>")
                Case "awquestions"

                    Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Preguntas de Seguridad','Debe de completar las preguntas de seguridad requeridas.', 'warning');</script>")

            End Select
        End If

    End Sub

    Private Sub bttGuardarPregunta_Click(sender As Object, e As EventArgs) Handles bttGuardarPregunta.Click
        Dim Ssql As String = "INSERT INTO `DB_Nac_Merca`.`tbl_23_preguntas_usuario` (`id_pregunta`, `id_usuario`, `respuesta`) VALUES (" & cmbPreguntas.SelectedValue & ", " & Session("user_idUsuario") & ", '" & txtrespuesta.Text & "');" '"INSERT INTO `DB_Nac_Merca`.`tbl_35_activacion_usuario` (`id_usuario`, `codigo_activacion`, `vencimiento`,`tipo`,`estado`) VALUES (" & registro("id_usuario") & ", '" & activationCode & "',DATE_ADD(CONVERT_TZ(NOW(), @@session.time_zone, '-6:00'), INTERVAL 2 DAY),'registro',1);"
        Using con As New ControlDB
            con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
        End Using
        Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=newquestions")
    End Sub

    Private Sub bttActualizar_Click(sender As Object, e As EventArgs) Handles bttActualizar.Click
        Dim Ssql As String = "SELECT *FROM `DB_Nac_Merca`.`tbl_23_preguntas_usuario`  WHERE id_pregunta=" & cmbNuevaPregunta.SelectedValue & " AND id_usuario= " & Session("user_idUsuario") & ""

        If CmbHiddenField1.Value <> cmbNuevaPregunta.SelectedItem.Text Then
            Using con As New ControlDB
                DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                Session("NumReg") = DataSetX.Tables(0).Rows.Count
            End Using
            If Session("NumReg") > 0 Then
                Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=updateerror")

            Else
                lblIDPregunta.Text = lblHidden1.Value
                Ssql = "update   `DB_Nac_Merca`.`tbl_23_preguntas_usuario` set id_pregunta=" & cmbNuevaPregunta.SelectedValue & ", respuesta= '" & txtRespuestaEditar.Text & "'  where id_usuario= " & Session("user_idUsuario") & " and id_pregunta_usuario=" & lblIDPregunta.Text & ""
                Using con As New ControlDB
                    con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                End Using
                Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=updatequestions")
            End If
        Else
            lblIDPregunta.Text = lblHidden1.Value
            Ssql = "update   `DB_Nac_Merca`.`tbl_23_preguntas_usuario` set id_pregunta=" & cmbNuevaPregunta.SelectedValue & ", respuesta= '" & txtRespuestaEditar.Text & "'  where id_usuario= " & Session("user_idUsuario") & " and id_pregunta_usuario=" & lblIDPregunta.Text & ""
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            Response.Redirect("~/modulos/confi_perfil_preguntas.aspx?acction=updatequestions")
        End If

    End Sub
End Class