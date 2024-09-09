Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Request.QueryString("ReturnUrl") Is Nothing Then
        '    Dim returnUrl As String = Request.QueryString("ReturnUrl")
        '    Dim urlPath() As String = returnUrl.Split("/")
        '    If urlPath(1).Equals("admins") Then
        '        Response.Redirect("~/secure/Login_a.aspx?ReturnUrl=" & returnUrl)
        '    ElseIf urlPath(1).Equals("member") Then
        '        Response.Redirect("~/Login_m.aspx?ReturnUrl=" & returnUrl)
        '    End If
        'Else
        Response.Redirect("~/Login.aspx")
        'End If
    End Sub

End Class