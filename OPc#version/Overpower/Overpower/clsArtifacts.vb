﻿Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel
Imports System.Drawing

<Serializable> Public Class Artifact : Inherits System.Windows.Forms.PictureBox
    Public ID As Integer
    Dim toUseType As Char
    Dim toUseValue As Integer = 6
    Dim tmToUseType As Char
    Dim tmToUseValue As Integer
    Public effect As String
    Dim lessThan As Boolean
    Dim opd As Boolean
    Public placed As Boolean = False
    Public wide As Boolean = False
    Public played As Boolean = False
    Public hit As Boolean = False
    Public perm As Boolean = False
    Public attack As Boolean = False
    Public defend As Boolean = False
    Public placedOn As String
    Public hitOn As String
    Public commonname As String
    Public rotated = False

    Public ImageFront As Image
    Dim imgFront As String
    Dim imgBack As String

    Public Sub New()
        Width = 64
        Height = 90
        Visible = False
        Anchor = AnchorStyles.None
        SizeMode = PictureBoxSizeMode.StretchImage
        Location = Cursor.Position
    End Sub

    Public Sub New(ByVal artid As Integer)
        ID = artid
        Visible = False
        Anchor = AnchorStyles.None
        SizeMode = PictureBoxSizeMode.StretchImage

        Dim dt As New OPCardsDataSet.artifactDataTable
        Dim da As New OPCardsDataSetTableAdapters.artifactTableAdapter
        Dim dr As New DataTableReader(dt)

        'find artifact in database using ID and fill table
        da.FillByID(dt, ID)

        'add artifact's data
        Using (dr)
            While (dr.Read)
                toUseType = dr.GetString(dr.GetOrdinal("touse_skill"))
                toUseValue = dr.GetInt32(dr.GetOrdinal("touse_power"))
                tmToUseType = dr.GetString(dr.GetOrdinal("teammate_skill"))
                tmToUseValue = dr.GetInt32(dr.GetOrdinal("teammate_power"))
                effect = dr.GetString(dr.GetOrdinal("effect"))
                opd = dr.GetInt32(dr.GetOrdinal("opd"))
                commonname = dr.GetString(dr.GetOrdinal("commonname"))
                imgFront = Application.StartupPath + "\Card Images\" + dr.GetString(dr.GetOrdinal("image"))
            End While
        End Using

        imgFront = imgFront.Replace("/", "\")
        ImageFront = Image.FromFile(imgFront)
        imgBack = "http://overpower.ca/cards/misc/op_back.png"
        Image = ImageFront
        If tmToUseValue = 5 Then
            lessThan = True
        End If
        commonname = Replace(commonname, " (Lost Promo)", "")
    End Sub
End Class
