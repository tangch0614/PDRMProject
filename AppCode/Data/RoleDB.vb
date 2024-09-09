Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class RoleDB

#Region "Public Methods"

        Public Shared Function GetRoleName(ByVal fldRoleID As Long, ByVal UserType As String, ByVal myConnection As MySqlConnection) As String
            Dim query As String = ""
            If UserType.Equals("A") Then
                query = "Select ifnull(fldRoleName,'') From tbladmintype Where fldRoleID = @fldRoleID"
            ElseIf UserType.Equals("M") Then
                query = "Select ifnull(fldRoleName,'') From tblmembertype Where fldRoleID = @fldRoleID"
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(query, myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            Return myCommand.ExecuteScalar()
        End Function

        Public Shared Function Authorization(ByVal roleID As Long, ByVal UserType As String, ByVal menuPath As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim menu As String = ""
            If UserType.Equals("A") Then
                menu = "tbladminmenu m"
            ElseIf UserType.Equals("M") Then
                menu = "tblmembermenu m"
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT COUNT(m.fldMenuID) FROM " & menu & " JOIN tblrolemenu r ON r.fldMenuID = m.fldMenuID WHERE fldUserType = @fldUserType AND fldRoleID = @fldRoleID AND fldMenuPath = @fldMenuPath AND m.fldStatus = 1", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldUserType", UserType)
            myCommand.Parameters.AddWithValue("@fldRoleID", roleID)
            myCommand.Parameters.AddWithValue("@fldMenuPath", menuPath)
            result = myCommand.ExecuteScalar
            Return result > 0
        End Function

        Public Shared Function GetRoleMenu(ByVal fldRoleID As Long, ByVal fldUserType As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT IFNULL(r.fldroleID, -1) As fldRoleID, m.* FROM tbladminmenu m LEFT JOIN tblrolemenu r ON r.fldMenuID = m.fldMenuID AND r.fldUserType = @fldUserType AND r.fldRoleID = @fldRoleID WHERE m.fldStatus = 1 ORDER BY m.fldParentMenuID, m.fldMenuOrder", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserType", fldUserType)
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetRoleMenuList(ByVal fldRoleID As Long, ByVal fldUserType As String, ByVal myConnection As MySqlConnection) As Array
            Dim rolemenulist As List(Of String) = New List(Of String)
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT * FROM tblrolemenu WHERE fldUserType = @fldUserType And fldRoleID = @fldRoleID ORDER BY fldMenuID", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserType", fldUserType)
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            Dim myDataReader As MySqlDataReader = myCommand.ExecuteReader(CommandBehavior.Default)
            Do While myDataReader.Read
                rolemenulist.Add(myDataReader.Item("fldMenuID"))
            Loop
            Return rolemenulist.ToArray()
        End Function

        Public Shared Function GetRole(ByVal fldRoleID As Long, ByVal myConnection As MySqlConnection) As RoleObj
            Dim myRole As RoleObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tbladmintype Where fldRoleID = @fldRoleID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myRole = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myRole
        End Function

        Public Shared Function GetRoleList(ByVal UserType As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If UserType.Equals("A") Then
                query = "Select * From tbladmintype Where fldStatus='Y'"
            ElseIf UserType.Equals("M") Then
                query = "Select * From tblmembertype"
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(query, myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal roleID As Long, ByVal UserType As String, ByVal menuList As String(), ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Delete From tblrolemenu Where fldRoleID = @fldRoleID And fldUserType = @fldUserType", myConnection)
            myCommand.Parameters.AddWithValue("@fldRoleID", roleID)
            myCommand.Parameters.AddWithValue("@fldUserType", UserType)
            myCommand.ExecuteNonQuery()
            Dim cmd As String = "Insert Into tblrolemenu (fldRoleID, fldMenuID, fldUserType) Values "
            For i As Integer = 0 To menuList.Length - 1
                If Not i = menuList.Length - 1 Then
                    cmd &= String.Format("(@fldRoleID{0}, @fldMenuID{0}, @fldUserType{0}),", i)
                Else
                    cmd &= String.Format("(@fldRoleID{0}, @fldMenuID{0}, @fldUserType{0});", i)
                End If
            Next
            myCommand = New MySqlCommand(cmd, myConnection)
            For i As Integer = 0 To menuList.Length - 1
                myCommand.Parameters.AddWithValue("@fldRoleID" & i, roleID)
                myCommand.Parameters.AddWithValue("@fldMenuID" & i, menuList(i))
                myCommand.Parameters.AddWithValue("@fldUserType" & i, UserType)
            Next
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function Save(ByVal myRole As RoleObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myRole.fldRoleID = Nothing Then
                processExe = "Insert into tbladmintype (fldRoleName) Values (@fldRoleName)"
                isInsert = True
            Else
                processExe = "Update tbladmintype set fldRoleName = @fldRoleName Where fldRoleID = @fldRoleID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldRoleID", myRole.fldRoleID)

            myCommand.Parameters.AddWithValue("@fldRoleName", myRole.fldRoleName)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldRoleID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tbladmintype Where fldRoleID = @fldRoleID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldRoleID", fldRoleID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As RoleObj
            Dim myRole As RoleObj = New RoleObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRoleID"))) Then
                myRole.fldRoleID = myDataRecord.GetInt64(myDataRecord.GetOrdinal("fldRoleID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldRoleName"))) Then
                myRole.fldRoleName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldRoleName"))
            End If
            Return myRole
        End Function
    End Class

 End NameSpace 
