Imports System.IO
Imports System.Reflection
Imports System.Security.Claims
Imports NiceLabel.SDK

Public Class _PrintingFlowTicket
    Dim label As ILabel

    Dim printers As IList(Of IPrinter)
    Dim selected_Printer As IPrinter

    Private Sub _PrintingFlowTicket_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializePrintEngine()

        Dim appPath As String = Application.StartupPath()
        label = PrintEngineFactory.PrintEngine.OpenLabel(appPath & "\Label\" & "FlowTicket.nlbl")

        For i As Integer = 1 To 10
            Dim row As String() = New String() {i.ToString, " ", " "}
            DataGridView1.Rows.Add(row)
        Next

        For i As Integer = 1 To 30
            Dim row As String() = New String() {i.ToString, "", "", ""}
            DataGridView2.Rows.Add(row)
        Next

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
            compress_line()
            isi_data()
            label.Print(1)
            globVar.failPrint = "No"
        Catch ex As Exception
            MsgBox(ex.Message)
            globVar.failPrint = "Yes"
        End Try
    End Sub

    Private Sub isi_data()
        Application.DoEvents()

        label.Variables("FG_Part_Number").SetValue(txt_fg_part_number.Text)
        label.Variables("part_description").SetValue(txt_part_description.Text)
        label.Variables("Line_No").SetValue(txt_Line_No.Text)
        label.Variables("date_code").SetValue(txt_date_code.Text)

        label.Variables("po_number").SetValue(txt_PO_Number.Text)
        label.Variables("qty_po").SetValue(txt_Quantity_PO.Text)
        label.Variables("Qty_per_Lot").SetValue(txt_Qty_per_Lot.Text)
        label.Variables("Lot_No").SetValue(txt_Lot_No.Text)

        label.Variables("qr_code").SetValue(txt_QR_Code.Text)

        For i As Integer = 1 To 10
            label.Variables("component_pn_" & i.ToString).SetValue(DataGridView1.Item(1, i - 1).Value)
            label.Variables("description_" & i.ToString).SetValue(DataGridView1.Item(2, i - 1).Value)
        Next

        label.Variables("txt_compress").SetValue(txt_compress.Text)

        'For i As Integer = 1 To 25
        '    label.Variables("operator_" & i.ToString).SetValue(DataGridView2.Item(1, i - 1).Value)
        '    label.Variables("ID_" & i.ToString).SetValue(DataGridView2.Item(2, i - 1).Value)
        '    label.Variables("process_" & i.ToString).SetValue(DataGridView2.Item(3, i - 1).Value)
        'Next

    End Sub

    Private Sub compress_line()
        Dim header_rtf = "{\rtf1\deff0{\fonttbl{\f0 Arial;}}"
        Dim a_char = "{\f0\fs12\cf0 "
        Dim b_tab_char = "}{\f0\fs12\cf0\tab "
        Dim No_label As String
        Dim tab_label = "\tab "
        Dim optr_label As String
        Dim Id_label As String
        Dim process_label As String
        Dim last_format = "{\par\f0\fs11\cf0 -----------------------------------------------------------------------------------------------------------------} "
        Dim end_header = "}\f0\fs16\par}"
        txt_compress.Text = header_rtf & a_char
        For jml_row As Integer = 0 To DataGridView2.Rows.Count - 2
            Dim int_No_label As Integer = DataGridView2.Item(0, jml_row).Value
            No_label = int_No_label.ToString("D2")
            optr_label = DataGridView2.Item(1, jml_row).Value
            Id_label = DataGridView2.Item(2, jml_row).Value
            process_label = DataGridView2.Item(3, jml_row).Value
            txt_compress.Text = txt_compress.Text & No_label & "  " & optr_label & tab_label & Id_label & tab_label & process_label & tab_label & tab_label & "|" & tab_label & "|" & last_format
        Next
        txt_compress.Text = txt_compress.Text & end_header
    End Sub

    Private Sub Clear_data()
        Application.DoEvents()

        label.Variables("FG_Part_Number").SetValue("")
        label.Variables("part_description").SetValue("")
        label.Variables("Line_No").SetValue("")
        label.Variables("date_code").SetValue("")

        label.Variables("po_number").SetValue("")
        label.Variables("qty_po").SetValue("")
        label.Variables("Qty_per_Lot").SetValue("")
        label.Variables("Lot_No").SetValue("")

        label.Variables("qr_code").SetValue("")

        For i As Integer = 1 To 10
            label.Variables("Component_PN_" & i.ToString).SetValue("")
            label.Variables("Description_" & i.ToString).SetValue("")
        Next

        For i As Integer = 1 To 25
            label.Variables("operator_" & i.ToString).SetValue("")
            label.Variables("ID_" & i.ToString).SetValue("")
            label.Variables("process_" & i.ToString).SetValue("")
        Next

    End Sub

    Private Sub _PrintingFlowTicket_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        PrintEngineFactory.PrintEngine.Shutdown()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        For i As Integer = 1 To 10
            DataGridView1.Rows(i - 1).Cells(1).Value = i * 10
            DataGridView1.Rows(i - 1).Cells(2).Value = i
        Next

        For i As Integer = 1 To 25
            DataGridView2.Rows(i - 1).Cells(1).Value = i
            DataGridView2.Rows(i - 1).Cells(2).Value = i * 10
            DataGridView2.Rows(i - 1).Cells(3).Value = i * 100
        Next
    End Sub

    Private Sub btn_Refresh_Click(sender As Object, e As EventArgs) Handles btn_Refresh.Click
        Dim appPath As String = Application.StartupPath()
        label = PrintEngineFactory.PrintEngine.OpenLabel(appPath & "\Label\" & "FlowTicket.nlbl")
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        compress_line()
    End Sub
End Class