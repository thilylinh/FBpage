﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Namespace WebApplication1

    Partial Public Class doan5_docbaoEntities
        Inherits DbContext
    
        Public Sub New()
            MyBase.New("name=doan5_docbaoEntities")
        End Sub
    
        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            Throw New UnintentionalCodeFirstException()
        End Sub
    
        Public Overridable Property BAIDANGs() As DbSet(Of BAIDANG)
        Public Overridable Property CHUYENMUCs() As DbSet(Of CHUYENMUC)
        Public Overridable Property LUOTXEMs() As DbSet(Of LUOTXEM)
        Public Overridable Property TAIKHOANs() As DbSet(Of TAIKHOAN)
        Public Overridable Property THELOAIs() As DbSet(Of THELOAI)
    
    End Class

End Namespace
