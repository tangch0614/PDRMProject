' ----------------------------------------------------------------------- 
' <copyright file="AppConfiguration.vb" company="Imar.Spaanjaars.Com"> 
' Copyright 2008 - 2009 - Imar.Spaanjaars.Com. All rights reserved. 
' Translation to VB.NET by: ClearMedia bvba - sven@clearmedia.be
' </copyright> 
' ----------------------------------------------------------------------- 

Imports System.Configuration

''' <summary> 
''' The AppConfiguaration class contains read-only properties that are essentially short cuts to settings in the web.config file. 
''' </summary> 
Public Module AppConfiguration

#Region "Public Properties"

	''' <summary>Returns the connectionstring for the application.</summary> 
	Public ReadOnly Property ConnectionString() As String
		Get
            Return ConfigurationManager.ConnectionStrings(ConnectionStringName).ConnectionString
		End Get
	End Property

	''' <summary>Returns the name of the current connectionstring for the application.</summary> 
	Public ReadOnly Property ConnectionStringName() As String
		Get
			Return ConfigurationManager.AppSettings("ConnectionStringName")
		End Get
	End Property

#End Region

End Module

