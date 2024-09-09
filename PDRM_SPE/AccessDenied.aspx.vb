﻿Imports AppCode.BusinessLogic

Public Class AccessDenied
    Inherits Base

    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
        If User.IsInRole("A") Then
            Me.MasterPageFile = "admins/Admin.Master"
        ElseIf User.IsInRole("M") Then
            Me.MasterPageFile = "member/Member.Master"
        End If
    End Sub

    Protected Overloads Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            SetText()
        End If
    End Sub

#Region "Language"

    Private Sub SetText()
        'Header/Title
        lblPageTitle.Text = GetText("AccessDenied")
        lblHeader.Text = GetText("AccessDenied")
        'lblMenuList.Text = GetText("MenuList")
        'access denied
        lblAccessDenied.Text = GetText("ErrorAccessDenied")
    End Sub

#End Region

End Class