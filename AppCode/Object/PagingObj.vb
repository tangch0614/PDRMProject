Namespace BusinessObject
    Public Class PagingObj
        Private _dataTable As DataTable = Nothing
        Private _currentPageIndex As Integer = 0
        Private _pageCount As Integer = 0
        Private _pageSize As Integer = 0
        Private _buttonCount As Integer = 10
        Private _recordCount As Integer = 0
        Private _buttonIDPrefix As String = String.Empty

        Public Property DataTable() As DataTable
            Get
                Return _dataTable
            End Get
            Set(value As DataTable)
                _dataTable = value
            End Set
        End Property
        Public Property CurrentPageIndex() As Integer
            Get
                Return _currentPageIndex
            End Get
            Set(value As Integer)
                _currentPageIndex = value
            End Set
        End Property
        Public Property PageCount As Integer
            Get
                Return _pageCount
            End Get
            Set(value As Integer)
                _pageCount = value
            End Set
        End Property
        Public Property PageSize As Integer
            Get
                Return _pageSize
            End Get
            Set(value As Integer)
                _pageSize = value
            End Set
        End Property
        Public Property ButtonCount As Integer
            Get
                Return _buttonCount
            End Get
            Set(value As Integer)
                _buttonCount = value
            End Set
        End Property
        Public Property RecordCount As Integer
            Get
                Return _recordCount
            End Get
            Set(value As Integer)
                _recordCount = value
            End Set
        End Property
        Public Property ButtonIDPrefix As String
            Get
                Return _buttonIDPrefix
            End Get
            Set(value As String)
                _buttonIDPrefix = value
            End Set
        End Property
    End Class
End Namespace
