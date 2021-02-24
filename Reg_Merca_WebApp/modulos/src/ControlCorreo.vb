Imports System.Net
Imports System.Net.Mail
Imports System.Security.Cryptography

Public Class ControlCorreo
    Implements IDisposable
    Public Sub Dispose2() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub
    Function envio_correo(ByVal xmsj As String, ByVal xmsjLink As String, ByVal emaildDestinatario As String, ByVal emailRemitente As String, ByVal calve As String, ByVal nombredDestinatario As String, ByVal urlLink As String, ByVal asunto As String, ByVal Host As String, ByVal puerto As Integer)
        Using mm As New MailMessage(emailRemitente, emaildDestinatario)
            mm.Subject = asunto
            mm.From = New MailAddress(emailRemitente, "RegMERCA")
            Dim body As String = "Hola " + nombredDestinatario + ","
            body += "<br /><br />" + xmsj
            body += "<br /><a href = '" + urlLink + "'>" + xmsjLink + "</a>"
            body += "<br /><br />Gracias"
            mm.Body = body
            mm.IsBodyHtml = True
            Dim smtp As New SmtpClient()
            smtp.Host = Host
            smtp.EnableSsl = True
            Dim NetworkCred As New NetworkCredential(emailRemitente, Desencriptar(calve))
            smtp.UseDefaultCredentials = True
            smtp.Credentials = NetworkCred
            smtp.Port = puerto
            smtp.Send(mm)
        End Using

        Return True

    End Function



    Private des As New TripleDESCryptoServiceProvider 'Algorithmo TripleDES
    Private hashmd5 As New MD5CryptoServiceProvider 'objeto md5
    Private myKey As String = "BDJzjq9wYr29fk31GmbS3g==" 'Clave secreta(puede alterarse)
    Private disposedValue As Boolean

    'Funcion para el Encriptado de Cadenas de Texto
    Function Encriptar(ByVal texto As String) As String

        If Trim(texto) = "" Then
            Encriptar = ""
        Else
            des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(myKey))
            des.Mode = CipherMode.ECB
            Dim encrypt As ICryptoTransform = des.CreateEncryptor()
            Dim buff() As Byte = UnicodeEncoding.ASCII.GetBytes(texto)
            Encriptar = Convert.ToBase64String(encrypt.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Encriptar
    End Function


    'Funcion para el Desencriptado de Cadenas de Texto
    Function Desencriptar(ByVal texto As String) As String
        If Trim(texto) = "" Then
            Desencriptar = ""
        Else
            des.Key = hashmd5.ComputeHash((New UnicodeEncoding).GetBytes(myKey))
            des.Mode = CipherMode.ECB
            Dim desencrypta As ICryptoTransform = des.CreateDecryptor()
            Dim buff() As Byte = Convert.FromBase64String(texto)
            Desencriptar = UnicodeEncoding.ASCII.GetString(desencrypta.TransformFinalBlock(buff, 0, buff.Length))
        End If
        Return Desencriptar
    End Function



End Class
