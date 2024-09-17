Imports System.Data
Imports MySql.Data.MySqlClient
Imports AppCode.BusinessObject


NameSpace DataAccess
    Public Class CountryDB

#Region "Public Methods"

        Public Shared Function UpdateMukimGeofence(ByVal mukim As String, ByVal geofence As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblcountrymukim set fldGeofence=@geofence Where fldMukim=@mukim", myConnection)
            myCommand.Parameters.AddWithValue("@mukim", mukim)
            myCommand.Parameters.AddWithValue("@geofence", geofence)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateDistrictGeofence(ByVal district As String, ByVal geofence As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblcountrydistrict set fldGeofence=@geofence Where fldDistrict=@district", myConnection)
            myCommand.Parameters.AddWithValue("@district", district)
            myCommand.Parameters.AddWithValue("@geofence", geofence)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function UpdateStateGeofence(ByVal state As String, ByVal geofence As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim myCommand As MySqlCommand = New MySqlCommand("Update tblcountrystate set fldGeofence=@geofence Where fldState=@state", myConnection)
            myCommand.Parameters.AddWithValue("@state", state)
            myCommand.Parameters.AddWithValue("@geofence", geofence)
            Dim result As Boolean = myCommand.ExecuteNonQuery() > 0
            Return result
        End Function

        Public Shared Function GetState(ByVal state As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrystate Where fldState = @state", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@state", state)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetDistrict(ByVal district As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrydistrict Where fldDistrict = @district", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@district", district)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMukim(ByVal mukim As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrymukim Where fldMukim = @mukim", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@mukim", mukim)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCurrency(ByVal countryID As String, ByVal myConnection As MySqlConnection) As String
            Dim currency As String = ""
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldCurrency From tblcountry where fldID = @fldID", myConnection)
            myCommand.Parameters.AddWithValue("@fldID", countryID)
            currency = myCommand.ExecuteScalar()
            Return currency
        End Function

        Public Shared Function GetCurrencyList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select Distinct fldCurrency From tblcountry Where fldCurrency != '' And fldStatus = 'Y' Order By fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCountryZoneList(ByVal CountryID As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountryzone Where fldCountryID = @fldCountryID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCountryID", CountryID)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCountryStateList(ByVal CountryID As String, ByVal CountryZone As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrystate Where fldCountryID = @fldCountryID  And fldCountryZone = @fldCountryZone Order By fldState", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCountryID", CountryID)
            myCommand.Parameters.AddWithValue("@fldCountryZone", CountryZone)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCountryStateList(ByVal CountryID As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrystate Where fldCountryID = @fldCountryID Order By fldState", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldCountryID", CountryID)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetDistrictList(ByVal State As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not String.IsNullOrEmpty(State) Then query &= " fldState = @State AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrydistrict " & query & " Order By fldState,fldDistrict", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@State", State)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetMukimList(ByVal State As String, ByVal district As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim query As String = ""
            If Not String.IsNullOrEmpty(State) Then query &= " fldState = @State AND "
            If Not String.IsNullOrEmpty(district) Then query &= " fldDistrict = @district AND "
            If Not String.IsNullOrEmpty(query) Then
                query = " Where " & query
                query = query.Substring(0, query.Length - 4)
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountrymukim " & query & " Order By fldState,fldDistrict,fldMukim", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@State", State)
            myCommand.Parameters.AddWithValue("@district", district)
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCountry(ByVal fldID As String, ByVal myConnection As MySqlConnection) As CountryObj
            Dim myCountry As CountryObj = Nothing
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountry Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)

            Using myReader As MySqlDataReader = myCommand.ExecuteReader()
                If myReader.Read() Then
                    myCountry = FillDataRecord(myReader)
                End If
                myReader.Close()
            End Using
            Return myCountry
        End Function

        Public Shared Function GetCountryList(ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select * From tblcountry Order By fldHQ = 'Y' Desc, fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function GetCountryList(ByVal LanguageField As String, ByVal myConnection As MySqlConnection) As DataTable
            Dim myDataTable As DataTable = New DataTable()
            Dim myCommand As MySqlCommand = New MySqlCommand("Select fldID, " & LanguageField & " As fldName, fldStatus, fldHQ From tblcountry Order By fldHQ = 'Y' Desc, fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter(myCommand)
            adapter.Fill(myDataTable)
            Return myDataTable
        End Function

        Public Shared Function Save(ByVal myCountry As CountryObj, ByVal myConnection As MySqlConnection) As Integer
            Dim result As Long = 0
            Dim processExe As String = ""
            Dim processReturn As String = "Select LAST_INSERT_ID()"
            Dim isInsert As Boolean = True
            If myCountry.fldID = Nothing Then
                processExe = "Insert into tblcountry (fldName, fldStatus, fldHQ) Values (@fldName, @fldStatus, @fldHQ)"
                isInsert = True
            Else
                processExe = "Update tblcountry set fldName = @fldName, fldStatus = @fldStatus, fldHQ = @fldHQ Where fldID = @fldID"
                isInsert = False
            End If
            Dim myCommand As MySqlCommand = New MySqlCommand(processExe, myConnection)
            myCommand.CommandType = CommandType.Text
            If Not isInsert Then myCommand.Parameters.AddWithValue("@fldID", myCountry.fldID)

            myCommand.Parameters.AddWithValue("@fldName", myCountry.fldName)
            myCommand.Parameters.AddWithValue("@fldStatus", myCountry.fldStatus)
            myCommand.Parameters.AddWithValue("@fldHQ", myCountry.fldHQ)

            result = myCommand.ExecuteNonQuery()
            If isInsert Then
                myCommand = New MySqlCommand(processReturn, myConnection)
                myCommand.CommandType = CommandType.Text
                result = myCommand.ExecuteScalar
            End If
            Return result
        End Function

        Public Shared Function Delete(ByVal fldID As String, ByVal myConnection As MySqlConnection) As Boolean
            Dim result As Long = 0
            Dim myCommand As MySqlCommand = New MySqlCommand("Delete From tblcountry Where fldID = @fldID", myConnection)
            myCommand.CommandType = CommandType.Text
            myCommand.Parameters.AddWithValue("@fldID", fldID)
            result = myCommand.ExecuteNonQuery()
            Return result > 0
        End Function

#End Region

        Private Shared Function FillDataRecord(ByVal myDataRecord As IDataRecord) As CountryObj
            Dim myCountry As CountryObj = New CountryObj()
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldID"))) Then
                myCountry.fldID = myDataRecord.GetString(myDataRecord.GetOrdinal("fldID"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldName"))) Then
                myCountry.fldName = myDataRecord.GetString(myDataRecord.GetOrdinal("fldName"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldStatus"))) Then
                myCountry.fldStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("fldStatus"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldHQ"))) Then
                myCountry.fldHQ = myDataRecord.GetString(myDataRecord.GetOrdinal("fldHQ"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldPhoneCode"))) Then
                myCountry.fldPhoneCode = myDataRecord.GetString(myDataRecord.GetOrdinal("fldPhoneCode"))
            End If
            If (Not myDataRecord.IsDBNull(myDataRecord.GetOrdinal("fldCurrency"))) Then
                myCountry.fldCurrency = myDataRecord.GetString(myDataRecord.GetOrdinal("fldCurrency"))
            End If
            Return myCountry
        End Function
    End Class

 End NameSpace 
