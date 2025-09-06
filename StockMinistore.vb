Imports System.ComponentModel
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

        TabControl1.TabIndex = 0
    End Sub

    Private Sub DGV_StockMiniststore()
        Try
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            DataGridView1.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [MTS_NO] [Mts],[STATUS] [Status],[MATERIAL] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [QTY] [Qty], [ACTUAL_QTY] [Act Qty], CASE 
                                                    WHEN qrcode_new is not null
	                                                THEN qrcode_new COLLATE SQL_Latin1_General_CP1_CI_AS 
	                                                ELSE qrcode COLLATE SQL_Latin1_General_CP1_CI_AS  
                                                END AS QRCode, INSERT_WHO [Scan By], DATETIME_SAVE [Date Time Save], SAVE_WHO [Save By], CASE 
                                                    WHEN split_material = 1 
                                                    THEN 'YES' 
                                                    ELSE 'NO' 
                                                END AS [Split Material] FROM STOCK_CARD where department='" & globVar.department & "' and CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and status in ('Receive From Main Store','Receive From Production','Production Request','Return To Main Store') order by datetime_insert"
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

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub btn_ExportTrace1_Click(sender As Object, e As EventArgs) Handles btn_ExportTrace1.Click
        If globVar.view > 0 Then
            ExportToExcelV2()

            'If TabControl1.SelectedIndex = 0 Then

            '    ProgressBar1.Visible = True
            '    ProgressBar1.Style = ProgressBarStyle.Marquee
            '    ProgressBar1.MarqueeAnimationSpeed = 30

            '    btn_ExportTrace1.Enabled = False

            '    BackgroundWorker1.RunWorkerAsync(DataGridView1)

            'ElseIf TabControl1.SelectedIndex = 1 Then

            '    ProgressBar1.Visible = True
            '    ProgressBar1.Style = ProgressBarStyle.Marquee
            '    ProgressBar1.MarqueeAnimationSpeed = 30

            '    btn_ExportTrace1.Enabled = False

            '    BackgroundWorker1.RunWorkerAsync(ReportDGAtas)

            'End If

        End If
    End Sub

    Private Sub ExportToExcelV2()
        If globVar.view > 0 Then

            Dim _datagridview As DataGridView
            Dim nama As String

            If TabControl1.SelectedIndex = 0 Then

                _datagridview = DataGridView1
                nama = "Stock Card Ministore "

            ElseIf TabControl1.SelectedIndex = 1 Then

                _datagridview = ReportDGAtas
                nama = "Report Ministore "

            End If

            If _datagridview.Rows.Count = 0 Then

                RJMessageBox.Show("Cannot export with data empty.")
                Exit Sub

            End If

            Dim xlApp As New Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value

            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")

            ' Mengatur header
            For k As Integer = 1 To _datagridview.Columns.Count
                xlWorkSheet.Cells(1, k) = _datagridview.Columns(k - 1).HeaderText
            Next

            ' Menyalin data ke array dua dimensi
            Dim dataArray(_datagridview.RowCount - 1, _datagridview.ColumnCount - 1) As Object
            For i As Integer = 0 To _datagridview.RowCount - 1
                For j As Integer = 0 To _datagridview.ColumnCount - 1
                    dataArray(i, j) = _datagridview(j, i).Value
                Next
            Next

            ' Menyalin array ke lembar kerja Excel
            xlWorkSheet.Range("A2").Resize(_datagridview.RowCount, _datagridview.ColumnCount).Value = dataArray

            ' Mengatur direktori awal untuk dialog
            FolderBrowserDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            ' Memilih folder penyimpanan
            If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
                Dim directoryPath As String = FolderBrowserDialog1.SelectedPath
                Dim currentDate As Date = DateTime.Now
                Dim namafile As String = nama & DateTimePicker1.Text & " - " & DateTimePicker2.Text & ".xlsx"
                Dim filePath As String = System.IO.Path.Combine(directoryPath, namafile)

                xlWorkSheet.SaveAs(filePath)
                RJMessageBox.Show("Export to Excel Success!" & Environment.NewLine & "Name is " & namafile)
            End If

            ' Membersihkan objek Excel
            xlWorkBook.Close(False)
            xlApp.Quit()

            releaseObject(xlWorkSheet)
            releaseObject(xlWorkBook)
            releaseObject(xlApp)
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
            If TabControl1.SelectedIndex = 0 Then

                DGV_StockMiniststore()
                btn_ExportTrace1.Enabled = True

            ElseIf TabControl1.SelectedIndex = 1 Then

                LoadDGAtas()
                btn_ExportTrace1.Enabled = True

            End If
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

    Private Sub TabControl1_Click(sender As Object, e As EventArgs) Handles TabControl1.Click
        If TabControl1.SelectedIndex = 0 Then

            DGV_StockMiniststore()

        ElseIf TabControl1.SelectedIndex = 1 Then

            LoadDGAtas()

            If ReportDGAtas.RowCount > 0 Then
                btn_ExportTrace1.Enabled = True
            End If

        End If
    End Sub

    Sub LoadDGAtas()
        Try
            ReportDGAtas.DataSource = Nothing
            ReportDGAtas.Rows.Clear()
            ReportDGAtas.Columns.Clear()
            Call Database.koneksi_database()
            'Dim queryInputStockDetail As String = "SELECT 
            '                                         material Material,
            '                                         SUM(qty) AS [Qty Receive From Main Store],
            '                                         SUM(ACTUAL_QTY) AS [Total Actual Qty Mini Store]
            '                                        FROM
            '                                         stock_card 
            '                                        WHERE
            '                                         department = '" & globVar.department & "' 
            '                                         AND CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' 
            '                                         AND CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' 
            '                                         AND status = 'Receive From Main Store'
            '                                        GROUP BY 
            '                                         material
            '                                        order by
            '                                            material"

            Dim queryInputStockDetail As String = "SELECT
                                                        sc.material AS Material,
                                                        SUM(sc.qty) AS [Qty Receive From Main Store],
                                                        SUM(sc.ACTUAL_QTY) AS [Total Actual Qty Receive From Main Store],
                                                        COALESCE((
                                                            SELECT SUM(sc2.qty)
                                                            FROM stock_card sc2
                                                            WHERE sc2.MATERIAL = sc.MATERIAL
                                                                AND sc2.STATUS = 'Receive From Production'
                                                                AND sc2.DEPARTMENT = '" & globVar.department & "'
                                                                AND CAST(sc2.datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "'
                                                                AND CAST(sc2.datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "'
                                                        ), 0) AS [Qty Return],
                                                        COALESCE((
                                                            SELECT SUM(sc2.ACTUAL_QTY)
                                                            FROM stock_card sc2
                                                            WHERE sc2.MATERIAL = sc.MATERIAL
                                                                AND sc2.STATUS = 'Receive From Production'
                                                                AND sc2.DEPARTMENT = '" & globVar.department & "'
                                                                AND CAST(sc2.datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "'
                                                                AND CAST(sc2.datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "'
                                                        ), 0) AS [Total Actual Qty Return],
                                                        COALESCE(SUM(sc.ACTUAL_QTY), 0) + COALESCE((
                                                            SELECT SUM(sc2.ACTUAL_QTY)
                                                            FROM stock_card sc2
                                                            WHERE sc2.MATERIAL = sc.MATERIAL
                                                                AND sc2.STATUS = 'Receive From Production'
                                                                AND sc2.DEPARTMENT = '" & globVar.department & "'
                                                                AND CAST(sc2.datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "'
                                                                AND CAST(sc2.datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "'
                                                        ), 0) AS [Actual Qty Mini Store (Act Qty Receive + Act Qty Return)]
                                                    FROM
                                                        stock_card sc
                                                    WHERE
                                                        sc.department = '" & globVar.department & "' 
                                                        AND CAST(sc.datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' 
                                                        AND CAST(sc.datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' 
                                                        AND sc.status = 'Receive From Main Store'
                                                    GROUP BY
                                                        sc.material
                                                    ORDER BY
                                                        sc.material"

            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            ReportDGAtas.DataSource = dtInputStockDetail

            Dim deleteProductionRequest As DataGridViewButtonColumn = New DataGridViewButtonColumn
            deleteProductionRequest.Name = "check"
            deleteProductionRequest.HeaderText = "Check"
            deleteProductionRequest.Width = 50
            deleteProductionRequest.Text = "Check"
            deleteProductionRequest.UseColumnTextForButtonValue = True
            ReportDGAtas.Columns.Insert(0, deleteProductionRequest)

        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReportDGAtas_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles ReportDGAtas.DataBindingComplete
        For i As Integer = 0 To ReportDGAtas.RowCount - 1
            If ReportDGAtas.Rows(i).Index Mod 2 = 0 Then
                ReportDGAtas.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                ReportDGAtas.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With ReportDGAtas
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

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Sub LoadDGBawah(material As String)
        Try
            ReportDGBawah.DataSource = Nothing
            ReportDGBawah.Rows.Clear()
            ReportDGBawah.Columns.Clear()
            Call Database.koneksi_database()
            Dim queryInputStockDetail As String = "SELECT [datetime_insert] [Date Time], [MTS_NO] [Mts],[STATUS] [Status],[MATERIAL] [Material], [INV_CTRL_DATE] [ICD], [TRACEABILITY] [Trace], [BATCH_NO] [Batch], [LOT_NO] [Lot], [QTY] [Qty], [ACTUAL_QTY] [Act Qty], CASE 
                                                        WHEN qrcode_new is not null
		                                                THEN qrcode_new COLLATE SQL_Latin1_General_CP1_CI_AS 
		                                                ELSE qrcode COLLATE SQL_Latin1_General_CP1_CI_AS  
                                                    END AS QRCode, INSERT_WHO [Scan By], DATETIME_SAVE [Date Time Save], SAVE_WHO [Save By] FROM STOCK_CARD where department='" & globVar.department & "' and CAST(datetime_insert AS DATE) >= '" & DateTimePicker1.Text & "' and CAST(datetime_insert AS DATE) <= '" & DateTimePicker2.Text & "' and status in ('Receive From Main Store','Receive From Production') and material='" & material & "' order by datetime_insert, status"
            Dim dtInputStockDetail As DataTable = Database.GetData(queryInputStockDetail)
            ReportDGBawah.DataSource = dtInputStockDetail
        Catch ex As Exception
            RJMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ReportDGBawah_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles ReportDGBawah.DataBindingComplete
        For i As Integer = 0 To ReportDGBawah.RowCount - 1
            If ReportDGBawah.Rows(i).Index Mod 2 = 0 Then
                ReportDGBawah.Rows(i).DefaultCellStyle.BackColor = Color.LightBlue
            Else
                ReportDGBawah.Rows(i).DefaultCellStyle.BackColor = Color.LemonChiffon
            End If
        Next i

        With ReportDGBawah
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

            .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.Fill
        End With
    End Sub

    Private Sub ReportDGAtas_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles ReportDGAtas.CellClick

        If e.RowIndex = -1 Then
            Exit Sub
        End If

        If e.ColumnIndex = -1 Then
            Exit Sub
        End If

        If ReportDGAtas.Columns(e.ColumnIndex).Name = "check" Then
            LoadDGBawah(ReportDGAtas.Rows(e.RowIndex).Cells("Material").Value)
        End If
    End Sub
End Class