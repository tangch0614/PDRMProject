Imports AppCode.BusinessLogic
Imports System.IO
Imports NPOI.SS.UserModel
Imports NPOI.XSSF.UserModel
Imports NPOI.HSSF.UserModel
Imports System.Threading
Imports System.Globalization

Public Class Base
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        If Request.Cookies("LanguageCookie") Is Nothing Then 'check if languagecookie exists
            PreferredLanguage() 'initialize language if cookie not exists
        End If
        Dim language As String = Request.Cookies("LanguageCookie").Value
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(language)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "Language"

    Public Sub UserIsAuthenticated()
        Dim result As Boolean = Page.Request.IsAuthenticated AndAlso AdminAuthentication.ValidateSession(CLng(AdminAuthentication.GetUserData(2)), CStr(AdminAuthentication.GetUserData(0)))
        If Not result Then
            AdminAuthentication.Logout(CLng(AdminAuthentication.GetUserData(2)), CStr(AdminAuthentication.GetUserData(0)))
            Response.Redirect("~/secure/Login_a.aspx")
        End If
    End Sub

    'Public Function GetText(ByVal text As String) As String
    '    If Not Session("Language") Is Nothing Then
    '        Dim dtTexts As DataTable = CType(Session("Language"), DataTable)
    '        Return MultiLanguageManager.GetText(text, dtTexts)
    '    End If
    '    Return text
    'End Function

    'Public Function GetText(ByVal text As String, ByVal language As String) As String
    '    Return MultiLanguageManager.GetText(text, language)
    'End Function

    Private Sub PreferredLanguage()
        Dim language As String = Application("DefaultLanguage") 'set language as default
        If User.Identity.IsAuthenticated() AndAlso Not CStr(AdminAuthentication.GetUserData(4)) Is Nothing Then 'check if usercookie and userpreferlanguagecookie exists
            language = AdminAuthentication.GetUserData(4) 'set language to user preferred language if cookie exists
        End If
        GenerateLanguageCookie(language) 'set language cookie
    End Sub

    Private Sub GenerateLanguageCookie(ByVal Language As String)
        Dim languageCookie As HttpCookie = Response.Cookies("LanguageCookie")
        If languageCookie Is Nothing Then
            languageCookie = New HttpCookie("LanguageCookie")
            languageCookie.Expires = DateTime.Now.AddDays(1)
        End If
        languageCookie.Value = Language
    End Sub

    Public Function GetText(ByVal key As String) As String
        Dim resourseHelper As ResourceHelper = New ResourceHelper
        Return resourseHelper.GetText(key)
    End Function

    Public Function GetResource() As System.Resources.ResourceManager
        Dim resourseHelper As ResourceHelper = New ResourceHelper
        Return resourseHelper.GetResource()
    End Function

    Public Function GetCurrentLanguage() As String
        If Not Request.Cookies("LanguageCookie") Is Nothing Then
            Return Request.Cookies("LanguageCookie").Value
        Else
            If HttpContext.Current.User.IsInRole("M") Then
                Return MemberAuthentication.GetUserData(4)
            Else
                Return AdminAuthentication.GetUserData(4)
            End If
        End If
    End Function

    Public Function ValidatePage(Optional ByVal group As String = "") As Boolean
        If Not String.IsNullOrWhiteSpace(group) Then
            Page.Validate(group)
        Else
            Page.Validate()
        End If
        If Not Page.IsValid Then
            Dim message As String = GetText("ErrorPageInvalid")
            Dim validatorCollection As ValidatorCollection = Page.Validators
            For i As Integer = 0 To validatorCollection.Count - 1
                If Not validatorCollection.Item(i).IsValid Then
                    message &= "\n- " & validatorCollection.Item(i).ErrorMessage
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "javascript", "alert('" & message & "');", True)
        End If
        Return Page.IsValid
    End Function

    Public Function ValidateDecimal(ByVal value As String, ByVal allowZero As Boolean, ByVal errorMessage As Boolean, ByVal valuename As String, ByRef msg As String) As Boolean
        Dim dec As Decimal = Nothing
        If Not (Decimal.TryParse(value, dec) AndAlso If(allowZero, value >= 0, value > 0)) Then
            msg = GetText("ErrorInvalidInputValue").Replace("vITEM", GetText(valuename))
            If errorMessage Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('" & GetText("ErrorInvalidInputValue").Replace("vITEM", GetText(valuename)) & "')", True)
            Return False
        End If
        Return True
    End Function

    Public Function ValidateInteger(ByVal value As String, ByVal allowZero As Boolean, ByVal errorMessage As Boolean, ByVal valuename As String, ByRef msg As String) As Boolean
        Dim int As Integer = Nothing
        If Not (Decimal.TryParse(value, int) AndAlso If(allowZero, value >= 0, value > 0)) Then
            msg = GetText("ErrorInvalidInputValue").Replace("vITEM", GetText(valuename))
            If errorMessage Then ScriptManager.RegisterStartupScript(Me, Me.GetType, "alert", "alert('" & GetText("ErrorInvalidInputValue").Replace("vITEM", GetText(valuename)) & "')", True)
            Return False
        End If
        Return True
    End Function

