'code retrieved from:http://www.codeproject.com/Articles/169371/Captcha-Image-using-C-in-ASP-NET
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Drawing.Imaging
Imports AppCode.BusinessObject
Imports AppCode.BusinessLogic

Partial Public Class CaptchaCode
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Create a random code and store it in the Session object.
        Session("CaptchaImageText") = UtilityManager.GenerateRandomNumber(5)
        ' Create a CAPTCHA image using the text stored in the Session object.
        Dim ci As New CaptchaImage(Session("CaptchaImageText").ToString(), 300, 50, Drawing.Color.Gray, Drawing.Color.Black)
        ' Change the response headers to output a JPEG image.
        Response.Clear()
        Response.ContentType = "image/jpeg"
        ' Write the image to the response stream in JPEG format.
        ci.Image.Save(Response.OutputStream, ImageFormat.Jpeg)
        ' Dispose of the CAPTCHA image object.
        ci.Dispose()
    End Sub

    ' Function to generate random string with Random class.
    'Private Function GenerateRandomCode() As String
    '    Dim r As New Random()
    '    Dim s As String = ""
    '    For j As Integer = 0 To 4
    '        Dim i As Integer = r.[Next](3)
    '        Dim ch As Integer
    '        Select Case i
    '            Case 1
    '                ch = r.[Next](1, 9)
    '                s = s & ch.ToString()
    '                Exit Select
    '            Case 2
    '                ch = r.[Next](65, 90)
    '                s = s & Convert.ToChar(ch).ToString()
    '                Exit Select
    '            Case 3
    '                ch = r.[Next](97, 122)
    '                s = s & Convert.ToChar(ch).ToString()
    '                Exit Select
    '            Case Else
    '                ch = r.[Next](97, 122)
    '                s = s & Convert.ToChar(ch).ToString()
    '                Exit Select
    '        End Select
    '        r.NextDouble()
    '        r.[Next](100, 1999)
    '    Next
    '    Return s
    'End Function
End Class
