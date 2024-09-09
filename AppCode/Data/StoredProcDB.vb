Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


Namespace DataAccess
    Public Class StoredProcDB

        Public Shared Sub spUpdateStock_NewProduct(ByVal ProductID As Long, ByVal myConnection As MySqlConnection)
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spUpdateStock_NewProduct"
            myCommand.Parameters.AddWithValue("?tPID", ProductID)
            myCommand.ExecuteNonQuery()
        End Sub

        Public Shared Sub spUpdateStock_NewStockist(ByVal MemberID As Long, ByVal myConnection As MySqlConnection)
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spUpdateStock_NewStockist"
            myCommand.Parameters.AddWithValue("?tMID", MemberID)
            myCommand.ExecuteNonQuery()
        End Sub

        Public Shared Function spListTrade(ByVal Type As String, ByVal refID As String, ByVal paymentType As String, ByVal memberID As Long, ByVal unit As Decimal, ByVal value As Decimal, ByVal rate As Decimal, ByVal tradeType As Integer, ByVal priority As Integer, ByVal remark As String, ByVal creatorID As Long, ByVal creatorType As String, ByVal company As Integer, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spListTrade"
            myCommand.Parameters.AddWithValue("?vType", Type)
            myCommand.Parameters.AddWithValue("?vRefID", refID)
            myCommand.Parameters.AddWithValue("?vPaymentType", paymentType)
            myCommand.Parameters.AddWithValue("?vMID", memberID)
            myCommand.Parameters.AddWithValue("?vUnit", unit)
            myCommand.Parameters.AddWithValue("?vValue", value)
            myCommand.Parameters.AddWithValue("?vRate", rate)
            myCommand.Parameters.AddWithValue("?vTradeType", tradeType)
            myCommand.Parameters.AddWithValue("?vPriority", priority)
            myCommand.Parameters.AddWithValue("?vRemark", remark)
            myCommand.Parameters.AddWithValue("?vCreatorID", creatorID)
            myCommand.Parameters.AddWithValue("?vCreatorType", creatorType)
            myCommand.Parameters.AddWithValue("?vCompany", company)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function spGenerateMID(ByVal memberID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spGenerateMID"
            myCommand.Parameters.AddWithValue("?vMID", memberID)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function spGenerateCode(ByVal type As String, ByVal countryID As String, ByVal myConnection As MySqlConnection) As String
            Dim refID As String = ""
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spGenerateCode"
            myCommand.Parameters.AddWithValue("?vType", type)
            myCommand.Parameters.AddWithValue("?vCID", countryID)
            myCommand.Parameters.Add(New MySqlParameter("@vRefID", MySqlDbType.String))
            myCommand.Parameters("@vRefID").Direction = Data.ParameterDirection.Output
            myCommand.ExecuteNonQuery()
            refID = myCommand.Parameters("@vRefID").Value
            Return refID
        End Function

        Public Shared Function spGenerateCode2(ByVal type As String, ByVal countryID As String, ByVal id As Long, ByRef refID As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spGenerateCode2"
            myCommand.Parameters.AddWithValue("?vType", type)
            myCommand.Parameters.AddWithValue("?vCID", countryID)
            myCommand.Parameters.AddWithValue("?vID", id)
            myCommand.Parameters.Add(New MySqlParameter("@vRefID", MySqlDbType.String))
            myCommand.Parameters("@vRefID").Direction = Data.ParameterDirection.Output
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            refID = myCommand.Parameters("@vRefID").Value
            Return result
        End Function

        Public Shared Sub spPlacementFarLeftRight(ByVal MID As Long, ByVal Ac As Long, ByVal Group As Integer, ByRef PMID As Long, ByRef PAc As Integer, ByVal myConnection As MySqlConnection)
            Dim refID As String = ""
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spPlacementFarLeftRight"
            myCommand.Parameters.AddWithValue("?tPMID", MID)
            myCommand.Parameters.AddWithValue("?tPAc", Ac)
            myCommand.Parameters.AddWithValue("?tGroup", Group)
            myCommand.Parameters.Add(New MySqlParameter("@rPMID", MySqlDbType.Int64))
            myCommand.Parameters.Add(New MySqlParameter("@rPAc", MySqlDbType.Int32))
            myCommand.Parameters("@rPMID").Direction = Data.ParameterDirection.Output
            myCommand.Parameters("@rPAc").Direction = Data.ParameterDirection.Output
            myCommand.ExecuteNonQuery()
            PMID = myCommand.Parameters("@rPMID").Value
            PAc = myCommand.Parameters("@rPAc").Value
        End Sub

        Public Shared Function spValidateSponsor(ByVal tSID As Long, ByVal myConnection As MySqlConnection) As Integer
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spValidateSponsor"
            myCommand.Parameters.AddWithValue("?tSID", tSID)
            myCommand.Parameters.AddWithValue("?tResult", MySqlDbType.Int32)
            myCommand.Parameters("?tResult").Direction = Data.ParameterDirection.Output
            myCommand.ExecuteNonQuery()

            Dim return_Val As Integer = myCommand.Parameters("?tResult").Value

            Return return_Val
        End Function

        Public Shared Function spInsert_Binary(ByVal memberID As Long, ByVal ac As Integer, ByVal transID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spInsert_Binary"
            myCommand.Parameters.AddWithValue("?tMID", memberID)
            myCommand.Parameters.AddWithValue("?tAc", ac)
            myCommand.Parameters.AddWithValue("?tTID", transID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spBinary_P(ByVal memberID As Long, ByVal tAc As Integer, ByVal PV As Decimal, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spBinary_P"
            myCommand.Parameters.AddWithValue("?tMID", memberID)
            myCommand.Parameters.AddWithValue("?tAc", tAc)
            myCommand.Parameters.AddWithValue("?tPV", PV)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spBinary(ByVal memberID As Long, ByVal tAc As Integer, ByVal PV As Decimal, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = CommandType.StoredProcedure
            myCommand.CommandText = "spBinary"
            myCommand.Parameters.AddWithValue("?tMID", memberID)
            myCommand.Parameters.AddWithValue("?tAc", tAc)
            myCommand.Parameters.AddWithValue("?tPV", PV)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spIntroducer(ByVal memberID As Long, ByVal sponsorID As Long, ByVal transactionID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spIntroducer"
            myCommand.Parameters.AddWithValue("?tMID", memberID)
            myCommand.Parameters.AddWithValue("?sMID", sponsorID)
            myCommand.Parameters.AddWithValue("?tTID", transactionID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spMasterDealer(ByVal stockistID As Long, ByVal purchaseID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spMasterDealer"
            myCommand.Parameters.AddWithValue("?tSID", stockistID)
            myCommand.Parameters.AddWithValue("?tPID", purchaseID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spUpdate_Sales(ByVal memberID As Long, ByVal transactionID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spUpdate_Sales"
            myCommand.Parameters.AddWithValue("?vMID", memberID)
            myCommand.Parameters.AddWithValue("?vTID", transactionID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spAutoPlacement(ByVal memberID As Long, ByVal transactionID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spAutoPlacement"
            myCommand.Parameters.AddWithValue("?vMID", memberID)
            myCommand.Parameters.AddWithValue("?vTID", transactionID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spUpdatePlacement(ByVal placementID As Long, ByVal placementAC As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spUpdatePlacement"
            myCommand.Parameters.AddWithValue("?pMID", placementID)
            myCommand.Parameters.AddWithValue("?pAc", placementAC)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

        Public Shared Function spUpdateSponsorGroup(ByVal sponsorID As Long, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As New MySqlCommand()
            myCommand.Connection = myConnection
            myCommand.CommandType = Data.CommandType.StoredProcedure
            myCommand.CommandText = "spUpdateSponsorGroup"
            myCommand.Parameters.AddWithValue("?pMID", sponsorID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

    End Class
End Namespace
