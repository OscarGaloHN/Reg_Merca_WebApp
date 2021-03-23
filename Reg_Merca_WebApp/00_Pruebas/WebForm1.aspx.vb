Imports System.Net
Imports System.IO
Public Class WebForm1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim amount As Decimal = 0
        Dim fromCurrency As String = ""
        Dim toCurrency As String = ""
        fromCurrency = TextBox1.Text.ToUpper()
        toCurrency = TextBox2.Text.ToUpper()
        amount = Convert.ToDecimal(TextBox3.Text)

        'Dim web As WebClient = New WebClient()
        'Dim uri As Uri = New Uri(String.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}", fromCurrency, toCurrency, amount))
        'Dim response As String = web.DownloadString(uri)
        'Dim regex As Regex = New Regex("rhs: \""(\d*.\d*)")
        'Dim match As Match = regex.Match(response)
        'Dim test As String = match.ToString()
        'Dim rate As Decimal = Convert.ToDecimal(match.Groups(1).Value)
        TextBox4.Text = CurrencyConversion(amount, fromCurrency, toCurrency)
    End Sub
    Public Function CurrencyConversion(ByVal amount As Decimal, ByVal fromCurrency As String, ByVal toCurrency As String) As String
        Dim web As New WebClient()
        'Dim apiURL As String = String.Format("http://www.google.com/ig/calculator?hl=en&q={2}{0}%3D%3F{1}", fromCurrency.ToUpper(), toCurrency.ToUpper(), amount)
        Dim apiURL As String = String.Format("https://www.google.com/search?q=1+USD+TO+HNL")
        'Dim request = WebRequest.Create(apiURL)

        Dim response As String = web.DownloadString(apiURL)
        Dim regex As New Regex(":(?<rhs>.+?),")
        Dim arrDigits As String() = regex.Split(response)
        Dim rate As String = arrDigits(3)


        'Dim response As String = web.DownloadString(url)
        'Dim regex As New Regex(":(?<rhs>.+?),")
        'Dim arrDigits As String() = regex.Split(response)
        'Dim rate As String = arrDigits(3)
        Return rate
    End Function
End Class