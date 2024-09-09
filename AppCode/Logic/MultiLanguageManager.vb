Imports System.Transactions
Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


NameSpace BusinessLogic

    Public Class MultiLanguageManager

#Region "Public Methods"

        Public Shared Function GetTextList(ByVal Language As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = MultiLanguageDB.GetTextList(SetLanguageField(Language), myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function


        Public Shared Function GetText(ByVal Text As String, ByVal DataTable As DataTable) As String
            If Not DataTable Is Nothing AndAlso DataTable.Rows.Count > 0 Then
                Dim valueRow As DataRow() = DataTable.Select("fldText = '" & UtilityManager.EscapeChars(Text) & "'")
                If valueRow.Count > 0 Then
                    If Not valueRow(0)("Text") Is DBNull.Value Then
                        Return valueRow(0)("Text")
                    End If
                End If
            End If
            Return Text
        End Function

        Public Shared Function GetText(ByVal Text As String, ByVal Language As String) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myText As String = MultiLanguageDB.GetText(Text, SetLanguageField(Language), myConnection)
                myConnection.Close()
                If Not myText Is DBNull.Value Then
                    Return myText
                End If
                Return Text
            End Using
        End Function

#End Region

#Region "Private Methods"

        Private Shared Function SetLanguageField(ByVal Language As String) As String
            If Not String.IsNullOrEmpty(Language) Then
                Return "fld" & Language.Replace("-", "_")
            End If
            Return "fldDefault"
        End Function

#End Region

    End Class

End Namespace
