Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports AppCode.BusinessObject

Namespace BusinessLogic
    Public Class DataPaging

        Public Shared Function bindDataWithPaging(ByVal DataControl As Control, ByVal DataTable As DataTable, ByVal Panel As Panel, ByVal Handler As EventHandler, ByVal PageObject As PagingObj) As PagingObj  'you can pass either DatList or Repeater to this function

            If (DataTable.Rows.Count > 0) Then ' if the datset contains data

                Dim pagedDataSource As PagedDataSource = New PagedDataSource()
                pagedDataSource.AllowPaging = True
                pagedDataSource.DataSource = DataTable.DefaultView
                pagedDataSource.CurrentPageIndex = PageObject.CurrentPageIndex
                pagedDataSource.PageSize = PageObject.PageSize

                'Binding data to the controls
                If (TypeOf (DataControl) Is DataList) Then '
                    CType(DataControl, DataList).DataSource = pagedDataSource
                    CType(DataControl, DataList).DataBind() '
                ElseIf (TypeOf (DataControl) Is Repeater) Then '
                    CType(DataControl, Repeater).DataSource = pagedDataSource
                    CType(DataControl, Repeater).DataBind() '
                ElseIf (TypeOf (DataControl) Is GridView) Then '
                    CType(DataControl, GridView).DataSource = pagedDataSource
                    CType(DataControl, GridView).DataBind() '
                Else '
                    CType(DataControl, Object).DataSource = pagedDataSource
                    CType(DataControl, Object).DataBind() '
                End If
                'saving the total page count in Viewstate for later use
                PageObject.PageCount = pagedDataSource.PageCount

                'create the linkbuttons for pagination
                BuildPagination(Panel, Handler, PageObject)

            End If
            Return PageObject
        End Function

        'Public Shared Function bindDataWithPaging(ByVal DataControl As Control, ByVal DataTable As DataTable, ByVal PageCount As Integer, ByVal Panel As Panel, ByVal Handler As EventHandler, ByVal PageObject As PagingObj) As PagingObj  'you can pass either DatList or Repeater to this function

        '    If (DataTable.Rows.Count > 0) Then ' if the datset contains data

        '        Dim pagedDataSource As PagedDataSource = New PagedDataSource()
        '        pagedDataSource.AllowPaging = True
        '        pagedDataSource.AllowCustomPaging = True
        '        pagedDataSource.DataSource = DataTable.DefaultView
        '        pagedDataSource.CurrentPageIndex = PageObject.CurrentPageIndex
        '        pagedDataSource.PageSize = PageObject.PageSize
        '        pagedDataSource.VirtualCount = PageCount

        '        'Binding data to the controls
        '        If (TypeOf (DataControl) Is DataList) Then '
        '            CType(DataControl, DataList).DataSource = pagedDataSource
        '            CType(DataControl, DataList).DataBind() '
        '        ElseIf (TypeOf (DataControl) Is Repeater) Then '
        '            CType(DataControl, Repeater).DataSource = pagedDataSource
        '            CType(DataControl, Repeater).DataBind() '
        '        ElseIf (TypeOf (DataControl) Is GridView) Then '
        '            CType(DataControl, GridView).DataSource = pagedDataSource
        '            CType(DataControl, GridView).DataBind() '
        '        Else '
        '            CType(DataControl, Object).DataSource = pagedDataSource
        '            CType(DataControl, Object).DataBind() '
        '        End If
        '        'saving the total page count in Viewstate for later use
        '        PageObject.PageCount = pagedDataSource.PageCount

        '        'create the linkbuttons for pagination
        '        BuildPagination(Panel, Handler, PageObject)

        '    End If
        '    Return PageObject
        'End Function

        'create the linkbuttons for pagination
        Public Shared Sub BuildPagination(ByVal Panel As Panel, ByVal Handler As EventHandler, ByVal PageObject As PagingObj)
            Dim ButtonsCount As Integer = PageObject.ButtonCount
            Dim PageCount As Integer = PageObject.PageCount
            Dim CurrentPage As Integer = PageObject.CurrentPageIndex
            Dim IdPrefix As String = PageObject.ButtonIDPrefix
            Panel.Controls.Clear()
            If (PageCount <= 1) Then Return 'at least two pages should be there to show the pagination
            'finding the first linkbutton to be shown in the current display
            Dim startpage As Integer = CurrentPage - (CurrentPage Mod ButtonsCount)
            'finding the last linkbutton to be shown in the current display
            Dim endpage As Integer = CurrentPage + (ButtonsCount - (CurrentPage Mod ButtonsCount))
            'if the start button is more than the number of buttons. If the start button is 11 we have to show the <<First link
            If (startpage > ButtonsCount - 1) Then
                Panel.Controls.Add(createButton(Handler, "<<", 0, IdPrefix, True))
                Panel.Controls.Add(createButton(Handler, "<", startpage - 1, IdPrefix, True))
            End If
            Dim i As Integer = 0
            Dim j As Integer = 0
            For i = startpage To endpage - 1
                If (i < PageCount) Then
                    If (i = CurrentPage) Then 'if its the current page
                        Panel.Controls.Add(createButton(Handler, (i + 1).ToString(), i, IdPrefix, False))
                    Else
                        Panel.Controls.Add(createButton(Handler, (i + 1).ToString(), i, IdPrefix, True))
                    End If
                Else
                    Exit For
                End If
                j += 1
            Next
            'If the total number of pages are greaer than the end page we have to show Last>> link
            If (PageCount > endpage) Then
                If PageCount - 1 <> i Then
                    Panel.Controls.Add(createButton(Handler, ">", i, IdPrefix, True))
                End If
                Panel.Controls.Add(createButton(Handler, ">>", PageCount - 1, IdPrefix, True))
            End If
        End Sub

        Public Shared Function createButton(ByVal Handler As EventHandler, ByVal Text As String, ByVal Index As Integer, ByVal IdPrefix As String, ByVal Enabled As Boolean) As Button
            Dim btn As Button = New Button()
            If Text.Equals(">") Then
                btn.ID = IdPrefix & "next"
            ElseIf Text.Equals(">>") Then
                btn.ID = IdPrefix & "last"
            ElseIf Text.Equals("<") Then
                btn.ID = IdPrefix & "previous"
            ElseIf Text.Equals("<<") Then
                btn.ID = IdPrefix & "first"
            Else
                btn.ID = IdPrefix & Index.ToString()
            End If
            btn.Text = Text
            btn.CommandArgument = Index.ToString()
            btn.CssClass = "buttonPager"
            btn.Enabled = Enabled
            AddHandler btn.Click, Handler
            Return btn
        End Function

        Public Shared Function PopulatePager(ByVal PageObject As PagingObj, ByVal PagerControl As Repeater) As PagingObj
            Dim ButtonsCount As Integer = PageObject.ButtonCount
            Dim PageSize As Integer = PageObject.PageSize
            Dim CurrentPage As Integer = PageObject.CurrentPageIndex
            Dim RecordCount As Integer = PageObject.RecordCount
            Dim PageCount As Integer = CInt(Math.Ceiling(RecordCount / PageSize))
            Dim pages As New List(Of ListItem)()
            Dim startIndex As Integer, endIndex As Integer
            PageObject.PageCount = PageCount
            'Calculate the Start and End Index of pages to be displayed.
            startIndex = If(CurrentPage > 1 AndAlso CurrentPage + ButtonsCount - 1 < ButtonsCount, CurrentPage, 1)
            endIndex = If(PageCount > ButtonsCount, ButtonsCount, PageCount)
            If CurrentPage > ButtonsCount Mod 2 Then
                If CurrentPage <= CInt(Math.Ceiling(ButtonsCount / 2)) Then
                    endIndex = ButtonsCount
                Else
                    endIndex = CurrentPage + CInt(Math.Ceiling(ButtonsCount / 2))
                End If
            Else
                endIndex = (ButtonsCount - CurrentPage) + 1
            End If

            If endIndex - (ButtonsCount - 1) > startIndex Then
                startIndex = endIndex - (ButtonsCount - 1)
            End If

            If endIndex > PageCount Then
                endIndex = PageCount
                startIndex = If(((endIndex - ButtonsCount) + 1) > 0, (endIndex - ButtonsCount) + 1, 1)
            End If

            'Add the First Page Button.
            If CurrentPage > 1 Then
                pages.Add(New ListItem("<<", 1))
            Else
                pages.Add(New ListItem("<<", "", False))
            End If

            'Add the Previous Button.
            If CurrentPage > 1 Then
                pages.Add(New ListItem("<", CurrentPage - 1))
            Else
                pages.Add(New ListItem("<", "", False))
            End If

            For i As Integer = startIndex To endIndex
                pages.Add(New ListItem(i.ToString(), i, i <> CurrentPage))
            Next

            'Add the Next Button.
            If CurrentPage < PageCount Then
                pages.Add(New ListItem(">", (CurrentPage + 1)))
            Else
                pages.Add(New ListItem(">", "", False))
            End If

            'Add the Last Button.
            If CurrentPage <> PageCount Then
                pages.Add(New ListItem(">>", PageCount))
            Else
                pages.Add(New ListItem(">>", "", False))
            End If
            PagerControl.DataSource = pages
            PagerControl.DataBind()
            Return PageObject
        End Function

    End Class

End Namespace

