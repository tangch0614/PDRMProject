Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class NewsDB

#Region "Public Methods"

        Public Shared Function SearchNewsList(ByVal newsID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal creator As String, ByVal subject As String, ByVal status As String, ByVal language As String, ByVal popup As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim query As String = " N.fldStatus != 'D' AND "
            If Not newsID = 0 Then query &= " N.fldID Like @fldID AND "
            If Not popup < 0 Then query &= " N.fldPopup = @fldPopup AND "
            If Not String.IsNullOrEmpty(creator) Then query &= " A.fldCode Like @fldCode AND "
            If Not String.IsNullOrEmpty(subject) Then query &= " N.fldSubject Like @fldSubject AND "
            If Not String.IsNullOrEmpty(status) Then query &= " N.fldStatus Like @fldStatus AND "
            If Not String.IsNullOrEmpty(language) Then query &= " N.fldLanguage = @fldLanguage AND "
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " Date(N.fldDateTime) >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " Date(N.fldDateTime) <= @dateTo AND "
            If Not String.IsNullOrWhiteSpace(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select N.*, A.fldCode From tblNews N Left Join tbladmin A On A.fldID = N.fldCreatorID " & query & " Order By N.fldDateTime Desc, N.fldID Desc", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", newsID)
            myCommand.Parameters.AddWithValue("@fldPopup", popup)
            myCommand.Parameters.AddWithValue("@fldCode", "%" & creator & "%")
            myCommand.Parameters.AddWithValue("@fldSubject", "%" & subject & "%")
            myCommand.Parameters.AddWithValue("@fldStatus", status)
            myCommand.Parameters.AddWithValue("@dateFrom", dateFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTo)
            myCommand.Parameters.AddWithValue("@fldLanguage", language)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function SearchNewsList(ByVal newsID As Long, ByVal dateFrom As String, ByVal dateTo As String, ByVal creator As String, ByVal subject As String, ByVal status As String, ByVal language As String, ByVal popup As Integer, ByVal PageNum As Integer, ByVal PageSize As Integer, ByRef PageCount As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand
            Dim recordCount As Integer = 0
            Dim limiter As String = ""
            Dim query As String = " N.fldStatus != 'D' AND "
            If Not newsID = 0 Then query &= " N.fldID Like @fldID AND "
            If Not popup < 0 Then query &= " N.fldPopup = @fldPopup AND "
            If Not String.IsNullOrEmpty(creator) Then query &= " A.fldCode Like @fldCode AND "
            If Not String.IsNullOrEmpty(subject) Then query &= " N.fldSubject Like @fldSubject AND "
            If Not String.IsNullOrEmpty(status) Then query &= " N.fldStatus = @fldStatus AND "
            If Not String.IsNullOrEmpty(language) Then query &= " N.fldLanguage = @fldLanguage AND "
            If Not String.IsNullOrEmpty(dateFrom) Then query &= " Date(N.fldDateTime) >= @dateFrom AND "
            If Not String.IsNullOrEmpty(dateTo) Then query &= " Date(N.fldDateTime) <= @dateTo AND "
            If Not String.IsNullOrWhiteSpace(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", newsID)
            myCommand.Parameters.AddWithValue("@fldPopup", popup)
            myCommand.Parameters.AddWithValue("@fldCode", "%" & creator & "%")
            myCommand.Parameters.AddWithValue("@fldSubject", "%" & subject & "%")
            myCommand.Parameters.AddWithValue("@fldStatus", status)
            myCommand.Parameters.AddWithValue("@dateFrom", dateFrom)
            myCommand.Parameters.AddWithValue("@dateTo", dateTo)
            myCommand.Parameters.AddWithValue("@fldLanguage", language)
            If Not PageSize = 0 Then
                limiter &= " Limit " & (PageNum - 1) * PageSize & "," & PageSize
                myCommand.CommandText = "Select Count(*) " _
                                    & " From tblNews N " _
                                    & " Left Join tbladmin A On A.fldID = N.fldCreatorID " _
                                    & query
                recordCount = myCommand.ExecuteScalar()
                If recordCount <= PageSize Then
                    PageCount = 1
                ElseIf recordCount > PageSize Then
                    PageCount = If(PageSize <= 0, 1, Math.Ceiling(recordCount / PageSize))
                End If
            End If
            myCommand.CommandText = "Select N.*, A.fldCode From tblNews N Left Join tbladmin A On A.fldID = N.fldCreatorID " & query & " Order By N.fldDateTime Desc, N.fldID Desc " & limiter
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetNews(ByVal fldID As Integer, ByVal myConnection As MySqlConnection) As NewsObj
            Dim myNews As NewsObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblnews Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myNews = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myNews
        End Function

        Public Shared Function GetNewsList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblnews Order By fldDateTime Desc", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetNewsList(ByVal Limit As Integer, ByVal language As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblnews Where fldStatus = 'Y' And fldLanguage = @fldLanguage Order By fldDateTime Desc Limit " & Limit, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldLanguage", language)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetNewsList(ByVal Limit As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblnews Where fldStatus = 'Y' Order By fldDateTime Desc Limit " & Limit, myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal myNews As NewsObj, ByVal myConnection As MySqlConnection) As Long
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myNews.fldID = Nothing Then
                processExe = "Insert into tblnews (fldDateTime, fldSubject, fldContent, fldCreatorID, fldCreatorName, fldStatus, fldLanguage, fldPopup) Values (@fldDateTime, @fldSubject, @fldContent, @fldCreatorID, @fldCreatorName, @fldStatus, @fldLanguage, @fldPopup)"
                isInsert = True
            Else
                processExe = "Update tblnews set fldDateTime = @fldDateTime, fldSubject = @fldSubject, fldContent = @fldContent, fldCreatorID = @fldCreatorID, fldCreatorName = @fldCreatorName, fldStatus = @fldStatus, fldLanguage=@fldLanguage, fldPopup=@fldPopup Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", myNews.fldID)

            myCommand.Parameters.AddWithValue("@fldDateTime", myNews.fldDateTime)
            myCommand.Parameters.AddWithValue("@fldSubject", myNews.fldSubject)
            myCommand.Parameters.AddWithValue("@fldContent", myNews.fldContent)
            myCommand.Parameters.AddWithValue("@fldCreatorID", myNews.fldCreatorID)
            myCommand.Parameters.AddWithValue("@fldCreatorName", myNews.fldCreatorName)
            myCommand.Parameters.AddWithValue("@fldStatus", myNews.fldStatus)
            myCommand.Parameters.AddWithValue("@fldLanguage", myNews.fldLanguage)
            myCommand.Parameters.AddWithValue("@fldPopup", myNews.fldPopup)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblnews Set fldStatus = 'D' Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        'Public Shared Function Delete(ByVal fldID As Integer, ByVal myConnection As MySqlConnection) As Boolean
        '    Dim result As Long = 0
        '    Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblnews Where fldID = @fldID", myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    myCommand.Parameters.AddWithValue("@fldID", fldID)
        '    result = myCommand.ExecuteNonQuery()
        '    Return result > 0
        'End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As NewsObj
            Dim myNews As NewsObj = New NewsObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                myNews.fldID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateTime"))) Then
                myNews.fldDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldSubject"))) Then
                myNews.fldSubject = myDataRecord.GetString(myDataRecord.GetOrdinal("fldSubject"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldContent"))) Then
                myNews.fldContent = myDataRecord.GetString(myDataRecord.GetOrdinal("fldContent"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorID"))) Then
                myNews.fldCreatorID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldCreatorID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCreatorName"))) Then
                myNews.fldCreatorName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCreatorName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                myNews.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLanguage"))) Then
                myNews.fldLanguage = myDataRecord.GetString(myDataRecord.GetOrdinal("fldLanguage"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPopup"))) Then
                myNews.fldPopup = myDataRecord.GetInt32(myDataRecord.GetOrdinal("fldPopup"))
            End If
            Return myNews
        End Function
    End Class

 End NameSpace 
