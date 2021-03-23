Imports System.ComponentModel
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports System.Net
Imports System.Text.RegularExpressions

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WebService
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function CurrencyConversion(ByVal amount As Decimal, ByVal fromCurrency As String, ByVal toCurrency As String) As String
        Dim web As New WebClient()
        Dim url As String = String.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}=?{1}", fromCurrency.ToUpper(), toCurrency.ToUpper(), amount)
        '"http://www.google.com/ig/calculator?hl=en&q=$amount$from_Currency=?$to_Currency";
        Dim response As String = web.DownloadString(url)
        Dim regex As New Regex(":(?<rhs>.+?),")
        Dim arrDigits As String() = regex.Split(response)
        Dim rate As String = arrDigits(3)
        Return rate
    End Function
End Class