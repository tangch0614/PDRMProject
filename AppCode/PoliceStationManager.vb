Imports System.Data
Imports System.Transactions
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject
Imports AppCode.DataAccess


Namespace BusinessLogic

    Public Class PoliceStationManager

#Region "Public Methods"

        Public Shared Function GetPoliceStationType(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetPoliceStationType(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetPoliceStationIPK(ByVal id As Long) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = PoliceStationDB.GetPoliceStationIPK(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetPoliceStationIPD(ByVal id As Long) As Long
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Long = PoliceStationDB.GetPoliceStationIPD(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetPoliceStationName(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetPoliceStationName(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetPoliceStationContactNo(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetPoliceStationContactNo(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetDepartmentName(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetDepartmentName(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetState(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetState(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetDistrict(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetDistrict(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetMukim(ByVal id As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = PoliceStationDB.GetMukim(id, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function GetPoliceStationList(ByVal id As Long, ByVal ipkid As Long, ByVal ipdid As Long, ByVal type As String, ByVal state As String, ByVal district As String, ByVal mukim As String, ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = PoliceStationDB.GetPoliceStationList(id, ipkid, ipdid, type, state, district, mukim, status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetDepartmentList(ByVal status As String) As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = PoliceStationDB.GetDepartmentList(status, myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

#End Region

    End Class

End Namespace
