Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


Namespace DataAccess

    Public Class LanggaranDB

#Region "Public Methods"

        Public Shared Function GetWebContent(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As LanggaranObj
            Dim webcontent As LanggaranObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblLanggaran Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    webcontent = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return webcontent
        End Function

        Public Shared Function GetWebContentList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblLanggaran", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetWebContentList(ByVal ID As Long, ByVal subject As String, ByVal categoryID As Long, ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not ID <= 0 Then query &= " w.fldID = @fldID AND "
            If Not categoryID <= 0 Then query &= " w.fldCategoryID = @fldCategoryID AND "
            If Not String.IsNullOrWhiteSpace(subject) Then query &= " w.fldSubject Like @fldSubject AND "
            If Not String.IsNullOrWhiteSpace(status) Then query &= " w.fldStatus = @fldStatus AND "
            If Not String.IsNullOrWhiteSpace(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select w.*, c.fldName As fldCategoryName, " _
                                                             & " ifnull(Case When w.fldCreatorType = 'A' Then a.fldName Else m.fldCode End,'') As fldCreatorName " _
                                                             & " From tblLanggaran w " _
                                                             & " Join tblLanggarancategory c On c.fldID = w.fldCategoryID " _
                                                             & " Left Join tbladmin a On a.fldID = w.fldCreatorID And w.fldCreatorType = 'A' " _
                                                             & " Left Join tblmembership m On m.fldID = w.fldCreatorID And w.fldCreatorType = 'M' " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", ID)
            myCommand.Parameters.AddWithValue("@fldCategoryID", categoryID)
            myCommand.Parameters.AddWithValue("@fldSubject", "%" & subject & "%")
            myCommand.Parameters.AddWithValue("@fldStatus", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetWebContentCategory(ByVal status As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not String.IsNullOrWhiteSpace(status) Then query &= " fldStatus = @fldStatus AND "
            If Not String.IsNullOrWhiteSpace(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblLanggarancategory " & query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldStatus", status)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        'Public Shared Function Save(ByVal webcontent As LanggaranObj, ByVal myConnection As MySqlConnection) As Integer
        '    Dim result As Long = 0
        '    Dim processExe As String = ""
        '    Dim processReturn As String = "Select LAST_INSERT_ID()"
        '    Dim isInsert As Boolean = True
        '    If webcontent.fldID = Nothing Then
        '        processExe = "Insert into tblLanggaran (fldDateTime, fldCategoryID, fldSubject, fldContent, fldCreatorID, fldCreatorType, fldStatus) Values (@fldDateTime, @fldCategoryID, @fldSubject, @fldContent, @fldCreatorID, @fldCreatorType, @fldStatus)"
        '        isInsert = True
        '    Else
        '        processExe = "Update tblLanggaran set fldDateTime = @fldDateTime, fldCategoryID = @fldCategoryID, fldSubject = @fldSubject, fldContent = @fldContent, fldCreatorID = @fldCreatorID, fldCreatorType = @fldCreatorType, fldStatus = @fldStatus Where fldID = @fldID"
        '        isInsert = False
        '    End If
        '    Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
        '    myCommand.CommandType = CommandType.Text
        '    If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", webcontent.fldID)

        '    myCommand.Parameters.AddWithValue("@fldDateTime", webcontent.fldDateTime)
        '    myCommand.Parameters.AddWithValue("@fldCategoryID", webcontent.fldCategoryID)
        '    myCommand.Parameters.AddWithValue("@fldSubject", webcontent.fldSubject)
        '    myCommand.Parameters.AddWithValue("@fldContent", webcontent.fldContent)
        '    myCommand.Parameters.AddWithValue("@fldCreatorID", webcontent.fldCreatorID)
        '    myCommand.Parameters.AddWithValue("@fldCreatorType", webcontent.fldCreatorType)
        '    myCommand.Parameters.AddWithValue("@fldStatus", webcontent.fldStatus)

        '    result = myCommand.ExecuteNonQuery()
        '    If isInsert Then
        '        myCommand = New MySqlCommand(processReturn, myConnection)
        '        myCommand.CommandType = CommandType.Text
        '        result = myCommand.ExecuteScalar
        '    End If
        '    Return result
        'End Function

        Public Shared Function Delete(ByVal fldID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblLanggaran Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As LanggaranObj
            Dim webcontent As LanggaranObj = New LanggaranObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                webcontent.fldID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDateTime"))) Then
                webcontent.fldDateTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("fldDateTime"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldoppID"))) Then
                webcontent.fldoppID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldoppID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldoppName"))) Then
                webcontent.fldoppName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldoppName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldoppContactNo"))) Then
                webcontent.fldoppContactNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldoppContactNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldoppJabatan"))) Then
                webcontent.fldoppJabatan = myDataRecord.GetString(myDataRecord.GetOrdinal("fldoppJabatan"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldoppBalai"))) Then
                webcontent.fldoppBalai = myDataRecord.GetString(myDataRecord.GetOrdinal("fldoppBalai"))
            End If


            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPolisID"))) Then
                webcontent.fldPolisID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldPolisID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldpolisNo"))) Then
                webcontent.fldpolisNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldpolisNo"))
            End If

            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldpolisContactNo"))) Then
                webcontent.fldpolisContactNo = myDataRecord.GetString(myDataRecord.GetOrdinal("fldpolisContactNo"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBalaiName"))) Then
                webcontent.fldBalaiName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBalaiName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBalaiContactNo1"))) Then
                webcontent.fldBalaiContactNo1 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBalaiContactNo1"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBalaiContactNo2"))) Then
                webcontent.fldBalaiContactNo2 = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBalaiContactNo2"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldemdID"))) Then
                webcontent.fldemdID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldemdID"))
            End If

            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldIMEI"))) Then
                webcontent.fldIMEI = myDataRecord.GetString(myDataRecord.GetOrdinal("fldIMEI"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldGPS"))) Then
                webcontent.fldGPS = myDataRecord.GetString(myDataRecord.GetOrdinal("fldGPS"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldLocation"))) Then
                webcontent.fldLocation = myDataRecord.GetString(myDataRecord.GetOrdinal("fldLocation"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldBatt"))) Then
                webcontent.fldBatt = myDataRecord.GetString(myDataRecord.GetOrdinal("fldBatt"))
            End If

            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                webcontent.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            Return webcontent
        End Function
    End Class

End Namespace