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
            lblCatatura.Text = Request.QueryString("idCaratula")
            'cargar logo para imprimir
            HiddenLogo.Value = "data:image/png;base64," & Application("ParametrosADMIN")(22)
            HiddenEmpresa.Value = Application("ParametrosADMIN")(2)

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

                Select Case Request.QueryString("action")
                    Case "deleteitems"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Items','El Item se elimino exitosamente.', 'success');</script>")
                    Case "deleteinactive"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Este usuario no puede ser eliminado, su estado paso a inactivo.', 'warning');</script>")
                    Case "deletefailed"
                        Page.ClientScript.RegisterStartupScript(Me.GetType(), "alert", "<script type=""text/javascript"">swal('Usuario','Error inesperado, este usuario no puedo ser eliminado.', 'error');</script>")
                End Select
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
            Response.Redirect("~/modulos/declaracion_aduanera/items.aspx?action=new&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btt_volver_Click(sender As Object, e As EventArgs) Handles btt_volver.Click
        Response.Redirect("~/modulos/declaracion_aduanera/caratula.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
    End Sub

    Private Sub bttEliminarDocumento_Click(sender As Object, e As EventArgs) Handles bttEliminarDocumento.Click
        Try
            Dim Ssql As String = "DELETE FROM DB_Nac_Merca.tbl_34_mercancias WHERE ID_Merca = " & lblHiddenIDDocumento.Value
            Using con As New ControlDB
                con.GME(Ssql, ControlDB.TipoConexion.Cx_Aduana)
            End Using
            'Using log_bitacora As New ControlBitacora
            '    log_bitacora.acciones_Comunes(6, Session("user_idUsuario"), Session("IDfrmQueIngresa"), "Se se elimino items: " & lblHiddenIDDocumento.Value & " con exito")
            'End Using

            Response.Redirect("~/modulos/declaracion_aduanera/Creacion_items.aspx?action=update&idCaratula=" & Request.QueryString("idCaratula"))
        Catch ex As Exception

        End Try
    End Sub
End Class