Public Class Creacion_items
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

        Try


            If Session("user_idUsuario") = Nothing Then
                Session.Abandon()
                Response.Redirect("~/Inicio/login.aspx")
            Else
                'Llenado de Gried
                Dim Ssql As String = String.Empty
                Ssql = "SELECT ROW_NUMBER() OVER (	ORDER BY ID_Merca) as numeroitems,a.ID_Merca,a.Id_poliza,
a.pesoneto,a.num_partida,a.cod_pais_fab,a.importes_factura
from tbl_34_mercancias a, tbl_01_polizas b
where a.Id_poliza=b.Id_poliza
and a.Id_poliza=" & Request.QueryString("idCaratula")

                Using con As New ControlDB
                    DataSetX = con.SelectX(Ssql, ControlDB.TipoConexion.Cx_Aduana)
                    Session("NumReg") = DataSetX.Tables(0).Rows.Count
                End Using
                If Session("NumReg") > 0 Then
                    gvCustomers.DataSource = DataSetX
                    gvCustomers.DataBind()
                End If

                'Select Case Request.QueryString("action")
                '    Case "deleteusuer"
                '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','El Usuario se elimino exitosamente.', 'success');</script>")
                '    Case "deleteinactive"
                '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Este usuario no puede ser eliminado, su estado paso a inactivo.', 'warning');</script>")
                '    Case "deletefailed"
                '        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Error inesperado, este usuario no puedo ser eliminado.', 'error');</script>")
                'End Select
            End If

            'If Not IsPostBack Then
            '    Using cusuario_bitacora As New ControlBitacora
            '        cusuario_bitacora.acciones_Comunes(3, Session("user_idUsuario"), 7, "El usuario ingresa a la pantalla de configuracion de usuarios")
            '    End Using
            'End If



        Catch ex As Exception

        End Try

    End Sub


    Private Sub bttNuevo_Click(sender As Object, e As EventArgs) Handles bttNuevo.Click
        Try
            'redirecciona a form items
            Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=new")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btt_volver_Click(sender As Object, e As EventArgs) Handles btt_volver.Click
        Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx")
    End Sub
End Class