Imports System.IO
Imports System.Reflection
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

        For i As Integer = 1 To 25
            Dim row As String() = New String() {i.ToString, " ", " "}
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
        Clear_data()
        isi_data()
        label.Print(1)
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

        For i As Integer = 1 To 25
            label.Variables("operator_" & i.ToString).SetValue(DataGridView2.Item(1, i - 1).Value)
            label.Variables("ID_" & i.ToString).SetValue(DataGridView2.Item(2, i - 1).Value)
            label.Variables("process_" & i.ToString).SetValue(DataGridView2.Item(3, i - 1).Value)
        Next

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
End Class