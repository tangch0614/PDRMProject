Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject

Namespace DataAccess

    Public Class MultiLanguageDB

#Region "Public Methods"

        Public Shared Function GetText(ByVal Text As String, ByVal LanguageField As String, ByVal myConnection As MySqlConnection) As String
            Return MySqlHelper.ExecuteScalar(myConnection, "Select " & LanguageField & " From tblmultilanguage Where fldText = @fldText", New MySqlParameter("@fldText", Text))
        End Function

        Public Shared Function GetTextList(ByVal LanguageField As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldID, fldText, " & LanguageField & " As Text From tblmultilanguage", myConnection)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetTextList(ByVal LanguageField As String, ByVal Texts As String(), ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim TextIn As String = " ("
            If Not Texts.Count <= 0 Then
                For i As Integer = 0 To Texts.Count - 1
                    If Not i = Texts.Count - 1 Then
                        TextIn &= String.Format("@fldText{0},", i)
                    Else
                        TextIn &= String.Format("@fldText{0}", i)
                        TextIn &= ") "
                    End If
                Next
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldID, fldText, " & LanguageField & " As Text From tblmultilanguage Where fldText In " & TextIn, myConnection)
            If Texts.Count > 0 Then
                For i As Integer = 0 To Texts.Count - 1
                    myCommand.Parameters.AddWithValue("@fldText" & i, Texts(i))
                Next
            End If
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

#End Region

    End Class

End Namespace
