﻿Imports System.IO
Imports System.Reflection
Imports NiceLabel.SDK

Public Class _PrintingDefect
    Dim label As ILabel

    Dim printers As IList(Of IPrinter)
    Dim selected_Printer As IPrinter
    Private Sub _PrintingDefect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializePrintEngine()

        Dim appPath As String = Application.StartupPath()
        label = PrintEngineFactory.PrintEngine.OpenLabel(appPath & "\Label\" & "Defect.nlbl")

    End Sub

    Private Sub InitializePrintEngine()
        Try
            Dim sdkFilesPath As String = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\SDKFiles")
            'MsgBox(sdkFilesPath)

            If My.Computer.FileSystem.DirectoryExists(sdkFilesPath) Then
                PrintEngineFactory.SDKFilesPath = sdkFilesPath
            End If
            PrintEngineFactory.PrintEngine.Initialize()
            'MsgBox("test")
        Catch ex As Exception
            MsgBox("Initialization of the SDK failed." + Environment.NewLine + Environment.NewLine + ex.ToString())
            Me.Close()
        End Try
    End Sub

    Public Sub btn_Print_Click(sender As Object, e As EventArgs) Handles btn_Print.Click
        Try
            Clear_data()
            isi_data()
            label.Print(1)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub isi_data()
        Application.DoEvents()

        label.Variables("unique_ID").SetValue(txt_Unique_id.Text)
        label.Variables("part_number").SetValue(txt_part_number.Text)
        label.Variables("part_description").SetValue(txt_Part_Description.Text)
        label.Variables("traceability").SetValue(txt_Traceability.Text)
        label.Variables("Inv_ctrl_date").SetValue(txt_Inv_crtl_date.Text)

        label.Variables("qr_code").SetValue(txt_QR_Code.Text)
    End Sub
    Private Sub Clear_data()
        Application.DoEvents()

        label.Variables("unique_ID").SetValue("")
        label.Variables("part_number").SetValue("")
        label.Variables("part_description").SetValue("")
        label.Variables("traceability").SetValue("")
        label.Variables("Inv_ctrl_date").SetValue("")

        label.Variables("qr_code").SetValue("")
    End Sub

    Private Sub _PrintingDefect_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        PrintEngineFactory.PrintEngine.Shutdown()
    End Sub

    Private Sub btn_refresh_Click(sender As Object, e As EventArgs) Handles btn_refresh.Click
        Dim appPath As String = Application.StartupPath()
        label = PrintEngineFactory.PrintEngine.OpenLabel(appPath & "\Label\" & "Defect.nlbl")
    End Sub
End Class