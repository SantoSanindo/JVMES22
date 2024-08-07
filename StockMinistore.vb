﻿Imports System.ComponentModel
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar
Imports Microsoft.Office.Interop

Public Class StockMinistore
    Public Shared menu As String = "Stock Card Mini Store"

    Private Sub StockMinistore_Load(sender As Object, e As EventArgs) Handles Me.Load
        DateTimePicker1.MaxDate = DateTime.Now
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "yyyy-MM-dd"

        DateTimePicker2.Format = DateTimePickerFormat.Custom
        DateTimePicker2.CustomFormat = "yyyy-MM-dd"
    End Sub

    Private Sub DGV_StockMiniststore()
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [MTS_NO] [Mts],[STATUS] [Status],[MATERIAL] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [QTY] [Qty], [ACTUAL_QTY] [ACT_QTY], [FINISH_GOODS_PN] [FG], [PO], [SUB_PO] [SPO], [SUB_SUB_PO] [SSPO], [LINE] [Line],[QRCODE] [QR Code], [SPLIT_MATERIAL] [Split Material],INSERT_WHO [Scan By], DATETIME_SAVE [Date Time Save], SAVE_WHO [Save By], PRODUCTION_REQUEST_DATETIME [Date Time Production Request], PRODUCTION_REQUEST_WHO [Scan Production Request] FROM STOCK_CARD where department='" & globVar.department & "' and CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and status in ('Receive From Main Store','Receive From Production','Production Request','Return To Main Store') order by datetime_insert"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            DataGridView1.DataSource = dtInputStockDetail
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles DataGridView1.DataBindingComplete
        For i As Integer = 0 To DataGridView1.RowCount - 1
            If DataGridView1.Rows(i).Index Mod 2 = 0 Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With DataGridView1
            .DefaultCellStyle.Font = New Font("Tahoma", 14)

            For i As Integer = 0 To .ColumnCount - 1
                .Columns(i).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Next

            .EnableHeadersVisualStyles = False
            With .ColumnHeadersDefaultCellStyle
                .BackColor = Color.Navy
                .ForeColor = Color.White
                .Font = New Font("Tahoma", 13, FontStyle.Bold)
                .Alignment = HorizontalAlignment.Center
                .Alignment = ContentAlignment.MiddleCenter
            End With

            '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            '.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub btn_ExportTrace1_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace1.Click
        If globVar.view > 0 Then
            ' Tampilkan ProgressBar
            ProgressBar1.Visible = True
            ProgressBar1.Style = ProgressBarStyle.Marquee
            ProgressBar1.MarqueeAnimationSpeed = 30

            ' Nonaktifkan tombol selama ekspor
            btn_ExportTrace1.Enabled = False

            ' Jalankan backgroundWorker
            BackgroundWorker1.RunWorkerAsync(DataGridView1)
        End If
    End Sub

    Private Sub exportToExcel(ByVal dgv As DataGridView)

        ' membuat objek Excel dan workbook baru
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

        ' mengisi data ke worksheet
        Dim dgvColumnIndex As Integer = 0
        For Each column As DataGridViewColumn In dgv.Columns
            dgvColumnIndex += 1
            xlWorkSheet.Cells(1, dgvColumnIndex) = column.HeaderText
        Next
        Dim dgvRowIndex As Integer = 0
        For Each row As DataGridViewRow In dgv.Rows
            dgvRowIndex += 1
            dgvColumnIndex = 0
            For Each cell As DataGridViewCell In row.Cells
                dgvColumnIndex += 1
                xlWorkSheet.Cells(dgvRowIndex + 1, dgvColumnIndex) = cell.Value
            Next
        Next

        ' menyimpan workbook ke file Excel
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel Workbook|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"
        saveFileDialog1.ShowDialog()

        If saveFileDialog1.FileName <> "" Then
            xlWorkBook.SaveAs(saveFileDialog1.FileName)
            RJMessageBox.Show("File saved successfully.")
        End If

        ' membersihkan objek Excel
        xlWorkBook.Close()
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If globVar.view > 0 Then
            DGV_StockMiniststore()
            btn_ExportTrace1.Enabled = True
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.MinDate = DateTimePicker1.Value
        DateTimePicker2.MaxDate = DateTime.Now.AddDays(1)
        btn_ExportTrace1.Enabled = False
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        e.Handled = True
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        e.Handled = True
    End Sub

    Private Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        btn_ExportTrace1.Enabled = False
        DataGridView1.DataSource = Nothing
        DataGridView1.Rows.Clear()
        DataGridView1.Columns.Clear()
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim dgv As DataGridView = CType(e.Argument, DataGridView)

        ' Membuat objek Excel dan workbook baru
        Dim xlApp As New Excel.Application
        Dim xlWorkBook As Excel.Workbook = xlApp.Workbooks.Add
        Dim xlWorkSheet As Excel.Worksheet = xlWorkBook.Worksheets(1)

        ' Mengisi data ke worksheet
        Dim dgvColumnIndex As Integer = 0
        For Each column As DataGridViewColumn In dgv.Columns
            dgvColumnIndex += 1
            xlWorkSheet.Cells(1, dgvColumnIndex) = column.HeaderText
        Next
        Dim dgvRowIndex As Integer = 0
        For Each row As DataGridViewRow In dgv.Rows
            dgvRowIndex += 1
            dgvColumnIndex = 0
            For Each cell As DataGridViewCell In row.Cells
                dgvColumnIndex += 1
                xlWorkSheet.Cells(dgvRowIndex + 1, dgvColumnIndex) = cell.Value
            Next
        Next

        ' Menyimpan workbook ke file Excel
        Dim saveFileDialog1 As New SaveFileDialog()
        saveFileDialog1.Filter = "Excel Workbook|*.xlsx"
        saveFileDialog1.Title = "Save Excel File"

        ' Gunakan Invoke untuk berinteraksi dengan UI thread
        Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim fileName As String = "Export_Stock_Ministore_" & DateTime.Now.ToString("yyyyMMdd") & ".xlsx"
        Dim filePath As String = System.IO.Path.Combine(desktopPath, fileName)
        xlWorkBook.SaveAs(filePath)
        RJMessageBox.Show("Success Export Stock Ministore and save into " & filePath, "Info", MessageBoxButtons.OK)

        ' Membersihkan objek Excel
        xlWorkBook.Close()
        xlApp.Quit()
        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)
    End Sub

    Private Sub backgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ProgressBar1.Visible = False
        btn_ExportTrace1.Enabled = True
    End Sub
End Class