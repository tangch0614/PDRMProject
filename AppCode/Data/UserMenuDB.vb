Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class UserMenuDB

#Region "Public Methods"

        Public Shared Function GetPermittedList(ByVal fldUserID As Long, ByVal fldUserType As String, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT ifnull(fldPermitID,'') FROM tblusermenu WHERE fldUserType = @fldUserType And fldUserID = @fldUserID", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserType", fldUserType)
            myCommand.Parameters.AddWithValue("@fldUserID", fldUserID)
            Dim menulist As String = myCommand.ExecuteScalar()
            Return menulist
        End Function

        Public Shared Function GetRestrictedList(ByVal fldUserID As Long, ByVal fldUserType As String, ByVal myConnection As MySqlConnection) As String
            Dim myCommand As MySqlCommand = New MySqlCommand("SELECT ifnull(fldRestrictID,'') FROM tblusermenu WHERE fldUserType = @fldUserType And fldUserID = @fldUserID", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserType", fldUserType)
            myCommand.Parameters.AddWithValue("@fldUserID", fldUserID)
            Dim menulist As String = myCommand.ExecuteScalar()
            Return menulist
        End Function

        Public Shared Function GetList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblUserMenu", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function SavePermit(ByVal userID As Long, ByVal UserType As String, ByVal permitList As String(), ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Delete From tblUsermenu Where fldUserID = @fldUserID And fldUserType = @fldUserType", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserID", userID)
            myCommand.Parameters.AddWithValue("@fldUserType", UserType)
            myCommand.ExecuteNonQuery()
            If permitList.Count > 0 Then
                Dim cmd As String = "Insert Into tblUsermenu (fldUserID, fldPermitID, fldUserType) Values (@fldUserID, @fldPermitID, @fldUserType)"
                myCommand = New MySqlCommand(cmd, myConnection)
                myCommand.Parameters.AddWithValue("@fldUserID", userID)
                myCommand.Parameters.AddWithValue("@fldPermitID", String.Join(",", permitList))
                myCommand.Parameters.AddWithValue("@fldUserType", UserType)
                result = myCommand.ExecuteNonQuery()
            Else
                result = 1
            End If
            Return result > 0
        End Function

        Public Shared Function SaveRestrict(ByVal userID As Long, ByVal UserType As String, ByVal restrictList As String(), ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = Nothing
            myCommand = New MySqlCommand("Delete From tblUsermenu Where fldUserID = @fldUserID And fldUserType = @fldUserType", myConnection)
            myCommand.Parameters.AddWithValue("@fldUserID", userID)
            myCommand.Parameters.AddWithValue("@fldUserType", UserType)
            myCommand.ExecuteNonQuery()
            If restrictList.Count > 0 Then
                Dim cmd As String = "Insert Into tblUsermenu (fldUserID, fldRestrictID, fldUserType) Values (@fldUserID, @fldRestrictID, @fldUserType)"
                myCommand = New MySqlCommand(cmd, myConnection)
                myCommand.Parameters.AddWithValue("@fldUserID", userID)
                myCommand.Parameters.AddWithValue("@fldRestrictID", String.Join(",", restrictList))
                myCommand.Parameters.AddWithValue("@fldUserType", UserType)
                result = myCommand.ExecuteNonQuery()
            Else
                result = 1
            End If
            Return result > 0
        End Function

#End Region

    End Class

 End NameSpace 
