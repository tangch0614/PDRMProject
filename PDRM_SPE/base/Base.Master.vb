Imports System.Globalization
Imports System.Threading
Imports AppCode.BusinessLogic

Public Class Base1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("LanguageCookie") Is Nothing Then 'check if languagecookie exists
            PreferredLanguage() 'initialize language if cookie not exists
        End If
        Dim language As String = Request.Cookies("LanguageCookie").Value
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(language)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub PreferredLanguage()
        Dim language As String = Application("DefaultLanguage") 'set language as default
        If HttpContext.Current.User.Identity.IsAuthenticated() AndAlso Not CStr(AdminAuthentication.GetUserData(4)) Is Nothing Then 'check if usercookie and userpreferlanguagecookie exists
            language = AdminAuthentication.GetUserData(4) 'set language to user preferred language if cookie exists
        End If
        GenerateLanguageCookie(language) 'set language cookie
    End Sub

    Protected Sub GenerateLanguageCookie(ByVal Language As String)
        Dim languageCookie As HttpCookie = Response.Cookies("LanguageCookie")
        If languageCookie Is Nothing Then
            languageCookie = New HttpCookie("LanguageCookie")
            languageCookie.Expires = DateTime.Now.AddDays(1)
        End If
        languageCookie.Value = Language
    End Sub

    Public Function GetText(ByVal key As String) As String
        Dim resourseHelper As ResourceHelper = New ResourceHelper
        Return resourseHelper.GetText(key)
    End Function

    Public Function GetResource() As System.Resources.ResourceManager
        Dim resourseHelper As ResourceHelper = New ResourceHelper
        Return resourseHelper.GetResource()
    End Function
End Class