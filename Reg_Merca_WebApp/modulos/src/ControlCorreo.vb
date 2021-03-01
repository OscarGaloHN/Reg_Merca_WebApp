Imports System.Net
Imports System.Net.Mail
Imports System.Security.Cryptography

Public Class ControlCorreo
    Implements IDisposable
    Public Sub Dispose2() Implements IDisposable.Dispose
        GC.SuppressFinalize(Me)
    End Sub

    Function envio_correo(ByVal xmsj As String, ByVal xmsjLink As String, ByVal emaildDestinatario As String, ByVal emailRemitente As String, ByVal calve As String, ByVal nombredDestinatario As String, ByVal urlLink As String, ByVal asunto As String, ByVal Host As String, ByVal puerto As Integer, ByVal xversionapp As String)
        Using mm As New MailMessage(emailRemitente, emaildDestinatario)
            mm.Subject = asunto
            mm.From = New MailAddress(emailRemitente, "RegMERCA")
            Dim body As String = "<!DOCTYPE html><html lang='es'><head><meta charset='utf-8'><title>Correo recuperacion</title></head>"
            body += "<body style='background-color: #ecf0f1 '><table style='max-width: 600px; padding: 10px; margin:0 auto; border-collapse: collapse;'><tr>"
            body += "<td style='background-color: #ecf0f1; text-align: Left(); padding: 0'>"
            body += "<img width='20%' style='display:block; margin: 2% 40.5%' src='https://drive.google.com/thumbnail?id=1CS7FKbXrvqBtp9ceQsh4_v1N7nbYSFkL'>"
            body += "</a></td></tr></tr></tr></tr><td style='background-color: #ecf0f1'>"
            body += "<div style='color: #34495e; margin: 4% 10% 2%; text-align: justify;font-family: sans-serif'>"
            body += "<h2 style='color: #009788; margin: 0 0 7px'>Estimado (a) " + nombredDestinatario + ".</h2>"
            body += "<p style='margin: 2px; font-size: 15px'> " + xmsj + " <br></ul>"
            body += "<div style='width: 100%; text-align: center; margin-top:5%'><br>"
            body += "<a style='text-decoration: none; border-radius: 5px; padding: 11px 23px; color: white; background-color: #009788' href=" + urlLink
            body += ">" + xmsjLink + " </a></div><p style='color: #b3b3b3; font-size: 12px; text-align: center;margin: 30px 0 0'>" + xversionapp + "</p></div></tr></td></table></body></html>"


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
    Private myKey As String = "BDJzjq9wYr29fk31GmbS3g==" '"92m1101sdI19" 'Clave secreta(puede alterarse)

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
