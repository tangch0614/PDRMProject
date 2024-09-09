Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports AppCode.DataAccess
Imports System.Web
Imports System.Drawing
Imports System.IO
Imports System.Net.Mail
Imports System.Net
Imports System.Text.RegularExpressions
Imports AppCode.BusinessObject
Imports System.Drawing.Imaging

Namespace BusinessLogic
    Public Class UtilityManager

        Public Shared Function GetDeptColor(ByVal deptid As String) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As String = UtilityDB.GetDeptColor(deptid, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function Base64Encode(input As String) As String
            Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
        End Function

        Public Shared Function GenerateSHA1String(ByVal inputString As String) As String
            Dim sha1 As SHA1 = SHA1Managed.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
            Dim hash As Byte() = sha1.ComputeHash(bytes)
            Dim stringBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                stringBuilder.Append(hash(i).ToString("X2"))
            Next
            Return stringBuilder.ToString().ToLower
        End Function

        Public Shared Function GenerateSHA256String(ByVal inputString As String) As String
            Dim sha256 As SHA256 = SHA256Managed.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Dim stringBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                stringBuilder.Append(hash(i).ToString("X2"))
            Next
            Return stringBuilder.ToString().ToLower
        End Function

        Public Shared Function MD5Encrypt(ByVal inputString As String) As String
            Dim md5Hash As MD5 = MD5.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(inputString)
            Dim hash As Byte() = md5Hash.ComputeHash(bytes)
            Dim stringBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                stringBuilder.Append(hash(i).ToString("X2"))
            Next
            Return stringBuilder.ToString().ToLower
        End Function

        'Public Shared Function MD5Encrypt(ByVal STR As String) As String
        '    Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
        '        Return UtilityDB.MD5Encrypt(STR, myConnection)
        '    End Using
        'End Function

        Public Shared Function spGenerateCode2(ByVal type As String, ByVal countryID As String, ByVal id As Long, ByRef refID As String) As Boolean
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim result As Boolean = StoredProcDB.spGenerateCode2(type, countryID, id, refID, myConnection)
                myConnection.Close()
                Return result
            End Using
        End Function

        Public Shared Function Rounding(ByVal amount As Decimal) As Decimal
            Dim decimalvalue As Decimal = (amount - (Math.Floor(amount * 10) / 10))
            If decimalvalue >= 0.01 AndAlso decimalvalue < 0.03 Then
                Return 0 - decimalvalue
            ElseIf decimalvalue >= 0.03 AndAlso decimalvalue < 0.08 Then
                Return 0.05 - decimalvalue
            ElseIf decimalvalue >= 0.08 AndAlso decimalvalue < 0.1 Then
                Return 0.1 - decimalvalue
            Else
                Return 0
            End If
        End Function

        Public Shared Sub SaveLog(ByVal memberID As Long, ByVal adminID As Long, ByVal type As String, ByVal description As String, ByVal reference As String)
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                UtilityDB.SaveLog(memberID, adminID, type, description, reference, myConnection)
                myConnection.Close()
            End Using
        End Sub

        Public Shared Function TableChanged(ByVal DataTable1 As DataTable, ByVal DataTable2 As DataTable) As Boolean
            DataTable1.Merge(DataTable2)
            Dim dtChanges As DataTable = DataTable2.GetChanges
            If Not dtChanges Is Nothing AndAlso dtChanges.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function SecureString(ByVal str As String) As String
            Dim strArr As Char() = str.ToCharArray
            Dim halfLength As Integer = str.Length / 2
            Dim newStr As String = ""
            For i As Integer = 0 To strArr.Length - 1
                Dim newchar As Char = strArr(i)
                If i >= halfLength Then
                    newchar = "*"
                End If
                newStr &= newchar
            Next
            Return newStr
        End Function

        Public Shared Function ValidateString(ByVal Value As String) As Boolean
            Dim result As Boolean = True
            Dim match As Match = Regex.Match(Value, "[^a-zA-Z0-9-_]", RegexOptions.IgnoreCase)
            'Dim match As Match = Regex.Match(Value, "[^\w]+", RegexOptions.IgnoreCase)
            Do While match.Success
                result = False
                Exit Do
            Loop
            Return result
        End Function

        Public Shared Function GeneratePassword(ByVal Length As Integer) As String
            Dim random As New Random()
            'Dim characters As String = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz"
            Dim characters As String = "123456789abcdefghijklmnpqrstuvwxyz"
            Dim password As New StringBuilder(Length)
            For i As Integer = 0 To Length - 1
                password.Append(characters(random.[Next](characters.Length)))
            Next
            Return password.ToString()
        End Function

        Public Shared Sub SendEmail(ByVal ReceiverEmail As String, ByVal Subject As String, ByVal Body As String)
            Dim client As New SmtpClient("smtp.gmail.com")
            Dim mail As New MailMessage()
            Dim sender As String = "hct0614@gmail.com"
            Dim password As String = "12wab08176"
            client.Port = 587
            client.Credentials = New NetworkCredential(sender, password)
            client.EnableSsl = True
            mail.IsBodyHtml = True
            'client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            mail.From = New MailAddress(sender)
            mail.To.Add(ReceiverEmail)
            mail.Subject = Subject
            mail.Body = Body
            client.Send(mail)
        End Sub

        Public Shared Function GeneratePassword(ByVal Text As String, ByVal StartIndex As Integer, ByVal Length As Integer) As String
            Dim newPassword As String = Text.Substring(StartIndex, IIf(Text.Length < Length, Text.Length, Length))
            Return newPassword
        End Function

        Public Shared Function GetMemberType() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = UtilityDB.GetMemberType(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function GetMemberType(ByVal ID As Long) As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim memberType As String = UtilityDB.GetMemberType(ID, myConnection)
                myConnection.Close()
                Return memberType
            End Using
        End Function

        Public Shared Function GetTransactionType() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = UtilityDB.GetTransactionType(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Sub AppendCookie(ByVal CookieName As String, ByVal AppendValue As String, ByVal Value As String)
            Dim myCookie As HttpCookie = HttpContext.Current.Request.Cookies(CookieName)
            myCookie.Values(AppendValue) = Value
            HttpContext.Current.Response.AppendCookie(myCookie)
        End Sub

        Public Shared Function FilterTable(ByVal Query As String, ByVal myDataTable As DataTable) As DataTable
            Dim myDataRow As DataRow() = myDataTable.Select(Query)
            Dim filteredDataTable As DataTable = myDataTable.Clone()
            If myDataRow.Count > 0 Then
                For i As Integer = 0 To myDataRow.Count - 1
                    filteredDataTable.ImportRow(myDataRow(i))
                Next
            End If
            Return filteredDataTable
        End Function

        Public Shared Function RandomSorting(Of T)(ByVal myArray As T()) As T()
            Dim rnd As New Random
            For i As Integer = myArray.Count - 1 To 0 Step -1
                Dim swapPos As Integer = rnd.Next(i + 1)
                Dim temp As T = myArray(i)
                myArray(i) = myArray(swapPos)
                myArray(swapPos) = temp
            Next
            Return myArray
        End Function

        Public Shared Function ResizeImage(ByVal OriImage As System.Drawing.Image, ByVal Width As Integer, ByVal Height As Integer) As Bitmap
            Dim ResizedImage As New Bitmap(Width, Height, Imaging.PixelFormat.Format16bppRgb555)
            ResizedImage.MakeTransparent()
            Using graphic As Graphics = Graphics.FromImage(ResizedImage)
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                graphic.DrawImage(OriImage, 0, 0, Width, Height)
                OriImage.Dispose()
            End Using
            Return ResizedImage
        End Function

        Public Shared Function ResizeImage_FixedImgSize(ByVal OriImage As System.Drawing.Image, ByVal Width As Integer, ByVal Height As Integer) As Bitmap
            Dim sourceWidth As Integer = OriImage.Width
            Dim sourceHeight As Integer = OriImage.Height
            Dim sourceX As Integer = 0
            Dim sourceY As Integer = 0
            Dim destX As Integer = 0
            Dim destY As Integer = 0

            Dim nPercent As Decimal = 0
            Dim nPercentW As Decimal = Width / sourceWidth
            Dim nPercentH As Decimal = Height / sourceHeight
            If (nPercentH < nPercentW) Then
                nPercent = nPercentH
                destX = CInt((Width - (sourceWidth * nPercent)) / 2)
            Else
                nPercent = nPercentW
                destY = CInt((Height - (sourceHeight * nPercent)) / 2)
            End If

            Dim destWidth As Integer = sourceWidth * nPercent
            Dim destHeight As Integer = sourceHeight * nPercent

            Dim ResizedImage As Bitmap = New Bitmap(Width, Height, PixelFormat.Format16bppRgb555)
            ResizedImage.MakeTransparent()
            ResizedImage.SetResolution(OriImage.HorizontalResolution, OriImage.VerticalResolution)

            Using graphic As Graphics = Graphics.FromImage(ResizedImage)
                graphic.Clear(Color.Transparent)
                graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality
                graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality
                graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality
                graphic.DrawImage(OriImage, New Rectangle(destX, destY, destWidth, destHeight), New Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel)
                OriImage.Dispose()
            End Using
            Return ResizedImage
        End Function

        Public Shared Function AspectRatioSize(ByVal imageSize As System.Drawing.Size, ByVal aspectRatio As System.Drawing.Size) As System.Drawing.Size
            Dim newSize As New System.Drawing.Size
            If aspectRatio.Width > aspectRatio.Height Then
                If imageSize.Height < aspectRatio.Height Then
                    newSize.Width = aspectRatio.Width * (imageSize.Height / aspectRatio.Height)
                    newSize.Height = imageSize.Height
                ElseIf imageSize.Height > aspectRatio.Height Then
                    newSize.Width = aspectRatio.Width * (aspectRatio.Height / imageSize.Height)
                    newSize.Height = aspectRatio.Height
                Else
                    newSize.Width = aspectRatio.Width
                    newSize.Height = aspectRatio.Height
                End If
            ElseIf aspectRatio.Width < aspectRatio.Height Then
                If imageSize.Width < aspectRatio.Width Then
                    newSize.Height = aspectRatio.Height * (imageSize.Width / aspectRatio.Width)
                    newSize.Width = imageSize.Width
                ElseIf imageSize.Width > aspectRatio.Width Then
                    newSize.Height = aspectRatio.Height * (aspectRatio.Width / imageSize.Width)
                    newSize.Width = aspectRatio.Width
                Else
                    newSize.Height = aspectRatio.Height
                    newSize.Width = aspectRatio.Width
                End If
            Else
                If imageSize.Width > aspectRatio.Width Then
                    newSize.Width = aspectRatio.Width
                    newSize.Height = aspectRatio.Height
                Else
                    newSize.Width = imageSize.Width
                    newSize.Height = imageSize.Height
                End If
            End If
            Return newSize
        End Function

        Public Shared Function GetEncoderParameters(ByVal compressionLevel As Long) As EncoderParameters
            Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality
            Dim myEncoderParameters As New EncoderParameters(1)
            Dim myEncoderParameter As New EncoderParameter(myEncoder, compressionLevel)
            myEncoderParameters.Param(0) = myEncoderParameter
            Return myEncoderParameters
        End Function

        Public Shared Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo
            Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()
            Dim codec As ImageCodecInfo
            For Each codec In codecs
                If codec.FormatID = format.Guid Then
                    Return codec
                End If
            Next codec
            Return Nothing
        End Function

        Public Shared Function EscapeChars(valueWithoutWildcards As String) As String
            Dim sb As New StringBuilder()
            For i As Integer = 0 To valueWithoutWildcards.Length - 1
                Dim c As Char = valueWithoutWildcards(i)
                If c = "*"c OrElse c = "%"c OrElse c = "["c OrElse c = "]"c Then
                    sb.Append("[").Append(c).Append("]")
                ElseIf c = "'"c Then
                    sb.Append("''")
                Else
                    sb.Append(c)
                End If
            Next
            Return sb.ToString()
        End Function

        Public Shared Function GetCountryList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = UtilityDB.GetCountryList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function

        Public Shared Function ValidateAmount(ByVal Amount As Decimal, ByVal Min As Decimal, ByVal Max As Decimal) As Boolean
            If Not String.IsNullOrEmpty(Amount) Then
                If Max > 0 Then
                    If Not Amount < Min AndAlso Not Amount > Max Then
                        Return True
                    End If
                Else
                    If Not Amount < Min Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Function

        Public Shared Function GetPublicIpAddress() As String
            Dim publicIP As String
            publicIP = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")

            If Not publicIP Is Nothing Then
                If publicIP.Split(",").Length > 1 Then
                    publicIP = publicIP.Split(",").Last()
                End If
                Return publicIP
            Else
                Return HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
            End If
        End Function

        Public Shared Function EncryptData(ByVal Data As String) As String
            Dim encoder As New UTF8Encoding()
            Dim md5 As New MD5CryptoServiceProvider()
            Dim hashPassword As Byte() = md5.ComputeHash(encoder.GetBytes(Data))
            Dim sBuilder As New StringBuilder()
            For Each character In hashPassword
                sBuilder.Append(character.ToString("x2"))
            Next
            Return sBuilder.ToString()
        End Function

        Public Shared Function GetServerDateTime() As DateTime
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim serverDateTime As DateTime = UtilityDB.GetServerDateTime(myConnection)
                myConnection.Close()
                Return serverDateTime
            End Using
        End Function

        Public Shared Function GetServerDayOfWeek() As Integer
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim dayOfWeek As Integer = UtilityDB.GetServerDayOfWeek(myConnection)
                myConnection.Close()
                Return dayOfWeek
            End Using
        End Function

        Public Shared Function GetServerDayNameOfWeek() As String
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim dayOfWeek As String = UtilityDB.GetServerDayNameOfWeek(myConnection)
                myConnection.Close()
                Return dayOfWeek
            End Using
        End Function

        Public Shared Function GenerateRandomNumber(ByVal length As Integer) As String
            Dim format As String = ""
            Dim max As String = ""
            For i As Integer = 1 To length
                format &= "0"
                max &= "9"
            Next
            Dim generator As System.Random = New System.Random()
            Return generator.Next(0, Int32.Parse(max)).ToString(format)
        End Function

        Public Shared Function GenerateID(ByVal prefix As String, datetime As DateTime) As String
            Dim splitDateTime As String() = datetime.ToString("dd:M:yy:HH:mm:ss").Split(":")
            Dim month As Char = Nothing
            Dim day As String = String.Format("{0:00}", splitDateTime(0))
            Dim year As String = splitDateTime(2)
            Dim hour As String = String.Format("{0:00}", splitDateTime(3))
            Dim minute As String = String.Format("{0:00}", splitDateTime(4))
            Dim second As String = splitDateTime(5)
            Select Case (splitDateTime(1))
                Case 1
                    month = "J"
                Case 2
                    month = "F"
                Case 3
                    month = "M"
                Case 4
                    month = "A"
                Case 5
                    month = "Y"
                Case 6
                    month = "N"
                Case 7
                    month = "L"
                Case 8
                    month = "G"
                Case 9
                    month = "S"
                Case 10
                    month = "O"
                Case 11
                    month = "V"
                Case 12
                    month = "D"
            End Select
            Return prefix & month & day & year & hour & minute & GenerateRandomNumber(3)
        End Function

        Public Shared Function GetYear() As ArrayList
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim thisYear As DateTime = UtilityDB.GetServerDateTime(myConnection)
                myConnection.Close()
                Dim year As ArrayList = New ArrayList()
                For i As Integer = thisYear.Year - 100 To thisYear.Year
                    year.Add(i)
                Next
                Return year
            End Using
        End Function

        Public Shared Function ValidateFile(ByVal FileExtension As String, ByVal AllowExtension As String()) As Boolean
            Dim result As Boolean = False
            For i As Integer = 0 To AllowExtension.Length - 1
                If FileExtension.ToLower().Equals(AllowExtension(i).ToLower()) Then
                    Return True
                End If
            Next
            Return False
        End Function

        Public Shared Function ValidateDate(ByVal dateForm As String, ByVal dateTo As String) As Boolean
            If (dateForm IsNot Nothing And dateTo IsNot Nothing) Then
                If Not Date.Parse(dateForm) <= Date.Parse(dateTo) Then
                    Return False
                End If
            End If
            Return True
        End Function

        Public Shared Function RandomSorting(ByVal myDataTable As DataTable) As DataTable
            myDataTable.Columns.Add(New DataColumn("Sequence", Type.GetType("System.Int32")))
            Dim randomNum As Random = New Random()
            For i As Integer = 0 To myDataTable.Rows.Count - 1
                myDataTable.Rows(i)("Sequence") = randomNum.Next(100)
            Next
            myDataTable.DefaultView.Sort = "Sequence"
            Return myDataTable.DefaultView.ToTable
        End Function

        Public Shared Function DataSorting(ByVal myDataTable As DataTable, ByVal SortBy As String, ByVal Desc As Boolean) As DataTable
            If Not String.IsNullOrWhiteSpace(SortBy) Then
                Dim sort As String = Nothing
                If Desc = True Then
                    sort = " desc"
                End If
                myDataTable.DefaultView.Sort = SortBy & sort
            End If
            Return myDataTable.DefaultView.ToTable
        End Function

        Public Shared Function ValidateNumberRange(ByVal MinNum As Long, ByVal MaxNum As Long) As Boolean
            If Not Convert.ToInt64(MinNum) <= Convert.ToInt64(MaxNum) Then
                Return False
            End If
            Return True
        End Function

        Public Shared Sub SetRowAttribute(ByVal myGridView As GridView, ByVal ClientScript As ClientScriptManager, ByVal EvenRowColor As String, ByVal OddRowColor As String, ByVal OnHoverColor As String, ByVal Selectable As Boolean)
            For Each row As GridViewRow In myGridView.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    row.Attributes("onmouseover") = "this.style.cursor='pointer';this.style.background='" & OnHoverColor & "';"
                    If row.RowIndex Mod 2 = 0 Then
                        row.Attributes("onmouseout") = "this.style.background='" & EvenRowColor & "';"
                    ElseIf row.RowIndex Mod 2 <> 0 Then
                        row.Attributes("onmouseout") = "this.style.background='" & OddRowColor & "';"
                    End If
                    If Selectable = True Then
                        row.Attributes("onclick") = ClientScript.GetPostBackClientHyperlink(myGridView, "Select$" & row.DataItemIndex, True)
                    End If
                End If
            Next
        End Sub

        Public Shared Function ConvertToSeconds(ByVal minutes As Integer, ByVal seconds As Integer) As Integer
            Dim limitTime As Integer = 0
            limitTime = (minutes * 60) + seconds
            Return limitTime
        End Function

        Public Shared Function ConvertToMinutes(ByVal seconds As Integer) As String
            Dim timespan As TimeSpan = timespan.FromSeconds(seconds)
            Dim minutes As Integer = (timespan.Hours * 60) + timespan.Minutes
            Dim limitTime As String = minutes.ToString & ":" & timespan.Seconds.ToString
            Return limitTime
        End Function

        Public Shared Function HasChanges(ByVal ValidateValue As String(), ByVal CompareValue As String()) As Boolean
            Dim result As Boolean = False
            For i As Integer = 0 To ValidateValue.Length - 1
                If Not ValidateValue(i).Equals(CompareValue(i)) Then
                    result = True
                    Exit For
                End If
            Next
            Return result
        End Function

        Public Shared Function GetLanguageList() As DataTable
            Using myConnection As MySqlConnection = New MySqlConnection(AppConfiguration.ConnectionString)
                myConnection.Open()
                Dim myDataTable As DataTable = UtilityDB.GetLanguageList(myConnection)
                myConnection.Close()
                Return myDataTable
            End Using
        End Function
    End Class
End Namespace