#End Region

#Region "File Upload"

    Public Function UploadFile(ByVal fuFile As FileUpload, ByVal location As String, ByVal prefix As String, ByVal suffix As String, ByVal genfilename As Boolean, ByRef FilePath As String) As Boolean
        Try
            If fuFile.HasFiles Then
                Dim FilePathList As List(Of String) = New List(Of String)
                For Each postedfile As HttpPostedFile In fuFile.PostedFiles
                    If postedfile.ContentLength > 0 Then
                        Dim datetime As DateTime = UtilityManager.GetServerDateTime
                        Dim SavePath As String = ""
                        If genfilename Then
                            SavePath = ValidateFilePath(location, UtilityManager.EscapeFileName(prefix & datetime.ToString("yMd_Hms") & suffix), Path.GetExtension(postedfile.FileName).ToLower())
                        Else
                            SavePath = ValidateFilePath(location, UtilityManager.EscapeFileName(prefix & Path.GetFileNameWithoutExtension(postedfile.FileName) & suffix), Path.GetExtension(postedfile.FileName).ToLower())
                        End If
                        If File.Exists(Server.MapPath(SavePath)) Then
                            File.Delete(Server.MapPath(SavePath))
                        End If
                        postedfile.SaveAs(Server.MapPath(SavePath))
                        FilePathList.Add(SavePath)
                    End If
                Next
                FilePath = String.Join(",", FilePathList)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUploadFailed") & "');", True)
            Return False
        End Try
        Return True
    End Function

    Public Function ValidateFileType(ByVal fileUpload As FileUpload, ByVal supportExt As String(), ByVal maxSizeMB As Integer, ByVal allowempty As Boolean, ByVal errorMessage As Boolean, ByRef msg As String) As Boolean
        If fileUpload.HasFiles Then
            For i As Integer = 0 To fileUpload.PostedFiles.Count - 1
                Dim postedfile As HttpPostedFile = fileUpload.PostedFiles(i)
                Dim maxbyte As Long = maxSizeMB * 1024 * 1024
                If postedfile.ContentLength > maxbyte Then
                    msg = GetText("ErrorFileSizeLimit").Replace("vSIZE", maxSizeMB & "MB")
                    If errorMessage Then ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & msg & "');", True)
                    Return False
                ElseIf Not supportExt.Contains(IO.Path.GetExtension(postedfile.FileName.ToLower)) Then
                    msg = GetText("ErrorInvalidFileFormat").Replace("vSUPPORT", String.Join(", ", supportExt))
                    If errorMessage Then ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & msg & "');", True)
                    Return False
                End If
            Next
        Else
            If Not allowempty Then
                msg = GetText("ErrorNoFileSelected")
                If errorMessage Then ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & msg & "');", True)
                Return False
            End If
        End If
        Return True
    End Function

    Public Function ValidateFilePath(ByVal path As String, ByVal fileName As String, ByVal extension As String) As String
        Dim filePath As String = path & fileName & extension
        Dim counter As Integer = 0
        While File.Exists(Server.MapPath(filePath))
            counter += 1
            filePath = path & fileName & "_" & counter & extension
        End While
        Return filePath
    End Function

