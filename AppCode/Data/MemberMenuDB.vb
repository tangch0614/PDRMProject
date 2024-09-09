Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class MemberMenuDB

#Region "Public Methods"

        Public Shared Function GetMenuID(ByVal menuPath As String, ByVal myConnection As MySqlConnection) As Long
            Dim mycommand As MySqlCommand = New MySqlCommand("Select fldMenuID From tblmembermenu Where fldMenuPath = @fldMenuPath", myConnection)
            mycommand.Parameters.AddWithValue("@fldMenuPath", menuPath)
            Dim result As Long = mycommand.ExecuteScalar
            Return result
        End Function

        Public Shared Function GetPermanentMenuList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT * FROM tblmembermenu WHERE fldStatus = 1 And fldPermanent = 1 Order By fldMenuOrder, fldMenuID", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMenuList(ByVal isSuper As Boolean, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT * FROM tblmembermenu WHERE fldStatus = 1 And fldDisplay = 1 Order By fldMenuOrder, fldMenuID", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMenuList(ByVal RoleID As Integer, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT M.* FROM tblmembermenu M JOIN tblrolemenu R ON R.fldMenuID = M.fldMenuID WHERE R.fldUserType = 'M' And R.fldRoleID = @fldRoleID And M.fldStatus = 1 And M.fldDisplay = 1 Order By fldMenuOrder, fldMenuID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldRoleID", RoleID)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMenuList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT M.* FROM tblmembermenu M WHERE M.fldStatus = 1 And M.fldDisplay = 1 Order By fldMenuOrder, fldMenuID", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetItem(ByVal fldMenuID As Long, ByVal myConnection As MySqlConnection) As MenuObj
            Dim myMenu As MenuObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembermenu Where fldMenuID = @fldMenuID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldMenuID", fldMenuID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myMenu = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myMenu
        End Function

        Public Shared Function GetList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblmembermenu", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal myMenu As MenuObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myMenu.fldMenuID = Nothing Then
                processExe = "Insert into tblmembermenu (fldMenuPath, fldParentMenuID, fldStatus, fldMenuOrder, fldMenuTitleText, fldMenuTarget, fldDisplay) Values (@fldMenuPath, @fldParentMenuID, @fldStatus, @fldMenuOrder, @fldMenuTitleText, @fldMenuTarget, @fldDisplay)"
                isInsert = True
            Else
                processExe = "Update tblmembermenu set fldMenuPath = @fldMenuPath, fldParentMenuID = @fldParentMenuID, fldStatus = @fldStatus, fldMenuOrder = @fldMenuOrder, fldMenuTitleText = @fldMenuTitleText, fldMenuTarget = @fldMenuTarget, fldDisplay = @fldDisplay Where fldMenuID = @fldMenuID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldMenuID", myMenu.fldMenuID)

            myCommand.Parameters.AddWithValue("@fldMenuPath", myMenu.fldMenuPath)
            myCommand.Parameters.AddWithValue("@fldParentMenuID", myMenu.fldParentMenuID)
            myCommand.Parameters.AddWithValue("@fldStatus", myMenu.fldStatus)
            myCommand.Parameters.AddWithValue("@fldMenuOrder", myMenu.fldMenuOrder)
            myCommand.Parameters.AddWithValue("@fldMenuTitleText", myMenu.fldMenuTitleText)
            myCommand.Parameters.AddWithValue("@fldMenuTarget", myMenu.fldMenuTarget)
            myCommand.Parameters.AddWithValue("@fldDisplay", myMenu.fldDisplay)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldMenuID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblmembermenu Where fldMenuID = @fldMenuID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldMenuID", fldMenuID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As MenuObj
            Dim myMenu As MenuObj = New MenuObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMenuID"))) Then
                myMenu.fldMenuID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldMenuID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMenuPath"))) Then
                myMenu.fldMenuPath = myDataRecord.GetString(myDataRecord.GetOrdinal("fldMenuPath"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldParentMenuID"))) Then
                myMenu.fldParentMenuID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldParentMenuID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                myMenu.fldStatus = myDataRecord.GetByte(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMenuOrder"))) Then
                myMenu.fldMenuOrder = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldMenuOrder"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMenuTitleText"))) Then
                myMenu.fldMenuTitleText = myDataRecord.GetString(myDataRecord.GetOrdinal("fldMenuTitleText"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldMenuTarget"))) Then
                myMenu.fldMenuTarget = myDataRecord.GetString(myDataRecord.GetOrdinal("fldMenuTarget"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldDisplay"))) Then
                myMenu.fldDisplay = myDataRecord.GetByte(myDataRecord.GetOrdinal("fldDisplay"))
            End If
            Return myMenu
        End Function
    End Class

 End NameSpace 
