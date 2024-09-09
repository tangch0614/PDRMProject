Public Class ResourceHelper

    Public Function GetText(ByVal key As String) As String
        If Not String.IsNullOrWhiteSpace(key) Then
            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.Load("App_GlobalResources")
            Dim resourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.language", assembly)
            resourceManager.IgnoreCase = True
            Dim value As String = resourceManager.GetString(key)
            If Not String.IsNullOrEmpty(value) Then
                Return value
            Else
                value = resourceManager.GetString("_" & key)
                If Not String.IsNullOrEmpty(value) Then
                    Return value
                Else
                    Return key
                End If
            End If
        Else
            Return key
        End If
    End Function

    Public Function GetResource() As System.Resources.ResourceManager
        Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.Load("App_GlobalResources")
        Dim resourceManager As System.Resources.ResourceManager = New System.Resources.ResourceManager("Resources.language", assembly)
        Return resourceManager
    End Function

End Class