#End Region

#Region "Excel"
    Public Sub ExportToExcel_NPOI(dt As DataTable, fileName As String, extension As String)

        Dim workbook As IWorkbook

        If extension = "xlsx" Then
            workbook = New XSSFWorkbook()
        ElseIf extension = "xls" Then
            workbook = New HSSFWorkbook()
        Else
            Throw New Exception("This format is not supported")
        End If

        Dim sheet1 As ISheet = workbook.CreateSheet("Sheet 1")

        'make a header row
        Dim row1 As IRow = sheet1.CreateRow(0)

        For j As Integer = 0 To dt.Columns.Count - 1

            Dim cell As ICell = row1.CreateCell(j)
            Dim columnName As String = dt.Columns(j).ToString()
            cell.SetCellValue(columnName)
        Next

        'loops through data
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim row As IRow = sheet1.CreateRow(i + 1)
            'For j As Integer = 0 To dt.Columns.Count - 1

            '    Dim cell As ICell = row.CreateCell(j)
            '    Dim columnName As String = dt.Columns(j).ToString()
            '    cell.SetCellValue(dt.Rows(i)(columnName).ToString)
            'Next

            For Each column As DataColumn In dt.Columns
                Dim cell As ICell = row.CreateCell(column.Ordinal)
                Select Case column.DataType.FullName
                    Case "System.Int", "System.Int32", "System.Int64", "System.Double", "System.Decimal"
                        Dim value As Decimal = 0
                        Decimal.TryParse(dt.Rows(i)(column).ToString, value)
                        cell.SetCellValue(value)
                    Case Else
                        cell.SetCellValue(dt.Rows(i)(column).ToString)
                End Select

            Next
        Next

        Using exportData = New MemoryStream()
            Response.Clear()
            workbook.Write(exportData)
            If extension = "xlsx" Then
                'xlsx file format
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName & ".xlsx"))
                Response.BinaryWrite(exportData.ToArray())
            ElseIf extension = "xls" Then
                'xls file format
                Response.ContentType = "application/vnd.ms-excel"
                Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", fileName & ".xls"))
                Response.BinaryWrite(exportData.GetBuffer())
            End If
            Response.[End]()
        End Using
    End Sub

    Public Sub ExportToExcel_xlsx(ByVal datatable As DataTable, ByVal fieldList As String(,), ByVal fileName As String)
        Dim exportTable As DataTable = New DataTable()
        For i As Integer = 0 To fieldList.GetUpperBound(0)
            Dim datatype As System.Type = datatable.Columns(fieldList(i, 0)).DataType
            exportTable.Columns.Add(fieldList(i, 1), datatype)
        Next
        For j As Integer = 0 To datatable.Rows.Count - 1
            Dim rowData As List(Of String) = New List(Of String)
            For k As Integer = 0 To fieldList.GetUpperBound(0)
                rowData.Add(datatable.Rows(j)(fieldList(k, 0)))
            Next
            exportTable.Rows.Add(rowData.ToArray)
        Next
        ExportToExcel_NPOI(exportTable, fileName, "xlsx")
    End Sub

    Public Sub ExportToExcel_xls(ByVal datatable As DataTable, ByVal fieldList As String(,), ByVal fileName As String)
        Dim exportTable As DataTable = New DataTable()
        For i As Integer = 0 To fieldList.GetUpperBound(0)
            Dim datatype As System.Type = datatable.Columns(fieldList(i, 0)).DataType
            exportTable.Columns.Add(fieldList(i, 1), datatype)
        Next
        For j As Integer = 0 To datatable.Rows.Count - 1
            Dim rowData As List(Of String) = New List(Of String)
            For k As Integer = 0 To fieldList.GetUpperBound(0)
                rowData.Add(datatable.Rows(j)(fieldList(k, 0)))
            Next
            exportTable.Rows.Add(rowData.ToArray)
        Next
        ExportToExcel_NPOI(exportTable, fileName, "xls")
    End Sub

#End Region

End Class