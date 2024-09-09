Imports AppCode.BusinessLogic
Imports AppCode.BusinessObject
Imports System.IO

Public Class ASelectImage
    Inherits ABase

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetText()
        If Not IsPostBack Then
            Initialize()
            hfControlID.Value = Request("c")
        End If
    End Sub

#Region "Languange"
    Private Sub SetText()
        'Header/Title
        Page.Header.Title = "Select Image | " & Application("Company")
        lblPageTitle.Text = GetText("SelectItem").Replace("vITEM", GetText("Image"))
        lblHeader.Text = GetText("SelectItem").Replace("vITEM", GetText("Image"))
        'IC Img
        lblImg.Text = GetText("NewImage")

        'Buttons/Message
        btnImg.Text = GetText("SelectItem").Replace("vITEM", GetText("Image"))
        btnSubmit.Text = GetText("Upload")
        hfConfirm.Value = GetText("MsgConfirm")
        hfConfirmDelete.Value = GetText("MsgConfirm")
    End Sub

#End Region

#Region "Initialize"

    Private Sub Initialize()
        Dim files() As String = Directory.GetFiles(Server.MapPath("~/upload/img/"))
        Dim datatable As New DataTable
        datatable.Columns.Add("fileName", GetType(String))
        datatable.Columns.Add("filePath", GetType(String))
        For i As Integer = 0 To files.Length - 1
            'Dim str() As String = files(i).Split("/")
            datatable.Rows.Add(Path.GetFileName(files(i)), "../upload/img/" & Path.GetFileName(files(i)))
        Next
        datatable.AcceptChanges()
        rptTable.DataSource = datatable
        rptTable.DataBind()
    End Sub

#End Region

#Region "Table binding"

    Private Sub BindTable()
        Dim myDataTable As DataTable = Nothing
        If Not myDataTable Is Nothing AndAlso myDataTable.Rows.Count > 0 Then
            rptTable.DataSource = myDataTable
            rptTable.DataBind()
        Else
            rptTable.DataSource = ""
            rptTable.DataBind()
        End If
    End Sub

#End Region

#Region "Action"

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        UserIsAuthenticated()
        Dim returnmsg As String = ""
        If ValidateFileType(fuImg, {".png", ".jpeg", ".jpg", ".bmp", ".gif"}, 10, False, False, returnmsg) Then
            Dim ImgPath As String = ValidateFilePath("~/upload/img/", Path.GetFileNameWithoutExtension(fuImg.PostedFile.FileName), Path.GetExtension(fuImg.PostedFile.FileName).ToLower())
            Try
                fuImg.PostedFile.SaveAs(Server.MapPath(ImgPath))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgUploadSuccess") & "');", True)
                Initialize()
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorUploadFailed") & "');", True)
            End Try
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & returnmsg & "');", True)
            txtImg.Text = ""
        End If
    End Sub

    Protected Sub rptTable_ItemCommand(source As Object, e As RepeaterCommandEventArgs)
        If e.CommandName = "deleteImg" Then
            Try
                Dim filePath As String = Server.MapPath(e.CommandArgument)
                File.Delete(filePath)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("MsgDeleteSuccess") & "');", True)
                Initialize()
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Alert", "alert('" & GetText("ErrorDeleteFailed") & "');", True)
            End Try
        End If
    End Sub
#End Region

#Region "Reset"

    Protected Sub btnReset_Click(sender As Object, e As EventArgs)
        Initialize()
    End Sub

#End Region
End Class